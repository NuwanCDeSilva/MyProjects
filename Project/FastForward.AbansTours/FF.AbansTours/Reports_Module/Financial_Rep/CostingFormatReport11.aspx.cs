using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;

namespace FF.AbansTours.Reports_Module.Financial_Rep
{
    public partial class CostingFormatReport11 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable CostingReport = new DataTable();
            BasePage basepage = new BasePage();

            ReportDocument crystalReport = new ReportDocument();

            //if ((Session["CostSheetNumber"]) != null)
            //{
            //    CostingReport = basepage.CHNLSVC.Tours.Get_CostingFormat(Session["CostSheetNumber"].ToString());
            //    //CostingReport = basepage.CHNLSVC.Tours.Get_CostingFormat("ATS/001/001-CTSHT-000008");

            //    //crystalReport.Load(Server.MapPath("/Reports_Module/Financial_Rep/CostingFormat_Report.rpt"));
            //    crystalReport.Load(Server.MapPath("CostingFormat_Report.rpt"));
            //    crystalReport.SetDataSource(CostingReport);
            //    CrystalReportViewer1.ReportSource = crystalReport;
            //    //cryrCostingReport.RefreshReport();
            //    CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;

            //}
            //else
            //{
            //    CrystalReportViewer1.Dispose();
            //}

            //pdf
            if ((Session["CostSheetNumber"]) != null)
            {

                CostingReport = basepage.CHNLSVC.Tours.Get_CostingFormat(Session["CostSheetNumber"].ToString());

                //crystalReport.Load(Server.MapPath("CostingFormat_Report.rpt"));
                //crystalReport.Load(Server.MapPath("CostingFormat_Report.rpt"));
                //crystalReport.SetDataSource(CostingReport);
                CrystalReportSource1.ReportDocument.DataSourceConnections.Clear();
                CrystalReportSource1.ReportDocument.Database.Tables["Costing_format"].SetDataSource(CostingReport);
                CrystalReportSource1.ReportDocument.Refresh();
                //CrystalReportSource1.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, HttpContext.Current.Response, true, "CostingFormatReport");

                //CrystalReport1 objRpt = new CrystalReport1();
                //objRpt.SetDataSource(ds.Tables[0]);
                //CrystalReportSource1.ReportDocument.DataSourceConnections.Clear();
                //CrystalReportSource1.ReportDocument.Database.Tables["DataTable1"].SetDataSource(ds.Tables[0]);
                //CrystalReportSource1.ReportDocument.Refresh();
                // CrystalReportSource1.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, HttpContext.Current.Response, true, "Report");

                string date = DateTime.Now.ToString("ddMMMyyyy");
                string time = DateTime.Now.ToString("hhmmss");
                string ID = Convert.ToString(Session["UserID"]);
                string FileName = time + ID;
                //string Path = Server.MapPath("Temp_report\\");
                string Path = "D:\\Report_temp\\";
                string NewPath = Path + date + "_" + time + "_" + ID + ".pdf";
                //CrystalReportSource1.ReportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, NewPath);

                CrystalReportSource1.ReportDocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "RoleMenuPrivilegesReport");

                Response.Clear();
                string filePath = NewPath;
                Response.ContentType = "application/pdf";
                //Response.TransmitFile(filePath);
                Response.WriteFile(filePath);

                Response.End();
            }            

        }
    }
}