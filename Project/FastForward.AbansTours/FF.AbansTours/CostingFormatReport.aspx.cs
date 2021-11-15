using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;

namespace FF.AbansTours
{
    public partial class CostingFormatReport : System.Web.UI.Page
    {
        BasePage basepage = new BasePage();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable CostingReport = new DataTable();

            if ((Session["CostSheetNumber"]) != null)
            {
                CostingReport = basepage.CHNLSVC.Tours.Get_CostingFormat(Session["CostSheetNumber"].ToString());
                //CostingReport = basepage.CHNLSVC.Tours.Get_CostingFormat("ATS/001/001-CTSHT-000008");
                ReportDocument crystalReport = new ReportDocument();
                //crystalReport.Load(Server.MapPath("/Reports_Module/Financial_Rep/CostingFormat_Report.rpt"));
                crystalReport.Load(Server.MapPath("CostingFormat_Report.rpt"));
                crystalReport.SetDataSource(CostingReport);
                cryrCostingReport.ReportSource = crystalReport;
                cryrCostingReport.RefreshReport();
                cryrCostingReport.ToolPanelView = ToolPanelViewType.None;
            }         

            //ReportDocument crystalReport = new ReportDocument();
            //crystalReport.Load(Server.MapPath("/Reports_Module/Financial_Rep/CostingFormat_Report.rpt"));
            //crystalReport.SetDataSource(CostingReport);
            //cryrCostingReport.ReportSource = crystalReport;

            //cryrCostingReport.ToolPanelView = ToolPanelViewType.None;
        }
    }
}