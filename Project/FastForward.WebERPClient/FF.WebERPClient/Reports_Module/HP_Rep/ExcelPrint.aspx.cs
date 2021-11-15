using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Text;

namespace FF.WebERPClient.Reports_Module.HP_Rep
{
    public partial class ExcelPrint : BasePage
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
                //ADD
                TimeSpan start1 = DateTime.Now.TimeOfDay;
                ReportDocument rr = new ReportDocument();
                rr.Load(Server.MapPath(GlbReportPath));
               rr.SetDatabaseLogon(System.Configuration.ConfigurationManager.AppSettings["DBUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["DBPassword"].ToString());
                // rr.SetDatabaseLogon("EMS", "EMS123");

                if (GlbReportName == "HP Cashflow forcasting Report")
                {
                    rr.SetParameterValue("IN_FROMDATE", GlbFromDate);
                    rr.SetParameterValue("IN_COMPANY", GlbCompany);
                    rr.SetParameterValue("in_user_id", GlbReportUser);
                }

                System.IO.MemoryStream oStream = (System.IO.MemoryStream)rr.ExportToStream(ExportFormatType.Excel);


                Response.Clear();
                Response.Buffer = true;
                // Response.ContentType = "application/pdf";
                Response.ContentType = "application/vnd.ms-excel";


                //if cover note show image
                Response.BinaryWrite(oStream.ToArray());
               // string Msg = "<script>SetPrintProperties();</script>";

                //string Msg = "<script>Print();</script>";
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                //string Msg1 = "<script>CloseWindow();</script>";
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
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