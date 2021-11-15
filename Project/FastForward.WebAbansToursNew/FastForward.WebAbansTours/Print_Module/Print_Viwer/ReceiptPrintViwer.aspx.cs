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
    public partial class ReceiptPrintViwer : System.Web.UI.Page
    {
        public FastForward.WebAbansTours.Print_Module.Reports.receiptPrint_Report _ReceiptFormat = new Reports.receiptPrint_Report();

        CrystalDecisions.CrystalReports.Engine.ReportDocument reportDocument = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string userId = Session["UserID"] as string;
            string company = Session["UserCompanyCode"] as string;
            string userDefPro = Session["UserDefProf"] as string;
            string userDefLoc = Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string recNo = Session["ReceiptNumber"] as string;
                if (recNo != "")
                {
                    DataTable salesDetails = new DataTable();
                    DataTable ProfitCenter = new DataTable();
                    DataTable mst_rec_tp = new DataTable();
                    DataTable mst_rec_dtl = new DataTable();
                    BaseController baseCon = new BaseController();
                    ReportDocument crystalReport = new ReportDocument();

                    salesDetails = baseCon.CHNLSVC.Sales.GetReceiptTBS(Session["ReceiptNumber"].ToString());
                    ProfitCenter = baseCon.CHNLSVC.Sales.GetProfitCenterTable(company, userDefPro);
                    mst_rec_tp = baseCon.CHNLSVC.Sales.GetReceiptType(salesDetails.Rows[0]["SIR_RECEIPT_TYPE"].ToString());
                    mst_rec_dtl = baseCon.CHNLSVC.Sales.GetReceiptPayDetails(Session["ReceiptNumber"].ToString());
                    if (this.reportDocument != null)
                    {
                        this.reportDocument.Close();
                        this.reportDocument.Dispose();
                    }
                    _ReceiptFormat.Database.Tables["salesDetails"].SetDataSource(salesDetails);
                    _ReceiptFormat.Database.Tables["ProfitCenter"].SetDataSource(ProfitCenter);
                    _ReceiptFormat.Database.Tables["mst_rec_tp"].SetDataSource(mst_rec_tp);
                    foreach (object repOp in _ReceiptFormat.ReportDefinition.ReportObjects)
                    {
                        string _s = repOp.GetType().ToString();
                        if (_s.ToLower().Contains("subreport"))
                        {
                            SubreportObject _cs = (SubreportObject)repOp;
                            if (_cs.SubreportName == "st_rct_dtl")
                            {
                                ReportDocument subRepDoc = _ReceiptFormat.Subreports[_cs.SubreportName];
                                subRepDoc.Database.Tables["mst_rec_dtl"].SetDataSource(mst_rec_dtl);
                            }
                        }
                    }                    

                    ReceiptReport.ReportSource = _ReceiptFormat;
                    ReceiptReport.RefreshReport();
                }
                else
                {
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