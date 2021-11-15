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
using System.Runtime.InteropServices;
using System.Reflection;

namespace FF.WindowsERPClient.Reports.Inventory
{
    public partial class ReportViewerInventory : Base
    {
        
        
        private Inward_Docs inwardReport1 = new Inward_Docs();
        private GRNreport GrnReport1 = new GRNreport(); //Tharanga 2016/06/01
      
        private Inward_Docs_Full inwardReport2 = new Inward_Docs_Full();
        private Inward_Docs_ABE inwardReport_abe = new Inward_Docs_ABE();
        private Inward_Docs_ABE2 inwardReport_abe2 = new Inward_Docs_ABE2();
        private Inward_Docs_AST inwardReport_ast = new Inward_Docs_AST();
        private Inward_Docs_ack inwardReport_ack = new Inward_Docs_ack();
        private SInward_Docs SinwardReport1 = new SInward_Docs();
        private Dealer_Inward_Docs DinwardReport1 = new Dealer_Inward_Docs();
        private Outward_Docs_ABE1 outwardReport_abe = new Outward_Docs_ABE1();
        private Outward_Docs_AST outwardReport_ast = new Outward_Docs_AST();
        private Outward_Docs outwardReport1 = new Outward_Docs();
        private Outward_Docs_Full outwardReport2 = new Outward_Docs_Full();
        private SOutward_Docs SoutwardReport1 = new SOutward_Docs();
        private Outward_Docs_Del_Conf outwarddelconfReport = new Outward_Docs_Del_Conf();
        private Dealer_Outward_Docs DoutwardReport1 = new Dealer_Outward_Docs();
        private GRANPrints _granReport1 = new GRANPrints();
        private GRANPrints_full _granReportfull = new GRANPrints_full();
        private SGRANPrints S_granReport1 = new SGRANPrints();
        public MRNRepPrints _mrnReport1 = new MRNRepPrints();
        private SMRNRepPrints S_mrnReport1 = new SMRNRepPrints();
        private Dealer_MRNRepPrints D_mrnReport1 = new Dealer_MRNRepPrints();
        private InterTransfer _interTrans = new InterTransfer();
        private SInterTransfer S_interTrans = new SInterTransfer();
        private RCCPrints _rccReport1 = new RCCPrints();
        private SRCCPrints S_rccReport1 = new SRCCPrints();
        private FixedAssetTransferNotes _fixTrnsReport2 = new FixedAssetTransferNotes();
        private FixedAssetTransferNote _fixTrnsReport1 = new FixedAssetTransferNote();
        private SFixedAssetTransferNotes S_fixTrnsReport1 = new SFixedAssetTransferNotes();
        private FixedAssetConfirmationNotes _fixConReport1 = new FixedAssetConfirmationNotes();
        private SFixedAssetConfirmationNotes S_fixConReport1 = new SFixedAssetConfirmationNotes();
        private DINPrints _dinReport1 = new DINPrints();
        private Dealer_DINPrints _ddinReport1 = new Dealer_DINPrints();
        private SDINPrints S_dinReport1 = new SDINPrints();
        private Outward_Docs_AODOUTNEW outwardReport_aodout = new Outward_Docs_AODOUTNEW();
        private Inward_Doc_auto _Inward_Doc_auto = new Inward_Doc_auto(); //Tharanga 2017/06/28
        public Outward_Docs_ABE _Outward_Docs_ABE = new Outward_Docs_ABE(); //Tharanga 
        private Inward_Docs_ABE_New _Inward_Docs_ABE_New = new Inward_Docs_ABE_New();//Tharanga
        public DO_print_ABE _DO_print_ABE = new DO_print_ABE();//Tharanga
        private Outward_Doc_ABE_New _Outward_Docs_ABE_NEW = new Outward_Doc_ABE_New();
        private GRNreport_ABE _GRNreport_ABE = new GRNreport_ABE();
        private SRN_Doc_ABE _SRN_Doc_ABE = new SRN_Doc_ABE();
        private Inward_Docs_abstract _Inward_Docs_abstract = new Inward_Docs_abstract();
        private Outward_Docs_Full_abstract _Outward_Docs_Full_abstract = new Outward_Docs_Full_abstract();
        private Aod_Acknowledgemnt objAcknowledge = new Aod_Acknowledgemnt();
       
 
        PrintDialog PrintDialog1 = new PrintDialog();


        clsInventoryRep obj = new clsInventoryRep();

        public ReportViewerInventory()
        {
            InitializeComponent();
        }


        private void ReportViewerInventory_Load(object sender, EventArgs e)
        {
            try
            {
                printDialogSettings();
                btnPrint.Visible = true;
                listAllPrinters();
                lblPrinter.Text = "Default Printer- " + GetDefaultPrinter();

                string _repname = string.Empty;
                string _papersize = string.Empty;
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
                if (GlbReportName == "Outward_Docs.rpt")
                {
                    Out_doc_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "Outward_Docs_Full.rpt")
                {
                    Out_doc_print_Full();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "Outward_Docs_ABE1.rpt")
                {
                    Out_doc_print_ABE();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "Outward_Docs_AST.rpt")
                {
                    Out_doc_print_AST();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "Outward_Docs_Del_Conf.rpt")
                {
                    Out_doc_Del_Conf_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "SOutward_Docs.rpt")
                {
                    SOut_doc_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "Dealer_Outward_Docs.rpt")
                {
                    DOut_doc_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "Inward_Docs.rpt")
                {
                    In_doc_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };

                if (GlbReportName == "Inward_Docs_Full.rpt")
                {
                    In_doc_print_Full();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "Inward_Docs_ack.rpt")
                {
                    In_doc_ack_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (GlbReportName == "Inward_Docs_AST.rpt")
                {
                    In_doc_print_ast();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "Inward_Docs_ABE2.rpt")
                {
                    In_doc_print_abe2();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "Inward_Docs_ABE.rpt")
                {
                    In_doc_print_abe();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "SInward_Docs.rpt")
                {
                    SIn_doc_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "Dealer_Inward_Docs.rpt")
                {
                    DIn_doc_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (BaseCls.GlbReportName == "WarrantyClaim")
                {
                    WarrantyClaimCreditNote();
                };
                if (GlbReportName == "GRANPrints.rpt")
                {
                    GRAN_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "GRANPrints_full.rpt")
                {
                    GRAN_print_full();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "SGRANPrints.rpt")
                {
                    SGRAN_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "MRNRepPrints.rpt")
                {
                    MRN_print();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (GlbReportName == "excess_stock_report.rpt")
                {
                    ExcessShort_print();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (GlbReportName == "TemporaryIssueItems.rpt")
                {
                    obj.PrintTempIssueItems();
                    this.Text = "Booking Infor";
                    crystalReportViewer1.ReportSource = obj._tempIssueItms;
                    crystalReportViewer1.RefreshReport();
                };
                if (GlbReportName == "SubLocationStockVal.rpt")
                {
                    obj.PrintSubLocationStockValue();
                    this.Text = "Booking Infor";
                    crystalReportViewer1.ReportSource = obj._subLocStock;
                    crystalReportViewer1.RefreshReport();
                };

                if (GlbReportName == "Dealer_MRNRepPrints.rpt")
                {
                    DealerMRN_print();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (GlbReportName == "SMRNRepPrints.rpt")
                {
                    SMRN_print();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (GlbReportName == "RCCPrints.rpt")
                {
                    RCC_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "SRCCPrints.rpt")
                {
                    SRCC_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };

                if (GlbReportName == "DINPrints.rpt")
                {
                    DIN_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };

                if (GlbReportName == "Dealer_DINPrints.rpt")
                {
                    DDIN_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };

                if (GlbReportName == "SDINPrints.rpt")
                {
                    SDIN_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "Exchange_Docs.rpt" || GlbReportName == "Exchange_Docs_Full.rpt")
                {
                    BaseCls.GlbReportTp = "EXE";
                    CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                    if (!(_repname == null || _repname == "")) { GlbReportName = _repname; BaseCls.GlbReportName = _repname; BaseCls.GlbReportPaperSize = _papersize; }

                    EXCNG_print();
                    btnPrint.Visible = true;
                };

                if (GlbReportName == "SExchange_Docs.rpt")
                {
                    SEXCNG_print();
                    btnPrint.Visible = true;
                };

                if (GlbReportName == "FixedAssetTransferNotes.rpt")
                {
                    BaseCls.GlbReportTp = "Fixedt";
                    CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                    if (!(_repname == null || _repname == "")) { GlbReportName = _repname; BaseCls.GlbReportName = _repname; BaseCls.GlbReportPaperSize = _papersize; }

                    Fixed_Trans_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };

                if (GlbReportName == "FixedAssetTransferNote.rpt")
                {
                    BaseCls.GlbReportTp = "Fixedt";
                    CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                    if (!(_repname == null || _repname == "")) { GlbReportName = _repname; BaseCls.GlbReportName = _repname; BaseCls.GlbReportPaperSize = _papersize; }

                    Fixed_Trans_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };

                if (GlbReportName == "SFixedAssetTransferNotes.rpt")
                {
                    SFixed_Trans_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };

                if (GlbReportName == "OtherShop_DO_Report.rpt")
                {
                    OtherShopDO_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };

                if (GlbReportName == "Reserved_Serial_Report.rpt")
                {
                    Reserved_Serial_print();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (GlbReportName == "FixedAssetConfirmationNotes.rpt")
                {
                    BaseCls.GlbReportTp = "Fixedc";
                    CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                    if (!(_repname == null || _repname == "")) { GlbReportName = _repname; BaseCls.GlbReportName = _repname; BaseCls.GlbReportPaperSize = _papersize; }

                    Fixed_Trans_confirmation_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (BaseCls.GlbReportName == "RevertSRN.rpt")
                {
                    RevetAdjPrint();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (BaseCls.GlbReportName == "SRevertSRN.rpt")
                {
                    SRevetAdjPrint();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (BaseCls.GlbReportName == "InventoryStatements.rpt")
                {
                    InventoryStatement();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "PSI.rpt")
                {
                    PrintPSI_Report();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "InventoryStatementsTr.rpt")
                {
                    InventoryStatementTr();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "rpt_GLB_Git_Document.rpt")
                {
                    GITAsat_Report();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "POSummary.rpt")
                {
                    POSummary();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Insu_Stock_Report.rpt")
                {
                    InsuredStock();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "InventoryStatementsTr2.rpt")
                {
                    InventoryStatementTr2();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "InventoryStatementsTr3.rpt")
                {
                    InventoryStatementTr3();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "InventoryStatementsTr3_new.rpt")
                {
                    InventoryStatementTr3_new();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };


                if (BaseCls.GlbReportName == "InventoryMovementAuditTrialWithSerials.rpt")
                {
                    MovementAuditTrialSerial();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "InventoryMovementAuditTrials.rpt")
                {
                    MovementAuditTrial();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "InventoryMovementAuditTrials_ARL.rpt")
                {
                    MovementAuditTrial_ARL();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "InventoryMovementAuditTrials_sum.rpt")
                {
                    MovementAuditTrial();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "InventoryMovementAuditTrials_GroupByCate.rpt")
                {
                    MovementAuditTrial();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Movement_audit_trial_cost.rpt" || BaseCls.GlbReportName == "Movement_audit_trial_ser.rpt" || BaseCls.GlbReportName == "Movement_audit_trial_ser_cost.rpt" || BaseCls.GlbReportName == "Movement_audit_trial.rpt" || BaseCls.GlbReportName == "Movement_audit_trial_det.rpt" || BaseCls.GlbReportName == "Movement_audit_trial_summary.rpt" || BaseCls.GlbReportName == "Movement_audit_trial_sum.rpt")
                {
                    MovementAuditTrialNew();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "DamageGoodsApproval.rpt")
                {
                    DamegeApproval();
                };
                if (BaseCls.GlbReportName == "GRAN_Details_Report.rpt")
                {
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                    GRANReport();
                };
                if (BaseCls.GlbReportName == "FAT_Dtl_Report.rpt")
                {
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                    FATReport();
                };

                if (BaseCls.GlbReportName == "PurchaseOrderPrint.rpt")
                {
                    POReport();
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "PurchaseOrderPrint_AST.rpt")
                {
                    POReportAST();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Stock_Balance.rpt" || BaseCls.GlbReportName == "Stock_Balance_WO_Stus.rpt" || BaseCls.GlbReportName == "Stock_Balance_WO_Det.rpt")
                {
                    btnPrint.Visible = false;
                    if (BaseCls.GlbReportType == "ASAT") { InventoryBalance(); }
                    if (BaseCls.GlbReportType == "CURR") { InventoryBalanceCurr(); }
                };

                if (BaseCls.GlbReportName == "Stock_Balance_AST.rpt")
                {
                    btnPrint.Visible = false;
                    if (BaseCls.GlbReportType == "ASAT") { InventoryBalance(); }
                    if (BaseCls.GlbReportType == "CURR") { InventoryBalanceCurr(); }
                };

                if (BaseCls.GlbReportName == "FastMovingItems.rpt" || BaseCls.GlbReportName == "NonMovingItems.rpt")
                {
                    FastMovingItems();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "LocItemStusAge.rpt" || BaseCls.GlbReportName == "ItmwiseItemAge.rpt" || BaseCls.GlbReportName == "CatwiseItemAge.rpt" || BaseCls.GlbReportName == "ItmBrndwiseItemAge.rpt" || BaseCls.GlbReportName == "LocwiseItemAge.rpt" || BaseCls.GlbReportName == "CatScatwiseItemAge.rpt" || BaseCls.GlbReportName == "CatItmwiseItemAge.rpt" || BaseCls.GlbReportName == "LocItemStusAgenew.rpt")
                {
                    Loc_wise_item_age();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "StockBalanceWithSerialAge.rpt")
                {
                    StockBalancewithAge();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "FIFO_Not_Followed_Report.rpt")
                {
                    FIFONotFollowed();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "WarrantyClaimNote.rpt")
                {
                    WarrantyClimInNote();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (BaseCls.GlbReportName == "Stock_BalanceCost.rpt")
                {
                    inventoryBalanceWithCost();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "Stock_BalanceCost_AST.rpt")
                {
                    inventoryBalanceWithCost();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "Stock_BalanceSerial.rpt")
                {
                    inventoryBalanceWithSerial();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "Stock_BalanceSerialAsat.rpt")
                {
                    inventoryBalanceWithSerial();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "InterTransfer_Details_Report.rpt")
                {
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                    IntrReport();
                };
                if (BaseCls.GlbReportName == "SerialAge.rpt")
                {
                    serialCompanyLocationAge();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "GvPrint")
                {
                    GvPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "MovementSummaryReport.rpt")
                {
                    MovementSummary();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "df_InvBal_Report.rpt")
                {
                    DFInvBalPricePrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "BOCReservedSerials.rpt")
                {
                    BOCReservedSerialDetails();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (GlbReportName == "Reorder_Items.rpt")
                {
                    ReorderItems();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (GlbReportName == "temp_saved_doc_report.rpt")
                {
                    TempSavedDocs();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (GlbReportName == "Current_balance_with_price.rpt")
                {
                    CurrBalPrice();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (GlbReportName == "curr_age_report.rpt")
                {
                    CurrAge();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "PurchaseOrderPrintUpdate.rpt")
                {
                    POReportUpdate();
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (GlbReportName == "Outward_Docs_AODOUT.rpt")
                {
                   // Out_doc_print_AOD();
                    Out_doc_printall();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "Outward_Docs_AODOUTNEW.rpt")
                {
                    // Out_doc_print_AOD();
                    Out_doc_printall();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };

                //Tharanga 2017/06/01
                if (GlbReportName == "GRNreport.rpt")
                {
                    GRN_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "Inward_Doc_auto.rpt")
                {
                    In_doc_Auto();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };

                if (GlbReportName == "DO_print_ABE.rpt") // Tharanga 2017/07/10
                {
                    Do_print_ABE();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (BaseCls.GlbReportName == "Inward_Docs_ABE_New.rpt") //Tharanga 2017/07/11
                {
                    In_doc_ABE();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "Outward_Doc_ABE_New.rpt")//Tharanga 2017/07/14
                {
                    Out_doc_ABE();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                //Tharanga 2017/07/17
                if (GlbReportName == "GRNreport_ABE.rpt")
                {
                    GRN_print_ABE();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (BaseCls.GlbReportName == "PurchaseOrderPrint_ABE.rpt")//Tharanga 2017/07/17
                {
                    POPrintABE();
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (GlbReportName == "SRN_Doc_ABE.rpt") //Tharanga 2017/07/18
                {
                    SRN_doc_ABE();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };
                if (GlbReportName == "Inward_Docs_abstract.rpt")
                {
                    In_doc_abstract();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (GlbReportName == "Outward_Docs_Full_abstract.rpt") //add by tharanga 2017/09/20
                {
                    Out_doc_print_Full_abstract();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = true;
                };

                //Tharindu 2017-11-17
                if (GlbReportName == "Aod_Acknowledgemnt.rpt")
                {
                    Aod_Ack_doc_print();
                    btnPrint.Visible = true;
                    crystalReportViewer1.ShowPrintButton = false;
                    crystalReportViewer1.ShowExportButton = false;
                };

                      //Wimal 27/03/2018
                if (GlbReportName == "AdjusmentCostWithCatType.rpt")
                {
                  
                     obj.PrintAdjDetlWDocCate_Report();
                     crystalReportViewer1.ReportSource = obj._StkAdjDtlWCate;
                     this.Text = "";
                     crystalReportViewer1.RefreshReport();
                };               
                       //Wimal 27/03/2018
                if (GlbReportName == "summarized_age_report.rpt")
                {
                    string err = "";
                    err = obj.Ageing_Report_for_Provisioning();
                    if (!string.IsNullOrEmpty(err))
                    {

                        MessageBox.Show(err, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   return;
                    }
                    crystalReportViewer1.ReportSource = obj._summarized_age_report;
            
                     this.Text = "";
                     crystalReportViewer1.RefreshReport();
                };
                if (GlbReportName == "Status_wise_ageing_report.rpt")
                {
                    string err = "";
                    err = obj.status_wise_ageing_report();
                    if (!string.IsNullOrEmpty(err))
                    {

                        MessageBox.Show(err, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    crystalReportViewer1.ReportSource = obj._Status_wise_ageing_report;

                    this.Text = "";
                    crystalReportViewer1.RefreshReport();
                };
                if (GlbReportName == "Disposal_summary.rpt")
                {
                    string err = "";
                    err = obj.Dispoal_summary();
                    if (!string.IsNullOrEmpty(err))
                    {

                        MessageBox.Show(err, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    crystalReportViewer1.ReportSource = obj._Disposal_summary;

                    this.Text = "";
                    crystalReportViewer1.RefreshReport();
                }; 
                
               
                //Tharindu 2018-03-27
                if (GlbReportName == "Charge_Sheet.rpt")
                {
                    ChargeSheet_print();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "AgeSummery.rpt")
                {
                    AgeSummery();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

               
                BaseCls.GlbReportTp = string.Empty;
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #region Report Functions
        private void In_doc_print_Full()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //Inward_Docs report1 = new Inward_Docs();

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

            int _numPages = 0;
            DataRow dr;

            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
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
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            PO_DTL.Clear();
            PO_DTL = CHNLSVC.Sales.GetPODetails(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();


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
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                    }

                    mst_com = CHNLSVC.General.GetCompanyByCode(_com);
                    mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeTable(row["ITH_SUB_TP"].ToString());
                }

                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
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
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            inwardReport2.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            inwardReport2.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            inwardReport2.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            inwardReport2.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            inwardReport2.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            inwardReport2.Database.Tables["mst_com"].SetDataSource(mst_com);
            inwardReport2.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            inwardReport2.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            inwardReport2.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);


            foreach (object repOp in inwardReport2.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = inwardReport2.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }

                }
            }
            this.Text = "Inward Print";


            crystalReportViewer1.ReportSource = inwardReport2;

            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();

        }

        private void In_doc_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //Inward_Docs report1 = new Inward_Docs();

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
           
            int _numPages = 0;
            DataRow dr;

            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr = PRINT_DOC.NewRow();
            dr["NOOFPAGES"] = _numPages;
            dr["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr);

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            MST_ITM.Columns.Add("MI_ITM_UOM", typeof(string));


            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));

            

            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            PO_DTL.Clear();
            PO_DTL = CHNLSVC.Sales.GetPODetails(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();


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
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                    }

                    mst_com = CHNLSVC.General.GetCompanyByCode(_com);
                    mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeTable(row["ITH_SUB_TP"].ToString());
                }






                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
                Int32 totqty = 0;
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
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            inwardReport1.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            inwardReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            inwardReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            inwardReport1.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            inwardReport1.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            inwardReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            inwardReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            inwardReport1.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            inwardReport1.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);

         


            foreach (object repOp in inwardReport1.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = inwardReport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }

                }
            }
            this.Text = "Inward Print";

            crystalReportViewer1.ReportSource = inwardReport1;

            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();

        }
        private void In_doc_ack_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //Inward_Docs report1 = new Inward_Docs();

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

            int _numPages = 0;
            DataRow dr;

            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));
            PRINT_DOC.Columns.Add("PRINT_USER", typeof(string));
            PRINT_DOC.Columns.Add("SR_MGR", typeof(string));
            PRINT_DOC.Columns.Add("NIC", typeof(string));
            PRINT_DOC.Columns.Add("EPF", typeof(string));
            PRINT_DOC.Columns.Add("REC_SR_MGR", typeof(string));
            PRINT_DOC.Columns.Add("REC_NIC", typeof(string));
            PRINT_DOC.Columns.Add("REC_EPF", typeof(string));

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));


            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));

            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            PO_DTL.Clear();
            PO_DTL = CHNLSVC.Sales.GetPODetails(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();

            string _loc = "";
            string _othLoc = "";
            foreach (DataRow row in VIW_INV_MOVEMENT_SERIAL.Rows)
            {
                string _com = row["ITH_COM"].ToString();
                _loc = row["ITH_LOC"].ToString();
                string _itm = row["ITS_ITM_CD"].ToString();
                _othLoc = row["ITH_OTH_LOC"].ToString();
                int index = VIW_INV_MOVEMENT_SERIAL.Rows.IndexOf(row);
                if (index == 0)
                {
                    MST_LOC = CHNLSVC.Sales.GetLocationCode(_com, _loc);
                    MST_LOC_1 = CHNLSVC.Sales.GetLocationCode(_com, _othLoc);

                    if (!string.IsNullOrEmpty(row["ITH_OTH_DOCNO"].ToString()))
                    {
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                    }

                    mst_com = CHNLSVC.General.GetCompanyByCode(_com);
                    mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeTable(row["ITH_SUB_TP"].ToString());
                }

                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
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

            DataTable _locmgr = CHNLSVC.Inventory.getLocManagerDetail(_loc);
            DataTable _locmgrrec = CHNLSVC.Inventory.getLocManagerDetail(_othLoc);
            dr = PRINT_DOC.NewRow();
            dr["NOOFPAGES"] = _numPages;
            dr["SHOWCOM"] = BaseCls.ShowComName;
            dr["PRINT_USER"] = BaseCls.GlbUserID;
            dr["SR_MGR"] = "";
            dr["NIC"] = "";
            dr["EPF"] = "";
            dr["REC_SR_MGR"] = "";
            dr["REC_NIC"] = "";
            dr["REC_EPF"] = "";
            foreach (DataRow row1 in _locmgr.Rows)
            {
                dr["SR_MGR"] = row1["emp_name"].ToString();
                dr["NIC"] = row1["esep_nic"].ToString();
                dr["EPF"] = row1["esep_epf"].ToString();
            }
            foreach (DataRow row1 in _locmgrrec.Rows)
            {
                dr["REC_SR_MGR"] = row1["emp_name"].ToString();
                dr["REC_NIC"] = row1["esep_nic"].ToString();
                dr["REC_EPF"] = row1["esep_epf"].ToString();
            }
            PRINT_DOC.Rows.Add(dr);

            MST_ITM_STUS = MST_ITM_STUS.DefaultView.ToTable(true);
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            inwardReport_ack.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            inwardReport_ack.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            inwardReport_ack.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            inwardReport_ack.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            inwardReport_ack.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            inwardReport_ack.Database.Tables["mst_com"].SetDataSource(mst_com);
            inwardReport_ack.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            inwardReport_ack.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            inwardReport_ack.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);


            foreach (object repOp in inwardReport_ack.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = inwardReport_ack.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }

                }
            }
            this.Text = "Inward Print";


            crystalReportViewer1.ReportSource = inwardReport_ack;

            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();

        }

        private void In_doc_print_ast()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //Inward_Docs report1 = new Inward_Docs();

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

            int _numPages = 0;
            DataRow dr;

            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
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
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            PO_DTL.Clear();
            PO_DTL = CHNLSVC.Sales.GetPODetails(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();


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
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                    }

                    mst_com = CHNLSVC.General.GetCompanyByCode(_com);
                    mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeTable(row["ITH_SUB_TP"].ToString());
                }

                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
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
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            inwardReport_ast.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            inwardReport_ast.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            inwardReport_ast.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            inwardReport_ast.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            inwardReport_ast.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            inwardReport_ast.Database.Tables["mst_com"].SetDataSource(mst_com);
            inwardReport_ast.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            inwardReport_ast.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            inwardReport_ast.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);


            foreach (object repOp in inwardReport_ast.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = inwardReport_ast.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }

                }
            }
            this.Text = "Inward Print";


            crystalReportViewer1.ReportSource = inwardReport_ast;

            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();

        }

        private void In_doc_print_abe2()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //Inward_Docs report1 = new Inward_Docs();

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

            int _numPages = 0;
            DataRow dr;

            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
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
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            PO_DTL.Clear();
            PO_DTL = CHNLSVC.Sales.GetPODetails(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();


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
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                    }

                    mst_com = CHNLSVC.General.GetCompanyByCode(_com);
                    mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeTable(row["ITH_SUB_TP"].ToString());
                }

                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
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
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            inwardReport_abe2.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            inwardReport_abe2.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            inwardReport_abe2.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            inwardReport_abe2.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            inwardReport_abe2.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            inwardReport_abe2.Database.Tables["mst_com"].SetDataSource(mst_com);
            inwardReport_abe2.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            inwardReport_abe2.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            inwardReport_abe2.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);


            foreach (object repOp in inwardReport_abe2.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = inwardReport_abe2.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }

                }
            }
            this.Text = "Inward Print";


            crystalReportViewer1.ReportSource = inwardReport_abe2;

            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();

        }

        private void In_doc_print_abe()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //Inward_Docs report1 = new Inward_Docs();

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

            int _numPages = 0;
            DataRow dr;

            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
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
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            PO_DTL.Clear();
            PO_DTL = CHNLSVC.Sales.GetPODetails(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();


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
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                    }

                    mst_com = CHNLSVC.General.GetCompanyByCode(_com);
                    mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeTable(row["ITH_SUB_TP"].ToString());
                }

                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
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
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            inwardReport_abe.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            inwardReport_abe.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            inwardReport_abe.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            inwardReport_abe.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            inwardReport_abe.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            inwardReport_abe.Database.Tables["mst_com"].SetDataSource(mst_com);
            inwardReport_abe.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            inwardReport_abe.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            inwardReport_abe.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);


            foreach (object repOp in inwardReport_abe.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = inwardReport_abe.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }

                }
            }
            this.Text = "Inward Print";


            crystalReportViewer1.ReportSource = inwardReport_abe;

            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();

        }

        private void SIn_doc_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //Inward_Docs report1 = new Inward_Docs();

            DataTable VIW_INV_MOVEMENT_SERIAL = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS1 = new DataTable();
            DataTable MST_LOC_1 = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            DataTable mst_movcatetp = new DataTable();

            int _numPages = 0;
            DataRow dr;

            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
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
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();


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
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                    }

                    mst_com = CHNLSVC.General.GetCompanyByCode(_com);
                    mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeTable(row["ITH_SUB_TP"].ToString());
                }

                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
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
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            SinwardReport1.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            SinwardReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            SinwardReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            SinwardReport1.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            SinwardReport1.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            SinwardReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            SinwardReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            SinwardReport1.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);




            foreach (object repOp in SinwardReport1.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = SinwardReport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }

                }
            }
            this.Text = "Inward Print";


            crystalReportViewer1.ReportSource = SinwardReport1;

            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();

        }

        private void DIn_doc_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //Inward_Docs report1 = new Inward_Docs();

            DataTable VIW_INV_MOVEMENT_SERIAL = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS1 = new DataTable();
            DataTable MST_LOC_1 = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            DataTable mst_movcatetp = new DataTable();

            int _numPages = 0;
            DataRow dr;

            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
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
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();


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
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                    }

                    mst_com = CHNLSVC.General.GetCompanyByCode(_com);
                    mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeTable(row["ITH_SUB_TP"].ToString());
                }

                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
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
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            DinwardReport1.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            DinwardReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            DinwardReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            DinwardReport1.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            DinwardReport1.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            DinwardReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            DinwardReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            DinwardReport1.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);




            foreach (object repOp in DinwardReport1.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = DinwardReport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }

                }
            }
            this.Text = "Inward Print";


            crystalReportViewer1.ReportSource = DinwardReport1;

            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();

        }

        private void Out_doc_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //    Outward_Docs report1 = new Outward_Docs();

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
            string inv_no = "";

            DataRow dr;
            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();

            DataTable param1 = new DataTable();
            param1.Columns.Add("saleType", typeof(string));
            DataTable tblComDate = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            DataTable receiptDetails = new DataTable();
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
            outwardReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);



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
                            //updated by akila 2018/01/08
                            VIW_SALES_DETAILS = CHNLSVC.Sales.GetSalesDetailsWithDo(row["ITH_DOC_NO"].ToString(), row["ITH_OTH_DOCNO"].ToString(), null);
                            //VIW_SALES_DETAILS = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                            //DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(row["ITH_OTH_DOCNO"].ToString());
                            DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(docNo);
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


                sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(inv_no);

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
            DataTable param = new DataTable();
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
            outwardReport1.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            outwardReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            outwardReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            outwardReport1.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            outwardReport1.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            outwardReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            outwardReport1.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            outwardReport1.Database.Tables["param1"].SetDataSource(param1);
            outwardReport1.Database.Tables["param"].SetDataSource(param);
            foreach (object repOp in outwardReport1.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = outwardReport1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }
                    if (_cs.SubreportName == "warr")
                    {
                        ReportDocument subRepDoc = outwardReport1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);

                    }
                    if (sat_hdr.Rows.Count > 0)
                    {
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = outwardReport1.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                        }
                        if (tblComDate.Rows.Count > 0)
                        {
                            if (_cs.SubreportName == "WarrComDate")
                            {
                                ReportDocument subRepDoc = outwardReport1.Subreports[_cs.SubreportName];
                                subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            }
                        }
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = outwardReport1.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                        }

                    }

                }

            }
            this.Text = "Outward Print";
            crystalReportViewer1.ReportSource = outwardReport1;
            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();



        }

        private void Out_doc_print_Full()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //    Outward_Docs report1 = new Outward_Docs();

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
            string inv_no = "";

            DataRow dr;
            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();

            DataTable param1 = new DataTable();
            param1.Columns.Add("saleType", typeof(string));
            DataTable tblComDate = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            DataTable receiptDetails = new DataTable();
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
            outwardReport2.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);



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
                    if (string.IsNullOrEmpty(_othLoc)) //Sanjeewa 2018-07-31 
                        MST_LOC_1 = CHNLSVC.Sales.GetLocationCode(_com, "Xxx");
                    else
                    MST_LOC_1 = CHNLSVC.Sales.GetLocationCode(_com, _othLoc);
                    if (!string.IsNullOrEmpty(row["ITH_OTH_DOCNO"].ToString()))
                    {
                        if (row["ITH_DOC_TP"].ToString() == "DO")
                        {
                            inv_no = row["ITH_OTH_DOCNO"].ToString();
                            sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);

                            //updated by akila 2018/01/08
                            VIW_SALES_DETAILS = CHNLSVC.Sales.GetSalesDetailsWithDo(row["ITH_DOC_NO"].ToString(), row["ITH_OTH_DOCNO"].ToString(), null);
                            //VIW_SALES_DETAILS = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                            //DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(row["ITH_OTH_DOCNO"].ToString()); //Sanjeewa 2017-02-15
                            DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(docNo);
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


                sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(inv_no);

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
            DataTable param = new DataTable();
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
            outwardReport2.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            outwardReport2.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            outwardReport2.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            outwardReport2.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            outwardReport2.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            outwardReport2.Database.Tables["mst_com"].SetDataSource(mst_com);
            outwardReport2.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            outwardReport2.Database.Tables["param1"].SetDataSource(param1);
            outwardReport2.Database.Tables["param"].SetDataSource(param);
            foreach (object repOp in outwardReport2.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = outwardReport2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                        subRepDoc.Database.Tables["SAT_HDR"].SetDataSource(sat_hdr);

                    }
                    if (_cs.SubreportName == "warr")
                    {
                        ReportDocument subRepDoc = outwardReport2.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);

                    }
                    if (sat_hdr.Rows.Count > 0)
                    {
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = outwardReport2.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                        }
                        if (tblComDate.Rows.Count > 0)
                        {
                            if (_cs.SubreportName == "WarrComDate")
                            {
                                ReportDocument subRepDoc = outwardReport2.Subreports[_cs.SubreportName];
                                subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            }
                        }
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = outwardReport2.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                        }

                    }

                }

            }
            this.Text = "Outward Print";
            crystalReportViewer1.ReportSource = outwardReport2;
            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();



        }

        private void Out_doc_print_AST()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //    Outward_Docs report1 = new Outward_Docs();

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
            string inv_no = "";

            DataRow dr;
            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();

            DataTable param1 = new DataTable();
            param1.Columns.Add("saleType", typeof(string));
            DataTable tblComDate = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            DataTable receiptDetails = new DataTable();
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
            outwardReport_ast.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);



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
                        inv_no = row["ITH_OTH_DOCNO"].ToString();
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                        VIW_SALES_DETAILS = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                        //DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(row["ITH_OTH_DOCNO"].ToString());
                        DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(docNo);
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

                        dr3 = VIW_SALES_DETAILS.NewRow();
                        dr3["SAD_ITM_CD"] = "1";

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


                sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(inv_no);

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
            DataTable param = new DataTable();
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
            outwardReport_ast.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            outwardReport_ast.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            outwardReport_ast.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            outwardReport_ast.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            outwardReport_ast.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            outwardReport_ast.Database.Tables["mst_com"].SetDataSource(mst_com);
            outwardReport_ast.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            outwardReport_ast.Database.Tables["param1"].SetDataSource(param1);
            outwardReport_ast.Database.Tables["param"].SetDataSource(param);
            foreach (object repOp in outwardReport_ast.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = outwardReport_ast.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }
                    if (_cs.SubreportName == "warr")
                    {
                        ReportDocument subRepDoc = outwardReport_ast.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);

                    }
                    if (sat_hdr.Rows.Count > 0)
                    {
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = outwardReport_ast.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                        }
                        if (tblComDate.Rows.Count > 0)
                        {
                            if (_cs.SubreportName == "WarrComDate")
                            {
                                ReportDocument subRepDoc = outwardReport_ast.Subreports[_cs.SubreportName];
                                subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            }
                        }
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = outwardReport_ast.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                        }

                    }

                }

            }
            this.Text = "Outward Print";
            crystalReportViewer1.ReportSource = outwardReport_ast;
            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();



        }

        private void Out_doc_print_ABE()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //    Outward_Docs report1 = new Outward_Docs();

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

            DataRow dr;
            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();

            DataTable param1 = new DataTable();
            param1.Columns.Add("saleType", typeof(string));
            DataTable tblComDate = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            DataTable receiptDetails = new DataTable();
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
            outwardReport_abe.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);



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
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                        VIW_SALES_DETAILS = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                        //DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(row["ITH_OTH_DOCNO"].ToString());
                        DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(docNo);
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

                        dr3 = VIW_SALES_DETAILS.NewRow();
                        dr3["SAD_ITM_CD"] = "1";

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
            DataTable param = new DataTable();
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
            outwardReport_abe.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            outwardReport_abe.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            outwardReport_abe.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            outwardReport_abe.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            outwardReport_abe.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            outwardReport_abe.Database.Tables["mst_com"].SetDataSource(mst_com);
            outwardReport_abe.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            outwardReport_abe.Database.Tables["param1"].SetDataSource(param1);
            outwardReport_abe.Database.Tables["param"].SetDataSource(param);
            foreach (object repOp in outwardReport_abe.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = outwardReport_abe.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }
                    if (_cs.SubreportName == "warr")
                    {
                        ReportDocument subRepDoc = outwardReport_abe.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);

                    }
                    if (sat_hdr.Rows.Count > 0)
                    {
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = outwardReport_abe.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                        }
                        if (tblComDate.Rows.Count > 0)
                        {
                            if (_cs.SubreportName == "WarrComDate")
                            {
                                ReportDocument subRepDoc = outwardReport_abe.Subreports[_cs.SubreportName];
                                subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            }
                        }

                    }

                }

            }
            this.Text = "Outward Print";
            crystalReportViewer1.ReportSource = outwardReport_abe;
            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();



        }

        private void Out_doc_Del_Conf_print()
        {
            string docNo = default(string);
            string cust_code = default(string);
            docNo = GlbReportDoc;

            //    Outward_Docs report1 = new Outward_Docs();

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
            DataTable mst_customer = new DataTable();

            DataRow dr;
            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();

            DataTable param1 = new DataTable();
            param1.Columns.Add("saleType", typeof(string));
            DataTable tblComDate = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            DataTable receiptDetails = new DataTable();
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
            outwarddelconfReport.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);



            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            MST_ITM.Columns.Add("MI_CATE_1", typeof(string));
            MST_ITM.Columns.Add("MI_CATE_2", typeof(string));
            MST_ITM.Columns.Add("MI_CATE_3", typeof(string));
            MST_ITM.Columns.Add("MI_BRAND", typeof(string));

            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));

            foreach (DataRow row in VIW_INV_MOVEMENT_SERIAL.Rows)
            {
                string _com = row["ITH_COM"].ToString();
                string _loc = row["ITH_LOC"].ToString();
                string _itm = row["ITS_ITM_CD"].ToString();
                string _othLoc = row["ITH_OTH_LOC"].ToString();
                cust_code = row["ITH_ENTRY_NO"].ToString(); //Sanjeewa 2015-08-05 QUOTATION NO
                int index = VIW_INV_MOVEMENT_SERIAL.Rows.IndexOf(row);
                if (index == 0)
                {
                    MST_LOC = CHNLSVC.Sales.GetLocationCode(_com, _loc);
                    MST_LOC_1 = CHNLSVC.Sales.GetLocationCode(_com, _othLoc);
                    if (!string.IsNullOrEmpty(row["ITH_OTH_DOCNO"].ToString()))
                    {
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                        VIW_SALES_DETAILS = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                        //DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(row["ITH_OTH_DOCNO"].ToString());
                        DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(docNo);
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

                        dr3 = VIW_SALES_DETAILS.NewRow();
                        dr3["SAD_ITM_CD"] = "1";

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

                mst_customer = new DataTable();
                mst_customer = CHNLSVC.Sales.Get_CustomerDetails(cust_code);

                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());


                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    dr["MI_CATE_1"] = row1["MI_CATE_1"].ToString();
                    dr["MI_CATE_2"] = row1["MI_CATE_2"].ToString();
                    dr["MI_CATE_3"] = row1["MI_CATE_3"].ToString();
                    dr["MI_BRAND"] = row1["MI_BRAND"].ToString();
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
            DataTable param = new DataTable();
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
            outwarddelconfReport.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            outwarddelconfReport.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            outwarddelconfReport.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            outwarddelconfReport.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            outwarddelconfReport.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            outwarddelconfReport.Database.Tables["mst_com"].SetDataSource(mst_com);
            outwarddelconfReport.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            outwarddelconfReport.Database.Tables["param1"].SetDataSource(param1);
            outwarddelconfReport.Database.Tables["param"].SetDataSource(param);
            outwarddelconfReport.Database.Tables["MST_BUSENTITY_GRUP"].SetDataSource(mst_customer);
            foreach (object repOp in outwarddelconfReport.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = outwarddelconfReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }
                    if (_cs.SubreportName == "warr")
                    {
                        ReportDocument subRepDoc = outwarddelconfReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);

                    }
                    if (sat_hdr.Rows.Count > 0)
                    {
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = outwarddelconfReport.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                        }
                        if (tblComDate.Rows.Count > 0)
                        {
                            if (_cs.SubreportName == "WarrComDate")
                            {
                                ReportDocument subRepDoc = outwarddelconfReport.Subreports[_cs.SubreportName];
                                subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            }
                        }

                    }

                }

            }
            this.Text = "Outward Print";
            crystalReportViewer1.ReportSource = outwarddelconfReport;
            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();

        }

        private void SOut_doc_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //    Outward_Docs report1 = new Outward_Docs();

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
            string inv_no = "";

            DataRow dr;
            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();

            DataTable param1 = new DataTable();
            param1.Columns.Add("saleType", typeof(string));
            DataTable tblComDate = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            DataTable receiptDetails = new DataTable();
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
            SoutwardReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);



            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));


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
                        inv_no = row["ITH_OTH_DOCNO"].ToString();
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                        VIW_SALES_DETAILS = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                        //DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(row["ITH_OTH_DOCNO"].ToString());
                        DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(docNo);
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

                        dr3 = VIW_SALES_DETAILS.NewRow();
                        dr3["SAD_ITM_CD"] = "1";

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



                sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(inv_no);
                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());


                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
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
            DataTable param = new DataTable();
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
            SoutwardReport1.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            SoutwardReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            SoutwardReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            SoutwardReport1.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            SoutwardReport1.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            SoutwardReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            SoutwardReport1.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            SoutwardReport1.Database.Tables["param1"].SetDataSource(param1);
            SoutwardReport1.Database.Tables["param"].SetDataSource(param);
            foreach (object repOp in SoutwardReport1.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = SoutwardReport1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }
                    if (_cs.SubreportName == "warr")
                    {
                        ReportDocument subRepDoc = SoutwardReport1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);

                    }
                    if (sat_hdr.Rows.Count > 0)
                    {
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = SoutwardReport1.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                        }
                        if (tblComDate.Rows.Count > 0)
                        {
                            if (_cs.SubreportName == "WarrComDate")
                            {
                                ReportDocument subRepDoc = SoutwardReport1.Subreports[_cs.SubreportName];
                                subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            }
                        }
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = outwardReport1.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                        }

                    }


                }

            }
            this.Text = "Outward Print";
            crystalReportViewer1.ReportSource = SoutwardReport1;
            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();



        }

        private void DOut_doc_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //    Outward_Docs report1 = new Outward_Docs();

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

            DataRow dr;
            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();

            DataTable param1 = new DataTable();
            param1.Columns.Add("saleType", typeof(string));
            DataTable tblComDate = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            DataTable receiptDetails = new DataTable();
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
            DoutwardReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);



            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));


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
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                        VIW_SALES_DETAILS = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                        //DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(row["ITH_OTH_DOCNO"].ToString());
                        DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(docNo);
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

                        dr3 = VIW_SALES_DETAILS.NewRow();
                        dr3["SAD_ITM_CD"] = "1";

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




                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());


                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
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
            DataTable param = new DataTable();
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
            DoutwardReport1.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            DoutwardReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            DoutwardReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            DoutwardReport1.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            DoutwardReport1.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            DoutwardReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            DoutwardReport1.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            DoutwardReport1.Database.Tables["param1"].SetDataSource(param1);
            DoutwardReport1.Database.Tables["param"].SetDataSource(param);
            foreach (object repOp in DoutwardReport1.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = DoutwardReport1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }
                    if (_cs.SubreportName == "warr")
                    {
                        ReportDocument subRepDoc = DoutwardReport1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);

                    }
                    if (sat_hdr.Rows.Count > 0)
                    {
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = DoutwardReport1.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                        }
                        if (tblComDate.Rows.Count > 0)
                        {
                            if (_cs.SubreportName == "WarrComDate")
                            {
                                ReportDocument subRepDoc = DoutwardReport1.Subreports[_cs.SubreportName];
                                subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            }
                        }

                    }

                }

            }
            this.Text = "Outward Print";
            crystalReportViewer1.ReportSource = DoutwardReport1;
            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();



        }

        private void DIN_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;
            DataTable int_req = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            //DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable MST_LOC_1 = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS1 = new DataTable();
            DataTable MST_ITM_STUS2 = new DataTable();
            DataRow dr;

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
            _dinReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            int_req = CHNLSVC.Inventory.GetInventoryRequestByReqNo(docNo);
            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));


            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));

            MST_ITM_STUS2.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS2.Columns.Add("MIS_DESC", typeof(string));

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable int_req_ser = new DataTable();
            string _comCode = string.Empty;
            foreach (DataRow row in int_req.Rows)
            {


                MST_LOC = CHNLSVC.Sales.GetLocationCode(row["ITR_COM"].ToString(), row["ITR_LOC"].ToString());
                MST_LOC_1 = CHNLSVC.Sales.GetLocationCode(row["ITR_COM"].ToString(), row["ITR_REC_TO"].ToString());
                mst_com = CHNLSVC.General.GetCompanyByCode(row["ITR_COM"].ToString());
                _comCode = row["ITR_COM"].ToString();
                int_req_ser = CHNLSVC.Inventory.GetInventoryRequestSerialsBySeqNo(row["ITR_SEQ_NO"].ToString());


            }
            foreach (DataRow row in int_req_ser.Rows)
            {
                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_comCode, row["ITRS_ITM_CD"].ToString());
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITRS_ITM_STUS"].ToString());


                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    MST_ITM.Rows.Add(dr);
                }
                foreach (DataRow row2 in MST_ITM_STUS1.Rows)
                {
                    dr = MST_ITM_STUS.NewRow();
                    dr["MIS_CD"] = row2["MIS_CD"].ToString();
                    dr["MIS_DESC"] = row2["MIS_DESC"].ToString();

                    MST_ITM_STUS.Rows.Add(dr);
                }

                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITRS_NITM_STUS"].ToString());
                foreach (DataRow row2 in MST_ITM_STUS1.Rows)
                {
                    dr = MST_ITM_STUS2.NewRow();
                    dr["MIS_CD"] = row2["MIS_CD"].ToString();
                    dr["MIS_DESC"] = row2["MIS_DESC"].ToString();

                    MST_ITM_STUS2.Rows.Add(dr);
                }
            }
            MST_ITM_STUS = MST_ITM_STUS.DefaultView.ToTable(true);
            MST_ITM_STUS2 = MST_ITM_STUS2.DefaultView.ToTable(true);
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            _dinReport1.Database.Tables["int_req"].SetDataSource(int_req);
            _dinReport1.Database.Tables["int_req_ser"].SetDataSource(int_req_ser);
            _dinReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _dinReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            _dinReport1.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            _dinReport1.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            _dinReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            _dinReport1.Database.Tables["MST_ITM_STUS_1"].SetDataSource(MST_ITM_STUS2);
            this.Text = "DIN Print";
            crystalReportViewer1.ReportSource = _dinReport1;
            crystalReportViewer1.RefreshReport();




        }

        private void DDIN_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;
            DataTable int_req = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            //DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable MST_LOC_1 = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS1 = new DataTable();
            DataTable MST_ITM_STUS2 = new DataTable();
            DataRow dr;

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
            _ddinReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            int_req = CHNLSVC.Inventory.GetInventoryRequestByReqNo(docNo);
            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));


            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));

            MST_ITM_STUS2.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS2.Columns.Add("MIS_DESC", typeof(string));

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable int_req_ser = new DataTable();
            string _comCode = string.Empty;
            foreach (DataRow row in int_req.Rows)
            {


                MST_LOC = CHNLSVC.Sales.GetLocationCode(row["ITR_COM"].ToString(), row["ITR_LOC"].ToString());
                MST_LOC_1 = CHNLSVC.Sales.GetLocationCode(row["ITR_COM"].ToString(), row["ITR_REC_TO"].ToString());
                mst_com = CHNLSVC.General.GetCompanyByCode(row["ITR_COM"].ToString());
                _comCode = row["ITR_COM"].ToString();
                int_req_ser = CHNLSVC.Inventory.GetInventoryRequestSerialsBySeqNo(row["ITR_SEQ_NO"].ToString());


            }
            foreach (DataRow row in int_req_ser.Rows)
            {
                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_comCode, row["ITRS_ITM_CD"].ToString());
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITRS_ITM_STUS"].ToString());


                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    MST_ITM.Rows.Add(dr);
                }
                foreach (DataRow row2 in MST_ITM_STUS1.Rows)
                {
                    dr = MST_ITM_STUS.NewRow();
                    dr["MIS_CD"] = row2["MIS_CD"].ToString();
                    dr["MIS_DESC"] = row2["MIS_DESC"].ToString();

                    MST_ITM_STUS.Rows.Add(dr);
                }

                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITRS_NITM_STUS"].ToString());
                foreach (DataRow row2 in MST_ITM_STUS1.Rows)
                {
                    dr = MST_ITM_STUS2.NewRow();
                    dr["MIS_CD"] = row2["MIS_CD"].ToString();
                    dr["MIS_DESC"] = row2["MIS_DESC"].ToString();

                    MST_ITM_STUS2.Rows.Add(dr);
                }
            }
            MST_ITM_STUS = MST_ITM_STUS.DefaultView.ToTable(true);

            _ddinReport1.Database.Tables["int_req"].SetDataSource(int_req);
            _ddinReport1.Database.Tables["int_req_ser"].SetDataSource(int_req_ser);
            _ddinReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _ddinReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            _ddinReport1.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            _ddinReport1.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            _ddinReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            _ddinReport1.Database.Tables["MST_ITM_STUS_1"].SetDataSource(MST_ITM_STUS2);
            this.Text = "DIN Print";
            crystalReportViewer1.ReportSource = _ddinReport1;
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
        private void SDIN_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;
            DataTable int_req = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            //DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable MST_LOC_1 = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS1 = new DataTable();
            DataTable MST_ITM_STUS2 = new DataTable();
            DataRow dr;

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
            S_dinReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            int_req = CHNLSVC.Inventory.GetInventoryRequestByReqNo(docNo);
            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));


            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));

            MST_ITM_STUS2.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS2.Columns.Add("MIS_DESC", typeof(string));

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable int_req_ser = new DataTable();
            string _comCode = string.Empty;
            foreach (DataRow row in int_req.Rows)
            {


                MST_LOC = CHNLSVC.Sales.GetLocationCode(row["ITR_COM"].ToString(), row["ITR_LOC"].ToString());
                MST_LOC_1 = CHNLSVC.Sales.GetLocationCode(row["ITR_COM"].ToString(), row["ITR_REC_TO"].ToString());
                mst_com = CHNLSVC.General.GetCompanyByCode(row["ITR_COM"].ToString());
                _comCode = row["ITR_COM"].ToString();
                int_req_ser = CHNLSVC.Inventory.GetInventoryRequestSerialsBySeqNo(row["ITR_SEQ_NO"].ToString());


            }
            foreach (DataRow row in int_req_ser.Rows)
            {
                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_comCode, row["ITRS_ITM_CD"].ToString());
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITRS_ITM_STUS"].ToString());


                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    MST_ITM.Rows.Add(dr);
                }
                foreach (DataRow row2 in MST_ITM_STUS1.Rows)
                {
                    dr = MST_ITM_STUS.NewRow();
                    dr["MIS_CD"] = row2["MIS_CD"].ToString();
                    dr["MIS_DESC"] = row2["MIS_DESC"].ToString();

                    MST_ITM_STUS.Rows.Add(dr);
                }

                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITRS_NITM_STUS"].ToString());
                foreach (DataRow row2 in MST_ITM_STUS1.Rows)
                {
                    dr = MST_ITM_STUS2.NewRow();
                    dr["MIS_CD"] = row2["MIS_CD"].ToString();
                    dr["MIS_DESC"] = row2["MIS_DESC"].ToString();

                    MST_ITM_STUS2.Rows.Add(dr);
                }
            }
            MST_ITM_STUS = MST_ITM_STUS.DefaultView.ToTable(true);

            S_dinReport1.Database.Tables["int_req"].SetDataSource(int_req);
            S_dinReport1.Database.Tables["int_req_ser"].SetDataSource(int_req_ser);
            S_dinReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            S_dinReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            S_dinReport1.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            S_dinReport1.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            S_dinReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            S_dinReport1.Database.Tables["MST_ITM_STUS_1"].SetDataSource(MST_ITM_STUS2);
            this.Text = "DIN Print";
            crystalReportViewer1.ReportSource = S_dinReport1;
            crystalReportViewer1.RefreshReport();
        }

        private void GRAN_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;
            DataTable int_req = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            //DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable MST_LOC_1 = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS1 = new DataTable();
            DataTable MST_ITM_STUS2 = new DataTable();
            DataRow dr;



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
            _granReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


            int_req = CHNLSVC.Inventory.GetInventoryRequestByReqNo(docNo);
            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));


            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));

            MST_ITM_STUS2.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS2.Columns.Add("MIS_DESC", typeof(string));

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable int_req_ser = new DataTable();
            DataTable serId = new DataTable();
            string _comCode = string.Empty;
            foreach (DataRow row in int_req.Rows)
            {


                MST_LOC = CHNLSVC.Sales.GetLocationCode(row["ITR_COM"].ToString(), row["ITR_LOC"].ToString());
                MST_LOC_1 = CHNLSVC.Sales.GetLocationCode(row["ITR_COM"].ToString(), row["ITR_REC_TO"].ToString());
                mst_com = CHNLSVC.General.GetCompanyByCode(row["ITR_COM"].ToString());
                _comCode = row["ITR_COM"].ToString();
                int_req_ser = CHNLSVC.Inventory.GetInventoryRequestSerialsBySeqNo(row["ITR_SEQ_NO"].ToString());


            }
            foreach (DataRow row in int_req_ser.Rows)
            {
                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_comCode, row["ITRS_ITM_CD"].ToString());
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITRS_ITM_STUS"].ToString());
                DataTable VIW_INV_MOVEMENT_SERIAL = new DataTable();
                VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(row["itrs_in_docno"].ToString());
                serId.Merge(VIW_INV_MOVEMENT_SERIAL);

                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    MST_ITM.Rows.Add(dr);
                }
                foreach (DataRow row2 in MST_ITM_STUS1.Rows)
                {
                    dr = MST_ITM_STUS.NewRow();
                    dr["MIS_CD"] = row2["MIS_CD"].ToString();
                    dr["MIS_DESC"] = row2["MIS_DESC"].ToString();

                    MST_ITM_STUS.Rows.Add(dr);
                }

                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITRS_NITM_STUS"].ToString());
                foreach (DataRow row2 in MST_ITM_STUS1.Rows)
                {
                    dr = MST_ITM_STUS2.NewRow();
                    dr["MIS_CD"] = row2["MIS_CD"].ToString();
                    dr["MIS_DESC"] = row2["MIS_DESC"].ToString();

                    MST_ITM_STUS2.Rows.Add(dr);
                }
            }
            serId = serId.DefaultView.ToTable(true);

            MST_ITM_STUS = MST_ITM_STUS.DefaultView.ToTable(true);
            MST_ITM_STUS2 = MST_ITM_STUS2.DefaultView.ToTable(true);
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            _granReport1.Database.Tables["int_req"].SetDataSource(int_req);
            _granReport1.Database.Tables["int_req_ser"].SetDataSource(int_req_ser);
            _granReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _granReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            _granReport1.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            _granReport1.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            _granReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            _granReport1.Database.Tables["MST_ITM_STUS_1"].SetDataSource(MST_ITM_STUS2);
            _granReport1.Database.Tables["serId"].SetDataSource(serId);
            this.Text = "GRAN Print";
            crystalReportViewer1.ReportSource = _granReport1;
            crystalReportViewer1.RefreshReport();




        }

        private void GRAN_print_full()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;
            DataTable int_req = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            //DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable MST_LOC_1 = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS1 = new DataTable();
            DataTable MST_ITM_STUS2 = new DataTable();
            DataRow dr;



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
            _granReportfull.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


            int_req = CHNLSVC.Inventory.GetInventoryRequestByReqNo(docNo);
            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));


            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));

            MST_ITM_STUS2.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS2.Columns.Add("MIS_DESC", typeof(string));

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable int_req_ser = new DataTable();
            DataTable serId = new DataTable();
            string _comCode = string.Empty;
            foreach (DataRow row in int_req.Rows)
            {


                MST_LOC = CHNLSVC.Sales.GetLocationCode(row["ITR_COM"].ToString(), row["ITR_LOC"].ToString());
                MST_LOC_1 = CHNLSVC.Sales.GetLocationCode(row["ITR_COM"].ToString(), row["ITR_REC_TO"].ToString());
                mst_com = CHNLSVC.General.GetCompanyByCode(row["ITR_COM"].ToString());
                _comCode = row["ITR_COM"].ToString();
                int_req_ser = CHNLSVC.Inventory.GetInventoryRequestSerialsBySeqNo(row["ITR_SEQ_NO"].ToString());


            }
            foreach (DataRow row in int_req_ser.Rows)
            {
                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_comCode, row["ITRS_ITM_CD"].ToString());
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITRS_ITM_STUS"].ToString());
                DataTable VIW_INV_MOVEMENT_SERIAL = new DataTable();
                DataTable _serial = new DataTable();
                DataTable _serial1 = new DataTable();
                _serial1 = CHNLSVC.Sales.GetMovementSerials(row["itrs_in_docno"].ToString());
                var filteredDataRows = _serial1.Select("its_itm_cd='" + row["itrs_itm_cd"].ToString() + "'");                
                if (filteredDataRows.Length != 0)
                    _serial = filteredDataRows.CopyToDataTable();
                else
                    _serial = _serial1.Clone();
                //VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(row["itrs_in_docno"].ToString());
                VIW_INV_MOVEMENT_SERIAL = _serial;

                //var _s = (from L in VIW_INV_MOVEMENT_SERIAL.AsEnumerable().Where(x => x.Field<int>("ITS_SER_ID") == Convert.ToInt32(row["ITRS_SER_ID"]) && x.Field<string>("ITH_DOC_NO") == row["ITRS_IN_DOCNO"].ToString())
                //          select new
                //          {
                //              ITS_DOC_NO = L.Field<string>("ITH_DOC_NO"),
                //              ITS_SER_ID = L.Field<string>("ITS_SER_ID"),
                //              ITS_WARR_NO = L.Field<string>("ITS_WARR_NO")
                //          }).ToList();
                //VIW_INV_MOVEMENT_SERIAL = LINQResultToDataTable(_s);

                serId.Merge(VIW_INV_MOVEMENT_SERIAL);

                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    MST_ITM.Rows.Add(dr);
                }
                foreach (DataRow row2 in MST_ITM_STUS1.Rows)
                {
                    dr = MST_ITM_STUS.NewRow();
                    dr["MIS_CD"] = row2["MIS_CD"].ToString();
                    dr["MIS_DESC"] = row2["MIS_DESC"].ToString();

                    MST_ITM_STUS.Rows.Add(dr);
                }

                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITRS_NITM_STUS"].ToString());
                foreach (DataRow row2 in MST_ITM_STUS1.Rows)
                {
                    dr = MST_ITM_STUS2.NewRow();
                    dr["MIS_CD"] = row2["MIS_CD"].ToString();
                    dr["MIS_DESC"] = row2["MIS_DESC"].ToString();

                    MST_ITM_STUS2.Rows.Add(dr);
                }
            }
            serId = serId.DefaultView.ToTable(true);

            MST_ITM_STUS = MST_ITM_STUS.DefaultView.ToTable(true);
            MST_ITM_STUS2 = MST_ITM_STUS2.DefaultView.ToTable(true);
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            _granReportfull.Database.Tables["int_req"].SetDataSource(int_req);
            _granReportfull.Database.Tables["int_req_ser"].SetDataSource(int_req_ser);
            _granReportfull.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _granReportfull.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            _granReportfull.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            _granReportfull.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            _granReportfull.Database.Tables["mst_com"].SetDataSource(mst_com);
            _granReportfull.Database.Tables["MST_ITM_STUS_1"].SetDataSource(MST_ITM_STUS2);
            _granReportfull.Database.Tables["serId"].SetDataSource(serId);
            this.Text = "GRAN Print";
            crystalReportViewer1.ReportSource = _granReportfull;
            crystalReportViewer1.RefreshReport();

        }

        public DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();

            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                    (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void SGRAN_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;
            DataTable int_req = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            //DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable MST_LOC_1 = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS1 = new DataTable();
            DataTable MST_ITM_STUS2 = new DataTable();
            DataRow dr;



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
            S_granReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


            int_req = CHNLSVC.Inventory.GetInventoryRequestByReqNo(docNo);
            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));


            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));

            MST_ITM_STUS2.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS2.Columns.Add("MIS_DESC", typeof(string));

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable int_req_ser = new DataTable();
            DataTable serId = new DataTable();
            string _comCode = string.Empty;
            foreach (DataRow row in int_req.Rows)
            {


                MST_LOC = CHNLSVC.Sales.GetLocationCode(row["ITR_COM"].ToString(), row["ITR_LOC"].ToString());
                MST_LOC_1 = CHNLSVC.Sales.GetLocationCode(row["ITR_COM"].ToString(), row["ITR_REC_TO"].ToString());
                mst_com = CHNLSVC.General.GetCompanyByCode(row["ITR_COM"].ToString());
                _comCode = row["ITR_COM"].ToString();
                int_req_ser = CHNLSVC.Inventory.GetInventoryRequestSerialsBySeqNo(row["ITR_SEQ_NO"].ToString());


            }
            foreach (DataRow row in int_req_ser.Rows)
            {
                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_comCode, row["ITRS_ITM_CD"].ToString());
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITRS_ITM_STUS"].ToString());
                DataTable VIW_INV_MOVEMENT_SERIAL = new DataTable();
                VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(row["itrs_in_docno"].ToString());
                serId.Merge(VIW_INV_MOVEMENT_SERIAL);

                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    MST_ITM.Rows.Add(dr);
                }
                foreach (DataRow row2 in MST_ITM_STUS1.Rows)
                {
                    dr = MST_ITM_STUS.NewRow();
                    dr["MIS_CD"] = row2["MIS_CD"].ToString();
                    dr["MIS_DESC"] = row2["MIS_DESC"].ToString();

                    MST_ITM_STUS.Rows.Add(dr);
                }

                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITRS_NITM_STUS"].ToString());
                foreach (DataRow row2 in MST_ITM_STUS1.Rows)
                {
                    dr = MST_ITM_STUS2.NewRow();
                    dr["MIS_CD"] = row2["MIS_CD"].ToString();
                    dr["MIS_DESC"] = row2["MIS_DESC"].ToString();

                    MST_ITM_STUS2.Rows.Add(dr);
                }
            }
            serId = serId.DefaultView.ToTable(true);

            MST_ITM_STUS = MST_ITM_STUS.DefaultView.ToTable(true);
            MST_ITM_STUS2 = MST_ITM_STUS2.DefaultView.ToTable(true);
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            S_granReport1.Database.Tables["int_req"].SetDataSource(int_req);
            S_granReport1.Database.Tables["int_req_ser"].SetDataSource(int_req_ser);
            S_granReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            S_granReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            S_granReport1.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            S_granReport1.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            S_granReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            S_granReport1.Database.Tables["MST_ITM_STUS_1"].SetDataSource(MST_ITM_STUS2);
            S_granReport1.Database.Tables["serId"].SetDataSource(serId);
            this.Text = "GRAN Print";
            crystalReportViewer1.ReportSource = S_granReport1;
            crystalReportViewer1.RefreshReport();
        }

        public void MRN_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;
            DataTable int_req = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable MST_ITM_STUS1 = new DataTable();
            DataTable MST_LOC_1 = new DataTable();
            DataTable int_req_itm = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            int_req = CHNLSVC.Inventory.GetInventoryRequestByReqNo(docNo);

            DataTable sec_user = new DataTable();
            DataTable sec_user1 = new DataTable();

            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;
            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _mrnReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();
            DataRow dr;

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            MST_ITM.Columns.Add("MI_ITM_UOM", typeof(string));

            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));


            string _comCode = string.Empty;
            foreach (DataRow row in int_req.Rows)
            {


                MST_LOC = CHNLSVC.Sales.GetLocationCode(row["ITR_COM"].ToString(), row["ITR_LOC"].ToString());
                MST_LOC_1 = CHNLSVC.Sales.GetLocationCode(row["ITR_COM"].ToString(), row["ITR_ISSUE_FROM"].ToString());
                mst_com = CHNLSVC.General.GetCompanyByCode(row["ITR_COM"].ToString());

                _comCode = row["ITR_COM"].ToString();
                int_req_itm = CHNLSVC.Inventory.GetInventoryRequestItemsBySeqNo(row["ITR_SEQ_NO"].ToString());
                sec_user = CHNLSVC.Inventory.GetUserNameByUserID(row["itr_cre_by"].ToString());



            }
            foreach (DataRow row in int_req_itm.Rows)
            {
                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_comCode, row["ITRI_ITM_CD"].ToString());

                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITRI_ITM_STUS"].ToString());

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

            if (GlbReportName == "MRNRepPrints.rpt")
            {
                foreach (DataRow rowj in int_req.Rows)
                {
                    BaseCls.GlbReportJobNo = rowj["itr_job_no"].ToString();
                }
                //kapila 17/6/2015
                DataTable ServiceJobSerials = new DataTable();
                ServiceJobSerials = CHNLSVC.CustService.getServicejobDet(BaseCls.GlbReportJobNo, 0);

                DataTable ServiceJobSerialsSub = new DataTable();
                ServiceJobSerialsSub = CHNLSVC.CustService.GetServiceJobDetailSubItemsData(BaseCls.GlbReportJobNo, 0);

                _mrnReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _mrnReport1.Database.Tables["int_req"].SetDataSource(int_req);
                _mrnReport1.Database.Tables["int_req_itm"].SetDataSource(int_req_itm);
                _mrnReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _mrnReport1.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _mrnReport1.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _mrnReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
                _mrnReport1.Database.Tables["sec_user"].SetDataSource(sec_user);

                foreach (object repOp in _mrnReport1.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Job_Items")
                        {
                            ReportDocument subRepDoc = _mrnReport1.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobSerials);

                        }
                        if (_cs.SubreportName == "Job_Items_Sub")
                        {
                            ReportDocument subRepDoc = _mrnReport1.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["SCV_JOB_DET_SUB"].SetDataSource(ServiceJobSerialsSub);

                        }

                    }
                }


                this.Text = "MRN Print";
            };

            if (GlbReportName == "InterTransfer.rpt")
            {

                _interTrans.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _interTrans.Database.Tables["int_req"].SetDataSource(int_req);
                _interTrans.Database.Tables["int_req_itm"].SetDataSource(int_req_itm);
                _interTrans.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _interTrans.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _interTrans.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _interTrans.Database.Tables["mst_com"].SetDataSource(mst_com);


                this.Text = "Inter Transfer";
            };


            crystalReportViewer1.ReportSource = _mrnReport1;
            crystalReportViewer1.RefreshReport();




        }

        private void SMRN_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;
            DataTable int_req = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable MST_ITM_STUS1 = new DataTable();
            DataTable MST_LOC_1 = new DataTable();
            DataTable int_req_itm = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            int_req = CHNLSVC.Inventory.GetInventoryRequestByReqNo(docNo);

            DataTable sec_user = new DataTable();
            DataTable sec_user1 = new DataTable();

            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;
            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            S_mrnReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();
            DataRow dr;

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));


            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));


            string _comCode = string.Empty;
            foreach (DataRow row in int_req.Rows)
            {


                MST_LOC = CHNLSVC.Sales.GetLocationCode(row["ITR_COM"].ToString(), row["ITR_LOC"].ToString());
                MST_LOC_1 = CHNLSVC.Sales.GetLocationCode(row["ITR_COM"].ToString(), row["ITR_ISSUE_FROM"].ToString());
                mst_com = CHNLSVC.General.GetCompanyByCode(row["ITR_COM"].ToString());

                _comCode = row["ITR_COM"].ToString();
                int_req_itm = CHNLSVC.Inventory.GetInventoryRequestItemsBySeqNo(row["ITR_SEQ_NO"].ToString());
                sec_user = CHNLSVC.Inventory.GetUserNameByUserID(row["itr_cre_by"].ToString());



            }
            foreach (DataRow row in int_req_itm.Rows)
            {
                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_comCode, row["ITRI_ITM_CD"].ToString());

                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITRI_ITM_STUS"].ToString());

                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
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


            if (GlbReportName == "SMRNRepPrints.rpt")
            {

                S_mrnReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                S_mrnReport1.Database.Tables["int_req"].SetDataSource(int_req);
                S_mrnReport1.Database.Tables["int_req_itm"].SetDataSource(int_req_itm);
                S_mrnReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                S_mrnReport1.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                S_mrnReport1.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                S_mrnReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
                S_mrnReport1.Database.Tables["sec_user"].SetDataSource(sec_user);


                this.Text = "MRN Print";
            };

            if (GlbReportName == "SInterTransfer.rpt")
            {

                S_interTrans.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                S_interTrans.Database.Tables["int_req"].SetDataSource(int_req);
                S_interTrans.Database.Tables["int_req_itm"].SetDataSource(int_req_itm);
                S_interTrans.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                S_interTrans.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                S_interTrans.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                S_interTrans.Database.Tables["mst_com"].SetDataSource(mst_com);


                this.Text = "Inter Transfer";
            };


            crystalReportViewer1.ReportSource = S_mrnReport1;
            crystalReportViewer1.RefreshReport();


        }

        private void DealerMRN_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;
            DataTable int_req = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable MST_ITM_STUS1 = new DataTable();
            DataTable MST_LOC_1 = new DataTable();
            DataTable int_req_itm = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            int_req = CHNLSVC.Inventory.GetInventoryRequestByReqNo(docNo);

            DataTable sec_user = new DataTable();
            DataTable sec_user1 = new DataTable();

            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;
            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            D_mrnReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();
            DataRow dr;

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));


            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));


            string _comCode = string.Empty;
            foreach (DataRow row in int_req.Rows)
            {


                MST_LOC = CHNLSVC.Sales.GetLocationCode(row["ITR_COM"].ToString(), row["ITR_LOC"].ToString());
                MST_LOC_1 = CHNLSVC.Sales.GetLocationCode(row["ITR_COM"].ToString(), row["ITR_ISSUE_FROM"].ToString());
                mst_com = CHNLSVC.General.GetCompanyByCode(row["ITR_COM"].ToString());

                _comCode = row["ITR_COM"].ToString();
                int_req_itm = CHNLSVC.Inventory.GetInventoryRequestItemsBySeqNo(row["ITR_SEQ_NO"].ToString());
                sec_user = CHNLSVC.Inventory.GetUserNameByUserID(row["itr_cre_by"].ToString());



            }
            foreach (DataRow row in int_req_itm.Rows)
            {
                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_comCode, row["ITRI_ITM_CD"].ToString());

                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITRI_ITM_STUS"].ToString());

                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
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


            if (GlbReportName == "Dealer_MRNRepPrints.rpt")
            {

                D_mrnReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                D_mrnReport1.Database.Tables["int_req"].SetDataSource(int_req);
                D_mrnReport1.Database.Tables["int_req_itm"].SetDataSource(int_req_itm);
                D_mrnReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                D_mrnReport1.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                D_mrnReport1.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                D_mrnReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
                D_mrnReport1.Database.Tables["sec_user"].SetDataSource(sec_user);


                this.Text = "MRN Print";
            };

            if (GlbReportName == "SInterTransfer.rpt")
            {

                S_interTrans.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                S_interTrans.Database.Tables["int_req"].SetDataSource(int_req);
                S_interTrans.Database.Tables["int_req_itm"].SetDataSource(int_req_itm);
                S_interTrans.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                S_interTrans.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                S_interTrans.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                S_interTrans.Database.Tables["mst_com"].SetDataSource(mst_com);


                this.Text = "Inter Transfer";
            };


            crystalReportViewer1.ReportSource = D_mrnReport1;
            crystalReportViewer1.RefreshReport();


        }

        private void RCC_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;
            DataTable int_rcc = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();

            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;
            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _rccReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            int_rcc = CHNLSVC.Inventory.GetRCCbyNoTable(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();

            string _comCode = string.Empty;
            foreach (DataRow row in int_rcc.Rows)
            {

                MST_LOC = CHNLSVC.Sales.GetLocationCode(row["INR_COM_CD"].ToString(), row["INR_LOC_CD"].ToString());
                mst_com = CHNLSVC.General.GetCompanyByCode(row["INR_COM_CD"].ToString());
                MST_ITM = CHNLSVC.Sales.GetItemCode(_comCode, row["INR_ITM"].ToString());
            }


            _rccReport1.Database.Tables["int_rcc"].SetDataSource(int_rcc);
            _rccReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _rccReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            _rccReport1.Database.Tables["mst_com"].SetDataSource(mst_com);


            this.Text = "RCC Print";
            crystalReportViewer1.ReportSource = _rccReport1;
            crystalReportViewer1.RefreshReport();




        }

        private void SRCC_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;
            DataTable int_rcc = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();

            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;
            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            S_rccReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            int_rcc = CHNLSVC.Inventory.GetRCCbyNoTable(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();

            string _comCode = string.Empty;
            foreach (DataRow row in int_rcc.Rows)
            {

                MST_LOC = CHNLSVC.Sales.GetLocationCode(row["INR_COM_CD"].ToString(), row["INR_LOC_CD"].ToString());
                mst_com = CHNLSVC.General.GetCompanyByCode(row["INR_COM_CD"].ToString());
                MST_ITM = CHNLSVC.Sales.GetItemCode(_comCode, row["INR_ITM"].ToString());
            }


            S_rccReport1.Database.Tables["int_rcc"].SetDataSource(int_rcc);
            S_rccReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            S_rccReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            S_rccReport1.Database.Tables["mst_com"].SetDataSource(mst_com);


            this.Text = "RCC Print";
            crystalReportViewer1.ReportSource = S_rccReport1;
            crystalReportViewer1.RefreshReport();


        }

        private void Fixed_Trans_print_1()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            DataTable int_adhoc_hdr = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable int_adhoc_det = new DataTable();
            DataTable inr_ser = new DataTable();
            DataTable inr_ser1 = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            inr_ser.Columns.Add("INS_SER_ID", typeof(double));
            inr_ser.Columns.Add("INS_SER_1", typeof(string));
            inr_ser.Columns.Add("INS_SER_2", typeof(string));

            int _numPages = 0;
            DataRow dr3;
            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));
            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;
            PRINT_DOC.Rows.Add(dr3);
            _fixTrnsReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            int_adhoc_hdr = CHNLSVC.Inventory.GetAdhochdrTable(docNo);
            int_adhoc_det = CHNLSVC.Inventory.GetAdhocdetTable(docNo);
            string _comCode = default(string);

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));

            DataTable mst_com = new DataTable();
            DataTable int_req_ser = new DataTable();
            string _loc = default(string);


            foreach (DataRow row in int_adhoc_hdr.Rows)
            {
                MST_LOC = CHNLSVC.Sales.GetLocationCode(row["IADH_COM"].ToString(), row["IADH_LOC"].ToString());
                mst_com = CHNLSVC.General.GetCompanyByCode(row["IADH_COM"].ToString());
                _comCode = row["IADH_COM"].ToString();
                _loc = row["IADH_LOC"].ToString();
            }
            DataRow dr;
            foreach (DataRow row in int_adhoc_det.Rows)
            {
                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_comCode, row["IADD_CLAIM_ITM"].ToString());

                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    MST_ITM.Rows.Add(dr);
                }

                inr_ser1 = CHNLSVC.Inventory.GetInventorySerialbyId(row["IADD_ANAL4"].ToString(), _loc);
                foreach (DataRow row1 in inr_ser1.Rows)
                {
                    dr = inr_ser.NewRow();
                    dr["INS_SER_ID"] = row1["INS_SER_ID"].ToString();
                    dr["INS_SER_1"] = row1["INS_SER_1"].ToString();
                    dr["INS_SER_2"] = row1["INS_SER_2"].ToString();
                    inr_ser.Rows.Add(dr);
                }
            }

            MST_ITM = MST_ITM.DefaultView.ToTable(true);

            inr_ser = inr_ser.DefaultView.ToTable(true);

            _fixTrnsReport1.Database.Tables["INT_ADHOC_HDR"].SetDataSource(int_adhoc_hdr);
            _fixTrnsReport1.Database.Tables["INT_ADHOC_DET"].SetDataSource(int_adhoc_det);
            _fixTrnsReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _fixTrnsReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            _fixTrnsReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            _fixTrnsReport1.Database.Tables["inr_ser"].SetDataSource(inr_ser);

            this.Text = "Fixed Assests Transfer Note Print";
            crystalReportViewer1.ReportSource = _fixTrnsReport1;
            crystalReportViewer1.RefreshReport();
        }

        private void Fixed_Trans_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

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
            _fixTrnsReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


            DataTable int_adhoc_hdr = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable int_adhoc_det = new DataTable();
            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));


            int_adhoc_hdr = CHNLSVC.Inventory.GetAdhochdrTable(docNo);
            int_adhoc_det = CHNLSVC.Inventory.GetAdhocdetTable(docNo);
            string _comCode = default(string);
            string _loc = default(string);

            DataTable mst_com = new DataTable();
            DataTable inr_ser = new DataTable();
            DataTable inr_ser1 = new DataTable();

            inr_ser.Columns.Add("INS_SER_ID", typeof(double));
            inr_ser.Columns.Add("INS_SER_1", typeof(string));
            inr_ser.Columns.Add("INS_SER_2", typeof(string));

            foreach (DataRow row in int_adhoc_hdr.Rows)
            {
                MST_LOC = CHNLSVC.Sales.GetLocationCode(row["IADH_COM"].ToString(), row["IADH_LOC"].ToString());
                mst_com = CHNLSVC.General.GetCompanyByCode(row["IADH_COM"].ToString());
                _comCode = row["IADH_COM"].ToString();
                _loc = row["IADH_LOC"].ToString();
            }
            DataRow dr;
            foreach (DataRow row in int_adhoc_det.Rows)
            {
                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_comCode, row["IADD_CLAIM_ITM"].ToString());


                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    MST_ITM.Rows.Add(dr);
                }

                inr_ser1 = CHNLSVC.Inventory.GetInventorySerialbyId(row["IADD_ANAL4"].ToString(), _loc);
                foreach (DataRow row1 in inr_ser1.Rows)
                {
                    dr = inr_ser.NewRow();
                    dr["INS_SER_ID"] = row1["INS_SER_ID"].ToString();
                    dr["INS_SER_1"] = row1["INS_SER_1"].ToString();
                    dr["INS_SER_2"] = row1["INS_SER_2"].ToString();
                    inr_ser.Rows.Add(dr);
                }

            }
            inr_ser = inr_ser.DefaultView.ToTable(true);
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            _fixTrnsReport1.Database.Tables["int_adhoc_hdr"].SetDataSource(int_adhoc_hdr);
            _fixTrnsReport1.Database.Tables["int_adhoc_det"].SetDataSource(int_adhoc_det);
            _fixTrnsReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _fixTrnsReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            _fixTrnsReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            _fixTrnsReport1.Database.Tables["inr_ser"].SetDataSource(inr_ser);

            this.Text = "Fixed Assests Transfer Request Note Print";
            crystalReportViewer1.ReportSource = _fixTrnsReport1;
            crystalReportViewer1.RefreshReport();
        }
        private void SFixed_Trans_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            DataTable int_adhoc_hdr = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable int_adhoc_det = new DataTable();
            DataTable inr_ser = new DataTable();
            DataTable inr_ser1 = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            inr_ser.Columns.Add("INS_SER_ID", typeof(double));
            inr_ser.Columns.Add("INS_SER_1", typeof(string));
            inr_ser.Columns.Add("INS_SER_2", typeof(string));

            int _numPages = 0;
            DataRow dr3;
            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            S_fixTrnsReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            int_adhoc_hdr = CHNLSVC.Inventory.GetAdhochdrTable(docNo);
            int_adhoc_det = CHNLSVC.Inventory.GetAdhocdetTable(docNo);
            string _comCode = default(string);

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));

            DataTable mst_com = new DataTable();
            DataTable int_req_ser = new DataTable();
            string _loc = default(string);


            foreach (DataRow row in int_adhoc_hdr.Rows)
            {
                MST_LOC = CHNLSVC.Sales.GetLocationCode(row["IADH_COM"].ToString(), row["IADH_LOC"].ToString());
                mst_com = CHNLSVC.General.GetCompanyByCode(row["IADH_COM"].ToString());
                _comCode = row["IADH_COM"].ToString();
                _loc = row["IADH_LOC"].ToString();
            }
            DataRow dr;
            foreach (DataRow row in int_adhoc_det.Rows)
            {
                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_comCode, row["IADD_CLAIM_ITM"].ToString());

                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    MST_ITM.Rows.Add(dr);
                }

                inr_ser1 = CHNLSVC.Inventory.GetInventorySerialbyId(row["IADD_ANAL4"].ToString(), _loc);
                foreach (DataRow row1 in inr_ser1.Rows)
                {
                    dr = inr_ser.NewRow();
                    dr["INS_SER_ID"] = row1["INS_SER_ID"].ToString();
                    dr["INS_SER_1"] = row1["INS_SER_1"].ToString();
                    dr["INS_SER_2"] = row1["INS_SER_2"].ToString();
                    inr_ser.Rows.Add(dr);
                }
            }

            MST_ITM = MST_ITM.DefaultView.ToTable(true);

            inr_ser = inr_ser.DefaultView.ToTable(true);

            S_fixTrnsReport1.Database.Tables["int_adhoc_hdr"].SetDataSource(int_adhoc_hdr);
            S_fixTrnsReport1.Database.Tables["int_adhoc_det"].SetDataSource(int_adhoc_det);
            S_fixTrnsReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            S_fixTrnsReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            S_fixTrnsReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            S_fixTrnsReport1.Database.Tables["inr_ser"].SetDataSource(inr_ser);

            this.Text = "Fixed Assests Transfer Note Print";
            crystalReportViewer1.ReportSource = S_fixTrnsReport1;
            crystalReportViewer1.RefreshReport();
        }



        private void Fixed_Trans_confirmation_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

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
            _fixConReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


            DataTable int_adhoc_hdr = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable int_adhoc_det = new DataTable();
            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));


            int_adhoc_hdr = CHNLSVC.Inventory.GetAdhochdrTable(docNo);
            int_adhoc_det = CHNLSVC.Inventory.GetAdhocdetTable(docNo);
            string _comCode = default(string);
            string _loc = default(string);

            DataTable mst_com = new DataTable();
            DataTable inr_ser = new DataTable();
            DataTable inr_ser1 = new DataTable();

            inr_ser.Columns.Add("INS_SER_ID", typeof(double));
            inr_ser.Columns.Add("INS_SER_1", typeof(string));
            inr_ser.Columns.Add("INS_SER_2", typeof(string));

            foreach (DataRow row in int_adhoc_hdr.Rows)
            {
                MST_LOC = CHNLSVC.Sales.GetLocationCode(row["IADH_COM"].ToString(), row["IADH_LOC"].ToString());
                mst_com = CHNLSVC.General.GetCompanyByCode(row["IADH_COM"].ToString());
                _comCode = row["IADH_COM"].ToString();
                _loc = row["IADH_LOC"].ToString();
            }
            DataRow dr;
            foreach (DataRow row in int_adhoc_det.Rows)
            {
                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_comCode, row["IADD_CLAIM_ITM"].ToString());


                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    MST_ITM.Rows.Add(dr);
                }

                inr_ser1 = CHNLSVC.Inventory.GetInventorySerialbyId(row["IADD_ANAL4"].ToString(), _loc);
                foreach (DataRow row1 in inr_ser1.Rows)
                {
                    dr = inr_ser.NewRow();
                    dr["INS_SER_ID"] = row1["INS_SER_ID"].ToString();
                    dr["INS_SER_1"] = row1["INS_SER_1"].ToString();
                    dr["INS_SER_2"] = row1["INS_SER_2"].ToString();
                    inr_ser.Rows.Add(dr);
                }

            }
            inr_ser = inr_ser.DefaultView.ToTable(true);
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            _fixConReport1.Database.Tables["int_adhoc_hdr"].SetDataSource(int_adhoc_hdr);
            _fixConReport1.Database.Tables["int_adhoc_det"].SetDataSource(int_adhoc_det);
            _fixConReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _fixConReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            _fixConReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            _fixConReport1.Database.Tables["inr_ser"].SetDataSource(inr_ser);

            this.Text = "Fixed Assests Confirmation Note Print";
            crystalReportViewer1.ReportSource = _fixConReport1;
            crystalReportViewer1.RefreshReport();
        }

        private void RevetAdjPrint()
        {

            obj.Revert_Adj_print();

            crystalReportViewer1.ReportSource = obj._recRvtrpt;

            this.Text = "Revert Adjustment";
            crystalReportViewer1.RefreshReport();
        }

        private void SRevetAdjPrint()
        {
            obj.SRevert_Adj_print();

            crystalReportViewer1.ReportSource = obj.S_recRvtrpt;

            this.Text = "Revert Adjustment";
            crystalReportViewer1.RefreshReport();
        }

        private void WarrantyClimInNote()
        {

            obj.WarrantyClimInNote();

            crystalReportViewer1.ReportSource = obj._warrClaim;

            this.Text = "Warranty Claim In Note";
            crystalReportViewer1.RefreshReport();
        }

        private void inventoryBalanceWithCost()
        {

            obj.inventoryBalanceWithCost();

            if (BaseCls.GlbReportDocType == "NOR")
            {
                crystalReportViewer1.ReportSource = obj._invBalCst;
            }
            else
            {
                crystalReportViewer1.ReportSource = obj._invBalCstAST;
            }
            this.Text = "Inventory Balance With Cost";
            crystalReportViewer1.RefreshReport();
        }

        private void inventoryBalanceWithSerial()
        {
            if (BaseCls.GlbReportType == "ASAT")
            {
                obj.inventoryBalanceSerial_Asat();
                crystalReportViewer1.ReportSource = obj._invBalSrlAsat;

                this.Text = "Inventory Balance With Serial";
                crystalReportViewer1.RefreshReport();
            }
            else
            {
                obj.inventoryBalanceSerial();
                crystalReportViewer1.ReportSource = obj._invBalSrl;

                this.Text = "Inventory Balance With Serial";
                crystalReportViewer1.RefreshReport();
            }


        }

        private void Loc_wise_item_age()
        {  // kapila 9/9/2016
            obj.Loc_wise_item_age();
            if (BaseCls.GlbReportName == "CatScatwiseItemAge.rpt")
                crystalReportViewer1.ReportSource = obj._catScatAge;
            if (BaseCls.GlbReportName == "CatwiseItemAge.rpt")
                crystalReportViewer1.ReportSource = obj._catAge;
            if (BaseCls.GlbReportName == "ItmBrndwiseItemAge.rpt")
                crystalReportViewer1.ReportSource = obj._ItmBrndAge;
            if (BaseCls.GlbReportName == "CatItmwiseItemAge.rpt")
                crystalReportViewer1.ReportSource = obj._catItmAge;
            if (BaseCls.GlbReportName == "LocwiseItemAge.rpt")
                crystalReportViewer1.ReportSource = obj._locAge;
            if (BaseCls.GlbReportName == "ItmwiseItemAge.rpt")
                crystalReportViewer1.ReportSource = obj._ItmAge;
            if (BaseCls.GlbReportName == "LocItemStusAgenew.rpt")
                crystalReportViewer1.ReportSource = obj._itmwise;
            if (BaseCls.GlbReportName == "LocItemStusAge.rpt")
                crystalReportViewer1.ReportSource = obj._LocItmAge;

            this.Text = "Item Age Analysis";
            crystalReportViewer1.RefreshReport();
        }




        private void FastMovingItems()
        {

            obj.FastMovingItems();
            if (BaseCls.GlbReportName == "FastMovingItems.rpt")
            {
                crystalReportViewer1.ReportSource = obj._fastMov;
            }
            else
            {
                crystalReportViewer1.ReportSource = obj._nonMov;
            }


            this.Text = "Fast Moving Items";
            crystalReportViewer1.RefreshReport();
        }


        private void StockBalancewithAge()
        {

            obj.SerialAgeReport();
            crystalReportViewer1.ReportSource = obj._serAge;

            this.Text = "StockBalance With Serial Age";
            crystalReportViewer1.RefreshReport();
        }

        private void FIFONotFollowed()
        {

            obj.FIFONotFollowedReport();
            crystalReportViewer1.ReportSource = obj._FIFO;

            this.Text = "FIFO NOT FOLLOWED REPORT";
            crystalReportViewer1.RefreshReport();
        }

        private void ExcessShort_print()
        {
            obj.ExcessStockReport();
            crystalReportViewer1.ReportSource = obj._Esxcessstk;

            this.Text = "EXCESS SHORT STOCK REPORT";
            crystalReportViewer1.RefreshReport();
        }


        private void POSummary()
        {  // Nadeeka 29-04-2015
            obj.POSummary();
            crystalReportViewer1.ReportSource = obj._poSummary;
            this.Text = "Purchar Order Summary";
            crystalReportViewer1.RefreshReport();
        }


        private void InventoryStatement()
        {  // Nadeeka 26-02-2013
            obj.inventoryStatement();
            crystalReportViewer1.ReportSource = obj._invSts;
            this.Text = "Inventory Statement";
            crystalReportViewer1.RefreshReport();
        }
        private void PrintPSI_Report()
        {
            obj.PrintPSI_Report();
            crystalReportViewer1.ReportSource = obj._PSI;
            this.Text = "";
            crystalReportViewer1.RefreshReport();
        }
        private void InsuredStock()
        {  // Sanjeewa 13-07-2015
            obj.InsuredStock_Report();
            crystalReportViewer1.ReportSource = obj._insustk;
            this.Text = "Insured Stock Report";
            crystalReportViewer1.RefreshReport();
        }

        private void InventoryStatementTr()
        {  // Sanjeewa 21-06-2013
            obj.inventoryStatement();
            crystalReportViewer1.ReportSource = obj._invStsTr;
            this.Text = "Inventory Statement (Transaction Listing)";
            crystalReportViewer1.RefreshReport();
        }

        private void GITAsat_Report()
        {  // Sanjeewa 02-11-2016
            obj.GITAsat_Report();
            crystalReportViewer1.ReportSource = obj._GitAsat;
            this.Text = "GIT As At Report";
            crystalReportViewer1.RefreshReport();
        }
        private void InventoryStatementTr2()
        {  // Sanjeewa 21-06-2013
            obj.inventoryStatement();
            crystalReportViewer1.ReportSource = obj._invStsTr2;
            this.Text = "Inventory Statement (Transaction Listing)";
            crystalReportViewer1.RefreshReport();
        }

        private void InventoryStatementTr3()
        {  // Sanjeewa 21-06-2013
            obj.inventoryStatement();
            crystalReportViewer1.ReportSource = obj._invStsTr3;
            this.Text = "Inventory Statement (Transaction Listing)";
            crystalReportViewer1.RefreshReport();
        }

        private void InventoryStatementTr3_new()
        {  // 
            obj.inventoryStatement();
            crystalReportViewer1.ReportSource = obj._invStsTr3_new;
            this.Text = "Inventory Statement (Transaction Listing)";
            crystalReportViewer1.RefreshReport();
        }

        private void OtherShopDO_print()
        {  // Sanjeewa 14-10-2013
            obj.OthershopDO_Report();
            crystalReportViewer1.ReportSource = obj._OthShopDO;
            this.Text = "CUSTOMER DELIVERIERS - OTHER PROFITCENTERS";
            crystalReportViewer1.RefreshReport();
        }

        private void Reserved_Serial_print()
        {  // Sanjeewa 11-12-2013
            obj.ReservedSerial_Report();
            crystalReportViewer1.ReportSource = obj._ResSer;
            this.Text = "RESERVED SERIALS TO CUSTOMERS";
            crystalReportViewer1.RefreshReport();
        }

        private void serialCompanyLocationAge()
        {  //Wimal 21-08-2013
            //obj.serilaCompanyLocationAge();
            //crystalReportViewer1.ReportSource = obj._serialAge;
            //this.Text = "Comapany Location Age";
            //crystalReportViewer1.RefreshReport();
        }
        private void GvPrint()
        { // Sanjeewa 25-11-2013
            obj.MovementAuditTrialSerial();
            //crystalReportViewer1.ReportSource = obj._invAuditSrl;
            //this.Text = "Gift Voucher Print";
            //crystalReportViewer1.RefreshReport();
        }

        private void MovementAuditTrialSerial()
        { // Nadeeka 27-02-2013
            obj.MovementAuditTrialSerial();
            crystalReportViewer1.ReportSource = obj._invAuditSrl;
            this.Text = "Inventory Audit Trial with Serial";
            crystalReportViewer1.RefreshReport();
        }


        private void BOCReservedSerialDetails()
        { //kapila
            obj.BOCReservedSerialDetails();
            crystalReportViewer1.ReportSource = obj._BOCRes;
            this.Text = "BOC Reserved Serials";
            crystalReportViewer1.RefreshReport();
        }

        private void ReorderItems()
        { //
            obj.ReorderItems();
            crystalReportViewer1.ReportSource = obj._reOrder;
            this.Text = "Re Order Items";
            crystalReportViewer1.RefreshReport();
        }

        private void TempSavedDocs()
        { //Sanjeewa
            obj.TempSaveDoc_Report();
            crystalReportViewer1.ReportSource = obj._tempsave;
            this.Text = "Temporary saved documents";
            crystalReportViewer1.RefreshReport();
        }

        private void CurrBalPrice()
        { //Sanjeewa
            obj.CurrBalwithPrice_Report();
            crystalReportViewer1.ReportSource = obj._CurrBalPrice;
            this.Text = "Temporary saved documents";
            crystalReportViewer1.RefreshReport();
        }

        private void CurrAge()
        { //Sanjeewa
            obj.Curr_Age_Report();
            crystalReportViewer1.ReportSource = obj._CurrAge;
            this.Text = "Current Age Report";
            crystalReportViewer1.RefreshReport();
        }
        private void DFInvBalPricePrint()
        { // Sanjeewa 10-10-2014
            obj.DutyFreeInventoryBalwithPriceReport();
            crystalReportViewer1.ReportSource = obj._dfInvBal;
            this.Text = "DUTY FREEINVENTORY BALANCE WITH PRICE";
            crystalReportViewer1.RefreshReport();
        }

        private void DamegeApproval()
        { // Nadeeka 27-02-2013
            obj.DamageGoodsApproval();
            crystalReportViewer1.ReportSource = obj._dmgApp;
            this.Text = "Approval Process Status for Damage Goods";
            crystalReportViewer1.RefreshReport();
        }

        private void InventoryBalance()
        { // Sanjeewa 01-03-2013
            obj.inventoryBalance();
            if (BaseCls.GlbReportDocType == "NOR")
            {
                crystalReportViewer1.ReportSource = obj._invBal;
            }
            else
            {
                crystalReportViewer1.ReportSource = obj._invBalAST;
            }
            this.Text = "Inventory Balance as at Date";
            crystalReportViewer1.RefreshReport();
        }

        private void GRANReport()
        { // Sanjeewa 25-03-2013
            obj.GRAN_Report();
            crystalReportViewer1.ReportSource = obj._GRAN;
            this.Text = "GRAN / DIN Report";
            crystalReportViewer1.RefreshReport();
        }

        private void FATReport()
        { // Sanjeewa 05-02-2014
            obj.FAT_Report();
            crystalReportViewer1.ReportSource = obj._FAT;
            this.Text = "FIXED ASSET TRANSFER DETAILS";
            crystalReportViewer1.RefreshReport();
        }

        private void EXCNG_print()
        { // Sanjeewa 08-10-2013
            obj.ExchangePrintReport();
            if (BaseCls.GlbReportName == "Exchange_Docs.rpt")
            {
                crystalReportViewer1.ReportSource = obj._ExchangeRep;
            }
            else
            {
                crystalReportViewer1.ReportSource = obj._ExchangeRepfull;
            }
            this.Text = "Exchange Print";
            crystalReportViewer1.RefreshReport();
        }

        private void SEXCNG_print()
        { // Sanjeewa 08-10-2013
            obj.SExchangePrintReport();
            crystalReportViewer1.ReportSource = obj.S_ExchangeRep;
            this.Text = "Exchange Print";
            crystalReportViewer1.RefreshReport();
        }

        private void IntrReport()
        { // Sanjeewa 23-08-2013
            obj.Intr_Report();
            crystalReportViewer1.ReportSource = obj._Intr;
            this.Text = "Inter Transfer Details Report";
            crystalReportViewer1.RefreshReport();
        }

        private void POReport()
        { // Sanjeewa 25-03-2013
            obj.PurcaseOrderPrint();
            crystalReportViewer1.ReportSource = obj._porpt;
            this.Text = "Purchase Order";
            crystalReportViewer1.RefreshReport();
        }

        private void POReportAST()
        { // Sanjeewa 07-11-2015
            obj.PurcaseOrderPrint_AST();
            crystalReportViewer1.ReportSource = obj._porptast;
            this.Text = "Purchase Order";
            crystalReportViewer1.RefreshReport();
        }
        private void PurcaseOrderPrintHalfLetter()
        { // Sanjeewa 25-03-2013
            obj.PurcaseOrderPrintHalfLetter();
            crystalReportViewer1.ReportSource = obj._porpt;
            this.Text = "Purchase Order";
            crystalReportViewer1.RefreshReport();
        }
        private void InventoryBalanceCurr()
        { // Sanjeewa 01-03-2013
            obj.inventoryBalance_Current();

            if (BaseCls.GlbReportDocType == "NOR")
            {
                if (BaseCls.GlbReportWithDetail == 1)
                {
                    if (BaseCls.GlbReportWithStatus == 1)
                        crystalReportViewer1.ReportSource = obj._invBal;
                    else
                        crystalReportViewer1.ReportSource = obj._invBalWOStus;
                }

                else
                    crystalReportViewer1.ReportSource = obj._invBalWODet;
            }
            else
            {
                crystalReportViewer1.ReportSource = obj._invBalAST;
            }
            this.Text = "Current Inventory Balance";
            crystalReportViewer1.RefreshReport();
        }

        private void MovementAuditTrialNew()
        {
            obj.Movement_Audit_Trial();   //29/9/2016
            if (BaseCls.GlbReportName == "Movement_audit_trial.rpt")
                crystalReportViewer1.ReportSource = obj._moveAuditTrial;

            if (BaseCls.GlbReportName == "Movement_audit_trial_det.rpt")
                crystalReportViewer1.ReportSource = obj._moveAuditTrialDet;

            if (BaseCls.GlbReportName == "Movement_audit_trial_summary.rpt")
                crystalReportViewer1.ReportSource = obj._moveAuditTrialSum;

            if (BaseCls.GlbReportName == "Movement_audit_trial_sum.rpt")
                crystalReportViewer1.ReportSource = obj._moveAuditTrialList;

            if (BaseCls.GlbReportName == "Movement_audit_trial_ser_cost.rpt")
                crystalReportViewer1.ReportSource = obj._moveAuditTrialSerCost;

            if (BaseCls.GlbReportName == "Movement_audit_trial_ser.rpt")
                crystalReportViewer1.ReportSource = obj._moveAuditTrialSer;

            if (BaseCls.GlbReportName == "Movement_audit_trial_cost.rpt")
                crystalReportViewer1.ReportSource = obj._moveAuditTrialCost;

            this.Text = "Movement Audit Trial";
            crystalReportViewer1.RefreshReport();
            obj = null;
            GC.Collect();
        }

        private void MovementAuditTrial()
        { // Nadeeka 28-02-2013
            obj.MovementAuditTrial();
            if (BaseCls.GlbReportType == "DTL")
            {
                if (BaseCls.GlbReportGroupLastGroupCat == "CAT1")
                {
                    crystalReportViewer1.ReportSource = obj._InvMovementAuditTrailGrpByCate;
                }
                else
                {
                    crystalReportViewer1.ReportSource = obj._invAudit;
                }
                //crystalReportViewer1.ReportSource = obj._invAudit;
            }
            else
            {
                crystalReportViewer1.ReportSource = obj._invAuditSum;
            }
            this.Text = "Inventory Audit Trial";
            crystalReportViewer1.RefreshReport();
            obj = null;
            //((IDisposable)(obj)).Dispose(); 
            GC.Collect();
        }

        private void MovementAuditTrial_ARL()
        {
            obj.MovementAuditTrial_ARL();
            if (BaseCls.GlbReportType == "DTL")
            {
                crystalReportViewer1.ReportSource = obj._invAudit_ARL;
            }
            else
            {
                crystalReportViewer1.ReportSource = obj._invAuditSum;
            }
            this.Text = "Inventory Audit Trial";
            crystalReportViewer1.RefreshReport();
            obj = null;
            //((IDisposable)(obj)).Dispose(); 
            GC.Collect();
        }
        //hasith 28/01/2015
        private void MovementSummary()
        {
            obj.MovementSummary();
            crystalReportViewer1.ReportSource = obj._invMovsum;
            this.Text = "Movement Summary";
            crystalReportViewer1.RefreshReport();
            obj = null;

            GC.Collect();
        }
        private void WarrantyClaimCreditNote()
        {
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;


            DataTable VIW_INV_MOVEMENT_SERIAL = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS1 = new DataTable();
            DataTable MST_LOC_1 = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            DataTable mst_movcatetp = new DataTable();

            int _numPages = 0;
            DataRow dr;
            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr = PRINT_DOC.NewRow();
            dr["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr);

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));


            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));

            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            if (VIW_INV_MOVEMENT_SERIAL.Rows.Count <= 0)
            {
                VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Inventory.GetWarrantyClaimAdj(docNo);
            }



            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();


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
                    sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                    mst_com = CHNLSVC.General.GetCompanyByCode(_com);
                    mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeMainTable(row["ITH_SUB_TP"].ToString(), "ADJ");
                }


                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
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
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            MST_ITM_STUS = MST_ITM_STUS.DefaultView.ToTable(true);

            inwardReport1.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            inwardReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            inwardReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            inwardReport1.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            inwardReport1.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            inwardReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            inwardReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            inwardReport1.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);




            foreach (object repOp in inwardReport1.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = inwardReport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }

                }
            }
            this.Text = "Warranty Claim Inventory Document";


            crystalReportViewer1.ReportSource = inwardReport1;

            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();



        }

        private void WarrantyClaimCreditNote(int _no)
        {
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;

            //    Outward_Docs report1 = new Outward_Docs();

            DataTable VIW_INV_MOVEMENT_SERIAL = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable MST_LOC_1 = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS1 = new DataTable();
            DataTable mst_movcatetp = new DataTable();
            DataRow dr;
            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);
            if (VIW_INV_MOVEMENT_SERIAL.Rows.Count <= 0)
            {
                VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Inventory.GetWarrantyClaimAdj(docNo);
            }

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();


            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;
            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            outwardReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);



            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));


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
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                    }
                    mst_com = CHNLSVC.General.GetCompanyByCode(_com);

                    mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeMainTable(row["ITH_SUB_TP"].ToString(), "ADJ");
                }




                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());


                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
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
            outwardReport1.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            outwardReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            outwardReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            outwardReport1.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            outwardReport1.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            outwardReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            outwardReport1.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            foreach (object repOp in outwardReport1.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = outwardReport1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                    }

                }
            }
            this.Text = "Warranty Claim Inventory Document";
            crystalReportViewer1.ReportSource = outwardReport1;
            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();



        }
        private void POReportUpdate()
        { //Isuru 2017/05/16
            obj.PurcaseOrderPrintUpdate();//PurchaseOrderPrintUpdate
            crystalReportViewer1.ReportSource = obj._poupdaterpt;
            this.Text = "Purchase Order";
            crystalReportViewer1.RefreshReport();
        }

        //Tharindu 2017-11-17
        private void Aod_Ack_doc_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //Inward_Docs report1 = new Inward_Docs();

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

            int _numPages = 0;
            DataRow dr;

            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr = PRINT_DOC.NewRow();
            dr["NOOFPAGES"] = _numPages;
            dr["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr);

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            MST_ITM.Columns.Add("MI_ITM_UOM", typeof(string));


            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));

            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            PO_DTL.Clear();
            PO_DTL = CHNLSVC.Sales.GetPODetails(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();

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
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                    }

                    mst_com = CHNLSVC.General.GetCompanyByCode(_com);
                    mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeTable(row["ITH_SUB_TP"].ToString());
                }

                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
                Int32 totqty = 0;
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
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            objAcknowledge.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            objAcknowledge.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            objAcknowledge.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            objAcknowledge.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            objAcknowledge.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            objAcknowledge.Database.Tables["mst_com"].SetDataSource(mst_com);
            objAcknowledge.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            objAcknowledge.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            objAcknowledge.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);

            foreach (object repOp in objAcknowledge.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = objAcknowledge.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }

                }
            }
            this.Text = "Aod Acknowledge Print";

            crystalReportViewer1.ReportSource = objAcknowledge;
            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();

        }

        //Wimal - 27/03/2018
        private void StockAdjDetails()
        {

        }

        #endregion

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            this.crystalReportViewer1.Dispose();
            this.crystalReportViewer1 = null;

            GC.Collect();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (GlbReportName == "Outward_Docs.rpt")
                {
                    outwardReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("DO") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    outwardReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    outwardReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    //outwardReport1.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    outwardReport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "Outward_Docs_Full.rpt")
                {
                    outwardReport2.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    outwardReport2.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    outwardReport2.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    //outwardReport2.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    outwardReport2.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (GlbReportName == "Outward_Docs_ABE1.rpt")
                {
                    outwardReport_abe.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    //int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    //outwarddelconfReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //outwarddelconfReport.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    //outwarddelconfReport.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    outwardReport_abe.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (GlbReportName == "Outward_Docs_AST.rpt")
                {
                    outwardReport_ast.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    //int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    //outwarddelconfReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //outwarddelconfReport.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    //outwarddelconfReport.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    outwardReport_ast.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (GlbReportName == "Outward_Docs_Del_Conf.rpt")
                {
                    outwarddelconfReport.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    //int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    //outwarddelconfReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //outwarddelconfReport.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    //outwarddelconfReport.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    outwarddelconfReport.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (GlbReportName == "SOutward_Docs.rpt")
                {

                    SoutwardReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("INV") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    SoutwardReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    SoutwardReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    SoutwardReport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (GlbReportName == "Dealer_Outward_Docs.rpt")
                {
                    int _numPages = 0;
                    _numPages = CHNLSVC.General.GetPrintDocCopies(BaseCls.GlbUserComCode, GlbReportName);
                    if (_numPages == 0)
                    {
                        _numPages = 1;
                    }
                    DoutwardReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize);
                    DoutwardReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //DoutwardReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    DoutwardReport1.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                    DoutwardReport1.PrintToPrinter(_numPages, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (GlbReportName == "Inward_Docs.rpt")//Inward_Docs.rpt
                {
                    inwardReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("DO") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    inwardReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    inwardReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    inwardReport1.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    inwardReport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (GlbReportName == "Inward_Docs_Full.rpt")
                {
                    inwardReport2.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    inwardReport2.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    inwardReport2.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    inwardReport2.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    inwardReport2.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "Inward_Docs_ack.rpt")
                {
                    inwardReport_ack.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("DO") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    inwardReport_ack.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    inwardReport_ack.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    inwardReport_ack.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    inwardReport_ack.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "Inward_Docs_AST.rpt")
                {
                    inwardReport_ast.PrintOptions.PrinterName = GetDefaultPrinter();
                    inwardReport_ast.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (GlbReportName == "Inward_Docs_ABE2.rpt")
                {
                    inwardReport_abe2.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    //int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("DO") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    //inwardReport_abe2.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //inwardReport_abe2.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    //inwardReport_abe2.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    inwardReport_abe2.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "Inward_Docs_ABE.rpt")
                {
                    inwardReport_abe.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("DO") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    inwardReport_abe.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    inwardReport_abe.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    inwardReport_abe.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    inwardReport_abe.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (GlbReportName == "SInward_Docs.rpt")
                {
                    SinwardReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("INV") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    SinwardReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    SinwardReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    SinwardReport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "Dealer_Inward_Docs.rpt")
                {
                    int _numPages = 0;
                    _numPages = CHNLSVC.General.GetPrintDocCopies(BaseCls.GlbUserComCode, GlbReportName);
                    if (_numPages == 0)
                    {
                        _numPages = 1;
                    }
                    DinwardReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize);  // returns 257 int
                    DinwardReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //DinwardReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    DinwardReport1.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                    DinwardReport1.PrintToPrinter(_numPages, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (BaseCls.GlbReportName == "WarrantyClaim")
                {

                    inwardReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    inwardReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    inwardReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    inwardReport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (GlbReportName == "DINPrints.rpt")
                {
                    _dinReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("DO") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    _dinReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _dinReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _dinReport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (GlbReportName == "Dealer_DINPrints.rpt")
                {
                    int _numPages = 0;
                    _numPages = CHNLSVC.General.GetPrintDocCopies(BaseCls.GlbUserComCode, GlbReportName);
                    if (_numPages == 0)
                    {
                        _numPages = 1;
                    }
                    _ddinReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize);// returns 257 int
                    _ddinReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //_ddinReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _ddinReport1.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                    _ddinReport1.PrintToPrinter(_numPages, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "SDINPrints.rpt")
                {
                    S_dinReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("INV") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    S_dinReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    S_dinReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    S_dinReport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "GRANPrints.rpt")
                {
                    _granReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("DO") : getprtnbr(BaseCls.GlbReportPaperSize);// returns 257 int
                    _granReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _granReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _granReport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "GRANPrints_full.rpt")
                {
                    _granReportfull.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize);// returns 257 int
                    _granReportfull.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _granReportfull.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _granReportfull.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "SGRANPrints.rpt")
                {
                    S_granReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("INV") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    S_granReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    S_granReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    S_granReport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };


                if (GlbReportName == "SMRNRepPrints.rpt")
                {
                    S_mrnReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    S_mrnReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    S_mrnReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    S_mrnReport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "Dealer_MRNRepPrints.rpt")
                {
                    int _numPages = 0;
                    _numPages = CHNLSVC.General.GetPrintDocCopies(BaseCls.GlbUserComCode, GlbReportName);
                    if (_numPages == 0)
                    {
                        _numPages = 1;
                    }
                    D_mrnReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    D_mrnReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //D_mrnReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    D_mrnReport1.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                    D_mrnReport1.PrintToPrinter(_numPages, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "InterTransfer.rpt")
                {
                    _interTrans.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    _interTrans.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _interTrans.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _interTrans.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "SInterTransfer.rpt")
                {
                    S_interTrans.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("INV"); // returns 257 int
                    S_interTrans.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    S_interTrans.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    S_interTrans.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "RCCPrints.rpt")
                {
                    _rccReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    _rccReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _rccReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _rccReport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "SRCCPrints.rpt")
                {
                    S_rccReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("INV"); // returns 257 int
                    S_rccReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    S_rccReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    S_rccReport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "FixedAssetTransferNotes.rpt")
                {
                    _fixTrnsReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    _fixTrnsReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //_fixTrnsReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _fixTrnsReport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (GlbReportName == "SFixedAssetTransferNotes.rpt")
                {
                    S_fixTrnsReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("INV"); // returns 257 int
                    S_fixTrnsReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //_fixTrnsReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    S_fixTrnsReport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "FixedAssetConfirmationNotes.rpt")
                {
                    _fixConReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    _fixConReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //_fixConReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _fixConReport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (BaseCls.GlbReportName == "RevertSRN.rpt")
                {
                    obj._recRvtrpt.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("DO") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    obj._recRvtrpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._recRvtrpt.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj._recRvtrpt.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (BaseCls.GlbReportName == "SRevertSRN.rpt")
                {
                    obj.S_recRvtrpt.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("INV") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    obj.S_recRvtrpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj.S_recRvtrpt.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj.S_recRvtrpt.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (BaseCls.GlbReportName == "Exchange_Docs.rpt")
                {
                    //obj._ExchangeRep.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    //int papernbr = getprtnbr("DO"); // returns 257 int
                    //obj._ExchangeRep.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //obj._ExchangeRep.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    //obj._ExchangeRep.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                    //17-06-2015 Modified by nadeeka
                    obj._ExchangeRep.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("DO") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    obj._ExchangeRep.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._ExchangeRep.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj._ExchangeRep.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    obj._ExchangeRep.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (BaseCls.GlbReportName == "Exchange_Docs_Full.rpt")
                {
                    //obj._ExchangeRep.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    //int papernbr = getprtnbr("DO"); // returns 257 int
                    //obj._ExchangeRep.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //obj._ExchangeRep.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    //obj._ExchangeRep.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                    //17-06-2015 Modified by nadeeka
                    obj._ExchangeRepfull.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    obj._ExchangeRepfull.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._ExchangeRepfull.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj._ExchangeRepfull.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    obj._ExchangeRepfull.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (BaseCls.GlbReportName == "SExchange_Docs.rpt")
                {
                    obj.S_ExchangeRep.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("INV"); // returns 257 int
                    obj.S_ExchangeRep.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj.S_ExchangeRep.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj.S_ExchangeRep.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (BaseCls.GlbReportName == "InventoryStatements.rpt")
                {

                    if (PrintDialog1.ShowDialog() == DialogResult.OK)
                        obj._invSts.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName;
                    obj._invSts.PrintToPrinter(1, false, PrintDialog1.PrinterSettings.FromPage, PrintDialog1.PrinterSettings.ToPage);
                    btnPrint.Enabled = false;
                };
                if (BaseCls.GlbReportName == "PurchaseOrderPrint.rpt")
                {
                    obj._porpt.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._porpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._porpt.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj._porpt.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (BaseCls.GlbReportName == "Stock_Balance.rpt" || BaseCls.GlbReportName == "Stock_Balance_WO_Stus.rpt")
                {
                    printDialogSettings();
                    if (PrintDialog1.ShowDialog() == DialogResult.OK)
                        obj._invBal.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName;
                    obj._invBal.PrintToPrinter(1, false, PrintDialog1.PrinterSettings.FromPage, PrintDialog1.PrinterSettings.ToPage);
                };

                if (BaseCls.GlbReportName == "FIFO_Not_Followed_Report.rpt")
                {
                    printDialogSettings();
                    if (PrintDialog1.ShowDialog() == DialogResult.OK)
                        obj._FIFO.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName;
                    obj._FIFO.PrintToPrinter(1, false, PrintDialog1.PrinterSettings.FromPage, PrintDialog1.PrinterSettings.ToPage);
                };

                if (BaseCls.GlbReportName == "GRAN_Details_Report.rpt")
                {
                    printDialogSettings();
                    if (PrintDialog1.ShowDialog() == DialogResult.OK)
                        obj._GRAN.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName;
                    obj._GRAN.PrintToPrinter(1, false, PrintDialog1.PrinterSettings.FromPage, PrintDialog1.PrinterSettings.ToPage);

                };
                if (BaseCls.GlbReportName == "InventoryMovementAuditTrials.rpt")
                {
                    if (PrintDialog1.ShowDialog() == DialogResult.OK)
                        obj._invAudit.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName;
                    obj._invAudit.PrintToPrinter(1, false, PrintDialog1.PrinterSettings.FromPage, PrintDialog1.PrinterSettings.ToPage);
                    btnPrint.Enabled = false;

                };
                if (BaseCls.GlbReportName == "InventoryMovementAuditTrialWithSerials.rpt")
                {
                    if (PrintDialog1.ShowDialog() == DialogResult.OK)
                        obj._invAuditSrl.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName;
                    obj._invAuditSrl.PrintToPrinter(1, false, PrintDialog1.PrinterSettings.FromPage, PrintDialog1.PrinterSettings.ToPage);
                    btnPrint.Enabled = false;

                };
                if (BaseCls.GlbReportName == "DamageGoodsApproval.rpt")
                {


                    if (PrintDialog1.ShowDialog() == DialogResult.OK)
                        obj._dmgApp.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName;

                    obj._dmgApp.PrintToPrinter(1, false, PrintDialog1.PrinterSettings.FromPage, PrintDialog1.PrinterSettings.ToPage);

                };
                if (BaseCls.GlbReportName == "WarrantyClaimNote.rpt")
                {

                    obj._warrClaim.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("DO") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    obj._warrClaim.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._warrClaim.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj._warrClaim.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (BaseCls.GlbReportName == "Reorder_Items.rpt")
                {
                    printDialogSettings();
                    if (PrintDialog1.ShowDialog() == DialogResult.OK)
                        obj._reOrder.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName;
                    obj._reOrder.PrintToPrinter(1, false, PrintDialog1.PrinterSettings.FromPage, PrintDialog1.PrinterSettings.ToPage);
                };
                if (BaseCls.GlbReportName == "PurchaseOrderPrintUpdate.rpt")
                {
                    obj._poupdaterpt.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._poupdaterpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._poupdaterpt.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj._poupdaterpt.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
            
                if (GlbReportName == "Outward_Docs_AODOUT.rpt")
                {
                    outwardReport_aodout.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("DO") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    outwardReport_aodout.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    outwardReport_aodout.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    //outwardReport1.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    outwardReport_aodout.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "Outward_Docs_AODOUTNEW.rpt")
                {
                    outwardReport_aodout.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("DO") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    outwardReport_aodout.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    outwardReport_aodout.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    //outwardReport1.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    outwardReport_aodout.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                //Tharanga 2017/06/01
                if (GlbReportName == "GRNreport.rpt")//Inward_Docs.rpt
                {
                    GrnReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    GrnReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    GrnReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    GrnReport1.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    GrnReport1.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "Inward_Docauto.rpt")//Inward_Docs.rpt
                {
                    _Inward_Doc_auto.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    _Inward_Doc_auto.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _Inward_Doc_auto.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _Inward_Doc_auto.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    _Inward_Doc_auto.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "DO_print_ABE.rpt")// Tharanga 2017/07/10
                {
                    _DO_print_ABE.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    _DO_print_ABE.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _DO_print_ABE.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _DO_print_ABE.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    _DO_print_ABE.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (BaseCls.GlbReportName == "Inward_Docs_ABE_New.rpt") //Tharanga 2017/07/11
                {
                    _Inward_Docs_ABE_New.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    _Inward_Docs_ABE_New.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _Inward_Docs_ABE_New.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _Inward_Docs_ABE_New.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    _Inward_Docs_ABE_New.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "Outward_Doc_ABE_New.rpt") //Tharanga 2017/07/14
                {
                    _Outward_Docs_ABE_NEW.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize);  // returns 257 int
                    _Outward_Docs_ABE_NEW.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //DinwardReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _Outward_Docs_ABE_NEW.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                    _Outward_Docs_ABE_NEW.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "GRNreport_ABE.rpt") //Tharanga 2017/07/17
                {
                    _GRNreport_ABE.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("INV") : getprtnbr(BaseCls.GlbReportPaperSize);  // returns 257 int
                    _GRNreport_ABE.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //DinwardReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _GRNreport_ABE.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                    _GRNreport_ABE.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "PurchaseOrderPrint_ABE.rpt") //Tharanga 2017/07/17
                {
                    obj._PurchaseOrderPrint_ABE.PrintOptions.PrinterName = GetDefaultPrinter();
                    //int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("INV") : getprtnbr(BaseCls.GlbReportPaperSize);  // returns 257 int
                    obj._PurchaseOrderPrint_ABE.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //DinwardReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj._PurchaseOrderPrint_ABE.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                    obj._PurchaseOrderPrint_ABE.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "SRN_Doc_ABE.rpt")
                {
                    _SRN_Doc_ABE.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("INV") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    _SRN_Doc_ABE.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _SRN_Doc_ABE.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _SRN_Doc_ABE.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    _SRN_Doc_ABE.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "Inward_Docs_abstract.rpt") //add by tharanga 2017/09/20
                {
                    _Inward_Docs_abstract.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    _Inward_Docs_abstract.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _Inward_Docs_abstract.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _Inward_Docs_abstract.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    _Inward_Docs_abstract.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "Outward_Docs_Full_abstract.rpt") //add by tharanga 2017/09/20
                {
                    _Outward_Docs_Full_abstract.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    _Outward_Docs_Full_abstract.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    _Outward_Docs_Full_abstract.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    _Outward_Docs_Full_abstract.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    _Outward_Docs_Full_abstract.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };
                if (GlbReportName == "summarized_age_report.rpt") //add by tharanga 2017/09/20
                {
                    obj._summarized_age_report.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    obj._summarized_age_report.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._summarized_age_report.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj._summarized_age_report.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    obj._summarized_age_report.PrintToPrinter(1, false, 0, 0);
                };
                if (GlbReportName == "Status_wise_ageing_report.rpt") //add by tharanga 2017/09/20
                {
                    obj._Status_wise_ageing_report.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = BaseCls.GlbReportPaperSize == "" ? getprtnbr("Letter") : getprtnbr(BaseCls.GlbReportPaperSize); // returns 257 int
                    obj._Status_wise_ageing_report.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._Status_wise_ageing_report.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj._Status_wise_ageing_report.PrintOptions.PaperOrientation = PaperOrientation.Portrait; //2014-03-29
                    obj._Status_wise_ageing_report.PrintToPrinter(1, false, 0, 0);
                };
                

                
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
                GC.Collect();
            }
        }

        public void printDialogSettings()
        {
            PrintDialog1.AllowPrintToFile = true;
            PrintDialog1.AllowSelection = true;
            PrintDialog1.AllowCurrentPage = true;
            PrintDialog1.AllowSomePages = true;
            PrintDialog1.PrintToFile = false;

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

       public string GetDefaultPrinter()
        {
            string _printerName = string.Empty;
            PrinterSettings settings = new PrinterSettings();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)
                {
                    _printerName = printer;
                    break;
                }
            }
            return _printerName;
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
        #region Common Functions

        public DataTable ConvertToDataTable<T>(IList<T> data)
        { // Nadeeka 04-01-2013 Convert List to data table
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            PrinterSettings printerSettings = new PrinterSettings();
            PrintDialog printDialog = new PrintDialog();
            printDialog.PrinterSettings = printerSettings;
            printDialog.AllowPrintToFile = false;
            printDialog.AllowSomePages = true;
            printDialog.UseEXDialog = true;
            DialogResult result = printDialog.ShowDialog();

            if (result == DialogResult.Cancel)
            {
                return;
            }


            //outwardReport1.PrintOptions.PrinterName = GetDefaultPrinter();
            //int traynbr = gettraynbr("Manual"); // returns 4 int
            //int papernbr = getprtnbr("DO"); // returns 257 int
            //outwardReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
            //outwardReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
            //outwardReport1.PrintToPrinter(1, false, 0, 0);
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pname = this.listBox1.SelectedItem.ToString();
            myPrinters.SetDefaultPrinter(pname);
            lblPrinter.Text = "Default Printer- " + GetDefaultPrinter();
        }
          
        //Isuru 2017/05/17
        private void Out_doc_printall()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //    Outward_Docs report1 = new Outward_Docs();

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
            DataTable mesuredet = new DataTable();
            DataTable mesuredet1 = new DataTable();
            string inv_no = "";

            DataRow dr;
            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();

            DataTable param1 = new DataTable();
            param1.Columns.Add("saleType", typeof(string));
            DataTable tblComDate = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            DataTable receiptDetails = new DataTable();
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
            outwardReport_aodout.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
        



            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            MST_ITM.Columns.Add("MI_ITM_UOM", typeof(string));

            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));

            DataRow dr5;
            decimal totqty = 0;
            mesuredet.Columns.Add("mesdet", typeof(string));


            foreach (DataRow row in VIW_INV_MOVEMENT_SERIAL.Rows)
            {
                string _com = row["ITH_COM"].ToString();
                string _loc = row["ITH_LOC"].ToString();
                string _itm = row["ITS_ITM_CD"].ToString();
                string _othLoc = row["ITH_OTH_LOC"].ToString();
                int index = VIW_INV_MOVEMENT_SERIAL.Rows.IndexOf(row);
                totqty = totqty +Convert.ToDecimal( row["QTY"].ToString()); 
                //mesuredet1 = CHNLSVC.Sales.getmeasuredetails(_itm);
                //if (mesuredet1 != null)
                //{
                //    foreach (DataRow data1 in mesuredet1.Rows)
                //    {
                //        if (data1["mesdet"].ToString() == "")
                //        {
                //            data1["mesdet"] = "N/A";
                //        }

                //        string mesdet = data1["mesdet"].ToString();

                //        dr5 = mesuredet.NewRow();
                //        dr5["mesdet"] = mesdet;
                //        mesuredet.Rows.Add(dr5);
                //    }
                //}
                
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
                            //DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(row["ITH_OTH_DOCNO"].ToString());
                            DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(docNo);
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


                sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(inv_no);

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
            DataTable param = new DataTable();
            param.Columns.Add("isCnote", typeof(Int16));
            param.Columns.Add("totqty", typeof(Int32));
            param.Columns.Add("user", typeof(string));
            if (isCredit == 1)
            {
                drr = param.NewRow();
                drr["isCnote"] = 1;
                drr["totqty"] = totqty;
                drr["user"] = BaseCls.GlbUserID;
                param.Rows.Add(drr);
            }
            else
            {
                drr = param.NewRow();
                drr["isCnote"] = 0;
                drr["totqty"] = totqty;
                drr["user"] = BaseCls.GlbUserID;
                param.Rows.Add(drr);
            }
            MST_ITM_STUS = MST_ITM_STUS.DefaultView.ToTable(true);
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            param1 = param1.DefaultView.ToTable(true);
            outwardReport_aodout.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            outwardReport_aodout.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            outwardReport_aodout.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            outwardReport_aodout.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            outwardReport_aodout.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            outwardReport_aodout.Database.Tables["mst_com"].SetDataSource(mst_com);
            outwardReport_aodout.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            outwardReport_aodout.Database.Tables["param1"].SetDataSource(param1);
            outwardReport_aodout.Database.Tables["param"].SetDataSource(param);
            //outwardReport_aodout.Database.Tables["mesuredet"].SetDataSource(mesuredet);
            foreach (object repOp in outwardReport1.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = outwardReport_aodout.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }
                    if (_cs.SubreportName == "warr")
                    {
                        ReportDocument subRepDoc = outwardReport_aodout.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);

                    }
                    if (sat_hdr.Rows.Count > 0)
                    {
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = outwardReport_aodout.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                        }
                        if (tblComDate.Rows.Count > 0)
                        {
                            if (_cs.SubreportName == "WarrComDate")
                            {
                                ReportDocument subRepDoc = outwardReport_aodout.Subreports[_cs.SubreportName];
                                subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            }
                        }
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = outwardReport_aodout.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                        }

                    }

                }

            }
            this.Text = "Outward Print";
            crystalReportViewer1.ReportSource = outwardReport_aodout;
            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();



        }

        //Tharanga 2017/06/01
        private void GRN_print()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //Inward_Docs report1 = new Inward_Docs();

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
            // Tharanga
            DataTable param = new DataTable();
            param.Columns.Add("totqty", typeof(Int32));
            //End
            int _numPages = 0;
            DataRow dr;

            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr = PRINT_DOC.NewRow();
            dr["NOOFPAGES"] = _numPages;
            dr["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr);

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            MST_ITM.Columns.Add("MI_ITM_UOM", typeof(string));


            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));



            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            PO_DTL.Clear();
            PO_DTL = CHNLSVC.Sales.GetPODetails(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();


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
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                    }

                    mst_com = CHNLSVC.General.GetCompanyByCode(_com);
                    mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeTable(row["ITH_SUB_TP"].ToString());
                }






                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
                Int32 totqty = 0;
                foreach (DataRow row1 in MST_ITM1.Rows)
                {

                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    dr["MI_ITM_UOM"] = row1["MI_ITM_UOM"].ToString();
                    totqty++;
                    MST_ITM.Rows.Add(dr);
                }
                foreach (DataRow row2 in MST_ITM_STUS1.Rows)
                {
                    dr = MST_ITM_STUS.NewRow();
                    dr["MIS_CD"] = row2["MIS_CD"].ToString();
                    dr["MIS_DESC"] = row2["MIS_DESC"].ToString();

                    MST_ITM_STUS.Rows.Add(dr);
                }
                param.Rows.Add(totqty);
            }

            MST_ITM_STUS = MST_ITM_STUS.DefaultView.ToTable(true);
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            GrnReport1.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            GrnReport1.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            GrnReport1.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            GrnReport1.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            GrnReport1.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            GrnReport1.Database.Tables["mst_com"].SetDataSource(mst_com);
            GrnReport1.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            GrnReport1.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            GrnReport1.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);

            GrnReport1.Database.Tables["param"].SetDataSource(param);


            foreach (object repOp in GrnReport1.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = GrnReport1.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }

                }
            }
            this.Text = "Inward Print";

            crystalReportViewer1.ReportSource = GrnReport1;

            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();

        }

        //Tharanga 2017/06/28
        private void In_doc_Auto()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //Inward_Docs report1 = new Inward_Docs();

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

            int _numPages = 0;
            DataRow dr;

            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr = PRINT_DOC.NewRow();
            dr["NOOFPAGES"] = _numPages;
            dr["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr);

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            MST_ITM.Columns.Add("MI_ITM_UOM", typeof(string));


            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));



            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            PO_DTL.Clear();
            PO_DTL = CHNLSVC.Sales.GetPODetails(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();


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
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                    }

                    mst_com = CHNLSVC.General.GetCompanyByCode(_com);
                    mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeTable(row["ITH_SUB_TP"].ToString());
                }






                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
                Int32 totqty = 0;
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
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            _Inward_Doc_auto.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            _Inward_Doc_auto.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _Inward_Doc_auto.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            _Inward_Doc_auto.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            _Inward_Doc_auto.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            _Inward_Doc_auto.Database.Tables["mst_com"].SetDataSource(mst_com);
            _Inward_Doc_auto.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            _Inward_Doc_auto.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
           // _Inward_Doc_auto.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);




            foreach (object repOp in _Inward_Doc_auto.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = _Inward_Doc_auto.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }

                }
            }
            this.Text = "Inward Print";

            crystalReportViewer1.ReportSource = _Inward_Doc_auto;

            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();

        }
      
        public void Do_print_ABE()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //    Outward_Docs report1 = new Outward_Docs();

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
            string inv_no = "";

            DataRow dr;
            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();

            DataTable param1 = new DataTable();
            param1.Columns.Add("saleType", typeof(string));
            DataTable tblComDate = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            DataTable receiptDetails = new DataTable();
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
            _DO_print_ABE.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);



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
                            //DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(row["ITH_OTH_DOCNO"].ToString());
                            DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(docNo);
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


                sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(inv_no);

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
            DataTable param = new DataTable();
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
            _DO_print_ABE.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            _DO_print_ABE.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _DO_print_ABE.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            _DO_print_ABE.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            _DO_print_ABE.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            _DO_print_ABE.Database.Tables["mst_com"].SetDataSource(mst_com);
            _DO_print_ABE.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            _DO_print_ABE.Database.Tables["param1"].SetDataSource(param1);
            _DO_print_ABE.Database.Tables["param"].SetDataSource(param);
            foreach (object repOp in _DO_print_ABE.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = _DO_print_ABE.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }
                    if (_cs.SubreportName == "rptCust1")
                    {
                        ReportDocument subRepDoc = _DO_print_ABE.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }
                    if (_cs.SubreportName == "warr")
                    {
                        ReportDocument subRepDoc = _DO_print_ABE.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);

                    }
                    if (sat_hdr.Rows.Count > 0)
                    {
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = _DO_print_ABE.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                        }
                        if (tblComDate.Rows.Count > 0)
                        {
                            if (_cs.SubreportName == "WarrComDate")
                            {
                                ReportDocument subRepDoc = _DO_print_ABE.Subreports[_cs.SubreportName];
                                subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            }
                        }
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = _DO_print_ABE.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                        }

                    }

                }

            }
            this.Text = "DO Print";
            crystalReportViewer1.ReportSource = _DO_print_ABE;
            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();



        }//Tharanga 2017/07/12
        private void In_doc_ABE()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //Inward_Docs report1 = new Inward_Docs();

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

            int _numPages = 0;
            DataRow dr;

            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));
            PRINT_DOC.Columns.Add("PRINT_USER", typeof(string));
            PRINT_DOC.Columns.Add("SR_MGR", typeof(string));
            PRINT_DOC.Columns.Add("NIC", typeof(string));
            PRINT_DOC.Columns.Add("EPF", typeof(string));
            PRINT_DOC.Columns.Add("REC_SR_MGR", typeof(string));
            PRINT_DOC.Columns.Add("REC_NIC", typeof(string));
            PRINT_DOC.Columns.Add("REC_EPF", typeof(string));

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));


            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));

            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            PO_DTL.Clear();
            PO_DTL = CHNLSVC.Sales.GetPODetails(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();

            string _loc = "";
            string _othLoc = "";
            foreach (DataRow row in VIW_INV_MOVEMENT_SERIAL.Rows)
            {
                string _com = row["ITH_COM"].ToString();
                _loc = row["ITH_LOC"].ToString();
                string _itm = row["ITS_ITM_CD"].ToString();
                _othLoc = row["ITH_OTH_LOC"].ToString();
                int index = VIW_INV_MOVEMENT_SERIAL.Rows.IndexOf(row);
                if (index == 0)
                {
                    MST_LOC = CHNLSVC.Sales.GetLocationCode(_com, _loc);
                    MST_LOC_1 = CHNLSVC.Sales.GetLocationCode(_com, _othLoc);

                    if (!string.IsNullOrEmpty(row["ITH_OTH_DOCNO"].ToString()))
                    {
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                    }

                    mst_com = CHNLSVC.General.GetCompanyByCode(_com);
                    mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeTable(row["ITH_SUB_TP"].ToString());
                }

                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
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

            DataTable _locmgr = CHNLSVC.Inventory.getLocManagerDetail(_loc);
            DataTable _locmgrrec = CHNLSVC.Inventory.getLocManagerDetail(_othLoc);
            dr = PRINT_DOC.NewRow();
            dr["NOOFPAGES"] = _numPages;
            dr["SHOWCOM"] = BaseCls.ShowComName;
            dr["PRINT_USER"] = BaseCls.GlbUserID;
            dr["SR_MGR"] = "";
            dr["NIC"] = "";
            dr["EPF"] = "";
            dr["REC_SR_MGR"] = "";
            dr["REC_NIC"] = "";
            dr["REC_EPF"] = "";
            foreach (DataRow row1 in _locmgr.Rows)
            {
                dr["SR_MGR"] = row1["emp_name"].ToString();
                dr["NIC"] = row1["esep_nic"].ToString();
                dr["EPF"] = row1["esep_epf"].ToString();
            }
            foreach (DataRow row1 in _locmgrrec.Rows)
            {
                dr["REC_SR_MGR"] = row1["emp_name"].ToString();
                dr["REC_NIC"] = row1["esep_nic"].ToString();
                dr["REC_EPF"] = row1["esep_epf"].ToString();
            }
            PRINT_DOC.Rows.Add(dr);

            MST_ITM_STUS = MST_ITM_STUS.DefaultView.ToTable(true);
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            _Inward_Docs_ABE_New.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            _Inward_Docs_ABE_New.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _Inward_Docs_ABE_New.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            _Inward_Docs_ABE_New.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            _Inward_Docs_ABE_New.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            _Inward_Docs_ABE_New.Database.Tables["mst_com"].SetDataSource(mst_com);
            _Inward_Docs_ABE_New.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            _Inward_Docs_ABE_New.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            _Inward_Docs_ABE_New.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);


            foreach (object repOp in _Inward_Docs_ABE_New.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = _Inward_Docs_ABE_New.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }

                }
            }
            this.Text = "Inward Print";


            crystalReportViewer1.ReportSource = _Inward_Docs_ABE_New;

            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();

        }

        private void Out_doc_ABE() //Tharanga 2017/07/14
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //    Outward_Docs report1 = new Outward_Docs();

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
            string inv_no = "";

            DataRow dr;
            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();

            DataTable param1 = new DataTable();
            param1.Columns.Add("saleType", typeof(string));
            DataTable tblComDate = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            DataTable receiptDetails = new DataTable();
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
            _Outward_Docs_ABE_NEW.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);



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
                            //DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(row["ITH_OTH_DOCNO"].ToString());
                            DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(docNo);
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


                sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(inv_no);

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
            DataTable param = new DataTable();
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
            _Outward_Docs_ABE_NEW.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            _Outward_Docs_ABE_NEW.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _Outward_Docs_ABE_NEW.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            _Outward_Docs_ABE_NEW.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            _Outward_Docs_ABE_NEW.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            _Outward_Docs_ABE_NEW.Database.Tables["mst_com"].SetDataSource(mst_com);
            _Outward_Docs_ABE_NEW.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            _Outward_Docs_ABE_NEW.Database.Tables["param1"].SetDataSource(param1);
            _Outward_Docs_ABE_NEW.Database.Tables["param"].SetDataSource(param);
            foreach (object repOp in _Outward_Docs_ABE_NEW.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = _Outward_Docs_ABE_NEW.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }
                    if (_cs.SubreportName == "warr")
                    {
                        ReportDocument subRepDoc = _Outward_Docs_ABE_NEW.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);

                    }
                    if (sat_hdr.Rows.Count > 0)
                    {
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = _Outward_Docs_ABE_NEW.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                        }
                        if (tblComDate.Rows.Count > 0)
                        {
                            if (_cs.SubreportName == "WarrComDate")
                            {
                                ReportDocument subRepDoc = _Outward_Docs_ABE_NEW.Subreports[_cs.SubreportName];
                                subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            }
                        }
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = _Outward_Docs_ABE_NEW.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                        }

                    }

                }

            }
            this.Text = "Outward Print";
            crystalReportViewer1.ReportSource = _Outward_Docs_ABE_NEW;
            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();



        }

        private void GRN_print_ABE() //Tharanga 2017/07/17
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //Inward_Docs report1 = new Inward_Docs();

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
            DataTable PO_DTE1 = new DataTable();

            // Tharanga
            DataTable param = new DataTable();
            
            param.Columns.Add("totqty", typeof(Int32));
            param.Columns.Add("user", typeof(string));
            //End

            DataTable remqty = new DataTable();
            remqty.Columns.Add("totqty", typeof(Int32));

            int _numPages = 0;
            DataRow dr;
            Int32 totqty = 0;

            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr = PRINT_DOC.NewRow();
            dr["NOOFPAGES"] = _numPages;
            dr["SHOWCOM"] = BaseCls.ShowComName;
          
           

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            MST_ITM.Columns.Add("MI_ITM_UOM", typeof(string));


            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));



            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            //VIW_INV_MOVEMENT_SERIAL ADD COLUMN PO_BAL
            //BASE_REF_LINE

            PO_DTL.Clear();
            PO_DTL = CHNLSVC.Sales.GetPODetails(docNo);


            PO_DTE1 = CHNLSVC.Inventory.GetPODetails(PO_DTL.Rows[0]["poh_doc_no"].ToString());
	       
            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();


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
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                    }

                    mst_com = CHNLSVC.General.GetCompanyByCode(_com);
                    mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeTable(row["ITH_SUB_TP"].ToString());
                }






                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
                
                foreach (DataRow row1 in MST_ITM1.Rows)
                {

                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    dr["MI_ITM_UOM"] = row1["MI_ITM_UOM"].ToString();
                    totqty++;
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
           
            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserName;
            dr["totqty"] = totqty;
            param.Rows.Add(dr);

            MST_ITM_STUS = MST_ITM_STUS.DefaultView.ToTable(true);
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            _GRNreport_ABE.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            _GRNreport_ABE.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _GRNreport_ABE.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            _GRNreport_ABE.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            _GRNreport_ABE.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            _GRNreport_ABE.Database.Tables["mst_com"].SetDataSource(mst_com);
            _GRNreport_ABE.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            _GRNreport_ABE.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            _GRNreport_ABE.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);
            _GRNreport_ABE.Database.Tables["PO_DTE1"].SetDataSource(PO_DTE1);

            _GRNreport_ABE.Database.Tables["param"].SetDataSource(param);


            foreach (object repOp in _GRNreport_ABE.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = _GRNreport_ABE.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }

                }
            }
            this.Text = "GRN Print";

            crystalReportViewer1.ReportSource = _GRNreport_ABE;

            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();

        }

        private void POPrintABE()
        { // Sanjeewa 25-03-2013
            obj.PurcaseOrderPrint_ABE();
            crystalReportViewer1.ReportSource = obj._PurchaseOrderPrint_ABE;
            this.Text = "Purchase Order";
            crystalReportViewer1.RefreshReport();
        }

        private void SRN_doc_ABE()
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //Inward_Docs report1 = new Inward_Docs();

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

            int _numPages = 0;
            DataRow dr;

            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr = PRINT_DOC.NewRow();
            dr["NOOFPAGES"] = _numPages;
            dr["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr);

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            MST_ITM.Columns.Add("MI_ITM_UOM", typeof(string));


            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));



            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            PO_DTL.Clear();
            PO_DTL = CHNLSVC.Sales.GetPODetails(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();


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
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                    }

                    mst_com = CHNLSVC.General.GetCompanyByCode(_com);
                    mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeTable(row["ITH_SUB_TP"].ToString());
                }






                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
                Int32 totqty = 0;
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
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            _SRN_Doc_ABE.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            _SRN_Doc_ABE.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _SRN_Doc_ABE.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            _SRN_Doc_ABE.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            _SRN_Doc_ABE.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            _SRN_Doc_ABE.Database.Tables["mst_com"].SetDataSource(mst_com);
            _SRN_Doc_ABE.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            _SRN_Doc_ABE.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            // _SRN_Doc_ABE.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);




            foreach (object repOp in _SRN_Doc_ABE.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = _SRN_Doc_ABE.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }

                }
            }
            this.Text = "Inward Print";

            crystalReportViewer1.ReportSource = _SRN_Doc_ABE;

            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();

        }

        private void In_doc_abstract() //add by tharanga 2017/09/20
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //Inward_Docs report1 = new Inward_Docs();

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

            int _numPages = 0;
            DataRow dr;

            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr = PRINT_DOC.NewRow();
            dr["NOOFPAGES"] = _numPages;
            dr["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr);

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            MST_ITM.Columns.Add("MI_ITM_UOM", typeof(string));


            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));

            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            PO_DTL.Clear();
            PO_DTL = CHNLSVC.Sales.GetPODetails(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();


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
                        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                    }

                    mst_com = CHNLSVC.General.GetCompanyByCode(_com);
                    mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeTable(row["ITH_SUB_TP"].ToString());
                }

                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    dr["MI_ITM_UOM"] = row1["mi_itm_uom"].ToString();
                    
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
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            _Inward_Docs_abstract.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            _Inward_Docs_abstract.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _Inward_Docs_abstract.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            _Inward_Docs_abstract.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            _Inward_Docs_abstract.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            _Inward_Docs_abstract.Database.Tables["mst_com"].SetDataSource(mst_com);
            _Inward_Docs_abstract.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            _Inward_Docs_abstract.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            _Inward_Docs_abstract.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);


            foreach (object repOp in _Inward_Docs_abstract.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = _Inward_Docs_abstract.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }

                }
            }
            this.Text = "Inward Print";


            crystalReportViewer1.ReportSource = _Inward_Docs_abstract;

            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();

        }
        private void Out_doc_print_Full_abstract() //add by tharanga
        {
            string docNo = default(string);
            docNo = GlbReportDoc;

            //    Outward_Docs report1 = new Outward_Docs();

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
            string inv_no = "";

            DataRow dr;
            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();

            DataTable param1 = new DataTable();
            param1.Columns.Add("saleType", typeof(string));
            DataTable tblComDate = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            DataTable receiptDetails = new DataTable();
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
            _Outward_Docs_Full_abstract.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);



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


                sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(inv_no);

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
            DataTable param = new DataTable();
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
            _Outward_Docs_Full_abstract.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            _Outward_Docs_Full_abstract.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _Outward_Docs_Full_abstract.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            _Outward_Docs_Full_abstract.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            _Outward_Docs_Full_abstract.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            _Outward_Docs_Full_abstract.Database.Tables["mst_com"].SetDataSource(mst_com);
            _Outward_Docs_Full_abstract.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            _Outward_Docs_Full_abstract.Database.Tables["param1"].SetDataSource(param1);
            _Outward_Docs_Full_abstract.Database.Tables["param"].SetDataSource(param);
            foreach (object repOp in _Outward_Docs_Full_abstract.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCustomer")
                    {
                        ReportDocument subRepDoc = _Outward_Docs_Full_abstract.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                        subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                    }
                    if (_cs.SubreportName == "warr")
                    {
                        ReportDocument subRepDoc = _Outward_Docs_Full_abstract.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);

                    }
                    if (sat_hdr.Rows.Count > 0)
                    {
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = _Outward_Docs_Full_abstract.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                        }
                        if (tblComDate.Rows.Count > 0)
                        {
                            if (_cs.SubreportName == "WarrComDate")
                            {
                                ReportDocument subRepDoc = _Outward_Docs_Full_abstract.Subreports[_cs.SubreportName];
                                subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            }
                        }
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = _Outward_Docs_Full_abstract.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                        }

                    }

                }

            }
            this.Text = "Outward Print";
            crystalReportViewer1.ReportSource = _Outward_Docs_Full_abstract;
            crystalReportViewer1.RefreshReport();
            //report1.Close();
            //report1.Dispose();



        }

        private void ChargeSheet_print()
        {
            obj.Chargesheetreport();
            crystalReportViewer1.ReportSource = obj._chargesheet;

            this.Text = "EXCESS SHORT STOCK REPORT";
            crystalReportViewer1.RefreshReport();
        }

        private void AgeSummery()
        {
            obj.age_summery();
            crystalReportViewer1.ReportSource = obj._ageSummery;
            this.Text = "Age Summery Report";
            crystalReportViewer1.RefreshReport();
        }
    }
}
