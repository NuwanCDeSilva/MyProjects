using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FF.BusinessObjects.Security;
using FF.BusinessObjects.Search;
using FF.BusinessObjects;
using FF.BusinessObjects.Genaral;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using FF.BusinessObjects;
using FF.BusinessObjects.Sales;



namespace FastForward.Logistic.Controllers
{
    public class EmployeeDetailsController : BaseController
    {
        // GET: EmployeeDetails
        public ActionResult Index(string returnUrl)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                SEC_SYSTEM_MENU per = CHNLSVC.Security.getUserPermission(userId, company, 13);
                if (per.SSM_ID != 0)
                {
                    // ViewBag.ReturnUrl = returnUrl;
                    return View();
                }
                else
                {
                    return Redirect("/Home/Error");
                }
            }
            else
            {
                return Redirect("~/Home/index");
            }
        }

        public JsonResult gettitleList()
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_TITLE> titleList = CHNLSVC.General.GetTitleList();
                    return Json(new { success = true, login = true, data = titleList }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = "error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult LoadEmployeeDetails(string val)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_EMP> P_list = CHNLSVC.General.GetEmployeedata(company, val);
                    if (P_list == null)
                    {
                        P_list = new List<MST_EMP>();
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


        public JsonResult getEmployeeTypeList()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<Mst_empcate> mst_empcate = CHNLSVC.General.Get_mst_empcate();
                    return Json(new { success = true, login = true, data = mst_empcate }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        //public JsonResult getEmployeeDetails(string empCd)
        //{
        //    string userId = HttpContext.Session["UserID"] as string;
        //    string company = HttpContext.Session["UserCompanyCode"] as string;
        //    string userDefPro = HttpContext.Session["UserDefProf"] as string;
        //    string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //        {
        //            empCd = empCd.Trim();
        //            MST_EMP EMP = CHNLSVC.Sales.getEmployeeDetails(empCd, company);
        //            //MST_EMPLOYEES employees = CHNLSVC.General.getMstEmployeeDetails(val);
        //            if (EMP == null)
        //            {
        //                return Json(new { success = true, login = true, data = "" }, JsonRequestBehavior.AllowGet);
        //            }
        //            List<MST_PCEMP> list = new List<MST_PCEMP>();
        //            list = CHNLSVC.Tours.Get_mst_pcemp(val);
        //            Session["profitCenters"] = list;

        //            if (list.Count > 0)
        //            {
        //                employees.profitCenterLst = list;
        //            }
        //            else
        //            {
        //                employees.profitCenterLst = null;
        //            }


        //            List<mst_fleet_driver> vehiAllocations = CHNLSVC.Tours.getAllocateVehicles(val);
        //            if (vehiAllocations.Count > 0)
        //            {
        //                employees.mstFleetDriver = vehiAllocations;
        //                Session["vahicleAllowcation"] = vehiAllocations;
        //            }
        //            else
        //            {

        //                Session["vahicleAllowcation"] = null;
        //            }
        //            if (employees != null)
        //            {
        //                return Json(new { success = true, login = true, data = employees }, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                return Json(new { success = true, login = true, data = "" }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        else
        //        {
        //            return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //        {
        //            return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //}

        //public JsonResult getEmployeeDetailsByNic(string Nic)
        //{
        //    string userId = HttpContext.Session["UserID"] as string;
        //    string company = HttpContext.Session["UserCompanyCode"] as string;
        //    string userDefPro = HttpContext.Session["UserDefProf"] as string;
        //    string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //        {
        //            Nic = Nic.Trim();
        //            MST_EMPLOYEES employees = CHNLSVC.Tours.getMstEmployeeDetailsByNic(Nic);
        //            if (employees != null)
        //            {
        //                List<MST_PCEMP> list = new List<MST_PCEMP>();
        //                list = CHNLSVC.Tours.Get_mst_pcemp(employees.MEMP_CD);
        //                Session["profitCenters"] = list;

        //                if (list.Count > 0)
        //                {
        //                    employees.profitCenterLst = list;
        //                }
        //                else
        //                {
        //                    employees.profitCenterLst = null;
        //                }


        //                List<mst_fleet_driver> vehiAllocations = CHNLSVC.Tours.getAllocateVehicles(employees.MEMP_CD);
        //                if (vehiAllocations.Count > 0)
        //                {
        //                    employees.mstFleetDriver = vehiAllocations;
        //                    Session["vahicleAllowcation"] = vehiAllocations;
        //                }
        //                else
        //                {

        //                    Session["vahicleAllowcation"] = null;
        //                }
        //                if (employees != null)
        //                {
        //                    return Json(new { success = true, login = true, data = employees }, JsonRequestBehavior.AllowGet);
        //                }
        //                else
        //                {
        //                    return Json(new { success = true, login = true, data = "" }, JsonRequestBehavior.AllowGet);
        //                }
        //            }
        //            return Json(new { success = true, login = true, data = "" }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //        {
        //            return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //}

        public JsonResult employeeCreation(MST_EMP emp)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string err = "0";
                    int check = CHNLSVC.General.Check_Employeeepf(emp.ESEP_EPF);
                    if (check == 1)
                    {
                        return Json(new { success = false, login = true, msg = "EPF number already exist", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (emp.ESEP_EPF == null)
                        {
                            return Json(new { success = false, login = true, msg = "Please Enter the EPF number", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (emp.ESEP_NIC == null)
                        {
                            return Json(new { success = false, login = true, msg = "Please enter the NIC number", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (emp.ESEP_FIRST_NAME == null)
                        {
                            return Json(new { success = false, login = true, msg = "Please enter the first name", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (emp.ESEP_LAST_NAME == null)
                        {
                            return Json(new { success = false, login = true, msg = "Please enter the last name", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (emp.ESEP_LIVING_ADD_1 == null)
                        {
                            return Json(new { success = false, login = true, msg = "Please enter the address", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (Convert.ToDateTime(emp.ESEP_DOJ)>DateTime.Now)
                        {
                            return Json(new { success = false, login = true, msg = "Date of join can’t be a future date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        //emp.MEMP_EPF = emp.MEMP_CD;
                        emp.ESEP_COM_CD = company;
                        emp.ESEP_CRE_BY = Convert.ToString(Session["UserID"]);
                        emp.ESEP_CRE_DT = DateTime.Now;

                        if (emp.ESEP_DOB.ToString() != "")
                            emp.ESEP_DOB = Convert.ToDateTime(emp.ESEP_DOB);
                        if (emp.ESEP_DOJ.ToString() != "")
                            emp.ESEP_DOJ = Convert.ToDateTime(emp.ESEP_DOJ);
                        //List<mst_fleet_driver> vehicleList = new List<mst_fleet_driver>();
                        if (emp.ESEP_CAT_CD == "DRIVER")
                        {
                            if (Convert.ToDateTime(emp.ESEP_DL_ISSDT) >= Convert.ToDateTime(emp.ESEP_DL_EXPDT))
                            {
                                return Json(new { success = false, login = true, msg = "License issue date can’t be the equal or greater than to expire date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            emp.ESEP_DL_NO = emp.ESEP_DL_NO;
                            emp.ESEP_DL_CLASS = emp.ESEP_DL_CLASS;
                            if (emp.ESEP_DL_EXPDT.ToString() != "")
                                emp.ESEP_DL_EXPDT = Convert.ToDateTime(emp.ESEP_DL_EXPDT);

                            if (emp.ESEP_DL_ISSDT.ToString() != "")
                                emp.ESEP_DL_ISSDT = Convert.ToDateTime(emp.ESEP_DL_ISSDT);

                            //vehicleList = Session["vahicleAllowcation"] as List<mst_fleet_driver>;
                            //if (vehicleList != null)
                            //{
                            //    foreach (mst_fleet_driver vehicle in vehicleList)
                            //    {
                            //        if (CheckOverLapDates(vehicle.MFD_FRM_DT.ToString(), vehicle.MFD_TO_DT.ToString(), vehicle.MFD_VEH_NO, vehicle.MFD_DRI, vehicle.MFD_SEQ.ToString()))
                            //        {
                            //            return Json(new { success = false, login = true, msg = "Either " + vehicle.MFD_VEH_NO + " vehicle or driver " + vehicle.MFD_DRI + " has another active allocation with in this period", type = "Info" }, JsonRequestBehavior.AllowGet);

                            //        }

                            //    }

                            //}
                        }
                        else
                        {
                            if (emp.ESEP_DL_NO != null)
                            {
                                return Json(new { success = false, login = true, msg = "Can't enter Driving Licence No for this emp. type", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            if (emp.ESEP_DL_CLASS != null)
                            {
                                return Json(new { success = false, login = true, msg = "Can't enter Driver Class for this emp. type", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            emp.ESEP_DL_NO = "Empty";
                            emp.ESEP_DL_CLASS = "Empty";
                        }
                        //List<MST_PCEMP> list = new List<MST_PCEMP>();
                        //if (Session["profitCenters"] != null)
                        //{
                        //    List<MST_PCEMP> profCen = Session["profitCenters"] as List<MST_PCEMP>;
                        //    foreach (MST_PCEMP prof in profCen)
                        //    {
                        //        MST_PCEMP mst_pcemp = new MST_PCEMP();
                        //        mst_pcemp.MPE_EPF = emp.MEMP_CD;
                        //        mst_pcemp.MPE_COM = company;
                        //        mst_pcemp.MPE_PC = prof.MPE_PC;
                        //        mst_pcemp.MPE_ASSN_DT = DateTime.Now;
                        //        mst_pcemp.MPE_ACT = Convert.ToInt32(prof.MPE_ACT);

                        //        list.Add(mst_pcemp);
                        //    }
                        //    //list = (List<MST_PCEMP>)Session["ProfitCenter"];
                        //}
                        ////save languages
                        //List<MST_LANGUAGE> langlist = (List<MST_LANGUAGE>)Session["languages"];
                        //string lan = "";
                        //if (langlist != null)
                        //{
                        //    foreach (var lancode in langlist)
                        //    {
                        //        lan = lan + lancode.mla_cd.ToString() + ",";
                        //    }
                        //}
                        //emp.MEMP_ANAL3 = lan;
                        //DataTable dtdrivercom = CHNLSVC.Tours.SP_TOURS_GET_DRIVER_COM(emp.MEMP_EPF);
                        //if (dtdrivercom.Rows.Count > 0)
                        //{
                        //    foreach (DataRow item in dtdrivercom.Rows)
                        //    {
                        //        Session["DRIVERCOM"] = item[0].ToString();
                        //    }
                        //}
                        string driveCom = Session["DRIVERCOM"] as string;
                        DateTime serverDatetime = CHNLSVC.Security.GetServerDateTime();
                        int results = CHNLSVC.Sales.SaveEmployeeData(emp, userId, userDefPro, serverDatetime, driveCom, out err);

                        if (results == 1)
                        {
                            return Json(new { success = true, login = true, msg = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = err, type = "Error" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = "Error", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult employeeUpdate(MST_EMP emp)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    int check = CHNLSVC.General.Check_Employeeepf(emp.ESEP_EPF);
                    if (check == 0)
                    {
                        return Json(new { success = false, login = true, msg = "There has no record for update.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (emp.ESEP_EPF == null)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter the employee No", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (emp.ESEP_NIC == null)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter the NIC No", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (emp.ESEP_FIRST_NAME == null)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter the first name No", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (emp.ESEP_LAST_NAME == null)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter the last name No", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (emp.ESEP_LIVING_ADD_1 == null)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter the address No", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToDateTime(emp.ESEP_DOJ) > DateTime.Now)
                    {
                        return Json(new { success = false, login = true, msg = "Date of join can’t be a future date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    string err = "0";
                    //emp.MEMP_EPF = emp.MEMP_CD;
                    emp.ESEP_COM_CD = company;
                    emp.ESEP_CRE_BY = Convert.ToString(Session["UserID"]);
                    emp.ESEP_CRE_DT = DateTime.Now;

                    if (emp.ESEP_DOB.ToString() != "")
                        emp.ESEP_DOB = Convert.ToDateTime(emp.ESEP_DOB);
                    if (emp.ESEP_DOJ.ToString() != "")
                        emp.ESEP_DOJ = Convert.ToDateTime(emp.ESEP_DOJ);
                    //List<mst_fleet_driver> vehicleList = new List<mst_fleet_driver>();
                    if (emp.ESEP_CAT_CD == "DRIVER")
                    {
                        if (Convert.ToDateTime(emp.ESEP_DL_ISSDT) >= Convert.ToDateTime(emp.ESEP_DL_EXPDT))
                        {
                            return Json(new { success = false, login = true, msg = "License issue date can’t be the equal or greater than to expire date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        emp.ESEP_DL_NO = emp.ESEP_DL_NO;
                        emp.ESEP_DL_CLASS = emp.ESEP_DL_CLASS;
                        if (emp.ESEP_DL_EXPDT.ToString() != "")
                            emp.ESEP_DL_EXPDT = Convert.ToDateTime(emp.ESEP_DL_EXPDT);

                        if (emp.ESEP_DL_ISSDT.ToString() != "")
                            emp.ESEP_DL_ISSDT = Convert.ToDateTime(emp.ESEP_DL_ISSDT);
                        //vehicleList = Session["vahicleAllowcation"] as List<mst_fleet_driver>;
                        //if (vehicleList != null)
                        //{
                        //    foreach (mst_fleet_driver vehicle in vehicleList)
                        //    {
                        //        if (CheckOverLapDates(vehicle.MFD_FRM_DT.ToString(), vehicle.MFD_TO_DT.ToString(), vehicle.MFD_VEH_NO, vehicle.MFD_DRI, vehicle.MFD_SEQ.ToString()))
                        //        {
                        //            return Json(new { success = false, login = true, msg = "Either " + vehicle.MFD_VEH_NO + " vehicle or driver " + vehicle.MFD_DRI + " has another active allocation with in this period", type = "Info" }, JsonRequestBehavior.AllowGet);

                        //        }

                        //    }

                        //}
                    }
                    else
                    {
                        if (emp.ESEP_DL_NO != null)
                        {
                            return Json(new { success = false, login = true, msg = "Can't enter Driving Licence No for this emp. type", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (emp.ESEP_DL_CLASS != null)
                        {
                            return Json(new { success = false, login = true, msg = "Can't enter Driver Class for this emp. type", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        emp.ESEP_DL_NO = "Empty";
                        emp.ESEP_DL_CLASS = "Empty";
                        //DateTime? dt = null;
                        //dt = new DateTime();
                        //emp.ESEP_DL_EXPDT = dt;
                        //emp.ESEP_DL_ISSDT = dt;

                    }
                    //List<MST_PCEMP> list = new List<MST_PCEMP>();
                    //if (Session["profitCenters"] != null)
                    //{
                    //    List<MST_PCEMP> profCen = Session["profitCenters"] as List<MST_PCEMP>;
                    //    foreach (MST_PCEMP prof in profCen)
                    //    {
                    //        MST_PCEMP mst_pcemp = new MST_PCEMP();
                    //        mst_pcemp.MPE_EPF = emp.MEMP_CD;
                    //        mst_pcemp.MPE_COM = company;
                    //        mst_pcemp.MPE_PC = prof.MPE_PC;
                    //        mst_pcemp.MPE_ASSN_DT = DateTime.Now;
                    //        mst_pcemp.MPE_ACT = Convert.ToInt32(prof.MPE_ACT);

                    //        list.Add(mst_pcemp);
                    //    }
                    //    //list = (List<MST_PCEMP>)Session["ProfitCenter"];

                    //}
                    //DataTable dtdrivercom = CHNLSVC.Tours.SP_TOURS_GET_DRIVER_COM(emp.MEMP_EPF);
                    //if (dtdrivercom.Rows.Count > 0)
                    //{
                    //    foreach (DataRow item in dtdrivercom.Rows)
                    //    {
                    //        Session["DRIVERCOM"] = item[0].ToString();
                    //    }
                    //}
                    string driveCom = Session["DRIVERCOM"] as string;
                    DateTime serverDatetime = CHNLSVC.Security.GetServerDateTime();
                    //save languages
                    //List<MST_LANGUAGE> langlist = (List<MST_LANGUAGE>)Session["languages"];
                    //string lan = "";
                    //if (langlist != null)
                    //{
                    //    foreach (var lancode in langlist)
                    //    {
                    //        lan = lan + lancode.mla_cd.ToString() + ",";
                    //    }
                    //}
                    //emp.MEMP_ANAL3 = lan;
                    int results = CHNLSVC.Sales.UpdateEmployeeData(emp, userId, userDefPro, serverDatetime, driveCom, out err);

                    if (results > 0)
                    {
                        return Json(new { success = true, login = true, msg = "Successfully Updated" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = err, type = "Error" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = "Error", type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        //public JsonResult LoadDriverTypes()
        //{
        //    try
        //    {
        //        string userId = HttpContext.Session["UserID"] as string;
        //        string company = HttpContext.Session["UserCompanyCode"] as string;
        //        string userDefPro = HttpContext.Session["UserDefProf"] as string;
        //        string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
        //        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //        {
        //            List<ComboBoxObject> oList = new List<ComboBoxObject>();
        //            ComboBoxObject o1 = new ComboBoxObject();
        //            o1.Text = "Company drivers";
        //            o1.Value = "Company drivers";
        //            oList.Add(o1);

        //            ComboBoxObject o2 = new ComboBoxObject();
        //            o2.Text = "Leased car drivers";
        //            o2.Value = "Company drivers";
        //            oList.Add(o2);
        //            return Json(new { success = true, login = true, data = oList }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, login = true, data = Resource.ServerError }, JsonRequestBehavior.AllowGet);
        //    }
        //}

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
                    if (string.IsNullOrEmpty(nic_))
                    {
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

                    try
                    {
                        DateTime dob = new DateTime(dobYear, dobMon, bornDate);
                        dob = dob.Date;

                        if (dob.Date.ToString("dd-MMM-yyyy") == "")
                            return Json(new { success = true, login = true, dob = String.Empty }, JsonRequestBehavior.AllowGet);
                        else
                            return Json(new { success = true, login = true, dob = dob.Date.ToString("dd/MMM/yyyy"), sex = sex, title = title }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception)
                    {
                        return Json(new { success = false, login = true, msg = "Invalid NIC.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = "Invalid NIC.", type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }


    }
}