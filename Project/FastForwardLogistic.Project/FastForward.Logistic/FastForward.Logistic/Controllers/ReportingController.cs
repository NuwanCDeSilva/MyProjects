using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.Shared;
using FF.BusinessObjects;
//using FF.Resources;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
//using FastForward.WebAbansTours.Models;
//using FF.BusinessObjects.ToursNew;
using System.Security.Authentication;
using System.Text;
using FastForward.Logistic.Models;
using FF.BusinessObjects.Security;

namespace FastForward.Logistic.Controllers
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
                //ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1025);
                //if (per.MNU_ID == 0)
                //{
                //    throw new AuthenticationException("You do not have the necessary permission to perform this action");
                //}
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
                            model.reportData = null;
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
                    //if (CHNLSVC.Security.Is_OptionPerimitted(company, userId.ToUpper(), 10044))
                    //{
                    //    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(company, channel, subChanel, null, null, null, proCen);
                    //    foreach (DataRow drow in dt.Rows)
                    //    {
                    //        lstPC.Add(drow["PROFIT_CENTER"].ToString());
                    //    }
                    //    return Json(new { success = true, login = true, profCen = lstPC }, JsonRequestBehavior.AllowGet);
                    //}
                    return Json(new { success = true, login = true, profCen = lstPC }, JsonRequestBehavior.AllowGet);
                    //else
                    //{
                    //    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(userId, company, channel, subChanel, null, null, null, proCen);
                    //    foreach (DataRow drow in dt.Rows)
                    //    {
                    //        lstPC.Add(drow["PROFIT_CENTER"].ToString());
                    //    }
                    //    return Json(new { success = true, login = true, profCen = lstPC }, JsonRequestBehavior.AllowGet);
                    //}
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
            string err_option = "";
            string err_form = "";

            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    Session["ReportData"] = null;
                    MasterCompany _msCom = CHNLSVC.Security.GetCompByCode(Session["UserCompanyCode"].ToString());
                    ReportViewerViewModel model = new ReportViewerViewModel();
                    ReportDocument rd = new ReportDocument();
                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();
                    DataTable dt4 = new DataTable();

                    model.reportName = collection["rad_select"];
                    model.groupType = collection["rpt_select"];
                    model.frmDate = Convert.ToDateTime(collection["FromDate"]);
                    model.todate = Convert.ToDateTime(collection["ToDate"]);
                    model.asatdate = Convert.ToDateTime(collection["AsAtDate"]);
                    if(collection["DocNo"] !="")
                    {
                        model.EnquiryNumber = collection["DocNo"];
                    }

                    if (collection["Bl_m_doc_no"] != "")
                    {
                        model.EnquiryNumber = collection["Bl_m_doc_no"];
                    }
                    if (collection["Bl_h_doc_no"] != "")
                    {
                        model.EnquiryNumber = collection["Bl_h_doc_no"];
                    }

                    
                    //model.EnquiryNumber = collection["Bl_m_doc_no"];
                    //model.EnquiryNumber = collection["Bl_h_doc_no"];
                    
                    //string combindedString1 = string.Join(",", selectedcompany.ToArray());
                    

                    string fleet = collection["TLH_FLEET"];
                    string customer_Code = collection["cus_cd"];
                    string proCnt_Code = collection["ProfitCenter"];
                    string cus_Type = collection["CusType"] == "" ? "All" : collection["CusType"];
                    string pay_Type = collection["CreateUser"] == "" ? "All" : collection["CreateUser"];
                    string username = collection["PayType"] == "" ? "All" : collection["PayType"];
                    if (collection["job-number"] != "")
                    {
                        model.JobNo = collection["job-number"];
                    }

                    if (collection["invno"] != "")
                    {
                        model.InvNo = collection["invno"];
                    }

                    model.itemCd = "";
                    model.UserId = userId;

                    model.EnquiryStatus = 1;
                    model.EnquiryType = string.Empty;
                    Session["ReportData"] = model;
                    switch (model.reportName)
                    {
                        case "manifest_Letter":
                            err_option = "RPT-manifest_Letter-PDF";
                            err_form = "manifest_Letter";

                            dt1 = CHNLSVC.Sales.GetManifestLetter_Dtl(model.frmDate, model.todate, model.EnquiryNumber, company);
                            rd.Load(Server.MapPath("/Reports/" + "rpt_ManifestLetter.rpt"));
                            rd.Database.Tables["ManiFestLetter"].SetDataSource(dt1);

                            var dt_enum = dt1.AsEnumerable();
                            DataTable param = new DataTable();
                            param.Clear();
                            param.Columns.Add("vesselvoyage", typeof(string));
                            param.Columns.Add("bl_shipper_name", typeof(string));
                            param.Columns.Add("ComAddress", typeof(string));

                            DataRow dr;
                            dr = param.NewRow();
                            var vv = (from r in dt_enum
                                    where r.Field<string>("vesselvoyage") != ""
                                    select r.Field<string>("vesselvoyage")).First<string>();
                            var shpname = (from r in dt_enum
                                           where r.Field<string>("bl_shipper_name") != ""
                                           select r.Field<string>("bl_shipper_name")).First<string>();
                            var comadrs = (from r in dt_enum
                                           where r.Field<string>("ComAddress") != ""
                                           select r.Field<string>("ComAddress")).First<string>();

                            dr["vesselvoyage"] = vv;
                            dr["bl_shipper_name"] = shpname;
                            dr["ComAddress"] = comadrs;

                            param.Rows.Add(dr);

                            rd.Database.Tables["ManiFestLetterParam"].SetDataSource(param);
                            
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
                        case "cargo_manifest":

                            err_option = "RPT-cargo_manifest-PDF";
                            err_form = "cargo_manifest";

                            dt1 = CHNLSVC.Sales.Get_CargoManifest_Dtl(model.frmDate, model.todate, model.EnquiryNumber, company);
                            dt2 = CHNLSVC.Sales.Get_Container_Dtl(model.EnquiryNumber);
                            rd.Load(Server.MapPath("/Reports/" + "CargoManifest.rpt"));
                            rd.Database.Tables["CargoManifest"].SetDataSource(dt1);

                            foreach (object repOp in rd.ReportDefinition.ReportObjects)
                            {
                                string _s = repOp.GetType().ToString();
                                if (_s.ToLower().Contains("subreport"))
                                {
                                    SubreportObject _cs = (SubreportObject)repOp;
                                    if (_cs.SubreportName == "Container")
                                    {
                                        ReportDocument subRepDoc = rd.Subreports[_cs.SubreportName];
                                        subRepDoc.Database.Tables["ContainerDtls"].SetDataSource(dt2);
                                    }
                                }
                            }
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
                        case "delivery_order":

                            err_option = "RPT-delivery_order-PDF";
                            err_form = "delivery_order";

                            dt1 = CHNLSVC.Sales.Get_DeliveryOrder_Dtl(model.frmDate, model.todate, model.EnquiryNumber, company);
                            dt2 = CHNLSVC.Sales.Get_Container_Dtl(model.EnquiryNumber);
                            dt3 = CHNLSVC.Sales.Get_Container_Dtlcount(model.EnquiryNumber);
                            string error = "";
                            if (dt1.Rows.Count>0)
                            {
                                Int32 eff = CHNLSVC.Sales.updateReprintDocStus_New(model.EnquiryNumber, company, out error);
                                rd.Load(Server.MapPath("/Reports/" + "rpt_DeliveryOrder.rpt"));
                            }
                            else
                            {
                                rd.Load(Server.MapPath("/Reports/" + "rpt_DeliveryOrderCopy.rpt"));
                            }

                            //rd.Load(Server.MapPath("/Reports/" + "rpt_DeliveryOrder.rpt"));
                            rd.Database.Tables["DeliveryOrder"].SetDataSource(dt1);
                            foreach (object repOp in rd.ReportDefinition.ReportObjects)
                            {
                                string _s = repOp.GetType().ToString();
                                if (_s.ToLower().Contains("subreport"))
                                {
                                    SubreportObject _cs = (SubreportObject)repOp;
                                    if (_cs.SubreportName == "Container")
                                    {
                                        ReportDocument subRepDoc = rd.Subreports[_cs.SubreportName];
                                        subRepDoc.Database.Tables["ContainerDtls"].SetDataSource(dt2);
                                    }
                                    if (_cs.SubreportName == "ContainerCount")
                                    {
                                        ReportDocument subRepDoc = rd.Subreports[_cs.SubreportName];
                                        subRepDoc.Database.Tables["ContainerDtls"].SetDataSource(dt2);
                                        ReportDocument subRepDoc1 = rd.Subreports[_cs.SubreportName];
                                        subRepDoc1.Database.Tables["ContainerDtlscount"].SetDataSource(dt3);
                                    }
                                }
                            }
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
                        case "draft":

                            err_option = "RPT-draft-PDF";
                            err_form = "draft";

                            dt1 = CHNLSVC.Sales.Get_Draft_Dtl(model.frmDate, model.todate, model.EnquiryNumber, company);
                            dt2 = CHNLSVC.Sales.Get_Container_Dtl(model.EnquiryNumber);
                            rd.Load(Server.MapPath("/Reports/" + "rpt_Draft.rpt"));
                            rd.Database.Tables["Draft"].SetDataSource(dt1);
                            foreach (object repOp in rd.ReportDefinition.ReportObjects)
                            {
                                string _s = repOp.GetType().ToString();
                                if (_s.ToLower().Contains("subreport"))
                                {
                                    SubreportObject _cs = (SubreportObject)repOp;
                                    if (_cs.SubreportName == "rptContainer")
                                    {
                                        ReportDocument subRepDoc = rd.Subreports[_cs.SubreportName];
                                        subRepDoc.Database.Tables["ContainerDtls"].SetDataSource(dt2);
                                    }
                                }
                            }
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
                        case "house":

                            err_option = "RPT-house-PDF";
                            err_form = "house";

                            dt1 = CHNLSVC.Sales.Get_House_Dtl(model.frmDate, model.todate, model.EnquiryNumber, company);
                            rd.Load(Server.MapPath("/Reports/" + "rpt_AirWayBill.rpt"));
                            rd.Database.Tables["House"].SetDataSource(dt1);
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
                        //case "salesWithGP":

                        //    err_option = "RPT-salesWithGP-PDF";
                        //    err_form = "salesWithGP";

                        //    dt1 = CHNLSVC.Sales.Get_Sales_GPProduct(model.frmDate, model.todate, model.EnquiryNumber, company);
                        //    //dt1 = CHNLSVC.Sales.Get_Sales_Dtl(model.frmDate, model.todate, model.EnquiryNumber, company);
                        //    rd.Load(Server.MapPath("/Reports/" + "rpt_Sales.rpt"));
                        //    rd.Database.Tables["Sales"].SetDataSource(dt1);
                        //    rd.SetParameterValue("grpOpt", model.groupType == "sales_man" ? "S" : model.groupType == "cus_wise" ? "C" : "P");
                        //    rd.SetParameterValue("userID", userId);
                        //    rd.SetParameterValue("frmDate", model.frmDate);
                        //    rd.SetParameterValue("toDate", model.todate);
                        //    rd.SetParameterValue("comName", _msCom.Mc_desc);
                        //    Response.Buffer = false;
                        //    Response.ClearContent();
                        //    Response.ClearHeaders();
                        //    try
                        //    {
                        //        this.Response.Clear();
                        //        this.Response.ContentType = "application/pdf";
                        //        Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        throw;
                        //    }
                        //    break;
                        case "salesWithGP":

                            err_option = "RPT-salesWithGP-PDF";
                            err_form = "salesWithGP";

                            if (model.groupType == "sales_man")
                            {
                                dt1 = CHNLSVC.Sales.Get_Sales_GPSales(model.frmDate, model.todate, model.EnquiryNumber, company);
                                rd.Load(Server.MapPath("/Reports/" + "rpt_GP_ProductEx.rpt"));
                            }
                            else if (model.groupType == "pro_wise")
                            {
                                dt1 = CHNLSVC.Sales.Get_Sales_GPProduct(model.frmDate, model.todate, model.EnquiryNumber, company);
                                rd.Load(Server.MapPath("/Reports/" + "rpt_GP_Product.rpt"));
                            }
                            //dt1 = CHNLSVC.Sales.Get_Sales_GPProduct(model.frmDate, model.todate, model.EnquiryNumber, company);
                            //dt1 = CHNLSVC.Sales.Get_Sales_Dtl(model.frmDate, model.todate, model.EnquiryNumber, company);
                            //rd.Load(Server.MapPath("/Reports/" + "rpt_GP_Product.rpt"));
                            rd.Database.Tables["GPProduct"].SetDataSource(dt1);
                            rd.SetParameterValue("grpOpt", model.groupType == "sales_man" ? "S" : model.groupType == "cus_wise" ? "C" : "P");
                            rd.SetParameterValue("userID", userId);
                            rd.SetParameterValue("frmDate", model.frmDate);
                            rd.SetParameterValue("toDate", model.todate);
                            rd.SetParameterValue("comName", _msCom.Mc_desc);
                            Response.Buffer = false;
                            Response.ClearContent();
                            Response.ClearHeaders();
                            try
                            {
                                //this.Response.Clear();
                                //this.Response.ContentType = "application/pdf";
                                //Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
                                var fName = Server.MapPath("~") + "Export\\SalesWithGP-" + Session["UserID"].ToString() + ".xls";
                                rd.ExportToDisk(ExportFormatType.Excel, fName);
                                return File(fName, "application/vnd.ms-excel", "SalesWithGP-" + Session["UserID"].ToString() + ".xls");
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            break;
                        case "debtorsOutstanding":

                            err_option = "RPT-debtorsOutstanding-PDF";
                            err_form = "debtorsOutstanding";

                            dt1 = CHNLSVC.Sales.Get_Debtors_Outstanding(model.frmDate, model.todate, customer_Code, company, proCnt_Code, userId);
                            rd.Load(Server.MapPath("/Reports/" + "rpt_Debtors_Outstanding.rpt"));
                            rd.Database.Tables["DebtorsOutstanding"].SetDataSource(dt1);
                            rd.SetParameterValue("userID", userId);
                            rd.SetParameterValue("frmDate", model.frmDate);
                            rd.SetParameterValue("toDate", model.todate);
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
                        //Tharindu 2017-12-26
                        case "cashOutstanding":

                            err_option = "RPT-cashOutstanding-EXCEL";
                            err_form = "cashOutstanding";

                            dt1 = CHNLSVC.Sales.Get_Cash_Outstanding(model.frmDate, model.todate, company, proCnt_Code);
                            rd.Load(Server.MapPath("/Reports/" + "rpt_CashAdvanceOutstanding.rpt"));
                            rd.Database.Tables["CashOutstanding"].SetDataSource(dt1);
                            
                            rd.SetParameterValue("userID", userId);
                            rd.SetParameterValue("frmDate", model.frmDate);
                            rd.SetParameterValue("toDate", model.todate);
                            rd.SetParameterValue("comName", _msCom.Mc_desc);
                            rd.SetParameterValue("Add1", _msCom.Mc_add1);
                            rd.SetParameterValue("Add2", _msCom.Mc_add2);
                            rd.SetParameterValue("Tel", _msCom.Mc_tel);

                            Response.Buffer = false;
                            Response.ClearContent();
                            Response.ClearHeaders();
                            try
                            {                               
                                var fName = Server.MapPath("~") + "Export\\CashAdvancesOutstanding-" + Session["UserID"].ToString() + ".xls";
                                rd.ExportToDisk(ExportFormatType.Excel, fName);
                                return File(fName, "application/vnd.ms-excel", "CashAdvancesOutstanding-" + Session["UserID"].ToString() + ".xls");
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            break;
                        //Tharindu 2017-12-29
                        case "colsumrpt":

                            err_option = "RPT-colsumrpt-PDF";
                            err_form = "colsumrpt";

                            dt1 = CHNLSVC.Sales.Get_Collection_Summary(model.frmDate, model.todate, company, proCnt_Code, model.CustomerType, model.PayType, model.UserName);
                            rd.Load(Server.MapPath("/Reports/" + "rpt_CollectionSummary.rpt"));
                            rd.Database.Tables["CollectionSmmary"].SetDataSource(dt1);

                            rd.SetParameterValue("UserID", userId);
                            rd.SetParameterValue("fromdate", model.frmDate);
                            rd.SetParameterValue("todate", model.todate);
                            rd.SetParameterValue("pc", proCnt_Code);
                            rd.SetParameterValue("ComName", _msCom.Mc_desc);
                            rd.SetParameterValue("Add1", _msCom.Mc_add1);
                            rd.SetParameterValue("Add2", _msCom.Mc_add2);
                            rd.SetParameterValue("Tel", _msCom.Mc_tel);
                            rd.SetParameterValue("pc", proCnt_Code == "" ? "All" : proCnt_Code);
                            rd.SetParameterValue("Type", cus_Type);
                            rd.SetParameterValue("PayType", pay_Type);
                            rd.SetParameterValue("UserType",username);

                            Response.Buffer = false;
                            Response.ClearContent();
                            Response.ClearHeaders();
                            try
                            {
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
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            break;
                            //THarindu 2018-01-05
                            case "InvoiceAuditTrail":

                            err_option = "RPT-InvoiceAuditTrail-PDF";
                            err_form = "InvoiceAuditTrail";

                            dt1 = CHNLSVC.Sales.GetInvoiceAuditTrail(model.frmDate, model.todate, company, proCnt_Code, customer_Code);
                       
                            rd.Load(Server.MapPath("/Reports/" + "rpt_InventoryAuditTrial.rpt"));
                            rd.Database.Tables["InvoiceAuditTrail"].SetDataSource(dt1);
                          
                            rd.SetParameterValue("userID", userId);
                            rd.SetParameterValue("frmDate", model.frmDate);
                            rd.SetParameterValue("todate", model.todate);
                           // rd.SetParameterValue("pc", proCnt_Code);
                            rd.SetParameterValue("ComName", _msCom.Mc_desc);
                            rd.SetParameterValue("Add1", _msCom.Mc_add1);
                            rd.SetParameterValue("Add2", _msCom.Mc_add2);
                            rd.SetParameterValue("Tel", _msCom.Mc_tel);


                            Response.Buffer = false;
                            Response.ClearContent();
                            Response.ClearHeaders();
                            try
                            {
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
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }

                            break;

                            //THarindu 2018-01-12
                            case "FrightArrivalNotice":

                            err_option = "RPT-FrightArrivalNotice-PDF";
                            err_form = "FrightArrivalNotice";

                            dt1 = CHNLSVC.Sales.Get_jb_header_new(company, model.JobNo, model.EnquiryNumber);
                            dt2 = CHNLSVC.Sales.GetfrightChargePayble(company,model.EnquiryNumber,model.InvNo);
                            dt3 = CHNLSVC.Sales.GetrptContainerDetails(company, model.EnquiryNumber);
                            dt4 = CHNLSVC.Sales.getCompanyDetailsBycd(company);

                            rd.Load(Server.MapPath("/Reports/" + "rpt_frieghtArrivalNotice.rpt"));
                   
                            rd.Database.Tables["JobHeader"].SetDataSource(dt1);
                            rd.Database.Tables["FrightChgPayble"].SetDataSource(dt2);
                            rd.Database.Tables["ContainerPkgdtls"].SetDataSource(dt3);
                            rd.Database.Tables["COMPANY"].SetDataSource(dt4);

                            Response.Buffer = false;
                            Response.ClearContent();
                            Response.ClearHeaders();
                            try
                            {
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
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }

                            break;
                            //dilshan 
                            case "debtorsOutstandingReport":

                            err_option = "RPT-debtorsOutstandingReport-EXCEL";
                            err_form = "debtorsOutstandingReport";

                            //dt1 = CHNLSVC.Sales.Get_Debtors_Out(model.frmDate, model.todate, customer_Code, company, proCnt_Code, userId);
                            dt1 = CHNLSVC.Sales.Get_Debtors_Out(model.frmDate, model.asatdate, customer_Code, company, proCnt_Code, userId);
                            rd.Load(Server.MapPath("/Reports/" + "rpt_Debtors_Out.rpt"));
                            rd.Database.Tables["DebtorsOutstandingRpt"].SetDataSource(dt1);
                            rd.SetParameterValue("userID", userId);
                            rd.SetParameterValue("frmDate", model.frmDate);
                            rd.SetParameterValue("toDate", model.todate);
                            rd.SetParameterValue("asatDate", model.asatdate);
                            Response.Buffer = false;
                            Response.ClearContent();
                            Response.ClearHeaders();
                            try
                            {
                                //this.Response.Clear();
                                //this.Response.ContentType = "application/pdf";
                                //Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");

                                var fName = Server.MapPath("~") + "Export\\DebtorsOutstandingRpt-" + Session["UserID"].ToString() + ".xls";
                                rd.ExportToDisk(ExportFormatType.Excel, fName);
                                return File(fName, "application/vnd.ms-excel", "DebtorsOutstandingRpt-" + Session["UserID"].ToString() + ".xls");
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            break;

                            //dilshan 
                            case "jobStatusReport":

                            err_option = "RPT-jobStatusReport-EXCEL";
                            err_form = "jobStatusReport";

                            //dt1 = CHNLSVC.Sales.Get_Debtors_Out(model.frmDate, model.asatdate, customer_Code, company, proCnt_Code, userId);
                            dt1 = CHNLSVC.Sales.Get_Job_Status_Detail(model.frmDate, model.todate, company, proCnt_Code, model.groupType);
                            rd.Load(Server.MapPath("/Reports/" + "rpt_JobStatusReport.rpt"));
                            //rd.Database.Tables["DebtorsOutstandingRpt"].SetDataSource(dt1);
                            //rd.SetParameterValue("userID", userId);
                            //rd.SetParameterValue("frmDate", model.frmDate);
                            //rd.SetParameterValue("toDate", model.todate);
                            //rd.SetParameterValue("asatDate", model.asatdate);

                            rd.Database.Tables["JobStatusDetails"].SetDataSource(dt1);
                            
                            rd.SetParameterValue("userID", userId);
                            rd.SetParameterValue("frmDate", model.frmDate);
                            rd.SetParameterValue("toDate", model.todate);
                            rd.SetParameterValue("comName", _msCom.Mc_desc);
                            rd.SetParameterValue("Add1", _msCom.Mc_add1);
                            rd.SetParameterValue("Add2", _msCom.Mc_add2);
                            rd.SetParameterValue("Tel", _msCom.Mc_tel);

                            Response.Buffer = false;
                            Response.ClearContent();
                            Response.ClearHeaders();
                            try
                            {
                                //this.Response.Clear();
                                //this.Response.ContentType = "application/pdf";
                                //Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");

                                var fName = Server.MapPath("~") + "Export\\JobStatusDetails-" + Session["UserID"].ToString() + ".xls";
                                rd.ExportToDisk(ExportFormatType.Excel, fName);
                                return File(fName, "application/vnd.ms-excel", "JobStatusDetails-" + Session["UserID"].ToString() + ".xls");
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            break;

                            //dilshan 02/10/2018
                            case "IRDReport":

                            err_option = "RPT-IRDReport-EXCEL";
                            err_form = "IRDReport";

                            //dt1 = CHNLSVC.Sales.Get_Debtors_Out(model.frmDate, model.asatdate, customer_Code, company, proCnt_Code, userId);
                            dt1 = CHNLSVC.Sales.Get_IRD_Detail(model.frmDate, model.todate, company, proCnt_Code, model.groupType);
                            rd.Load(Server.MapPath("/Reports/" + "rpt_IRDReport.rpt"));
                            //rd.Database.Tables["DebtorsOutstandingRpt"].SetDataSource(dt1);
                            //rd.SetParameterValue("userID", userId);
                            //rd.SetParameterValue("frmDate", model.frmDate);
                            //rd.SetParameterValue("toDate", model.todate);
                            //rd.SetParameterValue("asatDate", model.asatdate);

                            rd.Database.Tables["IRDDetails"].SetDataSource(dt1);

                            rd.SetParameterValue("userID", userId);
                            rd.SetParameterValue("frmDate", model.frmDate);
                            rd.SetParameterValue("toDate", model.todate);
                            rd.SetParameterValue("comName", _msCom.Mc_desc);
                            rd.SetParameterValue("Add1", _msCom.Mc_add1);
                            rd.SetParameterValue("Add2", _msCom.Mc_add2);
                            rd.SetParameterValue("Tel", _msCom.Mc_tel);

                            Response.Buffer = false;
                            Response.ClearContent();
                            Response.ClearHeaders();
                            try
                            {
                                //this.Response.Clear();
                                //this.Response.ContentType = "application/pdf";
                                //Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");

                                var fName = Server.MapPath("~") + "Export\\IRDReportDetails-" + Session["UserID"].ToString() + ".xls";
                                rd.ExportToDisk(ExportFormatType.Excel, fName);
                                return File(fName, "application/vnd.ms-excel", "IRDReportDetails-" + Session["UserID"].ToString() + ".xls");
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            break;

                            //dilshan 16/11/2018
                            case "MarketingReport":

                            err_option = "RPT-MarketingReport-EXCEL";
                            err_form = "MarketingReport";

                            //dt1 = CHNLSVC.Sales.Get_Debtors_Out(model.frmDate, model.asatdate, customer_Code, company, proCnt_Code, userId);
                            dt1 = CHNLSVC.Sales.Get_IRD_Detail(model.frmDate, model.todate, company, proCnt_Code, model.groupType);
                            rd.Load(Server.MapPath("/Reports/" + "rpt_IRDReport.rpt"));
                            //rd.Database.Tables["DebtorsOutstandingRpt"].SetDataSource(dt1);
                            //rd.SetParameterValue("userID", userId);
                            //rd.SetParameterValue("frmDate", model.frmDate);
                            //rd.SetParameterValue("toDate", model.todate);
                            //rd.SetParameterValue("asatDate", model.asatdate);

                            rd.Database.Tables["IRDDetails"].SetDataSource(dt1);

                            rd.SetParameterValue("userID", userId);
                            rd.SetParameterValue("frmDate", model.frmDate);
                            rd.SetParameterValue("toDate", model.todate);
                            rd.SetParameterValue("comName", _msCom.Mc_desc);
                            rd.SetParameterValue("Add1", _msCom.Mc_add1);
                            rd.SetParameterValue("Add2", _msCom.Mc_add2);
                            rd.SetParameterValue("Tel", _msCom.Mc_tel);

                            Response.Buffer = false;
                            Response.ClearContent();
                            Response.ClearHeaders();
                            try
                            {
                                //this.Response.Clear();
                                //this.Response.ContentType = "application/pdf";
                                //Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");

                                var fName = Server.MapPath("~") + "Export\\IRDReportDetails-" + Session["UserID"].ToString() + ".xls";
                                rd.ExportToDisk(ExportFormatType.Excel, fName);
                                return File(fName, "application/vnd.ms-excel", "IRDReportDetails-" + Session["UserID"].ToString() + ".xls");
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            break;

                            //DIlan 17/01/2019
                            case "InvoiceViseReport":
                            if (model.groupType != "InvoiceDate")
                            {
                                err_option = "RPT-InvoiceViseReport-EXCEL";
                                err_form = "InvoiceViseReport";

                                dt1 = CHNLSVC.Sales.Get_Job_Invoice_Detail(model.frmDate, model.todate, company, proCnt_Code, model.groupType);
                                rd.Load(Server.MapPath("/Reports/" + "rpt_InvoiceViseReport.rpt"));
                                rd.Database.Tables["InvoiceViseReport"].SetDataSource(dt1);

                                rd.SetParameterValue("userID", userId);
                                rd.SetParameterValue("frmDate", model.frmDate);
                                rd.SetParameterValue("toDate", model.todate);
                                rd.SetParameterValue("comName", _msCom.Mc_desc);
                                rd.SetParameterValue("Add1", _msCom.Mc_add1);
                                rd.SetParameterValue("Add2", _msCom.Mc_add2);
                                rd.SetParameterValue("Tel", _msCom.Mc_tel);

                                Response.Buffer = false;
                                Response.ClearContent();
                                Response.ClearHeaders();
                                try
                                {
                                    //this.Response.Clear();
                                    //this.Response.ContentType = "application/pdf";
                                    //Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");

                                    var fName = Server.MapPath("~") + "Export\\InvoiceViseReport-" + Session["UserID"].ToString() + ".xls";
                                    rd.ExportToDisk(ExportFormatType.Excel, fName);
                                    return File(fName, "application/vnd.ms-excel", "InvoiceViseReport-" + Session["UserID"].ToString() + ".xls");
                                }
                                catch (Exception ex)
                                {
                                    throw;
                                }
                            }
                            else
                            {
                                ViewData["Error"] = "Please Select Job Status";
                                return View("Error");
                            }
                            break;

                            //dilshan 24/01/2019
                            case "CostOfSales":

                            err_option = "RPT-CostOfSales-EXCEL";
                            err_form = "CostOfSales";

                            //dt1 = CHNLSVC.Sales.Get_Debtors_Out(model.frmDate, model.todate, customer_Code, company, proCnt_Code, userId);
                            dt1 = CHNLSVC.Sales.Get_Cost_Of_Sales(model.frmDate, model.todate, company, proCnt_Code, userId);
                            dt2 = CHNLSVC.Sales.Get_Cost_Of_Sales_Req(model.frmDate, model.todate, company, proCnt_Code, userId);
                            dt3 = CHNLSVC.Sales.Get_Cost_Of_Sales_Hdr(model.frmDate, model.todate, company, proCnt_Code, userId);
                            
                            rd.Load(Server.MapPath("/Reports/" + "rpt_CostOfSales.rpt"));
                            rd.Database.Tables["CostOfSales"].SetDataSource(dt1);
                            rd.Database.Tables["CostOfSalesReq"].SetDataSource(dt2);
                            rd.Database.Tables["CostOfSalesHdr"].SetDataSource(dt3);
                            rd.SetParameterValue("userID", userId);
                            rd.SetParameterValue("frmDate", model.frmDate);
                            rd.SetParameterValue("toDate", model.todate);
                            rd.SetParameterValue("comName", _msCom.Mc_desc);
                            rd.SetParameterValue("Add1", _msCom.Mc_add1);
                            rd.SetParameterValue("Add2", _msCom.Mc_add2);
                            rd.SetParameterValue("Tel", _msCom.Mc_tel);
                            Response.Buffer = false;
                            Response.ClearContent();
                            Response.ClearHeaders();
                            try
                            {
                                //this.Response.Clear();
                                //this.Response.ContentType = "application/pdf";
                                //Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");

                                var fName = Server.MapPath("~") + "Export\\CostOfSales-" + Session["UserID"].ToString() + ".xls";
                                rd.ExportToDisk(ExportFormatType.Excel, fName);
                                return File(fName, "application/vnd.ms-excel", "CostOfSales-" + Session["UserID"].ToString() + ".xls");
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            break;

                            //dilshan 24/01/2019
                            case "GPReport":

                            err_option = "RPT-GPReport-EXCEL";
                            err_form = "GPReport";

                            //dt1 = CHNLSVC.Sales.Get_Debtors_Out(model.frmDate, model.todate, customer_Code, company, proCnt_Code, userId);
                            dt1 = CHNLSVC.Sales.Get_GP_Closed_Job(model.frmDate, model.todate, company, proCnt_Code, userId);
                            dt2 = CHNLSVC.Sales.Get_GP_Closed_Job_Cost(model.frmDate, model.todate, company, proCnt_Code, userId);
                            dt3 = CHNLSVC.Sales.Get_Cost_Of_Sales_Hdr(model.frmDate, model.todate, company, proCnt_Code, userId);
                            rd.Load(Server.MapPath("/Reports/" + "rpt_GPReport.rpt"));
                            rd.Database.Tables["CostOfSalesHdr"].SetDataSource(dt3);
                            rd.Database.Tables["GPReport"].SetDataSource(dt1);
                            rd.Database.Tables["GPReport_Cost"].SetDataSource(dt2);
                            rd.SetParameterValue("userID", userId);
                            rd.SetParameterValue("frmDate", model.frmDate);
                            rd.SetParameterValue("toDate", model.todate);
                            rd.SetParameterValue("comName", _msCom.Mc_desc);
                            rd.SetParameterValue("Add1", _msCom.Mc_add1);
                            rd.SetParameterValue("Add2", _msCom.Mc_add2);
                            rd.SetParameterValue("Tel", _msCom.Mc_tel);
                            Response.Buffer = false;
                            Response.ClearContent();
                            Response.ClearHeaders();
                            try
                            {
                                //this.Response.Clear();
                                //this.Response.ContentType = "application/pdf";
                                //Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");

                                var fName = Server.MapPath("~") + "Export\\GPReport-" + Session["UserID"].ToString() + ".xls";
                                //var fName = Server.MapPath("~") + "Export\\CostOfSales-" + Session["UserID"].ToString() + ".xls";
                                rd.ExportToDisk(ExportFormatType.Excel, fName);
                                return File(fName, "application/vnd.ms-excel", "GPReport-" + Session["UserID"].ToString() + ".xls");
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            break;

                            //dilshan 24/01/2019
                            case "PendingAdv":

                            err_option = "RPT-PendingAdv-EXCEL";
                            err_form = "PendingAdv";

                            //dt1 = CHNLSVC.Sales.Get_Debtors_Out(model.frmDate, model.todate, customer_Code, company, proCnt_Code, userId);
                            dt1 = CHNLSVC.Sales.Get_Pending_Adv(model.frmDate, model.todate, company, proCnt_Code, userId);

                            rd.Load(Server.MapPath("/Reports/" + "rpt_PendingAdv.rpt"));
                            rd.Database.Tables["PendingAdv"].SetDataSource(dt1);
                            rd.SetParameterValue("userID", userId);
                            rd.SetParameterValue("frmDate", model.frmDate);
                            rd.SetParameterValue("toDate", model.todate);
                            rd.SetParameterValue("comName", _msCom.Mc_desc);
                            rd.SetParameterValue("Add1", _msCom.Mc_add1);
                            rd.SetParameterValue("Add2", _msCom.Mc_add2);
                            rd.SetParameterValue("Tel", _msCom.Mc_tel);
                            Response.Buffer = false;
                            Response.ClearContent();
                            Response.ClearHeaders();
                            try
                            {
                                //this.Response.Clear();
                                //this.Response.ContentType = "application/pdf";
                                //Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");

                                var fName = Server.MapPath("~") + "Export\\PendingAdv-" + Session["UserID"].ToString() + ".xls";
                                rd.ExportToDisk(ExportFormatType.Excel, fName);
                                return File(fName, "application/vnd.ms-excel", "PendingAdv-" + Session["UserID"].ToString() + ".xls");
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            break;
                            //dilshan 
                            case "debtorsOutstandingReportSumm":

                            err_option = "RPT-debtorsOutstandingReport-EXCEL";
                            err_form = "debtorsOutstandingReport";

                            //dt1 = CHNLSVC.Sales.Get_Debtors_Out(model.frmDate, model.todate, customer_Code, company, proCnt_Code, userId);
                            dt1 = CHNLSVC.Sales.Get_Debtors_Out_Summ(model.frmDate, model.asatdate, customer_Code, company, proCnt_Code, userId);
                            rd.Load(Server.MapPath("/Reports/" + "rpt_Debtors_Out_Summ.rpt"));
                            rd.Database.Tables["DebtorsOutstandingSumm"].SetDataSource(dt1);
                            rd.SetParameterValue("userID", userId);
                            //rd.SetParameterValue("frmDate", model.frmDate);
                            //rd.SetParameterValue("toDate", model.todate);
                            rd.SetParameterValue("asatDate", model.asatdate);
                            rd.SetParameterValue("comName", _msCom.Mc_desc);
                            rd.SetParameterValue("Add1", _msCom.Mc_add1);
                            rd.SetParameterValue("Add2", _msCom.Mc_add2);
                            rd.SetParameterValue("Tel", _msCom.Mc_tel);
                            Response.Buffer = false;
                            Response.ClearContent();
                            Response.ClearHeaders();
                            try
                            {
                                //this.Response.Clear();
                                //this.Response.ContentType = "application/pdf";
                                //Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");

                                var fName = Server.MapPath("~") + "Export\\DebtorsOutstandingRptSumm-" + Session["UserID"].ToString() + ".xls";
                                rd.ExportToDisk(ExportFormatType.Excel, fName);
                                return File(fName, "application/vnd.ms-excel", "DebtorsOutstandingRptSumm-" + Session["UserID"].ToString() + ".xls");
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
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    rd.Close();
                    rd.Dispose();
                    return File(stream, "application/pdf"); 

                    //return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                }
                else
                {
                    return Redirect("~/Login");
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.General.SaveReportErrorLog(err_option, err_form, ex.Message, Session["UserID"].ToString());
                ViewData["Error"] = ex.Message.ToString();
                return View("RequestPrint");
            }
        }  
	}
}