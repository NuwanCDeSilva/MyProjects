using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.ADMIN
{
    public partial class Report : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            BindReport();
        }

        protected void ExportPDF(object sender, EventArgs e)
        {
            DataSetCustomer ds = new DataSetCustomer();
            DataTable dt = ds.Tables.Add("DT");
            dt.Columns.Add("CusID", Type.GetType("System.Int32"));
            dt.Columns.Add("CusName", Type.GetType("System.String"));
            dt.Columns.Add("CusAddress", Type.GetType("System.String"));
            dt.Columns.Add("CusCountry", Type.GetType("System.String"));

            DataRow r;
            int i = 0;
            for (i = 0; i <= 10; i++)
            {
                r = dt.NewRow();
                r["CusID"] = i;
                r["CusName"] = "CusName" + i;
                r["CusAddress"] = "Hanwella" + i;
                r["CusCountry"] = "Sri Lanka" + i;
                dt.Rows.Add(r);
            }

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("CR.rpt"));
            crystalReport.SetDataSource(ds.Tables[1]);

            ExportFormatType formatType = ExportFormatType.NoFormat;
            switch (rbFormat.SelectedItem.Value)
            {
                case "Word":
                    formatType = ExportFormatType.WordForWindows;
                    break;
                case "PDF":
                    formatType = ExportFormatType.PortableDocFormat;
                    break;
                case "Excel":
                    formatType = ExportFormatType.Excel;
                    break;
                case "CSV":
                    formatType = ExportFormatType.CharacterSeparatedValues;
                    break;
            }
            crystalReport.ExportToHttpResponse(formatType, Response, true, "Crystal");
            Response.End();

        }

        private void BindReport()
        {
            DataSetCustomer ds = new DataSetCustomer();
            DataTable dt = ds.Tables.Add("DT");
            dt.Columns.Add("CusID", Type.GetType("System.Int32"));
            dt.Columns.Add("CusName", Type.GetType("System.String"));
            dt.Columns.Add("CusAddress", Type.GetType("System.String"));
            dt.Columns.Add("CusCountry", Type.GetType("System.String"));

            DataRow r;
            int i = 0;
            for (i = 0; i <= 10; i++)
            {
                r = dt.NewRow();
                r["CusID"] = i;
                r["CusName"] = "CusName" + i;
                r["CusAddress"] = "Hanwella" + i;
                r["CusCountry"] = "Sri Lanka" + i;
                dt.Rows.Add(r);
            }

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("CR.rpt"));
            crystalReport.SetDataSource(ds.Tables[1]);
            CrystalReportViewer1.ReportSource = crystalReport;
        }

    }
}