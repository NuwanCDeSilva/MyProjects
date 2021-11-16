using CrystalDecisions.CrystalReports.Engine;
using FF.BusinessObjects;
using FF.BusinessObjects.Genaral;
using FF.BusinessObjects.Sales;
using FF.BusinessObjects.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace FastForward.Logistic.Controllers
{
    public class DocumentReprintController : BaseController
    {
        // GET: DocumentReprint
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                SEC_SYSTEM_MENU per = CHNLSVC.Security.getUserPermission(userId, company, 39);
                if (per.SSM_ID != 0)
                {
                    TRN_MOD_MAX_APPLVL data = CHNLSVC.General.getMaxAppLvlPermission("PTYCSHREQ", company);
                    if (data.TMAL_MODULE != null)
                    {
                        Session["MAXAPPLVL"] = data;
                    }
                    else
                    {
                        data.TMAL_COM = company;
                        data.TMAL_MAX_APPLVL = 3;
                        data.TMAL_MODULE = "PTYCSHREQ";
                        Session["MAXAPPLVL"] = data;
                    }
                    return View();
                }
                else
                {
                    return Redirect("/Home/Error");
                }
            }
            else
            {
                return Redirect("~/Login");
            }
        }
        public JsonResult loadPendingRequests(string fromdt, string todt)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    DateTime fmdt;
                    DateTime tdt;
                    try
                    {
                        fmdt = Convert.ToDateTime(fromdt);
                        tdt = Convert.ToDateTime(todt);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter date range", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    if (fmdt > tdt)
                    {
                        return Json(new { success = false, login = true, msg = "Invalid date range", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (Session["Log_Autho"] == null || Session["Log_Autho"].ToString() == "")
                    {
                        return Json(new { success = false, login = true, msg = "Please set up user approve permission.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    Int32 applvl = Convert.ToInt32(Session["Log_Autho"].ToString());
                    string error = "";
                    List<TRN_PETTYCASH_REQ_HDR> data = CHNLSVC.Sales.loadPendingptyReq(company, userDefPro, fmdt, tdt, applvl, out error);
                    if (error == "")
                    {
                        foreach (TRN_PETTYCASH_REQ_HDR dt in data)
                        {
                            dt.TPRH_TYPE_DESC = CHNLSVC.Sales.requestTypeDesc(dt.TPRH_SEQ);
                        }
                        return Json(new { success = true, login = true, data = data }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult approveRequest(string reqno )
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string sessionid = Session["SessionID"].ToString();
                    string error = "";
                    bool perm = CHNLSVC.Security.Is_OptionPerimitted(company, userId, 1014);
                    //TRN_PETTYCASH_REQ_HDR request = CHNLSVC.Sales.getReqyestDetials(type, reqno, company, userDefPro, out error);
                    if (perm != true)
                    {
                        return Json(new { success = false, login = true, msg = "You don't have permission to approve request.(Requsted permission code 1014)", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (reqno != "")
                        {
                            Int32 eff = CHNLSVC.Sales.updateReprintDocStus(reqno, userId, out error);
                            if (eff == 1)
                            {
                                return Json(new { success = true, login = true, type = "Success", msg = "Successfully approved request " + reqno }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (error != "")
                                {
                                    return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Unable to approve request.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Please enter House BL number", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}