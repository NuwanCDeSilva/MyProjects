using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using FF.BusinessObjects.InventoryNew;
using System.IO;
using FastForward.SCMWeb.View.Reports.Warehouse;

namespace FastForward.SCMWeb.View.Reports.Warehouse
{
    public partial class WarehouseReportViewer : BasePage
    {

        csWarehouse obj = new csWarehouse();
        ReportDocument crystalReport = new ReportDocument();

        protected override void OnPreRender(EventArgs e)
        {
            Control c1 = this.Master.FindControl("DivTitle");
            c1.Visible = false;
            Control c2 = this.Master.FindControl("DivMenu");
            c2.Visible = false;
            base.OnPreRender(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            try
            {
                string _repname = string.Empty;
                string _papersize = string.Empty;
             

                CHNLSVC.General.CheckReportName(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["GlbReportType"].ToString(), out  _repname, out  BaseCls.ShowComName, out _papersize);
                if (_repname == null || _repname == "")
                {
                    CHNLSVC.General.CheckReportName(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefChannel, Session["GlbReportType"].ToString(), out  _repname, out  BaseCls.ShowComName, out _papersize);
                }
                if (_repname == null || _repname == "")
                {
                    CHNLSVC.General.CheckReportName(Session["UserCompanyCode"].ToString(), "N/A", Session["GlbReportType"].ToString(), out  _repname, out  BaseCls.ShowComName, out _papersize);
                }
                if (!(_repname == null || _repname == "")) { GlbReportName = _repname; Session["GlbReportName"] = _repname; BaseCls.GlbReportPaperSize = _papersize; }
                BaseCls.GlbReportTp = "";

                if (CVSale != null)
                {
                    CVSale.Dispose();
                }

                InvReportPara _objRepoPara = new InvReportPara();
                _objRepoPara = Session["InvReportPara"] as InvReportPara;

                if (Session["GlbReportName"].ToString() == "Item_Pick_Plan.rpt")
                {
                    obj.ItemPickPlanReport(_objRepoPara);
                    CVSale.ReportSource = obj._pickplan;
                    CVSale.RefreshReport();
                }
                if (Session["GlbReportName"].ToString() == "Stock_Verification.rpt")
                {
                    string docno = Session["docNo"].ToString();
                    string com = Session["UserCompanyCode"].ToString();
                    string loc = Session["UserDefLoca"].ToString();

                    string itemCode = Session["ItemCode"].ToString();
                  // obj.StockVerification(com, docno);
                   obj.StockVerificationnew(com, loc, itemCode, docno);
                   CVSale.ReportSource = obj.stock_verify;
                    CVSale.RefreshReport();
                }              
                BaseCls.GlbReportTp = string.Empty;
            }
            catch (Exception err)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog(Session["GlbReportName"].ToString(), "WarehouseReportViewer", err.Message, Session["UserID"].ToString());
                if (CVSale != null)
                {
                    CVSale.Dispose();
                }
                CHNLSVC.CloseChannel();
                Response.Redirect("~/Error.aspx?Error=" + err.Message + "");
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        string GetDefaultPrinter()
        {
            string _printerName = string.Empty;
            PrinterSettings settings = new PrinterSettings();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)
                {
                    _printerName = printer;
                    break;
                }
            }
            return _printerName;
        }



    }
}