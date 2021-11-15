using FF.BusinessObjects.Asycuda;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastForward.WebASYCUDA.Controllers
{
    public class SearchController : BaseController
    {
        // GET: Search
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            if (!string.IsNullOrEmpty(userId))
            {
                return View();
            }
            else
            {
                return Redirect("~/Login/Login");
            }
        }
        //public JsonResult getDocumentCount(string dbsrcid)
        //{
        //    string userId = HttpContext.Session["UserID"] as string;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(userId))
        //        {
        //            int dbsrc = Convert.ToInt32(dbsrcid);
        //            int getTotalDocuments = CHNLSVC.Asycuda.getDocumentDetailscCount(dbsrc);
        //            return Json(new { success = true, login = true, totalDoc = getTotalDocuments }, JsonRequestBehavior.AllowGet);
        //        }
        //        else {
        //            return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex) {
        //        if (!string.IsNullOrEmpty(userId))
        //        {
        //            return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //}
        public JsonResult getDocumentDetails(string dbsrcid, string pgeNum,string pgeSize,string searchFld,string searchVal) {
            string userId = HttpContext.Session["UserID"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId))
                 {
                     searchVal = searchVal.Trim();                    
                    int dbsrc = Convert.ToInt32(dbsrcid);
                    List<ASY_DOC_SEARCH_HEAD> documents = CHNLSVC.Security.getDocumentDetails(dbsrc, pgeNum, pgeSize, searchFld, searchVal);
                    //int getTotalDocuments = CHNLSVC.Asycuda.getDocumentDetailscCount(dbsrc, searchFld, searchVal);

                    if (documents != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(documents[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = documents, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else {
                        return Json(new { success = false, login = true ,msg="No document found.",data = ""}, JsonRequestBehavior.AllowGet);
                    }
                    
                }
                else {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            
               
            }
            catch (Exception ex) {
                if (!string.IsNullOrEmpty(userId))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
            
        }
    }
}