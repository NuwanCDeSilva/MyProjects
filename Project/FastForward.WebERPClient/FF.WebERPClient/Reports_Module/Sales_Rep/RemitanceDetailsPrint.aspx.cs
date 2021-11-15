﻿using System;
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
    public partial class RemitanceDetailsPrint : BasePage
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

                //by darshana 28-09-2012
                CrystalReportSource1.Report = Cr;
                CrystalReportSource1.ReportDocument.SetDatabaseLogon(System.Configuration.ConfigurationManager.AppSettings["DBUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["DBPassword"].ToString());


                CrystalReportSource1.ReportDocument.SetParameterValue("user", GlbReportUser);
                CrystalReportSource1.ReportDocument.SetParameterValue("FromDate", GlbFromDate);
                CrystalReportSource1.ReportDocument.SetParameterValue("ToDate", GlbToDate);

                CrystalReportSource1.ReportDocument.SetParameterValue("ReportCompany", GlbReportCompany);
                CrystalReportSource1.ReportDocument.SetParameterValue("ReportCompAddr", GlbReportCompanyAddr);
                CrystalReportSource1.ReportDocument.SetParameterValue("FrmDate", GlbFromDateSub);
                CrystalReportSource1.ReportDocument.SetParameterValue("tDate", GlbToDateSub);
                CrystalReportSource1.ReportDocument.SetParameterValue("P_USER", GlbReportUser);
                CrystalReportSource1.ReportDocument.SetParameterValue("P_FROM", GlbFromDate);
                CrystalReportSource1.ReportDocument.SetParameterValue("P_TO", GlbToDate);
                CrystalReportSource1.ReportDocument.SetParameterValue("P_COM", GlbUserComCode);
                CrystalReportSource1.ReportDocument.SetParameterValue("P_PC", GlbProfitCenter);


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