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
using FF.WindowsERPClient.Reports.HP;
namespace FF.WindowsERPClient.Reports.Sales
{
    public partial class invoice1 : Base
    {
        public invoice1()
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
               Reports.Inventory.ReportViewerInventory _view = new    Reports.Inventory.ReportViewerInventory();
               string _docNo;
               _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
               _view.GlbReportName = "Outward_Docs_Full.rpt";   //Outward_Docs_full
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
            _docNo = Interaction.InputBox("Document No", "Doc No",  "", 400, 100);


            _view.GlbReportName = "InvoicePrintTax.rpt";
            _view.GlbReportDoc = _docNo;
            _view.Show();
            _view = null;
   
        }

        private void button21_Click(object sender, EventArgs e)
        {
            ReportViewer _view = new ReportViewer();

       
        BaseCls.GlbReportName= "InsuranceCoverNote.rpt";
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
             BaseCls.GlbReportDataTable=ECD_VOU_PRINT;
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
                     
        {      Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();

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
            BaseCls.GlbReportName =  "SServiceReceiptPrints.rpt";
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
            _view.GlbReportName = "ReceiptPrints_n.rpt";
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

            BaseCls.GlbReportDoc=_docNo;
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
            BaseCls.GlbReportDoc = txttmpCusCode.Text ;
            _view.GlbReportDoc = txttmpCusCode.Text ;
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
            _filePath = CHNLSVC.MsgPortal.GetCommonExcelDetails("", BaseCls.GlbUserID, out _err);

            if (!string.IsNullOrEmpty(_err))
            {                
                Cursor.Current = Cursors.Default;
                MessageBox.Show(_err);
                return;
            }

            if (string.IsNullOrEmpty(_filePath))
            {                
                Cursor.Current = Cursors.Default;
                MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(_filePath);
            p.Start();

            MessageBox.Show("Export Completed", "Sales Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btn_HPRec_Click(object sender, EventArgs e)
        {
            //Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
            //BaseCls.GlbReportName = string.Empty;
            //_view.GlbReportName = string.Empty;

            //string _docNo;
            //_docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            //_view.GlbReportName = "HPReceiptPrint.rpt";
            //BaseCls.GlbReportName = "HPReceiptPrint.rpt";
            //_view.GlbReportDoc = _docNo;
            //BaseCls.GlbReportDoc= _docNo;
            ////  _view.GlbReportDoc = "AAZMD-INREV00002";
            //_view.Show();
            //_view = null;

            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            BaseCls.GlbReportDoc = _docNo;
            clsHpSalesRep objHp = new clsHpSalesRep();            
            if (objHp.checkIsDirectPrint() == true)
            {
                objHp.HPRecPrint_Direct();
            }
            else
            {
                Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                _view.GlbReportName = "HPReceiptPrint.rpt";
                BaseCls.GlbReportName = "HPReceiptPrint.rpt";
                _view.GlbReportDoc = _docNo;
                BaseCls.GlbReportDoc = _docNo;                
                _view.Show();
                _view = null;
            }
            
        }

        private void btn_DoPrint_Click(object sender, EventArgs e)
        {
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            BaseCls.GlbReportDoc = _docNo;
            clsInventoryRep objDo = new clsInventoryRep();
            objDo.DoRecPrint_Direct();
        }

        private void Invoice_Click(object sender, EventArgs e)
        {
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            BaseCls.GlbReportDoc = _docNo;
            clsSalesRep objInv = new clsSalesRep();
            objInv.InvoicePrint_Direct();
        }

        private void txtInsPrint_Click(object sender, EventArgs e)
        {
            string _docNo;
            _docNo = Interaction.InputBox("Document No", "Doc No", "", 400, 100);
            BaseCls.GlbReportDoc = _docNo;
            clsHpSalesRep objHp = new clsHpSalesRep();
            if (objHp.checkIsDirectPrint() == true)
            {
                objHp.InsurancePrint_Direct();
            }
        }
    }
}
