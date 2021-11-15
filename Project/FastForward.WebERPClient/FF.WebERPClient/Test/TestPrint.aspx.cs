using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace FF.WebERPClient.Test
{
    public partial class TestPrint : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            //GlbDocNosList = "RNE2-CS0565";
            //GlbReportPath = "~\\Reports_Module\\Sales_Rep\\VehInsCoverNote.rpt";
            //GlbReportMapPath = "~\\Reports_Module\\Sales_Rep\\VehInsCoverNote.rpt";

            //GlbReportName = "Invoice";

            //GlbMainPage = "~/Sales_Module/SalesEntry.aspx";
            string st = HiddenField1.Value.Split(',')[0];
            Session["defaultPrinter"] = st;
            Label1.Text = st;
            GlbDocNosList = TextBoxInv.Text;
            GlbReportPath = "~/Test/InvoiceHalfPrint3.rpt";
            GlbReportMapPath = "~/Test/InvoiceHalfPrint3.rpt";

            GlbReportName = "Invoice";

            GlbMainPage = "~/Sales_Module/SalesEntry.aspx";
            PrintTest pp = new PrintTest();
            string  b=pp.Display();
            if (b!="")
            {
                string Msg = "<script>alert('System Error Occur. : " + b+ "');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            else {
                string Msg = "<script>alert('Sucessfull!!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string st = HiddenField1.Value.Split(',')[0];
            Session["defaultPrinter"] = st;
            Label1.Text = st;
            PrintTest1 inv = new PrintTest1();
            bool b=inv.SetVariables(TextBoxInv.Text);
            if (!b)
            {
                string Msg = "<script>alert('System Error Occur. : " + "Unknown Error" + "');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            else
            {
                string Msg = "<script>alert('Sucessfull!!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //GlbDocNosList = "RNE2-CS0565";
            //GlbReportPath = "~\\Reports_Module\\Sales_Rep\\VehInsCoverNote.rpt";
            //GlbReportMapPath = "~\\Reports_Module\\Sales_Rep\\VehInsCoverNote.rpt";

            //GlbReportName = "Invoice";

            //GlbMainPage = "~/Sales_Module/SalesEntry.aspx";
            string st = HiddenField1.Value.Split(',')[0];
            Session["defaultPrinter"] = st;
            Label1.Text = st;
            GlbDocNosList = TextBoxInv.Text;
            GlbReportPath = "~/Test/InvoiceHalfPrint3.rpt";
            GlbReportMapPath = "~/Test/InvoiceHalfPrint3.rpt";

            GlbReportName = "Invoice";

            GlbMainPage = "~/Sales_Module/SalesEntry.aspx";
            string Msg = "<script>alert('Open!!');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            Response.Redirect("~/Test/HTMLPrint.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string st = HiddenField1.Value.Split(',')[0];
            Session["defaultPrinter"] = st;
            Label1.Text = st;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string st = HiddenField1.Value.Split(',')[0];
            Session["defaultPrinter"] = st;
            Label1.Text = st;
            GlbDocNosList = TextBoxInv.Text;
            GlbReportPath = "~/Test/InvoiceHalfPrint3.rpt";
            GlbReportMapPath = "~/Test/InvoiceHalfPrint3.rpt";

            GlbReportName = "Invoice";

            GlbMainPage = "~/Sales_Module/SalesEntry.aspx";
            string Msg = "<script>alert('Open!!');</script>";
            Msg = "window.open('../Test/PdfPrint.aspx',  '_blank');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, true);
            //Response.Redirect("~/Test/PdfPrint.aspx");
        }
    }
}