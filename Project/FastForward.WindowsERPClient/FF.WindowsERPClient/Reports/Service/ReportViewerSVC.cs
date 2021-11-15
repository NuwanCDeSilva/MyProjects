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
namespace FF.WindowsERPClient.Reports.Service
{
    public partial class ReportViewerSVC : Base
    {        
        clsServiceRep objSvc = new clsServiceRep();
        //private RCCPrint_New _rccPrint = new RCCPrint_New();


        public ReportViewerSVC()
        {
            InitializeComponent();
            crystalReportViewer1.ShowGroupTreeButton = false;
        }

        private void ReportViewerHP_Load(object sender, EventArgs e)
        {
            LoadAllPrinters(); //by akila 2017/07/03
            lblPrinter.Text = "Default Printer- " + GetDefaultPrinter();
            string _repname = string.Empty;
            string _papersize = string.Empty;
            _repname = "";
            //CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
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

            if (BaseCls.GlbReportName == null || BaseCls.GlbReportName == "")
            {
                MessageBox.Show("Report is not setup. Contact IT Department...\n", "Report not Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (BaseCls.GlbReportName == "RCC_Report.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    RCCReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "RCCReport.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    RCCReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "Job_Summary.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    JobSummaryReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "ServiceJobCardAut.rpt")
                {//
                    crystalReportViewer1.ShowExportButton = true;
                    ServiceJobCardAuto();
                };
                if (BaseCls.GlbReportName == "df_exchange_report.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DFExchangeReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "Job_Defect_Analysis.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    DefectAnalysisReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };

                if (BaseCls.GlbReportName == "Process_Tracking_Report.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowGroupTreeButton = true;
                    ProcessTrackingReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "smart_warr_Iss_Dtl_Report.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowGroupTreeButton = true;
                    SmartWarrIssueReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "ServiceJobCardAut.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    ServiceJobCardAuto();
                };

                if (BaseCls.GlbReportName == "JobSheet_FieldVisit.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    ServiceFieldVisitJobSheet();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "JobCard_Gen_Field.rpt")
                {
                    //BaseCls.GlbReportTp = "JOBF";
                    FieldJobCard();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "JobCard_Gen_TechMemo.rpt")
                {
                    //BaseCls.GlbReportTp = "JOBF";
                    TechMemoElecJobCard();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "JobCard_Gen_TechMemo2.rpt")
                {
                    //BaseCls.GlbReportTp = "JOBF";
                    TechMemoElecJobCard2();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "JobCard_Gen_WShop.rpt")
                {
                    //BaseCls.GlbReportTp = "JOBW";
                    WshopJobCard();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "JobCard_Gen_WShop_SGL.rpt")
                {
                    //BaseCls.GlbReportTp = "JOBW";
                    WshopSGLJobCard();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "JobCard_ITS.rpt")
                {
                    //BaseCls.GlbReportTp = "JOBW";
                    WshopITSJobCard();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "JobCard_AC_WShop.rpt")
                {                    
                    WshopACWJobCard();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "JobCard_ACBW_WShop.rpt")
                {                    
                    WshopACBWWJobCard();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "JobCard_ACIns_WShop.rpt")
                {
                    WshopACInsJobCard();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "JobCard_RF_WShop.rpt")
                {                    
                    WshopRFWJobCard();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "JobCard_WM_WShop.rpt")
                {                    
                    WshopWMWJobCard();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "JobCard_ITS_Field.rpt")
                {
                    //BaseCls.GlbReportTp = "JOBW";
                    WshopITSFieldJobCard();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "JobCard_Gen_WShop_Phone.rpt")
                {
                    //BaseCls.GlbReportTp = "JOBW";
                    WshopJobCard_Ph();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "JobCard_Gen_WShop_Arial.rpt")
                {
                    //BaseCls.GlbReportTp = "JOBW";
                    WshopJobCardArial();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "Job_Estimate.rpt")
                {
                    //BaseCls.GlbReportTp = "EST";
                    JobEstimate();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "Job_Gatepass.rpt")
                {
                    //BaseCls.GlbReportTp = "GP";
                    Gatepass();
                    //btnPrint.Visible = false;
                    //crystalReportViewer1.ShowPrintButton = true;
                };
                 if (BaseCls.GlbReportName == "Tech_Comments.rpt")
                 {
                     TechComment();
                     btnPrint.Visible = false;
                     crystalReportViewer1.ShowPrintButton = true;
                 };
                 if (BaseCls.GlbReportName == "Job_BER_Letter.rpt")
                 {
                     BERLetter();
                     btnPrint.Visible = false;
                     crystalReportViewer1.ShowPrintButton = true;
                 };
                 if (BaseCls.GlbReportName == "Repeated_Jobs.rpt")
                 {
                     RepeatedJobs();
                     btnPrint.Visible = false;
                     crystalReportViewer1.ShowExportButton = true;
                     crystalReportViewer1.ShowPrintButton = true;
                 };
                 if (BaseCls.GlbReportName == "Repeated_Jobs_Ph.rpt")
                 {
                     RepeatedJobs();
                     btnPrint.Visible = false;
                     crystalReportViewer1.ShowExportButton = true;
                     crystalReportViewer1.ShowPrintButton = true;
                 };
                 if (BaseCls.GlbReportName == "ServiceGP.rpt")
                 {
                     ServiceJobGP();
                     btnPrint.Visible = false;
                     crystalReportViewer1.ShowPrintButton = true;
                 };
                 if (BaseCls.GlbReportName == "ServiceGP_Detail.rpt")
                 {
                     ServiceJobGPDetail();
                     btnPrint.Visible = false;
                     crystalReportViewer1.ShowPrintButton = true;
                 };

                 if (BaseCls.GlbReportName == "AgreementDet.rpt")
                 {
                     ServiceAgreement();
                     btnPrint.Visible = false;
                     crystalReportViewer1.ShowPrintButton = true;
                     crystalReportViewer1.ShowExportButton = true;
                 };

                 if (BaseCls.GlbReportName == "Incentive_Detail.rpt")
                 {
                     IncentiveDetailReport();
                     btnPrint.Visible = false;
                     crystalReportViewer1.ShowPrintButton = true;
                 };

                 if (BaseCls.GlbReportName == "ServiceStandyIssue.rpt")
                 {
                     ServiceStandBy();
                     btnPrint.Visible = false;
                     crystalReportViewer1.ShowPrintButton = true;
                 };
                 if (BaseCls.GlbReportName == "Estimate_Det.rpt")
                 {
                     EstimateDetails();
                     btnPrint.Visible = false;
                     crystalReportViewer1.ShowPrintButton = true;
                 };
                 if (BaseCls.GlbReportName == "SupplierWarranty.rpt") 
                 {
                     ServiceSupplierClaim();
                     btnPrint.Visible = false;
                     crystalReportViewer1.ShowPrintButton = true;
                 };
                if (BaseCls.GlbReportName == "SupplierWarranty_Excel.rpt")
                {
                    ServiceSupplierClaim_Excel();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                //Tharanga 2017/06/10
                if (BaseCls.GlbReportName == "Job_Gatepassnew.rpt")
                {
                    //BaseCls.GlbReportTp = "GP";
                    Gatepassnew();
                    //btnPrint.Visible = false;
                    //crystalReportViewer1.ShowPrintButton = true;
                };
                //Tharanga 2017/06/13
                if (BaseCls.GlbReportName == "FeildJobCard.rpt")
                {
                    //BaseCls.GlbReportTp = "GP";
                    feildJobCard();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                //Tharanga 2017/06/13
                if (BaseCls.GlbReportName == "JobCard_Power_tools.rpt")
                {
                    //BaseCls.GlbReportTp = "GP";
                    Wshop_power_tool_JobCard();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                
                 if (BaseCls.GlbReportName == "job_EstimateAuto.rpt")
                 {
                     //BaseCls.GlbReportTp = "EST";
                     JobEstimateAuto();
                     btnPrint.Visible = false;
                     crystalReportViewer1.ShowPrintButton = true;
                 };
                 if (BaseCls.GlbReportName == "JobCard_ABE_Work_shop.rpt")//Tharanga 2017/07/05
                 {
                     btnPrint.Visible = false;
                     crystalReportViewer1.ShowPrintButton = false;
                     crystalReportViewer1.ShowExportButton = false;

                     if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10157))
                     {
                         ABE_ServiceJobCard_Workshop();

                         if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10158))
                         {
                             btnPrint.Visible = true;
                         }
                         else
                         {
                             MessageBox.Show("You don't have the permission to print this.\nPermission Code :- 10158", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                         }
                     }
                     else
                     {
                         MessageBox.Show("You don't have the permission show this Print.\nPermission Code :- 10157", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                         return;
                     }
                 }
                 if (BaseCls.GlbReportName == "JobCard_ABE_Feild.rpt")//Tharanga 2017/07/05
                 {
                     btnPrint.Visible = false;
                     crystalReportViewer1.ShowPrintButton = false;
                     crystalReportViewer1.ShowExportButton = false;

                     if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10157))
                     {
                         ABE_ServiceJobCard_Feild();

                         if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10158))
                         {
                             btnPrint.Visible = true;
                         }
                         else
                         {
                             MessageBox.Show("You don't have the permission to print this.\nPermission Code :- 10158", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                         }
                     }
                     else
                     {
                         MessageBox.Show("You don't have the permission show this Print.\nPermission Code :- 10157", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                         return;
                     }
                 }
                //Tharanga 2017/07/11
                 if (BaseCls.GlbReportName == "Spare_Parts_Movement_Report.rpt")
                 {
                     //BaseCls.GlbReportTp = "GP";
                     Get_Spare_arts_Movement_Report();
                     btnPrint.Visible = true;
                     crystalReportViewer1.ShowPrintButton = false;
                 };
                 //Tharanga 2017/07/11
                 if (BaseCls.GlbReportName == "Spare_parts_movement.rpt")
                 {
                     //BaseCls.GlbReportTp = "GP";
                     Get_Spare_arts_Movement();
                     btnPrint.Visible = true;
                     crystalReportViewer1.ShowPrintButton = false;
                 };
                 if (BaseCls.GlbReportName == "job_Estimate_ABE.rpt")
                 {
                     
                     JobEstimate_ABE();
                     btnPrint.Visible = true;
                     crystalReportViewer1.ShowPrintButton = false;
                 };
                 if (BaseCls.GlbReportName == "Job_BER_Letter_ABE.rpt") //Tharanga 2017/07/18
                 {
                     BERLetter_BER();
                     btnPrint.Visible = true;
                     crystalReportViewer1.ShowPrintButton = false;
                 };
                 if (BaseCls.GlbReportName == "RedyForCollection_ABE.rpt" || BaseCls.GlbReportName == "RedyForDelivery_ABE.rpt" || BaseCls.GlbReportName == "ReadyForDisposal_ABE.rpt" || BaseCls.GlbReportName  == "ShipmentLetter_ABE.rpt"
                     || BaseCls.GlbReportName == "Cannotbbrepaired_ABE.rpt" || BaseCls.GlbReportName == "NoticeOfDisposal_ABE.rpt")//Tharanga 2017/07/18
                 {
                     RedyForCollection_ABE();
                     btnPrint.Visible = true;
                     crystalReportViewer1.ShowPrintButton = false;
                 };
                 if (BaseCls.GlbReportName == "BrandModelwiseItemDetails_ABE.rpt" || BaseCls.GlbReportName == "BrandModelwiseItemSummary_ABE_N.rpt")//Tharanga 2017/07/27
                 {
                     JobDetailwithBrandandModel_ABE();
                     btnPrint.Visible = true;
                     crystalReportViewer1.ShowPrintButton = false;
                 };

                 if (BaseCls.GlbReportName == "RCC_Letter.rpt" || BaseCls.GlbReportName == "RCC_Letter.rpt")
                 {
                     RccLetterPrint();
                     btnPrint.Visible = true;
                     crystalReportViewer1.ShowPrintButton = false;
                 };
                 
                

                 BaseCls.GlbReportTp = string.Empty;
            }
        }

        //private void RCCPrint()
        //{
        //    DataTable param = new DataTable();
        //    DataRow dr;

        //    DataTable _RCC = CHNLSVC.Financial.Print_RCC_Receipt(BaseCls.GlbReportDoc);
        //    DataTable _retCond = CHNLSVC.Financial.Print_RCC_Ret_Condition(BaseCls.GlbReportDoc);

        //    _rccPrint.Database.Tables["RCC"].SetDataSource(_RCC);
        //    _rccPrint.Database.Tables["Ret_Cond"].SetDataSource(_retCond);

        //    crystalReportViewer1.ReportSource = _rccPrint;

        //    crystalReportViewer1.RefreshReport();

        //}

        private void RCCReport()
        { //
            objSvc.RCC_Report();
            if (BaseCls.GlbReportName == "RCCReport.rpt")
            crystalReportViewer1.ReportSource = objSvc._rccReport;
            else
                crystalReportViewer1.ReportSource = objSvc._rcc_Report;
            this.Text = "RCC Listing";
            crystalReportViewer1.RefreshReport();
        }
    
        private void JobSummaryReport()
        { //
            objSvc.JobSummary_Report();
            crystalReportViewer1.ReportSource = objSvc._Jobsum;
            this.Text = "Job Summary";
            crystalReportViewer1.RefreshReport();
        }

        private void DFExchangeReport()
        { //
            objSvc.DFExchange_Report();
            crystalReportViewer1.ReportSource = objSvc._df_Exchg;
            this.Text = "Exchange Report";
            crystalReportViewer1.RefreshReport();
        }

        private void ProcessTrackingReport()
        { //
            objSvc.ProcessTracking_Report();
            crystalReportViewer1.ReportSource = objSvc._ProcTrack;
            this.Text = "Process Tracking Report";
            crystalReportViewer1.RefreshReport();
        }
        private void SmartWarrIssueReport()
        { //
            objSvc.Smart_Insu_Claim_Report();
            crystalReportViewer1.ReportSource = objSvc._smrt_Warr;
            this.Text = "Smart Insurance Claim Details Report";
            crystalReportViewer1.RefreshReport();
        }
        private void FieldJobCard()
        { //
            objSvc.ServiceJobCardPrint();
            crystalReportViewer1.ReportSource = objSvc._JobCardF;
            this.Text = "Job Card";
            crystalReportViewer1.RefreshReport();
        }

        private void TechMemoElecJobCard()
        { //
            objSvc.ServiceJobCardPrint();
            crystalReportViewer1.ReportSource = objSvc._JobCardTMemo ;
            this.Text = "Job Card";
            crystalReportViewer1.RefreshReport();
        }
        private void TechMemoElecJobCard2()
        { //
            objSvc.ServiceJobCardPrint();
            crystalReportViewer1.ReportSource = objSvc._JobCardTMemo2 ;
            this.Text = "Job Card";
            crystalReportViewer1.RefreshReport();
        }
        private void WshopJobCard()
        { //
            objSvc.ServiceJobCardPrint();
            crystalReportViewer1.ReportSource = objSvc._JobCardW ;
            this.Text = "Job Card";
            crystalReportViewer1.RefreshReport();
        }
        
        private void WshopSGLJobCard()
        { //
            objSvc.ServiceJobCardPrint();
            crystalReportViewer1.ReportSource = objSvc._JobCardSGL ;
            this.Text = "Job Card";
            crystalReportViewer1.RefreshReport();
        }

        private void WshopITSJobCard()
        { //
            objSvc.ServiceJobCardPrint();
            crystalReportViewer1.ReportSource = objSvc._JobCardITS ;
            this.Text = "Job Card";
            crystalReportViewer1.RefreshReport();
        }
        private void WshopWMWJobCard()
        { //
            objSvc.ServiceJobCardPrint();
            crystalReportViewer1.ReportSource = objSvc._JobCardWMW ;
            this.Text = "Job Card";
            crystalReportViewer1.RefreshReport();
        }
        private void WshopRFWJobCard()
        { //
            objSvc.ServiceJobCardPrint();
            crystalReportViewer1.ReportSource = objSvc._JobCardRFW ;
            this.Text = "Job Card";
            crystalReportViewer1.RefreshReport();
        }
        private void WshopACWJobCard()
        { //
            objSvc.ServiceJobCardPrint();
            crystalReportViewer1.ReportSource = objSvc._JobCardACW ;
            this.Text = "Job Card";
            crystalReportViewer1.RefreshReport();
        }
        private void WshopACBDWJobCard()
        { //
            objSvc.ServiceJobCardPrint();
            crystalReportViewer1.ReportSource = objSvc._JobCardACBDW ;
            this.Text = "Job Card";
            crystalReportViewer1.RefreshReport();
        }
        private void WshopACInsJobCard()
        { //
            objSvc.ServiceJobCardPrint();
            crystalReportViewer1.ReportSource = objSvc._JobCardACIns ;
            this.Text = "Job Card";
            crystalReportViewer1.RefreshReport();
        }
        private void WshopACBWWJobCard()
        { //
            objSvc.ServiceJobCardPrint();
            crystalReportViewer1.ReportSource = objSvc._JobCardACBWW ;
            this.Text = "Job Card";
            crystalReportViewer1.RefreshReport();
        }
        private void WshopITSFieldJobCard()
        { //
            objSvc.ServiceJobCardPrint();
            crystalReportViewer1.ReportSource = objSvc._JobCardITS_F ;
            this.Text = "Job Card";
            crystalReportViewer1.RefreshReport();
        }
        private void WshopJobCard_Ph()
        { //
            objSvc.ServiceJobCardPrint();
            crystalReportViewer1.ReportSource = objSvc._JobCardWPh ;
            this.Text = "Job Card";
            //BaseCls.GlbReportDirectPrint = 1;
            //if (BaseCls.GlbReportDirectPrint==1)
            //{
            //    crystalReportViewer1.PrintReport();
            //}
            //else
           // { 
                crystalReportViewer1.RefreshReport();
            //}
        }
        private void WshopJobCardArial()
        { //
            objSvc.ServiceJobCardPrint();
            crystalReportViewer1.ReportSource = objSvc._JobCardWArial ;
            this.Text = "Job Card";
            crystalReportViewer1.RefreshReport();
        }
        private void JobEstimate()
        { //
            objSvc.ServiceJobEstimate();
            crystalReportViewer1.ReportSource = objSvc._JobEstimate ;
            this.Text = "Job Estimate";
            crystalReportViewer1.RefreshReport();
        }
        private void JobEstimateAuto()
        { //
            objSvc.ServiceJobEstimateAuto();
            crystalReportViewer1.ReportSource = objSvc._job_EstimateAuto;
            this.Text = "Job Estimate";
            crystalReportViewer1.RefreshReport();
        }
        

        private void Gatepass()
        { //
            objSvc.ServiceGatepass();
            crystalReportViewer1.ReportSource = objSvc._JobGatepass ;
            this.Text = "Gatepass";
            crystalReportViewer1.RefreshReport();           
        }
        //Tharanga 2017/06/10
        private void Gatepassnew()
        { //
            objSvc.ServiceGatepassnew();
            crystalReportViewer1.ReportSource = objSvc._JobGatepassnew;
            this.Text = "Gatepassnew";
            crystalReportViewer1.RefreshReport();

             
        }
        private void TechComment()
        { //
            objSvc.TechComment_Report();
            crystalReportViewer1.ReportSource = objSvc._techComnt;
            this.Text = "Technician Comment";
            crystalReportViewer1.RefreshReport();
        }

        private void BERLetter()
        {
            objSvc.BERLetterPrint();
            crystalReportViewer1.ReportSource = objSvc._BERLetter ;
            this.Text = "BER Letter";
        
            crystalReportViewer1.RefreshReport();
                      
        }

        private void RepeatedJobs()
        { //
            objSvc.RepeatedJobs_Report();
            if (BaseCls.GlbReportName == "Repeated_Jobs_Ph.rpt")
            {
                crystalReportViewer1.ReportSource = objSvc._repeatJobsph ;
            }
            else
            {
                crystalReportViewer1.ReportSource = objSvc._repeatJobs;
            }
            this.Text = "Repeated Jobs";
            crystalReportViewer1.RefreshReport();
        }

        private void DefectAnalysisReport()
        { //
            objSvc.DefectAnalysis_Report();
            crystalReportViewer1.ReportSource = objSvc._defAnal;
            this.Text = "Defect Analysis Report";
            crystalReportViewer1.RefreshReport();
        }

        private void ServiceFieldVisitJobSheet()
        { //
            objSvc.ServiceFieldVisitJobSheet();
            crystalReportViewer1.ReportSource = objSvc._Jobsheetfieldvisit;
            this.Text = "Job Sheet";
            crystalReportViewer1.RefreshReport();
        }
        private void ServiceJobCard()
        {
            objSvc.ServiceJobCard();
            crystalReportViewer1.ReportSource = objSvc._serJob;
            this.Text = "Service Job Card";

            crystalReportViewer1.RefreshReport();
        }

        private void ServiceJobCardAuto()
        {
            objSvc.ServiceJobCardAuto();
            crystalReportViewer1.ReportSource = objSvc._serJobAuto;
            this.Text = "Service Job Card";

            crystalReportViewer1.RefreshReport();
        }

        //Tharanga 2017/06/13
        private void feildJobCard()
        {
            objSvc.ServiceFeildJobCard();
            crystalReportViewer1.ReportSource = objSvc._FeildJobCard;
            this.Text = "Feild Job Card";

            crystalReportViewer1.RefreshReport();
        }
        private void SVCJobDtlPrint()
        //Sanjeewa 09-02-2013
        {
            objSvc.ServiceJobSummaryReport();
            crystalReportViewer1.ReportSource = objSvc._SvcJob;
            this.Text = "Service Job Details Report";

            crystalReportViewer1.RefreshReport();

        }

        private void SVCJobSummPrint()
        //Sanjeewa 15-08-2013
        {
            objSvc.ServiceJobReport();
            crystalReportViewer1.ReportSource = objSvc._SvcJobSumm;
            this.Text = "Service Job Summary Report";

            crystalReportViewer1.RefreshReport();

        }
        private void ServiceAgreement()
        //Nadeeka 07-11-2015
        {
            objSvc.ServiceAgreement();
            crystalReportViewer1.ReportSource = objSvc._serAgree;
            this.Text = "Service Agreement Report";

            crystalReportViewer1.RefreshReport();

        }

        private void IncentiveDetailReport()
        //Sanjeewa 09-11-2015
        {
            objSvc.IncentiveDetailReport();
            crystalReportViewer1.ReportSource = objSvc._incdet;
            this.Text = "Service Incentive Report";

            crystalReportViewer1.RefreshReport();

        }
        private void ServiceJobGP()
        //Nadeeka 27-04-2015
        {
            objSvc.ServiceJobGP();
            crystalReportViewer1.ReportSource = objSvc._sergp;
            this.Text = "Service Job GP Report";

            crystalReportViewer1.RefreshReport();

        }
        private void ServiceJobGPDetail()
        //Sanjeewa 21-09-2015
        {
            objSvc.ServiceJobGPDetail();
            crystalReportViewer1.ReportSource = objSvc._sergpdtl;
            this.Text = "Service Job GP Report";

            crystalReportViewer1.RefreshReport();

        }
        private void ServiceSupplierClaim()
        //Nadeeka 06-05-2015
        {
            objSvc.ServiceSupplierClaim();
            crystalReportViewer1.ReportSource = objSvc._suppWar;
            this.Text = "Supplier Warranty";

            crystalReportViewer1.RefreshReport();

        }
        private void ServiceSupplierClaim_Excel()
        //Nadeeka 06-05-2015
        {
            objSvc.ServiceSupplierClaim();
            crystalReportViewer1.ReportSource = objSvc._suppWarEx;
            this.Text = "Supplier Warranty";

            crystalReportViewer1.RefreshReport();

        }

        private void ServiceStandBy()
        //Nadeeka 27-04-2015
        {
            objSvc.ServiceStandBy();
            crystalReportViewer1.ReportSource = objSvc._stdby;
            this.Text = "Service Stand By Issue Report";

            crystalReportViewer1.RefreshReport();

        }
        //Tharanga 2017/06/13
        private void Wshop_power_tool_JobCard()
        { //
            objSvc.ServiceJobCardPowerToolPrint();
            crystalReportViewer1.ReportSource = objSvc._JobCard_Power_tools;
            this.Text = "Job Card";
            crystalReportViewer1.RefreshReport();
        }
        private void EstimateDetails()
        //kapila 27-04-2015
        {
            objSvc.ServiceJobEstimates();
            crystalReportViewer1.ReportSource = objSvc._estDet;
            this.Text = "Estimate Details Report";

            crystalReportViewer1.RefreshReport();

        }
        string GetDefaultPrinter()
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
            //string _printerName = string.Empty;
            ////PrinterSettings settings = new PrinterSettings();
            ////foreach (string printer in PrinterSettings.InstalledPrinters)
            ////{
            ////    settings.PrinterName = printer;
            ////    if (settings.IsDefaultPrinter)
            ////    {
            ////        _printerName = printer;
            ////        break;
            ////    }
            ////}
            //_printerName = printDialog1.PrinterSettings.PrinterName;
            //return _printerName;
        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            this.crystalReportViewer1.Dispose();
            this.crystalReportViewer1 = null;
            GC.Collect();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (BaseCls.GlbReportName == "Service_Job_Rpt.rpt")
            {
                ////PrintPDF();
                objSvc._SvcJob.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int

                objSvc._SvcJob.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                objSvc._SvcJob.PrintToPrinter(1, false, 0, 0);
                //btnPrint.Enabled = false;

            };
            //if (BaseCls.GlbReportName == "RCCPrint_New.rpt")
            //{

            //    _rccPrint.PrintOptions.PrinterName = GetDefaultPrinter();
            //    int papernbr = getprtnbr("DO"); // returns 257 int
            //    _rccPrint.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
            //    _rccPrint.PrintToPrinter(1, false, 0, 0);
            //    btnPrint.Enabled = false;


            //}
            if (BaseCls.GlbReportName == "ServiceJobCard.rpt")
            {    objSvc._serJob.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int
                //objSvc._rccPrint.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                objSvc._serJob.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
                crystalReportViewer1.ShowPrintButton = false;
                //btnPrint.Enabled = false;
            } ;

            if (BaseCls.GlbReportName == "ServiceJobCardAut.rpt")
            {
                objSvc._serJobAuto.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int
                //objSvc._rccPrint.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                objSvc._serJobAuto.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
                crystalReportViewer1.ShowPrintButton = false;
                //btnPrint.Enabled = false;
            };


            if (BaseCls.GlbReportName == "RCCReport.rpt")
            {
                objSvc._rccReport.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int
                //objSvc._rccPrint.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                objSvc._rccReport.PrintToPrinter(1, false, 0, 0);
                crystalReportViewer1.ShowPrintButton = false;
                //btnPrint.Enabled = false;
            };
            if (BaseCls.GlbReportName == "JobSheet_FieldVisit.rpt")
            {
                objSvc._Jobsheetfieldvisit.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int
                objSvc._Jobsheetfieldvisit.PrintToPrinter(1, false, 0, 0);
                crystalReportViewer1.ShowPrintButton = false;
            };
            if (BaseCls.GlbReportName == "Job_Gatepass.rpt")
            {
                PageMargins margins;
                margins.bottomMargin = 0;
                margins.leftMargin = 0;
                margins.rightMargin = 0;
                margins.topMargin = 0;
                objSvc._JobGatepass.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("DO"); // returns 257 int
                objSvc._JobGatepass.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                objSvc._JobGatepass.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
            };
            //Tharanga 2017/06/14
            if (BaseCls.GlbReportName == "Job_Gatepassnew.rpt")
            {
                PageMargins margins;
                margins.bottomMargin = 0;
                margins.leftMargin = 0;
                margins.rightMargin = 0;
                margins.topMargin = 0;
                objSvc._JobGatepassnew.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("DO"); // returns 257 int
                objSvc._JobGatepassnew.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                objSvc._JobGatepassnew.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
            };
            //Tharanga 2017/06/17
            if (BaseCls.GlbReportName == "job_EstimateAuto.rpt")
            {
                objSvc._job_EstimateAuto.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int
                objSvc._job_EstimateAuto.PrintToPrinter(1, false, 0, 0);
                crystalReportViewer1.ShowPrintButton = false;
            };

            if (BaseCls.GlbReportName == "JobCard_ABE_Work_shop.rpt")
            {
                objSvc._JobCard_ABE_Work_shop.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int
                objSvc._JobCard_ABE_Work_shop.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
                crystalReportViewer1.ShowPrintButton = false;
            };
            if (BaseCls.GlbReportName == "JobCard_ABE_Feild.rpt")
            {
                objSvc._JobCard_ABE_Feild.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int
                objSvc._JobCard_ABE_Feild.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
                crystalReportViewer1.ShowPrintButton = false;
            };
            if (BaseCls.GlbReportName == "Spare_Parts_Movement_Report.rpt")
            {
                objSvc._Spare_Parts_Movement_Report.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int
                objSvc._Spare_Parts_Movement_Report.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
                crystalReportViewer1.ShowPrintButton = false;
            };
            if (BaseCls.GlbReportName == "job_Estimate_ABE.rpt")
            {
                objSvc._job_Estimate_ABE.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int
                objSvc._job_Estimate_ABE.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                objSvc._job_Estimate_ABE.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;

            };
            if (BaseCls.GlbReportName == "Job_BER_Letter_ABE.rpt")
            {
                objSvc._Job_BER_Letter_ABE.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int
                objSvc._Job_BER_Letter_ABE.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
                crystalReportViewer1.ShowPrintButton = false;
            };
            if (BaseCls.GlbReportName == "RedyForCollection_ABE.rpt")//Tharanga 2017/07/25
            {
                objSvc._RedyForCollection_ABE.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int
                objSvc._RedyForCollection_ABE.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
                crystalReportViewer1.ShowPrintButton = false;
            };
            if (BaseCls.GlbReportName == "RedyForDelivery_ABE.rpt")//Tharanga 2017/07/25
            {
                objSvc._RedyForDelivery_ABE.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int
                objSvc._RedyForDelivery_ABE.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
                crystalReportViewer1.ShowPrintButton = false;
            };
            if (BaseCls.GlbReportName == "ReadyForDisposal_ABE.rpt")//Tharanga 2017/07/25
            {
                objSvc._ReadyForDisposal_ABE.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int
                objSvc._ReadyForDisposal_ABE.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
                crystalReportViewer1.ShowPrintButton = false;
              
            };
            if (BaseCls.GlbReportName == "ShipmentLetter_ABE.rpt")//Tharanga 2017/07/25
            {
                objSvc._ShipmentLetter_ABE.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); 
                objSvc._ShipmentLetter_ABE.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
                crystalReportViewer1.ShowPrintButton = false;
               
            };
            if (BaseCls.GlbReportName == "Cannotbbrepaired_ABE.rpt")//Tharanga 2017/07/27
            {
                objSvc._Cannotbbrepaired_ABE.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int
                objSvc._Cannotbbrepaired_ABE.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
                crystalReportViewer1.ShowPrintButton = false;
            };
            if (BaseCls.GlbReportName == "NoticeOfDisposal_ABE.rpt")//Tharanga 2017/07/27
            {
                objSvc._NoticeOfDisposal_ABE.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int
                objSvc._NoticeOfDisposal_ABE.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
                crystalReportViewer1.ShowPrintButton = false;
            };
            if (BaseCls.GlbReportName == "BrandModelwiseItemSummary_ABE_N.rpt")//Tharanga 2017/07/27
            {
                objSvc._BrandModelwiseItemSummary_ABE_N.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int
                objSvc._BrandModelwiseItemSummary_ABE_N.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
                crystalReportViewer1.ShowPrintButton = false;
            };
            if (BaseCls.GlbReportName == "BrandModelwiseItemDetails_ABE.rpt")//Tharanga 2017/07/27
            {
                objSvc._BrandModelwiseItemDetails_ABE.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int
                objSvc._BrandModelwiseItemDetails_ABE.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
                crystalReportViewer1.ShowPrintButton = false;
            };
            if (BaseCls.GlbReportName == "BrandModelwiseItemSummary_ABE_N.rpt")//Tharanga 2017/07/27
            {
                objSvc._BrandModelwiseItemSummary_ABE_N.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int
                objSvc._BrandModelwiseItemSummary_ABE_N.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
                crystalReportViewer1.ShowPrintButton = false;
            };//

            if (BaseCls.GlbReportName == "RCC_Letter.rpt")
            {
                objSvc._rccReport.PrintOptions.PrinterName = GetDefaultPrinter();
                int papernbr = getprtnbr("Letter"); // returns 257 int
                //objSvc._rccPrint.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                objSvc.objrccletter.PrintToPrinter(1, false, 0, 0);
                crystalReportViewer1.ShowPrintButton = false;
                //btnPrint.Enabled = false;
            };
            
            


        }

        public static int getprtnbr(string pnm)
        {
            PrintDocument printDoc = new PrintDocument();
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
        private List<InvoiceItem> BindAccountItem(string _account)
        {
            List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account);
            List<InvoiceItem> _itemList = new List<InvoiceItem>();

            if (_invoice != null)
                if (_invoice.Count > 0)
                {
                    _invoice = _invoice.OrderByDescending(x => x.Sah_direct).ToList();

                    var _sales = from _lst in _invoice
                                 where _lst.Sah_direct == true
                                 select _lst;
                    foreach (InvoiceHeader _hdr in _sales)
                    {
                        string _invoiceno = _hdr.Sah_inv_no;
                        List<InvoiceItem> _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoiceno);
                        var _forwardSale = from _lst in _invItem
                                           where _lst.Sad_qty - _lst.Sad_do_qty > 0
                                           select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_qty - _lst.Sad_do_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = true };

                        var _deliverdSale = from _lst in _invItem
                                            where _lst.Sad_do_qty - _lst.Sad_srn_qty > 0
                                            select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_do_qty - _lst.Sad_srn_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = false };

                        if (_forwardSale.Count() > 0)
                            _itemList.AddRange(_forwardSale);

                        if (_deliverdSale.Count() > 0)
                            _itemList.AddRange(_deliverdSale);
                    }
                }
            return _itemList;
        }

        //by akila 2017/07/03
        private void LoadAllPrinters()
        {
            foreach (var item in PrinterSettings.InstalledPrinters)
            {
                this.lbPrinters.Items.Add(item.ToString());
            }
        }

        private void lbPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            myPrinters.SetDefaultPrinter(GetDefaultPrinter());
            string pname = this.lbPrinters.SelectedItem.ToString();
            myPrinters.SetDefaultPrinter(pname);
            lblPrinter.Text = "Default Printer- " + GetDefaultPrinter();
        }

        public static class myPrinters
        {
            [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool SetDefaultPrinter(string Name);

        }

        private void ABE_ServiceJobCard_Workshop() //tharanga 2017/07/05
        { //
            objSvc.ABE_ServiceJobCard_Workshop();
            crystalReportViewer1.ReportSource = objSvc._JobCard_ABE_Work_shop;
            this.Text = "Job Card";
            crystalReportViewer1.RefreshReport();
        }
        private void ABE_ServiceJobCard_Feild() //tharanga 2017/07/05
        { //
            objSvc.ABE_ServiceJobCard_Workshop();
            crystalReportViewer1.ReportSource = objSvc._JobCard_ABE_Feild;
            this.Text = "Job Card";
            crystalReportViewer1.RefreshReport();
        }

        private void Get_Spare_arts_Movement_Report() //Tharanga 2017/07/11
        { //
            objSvc.Get_Spare_parts_Movement_Report();
            crystalReportViewer1.ReportSource = objSvc._Spare_Parts_Movement_Report;
            this.Text = "Job Card";
            crystalReportViewer1.RefreshReport();
        }

        private void Get_Spare_arts_Movement() //Tharanga 2017/07/11
        { //
            objSvc.Get_Spare_parts_Movement_Report();
            crystalReportViewer1.ReportSource = objSvc._Spare_parts_movement;
            this.Text = "Job Card";
            crystalReportViewer1.RefreshReport();
        }
        private void JobEstimate_ABE() //Tharanga 2017/07/18
        { //
            objSvc.ServiceJobEstimate_ABE();
            crystalReportViewer1.ReportSource = objSvc._job_Estimate_ABE;
            this.Text = "Job Estimate";
            crystalReportViewer1.RefreshReport();
        }

        private void BERLetter_BER() //Tharanga 2017/07/18
        {
            objSvc.BERLetterPrint_ABE();
            crystalReportViewer1.ReportSource = objSvc._Job_BER_Letter_ABE;
            this.Text = "BER Letter";
            crystalReportViewer1.RefreshReport();
        }
        private void RedyForCollection_ABE() //Tharanga 2017/07/18
        {
            objSvc.RedyForCollection_ABE();
            if (BaseCls.GlbReportName == "RedyForCollection_ABE.rpt")
            {
                crystalReportViewer1.ReportSource = objSvc._RedyForCollection_ABE;
            }
            else if (BaseCls.GlbReportName == "RedyForDelivery_ABE.rpt")
            {
                crystalReportViewer1.ReportSource = objSvc._RedyForDelivery_ABE;
            }
            else if (BaseCls.GlbReportName == "ReadyForDisposal_ABE.rpt")
            {
                crystalReportViewer1.ReportSource = objSvc._ReadyForDisposal_ABE;
            }
            else if (BaseCls.GlbReportName == "ShipmentLetter_ABE.rpt")
            {
                crystalReportViewer1.ReportSource = objSvc._ShipmentLetter_ABE;
            }
            else if (BaseCls.GlbReportName == "Cannotbbrepaired_ABE.rpt")
            {
                crystalReportViewer1.ReportSource = objSvc._Cannotbbrepaired_ABE;
            }
            else if (BaseCls.GlbReportName == "NoticeOfDisposal_ABE.rpt")
            {
                crystalReportViewer1.ReportSource = objSvc._NoticeOfDisposal_ABE;
            }
            else if (BaseCls.GlbReportName == "BrandModelwiseItemSummary_ABE_N.rpt")
            {
                crystalReportViewer1.ReportSource = objSvc._BrandModelwiseItemSummary_ABE_N;
            }
         
            this.Text = "Ready Letter";
            crystalReportViewer1.RefreshReport();
        }
        private void JobDetailwithBrandandModel_ABE() //Tharanga 2017/07/18
        {
            objSvc.JobDetailwithBrandandModel_ABE();
            if (BaseCls.GlbReportName == "JobDetailwithBrandandModel_ABE")
            {
                crystalReportViewer1.ReportSource = objSvc._BrandModelwiseItemDetails_ABE;
            }
            else if (BaseCls.GlbReportName == "BrandModelwiseItemSummary_ABE_N.rpt")
            {
                crystalReportViewer1.ReportSource = objSvc._BrandModelwiseItemSummary_ABE_N;
            }
            else if (BaseCls.GlbReportName == "BrandModelwiseItemDetails_ABE.rpt")
            {
                crystalReportViewer1.ReportSource = objSvc._BrandModelwiseItemDetails_ABE;
            }
            
            this.Text = "Ready Letter";
            crystalReportViewer1.RefreshReport();
        }

        private void RccLetterPrint() //Tharindu 2017-12-14
        {
            objSvc.RccLetterPrint();
            crystalReportViewer1.ReportSource = objSvc.objrccletter;
            this.Text = "RCC Letter";
            crystalReportViewer1.RefreshReport();
        }
    }
}
