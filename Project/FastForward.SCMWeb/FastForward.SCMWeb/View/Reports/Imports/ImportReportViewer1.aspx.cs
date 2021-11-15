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

namespace FastForward.SCMWeb.View.Reports.Imports
{
    public partial class ImportReportViewer1 : BasePage 
    {
        clsImports obj = new clsImports();
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
            try
            {
                //listAllPrinters(); 
                //lblPrinter.Text = "Default Printer- " + GetDefaultPrinter();
                //btnPrint.Visible = true;
                //crystalReportViewer1.ShowPrintButton = false;
                InvReportPara _objRepoPara = new InvReportPara();
                _objRepoPara = Session["InvReportPara"] as InvReportPara;

                if (CVImport != null)
                {
                    CVImport.Dispose();
                }

                if (Session["GlbReportName"].ToString() == "CusDecEntryReq.rpt")
                {
                    obj.CusDecEntryRequest(_objRepoPara);
                    CVImport.ReportSource = obj._cusDecEntry;
                    CVImport.RefreshReport();
                }
                if (Session["GlbReportName"].ToString() == "Total_Imports.rpt")
                {
                    //crystalReport.Load(Server.MapPath(Session["GlbReportName"]));
                    //CVImport.SetDataSource(obj._lcdtl);
                    obj.Total_Imports(_objRepoPara);
                    CVImport.ReportSource = obj._totImports;
                    CVImport.RefreshReport();
                }
                if (Session["GlbReportName"].ToString() == "Container_volume.rpt")
                {
                    //crystalReport.Load(Server.MapPath(Session["GlbReportName"]));
                    //CVImport.SetDataSource(obj._lcdtl);
                    obj.Container_Volume(_objRepoPara);
                    CVImport.ReportSource = obj._contVolume;
                    CVImport.RefreshReport();
                }
                CVImport.HasExportButton = true;
                CVImport.HasPrintButton = true;

                if (Session["GlbReportName"].ToString() == "LC_Details_Bankwise_Report.rpt")
                {
                    obj.LCDetails(_objRepoPara);
                    CVImport.ReportSource = obj._lcdtl;
                    CVImport.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "Shipment_Schedule.rpt")
                {
                    obj.ShipmentScheduleDetails(_objRepoPara);
                    CVImport.ReportSource = obj._shipsched;
                    CVImport.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "Order_Status_Report.rpt")
                {
                    obj.OrderStatus(_objRepoPara);
                    CVImport.ReportSource = obj._ordstatus;
                    CVImport.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "costing_sheet_report.rpt")
                {
                    obj.CostSheetDetails(_objRepoPara);
                    CVImport.ReportSource = obj._costsheet;
                    CVImport.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "Import_Schedule.rpt")
                {
                    obj.ImportSchedule(_objRepoPara);
                    CVImport.ReportSource = obj._ImpSched;
                    CVImport.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "rptShipmentStatus.rpt")
                {
                    //crystalReport.Load(Server.MapPath(Session["GlbReportName"]));
                    //CVImport.SetDataSource(obj._lcdtl);
                    obj.shipmentStatus(_objRepoPara);
                    CVImport.ReportSource = obj._shipmentStatus;
                    CVImport.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "rptImpCostAnalys.rpt")
                {
                    //crystalReport.Load(Server.MapPath(Session["GlbReportName"]));
                    //CVImport.SetDataSource(obj._lcdtl);
                    obj.importCostAnalysis(_objRepoPara);
                    CVImport.ReportSource = obj._impCstAnalysis;
                    CVImport.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "rptCostInformation.rpt")
                {
                    //crystalReport.Load(Server.MapPath(Session["GlbReportName"].ToString()));
                    //CVImport.SetDataSource(obj._lcdtl);
                    obj.importCostInformation(_objRepoPara);
                    CVImport.ReportSource = obj._CstInformationSummery;
                    CVImport.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "rptImportRegister.rpt")
                {
                    //crystalReport.Load(Server.MapPath(Session["GlbReportName"].ToString()));
                    //CVImport.SetDataSource(obj._lcdtl);
                    obj.importRegister(_objRepoPara);
                    CVImport.ReportSource = obj._ImptRegister;
                    CVImport.RefreshReport();
                }
                if (Session["GlbReportName"].ToString() == "rptSLPARegister.rpt")
                {
                    //crystalReport.Load(Server.MapPath(Session["GlbReportName"].ToString()));
                    //CVImport.SetDataSource(obj._lcdtl);
                    obj.slpaRegister(_objRepoPara);
                    CVImport.ReportSource = obj._slpaRegister;
                    CVImport.RefreshReport();
                }
            }
            catch (Exception err)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog(Session["GlbReportName"].ToString(), "ImportReportViewer", err.Message, Session["UserID"].ToString());
                if (CVImport != null)
                {
                    CVImport.Dispose();
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