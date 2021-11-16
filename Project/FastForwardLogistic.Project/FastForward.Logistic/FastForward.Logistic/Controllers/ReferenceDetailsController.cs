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
    public class ReferenceDetailsController : BaseController
    {
        // GET: ReferenceDetails
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

        public JsonResult LoadVessalDetails(string val)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_VESSEL> P_list = CHNLSVC.General.GetVessaldata(company, val);
                    if (P_list == null)
                    {
                        P_list = new List<MST_VESSEL>();
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

        public JsonResult LoadCostDetails(string val)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_VESSEL> P_list = CHNLSVC.General.GetCostdata(company, val);
                    if (P_list == null)
                    {
                        P_list = new List<MST_VESSEL>();
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

        public JsonResult LoadPortDetails(string val)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_VESSEL> P_list = CHNLSVC.General.GetPortdata(company, val);
                    if (P_list == null)
                    {
                        P_list = new List<MST_VESSEL>();
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

        public JsonResult vesselCreation(MST_VESSEL vsl)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if(vsl.VM_ACT==2)
                    {
                        return Json(new { success = false, login = true, msg = "Please Select an option", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                    string err = "0";
                    int check = CHNLSVC.General.Check_vessal(vsl.VM_VESSAL_CD);
                    if (check == 1)
                    {
                        return Json(new { success = false, login = true, msg = "Vessal number already exist", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (vsl.VM_VESSAL_CD == null)
                        {
                            return Json(new { success = false, login = true, msg = "Please Enter the Vessal number", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (vsl.VM_VESSAL_NAME == null)
                        {
                            return Json(new { success = false, login = true, msg = "Please enter the description", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        
                        //emp.MEMP_EPF = emp.MEMP_CD;
                        //emp.ESEP_COM_CD = company;
                        vsl.VM_CRE_BY = Convert.ToString(Session["UserID"]);
                        vsl.VM_CRE_DT = DateTime.Now;

                        DateTime serverDatetime = CHNLSVC.Security.GetServerDateTime();
                        int results = CHNLSVC.Sales.SaveVessalData(vsl, userId, userDefPro, serverDatetime, out err);

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
        public JsonResult vesselUpdate(MST_VESSEL vsl)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (vsl.VM_ACT == 2)
                    {
                        return Json(new { success = false, login = true, msg = "Please Select an option", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        string err = "0";
                        int check = CHNLSVC.General.Check_vessal(vsl.VM_VESSAL_CD);
                        if (check == 0)
                        {
                            return Json(new { success = false, login = true, msg = "No record found for the update", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (vsl.VM_VESSAL_CD == null)
                            {
                                return Json(new { success = false, login = true, msg = "Please Enter the Vessal number", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            if (vsl.VM_VESSAL_NAME == null)
                            {
                                return Json(new { success = false, login = true, msg = "Please enter the description", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }

                            //emp.MEMP_EPF = emp.MEMP_CD;
                            //emp.ESEP_COM_CD = company;
                            vsl.VM_MOD_BY = Convert.ToString(Session["UserID"]);
                            vsl.VM_MOD_DT = DateTime.Now;

                            DateTime serverDatetime = CHNLSVC.Security.GetServerDateTime();
                            int results = CHNLSVC.Sales.UpdateVessalData(vsl, userId, userDefPro, serverDatetime, out err);

                            if (results == 1)
                            {
                                return Json(new { success = true, login = true, msg = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = err, type = "Error" }, JsonRequestBehavior.AllowGet);
                            }
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

        public JsonResult CosteleCreation(MST_VESSEL vsl)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (vsl.MCE_ACT == 2)
                    {
                        return Json(new { success = false, login = true, msg = "Please Select an option", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (vsl.MCE_COST_REV_STS == "0")
                    {
                        return Json(new { success = false, login = true, msg = "Please Select an element type", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        string err = "0";
                        int check = CHNLSVC.General.Check_costele(vsl.MCE_CD);
                        if (check == 1)
                        {
                            return Json(new { success = false, login = true, msg = "Element number already exist", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (vsl.MCE_CD == null)
                            {
                                return Json(new { success = false, login = true, msg = "Please Enter the Vessal number", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            if (vsl.MCE_DESC == null)
                            {
                                return Json(new { success = false, login = true, msg = "Please enter the description", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }

                            //emp.MEMP_EPF = emp.MEMP_CD;
                            //emp.ESEP_COM_CD = company;
                            vsl.MCE_CRE_BY = Convert.ToString(Session["UserID"]);
                            vsl.MCE_CRE_DT = DateTime.Now;

                            DateTime serverDatetime = CHNLSVC.Security.GetServerDateTime();
                            int results = CHNLSVC.Sales.SaveCostEleData(vsl, userId, userDefPro, serverDatetime, out err);

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
        public JsonResult CosteleUpdate(MST_VESSEL vsl)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (vsl.MCE_ACT == 2)
                    {
                        return Json(new { success = false, login = true, msg = "Please Select an option", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        string err = "0";
                        int check = CHNLSVC.General.Check_costele(vsl.MCE_CD);
                        if (check == 0)
                        {
                            return Json(new { success = false, login = true, msg = "No record found for the update", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (vsl.MCE_CD == null)
                            {
                                return Json(new { success = false, login = true, msg = "Please Enter the Vessal number", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            if (vsl.MCE_DESC == null)
                            {
                                return Json(new { success = false, login = true, msg = "Please enter the description", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }

                            //emp.MEMP_EPF = emp.MEMP_CD;
                            //emp.ESEP_COM_CD = company;
                            vsl.MCE_MOD_BY = Convert.ToString(Session["UserID"]);
                            vsl.MCE_MOD_DT = DateTime.Now;

                            DateTime serverDatetime = CHNLSVC.Security.GetServerDateTime();
                            int results = CHNLSVC.Sales.UpdateCostEleData(vsl, userId, userDefPro, serverDatetime, out err);

                            if (results == 1)
                            {
                                return Json(new { success = true, login = true, msg = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = err, type = "Error" }, JsonRequestBehavior.AllowGet);
                            }
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

        public JsonResult PortCreation(MST_VESSEL vsl)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (vsl.PA_ACT == 2)
                    {
                        return Json(new { success = false, login = true, msg = "Please Select an option", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        string err = "0";
                        int check = CHNLSVC.General.Check_port(vsl.PA_PRT_CD);
                        if (check == 1)
                        {
                            return Json(new { success = false, login = true, msg = "Element number already exist", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (vsl.PA_PRT_CD == null)
                            {
                                return Json(new { success = false, login = true, msg = "Please Enter the Vessal number", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            if (vsl.PA_PRT_NAME == null)
                            {
                                return Json(new { success = false, login = true, msg = "Please enter the description", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }

                            //emp.MEMP_EPF = emp.MEMP_CD;
                            //emp.ESEP_COM_CD = company;
                            vsl.PA_CRE_BY = Convert.ToString(Session["UserID"]);
                            vsl.PA_CRE_DT = DateTime.Now;

                            DateTime serverDatetime = CHNLSVC.Security.GetServerDateTime();
                            int results = CHNLSVC.Sales.SavePortData(vsl, userId, userDefPro, serverDatetime, out err);

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
        public JsonResult PortUpdate(MST_VESSEL vsl)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (vsl.PA_ACT == 2)
                    {
                        return Json(new { success = false, login = true, msg = "Please Select an option", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        string err = "0";
                        int check = CHNLSVC.General.Check_port(vsl.PA_PRT_CD);
                        if (check == 0)
                        {
                            return Json(new { success = false, login = true, msg = "No record found for the update", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (vsl.PA_PRT_CD == null)
                            {
                                return Json(new { success = false, login = true, msg = "Please Enter the Vessal number", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            if (vsl.PA_PRT_NAME == null)
                            {
                                return Json(new { success = false, login = true, msg = "Please enter the description", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }

                            //emp.MEMP_EPF = emp.MEMP_CD;
                            //emp.ESEP_COM_CD = company;
                            vsl.PA_MOD_BY = Convert.ToString(Session["UserID"]);
                            vsl.PA_MOD_DT = DateTime.Now;

                            DateTime serverDatetime = CHNLSVC.Security.GetServerDateTime();
                            int results = CHNLSVC.Sales.UpdatePortData(vsl, userId, userDefPro, serverDatetime, out err);

                            if (results == 1)
                            {
                                return Json(new { success = true, login = true, msg = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = err, type = "Error" }, JsonRequestBehavior.AllowGet);
                            }
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
    }
}