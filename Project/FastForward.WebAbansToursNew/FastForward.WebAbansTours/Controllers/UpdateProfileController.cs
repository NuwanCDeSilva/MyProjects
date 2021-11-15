using FF.BusinessObjects;
using FF.BusinessObjects.ToursNew;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FastForward.WebAbansTours.Controllers
{
    public class UpdateProfileController : BaseController
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
                ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1034);
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
        // GET: UpdateProfile
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
        public JsonResult getUserDetails()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    SystemUser _systemUser = null;
                    _systemUser = CHNLSVC.Security.GetUserByUserID(userId);
                    return Json(new { success = true, login = true, usr = _systemUser }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult updatePassword(string Se_usr_id, string Se_usr_pw, string confirmPassword, string newPassword)
        {
            try
            {
                string curpw = Se_usr_pw.Trim();
                string newpw = newPassword.Trim();
                string confpw = confirmPassword.Trim();

                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    SystemUser _systemuser = CHNLSVC.Security.GetUserByUserID(userId);
                    if (_systemuser.Se_usr_id == null)
                    {
                        return Json(new { success = false, login = true,msg="Invalid user id.",type="Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (_systemuser.Se_act == 0)
                    {
                        return Json(new { success = false, login = true, msg = "Inactive user.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    if (string.IsNullOrEmpty(curpw))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter the current password.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    if (string.IsNullOrEmpty(newpw))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter a new password.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(confpw))
                    {
                        return Json(new { success = false, login = true, msg = "Please confirm new password.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (newpw != confpw)
                    {
                        return Json(new { success = false, login = true, msg = "The passwords you typed do not match. Please try again.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (newpw == curpw)
                    {
                        return Json(new { success = false, login = true, msg = "Your new password was rejected because it was similar to your current password.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (_systemuser.Se_usr_id.ToUpper().ToString() == newpw.ToUpper().ToString() )
                    {
                        return Json(new { success = false, login = true, msg = "Password cannot be user id.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    Int32 row_aff = 0;
                    string _msg = string.Empty;

                    SystemUser _SystemUser = new SystemUser();
                    _SystemUser = CHNLSVC.Security.GetUserByUserID(userId);
                    _SystemUser.Se_usr_pw = newpw;
                    if (_SystemUser.Se_pw_mustchange == 1)
                    {
                        _SystemUser.Se_pw_mustchange = 0;
                    }

                    _SystemUser.Se_pw_chng_by = Session["UserID"].ToString();

                    row_aff = CHNLSVC.Security.Change_Password(_SystemUser);
                    if (row_aff > 0)
                    {
                        return Json(new { success = false, login = true, msg = "Your password successfully changed.", type = "Info" }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Unable to change password.", type = "Info" }, JsonRequestBehavior.AllowGet);

                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}