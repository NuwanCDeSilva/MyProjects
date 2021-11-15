using FF.BusinessObjects;
using FF.BusinessObjects.Tours;
using FF.BusinessObjects.ToursNew;
using FF.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FastForward.WebAbansTours.Controllers
{
    public class EmployeeController : BaseController
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
                ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1008);
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
        // GET: Employee
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
            else {
                return Redirect("~/Login/index");
            }
        }
        public JsonResult gettitleList() { 
        
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_TITLE> titleList = CHNLSVC.Tours.GetTitleList();
                    return Json(new { success = true, login = true, data = titleList }, JsonRequestBehavior.AllowGet);
                }
                else { 
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }catch(Exception ex)
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
        public JsonResult getEmployeeTypeList() {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<Mst_empcate> mst_empcate = CHNLSVC.Tours.Get_mst_empcate();
                    return Json(new { success = true, login = true, data = mst_empcate }, JsonRequestBehavior.AllowGet);
                }
                else { 
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
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

        public JsonResult getEmployeeDetails(string val) 
        { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    val = val.Trim();
                    MST_EMPLOYEE_NEW_TBS employees = CHNLSVC.Tours.getMstEmployeeDetails(val);
                    if (employees == null) {
                        return Json(new { success = true, login = true, data = "" }, JsonRequestBehavior.AllowGet);
                    }
                    List<MST_PCEMP> list = new List<MST_PCEMP>();
                    list = CHNLSVC.Tours.Get_mst_pcemp(val);
                    Session["profitCenters"] = list;

                    if (list.Count > 0)
                    {
                        employees.profitCenterLst = list;
                    }
                    else
                    {
                        employees.profitCenterLst = null;
                    }


                    List<mst_fleet_driver> vehiAllocations = CHNLSVC.Tours.getAllocateVehicles(val);
                    if (vehiAllocations.Count > 0)
                    {
                        employees.mstFleetDriver = vehiAllocations;
                        Session["vahicleAllowcation"] = vehiAllocations;
                    }
                    else
                    {

                        Session["vahicleAllowcation"] = null;
                    }
                    if (employees != null)
                    {
                        return Json(new { success = true, login = true, data = employees }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = true, login = true, data = "" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else { 
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getEmployeeDetailsByNic(string Nic)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    Nic = Nic.Trim();
                    MST_EMPLOYEE_NEW_TBS employees = CHNLSVC.Tours.getMstEmployeeDetailsByNic(Nic);
                    if (employees != null) {
                        List<MST_PCEMP> list = new List<MST_PCEMP>();
                        list = CHNLSVC.Tours.Get_mst_pcemp(employees.MEMP_CD);
                        Session["profitCenters"] = list;

                        if (list.Count > 0)
                        {
                            employees.profitCenterLst = list;
                        }
                        else
                        {
                            employees.profitCenterLst = null;
                        }


                        List<mst_fleet_driver> vehiAllocations = CHNLSVC.Tours.getAllocateVehicles(employees.MEMP_CD);
                        if (vehiAllocations.Count > 0)
                        {
                            employees.mstFleetDriver = vehiAllocations;
                            Session["vahicleAllowcation"] = vehiAllocations;
                        }
                        else
                        {

                            Session["vahicleAllowcation"] = null;
                        }
                        if (employees != null)
                        {
                            return Json(new { success = true, login = true, data = employees }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = true, login = true, data = "" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    return Json(new { success = true, login = true,data="" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult addProfitCenter(string val,string epf) {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_PCEMP> list = Session["profitCenters"] as List<MST_PCEMP>;
                    MST_PCEMP profit = new MST_PCEMP();
                    profit.MPE_PC = val;
                    profit.MPE_ACT = 1;
                    profit.MPE_EPF = epf;
                    profit.MPE_COM = company;
                    list.Add(profit);
                    Session["profitCenters"] = list;
                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                }
                else { 
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
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
        public JsonResult removeProfitCenter(string val) {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_PCEMP> list = Session["profitCenters"] as List<MST_PCEMP>;
                    //list.RemoveAll(a => a.MPE_PC == val);
                    list.First(a => a.MPE_PC == val).MPE_ACT = 0;
                    Session["profitCenters"] = list;
                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);

                }
                else { 
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError,type="Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult addLanguages(string val, string epf)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_LANGUAGE> list = Session["languages"] as List<MST_LANGUAGE>;
                    if (Session["languages"]==null)
                    {
                        list = new List<MST_LANGUAGE>();
                        Session["languages"]= list; 
                    }
                    MST_LANGUAGE lang = new MST_LANGUAGE();
                    lang.mla_cd = val;
                    list.Add(lang);
                    Session["languages"] = list;
                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult removeLanguages(string val)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_LANGUAGE> list = Session["languages"] as List<MST_LANGUAGE>;
                    list.RemoveAll(a => a.mla_cd == val);
                   // list.First(a => a.code == val).code = 0;
                    Session["languages"] = list;
                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);

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
        public JsonResult employeeCreation(MST_EMPLOYEE_NEW_TBS emp) {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string err = "0";
                    int check = CHNLSVC.Tours.Check_Employeeepf(emp.MEMP_CD);
                    if (check == 1)
                    {
                        return Json(new { success = false, login = true, msg = Resource.txtInvEpfNo, type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (emp.MEMP_CD == null)
                        {
                            return Json(new { success = false, login = true, msg = Resource.errInvalidEmpCde, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (emp.MEMP_NIC == null)
                        {
                            return Json(new { success = false, login = true, msg = Resource.errInvalidNIC, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (emp.MEMP_FIRST_NAME == null)
                        {
                            return Json(new { success = false, login = true, msg = Resource.errInvalidfname, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (emp.MEMP_LAST_NAME == null)
                        {
                            return Json(new { success = false, login = true, msg = Resource.errInvalidlname, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (emp.MEMP_LIVING_ADD_1 == null)
                        {
                            return Json(new { success = false, login = true, msg = Resource.errInvalidadd, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        emp.MEMP_EPF = emp.MEMP_CD;
                        emp.MEMP_COM_CD = company;
                        emp.MEMP_CRE_BY = Convert.ToString(Session["UserID"]);
                        emp.MEMP_CRE_DT = DateTime.Now;

                        if (emp.MEMP_DOB.ToString() != "")
                            emp.MEMP_DOB = Convert.ToDateTime(emp.MEMP_DOB);
                        if (emp.MEMP_DOB.ToString() != "")
                            emp.MEMP_DOJ = Convert.ToDateTime(emp.MEMP_DOJ);
                        List<mst_fleet_driver> vehicleList = new List<mst_fleet_driver>();
                        if (emp.MEMP_CAT_CD == "DRIVER")
                        {
                            emp.MEMP_LIC_NO = emp.MEMP_LIC_NO;
                            emp.MEMP_LIC_CAT = emp.MEMP_LIC_CAT;
                            if (emp.MEMP_LIC_EXDT.ToString() != "")
                                emp.MEMP_LIC_EXDT = Convert.ToDateTime(emp.MEMP_LIC_EXDT);

                            vehicleList = Session["vahicleAllowcation"] as List<mst_fleet_driver>;
                            if (vehicleList != null)
                            {
                                foreach (mst_fleet_driver vehicle in vehicleList)
                                {
                                    if (CheckOverLapDates(vehicle.MFD_FRM_DT.ToString(), vehicle.MFD_TO_DT.ToString(), vehicle.MFD_VEH_NO, vehicle.MFD_DRI, vehicle.MFD_SEQ.ToString()))
                                    {
                                        return Json(new { success = false, login = true, msg = "Either " + vehicle.MFD_VEH_NO + " vehicle or driver " + vehicle.MFD_DRI + " has another active allocation with in this period", type = "Info" }, JsonRequestBehavior.AllowGet);

                                    }

                                }

                            }
                        }
                        else
                        {
                            emp.MEMP_LIC_NO = "";
                            emp.MEMP_LIC_CAT = "";
                        }
                        List<MST_PCEMP> list = new List<MST_PCEMP>();
                        if (Session["profitCenters"] != null)
                        {
                            List<MST_PCEMP> profCen = Session["profitCenters"] as List<MST_PCEMP>;
                            foreach (MST_PCEMP prof in profCen)
                            {
                                MST_PCEMP mst_pcemp = new MST_PCEMP();
                                mst_pcemp.MPE_EPF = emp.MEMP_CD;
                                mst_pcemp.MPE_COM = company;
                                mst_pcemp.MPE_PC = prof.MPE_PC;
                                mst_pcemp.MPE_ASSN_DT = DateTime.Now;
                                mst_pcemp.MPE_ACT = Convert.ToInt32(prof.MPE_ACT);

                                list.Add(mst_pcemp);
                            }
                            //list = (List<MST_PCEMP>)Session["ProfitCenter"];
                        }
                        //save languages
                        List<MST_LANGUAGE> langlist = (List<MST_LANGUAGE>)Session["languages"];
                        string lan = "";
                        if (langlist !=null)
                        {
                            foreach(var lancode in langlist)
                            {
                                lan = lan + lancode.mla_cd.ToString() + ",";
                            }
                        }
                        emp.MEMP_ANAL3 = lan;
                        DataTable dtdrivercom = CHNLSVC.Tours.SP_TOURS_GET_DRIVER_COM(emp.MEMP_EPF);
                        if (dtdrivercom.Rows.Count > 0)
                        {
                            foreach (DataRow item in dtdrivercom.Rows)
                            {
                                Session["DRIVERCOM"] = item[0].ToString();
                            }
                        }
                        string driveCom = Session["DRIVERCOM"] as string;
                        DateTime serverDatetime = CHNLSVC.Security.GetServerDateTime();
                        int results = CHNLSVC.Tours.SaveEmployeeData(emp, list, vehicleList, userId, userDefPro, serverDatetime, driveCom, out err);

                        if (results == 1)
                        {
                            return Json(new { success = true, login = true, msg = Resource.txtEmpCreteSuccess }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = err, type = "Error" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else { 
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        private bool CheckOverLapDates(string fromdte,string todte,string vehicle,string driver,string seq_no)
        {
            try
            {
                DateTime fromdate = Convert.ToDateTime(fromdte);
                DateTime todate = Convert.ToDateTime(todte);
                string p_mfd_seq_no = seq_no;

                DataTable dtoverlapdates = new DataTable();

                if (Convert.ToInt32(p_mfd_seq_no)==0)
                {
                    dtoverlapdates = CHNLSVC.Tours.SP_TOURS_GET_ALL_OVERLAP_DATES(vehicle, driver, fromdate, todate);
                }
                //else
                //{
                //    dtoverlapdates = CHNLSVC.Tours.SP_TOURS_GET_OVERLAP_DATES(vehicle, driver, fromdate, todate, p_mfd_seq_no);
                //}


                if (dtoverlapdates.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult removeSesData()
        { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_PCEMP> list = new List<MST_PCEMP>(); ;
                    Session["profitCenters"] = list;
                    List<mst_fleet_driver> listDrv = Session["vahicleAllowcation"] as List<mst_fleet_driver>;
                    Session["vahicleAllowcation"] = listDrv;
                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                }
                else { 
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        
        }
        public JsonResult employeeUpdate(MST_EMPLOYEE_NEW_TBS emp) { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (emp.MEMP_CD == null)
                    {
                        return Json(new { success = false, login = true, msg = Resource.errInvalidEmpCde, type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (emp.MEMP_NIC == null)
                    {
                        return Json(new { success = false, login = true, msg = Resource.errInvalidNIC, type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (emp.MEMP_FIRST_NAME == null)
                    {
                        return Json(new { success = false, login = true, msg = Resource.errInvalidfname, type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (emp.MEMP_LAST_NAME == null)
                    {
                        return Json(new { success = false, login = true, msg = Resource.errInvalidlname, type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (emp.MEMP_LIVING_ADD_1 == null)
                    {
                        return Json(new { success = false, login = true, msg = Resource.errInvalidadd, type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    string err = "0";
                    emp.MEMP_EPF = emp.MEMP_CD;
                    emp.MEMP_COM_CD = company;
                    emp.MEMP_CRE_BY = Convert.ToString(Session["UserID"]);
                    emp.MEMP_CRE_DT = DateTime.Now;

                    if (emp.MEMP_DOB.ToString() != "")
                        emp.MEMP_DOB = Convert.ToDateTime(emp.MEMP_DOB);
                    if (emp.MEMP_DOJ.ToString() != "")
                        emp.MEMP_DOJ = Convert.ToDateTime(emp.MEMP_DOJ);
                    List<mst_fleet_driver> vehicleList = new List<mst_fleet_driver>();
                    if (emp.MEMP_CAT_CD == "DRIVER")
                    {
                        emp.MEMP_LIC_NO = emp.MEMP_LIC_NO;
                        emp.MEMP_LIC_CAT = emp.MEMP_LIC_CAT;
                        if (emp.MEMP_LIC_EXDT.ToString() != "")
                            emp.MEMP_LIC_EXDT = Convert.ToDateTime(emp.MEMP_LIC_EXDT);
                        vehicleList = Session["vahicleAllowcation"] as List<mst_fleet_driver>;
                        if (vehicleList != null)
                        {
                            foreach (mst_fleet_driver vehicle in vehicleList)
                            {
                                if (CheckOverLapDates(vehicle.MFD_FRM_DT.ToString(), vehicle.MFD_TO_DT.ToString(), vehicle.MFD_VEH_NO, vehicle.MFD_DRI, vehicle.MFD_SEQ.ToString()))
                                {
                                    return Json(new { success = false, login = true, msg = "Either " + vehicle.MFD_VEH_NO + " vehicle or driver " + vehicle.MFD_DRI + " has another active allocation with in this period", type = "Info" }, JsonRequestBehavior.AllowGet);

                                }

                            }

                        }
                    }
                    else
                    {
                        emp.MEMP_LIC_NO = "";
                        emp.MEMP_LIC_CAT = "";
                    }
                    List<MST_PCEMP> list = new List<MST_PCEMP>();
                    if (Session["profitCenters"] != null)
                    {
                        List<MST_PCEMP> profCen = Session["profitCenters"] as List<MST_PCEMP>;
                        foreach (MST_PCEMP prof in profCen)
                        {
                            MST_PCEMP mst_pcemp = new MST_PCEMP();
                            mst_pcemp.MPE_EPF = emp.MEMP_CD;
                            mst_pcemp.MPE_COM = company;
                            mst_pcemp.MPE_PC = prof.MPE_PC;
                            mst_pcemp.MPE_ASSN_DT = DateTime.Now;
                            mst_pcemp.MPE_ACT = Convert.ToInt32(prof.MPE_ACT);

                            list.Add(mst_pcemp);
                        }
                        //list = (List<MST_PCEMP>)Session["ProfitCenter"];

                    }
                    DataTable dtdrivercom = CHNLSVC.Tours.SP_TOURS_GET_DRIVER_COM(emp.MEMP_EPF);
                    if (dtdrivercom.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtdrivercom.Rows)
                        {
                            Session["DRIVERCOM"] = item[0].ToString();
                        }
                    }
                    string driveCom = Session["DRIVERCOM"] as string;
                    DateTime serverDatetime = CHNLSVC.Security.GetServerDateTime();
                    //save languages
                    List<MST_LANGUAGE> langlist = (List<MST_LANGUAGE>)Session["languages"];
                    string lan = "";
                    if (langlist != null)
                    {
                        foreach (var lancode in langlist)
                        {
                            lan = lan + lancode.mla_cd.ToString() + ",";
                        }
                    }
                    emp.MEMP_ANAL3 = lan;
                    int results = CHNLSVC.Tours.UpdateEmployeeData(emp, list, vehicleList, userId, userDefPro, serverDatetime, driveCom, out err);

                    if (results > 0)
                    {
                        return Json(new { success = true, login = true, msg = Resource.txtEmpUpdateSuccess }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = err, type = "Error" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else { 
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
        public JsonResult getVehicleList() { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    DataTable dtvehicle = CHNLSVC.Tours.SP_TOURS_GET_VEHICLE(userDefPro);
                    List<string> list = new List<string>();
                    if (dtvehicle != null)
                    {
                        for (int i = 0; i < dtvehicle.Rows.Count; i++)
                        {
                            list.Add(dtvehicle.Rows[i]["MFA_REGNO"].ToString());
                        }
                    }
                    return Json(new { success = true, login = true, data = list }, JsonRequestBehavior.AllowGet);
                }
                else { 
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
        public JsonResult updateVehicle(string allowId, string activate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<mst_fleet_driver> list = Session["vahicleAllowcation"] as List<mst_fleet_driver>;
                    list.First(d => d.MFD_SEQ == Convert.ToInt32(allowId)).MFD_ACT = (activate == "true") ? 1 : 0;
                    Session["vahicleAllowcation"] = list;

                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                }
                else { 
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
        public JsonResult addVehicleAllowcation(string empId, string vehicle, string from_dte, string to_dte)
        { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if(  vehicle==""|| from_dte==""|| to_dte==""){
                        if (vehicle == "")
                        {
                            return Json(new { success = false, login = true, msg = "Please select vehicle.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (from_dte == "")
                        {
                            return Json(new { success = false, login = true, msg = "Please add from date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (to_dte == "")
                        {
                            return Json(new { success = false, login = true, msg = "Please add to date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    DateTime fd = Convert.ToDateTime(from_dte);
                    DateTime td = Convert.ToDateTime(to_dte);
                    DateTime now = DateTime.Today;
                    if (fd < now || td < now || fd > td)
                    {
                        if (fd > td) {
                            return Json(new { success = false, login = true, msg = "To date must be greater than from date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (fd < now|| td < now) {
                            return Json(new { success = false, login = true, msg = "Date must be greater than current date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    List<mst_fleet_driver> list = Session["vahicleAllowcation"] as List<mst_fleet_driver>;
                    if (list == null)
                    {
                        list = new List<mst_fleet_driver>();
                    }
                    
                    //list.Where((m => m.MFD_FRM_DT <= fd && m.MFD_TO_DT >= td) || ( m.MFD_FRM_DT >= fd && m.MFD_TO_DT <= td));
                    List<mst_fleet_driver> fleetval = list.Where(m => m.MFD_FRM_DT >= fd && m.MFD_TO_DT <= td || (m.MFD_FRM_DT >= fd && m.MFD_TO_DT >= td && td >= m.MFD_FRM_DT) || (m.MFD_FRM_DT <= fd && fd <= m.MFD_TO_DT && m.MFD_TO_DT <= td) || (m.MFD_FRM_DT <= fd && fd <= m.MFD_TO_DT && m.MFD_TO_DT <= td && td >= m.MFD_FRM_DT)).ToList();
                    
                    if (fleetval.Count > 0) {
                        return Json(new { success = false, login = true,msg="Driver allocate for some date range in this vehicle already.",type="Info" }, JsonRequestBehavior.AllowGet);
                    }
                    
                    mst_fleet_driver fleet = new mst_fleet_driver();
                    fleet.MFD_ACT = 1;
                    fleet.MFD_DRI = empId;
                    fleet.MFD_FRM_DT = Convert.ToDateTime(from_dte);
                    fleet.MFD_TO_DT = Convert.ToDateTime(to_dte);
                    fleet.MFD_VEH_NO = vehicle;
                    list.Add(fleet);
                    Session["vahicleAllowcation"] = list;
                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                }
                else { 
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
        public JsonResult LoadDriverTypes()
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
                    o1.Text = "Company drivers";
                    o1.Value = "Company drivers";
                    oList.Add(o1);

                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "Leased car drivers";
                    o2.Value = "Company drivers";
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
        public JsonResult LoadLanguages()
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
                    DataTable languagelist = CHNLSVC.General.get_Language();
                    int i = 0;
                    if (languagelist.Rows.Count>0)
                    {
                        foreach (var dt in languagelist.Rows)
                        {
                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = languagelist.Rows[i]["mla_desc"].ToString();
                            o1.Value = languagelist.Rows[i]["mla_cd"].ToString();
                            oList.Add(o1);
                            i++;
                        }
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
                return Json(new { success = false, login = true, data = Resource.ServerError }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult dobGeneration(string nic)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    //String nic_ = nic.Trim().ToUpper();
                    //char[] nicarray = nic_.ToCharArray();
                    //string thirdNum = (nicarray[2]).ToString();

                    ////---------DOB generation----------------------
                    //string threechar = (nicarray[2]).ToString() + (nicarray[3]).ToString() + (nicarray[4]).ToString();
                    //Int32 DPBnum = Convert.ToInt32(threechar);
                    //if (DPBnum > 500)
                    //{
                    //    DPBnum = DPBnum - 500;
                    //}

                    //// Int32 JAN = 1, FEF = 2, MAR = 3, APR = 4, MAY = 5, JUN = 6, JUL = 7, AUG = 8, SEP = 9, OCT = 10, NOV = 11, DEC = 12;
                    //Dictionary<string, Int32> monthDict = new Dictionary<string, int>();
                    //monthDict.Add("JAN", 31);
                    //monthDict.Add("FEF", 29);
                    //monthDict.Add("MAR", 31);
                    //monthDict.Add("APR", 30);
                    //monthDict.Add("MAY", 31);
                    //monthDict.Add("JUN", 30);
                    //monthDict.Add("JUL", 31);
                    //monthDict.Add("AUG", 31);
                    //monthDict.Add("SEP", 30);
                    //monthDict.Add("OCT", 31);
                    //monthDict.Add("NOV", 30);
                    //monthDict.Add("DEC", 31);

                    //string bornMonth = string.Empty;
                    //Int32 bornDate = 0;

                    //Int32 leftval = DPBnum;
                    //foreach (var itm in monthDict)
                    //{
                    //    bornDate = leftval;

                    //    if (leftval <= itm.Value)
                    //    {
                    //        bornMonth = itm.Key;

                    //        break;
                    //    }
                    //    leftval = leftval - itm.Value;
                    //}

                    //Dictionary<string, Int32> monthDict2 = new Dictionary<string, int>();
                    //monthDict2.Add("JAN", 1);
                    //monthDict2.Add("FEF", 2);
                    //monthDict2.Add("MAR", 3);
                    //monthDict2.Add("APR", 4);
                    //monthDict2.Add("MAY", 5);
                    //monthDict2.Add("JUN", 6);
                    //monthDict2.Add("JUL", 7);
                    //monthDict2.Add("AUG", 8);
                    //monthDict2.Add("SEP", 9);
                    //monthDict2.Add("OCT", 10);
                    //monthDict2.Add("NOV", 11);
                    //monthDict2.Add("DEC", 12);
                    //Int32 dobMon = 0;
                    //foreach (var itm in monthDict2)
                    //{
                    //    if (itm.Key == bornMonth)
                    //    {
                    //        dobMon = itm.Value;
                    //    }
                    //}
                    //Int32 dobYear = 1900 + Convert.ToInt32((nicarray[0].ToString())) * 10 + Convert.ToInt32((nicarray[1].ToString()));

                    //DateTime dob = new DateTime(dobYear, dobMon, bornDate);
                    //if (dob.Date.ToString("dd-MMM-yyyy") == "")
                    //    return Json(new { success = true, login = true ,dob=String.Empty}, JsonRequestBehavior.AllowGet);
                    //else
                    //    return Json(new { success = true, login = true, dob = dob.Date.ToString("dd/MMM/yyyy") }, JsonRequestBehavior.AllowGet);


                    String nic_ = nic.Trim();
                    if (string.IsNullOrEmpty(nic_)) {
                        return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                    }
                    char[] nicarray = nic_.ToCharArray();

                    string thirdNum = "";
                    if (nic_.Length == 10)     
                        thirdNum = (nicarray[2]).ToString();
                    else if (nic_.Length == 12)
                        thirdNum = (nicarray[4]).ToString();
                    string sex = "";
                    string title = "";
                    if (thirdNum == "5" || thirdNum == "6" || thirdNum == "7" || thirdNum == "8" || thirdNum == "9")
                    {
                        sex = "FEMALE";
                        title = "Mrs.";
                    }
                    else
                    {
                        sex = "MALE";
                        title = "Mr.";
                    }


                    //---------DOB generation----------------------
                    string threechar = "";
                    if (nic_.Length == 10)
                        threechar = (nicarray[2]).ToString() + (nicarray[3]).ToString() + (nicarray[4]).ToString();
                    else if (nic_.Length == 12)
                        threechar = (nicarray[4]).ToString() + (nicarray[5]).ToString() + (nicarray[6]).ToString();

                    Int32 DPBnum = Convert.ToInt32(threechar);
                    if (DPBnum > 500)
                    {
                        DPBnum = DPBnum - 500;
                    }



                    // Int32 JAN = 1, FEF = 2, MAR = 3, APR = 4, MAY = 5, JUN = 6, JUL = 7, AUG = 8, SEP = 9, OCT = 10, NOV = 11, DEC = 12;


                    Dictionary<string, Int32> monthDict = new Dictionary<string, int>();
                    monthDict.Add("JAN", 31);
                    monthDict.Add("FEF", 29);
                    monthDict.Add("MAR", 31);
                    monthDict.Add("APR", 30);
                    monthDict.Add("MAY", 31);
                    monthDict.Add("JUN", 30);
                    monthDict.Add("JUL", 31);
                    monthDict.Add("AUG", 31);
                    monthDict.Add("SEP", 30);
                    monthDict.Add("OCT", 31);
                    monthDict.Add("NOV", 30);
                    monthDict.Add("DEC", 31);

                    string bornMonth = string.Empty;
                    Int32 bornDate = 0;

                    Int32 leftval = DPBnum;
                    foreach (var itm in monthDict)
                    {
                        bornDate = leftval;

                        if (leftval <= itm.Value)
                        {
                            bornMonth = itm.Key;

                            break;
                        }
                        leftval = leftval - itm.Value;
                    }

                    //-------------------------------
                    // string bornMonth1 = bornMonth;
                    // Int32 bornDate1 = bornDate;

                    Dictionary<string, Int32> monthDict2 = new Dictionary<string, int>();
                    monthDict2.Add("JAN", 1);
                    monthDict2.Add("FEF", 2);
                    monthDict2.Add("MAR", 3);
                    monthDict2.Add("APR", 4);
                    monthDict2.Add("MAY", 5);
                    monthDict2.Add("JUN", 6);
                    monthDict2.Add("JUL", 7);
                    monthDict2.Add("AUG", 8);
                    monthDict2.Add("SEP", 9);
                    monthDict2.Add("OCT", 10);
                    monthDict2.Add("NOV", 11);
                    monthDict2.Add("DEC", 12);
                    Int32 dobMon = 0;
                    foreach (var itm in monthDict2)
                    {
                        if (itm.Key == bornMonth)
                        {
                            dobMon = itm.Value;
                        }
                    }
                    Int32 dobYear = 0;
                    if (nic_.Length == 10)
                        dobYear = 1900 + Convert.ToInt32((nicarray[0].ToString())) * 10 + Convert.ToInt32((nicarray[1].ToString()));
                    else if (nic_.Length == 12)
                        dobYear = 1900 + Convert.ToInt32((nicarray[2].ToString())) * 10 + Convert.ToInt32((nicarray[3].ToString()));

                    try {
                        DateTime dob = new DateTime(dobYear, dobMon, bornDate);
                        dob = dob.Date;

                        if (dob.Date.ToString("dd-MMM-yyyy") == "")
                            return Json(new { success = true, login = true, dob = String.Empty }, JsonRequestBehavior.AllowGet);
                        else
                            return Json(new { success = true, login = true, dob = dob.Date.ToString("dd/MMM/yyyy"), sex = sex, title = title }, JsonRequestBehavior.AllowGet);
                    }catch(Exception ){
                        return Json(new { success = false, login = true,msg="Invalid NIC.",type="Info" }, JsonRequestBehavior.AllowGet);
                    }
                        
                }
                else { 
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError,type="Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}