using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;

namespace FF.AbansTours
{
    public partial class InvoicePrint : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // Print();
        }
        //rukshan
        private void Print()
        {
            DataTable salesDetails = new DataTable();

            salesDetails = CHNLSVC.Tours.GetInvoiceDetailsForPrint(txtInoviceNo.Text, Session["UserCompanyCode"].ToString());
            if ((salesDetails.Rows.Count > 0))
            {


                ReportDocument crystalReport = new ReportDocument();
                crystalReport.Load(Server.MapPath("Reports_Module\\Sales_Rep\\InvoiceCrystalReport.rpt"));
                crystalReport.SetDataSource(salesDetails);
                CrystalReportViewer1.ReportSource = crystalReport;
                //CrystalReportViewer1.RefreshReport();
               // CrystalReportViewer1.GroupTreeStyle=;
            }
            else
            {
                DisplayMessages("Wrong Invoice No.");
                CrystalReportViewer1.ReportSource = null;
                HttpContext.Current.Response.Redirect("InvoicePrint.aspx");
            }
        }
        private void DisplayMessages(string message)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
            }
        }
        protected void SuperBtn_Click(object sender, EventArgs e)
        {
            if (!(txtInoviceNo.Text == ""))
            {
                Print();
            }
            else
            {
                DisplayMessages("Please Fill Invoice No.");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
        }

      
    }
}