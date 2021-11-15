using FF.BusinessObjects.Commission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FF.SCMWebMVC.Controllers
{
    public class ProcessArreasController : BaseController
    {
        //
        // GET: /ProcessArreas/
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
                return Redirect("~/Login/index");
            }
        }
        public JsonResult ArrearsProcess(string Month)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            Month = Month.Trim();
            string err = "";
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                int effect = CHNLSVC.Finance.AccountsArrearsProcess(Convert.ToDateTime(Month).AddMonths(1).AddDays(-1), userId,company , out  err);
                if (effect > 0)
                {
                    return Json(new { success = true, login = true, msg = "Successfull Saved " + err }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (err.Contains("Please Refer File"))
                    {
                        return Json(new { success = false, login = true, msg = err ,type="Info"}, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = err, type = "error"}, JsonRequestBehavior.AllowGet);
                    }
                   
                }
               
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult LoadArrdata(string pc, string Month)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            Month = Month.Trim();
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<hpt_arr_acc> _arrpc = CHNLSVC.Finance.GetArrBalAccHdr(company, pc, Convert.ToDateTime(Month).AddMonths(1).AddDays(-1));
                if (_arrpc==null)
                {
                    _arrpc = new List<hpt_arr_acc>();
                }
                return Json(new { success = true, login = true, list = _arrpc }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
	}
}