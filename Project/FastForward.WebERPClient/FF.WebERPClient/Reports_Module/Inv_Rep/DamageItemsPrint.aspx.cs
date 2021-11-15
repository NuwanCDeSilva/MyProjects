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

namespace FF.WebERPClient.Reports_Module.Inv_Rep
{
    public partial class DamageItemsPrint : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
       

            if (hdnValidator.Value == "0")
            {
                Display();
            }
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
            //Response.Redirect(Convert.ToString(GlbMainPage));
            Response.Redirect("~/Default.aspx", false);

        }

        public void Display()
        {
            try
            {

                ConnectionInfo connectionInfo = new ConnectionInfo();

                Report Cr = new Report();
                Cr.FileName = Convert.ToString(GlbReportPath);
                Cr.FileName = Convert.ToString(GlbReportMapPath);
                CrystalReportSource1 = new CrystalReportSource();

                //by Nadeeka
                CrystalReportSource1.Report = Cr;
               CrystalReportSource1.ReportDocument.SetDatabaseLogon(System.Configuration.ConfigurationManager.AppSettings["DBUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["DBPassword"].ToString());
               

                CrystalReportSource1.ReportDocument.SetParameterValue("IN_FROMDATE", System.DateTime.Now);
                CrystalReportSource1.ReportDocument.SetParameterValue("IN_TODATE", System.DateTime.Now);
                CrystalReportSource1.ReportDocument.SetParameterValue("IN_USER_ID", GlbReportUser);
                CrystalReportSource1.ReportDocument.SetParameterValue("IN_LOCATION_CODE", GlbLocation);
                CrystalReportSource1.ReportDocument.SetParameterValue("IN_COMPANY", GlbCompany);
                CrystalReportSource1.ReportDocument.SetParameterValue("in_Brand", GlbBrand);
                CrystalReportSource1.ReportDocument.SetParameterValue("in_Model", GlbModel);
                CrystalReportSource1.ReportDocument.SetParameterValue("in_Itemcode", GlbItemCode);
                CrystalReportSource1.ReportDocument.SetParameterValue("in_Itemcat1", GlbItemCat1);
                CrystalReportSource1.ReportDocument.SetParameterValue("in_Itemcat2", GlbItemCat2);
                CrystalReportSource1.ReportDocument.SetParameterValue("in_Itemcat3", GlbItemCat3);


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

        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {

        }
    }
}