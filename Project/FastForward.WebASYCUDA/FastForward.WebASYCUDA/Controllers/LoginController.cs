using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resources;
using FF.BusinessObjects;
using FastForward.WebASYCUDA.Services;
using System.Net;
using System.Web.UI.WebControls;
using System.Web.Security;
namespace FastForward.WebASYCUDA.Controllers
{
    public class LoginController : BaseController
    {
        private int _loginRetryInCounter = 0;
        private int _loginRetryOutCounter = 0;
        // GET: Show Login view action
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            if (string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(company))
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
            else {
                return Redirect("~/Home/index");
            }
        }
        //Get: check login details
        [HttpPost]
        public ActionResult Login(LoginUser loginUser,string returnUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(loginUser.User_name) || string.IsNullOrEmpty(loginUser.Password))
                {
                    ViewBag.ErrorMsg = Resource.InvalidLoginDet;
                    if (!string.IsNullOrEmpty(Request.Form["request"]) && Request.Form["request"] == "req")
                    {
                        return Json(new { success = false, msg = Resource.InvalidLoginDet });
                    }
                    else
                    {
                        return View(loginUser);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(loginUser.Mst_com.Mc_cd))
                    {

                        string msgv = Resource.NoCompanyAssignMsg;
                        ViewBag.ErrorMsg = msgv;
                        if (!string.IsNullOrEmpty(Request.Form["request"]) && Request.Form["request"] == "req")
                        {
                            return Json(new { success = false, msg = msgv });
                        }
                        else
                        {
                            return View(loginUser);
                        }
                        
                    }

                    string _msg = string.Empty;
                    string _msgTitle = string.Empty;
                    int _msgStatus = 0;
                    
                    string _WindowsLogonName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    string _WindowsUser = Environment.UserName;
                    loginUser.User_name = loginUser.User_name.ToUpper();
                    string GlbVersionNo = CHNLSVC.Security.GetCurrentVersion().ToString();
                    LoginUser _loginUser = CHNLSVC.Security.LoginToSystem(loginUser.User_name.ToUpper(), loginUser.Password.ToString(), loginUser.Mst_com.Mc_cd,GlbVersionNo, (string)Session["GlbUserIP"], (string)Session["GlbHostName"], _loginRetryInCounter, out _loginRetryOutCounter, out _msgStatus, out _msg, out _msgTitle);
                    if (_msgStatus == 1 || _msgStatus == -3)
                    {
                        Session["UserID"] = loginUser.User_name;
                        Session["UserName"] = _loginUser.User_name;
                        Session["UserCompanyCode"] = (!string.IsNullOrEmpty(loginUser.Mst_com.Mc_cd))?loginUser.Mst_com.Mc_cd.ToString():"";
                        Session["UserCompanyName"] = (!string.IsNullOrEmpty(loginUser.Mst_com.Mc_desc)) ? loginUser.Mst_com.Mc_desc.ToString() : "";
                        Session["UserIP"] = Request.UserHostAddress;
                        Session["UserComputer"] = Request.UserHostName;
                        Session["version"] = GlbVersionNo;

                        Session["GlbUserID"] = loginUser.User_name;
                        Session["GlbUserComCode"] = (!string.IsNullOrEmpty(loginUser.Mst_com.Mc_cd)) ? loginUser.Mst_com.Mc_cd.ToString() : "";
                        Session["GlbUserIP"] = GetIP();
                        Session["GlbHostName"] = Dns.GetHostName();
                        SetLoginCacheLayer(_loginUser, _WindowsLogonName, _WindowsUser);
                        FormsAuthentication.SetAuthCookie(loginUser.User_name, false);

                        if (!string.IsNullOrEmpty(Request.Form["request"]) && Request.Form["request"] == "req")
                        {
                            return Json(new { success = true });
                        }
                        else {
                            return Redirect("~/Home/index");
                        }
                        

                    }
                    else if (_msgStatus == -1)
                    {
                        if (_loginUser.Login_attempts >= _loginRetryOutCounter)
                        {
                            _loginRetryInCounter = _loginRetryOutCounter;
                            ViewBag.ErrorMsg = _msg;
                            if (!string.IsNullOrEmpty(Request.Form["request"]) && Request.Form["request"] == "req")
                            {
                                return Json(new {success= false, msg = _msg});
                            
                            }else
                            {
                                return View(_loginUser);
                            }
                        }
                        else
                        {
                            CHNLSVC.Security.Save_User_Falis(loginUser.User_name.ToString(), loginUser.Password.ToString(), loginUser.Mst_com.Mc_cd, (string)Session["GlbUserIP"], _WindowsLogonName, _WindowsUser);
                            _msg = Resource.FailDetailRemember;
                            ViewBag.ErrorMsg = _msg;
                            if (!string.IsNullOrEmpty(Request.Form["request"]) && Request.Form["request"] == "req")
                            {
                                return Json(new { success = false, msg = _msg });
                            }
                            else
                            {
                                return View(_loginUser);
                            }
                        }
                    }
                    else if (_msgStatus == -2)
                    {
                        _loginRetryInCounter = _loginRetryOutCounter;
                        ViewBag.ErrorMsg = _msg;
                        if (!string.IsNullOrEmpty(Request.Form["request"]) && Request.Form["request"] == "req")
                        {
                            return Json(new { success = false, msg = _msg });
                        }
                        else
                        {
                            return View(_loginUser);
                        }
                    }
                    else if (_msgStatus == -99)
                    {
                        ViewBag.ErrorMsg = _msg;
                        if (!string.IsNullOrEmpty(Request.Form["request"]) && Request.Form["request"] == "req")
                        {
                            return Json(new { success = false, msg = _msg });
                        }
                        else
                        {
                            return View(_loginUser);
                        }
                    }
                    else if (_msgStatus == -4)
                    {
                        ViewBag.ErrorMsg = _msg;
                        if (!string.IsNullOrEmpty(Request.Form["request"]) && Request.Form["request"] == "req")
                        {
                            return Json(new { success = false, msg = _msg });
                        }
                        else
                        {
                            return View(_loginUser);
                        }
                    }
                    return View(loginUser);
                }
            }
            catch (Exception ex) {
                ViewBag.ErrorMsg = Resource.ServerError;
                if (!string.IsNullOrEmpty(Request.Form["request"]) && Request.Form["request"] == "req")
                {
                    return Json(new { success = false, msg = Resource.ServerError });
                }
                else
                {
                    return View();
                }
               
            }
        }
        //get machine ip address
        public string GetIP()
        {
            IPHostEntry host;
            string localIP = "";
            Session["GlbHostName"] = Dns.GetHostName();
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
        //get company list for login company drop down list
        public JsonResult GetCompanyList(string username)
        {
            List<SystemUserCompany> _systemUserCompanyList = new List<SystemUserCompany>();
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    _systemUserCompanyList = CHNLSVC.Security.GetUserCompany(username.ToUpper());
                    if (_systemUserCompanyList != null)
                    {
                        return Json(new { success = true, data = _systemUserCompanyList, msg = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else {
                        return Json(new { success = false, data = _systemUserCompanyList, msg = Resource.NoCompanyAssignMsg }, JsonRequestBehavior.AllowGet);
                    }

                }
                else {
                    return Json(new { success = false, data = _systemUserCompanyList, msg = Resource.InvalidLoginDet }, JsonRequestBehavior.AllowGet); 
                }
                
            }
            catch(Exception ex) 
            {
                return Json(new { success = false, data = _systemUserCompanyList, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
            }
        }
        //logoff action
        public ActionResult LogOff()
        {
            try
            {
                if ((string)Session["GlbIsExit"] == "false")
                {
                    CHNLSVC.Security.ExitLoginSession((string)Session["GlbUserID"],(string) Session["GlbUserComCode"],(string) Session["GlbUserSessionID"]);
                    Session["GlbIsExit"] = "true";
                }
                Session.RemoveAll();
                Session.Clear();
                if (!string.IsNullOrEmpty(Request.Form["request"]) && Request.Form["request"] == "req")
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return RedirectToAction("Login", "Login");
                }
               
            }
            catch (Exception err)
            {
                if (!string.IsNullOrEmpty(Request.Form["request"]) && Request.Form["request"] == "req")
                {
                    return Json(new { success = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return RedirectToAction(Request.RawUrl);
                }
            }

        }
    }
}