using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FF.BusinessObjects.Sales;
using CrystalDecisions.Shared;
using FF.BusinessObjects.Tours;
using CrystalDecisions.CrystalReports.Engine;

namespace FF.AbansTours
{
    public partial class InvoicePrint2 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindReport();
            }
        }
        //create byrukshan
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
        private void ShowDiv()
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "DisplayDiv();", true);
        }
        private void BindReport()
        {
           // DataTable salesDetails = new DataTable();

           // salesDetails = CHNLSVC.Tours.GetInvoiceDetailsForPrint("TINVO00010");

           // ReportDocument crystalReport = new ReportDocument();
           // crystalReport.Load(Server.MapPath("Reports_Module/Sales_Rep/InvoiceCrystalReport.rpt"));
           // crystalReport.Database.Tables["DataTable1"].SetDataSource(salesDetails);

           // //CrystalReportSource1.ReportDocument.Refresh();
           //// crystalReport.SetDataSource(salesDetails);
           
           // CrystalReportViewer1.ReportSource = crystalReport;
           // CrystalReportSource1.ReportDocument.Refresh();
           // //CrystalReportViewer1.DisplayGroupTree = false;


            DataTable salesDetails = new DataTable();

            salesDetails = CHNLSVC.Tours.GetInvoiceDetailsForPrint("TINVO00010", Session["UserCompanyCode"].ToString());
            if ((salesDetails.Rows.Count > 0))
            {


                ReportDocument crystalReport = new ReportDocument();
                crystalReport.Load(Server.MapPath("Reports_Module\\Sales_Rep\\InvoiceCrystalReport.rpt"));
                crystalReport.Database.Tables["DataTable1"].SetDataSource(salesDetails);
                crystalReport.SetDataSource(salesDetails);
                CrystalReportViewer1.ReportSource = crystalReport;
                CrystalReportViewer1.RefreshReport();
                //CrystalReportViewer1.PrintMode = ;                // CrystalReportViewer1.GroupTreeStyle=;
            }
            else
            {
                DisplayMessages("Wrong Invoice No.");
                CrystalReportViewer1.ReportSource = null;
                HttpContext.Current.Response.Redirect("InvoicePrint.aspx");
            }
        }
        protected void SuperBtn_Click(object sender, EventArgs e)
        {
            if (!(txtInoviceNo.Text == ""))
            {
                ShowDiv();
                DataTable salesDetails = new DataTable();
                salesDetails = CHNLSVC.Tours.GetInvoiceDetailsForPrint(txtInoviceNo.Text, Session["UserCompanyCode"].ToString());
                if ((salesDetails.Rows.Count > 0))
                {
                    foreach (DataRow dr in salesDetails.Rows)
                    {
                        lblCName.Text = dr[0].ToString();
                        lblCAddress1.Text = dr[1].ToString();
                        lblCAddress2.Text = dr[2].ToString();
                        lblCTel.Text = dr[3].ToString();
                        lblCFax.Text = dr[4].ToString();
                        lblCEmail.Text = dr[5].ToString();
                        lblCWeb.Text = dr[6].ToString();
                        lblCuName.Text = dr[7].ToString();
                        lblCUAddress1.Text = dr[8].ToString();
                        lblCUAddress2.Text = dr[9].ToString();
                        lblRefNo.Text = dr[10].ToString();
                        lblInvoiceDate.Text = dr[11].ToString();
                        lblInvoiceNo.Text = dr[12].ToString();
                        lblVatNo.Text = dr[13].ToString();
                    }
                    GridView1.DataSource = salesDetails;
                    GridView1.DataBind();

                    int sum = 0;
                    foreach (DataRow dr in salesDetails.Rows)
                    {
                        sum = sum + Convert.ToInt32(dr["sii_tot_amt"]);
                    }
                    Label2.Text = sum.ToString();
                }
                else
                {
                    DisplayMessages("Wrong Invoice No.");
                    HttpContext.Current.Response.Redirect("InvoicePrint2.aspx");
                }
            }
            else
            {
                DisplayMessages("Please Fill Invoice No.");
            }
        }

        protected void btnprintD_Click(object sender, EventArgs e)
        {
           // this.BindReport();
            DataTable salesDetails = new DataTable();

            salesDetails = CHNLSVC.Tours.GetInvoiceDetailsForPrint(txtInoviceNo.Text, Session["UserCompanyCode"].ToString());
            CrystalReportSource1.ReportDocument.DataSourceConnections.Clear();
            CrystalReportSource1.ReportDocument.Database.Tables["DataTable1"].SetDataSource(salesDetails);
            CrystalReportSource1.ReportDocument.Refresh();
            //CrystalReportSource1.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, HttpContext.Current.Response, true, "test");
            CrystalReportSource1.ReportDocument.PrintToPrinter(1, false, 0, 0);
           
        }

        protected void btnprintL_Click(object sender, EventArgs e)
        {
            List<invoiceCenter> range = CHNLSVC.Tours.InvoiceDeatilsForPrintList(txtInoviceNo.Text);

            CrystalReportSource2.ReportDocument.DataSourceConnections.Clear();
            CrystalReportSource2.ReportDocument.Database.Tables["DataTable2"].SetDataSource(range);
            CrystalReportSource2.ReportDocument.Refresh();
            CrystalReportSource2.ReportDocument.PrintToPrinter(1, false, 0, 0);
        }
    }
}