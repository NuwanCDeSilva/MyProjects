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
namespace FF.WindowsERPClient.Reports.Finance
{
    public partial class ReportViewerFinance : Base
    {
        // private Inward_Docs inwardReport1 = new Inward_Docs();


        clsFinanceRep obj = new clsFinanceRep();

        public ReportViewerFinance()
        {
            InitializeComponent();
        }

        private void PrintIntroCommDefReport()
        {
            obj.PrintIntroCommDefReport();
            crystalReportViewer1.ReportSource = obj._introComm;
            this.Text = "Introducer Commission Report";
            crystalReportViewer1.RefreshReport();
        }

        private void ReportViewerFinance_Load(object sender, EventArgs e)
        {
            try
            {
                lblPrinter.Text = "Default Printer- " + GetDefaultPrinter();

                if (BaseCls.GlbReportName == "IntroComm.rpt")
                {
                    PrintIntroCommDefReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "PersonalChequeStatement.rpt")
                {
                    PersonalChequeStatementRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "AD_Loan_Settlement.rpt")
                {
                    ADLoanSettRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "ESD_Dtl_Report.rpt")
                {
                    ESDSettRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "Cr_Balance_Report.rpt")
                {
                    CrBalanceRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Physical_Cash_Verify_Report.rpt")
                {
                    PhysicalCashVerifyRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "HP_GRP_Comm_Target_Report.rpt")
                {
                    HPGroupCommRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Claim_Expenses_Voucher.rpt")
                {
                    ClaimExpVoucherRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                //dilshan on 21/08/2018***********
                if (BaseCls.GlbReportName == "ManagerIncome.rpt")
                {
                    ManagerIncomRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                //********************************
                if (BaseCls.GlbReportName == "short_rem_statement.rpt")
                {
                    ShortRemStatementPrint();
                };

                if (BaseCls.GlbReportName == "Excess_Rem_Statement.rpt")
                {
                    ExcessRemStatementPrint();
                };


                if (BaseCls.GlbReportName == "Doc_Check_List.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    ManualDocumentCheckList();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "CashControl.rpt")
                {
                    CashControl();
                };
                if (BaseCls.GlbReportName == "RemitanceCheckList.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    RemitCheckList();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "SignOff.rpt" || BaseCls.GlbReportName == "Cashierwisesales.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    SignOff();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "Elite_Comm_Def.rpt")
                {
                    Elite_CommDefReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                };

                if (BaseCls.GlbReportName == "Elite_Comm_Summary_R1.rpt")
                {
                    EliteCommSumR1Report();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                };

                if (BaseCls.GlbReportName == "Elite_Comm_Summary_R1D.rpt")
                {
                    EliteCommSumR1DReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                };

                if (BaseCls.GlbReportName == "Elite_Comm_Summary_R2.rpt")
                {
                    EliteCommSumR2Report();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "AdvanceReceiptReg.rpt")
                {
                    AdvanceRecRegi();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "Incentive_Def.rpt")
                {
                    IncentiveSchemeDef();
                };
                if (BaseCls.GlbReportName == "OverShortStatement.rpt")
                {
                    Over_Short_State();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                //dilshan on 07-09-2018
                if (BaseCls.GlbReportName == "OverShortDetail.rpt")
                {
                    Over_Short_Details();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "OverShortSum.rpt")
                {
                    Over_Short_Sum();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "OverShortMovement.rpt")
                {
                    Over_Short_Movement();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "CashCommDef.rpt")
                {
                    Cash_comm_def();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "DailyExpences.rpt")
                {
                    DailyExpences();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "DeliveredSales.rpt")
                {
                    DeliveredSales();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "ProductBonus.rpt" || BaseCls.GlbReportName == "ProductBonusApp.rpt")
                {
                    ProductBonus();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "ProductBonusInv.rpt")
                {
                    ProductBonusInvs();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "ProductBonusInvInc.rpt")
                {
                    ProductBonusInvsInc();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "ProdBonusInvIncDet.rpt")
                {
                    ProductBonusInvsIncDet();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "Prod_Bonus_Voucher.rpt")
                {
                    ProductBonusVoucher();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "rcv_desk_rem_sum_rep.rpt")
                {
                    RvcDeskRemSum();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "Short_Sett_Report.rpt")
                {
                    ShortSettRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                };
                if (BaseCls.GlbReportName == "ESD_recon_Report.rpt")
                {
                    ESDReconRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "AdvRecRecon.rpt" || BaseCls.GlbReportName == "AdvRecReconSum.rpt")
                {
                    AdvanceReceiptRecon();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "rcv_dsk_processed_Report.rpt")
                {
                    RcvDeskProcessed();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Bank_Statement_Report.rpt")
                {

                    LoadBankStatement();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Not_Realized_Transactions.rpt")
                {
                    NotRealizedTransactionProcessed();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };

                if (BaseCls.GlbReportName == "Extra_add_Docs.rpt")
                {
                    PrintExtraAddBankDocs();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "RealizationFinalizeStatus.rpt")
                {
                    RealizationFinalizeReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "crcd_recon_report.rpt")
                {
                    crcd_recon_report();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "BankReconciliation.rpt")
                {
                    BankReconciliationReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "BankReconciliationSummery.rpt") // Added by Chathura on 20-oct-2017
                {
                    BankReconciliationSummeryReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "RealizationStatus.rpt") // Added by Chathura on 20-oct-2017
                {
                    RealizationStatusReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "RemitanceControlRecon.rpt") // Added by Chathura on 20-oct-2017
                {
                    RemitanceControlReconReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "RemitanceControlReconSummery.rpt") // Added by Chathura on 20-oct-2017
                {
                    RemitanceControlReconSummeryReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "bankaccounttransfferingreport.rpt") // Added by tharanga 2018/08/18
                {
                    RankAccountTransfferingReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "BankReconciliationnew.rpt")
                {
                    BankReconciliationReportnew();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
                if (BaseCls.GlbReportName == "CreditCardsReconcilation.rpt")
                {
                    CreditCardReconciliationReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                };
              

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

        private void AdvanceRecRegi()
        {
            obj.AdvanceReceiptRegistry();
            crystalReportViewer1.ReportSource = obj._advRecReg;
            this.Text = "ADVANCE RECEIPT REGISTRY REPORT";
            crystalReportViewer1.RefreshReport();
        }
        private void Over_Short_State()
        {
            obj.OverandShortStatement();
            crystalReportViewer1.ReportSource = obj._overShort;
            this.Text = "OVER AND SHORT STATEMENT";
            crystalReportViewer1.RefreshReport();
        }
        private void Over_Short_Details()
        {
            obj.OverandShortDetail();
            crystalReportViewer1.ReportSource = obj._overShortdet;
            this.Text = "OVER AND SHORT DETAIL";
            crystalReportViewer1.RefreshReport();
        }
        private void Over_Short_Sum()
        {
            obj.OverandShortSum();
            crystalReportViewer1.ReportSource = obj._overShortsum;
            this.Text = "OVER AND SHORT SUMMERY";
            crystalReportViewer1.RefreshReport();
        }
        private void Over_Short_Movement()
        {
            obj.OverandShortMovement();
            crystalReportViewer1.ReportSource = obj._overShortmov;
            this.Text = "OVER AND SHORT MOVEMENT";
            crystalReportViewer1.RefreshReport();
        }
        private void Cash_comm_def()
        {
            obj.CashCommissionDef();
            crystalReportViewer1.ReportSource = obj._cashComDef;
            this.Text = "CASH COMMISSION DEFINITION";
            crystalReportViewer1.RefreshReport();
        }
        private void EliteCommSumR1Report()
        {  // Sanjeewa 11-07-2013
            obj.EliteCommR1Report();
            crystalReportViewer1.ReportSource = obj._EComm_R1;
            this.Text = "SALES COMMISSION SUMMARY REPORT";
            crystalReportViewer1.RefreshReport();
        }
        private void EliteCommSumR1DReport()
        {  // Sanjeewa 11-10-2013
            obj.EliteCommR1DReport();
            crystalReportViewer1.ReportSource = obj._EComm_R1D;
            this.Text = "SALES COMMISSION DETAIL REPORT";
            crystalReportViewer1.RefreshReport();
        }

        private void Elite_CommDefReport()
        {  // Sanjeewa 15-07-2013
            obj.EliteCommDefReport();
            crystalReportViewer1.ReportSource = obj._EComm_Def;
            this.Text = "SALES COMMISSION DEFINITION REPORT";
            crystalReportViewer1.RefreshReport();
        }

        private void EliteCommSumR2Report()
        {  // Sanjeewa 11-07-2013
            obj.EliteCommR2Report();
            crystalReportViewer1.ReportSource = obj._EComm_R2;
            this.Text = "SALES COMMISSION SUMMARY (WITH ALLOWANCES) REPORT";
            crystalReportViewer1.RefreshReport();
        }

        private void SignOff()
        {
            obj.SignOff();
            this.Text = "Sign Off";
            if (BaseCls.GlbReportName == "SignOff.rpt")
            {
                crystalReportViewer1.ReportSource = obj._signoff;
            }
            else
            {
                crystalReportViewer1.ReportSource = obj._cashiersales;
            }
            crystalReportViewer1.RefreshReport();
        }

        private void RemitCheckList()
        {
            //kapila 19/4/2013
            obj.RemitanceCheckList();
            this.Text = "Remittance CHeck List";
            crystalReportViewer1.ReportSource = obj._remitCheckList;
            crystalReportViewer1.RefreshReport();
        }

        private void CashControl()
        {
            //kapila 19/4/2013
            obj.CashControl();
            this.Text = "Cash Control Account";
            crystalReportViewer1.ReportSource = obj._cashControl;
            crystalReportViewer1.RefreshReport();
        }

        private void ManualDocumentCheckList()
        {
            //kapila 19/4/2013
            obj.ManualDocumentCheckList();
            this.Text = "Manual Document Check List";
            crystalReportViewer1.ReportSource = obj._docCheckList;
            crystalReportViewer1.RefreshReport();
        }

        private void ShortRemStatementPrint()
        {
            //kapila 9/4/2013
            obj.ShortRemStatementPrint();
            this.Text = "Short Remittance Statement";
            crystalReportViewer1.ReportSource = obj._ShortRem;
            crystalReportViewer1.RefreshReport();
        }

        private void ExcessRemStatementPrint()
        {
            //Nadeeka 31/1/2014 
            obj.ExcessRemStatementPrint();
            this.Text = "Excess Remittance Statement";
            crystalReportViewer1.ReportSource = obj._ExcessRem;
            crystalReportViewer1.RefreshReport();
        }

        private void PersonalChequeStatementRep()
        {  // Nadeeka 26-02-2013
            int rowc = 0;
            rowc = obj.PersonalChequeStatementRep();
            if (rowc == 1)
            {
                crystalReportViewer1.ReportSource = obj._chqSts;
                this.Text = "Personal Cheque Statement";
                crystalReportViewer1.RefreshReport();
            }
            else
            {

                MessageBox.Show("No Data for selected criteria", "Personal settlements Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void DailyExpences()
        {  // Nadeeka 26-02-2013
            obj.DailyExpences();
            crystalReportViewer1.ReportSource = obj._dailyExp;
            this.Text = "Daily Expences";
            crystalReportViewer1.RefreshReport();
        }

        private void DeliveredSales()
        {
            obj.DeliveredSalesForComm();
            crystalReportViewer1.ReportSource = obj._delSales;
            this.Text = "Delivered Sales";
            crystalReportViewer1.RefreshReport();
        }

        private void ProductBonus()
        {
            obj.ProductBonus();
            this.Text = "Product Bonus";
            if (BaseCls.GlbReportName == "ProductBonus.rpt")
            {
                crystalReportViewer1.ReportSource = obj._prodBon;
            }
            else
            {
                crystalReportViewer1.ReportSource = obj._prodBonApp;
            }
            crystalReportViewer1.RefreshReport();
        }
        private void ProductBonusInvs()
        {
            obj.ProductBonusInvoices();
            this.Text = "Product Bonus";
            crystalReportViewer1.ReportSource = obj._prodBonInv;
            crystalReportViewer1.RefreshReport();
        }
        private void ProductBonusInvsInc()
        {
            obj.ProductBonusInvoicesInc();
            this.Text = "Product Bonus";
            crystalReportViewer1.ReportSource = obj._prodBonInvInc;
            crystalReportViewer1.RefreshReport();
        }

        private void ProductBonusInvsIncDet()
        {
            obj.ProductBonusInvoicesInc();
            this.Text = "Product Bonus";
            crystalReportViewer1.ReportSource = obj._prodBonInvIncDet;
            crystalReportViewer1.RefreshReport();
        }

        private void ProductBonusVoucher()
        {
            obj.ProductBonusVoucher();
            this.Text = "Product Bonus Voucher";
            crystalReportViewer1.ReportSource = obj._prodBonVou;
            crystalReportViewer1.RefreshReport();
        }

        private void RvcDeskRemSum()
        {
            obj.RecievingDeskRemitanceSummary();
            this.Text = "Recieving Desk Remitance Summary";
            crystalReportViewer1.ReportSource = obj._rcv_Sum;
            crystalReportViewer1.RefreshReport();
        }

        private void ShortSettRep()
        {
            obj.ShortSettlementReport();
            this.Text = "SHORT SETTLEMENT REPORT";
            crystalReportViewer1.ReportSource = obj._shortsett;
            crystalReportViewer1.RefreshReport();
        }

        private void IncentiveSchemeDef()
        {
            obj.IncenticeSchemeDef();
            crystalReportViewer1.ReportSource = obj._incentiveDef;
            this.Text = "Incentive Definition";
            crystalReportViewer1.RefreshReport();
        }

        private void ADLoanSettRep()
        {  // Nadeeka 26-02-2013
            obj.ADLoanSettReport();
            crystalReportViewer1.ReportSource = obj._adLoanSett;
            this.Text = "AD Loan Settlement Report";
            crystalReportViewer1.RefreshReport();
        }

        private void ESDSettRep()
        {  // Sanejewa 14-06-2014
            obj.ESDStatement();
            crystalReportViewer1.ReportSource = obj._ESDStmt;
            this.Text = "ESD STATEMENT";
            crystalReportViewer1.RefreshReport();
        }
        private void ESDReconRep()
        {  // kapila 16/6/2014
            obj.ESDRecon();
            crystalReportViewer1.ReportSource = obj._ESDRecon;
            this.Text = "ESD RECONCILATION";
            crystalReportViewer1.RefreshReport();
        }
        private void AdvanceReceiptRecon()
        {  // kapila 
            obj.AdvanceReceiptRecon();
            if (BaseCls.GlbReportName == "AdvRecRecon.rpt")
                crystalReportViewer1.ReportSource = obj._advRecRecon;
            else
                crystalReportViewer1.ReportSource = obj._advRecReconSum;
            this.Text = "ADVANCE REFUND RECONCILATION";
            crystalReportViewer1.RefreshReport();
        }
        private void RcvDeskProcessed()
        {  // kapila 
            obj.RcvDeskProcessedReport();
            crystalReportViewer1.ReportSource = obj._remsunprcs;
            this.Text = "SCAN PHYSICAL NOT PROCESSED REPORT";
            crystalReportViewer1.RefreshReport();
        }
        private void CrBalanceRep()
        {  // Sanjeewa 07-11-2013
            obj.CrBalanceDReport();
            crystalReportViewer1.ReportSource = obj._CrBal;
            this.Text = "AD Loan Settlement Report";
            crystalReportViewer1.RefreshReport();
        }

        private void PhysicalCashVerifyRep()
        {  // Sanjeewa 16-10-2013
            obj.PhysicalVerificationCashReport();
            crystalReportViewer1.ReportSource = obj._physicalCash;
            this.Text = "Physical Verification of Cash Report";
            crystalReportViewer1.RefreshReport();
        }

        private void HPGroupCommRep()
        {  // Sanjeewa 23-10-2013
            obj.HPGroupCommissionDReport();
            crystalReportViewer1.ReportSource = obj._HPGRPComm;
            this.Text = "HP Group sales Commission for Acheived Targets";
            crystalReportViewer1.RefreshReport();
        }

        private void ClaimExpVoucherRep()
        {  // Sanjeewa 08-04-2013
            obj.ClaimExpVoucherReport();
            crystalReportViewer1.ReportSource = obj._claimExpVou;
            this.Text = "Claim Expenses Voucher Statement";
            crystalReportViewer1.RefreshReport();
        }
        private void ManagerIncomRep()
        {  // Dilshan 21/08/2018
            obj.ManagerIncomeReport();
            crystalReportViewer1.ReportSource = obj._managerIncome;
            this.Text = "Manager Income Statement";
            crystalReportViewer1.RefreshReport();
        }
        private void NotRealizedTransactionProcessed()
        {
            obj.NotRealizedTransactionReport();
            crystalReportViewer1.ReportSource = obj._NOT_RLZ_TRNZ;
            this.Text = "NOT REALIZED TRANSACTION REPORT";
            crystalReportViewer1.RefreshReport();
        }
        private void PrintExtraAddBankDocs()
        {
            obj.PrintExtraAddBankDocs();
            crystalReportViewer1.ReportSource = obj._extraAddDocs;
            this.Text = "EXTRA ADD DOCS REPORT";
            crystalReportViewer1.RefreshReport();
        }
        private void LoadBankStatement()
        {
            obj.LoadBankStatement();
            crystalReportViewer1.ReportSource = obj._bnkste;
            this.Text = "Bank Statement";
            crystalReportViewer1.RefreshReport();
        }
        private void RealizationFinalizeReport()
        {
            obj.RealizationFinalizeReport();
            crystalReportViewer1.ReportSource = obj._RLZ_FINZ;
            this.Text = "REALIZATIONS FINALIZE STATUS REPORT";
            crystalReportViewer1.RefreshReport();
        }
        private void crcd_recon_report()
        {
            obj.CrcdReconciliationReport();
            crystalReportViewer1.ReportSource = obj._crcdrecon;
            this.Text = "Credit Card Reconciliation Report";
            crystalReportViewer1.RefreshReport();
        }
        private void BankReconciliationReport()
        {
            obj.BankReconciliationReport();
            crystalReportViewer1.ReportSource = obj._BNK_RECON;
            this.Text = "BANK RECONCILIATION REPORT";
            crystalReportViewer1.RefreshReport();
        }
        private void BankReconciliationSummeryReport() // Added by Chathura on 20-oct-2017
        {
            obj.BankReconciliationSummeryReport();
            crystalReportViewer1.ReportSource = obj._BNK_RECON_SMRY;
            this.Text = "BANK RECONCILIATION REPORT";
            crystalReportViewer1.RefreshReport();
        }
        private void RealizationStatusReport() // Added by Chathura on 20-oct-2017
        {
            obj.RealizationStatusReport();
            crystalReportViewer1.ReportSource = obj._BNK_RLZ_ST;
            this.Text = "REALIZATION STATUS REPORT";
            crystalReportViewer1.RefreshReport();
        }
        private void RemitanceControlReconReport() // Added by Chathura on 20-oct-2017
        {
            obj.RemitanceControlReconReport();
            crystalReportViewer1.ReportSource = obj._REM_CON;
            this.Text = "REMITANCE CONTROL RECONSILIATION DETAIL REPORT";
            crystalReportViewer1.RefreshReport();
        }
        private void RemitanceControlReconSummeryReport() // Added by Chathura on 20-oct-2017
        {
            obj.RemitanceControlReconSummeryReport();
            crystalReportViewer1.ReportSource = obj._REM_CON_SMRY;
            this.Text = "REMITANCE CONTROL RECONSILIATION SUMMERY REPORT"; 
            crystalReportViewer1.RefreshReport();
        }
        private void BankReconciliationReportnew()
        {
            obj.BankReconciliationReport();
            crystalReportViewer1.ReportSource = obj._BNK_RECONnew;
            this.Text = "BANK RECONCILIATION REPORT";
            crystalReportViewer1.RefreshReport();
        }

        private void CreditCardReconciliationReport()
        {
            obj.CreditCardReconciliationReport();
            crystalReportViewer1.ReportSource = obj._creditcard_recon;
            this.Text = "CREDIT CARDS RECONCILIATION REPORT";
            crystalReportViewer1.RefreshReport();
        }

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
                if (BaseCls.GlbReportName == "PersonalChequeStatement.rpt")
                {

                    obj._chqSts.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._chqSts.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._chqSts.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj._chqSts.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                };

                if (BaseCls.GlbReportName == "AD_Loan_Settlement.rpt")
                {
                    obj._adLoanSett.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._adLoanSett.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._adLoanSett.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj._adLoanSett.PrintToPrinter(1, false, 0, 0);
                };

                if (BaseCls.GlbReportName == "short_rem_statement.rpt")
                {
                    obj._ShortRem.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._ShortRem.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._ShortRem.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj._ShortRem.PrintToPrinter(1, false, 0, 0);
                };

                if (BaseCls.GlbReportName == "Claim_Expenses_Voucher.rpt")
                {
                    obj._claimExpVou.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._claimExpVou.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._claimExpVou.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj._claimExpVou.PrintToPrinter(1, false, 0, 0);
                };

                if (BaseCls.GlbReportName == "Doc_Check_List.rpt")
                {
                    obj._docCheckList.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._docCheckList.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._docCheckList.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj._docCheckList.PrintToPrinter(1, false, 0, 0);
                };
                if (BaseCls.GlbReportName == "CashControl.rpt")
                {
                    obj._cashControl.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._cashControl.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._cashControl.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj._cashControl.PrintToPrinter(1, false, 0, 0);
                };
                if (BaseCls.GlbReportName == "RemitanceCheckList.rpt")
                {
                    obj._remitCheckList.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._remitCheckList.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._remitCheckList.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj._remitCheckList.PrintToPrinter(1, false, 0, 0);
                };
                if (BaseCls.GlbReportName == "SignOff.rpt")
                {
                    obj._signoff.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._signoff.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._signoff.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj._signoff.PrintToPrinter(1, false, 0, 0);
                };
                if (BaseCls.GlbReportName == "Cashierwisesales.rpt")
                {
                    obj._cashiersales.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._cashiersales.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._cashiersales.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj._cashiersales.PrintToPrinter(1, false, 0, 0);
                };
                if (BaseCls.GlbReportName == "AdvanceReceiptReg.rpt")
                {
                    obj._advRecReg.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    obj._advRecReg.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    obj._advRecReg.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    obj._advRecReg.PrintToPrinter(1, false, 0, 0);
                };
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
                GC.Collect();
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

        private void Print()
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
        }
        private void RankAccountTransfferingReport() // Added by Chathura on 20-oct-2017
        {
            obj.RankAccountTransfferingReport();
            crystalReportViewer1.ReportSource = obj._bankaccounttransfferingreport;
            this.Text = "Rank Account Transffering Report";
            crystalReportViewer1.RefreshReport();
        }
    }
}
