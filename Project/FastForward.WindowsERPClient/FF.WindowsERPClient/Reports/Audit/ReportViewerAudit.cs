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

namespace FF.WindowsERPClient.Reports.Audit
{
    public partial class ReportViewerAudit : Base
    {
        clsAuditRep obj = new clsAuditRep();

        public ReportViewerAudit()
        {
            InitializeComponent();

        }

        private void ReportViewerAudit_Load(object sender, EventArgs e)
        {
            try
            {
                lblPrinter.Text = "Default Printer- " + GetDefaultPrinter();

                if (BaseCls.GlbReportName == "Audit_Phy_Stock_Bal_Coll.rpt")
                {
                    PhyStkBalCollectionRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Audit_Phy_Stock_Verification.rpt")
                {
                    PhyStkVerificationRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Audit_Phy_Stock_Verification_AST.rpt")
                {
                    PhyStkVerificationASTRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Audit_Phy_Stock_Verification_ByRef.rpt")
                {
                    PhyStkVerificationByRefRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Audit_Manager_Explanation.rpt")
                {
                    ManagerExplanationRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Audit_Phy_Stock_Verification_DefItems.rpt")
                {
                    PhyStkVerificationDefRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Audit_Mismatch_Serials.rpt")
                {
                    SerialMisMatchRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Audit_Used_as_FixedAsset.rpt")
                {
                    UsedFixedAssetRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Audit_Ageing_Items.rpt")
                {
                    PhyStkVerificationAgeRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Audit_Unconfirmed_AOD.rpt")
                {
                    UnconfirmedAODRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Audit_FIFO_not_Followed.rpt")
                {
                    PhyStkFIFORep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Audit_Fixed_Asset_Bal.rpt")
                {
                    FixedAssetBalRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Audit_Stock_Varience_Note.rpt")
                {
                    StkVarienceNoteRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "RevertedItems.rpt")
                {
                    RevertedItemsRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "PendingDelivery.rpt")
                {
                    PendingDeliveryRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                if (BaseCls.GlbReportName == "MultipleAccounts.rpt")
                {
                    MultipleAccReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                if (BaseCls.GlbReportName == "Arrears.rpt")
                {
                    ArrearsReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                if (BaseCls.GlbReportName == "AuditDeliveredSalesReport.rpt")
                {
                    DeliveredSalesReportAudit();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                if (BaseCls.GlbReportName == "User_Prev_Menu.rpt")
                {
                    UserPrevilegeReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                if (BaseCls.GlbReportName == "User_Role_Prev.rpt")
                {
                    RolePrevilegeReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                if (BaseCls.GlbReportName == "User_Spec_Perm.rpt")
                {
                    SpecialPrevilegeReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                if (BaseCls.GlbReportName == "User_list.rpt")
                {
                    UserListReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                if (BaseCls.GlbReportName == "Audit_Stock_Verification_Exec_Sum.rpt")
                {
                    ExecutiveSumReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }

                if (BaseCls.GlbReportName == "Audit_Stock_Verification_stk_sign.rpt")
                {
                    StockSignReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                //if (BaseCls.GlbReportName == "Executive_Summary.rpt")
                //{
                //    //Lakshika 2016/10/20
                //    StockSummaryReport();
                //    btnPrint.Visible = false;
                //    crystalReportViewer1.ShowPrintButton = true;
                //    crystalReportViewer1.ShowExportButton = true;
                //};

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void RevertedItemsRep()
        {  // kapila
            obj.RevertedItemsReport();
            crystalReportViewer1.ReportSource = obj._revertedItm;
            this.Text = "REVERTED ITEMS";
            crystalReportViewer1.RefreshReport();
        }

        private void PendingDeliveryRep()
        {  // kapila
            obj.PendingDeliveryReport();
            crystalReportViewer1.ReportSource = obj._pendingDel;
            this.Text = "PENDING DELIVERY";
            crystalReportViewer1.RefreshReport();
        }
        private void MultipleAccReport()
        {  // kapila
            obj.MultipleAccountsRep();
            crystalReportViewer1.ReportSource = obj._multiAcc;
            this.Text = "MULTIPLE ACCOUNTS";
            crystalReportViewer1.RefreshReport();
        }
        private void ArrearsReport()
        {  // kapila
            obj.AgeAnalysisOfDebtorsArrearsPrint();
            crystalReportViewer1.ReportSource = obj._Age_Debt_ArrearsReport;
            this.Text = "ARREARS";
            crystalReportViewer1.RefreshReport();
        }
        private void PhyStkBalCollectionRep()
        {  // Sanjeewa 25-10-2013
            obj.PhyStkBalCollectionReport();
            crystalReportViewer1.ReportSource = obj._PhyStkColl;
            this.Text = "PHYSICAL STOCK BALANCE COLLECTION SHEET";
            crystalReportViewer1.RefreshReport();
        }
        
        private void PhyStkVerificationASTRep()
        {  // Sanjeewa 26-10-2013
            obj.PhyStkVerificationCommStkReport();
            crystalReportViewer1.ReportSource = obj._PhyStkVerifycmstk;
            this.Text = "PHYSICAL VERIFICATION OF STOCK";
            crystalReportViewer1.RefreshReport();
        }

        private void PhyStkVerificationRep()
        {  // Sanjeewa 26-10-2013
            obj.PhyStkVerificationReport();
            crystalReportViewer1.ReportSource = obj._PhyStkVerify;
            this.Text = "PHYSICAL VERIFICATION OF STOCK";
            crystalReportViewer1.RefreshReport();
        }
        private void PhyStkVerificationByRefRep()
        {  // Sanjeewa 28-10-2013
            obj.PhyStkVerificationByRefReport();
            crystalReportViewer1.ReportSource = obj._PhyStkbyRef;
            this.Text = "PHYSICAL VERIFICATION OF STOCK (BY REFERENCE STATUS";
            crystalReportViewer1.RefreshReport();
        }

        private void ManagerExplanationRep()
        {  // Sanjeewa 28-10-2013
            obj.ManagerExplanationReport();
            crystalReportViewer1.ReportSource = obj._PhyMgrExpl;
            this.Text = "EXPLANATION OF SHOWROOM MANAGER (BY REFERENCE STATUS)";
            crystalReportViewer1.RefreshReport();
        }

        private void PhyStkVerificationDefRep()
        {  // Sanjeewa 28-10-2013
            obj.PhyStkVerificationDefReport();
            crystalReportViewer1.ReportSource = obj._PhyStkDef;
            this.Text = "PHYSICAL VARIFICATION OF DAMAGE/ DEFECTIVE ITEMS (BY REFERENCE STATUS)";
            crystalReportViewer1.RefreshReport();
        }

        private void SerialMisMatchRep()
        {  // Sanjeewa 28-10-2013
            obj.SerialMismatchReport();
            crystalReportViewer1.ReportSource = obj._SerMismatch;
            this.Text = "MISMATCH OF SERIAL NUMBERS";
            crystalReportViewer1.RefreshReport();
        }

        private void DeliveredSalesReportAudit()
        {  // Nadeeka 08-05-2014
            obj.DeliveredSalesReportAudit();
            crystalReportViewer1.ReportSource = obj._delSalesAud;
            this.Text = "DELIVERED SALES";
            crystalReportViewer1.RefreshReport();
        }

        private void UserPrevilegeReport()
        {  // Sanjeewa 18-06-2014
            obj.UserMenuPrivilegesReport();
            crystalReportViewer1.ReportSource = obj._UPermMnu;
            this.Text = "USER PRIVILEGES FOR MENU FUNCTIONS";
            crystalReportViewer1.RefreshReport();
        }

        private void UserListReport()
        {  // Wimal 02/03/2017
            obj.UserListReport();
            crystalReportViewer1.ReportSource = obj._UList;
            this.Text = "USER LIST";
            crystalReportViewer1.RefreshReport();
        }

        private void RolePrevilegeReport()
        {  // Sanjeewa 19-06-2014
            obj.RoleMenuPrivilegesReport();
            crystalReportViewer1.ReportSource = obj._URoleMnu;
            this.Text = "USER ROLE PRIVILEGES FOR MENU FUNCTIONS";
            crystalReportViewer1.RefreshReport();
        }

        private void ExecutiveSumReport()
        {  // Sanjeewa 17-03-2017
            obj.ExecutiveSummaryReport();
            crystalReportViewer1.ReportSource = obj._executiveSummary;
            this.Text = "EXECUTIVE SUMMARY";
            crystalReportViewer1.RefreshReport();
        }

        private void StockSignReport()
        {  // Sanjeewa 18-03-2017
            obj.StockSignatureReport();
            crystalReportViewer1.ReportSource = obj._stksign;
            this.Text = "STOCK SIGNATURE REPORT";
            crystalReportViewer1.RefreshReport();
        }

        private void SpecialPrevilegeReport()
        {  // Sanjeewa 19-06-2014
            obj.SpecialPrivilegesReport();
            crystalReportViewer1.ReportSource = obj._USpcPerm;
            this.Text = "USER SPECIAL PERMISSION DETAILS";
            crystalReportViewer1.RefreshReport();
        }

        private void UsedFixedAssetRep()
        {  // Sanjeewa 29-10-2013
            obj.UsedFixedAssetReport();
            crystalReportViewer1.ReportSource = obj._FixAsst;
            if (BaseCls.GlbReportStrStatus == "UFA")
            {
                this.Text = "USED AS FIXED ASSET";
            }
            else if (BaseCls.GlbReportStrStatus == "POSM")
            {
                this.Text = "POS MATERIAL";
            }
            else if (BaseCls.GlbReportStrStatus == "IND")
            {
                this.Text = "ITEM NOT DISPLAYED";
            }
            crystalReportViewer1.RefreshReport();
        }

        private void PhyStkVerificationAgeRep()
        {  // Sanjeewa 29-10-2013
            obj.PhyStkBalAgeingReport();
            crystalReportViewer1.ReportSource = obj._AgeItm;
            this.Text = "AGEING ITEMS";
            crystalReportViewer1.RefreshReport();
        }

        private void PhyStkFIFORep()
        {  // Sanjeewa 29-10-2013
            obj.PhyStkBalFIFOReport();
            crystalReportViewer1.ReportSource = obj._FIFO;
            this.Text = "FIFO NOT FOLLOWED";
            crystalReportViewer1.RefreshReport();
        }

        private void UnconfirmedAODRep()
        {  // Sanjeewa 30-10-2013
            obj.UnconfirmedAODReport();
            crystalReportViewer1.ReportSource = obj._GIT;
            this.Text = "AOD OUTSTANDING (INWARDS/OUTWARDS)";
            crystalReportViewer1.RefreshReport();
        }

        private void FixedAssetBalRep()
        {  // Sanjeewa 30-10-2013
            obj.FixedAssetBalReport();
            crystalReportViewer1.ReportSource = obj._FixBal;
            this.Text = "CURRENT FIXED ASSETS BALANCE";
            crystalReportViewer1.RefreshReport();
        }

        private void StkVarienceNoteRep()
        {  // Sanjeewa 06-11-2013
            obj.PhyStkVarienceNoteReport();
            crystalReportViewer1.ReportSource = obj._StkVarNote;
            this.Text = "STOCK VARIENCES NOTE";
            crystalReportViewer1.RefreshReport();
        }

        //private void StockSummaryReport()
        //{  // Lakshika 2016/10/20
        //    obj.StockSummaryReport();
        //    crystalReportViewer1.ReportSource = obj._executiveSummary;
        //    this.Text = "STOCK SUMMARY REPORT";
        //    crystalReportViewer1.RefreshReport();
        //}

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            this.crystalReportViewer1.Dispose();
            this.crystalReportViewer1 = null;

            GC.Collect();
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


    }

}
