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
using Microsoft.VisualBasic;
using FF.BusinessObjects;
using FF.Interfaces;
namespace FF.WindowsERPClient.Reports.Sales
{
    public partial class DataMigrate : FF.WindowsERPClient.Base
    {

        public DataMigrate()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();
            BaseCls.GlbReportName = string.Empty;
            _view.GlbReportName = string.Empty;

            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            _view.GlbReportName = "InvoiceHalfPrints.rpt";
            _view.GlbReportDoc = _docNo;
            //  _view.GlbReportDoc = "AAZMD-INREV00002";
            _view.Show();
            _view = null;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            BaseCls.GlbReportName = string.Empty;
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            _view.GlbReportName = "Outward_Docs.rpt";
            _view.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;




        }

        private void button3_Click(object sender, EventArgs e)
        {
            string _docNo = "AAZMD-000014";
            _docNo = Interaction.InputBox("Document No", "Doc No", _docNo, 400, 100);

            ReportViewer _view = new ReportViewer();


            _view.GlbReportName = "InsurancePrint.rpt";
            _view.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string _docNo = "AAZMD-HS00024";
            _docNo = Interaction.InputBox("Document No", "Doc No", _docNo, 400, 100);

            ReportViewer _view = new ReportViewer();

            _view.GlbReportName = "InvoiceHalfPrints.rpt";
            _view.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            _view.GlbReportName = "ReceiptPrints.rpt";
            _view.GlbReportDoc = _docNo;
            _view.GlbReportProfit = BaseCls.GlbUserDefProf;
            _view.Show();
            _view = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();

            _view.GlbReportName = "Inward_Docs.rpt";
            _view.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();

            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);

            _view.GlbReportName = "InvoicePrints.rpt";
            _view.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;
        }

        private void invoice1_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            _view.GlbReportName = "GRANPrints.rpt";
            _view.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();

            _view.GlbReportName = "MRNRepPrints.rpt";
            _view.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            BaseCls.GlbReportName = string.Empty;
            Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
            BaseCls.GlbReportName = "HPReceiptPrint.rpt";
            BaseCls.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();

            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            //_view.GlbReportName = "RCCPrints.rpt";
            //_view.GlbReportDoc = _docNo;
            //_view.Show();
            //_view = null;
            Reports.Sales.ReportViewer _view1 = new Reports.Sales.ReportViewer();
            _view1.GlbReportName = "RCCPrint_New.rpt";
            BaseCls.GlbReportName = "RCCPrint_New.rpt";
            _view1.GlbReportDoc = _docNo;
            BaseCls.GlbReportDoc = _docNo;
            _view1.Show();
            _view1 = null;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);

            _view.GlbReportName = "FixedAssetTransferNotes.rpt";
            _view.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            _view.GlbReportName = "FixedAssetConfirmationNotes.rpt";
            _view.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
            BaseCls.GlbReportName = "ServiceReceiptPrints.rpt";
            BaseCls.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            BaseCls.GlbReportName = "RevertSRN.rpt";
            BaseCls.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);

            _view.GlbReportName = "InvoicePrint_insus.rpt";
            _view.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();


            _view.GlbReportName = "ReceiptPrintDealers.rpt";
            _view.GlbReportDoc = "500ORC0090";
            _view.GlbReportProfit = "500";
            _view.Show();
            _view = null;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            _view.GlbReportName = "DINPrints.rpt";
            _view.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();

            _view.GlbReportName = "InvoiceHalfPrints.rpt";
            _view.GlbReportDoc = "AAZTS-INREV00017";
            //  _view.GlbReportDoc = "AAZMD-INREV00002";
            _view.Show();
            _view = null;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);


            _view.GlbReportName = "InvoicePrintTax.rpt";
            _view.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;

        }

        private void button21_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();


            BaseCls.GlbReportName = "InsuranceCoverNote.rpt";
            BaseCls.GlbReportDoc = "ABN-000885";
            _view.Show();
            _view = null;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);


            _view.GlbReportName = "InvoicePrintTax_insus.rpt";
            _view.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();


            BaseCls.GlbReportName = "PurchaseOrderPrint.rpt";
            BaseCls.GlbReportDoc = "AAL-PUR000057";
            _view.Show();
            _view = null;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();

            BaseCls.GlbReportName = "VoucherPrints.rpt";
            BaseCls.GlbReportDoc = "AAZTS-VOU-00013";
            _view.Show();
            _view = null;
        }


        private void button25_Click(object sender, EventArgs e)
        {
            Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();

            BaseCls.GlbReportName = "ServiceJobCard.rpt";
            BaseCls.GlbReportDoc = "PPR/AAL/1212-0009";
            _view.Show();
            _view = null;
        }

        private void button26_Click(object sender, EventArgs e)
        {

            ReportViewer _view = new ReportViewer();
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            if (BaseCls.GlbUserComCode == "SGL")
            {
                BaseCls.GlbReportName = "sInvoiceDutyFree.rpt";//Add by Chamal 04-Mar-2014
            }
            else
            {
                BaseCls.GlbReportName = "InvoiceDutyFree.rpt";
            }
            _view.GlbReportDoc = _docNo;

            _view.Show();
            _view = null;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            DataTable ECD_VOU_PRINT = new DataTable();
            Base bsObj = new Base(); ;
            clsSalesRep objrep = new clsSalesRep();


            DataTable TMP_ECD_VOU_PRINT = new DataTable();
            TMP_ECD_VOU_PRINT = bsObj.CHNLSVC.Sales.ECD_vouchers_Print("RAMB-GVECD-00003524");
            ECD_VOU_PRINT.Merge(TMP_ECD_VOU_PRINT);

            TMP_ECD_VOU_PRINT = bsObj.CHNLSVC.Sales.ECD_vouchers_Print("RAMB-GVECD-00003525");
            ECD_VOU_PRINT.Merge(TMP_ECD_VOU_PRINT);


            //BaseCls.GlbReportDoc = voucherNo;//_docNo;
            //  objrep.ECDVoucher(ECD_VOU_PRINT);

            ReportViewer _view = new ReportViewer();
            BaseCls.GlbReportName = string.Empty;
            _view.GlbReportName = string.Empty;


            BaseCls.GlbReportName = "EcdVouchar.rpt";
            _view.GlbReportName = "EcdVouchar.rpt";
            BaseCls.GlbReportDataTable = ECD_VOU_PRINT;
            //  _view.GlbReportDoc = "AAZMD-INREV00002";
            _view.Show();
            _view = null;

        }

        private void button28_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();
            BaseCls.GlbReportName = string.Empty;
            _view.GlbReportName = string.Empty;

            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            BaseCls.GlbReportName = "SparePartPrint.rpt";
            BaseCls.GlbReportDoc = _docNo;

            _view.Show();
            _view = null;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();

            string _docNo;
            BaseCls.GlbReportName = string.Empty;
            _view.GlbReportName = string.Empty;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);


            BaseCls.GlbReportName = "WarrantyClaimNote.rpt";
            BaseCls.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();
            BaseCls.GlbReportName = string.Empty;
            _view.GlbReportName = string.Empty;

            string _docNo;
            // _docNo = "ADCR00099";
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            _view.GlbReportName = "InvoiceHalfPrintNew.rpt";
            _view.GlbReportDoc = _docNo;
            //  _view.GlbReportDoc = "AAZMD-INREV00002";
            _view.Show();
            _view = null;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            string _docNo = "RHOM-000823";
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
            BaseCls.GlbReportName = string.Empty;
            _view.GlbReportName = string.Empty;

            BaseCls.GlbReportName = "HpInsuranceAgreement.rpt";
            BaseCls.GlbReportDoc = _docNo;

            _view.Show();
            _view = null;
        }

        private void button32_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();
            BaseCls.GlbReportName = string.Empty;
            _view.GlbReportName = string.Empty;
            _view.GlbReportName = "InvoiceHalfPrints.rpt";
            _view.GlbReportDoc = "RHNL-HS-00004";
            _view.GlbSerial = null;
            _view.GlbWarranty = null;
            _view.Show();
            _view = null;


            ReportViewer _insu = new ReportViewer();
            BaseCls.GlbReportName = string.Empty;
            _insu.GlbReportName = string.Empty;
            _insu.GlbReportName = "InsuPrints.rpt";
            _insu.GlbReportDoc = "RHNL-001296";
            _insu.Show();
            _insu = null;

        }

        //public void print()
        //{
        //    PrintDocument pdoc = null;
        //    PrintDialog pd = new PrintDialog();
        //    pdoc = new PrintDocument();
        //    PrinterSettings ps = new PrinterSettings();
        //    Font font = new Font("Courier New", 15);

        //    //PaperSize psize = new PaperSize("Custom", 100, 200);
        //    //ps.DefaultPageSettings.PaperSize = psize;

        //    pd.Document = pdoc;
        //    //pd.Document.DefaultPageSettings.PaperSize = psize;
        //    //pdoc.DefaultPageSettings.PaperSize.Height =320;
        //    pdoc.DefaultPageSettings.PaperSize.Height = 520;

        //    pdoc.DefaultPageSettings.PaperSize.Width = 820;

        //    pdoc.PrintPage += new PrintPageEventHandler(obj.GiftVoucherPrintReport());

        //    DialogResult result = pd.ShowDialog();
        //    if (result == DialogResult.OK)
        //    {
        //        PrintPreviewDialog pp = new PrintPreviewDialog();
        //        pp.Document = pdoc;
        //        result = pp.ShowDialog();
        //        if (result == DialogResult.OK)
        //        {
        //            pdoc.Print();
        //        }
        //    }

        //}

        private void button33_Click(object sender, EventArgs e)
        {
            //PrintDocument pdoc = null;
            //PrintDialog pd = new PrintDialog();
            //pdoc = new PrintDocument();
            //PrinterSettings ps = new PrinterSettings();
            //Font font = new Font("Courier New", 15);

            //BaseCls.GlbReportProfit = "85";
            //BaseCls.GlbReportBook = 37;
            //BaseCls.GlbReportFromPage = 921;
            //BaseCls.GlbReportToPage = 921;



            //PaperSize psize = new PaperSize("Custom", 100, 200);
            //ps.DefaultPageSettings.PaperSize = psize;

            //pd.Document = pdoc;
            ////pd.Document.DefaultPageSettings.PaperSize = psize;            
            //pdoc.DefaultPageSettings.PaperSize.Height = 520;
            //pdoc.DefaultPageSettings.PaperSize.Width = 820;

            clsInventoryRep obj = new clsInventoryRep();

            BaseCls.GlbReportProfit = "85";
            BaseCls.GlbReportBook = 1637;
            BaseCls.GlbReportFromPage = 81820;
            BaseCls.GlbReportToPage = 81820;

            obj.GiftVoucherPrintReport();

            //pdoc.PrintPage += new PrintPageEventHandler(obj.pdoc_PrintPage);

            //DialogResult result = pd.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    PrintPreviewDialog pp = new PrintPreviewDialog();
            //    pp.Document = pdoc;
            //    result = pp.ShowDialog();
            //    if (result == DialogResult.OK)
            //    {
            //        pdoc.Print();
            //    }
            //}



            //FF.WindowsERPClient.Reports.Inventory.ReportViewerInventory _view = new FF.WindowsERPClient.Reports.Inventory.ReportViewerInventory();

            //BaseCls.GlbReportName = "GvPrint";

            //_view.Show();
            //_view = null;
        }

        private void button34_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();
            BaseCls.GlbReportName = string.Empty;
            _view.GlbReportName = string.Empty;

            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            _view.GlbReportName = "SInvoiceHalfPrints.rpt";
            _view.GlbReportDoc = _docNo;
            //  _view.GlbReportDoc = "AAZMD-INREV00002";
            _view.Show();
            _view = null;
        }

        private void button35_Click(object sender, EventArgs e)
        {


            ReportViewer _view = new ReportViewer();
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            _view.GlbReportName = "SReceiptPrints.rpt";
            _view.GlbReportDoc = _docNo;
            _view.GlbReportProfit = "RKRB";
            _view.Show();
            _view = null;
        }

        private void button36_Click(object sender, EventArgs e)
        {

            string _docNo = "AAZMD-000014";
            _docNo = Interaction.InputBox("Document No", "Doc No", _docNo, 400, 100);

            ReportViewer _view = new ReportViewer();


            _view.GlbReportName = "SInsuPrints.rpt";
            _view.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;
        }

        private void button37_Click(object sender, EventArgs e)
        {
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
            BaseCls.GlbReportName = "SServiceReceiptPrints.rpt";
            BaseCls.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;
        }

        private void button38_Click(object sender, EventArgs e)
        {
            BaseCls.GlbReportName = string.Empty;
            BaseCls.GlbReportName = string.Empty;
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            _view.GlbReportName = "SInward_Docs.rpt";
            BaseCls.GlbReportName = "SInward_Docs.rpt";
            _view.GlbReportDoc = "RNGD+AOD-13-00111";
            _view.Show();
            _view = null;
        }

        private void button39_Click(object sender, EventArgs e)
        {
            BaseCls.GlbReportName = string.Empty;
            BaseCls.GlbReportName = string.Empty;
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            _view.GlbReportName = "SOutward_Docs.rpt";
            BaseCls.GlbReportName = "SOutward_Docs.rpt";
            _view.GlbReportDoc = "RNGD-DO-13-00315";
            _view.Show();
            _view = null;
        }

        private void button40_Click(object sender, EventArgs e)
        {
            BaseCls.GlbReportName = string.Empty;
            BaseCls.GlbReportName = string.Empty;
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            _view.GlbReportName = "SDINPrints.rpt";
            BaseCls.GlbReportName = "SDINPrints.rpt";
            _view.GlbReportDoc = "RNGD-DIN-00003048";
            _view.Show();
            _view = null;
        }

        private void button41_Click(object sender, EventArgs e)
        {
            BaseCls.GlbReportName = string.Empty;
            BaseCls.GlbReportName = string.Empty;
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            _view.GlbReportName = "SGRANPrints.rpt";
            BaseCls.GlbReportName = "SGRANPrints.rpt";
            _view.GlbReportDoc = "RNGD-GRAN-13-03513";
            _view.Show();
            _view = null;
        }

        private void button42_Click(object sender, EventArgs e)
        {
            BaseCls.GlbReportName = string.Empty;
            BaseCls.GlbReportName = string.Empty;
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            _view.GlbReportName = "SMRNRepPrints.rpt";
            BaseCls.GlbReportName = "SMRNRepPrints.rpt";
            _view.GlbReportDoc = txttmpCusCode.Text;
            _view.Show();
            _view = null;
        }

        private void button43_Click(object sender, EventArgs e)
        {
            BaseCls.GlbReportName = string.Empty;
            BaseCls.GlbReportName = string.Empty;
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            _view.GlbReportName = "MRNRepPrints.rpt";
            BaseCls.GlbReportName = "MRNRepPrints.rpt";
            _view.GlbReportDoc = txttmpCusCode.Text;
            _view.Show();
            _view = null;
        }

        private void button44_Click(object sender, EventArgs e)
        {
            BaseCls.GlbReportName = string.Empty;
            BaseCls.GlbReportName = string.Empty;
            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
            _view.GlbReportName = "SRCCPrint_New.rpt";
            BaseCls.GlbReportName = "SRCCPrint_New.rpt";
            _view.GlbReportDoc = "RNGD-RCC-13-0003288";
            BaseCls.GlbReportDoc = "RNGD-RCC-13-0003288";
            _view.Show();
            _view = null;
        }

        private void button45_Click(object sender, EventArgs e)
        {
            BaseCls.GlbReportName = string.Empty;
            BaseCls.GlbReportName = string.Empty;
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            _view.GlbReportName = "Exchange_Docs.rpt";
            BaseCls.GlbReportName = "SExchange_Docs.rpt";
            BaseCls.GlbReportDoc = "MSR02+ADJ-13-00025";
            _view.Show();
            _view = null;
        }

        private void button46_Click(object sender, EventArgs e)
        {
            BaseCls.GlbReportName = string.Empty;
            BaseCls.GlbReportName = string.Empty;
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            _view.GlbReportName = "Exchange_Docs.rpt";
            BaseCls.GlbReportName = "SExchange_Docs.rpt";
            BaseCls.GlbReportDoc = "MSR02-ADJ-13-00026";
            _view.Show();
            _view = null;
        }

        private void button47_Click(object sender, EventArgs e)
        {
            BaseCls.GlbReportName = string.Empty;
            BaseCls.GlbReportName = string.Empty;
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            _view.GlbReportName = "SFixedAssetTransferNotes.rpt";
            BaseCls.GlbReportName = "SFixedAssetTransferNotes.rpt";
            _view.GlbReportDoc = "MSR21-FAT-00000008";
            _view.Show();
            _view = null;
        }

        private void button48_Click(object sender, EventArgs e)
        {
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            BaseCls.GlbReportName = string.Empty;
            BaseCls.GlbReportName = string.Empty;
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            _view.GlbReportName = "RevertSRN.rpt";
            BaseCls.GlbReportName = "RevertSRN.rpt";
            BaseCls.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;
        }

        private void button49_Click(object sender, EventArgs e)
        {
            BaseCls.GlbReportName = string.Empty;
            BaseCls.GlbReportName = string.Empty;
            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
            BaseCls.GlbReportName = "SServiceReceiptPrints.rpt";
            BaseCls.GlbReportDoc = "AAZKT-SVJOB-000005";//- Receipt #
            _view.Show();
            _view = null;
        }

        private void button50_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();
            BaseCls.GlbReportName = string.Empty;
            _view.GlbReportName = string.Empty;

            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            _view.GlbReportName = "InvoicePrints_New1.rpt";
            _view.GlbReportDoc = _docNo;
            //  _view.GlbReportDoc = "AAZMD-INREV00002";
            _view.Show();
            _view = null;
        }

        private void button51_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();
            ReportViewer _view1 = new ReportViewer();
            ReportViewer _view2 = new ReportViewer();

            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            _view.GlbReportName = string.Empty;
            _view.GlbReportDoc = string.Empty;
            _view.GlbReportName = "DealerCreditInvoicePrints.rpt";
            BaseCls.GlbReportName = "DealerCreditInvoicePrints.rpt";
            BaseCls.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;

            _view1.GlbReportName = string.Empty;
            _view1.GlbReportDoc = string.Empty;
            _view1.GlbReportName = "DealerInvoicePrints.rpt";
            BaseCls.GlbReportName = "DealerInvoicePrints.rpt";
            BaseCls.GlbReportDoc = _docNo;
            _view1.Show();
            _view1 = null;

            _view2.GlbReportName = string.Empty;
            _view2.GlbReportDoc = string.Empty;
            _view2.GlbReportName = "InvoicePrints.rpt";
            BaseCls.GlbReportName = "InvoicePrints.rpt";
            BaseCls.GlbReportDoc = _docNo;
            _view2.Show();
            _view2 = null;
        }

        private void button52_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            BaseCls.GlbReportName = "sInvoiceDutyFree.rpt";
            _view.GlbReportDoc = _docNo;

            _view.Show();
            _view = null;
        }

        private void button53_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            _view.GlbReportName = "DealerReceiptPrint.rpt";
            _view.GlbReportDoc = _docNo;
            _view.GlbReportProfit = "500";
            _view.Show();
            _view = null;
        }

        private void button54_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();
            BaseCls.GlbReportName = string.Empty;
            _view.GlbReportName = string.Empty;

            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            _view.GlbReportName = "InvoicePrints_Gold.rpt";
            _view.GlbReportDoc = _docNo;

            _view.Show();
            _view = null;
        }

        private void button55_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            BaseCls.GlbReportName = "InvoiceDutyFreeEdison.rpt";
            _view.GlbReportDoc = _docNo;

            _view.Show();
            _view = null;
        }

        private void button56_Click(object sender, EventArgs e)
        {
            // string CusCode = "AHDR2C0005";
            string CusCode = txttmpCusCode.Text.Trim();
            ucCustomerSalesHistory2.LoadDetails(CusCode);
            panel1.Visible = true;



        }

        private void btnVoucherSearch_Click(object sender, EventArgs e)
        {

            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
            clsSalesRep objsales = new clsSalesRep();
            BaseCls.GlbReportName = "CheckPrinting1.rpt";
            BaseCls.GlbReportHeading = "Check Printing Voucher";
            //BaseCls.GlbReportDoc = "VOU-10002";
            List<string> _vouList = new List<string>();

            string[] voucherNo = new string[3];
            voucherNo[0] = "VOU-10004";
            voucherNo[1] = "VOU-10016";
            voucherNo[2] = "VOU-10018";
            for (int i = 0; i < voucherNo.Length; i++)
            {
                string vou = voucherNo[i];
                _vouList.Add(vou);
            }
            objsales.getVoudets(_vouList);



            _view.Show();
            _view = null;



        }

        private void button57_Click(object sender, EventArgs e)
        {
            string _docNo;
            _docNo = Interaction.InputBox("Reference No", "Doc No", "", 400, 100);

            BaseCls.GlbReportDoc = _docNo;
            Reconciliation.clsRecon obj = new Reconciliation.clsRecon();
            //Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
            obj.VehRegAppReport1();

            //BaseCls.GlbReportName = "Vehicle_Reg_Form.rpt";
            //BaseCls.GlbReportHeading = "Veh reg print";
            //BaseCls.GlbReportDoc = _docNo;
            //_view.Show();
            //_view = null;

        }

        private void button58_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();
            BaseCls.GlbReportName = string.Empty;
            _view.GlbReportName = string.Empty;

            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            _view.GlbReportName = "VehicleRegistrationSlip.rpt";
            BaseCls.GlbReportName = "VehicleRegistrationSlip.rpt";
            _view.GlbReportDoc = _docNo;
            BaseCls.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;
        }

        private void button59_Click(object sender, EventArgs e)
        {
            string _docNo;
            _docNo = Interaction.InputBox("Reference No", "Doc No", "", 400, 100);

            BaseCls.GlbReportDoc = _docNo;
            Reconciliation.clsRecon obj = new Reconciliation.clsRecon();
            obj.VehicleRegistrationSlip();
        }

        private void button62_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();

            _view.GlbReportName = string.Empty;
            _view.GlbReportDoc = string.Empty;
            _view.GlbReportName = "DealerCreditInvoicePrints.rpt";
            BaseCls.GlbReportName = "DealerCreditInvoicePrints.rpt";
            BaseCls.GlbReportDoc = txttmpCusCode.Text;
            _view.GlbReportDoc = txttmpCusCode.Text;
            _view.Show();
            _view = null;
        }

        private void button61_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();

            _view.GlbReportName = string.Empty;
            _view.GlbReportDoc = string.Empty;
            _view.GlbReportName = "DealerInvoicePrints.rpt";
            BaseCls.GlbReportName = "DealerInvoicePrints.rpt";
            BaseCls.GlbReportDoc = txttmpCusCode.Text;
            _view.GlbReportDoc = txttmpCusCode.Text;
            _view.Show();
            _view = null;
        }

        private void button60_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();

            _view.GlbReportName = string.Empty;
            _view.GlbReportDoc = string.Empty;
            _view.GlbReportName = "InvoicePrints.rpt";
            BaseCls.GlbReportName = "InvoicePrints.rpt";
            BaseCls.GlbReportDoc = txttmpCusCode.Text;
            _view.GlbReportDoc = txttmpCusCode.Text;
            _view.Show();
            _view = null;
        }

        private void button63_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();

            _view.GlbReportName = string.Empty;
            _view.GlbReportDoc = string.Empty;
            _view.GlbReportName = "InvoicePrint_insus.rpt";
            BaseCls.GlbReportName = "InvoicePrint_insus.rpt";
            BaseCls.GlbReportDoc = txttmpCusCode.Text;
            _view.GlbReportDoc = txttmpCusCode.Text;
            _view.Show();
            _view = null;
        }

        private void button64_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();

            _view.GlbReportName = string.Empty;
            _view.GlbReportDoc = string.Empty;
            _view.GlbReportName = "InvoicePrintTax.rpt";
            BaseCls.GlbReportName = "InvoicePrintTax.rpt";
            BaseCls.GlbReportDoc = txttmpCusCode.Text;
            _view.GlbReportDoc = txttmpCusCode.Text;
            _view.Show();
            _view = null;
        }

        private void button65_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();

            _view.GlbReportName = string.Empty;
            _view.GlbReportDoc = string.Empty;
            _view.GlbReportName = "InvoicePrintTax_insus.rpt";
            BaseCls.GlbReportName = "InvoicePrintTax_insus.rpt";
            BaseCls.GlbReportDoc = txttmpCusCode.Text;
            _view.GlbReportDoc = txttmpCusCode.Text;
            _view.Show();
            _view = null;
        }

        private void button66_Click(object sender, EventArgs e)
        {
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();

            _view.GlbReportName = string.Empty;
            _view.GlbReportDoc = string.Empty;
            _view.GlbReportName = "SOutward_Docs.rpt";
            BaseCls.GlbReportName = "SOutward_Docs.rpt";
            BaseCls.GlbReportDoc = txttmpCusCode.Text;
            _view.GlbReportDoc = txttmpCusCode.Text;
            _view.Show();
            _view = null;
        }

        private void button67_Click(object sender, EventArgs e)
        {
            DataTable _jobs = new DataTable();
            _jobs = CHNLSVC.CustService.get_BulkJobPrint(BaseCls.GlbUserComCode);

            BaseCls.GlbReportTp = "WW";
            string _repname = string.Empty;
            string _papersize = string.Empty;
            CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
            if (!(_repname == null || _repname == "")) { GlbReportName = _repname; BaseCls.GlbReportName = _repname; BaseCls.GlbReportPaperSize = _papersize; }

            if (BaseCls.GlbReportName == null || BaseCls.GlbReportName == "")
            {
                MessageBox.Show("Report is not setup. Contact IT Department...\n", "Report not Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (_jobs.Rows.Count > 0)
                {
                    foreach (DataRow drow in _jobs.Rows)
                    {
                        Reports.Service.ReportViewerSVC _viewJob = new Reports.Service.ReportViewerSVC();
                        BaseCls.GlbReportCompCode = BaseCls.GlbUserComCode;
                        BaseCls.GlbReportDoc = drow["job_no"].ToString();

                        Reports.Service.clsServiceRep objSvc = new Reports.Service.clsServiceRep();
                        BaseCls.GlbReportDirectPrint = 0;
                        objSvc.ServiceJobCardPrint();
                        Reports.Sales.ReportViewer _viewsale = new Reports.Sales.ReportViewer();
                        objSvc._JobCardWPh.PrintOptions.PrinterName = _viewsale.GetDefaultPrinter();
                        //MessageBox.Show("Please check whether printer load the Job documents and press ok!\n", "Load Documents.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        objSvc._JobCardWPh.PrintToPrinter(1, false, 0, 0);

                        label1.Text = BaseCls.GlbReportDoc + " Line :" + drow["line_no"].ToString();
                        //DoEvents();
                        //int x = CHNLSVC.CustService.get_UpdateBulkJobPrint(BaseCls.GlbReportDoc);
                    }
                }
            }

        }

        private void button68_Click(object sender, EventArgs e)
        {
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();

            _view.GlbReportName = string.Empty;
            _view.GlbReportDoc = string.Empty;
            _view.GlbReportName = "Inward_Docs_ack.rpt";
            BaseCls.GlbReportName = "Inward_Docs_ack.rpt";
            BaseCls.GlbReportDoc = txttmpCusCode.Text;
            _view.GlbReportDoc = txttmpCusCode.Text;
            _view.Show();
            _view = null;
        }

        private void button69_Click(object sender, EventArgs e)
        {
            if (txttmpCusCode.Text == "")
            {
                MessageBox.Show("Invalid Account", "xxx", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                CHNLSVC.Sales.generate_Voucher(txttmpCusCode.Text);
                MessageBox.Show("Voucher Generated", "xxx", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txttmpCusCode.Text = "";
            }
        }

        private void btnSQL_Click(object sender, EventArgs e)
        {
            string _err = string.Empty;
            string _filePath = string.Empty;
            string _locCode = "";
            string _toLocCode = "";
            Int32 affected_rows = 0;
            string _docTp = "";
            Int32 _baseItmLine = 1;
            string _binCode = "";
            string _tobinCode = "";
            string COM_OUT = "DO";
            string _suplier_Cde = string.Empty;
            txtSQL.Text = "";


            _locCode = txtLoc.Text;
            _toLocCode = txtToLoc.Text;
            _binCode = CHNLSVC.Inventory.Get_default_binCD(txtFromComp.Text, _locCode);

            MasterAutoNumber _inventoryAuto = new MasterAutoNumber();
            _inventoryAuto.Aut_cate_cd = _locCode;
            _inventoryAuto.Aut_cate_tp = "LOC";
            _inventoryAuto.Aut_direction = null;
            _inventoryAuto.Aut_modify_dt = null;
            _inventoryAuto.Aut_moduleid = string.Empty;
            _inventoryAuto.Aut_start_char = string.Empty;
            _inventoryAuto.Aut_modify_dt = null;
            _inventoryAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;

            MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
            _invoiceAuto.Aut_cate_cd = _locCode;
            _invoiceAuto.Aut_cate_tp = "PRO";
            _invoiceAuto.Aut_direction = null;
            _invoiceAuto.Aut_modify_dt = null;
            _invoiceAuto.Aut_moduleid = "CRED";
            _invoiceAuto.Aut_start_char = "CRED";
            _invoiceAuto.Aut_modify_dt = null;
            _invoiceAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;

            InventoryHeader _inventoryHeader = new InventoryHeader();
            _inventoryHeader.Ith_acc_no = string.Empty;
            _inventoryHeader.Ith_anal_1 = string.Empty;
            _inventoryHeader.Ith_anal_10 = true;
            _inventoryHeader.Ith_anal_11 = false;
            _inventoryHeader.Ith_anal_12 = false;
            _inventoryHeader.Ith_anal_2 = string.Empty;
            _inventoryHeader.Ith_anal_3 = string.Empty;
            _inventoryHeader.Ith_anal_4 = string.Empty;
            _inventoryHeader.Ith_anal_5 = string.Empty;
            _inventoryHeader.Ith_anal_6 = 0;
            _inventoryHeader.Ith_anal_7 = 0;
            _inventoryHeader.Ith_anal_8 = Convert.ToDateTime(txtDate.Text).Date;
            _inventoryHeader.Ith_anal_9 = Convert.ToDateTime(txtDate.Text).Date;
            _inventoryHeader.Ith_bus_entity = string.Empty;
            _inventoryHeader.Ith_cate_tp = string.Empty;
            _inventoryHeader.Ith_channel = string.Empty;
            _inventoryHeader.Ith_com = txtToComp.Text;
            _inventoryHeader.Ith_com_docno = string.Empty;
            _inventoryHeader.Ith_cre_by = BaseCls.GlbUserID;
            _inventoryHeader.Ith_cre_when = DateTime.Now.Date;
            _inventoryHeader.Ith_del_add1 = string.Empty;
            _inventoryHeader.Ith_del_add2 = string.Empty;
            _inventoryHeader.Ith_del_code = string.Empty;
            _inventoryHeader.Ith_del_party = string.Empty;
            _inventoryHeader.Ith_del_town = string.Empty;
            _inventoryHeader.Ith_direct = false;
            _inventoryHeader.Ith_doc_date = Convert.ToDateTime(txtDate.Text);
            _inventoryHeader.Ith_doc_no = string.Empty;
            _inventoryHeader.Ith_doc_tp = string.Empty;
            _inventoryHeader.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Date.Year;
            _inventoryHeader.Ith_entry_no = string.Empty;
            _inventoryHeader.Ith_entry_tp = string.Empty;
            _inventoryHeader.Ith_git_close = false;
            _inventoryHeader.Ith_git_close_date = Convert.ToDateTime(txtDate.Text).Date;
            _inventoryHeader.Ith_git_close_doc = string.Empty;
            _inventoryHeader.Ith_is_manual = false;
            _inventoryHeader.Ith_isprinted = false;
            _inventoryHeader.Ith_job_no = string.Empty;
            _inventoryHeader.Ith_loading_point = string.Empty;
            _inventoryHeader.Ith_loading_user = string.Empty;
            _inventoryHeader.Ith_loc = _locCode;
            _inventoryHeader.Ith_manual_ref = "";
            _inventoryHeader.Ith_mod_by = BaseCls.GlbUserID;
            _inventoryHeader.Ith_mod_when = DateTime.Now.Date;
            _inventoryHeader.Ith_noofcopies = 0;
            _inventoryHeader.Ith_oth_loc = "";
            _inventoryHeader.Ith_oth_docno = string.Empty;
            _inventoryHeader.Ith_remarks = "MIG";
            _inventoryHeader.Ith_sbu = string.Empty;
            //_inventoryHeader.Ith_seq_no = 0; removed by Chamal 12-05-2013
            _inventoryHeader.Ith_session_id = BaseCls.GlbUserSessionID;
            _inventoryHeader.Ith_stus = "A";
            _inventoryHeader.Ith_sub_tp = string.Empty;
            _inventoryHeader.Ith_vehi_no = string.Empty;
            _inventoryHeader.Ith_oth_com = txtToComp.Text;
            _inventoryHeader.Ith_anal_1 = "0";
            _inventoryHeader.Ith_anal_2 = string.Empty;

            InvoiceHeader _invoiceheader = new InvoiceHeader();
            _invoiceheader.Sah_com = txtFromComp.Text;
            _invoiceheader.Sah_cre_by = BaseCls.GlbUserID;
            _invoiceheader.Sah_cre_when = DateTime.Now;
            _invoiceheader.Sah_currency = "LKR";
            _invoiceheader.Sah_cus_add1 = string.Empty;
            _invoiceheader.Sah_cus_add2 = string.Empty;
            _invoiceheader.Sah_cus_cd = "CASH";
            _invoiceheader.Sah_cus_name = string.Empty;
            _invoiceheader.Sah_d_cust_add1 = string.Empty;
            _invoiceheader.Sah_d_cust_add2 = string.Empty;
            _invoiceheader.Sah_d_cust_cd = "CASH";
            _invoiceheader.Sah_direct = true;
            _invoiceheader.Sah_dt = Convert.ToDateTime(txtDate.Text);
            _invoiceheader.Sah_epf_rt = 0;
            _invoiceheader.Sah_esd_rt = 0;
            _invoiceheader.Sah_ex_rt = 1;
            _invoiceheader.Sah_inv_no = "NA";
            _invoiceheader.Sah_inv_sub_tp = "SA"; //(Old Value - CS)Change value as per the Dilanda request ::Chamal De Silva 18-08-2012 16:30
            _invoiceheader.Sah_inv_tp = "INTR"; //(Old Value - CRED)Change value as per the Dilanda request ::Chamal De Silva 18-08-2012 16:30
            _invoiceheader.Sah_is_acc_upload = false;
            _invoiceheader.Sah_man_cd = string.Empty;
            _invoiceheader.Sah_man_ref = string.Empty;
            _invoiceheader.Sah_manual = false;
            _invoiceheader.Sah_mod_by = BaseCls.GlbUserID;
            _invoiceheader.Sah_mod_when = DateTime.Now;
            _invoiceheader.Sah_pc = _locCode;
            _invoiceheader.Sah_pdi_req = 0;
            _invoiceheader.Sah_ref_doc = string.Empty;
            _invoiceheader.Sah_remarks = string.Empty;
            _invoiceheader.Sah_sales_chn_cd = string.Empty;
            _invoiceheader.Sah_sales_chn_man = string.Empty;
            _invoiceheader.Sah_sales_ex_cd = string.Empty;
            _invoiceheader.Sah_sales_region_cd = string.Empty;
            _invoiceheader.Sah_sales_region_man = string.Empty;
            _invoiceheader.Sah_sales_sbu_cd = string.Empty;
            _invoiceheader.Sah_sales_sbu_man = string.Empty;
            _invoiceheader.Sah_sales_str_cd = string.Empty;
            _invoiceheader.Sah_sales_zone_cd = string.Empty;
            _invoiceheader.Sah_sales_zone_man = string.Empty;
            _invoiceheader.Sah_seq_no = 1;
            _invoiceheader.Sah_session_id = BaseCls.GlbUserSessionID;
            _invoiceheader.Sah_structure_seq = string.Empty;
            _invoiceheader.Sah_stus = "D";
            _invoiceheader.Sah_town_cd = string.Empty;
            _invoiceheader.Sah_tp = "INV";
            _invoiceheader.Sah_wht_rt = 0;

            //get all available serials
            List<ReptPickSerials> _reptPickSerial_ = new List<ReptPickSerials>();
            _reptPickSerial_ = null; //CHNLSVC.Inventory.GetAvailableSerials(txtFromComp.Text, _locCode);

            //add temp pick header
            Int32 generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "DO", 1, txtFromComp.Text);//direction always =1 for this method

            //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = "DO";
            RPH.Tuh_cre_dt = DateTime.Today;
            RPH.Tuh_ischek_itmstus = true;
            RPH.Tuh_ischek_reqqty = true;
            RPH.Tuh_ischek_simitm = true;
            RPH.Tuh_session_id = BaseCls.GlbUserSessionID;
            RPH.Tuh_usr_com = txtFromComp.Text;
            RPH.Tuh_usr_id = BaseCls.GlbUserID;
            RPH.Tuh_usrseq_no = generated_seq;

            RPH.Tuh_direct = false;
            RPH.Tuh_doc_no = "-1";

            //write entry to TEMP_PICK_HDR
            affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);

            //check whether the serial should be DO
            foreach (ReptPickSerials _ser in _reptPickSerial_)
            {
                _docTp = DocumentTypeDecider(_ser.Tus_ser_id);

                //add to temp pick ser
                if (_docTp == "DO")
                {
                    #region add
                    MasterItem msitem = CHNLSVC.Inventory.GetItem(txtFromComp.Text, _ser.Tus_itm_cd);

                    if (msitem.Mi_is_ser1 != -1)
                    //change msitem.Mi_is_ser1 == true
                    {
                        int rowCount = 0;

                        Int32 serID = _ser.Tus_ser_id;

                        string binCode = _ser.Tus_bin;
                        ReptPickSerials _reptPick_Serial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(txtFromComp.Text, _locCode, binCode, _ser.Tus_itm_cd, serID);
                        //Update_inrser_INS_AVAILABLE
                        Boolean update_inr_ser = false;
                        //   update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(txtFromComp.Text, _locCode, _ser.Tus_itm_cd, serID, -1);

                        _reptPick_Serial_.Tus_cre_by = BaseCls.GlbUserID;
                        _reptPick_Serial_.Tus_usrseq_no = generated_seq;
                        _reptPick_Serial_.Tus_cre_by = BaseCls.GlbUserID;
                        _reptPick_Serial_.Tus_base_doc_no = "-1";
                        _reptPick_Serial_.Tus_base_itm_line = 1;
                        _reptPick_Serial_.Tus_itm_desc = msitem.Mi_shortdesc;
                        _reptPick_Serial_.Tus_itm_model = msitem.Mi_model;
                        _reptPick_Serial_.Tus_job_no = "";
                        _reptPick_Serial_.Tus_pgs_prefix = _ser.Tus_itm_cd;
                        _reptPick_Serial_.Tus_job_line = 0;
                        _reptPick_Serial_.Tus_new_remarks = "DO";
                        //enter row into TEMP_PICK_SER
                        affected_rows = -1;

                        affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPick_Serial_, null);

                        rowCount++;

                    }
                    else
                    {
                        ReptPickSerials _reptPick_Serial_ = new ReptPickSerials();
                        _reptPick_Serial_.Tus_cre_by = BaseCls.GlbUserID;
                        _reptPick_Serial_.Tus_usrseq_no = generated_seq;
                        _reptPick_Serial_.Tus_cre_by = BaseCls.GlbUserID;
                        _reptPick_Serial_.Tus_base_doc_no = "-1";
                        _reptPick_Serial_.Tus_base_itm_line = 0;
                        _reptPick_Serial_.Tus_itm_desc = msitem.Mi_shortdesc;
                        _reptPick_Serial_.Tus_itm_model = msitem.Mi_model;
                        _reptPick_Serial_.Tus_com = txtFromComp.Text;
                        _reptPick_Serial_.Tus_loc = _locCode;
                        _reptPick_Serial_.Tus_bin = _binCode;
                        _reptPick_Serial_.Tus_itm_cd = _ser.Tus_itm_cd;
                        _reptPick_Serial_.Tus_itm_stus = _ser.Tus_itm_stus;
                        _reptPick_Serial_.Tus_qty = _ser.Tus_qty;
                        _reptPick_Serial_.Tus_ser_1 = "N/A";
                        _reptPick_Serial_.Tus_ser_2 = "N/A";
                        _reptPick_Serial_.Tus_ser_3 = "N/A";
                        _reptPick_Serial_.Tus_ser_4 = "N/A";
                        _reptPick_Serial_.Tus_ser_id = 0;
                        _reptPick_Serial_.Tus_serial_id = "0";
                        _reptPick_Serial_.Tus_job_no = "";
                        _reptPick_Serial_.Tus_pgs_prefix = _ser.Tus_itm_cd;
                        _reptPick_Serial_.Tus_job_line = 0;
                        _reptPick_Serial_.Tus_new_remarks = "DO";

                        //enter row into TEMP_PICK_SER
                        affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPick_Serial_, null);
                    }
                    #endregion
                }
            }
            //get saved temp pick ser
            List<ReptPickSerials> _reptPickSerials = new List<ReptPickSerials>();
            _reptPickSerials = CHNLSVC.Inventory.GetAllScanSerialsList(txtFromComp.Text, _locCode, BaseCls.GlbUserID, generated_seq, COM_OUT);

            if (_reptPickSerials != null)
            {
                //update base item line
                var _scanItems = _reptPickSerials.GroupBy(x => new { x.Tus_itm_cd }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                foreach (var itm in _scanItems)
                {
                    foreach (var item_ in _reptPickSerials.Where(w => w.Tus_itm_cd == itm.Peo.Tus_itm_cd))
                    {
                        item_.Tus_base_itm_line = _baseItmLine;
                        _baseItmLine = _baseItmLine + 1;
                    }

                }


                string _message = string.Empty;
                string _genInventoryDoc = string.Empty;
                string _genSalesDoc = string.Empty;

                Int32 _effect = -1;

                if (_reptPickSerials.Count != 0)
                {
                    _effect = CHNLSVC.Inventory.SaveCommonOutWardEntry(txtFromComp.Text, _locCode, txtToComp.Text, null, _inventoryHeader, _inventoryAuto, _invoiceheader, _invoiceAuto, _reptPickSerials, null, out _message, out _genSalesDoc, out _genInventoryDoc, false, false);

                    if (_effect == -1)
                    {
                        this.Cursor = Cursors.Default;
                        if (_message.Contains("EMS.CHK_INLFREEQTY"))
                            txtSQL.Text = txtSQL.Text + "\n" + _locCode + " - There is no free stock balance available";
                        else if (_message.Contains("EMS.CHK_INBFREEQTY"))
                            txtSQL.Text = txtSQL.Text + "\n" + _locCode + " - There is no free stock balance available";
                        else
                            txtSQL.Text = txtSQL.Text + "\n" + _locCode + " - Please check the issues of " + _message;
                    }
                    else
                    {
                        txtSQL.Text = txtSQL.Text + "\n" + _locCode + " - DO Completed - " + _genInventoryDoc;

                        //Start GRN process--------------
                        string _msg = "";
                        string _ponumber = "";

                        DataTable _adminT = CHNLSVC.Inventory.Get_location_by_code(txtFromComp.Text, _locCode);

                        List<InterCompanySalesParameter> _priceParam = CHNLSVC.Sales.GetInterCompanyParameter(_adminT.Rows[0].Field<string>("ml_ope_cd"), txtFromComp.Text, string.Empty, txtToComp.Text, string.Empty);
                        _tobinCode = CHNLSVC.Inventory.Get_default_binCD(txtToComp.Text, _toLocCode);

                        //get PO number
                        InventoryHeader _invH = CHNLSVC.Inventory.Get_Int_Hdr(_genInventoryDoc);
                        _ponumber = _invH.Ith_sub_docno;
                        //get supplier
                        PurchaseOrder _poHdr = CHNLSVC.Inventory.GetPOHeader(txtToComp.Text, _ponumber, "L");
                        _suplier_Cde = _poHdr.Poh_supp.ToString();

                        CHNLSVC.Inventory.GetSCMDeliveryOrder(txtDate.Value.Date, txtToComp.Text, _toLocCode, _tobinCode, _priceParam[0].Sritc_sup, _genInventoryDoc, _ponumber, BaseCls.GlbUserID, out _msg);
                        if (!string.IsNullOrEmpty(_msg))
                            txtSQL.Text = txtSQL.Text + "\n" + _toLocCode + " (GRN) - " + _msg;
                        else
                        {
                            List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                            InventoryHeader invHdr = new InventoryHeader();
                            string documntNo = "";
                            Int32 result = -99;

                            Int32 _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", txtToComp.Text, _ponumber, 1);
                            reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(txtToComp.Text, _toLocCode, BaseCls.GlbUserID, _userSeqNo, "GRN");
                            if (reptPickSerialsList == null)
                            {
                                txtSQL.Text = txtSQL.Text + "\n" + _genInventoryDoc + " - No items found for GRN";
                            }
                            else
                            {
                                var _dup = reptPickSerialsList.Where(x => x.Tus_ser_1 != "N/A").Select(y => y.Tus_ser_id).ToList();

                                string _duplicateItems = string.Empty;
                                bool _isDuplicate = false;
                                if (_dup != null)
                                    if (_dup.Count > 0)
                                        foreach (Int32 _id in _dup)
                                        {
                                            Int32 _counts = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
                                            if (_counts > 1)
                                            {
                                                _isDuplicate = true;
                                                var _item = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
                                                foreach (string _str in _item)
                                                    if (string.IsNullOrEmpty(_duplicateItems))
                                                        _duplicateItems = _str;
                                                    else
                                                        _duplicateItems += "," + _str;
                                            }
                                        }
                                if (_isDuplicate)
                                {
                                    Cursor.Current = Cursors.Default;
                                    //MessageBox.Show("Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtSQL.Text = txtSQL.Text + "\n" + _toLocCode + " - Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems;
                                    //return;
                                }


                                reptPickSerialsList.ForEach(i => { i.Tus_exist_grncom = txtToComp.Text; i.Tus_exist_grndt = txtDate.Value.Date; i.Tus_exist_supp = _suplier_Cde; i.Tus_orig_grncom = txtToComp.Text; i.Tus_orig_grndt = txtDate.Value.Date; i.Tus_orig_supp = _suplier_Cde; });

                                List<PurchaseOrderDelivery> _purchaseOrderDeliveryList = new List<PurchaseOrderDelivery>();
                                _purchaseOrderDeliveryList = CHNLSVC.Inventory.GetConsignmentItemDetails(txtToComp.Text, _ponumber, _toLocCode);

                                if (reptPickSerialsList != null)
                                {
                                    var _scanItems1 = reptPickSerialsList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                                    foreach (var itm in _scanItems1)
                                    {
                                        foreach (PurchaseOrderDelivery _invItem in _purchaseOrderDeliveryList)
                                        {
                                            if (itm.Peo.Tus_itm_cd == _invItem.MasterItem.Mi_cd && itm.Peo.Tus_itm_line == _invItem.Podi_line_no)
                                            {
                                                _invItem.Actual_qty = itm.theCount; // Current scan qty
                                            }
                                        }
                                    }
                                }

                                InventoryHeader _invHeader = new InventoryHeader();

                                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(txtToComp.Text, _toLocCode);
                                foreach (DataRow r in dt_location.Rows)
                                {
                                    // Get the value of the wanted column and cast it to string
                                    _invHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                                    if (System.DBNull.Value != r["ML_CATE_2"]) _invHeader.Ith_channel = (string)r["ML_CATE_2"]; else _invHeader.Ith_channel = string.Empty;
                                }

                                _invHeader.Ith_com = txtToComp.Text;
                                _invHeader.Ith_loc = _toLocCode;
                                _invHeader.Ith_doc_date = txtDate.Value.Date;
                                _invHeader.Ith_doc_year = txtDate.Value.Date.Year;
                                _invHeader.Ith_direct = true;
                                _invHeader.Ith_doc_tp = "GRN";

                                _invHeader.Ith_cate_tp = "LOCAL";
                                _invHeader.Ith_sub_tp = "LOCAL";

                                _invHeader.Ith_bus_entity = _suplier_Cde;
                                _invHeader.Ith_is_manual = false;
                                _invHeader.Ith_manual_ref = "";
                                _invHeader.Ith_remarks = "MIG";
                                _invHeader.Ith_stus = "A";
                                _invHeader.Ith_cre_by = BaseCls.GlbUserID;
                                _invHeader.Ith_cre_when = DateTime.Now;
                                _invHeader.Ith_mod_by = BaseCls.GlbUserID;
                                _invHeader.Ith_mod_when = DateTime.Now;
                                _invHeader.Ith_session_id = BaseCls.GlbUserSessionID;
                                _invHeader.Ith_oth_docno = _ponumber;


                                MasterAutoNumber _masterAuto = new MasterAutoNumber();
                                _masterAuto.Aut_cate_cd = _toLocCode; ;
                                _masterAuto.Aut_cate_tp = "LOC";
                                _masterAuto.Aut_direction = null;
                                _masterAuto.Aut_modify_dt = null;
                                _masterAuto.Aut_moduleid = "GRN";
                                _masterAuto.Aut_number = 0;
                                _masterAuto.Aut_start_char = "GRN";
                                _masterAuto.Aut_year = _invHeader.Ith_doc_date.Date.Year;

                                //Add by Chamal 23-May-2014
                                int _updateDate = CHNLSVC.Inventory.ChangeScanSerialDocDate(_invHeader.Ith_oth_com, _invHeader.Ith_com, _invHeader.Ith_doc_tp, _ponumber, _invHeader.Ith_doc_date.Date, BaseCls.GlbUserID);
                                reptPickSerialsList.ForEach(x => x.Tus_doc_dt = _invHeader.Ith_doc_date.Date);
                                if (_invHeader.Ith_doc_tp == "GRN")
                                {
                                    if (_invHeader.Ith_oth_com == "ABL" && _invHeader.Ith_com == "LRP")
                                    {
                                        reptPickSerialsList.ForEach(x => x.Tus_orig_grndt = _invHeader.Ith_doc_date.Date);
                                        reptPickSerialsList.ForEach(x => x.Tus_exist_grndt = _invHeader.Ith_doc_date.Date);
                                    }
                                    if (_invHeader.Ith_oth_com == "SGL" && _invHeader.Ith_com == "SGD")
                                    {
                                        reptPickSerialsList.ForEach(x => x.Tus_orig_grndt = _invHeader.Ith_doc_date.Date);
                                        reptPickSerialsList.ForEach(x => x.Tus_exist_grndt = _invHeader.Ith_doc_date.Date);
                                    }
                                }

                                result = CHNLSVC.Inventory.ConsignmentReceipt(_invHeader, reptPickSerialsList, null, _masterAuto, _purchaseOrderDeliveryList, out documntNo);
                                if (result != -99 && result >= 0)
                                {
                                    txtSQL.Text = txtSQL.Text + "\n" + _toLocCode + " - GRN Completed - " + documntNo;
                                }
                            }

                        }

                        //End GRN
                    }
                }
                else
                {
                    txtSQL.Text = txtSQL.Text + "\n" + _locCode + " - No Balance to transfer";
                }
            }
            else
            {
                txtSQL.Text = txtSQL.Text + "\n" + _locCode + " - No Balance to transfer";
            }

        }

        private string DocumentTypeDecider(Int32 _serialID)
        {
            InventorySerialMaster _master = CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(_serialID);
            string _userCompany = txtFromComp.Text;//LRP
            string _selectCompany = txtToComp.Text.ToString();//ABL
            string _itemReceiveCompany = _master.Irsm_rec_com;

            string _comOutDocType = "NON";

            if (_userCompany == _selectCompany)
                _comOutDocType = "AOD-OUT";
            else if (_selectCompany == _itemReceiveCompany)
                _comOutDocType = "PRN";
            else if (_selectCompany != _itemReceiveCompany)
                _comOutDocType = "DO";

            if (_master.Irsm_itm_stus == "CONS")
                _comOutDocType = "AOD-OUT"; //Add by Chamal 06-May-2014
            return _comOutDocType;
        }

        private void button70_Click(object sender, EventArgs e)
        {
            string _err = string.Empty;
            string _filePath = string.Empty;
            string _locCode = "";

            Int32 affected_rows = 0;
            string _docTp = "";
            Int32 _baseItmLine = 1;
            string _binCode = "";
            string COM_OUT = "PRN";
            string _toLocCode = "";


            _locCode = txtLoc.Text;
            _toLocCode = txtToLoc.Text;
            _binCode = CHNLSVC.Inventory.Get_default_binCD(txtFromComp.Text, _locCode);

            MasterAutoNumber _inventoryAuto = new MasterAutoNumber();
            _inventoryAuto.Aut_cate_cd = _locCode;
            _inventoryAuto.Aut_cate_tp = "LOC";
            _inventoryAuto.Aut_direction = null;
            _inventoryAuto.Aut_modify_dt = null;
            _inventoryAuto.Aut_moduleid = string.Empty;
            _inventoryAuto.Aut_start_char = string.Empty;
            _inventoryAuto.Aut_modify_dt = null;
            _inventoryAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;

            MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
            _invoiceAuto.Aut_cate_cd = _locCode;
            _invoiceAuto.Aut_cate_tp = "PRO";
            _invoiceAuto.Aut_direction = null;
            _invoiceAuto.Aut_modify_dt = null;
            _invoiceAuto.Aut_moduleid = "CRED";
            _invoiceAuto.Aut_start_char = "CRED";
            _invoiceAuto.Aut_modify_dt = null;
            _invoiceAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;

            InventoryHeader _inventoryHeader = new InventoryHeader();
            _inventoryHeader.Ith_acc_no = string.Empty;
            _inventoryHeader.Ith_anal_1 = string.Empty;
            _inventoryHeader.Ith_anal_10 = true;
            _inventoryHeader.Ith_anal_11 = false;
            _inventoryHeader.Ith_anal_12 = false;
            _inventoryHeader.Ith_anal_2 = string.Empty;
            _inventoryHeader.Ith_anal_3 = string.Empty;
            _inventoryHeader.Ith_anal_4 = string.Empty;
            _inventoryHeader.Ith_anal_5 = string.Empty;
            _inventoryHeader.Ith_anal_6 = 0;
            _inventoryHeader.Ith_anal_7 = 0;
            _inventoryHeader.Ith_anal_8 = Convert.ToDateTime(txtDate.Text).Date;
            _inventoryHeader.Ith_anal_9 = Convert.ToDateTime(txtDate.Text).Date;
            _inventoryHeader.Ith_bus_entity = string.Empty;
            _inventoryHeader.Ith_cate_tp = string.Empty;
            _inventoryHeader.Ith_channel = string.Empty;
            _inventoryHeader.Ith_com = txtFromComp.Text;
            _inventoryHeader.Ith_com_docno = string.Empty;
            _inventoryHeader.Ith_cre_by = BaseCls.GlbUserID;
            _inventoryHeader.Ith_cre_when = DateTime.Now.Date;
            _inventoryHeader.Ith_del_add1 = string.Empty;
            _inventoryHeader.Ith_del_add2 = string.Empty;
            _inventoryHeader.Ith_del_code = string.Empty;
            _inventoryHeader.Ith_del_party = string.Empty;
            _inventoryHeader.Ith_del_town = string.Empty;
            _inventoryHeader.Ith_direct = false;
            _inventoryHeader.Ith_doc_date = Convert.ToDateTime(txtDate.Text);
            _inventoryHeader.Ith_doc_no = string.Empty;
            _inventoryHeader.Ith_doc_tp = string.Empty;
            _inventoryHeader.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Date.Year;
            _inventoryHeader.Ith_entry_no = string.Empty;
            _inventoryHeader.Ith_entry_tp = string.Empty;
            _inventoryHeader.Ith_git_close = false;
            _inventoryHeader.Ith_git_close_date = Convert.ToDateTime(txtDate.Text).Date;
            _inventoryHeader.Ith_git_close_doc = string.Empty;
            _inventoryHeader.Ith_is_manual = false;
            _inventoryHeader.Ith_isprinted = false;
            _inventoryHeader.Ith_job_no = string.Empty;
            _inventoryHeader.Ith_loading_point = string.Empty;
            _inventoryHeader.Ith_loading_user = string.Empty;
            _inventoryHeader.Ith_loc = _locCode;
            _inventoryHeader.Ith_manual_ref = "";
            _inventoryHeader.Ith_mod_by = BaseCls.GlbUserID;
            _inventoryHeader.Ith_mod_when = DateTime.Now.Date;
            _inventoryHeader.Ith_noofcopies = 0;
            _inventoryHeader.Ith_oth_loc = "";
            _inventoryHeader.Ith_oth_docno = string.Empty;
            _inventoryHeader.Ith_remarks = "MIG";
            _inventoryHeader.Ith_sbu = string.Empty;
            //_inventoryHeader.Ith_seq_no = 0; removed by Chamal 12-05-2013
            _inventoryHeader.Ith_session_id = BaseCls.GlbUserSessionID;
            _inventoryHeader.Ith_stus = "A";
            _inventoryHeader.Ith_sub_tp = string.Empty;
            _inventoryHeader.Ith_vehi_no = string.Empty;
            _inventoryHeader.Ith_oth_com = txtToComp.Text;
            _inventoryHeader.Ith_anal_1 = "0";
            _inventoryHeader.Ith_anal_2 = string.Empty;

            InvoiceHeader _invoiceheader = new InvoiceHeader();
            _invoiceheader.Sah_com = txtFromComp.Text;
            _invoiceheader.Sah_cre_by = BaseCls.GlbUserID;
            _invoiceheader.Sah_cre_when = DateTime.Now;
            _invoiceheader.Sah_currency = "LKR";
            _invoiceheader.Sah_cus_add1 = string.Empty;
            _invoiceheader.Sah_cus_add2 = string.Empty;
            _invoiceheader.Sah_cus_cd = "CASH";
            _invoiceheader.Sah_cus_name = string.Empty;
            _invoiceheader.Sah_d_cust_add1 = string.Empty;
            _invoiceheader.Sah_d_cust_add2 = string.Empty;
            _invoiceheader.Sah_d_cust_cd = "CASH";
            _invoiceheader.Sah_direct = true;
            _invoiceheader.Sah_dt = Convert.ToDateTime(txtDate.Text);
            _invoiceheader.Sah_epf_rt = 0;
            _invoiceheader.Sah_esd_rt = 0;
            _invoiceheader.Sah_ex_rt = 1;
            _invoiceheader.Sah_inv_no = "NA";
            _invoiceheader.Sah_inv_sub_tp = "SA"; //(Old Value - CS)Change value as per the Dilanda request ::Chamal De Silva 18-08-2012 16:30
            _invoiceheader.Sah_inv_tp = "INTR"; //(Old Value - CRED)Change value as per the Dilanda request ::Chamal De Silva 18-08-2012 16:30
            _invoiceheader.Sah_is_acc_upload = false;
            _invoiceheader.Sah_man_cd = string.Empty;
            _invoiceheader.Sah_man_ref = string.Empty;
            _invoiceheader.Sah_manual = false;
            _invoiceheader.Sah_mod_by = BaseCls.GlbUserID;
            _invoiceheader.Sah_mod_when = DateTime.Now;
            _invoiceheader.Sah_pc = _locCode;
            _invoiceheader.Sah_pdi_req = 0;
            _invoiceheader.Sah_ref_doc = string.Empty;
            _invoiceheader.Sah_remarks = string.Empty;
            _invoiceheader.Sah_sales_chn_cd = string.Empty;
            _invoiceheader.Sah_sales_chn_man = string.Empty;
            _invoiceheader.Sah_sales_ex_cd = string.Empty;
            _invoiceheader.Sah_sales_region_cd = string.Empty;
            _invoiceheader.Sah_sales_region_man = string.Empty;
            _invoiceheader.Sah_sales_sbu_cd = string.Empty;
            _invoiceheader.Sah_sales_sbu_man = string.Empty;
            _invoiceheader.Sah_sales_str_cd = string.Empty;
            _invoiceheader.Sah_sales_zone_cd = string.Empty;
            _invoiceheader.Sah_sales_zone_man = string.Empty;
            _invoiceheader.Sah_seq_no = 1;
            _invoiceheader.Sah_session_id = BaseCls.GlbUserSessionID;
            _invoiceheader.Sah_structure_seq = string.Empty;
            _invoiceheader.Sah_stus = "D";
            _invoiceheader.Sah_town_cd = string.Empty;
            _invoiceheader.Sah_tp = "INV";
            _invoiceheader.Sah_wht_rt = 0;

            //get all available serials
            List<ReptPickSerials> _reptPickSerial_ = new List<ReptPickSerials>();
            _reptPickSerial_ = null;// CHNLSVC.Inventory.GetAvailableSerials(txtFromComp.Text, _locCode);

            //add temp pick header
            Int32 generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "PRN", 1, txtFromComp.Text);//direction always =1 for this method

            //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = "PRN";
            RPH.Tuh_cre_dt = DateTime.Today;
            RPH.Tuh_ischek_itmstus = true;
            RPH.Tuh_ischek_reqqty = true;
            RPH.Tuh_ischek_simitm = true;
            RPH.Tuh_session_id = BaseCls.GlbUserSessionID;
            RPH.Tuh_usr_com = txtFromComp.Text;
            RPH.Tuh_usr_id = BaseCls.GlbUserID;
            RPH.Tuh_usrseq_no = generated_seq;

            RPH.Tuh_direct = false;
            RPH.Tuh_doc_no = "-1";

            //write entry to TEMP_PICK_HDR
            affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);

            //check whether the serial should be PRN
            foreach (ReptPickSerials _ser in _reptPickSerial_)
            {
                _docTp = DocumentTypeDecider(_ser.Tus_ser_id);

                //add to temp pick ser
                if (_docTp == "PRN")
                {
                    #region add
                    MasterItem msitem = CHNLSVC.Inventory.GetItem(txtFromComp.Text, _ser.Tus_itm_cd);

                    if (msitem.Mi_is_ser1 != -1)
                    //change msitem.Mi_is_ser1 == true
                    {
                        int rowCount = 0;

                        Int32 serID = _ser.Tus_ser_id;

                        string binCode = _ser.Tus_bin;
                        ReptPickSerials _reptPick_Serial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(txtFromComp.Text, _locCode, binCode, _ser.Tus_itm_cd, serID);
                        //Update_inrser_INS_AVAILABLE
                        Boolean update_inr_ser = false;
                        //   update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(txtFromComp.Text, _locCode, _ser.Tus_itm_cd, serID, -1);

                        _reptPick_Serial_.Tus_cre_by = BaseCls.GlbUserID;
                        _reptPick_Serial_.Tus_usrseq_no = generated_seq;
                        _reptPick_Serial_.Tus_cre_by = BaseCls.GlbUserID;
                        _reptPick_Serial_.Tus_base_doc_no = "-1";
                        _reptPick_Serial_.Tus_base_itm_line = 1;
                        _reptPick_Serial_.Tus_itm_desc = msitem.Mi_shortdesc;
                        _reptPick_Serial_.Tus_itm_model = msitem.Mi_model;
                        _reptPick_Serial_.Tus_job_no = "";
                        _reptPick_Serial_.Tus_pgs_prefix = _ser.Tus_itm_cd;
                        _reptPick_Serial_.Tus_job_line = 0;
                        _reptPick_Serial_.Tus_new_remarks = "PRN";
                        //enter row into TEMP_PICK_SER
                        affected_rows = -1;

                        affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPick_Serial_, null);

                        rowCount++;
                        //if (!_isWriteToTemporaryTable)
                        //{
                        //    if (_selectedItemList == null || _selectedItemList.Count <= 0) _selectedItemList = new List<ReptPickSerials>();
                        //    _selectedItemList.Add(_reptPick_Serial_);
                        //}
                        //isManualscan = true;

                    }
                    else
                    {
                        ReptPickSerials _reptPick_Serial_ = new ReptPickSerials();
                        _reptPick_Serial_.Tus_cre_by = BaseCls.GlbUserID;
                        _reptPick_Serial_.Tus_usrseq_no = generated_seq;
                        _reptPick_Serial_.Tus_cre_by = BaseCls.GlbUserID;
                        _reptPick_Serial_.Tus_base_doc_no = "-1";
                        _reptPick_Serial_.Tus_base_itm_line = 0;
                        _reptPick_Serial_.Tus_itm_desc = msitem.Mi_shortdesc;
                        _reptPick_Serial_.Tus_itm_model = msitem.Mi_model;
                        _reptPick_Serial_.Tus_com = txtFromComp.Text;
                        _reptPick_Serial_.Tus_loc = _locCode;
                        _reptPick_Serial_.Tus_bin = _binCode;
                        _reptPick_Serial_.Tus_itm_cd = _ser.Tus_itm_cd;
                        _reptPick_Serial_.Tus_itm_stus = _ser.Tus_itm_stus;
                        _reptPick_Serial_.Tus_qty = _ser.Tus_qty;
                        _reptPick_Serial_.Tus_ser_1 = "N/A";
                        _reptPick_Serial_.Tus_ser_2 = "N/A";
                        _reptPick_Serial_.Tus_ser_3 = "N/A";
                        _reptPick_Serial_.Tus_ser_4 = "N/A";
                        _reptPick_Serial_.Tus_ser_id = 0;
                        _reptPick_Serial_.Tus_serial_id = "0";
                        _reptPick_Serial_.Tus_job_no = "";
                        _reptPick_Serial_.Tus_pgs_prefix = _ser.Tus_itm_cd;
                        _reptPick_Serial_.Tus_job_line = 0;
                        _reptPick_Serial_.Tus_new_remarks = "PRN";

                        //enter row into TEMP_PICK_SER
                        affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPick_Serial_, null);
                    }
                    #endregion
                }
            }
            //get saved temp pick ser
            List<ReptPickSerials> _reptPickSerials = new List<ReptPickSerials>();
            _reptPickSerials = CHNLSVC.Inventory.GetAllScanSerialsList(txtFromComp.Text, _locCode, BaseCls.GlbUserID, generated_seq, COM_OUT);

            if (_reptPickSerials != null)
            {
                //update base item line
                var _scanItems = _reptPickSerials.GroupBy(x => new { x.Tus_itm_cd }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                foreach (var itm in _scanItems)
                {
                    foreach (var item_ in _reptPickSerials.Where(w => w.Tus_itm_cd == itm.Peo.Tus_itm_cd))
                    {
                        item_.Tus_base_itm_line = _baseItmLine;
                        _baseItmLine = _baseItmLine + 1;
                    }

                }


                string _message = string.Empty;
                string _genInventoryDoc = string.Empty;
                string _genSalesDoc = string.Empty;
                Int32 _effect = -1;

                if (_reptPickSerials.Count != 0)
                {
                    _effect = CHNLSVC.Inventory.SaveCommonOutWardEntry(txtFromComp.Text, _locCode, txtToComp.Text, null, _inventoryHeader, _inventoryAuto, _invoiceheader, _invoiceAuto, _reptPickSerials, null, out _message, out _genSalesDoc, out _genInventoryDoc, false, false);

                    if (_effect == -1)
                    {
                        this.Cursor = Cursors.Default;
                        if (_message.Contains("EMS.CHK_INLFREEQTY"))
                        {
                            txtSQL.Text = txtSQL.Text + "\n" + _locCode + " - There is no free stock balance available";

                        }
                        else if (_message.Contains("EMS.CHK_INBFREEQTY"))
                        {
                            txtSQL.Text = txtSQL.Text + "\n" + _locCode + " - There is no free stock balance available";

                        }
                        else
                        {
                            txtSQL.Text = txtSQL.Text + "\n" + _locCode + " - Please check the issues of " + _message;

                        }
                    }
                    else
                    {
                        txtSQL.Text = txtSQL.Text + "\n" + _locCode + " - Completed - " + _genInventoryDoc;

                        //start SRN process
                        string _supplier = string.Empty;
                        string _subdoc = string.Empty;

                        DataTable _headerchk = CHNLSVC.Inventory.GetPickHeaderByDocument(txtToComp.Text, _genInventoryDoc);
                        if (_headerchk != null && _headerchk.Rows.Count > 0)
                        {
                            string _headerUser = _headerchk.Rows[0].Field<string>("tuh_usr_id");
                            string _scanDate = Convert.ToString(_headerchk.Rows[0].Field<DateTime>("tuh_cre_dt"));
                            if (!string.IsNullOrEmpty(_headerUser))
                                if (BaseCls.GlbUserID.Trim() != _headerUser.Trim())
                                {

                                    txtSQL.Text = txtSQL.Text + "\n" + _genInventoryDoc + " had been already scanned by the user";
                                }
                        }
                        DateTime hdnOutwarddate = txtDate.Value.Date;
                        string OutwardType = "PRN";
                        _supplier = _headerchk.Rows[0].Field<string>("ith_bus_entity"); //Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_supcode"].Value);
                        _subdoc = _headerchk.Rows[0].Field<string>("ith_sub_docno"); //Convert.ToString(gvPending.Rows[_rowIndex].Cells["pen_subdoc"].Value);
                        string EntryNo = _headerchk.Rows[0].Field<string>("ith_entry_no");   // Convert.ToString(gvPending.Rows[_rowIndex].Cells["Entry_no"].Value);

                        string _dono = string.Empty;
                        List<ReptPickSerials> PickSerialsList = null;
                        ReptPickHeader _reptPickHdr = new ReptPickHeader();
                        Int32 _seq = CHNLSVC.Inventory.GetRequestUserSeqNo(BaseCls.GlbUserComCode, _genInventoryDoc);
                        Int32 UserSeqNo = _seq;
                        _reptPickHdr.Tuh_direct = true;
                        _reptPickHdr.Tuh_doc_no = _genInventoryDoc;
                        _reptPickHdr.Tuh_doc_tp = OutwardType;
                        _reptPickHdr.Tuh_ischek_itmstus = false;
                        _reptPickHdr.Tuh_ischek_reqqty = true;
                        _reptPickHdr.Tuh_ischek_simitm = false;
                        _reptPickHdr.Tuh_session_id = BaseCls.GlbUserSessionID;
                        _reptPickHdr.Tuh_usr_com = BaseCls.GlbUserComCode;
                        _reptPickHdr.Tuh_usr_id = BaseCls.GlbUserID;
                        _reptPickHdr.Tuh_usrseq_no = _seq;
                        string _unavailableitemlist = string.Empty;
                        List<ReptPickSerials> PickSerials = CHNLSVC.Inventory.GetOutwarditems(BaseCls.GlbUserDefLoca, "0", _reptPickHdr, out _unavailableitemlist);
                        if (!string.IsNullOrEmpty(_unavailableitemlist))
                        {
                            MessageBox.Show("Following item does not setup in the current system.\nItem List " + _unavailableitemlist, "Unavailable Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        if (PickSerials != null)
                        {

                            DataTable _lpstatus = CHNLSVC.General.GetItemLPStatus();
                            int _adhocline = 1;
                            foreach (ReptPickSerials _pik in PickSerials)
                            {
                                InventorySerialMaster _master = CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(_pik.Tus_ser_id);
                                if (_master != null && !string.IsNullOrEmpty(_master.Irsm_com))
                                {
                                    _pik.Tus_new_remarks = _master.Irsm_anal_2;
                                    _dono = _master.Irsm_anal_2; DataTable _tbl = CHNLSVC.Inventory.GetPOLine(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _dono, _pik.Tus_ser_id);
                                    if (_tbl != null && _tbl.Rows.Count > 0)
                                    { _pik.Tus_itm_stus = _tbl.Rows[0].Field<string>("itb_itm_stus"); _pik.Tus_new_status = Convert.ToString(_tbl.Rows[0].Field<Int32>("itb_base_refline")); _pik.Tus_base_itm_line = _tbl.Rows[0].Field<Int32>("itb_base_refline"); }
                                    else
                                    { var _lp = _lpstatus.AsEnumerable().Where(x => x.Field<string>("mis_cd") == _pik.Tus_itm_stus).Select(x => x.Field<string>("mis_scm2_imp")).ToList(); _pik.Tus_itm_stus = Convert.ToString(_lp[0]); _pik.Tus_new_status = Convert.ToString(_adhocline); _pik.Tus_base_itm_line = _adhocline; _adhocline += 1; }
                                }
                            }

                            var _tblItems = (from _pickSerials in PickSerials group _pickSerials by new { _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_stus } into item select new { Tus_itm_cd = item.Key.Tus_itm_cd, Tus_itm_desc = item.Key.Tus_itm_desc, Tus_itm_model = item.Key.Tus_itm_model, Tus_itm_stus = item.Key.Tus_itm_stus, Tus_qty = item.Sum(p => p.Tus_qty) }).ToList();

                            PickSerialsList = PickSerials;
                            //-------------------------------------------
                            InventoryHeader invHdr = new InventoryHeader();
                            invHdr.Ith_loc = BaseCls.GlbUserDefLoca;
                            invHdr.Ith_com = BaseCls.GlbUserComCode;
                            invHdr.Ith_oth_docno = _genInventoryDoc;
                            invHdr.Ith_doc_date = Convert.ToDateTime(txtDate.Text).Date;
                            invHdr.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Year;
                          invHdr.Ith_doc_tp = "SRN"; invHdr.Ith_cate_tp = "NOR"; invHdr.Ith_sub_tp = "NOR"; invHdr.Ith_sub_docno = _genInventoryDoc; 
              
                            PurchaseOrder _pohdr = CHNLSVC.Inventory.GetPurchaseOrderHeaderDetails(BaseCls.GlbUserComCode, invHdr.Ith_oth_docno);
                            PurchaseOrderDetail _poone = new PurchaseOrderDetail();
                            List<PurchaseOrderDetail> _poLst = new List<PurchaseOrderDetail>();
                            string _supplierclaimcode = string.Empty;

                            DataTable Invoice = null;
                            if (string.IsNullOrEmpty(_dono))
                            {
                                MessageBox.Show("Can't find delivery order details!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            else
                            {
                                Invoice = CHNLSVC.Inventory.GetInvoiceDet(BaseCls.GlbUserComCode, _dono);
                                if (Invoice == null || Invoice.Rows.Count <= 0)
                                {
                                    //Check DO in SCM database :: Chamal 29-04-2014 ::
                                    Invoice = CHNLSVC.Inventory.GetSCMInvoiceDet(BaseCls.GlbUserComCode, _dono);
                                    if (Invoice == null || Invoice.Rows.Count <= 0)
                                    {
                                        MessageBox.Show("Invalid delivery order no!/nTECH INFOR - see the inr_ser.irsm_anal_2 value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                            }



                            string _invTp = Invoice.Rows[0].Field<string>("sah_inv_tp");
                            string _pc = Invoice.Rows[0].Field<string>("sah_pc");
                            string _invoiceno = Invoice.Rows[0].Field<string>("sah_inv_no");
                            string _customercode = Invoice.Rows[0].Field<string>("sah_cus_cd");

                            _invTp = "CRED";


                            InvoiceHeader _invheader = new InvoiceHeader();
                            _invheader.Sah_com = BaseCls.GlbUserComCode; _invheader.Sah_cre_by = BaseCls.GlbUserID;
                            _invheader.Sah_cre_when = DateTime.Now;
                            _invheader.Sah_currency = "LKR"; _invheader.Sah_cus_add1 = string.Empty;
                            _invheader.Sah_cus_add2 = string.Empty; _invheader.Sah_cus_cd = _customercode; _invheader.Sah_cus_name = string.Empty;
                            _invheader.Sah_d_cust_add1 = string.Empty; _invheader.Sah_d_cust_add2 = string.Empty; _invheader.Sah_d_cust_cd = _customercode;
                            _invheader.Sah_direct = false; _invheader.Sah_dt = Convert.ToDateTime(txtDate.Value).Date; _invheader.Sah_epf_rt = 0;
                            _invheader.Sah_esd_rt = 0; _invheader.Sah_ex_rt = 1; _invheader.Sah_inv_no = "na";
                            _invheader.Sah_inv_sub_tp = "REV"; _invheader.Sah_inv_tp = _invTp; _invheader.Sah_is_acc_upload = false;
                            _invheader.Sah_man_cd = "N/A"; _invheader.Sah_man_ref = string.Empty; _invheader.Sah_manual = false;
                            _invheader.Sah_mod_by = BaseCls.GlbUserID; _invheader.Sah_mod_when = DateTime.Now; _invheader.Sah_pc = _pc; //BaseCls.GlbUserDefProf;
                            _invheader.Sah_pdi_req = 0; _invheader.Sah_ref_doc = _invoiceno; _invheader.Sah_remarks = string.Empty;
                            _invheader.Sah_sales_chn_cd = string.Empty; _invheader.Sah_sales_chn_man = string.Empty; _invheader.Sah_sales_ex_cd = "N/A";
                            _invheader.Sah_sales_region_cd = string.Empty; _invheader.Sah_sales_region_man = string.Empty; _invheader.Sah_sales_sbu_cd = string.Empty;
                            _invheader.Sah_sales_sbu_man = string.Empty; _invheader.Sah_sales_str_cd = string.Empty; _invheader.Sah_sales_zone_cd = string.Empty;
                            _invheader.Sah_sales_zone_man = string.Empty; _invheader.Sah_seq_no = 1;
                            _invheader.Sah_session_id = BaseCls.GlbUserSessionID; _invheader.Sah_structure_seq = string.Empty;
                            _invheader.Sah_stus = "A"; _invheader.Sah_town_cd = string.Empty;
                            _invheader.Sah_tp = "INV"; _invheader.Sah_wht_rt = 0;
                            _invheader.Sah_tax_inv = false; _invheader.Sah_anal_5 = _dono;
                           // MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                            _invoiceAuto.Aut_cate_cd = _pc; _invoiceAuto.Aut_cate_tp = "PC";
                            _invoiceAuto.Aut_direction = 0; _invoiceAuto.Aut_modify_dt = null;
                            _invoiceAuto.Aut_moduleid = "REV"; _invoiceAuto.Aut_number = 0;
                            if (BaseCls.GlbUserComCode == "LRP") _invoiceAuto.Aut_start_char = "RINREV"; else _invoiceAuto.Aut_start_char = "INREV";
                            _invoiceAuto.Aut_year = null;
                            decimal _unitAmt = 0; decimal _disAmt = 0; decimal _taxAmt = 0; decimal _totAmt = 0;
                            //List<InvoiceItem> _paramInvoiceItems = CHNLSVC.Sales.GetInvoiceDetailsForReversal(_invoiceno, "DELIVERD");                  
                            //Chamal edit on 30/04/2014
                            List<InvoiceItem> _paramInvoiceItems = CHNLSVC.Sales.GetInvoiceDetailsForIntrPRN(_invoiceno, "DELIVERD", _genInventoryDoc);
                            List<InvoiceItem> CreditNoteLst = new List<InvoiceItem>();
                            int invoiceLine = 0;
                            if (_paramInvoiceItems != null && _paramInvoiceItems.Count > 0)
                            {
                                foreach (ReptPickSerials s in PickSerialsList)
                                {
                                    List<ReptPickSerials> _serLst = CHNLSVC.Inventory.GetInvoiceSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, "", string.Empty, _invoiceno, Convert.ToInt32(s.Tus_new_status));
                                    var _ucost = _serLst.Where(x => x.Tus_ser_id == s.Tus_ser_id).Select(x => x.Tus_unit_cost).ToList();
                                    if (_ucost != null && _ucost.Count() > 0) s.Tus_unit_cost = _ucost[0];
                                    var InvoiceItem = _paramInvoiceItems.Where(x => x.Sad_itm_line == Convert.ToInt32(s.Tus_new_status)).ToList();

                                    if (InvoiceItem != null && InvoiceItem.Count > 0)
                                    {
                                        foreach (InvoiceItem item in InvoiceItem)
                                        {
                                            item.Sad_itm_line = invoiceLine;
                                            s.Tus_base_itm_line = invoiceLine;
                                            invoiceLine++;
                                            _unitAmt = (Convert.ToDecimal(item.Sad_unit_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty)); _disAmt = (Convert.ToDecimal(item.Sad_disc_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty)); _taxAmt = (Convert.ToDecimal(item.Sad_itm_tax_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty)); _totAmt = (Convert.ToDecimal(item.Sad_tot_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty));
                                            item.Sad_unit_amt = Convert.ToDecimal(_unitAmt); item.Sad_disc_amt = Convert.ToDecimal(_disAmt); item.Sad_itm_tax_amt = Convert.ToDecimal(_taxAmt); item.Sad_tot_amt = Convert.ToDecimal(_totAmt);
                                            CreditNoteLst.Add(item);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                int q = 1;
                                //Check SCM database :: Chamal 29/04/2014
                                _paramInvoiceItems = CHNLSVC.Sales.GetInvoiceDetailsForReversalSCM(_invoiceno, "DELIVERD", _genInventoryDoc);
                                if (_paramInvoiceItems != null && _paramInvoiceItems.Count > 0)
                                {
                                    foreach (ReptPickSerials s in PickSerialsList)
                                    {
                                        //List<ReptPickSerials> _serLst = CHNLSVC.Inventory.GetInvoiceSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, "", string.Empty, _invoiceno, Convert.ToInt32(s.Tus_new_status));
                                        //var _ucost = _serLst.Where(x => x.Tus_ser_id == s.Tus_ser_id).Select(x => x.Tus_unit_cost).ToList();
                                        //if (_ucost != null && _ucost.Count() > 0) s.Tus_unit_cost = _ucost[0];

                                        int _isSer = 0;
                                        if (s.Tus_ser_1 != "N/A")
                                        {
                                            _isSer = 1;
                                        }

                                        DataTable _dtscmcost = CHNLSVC.Inventory.GetItemCostSerialSCM(_dono, s.Tus_itm_cd, s.Tus_itm_stus, s.Tus_ser_1, _isSer);
                                        if (_dtscmcost != null)
                                        {
                                            if (_dtscmcost.Rows.Count > 0)  //kapila 6/62016
                                            {
                                                s.Tus_unit_cost = _dtscmcost.Rows[0].Field<decimal>("UNIT_COST");
                                            }
                                            else
                                            {
                                                s.Tus_unit_cost = 0;
                                            }
                                        }
                                        else
                                            s.Tus_unit_cost = 0;

                                        var InvoiceItem = _paramInvoiceItems.Where(x => x.Sad_itm_cd == s.Tus_itm_cd).ToList();
                                        if (InvoiceItem != null && InvoiceItem.Count > 0)
                                        {
                                            foreach (InvoiceItem item in InvoiceItem)
                                            {
                                                InvoiceItem itemn = new InvoiceItem();
                                                itemn.Sad_alt_itm_cd = item.Sad_alt_itm_cd;
                                                itemn.Sad_alt_itm_desc = item.Sad_alt_itm_desc;
                                                itemn.Sad_comm_amt = item.Sad_comm_amt;
                                                itemn.Sad_conf_line = item.Sad_conf_line;
                                                itemn.Sad_conf_no = item.Sad_conf_no;
                                                itemn.Sad_dis_line = item.Sad_dis_line;
                                                itemn.Sad_dis_seq = item.Sad_dis_seq;
                                                itemn.Sad_dis_type = item.Sad_dis_type;
                                                itemn.Sad_disc_amt = item.Sad_disc_amt;
                                                itemn.Sad_disc_rt = item.Sad_disc_rt;
                                                itemn.Sad_do_qty = item.Sad_do_qty;
                                                itemn.Sad_fws_ignore_qty = item.Sad_fws_ignore_qty;
                                                itemn.Sad_inv_no = item.Sad_inv_no;
                                                itemn.Sad_is_promo = item.Sad_is_promo;
                                                itemn.Sad_isapp = item.Sad_isapp;
                                                itemn.Sad_iscovernote = item.Sad_iscovernote;
                                                itemn.Sad_itm_cd = item.Sad_itm_cd;
                                                itemn.Sad_itm_line = item.Sad_itm_line;
                                                itemn.Sad_itm_seq = item.Sad_itm_seq;
                                                itemn.Sad_itm_stus = item.Sad_itm_stus;
                                                itemn.Sad_itm_stus_desc = item.Sad_itm_stus_desc;
                                                itemn.Sad_itm_tax_amt = item.Sad_itm_tax_amt;
                                                itemn.Sad_itm_tp = item.Sad_itm_tp;
                                                itemn.Sad_job_line = item.Sad_job_line;
                                                itemn.Sad_job_no = item.Sad_job_no;
                                                itemn.Sad_merge_itm = item.Sad_merge_itm;
                                                itemn.Sad_outlet_dept = item.Sad_outlet_dept;
                                                itemn.Sad_pb_lvl = item.Sad_pb_lvl;
                                                itemn.Sad_pb_price = item.Sad_pb_price;
                                                itemn.Sad_pbook = item.Sad_pbook;
                                                itemn.Sad_print_stus = item.Sad_print_stus;
                                                itemn.Sad_promo_cd = item.Sad_promo_cd;
                                                itemn.Sad_qty = item.Sad_qty;
                                                itemn.Sad_res_line_no = item.Sad_res_line_no;
                                                itemn.Sad_res_no = item.Sad_res_no;
                                                itemn.Sad_seq = item.Sad_seq;
                                                itemn.Sad_seq_no = item.Sad_seq_no;
                                                itemn.Sad_sim_itm_cd = item.Sad_sim_itm_cd;
                                                itemn.Sad_srn_qty = item.Sad_srn_qty;
                                                itemn.Sad_tot_amt = item.Sad_tot_amt;
                                                itemn.Sad_trd_svc_chrg = item.Sad_trd_svc_chrg;
                                                itemn.Sad_unit_amt = item.Sad_unit_amt;
                                                itemn.Sad_unit_rt = item.Sad_unit_rt;
                                                itemn.Sad_uom = item.Sad_uom;
                                                itemn.Sad_warr_based = item.Sad_warr_based;
                                                itemn.Sad_warr_period = item.Sad_warr_period;
                                                itemn.Sad_warr_remarks = item.Sad_warr_remarks;
                                                itemn.SII_CURR = item.SII_CURR;
                                                itemn.SII_EX_RT = item.SII_EX_RT;

                                                itemn.Sad_do_qty = s.Tus_qty;
                                                itemn.Sad_srn_qty = s.Tus_qty;
                                                itemn.Sad_qty = s.Tus_qty;
                                                itemn.Sad_tot_amt = itemn.Sad_unit_rt * s.Tus_qty;
                                                itemn.Sad_unit_amt = itemn.Sad_unit_rt * s.Tus_qty;
                                                if (item.Sad_itm_tax_amt > 0) itemn.Sad_itm_tax_amt = item.Sad_itm_tax_amt / item.Sad_qty * s.Tus_qty;
                                                itemn.Sad_itm_line = q;
                                                CreditNoteLst.Add(itemn);
                                                q += 1;
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Invoice items not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }

                            _invheader.Sah_anal_7 = CreditNoteLst.Sum(X => X.Sad_tot_amt);
                            MasterAutoNumber masterAutoNum = new MasterAutoNumber();
                            masterAutoNum.Aut_moduleid = "SRN"; masterAutoNum.Aut_start_char = "SRN";
                            PickSerialsList.OrderBy(X => X.Tus_itm_cd); string _crno = string.Empty;
                            invHdr.Ith_oth_loc = string.Empty; invHdr.Ith_cate_tp = "INTR";
                            string documntNo = "";
                            Int32 result = CHNLSVC.Sales.SaveReversalNew(_invheader, CreditNoteLst, _invoiceAuto, false, out _crno, invHdr, PickSerialsList, null, masterAutoNum, null, null, null, false, null, null, null, false, false, _invheader.Sah_pc, null, null, null, null, null, false, out documntNo);

                        }

                        //End SRN process
                    }
                }
                else
                {
                    txtSQL.Text = txtSQL.Text + "\n" + _locCode + " - No Balance to transfer";
                }
            }
            else
                txtSQL.Text = txtSQL.Text + "\n" + _locCode + " - No Balance to transfer";
        }

        private void button71_Click(object sender, EventArgs e)
        {
            string _err = string.Empty;
            string _filePath = string.Empty;
            string _locCode = "";
            string _toLocCode = "";
            string _tobinCode = "";

            string _suplier_Cde = string.Empty;
            txtSQL.Text = "";


            _locCode = txtLoc.Text;
            _toLocCode = txtToLoc.Text;

            string _msg = "";
            string _ponumber = "";
            string _genInventoryDoc = txtDO.Text;

            DataTable _adminT = CHNLSVC.Inventory.Get_location_by_code(txtFromComp.Text, _locCode);

            List<InterCompanySalesParameter> _priceParam = CHNLSVC.Sales.GetInterCompanyParameter(_adminT.Rows[0].Field<string>("ml_ope_cd"), txtFromComp.Text, string.Empty, txtToComp.Text, string.Empty);
            _tobinCode = CHNLSVC.Inventory.Get_default_binCD(txtToComp.Text, _toLocCode);

            //get PO number
            InventoryHeader _invH = CHNLSVC.Inventory.Get_Int_Hdr(_genInventoryDoc);
            _ponumber = _invH.Ith_sub_docno;
            //get supplier
            PurchaseOrder _poHdr = CHNLSVC.Inventory.GetPOHeader(txtToComp.Text, _ponumber, "L");
            _suplier_Cde = _poHdr.Poh_supp.ToString();

            CHNLSVC.Inventory.GetSCMDeliveryOrder(txtDate.Value.Date, txtToComp.Text, _toLocCode, _tobinCode, _priceParam[0].Sritc_sup, _genInventoryDoc, _ponumber, BaseCls.GlbUserID, out _msg);
            if (!string.IsNullOrEmpty(_msg))
                txtSQL.Text = txtSQL.Text + "\n" + _toLocCode + " (GRN) - " + _msg;
            else
            {
                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                InventoryHeader invHdr = new InventoryHeader();
                string documntNo = "";
                Int32 result = -99;

                Int32 _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", txtToComp.Text, _ponumber, 1);
                reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(txtToComp.Text, _toLocCode, BaseCls.GlbUserID, _userSeqNo, "GRN");
                if (reptPickSerialsList == null)
                {
                    txtSQL.Text = txtSQL.Text + "\n" + _genInventoryDoc + " - No items found for GRN";
                }
                else
                {
                    var _dup = reptPickSerialsList.Where(x => x.Tus_ser_1 != "N/A").Select(y => y.Tus_ser_id).ToList();

                    string _duplicateItems = string.Empty;
                    bool _isDuplicate = false;
                    if (_dup != null)
                        if (_dup.Count > 0)
                            foreach (Int32 _id in _dup)
                            {
                                Int32 _counts = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
                                if (_counts > 1)
                                {
                                    _isDuplicate = true;
                                    var _item = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
                                    foreach (string _str in _item)
                                        if (string.IsNullOrEmpty(_duplicateItems))
                                            _duplicateItems = _str;
                                        else
                                            _duplicateItems += "," + _str;
                                }
                            }
                    if (_isDuplicate)
                    {
                        Cursor.Current = Cursors.Default;
                        //MessageBox.Show("Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSQL.Text = txtSQL.Text + "\n" + _toLocCode + " - Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems;
                        //return;
                    }


                    reptPickSerialsList.ForEach(i => { i.Tus_exist_grncom = txtToComp.Text; i.Tus_exist_grndt = txtDate.Value.Date; i.Tus_exist_supp = _suplier_Cde; i.Tus_orig_grncom = txtToComp.Text; i.Tus_orig_grndt = txtDate.Value.Date; i.Tus_orig_supp = _suplier_Cde; });

                    List<PurchaseOrderDelivery> _purchaseOrderDeliveryList = new List<PurchaseOrderDelivery>();
                    _purchaseOrderDeliveryList = CHNLSVC.Inventory.GetConsignmentItemDetails(txtToComp.Text, _ponumber, _toLocCode);

                    if (reptPickSerialsList != null)
                    {
                        var _scanItems1 = reptPickSerialsList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems1)
                        {
                            //foreach (PurchaseOrderDelivery _invItem in _purchaseOrderDeliveryList)
                            //{
                            //    if (itm.Peo.Tus_itm_cd == _invItem.MasterItem.Mi_cd && itm.Peo.Tus_itm_line == _invItem.Podi_line_no)
                            //    {
                            //        _invItem.Actual_qty = itm.theCount; // Current scan qty
                            //    }
                            //}
                        }
                    }

                    InventoryHeader _invHeader = new InventoryHeader();

                    DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(txtToComp.Text, _toLocCode);
                    foreach (DataRow r in dt_location.Rows)
                    {
                        // Get the value of the wanted column and cast it to string
                        _invHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                        if (System.DBNull.Value != r["ML_CATE_2"]) _invHeader.Ith_channel = (string)r["ML_CATE_2"]; else _invHeader.Ith_channel = string.Empty;
                    }

                    _invHeader.Ith_com = txtToComp.Text;
                    _invHeader.Ith_loc = _toLocCode;
                    _invHeader.Ith_doc_date = txtDate.Value.Date;
                    _invHeader.Ith_doc_year = txtDate.Value.Date.Year;
                    _invHeader.Ith_direct = true;
                    _invHeader.Ith_doc_tp = "GRN";

                    _invHeader.Ith_cate_tp = "LOCAL";
                    _invHeader.Ith_sub_tp = "LOCAL";

                    _invHeader.Ith_bus_entity = _suplier_Cde;
                    _invHeader.Ith_is_manual = false;
                    _invHeader.Ith_manual_ref = "";
                    _invHeader.Ith_remarks = "MIG";
                    _invHeader.Ith_stus = "A";
                    _invHeader.Ith_cre_by = BaseCls.GlbUserID;
                    _invHeader.Ith_cre_when = DateTime.Now;
                    _invHeader.Ith_mod_by = BaseCls.GlbUserID;
                    _invHeader.Ith_mod_when = DateTime.Now;
                    _invHeader.Ith_session_id = BaseCls.GlbUserSessionID;
                    _invHeader.Ith_oth_docno = _ponumber;


                    MasterAutoNumber _masterAuto = new MasterAutoNumber();
                    _masterAuto.Aut_cate_cd = _toLocCode; ;
                    _masterAuto.Aut_cate_tp = "LOC";
                    _masterAuto.Aut_direction = null;
                    _masterAuto.Aut_modify_dt = null;
                    _masterAuto.Aut_moduleid = "GRN";
                    _masterAuto.Aut_number = 0;
                    _masterAuto.Aut_start_char = "GRN";
                    _masterAuto.Aut_year = _invHeader.Ith_doc_date.Date.Year;

                    //Add by Chamal 23-May-2014
                    int _updateDate = CHNLSVC.Inventory.ChangeScanSerialDocDate(_invHeader.Ith_oth_com, _invHeader.Ith_com, _invHeader.Ith_doc_tp, _ponumber, _invHeader.Ith_doc_date.Date, BaseCls.GlbUserID);
                    reptPickSerialsList.ForEach(x => x.Tus_doc_dt = _invHeader.Ith_doc_date.Date);
                    if (_invHeader.Ith_doc_tp == "GRN")
                    {
                        if (_invHeader.Ith_oth_com == "ABL" && _invHeader.Ith_com == "LRP")
                        {
                            reptPickSerialsList.ForEach(x => x.Tus_orig_grndt = _invHeader.Ith_doc_date.Date);
                            reptPickSerialsList.ForEach(x => x.Tus_exist_grndt = _invHeader.Ith_doc_date.Date);
                        }
                        if (_invHeader.Ith_oth_com == "SGL" && _invHeader.Ith_com == "SGD")
                        {
                            reptPickSerialsList.ForEach(x => x.Tus_orig_grndt = _invHeader.Ith_doc_date.Date);
                            reptPickSerialsList.ForEach(x => x.Tus_exist_grndt = _invHeader.Ith_doc_date.Date);
                        }
                    }

                    result = CHNLSVC.Inventory.ConsignmentReceipt(_invHeader, reptPickSerialsList, null, _masterAuto, _purchaseOrderDeliveryList, out documntNo);
                    if (result != -99 && result >= 0)
                    {
                        txtSQL.Text = txtSQL.Text + "\n" + _toLocCode + " - GRN Completed - " + documntNo;
                    }
                }

            }
        }


    }
}
