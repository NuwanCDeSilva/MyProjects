using FF.BusinessObjects;
using FF.BusinessObjects.ToursNew;
using FF.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FastForward.WebAbansTours.Controllers
{
    public class DriverAllocationController : BaseController
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
                ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1014);
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
        public ActionResult Index(string EnqID="", string Driver="")
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string date = DateTime.Today.Date.ToString();
            string[] tokens = date.Split(' ');
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                ViewBag.date = tokens[0];
                ViewBag.vehicle = EnqID;
                ViewBag.driver = Driver;
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }

        }
        public JsonResult getDriverAllocDetailsByID(string val)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                val = val.Trim();

                List<mst_fleet_driver> vehiAllocations = CHNLSVC.Tours.getAllocateVehiclesByID(val);
                if (vehiAllocations != null)
                {
                    return Json(new { success = true, login = true, data = vehiAllocations }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = true, data = "" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getDriverAllocDetailsByDriver(string val)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                val = val.Trim();

                List<Mst_Fleet_driver_new> vehiAllocations = CHNLSVC.Tours.getAllocateVehiclesnew(val);
                if (vehiAllocations != null)
                {
                    return Json(new { success = true, login = true, data = vehiAllocations }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = true, data = "" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public ActionResult DriverAllocate(mst_fleet_driver f)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            f.MFD_COM = company;
            f.MFD_CRE_BY = userId;
            f.MFD_CRE_DT = DateTime.Today;
            f.MFD_PC = userDefPro;

            if (f != null)
            {
                Int32 effect = CHNLSVC.Tours.sp_tours_update_driver_alloc("", f.MFD_VEH_NO, f.MFD_DRI, f.MFD_ACT, f.MFD_FRM_DT, f.MFD_TO_DT, f.MFD_CRE_BY, f.MFD_CRE_DT, f.MFD_MOD_BY, f.MFD_MOD_DT, f.MFD_COM, f.MFD_PC);
                return RedirectToAction("Index");
            }

            return View();

        }
        public ActionResult Alert()
        {
            return View();
        }

        public ActionResult StatusOperationDriAlloc(int id, string text, string st)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Int32 effect = CHNLSVC.Tours.UPDATE_DRIVER_ALLO_STATUS_TO_INACT(id);

            Session["id"] = text;
            Session["st"] = st;
            return RedirectToAction("Index");
        }
        public ActionResult StatusOperationDriAlloc2(int id, string text, string st)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Int32 effect = CHNLSVC.Tours.UPDATE_DRIVER_ALLO_STATUS_TO_ACT(id);
            Session["id"] = text;
            Session["st"] = st;
            return RedirectToAction("Index");
        }
        public JsonResult DriverAllocateNew(mst_fleet_driver f)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                f.MFD_COM = company;
                f.MFD_CRE_BY = userId;
                f.MFD_CRE_DT = DateTime.Today;
                f.MFD_PC = userDefPro;
                if (f.MFD_VEH_NO == null | f.MFD_VEH_NO == "")
                {
                    return Json(new { success = false, login = true, msg = "Vehicle Number Required", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (f.MFD_DRI == null | f.MFD_DRI == "")
                {
                    return Json(new { success = false, login = true, msg = "Driver Required", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (f.MFD_TO_DT == null)
                {
                    return Json(new { success = false, login = true, msg = "To Date Required", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                List<mst_fleet_driver> driver =CHNLSVC.Tours.getAlowcatedFleetAndDriverDetails(f.MFD_DRI, f.MFD_VEH_NO, f.MFD_FRM_DT, f.MFD_TO_DT, "DRIVER");
                if (driver.Count > 0) {
                    return Json(new { success = false, login = true, msg = "Driver already assigned for vehicle :" + driver[0].MFD_VEH_NO+" for this date range.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                List<mst_fleet_driver> fleet = CHNLSVC.Tours.getAlowcatedFleetAndDriverDetails(f.MFD_DRI, f.MFD_VEH_NO, f.MFD_FRM_DT, f.MFD_TO_DT, "FLEET");
                if (fleet.Count > 0)
                {
                    return Json(new { success = false, login = true, msg = "Fleet already assigned for driver:" + fleet[0].MFD_DRI + " for this date range.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }

                //List<GEN_CUST_ENQ> enqDrivaerAlow = CHNLSVC.Tours.getAlowcatedFleetAndDriverDetailsInEnquiry(f.MFD_DRI, f.MFD_VEH_NO, f.MFD_FRM_DT, f.MFD_TO_DT, "DRIVER");
                //if (enqDrivaerAlow.Count > 0)
                //{
                //    string ids="";
                //    int i = 1;
                //    foreach(GEN_CUST_ENQ enq in enqDrivaerAlow){
                //        ids += enq.GCE_ENQ_ID;
                //        ids= (ids != "") ?(i==enqDrivaerAlow.Count) ?"," :"": "";
                //    }
                //    return Json(new { success = false, login = true, msg = "Driver already assigned for enquies :" + ids + " for this date range.", type = "Info" }, JsonRequestBehavior.AllowGet);
                //}
                //List<GEN_CUST_ENQ> fleetEnqAlow = CHNLSVC.Tours.getAlowcatedFleetAndDriverDetailsInEnquiry(f.MFD_DRI, f.MFD_VEH_NO, f.MFD_FRM_DT, f.MFD_TO_DT, "FLEET");
                //if (fleetEnqAlow.Count > 0)
                //{
                //    string ids = "";
                //    int i = 1;
                //    foreach (GEN_CUST_ENQ enq in fleetEnqAlow)
                //    {
                //        ids += enq.GCE_ENQ_ID;
                //        ids = (ids != "") ? (i == enqDrivaerAlow.Count) ? "," : "" : "";
                //    }
                //    return Json(new { success = false, login = true, msg = "Fleet already assigned for enquies :" + ids + " for this date range.", type = "Info" }, JsonRequestBehavior.AllowGet);
                //} 

                    Int32 effect = CHNLSVC.Tours.sp_tours_update_driver_alloc("", f.MFD_VEH_NO, f.MFD_DRI, f.MFD_ACT, f.MFD_FRM_DT, f.MFD_TO_DT, f.MFD_CRE_BY, f.MFD_CRE_DT, f.MFD_MOD_BY, f.MFD_MOD_DT, f.MFD_COM, f.MFD_PC);
                    if (effect == 1)
                    {
                        return Json(new { success = true, login = true, msg = "Driver Allocation Completed", type = "Success" }, JsonRequestBehavior.AllowGet);
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
    }
}