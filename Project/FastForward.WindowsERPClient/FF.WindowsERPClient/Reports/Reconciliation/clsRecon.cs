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

namespace FF.WindowsERPClient.Reports.Reconciliation
{
    class clsRecon
    {

        Base bsObj;
        public FF.WindowsERPClient.Reports.Reconciliation.TransactionVariance _TransVar = new FF.WindowsERPClient.Reports.Reconciliation.TransactionVariance();
        public FF.WindowsERPClient.Reports.Reconciliation.Last_No_Seq_Rep _LastNoSeq = new FF.WindowsERPClient.Reports.Reconciliation.Last_No_Seq_Rep();
        public FF.WindowsERPClient.Reports.Reconciliation.Latest_Day_End_Log _DayEndLog = new FF.WindowsERPClient.Reports.Reconciliation.Latest_Day_End_Log();
        public FF.WindowsERPClient.Reports.Reconciliation.Scheme_Creation_Dtl_Report _SchemeDtl = new FF.WindowsERPClient.Reports.Reconciliation.Scheme_Creation_Dtl_Report();
        public FF.WindowsERPClient.Reports.Reconciliation.VehicleRegDefinition _vehiReg = new FF.WindowsERPClient.Reports.Reconciliation.VehicleRegDefinition();
        public FF.WindowsERPClient.Reports.Reconciliation.Request_Approval_Details _ReqAppDtl = new FF.WindowsERPClient.Reports.Reconciliation.Request_Approval_Details();
        public FF.WindowsERPClient.Reports.Reconciliation.discount_report _DiscDtl = new FF.WindowsERPClient.Reports.Reconciliation.discount_report();
        public FF.WindowsERPClient.Reports.Reconciliation.Rec_Desk_Sum_Report _RecDsk = new FF.WindowsERPClient.Reports.Reconciliation.Rec_Desk_Sum_Report();
        public FF.WindowsERPClient.Reports.Reconciliation.InsuDefRep _InsuDef = new FF.WindowsERPClient.Reports.Reconciliation.InsuDefRep();
        public FF.WindowsERPClient.Reports.Reconciliation.Item_Restr_Report _ItmRestr = new FF.WindowsERPClient.Reports.Reconciliation.Item_Restr_Report();
        public FF.WindowsERPClient.Reports.Reconciliation.Dep_Bank_Def_Report _DepBankDef = new FF.WindowsERPClient.Reports.Reconciliation.Dep_Bank_Def_Report();
        public FF.WindowsERPClient.Reports.Reconciliation.Vehicle_Reg_Form _VEhRegApp = new FF.WindowsERPClient.Reports.Reconciliation.Vehicle_Reg_Form();
        public FF.WindowsERPClient.Reports.Reconciliation.Merchant_Id_Def_Report _merchantDef = new FF.WindowsERPClient.Reports.Reconciliation.Merchant_Id_Def_Report();
        public FF.WindowsERPClient.Reports.Reconciliation.Reg_Unreg_Vehicle _VRegUnreg = new FF.WindowsERPClient.Reports.Reconciliation.Reg_Unreg_Vehicle();
        public FF.WindowsERPClient.Reports.Reconciliation.Unused_doc_details_Report _unuseddef = new FF.WindowsERPClient.Reports.Reconciliation.Unused_doc_details_Report();
        public FF.WindowsERPClient.Reports.Reconciliation.app_curr_status_report _appcurrstus = new FF.WindowsERPClient.Reports.Reconciliation.app_curr_status_report();
        public FF.WindowsERPClient.Reports.Reconciliation.app_curr_status_user _appcurrstususer = new FF.WindowsERPClient.Reports.Reconciliation.app_curr_status_user();
        public FF.WindowsERPClient.Reports.Reconciliation.ManualDocsRep _manDoc = new FF.WindowsERPClient.Reports.Reconciliation.ManualDocsRep();
        public FF.WindowsERPClient.Reports.Reconciliation.app_status_by_reason _appcurrstusbyreason = new FF.WindowsERPClient.Reports.Reconciliation.app_status_by_reason();
        //hasith 26/01/2015
        public FF.WindowsERPClient.Reports.Reconciliation.GVDetailsReport _gvDetails = new FF.WindowsERPClient.Reports.Reconciliation.GVDetailsReport();

        public FF.WindowsERPClient.Reports.Reconciliation.RegisProcess _regisProc = new FF.WindowsERPClient.Reports.Reconciliation.RegisProcess();
        public FF.WindowsERPClient.Reports.Reconciliation.SalesReconcilation _salesRecon = new FF.WindowsERPClient.Reports.Reconciliation.SalesReconcilation();
        DataTable tmp_user_pc = new DataTable();
        DataTable GLOB_DataTable = new DataTable();

        DataTable MST_ITM_BRAND = new DataTable();
        DataTable MST_COUNTRY = new DataTable();
        DataTable SAT_VEH_REG_TXN = new DataTable();
        DataTable MST_ITEM = new DataTable();
        string vWord;
        int startX = 0;
        int startY = 0;
        int maxLetters = 0;
        int noOfRows = 0;

        public clsRecon()
        {
            bsObj = new Base();

        }

        private string GetBookNo(string _pageno, string _type)
        {
            string _book = string.Empty;

            return _book;


        }

        public void SalesReconcilationReport()
        {
            DataTable param = new DataTable();
            DataRow dr;
            DataTable tmp_user_pc = new DataTable();
            DataTable _glob_table = new DataTable();

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable _tmpDT = new DataTable();

                    _tmpDT = bsObj.CHNLSVC.MsgPortal.GetSalesReconcilationDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date,
                        Convert.ToDateTime(BaseCls.GlbReportToDate).Date, drow["tpl_com"].ToString(),
                        drow["tpl_pc"].ToString()
                        );

                    _glob_table.Merge(_tmpDT);

                }
            }


            param.Clear();

            param.Columns.Add("PARA_USER", typeof(string));
            param.Columns.Add("PARA_PERIOD", typeof(string));
            param.Columns.Add("PARA_PCENTER", typeof(string));
            param.Columns.Add("PARA_HEADING", typeof(string));

            dr = param.NewRow();
            dr["PARA_USER"] = BaseCls.GlbUserID;
            dr["PARA_PERIOD"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["PARA_PCENTER"] = BaseCls.GlbReportProfit;
            dr["PARA_HEADING"] = BaseCls.GlbReportHeading;

            param.Rows.Add(dr);


            _salesRecon.Database.Tables["GLB_SALES_RECON"].SetDataSource(_glob_table);
            _salesRecon.Database.Tables["REP_PARA"].SetDataSource(param);


        }

        public void RegistrationProcessReport()
        {
            DataTable param = new DataTable();
            DataRow dr;
            DataTable tmp_user_pc = new DataTable();
            DataTable _glob_table = new DataTable();

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable _tmpDT = new DataTable();

                    _tmpDT = bsObj.CHNLSVC.Financial.RegistrationProcessReport(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportParaLine1, BaseCls.GlbReportDoc);

                    _glob_table.Merge(_tmpDT);

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
            dr["fromdate"] = BaseCls.GlbReportAsAtDate;
            dr["todate"] = BaseCls.GlbReportAsAtDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            param.Rows.Add(dr);


            _regisProc.Database.Tables["Reg_Process"].SetDataSource(_glob_table);
            _regisProc.Database.Tables["param"].SetDataSource(param);


        }

        public void ManualDocumentsReport()
        {// kapila
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
                    DataTable tmp_manualbooks = new DataTable();

                    tmp_manualbooks = bsObj.CHNLSVC.General.ManualDocumentsReport(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), Convert.ToInt32(BaseCls.GlbReportIsAsAt), Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());

                    UnusedReceiptReport.Merge(tmp_manualbooks);

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


            _manDoc.Database.Tables["UnusedReceiptReport"].SetDataSource(UnusedReceiptReport);
            _manDoc.Database.Tables["mst_com"].SetDataSource(mst_com);
            _manDoc.Database.Tables["param"].SetDataSource(param);
            _manDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);

        }

        public void ReqAppCurrentStatusUserWise()
        {// kapila
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.ReqAppCurrentStatusUserWise(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportDoc1, BaseCls.GlbReportStrStatus, BaseCls.GlbUserID, BaseCls.GlbReportUser);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("reqtp", typeof(string));
            param.Columns.Add("status", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["reqtp"] = BaseCls.GlbReportDoc1 == "" ? "ALL" : BaseCls.GlbReportDoc1;
            dr["status"] = BaseCls.GlbReportStrStatus == "" ? "ALL" : BaseCls.GlbReportStrStatus;
            param.Rows.Add(dr);

            _appcurrstususer.Database.Tables["APP_CURR_STATUS"].SetDataSource(GLOB_DataTable);
            _appcurrstususer.Database.Tables["param"].SetDataSource(param);
        }

        public void LastNoSeqReport()
        {// Sanjeewa 09-04-2013
            DataTable param = new DataTable();
            DataTable LASTNOSEQPAGE = new DataTable();
            DataTable DataTable_U = new DataTable();
            DataRow dr;

            if (BaseCls.GlbReportParaLine1 == 1)   //get processed data
                GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.GetLastNoSeqDetails_Pro(BaseCls.GlbReportProfit, BaseCls.GlbReportDocType);
            else
            {
                tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

                if (tmp_user_pc.Rows.Count > 0)
                {
                    foreach (DataRow drow in tmp_user_pc.Rows)
                    {
                        DataTable TMP_DataTable = new DataTable();
                        TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetLastNoSeqDetails(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportDocType, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
                        GLOB_DataTable.Merge(TMP_DataTable);
                    }
                }
            }

            DataTable_U = GLOB_DataTable.DefaultView.ToTable(true, "profitcenter");
            //DataTable LASTNOSEQ = bsObj.CHNLSVC.Sales.GetLastNoSeqDetails(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportDocType);

            //DataTable LASTNOSEQPAGE = bsObj.CHNLSVC.Inventory.GetLastNoSeqPageDetails(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date);

            if (DataTable_U.Rows.Count > 0)
            {
                if (BaseCls.GlbReportParaLine1 == 1)   //get processed data
                    LASTNOSEQPAGE = bsObj.CHNLSVC.MsgPortal.GetLastNoSeqPageDetails_Pro(BaseCls.GlbReportProfit, BaseCls.GlbReportDocType);
                else
                {
                    foreach (DataRow drow in DataTable_U.Rows)
                    {
                        DataTable TMP_DataTable = new DataTable();
                        TMP_DataTable = bsObj.CHNLSVC.Inventory.GetLastNoSeqPageDetails(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, drow["profitcenter"].ToString());
                        LASTNOSEQPAGE.Merge(TMP_DataTable);
                    }
                }
            }

            if (LASTNOSEQPAGE.Rows.Count <= 0)
            {
                if (BaseCls.GlbReportParaLine1 == 1)   //get processed data
                    LASTNOSEQPAGE = bsObj.CHNLSVC.MsgPortal.GetLastNoSeqPageDetails_Pro("", BaseCls.GlbReportDocType);
                else
                    LASTNOSEQPAGE = bsObj.CHNLSVC.Inventory.GetLastNoSeqPageDetails(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, "");
            }
            //var result = LASTNOSEQ_DOC.Where(t1=>!db.table2.Any(t2=>t2.DealReference==t1.DealReference))


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

            _LastNoSeq.Database.Tables["PROC_LASTNO_SEQ"].SetDataSource(GLOB_DataTable);
            _LastNoSeq.Database.Tables["LAST_NO_SEQ_PAGES"].SetDataSource(LASTNOSEQPAGE);
            _LastNoSeq.Database.Tables["param"].SetDataSource(param);
        }

        public void RecDeskSummReport()
        {// Sanjeewa 30-12-2013
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.RecDeskSummDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
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

            _RecDsk.Database.Tables["REC_DESK_SUM"].SetDataSource(GLOB_DataTable);
            _RecDsk.Database.Tables["param"].SetDataSource(param);
        }

        public void VehicleRegistrationSlip()
        {// Sanjeewa 29-09-2014           
            PrintDocument pdoc = null;
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            //pdoc.PrinterSettings.DefaultPageSettings.Landscape = true;

            Font font = new Font("Tahoma", 8);

            pd.Document = pdoc;


            DialogResult result = pd.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (result == DialogResult.OK)
                {
                    SAT_VEH_REG_TXN = bsObj.CHNLSVC.Sales.GetVehicalRegistrations(BaseCls.GlbReportDoc);

                    foreach (DataRow row in SAT_VEH_REG_TXN.Rows)
                    {
                        MST_ITEM = bsObj.CHNLSVC.Sales.GetItemCode(BaseCls.GlbUserComCode, row["srvt_itm_cd"].ToString());
                    }
                    MST_ITEM = MST_ITEM.DefaultView.ToTable(true);

                    if (SAT_VEH_REG_TXN.Rows.Count > 0)
                    {
                        foreach (DataRow drow in SAT_VEH_REG_TXN.Rows)
                        {
                            startX = 0;
                            startY = 0;
                            maxLetters = 0;
                            noOfRows = 0;

                            DialogResult dialogResult = new DialogResult();
                            dialogResult = MessageBox.Show("Insert the document to the printer & Press Ok.", "Vehicle Registration Slip Print", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                            if (dialogResult == DialogResult.OK)
                            {
                                BaseCls.GlbReportnoofDays = 1;
                                pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage_slip);
                                pdoc.Print();
                            }
                        }
                    }
                }
            }

        }

        public void pdoc_PrintPage_slip(object sender, PrintPageEventArgs e)
        {
            int Offset = 0;
            Graphics graphics = e.Graphics;
            Graphics graphics1 = e.Graphics;
            Graphics graphics2 = e.Graphics;
            Graphics graphics3 = e.Graphics;
            Graphics graphics4 = e.Graphics;
            Graphics graphics5 = e.Graphics;
            Graphics graphics6 = e.Graphics;

            //graphics.RotateTransform(90);

            if (SAT_VEH_REG_TXN.Rows.Count > 0)
            {
                foreach (DataRow drowword in SAT_VEH_REG_TXN.Rows)
                {
                    if (BaseCls.GlbReportnoofDays == 1)
                    {//Page 01                      
                        string _nowDT = DateTime.Now.Date.ToShortDateString();  // bsObj.CHNLSVC.Security.GetServerDateTime().Date;

                        graphics.TranslateTransform(770, 90);   //Date                                            
                        vWord = _nowDT;
                        graphics.RotateTransform(90);
                        PointF drawPoint = new PointF(0, 0);
                        graphics.DrawString(vWord, new Font("Tahoma", 10), new SolidBrush(Color.Black), drawPoint);

                        graphics1.TranslateTransform(140, 220); //Make
                        vWord = drowword["svrt_brd"].ToString();
                        graphics1.RotateTransform(0);
                        PointF drawPoint1 = new PointF(0, 0);
                        graphics1.DrawString(vWord, new Font("Tahoma", 10), new SolidBrush(Color.Black), drawPoint1);

                        graphics2.TranslateTransform(0, 33); //Model
                        vWord = drowword["svrt_model"].ToString();
                        graphics2.RotateTransform(0);
                        PointF drawPoint2 = new PointF(0, 0);
                        graphics2.DrawString(vWord, new Font("Tahoma", 10), new SolidBrush(Color.Black), drawPoint2);

                        graphics3.TranslateTransform(0, 33); //Chassis
                        vWord = drowword["svrt_chassis"].ToString();
                        graphics3.RotateTransform(0);
                        PointF drawPoint3 = new PointF(0, 0);
                        graphics3.DrawString(vWord, new Font("Tahoma", 10), new SolidBrush(Color.Black), drawPoint3);

                        graphics4.TranslateTransform(0, 33); //Engine
                        vWord = drowword["svrt_engine"].ToString();
                        graphics4.RotateTransform(0);
                        PointF drawPoint4 = new PointF(0, 0);
                        graphics4.DrawString(vWord, new Font("Tahoma", 10), new SolidBrush(Color.Black), drawPoint4);

                        graphics5.TranslateTransform(-120, 70); //Name
                        vWord = drowword["svrt_full_name"].ToString() + " " + drowword["svrt_last_name"].ToString();
                        graphics5.RotateTransform(0);
                        PointF drawPoint5 = new PointF(0, 0);
                        graphics5.DrawString(vWord, new Font("Tahoma", 10), new SolidBrush(Color.Black), drawPoint5);

                        graphics6.TranslateTransform(-20, 33); //Address
                        vWord = drowword["svrt_add01"].ToString() + " " + drowword["svrt_add02"].ToString();
                        graphics6.RotateTransform(0);
                        PointF drawPoint6 = new PointF(0, 0);
                        graphics6.DrawString(vWord, new Font("Tahoma", 10), new SolidBrush(Color.Black), drawPoint6);
                    }
                }
            }
        }

        public void VehRegAppReport()
        {// Sanjeewa 23-09-2014
            DataTable MST_ITM_BRAND = new DataTable();
            DataTable MST_COUNTRY = new DataTable();
            DataTable SAT_VEH_REG_TXN = bsObj.CHNLSVC.MsgPortal.GetVehicleRegistrationDetails(BaseCls.GlbReportDoc);

            if (SAT_VEH_REG_TXN.Rows.Count > 0)
            {
                foreach (DataRow drow in SAT_VEH_REG_TXN.Rows)
                {
                    MST_ITM_BRAND = bsObj.CHNLSVC.MsgPortal.GetBrandDetails(drow["svrt_brd"].ToString());
                    MST_COUNTRY = bsObj.CHNLSVC.MsgPortal.GetCountryDetails(drow["svrt_country"].ToString());
                }
            }

            _VEhRegApp.Database.Tables["SAT_VEH_REG_TXN"].SetDataSource(SAT_VEH_REG_TXN);
            _VEhRegApp.Database.Tables["MST_ITM_BRAND"].SetDataSource(MST_ITM_BRAND);
            _VEhRegApp.Database.Tables["MST_COUNTRY"].SetDataSource(MST_COUNTRY);
        }

        public void VehRegAppReport1()
        {// Sanjeewa 24-09-2014           
            PrintDocument pdoc = null;
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Tahoma", 8);

            pd.Document = pdoc;

            DialogResult result = pd.ShowDialog();
            if (result == DialogResult.OK)
            {
                SAT_VEH_REG_TXN = bsObj.CHNLSVC.MsgPortal.GetVehicleRegistrationDetails(BaseCls.GlbReportDoc);

                if (SAT_VEH_REG_TXN.Rows.Count > 0)
                {
                    foreach (DataRow drow in SAT_VEH_REG_TXN.Rows)
                    {
                        startX = 0;
                        startY = 0;
                        maxLetters = 0;
                        noOfRows = 0;

                        MST_ITM_BRAND = bsObj.CHNLSVC.MsgPortal.GetBrandDetails(drow["svrt_brd"].ToString());
                        MST_COUNTRY = bsObj.CHNLSVC.MsgPortal.GetCountryDetails(drow["svrt_country"].ToString());

                        DialogResult dialogResult = new DialogResult();
                        dialogResult = MessageBox.Show("Insert the first (1) page to the printer & Press Ok.", "Vehicle Registration Application Print", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                        if (dialogResult == DialogResult.OK)
                        {
                            BaseCls.GlbReportnoofDays = 1;
                            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
                            pdoc.Print();
                        }

                        dialogResult = MessageBox.Show("Insert the second (2) page to the printer & Press Ok.", "Vehicle Registration Application Print", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                        if (dialogResult == DialogResult.OK)
                        {
                            BaseCls.GlbReportnoofDays = 2;
                            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
                            pdoc.Print();
                        }

                        dialogResult = MessageBox.Show("Insert the third (3) page to the printer & Press Ok.", "Vehicle Registration Application Print", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                        if (dialogResult == DialogResult.OK)
                        {
                            BaseCls.GlbReportnoofDays = 3;
                            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
                            pdoc.Print();
                        }
                    }
                }

            }

        }

        public void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            int Offset = 31;
            int Offsetx = -5;
            Graphics graphics = e.Graphics;

            if (SAT_VEH_REG_TXN.Rows.Count > 0)
            {
                foreach (DataRow drowword in SAT_VEH_REG_TXN.Rows)
                {
                    if (BaseCls.GlbReportnoofDays == 1)
                    {//Page 01
                        noOfRows = 1; maxLetters = 30; startX = 23 + Offsetx; startY = 502 + Offset; //01
                        vWord = drowword["svrt_last_name"].ToString();
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 4; startX = 650 + Offsetx; startY = 505 + Offset; //03
                        vWord = drowword["svrt_initial"].ToString();
                        setupPrint(graphics);

                        noOfRows = 3; maxLetters = 30; startX = 23 + Offsetx; startY = 562 + Offset; //02
                        vWord = drowword["svrt_full_name"].ToString();
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 1; startX = 650 + Offsetx;  //04
                        if (drowword["svrt_cust_title"].ToString() == "Mr.") { startY = 567 + Offset; vWord = "X"; }
                        else
                        {
                            if (drowword["svrt_cust_title"].ToString() == "Mrs.") { startY = 697 + Offset; vWord = "X"; }
                            else
                            {
                                if (drowword["svrt_cust_title"].ToString() == "Miss.") { startY = 647 + Offset; vWord = "X"; }
                                else { startY = 675 + Offset; vWord = "X"; setupPrint(graphics); startY = 720 + Offset; startX = 660 + Offsetx; maxLetters = 4; vWord = drowword["svrt_cust_title"].ToString().ToUpper(); }
                            }
                        }
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 14; startX = 23 + Offsetx; startY = 720 + Offset; //05A
                        vWord = drowword["svrt_id_ref"].ToString();
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 1; startX = 358 + Offsetx;  //5B
                        if (drowword["svrt_id_tp"].ToString() == "NIC") { startY = 720 + Offset; vWord = "X"; }
                        else
                        {
                            if (drowword["svrt_id_tp"].ToString() == "PP") { startY = 752 + Offset; vWord = "X"; }
                            else
                            {
                                if (drowword["svrt_id_tp"].ToString() == "DL") { startY = 827 + Offset; vWord = "X"; }
                                else { if (drowword["svrt_id_tp"].ToString() == "BR") { startY = 857 + Offset; vWord = "X"; } }
                            }
                        }
                        setupPrint(graphics);

                        noOfRows = 2; maxLetters = 30; startX = 23 + Offsetx; startY = 930 + Offset; //06A
                        vWord = drowword["svrt_add01"].ToString() + " " + drowword["svrt_add02"].ToString();
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 14; startX = 23 + Offsetx; startY = 1028 + Offset; //06B
                        vWord = drowword["svrt_city"].ToString();
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 10; startX = 436 + Offsetx; startY = 1028 + Offset; //06B
                        vWord = drowword["svrt_contact"].ToString();
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 14; startX = 73 + Offsetx; startY = 1073 + Offset; //07
                        vWord = drowword["svrt_district"].ToString();
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 14; startX = 498 + Offsetx; startY = 1073 + Offset; //07B
                        vWord = drowword["svrt_province"].ToString();
                        setupPrint(graphics);
                    }

                    if (BaseCls.GlbReportnoofDays == 2)
                    {//Page 02
                        noOfRows = 1; maxLetters = 1; startX = 21 + Offsetx; startY = 48 + Offset; //08
                        vWord = "X";
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 1; startX = 21 + Offsetx; startY = 145 + Offset; //09
                        vWord = "X";
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 1; startX = 223 + Offsetx; startY = 13 + Offset; //10
                        vWord = "X";
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 1; startX = 463 + Offsetx; startY = 75 + Offset; //11
                        vWord = "X";
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 1; startX = 463 + Offsetx; startY = 177 + Offset; //12
                        vWord = "X";
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 1; startX = 173 + Offsetx; startY = 360 + Offset; //13
                        vWord = "X";
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 16; startX = 21 + Offsetx; startY = 432 + Offset; //14
                        vWord = drowword["svrt_brd"].ToString();
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 16; startX = 353 + Offsetx; startY = 432 + Offset; //15
                        vWord = drowword["svrt_model"].ToString();
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 4; startX = 680 + Offsetx; startY = 432 + Offset; //16
                        vWord = drowword["svrt_man_year"].ToString();
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 21; startX = 21 + Offsetx; startY = 482 + Offset; //17
                        vWord = drowword["svrt_chassis"].ToString();
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 21; startX = 21 + Offsetx; startY = 532 + Offset; //18
                        vWord = drowword["svrt_engine"].ToString();
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 21; startX = 21 + Offsetx; startY = 586 + Offset; //19
                        vWord = drowword["svrt_color"].ToString();
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 21; startX = 21 + Offsetx; startY = 639 + Offset; //20
                        vWord = drowword["svrt_fuel"].ToString();
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 6; startX = 21 + Offsetx; startY = 689 + Offset; //21A
                        vWord = drowword["svrt_capacity"].ToString();
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 1; startX = 237 + Offsetx; startY = 689 + Offset; //21B
                        vWord = "X";
                        setupPrint(graphics);

                    }

                    if (BaseCls.GlbReportnoofDays == 3)
                    {//Page 03
                        //if (MST_COUNTRY.Rows.Count > 0)
                        //{
                        //    foreach (DataRow drowcountry in MST_COUNTRY.Rows)
                        //    {
                        //        noOfRows = 1; maxLetters = 21; startX = 21; startY = 400 + Offset; //33
                        //        vWord = drowcountry["mcu_desc"].ToString();
                        //        setupPrint(graphics);
                        //    }
                        //}

                        //noOfRows = 1; maxLetters = 21; startX = 21 + Offsetx; startY = 400 + Offset; //33
                        //vWord = drowword["svrt_country"].ToString();
                        //setupPrint(graphics);

                        DateTime _nowDT = bsObj.CHNLSVC.Security.GetServerDateTime().Date;
                        noOfRows = 1; maxLetters = 2; startX = 265 + Offsetx; startY = 573 + Offset; //38 yy                        
                        vWord = _nowDT.Year.ToString().Substring(_nowDT.Year.ToString().Length - 2).ToString();
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 2; startX = 335 + Offsetx; startY = 573 + Offset; //38 mm
                        vWord = _nowDT.Month.ToString("0#");
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 2; startX = 412 + Offsetx; startY = 573 + Offset; //38 dd
                        vWord = _nowDT.Date.Day.ToString("0#");
                        setupPrint(graphics);

                        //noOfRows = 1; maxLetters = 26; startX = 21 + Offsetx; startY = 644 + Offset; //39
                        //vWord = "ABANS AUTO (PVT) LTD";
                        //setupPrint(graphics);

                        //noOfRows = 1; maxLetters = 26; startX = 21 + Offsetx; startY = 706 + Offset; //39B
                        //vWord = "NO 498 GALLE ROAD,";
                        //setupPrint(graphics);

                        //noOfRows = 1; maxLetters = 26; startX = 21 + Offsetx; startY = 751 + Offset; //39B
                        //vWord = "COLOMBO 03";
                        //setupPrint(graphics);

                        noOfRows = 1; maxLetters = 1; startX = 463 + Offsetx; startY = 1021 + Offset; //40A
                        vWord = "X";
                        setupPrint(graphics);

                        noOfRows = 1; maxLetters = 6; startX = 21 + Offsetx; startY = 1050 + Offset; //Create by
                        vWord = drowword["svrt_cre_bt"].ToString();
                        setupPrint(graphics);
                    }

                }
            }


        }

        public void setupPrint(Graphics graphics)
        {
            Font font = new Font("Tahoma", 10);
            float fontHeight = font.GetHeight();
            int OffsetX = 18;
            int OffsetY = 2;
            string vLetter = "";
            int currRow = 1;
            for (int i = 0; i < vWord.Length; i++)
            {
                vLetter = vWord.Substring(i, 1);

                if (i > 1) { if (i % maxLetters == 0) { if (noOfRows - currRow > 0) { startY += 45; currRow += 1; OffsetX = 18; } else { break; } } }
                graphics.DrawString(vLetter, new Font("Tahoma", 10),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                OffsetX += 20;
            }
        }

        //public void GiftVoucherPrintReport()
        //{// Sanjeewa 21-11-2013
        //    PrintDocument pdoc = null;
        //    PrintDialog pd = new PrintDialog();
        //    pdoc = new PrintDocument();
        //    PrinterSettings ps = new PrinterSettings();
        //    Font font = new Font("Tahoma", 8);

        //    pd.Document = pdoc;

        //    DialogResult result = pd.ShowDialog();
        //    if (result == DialogResult.OK)
        //    {

        //        DataTable GV_PRINT1 = CHNLSVC.Inventory.GetGVPrintDetails(BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, 0, 0, 0);
        //        DataTable GV_PRINTPage = GV_PRINT1.DefaultView.ToTable(true, "gvi_book", "gvi_page", "gvp_cus_name", "gvp_from");

        //        if (GV_PRINTPage.Rows.Count > 0)
        //        {
        //            foreach (DataRow drow in GV_PRINTPage.Rows)
        //            {
        //                GV_PRINTDoc = new DataTable();
        //                BaseCls.GlbReportDoc1 = "";
        //                BaseCls.GlbReportDoc2 = "";
        //                BaseCls.GlbReportCustomerCode = "";
        //                BaseCls.GlbReportExecCode = "";

        //                GV_PRINTDoc = GV_PRINT1.Select("gvi_book = '" + drow["gvi_book"].ToString() + "' AND gvi_page = '" + drow["gvi_page"].ToString() + "'").CopyToDataTable();

        //                BaseCls.GlbReportDoc1 = drow["gvi_book"].ToString();
        //                BaseCls.GlbReportDoc2 = drow["gvi_page"].ToString();
        //                BaseCls.GlbReportCustomerCode = drow["gvp_cus_name"].ToString();
        //                BaseCls.GlbReportExecCode = drow["gvp_from"].ToString();

        //                DialogResult dialogResult = MessageBox.Show("Insert the Gift Voucher - Book : " + BaseCls.GlbReportDoc1 + " , Page : " + BaseCls.GlbReportDoc2 + " - 1 st side to the printer & Press Ok.", "Gift Voucher Print", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

        //                if (dialogResult == DialogResult.OK)
        //                {
        //                    BaseCls.GlbReportnoofDays = 1;
        //                    pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
        //                    pdoc.Print();
        //                }

        //                DialogResult dialogResult1 = MessageBox.Show("Insert the Gift Voucher - Book : " + BaseCls.GlbReportDoc1 + " , Page : " + BaseCls.GlbReportDoc2 + " - back side to the printer & Press Ok.", "Gift Voucher Print", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

        //                if (dialogResult1 == DialogResult.OK)
        //                {
        //                    BaseCls.GlbReportnoofDays = 2;
        //                    pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
        //                    pdoc.Print();
        //                }
        //            }
        //        }

        //    }

        //}



        //public void pdoc_PrintPage1( object sender, PrintPageEventArgs e)
        //{
        //    Graphics graphics = e.Graphics;
        //    Font font = new Font("Tahoma", 10);
        //    float fontHeight = font.GetHeight();
        //    int startX = 110;
        //    int startY = 120;
        //    int Offset = 25;
        //    int OffsetX = 110;
        //    string vLetter = "";


        //    if (SAT_VEH_REG_TXN.Rows.Count > 0)
        //    {
        //        foreach (DataRow drow in SAT_VEH_REG_TXN.Rows)
        //        {

        //            if (BaseCls.GlbReportnoofDays == 1)
        //            {
        //                drowcat["gvi_val"].ToString()

        //                startX = 110 + OffsetX;
        //                graphics.DrawString(vLetter, new Font("Tahoma", 10),
        //                                new SolidBrush(Color.Black), startX, startY + Offset);
        //                Offset = Offset + 30;

        //            }

        //        }
        //    }


        //    if (BaseCls.GlbReportnoofDays == 1)
        //    {
        //        startX = 110 + OffsetX;
        //        graphics.DrawString(BaseCls.GlbReportCustomerCode, new Font("Tahoma", 10),
        //                        new SolidBrush(Color.Black), startX, startY + Offset);
        //        Offset = Offset + 30;
        //        graphics.DrawString(BaseCls.GlbReportExecCode, new Font("Tahoma", 10),
        //                        new SolidBrush(Color.Black), startX, startY + Offset);
        //        Offset = Offset + 20;
        //        graphics.DrawString("“Please refer overleaf for Details of Gift”", new Font("Tahoma", 10),
        //                        new SolidBrush(Color.Black), startX, startY + Offset);
        //        Offset = Offset + 20;
        //    }
        //    else
        //    {
        //        DataTable GV_PrintCat = GV_PRINTDoc.DefaultView.ToTable(true, "gvi_val");
        //        DataTable GV_PrintCatDtl = new DataTable();

        //        int j = 0;
        //        startY = 10;

        //        if (GV_PrintCat.Rows.Count > 4)
        //        {
        //            MessageBox.Show("Only Four (4) Categories are allowed to be printed on one Gift Voucher.", "Gift Voucher Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }

        //        if (GV_PrintCat.Rows.Count > 0)
        //        {
        //            foreach (DataRow drowcat in GV_PrintCat.Rows)
        //            {
        //                j = j + 1;
        //                if (j > 1)
        //                {
        //                    Offset = Offset + 30;
        //                }
        //                if (j == 3)
        //                {
        //                    Offset = 20;
        //                }

        //                if (j > 2)
        //                {
        //                    startX = 310 + OffsetX;
        //                }
        //                else
        //                {
        //                    startX = 10 + OffsetX;
        //                }
        //                graphics.DrawString("Option " + j + " :", new Font("Tahoma", 8, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
        //                Offset = Offset + 20;

        //                GV_PrintCatDtl = GV_PRINTDoc.Select("gvi_val = '" + drowcat["gvi_val"].ToString() + "'").CopyToDataTable();
        //                int i = 0;
        //                if (GV_PrintCatDtl.Rows.Count > 0)
        //                {
        //                    DataTable _mytbl = GV_PrintCatDtl.AsEnumerable().OrderByDescending(x => x.Field<Int16>("gvi_verb")).OrderBy(x => x.Field<string>("gvi_val")).ToList().CopyToDataTable();
        //                    foreach (DataRow drow in _mytbl.Rows)
        //                    {
        //                        i = i + 1;
        //                        string itemdesc = CHNLSVC.Inventory.getItemDescription(drow["gvi_itm"].ToString());

        //                        if (j > 2)
        //                        {
        //                            startX = 310 + OffsetX;
        //                        }
        //                        else
        //                        {
        //                            startX = 10 + OffsetX;
        //                        }

        //                        graphics.DrawString(itemdesc + " - " + drow["gvi_itm"].ToString(), new Font("Tahoma", 8),
        //                                        new SolidBrush(Color.Black), startX, startY + Offset);
        //                        Offset = Offset + 15;

        //                        if (i < GV_PrintCatDtl.Rows.Count)
        //                        {
        //                            if (j > 2)
        //                            {
        //                                startX = 450 + OffsetX;
        //                            }
        //                            else
        //                            {
        //                                startX = 150 + OffsetX;
        //                            }
        //                            graphics.DrawString(drow["gvi_verb"].ToString() == "1" ? " & " : " OR ", new Font("Tahoma", 8), new SolidBrush(Color.Black), startX, startY + Offset);
        //                            Offset = Offset + 15;
        //                        }

        //                    }
        //                }
        //            }
        //        }

        //    }

        //}

        public void ItemRestrictionReport()
        {// Sanjeewa 24-06-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetItemRestrictionDetails(BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;

            param.Rows.Add(dr);

            _ItmRestr.Database.Tables["ITM_RESTR"].SetDataSource(GLOB_DataTable);
            _ItmRestr.Database.Tables["param"].SetDataSource(param);
        }

        public void DepositBankDefReport()
        {// Sanjeewa 29-08-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetDepositBankDefDetails(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;

            param.Rows.Add(dr);

            _DepBankDef.Database.Tables["DEP_BANK_DEF"].SetDataSource(GLOB_DataTable);
            _DepBankDef.Database.Tables["param"].SetDataSource(param);
        }

        public void RegUnregVehicleDEtailsReport()
        {// Sanjeewa 27-10-2014
            DataTable param = new DataTable();
            DataRow dr;

            DataTable REG_UNREG = bsObj.CHNLSVC.MsgPortal.GetVehicleRegUnreg_Report(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDoc2, BaseCls.GlbUserID, BaseCls.GlbReportDocType);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("period", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");

            param.Rows.Add(dr);

            _VRegUnreg.Database.Tables["REG_UNREG"].SetDataSource(REG_UNREG);
            _VRegUnreg.Database.Tables["param"].SetDataSource(param);
        }

        public void MerchantIdDefReport()
        {// shanuka 12-09-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Sales.getAllMid_Details(drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;

            param.Rows.Add(dr);

            _merchantDef.Database.Tables["MID"].SetDataSource(GLOB_DataTable);
            _merchantDef.Database.Tables["param"].SetDataSource(param);
        }



        public void UnusedDefReport()
        {// shanuka 25-09-2014
            DataTable param = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataTable Unused_doc_dt = new DataTable();

            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_Unused_doc_report = new DataTable();
                    DataRow[] foundRows;
                    DataTable hdt1 = null;
                    string type = BaseCls.GlbReportType;

                    tmp_Unused_doc_report = bsObj.CHNLSVC.Sales.get_unused_doc_report(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), drow["tpl_pc"].ToString());
                    string expression = "T_MDD_ITM_CD like '%" + type + "%'";
                    foundRows = tmp_Unused_doc_report.Select(expression);
                    if (foundRows.Count() > 0)
                    {
                        hdt1 = foundRows.CopyToDataTable<DataRow>();
                        Unused_doc_dt.Merge(hdt1);
                    }
                    else
                    {
                        MessageBox.Show("No record found..", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    //Unused_doc_dt.Merge(hdt1);

                }
            }
            DataTable mst_com = default(DataTable);


            mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbUserComCode, string.Empty);


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
            dr["comp"] = BaseCls.GlbUserComCode;
            param.Rows.Add(dr);


            _unuseddef.Database.Tables["UnusedDoc"].SetDataSource(Unused_doc_dt);
            _unuseddef.Database.Tables["mst_com"].SetDataSource(mst_com);
            _unuseddef.Database.Tables["param"].SetDataSource(param);
            _unuseddef.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
        }






        public void InsuranceDeinition()
        {// Prabhath 30-12-2013
            DataTable param = new DataTable();
            DataRow dr;
            string _error = string.Empty;

            DataTable _c = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);

            if (BaseCls.GlbReportDocType == "CIRC")
            {
                GLOB_DataTable = bsObj.CHNLSVC.Sales.GetInsuReportDetCirc(BaseCls.GlbUserComCode, BaseCls.GlbReportItemCode, 0, BaseCls.GlbReportAsAtDate, BaseCls.GlbReportInsComp, BaseCls.GlbReportPolCode, BaseCls.GlbReportPriceBook, BaseCls.GlbReportPBLevel, BaseCls.GlbReportParty, BaseCls.GlbReportPartyCode, BaseCls.GlbUserID, BaseCls.GlbReportDoc, "PC", out _error);
            }
            else
            {
                GLOB_DataTable = bsObj.CHNLSVC.Sales.GetInsuReportDet(BaseCls.GlbUserComCode, BaseCls.GlbReportItemCode, 0, BaseCls.GlbReportAsAtDate, BaseCls.GlbReportInsComp, BaseCls.GlbReportPolCode, BaseCls.GlbReportPriceBook, BaseCls.GlbReportPBLevel, BaseCls.GlbReportParty, BaseCls.GlbReportPartyCode, BaseCls.GlbUserID, "PC", out _error);
            }

            foreach (DataRow r in GLOB_DataTable.Rows)
            {
                decimal _price = -1;
                List<PriceDetailRef> _det = bsObj.CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, r.Field<string>("ins_loc"), string.Empty, _c.Rows[0].Field<string>("Mc_anal7"), _c.Rows[0].Field<string>("Mc_anal8"), string.Empty, r.Field<string>("ins_itm"), 1, BaseCls.GlbReportAsAtDate);
                if (_det != null && _det.Count > 0)
                { var _normal = _det.Where(x => x.Sapd_price_type == 0).ToList(); if (_normal != null && _normal.Count > 0) _price = _normal[0].Sapd_itm_price; }
                else _error = "There is no price defain for the item";
                List<MasterItemTax> _tx = bsObj.CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, r.Field<string>("ins_itm"), "GOD", "VAT", string.Empty);
                if (_tx != null && _tx.Count > 0)
                    _price = _price * ((100 + _tx[0].Mict_tax_rate) / 100);
                r.SetField("ins_price", _price);
            }


            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            param.Rows.Add(dr);

            _InsuDef.Database.Tables["glb_insu_def"].SetDataSource(GLOB_DataTable);
            _InsuDef.Database.Tables["param"].SetDataSource(param);
        }


        public void ReqAppDetailsReport()
        {// Sanjeewa 16-09-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable REQAPP = bsObj.CHNLSVC.MsgPortal.GetReq_App_Details(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportType, BaseCls.GlbStatus, BaseCls.GlbUserID);
            DataTable REQAPPHDR = bsObj.CHNLSVC.Sales.GetReq_App_Headings();
            DataTable REQAPPHDRVIEW = bsObj.CHNLSVC.Sales.GetReq_App_HeadingsView();

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("reqtp", typeof(string));
            param.Columns.Add("appstatus", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["reqtp"] = BaseCls.GlbReportType == "" ? "ALL" : BaseCls.GlbReportType;
            dr["appstatus"] = BaseCls.GlbStatus == "" ? "ALL" : BaseCls.GlbStatus;
            param.Rows.Add(dr);

            _ReqAppDtl.Database.Tables["REQ_APP"].SetDataSource(REQAPP);
            _ReqAppDtl.Database.Tables["REQ_APP_HEAD"].SetDataSource(REQAPPHDR);
            _ReqAppDtl.Database.Tables["REQ_APP_HEAD_VIEW"].SetDataSource(REQAPPHDRVIEW);
            _ReqAppDtl.Database.Tables["param"].SetDataSource(param);
        }

        public void ReqAppDetByReasonReport()
        {// kapila
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.ReqAppDetByReasonReport(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportDoc, BaseCls.GlbReportStrStatus, BaseCls.GlbUserID);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("reqtp", typeof(string));
            param.Columns.Add("status", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["reqtp"] = BaseCls.GlbReportDoc1 == "" ? "ALL" : BaseCls.GlbReportDoc1;
            dr["status"] = BaseCls.GlbReportStrStatus == "" ? "ALL" : BaseCls.GlbReportStrStatus;
            param.Rows.Add(dr);

            _appcurrstusbyreason.Database.Tables["APP_REASON"].SetDataSource(GLOB_DataTable);
            _appcurrstusbyreason.Database.Tables["param"].SetDataSource(param);
        }

        public void ReqAppCurrentStatusReport()
        {// Sanjeewa 24-11-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.ReqAppCurrentStatusDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportDoc1, BaseCls.GlbReportStrStatus, BaseCls.GlbUserID);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("reqtp", typeof(string));
            param.Columns.Add("status", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["reqtp"] = BaseCls.GlbReportDoc1 == "" ? "ALL" : BaseCls.GlbReportDoc1;
            dr["status"] = BaseCls.GlbReportStrStatus == "" ? "ALL" : BaseCls.GlbReportStrStatus;
            param.Rows.Add(dr);

            _appcurrstus.Database.Tables["APP_CURR_STATUS"].SetDataSource(GLOB_DataTable);
            _appcurrstus.Database.Tables["param"].SetDataSource(param);
        }

        public void LatestDayEndLogReport()
        {// Sanjeewa 09-04-2013\\
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetLastDayEndLogDetails(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }
            //DataTable DAYENDLOG = bsObj.CHNLSVC.Sales.GetLastDayEndLogDetails(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbUserID);

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

            _DayEndLog.Database.Tables["LATEST_DAY_END_LOG"].SetDataSource(GLOB_DataTable);
            _DayEndLog.Database.Tables["param"].SetDataSource(param);
        }

        public void TrPayTypeDefinitionReport()
        {// Sanjeewa 13-10-2014
            DataTable param = new DataTable();
            //DataRow dr;
            string _err;
            string _filePath;

            Boolean TRPAYTP = bsObj.CHNLSVC.MsgPortal.TrPayTpDefDetails(BaseCls.GlbUserComCode, BaseCls.GlbReportDoc, "", BaseCls.GlbReportDoc1, BaseCls.GlbReportDoc2, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbUserID, out _err, out _filePath);

            if (TRPAYTP == false)
            {
                MessageBox.Show(_err.ToString(), "No data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
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
        }

        public void DiscountDetailReport()
        {// Sanjeewa 08-12-2013\\
            DataTable param = new DataTable();
            //DataRow dr;
            string _err;
            string _filePath;

            Boolean DISC_DTL = bsObj.CHNLSVC.MsgPortal.GetDiscountDetails1(BaseCls.GlbReportDoc, BaseCls.GlbUserID, BaseCls.GlbReportIsExport, BaseCls.GlbUserComCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, out _err, out _filePath);

            if (DISC_DTL == false)
            {
                MessageBox.Show(_err.ToString(), "No data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
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

            //param.Clear();

            //param.Columns.Add("user", typeof(string));
            //param.Columns.Add("heading_1", typeof(string));
            //param.Columns.Add("circularno", typeof(string));
            //param.Columns.Add("profitcenter", typeof(string));

            //dr = param.NewRow();
            //dr["user"] = BaseCls.GlbUserID;
            //dr["heading_1"] = BaseCls.GlbReportHeading;
            //dr["circularno"] = BaseCls.GlbReportDoc;
            //dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            //param.Rows.Add(dr);

            //_DiscDtl.Database.Tables["DISC_DTL"].SetDataSource(DISC_DTL);
            //_DiscDtl.Database.Tables["param"].SetDataSource(param);
        }


        public void ProfitcenterMasterReport()
        {// Sanjeewa 27-12-2013\\
            //DataTable param = new DataTable();
            //DataRow dr;

            DataTable PC_MASTER = bsObj.CHNLSVC.MsgPortal.ProfitCenterDetails(BaseCls.GlbUserID, BaseCls.GlbReportIsExport);

            //param.Clear();

            //param.Columns.Add("user", typeof(string));
            //param.Columns.Add("heading_1", typeof(string));
            //param.Columns.Add("circularno", typeof(string));
            //param.Columns.Add("profitcenter", typeof(string));

            //dr = param.NewRow();
            //dr["user"] = BaseCls.GlbUserID;
            //dr["heading_1"] = BaseCls.GlbReportHeading;
            //dr["circularno"] = BaseCls.GlbReportDoc;
            //dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            //param.Rows.Add(dr);

            //_DiscDtl.Database.Tables["DISC_DTL"].SetDataSource(DISC_DTL);
            //_DiscDtl.Database.Tables["param"].SetDataSource(param);
        }

        public void SchemeCreationDetailsReport()
        {// Sanjeewa 13-05-2013\\
            DataTable param = new DataTable();
            DataRow dr;

            DataTable HPR_SCH_CAT = bsObj.CHNLSVC.MsgPortal.GetSchemeCategory(BaseCls.GlbReportDoc);
            DataTable HPR_SCH_TP = bsObj.CHNLSVC.MsgPortal.GetSchemeType(BaseCls.GlbReportDoc);
            DataTable HPR_SCH_COMM = bsObj.CHNLSVC.MsgPortal.GetSchemeCommission(BaseCls.GlbReportDoc);
            DataTable HPR_SCH_DET = bsObj.CHNLSVC.MsgPortal.GetSchemeDetail(BaseCls.GlbReportDoc);
            DataTable HPR_SCH_SHED = bsObj.CHNLSVC.MsgPortal.GetSchemeSchedule(BaseCls.GlbReportDoc);
            DataTable HPR_SCH_RSCH = bsObj.CHNLSVC.MsgPortal.GetSchemeReSchedule(BaseCls.GlbReportDoc);
            DataTable HPR_SCH_PRD = bsObj.CHNLSVC.MsgPortal.GetSchemeProofDoc(BaseCls.GlbReportDoc);
            DataTable HPR_SER_CHG = bsObj.CHNLSVC.MsgPortal.GetSchemeSerCharge(BaseCls.GlbReportDoc);
            DataTable HPR_OTH_CHG = bsObj.CHNLSVC.MsgPortal.GetSchemeOtherCharge(BaseCls.GlbReportDoc);
            DataTable HPR_GUA_PARM = bsObj.CHNLSVC.MsgPortal.GetSchemeGuaranter(BaseCls.GlbReportDoc);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("circularno", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["company"] = BaseCls.GlbReportComp;
            dr["circularno"] = BaseCls.GlbReportDoc;

            param.Rows.Add(dr);

            _SchemeDtl.Database.Tables["HPR_SCH_COMM"].SetDataSource(HPR_SCH_COMM);

            foreach (object repOp in _SchemeDtl.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "Scheme Category")
                    {
                        ReportDocument subRepDoc = _SchemeDtl.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["HPR_SCH_CAT"].SetDataSource(HPR_SCH_CAT);
                    }

                    if (_cs.SubreportName == "scheme type")
                    {
                        ReportDocument subRepDoc = _SchemeDtl.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["HPR_SCH_TP"].SetDataSource(HPR_SCH_TP);
                    }

                    if (_cs.SubreportName == "scheme detail")
                    {
                        ReportDocument subRepDoc = _SchemeDtl.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["HPR_SCH_DET"].SetDataSource(HPR_SCH_DET);
                    }

                    if (_cs.SubreportName == "scheme Commission")
                    {
                        ReportDocument subRepDoc = _SchemeDtl.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["HPR_SCH_COMM"].SetDataSource(HPR_SCH_COMM);
                    }

                    if (_cs.SubreportName == "Scheme Schedule")
                    {
                        ReportDocument subRepDoc = _SchemeDtl.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["HPR_SCH_SHED"].SetDataSource(HPR_SCH_SHED);
                    }

                    if (_cs.SubreportName == "Scheme Reschedule")
                    {
                        ReportDocument subRepDoc = _SchemeDtl.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["HPR_SCH_RSCH"].SetDataSource(HPR_SCH_RSCH);
                    }

                    if (_cs.SubreportName == "Scheme Proof Doc")
                    {
                        ReportDocument subRepDoc = _SchemeDtl.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["HPR_SCH_PRD"].SetDataSource(HPR_SCH_PRD);
                    }

                    if (_cs.SubreportName == "Scheme SerCharge")
                    {
                        ReportDocument subRepDoc = _SchemeDtl.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["HPR_SER_CHG"].SetDataSource(HPR_SER_CHG);
                    }

                    if (_cs.SubreportName == "Scheme OtherCharge")
                    {
                        ReportDocument subRepDoc = _SchemeDtl.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["HPR_OTH_CHG"].SetDataSource(HPR_OTH_CHG);
                    }

                    if (_cs.SubreportName == "Scheme Guarantor")
                    {
                        ReportDocument subRepDoc = _SchemeDtl.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["HPR_GUA_PARM"].SetDataSource(HPR_GUA_PARM);
                    }
                }
            }

            _SchemeDtl.Database.Tables["param"].SetDataSource(param);
        }

        public void TransactionVarianceReport()
        {// Nadeeka 01-03-13
            DataTable param = new DataTable();
            DataRow dr;

            DataTable PROC_TRANS_VARIANCE = bsObj.CHNLSVC.Sales.TransactionVarienceReport(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbReportComp, BaseCls.GlbReportProfit);

            DataTable mst_com = default(DataTable);
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

            PROC_TRANS_VARIANCE = PROC_TRANS_VARIANCE.DefaultView.ToTable(true);

            _TransVar.Database.Tables["PROC_TRANS_VARIANCE"].SetDataSource(PROC_TRANS_VARIANCE);
            _TransVar.Database.Tables["mst_com"].SetDataSource(mst_com);
            _TransVar.Database.Tables["param"].SetDataSource(param);
            foreach (object repOp in _TransVar.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "price")
                    {
                        ReportDocument subRepDoc = _TransVar.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["PROC_TRANS_VARIANCE"].SetDataSource(PROC_TRANS_VARIANCE);

                    }
                    if (_cs.SubreportName == "Comm")
                    {
                        ReportDocument subRepDoc = _TransVar.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["PROC_TRANS_VARIANCE"].SetDataSource(PROC_TRANS_VARIANCE);

                    }
                    if (_cs.SubreportName == "DP")
                    {
                        ReportDocument subRepDoc = _TransVar.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["PROC_TRANS_VARIANCE"].SetDataSource(PROC_TRANS_VARIANCE);

                    }

                    if (_cs.SubreportName == "ins")
                    {
                        ReportDocument subRepDoc = _TransVar.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["PROC_TRANS_VARIANCE"].SetDataSource(PROC_TRANS_VARIANCE);

                    }
                }
            }

        }

        public void VehicleRegistrationDefReport()
        {// Nadeeka 01-03-13
            DataTable param = new DataTable();
            DataRow dr;
            //DataTable sar_veh_reg_defn = bsObj.CHNLSVC.MsgPortal.GetVehicleRegistrationReport(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbReportComp, BaseCls.GlbUserID, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode);

            DataTable sar_veh_reg_defn = bsObj.CHNLSVC.MsgPortal.GetVehicleRegistrationReport(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbReportComp, BaseCls.GlbUserID, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate));

            DataTable mst_com = default(DataTable);
            mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("SaleType", typeof(string));


            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            //dr["fromdate"] = BaseCls.GlbReportAsAtDate;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            //dr["todate"] = BaseCls.GlbReportAsAtDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["SaleType"] = BaseCls.GlbReportDocType;
            param.Rows.Add(dr);


            _vehiReg.Database.Tables["sar_veh_reg_defn"].SetDataSource(sar_veh_reg_defn);
            _vehiReg.Database.Tables["mst_com"].SetDataSource(mst_com);
            _vehiReg.Database.Tables["param"].SetDataSource(param);


        }




        //hasith 26/01/2015
        public void GetGVDetails()
        {
            DataTable param = new DataTable();
            DataTable GVDetails = new DataTable();
            DataTable mst_com = default(DataTable);
            DataRow dr;
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);
            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_GVDetails = new DataTable();
                    tmp_GVDetails = bsObj.CHNLSVC.MsgPortal.GetGVDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString());
                    GVDetails.Merge(tmp_GVDetails);
                }
            }
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("pc", typeof(string));
            param.Columns.Add("com", typeof(string));

            dr = param.NewRow();
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["pc"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["com"] = BaseCls.GlbReportComp;
            param.Rows.Add(dr);
            //GVDetails = bsObj.CHNLSVC.MsgPortal.GetGVDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, BaseCls.GlbReportProfit);
            _gvDetails.Database.Tables["GVDetails"].SetDataSource(GVDetails);
            _gvDetails.Database.Tables["param"].SetDataSource(param);
            _gvDetails.Database.Tables["mst_com"].SetDataSource(mst_com);





        }
    }
}
