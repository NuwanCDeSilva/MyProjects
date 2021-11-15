using CrystalDecisions.Shared;
using FF.BusinessObjects;
using FF.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using FastForward.WebAbansTours.Models;
using FF.BusinessObjects.ToursNew;
using System.Security.Authentication;
using System.Text;

namespace FastForward.WebAbansTours.Controllers
{
    public class ReportingController : BaseController
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1025);
                if (per.MNU_ID == 0)
                {
                   // throw new AuthenticationException("You do not have the necessary permission to perform this action");
                }
            }
            else
            {
                Redirect("~/Login/index");
            }

        }
        // GET: Reports
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                return View();
            }
            else
            {
                return Redirect("~/Login");
            }
        }

        public ActionResult SalesReports()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                return View();
            }
            else
            {
                return Redirect("~/Login");
            }
        }
        public ActionResult Download(List<string> selectedcompany, List<string> selectedprofcen, FormCollection collection)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    Session["ReportData"] = null;
                    string content;
                    ReportViewerViewModel model = new ReportViewerViewModel();
                    content = Url.Content("~/Reports/ReportViwer/SalesReports.aspx");
                    model.ReportPath = content;
                    model.reportName = collection["REPORT_TYPE"];
                    model.frmDate = (collection["FromDate"] != "") ? Convert.ToDateTime(collection["FromDate"]) : DateTime.Now;
                    model.todate = (collection["ToDate"] != "") ? Convert.ToDateTime(collection["ToDate"]) : DateTime.Now;
                    model.Customer = collection["Customer"];

                    model.selectCom = (selectedcompany != null) ? selectedcompany[0] : "";
                    model.seleUserDefPro = (selectedprofcen != null) ? selectedprofcen[0] : "";
                    model.itemCd = "";
                    model.UserId = userId;

                    model.EnquiryNumber = String.Empty;
                    model.EnquiryStatus = 1;
                    model.EnquiryType = string.Empty;
                    Session["ReportData"] = model;
                    switch (model.reportName)
                    {
                        case "rptdailysales":
                            model.reportData = CHNLSVC.Tours.Get_DailySalesReport(model.frmDate, model.todate, model.Customer, model.itemCd, model.selectCom, model.seleUserDefPro, userId);
                            break;
                        case "rptreceiptdtl":
                            model.reportData = CHNLSVC.Tours.Get_ReceiptDetailReport(model.frmDate, model.todate, model.Customer, model.selectCom, model.seleUserDefPro, userId);
                            break;
                        case "rptsalescomm":
                            model.reportData = null;
                            break;
                        case "DebtorSettlement":
                            model.reportData = CHNLSVC.Tours.Get_DebtorStatementReport(model.frmDate, model.todate, model.Customer, model.selectCom, model.seleUserDefPro, userId);
                            break;
                        case "rptdailysales_dtl":
                            model.reportData = CHNLSVC.Tours.Get_DailySalesDetailReport(model.frmDate, model.todate, model.Customer, model.itemCd, model.selectCom, model.seleUserDefPro, userId);
                            break;
                        case "pendinginquiry":
                            model.reportData = CHNLSVC.Tours.Get_ATSInquiryReport(model.frmDate, model.todate, model.Customer, model.EnquiryNumber, model.EnquiryStatus, model.EnquiryType, model.selectCom, model.seleUserDefPro, userId);
                            break;
                        default:
                            ViewData["Error"] = "Invalid report selected.";
                            return View("Download");
                    }

                    return View("ReportViwer", model);
                }
                else
                {
                    return Redirect("~/Login");
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message.ToString();
                return View("Download");
            }
        }

        public ActionResult ReportViwer(ReportViewerViewModel model)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return View(model);
                }
                else
                {
                    return Redirect("~/Login");
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message.ToString();
                return View("Download");
            }
        }
        public bool GenerateReport(string fileName, string reportName, DataTable dt, string fileType)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    ReportClass rptH = new ReportClass();

                    rptH.FileName = Server.MapPath("/Reports/" + reportName + ".rpt");
                    rptH.Load();
                    rptH.SetDataSource(dt);
                    ExportFormatType formatType = ExportFormatType.NoFormat;
                    Stream stream;
                    switch (fileType)
                    {
                        case "PDF":
                            formatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                            fileName = fileName + ".pdf";
                            stream = rptH.ExportToStream(formatType);
                            File(stream, "application/pdf", fileName);
                            break;
                        case "Excel":
                            formatType = CrystalDecisions.Shared.ExportFormatType.Excel;
                            fileName = fileName + ".xls";
                            stream = rptH.ExportToStream(formatType);
                            File(stream, "application/ms-excel", fileName);
                            break;
                        case "CSV":
                            formatType = CrystalDecisions.Shared.ExportFormatType.CharacterSeparatedValues;
                            fileName = fileName + ".csv";
                            stream = rptH.ExportToStream(formatType);
                            File(stream, "application/vnd.ms-excel", fileName);
                            break;
                        default:
                            formatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                            fileName = fileName + ".pdf";
                            stream = rptH.ExportToStream(formatType);
                            File(stream, "application/pdf", fileName);
                            break;
                    }

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public JsonResult loadCompany()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<SystemUserCompany> _systemUserCompanyList = CHNLSVC.Security.GetUserCompany(userId.ToUpper());
                    if (_systemUserCompanyList != null)
                    {
                        return Json(new { success = true, login = true, companyList = _systemUserCompanyList }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No company found for display" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult loadProfitCenters(string channel, string subChanel, string proCen)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<string> lstPC = new List<string>();
                    string _masterLocation = (string.IsNullOrEmpty(userDefLoc)) ? userDefPro : userDefLoc;
                    if (CHNLSVC.Security.Is_OptionPerimitted(company, userId.ToUpper(), 10044))
                    {
                        DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(company, channel, subChanel, null, null, null, proCen);
                        foreach (DataRow drow in dt.Rows)
                        {
                            lstPC.Add(drow["PROFIT_CENTER"].ToString());
                        }
                        return Json(new { success = true, login = true, profCen = lstPC }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(userId, company, channel, subChanel, null, null, null, proCen);
                        foreach (DataRow drow in dt.Rows)
                        {
                            lstPC.Add(drow["PROFIT_CENTER"].ToString());
                        }
                        return Json(new { success = true, login = true, profCen = lstPC }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        //ISURU 2017/02/22
        public ActionResult OperationReports()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                return View();
            }
            else
            {
                return Redirect("~/Login");
            }
        }

        public ActionResult RequestPrint(List<string> selectedcompany, List<string> selectedprofcen, FormCollection collection)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    Session["ReportData"] = null;

                    ReportViewerViewModel model = new ReportViewerViewModel();
                    ReportDocument rd = new ReportDocument();

                    model.reportName = collection["rad_select"];
                    model.frmDate = Convert.ToDateTime(collection["FromDate"]);
                    model.todate = Convert.ToDateTime(collection["ToDate"]);
                    string combindedString1 = string.Join(",", selectedcompany.ToArray());
                    string combindedString2 = string.Join(",", selectedprofcen.ToArray());
                    string fleet = collection["TLH_FLEET"];

                    model.itemCd = "";
                    model.UserId = userId;

                    model.EnquiryNumber = String.Empty;
                    model.EnquiryStatus = 1;
                    model.EnquiryType = string.Empty;
                    Session["ReportData"] = model;
                    switch (model.reportName)
                    {
                        case "log_sheet":
                            DataTable dt1 = CHNLSVC.Tours.GET_LOGSHEET_DATA(model.frmDate, model.todate, combindedString1, combindedString2);
                            rd.Load(Server.MapPath("/Reports/" + "rpt_logsheet_report.rpt"));
                            rd.Database.Tables["LogSheetData"].SetDataSource(dt1);
                            Response.Buffer = false;
                            Response.ClearContent();
                            Response.ClearHeaders();
                            try
                            {
                                this.Response.Clear();
                                this.Response.ContentType = "application/pdf";
                                Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            break;
                        case "Trip_Request":
                            DataTable dt2 = CHNLSVC.Tours.GET_TRIPREQUEST_DATA(model.frmDate, model.todate, combindedString1, combindedString2);
                            rd.Load(Server.MapPath("/Reports/" + "rpt_triprequest_report.rpt"));
                            rd.Database.Tables["TripRequestData"].SetDataSource(dt2);
                            Response.Buffer = false;
                            Response.ClearContent();
                            Response.ClearHeaders();
                            try
                            {
                            this.Response.Clear();
                            this.Response.ContentType = "application/pdf";
                            Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
                            }
                            catch (Exception ex)
                            {
                            throw;
                            }
                            break;
                        case "Lease_Car":
                            DataTable dt3 = CHNLSVC.Tours.LEASED_CAR_DATA(model.frmDate, model.todate, combindedString1, fleet);
                            rd.Load(Server.MapPath("/Reports/" + "rpt_leasedcar_report.rpt"));
                            rd.Database.Tables["LeasedCarData"].SetDataSource(dt3);
                            Response.Buffer = false;
                            Response.ClearContent();
                            Response.ClearHeaders();
                            try
                            {
                            this.Response.Clear();
                            this.Response.ContentType = "application/pdf";
                            Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
                            }
                            catch (Exception ex)
                            {
                            throw;
                            }
                            break;
                        default:
                            ViewData["Error"] = "Invalid report selected.";
                            return View("RequestPrint");

                    }
                    return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                }
                else
                {
                    return Redirect("~/Login");
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message.ToString();
                return View("RequestPrint");
            }
        }  

    }
}