using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;

namespace FF.WebERPClient.Reports_Module.HP_Rep
{
    public partial class HPAllDueSummaryPrint : BasePage
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", true);
        }

        public void Display()
        {
            try
            {

                ConnectionInfo connectionInfo = new ConnectionInfo();

                Report Cr = new Report();
                Cr.FileName = Convert.ToString(GlbReportPath);
                CrystalReportSource1 = new CrystalReportSource();

                CrystalReportSource1.Report = Cr;
                //remove comment
                CrystalReportSource1.ReportDocument.SetDatabaseLogon(System.Configuration.ConfigurationManager.AppSettings["DBUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["DBPassword"].ToString());
                //CrystalReportSource1.ReportDocument.SetDatabaseLogon("EMS","EMS123");
                CrystalReportSource1.ReportDocument.SetParameterValue("P_USER", GlbUserName);
                CrystalReportSource1.ReportDocument.SetParameterValue("P_COM", GlbCompany);
                CrystalReportSource1.ReportDocument.SetParameterValue("P_LASTDAY", GlbCLS_BAL_DueDate);

                CrystalReportViewer1.ReportSource = CrystalReportSource1;

                CrystalReportViewer1.EnableDatabaseLogonPrompt = false;
                CrystalReportViewer1.EnableParameterPrompt = false;

                CrystalReportViewer1.Visible = true;

                CrystalReportSource1.ReportDocument.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;

                CrystalReportViewer1.Visible = true;
                CrystalReportViewer1.HasPrintButton = true;
                CrystalReportViewer1.PrintMode = PrintMode.Pdf;
                //CrystalReportViewer1.RefreshReport();       //kapila 20/11/2012
            }
            catch (Exception er)
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                string Msg = "<script>alert('System Error Occur. : " + er.Message + "');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
        }

    }
}