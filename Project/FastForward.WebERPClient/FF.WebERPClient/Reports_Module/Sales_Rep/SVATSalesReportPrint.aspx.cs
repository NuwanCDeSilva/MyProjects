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
    public partial class SVATSalesReportPrint : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (hdnValidator.Value == "0")
                Display();
            else Response.Redirect(GlbMainPage);
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

                if (!string.IsNullOrEmpty(GlbReportUser)) { CrystalReportSource1.ReportDocument.SetParameterValue("user", GlbReportUser); }
                if (!string.IsNullOrEmpty(GlbReportHeading_1)) { CrystalReportSource1.ReportDocument.SetParameterValue("heading_1", GlbReportHeading_1); }
                if (!string.IsNullOrEmpty(GlbProfitCenter)) { CrystalReportSource1.ReportDocument.SetParameterValue("ProfitCenter", GlbProfitCenter); }
                if (!string.IsNullOrEmpty(GlbPeriod)) { CrystalReportSource1.ReportDocument.SetParameterValue("Period", GlbPeriod); }
                if (!string.IsNullOrEmpty(GlbDocType)) { CrystalReportSource1.ReportDocument.SetParameterValue("DocType", GlbDocType); }
                if (!string.IsNullOrEmpty(GlbDocSubType)) { CrystalReportSource1.ReportDocument.SetParameterValue("DocSubType", GlbDocSubType); }

                if (!string.IsNullOrEmpty(GlbSelectionFormula))
                {
                    CrystalReportSource1.ReportDocument.RecordSelectionFormula = GlbSelectionFormula;
                }

               

                CrystalReportViewer1.ReportSource = CrystalReportSource1;

                CrystalReportViewer1.EnableDatabaseLogonPrompt = false;
                CrystalReportViewer1.EnableParameterPrompt = false;

                CrystalReportSource1.ReportDocument.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;

                CrystalReportViewer1.Visible = true;
                CrystalReportViewer1.HasPrintButton = true;
                CrystalReportViewer1.PrintMode = PrintMode.Pdf;
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
