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
using FF.BusinessObjects;
using System.IO;
using System.Runtime.InteropServices;
namespace FF.WindowsERPClient.Reports.Sales
{
    public partial class ReportViewer : Base
    {
        #region reports
        public ListView _lstView = new ListView();
        private InvoiceHalfPrints invReport1 = new InvoiceHalfPrints();
        private InvoiceHalfPrints_Full invReport3 = new InvoiceHalfPrints_Full();
        private InvoicePOSPrint invReportPOS = new InvoicePOSPrint();     //kapila 29/6/2015
        private InvoiceFullPrints invfullReport = new InvoiceFullPrints();
        private SInvoiceHalfPrints SinvReport1 = new SInvoiceHalfPrints();
        private InsurancePrint insReport2 = new InsurancePrint();
        private SInsuPrints SinsReport1 = new SInsuPrints();

        private ReceiptPrints recreport1 = new ReceiptPrints();
        private ReceiptPrints_n recreport1_n = new ReceiptPrints_n();
        private DealerReceiptPrint delrecreport1 = new DealerReceiptPrint();
        private SReceiptPrints Srecreport1 = new SReceiptPrints();
        //private SalesSummary1 _SalesSumReport = new SalesSummary1();
        private DealerInvoicePrints _DealerinvAuto = new DealerInvoicePrints();
        private DealerCreditInvoicePrints _DealerinvAutoCred = new DealerCreditInvoicePrints();
        private DealerCreditInvoicePrints_AFSL _DealerinvAutoCredAFSL = new DealerCreditInvoicePrints_AFSL();
        private DealerInvoicePrints _DealerinvBOC = new DealerInvoicePrints();
        private InvoicePrints _DealerinvReport = new InvoicePrints();
        private InvoicePrint_insus _DealerinvInsReport = new InvoicePrint_insus();
        private ReceiptPrintDealers _DealerrecReport = new ReceiptPrintDealers();
        private InvoicePrintTax _TaxinvReport = new InvoicePrintTax();
        private InvoicePrint_AST _invReportAST = new InvoicePrint_AST();
        private InvoicePrintTax_insus _TaxinvInsReport = new InvoicePrintTax_insus();
        private InvoiceDutyFree _DfInsReport = new InvoiceDutyFree();
        private sInvoiceDutyFree _sDfInsReport = new sInvoiceDutyFree();
        private InvoiceDutyFreeEdison _DfInvEdi = new InvoiceDutyFreeEdison();
        private InvoiceDutyFree_CLC _DfInvCLC = new InvoiceDutyFree_CLC();
        private InvoiceDutyFree_PP _DfInvPP = new InvoiceDutyFree_PP();
        private RCCPrint_New _rccPrint = new RCCPrint_New();
        private RCCPrint_New_Full _rccPrintfull = new RCCPrint_New_Full();
        private RCCPrint_Ack _rccPrintAck = new RCCPrint_Ack();
        private RCCPrint_Ack_Full _rccPrintAckFull = new RCCPrint_Ack_Full();
        private SRCCPrint_Ack S_rccPrintAck = new SRCCPrint_Ack();
        private SRCCPrint_New S_rccPrint = new SRCCPrint_New();
        private InvoiceHalfPrintNew invReport2 = new InvoiceHalfPrintNew();
        private InvoicePrints_Gold _invGold = new InvoicePrints_Gold();
        private HpReceiptPrints _HpRec = new HpReceiptPrints();
        private AOACollection _aoaColl = new AOACollection();
         
        private FF.WindowsERPClient.Reports.Service.Job_Invoice _JobInvoice = new FF.WindowsERPClient.Reports.Service.Job_Invoice();
        private FF.WindowsERPClient.Reports.Service.Job_Invoice_ABE _JobInvoiceABE = new FF.WindowsERPClient.Reports.Service.Job_Invoice_ABE();
        private FF.WindowsERPClient.Reports.Service.Job_Invoice_Phone _JobInvoicePh = new FF.WindowsERPClient.Reports.Service.Job_Invoice_Phone();
        private ACInsReqPrint _acInsReqPrint = new ACInsReqPrint();
        private FF.WindowsERPClient.Reports.Sales.InvoiceAuto _InvoiceAuto = new FF.WindowsERPClient.Reports.Sales.InvoiceAuto(); //Tharanga 2017/06/15
        private ConsignmentReceiptPrint recConreport1 = new ConsignmentReceiptPrint();
        private InvoicePrints_New1 invReportnew1 = new InvoicePrints_New1();
        private FF.WindowsERPClient.Reports.Service.Job_InvoiceNew _JobInvoiceNew = new FF.WindowsERPClient.Reports.Service.Job_InvoiceNew();
        private Invoice_summary _Invoice_summary = new Invoice_summary();//Tharanga 2017/06/24
        private ReceptPrintNew _ReceptPrintNew = new ReceptPrintNew();//tharanga 2017/07/03
        private FF.WindowsERPClient.Reports.Service.clsServiceRep _clsServiceRep = new FF.WindowsERPClient.Reports.Service.clsServiceRep(); //tharanga 2017/07/07

        private giftvoucher giftvoucherprint = new giftvoucher();
        private InvPrints_Full_Abstract _InvPrints_Full_Abstract = new InvPrints_Full_Abstract();

        private Sales_Order_Sum_New _sosumrpt = new Sales_Order_Sum_New();
        #endregion
        Base bsObj;
        clsSalesRep obj = new clsSalesRep();



        public ReportViewer()
        {
            InitializeComponent();

        }

        private void ExcessShort()
        {
            obj.ExcessShortPrint();
            this.Text = "Excess Short";
            crystalReportViewer1.ReportSource = obj._excesShort;
            crystalReportViewer1.RefreshReport();
        }
        private void BOCCusReceipt()
        {
            obj.BOCCustReceipt();
            this.Text = "Booking Infor";
            crystalReportViewer1.ReportSource = obj._bocCusReceipt;
            crystalReportViewer1.RefreshReport();
        }

        private void TransVariationPrint()
        {
            obj.TransVariationPrint();
            this.Text = "Transaction Variation";
            crystalReportViewer1.ReportSource = obj._Variation;
            crystalReportViewer1.RefreshReport();
        }

        private void ExtendedWarrantyPrint()
        {
            obj.ExtendedWarrantyPrint();
            crystalReportViewer1.ReportSource = obj._extendedWar;
            this.Text = "Extended Warranty";
            crystalReportViewer1.RefreshReport();
        }

        private void RemitanceDetPrint()
        {
            obj.RemitanceDetPrint();
            this.Text = "Remitance Detail";
            crystalReportViewer1.ReportSource = obj._remit_det;
            crystalReportViewer1.RefreshReport();
        }

        private void SOSPrint()
        {
            obj.SOSPrint();
            this.Text = "SOS";
            crystalReportViewer1.ReportSource = obj._sos;
            crystalReportViewer1.RefreshReport();
        }


        private void DebtorSales_ReceiptsPrint()
        {
            obj.DebtorSales_ReceiptsPrint();
            this.Text = "Debtor Sales Receipts";
            crystalReportViewer1.ReportSource = obj._DebtSalesRec;
            crystalReportViewer1.RefreshReport();
        }
        private void ExchangeCreditNote()
        {
            obj.ExchangeCreditNotesPrint();
            this.Text = "Exchange Credit Note Details";
            crystalReportViewer1.ReportSource = obj._excrnote;
            crystalReportViewer1.RefreshReport();
        }
        private void PrintDiscountReport()
        {
            obj.GetDiscountReportDetails();
            this.Text = "Discount Report";
            crystalReportViewer1.ReportSource = obj._discRep;
            crystalReportViewer1.RefreshReport();
        }

        private void InsuranceCoverNote()
        {
            obj.InsuranceCoverNote();
            this.Text = "Insurance Cover Note";
            if (BaseCls.GlbReportName == "InsuranceCoverNote.rpt")
            {
                crystalReportViewer1.ReportSource = obj._insCover;
            }
            if (BaseCls.GlbReportName == "InsuranceCoverNoteMBSL.rpt")
            {
                crystalReportViewer1.ReportSource = obj._insCoverMBSL;
            }
            if (BaseCls.GlbReportName == "InsuranceCoverNoteJS.rpt")
            {
                crystalReportViewer1.ReportSource = obj._insCoverJS;
            }
            if (BaseCls.GlbReportName == "InsuranceCoverNoteUMS.rpt")
            {
                crystalReportViewer1.ReportSource = obj._insCoverUMS;
            }
            if (BaseCls.GlbReportName == "InsuranceCoverNoteAIA.rpt")
            {
                crystalReportViewer1.ReportSource = obj._insCoverAIA;
            }

            crystalReportViewer1.RefreshReport();
        }
        private void DeliveredSalesWithSerial()
        {
            obj.DeliveredSalesWithSerial();
            this.Text = "Delivered Sales With Serial";
            crystalReportViewer1.ReportSource = obj._delSerlSales;
            crystalReportViewer1.RefreshReport();
        }
        private void PriceDetReport()
        {
            obj.PriceDetailReport();
            crystalReportViewer1.ReportSource = obj._priceDet;
            crystalReportViewer1.RefreshReport();
        }
        //hasith call object
        private void CreditNoteDetails()
        {
            obj.CreditNoteDetails();
            this.Text = "Creditnote Details";
            crystalReportViewer1.ReportSource = obj._creditnotedetails;
            crystalReportViewer1.RefreshReport();
        }
        private void DealerCommission()
        {
            obj.DealerCommission();
            this.Text = "Dealer Commission";
            crystalReportViewer1.ReportSource = obj._delSerDelComm;
            crystalReportViewer1.RefreshReport();
        }
        private void SalesTargetAchivement()
        {
            obj.SalesTargetAchievementReport();
            this.Text = "Sales Target Achivement";
            crystalReportViewer1.ReportSource = obj._trgt_Ach;
            crystalReportViewer1.RefreshReport();
        }

        private void listAllPrinters()
        {
            foreach (var item in PrinterSettings.InstalledPrinters)
            {
                this.listBox1.Items.Add(item.ToString());
            }
        }



        public static class myPrinters
        {
            [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool SetDefaultPrinter(string Name);

        }


        private void ReturnChequeSettlemtsOutstanding()
        {
            int rowc = 0;
            rowc = obj.ReturnChequeSettlemet();
            this.Text = "Return Cheque Settlemet";
            if (rowc > 0)
            {

                crystalReportViewer1.ReportSource = obj._rtnChe;
                crystalReportViewer1.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data found", "Return Cheque Settlemet", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void InsuranceRegister()
        {
            int insVal = 0;

            insVal = obj.InsuranceRegistrationnew();

            this.Text = "Insurance Register";
            if (insVal == 0)
            {
                crystalReportViewer1.ReportSource = obj._insReg;
                crystalReportViewer1.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data found", "Insurance Register", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void InsuranceRegistrationnew()
        {
            int insVal = 0;

            insVal = obj.InsuranceRegistrationnew();

            this.Text = "Insurance Register";
            if (insVal == 0)
            {
                if (BaseCls.GlbReportName == "HPInsuranceRegisterNew.rpt")
                {
                    crystalReportViewer1.ReportSource = obj._insRegnew;
                }
                else if (BaseCls.GlbReportName == "HPInsurancePolicyReport.rpt")
                {
                    crystalReportViewer1.ReportSource = obj._insRegPol;
                }
                crystalReportViewer1.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data found", "Insurance Register", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void InsuranceRegistrationSettlements()
        {
            int insVal = 0;

            insVal = obj.InsuranceRegistrationSettlements();

            this.Text = "Insurance Register";
            if (insVal == 0)
            {
                crystalReportViewer1.ReportSource = obj._insRegSett;
                crystalReportViewer1.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data found", "Insurance Register", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        private void InsuranceCollection()
        {
            int insVal = 0;

            insVal = obj.InsuranceCollection();

            this.Text = "Insurance Collection";
            if (insVal == 0)
            {
                crystalReportViewer1.ReportSource = obj._vehicleInsCollect;
                crystalReportViewer1.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data found", "Insurance Register", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void CancelledDocuments()
        {
            int insVal = 0;

            insVal = obj.CancelledDocuments();

            this.Text = "Cancelled Documents";
            if (insVal == 0)
            {
                crystalReportViewer1.ReportSource = obj._canDoc;
                crystalReportViewer1.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data found", "Cancelled Documnets", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void ManualDocuments()
        {
            int insVal = 0;

            insVal = obj.ManualDocuments();

            this.Text = "Manual Documents";
            if (insVal == 0)
            {
                crystalReportViewer1.ReportSource = obj._manDoc;
                crystalReportViewer1.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data found", "Manual Documnets", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void ReturnChequeSettlements()
        {
            int insVal = 0;

            insVal = obj.ReturnChequeSettlements();

            this.Text = "Return Cheque Settlements";
            if (insVal == 0)
            {
                crystalReportViewer1.ReportSource = obj._rtnCheDet;
                crystalReportViewer1.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data found", "Return Cheque Settlements", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void SummaryOfWeekly()
        {
            int insVal = 0;

            insVal = obj.SummaryOfWeekly();

            this.Text = "Summary Of Weekly";
            if (insVal == 0)
            {
                crystalReportViewer1.ReportSource = obj._rtnSumWeek;
                crystalReportViewer1.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data found", "Summary Of Weekly", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void InsuranceRegistrationClaims()
        {
            int insVal = 0;

            insVal = obj.InsuranceRegistrationClaims();

            this.Text = "Insurance Register";
            if (insVal == 0)
            {
                if (BaseCls.GlbReportName == "HPInsuranceClaimRegister.rpt")
                {
                    crystalReportViewer1.ReportSource = obj._insRegClaim;
                }
                else if (BaseCls.GlbReportName == "HPInsuranceDocumentRequired.rpt")
                {
                    crystalReportViewer1.ReportSource = obj._insRegDoc;
                }
                crystalReportViewer1.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data found", "Insurance Register", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        private void InternalVoucher()
        {
            int insVal = 0;

            insVal = obj.InternalVoucher();

            this.Text = "Internal Voucher";
            if (insVal == 0)
            {
                crystalReportViewer1.ReportSource = obj._intVou;
                crystalReportViewer1.RefreshReport();
            }
        }

        private void SearchVoucher()
        {
            //int insVal = 0;

            obj.SearchVoucher();

            this.Text = "Check Printing Voucher";

            crystalReportViewer1.ReportSource = obj._vouPrn;
            crystalReportViewer1.RefreshReport();

        }
        private void SearchVoucherPrint()
        {
            //int insVal = 0;

            obj.SearchVoucherPrint();

            this.Text = "Check Printing Voucher";

            crystalReportViewer1.ReportSource = obj._vouPrnvou;
            crystalReportViewer1.RefreshReport();

        }

        private void ECDVoucher(DataTable ECD_VOU_PRINT)
        {
            //int insVal = 0;

            obj.ECDVoucher(ECD_VOU_PRINT);

            this.Text = "Check Printing Voucher";

            crystalReportViewer1.ReportSource = obj._ecdVou;
            crystalReportViewer1.RefreshReport();

        }
        private void Load_Channel_dets()
        {

            obj.Load_Channel_dets();

            this.Text = "Channel Details";
            btnPrint.Visible = false;
            crystalReportViewer1.ShowPrintButton = true;
            crystalReportViewer1.ReportSource = obj._chnlPrn;
            crystalReportViewer1.RefreshReport();

        }
        private void Load_Sub_Channel_dets()
        {

            obj.Load_Sub_Channel_dets();

            this.Text = "Sub Channel Details";
            btnPrint.Visible = false;
            crystalReportViewer1.ShowPrintButton = true;
            crystalReportViewer1.ReportSource = obj._sub_chnlPrn;
            crystalReportViewer1.RefreshReport();

        }

        private void Load_Area_dets()
        {

            obj.Load_Area_dets();

            this.Text = "Area Details";
            btnPrint.Visible = false;
            crystalReportViewer1.ShowPrintButton = true;
            crystalReportViewer1.ReportSource = obj._arePrn;
            crystalReportViewer1.RefreshReport();
        }

        private void Load_Allocate_Items()
        {

            obj.Load_Allocate_Items();

            this.Text = "Allocate Item Details";
            btnPrint.Visible = false;
            crystalReportViewer1.ShowPrintButton = true;
            crystalReportViewer1.ReportSource = obj._allocateItm;
            crystalReportViewer1.RefreshReport();
        }

        private void Load_Service_loc_dets()
        {

            obj.Load_Service_loc_dets();

            this.Text = "Service Location Details";
            btnPrint.Visible = false;
            crystalReportViewer1.ShowPrintButton = true;
            crystalReportViewer1.ReportSource = obj._serloc;
            crystalReportViewer1.RefreshReport();
        }

        private void Load_Service_chnl_dets()
        {
            obj.Load_Service_chnl_dets();

            this.Text = "Service Channel Parameter Details";

            crystalReportViewer1.ReportSource = obj._ser_chnlpara;
            crystalReportViewer1.RefreshReport();

        }


        private void Load_Region_dets()
        {

            obj.Load_Region_dets();

            this.Text = "Region Details";
            btnPrint.Visible = false;
            crystalReportViewer1.ShowPrintButton = true;
            crystalReportViewer1.ReportSource = obj._regPrn;
            crystalReportViewer1.RefreshReport();

        }
        private void Load_Zone_dets()
        {

            obj.Load_Zone_dets();

            this.Text = "Zone Details";
            btnPrint.Visible = false;
            crystalReportViewer1.ShowPrintButton = true;
            crystalReportViewer1.ReportSource = obj._zonPrn;
            crystalReportViewer1.RefreshReport();

        }





        private void InvoicePrint_Load(object sender, EventArgs e)
        {
            try
            {
                listAllPrinters();
                lblPrinter.Text = "Default Printer - " + GetDefaultPrinter();

                string _repname = string.Empty;
                string _papersize = string.Empty;
                //if (BaseCls.GlbDefSubChannel == "MCS")
                //{
                //    CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                //}
                //else
                //{
                //    CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                //}
                //if (BaseCls.GlbDefSubChannel == "ELEC")
                //{
                CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                //}
                //else
                if (_repname == null || _repname == "")
                {
                    CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                }
                if (_repname == null || _repname == "")
                {
                    CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, "N/A", BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                }
                if (!(_repname == null || _repname == "")) { GlbReportName = _repname; BaseCls.GlbReportName = _repname; BaseCls.GlbReportPaperSize = _papersize; }
                BaseCls.GlbReportTp = "";

                if (BaseCls.GlbReportName == "RCCPrint_New.rpt" || BaseCls.GlbReportName == "RCCPrint_Ack.rpt" || BaseCls.GlbReportName == "RCCPrint_New_Full.rpt" || BaseCls.GlbReportName == "RCCPrint_Ack_Full.rpt")
                {
                    //   crystalReportViewer1.ShowExportButton = true;
                    RCCPrint();
                    //  btnPrint.Visible = false;
                    //  crystalReportViewer1.ShowPrintButton = true;
                }

                if (BaseCls.GlbReportName == "SRCCPrint_New.rpt")
                {
                    SRCCPrint();
                }
                if (BaseCls.GlbReportName == "ACInsReqPrint.rpt")
                {
                    AcInsReqPrint();
                }

                if (GlbReportName == "InvoiceHalfPrints.rpt" || BaseCls.GlbReportName == "InvoicePOSPrint.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    //crystalReportViewer1.PrintReport();
                    //BaseCls.GlbReportTp = "INV";
                    //CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                    //if (!(_repname == null || _repname == "")) { GlbReportName = _repname; BaseCls.GlbReportName = _repname; BaseCls.GlbReportPaperSize = _papersize; }
                    //BaseCls.GlbReportTp = "";

                    InvociePrint();
                }
                if (GlbReportName == "InvoiceHalfPrints_Full.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    InvociePrintFull();
                }

                if (GlbReportName == "InvoiceFullPrints.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                    btnPrint.Visible = false;
                    //BaseCls.GlbReportTp = "INV";
                    //CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                    //if (!(_repname == null || _repname == "")) { GlbReportName = _repname; BaseCls.GlbReportName = _repname; BaseCls.GlbReportPaperSize = _papersize; }
                    //BaseCls.GlbReportTp = "";

                    InvocieFullPrint();
                }
                if (GlbReportName == "SInvoiceHalfPrints.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    InvociePrint_INV();
                }
                if (GlbReportName == "Job_Invoice.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    InvociePrintService();
                }
                if (GlbReportName == "Job_Invoice_ABE.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    InvociePrintServiceABE();
                }
                if (GlbReportName == "Job_Invoice_Phone.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    InvociePrintServicePhone();
                }

                if (GlbReportName == "InvoicePrints_New1.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    InvociePrint_INV_NEW1();
                }

                if (GlbReportName == "InvoiceHalfPrintNew.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    InvociePrintnew();
                }
                if (GlbReportName == "InvoicePrints_Gold.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    InvoicePrintGold();
                }
                if (GlbReportName == "InvoicePrint_AST.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    InvociePrintAST();
                }

                if (GlbReportName == "InvoicePrintTax.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    SystemAppLevelParam _sysApp = new SystemAppLevelParam();

                    _sysApp = CHNLSVC.Sales.CheckApprovePermission("ARQT030", BaseCls.GlbUserID);
                    crystalReportViewer1.ShowExportButton = false;

                    if (_sysApp.Sarp_cd != null)
                    {
                        crystalReportViewer1.ShowExportButton = true;
                    }
                    else
                    {
                        crystalReportViewer1.ShowExportButton = false;
                    }

                    InvociePrintTax();
                }

                if (GlbReportName == "DealerCreditInvoicePrints.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    SystemAppLevelParam _sysApp = new SystemAppLevelParam();

                    _sysApp = CHNLSVC.Sales.CheckApprovePermission("ARQT030", BaseCls.GlbUserID);
                    crystalReportViewer1.ShowExportButton = false;

                    if (_sysApp.Sarp_cd != null)
                    {
                        crystalReportViewer1.ShowExportButton = true;
                    }
                    else
                    {
                        crystalReportViewer1.ShowExportButton = false;
                    }

                    InvociePrintDealerAutoCred();
                }

                if (GlbReportName == "DealerCreditInvoicePrints_AFSL.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    SystemAppLevelParam _sysApp = new SystemAppLevelParam();

                    _sysApp = CHNLSVC.Sales.CheckApprovePermission("ARQT030", BaseCls.GlbUserID);
                    crystalReportViewer1.ShowExportButton = false;

                    if (_sysApp.Sarp_cd != null)
                    {
                        crystalReportViewer1.ShowExportButton = true;
                    }
                    else
                    {
                        crystalReportViewer1.ShowExportButton = false;
                    }

                    InvociePrintDealerAutoCredAFSL();
                }

                if (GlbReportName == "InvociePrintBOC.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    SystemAppLevelParam _sysApp = new SystemAppLevelParam();

                    _sysApp = CHNLSVC.Sales.CheckApprovePermission("ARQT030", BaseCls.GlbUserID);
                    crystalReportViewer1.ShowExportButton = false;

                    if (_sysApp.Sarp_cd != null)
                    {
                        crystalReportViewer1.ShowExportButton = true;
                    }
                    else
                    {
                        crystalReportViewer1.ShowExportButton = false;
                    }

                    InvociePrintBOC();
                }

                if (GlbReportName == "InvoicePrintTax_insus.rpt")
                {
                    SystemAppLevelParam _sysApp = new SystemAppLevelParam();

                    _sysApp = CHNLSVC.Sales.CheckApprovePermission("ARQT030", BaseCls.GlbUserID);
                    crystalReportViewer1.ShowExportButton = false;

                    if (_sysApp.Sarp_cd != null)
                    {
                        crystalReportViewer1.ShowExportButton = true;
                    }
                    else
                    {
                        crystalReportViewer1.ShowExportButton = false;
                    }
                    // crystalReportViewer1.ShowExportButton = false;
                    InvociePrintTaxInsurance();
                }
                else if (GlbReportName == "SInsuPrints.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    InsurancePrint_INV();
                }

                else if (GlbReportName == "InsurancePrint.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;

                    // BaseCls.GlbReportTp = "INSUR";
                    //CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                    //if (!(_repname == null || _repname == "")) { GlbReportName = _repname; BaseCls.GlbReportName = _repname; BaseCls.GlbReportPaperSize = _papersize; }
                    //BaseCls.GlbReportTp = "";
                    InsurancePrint();
                }



                else if (BaseCls.GlbReportName == "InsuranceCoverNoteAIA.rpt" || BaseCls.GlbReportName == "InsuranceCoverNote.rpt" || BaseCls.GlbReportName == "InsuranceCoverNoteMBSL.rpt" || BaseCls.GlbReportName == "InsuranceCoverNoteJS.rpt" || BaseCls.GlbReportName == "InsuranceCoverNoteUMS.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    InsuranceCoverNote();
                }

                else if (BaseCls.GlbReportName == "PriceDet.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    PriceDetReport();
                }

                else if (BaseCls.GlbReportName == "SalesSummary1.rpt")
                {
                    SalesSummaryPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }

                else if (BaseCls.GlbReportName == "A_Sales_Report.rpt")
                {
                    A_SalesSummaryPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }
                else if (BaseCls.GlbReportName == "SignOn_Slip.rpt" || BaseCls.GlbReportName == "SignOff_Slip.rpt")
                {
                    PrintSignOnSlip();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }
                else if (BaseCls.GlbReportName == "SignOff_Deno.rpt")
                {
                    PrintSignOffDeno();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }

                else if (BaseCls.GlbReportName == "Loyality_Disc_Report.rpt")
                {
                    LoyalityDiscountPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }

                else if (BaseCls.GlbReportName == "HP_SummaryRep.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    SalesSummaryPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                else if (BaseCls.GlbReportName == "PO_Allocation.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    POAllocationPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "DF_Sales_Statement.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DFSalesStatementPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                else if (BaseCls.GlbReportName == "DF_Sales_Currencywise.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DFSalesStatementCurrPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                else if (BaseCls.GlbReportName == "DF_Sales_Currencywise_dtl.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DFSalesStatementCurrDtlPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "DF_Sales_CurrencywiseTr.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DFSalesStatementCurrTrPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                else if (BaseCls.GlbReportName == "DF_Categorywise_Sales.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DFCategorywiseSalesPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                else if (BaseCls.GlbReportName == "DF_Consolidated_Sales.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DFConsolidatedSalesStatementPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "DF_ModelwiseSales.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DFSalesModelwise();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "GPSummary.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    GPSummary();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                    
                else if (BaseCls.GlbReportName == "DF_SalesWithQty.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DFSaleswithQtyPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                else if (BaseCls.GlbReportName == "DF_Sales_Qty.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DFSalesQtyPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "DF_MothlySales.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DFMonthSalesReport(1);
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "DF_WeeklySales.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DFMonthSalesReport(2);
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "DF_ItemCategorywise_Sales.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DFMonthSalesReport(3);
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (GlbReportName == "Age_Debtor_Outstanding.rpt" || GlbReportName == "Age_Debtor_Outstanding_PC.rpt" || GlbReportName == "Age_Debtor_Outstanding_new.rpt" || GlbReportName == "Age_Debtor_Outstanding_PC_new.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    AgeAnalysisOfDebtorsOutstandingPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (GlbReportName == "Age_Debtor_Outstanding_dcn.rpt" || GlbReportName == "Age_Debtor_Outstanding_new_dcn.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    AgeAnalysisOfDebtorsOutstandingDCNPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (GlbReportName == "Age_Debtor_Outstanding_adv.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    AgeAnalysisOfDebtorsOutstandingAdvPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (GlbReportName == "DebtorSettlement.rpt" || GlbReportName == "DebtorSettlement_PC.rpt" || GlbReportName == "DebtorSettlement_Outs_PC.rpt" || GlbReportName == "DebtorSettlement_Outs.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DebtorSettlementPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                else if (GlbReportName == "DebtorSalesReceipts.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DebtorSales_ReceiptsPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "exchange_crnote_report.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    ExchangeCreditNote();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Discount_Report.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    PrintDiscountReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (GlbReportName == "ReceiptPrints.rpt")
                {
                    //BaseCls.GlbReportTp = "REC";
                    //CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                    //if (!(_repname == null || _repname == "")) { GlbReportName = _repname; BaseCls.GlbReportName = _repname; BaseCls.GlbReportPaperSize = _papersize; }
                    //BaseCls.GlbReportTp = "";
                    Receipt_print();
                }
                else if (GlbReportName == "ReceiptPrints_n.rpt")
                {
                    //BaseCls.GlbReportTp = "REC";
                    //CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                    //if (!(_repname == null || _repname == "")) { GlbReportName = _repname; BaseCls.GlbReportName = _repname; BaseCls.GlbReportPaperSize = _papersize; }
                    //BaseCls.GlbReportTp = "";
                    Receipt_print_n();
                }
                else if (GlbReportName == "DealerReceiptPrint.rpt")
                {
                    Receipt_printDealer();
                }


                else if (GlbReportName == "InvoicePrints.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    SystemAppLevelParam _sysApp = new SystemAppLevelParam();

                    _sysApp = CHNLSVC.Sales.CheckApprovePermission("ARQT030", BaseCls.GlbUserID);
                    //crystalReportViewer1.ShowExportButton = false;

                    if (_sysApp.Sarp_cd != null)
                    {
                        crystalReportViewer1.ShowExportButton = true;
                    }
                    else
                    {
                        crystalReportViewer1.ShowExportButton = false;
                    }
                    InvociePrintDealer();
                }
                else if (GlbReportName == "InvoicePrint_insus.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    SystemAppLevelParam _sysApp = new SystemAppLevelParam();

                    _sysApp = CHNLSVC.Sales.CheckApprovePermission("ARQT030", BaseCls.GlbUserID);
                    //crystalReportViewer1.ShowExportButton = false;

                    if (_sysApp.Sarp_cd != null)
                    {
                        crystalReportViewer1.ShowExportButton = true;
                    }
                    else
                    {
                        crystalReportViewer1.ShowExportButton = false;
                    }
                    InvociePrintDealerInsurance();
                }

                else if (GlbReportName == "ReceiptPrintDealers.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    DealerReceipt_print();
                }


                else if (GlbReportName == "SReceiptPrints.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    Receipt_print_Inv();
                }
                else if (GlbReportName == "ConsignmentReceiptPrint.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    Receipt_print_Consignment();
                }



                else if (BaseCls.GlbReportName == "ServiceReceiptPrints.rpt")
                {
                    BaseCls.GlbReportTp = "SERREC";
                    CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                    if (!(_repname == null || _repname == "")) { GlbReportName = _repname; BaseCls.GlbReportName = _repname; BaseCls.GlbReportPaperSize = _papersize; }
                    BaseCls.GlbReportTp = "";
                    crystalReportViewer1.ShowExportButton = false;
                    ServiceReceiptPrint();
                }
                else if (BaseCls.GlbReportName == "SServiceReceiptPrints.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    SServiceReceiptPrint();
                }
                else if (BaseCls.GlbReportName == "ReceivableMovementReports.rpt" || BaseCls.GlbReportName == "ReceivableMovementSummaryReports.rpt")
                {

                    ReceivableMovementRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (GlbReportName == "HpReceiptPrints.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    HPReceipt_print();
                }
                else if (BaseCls.GlbReportName == "DeliveredSalesReport.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DeliveredSalesRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "GP_Detail_Repl.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    GpDetailwithReplacementReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Stock_Sale_Report.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    StockSaleRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Stock_Sale_Report_sum.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    StockSaleRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                else if (BaseCls.GlbReportName == "DeliveredSalesInsuranceReport.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DeliveredSalesInsuRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "DeliveredSalesComparisonReport.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    ComparisonofDeliveredSalesRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                else if (BaseCls.GlbReportName == "DeliveredSalesReport80-20.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    TotalSales8020Rep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                
                else if (BaseCls.GlbReportName == "Delivered_Sales_GRN.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DeliveredSalesGRNRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                else if (BaseCls.GlbReportName == "Delivered_Sales_GRN_Cost.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DeliveredSalesGRNCostRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "PaymodewiseTr_Report.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    PayModeTrRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                else if (BaseCls.GlbReportName == "Del_Conf_Dtl_Report.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DelConfDetRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "DeliveredSalesReport.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DeliveredSalesRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Total_Revenue_Report.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    TotalRevenueRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                else if (BaseCls.GlbReportName == "DeliveredSalesReport_withCust.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DeliveredSalesRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "DeliveredSalesReport_Itemwise.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DeliveredSalesRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (GlbReportName == "Receipt_List.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    ReceiptListingPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (GlbReportName == "Receipt_List_summary.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    ReceiptListingPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Remitance_Sum.rpt" || BaseCls.GlbReportName == "Remitance_Sum_view.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    RemSummaryPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Remitance_Sum_Recon.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    RemSummaryReconPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "SOS.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    SOSPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "remitance_det.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    RemitanceDetPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "variation.rpt")
                {
                    TransVariationPrint();

                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Sales_Figures.rpt" || BaseCls.GlbReportName == "Sales_Figures_OrderBy.rpt")
                {
                    SalesFiguresPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Sales_Target_Achievement_Del.rpt")
                {
                    SalesTargetAchievementPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                else if (BaseCls.GlbReportName == "SalesPromoterDetails.rpt")
                {
                    SalesPromoterDetail();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                else if (BaseCls.GlbReportName == "Elite_Commission_Details.rpt")
                {
                    EliteCommDetailPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Elite_Commission_Summary.rpt")
                {
                    EliteCommSummPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Not_Reg_Vehicles_Report.rpt")
                {
                    NotRegVehDetailPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Stamp_Duty_Report.rpt")
                {
                    StampDutyPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Forward_Sales_Report1.rpt")
                {
                    ForwardSalesPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Forward_Sales_Report2.rpt")
                {
                    ForwardSalesPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Forward_Sales_Report_cost.rpt")
                {
                    ForwardSalesPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "POS_Detail_Report.rpt")
                {
                    POSDetailPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "SVAT_Report.rpt")
                {
                    SVATPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "VehicleRegistrationSlip.rpt")
                {
                    VehicleRegistrationSlip();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "QuoationDet.rpt")
                {
                    QuotationDetails();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }



                else if (BaseCls.GlbReportName == "Warr_Rpl_CRNote.rpt")
                {
                    Warr_Rpl_CRNoteReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Sales_Promotion_Achievement_Report.rpt")
                {
                    Sales_Promotion_Achievement_Report();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Price_Details_Report.rpt")
                {
                    PriceDetails_Report();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "HPInsuranceRegister.rpt")
                {
                    InsuranceRegister();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;

                }
                else if (BaseCls.GlbReportName == "HPInsuranceRegisterNew.rpt" || BaseCls.GlbReportName == "HPInsurancePolicyReport.rpt")
                {
                    InsuranceRegistrationnew();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;

                }
                else if (BaseCls.GlbReportName == "HPInsuranceClaimRegister.rpt" || BaseCls.GlbReportName == "HPInsuranceDocumentRequired.rpt")
                {
                    InsuranceRegistrationClaims();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }

                else if (BaseCls.GlbReportName == "HPInsuranceSettlemetInscom.rpt")
                {
                    InsuranceRegistrationSettlements();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;

                }

                else if (BaseCls.GlbReportName == "VehicleInsuranceCollection.rpt")
                {
                    InsuranceCollection();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;

                }

                else if (BaseCls.GlbReportName == "VoucherPrints.rpt")
                {
                    InternalVoucher();

                }
                else if (BaseCls.GlbReportName == "ChequePrinting1.rpt")
                {
                    SearchVoucher();

                }
                else if (BaseCls.GlbReportName == "ChequePrinting.rpt")
                {
                    SearchVoucherPrint();

                }
                else if (BaseCls.GlbReportName == "EcdVouchar.rpt")
                {
                    ECDVoucher(BaseCls.GlbReportDataTable);

                }
                else if (BaseCls.GlbReportName == "Channel_det_Report.rpt")
                {
                    Load_Channel_dets();


                }
                else if (BaseCls.GlbReportName == "Sub_channel_det_Report.rpt")
                {
                    Load_Sub_Channel_dets();

                }
                else if (BaseCls.GlbReportName == "Area_det_Report.rpt")
                {
                    Load_Area_dets();

                }
                else if (BaseCls.GlbReportName == "AllocateItemReport.rpt")
                {
                    Load_Allocate_Items();

                }
                else if (BaseCls.GlbReportName == "ServiceLocDetailsReport.rpt")
                {
                    Load_Service_loc_dets();

                }
                else if (BaseCls.GlbReportName == "ServiceChnlParaReport.rpt")
                {
                    Load_Service_chnl_dets();

                }

                else if (BaseCls.GlbReportName == "Region_det_Report.rpt")
                {
                    Load_Region_dets();

                }
                else if (BaseCls.GlbReportName == "Zone_det_Report.rpt")
                {
                    Load_Zone_dets();
                }

                else if (BaseCls.GlbReportName == "InvoiceDutyFree.rpt")
                {
                    InvociePrintDutyFree();

                }

                else if (BaseCls.GlbReportName == "InvoiceDutyFree_CLC.rpt")
                {
                    InvociePrintDutyFreeCLC();

                }
                else if (BaseCls.GlbReportName == "InvoiceDutyFree_PP.rpt")
                {
                    InvociePrintDutyFreePP();

                }
                else if (BaseCls.GlbReportName == "InvoiceDutyFreeEdison.rpt")
                {
                    InvociePrintDutyFree_Edision();

                }

                else if (BaseCls.GlbReportName == "sInvoiceDutyFree.rpt")
                {
                    InvociePrintDutyFreeInv();

                }


                else if (BaseCls.GlbReportName == "Quotation_RepPrint.rpt")
                {
                    QuotationPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Execitivewise_Sales_with_Invoices.rpt")
                {
                    ExecwiseSalesInvPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "SparePartPrint.rpt")
                {
                    SparePartPrint();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                }
                else if (BaseCls.GlbReportName == "DeliveredSalesWithSerial.rpt")
                {
                    DeliveredSalesWithSerial();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }
                    //hasith button active in print
                else if (BaseCls.GlbReportName == "CreditNoteDetails.rpt")
                {
                    CreditNoteDetails();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }
                else if (BaseCls.GlbReportName == "DealerCommision.rpt")
                {
                    DealerCommission();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }
                else if (BaseCls.GlbReportName == "Sales_Target_Achievement_Del.rpt")
                {
                    SalesTargetAchivement();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }
                else if (BaseCls.GlbReportName == "CancelledDocuments.rpt")
                {
                    CancelledDocuments();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;

                }
                else if (BaseCls.GlbReportName == "ManualDocument.rpt")
                {
                    ManualDocuments();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }

                else if (BaseCls.GlbReportName == "ExtendedWarranty.rpt")
                {
                    ExtendedWarrantyPrint();

                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "ReturnChequeSettmentOutstanding.rpt")
                {
                    ReturnChequeSettlemtsOutstanding();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;

                }
                else if (BaseCls.GlbReportName == "ReturnChequeSettlemtPayments.rpt")
                {
                    ReturnChequeSettlements();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }
                else if (BaseCls.GlbReportName == "SummaryofWeekly.rpt")
                {
                    SummaryOfWeekly();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }

                else if (BaseCls.GlbReportName == "ExcessShort.rpt")
                {
                    ExcessShort();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;

                }
                else if (BaseCls.GlbReportName == "BOCCusResReceipt.rpt")
                {
                    BOCCusReceipt();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;

                }
                else if (BaseCls.GlbReportName == "DebtorSettlement_Outs_PC_Meeting.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DebtorSettlementPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "DebtorSettlement_Outs_PC_with_comm.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DebtorSettlementPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "AOACollection.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    AOACollection();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                else if (BaseCls.GlbReportName == "DealerInvoicePrints.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    InvociePrintDealerAuto();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                }
                else if (BaseCls.GlbReportName == "RegistrationReport.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    RegistrationReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                if (BaseCls.GlbReportName == "Job_Invoice_SGL.rpt")
                {
                    ServiceJobInvoiceSGL();
                    //btnPrint.Visible = false;
                    //crystalReportViewer1.ShowPrintButton = true;
                }
                if (GlbReportName == "Job_InvoiceNew.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    InvoicePrintServiceall();
                }
                ;
                if (GlbReportName == "giftvoucher.rpt") //Tharanga 2017/0
                {
                    crystalReportViewer1.ShowExportButton = true;
                    giftvouPrint();
                }
                //tHARANGA 
                if (GlbReportName == "InvoiceAuto.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    SystemAppLevelParam _sysApp = new SystemAppLevelParam();

                
                    crystalReportViewer1.ShowExportButton = false;

                    if (_sysApp.Sarp_cd != null)
                    {
                        crystalReportViewer1.ShowExportButton = true;
                    }
                    else
                    {
                        crystalReportViewer1.ShowExportButton = false;
                    }

                    
                    InvoicePrintAUTO();
                }
                if (BaseCls.GlbReportName == "Invoice_summary.rpt") //Tharanga 2017/06/24
                {
                    crystalReportViewer1.ShowExportButton = true;
                    invoceSummary();
                }
                else if (GlbReportName == "ReceptPrintNew.rpt")
                {
                    //BaseCls.GlbReportTp = "REC";
                    //CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                    //if (!(_repname == null || _repname == "")) { GlbReportName = _repname; BaseCls.GlbReportName = _repname; BaseCls.GlbReportPaperSize = _papersize; }
                    //BaseCls.GlbReportTp = "";
                    Receipt_print_new();
                }
                else if (BaseCls.GlbReportName == "Service_Invoice_ABE.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    Service_Invocie_ABE();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowExportButton = false;
                    crystalReportViewer1.ShowPrintButton = false;
                }
                if (GlbReportName == "InvPrints_Full_Abstract.rpt") //add by tharanga 2017/09/19
                {
                    crystalReportViewer1.ShowExportButton = false;
                    InvPrintTax_abstrct();
                }
                else if (GlbReportName == "Age_Debtor_Outstanding_Veh_reg.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    AgeAnalysisOfDebtorsOutstanding_vehical_reg();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "GP_Report.rpt")//add by tharanga 2017/12/11
                {
                    crystalReportViewer1.ShowExportButton = true;
                    GP_report();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Bill_Collection_detail.rpt")//add by Wimal 11/09/2018
                {
                    crystalReportViewer1.ShowExportButton = true;
                    Bill_Collectio_report();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                else if (BaseCls.GlbReportName == "Bill_Collection_summery.rpt")//add by Wimal 11/09/2018
                {
                    crystalReportViewer1.ShowExportButton = true;
                    Bill_Collection_summery_report();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                if (BaseCls.GlbReportName == "Sales_Order_Sum_New.rpt") //Tharindu2018-03-13
                {
                    crystalReportViewer1.ShowExportButton = true;
                    Get_sales_sum_report();
                }

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Sales Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        //kapila 2/2/2013
        private void ReceiptListingPrint()
        {
            
            obj.ReceiptListingPrint();
            this.Text = "Receipt Listing";
            if (BaseCls.GlbReportName == "Receipt_List_summary.rpt")
            {
                crystalReportViewer1.ReportSource = obj._Receipt_List_summary;
                crystalReportViewer1.RefreshReport();
            }
            else
            {
                crystalReportViewer1.ReportSource = obj._RecList;
                crystalReportViewer1.RefreshReport();
            }

          
            


        }

        //kapila 19/2/2013
        private void RemSummaryPrint()
        {
            obj.RemSummaryPrint();
            this.Text = "Remitance Summary";
            crystalReportViewer1.ReportSource = obj._RemSum;
            crystalReportViewer1.RefreshReport();

        }
        private void RemSummaryReconPrint()
        {
            obj.RemSummaryReconPrint();
            this.Text = "Remitance Summary Reconcilation";
            crystalReportViewer1.ReportSource = obj._RemSumRecon;
            crystalReportViewer1.RefreshReport();

        }


        //kapila 1/1/13
        private void DebtorSettlementPrint()
        {
            obj.DebtorSettlementPrint();
            if (BaseCls.GlbReportName == "DebtorSettlement.rpt")
            {
                crystalReportViewer1.ReportSource = obj._DebtSett;
            }
            if (BaseCls.GlbReportName == "DebtorSettlement_PC.rpt")
            {
                crystalReportViewer1.ReportSource = obj._DebtSettPC;
            }
            if (BaseCls.GlbReportName == "DebtorSettlement_Outs_PC.rpt")
            {
                crystalReportViewer1.ReportSource = obj._DebtSettOutPC;
            }
            if (BaseCls.GlbReportName == "DebtorSettlement_Outs.rpt")
            {
                crystalReportViewer1.ReportSource = obj._DebtSettOuts;
            }
            if (BaseCls.GlbReportName == "DebtorSettlement_Outs_PC_Meeting.rpt")
            {
                crystalReportViewer1.ReportSource = obj._DebtSettOutPCMeeting;
            }
            if (BaseCls.GlbReportName == "DebtorSettlement_Outs_PC_with_comm.rpt")
            {
                crystalReportViewer1.ReportSource = obj._DebtSettOutPCWithComm;
            }
            crystalReportViewer1.RefreshReport();

        }

        //kapila 1/1/13
        private void AgeAnalysisOfDebtorsOutstandingPrint()
        {
            obj.AgeAnalysisOfDebtorsOutstandingPrint();
            this.Text = "Age Analysis of Debtors Outstanding";
            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding.rpt")
                crystalReportViewer1.ReportSource = obj._AgeDebtOuts;

            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_PC.rpt")
                crystalReportViewer1.ReportSource = obj._AgeDebtOutsPC;

            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_new.rpt")
                crystalReportViewer1.ReportSource = obj._AgeDebtOuts_new;

            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_PC_new.rpt")
                crystalReportViewer1.ReportSource = obj._AgeDebtOutsPC_new;

            crystalReportViewer1.RefreshReport();

        }
        private void AgeAnalysisOfDebtorsOutstanding_vehical_reg()
        {
            obj.AgeAnalysisOfDebtorsOutstanding_vehical_reg();
            this.Text = "Age Analysis of Debtors Outstanding";
            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_Veh_reg.rpt")
                crystalReportViewer1.ReportSource = obj._Age_Debtor_Outstanding_Veh_reg;
        }
        private void AgeAnalysisOfDebtorsOutstandingDCNPrint()
        {
            obj.AgeAnalysisOfDebtorsOutstandingDCNPrint();
            this.Text = "Age Analysis of Debtors Outstanding";
            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_dcn.rpt")
                crystalReportViewer1.ReportSource = obj._AgeDebtOutsdcn;
           
            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_new_dcn.rpt")
                crystalReportViewer1.ReportSource = obj._AgeDebtOuts_newdcn;

            crystalReportViewer1.RefreshReport();

        }

        private void AgeAnalysisOfDebtorsOutstandingAdvPrint()
        {
            obj.AgeAnalysisOfDebtorsOutstandingPrint();
            this.Text = "Age Analysis of Debtors Outstanding";
            crystalReportViewer1.ReportSource = obj._AgeDebtOuts_adv;
            crystalReportViewer1.RefreshReport();

        }

        private void SalesSummaryPrint()
        //Sanjeewa 09-02-2013
        {


            if (BaseCls.GlbReportType == "CASHSALE")
            {
                obj.SalesSummaryReport();
                crystalReportViewer1.ReportSource = obj._cashSalesrpt;
                this.Text = "Cash Sales Summary";
            }
            else if (BaseCls.GlbReportType == "CREDITSALE")
            {
                obj.SalesSummaryReport();
                crystalReportViewer1.ReportSource = obj._cashSalesrpt;
                this.Text = "Credit Sales Summary";
            }
            else
            {
                obj.HireSalesSummaryReport();
                crystalReportViewer1.ReportSource = obj._HPSalesrpt;
                this.Text = "Hire Sales Summary";
            }
            crystalReportViewer1.RefreshReport();

        }

        private void SalesTargetAchievementPrint()
        //Sanjeewa 03-03-2013
        {
            obj.SalesTargetAchievementReport();
            crystalReportViewer1.ReportSource = obj._trgt_Ach;
            this.Text = "Sales Target Achievement";
            crystalReportViewer1.RefreshReport();
        }

        private void POAllocationPrint()
        //Sanjeewa 10-02-2016
        {
            obj.POAllocationDetailReport();
            crystalReportViewer1.ReportSource = obj._poalloc;
            this.Text = "PO Allocation";
            crystalReportViewer1.RefreshReport();
        }

        private void SalesPromoterDetail()
        //Wimal 22/06/2015
        {
            obj.GetSalesPromoterDetails();
            crystalReportViewer1.ReportSource = obj._salePromot_Details;
            this.Text = "Sales Promoter Details";
            crystalReportViewer1.RefreshReport();
        }

 

        private void SalesFiguresPrint()
        //Sanjeewa 25-05-2015
        {
            obj.SalesFiguresReport();
            if (BaseCls.GlbReportDocType == "Y")
            {
                crystalReportViewer1.ReportSource = obj._SalesFigOrdrpt;
            }
            else
            {
                crystalReportViewer1.ReportSource = obj._SalesFigrpt;
            }
            this.Text = "Sales Figures";
            crystalReportViewer1.RefreshReport();
        }


        private void A_SalesSummaryPrint()
        //Sanjeewa 10-03-2014
        {
            obj.A_SalesReport();
            crystalReportViewer1.ReportSource = obj._ASales;
            this.Text = "SALES REPORT";
            crystalReportViewer1.RefreshReport();
        }

        private void PrintSignOnSlip()
        {
            obj.PrintSignOnOffSlip();
            if (BaseCls.GlbReportName == "SignOn_Slip.rpt")
                crystalReportViewer1.ReportSource = obj._signOnSlip;
            else
                crystalReportViewer1.ReportSource = obj._signOffSlip;
            this.Text = "SALES REPORT";
            crystalReportViewer1.RefreshReport();
        }
        private void PrintSignOffDeno()
        {
            obj.PrintSignOnOffDeno();

                crystalReportViewer1.ReportSource = obj._signOffDeno;
            this.Text = "SALES REPORT";
            crystalReportViewer1.RefreshReport();
        }

        private void LoyalityDiscountPrint()
        //Sanjeewa 20-03-2014
        {
            obj.LoyalityDiscountReport();
            crystalReportViewer1.ReportSource = obj._LDisc;
            this.Text = "LOYALTY DISCOUNTS GIVEN TO CUSTOMERS";
            crystalReportViewer1.RefreshReport();
        }

        private void DFSalesStatementPrint()
        //Sanjeewa 30-10-2013
        {
            obj.DFSalesStatementReport();
            crystalReportViewer1.ReportSource = obj._DFSaleSt;
            this.Text = "DUTY FREE SALES STATEMENT";
            crystalReportViewer1.RefreshReport();
        }

        private void DFSalesStatementCurrPrint()
        //Sanjeewa 31-10-2013
        {
            obj.DFSalesStatementCurrencyReport();
            crystalReportViewer1.ReportSource = obj._DFCurrSale;
            this.Text = "DUTY FREE CURRENCY WISE SALES";
            crystalReportViewer1.RefreshReport();
        }

        private void DFSalesStatementCurrDtlPrint()
        //Sanjeewa 27-12-2016
        {
            obj.DFSalesStatementCurrencyDtlReport();
            crystalReportViewer1.ReportSource = obj._DFCurrSaleDtl;
            this.Text = "DUTY FREE CURRENCY WISE SALES";
            crystalReportViewer1.RefreshReport();
        }

        private void DFSalesStatementCurrTrPrint()
        //Sanjeewa 26-12-2013
        {
            obj.DFSalesStatementCurrencyTrReport();
            crystalReportViewer1.ReportSource = obj._DFCurrTr;
            this.Text = "DUTY FREE FOREIGN CURRENCY TRANSACTIONS";
            crystalReportViewer1.RefreshReport();
        }

        private void DFCategorywiseSalesPrint()
        //Sanjeewa 31-10-2013
        {
            obj.DFCategorywiseSalesReport();
            crystalReportViewer1.ReportSource = obj._DFCatSale;
            this.Text = "DUTY FREE CATEGORY WISE SALES";
            crystalReportViewer1.RefreshReport();
        }

        private void DFConsolidatedSalesStatementPrint()
        //Sanjeewa 30-10-2013
        {
            obj.DFConsolidatedSalesStatementReport();
            crystalReportViewer1.ReportSource = obj._DFSaleConsolidate;
            this.Text = "DUTY FREE CONSOLIDATED STATEMENT OF SALES";
            crystalReportViewer1.RefreshReport();
        }
        private void DFSalesModelwise()
        {
            obj.DFSalesModelwise();
            crystalReportViewer1.ReportSource = obj._DFSaleModel;
            this.Text = "MODEL WISE DUTY FREE SALES REPORT";
            crystalReportViewer1.RefreshReport();
        }
        private void GPSummary()
        {
            obj.GPSummaryReport();
            crystalReportViewer1.ReportSource = obj._gpSumm;
            this.Text = "GP SUMMARY REPORT";
            crystalReportViewer1.RefreshReport();
        }
        private void DFSaleswithQtyPrint()
        //Sanjeewa 01-11-2013
        {
            obj.DFSaleswithQtyReport();
            crystalReportViewer1.ReportSource = obj._DFSaleQty;
            this.Text = "DUTY FREE SALES QUANTITY AND VALUE REPORT";
            crystalReportViewer1.RefreshReport();
        }

        private void DFSalesQtyPrint()
        //Sanjeewa 04-11-2013
        {
            obj.DFSalesQtyReport();
            crystalReportViewer1.ReportSource = obj._DFSQty;
            this.Text = "DUTY FREE SALES QUANTITY REPORT";
            crystalReportViewer1.RefreshReport();
        }
        private void DFMonthSalesReport(int repid)
        //Nadeeka 06-11-2013
        {
            obj.DFMonthSalesReport(repid);
            if (repid == 1)
            {
                crystalReportViewer1.ReportSource = obj._DFSMonSls;
                this.Text = "DUTY FREE MONTHLY SALES REPORT";
            }
            if (repid == 2)
            {
                crystalReportViewer1.ReportSource = obj._DFSWeekSls;
                this.Text = "DUTY FREE WEEKLY SALES REPORT";
            }
            if (repid == 3)
            {
                crystalReportViewer1.ReportSource = obj._DFSCatSls;
                this.Text = "DUTY FREE  SALES REPORT";
            }
            crystalReportViewer1.RefreshReport();
        }
        private void EliteCommDetailPrint()
        //Sanjeewa 19-06-2013
        {
            obj.EliteCommissionReport();
            crystalReportViewer1.ReportSource = obj._ECommPrint;
            this.Text = "ELITE COMMISSION DETAILS";
            crystalReportViewer1.RefreshReport();
        }

        private void EliteCommSummPrint()
        //Sanjeewa 08-07-2013
        {
            obj.EliteCommissionReport();
            crystalReportViewer1.ReportSource = obj._ECommSumm;
            this.Text = "ELITE COMMISSION SUMMARY";
            crystalReportViewer1.RefreshReport();
        }

        private void NotRegVehDetailPrint()
        //Sanjeewa 20-06-2013
        {
            obj.NotRegVehiclesReport();
            crystalReportViewer1.ReportSource = obj._NotRegVeh;
            this.Text = "PENDING REGISTRATION RECEIPTS";
            crystalReportViewer1.RefreshReport();
        }
        private void ExecwiseSalesInvPrint()
        //Sanjeewa 31-05-2013
        {
            obj.ExecutivewiseSalesInvoiceReport();
            crystalReportViewer1.ReportSource = obj._ExecSaleInvoice;
            this.Text = "EXECUTIVE WISE SALES INVOICE REPORT";
            crystalReportViewer1.RefreshReport();
        }

        private void QuotationPrint()
        //Sanjeewa 12-06-2013
        {
            obj.QuotationPrintReport();
            crystalReportViewer1.ReportSource = obj._QuoPrint;
            this.Text = "QUOTATION";
            crystalReportViewer1.RefreshReport();
        }

        private void StampDutyPrint()
        //Sanjeewa 07-03-2013
        {
            obj.StampDutyReport();
            crystalReportViewer1.ReportSource = obj._Stamp_Duty;
            this.Text = "Stamp Duty Report";
            crystalReportViewer1.RefreshReport();
        }

        private void ForwardSalesPrint()
        //Sanjeewa 07-03-2013
        {
            obj.ForwardSalesReport();
            if (BaseCls.GlbReportType == "N")
            {
                if (BaseCls.GlbReportWithCost == 1)
                    crystalReportViewer1.ReportSource = obj._ForwardSalesrptcost;
                else
                    crystalReportViewer1.ReportSource = obj._ForwardSalesrpt1;
            }
            else
            {
                crystalReportViewer1.ReportSource = obj._ForwardSalesrpt2;
            }
            this.Text = "Forward Sales Report";
            crystalReportViewer1.RefreshReport();
        }

        private void POSDetailPrint()
        //Sanjeewa 01-04-2013
        {
            obj.POSDetailReport();
            crystalReportViewer1.ReportSource = obj._POSDtlrpt;
            this.Text = "POS Detail Report";
            crystalReportViewer1.RefreshReport();
        }

        private void SVATPrint()
        //Sanjeewa 07-03-2013
        {
            obj.SVATReport();
            crystalReportViewer1.ReportSource = obj._SVAT;
            this.Text = "SVAT Report";
            crystalReportViewer1.RefreshReport();
        }

        private void VehicleRegistrationSlip()
        //Nadeeka
        {
            obj.VehicleRegistrationSlip();
            crystalReportViewer1.ReportSource = obj._vheRegSlip;
            this.Text = "Vehicle Registration Print";
            crystalReportViewer1.RefreshReport();
        }


        private void QuotationDetails()
        //Nadeeka
        {
            obj.QuotationDetails();
            crystalReportViewer1.ReportSource = obj._quoDet;
            this.Text = "Quotation Details";
            crystalReportViewer1.RefreshReport();
        }


        private void Warr_Rpl_CRNoteReport()
        //Sanjeewa 24-06-2013
        {
            obj.Warr_Rpl_CRNoteReport();
            crystalReportViewer1.ReportSource = obj._Warr_Rpl_CRNote;
            this.Text = "Warranty Replacement Credit Notes Report";
            crystalReportViewer1.RefreshReport();
        }

        private void Sales_Promotion_Achievement_Report()
        //Sanjeewa 01-07-2013
        {
            obj.SalesPromoAchievementReport();
            crystalReportViewer1.ReportSource = obj._SalePromoArchieve;
            this.Text = "Sales Promotion Achievement Report";
            crystalReportViewer1.RefreshReport();
        }

        private void PriceDetails_Report()
        //Sanjeewa 23-07-2013
        {
            obj.PriceDetailsReport();
            crystalReportViewer1.ReportSource = obj._pricedtl;
            this.Text = "PRICE DETAILS REPORT";
            crystalReportViewer1.RefreshReport();
        }

        private void SparePartPrint()
        {
            obj.SparePartPrint();
            crystalReportViewer1.ReportSource = obj._sprPrint;
            this.Text = "Spare Part Print";
            crystalReportViewer1.RefreshReport();
        }
        private void InvociePrintnew()
        {// Nadeeka 17-07-13
            string invNo = default(string);
            invNo = GlbReportDoc;
            DataTable mst_tax_master = new DataTable();

            //   DataTable salesDetails = new DataTable();
            //   salesDetails.Clear();
            //salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);

            mst_tax_master = CHNLSVC.Sales.GetinvTax(invNo);

            DataTable sat_hdr1 = CHNLSVC.Sales.GetSalesHdr(invNo);
            DataTable sat_itm = CHNLSVC.Sales.GetSalesDet(invNo);
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_emp = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable sar_sub_tp = new DataTable();
            mst_item = CHNLSVC.Sales.GetInvItemCode(invNo);
            foreach (DataRow row in sat_hdr1.Rows)
            {
                mst_profit_center = CHNLSVC.Sales.GetProfitCenterTable(row["SAH_COM"].ToString(), row["SAH_PC"].ToString());
                mst_com = CHNLSVC.General.GetCompanyByCode(row["SAH_COM"].ToString());
                mst_busentity = CHNLSVC.Sales.GetBusinessCompanyDetailTable(row["SAH_COM"].ToString(), row["SAH_CUS_CD"].ToString(), "", "", "C");
                sar_sub_tp = CHNLSVC.Sales.GetinvSubType(row["sah_inv_tp"].ToString(), row["sah_inv_sub_tp"].ToString());
                sec_user = CHNLSVC.Sales.GetinvUser(row["sah_cre_by"].ToString());
                mst_emp = CHNLSVC.Sales.GetinvEmp(row["SAH_COM"].ToString(), row["sah_sales_ex_cd"].ToString());
            }

            DataTable int_batch = CHNLSVC.Sales.GetinvBatch(invNo);
            DataTable int_ser = new DataTable();
            foreach (DataRow row in int_batch.Rows)
            {
                int_ser = CHNLSVC.Sales.GetinvSer(Convert.ToDouble(row["ITB_SEQ_NO"].ToString()));
            }
            DataTable MST_ITM = new DataTable();



            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(invNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            invReport2.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


            DataTable MST_LOC = new DataTable();
            DataTable sat_receipt = new DataTable();

            DataTable hpt_acc = CHNLSVC.Sales.GetAccountDetails(invNo);
            DataTable sat_receiptitm = CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);
            sat_receipt = CHNLSVC.Sales.GetInvoiceReceiptHdr(invNo);

            DataTable hpt_shed = CHNLSVC.Sales.GetAccountSchedule(invNo);
            DataTable ref_rep_infor = CHNLSVC.Sales.GetReportInfor("InvoiceHalfPrints.rpt");

            mst_item = mst_item.DefaultView.ToTable(true);
            invReport2.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            invReport2.Database.Tables["mst_com"].SetDataSource(mst_com);
            invReport2.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            invReport2.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            invReport2.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            invReport2.Database.Tables["mst_item"].SetDataSource(mst_item);
            invReport2.Database.Tables["sec_user"].SetDataSource(sec_user);
            invReport2.Database.Tables["sar_sub_tp"].SetDataSource(sar_sub_tp);
            invReport2.Database.Tables["mst_emp"].SetDataSource(mst_emp);

            foreach (object repOp in invReport2.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptWarranty")
                    {
                        ReportDocument subRepDoc = invReport2.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);

                    }


                    if (_cs.SubreportName == "rptCheque")
                    {
                        ReportDocument subRepDoc = invReport2.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "rptAccount")
                    {
                        ReportDocument subRepDoc = invReport2.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);
                        subRepDoc.Database.Tables["hpt_shed"].SetDataSource(hpt_shed);
                    }



                    if (_cs.SubreportName == "tax")
                    {
                        ReportDocument subRepDoc = invReport2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);


                    }
                    if (_cs.SubreportName == "rptComm")
                    {
                        ReportDocument subRepDoc = invReport2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                    }
                    if (_cs.SubreportName == "rptWarr")
                    {
                        sat_itm = sat_itm.DefaultView.ToTable(true);
                        ReportDocument subRepDoc = invReport2.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);
                        subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                        subRepDoc.Database.Tables["MST_ITM"].SetDataSource(mst_item);


                    }

                }
            }




            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = invReport2;

            crystalReportViewer1.RefreshReport();



        }

        private void InvociePrint_INV()
        {// Nadeeka 26-12-12
            string invNo = default(string);
            invNo = GlbReportDoc;
            DataTable mst_tax_master = new DataTable();
            DataRow drr;
            DataTable salesDetails = new DataTable();
            DataTable sat_vou_det = new DataTable();
            DataTable param = new DataTable();

            param.Clear();
            param.Columns.Add("isCnote", typeof(Int16));

            Int16 isCredit = 0;



            salesDetails.Clear();


            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);

            sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(invNo);

            mst_tax_master = CHNLSVC.Sales.GetinvTax(invNo);
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

            _numPages = CHNLSVC.General.GetReprintDocCount(invNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            SinvReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


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
            sat_hdr1.Columns.Add("sah_del_loc", typeof(string));
            sat_hdr1.Columns.Add("sah_currency", typeof(string));

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



                int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));
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
                    dr["sah_currency"] = row["sah_currency"].ToString();
                    if (!string.IsNullOrEmpty(row["sah_del_loc"].ToString()))
                    {
                        dr["sah_del_loc"] = row["sah_del_loc"].ToString();
                        MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["sah_del_loc"].ToString());
                    }
                    else
                    {
                        dr["sah_del_loc"] = string.Empty;
                    }
                    sar_sub_tp = CHNLSVC.Sales.GetSalesSubTypeTable(row["SAH_INV_TP"].ToString(), row["SAH_INV_SUB_TP"].ToString());

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
                    tblComDate = CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), invNo);

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

            DataTable deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
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
                MST_ITM1 = CHNLSVC.Sales.GetItemCode(row["ITH_COM"].ToString(), row["ITS_ITM_CD"].ToString());
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

            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
            DataTable hpt_acc = new DataTable();



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

            DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);


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
                    sat_receiptitmCQ.Rows.Add(dr);


                    if (row["SARD_PAY_TP"].ToString() == "CRNOTE")
                    {
                        isCredit = 1;

                    }
                };



            }

            DataTable hpt_shed = CHNLSVC.Sales.GetAccountSchedule(invNo);


            DataTable ref_rep_infor = CHNLSVC.Sales.GetReportInfor("InvoiceHalfPrints.rpt");
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


            SinvReport1.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            SinvReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            SinvReport1.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            SinvReport1.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            SinvReport1.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            SinvReport1.Database.Tables["mst_item"].SetDataSource(mst_item);
            SinvReport1.Database.Tables["sec_user"].SetDataSource(sec_user);
            SinvReport1.Database.Tables["sar_sub_tp"].SetDataSource(sar_sub_tp);
            SinvReport1.Database.Tables["param"].SetDataSource(param);






            foreach (object repOp in SinvReport1.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptWarranty")
                    {
                        ReportDocument subRepDoc = SinvReport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);

                    }


                    if (_cs.SubreportName == "rptCheque")
                    {
                        ReportDocument subRepDoc = SinvReport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receiptCQ);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitmCQ);

                    }
                    if (_cs.SubreportName == "rptAccount")
                    {
                        ReportDocument subRepDoc = SinvReport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);
                        subRepDoc.Database.Tables["hpt_shed"].SetDataSource(hpt_shed);
                    }



                    if (_cs.SubreportName == "tax")
                    {
                        ReportDocument subRepDoc = SinvReport1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);


                    }
                    if (_cs.SubreportName == "rptComm")
                    {
                        ReportDocument subRepDoc = SinvReport1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                    }
                    if (_cs.SubreportName == "rptWarr")
                    {
                        mst_item1 = mst_item1.DefaultView.ToTable(true);
                        ReportDocument subRepDoc = SinvReport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);
                        subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                        subRepDoc.Database.Tables["MST_ITM"].SetDataSource(mst_item1);
                        subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);



                    }
                    if (_cs.SubreportName == "giftVou")
                    {
                        ReportDocument subRepDoc = SinvReport1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                    }
                    if (_cs.SubreportName == "loc")
                    {
                        ReportDocument subRepDoc = SinvReport1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                    }


                    //if (tblComDate.Rows.Count > 0)
                    //{
                    //    if (_cs.SubreportName == "warrComDate")
                    //    {
                    //        ReportDocument subRepDoc = SinvReport1.Subreports[_cs.SubreportName];
                    //        subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                    //    }
                    //}

                }
            }




            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = SinvReport1;

            crystalReportViewer1.RefreshReport();




        }

        private void InvociePrintServiceABE()
        {// Sanjeewa 2015-03-17
            string invNo = default(string);
            string accNo = default(string);
            Boolean ishp = default(Boolean);
            ishp = false;
            invNo = GlbReportDoc;
            DataTable mst_tax_master = new DataTable();
            DataRow drr;
            DataTable salesDetails = new DataTable();
            //DataTable sat_vou_det = new DataTable();
            DataTable param = new DataTable();

            param.Clear();
            param.Columns.Add("isCnote", typeof(Int16));

            Int16 isCredit = 0;

            salesDetails.Clear();

            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            //sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(invNo);
            mst_tax_master = CHNLSVC.Sales.GetinvTax(invNo);

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

            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(invNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr3);
            _JobInvoiceABE.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();

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
                        MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["sah_del_loc"].ToString());
                    }
                    else
                    {
                        dr["sah_del_loc"] = string.Empty;
                    }
                    sar_sub_tp = CHNLSVC.Sales.GetSalesSubTypeTable(row["SAH_INV_TP"].ToString(), row["SAH_INV_SUB_TP"].ToString());

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
                dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                dr["MI_CD"] = row["MI_CD"].ToString();

                mst_item.Rows.Add(dr);

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

            DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);

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

            DataTable ref_rep_infor = CHNLSVC.Sales.GetReportInfor("InvoiceHalfPrints.rpt");
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

            _JobInvoiceABE.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _JobInvoiceABE.Database.Tables["mst_com"].SetDataSource(mst_com);
            _JobInvoiceABE.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            _JobInvoiceABE.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            _JobInvoiceABE.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            _JobInvoiceABE.Database.Tables["mst_item"].SetDataSource(mst_item);
            _JobInvoiceABE.Database.Tables["sec_user"].SetDataSource(sec_user);
            _JobInvoiceABE.Database.Tables["sar_sub_tp"].SetDataSource(sar_sub_tp);
            _JobInvoiceABE.Database.Tables["param"].SetDataSource(param);
            //_JobInvoiceABE.Database.Tables["Promo"].SetDataSource(Promo);

            foreach (object repOp in _JobInvoiceABE.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    //if (_cs.SubreportName == "rptWarranty")
                    //{
                    //    ReportDocument subRepDoc = _JobInvoiceABE.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                    //}

                    if (_cs.SubreportName == "rptCheque")
                    {
                        ReportDocument subRepDoc = _JobInvoiceABE.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receiptCQ);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitmCQ);
                    }
                    //if (_cs.SubreportName == "rptAccount")
                    //{
                    //    ReportDocument subRepDoc = _JobInvoiceABE.Subreports[_cs.SubreportName];

                    //    subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);
                    //    subRepDoc.Database.Tables["hpt_shed"].SetDataSource(hpt_shed);
                    //}

                    if (_cs.SubreportName == "tax")
                    {
                        ReportDocument subRepDoc = _JobInvoiceABE.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);
                    }
                    if (_cs.SubreportName == "rptComm")
                    {
                        ReportDocument subRepDoc = _JobInvoiceABE.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                    }
                    //if (_cs.SubreportName == "rptWarr")
                    //{
                    //    mst_item1 = mst_item1.DefaultView.ToTable(true);
                    //    ReportDocument subRepDoc = _JobInvoiceABE.Subreports[_cs.SubreportName];

                    //    subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);
                    //    subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                    //    subRepDoc.Database.Tables["MST_ITM"].SetDataSource(mst_item1);
                    //    subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                    //}
                    //if (_cs.SubreportName == "giftVou")
                    //{
                    //    ReportDocument subRepDoc = _JobInvoiceABE.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                    //}
                    if (_cs.SubreportName == "loc")
                    {
                        ReportDocument subRepDoc = _JobInvoiceABE.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                    }

                    //if (tblComDate.Rows.Count >0) 
                    //{
                    //  if (_cs.SubreportName == "warrComDate")
                    //  {
                    //      ReportDocument subRepDoc = _JobInvoiceABE.Subreports[_cs.SubreportName];
                    //      subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                    //  }
                    //}
                }
            }

            this.Text = "Invoice Print";
            crystalReportViewer1.ReportSource = _JobInvoiceABE;
            crystalReportViewer1.RefreshReport();
        }

        private void InvociePrintService()
        {// Sanjeewa 2015-03-17
            string invNo = default(string);
            string accNo = default(string);
            Boolean ishp = default(Boolean);
            ishp = false;
            invNo = GlbReportDoc;
            DataTable mst_tax_master = new DataTable();
            DataRow drr;
            DataTable salesDetails = new DataTable();
            //DataTable sat_vou_det = new DataTable();
            DataTable param = new DataTable();

            param.Clear();
            param.Columns.Add("isCnote", typeof(Int16));

            Int16 isCredit = 0;

            salesDetails.Clear();

            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            //sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(invNo);
            mst_tax_master = CHNLSVC.Sales.GetinvTax(invNo);

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

            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(invNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr3);
            _JobInvoice.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();

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
                        MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["sah_del_loc"].ToString());
                    }
                    else
                    {
                        dr["sah_del_loc"] = string.Empty;
                    }
                    sar_sub_tp = CHNLSVC.Sales.GetSalesSubTypeTable(row["SAH_INV_TP"].ToString(), row["SAH_INV_SUB_TP"].ToString());

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
                dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                dr["MI_CD"] = row["MI_CD"].ToString();

                mst_item.Rows.Add(dr);

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

            DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);

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

            DataTable ref_rep_infor = CHNLSVC.Sales.GetReportInfor("InvoiceHalfPrints.rpt");
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

            _JobInvoice.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _JobInvoice.Database.Tables["mst_com"].SetDataSource(mst_com);
            _JobInvoice.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            _JobInvoice.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            _JobInvoice.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            _JobInvoice.Database.Tables["mst_item"].SetDataSource(mst_item);
            _JobInvoice.Database.Tables["sec_user"].SetDataSource(sec_user);
            _JobInvoice.Database.Tables["sar_sub_tp"].SetDataSource(sar_sub_tp);
            _JobInvoice.Database.Tables["param"].SetDataSource(param);
            //_JobInvoice.Database.Tables["Promo"].SetDataSource(Promo);

            foreach (object repOp in _JobInvoice.ReportDefinition.ReportObjects)
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
                        ReportDocument subRepDoc = _JobInvoice.Subreports[_cs.SubreportName];
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
                        ReportDocument subRepDoc = _JobInvoice.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);
                    }
                    if (_cs.SubreportName == "rptComm")
                    {
                        ReportDocument subRepDoc = _JobInvoice.Subreports[_cs.SubreportName];
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
                        ReportDocument subRepDoc = _JobInvoice.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                    }

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

            this.Text = "Invoice Print";
            crystalReportViewer1.ReportSource = _JobInvoice;
            crystalReportViewer1.RefreshReport();
        }


        private void InvociePrintServicePhone()
        {// Sanjeewa 2015-06-12
            string invNo = default(string);
            string accNo = default(string);
            Boolean ishp = default(Boolean);
            ishp = false;
            invNo = GlbReportDoc;
            DataTable mst_tax_master = new DataTable();
            DataRow drr;
            DataTable salesDetails = new DataTable();
            //DataTable sat_vou_det = new DataTable();
            DataTable param = new DataTable();

            param.Clear();
            param.Columns.Add("isCnote", typeof(Int16));

            Int16 isCredit = 0;

            salesDetails.Clear();

            //salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            salesDetails = CHNLSVC.Inventory.GetSalesDetailsMobSer(invNo);
            //sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(invNo);
            mst_tax_master = CHNLSVC.Sales.GetinvTax(invNo);

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

            _numPages = CHNLSVC.General.GetReprintDocCount(invNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr3);
            _JobInvoicePh.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();

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

                int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));
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
                        MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["sah_del_loc"].ToString());
                    }
                    else
                    {
                        dr["sah_del_loc"] = string.Empty;
                    }
                    sar_sub_tp = CHNLSVC.Sales.GetSalesSubTypeTable(row["SAH_INV_TP"].ToString(), row["SAH_INV_SUB_TP"].ToString());

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

            DataTable deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
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
            }

            DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);

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

            DataTable ref_rep_infor = CHNLSVC.Sales.GetReportInfor("InvoiceHalfPrints.rpt");
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


            _JobInvoicePh.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _JobInvoicePh.Database.Tables["mst_com"].SetDataSource(mst_com);
            _JobInvoicePh.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            _JobInvoicePh.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            _JobInvoicePh.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            _JobInvoicePh.Database.Tables["mst_item"].SetDataSource(mst_item);
            _JobInvoicePh.Database.Tables["sec_user"].SetDataSource(sec_user);
            _JobInvoicePh.Database.Tables["sar_sub_tp"].SetDataSource(sar_sub_tp);
            _JobInvoicePh.Database.Tables["param"].SetDataSource(param);

            DataTable ServiceJobDef = new DataTable();
            DataTable ServiceJobSer = new DataTable();
            DataTable ServiceJobSerSub = new DataTable();

            if (sat_itm.Rows.Count > 0)
            {
                foreach (DataRow drow in sat_itm.Rows)
                {
                    DataTable ServiceJobDef1 = new DataTable();
                    ServiceJobDef1 = CHNLSVC.CustService.sp_get_job_defects(drow["SAD_JOB_NO"].ToString());
                    ServiceJobDef.Merge(ServiceJobDef1);

                    //kapila 17/6/2015
                    DataTable ServiceJobSerials = new DataTable();
                    ServiceJobSerials = CHNLSVC.CustService.getServicejobDet(drow["SAD_JOB_NO"].ToString(), Convert.ToInt32(drow["SAD_JOB_LINE"]));
                    ServiceJobSer.Merge(ServiceJobSerials);

                    DataTable ServiceJobSerialsSub = new DataTable();
                    ServiceJobSerialsSub = CHNLSVC.CustService.GetServiceJobDetailSubItemsData(drow["SAD_JOB_NO"].ToString(), Convert.ToInt32(drow["SAD_JOB_LINE"]));
                    ServiceJobSerSub.Merge(ServiceJobSerialsSub);


                }

                ServiceJobDef = ServiceJobDef.DefaultView.ToTable(true);
                ServiceJobSer = ServiceJobSer.DefaultView.ToTable(true);
                ServiceJobSerSub = ServiceJobSerSub.DefaultView.ToTable(true);

            }

            //_JobInvoice.Database.Tables["Promo"].SetDataSource(Promo);

            foreach (object repOp in _JobInvoicePh.ReportDefinition.ReportObjects)
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
                        ReportDocument subRepDoc = _JobInvoicePh.Subreports[_cs.SubreportName];
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
                        ReportDocument subRepDoc = _JobInvoicePh.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);
                    }
                    if (_cs.SubreportName == "rptComm")
                    {
                        ReportDocument subRepDoc = _JobInvoicePh.Subreports[_cs.SubreportName];
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
                        ReportDocument subRepDoc = _JobInvoicePh.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                    }
                    if (_cs.SubreportName == "serials")
                    {
                        ReportDocument subRepDoc = _JobInvoicePh.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["INT_BATCH"].SetDataSource(int_batch1);
                        subRepDoc.Database.Tables["INT_SER"].SetDataSource(int_ser);
                    }
                    if (_cs.SubreportName == "Job_Defects")
                    {
                        ReportDocument subRepDoc = _JobInvoicePh.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                    }
                    if (_cs.SubreportName == "Job_Serials")
                    {
                        ReportDocument subRepDoc = _JobInvoicePh.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobSer);
                    }
                    if (_cs.SubreportName == "Job_Serials_Sub")
                    {
                        ReportDocument subRepDoc = _JobInvoicePh.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SCV_JOB_DET_SUB"].SetDataSource(ServiceJobSerSub);
                    }

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

            this.Text = "Invoice Print";
            crystalReportViewer1.ReportSource = _JobInvoicePh;
            crystalReportViewer1.RefreshReport();
        }

        private void InvociePrint()
        {// Nadeeka 26-12-12
            string invNo = default(string);
            string accNo = default(string);
            string cust_code = default(string);
            Boolean ishp = default(Boolean);
            ishp = false;
            invNo = GlbReportDoc;
            DataTable mst_tax_master = new DataTable();
            DataRow drr;
            DataTable salesDetails = new DataTable();
            DataTable sat_vou_det = new DataTable();
            DataTable param = new DataTable();
            DataTable mst_customer = new DataTable();

            param.Clear();
            param.Columns.Add("isCnote", typeof(Int16));

            Int16 isCredit = 0;



            salesDetails.Clear();


            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);

            sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(invNo);

            mst_tax_master = CHNLSVC.Sales.GetinvTax(invNo);
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

            _numPages = CHNLSVC.General.GetReprintDocCount(invNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr3);
            if (GlbReportName == "InvoiceHalfPrints.rpt")
                invReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            else
                invReportPOS.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();


            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
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



                int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));
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
                        MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["sah_del_loc"].ToString());
                    }
                    else
                    {
                        dr["sah_del_loc"] = string.Empty;
                    }
                    cust_code = row["SAH_STRUCTURE_SEQ"].ToString(); //Sanjeewa 2015-08-05 QUOTATION NO
                    mst_customer = new DataTable();
                    mst_customer = CHNLSVC.Sales.Get_CustomerDetails(cust_code);
                    foreach (DataRow row2 in mst_customer.Rows)
                    {
                        dr["SAH_QT_CUST"] = row2["mbg_name"].ToString();
                    }
                    sar_sub_tp = CHNLSVC.Sales.GetSalesSubTypeTable(row["SAH_INV_TP"].ToString(), row["SAH_INV_SUB_TP"].ToString());

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
                        tblComDate = CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), accNo);
                    }
                    else
                    {
                        tblComDate = CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), invNo);
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

            DataTable deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
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
                MST_ITM1 = CHNLSVC.Sales.GetItemCode(row["ITH_COM"].ToString(), row["ITS_ITM_CD"].ToString());
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







            DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);


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

            DataTable hpt_shed = CHNLSVC.Sales.GetAccountSchedule(invNo);
            DataTable Promo = CHNLSVC.Sales.GetPromotionByInvoice(invNo);
            DataTable ref_rep_infor = new DataTable();

            if (GlbReportName == "InvoiceHalfPrints.rpt")
                ref_rep_infor = CHNLSVC.Sales.GetReportInfor("InvoiceHalfPrints.rpt");
            else
                ref_rep_infor = CHNLSVC.Sales.GetReportInfor("InvoicePOSPrint.rpt");        //kapila 29/6/2015

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

            if (GlbReportName == "InvoiceHalfPrints.rpt")
            {
                invReport1.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                invReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
                invReport1.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
                invReport1.Database.Tables["sat_itm"].SetDataSource(sat_itm);
                invReport1.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
                invReport1.Database.Tables["mst_item"].SetDataSource(mst_item);
                invReport1.Database.Tables["sec_user"].SetDataSource(sec_user);
                invReport1.Database.Tables["sar_sub_tp"].SetDataSource(sar_sub_tp);
                invReport1.Database.Tables["param"].SetDataSource(param);
                invReport1.Database.Tables["Promo"].SetDataSource(Promo);
            }
            else
            {
                invReportPOS.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                invReportPOS.Database.Tables["mst_com"].SetDataSource(mst_com);
                invReportPOS.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
                invReportPOS.Database.Tables["sat_itm"].SetDataSource(sat_itm);
                invReportPOS.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
                invReportPOS.Database.Tables["mst_item"].SetDataSource(mst_item);
                invReportPOS.Database.Tables["sec_user"].SetDataSource(sec_user);
                invReportPOS.Database.Tables["sar_sub_tp"].SetDataSource(sar_sub_tp);
                invReportPOS.Database.Tables["param"].SetDataSource(param);
                invReportPOS.Database.Tables["Promo"].SetDataSource(Promo);
            }



            if (GlbReportName == "InvoiceHalfPrints.rpt")
            {
                foreach (object repOp in invReport1.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptWarranty")
                        {
                            ReportDocument subRepDoc = invReport1.Subreports[_cs.SubreportName];

                            subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);

                        }


                        if (_cs.SubreportName == "rptCheque")
                        {
                            ReportDocument subRepDoc = invReport1.Subreports[_cs.SubreportName];

                            subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receiptCQ);
                            subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitmCQ);

                        }
                        if (_cs.SubreportName == "rptAccount")
                        {
                            ReportDocument subRepDoc = invReport1.Subreports[_cs.SubreportName];

                            subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);
                            subRepDoc.Database.Tables["hpt_shed"].SetDataSource(hpt_shed);
                        }



                        if (_cs.SubreportName == "tax")
                        {
                            ReportDocument subRepDoc = invReport1.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);


                        }
                        if (_cs.SubreportName == "rptComm")
                        {
                            ReportDocument subRepDoc = invReport1.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                        }
                        if (_cs.SubreportName == "rptWarr")
                        {
                            mst_item1 = mst_item1.DefaultView.ToTable(true);
                            ReportDocument subRepDoc = invReport1.Subreports[_cs.SubreportName];

                            subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);
                            subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                            subRepDoc.Database.Tables["MST_ITM"].SetDataSource(mst_item1);
                            subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);



                        }
                        if (_cs.SubreportName == "giftVou")
                        {
                            ReportDocument subRepDoc = invReport1.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                        }
                        if (_cs.SubreportName == "loc")
                        {
                            ReportDocument subRepDoc = invReport1.Subreports[_cs.SubreportName];
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
                foreach (object repOp in invReportPOS.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptWarranty")
                        {
                            ReportDocument subRepDoc = invReportPOS.Subreports[_cs.SubreportName];

                            subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);

                        }


                        if (_cs.SubreportName == "rptCheque")
                        {
                            ReportDocument subRepDoc = invReportPOS.Subreports[_cs.SubreportName];

                            subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receiptCQ);
                            subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitmCQ);

                        }
                        if (_cs.SubreportName == "rptAccount")
                        {
                            ReportDocument subRepDoc = invReportPOS.Subreports[_cs.SubreportName];

                            subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);
                            subRepDoc.Database.Tables["hpt_shed"].SetDataSource(hpt_shed);
                        }



                        if (_cs.SubreportName == "tax")
                        {
                            ReportDocument subRepDoc = invReportPOS.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);


                        }
                        if (_cs.SubreportName == "rptComm")
                        {
                            ReportDocument subRepDoc = invReportPOS.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                        }
                        if (_cs.SubreportName == "rptWarr")
                        {
                            mst_item1 = mst_item1.DefaultView.ToTable(true);
                            ReportDocument subRepDoc = invReportPOS.Subreports[_cs.SubreportName];

                            subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);
                            subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                            subRepDoc.Database.Tables["MST_ITM"].SetDataSource(mst_item1);
                            subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);



                        }
                        if (_cs.SubreportName == "giftVou")
                        {
                            ReportDocument subRepDoc = invReportPOS.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                        }
                        if (_cs.SubreportName == "loc")
                        {
                            ReportDocument subRepDoc = invReportPOS.Subreports[_cs.SubreportName];
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




            this.Text = "Invoice Print";

            if (GlbReportName == "InvoiceHalfPrints.rpt")
                crystalReportViewer1.ReportSource = invReport1;
            else
                crystalReportViewer1.ReportSource = invReportPOS;

            crystalReportViewer1.RefreshReport();




        }

        private void InvociePrintFull()
        {// Sanjeewa 2016-12-31
            string invNo = default(string);
            string accNo = default(string);
            string cust_code = default(string);
            Boolean ishp = default(Boolean);
            ishp = false;
            invNo = GlbReportDoc;
            DataTable mst_tax_master = new DataTable();
            DataRow drr;
            DataTable salesDetails = new DataTable();
            DataTable sat_vou_det = new DataTable();
            DataTable param = new DataTable();
            DataTable mst_customer = new DataTable();

            param.Clear();
            param.Columns.Add("isCnote", typeof(Int16));

            Int16 isCredit = 0;

            salesDetails.Clear();


            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);

            sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(invNo);

            mst_tax_master = CHNLSVC.Sales.GetinvTax(invNo);
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

            _numPages = CHNLSVC.General.GetReprintDocCount(invNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr3);
            
            invReport3.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            


            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();


            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
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



                int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));
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
                        MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["sah_del_loc"].ToString());
                    }
                    else
                    {
                        dr["sah_del_loc"] = string.Empty;
                    }
                    cust_code = row["SAH_STRUCTURE_SEQ"].ToString(); //Sanjeewa 2015-08-05 QUOTATION NO
                    mst_customer = new DataTable();
                    mst_customer = CHNLSVC.Sales.Get_CustomerDetails(cust_code);
                    foreach (DataRow row2 in mst_customer.Rows)
                    {
                        dr["SAH_QT_CUST"] = row2["mbg_name"].ToString();
                    }
                    sar_sub_tp = CHNLSVC.Sales.GetSalesSubTypeTable(row["SAH_INV_TP"].ToString(), row["SAH_INV_SUB_TP"].ToString());

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
                        tblComDate = CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), accNo);
                    }
                    else
                    {
                        tblComDate = CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), invNo);
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

            DataTable deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
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
                MST_ITM1 = CHNLSVC.Sales.GetItemCode(row["ITH_COM"].ToString(), row["ITS_ITM_CD"].ToString());
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

            DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);


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

            DataTable hpt_shed = CHNLSVC.Sales.GetAccountSchedule(invNo);
            DataTable Promo = CHNLSVC.Sales.GetPromotionByInvoice(invNo);
            DataTable ref_rep_infor = new DataTable();

            if (GlbReportName == "InvoiceHalfPrints.rpt")
                ref_rep_infor = CHNLSVC.Sales.GetReportInfor("InvoiceHalfPrints.rpt");
            else
                ref_rep_infor = CHNLSVC.Sales.GetReportInfor("InvoicePOSPrint.rpt");        //kapila 29/6/2015

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
                        
            invReport3.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            invReport3.Database.Tables["mst_com"].SetDataSource(mst_com);
            invReport3.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            invReport3.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            invReport3.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            invReport3.Database.Tables["mst_item"].SetDataSource(mst_item);
            invReport3.Database.Tables["sec_user"].SetDataSource(sec_user);
            invReport3.Database.Tables["sar_sub_tp"].SetDataSource(sar_sub_tp);
            invReport3.Database.Tables["param"].SetDataSource(param);
            invReport3.Database.Tables["Promo"].SetDataSource(Promo);


            foreach (object repOp in invReport3.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptWarranty")
                    {
                        ReportDocument subRepDoc = invReport3.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                    }

                    if (_cs.SubreportName == "rptCheque")
                    {
                        ReportDocument subRepDoc = invReport3.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receiptCQ);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitmCQ);

                    }
                    if (_cs.SubreportName == "rptAccount")
                    {
                        ReportDocument subRepDoc = invReport3.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);
                        subRepDoc.Database.Tables["hpt_shed"].SetDataSource(hpt_shed);
                    }

                    if (_cs.SubreportName == "tax")
                    {
                        ReportDocument subRepDoc = invReport3.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);
                        
                    }
                    if (_cs.SubreportName == "rptComm")
                    {
                        ReportDocument subRepDoc = invReport3.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                    }
                    if (_cs.SubreportName == "rptWarr")
                    {
                        mst_item1 = mst_item1.DefaultView.ToTable(true);
                        ReportDocument subRepDoc = invReport3.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);
                        subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                        subRepDoc.Database.Tables["MST_ITM"].SetDataSource(mst_item1);
                        subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);

                    }
                    if (_cs.SubreportName == "giftVou")
                    {
                        ReportDocument subRepDoc = invReport3.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                    }
                    if (_cs.SubreportName == "loc")
                    {
                        ReportDocument subRepDoc = invReport3.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                    }


                    //if (tblComDate.Rows.Count >0) 
                    //{
                    //  if (_cs.SubreportName == "warrComDate")
                    //  {
                    //      ReportDocument subRepDoc = invReport3.Subreports[_cs.SubreportName];
                    //      subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                    //  }
                    //}

                }
            }
                        

            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = invReport3;
            
            crystalReportViewer1.RefreshReport();
        }

        private void InvocieFullPrint()
        {// Sanjeewa 18-04-2015
            string invNo = default(string);
            string accNo = default(string);
            Boolean ishp = default(Boolean);
            ishp = false;
            invNo = GlbReportDoc;
            DataTable mst_tax_master = new DataTable();
            DataRow drr;
            DataTable salesDetails = new DataTable();
            DataTable sat_vou_det = new DataTable();
            DataTable param = new DataTable();

            param.Clear();
            param.Columns.Add("isCnote", typeof(Int16));

            Int16 isCredit = 0;

            salesDetails.Clear();

            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);

            sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(invNo);

            mst_tax_master = CHNLSVC.Sales.GetinvTax(invNo);
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

            _numPages = CHNLSVC.General.GetReprintDocCount(invNo);
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


            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
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



                int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));
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
                        MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["sah_del_loc"].ToString());
                    }
                    else
                    {
                        dr["sah_del_loc"] = string.Empty;
                    }
                    sar_sub_tp = CHNLSVC.Sales.GetSalesSubTypeTable(row["SAH_INV_TP"].ToString(), row["SAH_INV_SUB_TP"].ToString());

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
                        tblComDate = CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), accNo);
                    }
                    else
                    {
                        tblComDate = CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), invNo);
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

            DataTable deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
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
                MST_ITM1 = CHNLSVC.Sales.GetItemCode(row["ITH_COM"].ToString(), row["ITS_ITM_CD"].ToString());
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

            DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);

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

            DataTable hpt_shed = CHNLSVC.Sales.GetAccountSchedule(invNo);
            DataTable Promo = CHNLSVC.Sales.GetPromotionByInvoice(invNo);

            DataTable ref_rep_infor = CHNLSVC.Sales.GetReportInfor("InvoiceHalfPrints.rpt");
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

                    }


                    if (_cs.SubreportName == "rptCheque")
                    {
                        ReportDocument subRepDoc = invfullReport.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receiptCQ);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitmCQ);

                    }
                    if (_cs.SubreportName == "rptAccount")
                    {
                        ReportDocument subRepDoc = invfullReport.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);
                        subRepDoc.Database.Tables["hpt_shed"].SetDataSource(hpt_shed);
                    }



                    if (_cs.SubreportName == "tax")
                    {
                        ReportDocument subRepDoc = invfullReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);


                    }
                    if (_cs.SubreportName == "rptComm")
                    {
                        ReportDocument subRepDoc = invfullReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                    }
                    if (_cs.SubreportName == "rptWarr")
                    {
                        mst_item1 = mst_item1.DefaultView.ToTable(true);
                        ReportDocument subRepDoc = invfullReport.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);
                        subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                        subRepDoc.Database.Tables["MST_ITM"].SetDataSource(mst_item1);
                        subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);



                    }
                    if (_cs.SubreportName == "giftVou")
                    {
                        ReportDocument subRepDoc = invfullReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                    }
                    if (_cs.SubreportName == "loc")
                    {
                        ReportDocument subRepDoc = invfullReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                    }


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




            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = invfullReport;

            crystalReportViewer1.RefreshReport();




        }
        //Tharanga 2017/06/07
        private void giftvouPrint()
        {
            string invNo = default(string);
            invNo = GlbReportDoc;
            DataTable odt = new DataTable();
            odt = CHNLSVC.Sales.get_sar_provou_tp(BaseCls.GlbUserComCode, invNo);
            if (odt.Rows.Count > 0)
            {
                giftvoucherprint.Database.Tables["giftvoucher"].SetDataSource(odt);
                this.Text = "Print voucher separately";
                crystalReportViewer1.ReportSource = giftvoucherprint;
                crystalReportViewer1.RefreshReport();
              
            }
           
          
            // report1.Close();
            // report1.Dispose();



        }
        private void InsurancePrint_INV()
        {// Nadeeka 26-12-12
            string _accNo = default(string);
            _accNo = GlbReportDoc;

            //  InsuPrints report1 = new InsuPrints();
            DataTable salesDetails = new DataTable();
            salesDetails.Clear();
            salesDetails = CHNLSVC.Sales.GetSalesDetails(null, _accNo);
            DataTable sat_hdr = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();

            DataTable hpt_cust = new DataTable();
            DataTable hpt_insu = new DataTable();

            hpt_cust = CHNLSVC.Sales.GetHpAccCustomer(_accNo);
            hpt_insu = CHNLSVC.Sales.GetInsurance(_accNo);


            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(_accNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            SinsReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


            sat_hdr.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();


            DataRow dr;
            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TP", typeof(string));
            mst_busentity.Columns.Add("MBE_NAME", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD1", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD2", typeof(string));

            sat_hdr.Columns.Add("SAH_DT", typeof(DateTime));
            sat_hdr.Columns.Add("SAH_INV_NO", typeof(string));
            sat_hdr.Columns.Add("SAH_INV_TP", typeof(string));
            sat_hdr.Columns.Add("SAH_IS_SVAT", typeof(Int16));
            sat_hdr.Columns.Add("SAH_MAN_REF", typeof(string));
            sat_hdr.Columns.Add("SAH_PC", typeof(string));
            sat_hdr.Columns.Add("SAH_REMARKS", typeof(string));
            sat_hdr.Columns.Add("SAH_SALES_EX_CD", typeof(string));
            sat_hdr.Columns.Add("SAH_TAX_EXEMPTED", typeof(Int16));
            sat_hdr.Columns.Add("SAH_TAX_INV", typeof(Int16));
            sat_hdr.Columns.Add("SAH_ACC_NO", typeof(string));
            sat_hdr.Columns.Add("SAH_D_CUST_CD", typeof(string));
            sat_hdr.Columns.Add("SAH_D_CUST_ADD2", typeof(string));
            sat_hdr.Columns.Add("SAH_D_CUST_ADD1", typeof(string));
            sat_hdr.Columns.Add("SAH_CUS_NAME", typeof(string));
            sat_hdr.Columns.Add("SAH_CUS_CD", typeof(string));
            sat_hdr.Columns.Add("SAH_CUS_ADD2", typeof(string));
            sat_hdr.Columns.Add("SAH_CUS_ADD1", typeof(string));
            sat_hdr.Columns.Add("SAH_SEQ_NO", typeof(decimal));
            sat_hdr.Columns.Add("SAH_COM", typeof(string));
            sat_hdr.Columns.Add("SAH_CRE_BY", typeof(string));



            sat_itm.Columns.Add("SAD_WARR_REMARKS", typeof(string));
            sat_itm.Columns.Add("SAD_UNIT_RT", typeof(decimal));
            sat_itm.Columns.Add("SAD_TOT_AMT", typeof(decimal));
            sat_itm.Columns.Add("SAD_QTY", typeof(decimal));
            sat_itm.Columns.Add("SAD_ITM_TAX_AMT", typeof(decimal));
            sat_itm.Columns.Add("SAD_ITM_CD", typeof(string));
            sat_itm.Columns.Add("SAD_DISC_RT", typeof(decimal));
            sat_itm.Columns.Add("SAD_DISC_AMT", typeof(decimal));
            sat_itm.Columns.Add("SAD_SEQ_NO", typeof(string));



            mst_profit_center.Columns.Add("MPC_ADD_1", typeof(string));
            mst_profit_center.Columns.Add("MPC_ADD_2", typeof(string));
            mst_profit_center.Columns.Add("MPC_CHNL", typeof(string));
            mst_profit_center.Columns.Add("MPC_DESC", typeof(string));
            mst_profit_center.Columns.Add("MPC_COM", typeof(string));
            mst_profit_center.Columns.Add("MPC_CD", typeof(string));



            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));



            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));
            mst_com.Columns.Add("MC_ANAL3", typeof(string));



            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));


            foreach (DataRow row in salesDetails.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails.Rows.IndexOf(row);
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TP"] = row["MBE_TP"].ToString();
                    dr["MBE_NAME"] = row["MBE_NAME"].ToString();
                    dr["MBE_ADD1"] = row["MBE_ADD1"].ToString();
                    dr["MBE_ADD2"] = row["MBE_ADD2"].ToString();


                    mst_busentity.Rows.Add(dr);


                    dr = sat_hdr.NewRow();

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
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    sat_hdr.Rows.Add(dr);
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
                    dr["MC_ANAL3"] = row["MC_ANAL3"].ToString();
                    mst_com.Rows.Add(dr);

                    dr = sec_user.NewRow();
                    dr["SE_USR_NAME"] = row["SE_USR_NAME"].ToString();
                    dr["SE_USR_ID"] = row["SE_USR_ID"].ToString();
                    sec_user.Rows.Add(dr);
                };

            }




            SinsReport1.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            SinsReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            SinsReport1.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            SinsReport1.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            SinsReport1.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
            SinsReport1.Database.Tables["hpt_cust"].SetDataSource(hpt_cust);
            SinsReport1.Database.Tables["hpt_insu"].SetDataSource(hpt_insu);




            mst_item = mst_item.DefaultView.ToTable(true);
            foreach (object repOp in SinsReport1.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptItem")
                    {
                        ReportDocument subRepDoc = SinsReport1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_itm"].SetDataSource(sat_itm);
                        subRepDoc.Database.Tables["mst_item"].SetDataSource(mst_item);


                    }


                }
            }

            this.Text = "Insurance Print";
            crystalReportViewer1.ReportSource = SinsReport1;
            crystalReportViewer1.RefreshReport();
            // report1.Close();
            // report1.Dispose();



        }
        private void InsurancePrint()
        {// Nadeeka 26-12-12
            string _accNo = default(string);
            _accNo = GlbReportDoc;

            //  InsuPrints report1 = new InsuPrints();
            DataTable salesDetails = new DataTable();
            salesDetails.Clear();
            salesDetails = CHNLSVC.Sales.GetSalesDetails(null, _accNo);
            DataTable sat_hdr = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            // DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();

            DataTable hpt_cust = new DataTable();
            DataTable hpt_insu = new DataTable();

            hpt_cust = CHNLSVC.Sales.GetHpAccCustomer(_accNo);
            hpt_insu = CHNLSVC.Sales.GetInsurance(_accNo);


            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(_accNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;
            PRINT_DOC.Rows.Add(dr3);
            insReport2.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


            sat_hdr.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            // mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();


            DataRow dr;
            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TP", typeof(string));
            mst_busentity.Columns.Add("MBE_NAME", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD1", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD2", typeof(string));

            sat_hdr.Columns.Add("SAH_DT", typeof(DateTime));
            sat_hdr.Columns.Add("SAH_INV_NO", typeof(string));
            sat_hdr.Columns.Add("SAH_INV_TP", typeof(string));
            sat_hdr.Columns.Add("SAH_IS_SVAT", typeof(Int16));
            sat_hdr.Columns.Add("SAH_MAN_REF", typeof(string));
            sat_hdr.Columns.Add("SAH_PC", typeof(string));
            sat_hdr.Columns.Add("SAH_REMARKS", typeof(string));
            sat_hdr.Columns.Add("SAH_SALES_EX_CD", typeof(string));
            sat_hdr.Columns.Add("SAH_TAX_EXEMPTED", typeof(Int16));
            sat_hdr.Columns.Add("SAH_TAX_INV", typeof(Int16));
            sat_hdr.Columns.Add("SAH_ACC_NO", typeof(string));
            sat_hdr.Columns.Add("SAH_D_CUST_CD", typeof(string));
            sat_hdr.Columns.Add("SAH_D_CUST_ADD2", typeof(string));
            sat_hdr.Columns.Add("SAH_D_CUST_ADD1", typeof(string));
            sat_hdr.Columns.Add("SAH_CUS_NAME", typeof(string));
            sat_hdr.Columns.Add("SAH_CUS_CD", typeof(string));
            sat_hdr.Columns.Add("SAH_CUS_ADD2", typeof(string));
            sat_hdr.Columns.Add("SAH_CUS_ADD1", typeof(string));
            sat_hdr.Columns.Add("SAH_SEQ_NO", typeof(decimal));
            sat_hdr.Columns.Add("SAH_COM", typeof(string));
            sat_hdr.Columns.Add("SAH_CRE_BY", typeof(string));



            sat_itm.Columns.Add("SAD_WARR_REMARKS", typeof(string));
            sat_itm.Columns.Add("SAD_UNIT_RT", typeof(decimal));
            sat_itm.Columns.Add("SAD_TOT_AMT", typeof(decimal));
            sat_itm.Columns.Add("SAD_QTY", typeof(decimal));
            sat_itm.Columns.Add("SAD_ITM_TAX_AMT", typeof(decimal));
            sat_itm.Columns.Add("SAD_ITM_CD", typeof(string));
            sat_itm.Columns.Add("SAD_DISC_RT", typeof(decimal));
            sat_itm.Columns.Add("SAD_DISC_AMT", typeof(decimal));
            sat_itm.Columns.Add("SAD_SEQ_NO", typeof(string));



            mst_profit_center.Columns.Add("MPC_ADD_1", typeof(string));
            mst_profit_center.Columns.Add("MPC_ADD_2", typeof(string));
            mst_profit_center.Columns.Add("MPC_CHNL", typeof(string));
            mst_profit_center.Columns.Add("MPC_DESC", typeof(string));
            mst_profit_center.Columns.Add("MPC_COM", typeof(string));
            mst_profit_center.Columns.Add("MPC_CD", typeof(string));
            mst_profit_center.Columns.Add("MPC_TEL", typeof(string));
            mst_profit_center.Columns.Add("MPC_FAX", typeof(string));
            mst_profit_center.Columns.Add("MPC_EMAIL", typeof(string));



            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));



            //mst_com.Columns.Add("MC_TAX1", typeof(string));
            //mst_com.Columns.Add("MC_TAX2", typeof(string));
            //mst_com.Columns.Add("MC_CD", typeof(string));
            //mst_com.Columns.Add("MC_DESC", typeof(string));
            //mst_com.Columns.Add("MC_IT_POWERED", typeof(string));
            //mst_com.Columns.Add("MC_ANAL3", typeof(string));
            //mst_com.Columns.Add("MC_TAX3", typeof(string));


            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));


            foreach (DataRow row in salesDetails.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails.Rows.IndexOf(row);
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TP"] = row["MBE_TP"].ToString();
                    dr["MBE_NAME"] = row["MBE_NAME"].ToString();
                    dr["MBE_ADD1"] = row["MBE_ADD1"].ToString();
                    dr["MBE_ADD2"] = row["MBE_ADD2"].ToString();


                    mst_busentity.Rows.Add(dr);


                    dr = sat_hdr.NewRow();

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
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    sat_hdr.Rows.Add(dr);
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
                    dr["MPC_TEL"] = row["MPC_TEL"].ToString();
                    dr["MPC_FAX"] = row["MPC_FAX"].ToString();
                    dr["MPC_EMAIL"] = row["MPC_EMAIL"].ToString();


                    mst_profit_center.Rows.Add(dr);


                    //dr = mst_com.NewRow();
                    //dr["MC_TAX1"] = row["MC_TAX1"].ToString();
                    //dr["MC_TAX2"] = row["MC_TAX2"].ToString();
                    //dr["MC_CD"] = row["MC_CD"].ToString();
                    //dr["MC_DESC"] = row["MC_DESC"].ToString();
                    //dr["MC_IT_POWERED"] = row["MC_IT_POWERED"].ToString();
                    //dr["MC_ANAL3"] = row["MC_ANAL3"].ToString();
                    //dr["MC_TAX3"] = row["MC_TAX3"].ToString();
                    //mst_com.Rows.Add(dr);

                    dr = sec_user.NewRow();
                    dr["SE_USR_NAME"] = row["SE_USR_NAME"].ToString();
                    dr["SE_USR_ID"] = row["SE_USR_ID"].ToString();
                    sec_user.Rows.Add(dr);
                };

            }

            DataTable mst_com = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);


            insReport2.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            insReport2.Database.Tables["mst_com"].SetDataSource(mst_com);
            insReport2.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            insReport2.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
            insReport2.Database.Tables["hpt_cust"].SetDataSource(hpt_cust);
            insReport2.Database.Tables["hpt_insu"].SetDataSource(hpt_insu);




            mst_item = mst_item.DefaultView.ToTable(true);
            foreach (object repOp in insReport2.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptItem")
                    {
                        ReportDocument subRepDoc = insReport2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_itm"].SetDataSource(sat_itm);
                        subRepDoc.Database.Tables["mst_item"].SetDataSource(mst_item);


                    }


                }
            }

            this.Text = "Insurance Print";
            crystalReportViewer1.ReportSource = insReport2;
            crystalReportViewer1.RefreshReport();
            // report1.Close();
            // report1.Dispose();



        }
        private void Receipt_print_Inv()
        {// Nadeeka 26-12-12
            string docNo = default(string);
            docNo = GlbReportDoc;



            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();
            sat_receipt.Clear();


            sat_receipt = CHNLSVC.Sales.GetReceipt(GlbReportDoc);
            sat_receiptitm = CHNLSVC.Sales.GetReceiptItemDetails(GlbReportDoc);
            DataTable mst_profit_center = new DataTable();

            DataTable mst_rec_tp = new DataTable();
            DataTable hpt_insu = new DataTable();
            DataTable sat_recwarrex = new DataTable();
            DataTable sat_veh_reg_txn = new DataTable();
            DataTable sat_receiptitemdetails = new DataTable();
            DataTable mst_com = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            DataTable mst_emp = new DataTable();


            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            Srecreport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            foreach (DataRow row in sat_receipt.Rows)
            {

                mst_profit_center = CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbUserComCode, GlbReportProfit);
                mst_rec_tp = CHNLSVC.Sales.GetReceiptType(row["SAR_RECEIPT_TYPE"].ToString());
                hpt_insu = CHNLSVC.Sales.GetInsurance(row["SAR_RECEIPT_NO"].ToString());
                sat_recwarrex = CHNLSVC.Sales.GetReceiptWarranty(row["SAR_RECEIPT_NO"].ToString());
                sat_veh_reg_txn = CHNLSVC.Sales.GetVehicalRegistrations(row["SAR_RECEIPT_NO"].ToString());
                sat_receiptitemdetails = CHNLSVC.Sales.GetAdvanRecItems(row["SAR_RECEIPT_NO"].ToString());
                if (!string.IsNullOrEmpty(row["sar_anal_4"].ToString()))
                {
                    mst_emp = CHNLSVC.Sales.GetinvEmp(row["SAR_COM_CD"].ToString(), row["sar_anal_4"].ToString());
                }
            }

            DataTable MST_ITM = new DataTable();

            foreach (DataRow row in sat_veh_reg_txn.Rows)
            {
                MST_ITM = CHNLSVC.Sales.GetItemCode(BaseCls.GlbUserComCode, row["srvt_itm_cd"].ToString());
            }



            Srecreport1.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
            Srecreport1.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            Srecreport1.Database.Tables["mst_rec_tp"].SetDataSource(mst_rec_tp);



            foreach (object repOp in Srecreport1.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptVehicle")
                    {
                        ReportDocument subRepDoc = Srecreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_veh_reg_txn"].SetDataSource(sat_veh_reg_txn);
                        subRepDoc.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);


                    }
                    if (_cs.SubreportName == "warr")
                    {
                        ReportDocument subRepDoc = Srecreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_recwarrex"].SetDataSource(sat_recwarrex);

                    }
                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = Srecreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }
                    if (_cs.SubreportName == "pay")
                    {
                        ReportDocument subRepDoc = Srecreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "inv")
                    {
                        ReportDocument subRepDoc = Srecreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "item")
                    {
                        ReportDocument subRepDoc = Srecreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitemdetails"].SetDataSource(sat_receiptitemdetails);

                    }

                    if (_cs.SubreportName == "gv")
                    {
                        ReportDocument subRepDoc = Srecreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitemdetails"].SetDataSource(sat_receiptitemdetails);

                    }
                    if (_cs.SubreportName == "emp")
                    {
                        ReportDocument subRepDoc = Srecreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_emp"].SetDataSource(mst_emp);

                    }



                }
            }

            crystalReportViewer1.ReportSource = Srecreport1;
            this.Text = "Receipt Print";
            crystalReportViewer1.RefreshReport();



        }
        private void Receipt_printDealer()
        {// Nadeeka 05-03-14
            string docNo = default(string);
            docNo = GlbReportDoc;



            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();
            sat_receipt.Clear();
            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            //  _DealerrecReport.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


            sat_receipt = CHNLSVC.Sales.GetReceipt(GlbReportDoc);

            sat_receiptitm = CHNLSVC.Sales.GetReceiptItemDetails(GlbReportDoc);

            DataTable mst_profit_center = new DataTable();

            DataTable mst_rec_tp = new DataTable();

            DataTable sat_veh_reg_txn = new DataTable();


            DataTable mst_com = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);

            foreach (DataRow row in sat_receipt.Rows)
            {
                sat_veh_reg_txn = CHNLSVC.Sales.GetVehicalRegistrations(row["SAR_RECEIPT_NO"].ToString());
                mst_profit_center = CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbUserComCode, GlbReportProfit);
                mst_rec_tp = CHNLSVC.Sales.GetReceiptType(row["SAR_RECEIPT_TYPE"].ToString());

            }
            DataTable MST_ITM = new DataTable();
            foreach (DataRow row in sat_veh_reg_txn.Rows)
            {
                MST_ITM = CHNLSVC.Sales.GetItemCode(BaseCls.GlbUserComCode, row["srvt_itm_cd"].ToString());
            }

            DataTable mst_rec_div = CHNLSVC.Sales.GetRecDivision(BaseCls.GlbUserComCode, GlbReportProfit);

            delrecreport1.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
            delrecreport1.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            delrecreport1.Database.Tables["mst_rec_tp"].SetDataSource(mst_rec_tp);
            delrecreport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            delrecreport1.Database.Tables["mst_rec_div"].SetDataSource(mst_rec_div);

            foreach (object repOp in delrecreport1.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptVehicle")
                    {
                        ReportDocument subRepDoc = delrecreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_veh_reg_txn"].SetDataSource(sat_veh_reg_txn);
                        subRepDoc.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                    }
                    if (_cs.SubreportName == "rptRec")
                    {
                        ReportDocument subRepDoc = delrecreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "inv")
                    {
                        ReportDocument subRepDoc = delrecreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = delrecreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }


                }
            }

            crystalReportViewer1.ReportSource = delrecreport1;
            this.Text = "Receipt Print";
            crystalReportViewer1.RefreshReport();



        }
        private void Receipt_print_Consignment()
        {// Nadeeka 17-03-14
            string docNo = default(string);
            docNo = GlbReportDoc;



            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();
            sat_receipt.Clear();


            sat_receipt = CHNLSVC.Sales.GetReceipt(GlbReportDoc);
            sat_receiptitm = CHNLSVC.Sales.GetReceiptItemDetails(GlbReportDoc);
            DataTable mst_profit_center = new DataTable();

            DataTable mst_rec_tp = new DataTable();
            DataTable hpt_insu = new DataTable();
            DataTable sat_recwarrex = new DataTable();
            DataTable sat_veh_reg_txn = new DataTable();
            DataTable sat_receiptitemdetails = new DataTable();
            DataTable mst_com = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            DataTable mst_emp = new DataTable();


            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            recreport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            foreach (DataRow row in sat_receipt.Rows)
            {

                mst_profit_center = CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbUserComCode, GlbReportProfit);
                mst_rec_tp = CHNLSVC.Sales.GetReceiptType(row["SAR_RECEIPT_TYPE"].ToString());
                hpt_insu = CHNLSVC.Sales.GetInsurance(row["SAR_RECEIPT_NO"].ToString());
                sat_recwarrex = CHNLSVC.Sales.GetReceiptWarranty(row["SAR_RECEIPT_NO"].ToString());
                sat_veh_reg_txn = CHNLSVC.Sales.GetVehicalRegistrations(row["SAR_RECEIPT_NO"].ToString());
                sat_receiptitemdetails = CHNLSVC.Sales.GetAdvanRecItems(row["SAR_RECEIPT_NO"].ToString());
                if (!string.IsNullOrEmpty(row["sar_anal_4"].ToString()))
                {
                    mst_emp = CHNLSVC.Sales.GetinvEmp(row["SAR_COM_CD"].ToString(), row["sar_anal_4"].ToString());
                }
            }
            DataTable mst_buscom = CHNLSVC.Sales.GetInsuranceCompanyName(GlbReportDoc);

            DataTable MST_ITM = new DataTable();

            foreach (DataRow row in sat_veh_reg_txn.Rows)
            {
                MST_ITM = CHNLSVC.Sales.GetItemCode(BaseCls.GlbUserComCode, row["srvt_itm_cd"].ToString());
            }



            recConreport1.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
            recConreport1.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            recConreport1.Database.Tables["mst_rec_tp"].SetDataSource(mst_rec_tp);
            recConreport1.Database.Tables["mst_com"].SetDataSource(mst_com);


            foreach (object repOp in recConreport1.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptVehicle")
                    {
                        ReportDocument subRepDoc = recConreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_veh_reg_txn"].SetDataSource(sat_veh_reg_txn);
                        subRepDoc.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);


                    }
                    if (_cs.SubreportName == "warr")
                    {
                        ReportDocument subRepDoc = recConreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_recwarrex"].SetDataSource(sat_recwarrex);

                    }
                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = recConreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }
                    if (_cs.SubreportName == "pay")
                    {
                        ReportDocument subRepDoc = recConreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "inv")
                    {
                        ReportDocument subRepDoc = recConreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "item")
                    {
                        ReportDocument subRepDoc = recConreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitemdetails"].SetDataSource(sat_receiptitemdetails);

                    }

                    if (_cs.SubreportName == "gv")
                    {
                        ReportDocument subRepDoc = recConreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitemdetails"].SetDataSource(sat_receiptitemdetails);

                    }
                    if (_cs.SubreportName == "emp")
                    {
                        ReportDocument subRepDoc = recConreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_emp"].SetDataSource(mst_emp);

                    }
                    if (_cs.SubreportName == "insCom")
                    {
                        ReportDocument subRepDoc = recConreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_buscom"].SetDataSource(mst_buscom);

                    }


                }
            }

            crystalReportViewer1.ReportSource = recConreport1;
            this.Text = "Receipt Print";
            crystalReportViewer1.RefreshReport();



        }
        
        
        private void Receipt_print()
        {// Nadeeka 26-12-12
            string docNo = default(string);
            docNo = GlbReportDoc;



            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();
            sat_receipt.Clear();


            sat_receipt = CHNLSVC.Sales.GetReceipt(GlbReportDoc);
            sat_receiptitm = CHNLSVC.Sales.GetReceiptItemDetails(GlbReportDoc);
            DataTable mst_profit_center = new DataTable();

            DataTable mst_rec_tp = new DataTable();
            DataTable hpt_insu = new DataTable();
            DataTable sat_recwarrex = new DataTable();
            DataTable sat_veh_reg_txn = new DataTable();
            DataTable sat_receiptitemdetails = new DataTable();
            DataTable mst_com = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            DataTable mst_emp = new DataTable();


            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;
            PRINT_DOC.Rows.Add(dr3);
            recreport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            foreach (DataRow row in sat_receipt.Rows)
            {


                mst_profit_center = CHNLSVC.Sales.GetProfitCenterTable(row["sar_com_cd"].ToString(), row["sar_profit_center_cd"].ToString());
                mst_rec_tp = CHNLSVC.Sales.GetReceiptType(row["SAR_RECEIPT_TYPE"].ToString());
                hpt_insu = CHNLSVC.Sales.GetInsurance(row["SAR_RECEIPT_NO"].ToString());
                sat_recwarrex = CHNLSVC.Sales.GetReceiptWarranty(row["SAR_RECEIPT_NO"].ToString());
                sat_veh_reg_txn = CHNLSVC.Sales.GetVehicalRegistrations(row["SAR_RECEIPT_NO"].ToString());
                sat_receiptitemdetails = CHNLSVC.Sales.GetAdvanRecItems(row["SAR_RECEIPT_NO"].ToString());
                if (!string.IsNullOrEmpty(row["sar_anal_4"].ToString()))
                {
                    mst_emp = CHNLSVC.Sales.GetinvEmp(row["SAR_COM_CD"].ToString(), row["sar_anal_4"].ToString());
                }
            }
            DataTable mst_buscom = CHNLSVC.Sales.GetInsuranceCompanyName(GlbReportDoc);

            DataTable itemSerial = CHNLSVC.Sales.GetInvoice_Serials(GlbReportDoc);

            DataTable MST_ITM = new DataTable();

            foreach (DataRow row in sat_veh_reg_txn.Rows)
            {
                MST_ITM = CHNLSVC.Sales.GetItemCode(BaseCls.GlbUserComCode, row["srvt_itm_cd"].ToString());
            }



            recreport1.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
            recreport1.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            recreport1.Database.Tables["mst_rec_tp"].SetDataSource(mst_rec_tp);
            recreport1.Database.Tables["mst_com"].SetDataSource(mst_com);


            foreach (object repOp in recreport1.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptVehicle")
                    {
                        ReportDocument subRepDoc = recreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_veh_reg_txn"].SetDataSource(sat_veh_reg_txn);
                        subRepDoc.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);


                    }
                    if (_cs.SubreportName == "warr")
                    {
                        ReportDocument subRepDoc = recreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_recwarrex"].SetDataSource(sat_recwarrex);

                    }
                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = recreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }
                    if (_cs.SubreportName == "pay")
                    {
                        ReportDocument subRepDoc = recreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "inv")
                    {
                        ReportDocument subRepDoc = recreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "item")
                    {
                        ReportDocument subRepDoc = recreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitemdetails"].SetDataSource(sat_receiptitemdetails);

                    }

                    if (_cs.SubreportName == "gv")
                    {
                        ReportDocument subRepDoc = recreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitemdetails"].SetDataSource(sat_receiptitemdetails);

                    }
                    if (_cs.SubreportName == "emp")
                    {
                        ReportDocument subRepDoc = recreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_emp"].SetDataSource(mst_emp);

                    }
                    if (_cs.SubreportName == "insCom")
                    {
                        ReportDocument subRepDoc = recreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_buscom"].SetDataSource(mst_buscom);

                    }

                    if (_cs.SubreportName == "ItemSerials")
                    {
                        ReportDocument subRepDoc = recreport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["itemSerial"].SetDataSource(itemSerial);

                    }

                }
            }

            crystalReportViewer1.ReportSource = recreport1;
            this.Text = "Receipt Print";
            crystalReportViewer1.RefreshReport();



        }
       
        private void Receipt_print_n()
        {// Sanjeewa 31-08-2015
            string docNo = default(string);
            docNo = GlbReportDoc;
            
            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();
            sat_receipt.Clear();


            sat_receipt = CHNLSVC.Sales.GetReceipt(GlbReportDoc);
            sat_receiptitm = CHNLSVC.Sales.GetReceiptItemDetails(GlbReportDoc);
            DataTable mst_profit_center = new DataTable();

            DataTable mst_rec_tp = new DataTable();
            DataTable hpt_insu = new DataTable();
            DataTable sat_recwarrex = new DataTable();
            DataTable sat_veh_reg_txn = new DataTable();
            DataTable sat_receiptitemdetails = new DataTable();
            DataTable mst_com = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            DataTable mst_emp = new DataTable();


            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;
            PRINT_DOC.Rows.Add(dr3);
            recreport1_n.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            foreach (DataRow row in sat_receipt.Rows)
            {


                mst_profit_center = CHNLSVC.Sales.GetProfitCenterTable(row["sar_com_cd"].ToString(), row["sar_profit_center_cd"].ToString());
                mst_rec_tp = CHNLSVC.Sales.GetReceiptType(row["SAR_RECEIPT_TYPE"].ToString());
                hpt_insu = CHNLSVC.Sales.GetInsurance(row["SAR_RECEIPT_NO"].ToString());
                sat_recwarrex = CHNLSVC.Sales.GetReceiptWarranty(row["SAR_RECEIPT_NO"].ToString());
                sat_veh_reg_txn = CHNLSVC.Sales.GetVehicalRegistrations(row["SAR_RECEIPT_NO"].ToString());
                sat_receiptitemdetails = CHNLSVC.Sales.GetAdvanRecItems(row["SAR_RECEIPT_NO"].ToString());
                if (!string.IsNullOrEmpty(row["sar_anal_4"].ToString()))
                {
                    mst_emp = CHNLSVC.Sales.GetinvEmp(row["SAR_COM_CD"].ToString(), row["sar_anal_4"].ToString());
                }
            }
            DataTable mst_buscom = CHNLSVC.Sales.GetInsuranceCompanyName(GlbReportDoc);

            DataTable itemSerial = CHNLSVC.Sales.GetInvoice_Serials(GlbReportDoc);

            DataTable MST_ITM = new DataTable();

            foreach (DataRow row in sat_veh_reg_txn.Rows)
            {
                MST_ITM = CHNLSVC.Sales.GetItemCode(BaseCls.GlbUserComCode, row["srvt_itm_cd"].ToString());
            }



            recreport1_n.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
            recreport1_n.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            recreport1_n.Database.Tables["mst_rec_tp"].SetDataSource(mst_rec_tp);
            recreport1_n.Database.Tables["mst_com"].SetDataSource(mst_com);


            foreach (object repOp in recreport1_n.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptVehicle")
                    {
                        ReportDocument subRepDoc = recreport1_n.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_veh_reg_txn"].SetDataSource(sat_veh_reg_txn);
                        subRepDoc.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);


                    }
                    if (_cs.SubreportName == "warr")
                    {
                        ReportDocument subRepDoc = recreport1_n.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_recwarrex"].SetDataSource(sat_recwarrex);

                    }
                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = recreport1_n.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }
                    if (_cs.SubreportName == "pay")
                    {
                        ReportDocument subRepDoc = recreport1_n.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "paymode")
                    {
                        ReportDocument subRepDoc = recreport1_n.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "inv")
                    {
                        ReportDocument subRepDoc = recreport1_n.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "item")
                    {
                        ReportDocument subRepDoc = recreport1_n.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitemdetails"].SetDataSource(sat_receiptitemdetails);

                    }

                    if (_cs.SubreportName == "gv")
                    {
                        ReportDocument subRepDoc = recreport1_n.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitemdetails"].SetDataSource(sat_receiptitemdetails);

                    }
                    if (_cs.SubreportName == "emp")
                    {
                        ReportDocument subRepDoc = recreport1_n.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_emp"].SetDataSource(mst_emp);

                    }
                    if (_cs.SubreportName == "insCom")
                    {
                        ReportDocument subRepDoc = recreport1_n.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_buscom"].SetDataSource(mst_buscom);

                    }

                    if (_cs.SubreportName == "ItemSerials")
                    {
                        ReportDocument subRepDoc = recreport1_n.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["itemSerial"].SetDataSource(itemSerial);

                    }

                }
            }

            crystalReportViewer1.ReportSource = recreport1_n;
            this.Text = "Receipt Print";
            crystalReportViewer1.RefreshReport();



        }
       
        private void DealerReceipt_print()
        {// Nadeeka 26-12-12
            string docNo = default(string);
            docNo = GlbReportDoc;



            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();
            sat_receipt.Clear();
            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            //  _DealerrecReport.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


            sat_receipt = CHNLSVC.Sales.GetReceipt(GlbReportDoc);

            sat_receiptitm = CHNLSVC.Sales.GetReceiptItemDetails(GlbReportDoc);

            DataTable mst_profit_center = new DataTable();

            DataTable mst_rec_tp = new DataTable();

            DataTable sat_veh_reg_txn = new DataTable();


            DataTable mst_com = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);

            foreach (DataRow row in sat_receipt.Rows)
            {
                sat_veh_reg_txn = CHNLSVC.Sales.GetVehicalRegistrations(row["SAR_RECEIPT_NO"].ToString());
                mst_profit_center = CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbUserComCode, GlbReportProfit);
                mst_rec_tp = CHNLSVC.Sales.GetReceiptType(row["SAR_RECEIPT_TYPE"].ToString());

            }
            DataTable MST_ITM = new DataTable();
            foreach (DataRow row in sat_veh_reg_txn.Rows)
            {
                MST_ITM = CHNLSVC.Sales.GetItemCode(BaseCls.GlbUserComCode, row["srvt_itm_cd"].ToString());
            }

            DataTable mst_rec_div = CHNLSVC.Sales.GetRecDivision(BaseCls.GlbUserComCode, GlbReportProfit);

            _DealerrecReport.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
            _DealerrecReport.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _DealerrecReport.Database.Tables["mst_rec_tp"].SetDataSource(mst_rec_tp);
            _DealerrecReport.Database.Tables["mst_com"].SetDataSource(mst_com);
            _DealerrecReport.Database.Tables["mst_rec_div"].SetDataSource(mst_rec_div);

            foreach (object repOp in _DealerrecReport.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptVehicle")
                    {
                        ReportDocument subRepDoc = _DealerrecReport.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_veh_reg_txn"].SetDataSource(sat_veh_reg_txn);
                        subRepDoc.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                    }
                    if (_cs.SubreportName == "rptRec")
                    {
                        ReportDocument subRepDoc = _DealerrecReport.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "inv")
                    {
                        ReportDocument subRepDoc = _DealerrecReport.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = _DealerrecReport.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }


                }
            }

            crystalReportViewer1.ReportSource = _DealerrecReport;
            this.Text = "Receipt Print";
            crystalReportViewer1.RefreshReport();



        }
        private void HPReceipt_print()
        {// Nadeeka 27-01-13
            string accNo = default(string);
            accNo = GlbReportDoc;

            DataTable sat_receipt = new DataTable();

            sat_receipt.Clear();

            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _HpRec.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


            DataTable mst_profit_center = new DataTable();
            DataTable hpt_acc = CHNLSVC.Sales.GetHP_Account_AccNo(accNo);
            DataTable hpt_txn = CHNLSVC.Sales.GetAccountTrans(accNo);
            DataTable mst_com = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            sat_receipt = CHNLSVC.Sales.GetReceipt(accNo);
            DataTable sat_hdr = new DataTable();
            DataTable sat_itm = new DataTable();

            foreach (DataRow row in sat_receipt.Rows)
            {

                mst_profit_center = CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbUserComCode, BaseCls.GlbReportProfit);
                sat_hdr = CHNLSVC.Sales.GetInvoiceByAccountNoTable(row["SAR_COM_CD"].ToString(), row["SAR_PROFIT_CENTER_CD"].ToString(), accNo);
            }
            foreach (DataRow row in sat_hdr.Rows)
            {
                sat_itm = CHNLSVC.Sales.GetInvoiceDetailByInvoiceTable(row["SAH_INV_NO"].ToString());
            }



            _HpRec.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);
            _HpRec.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
            _HpRec.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _HpRec.Database.Tables["hpt_txn"].SetDataSource(hpt_txn);

            foreach (object repOp in _HpRec.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;


                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = _HpRec.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }

                    if (_cs.SubreportName == "rptItem")
                    {
                        ReportDocument subRepDoc = _HpRec.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                        subRepDoc.Database.Tables["sat_itm"].SetDataSource(sat_itm);

                    }
                    if (_cs.SubreportName == "rptName")
                    {
                        ReportDocument subRepDoc = _HpRec.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }
                }
            }

            crystalReportViewer1.ReportSource = _HpRec;
            this.Text = "Receipt Print";
            crystalReportViewer1.RefreshReport();



        }

        private void InvociePrintBOC()
        {// Sanjeewa 2014-09-04

            string invNo = default(string);
            string cust_code = default(string);
            invNo = GlbReportDoc;

            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _DealerinvBOC.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


            DataTable salesDetails = new DataTable();
            salesDetails.Clear();
            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable int_batch = new DataTable();
            DataTable int_batch1 = new DataTable();
            DataTable mst_item1 = new DataTable();
            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();


            DataRow dr;
            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_NAME", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD1", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD2", typeof(string));


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
            mst_item.Columns.Add("MI_BRAND", typeof(string));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_BRAND", typeof(string));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));

            int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));


            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));


            foreach (DataRow row in salesDetails.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails.Rows.IndexOf(row);
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_NAME"] = row["MBE_NAME"].ToString();
                    dr["MBE_ADD1"] = row["MBE_ADD1"].ToString();
                    dr["MBE_ADD2"] = row["MBE_ADD2"].ToString();

                    mst_busentity.Rows.Add(dr);

                    MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["SAH_DEL_LOC"].ToString());

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
                    dr["SAH_D_CUST_NAME"] = row["SAH_D_CUST_NAME"].ToString();
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    sat_hdr1.Rows.Add(dr);
                };

                // int_batch = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));

                int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));


                foreach (DataRow row1 in int_batch1.Rows)
                {
                    dr = int_batch.NewRow();
                    dr["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                    dr["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                    dr["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                    dr["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                    dr["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                    int_batch.Rows.Add(dr);

                }


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
                dr["MI_BRAND"] = row["MI_BRAND"].ToString();
                mst_item.Rows.Add(dr);
                //if (row["MI_CD"].ToString() != "LPHESEJK01")
                //{
                //    if (row["MI_CD"].ToString() != "LPHEHKHL01")
                //    {
                //        if (row["MI_BRAND"].ToString() != "TITAN")
                //        {
                //            dr = mst_item1.NewRow();
                //            dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                //            dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                //            dr["MI_CD"] = row["MI_CD"].ToString();
                //            dr["MI_BRAND"] = row["MI_BRAND"].ToString();
                //            mst_item1.Rows.Add(dr);
                //        }
                //    }
                //}

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


            DataTable deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
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

            foreach (DataRow row in deliveredSerials.Rows)
            {
                dr = int_hdr.NewRow();
                dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                dr["ITH_COM"] = row["ITH_COM"].ToString();
                dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                int_hdr.Rows.Add(dr);

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

            }

            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
            DataTable hpt_acc = new DataTable();
            DataTable sat_itm_tax = CHNLSVC.Sales.GetSalesTax(invNo);


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

            mst_item = mst_item.DefaultView.ToTable(true);

            _DealerinvBOC.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _DealerinvBOC.Database.Tables["mst_com"].SetDataSource(mst_com);
            _DealerinvBOC.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            _DealerinvBOC.Database.Tables["sec_user"].SetDataSource(sec_user);
            _DealerinvBOC.Database.Tables["viw_sales_details"].SetDataSource(salesDetails);
            _DealerinvBOC.Database.Tables["sat_itm_tax"].SetDataSource(sat_itm_tax);
            _DealerinvBOC.Database.Tables["MST_ITM"].SetDataSource(mst_item);

            if (int_batch.Rows.Count > 0)
            {
                int_batch = int_batch.DefaultView.ToTable(true);
            }

            foreach (object repOp in _DealerinvBOC.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "delLoc")
                    {
                        ReportDocument subRepDoc = _DealerinvBOC.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

                    }
                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = _DealerinvBOC.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }
                    //if (_cs.SubreportName == "rptEngine")
                    //{
                    //    ReportDocument subRepDoc = _DealerinvBOC.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                    //    subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);

                    //}

                }
            }



            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = _DealerinvBOC;

            crystalReportViewer1.RefreshReport();


        }


        private void InvociePrintDealerInsurance()
        {// Nadeeka 24-01-13
            string invNo = default(string);
            string cust_code = default(string);
            invNo = GlbReportDoc;

            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _DealerinvInsReport.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            string _pc = "";
            DataTable salesDetails = new DataTable();
            salesDetails.Clear();
            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable int_batch = new DataTable();
            DataTable int_batch1 = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_customer = new DataTable();
            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();


            DataRow dr;
            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_NAME", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD1", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD2", typeof(string));


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
            mst_item.Columns.Add("MI_BRAND", typeof(string));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_BRAND", typeof(string));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));

            int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));


            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));


            foreach (DataRow row in salesDetails.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails.Rows.IndexOf(row);
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_NAME"] = row["MBE_NAME"].ToString();
                    dr["MBE_ADD1"] = row["MBE_ADD1"].ToString();
                    dr["MBE_ADD2"] = row["MBE_ADD2"].ToString();

                    mst_busentity.Rows.Add(dr);

                    MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["SAH_DEL_LOC"].ToString());

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
                    dr["SAH_D_CUST_NAME"] = row["SAH_D_CUST_NAME"].ToString();
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    sat_hdr1.Rows.Add(dr);

                    _pc = row["SAH_PC"].ToString();
                    cust_code = row["SAH_STRUCTURE_SEQ"].ToString(); //Sanjeewa 2015-08-05 QUOTATION NO
                };

                // int_batch = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));

                int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));


                foreach (DataRow row1 in int_batch1.Rows)
                {
                    dr = int_batch.NewRow();
                    dr["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                    dr["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                    dr["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                    dr["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                    dr["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                    int_batch.Rows.Add(dr);

                }


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
                dr["MI_BRAND"] = row["MI_BRAND"].ToString();
                mst_item.Rows.Add(dr);
                //if (row["MI_CD"].ToString() != "LPHESEJK01")
                //{
                //    if (row["MI_CD"].ToString() != "LPHEHKHL01")
                //    {
                //        if (row["MI_BRAND"].ToString() != "TITAN")
                //        {
                //            dr = mst_item1.NewRow();
                //            dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                //            dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                //            dr["MI_CD"] = row["MI_CD"].ToString();
                //            dr["MI_BRAND"] = row["MI_BRAND"].ToString();
                //            mst_item1.Rows.Add(dr);
                //        }
                //    }
                //}

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

            DataTable deliveredSerials = new DataTable();
            if (_pc != "BOC")
            {
                deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
            }
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

            if (_pc != "BOC")
            {
                foreach (DataRow row in deliveredSerials.Rows)
                {
                    dr = int_hdr.NewRow();
                    dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                    dr["ITH_COM"] = row["ITH_COM"].ToString();
                    dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                    dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                    int_hdr.Rows.Add(dr);

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

                }
            }
            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
            DataTable hpt_acc = new DataTable();
            DataTable sat_itm_tax = CHNLSVC.Sales.GetSalesTax(invNo);


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

            mst_customer = new DataTable();
            mst_customer = CHNLSVC.Sales.Get_CustomerDetails(cust_code); 
            
            mst_item = mst_item.DefaultView.ToTable(true);

            _DealerinvInsReport.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _DealerinvInsReport.Database.Tables["mst_com"].SetDataSource(mst_com);
            _DealerinvInsReport.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            _DealerinvInsReport.Database.Tables["sec_user"].SetDataSource(sec_user);
            _DealerinvInsReport.Database.Tables["viw_sales_details"].SetDataSource(salesDetails);
            _DealerinvInsReport.Database.Tables["sat_itm_tax"].SetDataSource(sat_itm_tax);
            _DealerinvInsReport.Database.Tables["MST_ITM"].SetDataSource(mst_item);

            if (int_batch.Rows.Count > 0)
            {
                int_batch = int_batch.DefaultView.ToTable(true);
            }

            foreach (object repOp in _DealerinvInsReport.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "delLoc")
                    {
                        ReportDocument subRepDoc = _DealerinvInsReport.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

                    }
                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = _DealerinvInsReport.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }
                    if (_cs.SubreportName == "rptEngine")
                    {
                        ReportDocument subRepDoc = _DealerinvInsReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                        subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);

                    }
                    if (_cs.SubreportName == "Del_Detail")
                    {
                        ReportDocument subRepDoc = _DealerinvInsReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_BUSENTITY_GRUP"].SetDataSource(mst_customer);
                    } 
                }
            }



            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = _DealerinvInsReport;

            crystalReportViewer1.RefreshReport();




        }
        private void InvociePrint_INV_NEW1()
        {// Nadeeka 11-02-14
            string invNo = default(string);
            invNo = GlbReportDoc;


            DataTable salesDetails = new DataTable();
            salesDetails.Clear();
            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable int_batch = new DataTable();
            DataTable int_batch1 = new DataTable();
            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();
            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            invReportnew1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            DataRow dr;
            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_NAME", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD1", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD2", typeof(string));
            mst_busentity.Columns.Add("mbe_svat_no", typeof(string));

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
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_NAME", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_SEQ_NO", typeof(decimal));
            sat_hdr1.Columns.Add("SAH_COM", typeof(string));
            sat_hdr1.Columns.Add("SAH_CRE_BY", typeof(string));
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
            sat_itm.Columns.Add("sad_warr_period", typeof(Int16));

            mst_profit_center.Columns.Add("MPC_ADD_1", typeof(string));
            mst_profit_center.Columns.Add("MPC_ADD_2", typeof(string));
            mst_profit_center.Columns.Add("MPC_CHNL", typeof(string));
            mst_profit_center.Columns.Add("MPC_DESC", typeof(string));
            mst_profit_center.Columns.Add("MPC_COM", typeof(string));
            mst_profit_center.Columns.Add("MPC_CD", typeof(string));
            mst_profit_center.Columns.Add("MPC_TEL", typeof(string));


            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));
            mst_item.Columns.Add("MI_BRAND", typeof(string));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_BRAND", typeof(string));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));


            int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));

            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));


            foreach (DataRow row in salesDetails.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails.Rows.IndexOf(row);
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_NAME"] = row["MBE_NAME"].ToString();
                    dr["MBE_ADD1"] = row["MBE_ADD1"].ToString();
                    dr["MBE_ADD2"] = row["MBE_ADD2"].ToString();
                    dr["mbe_svat_no"] = row["mbe_svat_no"].ToString();

                    mst_busentity.Rows.Add(dr);

                    MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["SAH_DEL_LOC"].ToString());

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
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    dr["SAH_ANAL_4"] = row["SAH_ANAL_4"].ToString();
                    sat_hdr1.Rows.Add(dr);
                };

                int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));


                foreach (DataRow row1 in int_batch1.Rows)
                {
                    dr = int_batch.NewRow();
                    dr["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                    dr["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                    dr["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                    dr["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                    dr["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                    int_batch.Rows.Add(dr);

                }


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
                dr["sad_warr_period"] = Convert.ToInt16(row["sad_warr_period"].ToString());

                sat_itm.Rows.Add(dr);



                dr = mst_item.NewRow();
                dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                dr["MI_CD"] = row["MI_CD"].ToString();
                dr["MI_BRAND"] = row["MI_BRAND"].ToString();

                mst_item.Rows.Add(dr);

                if (row["MI_CD"].ToString() != "LPHESEJK01")
                {
                    if (row["MI_CD"].ToString() != "LPHEHKHL01")
                    {
                        dr = mst_item1.NewRow();
                        dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                        dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                        dr["MI_CD"] = row["MI_CD"].ToString();
                        dr["MI_BRAND"] = row["MI_BRAND"].ToString();
                        mst_item1.Rows.Add(dr);
                    }
                }
                if (index == 0)
                {

                    dr = mst_profit_center.NewRow();
                    dr["MPC_ADD_1"] = row["MPC_ADD_1"].ToString();
                    dr["MPC_ADD_2"] = row["MPC_ADD_2"].ToString();
                    dr["MPC_CHNL"] = row["MPC_CHNL"].ToString();
                    dr["MPC_DESC"] = row["MPC_DESC"].ToString();
                    dr["MPC_COM"] = row["MPC_COM"].ToString();
                    dr["MPC_CD"] = row["MPC_CD"].ToString();
                    dr["MPC_TEL"] = row["MPC_TEL"].ToString();
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


            DataTable deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
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

            foreach (DataRow row in deliveredSerials.Rows)
            {
                dr = int_hdr.NewRow();
                dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                dr["ITH_COM"] = row["ITH_COM"].ToString();
                dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                int_hdr.Rows.Add(dr);

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

            }

            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
            DataTable hpt_acc = new DataTable();



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


            invReportnew1.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            invReportnew1.Database.Tables["mst_com"].SetDataSource(mst_com);
            invReportnew1.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            // _TaxinvReport.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            //  _DealerinvReport.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            //    _DealerinvReport.Database.Tables["mst_item"].SetDataSource(mst_item);
            invReportnew1.Database.Tables["sec_user"].SetDataSource(sec_user);
            invReportnew1.Database.Tables["viw_sales_details"].SetDataSource(salesDetails);
            //   _TaxinvReport.Database.Tables["mst_item1"].SetDataSource(mst_item1);

            DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);
            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();
            DataTable sat_receiptGV = new DataTable();
            DataTable sat_receiptitmGV = new DataTable();

            sat_receipt.Columns.Add("SAR_SEQ_NO", typeof(string));
            sat_receipt.Columns.Add("SAR_COM_CD", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_TYPE", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_NO", typeof(string));


            sat_receiptitm.Columns.Add("SARD_SEQ_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_RECEIPT_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_INV_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_PAY_TP", typeof(string));
            sat_receiptitm.Columns.Add("SARD_REF_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_SETTLE_AMT", typeof(decimal));



            foreach (DataRow row in receiptDetails.Rows)
            {
                if (row["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                {

                    dr = sat_receipt.NewRow();
                    dr["SAR_SEQ_NO"] = row["SAR_SEQ_NO"].ToString();
                    dr["SAR_COM_CD"] = row["SAR_COM_CD"].ToString();
                    dr["SAR_RECEIPT_TYPE"] = row["SAR_RECEIPT_TYPE"].ToString();
                    dr["SAR_RECEIPT_NO"] = row["SAR_RECEIPT_NO"].ToString();
                    sat_receipt.Rows.Add(dr);



                    dr = sat_receiptitm.NewRow();
                    dr["SARD_SEQ_NO"] = row["SARD_SEQ_NO"].ToString();
                    dr["SARD_RECEIPT_NO"] = row["SARD_RECEIPT_NO"].ToString();
                    dr["SARD_INV_NO"] = row["SARD_INV_NO"].ToString();
                    dr["SARD_PAY_TP"] = row["SARD_PAY_TP"].ToString();
                    dr["SARD_REF_NO"] = row["SARD_REF_NO"].ToString();
                    dr["SARD_SETTLE_AMT"] = Convert.ToDecimal(row["SARD_SETTLE_AMT"].ToString());
                    sat_receiptitm.Rows.Add(dr);
                };
            };

            foreach (object repOp in invReportnew1.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "delLoc")
                    {
                        ReportDocument subRepDoc = invReportnew1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

                    }
                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = invReportnew1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }
                    if (_cs.SubreportName == "rptEngine")
                    {
                        ReportDocument subRepDoc = invReportnew1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                        subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);

                    }
                    if (_cs.SubreportName == "payments")
                    {
                        ReportDocument subRepDoc = invReportnew1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }

                }
            }



            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = invReportnew1;

            crystalReportViewer1.RefreshReport();




        }
        private void InvociePrintTax()
        {// Nadeeka 26-12-12
            string invNo = default(string);
            string cust_code = default(string);
            invNo = GlbReportDoc;

            string _pc = "";
            DataTable salesDetails = new DataTable();
            DataTable salesDetails1 = new DataTable();
            salesDetails.Clear();
            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            if (_pc != "BOC")
            {
                salesDetails1 = salesDetails.Select("SAD_TOT_AMT <> 0").CopyToDataTable();
            }
            else
            {
                salesDetails1 = salesDetails;
            }
            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable int_batch = new DataTable();
            DataTable int_batch1 = new DataTable();
            DataTable mst_customer = new DataTable();
            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();
            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _DealerinvReport.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            DataRow dr;
            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_NAME", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD1", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD2", typeof(string));
            mst_busentity.Columns.Add("mbe_svat_no", typeof(string));

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
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_NAME", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_SEQ_NO", typeof(decimal));
            sat_hdr1.Columns.Add("SAH_COM", typeof(string));
            sat_hdr1.Columns.Add("SAH_CRE_BY", typeof(string));
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
            sat_itm.Columns.Add("sad_warr_period", typeof(Int16));

            mst_profit_center.Columns.Add("MPC_ADD_1", typeof(string));
            mst_profit_center.Columns.Add("MPC_ADD_2", typeof(string));
            mst_profit_center.Columns.Add("MPC_CHNL", typeof(string));
            mst_profit_center.Columns.Add("MPC_DESC", typeof(string));
            mst_profit_center.Columns.Add("MPC_COM", typeof(string));
            mst_profit_center.Columns.Add("MPC_CD", typeof(string));



            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));
            mst_item.Columns.Add("MI_BRAND", typeof(string));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_BRAND", typeof(string));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));


            int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));

            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));


            foreach (DataRow row in salesDetails1.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails1.Rows.IndexOf(row);
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_NAME"] = row["MBE_NAME"].ToString();
                    dr["MBE_ADD1"] = row["MBE_ADD1"].ToString();
                    dr["MBE_ADD2"] = row["MBE_ADD2"].ToString();
                    dr["mbe_svat_no"] = row["mbe_svat_no"].ToString();

                    mst_busentity.Rows.Add(dr);

                    MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["SAH_DEL_LOC"].ToString());

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
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    dr["SAH_ANAL_4"] = row["SAH_ANAL_4"].ToString();
                    sat_hdr1.Rows.Add(dr);

                    _pc = row["SAH_PC"].ToString();
                    cust_code = row["SAH_STRUCTURE_SEQ"].ToString(); //Sanjeewa 2015-08-05 QUOTATION NO
                };

                if (_pc != "BOC")
                {
                    int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));


                    foreach (DataRow row1 in int_batch1.Rows)
                    {
                        dr = int_batch.NewRow();
                        dr["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                        dr["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                        dr["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                        dr["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                        dr["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                        int_batch.Rows.Add(dr);

                    }

                }

                //if (!(_pc == "BOC" && Convert.ToDecimal(row["SAD_TOT_AMT"].ToString())==0))                    
                //{

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
                dr["sad_warr_period"] = Convert.ToInt16(row["sad_warr_period"].ToString());

                sat_itm.Rows.Add(dr);



                dr = mst_item.NewRow();
                dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                dr["MI_CD"] = row["MI_CD"].ToString();
                dr["MI_BRAND"] = row["MI_BRAND"].ToString();

                mst_item.Rows.Add(dr);

                if (row["MI_CD"].ToString() != "LPHESEJK01")
                {
                    if (row["MI_CD"].ToString() != "LPHEHKHL01")
                    {
                        dr = mst_item1.NewRow();
                        dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                        dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                        dr["MI_CD"] = row["MI_CD"].ToString();
                        dr["MI_BRAND"] = row["MI_BRAND"].ToString();
                        mst_item1.Rows.Add(dr);
                    }
                }

                //}
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

            DataTable deliveredSerials = new DataTable();
            if (_pc != "BOC")
            {
                deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
            }
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

            if (_pc != "BOC")
            {
                foreach (DataRow row in deliveredSerials.Rows)
                {
                    dr = int_hdr.NewRow();
                    dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                    dr["ITH_COM"] = row["ITH_COM"].ToString();
                    dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                    dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                    int_hdr.Rows.Add(dr);

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

                }
            }
            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
            DataTable hpt_acc = new DataTable();



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


            _TaxinvReport.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _TaxinvReport.Database.Tables["mst_com"].SetDataSource(mst_com);
            _TaxinvReport.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            // _TaxinvReport.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            //  _DealerinvReport.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            //    _DealerinvReport.Database.Tables["mst_item"].SetDataSource(mst_item);
            _TaxinvReport.Database.Tables["sec_user"].SetDataSource(sec_user);
            _TaxinvReport.Database.Tables["viw_sales_details"].SetDataSource(salesDetails1);
            //   _TaxinvReport.Database.Tables["mst_item1"].SetDataSource(mst_item1);

            DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);
            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();
            DataTable sat_receiptGV = new DataTable();
            DataTable sat_receiptitmGV = new DataTable();

            sat_receipt.Columns.Add("SAR_SEQ_NO", typeof(string));
            sat_receipt.Columns.Add("SAR_COM_CD", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_TYPE", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_NO", typeof(string));


            sat_receiptitm.Columns.Add("SARD_SEQ_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_RECEIPT_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_INV_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_PAY_TP", typeof(string));
            sat_receiptitm.Columns.Add("SARD_REF_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_SETTLE_AMT", typeof(decimal));
            if (int_batch.Rows.Count > 0)
            {
                int_batch = int_batch.DefaultView.ToTable(true);
            }
            foreach (DataRow row in receiptDetails.Rows)
            {
                if (row["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                {

                    dr = sat_receipt.NewRow();
                    dr["SAR_SEQ_NO"] = row["SAR_SEQ_NO"].ToString();
                    dr["SAR_COM_CD"] = row["SAR_COM_CD"].ToString();
                    dr["SAR_RECEIPT_TYPE"] = row["SAR_RECEIPT_TYPE"].ToString();
                    dr["SAR_RECEIPT_NO"] = row["SAR_RECEIPT_NO"].ToString();
                    sat_receipt.Rows.Add(dr);



                    dr = sat_receiptitm.NewRow();
                    dr["SARD_SEQ_NO"] = row["SARD_SEQ_NO"].ToString();
                    dr["SARD_RECEIPT_NO"] = row["SARD_RECEIPT_NO"].ToString();
                    dr["SARD_INV_NO"] = row["SARD_INV_NO"].ToString();
                    dr["SARD_PAY_TP"] = row["SARD_PAY_TP"].ToString();
                    dr["SARD_REF_NO"] = row["SARD_REF_NO"].ToString();
                    dr["SARD_SETTLE_AMT"] = Convert.ToDecimal(row["SARD_SETTLE_AMT"].ToString());
                    sat_receiptitm.Rows.Add(dr);
                };
            };

            mst_customer = new DataTable();
            mst_customer = CHNLSVC.Sales.Get_CustomerDetails(cust_code); 
            
            foreach (object repOp in _TaxinvReport.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "delLoc")
                    {
                        ReportDocument subRepDoc = _TaxinvReport.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

                    }
                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = _TaxinvReport.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }

                    if (_pc != "BOC")
                    {
                        if (_cs.SubreportName == "rptEngine")
                        {
                            ReportDocument subRepDoc = _TaxinvReport.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                            subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);

                        }
                    };
                    if (_cs.SubreportName == "payments")
                    {
                        ReportDocument subRepDoc = _TaxinvReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "Del_Detail")
                    {
                        ReportDocument subRepDoc = _TaxinvReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_BUSENTITY_GRUP"].SetDataSource(mst_customer);
                }
            }
            }



            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = _TaxinvReport;

            crystalReportViewer1.RefreshReport();




        }

        private void InvociePrintAST()
        {// Sanjeewa 02-05-2016
            string invNo = default(string);
            string cust_code = default(string);
            invNo = GlbReportDoc;

            string _pc = "";
            DataTable salesDetails = new DataTable();
            DataTable salesDetails1 = new DataTable();
            salesDetails.Clear();
            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            if (_pc != "BOC")
            {
                salesDetails1 = salesDetails.Select("SAD_TOT_AMT <> 0").CopyToDataTable();
            }
            else
            {
                salesDetails1 = salesDetails;
            }
            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable int_batch = new DataTable();
            DataTable int_batch1 = new DataTable();
            DataTable mst_customer = new DataTable();
            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();
            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _invReportAST.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            DataRow dr;
            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_NAME", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD1", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD2", typeof(string));
            mst_busentity.Columns.Add("mbe_svat_no", typeof(string));

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
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_NAME", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_SEQ_NO", typeof(decimal));
            sat_hdr1.Columns.Add("SAH_COM", typeof(string));
            sat_hdr1.Columns.Add("SAH_CRE_BY", typeof(string));
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
            sat_itm.Columns.Add("sad_warr_period", typeof(Int16));

            mst_profit_center.Columns.Add("MPC_ADD_1", typeof(string));
            mst_profit_center.Columns.Add("MPC_ADD_2", typeof(string));
            mst_profit_center.Columns.Add("MPC_CHNL", typeof(string));
            mst_profit_center.Columns.Add("MPC_DESC", typeof(string));
            mst_profit_center.Columns.Add("MPC_COM", typeof(string));
            mst_profit_center.Columns.Add("MPC_CD", typeof(string));



            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));
            mst_item.Columns.Add("MI_BRAND", typeof(string));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_BRAND", typeof(string));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));


            int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));

            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));


            foreach (DataRow row in salesDetails1.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails1.Rows.IndexOf(row);
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_NAME"] = row["MBE_NAME"].ToString();
                    dr["MBE_ADD1"] = row["MBE_ADD1"].ToString();
                    dr["MBE_ADD2"] = row["MBE_ADD2"].ToString();
                    dr["mbe_svat_no"] = row["mbe_svat_no"].ToString();

                    mst_busentity.Rows.Add(dr);

                    MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["SAH_DEL_LOC"].ToString());

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
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    dr["SAH_ANAL_4"] = row["SAH_ANAL_4"].ToString();
                    sat_hdr1.Rows.Add(dr);

                    _pc = row["SAH_PC"].ToString();
                    cust_code = row["SAH_STRUCTURE_SEQ"].ToString(); //Sanjeewa 2015-08-05 QUOTATION NO
                };

                if (_pc != "BOC")
                {
                    int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));


                    foreach (DataRow row1 in int_batch1.Rows)
                    {
                        dr = int_batch.NewRow();
                        dr["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                        dr["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                        dr["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                        dr["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                        dr["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                        int_batch.Rows.Add(dr);

                    }

                }

                //if (!(_pc == "BOC" && Convert.ToDecimal(row["SAD_TOT_AMT"].ToString())==0))                    
                //{

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
                dr["sad_warr_period"] = Convert.ToInt16(row["sad_warr_period"].ToString());

                sat_itm.Rows.Add(dr);



                dr = mst_item.NewRow();
                dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                dr["MI_CD"] = row["MI_CD"].ToString();
                dr["MI_BRAND"] = row["MI_BRAND"].ToString();

                mst_item.Rows.Add(dr);

                if (row["MI_CD"].ToString() != "LPHESEJK01")
                {
                    if (row["MI_CD"].ToString() != "LPHEHKHL01")
                    {
                        dr = mst_item1.NewRow();
                        dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                        dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                        dr["MI_CD"] = row["MI_CD"].ToString();
                        dr["MI_BRAND"] = row["MI_BRAND"].ToString();
                        mst_item1.Rows.Add(dr);
                    }
                }

                //}
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

            DataTable deliveredSerials = new DataTable();
            if (_pc != "BOC")
            {
                deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
            }
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

            if (_pc != "BOC")
            {
                foreach (DataRow row in deliveredSerials.Rows)
                {
                    dr = int_hdr.NewRow();
                    dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                    dr["ITH_COM"] = row["ITH_COM"].ToString();
                    dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                    dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                    int_hdr.Rows.Add(dr);

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

                }
            }
            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
            DataTable hpt_acc = new DataTable();



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


            _invReportAST.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _invReportAST.Database.Tables["mst_com"].SetDataSource(mst_com);
            _invReportAST.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            // _invReportAST.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            //  _DealerinvReport.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            //    _DealerinvReport.Database.Tables["mst_item"].SetDataSource(mst_item);
            _invReportAST.Database.Tables["sec_user"].SetDataSource(sec_user);
            _invReportAST.Database.Tables["viw_sales_details"].SetDataSource(salesDetails1);
            //   _invReportAST.Database.Tables["mst_item1"].SetDataSource(mst_item1);

            DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);
            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();
            DataTable sat_receiptGV = new DataTable();
            DataTable sat_receiptitmGV = new DataTable();

            sat_receipt.Columns.Add("SAR_SEQ_NO", typeof(string));
            sat_receipt.Columns.Add("SAR_COM_CD", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_TYPE", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_NO", typeof(string));


            sat_receiptitm.Columns.Add("SARD_SEQ_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_RECEIPT_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_INV_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_PAY_TP", typeof(string));
            sat_receiptitm.Columns.Add("SARD_REF_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_SETTLE_AMT", typeof(decimal));
            if (int_batch.Rows.Count > 0)
            {
                int_batch = int_batch.DefaultView.ToTable(true);
            }
            foreach (DataRow row in receiptDetails.Rows)
            {
                if (row["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                {

                    dr = sat_receipt.NewRow();
                    dr["SAR_SEQ_NO"] = row["SAR_SEQ_NO"].ToString();
                    dr["SAR_COM_CD"] = row["SAR_COM_CD"].ToString();
                    dr["SAR_RECEIPT_TYPE"] = row["SAR_RECEIPT_TYPE"].ToString();
                    dr["SAR_RECEIPT_NO"] = row["SAR_RECEIPT_NO"].ToString();
                    sat_receipt.Rows.Add(dr);



                    dr = sat_receiptitm.NewRow();
                    dr["SARD_SEQ_NO"] = row["SARD_SEQ_NO"].ToString();
                    dr["SARD_RECEIPT_NO"] = row["SARD_RECEIPT_NO"].ToString();
                    dr["SARD_INV_NO"] = row["SARD_INV_NO"].ToString();
                    dr["SARD_PAY_TP"] = row["SARD_PAY_TP"].ToString();
                    dr["SARD_REF_NO"] = row["SARD_REF_NO"].ToString();
                    dr["SARD_SETTLE_AMT"] = Convert.ToDecimal(row["SARD_SETTLE_AMT"].ToString());
                    sat_receiptitm.Rows.Add(dr);
                };
            };

            mst_customer = new DataTable();
            mst_customer = CHNLSVC.Sales.Get_CustomerDetails(cust_code);

            foreach (object repOp in _invReportAST.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "delLoc")
                    {
                        ReportDocument subRepDoc = _invReportAST.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

                    }
                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = _invReportAST.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }

                    if (_pc != "BOC")
                    {
                        if (_cs.SubreportName == "rptEngine")
                        {
                            ReportDocument subRepDoc = _invReportAST.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                            subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);

                        }
                    };
                    if (_cs.SubreportName == "payments")
                    {
                        ReportDocument subRepDoc = _invReportAST.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "Del_Detail")
                    {
                        ReportDocument subRepDoc = _invReportAST.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_BUSENTITY_GRUP"].SetDataSource(mst_customer);
                    }
                }
            }



            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = _invReportAST;

            crystalReportViewer1.RefreshReport();




        }

        private void InvociePrintDealerAutoCred()
        {// Sanjeewa 13-10-2014
            string invNo = default(string);
            string cust_code = default(string);
            invNo = GlbReportDoc;

            string _pc = "";
            DataTable salesDetails = new DataTable();
            DataTable salesDetails1 = new DataTable();
            salesDetails.Clear();
            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            if (_pc != "BOC")
            {
                salesDetails1 = salesDetails.Select("SAD_TOT_AMT <> 0").CopyToDataTable();
            }
            else
            {
                salesDetails1 = salesDetails;
            }
            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable int_batch = new DataTable();
            DataTable int_batch1 = new DataTable();
            DataTable mst_customer = new DataTable();
            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();
            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _DealerinvReport.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            DataRow dr;
            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_NAME", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD1", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD2", typeof(string));
            mst_busentity.Columns.Add("mbe_svat_no", typeof(string));

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
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_NAME", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_SEQ_NO", typeof(decimal));
            sat_hdr1.Columns.Add("SAH_COM", typeof(string));
            sat_hdr1.Columns.Add("SAH_CRE_BY", typeof(string));
            sat_hdr1.Columns.Add("SAH_ANAL_4", typeof(string));
            sat_hdr1.Columns.Add("SAH_STRUCTURE_SEQ", typeof(string));

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
            sat_itm.Columns.Add("sad_warr_period", typeof(Int16));

            mst_profit_center.Columns.Add("MPC_ADD_1", typeof(string));
            mst_profit_center.Columns.Add("MPC_ADD_2", typeof(string));
            mst_profit_center.Columns.Add("MPC_CHNL", typeof(string));
            mst_profit_center.Columns.Add("MPC_DESC", typeof(string));
            mst_profit_center.Columns.Add("MPC_COM", typeof(string));
            mst_profit_center.Columns.Add("MPC_CD", typeof(string));



            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));
            mst_item.Columns.Add("MI_BRAND", typeof(string));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_BRAND", typeof(string));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));


            int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));

            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));


            foreach (DataRow row in salesDetails1.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails1.Rows.IndexOf(row);
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_NAME"] = row["MBE_NAME"].ToString();
                    dr["MBE_ADD1"] = row["MBE_ADD1"].ToString();
                    dr["MBE_ADD2"] = row["MBE_ADD2"].ToString();
                    dr["mbe_svat_no"] = row["mbe_svat_no"].ToString();

                    mst_busentity.Rows.Add(dr);

                    MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["SAH_DEL_LOC"].ToString());

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
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    dr["SAH_ANAL_4"] = row["SAH_ANAL_4"].ToString();
                    if (row["SAH_STRUCTURE_SEQ"].ToString() == "") { dr["SAH_STRUCTURE_SEQ"] = "N/A"; }
                    else { dr["SAH_STRUCTURE_SEQ"] = row["SAH_STRUCTURE_SEQ"].ToString(); }
                    sat_hdr1.Rows.Add(dr);

                    _pc = row["SAH_PC"].ToString();
                    cust_code = row["SAH_STRUCTURE_SEQ"].ToString(); //Sanjeewa 2015-08-05 QUOTATION NO
                };

                if (_pc != "BOC")
                {
                    int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));


                    foreach (DataRow row1 in int_batch1.Rows)
                    {
                        dr = int_batch.NewRow();
                        dr["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                        dr["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                        dr["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                        dr["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                        dr["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                        int_batch.Rows.Add(dr);

                    }

                }

                //if (!(_pc == "BOC" && Convert.ToDecimal(row["SAD_TOT_AMT"].ToString())==0))                    
                //{

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
                dr["sad_warr_period"] = Convert.ToInt16(row["sad_warr_period"].ToString());

                sat_itm.Rows.Add(dr);



                dr = mst_item.NewRow();
                dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                dr["MI_CD"] = row["MI_CD"].ToString();
                dr["MI_BRAND"] = row["MI_BRAND"].ToString();

                mst_item.Rows.Add(dr);

                if (row["MI_CD"].ToString() != "LPHESEJK01")
                {
                    if (row["MI_CD"].ToString() != "LPHEHKHL01")
                    {
                        dr = mst_item1.NewRow();
                        dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                        dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                        dr["MI_CD"] = row["MI_CD"].ToString();
                        dr["MI_BRAND"] = row["MI_BRAND"].ToString();
                        mst_item1.Rows.Add(dr);
                    }
                }

                //}
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

            DataTable deliveredSerials = new DataTable();
            if (_pc != "BOC")
            {
                deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
            }
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

            if (_pc != "BOC")
            {
                foreach (DataRow row in deliveredSerials.Rows)
                {
                    dr = int_hdr.NewRow();
                    dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                    dr["ITH_COM"] = row["ITH_COM"].ToString();
                    dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                    dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                    int_hdr.Rows.Add(dr);

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

                }
            }
            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
            DataTable hpt_acc = new DataTable();



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


            _DealerinvAutoCred.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _DealerinvAutoCred.Database.Tables["mst_com"].SetDataSource(mst_com);
            _DealerinvAutoCred.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            // _TaxinvReport.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            //  _DealerinvReport.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            //    _DealerinvReport.Database.Tables["mst_item"].SetDataSource(mst_item);
            _DealerinvAutoCred.Database.Tables["sec_user"].SetDataSource(sec_user);
            _DealerinvAutoCred.Database.Tables["viw_sales_details"].SetDataSource(salesDetails1);
            //   _TaxinvReport.Database.Tables["mst_item1"].SetDataSource(mst_item1);

            DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);
            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();
            DataTable sat_receiptGV = new DataTable();
            DataTable sat_receiptitmGV = new DataTable();

            sat_receipt.Columns.Add("SAR_SEQ_NO", typeof(string));
            sat_receipt.Columns.Add("SAR_COM_CD", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_TYPE", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_NO", typeof(string));


            sat_receiptitm.Columns.Add("SARD_SEQ_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_RECEIPT_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_INV_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_PAY_TP", typeof(string));
            sat_receiptitm.Columns.Add("SARD_REF_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_SETTLE_AMT", typeof(decimal));
            if (int_batch.Rows.Count > 0)
            {
                int_batch = int_batch.DefaultView.ToTable(true);
            }
            foreach (DataRow row in receiptDetails.Rows)
            {
                if (row["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                {

                    dr = sat_receipt.NewRow();
                    dr["SAR_SEQ_NO"] = row["SAR_SEQ_NO"].ToString();
                    dr["SAR_COM_CD"] = row["SAR_COM_CD"].ToString();
                    dr["SAR_RECEIPT_TYPE"] = row["SAR_RECEIPT_TYPE"].ToString();
                    dr["SAR_RECEIPT_NO"] = row["SAR_RECEIPT_NO"].ToString();
                    sat_receipt.Rows.Add(dr);



                    dr = sat_receiptitm.NewRow();
                    dr["SARD_SEQ_NO"] = row["SARD_SEQ_NO"].ToString();
                    dr["SARD_RECEIPT_NO"] = row["SARD_RECEIPT_NO"].ToString();
                    dr["SARD_INV_NO"] = row["SARD_INV_NO"].ToString();
                    dr["SARD_PAY_TP"] = row["SARD_PAY_TP"].ToString();
                    dr["SARD_REF_NO"] = row["SARD_REF_NO"].ToString();
                    dr["SARD_SETTLE_AMT"] = Convert.ToDecimal(row["SARD_SETTLE_AMT"].ToString());
                    sat_receiptitm.Rows.Add(dr);
                };
            };

            mst_customer = new DataTable();
            mst_customer = CHNLSVC.Sales.Get_CustomerDetails(cust_code); 

            foreach (object repOp in _DealerinvAutoCred.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "delLoc")
                    {
                        ReportDocument subRepDoc = _DealerinvAutoCred.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

                    }
                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = _DealerinvAutoCred.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }

                    if (_pc != "BOC")
                    {
                        if (_cs.SubreportName == "rptEngine")
                        {
                            ReportDocument subRepDoc = _DealerinvAutoCred.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                            subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);

                        }
                    };
                    if (_cs.SubreportName == "payments")
                    {
                        ReportDocument subRepDoc = _DealerinvAutoCred.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "Del_Detail")
                    {
                        ReportDocument subRepDoc = _DealerinvAutoCred.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_BUSENTITY_GRUP"].SetDataSource(mst_customer);
                    }

                }
            }



            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = _DealerinvAutoCred;

            crystalReportViewer1.RefreshReport();




        }
        private void InvociePrintDealerAutoCredAFSL()
        {// Sanjeewa 20-09-2016
            string invNo = default(string);
            string cust_code = default(string);
            invNo = GlbReportDoc;

            string _pc = "";
            
            DataTable salesDetails = new DataTable();
            DataTable salesDetails1 = new DataTable();
            DataTable creditDetails = new DataTable();
            salesDetails.Clear();
            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            if (_pc != "BOC")
            {
                salesDetails1 = salesDetails.Select("SAD_TOT_AMT <> 0").CopyToDataTable();
            }
            else
            {
                salesDetails1 = salesDetails;
            }
            creditDetails.Clear();
            creditDetails = CHNLSVC.Sales.GetInvCreditDetails(invNo);
            
            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable int_batch = new DataTable();
            DataTable int_batch1 = new DataTable();
            DataTable mst_customer = new DataTable();
            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();
            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _DealerinvReport.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            DataRow dr;
            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_NAME", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD1", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD2", typeof(string));
            mst_busentity.Columns.Add("mbe_svat_no", typeof(string));

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
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_NAME", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_SEQ_NO", typeof(decimal));
            sat_hdr1.Columns.Add("SAH_COM", typeof(string));
            sat_hdr1.Columns.Add("SAH_CRE_BY", typeof(string));
            sat_hdr1.Columns.Add("SAH_ANAL_4", typeof(string));
            sat_hdr1.Columns.Add("SAH_STRUCTURE_SEQ", typeof(string));            

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
            sat_itm.Columns.Add("sad_warr_period", typeof(Int16));

            mst_profit_center.Columns.Add("MPC_ADD_1", typeof(string));
            mst_profit_center.Columns.Add("MPC_ADD_2", typeof(string));
            mst_profit_center.Columns.Add("MPC_CHNL", typeof(string));
            mst_profit_center.Columns.Add("MPC_DESC", typeof(string));
            mst_profit_center.Columns.Add("MPC_COM", typeof(string));
            mst_profit_center.Columns.Add("MPC_CD", typeof(string));



            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));
            mst_item.Columns.Add("MI_BRAND", typeof(string));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_BRAND", typeof(string));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));


            int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));

            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));


            foreach (DataRow row in salesDetails1.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails1.Rows.IndexOf(row);
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_NAME"] = row["MBE_NAME"].ToString();
                    dr["MBE_ADD1"] = row["MBE_ADD1"].ToString();
                    dr["MBE_ADD2"] = row["MBE_ADD2"].ToString();
                    dr["mbe_svat_no"] = row["mbe_svat_no"].ToString();

                    mst_busentity.Rows.Add(dr);

                    MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["SAH_DEL_LOC"].ToString());

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
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    dr["SAH_ANAL_4"] = row["SAH_ANAL_4"].ToString();                    
                    if (row["SAH_STRUCTURE_SEQ"].ToString() == "") { dr["SAH_STRUCTURE_SEQ"] = "N/A"; }
                    else { dr["SAH_STRUCTURE_SEQ"] = row["SAH_STRUCTURE_SEQ"].ToString(); }
                    sat_hdr1.Rows.Add(dr);

                    _pc = row["SAH_PC"].ToString();
                    cust_code = row["SAH_STRUCTURE_SEQ"].ToString(); //Sanjeewa 2015-08-05 QUOTATION NO
                };

                if (_pc != "BOC")
                {
                    int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));


                    foreach (DataRow row1 in int_batch1.Rows)
                    {
                        dr = int_batch.NewRow();
                        dr["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                        dr["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                        dr["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                        dr["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                        dr["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                        int_batch.Rows.Add(dr);

                    }

                }

                //if (!(_pc == "BOC" && Convert.ToDecimal(row["SAD_TOT_AMT"].ToString())==0))                    
                //{

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
                dr["sad_warr_period"] = Convert.ToInt16(row["sad_warr_period"].ToString());

                sat_itm.Rows.Add(dr);



                dr = mst_item.NewRow();
                dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                dr["MI_CD"] = row["MI_CD"].ToString();
                dr["MI_BRAND"] = row["MI_BRAND"].ToString();

                mst_item.Rows.Add(dr);

                if (row["MI_CD"].ToString() != "LPHESEJK01")
                {
                    if (row["MI_CD"].ToString() != "LPHEHKHL01")
                    {
                        dr = mst_item1.NewRow();
                        dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                        dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                        dr["MI_CD"] = row["MI_CD"].ToString();
                        dr["MI_BRAND"] = row["MI_BRAND"].ToString();
                        mst_item1.Rows.Add(dr);
                    }
                }

                //}
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

            DataTable deliveredSerials = new DataTable();
            if (_pc != "BOC")
            {
                deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
            }
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

            if (_pc != "BOC")
            {
                foreach (DataRow row in deliveredSerials.Rows)
                {
                    dr = int_hdr.NewRow();
                    dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                    dr["ITH_COM"] = row["ITH_COM"].ToString();
                    dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                    dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                    int_hdr.Rows.Add(dr);

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

                }
            }
            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
            DataTable hpt_acc = new DataTable();

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


            _DealerinvAutoCredAFSL.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _DealerinvAutoCredAFSL.Database.Tables["mst_com"].SetDataSource(mst_com);
            _DealerinvAutoCredAFSL.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            // _TaxinvReport.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            //  _DealerinvReport.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            //    _DealerinvReport.Database.Tables["mst_item"].SetDataSource(mst_item);
            _DealerinvAutoCredAFSL.Database.Tables["sec_user"].SetDataSource(sec_user);
            _DealerinvAutoCredAFSL.Database.Tables["viw_sales_details"].SetDataSource(salesDetails1);
            _DealerinvAutoCredAFSL.Database.Tables["INV_CRED_DTL"].SetDataSource(creditDetails);
            //   _TaxinvReport.Database.Tables["mst_item1"].SetDataSource(mst_item1);

            DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);
            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();
            DataTable sat_receiptGV = new DataTable();
            DataTable sat_receiptitmGV = new DataTable();

            sat_receipt.Columns.Add("SAR_SEQ_NO", typeof(string));
            sat_receipt.Columns.Add("SAR_COM_CD", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_TYPE", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_NO", typeof(string));


            sat_receiptitm.Columns.Add("SARD_SEQ_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_RECEIPT_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_INV_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_PAY_TP", typeof(string));
            sat_receiptitm.Columns.Add("SARD_REF_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_SETTLE_AMT", typeof(decimal));
            if (int_batch.Rows.Count > 0)
            {
                int_batch = int_batch.DefaultView.ToTable(true);
            }
            foreach (DataRow row in receiptDetails.Rows)
            {
                if (row["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                {

                    dr = sat_receipt.NewRow();
                    dr["SAR_SEQ_NO"] = row["SAR_SEQ_NO"].ToString();
                    dr["SAR_COM_CD"] = row["SAR_COM_CD"].ToString();
                    dr["SAR_RECEIPT_TYPE"] = row["SAR_RECEIPT_TYPE"].ToString();
                    dr["SAR_RECEIPT_NO"] = row["SAR_RECEIPT_NO"].ToString();
                    sat_receipt.Rows.Add(dr);



                    dr = sat_receiptitm.NewRow();
                    dr["SARD_SEQ_NO"] = row["SARD_SEQ_NO"].ToString();
                    dr["SARD_RECEIPT_NO"] = row["SARD_RECEIPT_NO"].ToString();
                    dr["SARD_INV_NO"] = row["SARD_INV_NO"].ToString();
                    dr["SARD_PAY_TP"] = row["SARD_PAY_TP"].ToString();
                    dr["SARD_REF_NO"] = row["SARD_REF_NO"].ToString();
                    dr["SARD_SETTLE_AMT"] = Convert.ToDecimal(row["SARD_SETTLE_AMT"].ToString());
                    sat_receiptitm.Rows.Add(dr);
                };
            };

            mst_customer = new DataTable();
            mst_customer = CHNLSVC.Sales.Get_CustomerDetails(cust_code);

            foreach (object repOp in _DealerinvAutoCredAFSL.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "delLoc")
                    {
                        ReportDocument subRepDoc = _DealerinvAutoCredAFSL.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

                    }
                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = _DealerinvAutoCredAFSL.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }

                    if (_pc != "BOC")
                    {
                        if (_cs.SubreportName == "rptEngine")
                        {
                            ReportDocument subRepDoc = _DealerinvAutoCredAFSL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                            subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);

                        }
                    };
                    if (_cs.SubreportName == "payments")
                    {
                        ReportDocument subRepDoc = _DealerinvAutoCredAFSL.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "Del_Detail")
                    {
                        ReportDocument subRepDoc = _DealerinvAutoCredAFSL.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_BUSENTITY_GRUP"].SetDataSource(mst_customer);
                    }

                }
            }

            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = _DealerinvAutoCredAFSL;

            crystalReportViewer1.RefreshReport();            

        }

        private void InvociePrintTaxInsurance()
        {// Nadeeka 24-01-13
            string invNo = default(string);
            string cust_code = default(string);
            invNo = GlbReportDoc;



            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _DealerinvInsReport.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            string _pc = "";
            DataTable salesDetails = new DataTable();
            salesDetails.Clear();
            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable int_batch = new DataTable();
            DataTable int_batch1 = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_customer = new DataTable();
            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();


            DataRow dr;
            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_NAME", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD1", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD2", typeof(string));
            mst_busentity.Columns.Add("mbe_svat_no", typeof(string));

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
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_NAME", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_SEQ_NO", typeof(decimal));
            sat_hdr1.Columns.Add("SAH_COM", typeof(string));
            sat_hdr1.Columns.Add("SAH_CRE_BY", typeof(string));
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


            mst_profit_center.Columns.Add("MPC_ADD_1", typeof(string));
            mst_profit_center.Columns.Add("MPC_ADD_2", typeof(string));
            mst_profit_center.Columns.Add("MPC_CHNL", typeof(string));
            mst_profit_center.Columns.Add("MPC_DESC", typeof(string));
            mst_profit_center.Columns.Add("MPC_COM", typeof(string));
            mst_profit_center.Columns.Add("MPC_CD", typeof(string));



            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));
            mst_item.Columns.Add("MI_BRAND", typeof(string));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_BRAND", typeof(string));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));

            int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));


            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));


            foreach (DataRow row in salesDetails.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails.Rows.IndexOf(row);
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_NAME"] = row["MBE_NAME"].ToString();
                    dr["MBE_ADD1"] = row["MBE_ADD1"].ToString();
                    dr["MBE_ADD2"] = row["MBE_ADD2"].ToString();
                    dr["mbe_svat_no"] = row["mbe_svat_no"].ToString();
                    mst_busentity.Rows.Add(dr);

                    MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["SAH_DEL_LOC"].ToString());

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
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    dr["SAH_ANAL_4"] = row["SAH_ANAL_4"].ToString();
                    sat_hdr1.Rows.Add(dr);

                    _pc = row["SAH_PC"].ToString();
                    cust_code = row["SAH_STRUCTURE_SEQ"].ToString(); //Sanjeewa 2015-08-05 QUOTATION NO
                };

                // int_batch = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));

                int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));


                foreach (DataRow row1 in int_batch1.Rows)
                {
                    dr = int_batch.NewRow();
                    dr["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                    dr["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                    dr["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                    dr["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                    dr["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                    int_batch.Rows.Add(dr);

                }


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



                //dr = mst_item.NewRow();
                //dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                //dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                //dr["MI_CD"] = row["MI_CD"].ToString();
                //dr["MI_BRAND"] = row["MI_BRAND"].ToString();
                //mst_item.Rows.Add(dr);


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

            DataTable deliveredSerials = new DataTable();
            if (_pc != "BOC")
            {
                deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
            }
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

            foreach (DataRow row in deliveredSerials.Rows)
            {
                dr = int_hdr.NewRow();
                dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                dr["ITH_COM"] = row["ITH_COM"].ToString();
                dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                int_hdr.Rows.Add(dr);

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

            }

            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);

            DataTable hpt_acc = new DataTable();
            DataTable sat_itm_tax = CHNLSVC.Sales.GetSalesTax(invNo);


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


            _TaxinvInsReport.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _TaxinvInsReport.Database.Tables["mst_com"].SetDataSource(mst_com);
            _TaxinvInsReport.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            _TaxinvInsReport.Database.Tables["sec_user"].SetDataSource(sec_user);
            _TaxinvInsReport.Database.Tables["viw_sales_details"].SetDataSource(salesDetails);
            _TaxinvInsReport.Database.Tables["sat_itm_tax"].SetDataSource(sat_itm_tax);

            DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);
            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();
            DataTable sat_receiptGV = new DataTable();
            DataTable sat_receiptitmGV = new DataTable();

            sat_receipt.Columns.Add("SAR_SEQ_NO", typeof(string));
            sat_receipt.Columns.Add("SAR_COM_CD", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_TYPE", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_NO", typeof(string));


            sat_receiptitm.Columns.Add("SARD_SEQ_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_RECEIPT_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_INV_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_PAY_TP", typeof(string));
            sat_receiptitm.Columns.Add("SARD_REF_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_SETTLE_AMT", typeof(decimal));



            foreach (DataRow row in receiptDetails.Rows)
            {
                if (row["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                {

                    dr = sat_receipt.NewRow();
                    dr["SAR_SEQ_NO"] = row["SAR_SEQ_NO"].ToString();
                    dr["SAR_COM_CD"] = row["SAR_COM_CD"].ToString();
                    dr["SAR_RECEIPT_TYPE"] = row["SAR_RECEIPT_TYPE"].ToString();
                    dr["SAR_RECEIPT_NO"] = row["SAR_RECEIPT_NO"].ToString();
                    sat_receipt.Rows.Add(dr);



                    dr = sat_receiptitm.NewRow();
                    dr["SARD_SEQ_NO"] = row["SARD_SEQ_NO"].ToString();
                    dr["SARD_RECEIPT_NO"] = row["SARD_RECEIPT_NO"].ToString();
                    dr["SARD_INV_NO"] = row["SARD_INV_NO"].ToString();
                    dr["SARD_PAY_TP"] = row["SARD_PAY_TP"].ToString();
                    dr["SARD_REF_NO"] = row["SARD_REF_NO"].ToString();
                    dr["SARD_SETTLE_AMT"] = Convert.ToDecimal(row["SARD_SETTLE_AMT"].ToString());
                    sat_receiptitm.Rows.Add(dr);
                };
            };

            mst_customer = new DataTable();
            mst_customer = CHNLSVC.Sales.Get_CustomerDetails(cust_code); 
            
            if (int_batch.Rows.Count > 0)
            {
                int_batch = int_batch.DefaultView.ToTable(true);
            }
            foreach (object repOp in _TaxinvInsReport.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "delLoc")
                    {
                        ReportDocument subRepDoc = _TaxinvInsReport.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

                    }
                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = _TaxinvInsReport.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }
                    if (_cs.SubreportName == "rptEngine")
                    {
                        ReportDocument subRepDoc = _TaxinvInsReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                        subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);

                    }
                    if (_cs.SubreportName == "payments")
                    {
                        ReportDocument subRepDoc = _TaxinvInsReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "Del_Detail")
                    {
                        ReportDocument subRepDoc = _TaxinvInsReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_BUSENTITY_GRUP"].SetDataSource(mst_customer);
                    } 

                }
            }



            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = _TaxinvInsReport;

            crystalReportViewer1.RefreshReport();




        }
        private void InvoicePrintGold()
        {// Nadeeka 06-03-14
            string invNo = default(string);            
            invNo = GlbReportDoc;


            DataTable salesDetails = new DataTable();
            salesDetails.Clear();
            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable int_batch = new DataTable();
            DataTable int_batch1 = new DataTable();
            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();
            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _DealerinvReport.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            DataRow dr;
            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_NAME", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD1", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD2", typeof(string));


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


            mst_profit_center.Columns.Add("MPC_ADD_1", typeof(string));
            mst_profit_center.Columns.Add("MPC_ADD_2", typeof(string));
            mst_profit_center.Columns.Add("MPC_CHNL", typeof(string));
            mst_profit_center.Columns.Add("MPC_DESC", typeof(string));
            mst_profit_center.Columns.Add("MPC_COM", typeof(string));
            mst_profit_center.Columns.Add("MPC_CD", typeof(string));



            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));
            mst_item.Columns.Add("MI_BRAND", typeof(string));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_BRAND", typeof(string));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));


            int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));

            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));


            foreach (DataRow row in salesDetails.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails.Rows.IndexOf(row);
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_NAME"] = row["MBE_NAME"].ToString();
                    dr["MBE_ADD1"] = row["MBE_ADD1"].ToString();
                    dr["MBE_ADD2"] = row["MBE_ADD2"].ToString();

                    mst_busentity.Rows.Add(dr);

                    MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["SAH_DEL_LOC"].ToString());

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
                    dr["SAH_D_CUST_NAME"] = row["SAH_D_CUST_NAME"].ToString(); //Add by Chamal 23/05/2013
                    dr["SAH_D_CUST_CD"] = row["SAH_D_CUST_CD"].ToString();
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    dr["SAH_ANAL_4"] = row["SAH_ANAL_4"].ToString();
                    sat_hdr1.Rows.Add(dr);
                };

                int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));


                foreach (DataRow row1 in int_batch1.Rows)
                {
                    dr = int_batch.NewRow();
                    dr["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                    dr["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                    dr["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                    dr["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                    dr["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                    int_batch.Rows.Add(dr);

                }


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
                dr["MI_BRAND"] = row["MI_BRAND"].ToString();

                mst_item.Rows.Add(dr);

                //if (row["MI_CD"].ToString() != "LPHESEJK01")
                //{
                //    if (row["MI_CD"].ToString() != "LPHEHKHL01")
                //    {
                //        if (row["MI_BRAND"].ToString() != "TITAN")
                //        {
                //            dr = mst_item1.NewRow();
                //            dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                //            dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                //            dr["MI_CD"] = row["MI_CD"].ToString();
                //            dr["MI_BRAND"] = row["MI_BRAND"].ToString();
                //            mst_item1.Rows.Add(dr);
                //        }
                //    }
                //}
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


            DataTable deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
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

            foreach (DataRow row in deliveredSerials.Rows)
            {
                dr = int_hdr.NewRow();
                dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                dr["ITH_COM"] = row["ITH_COM"].ToString();
                dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                int_hdr.Rows.Add(dr);

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

            }

            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
            DataTable hpt_acc = new DataTable();



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


            DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);


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
                    sat_receiptitmCQ.Rows.Add(dr);



                };


                mst_item = mst_item.DefaultView.ToTable(true);
                _invGold.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                _invGold.Database.Tables["mst_com"].SetDataSource(mst_com);
                _invGold.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
                _invGold.Database.Tables["sec_user"].SetDataSource(sec_user);
                _invGold.Database.Tables["viw_sales_details"].SetDataSource(salesDetails);
                _invGold.Database.Tables["MST_ITM"].SetDataSource(mst_item);

                foreach (object repOp in _invGold.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "delLoc")
                        {
                            ReportDocument subRepDoc = _invGold.Subreports[_cs.SubreportName];

                            subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

                        }
                        if (_cs.SubreportName == "rptPower")
                        {
                            ReportDocument subRepDoc = _invGold.Subreports[_cs.SubreportName];

                            subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                        }
                        if (_cs.SubreportName == "rptEngine")
                        {
                            ReportDocument subRepDoc = _invGold.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                            subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);

                        }
                        if (_cs.SubreportName == "Rec")
                        {
                            ReportDocument subRepDoc = _invGold.Subreports[_cs.SubreportName];

                            subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receiptCQ);
                            subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitmCQ);

                        }
                    }
                }
            }



            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = _invGold;

            crystalReportViewer1.RefreshReport();




        }
        private void InvociePrintDealer()
        {// Nadeeka 26-12-12
            string invNo = default(string);
            string cust_code = default(string);
            invNo = GlbReportDoc;


            DataTable salesDetails = new DataTable();
            salesDetails.Clear();
            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable int_batch = new DataTable();
            DataTable int_batch1 = new DataTable();
            DataTable mst_customer = new DataTable();
            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();
            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _DealerinvReport.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            DataRow dr;
            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_NAME", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD1", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD2", typeof(string));


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


            mst_profit_center.Columns.Add("MPC_ADD_1", typeof(string));
            mst_profit_center.Columns.Add("MPC_ADD_2", typeof(string));
            mst_profit_center.Columns.Add("MPC_CHNL", typeof(string));
            mst_profit_center.Columns.Add("MPC_DESC", typeof(string));
            mst_profit_center.Columns.Add("MPC_COM", typeof(string));
            mst_profit_center.Columns.Add("MPC_CD", typeof(string));



            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));
            mst_item.Columns.Add("MI_BRAND", typeof(string));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_BRAND", typeof(string));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));


            int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));

            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));


            foreach (DataRow row in salesDetails.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails.Rows.IndexOf(row);
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_NAME"] = row["MBE_NAME"].ToString();
                    dr["MBE_ADD1"] = row["MBE_ADD1"].ToString();
                    dr["MBE_ADD2"] = row["MBE_ADD2"].ToString();

                    mst_busentity.Rows.Add(dr);

                    MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["SAH_DEL_LOC"].ToString());

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
                    dr["SAH_D_CUST_NAME"] = row["SAH_D_CUST_NAME"].ToString(); //Add by Chamal 23/05/2013
                    dr["SAH_D_CUST_CD"] = row["SAH_D_CUST_CD"].ToString();
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    dr["SAH_ANAL_4"] = row["SAH_ANAL_4"].ToString();
                    sat_hdr1.Rows.Add(dr);

                    cust_code = row["SAH_STRUCTURE_SEQ"].ToString(); //Sanjeewa 2015-08-05 QUOTATION NO
                };

                int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));


                foreach (DataRow row1 in int_batch1.Rows)
                {
                    dr = int_batch.NewRow();
                    dr["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                    dr["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                    dr["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                    dr["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                    dr["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                    int_batch.Rows.Add(dr);

                }


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
                dr["MI_BRAND"] = row["MI_BRAND"].ToString();

                mst_item.Rows.Add(dr);

                //if (row["MI_CD"].ToString() != "LPHESEJK01")
                //{
                //    if (row["MI_CD"].ToString() != "LPHEHKHL01")
                //    {
                //        if (row["MI_BRAND"].ToString() != "TITAN")
                //        {
                //            dr = mst_item1.NewRow();
                //            dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                //            dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                //            dr["MI_CD"] = row["MI_CD"].ToString();
                //            dr["MI_BRAND"] = row["MI_BRAND"].ToString();
                //            mst_item1.Rows.Add(dr);
                //        }
                //    }
                //}
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


            DataTable deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
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

            foreach (DataRow row in deliveredSerials.Rows)
            {
                dr = int_hdr.NewRow();
                dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                dr["ITH_COM"] = row["ITH_COM"].ToString();
                dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                int_hdr.Rows.Add(dr);

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

            }

            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
            DataTable hpt_acc = new DataTable();



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

            mst_customer = new DataTable();
            mst_customer = CHNLSVC.Sales.Get_CustomerDetails(cust_code); 
            
            mst_item = mst_item.DefaultView.ToTable(true);
            _DealerinvReport.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _DealerinvReport.Database.Tables["mst_com"].SetDataSource(mst_com);
            _DealerinvReport.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            // _DealerinvReport.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            //  _DealerinvReport.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            //    _DealerinvReport.Database.Tables["mst_item"].SetDataSource(mst_item);
            _DealerinvReport.Database.Tables["sec_user"].SetDataSource(sec_user);
            _DealerinvReport.Database.Tables["viw_sales_details"].SetDataSource(salesDetails);
            _DealerinvReport.Database.Tables["MST_ITM"].SetDataSource(mst_item);

            foreach (object repOp in _DealerinvReport.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "delLoc")
                    {
                        ReportDocument subRepDoc = _DealerinvReport.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

                    }
                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = _DealerinvReport.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }
                    if (_cs.SubreportName == "rptEngine")
                    {
                        ReportDocument subRepDoc = _DealerinvReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                        subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);

                    }
                    if (_cs.SubreportName == "Del_Detail")
                    {
                        ReportDocument subRepDoc = _DealerinvReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_BUSENTITY_GRUP"].SetDataSource(mst_customer);
                    }

                }
            }



            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = _DealerinvReport;

            crystalReportViewer1.RefreshReport();




        }
        private void InvociePrintDealerAuto()
        {// Nadeeka 03-02-14
            string invNo = default(string);

            invNo = BaseCls.GlbReportDoc;


            DataTable salesDetails = new DataTable();
            salesDetails.Clear();
            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable int_batch = new DataTable();
            DataTable int_batch1 = new DataTable();
            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();
            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _DealerinvReport.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            DataRow dr;
            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_NAME", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD1", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD2", typeof(string));


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


            mst_profit_center.Columns.Add("MPC_ADD_1", typeof(string));
            mst_profit_center.Columns.Add("MPC_ADD_2", typeof(string));
            mst_profit_center.Columns.Add("MPC_CHNL", typeof(string));
            mst_profit_center.Columns.Add("MPC_DESC", typeof(string));
            mst_profit_center.Columns.Add("MPC_COM", typeof(string));
            mst_profit_center.Columns.Add("MPC_CD", typeof(string));



            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));
            mst_item.Columns.Add("MI_BRAND", typeof(string));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_BRAND", typeof(string));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));
            mst_com.Columns.Add("mc_add1", typeof(string));
            mst_com.Columns.Add("mc_add2", typeof(string));
            mst_com.Columns.Add("mc_tel", typeof(string));
            mst_com.Columns.Add("mc_fax", typeof(string));
            mst_com.Columns.Add("mc_email", typeof(string));
            mst_com.Columns.Add("mc_web", typeof(string));
            mst_com.Columns.Add("mc_tax1", typeof(string));
            mst_com.Columns.Add("mc_tax2", typeof(string));
            mst_com.Columns.Add("mc_tax3", typeof(string));

            int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));

            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));


            foreach (DataRow row in salesDetails.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails.Rows.IndexOf(row);
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_NAME"] = row["MBE_NAME"].ToString();
                    dr["MBE_ADD1"] = row["MBE_ADD1"].ToString();
                    dr["MBE_ADD2"] = row["MBE_ADD2"].ToString();

                    mst_busentity.Rows.Add(dr);

                    MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["SAH_DEL_LOC"].ToString());

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
                    dr["SAH_D_CUST_NAME"] = row["SAH_D_CUST_NAME"].ToString(); //Add by Chamal 23/05/2013
                    dr["SAH_D_CUST_CD"] = row["SAH_D_CUST_CD"].ToString();
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    dr["SAH_ANAL_4"] = row["SAH_ANAL_4"].ToString();
                    sat_hdr1.Rows.Add(dr);
                };

                int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));


                foreach (DataRow row1 in int_batch1.Rows)
                {
                    dr = int_batch.NewRow();
                    dr["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                    dr["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                    dr["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                    dr["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                    dr["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                    int_batch.Rows.Add(dr);

                }


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
                dr["MI_BRAND"] = row["MI_BRAND"].ToString();

                mst_item.Rows.Add(dr);

                //if (row["MI_CD"].ToString() != "LPHESEJK01")
                //{
                //    if (row["MI_CD"].ToString() != "LPHEHKHL01")
                //    {
                //        if (row["MI_BRAND"].ToString() != "TITAN")
                //        {
                //            dr = mst_item1.NewRow();
                //            dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                //            dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                //            dr["MI_CD"] = row["MI_CD"].ToString();
                //            dr["MI_BRAND"] = row["MI_BRAND"].ToString();
                //            mst_item1.Rows.Add(dr);
                //        }
                //    }
                //}
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
                    dr["mc_add1"] = row["mc_add1"].ToString();
                    dr["mc_add2"] = row["mc_add2"].ToString();
                    dr["mc_tel"] = row["mc_tel"].ToString();
                    dr["mc_fax"] = row["mc_fax"].ToString();
                    dr["mc_email"] = row["mc_email"].ToString();
                    dr["mc_web"] = row["mc_web"].ToString();
                    dr["mc_tax1"] = row["mc_tax1"].ToString();
                    dr["mc_tax2"] = row["mc_tax2"].ToString();
                    dr["mc_tax3"] = row["mc_tax3"].ToString();

                    mst_com.Rows.Add(dr);

                    dr = sec_user.NewRow();
                    dr["SE_USR_NAME"] = row["SE_USR_NAME"].ToString();
                    dr["SE_USR_ID"] = row["SE_USR_ID"].ToString();
                    sec_user.Rows.Add(dr);
                };

            }


            DataTable deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
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

            foreach (DataRow row in deliveredSerials.Rows)
            {
                dr = int_hdr.NewRow();
                dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                dr["ITH_COM"] = row["ITH_COM"].ToString();
                dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                int_hdr.Rows.Add(dr);

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

            }

            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
            DataTable hpt_acc = new DataTable();



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

            mst_item = mst_item.DefaultView.ToTable(true);
            _DealerinvAuto.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _DealerinvAuto.Database.Tables["mst_com"].SetDataSource(mst_com);
            _DealerinvAuto.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            _DealerinvAuto.Database.Tables["sec_user"].SetDataSource(sec_user);
            _DealerinvAuto.Database.Tables["viw_sales_details"].SetDataSource(salesDetails);
            _DealerinvAuto.Database.Tables["MST_ITM"].SetDataSource(mst_item);

            foreach (object repOp in _DealerinvAuto.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "delLoc")
                    {
                        ReportDocument subRepDoc = _DealerinvAuto.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

                    }
                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = _DealerinvAuto.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }
                    if (_cs.SubreportName == "rptEngine")
                    {
                        ReportDocument subRepDoc = _DealerinvAuto.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                        subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);

                    }

                }
            }



            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = _DealerinvAuto;

            crystalReportViewer1.RefreshReport();




        }


        private void InvociePrintDutyFreeInv()
        {// Nadeeka 04-03-14
            string invNo = default(string);
            invNo = GlbReportDoc;


            DataTable salesDetails = new DataTable();
            salesDetails.Clear();
            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable int_batch = new DataTable();
            DataTable int_batch1 = new DataTable();

            DataTable SAT_DF_CUS_DET = CHNLSVC.Sales.GetDFCusPassport(GlbReportDoc);
            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();
            DataTable sat_vou_det = new DataTable();
            sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(invNo);


            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();
            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _DealerinvReport.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            DataRow dr;

            sat_receipt.Columns.Add("SAR_SEQ_NO", typeof(string));
            sat_receipt.Columns.Add("SAR_COM_CD", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_TYPE", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_NO", typeof(string));


            sat_receiptitm.Columns.Add("SARD_SEQ_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_RECEIPT_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_INV_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_PAY_TP", typeof(string));
            sat_receiptitm.Columns.Add("SARD_REF_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_SETTLE_AMT", typeof(decimal));
            sat_receiptitm.Columns.Add("sard_anal_1", typeof(string));
            sat_receiptitm.Columns.Add("sard_anal_4", typeof(decimal));

            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_NAME", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD1", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD2", typeof(string));
            mst_busentity.Columns.Add("MBE_COUNTRY_CD", typeof(string));
            mst_busentity.Columns.Add("mbe_nationality", typeof(string));

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
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_NAME", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_SEQ_NO", typeof(decimal));
            sat_hdr1.Columns.Add("SAH_COM", typeof(string));
            sat_hdr1.Columns.Add("SAH_CRE_BY", typeof(string));
            sat_hdr1.Columns.Add("SAH_CURRENCY", typeof(string));
            sat_hdr1.Columns.Add("esep_name_initials", typeof(string));


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
            mst_profit_center.Columns.Add("MPC_TEL", typeof(string));
            mst_profit_center.Columns.Add("MPC_FAX", typeof(string));
            mst_profit_center.Columns.Add("MPC_EMAIL", typeof(string));


            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));
            mst_item.Columns.Add("MI_BRAND", typeof(string));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_BRAND", typeof(string));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));
            //mst_com.Columns.Add("MC_ANAL18", typeof(string));
            //mst_com.Columns.Add("MC_TAX3", typeof(string));

            int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));

            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));


            foreach (DataRow row in salesDetails.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails.Rows.IndexOf(row);
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_NAME"] = row["MBE_NAME"].ToString();
                    dr["MBE_ADD1"] = row["MBE_ADD1"].ToString();
                    dr["MBE_ADD2"] = row["MBE_ADD2"].ToString();
                    dr["MBE_COUNTRY_CD"] = row["MBE_COUNTRY_CD"].ToString();
                    dr["mbe_nationality"] = row["mbe_nationality"].ToString();

                    mst_busentity.Rows.Add(dr);

                    MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["SAH_DEL_LOC"].ToString());

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
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    dr["SAH_CURRENCY"] = row["SAH_CURRENCY"].ToString();
                    dr["esep_name_initials"] = row["esep_name_initials"].ToString();

                    sat_hdr1.Rows.Add(dr);
                };

                int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));


                foreach (DataRow row1 in int_batch1.Rows)
                {
                    dr = int_batch.NewRow();
                    dr["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                    dr["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                    dr["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                    dr["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                    dr["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                    int_batch.Rows.Add(dr);

                }


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
                dr["MI_BRAND"] = row["MI_BRAND"].ToString();

                mst_item.Rows.Add(dr);





                DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(GlbReportDoc);








                foreach (DataRow row1 in receiptDetails.Rows)
                {
                    if (row1["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                    {

                        dr = sat_receipt.NewRow();
                        dr["SAR_SEQ_NO"] = row1["SAR_SEQ_NO"].ToString();
                        dr["SAR_COM_CD"] = row1["SAR_COM_CD"].ToString();
                        dr["SAR_RECEIPT_TYPE"] = row1["SAR_RECEIPT_TYPE"].ToString();
                        dr["SAR_RECEIPT_NO"] = row1["SAR_RECEIPT_NO"].ToString();
                        sat_receipt.Rows.Add(dr);



                        dr = sat_receiptitm.NewRow();
                        dr["SARD_SEQ_NO"] = row1["SARD_SEQ_NO"].ToString();
                        dr["SARD_RECEIPT_NO"] = row1["SARD_RECEIPT_NO"].ToString();
                        dr["SARD_INV_NO"] = row1["SARD_INV_NO"].ToString();
                        dr["SARD_PAY_TP"] = row1["SARD_PAY_TP"].ToString();
                        dr["SARD_REF_NO"] = row1["SARD_REF_NO"].ToString();
                        dr["SARD_SETTLE_AMT"] = Convert.ToDecimal(row1["SARD_SETTLE_AMT"].ToString());
                        dr["sard_anal_1"] = row1["sard_anal_1"].ToString();
                        dr["sard_anal_4"] = Convert.ToDecimal(row1["sard_anal_4"].ToString());
                        sat_receiptitm.Rows.Add(dr);
                    };
                };


                if (index == 0)
                {

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
                    mst_profit_center.Rows.Add(dr);


                    dr = mst_com.NewRow();
                    dr["MC_TAX1"] = row["MC_TAX1"].ToString();
                    dr["MC_TAX2"] = row["MC_TAX2"].ToString();
                    dr["MC_CD"] = row["MC_CD"].ToString();
                    dr["MC_DESC"] = row["MC_DESC"].ToString();
                    dr["MC_IT_POWERED"] = row["MC_IT_POWERED"].ToString();
                    //dr["MC_ANAL18"] = row["MC_ANAL18"].ToString();
                    //dr["MC_TAX3"] = row["MC_TAX3"].ToString();
                    mst_com.Rows.Add(dr);

                    dr = sec_user.NewRow();
                    dr["SE_USR_NAME"] = row["SE_USR_NAME"].ToString();
                    dr["SE_USR_ID"] = row["SE_USR_ID"].ToString();
                    sec_user.Rows.Add(dr);
                };

            }


            DataTable deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(GlbReportDoc);
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

            foreach (DataRow row in deliveredSerials.Rows)
            {
                dr = int_hdr.NewRow();
                dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                dr["ITH_COM"] = row["ITH_COM"].ToString();
                dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                int_hdr.Rows.Add(dr);

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

            }

            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
            DataTable hpt_acc = new DataTable();



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

            mst_item = mst_item.DefaultView.ToTable(true);

            _sDfInsReport.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _sDfInsReport.Database.Tables["mst_com"].SetDataSource(mst_com);
            _sDfInsReport.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);

            _sDfInsReport.Database.Tables["sec_user"].SetDataSource(sec_user);
            _sDfInsReport.Database.Tables["viw_sales_details"].SetDataSource(salesDetails);
            _sDfInsReport.Database.Tables["MST_ITM"].SetDataSource(mst_item);
            sat_receipt = sat_receipt.DefaultView.ToTable(true);
            sat_receiptitm = sat_receiptitm.DefaultView.ToTable(true);
            foreach (object repOp in _sDfInsReport.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;

                    if (_cs.SubreportName == "payments")
                    {
                        ReportDocument subRepDoc = _sDfInsReport.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "customer")
                    {
                        ReportDocument subRepDoc = _sDfInsReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SAT_DF_CUS_DET"].SetDataSource(SAT_DF_CUS_DET);
                    }
                    if (_cs.SubreportName == "giftVou")
                    {
                        ReportDocument subRepDoc = _sDfInsReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                    }

                }
            }



            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = _sDfInsReport;

            crystalReportViewer1.RefreshReport();




        }
        private void InvociePrintDutyFree()
        {// Nadeeka 26-12-12
            string invNo = default(string);
            invNo = GlbReportDoc;

            DataTable salesDetails = new DataTable();
            salesDetails.Clear();
            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable int_batch = new DataTable();
            DataTable int_batch1 = new DataTable();

            DataTable SAT_DF_CUS_DET = CHNLSVC.Sales.GetDFCusPassport(GlbReportDoc);
            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();

            DataTable sat_vou_det = new DataTable();
            sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(invNo);

            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();
            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _DealerinvReport.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            DataRow dr;

            sat_receipt.Columns.Add("SAR_SEQ_NO", typeof(string));
            sat_receipt.Columns.Add("SAR_COM_CD", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_TYPE", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_NO", typeof(string));

            sat_receiptitm.Columns.Add("SARD_SEQ_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_RECEIPT_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_INV_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_PAY_TP", typeof(string));
            sat_receiptitm.Columns.Add("SARD_REF_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_SETTLE_AMT", typeof(decimal));
            sat_receiptitm.Columns.Add("sard_anal_1", typeof(string));
            sat_receiptitm.Columns.Add("sard_anal_4", typeof(decimal));

            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_NAME", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD1", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD2", typeof(string));
            mst_busentity.Columns.Add("MBE_COUNTRY_CD", typeof(string));
            mst_busentity.Columns.Add("mbe_nationality", typeof(string));

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
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_NAME", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_SEQ_NO", typeof(decimal));
            sat_hdr1.Columns.Add("SAH_COM", typeof(string));
            sat_hdr1.Columns.Add("SAH_CRE_BY", typeof(string));
            sat_hdr1.Columns.Add("SAH_CURRENCY", typeof(string));
            sat_hdr1.Columns.Add("esep_name_initials", typeof(string));

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
            mst_profit_center.Columns.Add("MPC_TEL", typeof(string));
            mst_profit_center.Columns.Add("MPC_FAX", typeof(string));
            mst_profit_center.Columns.Add("MPC_EMAIL", typeof(string));

            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));
            mst_item.Columns.Add("MI_BRAND", typeof(string));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_BRAND", typeof(string));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));
            //mst_com.Columns.Add("MC_ANAL18", typeof(string));
            //mst_com.Columns.Add("MC_TAX3", typeof(string));

            int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));

            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));

            foreach (DataRow row in salesDetails.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails.Rows.IndexOf(row);
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_NAME"] = row["MBE_NAME"].ToString();
                    dr["MBE_ADD1"] = row["MBE_ADD1"].ToString();
                    dr["MBE_ADD2"] = row["MBE_ADD2"].ToString();
                    dr["MBE_COUNTRY_CD"] = row["MBE_COUNTRY_CD"].ToString();
                    dr["mbe_nationality"] = row["mbe_nationality"].ToString();

                    mst_busentity.Rows.Add(dr);

                    MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["SAH_DEL_LOC"].ToString());

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
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    dr["SAH_CURRENCY"] = row["SAH_CURRENCY"].ToString();
                    dr["esep_name_initials"] = row["esep_name_initials"].ToString();

                    sat_hdr1.Rows.Add(dr);
                };

                int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));

                foreach (DataRow row1 in int_batch1.Rows)
                {
                    dr = int_batch.NewRow();
                    dr["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                    dr["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                    dr["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                    dr["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                    dr["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                    int_batch.Rows.Add(dr);
                }

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
                dr["MI_BRAND"] = row["MI_BRAND"].ToString();

                mst_item.Rows.Add(dr);

                DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(GlbReportDoc);

                foreach (DataRow row1 in receiptDetails.Rows)
                {
                    if (row1["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                    {

                        dr = sat_receipt.NewRow();
                        dr["SAR_SEQ_NO"] = row1["SAR_SEQ_NO"].ToString();
                        dr["SAR_COM_CD"] = row1["SAR_COM_CD"].ToString();
                        dr["SAR_RECEIPT_TYPE"] = row1["SAR_RECEIPT_TYPE"].ToString();
                        dr["SAR_RECEIPT_NO"] = row1["SAR_RECEIPT_NO"].ToString();
                        sat_receipt.Rows.Add(dr);



                        dr = sat_receiptitm.NewRow();
                        dr["SARD_SEQ_NO"] = row1["SARD_SEQ_NO"].ToString();
                        dr["SARD_RECEIPT_NO"] = row1["SARD_RECEIPT_NO"].ToString();
                        dr["SARD_INV_NO"] = row1["SARD_INV_NO"].ToString();
                        dr["SARD_PAY_TP"] = row1["SARD_PAY_TP"].ToString();
                        dr["SARD_REF_NO"] = row1["SARD_REF_NO"].ToString();
                        dr["SARD_SETTLE_AMT"] = Convert.ToDecimal(row1["SARD_SETTLE_AMT"].ToString());
                        dr["sard_anal_1"] = row1["sard_anal_1"].ToString();
                        dr["sard_anal_4"] = Convert.ToDecimal(row1["sard_anal_4"].ToString());
                        sat_receiptitm.Rows.Add(dr);
                    };
                };


                if (index == 0)
                {

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
                    mst_profit_center.Rows.Add(dr);


                    dr = mst_com.NewRow();
                    dr["MC_TAX1"] = row["MC_TAX1"].ToString();
                    dr["MC_TAX2"] = row["MC_TAX2"].ToString();
                    dr["MC_CD"] = row["MC_CD"].ToString();
                    dr["MC_DESC"] = row["MC_DESC"].ToString();
                    dr["MC_IT_POWERED"] = row["MC_IT_POWERED"].ToString();
                    //dr["MC_ANAL18"] = row["MC_ANAL18"].ToString();
                    //dr["MC_TAX3"] = row["MC_TAX3"].ToString();
                    mst_com.Rows.Add(dr);

                    dr = sec_user.NewRow();
                    dr["SE_USR_NAME"] = row["SE_USR_NAME"].ToString();
                    dr["SE_USR_ID"] = row["SE_USR_ID"].ToString();
                    sec_user.Rows.Add(dr);
                };

            }


            DataTable deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(GlbReportDoc);
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

            foreach (DataRow row in deliveredSerials.Rows)
            {
                dr = int_hdr.NewRow();
                dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                dr["ITH_COM"] = row["ITH_COM"].ToString();
                dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                int_hdr.Rows.Add(dr);

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

            }

            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
            DataTable hpt_acc = new DataTable();



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

            mst_item = mst_item.DefaultView.ToTable(true);

            _DfInsReport.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _DfInsReport.Database.Tables["mst_com"].SetDataSource(mst_com);
            _DfInsReport.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);

            _DfInsReport.Database.Tables["sec_user"].SetDataSource(sec_user);
            _DfInsReport.Database.Tables["viw_sales_details"].SetDataSource(salesDetails);
            _DfInsReport.Database.Tables["MST_ITM"].SetDataSource(mst_item);
            sat_receipt = sat_receipt.DefaultView.ToTable(true);
            sat_receiptitm = sat_receiptitm.DefaultView.ToTable(true);
            foreach (object repOp in _DfInsReport.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;

                    if (_cs.SubreportName == "payments")
                    {
                        ReportDocument subRepDoc = _DfInsReport.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "customer")
                    {
                        ReportDocument subRepDoc = _DfInsReport.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["SAT_DF_CUS_DET"].SetDataSource(SAT_DF_CUS_DET);
                    }

                    if (_cs.SubreportName == "giftVou")
                    {
                        ReportDocument subRepDoc = _DfInsReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                    }

                }
            }



            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = _DfInsReport;

            crystalReportViewer1.RefreshReport();




        }

        private void InvociePrintDutyFreeCLC()
        {// Sanjeewa 23-03-2015
            string invNo = default(string);
            invNo = GlbReportDoc;

            DataTable salesDetails = new DataTable();
            salesDetails.Clear();
            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable int_batch = new DataTable();
            DataTable int_batch1 = new DataTable();

            DataTable SAT_DF_CUS_DET = CHNLSVC.Sales.GetDFCusPassport(GlbReportDoc);
            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();

            DataTable sat_vou_det = new DataTable();
            sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(invNo);


            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();
            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _DealerinvReport.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            DataRow dr;

            sat_receipt.Columns.Add("SAR_SEQ_NO", typeof(string));
            sat_receipt.Columns.Add("SAR_COM_CD", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_TYPE", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_NO", typeof(string));

            sat_receiptitm.Columns.Add("SARD_SEQ_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_RECEIPT_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_INV_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_PAY_TP", typeof(string));
            sat_receiptitm.Columns.Add("SARD_REF_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_SETTLE_AMT", typeof(decimal));
            sat_receiptitm.Columns.Add("sard_anal_1", typeof(string));
            sat_receiptitm.Columns.Add("sard_anal_4", typeof(decimal));

            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_NAME", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD1", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD2", typeof(string));
            mst_busentity.Columns.Add("MBE_COUNTRY_CD", typeof(string));
            mst_busentity.Columns.Add("mbe_nationality", typeof(string));

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
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_NAME", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_SEQ_NO", typeof(decimal));
            sat_hdr1.Columns.Add("SAH_COM", typeof(string));
            sat_hdr1.Columns.Add("SAH_CRE_BY", typeof(string));
            sat_hdr1.Columns.Add("SAH_CURRENCY", typeof(string));
            sat_hdr1.Columns.Add("esep_name_initials", typeof(string));

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
            mst_profit_center.Columns.Add("MPC_TEL", typeof(string));
            mst_profit_center.Columns.Add("MPC_FAX", typeof(string));
            mst_profit_center.Columns.Add("MPC_EMAIL", typeof(string));

            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));
            mst_item.Columns.Add("MI_BRAND", typeof(string));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_BRAND", typeof(string));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));
            //mst_com.Columns.Add("MC_ANAL18", typeof(string));
            //mst_com.Columns.Add("MC_TAX3", typeof(string));

            int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));

            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));

            foreach (DataRow row in salesDetails.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails.Rows.IndexOf(row);
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_NAME"] = row["MBE_NAME"].ToString();
                    dr["MBE_ADD1"] = row["MBE_ADD1"].ToString();
                    dr["MBE_ADD2"] = row["MBE_ADD2"].ToString();
                    dr["MBE_COUNTRY_CD"] = row["MBE_COUNTRY_CD"].ToString();
                    dr["mbe_nationality"] = row["mbe_nationality"].ToString();

                    mst_busentity.Rows.Add(dr);

                    MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["SAH_DEL_LOC"].ToString());

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
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    dr["SAH_CURRENCY"] = row["SAH_CURRENCY"].ToString();
                    dr["esep_name_initials"] = row["esep_name_initials"].ToString();

                    sat_hdr1.Rows.Add(dr);
                };

                int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));

                foreach (DataRow row1 in int_batch1.Rows)
                {
                    dr = int_batch.NewRow();
                    dr["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                    dr["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                    dr["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                    dr["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                    dr["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                    int_batch.Rows.Add(dr);
                }

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
                dr["MI_BRAND"] = row["MI_BRAND"].ToString();

                mst_item.Rows.Add(dr);

                DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(GlbReportDoc);

                foreach (DataRow row1 in receiptDetails.Rows)
                {
                    if (row1["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                    {

                        dr = sat_receipt.NewRow();
                        dr["SAR_SEQ_NO"] = row1["SAR_SEQ_NO"].ToString();
                        dr["SAR_COM_CD"] = row1["SAR_COM_CD"].ToString();
                        dr["SAR_RECEIPT_TYPE"] = row1["SAR_RECEIPT_TYPE"].ToString();
                        dr["SAR_RECEIPT_NO"] = row1["SAR_RECEIPT_NO"].ToString();
                        sat_receipt.Rows.Add(dr);



                        dr = sat_receiptitm.NewRow();
                        dr["SARD_SEQ_NO"] = row1["SARD_SEQ_NO"].ToString();
                        dr["SARD_RECEIPT_NO"] = row1["SARD_RECEIPT_NO"].ToString();
                        dr["SARD_INV_NO"] = row1["SARD_INV_NO"].ToString();
                        dr["SARD_PAY_TP"] = row1["SARD_PAY_TP"].ToString();
                        dr["SARD_REF_NO"] = row1["SARD_REF_NO"].ToString();
                        dr["SARD_SETTLE_AMT"] = Convert.ToDecimal(row1["SARD_SETTLE_AMT"].ToString());
                        dr["sard_anal_1"] = row1["sard_anal_1"].ToString();
                        dr["sard_anal_4"] = Convert.ToDecimal(row1["sard_anal_4"].ToString());
                        sat_receiptitm.Rows.Add(dr);
                    };
                };


                if (index == 0)
                {

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
                    mst_profit_center.Rows.Add(dr);


                    dr = mst_com.NewRow();
                    dr["MC_TAX1"] = row["MC_TAX1"].ToString();
                    dr["MC_TAX2"] = row["MC_TAX2"].ToString();
                    dr["MC_CD"] = row["MC_CD"].ToString();
                    dr["MC_DESC"] = row["MC_DESC"].ToString();
                    dr["MC_IT_POWERED"] = row["MC_IT_POWERED"].ToString();
                    //dr["MC_ANAL18"] = row["MC_ANAL18"].ToString();
                    //dr["MC_TAX3"] = row["MC_TAX3"].ToString();
                    mst_com.Rows.Add(dr);

                    dr = sec_user.NewRow();
                    dr["SE_USR_NAME"] = row["SE_USR_NAME"].ToString();
                    dr["SE_USR_ID"] = row["SE_USR_ID"].ToString();
                    sec_user.Rows.Add(dr);
                };

            }


            DataTable deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(GlbReportDoc);
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

            foreach (DataRow row in deliveredSerials.Rows)
            {
                dr = int_hdr.NewRow();
                dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                dr["ITH_COM"] = row["ITH_COM"].ToString();
                dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                int_hdr.Rows.Add(dr);

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

            }

            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
            DataTable hpt_acc = new DataTable();



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

            mst_item = mst_item.DefaultView.ToTable(true);

            _DfInvCLC.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _DfInvCLC.Database.Tables["mst_com"].SetDataSource(mst_com);
            _DfInvCLC.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);

            _DfInvCLC.Database.Tables["sec_user"].SetDataSource(sec_user);
            _DfInvCLC.Database.Tables["viw_sales_details"].SetDataSource(salesDetails);
            _DfInvCLC.Database.Tables["MST_ITM"].SetDataSource(mst_item);
            sat_receipt = sat_receipt.DefaultView.ToTable(true);
            sat_receiptitm = sat_receiptitm.DefaultView.ToTable(true);
            foreach (object repOp in _DfInvCLC.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;

                    if (_cs.SubreportName == "payments")
                    {
                        ReportDocument subRepDoc = _DfInvCLC.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "customer")
                    {
                        ReportDocument subRepDoc = _DfInvCLC.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["SAT_DF_CUS_DET"].SetDataSource(SAT_DF_CUS_DET);


                    }
                }
            }



            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = _DfInvCLC;

            crystalReportViewer1.RefreshReport();




        }

        private void InvociePrintDutyFreePP()
        {// Sanjeewa 23-03-2015
            string invNo = default(string);
            invNo = GlbReportDoc;

            DataTable salesDetails = new DataTable();
            salesDetails.Clear();
            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable int_batch = new DataTable();
            DataTable int_batch1 = new DataTable();

            DataTable SAT_DF_CUS_DET = CHNLSVC.Sales.GetDFCusPassport(GlbReportDoc);
            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();
            DataTable sat_vou_det = new DataTable();
            sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(invNo);

            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();
            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _DealerinvReport.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            DataRow dr;

            sat_receipt.Columns.Add("SAR_SEQ_NO", typeof(string));
            sat_receipt.Columns.Add("SAR_COM_CD", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_TYPE", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_NO", typeof(string));

            sat_receiptitm.Columns.Add("SARD_SEQ_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_RECEIPT_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_INV_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_PAY_TP", typeof(string));
            sat_receiptitm.Columns.Add("SARD_REF_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_SETTLE_AMT", typeof(decimal));
            sat_receiptitm.Columns.Add("sard_anal_1", typeof(string));
            sat_receiptitm.Columns.Add("sard_anal_4", typeof(decimal));

            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_NAME", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD1", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD2", typeof(string));
            mst_busentity.Columns.Add("MBE_COUNTRY_CD", typeof(string));
            mst_busentity.Columns.Add("mbe_nationality", typeof(string));

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
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_NAME", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_SEQ_NO", typeof(decimal));
            sat_hdr1.Columns.Add("SAH_COM", typeof(string));
            sat_hdr1.Columns.Add("SAH_CRE_BY", typeof(string));
            sat_hdr1.Columns.Add("SAH_CURRENCY", typeof(string));
            sat_hdr1.Columns.Add("esep_name_initials", typeof(string));

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
            mst_profit_center.Columns.Add("MPC_TEL", typeof(string));
            mst_profit_center.Columns.Add("MPC_FAX", typeof(string));
            mst_profit_center.Columns.Add("MPC_EMAIL", typeof(string));

            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));
            mst_item.Columns.Add("MI_BRAND", typeof(string));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_BRAND", typeof(string));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));
            //mst_com.Columns.Add("MC_ANAL18", typeof(string));
            //mst_com.Columns.Add("MC_TAX3", typeof(string));

            int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));

            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));

            foreach (DataRow row in salesDetails.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails.Rows.IndexOf(row);
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_NAME"] = row["MBE_NAME"].ToString();
                    dr["MBE_ADD1"] = row["MBE_ADD1"].ToString();
                    dr["MBE_ADD2"] = row["MBE_ADD2"].ToString();
                    dr["MBE_COUNTRY_CD"] = row["MBE_COUNTRY_CD"].ToString();
                    dr["mbe_nationality"] = row["mbe_nationality"].ToString();

                    mst_busentity.Rows.Add(dr);

                    MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["SAH_DEL_LOC"].ToString());

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
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    dr["SAH_CURRENCY"] = row["SAH_CURRENCY"].ToString();
                    dr["esep_name_initials"] = row["esep_name_initials"].ToString();

                    sat_hdr1.Rows.Add(dr);
                };

                int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));

                foreach (DataRow row1 in int_batch1.Rows)
                {
                    dr = int_batch.NewRow();
                    dr["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                    dr["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                    dr["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                    dr["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                    dr["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                    int_batch.Rows.Add(dr);
                }

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
                dr["MI_BRAND"] = row["MI_BRAND"].ToString();

                mst_item.Rows.Add(dr);

                DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(GlbReportDoc);

                foreach (DataRow row1 in receiptDetails.Rows)
                {
                    if (row1["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                    {

                        dr = sat_receipt.NewRow();
                        dr["SAR_SEQ_NO"] = row1["SAR_SEQ_NO"].ToString();
                        dr["SAR_COM_CD"] = row1["SAR_COM_CD"].ToString();
                        dr["SAR_RECEIPT_TYPE"] = row1["SAR_RECEIPT_TYPE"].ToString();
                        dr["SAR_RECEIPT_NO"] = row1["SAR_RECEIPT_NO"].ToString();
                        sat_receipt.Rows.Add(dr);



                        dr = sat_receiptitm.NewRow();
                        dr["SARD_SEQ_NO"] = row1["SARD_SEQ_NO"].ToString();
                        dr["SARD_RECEIPT_NO"] = row1["SARD_RECEIPT_NO"].ToString();
                        dr["SARD_INV_NO"] = row1["SARD_INV_NO"].ToString();
                        dr["SARD_PAY_TP"] = row1["SARD_PAY_TP"].ToString();
                        dr["SARD_REF_NO"] = row1["SARD_REF_NO"].ToString();
                        dr["SARD_SETTLE_AMT"] = Convert.ToDecimal(row1["SARD_SETTLE_AMT"].ToString());
                        dr["sard_anal_1"] = row1["sard_anal_1"].ToString();
                        dr["sard_anal_4"] = Convert.ToDecimal(row1["sard_anal_4"].ToString());
                        sat_receiptitm.Rows.Add(dr);
                    };
                };


                if (index == 0)
                {

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
                    mst_profit_center.Rows.Add(dr);


                    dr = mst_com.NewRow();
                    dr["MC_TAX1"] = row["MC_TAX1"].ToString();
                    dr["MC_TAX2"] = row["MC_TAX2"].ToString();
                    dr["MC_CD"] = row["MC_CD"].ToString();
                    dr["MC_DESC"] = row["MC_DESC"].ToString();
                    dr["MC_IT_POWERED"] = row["MC_IT_POWERED"].ToString();
                    //dr["MC_ANAL18"] = row["MC_ANAL18"].ToString();
                    //dr["MC_TAX3"] = row["MC_TAX3"].ToString();
                    mst_com.Rows.Add(dr);

                    dr = sec_user.NewRow();
                    dr["SE_USR_NAME"] = row["SE_USR_NAME"].ToString();
                    dr["SE_USR_ID"] = row["SE_USR_ID"].ToString();
                    sec_user.Rows.Add(dr);
                };

            }


            DataTable deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(GlbReportDoc);
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

            foreach (DataRow row in deliveredSerials.Rows)
            {
                dr = int_hdr.NewRow();
                dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                dr["ITH_COM"] = row["ITH_COM"].ToString();
                dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                int_hdr.Rows.Add(dr);

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

            }

            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
            DataTable hpt_acc = new DataTable();

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

            mst_item = mst_item.DefaultView.ToTable(true);

            _DfInvPP.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _DfInvPP.Database.Tables["mst_com"].SetDataSource(mst_com);
            _DfInvPP.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);

            _DfInvPP.Database.Tables["sec_user"].SetDataSource(sec_user);
            _DfInvPP.Database.Tables["viw_sales_details"].SetDataSource(salesDetails);
            _DfInvPP.Database.Tables["MST_ITM"].SetDataSource(mst_item);
            sat_receipt = sat_receipt.DefaultView.ToTable(true);
            sat_receiptitm = sat_receiptitm.DefaultView.ToTable(true);
            foreach (object repOp in _DfInvPP.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;

                    if (_cs.SubreportName == "payments")
                    {
                        ReportDocument subRepDoc = _DfInvPP.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "customer")
                    {
                        ReportDocument subRepDoc = _DfInvPP.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["SAT_DF_CUS_DET"].SetDataSource(SAT_DF_CUS_DET);

                    }
                }
            }

            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = _DfInvPP;

            crystalReportViewer1.RefreshReport();

        }

        private void InvociePrintDutyFree_Edision()
        {// Nadeeka 25-07-2014
            string invNo = default(string);
            invNo = GlbReportDoc;


            DataTable salesDetails = new DataTable();
            salesDetails.Clear();
            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable int_batch = new DataTable();
            DataTable int_batch1 = new DataTable();

            DataTable SAT_DF_CUS_DET = CHNLSVC.Sales.GetDFCusPassport(GlbReportDoc);
            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();

            DataTable sat_vou_det = new DataTable();
            sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(invNo);


            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();
            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _DealerinvReport.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            DataRow dr;

            sat_receipt.Columns.Add("SAR_SEQ_NO", typeof(string));
            sat_receipt.Columns.Add("SAR_COM_CD", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_TYPE", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_NO", typeof(string));


            sat_receiptitm.Columns.Add("SARD_SEQ_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_RECEIPT_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_INV_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_PAY_TP", typeof(string));
            sat_receiptitm.Columns.Add("SARD_REF_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_SETTLE_AMT", typeof(decimal));
            sat_receiptitm.Columns.Add("sard_anal_1", typeof(string));
            sat_receiptitm.Columns.Add("sard_anal_4", typeof(decimal));

            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_NAME", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD1", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD2", typeof(string));
            mst_busentity.Columns.Add("MBE_COUNTRY_CD", typeof(string));
            mst_busentity.Columns.Add("mbe_nationality", typeof(string));

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
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_NAME", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_SEQ_NO", typeof(decimal));
            sat_hdr1.Columns.Add("SAH_COM", typeof(string));
            sat_hdr1.Columns.Add("SAH_CRE_BY", typeof(string));
            sat_hdr1.Columns.Add("SAH_CURRENCY", typeof(string));
            sat_hdr1.Columns.Add("esep_name_initials", typeof(string));


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
            mst_profit_center.Columns.Add("MPC_TEL", typeof(string));
            mst_profit_center.Columns.Add("MPC_FAX", typeof(string));
            mst_profit_center.Columns.Add("MPC_EMAIL", typeof(string));


            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));
            mst_item.Columns.Add("MI_BRAND", typeof(string));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_BRAND", typeof(string));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));
            //mst_com.Columns.Add("MC_ANAL18", typeof(string));
            //mst_com.Columns.Add("MC_TAX3", typeof(string));

            int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));

            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));


            foreach (DataRow row in salesDetails.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails.Rows.IndexOf(row);
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_NAME"] = row["MBE_NAME"].ToString();
                    dr["MBE_ADD1"] = row["MBE_ADD1"].ToString();
                    dr["MBE_ADD2"] = row["MBE_ADD2"].ToString();
                    dr["MBE_COUNTRY_CD"] = row["MBE_COUNTRY_CD"].ToString();
                    dr["mbe_nationality"] = row["mbe_nationality"].ToString();

                    mst_busentity.Rows.Add(dr);

                    MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["SAH_DEL_LOC"].ToString());

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
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    dr["SAH_CURRENCY"] = row["SAH_CURRENCY"].ToString();
                    dr["esep_name_initials"] = row["esep_name_initials"].ToString();

                    sat_hdr1.Rows.Add(dr);
                };

                int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));


                foreach (DataRow row1 in int_batch1.Rows)
                {
                    dr = int_batch.NewRow();
                    dr["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                    dr["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                    dr["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                    dr["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                    dr["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                    int_batch.Rows.Add(dr);

                }


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
                dr["MI_BRAND"] = row["MI_BRAND"].ToString();

                mst_item.Rows.Add(dr);





                DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(GlbReportDoc);








                foreach (DataRow row1 in receiptDetails.Rows)
                {
                    if (row1["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                    {

                        dr = sat_receipt.NewRow();
                        dr["SAR_SEQ_NO"] = row1["SAR_SEQ_NO"].ToString();
                        dr["SAR_COM_CD"] = row1["SAR_COM_CD"].ToString();
                        dr["SAR_RECEIPT_TYPE"] = row1["SAR_RECEIPT_TYPE"].ToString();
                        dr["SAR_RECEIPT_NO"] = row1["SAR_RECEIPT_NO"].ToString();
                        sat_receipt.Rows.Add(dr);



                        dr = sat_receiptitm.NewRow();
                        dr["SARD_SEQ_NO"] = row1["SARD_SEQ_NO"].ToString();
                        dr["SARD_RECEIPT_NO"] = row1["SARD_RECEIPT_NO"].ToString();
                        dr["SARD_INV_NO"] = row1["SARD_INV_NO"].ToString();
                        dr["SARD_PAY_TP"] = row1["SARD_PAY_TP"].ToString();
                        dr["SARD_REF_NO"] = row1["SARD_REF_NO"].ToString();
                        dr["SARD_SETTLE_AMT"] = Convert.ToDecimal(row1["SARD_SETTLE_AMT"].ToString());
                        dr["sard_anal_1"] = row1["sard_anal_1"].ToString();
                        dr["sard_anal_4"] = Convert.ToDecimal(row1["sard_anal_4"].ToString());
                        sat_receiptitm.Rows.Add(dr);
                    };
                };


                if (index == 0)
                {

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
                    mst_profit_center.Rows.Add(dr);


                    dr = mst_com.NewRow();
                    dr["MC_TAX1"] = row["MC_TAX1"].ToString();
                    dr["MC_TAX2"] = row["MC_TAX2"].ToString();
                    dr["MC_CD"] = row["MC_CD"].ToString();
                    dr["MC_DESC"] = row["MC_DESC"].ToString();
                    dr["MC_IT_POWERED"] = row["MC_IT_POWERED"].ToString();
                    //dr["MC_ANAL18"] = row["MC_ANAL18"].ToString();
                    //dr["MC_TAX3"] = row["MC_TAX3"].ToString();
                    mst_com.Rows.Add(dr);

                    dr = sec_user.NewRow();
                    dr["SE_USR_NAME"] = row["SE_USR_NAME"].ToString();
                    dr["SE_USR_ID"] = row["SE_USR_ID"].ToString();
                    sec_user.Rows.Add(dr);
                };

            }


            DataTable deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(GlbReportDoc);
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

            foreach (DataRow row in deliveredSerials.Rows)
            {
                dr = int_hdr.NewRow();
                dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                dr["ITH_COM"] = row["ITH_COM"].ToString();
                dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                int_hdr.Rows.Add(dr);

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

            }

            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
            DataTable hpt_acc = new DataTable();



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

            mst_item = mst_item.DefaultView.ToTable(true);

            _DfInvEdi.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _DfInvEdi.Database.Tables["mst_com"].SetDataSource(mst_com);
            _DfInvEdi.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);

            _DfInvEdi.Database.Tables["sec_user"].SetDataSource(sec_user);
            _DfInvEdi.Database.Tables["viw_sales_details"].SetDataSource(salesDetails);
            _DfInvEdi.Database.Tables["MST_ITM"].SetDataSource(mst_item);
            sat_receipt = sat_receipt.DefaultView.ToTable(true);
            sat_receiptitm = sat_receiptitm.DefaultView.ToTable(true);
            foreach (object repOp in _DfInvEdi.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;

                    if (_cs.SubreportName == "payments")
                    {
                        ReportDocument subRepDoc = _DfInvEdi.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "customer")
                    {
                        ReportDocument subRepDoc = _DfInvEdi.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["SAT_DF_CUS_DET"].SetDataSource(SAT_DF_CUS_DET);


                    }
                    if (_cs.SubreportName == "serial")
                    {
                        ReportDocument subRepDoc = _DfInvEdi.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);
                        subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch1);

                    }
                }
            }



            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = _DfInvEdi;

            crystalReportViewer1.RefreshReport();




        }
        private void ReceivableMovementRep()
        {
            clsSalesRep obj = new clsSalesRep();
            obj.ReceivableMovementReport();

            if (BaseCls.GlbReportName == "ReceivableMovementReports.rpt")
                crystalReportViewer1.ReportSource = obj._recMoverpt;
            else
                crystalReportViewer1.ReportSource = obj._recMoveSumrpt;
            this.Text = "Receivable Movement";
            crystalReportViewer1.ShowExportButton = true;
            crystalReportViewer1.RefreshReport();

        }

        private void DeliveredSalesRep()
        {
            obj.DeliveredSalesReport();

            if (BaseCls.GlbReportType == "DSALE")
            {
                if (BaseCls.GlbReportName == "DeliveredSalesReport.rpt")
                {
                    crystalReportViewer1.ReportSource = obj._delSalesrptPC;
                    this.Text = "Delivered Sales Report - Profit center wise";
                }
                else if (BaseCls.GlbReportName == "DeliveredSalesReport_withCust.rpt")
                {
                    crystalReportViewer1.ReportSource = obj._delSalesrptCust;
                    this.Text = "Delivered Sales Detail Report";
                }
                else
                {
                    crystalReportViewer1.ReportSource = obj._delSalesrptItem;
                    this.Text = "Delivered Sales Report - Item wise";
                }
            }
            else
            {
                if (BaseCls.GlbReportName == "DeliveredSalesReport.rpt")
                {
                    crystalReportViewer1.ReportSource = obj._delSalesrptPC;
                    this.Text = "Total Sales Report - Profit center wise";
                }
                else
                {
                    crystalReportViewer1.ReportSource = obj._delSalesrptItem;
                    this.Text = "Total Sales Report - Item wise";
                }
            }

            crystalReportViewer1.RefreshReport();
        }

        private void ComparisonofDeliveredSalesRep()
        {
            obj.ComparisonofDeliveredSalesReport();
            crystalReportViewer1.ReportSource = obj._delSalesComrpt;
            this.Text = "GP WITH REPLACED MODELS REPORT";
            crystalReportViewer1.RefreshReport();
        }

        private void GpDetailwithReplacementReport()
        {
            obj.GpDetailwithReplacementReport();
            crystalReportViewer1.ReportSource = obj._gpRepl;
            this.Text = "COMPARISON OF DELIVERED SALES";
            crystalReportViewer1.RefreshReport();
        }
        private void StockSaleRep()
        {
            obj.StockSalesReport();
            if (BaseCls.GlbReportType == "DTL")
            {
                crystalReportViewer1.ReportSource = obj._stocksale;
            }
            else
            {
                crystalReportViewer1.ReportSource = obj._stocksalesum;
            }
            this.Text = "Supplier wise Stock Sales Report";
            crystalReportViewer1.RefreshReport();
        }
        private void DeliveredSalesInsuRep()
        {
            obj.DeliveredSalesInsuReport();
            crystalReportViewer1.ReportSource = obj._delSalesrptInsu;
            this.Text = "SMART INSURED REPORT";
            crystalReportViewer1.RefreshReport();
        }

        private void TotalSales8020Rep()
        {
            obj.TotalSales8020Report();
            crystalReportViewer1.ReportSource = obj._delSalesrpt8020;
            this.Text = "CUSTOMER ANALYSIS (80% - 20%)";
            crystalReportViewer1.RefreshReport();
        }
        private void RegistrationReport()
        {
            obj.RegistrationReport();
            crystalReportViewer1.ReportSource = obj._vheRegRPT;
            this.Text = "REGISTRATION REPORT";
            crystalReportViewer1.RefreshReport();
        }
        private void ServiceJobInvoiceSGL()
        { //
            obj.InvociePrintServiceSGL();
            crystalReportViewer1.ReportSource = obj._JobInvoiceSGL;
            this.Text = "Job Invoice";
            crystalReportViewer1.RefreshReport();
        }

        private void DeliveredSalesGRNRep()
        {
            obj.DeliveredSalesGRNReport();

            crystalReportViewer1.ReportSource = obj._delSalesrptGRN;
            this.Text = "Delivered Sales Report - With GRN Details";
            crystalReportViewer1.RefreshReport();
        }

        private void DeliveredSalesGRNCostRep()
        {
            obj.DeliveredSalesGRNCostReport();

            crystalReportViewer1.ReportSource = obj._delSalesrptGRNCost;
            this.Text = "Delivered Sales Report - With GRN & Cost Details";
            crystalReportViewer1.RefreshReport();
        }

        private void PayModeTrRep()
        {
            obj.PaymodewiseTrReport();

            crystalReportViewer1.ReportSource = obj._pmodewise;
            this.Text = "Paymode wise Transaction Report";
            crystalReportViewer1.RefreshReport();
        }

        private void DelConfDetRep()
        {
            obj.GetDeliveryConfirmationPendingDetails();

            crystalReportViewer1.ReportSource = obj._DelConfRep;
            this.Text = "Pending Delivery Confirmations";
            crystalReportViewer1.RefreshReport();
        }

        private void TotalRevenueRep()
        {
            obj.TotalRevenueReport();

            crystalReportViewer1.ReportSource = obj._totrev;
            this.Text = "TOTAL REVENUE REPORT";
            crystalReportViewer1.RefreshReport();
        }

        private void ServiceReceiptPrint()
        {

            obj.ServiceReceiptPrint();

            crystalReportViewer1.ReportSource = obj._recSevRecrpt;

            this.Text = "Service Receipt";
            crystalReportViewer1.RefreshReport();
        }

        private void SServiceReceiptPrint()
        {

            obj.SServiceReceiptPrint();

            crystalReportViewer1.ReportSource = obj._SrecSevRecrpt;

            this.Text = "Service Receipt";
            crystalReportViewer1.RefreshReport();
        }


        protected void Page_UnLoad(object sender, EventArgs e)
        {
            this.crystalReportViewer1.Dispose();
            this.crystalReportViewer1 = null;

            GC.Collect();
        }

        public string GetDefaultPrinter()
        {
            string _printerName = string.Empty;
            //PrinterSettings settings = new PrinterSettings();
            //foreach (string printer in PrinterSettings.InstalledPrinters)
            //{
            //    settings.PrinterName = printer;
            //    if (settings.IsDefaultPrinter)
            //    {
            //        _printerName = printer;
            //        break;
            //    }
            //}
            _printerName = printDialog1.PrinterSettings.PrinterName;
            return _printerName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (BaseCls.GlbReportName == "ACInsReqPrint.rpt")
                {

                    _acInsReqPrint.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("DO") : getprtnbr(BaseCls.GlbReportPaperSize);
                    //int papernbr = getprtnbr("DO"); // returns 257 int
                    _acInsReqPrint.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _acInsReqPrint.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }
                if (BaseCls.GlbReportName == "RCCPrint_New.rpt")
                {
                    _rccPrint.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("DO") : getprtnbr(BaseCls.GlbReportPaperSize);
                    //int papernbr = getprtnbr("DO"); // returns 257 int
                    _rccPrint.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _rccPrint.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }
                if (BaseCls.GlbReportName == "RCCPrint_New_Full.rpt")
                {
                    _rccPrintfull.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize);
                    //int papernbr = getprtnbr("DO"); // returns 257 int
                    _rccPrintfull.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _rccPrintfull.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }
                if (BaseCls.GlbReportName == "SRCCPrint_New.rpt")
                {

                    S_rccPrint.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("INV"); // returns 257 int
                    S_rccPrint.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    S_rccPrint.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }

                if (BaseCls.GlbReportName == "RCCPrint_Ack.rpt")
                {

                    _rccPrintAck.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("DO") : getprtnbr(BaseCls.GlbReportPaperSize);
                    //int papernbr = getprtnbr("DO"); // returns 257 int
                    _rccPrintAck.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _rccPrintAck.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }

                if (BaseCls.GlbReportName == "RCCPrint_Ack_Full.rpt")
                {

                    _rccPrintAckFull.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize);
                    //int papernbr = getprtnbr("DO"); // returns 257 int
                    _rccPrintAckFull.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _rccPrintAckFull.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }

                if (BaseCls.GlbReportName == "SRCCPrint_Ack.rpt")
                {

                    S_rccPrintAck.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("INV"); // returns 257 int
                    S_rccPrintAck.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    S_rccPrintAck.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }

                if (GlbReportName == "InvoiceHalfPrints.rpt")
                {
                    invReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    invReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //invReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    invReport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }
                if (GlbReportName == "InvoiceHalfPrints_Full.rpt")
                {
                    invReport3.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    invReport3.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //invReport3.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    invReport3.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }
                if (GlbReportName == "InvoicePOSPrint.rpt")
                {
                    invReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("POS"); // returns 257 int
                    invReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //invReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    invReport1.PrintToPrinter(1, false, 0, 0);

                    btnPrint.Enabled = false;
                }
                if (GlbReportName == "Job_Invoice.rpt")
                {
                    _JobInvoice.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    _JobInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //invReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _JobInvoice.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }
                if (GlbReportName == "Job_Invoice_ABE.rpt")
                {
                    _JobInvoiceABE.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    _JobInvoiceABE.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //invReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _JobInvoiceABE.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }
                if (GlbReportName == "Job_Invoice_Phone.rpt")
                {
                    _JobInvoicePh.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    _JobInvoicePh.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //invReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _JobInvoicePh.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }

                if (GlbReportName == "SInvoiceHalfPrints.rpt")
                {
                    SinvReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("INV"); // returns 257 int
                    SinvReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    SinvReport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;

                }

                if (GlbReportName == "InvoiceHalfPrintNew.rpt")
                {

                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    invfullReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    invfullReport.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                    invfullReport.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;

                }

                if (GlbReportName == "InvoiceHalfPrintNew.rpt")
                {
                    //invReport2.PrintOptions.PrinterName = GetDefaultPrinter();
                    ////int traynbr = gettraynbr("Manual"); // returns 4 int
                    //int papernbr = getprtnbr("DO"); // returns 257 int
                    //invReport2.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //invReport2.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    ////invReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    //invReport2.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    invReport2.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    invReport2.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                    invReport2.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;

                }

                if (GlbReportName == "InvoicePrints_Gold.rpt")
                {
                    _invGold.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    _invGold.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //invReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _invGold.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;



                }


                else if (GlbReportName == "InsurancePrint.rpt")
                {
                    //PageMargins margins;
                    //margins.bottomMargin = 0;
                    //margins.leftMargin = 0;
                    //margins.rightMargin = 0;
                    //margins.topMargin = 0;

                    insReport2.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    //  insReport2.PrintOptions.ApplyPageMargins(margins);
                    insReport2.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;

                    // insReport2.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true; 
                    insReport2.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;

                }


                else if (GlbReportName == "SInsuPrints.rpt")
                {
                    SinsReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("INV"); // returns 257 int
                    SinsReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    SinsReport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;



                }

                else if (GlbReportName == "InvoicePrints.rpt")
                {

                    ////PrintPDF();
                    _DealerinvReport.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    _DealerinvReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _DealerinvReport.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;


                }
                else if (BaseCls.GlbReportName == "DealerInvoicePrints.rpt" || GlbReportName == "DealerInvoicePrints.rpt")
                {
                    int _numPages = 0;
                    _numPages = CHNLSVC.General.GetPrintDocCopies(BaseCls.GlbUserComCode, GlbReportName);
                    if (_numPages == 0)
                    {
                        _numPages = 1;
                    }

                    _DealerinvAuto.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("A4"); // returns 257 int

                    _DealerinvAuto.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _DealerinvAuto.PrintToPrinter(_numPages, false, 0, 0);
                    btnPrint.Enabled = false;


                }
                else if (GlbReportName == "InvoicePrints_New1.rpt")
                {

                    ////PrintPDF();
                    invReportnew1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    invReportnew1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    invReportnew1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;


                }
                else if (GlbReportName == "InvoicePrintTax.rpt")
                {

                    ////PrintPDF();
                    _TaxinvReport.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    _TaxinvReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _TaxinvReport.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;

                }

                else if (GlbReportName == "DealerCreditInvoicePrints.rpt")
                {
                    int _numPages = 0;
                    _numPages = CHNLSVC.General.GetPrintDocCopies(BaseCls.GlbUserComCode, GlbReportName);
                    if (_numPages == 0)
                    {
                        _numPages = 1;
                    }
                    ////PrintPDF();
                    _DealerinvAutoCred.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    _DealerinvAutoCred.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _DealerinvAutoCred.PrintToPrinter(_numPages, false, 0, 0);
                    btnPrint.Enabled = false;

                }

                else if (GlbReportName == "DealerCreditInvoicePrints_AFSL.rpt")
                {
                    int _numPages = 0;
                    _numPages = CHNLSVC.General.GetPrintDocCopies(BaseCls.GlbUserComCode, GlbReportName);
                    if (_numPages == 0)
                    {
                        _numPages = 1;
                    }
                    ////PrintPDF();
                    _DealerinvAutoCred.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    _DealerinvAutoCredAFSL.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _DealerinvAutoCredAFSL.PrintToPrinter(_numPages, false, 0, 0);
                    btnPrint.Enabled = false;

                }
                else if (GlbReportName == "InvoicePrintTax_BOC.rpt")
                {

                    ////PrintPDF();
                    _DealerinvBOC.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    _DealerinvBOC.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _DealerinvBOC.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;


                }

                else if (GlbReportName == "InvoicePrintTax_insus.rpt")
                {

                    ////PrintPDF();
                    _TaxinvInsReport.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    _TaxinvInsReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _TaxinvInsReport.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;


                }
                else if (BaseCls.GlbReportName == "SalesSummary1.rpt")
                {
                    ////PrintPDF();
                    obj._cashSalesrpt.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    obj._cashSalesrpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._cashSalesrpt.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                }

                else if (BaseCls.GlbReportName == "Sales_Figures.rpt")
                {
                    ////PrintPDF();
                    obj._SalesFigrpt.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    obj._SalesFigrpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._SalesFigrpt.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                }

                else if (BaseCls.GlbReportName == "Execitivewise_Sales_with_Invoices.rpt")
                {
                    ////PrintPDF();
                    obj._ExecSaleInvoice.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    obj._ExecSaleInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._ExecSaleInvoice.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Visible = false;

                }
                else if (BaseCls.GlbReportName == "HP_SummaryRep.rpt")
                {
                    ////PrintPDF();
                    obj._HPSalesrpt.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    obj._HPSalesrpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._HPSalesrpt.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                }

                else if (BaseCls.GlbReportName == "DeliveredSalesReport.rpt")
                {
                    ////PrintPDF();
                    obj._delSalesrptPC.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    obj._delSalesrptPC.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._delSalesrptPC.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                }

                else if (BaseCls.GlbReportName == "DeliveredSalesReport_withCust.rpt")
                {
                    ////PrintPDF();
                    obj._delSalesrptCust.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    obj._delSalesrptCust.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._delSalesrptCust.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                }

                else if (BaseCls.GlbReportName == "Forward_Sales_Report.rpt")
                {
                    ////PrintPDF();
                    obj._ForwardSalesrpt1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    obj._ForwardSalesrpt1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._ForwardSalesrpt1.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                }

                else if (BaseCls.GlbReportName == "DeliveredSalesReport_Itemwise.rpt")
                {
                    ////PrintPDF();
                    obj._delSalesrptItem.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    obj._delSalesrptItem.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._delSalesrptItem.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;


                }

                else if (GlbReportName == "InvoicePrint_insus.rpt")
                {


                    _DealerinvInsReport.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    _DealerinvInsReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _DealerinvInsReport.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }
                else if (BaseCls.GlbReportName == "ServiceReceiptPrints.rpt")
                {
                    obj._recSevRecrpt.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    obj._recSevRecrpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._recSevRecrpt.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }
                else if (BaseCls.GlbReportName == "SServiceReceiptPrints.rpt")
                {
                    obj._SrecSevRecrpt.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("INV"); // returns 257 int
                    obj._SrecSevRecrpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._SrecSevRecrpt.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }

                else if (GlbReportName == "SReceiptPrints.rpt")
                {
                    Srecreport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("INV"); // returns 257 int
                    Srecreport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    Srecreport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }

                else if (GlbReportName == "ConsignmentReceiptPrint.rpt")
                {
                    int _numPages = 0;
                    _numPages = CHNLSVC.General.GetPrintDocCopies(BaseCls.GlbUserComCode, GlbReportName);
                    if (_numPages == 0)
                    {
                        _numPages = 1;
                    }
                    recConreport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("A5"); // returns 257 int
                    recConreport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    recConreport1.PrintToPrinter(_numPages, false, 0, 0);
                    btnPrint.Enabled = false;
                }

                else if (BaseCls.GlbReportName == "ReceivableMovementReports.rpt")
                {
                    obj._recMoverpt.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    obj._recMoverpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._recMoverpt.PrintToPrinter(1, false, 0, 0);
                }
                else if (BaseCls.GlbReportName == "ReceivableMovementSummaryReports.rpt")
                {
                    obj._recMoveSumrpt.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    obj._recMoveSumrpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._recMoveSumrpt.PrintToPrinter(1, false, 0, 0);
                }

                else if (GlbReportName == "ReceiptPrints.rpt")
                {
                    PageMargins margins;
                    margins.bottomMargin = 0;
                    margins.leftMargin = 0;
                    margins.rightMargin = 0;
                    margins.topMargin = 0;

                    recreport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    recreport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //recreport1.PrintOptions.ApplyPageMargins(margins);
                    //recreport1.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true; 
                    recreport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }
                else if (GlbReportName == "ReceiptPrints_n.rpt")
                {
                    PageMargins margins;
                    margins.bottomMargin = 0;
                    margins.leftMargin = 0;
                    margins.rightMargin = 0;
                    margins.topMargin = 0;

                    recreport1_n.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    recreport1_n.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //recreport1.PrintOptions.ApplyPageMargins(margins);
                    //recreport1.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true; 
                    recreport1_n.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }
                else if (GlbReportName == "ReceiptPrintDealers.rpt")
                {

                    _DealerrecReport.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    _DealerrecReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _DealerrecReport.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }
                else if (GlbReportName == "DealerReceiptPrint.rpt")
                {

                    delrecreport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("A4"); // returns 257 int
                    delrecreport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    delrecreport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }

                else if (GlbReportName == "HpReceiptPrints.rpt")
                {
                    _HpRec.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    _HpRec.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _HpRec.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }
                else if (BaseCls.GlbReportName == "DebtorSettlement.rpt")
                {
                    obj._DebtSett.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._DebtSett.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._DebtSett.PrintToPrinter(1, false, 0, 0);
                }
                else if (BaseCls.GlbReportName == "DebtorSettlement_PC.rpt")
                {
                    obj._DebtSettPC.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._DebtSettPC.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._DebtSettPC.PrintToPrinter(1, false, 0, 0);
                }
                else if (BaseCls.GlbReportName == "DebtorSettlement_Outs_PC.rpt")
                {
                    obj._DebtSettOutPC.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._DebtSettOutPC.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._DebtSettOutPC.PrintToPrinter(1, false, 0, 0);
                }
                else if (BaseCls.GlbReportName == "DebtorSettlement_Outs.rpt")
                {
                    obj._DebtSettOuts.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._DebtSettOuts.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._DebtSettOuts.PrintToPrinter(1, false, 0, 0);
                }
                else if (BaseCls.GlbReportName == "Age_Debtor_Outstanding.rpt")
                {
                    obj._AgeDebtOuts.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._AgeDebtOuts.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._AgeDebtOuts.PrintToPrinter(1, false, 0, 0);
                }
                else if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_PC.rpt")
                {
                    obj._AgeDebtOutsPC.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._AgeDebtOutsPC.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._AgeDebtOutsPC.PrintToPrinter(1, false, 0, 0);
                }
                else if (BaseCls.GlbReportName == "DebtorSalesReceipts.rpt")
                {
                    obj._DebtSalesRec.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._DebtSalesRec.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._DebtSalesRec.PrintToPrinter(1, false, 0, 0);
                }
                else if (BaseCls.GlbReportName == "Receipt_List.rpt")
                {
                    obj._RecList.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._RecList.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._RecList.PrintToPrinter(1, false, 0, 0);
                }
                else if (BaseCls.GlbReportName == "remitance_det.rpt")
                {
                    obj._remit_det.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._remit_det.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._remit_det.PrintToPrinter(1, false, 0, 0);
                }
                else if (BaseCls.GlbReportName == "Remitance_Sum.rpt" || BaseCls.GlbReportName == "Remitance_Sum_view.rpt")
                {
                    obj._RemSum.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._RemSum.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._RemSum.PrintToPrinter(1, false, 0, 0);
                }

                else if (BaseCls.GlbReportName == "Stamp_Duty_Report.rpt")
                {
                    ////PrintPDF();
                    obj._Stamp_Duty.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    obj._Stamp_Duty.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._Stamp_Duty.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                }
                else if (BaseCls.GlbReportName == "SVAT_Report.rpt")
                {
                    ////PrintPDF();
                    obj._SVAT.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    obj._SVAT.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._SVAT.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                }

                else if (BaseCls.GlbReportName == "POS_Detail_Report.rpt")
                {
                    ////PrintPDF();
                    obj._POSDtlrpt.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    obj._POSDtlrpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._POSDtlrpt.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                }

                else if (BaseCls.GlbReportName == "InsuranceCoverNote.rpt")
                {

                    obj._insCover.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    obj._insCover.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._insCover.PrintToPrinter(1, false, 0, 0);


                }
                else if (BaseCls.GlbReportName == "InsuranceCoverNoteJS.rpt")
                {

                    obj._insCoverJS.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    obj._insCoverJS.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._insCoverJS.PrintToPrinter(1, false, 0, 0);


                }
                else if (BaseCls.GlbReportName == "InsuranceCoverNoteMBSL.rpt")
                {

                    obj._insCoverMBSL.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    obj._insCoverMBSL.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._insCoverMBSL.PrintToPrinter(1, false, 0, 0);


                }
                else if (BaseCls.GlbReportName == "InsuranceCoverNoteUMS.rpt")
                {

                    obj._insCoverUMS.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    obj._insCoverUMS.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._insCoverUMS.PrintToPrinter(1, false, 0, 0);


                }
                else if (BaseCls.GlbReportName == "VoucherPrints.rpt")
                {

                    obj._intVou.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("INV"); // returns 257 int

                    obj._intVou.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._intVou.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;

                }
                else if (BaseCls.GlbReportName == "ChequePrinting1.rpt")
                {

                    obj._vouPrn.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("INV"); // returns 257 int

                    obj._vouPrn.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._vouPrn.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;

                }
                else if (BaseCls.GlbReportName == "ChequePrinting.rpt")
                {

                    obj._vouPrnvou.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("DO"); // returns 257 int

                    obj._vouPrnvou.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._vouPrnvou.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;

                }
                else if (BaseCls.GlbReportName == "EcdVouchar.rpt")
                {

                    obj._ecdVou.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    obj._ecdVou.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._ecdVou.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;

                }


                else if (GlbReportName == "InvoiceDutyFree.rpt")
                {
                    _DfInsReport.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    _DfInsReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //invReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _DfInsReport.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }

                else if (GlbReportName == "InvoiceDutyFree_CLC.rpt")
                {
                    _DfInvCLC.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("INV"); // returns 257 int
                    _DfInvCLC.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //invReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _DfInvCLC.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }

                else if (GlbReportName == "InvoiceDutyFree_PP.rpt")
                {
                    _DfInvPP.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    _DfInvPP.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //invReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _DfInvPP.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }

                else if (BaseCls.GlbReportName == "InvoiceDutyFreeEdison.rpt")
                {
                    _DfInvEdi.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("INV"); // returns 257 int
                    _DfInvEdi.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //invReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _DfInvEdi.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;



                }

                else if (BaseCls.GlbReportName == "sInvoiceDutyFree.rpt")
                {
                    _sDfInsReport.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("INV"); // returns 257 int
                    _sDfInsReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //invReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _sDfInsReport.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;



                }
                else if (BaseCls.GlbReportName == "InvoiceDutyFree.rpt")
                {
                    _DfInsReport.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    _DfInsReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //invReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _DfInsReport.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;



                }
                else if (BaseCls.GlbReportName == "SparePartPrint.rpt")
                {
                    obj._sprPrint.PrintOptions.PrinterName = GetDefaultPrinter();

                    int papernbr = getprtnbr("SP"); // returns 257 int
                    obj._sprPrint.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;

                    obj._sprPrint.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }
                else if (BaseCls.GlbReportName == "Job_Invoice_SGL.rpt")
                {
                    //obj._JobInvoiceSGL.PrintOptions.PrinterName = GetDefaultPrinter();
                    ////int traynbr = gettraynbr("Manual"); // returns 4 int
                    ////int papernbr = getprtnbr("DO"); // returns 257 int
                    ////obj._JobInvoiceSGL.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    ////obj._JobInvoiceSGL.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                    ////invReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    //obj._JobInvoiceSGL.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                    PageMargins margins;
                    margins.bottomMargin = 0;
                    margins.leftMargin = 0;
                    margins.rightMargin = 0;
                    margins.topMargin = 0;

                    obj._JobInvoiceSGL.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    obj._JobInvoiceSGL.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //recreport1.PrintOptions.ApplyPageMargins(margins);
                    //recreport1.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true; 
                    obj._JobInvoiceSGL.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;

                }
                if (GlbReportName == "Job_InvoiceNew.rpt")
                {
                    _JobInvoiceNew.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    _JobInvoiceNew.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //invReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _JobInvoiceNew.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }
                ;


                if (GlbReportName == "InvoiceAuto.rpt") //tharanga 2017/06/15 InvoiceAuto_mob.rpt
                {
                    int _numPages = 0;
                    //_numPages = CHNLSVC.General.GetPrintDocCopies(BaseCls.GlbUserComCode, GlbReportName);
                    //if (_numPages == 0)
                    //{
                    //    _numPages = 1;
                    //}
                    ////PrintPDF();
                    _InvoiceAuto.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    _InvoiceAuto.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _InvoiceAuto.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;

                }

                //tHARANGA 
                if (GlbReportName == "giftvoucher.rpt")
                {
                    giftvoucherprint.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("DO") : getprtnbr(BaseCls.GlbReportPaperSize);
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    giftvoucherprint.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    giftvoucherprint.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }
                if (BaseCls.GlbReportName == "Invoice_summary.rpt") //tharanga 2017/06/26 
                {
                    int _numPages = 0;
                 
                    _Invoice_summary.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    _Invoice_summary.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _Invoice_summary.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;

                }

                else if (GlbReportName == "ReceptPrintNew.rpt")
                {
                    PageMargins margins;
                    margins.bottomMargin = 0;
                    margins.leftMargin = 0;
                    margins.rightMargin = 0;
                    margins.topMargin = 0;

                    _ReceptPrintNew.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    _ReceptPrintNew.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //recreport1.PrintOptions.ApplyPageMargins(margins);
                    //recreport1.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true; 
                    _ReceptPrintNew.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }
                else if (GlbReportName == "Service_Invoice_ABE.rpt")
                {
                    PageMargins margins;
                    margins.bottomMargin = 0;
                    margins.leftMargin = 0;
                    margins.rightMargin = 0;
                    margins.topMargin = 0;

                    _clsServiceRep._Service_Invoice_ABE.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    _clsServiceRep._Service_Invoice_ABE.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    recreport1.PrintOptions.ApplyPageMargins(margins);
                    recreport1.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
                    _clsServiceRep._Service_Invoice_ABE.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }
                    //add  by thqaranga 2017/09/19
                else if (GlbReportName == "InvPrints_Full_Abstract.rpt")
                {
                    _InvPrints_Full_Abstract.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    _InvPrints_Full_Abstract.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //recreport1.PrintOptions.ApplyPageMargins(margins);
                    //recreport1.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true; 
                    _InvPrints_Full_Abstract.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Sales Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        public static int getprtnbr(string pnm)
        {

            PrintDocument printDoc = new PrintDocument();

            //CrystalDecisions.Shared.PaperSize pkSize = new CrystalDecisions.Shared.PaperSize();

            int rawKind = 0;

            for (int a = printDoc.PrinterSettings.PaperSizes.Count - 1; a >= 0; a--)
            {

                if (printDoc.PrinterSettings.PaperSizes[a].PaperName == pnm)
                {

                    rawKind = (int)printDoc.PrinterSettings.PaperSizes[a].RawKind;

                    break;

                }

            }

            return rawKind;

        }

        public int getprintertnbr(string pnm)
        {

            PrintDocument printDoc = new PrintDocument();

            //CrystalDecisions.Shared.PaperSize pkSize = new CrystalDecisions.Shared.PaperSize();

            int rawKind = 0;

            for (int a = printDoc.PrinterSettings.PaperSizes.Count - 1; a >= 0; a--)
            {

                if (printDoc.PrinterSettings.PaperSizes[a].PaperName == pnm)
                {

                    rawKind = (int)printDoc.PrinterSettings.PaperSizes[a].RawKind;

                    break;

                }

            }

            return rawKind;

        }
        public static int gettraynbr(string pnm)
        {

            PrintDocument printDoc = new PrintDocument();

            //CrystalDecisions.Shared.PaperSize pkSize = new CrystalDecisions.Shared.PaperSize();

            int rawKind = 0;

            for (int a = printDoc.PrinterSettings.PaperSources.Count - 1; a >= 0; a--)
            {

                if (printDoc.PrinterSettings.PaperSources[a].SourceName.Substring(0, 6) == pnm)
                {

                    rawKind = (int)printDoc.PrinterSettings.PaperSources[a].RawKind;

                    break;

                }

            }

            return rawKind;

        }

        public void PrintPDF()
        {
            Process proc = new Process();

            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.Verb = "print";
            proc.StartInfo.Verb = "PrintTo";

            string pdfFileName = "1.pdf";


            proc.StartInfo.FileName = @"C:\Program Files\Adobe\Reader 10.0\Reader\AcroRd32.exe";
            proc.StartInfo.Arguments = @"/p /h D:\1.pdf";
            //proc.StartInfo. 
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;

            PrinterSettings setting = new PrinterSettings();
            setting.DefaultPageSettings.Landscape = true;
            int papernbr = getprtnbr("DO"); // returns 257 int
            //setting.DefaultPageSettings.PaperSize = new PaperSize("DO",1,1); 

            //printDialog1.PrinterSettings.PrinterName

            proc.Start();

            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            if (proc.HasExited == false)
            {
                proc.WaitForExit(10000);
                //proc.Kill();
            }

            proc.EnableRaisingEvents = true;
            // AcroRd32.exe
            proc.CloseMainWindow();
            proc.Close();
        }
        public DataTable ConvertListToDataTable(List<string[]> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }

            return table;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void RCCPrint()
        {
            DataTable param = new DataTable();
            DataRow dr;

            DataTable _RCC = CHNLSVC.Financial.Print_RCC_Receipt(BaseCls.GlbReportDoc);
            DataTable _retCond = CHNLSVC.Financial.Print_RCC_Ret_Condition(BaseCls.GlbReportDoc);
            System.Data.DataColumn newColumn = new System.Data.DataColumn("SHOWCOM", typeof(System.Int16));
            newColumn.DefaultValue = BaseCls.ShowComName;
            _RCC.Columns.Add(newColumn);

            if (BaseCls.GlbReportName == "RCCPrint_New.rpt" || BaseCls.GlbReportName == "SRCCPrint_New.rpt")
            {
                _rccPrint.Database.Tables["RCC"].SetDataSource(_RCC);
                crystalReportViewer1.ReportSource = _rccPrint;
            }
            else if (BaseCls.GlbReportName == "RCCPrint_New_Full.rpt")
            {
                _rccPrintfull.Database.Tables["RCC"].SetDataSource(_RCC);
                crystalReportViewer1.ReportSource = _rccPrintfull;
            }
            else if (BaseCls.GlbReportName == "RCCPrint_Ack.rpt" || BaseCls.GlbReportName == "SRCCPrint_Ack.rpt")
            {
                _rccPrintAck.Database.Tables["RCC"].SetDataSource(_RCC);
                crystalReportViewer1.ReportSource = _rccPrintAck;
            }
            else if (BaseCls.GlbReportName == "RCCPrint_Ack_Full.rpt")
            {
                _rccPrintAckFull.Database.Tables["RCC"].SetDataSource(_RCC);
                crystalReportViewer1.ReportSource = _rccPrintAckFull;
            }
            crystalReportViewer1.RefreshReport();

        }

        private void AcInsReqPrint()
        {
            DataTable param = new DataTable();
            DataRow dr;

            DataTable _RCC = CHNLSVC.Financial.Print_ACInsNote(BaseCls.GlbReportDoc);
            DataTable _ReqDet = CHNLSVC.CustService.GetSCVReqData(BaseCls.GlbReportDoc, BaseCls.GlbUserComCode);

            System.Data.DataColumn newColumn = new System.Data.DataColumn("SHOWCOM", typeof(System.Int16));
            newColumn.DefaultValue = BaseCls.ShowComName;
            _RCC.Columns.Add(newColumn);


            _acInsReqPrint.Database.Tables["RCC"].SetDataSource(_RCC);
            _acInsReqPrint.Database.Tables["SCV_REQ_DET"].SetDataSource(_ReqDet);
            crystalReportViewer1.ReportSource = _acInsReqPrint;

            crystalReportViewer1.RefreshReport();

        }

        private void SRCCPrint()
        {
            DataTable param = new DataTable();
            DataRow dr;

            DataTable _RCC = CHNLSVC.Financial.Print_RCC_Receipt(BaseCls.GlbReportDoc);
            DataTable _retCond = CHNLSVC.Financial.Print_RCC_Ret_Condition(BaseCls.GlbReportDoc);

            S_rccPrint.Database.Tables["RCC"].SetDataSource(_RCC);
            // _rccPrint.Database.Tables["Ret_Cond"].SetDataSource(_retCond);

            crystalReportViewer1.ReportSource = S_rccPrint;

            crystalReportViewer1.RefreshReport();

        }

      
            
        public void AOACollection()
        {
            DataTable param = new DataTable();
            DataRow dr;
            DataTable Collection = new DataTable();
            DataTable CollectionT = new DataTable();

            //bll name data get
            foreach (ListViewItem _o in _lstView.Items)
            {
                if (_o.Checked)
                {
                    List<string> tmpList = new List<string>();
                    tmpList = _o.Text.Split(new string[] { "|" }, StringSplitOptions.None).ToList();

                    string pc = null;                    
                    if ((tmpList != null) && (tmpList.Count > 0))
                    {
                        pc = tmpList[0];                        
                    }

                    Collection = CHNLSVC.Inventory.GetAOACollection(BaseCls.GlbUserComCode, pc, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);
                    CollectionT.Merge(Collection);
                }
            }

            DataTable _tblFill = new DataTable();
            _tblFill = CollectionT;
            if (CollectionT != null && CollectionT.Rows.Count > 0)
                if (!string.IsNullOrEmpty(BaseCls.GlbPayType.ToString()))
                {
                    try
                    {
                        _tblFill = CollectionT.AsEnumerable().Where(x => x.Field<string>("pay_tp") == BaseCls.GlbPayType.ToString()).ToList().CopyToDataTable();
                    }
                    catch
                    {
                        _tblFill = CHNLSVC.Inventory.GetAOACollection(BaseCls.GlbUserComCode, string.Empty, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);
                    }
                }
            if (_tblFill != null && _tblFill.Rows.Count > 0)
                if (!string.IsNullOrEmpty(BaseCls.GlbReportExecCode))
                {
                    try
                    {
                        _tblFill = _tblFill.AsEnumerable().Where(x => x.Field<string>("sar_create_by") == BaseCls.GlbReportExecCode.ToString()).ToList().CopyToDataTable();
                    }
                    catch
                    {
                        if (_tblFill == null || _tblFill.Rows.Count <= 0)
                            _tblFill = CHNLSVC.Inventory.GetAOACollection(BaseCls.GlbUserComCode, string.Empty, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);
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

            _aoaColl.Database.Tables["Collection"].SetDataSource(_tblFill);
            _aoaColl.Database.Tables["param"].SetDataSource(param);

            crystalReportViewer1.ReportSource = _aoaColl;
            crystalReportViewer1.RefreshReport();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pname = this.listBox1.SelectedItem.ToString();
            myPrinters.SetDefaultPrinter(pname);
            lblPrinter.Text = "Default Printer- " + GetDefaultPrinter();
        }
     
        private void InvoicePrintServiceall()
        {// Isuru 2017/05/16

            //string itmcode = Data["inl_itm_cd"].ToString();

            string invNo = default(string);
            string accNo = default(string);
            Boolean ishp = default(Boolean);
            ishp = false;
            invNo = GlbReportDoc;
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
            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);

            DataTable _salesDetails = new DataTable();
                         
            typedef = CHNLSVC.Sales.typedetails(invNo);


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
            _JobInvoiceNew.Database.Tables["invoicetype"].SetDataSource(tyedef1);
            //sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(invNo);
            mst_tax_master = CHNLSVC.Sales.GetinvTax(invNo);

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

            _numPages = CHNLSVC.General.GetReprintDocCount(invNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr3);
            _JobInvoiceNew.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

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
            sat_itm.Columns.Add("SAD_MERGE_ITM", typeof(string));
            sat_itm.Columns.Add("SAD_MERGE_ITM_DESC", typeof(string));
            sat_itm.Columns.Add("VAT_AMT", typeof(decimal));
            sat_itm.Columns.Add("NBT_AMT", typeof(decimal));
            sat_itm.Columns.Add("OTH_TAX_AMT", typeof(decimal));
            sat_itm.Columns.Add("SAD_ALT_ITM_DESC", typeof(string));
            sat_itm.Columns.Add("MI_SHORTDESC", typeof(string));
            sat_itm.Columns.Add("MI_MODEL", typeof(string));
            

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
                        MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["sah_del_loc"].ToString());
                    }
                    else
                    {
                        dr["sah_del_loc"] = string.Empty;
                    }
                    sar_sub_tp = CHNLSVC.Sales.GetSalesSubTypeTable(row["SAH_INV_TP"].ToString(), row["SAH_INV_SUB_TP"].ToString());

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
                { 
                    dr["SAD_MERGE_ITM"] = "N/A"; 
                }
                else
                { 
                    dr["SAD_MERGE_ITM"] = row["SAD_MERGE_ITM"].ToString(); 
                }
                dr["SAD_MERGE_ITM_DESC"] = row["SAD_MERGE_ITM_DESC"].ToString();


                dr["VAT_AMT"] = Convert.ToDecimal(row["VAT_AMT"].ToString());
                dr["NBT_AMT"] = Convert.ToDecimal(row["NBT_AMT"].ToString());
                dr["OTH_TAX_AMT"] = Convert.ToDecimal(row["OTH_TAX_AMT"].ToString());

                dr["SAD_ALT_ITM_DESC"] = row["SAD_ALT_ITM_DESC"].ToString();
                dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                if (row["SAD_ALT_ITM_DESC"].ToString() != "" && row["SAD_ALT_ITM_DESC"].ToString() != "N/A")
                {
                    dr["MI_SHORTDESC"] = row["SAD_ALT_ITM_DESC"].ToString();
                }
                else
                {
                    dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                }
                
                dr["MI_MODEL"] = row["MI_MODEL"].ToString();
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

             
               dt = CHNLSVC.Sales.getjobdetailsforjobinvoiceall(jobnum);
                
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

            DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);

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

            DataTable ref_rep_infor = CHNLSVC.Sales.GetReportInfor("InvoiceHalfPrints.rpt");
            mst_item = mst_item.DefaultView.ToTable(true);
            //mst_tax_master
            if (isCredit == 1)
            {
                drr = param.NewRow();
                drr["isCnote"] = 1;
                drr["vatrate"] = mst_tax_master.Rows[0]["SATX_ITM_TAX_RT"];
                param.Rows.Add(drr);
            }
            else
            {
                drr = param.NewRow();
                drr["isCnote"] = 0;
                drr["vatrate"] = mst_tax_master.Rows[0]["SATX_ITM_TAX_RT"];
                param.Rows.Add(drr);
            }

            
           



            _JobInvoiceNew.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _JobInvoiceNew.Database.Tables["mst_com"].SetDataSource(mst_com);
            _JobInvoiceNew.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            _JobInvoiceNew.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            _JobInvoiceNew.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            //_JobInvoiceNew.Database.Tables["mst_item"].SetDataSource(mst_item);
            _JobInvoiceNew.Database.Tables["sec_user"].SetDataSource(sec_user);
            _JobInvoiceNew.Database.Tables["sar_sub_tp"].SetDataSource(sar_sub_tp);
            _JobInvoiceNew.Database.Tables["param"].SetDataSource(param);
            //_JobInvoice.Database.Tables["Promo"].SetDataSource(Promo);
            _JobInvoiceNew.Database.Tables["jobdetails"].SetDataSource(dt);

            foreach (object repOp in _JobInvoiceNew.ReportDefinition.ReportObjects)
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
                        ReportDocument subRepDoc = _JobInvoiceNew.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receiptCQ);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitmCQ);
                    }
                    //if (_cs.SubreportName == "rptAccount")
                    //{
                    //    ReportDocument subRepDoc = _JobInvoiceall.Subreports[_cs.SubreportName];

                    //    subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);
                    //    subRepDoc.Database.Tables["hpt_shed"].SetDataSource(hpt_shed);
                    //}

                    if (_cs.SubreportName == "tax")
                    {
                        ReportDocument subRepDoc = _JobInvoiceNew.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);
                    }
                    if (_cs.SubreportName == "rptComm")
                    {
                        ReportDocument subRepDoc = _JobInvoiceNew.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
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
                        ReportDocument subRepDoc = _JobInvoiceNew.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
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

            this.Text = "Invoice Print";
            crystalReportViewer1.ReportSource = _JobInvoiceNew;
            crystalReportViewer1.RefreshReport();
        }
        
       

       //Tharanga 2017/06/15
        private void InvoicePrintAUTO()
        {
            string invNo = default(string);
            string accNo = default(string);
            Boolean ishp = default(Boolean);
            ishp = false;
            invNo = GlbReportDoc;
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
            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);


            typedef = CHNLSVC.Sales.typedetails(invNo);


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
            _InvoiceAuto.Database.Tables["invoicetype"].SetDataSource(tyedef1);
            //sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(invNo);
            mst_tax_master = CHNLSVC.Sales.GetinvTax(invNo);

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

            _numPages = CHNLSVC.General.GetReprintDocCount(invNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr3);
            _InvoiceAuto.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

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
                    dr["mbe_tit"] = row["mbe_tit"].ToString()=="" ? " ": row["mbe_tit"].ToString();
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
                        MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["sah_del_loc"].ToString());
                    }
                    else
                    {
                        dr["sah_del_loc"] = string.Empty;
                    }
                    sar_sub_tp = CHNLSVC.Sales.GetSalesSubTypeTable(row["SAH_INV_TP"].ToString(), row["SAH_INV_SUB_TP"].ToString());

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


                dt = CHNLSVC.Sales.getjobdetailsforjobinvoiceall(jobnum);

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

            DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);

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

            DataTable ref_rep_infor = CHNLSVC.Sales.GetReportInfor("InvoiceHalfPrints.rpt");
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
                if (mst_tax_master.Rows.Count>0)
                {
                    drr = param.NewRow();
                    drr["isCnote"] = 0;
                    drr["vatrate"] = mst_tax_master.Rows[0]["SATX_ITM_TAX_RT"];
                    param.Rows.Add(drr);
                }
              
            }






            _InvoiceAuto.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _InvoiceAuto.Database.Tables["mst_com"].SetDataSource(mst_com);
            _InvoiceAuto.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            _InvoiceAuto.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            _InvoiceAuto.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            _InvoiceAuto.Database.Tables["mst_item"].SetDataSource(mst_item);
            _InvoiceAuto.Database.Tables["sec_user"].SetDataSource(sec_user);
            _InvoiceAuto.Database.Tables["sar_sub_tp"].SetDataSource(sar_sub_tp);
            _InvoiceAuto.Database.Tables["param"].SetDataSource(param);
            //_JobInvoice.Database.Tables["Promo"].SetDataSource(Promo);
            _InvoiceAuto.Database.Tables["jobdetails"].SetDataSource(dt);

            foreach (object repOp in _InvoiceAuto.ReportDefinition.ReportObjects)
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
                        ReportDocument subRepDoc = _InvoiceAuto.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receiptCQ);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitmCQ);
                    }
                    //if (_cs.SubreportName == "rptAccount")
                    //{
                    //    ReportDocument subRepDoc = _JobInvoiceall.Subreports[_cs.SubreportName];

                    //    subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);
                    //    subRepDoc.Database.Tables["hpt_shed"].SetDataSource(hpt_shed);
                    //}

                    if (_cs.SubreportName == "tax")
                    {
                        ReportDocument subRepDoc = _InvoiceAuto.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);
                    }
                    if (_cs.SubreportName == "rptComm")
                    {
                        ReportDocument subRepDoc = _InvoiceAuto.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
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
                        ReportDocument subRepDoc = _InvoiceAuto.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
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

            this.Text = "Invoice Print";
            crystalReportViewer1.ReportSource = _InvoiceAuto;
            crystalReportViewer1.RefreshReport();
        }



        //Tharanga 2017/06/26
        private void invoceSummary()
        {
            DataTable param = new DataTable();
            DataRow dr;
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("Location", typeof(string)); 
            param.Columns.Add("period", typeof(string));
         

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["Location"] = BaseCls.GlbUserDefLoca == "" ? "ALL" : BaseCls.GlbUserDefLoca;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            param.Rows.Add(dr);

            DataTable _invoceSummary = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable mst_loc = new DataTable();
            mst_loc = CHNLSVC.Sales.GetLocationCode(BaseCls.GlbUserComCode,BaseCls.GlbUserDefLoca);
            mst_com = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            _invoceSummary = CHNLSVC.CustService.GetInvoice_Summary(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportProfit);

            _Invoice_summary.Database.Tables["Invoice_Summary"].SetDataSource(_invoceSummary);
            _Invoice_summary.Database.Tables["param"].SetDataSource(param);
            _Invoice_summary.Database.Tables["mst_com"].SetDataSource(mst_com);
            _Invoice_summary.Database.Tables["mst_loc"].SetDataSource(mst_loc);

            this.Text = "Invoice Print";
            crystalReportViewer1.ReportSource = _Invoice_summary;
            crystalReportViewer1.RefreshReport();
        
        }
        //Tharanga 2017/07/03
        private void Receipt_print_new()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();
            sat_receipt.Clear();


            sat_receipt = CHNLSVC.Sales.GetReceipt(GlbReportDoc);
            sat_receiptitm = CHNLSVC.Sales.GetReceiptItemDetails(GlbReportDoc);
            DataTable mst_profit_center = new DataTable();

            DataTable mst_rec_tp = new DataTable();
            DataTable hpt_insu = new DataTable();
            DataTable sat_recwarrex = new DataTable();
            DataTable sat_veh_reg_txn = new DataTable();
            DataTable sat_receiptitemdetails = new DataTable();
            DataTable mst_com = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            DataTable mst_emp = new DataTable();


            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;
            PRINT_DOC.Rows.Add(dr3);
            _ReceptPrintNew.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            foreach (DataRow row in sat_receipt.Rows)
            {


                mst_profit_center = CHNLSVC.Sales.GetProfitCenterTable(row["sar_com_cd"].ToString(), row["sar_profit_center_cd"].ToString());
                mst_rec_tp = CHNLSVC.Sales.GetReceiptType(row["SAR_RECEIPT_TYPE"].ToString());
                hpt_insu = CHNLSVC.Sales.GetInsurance(row["SAR_RECEIPT_NO"].ToString());
                sat_recwarrex = CHNLSVC.Sales.GetReceiptWarranty(row["SAR_RECEIPT_NO"].ToString());
                sat_veh_reg_txn = CHNLSVC.Sales.GetVehicalRegistrations(row["SAR_RECEIPT_NO"].ToString());
                sat_receiptitemdetails = CHNLSVC.Sales.GetAdvanRecItems(row["SAR_RECEIPT_NO"].ToString());
                if (!string.IsNullOrEmpty(row["sar_anal_4"].ToString()))
                {
                    mst_emp = CHNLSVC.Sales.GetinvEmp(row["SAR_COM_CD"].ToString(), row["sar_anal_4"].ToString());
                }
            }
            DataTable mst_buscom = CHNLSVC.Sales.GetInsuranceCompanyName(GlbReportDoc);

            DataTable itemSerial = CHNLSVC.Sales.GetInvoice_Serials(GlbReportDoc);

            DataTable MST_ITM = new DataTable();

            foreach (DataRow row in sat_veh_reg_txn.Rows)
            {
                MST_ITM = CHNLSVC.Sales.GetItemCode(BaseCls.GlbUserComCode, row["srvt_itm_cd"].ToString());
            }



            _ReceptPrintNew.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
            _ReceptPrintNew.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _ReceptPrintNew.Database.Tables["mst_rec_tp"].SetDataSource(mst_rec_tp);
            _ReceptPrintNew.Database.Tables["mst_com"].SetDataSource(mst_com);


            foreach (object repOp in _ReceptPrintNew.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptVehicle")
                    {
                        ReportDocument subRepDoc = _ReceptPrintNew.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_veh_reg_txn"].SetDataSource(sat_veh_reg_txn);
                        subRepDoc.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);


                    }
                    if (_cs.SubreportName == "warr")
                    {
                        ReportDocument subRepDoc = _ReceptPrintNew.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_recwarrex"].SetDataSource(sat_recwarrex);

                    }
                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = _ReceptPrintNew.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }
                    if (_cs.SubreportName == "pay")
                    {
                        ReportDocument subRepDoc = _ReceptPrintNew.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "paymode")
                    {
                        ReportDocument subRepDoc = _ReceptPrintNew.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "inv")
                    {
                        ReportDocument subRepDoc = _ReceptPrintNew.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "item")
                    {
                        ReportDocument subRepDoc = _ReceptPrintNew.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitemdetails"].SetDataSource(sat_receiptitemdetails);

                    }

                    if (_cs.SubreportName == "gv")
                    {
                        ReportDocument subRepDoc = _ReceptPrintNew.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receiptitemdetails"].SetDataSource(sat_receiptitemdetails);

                    }
                    if (_cs.SubreportName == "emp")
                    {
                        ReportDocument subRepDoc = _ReceptPrintNew.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_emp"].SetDataSource(mst_emp);

                    }
                    if (_cs.SubreportName == "insCom")
                    {
                        ReportDocument subRepDoc = _ReceptPrintNew.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_buscom"].SetDataSource(mst_buscom);

                    }

                    if (_cs.SubreportName == "ItemSerials")
                    {
                        ReportDocument subRepDoc = _ReceptPrintNew.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["itemSerial"].SetDataSource(itemSerial);

                    }

                }
            }

            crystalReportViewer1.ReportSource = _ReceptPrintNew;
            this.Text = "Receipt Print";
            crystalReportViewer1.RefreshReport();



        }

       

        private void Service_Invocie_ABE()//Tharanga 2017/07/06
        {
            _clsServiceRep.Service_Invocie_ABE();
            this.Text = "Invoice Print";
            crystalReportViewer1.ReportSource = _clsServiceRep._Service_Invoice_ABE;
            crystalReportViewer1.RefreshReport();
        }
        //private void InvociePrintFull_ABSTRACT()
        //{
        //    string invNo = default(string);
        //    string accNo = default(string);
        //    string cust_code = default(string);
        //    Boolean ishp = default(Boolean);
        //    ishp = false;
        //    invNo = GlbReportDoc;
        //    DataTable mst_tax_master = new DataTable();
        //    DataRow drr;
        //    DataTable salesDetails = new DataTable();
        //    DataTable sat_vou_det = new DataTable();
        //    DataTable param = new DataTable();
        //    DataTable mst_customer = new DataTable();

        //    param.Clear();
        //    param.Columns.Add("isCnote", typeof(Int16));

        //    Int16 isCredit = 0;

        //    salesDetails.Clear();


        //    salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);

        //    sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(invNo);

        //    mst_tax_master = CHNLSVC.Sales.GetinvTax(invNo);
        //    DataTable sat_hdr1 = new DataTable();
        //    DataTable sat_itm = new DataTable();
        //    DataTable mst_profit_center = new DataTable();
        //    DataTable mst_item = new DataTable();
        //    DataTable mst_item1 = new DataTable();
        //    DataTable mst_com = new DataTable();
        //    DataTable sec_user = new DataTable();
        //    DataTable mst_busentity = new DataTable();
        //    DataTable int_batch = new DataTable();
        //    DataTable int_batch1 = new DataTable();
        //    DataTable MST_ITM = new DataTable();

        //    DataTable MST_LOC = new DataTable();
        //    DataTable sar_sub_tp = new DataTable();
        //    DataTable PRINT_DOC = new DataTable();


        //    DataTable tblComDate = new DataTable();



        //    int _numPages = 0;
        //    DataRow dr3;

        //    _numPages = CHNLSVC.General.GetReprintDocCount(invNo);
        //    PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
        //    PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

        //    dr3 = PRINT_DOC.NewRow();
        //    dr3["NOOFPAGES"] = _numPages;
        //    dr3["SHOWCOM"] = BaseCls.ShowComName;

        //    PRINT_DOC.Rows.Add(dr3);

        //    _InvPrints_Full_Abstract.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);



        //    sat_hdr1.Clear();
        //    sat_itm.Clear();
        //    mst_profit_center.Clear();
        //    mst_item.Clear();
        //    mst_com.Clear();
        //    sec_user.Clear();
        //    mst_busentity.Clear();


        //    DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
        //    DataTable hpt_acc = new DataTable();
        //    DataRow dr;
        //    DataRow dr1;

        //    hpt_acc.Columns.Add("HPA_ACC_NO", typeof(string));
        //    hpt_acc.Columns.Add("HPA_ACC_CRE_DT", typeof(DateTime));
        //    hpt_acc.Columns.Add("HPA_SCH_CD", typeof(string));
        //    hpt_acc.Columns.Add("HPA_TERM", typeof(decimal));
        //    hpt_acc.Columns.Add("HPA_DP_VAL", typeof(decimal));
        //    hpt_acc.Columns.Add("HPA_AF_VAL", typeof(decimal));
        //    hpt_acc.Columns.Add("HPA_INIT_INS", typeof(decimal));
        //    hpt_acc.Columns.Add("HPA_INIT_VAT", typeof(decimal));
        //    hpt_acc.Columns.Add("HPA_INIT_STM", typeof(decimal));
        //    hpt_acc.Columns.Add("HPA_INIT_SER_CHG", typeof(decimal));
        //    hpt_acc.Columns.Add("HPA_PND_VOU", typeof(Int16));
        //    hpt_acc.Columns.Add("HPA_VOU_RMK", typeof(string));

        //    foreach (DataRow row in accountDetails.Rows)
        //    {
        //        dr = hpt_acc.NewRow();
        //        accNo = row["HPA_ACC_NO"].ToString();
        //        dr["HPA_ACC_NO"] = row["HPA_ACC_NO"].ToString();
        //        dr["HPA_ACC_CRE_DT"] = row["HPA_ACC_CRE_DT"].ToString();
        //        dr["HPA_SCH_CD"] = row["HPA_SCH_CD"].ToString();
        //        dr["HPA_TERM"] = Convert.ToDecimal(row["HPA_TERM"].ToString());
        //        dr["HPA_DP_VAL"] = Convert.ToDecimal(row["HPA_DP_VAL"].ToString());
        //        dr["HPA_AF_VAL"] = Convert.ToDecimal(row["HPA_AF_VAL"].ToString());
        //        dr["HPA_INIT_INS"] = Convert.ToDecimal(row["HPA_INIT_INS"].ToString());
        //        dr["HPA_INIT_VAT"] = Convert.ToDecimal(row["HPA_INIT_VAT"].ToString());
        //        dr["HPA_INIT_STM"] = Convert.ToDecimal(row["HPA_INIT_STM"].ToString());
        //        dr["HPA_INIT_SER_CHG"] = Convert.ToDecimal(row["HPA_INIT_SER_CHG"].ToString());
        //        if (row["HPA_PND_VOU"].ToString() == "") { dr["HPA_PND_VOU"] = 0; } else { dr["HPA_PND_VOU"] = Convert.ToInt16(row["HPA_PND_VOU"].ToString()); }
        //        dr["HPA_VOU_RMK"] = row["HPA_VOU_RMK"].ToString();
        //        hpt_acc.Rows.Add(dr);
        //    }

        //    mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
        //    mst_busentity.Columns.Add("MBE_MOB", typeof(string));
        //    mst_busentity.Columns.Add("MBE_NIC", typeof(string));
        //    mst_busentity.Columns.Add("MBE_COM", typeof(string));
        //    mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
        //    mst_busentity.Columns.Add("MBE_CD", typeof(string));
        //    mst_busentity.Columns.Add("MBE_TEL", typeof(string));
        //    mst_busentity.Columns.Add("MBE_SVAT_NO", typeof(string));

        //    sat_hdr1.Columns.Add("SAH_DT", typeof(DateTime));
        //    sat_hdr1.Columns.Add("SAH_INV_NO", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_INV_TP", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_IS_SVAT", typeof(Int16));
        //    sat_hdr1.Columns.Add("SAH_MAN_REF", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_PC", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_REMARKS", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_SALES_EX_CD", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_TAX_EXEMPTED", typeof(Int16));
        //    sat_hdr1.Columns.Add("SAH_TAX_INV", typeof(Int16));
        //    sat_hdr1.Columns.Add("SAH_ACC_NO", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_D_CUST_CD", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_D_CUST_NAME", typeof(string)); //Add by Chamal 23/05/2013
        //    sat_hdr1.Columns.Add("SAH_D_CUST_ADD2", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_D_CUST_ADD1", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_CUS_NAME", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_CUS_CD", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_CUS_ADD2", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_CUS_ADD1", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_SEQ_NO", typeof(decimal));
        //    sat_hdr1.Columns.Add("SAH_COM", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_CRE_BY", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_INV_SUB_TP", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_REF_DOC", typeof(string));
        //    sat_hdr1.Columns.Add("esep_name_initials", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_ANAL_11", typeof(Int16));
        //    sat_hdr1.Columns.Add("sah_del_loc", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_QT_CUST", typeof(string));
        //    sat_hdr1.Columns.Add("SAH_ANAL_4", typeof(string));

        //    sat_itm.Columns.Add("SAD_WARR_REMARKS", typeof(string));
        //    sat_itm.Columns.Add("SAD_UNIT_RT", typeof(decimal));
        //    sat_itm.Columns.Add("SAD_TOT_AMT", typeof(decimal));
        //    sat_itm.Columns.Add("SAD_QTY", typeof(decimal));
        //    sat_itm.Columns.Add("SAD_ITM_TAX_AMT", typeof(decimal));
        //    sat_itm.Columns.Add("SAD_ITM_CD", typeof(string));
        //    sat_itm.Columns.Add("SAD_DISC_RT", typeof(decimal));
        //    sat_itm.Columns.Add("SAD_DISC_AMT", typeof(decimal));
        //    sat_itm.Columns.Add("SAD_SEQ_NO", typeof(string));
        //    sat_itm.Columns.Add("SAD_ITM_LINE", typeof(Int16));
        //    sat_itm.Columns.Add("SAD_WARR_PERIOD", typeof(Int16));

        //    mst_profit_center.Columns.Add("MPC_ADD_1", typeof(string));
        //    mst_profit_center.Columns.Add("MPC_ADD_2", typeof(string));
        //    mst_profit_center.Columns.Add("MPC_CHNL", typeof(string));
        //    mst_profit_center.Columns.Add("MPC_DESC", typeof(string));
        //    mst_profit_center.Columns.Add("MPC_COM", typeof(string));
        //    mst_profit_center.Columns.Add("MPC_CD", typeof(string));
        //    mst_profit_center.Columns.Add("MPC_TEL", typeof(string));
        //    mst_profit_center.Columns.Add("MPC_FAX", typeof(string));
        //    mst_profit_center.Columns.Add("MPC_EMAIL", typeof(string));
        //    mst_profit_center.Columns.Add("MPC_OTH_REF", typeof(string));

        //    mst_item.Columns.Add("MI_MODEL", typeof(string));
        //    mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
        //    mst_item.Columns.Add("MI_CD", typeof(string));
        //    mst_item.Columns.Add("MI_IS_SER1", typeof(Int16));
        //    mst_item.Columns.Add("MI_IS_SER2", typeof(Int16));
        //    mst_item.Columns.Add("MI_WARR", typeof(Int16));

        //    mst_item1.Columns.Add("MI_MODEL", typeof(string));
        //    mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
        //    mst_item1.Columns.Add("MI_CD", typeof(string));
        //    mst_item1.Columns.Add("MI_IS_SER1", typeof(Int16));
        //    mst_item1.Columns.Add("MI_IS_SER2", typeof(Int16));
        //    mst_item1.Columns.Add("MI_WARR", typeof(Int16));

        //    mst_com.Columns.Add("MC_TAX1", typeof(string));
        //    mst_com.Columns.Add("MC_TAX2", typeof(string));
        //    mst_com.Columns.Add("MC_CD", typeof(string));
        //    mst_com.Columns.Add("MC_DESC", typeof(string));
        //    mst_com.Columns.Add("MC_IT_POWERED", typeof(string));
        //    mst_com.Columns.Add("MC_TAX3", typeof(string));
        //    mst_com.Columns.Add("MC_ANAL18", typeof(string));
        //    mst_com.Columns.Add("MC_ANAL19", typeof(string));


        //    sec_user.Columns.Add("SE_USR_NAME", typeof(string));
        //    sec_user.Columns.Add("SE_USR_ID", typeof(string));


        //    int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
        //    int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
        //    int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
        //    int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
        //    int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));
        //    int_batch.Columns.Add("ITB_BATCH_LINE", typeof(Int16));


        //    foreach (DataRow row in salesDetails.Rows)
        //    {

        //        dr = mst_busentity.NewRow();
        //        int index = salesDetails.Rows.IndexOf(row);



        //        int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));
        //        foreach (DataRow row1 in int_batch1.Rows)
        //        {
        //            dr1 = int_batch.NewRow();
        //            dr1["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
        //            dr1["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
        //            dr1["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
        //            dr1["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
        //            dr1["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
        //            dr1["ITB_BATCH_LINE"] = Convert.ToInt16(row1["ITB_BATCH_LINE"].ToString());
        //            int_batch.Rows.Add(dr1);

        //        }
        //        if (index == 0)
        //        {
        //            dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
        //            dr["MBE_MOB"] = row["MBE_MOB"].ToString();
        //            dr["MBE_NIC"] = row["MBE_NIC"].ToString();
        //            dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
        //            dr["MBE_COM"] = row["MBE_COM"].ToString();
        //            dr["MBE_CD"] = row["MBE_CD"].ToString();
        //            dr["MBE_TEL"] = row["MBE_TEL"].ToString();
        //            dr["MBE_SVAT_NO"] = row["MBE_SVAT_NO"].ToString();

        //            mst_busentity.Rows.Add(dr);


        //            dr = sat_hdr1.NewRow();

        //            dr["SAH_DT"] = Convert.ToDateTime(row["SAH_DT"].ToString());
        //            dr["SAH_INV_NO"] = row["SAH_INV_NO"].ToString();
        //            dr["SAH_INV_TP"] = row["SAH_INV_TP"].ToString();
        //            dr["SAH_IS_SVAT"] = Convert.ToInt16(row["SAH_IS_SVAT"].ToString());
        //            dr["SAH_MAN_REF"] = row["SAH_MAN_REF"].ToString();
        //            dr["SAH_PC"] = row["SAH_PC"].ToString();
        //            dr["SAH_REMARKS"] = row["SAH_REMARKS"].ToString();
        //            dr["SAH_SALES_EX_CD"] = row["SAH_SALES_EX_CD"].ToString();
        //            dr["SAH_TAX_EXEMPTED"] = Convert.ToInt16(row["SAH_TAX_EXEMPTED"].ToString());
        //            dr["SAH_TAX_INV"] = Convert.ToInt16(row["SAH_TAX_INV"].ToString());
        //            dr["SAH_ACC_NO"] = row["SAH_ACC_NO"].ToString();
        //            dr["SAH_D_CUST_CD"] = row["SAH_D_CUST_CD"].ToString();
        //            dr["SAH_D_CUST_NAME"] = row["SAH_D_CUST_NAME"].ToString();//Add by Chamal 23/05/2013
        //            dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
        //            dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
        //            dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
        //            dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
        //            dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
        //            dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
        //            dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
        //            dr["SAH_COM"] = row["SAH_COM"].ToString();
        //            dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
        //            dr["SAH_INV_SUB_TP"] = row["SAH_INV_SUB_TP"].ToString();
        //            dr["SAH_REF_DOC"] = row["SAH_REF_DOC"].ToString();
        //            dr["esep_name_initials"] = row["esep_name_initials"].ToString();
        //            dr["SAH_ANAL_4"] = row["SAH_ANAL_4"].ToString();
        //            if (!string.IsNullOrEmpty(row["SAH_ANAL_11"].ToString()))// Nadeeka 02-03-2015
        //            {
        //                dr["SAH_ANAL_11"] = Convert.ToInt16(row["SAH_ANAL_11"].ToString());
        //            }
        //            else
        //            {
        //                dr["SAH_ANAL_11"] = 0;
        //            }
        //            if (!string.IsNullOrEmpty(row["sah_del_loc"].ToString()))
        //            {
        //                dr["sah_del_loc"] = row["sah_del_loc"].ToString();
        //                MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["sah_del_loc"].ToString());
        //            }
        //            else
        //            {
        //                dr["sah_del_loc"] = string.Empty;
        //            }
        //            cust_code = row["SAH_STRUCTURE_SEQ"].ToString(); //Sanjeewa 2015-08-05 QUOTATION NO
        //            mst_customer = new DataTable();
        //            mst_customer = CHNLSVC.Sales.Get_CustomerDetails(cust_code);
        //            foreach (DataRow row2 in mst_customer.Rows)
        //            {
        //                dr["SAH_QT_CUST"] = row2["mbg_name"].ToString();
        //            }
        //            sar_sub_tp = CHNLSVC.Sales.GetSalesSubTypeTable(row["SAH_INV_TP"].ToString(), row["SAH_INV_SUB_TP"].ToString());

        //            sat_hdr1.Rows.Add(dr);
        //        };



        //        dr = sat_itm.NewRow();
        //        dr["SAD_WARR_REMARKS"] = row["SAD_WARR_REMARKS"].ToString();
        //        dr["SAD_UNIT_RT"] = Convert.ToDecimal(row["SAD_UNIT_RT"].ToString());
        //        dr["SAD_TOT_AMT"] = Convert.ToDecimal(row["SAD_TOT_AMT"].ToString());
        //        dr["SAD_QTY"] = Convert.ToDecimal(row["SAD_QTY"].ToString());
        //        dr["SAD_ITM_TAX_AMT"] = Convert.ToDecimal(row["SAD_ITM_TAX_AMT"].ToString());
        //        dr["SAD_ITM_CD"] = row["SAD_ITM_CD"].ToString();
        //        dr["SAD_DISC_RT"] = Convert.ToDecimal(row["SAD_DISC_RT"].ToString());
        //        dr["SAD_DISC_AMT"] = Convert.ToDecimal(row["SAD_DISC_AMT"].ToString());
        //        dr["SAD_SEQ_NO"] = Convert.ToDecimal(row["SAD_SEQ_NO"].ToString());
        //        dr["SAD_ITM_LINE"] = Convert.ToInt16(row["SAD_ITM_LINE"].ToString());
        //        dr["SAD_WARR_PERIOD"] = Convert.ToInt16(row["SAD_WARR_PERIOD"].ToString());
        //        sat_itm.Rows.Add(dr);

        //        dr = mst_item.NewRow();
        //        dr["MI_MODEL"] = row["MI_MODEL"].ToString();
        //        dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
        //        dr["MI_CD"] = row["MI_CD"].ToString();

        //        mst_item.Rows.Add(dr);

        //        if (index == 0)
        //        {
        //            if (accountDetails.Rows.Count > 0)
        //            {
        //                tblComDate = CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), accNo);
        //            }
        //            else
        //            {
        //                tblComDate = CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), invNo);
        //            }

        //            dr = mst_profit_center.NewRow();
        //            dr["MPC_ADD_1"] = row["MPC_ADD_1"].ToString();
        //            dr["MPC_ADD_2"] = row["MPC_ADD_2"].ToString();
        //            dr["MPC_CHNL"] = row["MPC_CHNL"].ToString();
        //            dr["MPC_DESC"] = row["MPC_DESC"].ToString();
        //            dr["MPC_COM"] = row["MPC_COM"].ToString();
        //            dr["MPC_CD"] = row["MPC_CD"].ToString();
        //            dr["MPC_TEL"] = row["MPC_TEL"].ToString();
        //            dr["MPC_FAX"] = row["MPC_FAX"].ToString();
        //            dr["MPC_EMAIL"] = row["MPC_EMAIL"].ToString();
        //            dr["MPC_OTH_REF"] = row["MPC_OTH_REF"].ToString();

        //            mst_profit_center.Rows.Add(dr);


        //            dr = mst_com.NewRow();
        //            dr["MC_TAX1"] = row["MC_TAX1"].ToString();
        //            dr["MC_TAX2"] = row["MC_TAX2"].ToString();
        //            dr["MC_CD"] = row["MC_CD"].ToString();
        //            dr["MC_DESC"] = row["MC_DESC"].ToString();
        //            dr["MC_IT_POWERED"] = row["MC_IT_POWERED"].ToString();
        //            dr["MC_TAX3"] = row["MC_TAX3"].ToString();
        //            dr["MC_ANAL18"] = row["MC_ANAL18"].ToString();
        //            dr["MC_ANAL19"] = row["MC_ANAL19"].ToString();
        //            mst_com.Rows.Add(dr);

        //            dr = sec_user.NewRow();
        //            dr["SE_USR_NAME"] = row["SE_USR_NAME"].ToString();
        //            dr["SE_USR_ID"] = row["SE_USR_ID"].ToString();
        //            sec_user.Rows.Add(dr);
        //        };

        //    }

        //    DataTable deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
        //    DataTable int_hdr = new DataTable();
        //    DataTable int_ser = new DataTable();



        //    int_hdr.Columns.Add("ITH_SEQ_NO", typeof(decimal));
        //    int_hdr.Columns.Add("ITH_COM", typeof(string));
        //    int_hdr.Columns.Add("ITH_OTH_DOCNO", typeof(string));
        //    int_hdr.Columns.Add("ITH_DOC_NO", typeof(string));

        //    int_ser.Columns.Add("ITS_SEQ_NO", typeof(string));
        //    int_ser.Columns.Add("ITS_ITM_LINE", typeof(Int16));
        //    int_ser.Columns.Add("ITS_BATCH_LINE", typeof(Int16));
        //    int_ser.Columns.Add("ITS_SER_LINE", typeof(Int16));
        //    int_ser.Columns.Add("ITS_DOC_NO", typeof(string));
        //    int_ser.Columns.Add("ITS_SER_ID", typeof(string));
        //    int_ser.Columns.Add("ITS_SER_1", typeof(string));
        //    int_ser.Columns.Add("ITS_SER_2", typeof(string));
        //    int_ser.Columns.Add("ITS_WARR_NO", typeof(string));
        //    int_ser.Columns.Add("ITS_WARR_PERIOD", typeof(decimal));
        //    int_ser.Columns.Add("ITS_ITM_CD", typeof(string));


        //    foreach (DataRow row in deliveredSerials.Rows)
        //    {
        //        dr = int_hdr.NewRow();
        //        dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
        //        dr["ITH_COM"] = row["ITH_COM"].ToString();
        //        dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
        //        dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
        //        int_hdr.Rows.Add(dr);

        //        if (row["ITS_SER_1"].ToString() != "N/A")
        //        {
        //            dr = int_ser.NewRow();
        //            dr["ITS_SEQ_NO"] = row["ITS_SEQ_NO"].ToString();
        //            dr["ITS_ITM_LINE"] = Convert.ToInt16(row["ITS_ITM_LINE"].ToString());
        //            dr["ITS_BATCH_LINE"] = Convert.ToInt16(row["ITS_BATCH_LINE"].ToString());
        //            dr["ITS_SER_LINE"] = row["ITS_SER_LINE"].ToString();
        //            dr["ITS_DOC_NO"] = row["ITS_DOC_NO"].ToString();
        //            dr["ITS_SER_ID"] = row["ITS_SER_ID"].ToString();
        //            dr["ITS_SER_1"] = row["ITS_SER_1"].ToString();
        //            dr["ITS_SER_2"] = row["ITS_SER_2"].ToString();
        //            dr["ITS_WARR_NO"] = row["ITS_WARR_NO"].ToString();
        //            dr["ITS_WARR_PERIOD"] = Convert.ToDecimal(row["ITS_WARR_PERIOD"].ToString());
        //            int_ser.Rows.Add(dr);
        //        };

        //        DataTable MST_ITM1 = new DataTable();
        //        MST_ITM1 = CHNLSVC.Sales.GetItemCode(row["ITH_COM"].ToString(), row["ITS_ITM_CD"].ToString());
        //        foreach (DataRow row1 in MST_ITM1.Rows)
        //        {
        //            dr = mst_item1.NewRow();
        //            dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
        //            dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
        //            dr["MI_CD"] = row1["MI_CD"].ToString();
        //            dr["MI_IS_SER1"] = row1["MI_IS_SER1"].ToString();
        //            dr["MI_IS_SER2"] = row1["MI_IS_SER2"].ToString();
        //            dr["MI_WARR"] = row1["MI_WARR"].ToString();
        //            mst_item1.Rows.Add(dr);
        //        }

        //    }

        //    DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);


        //    DataTable sat_receiptCQ = new DataTable();
        //    DataTable sat_receiptitmCQ = new DataTable();
        //    sat_receiptCQ.Columns.Add("SAR_SEQ_NO", typeof(string));
        //    sat_receiptCQ.Columns.Add("SAR_COM_CD", typeof(string));
        //    sat_receiptCQ.Columns.Add("SAR_RECEIPT_TYPE", typeof(string));
        //    sat_receiptCQ.Columns.Add("SAR_RECEIPT_NO", typeof(string));

        //    sat_receiptitmCQ.Columns.Add("SARD_SEQ_NO", typeof(string));
        //    sat_receiptitmCQ.Columns.Add("SARD_RECEIPT_NO", typeof(string));
        //    sat_receiptitmCQ.Columns.Add("SARD_INV_NO", typeof(string));
        //    sat_receiptitmCQ.Columns.Add("SARD_PAY_TP", typeof(string));
        //    sat_receiptitmCQ.Columns.Add("SARD_REF_NO", typeof(string));
        //    sat_receiptitmCQ.Columns.Add("SARD_SETTLE_AMT", typeof(decimal));
        //    sat_receiptitmCQ.Columns.Add("SAPT_DESC", typeof(string));
        //    sat_receiptitmCQ.Columns.Add("SARD_RMK", typeof(string));
        //    sat_receiptitmCQ.Columns.Add("SARD_ANAL_3", typeof(decimal));
        //    sat_receiptitmCQ.Columns.Add("SARD_CC_PERIOD", typeof(int));

        //    foreach (DataRow row in receiptDetails.Rows)
        //    {

        //        if (row["SAR_RECEIPT_TYPE"].ToString() == "DIR")//(row["SARD_PAY_TP"].ToString() == "CHEQUE" && row["SAR_RECEIPT_TYPE"].ToString() == "DIR")
        //        {

        //            dr = sat_receiptCQ.NewRow();
        //            dr["SAR_SEQ_NO"] = row["SAR_SEQ_NO"].ToString();
        //            dr["SAR_COM_CD"] = row["SAR_COM_CD"].ToString();
        //            dr["SAR_RECEIPT_TYPE"] = row["SAR_RECEIPT_TYPE"].ToString();
        //            dr["SAR_RECEIPT_NO"] = row["SAR_RECEIPT_NO"].ToString();
        //            sat_receiptCQ.Rows.Add(dr);


        //            dr = sat_receiptitmCQ.NewRow();
        //            dr["SARD_SEQ_NO"] = row["SARD_SEQ_NO"].ToString();
        //            dr["SARD_RECEIPT_NO"] = row["SARD_RECEIPT_NO"].ToString();
        //            dr["SARD_INV_NO"] = row["SARD_INV_NO"].ToString();
        //            dr["SARD_PAY_TP"] = row["SARD_PAY_TP"].ToString();
        //            dr["SARD_REF_NO"] = row["SARD_REF_NO"].ToString();
        //            dr["SAPT_DESC"] = row["SAPT_DESC"].ToString();
        //            dr["SARD_SETTLE_AMT"] = Convert.ToDecimal(row["SARD_SETTLE_AMT"].ToString());
        //            dr["SARD_RMK"] = row["SARD_RMK"].ToString();
        //            dr["SARD_ANAL_3"] = Convert.ToDecimal(row["SARD_ANAL_3"].ToString());
        //            dr["SARD_CC_PERIOD"] = Convert.ToDecimal(row["SARD_CC_PERIOD"].ToString());
        //            sat_receiptitmCQ.Rows.Add(dr);


        //            if (row["SARD_PAY_TP"].ToString() == "CRNOTE")
        //            {
        //                isCredit = 1;

        //            }
        //        };

        //    }

        //    DataTable hpt_shed = CHNLSVC.Sales.GetAccountSchedule(invNo);
        //    DataTable Promo = CHNLSVC.Sales.GetPromotionByInvoice(invNo);
        //    DataTable ref_rep_infor = new DataTable();

        //    if (GlbReportName == "InvoiceHalfPrints.rpt")
        //        ref_rep_infor = CHNLSVC.Sales.GetReportInfor("InvoiceHalfPrints.rpt");
        //    else
        //        ref_rep_infor = CHNLSVC.Sales.GetReportInfor("InvoicePOSPrint.rpt");        //kapila 29/6/2015

        //    mst_item = mst_item.DefaultView.ToTable(true);

        //    if (isCredit == 1)
        //    {
        //        drr = param.NewRow();
        //        drr["isCnote"] = 1;
        //        param.Rows.Add(drr);
        //    }
        //    else
        //    {
        //        drr = param.NewRow();
        //        drr["isCnote"] = 0;
        //        param.Rows.Add(drr);
        //    }

        //    _InvPrints_Full_Abstract.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
        //    _InvPrints_Full_Abstract.Database.Tables["mst_com"].SetDataSource(mst_com);
        //    _InvPrints_Full_Abstract.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
        //   // _InvPrints_Full_Abstract.Database.Tables["sat_itm"].SetDataSource(sat_itm);
        //    _InvPrints_Full_Abstract.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
        //    _InvPrints_Full_Abstract.Database.Tables["mst_item"].SetDataSource(mst_item);
        //    _InvPrints_Full_Abstract.Database.Tables["sec_user"].SetDataSource(sec_user);
        //    _InvPrints_Full_Abstract.Database.Tables["sar_sub_tp"].SetDataSource(sar_sub_tp);
        //    _InvPrints_Full_Abstract.Database.Tables["param"].SetDataSource(param);
        //    _InvPrints_Full_Abstract.Database.Tables["Promo"].SetDataSource(Promo);


        //    //foreach (object repOp in _InvPrints_Full_Abstract.ReportDefinition.ReportObjects)
        //    //{
        //    //    string _s = repOp.GetType().ToString();
        //    //    if (_s.ToLower().Contains("subreport"))
        //    //    {
        //    //        SubreportObject _cs = (SubreportObject)repOp;
        //    //        if (_cs.SubreportName == "rptWarranty")
        //    //        {
        //    //            ReportDocument subRepDoc = _InvPrints_Full_Abstract.Subreports[_cs.SubreportName];

        //    //            subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
        //    //        }

        //    //        if (_cs.SubreportName == "rptCheque")
        //    //        {
        //    //            ReportDocument subRepDoc = _InvPrints_Full_Abstract.Subreports[_cs.SubreportName];

        //    //            subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receiptCQ);
        //    //            subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitmCQ);

        //    //        }
        //    //        if (_cs.SubreportName == "rptAccount")
        //    //        {
        //    //            ReportDocument subRepDoc = _InvPrints_Full_Abstract.Subreports[_cs.SubreportName];

        //    //            subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);
        //    //            subRepDoc.Database.Tables["hpt_shed"].SetDataSource(hpt_shed);
        //    //        }

        //    //        if (_cs.SubreportName == "tax")
        //    //        {
        //    //            ReportDocument subRepDoc = _InvPrints_Full_Abstract.Subreports[_cs.SubreportName];
        //    //            subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);

        //    //        }
        //    //        if (_cs.SubreportName == "rptComm")
        //    //        {
        //    //            ReportDocument subRepDoc = _InvPrints_Full_Abstract.Subreports[_cs.SubreportName];
        //    //            subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
        //    //        }
        //    //        if (_cs.SubreportName == "rptWarr")
        //    //        {
        //    //            mst_item1 = mst_item1.DefaultView.ToTable(true);
        //    //            ReportDocument subRepDoc = _InvPrints_Full_Abstract.Subreports[_cs.SubreportName];

        //    //            subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);
        //    //            subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
        //    //            subRepDoc.Database.Tables["MST_ITM"].SetDataSource(mst_item1);
        //    //            subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);

        //    //        }
        //    //        if (_cs.SubreportName == "giftVou")
        //    //        {
        //    //            ReportDocument subRepDoc = _InvPrints_Full_Abstract.Subreports[_cs.SubreportName];
        //    //            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
        //    //        }
        //    //        if (_cs.SubreportName == "loc")
        //    //        {
        //    //            ReportDocument subRepDoc = _InvPrints_Full_Abstract.Subreports[_cs.SubreportName];
        //    //            subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
        //    //        }


        //    //        //if (tblComDate.Rows.Count >0) 
        //    //        //{
        //    //        //  if (_cs.SubreportName == "warrComDate")
        //    //        //  {
        //    //        //      ReportDocument subRepDoc = invReport3.Subreports[_cs.SubreportName];
        //    //        //      subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
        //    //        //  }
        //    //        //}

        //       // }
        //   // }


        //   // this.Text = "Invoice Print";

        //   // crystalReportViewer1.ReportSource = _InvPrints_Full_Abstract;

        //    crystalReportViewer1.RefreshReport();
        //}


        private void InvPrintTax_abstrct()
        {// Tharanga 2017/09/20
            string invNo = default(string);
            string cust_code = default(string);
            string sales_ex_cd = default(string);
            invNo = GlbReportDoc;

            string _pc = "";
            DataTable salesDetails = new DataTable();
            DataTable salesDetails1 = new DataTable();
            salesDetails.Clear();
            salesDetails = CHNLSVC.Sales.GetSalesDetails(invNo, null);
            if (_pc != "BOC")
            {
                salesDetails1 = salesDetails.Select("SAD_TOT_AMT <> 0").CopyToDataTable();
            }
            else
            {
                salesDetails1 = salesDetails;
            }
            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable int_batch = new DataTable();
            DataTable int_batch1 = new DataTable();
            DataTable mst_customer = new DataTable();
            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();
            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = CHNLSVC.General.GetReprintDocCount(GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _DealerinvReport.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            DataRow dr;
            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_NAME", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD1", typeof(string));
            mst_busentity.Columns.Add("MBE_ADD2", typeof(string));
            mst_busentity.Columns.Add("mbe_svat_no", typeof(string));

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
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_NAME", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_SEQ_NO", typeof(decimal));
            sat_hdr1.Columns.Add("SAH_COM", typeof(string));
            sat_hdr1.Columns.Add("SAH_CRE_BY", typeof(string));
            sat_hdr1.Columns.Add("SAH_ANAL_4", typeof(string));
            sat_hdr1.Columns.Add("ESEP_LAST_NAME", typeof(string));


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
            sat_itm.Columns.Add("sad_warr_period", typeof(Int16));

            mst_profit_center.Columns.Add("MPC_ADD_1", typeof(string));
            mst_profit_center.Columns.Add("MPC_ADD_2", typeof(string));
            mst_profit_center.Columns.Add("MPC_CHNL", typeof(string));
            mst_profit_center.Columns.Add("MPC_DESC", typeof(string));
            mst_profit_center.Columns.Add("MPC_COM", typeof(string));
            mst_profit_center.Columns.Add("MPC_CD", typeof(string));
            mst_profit_center.Columns.Add("ESEP_LAST_NAME", typeof(string));


            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));
            mst_item.Columns.Add("MI_BRAND", typeof(string));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_BRAND", typeof(string));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));


            int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));

            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));


            foreach (DataRow row in salesDetails1.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails1.Rows.IndexOf(row);
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_NAME"] = row["MBE_NAME"].ToString();
                    dr["MBE_ADD1"] = row["MBE_ADD1"].ToString();
                    dr["MBE_ADD2"] = row["MBE_ADD2"].ToString();
                    dr["mbe_svat_no"] = row["mbe_svat_no"].ToString();

                    mst_busentity.Rows.Add(dr);

                    MST_LOC = CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["SAH_DEL_LOC"].ToString());

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
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    dr["SAH_ANAL_4"] = row["SAH_ANAL_4"].ToString();
                  
                    sat_hdr1.Rows.Add(dr);

                    _pc = row["SAH_PC"].ToString();
                    cust_code = row["SAH_STRUCTURE_SEQ"].ToString(); //Sanjeewa 2015-08-05 QUOTATION NO
                    sales_ex_cd = row["SAH_SALES_EX_CD"].ToString();
                };
                DataTable dtSuperwiser = CHNLSVC.Sales.GetEmployee(BaseCls.GlbUserComCode, sales_ex_cd);
                if (_pc != "BOC")
                {
                    int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));


                    foreach (DataRow row1 in int_batch1.Rows)
                    {
                        dr = int_batch.NewRow();
                        dr["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                        dr["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                        dr["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                        dr["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                        dr["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                        int_batch.Rows.Add(dr);

                    }

                }

                //if (!(_pc == "BOC" && Convert.ToDecimal(row["SAD_TOT_AMT"].ToString())==0))                    
                //{

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
                dr["sad_warr_period"] = Convert.ToInt16(row["sad_warr_period"].ToString());

                sat_itm.Rows.Add(dr);



                dr = mst_item.NewRow();
                dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                dr["MI_CD"] = row["MI_CD"].ToString();
                dr["MI_BRAND"] = row["MI_BRAND"].ToString();

                mst_item.Rows.Add(dr);

                if (row["MI_CD"].ToString() != "LPHESEJK01")
                {
                    if (row["MI_CD"].ToString() != "LPHEHKHL01")
                    {
                        dr = mst_item1.NewRow();
                        dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                        dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                        dr["MI_CD"] = row["MI_CD"].ToString();
                        dr["MI_BRAND"] = row["MI_BRAND"].ToString();
                        mst_item1.Rows.Add(dr);
                    }
                }

                //}
                if (index == 0)
                {

                    dr = mst_profit_center.NewRow();
                    dr["MPC_ADD_1"] = row["MPC_ADD_1"].ToString();
                    dr["MPC_ADD_2"] = row["MPC_ADD_2"].ToString();
                    dr["MPC_CHNL"] = row["MPC_CHNL"].ToString();
                    dr["MPC_DESC"] = row["MPC_DESC"].ToString();
                    dr["MPC_COM"] = row["MPC_COM"].ToString();
                    dr["MPC_CD"] = row["MPC_CD"].ToString();
                    dr["ESEP_LAST_NAME"] = row["ESEP_LAST_NAME"].ToString();
                  
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

            DataTable deliveredSerials = new DataTable();
            if (_pc != "BOC")
            {
                deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
            }
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

            if (_pc != "BOC")
            {
                foreach (DataRow row in deliveredSerials.Rows)
                {
                    dr = int_hdr.NewRow();
                    dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                    dr["ITH_COM"] = row["ITH_COM"].ToString();
                    dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                    dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                    int_hdr.Rows.Add(dr);

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

                }
            }
            DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
            DataTable hpt_acc = new DataTable();



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


            _InvPrints_Full_Abstract.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _InvPrints_Full_Abstract.Database.Tables["mst_com"].SetDataSource(mst_com);
            _InvPrints_Full_Abstract.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            // _InvPrints_Full_Abstract.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            //_InvPrints_Full_Abstract.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            //    _DealerinvReport.Database.Tables["mst_item"].SetDataSource(mst_item);
            _InvPrints_Full_Abstract.Database.Tables["sec_user"].SetDataSource(sec_user);
            _InvPrints_Full_Abstract.Database.Tables["viw_sales_details"].SetDataSource(salesDetails1);
            //   _InvPrints_Full_Abstract.Database.Tables["mst_item1"].SetDataSource(mst_item1);

            DataTable receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);
            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();
            DataTable sat_receiptGV = new DataTable();
            DataTable sat_receiptitmGV = new DataTable();

            sat_receipt.Columns.Add("SAR_SEQ_NO", typeof(string));
            sat_receipt.Columns.Add("SAR_COM_CD", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_TYPE", typeof(string));
            sat_receipt.Columns.Add("SAR_RECEIPT_NO", typeof(string));


            sat_receiptitm.Columns.Add("SARD_SEQ_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_RECEIPT_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_INV_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_PAY_TP", typeof(string));
            sat_receiptitm.Columns.Add("SARD_REF_NO", typeof(string));
            sat_receiptitm.Columns.Add("SARD_SETTLE_AMT", typeof(decimal));
            if (int_batch.Rows.Count > 0)
            {
                int_batch = int_batch.DefaultView.ToTable(true);
            }
            foreach (DataRow row in receiptDetails.Rows)
            {
                if (row["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                {

                    dr = sat_receipt.NewRow();
                    dr["SAR_SEQ_NO"] = row["SAR_SEQ_NO"].ToString();
                    dr["SAR_COM_CD"] = row["SAR_COM_CD"].ToString();
                    dr["SAR_RECEIPT_TYPE"] = row["SAR_RECEIPT_TYPE"].ToString();
                    dr["SAR_RECEIPT_NO"] = row["SAR_RECEIPT_NO"].ToString();
                    sat_receipt.Rows.Add(dr);



                    dr = sat_receiptitm.NewRow();
                    dr["SARD_SEQ_NO"] = row["SARD_SEQ_NO"].ToString();
                    dr["SARD_RECEIPT_NO"] = row["SARD_RECEIPT_NO"].ToString();
                    dr["SARD_INV_NO"] = row["SARD_INV_NO"].ToString();
                    dr["SARD_PAY_TP"] = row["SARD_PAY_TP"].ToString();
                    dr["SARD_REF_NO"] = row["SARD_REF_NO"].ToString();
                    dr["SARD_SETTLE_AMT"] = Convert.ToDecimal(row["SARD_SETTLE_AMT"].ToString());
                    sat_receiptitm.Rows.Add(dr);
                };
            };

            mst_customer = new DataTable();
            mst_customer = CHNLSVC.Sales.Get_CustomerDetails(cust_code);

         


            foreach (object repOp in _InvPrints_Full_Abstract.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "delLoc")
                    {
                        ReportDocument subRepDoc = _InvPrints_Full_Abstract.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

                    }
                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = _InvPrints_Full_Abstract.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }

                    if (_pc != "BOC")
                    {
                        if (_cs.SubreportName == "rptEngine")
                        {
                            ReportDocument subRepDoc = _InvPrints_Full_Abstract.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                            subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);

                        }
                    };
                    if (_cs.SubreportName == "payments")
                    {
                        ReportDocument subRepDoc = _InvPrints_Full_Abstract.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);

                    }
                    if (_cs.SubreportName == "Del_Detail")
                    {
                        ReportDocument subRepDoc = _InvPrints_Full_Abstract.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_BUSENTITY_GRUP"].SetDataSource(mst_customer);
                    }
                }
            }



            this.Text = "Invoice Print";

            crystalReportViewer1.ReportSource = _InvPrints_Full_Abstract;

            crystalReportViewer1.RefreshReport();




        }

        private void GP_report()
        
        {
            obj.GPSummaryReportNew();
            crystalReportViewer1.ReportSource = obj._GP_Report;
            this.Text = "GP REPORT";
            crystalReportViewer1.RefreshReport();
        }

        private void Bill_Collectio_report()
        {
            //Bill Collection Detail - Wimal 11/09/2018
            obj.BillCollection();
            crystalReportViewer1.ReportSource = obj._billcoll_dtl;
            this.Text = "Bill Collection Detail";
            crystalReportViewer1.RefreshReport();
        }

        private void Bill_Collection_summery_report()
        {
            //Bill Collection Summery - Wimal 11/09/2018
            obj.BillCollection();
            crystalReportViewer1.ReportSource = obj._billcoll_summ;
            this.Text = "Bill Collection Summery";
            crystalReportViewer1.RefreshReport();
        }

        //Tharindu 2018-03-13
        private void Get_sales_sum_report()
        {          
            string _err = "";
            DataTable results = new DataTable();
            DataTable param1 = new DataTable();
            DataRow dr;

            results = CHNLSVC.MsgPortal.getSalesOrderSummaryDetails(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbUserComCode, BaseCls.GlbReportOtherLoc, "", BaseCls.GlbUserID, BaseCls.GlbReportCusId, out _err);


            param1.Columns.Add("fromdate", typeof(string));
            param1.Columns.Add("todate", typeof(string));
            param1.Columns.Add("user", typeof(string));
            param1.Columns.Add("com", typeof(string));

            dr = param1.NewRow();

             dr["fromdate"] = BaseCls.GlbReportFromDate;
             dr["todate"] = BaseCls.GlbReportToDate;
             dr["user"] = BaseCls.GlbUserID;
             dr["com"] = "ABANS AUTO (Pvt)";

             param1.Rows.Add(dr);

             _sosumrpt.Database.Tables["Sales_Summary_Details"].SetDataSource(results);
             _sosumrpt.Database.Tables["paradetails"].SetDataSource(param1);

             this.Text = "Invoice Print";
             crystalReportViewer1.ReportSource = _sosumrpt;
             crystalReportViewer1.RefreshReport();
        }
    }
}
