using FF.BusinessObjects;
using FF.BusinessObjects.ToursNew;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FastForward.WebAbansTours.Controllers
{
    public class ExchangeRateController : BaseController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1011);
                if (per.MNU_ID == 0)
                {
                    throw new AuthenticationException("You do not have the necessary permission to perform this action");
                }
            }
            else
            {
                Redirect("~/Login/index");
            }

        }
        // GET: ExchangeRate
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SaveExchangeRate(MasterExchangeRate _exg)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                _exg.Mer_act = true;
                _exg.Mer_com = company;
                _exg.Mer_cre_by = userId;
                _exg.Mer_cre_dt = DateTime.Now;
                _exg.Mer_pc = userDefPro;
                _exg.Mer_buyvad_from = _exg.Mer_vad_from;
                _exg.Mer_buyvad_to = _exg.Mer_vad_to;
                _exg.Mer_session_id = Session["SessionID"].ToString();

                if (_exg.Mer_cur == "" | _exg.Mer_cur == null)
                {
                    return Json(new { success = false, login = true, msg = "From Currency Required", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_exg.Mer_to_cur == null | _exg.Mer_to_cur == "")
                {
                    return Json(new { success = false, login = true, msg = "To Currency Required", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_exg.Mer_vad_from == null)
                {
                    return Json(new { success = false, login = true, msg = "From Validate Date Required", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_exg.Mer_vad_to == null)
                {
                    return Json(new { success = false, login = true, msg = "To Validate Date Required", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_exg.Mer_bnkbuy_rt == null | _exg.Mer_bnkbuy_rt == 0)
                {
                    return Json(new { success = false, login = true, msg = "Buy Rate Required", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_exg.Mer_bnksel_rt == null | _exg.Mer_bnksel_rt == 0)
                {
                    return Json(new { success = false, login = true, msg = "Selling Rate Required", type = "Info" }, JsonRequestBehavior.AllowGet);
                }

                Int32 effect = CHNLSVC.Sales.Save_ExchangeRate(_exg);
                if (effect == 1)
                {
                    return Json(new { success = true, login = true, msg = "Exchange Rate Added", type = "Success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "Get Error", type = "Error" }, JsonRequestBehavior.AllowGet);
                }


            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult getExchangeData(string frmcurr, DateTime date, string tocurr)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                // int asd = CHNLSVC.Tours.GetInvoiceDetails(userId, userDefPro, invNo, out oHeader, out oMainItemsList, out oRecieptHeader, out _recieptItem, out   err);

                List<MasterExchangeRate> data = CHNLSVC.Sales.GetValid_ExchangeRates(company, frmcurr, tocurr, date);
                return Json(new { success = true, login = true, data = data }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}