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
namespace FF.WindowsERPClient.Reports.Service
{
    class clsServiceRep
    {
        public FF.WindowsERPClient.Reports.Service.Service_Job_Rpt _SvcJob = new FF.WindowsERPClient.Reports.Service.Service_Job_Rpt();
        // public FF.WindowsERPClient.Reports.Service.RCC_Print _rccPrint = new FF.WindowsERPClient.Reports.Service.RCC_Print();
        public FF.WindowsERPClient.Reports.Service.ServiceJobCard _serJob = new FF.WindowsERPClient.Reports.Service.ServiceJobCard();
        public FF.WindowsERPClient.Reports.Service.ServiceJobCardAut _serJobAuto = new FF.WindowsERPClient.Reports.Service.ServiceJobCardAut();
        public FF.WindowsERPClient.Reports.Service.RCCReport _rccReport = new FF.WindowsERPClient.Reports.Service.RCCReport();
        public FF.WindowsERPClient.Reports.Service.Service_Job_Charges_Report _SvcJobSumm = new FF.WindowsERPClient.Reports.Service.Service_Job_Charges_Report();

        public FF.WindowsERPClient.Reports.Service.Job_Summary _Jobsum = new FF.WindowsERPClient.Reports.Service.Job_Summary();
        public FF.WindowsERPClient.Reports.Service.Process_Tracking_Report _ProcTrack = new FF.WindowsERPClient.Reports.Service.Process_Tracking_Report();
        public FF.WindowsERPClient.Reports.Service.JobSheet_FieldVisit _Jobsheetfieldvisit = new FF.WindowsERPClient.Reports.Service.JobSheet_FieldVisit();
       
        public FF.WindowsERPClient.Reports.Service.JobGatePassnew _JobGatepassnew = new FF.WindowsERPClient.Reports.Service.JobGatePassnew();
        public FF.WindowsERPClient.Reports.Service.JobCard_Gen_Field _JobCardF = new FF.WindowsERPClient.Reports.Service.JobCard_Gen_Field();
        public FF.WindowsERPClient.Reports.Service.JobCard_Gen_WShop _JobCardW = new FF.WindowsERPClient.Reports.Service.JobCard_Gen_WShop();
        public FF.WindowsERPClient.Reports.Service.JobCard_ITS _JobCardITS = new FF.WindowsERPClient.Reports.Service.JobCard_ITS();
        public FF.WindowsERPClient.Reports.Service.JobCard_ITS_Field _JobCardITS_F = new FF.WindowsERPClient.Reports.Service.JobCard_ITS_Field();
        public FF.WindowsERPClient.Reports.Service.JobCard_Gen_WShop_Phone _JobCardWPh = new FF.WindowsERPClient.Reports.Service.JobCard_Gen_WShop_Phone();
        public FF.WindowsERPClient.Reports.Service.JobCard_Gen_WShop_Arial _JobCardWArial = new FF.WindowsERPClient.Reports.Service.JobCard_Gen_WShop_Arial();
        public FF.WindowsERPClient.Reports.Service.JobCard_Gen_MobileCheck _JobCardMCheck = new FF.WindowsERPClient.Reports.Service.JobCard_Gen_MobileCheck();
        public FF.WindowsERPClient.Reports.Service.JobCard_Gen_WShop_SGL _JobCardSGL = new FF.WindowsERPClient.Reports.Service.JobCard_Gen_WShop_SGL();
        public FF.WindowsERPClient.Reports.Service.JobCard_Gen_TechMemo _JobCardTMemo = new FF.WindowsERPClient.Reports.Service.JobCard_Gen_TechMemo();
        public FF.WindowsERPClient.Reports.Service.JobCard_Gen_TechMemo2 _JobCardTMemo2 = new FF.WindowsERPClient.Reports.Service.JobCard_Gen_TechMemo2();
        public FF.WindowsERPClient.Reports.Service.JobCard_AC_WShop _JobCardACW = new FF.WindowsERPClient.Reports.Service.JobCard_AC_WShop();
        public FF.WindowsERPClient.Reports.Service.JobCard_RF_WShop _JobCardRFW = new FF.WindowsERPClient.Reports.Service.JobCard_RF_WShop();
        public FF.WindowsERPClient.Reports.Service.JobCard_WM_WShop _JobCardWMW = new FF.WindowsERPClient.Reports.Service.JobCard_WM_WShop();
        public FF.WindowsERPClient.Reports.Service.JobCard_ACBD_WShop _JobCardACBDW = new FF.WindowsERPClient.Reports.Service.JobCard_ACBD_WShop();
        public FF.WindowsERPClient.Reports.Service.JobCard_ACBW_WShop _JobCardACBWW = new FF.WindowsERPClient.Reports.Service.JobCard_ACBW_WShop();
        public FF.WindowsERPClient.Reports.Service.JobCard_ACIns_WShop _JobCardACIns = new FF.WindowsERPClient.Reports.Service.JobCard_ACIns_WShop();
        public FF.WindowsERPClient.Reports.Service.Job_Estimate _JobEstimate = new FF.WindowsERPClient.Reports.Service.Job_Estimate();
        public FF.WindowsERPClient.Reports.Service.Job_Invoice _JobInvoice = new FF.WindowsERPClient.Reports.Service.Job_Invoice();
        public FF.WindowsERPClient.Reports.Service.Job_BER_Letter _BERLetter = new FF.WindowsERPClient.Reports.Service.Job_BER_Letter();
        public FF.WindowsERPClient.Reports.Service.Tech_Comments _techComnt = new FF.WindowsERPClient.Reports.Service.Tech_Comments();
        public FF.WindowsERPClient.Reports.Service.Repeated_Jobs _repeatJobs = new FF.WindowsERPClient.Reports.Service.Repeated_Jobs();
        public FF.WindowsERPClient.Reports.Service.Repeated_Jobs_Ph _repeatJobsph = new FF.WindowsERPClient.Reports.Service.Repeated_Jobs_Ph();
        public FF.WindowsERPClient.Reports.Service.Job_Defect_Analysis _defAnal = new FF.WindowsERPClient.Reports.Service.Job_Defect_Analysis();
        public FF.WindowsERPClient.Reports.Service.ServiceGP _sergp = new FF.WindowsERPClient.Reports.Service.ServiceGP();
        public FF.WindowsERPClient.Reports.Service.ServiceGP_Detail _sergpdtl = new FF.WindowsERPClient.Reports.Service.ServiceGP_Detail();
        public FF.WindowsERPClient.Reports.Service.ServiceStandyIssue _stdby = new FF.WindowsERPClient.Reports.Service.ServiceStandyIssue();
        public FF.WindowsERPClient.Reports.Service.smart_warr_Iss_Dtl_Report _smrt_Warr = new FF.WindowsERPClient.Reports.Service.smart_warr_Iss_Dtl_Report();
        public FF.WindowsERPClient.Reports.Service.Estimate_Det _estDet = new FF.WindowsERPClient.Reports.Service.Estimate_Det();

        public FF.WindowsERPClient.Reports.Service.SupplierWarranty _suppWar = new FF.WindowsERPClient.Reports.Service.SupplierWarranty();
        public FF.WindowsERPClient.Reports.Service.df_exchange_report _df_Exchg = new FF.WindowsERPClient.Reports.Service.df_exchange_report();
        public FF.WindowsERPClient.Reports.Service.Job_Invoice_Phone _JobInvoicePh = new FF.WindowsERPClient.Reports.Service.Job_Invoice_Phone();
        public FF.WindowsERPClient.Reports.Service.AgreementDet _serAgree= new FF.WindowsERPClient.Reports.Service.AgreementDet();
        public FF.WindowsERPClient.Reports.Service.Incentive_Detail _incdet = new FF.WindowsERPClient.Reports.Service.Incentive_Detail();
        public FF.WindowsERPClient.Reports.Service.SupplierWarranty_Excel _suppWarEx = new FF.WindowsERPClient.Reports.Service.SupplierWarranty_Excel();
        
        public FF.WindowsERPClient.Reports.Service.RCC_Report _rcc_Report = new FF.WindowsERPClient.Reports.Service.RCC_Report();
        public FF.WindowsERPClient.Reports.Service.Job_Invoiceall _JobInvoiceall = new FF.WindowsERPClient.Reports.Service.Job_Invoiceall();
        //Tharanga 
        public FF.WindowsERPClient.Reports.Service.Job_Gatepass _JobGatepass = new FF.WindowsERPClient.Reports.Service.Job_Gatepass();
        public FF.WindowsERPClient.Reports.Service.FeildJobCard _FeildJobCard = new FF.WindowsERPClient.Reports.Service.FeildJobCard();
        public FF.WindowsERPClient.Reports.Service.JobCard_Power_tools _JobCard_Power_tools = new FF.WindowsERPClient.Reports.Service.JobCard_Power_tools();
        public FF.WindowsERPClient.Reports.Service.job_EstimateAuto _job_EstimateAuto = new FF.WindowsERPClient.Reports.Service.job_EstimateAuto();
        public FF.WindowsERPClient.Reports.Service.JobCard_ABE_Work_shop _JobCard_ABE_Work_shop=new FF.WindowsERPClient.Reports.Service.JobCard_ABE_Work_shop();
        public FF.WindowsERPClient.Reports.Service.JobCard_ABE_Feild _JobCard_ABE_Feild = new FF.WindowsERPClient.Reports.Service.JobCard_ABE_Feild();
        public FF.WindowsERPClient.Reports.Service.Service_Invoice_ABE _Service_Invoice_ABE = new FF.WindowsERPClient.Reports.Service.Service_Invoice_ABE();
        public FF.WindowsERPClient.Reports.Inventory.Spare_Parts_Movement_Report _Spare_Parts_Movement_Report = new FF.WindowsERPClient.Reports.Inventory.Spare_Parts_Movement_Report();
        public FF.WindowsERPClient.Reports.Inventory.Spare_parts_movement _Spare_parts_movement = new FF.WindowsERPClient.Reports.Inventory.Spare_parts_movement();
        public FF.WindowsERPClient.Reports.Service.job_Estimate_ABE _job_Estimate_ABE = new FF.WindowsERPClient.Reports.Service.job_Estimate_ABE();
        public FF.WindowsERPClient.Reports.Service.Job_BER_Letter_ABE _Job_BER_Letter_ABE = new FF.WindowsERPClient.Reports.Service.Job_BER_Letter_ABE();
        public FF.WindowsERPClient.Reports.Service.RedyForCollection_ABE _RedyForCollection_ABE=new FF.WindowsERPClient.Reports.Service.RedyForCollection_ABE();
        public FF.WindowsERPClient.Reports.Service.RedyForDelivery_ABE _RedyForDelivery_ABE = new FF.WindowsERPClient.Reports.Service.RedyForDelivery_ABE();
        public FF.WindowsERPClient.Reports.Service.ReadyForDisposal_ABE _ReadyForDisposal_ABE = new FF.WindowsERPClient.Reports.Service.ReadyForDisposal_ABE();
        public FF.WindowsERPClient.Reports.Service.ShipmentLetter_ABE _ShipmentLetter_ABE = new FF.WindowsERPClient.Reports.Service.ShipmentLetter_ABE();
        public FF.WindowsERPClient.Reports.Service.BrandModelwiseItemDetails_ABE _BrandModelwiseItemDetails_ABE = new FF.WindowsERPClient.Reports.Service.BrandModelwiseItemDetails_ABE();
        public FF.WindowsERPClient.Reports.Service.Cannotbbrepaired_ABE _Cannotbbrepaired_ABE = new FF.WindowsERPClient.Reports.Service.Cannotbbrepaired_ABE();
        public FF.WindowsERPClient.Reports.Service.NoticeOfDisposal_ABE _NoticeOfDisposal_ABE = new FF.WindowsERPClient.Reports.Service.NoticeOfDisposal_ABE();
        public FF.WindowsERPClient.Reports.Service.BrandModelwiseItemSummary_ABE_N _BrandModelwiseItemSummary_ABE_N = new FF.WindowsERPClient.Reports.Service.BrandModelwiseItemSummary_ABE_N();

        //Tharindu 2017-12-14
        public FF.WindowsERPClient.Reports.Service.RCC_Letter objrccletter = new FF.WindowsERPClient.Reports.Service.RCC_Letter();

        DataTable tmp_user_pc = new DataTable();
        DataTable GLOB_DataTable = new DataTable();
       
        Base bsObj;
        public clsServiceRep()
        {
            bsObj = new Base();

        }
        public void ServiceJobEstimates()
        {// kapila 28/4/2015
            DataTable param = new DataTable();
            DataTable GLB_TABLE = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataTable tmp_job = new DataTable();
            DataRow dr;

            DataTable ServiceJobHdr = new DataTable();
            DataTable ServiceJobDet = new DataTable();
            DataTable ServiceJobEstHdr = new DataTable();
            DataTable ServiceJobEstDtl = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_COM = new DataTable();

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            param.Rows.Add(dr);

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow dr1 in tmp_user_pc.Rows)
                {
                    DataTable _TMPLOC = new DataTable();
                    _TMPLOC = new DataTable();
                    _TMPLOC = bsObj.CHNLSVC.CustService.sp_get_loc_details(dr1["tpl_pc"].ToString());
                    MST_LOC.Merge(_TMPLOC);

                    DataTable _TMP1 = new DataTable();
                    _TMP1 = bsObj.CHNLSVC.CustService.sp_get_com_details(dr1["tpl_com"].ToString());
                    MST_COM.Merge(_TMP1);

                    tmp_job = bsObj.CHNLSVC.CustService.sp_get_Estimatejobs(BaseCls.GlbReportCompCode, dr1["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDocType, BaseCls.GlbReportCusId, BaseCls.GlbReportTechncian);
                    foreach (DataRow drjob in tmp_job.Rows)
                    {
                        DataTable _TMP = new DataTable();
                        _TMP = bsObj.CHNLSVC.CustService.sp_get_Estimate_det(BaseCls.GlbReportCompCode, drjob["esh_estno"].ToString());
                        ServiceJobEstHdr.Merge(_TMP);

                         _TMP = new DataTable();
                         _TMP = bsObj.CHNLSVC.CustService.sp_get_EstimateItem_details(drjob["esh_estno"].ToString());
                        ServiceJobEstDtl.Merge(_TMP);

                        _TMP = new DataTable();
                        _TMP = bsObj.CHNLSVC.CustService.sp_get_job_header(drjob["ESH_JOB_NO"].ToString());
                        ServiceJobHdr.Merge(_TMP);

                        _TMP = new DataTable();
                        _TMP = bsObj.CHNLSVC.CustService.sp_get_job_details(drjob["ESH_JOB_NO"].ToString(), "JOB");
                        ServiceJobDet.Merge(_TMP);

                        _TMP = new DataTable();
                        _TMP = bsObj.CHNLSVC.CustService.sp_get_loc_details(drjob["ESH_LOC"].ToString());
                        MST_LOC.Merge(_TMP);

                        //_TMP = new DataTable();
                        //_TMP = bsObj.CHNLSVC.CustService.sp_get_com_details(drjob["ESH_COM"].ToString());
                        //MST_COM.Merge(_TMP);

                    }
                }
            }

            _estDet.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
            _estDet.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
            _estDet.Database.Tables["SCV_EST_HDR"].SetDataSource(ServiceJobEstHdr);
            _estDet.Database.Tables["SCV_EST_ITM"].SetDataSource(ServiceJobEstDtl);
            _estDet.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _estDet.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _estDet.Database.Tables["param"].SetDataSource(param);

        }

        public void RepeatedJobs_Report()
        {
            DataTable param = new DataTable();
            DataTable GLB_TECH = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_INV_BAL = new DataTable();
                    TMP_INV_BAL = bsObj.CHNLSVC.CustService.PrintRepeatedJobs(BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, "", BaseCls.GlbReportJobCat, BaseCls.GlbReportStatus);

                    GLB_TECH.Merge(TMP_INV_BAL);
                }
            }

            //  GLB_TECH = bsObj.CHNLSVC.CustService.PrintRepeatedJobs(BaseCls.GlbReportCompCode, "", Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3,"");

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("technician", typeof(string));
            param.Columns.Add("jobcategory", typeof(string));
            param.Columns.Add("itemtype", typeof(string));
            param.Columns.Add("jobstatus", typeof(string));
            param.Columns.Add("warrantystatus", typeof(string));
            param.Columns.Add("jobno", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("jobstage", typeof(Int32));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["technician"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobcategory"] = BaseCls.GlbReportJobCat == "" ? "ALL" : BaseCls.GlbReportJobCat;
            dr["itemtype"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
           // dr["jobstage"] = BaseCls.GlbReportJobStatus == "" ? "0" : BaseCls.GlbReportJobStatus;
            dr["warrantystatus"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["warrantystatus"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;

            param.Rows.Add(dr);

            if (BaseCls.GlbReportName == "Repeated_Jobs_Ph.rpt")
            {
                _repeatJobsph.Database.Tables["REPEAT_JOBS"].SetDataSource(GLB_TECH);
                _repeatJobsph.Database.Tables["param"].SetDataSource(param);
            }
            else
            {
                _repeatJobs.Database.Tables["REPEAT_JOBS"].SetDataSource(GLB_TECH);
                _repeatJobs.Database.Tables["param"].SetDataSource(param);
            }
        }

        public void DefectAnalysis_Report()
        {
            DataTable param = new DataTable();
            DataTable GLB_TECH = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DEFANAL = new DataTable();
                    TMP_DEFANAL = bsObj.CHNLSVC.CustService.DefectAnalysisDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportDefectType, BaseCls.GlbReportWarrStatus, BaseCls.GlbUserID);

                    GLB_TECH.Merge(TMP_DEFANAL);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("warrantystatus", typeof(string));
            param.Columns.Add("comp", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["warrantystatus"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportDefectType == "" ? "ALL" : BaseCls.GlbReportDefectType;

            param.Rows.Add(dr);

            _defAnal.Database.Tables["DEFECT_ANALYSIS"].SetDataSource(GLB_TECH);
            _defAnal.Database.Tables["param"].SetDataSource(param);
        }

        public void DFExchange_Report()
        {
            DataTable param = new DataTable();
            DataTable GLB_EXCHG = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_INV_BAL = new DataTable();
                    TMP_INV_BAL = bsObj.CHNLSVC.CustService.DFExchangeDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportIsExport, BaseCls.GlbReportWarrStatus);

                    GLB_EXCHG.Merge(TMP_INV_BAL);
                }
            }

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

            _df_Exchg.Database.Tables["DF_EXCHANGE"].SetDataSource(GLB_EXCHG);
            _df_Exchg.Database.Tables["param"].SetDataSource(param);
        }


        public void TechComment_Report()
        {
            DataTable param = new DataTable();
            DataTable GLB_TECH = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_INV_BAL = new DataTable();
                    TMP_INV_BAL = bsObj.CHNLSVC.CustService.PrintTechComments(BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportJobNo, BaseCls.GlbReportDoc, BaseCls.GlbReportJobCat);

                    GLB_TECH.Merge(TMP_INV_BAL);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("technician", typeof(string));
            param.Columns.Add("jobcategory", typeof(string));
            param.Columns.Add("itemtype", typeof(string));
            param.Columns.Add("jobstatus", typeof(string));
            param.Columns.Add("warrantystatus", typeof(string));
            param.Columns.Add("jobno", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("coment", typeof(string));


            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["technician"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobcategory"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["itemtype"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobstatus"] = BaseCls.GlbReportJobCat == "" ? "ALL" : BaseCls.GlbReportJobCat;
            dr["warrantystatus"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["coment"] = BaseCls.GlbReportDoc == "" ? "ALL" : BaseCls.GlbReportDoc;

            param.Rows.Add(dr);

            _techComnt.Database.Tables["TECH_COMENT"].SetDataSource(GLB_TECH);
            _techComnt.Database.Tables["param"].SetDataSource(param);
        }

        public void ServiceJobEstimate()
        {
            //Sanjeewa 2015-02-20 
            DataTable ServiceJobHdr = new DataTable();
            DataTable ServiceJobDet = new DataTable();
            DataTable ServiceJobEstHdr = new DataTable();
            DataTable ServiceJobEstDtl = new DataTable();
            DataTable ReportConditions = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_COM = new DataTable();

            ServiceJobEstHdr = bsObj.CHNLSVC.CustService.sp_get_Estimate_details(BaseCls.GlbReportDoc);
            ServiceJobEstDtl = bsObj.CHNLSVC.CustService.sp_get_EstimateItem_details(BaseCls.GlbReportDoc);

            if (ServiceJobEstHdr.Rows.Count > 0)
            {
                foreach (DataRow drow in ServiceJobEstHdr.Rows)
                {
                    {
                        ServiceJobHdr = bsObj.CHNLSVC.CustService.sp_get_job_header(drow["ESH_JOB_NO"].ToString());
                        ServiceJobDet = bsObj.CHNLSVC.CustService.sp_get_job_details(drow["ESH_JOB_NO"].ToString(), "JOB");
                        MST_LOC = bsObj.CHNLSVC.CustService.sp_get_loc_details(drow["ESH_LOC"].ToString());
                        MST_COM = bsObj.CHNLSVC.CustService.sp_get_com_details(drow["ESH_COM"].ToString());
                    }
                }
            }

            if (MST_LOC.Rows.Count > 0)
            {
                foreach (DataRow drow1 in MST_LOC.Rows)
                {
                    ReportConditions = bsObj.CHNLSVC.CustService.sp_get_Report_info_chnl(BaseCls.GlbReportName, drow1["ML_CATE_3"].ToString());
                }
            }

            _JobEstimate.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
            _JobEstimate.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
            _JobEstimate.Database.Tables["SCV_EST_HDR"].SetDataSource(ServiceJobEstHdr);
            _JobEstimate.Database.Tables["SCV_EST_ITM"].SetDataSource(ServiceJobEstDtl);
            _JobEstimate.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _JobEstimate.Database.Tables["MST_COM"].SetDataSource(MST_COM);

            foreach (object repOp in _JobEstimate.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "Conditions")
                    {
                        ReportDocument subRepDoc = _JobEstimate.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                    }
                }
            }

        }

        public void ServiceJobCardPrint()
        {
            //Sanjeewa 2015-02-20 
            DataTable ServiceJobHdr = new DataTable();
            DataTable ServiceJobDet = new DataTable();
            DataTable ServiceJobDef = new DataTable();
            DataTable ServiceJobTech = new DataTable();
            DataTable ReportConditions = new DataTable();
            DataTable ServiceJobDetSub = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable USER = new DataTable();

            ServiceJobHdr = bsObj.CHNLSVC.CustService.sp_get_job_header(BaseCls.GlbReportDoc);
            ServiceJobDet = bsObj.CHNLSVC.CustService.sp_get_job_details(BaseCls.GlbReportDoc, "JOB");
            ServiceJobDef = bsObj.CHNLSVC.CustService.sp_get_job_defects(BaseCls.GlbReportDoc);
            ServiceJobTech = bsObj.CHNLSVC.CustService.getJobTechnician(BaseCls.GlbReportDoc, BaseCls.GlbUserComCode);
            ServiceJobDetSub = bsObj.CHNLSVC.CustService.sp_get_job_detailsSub(BaseCls.GlbReportDoc);
            //USER = bsObj.CHNLSVC.CustService.getServiceJobUser(BaseCls.GlbUserID);

            if (ServiceJobHdr.Rows.Count > 0)
            {
                foreach (DataRow drow in ServiceJobHdr.Rows)
                {
                    MST_COM = bsObj.CHNLSVC.CustService.sp_get_com_details(drow["SJB_COM"].ToString());
                }
            }
            if (ServiceJobDet.Rows.Count > 0)
            {
                foreach (DataRow drow in ServiceJobDet.Rows)
                {
                    MST_LOC = bsObj.CHNLSVC.CustService.sp_get_loc_details(drow["JBD_LOC"].ToString());

                    if (MST_LOC.Rows.Count > 0)
                    {
                        foreach (DataRow drow1 in MST_LOC.Rows)
                        {
                            ReportConditions = bsObj.CHNLSVC.CustService.sp_get_Report_info_chnl(BaseCls.GlbReportName, drow1["ML_CATE_3"].ToString());
                        }
                    }
                }
            }

            if (BaseCls.GlbReportName == "JobCard_Gen_Field.rpt")
            {
                _JobCardF.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _JobCardF.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _JobCardF.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                _JobCardF.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _JobCardF.Database.Tables["MST_COM"].SetDataSource(MST_COM);

                foreach (object repOp in _JobCardF.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Conditions")
                        {
                            ReportDocument subRepDoc = _JobCardF.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                        }
                        if (_cs.SubreportName == "JobTechnican")
                        {
                            ReportDocument subRepDoc = _JobCardF.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["JOB_TECHNICIAN"].SetDataSource(ServiceJobTech);
                        }
                    }
                }
            }

            if (BaseCls.GlbReportName == "JobCard_Gen_WShop.rpt")
            {
                _JobCardW.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _JobCardW.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _JobCardW.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                _JobCardW.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _JobCardW.Database.Tables["MST_COM"].SetDataSource(MST_COM);

                foreach (object repOp in _JobCardW.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Conditions")
                        {
                            ReportDocument subRepDoc = _JobCardW.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                        }
                        if (_cs.SubreportName == "JobTechnican")
                        {
                            ReportDocument subRepDoc = _JobCardW.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["JOB_TECHNICIAN"].SetDataSource(ServiceJobTech);
                        }
                    }
                }
            }

            if (BaseCls.GlbReportName == "JobCard_Gen_WShop_SGL.rpt")
            {
                _JobCardSGL.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _JobCardSGL.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _JobCardSGL.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                _JobCardSGL.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _JobCardSGL.Database.Tables["MST_COM"].SetDataSource(MST_COM);

                foreach (object repOp in _JobCardSGL.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Conditions")
                        {
                            ReportDocument subRepDoc = _JobCardSGL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                        }
                        if (_cs.SubreportName == "JobTechnican")
                        {
                            ReportDocument subRepDoc = _JobCardSGL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["JOB_TECHNICIAN"].SetDataSource(ServiceJobTech);
                        }
                        if (_cs.SubreportName == "sub_serial")
                        {
                            ReportDocument subRepDoc = _JobCardSGL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["SCV_JOB_DET_SUB"].SetDataSource(ServiceJobDetSub);
                        }
                    }
                }
            }

            if (BaseCls.GlbReportName == "JobCard_ITS.rpt")
            {
                _JobCardITS.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _JobCardITS.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _JobCardITS.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                _JobCardITS.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _JobCardITS.Database.Tables["MST_COM"].SetDataSource(MST_COM);

                foreach (object repOp in _JobCardITS.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Conditions")
                        {
                            ReportDocument subRepDoc = _JobCardITS.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                        }
                        if (_cs.SubreportName == "JobTechnican")
                        {
                            ReportDocument subRepDoc = _JobCardITS.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["JOB_TECHNICIAN"].SetDataSource(ServiceJobTech);
                        }
                    }
                }
            }

            if (BaseCls.GlbReportName == "JobCard_Gen_TechMemo.rpt")
            {
                _JobCardTMemo.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _JobCardTMemo.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _JobCardTMemo.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                _JobCardTMemo.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _JobCardTMemo.Database.Tables["MST_COM"].SetDataSource(MST_COM);

                foreach (object repOp in _JobCardTMemo.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        
                        if (_cs.SubreportName == "JobTechnican")
                        {
                            ReportDocument subRepDoc = _JobCardTMemo.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["JOB_TECHNICIAN"].SetDataSource(ServiceJobTech);
                        }
                    }
                }
            }

            if (BaseCls.GlbReportName == "JobCard_Gen_TechMemo2.rpt")
            {
                _JobCardTMemo2.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _JobCardTMemo2.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _JobCardTMemo2.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                _JobCardTMemo2.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _JobCardTMemo2.Database.Tables["MST_COM"].SetDataSource(MST_COM);

                foreach (object repOp in _JobCardTMemo2.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        
                        if (_cs.SubreportName == "JobTechnican")
                        {
                            ReportDocument subRepDoc = _JobCardTMemo2.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["JOB_TECHNICIAN"].SetDataSource(ServiceJobTech);
                        }
                    }
                }
            }

            if (BaseCls.GlbReportName == "JobCard_ITS_Field.rpt")
            {
                DataTable param = new DataTable();
                DataRow dr;

                param.Columns.Add("visitLine", typeof(Int32));

                dr = param.NewRow();
                dr["visitLine"] = BaseCls.GlbReportParaLine2;
                param.Rows.Add(dr);

                _JobCardITS_F.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _JobCardITS_F.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _JobCardITS_F.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                _JobCardITS_F.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _JobCardITS_F.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _JobCardITS_F.Database.Tables["param"].SetDataSource(param);

                foreach (object repOp in _JobCardITS_F.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Conditions")
                        {
                            ReportDocument subRepDoc = _JobCardITS_F.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                        }
                        if (_cs.SubreportName == "JobTechnican")
                        {
                            ReportDocument subRepDoc = _JobCardITS_F.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["JOB_TECHNICIAN"].SetDataSource(ServiceJobTech);
                        }
                    }
                }
            }

            if (BaseCls.GlbReportName == "JobCard_AC_WShop.rpt")
            {
                _JobCardACW.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                //_JobCardACW.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                //_JobCardACW.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                _JobCardACW.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _JobCardACW.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                //_JobCardACW.Database.Tables["USER"].SetDataSource(USER);

                DataTable JOB_DET = new DataTable();
                DataRow dr;

                JOB_DET.Columns.Add("JOB_NO", typeof(string));
                JOB_DET.Columns.Add("JOB_LINE", typeof(Int16));   
                JOB_DET.Columns.Add("COL_HEAD", typeof(string));                
                JOB_DET.Columns.Add("ITEM_1", typeof(string));
                JOB_DET.Columns.Add("ITEM_2", typeof(string));
                JOB_DET.Columns.Add("ITEM_3", typeof(string));

                string vSerial1="";
                string vSerial2="";
                string vSerial3="";
                string vSerial21 = "";
                string vSerial22 = "";
                string vSerial23 = "";
                string vModel1="";
                string vModel2="";
                string vModel3="";

                int vCount = 0;
                int vLine = 0;

                foreach (DataRow row in ServiceJobDet.Rows)
                {                   
                    vCount = vCount + 1;
                    vLine = vLine + 1;
                    if (vCount == 1)
                    {
                        vSerial1 = row["JBD_SER1"].ToString();
                        vSerial21 = row["JBD_SER2"].ToString();
                        vModel1 = row["JBD_MODEL"].ToString();
                    }
                    if (vCount == 2)
                    {
                        vSerial2 = row["JBD_SER1"].ToString();
                        vSerial22 = row["JBD_SER2"].ToString();
                        vModel2 = row["JBD_MODEL"].ToString();
                    }
                    if (vCount == 3)
                    {
                        vSerial3 = row["JBD_SER1"].ToString();
                        vSerial23 = row["JBD_SER2"].ToString();
                        vModel3 = row["JBD_MODEL"].ToString();
                    }

                    if (vCount == 3 | ServiceJobDet.Rows.Count == vLine)
                    {
                        vCount = 0;
                        dr = JOB_DET.NewRow();
                        dr["JOB_NO"] = row["JBD_JOBNO"].ToString();
                        dr["JOB_LINE"] = vLine;
                        dr["COL_HEAD"] = "Model";
                        dr["ITEM_1"] = vModel1;
                        dr["ITEM_2"] = vModel2;
                        dr["ITEM_3"] = vModel3;
                        JOB_DET.Rows.Add(dr);

                        dr = JOB_DET.NewRow();
                        dr["JOB_NO"] = row["JBD_JOBNO"].ToString();
                        dr["JOB_LINE"] = vLine;
                        dr["COL_HEAD"] = "Equipment Location";
                        dr["ITEM_1"] = "";
                        dr["ITEM_2"] = "";
                        dr["ITEM_3"] = "";
                        JOB_DET.Rows.Add(dr);

                        dr = JOB_DET.NewRow();
                        dr["JOB_NO"] = row["JBD_JOBNO"].ToString();
                        dr["JOB_LINE"] = vLine;
                        dr["COL_HEAD"] = "Indoor Serial No";
                        dr["ITEM_1"] = vSerial21;
                        dr["ITEM_2"] = vSerial22;
                        dr["ITEM_3"] = vSerial23;
                        JOB_DET.Rows.Add(dr);

                        dr = JOB_DET.NewRow();
                        dr["JOB_NO"] = row["JBD_JOBNO"].ToString();
                        dr["JOB_LINE"] = vLine;
                        dr["COL_HEAD"] = "Outdoor Serial No";
                        dr["ITEM_1"] = vSerial1;
                        dr["ITEM_2"] = vSerial2;
                        dr["ITEM_3"] = vSerial3;
                        JOB_DET.Rows.Add(dr);

                        dr = JOB_DET.NewRow();
                        dr["JOB_NO"] = row["JBD_JOBNO"].ToString();
                        dr["JOB_LINE"] = vLine;
                        dr["COL_HEAD"] = "Clean Air filters and Blower";
                        dr["ITEM_1"] = "";
                        dr["ITEM_2"] = "";
                        dr["ITEM_3"] = "";
                        JOB_DET.Rows.Add(dr);

                        dr = JOB_DET.NewRow();
                        dr["JOB_NO"] = row["JBD_JOBNO"].ToString();
                        dr["JOB_LINE"] = vLine;
                        dr["COL_HEAD"] = "Clean Evaporator coil";
                        dr["ITEM_1"] = "";
                        dr["ITEM_2"] = "";
                        dr["ITEM_3"] = "";
                        JOB_DET.Rows.Add(dr);

                        dr = JOB_DET.NewRow();
                        dr["JOB_NO"] = row["JBD_JOBNO"].ToString();
                        dr["JOB_LINE"] = vLine;
                        dr["COL_HEAD"] = "Clean Condensor coil";
                        dr["ITEM_1"] = "";
                        dr["ITEM_2"] = "";
                        dr["ITEM_3"] = "";
                        JOB_DET.Rows.Add(dr);

                        dr = JOB_DET.NewRow();
                        dr["JOB_NO"] = row["JBD_JOBNO"].ToString();
                        dr["JOB_LINE"] = vLine;
                        dr["COL_HEAD"] = "Check Drain Leakage";
                        dr["ITEM_1"] = "";
                        dr["ITEM_2"] = "";
                        dr["ITEM_3"] = "";
                        JOB_DET.Rows.Add(dr);

                        dr = JOB_DET.NewRow();
                        dr["JOB_NO"] = row["JBD_JOBNO"].ToString();
                        dr["JOB_LINE"] = vLine;
                        dr["COL_HEAD"] = "Suction Pressure";
                        dr["ITEM_1"] = "";
                        dr["ITEM_2"] = "";
                        dr["ITEM_3"] = "";
                        JOB_DET.Rows.Add(dr);

                        dr = JOB_DET.NewRow();
                        dr["JOB_NO"] = row["JBD_JOBNO"].ToString();
                        dr["JOB_LINE"] = vLine;
                        dr["COL_HEAD"] = "Discharge Pressure";
                        dr["ITEM_1"] = "";
                        dr["ITEM_2"] = "";
                        dr["ITEM_3"] = "";
                        JOB_DET.Rows.Add(dr);

                        dr = JOB_DET.NewRow();
                        dr["JOB_NO"] = row["JBD_JOBNO"].ToString();
                        dr["JOB_LINE"] = vLine;
                        dr["COL_HEAD"] = "Running Current";
                        dr["ITEM_1"] = "";
                        dr["ITEM_2"] = "";
                        dr["ITEM_3"] = "";
                        JOB_DET.Rows.Add(dr);

                        dr = JOB_DET.NewRow();
                        dr["JOB_NO"] = row["JBD_JOBNO"].ToString();
                        dr["JOB_LINE"] = vLine;
                        dr["COL_HEAD"] = "Loose connection on Comp. Contactors";
                        dr["ITEM_1"] = "";
                        dr["ITEM_2"] = "";
                        dr["ITEM_3"] = "";
                        JOB_DET.Rows.Add(dr);

                        dr = JOB_DET.NewRow();
                        dr["JOB_NO"] = row["JBD_JOBNO"].ToString();
                        dr["JOB_LINE"] = vLine;
                        dr["COL_HEAD"] = "Loose conection on Elect. Panel";
                        dr["ITEM_1"] = "";
                        dr["ITEM_2"] = "";
                        dr["ITEM_3"] = "";
                        JOB_DET.Rows.Add(dr);

                        dr = JOB_DET.NewRow();
                        dr["JOB_NO"] = row["JBD_JOBNO"].ToString();
                        dr["JOB_LINE"] = vLine;
                        dr["COL_HEAD"] = "Temperature Controller Functions";
                        dr["ITEM_1"] = "";
                        dr["ITEM_2"] = "";
                        dr["ITEM_3"] = "";
                        JOB_DET.Rows.Add(dr);

                        dr = JOB_DET.NewRow();
                        dr["JOB_NO"] = row["JBD_JOBNO"].ToString();
                        dr["JOB_LINE"] = vLine;
                        dr["COL_HEAD"] = "Abnormal noise on Blower";
                        dr["ITEM_1"] = "";
                        dr["ITEM_2"] = "";
                        dr["ITEM_3"] = "";
                        JOB_DET.Rows.Add(dr);

                        vSerial1 = "";
                        vSerial2 = "";
                        vSerial3 = "";
                        vSerial21 = "";
                        vSerial22 = "";
                        vSerial23 = "";
                        vModel1 = "";
                        vModel2 = "";
                        vModel3 = "";
                    }
                }
                _JobCardACW.Database.Tables["JOB_DET"].SetDataSource(JOB_DET);

                foreach (object repOp in _JobCardACW.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Conditions")
                        {
                            ReportDocument subRepDoc = _JobCardACW.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                        }
                        if (_cs.SubreportName == "JobTechnican")
                        {
                            ReportDocument subRepDoc = _JobCardACW.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["JOB_TECHNICIAN"].SetDataSource(ServiceJobTech);
                        }
                    }
                }
            }

            if (BaseCls.GlbReportName == "JobCard_ACBD_WShop.rpt")
            {
                _JobCardACBDW.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _JobCardACBDW.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _JobCardACBDW.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                _JobCardACBDW.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _JobCardACBDW.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                //_JobCardACBDW.Database.Tables["USER"].SetDataSource(USER);

                foreach (object repOp in _JobCardACBDW.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Conditions")
                        {
                            ReportDocument subRepDoc = _JobCardACBDW.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                        }
                        if (_cs.SubreportName == "JobTechnican")
                        {
                            ReportDocument subRepDoc = _JobCardACBDW.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["JOB_TECHNICIAN"].SetDataSource(ServiceJobTech);
                        }
                    }
                }
            }

            if (BaseCls.GlbReportName == "JobCard_ACBW_WShop.rpt")
            {
                _JobCardACBWW.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _JobCardACBWW.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _JobCardACBWW.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                _JobCardACBWW.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _JobCardACBWW.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                //_JobCardACBWW.Database.Tables["USER"].SetDataSource(USER);

                foreach (object repOp in _JobCardACBWW.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Conditions")
                        {
                            ReportDocument subRepDoc = _JobCardACBWW.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                        }
                        if (_cs.SubreportName == "JobTechnican")
                        {
                            ReportDocument subRepDoc = _JobCardACBWW.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["JOB_TECHNICIAN"].SetDataSource(ServiceJobTech);
                        }
                    }
                }
            }

            if (BaseCls.GlbReportName == "JobCard_ACIns_WShop.rpt")
            {
                _JobCardACIns.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _JobCardACIns.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _JobCardACIns.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                _JobCardACIns.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _JobCardACIns.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                //_JobCardACIns.Database.Tables["USER"].SetDataSource(USER);

                foreach (object repOp in _JobCardACIns.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Conditions")
                        {
                            ReportDocument subRepDoc = _JobCardACIns.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                        }
                        if (_cs.SubreportName == "JobTechnican")
                        {
                            ReportDocument subRepDoc = _JobCardACIns.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["JOB_TECHNICIAN"].SetDataSource(ServiceJobTech);
                        }
                    }
                }
            }

            if (BaseCls.GlbReportName == "JobCard_RF_WShop.rpt")
            {
                _JobCardRFW.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _JobCardRFW.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _JobCardRFW.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                _JobCardRFW.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _JobCardRFW.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                //_JobCardRFW.Database.Tables["USER"].SetDataSource(USER);

                foreach (object repOp in _JobCardRFW.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Conditions")
                        {
                            ReportDocument subRepDoc = _JobCardRFW.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                        }
                        if (_cs.SubreportName == "JobTechnican")
                        {
                            ReportDocument subRepDoc = _JobCardRFW.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["JOB_TECHNICIAN"].SetDataSource(ServiceJobTech);
                        }
                    }
                }
            }

            if (BaseCls.GlbReportName == "JobCard_WM_WShop.rpt")
            {
                _JobCardWMW.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _JobCardWMW.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _JobCardWMW.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                _JobCardWMW.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _JobCardWMW.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                //_JobCardWMW.Database.Tables["USER"].SetDataSource(USER);

                foreach (object repOp in _JobCardWMW.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Conditions")
                        {
                            ReportDocument subRepDoc = _JobCardWMW.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                        }
                        if (_cs.SubreportName == "JobTechnican")
                        {
                            ReportDocument subRepDoc = _JobCardWMW.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["JOB_TECHNICIAN"].SetDataSource(ServiceJobTech);
                        }
                    }
                }
            }

            if (BaseCls.GlbReportName == "JobCard_Gen_WShop_Phone.rpt")
            {
                _JobCardWPh.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _JobCardWPh.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _JobCardWPh.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                _JobCardWPh.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _JobCardWPh.Database.Tables["MST_COM"].SetDataSource(MST_COM);

                foreach (object repOp in _JobCardWPh.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Conditions")
                        {
                            ReportDocument subRepDoc = _JobCardWPh.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                        }
                        if (_cs.SubreportName == "JobTechnican")
                        {
                            ReportDocument subRepDoc = _JobCardWPh.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["JOB_TECHNICIAN"].SetDataSource(ServiceJobTech);
                        }
                        if (_cs.SubreportName == "SubSerials")
                        {
                            ReportDocument subRepDoc = _JobCardWPh.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["SCV_JOB_DET_SUB"].SetDataSource(ServiceJobDetSub);
                        }
                    }
                }
            }

            if (BaseCls.GlbReportName == "JobCard_Gen_WShop_Arial.rpt")
            {
                _JobCardWArial.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _JobCardWArial.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _JobCardWArial.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                _JobCardWArial.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _JobCardWArial.Database.Tables["MST_COM"].SetDataSource(MST_COM);

                foreach (object repOp in _JobCardWArial.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Conditions")
                        {
                            ReportDocument subRepDoc = _JobCardWArial.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                        }
                    }
                }
            }
            if (BaseCls.GlbReportName == "JobCard_ABE_Work_shop.rpt")
            {
                _JobCard_ABE_Work_shop.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _JobCard_ABE_Work_shop.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _JobCard_ABE_Work_shop.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                _JobCard_ABE_Work_shop.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _JobCard_ABE_Work_shop.Database.Tables["MST_COM"].SetDataSource(MST_COM);

                foreach (object repOp in _JobCard_ABE_Work_shop.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Conditions")
                        {
                            ReportDocument subRepDoc = _JobCard_ABE_Work_shop.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                        }
                        if (_cs.SubreportName == "JobTechnican")
                        {
                            ReportDocument subRepDoc = _JobCard_ABE_Work_shop.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["JOB_TECHNICIAN"].SetDataSource(ServiceJobTech);
                        }
                        if (_cs.SubreportName == "sub_serial")
                        {
                            ReportDocument subRepDoc = _JobCard_ABE_Work_shop.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["SCV_JOB_DET_SUB"].SetDataSource(ServiceJobDetSub);
                        }
                    }
                }
            }

        }
        //Tharanga 2017/06/13
        public void ServiceJobCardPowerToolPrint()
        {
            
            DataTable ServiceJobHdr = new DataTable();
            DataTable ServiceJobDet = new DataTable();
            DataTable ServiceJobDef = new DataTable();
            DataTable ServiceJobTech = new DataTable();
            DataTable ReportConditions = new DataTable();
            DataTable ServiceJobDetSub = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable USER = new DataTable();

            ServiceJobHdr = bsObj.CHNLSVC.CustService.sp_get_job_header(BaseCls.GlbReportDoc);
            ServiceJobDet = bsObj.CHNLSVC.CustService.sp_get_job_details(BaseCls.GlbReportDoc, "JOB");
            ServiceJobDef = bsObj.CHNLSVC.CustService.sp_get_job_defects(BaseCls.GlbReportDoc);
            ServiceJobTech = bsObj.CHNLSVC.CustService.getJobTechnician(BaseCls.GlbReportDoc, BaseCls.GlbUserComCode);
            ServiceJobDetSub = bsObj.CHNLSVC.CustService.sp_get_job_detailsSub(BaseCls.GlbReportDoc);
            //USER = bsObj.CHNLSVC.CustService.getServiceJobUser(BaseCls.GlbUserID);

            if (ServiceJobHdr.Rows.Count > 0)
            {
                foreach (DataRow drow in ServiceJobHdr.Rows)
                {
                    MST_COM = bsObj.CHNLSVC.CustService.sp_get_com_details(drow["SJB_COM"].ToString());
                }
            }
            if (ServiceJobDet.Rows.Count > 0)
            {
                foreach (DataRow drow in ServiceJobDet.Rows)
                {
                    MST_LOC = bsObj.CHNLSVC.CustService.sp_get_loc_details(drow["JBD_LOC"].ToString());

                    if (MST_LOC.Rows.Count > 0)
                    {
                        foreach (DataRow drow1 in MST_LOC.Rows)
                        {
                            ReportConditions = bsObj.CHNLSVC.CustService.sp_get_Report_info_chnl(BaseCls.GlbReportName, drow1["ML_CATE_3"].ToString());
                        }
                    }
                }
            }




            if (BaseCls.GlbReportName == "JobCard_Power_tools.rpt")
            {
                _JobCard_Power_tools.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _JobCard_Power_tools.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _JobCard_Power_tools.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                _JobCard_Power_tools.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _JobCard_Power_tools.Database.Tables["MST_COM"].SetDataSource(MST_COM);

                foreach (object repOp in _JobCard_Power_tools.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Conditions")
                        {
                            ReportDocument subRepDoc = _JobCard_Power_tools.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                        }
                        if (_cs.SubreportName == "JobTechnican")
                        {
                            ReportDocument subRepDoc = _JobCard_Power_tools.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["JOB_TECHNICIAN"].SetDataSource(ServiceJobTech);
                        }
                        if (_cs.SubreportName == "sub_serial")
                        {
                            ReportDocument subRepDoc = _JobCard_Power_tools.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["SCV_JOB_DET_SUB"].SetDataSource(ServiceJobDetSub);
                        }
                    }
                }
            }

        }
        public void BERLetterPrint()
        {
            //Sanjeewa 2015-05-16             
            DataTable BER = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable ReportConditions = new DataTable();
            DataTable ServiceJobDetSub = new DataTable();
          
            BER = bsObj.CHNLSVC.CustService.BERLetterDetails(BaseCls.GlbReportJobNo, BaseCls.GlbReportTp);
            ServiceJobDetSub = bsObj.CHNLSVC.CustService.sp_get_job_detailsSub(BaseCls.GlbReportJobNo);

            if (BER.Rows.Count > 0)
            {
                foreach (DataRow drow in BER.Rows)
                {
                    if (drow["ref_type"].ToString() == "BER")
                    {
                        MST_LOC = bsObj.CHNLSVC.CustService.sp_get_loc_details(drow["REF_PC_CODE"].ToString());
                    }
                    else
                    {
                        MST_LOC = bsObj.CHNLSVC.CustService.sp_get_locbypc_details(BaseCls.GlbUserComCode, drow["REF_PC_CODE"].ToString());

                        if (MST_LOC.Rows.Count == 0)
                        {
                            MST_LOC = bsObj.CHNLSVC.CustService.sp_get_loc_details(drow["REF_PC_CODE"].ToString());
                        }
                    }

                    if (MST_LOC.Rows.Count > 0)
                    {
                        foreach (DataRow drow1 in MST_LOC.Rows)
                        {
                            ReportConditions = bsObj.CHNLSVC.CustService.sp_get_Report_info_chnl(BaseCls.GlbReportName, drow1["ML_CATE_3"].ToString());
                        }
                    }
                }
            }
            else
            {
                ReportConditions = bsObj.CHNLSVC.CustService.sp_get_Report_info_chnl(BaseCls.GlbReportName, BaseCls.GlbDefSubChannel);
            }

            _BERLetter.Database.Tables["BER_REQUEST"].SetDataSource(BER);
            _BERLetter.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);

            foreach (object repOp in _BERLetter.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "sub_serials")
                    {
                        ReportDocument subRepDoc = _BERLetter.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SCV_JOB_DET_SUB"].SetDataSource(ServiceJobDetSub);
                    }
                }
            }
            
        }

        public void ServiceGatepass()
        {
            //Sanjeewa 2015-02-19            
            DataTable ServiceJobGatepass = bsObj.CHNLSVC.CustService.sp_get_gatepass_details(BaseCls.GlbReportDoc);
            DataTable ServiceJobGatepass_1 = ServiceJobGatepass.DefaultView.ToTable(true, "SGP_COM", "SGP_LOC", "SGP_JOBNO");
            DataTable ServiceJobHdr = new DataTable();
            DataTable ServiceJobDet = new DataTable();
            DataTable ServiceJobOldpart = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_COM = new DataTable();
            int i = 0;

            if (ServiceJobGatepass_1.Rows.Count > 0)
            {
                foreach (DataRow drow in ServiceJobGatepass_1.Rows)
                {
                    i = i + 1;
                    if (i == 1)
                    {
                        MST_LOC = bsObj.CHNLSVC.CustService.sp_get_loc_details(drow["SGP_LOC"].ToString());
                        MST_COM = bsObj.CHNLSVC.CustService.sp_get_com_details(drow["SGP_COM"].ToString());
                    }
                    DataTable ServiceJobHdr1 = new DataTable();
                    DataTable ServiceJobDet1 = new DataTable();

                    ServiceJobHdr1 = bsObj.CHNLSVC.CustService.sp_get_job_header(drow["SGP_JOBNO"].ToString());
                    ServiceJobDet1 = bsObj.CHNLSVC.CustService.sp_get_job_details(drow["SGP_JOBNO"].ToString(), "GATEPASS");

                    ServiceJobHdr.Merge(ServiceJobHdr1);
                    ServiceJobDet.Merge(ServiceJobDet1);
                }
            }

            if (ServiceJobGatepass.Rows.Count > 0)
            {
                foreach (DataRow drow in ServiceJobGatepass.Rows)
                {
                    DataTable ServiceJobOldpart1 = new DataTable();
                    ServiceJobOldpart1 = bsObj.CHNLSVC.CustService.sp_get_gpOldpart_details(drow["SGP_JOBNO"].ToString(), Convert.ToInt16(drow["SGP_JOBLINE"].ToString()));
                    ServiceJobOldpart.Merge(ServiceJobOldpart1);
                }
            }

            _JobGatepass.Database.Tables["SCV_GATEPASS_HDR"].SetDataSource(ServiceJobGatepass);
            _JobGatepass.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
            _JobGatepass.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
            _JobGatepass.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _JobGatepass.Database.Tables["MST_COM"].SetDataSource(MST_COM);

            foreach (object repOp in _JobGatepass.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "OLDPARTS")
                    {
                        ReportDocument subRepDoc = _JobGatepass.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SCV_OLDPART"].SetDataSource(ServiceJobOldpart);
                    }

                }
            }

        }
        //Tharanga 2017/06/10
        public void ServiceGatepassnew()
        {
                     
            DataTable ServiceJobGatepass = bsObj.CHNLSVC.CustService.sp_get_gatepass_details(BaseCls.GlbReportDoc);
            DataTable ServiceJobGatepass_1 = ServiceJobGatepass.DefaultView.ToTable(true, "SGP_COM", "SGP_LOC", "SGP_JOBNO");
            DataTable ServiceJobHdr = new DataTable();
            DataTable ServiceJobDet = new DataTable();
            DataTable ServiceJobOldpart = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_COM = new DataTable();
            int i = 0;

            if (ServiceJobGatepass_1.Rows.Count > 0)
            {
                foreach (DataRow drow in ServiceJobGatepass_1.Rows)
                {
                    i = i + 1;
                    if (i == 1)
                    {
                        MST_LOC = bsObj.CHNLSVC.CustService.sp_get_loc_details(drow["SGP_LOC"].ToString());
                        MST_COM = bsObj.CHNLSVC.CustService.sp_get_com_details(drow["SGP_COM"].ToString());
                    }
                    DataTable ServiceJobHdr1 = new DataTable();
                    DataTable ServiceJobDet1 = new DataTable();

                    ServiceJobHdr1 = bsObj.CHNLSVC.CustService.sp_get_job_header(drow["SGP_JOBNO"].ToString());
                    ServiceJobDet1 = bsObj.CHNLSVC.CustService.sp_get_job_details(drow["SGP_JOBNO"].ToString(), "GATEPASS");

                    ServiceJobHdr.Merge(ServiceJobHdr1);
                    ServiceJobDet.Merge(ServiceJobDet1);
                }
            }

            if (ServiceJobGatepass.Rows.Count > 0)
            {
                foreach (DataRow drow in ServiceJobGatepass.Rows)
                {
                    DataTable ServiceJobOldpart1 = new DataTable();
                    ServiceJobOldpart1 = bsObj.CHNLSVC.CustService.sp_get_gpOldpart_details(drow["SGP_JOBNO"].ToString(), Convert.ToInt16(drow["SGP_JOBLINE"].ToString()));
                    ServiceJobOldpart.Merge(ServiceJobOldpart1);
                }
            }

            _JobGatepassnew.Database.Tables["SCV_GATEPASS_HDR"].SetDataSource(ServiceJobGatepass);
            _JobGatepassnew.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
            _JobGatepassnew.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
            _JobGatepassnew.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _JobGatepassnew.Database.Tables["MST_COM"].SetDataSource(MST_COM);

            foreach (object repOp in _JobGatepass.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "OLDPARTS")
                    {
                        ReportDocument subRepDoc = _JobGatepassnew.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SCV_OLDPART"].SetDataSource(ServiceJobOldpart);
                    }

                }
            }

        }
        //kapila
        public void ServiceFieldVisitJobSheet()
        {

            DataTable MST_ITM = new DataTable();

            DataTable MST_ITM1 = new DataTable();

            DataTable param = new DataTable();

            DataRow dr;

            param.Columns.Add("visitLine", typeof(Int32));

            dr = param.NewRow();
            dr["visitLine"] = BaseCls.GlbReportParaLine2;
            param.Rows.Add(dr);

            DataTable ServiceJobHdr = bsObj.CHNLSVC.CustService.sp_get_job_hdrby_jobno(BaseCls.GlbReportDoc);
            DataTable ServiceJobDet = bsObj.CHNLSVC.CustService.getServicejobDet(BaseCls.GlbReportDoc, BaseCls.GlbReportParaLine1);
            DataTable ServiceJobDef = bsObj.CHNLSVC.CustService.getServicejobDef(BaseCls.GlbReportDoc, BaseCls.GlbReportParaLine1);
            DataTable ServiceJobTempIssu = bsObj.CHNLSVC.CustService.getServiceTempIssuItems(BaseCls.GlbReportDoc, BaseCls.GlbReportParaLine1, BaseCls.GlbReportParaLine2);
            DataTable MST_LOC = bsObj.CHNLSVC.Sales.GetLocationCode(BaseCls.GlbReportCompCode, string.Empty);
            DataTable MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);


            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            MST_ITM.Columns.Add("MI_BRAND", typeof(string));

            //foreach (DataRow row in ServiceJobDet.Rows)
            //{

            //    MST_ITM1 = bsObj.CHNLSVC.Sales.GetItemCode(BaseCls.GlbReportCompCode, row["JBD_ITM_CD"].ToString());

            //    foreach (DataRow row1 in MST_ITM1.Rows)
            //    {
            //        dr = MST_ITM.NewRow();
            //        dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
            //        dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
            //        dr["MI_CD"] = row1["MI_CD"].ToString();
            //        dr["MI_BRAND"] = row1["MI_BRAND"].ToString();
            //        MST_ITM.Rows.Add(dr);
            //    }
            //}

            _Jobsheetfieldvisit.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
            _Jobsheetfieldvisit.Database.Tables["scv_job_det"].SetDataSource(ServiceJobDet);
            _Jobsheetfieldvisit.Database.Tables["scv_job_def"].SetDataSource(ServiceJobDef);
            //   _Jobsheetfieldvisit.Database.Tables["SCV_TEMP_ISSUE"].SetDataSource(ServiceJobTempIssu);
            _Jobsheetfieldvisit.Database.Tables["param"].SetDataSource(param);


            _Jobsheetfieldvisit.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _Jobsheetfieldvisit.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            //  _Jobsheetfieldvisit.Database.Tables["MST_ITEM"].SetDataSource(MST_ITM);

            foreach (object repOp in _Jobsheetfieldvisit.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "Temp_issue_items")
                    {
                        ReportDocument subRepDoc = _Jobsheetfieldvisit.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SCV_TEMP_ISSUE"].SetDataSource(ServiceJobTempIssu);
                    }

                }
            }


        }

        public void RCC_Report()
        {
            DataTable RCC_Report = new DataTable();
            DataTable RCC_JobReport = new DataTable();
            DataTable param = new DataTable();
            DataTable GLOB_DataTable = null;
            DataTable GLOB_DataTable1 = null;

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

            RCC_Report.Clear();
            DataTable tmp_user_pc = new DataTable();

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    GLOB_DataTable = bsObj.CHNLSVC.Financial.Print_RCC_Listing_Report(BaseCls.GlbReportCompCode,drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbRccType, BaseCls.GlbRccAgent, BaseCls.GlbRccColMethod, BaseCls.GlbRccCloseTp, BaseCls.GlbStatus, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode);
                    RCC_Report.Merge (GLOB_DataTable);

                    if (BaseCls.GlbReportName != "RCCReport.rpt")
                    {
                        GLOB_DataTable1 = bsObj.CHNLSVC.Financial.Print_RCC_Job_Report(BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID);
                        RCC_JobReport.Merge(GLOB_DataTable1);
                    }
                }
            }

            if (BaseCls.GlbReportName == "RCCReport.rpt")
            {
                _rccReport.Database.Tables["INT_RCC"].SetDataSource(RCC_Report);
                _rccReport.Database.Tables["param"].SetDataSource(param);
            }
            else
            {
                _rcc_Report.Database.Tables["INT_RCC"].SetDataSource(RCC_Report);
                _rcc_Report.Database.Tables["RCC_JOB_DTL"].SetDataSource(RCC_JobReport);
                _rcc_Report.Database.Tables["param"].SetDataSource(param);
            }

        }

        public void JobSummary_Report()
        {
            DataTable param = new DataTable();
            DataRow dr;

            DataTable tmp_user_pc = new DataTable();

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable JOBSUM = bsObj.CHNLSVC.CustService.JobSummaryDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportTechncian, BaseCls.GlbReportJobCat, BaseCls.GlbReportItemType, 0, 0, BaseCls.GlbReportJobNo, BaseCls.GlbUserID);
                    GLOB_DataTable.Merge(JOBSUM);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("technician", typeof(string));
            param.Columns.Add("jobcategory", typeof(string));
            param.Columns.Add("itemtype", typeof(string));
            param.Columns.Add("jobstatus", typeof(string));
            param.Columns.Add("warrantystatus", typeof(string));
            param.Columns.Add("jobno", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["technician"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobcategory"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["itemtype"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobstatus"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["warrantystatus"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobno"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;

            param.Rows.Add(dr);

            _Jobsum.Database.Tables["JOB_SUMMARY"].SetDataSource(GLOB_DataTable);
            _Jobsum.Database.Tables["param"].SetDataSource(param);
        }

        public void ProcessTracking_Report()
        {
            DataTable param = new DataTable();
            DataRow dr;

            DataTable tmp_user_pc = new DataTable();

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable JOBSUM = bsObj.CHNLSVC.CustService.JobProcessTrackingDetails1(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportTechncian, BaseCls.GlbReportJobCat, BaseCls.GlbReportItemType, BaseCls.GlbReportDiscRate, BaseCls.GlbReportWarrStatus, BaseCls.GlbReportJobNo, BaseCls.GlbUserID, BaseCls.GlbReportJobStatus, drow["tpl_pc"].ToString(), BaseCls.GlbReportIsExport, BaseCls.GlbReportDoc2);
                     GLOB_DataTable.Merge(JOBSUM);                    
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("technician", typeof(string));
            param.Columns.Add("jobcategory", typeof(string));
            param.Columns.Add("itemtype", typeof(string));
            param.Columns.Add("jobstatus", typeof(string));
            param.Columns.Add("warrantystatus", typeof(string));
            param.Columns.Add("jobno", typeof(string));
            param.Columns.Add("jobtp", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["technician"] =  BaseCls.GlbReportTechncian == "" ? "ALL" : BaseCls.GlbReportTechncian;
            dr["jobcategory"] = BaseCls.GlbReportJobCat == "" ? "ALL" : BaseCls.GlbReportJobCat;
            dr["itemtype"] = BaseCls.GlbReportItemType == "" ? "ALL" : BaseCls.GlbReportItemType;
            dr["jobstatus"] = BaseCls.GlbReportJobStatus == "Y" ? "LEVEL " + BaseCls.GlbReportDiscRate.ToString() +   " ACHIEVED" : "LEVEL " + BaseCls.GlbReportDiscRate.ToString() +  " NOT ACHIEVED";
            dr["warrantystatus"] = BaseCls.GlbReportWarrStatus=="0" ? "ALL":BaseCls.GlbReportWarrStatus=="1" ? "UNDER WARRANTY" : "OVER WARRANTY";
            dr["jobno"] = BaseCls.GlbReportJobNo == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobtp"] = BaseCls.GlbReportJobStatus == "" ? "ALL" : BaseCls.GlbReportJobStatus;
            param.Rows.Add(dr);

            _ProcTrack.Database.Tables["JOB_SUMMARY"].SetDataSource(GLOB_DataTable);
            _ProcTrack.Database.Tables["param"].SetDataSource(param);
        }
 
        public void Smart_Insu_Claim_Report()
        {
            DataTable param = new DataTable();
            DataRow dr;

            DataTable tmp_user_pc = new DataTable();

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable JOBSUM = bsObj.CHNLSVC.CustService.SmartInsuClaimDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3);
                     GLOB_DataTable.Merge(JOBSUM);                    
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("period", typeof(string));
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
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;            
            param.Rows.Add(dr);

            _smrt_Warr.Database.Tables["SMART_WARR_DET"].SetDataSource(GLOB_DataTable);
            _smrt_Warr.Database.Tables["param"].SetDataSource(param);
        }

        public void RCCPrintReport()
        {// Sanjeewa 27-02-2013
            DataTable param = new DataTable();
            DataRow dr;

            //DataTable _RCC = bsObj.CHNLSVC.Financial.Print_RCC_Receipt(BaseCls.GlbReportDoc);
            //DataTable _retCond = bsObj.CHNLSVC.Financial.Print_RCC_Ret_Condition(BaseCls.GlbReportDoc);

            //_rccPrint.Database.Tables["RCC"].SetDataSource(_RCC);
            //_rccPrint.Database.Tables["Ret_Cond"].SetDataSource(_retCond);
            //_rccPrint.Database.Tables["param"].SetDataSource(param);

        }
        public void ServiceJobCardAuto()
        {

            DataTable MST_ITM = new DataTable();

            DataTable MST_ITM1 = new DataTable();


            DataTable ServiceJobCard = bsObj.CHNLSVC.Inventory.ServicejobCard(BaseCls.GlbReportDoc);
            DataTable ServiceJobDefects = bsObj.CHNLSVC.Inventory.ServicejobCardDefect(BaseCls.GlbReportDoc);
            DataTable ServiceJobHistory = bsObj.CHNLSVC.Inventory.ServicejobCardHistory(BaseCls.GlbReportDoc);
            DataTable MST_LOC = bsObj.CHNLSVC.Sales.GetLocationCode(BaseCls.GlbReportCompCode, BaseCls.GlbUserDefLoca);
            DataTable MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);


            DataRow dr;
            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            MST_ITM.Columns.Add("MI_BRAND", typeof(string));

            foreach (DataRow row in ServiceJobCard.Rows)
            {
                MST_ITM1 = bsObj.CHNLSVC.Sales.GetItemCode(BaseCls.GlbReportCompCode, row["JBD_ITM_CD"].ToString());

                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    dr["MI_BRAND"] = row1["MI_BRAND"].ToString();
                    MST_ITM.Rows.Add(dr);
                }
            }

            _serJobAuto.Database.Tables["ServiceJobCard"].SetDataSource(ServiceJobCard);
            _serJobAuto.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _serJobAuto.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _serJobAuto.Database.Tables["MST_ITEM"].SetDataSource(MST_ITM);

            foreach (object repOp in _serJobAuto.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "jobDefects")
                    {
                        ReportDocument subRepDoc = _serJobAuto.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["ServiceJobDefects"].SetDataSource(ServiceJobDefects);
                    }
                    if (_cs.SubreportName == "jobhistory")
                    {
                        ReportDocument subRepDoc = _serJobAuto.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["ServiceJobHistory"].SetDataSource(ServiceJobHistory);
                    }
                }
            }


        }


        //Tharanga 2017/06/13
        public void ServiceFeildJobCard()
        {

            DataTable MST_ITM = new DataTable();

            DataTable MST_ITM1 = new DataTable();


            DataTable ServiceJobCard = bsObj.CHNLSVC.Inventory.ServicejobCard(BaseCls.GlbReportDoc);
            DataTable ServiceJobDefects = bsObj.CHNLSVC.Inventory.ServicejobCardDefect(BaseCls.GlbReportDoc);
            DataTable ServiceJobHistory = bsObj.CHNLSVC.Inventory.ServicejobCardHistory(BaseCls.GlbReportDoc);
            DataTable MST_LOC = bsObj.CHNLSVC.Sales.GetLocationCode(BaseCls.GlbReportCompCode, BaseCls.GlbUserDefLoca);
            DataTable MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);


            DataRow dr;
            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            MST_ITM.Columns.Add("MI_BRAND", typeof(string));

            foreach (DataRow row in ServiceJobCard.Rows)
            {
                MST_ITM1 = bsObj.CHNLSVC.Sales.GetItemCode(BaseCls.GlbReportCompCode, row["JBD_ITM_CD"].ToString());

                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    dr["MI_BRAND"] = row1["MI_BRAND"].ToString();
                    MST_ITM.Rows.Add(dr);
                }
            }

            _FeildJobCard.Database.Tables["ServiceJobCard"].SetDataSource(ServiceJobCard);
            _FeildJobCard.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _FeildJobCard.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _FeildJobCard.Database.Tables["MST_ITEM"].SetDataSource(MST_ITM);

            foreach (object repOp in _FeildJobCard.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "jobDefects")
                    {
                        ReportDocument subRepDoc = _FeildJobCard.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["ServiceJobDefects"].SetDataSource(ServiceJobDefects);
                    }
                    if (_cs.SubreportName == "jobhistory")
                    {
                        ReportDocument subRepDoc = _FeildJobCard.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["ServiceJobHistory"].SetDataSource(ServiceJobHistory);
                    }
                }
            }


        }
        public void ServiceJobCard()
        {

            DataTable MST_ITM = new DataTable();

            DataTable MST_ITM1 = new DataTable();


            DataTable ServiceJobCard = bsObj.CHNLSVC.Inventory.ServicejobCard(BaseCls.GlbReportDoc);
            DataTable ServiceJobDefects = bsObj.CHNLSVC.Inventory.ServicejobCardDefect(BaseCls.GlbReportDoc);
            DataTable ServiceJobHistory = bsObj.CHNLSVC.Inventory.ServicejobCardHistory(BaseCls.GlbReportDoc);
            DataTable MST_LOC = bsObj.CHNLSVC.Sales.GetLocationCode(BaseCls.GlbReportCompCode, string.Empty);
            DataTable MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);


            DataRow dr;
            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            MST_ITM.Columns.Add("MI_BRAND", typeof(string));

            foreach (DataRow row in ServiceJobCard.Rows)
            {
                MST_ITM1 = bsObj.CHNLSVC.Sales.GetItemCode(BaseCls.GlbReportCompCode, row["JBD_ITM_CD"].ToString());

                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    dr["MI_BRAND"] = row1["MI_BRAND"].ToString();
                    MST_ITM.Rows.Add(dr);
                }
            }

            _serJob.Database.Tables["ServiceJobCard"].SetDataSource(ServiceJobCard);
            _serJob.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _serJob.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _serJob.Database.Tables["MST_ITEM"].SetDataSource(MST_ITM);

            foreach (object repOp in _serJob.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "jobDefects")
                    {
                        ReportDocument subRepDoc = _serJob.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["ServiceJobDefects"].SetDataSource(ServiceJobDefects);
                    }
                    if (_cs.SubreportName == "jobhistory")
                    {
                        ReportDocument subRepDoc = _serJob.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["ServiceJobHistory"].SetDataSource(ServiceJobHistory);
                    }
                }
            }


        }
        public void ServiceJobSummaryReport()
        {// Sanjeewa 27-02-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable JOBSVC = bsObj.CHNLSVC.MsgPortal.GetJobDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, BaseCls.GlbReportChannel, BaseCls.GlbReportStatus);
            //DataTable JOBSVCSCHED = bsObj.CHNLSVC.Sales.GetJobScheduleDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("Channel", typeof(string));
            param.Columns.Add("JobStatus", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["Channel"] = BaseCls.GlbReportChannel == "" ? "ALL" : BaseCls.GlbReportChannel;
            dr["JobStatus"] = BaseCls.GlbReportStatus == 0 ? "Pending" : BaseCls.GlbReportStatus == 1 ? "Started" : BaseCls.GlbReportStatus == 2 ? "Completed" : BaseCls.GlbReportStatus == 3 ? "Hold" : BaseCls.GlbReportStatus == 4 ? "ReAllocated" : "Pending";
            param.Rows.Add(dr);

            _SvcJob.Database.Tables["SVC_JOBS"].SetDataSource(JOBSVC);
            _SvcJob.Database.Tables["param"].SetDataSource(param);

            //foreach (object repOp in _SvcJob.ReportDefinition.ReportObjects)
            //{
            //    string _s = repOp.GetType().ToString();
            //    if (_s.ToLower().Contains("subreport"))
            //    {
            //        SubreportObject _cs = (SubreportObject)repOp;
            //        if (_cs.SubreportName == "ServiceSchedule")
            //        {
            //            ReportDocument subRepDoc = _SvcJob.Subreports[_cs.SubreportName];
            //            subRepDoc.Database.Tables["SVC_JOB_SCHEDULE"].SetDataSource(JOBSVCSCHED);
            //        }
            //    }
            //}

        }

        public void ServiceJobReport()
        {// Sanjeewa 15-08-2013
            DataTable param = new DataTable();
            DataTable glob_svc = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_svc = new DataTable();

                    tmp_svc = bsObj.CHNLSVC.MsgPortal.GetJobSummaryDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID, drow["tpl_pc"].ToString(), BaseCls.GlbUserComCode);
                    glob_svc.Merge(tmp_svc);

                }
            }
            //DataTable JOBSVC = bsObj.CHNLSVC.Sales.GetJobSummaryDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserID);            

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

            _SvcJobSumm.Database.Tables["SVC_JOB_CHG"].SetDataSource(glob_svc);
            _SvcJobSumm.Database.Tables["param"].SetDataSource(param);

        }

        public void ServiceJobGP()
        {// Nadeeka 27-04-2015
            DataTable param = new DataTable();
            DataTable GLB_SERVICE_GP = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_svc = new DataTable();
                    tmp_svc = bsObj.CHNLSVC.MsgPortal.GetServiceGP(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate));
                    GLB_SERVICE_GP.Merge(tmp_svc);

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

            _sergp.Database.Tables["GLB_SERVICE_GP"].SetDataSource(GLB_SERVICE_GP);
            _sergp.Database.Tables["param"].SetDataSource(param);

        }
        public void ServiceAgreement()
        {// Nadeeka 07-11-2015
            DataTable param = new DataTable();
            DataTable GLB_AGR_SERVICE = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataRow dr;

            //tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            //if (tmp_user_pc.Rows.Count > 0)
            //{
            //    foreach (DataRow drow in tmp_user_pc.Rows)
            //    {
                    DataTable tmp_svc = new DataTable();
                    tmp_svc = bsObj.CHNLSVC.CustService.get_ServiceAgreement(BaseCls.GlbUserComCode,null, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate));
                    GLB_AGR_SERVICE.Merge(tmp_svc);

            //    }
            //}

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

            _serAgree.Database.Tables["GLB_AGR_SERVICE"].SetDataSource(GLB_AGR_SERVICE);
            _serAgree.Database.Tables["param"].SetDataSource(param);

        }

        public void IncentiveDetailReport()
        {// Nadeeka 07-11-2015
            DataTable param = new DataTable();
            DataTable GLB_INC_REP = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataRow dr;

            GLB_INC_REP = bsObj.CHNLSVC.CustService.get_ServiceIncentive(BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbReportDoc);

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

            _incdet.Database.Tables["INCENTIVE_DET"].SetDataSource(GLB_INC_REP);
            _incdet.Database.Tables["param"].SetDataSource(param);

        }

        public void ServiceJobGPDetail()
        {// Nadeeka 27-04-2015 , Sanjeewa 2015-09-21
            DataTable param = new DataTable();
            DataTable GLB_SERVICE_GP = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_svc = new DataTable();
                    tmp_svc = bsObj.CHNLSVC.MsgPortal.GetServiceGPDetail(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbReportDocType);
                    GLB_SERVICE_GP.Merge(tmp_svc);
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

            _sergpdtl.Database.Tables["GLB_SERVICE_GP"].SetDataSource(GLB_SERVICE_GP);
            _sergpdtl.Database.Tables["param"].SetDataSource(param);

        }
        public void ServiceSupplierClaim()
        {// Nadeeka 27-04-2015
            DataTable param = new DataTable();
            DataTable GLB_SUPCLAIM_DETAILS = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_svc = new DataTable();
                    tmp_svc = bsObj.CHNLSVC.CustService.GetServiceSupplierWarranty(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportSupplier, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportModel, BaseCls.GlbReportBrand, BaseCls.GlbReportItemCode, Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbReportDocType);

                    GLB_SUPCLAIM_DETAILS.Merge(tmp_svc);

                }
            }


            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("docType", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("Direct", typeof(string));
            param.Columns.Add("Model", typeof(string));
            param.Columns.Add("Brand", typeof(string));
            param.Columns.Add("itemCode", typeof(string));


            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
             dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["docType"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["Direct"] = BaseCls.GlbReportSupplier == "" ? "ALL" : BaseCls.GlbReportSupplier;
            dr["Model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["Brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["itemCode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            param.Rows.Add(dr);

            if (BaseCls.GlbReportParaLine1 == 0)
            {
                _suppWar.Database.Tables["GLB_SUPCLAIM_DETAILS"].SetDataSource(GLB_SUPCLAIM_DETAILS);
                _suppWar.Database.Tables["param"].SetDataSource(param);
            }
            else
            {
                _suppWarEx.Database.Tables["GLB_SUPCLAIM_DETAILS"].SetDataSource(GLB_SUPCLAIM_DETAILS);
                _suppWarEx.Database.Tables["param"].SetDataSource(param);
            }

        }
        public void ServiceStandBy()
        {// Nadeeka 27-04-2015
            DataTable param = new DataTable();
            DataTable GLB_SER_STANDBY = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_svc = new DataTable();
                    tmp_svc = bsObj.CHNLSVC.MsgPortal.GetServiceStandBy(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate));
                    GLB_SER_STANDBY.Merge(tmp_svc);

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

            _stdby.Database.Tables["GLB_SER_STANDBY"].SetDataSource(GLB_SER_STANDBY);
            _stdby.Database.Tables["param"].SetDataSource(param);

        }


        public void InvociePrintServicePhone()
        {// Sanjeewa 2015-06-12
            string invNo = default(string);
            string accNo = default(string);
            Boolean ishp = default(Boolean);
            ishp = false;
            invNo = BaseCls.GlbReportDoc;
            DataTable mst_tax_master = new DataTable();
            DataRow drr;
            DataTable salesDetails = new DataTable();
            //DataTable sat_vou_det = new DataTable();
            DataTable param = new DataTable();

            param.Clear();
            param.Columns.Add("isCnote", typeof(Int16));

            Int16 isCredit = 0;

            salesDetails.Clear();

            //salesDetails = bsObj.CHNLSVC.Sales.GetSalesDetails(invNo, null);
            salesDetails = bsObj.CHNLSVC.Inventory.GetSalesDetailsMobSer(invNo);
            //sat_vou_det = bsObj.CHNLSVC.Sales.getSalesGiftVouchaer(invNo);
            mst_tax_master = bsObj.CHNLSVC.Sales.GetinvTax(invNo);

            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable int_batch = new DataTable();
            DataTable int_batch1 = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable sar_sub_tp = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            //DataTable tblComDate = new DataTable();

            int _numPages = 0;
            DataRow dr3;

            _numPages = bsObj.CHNLSVC.General.GetReprintDocCount(invNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr3);
            _JobInvoicePh.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();

            //DataTable accountDetails = bsObj.CHNLSVC.Sales.GetAccountDetails(invNo);
            //DataTable hpt_acc = new DataTable();
            DataRow dr;
            DataRow dr1;

            //hpt_acc.Columns.Add("HPA_ACC_NO", typeof(string));
            //hpt_acc.Columns.Add("HPA_ACC_CRE_DT", typeof(DateTime));
            //hpt_acc.Columns.Add("HPA_SCH_CD", typeof(string));
            //hpt_acc.Columns.Add("HPA_TERM", typeof(decimal));
            //hpt_acc.Columns.Add("HPA_DP_VAL", typeof(decimal));
            //hpt_acc.Columns.Add("HPA_AF_VAL", typeof(decimal));
            //hpt_acc.Columns.Add("HPA_INIT_INS", typeof(decimal));
            //hpt_acc.Columns.Add("HPA_INIT_VAT", typeof(decimal));
            //hpt_acc.Columns.Add("HPA_INIT_STM", typeof(decimal));
            //hpt_acc.Columns.Add("HPA_INIT_SER_CHG", typeof(decimal));

            //foreach (DataRow row in accountDetails.Rows)
            //{
            //    dr = hpt_acc.NewRow();
            //    accNo = row["HPA_ACC_NO"].ToString();
            //    dr["HPA_ACC_NO"] = row["HPA_ACC_NO"].ToString();
            //    dr["HPA_ACC_CRE_DT"] = row["HPA_ACC_CRE_DT"].ToString();
            //    dr["HPA_SCH_CD"] = row["HPA_SCH_CD"].ToString();
            //    dr["HPA_TERM"] = Convert.ToDecimal(row["HPA_TERM"].ToString());
            //    dr["HPA_DP_VAL"] = Convert.ToDecimal(row["HPA_DP_VAL"].ToString());
            //    dr["HPA_AF_VAL"] = Convert.ToDecimal(row["HPA_AF_VAL"].ToString());
            //    dr["HPA_INIT_INS"] = Convert.ToDecimal(row["HPA_INIT_INS"].ToString());
            //    dr["HPA_INIT_VAT"] = Convert.ToDecimal(row["HPA_INIT_VAT"].ToString());
            //    dr["HPA_INIT_STM"] = Convert.ToDecimal(row["HPA_INIT_STM"].ToString());
            //    dr["HPA_INIT_SER_CHG"] = Convert.ToDecimal(row["HPA_INIT_SER_CHG"].ToString());
            //    hpt_acc.Rows.Add(dr);
            //}

            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_SVAT_NO", typeof(string));

            sat_hdr1.Columns.Add("SAH_DT", typeof(DateTime));
            sat_hdr1.Columns.Add("SAH_INV_NO", typeof(string));
            sat_hdr1.Columns.Add("SAH_INV_TP", typeof(string));
            sat_hdr1.Columns.Add("SAH_IS_SVAT", typeof(Int16));
            sat_hdr1.Columns.Add("SAH_MAN_REF", typeof(string));
            sat_hdr1.Columns.Add("SAH_PC", typeof(string));
            sat_hdr1.Columns.Add("SAH_REMARKS", typeof(string));
            sat_hdr1.Columns.Add("SAH_SALES_EX_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_TAX_EXEMPTED", typeof(Int16));
            sat_hdr1.Columns.Add("SAH_TAX_INV", typeof(Int16));
            sat_hdr1.Columns.Add("SAH_ACC_NO", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_NAME", typeof(string)); //Add by Chamal 23/05/2013
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_NAME", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_SEQ_NO", typeof(decimal));
            sat_hdr1.Columns.Add("SAH_COM", typeof(string));
            sat_hdr1.Columns.Add("SAH_CRE_BY", typeof(string));
            sat_hdr1.Columns.Add("SAH_INV_SUB_TP", typeof(string));
            sat_hdr1.Columns.Add("SAH_REF_DOC", typeof(string));
            sat_hdr1.Columns.Add("esep_name_initials", typeof(string));
            sat_hdr1.Columns.Add("SAH_ANAL_11", typeof(Int16));
            sat_hdr1.Columns.Add("sah_del_loc", typeof(string));

            sat_itm.Columns.Add("SAD_WARR_REMARKS", typeof(string));
            sat_itm.Columns.Add("SAD_UNIT_RT", typeof(decimal));
            sat_itm.Columns.Add("SAD_TOT_AMT", typeof(decimal));
            sat_itm.Columns.Add("SAD_QTY", typeof(decimal));
            sat_itm.Columns.Add("SAD_ITM_TAX_AMT", typeof(decimal));
            sat_itm.Columns.Add("SAD_ITM_CD", typeof(string));
            sat_itm.Columns.Add("SAD_DISC_RT", typeof(decimal));
            sat_itm.Columns.Add("SAD_DISC_AMT", typeof(decimal));
            sat_itm.Columns.Add("SAD_SEQ_NO", typeof(string));
            sat_itm.Columns.Add("SAD_ITM_LINE", typeof(Int16));
            sat_itm.Columns.Add("SAD_WARR_PERIOD", typeof(Int16));
            sat_itm.Columns.Add("SAD_JOB_NO", typeof(string));
            sat_itm.Columns.Add("SAD_JOB_LINE", typeof(Int16));
            sat_itm.Columns.Add("VAT_AMT", typeof(decimal));
            sat_itm.Columns.Add("NBT_AMT", typeof(decimal));
            sat_itm.Columns.Add("OTH_TAX_AMT", typeof(decimal));

            mst_profit_center.Columns.Add("MPC_ADD_1", typeof(string));
            mst_profit_center.Columns.Add("MPC_ADD_2", typeof(string));
            mst_profit_center.Columns.Add("MPC_CHNL", typeof(string));
            mst_profit_center.Columns.Add("MPC_DESC", typeof(string));
            mst_profit_center.Columns.Add("MPC_COM", typeof(string));
            mst_profit_center.Columns.Add("MPC_CD", typeof(string));
            mst_profit_center.Columns.Add("MPC_TEL", typeof(string));
            mst_profit_center.Columns.Add("MPC_FAX", typeof(string));
            mst_profit_center.Columns.Add("MPC_EMAIL", typeof(string));
            mst_profit_center.Columns.Add("MPC_OTH_REF", typeof(string));

            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));
            mst_item.Columns.Add("MI_IS_SER1", typeof(Int16));
            mst_item.Columns.Add("MI_IS_SER2", typeof(Int16));
            mst_item.Columns.Add("MI_WARR", typeof(Int16));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_IS_SER1", typeof(Int16));
            mst_item1.Columns.Add("MI_IS_SER2", typeof(Int16));
            mst_item1.Columns.Add("MI_WARR", typeof(Int16));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));
            mst_com.Columns.Add("MC_TAX3", typeof(string));
            mst_com.Columns.Add("MC_ANAL18", typeof(string));
            mst_com.Columns.Add("MC_ANAL19", typeof(string));

            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));

            int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));
            int_batch.Columns.Add("ITB_BATCH_LINE", typeof(Int16));

            foreach (DataRow row in salesDetails.Rows)
            {
                dr = mst_busentity.NewRow();
                int index = salesDetails.Rows.IndexOf(row);

                int_batch1 = bsObj.CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));
                foreach (DataRow row1 in int_batch1.Rows)
                {
                    dr1 = int_batch.NewRow();
                    dr1["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                    dr1["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                    dr1["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                    dr1["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                    dr1["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                    dr1["ITB_BATCH_LINE"] = Convert.ToInt16(row1["ITB_BATCH_LINE"].ToString());
                    int_batch.Rows.Add(dr1);

                }
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_SVAT_NO"] = row["MBE_SVAT_NO"].ToString();

                    mst_busentity.Rows.Add(dr);

                    dr = sat_hdr1.NewRow();
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
                    dr["SAH_D_CUST_NAME"] = row["SAH_D_CUST_NAME"].ToString();//Add by Chamal 23/05/2013
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    dr["SAH_INV_SUB_TP"] = row["SAH_INV_SUB_TP"].ToString();
                    dr["SAH_REF_DOC"] = row["SAH_REF_DOC"].ToString();
                    dr["esep_name_initials"] = row["esep_name_initials"].ToString();
                    if (!string.IsNullOrEmpty(row["SAH_ANAL_11"].ToString()))// Nadeeka 02-03-2015
                    {
                        dr["SAH_ANAL_11"] = Convert.ToInt16(row["SAH_ANAL_11"].ToString());
                    }
                    else
                    {
                        dr["SAH_ANAL_11"] = 0;
                    }
                    if (!string.IsNullOrEmpty(row["sah_del_loc"].ToString()))
                    {
                        dr["sah_del_loc"] = row["sah_del_loc"].ToString();
                        MST_LOC = bsObj.CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["sah_del_loc"].ToString());
                    }
                    else
                    {
                        dr["sah_del_loc"] = string.Empty;
                    }
                    sar_sub_tp = bsObj.CHNLSVC.Sales.GetSalesSubTypeTable(row["SAH_INV_TP"].ToString(), row["SAH_INV_SUB_TP"].ToString());

                    sat_hdr1.Rows.Add(dr);
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
                dr["SAD_ITM_LINE"] = Convert.ToInt16(row["SAD_ITM_LINE"].ToString());
                dr["SAD_WARR_PERIOD"] = Convert.ToInt16(row["SAD_WARR_PERIOD"].ToString());
                dr["SAD_JOB_NO"] = row["SAD_JOB_NO"].ToString();
                dr["SAD_JOB_LINE"] = Convert.ToInt16(row["SAD_JOB_LINE"].ToString());
                dr["VAT_AMT"] = Convert.ToDecimal(row["VAT_AMT"].ToString());
                dr["NBT_AMT"] = Convert.ToDecimal(row["NBT_AMT"].ToString());
                dr["OTH_TAX_AMT"] = Convert.ToDecimal(row["OTH_TAX_AMT"].ToString());                
                sat_itm.Rows.Add(dr);

                dr = mst_item.NewRow();
                dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                dr["MI_CD"] = row["MI_CD"].ToString();

                mst_item.Rows.Add(dr);

                if (index == 0)
                {
                    //if (accountDetails.Rows.Count > 0)
                    //{
                    //    tblComDate = bsObj.CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), accNo);
                    //}
                    //else
                    //{
                    //    tblComDate = bsObj.CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), invNo);
                    //}

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
                    dr["MPC_OTH_REF"] = row["MPC_OTH_REF"].ToString();

                    mst_profit_center.Rows.Add(dr);

                    dr = mst_com.NewRow();
                    dr["MC_TAX1"] = row["MC_TAX1"].ToString();
                    dr["MC_TAX2"] = row["MC_TAX2"].ToString();
                    dr["MC_CD"] = row["MC_CD"].ToString();
                    dr["MC_DESC"] = row["MC_DESC"].ToString();
                    dr["MC_IT_POWERED"] = row["MC_IT_POWERED"].ToString();
                    dr["MC_TAX3"] = row["MC_TAX3"].ToString();
                    dr["MC_ANAL18"] = row["MC_ANAL18"].ToString();
                    dr["MC_ANAL19"] = row["MC_ANAL19"].ToString();
                    mst_com.Rows.Add(dr);

                    dr = sec_user.NewRow();
                    dr["SE_USR_NAME"] = row["SE_USR_NAME"].ToString();
                    dr["SE_USR_ID"] = row["SE_USR_ID"].ToString();
                    sec_user.Rows.Add(dr);
                };
            }

            DataTable deliveredSerials = bsObj.CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
            //DataTable int_hdr = new DataTable();
            DataTable int_ser = new DataTable();

            //int_hdr.Columns.Add("ITH_SEQ_NO", typeof(decimal));
            //int_hdr.Columns.Add("ITH_COM", typeof(string));
            //int_hdr.Columns.Add("ITH_OTH_DOCNO", typeof(string));
            //int_hdr.Columns.Add("ITH_DOC_NO", typeof(string));
            int_ser.Columns.Add("ITS_SEQ_NO", typeof(string));
            int_ser.Columns.Add("ITS_ITM_LINE", typeof(Int16));
            int_ser.Columns.Add("ITS_BATCH_LINE", typeof(Int16));
            int_ser.Columns.Add("ITS_SER_LINE", typeof(Int16));
            int_ser.Columns.Add("ITS_DOC_NO", typeof(string));
            int_ser.Columns.Add("ITS_SER_ID", typeof(string));
            int_ser.Columns.Add("ITS_SER_1", typeof(string));
            int_ser.Columns.Add("ITS_SER_2", typeof(string));
            int_ser.Columns.Add("ITS_WARR_NO", typeof(string));
            int_ser.Columns.Add("ITS_WARR_PERIOD", typeof(decimal));
            int_ser.Columns.Add("ITS_ITM_CD", typeof(string));

            foreach (DataRow row in deliveredSerials.Rows)
            {
                //    dr = int_hdr.NewRow();
                //    dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                //    dr["ITH_COM"] = row["ITH_COM"].ToString();
                //    dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                //    dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                //    int_hdr.Rows.Add(dr);

                if (row["ITS_SER_1"].ToString() != "N/A")
                {
                    dr = int_ser.NewRow();
                    dr["ITS_SEQ_NO"] = row["ITS_SEQ_NO"].ToString();
                    dr["ITS_ITM_LINE"] = Convert.ToInt16(row["ITS_ITM_LINE"].ToString());
                    dr["ITS_BATCH_LINE"] = Convert.ToInt16(row["ITS_BATCH_LINE"].ToString());
                    dr["ITS_SER_LINE"] = row["ITS_SER_LINE"].ToString();
                    dr["ITS_DOC_NO"] = row["ITS_DOC_NO"].ToString();
                    dr["ITS_SER_ID"] = row["ITS_SER_ID"].ToString();
                    dr["ITS_SER_1"] = row["ITS_SER_1"].ToString();
                    dr["ITS_SER_2"] = row["ITS_SER_2"].ToString();
                    dr["ITS_WARR_NO"] = row["ITS_WARR_NO"].ToString();
                    dr["ITS_WARR_PERIOD"] = Convert.ToDecimal(row["ITS_WARR_PERIOD"].ToString());
                    int_ser.Rows.Add(dr);
                };

                //    DataTable MST_ITM1 = new DataTable();
                //    MST_ITM1 = bsObj.CHNLSVC.Sales.GetItemCode(row["ITH_COM"].ToString(), row["ITS_ITM_CD"].ToString());
                //    foreach (DataRow row1 in MST_ITM1.Rows)
                //    {
                //        dr = mst_item1.NewRow();
                //        dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                //        dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                //        dr["MI_CD"] = row1["MI_CD"].ToString();
                //        dr["MI_IS_SER1"] = row1["MI_IS_SER1"].ToString();
                //        dr["MI_IS_SER2"] = row1["MI_IS_SER2"].ToString();
                //        dr["MI_WARR"] = row1["MI_WARR"].ToString();
                //        mst_item1.Rows.Add(dr);
                //    }
            }

            DataTable receiptDetails = bsObj.CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);

            DataTable sat_receiptCQ = new DataTable();
            DataTable sat_receiptitmCQ = new DataTable();
            sat_receiptCQ.Columns.Add("SAR_SEQ_NO", typeof(string));
            sat_receiptCQ.Columns.Add("SAR_COM_CD", typeof(string));
            sat_receiptCQ.Columns.Add("SAR_RECEIPT_TYPE", typeof(string));
            sat_receiptCQ.Columns.Add("SAR_RECEIPT_NO", typeof(string));

            sat_receiptitmCQ.Columns.Add("SARD_SEQ_NO", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_RECEIPT_NO", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_INV_NO", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_PAY_TP", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_REF_NO", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_SETTLE_AMT", typeof(decimal));
            sat_receiptitmCQ.Columns.Add("SAPT_DESC", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_RMK", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_ANAL_3", typeof(decimal));

            foreach (DataRow row in receiptDetails.Rows)
            {
                if (row["SAR_RECEIPT_TYPE"].ToString() == "DIR")//(row["SARD_PAY_TP"].ToString() == "CHEQUE" && row["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                {
                    dr = sat_receiptCQ.NewRow();
                    dr["SAR_SEQ_NO"] = row["SAR_SEQ_NO"].ToString();
                    dr["SAR_COM_CD"] = row["SAR_COM_CD"].ToString();
                    dr["SAR_RECEIPT_TYPE"] = row["SAR_RECEIPT_TYPE"].ToString();
                    dr["SAR_RECEIPT_NO"] = row["SAR_RECEIPT_NO"].ToString();
                    sat_receiptCQ.Rows.Add(dr);

                    dr = sat_receiptitmCQ.NewRow();
                    dr["SARD_SEQ_NO"] = row["SARD_SEQ_NO"].ToString();
                    dr["SARD_RECEIPT_NO"] = row["SARD_RECEIPT_NO"].ToString();
                    dr["SARD_INV_NO"] = row["SARD_INV_NO"].ToString();
                    dr["SARD_PAY_TP"] = row["SARD_PAY_TP"].ToString();
                    dr["SARD_REF_NO"] = row["SARD_REF_NO"].ToString();
                    dr["SAPT_DESC"] = row["SAPT_DESC"].ToString();
                    dr["SARD_SETTLE_AMT"] = Convert.ToDecimal(row["SARD_SETTLE_AMT"].ToString());
                    dr["SARD_RMK"] = row["SARD_RMK"].ToString();
                    dr["SARD_ANAL_3"] = Convert.ToDecimal(row["SARD_ANAL_3"].ToString());
                    sat_receiptitmCQ.Rows.Add(dr);


                    if (row["SARD_PAY_TP"].ToString() == "CRNOTE")
                    {
                        isCredit = 1;
                    }
                };
            }

            //DataTable hpt_shed = bsObj.CHNLSVC.Sales.GetAccountSchedule(invNo);
            //DataTable Promo = bsObj.CHNLSVC.Sales.GetPromotionByInvoice(invNo);

            DataTable ref_rep_infor = bsObj.CHNLSVC.Sales.GetReportInfor("InvoiceHalfPrints.rpt");
            mst_item = mst_item.DefaultView.ToTable(true);

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


            _JobInvoicePh.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _JobInvoicePh.Database.Tables["mst_com"].SetDataSource(mst_com);
            _JobInvoicePh.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            _JobInvoicePh.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            _JobInvoicePh.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            _JobInvoicePh.Database.Tables["mst_item"].SetDataSource(mst_item);
            _JobInvoicePh.Database.Tables["sec_user"].SetDataSource(sec_user);
            _JobInvoicePh.Database.Tables["sar_sub_tp"].SetDataSource(sar_sub_tp);
            _JobInvoicePh.Database.Tables["param"].SetDataSource(param);

            DataTable ServiceJobDef = new DataTable();
            DataTable ServiceJobSer = new DataTable();
            DataTable ServiceJobSerSub = new DataTable();

            if (sat_itm.Rows.Count > 0)
            {
                foreach (DataRow drow in sat_itm.Rows)
                {
                    DataTable ServiceJobDef1 = new DataTable();
                    ServiceJobDef1 = bsObj.CHNLSVC.CustService.sp_get_job_defects(drow["SAD_JOB_NO"].ToString());
                    ServiceJobDef.Merge(ServiceJobDef1);

                    //kapila 17/6/2015
                    DataTable ServiceJobSerials = new DataTable();
                    ServiceJobSerials = bsObj.CHNLSVC.CustService.getServicejobDet(drow["SAD_JOB_NO"].ToString(), Convert.ToInt32(drow["SAD_JOB_LINE"]));
                    ServiceJobSer.Merge(ServiceJobSerials);

                    DataTable ServiceJobSerialsSub = new DataTable();
                    ServiceJobSerialsSub = bsObj.CHNLSVC.CustService.GetServiceJobDetailSubItemsData(drow["SAD_JOB_NO"].ToString(), Convert.ToInt32(drow["SAD_JOB_LINE"]));
                    ServiceJobSerSub.Merge(ServiceJobSerialsSub);


                }

                ServiceJobDef = ServiceJobDef.DefaultView.ToTable(true);
                ServiceJobSer = ServiceJobSer.DefaultView.ToTable(true);
                ServiceJobSerSub = ServiceJobSerSub.DefaultView.ToTable(true);

            }

            //_JobInvoice.Database.Tables["Promo"].SetDataSource(Promo);

            foreach (object repOp in _JobInvoicePh.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    //if (_cs.SubreportName == "rptWarranty")
                    //{
                    //    ReportDocument subRepDoc = _JobInvoice.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                    //}

                    if (_cs.SubreportName == "rptCheque")
                    {
                        ReportDocument subRepDoc = _JobInvoicePh.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receiptCQ);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitmCQ);
                    }
                    //if (_cs.SubreportName == "rptAccount")
                    //{
                    //    ReportDocument subRepDoc = _JobInvoice.Subreports[_cs.SubreportName];

                    //    subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);
                    //    subRepDoc.Database.Tables["hpt_shed"].SetDataSource(hpt_shed);
                    //}

                    if (_cs.SubreportName == "tax")
                    {
                        ReportDocument subRepDoc = _JobInvoicePh.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);
                    }
                    if (_cs.SubreportName == "rptComm")
                    {
                        ReportDocument subRepDoc = _JobInvoicePh.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                    }
                    //if (_cs.SubreportName == "rptWarr")
                    //{
                    //    mst_item1 = mst_item1.DefaultView.ToTable(true);
                    //    ReportDocument subRepDoc = _JobInvoice.Subreports[_cs.SubreportName];

                    //    subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);
                    //    subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                    //    subRepDoc.Database.Tables["MST_ITM"].SetDataSource(mst_item1);
                    //    subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                    //}
                    //if (_cs.SubreportName == "giftVou")
                    //{
                    //    ReportDocument subRepDoc = _JobInvoice.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                    //}
                    if (_cs.SubreportName == "loc")
                    {
                        ReportDocument subRepDoc = _JobInvoicePh.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                    }
                    if (_cs.SubreportName == "serials")
                    {
                        ReportDocument subRepDoc = _JobInvoicePh.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["INT_BATCH"].SetDataSource(int_batch1);
                        subRepDoc.Database.Tables["INT_SER"].SetDataSource(int_ser);
                    }
                    if (_cs.SubreportName == "Job_Defects")
                    {
                        ReportDocument subRepDoc = _JobInvoicePh.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                    }
                    if (_cs.SubreportName == "Job_Serials")
                    {
                        ReportDocument subRepDoc = _JobInvoicePh.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobSer);
                    }
                    if (_cs.SubreportName == "Job_Serials_Sub")
                    {
                        ReportDocument subRepDoc = _JobInvoicePh.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SCV_JOB_DET_SUB"].SetDataSource(ServiceJobSerSub);
                    }

                    //if (tblComDate.Rows.Count >0) 
                    //{
                    //  if (_cs.SubreportName == "warrComDate")
                    //  {
                    //      ReportDocument subRepDoc = _JobInvoice.Subreports[_cs.SubreportName];
                    //      subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                    //  }
                    //}
                }
            }


            
            
        }

        public void ServiceJobEstimateAuto() //Tharanga 2017/06/17
        {
           
            DataTable ServiceJobHdr = new DataTable();
            DataTable ServiceJobDet = new DataTable();
            DataTable ServiceJobEstHdr = new DataTable();
            DataTable ServiceJobEstDtl = new DataTable();
            DataTable ReportConditions = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_COM = new DataTable();

            ServiceJobEstHdr = bsObj.CHNLSVC.CustService.sp_get_Estimate_details(BaseCls.GlbReportDoc);
            ServiceJobEstDtl = bsObj.CHNLSVC.CustService.sp_get_EstimateItem_details(BaseCls.GlbReportDoc);

            if (ServiceJobEstHdr.Rows.Count > 0)
            {
                foreach (DataRow drow in ServiceJobEstHdr.Rows)
                {
                    {
                        ServiceJobHdr = bsObj.CHNLSVC.CustService.sp_get_job_header(drow["ESH_JOB_NO"].ToString());
                        ServiceJobDet = bsObj.CHNLSVC.CustService.sp_get_job_details(drow["ESH_JOB_NO"].ToString(), "JOB");
                        MST_LOC = bsObj.CHNLSVC.CustService.sp_get_loc_details(drow["ESH_LOC"].ToString());
                        MST_COM = bsObj.CHNLSVC.CustService.sp_get_com_details(drow["ESH_COM"].ToString());
                    }
                }
            }

            if (MST_LOC.Rows.Count > 0)
            {
                foreach (DataRow drow1 in MST_LOC.Rows)
                {
                    ReportConditions = bsObj.CHNLSVC.CustService.sp_get_Report_info_chnl(BaseCls.GlbReportName, drow1["ML_CATE_3"].ToString());
                }
            }

            _job_EstimateAuto.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
            _job_EstimateAuto.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
            _job_EstimateAuto.Database.Tables["SCV_EST_HDR"].SetDataSource(ServiceJobEstHdr);
            _job_EstimateAuto.Database.Tables["SCV_EST_ITM"].SetDataSource(ServiceJobEstDtl);
            _job_EstimateAuto.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _job_EstimateAuto.Database.Tables["MST_COM"].SetDataSource(MST_COM);

            foreach (object repOp in _job_EstimateAuto.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "Conditions")
                    {
                        ReportDocument subRepDoc = _job_EstimateAuto.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                    }
                }
            }

        }

        public void ABE_ServiceJobCard_Workshop() //Tharanga 2017/07/06
        {

            DataTable ServiceJobHdr = new DataTable();
            DataTable ServiceJobDet = new DataTable();
            DataTable ServiceJobDef = new DataTable();
            DataTable ServiceJobTech = new DataTable();
            DataTable ReportConditions = new DataTable();
            DataTable ServiceJobDetSub = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable USER = new DataTable();

            ServiceJobHdr = bsObj.CHNLSVC.CustService.sp_get_job_header(BaseCls.GlbReportDoc);
            ServiceJobDet = bsObj.CHNLSVC.CustService.sp_get_job_details(BaseCls.GlbReportDoc, "JOB");
            ServiceJobDef = bsObj.CHNLSVC.CustService.sp_get_job_defects(BaseCls.GlbReportDoc);
            ServiceJobTech = bsObj.CHNLSVC.CustService.getJobTechnician(BaseCls.GlbReportDoc, BaseCls.GlbUserComCode);
            ServiceJobDetSub = bsObj.CHNLSVC.CustService.sp_get_job_detailsSub(BaseCls.GlbReportDoc);
            //USER = bsObj.CHNLSVC.CustService.getServiceJobUser(BaseCls.GlbUserID);

            if (ServiceJobHdr.Rows.Count > 0)
            {
                foreach (DataRow drow in ServiceJobHdr.Rows)
                {
                    MST_COM = bsObj.CHNLSVC.CustService.sp_get_com_details(drow["SJB_COM"].ToString());
                }
            }
            if (ServiceJobDet.Rows.Count > 0)
            {
                foreach (DataRow drow in ServiceJobDet.Rows)
                {
                    MST_LOC = bsObj.CHNLSVC.CustService.sp_get_loc_details(drow["JBD_LOC"].ToString());

                    if (MST_LOC.Rows.Count > 0)
                    {
                        foreach (DataRow drow1 in MST_LOC.Rows)
                        {
                            ReportConditions = bsObj.CHNLSVC.CustService.sp_get_Report_info_chnl(BaseCls.GlbReportName, drow1["ML_CATE_3"].ToString());
                        }
                    }
                }
            }




            if (BaseCls.GlbReportName == "JobCard_ABE_Work_shop.rpt")
            {
                _JobCard_ABE_Work_shop.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _JobCard_ABE_Work_shop.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _JobCard_ABE_Work_shop.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                _JobCard_ABE_Work_shop.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _JobCard_ABE_Work_shop.Database.Tables["MST_COM"].SetDataSource(MST_COM);

                foreach (object repOp in _JobCard_ABE_Work_shop.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Conditions")
                        {
                            ReportDocument subRepDoc = _JobCard_ABE_Work_shop.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                        }
                        if (_cs.SubreportName == "JobTechnican")
                        {
                            ReportDocument subRepDoc = _JobCard_ABE_Work_shop.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["JOB_TECHNICIAN"].SetDataSource(ServiceJobTech);
                        }
                        if (_cs.SubreportName == "sub_serial")
                        {
                            ReportDocument subRepDoc = _JobCard_ABE_Work_shop.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["SCV_JOB_DET_SUB"].SetDataSource(ServiceJobDetSub);
                        }
                    }
                }
            }

            if (BaseCls.GlbReportName == "JobCard_ABE_Feild.rpt")
            {
                _JobCard_ABE_Feild.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _JobCard_ABE_Feild.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _JobCard_ABE_Feild.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                _JobCard_ABE_Feild.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _JobCard_ABE_Feild.Database.Tables["MST_COM"].SetDataSource(MST_COM);

                foreach (object repOp in _JobCard_ABE_Feild.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Conditions")
                        {
                            ReportDocument subRepDoc = _JobCard_ABE_Feild.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                        }
                        if (_cs.SubreportName == "JobTechnican")
                        {
                            ReportDocument subRepDoc = _JobCard_ABE_Feild.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["JOB_TECHNICIAN"].SetDataSource(ServiceJobTech);
                        }
                    }
                }
            }

        }

        public void Service_Invocie_ABE()//Tharanga 2017/07/07
        {
            string invNo = default(string);
            string accNo = default(string);
            Boolean ishp = default(Boolean);
            ishp = false;
            invNo = BaseCls.GlbReportDoc;
            DataTable mst_tax_master = new DataTable();
            DataRow drr;
            DataTable salesDetails = new DataTable();
            //DataTable sat_vou_det = new DataTable();
            DataTable param = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable ReportConditions = new DataTable();
            DataTable ServiceJobHdr = new DataTable();
            DataTable ServiceJobDet = new DataTable();
            param.Clear();
            param.Columns.Add("isCnote", typeof(Int16));

            Int16 isCredit = 0;

            salesDetails.Clear();
            ServiceJobHdr.Clear();
            ServiceJobDet.Clear();

            salesDetails = bsObj.CHNLSVC.Sales.GetSalesDetails(invNo, null);
            ServiceJobHdr = bsObj.CHNLSVC.CustService.sp_get_job_header(BaseCls.GlbReportDoc);
            ServiceJobDet = bsObj.CHNLSVC.CustService.sp_get_job_details(BaseCls.GlbReportDoc, "JOB");

            if (salesDetails.Rows.Count > 0)
            {
                foreach (DataRow drow in salesDetails.Rows)
                {
                    {
                      
                        MST_LOC = bsObj.CHNLSVC.CustService.sp_get_loc_details(drow["SAH_PC"].ToString());
                       
                    }
                }
            }

            if (MST_LOC.Rows.Count > 0)
            {
                foreach (DataRow drow1 in MST_LOC.Rows)
                {
                    ReportConditions = bsObj.CHNLSVC.CustService.sp_get_Report_info_chnl(BaseCls.GlbReportName, drow1["ML_CATE_3"].ToString());
                }
            }




            mst_tax_master = bsObj.CHNLSVC.Sales.GetinvTax(invNo);

            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            //DataTable int_batch = new DataTable();
            // DataTable int_batch1 = new DataTable();
            DataTable MST_ITM = new DataTable();
            //DataTable MST_LOC = new DataTable();
            DataTable sar_sub_tp = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            //DataTable tblComDate = new DataTable();
            DataTable int_hdr = new DataTable();
            DataTable int_ser = new DataTable();

            int _numPages = 0;
            DataRow dr3;

            _numPages = bsObj.CHNLSVC.General.GetReprintDocCount(invNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr3);
            _Service_Invoice_ABE.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();
            int_hdr.Clear();
            int_ser.Clear();

            //DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
            //DataTable hpt_acc = new DataTable();
            DataRow dr;
            DataRow dr1;

            //hpt_acc.Columns.Add("HPA_ACC_NO", typeof(string));
            //hpt_acc.Columns.Add("HPA_ACC_CRE_DT", typeof(DateTime));
            //hpt_acc.Columns.Add("HPA_SCH_CD", typeof(string));
            //hpt_acc.Columns.Add("HPA_TERM", typeof(decimal));
            //hpt_acc.Columns.Add("HPA_DP_VAL", typeof(decimal));
            //hpt_acc.Columns.Add("HPA_AF_VAL", typeof(decimal));
            //hpt_acc.Columns.Add("HPA_INIT_INS", typeof(decimal));
            //hpt_acc.Columns.Add("HPA_INIT_VAT", typeof(decimal));
            //hpt_acc.Columns.Add("HPA_INIT_STM", typeof(decimal));
            //hpt_acc.Columns.Add("HPA_INIT_SER_CHG", typeof(decimal));

            //foreach (DataRow row in accountDetails.Rows)
            //{
            //    dr = hpt_acc.NewRow();
            //    accNo = row["HPA_ACC_NO"].ToString();
            //    dr["HPA_ACC_NO"] = row["HPA_ACC_NO"].ToString();
            //    dr["HPA_ACC_CRE_DT"] = row["HPA_ACC_CRE_DT"].ToString();
            //    dr["HPA_SCH_CD"] = row["HPA_SCH_CD"].ToString();
            //    dr["HPA_TERM"] = Convert.ToDecimal(row["HPA_TERM"].ToString());
            //    dr["HPA_DP_VAL"] = Convert.ToDecimal(row["HPA_DP_VAL"].ToString());
            //    dr["HPA_AF_VAL"] = Convert.ToDecimal(row["HPA_AF_VAL"].ToString());
            //    dr["HPA_INIT_INS"] = Convert.ToDecimal(row["HPA_INIT_INS"].ToString());
            //    dr["HPA_INIT_VAT"] = Convert.ToDecimal(row["HPA_INIT_VAT"].ToString());
            //    dr["HPA_INIT_STM"] = Convert.ToDecimal(row["HPA_INIT_STM"].ToString());
            //    dr["HPA_INIT_SER_CHG"] = Convert.ToDecimal(row["HPA_INIT_SER_CHG"].ToString());
            //    hpt_acc.Rows.Add(dr);
            //}

            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_SVAT_NO", typeof(string));

            sat_hdr1.Columns.Add("SAH_DT", typeof(DateTime));
            sat_hdr1.Columns.Add("SAH_INV_NO", typeof(string));
            sat_hdr1.Columns.Add("SAH_INV_TP", typeof(string));
            sat_hdr1.Columns.Add("SAH_IS_SVAT", typeof(Int16));
            sat_hdr1.Columns.Add("SAH_MAN_REF", typeof(string));
            sat_hdr1.Columns.Add("SAH_PC", typeof(string));
            sat_hdr1.Columns.Add("SAH_REMARKS", typeof(string));
            sat_hdr1.Columns.Add("SAH_SALES_EX_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_TAX_EXEMPTED", typeof(Int16));
            sat_hdr1.Columns.Add("SAH_TAX_INV", typeof(Int16));
            sat_hdr1.Columns.Add("SAH_ACC_NO", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_NAME", typeof(string)); //Add by Chamal 23/05/2013
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_NAME", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_SEQ_NO", typeof(decimal));
            sat_hdr1.Columns.Add("SAH_COM", typeof(string));
            sat_hdr1.Columns.Add("SAH_CRE_BY", typeof(string));
            sat_hdr1.Columns.Add("SAH_INV_SUB_TP", typeof(string));
            sat_hdr1.Columns.Add("SAH_REF_DOC", typeof(string));
            sat_hdr1.Columns.Add("esep_name_initials", typeof(string));
            sat_hdr1.Columns.Add("SAH_ANAL_11", typeof(Int16));
            sat_hdr1.Columns.Add("sah_del_loc", typeof(string));

            sat_itm.Columns.Add("SAD_WARR_REMARKS", typeof(string));
            sat_itm.Columns.Add("SAD_UNIT_RT", typeof(decimal));
            sat_itm.Columns.Add("SAD_TOT_AMT", typeof(decimal));
            sat_itm.Columns.Add("SAD_QTY", typeof(decimal));
            sat_itm.Columns.Add("SAD_ITM_TAX_AMT", typeof(decimal));
            sat_itm.Columns.Add("SAD_ITM_CD", typeof(string));
            sat_itm.Columns.Add("SAD_DISC_RT", typeof(decimal));
            sat_itm.Columns.Add("SAD_DISC_AMT", typeof(decimal));
            sat_itm.Columns.Add("SAD_SEQ_NO", typeof(string));
            sat_itm.Columns.Add("SAD_ITM_LINE", typeof(Int16));
            sat_itm.Columns.Add("SAD_WARR_PERIOD", typeof(Int16));
            sat_itm.Columns.Add("SAD_JOB_NO", typeof(string));
            sat_itm.Columns.Add("SAD_MERGE_ITM", typeof(string));
            sat_itm.Columns.Add("SAD_MERGE_ITM_DESC", typeof(string));
            sat_itm.Columns.Add("VAT_AMT", typeof(decimal));
            sat_itm.Columns.Add("NBT_AMT", typeof(decimal));
            sat_itm.Columns.Add("OTH_TAX_AMT", typeof(decimal));

            mst_profit_center.Columns.Add("MPC_ADD_1", typeof(string));
            mst_profit_center.Columns.Add("MPC_ADD_2", typeof(string));
            mst_profit_center.Columns.Add("MPC_CHNL", typeof(string));
            mst_profit_center.Columns.Add("MPC_DESC", typeof(string));
            mst_profit_center.Columns.Add("MPC_COM", typeof(string));
            mst_profit_center.Columns.Add("MPC_CD", typeof(string));
            mst_profit_center.Columns.Add("MPC_TEL", typeof(string));
            mst_profit_center.Columns.Add("MPC_FAX", typeof(string));
            mst_profit_center.Columns.Add("MPC_EMAIL", typeof(string));
            mst_profit_center.Columns.Add("MPC_OTH_REF", typeof(string));

            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));
            mst_item.Columns.Add("MI_IS_SER1", typeof(Int16));
            mst_item.Columns.Add("MI_IS_SER2", typeof(Int16));
            mst_item.Columns.Add("MI_WARR", typeof(Int16));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_IS_SER1", typeof(Int16));
            mst_item1.Columns.Add("MI_IS_SER2", typeof(Int16));
            mst_item1.Columns.Add("MI_WARR", typeof(Int16));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));
            mst_com.Columns.Add("MC_TAX3", typeof(string));
            mst_com.Columns.Add("MC_ANAL18", typeof(string));
            mst_com.Columns.Add("MC_ANAL19", typeof(string));

            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));

            //int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            //int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            //int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            //int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            //int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));
            //int_batch.Columns.Add("ITB_BATCH_LINE", typeof(Int16));

            foreach (DataRow row in salesDetails.Rows)
            {
                dr = mst_busentity.NewRow();
                int index = salesDetails.Rows.IndexOf(row);

                //int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));
                //foreach (DataRow row1 in int_batch1.Rows)
                //{
                //    dr1 = int_batch.NewRow();
                //    dr1["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                //    dr1["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                //    dr1["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                //    dr1["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                //    dr1["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                //    dr1["ITB_BATCH_LINE"] = Convert.ToInt16(row1["ITB_BATCH_LINE"].ToString());
                //    int_batch.Rows.Add(dr1);

                //}
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_SVAT_NO"] = row["MBE_SVAT_NO"].ToString();

                    mst_busentity.Rows.Add(dr);

                    dr = sat_hdr1.NewRow();
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
                    dr["SAH_D_CUST_NAME"] = row["SAH_D_CUST_NAME"].ToString();//Add by Chamal 23/05/2013
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    dr["SAH_INV_SUB_TP"] = row["SAH_INV_SUB_TP"].ToString();
                    dr["SAH_REF_DOC"] = row["SAH_REF_DOC"].ToString();
                    dr["esep_name_initials"] = row["esep_name_initials"].ToString();
                    if (!string.IsNullOrEmpty(row["SAH_ANAL_11"].ToString()))// Nadeeka 02-03-2015
                    {
                        dr["SAH_ANAL_11"] = Convert.ToInt16(row["SAH_ANAL_11"].ToString());
                    }
                    else
                    {
                        dr["SAH_ANAL_11"] = 0;
                    }
                    if (!string.IsNullOrEmpty(row["sah_del_loc"].ToString()))
                    {
                        dr["sah_del_loc"] = row["sah_del_loc"].ToString();
                        MST_LOC = bsObj.CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["sah_del_loc"].ToString());
                    }
                    else
                    {
                        dr["sah_del_loc"] = string.Empty;
                    }
                    sar_sub_tp = bsObj.CHNLSVC.Sales.GetSalesSubTypeTable(row["SAH_INV_TP"].ToString(), row["SAH_INV_SUB_TP"].ToString());

                    sat_hdr1.Rows.Add(dr);
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
                dr["SAD_ITM_LINE"] = Convert.ToInt16(row["SAD_ITM_LINE"].ToString());
                dr["SAD_WARR_PERIOD"] = Convert.ToInt16(row["SAD_WARR_PERIOD"].ToString());
                dr["SAD_JOB_NO"] = row["SAD_JOB_NO"].ToString();
                if (row["SAD_MERGE_ITM"].ToString() == "")
                { dr["SAD_MERGE_ITM"] = "N/A"; }
                else
                { dr["SAD_MERGE_ITM"] = row["SAD_MERGE_ITM"].ToString(); }
                dr["SAD_MERGE_ITM_DESC"] = row["SAD_MERGE_ITM_DESC"].ToString();
                dr["VAT_AMT"] = Convert.ToDecimal(row["VAT_AMT"].ToString());
                dr["NBT_AMT"] = Convert.ToDecimal(row["NBT_AMT"].ToString());
                dr["OTH_TAX_AMT"] = Convert.ToDecimal(row["OTH_TAX_AMT"].ToString());
                sat_itm.Rows.Add(dr);

                dr = mst_item.NewRow();
                dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                dr["MI_CD"] = row["MI_CD"].ToString();

                mst_item.Rows.Add(dr);

                if (index == 0)
                {
                    //if (accountDetails.Rows.Count > 0)
                    //{
                    //    tblComDate = CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), accNo);
                    //}
                    //else
                    //{
                    //    tblComDate = CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), invNo);
                    //}

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
                    dr["MPC_OTH_REF"] = row["MPC_OTH_REF"].ToString();

                    mst_profit_center.Rows.Add(dr);

                    dr = mst_com.NewRow();
                    dr["MC_TAX1"] = row["MC_TAX1"].ToString();
                    dr["MC_TAX2"] = row["MC_TAX2"].ToString();
                    dr["MC_CD"] = row["MC_CD"].ToString();
                    dr["MC_DESC"] = row["MC_DESC"].ToString();
                    dr["MC_IT_POWERED"] = row["MC_IT_POWERED"].ToString();
                    dr["MC_TAX3"] = row["MC_TAX3"].ToString();
                    dr["MC_ANAL18"] = row["MC_ANAL18"].ToString();
                    dr["MC_ANAL19"] = row["MC_ANAL19"].ToString();
                    mst_com.Rows.Add(dr);

                    dr = sec_user.NewRow();
                    dr["SE_USR_NAME"] = row["SE_USR_NAME"].ToString();
                    dr["SE_USR_ID"] = row["SE_USR_ID"].ToString();
                    sec_user.Rows.Add(dr);
                };
            }

            DataTable deliveredSerials = bsObj.CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
            if (deliveredSerials.Rows.Count>0)
            {
                
           
           

            int_hdr.Columns.Add("ITH_SEQ_NO", typeof(decimal));
            int_hdr.Columns.Add("ITH_COM", typeof(string));
            int_hdr.Columns.Add("ITH_OTH_DOCNO", typeof(string));
            int_hdr.Columns.Add("ITH_DOC_NO", typeof(string));
            int_ser.Columns.Add("ITS_SEQ_NO", typeof(string));
            int_ser.Columns.Add("ITS_ITM_LINE", typeof(Int16));
            int_ser.Columns.Add("ITS_BATCH_LINE", typeof(Int16));
            int_ser.Columns.Add("ITS_SER_LINE", typeof(Int16));
            int_ser.Columns.Add("ITS_DOC_NO", typeof(string));
            int_ser.Columns.Add("ITS_SER_ID", typeof(string));
            int_ser.Columns.Add("ITS_SER_1", typeof(string));
            int_ser.Columns.Add("ITS_SER_2", typeof(string));
            int_ser.Columns.Add("ITS_WARR_NO", typeof(string));
            int_ser.Columns.Add("ITS_WARR_PERIOD", typeof(decimal));
            int_ser.Columns.Add("ITS_ITM_CD", typeof(string));

            foreach (DataRow row in deliveredSerials.Rows)
            {
                dr = int_hdr.NewRow();
                dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                dr["ITH_COM"] = row["ITH_COM"].ToString();
                dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                int_hdr.Rows.Add(dr);

                if (row["ITS_SER_1"].ToString() != "N/A")
                {
                    dr = int_ser.NewRow();
                    dr["ITS_SEQ_NO"] = row["ITS_SEQ_NO"].ToString();
                    dr["ITS_ITM_LINE"] = Convert.ToInt16(row["ITS_ITM_LINE"].ToString());
                    dr["ITS_BATCH_LINE"] = Convert.ToInt16(row["ITS_BATCH_LINE"].ToString());
                    dr["ITS_SER_LINE"] = row["ITS_SER_LINE"].ToString();
                    dr["ITS_DOC_NO"] = row["ITS_DOC_NO"].ToString();
                    dr["ITS_SER_ID"] = row["ITS_SER_ID"].ToString();
                    dr["ITS_SER_1"] = row["ITS_SER_1"].ToString();
                    dr["ITS_SER_2"] = row["ITS_SER_2"].ToString();
                    dr["ITS_WARR_NO"] = row["ITS_WARR_NO"].ToString();
                    dr["ITS_WARR_PERIOD"] = Convert.ToDecimal(row["ITS_WARR_PERIOD"].ToString());
                    int_ser.Rows.Add(dr);
                };

                DataTable MST_ITM1 = new DataTable();
                MST_ITM1 = bsObj.CHNLSVC.Sales.GetItemCode(row["ITH_COM"].ToString(), row["ITS_ITM_CD"].ToString());
                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = mst_item1.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    dr["MI_IS_SER1"] = row1["MI_IS_SER1"].ToString();
                    dr["MI_IS_SER2"] = row1["MI_IS_SER2"].ToString();
                    dr["MI_WARR"] = row1["MI_WARR"].ToString();
                    mst_item1.Rows.Add(dr);
                }
            }
            }
            DataTable receiptDetails = bsObj.CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);

            DataTable sat_receiptCQ = new DataTable();
            DataTable sat_receiptitmCQ = new DataTable();
            sat_receiptCQ.Columns.Add("SAR_SEQ_NO", typeof(string));
            sat_receiptCQ.Columns.Add("SAR_COM_CD", typeof(string));
            sat_receiptCQ.Columns.Add("SAR_RECEIPT_TYPE", typeof(string));
            sat_receiptCQ.Columns.Add("SAR_RECEIPT_NO", typeof(string));

            sat_receiptitmCQ.Columns.Add("SARD_SEQ_NO", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_RECEIPT_NO", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_INV_NO", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_PAY_TP", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_REF_NO", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_SETTLE_AMT", typeof(decimal));
            sat_receiptitmCQ.Columns.Add("SAPT_DESC", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_RMK", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_ANAL_3", typeof(decimal));

            foreach (DataRow row in receiptDetails.Rows)
            {
                if (row["SAR_RECEIPT_TYPE"].ToString() == "DIR")//(row["SARD_PAY_TP"].ToString() == "CHEQUE" && row["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                {
                    dr = sat_receiptCQ.NewRow();
                    dr["SAR_SEQ_NO"] = row["SAR_SEQ_NO"].ToString();
                    dr["SAR_COM_CD"] = row["SAR_COM_CD"].ToString();
                    dr["SAR_RECEIPT_TYPE"] = row["SAR_RECEIPT_TYPE"].ToString();
                    dr["SAR_RECEIPT_NO"] = row["SAR_RECEIPT_NO"].ToString();
                    sat_receiptCQ.Rows.Add(dr);

                    dr = sat_receiptitmCQ.NewRow();
                    dr["SARD_SEQ_NO"] = row["SARD_SEQ_NO"].ToString();
                    dr["SARD_RECEIPT_NO"] = row["SARD_RECEIPT_NO"].ToString();
                    dr["SARD_INV_NO"] = row["SARD_INV_NO"].ToString();
                    dr["SARD_PAY_TP"] = row["SARD_PAY_TP"].ToString();
                    dr["SARD_REF_NO"] = row["SARD_REF_NO"].ToString();
                    dr["SAPT_DESC"] = row["SAPT_DESC"].ToString();
                    dr["SARD_SETTLE_AMT"] = Convert.ToDecimal(row["SARD_SETTLE_AMT"].ToString());
                    dr["SARD_RMK"] = row["SARD_RMK"].ToString();
                    dr["SARD_ANAL_3"] = Convert.ToDecimal(row["SARD_ANAL_3"].ToString());
                    sat_receiptitmCQ.Rows.Add(dr);


                    if (row["SARD_PAY_TP"].ToString() == "CRNOTE")
                    {
                        isCredit = 1;
                    }
                };
            }

            //DataTable hpt_shed = CHNLSVC.Sales.GetAccountSchedule(invNo);
            //DataTable Promo = CHNLSVC.Sales.GetPromotionByInvoice(invNo);

           // DataTable ref_rep_infor = bsObj.CHNLSVC.Sales.GetReportInfor("Service_Invoice_ABE.rpt");
            mst_item = mst_item.DefaultView.ToTable(true);

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

            _Service_Invoice_ABE.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _Service_Invoice_ABE.Database.Tables["mst_com"].SetDataSource(mst_com);
            _Service_Invoice_ABE.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            _Service_Invoice_ABE.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            _Service_Invoice_ABE.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            _Service_Invoice_ABE.Database.Tables["mst_item"].SetDataSource(mst_item);
            _Service_Invoice_ABE.Database.Tables["sec_user"].SetDataSource(sec_user);
            _Service_Invoice_ABE.Database.Tables["sar_sub_tp"].SetDataSource(sar_sub_tp);
            _Service_Invoice_ABE.Database.Tables["param"].SetDataSource(param);
            _Service_Invoice_ABE.Database.Tables["int_hdr"].SetDataSource(int_hdr);
            _Service_Invoice_ABE.Database.Tables["int_ser"].SetDataSource(int_ser);
            //_Job_Invoce_ABE.Database.Tables["Promo"].SetDataSource(Promo);

            foreach (object repOp in _Service_Invoice_ABE.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    //if (_cs.SubreportName == "rptWarranty")
                    //{
                    //    ReportDocument subRepDoc = _Job_Invoce_ABE.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                    //}

                    if (_cs.SubreportName == "Conditions")
                    {
                        ReportDocument subRepDoc = _Service_Invoice_ABE.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                    }

                    if (_cs.SubreportName == "rptCheque")
                    {
                        ReportDocument subRepDoc = _Service_Invoice_ABE.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receiptCQ);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitmCQ);
                    }
                    //if (_cs.SubreportName == "rptAccount")
                    //{
                    //    ReportDocument subRepDoc = _Job_Invoce_ABE.Subreports[_cs.SubreportName];

                    //    subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);
                    //    subRepDoc.Database.Tables["hpt_shed"].SetDataSource(hpt_shed);
                    //}

                    if (_cs.SubreportName == "tax")
                    {
                        ReportDocument subRepDoc = _Service_Invoice_ABE.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);
                    }
                    //if (_cs.SubreportName == "rptComm")
                    //{
                    //    ReportDocument subRepDoc = _Service_Invoice_ABE.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                    //}
                    //if (_cs.SubreportName == "rptWarr")
                    //{
                    //    mst_item1 = mst_item1.DefaultView.ToTable(true);
                    //    ReportDocument subRepDoc = _Job_Invoce_ABE.Subreports[_cs.SubreportName];

                    //    subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);
                    //    subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                    //    subRepDoc.Database.Tables["MST_ITM"].SetDataSource(mst_item1);
                    //    subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                    //}
                    //if (_cs.SubreportName == "giftVou")
                    //{
                    //    ReportDocument subRepDoc = _Job_Invoce_ABE.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                    //}
                    if (_cs.SubreportName == "loc")
                    {
                        ReportDocument subRepDoc = _Service_Invoice_ABE.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                    }

                    //if (tblComDate.Rows.Count >0) 
                    //{
                    //  if (_cs.SubreportName == "warrComDate")
                    //  {
                    //      ReportDocument subRepDoc = _Job_Invoce_ABE.Subreports[_cs.SubreportName];
                    //      subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                    //  }
                    //}
                }
            }

          
        }


        public void Get_Spare_parts_Movement_Report() //Tharanga
        {

            DataTable Spare_parts = new DataTable();
            DataTable ServiceJobDet = new DataTable();
            DataTable ServiceJobDef = new DataTable();
            DataTable ServiceJobTech = new DataTable();
            DataTable ReportConditions = new DataTable();
            DataTable ServiceJobDetSub = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable USER = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            param.Rows.Add(dr);

            MST_LOC = bsObj.CHNLSVC.CustService.sp_get_loc_details(BaseCls.GlbUserDefLoca);
            MST_COM = bsObj.CHNLSVC.CustService.sp_get_com_details(BaseCls.GlbUserComCode);
           // Spare_parts = bsObj.CHNLSVC.Inventory.Get_Spare_parts_Movement_Report(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbUserDefProf);
            //ServiceJobDet = bsObj.CHNLSVC.CustService.sp_get_job_details(BaseCls).GlbReportDoc, "JOB");
            //ServiceJobDef = bsObj.CHNLSVC.CustService.sp_get_job_defects(BaseCls.GlbReportDoc);
            //ServiceJobTech = bsObj.CHNLSVC.CustService.getJobTechnician(BaseCls.GlbReportDoc, BaseCls.GlbUserComCode);
            //ServiceJobDetSub = bsObj.CHNLSVC.CustService.sp_get_job_detailsSub(BaseCls.GlbReportDoc);
            //USER = bsObj.CHNLSVC.CustService.getServiceJobUser(BaseCls.GlbUserID);
            if (BaseCls.GlbReportName=="Spare_Parts_Movement_Report.rpt")
            {
                Spare_parts = bsObj.CHNLSVC.Inventory.Get_Spare_parts_Movement_Report(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbUserDefProf);
                _Spare_Parts_Movement_Report.Database.Tables["Spare_arts_Movement_Report"].SetDataSource(Spare_parts);
                _Spare_Parts_Movement_Report.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _Spare_Parts_Movement_Report.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _Spare_Parts_Movement_Report.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportName == "Spare_parts_movement.rpt")  
            {

                Spare_parts = bsObj.CHNLSVC.Inventory.Get_Spare_parts_Movement(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbUserDefProf, BaseCls.GlbReqUserPermissionLevel);
                _Spare_parts_movement.Database.Tables["Spare_arts_Movement_Report"].SetDataSource(Spare_parts);
                _Spare_parts_movement.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _Spare_parts_movement.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _Spare_parts_movement.Database.Tables["param"].SetDataSource(param);
            }
           
                //_JobCard_Power_tools.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                //_JobCard_Power_tools.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                //_JobCard_Power_tools.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                //_JobCard_Power_tools.Database.Tables["MST_COM"].SetDataSource(MST_COM);

               
            

        }
        public void ServiceJobEstimate_ABE() //Tharanga 2017/07/18
        {

            DataTable ServiceJobHdr = new DataTable();
            DataTable ServiceJobDet = new DataTable();
            DataTable ServiceJobEstHdr = new DataTable();
            DataTable ServiceJobEstDtl = new DataTable();
            DataTable ReportConditions = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable odt = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            param.Clear();
            param.Columns.Add("esv_tax_tp", typeof(string));
            param.Columns.Add("esv_tax_amt", typeof(Int32));


        

            ServiceJobEstHdr = bsObj.CHNLSVC.CustService.sp_get_Estimate_details(BaseCls.GlbReportDoc);
            ServiceJobEstDtl = bsObj.CHNLSVC.CustService.sp_get_EstimateItem_details(BaseCls.GlbReportDoc);
           

            if (ServiceJobEstHdr.Rows.Count > 0)
            {
                foreach (DataRow drow in ServiceJobEstHdr.Rows)
                {
                    {
                        ServiceJobHdr = bsObj.CHNLSVC.CustService.sp_get_job_header(drow["ESH_JOB_NO"].ToString());
                        ServiceJobDet = bsObj.CHNLSVC.CustService.sp_get_job_details(drow["ESH_JOB_NO"].ToString(), "JOB");
                        MST_LOC = bsObj.CHNLSVC.CustService.sp_get_loc_details(drow["ESH_LOC"].ToString());
                        MST_COM = bsObj.CHNLSVC.CustService.sp_get_com_details(drow["ESH_COM"].ToString());
                    }
                }
            }

            if (MST_LOC.Rows.Count > 0)
            {
                foreach (DataRow drow1 in MST_LOC.Rows)
                {
                    ReportConditions = bsObj.CHNLSVC.CustService.sp_get_Report_info_chnl(BaseCls.GlbReportName, drow1["ML_CATE_3"].ToString());
                }
            }

            _job_Estimate_ABE.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
            _job_Estimate_ABE.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
            _job_Estimate_ABE.Database.Tables["SCV_EST_HDR"].SetDataSource(ServiceJobEstHdr);
            _job_Estimate_ABE.Database.Tables["SCV_EST_ITM"].SetDataSource(ServiceJobEstDtl);
            _job_Estimate_ABE.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _job_Estimate_ABE.Database.Tables["MST_COM"].SetDataSource(MST_COM);

            foreach (object repOp in _job_Estimate_ABE.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "Conditions")
                    {
                        ReportDocument subRepDoc = _job_Estimate_ABE.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
                    }
                }
            }

        }

        public void BERLetterPrint_ABE()//Tharanga 2017/07/18
        {
                   
            DataTable BER = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable ReportConditions = new DataTable();
            DataTable ServiceJobDetSub = new DataTable();
            DataTable MST_COM = new DataTable();

            BER = bsObj.CHNLSVC.CustService.BERLetterDetails(BaseCls.GlbReportJobNo, BaseCls.GlbReportTp);
            ServiceJobDetSub = bsObj.CHNLSVC.CustService.sp_get_job_detailsSub(BaseCls.GlbReportJobNo);
          
            MST_COM = bsObj.CHNLSVC.CustService.sp_get_com_details(BaseCls.GlbUserComCode);

            if (BER.Rows.Count > 0)
            {
                foreach (DataRow drow in BER.Rows)
                {
                    if (drow["ref_type"].ToString() == "BER")
                    {
                        MST_LOC = bsObj.CHNLSVC.CustService.sp_get_loc_details(drow["REF_PC_CODE"].ToString());
                    }
                    else
                    {
                        MST_LOC = bsObj.CHNLSVC.CustService.sp_get_locbypc_details(BaseCls.GlbUserComCode, drow["REF_PC_CODE"].ToString());
                    }

                    if (MST_LOC.Rows.Count > 0)
                    {
                        foreach (DataRow drow1 in MST_LOC.Rows)
                        {
                            ReportConditions = bsObj.CHNLSVC.CustService.sp_get_Report_info_chnl(BaseCls.GlbReportName, drow1["ML_CATE_3"].ToString());
                        }
                    }
                }
            }
            else
            {
                ReportConditions = bsObj.CHNLSVC.CustService.sp_get_Report_info_chnl(BaseCls.GlbReportName, BaseCls.GlbDefSubChannel);
            }

            _Job_BER_Letter_ABE.Database.Tables["BER_REQUEST"].SetDataSource(BER);
            _Job_BER_Letter_ABE.Database.Tables["REF_REP_INFOR_CHNL"].SetDataSource(ReportConditions);
            _Job_BER_Letter_ABE.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _Job_BER_Letter_ABE.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

            foreach (object repOp in _Job_BER_Letter_ABE.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "sub_serials")
                    {
                        ReportDocument subRepDoc = _Job_BER_Letter_ABE.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SCV_JOB_DET_SUB"].SetDataSource(ServiceJobDetSub);
                    }
                }
            }

        }
        public void RedyForCollection_ABE()//Tharanga 2017/07/25
        {

            DataTable scv_amount = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable ReportConditions = new DataTable();
            DataTable ServiceJobDetSub = new DataTable();
            DataTable ServiceJobamount = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable ServiceJobHdr = new DataTable();
            DataTable ServiceJobDet = new DataTable();
            DataTable ServiceJobDet1 = new DataTable();


            ServiceJobHdr = bsObj.CHNLSVC.CustService.sp_get_job_header(BaseCls.GlbReportJobNo);
            ServiceJobDet1 = bsObj.CHNLSVC.CustService.sp_get_job_details(BaseCls.GlbReportJobNo, "JOB");

            if (ServiceJobDet1.Rows.Count < 1)
            {
                MessageBox.Show("Job details not found", "Informetion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (ServiceJobDet1.Rows.Count > 1)
            {
                IEnumerable<DataRow> results = (from MyRows in ServiceJobDet1.AsEnumerable()
                                                where
                                                 MyRows.Field<string>("JBD_SER1") == BaseCls.GlbReportItmClasif
                                                &&
                                                MyRows.Field<string>("JBD_ITM_CD") == BaseCls.GlbReportItemCode
                                                select MyRows);
                ServiceJobDet = results.CopyToDataTable();
            }
            else if (ServiceJobDet1.Rows.Count == 1)
            {
                ServiceJobDet = ServiceJobDet1;
                BaseCls.GlbReportParaLine1 = 1;
            }
           



            scv_amount = bsObj.CHNLSVC.CustService.sp_get_scv_job_amount(BaseCls.GlbReportJobNo, BaseCls.GlbReportParaLine1);
            if (scv_amount.Rows.Count < 1)
            {
                MessageBox.Show("Job Amount not found", "Informetion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ServiceJobDetSub = bsObj.CHNLSVC.CustService.sp_get_job_detailsSub(BaseCls.GlbReportJobNo);

            MST_COM = bsObj.CHNLSVC.CustService.sp_get_com_details(BaseCls.GlbUserComCode);
            MST_LOC = bsObj.CHNLSVC.CustService.sp_get_locbypc_details(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            if (BaseCls.GlbReportName == "RedyForCollection_ABE.rpt")
            {
                _RedyForCollection_ABE.Database.Tables["scv_amount"].SetDataSource(scv_amount);
                _RedyForCollection_ABE.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _RedyForCollection_ABE.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _RedyForCollection_ABE.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _RedyForCollection_ABE.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            }
            else if (BaseCls.GlbReportName == "RedyForDelivery_ABE.rpt")
            {

                _RedyForDelivery_ABE.Database.Tables["scv_amount"].SetDataSource(scv_amount);
                _RedyForDelivery_ABE.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _RedyForDelivery_ABE.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _RedyForDelivery_ABE.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _RedyForDelivery_ABE.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            }
            else if (BaseCls.GlbReportName == "ReadyForDisposal_ABE.rpt")
            {

                _ReadyForDisposal_ABE.Database.Tables["scv_amount"].SetDataSource(scv_amount);
                _ReadyForDisposal_ABE.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _ReadyForDisposal_ABE.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _ReadyForDisposal_ABE.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _ReadyForDisposal_ABE.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            }
            else if (BaseCls.GlbReportName == "ShipmentLetter_ABE.rpt")
            {

                _ShipmentLetter_ABE.Database.Tables["scv_amount"].SetDataSource(scv_amount);
                _ShipmentLetter_ABE.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _ShipmentLetter_ABE.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _ShipmentLetter_ABE.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _ShipmentLetter_ABE.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            }
             else if (BaseCls.GlbReportName == "Cannotbbrepaired_ABE.rpt")
            {

                _Cannotbbrepaired_ABE.Database.Tables["scv_amount"].SetDataSource(scv_amount);
                _Cannotbbrepaired_ABE.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _Cannotbbrepaired_ABE.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _Cannotbbrepaired_ABE.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _Cannotbbrepaired_ABE.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            }
            else if (BaseCls.GlbReportName == "NoticeOfDisposal_ABE.rpt")
            {

                _NoticeOfDisposal_ABE.Database.Tables["scv_amount"].SetDataSource(scv_amount);
                _NoticeOfDisposal_ABE.Database.Tables["SCV_JOB_HDR"].SetDataSource(ServiceJobHdr);
                _NoticeOfDisposal_ABE.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobDet);
                _NoticeOfDisposal_ABE.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _NoticeOfDisposal_ABE.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            }
         
            


        }

        public void JobDetailwithBrandandModel_ABE()//Tharanga 2017/07/25
        {

            DataTable param = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable ReportConditions = new DataTable();
            DataTable ServiceJobDetSub = new DataTable();
            DataTable ServiceJobamount = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable ServiceJobHdr = new DataTable();
            DataTable ServiceJobDet = new DataTable();
            DataTable ServiceJobDet1 = new DataTable();
            DataRow dr;
            string _error = "";
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            MST_COM = bsObj.CHNLSVC.CustService.sp_get_com_details(BaseCls.GlbUserComCode);
            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["comp"] = BaseCls.GlbUserComCode;
            dr["compaddr"] = BaseCls.GlbUserDefLoca;
            param.Rows.Add(dr);

                 tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

                 if (tmp_user_pc.Rows.Count > 0)
                 {
                     foreach (DataRow drow in tmp_user_pc.Rows)
                     {
                         DataTable dt = bsObj.CHNLSVC.CustService.JobDetails_ABE(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode,drow["tpl_pc"].ToString(),//BaseCls.GlbUserDefLoca,
                BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, 
                BaseCls.GlbReportItemCat3, BaseCls.GlbReportTechncian, BaseCls.GlbReportJobCat, BaseCls.GlbReportItemType, 
                BaseCls.GlbReportDiscRate, BaseCls.GlbReportWarrStatus, BaseCls.GlbReportJobNo, BaseCls.GlbUserID, out _error);
                         ServiceJobDet.Merge(dt);
                     }
                 }
           

          
            if (BaseCls.GlbReportName == "BrandModelwiseItemDetails_ABE.rpt")
	            {
                    _BrandModelwiseItemDetails_ABE.Database.Tables["JOB_SUMMARY_ABE"].SetDataSource(ServiceJobDet);
                    _BrandModelwiseItemDetails_ABE.Database.Tables["param"].SetDataSource(param);
                    _BrandModelwiseItemDetails_ABE.Database.Tables["MST_COM"].SetDataSource(MST_COM);
	            }
           
               else if (BaseCls.GlbReportName == "BrandModelwiseItemSummary_ABE_N.rpt")
                {

                    _BrandModelwiseItemSummary_ABE_N.Database.Tables["JOB_SUMMARY_ABE"].SetDataSource(ServiceJobDet);
                    _BrandModelwiseItemSummary_ABE_N.Database.Tables["param"].SetDataSource(param);
                    _BrandModelwiseItemSummary_ABE_N.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                    
                }

               
          


        }

        public void RccLetterPrint()
        {
            DataTable rcc_dt = new DataTable();
            DataTable dt = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_COM = new DataTable();

            DataRow dr;
            rcc_dt = bsObj.CHNLSVC.CustService.GetRccLetterDetails(BaseCls.GlbREportRccNO);
            MST_LOC = bsObj.CHNLSVC.CustService.sp_get_loc_details(BaseCls.GlbUserDefLoca);
            MST_COM = bsObj.CHNLSVC.CustService.sp_get_com_details(BaseCls.GlbUserComCode);

            if(rcc_dt.Rows.Count > 0)
            {
                decimal val = 0;

                dt.Columns.Add("INR_NO", typeof(string));
                dt.Columns.Add("INR_CUST_NAME", typeof(string));
                dt.Columns.Add("INR_ADDR", typeof(string));
                dt.Columns.Add("INR_INV_DT", typeof(string));
                dt.Columns.Add("INR_ITM", typeof(string));
                dt.Columns.Add("INR_SER", typeof(string));
                dt.Columns.Add("WARRENTY", typeof(string));
                dt.Columns.Add("INR_LOC_CD", typeof(string));
                dt.Columns.Add("INR_REM_COUNT", typeof(decimal));

                dr = dt.NewRow();
                dr["INR_NO"] = rcc_dt.Rows[0]["INR_NO"].ToString();
                dr["INR_CUST_NAME"] = rcc_dt.Rows[0]["INR_CUST_NAME"].ToString();
                dr["INR_ADDR"] = rcc_dt.Rows[0]["INR_ADDR"].ToString();
                dr["INR_INV_DT"] = rcc_dt.Rows[0]["INR_INV_DT"].ToString();
                dr["INR_ITM"] = rcc_dt.Rows[0]["INR_ITM"].ToString();
                dr["INR_SER"] = rcc_dt.Rows[0]["INR_SER"].ToString();
                dr["WARRENTY"] = rcc_dt.Rows[0]["WARRENTY"].ToString();
                dr["INR_LOC_CD"] = rcc_dt.Rows[0]["INR_LOC_CD"].ToString();
                dr["INR_REM_COUNT"] = decimal.TryParse(rcc_dt.Rows[0]["INR_REM_COUNT"].ToString(),out val);
                dt.Rows.Add(dr);

                objrccletter.Database.Tables["RCC_Letter"].SetDataSource(rcc_dt);
                objrccletter.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                objrccletter.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            }

            
        }
    }
}
