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

namespace FastForward.SCMWeb.View.Reports.Sales
{
    public partial class SalesReportViewer : BasePage 
    {
      

        clsSales obj = new clsSales();
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
                //CVSale.ShowGroupTreeButton = false;
                CVSale.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;                
                CVSale.DisplayGroupTree = false;                
                CVSale.HasPageNavigationButtons = false;
                if (Session["GlbReportName"].ToString() == "InvoiceFullPrints.rpt")
                {
                    obj.InvocieFullPrint();
                    CVSale.ReportSource = obj.invfullReport;
                    CVSale.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "DeliveredSalesReport.rpt")
                {
                    obj.DeliveredSalesReport(_objRepoPara);
                    CVSale.ReportSource = obj._delSalesrptPC;
                    //WriteErrLog(Convert.ToString(_objRepoPara._GlbReportGroupPC) + " Report : DeliveredSalesReport", _objRepoPara._GlbUserID);
                    CVSale.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "excecutivewisesales.rpt")
                {
                    obj.executiveWiseSalesReport(_objRepoPara);
                    CVSale.ReportSource = obj._exectSales;
                    //WriteErrLog(Convert.ToString(_objRepoPara._GlbReportGroupPC) + " Report : DeliveredSalesReport", _objRepoPara._GlbUserID);
                    CVSale.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "Delivered_Sales_GRN.rpt")
                {
                    obj.DeliveredSalesGRNReport(_objRepoPara);
                    CVSale.ReportSource = obj._delSalesrptGRN;
                    //WriteErrLog(Convert.ToString(_objRepoPara._GlbReportGroupPC) + " Report : ConsignmentSalesReport", _objRepoPara._GlbUserID);
                    CVSale.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "DebtorSettlement1.rpt" || Session["GlbReportName"].ToString() == "DebtorSettlement_PC.rpt" || Session["GlbReportName"].ToString() == "DebtorSettlement_Outs_PC.rpt" || Session["GlbReportName"].ToString() == "DebtorSettlement_Outs.rpt")                
                {
                    obj.DebtorSettlementPrint(_objRepoPara);
                    if (Session["GlbReportName"].ToString() == "DebtorSettlement.rpt")
                    {
                        CVSale.ReportSource = obj._DebtSett;
                    }
                    if (Session["GlbReportName"].ToString() == "DebtorSettlement_PC.rpt")
                    {
                        CVSale.ReportSource = obj._DebtSettPC;
                    }
                    if (Session["GlbReportName"].ToString() == "DebtorSettlement_Outs_PC.rpt")
                    {
                        CVSale.ReportSource = obj._DebtSettOutPC;
                    }
                    if (Session["GlbReportName"].ToString() == "DebtorSettlement_Outs.rpt")
                    {
                        CVSale.ReportSource = obj._DebtSettOuts;
                    }
                    if (Session["GlbReportName"].ToString() == "DebtorSettlement_Outs_PC_Meeting.rpt")
                    {
                        CVSale.ReportSource = obj._DebtSettOutPCMeeting;
                    }
                    if (Session["GlbReportName"].ToString() == "DebtorSettlement_Outs_PC_with_comm.rpt")
                    {
                        CVSale.ReportSource = obj._DebtSettOutPCWithComm;
                    }                    
                    CVSale.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "Age_Debtor_Outstanding.rpt" || Session["GlbReportName"].ToString() == "Age_Debtor_Outstanding_PC.rpt" || Session["GlbReportName"].ToString() == "Age_Debtor_Outstanding_new.rpt" || Session["GlbReportName"].ToString() == "Age_Debtor_Outstanding_PC_new.rpt")
                {
                    obj.AgeAnalysisOfDebtorsOutstandingPrint(_objRepoPara);

                    if (Session["GlbReportName"].ToString() == "Age_Debtor_Outstanding.rpt")
                    {
                        CVSale.ReportSource = obj._AgeDebtOuts;
                    }
                    if (Session["GlbReportName"].ToString() == "Age_Debtor_Outstanding_PC.rpt")
                    {
                        CVSale.ReportSource = obj._AgeDebtOutsPC;
                    }
                    if (Session["GlbReportName"].ToString() == "Age_Debtor_Outstanding_new.rpt")
                    {
                        CVSale.ReportSource = obj._AgeDebtOuts_new;
                    }
                    if (Session["GlbReportName"].ToString() == "Age_Debtor_Outstanding_PC_new.rpt")
                    {
                        CVSale.ReportSource = obj._AgeDebtOutsPC_new;
                    }
                    CVSale.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "Rep_INV_DP.rpt")
                {
                    obj.IvoicePrint(Session["InvoiceNo"].ToString());
                    CVSale.ReportSource = obj._invdp;
                    CVSale.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "Rep_INV_DP_lrp_rp.rpt")
                {
                    obj.IvoicePrint(Session["InvoiceNo"].ToString());
                    CVSale.ReportSource = obj._invdp;
                    CVSale.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "Forward_Sales_Report1.rpt" || Session["GlbReportName"].ToString() == "Forward_Sales_Report2.rpt" || Session["GlbReportName"].ToString() == "Forward_Sales_Report_cost.rpt")
                {
                    obj.PendingDeliveryReport(_objRepoPara);
                    if (_objRepoPara._GlbReportType == "N")
                    {
                        if (_objRepoPara._GlbReportWithCost == 1)
                            CVSale.ReportSource = obj._Collectiondetail;
                        else
                            CVSale.ReportSource = obj._ForwardSalesrpt1;
                    }
                    else
                    {
                        CVSale.ReportSource = obj._ForwardSalesrpt2;
                    }
                    CVSale.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "rpt_PendingDeliveryReportNew.rpt")
                {
                    obj.PendingDeliveryReport(_objRepoPara);
                    CVSale.ReportSource = obj._fwSales;
                    CVSale.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "rpt_PendingSalesReportNew.rpt")
                {
                    obj.PendingSaelsReport();
                    CVSale.ReportSource = obj._pndSales;
                    CVSale.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "rptSaleOrderstatusReport.rpt")
                {
                   obj.so_status();
                    CVSale.ReportSource = obj._sostatus;
                    CVSale.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "rptSaleOrderstatusReport_summery.rpt")
                {
                    obj.so_status();
                    CVSale.ReportSource = obj._sostatus;
                    CVSale.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "Quotation_RepPrint.rpt")
                {
                    obj.QuotationPrintReport("","");
                    CVSale.ReportSource = obj._QuoPrint;
                    CVSale.RefreshReport();
                }
                if (Session["GlbReportName"].ToString() == "sales_order.rpt")
                {
                    string COM = Session["UserCompanyCode"] as string;
                    string pc = Session["UserDefProf"] as string;
                    string OrNo = Session["OrNo"] as string;
                    string loc = Session["UserDefLoca"].ToString();
                    obj.get_Sales_Orders(OrNo, COM, pc,loc);
                    CVSale.ReportSource = obj._sales_order;
                    CVSale.RefreshReport();
                }
                if (Session["GlbReportName"].ToString() == "pcdetails.rpt")
                {
                    string COM = Session["UserCompanyCode"] as string;
                    string pc = Session["UserDefProf"] as string;
                    string loc = Session["UserDefLoca"].ToString();
                    string pclist = Session["LocationList"] as string;
                    string user = Session["UserID"] as string;
                    obj.ProfitCenterDetails(COM, loc, pclist, user);
                    CVSale.ReportSource = obj._pcdetails;
                    CVSale.RefreshReport();
                }
                if (Session["GlbReportName"].ToString() == "SRN.rpt")
                {
                    string COM = Session["UserCompanyCode"] as string;
                    string loc = Session["UserDefLoca"].ToString();
                    string user = Session["UserID"] as string;
                    string invno = Session["DocNo"].ToString();

                    obj.GetSRNdata(user, COM, invno, loc);
                    CVSale.ReportSource = obj._SRNreport;
                    CVSale.RefreshReport();
                }


                BaseCls.GlbReportTp = string.Empty;
            }
            catch (Exception err)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog(Session["GlbReportName"].ToString(), "SalesReportViewer", err.Message, Session["UserID"].ToString());
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

        private void WriteErrLog(string _txt, string _user)
        {
            try
            {
                using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/reporterr.txt"), true))
                {
                    _testData.WriteLine(DateTime.Now.ToString("dd/MMM/yyyy  hh:mm:ss tt") + " - " + _txt + " User :" + _user);
                    
                }
            }
            catch (Exception _err)
            {
                //DisplayMessage(_err.Message, 4);
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