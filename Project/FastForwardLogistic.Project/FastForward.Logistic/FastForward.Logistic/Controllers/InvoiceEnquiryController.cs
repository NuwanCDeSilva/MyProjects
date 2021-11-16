using FF.BusinessObjects;
using FF.BusinessObjects.Genaral;
using FF.BusinessObjects.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastForward.Logistic.Controllers
{
    public class InvoiceEnquiryController : BaseController
    {
        //
        // GET: /InvoiceEnquiry/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadInvoiceEnquiry(string JobNo, string modOfShpmnt, string typOfShpmnt, string cusCode, string hbl)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<trn_inv_hdr> P_list = CHNLSVC.Sales.GetInvHdr_Dtls(JobNo, modOfShpmnt, typOfShpmnt, cusCode, hbl, company);
                    if (P_list == null)
                    {
                        P_list = new List<trn_inv_hdr>();
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
        public JsonResult LoadModeOfShipment()
        {
            int iCount = 0;
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MainServices> P_list = CHNLSVC.General.GetMainServicesCodes();
                    if (P_list == null)
                    {
                        P_list = new List<MainServices>();
                    }
                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    foreach (var items in P_list)
                    {
                        ComboBoxObject o1 = new ComboBoxObject();
                        if (iCount != 0)
                        {
                            o1.Text = items.fms_ser_desc;
                            o1.Value = items.fms_ser_cd;
                        }
                        else
                        {
                            o1.Text = string.Empty;
                            o1.Value = string.Empty;
                        }
                        oList.Add(o1);
                        iCount++;
                    }
                    return Json(new { success = true, login = true, data = oList }, JsonRequestBehavior.AllowGet);
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
        public JsonResult InvoiceDetails(string SeqNo)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<trn_inv_det> Inv_det = CHNLSVC.Sales.Get_Inv_det(SeqNo);
                    return Json(new { success = true, login = true, data = Inv_det }, JsonRequestBehavior.AllowGet);
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