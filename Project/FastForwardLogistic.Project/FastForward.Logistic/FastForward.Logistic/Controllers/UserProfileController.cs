using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FF.BusinessObjects;
using FF.BusinessObjects.Genaral;
using System.Text.RegularExpressions;
using FF.BusinessObjects.Search;
using FF.BusinessObjects.Sales;
using FF.BusinessObjects.Security;

namespace FastForward.Logistic.Controllers
{
    public class UserProfileController : BaseController
    {
        // GET: UserProfile
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string userDefChnl = HttpContext.Session["UserDefChnl"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }

        [HttpPost]
        public JsonResult Save_User_Details(FormCollection data, string choice, string ispassword)
        {

            string p_Se_usr_id = data["user_id"].ToString();
            string p_Se_usr_desc = data["description"].ToString();
            string p_Se_usr_name = data["full_name"].ToString();
            string p_Se_usr_pw = data["password"].ToString();
            string p_Se_usr_cat = data["designation_id"].ToString();
            string p_Se_dept_id = data["department_id"].ToString();
            string p_Se_emp_id = data["employee_id"].ToString();
            string p_Se_emp_cd = data["emp_code"].ToString();
            string p_Se_isdomain = data["IsDomain"].ToString();
            string p_Se_iswinauthend = data["IsWindowsAuth"].ToString();
            string p_Se_SUN_ID = data["sun_user_id"].ToString();
            string p_se_Email = data["email"].ToString();
            string p_se_Mob = data["mobile_no"].ToString();
            string p_se_Phone = data["phone_no"].ToString();
            string p_Se_act = data["status"].ToString();
            string p_Se_ischange_pw = data["IsAllowToChangePassword"].ToString();
            string p_Se_pw_mustchange = data["IsUserMustChangeNxtLog"].ToString();
            string p_se_act_rmk = data["se_act_rmk"].ToString();

            if (p_Se_isdomain == "true,false")
            {
                p_Se_isdomain = "1";
            }
            else {
                p_Se_isdomain = "0";
            }


            if (p_Se_iswinauthend == "true,false")
            {
                p_Se_iswinauthend = "1";
            }
            else
            {
                p_Se_iswinauthend = "0";
            }


            if (p_Se_ischange_pw == "true,false")
            {
                p_Se_ischange_pw = "1";
            }
            else
            {
                p_Se_ischange_pw = "0";
            }


            if (p_Se_pw_mustchange == "true,false")
            {
                p_Se_pw_mustchange = "1";
            }
            else
            {
                p_Se_pw_mustchange = "0";
            }


            if (p_Se_act == "Inactive") {
                p_Se_act = "0";
            }
            else if (p_Se_act == "Locked") {
                p_Se_act = "-1";
            }
            else if (p_Se_act == "PermanentlyDisable") {
                p_Se_act = "-2";
            }
            else {
                p_Se_act = "1";
            }

            string userId = HttpContext.Session["UserId"] as string;
            string companyId = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string err = "0";
                    int documents = CHNLSVC.Security.Save_User_Details(userId, p_Se_usr_id, p_Se_usr_desc, p_Se_usr_name, p_Se_usr_pw, p_Se_usr_cat, p_Se_dept_id, p_Se_emp_id, p_Se_emp_cd,
                             p_Se_isdomain, p_Se_iswinauthend, p_Se_SUN_ID, p_se_Email, p_se_Mob, p_se_Phone, p_Se_act, p_Se_ischange_pw, p_Se_pw_mustchange,
                              choice, ispassword, p_se_act_rmk, out err);

                    if (documents != 0)
                    {
                        return Json(new { success = true, login = true, data = documents }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No data found for this search criteria.", type = "Error" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult getUserListCom(string userID)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    List<USER_COMPANY_LIST> documents = CHNLSVC.General.getUserListCom(userID);

                    if (documents != null)
                    {
                        return Json(new { success = true, login = true, data = documents }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No data found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }


        public JsonResult Delete_User_Company(List<string> ComUserlist)
        {
            List<string> UserList = new List<string>();
            List<string> CompanyList = new List<string>();
            string[] div = new string[2];
            for (int i = 0; i < ComUserlist.Count; i++)
            {
                div = ComUserlist[i].Split('%');
                CompanyList.Insert(i, div[0].ToString());
                UserList.Insert(i, div[1].ToString());
            }

            string userId = HttpContext.Session["UserId"] as string;
            string companyId = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string err = "0";

                    int documents = CHNLSVC.Security.Remove_User_Company(CompanyList, UserList, out err);

                    if (documents != 0)
                    {
                        return Json(new { success = true, login = true, data = documents }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No data found for this search criteria.", type = "Error" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult Update_User_Company(string p_company, string p_userid, string p_isactive, string p_isdefault)
        {

            string userId = HttpContext.Session["UserId"] as string;
            string companyId = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string p_session = HttpContext.Session["Session"] as string;

            if (p_isactive == "true")
            {
                p_isactive = "1";
            }
            else { p_isactive = "0"; }

            if (p_isdefault == "true")
            {
                p_isdefault = "1";
            }
            else { p_isdefault = "0"; }

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string err = "0";

                    int documents = CHNLSVC.Security.Update_User_Company(p_company, p_userid, p_isactive, p_isdefault, userId,p_session, out err);

                    if (documents != 0)
                    {
                        return Json(new { success = true, login = true, data = documents }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No data found for this search criteria.", type = "Error" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

    }
}