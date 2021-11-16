using FF.BusinessObjects.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastForward.Logistic.Controllers
{
    public class PaymentVoucherEnquiryController : BaseController
    {
        //
        // GET: /PaymentVoucherEnquiry/
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadVoucherDetails(string frmDate, string toDate, string reqNo, string jobNo, string manRefNo, string proCnt)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<TRN_PETTYCASH_REQ_HDR> P_list = CHNLSVC.Sales.GetVou_Hdr(Convert.ToDateTime(string.IsNullOrEmpty(frmDate) ? DateTime.MinValue.ToShortDateString() : frmDate), Convert.ToDateTime(string.IsNullOrEmpty(toDate) ? DateTime.MaxValue.ToShortDateString() : toDate), reqNo, jobNo, manRefNo, proCnt);
                    if (P_list == null)
                    {
                        P_list = new List<TRN_PETTYCASH_REQ_HDR>();
                    }
                    return Json(new { success = true, login = true, data = P_list }, JsonRequestBehavior.AllowGet);
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
        public JsonResult VouDetails(string ReqNo, string SeqNo)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<TRN_PETTYCASH_REQ_DTL> Vou_det = CHNLSVC.Sales.GetVou_Dtls(ReqNo, SeqNo);
                    return Json(new { success = true, login = true, data = Vou_det }, JsonRequestBehavior.AllowGet);
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
	}
}