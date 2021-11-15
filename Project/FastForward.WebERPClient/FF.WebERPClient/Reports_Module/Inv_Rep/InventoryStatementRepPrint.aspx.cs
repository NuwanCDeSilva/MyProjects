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
    public partial class InventoryStatementRepPrint : BasePage
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
                //  ReportDocument cryRpt = new ReportDocument();
                //  cryRpt.Load(Convert.ToString(GlbReportPath));
                //  cryRpt.Load(Server.MapPath(Convert.ToString(GlbReportPath)));

                ConnectionInfo connectionInfo = new ConnectionInfo();


                ////COMMENT
                Report Cr = new Report();
                Cr.FileName = Convert.ToString(GlbReportPath);
                CrystalReportSource1 = new CrystalReportSource();

                CrystalReportSource1.Report = Cr;

               //CrystalReportSource1.ReportDocument.SetDatabaseLogon(System.Configuration.ConfigurationManager.AppSettings["REPDBUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["REPDBPassword"].ToString(), System.Configuration.ConfigurationManager.AppSettings["REPDBServer"].ToString(), System.Configuration.ConfigurationManager.AppSettings["REPDBName"].ToString());

               // if (GlbRepDB == 1)
                //{
                    //CrystalReportSource1.ReportDocument.SetDatabaseLogon(System.Configuration.ConfigurationManager.AppSettings["REPDBUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["REPDBPassword"].ToString());
               // }
               // else
               // {
                  //  CrystalReportSource1.ReportDocument.SetDatabaseLogon(System.Configuration.ConfigurationManager.AppSettings["DBUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["DBPassword"].ToString());
               // }
                
             CrystalReportSource1.ReportDocument.SetDatabaseLogon(System.Configuration.ConfigurationManager.AppSettings["DBUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["DBPassword"].ToString());
          //     CrystalReportSource1.ReportDocument.SetDatabaseLogon("EMS", "EMS123");
              
                if (!string.IsNullOrEmpty(GlbReportUser)) { CrystalReportSource1.ReportDocument.SetParameterValue("in_user_id", GlbReportUser); }
                if (GlbFromDate.ToString() != "01/01/0001 00:00:00") { CrystalReportSource1.ReportDocument.SetParameterValue("in_FromDate", GlbFromDate); }
                if (GlbToDate.ToString() != "01/01/0001 00:00:00") { CrystalReportSource1.ReportDocument.SetParameterValue("in_ToDate", GlbToDate); }
                        
          
                CrystalReportSource1.ReportDocument.SetParameterValue("in_Company", GlbCompany);  
                CrystalReportSource1.ReportDocument.SetParameterValue("in_Location_code", GlbLocation);  
                CrystalReportSource1.ReportDocument.SetParameterValue("in_Itemcat1", GlbItemCat1);  
                CrystalReportSource1.ReportDocument.SetParameterValue("in_Itemcat2", GlbItemCat2); 
                CrystalReportSource1.ReportDocument.SetParameterValue("in_Itemcat3", GlbItemCat3);  
                CrystalReportSource1.ReportDocument.SetParameterValue("in_Itemcode", GlbItemCode);  
                CrystalReportSource1.ReportDocument.SetParameterValue("in_Brand", GlbBrand);  
                CrystalReportSource1.ReportDocument.SetParameterValue("in_Model", GlbModel);  
          
 

              
                CrystalReportViewer1.ReportSource = CrystalReportSource1;

                CrystalReportViewer1.EnableDatabaseLogonPrompt =false;
                CrystalReportViewer1.EnableParameterPrompt = false;

                CrystalReportViewer1.Visible = true;

                CrystalReportSource1.ReportDocument.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;

                CrystalReportViewer1.Visible = true;
                CrystalReportViewer1.HasPrintButton = true;
                CrystalReportViewer1.PrintMode = PrintMode.Pdf;
                //CrystalReportViewer1.RefreshReport();       //kapila 20/11/2012
                ////END


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