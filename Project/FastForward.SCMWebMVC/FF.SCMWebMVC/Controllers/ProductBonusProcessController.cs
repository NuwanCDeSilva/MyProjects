using FF.BusinessObjects.Commission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FF.SCMWebMVC.Controllers
{
    public class ProductBonusProcessController : BaseController
    {
        //
        // GET: /ProductBonusProcess/
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["Allinvdat"] = null;
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }
        public JsonResult ViewProductBonus(string Code, string FromDate, string ToDate, string salesfdate, string salestdate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<PoductBonusData> bonusdata = new List<PoductBonusData>();
                bonusdata = CHNLSVC.Finance.BonusProcess(Code, Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), company, Convert.ToDateTime(salesfdate), Convert.ToDateTime(salestdate));
                Session["Allinvdat"] = bonusdata;
                //group by emp code

                bonusdata = bonusdata.GroupBy(l => new { l.ExecCode })
.Select(cl => new PoductBonusData
{
    ExecCode = cl.First().ExecCode,
    InvoiceNo = cl.First().InvoiceNo,
    ExecName = cl.First().ExecName,
    TotAmmount = cl.Sum(a=>a.TotAmmount),
    Qty = cl.Sum(a=>a.Qty),
    pc=cl.First().pc,
    TotMarks=cl.Sum(a=>a.TotMarks)
}).OrderByDescending(a=>a.TotMarks).ToList();
                return Json(new { success = true, login = true, summery = bonusdata }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetInvoiceNumbers(string ExecCode)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<PoductBonusData> list = Session["Allinvdat"] as List<PoductBonusData>;
                list = list.Where(a => a.ExecCode == ExecCode).ToList();
                return Json(new { success = true, login = true, allinvoice = list }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult SaveProductBonus()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string err = "";
                List<PoductBonusData> bonus = Session["Allinvdat"] as List<PoductBonusData>;
                int effect = CHNLSVC.Finance.SaveProductBonusdata(bonus,company, userId, out err);
                if (effect == 1)
                {
                    return Json(new { success = true, login = true, msg = "Successfully Finalized" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = true, type = "err", msg = err }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
	}
}