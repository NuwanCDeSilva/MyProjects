using CrystalDecisions.CrystalReports.Engine;
using FF.BusinessObjects;
using FF.BusinessObjects.ToursNew;
using FF.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FastForward.WebAbansTours.Controllers
{
    public class FleetDefinitionController : BaseController
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
                ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1009);
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
        // GET: FleetDefinition
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

        public JsonResult addProfitCenter(string val, string reg)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                List<mst_fleet_alloc> list = Session["profitCenters"] as List<mst_fleet_alloc>;
                if (list == null)
                {
                    list = new List<mst_fleet_alloc>();
                }
                mst_fleet_alloc profit = new mst_fleet_alloc();
                profit.MFA_REGNO = reg;
                profit.MFA_ACT = 1;
                profit.MFA_PC = val;
                list.Add(profit);
                Session["profitCenters"] = list;

                return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
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

        public JsonResult removeProfitCenter(string val)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                List<mst_fleet_alloc> list = Session["profitCenters"] as List<mst_fleet_alloc>;
                //list.RemoveAll(a => a.MPE_PC == val);
                list.First(a => a.MFA_PC == val).MFA_ACT = 0;
                Session["profitCenters"] = list;
                return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
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

        //[HttpPost]
        //public ActionResult FleetCreation(mst_fleet f)
        //{

        //    string userId = HttpContext.Session["UserID"] as string;
        //    string company = HttpContext.Session["UserCompanyCode"] as string;
        //    string userDefPro = HttpContext.Session["UserDefProf"] as string;
        //    string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
        //    string MFD_DRI = Request["MFD_DRI"];
        //    string MFD_FRM_DT1 = Request["MFD_FRM_DT"];
        //    string MFD_TO_DT1 = Request["MFD_TO_DT"];
        //    string profitcenter = Request["MFA_PC"];
        //    string MSTF_IS_LEASE1 = Request["MSTF_IS_LEASE"];
        //    int MSTF_IS_LEASE = Convert.ToInt32(MSTF_IS_LEASE1);
        //    f.MSTF_CRE_BY = userId;
        //    f.MSTF_MOD_BY = userId;
        //    DateTime today = DateTime.Today;
        //    f.MSTF_MOD_DT = today;
        //    f.MSTF_CRE_DT = today;
        //    String SeqNo = "";

        //    List<mst_fleet_alloc> list = new List<mst_fleet_alloc>();
        //    if (Session["profitCenters"] != null)
        //    {
        //        List<mst_fleet_alloc> profCen = Session["profitCenters"] as List<mst_fleet_alloc>;
        //        foreach (mst_fleet_alloc prof in profCen)
        //        {
        //            mst_fleet_alloc mst_pcemp = new mst_fleet_alloc();
        //            mst_pcemp.MFA_REGNO = f.MSTF_REGNO;
        //            // mst_pcemp.MPE_COM = company;
        //            mst_pcemp.MFA_PC = prof.MFA_PC;
        //            //  mst_pcemp.MPE_ASSN_DT = DateTime.Now;
        //            mst_pcemp.MFA_ACT = Convert.ToInt32(prof.MFA_ACT);
        //            list.Add(mst_pcemp);
        //        }
        //    }

        //    if (MFD_FRM_DT1 == "" | MFD_TO_DT1 == "")
        //    {
        //        DateTime MFD_FRM_DT = new DateTime(1974, 7, 10, 7, 10, 24);
        //        DateTime MFD_TO_DT = new DateTime(1974, 7, 10, 7, 10, 24);

        //        MST_FLEET_NEW fleets = CHNLSVC.Tours.getMstFleetDetails(f.MSTF_REGNO);

        //        List<mst_fleet_driver> vehicleList = new List<mst_fleet_driver>();
        //        vehicleList = Session["vahicleAllowcation"] as List<mst_fleet_driver>;
        //        if (vehicleList != null)
        //        {
        //            foreach (mst_fleet_driver vehicle in vehicleList)
        //            {

        //                Int32 _effectOR = CHNLSVC.Tours.SaveFleetData(vehicle.MFD_SEQ.ToString(), f.MSTF_REGNO, f.MSTF_DT, f.MSTF_BRD, f.MSTF_MODEL, f.MSTF_VEH_TP, f.MSTF_SIPP_CD, f.MSTF_ST_METER, f.MSTF_OWN, f.MSTF_OWN_NM, f.MSTF_OWN_CONT, f.MSTF_LST_SERMET, f.MSTF_TOU_REGNO, f.MSTF_IS_LEASE, f.MSTF_INSU_EXP, f.MSTF_REG_EXP, f.MSTF_FUAL_TP, f.MSTF_ACT, f.MSTF_CRE_BY, f.MSTF_CRE_DT, f.MSTF_MOD_BY, f.MSTF_MOD_DT, f.MSTF_ENGIN_CAP, f.MSTF_NOOF_SEAT, f.MSTF_OWN_EMAIL, f.MSTF_COMMENTS, f.MSTF_INSU_COM, f.MSTF_REGNO, vehicle.MFD_DRI, vehicle.MFD_ACT, vehicle.MFD_FRM_DT, vehicle.MFD_TO_DT, f.MSTF_CRE_BY, f.MSTF_CRE_DT, f.MSTF_MOD_BY, f.MSTF_MOD_DT, company, userDefPro, list,f.MSTF_COST);
        //            }
        //        }
        //        Int32 _effectOR2 = CHNLSVC.Tours.SaveFleetData(SeqNo, f.MSTF_REGNO, f.MSTF_DT, f.MSTF_BRD, f.MSTF_MODEL, f.MSTF_VEH_TP, f.MSTF_SIPP_CD, f.MSTF_ST_METER, f.MSTF_OWN, f.MSTF_OWN_NM, f.MSTF_OWN_CONT, f.MSTF_LST_SERMET, f.MSTF_TOU_REGNO, f.MSTF_IS_LEASE, f.MSTF_INSU_EXP, f.MSTF_REG_EXP, f.MSTF_FUAL_TP, f.MSTF_ACT, f.MSTF_CRE_BY, f.MSTF_CRE_DT, f.MSTF_MOD_BY, f.MSTF_MOD_DT, f.MSTF_ENGIN_CAP, f.MSTF_NOOF_SEAT, f.MSTF_OWN_EMAIL, f.MSTF_COMMENTS, f.MSTF_INSU_COM, f.MSTF_REGNO, MFD_DRI, f.MSTF_ACT, MFD_FRM_DT, MFD_TO_DT, f.MSTF_CRE_BY, f.MSTF_CRE_DT, f.MSTF_MOD_BY, f.MSTF_MOD_DT, company, userDefPro, list, f.MSTF_COST);

        //        Session["profitCenters"] = null;
        //        Session["seq"] = null;
        //    }
        //    else
        //    {
        //        DateTime MFD_FRM_DT = Convert.ToDateTime(MFD_FRM_DT1);
        //        DateTime MFD_TO_DT = Convert.ToDateTime(MFD_TO_DT1);

        //        MST_FLEET_NEW fleets = CHNLSVC.Tours.getMstFleetDetails(f.MSTF_REGNO);

        //        List<mst_fleet_driver> vehicleList = new List<mst_fleet_driver>();
        //        vehicleList = Session["vahicleAllowcation"] as List<mst_fleet_driver>;
        //        if (vehicleList != null)
        //        {
        //            foreach (mst_fleet_driver vehicle in vehicleList)
        //            {

        //                Int32 _effectOR = CHNLSVC.Tours.SaveFleetData(vehicle.MFD_SEQ.ToString(), f.MSTF_REGNO, f.MSTF_DT, f.MSTF_BRD, f.MSTF_MODEL, f.MSTF_VEH_TP, f.MSTF_SIPP_CD, f.MSTF_ST_METER, f.MSTF_OWN, f.MSTF_OWN_NM, f.MSTF_OWN_CONT, f.MSTF_LST_SERMET, f.MSTF_TOU_REGNO, f.MSTF_IS_LEASE, f.MSTF_INSU_EXP, f.MSTF_REG_EXP, f.MSTF_FUAL_TP, f.MSTF_ACT, f.MSTF_CRE_BY, f.MSTF_CRE_DT, f.MSTF_MOD_BY, f.MSTF_MOD_DT, f.MSTF_ENGIN_CAP, f.MSTF_NOOF_SEAT, f.MSTF_OWN_EMAIL, f.MSTF_COMMENTS, f.MSTF_INSU_COM, f.MSTF_REGNO, vehicle.MFD_DRI, vehicle.MFD_ACT, vehicle.MFD_FRM_DT, vehicle.MFD_TO_DT, f.MSTF_CRE_BY, f.MSTF_CRE_DT, f.MSTF_MOD_BY, f.MSTF_MOD_DT, company, userDefPro, list,f.MSTF_COST);
        //            }
        //        }
        //        Int32 _effectOR2 = CHNLSVC.Tours.SaveFleetData(SeqNo, f.MSTF_REGNO, f.MSTF_DT, f.MSTF_BRD, f.MSTF_MODEL, f.MSTF_VEH_TP, f.MSTF_SIPP_CD, f.MSTF_ST_METER, f.MSTF_OWN, f.MSTF_OWN_NM, f.MSTF_OWN_CONT, f.MSTF_LST_SERMET, f.MSTF_TOU_REGNO, f.MSTF_IS_LEASE, f.MSTF_INSU_EXP, f.MSTF_REG_EXP, f.MSTF_FUAL_TP, f.MSTF_ACT, f.MSTF_CRE_BY, f.MSTF_CRE_DT, f.MSTF_MOD_BY, f.MSTF_MOD_DT, f.MSTF_ENGIN_CAP, f.MSTF_NOOF_SEAT, f.MSTF_OWN_EMAIL, f.MSTF_COMMENTS, f.MSTF_INSU_COM, f.MSTF_REGNO, MFD_DRI, f.MSTF_ACT, MFD_FRM_DT, MFD_TO_DT, f.MSTF_CRE_BY, f.MSTF_CRE_DT, f.MSTF_MOD_BY, f.MSTF_MOD_DT, company, userDefPro, list,f.MSTF_COST);

        //        Session["profitCenters"] = null;
        //        Session["seq"] = null;
        //    }

        //    return RedirectToAction("Index");
        //}
        public JsonResult LoadStatus()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    ComboBoxObject o1 = new ComboBoxObject();
                    o1.Text = "ACTIVE";
                    o1.Value = "1";
                    oList.Add(o1);

                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "INACTIVE";
                    o2.Value = "0";
                    oList.Add(o2);


                    return Json(new { success = true, login = true, data = oList }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = Resource.ServerError }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult LoadFuelType()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    ComboBoxObject o1 = new ComboBoxObject();
                    o1.Text = "DIESEL";
                    o1.Value = "DIESEL";
                    oList.Add(o1);

                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "PETROL";
                    o2.Value = "PETROL";
                    oList.Add(o2);
                    return Json(new { success = true, login = true, data = oList }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = Resource.ServerError }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult LoadSource()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    ComboBoxObject o1 = new ComboBoxObject();
                    o1.Text = "INTERNAL";
                    o1.Value = "INTERNAL";
                    oList.Add(o1);

                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "EXTERNAL";
                    o2.Value = "EXTERNAL";
                    oList.Add(o2);

                    ComboBoxObject o3 = new ComboBoxObject();
                    o3.Text = "TEMPORARY";
                    o3.Value = "TEMPORARY";
                    oList.Add(o3);
                    return Json(new { success = true, login = true, data = oList }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = Resource.ServerError }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult LoadVehType()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    ComboBoxObject o1 = new ComboBoxObject();
                    o1.Text = "CAR";
                    o1.Value = "CAR";
                    oList.Add(o1);

                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "VAN";
                    o2.Value = "VAN";
                    oList.Add(o2);

                    ComboBoxObject o3 = new ComboBoxObject();
                    o3.Text = "BUS";
                    o3.Value = "BUS";
                    oList.Add(o3);

                    ComboBoxObject o4 = new ComboBoxObject();
                    o4.Text = "SUV";
                    o4.Value = "SUV";
                    oList.Add(o4);


                    return Json(new { success = true, login = true, data = oList }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = Resource.ServerError }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult loadResons()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    ComboBoxObject o1 = new ComboBoxObject();
                    o1.Text = "Hiring";
                    o1.Value = "Hiring";
                    oList.Add(o1);

                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "Long term lease";
                    o2.Value = "Long term lease";
                    oList.Add(o2);

                    ComboBoxObject o3 = new ComboBoxObject();
                    o3.Text = "Short term lease";
                    o3.Value = "Short term lease";
                    oList.Add(o3);

                    ComboBoxObject o4 = new ComboBoxObject();
                    o4.Text = "Company owned";
                    o4.Value = "Company owned";
                    oList.Add(o4);

                    return Json(new { success = true, login = true, data = oList }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = Resource.ServerError }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult getFleetDetails(string val)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                val = val.Trim();
                MST_FLEET_NEW fleets = CHNLSVC.Tours.getMstFleetDetails(val);

                List<mst_fleet_driver> vehiAllocations = CHNLSVC.Tours.getAllocateVehiclesByID(val);
                if (vehiAllocations.Count > 0)
                {
                    fleets.mstFleetDriver = vehiAllocations;
                    Session["vahicleAllowcation"] = vehiAllocations;
                }
                else
                {

                    Session["vahicleAllowcation"] = null;
                }
            
                List<mst_fleet_alloc> list = new List<mst_fleet_alloc>();
                list = CHNLSVC.Tours.Get_mst_fleet_alloc(val);
                Session["profitCenters"] = list;

                if (list.Count > 0)
                {
                   
                    fleets.profitCenterLstss = list;
                }
                else
                {
                    fleets.profitCenterLstss = null;
                }
                if (fleets != null)
                {
                    return Json(new { success = true, login = true, data = fleets }, JsonRequestBehavior.AllowGet);
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

        public JsonResult updateVehicle(string allowId, string activate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                List<mst_fleet_driver> list = Session["vahicleAllowcation"] as List<mst_fleet_driver>;
                list.First(d => d.MFD_SEQ == Convert.ToInt32(allowId)).MFD_ACT = (activate == "true") ? 1 : 0;
                Session["vahicleAllowcation"] = list;

                return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
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
        public ActionResult Alert()
        {
            return View();
        }
        public JsonResult FleetCreationNew(mst_fleet f)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string MFD_DRI = Request["MFD_DRI"];
            string MFD_FRM_DT1 = Request["MFD_FRM_DT"];
            string MFD_TO_DT1 = Request["MFD_TO_DT"];
            string profitcenter = Request["MFA_PC"];
            string MSTF_IS_LEASE1 = Request["MSTF_IS_LEASE"];
            int MSTF_IS_LEASE = Convert.ToInt32(MSTF_IS_LEASE1);
            f.MSTF_CRE_BY = userId;
            f.MSTF_MOD_BY = userId;
            DateTime today = DateTime.Today;
            f.MSTF_MOD_DT = today;
            f.MSTF_CRE_DT = today;
            String SeqNo = "";

            if (f.MSTF_MODEL == null | f.MSTF_MODEL=="")
            {
                return Json(new { success = false, login = true, msg = "Vehicle Modal Required", type = "Info" }, JsonRequestBehavior.AllowGet);
            }
            if (f.MSTF_BRD == null | f.MSTF_BRD=="")
            {
                return Json(new { success = false, login = true, msg = "Vehicle Brand Required", type = "Info" }, JsonRequestBehavior.AllowGet);
            }
            if (f.MSTF_ENGIN_CAP == null | f.MSTF_ENGIN_CAP == "")
            {
                return Json(new { success = false, login = true, msg = "Engine Capacity Required", type = "Info" }, JsonRequestBehavior.AllowGet);
            }
            if (f.MSTF_NOOF_SEAT == null | f.MSTF_NOOF_SEAT ==0)
            {
                return Json(new { success = false, login = true, msg = "No Of Seats Required", type = "Info" }, JsonRequestBehavior.AllowGet);
            }
            //if (f.MSTF_SIPP_CD == null | f.MSTF_SIPP_CD =="")
            //{
            //    return Json(new { success = false, login = true, msg = "SIPP Code Required", type = "Info" }, JsonRequestBehavior.AllowGet);
            //}
            if (f.MSTF_OWN_NM == null | f.MSTF_OWN_NM == "")
            {
                return Json(new { success = false, login = true, msg = "Owner Name Required", type = "Info" }, JsonRequestBehavior.AllowGet);
            }
            if (f.MSTF_OWN_CONT == null | f.MSTF_OWN_CONT ==0)
            {
                return Json(new { success = false, login = true, msg = "Owner Contact Number Required", type = "Info" }, JsonRequestBehavior.AllowGet);
            }
            if (f.MSTF_OWN_EMAIL == null | f.MSTF_OWN_EMAIL =="")
            {
                return Json(new { success = false, login = true, msg = "Owner Email Required", type = "Info" }, JsonRequestBehavior.AllowGet);
            }
            if (f.MSTF_LST_SERMET == null | f.MSTF_LST_SERMET == 0)
            {
                return Json(new { success = false, login = true, msg = "Last Service Meter Required", type = "Info" }, JsonRequestBehavior.AllowGet);
            }
            if (f.MSTF_INSU_COM == null | f.MSTF_INSU_COM =="")
            {
                return Json(new { success = false, login = true, msg = "Insuarance Company Required", type = "Info" }, JsonRequestBehavior.AllowGet);
            }
            List<mst_fleet_alloc> list = new List<mst_fleet_alloc>();
            if (Session["profitCenters"] != null)
            {
                List<mst_fleet_alloc> profCen = Session["profitCenters"] as List<mst_fleet_alloc>;
                foreach (mst_fleet_alloc prof in profCen)
                {
                    mst_fleet_alloc mst_pcemp = new mst_fleet_alloc();
                    mst_pcemp.MFA_REGNO = f.MSTF_REGNO;
                    // mst_pcemp.MPE_COM = company;
                    mst_pcemp.MFA_PC = prof.MFA_PC;
                    //  mst_pcemp.MPE_ASSN_DT = DateTime.Now;
                    mst_pcemp.MFA_ACT = Convert.ToInt32(prof.MFA_ACT);
                    list.Add(mst_pcemp);
                }
            }

            if (MFD_FRM_DT1 == "" | MFD_TO_DT1 == "")
            {
                DateTime MFD_FRM_DT = new DateTime(1974, 7, 10, 7, 10, 24);
                DateTime MFD_TO_DT = new DateTime(1974, 7, 10, 7, 10, 24);

                MST_FLEET_NEW fleets = CHNLSVC.Tours.getMstFleetDetails(f.MSTF_REGNO);

                List<mst_fleet_driver> vehicleList = new List<mst_fleet_driver>();
                vehicleList = Session["vahicleAllowcation"] as List<mst_fleet_driver>;

                Int32 _effectOR2 = CHNLSVC.Tours.SaveFleetData(SeqNo, f.MSTF_REGNO, f.MSTF_DT, f.MSTF_BRD, f.MSTF_MODEL, f.MSTF_VEH_TP, f.MSTF_SIPP_CD, f.MSTF_ST_METER, f.MSTF_OWN, f.MSTF_OWN_NM, f.MSTF_OWN_CONT, f.MSTF_LST_SERMET, f.MSTF_TOU_REGNO, f.MSTF_IS_LEASE, f.MSTF_INSU_EXP, f.MSTF_REG_EXP, f.MSTF_FUAL_TP, f.MSTF_ACT, f.MSTF_CRE_BY, f.MSTF_CRE_DT, f.MSTF_MOD_BY, f.MSTF_MOD_DT, f.MSTF_ENGIN_CAP, f.MSTF_NOOF_SEAT, f.MSTF_OWN_EMAIL, f.MSTF_COMMENTS, f.MSTF_INSU_COM, f.MSTF_REGNO, MFD_DRI, f.MSTF_ACT, MFD_FRM_DT, MFD_TO_DT, f.MSTF_CRE_BY, f.MSTF_CRE_DT, f.MSTF_MOD_BY, f.MSTF_MOD_DT, company, userDefPro, list, f.MSTF_COST, f.MSTF_REASON, f.MSTF_OWN_ADD1, f.MSTF_OWN_ADD2, f.MSTF_FROM_DT, f.MSTF_TO_DT,f.MSTF_OWN_NIC,f.MSTF_PRO_MILGE,f.MSTF_ADD_FULL_DAY,f.MSTF_ADD_HALF_DAY,f.MSTF_ADD_AIR_RET,f.MSTF_CORR_AMT,f.MSTF_HIRING_DEPOSITE);
                if (_effectOR2 == 1)
                {
                    Session["profitCenters"] = null;
                    Session["seq"] = null;
                    return Json(new { success = true, login = true, msg = "Fleet Creation Successful", type = "Success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "Get Error", type = "Fail" }, JsonRequestBehavior.AllowGet);
                }
               
            }
            else
            {
                DateTime MFD_FRM_DT = Convert.ToDateTime(MFD_FRM_DT1);
                DateTime MFD_TO_DT = Convert.ToDateTime(MFD_TO_DT1);

                MST_FLEET_NEW fleets = CHNLSVC.Tours.getMstFleetDetails(f.MSTF_REGNO);

                List<mst_fleet_driver> vehicleList = new List<mst_fleet_driver>();
                vehicleList = Session["vahicleAllowcation"] as List<mst_fleet_driver>;
               
                Int32 _effectOR2 = CHNLSVC.Tours.SaveFleetData(SeqNo, f.MSTF_REGNO, f.MSTF_DT, f.MSTF_BRD, f.MSTF_MODEL, f.MSTF_VEH_TP, f.MSTF_SIPP_CD, f.MSTF_ST_METER, f.MSTF_OWN, f.MSTF_OWN_NM, f.MSTF_OWN_CONT, f.MSTF_LST_SERMET, f.MSTF_TOU_REGNO, f.MSTF_IS_LEASE, f.MSTF_INSU_EXP, f.MSTF_REG_EXP, f.MSTF_FUAL_TP, f.MSTF_ACT, f.MSTF_CRE_BY, f.MSTF_CRE_DT, f.MSTF_MOD_BY, f.MSTF_MOD_DT, f.MSTF_ENGIN_CAP, f.MSTF_NOOF_SEAT, f.MSTF_OWN_EMAIL, f.MSTF_COMMENTS, f.MSTF_INSU_COM, f.MSTF_REGNO, MFD_DRI, f.MSTF_ACT, MFD_FRM_DT, MFD_TO_DT, f.MSTF_CRE_BY, f.MSTF_CRE_DT, f.MSTF_MOD_BY, f.MSTF_MOD_DT, company, userDefPro, list,f.MSTF_COST,f.MSTF_REASON, f.MSTF_OWN_ADD1, f.MSTF_OWN_ADD2,f.MSTF_FROM_DT,f.MSTF_TO_DT,f.MSTF_OWN_NIC,f.MSTF_PRO_MILGE,f.MSTF_ADD_FULL_DAY,f.MSTF_ADD_HALF_DAY,f.MSTF_ADD_AIR_RET,f.MSTF_CORR_AMT, f.MSTF_HIRING_DEPOSITE);
                if (_effectOR2 == 1)
                {
                    Session["profitCenters"] = null;
                    Session["seq"] = null;
                    return Json(new { success = true, login = true, msg = "Fleet Creation Successful", type = "Success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Session["profitCenters"] = null;
                    Session["seq"] = null;
                    return Json(new { success = false, login = true, msg = "Get Error", type = "Fail" }, JsonRequestBehavior.AllowGet);
                }
              
              
            }

            return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FleetAggrement(string regno , string prtype)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                regno = regno.Trim();
                ReportDocument rd = new ReportDocument();
                if (prtype == "Hiring")
                {
                    // get agreement para
                    DataTable dt = new DataTable();
                    DataRow dr;
                    dr = dt.NewRow();
                    DataTable agrrpara = CHNLSVC.Tours.GetAgreementParameters(2);
                    int p = 0;
                    foreach (var para in agrrpara.Rows)
                    {
                        dt.Columns.Add(agrrpara.Rows[p]["fagp_desc"].ToString(), typeof(string));
                        p++;
                    }
                    p = 0;
                    foreach (var para in agrrpara.Rows)
                    {
                        dr[agrrpara.Rows[p]["fagp_desc"].ToString()] = agrrpara.Rows[p]["fagp_para1"].ToString();
                        p++;
                    }
                    dt.Rows.Add(dr);
                    

                    MST_FLEET_NEW fleets = CHNLSVC.Tours.getMstFleetDetails(regno);
                    Int32 months = ((fleets.MSTF_TO_DT.Year - fleets.MSTF_FROM_DT.Year) * 12) + (fleets.MSTF_TO_DT.Month - fleets.MSTF_FROM_DT.Month);
                    fleets.month = months.ToString();
                    fleets.rental = Convert.ToDecimal(fleets.MSTF_COST);
                    fleets.days =Convert.ToDecimal( (months * 30));
                    fleets.year = (Math.Round((Convert.ToDecimal( months)/ 12), 2)).ToString();
                    List<MST_FLEET_NEW> fleetlist = new List<MST_FLEET_NEW>();
                    fleetlist.Add(fleets);
                    rd.Load(Server.MapPath("/Reports/" + "FleetAggrement2.rpt"));
                    rd.Database.Tables["FleetAggrement"].SetDataSource(fleetlist);
                    rd.Database.Tables["Param2"].SetDataSource(dt);
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                }
                else
                {
                    // get agreement para
                    DataTable dt = new DataTable();
                    DataRow dr;
                    dr = dt.NewRow();
                    DataTable agrrpara = CHNLSVC.Tours.GetAgreementParameters(1);
                    int p = 0;
                    foreach (var para in agrrpara.Rows)
                    {
                        dt.Columns.Add(agrrpara.Rows[p]["fagp_desc"].ToString(), typeof(string));
                        p++;
                    }
                    p = 0;
                    foreach (var para in agrrpara.Rows)
                    {
                        dr[agrrpara.Rows[p]["fagp_desc"].ToString()] = agrrpara.Rows[p]["fagp_para1"].ToString();
                        p++;
                    }
                    dt.Rows.Add(dr);

                    MST_FLEET_NEW fleets = CHNLSVC.Tours.getMstFleetDetails(regno);
                    Int32 months = ((fleets.MSTF_TO_DT.Year - fleets.MSTF_FROM_DT.Year) * 12) + (fleets.MSTF_TO_DT.Month - fleets.MSTF_FROM_DT.Month);
                    fleets.month = months.ToString();
                    fleets.rental = Convert.ToDecimal(fleets.MSTF_COST);
                    List<MST_FLEET_NEW> fleetlist = new List<MST_FLEET_NEW>();
                    fleetlist.Add(fleets);
                    rd.Load(Server.MapPath("/Reports/" + "FleetAggrement.rpt"));
                    rd.Database.Tables["FleetAggrement"].SetDataSource(fleetlist);
                    rd.Database.Tables["Param1"].SetDataSource(dt);
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                }

               
                try
                {
                    this.Response.Clear();
                    this.Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
                    return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
            {
            
                return Redirect("~/Login");

            }
        }
        public ActionResult FleetInvoicePrint(string fleet, DateTime fromdate, DateTime todate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                fleet = (fleet != null) ? fleet.Trim() : null;
                if (fleet != null)
                {
                    ReportDocument rd = new ReportDocument();
                    DataTable fleetinvoice = CHNLSVC.Tours.GETFLEETGES(fleet,fromdate,todate);
                    rd.Load(Server.MapPath("/Reports/" + "rpt_fleet_invoice.rpt"));
                    rd.Database.Tables["FleetInvoice"].SetDataSource(fleetinvoice);
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        this.Response.Clear();
                        this.Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
                        return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                else
                {
                    return Redirect("~/Invoicing");
                }

            }
            else
            {
                return Redirect("~/Login");
            }

        }
    }
}