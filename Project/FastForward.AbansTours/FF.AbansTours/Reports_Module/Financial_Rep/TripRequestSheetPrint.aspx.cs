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
    public partial class TripRequestSheetPrint : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        DataTable TripRequestSheet = new DataTable();
        BasePage basepage = new BasePage();

        protected void Page_Load(object sender, EventArgs e)
        {
            searchDeatils();
        }

        private void searchDeatils()
        {
            if (Session["EnquiryID"] == null)
            {
                return;
            }
            TripRequestSheet = basepage.CHNLSVC.Tours.Get_triprequest(Session["EnquiryID"].ToString());

            //pdf
            if ((Session["EnquiryID"]) != null)
            {
                CrystalReportSource1.ReportDocument.Database.Tables["TripRequestSheet"].SetDataSource(TripRequestSheet);
                CrystalReportSource1.ReportDocument.Refresh();

                CrystalReportViewer1.ReportSource = CrystalReportSource1;
                CrystalReportViewer1.RefreshReport();
                CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('Wrong receipt No.');", true);
            }
        }        
    }
}