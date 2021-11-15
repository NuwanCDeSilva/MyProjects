using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
    public partial class ReceiptPrint : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();

        DataTable salesDetails = new DataTable();
        DataTable ProfitCenter = new DataTable();
        DataTable mst_rec_tp = new DataTable();

        BasePage basepage = new BasePage();

        protected void Page_Load(object sender, EventArgs e)
        {
            searchDeatils();
        }
        
        private void searchDeatils()
        {
            if (Session["receiptno"] == null)
            {
                return;
            }

            salesDetails = basepage.CHNLSVC.Sales.GetReceiptTBS(Session["receiptno"].ToString());
            ProfitCenter = basepage.CHNLSVC.Sales.GetProfitCenterTable(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            mst_rec_tp = basepage.CHNLSVC.Sales.GetReceiptType(salesDetails.Rows[0]["SIR_RECEIPT_TYPE"].ToString());

            //Session["salesDetails"] = salesDetails;
            //Session["ProfitCenter"] = ProfitCenter;
            //Session["mst_rec_tp"] = mst_rec_tp;

            //pdf
            if ((Session["receiptno"]) != null)
            {
                //CrystalReportSource1.ReportDocument.DataSourceConnections.Clear();
                CrystalReportSource1.ReportDocument.Database.Tables["salesDetails"].SetDataSource(salesDetails);
                CrystalReportSource1.ReportDocument.Database.Tables["ProfitCenter"].SetDataSource(ProfitCenter);
                CrystalReportSource1.ReportDocument.Database.Tables["mst_rec_tp"].SetDataSource(mst_rec_tp);
                CrystalReportSource1.ReportDocument.Refresh();

                //string date = DateTime.Now.ToString("ddMMyyyy");
                //string time = DateTime.Now.ToString("hhmmss");
                //string ID = Convert.ToString(Session["UserID"]);
                //string FileName = time + ID;
                ////string Path = Server.MapPath("Temp_report\\");
                //string Path = "D:\\Report_temp\\";
                //string NewPath = Path + date + "_" + time + "_" + ID + ".pdf";
                //CrystalReportSource1.ReportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, NewPath);

                //ReportDocument reportDocument = new ReportDocument();
                //reportDocument.Load(Server.MapPath("receiptPrint_Report.rpt"));
                //reportDocument.Database.Tables["salesDetails"].SetDataSource(salesDetails);
                //reportDocument.Database.Tables["ProfitCenter"].SetDataSource(ProfitCenter);
                //reportDocument.Database.Tables["mst_rec_tp"].SetDataSource(mst_rec_tp);

                //crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "printdvg");
                CrystalReportViewer1.ReportSource = CrystalReportSource1;
                CrystalReportViewer1.RefreshReport();
                CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;

                //Response.Clear();
                //string filePath = NewPath;
                //Response.ContentType = "application/pdf";
                //Response.WriteFile(filePath);

                //Response.End();
            }

            //if ((salesDetails.Rows.Count > 0))
            //{
            //    foreach (DataRow dr in salesDetails.Rows)
            //    {
            //        lblCD.Text = dr["SAR_DEBTOR_CD"].ToString();
            //        lblName.Text = dr["SAR_DEBTOR_NAME"].ToString();
            //        lblAddress.Text = dr["SAR_DEBTOR_ADD_1"].ToString();
            //        lblAddress2.Text = dr["SAR_DEBTOR_ADD_2"].ToString();
            //        lblTel.Text = dr["SAR_MOB_NO"].ToString();
            //        lblReceiptNo.Text = dr["SAR_RECEIPT_NO"].ToString();
            //        lblAmount.Text = dr["SAR_TOT_SETTLE_AMT"].ToString();
            //        lblReceiptdate.Text = dr["SAR_RECEIPT_DATE"].ToString();

            //    }
            //}

            //if ((ProfitCenter.Rows.Count > 0))
            //{
            //    foreach (DataRow dr in salesDetails.Rows)
            //    {

            //    }
            //}

            //if ((salesDetails.Rows.Count > 0))
            //{
            //    ReportDocument crystalReport = new ReportDocument();
            //    crystalReport.Load(Server.MapPath("/Reports_Module/Financial_Rep/receiptPrint_Report.rpt"));

            //    crystalReport.Database.Tables["salesDetails"].SetDataSource(salesDetails);
            //    crystalReport.Database.Tables["ProfitCenter"].SetDataSource(ProfitCenter);
            //    crystalReport.Database.Tables["mst_rec_tp"].SetDataSource(mst_rec_tp);

            //    //crvReceiptPrint.ReportSource = crystalReport;

            //    //crvReceiptPrint.ToolPanelView = ToolPanelViewType.None;
            //    //crystalReport.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
            //    //crystalReport.PrintOptions.PaperSize = PaperSize.PaperA4;
            //    //crystalReport.PrintToPrinter(1, false, 0, 1);

            //    //Clear_Data();
            //}
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('Wrong receipt No.');", true);
                //HttpContext.Current.Response.Redirect("ReceiptEnrty.aspx");
            }
        }

        //protected void btnprintL_Click(object sender, EventArgs e)
        //{
        //    //List<invoiceCenter> range = CHNLSVC.Tours.InvoiceDeatilsForPrintList("TINVO00010");
        //    //CrystalReportSource2.ReportDocument.DataSourceConnections.Clear();
        //    //CrystalReportSource2.ReportDocument.Database.Tables["DataTable2"].SetDataSource(range);
        //    //CrystalReportSource2.ReportDocument.Refresh();
        //    //CrystalReportSource2.ReportDocument.PrintToPrinter(1, false, 0, 0);

        //    ReportDocument crystalReport = new ReportDocument();
        //    crystalReport.Load(Server.MapPath("/Reports_Module/Financial_Rep/receiptPrint_Report.rpt"));

        //    crystalReport.Database.Tables["salesDetails"].SetDataSource((DataTable)Session["salesDetails"]);
        //    crystalReport.Database.Tables["ProfitCenter"].SetDataSource((DataTable)Session["ProfitCenter"]);
        //    crystalReport.Database.Tables["mst_rec_tp"].SetDataSource((DataTable)Session["mst_rec_tp"]);

        //    CrystalReportSource1.ReportDocument.Database.Tables["salesDetails"].SetDataSource((DataTable)Session["salesDetails"]);
        //    CrystalReportSource1.ReportDocument.Database.Tables["ProfitCenter"].SetDataSource((DataTable)Session["ProfitCenter"]);
        //    CrystalReportSource1.ReportDocument.Database.Tables["mst_rec_tp"].SetDataSource((DataTable)Session["mst_rec_tp"]);

        //    crystalReport.PrintToPrinter(1, false, 0, 1);

        //}
    }
}