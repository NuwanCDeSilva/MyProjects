using CrystalDecisions.CrystalReports.Engine;
using FastForward.WebAbansTours.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.WebAbansTours.Print_Module.Print_Viwer
{
    public partial class CostingPrintViwer : System.Web.UI.Page
    {
      
        public FastForward.WebAbansTours.Print_Module.Reports.CostingFormat_Report_New _costingFormat = new Reports.CostingFormat_Report_New();

        CrystalDecisions.CrystalReports.Engine.ReportDocument reportDocument = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string userId = Session["UserID"] as string;
            string company = Session["UserCompanyCode"] as string;
            string userDefPro = Session["UserDefProf"] as string;
            string userDefLoc = Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string costNo = Session["CostSheetNumber"] as string;
                if (costNo != "")
                {
                    DataTable CostingReports = new DataTable();
                    BaseController baseCon = new BaseController();
                    ReportDocument crystalReport = new ReportDocument();

                    CostingReports = baseCon.CHNLSVC.Tours.Get_CostingFormat(costNo);

                    if (this.reportDocument != null)
                    {
                        this.reportDocument.Close();
                        this.reportDocument.Dispose();
                    }
                    string date = DateTime.Now.ToString("ddMMMyyyy");
                    string time = DateTime.Now.ToString("hhmmss");
                    string ID = Convert.ToString(Session["UserID"]);
                    string FileName = time + ID;
                    _costingFormat.Database.Tables["Costing_format"].SetDataSource(CostingReports);

                    CostingReport.ReportSource = _costingFormat;
                    CostingReport.RefreshReport();
                }
                else {
                    Response.Redirect("~/CostingSheet");
                }
               
            }
            else
            {
                Response.Redirect("~/Login");
            }
           
        }

    }
}