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
namespace FF.WindowsERPClient.Reports.Finance
{
    class clsFinanceRep
    {
        public FF.WindowsERPClient.Reports.Finance.PersonalChequeStatement _chqSts = new FF.WindowsERPClient.Reports.Finance.PersonalChequeStatement();
        public FF.WindowsERPClient.Reports.Finance.AD_Loan_Settlement _adLoanSett = new FF.WindowsERPClient.Reports.Finance.AD_Loan_Settlement();
        public FF.WindowsERPClient.Reports.Finance.Claim_Expenses_Voucher _claimExpVou = new FF.WindowsERPClient.Reports.Finance.Claim_Expenses_Voucher();
        public FF.WindowsERPClient.Reports.Finance.Short_Rem_Statement _ShortRem = new FF.WindowsERPClient.Reports.Finance.Short_Rem_Statement();
        public FF.WindowsERPClient.Reports.Finance.Doc_Check_List _docCheckList = new FF.WindowsERPClient.Reports.Finance.Doc_Check_List();
        
        public FF.WindowsERPClient.Reports.Finance.CashControl _cashControl = new FF.WindowsERPClient.Reports.Finance.CashControl();
        public FF.WindowsERPClient.Reports.Finance.CashControlRecon _cashControlRecon = new FF.WindowsERPClient.Reports.Finance.CashControlRecon();
        public FF.WindowsERPClient.Reports.Finance.Excess_Short_Summ _excsShortSumm = new FF.WindowsERPClient.Reports.Finance.Excess_Short_Summ();
        public FF.WindowsERPClient.Reports.Finance.CashControlCash _cashControlCash = new FF.WindowsERPClient.Reports.Finance.CashControlCash();

        public FF.WindowsERPClient.Reports.Finance.RemitanceCheckList _remitCheckList = new FF.WindowsERPClient.Reports.Finance.RemitanceCheckList();
        public FF.WindowsERPClient.Reports.Finance.SignOff _signoff = new FF.WindowsERPClient.Reports.Finance.SignOff();
        public FF.WindowsERPClient.Reports.Finance.Cashierwisesales _cashiersales = new FF.WindowsERPClient.Reports.Finance.Cashierwisesales();
        public FF.WindowsERPClient.Reports.Reconciliation.Elite_Comm_Summary_R1 _EComm_R1 = new FF.WindowsERPClient.Reports.Reconciliation.Elite_Comm_Summary_R1();
        public FF.WindowsERPClient.Reports.Reconciliation.Elite_Comm_Summary_R1D _EComm_R1D = new FF.WindowsERPClient.Reports.Reconciliation.Elite_Comm_Summary_R1D();
        public FF.WindowsERPClient.Reports.Reconciliation.Elite_Comm_Summary_R2 _EComm_R2 = new FF.WindowsERPClient.Reports.Reconciliation.Elite_Comm_Summary_R2();
        public FF.WindowsERPClient.Reports.Reconciliation.Elite_Comm_Def _EComm_Def = new FF.WindowsERPClient.Reports.Reconciliation.Elite_Comm_Def();

        public FF.WindowsERPClient.Reports.Finance.Incentive_Def _incentiveDef = new FF.WindowsERPClient.Reports.Finance.Incentive_Def();
        public FF.WindowsERPClient.Reports.Finance.AdvanceReceiptReg _advRecReg = new FF.WindowsERPClient.Reports.Finance.AdvanceReceiptReg();
        public FF.WindowsERPClient.Reports.Finance.OverShortStatement _overShort = new FF.WindowsERPClient.Reports.Finance.OverShortStatement();
        public FF.WindowsERPClient.Reports.Finance.CashCommDef _cashComDef = new FF.WindowsERPClient.Reports.Finance.CashCommDef();
        public FF.WindowsERPClient.Reports.Finance.DailyExpences _dailyExp = new FF.WindowsERPClient.Reports.Finance.DailyExpences();
        public FF.WindowsERPClient.Reports.Finance.DeliveredSales _delSales = new FF.WindowsERPClient.Reports.Finance.DeliveredSales();
        public FF.WindowsERPClient.Reports.Finance.Physical_Cash_Verify_Report _physicalCash = new FF.WindowsERPClient.Reports.Finance.Physical_Cash_Verify_Report();
        public FF.WindowsERPClient.Reports.Finance.HP_GRP_Comm_Target_Report _HPGRPComm = new FF.WindowsERPClient.Reports.Finance.HP_GRP_Comm_Target_Report();
        public FF.WindowsERPClient.Reports.Finance.Cr_Balance_Report _CrBal = new FF.WindowsERPClient.Reports.Finance.Cr_Balance_Report();
        public FF.WindowsERPClient.Reports.Finance.ProductBonus _prodBon = new FF.WindowsERPClient.Reports.Finance.ProductBonus();
        public FF.WindowsERPClient.Reports.Finance.ProductBonusApp _prodBonApp = new FF.WindowsERPClient.Reports.Finance.ProductBonusApp();
        public FF.WindowsERPClient.Reports.Finance.ProductBonusInv _prodBonInv = new FF.WindowsERPClient.Reports.Finance.ProductBonusInv();
        public FF.WindowsERPClient.Reports.Finance.ProductBonusInvInc _prodBonInvInc = new FF.WindowsERPClient.Reports.Finance.ProductBonusInvInc();
        public FF.WindowsERPClient.Reports.Finance.ProdBonusInvIncDet _prodBonInvIncDet = new FF.WindowsERPClient.Reports.Finance.ProdBonusInvIncDet();
        public FF.WindowsERPClient.Reports.Finance.Prod_Bonus_Voucher _prodBonVou = new FF.WindowsERPClient.Reports.Finance.Prod_Bonus_Voucher();
        public FF.WindowsERPClient.Reports.Finance.rcv_desk_rem_sum_rep _rcv_Sum = new FF.WindowsERPClient.Reports.Finance.rcv_desk_rem_sum_rep();
        public FF.WindowsERPClient.Reports.Finance.Excess_Rem_Statement _ExcessRem = new FF.WindowsERPClient.Reports.Finance.Excess_Rem_Statement();
        public FF.WindowsERPClient.Reports.Finance.Short_Sett_Report _shortsett = new FF.WindowsERPClient.Reports.Finance.Short_Sett_Report();
        public FF.WindowsERPClient.Reports.Finance.ESD_Dtl_Report _ESDStmt = new FF.WindowsERPClient.Reports.Finance.ESD_Dtl_Report();
        public FF.WindowsERPClient.Reports.Finance.ESD_recon_Report _ESDRecon = new FF.WindowsERPClient.Reports.Finance.ESD_recon_Report();
        public FF.WindowsERPClient.Reports.Finance.rcv_dsk_processed_Report _remsunprcs = new FF.WindowsERPClient.Reports.Finance.rcv_dsk_processed_Report();
        public FF.WindowsERPClient.Reports.Finance.AdvRecRecon _advRecRecon = new FF.WindowsERPClient.Reports.Finance.AdvRecRecon();
        public FF.WindowsERPClient.Reports.Finance.Not_Realized_Transactions _NOT_RLZ_TRNZ = new FF.WindowsERPClient.Reports.Finance.Not_Realized_Transactions();
        public FF.WindowsERPClient.Reports.Finance.Bank_Statement_Report _bnkste = new FF.WindowsERPClient.Reports.Finance.Bank_Statement_Report();
        public FF.WindowsERPClient.Reports.Finance.RealizationFinalizeStatus _RLZ_FINZ = new FF.WindowsERPClient.Reports.Finance.RealizationFinalizeStatus();
        public FF.WindowsERPClient.Reports.Finance.crcd_recon_report _crcdrecon = new FF.WindowsERPClient.Reports.Finance.crcd_recon_report();
        public FF.WindowsERPClient.Reports.Finance.BankReconciliation _BNK_RECON = new FF.WindowsERPClient.Reports.Finance.BankReconciliation();
        public FF.WindowsERPClient.Reports.Finance.BankReconciliationSummery _BNK_RECON_SMRY = new FF.WindowsERPClient.Reports.Finance.BankReconciliationSummery(); // Added by Chathura on 20-oct-2017
        public FF.WindowsERPClient.Reports.Finance.AdvRecReconSum _advRecReconSum = new FF.WindowsERPClient.Reports.Finance.AdvRecReconSum();
        public FF.WindowsERPClient.Reports.Finance.IntrodComm _introComm = new FF.WindowsERPClient.Reports.Finance.IntrodComm();
  
        public FF.WindowsERPClient.Reports.Finance.Extra_add_Docs _extraAddDocs = new FF.WindowsERPClient.Reports.Finance.Extra_add_Docs();
        public FF.WindowsERPClient.Reports.Finance.RealizationStatus _BNK_RLZ_ST = new FF.WindowsERPClient.Reports.Finance.RealizationStatus(); // Added by Chathura on 20-oct-2017
        public FF.WindowsERPClient.Reports.Finance.RemitanceControlRecon _REM_CON = new FF.WindowsERPClient.Reports.Finance.RemitanceControlRecon(); // Added by Chathura on 24-oct-2017
        public FF.WindowsERPClient.Reports.Finance.RemitanceControlReconSummery _REM_CON_SMRY = new FF.WindowsERPClient.Reports.Finance.RemitanceControlReconSummery(); // Added by Chathura on 30-oct-2017
        public FF.WindowsERPClient.Reports.Finance.bankaccounttransfferingreport _bankaccounttransfferingreport = new FF.WindowsERPClient.Reports.Finance.bankaccounttransfferingreport(); // Added by Chathura on 30-oct-2017
        public FF.WindowsERPClient.Reports.Finance.BankReconciliationnew _BNK_RECONnew = new FF.WindowsERPClient.Reports.Finance.BankReconciliationnew();

        public FF.WindowsERPClient.Reports.Finance.ManagerCommBonus _managerComm = new FF.WindowsERPClient.Reports.Finance.ManagerCommBonus();
        public FF.WindowsERPClient.Reports.Finance.ManagerIncome _managerIncome = new FF.WindowsERPClient.Reports.Finance.ManagerIncome();

        public FF.WindowsERPClient.Reports.Finance.OverShortDetail _overShortdet = new FF.WindowsERPClient.Reports.Finance.OverShortDetail();
        public FF.WindowsERPClient.Reports.Finance.OverShortSum _overShortsum = new FF.WindowsERPClient.Reports.Finance.OverShortSum();
        public FF.WindowsERPClient.Reports.Finance.CreditCardsReconcilation _creditcard_recon = new FF.WindowsERPClient.Reports.Finance.CreditCardsReconcilation();
        public FF.WindowsERPClient.Reports.Finance.OverShortMovement _overShortmov = new FF.WindowsERPClient.Reports.Finance.OverShortMovement();
        
        Base bsObj;
        DataTable tmp_user_pc = new DataTable();
        DataTable GLOB_DataTable = new DataTable();

        public clsFinanceRep()
        {
            bsObj = new Base();

        }

        public void PrintIntroCommDefReport()
        {
            DataTable param = new DataTable();
            DataRow dr;

            DataTable _comHdr = bsObj.CHNLSVC.Financial.GetPromotCommHdr(BaseCls.GlbUserComCode, BaseCls.GlbReportDoc);
           // DataTable _comItm = bsObj.CHNLSVC.Financial.GetPromotCommDetails(BaseCls.GlbReportParaLine1);
            DataTable _comSch = bsObj.CHNLSVC.Financial.GetPromotCommSch(BaseCls.GlbReportParaLine1);
            DataTable _comParty = bsObj.CHNLSVC.Financial.GetPromotCommParty(BaseCls.GlbReportParaLine1);
            DataTable _comDet = bsObj.CHNLSVC.Financial.GetPromotCommDet(BaseCls.GlbReportParaLine1);
           // DataTable _comSaleTp = bsObj.CHNLSVC.Financial.GetPromotCommSaleTp(BaseCls.GlbReportParaLine1);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("doc_type", typeof(string));
            param.Columns.Add("todate", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["doc_type"] = BaseCls.GlbReportDocType;
            dr["todate"] = Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            param.Rows.Add(dr);

            foreach (object repOp in _introComm.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "promo_itm")
                    {
                        ReportDocument subRepDoc = _introComm.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["HPR_PROMOT_COM_ITM"].SetDataSource(_comDet);
                    }
                    if (_cs.SubreportName == "promo_sch")
                    {
                        ReportDocument subRepDoc = _introComm.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["HPR_PROMOT_COM_SCH"].SetDataSource(_comSch);
                    }
                    if (_cs.SubreportName == "promo_party")
                    {
                        ReportDocument subRepDoc = _introComm.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["HPR_PROMOT_COM_PTY"].SetDataSource(_comParty);
                    }
                    //if (_cs.SubreportName == "promo_det")
                    //{
                    //    ReportDocument subRepDoc = _introComm.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["HPR_PROMOT_COM_DET"].SetDataSource(_comItm);
                    //}
     
                }
            }


            _introComm.Database.Tables["HPR_PROMOT_COM_HDR"].SetDataSource(_comHdr);
            _introComm.Database.Tables["param"].SetDataSource(param);
        }

        public void PrintExtraAddBankDocs()
        {
            DataTable param = new DataTable();
            DataRow dr;

            DataTable HP_RLZ_TRNS = bsObj.CHNLSVC.Sales.PrintExtraAddBankDocs(BaseCls.GlbUserComCode, BaseCls.GlbReportDoc, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbPayType);

            param.Clear();

            param.Columns.Add("usr", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("doc_type", typeof(string));
            param.Columns.Add("todate", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("AccNo", typeof(string));
            dr = param.NewRow();
            dr["usr"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM  " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO  " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["doc_type"] = BaseCls.GlbReportDocType;
            dr["todate"] = Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["AccNo"] = BaseCls.GlbReportDoc;
            param.Rows.Add(dr);

            _extraAddDocs.Database.Tables["NRT_DT"].SetDataSource(HP_RLZ_TRNS);
            _extraAddDocs.Database.Tables["param"].SetDataSource(param);
        }

        public void InternalPayVouReport()
        { // Sanjeewa 28-03-2014                        
            string _err;
            string _filePath;

            DataTable INT_PAY = bsObj.CHNLSVC.Financial.GetInternalPayVouDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportExecCode, BaseCls.GlbUserComCode, "", BaseCls.GlbReportTp, BaseCls.GlbUserID, out _err, out _filePath);

            if (!string.IsNullOrEmpty(_err))
            {
                MessageBox.Show(_err.ToString(), "System Error in Exporting to Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(_filePath);
                p.Start();
            }
        }

        //kapila
        public void AdvanceReceiptRecon()
        {
            // kapila 
            DataTable advRecReconc = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

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
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Financial.PrintAdvReceiptRecon(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID);
                    advRecReconc.Merge(TMP_DataTable);
                }
            }

            if (BaseCls.GlbReportName == "AdvRecRecon.rpt")
            {
                _advRecRecon.Database.Tables["AdvRefRecon"].SetDataSource(advRecReconc);
                _advRecRecon.Database.Tables["param"].SetDataSource(param);
            }
            else
            {
                _advRecReconSum.Database.Tables["AdvRefRecon"].SetDataSource(advRecReconc);
                _advRecReconSum.Database.Tables["param"].SetDataSource(param);
            }
        }

        public void ProductBonusVoucher()
        {
            // kapila 

            DataTable glb_PBVou = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

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

            glb_PBVou.Clear();
            glb_PBVou = bsObj.CHNLSVC.Financial.PrintPBonusVoucher(BaseCls.GlbReportDoc, BaseCls.GlbUserComCode, BaseCls.GlbReportProfit);

            _prodBonVou.Database.Tables["PBONUS_VOU"].SetDataSource(glb_PBVou);
            _prodBonVou.Database.Tables["param"].SetDataSource(param);
        }

        public void RecievingDeskRemitanceSummary()
        {
            //Sanjeewa 27-01-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Financial.GetRcvDeskRemSumDetails(BaseCls.GlbReportYear, BaseCls.GlbReportMonth, BaseCls.GlbReportnoofDays, BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("month", typeof(string));
            param.Columns.Add("week", typeof(string));
            param.Columns.Add("year", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["month"] = BaseCls.GlbReportMonth;
            dr["week"] = BaseCls.GlbReportnoofDays == 0 ? "ALL" : Convert.ToString(BaseCls.GlbReportnoofDays);
            dr["year"] = BaseCls.GlbReportYear;

            param.Rows.Add(dr);

            _rcv_Sum.Database.Tables["RCV_REM_SUM"].SetDataSource(GLOB_DataTable);
            _rcv_Sum.Database.Tables["param"].SetDataSource(param);

        }

        public void ShortSettlementReport()
        {
            //Sanjeewa 03-02-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Financial.GetShortSettDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
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

            _shortsett.Database.Tables["SHORT_SETT"].SetDataSource(GLOB_DataTable);
            _shortsett.Database.Tables["param"].SetDataSource(param);

        }

        public void ESDStatement()
        {
            //Sanjeewa 14-06-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Financial.GetESDStatement(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
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

            _ESDStmt.Database.Tables["ESD_DTL"].SetDataSource(GLOB_DataTable);
            _ESDStmt.Database.Tables["param"].SetDataSource(param);

        }

        public void ESDRecon()
        {
            //kapila 16/6/2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Financial.GetESDRecon(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
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
            param.Columns.Add("grouplocation", typeof(Int32));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            param.Rows.Add(dr);

            _ESDRecon.Database.Tables["GLB_ESD_RECON"].SetDataSource(GLOB_DataTable);
            _ESDRecon.Database.Tables["param"].SetDataSource(param);

        }

        public void ProductBonusInvoicesInc()
        {
            // kapila 

            DataTable dt_inc_calc = new DataTable();
            DataTable dt_inc_calc_inv = new DataTable();
            DataTable dt_inc_calc_dt = new DataTable();
            DataTable dt_inc_calc_inc = new DataTable();
            DataTable dt_inc_sch_dt = new DataTable();
            DataTable dt_inc_sch_inc_dt = new DataTable();
            DataTable dt_inc_sch = new DataTable();
            DataTable dt_inc_sch_inc = new DataTable();
            DataTable param = new DataTable();
            DataTable dt_PC = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("grouplocation", typeof(Int32));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["grouplocation"] = BaseCls.GlbReportGroupProfit;
            param.Rows.Add(dr);

            dt_inc_calc.Clear();
            dt_inc_calc = bsObj.CHNLSVC.Financial.Get_gnt_inc_calc(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            dt_inc_calc_inv.Clear();
            dt_inc_calc_inv = bsObj.CHNLSVC.Financial.Get_dt_inc_calc_inv(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            dt_inc_calc_dt.Clear();
            dt_inc_calc_dt = bsObj.CHNLSVC.Financial.Get_dt_inc_calc_dt(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            dt_inc_calc_inc.Clear();
            dt_inc_calc_inc = bsObj.CHNLSVC.Financial.Get_dt_inc_calc_inc(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            dt_inc_sch_dt.Clear();
            dt_inc_sch_dt = bsObj.CHNLSVC.Financial.Get_dt_inc_sch_dt(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            dt_inc_sch_inc_dt.Clear();
            dt_inc_sch_inc_dt = bsObj.CHNLSVC.Financial.Get_dt_inc_sch_inc_dt(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            dt_inc_sch_inc.Clear();
            dt_inc_sch_inc = bsObj.CHNLSVC.Financial.Get_dt_inc_sch_inc(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            dt_PC.Clear();
            dt_PC = bsObj.CHNLSVC.General.Get_All_PC(BaseCls.GlbUserComCode);

            if (BaseCls.GlbReportName == "ProdBonusInvIncDet.rpt")
            {
                // dt_inc_sch.Clear();
                //  dt_inc_sch = bsObj.CHNLSVC.Financial.sp_get_inc_sch(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);

                //  _prodBonInvInc.Database.Tables["gnr_inc_sch"].SetDataSource(dt_inc_sch);
                _prodBonInvIncDet.Database.Tables["gnr_inc_sch_inc"].SetDataSource(dt_inc_sch_inc);
                _prodBonInvIncDet.Database.Tables["gnt_inc_calc"].SetDataSource(dt_inc_calc);
                _prodBonInvIncDet.Database.Tables["gnt_inc_calc_inv"].SetDataSource(dt_inc_calc_inv);
                _prodBonInvIncDet.Database.Tables["gnt_inc_calc_dt"].SetDataSource(dt_inc_calc_dt);
                _prodBonInvIncDet.Database.Tables["gnt_inc_calc_inc"].SetDataSource(dt_inc_calc_inc);
                _prodBonInvIncDet.Database.Tables["gnr_inc_sch_dt"].SetDataSource(dt_inc_sch_dt);
                _prodBonInvIncDet.Database.Tables["gnr_inc_sch_inc_dt"].SetDataSource(dt_inc_sch_inc_dt);
                _prodBonInvIncDet.Database.Tables["param"].SetDataSource(param);
                _prodBonInvIncDet.Database.Tables["MST_PC"].SetDataSource(dt_PC);
            }
            else
            {
                _prodBonInvInc.Database.Tables["gnr_inc_sch_inc"].SetDataSource(dt_inc_sch_inc);
                _prodBonInvInc.Database.Tables["gnt_inc_calc"].SetDataSource(dt_inc_calc);
                _prodBonInvInc.Database.Tables["gnt_inc_calc_inv"].SetDataSource(dt_inc_calc_inv);
                _prodBonInvInc.Database.Tables["gnt_inc_calc_dt"].SetDataSource(dt_inc_calc_dt);
                _prodBonInvInc.Database.Tables["gnt_inc_calc_inc"].SetDataSource(dt_inc_calc_inc);
                _prodBonInvInc.Database.Tables["gnr_inc_sch_dt"].SetDataSource(dt_inc_sch_dt);
                _prodBonInvInc.Database.Tables["gnr_inc_sch_inc_dt"].SetDataSource(dt_inc_sch_inc_dt);
                _prodBonInvInc.Database.Tables["param"].SetDataSource(param);
                _prodBonInvInc.Database.Tables["MST_PC"].SetDataSource(dt_PC);
            }



        }

        public void ProductBonusInvoices()
        {
            // kapila 

            DataTable dt_inc_calc = new DataTable();
            DataTable dt_inc_calc_inv = new DataTable();
            DataTable param = new DataTable();
            DataTable dt_PC = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("grouplocation", typeof(Int32));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["grouplocation"] = BaseCls.GlbReportGroupProfit;
            param.Rows.Add(dr);

            dt_inc_calc.Clear();
            dt_inc_calc = bsObj.CHNLSVC.Financial.Get_gnt_inc_calc(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            dt_inc_calc_inv.Clear();
            dt_inc_calc_inv = bsObj.CHNLSVC.Financial.Get_dt_inc_calc_inv(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            dt_PC.Clear();
            dt_PC = bsObj.CHNLSVC.General.Get_All_PC(BaseCls.GlbUserComCode);


            _prodBonInv.Database.Tables["gnt_inc_calc"].SetDataSource(dt_inc_calc);
            _prodBonInv.Database.Tables["gnt_inc_calc_inv"].SetDataSource(dt_inc_calc_inv);
            _prodBonInv.Database.Tables["param"].SetDataSource(param);
            _prodBonInv.Database.Tables["MST_PC"].SetDataSource(dt_PC);
        }

        public void ProductBonus()
        {
            // kapila 

            DataTable dt_inc_calc = new DataTable();
            DataTable dt_inc_calc_dt = new DataTable();
            DataTable dt_inc_calc_inc = new DataTable();
            DataTable dt_inc_sch_inc = new DataTable();
            DataTable dt_inc_sch_inc_dt = new DataTable();
            DataTable dt_inc_sch_dt = new DataTable();
            DataTable param = new DataTable();
            DataTable dt_PC = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("grouplocation", typeof(Int32));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["grouplocation"] = BaseCls.GlbReportGroupProfit;
            param.Rows.Add(dr);

            dt_inc_calc.Clear();
            dt_inc_calc = bsObj.CHNLSVC.Financial.Get_gnt_inc_calc(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            dt_inc_calc_dt.Clear();
            dt_inc_calc_dt = bsObj.CHNLSVC.Financial.Get_dt_inc_calc_dt(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            dt_inc_calc_inc.Clear();
            dt_inc_calc_inc = bsObj.CHNLSVC.Financial.Get_dt_inc_calc_inc(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            dt_inc_sch_inc.Clear();
            dt_inc_sch_inc = bsObj.CHNLSVC.Financial.Get_dt_inc_sch_inc(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            dt_inc_sch_inc_dt.Clear();
            dt_inc_sch_inc_dt = bsObj.CHNLSVC.Financial.Get_dt_inc_sch_inc_dt(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            dt_inc_sch_dt.Clear();
            dt_inc_sch_dt = bsObj.CHNLSVC.Financial.Get_dt_inc_sch_dt(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            dt_PC.Clear();
            dt_PC = bsObj.CHNLSVC.General.Get_All_PC(BaseCls.GlbUserComCode);

            if (BaseCls.GlbReportName == "ProductBonus.rpt")
            {
                _prodBon.Database.Tables["gnt_inc_calc"].SetDataSource(dt_inc_calc);
                _prodBon.Database.Tables["gnt_inc_calc_dt"].SetDataSource(dt_inc_calc_dt);
                _prodBon.Database.Tables["gnt_inc_calc_inc"].SetDataSource(dt_inc_calc_inc);
                _prodBon.Database.Tables["gnr_inc_sch_inc"].SetDataSource(dt_inc_sch_inc);
                _prodBon.Database.Tables["gnr_inc_sch_inc_dt"].SetDataSource(dt_inc_sch_inc_dt);
                _prodBon.Database.Tables["gnr_inc_sch_dt"].SetDataSource(dt_inc_sch_dt);
                _prodBon.Database.Tables["MST_PC"].SetDataSource(dt_PC);
                _prodBon.Database.Tables["param"].SetDataSource(param);
            }
            else
            {
                _prodBonApp.Database.Tables["gnt_inc_calc"].SetDataSource(dt_inc_calc);
                _prodBonApp.Database.Tables["gnt_inc_calc_dt"].SetDataSource(dt_inc_calc_dt);
                _prodBonApp.Database.Tables["gnt_inc_calc_inc"].SetDataSource(dt_inc_calc_inc);
                _prodBonApp.Database.Tables["gnr_inc_sch_inc"].SetDataSource(dt_inc_sch_inc);
                _prodBonApp.Database.Tables["gnr_inc_sch_inc_dt"].SetDataSource(dt_inc_sch_inc_dt);
                _prodBonApp.Database.Tables["gnr_inc_sch_dt"].SetDataSource(dt_inc_sch_dt);
                _prodBonApp.Database.Tables["MST_PC"].SetDataSource(dt_PC);
                _prodBonApp.Database.Tables["param"].SetDataSource(param);
            }
        }

        public void ExportProductBonusDetailReport()
        {// Sanjeewa 22-01-20134            
            DataTable PBONUS_DTL = new DataTable();

            PBONUS_DTL = bsObj.CHNLSVC.Financial.getProductBonusDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserComCode, "");

        }


        public void DeliveredSalesForComm()
        {
            // kapila 

            DataTable delSales = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

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

            delSales.Clear();
            delSales = bsObj.CHNLSVC.Financial.PrintDeliveredSalesForComm(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            _delSales.Database.Tables["DelSales"].SetDataSource(delSales);
            _delSales.Database.Tables["param"].SetDataSource(param);
        }

        public void CashCommissionDef()
        {
            // kapila 
            DataTable cashcomheader = new DataTable();
            DataTable cashcomdef = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

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

            cashcomheader.Clear();
            cashcomheader = bsObj.CHNLSVC.Financial.PrintCashCommDefHead(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDoc);
            cashcomdef = bsObj.CHNLSVC.Financial.PrintCashCommDefDet(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDoc);

            _cashComDef.Database.Tables["SAR_CASH_COMM_HDR"].SetDataSource(cashcomheader);
            _cashComDef.Database.Tables["SAR_CASH_COMM_DET"].SetDataSource(cashcomdef);
            _cashComDef.Database.Tables["param"].SetDataSource(param);
        }

        public void OverandShortStatement()
        {
            // kapila 
            DataTable overShort = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportAsAtDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            param.Rows.Add(dr);


            overShort.Clear();
            overShort = bsObj.CHNLSVC.Financial.PrintOverandShort(BaseCls.GlbUserComCode, Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbUserID);

            _overShort.Database.Tables["Over_Short"].SetDataSource(overShort);
            _overShort.Database.Tables["param"].SetDataSource(param);
        }
        public void OverandShortDetail()
        {
            // Dilshan 
            DataTable overShort = new DataTable();
            DataTable param = new DataTable();
            DataTable MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            DataRow dr;

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


            overShort.Clear();
            overShort = bsObj.CHNLSVC.Financial.PrintOverandShortDetail(BaseCls.GlbReportProfit, BaseCls.GlbUserComCode, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID);

            _overShortdet.Database.Tables["Over_Short_Detail"].SetDataSource(overShort);
            _overShortdet.Database.Tables["param"].SetDataSource(param);
            //_RLZ_FINZ.Database.Tables["mst_com"].SetDataSource(MST_COM);
        }
        public void OverandShortSum()
        {
            // Dilshan 
            DataTable overShort = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

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


            overShort.Clear();
            overShort = bsObj.CHNLSVC.Financial.PrintOverandShortSum(BaseCls.GlbReportProfit, BaseCls.GlbUserComCode, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID);

            _overShortsum.Database.Tables["Over_Short_Detail"].SetDataSource(overShort);
            _overShortsum.Database.Tables["param"].SetDataSource(param);
        }
        public void OverandShortMovement()
        {
            // Dilshan 
            DataTable overShort = new DataTable();
            DataTable param = new DataTable();
            DataTable MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            DataRow dr;

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


            overShort.Clear();
            overShort = bsObj.CHNLSVC.Financial.PrintOverandShortDetail(BaseCls.GlbReportProfit, BaseCls.GlbUserComCode, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID);

            _overShortmov.Database.Tables["Over_Short_Detail"].SetDataSource(overShort);
            _overShortmov.Database.Tables["param"].SetDataSource(param);
            //_RLZ_FINZ.Database.Tables["mst_com"].SetDataSource(MST_COM);
        }
        public void AdvanceReceiptRegistry()
        {
            // kapila 
            DataTable advRecReg = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

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


            advRecReg.Clear();
            advRecReg = bsObj.CHNLSVC.Financial.PrintAdvReceiptRegistry(BaseCls.GlbUserComCode, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID);


            _advRecReg.Database.Tables["AdvRecReg"].SetDataSource(advRecReg);
            _advRecReg.Database.Tables["param"].SetDataSource(param);
        }

        public void IncenticeSchemeDef()
        {
            DataTable param = new DataTable();
            DataRow dr;

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

            DataTable _incSch = bsObj.CHNLSVC.Financial.GetIncSch(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            DataTable _incSchDet = bsObj.CHNLSVC.Financial.GetIncSchDet(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            DataTable _incSchPerson = bsObj.CHNLSVC.Financial.GetIncSchPerson(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            DataTable _incSchPC = bsObj.CHNLSVC.Financial.GetIncSchPC(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            DataTable _incSchSalesTp = bsObj.CHNLSVC.Financial.GetIncSchSalesTp(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            DataTable _incSchInc = bsObj.CHNLSVC.Financial.GetIncSchInc(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);
            DataTable _incSchIncDet = bsObj.CHNLSVC.Financial.GetIncSchIncDt(BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);

            _incentiveDef.Database.Tables["GNR_INC_SCH"].SetDataSource(_incSch);
            _incentiveDef.Database.Tables["GNR_INC_SCH_DT"].SetDataSource(_incSchDet);
            _incentiveDef.Database.Tables["GNR_INC_SCH_PERSON"].SetDataSource(_incSchPerson);
            _incentiveDef.Database.Tables["GNR_INC_SCH_PC"].SetDataSource(_incSchPC);
            _incentiveDef.Database.Tables["GNR_INC_SALE_TP"].SetDataSource(_incSchSalesTp);
            //  _incentiveDef.Database.Tables["GNR_INC_SCH_INC"].SetDataSource(_incSchInc);
            _incentiveDef.Database.Tables["GNR_INC_SCH_INC_DT"].SetDataSource(_incSchIncDet);
            _incentiveDef.Database.Tables["param"].SetDataSource(param);

        }

        public void SignOff()
        {
            string _userID = "";
            DataTable _signOff = new DataTable();
            DataTable _summary = new DataTable();
            DataTable _revsummary = new DataTable();
            DataTable _advReceipt = new DataTable();
            DataTable _credInv = new DataTable();
            DataTable _cashRefund = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("openbal", typeof(Decimal));
            param.Columns.Add("closebal", typeof(Decimal));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("sDate", typeof(DateTime));
            param.Columns.Add("eDate", typeof(DateTime));
            param.Columns.Add("cashTot", typeof(Decimal));
            param.Columns.Add("TotLess", typeof(Decimal));
            param.Columns.Add("SystemBal", typeof(Decimal));
            param.Columns.Add("group_Cashier", typeof(int));
            param.Columns.Add("group_Exec", typeof(int));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["openbal"] = BaseCls.GlbReportOpBal;
            dr["closebal"] = BaseCls.GlbReportCloseBal;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["sDate"] = BaseCls.GlbReportFromDate;
            dr["eDate"] = BaseCls.GlbReportToDate;
            dr["cashTot"] = BaseCls.GlbCIH;
            dr["TotLess"] = BaseCls.GlbCommWithdrawn;
            dr["SystemBal"] = BaseCls.GlbCIHFinal;
            dr["group_Cashier"] = BaseCls.GlbReportGroupCustomerCode;
            dr["group_Exec"] = BaseCls.GlbReportGroupExecCode;
            param.Rows.Add(dr);

            _userID = BaseCls.GlbUserComCode + BaseCls.GlbUserID;
            
            //signOff = bsObj.CHNLSVC.Financial.PrintSignOff(BaseCls.GlbReportProfit, Convert.ToDateTime("06/Jun/2013").Date, Convert.ToDateTime("06/Jun/2013").Date, BaseCls.GlbUserID, 0);

            if (BaseCls.GlbReportName == "SignOff.rpt")
            {
                _signOff.Clear();
                _signOff = bsObj.CHNLSVC.Financial.PrintSignOff(BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, 0, BaseCls.GlbReportIsCostPrmission);

                _signoff.Database.Tables["signoff"].SetDataSource(_signOff);
                _signoff.Database.Tables["param"].SetDataSource(param);
            }
            else
            {
                _signOff.Clear();
                _summary.Clear();
                _revsummary.Clear();
                _advReceipt.Clear();
                _credInv.Clear();
                _cashRefund.Clear();
                DataTable tmp_user_pc = new DataTable();
                tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

                if (tmp_user_pc.Rows.Count > 0)
                {
                    foreach (DataRow drow in tmp_user_pc.Rows)
                    {
                        
                        DataTable TMP_DataTable = new DataTable();
                        TMP_DataTable = bsObj.CHNLSVC.Financial.PrintCancelInvs(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, 0, BaseCls.GlbReportIsCostPrmission);
                        _summary.Merge(TMP_DataTable);

                        
                        TMP_DataTable = bsObj.CHNLSVC.Financial.PrintReverseInv(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, 0, BaseCls.GlbReportIsCostPrmission);
                        _revsummary.Merge(TMP_DataTable);

                        
                        TMP_DataTable = bsObj.CHNLSVC.Financial.PrintAdvReceipts(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, 0, BaseCls.GlbReportIsCostPrmission);
                        _advReceipt.Merge(TMP_DataTable);

                        
                        TMP_DataTable = bsObj.CHNLSVC.Financial.PrintCreditInvoices(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, 0, BaseCls.GlbReportIsCostPrmission);
                        _credInv.Merge(TMP_DataTable);

                        
                        TMP_DataTable = bsObj.CHNLSVC.Financial.PrintCashRefunds(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, 0, BaseCls.GlbReportIsCostPrmission);
                        _cashRefund.Merge(TMP_DataTable);


                        TMP_DataTable = bsObj.CHNLSVC.Financial.PrintSignOff(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, 0, BaseCls.GlbReportIsCostPrmission);
                        _signOff.Merge(TMP_DataTable);

                    }
                }
                //_summary.Clear();
                //_summary = bsObj.CHNLSVC.Financial.PrintCancelInvs(BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, 0, BaseCls.GlbReportIsCostPrmission);

                //_revsummary.Clear();
                //_revsummary = bsObj.CHNLSVC.Financial.PrintReverseInv(BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, 0, BaseCls.GlbReportIsCostPrmission);

                //_advReceipt.Clear();
                //_advReceipt = bsObj.CHNLSVC.Financial.PrintAdvReceipts(BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, 0, BaseCls.GlbReportIsCostPrmission);

                //_credInv.Clear();
                //_credInv = bsObj.CHNLSVC.Financial.PrintCreditInvoices(BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, 0, BaseCls.GlbReportIsCostPrmission);

                //_cashRefund.Clear();
                //_cashRefund = bsObj.CHNLSVC.Financial.PrintCashRefunds(BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, 0, BaseCls.GlbReportIsCostPrmission);

                _cashiersales.Database.Tables["signoff"].SetDataSource(_signOff);
                _cashiersales.Database.Tables["summary"].SetDataSource(_summary);
                _cashiersales.Database.Tables["revsummary"].SetDataSource(_revsummary);
                _cashiersales.Database.Tables["advreceipt"].SetDataSource(_advReceipt);
                _cashiersales.Database.Tables["creditinvoice"].SetDataSource(_credInv);
                _cashiersales.Database.Tables["cashrefund"].SetDataSource(_cashRefund);
                _cashiersales.Database.Tables["param"].SetDataSource(param);
            }
        }

        public void EliteCommDefReport()
        {// Sanjeewa 15-07-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable COMMDEF = bsObj.CHNLSVC.MsgPortal.GetEliteCommDefDetails(BaseCls.GlbReportDoc, BaseCls.GlbUserID);
            DataTable COMMPC = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbUserComCode, "");
            DataTable COMMCOM = bsObj.CHNLSVC.MsgPortal.GetCompanyTable();
            DataTable COMMPARTY = bsObj.CHNLSVC.MsgPortal.GetEliteCommPartyDetails(BaseCls.GlbReportDoc, BaseCls.GlbUserID);
            DataTable COMMDET = bsObj.CHNLSVC.MsgPortal.GetEliteCommDtlDetails(BaseCls.GlbReportDoc, BaseCls.GlbUserID);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("circularno", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("year_", typeof(int));
            param.Columns.Add("month_", typeof(int));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["circularno"] = BaseCls.GlbReportDoc;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["year_"] = BaseCls.GlbReportYear;
            dr["month_"] = BaseCls.GlbReportMonth;
            param.Rows.Add(dr);

            _EComm_Def.Database.Tables["SAR_ELITE_COMM_DEF"].SetDataSource(COMMDEF);
            _EComm_Def.Database.Tables["MST_PROFIT_CENTER"].SetDataSource(COMMPC);
            _EComm_Def.Database.Tables["MST_COM"].SetDataSource(COMMCOM);
            _EComm_Def.Database.Tables["SAR_ELITE_COMM_PRTY"].SetDataSource(COMMPARTY);
            _EComm_Def.Database.Tables["param"].SetDataSource(param);

            foreach (object repOp in _EComm_Def.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "Comm_Def_Dtl")
                    {
                        ReportDocument subRepDoc = _EComm_Def.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SAR_ELITE_COMM_DET"].SetDataSource(COMMDET);
                    }
                }
            }
        }

        public void EliteCommR1Report()
        {// Sanjeewa 11-07-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable COMMDTL = bsObj.CHNLSVC.MsgPortal.GetEliteCommDetails(BaseCls.GlbReportDoc, Convert.ToInt16(BaseCls.GlbReportYear), Convert.ToInt16(BaseCls.GlbReportMonth), BaseCls.GlbUserID);
            DataTable COMMPC = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbUserComCode, "");
            DataTable COMMCOM = bsObj.CHNLSVC.MsgPortal.GetCompanyTable();
            DataTable COMMEMP = bsObj.CHNLSVC.MsgPortal.GetEmployeeTable(BaseCls.GlbUserComCode);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("circularno", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("year_", typeof(int));
            param.Columns.Add("month_", typeof(int));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["circularno"] = BaseCls.GlbReportDoc;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["year_"] = BaseCls.GlbReportYear;
            dr["month_"] = BaseCls.GlbReportMonth;
            param.Rows.Add(dr);

            _EComm_R1.Database.Tables["SAT_ELITE_COMM"].SetDataSource(COMMDTL);
            _EComm_R1.Database.Tables["MST_PROFIT_CENTER"].SetDataSource(COMMPC);
            _EComm_R1.Database.Tables["MST_COM"].SetDataSource(COMMCOM);
            _EComm_R1.Database.Tables["MST_EMP"].SetDataSource(COMMEMP);
            _EComm_R1.Database.Tables["param"].SetDataSource(param);
        }

        public void PhysicalVerificationCashReport()
        {// Sanjeewa 16-10-2013

            DataTable AUD_CVR_MAIN = bsObj.CHNLSVC.MsgPortal.GetPhyCashVerifyMDetails(BaseCls.GlbReportDoc);
            DataTable AUD_CVR_DET = bsObj.CHNLSVC.MsgPortal.GetPhyCashVerifyDTDetails(BaseCls.GlbReportDoc);
            DataTable AUD_CVR_DENO = bsObj.CHNLSVC.MsgPortal.GetPhyCashVerifyDNDetails(BaseCls.GlbReportDoc);
            DataTable AUD_CVR_CASH = bsObj.CHNLSVC.MsgPortal.GetPhyCashVerifyCSDetails(BaseCls.GlbReportDoc);
            DataTable AUD_REM_DET = new DataTable(); 

            foreach (DataRow row in AUD_CVR_MAIN.Rows)
            {
                AUD_REM_DET = bsObj.CHNLSVC.MsgPortal.GetPhyCashVerifyRemDetails(row["AUCM_COM"].ToString(), row["AUCM_PC"].ToString(), Convert.ToDateTime(row["AUCM_FROM"].ToString()).Date, Convert.ToDateTime(row["AUCM_TO"].ToString()).Date);
            }

            _physicalCash.Database.Tables["AUD_CVR_MAIN"].SetDataSource(AUD_CVR_MAIN);

            foreach (object repOp in _physicalCash.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "Collection_for_Curr_Week")
                    {
                        ReportDocument subRepDoc = _physicalCash.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_CVR_DET"].SetDataSource(AUD_CVR_DET);
                    }
                    if (_cs.SubreportName == "Denomination")
                    {
                        ReportDocument subRepDoc = _physicalCash.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_CVR_DENO"].SetDataSource(AUD_CVR_DENO);
                    }
                    if (_cs.SubreportName == "Acc_Cash")
                    {
                        ReportDocument subRepDoc = _physicalCash.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_CVR_CASH"].SetDataSource(AUD_CVR_CASH);
                    }
                    if (_cs.SubreportName == "AccDet")
                    {
                        ReportDocument subRepDoc = _physicalCash.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_CVR_CASH"].SetDataSource(AUD_CVR_CASH);
                    }
                    if (_cs.SubreportName == "remsum")
                    {
                        ReportDocument subRepDoc = _physicalCash.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["GNT_REM_SUM"].SetDataSource(AUD_REM_DET);
                    }
                }
            }

        }

        public void EliteCommR1DReport()
        {// Sanjeewa 11-10-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable COMMDTL = bsObj.CHNLSVC.MsgPortal.GetCommDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDoc, BaseCls.GlbUserID, 0);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("circularno", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("year_", typeof(int));
            param.Columns.Add("month_", typeof(int));
            param.Columns.Add("period", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["circularno"] = BaseCls.GlbReportDoc;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["year_"] = BaseCls.GlbReportYear;
            dr["month_"] = BaseCls.GlbReportMonth;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            param.Rows.Add(dr);

            _EComm_R1D.Database.Tables["ELITE_COMM_INVOICE"].SetDataSource(COMMDTL);
            _EComm_R1D.Database.Tables["param"].SetDataSource(param);
        }

        public void HPGroupCommissionDReport()
        {// Sanjeewa 23-10-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable HP_GRP_COMM = bsObj.CHNLSVC.MsgPortal.GetHPGroupCommissionDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportDocType);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("doc_type", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["doc_type"] = BaseCls.GlbReportDocType;
            param.Rows.Add(dr);

            _HPGRPComm.Database.Tables["HP_GRP_COMM"].SetDataSource(HP_GRP_COMM);
            _HPGRPComm.Database.Tables["param"].SetDataSource(param);
        }
        public void NotRealizedTransactionReport()
        {
            DataTable param = new DataTable();
            DataRow dr;

            DataTable HP_RLZ_TRNS = bsObj.CHNLSVC.Sales.GetNotRealizeTransactionDet(BaseCls.GlbUserComCode, BaseCls.GlbReportDoc, Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportIsAsAt);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("doc_type", typeof(string));
            param.Columns.Add("todate", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            dr = param.NewRow();
         
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            if (BaseCls.GlbReportDoc == "ALL")
            {
                dr["doc_type"] = BaseCls.GlbReportDoc;
            }
            else
            {
                DataTable _result = new DataTable();
                _result = bsObj.CHNLSVC.Financial.GET_ACC_DETAILS(BaseCls.GlbUserComCode, BaseCls.GlbReportDocType);
                if (_result.Rows.Count > 0)
                {
                    dr["doc_type"] = BaseCls.GlbReportDocType + " "+ "-"+ _result.Rows[0]["Description"].ToString();
                }
                else
                {

                    dr["doc_type"] = BaseCls.GlbReportDocType;
                }
            }
            dr["todate"] = Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            param.Rows.Add(dr);

            _NOT_RLZ_TRNZ.Database.Tables["NRT_DT"].SetDataSource(HP_RLZ_TRNS);
            _NOT_RLZ_TRNZ.Database.Tables["param"].SetDataSource(param);
        }

        public void LoadBankStatement()
        {
            DataTable param = new DataTable();
            DataRow dr;

            DataTable dtBank = bsObj.CHNLSVC.General.get_All_Bnk_StmDetails(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportDoc);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            //param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("doc_type", typeof(string));
            //param.Columns.Add("todate", typeof(string));
            //param.Columns.Add("comp", typeof(string));
            //param.Columns.Add("compaddr", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            //dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["doc_type"] = BaseCls.GlbReportDoc;
           // dr["todate"] = Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date;
           // dr["comp"] = BaseCls.GlbReportComp;
           // dr["compaddr"] = BaseCls.GlbReportCompAddr;
            param.Rows.Add(dr);

            _bnkste.Database.Tables["Bnk_Ste"].SetDataSource(dtBank);
            _bnkste.Database.Tables["param"].SetDataSource(param);
        }
                
        public void CrcdReconciliationReport()
        {
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Financial.CrcdReconciliationDetails(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbReportDoc, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID);           
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }           

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("account", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["account"] = BaseCls.GlbReportDoc;
            param.Rows.Add(dr);

            _crcdrecon.Database.Tables["CRCD_RECON"].SetDataSource(GLOB_DataTable);
            _crcdrecon.Database.Tables["param"].SetDataSource(param);
        }

        public void CrBalanceDReport()
        {// Sanjeewa 07-11-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable GLB_CRBAL_REP = bsObj.CHNLSVC.MsgPortal.GetCrBalanceDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID);

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

            _CrBal.Database.Tables["GLB_CRBAL_REP"].SetDataSource(GLB_CRBAL_REP);
            _CrBal.Database.Tables["param"].SetDataSource(param);
        }

        public void ExcessShortSummary()
        {
            // kapila 
            DataTable glb_cashcontrol = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

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


            glb_cashcontrol.Clear();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Financial.CashControlReconPrint(BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportFromDate);
                    glb_cashcontrol.Merge(TMP_DataTable);
                }
            }

            _excsShortSumm.Database.Tables["Cash_Control_Recon"].SetDataSource(glb_cashcontrol);
            _excsShortSumm.Database.Tables["param"].SetDataSource(param);
        }
        public void CashControlRecon()
        {
            // kapila 
            DataTable glb_cashcontrol = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

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


            glb_cashcontrol.Clear();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Financial.CashControlReconPrint(BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(),  BaseCls.GlbReportFromDate);                
                    glb_cashcontrol.Merge(TMP_DataTable);
                }
            }

            _cashControlRecon.Database.Tables["Cash_Control_Recon"].SetDataSource(glb_cashcontrol);
            _cashControlRecon.Database.Tables["param"].SetDataSource(param);
        }

        public void RcvDeskProcessedReport()
        {// Sanjeewa 10-07-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Financial.GetRcvDskProcessed(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
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

            _remsunprcs.Database.Tables["RCV_DSK_PROCESSED"].SetDataSource(GLOB_DataTable);
            _remsunprcs.Database.Tables["param"].SetDataSource(param);
        }

        public void EliteCommR2Report()
        {// Sanjeewa 11-07-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable COMMDTL = bsObj.CHNLSVC.MsgPortal.GetEliteCommDetails(BaseCls.GlbReportDoc, Convert.ToInt16(BaseCls.GlbReportYear), Convert.ToInt16(BaseCls.GlbReportMonth), BaseCls.GlbUserID);
            DataTable COMMPC = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbUserComCode, "");
            DataTable COMMCOM = bsObj.CHNLSVC.MsgPortal.GetCompanyTable();
            DataTable COMMEMP = bsObj.CHNLSVC.MsgPortal.GetEmployeeTable(BaseCls.GlbUserComCode);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("circularno", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("year_", typeof(int));
            param.Columns.Add("month_", typeof(int));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["circularno"] = BaseCls.GlbReportDoc;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["year_"] = BaseCls.GlbReportYear;
            dr["month_"] = BaseCls.GlbReportMonth;
            param.Rows.Add(dr);

            _EComm_R2.Database.Tables["SAT_ELITE_COMM"].SetDataSource(COMMDTL);
            _EComm_R2.Database.Tables["MST_PROFIT_CENTER"].SetDataSource(COMMPC);
            _EComm_R2.Database.Tables["MST_COM"].SetDataSource(COMMCOM);
            _EComm_R2.Database.Tables["MST_EMP"].SetDataSource(COMMEMP);
            _EComm_R2.Database.Tables["param"].SetDataSource(param);
        }

        public void RemitanceCheckList()
        {
            // kapila 19/4/2013
            DataTable remCheckList = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

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


           // remCheckList.Clear();
          //  remCheckList = bsObj.CHNLSVC.Financial.RemitanceCheckListPrint(BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);

            //kapila 10/3/2017
            DataTable tmp_user_pc = new DataTable();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable _temp = new DataTable();
                    _temp = bsObj.CHNLSVC.Financial.RemitanceCheckListPrint(BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);
                    remCheckList.Merge(_temp);
                }
            }

            _remitCheckList.Database.Tables["Rem_Check_List"].SetDataSource(remCheckList);
            _remitCheckList.Database.Tables["param"].SetDataSource(param);
        }

        public void CashControlCash()
        {
            // kapila 11/11/2014
            DataTable cashcontrol = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

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


            cashcontrol.Clear();
            cashcontrol = bsObj.CHNLSVC.Financial.CashControlCashPrint(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, BaseCls.GlbReportYear, BaseCls.GlbReportMonth, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate);


            _cashControlCash.Database.Tables["Cash_Control_Cash"].SetDataSource(cashcontrol);
            _cashControlCash.Database.Tables["param"].SetDataSource(param);
        }

        public void CashControl()
        {
            // kapila 19/4/2013
            DataTable cashcontrol = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

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


            cashcontrol.Clear();
            cashcontrol = bsObj.CHNLSVC.Financial.CashControlPrint(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, BaseCls.GlbReportYear, BaseCls.GlbReportMonth, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate,BaseCls.GlbReportStrStatus);


            _cashControl.Database.Tables["Cash_Control"].SetDataSource(cashcontrol);
            _cashControl.Database.Tables["param"].SetDataSource(param);
        }

        //dilshan on 20/08/2018
        public void ManagerCommControl(int isSummary)
        {
            DataRow dr;
            DataTable dtManagerCommReport = new DataTable();
            DataTable dtManagerCBonusReport = new DataTable();
            DataTable dtManagerCode = new DataTable();
            DataTable dtManagerProfit = new DataTable();
            DataTable dtManagerFinal = new DataTable();

            string repType = string.Empty;

            if (isSummary == 0)
            { repType = "ManagerCommissionDetail"; }
            else { repType = "ManagerCommissionSummary"; }


            DataTable dtManagerCommReportAll = new DataTable();

            dtManagerCode.Columns.Add("mepf", typeof(string));
            dtManagerCode.Columns.Add("esep_name_initials", typeof(string));

            dtManagerCommReportAll.Columns.Add("mepf", typeof(string));
            dtManagerCommReportAll.Columns.Add("esep_name_initials", typeof(string));
            //dtManagerCommReportAll.Columns.Add("rem_val_final", typeof(double));
            //dtManagerCommReportAll.Columns.Add("mpc_desc", typeof(string));
            dtManagerCommReportAll.Columns.Add("rem_pc", typeof(string));
            //dtManagerCommReportAll.Columns.Add("ctype", typeof(string));

            dtManagerFinal.Columns.Add("rem_com", typeof(string));
            dtManagerFinal.Columns.Add("rem_pc", typeof(string));
            dtManagerFinal.Columns.Add("mpc_desc", typeof(string));
            dtManagerFinal.Columns.Add("esep_name_initials", typeof(string));
            dtManagerFinal.Columns.Add("esep_epf", typeof(string));
            dtManagerFinal.Columns.Add("rem_val_final", typeof(double));
            dtManagerFinal.Columns.Add("rem_dt", typeof(DateTime));
            dtManagerFinal.Columns.Add("rem_bonusval_final", typeof(double));
            dtManagerFinal.Columns.Add("rem_firstdt", typeof(DateTime));
            dtManagerFinal.Columns.Add("rem_lastdt", typeof(DateTime));

            // dtManagerCode.Columns.Add("pft", typeof(string));
            DataTable tmp_user_pc = new DataTable();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportComp, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable dtManagerCommReport_temp = new DataTable();
                    DataTable dtManagerCBonusReport_temp = new DataTable();
                    dtManagerCommReport_temp = bsObj.CHNLSVC.MsgPortal.ProcessManagerCommission(drow["tpl_pc"].ToString(), BaseCls.GlbUserID, string.Empty, BaseCls.GlbReportComp, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate));
                    dtManagerCBonusReport_temp = bsObj.CHNLSVC.MsgPortal.ProcessManagerCollBonus(drow["tpl_pc"].ToString(), BaseCls.GlbUserID, string.Empty, BaseCls.GlbReportComp, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate));

                    dtManagerCommReport.Merge(dtManagerCommReport_temp);
                    dtManagerCBonusReport.Merge(dtManagerCBonusReport_temp);

                }
            }

            //dilshan on 20/08/2018***************
            foreach (DataRow row in dtManagerCommReport.Rows)
            {
                foreach (DataRow row1 in dtManagerCBonusReport.Rows)
                {
                    if (row["rem_pc"].ToString() == row1["rem_pc"].ToString() && row["rem_dt"].ToString() == row1["rem_dt"].ToString())
                    {
                        DateTime date = DateTime.Parse(row["rem_dt"].ToString());

                        var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                        dr = dtManagerFinal.NewRow();
                        dr["rem_com"] = row["rem_com"].ToString();
                        dr["rem_pc"] = row["rem_pc"].ToString();
                        dr["mpc_desc"] = row["mpc_desc"].ToString();
                        dr["esep_name_initials"] = row["esep_name_initials"].ToString();
                        dr["esep_epf"] = row["esep_epf"].ToString();
                        dr["rem_val_final"] = double.Parse(row["rem_val_final"].ToString());
                        //dr["rem_dt"] = DateTime.Parse(row["rem_dt"].ToString());
                        dr["rem_dt"] = DateTime.Parse(firstDayOfMonth.ToString());
                        dr["rem_bonusval_final"] = double.Parse(row1["rem_val_final"].ToString());
                        dr["rem_firstdt"] = DateTime.Parse(firstDayOfMonth.ToString());
                        dr["rem_lastdt"] = DateTime.Parse(lastDayOfMonth.ToString());
                        dtManagerFinal.Rows.Add(dr);
                    }
                }
            }
            //************************************
            //////////////////////////
            DataTable cashcontrol = new DataTable();
            DataTable param = new DataTable();

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


            //cashcontrol.Clear();
            //cashcontrol = bsObj.CHNLSVC.Financial.CashControlPrint(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, BaseCls.GlbReportYear, BaseCls.GlbReportMonth, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportStrStatus);


            _managerComm.Database.Tables["Manager_Comm"].SetDataSource(dtManagerFinal);
            _managerComm.Database.Tables["param"].SetDataSource(param);
        }

        public void ManualDocumentCheckList()
        {
            // kapila 19/4/2013
            DataTable ManDocCheckList = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

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


            ManDocCheckList.Clear();
            ManDocCheckList = bsObj.CHNLSVC.Financial.GetManualDocCheckListPrint(BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbReportWeek);


            _docCheckList.Database.Tables["Doc_Check_List"].SetDataSource(ManDocCheckList);
            _docCheckList.Database.Tables["param"].SetDataSource(param);
        }

        //kapila 9/4/2013
        public void ShortRemStatementPrint()
        {
            DataTable mst_com = new DataTable();
            DataTable _glb_short_rem = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            mst_com.Clear();
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            param.Rows.Add(dr);

            MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

            dr = mst_com.NewRow();
            dr["MC_CD"] = BaseCls.GlbReportCompCode;
            dr["MC_IT_POWERED"] = _MasterComp.Mc_it_powered.ToString();
            mst_com.Rows.Add(dr);

            _glb_short_rem = bsObj.CHNLSVC.Financial.Process_Short_Rem_statement(BaseCls.GlbUserID, BaseCls.GlbUserComCode, Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbReportIsAsAt, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate);

            _ShortRem.Database.Tables["param"].SetDataSource(param);
            _ShortRem.Database.Tables["glb_short_rem"].SetDataSource(_glb_short_rem);

        }

        public void ExcessRemStatementPrint()
        {   //Nadeeka 31/1/2014
            DataTable mst_com = new DataTable();
            DataTable _glb_short_rem = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            mst_com.Clear();
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            param.Rows.Add(dr);

            MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

            dr = mst_com.NewRow();
            dr["MC_CD"] = BaseCls.GlbReportCompCode;   // row["MC_CD"].ToString();
            dr["MC_IT_POWERED"] = _MasterComp.Mc_it_powered.ToString();
            mst_com.Rows.Add(dr);

            _glb_short_rem = bsObj.CHNLSVC.Financial.Process_Excess_Rem_statement(BaseCls.GlbUserID, BaseCls.GlbUserComCode, Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbReportIsAsAt, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate);

            _ExcessRem.Database.Tables["param"].SetDataSource(param);
            _ExcessRem.Database.Tables["glb_short_rem"].SetDataSource(_glb_short_rem);

        }
        public int PersonalChequeStatementRep()
        {
            // Nadeeka 20-03-2013
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;
            int rowc = 0;

            DataTable PersonalChequesStatment = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("SaleType", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            if (BaseCls.GlbPayType != null)
            {

                dr["SaleType"] = BaseCls.GlbPayType;
            }
            else
            {
                dr["SaleType"] = "ALL";

            }
            param.Rows.Add(dr);

            DataTable tempDTB = new DataTable();
            tempDTB.Clear();
            PersonalChequesStatment.Clear();

            DataTable tmp_user_pc = new DataTable();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportComp, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable _temp = new DataTable();
                    _temp = bsObj.CHNLSVC.Financial.ProcessPersonalChequeStatement(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString());
                    tempDTB.Merge(_temp); 
                }
            }

            if (BaseCls.GlbPayType != null)
            {
                DataRow[] dr2 = tempDTB.Select("(sapt_cd ='" + BaseCls.GlbPayType + "') ");
                if (dr2.Count() > 0)
                {
                    PersonalChequesStatment = tempDTB.Select("(sapt_cd ='" + BaseCls.GlbPayType + "') ").CopyToDataTable();
                    MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

                    _chqSts.Database.Tables["PersonalChequesStatment"].SetDataSource(PersonalChequesStatment);


                    _chqSts.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                    _chqSts.Database.Tables["param"].SetDataSource(param);
                    rowc = 1;
                }
                //else
                //    {
                //           PersonalChequesStatment = tempDTB;
                //    }

            }
            else
            {
                PersonalChequesStatment = tempDTB;
                MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

                _chqSts.Database.Tables["PersonalChequesStatment"].SetDataSource(PersonalChequesStatment);


                _chqSts.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _chqSts.Database.Tables["param"].SetDataSource(param);
                rowc = 1;
            }
            return rowc;


        }

        public void DailyExpences()
        {
            // Nadeeka 20-03-2013
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;


            DataTable gnt_rem_sum = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();
            DataRow dr;

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


            gnt_rem_sum.Clear();

             DataTable tmp_user_pc = new DataTable();
             tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable gnt_rem_sum_temp = new DataTable();
                    gnt_rem_sum_temp = bsObj.CHNLSVC.Sales.getDailyExpences(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportCompCode, BaseCls.GlbRecType, drow["tpl_pc"].ToString());
                    gnt_rem_sum.Merge(gnt_rem_sum_temp);
                }
            }
                    
                    MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            _dailyExp.Database.Tables["gnt_rem_sum"].SetDataSource(gnt_rem_sum);


            _dailyExp.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _dailyExp.Database.Tables["param"].SetDataSource(param);
        }


        public void ADLoanSettReport()
        {// Sanjeewa 05-04-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable ADLOANSETT = bsObj.CHNLSVC.Financial.GetADLoanSettDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID);

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

            _adLoanSett.Database.Tables["AD_LOAN_SETT"].SetDataSource(ADLOANSETT);
            _adLoanSett.Database.Tables["param"].SetDataSource(param);
        }

        public void ClaimExpVoucherReport()
        {// Sanjeewa 08-04-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable CLAIMEXPVOU = bsObj.CHNLSVC.Financial.GetClaimExpVoucherDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID);

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

            _claimExpVou.Database.Tables["EXP_VOUCHER"].SetDataSource(CLAIMEXPVOU);
            _claimExpVou.Database.Tables["param"].SetDataSource(param);
        }
        public void ManagerIncomeReport()
        {// Dilshan 21/08/2018
            DataRow dr;
            DataTable dtManagerCommReport = new DataTable();
            DataTable dtManagerCBonusReport = new DataTable();
            DataTable dtManagerCode = new DataTable();
            DataTable dtManagerProfit = new DataTable();
            DataTable dtManagerFinal = new DataTable();

            string repType = string.Empty;

            //if (isSummary == 0)
            //{ repType = "ManagerCommissionDetail"; }
            //else { repType = "ManagerCommissionSummary"; }


            DataTable dtManagerCommReportAll = new DataTable();

            dtManagerCode.Columns.Add("mepf", typeof(string));
            dtManagerCode.Columns.Add("esep_name_initials", typeof(string));

            dtManagerCommReportAll.Columns.Add("mepf", typeof(string));
            dtManagerCommReportAll.Columns.Add("esep_name_initials", typeof(string));
            //dtManagerCommReportAll.Columns.Add("rem_val_final", typeof(double));
            //dtManagerCommReportAll.Columns.Add("mpc_desc", typeof(string));
            dtManagerCommReportAll.Columns.Add("rem_pc", typeof(string));
            //dtManagerCommReportAll.Columns.Add("ctype", typeof(string));

            dtManagerFinal.Columns.Add("rem_com", typeof(string));
            dtManagerFinal.Columns.Add("rem_pc", typeof(string));
            dtManagerFinal.Columns.Add("mpc_desc", typeof(string));
            dtManagerFinal.Columns.Add("esep_name_initials", typeof(string));
            dtManagerFinal.Columns.Add("esep_epf", typeof(string));
            dtManagerFinal.Columns.Add("rem_val_final", typeof(double));
            dtManagerFinal.Columns.Add("rem_dt", typeof(DateTime));
            dtManagerFinal.Columns.Add("rem_bonusval_final", typeof(double));
            dtManagerFinal.Columns.Add("rem_firstdt", typeof(DateTime));
            dtManagerFinal.Columns.Add("rem_lastdt", typeof(DateTime));

            // dtManagerCode.Columns.Add("pft", typeof(string));
            DataTable tmp_user_pc = new DataTable();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportComp, BaseCls.GlbUserID);



            DateTime date1 = DateTime.Parse((BaseCls.GlbReportAsAtDate).ToString());
            var firstDayOfMonth1 = new DateTime(date1.Year, date1.Month, 1);
            var lastDayOfMonth1 = firstDayOfMonth1.AddMonths(1).AddDays(-1);

            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1); 
            var first = month.AddMonths(-6);
            var last = month.AddDays(-1);

            //DateTime dt = new DateTime(2000, 1, 1);
            int prevMonth = today.AddMonths(-1).Month;
            var month1 = new DateTime(today.Year, today.Month, 1);
            var first1 = month.AddMonths(-6);
            var last1 = month.AddDays(-1);
            string q=BaseCls.GlbReportExecCode;
            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable dtManagerCommReport_temp = new DataTable();
                    DataTable dtManagerCBonusReport_temp = new DataTable();
                    dtManagerCommReport_temp = bsObj.CHNLSVC.MsgPortal.ProcessManagerCommission(drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbReportExecCode, BaseCls.GlbReportComp, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate));
                    dtManagerCBonusReport_temp = bsObj.CHNLSVC.MsgPortal.ProcessManagerCollBonus(drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbReportExecCode, BaseCls.GlbReportComp, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate));
                    //dtManagerCommReport_temp = bsObj.CHNLSVC.MsgPortal.ProcessManagerCommission(drow["tpl_pc"].ToString(), BaseCls.GlbUserID, string.Empty, BaseCls.GlbReportComp, Convert.ToDateTime(first), Convert.ToDateTime(last));
                    //dtManagerCBonusReport_temp = bsObj.CHNLSVC.MsgPortal.ProcessManagerCollBonus(drow["tpl_pc"].ToString(), BaseCls.GlbUserID, string.Empty, BaseCls.GlbReportComp, Convert.ToDateTime(first), Convert.ToDateTime(last));

                    dtManagerCommReport.Merge(dtManagerCommReport_temp);
                    dtManagerCBonusReport.Merge(dtManagerCBonusReport_temp);

                }
            }

            //***************
            foreach (DataRow row in dtManagerCommReport.Rows)
            {
                foreach (DataRow row1 in dtManagerCBonusReport.Rows)
                {
                    if (row["rem_pc"].ToString() == row1["rem_pc"].ToString() && row["rem_dt"].ToString() == row1["rem_dt"].ToString())
                    {
                        DateTime date = DateTime.Parse(row["rem_dt"].ToString());

                        var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                        dr = dtManagerFinal.NewRow();
                        dr["rem_com"] = row["rem_com"].ToString();
                        dr["rem_pc"] = row["rem_pc"].ToString();
                        dr["mpc_desc"] = row["mpc_desc"].ToString();
                        dr["esep_name_initials"] = row["esep_name_initials"].ToString();
                        dr["esep_epf"] = row["esep_epf"].ToString();
                        dr["rem_val_final"] = double.Parse(row["rem_val_final"].ToString());
                        dr["rem_dt"] = DateTime.Parse(row["rem_dt"].ToString());
                        dr["rem_bonusval_final"] = double.Parse(row1["rem_val_final"].ToString());
                        dr["rem_firstdt"] = DateTime.Parse(firstDayOfMonth.ToString());
                        dr["rem_lastdt"] = DateTime.Parse(lastDayOfMonth.ToString());
                        dtManagerFinal.Rows.Add(dr);
                        dtManagerFinal.Rows[0]["rem_lastdt"] = DateTime.Parse(lastDayOfMonth.ToString());
                    }
                }
            }

            //************************************
            //////////////////////////
            DataTable cashcontrol = new DataTable();
            DataTable param = new DataTable();

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
            //dr["comp"] = BaseCls.GlbReportComp;
            dr["comp"] = BaseCls.GlbSelectData;
            //dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["compaddr"] = BaseCls.GlbReportDoc;
            param.Rows.Add(dr);


            //cashcontrol.Clear();
            //cashcontrol = bsObj.CHNLSVC.Financial.CashControlPrint(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, BaseCls.GlbReportYear, BaseCls.GlbReportMonth, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportStrStatus);


            _managerIncome.Database.Tables["Manager_Comm"].SetDataSource(dtManagerFinal);
            _managerIncome.Database.Tables["param"].SetDataSource(param);
        }
        public void RealizationFinalizeReport()
        {
            DataTable param = new DataTable();
            DataRow dr;
            DataTable RLZ_FNLZ = bsObj.CHNLSVC.Sales.Realization_Finaliz_sts(BaseCls.GlbReportDoc, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbUserComCode);
            DataTable MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            param.Clear();


            param.Columns.Add("BankAccount", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("fromdate", typeof(string));
            param.Columns.Add("todate", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("user", typeof(string));
            dr = param.NewRow();
            
            dr["BankAccount"] = BaseCls.GlbReportDocType;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["user"] = BaseCls.GlbReportUser;
            param.Rows.Add(dr);

            _RLZ_FINZ.Database.Tables["RLZ_FINALZ"].SetDataSource(RLZ_FNLZ);
            _RLZ_FINZ.Database.Tables["param"].SetDataSource(param);
            _RLZ_FINZ.Database.Tables["mst_com"].SetDataSource(MST_COM);
        }

        public void BankReconciliationReport()
        {
            DataTable param = new DataTable();
            DataRow dr;
            DataTable BNK_REC = new DataTable();
            BNK_REC.Clear();
            BNK_REC = bsObj.CHNLSVC.Sales.Bank_Reconciliation_Rpt(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbReportDoc);
            DataTable MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            param.Clear();


            
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("Asatadate", typeof(string));
            param.Columns.Add("AccNo", typeof(string));
            param.Columns.Add("user", typeof(string));
            dr = param.NewRow();
            
           
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["Asatadate"] = BaseCls.GlbReportAsAtDate;
            dr["AccNo"] = BaseCls.GlbReportDocType;
            dr["user"] = BaseCls.GlbUserID;
            param.Rows.Add(dr);

            //_BNK_RECON.Database.Tables["DT_BNK_REC"].SetDataSource(BNK_REC);
            //_BNK_RECON.Database.Tables["param"].SetDataSource(param);
            //_BNK_RECON.Database.Tables["mst_com"].SetDataSource(MST_COM);

            _BNK_RECONnew.Database.Tables["DT_BNK_REC"].SetDataSource(BNK_REC);
            _BNK_RECONnew.Database.Tables["param"].SetDataSource(param);
            _BNK_RECONnew.Database.Tables["mst_com"].SetDataSource(MST_COM);
            
        }
        public void ProcessManagerReport(int isSummary)
        {
            DataRow dr;
            DataTable dtManagerCommReport = new DataTable();
            DataTable dtManagerCBonusReport = new DataTable();
            DataTable dtManagerCode = new DataTable();
            DataTable dtManagerProfit = new DataTable();
            DataTable dtManagerFinal = new DataTable();
            Double _amAmt = 0;
            Double _ambAmt = 0;
            Double _mgrCnt = 3;
            Double _mgrCnttO = 0;
            Double G = 0;
            Double H = 0;
            Double I = 0;
            Double J = 0;
            Double K = 0;
            Double L = 0;
            Double M = 0;
            Double N = 0;
            Double P = 0;
            Double Q = 0;
            Double R = 0;

            Double G1 = 0;
            Double H1 = 0;
            Double I1 = 0;
            Double J1 = 0;
            Double K1 = 0;
            Double L1 = 0;
            Double M1 = 0;
            Double N1 = 0;
            Double P1 = 0;
            Double Q1 = 0;
            Double R1 = 0;
            string repType = string.Empty;

            if (isSummary == 0)
            { repType = "ManagerCommissionDetail"; }
            else { repType = "ManagerCommissionSummary"; }


            DataTable dtManagerCommReportAll = new DataTable();

            dtManagerCode.Columns.Add("mepf", typeof(string));
            dtManagerCode.Columns.Add("esep_name_initials", typeof(string));

            dtManagerCommReportAll.Columns.Add("mepf", typeof(string));
            dtManagerCommReportAll.Columns.Add("esep_name_initials", typeof(string));
            //dtManagerCommReportAll.Columns.Add("rem_val_final", typeof(double));
            //dtManagerCommReportAll.Columns.Add("mpc_desc", typeof(string));
            dtManagerCommReportAll.Columns.Add("rem_pc", typeof(string));
            //dtManagerCommReportAll.Columns.Add("ctype", typeof(string));

            dtManagerFinal.Columns.Add("rem_com", typeof(string));
            dtManagerFinal.Columns.Add("rem_pc", typeof(string));
            dtManagerFinal.Columns.Add("mpc_desc", typeof(string));
            dtManagerFinal.Columns.Add("esep_name_initials", typeof(string));
            dtManagerFinal.Columns.Add("esep_epf", typeof(string));
            dtManagerFinal.Columns.Add("rem_val_final", typeof(double));
            dtManagerFinal.Columns.Add("rem_dt", typeof(DateTime));
            dtManagerFinal.Columns.Add("rem_bonusval_final", typeof(double));

            // dtManagerCode.Columns.Add("pft", typeof(string));
            DataTable tmp_user_pc = new DataTable();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportComp, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable dtManagerCommReport_temp = new DataTable();
                    DataTable dtManagerCBonusReport_temp = new DataTable();
                    dtManagerCommReport_temp = bsObj.CHNLSVC.MsgPortal.ProcessManagerCommission(drow["tpl_pc"].ToString(), BaseCls.GlbUserID, string.Empty, BaseCls.GlbReportComp, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate));
                    dtManagerCBonusReport_temp = bsObj.CHNLSVC.MsgPortal.ProcessManagerCollBonus(drow["tpl_pc"].ToString(), BaseCls.GlbUserID, string.Empty, BaseCls.GlbReportComp, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate));

                    dtManagerCommReport.Merge(dtManagerCommReport_temp);
                    dtManagerCBonusReport.Merge(dtManagerCBonusReport_temp);

                }
            }

            //dilshan on 20/08/2018***************
            foreach (DataRow row in dtManagerCommReport.Rows)
            {
                foreach (DataRow row1 in dtManagerCBonusReport.Rows)
                { 
                    if (row["rem_pc"].ToString() == row1["rem_pc"].ToString() && row["rem_dt"].ToString() == row1["rem_dt"].ToString())
                    {
                        dr = dtManagerFinal.NewRow();
                        dr["rem_com"] = row["rem_com"].ToString();
                        dr["rem_pc"] = row["rem_pc"].ToString();
                        dr["mpc_desc"] = row["mpc_desc"].ToString();
                        dr["esep_name_initials"] = row["esep_name_initials"].ToString();
                        dr["esep_epf"] = row["esep_epf"].ToString();
                        dr["rem_val_final"] = double.Parse(row["rem_val_final"].ToString());
                        dr["rem_dt"] = DateTime.Parse(row["rem_dt"].ToString());
                        dr["rem_bonusval_final"] = double.Parse(row1["rem_val_final"].ToString()); 
                        dtManagerFinal.Rows.Add(dr);
                    }
                }
            }
            //************************************


            foreach (DataRow row in dtManagerCommReport.Rows)
            {
                if (double.Parse(row["rem_val_final"].ToString()) > 0)
                {
                    dr = dtManagerCode.NewRow();
                    dr["mepf"] = row["esep_epf"].ToString();
                    dr["esep_name_initials"] = row["esep_name_initials"].ToString();
                    //   dr["pft"] = row["rem_pc"].ToString(); 
                    dtManagerCode.Rows.Add(dr);


                    dr = dtManagerCommReportAll.NewRow();
                    dr["mepf"] = row["esep_epf"].ToString();
                    dr["esep_name_initials"] = row["esep_name_initials"].ToString();
                    //dr["rem_val_final"] = row["rem_val_final"].ToString();
                    //dr["mpc_desc"] = row["mpc_desc"].ToString();
                    dr["rem_pc"] = row["rem_pc"].ToString();
                    //dr["rem_pc"] = "C";
                    dtManagerCommReportAll.Rows.Add(dr);
                }
            }

            foreach (DataRow row in dtManagerCBonusReport.Rows)
            {
                if (double.Parse(row["rem_val_final"].ToString()) > 0)
                {
                    dr = dtManagerCode.NewRow();
                    dr["mepf"] = row["esep_epf"].ToString();
                    dr["esep_name_initials"] = row["esep_name_initials"].ToString();
                    //  dr["pft"] = row["rem_pc"].ToString();
                    dtManagerCode.Rows.Add(dr);


                    dr = dtManagerCommReportAll.NewRow();
                    dr["mepf"] = row["esep_epf"].ToString();
                    dr["esep_name_initials"] = row["esep_name_initials"].ToString();
                    //dr["rem_val_final"] = row["rem_val_final"].ToString();
                    //dr["mpc_desc"] = row["mpc_desc"].ToString();
                    dr["rem_pc"] = row["rem_pc"].ToString();
                    //dr["rem_pc"] = "C";
                    dtManagerCommReportAll.Rows.Add(dr);
                }
            }
            if (dtManagerCode.Rows.Count > 0)
            {
                dtManagerCode = dtManagerCode.DefaultView.ToTable(true);
            }
            if (dtManagerCommReportAll.Rows.Count > 0)
            {
                dtManagerCommReportAll = dtManagerCommReportAll.DefaultView.ToTable(true);
            }
            //  List<MasterCompany> _list = bsObj.CHNLSVC.General.GetALLMasterCompaniesData( );




            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = (Excel.Workbook)excelApp.Workbooks.Add(Missing.Value);
            Excel.Worksheet worksheet;

            // Opening excel file

            string _repPath = "";
            MasterCompany _masterComp = null;
            _masterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

            if (_masterComp != null)
            {
                _repPath = _masterComp.Mc_anal16;

            }

            workbook.SaveAs(_repPath + repType + BaseCls.GlbUserID + ".xls", Excel.XlPlatform.xlWindows, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            workbook.Close(true, Missing.Value, Missing.Value);
            workbook = excelApp.Workbooks.Open(_repPath + repType + BaseCls.GlbUserID + ".xls", 0, false, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            // Get first Worksheet
            worksheet = (Excel.Worksheet)workbook.Sheets.get_Item(1);
            ((Excel.Range)worksheet.Cells[1, "B"]).Value2 = "Period From " + Convert.ToDateTime(BaseCls.GlbReportFromDate) + "To " + Convert.ToDateTime(BaseCls.GlbReportToDate).Date;
            if (isSummary == 0)
            { ((Excel.Range)worksheet.Cells[1, "A"]).Value2 = "Manager Commission Statement"; }
            else { ((Excel.Range)worksheet.Cells[1, "A"]).Value2 = "Manager Commission Statement(Summary)"; }


            ((Excel.Range)worksheet.Cells[1, "C"]).Value2 = "User :" + BaseCls.GlbUserID + " On " + DateTime.Now;


            Int16 rwcount = 3;
            ((Excel.Range)worksheet.Cells[2, "A"]).Value2 = "EPF No";
            ((Excel.Range)worksheet.Cells[2, "B"]).Value2 = "S/R Manager Name";
            ((Excel.Range)worksheet.Cells[2, "D"]).Value2 = "S/R Name";



            //foreach (MasterCompany _cCode in _list)
            //{
            if (isSummary == 0)
            {
                //  if (_cCode.Mc_anal11 == 1) {
                ((Excel.Range)worksheet.Cells[2, "E"]).Value2 = "Total Comm " + _masterComp.Mc_desc; ((Excel.Range)worksheet.Cells[2, "C"]).Value2 = _masterComp.Mc_desc;
                // }
                //if (_cCode.Mc_anal11 == 2) { ((Excel.Range)worksheet.Cells[2, "H"]).Value2 = "Total Comm " + _cCode.Mc_desc; ((Excel.Range)worksheet.Cells[2, "D"]).Value2 = _cCode.Mc_desc; }
                //if (_cCode.Mc_anal11 == 3) { ((Excel.Range)worksheet.Cells[2, "I"]).Value2 = "Total Comm " + _cCode.Mc_desc; ((Excel.Range)worksheet.Cells[2, "E"]).Value2 = _cCode.Mc_desc; }

                //  if (_cCode.Mc_anal11 == 1) {
                ((Excel.Range)worksheet.Cells[2, "F"]).Value2 = "Collection Bonus " + _masterComp.Mc_desc; ((Excel.Range)worksheet.Cells[2, "C"]).Value2 = string.Empty;
                //  }
                //if (_cCode.Mc_anal11 == 2) { ((Excel.Range)worksheet.Cells[2, "L"]).Value2 = "Collection Bonus  " + _cCode.Mc_desc; }
                //if (_cCode.Mc_anal11 == 3) { ((Excel.Range)worksheet.Cells[2, "M"]).Value2 = "Collection Bonus " + _cCode.Mc_desc; }
            }
            else
            {
                //  if (_cCode.Mc_anal11 == 1) {

                ((Excel.Range)worksheet.Cells[2, "E"]).Value2 = "Total Comm " + _masterComp.Mc_desc;
                //  }
                //if (_cCode.Mc_anal11 == 2) { ((Excel.Range)worksheet.Cells[2, "H"]).Value2 = "Total Comm " + _cCode.Mc_desc;  }
                //if (_cCode.Mc_anal11 == 3) { ((Excel.Range)worksheet.Cells[2, "I"]).Value2 = "Total Comm " + _cCode.Mc_desc;   }

                // if (_cCode.Mc_anal11 == 1) {
                ((Excel.Range)worksheet.Cells[2, "F"]).Value2 = "Collection Bonus " + _masterComp.Mc_desc;
                //}
                //if (_cCode.Mc_anal11 == 2) { ((Excel.Range)worksheet.Cells[2, "L"]).Value2 = "Collection Bonus  " + _cCode.Mc_desc; }
                //if (_cCode.Mc_anal11 == 3) { ((Excel.Range)worksheet.Cells[2, "M"]).Value2 = "Collection Bonus " + _cCode.Mc_desc; }
            }

            //   }


            // ((Excel.Range)worksheet.Cells[2, "J"]).Value2 = "Total Comm";

            //  ((Excel.Range)worksheet.Cells[2, "N"]).Value2 = "Coll Bonus Total";
            ((Excel.Range)worksheet.Cells[2, "G"]).Value2 = "Adjustment";
            ((Excel.Range)worksheet.Cells[2, "H"]).Value2 = "Total Comm";
            ((Excel.Range)worksheet.Cells[2, "I"]).Value2 = "EPF 20%";
            ((Excel.Range)worksheet.Cells[2, "J"]).Value2 = "ETF 3%";
            ((Excel.Range)worksheet.Cells[2, "K"]).Value2 = "Commission Finalized Status";
            foreach (DataRow row1 in dtManagerCode.Rows)
            {
                //_amAmt = 0;
                //_ambAmt = 0;
                G1 = 0;
                H1 = 0;
                I1 = 0;
                J1 = 0;
                K1 = 0;
                L1 = 0;
                M1 = 0;
                N1 = 0;
                P1 = 0;
                Q1 = 0;
                R1 = 0;
                //foreach (MasterCompany _cCode in _list)
                //{


                //if (_cCode.Mc_anal11 != 0)
                //{
                if (isSummary == 0)
                {
                    //   if (_cCode.Mc_anal11 == 1) {
                    ((Excel.Range)worksheet.Cells[rwcount, "E"]).Value2 = 0;
                    //  }

                    //if (_cCode.Mc_anal11 == 2) { ((Excel.Range)worksheet.Cells[rwcount, "H"]).Value2 = 0; }

                    //if (_cCode.Mc_anal11 == 3) { ((Excel.Range)worksheet.Cells[rwcount, "I"]).Value2 = 0; }

                    //if (_cCode.Mc_anal11 == 1) { 
                    ((Excel.Range)worksheet.Cells[rwcount, "F"]).Value2 = 0;
                    //}

                    //if (_cCode.Mc_anal11 == 2) { ((Excel.Range)worksheet.Cells[rwcount, "L"]).Value2 = 0; }

                    //if (_cCode.Mc_anal11 == 3) { ((Excel.Range)worksheet.Cells[rwcount, "M"]).Value2 = 0; }
                }

                foreach (DataRow row2 in dtManagerCommReportAll.Rows)
                {
                    _amAmt = 0;
                    _ambAmt = 0;

                    dtManagerCommReport = bsObj.CHNLSVC.MsgPortal.ProcessManagerCommission(row2["rem_pc"].ToString(), BaseCls.GlbUserID, row1["mepf"].ToString(), _masterComp.Mc_cd, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate));
                    dtManagerCBonusReport = bsObj.CHNLSVC.MsgPortal.ProcessManagerCollBonus(row2["rem_pc"].ToString(), BaseCls.GlbUserID, row1["mepf"].ToString(), _masterComp.Mc_cd, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate));
                    foreach (DataRow row in dtManagerCommReport.Rows)
                    {
                        // Setting cell values
                        if (isSummary == 0)
                        {
                            ((Excel.Range)worksheet.Cells[rwcount, "D"]).Value2 = row["mpc_desc"].ToString();
                            ((Excel.Range)worksheet.Cells[rwcount, "B"]).Value2 = row["esep_name_initials"].ToString();
                            ((Excel.Range)worksheet.Cells[rwcount, "A"]).Value2 = row["esep_epf"].ToString();

                            //   if (_cCode.Mc_anal11 == 1) {
                            ((Excel.Range)worksheet.Cells[rwcount, "E"]).Value2 = row["rem_val_final"].ToString();
                            _amAmt += double.Parse(row["rem_val_final"].ToString());
                            ((Excel.Range)worksheet.Cells[rwcount, "C"]).Value2 = row["rem_pc"].ToString();
                            G += _amAmt;
                            // }

                            //if (_cCode.Mc_anal11 == 2) { ((Excel.Range)worksheet.Cells[rwcount, "H"]).Value2 = row["rem_val_final"].ToString(); _amAmt += double.Parse(row["rem_val_final"].ToString()); ((Excel.Range)worksheet.Cells[rwcount, "D"]).Value2 = row["rem_pc"].ToString(); H += _amAmt; }

                            //if (_cCode.Mc_anal11 == 3) { ((Excel.Range)worksheet.Cells[rwcount, "I"]).Value2 = row["rem_val_final"].ToString(); _amAmt += double.Parse(row["rem_val_final"].ToString()); ((Excel.Range)worksheet.Cells[rwcount, "E"]).Value2 = row["rem_pc"].ToString(); I += _amAmt; }
                        }
                        else
                        {
                            //   if (_cCode.Mc_anal11 == 1) { 
                            _amAmt += double.Parse(row["rem_val_final"].ToString()); G += _amAmt; G1 += _amAmt;
                            //   }

                            //if (_cCode.Mc_anal11 == 2) { _amAmt += double.Parse(row["rem_val_final"].ToString()); H += _amAmt; H1 += _amAmt; }

                            //if (_cCode.Mc_anal11 == 3) { _amAmt += double.Parse(row["rem_val_final"].ToString()); I += _amAmt; I1 += _amAmt; }

                        }

                    }
                    if (isSummary == 0)
                    {
                        ((Excel.Range)worksheet.Cells[rwcount, "H"]).Value2 = _amAmt;
                    }
                    //   J += _amAmt;

                    foreach (DataRow row in dtManagerCBonusReport.Rows)
                    {   // Setting cell values
                        if (isSummary == 0)
                        {
                            ((Excel.Range)worksheet.Cells[rwcount, "D"]).Value2 = row["mpc_desc"].ToString();
                            ((Excel.Range)worksheet.Cells[rwcount, "B"]).Value2 = row["esep_name_initials"].ToString();
                            ((Excel.Range)worksheet.Cells[rwcount, "A"]).Value2 = row["esep_epf"].ToString();
                            // if (_cCode.Mc_anal11 == 1) {
                            ((Excel.Range)worksheet.Cells[rwcount, "F"]).Value2 = row["rem_val_final"].ToString(); _ambAmt += double.Parse(row["rem_val_final"].ToString()); ((Excel.Range)worksheet.Cells[rwcount, "C"]).Value2 = row["rem_pc"].ToString(); K += _ambAmt;
                            //  }

                            //if (_cCode.Mc_anal11 == 2) { ((Excel.Range)worksheet.Cells[rwcount, "L"]).Value2 = row["rem_val_final"].ToString(); _ambAmt += double.Parse(row["rem_val_final"].ToString()); ((Excel.Range)worksheet.Cells[rwcount, "D"]).Value2 = row["rem_pc"].ToString(); L += _ambAmt; }

                            //if (_cCode.Mc_anal11 == 3) { ((Excel.Range)worksheet.Cells[rwcount, "M"]).Value2 = row["rem_val_final"].ToString(); _ambAmt += double.Parse(row["rem_val_final"].ToString()); ((Excel.Range)worksheet.Cells[rwcount, "E"]).Value2 = row["rem_pc"].ToString(); M += _ambAmt; }
                        }
                        else
                        {
                            //  if (_cCode.Mc_anal11 == 1) {
                            _ambAmt += double.Parse(row["rem_val_final"].ToString()); K += _ambAmt; K1 += _ambAmt;
                            //  }

                            //if (_cCode.Mc_anal11 == 2) { _ambAmt += double.Parse(row["rem_val_final"].ToString()); L += _ambAmt; L1 += _ambAmt; }

                            //if (_cCode.Mc_anal11 == 3) { _ambAmt += double.Parse(row["rem_val_final"].ToString()); M += _ambAmt; M1 += _ambAmt; }

                        }
                    }

                    if (isSummary == 0)
                    {
                        // ((Excel.Range)worksheet.Cells[rwcount, "N"]).Value2 = (_ambAmt).ToString("n");
                        // N += _ambAmt;
                        ((Excel.Range)worksheet.Cells[rwcount, "G"]).Value2 = string.Empty;
                        ((Excel.Range)worksheet.Cells[rwcount, "H"]).Value2 = (_ambAmt + _amAmt).ToString("n");
                        // P += _ambAmt + _amAmt;
                        ((Excel.Range)worksheet.Cells[rwcount, "I"]).Value2 = ((_ambAmt + _amAmt) * 20 / 100).ToString("n");
                        Q += ((Excel.Range)worksheet.Cells[rwcount, "I"]).Value2;
                        ((Excel.Range)worksheet.Cells[rwcount, "J"]).Value2 = ((_ambAmt + _amAmt) * 3 / 100).ToString("n");
                        R += ((Excel.Range)worksheet.Cells[rwcount, "J"]).Value2;
                        ((Excel.Range)worksheet.Cells[rwcount, "K"]).Value2 = string.Empty;

                        Excel.Range cellRange1 = (Excel.Range)worksheet.get_Range("E" + rwcount, "J" + rwcount);

                        cellRange1.NumberFormat = "0,0.00";

                    }
                    else
                    {
                        N += _ambAmt;
                        P += _ambAmt + _amAmt;
                        N1 += _ambAmt;
                        P1 += _ambAmt + _amAmt;
                    }
                    if (isSummary == 0)
                    {
                        if (_ambAmt + _amAmt > 0)
                        {
                            rwcount += 1;
                        }
                    }
                }


                //  }


                //  }



                if (isSummary == 0)
                {
                    _mgrCnttO = rwcount - 1;

                    ((Excel.Range)worksheet.Cells[rwcount, "A"]).Value2 = "Sub Total";
                    //   ((Excel.Range)worksheet.Cells[rwcount + 1, "B"]).Value2 = row1["esep_name_initials"].ToString();

                    ((Excel.Range)worksheet.Cells[rwcount, "E"]).Formula = "=sum(E" + _mgrCnt + ":E" + _mgrCnttO + ")";
                    ((Excel.Range)worksheet.Cells[rwcount, "F"]).Formula = "=sum(F" + _mgrCnt + ":F" + _mgrCnttO + ")";
                    ((Excel.Range)worksheet.Cells[rwcount, "G"]).Formula = "=sum(G" + _mgrCnt + ":G" + _mgrCnttO + ")";
                    ((Excel.Range)worksheet.Cells[rwcount, "H"]).Formula = "=sum(H" + _mgrCnt + ":H" + _mgrCnttO + ")";
                    ((Excel.Range)worksheet.Cells[rwcount, "I"]).Formula = "=sum(I" + _mgrCnt + ":I" + _mgrCnttO + ")";
                    ((Excel.Range)worksheet.Cells[rwcount, "J"]).Formula = "=sum(J" + _mgrCnt + ":J" + _mgrCnttO + ")";
                    //((Excel.Range)worksheet.Cells[rwcount, "M"]).Formula = "=sum(M" + _mgrCnt + ":M" + _mgrCnttO + ")";
                    //((Excel.Range)worksheet.Cells[rwcount, "N"]).Formula = "=sum(N" + _mgrCnt + ":N" + _mgrCnttO + ")";
                    //((Excel.Range)worksheet.Cells[rwcount, "P"]).Formula = "=sum(P" + _mgrCnt + ":P" + _mgrCnttO + ")";
                    //((Excel.Range)worksheet.Cells[rwcount, "Q"]).Formula = "=sum(Q" + _mgrCnt + ":Q" + _mgrCnttO + ")";
                    //((Excel.Range)worksheet.Cells[rwcount, "R"]).Formula = "=sum(R" + _mgrCnt + ":R" + _mgrCnttO + ")";

                    Excel.Range cellRange = (Excel.Range)worksheet.get_Range("A" + rwcount, "K" + rwcount);
                    cellRange.Font.Bold = true;
                    cellRange.RowHeight = 25;
                    cellRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                    cellRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    cellRange.Borders.Weight = 1d;

                    //  cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    //  cellRange.NumberFormat = "0,0.00";

                    _mgrCnt = rwcount + 1;
                    rwcount += 1;



                }
                else
                {
                    dtManagerProfit = bsObj.CHNLSVC.Sales.GetManagerDefProfit(BaseCls.GlbReportComp, row1["mepf"].ToString());
                    foreach (DataRow row11 in dtManagerProfit.Rows)
                    {
                        ((Excel.Range)worksheet.Cells[rwcount, "D"]).Value2 = row11["ESEP_DEF_PROFIT"].ToString();
                    }
                    ((Excel.Range)worksheet.Cells[2, "D"]).Value2 = "Showroom";
                    ((Excel.Range)worksheet.Cells[rwcount, "A"]).Value2 = row1["mepf"].ToString();
                    ((Excel.Range)worksheet.Cells[rwcount, "B"]).Value2 = row1["esep_name_initials"].ToString();
                    ((Excel.Range)worksheet.Cells[rwcount, "E"]).Value2 = G1.ToString("n");
                    //((Excel.Range)worksheet.Cells[rwcount  , "F"]).Value2 = H1.ToString("n");
                    //((Excel.Range)worksheet.Cells[rwcount  , "I"]).Value2 = I1.ToString("n");
                    //((Excel.Range)worksheet.Cells[rwcount  , "J"]).Value2 = (G1 + H1 + I1).ToString("n");
                    ((Excel.Range)worksheet.Cells[rwcount, "F"]).Value2 = K1.ToString("n");
                    //((Excel.Range)worksheet.Cells[rwcount  , "L"]).Value2 = L1.ToString("n");
                    //((Excel.Range)worksheet.Cells[rwcount  , "M"]).Value2 = M1.ToString("n");
                    //((Excel.Range)worksheet.Cells[rwcount  , "N"]).Value2 = (K1 + L1 + M1).ToString("n");
                    ((Excel.Range)worksheet.Cells[rwcount, "H"]).Value2 = (G1 + H1 + I1 + K1 + L1 + M1).ToString("n");
                    ((Excel.Range)worksheet.Cells[rwcount, "I"]).Value2 = ((G1 + H1 + I1 + K1 + L1 + M1) * 20 / 100).ToString("n");
                    ((Excel.Range)worksheet.Cells[rwcount, "J"]).Value2 = ((G1 + H1 + I1 + K1 + L1 + M1) * 3 / 100).ToString("n");
                    rwcount += 1;
                }
            }

            rwcount += 1;
            ((Excel.Range)worksheet.Cells[rwcount, "A"]).Value2 = "Grand Total";
            ((Excel.Range)worksheet.Cells[rwcount, "E"]).Value2 = G.ToString("n");
            //((Excel.Range)worksheet.Cells[rwcount  , "H"]).Value2 = H.ToString("n");
            //((Excel.Range)worksheet.Cells[rwcount  , "I"]).Value2 = I.ToString("n");
            //((Excel.Range)worksheet.Cells[rwcount  , "J"]).Value2 = (G + H + I).ToString("n");
            ((Excel.Range)worksheet.Cells[rwcount, "F"]).Value2 = K.ToString("n");
            //((Excel.Range)worksheet.Cells[rwcount  , "L"]).Value2 = L.ToString("n");
            //((Excel.Range)worksheet.Cells[rwcount  , "M"]).Value2 = M.ToString("n");
            //((Excel.Range)worksheet.Cells[rwcount  , "N"]).Value2 = (K + L + M).ToString("n");
            ((Excel.Range)worksheet.Cells[rwcount, "H"]).Value2 = (G + H + I + K + L + M).ToString("n");
            ((Excel.Range)worksheet.Cells[rwcount, "I"]).Value2 = ((G + H + I + K + L + M) * 20 / 100).ToString("n");
            ((Excel.Range)worksheet.Cells[rwcount, "J"]).Value2 = ((G + H + I + K + L + M) * 3 / 100).ToString("n");

            Excel.Range cellRange2 = (Excel.Range)worksheet.get_Range("A" + rwcount, "J" + rwcount);
            cellRange2.Font.Bold = true;
            cellRange2.RowHeight = 25.5;
            cellRange2.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
            cellRange2.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            cellRange2.Borders.Weight = 1d;

            //   cellRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            cellRange2.NumberFormat = "0,0.00";



            //   worksheet.Columns.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White); // Color Sheet5 to white, BusLoad
            //  worksheet.Columns.NumberFormat = "@";
            Excel.Range rng = (Excel.Range)worksheet.get_Range("A2", "K2");
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

            string workbookPath = _repPath + repType + BaseCls.GlbUserID + ".xls";
            Excel.Workbook excelWorkbook = excelApp1.Workbooks.Open(workbookPath,
                    0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);


        }

        public void BankReconciliationSummeryReport() // Added by Chathura on 20-oct-2017
        {
            DataTable param = new DataTable();
            DataRow dr;
            DataTable BNK_REC_SMRY = new DataTable();
            BNK_REC_SMRY.Clear();
            BNK_REC_SMRY = bsObj.CHNLSVC.Sales.Bank_Reconciliation_Summery_Rpt(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbReportDoc);
            DataTable MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            param.Clear();



            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("Asatadate", typeof(string));
            param.Columns.Add("AccNo", typeof(string));
            param.Columns.Add("user", typeof(string));
            dr = param.NewRow();


            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["Asatadate"] = BaseCls.GlbReportAsAtDate;
            dr["AccNo"] = BaseCls.GlbReportDocType;
            dr["user"] = BaseCls.GlbUserID;
            param.Rows.Add(dr);

            _BNK_RECON_SMRY.Database.Tables["BNK_REC_SMRY"].SetDataSource(BNK_REC_SMRY);
            _BNK_RECON_SMRY.Database.Tables["param"].SetDataSource(param);
            _BNK_RECON_SMRY.Database.Tables["mst_com"].SetDataSource(MST_COM);

        }

        public void RealizationStatusReport() // Added by Chathura on 20-oct-2017
        {
            DataTable param = new DataTable();
            DataRow dr;
            DataTable BNK_RLZ_ST = new DataTable();
            BNK_RLZ_ST.Clear();

            string repType = "SINGLE";
            if (BaseCls.GlbReportDoc == "ALL") repType = "ALL";

            BNK_RLZ_ST = bsObj.CHNLSVC.Sales.RealizationStatusReport(repType, (BaseCls.GlbReportDoc == null) ? "" : BaseCls.GlbReportDoc, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportProfit);
            DataTable MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            param.Clear();



            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("Asatadate", typeof(string));
            param.Columns.Add("AccNo", typeof(string));
            param.Columns.Add("user", typeof(string));
            dr = param.NewRow();


            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["Asatadate"] = BaseCls.GlbReportAsAtDate;
            dr["AccNo"] = BaseCls.GlbReportDocType;
            dr["user"] = BaseCls.GlbUserID;
            param.Rows.Add(dr);

            _BNK_RLZ_ST.Database.Tables["BNK_RLZ_STATUS"].SetDataSource(BNK_RLZ_ST);
            _BNK_RLZ_ST.Database.Tables["param"].SetDataSource(param);
            _BNK_RLZ_ST.Database.Tables["mst_com"].SetDataSource(MST_COM);

        }
        public void RemitanceControlReconReport() // Added by Chathura on 20-oct-2017
        {
            DataTable param = new DataTable();
            DataRow dr;
            DataTable REM_CON_RS = new DataTable();
            REM_CON_RS.Clear();

            REM_CON_RS = bsObj.CHNLSVC.Sales.RemitanceControlReconReport(BaseCls.GlbReportProfit, BaseCls.GlbReportComp, Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbReportIsAsAt, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID);
            DataTable MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            param.Clear();
            
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("Asatadate", typeof(string));
            param.Columns.Add("AccNo", typeof(string));
            param.Columns.Add("user", typeof(string));
            dr = param.NewRow();

            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["Asatadate"] = BaseCls.GlbReportAsAtDate;
            dr["AccNo"] = BaseCls.GlbReportDocType;
            dr["user"] = BaseCls.GlbUserID;
            param.Rows.Add(dr); 

            //_REM_CON.Database.Tables["REM_CON_RSET"].SetDataSource(REM_CON_RS);

            DataTable dtHD = new DataTable();
            dtHD = REM_CON_RS.Clone();
            var rowdtHD = REM_CON_RS.AsEnumerable().Where(r => r.Field<string>("SUB_TYPE") == "HD");
            if (rowdtHD.Any())
            {
                dtHD = rowdtHD.CopyToDataTable();
                _REM_CON.Database.Tables["REM_CON_RSET"].SetDataSource(dtHD);
            }

            DataTable dtSR = new DataTable();
            dtSR = REM_CON_RS.Clone();
            var rowdtSR = REM_CON_RS.AsEnumerable().Where(r => r.Field<string>("SUB_TYPE") == "SR");
            if (rowdtSR.Any()) 
            { 
                dtSR = rowdtSR.CopyToDataTable(); 
                _REM_CON.Database.Tables["REM_CON_RSET_RS"].SetDataSource(dtSR);
            }

            DataTable dtSS = new DataTable();
            dtSS = REM_CON_RS.Clone();
            var rowdtSS = REM_CON_RS.AsEnumerable().Where(r => r.Field<string>("SUB_TYPE") == "SS");
            if (rowdtSS.Any())
            {
                dtSS = rowdtSS.CopyToDataTable();
                _REM_CON.Database.Tables["REM_CON_RSET_SS"].SetDataSource(dtSS);
            }
            
            DataTable dtER = new DataTable();
            dtER = REM_CON_RS.Clone();
            var rowdtER = REM_CON_RS.AsEnumerable().Where(r => r.Field<string>("SUB_TYPE") == "ER");
            if (rowdtER.Any())
            {
                dtER = rowdtER.CopyToDataTable();
                _REM_CON.Database.Tables["REM_CON_RSET_ER"].SetDataSource(dtER);
            }
            
            DataTable dtRD = new DataTable();
            dtRD = REM_CON_RS.Clone();
            var rowdtRD = REM_CON_RS.AsEnumerable().Where(r => r.Field<string>("SUB_TYPE") == "RD");
            if (rowdtRD.Any())
            {
                dtRD = rowdtRD.CopyToDataTable();
                _REM_CON.Database.Tables["REM_CON_RSET_RD"].SetDataSource(dtRD);
            }

            DataTable dtZR = new DataTable();
            dtZR = REM_CON_RS.Clone();
            var rowdtZR = REM_CON_RS.AsEnumerable().Where(r => r.Field<string>("SUB_TYPE") == "ZR");
            if (rowdtZR.Any())
            {
                dtZR = rowdtZR.CopyToDataTable();
                _REM_CON.Database.Tables["REM_CON_RSET_ZR"].SetDataSource(dtZR);
            }
            
            _REM_CON.Database.Tables["param"].SetDataSource(param);
            _REM_CON.Database.Tables["mst_com"].SetDataSource(MST_COM);

        }

        public void RemitanceControlReconSummeryReport() // Added by Chathura on 30-oct-2017
        {
            DataTable param = new DataTable();
            DataRow dr;
            DataTable REM_CON_RS = new DataTable();
            REM_CON_RS.Clear();

            REM_CON_RS = bsObj.CHNLSVC.Sales.RemitanceControlReconReport(BaseCls.GlbReportProfit, BaseCls.GlbReportComp, Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbReportIsAsAt, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID);
            DataTable MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            param.Clear();

            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("Asatadate", typeof(string));
            param.Columns.Add("AccNo", typeof(string));
            param.Columns.Add("user", typeof(string));
            dr = param.NewRow();

            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["Asatadate"] = BaseCls.GlbReportAsAtDate;
            dr["AccNo"] = BaseCls.GlbReportDocType;
            dr["user"] = BaseCls.GlbUserID;
            param.Rows.Add(dr);

            //_REM_CON.Database.Tables["REM_CON_RSET"].SetDataSource(REM_CON_RS);

            DataTable dtHD = new DataTable();
            dtHD = REM_CON_RS.Clone();
            var rowdtHD = REM_CON_RS.AsEnumerable().Where(r => r.Field<string>("SUB_TYPE") == "HD");
            if (rowdtHD.Any())
            {
                dtHD = rowdtHD.CopyToDataTable();
                _REM_CON_SMRY.Database.Tables["REM_CON_RSET"].SetDataSource(dtHD);
            }

            //DataTable dtSR = new DataTable();
            //dtSR = REM_CON_RS.Clone();
            //var rowdtSR = REM_CON_RS.AsEnumerable().Where(r => r.Field<string>("SUB_TYPE") == "SR");
            //if (rowdtSR.Any())
            //{
            //    dtSR = rowdtSR.CopyToDataTable();
            //    _REM_CON_SMRY.Database.Tables["REM_CON_RSET"].SetDataSource(dtSR);
            //}


            //DataTable dtZR = new DataTable();
            //dtZR = REM_CON_RS.Clone();
            //var rowdtZR = REM_CON_RS.AsEnumerable().Where(r => r.Field<string>("SUB_TYPE") == "ZR");
            //if (rowdtZR.Any())
            //{
            //    dtZR = rowdtZR.CopyToDataTable();
            //    _REM_CON_SMRY.Database.Tables["REM_CON_RSET"].SetDataSource(dtZR);
            //}

            _REM_CON_SMRY.Database.Tables["param"].SetDataSource(param);
            _REM_CON_SMRY.Database.Tables["mst_com"].SetDataSource(MST_COM);

        }
        public void RankAccountTransfferingReport() // Added by tharanga 2018/08/18
        {
            DataTable param = new DataTable();
            DataRow dr;
            DataTable BNK_REC_SMRY = new DataTable();
            BNK_REC_SMRY.Clear();
            
            DataTable MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            param.Clear();

            DataTable tmp_user_pc = new DataTable();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable gnt_rem_sum_temp = new DataTable();
                    gnt_rem_sum_temp = bsObj.CHNLSVC.MsgPortal.RankAccountTransfferingReport(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, drow["tpl_pc"].ToString(), BaseCls.GlbUserComCode);
                        //(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportCompCode, BaseCls.GlbRecType, drow["tpl_pc"].ToString());
                    BNK_REC_SMRY.Merge(gnt_rem_sum_temp);
                }
            }

            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("Asatadate", typeof(string));
            param.Columns.Add("AccNo", typeof(string));
            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            dr = param.NewRow();


            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["Asatadate"] = BaseCls.GlbReportAsAtDate;
            dr["AccNo"] = BaseCls.GlbReportDocType;
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] =   BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
           
            param.Rows.Add(dr);

            _bankaccounttransfferingreport.Database.Tables["log_gnt_bnk_stmnt_det"].SetDataSource(BNK_REC_SMRY);
            _bankaccounttransfferingreport.Database.Tables["param"].SetDataSource(param);
            _bankaccounttransfferingreport.Database.Tables["mst_com"].SetDataSource(MST_COM);

        }

        public void CreditCardReconciliationReport()
        {
            //Wimal 01/Nov/2018
            DataTable param = new DataTable();
            DataRow dr;
            DataTable CC_REC = new DataTable();
            CC_REC.Clear();
            //BNK_REC = bsObj.CHNLSVC.Sales.Bank_Reconciliation_Rpt(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbReportDoc);
            CC_REC = bsObj.CHNLSVC.Sales.CC_Reconciliation_Rpt(BaseCls.GlbUserComCode, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDocType);
            DataTable MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            param.Clear();



            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("AccNo", typeof(string));
            param.Columns.Add("user", typeof(string));
            param.Columns.Add("ComDesc", typeof(string));            
            dr = param.NewRow();


            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "From " + BaseCls.GlbReportFromDate.ToString("dd/MMM/yyyy") + " To " + BaseCls.GlbReportToDate.ToString("dd/MMM/yyyy");
            dr["AccNo"] = BaseCls.GlbReportDocType;
            dr["user"] = BaseCls.GlbUserID;
            dr["ComDesc"] = "";
            param.Rows.Add(dr);

            //_BNK_RECON.Database.Tables["DT_BNK_REC"].SetDataSource(BNK_REC);
            _creditcard_recon.Database.Tables["param"].SetDataSource(param);
            _creditcard_recon.Database.Tables["mst_com"].SetDataSource(MST_COM);
            _creditcard_recon.Database.Tables["cc_recon_dtl"].SetDataSource(CC_REC);
            //_creditcard.Database.Tables["param"].SetDataSource(param);
            //_creditcard.Database.Tables["mst_com"].SetDataSource(MST_COM);

        }
    }
}
