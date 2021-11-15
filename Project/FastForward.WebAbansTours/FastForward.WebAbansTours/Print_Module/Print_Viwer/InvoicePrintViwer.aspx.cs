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
    public partial class InvoicePrintViwer : System.Web.UI.Page
    {
        public FastForward.WebAbansTours.Print_Module.Reports.InvoiceCrystalReport _invocingFormat = new Reports.InvoiceCrystalReport();

        CrystalDecisions.CrystalReports.Engine.ReportDocument reportDocument = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string userId = Session["UserID"] as string;
            string company = Session["UserCompanyCode"] as string;
            string userDefPro = Session["UserDefProf"] as string;
            string userDefLoc = Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string invNo = Session["invoiceNum"] as string;
                if (invNo != "")
                {
                    DataTable InvoicingReports = new DataTable();
                    DataTable param = new DataTable("tbl");



                    BaseController baseCon = new BaseController();
                    ReportDocument crystalReport = new ReportDocument();
                    InvoicingReports = baseCon.CHNLSVC.Tours.GetInvoiceDetailsForPrint(invNo, userDefPro);
                    decimal total = 0;
                    if (InvoicingReports.Rows.Count>0)
                    {
                        foreach(DataRow dr in  InvoicingReports.Rows)
                        {
                            total = total + Convert.ToDecimal(dr["spd_amt"].ToString());
                        }
                    }
                    param.Columns.Add("total", typeof(String));

                    param.Rows.Add(total.ToString("N2"));

                    if (this.reportDocument != null)
                    {
                        this.reportDocument.Close();
                        this.reportDocument.Dispose();
                    }



                    _invocingFormat.Database.Tables["DataTable1"].SetDataSource(InvoicingReports);
                    _invocingFormat.Database.Tables["DataTable3"].SetDataSource(param);

                    InvoicingReport.ReportSource = _invocingFormat;
                    InvoicingReport.RefreshReport();
                    InvoicingReport.DisplayGroupTree = false;
                }
                else
                {
                    Response.Redirect("~/Invoicing");
                }

            }
            else
            {
                Response.Redirect("~/Login");
            }

        }
    }
}