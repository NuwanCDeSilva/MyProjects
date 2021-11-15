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

namespace FastForward.SCMWeb.View.Reports.Inventory
{
    public partial class InventoryReportViewer : BasePage
    {
        clsInventory obj = new clsInventory();
        ReportDocument crystalReport = new ReportDocument();
        private InvReportPara _objRepoPara = new InvReportPara();

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

                _objRepoPara = Session["InvReportPara"] as InvReportPara;   //28/4/2016  

                if (CVInventory != null)
                {
                    CVInventory.Dispose();
                }

                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                if (Session["GlbReportName"].ToString() == "Last_No_Seq_Rep.rpt")
                {
                    obj.LastNoSeqReport(_objRepoPara);
                    PrintPDF(targetFileName, obj._LastNoSeq);
                    //CVInventory.ReportSource = obj._LastNoSeq;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "ReservationDetail.rpt")
                {
                    obj.Reservation_Details(_objRepoPara);
                    PrintPDF(targetFileName, obj._resDet);
                    //CVInventory.ReportSource = obj._mrnStus;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "ReservationSummary.rpt")
                {
                    obj.Reservation_Summary(_objRepoPara);
                    PrintPDF(targetFileName, obj._resSumm);
                    //CVInventory.ReportSource = obj._resSumm;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "MRN_Status.rpt")
                {
                    obj.MRN_Status(_objRepoPara);
                    PrintPDF(targetFileName, obj._mrnStus);
                    //CVInventory.ReportSource = obj._mrnStus;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "ToBondStatus.rpt")
                {
                    obj.To_Bond_Status(_objRepoPara);
                    PrintPDF(targetFileName, obj._toBondStus);
                    //CVInventory.ReportSource = obj._toBondStus;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "ItemBufferStatus.rpt")
                {
                    obj.Item_Buffer_Status(_objRepoPara);
                    PrintPDF(targetFileName, obj._itmBufStus);
                    //CVInventory.ReportSource = obj._itmBufStus;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "SerialMovement.rpt")
                {
                    obj.Movement_Audit_Trial(_objRepoPara);
                    PrintPDF(targetFileName, obj._serMove);
                    //CVInventory.ReportSource = obj._serMove;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "Stock_Balance.rpt" || Session["GlbReportName"].ToString() == "Stock_BalanceCat.rpt")
                {
                    obj.Current_Stock_Balance(_objRepoPara);
                    if (Session["GlbReportName"].ToString() == "Stock_BalanceCat.rpt")
                        PrintPDF(targetFileName, obj._curStkBalCat);
                        //CVInventory.ReportSource = obj._curStkBalCat;
                    else
                        PrintPDF(targetFileName, obj._curStkBal);
                        //CVInventory.ReportSource = obj._curStkBal;

                   // CVInventory.RefreshReport();
                   // CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "MoveCostDetail.rpt")
                {
                    obj.movementCostDetail(_objRepoPara);
                    PrintPDF(targetFileName, obj._movCostDet);
                    //CVInventory.ReportSource = obj._movCostDet;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "SerialMovement.rpt")
                {
                    obj.movementCostDetail(_objRepoPara);
                    PrintPDF(targetFileName, obj._movCostDet);
                    //CVInventory.ReportSource = obj._movCostDet;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "curr_age_report.rpt")
                {
                    obj.Curr_Age_Report(_objRepoPara);
                    PrintPDF(targetFileName, obj._CurrAge);
                    //CVInventory.ReportSource = obj._CurrAge;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "Stock_BalanceCost.rpt")
                {
                    obj.inventoryBalanceWithCost(_objRepoPara);
                    PrintPDF(targetFileName, obj._invBalCst);
                    //CVInventory.ReportSource = obj._invBalCst;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "Stock_BalanceSerialAs_at.rpt")
                {
                    obj.inventoryBalanceSerial_Asat(_objRepoPara);
                   // PrintPDF(targetFileName, obj._invBalSrlAsat);
                    CVInventory.ReportSource = obj._invBalSrlAsat;
                    CVInventory.RefreshReport();
                    CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "Movement_audit_trial_sum.rpt")
                {
                    obj.Movement_Audit_Trial(_objRepoPara);
                    PrintPDF(targetFileName, obj._moveAuditTrialSum);
                    //CVInventory.ReportSource = obj._moveAuditTrialSum;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "Movement_audit_trial.rpt")
                {
                    obj.Movement_Audit_Trial(_objRepoPara);
                    PrintPDF(targetFileName, obj._moveAuditTrial);
                    //CVInventory.ReportSource = obj._moveAuditTrial;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "Movement_audit_trial_cost.rpt")
                {
                    obj.Movement_Audit_Trial(_objRepoPara);
                    PrintPDF(targetFileName, obj._moveAuditTrialCost);
                    //CVInventory.ReportSource = obj._moveAuditTrialCost;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "Movement_audit_trial_ser.rpt")
                {
                    obj.Movement_Audit_Trial(_objRepoPara);
                    PrintPDF(targetFileName, obj._moveAuditTrialSer);
                    //CVInventory.ReportSource = obj._moveAuditTrialSer;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "Movement_audit_trial_ser_cost.rpt")
                {
                    obj.Movement_Audit_Trial(_objRepoPara);
                    PrintPDF(targetFileName, obj._moveAuditTrialSerCost);
                    //CVInventory.ReportSource = obj._moveAuditTrialSerCost;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "StockAge.rpt")
                {
                    obj.StockAgeReport(_objRepoPara);
                    PrintPDF(targetFileName, obj._stkAge);
                    //CVInventory.ReportSource = obj._stkAge;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }

                if (Session["GlbReportName"].ToString() == "rptPurchaseorderSummery_Summery.rpt")
                {
                    obj.purchaseOrderSummery(_objRepoPara);
                    PrintPDF(targetFileName, obj._purOrdSumm_summ);
                    //CVInventory.ReportSource = obj._purOrdSumm_summ;
                    //CVInventory.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "rptPurchaseorderSummery_Detail.rpt")
                {
                    obj.purchaseOrderSummery_Detail(_objRepoPara);
                    PrintPDF(targetFileName, obj._purOrdSumm_dtl);
                    //CVInventory.ReportSource = obj._purOrdSumm_dtl;
                    //CVInventory.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "Rep_Pur_Order.rpt")
                {
                    string COM = Session["UserCompanyCode"] as string;
                    obj.purchaseOrderPrint(COM, Session["ReportDoc"].ToString());
                    PrintPDF(targetFileName, obj._PoPrint);
                    //CVInventory.ReportSource = obj._PoPrint;
                    //CVInventory.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "rptPOGRNPending.rpt")
                {
                    obj.pending_GRNPO(_objRepoPara);
                    PrintPDF(targetFileName, obj._pendingGRN);
                    //CVInventory.ReportSource = obj._pendingGRN;
                    //CVInventory.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "rptLocalpurchaseCostDetail.rpt")
                {
                    obj.Local_Purchase_Costing(_objRepoPara);
                    PrintPDF(targetFileName, obj._localPurcaseCost);
                    //CVInventory.ReportSource = obj._localPurcaseCost;
                    //CVInventory.RefreshReport();
                }
                if (Session["GlbReportName"].ToString() == "StockBalanceWithSerialAge.rpt")
                {
                    obj.SerialAgeReport(_objRepoPara);
                  //  PrintPDF(targetFileName, obj._serAge);
                    CVInventory.ReportSource = obj._serAge;
                    CVInventory.RefreshReport();
                    CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "rptValueAddition.rpt")
                {
                    obj.Value_Addtion(_objRepoPara);
                    PrintPDF(targetFileName, obj._valueAddition);
                    //CVInventory.ReportSource = obj._valueAddition;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "InventoryStatementsTr3.rpt")
                {
                    obj.ItemWiseTransDetListing(_objRepoPara);
                    PrintPDF(targetFileName, obj._invStsTr3);
                    //CVInventory.ReportSource = obj._invStsTr3;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "Outward_Docs.rpt")
                {
                    obj.printOutwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
                    CVInventory.ReportSource = obj._outdoc;
                    CVInventory.RefreshReport();
                }
                if (Session["GlbReportName"].ToString() == "Outward_Docs_DO.rpt")
                {
                    obj.printOutwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
                    CVInventory.ReportSource = obj._outdocDO;
                    CVInventory.RefreshReport();
                }
                if (Session["GlbReportName"].ToString() == "Outward_Docs_Full.rpt")
                {
                    obj.printOutwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
                    CVInventory.ReportSource = obj._outdocfull;
                    CVInventory.RefreshReport();

                    //ReportDocument crystalReport = new ReportDocument(); // creating object of crystal report
                    //crystalReport.Load(Server.MapPath("~/CrystalPersonInfo.rpt")); // path of report 
                    //crystalReport.SetDataSource(datatable); // binding datatable
                    //CVInventory.ReportSource = crystalReport;
                    //ReportDocument rpt = (ReportDocument)obj._outdocfull;
                    //rpt.ExportToHttpResponse
                    //(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, Session["GlbReportName"].ToString().Replace(".rpt",""));
                }
                if (Session["GlbReportName"].ToString() == "Inward_Docs.rpt")
                {
                    obj.printInwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
                    CVInventory.ReportSource = obj._indoc;
                    CVInventory.RefreshReport();
                }
                if (Session["GlbReportName"].ToString() == "serial_items.rpt")
                {
                    string docno = Session["documntNo"] as string;
                    string user = Session["UserID"] as string;
                    string location = Session["UserDefLoca"].ToString();
                    obj.get_Item_Serials(docno, user, location);
                    PrintPDF(targetFileName, obj._serialItems);
                    //CVInventory.ReportSource = obj._serialItems;
                    //CVInventory.RefreshReport();
                }
                if (Session["GlbReportName"].ToString() == "Inward_Docs_Full.rpt")
                {
                    obj.printInwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
                    CVInventory.ReportSource = obj._indocfull;
                    CVInventory.RefreshReport();
                }
                if (Session["GlbReportName"].ToString() == "Inward_Docs_Full_GRN.rpt")
                {
                    obj.printInwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
                    CVInventory.ReportSource = obj._indocfullGRN;
                    CVInventory.RefreshReport();
                }
                if (Session["GlbReportName"].ToString() == "InventoryStatements.rpt")
                {
                    obj.inventoryStatement(_objRepoPara);
                    PrintPDF(targetFileName, obj._invSts);
                    //CVInventory.ReportSource = obj._invSts;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "StockLedger.rpt")
                {
                    obj.StockLedger(_objRepoPara);
                    PrintPDF(targetFileName, obj._stkLedger);
                    //CVInventory.ReportSource = obj._stkLedger;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "excess_stock_report.rpt")
                {
                    obj.ExcessStockReport(_objRepoPara);
                    PrintPDF(targetFileName, obj._Esxcessstk);
                    //CVInventory.ReportSource = obj._Esxcessstk;
                    //CVInventory.RefreshReport();
                }
                if (Session["GlbReportName"].ToString() == "ConsignmentMovement.rpt")
                {
                    obj.ConsignmentMovement(_objRepoPara);
                    PrintPDF(targetFileName, obj._consMove);
                    //CVInventory.ReportSource = obj._consMove;
                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "ItmBrndwiseItemAge.rpt" || Session["GlbReportName"].ToString() == "LocwiseItemAge.rpt" || Session["GlbReportName"].ToString() == "CatwiseItemAge.rpt" || Session["GlbReportName"].ToString() == "CatScatwiseItemAge.rpt" || Session["GlbReportName"].ToString() == "StuswiseItemAge.rpt")
                {
                    obj.Loc_wise_item_age(_objRepoPara);
                    if (Session["GlbReportName"].ToString() == "LocwiseItemAge.rpt")
                        PrintPDF(targetFileName, obj._locAge);
                       // CVInventory.ReportSource = obj._locAge;
                    if (Session["GlbReportName"].ToString() == "CatwiseItemAge.rpt")
                        PrintPDF(targetFileName, obj._catAge);
                      //  CVInventory.ReportSource = obj._catAge;
                    if (Session["GlbReportName"].ToString() == "CatScatwiseItemAge.rpt")
                        PrintPDF(targetFileName, obj._catScatAge);
                       // CVInventory.ReportSource = obj._catScatAge;
                    if (Session["GlbReportName"].ToString() == "StuswiseItemAge.rpt")
                        PrintPDF(targetFileName, obj._StusAge);
                       // CVInventory.ReportSource = obj._StusAge;
                    if (Session["GlbReportName"].ToString() == "ItmBrndwiseItemAge.rpt")
                        PrintPDF(targetFileName, obj._ItmBrndAge);
                       // CVInventory.ReportSource = obj._ItmBrndAge;

                    //CVInventory.RefreshReport();
                    //CVInventory.HasExportButton = true;
                }
                if (Session["GlbReportName"].ToString() == "AgeMonitoring.rpt")
                {
                    obj.Age_Monitoring(_objRepoPara);
                    PrintPDF(targetFileName, obj._ageMonit);
                    //CVInventory.ReportSource = obj._ageMonit;
                    //CVInventory.RefreshReport();
                }
                if (Session["GlbReportName"].ToString() == "abscourier.rpt")
                {
                    string Refdoc = Session["RefDoc"].ToString();
                    string COM = Session["UserCompanyCode"] as string;
                    string pc = Session["UserDefProf"] as string;
                    string user = Session["UserID"] as string;
                    string type = Session["Type"] as string;
                    obj.get_courierdata(Refdoc, COM, type, user);
                    CVInventory.ReportSource = obj._abscour;
                    CVInventory.RefreshReport();
                }
                if (Session["GlbReportName"].ToString() == "AllocationDetails.rpt")
                {
                    string doc = Session["DocNo"].ToString();
                    string COM = Session["Company"] as string;
                    // string pc = Session["UserDefProf"] as string;
                    string Chanal = Session["Chanal"] as string;
                    string Location = Session["Location"] as string;
                    string Mcat = Session["Mcat"] as string;
                    string Scat = Session["Scat"] as string;
                    string range = Session["Range"] as string;
                    string ItemCode = Session["ItemCode"] as string;
                    string Model = Session["Model"] as string;
                    string Brand = Session["Brand"] as string;
                    string FrmDate = Session["FrmDate"] as string;
                    string ToDt = Session["ToDt"] as string;
                    string user = Session["UserID"] as string;
                    obj.AllocationDetails(COM, Chanal, Location, Mcat, Scat, range, ItemCode, Model, Brand, FrmDate, ToDt, user, doc);
                    CVInventory.ReportSource = obj._allocDet;
                    CVInventory.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "rpt_GLB_Git_Document.rpt")
                {
                    obj.currentGIT(_objRepoPara);
                    CVInventory.ReportSource = obj.rptgit;
                    CVInventory.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "rpt_Bond_Balance_Report1.rpt")
                {
                    obj.liabilityReport(_objRepoPara);
                    CVInventory.ReportSource = obj._LiableRep;
                    CVInventory.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "RepWarrantyCard_AOA.rpt")
                {
                    obj.Print_AOA_Warra(_objRepoPara);
                    //CVInventory.ReportSource = obj._warraPrint;
                    obj._warraPrint.PrintOptions.PrinterName = GetDefaultPrinter();
                    obj._warraPrint.PrintToPrinter(1, false, 0, 0);
                    //CVInventory.RefreshReport();
                  

                }

                if (Session["GlbReportName"].ToString() == "cr_locationdetails.rpt")
                {
                    // List<string> LOCVALUE = new List<string>();
                    // LOCVALUE = (List<string>)Session["LocationList"];
                    string LOCVALUE = Session["LocationList"] as string;
                    string COM = Session["Company"] as string;
                    string Chanal = Session["Chanal"] as string;
                    string Location = Session["Location"] as string;
                    obj.LocationDetails(COM, Chanal, Location, LOCVALUE);
                    CVInventory.ReportSource = obj._Locationdet;
                    CVInventory.RefreshReport();
                }
                if (Session["GlbReportName"].ToString() == "item_profile.rpt")
                {

                    string COM = Session["Company"] as string;
                    string Location = Session["Location"] as string;
                    string cat1 = Session["Cat1"] as string;
                    string cat2 = Session["Cat2"] as string;
                    string cat3 = Session["Cat3"] as string;
                    string cat4 = Session["Cat4"] as string;
                    string cat5 = Session["Cat5"] as string;
                    string code = Session["Code"] as string;
                    string brand = Session["Brand"] as string;
                    string model = Session["Model"] as string;
                    string user = Session["UserId"] as string;
                    string act = "";
                    obj.ItemProfileDetails(COM, user, Location, cat1, cat2, cat3, cat4, cat5, code, brand, model, act);
                    CVInventory.ReportSource = obj._itemprofile;
                    CVInventory.RefreshReport();
                }
                if (Session["GlbReportName"].ToString() == "RequistionNote.rpt")
                {
                    string com = Session["UserCompanyCode"].ToString();
                    string reqno = Session["RequestNo"].ToString();
                    string loginuser = Session["UserID"].ToString();
                    string loginloc = Session["UserDefLoca"].ToString();

                    obj.RequistionNote(com,reqno,loginuser,loginloc, string.Empty);
                    CVInventory.ReportSource = obj._ReqestionNote;
                    CVInventory.RefreshReport();
                }

                if (Session["GlbReportName"].ToString() == "WarrPrint_abl.rpt")
                {
                    PrintPDF(targetFileName, obj._warrprint);
                }

                String url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                BaseCls.GlbReportTp = string.Empty;
                
            }
            catch (Exception err)
            {
                if (CVInventory != null)
                {
                    CVInventory.Dispose();
                }
                CHNLSVC.CloseChannel();
                Response.Redirect("~/Error.aspx?Error=" + err.Message + "");
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        public void PrintPDF(string targetFileName,ReportDocument _rpt)
        {
            try
            { 
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

        private object File(Stream stream, string p)
        {
            throw new NotImplementedException();
        }

        private void set_Report_Para(InvReportPara _obj)
        {
            _objRepoPara = _obj;
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