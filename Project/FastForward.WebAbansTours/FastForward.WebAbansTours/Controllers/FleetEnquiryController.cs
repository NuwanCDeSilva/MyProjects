using FF.BusinessObjects.Tours;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastForward.WebAbansTours.Controllers
{
    public class FleetEnquiryController : BaseController
    {
        // GET: FleetEnquiry
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


        //ISURU  2017/02/28
        public JsonResult FleetAllocationDaily(string fdate , string todate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;


     

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<FleetAllocDaily> list = CHNLSVC.Tours.FleetAllocDailydata(company, Convert.ToDateTime(fdate), Convert.ToDateTime(todate), userDefPro);
                //List<test> ss = new List<test>();
                //foreach (FleetAllocDaily ls in list)
                //{
                //    test a = new test();
                //    a.label = ls.mfd_veh_no;
                //    a.y = new string[2] { ls.mfd_frm_dt.ToString(), ls.mfd_to_dt.ToString() };
                //    ss.Add(a);
                //}
                return Json(new { success = true, login = true, data = list }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, login = false, profCen = "" }, JsonRequestBehavior.AllowGet);
            }

        }
    
    }
}
