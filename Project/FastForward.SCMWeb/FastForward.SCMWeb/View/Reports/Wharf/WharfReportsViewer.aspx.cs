using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Printing;
using CrystalDecisions.CrystalReports.Engine;
using FastForward.SCMWeb.View.Reports.Inventory;
using CrystalDecisions.Shared;

namespace FastForward.SCMWeb.View.Reports.Wharf
{
    public partial class WharfReportsViewer : BasePage
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            clswharf obj = new clswharf();
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

                if (CVWharf != null)
                {
                    CVWharf.Dispose();
                }

                InvReportPara _objRepoPara = new InvReportPara();
                _objRepoPara = Session["InvReportPara"] as InvReportPara;
                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                if (Session["GlbReportName"].ToString() == "custom_val_declear.rpt")
                {
                    obj = new clswharf();
                   
                    string COM = Session["UserCompanyCode"] as string;
                    string pc = Session["UserDefProf"] as string;
                    string EntryNo = Session["RefDoc"] as string;
                    obj.ValueDeclaration(EntryNo, COM);
                   // CVWharf.ReportSource = obj._cus_val_declar;
                   // CVWharf.RefreshReport();

                    PrintPDF(targetFileName, obj._cus_val_declar);
                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }
                if (Session["GlbReportName"].ToString() == "custom_ele.rpt")
                {
                    obj = new clswharf();

                    string COM = Session["UserCompanyCode"] as string;
                    string pc = Session["UserDefProf"] as string;
                    string EntryNo = Session["RefDoc"] as string;
                    string user = Session["UserID"].ToString();
                    obj.CustomElereport(COM, EntryNo, "DUTY", user);
                   // CVWharf.ReportSource = obj._cus_ele;
                   // CVWharf.RefreshReport();
                    PrintPDF(targetFileName, obj._cus_ele);
                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }
                if (Session["GlbReportName"].ToString() == "custom_workingsheet.rpt")
                {
                    obj = new clswharf();

                    string COM = Session["UserCompanyCode"] as string;
                    string pc = Session["UserDefProf"] as string;
                    string EntryNo = Session["RefDoc"] as string;
                    obj.CreateWorkingSheet(EntryNo, "TOT","","");
                    //CVWharf.ReportSource = obj._cus_work;
                  //  CVWharf.RefreshReport();
                    PrintPDF(targetFileName, obj._cus_work);
                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }
                if (Session["GlbReportName"].ToString() == "cargo_imports.rpt")
                {
                    obj = new clswharf();
                    string COM = Session["UserCompanyCode"] as string;
                    string pc = Session["UserDefProf"] as string;
                    string EntryNo = Session["RefDoc"] as string;
                    obj.CargoImports(EntryNo, COM);
                    CVWharf.ReportSource = obj._cargo_imp;
                    CVWharf.RefreshReport();
                    //obj._cargo_imp.PrintOptions.PrinterName = GetDefaultPrinter();
                    //obj._cargo_imp.PrintToPrinter(1, false, 0, 0);

                    //PrintPDF(targetFileName, obj._cargo_imp);
                 string   url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                 ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                


                    
                }
                if (Session["GlbReportName"].ToString() == "GoodsDeclaration.rpt")
                {
                    obj = new clswharf();
                    string COM = Session["UserCompanyCode"] as string;
                    string pc = Session["UserDefProf"] as string;
                    string EntryNo = Session["RefDoc"] as string;
                    obj.GoodsDecarration(EntryNo, COM);
                  //  CVWharf.ReportSource = obj._goods_declaration;
                   // CVWharf.RefreshReport();


                    PrintPDF(targetFileName, obj._goods_declaration);
                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
 

                }
                if (Session["GlbReportName"].ToString() == "GoodsDeclarationSheet.rpt")
                {
                    obj = new clswharf();
                    string COM = Session["UserCompanyCode"] as string;
                    string pc = Session["UserDefProf"] as string;
                    string EntryNo = Session["RefDoc"] as string;
                    obj.GoodsDecarrationSheet(EntryNo, COM);
                   // CVWharf.ReportSource = obj._goods_declarationsheet;
                  //  CVWharf.RefreshReport();
                    PrintPDF(targetFileName, obj._goods_declarationsheet);
                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);


                }
                if (Session["GlbReportName"].ToString() == "GoodsDeclarationII.rpt")
                {
                    obj = new clswharf();
                    string COM = Session["UserCompanyCode"] as string;
                    string pc = Session["UserDefProf"] as string;
                    string EntryNo = Session["RefDoc"] as string;
                    obj.GoodsDeclarationSchedule2(EntryNo, COM);
                  //  CVWharf.ReportSource = obj._goods_declarationII;
                   // CVWharf.RefreshReport();
                    PrintPDF(targetFileName, obj._goods_declarationII);
                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                  

                }
                if (Session["GlbReportName"].ToString() == "GoodsDeclarationIII.rpt")
                {
                    obj = new clswharf();
                    string COM = Session["UserCompanyCode"] as string;
                    string pc = Session["UserDefProf"] as string;
                    string EntryNo = Session["RefDoc"] as string;
                    obj.GoodsDecarrationAsycuda(EntryNo, COM);
                  //  CVWharf.ReportSource = obj._goods_declarationIII;
                   // CVWharf.RefreshReport();

                    PrintPDF(targetFileName, obj._goods_declarationIII);
                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);


                }

            }
            catch (Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog(Session["GlbReportName"].ToString(), "WarfReportsViewer", ex.Message, Session["UserID"].ToString());
                if (CVWharf != null)
                {
                    CVWharf.Dispose();
                }
                CHNLSVC.CloseChannel();
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            try
            {
                //clsInventory obj = new clsInventory();
                //InvReportPara _objRepoPara = new InvReportPara();
                //_objRepoPara = Session["InvReportPara"] as InvReportPara;
                //obj.Print_AOA_Warra(_objRepoPara);
                //ReportDocument Rel = new ReportDocument();
                ReportDocument rptDoc = (ReportDocument)_rpt;
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                diskOpts.DiskFileName = targetFileName;
                rptDoc.ExportOptions.DestinationOptions = diskOpts;
                rptDoc.Export();

                rptDoc.Close();
                rptDoc.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //string GetDefaultPrinter()
        //{
        //    string _printerName = string.Empty;
        //    PrinterSettings settings = new PrinterSettings();
        //    foreach (string printer in PrinterSettings.InstalledPrinters)
        //    {
        //        settings.PrinterName = printer;
        //        if (settings.IsDefaultPrinter)
        //        {
        //            _printerName = printer;
        //            break;
        //        }
        //    }
        //    return _printerName;
        //}
    }
}