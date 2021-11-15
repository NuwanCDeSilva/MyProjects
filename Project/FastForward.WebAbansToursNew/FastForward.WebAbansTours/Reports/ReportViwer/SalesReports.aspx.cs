using FastForward.WebAbansTours.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace FastForward.WebAbansTours.Reports.ReportViwer
{
    public partial class DailySales : System.Web.UI.Page
    {
        public FastForward.WebAbansTours.Reports.rptdailysales _dsales = new FastForward.WebAbansTours.Reports.rptdailysales();
        public FastForward.WebAbansTours.Reports.pendinginquiry _penenq = new FastForward.WebAbansTours.Reports.pendinginquiry();
        public FastForward.WebAbansTours.Reports.rptreceiptdtl _recdet = new FastForward.WebAbansTours.Reports.rptreceiptdtl();
        public FastForward.WebAbansTours.Reports.rptdailysales_dtl _dsalesdtl = new FastForward.WebAbansTours.Reports.rptdailysales_dtl();
        public FastForward.WebAbansTours.Reports.Debtors_Settlement _debtsetl = new FastForward.WebAbansTours.Reports.Debtors_Settlement();

        CrystalDecisions.CrystalReports.Engine.ReportDocument reportDocument = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string userId = Session["UserID"] as string;
                string company = Session["UserCompanyCode"] as string;
                string userDefPro = Session["UserDefProf"] as string;
                string userDefLoc = Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    ReportViewerViewModel model = (ReportViewerViewModel)Session["ReportData"];
                    if (model != null)
                    {
                        switch (model.reportName)
                        {
                            case "rptdailysales":
                                model.fileName = "Daily sales report";
                                LoadReportDailySales(model);
                                break;
                            case "rptreceiptdtl":
                                model.fileName = " Receipt Summary";
                                LoadReceiptDetails(model);
                                break;
                            case "rptsalescomm":
                                model.fileName = "Commission Report";
                                LoadCommision(model);
                                break;
                            case "DebtorSettlement":
                                model.fileName = "Debtors Statement";
                                LoadDebetorStat(model);
                                break;
                            case "rptdailysales_dtl":
                                model.fileName = "Daily sales Servic ewise";
                                LoadDailySalesSerEwise(model);
                                break;
                            case "pendinginquiry":
                                 model.fileName = "Pending Inquiries";
                                LoadPendingInquiries(model);
                                break;
                            default:
                                Response.Redirect("~/Reporting/SalesReports");
                                break;
                        }
                    }
                    else {
                        Response.Redirect("~/Reporting/SalesReports");
                    }
                    
                }
                else {
                    Response.Redirect("~/Login");
                }
        }

        private void LoadPendingInquiries(ReportViewerViewModel model)
        {
            if (this.reportDocument != null)
            {
                this.reportDocument.Close();
                this.reportDocument.Dispose();
            }
            reportDocument = new ReportDocument();
            DataTable param = new DataTable();
            DataRow dr;

            //Report path
            //string reportPath = Server.MapPath("~/Reports/Report/" + model.reportName + ".rpt");
            //reportDocument.Load(reportPath);
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("customer", typeof(string));
            param.Columns.Add("grp_cust", typeof(Int16));
            param.Columns.Add("grp_pc", typeof(Int16));
            param.Columns.Add("type", typeof(string));
            param.Columns.Add("refno", typeof(string));
            param.Columns.Add("status", typeof(Int16));

            dr = param.NewRow();
            dr["user"] = model.UserId;
            dr["period"] = "FROM " + model.frmDate.Date.ToString("dd/MMM/yyyy") + " TO " + model.todate.Date.ToString("dd/MMM/yyyy");// System.DateTime.Now.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = model.seleUserDefPro;//BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = model.fileName;
            dr["customer"] = model.Customer == "" ? "ALL" : model.Customer;
            dr["grp_cust"] = 0;
            dr["grp_pc"] = 1;
            dr["type"] = model.EnquiryType;
            dr["status"] = model.EnquiryStatus;
            dr["refno"] = model.EnquiryNumber;
            param.Rows.Add(dr);

            // Report connection
            _penenq.Database.Tables["ATS_INQUIRY"].SetDataSource(model.reportData);
            _penenq.Database.Tables["param"].SetDataSource(param);

            SalesReport.ReportSource = _penenq;
            SalesReport.RefreshReport();
        }

        private void LoadDailySalesSerEwise(ReportViewerViewModel model)
        {
            if (this.reportDocument != null)
            {
                this.reportDocument.Close();
                this.reportDocument.Dispose();
            }
            reportDocument = new ReportDocument();
            DataTable param = new DataTable();
            DataRow dr;

            //Report path
            //string reportPath = Server.MapPath("~/Reports/Report/" + model.reportName + ".rpt");
            //reportDocument.Load(reportPath);
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("customer", typeof(string));
            param.Columns.Add("grp_cust", typeof(Int16));
            param.Columns.Add("grp_pc", typeof(Int16));

            dr = param.NewRow();
            dr["user"] = model.UserId;
            dr["period"] = "FROM " + model.frmDate.Date.ToString("dd/MMM/yyyy") + " TO " + model.todate.Date.ToString("dd/MMM/yyyy");// System.DateTime.Now.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = model.seleUserDefPro;//BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = model.fileName;
            dr["customer"] = model.Customer == "" ? "ALL" : model.Customer;
            dr["grp_cust"] = 0;
            dr["grp_pc"] = 1;
            param.Rows.Add(dr);

            // Report connection
            _dsalesdtl.Database.Tables["D_SALES"].SetDataSource(model.reportData);
            _dsalesdtl.Database.Tables["param"].SetDataSource(param);

            SalesReport.ReportSource = _dsalesdtl;
            SalesReport.RefreshReport();
        }

        private void LoadDebetorStat(ReportViewerViewModel model)
        {
            if (this.reportDocument != null)
            {
                this.reportDocument.Close();
                this.reportDocument.Dispose();
            }
            reportDocument = new ReportDocument();
            DataTable param = new DataTable();
            DataRow dr;

            //Report path
            //string reportPath = Server.MapPath("~/Reports/Report/" + model.reportName + ".rpt");
            //reportDocument.Load(reportPath);
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("customer", typeof(string));
            param.Columns.Add("grp_cust", typeof(Int16));
            param.Columns.Add("grp_pc", typeof(Int16));

            dr = param.NewRow();
            dr["user"] = model.UserId;
            dr["period"] = "FROM " + model.frmDate.Date.ToString("dd/MMM/yyyy") + " TO " + model.todate.Date.ToString("dd/MMM/yyyy");// System.DateTime.Now.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = model.seleUserDefPro;//BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = model.fileName;
            dr["customer"] = model.Customer == "" ? "ALL" : model.Customer;
            dr["grp_cust"] = 0;
            dr["grp_pc"] = 1;
            param.Rows.Add(dr);

            // Report connection
            _debtsetl.Database.Tables["DEBT_SETT"].SetDataSource(model.reportData);
            _debtsetl.Database.Tables["param"].SetDataSource(param);

            SalesReport.ReportSource = _debtsetl;
            SalesReport.RefreshReport();
        }

        private void LoadCommision(ReportViewerViewModel model)
        {
            throw new NotImplementedException();
        }

        private void LoadReceiptDetails(ReportViewerViewModel model)
        {
            if (this.reportDocument != null)
            {
                this.reportDocument.Close();
                this.reportDocument.Dispose();
            }
            reportDocument = new ReportDocument();
            DataTable param = new DataTable();
            DataRow dr;

            //Report path
            //string reportPath = Server.MapPath("~/Reports/Report/" + model.reportName + ".rpt");
            //reportDocument.Load(reportPath);
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("customer", typeof(string));
            param.Columns.Add("grp_cust", typeof(Int16));
            param.Columns.Add("grp_pc", typeof(Int16));

            dr = param.NewRow();
            dr["user"] = model.UserId;
            dr["period"] = "FROM " + model.frmDate.Date.ToString("dd/MMM/yyyy") + " TO " + model.todate.Date.ToString("dd/MMM/yyyy");// System.DateTime.Now.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = model.seleUserDefPro;//BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = model.fileName;
            dr["customer"] = model.Customer == "" ? "ALL" : model.Customer;
            dr["grp_cust"] = 0;
            dr["grp_pc"] = 1;
            param.Rows.Add(dr);

            // Report connection
            _recdet.Database.Tables["REC_DTL"].SetDataSource(model.reportData);
            _recdet.Database.Tables["param"].SetDataSource(param);

            SalesReport.ReportSource = _recdet;
            SalesReport.RefreshReport();
        }
        private void LoadReportDailySales(ReportViewerViewModel model)
        {
            if (this.reportDocument != null)
            {
                this.reportDocument.Close();
                this.reportDocument.Dispose();
            }
            reportDocument = new ReportDocument();
            DataTable param = new DataTable();
            DataRow dr;

            //Report path
            //string reportPath = Server.MapPath("~/Reports/Report/" + model.reportName + ".rpt");
            //reportDocument.Load(reportPath);
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("customer", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("grp_cust", typeof(Int16));
            param.Columns.Add("grp_pc", typeof(Int16));            

            dr = param.NewRow();
            dr["user"] = model.UserId;
            dr["period"] = "FROM " + model.frmDate.Date.ToString("dd/MMM/yyyy") + " TO " + model.todate.Date.ToString("dd/MMM/yyyy");// System.DateTime.Now.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = model.seleUserDefPro;//BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = model.fileName;
            dr["customer"] = model.Customer == "" ? "ALL" : model.Customer;
            dr["itemcode"] = model.itemCd;
            dr["grp_cust"] = 0;
            dr["grp_pc"] = 1;
            param.Rows.Add(dr);

            // Report connection
            _dsales.Database.Tables["D_SALES"].SetDataSource(model.reportData);
            _dsales.Database.Tables["param"].SetDataSource(param);
            
            SalesReport.ReportSource = _dsales;
            SalesReport.RefreshReport();
        }
    }
}