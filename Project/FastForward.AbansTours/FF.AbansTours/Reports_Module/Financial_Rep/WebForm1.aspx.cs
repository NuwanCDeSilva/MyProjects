using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FF.AbansTours.Reports_Module.Financial_Rep
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Marks");
            DataRow _ravi = dt.NewRow();
            _ravi["Name"] = "ravi";
            _ravi["Marks"] = "500";
            dt.Rows.Add(_ravi);

            BasePage basepage = new BasePage();
            crystalReport.Load(Server.MapPath("CrystalReport1.rpt"));
            crystalReport.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = crystalReport;
            //cryrCostingReport.RefreshReport();
            //CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
        }
    }
}