using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Text;

namespace FF.WebERPClient.Test
{
    public partial class PdfPrint : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Display();
        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            GC.Collect();
        }

        public bool Display()
        {

            try
            {
                //ReportDocument cryRpt = new ReportDocument();
                //cryRpt.Load(Server.MapPath(Convert.ToString(GlbReportPath)));
                ////cryRpt.Load(GlbReportPath);

                ConnectionInfo connectionInfo = new ConnectionInfo();

                ////loading
                //CrystalReportViewer1.ReportSource = cryRpt;
                //cryRpt.Load(Server.MapPath(Convert.ToString(GlbReportMapPath)));

                ////parameters
                //if (!string.IsNullOrEmpty(GlbReportUser)) { cryRpt.SetParameterValue("user", GlbReportUser); }
                //if (GlbFromDate.ToString() != null) { cryRpt.SetParameterValue("FromDate", GlbFromDate); }
                //if (GlbToDate.ToString() != null) { cryRpt.SetParameterValue("ToDate", GlbToDate); }
                //if (!string.IsNullOrEmpty(GlbReportHeading_1)) { cryRpt.SetParameterValue("heading_1", GlbReportHeading_1); }
                //if (!string.IsNullOrEmpty(GlbDocNosList)) { cryRpt.SetParameterValue("DocNoList", GlbDocNosList); }
                //if (!string.IsNullOrEmpty(GlbProfitCenter)) { cryRpt.SetParameterValue("ProfitCenter", GlbProfitCenter); } //Sanjeewa 2012-07-21
                //if (!string.IsNullOrEmpty(GlbPeriod)) { cryRpt.SetParameterValue("Period", GlbPeriod); }
                //if (!string.IsNullOrEmpty(GlbDocType)) { cryRpt.SetParameterValue("DocType", GlbDocType); }
                //if (!string.IsNullOrEmpty(GlbDocSubType)) { cryRpt.SetParameterValue("DocSubType", GlbDocSubType); } //Sanjeewa
                ////kapila 27/8/2012
                //if (GlbReportName == "Remittance Summary")
                //{
                //    cryRpt.SetParameterValue("FrmDate", GlbFromDateSub);
                //    cryRpt.SetParameterValue("tDate", GlbToDateSub);
                //    cryRpt.SetParameterValue("RemToBeBanked", GlbRemToBeBanked);
                //    cryRpt.SetParameterValue("CIH", GlbCIH);
                //    cryRpt.SetParameterValue("TotRemitance", GlbTotRemitance);
                //    cryRpt.SetParameterValue("CommWithdrawn", GlbCommWithdrawn);
                //}

                //connectionInfo.ServerName = System.Configuration.ConfigurationSettings.AppSettings["DBServer"];
                //connectionInfo.DatabaseName = System.Configuration.ConfigurationSettings.AppSettings["DBName"];
                //connectionInfo.UserID = System.Configuration.ConfigurationSettings.AppSettings["DBUser"];
                //connectionInfo.Password = System.Configuration.ConfigurationSettings.AppSettings["DBPassword"];

                //SetDBLogonForReport(connectionInfo);


                //COMMENT
                //Report Cr = new Report();
                //Cr.FileName = Convert.ToString(GlbReportPath);
                //if (GlbReportName == "InvoiceInDO") Cr.FileName = Convert.ToString(GlbReportMapPath);
                //CrystalReportSource1 = new CrystalReportSource();


                //CrystalReportSource1.Report = Cr;
                //CrystalReportSource1.ReportDocument.SetDatabaseLogon(System.Configuration.ConfigurationManager.AppSettings["DBUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["DBPassword"].ToString());
                ////CrystalReportSource1.ReportDocument.SetDatabaseLogon("EMS", "SYSTEM", "192.168.1.21", "EMS");

                //if (!string.IsNullOrEmpty(GlbDocNosList)) { CrystalReportSource1.ReportDocument.SetParameterValue("DocNoList", GlbDocNosList); }
                //if (!string.IsNullOrEmpty(GlbReportUser)) { CrystalReportSource1.ReportDocument.SetParameterValue("user", GlbReportUser); }
                //if (GlbFromDate.ToString() != "01/01/0001 00:00:00") { CrystalReportSource1.ReportDocument.SetParameterValue("FromDate", GlbFromDate); }
                //if (GlbToDate.ToString() != "01/01/0001 00:00:00") { CrystalReportSource1.ReportDocument.SetParameterValue("ToDate", GlbToDate); }
                //if (!string.IsNullOrEmpty(GlbReportHeading_1)) { CrystalReportSource1.ReportDocument.SetParameterValue("heading_1", GlbReportHeading_1); }
                //if (!string.IsNullOrEmpty(GlbProfitCenter)) { CrystalReportSource1.ReportDocument.SetParameterValue("ProfitCenter", GlbProfitCenter); } //Sanjeewa 2012-07-21
                //if (!string.IsNullOrEmpty(GlbPeriod)) { CrystalReportSource1.ReportDocument.SetParameterValue("Period", GlbPeriod); }
                //if (!string.IsNullOrEmpty(GlbDocType)) { CrystalReportSource1.ReportDocument.SetParameterValue("DocType", GlbDocType); }
                //if (!string.IsNullOrEmpty(GlbDocSubType)) { CrystalReportSource1.ReportDocument.SetParameterValue("DocSubType", GlbDocSubType); }
                //if (!string.IsNullOrEmpty(GlbVehRegRecNo)) { CrystalReportSource1.ReportDocument.SetParameterValue("RecNo", GlbVehRegRecNo); }
                ////Added By Prabhath on 03/09/2012 for Half invoice print to show serial n warranty list

                //// Added below new parameters for Collection summary report.
                //if (!string.IsNullOrEmpty(GlbIsCancelled)) { CrystalReportSource1.ReportDocument.SetParameterValue("IsCancel", GlbIsCancelled); }
                //if (!string.IsNullOrEmpty(GlbOtherSp)) { CrystalReportSource1.ReportDocument.SetParameterValue("Others ", GlbIsCancelled); }


                ////kapila 27/8/2012
                //if (GlbReportName == "Remittance Summary")
                //{
                //    CrystalReportSource1.ReportDocument.SetParameterValue("FrmDate", GlbFromDateSub);
                //    CrystalReportSource1.ReportDocument.SetParameterValue("tDate", GlbToDateSub);
                //    CrystalReportSource1.ReportDocument.SetParameterValue("RemToBeBanked", GlbRemToBeBanked);
                //    CrystalReportSource1.ReportDocument.SetParameterValue("CIH", GlbCIH);
                //    CrystalReportSource1.ReportDocument.SetParameterValue("TotRemitance", GlbTotRemitance);
                //    CrystalReportSource1.ReportDocument.SetParameterValue("CommWithdrawn", GlbCommWithdrawn);
                //}
                //if (GlbReportName == "Invoice")
                //{
                //    CrystalReportSource1.ReportDocument.SetParameterValue("p_serials", GlbReportSerialList);
                //    CrystalReportSource1.ReportDocument.SetParameterValue("p_warranty", GlbReportWarrantyList);
                //}


                //if (!string.IsNullOrEmpty(GlbSelectionFormula)) //Sanjeewa 2012-07-21
                //{
                //    CrystalReportSource1.ReportDocument.RecordSelectionFormula = GlbSelectionFormula;
                //}
                //if (GlbReportName == "Closing Balance Summary")//Shani  2012-09-26     HP-Closing Balance Summary
                //{
                //    //p_userName in NVARCHAR2,p_pc in NVARCHAR2,p_schTp in NVARCHAR2,p_schCD in NVARCHAR2,p_aCCNo In NVARCHAR2,p_asat_date in DATE
                //    CrystalReportSource1.ReportDocument.SetParameterValue("p_userName", GlbCLS_BAL_UserName);
                //    CrystalReportSource1.ReportDocument.SetParameterValue("p_pc", GlbCLS_BAL_Pc);
                //    CrystalReportSource1.ReportDocument.SetParameterValue("p_schTp",null);
                //    CrystalReportSource1.ReportDocument.SetParameterValue("p_schCD", GlbCLS_BAL_SchCode);
                //    CrystalReportSource1.ReportDocument.SetParameterValue("p_aCCNo", null);
                //    CrystalReportSource1.ReportDocument.SetParameterValue("p_asat_date", GlbCLS_BAL_DueDate);

                //    //GlbReportName = "Closing Balance Summary";
                //    //GlbCLS_BAL_Pc = GlbUserDefProf;//change this according to the selected PCs.
                //    //GlbCLS_BAL_SchCode = null;
                //    //GlbCLS_BAL_DueDate = DateTime.Now.Date;
                //    //GlbCLS_BAL_UserName = GlbUserName;
                //}

                //CrystalReportViewer1.ReportSource = CrystalReportSource1;

                //CrystalReportViewer1.EnableDatabaseLogonPrompt = false;
                //CrystalReportViewer1.EnableParameterPrompt = false;

                //CrystalReportSource1.ReportDocument.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;

                //CrystalReportViewer1.Visible = true;
                //CrystalReportViewer1.HasPrintButton = true;
                //CrystalReportViewer1.PrintMode = PrintMode.Pdf;
                //END

                //ADD
                TimeSpan start1 = DateTime.Now.TimeOfDay;
                ReportDocument rr = new ReportDocument();
                rr.Load(Server.MapPath(GlbReportPath));
                if (GlbReportName == "InvoiceInDO") rr.Load(Server.MapPath(GlbReportMapPath));


                rr.SetDatabaseLogon(System.Configuration.ConfigurationManager.AppSettings["DBUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["DBPassword"].ToString());
                //CrystalReportSource1.ReportDocument.SetDatabaseLogon("EMS", "SYSTEM", "192.168.1.21", "EMS");

                if (!string.IsNullOrEmpty(GlbDocNosList)) { rr.SetParameterValue("DocNoList", GlbDocNosList); }
                if (!string.IsNullOrEmpty(GlbReportUser)) { rr.SetParameterValue("user", GlbReportUser); }
                if (GlbFromDate.ToString() != "01/01/0001 00:00:00") { rr.SetParameterValue("FromDate", GlbFromDate); }
                if (GlbToDate.ToString() != "01/01/0001 00:00:00") { rr.SetParameterValue("ToDate", GlbToDate); }
                if (!string.IsNullOrEmpty(GlbReportHeading_1)) { rr.SetParameterValue("heading_1", GlbReportHeading_1); }
                if (!string.IsNullOrEmpty(GlbProfitCenter)) { rr.SetParameterValue("ProfitCenter", GlbProfitCenter); } //Sanjeewa 2012-07-21
                if (!string.IsNullOrEmpty(GlbPeriod)) { rr.SetParameterValue("Period", GlbPeriod); }
                if (!string.IsNullOrEmpty(GlbDocType)) { rr.SetParameterValue("DocType", GlbDocType); }
                if (!string.IsNullOrEmpty(GlbDocSubType)) { rr.SetParameterValue("DocSubType", GlbDocSubType); }
                if (!string.IsNullOrEmpty(GlbVehRegRecNo)) { rr.SetParameterValue("RecNo", GlbVehRegRecNo); }
                //Added By Prabhath on 03/09/2012 for Half invoice print to show serial n warranty list



                //kapila 27/8/2012
                if (GlbReportName == "Remittance Summary")
                {
                    rr.SetParameterValue("FrmDate", GlbFromDateSub);
                    rr.SetParameterValue("tDate", GlbToDateSub);
                    rr.SetParameterValue("RemToBeBanked", GlbRemToBeBanked);
                    rr.SetParameterValue("CIH", GlbCIH);
                    rr.SetParameterValue("TotRemitance", GlbTotRemitance);
                    rr.SetParameterValue("CommWithdrawn", GlbCommWithdrawn);
                }
                if (GlbReportName == "Invoice")
                {
                    rr.SetParameterValue("p_serials", GlbReportSerialList);
                    rr.SetParameterValue("p_warranty", GlbReportWarrantyList);
                }
                System.IO.MemoryStream oStream = (System.IO.MemoryStream)rr.ExportToStream(ExportFormatType.PortableDocFormat);
                //rr.ExportToDisk(ExportFormatType.PortableDocFormat,Server.MapPath("aaa.pdf"));
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                //Response.Write(" <object id=\"pdf\" classid=\"clsid:CA8A9780-280D-11CF-A24D-444553540000\" viewastext><param name=\"src\" width=\"300\" height=\"200\" value=\"aaa.pdf\"></object>");
                //Response.Write("<object data=\"aaa.pdf\" type=\"application/pdf\" width=\"900\" height=\"900\"></object>");
                //if cover note show image
                Response.BinaryWrite(oStream.ToArray());
                string Msg = "<script>SetPrintProperties();</script>";

                //string Msg = "<script>Print();</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                string Msg1 = "<script>CloseWindow();</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
                TimeSpan end2 = DateTime.Now.TimeOfDay;

                TimeSpan diiff1 = end2 - start1;

                rr.Close();
                rr.Dispose();
                rr = null;
                return true;
                //END
            }
            catch (Exception er)
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                string Msg = "<script>alert('System Error Occur. : " + er.Message + "')</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return false;
            }
        }
    }
}