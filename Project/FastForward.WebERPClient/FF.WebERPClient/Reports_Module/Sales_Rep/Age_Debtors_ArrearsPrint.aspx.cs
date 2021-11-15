using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;

namespace FF.WebERPClient.Reports_Module.Sales_Rep
{
    public partial class Age_Debtors_ArrearsPrint : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (hdnValidator.Value == "0")
                Display();
            else Response.Redirect(GlbMainPage);

        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            this.CrystalReportViewer1.Dispose();
            this.CrystalReportViewer1 = null;
            this.CrystalReportSource1.Dispose();
            this.CrystalReportSource1 = null;
            GC.Collect();
        }

        //protected void btnBack_Click(object sender, EventArgs e)
        //{
        //   Response.Redirect("~/Default.aspx", true);
        //}

        public void Display()
        {

            try
            {

                ConnectionInfo connectionInfo = new ConnectionInfo();

                Report Cr = new Report();
                Cr.FileName = Convert.ToString(GlbReportPath);
                Cr.FileName = Convert.ToString(GlbReportMapPath);
                CrystalReportSource1 = new CrystalReportSource();


                //by darshana
                CrystalReportSource1.Report = Cr;
                CrystalReportSource1.ReportDocument.SetDatabaseLogon(System.Configuration.ConfigurationManager.AppSettings["DBUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["DBPassword"].ToString());
                //CrystalReportSource1.ReportDocument.SetDatabaseLogon("EMS", "EMS123");

                CrystalReportSource1.ReportDocument.SetParameterValue("user", GlbReportUser);
                CrystalReportSource1.ReportDocument.SetParameterValue("AsAtDate", GlbAsatDate);

                //CrystalReportSource1.ReportDocument.SetParameterValue("p_asatdate", GlbAsatDate);
                //CrystalReportSource1.ReportDocument.SetParameterValue("p_com", GlbUserComCode);
                //CrystalReportSource1.ReportDocument.SetParameterValue("p_pc", GlbProfitCenter);
                //CrystalReportSource1.ReportDocument.SetParameterValue("p_user", GlbReportUser);
                //CrystalReportSource1.ReportDocument.SetParameterValue("p_scheme", GlbRecType);

                CrystalReportSource1.ReportDocument.SetParameterValue("ReportCompany", GlbReportCompany);
                CrystalReportSource1.ReportDocument.SetParameterValue("ReportCompAddr", GlbReportCompanyAddr);


                //RptDoc.OpenSubreport("aa");
                //RptDoc.SetDatabaseLogon(System.Configuration.ConfigurationManager.AppSettings["DBUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["DBPassword"].ToString());
                //RptDoc.SetParameterValue(0, "RABT");
                //RptDoc.SetParameterValue(1, "ADMIN");



                // Get the ReportObject by name and cast it as a 
                // SubreportObject.


                //foreach (ReportObject repOp in RptDoc.ReportDefinition.ReportObjects)
                //{
                //    if (repOp.Kind == ReportObjectKind.SubreportObject)
                //    {
                //        string SubRepName = ((SubreportObject)repOp).SubreportName;
                //        ReportDocument subRepDoc = RptDoc.Subreports[SubRepName];
                //        SubreportObject subReport = (SubreportObject)repOp; 
                //        ReportDocument subDocument = subReport.OpenSubreport(subReport.SubreportName); 
                //        subRepDoc.SetDatabaseLogon(System.Configuration.ConfigurationManager.AppSettings["DBUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["DBPassword"].ToString());
                //        subRepDoc.SetParameterValue(0, "RABT");
                //        subRepDoc.SetParameterValue(1, "ADMIN");
                //    }
                //}

                CrystalReportViewer1.ReportSource = CrystalReportSource1;

                CrystalReportViewer1.EnableDatabaseLogonPrompt = false;
                CrystalReportViewer1.EnableParameterPrompt = false;

                CrystalReportSource1.ReportDocument.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;

                CrystalReportViewer1.Visible = true;
                CrystalReportViewer1.HasPrintButton = true;
                CrystalReportViewer1.PrintMode = PrintMode.Pdf;
                //CrystalReportViewer1.RefreshReport();       //kapila 20/11/2012
            }
            catch (Exception er)
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = '../Default.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }


        }




        private void SetDBLogonForReport(ConnectionInfo connectionInfo)
        {
            TableLogOnInfos tableLogOnInfos = CrystalReportViewer1.LogOnInfo;
            foreach (TableLogOnInfo tableLogOnInfo in tableLogOnInfos)
            {
                tableLogOnInfo.ConnectionInfo = connectionInfo;

            }

        }

    }
}
