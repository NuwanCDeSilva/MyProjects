using FF.BusinessObjects.Sales;
using FF.BusinessObjects.Search;
using FF.BusinessObjects;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using FastForward.Logistic.Models;
using CrystalDecisions.Shared;
using System.Security.Authentication;
using FF.BusinessObjects.Security;
using System.IO;

namespace FastForward.Logistic.Controllers
{
    public class JobClosureController : BaseController
    {     
        //
        // GET: /JobClosure/
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                ViewBag.Logincompany = company;
                Session["old_job_cost"] = null;
                Session["new_job_cost"] = null;
                Session["old_job_actual_cost"] = null;
                return View();
            }
            else
            {
                return Redirect("~/Home/index");
            }
        }
        public JsonResult LoadJobCostinData(string JobNo)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<TRN_JOB_COST> cost_list = CHNLSVC.Sales.GetJobCostData(JobNo);
                    List<TRN_JOB_COST> actual_list = CHNLSVC.Sales.GetJobActualCostData(JobNo);
                    if (cost_list == null)
                    {
                        cost_list = new List<TRN_JOB_COST>();
                    }
                    if (actual_list == null)
                    {
                        actual_list = new List<TRN_JOB_COST>();
                    }
                    Session["old_job_cost"] = cost_list;
                    Session["old_job_actual_cost"] = actual_list;
                    Session["new_job_cost"] = null;

                    decimal tot_cost = cost_list.AsEnumerable().Sum(o => o.TJC_COST_AMT);
                    decimal tot_Act_cost = actual_list.AsEnumerable().Sum(o => o.TJC_COST_AMT);

                    decimal costdiff = tot_Act_cost - tot_cost;

                    return Json(new { success = true, login = true, costData = cost_list, actualCostData = actual_list, costdiff = costdiff }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddJobCostinData(string JobNo, string Desc, string Cost)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    int lineno = 0;
                    List<TRN_JOB_COST> old_job_cost = Session["old_job_cost"] as List<TRN_JOB_COST>;
                    List<TRN_JOB_COST> new_job_cost = Session["new_job_cost"] as List<TRN_JOB_COST>;

                    if (old_job_cost != null)
                    {
                        if (old_job_cost.Count > 0)
                        {
                            lineno = old_job_cost.Max(a => a.TJC_LINE_NO);
                        }

                    }


                    if (new_job_cost == null)
                    {
                        new_job_cost = new List<TRN_JOB_COST>();
                    }
                    else
                    {
                        if (new_job_cost.Count > 0)
                        {
                            lineno = new_job_cost.Max(a => a.TJC_LINE_NO);
                        }
                    }
                    TRN_JOB_COST _ob = new TRN_JOB_COST();
                    _ob.TJC_ACT = 1;
                    _ob.TJC_COM = company;
                    _ob.TJC_COST_AMT = Convert.ToDecimal(Cost);
                    _ob.TJC_CRE_BY = userId;
                    _ob.TJC_CRE_DT = DateTime.Now;
                    _ob.TJC_DESC = Desc;
                    _ob.TJC_DT = DateTime.Now;
                    _ob.TJC_ELEMENT_CODE = "";
                    _ob.TJC_ELEMENT_TYPE = "";
                    _ob.TJC_JOB_NO = JobNo;
                    _ob.TJC_LINE_NO = lineno + 1;
                    _ob.TJC_MOD_BY = userId;
                    _ob.TJC_MOD_DT = DateTime.Now;
                    new_job_cost.Add(_ob);
                    old_job_cost.Add(_ob);
                    Session["old_job_cost"] = old_job_cost;
                    Session["new_job_cost"] = new_job_cost;

                    return Json(new { success = true, login = true, data = old_job_cost }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult RemoveCostDetails(string Desc, string Cost)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<TRN_JOB_COST> old_job_cost = Session["old_job_cost"] as List<TRN_JOB_COST>;
                List<TRN_JOB_COST> new_job_cost = Session["new_job_cost"] as List<TRN_JOB_COST>;

                if (new_job_cost != null)
                {
                    if (new_job_cost.Count > 0)
                    {
                        int count = new_job_cost.Where(a => a.TJC_DESC == Desc && a.TJC_COST_AMT == Convert.ToDecimal(Cost)).Count();
                        if (count == 0)
                        {
                            return Json(new { success = false, login = true, msg = "Cant Remove This Cost(Already added Cost)", Type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            var removeitem = new_job_cost.First(a => a.TJC_DESC == Desc && a.TJC_COST_AMT == Convert.ToDecimal(Cost));
                            new_job_cost.Remove(removeitem);
                            old_job_cost.Remove(removeitem);
                            Session["old_job_cost"] = old_job_cost;
                            Session["new_job_cost"] = new_job_cost;
                        }

                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Cant Remove This Cost(Already added Cost)", Type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "Cant Remove This Cost(Already added Cost)", Type = "Info" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, login = true, data = old_job_cost }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveJobClose(string JobNo, string Date, string Remark)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<TRN_JOB_COST> old_job_cost = Session["old_job_cost"] as List<TRN_JOB_COST>;
                List<TRN_JOB_COST> new_job_cost = Session["new_job_cost"] as List<TRN_JOB_COST>;
                string err = "";
                int effect = CHNLSVC.Sales.SaveJobClose(JobNo, Remark, userId, Convert.ToDateTime(Date), new_job_cost, out err);
                if (effect > 0)
                {
                    return Json(new { success = true, login = true, msg = "Successfully Closed!!!", Type = "succ" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = err, Type = "Error" }, JsonRequestBehavior.AllowGet);
                }


            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ReopenJobClose(string JobNo, string Date, string Remark)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<TRN_JOB_COST> old_job_cost = Session["old_job_cost"] as List<TRN_JOB_COST>;
                List<TRN_JOB_COST> new_job_cost = Session["new_job_cost"] as List<TRN_JOB_COST>;
                string err = "";
                Int32 appPer = Convert.ToInt32(Session["Log_Autho"].ToString());
                if (appPer != 3)
                {
                    return Json(new { success = false, login = true, msg = "Yoe don't have permission for edit job status.(Requsted permission code 1015)", Type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int effect = CHNLSVC.Sales.ReopenJobClose(JobNo, Remark, userId, Convert.ToDateTime(Date), out err);
                    if (effect > 0)
                    {
                        return Json(new { success = true, login = true, msg = "Successfully Re-Opened!!!", Type = "succ" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = err, Type = "Error" }, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public void ClearSession()
        {
            Session["old_job_cost"] = null;
            Session["new_job_cost"] = null;
            Session["old_job_actual_cost"] = null;
        }

        public JsonResult JobOrPouchDetails(string code, string type)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<JOB_NUM_SEARCH> details = CHNLSVC.Sales.JobOrPouchDetails(code, type, company, userId);
                    int count = details.Count;

                    return Json(new { success = true, login = true, data = details, count = count }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RequestPrint(List<string> selectedcompany, List<string> selectedprofcen, FormCollection collection, string JobNo)
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
                    MasterCompany _msCom = CHNLSVC.Security.GetCompByCode(Session["UserCompanyCode"].ToString());
                    ReportViewerViewModel model = new ReportViewerViewModel();
                    ReportDocument rd = new ReportDocument();
                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();

                    //
                    string reportName = "";
                    string fileName = "";
                    string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"].ToString();
                    string report = "";

                    DataTable comData = new DataTable("comdata");
                    comData = CHNLSVC.Sales.getCompanyDetailsBycd(company);

                    //THarindu 2018-01-04

                    dt1 = CHNLSVC.Sales.Get_jb_header(company, JobNo);
                    dt2 = CHNLSVC.Sales.GetJobCostingData(JobNo);
                    dt3 = CHNLSVC.Sales.GetJobActualCostingData(JobNo);

                    reportName = "rpt_JobCostingSheet.rpt";
                    fileName = "JobCostingSheet.pdf";
                  //  reportName = "test.rpt";
                  //  fileName = "test.pdf";
                    //report = ReportPath + "\\" + reportName;
                    //rd.Load(report);
                    rd.Load(Path.Combine(Server.MapPath("~/Reports"), "rpt_JobCostingSheet.rpt"));
                                          
              //     rd.Load(Server.MapPath("/Reports/" + "rpt_JobCostingSheet.rpt"));
                    rd.Database.Tables["JobHeader"].SetDataSource(dt1);
                    rd.Database.Tables["JobCost"].SetDataSource(dt2);
                    rd.Database.Tables["ActuvalJobCost"].SetDataSource(dt3);

                    rd.SetParameterValue("UserID", userId);
                    rd.SetParameterValue("frmDate", model.frmDate);
                    rd.SetParameterValue("toDate", model.todate);
                    rd.SetParameterValue("ComName", _msCom.Mc_desc);
                    rd.SetParameterValue("Add1", _msCom.Mc_add1);
                    rd.SetParameterValue("Add2", _msCom.Mc_add2);
                    rd.SetParameterValue("Tel", _msCom.Mc_tel);

                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();

                    try
                    {
                        this.Response.Clear();
                        this.Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);

                        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        stream.Seek(0, SeekOrigin.Begin);
                        rd.Close();
                        rd.Dispose();
                        return File(stream, "application/pdf"); 
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    //return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                }
                else
                {
                     return Redirect("~/Home/index");
                }

                
            }

            catch (Exception ex)
            {
                CHNLSVC.General.SaveReportErrorLog("rpt_JobCostingSheet-PDF", "rpt_JobCostingSheet", ex.Message, Session["UserID"].ToString());
                ViewData["Error"] = ex.Message.ToString();
                return View("RequestPrint");
            }

           // return Redirect("~/Home/index");
        }

    }
}