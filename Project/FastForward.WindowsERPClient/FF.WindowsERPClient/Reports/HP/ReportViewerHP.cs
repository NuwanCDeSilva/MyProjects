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
using FF.BusinessObjects;
using System.Runtime.InteropServices;
namespace FF.WindowsERPClient.Reports.HP
{
    public partial class ReportViewerHP : Base
    {
        private Cust_Acc_History Cust_Acc_HistoryReport1 = new Cust_Acc_History();
        private Cust_Acc_History_W_Red_bal Cust_Acc_HistoryReport_WRedBal = new Cust_Acc_History_W_Red_bal();
        private HPClosingBalSummaryRep HP_Closing_Balance = new HPClosingBalSummaryRep();
        private HPClosingBalChannelSummary HP_Closing_Channal_Wise = new HPClosingBalChannelSummary();
        private Age_Debtors_Arrears _Age_Debt_ArrearsReport = new Age_Debtors_Arrears();
        private Cust_Acc_His_Insu Cust_Acc_HistoryInsu = new Cust_Acc_His_Insu();
        clsHpSalesRep objHp = new clsHpSalesRep();
        private string defPrinter =String.Empty ;
        private string rmk="";

        public ReportViewerHP()
        {
            InitializeComponent();
            crystalReportViewer1.ShowGroupTreeButton = false;
        }
       

        private void ReportViewerHP_Load(object sender, EventArgs e)
        {
            try
            {
                listAllPrinters();
                lblPrinter.Text = "Default Printer- " + GetDefaultPrinter();
                btnPrint.Visible = true;
                crystalReportViewer1.ShowPrintButton = false;
                if (BaseCls.GlbReportName == "Cust_Acc_History.rpt")
                {
                    Cust_Acc_History_print();
                }
                else if (BaseCls.GlbReportName == "Cust_Acc_History_W_Red_bal.rpt")
                {
                    Cust_Acc_History_print_WithReducingBalance();
                }
                else if (BaseCls.GlbReportName == "Cust_Acc_His_Insu.rpt")   //kapila 11/4/2015
                {
                    Cust_Acc_History_Insu_Pay_print();
                }
                else if (GlbReportName == "Age_Debtors_Arrears.rpt")    //kapila
                {
                    crystalReportViewer1.ShowExportButton = true;
                    AgeAnalysisOfDebtorsArrearsPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (GlbReportName == "Age_Debtors_Arrears_Service.rpt")    //Sanjeewa
                {
                    crystalReportViewer1.ShowExportButton = true;
                    AgeAnalysisOfDebtorsArrearsScvPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (GlbReportName == "HPClosingBalSummaryRep.rpt") //darshana
                {
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    Hp_Closing_Bal_Print();
                }
                else if (GlbReportName == "HPClosingBalChannelSummary.rpt") // darshana
                {
                    //Hp_Closing_Bal_Print();
                    Hp_Closing_Bal_ChannalWise_Print();
                }

                else if (BaseCls.GlbReportName == "HP_CashFlowForecastingReport.rpt")
                {
                    HPCashFlowForecasting();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;

                }

                else if (BaseCls.GlbReportName == "ClosedAccountsDetails.rpt")
                {
                    ClosedAccountsDetails();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;

                }

                else if (BaseCls.GlbReportName == "HPReceivableDetails.rpt")
                {
                    HPReceivable_report();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }
                else if (BaseCls.GlbReportName == "trim_account_report.rpt")
                {
                    TrimAcc_report();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }
                else if (BaseCls.GlbReportName == "Collection_Bonus_Recon_Rep.rpt")
                {
                    ColBonusRecon_report();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }

                else if (BaseCls.GlbReportName == "Cust_Acknowledgement_Report.rpt")
                {
                    CustAck_report();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }
                else if (BaseCls.GlbReportName == "Cust_Acknowledgement_Report_SGL.rpt")
                {
                    CustAckSGL_report();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }

                else if (BaseCls.GlbReportName == "InsuranceFund.rpt")
                {
                    InsuranceFundRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;

                }


                else if (BaseCls.GlbReportName == "Revert_and_Release_Report.rpt")
                {
                    crystalReportViewer1.ShowPrintButton = true;
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    RvtNRlsPrint();
                }

                else if (BaseCls.GlbReportName == "RevertRelAction_Oth_Sr.rpt")
                {
                    crystalReportViewer1.ShowPrintButton = true;
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowExportButton = true;
                    RevertReleaseOther();
                }

                else if (BaseCls.GlbReportName == "No_of_Acc_Report.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    NoOfAccountsPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Grp_Sale_Report.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    GroupSalePrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "HP_Pure_Creation.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    HPPureCrePrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "add_Incentive_Scheme_Report.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    AddIncentivePrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "IntroducerCommission_Report.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    IntroducerCommissioneReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }



                else if (BaseCls.GlbReportName == "Transfered_Accounts_Report.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    TransferedAccountsPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "No_of_Act_Accounts.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    NoOfActAccountsPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;

                }
                else if (BaseCls.GlbReportName == "HPInsurance.rpt")
                {
                    InsuranceRep();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }

                else if (BaseCls.GlbReportName == "Agreement_Statement.rpt")
                {
                    AgreementStatementPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }

                else if (BaseCls.GlbReportName == "Agreement_Statement_Dtl.rpt")
                {
                    AgreementStatementDtlPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }

                else if (BaseCls.GlbReportName == "Agreement_Pending.rpt")
                {
                    AgreementPendingPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }

                else if (BaseCls.GlbReportName == "Agreement_Check_Status.rpt")
                {
                    AgreementCheckPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }

                else if (BaseCls.GlbReportName == "Agreement_Checklist.rpt")
                {
                    AgreementCheckListPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }

                else if (BaseCls.GlbReportName == "Doc_Chk_List_Report.rpt")
                {
                    DocCheckListPrint();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }
                else if (BaseCls.GlbReportName == "HPCollectionSummary.rpt")
                {
                    CollectionSummary();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;

                }
                else if (BaseCls.GlbReportName == "HPOtheCollectionSummary.rpt")
                {
                    CollectionSummaryOther();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;

                }
                else if (BaseCls.GlbReportName == "HPOtheCollectionSummaryRec.rpt")
                {
                    CollectionSummaryOtherRec();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }

                else if (BaseCls.GlbReportName == "HpCeditDebitNote.rpt")
                {
                    CreditDebitReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;

                }
                else if (BaseCls.GlbReportName == "ExcessShortOutstandingStatement.rpt")
                {
                    ExcessShortReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }
                else if (BaseCls.GlbReportName == "Total_Arrears_Report.rpt")
                {
                    TotalArrearsReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }
                else if (BaseCls.GlbReportName == "Cust_Ack_Log_Report.rpt")
                {
                    CustomerAckLogReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }

                else if (BaseCls.GlbReportName == "GR_P_Arrears_Report.rpt")
                {
                    GracePeriodArrearsReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                }

                else if (BaseCls.GlbReportName == "FinalReminder.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    FinalReminderPrint("FinalReminder");
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "AccountClose.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    FinalReminderPrint("AccountClose");
                }
                else if (BaseCls.GlbReportName == "Reminder.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    FinalReminderPrint("Reminder");
                }
                else if (BaseCls.GlbReportName == "HPArrears.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    HPArrearsPrint();
                }
                else if (BaseCls.GlbReportName == "UnusedReceipts.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    UnusedReceiptBookReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "Curr_Month_Due_Report.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    CurrentMDueReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "All_Due_Summ_Report.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    AllDueReport();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                }
                else if (BaseCls.GlbReportName == "HPMultipleAccounts.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    HPMultipleAccounts();
                }
                else if (BaseCls.GlbReportName == "HPAgreemnt_Print.rpt")
                {
                    //lblPrinter.Text = "Default Printer- " + GetDefaultPrinter();
                    crystalReportViewer1.ShowPrintButton = true;
                    btnPrint.Visible = false;
                    HPAgreementReport();
                }
                else if (BaseCls.GlbReportName == "HPAgreemnt_Print_SGL.rpt")
                {
                    //lblPrinter.Text = "Default Printer- " + GetDefaultPrinter();
                    crystalReportViewer1.ShowPrintButton = true;
                    btnPrint.Visible = false;
                    HPAgreementReport();
                }
                else if (BaseCls.GlbReportName == "HPInsuranceArrear.rpt")
                {
                    crystalReportViewer1.ShowExportButton = true;
                    crystalReportViewer1.ShowPrintButton = true;
                    btnPrint.Visible = false;
                    HP_InsArrears();
                }
                else if (BaseCls.GlbReportName == "HPReceiptPrint.rpt" || GlbReportName == "HPReceiptPrint.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    crystalReportViewer1.ShowPrintButton = false;
                    btnPrint.Visible = true;
                    HP_ReceiptPrint();
                }
                else if (BaseCls.GlbReportName == "HPReceiptPrintAdd.rpt" || GlbReportName == "HPReceiptPrintAdd.rpt")
                {
                    crystalReportViewer1.ShowExportButton = false;
                    crystalReportViewer1.ShowPrintButton = false;
                    btnPrint.Visible = true;
                    HP_ReceiptPrint_Add();
                }


                else if (BaseCls.GlbReportName == "HpInsuranceAgreement.rpt")
                {
                    crystalReportViewer1.ShowPrintButton = true;
                    btnPrint.Visible = false;
                    HP_InsuAgreementPrint();
                }
                else if (BaseCls.GlbReportName == "InsuFund.rpt")
                {
                    crystalReportViewer1.ShowPrintButton = true;
                    btnPrint.Visible = false;
                    HP_DiriyaFundPrint();
                }
                else if (BaseCls.GlbReportName == "HP_Infor.rpt")
                {
                    crystalReportViewer1.ShowPrintButton = true;
                    btnPrint.Visible = false;
                    HP_InforPrint();
                }
                else if (BaseCls.GlbReportName == "HPCollectionList.rpt")
                {
                    HPCollectionList();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;

                }
                //else if (BaseCls.GlbReportName == "Recievable_Age_Analysis.rpt")
                //{
                //    crystalReportViewer1.ShowPrintButton = true;
                //    btnPrint.Visible = false;
                //    Rec_Age_Print();
                //}
                else if (BaseCls.GlbReportName == "NotRecManIssues.rpt")
                {
                    crystalReportViewer1.ShowPrintButton = true;
                    btnPrint.Visible = false;
                    NotRecManIssuesPrint();
                }
                else if (BaseCls.GlbReportName == "InterestCalcReduceBal.rpt" || BaseCls.GlbReportName == "InterestCalcReduceBal_new.rpt")
                {
                    crystalReportViewer1.ShowPrintButton = true;
                    btnPrint.Visible = false;
                    GetReduceBalInterest();
                }
               

                else if (BaseCls.GlbReportName == "HP_Completed_Agreement.rpt")
                {
                    crystalReportViewer1.ShowPrintButton = true;
                    btnPrint.Visible = false;
                    PrintHPCompletedAgreement();
                }
                else if (BaseCls.GlbReportName == "HP_Service_Charge.rpt")
                {
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                    HPServiceCharge();
                }
                else if (BaseCls.GlbReportName == "HP_mobile_int_income.rpt" || BaseCls.GlbReportName == "HP_mobile_int_income_1.rpt")
                {
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;
                    HPMobileIntIncome();
                }
                else if (BaseCls.GlbReportName == "HP_Acc_Rescription_History.rpt")//add by tharanga 2017/11/23
                {
                    HP_Acc_Rescription_History();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;

                }
                else if (BaseCls.GlbReportName == "HP_Reject_Acc_Details.rpt")//add by Dilshan 2018/11/02
                {
                    HP_Reject_Acc_Details();
                    btnPrint.Visible = false;
                    crystalReportViewer1.ShowPrintButton = true;
                    crystalReportViewer1.ShowExportButton = true;

                }
              
                                
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void UnusedReceiptBookReport()
        {
            objHp.UnusedReceiptBookReport();
            crystalReportViewer1.ReportSource = objHp._unuRec;
            crystalReportViewer1.RefreshReport();

        }
        private void HPArrearsPrint()
        {
            objHp.HPArrearsPrint();
            
            
            crystalReportViewer1.ReportSource = objHp._hpArrears;
            crystalReportViewer1.RefreshReport();

        }

        private void Cust_Acc_History_Insu_Pay_print()
        {
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;

            DataTable param = new DataTable();
            DataRow dr;

            DataTable HPT_ACC_LOG = new DataTable();
          //  DataTable HPT_CUST = new DataTable();
            DataTable MST_PROFIT_CENTER = new DataTable();
            DataTable MST_BUSENTITY = new DataTable();
            DataTable HPT_SHED = new DataTable();
            DataTable HPT_TXN = new DataTable();
            DataTable MST_COM = new DataTable();
           DataTable SAT_ITM = new DataTable();
            DataTable HPT_CUST_ADDRESS = new DataTable();
            DataTable HPT_GUARA_ADDRESS = new DataTable();
            DataTable HPT_ACC_SUM = new DataTable();
            DataTable SAT_VEH_REG_TXN = new DataTable();
        //    DataTable CL_BAL_ARREARS = new DataTable();
            DataTable VEHICAL_INSU = new DataTable();
            DataTable INSU_PAY = new DataTable();

            HPT_ACC_LOG = CHNLSVC.MsgPortal.GetAccountLogDetails(docNo);
         //   HPT_CUST = CHNLSVC.Sales.GetHPCustomerDetails("C", BaseCls.GlbReportCustomerCode, 1, docNo);
            List<BusinessObjects.MasterProfitCenter> _pc = new List<BusinessObjects.MasterProfitCenter>();
            _pc.Add(CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf));
            MST_PROFIT_CENTER = CacheLayer.ToDataTable(_pc);
            List<BusinessObjects.MasterBusinessEntity> _cusprof = new List<BusinessObjects.MasterBusinessEntity>();
            _cusprof.Add(CHNLSVC.Sales.GetCustomerProfile(BaseCls.GlbReportCustomerCode, string.Empty, string.Empty, string.Empty, string.Empty));
            MST_BUSENTITY = CacheLayer.ToDataTable(_cusprof);
            HPT_SHED = CHNLSVC.Sales.GetAccountSchedule_Acc(docNo);
            HPT_TXN = CHNLSVC.Sales.GetMonthlyDueWithAccountSummary(docNo);
            List<BusinessObjects.MasterCompany> _com = new List<BusinessObjects.MasterCompany>();
            _com.Add(CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode));
            MST_COM = CacheLayer.ToDataTable(_com);
            //List<InvoiceItem> _invItem = BindAccountItem(docNo);
           // SAT_ITM = CacheLayer.ToDataTable(_invItem);
            SAT_ITM = CHNLSVC.Sales.GetHpItems(docNo);
            HPT_CUST_ADDRESS = CHNLSVC.Sales.GetHpGuarantor("C", docNo);
            HPT_GUARA_ADDRESS = CHNLSVC.Sales.GetHpGuarantor("G", docNo);
            HPT_ACC_SUM = CHNLSVC.Sales.GetAccountSummary(docNo);
            SAT_VEH_REG_TXN = CHNLSVC.Sales.GetVehicalRegDetails(docNo);
            VEHICAL_INSU = CHNLSVC.Sales.GetVehicalInsuranceDetails(docNo);
         //   CRED_NOTE = CHNLSVC.Sales.GetHPCreditNoteDetails(docNo);
         //   CL_BAL_ARREARS = CHNLSVC.Sales.GetArrears_ClosingBalance(docNo, DateTime.Now.Date);
            INSU_PAY = CHNLSVC.Sales.GetVehicleInsurance(docNo);

            param.Clear();
            param.Columns.Add("user", typeof(string));
            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            param.Rows.Add(dr);
            //tharanga
            ReminderLetter _ltr = new ReminderLetter();
            _ltr.Hrl_com = BaseCls.GlbUserComCode;
            string cmpnyName = "";
            DataTable oDataTable = new DataTable();
            oDataTable = CHNLSVC.General.GetCompanyByCode(_ltr.Hrl_com);




            Cust_Acc_HistoryInsu.Database.Tables["HPT_ACC_LOG"].SetDataSource(HPT_ACC_LOG);
          //  Cust_Acc_HistoryInsu.Database.Tables["HPT_CUST"].SetDataSource(HPT_CUST);
            Cust_Acc_HistoryInsu.Database.Tables["MST_PROFIT_CENTER"].SetDataSource(MST_PROFIT_CENTER);
            Cust_Acc_HistoryInsu.Database.Tables["MST_BUSENTITY"].SetDataSource(MST_BUSENTITY);
            Cust_Acc_HistoryInsu.Database.Tables["HPT_SHED"].SetDataSource(HPT_SHED);
            Cust_Acc_HistoryInsu.Database.Tables["HPT_TXN"].SetDataSource(HPT_TXN);
            Cust_Acc_HistoryInsu.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            Cust_Acc_HistoryInsu.Database.Tables["SAT_VEH_REG_TXN"].SetDataSource(SAT_VEH_REG_TXN);
          //  Cust_Acc_HistoryInsu.Database.Tables["CL_BAL_ARREARS"].SetDataSource(CL_BAL_ARREARS);
            Cust_Acc_HistoryInsu.Database.Tables["param"].SetDataSource(param);


            foreach (object repOp in Cust_Acc_HistoryInsu.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "itemDetail")
                    {
                        ReportDocument subRepDoc = Cust_Acc_HistoryInsu.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SAT_ITM"].SetDataSource(SAT_ITM);
                    }
                    if (_cs.SubreportName == "INSU PAY")
                    {
                        ReportDocument subRepDoc = Cust_Acc_HistoryInsu.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["INSU_PAY"].SetDataSource(INSU_PAY);
                    }

                }
            }

            this.Text = "Customer Account Insu Payment Print";
            crystalReportViewer1.ReportSource = Cust_Acc_HistoryInsu;
            crystalReportViewer1.RefreshReport();
            crystalReportViewer1.ShowPrintButton = true;
            btnPrint.Visible = false;

        }

        private void FinalReminderPrint(string _rept)
        {
            try
            {
                objHp.FinalReminderPrint(_rept);
                if (_rept == "FinalReminder")
                {
                    crystalReportViewer1.ReportSource = objHp._final_Reminder;
                }
                if (_rept == "AccountClose")
                {
                    crystalReportViewer1.ReportSource = objHp._accClose;
                }
                if (_rept == "Reminder")
                {
                    crystalReportViewer1.ReportSource = objHp._reminder;
                }
                crystalReportViewer1.RefreshReport();
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void HP_InsArrears()
        //Nadeeka 12-06-2013
        {
            objHp.HP_InsArrears();
            crystalReportViewer1.ReportSource = objHp._HPInsArr;
            this.Text = "HP Insurance Arrears";

            crystalReportViewer1.RefreshReport();

        }
        private void NoOfAccountsPrint()
        //Sanjeewa 14-02-2013
        {
            objHp.NoOfAccountsReport();
            crystalReportViewer1.ReportSource = objHp._NoOfAcc;
            this.Text = "No of Accounts Report";

            crystalReportViewer1.RefreshReport();
        }

        private void GroupSalePrint()
        //Sanjeewa 01-01-2014
        {
            objHp.GroupSaleReport();
            crystalReportViewer1.ReportSource = objHp._grpSale;
            this.Text = "Group Sale Report";

            crystalReportViewer1.RefreshReport();
        }

        private void HPPureCrePrint()
        //Sanjeewa 06-06-2014
        {
            objHp.HPPureCreationReport();
            crystalReportViewer1.ReportSource = objHp._HPPureCre ;
            this.Text = "NET NO OF ACCOUNTS";

            crystalReportViewer1.RefreshReport();
        }

        private void AddIncentivePrint()
        //Sanjeewa 14-03-2014
        {
            objHp.AdditionalIncentiveReport();
            crystalReportViewer1.ReportSource = objHp._addIncent;
            this.Text = "Additional Incentive Schemes Report";

            crystalReportViewer1.RefreshReport();
        }

        private void IntroducerCommissioneReport()
        // Nadeeka 25-02-2015
        {
            objHp.IntroducerCommissioneReport();
            crystalReportViewer1.ReportSource = objHp._interdComm;
            this.Text = "Introducer Commission Report";

            crystalReportViewer1.RefreshReport();
        }
        private void CurrentMDueReport()
        //Sanjeewa 17-05-2013
        {
            objHp.CurrentMonthDueReport();
            crystalReportViewer1.ReportSource = objHp._CurrMDue;
            this.Text = "CURRENT MONTH DUE REPORT";

            crystalReportViewer1.RefreshReport();

        }
        

        private void AllDueReport()
        //Sanjeewa 23-05-2013
        {
            objHp.AllDueReport();
            crystalReportViewer1.ReportSource = objHp._AllDue;
            this.Text = "ALL DUE REPORT";

            crystalReportViewer1.RefreshReport();

        }

        private void TransferedAccountsPrint()
        //Sanjeewa 25-02-2013
        {
            objHp.TransferedAccountsReport();
            crystalReportViewer1.ReportSource = objHp._TransAcc;
            this.Text = "Transfered Accounts Report";

            crystalReportViewer1.RefreshReport();

        }

        private void NoOfActAccountsPrint()
        //Sanjeewa 14-02-2013
        {
            objHp.NoOfActAccountsReport();
            crystalReportViewer1.ReportSource = objHp._NoOfActAcc;
            this.Text = "No of Active Accounts Report";

            crystalReportViewer1.RefreshReport();
        }

        private void AgreementStatementPrint()
        //Sanjeewa 19-12-2013
        {
            objHp.AgreementStatementReport();
            crystalReportViewer1.ReportSource = objHp._agreStmt;
            this.Text = "AGREEMENT STATEMENT";

            crystalReportViewer1.RefreshReport();
        }

        private void AgreementStatementDtlPrint()
        //Sanjeewa 21-12-2013
        {
            objHp.AgreementStatementDetailReport();
            crystalReportViewer1.ReportSource = objHp._agreStmtDtl;
            this.Text = "AGREEMENT STATEMENT DETAIL";

            crystalReportViewer1.RefreshReport();
        }

        private void AgreementPendingPrint()
        //Sanjeewa 22-10-2014
        {
            objHp.AgreementPendingDetailReport();
            crystalReportViewer1.ReportSource = objHp._agrepend;
            this.Text = "PENDING AGREEMENTS";

            crystalReportViewer1.RefreshReport();
        }

        private void AgreementCheckPrint()
        //Sanjeewa 20-12-2013
        {
            objHp.AgreementCheckReport();
            crystalReportViewer1.ReportSource = objHp._agreCheck;
            this.Text = "AGREEMENT CHECKING STATUS";

            crystalReportViewer1.RefreshReport();
        }

        private void AgreementCheckListPrint()
        //Sanjeewa 17-07-2015
        {
            objHp.AgreementCheckListReport();
            crystalReportViewer1.ReportSource = objHp._agrechklist;
            this.Text = "Agreement Check list Report";

            crystalReportViewer1.RefreshReport();
        }

        private void DocCheckListPrint()
        //Sanjeewa 17-07-2015
        {
            objHp.PrintDocCheckList();
            crystalReportViewer1.ReportSource = objHp._chklist;
            this.Text = "Document Check list Report";

            crystalReportViewer1.RefreshReport();
        }
        private void HPAgreementReport()
        //Sanjeewa 08-06-2013
        {
            objHp.HPAgreementPrint();
            if (BaseCls.GlbUserComCode == "SGL" | BaseCls.GlbUserComCode == "SGD")
            {
                crystalReportViewer1.ReportSource = objHp._HPAgreerptSGL;
            }
            else
            {
                crystalReportViewer1.ReportSource = objHp._HPAgreerpt;
            }
            this.Text = "HP AGREEMENT REPORT";

            crystalReportViewer1.RefreshReport();

        }

        private void RvtNRlsPrint()
        //Sanjeewa 09-02-2013
        {
            objHp.RevertNReleaseReport();
            crystalReportViewer1.ReportSource = objHp._RvtNRlssrpt;
            this.Text = "Revert and Revert Release Report";

            crystalReportViewer1.RefreshReport();

        }
        private void RevertReleaseOther()
        //Nadeeka 25-04-2014
        {
            objHp.RevertReleaseOther();
            crystalReportViewer1.ReportSource = objHp._rvtOth;
            this.Text = "Revert and Revert Release Report";

            crystalReportViewer1.RefreshReport();

        }
        private void Hp_Closing_Bal_Print()
        {
            DataTable GLOB_HP_CLS_BAL = new DataTable();
       
            DataTable tmp_user_pc = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportComp, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count >0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_HP_CLS_BAL = new DataTable();
                    TMP_HP_CLS_BAL = CHNLSVC.Sales.Process_Hp_Closing_Bal_New(BaseCls.GlbReportComp, BaseCls.GlbUserID, null, BaseCls.GlbReportScheme, BaseCls.GlbReportAsAtDate, drow["tpl_pc"].ToString(), BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel);
                    GLOB_HP_CLS_BAL.Merge(TMP_HP_CLS_BAL);
 
                    //lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                }
            }

            //GLOB_HP_CLS_BAL = CHNLSVC.Sales.Process_Hp_Closing_Bal(BaseCls.GlbReportComp, BaseCls.GlbUserID, null, null, BaseCls.GlbReportAsAtDate);

            param.Columns.Add("groupingby", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));

            dr = param.NewRow();
            dr["groupingby"] = BaseCls.GlbReportGroupBy;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            param.Rows.Add(dr);

            mst_com = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);

            HP_Closing_Balance.Database.Tables["param"].SetDataSource(param);
            HP_Closing_Balance.Database.Tables["GLOB_HP_CLS_BAL"].SetDataSource(GLOB_HP_CLS_BAL);
            HP_Closing_Balance.Database.Tables["mst_com"].SetDataSource(mst_com);

            this.Text = "HP Closing Balance";
            crystalReportViewer1.ReportSource = HP_Closing_Balance;
            crystalReportViewer1.RefreshReport();
        }

        private void Hp_Closing_Bal_ChannalWise_Print()
        {
            DataTable GLOB_HP_CLS_BAL = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataRow dr;

            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportComp, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_HP_CLS_BAL = new DataTable();
                    TMP_HP_CLS_BAL = CHNLSVC.Sales.Process_Hp_Closing_Bal_New(BaseCls.GlbReportComp, BaseCls.GlbUserID, null, BaseCls.GlbReportScheme, BaseCls.GlbReportAsAtDate, drow["tpl_pc"].ToString(), BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel);
                    GLOB_HP_CLS_BAL.Merge(TMP_HP_CLS_BAL);

                    //lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                }
            }
            //GLOB_HP_CLS_BAL = CHNLSVC.Sales.Process_Hp_Closing_Bal(BaseCls.GlbReportComp, BaseCls.GlbUserID, null, null, BaseCls.GlbReportAsAtDate);

            param.Columns.Add("groupingby", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));

            dr = param.NewRow();
            dr["groupingby"] = BaseCls.GlbReportGroupBy;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            param.Rows.Add(dr);


            mst_com = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);

            HP_Closing_Channal_Wise.Database.Tables["param"].SetDataSource(param);
            HP_Closing_Channal_Wise.Database.Tables["GLOB_HP_CLS_BAL"].SetDataSource(GLOB_HP_CLS_BAL);
            HP_Closing_Channal_Wise.Database.Tables["mst_com"].SetDataSource(mst_com);

            this.Text = "HP Closing Balance";
            crystalReportViewer1.ReportSource = HP_Closing_Channal_Wise;
            crystalReportViewer1.RefreshReport();
        }

        private void Cust_Acc_History_print()
        {
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;

            DataTable param = new DataTable();
            DataRow dr;

            DataTable HPT_ACC_LOG = new DataTable();
            DataTable HPT_CUST = new DataTable();
            DataTable MST_PROFIT_CENTER = new DataTable();
            DataTable MST_BUSENTITY = new DataTable();
            DataTable HPT_SHED = new DataTable();
            DataTable HPT_TXN = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable SAT_ITM = new DataTable();
            DataTable HPT_CUST_ADDRESS = new DataTable();
            DataTable HPT_GUARA_ADDRESS = new DataTable();
            DataTable HPT_ACC_SUM = new DataTable();
            DataTable SAT_VEH_REG_TXN = new DataTable();
            DataTable CL_BAL_ARREARS = new DataTable();
            DataTable VEHICAL_INSU = new DataTable();
            DataTable CRED_NOTE = new DataTable();

            HPT_ACC_LOG = CHNLSVC.MsgPortal.GetAccountLogDetails(docNo);
            HPT_CUST = CHNLSVC.MsgPortal.GetHPCustomerDetails("C", BaseCls.GlbReportCustomerCode, 1, BaseCls.GlbUserComCode);
            List<BusinessObjects.MasterProfitCenter> _pc = new List<BusinessObjects.MasterProfitCenter>();
            _pc.Add(CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf));
            MST_PROFIT_CENTER = CacheLayer.ToDataTable(_pc);
            List<BusinessObjects.MasterBusinessEntity> _cusprof = new List<BusinessObjects.MasterBusinessEntity>();
            //_cusprof.Add(CHNLSVC.Sales.GetCustomerProfile(BaseCls.GlbReportCustomerCode, string.Empty, string.Empty, string.Empty, string.Empty));
            _cusprof.Add(CHNLSVC.Sales.GetCustomerProfileByCom(BaseCls.GlbReportCustomerCode, string.Empty, string.Empty, string.Empty, string.Empty,BaseCls.GlbUserComCode)); //add by tharanga 2017/10/13
            MST_BUSENTITY = CacheLayer.ToDataTable(_cusprof);
            HPT_SHED = CHNLSVC.Sales.GetAccountSchedule_Acc(docNo);
            HPT_TXN = CHNLSVC.Sales.GetMonthlyDueWithAccountSummary(docNo);
            List<BusinessObjects.MasterCompany> _com = new List<BusinessObjects.MasterCompany>();
            _com.Add(CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode));
            MST_COM = CacheLayer.ToDataTable(_com);
            //List<InvoiceItem> _invItem = BindAccountItem(docNo);
            //SAT_ITM = CacheLayer.ToDataTable(_invItem);
            SAT_ITM = CHNLSVC.Sales.GetHpItems( docNo);
            HPT_CUST_ADDRESS = CHNLSVC.Sales.GetHpGuarantor("C", docNo);
            HPT_GUARA_ADDRESS = CHNLSVC.Sales.GetHpGuarantor("G", docNo);
            HPT_ACC_SUM = CHNLSVC.Sales.GetAccountSummary(docNo);
            SAT_VEH_REG_TXN = CHNLSVC.Sales.GetVehicalRegDetails(docNo);
            VEHICAL_INSU = CHNLSVC.Sales.GetVehicalInsuranceDetails(docNo);
            CRED_NOTE = CHNLSVC.MsgPortal.GetHPCreditNoteDetails(docNo);
            CL_BAL_ARREARS = CHNLSVC.MsgPortal.GetArrears_ClosingBalance(docNo, DateTime.Now.Date);

            param.Clear();
            param.Columns.Add("user", typeof(string));
            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            param.Rows.Add(dr);

            Cust_Acc_HistoryReport1.Database.Tables["HPT_ACC_LOG"].SetDataSource(HPT_ACC_LOG);
            Cust_Acc_HistoryReport1.Database.Tables["HPT_CUST"].SetDataSource(HPT_CUST);
            Cust_Acc_HistoryReport1.Database.Tables["MST_PROFIT_CENTER"].SetDataSource(MST_PROFIT_CENTER);
            Cust_Acc_HistoryReport1.Database.Tables["MST_BUSENTITY"].SetDataSource(MST_BUSENTITY);
            Cust_Acc_HistoryReport1.Database.Tables["HPT_SHED"].SetDataSource(HPT_SHED);
            Cust_Acc_HistoryReport1.Database.Tables["HPT_TXN"].SetDataSource(HPT_TXN);
            Cust_Acc_HistoryReport1.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            Cust_Acc_HistoryReport1.Database.Tables["SAT_VEH_REG_TXN"].SetDataSource(SAT_VEH_REG_TXN);
            Cust_Acc_HistoryReport1.Database.Tables["CL_BAL_ARREARS"].SetDataSource(CL_BAL_ARREARS);
            Cust_Acc_HistoryReport1.Database.Tables["param"].SetDataSource(param);


            foreach (object repOp in Cust_Acc_HistoryReport1.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "itemDetail")
                    {
                        ReportDocument subRepDoc = Cust_Acc_HistoryReport1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SAT_ITM"].SetDataSource(SAT_ITM);
                    }
                    if (_cs.SubreportName == "accSummary")
                    {
                        ReportDocument subRepDoc = Cust_Acc_HistoryReport1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["HPT_ACC_SUM"].SetDataSource(HPT_ACC_SUM);
                    }

                    if (_cs.SubreportName == "accCustomerDetail")
                    {
                        ReportDocument subRepDoc = Cust_Acc_HistoryReport1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["HPT_CUST_ADDRESS"].SetDataSource(HPT_CUST_ADDRESS);
                    }
                    if (_cs.SubreportName == "accGuarantor")
                    {
                        ReportDocument subRepDoc = Cust_Acc_HistoryReport1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["HPT_GUARA_ADDRESS"].SetDataSource(HPT_GUARA_ADDRESS);
                    }
                    if (_cs.SubreportName == "Vehical Insurance")
                    {
                        ReportDocument subRepDoc = Cust_Acc_HistoryReport1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["VEHICAL_INSU"].SetDataSource(VEHICAL_INSU);
                    }
                    //kapila 11/4/2015
                    if (_cs.SubreportName == "Credit Note")
                    {
                        ReportDocument subRepDoc = Cust_Acc_HistoryReport1.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["CRED_NOTE"].SetDataSource(CRED_NOTE);
                    }

                }
            }



            this.Text = "Customer Account History Print";
            crystalReportViewer1.ReportSource = Cust_Acc_HistoryReport1;
            crystalReportViewer1.RefreshReport();
            crystalReportViewer1.ShowPrintButton = true;
            btnPrint.Visible = false;
            //report1.Close();
            //report1.Dispose();

        }

        private void Cust_Acc_History_print_WithReducingBalance()
        {
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;

            DataTable param = new DataTable();
            DataRow dr;

            DataTable HPT_ACC_LOG = new DataTable();
            DataTable HPT_CUST = new DataTable();
            DataTable MST_PROFIT_CENTER = new DataTable();
            DataTable MST_BUSENTITY = new DataTable();
            DataTable HPT_SHED = new DataTable();
            DataTable HPT_TXN = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable SAT_ITM = new DataTable();
            DataTable HPT_CUST_ADDRESS = new DataTable();
            DataTable HPT_GUARA_ADDRESS = new DataTable();
            DataTable HPT_ACC_SUM = new DataTable();
            DataTable SAT_VEH_REG_TXN = new DataTable();
            DataTable CL_BAL_ARREARS = new DataTable();
            DataTable VEHICAL_INSU = new DataTable();
            DataTable CRED_NOTE = new DataTable();
            DataTable REDUCING_BAL = new DataTable();
            

            HPT_ACC_LOG = CHNLSVC.MsgPortal.GetAccountLogDetails(docNo);
            HPT_CUST = CHNLSVC.MsgPortal.GetHPCustomerDetails("C", BaseCls.GlbReportCustomerCode, 1, BaseCls.GlbUserComCode);
            List<BusinessObjects.MasterProfitCenter> _pc = new List<BusinessObjects.MasterProfitCenter>();
            _pc.Add(CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf));
            MST_PROFIT_CENTER = CacheLayer.ToDataTable(_pc);
            List<BusinessObjects.MasterBusinessEntity> _cusprof = new List<BusinessObjects.MasterBusinessEntity>();
            //_cusprof.Add(CHNLSVC.Sales.GetCustomerProfile(BaseCls.GlbReportCustomerCode, string.Empty, string.Empty, string.Empty, string.Empty));
            _cusprof.Add(CHNLSVC.Sales.GetCustomerProfileByCom(BaseCls.GlbReportCustomerCode, string.Empty, string.Empty, string.Empty, string.Empty, BaseCls.GlbUserComCode)); //add by tharanga 2017/10/13
            MST_BUSENTITY = CacheLayer.ToDataTable(_cusprof);
            HPT_SHED = CHNLSVC.Sales.GetAccountSchedule_Acc(docNo);
            HPT_TXN = CHNLSVC.Sales.GetMonthlyDueWithAccountSummary(docNo);
            List<BusinessObjects.MasterCompany> _com = new List<BusinessObjects.MasterCompany>();
            _com.Add(CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode));
            MST_COM = CacheLayer.ToDataTable(_com);
            //List<InvoiceItem> _invItem = BindAccountItem(docNo);
            //SAT_ITM = CacheLayer.ToDataTable(_invItem);
            SAT_ITM = CHNLSVC.Sales.GetHpItems(docNo);
            HPT_CUST_ADDRESS = CHNLSVC.Sales.GetHpGuarantor("C", docNo);
            HPT_GUARA_ADDRESS = CHNLSVC.Sales.GetHpGuarantor("G", docNo);
            HPT_ACC_SUM = CHNLSVC.Sales.GetAccountSummary(docNo);
            SAT_VEH_REG_TXN = CHNLSVC.Sales.GetVehicalRegDetails(docNo);
            VEHICAL_INSU = CHNLSVC.Sales.GetVehicalInsuranceDetails(docNo);
            CRED_NOTE = CHNLSVC.MsgPortal.GetHPCreditNoteDetails(docNo);
            CL_BAL_ARREARS = CHNLSVC.MsgPortal.GetArrears_ClosingBalance(docNo, DateTime.Now.Date);
            REDUCING_BAL = CHNLSVC.MsgPortal.Get_ReducingBalance(docNo, DateTime.Now.Date);

            param.Clear();
            param.Columns.Add("user", typeof(string));
            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            param.Rows.Add(dr);

            Cust_Acc_HistoryReport_WRedBal.Database.Tables["HPT_ACC_LOG"].SetDataSource(HPT_ACC_LOG);
            Cust_Acc_HistoryReport_WRedBal.Database.Tables["HPT_CUST"].SetDataSource(HPT_CUST);
            Cust_Acc_HistoryReport_WRedBal.Database.Tables["MST_PROFIT_CENTER"].SetDataSource(MST_PROFIT_CENTER);
            Cust_Acc_HistoryReport_WRedBal.Database.Tables["MST_BUSENTITY"].SetDataSource(MST_BUSENTITY);
            Cust_Acc_HistoryReport_WRedBal.Database.Tables["HPT_SHED"].SetDataSource(HPT_SHED);
            Cust_Acc_HistoryReport_WRedBal.Database.Tables["HPT_TXN"].SetDataSource(HPT_TXN);
            Cust_Acc_HistoryReport_WRedBal.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            Cust_Acc_HistoryReport_WRedBal.Database.Tables["SAT_VEH_REG_TXN"].SetDataSource(SAT_VEH_REG_TXN);
            Cust_Acc_HistoryReport_WRedBal.Database.Tables["CL_BAL_ARREARS"].SetDataSource(CL_BAL_ARREARS);
            Cust_Acc_HistoryReport_WRedBal.Database.Tables["REDUCING_BAL"].SetDataSource(REDUCING_BAL);
            Cust_Acc_HistoryReport_WRedBal.Database.Tables["param"].SetDataSource(param);


            foreach (object repOp in Cust_Acc_HistoryReport_WRedBal.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "itmDetails")
                    {
                        ReportDocument subRepDoc = Cust_Acc_HistoryReport_WRedBal.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SAT_ITM"].SetDataSource(SAT_ITM);
                    }
                    if (_cs.SubreportName == "reducabaldtl")
                    {
                        ReportDocument subRepDoc = Cust_Acc_HistoryReport_WRedBal.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["REDUCING_BAL"].SetDataSource(REDUCING_BAL);
                    }

                    //if (_cs.SubreportName == "accCustomerDetail")
                    //{
                    //    ReportDocument subRepDoc = Cust_Acc_HistoryReport_WRedBal.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["HPT_CUST_ADDRESS"].SetDataSource(HPT_CUST_ADDRESS);
                    //}
                    //if (_cs.SubreportName == "accGuarantor")
                    //{
                    //    ReportDocument subRepDoc = Cust_Acc_HistoryReport_WRedBal.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["HPT_GUARA_ADDRESS"].SetDataSource(HPT_GUARA_ADDRESS);
                    //}
                    //if (_cs.SubreportName == "Vehical Insurance")
                    //{
                    //    ReportDocument subRepDoc = Cust_Acc_HistoryReport_WRedBal.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["VEHICAL_INSU"].SetDataSource(VEHICAL_INSU);
                    //}
                    ////kapila 11/4/2015
                    //if (_cs.SubreportName == "Credit Note")
                    //{
                    //    ReportDocument subRepDoc = Cust_Acc_HistoryReport_WRedBal.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["CRED_NOTE"].SetDataSource(CRED_NOTE);
                    //}

                }
            }



            this.Text = "Customer Account History Print";
            crystalReportViewer1.ReportSource = Cust_Acc_HistoryReport_WRedBal;
            crystalReportViewer1.RefreshReport();
            crystalReportViewer1.ShowPrintButton = true;
            btnPrint.Visible = false;
            //report1.Close();
            //report1.Dispose();

        }


        private void HPCashFlowForecasting()
        {

            objHp.HPCashFlowForecastingReport();
            //// crystalReportViewer1.ReportSource = objHp._recCashFlow;
            // objHp._recCashFlow.ExportToDisk(ExportFormatType.Excel, "report.xls");

            //objHp._recCashFlow.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //objHp._recCashFlow.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
            //objHp._recCashFlow.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.Excel;


            //  objHp._recCashFlow.Export();
            this.Text = "HP Cas hFlow Forecasting";


            //   crystalReportViewer1.RefreshReport();


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

        [DllImport("Winspool.drv")]
        private static extern bool SetDefaultPrinter(string printerName);               
    
        string SendtoSelectedPrinter(string vReport)
        {
            PrintDialog printDlg = new PrintDialog();
            PrintDocument printDoc = new PrintDocument();
            string _printerName = string.Empty;
            string _printerNameOld = string.Empty;

            printDoc.DocumentName = vReport;
            printDlg.Document = printDoc;
            printDlg.AllowSelection = true;
            printDlg.AllowSomePages = true;

            _printerNameOld = GetDefaultPrinter();

            // Change the default printer to XPS Document Writer
            SetDefaultPrinter("Microsoft XPS Document Writer");
            if (GetDefaultPrinter() != "Microsoft XPS Document Writer")
            {
                SetDefaultPrinter("Microsoft Office Document Image Writer");
            }

            DialogResult result = printDlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (!printDoc.PrinterSettings.IsValid)
                {
                    throw new Exception("Error: cannot find the default printer.");
                }
                else
                {
                    if (printDoc.PrinterSettings.PrinterName.Contains("Image") | printDoc.PrinterSettings.PrinterName.Contains("XPS"))
                    {
                        printDoc.PrinterSettings.PrintFileName = vReport + ".XPS";
                        printDoc.PrinterSettings.PrintToFile = true;
                    }
                    
                    _printerName = printDoc.PrinterSettings.PrinterName;
                }
            }
            else
            {
                _printerName = "Exit";
            }

            SetDefaultPrinter(_printerNameOld);            

            return _printerName;
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
                if (BaseCls.GlbReportName == "Cust_Acc_History.rpt")
                {
                    Cust_Acc_HistoryReport1.PrintOptions.PrinterName = GetDefaultPrinter();
                    int traynbr = gettraynbr("Manual"); // returns 4 int
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    Cust_Acc_HistoryReport1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //  Cust_Acc_HistoryReport1.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                    Cust_Acc_HistoryReport1.PrintToPrinter(1, false, 0, 0);
                };

                if (BaseCls.GlbReportName == "HP_CashFlowForecastingReport.rpt")
                {
                    objHp._recCashFlow.PrintOptions.PrinterName = GetDefaultPrinter();

                    objHp._recCashFlow.PrintToPrinter(1, false, 0, 0);
                };
                if (BaseCls.GlbReportName == "InsuranceFund.rpt")
                {
                    objHp._recInsFund.PrintOptions.PrinterName = GetDefaultPrinter();

                    objHp._recInsFund.PrintToPrinter(1, false, 0, 0);


                };

                if (BaseCls.GlbReportName == "HPClosingBalChannelSummary.rpt")
                {
                    objHp._colSum.PrintOptions.PrinterName = GetDefaultPrinter();
                    objHp._colSum.PrintToPrinter(1, false, 0, 0);
                };

                if (BaseCls.GlbReportName == "HPInsurance.rpt")
                {
                    objHp._recIns.PrintOptions.PrinterName = GetDefaultPrinter();

                    objHp._recIns.PrintToPrinter(1, false, 0, 0);


                };

                if (BaseCls.GlbReportName == "No_of_Acc_Report.rpt")
                {
                    ////PrintPDF();
                    objHp._NoOfAcc.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    objHp._NoOfAcc.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    objHp._NoOfAcc.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                };

                if (BaseCls.GlbReportName == "Transfered_Accounts_Report.rpt")
                {
                    ////PrintPDF();
                    objHp._TransAcc.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    objHp._TransAcc.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    objHp._TransAcc.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                };

                if (BaseCls.GlbReportName == "No_of_Act_Accounts.rpt")
                {
                    ////PrintPDF();
                    objHp._NoOfActAcc.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    objHp._NoOfActAcc.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    objHp._NoOfActAcc.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                };

                if (BaseCls.GlbReportName == "Reminder.rpt")
                {
                    ////PrintPDF();
                    objHp._reminder.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    objHp._reminder.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    objHp._reminder.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                };

                if (BaseCls.GlbReportName == "FinalReminder.rpt")
                {
                    ////PrintPDF();
                    objHp._final_Reminder.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    objHp._final_Reminder.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    objHp._final_Reminder.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                };

                if (BaseCls.GlbReportName == "AccountClose.rpt")
                {
                    ////PrintPDF();
                    objHp._accClose.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    objHp._accClose.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    objHp._accClose.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                };

                if (BaseCls.GlbReportName == "HPArrears.rpt")
                {
                    ////PrintPDF();
                    objHp._hpArrears.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    objHp._hpArrears.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    objHp._hpArrears.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                };

                if (BaseCls.GlbReportName == "Revert_and_Release_Report.rpt")
                {
                    ////PrintPDF();
                    objHp._RvtNRlssrpt.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    objHp._RvtNRlssrpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    objHp._RvtNRlssrpt.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                };

                if (BaseCls.GlbReportName == "Age_Debtors_Arrears.rpt")
                {
                    objHp._Age_Debt_ArrearsReport.PrintOptions.PrinterName = GetDefaultPrinter();

                    objHp._Age_Debt_ArrearsReport.PrintToPrinter(1, false, 0, 0);


                };
                if (BaseCls.GlbReportName == "HPCollectionSummary.rpt")
                {
                    objHp._colSum.PrintOptions.PrinterName = GetDefaultPrinter();

                    objHp._colSum.PrintToPrinter(1, false, 0, 0);


                };
                if (BaseCls.GlbReportName == "HPOtheCollectionSummaryRec.rpt")
                {
                    objHp._colSumOthRec.PrintOptions.PrinterName = GetDefaultPrinter();

                    objHp._colSumOthRec.PrintToPrinter(1, false, 0, 0);


                }
                if (BaseCls.GlbReportName == "HPOtherCollectionSummary.rpt")
                {
                    objHp._colSumOth.PrintOptions.PrinterName = GetDefaultPrinter();

                    objHp._colSumOth.PrintToPrinter(1, false, 0, 0);


                }
                if (BaseCls.GlbReportName == "HpCeditDebitNote.rpt")
                {
                    objHp._debCrd.PrintOptions.PrinterName = GetDefaultPrinter();

                    objHp._debCrd.PrintToPrinter(1, false, 0, 0);


                }

                if (BaseCls.GlbReportName == "Curr_Month_Due_Report.rpt")
                {
                    objHp._CurrMDue.PrintOptions.PrinterName = GetDefaultPrinter();
                    objHp._CurrMDue.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Visible = false;
                }

                if (BaseCls.GlbReportName == "All_Due_Summ_Report.rpt")
                {
                    objHp._AllDue.PrintOptions.PrinterName = GetDefaultPrinter();
                    objHp._AllDue.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Visible = false;
                }


                if (BaseCls.GlbReportName == "HPAgreemnt_Print.rpt")
                {
                    objHp._HPAgreerpt.PrintOptions.PrinterName = SendtoSelectedPrinter(BaseCls.GlbReportName);
                    if (objHp._HPAgreerpt.PrintOptions.PrinterName!="Exit")
                    {
                    int papernbr = getprtnbr("Letter"); // returns 257 int
                                        
                    objHp._HPAgreerpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    objHp._HPAgreerpt.PrintToPrinter(1, false, 0, 0);
                    }
                    //objHp._HPAgreerpt.PrintOptions.PrinterName = GetDefaultPrinter();
                    //objHp._HPAgreerpt.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Visible = false;
                }

                if (BaseCls.GlbReportName == "ExcessShortOutstandingStatement.rpt")
                {
                    objHp._exShort.PrintOptions.PrinterName = GetDefaultPrinter();

                    objHp._exShort.PrintToPrinter(1, false, 0, 0);


                }
                if (BaseCls.GlbReportName == "HPReceiptPrint.rpt" || GlbReportName == "HPReceiptPrint.rpt")
                {
                    //PrintDialog printDialog1 = new PrintDialog();
                    //printDialog1.PrinterSettings.Copies =1;
                    //printDialog1.AllowSomePages = true;
                    //printDialog1.AllowSelection = false;
                    //printDialog1.AllowCurrentPage = false;
                    //if (printDialog1.ShowDialog() == DialogResult.OK)
                    //{
                    //    objHp._HPRec.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName; ;
                    //    int papernbr = getprtnbr("DO"); // returns 257 int
                    //    objHp._HPRec.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    //    objHp._HPRec.PrintToPrinter(1, false, 0, 0);
                    //    btnPrint.Enabled = false;
                    //}

                    MessageBox.Show("HP Receipt Printing...."  , "HP Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    objHp._HPRec.PrintOptions.PrinterName = GetDefaultPrinter() ;
                 
                     int papernbr = getprtnbr("Letter"); // returns 257 int
                    objHp._HPRec.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    objHp._HPRec.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                    objHp._HPRec.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                    myPrinters.SetDefaultPrinter(defPrinter);
                    lblPrinter.Text = "Default Printer- " + GetDefaultPrinter();
                

                };


                if (BaseCls.GlbReportName == "HPReceiptPrintAdd.rpt" || GlbReportName == "HPReceiptPrintAdd.rpt")
                {
               
                    MessageBox.Show("HP Receipt Printing....", "HP Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    objHp._HPRecAdd.PrintOptions.PrinterName = GetDefaultPrinter();

                    int papernbr = getprtnbr("Letter"); // returns 257 int
                    objHp._HPRecAdd.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    objHp._HPRecAdd.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                    objHp._HPRecAdd.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;
                    myPrinters.SetDefaultPrinter(defPrinter);
                    lblPrinter.Text = "Default Printer- " + GetDefaultPrinter();


                };

                if (BaseCls.GlbReportName == "HPInfor.rpt")
                {
                    objHp._hpInfor.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("DO"); // returns 257 int
                    objHp._hpInfor.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    objHp._hpInfor.PrintToPrinter(1, false, 0, 0);
                    btnPrint.Enabled = false;

                };
                if (BaseCls.GlbReportName == "NotRecManIssues.rpt")
                {
                    objHp._NotRecManIss.PrintOptions.PrinterName = GetDefaultPrinter();
                    int papernbr = getprtnbr("Letter"); // returns 257 int

                    objHp._NotRecManIss.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                    objHp._NotRecManIss.PrintToPrinter(1, false, 0, 0);
                    //btnPrint.Enabled = false;

                };

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
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

        private void InsuranceFundRep()
        {
            objHp.HPInsuranceFundReport();
            crystalReportViewer1.ReportSource = objHp._recInsFund;
            crystalReportViewer1.RefreshReport();
        }

        private void HPReceivable_report()
        {
            objHp.HPReceivable_report();
            crystalReportViewer1.ReportSource = objHp._recevDet;
            crystalReportViewer1.RefreshReport();
        }

        private void TrimAcc_report()
        {
            objHp.TrimAccountsReport();
            crystalReportViewer1.ReportSource = objHp._trim_acc;
            crystalReportViewer1.RefreshReport();
        }

        private void ColBonusRecon_report()
        {
            objHp.CollectionBonusReconReport();
            crystalReportViewer1.ReportSource = objHp._colbonrecon;
            crystalReportViewer1.RefreshReport();
        }

        private void CustAck_report()
        {
            objHp.CustomerAcknowledgementPrintCrReport();
            crystalReportViewer1.ReportSource = objHp._ack_print;
            crystalReportViewer1.RefreshReport();
        }

        private void GetReduceBalInterest()
        {
            objHp.GetReduceBalInterest();
            if (BaseCls.GlbReportName == "InterestCalcReduceBal_new.rpt")
            {
                crystalReportViewer1.ReportSource = objHp._intredbalnew;
                crystalReportViewer1.RefreshReport();
            }
            if (BaseCls.GlbReportName == "InterestCalcReduceBal.rpt")
            {
                crystalReportViewer1.ReportSource = objHp._intredbal;
                crystalReportViewer1.RefreshReport();
            }
            

            //crystalReportViewer1.ReportSource = objHp._intredbal;
            //crystalReportViewer1.RefreshReport();
        }
        private void PrintHPCompletedAgreement()
        {
            objHp.PrintHPCompletedAgreement();
            crystalReportViewer1.ReportSource = objHp._complAgr;
            crystalReportViewer1.RefreshReport();
        }
        private void HPServiceCharge()
        {
            objHp.HPServiceCharge();
            crystalReportViewer1.ReportSource = objHp._hpSerChrge;
            crystalReportViewer1.RefreshReport();
        }
        private void HPMobileIntIncome()
        {
            objHp.HPMobileIntIncome();
            if( BaseCls.GlbReportName == "HP_mobile_int_income.rpt")
            crystalReportViewer1.ReportSource = objHp._hpMobIntInc;
            else
                crystalReportViewer1.ReportSource = objHp._hpMobIntInc_1;
            crystalReportViewer1.RefreshReport();
        }
        private void CustAckSGL_report()
        {
            objHp.CustomerAcknowledgementPrintCrReport();
            crystalReportViewer1.ReportSource = objHp._ackSGL_print;
            crystalReportViewer1.RefreshReport();
        }

        private void ClosedAccountsDetails()
        {

            objHp.ClosedAccountsDetails();
            crystalReportViewer1.ReportSource = objHp._clsAcc;
            crystalReportViewer1.RefreshReport();
        }

        private void InsuranceRep()
        {

            objHp.HPInsuranceReport();
            crystalReportViewer1.ReportSource = objHp._recIns;
            crystalReportViewer1.RefreshReport();
        }
        private void CollectionSummary()
        {

            objHp.HPCollectionSummary();
            crystalReportViewer1.ReportSource = objHp._colSum;
            crystalReportViewer1.RefreshReport();
        }
        private void HPCollectionList()
        {

            objHp.HPReceiptList();
            crystalReportViewer1.ReportSource = objHp._hpRecList;
            crystalReportViewer1.RefreshReport();
        }

        private void CollectionSummaryOtherRec()
        {

            objHp.CollectionSummaryOther();
            crystalReportViewer1.ReportSource = objHp._colSumOthRec;
            crystalReportViewer1.RefreshReport();
        }
        private void CollectionSummaryOther()
        {

            objHp.CollectionSummaryOther();
            crystalReportViewer1.ReportSource = objHp._colSumOth;
            crystalReportViewer1.RefreshReport();
        }
        private void AgeAnalysisOfDebtorsArrearsPrint()
        {
            objHp.AgeAnalysisOfDebtorsArrearsPrint();
            crystalReportViewer1.ReportSource = objHp._Age_Debt_ArrearsReport;
            crystalReportViewer1.RefreshReport();
        }
        private void AgeAnalysisOfDebtorsArrearsScvPrint()
        {            
            crystalReportViewer1.ReportSource = objHp._Age_Debt_ArrearsScvReport;
            crystalReportViewer1.RefreshReport();          
        }
        private void CreditDebitReport()
        {
            objHp.HPCReditDebitReport();
            crystalReportViewer1.ReportSource = objHp._debCrd;
            crystalReportViewer1.RefreshReport();
        }
        private void ExcessShortReport()
        {
            objHp.HPExcessShortReport();
            crystalReportViewer1.ReportSource = objHp._exShort;
            crystalReportViewer1.RefreshReport();
        }

        private void TotalArrearsReport()
        {
            objHp.TotalArrearsReport();
            crystalReportViewer1.ReportSource = objHp._TotArrears;
            crystalReportViewer1.RefreshReport();
        }

        private void CustomerAckLogReport()
        {
            objHp.CustomerAcknowledgementLogReport();
            crystalReportViewer1.ReportSource = objHp._ackLog;
            crystalReportViewer1.RefreshReport();
        }

        private void GracePeriodArrearsReport()
        {
            objHp.GracePeriodArrearsReport();
            crystalReportViewer1.ReportSource = objHp._GrpArrears;
            crystalReportViewer1.RefreshReport();
        }
        private void HPMultipleAccounts()
        {
            objHp.HPMultipleAccounts();
            crystalReportViewer1.ReportSource = objHp._mulAcc;
            crystalReportViewer1.RefreshReport();
        }
        private void HP_ReceiptPrint()
        {
            objHp.HP_ReceiptPrint();
            crystalReportViewer1.ReportSource = objHp._HPRec;
            crystalReportViewer1.RefreshReport();
        }
        private void HP_ReceiptPrint_Add()
        {
            objHp.HP_ReceiptPrint_Add();
            crystalReportViewer1.ReportSource = objHp._HPRecAdd;
            crystalReportViewer1.RefreshReport();
        }
        private void HP_InsuAgreementPrint()
        {
            objHp.HP_InsuAgreementPrint();
            crystalReportViewer1.ReportSource = objHp._HPInsAgree;
            crystalReportViewer1.RefreshReport();
        }
        private void HP_DiriyaFundPrint()
        {
            objHp.HPDiriyaFundPrint();
            crystalReportViewer1.ReportSource = objHp._insuFund;
            crystalReportViewer1.RefreshReport();
        }
        private void HP_InforPrint()
        {
            objHp.HPInforPrint();
            crystalReportViewer1.ReportSource = objHp._hpInfor;
            crystalReportViewer1.RefreshReport();
        }

        private void NotRecManIssuesPrint()
        {
            objHp.NotRecManIssuesPrint();
            crystalReportViewer1.ReportSource = objHp._NotRecManIss;
            crystalReportViewer1.RefreshReport();
        }

        //private void Rec_Age_Print()
        //{
        //    objHp.RecievableAgeAnalysisReport();
        //    crystalReportViewer1.ReportSource = objHp._RecAge;
        //    crystalReportViewer1.RefreshReport();
        //}

        private void listAllPrinters()
        {
            foreach (var item in PrinterSettings.InstalledPrinters)
            {
                this.listBox1.Items.Add(item.ToString());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            defPrinter = GetDefaultPrinter();
            string pname = this.listBox1.SelectedItem.ToString();
            myPrinters.SetDefaultPrinter(pname);
            lblPrinter.Text = "Default Printer- " + GetDefaultPrinter();
        }
        public static class myPrinters
        {
            [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool SetDefaultPrinter(string Name);

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
        private void HP_Acc_Rescription_History()
        {

            objHp.HP_Acc_Rescription_History();
            crystalReportViewer1.ReportSource = objHp._HP_Acc_Rescription_History;
            crystalReportViewer1.RefreshReport();
        }
        private void HP_Reject_Acc_Details()
        {

            objHp.HP_Reject_Acc_Details();
            crystalReportViewer1.ReportSource = objHp._HP_Reject_Acc_Details;
            crystalReportViewer1.RefreshReport();
        }
        
    }
}
