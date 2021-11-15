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

namespace FF.WindowsERPClient.Reports.Reconciliation
{
    public partial class ReportViewerRecon  : Base
    {
        clsRecon obj = new clsRecon();
        public ReportViewerRecon()
        {
            InitializeComponent();
            lblPrinter.Text = "Default Printer- " + GetDefaultPrinter();
           //F
            
        }
        private void ReportViewerRecon_Load(object sender, EventArgs e)
        {
            if (BaseCls.GlbReportName == "SalesReconcilation.rpt")
            {

                SalesReconcilationReport();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            };
            if (BaseCls.GlbReportName == "RegisProcess.rpt")
            {

                RegistrationProcessReport();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            };
            if (BaseCls.GlbReportName == "TransactionVariance.rpt")
            {

                TransactionVarianceReport();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            };

            if (BaseCls.GlbReportName == "Last_No_Seq_Rep.rpt")
            {             
                LastNoSeqReport();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            };

            if (BaseCls.GlbReportName == "Request_Approval_Details.rpt")
            {
                RequestApprovalDetailsReport();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            };
            if (BaseCls.GlbReportName == "app_status_by_reason.rpt")
            {
                RequestApprovalDetailsByReasonReport();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            };
            

            if (BaseCls.GlbReportName == "Latest_Day_End_Log.rpt")
            {
                LatestDayEndLogReport();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            };

            if (BaseCls.GlbReportName == "Scheme_Creation_Dtl_Report.rpt")
            {
                SchemeCreationDetailReport();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            };
            if (BaseCls.GlbReportName == "VehicleRegDefinition.rpt")
            {
                VehicleRegistrationDefReport();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            };
            if (BaseCls.GlbReportName == "discount_report.rpt")
            {
                DiscDetailReport();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            };
            if (BaseCls.GlbReportName == "Item_Restr_Report.rpt")
            {
                ItemRestrDetailReport();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            };

            if (BaseCls.GlbReportName == "Rec_Desk_Sum_Report.rpt")
            {
                RecDeskSumReport();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            };
            if (BaseCls.GlbReportName == "InsuDef.rpt")
            {
                InsuranceDefiition();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            }
            if (BaseCls.GlbReportName == "Dep_Bank_Def_Report.rpt")
            {
                DepositBankDef();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            }
            if (BaseCls.GlbReportName == "Merchant_Id_Def_Report.rpt")
            {
                MerchantIdDef();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            }
            if (BaseCls.GlbReportName == "Vehicle_Reg_Form.rpt")
            {
                VehRegApplication();
                btnPrint.Visible = false;                
                crystalReportViewer1.ShowPrintButton = true;
            }
            if (BaseCls.GlbReportName == "Unused_doc_details_Report.rpt")
            {
                UnusedDocDef();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            }
            if (BaseCls.GlbReportName == "Reg_Unreg_Vehicle.rpt")
            {
                reg_unreg_veh();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            }
            if (BaseCls.GlbReportName == "app_curr_status_report.rpt")
            {
                appCurrStatus();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            }
            if (BaseCls.GlbReportName == "app_curr_status_user.rpt")
            {
                appCurrStatusUser();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            }
            if (BaseCls.GlbReportName == "ManualDocsRep.rpt")
            {
                crystalReportViewer1.ShowExportButton = true;
                ManualDocuments();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowPrintButton = true;
            }
            //hasith 26/01/2015
            if (BaseCls.GlbReportName == "GVDetailsReport.rpt")
            {
                GVdetails();
                btnPrint.Visible = false;
                crystalReportViewer1.ShowExportButton = true;
                crystalReportViewer1.ShowPrintButton = true;
            }

        }
       


        #region Report Functions
        private void ManualDocuments()
        {
            obj.ManualDocumentsReport();
            crystalReportViewer1.ReportSource = obj._manDoc;
            crystalReportViewer1.RefreshReport();

        }

        private void TransactionVarianceReport()
        {  // Nadeeka 02-03-2013
            obj.TransactionVarianceReport();
            crystalReportViewer1.ReportSource = obj._TransVar;
            this.Text = "Transaction Variance Report";
            crystalReportViewer1.RefreshReport();
        }
        private void RegistrationProcessReport()
        {  
            obj.RegistrationProcessReport();
            crystalReportViewer1.ReportSource = obj._regisProc;
            this.Text = "Registration Process Report";
            crystalReportViewer1.RefreshReport();
        }

        private void SalesReconcilationReport()
        {
            obj.SalesReconcilationReport();
            crystalReportViewer1.ReportSource = obj._salesRecon;
            this.Text = "Sales Reconcilation Report";
            crystalReportViewer1.RefreshReport();
        }

        private void LastNoSeqReport()
        {  // Sanjeewa 09-04-2013
            obj.LastNoSeqReport();
            crystalReportViewer1.ReportSource = obj._LastNoSeq;
            this.Text = "LAST NUMBER SEQUENCE REPORT";
            crystalReportViewer1.RefreshReport();
        }

        private void RequestApprovalDetailsByReasonReport()
        {  // kapila
            obj.ReqAppDetByReasonReport();
            crystalReportViewer1.ReportSource = obj._appcurrstusbyreason;
            this.Text = "REQUEST APPROVAL DETAILS REPORT";
            crystalReportViewer1.RefreshReport();
        }

        private void RequestApprovalDetailsReport()
        {  // Sanjeewa 16-09-2013
            obj.ReqAppDetailsReport();
            crystalReportViewer1.ReportSource = obj._ReqAppDtl;
            this.Text = "REQUEST APPROVAL DETAILS REPORT";
            crystalReportViewer1.RefreshReport();
        }

        //kapila

        private void LatestDayEndLogReport()
        {  // Sanjeewa 09-04-2013
            obj.LatestDayEndLogReport();
            crystalReportViewer1.ReportSource = obj._DayEndLog;
            this.Text = "LAST TRANSACTION DATE AND TIME LOG REPORT";
            crystalReportViewer1.RefreshReport();
        }

        private void SchemeCreationDetailReport()
        {  // Sanjeewa 09-04-2013
            obj.SchemeCreationDetailsReport();
            crystalReportViewer1.ReportSource = obj._SchemeDtl;
            this.Text = "SCHEME CREATION DETAIL REPORT";
            crystalReportViewer1.RefreshReport();
        }
        private void VehicleRegistrationDefReport()
        {  // Nadeeka  19-07-2013
            obj.VehicleRegistrationDefReport();
            crystalReportViewer1.ReportSource = obj._vehiReg;
            this.Text = "VEHICLE REGISTRATION DEFINITION";
            crystalReportViewer1.RefreshReport();
        }
        private void DiscDetailReport()
        {  // Sanjeewa  08-12-2013
            obj.DiscountDetailReport();
            crystalReportViewer1.ReportSource = obj._DiscDtl;
            this.Text = "DISCOUNT DETAILS REPORT";
            crystalReportViewer1.RefreshReport();
        }
        private void ItemRestrDetailReport()
        {  // Sanjeewa  24-06-2014
            obj.ItemRestrictionReport();
            crystalReportViewer1.ReportSource = obj._ItmRestr;
            this.Text = "RESTRICTED ITEMS";
            crystalReportViewer1.RefreshReport();
        }
        private void RecDeskSumReport()
        {  // Sanjeewa  30-12-2013
            obj.RecDeskSummReport();
            crystalReportViewer1.ReportSource = obj._RecDsk;
            this.Text = "RECIEVING DESK SUMMARY";
            crystalReportViewer1.RefreshReport();
        }
        private void InsuranceDefiition()
        {  // Prabhath  30-02-2014
            obj.InsuranceDeinition();
            crystalReportViewer1.ReportSource = obj._InsuDef;
            this.Text = "INSURANCE DEFINITION";
            crystalReportViewer1.RefreshReport();
        }

        private void DepositBankDef()
        {  // Sanjeewa  29-08-2014
            obj.DepositBankDefReport();
            crystalReportViewer1.ReportSource = obj._DepBankDef;
            this.Text = "DEPOSITE BANK DEFINITION";
            crystalReportViewer1.RefreshReport();
        }
        private void MerchantIdDef()
        {  // shanuka  12-09-2014
            obj.MerchantIdDefReport();
            crystalReportViewer1.ReportSource = obj._merchantDef;
            this.Text = "Merchant Id Definition";
            crystalReportViewer1.RefreshReport();
        }
        private void UnusedDocDef()
        {  // shanuka  25-09-2014
            obj.UnusedDefReport();
            crystalReportViewer1.ReportSource = obj._unuseddef;
            this.Text = "Unused Document Details";
            crystalReportViewer1.RefreshReport();
        }

        private void reg_unreg_veh()
        {  // Sanjeewa  27-10-2014
            obj.RegUnregVehicleDEtailsReport();
            crystalReportViewer1.ReportSource = obj._VRegUnreg;
            this.Text = "Unused Document Details";
            crystalReportViewer1.RefreshReport();
        }

        private void appCurrStatus()
        {  // Sanjeewa  24-11-2014
            obj.ReqAppCurrentStatusReport();
            crystalReportViewer1.ReportSource = obj._appcurrstus;
            this.Text = "Current Status of the Given Approvals";
            crystalReportViewer1.RefreshReport();
        }

        private void appCurrStatusUser()
        {  //
            obj.ReqAppCurrentStatusUserWise();
            crystalReportViewer1.ReportSource = obj._appcurrstususer;
            this.Text = "Current Status of the Given Approvals(USerwise)";
            crystalReportViewer1.RefreshReport();
        }

        private void VehRegApplication()
        {  // Sanjeewa 23-09-2014
            obj.VehRegAppReport();
            crystalReportViewer1.ReportSource = obj._VEhRegApp;
            this.Text = "Vehicle Registration Application";
            crystalReportViewer1.RefreshReport();
        }
        private void GVdetails()
        {  // hasith 26/01/2015
            obj.GetGVDetails();
            crystalReportViewer1.ReportSource = obj._gvDetails;
            this.Text = "Gift Voucher Details";
            crystalReportViewer1.RefreshReport();
        }
        #endregion
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
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            if (GlbReportName == "TransactionVariance.rpt")
            {

                obj._TransVar.PrintOptions.PrinterName = GetDefaultPrinter();
                     
                obj._TransVar.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
            };

            if (GlbReportName == "Last_No_Seq_Rep.rpt")
            {
                obj._LastNoSeq.PrintOptions.PrinterName = GetDefaultPrinter();
                obj._LastNoSeq.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
            };

            if (GlbReportName == "Latest_Day_End_Log.rpt")
            {
                obj._DayEndLog.PrintOptions.PrinterName = GetDefaultPrinter();
                obj._DayEndLog.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
            };

            if (GlbReportName == "Scheme_Creation_Dtl_Report.rpt")
            {
                obj._SchemeDtl.PrintOptions.PrinterName = GetDefaultPrinter();
                obj._SchemeDtl.PrintToPrinter(1, false, 0, 0);
                btnPrint.Enabled = false;
            };
        }
    }
}
