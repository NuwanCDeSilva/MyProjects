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
    public class ManualJobClosureController : BaseController
    {
        // GET: ManualJobClosure
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
                int effect = CHNLSVC.Sales.SaveAutoJobClose(JobNo, Remark, userId, Convert.ToDateTime(Date), new_job_cost, out err);
                if (effect > 0)
                {
                    return Json(new { success = true, login = true, msg = "Successfully Closed!!!", Type = "succ" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "No Data Found", Type = "Info" }, JsonRequestBehavior.AllowGet);
                }


            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}