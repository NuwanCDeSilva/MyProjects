using FF.BusinessObjects;
using FF.BusinessObjects.ToursNew;
using FF.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FastForward.WebAbansTours.Controllers
{
    public class CheckInOutController : BaseController
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
                ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1012);
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
        // GET: CHeckInOut
        public ActionResult Index(string enqId=null)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                MST_CHKINOUT check = new MST_CHKINOUT();
                check.CHK_ENQ_ID = enqId;
                return View(check);
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }
        public JsonResult enquiryDataLoad(string enqId) { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    enqId = enqId.Trim();
                    if (enqId != "")
                    {
                        MST_CHKINOUT chkOut = CHNLSVC.Tours.getEnqChkData(enqId);
                        if (chkOut.CHK_ENQ_ID != null)
                        {
                            return Json(new { success = true, login = true, chkOut = chkOut }, JsonRequestBehavior.AllowGet);
                        }
                        else {
                            return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
                        return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult saveCheckData(MST_CHKINOUT frm) { 
         string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string _error = string.Empty;
                    if (frm.CHK_ENQ_ID == "") {
                        return Json(new { success = false, login = true,msg="Please select enquiry id." ,type="Info"}, JsonRequestBehavior.AllowGet);
                    }
                    GEN_CUST_ENQ oItem = CHNLSVC.Tours.getEnquiryDetails(company, userDefPro, frm.CHK_ENQ_ID);
                    if (oItem.GCE_ENQ_ID == null) {
                        return Json(new { success = false, login = true, msg = "Please select valid enquiry id.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (frm.CHK_OUT_KM.ToString() == "") {
                        return Json(new { success = false, login = true, msg = "Please enter checkout KM.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    frm.CHK_MOD_BY = userId;
                    frm.CHK_CRE_BY = userId;
                    frm.CHK_RMKS = (frm.CHK_RMKS != null) ? frm.CHK_RMKS : "";
                    Int32 result = CHNLSVC.Tours.saveCheckoutDetails(frm,out _error);
                    if (result > 0)
                    {
                        return Json(new { success = true, login = true,msg="Enquiry check in/out successfully updated." }, JsonRequestBehavior.AllowGet);
                    }
                    else {
                        return Json(new { success = false, login = true,msg=_error,type="Info" }, JsonRequestBehavior.AllowGet);
                    }
                   
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        
        }
        //public JsonResult GetImageDetails(string enqid)
        //{
        //    string userId = HttpContext.Session["UserID"] as string;
        //    string company = HttpContext.Session["UserCompanyCode"] as string;
        //    string userDefPro = HttpContext.Session["UserDefProf"] as string;
        //    string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

        //    if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //    {

        //        List<TBS_IMG_UPLOAD> oCostMainItems = CHNLSVC.CustService.GetImageDetails(enqid);
        //        List<TBS_IMG_UPLOAD> oCostMainItemsnew = new List<TBS_IMG_UPLOAD>();
        //        foreach (var oCostMainItems1 in oCostMainItems)
        //        {

        //            string url = oCostMainItems1.Jbimg_img_path.ToString() + oCostMainItems1.Jbimg_img.ToString();
        //            if (checkImage(url) == true)
        //            {
        //                oCostMainItemsnew.Add(oCostMainItems1);
        //            }



        //        }
        //        return Json(new { success = true, login = true, data = oCostMainItemsnew }, JsonRequestBehavior.AllowGet);

        //    }
        //    else
        //    {
        //        return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
        //    }

        //}
        //private bool checkImage(string url)
        //{
        //    WebRequest request = WebRequest.Create(Server.MapPath("~/") + url);

        //    try
        //    {
        //        WebResponse response = request.GetResponse();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
    }
}