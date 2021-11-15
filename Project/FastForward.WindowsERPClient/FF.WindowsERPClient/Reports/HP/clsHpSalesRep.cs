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
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace FF.WindowsERPClient.Reports.HP
{
    class clsHpSalesRep
    {
        public FF.WindowsERPClient.Reports.HP.HP_CashFlowForecastingSummaryReport _recCashFlowSum = new FF.WindowsERPClient.Reports.HP.HP_CashFlowForecastingSummaryReport();
        public FF.WindowsERPClient.Reports.HP.HP_CashFlowForecastingReport _recCashFlow = new FF.WindowsERPClient.Reports.HP.HP_CashFlowForecastingReport();
        public FF.WindowsERPClient.Reports.HP.InsuranceFund _recInsFund = new FF.WindowsERPClient.Reports.HP.InsuranceFund();
        public FF.WindowsERPClient.Reports.HP.HPInsurance _recIns = new FF.WindowsERPClient.Reports.HP.HPInsurance();
        public FF.WindowsERPClient.Reports.HP.No_of_Acc_Report _NoOfAcc = new FF.WindowsERPClient.Reports.HP.No_of_Acc_Report();
        public FF.WindowsERPClient.Reports.HP.Revert_and_Release_Report _RvtNRlssrpt = new FF.WindowsERPClient.Reports.HP.Revert_and_Release_Report();
        public FF.WindowsERPClient.Reports.HP.HPCollectionSummary _colSum = new FF.WindowsERPClient.Reports.HP.HPCollectionSummary();
        public FF.WindowsERPClient.Reports.HP.Age_Debtors_Arrears _Age_Debt_ArrearsReport = new FF.WindowsERPClient.Reports.HP.Age_Debtors_Arrears();
        public FF.WindowsERPClient.Reports.HP.Age_Debtors_Arrears_Service _Age_Debt_ArrearsScvReport = new FF.WindowsERPClient.Reports.HP.Age_Debtors_Arrears_Service();
        public FF.WindowsERPClient.Reports.HP.HPOtheCollectionSummary _colSumOth = new FF.WindowsERPClient.Reports.HP.HPOtheCollectionSummary();
        public FF.WindowsERPClient.Reports.HP.HPOtheCollectionSummaryRec _colSumOthRec = new FF.WindowsERPClient.Reports.HP.HPOtheCollectionSummaryRec();
        public FF.WindowsERPClient.Reports.HP.No_of_Act_Accounts _NoOfActAcc = new FF.WindowsERPClient.Reports.HP.No_of_Act_Accounts();
        public FF.WindowsERPClient.Reports.HP.HpCeditDebitNote _debCrd = new FF.WindowsERPClient.Reports.HP.HpCeditDebitNote();
        public FF.WindowsERPClient.Reports.HP.ExcessShortOutstandingStatement _exShort = new FF.WindowsERPClient.Reports.HP.ExcessShortOutstandingStatement();
        public FF.WindowsERPClient.Reports.HP.Transfered_Accounts_Report _TransAcc = new FF.WindowsERPClient.Reports.HP.Transfered_Accounts_Report();
        public FF.WindowsERPClient.Reports.HP.Curr_Month_Due_Report _CurrMDue = new FF.WindowsERPClient.Reports.HP.Curr_Month_Due_Report();
        public FF.WindowsERPClient.Reports.HP.All_Due_Summ_Report _AllDue = new FF.WindowsERPClient.Reports.HP.All_Due_Summ_Report();

        public FF.WindowsERPClient.Reports.HP.Reminder _reminder = new FF.WindowsERPClient.Reports.HP.Reminder();
        public FF.WindowsERPClient.Reports.HP.FinalReminder _final_Reminder = new FF.WindowsERPClient.Reports.HP.FinalReminder();
        public FF.WindowsERPClient.Reports.HP.AccountClose _accClose = new FF.WindowsERPClient.Reports.HP.AccountClose();
        public FF.WindowsERPClient.Reports.HP.HPArrears _hpArrears = new FF.WindowsERPClient.Reports.HP.HPArrears();
        public FF.WindowsERPClient.Reports.HP.ClosedAccountsDetails _clsAcc = new FF.WindowsERPClient.Reports.HP.ClosedAccountsDetails();
        public FF.WindowsERPClient.Reports.HP.UnusedReceipts _unuRec = new FF.WindowsERPClient.Reports.HP.UnusedReceipts();
        public FF.WindowsERPClient.Reports.HP.HPMultipleAccounts _mulAcc = new FF.WindowsERPClient.Reports.HP.HPMultipleAccounts();
        public FF.WindowsERPClient.Reports.HP.HPAgreemnt_Print _HPAgreerpt = new FF.WindowsERPClient.Reports.HP.HPAgreemnt_Print();
        public FF.WindowsERPClient.Reports.HP.HPAgreemnt_Print_SGL _HPAgreerptSGL = new FF.WindowsERPClient.Reports.HP.HPAgreemnt_Print_SGL();
        public FF.WindowsERPClient.Reports.HP.HPInsuranceArrear _HPInsArr = new FF.WindowsERPClient.Reports.HP.HPInsuranceArrear();
        public FF.WindowsERPClient.Reports.HP.HPReceiptPrint _HPRec = new FF.WindowsERPClient.Reports.HP.HPReceiptPrint();
        public FF.WindowsERPClient.Reports.HP.HPReceiptPrint _HPRecAdd = new FF.WindowsERPClient.Reports.HP.HPReceiptPrint();
        public FF.WindowsERPClient.Reports.HP.HpInsuranceAgreement _HPInsAgree = new FF.WindowsERPClient.Reports.HP.HpInsuranceAgreement();
        public FF.WindowsERPClient.Reports.HP.Total_Arrears_Report _TotArrears = new FF.WindowsERPClient.Reports.HP.Total_Arrears_Report();
        public FF.WindowsERPClient.Reports.HP.GR_P_Arrears_Report _GrpArrears = new FF.WindowsERPClient.Reports.HP.GR_P_Arrears_Report();
        public FF.WindowsERPClient.Reports.HP.Recievable_Age_Analysis _RecAge = new FF.WindowsERPClient.Reports.HP.Recievable_Age_Analysis();
        public FF.WindowsERPClient.Reports.HP.InsuFund _insuFund = new FF.WindowsERPClient.Reports.HP.InsuFund();
        public FF.WindowsERPClient.Reports.HP.HP_Infor _hpInfor = new FF.WindowsERPClient.Reports.HP.HP_Infor();
        public FF.WindowsERPClient.Reports.HP.HPCollectionList _hpRecList = new FF.WindowsERPClient.Reports.HP.HPCollectionList();
        public FF.WindowsERPClient.Reports.HP.Agreement_Statement _agreStmt = new FF.WindowsERPClient.Reports.HP.Agreement_Statement();
        public FF.WindowsERPClient.Reports.HP.Agreement_Check_Status _agreCheck = new FF.WindowsERPClient.Reports.HP.Agreement_Check_Status();
        public FF.WindowsERPClient.Reports.HP.Agreement_Statement_Dtl _agreStmtDtl = new FF.WindowsERPClient.Reports.HP.Agreement_Statement_Dtl();
        public FF.WindowsERPClient.Reports.HP.Agreement_Checklist _agrechklist = new FF.WindowsERPClient.Reports.HP.Agreement_Checklist();
        public FF.WindowsERPClient.Reports.HP.Agreement_Pending _agrepend = new FF.WindowsERPClient.Reports.HP.Agreement_Pending();
        public FF.WindowsERPClient.Reports.HP.HPReceivableDetails _recevDet = new FF.WindowsERPClient.Reports.HP.HPReceivableDetails();
        public FF.WindowsERPClient.Reports.HP.Grp_Sale_Report _grpSale = new FF.WindowsERPClient.Reports.HP.Grp_Sale_Report();
        public FF.WindowsERPClient.Reports.HP.add_Incentive_Scheme_Report _addIncent = new FF.WindowsERPClient.Reports.HP.add_Incentive_Scheme_Report();
        public FF.WindowsERPClient.Reports.HP.trim_account_report _trim_acc = new FF.WindowsERPClient.Reports.HP.trim_account_report();
        public FF.WindowsERPClient.Reports.HP.Cust_Ack_Log_Report _ackLog = new FF.WindowsERPClient.Reports.HP.Cust_Ack_Log_Report();
        public FF.WindowsERPClient.Reports.HP.Cust_Acknowledgement_Report _ack_print = new FF.WindowsERPClient.Reports.HP.Cust_Acknowledgement_Report();
        public FF.WindowsERPClient.Reports.HP.Cust_Acknowledgement_Report_SGL _ackSGL_print = new FF.WindowsERPClient.Reports.HP.Cust_Acknowledgement_Report_SGL();
        public FF.WindowsERPClient.Reports.HP.RevertRelAction_Oth_Sr _rvtOth = new FF.WindowsERPClient.Reports.HP.RevertRelAction_Oth_Sr();
        public FF.WindowsERPClient.Reports.HP.HP_Pure_Creation _HPPureCre = new FF.WindowsERPClient.Reports.HP.HP_Pure_Creation();
        public FF.WindowsERPClient.Reports.HP.NotRecManIssues _NotRecManIss = new FF.WindowsERPClient.Reports.HP.NotRecManIssues();
        public FF.WindowsERPClient.Reports.HP.InterestCalcReduceBal _intredbal = new FF.WindowsERPClient.Reports.HP.InterestCalcReduceBal();
        public FF.WindowsERPClient.Reports.HP.Collection_Bonus_Recon_Rep _colbonrecon = new FF.WindowsERPClient.Reports.HP.Collection_Bonus_Recon_Rep();
        public FF.WindowsERPClient.Reports.HP.IntroducerCommission_Report _interdComm = new FF.WindowsERPClient.Reports.HP.IntroducerCommission_Report();
        public FF.WindowsERPClient.Reports.HP.HP_Completed_Agreement _complAgr = new FF.WindowsERPClient.Reports.HP.HP_Completed_Agreement();
        public FF.WindowsERPClient.Reports.HP.Doc_Chk_List_Report _chklist = new FF.WindowsERPClient.Reports.HP.Doc_Chk_List_Report();
        public FF.WindowsERPClient.Reports.HP.HP_Service_Charge _hpSerChrge = new FF.WindowsERPClient.Reports.HP.HP_Service_Charge();
        public FF.WindowsERPClient.Reports.HP.HP_mobile_int_income _hpMobIntInc = new FF.WindowsERPClient.Reports.HP.HP_mobile_int_income();
        public FF.WindowsERPClient.Reports.HP.HP_mobile_int_income_1 _hpMobIntInc_1 = new FF.WindowsERPClient.Reports.HP.HP_mobile_int_income_1();
        public FF.WindowsERPClient.Reports.HP.HP_Acc_Rescription_History _HP_Acc_Rescription_History = new FF.WindowsERPClient.Reports.HP.HP_Acc_Rescription_History();
        public FF.WindowsERPClient.Reports.HP.InterestCalcReduceBal_new _intredbalnew = new FF.WindowsERPClient.Reports.HP.InterestCalcReduceBal_new();
        public FF.WindowsERPClient.Reports.HP.HP_Reject_Acc_Details _HP_Reject_Acc_Details = new FF.WindowsERPClient.Reports.HP.HP_Reject_Acc_Details();
        
        
        DataTable tmp_user_pc = new DataTable();
        DataTable GLOB_DataTable = new DataTable();
        DataTable _PRINTDoc = new DataTable();

        Base bsObj;

        //Insurance Direct Print DataTable
        DataTable salesDetails = new DataTable();
        DataTable sat_hdr = new DataTable();
        DataTable sat_itm = new DataTable();
        DataTable mst_profit_center = new DataTable();
        DataTable mst_item = new DataTable();
        DataTable mst_com = new DataTable();
        DataTable sec_user = new DataTable();
        DataTable mst_busentity = new DataTable();
        DataTable hpt_cust = new DataTable();
        DataTable hpt_insu = new DataTable();
        DataTable PRINT_DOC = new DataTable();

        public clsHpSalesRep()
        {
            bsObj = new Base();

        }

        public void HPMobileIntIncome()
        {// kapila
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.HPMobileIntIncomeReport(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportBrand, BaseCls.GlbReportDocType, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
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
            param.Columns.Add("itemCode", typeof(string));
            param.Columns.Add("itemCat1", typeof(string));
            param.Columns.Add("itemCat2", typeof(string));
            param.Columns.Add("itemCat3", typeof(string));
            param.Columns.Add("itemModel", typeof(string));
            param.Columns.Add("itemBrand", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportDocType;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["itemCode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["itemCat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["itemCat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["itemCat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["itemModel"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["itemBrand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            param.Rows.Add(dr);

            if (BaseCls.GlbReportName == "HP_mobile_int_income.rpt")
            {
                _hpMobIntInc.Database.Tables["INT_INCOME"].SetDataSource(GLOB_DataTable);
                _hpMobIntInc.Database.Tables["param"].SetDataSource(param);
            }
            else
            {
                _hpMobIntInc_1.Database.Tables["INT_INCOME"].SetDataSource(GLOB_DataTable);
                _hpMobIntInc_1.Database.Tables["param"].SetDataSource(param);
            }
            

        }
        public void HPServiceCharge()
        {// kapila
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.PrintHPServiceCharge(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportBrand, BaseCls.GlbReportModel);
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
            param.Columns.Add("itemCode", typeof(string));
            param.Columns.Add("itemCat1", typeof(string));
            param.Columns.Add("itemCat2", typeof(string));
            param.Columns.Add("itemCat3", typeof(string));
            param.Columns.Add("itemModel", typeof(string));
            param.Columns.Add("itemBrand", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["itemCode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["itemCat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["itemCat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["itemCat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["itemModel"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["itemBrand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            param.Rows.Add(dr);

            _hpSerChrge.Database.Tables["SER_CHARGE"].SetDataSource(GLOB_DataTable);
            _hpSerChrge.Database.Tables["param"].SetDataSource(param);

        }
        public void PrintHPCompletedAgreement()
        {// kapila
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.PrintCompletedHPAgreement(BaseCls.GlbUserComCode, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, drow["tpl_pc"].ToString(), BaseCls.GlbReportUser, BaseCls.GlbUserID);
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

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            param.Rows.Add(dr);

            _complAgr.Database.Tables["HP_COMP_AGR"].SetDataSource(GLOB_DataTable);
            _complAgr.Database.Tables["param"].SetDataSource(param);

        }

        public void PrintDocCheckList()
        {// Sanjeewa
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.PrintCompletedHPAgreement(BaseCls.GlbUserComCode, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, drow["tpl_pc"].ToString(), BaseCls.GlbReportUser, BaseCls.GlbUserID);
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

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            param.Rows.Add(dr);

            _chklist.Database.Tables["DOC_CHK_LIST"].SetDataSource(GLOB_DataTable);
            _chklist.Database.Tables["param"].SetDataSource(param);

        }

        public void CustomerAcknowledgementPrintCrReport()
        {
            DataTable _PRINT = bsObj.CHNLSVC.Sales.GetCustAcknowledgeDetails(BaseCls.GlbUserID);

            if (BaseCls.GlbUserComCode == "SGL" || BaseCls.GlbUserComCode == "SGD")
                _ackSGL_print.Database.Tables["CUST_ACK"].SetDataSource(_PRINT);
            else
                _ack_print.Database.Tables["CUST_ACK"].SetDataSource(_PRINT);
        }

        public void CustomerAcknowledgementPrintReport()
        {// Sanjeewa 06-03-2014
            PrintDocument pdoc = null;
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Tahoma", 8);

            pd.Document = pdoc;

            DialogResult result = pd.ShowDialog();
            if (result == DialogResult.OK)
            {
                DataTable _PRINT = bsObj.CHNLSVC.Sales.GetCustAcknowledgeDetails(BaseCls.GlbUserID);

                if (_PRINT.Rows.Count > 0)
                {
                    foreach (DataRow drow in _PRINT.Rows)
                    {
                        _PRINTDoc = _PRINT.Select("ack_accno = '" + drow["ack_accno"].ToString() + "'").CopyToDataTable();

                        pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
                        pdoc.Print();
                    }
                }

            }

        }

        public void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Tahoma", 10);
            float fontHeight = font.GetHeight();
            int startX = 330;
            int startY = 0;
            int Offset = 0;
            int OffsetX = 0;

            if (_PRINTDoc.Rows.Count > 0)
            {
                foreach (DataRow drowcat in _PRINTDoc.Rows)
                {
                    graphics.DrawString(drowcat["ack_cust_name"].ToString(), new Font("Tahoma", 10),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);

                    if (drowcat["ack_item_desc"].ToString().Length > 60)
                    {
                        startY = startY + 30;
                        graphics.DrawString(drowcat["ack_item_desc"].ToString().Substring(0, 60), new Font("Tahoma", 9),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);

                        if (drowcat["ack_item_desc"].ToString().Length > 120)
                        {
                            startY = startY + 15;
                            graphics.DrawString(drowcat["ack_item_desc"].ToString().Substring(61, 60), new Font("Tahoma", 9),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);

                            startY = startY + 15;
                            graphics.DrawString(drowcat["ack_item_desc"].ToString().Substring(121, drowcat["ack_item_desc"].ToString().Length - 121), new Font("Tahoma", 9),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);
                        }
                        else
                        {
                            startY = startY + 15;
                            graphics.DrawString(drowcat["ack_item_desc"].ToString().Substring(61, drowcat["ack_item_desc"].ToString().Length - 61), new Font("Tahoma", 9),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);
                        }

                        startY = startY + 30;
                    }
                    else
                    {
                        startY = startY + 45;
                        graphics.DrawString(drowcat["ack_item_desc"].ToString().Substring(0, drowcat["ack_item_desc"].ToString().Length), new Font("Tahoma", 10),
                                      new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);

                        startY = startY + 45;
                    }

                    graphics.DrawString("Rs. ", new Font("Tahoma", 10),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);
                    graphics.DrawString(Convert.ToDecimal(drowcat["ack_hire_val"].ToString()).ToString("#,#00.00"), new Font("Tahoma", 10),
                                    new SolidBrush(Color.Black), startX + OffsetX + 25, startY + Offset);
                    startY = startY + 45;

                    graphics.DrawString("Rs. ", new Font("Tahoma", 10),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);
                    graphics.DrawString(Convert.ToDecimal(drowcat["ack_diriya_val"].ToString()).ToString("#,#00.00"), new Font("Tahoma", 10),
                                    new SolidBrush(Color.Black), startX + OffsetX + 25, startY + Offset);
                    startY = startY + 45;

                    graphics.DrawString(drowcat["ack_accno"].ToString(), new Font("Tahoma", 10),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);
                    startY = startY + 50;

                    graphics.DrawString(Convert.ToDateTime(drowcat["ack_create_dt"].ToString()).Date.ToString("dd/MMM/yyyy"), new Font("Tahoma", 10),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);
                    startY = startY + 45;

                    graphics.DrawString(drowcat["ack_loc_desc"].ToString(), new Font("Tahoma", 10),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);
                    startY = startY + 35;

                    graphics.DrawString(drowcat["ack_guar1_name"].ToString(), new Font("Tahoma", 10),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);
                    startY = startY + 30;

                    graphics.DrawString(drowcat["ack_guar2_name"].ToString(), new Font("Tahoma", 10),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);


                    //Address
                    startY = startY + 580;
                    if (drowcat["ack_print_add_tp"].ToString() == "C")
                    {
                        graphics.DrawString(drowcat["ack_cust_name"].ToString(), new Font("Tahoma", 10),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);
                        startY = startY + 20;

                        graphics.DrawString(drowcat["ack_cust_add1"].ToString(), new Font("Tahoma", 10),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);
                        startY = startY + 20;

                        graphics.DrawString(drowcat["ack_cust_add2"].ToString(), new Font("Tahoma", 10),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);
                        startY = startY + 20;
                    }

                    else if (drowcat["ack_print_add_tp"].ToString() == "G1")
                    {
                        graphics.DrawString(drowcat["ack_guar1_name"].ToString(), new Font("Tahoma", 10),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);
                        startY = startY + 20;

                        graphics.DrawString(drowcat["ack_guar1_add1"].ToString(), new Font("Tahoma", 10),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);
                        startY = startY + 20;

                        graphics.DrawString(drowcat["ack_guar1_add2"].ToString(), new Font("Tahoma", 10),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);
                        startY = startY + 20;
                    }

                    else if (drowcat["ack_print_add_tp"].ToString() == "G2")
                    {
                        graphics.DrawString(drowcat["ack_guar2_name"].ToString(), new Font("Tahoma", 10),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);
                        startY = startY + 20;

                        graphics.DrawString(drowcat["ack_guar2_add1"].ToString(), new Font("Tahoma", 10),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);
                        startY = startY + 20;

                        graphics.DrawString(drowcat["ack_guar2_add2"].ToString(), new Font("Tahoma", 10),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + Offset);
                        startY = startY + 20;
                    }
                }
            }

        }

        public void HPInforPrint()
        {
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataTable glb_hp_infor = new DataTable();
            DataRow dr;

            param.Clear();

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

            //DataTable glb_hp_infor = bsObj.CHNLSVC.Financial.PrintHPInformation(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbReportCompCode, BaseCls.GlbUserID, drow["tpl_pc"].ToString());
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_hp_infor = new DataTable();

                    tmp_hp_infor = bsObj.CHNLSVC.Financial.PrintHPInformation(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbReportCompCode, BaseCls.GlbUserID, drow["tpl_pc"].ToString());

                    glb_hp_infor.Merge(tmp_hp_infor);

                }
            }
            _hpInfor.Database.Tables["HP_INFOR"].SetDataSource(glb_hp_infor);
            _hpInfor.Database.Tables["param"].SetDataSource(param);

        }

        //kapila 1/8/2014
        public void NotRecManIssuesPrint()
        {
            DataTable param = new DataTable();
            DataTable GLOB_NOT_REC_MAN_ISS = new DataTable();
            DataRow dr;

            param.Clear();

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


            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_GLOB_NOT_REC_MAN_ISS = new DataTable();

                    tmp_GLOB_NOT_REC_MAN_ISS = bsObj.CHNLSVC.Sales.PrintNotRecManIssues(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate));

                    GLOB_NOT_REC_MAN_ISS.Merge(tmp_GLOB_NOT_REC_MAN_ISS);

                }
            }

            _NotRecManIss.Database.Tables["NotRecManIss"].SetDataSource(GLOB_NOT_REC_MAN_ISS);
            _NotRecManIss.Database.Tables["param"].SetDataSource(param);

        }

        public void HPDiriyaFundPrint()
        {
            DataTable param = new DataTable();
            DataRow dr;

            param.Clear();

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

            DataTable glb_diriya_fund = bsObj.CHNLSVC.Financial.PrintDiriyaFund(BaseCls.GlbReportCompCode, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID);

            _insuFund.Database.Tables["Insu_Fund"].SetDataSource(glb_diriya_fund);
            _insuFund.Database.Tables["param"].SetDataSource(param);

        }

        public void HPArrearsPrint()
        {// kapila 27/3/2013
            DataTable param = new DataTable();
            DataRow dr;
            DataTable _HPArr = bsObj.CHNLSVC.Financial.ProcessHPArrearsPrint(BaseCls.GlbReportDoc, BaseCls.GlbReportDocType, Convert.ToDateTime(BaseCls.GlbReportFromDate));

            param.Clear();
            param.Columns.Add("reminddate", typeof(string));
            param.Columns.Add("Remark", typeof(string));
            dr = param.NewRow();
            dr["reminddate"] = BaseCls.GlbReportAsAtDate;
            dr["Remark"] = BaseCls.GlbReportRmk;
            param.Rows.Add(dr);

            //tharanga 2017-04-28  
            #region
            ReminderLetter _ltr = new ReminderLetter();

            DataTable oDataTable = new DataTable();
            oDataTable = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            DataTable oprofitCenter = new DataTable();
            oprofitCenter = bsObj.CHNLSVC.CustService.get_profitcenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            #endregion


            _hpArrears.Database.Tables["HPArrears"].SetDataSource(_HPArr);
            _hpArrears.Database.Tables["param"].SetDataSource(param);
            //tharanga 
            _hpArrears.Database.Tables["compnyDetails"].SetDataSource(oDataTable);
            _hpArrears.Database.Tables["BranchDetails"].SetDataSource(oprofitCenter);

        }

        public void HPAgreementPrint()
        {// Sanjeewa 08/06/2013
            DataTable _HPAgreeHdr = bsObj.CHNLSVC.MsgPortal.GetAgreementPrintingHeaderDetails(BaseCls.GlbReportDoc);
            DataTable _HPAgreeCust = bsObj.CHNLSVC.MsgPortal.GetAgreementPrintingCustomerDetails(BaseCls.GlbReportDoc);
            DataTable _HPAgreeItm = bsObj.CHNLSVC.MsgPortal.GetAgreementPrintingItemDetails(BaseCls.GlbReportDoc);

            if (BaseCls.GlbUserComCode == "SGL" | BaseCls.GlbUserComCode == "SGD")
            {
                _HPAgreerptSGL.Database.Tables["AGR_HDR"].SetDataSource(_HPAgreeHdr);
                _HPAgreerptSGL.Database.Tables["AGR_CUST"].SetDataSource(_HPAgreeCust);
                _HPAgreerptSGL.Database.Tables["AGR_ITEM"].SetDataSource(_HPAgreeItm);
            }
            else
            {
                _HPAgreerpt.Database.Tables["AGR_HDR"].SetDataSource(_HPAgreeHdr);
                _HPAgreerpt.Database.Tables["AGR_CUST"].SetDataSource(_HPAgreeCust);
                _HPAgreerpt.Database.Tables["AGR_ITEM"].SetDataSource(_HPAgreeItm);
            }

        }

        public void FinalReminderPrint(string _rpt)
        {// kapila 27/3/2013
            DataTable param = new DataTable();
            DataRow dr;
            DataTable _finalReminder = bsObj.CHNLSVC.Financial.ProcessFinalReminder(BaseCls.GlbReportDoc, BaseCls.GlbReportDocType, Convert.ToDateTime(BaseCls.GlbReportFromDate));

            param.Clear();
            param.Columns.Add("reminddate", typeof(DateTime));
            param.Columns.Add("Remark", typeof(string));

            dr = param.NewRow();
            dr["reminddate"] = BaseCls.GlbReportAsAtDate;
            dr["Remark"] = BaseCls.GlbReportRmk;
            param.Rows.Add(dr);

            //Tharanga  27/04/2017

            ReminderLetter _ltr = new ReminderLetter();
            _ltr.Hrl_com = BaseCls.GlbUserComCode;

            string cmpnyName = "";
            DataTable oDataTable = new DataTable();
            oDataTable = bsObj.CHNLSVC.General.GetCompanyByCode(_ltr.Hrl_com);

            DataTable oprofitCenter = new DataTable();
            oprofitCenter = bsObj.CHNLSVC.CustService.get_profitcenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            //


            if (_rpt == "FinalReminder")
            {
                _final_Reminder.Database.Tables["FinalReminder"].SetDataSource(_finalReminder);
                _final_Reminder.Database.Tables["param"].SetDataSource(param);
                _final_Reminder.Database.Tables["compnyDetails"].SetDataSource(oDataTable);
                _final_Reminder.Database.Tables["BranchDetails"].SetDataSource(oprofitCenter);

            }
            if (_rpt == "AccountClose")
            {
                _accClose.Database.Tables["FinalReminder"].SetDataSource(_finalReminder);
                //_accClose.Database.Tables["param"].SetDataSource(param);

            }
            if (_rpt == "Reminder")
            {
                _reminder.Database.Tables["FinalReminder"].SetDataSource(_finalReminder);
                _reminder.Database.Tables["param"].SetDataSource(param);
                _reminder.Database.Tables["compnyDetails"].SetDataSource(oDataTable);
                _reminder.Database.Tables["BranchDetails"].SetDataSource(oprofitCenter);

            }
            if (_rpt == "FinalReminder")
            {
                _final_Reminder.Database.Tables["FinalReminder"].SetDataSource(_finalReminder);
                _final_Reminder.Database.Tables["param"].SetDataSource(param);
                _final_Reminder.Database.Tables["compnyDetails"].SetDataSource(oDataTable);
                _final_Reminder.Database.Tables["BranchDetails"].SetDataSource(oprofitCenter);

            }
            if (_rpt == "AccountClose")
            {
                _accClose.Database.Tables["FinalReminder"].SetDataSource(_finalReminder);
                //_accClose.Database.Tables["param"].SetDataSource(param);

            }
            if (_rpt == "Reminder")
            {
                _reminder.Database.Tables["FinalReminder"].SetDataSource(_finalReminder);
                _reminder.Database.Tables["param"].SetDataSource(param);
                _reminder.Database.Tables["compnyDetails"].SetDataSource(oDataTable);
                _reminder.Database.Tables["BranchDetails"].SetDataSource(oprofitCenter);

            }

        }
        public void RevertReleaseOther()
        {// Nadeeka 25-04-2014
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataTable GLB_REVERT_REPORT_OTH = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_GLB_REVERT_REPORT_OTH = new DataTable();
                    tmp_GLB_REVERT_REPORT_OTH = bsObj.CHNLSVC.MsgPortal.GetRevertReleaseOther(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbReportStatus);
                    GLB_REVERT_REPORT_OTH.Merge(tmp_GLB_REVERT_REPORT_OTH);
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


            _rvtOth.Database.Tables["GLB_REVERT_REPORT_OTH"].SetDataSource(GLB_REVERT_REPORT_OTH);
            _rvtOth.Database.Tables["param"].SetDataSource(param);

        }

        public void GetReduceBalInterest()
        {// Nadeeka 29-08-2014
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataTable GLB_REDUCE_BAL_REP = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_GLB_REDUCE_BAL_REP = new DataTable();

                    tmp_GLB_REDUCE_BAL_REP = bsObj.CHNLSVC.MsgPortal.GetReduceBalInterest(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportWithStatus, BaseCls.GlbReportWithDetail);

                    GLB_REDUCE_BAL_REP.Merge(tmp_GLB_REDUCE_BAL_REP);

                }
            }

            param.Clear();
            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("par1", typeof(Int16));
            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            if (BaseCls.GlbReportWithStatus == 1)
            {
                dr["par1"] = 1;
            }
            else
            {
                dr["par1"] = 0;
            }
            param.Rows.Add(dr);

            if (BaseCls.GlbReportName == "InterestCalcReduceBal_new.rpt")
            {
                _intredbalnew.Database.Tables["GLB_REDUCE_BAL_REP"].SetDataSource(GLB_REDUCE_BAL_REP);
                _intredbalnew.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportName == "InterestCalcReduceBal.rpt")
            {
                _intredbal.Database.Tables["GLB_REDUCE_BAL_REP"].SetDataSource(GLB_REDUCE_BAL_REP);
                _intredbal.Database.Tables["param"].SetDataSource(param);
            }
            

        }

        public void RevertNReleaseReport()
        {// Sanjeewa 13-02-2013
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataTable PROC_RVT_N_RLS = new DataTable();
            DataRow dr;

            // DataTable PROC_RVT_N_RLS = bsObj.CHNLSVC.Sales.GetRevertNReleaseDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbReportProfit, BaseCls.GlbUserID);

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_RVT_N_RLS = new DataTable();

                    tmp_RVT_N_RLS = bsObj.CHNLSVC.MsgPortal.GetRevertNReleaseDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID);

                    PROC_RVT_N_RLS.Merge(tmp_RVT_N_RLS);

                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            DataTable PROC_RVT_N_RLS1 = new DataTable();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            param.Rows.Add(dr);
            if (BaseCls.GlbReportExeType == "All")
            {
                _RvtNRlssrpt.Database.Tables["PROC_RVT_N_RLS"].SetDataSource(PROC_RVT_N_RLS);
                _RvtNRlssrpt.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportExeType == "<")
            {
                DataRow[] dr2 = PROC_RVT_N_RLS.Select("(INS_CNT < " + BaseCls.GlbReportDiscRate + ") ");
                if (dr2.Count() > 0)
                {
                    PROC_RVT_N_RLS1 = PROC_RVT_N_RLS.Select("(INS_CNT <" + BaseCls.GlbReportDiscRate + ") ").CopyToDataTable();
                    _RvtNRlssrpt.Database.Tables["PROC_RVT_N_RLS"].SetDataSource(PROC_RVT_N_RLS1);
                    _RvtNRlssrpt.Database.Tables["param"].SetDataSource(param);
                }
            }
            if (BaseCls.GlbReportExeType == "=")
            {
                DataRow[] dr2 = PROC_RVT_N_RLS.Select("(INS_CNT = " + BaseCls.GlbReportDiscRate + ") ");
                if (dr2.Count() > 0)
                {
                    PROC_RVT_N_RLS1 = PROC_RVT_N_RLS.Select("(INS_CNT =" + BaseCls.GlbReportDiscRate + ") ").CopyToDataTable();
                    _RvtNRlssrpt.Database.Tables["PROC_RVT_N_RLS"].SetDataSource(PROC_RVT_N_RLS1);
                    _RvtNRlssrpt.Database.Tables["param"].SetDataSource(param);
                }
            }
            if (BaseCls.GlbReportExeType == ">")
            {
                DataRow[] dr2 = PROC_RVT_N_RLS.Select("(INS_CNT > " + BaseCls.GlbReportDiscRate + ") ");
                if (dr2.Count() > 0)
                {
                    PROC_RVT_N_RLS1 = PROC_RVT_N_RLS.Select("(INS_CNT >" + BaseCls.GlbReportDiscRate + ") ").CopyToDataTable();
                    _RvtNRlssrpt.Database.Tables["PROC_RVT_N_RLS"].SetDataSource(PROC_RVT_N_RLS1);
                    _RvtNRlssrpt.Database.Tables["param"].SetDataSource(param);
                }
            }
        }

        public void ExportCustomerDetailReport()
        {// Sanjeewa 05-10-2013
            DataTable CUSTOMER_DTL = new DataTable();
            Int16 IsLast = 0;
            short rcont = 0;
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    //if (rcont ==  (tmp_user_pc.Rows.Count-1))
                    //{ IsLast = 1; }
                    //else
                    //{ IsLast = 0; }

                    //    rcont = + 1;
                    DataTable TMP_DataTable = new DataTable();
                    //TMP_DataTable = bsObj.CHNLSVC.Sales.GetCutomerDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbUserID, BaseCls.GlbReportDiscTp, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), 0);

                    CUSTOMER_DTL.Merge(TMP_DataTable);
                }
            }
            //CUSTOMER_DTL = bsObj.CHNLSVC.Sales.GetCutomerDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbUserID, BaseCls.GlbReportDiscTp, BaseCls.GlbUserComCode, BaseCls.GlbUserID, 1);

            //  DataTable CUSTOMER_DTL = bsObj.CHNLSVC.Sales.GetCutomerDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbUserID, BaseCls.GlbReportDiscTp, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);

        }

        public void TotalArrearsReport()
        {// Sanjeewa 05-08-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable TOT_ARREARS = bsObj.CHNLSVC.MsgPortal.GetTotal_Arrears(Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbReportScheme, BaseCls.GlbUserID);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("scheme", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["scheme"] = BaseCls.GlbReportScheme == "" ? "ALL" : BaseCls.GlbReportScheme;

            param.Rows.Add(dr);

            _TotArrears.Database.Tables["TOT_ARREARS"].SetDataSource(TOT_ARREARS);
            _TotArrears.Database.Tables["param"].SetDataSource(param);

        }

       
        //public void RecievableAgeAnalysisReport()
        //{// Sanjeewa 20-11-2013
        //    DataTable param = new DataTable();
        //    DataRow dr;

        //    tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

        //    if (BaseCls.GlbReportGroupBy.Contains("ACC"))
        //        if (tmp_user_pc != null && tmp_user_pc.Rows.Count > 0)
        //            if (tmp_user_pc.Rows.Count > 1)
        //            {
        //                MessageBox.Show("Please use one location at a time to get account wise report", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                return;
        //            }

        //    if (tmp_user_pc.Rows.Count > 0)
        //    {
        //        foreach (DataRow drow in tmp_user_pc.Rows)
        //        {
        //            DataTable TMP_DataTable = new DataTable();
        //            TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetRec_Age_Analysis(BaseCls.GlbReportYear, BaseCls.GlbReportMonth, BaseCls.GlbReportnoofDays, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportDoc1, BaseCls.GlbUserID);
        //            GLOB_DataTable.Merge(TMP_DataTable);
        //        }
        //    }
        //    //string _erro = string.Empty;
        //    //DataTable GLOB_DataTable = new DataTable();
        //    //GLOB_DataTable = bsObj.CHNLSVC.Sales.GetRec_Age_Analysis_New(BaseCls.GlbReportYear, BaseCls.GlbReportMonth, BaseCls.GlbReportnoofDays, BaseCls.GlbUserComCode, BaseCls.GlbUserID, BaseCls.GlbReportGroupBy, out _erro);

        //    param.Clear();

        //    param.Columns.Add("user", typeof(string));
        //    param.Columns.Add("heading_1", typeof(string));
        //    param.Columns.Add("period", typeof(string));
        //    param.Columns.Add("profitcenter", typeof(string));
        //    param.Columns.Add("noofmonths", typeof(int));
        //    param.Columns.Add("reportgroup", typeof(string));

        //    dr = param.NewRow();
        //    dr["user"] = BaseCls.GlbUserID;
        //    dr["heading_1"] = BaseCls.GlbReportHeading;
        //    dr["period"] = BaseCls.GlbReportYear + " - " + BaseCls.GlbReportMonth;
        //    dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
        //    dr["noofmonths"] = BaseCls.GlbReportnoofDays;
        //    dr["reportgroup"] = BaseCls.GlbReportGroupBy;

        //    param.Rows.Add(dr);

        //    _RecAge.Database.Tables["REC_AGE"].SetDataSource(GLOB_DataTable);
        //    _RecAge.Database.Tables["param"].SetDataSource(param);

        //}

        public void HPPureCreationReport()
        {// Sanjeewa 06-06-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.getHPPureCreationDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

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

            _HPPureCre.Database.Tables["PURE_CRE"].SetDataSource(GLOB_DataTable);
            _HPPureCre.Database.Tables["param"].SetDataSource(param);

        }

        public void GroupSaleReport()
        {// Sanjeewa 20-01-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.Get_Group_Sale_Details(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("with_items", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["with_items"] = BaseCls.GlbReportItemType;

            param.Rows.Add(dr);

            _grpSale.Database.Tables["GRP_SALE"].SetDataSource(GLOB_DataTable);
            _grpSale.Database.Tables["param"].SetDataSource(param);

            DataTable ItemDetails = new DataTable();
            //ItemDetails = bsObj.CHNLSVC.Sales.Get_GroupSaleInvoiceDetails(GLOB_DataTable);

            if (GLOB_DataTable.Rows.Count > 0)
            {
                foreach (DataRow drow in GLOB_DataTable.Rows)
                {
                    DataTable _temp = new DataTable();
                    _temp = bsObj.CHNLSVC.Sales.Get_GroupSaleInvoiceDetails1(drow["HPA_INVC_NO"].ToString());
                    ItemDetails.Merge(_temp);
                }
            }
            else
            {
                ItemDetails = bsObj.CHNLSVC.Sales.Get_GroupSaleInvoiceDetails1("N/A");
            }

            foreach (object repOp in _grpSale.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "item_details")
                    {
                        ReportDocument subRepDoc = _grpSale.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SAT_ITM"].SetDataSource(ItemDetails);
                    }
                }
            }

        }
        public void IntroducerCommissioneReport()
        {// Nadeeka 25-02-2015
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetAddIntroduceCommision(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportScheme, BaseCls.GlbUserID, BaseCls.GlbReportExecCode);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("scheme", typeof(string));
            param.Columns.Add("promoter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["scheme"] = BaseCls.GlbReportScheme == "" ? "ALL" : BaseCls.GlbReportScheme;
            dr["promoter"] = BaseCls.GlbReportExecCode == "" ? "ALL" : BaseCls.GlbReportExecCode;
            param.Rows.Add(dr);

            _interdComm.Database.Tables["INC_SCHEME"].SetDataSource(GLOB_DataTable);
            _interdComm.Database.Tables["param"].SetDataSource(param);

        }
        public void AdditionalIncentiveReport()
        {// Sanjeewa 14-03-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetAddIncentiveDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportScheme, BaseCls.GlbUserID);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("scheme", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["scheme"] = BaseCls.GlbReportScheme == "" ? "ALL" : BaseCls.GlbReportScheme;

            param.Rows.Add(dr);

            _addIncent.Database.Tables["INC_SCHEME"].SetDataSource(GLOB_DataTable);
            _addIncent.Database.Tables["param"].SetDataSource(param);

        }

        public void TrimAccountsReport()
        {// Sanjeewa 19-03-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetTrimAccountDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportScheme, BaseCls.GlbUserID);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("scheme", typeof(string));
            param.Columns.Add("reptp", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["scheme"] = BaseCls.GlbReportScheme == "" ? "ALL" : BaseCls.GlbReportScheme;
            dr["reptp"] = BaseCls.GlbReportGroupBy;

            param.Rows.Add(dr);

            _trim_acc.Database.Tables["TRIM_ACC"].SetDataSource(GLOB_DataTable);
            _trim_acc.Database.Tables["param"].SetDataSource(param);

        }

        public void CustomerAcknowledgementLogReport()
        {// Sanjeewa 21-03-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetCustomerAckLogDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbReportDoc);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

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

            _ackLog.Database.Tables["CUST_ACK_LOG"].SetDataSource(GLOB_DataTable);
            _ackLog.Database.Tables["param"].SetDataSource(param);

        }

        public void GracePeriodArrearsReport()
        {// Sanjeewa 13-09-2013
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetGracePeriod_Arrears(Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }
            //DataTable GRP_ARREARS = bsObj.CHNLSVC.Sales.GetGracePeriod_Arrears(Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbUserID);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;

            param.Rows.Add(dr);

            _GrpArrears.Database.Tables["GR_P_ARREARS"].SetDataSource(GLOB_DataTable);
            _GrpArrears.Database.Tables["param"].SetDataSource(param);

        }

        public void AgreementStatementReport()
        {// Sanjeewa 19-12-2013
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetAgreementStatementDetails(Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            ////dr["asatdate"] = DateTime.Now; //BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy"); // Commented by Chathura
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy"); // Added by Chathura
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;

            param.Rows.Add(dr);

            _agreStmt.Database.Tables["AGREE_STATE"].SetDataSource(GLOB_DataTable);
            _agreStmt.Database.Tables["param"].SetDataSource(param);

        }

        public void AgreementCheckReport()
        {// Sanjeewa 20-12-2013
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetAgreementCheckDetails(Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;

            param.Rows.Add(dr);

            _agreCheck.Database.Tables["AGREE_CHECK"].SetDataSource(GLOB_DataTable);
            _agreCheck.Database.Tables["param"].SetDataSource(param);

        }

        public void AgreementCheckListReport()
        {// Sanjeewa 23-01-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetAgreementChecklistDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;

            param.Rows.Add(dr);

            _agrechklist.Database.Tables["AGREE_CHECKLIST"].SetDataSource(GLOB_DataTable);
            _agrechklist.Database.Tables["param"].SetDataSource(param);

        }

        public void CollectionBonusReconReport()
        {// Sanjeewa 22-12-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetColBonusReconDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbReportDocType);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("type", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["type"] = BaseCls.GlbReportDocType;

            param.Rows.Add(dr);

            _colbonrecon.Database.Tables["COL_BON_RECON"].SetDataSource(GLOB_DataTable);
            _colbonrecon.Database.Tables["param"].SetDataSource(param);

        }

        public void AgreementStatementDetailReport()
        {// Sanjeewa 21-12-2013
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetAgreementStatementDtlDetails(Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;

            param.Rows.Add(dr);

            _agreStmtDtl.Database.Tables["AGREE_DTL"].SetDataSource(GLOB_DataTable);
            _agreStmtDtl.Database.Tables["param"].SetDataSource(param);

        }

        public void AgreementPendingDetailReport()
        {// Sanjeewa 22-10-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetAgreementPendingDetails(Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;

            param.Rows.Add(dr);

            _agrepend.Database.Tables["AGREE_PENDING"].SetDataSource(GLOB_DataTable);
            _agrepend.Database.Tables["param"].SetDataSource(param);

        }

        public void CurrentMonthDueReport()
        {// Sanjeewa 17-05-2013
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetCurrentMonthDueDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Year, Convert.ToDateTime(BaseCls.GlbReportFromDate).Month, BaseCls.GlbReportScheme, BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }
            //DataTable CURR_M_DUE = bsObj.CHNLSVC.Sales.GetCurrentMonthDueDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Year, Convert.ToDateTime(BaseCls.GlbReportFromDate).Month, BaseCls.GlbReportScheme, BaseCls.GlbUserID);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("scheme", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = BaseCls.GlbReportFromDate.Date.Year + " - " + BaseCls.GlbReportFromDate.Date.Month;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["scheme"] = BaseCls.GlbReportScheme == "" ? "ALL" : BaseCls.GlbReportScheme;

            param.Rows.Add(dr);

            _CurrMDue.Database.Tables["CURR_M_DUE"].SetDataSource(GLOB_DataTable);
            _CurrMDue.Database.Tables["param"].SetDataSource(param);
        }

        public void AllDueReport()
        {// Sanjeewa 23-05-2013
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataTable ALL_DUE_SUM = new DataTable();
            DataRow dr;

            // DataTable ALL_DUE_SUM = bsObj.CHNLSVC.Sales.GetAllDueDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Year, Convert.ToDateTime(BaseCls.GlbReportFromDate).Month, BaseCls.GlbReportScheme, BaseCls.GlbUserID,, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_ALL_DUE_SUM = new DataTable();

                    tmp_ALL_DUE_SUM = bsObj.CHNLSVC.MsgPortal.GetAllDueDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Year, Convert.ToDateTime(BaseCls.GlbReportFromDate).Month, BaseCls.GlbReportScheme, BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());

                    ALL_DUE_SUM.Merge(tmp_ALL_DUE_SUM);

                }
            }
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("scheme", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = BaseCls.GlbReportFromDate.Date.Year + " - " + BaseCls.GlbReportFromDate.Date.Month;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["scheme"] = BaseCls.GlbReportScheme == "" ? "ALL" : BaseCls.GlbReportScheme;

            param.Rows.Add(dr);

            _AllDue.Database.Tables["ALL_DUE_SUM"].SetDataSource(ALL_DUE_SUM);
            _AllDue.Database.Tables["param"].SetDataSource(param);
        }

        //public void ProcessManagerReport()
        //{
        //    DataRow dr;
        //    DataTable dtManagerCommReport = new DataTable();
        //    DataTable dtManagerCBonusReport = new DataTable();
        //    DataTable dtManagerCode = new DataTable();
        //    Double _amAmt=0;
        //    Double _ambAmt = 0;
        //    dtManagerCode.Columns.Add("mepf", typeof(string));
        //    dtManagerCommReport = bsObj.CHNLSVC.Sales.ProcessManagerCommission(string.Empty ,BaseCls.GlbUserID,BaseCls.GlbReportDoc, string.Empty, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate));
        //    dtManagerCBonusReport = bsObj.CHNLSVC.Sales.ProcessManagerCollBonus(string.Empty, BaseCls.GlbUserID, BaseCls.GlbReportDoc, string.Empty, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate));

        //    foreach (DataRow row in dtManagerCommReport.Rows)
        //    {
        //        dr = dtManagerCode.NewRow();
        //        dr["mepf"] = row["esep_epf"].ToString();
        //        dtManagerCode.Rows.Add(dr);
        //    }

        //    foreach (DataRow row in dtManagerCBonusReport.Rows)
        //    {
        //        dr = dtManagerCode.NewRow();
        //        dr["mepf"] = row["esep_epf"].ToString();
        //        dtManagerCode.Rows.Add(dr);
        //    }
        //    if (dtManagerCode.Rows.Count  > 0)
        //    {
        //        dtManagerCode = dtManagerCode.DefaultView.ToTable(true);
        //    }

        //    List<MasterCompany> _list = bsObj.CHNLSVC.General.GetALLMasterCompaniesData();




        //    Excel.Application excelApp = new Excel.Application();
        //    Excel.Workbook workbook = (Excel.Workbook)excelApp.Workbooks.Add(Missing.Value);
        //    Excel.Worksheet worksheet;

        //    // Opening excel file
        //    workbook.SaveAs("D:\\test2.xls", Excel.XlPlatform.xlWindows, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
        //    workbook.Close(true, Missing.Value, Missing.Value);
        //    workbook = excelApp.Workbooks.Open("D:\\test2.xls", 0, false, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
        //    // Get first Worksheet
        //    worksheet = (Excel.Worksheet)workbook.Sheets.get_Item(1);
        //    ((Excel.Range)worksheet.Cells[1, "A"]).Value2 = "Period From " + Convert.ToDateTime(BaseCls.GlbReportFromDate) + "To " + Convert.ToDateTime(BaseCls.GlbReportToDate);
        //    Int16 rwcount =3;
        //            ((Excel.Range)worksheet.Cells[2, "A"]).Value2 = "Code";
        //            ((Excel.Range)worksheet.Cells[2, "B"]).Value2 = "Showroom";
        //            ((Excel.Range)worksheet.Cells[2, "C"]).Value2 = "SR Manager Name";
        //            ((Excel.Range)worksheet.Cells[2, "D"]).Value2 = "EPF No";
        //            ((Excel.Range)worksheet.Cells[2, "J"]).Value2 = "Total Comm";

        //    foreach (DataRow row1 in dtManagerCode.Rows)
        //    {
        //    foreach (MasterCompany _cCode in _list)
        //    {
        //        if (_cCode.Mc_anal11 != 0)
        //        {
        //            if (_cCode.Mc_anal11 == 1) { ((Excel.Range)worksheet.Cells[rwcount, "E"]).Value2 = 0; ((Excel.Range)worksheet.Cells[2, "E"]).Value2 = "Total Comm " + _cCode.Mc_desc; }

        //            if (_cCode.Mc_anal11 == 2) { ((Excel.Range)worksheet.Cells[rwcount, "F"]).Value2 = 0; ((Excel.Range)worksheet.Cells[2, "F"]).Value2 = "Total Comm " + _cCode.Mc_desc; }

        //            if (_cCode.Mc_anal11 == 3) { ((Excel.Range)worksheet.Cells[rwcount, "G"]).Value2 = 0; ((Excel.Range)worksheet.Cells[2, "G"]).Value2 = "Total Comm " + _cCode.Mc_desc; }

        //            if (_cCode.Mc_anal11 == 4) { ((Excel.Range)worksheet.Cells[rwcount, "H"]).Value2 = 0; ((Excel.Range)worksheet.Cells[2, "H"]).Value2 = "Total Comm " + _cCode.Mc_desc; }

        //            if (_cCode.Mc_anal11 == 5) { ((Excel.Range)worksheet.Cells[rwcount, "I"]).Value2 = 0; ((Excel.Range)worksheet.Cells[2, "I"]).Value2 = "Total Comm " + _cCode.Mc_desc; }
        //            if (_cCode.Mc_anal11 == 1) { ((Excel.Range)worksheet.Cells[rwcount, "K"]).Value2 = 0; ((Excel.Range)worksheet.Cells[2, "K"]).Value2 = "Collection Bonus " + _cCode.Mc_desc; }

        //            if (_cCode.Mc_anal11 == 2) { ((Excel.Range)worksheet.Cells[rwcount, "L"]).Value2 = 0; ((Excel.Range)worksheet.Cells[2, "L"]).Value2 = "Collection Bonus  " + _cCode.Mc_desc; }

        //            if (_cCode.Mc_anal11 == 3) { ((Excel.Range)worksheet.Cells[rwcount, "M"]).Value2 = 0; ((Excel.Range)worksheet.Cells[2, "M"]).Value2 = "Collection Bonus " + _cCode.Mc_desc; }

        //            if (_cCode.Mc_anal11 == 4) { ((Excel.Range)worksheet.Cells[rwcount, "N"]).Value2 = 0; ((Excel.Range)worksheet.Cells[2, "N"]).Value2 = "Collection Bonus " + _cCode.Mc_desc; }

        //            if (_cCode.Mc_anal11 == 5) { ((Excel.Range)worksheet.Cells[rwcount, "O"]).Value2 = 0; ((Excel.Range)worksheet.Cells[2, "O"]).Value2 = "Collection Bonus " + _cCode.Mc_desc; }

        //            _amAmt = 0;
        //            _ambAmt = 0;
        //            dtManagerCommReport = bsObj.CHNLSVC.Sales.ProcessManagerCommission(string.Empty, BaseCls.GlbUserID, row1["mepf"].ToString(), _cCode.Mc_cd, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate));
        //            dtManagerCBonusReport = bsObj.CHNLSVC.Sales.ProcessManagerCollBonus(string.Empty, BaseCls.GlbUserID, row1["mepf"].ToString(), _cCode.Mc_cd, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate));
        //                foreach (DataRow row in dtManagerCommReport.Rows)
        //                {
        //                    // Setting cell values
        //                    ((Excel.Range)worksheet.Cells[rwcount, "A"]).Value2 = row["rem_pc"].ToString();
        //                    ((Excel.Range)worksheet.Cells[rwcount, "B"]).Value2 = row["mpc_desc"].ToString();
        //                    ((Excel.Range)worksheet.Cells[rwcount, "C"]).Value2 = row["esep_name_initials"].ToString();
        //                    ((Excel.Range)worksheet.Cells[rwcount, "D"]).Value2 = row["esep_epf"].ToString();
        //                    if (_cCode.Mc_anal11 == 1) { ((Excel.Range)worksheet.Cells[rwcount, "E"]).Value2 = row["rem_val_final"].ToString();   }

        //                    if (_cCode.Mc_anal11 == 2) { ((Excel.Range)worksheet.Cells[rwcount, "F"]).Value2 = row["rem_val_final"].ToString();  }

        //                    if (_cCode.Mc_anal11 == 3) { ((Excel.Range)worksheet.Cells[rwcount, "G"]).Value2 = row["rem_val_final"].ToString();  }

        //                    if (_cCode.Mc_anal11 == 4) { ((Excel.Range)worksheet.Cells[rwcount, "H"]).Value2 = row["rem_val_final"].ToString(); }

        //                    if (_cCode.Mc_anal11 == 5) { ((Excel.Range)worksheet.Cells[rwcount, "I"]).Value2 = row["rem_val_final"].ToString();   }

        //                    _amAmt  += double.Parse(row["rem_val_final"].ToString());

        //                }
        //                ((Excel.Range)worksheet.Cells[rwcount, "j"]).Value2 = _amAmt;

        //                foreach (DataRow row in dtManagerCBonusReport.Rows)
        //                {   // Setting cell values
        //                    ((Excel.Range)worksheet.Cells[rwcount, "A"]).Value2 = row["rem_pc"].ToString();
        //                    ((Excel.Range)worksheet.Cells[rwcount, "B"]).Value2 = row["mpc_desc"].ToString();
        //                    ((Excel.Range)worksheet.Cells[rwcount, "C"]).Value2 = row["esep_name_initials"].ToString();
        //                    ((Excel.Range)worksheet.Cells[rwcount, "D"]).Value2 = row["esep_epf"].ToString();
        //                    if (_cCode.Mc_anal11 == 1) { ((Excel.Range)worksheet.Cells[rwcount, "K"]).Value2 = row["rem_val_final"].ToString();   }

        //                    if (_cCode.Mc_anal11 == 2) { ((Excel.Range)worksheet.Cells[rwcount, "L"]).Value2 = row["rem_val_final"].ToString();  }

        //                    if (_cCode.Mc_anal11 == 3) { ((Excel.Range)worksheet.Cells[rwcount, "M"]).Value2 = row["rem_val_final"].ToString();  }

        //                    if (_cCode.Mc_anal11 == 4) { ((Excel.Range)worksheet.Cells[rwcount, "N"]).Value2 = row["rem_val_final"].ToString();   }

        //                    if (_cCode.Mc_anal11 == 5) { ((Excel.Range)worksheet.Cells[rwcount, "O"]).Value2 = row["rem_val_final"].ToString();   }
        //                    _ambAmt += double.Parse(row["rem_val_final"].ToString());
        //                  }
        //                ((Excel.Range)worksheet.Cells[2, "P"]).Value2 = "Coll Bonus Total";
        //                ((Excel.Range)worksheet.Cells[2, "Q"]).Value2 = "Adjustment";
        //                ((Excel.Range)worksheet.Cells[2, "R"]).Value2 = "Toal Comm";
        //                ((Excel.Range)worksheet.Cells[2, "S"]).Value2 ="EPF 20%";
        //                ((Excel.Range)worksheet.Cells[2, "T"]).Value2 = "ETF 3%";
        //                ((Excel.Range)worksheet.Cells[2, "U"]).Value2 = "Commission Finalized Status";

        //                ((Excel.Range)worksheet.Cells[rwcount, "P"]).Value2 = _ambAmt;
        //                ((Excel.Range)worksheet.Cells[rwcount, "Q"]).Value2 = string.Empty;
        //                ((Excel.Range)worksheet.Cells[rwcount, "R"]).Value2 = _ambAmt + _amAmt;
        //                ((Excel.Range)worksheet.Cells[rwcount, "S"]).Value2 = (_ambAmt + _amAmt) * 20 / 100;
        //                ((Excel.Range)worksheet.Cells[rwcount, "T"]).Value2 = (_ambAmt + _amAmt) * 3 / 100;
        //                ((Excel.Range)worksheet.Cells[rwcount, "U"]).Value2 = string.Empty;

        //            }


        //        }
        //    rwcount += 1;
        //    }
        //    ((Excel.Range)worksheet.Cells[rwcount + 1, "A"]).Value2 = "Grand Total";
        //    ((Excel.Range)worksheet.Cells[rwcount + 1, "E"]).Formula = "=sum(E3)";
        //    ((Excel.Range)worksheet.Cells[rwcount + 1, "F"]).Formula = "=sum(F3)";
        //    ((Excel.Range)worksheet.Cells[rwcount + 1, "G"]).Formula = "=sum(G3)";
        //    ((Excel.Range)worksheet.Cells[rwcount + 1, "H"]).Formula = "=sum(H3)";
        //    ((Excel.Range)worksheet.Cells[rwcount + 1, "I"]).Formula = "=sum(I3)";
        //    ((Excel.Range)worksheet.Cells[rwcount + 1, "K"]).Formula = "=sum(K3)";
        //    ((Excel.Range)worksheet.Cells[rwcount + 1, "L"]).Formula = "=sum(L3)";
        //    ((Excel.Range)worksheet.Cells[rwcount + 1, "M"]).Formula = "=sum(M3)";
        //    ((Excel.Range)worksheet.Cells[rwcount + 1, "N"]).Formula = "=sum(N3)";
        //    ((Excel.Range)worksheet.Cells[rwcount + 1, "O"]).Formula = "=sum(O3)";
        //    ((Excel.Range)worksheet.Cells[rwcount+1, "P"]).Formula=  "=sum(P3)";
        //    ((Excel.Range)worksheet.Cells[rwcount + 1, "R"]).Formula = "=sum(R3)";
        //    ((Excel.Range)worksheet.Cells[rwcount + 1, "S"]).Formula = "=sum(S3)";
        //    ((Excel.Range)worksheet.Cells[rwcount + 1, "T"]).Formula = "=sum(T3)";
        //     workbook.Save();

        //     workbook.Close(0, 0, 0);

        //     excelApp.Quit();
        //     Marshal.ReleaseComObject(worksheet);
        //     Marshal.ReleaseComObject(workbook);
        //     Marshal.ReleaseComObject(excelApp);

        //     Excel.Application excelApp1 = new Excel.Application();
        //     excelApp1.Visible = true;

        //     string workbookPath = "D:\\test2.xls";
        //     Excel.Workbook excelWorkbook = excelApp1.Workbooks.Open(workbookPath,
        //             0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
        //             true, false, 0, true, false, false);


        //}

        public void NoOfAccountsReport()
        {// Sanjeewa 13-02-2013
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataTable NO_OF_ACC = new DataTable();
            DataRow dr;

            //  DataTable NO_OF_ACC = bsObj.CHNLSVC.Sales.GetNoOfAccountDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID);
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_NO_OF_ACC = new DataTable();

                    tmp_NO_OF_ACC = bsObj.CHNLSVC.Sales.GetNoOfAccountDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());

                    NO_OF_ACC.Merge(tmp_NO_OF_ACC);

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

            _NoOfAcc.Database.Tables["NO_OF_ACC"].SetDataSource(NO_OF_ACC);
            _NoOfAcc.Database.Tables["param"].SetDataSource(param);

        }

        public void NoOfActAccountsReport()
        {// Sanjeewa 19-02-2013
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataTable NO_OF_ACTACC = new DataTable();
            DataRow dr;

            //DataTable NO_OF_ACTACC = bsObj.CHNLSVC.Sales.GetNoOfActAccountDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_NO_OF_ACTACC = new DataTable();

                    tmp_NO_OF_ACTACC = bsObj.CHNLSVC.MsgPortal.GetNoOfActAccountDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());

                    NO_OF_ACTACC.Merge(tmp_NO_OF_ACTACC);

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

            _NoOfActAcc.Database.Tables["ACT_ACCOUNTS"].SetDataSource(NO_OF_ACTACC);
            _NoOfActAcc.Database.Tables["param"].SetDataSource(param);

        }

        public void TransferedAccountsReport()
        {// Sanjeewa 25-02-2013
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetTransferedAccountDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            //DataTable TRANSACC = bsObj.CHNLSVC.Sales.GetTransferedAccountDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID);

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

            _TransAcc.Database.Tables["TRANS_ACC"].SetDataSource(GLOB_DataTable);
            _TransAcc.Database.Tables["param"].SetDataSource(param);

        }

        //kapila 1/1/13
        public void AgeAnalysisOfDebtorsArrearsPrint()
        {
            int i = 0;
            DataTable mst_PC = new DataTable();
            DataTable glob_debt_arr = new DataTable();
            DataTable param = new DataTable();
            DataTable Glb_Debt_Arr_Sum = new DataTable();
            DataTable Glb_Debt_Arr_Chnl = new DataTable();
            DataRow dr;
            DataRow drPara;

            Boolean isItem = false;

            mst_PC.Clear();
            param.Clear();
            glob_debt_arr.Clear();
            Glb_Debt_Arr_Sum.Clear();
            Glb_Debt_Arr_Chnl.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("vGroupBy", typeof(string));
            param.Columns.Add("startTime", typeof(DateTime));
            param.Columns.Add("endTime", typeof(DateTime));
            param.Columns.Add("itemCode", typeof(string));
            param.Columns.Add("itemCat1", typeof(string));
            param.Columns.Add("itemCat2", typeof(string));
            param.Columns.Add("itemCat3", typeof(string));
            param.Columns.Add("itemModel", typeof(string));
            param.Columns.Add("itemBrand", typeof(string));
            param.Columns.Add("Scheme", typeof(string));



            Glb_Debt_Arr_Sum.Columns.Add("ARR_COM", typeof(string));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_PC", typeof(string));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_ACC", typeof(Decimal));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_RVT_ACC", typeof(Decimal));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_ACT_ACC", typeof(Decimal));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_TOT_ARR_ACC", typeof(Decimal));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_TOT_ARR_VAL", typeof(Decimal));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_CLOSE_BAL", typeof(Decimal));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_POVR_ACC", typeof(Decimal));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_POVR_VAL", typeof(Decimal));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_USER", typeof(string));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_LOC_DESC", typeof(string));

            drPara = param.NewRow();
            drPara["user"] = BaseCls.GlbUserID;
            drPara["asatdate"] = BaseCls.GlbReportAsAtDate;
            drPara["comp"] = BaseCls.GlbReportComp;
            drPara["compaddr"] = BaseCls.GlbReportCompAddr;
            drPara["vGroupBy"] = BaseCls.GlbReportGroupBy;
            drPara["startTime"] = BaseCls.GlbReportStartOn;
            drPara["itemCode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            drPara["itemCat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            drPara["itemCat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            drPara["itemCat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            drPara["itemModel"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            drPara["itemBrand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            drPara["Scheme"] = BaseCls.GlbReportScheme == "" ? "ALL" : BaseCls.GlbReportScheme;


            if (BaseCls.GlbReportScheme == "" && BaseCls.GlbReportItemCode == "" && BaseCls.GlbReportItemCat1 == "" && BaseCls.GlbReportItemCat2 == "" && BaseCls.GlbReportItemCat3 == "" && BaseCls.GlbReportModel == "" && BaseCls.GlbReportBrand == "")
            {
                isItem = false;
            }
            else
            {
                isItem = true;
            }


            //           param.Rows.Add(dr);

            mst_PC = bsObj.CHNLSVC.Sales.GetTempPCLoc(BaseCls.GlbUserID);
            foreach (DataRow row in mst_PC.Rows)
            {
                dr = mst_PC.NewRow();
                dr["MPC_CD"] = row["MPC_CD"].ToString();
                dr["MPC_DESC"] = row["MPC_DESC"].ToString();
                dr["MPC_COM"] = row["MPC_COM"].ToString();
                i = i + 1;
            }

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            DataTable GLOB_DataTable1 = new DataTable();
            DataTable GLOB_DataTable2 = new DataTable();
            DataTable _jobs = new DataTable();

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    DataTable _jobs1 = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Financial.Process_AgeOfDebtors_Arrears_Win(Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbReportScheme, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportModel, BaseCls.GlbReportBrand, isItem, BaseCls.GlbReportDoc, out _jobs1);
                    GLOB_DataTable.Merge(TMP_DataTable);
                    _jobs.Merge(_jobs1);

                    if (i > 1)
                    {
                        TMP_DataTable = new DataTable();
                        TMP_DataTable = bsObj.CHNLSVC.Financial.Process_AgeOfDebtors_Arrears_Chanel(Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbReportScheme, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportModel, BaseCls.GlbReportBrand, isItem);
                        GLOB_DataTable2.Merge(TMP_DataTable);
                    }
                    TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Financial.Process_AgeOfDebtors_Arrears_Sum(Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbReportScheme, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportModel, BaseCls.GlbReportBrand, isItem);
                    GLOB_DataTable1.Merge(TMP_DataTable);
                }
            }

            //glob_debt_arr = bsObj.CHNLSVC.Financial.Process_AgeOfDebtors_Arrears_Win(Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbReportCompCode, null, BaseCls.GlbUserID, BaseCls.GlbReportScheme);

            //Glb_Debt_Arr_Sum = bsObj.CHNLSVC.Financial.Process_AgeOfDebtors_Arrears_Sum(Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbReportCompCode, null, BaseCls.GlbUserID, BaseCls.GlbReportScheme);

            if (i > 1)
            {
                //Glb_Debt_Arr_Chnl = bsObj.CHNLSVC.Financial.Process_AgeOfDebtors_Arrears_Chanel(Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbReportCompCode, null, BaseCls.GlbUserID, BaseCls.GlbReportScheme);
                //_Age_Debt_ArrearsReport.Database.Tables["DEBT_ARR_CHNL"].SetDataSource(Glb_Debt_Arr_Chnl);
                _Age_Debt_ArrearsReport.Database.Tables["DEBT_ARR_CHNL"].SetDataSource(GLOB_DataTable2);
            }

            //_Age_Debt_ArrearsReport.Database.Tables["SP_PROC_AGE_ANLS_OF_DEBT_ARR"].SetDataSource(glob_debt_arr);
            _Age_Debt_ArrearsReport.Database.Tables["SP_PROC_AGE_ANLS_OF_DEBT_ARR"].SetDataSource(GLOB_DataTable);

            drPara["endTime"] = DateTime.Now;
            param.Rows.Add(drPara);
            _Age_Debt_ArrearsReport.Database.Tables["param"].SetDataSource(param);

            //_Age_Debt_ArrearsReport.Database.Tables["Debt_Arr_Sum"].SetDataSource(Glb_Debt_Arr_Sum);
            _Age_Debt_ArrearsReport.Database.Tables["Debt_Arr_Sum"].SetDataSource(GLOB_DataTable1);

            if (BaseCls.GlbReportDoc == "Y")
            {
                _Age_Debt_ArrearsScvReport.Database.Tables["DEBT_ARR_SCV"].SetDataSource(_jobs);
            }

        }


        public void HPCollectionSummary()
        { // Nadeeka 15-02-13
            DataTable param = new DataTable();
            DataRow dr;
            string accNo = default(string);
            accNo = BaseCls.GlbReportDoc;
            DataTable PROC_HS_COLLECTION_ECD = new DataTable();
            DataTable PROC_HS_COLLECTION_REPORT = new DataTable();

            DataTable mst_profit_center = default(DataTable);
            DataTable tmp_user_pc = new DataTable();//Added by Prabhath on 09/11/2013
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportComp, BaseCls.GlbUserID);//Added by Prabhath on 09/11/2013



            foreach (DataRow _r in tmp_user_pc.Rows)
            {

                DataTable _temp0 = bsObj.CHNLSVC.Sales.CollectionSummaryReport(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbReportComp, _r.Field<string>("tpl_pc"), BaseCls.GlbReportExecCode);
                PROC_HS_COLLECTION_REPORT.Merge(_temp0);
                DataTable _temp = bsObj.CHNLSVC.Sales.CollectionSummaryReportECD(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbReportComp, _r.Field<string>("tpl_pc"));
                PROC_HS_COLLECTION_ECD.Merge(_temp);
            }


            DataTable mst_com = default(DataTable);

            foreach (DataRow row in PROC_HS_COLLECTION_REPORT.Rows)
            {
                int index = PROC_HS_COLLECTION_REPORT.Rows.IndexOf(row);
                if (index == 0)
                {
                    mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbReportComp, string.Empty);
                    mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);
                }
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


            _colSum.Database.Tables["PROC_HS_COLLECTION_REPORT"].SetDataSource(PROC_HS_COLLECTION_REPORT);
            // _colSum.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _colSum.Database.Tables["mst_com"].SetDataSource(mst_com);
            _colSum.Database.Tables["param"].SetDataSource(param);



            foreach (object repOp in _colSum.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "ecd")
                    {
                        ReportDocument subRepDoc = _colSum.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["PROC_HS_COLLECTION_ECD"].SetDataSource(PROC_HS_COLLECTION_ECD);
                        subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);


                    }

                    if (_cs.SubreportName == "cancelRec")
                    {
                        ReportDocument subRepDoc = _colSum.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["PROC_HS_COLLECTION_REPORT"].SetDataSource(PROC_HS_COLLECTION_REPORT);



                    }

                }
            }
        }
        public void HPReceiptList()
        { // Nadeeka 20-11-13
            DataTable param = new DataTable();
            DataRow dr;
            string accNo = default(string);
            accNo = BaseCls.GlbReportDoc;
            DataTable GLB_HP_RECEIPT_LIST = new DataTable();
            DataTable PROC_HS_COLLECTION_REPORT = new DataTable();

            DataTable mst_profit_center = default(DataTable);
            DataTable tmp_user_pc = new DataTable();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);



            foreach (DataRow _r in tmp_user_pc.Rows)
            {

                DataTable _temp0 = bsObj.CHNLSVC.Sales.CollectionSummaryReport(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbReportCompCode, _r.Field<string>("tpl_pc"), BaseCls.GlbReportExecCode);
                PROC_HS_COLLECTION_REPORT.Merge(_temp0);
                DataTable _temp = bsObj.CHNLSVC.MsgPortal.ProcessHPReceiptList(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbReportCompCode, _r.Field<string>("tpl_pc"));
                GLB_HP_RECEIPT_LIST.Merge(_temp);
            }


            DataTable mst_com = default(DataTable);

            foreach (DataRow row in PROC_HS_COLLECTION_REPORT.Rows)
            {
                int index = PROC_HS_COLLECTION_REPORT.Rows.IndexOf(row);
                if (index == 0)
                {
                    mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbReportCompCode, string.Empty);
                    mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);
                }
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


            _hpRecList.Database.Tables["PROC_HS_COLLECTION_REPORT"].SetDataSource(PROC_HS_COLLECTION_REPORT);

            _hpRecList.Database.Tables["mst_com"].SetDataSource(mst_com);
            _hpRecList.Database.Tables["param"].SetDataSource(param);



            foreach (object repOp in _hpRecList.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "paymode")
                    {
                        ReportDocument subRepDoc = _hpRecList.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["GLB_HP_RECEIPT_LIST"].SetDataSource(GLB_HP_RECEIPT_LIST);



                    }



                }
            }
        }
        public void CollectionSummaryOther()
        { // Nadeeka 15-02-13
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataRow dr;
            string accNo = default(string);
            accNo = BaseCls.GlbReportDoc;

            DataTable mst_profit_center = default(DataTable);

            //DataTable PROC_HS_COLLECTION_REPORT = default(DataTable);
            //DataTable PROC_HS_COLLECTION_ECD = default(DataTable);
            DataTable PROC_HS_COLLECTION_REPORT = new DataTable();
            DataTable PROC_HS_COLLECTION_ECD = new DataTable();

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportComp, BaseCls.GlbUserID);

            if (BaseCls.GlbReportType == "REC")
            {
                foreach (DataRow _r in tmp_user_pc.Rows)
                {
                    DataTable _temp = new DataTable();
                    //DataTable PROC_HS_COLLECTION_REPORT = bsObj.CHNLSVC.Sales.CollectionSummaryReport_otherCol(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbReportComp);
                    _temp = bsObj.CHNLSVC.Sales.CollectionSummaryReport_otherCol(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbReportComp, _r.Field<string>("tpl_pc"));//Added by Prabhath on 09/11/2013
                    PROC_HS_COLLECTION_REPORT.Merge(_temp);
                }
            }
            else
            {
                foreach (DataRow _r in tmp_user_pc.Rows)
                {
                    DataTable _temp = new DataTable();
                    //DataTable PROC_HS_COLLECTION_REPORT = bsObj.CHNLSVC.Sales.CollectionSummaryReport_other(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbReportComp);
                    _temp = bsObj.CHNLSVC.Sales.CollectionSummaryReport_other(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbReportComp, _r.Field<string>("tpl_pc"));//Added by Prabhath on 09/11/2013
                    PROC_HS_COLLECTION_REPORT.Merge(_temp);
                }
            }
            foreach (DataRow _r in tmp_user_pc.Rows)
            {
                DataTable _temp = new DataTable();
                _temp = bsObj.CHNLSVC.Sales.CollectionSummaryReportECD(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbReportComp, _r.Field<string>("tpl_pc"));
                PROC_HS_COLLECTION_ECD.Merge(_temp);
            }

            DataTable mst_com = default(DataTable);

            foreach (DataRow row in PROC_HS_COLLECTION_REPORT.Rows)
            {
                int index = PROC_HS_COLLECTION_REPORT.Rows.IndexOf(row);
                if (index == 0)
                {
                    mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbReportComp, string.Empty);
                    mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);
                }
            }


            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("othershop", typeof(Int16));
            param.Columns.Add("repHead", typeof(string));


            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            if (BaseCls.GlbReportType == "REC")
            {
                dr["othershop"] = 1;
                dr["repHead"] = "Received Other Shop Collection Summary";
            }
            else
            {
                dr["othershop"] = 0;
                dr["repHead"] = "Collected Other Shop Collection Summary";
            }


            param.Rows.Add(dr);



            if (BaseCls.GlbReportType != "REC")
            {
                _colSumOth.Database.Tables["PROC_HS_COLLECTION_REPORT"].SetDataSource(PROC_HS_COLLECTION_REPORT);

                _colSumOth.Database.Tables["mst_com"].SetDataSource(mst_com);
                _colSumOth.Database.Tables["param"].SetDataSource(param);
                //foreach (object repOp in _colSumOth.ReportDefinition.ReportObjects)
                //{
                //    string _s = repOp.GetType().ToString();
                //    if (_s.ToLower().Contains("subreport"))
                //    {
                //        SubreportObject _cs = (SubreportObject)repOp;
                //        if (_cs.SubreportName == "ecd")
                //        {
                //            ReportDocument subRepDoc = _colSumOth.Subreports[_cs.SubreportName];
                //            subRepDoc.Database.Tables["PROC_HS_COLLECTION_ECD"].SetDataSource(PROC_HS_COLLECTION_ECD);
                //            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);


                //        }


                //    }
                //}
            }
            else
            {
                _colSumOthRec.Database.Tables["PROC_HS_COLLECTION_REPORT"].SetDataSource(PROC_HS_COLLECTION_REPORT);

                _colSumOthRec.Database.Tables["mst_com"].SetDataSource(mst_com);
                _colSumOthRec.Database.Tables["param"].SetDataSource(param);

                //foreach (object repOp in _colSumOthRec.ReportDefinition.ReportObjects)
                //{
                //    string _s = repOp.GetType().ToString();
                //    if (_s.ToLower().Contains("subreport"))
                //    {
                //        SubreportObject _cs = (SubreportObject)repOp;
                //        if (_cs.SubreportName == "ecd")
                //        {
                //            ReportDocument subRepDoc = _colSumOthRec.Subreports[_cs.SubreportName];
                //            subRepDoc.Database.Tables["PROC_HS_COLLECTION_ECD"].SetDataSource(PROC_HS_COLLECTION_ECD);
                //            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);


                //        }


                //    }
                //}
            }



        }
        public void HPInsuranceReport()
        { // Nadeeka 11-02-13


            DataTable param = new DataTable();
            DataRow dr;
            string accNo = default(string);
            accNo = BaseCls.GlbReportDoc;
            DataTable tmp_user_pc = new DataTable();
            DataTable mst_profit_center = default(DataTable);
            DataTable PROC_HS_INSURANCE_FUND = new DataTable();

            //  DataTable PROC_HS_INSURANCE_FUND = bsObj.CHNLSVC.Sales.HPInsuranceReport(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbReportComp);
            DataTable mst_com = default(DataTable);

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportComp, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_HP_CLS_BAL = new DataTable();
                    TMP_HP_CLS_BAL = bsObj.CHNLSVC.Sales.HPInsuranceReport(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbReportComp, drow["tpl_pc"].ToString());
                    PROC_HS_INSURANCE_FUND.Merge(TMP_HP_CLS_BAL);


                }
            }





            foreach (DataRow row in PROC_HS_INSURANCE_FUND.Rows)
            {
                int index = PROC_HS_INSURANCE_FUND.Rows.IndexOf(row);
                if (index == 0)
                {
                    mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbReportComp, string.Empty);
                    mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);
                }
            }
            if (PROC_HS_INSURANCE_FUND.Rows.Count > 0)
            {
                PROC_HS_INSURANCE_FUND = PROC_HS_INSURANCE_FUND.DefaultView.ToTable(true);
                mst_profit_center = mst_profit_center.DefaultView.ToTable(true);
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


            _recIns.Database.Tables["PROC_HS_INSURANCE_FUND"].SetDataSource(PROC_HS_INSURANCE_FUND);
            _recIns.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _recIns.Database.Tables["mst_com"].SetDataSource(mst_com);
            _recIns.Database.Tables["param"].SetDataSource(param);



            foreach (object repOp in _recIns.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptSrWise")
                    {
                        ReportDocument subRepDoc = _recIns.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["PROC_HS_INSURANCE_FUND"].SetDataSource(PROC_HS_INSURANCE_FUND);
                        subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);


                    }
                    if (_cs.SubreportName == "rptChannel")
                    {
                        ReportDocument subRepDoc = _recIns.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["PROC_HS_INSURANCE_FUND"].SetDataSource(PROC_HS_INSURANCE_FUND);
                        subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);


                    }


                }
            }
        }

        public void GiftVoucherPrintReport()
        {// Sanjeewa 23-11-2013
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetTransferedAccountDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            //DataTable TRANSACC = bsObj.CHNLSVC.Sales.GetTransferedAccountDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID);

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

            _TransAcc.Database.Tables["TRANS_ACC"].SetDataSource(GLOB_DataTable);
            _TransAcc.Database.Tables["param"].SetDataSource(param);

        }

        public void HPInsuranceFundReport()
        { //  Nadeeka 11-02-13

            DataTable tmp_user_pc = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            string accNo = default(string);
            accNo = BaseCls.GlbReportDoc;
            DataTable GLOB_HP_CLS_BAL = new DataTable();
            DataTable mst_profit_center = default(DataTable);
            Int16 isSum = 0;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportComp, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_HP_CLS_BAL = new DataTable();

                    TMP_HP_CLS_BAL = bsObj.CHNLSVC.Sales.GPInsuranceFundReport(Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbUserID, BaseCls.GlbReportComp, drow["tpl_pc"].ToString(), isSum);
                    GLOB_HP_CLS_BAL.Merge(TMP_HP_CLS_BAL);

                    //lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                }
            }


            DataTable mst_com = default(DataTable);

            foreach (DataRow row in GLOB_HP_CLS_BAL.Rows)
            {
                int index = GLOB_HP_CLS_BAL.Rows.IndexOf(row);
                if (index == 0)
                {
                    mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbReportComp, string.Empty);
                    mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);
                }
            }
            //if (PROC_HS_INSURANCE_FUND.Rows.Count > 0)
            //{
            //    PROC_HS_INSURANCE_FUND = PROC_HS_INSURANCE_FUND.DefaultView.ToTable(true);
            //}

            //if (GLOB_HP_CLS_BAL.Rows.Count > 0)
            //{
            //    GLOB_HP_CLS_BAL = GLOB_HP_CLS_BAL.DefaultView.ToTable(true);
            //}
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportAsAtDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportGroupBy == "Summary" ? "S" : "A";
            param.Rows.Add(dr);


            //   _recInsFund.Database.Tables["PROC_HS_INSURANCE_FUND"].SetDataSource(PROC_HS_INSURANCE_FUND);
            _recInsFund.Database.Tables["GLOB_HP_CLS_BAL"].SetDataSource(GLOB_HP_CLS_BAL);
            _recInsFund.Database.Tables["mst_com"].SetDataSource(mst_com);
            _recInsFund.Database.Tables["param"].SetDataSource(param);
        }

        public void HPReceivable_report()
        { //  Nadeeka 20-jan-2014

            DataTable tmp_user_pc = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            string accNo = default(string);
            accNo = BaseCls.GlbReportDoc;
            DataTable GLB_REC_MOVEMENT = new DataTable();
            DataTable mst_profit_center = default(DataTable);


            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportComp, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_HP_CLS_BAL = new DataTable();
                    TMP_HP_CLS_BAL = bsObj.CHNLSVC.Sales.GetReceivableDetails(Convert.ToDateTime(BaseCls.GlbReportAsAtDate), Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbUserID, BaseCls.GlbReportComp, drow["tpl_pc"].ToString(), BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCatType);
                    GLB_REC_MOVEMENT.Merge(TMP_HP_CLS_BAL);


                }
            }


            DataTable mst_com = default(DataTable);

            foreach (DataRow row in GLB_REC_MOVEMENT.Rows)
            {
                int index = GLB_REC_MOVEMENT.Rows.IndexOf(row);
                if (index == 0)
                {
                    mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbReportComp, string.Empty);
                    mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("repHead", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportAsAtDate;
            dr["todate"] = BaseCls.GlbReportAsAtDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["repHead"] = BaseCls.GlbReportGroupBy;
            param.Rows.Add(dr);


            _recevDet.Database.Tables["GLB_REC_MOVEMENT"].SetDataSource(GLB_REC_MOVEMENT);
            _recevDet.Database.Tables["mst_com"].SetDataSource(mst_com);
            _recevDet.Database.Tables["param"].SetDataSource(param);
        }
        public void HPCashFlowForecastingReport()
        {// Nadeeka 06-02-13
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable(); // Added by Prabhath on 09/11/2013
            DataRow dr;
            string accNo = default(string);
            accNo = BaseCls.GlbReportDoc;
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportComp, BaseCls.GlbUserID); // Added By Prabhath on 09/11/2013
            DataTable PROC_HS_FORECASTING = new DataTable(); // Added by Prabhath on 09/11/2013
            DataTable mst_profit_center = default(DataTable);
            foreach (DataRow _r in tmp_user_pc.Rows)// Added by Prabhath on 09/11/2013
            {
                //DataTable PROC_HS_FORECASTING = bsObj.CHNLSVC.Sales.HPCashFlowForecastingReport(Convert.ToDateTime(BaseCls.GlbReportFromDate), BaseCls.GlbUserID, BaseCls.GlbReportComp);
                DataTable _temptbl = bsObj.CHNLSVC.Sales.HPCashFlowForecastingReport(Convert.ToDateTime(BaseCls.GlbReportFromDate), BaseCls.GlbUserID, BaseCls.GlbReportComp, _r.Field<string>("tpl_pc")); // Added by Prabhath on 09/11/2013
                PROC_HS_FORECASTING.Merge(_temptbl);
            }
            DataTable mst_com = default(DataTable);

            foreach (DataRow row in PROC_HS_FORECASTING.Rows)
            {
                int index = PROC_HS_FORECASTING.Rows.IndexOf(row);
                if (index == 0)
                {
                    mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(row["GCF_COM"].ToString(), string.Empty);
                    mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(row["GCF_COM"].ToString());
                }
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

            if (BaseCls.GlbReportName == "HP_CashFlowForecastingReport.rpt")
            {
                _recCashFlow.Database.Tables["PROC_HS_FORECASTING"].SetDataSource(PROC_HS_FORECASTING);
                _recCashFlow.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                _recCashFlow.Database.Tables["mst_com"].SetDataSource(mst_com);
                _recCashFlow.Database.Tables["param"].SetDataSource(param);
            }
            else
            {
                _recCashFlowSum.Database.Tables["PROC_HS_FORECASTING"].SetDataSource(PROC_HS_FORECASTING);
                _recCashFlowSum.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                _recCashFlowSum.Database.Tables["mst_com"].SetDataSource(mst_com);
                _recCashFlowSum.Database.Tables["param"].SetDataSource(param);
            }

        }
        public void HPCReditDebitReport()
        {// Nadeeka 06-02-13
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataTable PROC_HS_CR_DR_REPORT = new DataTable();
            DataRow dr;



            //  DataTable mst_profit_center = default(DataTable);
            // DataTable PROC_HS_CR_DR_REPORT = bsObj.CHNLSVC.Sales.HPDebitCreditRep(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbReportComp);
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_HS_CR_DR_REPORT = new DataTable();

                    tmp_HS_CR_DR_REPORT = bsObj.CHNLSVC.MsgPortal.HPDebitCreditRep(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());

                    PROC_HS_CR_DR_REPORT.Merge(tmp_HS_CR_DR_REPORT);

                }
            }

            DataTable mst_com = default(DataTable);

            foreach (DataRow row in PROC_HS_CR_DR_REPORT.Rows)
            {
                int index = PROC_HS_CR_DR_REPORT.Rows.IndexOf(row);
                if (index == 0)
                {
                    //  mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(row["HCD_COM"].ToString(), string.Empty);
                    mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(row["HCD_COM"].ToString());
                }
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


            _debCrd.Database.Tables["PROC_HS_CR_DR_REPORT"].SetDataSource(PROC_HS_CR_DR_REPORT);
            //_debCrd.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _debCrd.Database.Tables["mst_com"].SetDataSource(mst_com);
            _debCrd.Database.Tables["param"].SetDataSource(param);


        }
        public void HPExcessShortReport()
        {// Nadeeka 22-02-13
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataTable PROC_HS_EXCESS_SHORT_REPORT = new DataTable();
            DataRow dr;



            //  DataTable mst_profit_center = default(DataTable);
            // DataTable PROC_HS_EXCESS_SHORT_REPORT = bsObj.CHNLSVC.Sales.HPExcessShortReport(Convert.ToDateTime(BaseCls.GlbReportFromDate), BaseCls.GlbUserID, BaseCls.GlbReportComp);
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_HS_EXCESS_SHORT_REPORT = new DataTable();

                    tmp_HS_EXCESS_SHORT_REPORT = bsObj.CHNLSVC.Sales.HPExcessShortReport(Convert.ToDateTime(BaseCls.GlbReportFromDate), BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());

                    PROC_HS_EXCESS_SHORT_REPORT.Merge(tmp_HS_EXCESS_SHORT_REPORT);

                }
            }
            DataTable mst_com = default(DataTable);

            foreach (DataRow row in PROC_HS_EXCESS_SHORT_REPORT.Rows)
            {
                int index = PROC_HS_EXCESS_SHORT_REPORT.Rows.IndexOf(row);
                if (index == 0)
                {
                    //  mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(row["HCD_COM"].ToString(), string.Empty);
                    mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(row["HEX_COM"].ToString());
                }
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


            _exShort.Database.Tables["PROC_HS_EXCESS_SHORT_REPORT"].SetDataSource(PROC_HS_EXCESS_SHORT_REPORT);
            _exShort.Database.Tables["mst_com"].SetDataSource(mst_com);
            _exShort.Database.Tables["param"].SetDataSource(param);


        }
        public void UnusedReceiptBookReport()
        {// Nadeeka 22-02-13
            DataTable param = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataTable UnusedReceiptReport = new DataTable();

            DataRow dr;


            // DataTable UnusedReceiptReport = bsObj.CHNLSVC.Sales.UnusedReceiptReport(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_UnusedReceiptReport = new DataTable();

                    tmp_UnusedReceiptReport = bsObj.CHNLSVC.MsgPortal.UnusedReceiptReport(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());

                    UnusedReceiptReport.Merge(tmp_UnusedReceiptReport);

                }
            }
            DataTable mst_com = default(DataTable);


            mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);
            mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbReportComp, string.Empty);


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


            _unuRec.Database.Tables["UnusedReceiptReport"].SetDataSource(UnusedReceiptReport);
            _unuRec.Database.Tables["mst_com"].SetDataSource(mst_com);
            _unuRec.Database.Tables["param"].SetDataSource(param);
            _unuRec.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);

        }
        public void ClosedAccountsDetails()
        {// Nadeeka 29-02-13
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataTable GLB_TEMP_CLS_ACCOUNT = new DataTable();

            DataRow dr;



            DataTable mst_profit_center = default(DataTable);
            //DataTable GLB_TEMP_CLS_ACCOUNT = bsObj.CHNLSVC.Sales.ProcessClosedAccounts(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportComp, drow["tpl_pc"].ToString());
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_TEMP_CLS_ACCOUNT = new DataTable();

                    tmp_TEMP_CLS_ACCOUNT = bsObj.CHNLSVC.MsgPortal.ProcessClosedAccounts(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());

                    GLB_TEMP_CLS_ACCOUNT.Merge(tmp_TEMP_CLS_ACCOUNT);

                }
            }
            DataTable mst_com = default(DataTable);
            if (GLB_TEMP_CLS_ACCOUNT.Rows.Count > 0)
            {
                GLB_TEMP_CLS_ACCOUNT = GLB_TEMP_CLS_ACCOUNT.DefaultView.ToTable(true);
            }

            mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbReportComp, string.Empty);
            mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);
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


            _clsAcc.Database.Tables["GLB_TEMP_CLS_ACCOUNT"].SetDataSource(GLB_TEMP_CLS_ACCOUNT);
            _clsAcc.Database.Tables["mst_com"].SetDataSource(mst_com);
            _clsAcc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _clsAcc.Database.Tables["param"].SetDataSource(param);


        }
        public void HPMultipleAccounts()
        {// Nadeeka 03-06-13
            DataTable param = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataTable glb_hp_multiple_acc = new DataTable();

            DataRow dr;

            // DataTable glb_hp_multiple_acc = bsObj.CHNLSVC.Sales.ProcessHPMultipleAccounts(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbReportComp, BaseCls.GlbReportScheme, BaseCls.GlbReportCusId, BaseCls.GlbReportCusAccBal, drow["tpl_pc"].ToString());
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_hp_multiple_acc = new DataTable();

                    tmp_hp_multiple_acc = bsObj.CHNLSVC.MsgPortal.ProcessHPMultipleAccounts(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbReportComp, BaseCls.GlbReportScheme, BaseCls.GlbReportCusId, BaseCls.GlbReportCusAccBal, drow["tpl_pc"].ToString());

                    glb_hp_multiple_acc.Merge(tmp_hp_multiple_acc);

                }
            }
            DataTable mst_com = default(DataTable);


            mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);
            mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbReportComp, string.Empty);


            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("scheme", typeof(string));
            param.Columns.Add("cusid", typeof(string));
            param.Columns.Add("cusBal", typeof(Double));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportAsAtDate;
            dr["todate"] = BaseCls.GlbReportAsAtDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["scheme"] = BaseCls.GlbReportScheme == "" ? "ALL" : BaseCls.GlbReportScheme;
            dr["cusid"] = BaseCls.GlbReportCusId == "" ? "ALL" : BaseCls.GlbReportCusId;
            dr["cusBal"] = BaseCls.GlbReportCusAccBal;
            param.Rows.Add(dr);

            glb_hp_multiple_acc = glb_hp_multiple_acc.DefaultView.ToTable(true);

            _mulAcc.Database.Tables["glb_hp_multiple_acc"].SetDataSource(glb_hp_multiple_acc);
            _mulAcc.Database.Tables["mst_com"].SetDataSource(mst_com);
            _mulAcc.Database.Tables["param"].SetDataSource(param);
            _mulAcc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);


            foreach (object repOp in _mulAcc.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "sum")
                    {
                        ReportDocument subRepDoc = _mulAcc.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["glb_hp_multiple_acc"].SetDataSource(glb_hp_multiple_acc);
                        subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                    }

                }
            }

        }
        public void HP_InsArrears()
        {// Nadeeka 12-06-13
            DataTable param = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable PROC_HS_INSURANCE_ARREARS = new DataTable();

            DataRow dr;

            //kapila modified on 28/7/2016
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_hp_multiple_acc = new DataTable();

                    tmp_hp_multiple_acc = bsObj.CHNLSVC.MsgPortal.ProcessVehicleInsuranceArrears(BaseCls.GlbUserID, BaseCls.GlbReportComp, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), drow["tpl_pc"].ToString());

                    PROC_HS_INSURANCE_ARREARS.Merge(tmp_hp_multiple_acc);

                }
            }

            // DataTable PROC_HS_INSURANCE_ARREARS = bsObj.CHNLSVC.Sales.ProcessVehicleInsuranceArrears(BaseCls.GlbUserID, BaseCls.GlbReportComp, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate));

            DataTable glb_hp_vehi_ins_det = bsObj.CHNLSVC.Sales.ProcessVehicleInsuranceArrearsDet();

            DataTable mst_com = default(DataTable);


            mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);
            mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbReportComp, string.Empty);


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


            _HPInsArr.Database.Tables["PROC_HS_INSURANCE_ARREARS"].SetDataSource(PROC_HS_INSURANCE_ARREARS);
            _HPInsArr.Database.Tables["mst_com"].SetDataSource(mst_com);
            _HPInsArr.Database.Tables["param"].SetDataSource(param);
            _HPInsArr.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);


            foreach (object repOp in _HPInsArr.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rec")
                    {
                        ReportDocument subRepDoc = _HPInsArr.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["glb_hp_vehi_ins_det"].SetDataSource(glb_hp_vehi_ins_det);

                    }

                }
            }

        }

        public void HP_InsuAgreementPrint()
        {
            DataTable GLB_INS_AGREE = bsObj.CHNLSVC.Sales.GetInsuranceAgreement(BaseCls.GlbReportDoc);
            _HPInsAgree.Database.Tables["GLB_INS_AGREE"].SetDataSource(GLB_INS_AGREE);
        }
        public void HP_ReceiptPrint_Add()
        {// Nadeeka 12-06-13
            //    DataTable param = new DataTable();
            //     DataTable mst_profit_center = new DataTable();

            //     DataRow dr;

            DataTable GLB_HP_RECEIPT_PRINT = bsObj.CHNLSVC.MsgPortal.ProcessHPReceiptPrint(BaseCls.GlbReportDoc);
            DataTable GLB_HP_RECEIPT_PAYMENTS = bsObj.CHNLSVC.MsgPortal.ProcessHPReceiptPrintPayment(BaseCls.GlbReportDoc);
            DataTable GLB_HP_RECEIPT_PAYTYPE = bsObj.CHNLSVC.MsgPortal.ProcessHPReceiptPrintPayMode(BaseCls.GlbReportDoc);
            //        DataTable mst_com = default(DataTable);


            //   mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);
            //   mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbReportComp, string.Empty);


            //  param.Rows.Add(dr);


            _HPRecAdd.Database.Tables["GLB_HP_RECEIPT_PRINT"].SetDataSource(GLB_HP_RECEIPT_PRINT);

            foreach (object repOp in _HPRecAdd.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "payments")
                    {
                        ReportDocument subRepDoc = _HPRecAdd.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["GLB_HP_RECEIPT_PAYMENTS"].SetDataSource(GLB_HP_RECEIPT_PAYMENTS);

                    }
                    if (_cs.SubreportName == "paymode")
                    {
                        ReportDocument subRepDoc = _HPRecAdd.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["GLB_HP_RECEIPT_PAYTYPE"].SetDataSource(GLB_HP_RECEIPT_PAYTYPE);

                    }
                    if (_cs.SubreportName == "chq")
                    {
                        ReportDocument subRepDoc = _HPRecAdd.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["GLB_HP_RECEIPT_PAYTYPE"].SetDataSource(GLB_HP_RECEIPT_PAYTYPE);

                    }
                }
            }

        }

        public void HPRecPrint_Direct()
        {// Sanjeewa 11-04-2017
            PrintDocument pdoc = null;
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Tahoma", 8);

            pd.Document = pdoc;

            DialogResult result = pd.ShowDialog();
            if (result == DialogResult.OK)
            {
                DialogResult dialogResult = MessageBox.Show("Insert Receipt to the printer & Press Ok.", "HP Receipt Print", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dialogResult == DialogResult.OK)
                {
                    BaseCls.GlbReportnoofDays = 1;
                    pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage_HPRec);
                    pdoc.Print();
                }
            }

        }

        public void pdoc_PrintPage_HPRec(object sender, PrintPageEventArgs e)
        {
            DataTable GLB_HP_RECEIPT_PRINT = bsObj.CHNLSVC.MsgPortal.ProcessHPReceiptPrint(BaseCls.GlbReportDoc);
            DataTable GLB_HP_RECEIPT_PAYMENTS = bsObj.CHNLSVC.MsgPortal.ProcessHPReceiptPrintPayment(BaseCls.GlbReportDoc);
            DataTable GLB_HP_RECEIPT_PAYTYPE = bsObj.CHNLSVC.MsgPortal.ProcessHPReceiptPrintPayMode(BaseCls.GlbReportDoc);

            Graphics graphics = e.Graphics;
            Font font = new Font("Tahoma", 8);
            float fontHeight = font.GetHeight();
            int startX = 0;
            int startY = 0;
            int OffsetY = 8;
            int OffsetX = 82;

            if (GLB_HP_RECEIPT_PRINT.Rows.Count > 0)
            {
                foreach (DataRow drowh in GLB_HP_RECEIPT_PRINT.Rows)
                {
                    startX = 0;
                    startY = 2;

                    if (drowh["mc_anal18"].ToString() != "")
                    {
                        startX = 300;
                        graphics.DrawString(drowh["mc_anal18"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        startX = startX + (drowh["mc_anal18"].ToString().Length * 6) + 20;
                        graphics.DrawString(drowh["mc_tax3"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    }
                    startY = startY + 17;

                    startX = 80;
                    graphics.DrawString("An. " + drowh["hpr_acc"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    startX = 270;
                    graphics.DrawString(drowh["hpr_acc"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    startX = 540;
                    graphics.DrawString(drowh["hpr_recno"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                    startY = startY + 20;
                    startX = 50;
                    graphics.DrawString("Rn. " + drowh["hpr_recno"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    startX = 270;
                    graphics.DrawString(drowh["hpr_name"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    startX = 540;
                    graphics.DrawString(Convert.ToDateTime(drowh["hpr_date"]).ToString("dd/MMM/yyyy"), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                    startY = startY + 20;
                    startX = 350;
                    graphics.DrawString(String.Format("{0:#,##0.00}", Convert.ToDecimal(drowh["hpr_hval"].ToString())) + "(LKR)", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    startX = 540;
                    graphics.DrawString(drowh["hpr_loc"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                    startY = startY + 40;
                    startX = 100;
                    graphics.DrawString(Convert.ToDateTime(drowh["hpr_date"]).ToString("dd/MMM/yyyy"), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                    startY = startY + 20;
                    startX = 80;
                    graphics.DrawString("Ml. " + drowh["hpr_model"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    startX = 200;
                    graphics.DrawString(drowh["hpr_model"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    startX = 480;
                    graphics.DrawString(drowh["hpr_serial"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                    startY = startY + 60;
                    startX = 20;
                    graphics.DrawString("Sn. " + drowh["hpr_serial"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                    startY = startY + 254;
                    startX = 200;
                    int i = 0;
                    for (i = 1; i <= (drowh["hpr_remark"].ToString().Length / 60); i++)
                    {
                        startY = startY + 12;
                        graphics.DrawString(drowh["hpr_remark"].ToString().Substring(((i - 1) * 60), 60), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    }
                    startY = startY + 12;
                    graphics.DrawString(drowh["hpr_remark"].ToString().Substring(((i - 1) * 60), (drowh["hpr_remark"].ToString().Length - ((i - 1) * 60))), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                    startY = 400;
                    startX = 20;
                    graphics.DrawString(drowh["hpr_prefix"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    startX = 100;
                    graphics.DrawString(drowh["hpr_manref"].ToString(), new Font("Tahoma", 11),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                }
            }

            if (GLB_HP_RECEIPT_PAYMENTS.Rows.Count > 0)
            {
                startY = 200;
                decimal _Total = 0;
                foreach (DataRow drowp in GLB_HP_RECEIPT_PAYMENTS.Rows)
                {
                    string _desc = drowp["hpr_desc"].ToString() == "EARLY CLOSING DISCOUNT" ? "ECD" : "";

                    startY = startY + 20;
                    startX = 20;
                    graphics.DrawString(_desc, new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    startX = 110;
                    graphics.DrawString(String.Format("{0:#,##0.00}", Convert.ToDecimal(drowp["hpr_amt"].ToString())), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    _Total = _Total + Convert.ToDecimal(drowp["hpr_amt"].ToString());
                    startX = 210;
                    graphics.DrawString(drowp["hpr_desc"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    startX = 360;
                    graphics.DrawString(String.Format("{0:#,##0.00}", Convert.ToDecimal(drowp["hpr_amt"].ToString())), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                }

                startY = startY + 20;
                startX = 110;
                graphics.DrawString("------------------", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX - 20, startY + OffsetY - 7);
                graphics.DrawString(String.Format("{0:#,##0.00}", _Total), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                startX = 270;
                graphics.DrawString("LKR", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                startX = 360;
                graphics.DrawString("------------------", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX - 20, startY + OffsetY - 7);
                graphics.DrawString(String.Format("{0:#,##0.00}", _Total), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            }

            if (GLB_HP_RECEIPT_PAYTYPE.Rows.Count > 0)
            {
                startY = 200;
                startX = 465;
                graphics.DrawString("Pay Mode", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                startX = startX + 50;
                graphics.DrawString("Amount", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                startX = startX + 50;
                graphics.DrawString("Bank", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                startX = startX + 45;
                graphics.DrawString("Ref. No", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                startX = startX + 50;
                graphics.DrawString("C. Date", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                foreach (DataRow drowpm in GLB_HP_RECEIPT_PAYTYPE.Rows)
                {
                    startY = startY + 20;
                    startX = 465;
                    graphics.DrawString(drowpm["hpr_mode"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    startX = startX + 50;
                    graphics.DrawString(String.Format("{0:#,##0.00}", Convert.ToDecimal(drowpm["hpr_amt"].ToString())), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    startX = startX + 50;
                    graphics.DrawString(drowpm["hpr_bank"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    startX = startX + 45;
                    graphics.DrawString(drowpm["hpr_ref"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    startX = startX + 50;
                    if (drowpm["hpr_mode"].ToString() == "CHEQUE")
                    {
                        graphics.DrawString(Convert.ToDateTime(drowpm["hpr_date"]).ToString("dd/MMM/yyyy"), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    }
                }

                startY = 325;
                foreach (DataRow drowpm in GLB_HP_RECEIPT_PAYTYPE.Rows)
                {
                    if (drowpm["hpr_mode"].ToString() == "CHEQUE")
                    {
                        string _pmode = drowpm["hpr_mode"].ToString() == "CRCD" ? "Cr." : drowpm["hpr_mode"].ToString() == "CHEQUE" ? "Cn." : "";

                        startY = startY + 20;
                        startX = 20;
                        graphics.DrawString(_pmode + "  " + drowpm["hpr_ref"].ToString() + "  Cd.  " + Convert.ToDateTime(drowpm["hpr_date"]).ToString("dd/MMM/yyyy"), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    }
                }
            }
        }

        public Boolean checkIsDirectPrint()
        {
            HpSystemParameters _SystemPara = new HpSystemParameters();
            _SystemPara = bsObj.CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "DIRPRT_REC", DateTime.Now.Date);
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

        public void HP_ReceiptPrint()
        {// Nadeeka 12-06-13
            //    DataTable param = new DataTable();
            //     DataTable mst_profit_center = new DataTable();

            //     DataRow dr;

            DataTable GLB_HP_RECEIPT_PRINT = bsObj.CHNLSVC.MsgPortal.ProcessHPReceiptPrint(BaseCls.GlbReportDoc);
            DataTable GLB_HP_RECEIPT_PAYMENTS = bsObj.CHNLSVC.MsgPortal.ProcessHPReceiptPrintPayment(BaseCls.GlbReportDoc);
            DataTable GLB_HP_RECEIPT_PAYTYPE = bsObj.CHNLSVC.MsgPortal.ProcessHPReceiptPrintPayMode(BaseCls.GlbReportDoc);
            //        DataTable mst_com = default(DataTable);


            //   mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);
            //   mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbReportComp, string.Empty);


            //  param.Rows.Add(dr);


            _HPRec.Database.Tables["GLB_HP_RECEIPT_PRINT"].SetDataSource(GLB_HP_RECEIPT_PRINT);

            foreach (object repOp in _HPRec.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "payments")
                    {
                        ReportDocument subRepDoc = _HPRec.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["GLB_HP_RECEIPT_PAYMENTS"].SetDataSource(GLB_HP_RECEIPT_PAYMENTS);

                    }
                    if (_cs.SubreportName == "paymode")
                    {
                        ReportDocument subRepDoc = _HPRec.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["GLB_HP_RECEIPT_PAYTYPE"].SetDataSource(GLB_HP_RECEIPT_PAYTYPE);

                    }
                    if (_cs.SubreportName == "chq")
                    {
                        ReportDocument subRepDoc = _HPRec.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["GLB_HP_RECEIPT_PAYTYPE"].SetDataSource(GLB_HP_RECEIPT_PAYTYPE);

                    }
                }
            }

        }
        public void CollectionBonusNew()
        {
            string _Err = string.Empty;
            Cursor.Current = Cursors.WaitCursor;
            string _filePath = bsObj.CHNLSVC.Sales.GetCollectionBonusDetailsRepNew(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportAsAtDate), 0, "", BaseCls.GlbUserComCode, out _Err);

            if (!string.IsNullOrEmpty(_Err))
            {
                MessageBox.Show(_Err.ToString(), "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(_filePath);
                p.Start();
            }

            Cursor.Current = Cursors.Default;
        }
        public void CollectionBonus()
        {

            DataTable HPT_COL_BONUS_DET = new DataTable();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportComp, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_HP_CLS_BAL = new DataTable();

                    TMP_HP_CLS_BAL = bsObj.CHNLSVC.Sales.GetCollectionBonusDetailsRep(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportAsAtDate), 0, drow["tpl_pc"].ToString());
                    HPT_COL_BONUS_DET.Merge(TMP_HP_CLS_BAL);


                }
            }




            Double x = 0;
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = (Excel.Workbook)excelApp.Workbooks.Add(Missing.Value);
            Excel.Worksheet worksheet;
            Excel.Worksheet worksheet2;
            // Opening excel file

            string _repPath = "";
            MasterCompany _masterComp = null;
            _masterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

            if (_masterComp != null)
            {
                _repPath = _masterComp.Mc_anal16;

            }

            workbook.SaveAs(_repPath + "ColBonus" + BaseCls.GlbUserID + ".xls", Excel.XlPlatform.xlWindows, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            workbook.Close(true, Missing.Value, Missing.Value);
            workbook = excelApp.Workbooks.Open(_repPath + "ColBonus" + BaseCls.GlbUserID + ".xls", 0, false, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            // Get first Worksheet
            worksheet = (Excel.Worksheet)workbook.Sheets.get_Item(1);

            ((Excel.Range)worksheet.Cells[1, "B"]).Value2 = "AS AT  " + Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date;

            ((Excel.Range)worksheet.Cells[1, "A"]).Value2 = "COLLECTION BONUS";



            ((Excel.Range)worksheet.Cells[1, "C"]).Value2 = "User :" + BaseCls.GlbUserID + " On " + DateTime.Now;

            x = 2;

            ((Excel.Range)worksheet.Cells[x, "A"]).Value2 = "COMPANY";
            ((Excel.Range)worksheet.Cells[x, "B"]).Value2 = "PRFOT CENTER";
            ((Excel.Range)worksheet.Cells[x, "C"]).Value2 = "ACCOUNT NO";
            ((Excel.Range)worksheet.Cells[x, "D"]).Value2 = "MONTHLY DUE";
            ((Excel.Range)worksheet.Cells[x, "E"]).Value2 = "PREVIOUS ARREARS";
            ((Excel.Range)worksheet.Cells[x, "F"]).Value2 = "CURRENT ARREARS";
            ((Excel.Range)worksheet.Cells[x, "G"]).Value2 = "TOTAL COLLECTION";
            ((Excel.Range)worksheet.Cells[x, "H"]).Value2 = "REVERSAL";
            ((Excel.Range)worksheet.Cells[x, "I"]).Value2 = "TOTAL REMITANCE";
            ((Excel.Range)worksheet.Cells[x, "J"]).Value2 = "SUP COLLECTION";
            ((Excel.Range)worksheet.Cells[x, "K"]).Value2 = "GRACE SETTLEMT ";
            ((Excel.Range)worksheet.Cells[x, "L"]).Value2 = "GRACE COLLECTION";
            ((Excel.Range)worksheet.Cells[x, "M"]).Value2 = "BONUS DATE";
            ((Excel.Range)worksheet.Cells[x, "N"]).Value2 = "SUP DATE";
            ((Excel.Range)worksheet.Cells[x, "O"]).Value2 = "GRACE DATE";
            ((Excel.Range)worksheet.Cells[x, "P"]).Value2 = "CREATED BY";
            ((Excel.Range)worksheet.Cells[x, "Q"]).Value2 = "CREATE DATE";
            ((Excel.Range)worksheet.Cells[x, "R"]).Value2 = "SUP ADJUSTMENTS";
            ((Excel.Range)worksheet.Cells[x, "S"]).Value2 = "GRACE ADJUSTMENTS";
            ((Excel.Range)worksheet.Cells[x, "T"]).Value2 = "SCHEME";
            foreach (DataRow row in HPT_COL_BONUS_DET.Rows)
            {

                x += 1;

                ((Excel.Range)worksheet.Cells[x, "A"]).Value2 = row["HCBDT_COM"].ToString();
                ((Excel.Range)worksheet.Cells[x, "B"]).Value2 = row["HCBDT_PC"].ToString();
                ((Excel.Range)worksheet.Cells[x, "C"]).Value2 = row["HCBDT_AC"].ToString();
                ((Excel.Range)worksheet.Cells[x, "D"]).Value2 = row["HCBDT_MON_DUE"].ToString();
                ((Excel.Range)worksheet.Cells[x, "E"]).Value2 = row["HCBDT_PRV_ARS"].ToString();
                ((Excel.Range)worksheet.Cells[x, "F"]).Value2 = row["HCBDT_CUR_ARS"].ToString();
                ((Excel.Range)worksheet.Cells[x, "G"]).Value2 = row["HCBDT_TOT_COL"].ToString();
                ((Excel.Range)worksheet.Cells[x, "H"]).Value2 = row["HCBDT_REV"].ToString();
                ((Excel.Range)worksheet.Cells[x, "I"]).Value2 = row["HCBDT_TOT_REMIT"].ToString();
                ((Excel.Range)worksheet.Cells[x, "J"]).Value2 = row["HCBDT_SUP_COL"].ToString();
                ((Excel.Range)worksheet.Cells[x, "K"]).Value2 = row["HCBDT_GRCE_SETT"].ToString();
                ((Excel.Range)worksheet.Cells[x, "L"]).Value2 = row["HCBDT_GRCE_COL"].ToString();
                ((Excel.Range)worksheet.Cells[x, "M"]).Value2 = row["HCBDT_BONUS_DT"].ToString();
                ((Excel.Range)worksheet.Cells[x, "N"]).Value2 = row["HCBDT_SUP_DT"].ToString();
                ((Excel.Range)worksheet.Cells[x, "O"]).Value2 = row["HCBDT_GRCE_DT"].ToString();
                ((Excel.Range)worksheet.Cells[x, "P"]).Value2 = row["HCBDT_CRE_BY"].ToString();
                ((Excel.Range)worksheet.Cells[x, "Q"]).Value2 = row["HCBDT_CRE_DT"].ToString();
                ((Excel.Range)worksheet.Cells[x, "R"]).Value2 = row["HCBDT_SUP_ADJ"].ToString();
                ((Excel.Range)worksheet.Cells[x, "S"]).Value2 = row["HCBDT_GRCE_ADJ"].ToString();
                ((Excel.Range)worksheet.Cells[x, "T"]).Value2 = row["HCBDT_SCHEME"].ToString();

            }

            Excel.Range rng = (Excel.Range)worksheet.get_Range("A2", "T2");
            rng.RowHeight = 25.5;
            rng.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
            rng.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.Weight = 1d;
            rng.Font.Bold = true;
            rng.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            rng.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);

            workbook.Save();
            workbook.Close(0, 0, 0);

            excelApp.Quit();
            Marshal.ReleaseComObject(worksheet);
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(excelApp);

            Excel.Application excelApp1 = new Excel.Application();
            excelApp1.Visible = true;

            string workbookPath = _repPath + "ColBonus" + BaseCls.GlbUserID + ".xls";
            Excel.Workbook excelWorkbook = excelApp1.Workbooks.Open(workbookPath,
                    0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);
        }
        public void CollectionBonus_SUMMARY()
        {

            string _repPath = "";
            MasterCompany _masterComp = null;
            _masterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
            if (_masterComp != null)
            {
                _repPath = _masterComp.Mc_anal16;

            }

            DataTable HPT_COL_BONUS_DET = new DataTable();

            HPT_COL_BONUS_DET = bsObj.CHNLSVC.Sales.GetCollectionBonusDetailsRep(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportAsAtDate), 1, string.Empty);

            //tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportComp, BaseCls.GlbUserID);

            //if (tmp_user_pc.Rows.Count > 0)
            //{
            //    foreach (DataRow drow in tmp_user_pc.Rows)
            //    {
            //        DataTable TMP_HP_CLS_BAL = new DataTable();

            //        TMP_HP_CLS_BAL = bsObj.CHNLSVC.Sales.GetCollectionBonusDetailsRep(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportAsAtDate), 1, drow["tpl_pc"].ToString());
            //        HPT_COL_BONUS_DET.Merge(TMP_HP_CLS_BAL);


            //    }
            //}



            Double x = 0;
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = (Excel.Workbook)excelApp.Workbooks.Add(Missing.Value);
            Excel.Worksheet worksheet;
            Excel.Worksheet worksheet2;
            // Opening excel file

            workbook.SaveAs(_repPath + "ColBonussummary" + BaseCls.GlbUserID + ".xls", Excel.XlPlatform.xlWindows, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            workbook.Close(true, Missing.Value, Missing.Value);
            workbook = excelApp.Workbooks.Open(_repPath + "ColBonussummary" + BaseCls.GlbUserID + ".xls", 0, false, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            // Get first Worksheet
            worksheet = (Excel.Worksheet)workbook.Sheets.get_Item(1);

            ((Excel.Range)worksheet.Cells[1, "B"]).Value2 = "AS AT  " + Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date;

            ((Excel.Range)worksheet.Cells[1, "A"]).Value2 = "COLLECTION BONUS SUMMARY";



            ((Excel.Range)worksheet.Cells[1, "C"]).Value2 = "User :" + BaseCls.GlbUserID + " On " + DateTime.Now;

            x = 2;

            ((Excel.Range)worksheet.Cells[x, "A"]).Value2 = "COMPANY";
            ((Excel.Range)worksheet.Cells[x, "B"]).Value2 = "PRFOT CENTER";
            // ((Excel.Range)worksheet.Cells[x, "C"]).Value2 = "ACCOUNT NO";
            ((Excel.Range)worksheet.Cells[x, "C"]).Value2 = "MONTHLY DUE";
            ((Excel.Range)worksheet.Cells[x, "D"]).Value2 = "PREVIOUS ARREARS";
            ((Excel.Range)worksheet.Cells[x, "E"]).Value2 = "CURRENT ARREARS";
            ((Excel.Range)worksheet.Cells[x, "F"]).Value2 = "TOTAL COLLECTION";
            ((Excel.Range)worksheet.Cells[x, "G"]).Value2 = "REVERSAL";
            ((Excel.Range)worksheet.Cells[x, "H"]).Value2 = "TOTAL REMITANCE";
            ((Excel.Range)worksheet.Cells[x, "I"]).Value2 = "SUP COLLECTION";
            ((Excel.Range)worksheet.Cells[x, "J"]).Value2 = "GRACE SETTLEMT ";
            ((Excel.Range)worksheet.Cells[x, "K"]).Value2 = "GRACE COLLECTION";
            ((Excel.Range)worksheet.Cells[x, "L"]).Value2 = "BONUS DATE";
            ((Excel.Range)worksheet.Cells[x, "M"]).Value2 = "SUP DATE";
            ((Excel.Range)worksheet.Cells[x, "N"]).Value2 = "GRACE DATE";
            //  ((Excel.Range)worksheet.Cells[x, "P"]).Value2 = "CREATED BY";
            //  ((Excel.Range)worksheet.Cells[x, "Q"]).Value2 = "CREATE DATE";
            ((Excel.Range)worksheet.Cells[x, "O"]).Value2 = "SUP ADJUSTMENTS";
            ((Excel.Range)worksheet.Cells[x, "P"]).Value2 = "GRACE ADJUSTMENTS";

            foreach (DataRow row in HPT_COL_BONUS_DET.Rows)
            {

                x += 1;

                ((Excel.Range)worksheet.Cells[x, "A"]).Value2 = row["HCBDT_COM"].ToString();
                ((Excel.Range)worksheet.Cells[x, "B"]).Value2 = row["HCBDT_PC"].ToString();
                //   ((Excel.Range)worksheet.Cells[x, "C"]).Value2 = row["HCBDT_AC"].ToString();
                ((Excel.Range)worksheet.Cells[x, "C"]).Value2 = row["HCBDT_MON_DUE"].ToString();
                ((Excel.Range)worksheet.Cells[x, "D"]).Value2 = row["HCBDT_PRV_ARS"].ToString();
                ((Excel.Range)worksheet.Cells[x, "E"]).Value2 = row["HCBDT_CUR_ARS"].ToString();
                ((Excel.Range)worksheet.Cells[x, "F"]).Value2 = row["HCBDT_TOT_COL"].ToString();
                ((Excel.Range)worksheet.Cells[x, "G"]).Value2 = row["HCBDT_REV"].ToString();
                ((Excel.Range)worksheet.Cells[x, "H"]).Value2 = row["HCBDT_TOT_REMIT"].ToString();
                ((Excel.Range)worksheet.Cells[x, "I"]).Value2 = row["HCBDT_SUP_COL"].ToString();
                ((Excel.Range)worksheet.Cells[x, "J"]).Value2 = row["HCBDT_GRCE_SETT"].ToString();
                ((Excel.Range)worksheet.Cells[x, "K"]).Value2 = row["HCBDT_GRCE_COL"].ToString();
                ((Excel.Range)worksheet.Cells[x, "L"]).Value2 = row["HCBDT_BONUS_DT"].ToString();
                ((Excel.Range)worksheet.Cells[x, "M"]).Value2 = row["HCBDT_SUP_DT"].ToString();
                ((Excel.Range)worksheet.Cells[x, "N"]).Value2 = row["HCBDT_GRCE_DT"].ToString();
                //  ((Excel.Range)worksheet.Cells[x, "P"]).Value2 = row["HCBDT_CRE_BY"].ToString();
                //  ((Excel.Range)worksheet.Cells[x, "Q"]).Value2 = row["HCBDT_CRE_DT"].ToString();
                ((Excel.Range)worksheet.Cells[x, "O"]).Value2 = row["HCBDT_SUP_ADJ"].ToString();
                ((Excel.Range)worksheet.Cells[x, "P"]).Value2 = row["HCBDT_GRCE_ADJ"].ToString();

            }

            Excel.Range rng = (Excel.Range)worksheet.get_Range("A2", "S2");
            rng.RowHeight = 25.5;
            rng.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
            rng.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.Weight = 1d;
            rng.Font.Bold = true;
            rng.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            rng.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);

            workbook.Save();
            workbook.Close(0, 0, 0);

            excelApp.Quit();
            Marshal.ReleaseComObject(worksheet);
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(excelApp);

            Excel.Application excelApp1 = new Excel.Application();
            excelApp1.Visible = true;

            string workbookPath = _repPath + "ColBonussummary" + BaseCls.GlbUserID + ".xls";
            Excel.Workbook excelWorkbook = excelApp1.Workbooks.Open(workbookPath,
                    0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);
        }

        public void RevertStatus()
        {
            string _Err = string.Empty;
            Cursor.Current = Cursors.WaitCursor;
            string _filePath = bsObj.CHNLSVC.MsgPortal.GetRevertStatus(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserComCode, "", BaseCls.GlbUserID, out _Err);
            if (!string.IsNullOrEmpty(_Err))
            {
                MessageBox.Show(_Err.ToString(), "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(_filePath);
                p.Start();
            }

            Cursor.Current = Cursors.Default;
        }

        //Udaya 29.06.2017
        private void InsurancePrint()
        {
            // Nadeeka 26-12-12
            string _accNo = default(string);
            _accNo = BaseCls.GlbReportDoc;

            //  InsuPrints report1 = new InsuPrints();

            salesDetails.Clear();
            salesDetails = bsObj.CHNLSVC.Sales.GetSalesDetails(null, _accNo);

            hpt_cust = bsObj.CHNLSVC.Sales.GetHpAccCustomer(_accNo);
            hpt_insu = bsObj.CHNLSVC.Sales.GetInsurance(_accNo);

            int _numPages = 0;
            DataRow dr3;

            _numPages = bsObj.CHNLSVC.General.GetReprintDocCount(_accNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;
            PRINT_DOC.Rows.Add(dr3);
            //insReport2.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


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
            mst_profit_center.Columns.Add("MPC_TEL", typeof(string));
            mst_profit_center.Columns.Add("MPC_FAX", typeof(string));
            mst_profit_center.Columns.Add("MPC_EMAIL", typeof(string));



            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));



            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));
            mst_com.Columns.Add("MC_ANAL3", typeof(string));
            mst_com.Columns.Add("MC_TAX3", typeof(string));


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


                    dr = mst_com.NewRow();
                    dr["MC_TAX1"] = row["MC_TAX1"].ToString();
                    dr["MC_TAX2"] = row["MC_TAX2"].ToString();
                    dr["MC_CD"] = row["MC_CD"].ToString();
                    dr["MC_DESC"] = row["MC_DESC"].ToString();
                    dr["MC_IT_POWERED"] = row["MC_IT_POWERED"].ToString();
                    dr["MC_ANAL3"] = row["MC_ANAL3"].ToString();
                    dr["MC_TAX3"] = row["MC_TAX3"].ToString();
                    mst_com.Rows.Add(dr);

                    dr = sec_user.NewRow();
                    dr["SE_USR_NAME"] = row["SE_USR_NAME"].ToString();
                    dr["SE_USR_ID"] = row["SE_USR_ID"].ToString();
                    sec_user.Rows.Add(dr);
                };

            }

            //DataTable mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);


            //insReport2.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            //insReport2.Database.Tables["mst_com"].SetDataSource(mst_com);
            //insReport2.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            //insReport2.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
            //insReport2.Database.Tables["hpt_cust"].SetDataSource(hpt_cust);
            //insReport2.Database.Tables["hpt_insu"].SetDataSource(hpt_insu);




            //mst_item = mst_item.DefaultView.ToTable(true);
            //foreach (object repOp in insReport2.ReportDefinition.ReportObjects)
            //{
            //    string _s = repOp.GetType().ToString();
            //    if (_s.ToLower().Contains("subreport"))
            //    {
            //        SubreportObject _cs = (SubreportObject)repOp;
            //        if (_cs.SubreportName == "rptItem")
            //        {
            //            ReportDocument subRepDoc = insReport2.Subreports[_cs.SubreportName];
            //            subRepDoc.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            //            subRepDoc.Database.Tables["mst_item"].SetDataSource(mst_item);


            //        }

            //    }
            //}
        }

        public void InsurancePrint_Direct()
        {
            //Udaya 29.06.2017
            PrintDocument pdoc = null;
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Tahoma", 8);

            pd.Document = pdoc;

            DialogResult result = pd.ShowDialog();
            if (result == DialogResult.OK)
            {
                DialogResult dialogResult = MessageBox.Show("Insert Insurance document to the printer & Press Ok.", "Insurance Print", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dialogResult == DialogResult.OK)
                {
                    InsurancePrint();
                    BaseCls.GlbReportnoofDays = 1;
                    pdoc.PrintPage += new PrintPageEventHandler(pdoc_InsPrintPage);
                    pdoc.Print();
                }
            }
        }
        public void pdoc_InsPrintPage(object sender, PrintPageEventArgs e)
        {
            int startX = 0;
            int startY = 0;
            int OffsetY = 5;
            int OffsetX = 5;
            decimal _val = 0;
            int _OffsetY = 0;
            int _RefNoOffsetY = 0;
            Graphics graphics = e.Graphics;

            #region variables
            Int16 SHOWCOM = (from DataRow drw in PRINT_DOC.Rows select Convert.ToInt16(drw["SHOWCOM"])).FirstOrDefault();
            Int16 NOOFPAGES = (from DataRow drw in PRINT_DOC.Rows select Convert.ToInt16(drw["NOOFPAGES"])).FirstOrDefault();

            string MC_DESC = (from DataRow drw in mst_com.Rows select drw["MC_DESC"] == null ? string.Empty : (string)drw["MC_DESC"]).FirstOrDefault();
            string MC_ANAL3 = (from DataRow drw in mst_com.Rows select (drw["MC_ANAL3"] == null || drw["MC_ANAL3"].ToString() == "") ? string.Empty : (string)drw["MC_ANAL3"]).FirstOrDefault();
            string MC_TAX3 = (from DataRow drw in mst_com.Rows select (drw["MC_TAX3"] == null || drw["MC_TAX3"].ToString() == "") ? string.Empty : (string)drw["MC_TAX3"]).FirstOrDefault();

            string PC_Add = (from DataRow drw in mst_profit_center.Rows select (drw["MPC_ADD_1"] == null || drw["MPC_ADD_1"].ToString() == "") ? string.Empty : (string)drw["MPC_ADD_1"]).FirstOrDefault() + ", " + (from DataRow drw in mst_profit_center.Rows select (drw["MPC_ADD_2"] == null || drw["MPC_ADD_2"].ToString() == "") ? string.Empty : (string)drw["MPC_ADD_2"]).FirstOrDefault();
            string MPC_TEL = (from DataRow drw in mst_profit_center.Rows select (drw["MPC_TEL"] == null || drw["MPC_TEL"].ToString() == "") ? string.Empty : (string)drw["MPC_TEL"]).FirstOrDefault();
            string MPC_FAX = (from DataRow drw in mst_profit_center.Rows select (drw["MPC_FAX"] == null || drw["MPC_FAX"].ToString() == "") ? string.Empty : (string)drw["MPC_FAX"]).FirstOrDefault();
            string MPC_DESC = (from DataRow drw in mst_profit_center.Rows select (drw["MPC_DESC"] == null || drw["MPC_DESC"].ToString() == "") ? string.Empty : (string)drw["MPC_DESC"]).FirstOrDefault();

            string MBE_NAME = (from DataRow drw in mst_busentity.Rows select (drw["MBE_NAME"] == null || drw["MBE_NAME"].ToString() == "") ? string.Empty : (string)drw["MBE_NAME"]).FirstOrDefault();
            string CUS_Add = (from DataRow drw in mst_busentity.Rows select (drw["MBE_ADD1"] == null || drw["MBE_ADD1"].ToString() == "") ? string.Empty : (string)drw["MBE_ADD1"]).FirstOrDefault() + ", " + (from DataRow drw in mst_busentity.Rows select (drw["MBE_ADD2"] == null || drw["MBE_ADD2"].ToString() == "") ? string.Empty : (string)drw["MBE_ADD2"]).FirstOrDefault();

            string HTI_REF = (from DataRow drw in hpt_insu.Rows select (drw["HTI_REF"] == null || drw["HTI_REF"].ToString() == "") ? string.Empty : (string)drw["HTI_REF"]).FirstOrDefault();
            string HTI_ACC_NUM = (from DataRow drw in hpt_insu.Rows select (drw["HTI_ACC_NUM"] == null || drw["HTI_ACC_NUM"].ToString() == "") ? string.Empty : (string)drw["HTI_ACC_NUM"]).FirstOrDefault();
            decimal HTI_INS_VAL = (from DataRow drw in hpt_insu.Rows select Convert.ToDecimal(drw["HTI_INS_VAL"])).FirstOrDefault();
            decimal HTI_VAT_VAL = (from DataRow drw in hpt_insu.Rows select Convert.ToDecimal(drw["HTI_VAT_VAL"])).FirstOrDefault();
            decimal netAmt = (HTI_INS_VAL - HTI_VAT_VAL);

            string SAH_DT = (from DataRow drw in sat_hdr.Rows select (drw["SAH_DT"] == null || drw["SAH_DT"].ToString() == "") ? string.Empty : Convert.ToDateTime(drw["SAH_DT"]).ToShortDateString()).FirstOrDefault();
            #endregion

            #region Header
            OffsetX = 5;
            graphics.DrawString(MC_ANAL3 == null ? string.Empty : MC_ANAL3.ToString() + " CASH MEMO", new Font("Tahoma", 8, FontStyle.Bold),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            OffsetX = Convert.ToInt16(Math.Round((Convert.ToDecimal((10 * 72) / 2) - Convert.ToDecimal((((MC_DESC.ToString().Length) * 72) / 14) / 4)), 0));
            graphics.DrawString(MC_DESC == null ? string.Empty : MC_DESC.ToString(), new Font("Tahoma", 14, FontStyle.Bold),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            OffsetX = 600;
            graphics.DrawString(DateTime.Now.ToString(), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY + 3);

            OffsetY = OffsetY + 10;
            OffsetX = 5;
            if (NOOFPAGES > 0)
            {
                graphics.DrawString("Reprint Copy - ", new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                OffsetX = OffsetX + 80;
                graphics.DrawString(NOOFPAGES.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY + 3);
            }
            OffsetX = 600;
            graphics.DrawString(MC_TAX3 == null ? string.Empty : MC_TAX3.ToString(), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY + 3);

            OffsetY = OffsetY + 10;
            OffsetX = Convert.ToInt16(Math.Round((Convert.ToDecimal((10 * 72) / 2) - Convert.ToDecimal((((PC_Add.ToString().Length) * 72) / 8) / 4)), 0));
            graphics.DrawString(PC_Add == null ? string.Empty : PC_Add.ToString(), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            OffsetY = OffsetY + 10;
            OffsetX = Convert.ToInt16(Math.Round((Convert.ToDecimal((10 * 72) / 2) - Convert.ToDecimal((((("Tel - " + (MPC_TEL == null ? string.Empty : MPC_TEL.ToString()) + " Fax - " + (MPC_FAX == null ? string.Empty : MPC_FAX.ToString())).Length) * 72) / 8) / 4)), 0));
            graphics.DrawString("Tel - " + (MPC_TEL == null ? string.Empty : MPC_TEL.ToString()) + " Fax - " + (MPC_FAX == null ? string.Empty : MPC_FAX.ToString()), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            #endregion

            OffsetX = 5;
            OffsetY = OffsetY + 25;//24 20
            _OffsetY = OffsetY;
            graphics.DrawString("Customer :", new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            OffsetX = OffsetX + 60;
            graphics.DrawString(MBE_NAME == null ? string.Empty : MBE_NAME.ToString(), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            OffsetX = 600;
            _RefNoOffsetY = OffsetY;
            graphics.DrawString(HTI_REF == null ? string.Empty : HTI_REF.ToString(), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            OffsetX = 5;
            OffsetY = _OffsetY + 20;
            graphics.DrawString("Address    :", new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            OffsetX = OffsetX + 60;
            graphics.DrawString(CUS_Add == null ? string.Empty : CUS_Add.ToString(), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            OffsetX = 600;
            graphics.DrawString(SAH_DT.ToString(), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            OffsetX = 600;
            OffsetY = OffsetY + 16;
            graphics.DrawString(MPC_DESC == null ? string.Empty : MPC_DESC.ToString(), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            OffsetX = 503;
            OffsetY = OffsetY + 20;
            graphics.DrawString("A/C No :", new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            OffsetX = 600;
            graphics.DrawString(HTI_ACC_NUM == null ? string.Empty : HTI_ACC_NUM.ToString(), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            OffsetX = 5;
            OffsetY = OffsetY + 85; //Details Print
            graphics.DrawString("Premium for ", new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            OffsetX = OffsetX + 65;
            graphics.DrawString(MC_ANAL3 == null ? string.Empty : MC_ANAL3.ToString(), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);


            OffsetX = 350;
            graphics.DrawString("Net Amount", new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            OffsetX = 672;
            _val = Convert.ToDecimal(netAmt.ToString("#,#00.00"));
            OffsetX = Convert.ToInt16((OffsetX + 10 * 72 / 8 / 2) - (_val.ToString("N2").Length * 72 / 8 / 2));

            graphics.DrawString(netAmt.ToString("#,#00.00"), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            OffsetX = 350;
            OffsetY = OffsetY + 20;
            graphics.DrawString("VAT", new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            OffsetX = 672;
            _val = Convert.ToDecimal(HTI_VAT_VAL.ToString("#,#00.00"));
            OffsetX = Convert.ToInt16((OffsetX + 10 * 72 / 8 / 2) - (_val.ToString("N2").Length * 72 / 8 / 2));

            graphics.DrawString(HTI_VAT_VAL.ToString("#,#00.00"), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            OffsetX = 620;
            OffsetY = OffsetY + 5;
            graphics.DrawString("----------------------------------", new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            OffsetX = 350;
            OffsetY = OffsetY + 20;
            graphics.DrawString("Gross Amount", new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            OffsetX = 672;
            _val = Convert.ToDecimal(HTI_INS_VAL.ToString("#,#00.00"));
            OffsetX = Convert.ToInt16((OffsetX + 10 * 72 / 8 / 2) - (_val.ToString("N2").Length * 72 / 8 / 2));

            graphics.DrawString(HTI_INS_VAL.ToString("#,#00.00"), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            OffsetX = 620;
            OffsetY = OffsetY + 8;
            graphics.DrawString("================", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
        }
   //add by tharanga 2017/11/23
        public void HP_Acc_Rescription_History()
        {// Nadeeka 29-02-13
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataTable HP_Acc_Resc_His = new DataTable();
            Int32 as_at_date = 0;
            DataRow dr;

            if (BaseCls.GlbReportHeading == "Accounts Creation Restriction As at Date")
            {
                as_at_date = 1;
            }
            
            DataTable mst_profit_center = default(DataTable);
             mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbReportComp, string.Empty);
            //DataTable GLB_TEMP_CLS_ACCOUNT = bsObj.CHNLSVC.Sales.ProcessClosedAccounts(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportComp, drow["tpl_pc"].ToString());
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_HP_Acc_Resc_His = new DataTable();

                    tmp_HP_Acc_Resc_His = bsObj.CHNLSVC.Financial.GET_HP_Acc_Resc_His(drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, as_at_date);

                    HP_Acc_Resc_His.Merge(tmp_HP_Acc_Resc_His);

                }
            }
            DataTable mst_com = default(DataTable);
            if (HP_Acc_Resc_His.Rows.Count > 0)
            {
                HP_Acc_Resc_His = HP_Acc_Resc_His.DefaultView.ToTable(true);
            }

            mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbReportComp, string.Empty);
            mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(string));
            param.Columns.Add("todate", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("repHead", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate.ToString("dd/MM/yyyy");
            dr["todate"] = BaseCls.GlbReportToDate.ToString("dd/MM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["repHead"] = BaseCls.GlbReportHeading;
            param.Rows.Add(dr);


            _HP_Acc_Rescription_History.Database.Tables["HP_Acc_Resc_His"].SetDataSource(HP_Acc_Resc_His);
            _HP_Acc_Rescription_History.Database.Tables["mst_com"].SetDataSource(mst_com);
            //_HP_Acc_Rescription_History.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _HP_Acc_Rescription_History.Database.Tables["param"].SetDataSource(param);


        }
        //add by Dilshan 2018/11/02
        public void HP_Reject_Acc_Details()
        {
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataTable HP_Acc_Resc_His = new DataTable();
            Int32 as_at_date = 0;
            DataRow dr;

            if (BaseCls.GlbReportHeading == "Accounts Creation Restriction As at Date")
            {
                as_at_date = 1;
            }

            DataTable mst_profit_center = default(DataTable);
            mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbReportComp, string.Empty);
            //DataTable GLB_TEMP_CLS_ACCOUNT = bsObj.CHNLSVC.Sales.ProcessClosedAccounts(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportComp, drow["tpl_pc"].ToString());
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_HP_Acc_Resc_His = new DataTable();

                    //tmp_HP_Acc_Resc_His = bsObj.CHNLSVC.Financial.GET_HP_Acc_Resc_His(drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, as_at_date);
                    //tmp_HP_Acc_Resc_His = bsObj.CHNLSVC.Financial.Get_rejectAccBalance(_com, drow["tpl_pc"].ToString(), _asatDate, _userID);
                    tmp_HP_Acc_Resc_His = bsObj.CHNLSVC.Financial.Get_rejectAccBalance_New(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID);

                    HP_Acc_Resc_His.Merge(tmp_HP_Acc_Resc_His);

                }
            }
            DataTable mst_com = default(DataTable);
            if (HP_Acc_Resc_His.Rows.Count > 0)
            {
                HP_Acc_Resc_His = HP_Acc_Resc_His.DefaultView.ToTable(true);
            }

            mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbReportComp, string.Empty);
            mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(string));
            param.Columns.Add("todate", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("repHead", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate.ToString("dd/MM/yyyy");
            dr["todate"] = BaseCls.GlbReportToDate.ToString("dd/MM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["repHead"] = BaseCls.GlbReportHeading;
            param.Rows.Add(dr);


            _HP_Reject_Acc_Details.Database.Tables["HP_Reject_Acc"].SetDataSource(HP_Acc_Resc_His);
            _HP_Reject_Acc_Details.Database.Tables["mst_com"].SetDataSource(mst_com);
            //_HP_Acc_Rescription_History.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _HP_Reject_Acc_Details.Database.Tables["param"].SetDataSource(param);


        }
    }
}
