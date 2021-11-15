using FastForward.WebAbansTours.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.ToursNew;
using FF.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FastForward.WebAbansTours.Controllers
{
    public class LoginController : BaseController
    {
        private int _loginRetryInCounter = 0;
        private int _loginRetryOutCounter = 0;
        // GET: Login
        [HttpGet]
        public ActionResult Index(string returnUrl)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(company) && string.IsNullOrEmpty(userDefPro) &&string.IsNullOrEmpty(userDefLoc))
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
            else
            {
                return Redirect("~/Home/index");
            }
        }

        //Get: check login details
        [HttpPost]
        public ActionResult Login(LoginUser loginUser, string returnUrl)
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
                   string GlbVersionNo = CHNLSVC.Security.GetCurrentVersion().ToString();
                    LoginUser _loginUser = CHNLSVC.Security.LoginToSystem(loginUser.User_name.ToUpper(), loginUser.Password.ToString(), loginUser.Mst_com.Mc_cd, GlbVersionNo,(string)Session["GlbUserIP"], (string)Session["GlbHostName"], _loginRetryInCounter, out _loginRetryOutCounter, out _msgStatus, out _msg, out _msgTitle);
                    if (_msgStatus == 1 || _msgStatus == -3)
                    {
                        Session["UserID"] = loginUser.User_name.ToUpper();
                        Session["UserName"] = _loginUser.User_name;
                        Session["UserCompanyCode"] = (!string.IsNullOrEmpty(loginUser.Mst_com.Mc_cd)) ? loginUser.Mst_com.Mc_cd.ToString() : "";
                        Session["UserCompanyName"] = (!string.IsNullOrEmpty(loginUser.Mst_com.Mc_desc)) ? loginUser.Mst_com.Mc_desc.ToString() : "";
                        Session["UserIP"] = Request.UserHostAddress;
                        Session["UserComputer"] = Request.UserHostName;
                        
                        Session["version"] = GlbVersionNo;
                        Session["LoginDateTime"] = DateTime.Now.ToString();
                        

                        Session["GlbUserID"] = loginUser.User_name;
                        Session["GlbUserComCode"] = (!string.IsNullOrEmpty(loginUser.Mst_com.Mc_cd)) ? loginUser.Mst_com.Mc_cd.ToString() : "";
                        Session["GlbUserIP"] = GetIP();
                        Session["GlbHostName"] = Dns.GetHostName();
                        SetLoginCacheLayer(_loginUser, _WindowsLogonName, _WindowsUser);
                        FormsAuthentication.SetAuthCookie(loginUser.User_name, false);

                        string menu = genarateUserMenu(loginUser.User_name.ToUpper());
                        Session["Menu"] = menu;
                        if (!string.IsNullOrEmpty(Request.Form["request"]) && Request.Form["request"] == "req")
                        {
                            ///added for check compny and set sesstion
                            Session["SessionID"] = CHNLSVC.Security.SaveLoginSession(loginUser.User_name, loginUser.Mst_com.Mc_cd.ToString(), Request.UserHostAddress, Request.UserHostName, "", "");


                            List<SystemUserProf> _userprofs = CHNLSVC.Security.GetUserProfCenters(loginUser.User_name.ToUpper(), loginUser.Mst_com.Mc_cd.ToString());
                            if (_userprofs != null)
                            {
                                var _userprofQuery =
                                from userProf in _userprofs
                                where userProf.Sup_usr_id == loginUser.User_name.ToUpper() && userProf.Sup_com_cd == loginUser.Mst_com.Mc_cd.ToString() && userProf.Sup_def_pccd == true
                                select userProf;

                                foreach (var _userprof in _userprofQuery)
                                {
                                    Session["UserDefProf"] = _userprof.Sup_pc_cd;
                                }
                            }

                            if (Session["UserDefProf"] == null)
                            {
                                Session["UserID"] = "";
                                Session["UserName"] = "";
                                Session["UserCompanyCode"] = "";
                                Session["UserCompanyName"] = "";
                                Session["UserIP"] = "";
                                Session["UserComputer"] = "";
                                Session["version"] = "";
                                return Json(new { success = false, msg = Resource.txtSetupProfCentr });

                            }
                            string  userDefPro = HttpContext.Session["UserDefProf"] as string;
                            string com = HttpContext.Session["UserCompanyCode"] as string;
                            MasterProfitCenter proCenDet = CHNLSVC.Sales.GetProfitCenter(com, userDefPro);
                            Session["Title"] = proCenDet.Mpc_oth_ref;
                            List<SystemUserLoc> _userLocas = CHNLSVC.Security.GetUserLoc(loginUser.User_name.ToUpper(), loginUser.Mst_com.Mc_cd.ToString());
                            if (_userLocas != null)
                            {
                                var _userLocaQuery =
                                    from userLoca in _userLocas
                                    where userLoca.SEL_USR_ID == loginUser.User_name.ToUpper() && userLoca.SEL_COM_CD == loginUser.Mst_com.Mc_cd.ToString() && userLoca.SEL_DEF_LOCCD == 1
                                    select userLoca;

                                foreach (var _userLoca in _userLocaQuery)
                                {
                                    Session["UserDefLoca"] = _userLoca.SEL_LOC_CD;
                                }

                                if (Session["UserDefLoca"] == null)
                                {
                                    Session["UserID"] = "";
                                    Session["UserName"] ="";
                                    Session["UserCompanyCode"] = "";
                                    Session["UserCompanyName"] ="";
                                    Session["UserIP"] = "";
                                    Session["UserComputer"] = "";
                                    Session["version"] = "";
                                    return Json(new { success = false, msg = Resource.txtPlseSetupLoc });

                                }
                                string company = HttpContext.Session["UserCompanyCode"] as string;
                                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                                MasterLocation oMstLoc1 = CHNLSVC.General.GetLocationByLocCode(company, userDefLoc);
                                if (oMstLoc1 != null)
                                {
                                    Session["UserChannl"] = oMstLoc1.Ml_cate_2;
                                    Session["UserSubChannl"] = oMstLoc1.Ml_cate_3;
                                }
                            }

                            //if (Request.QueryString["ReturnUrl"] != null)
                            //{
                            //    FormsAuthentication.RedirectFromLoginPage(userID, false);
                            //}
                            //else
                            //{
                            //    FormsAuthentication.SetAuthCookie(userID, false);
                            //    Response.Redirect("Default.aspx");
                            //}
                            ///end
                            return Json(new { success = true });
                        }
                        else
                        {
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
                                return Json(new { success = false, msg = _msg });

                            }
                            else
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
            catch (Exception ex)
            {
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

        private string genarateUserMenu(string loginUser)
        {
           string menu = "";
            //menu ="<ul class='nav navbar-nav'>";
           List<ST_MENU> menuItem= CHNLSVC.Tours.getUserMenu(loginUser);
           int i = 0;
           foreach (ST_MENU item in menuItem) {
               if (i > 0 && item.MNU_PARENT_ID ==0)
               {
                   menu += "<li class='dropdown bar-line'>";
               }
               if (item.MNU_PARENT_ID == 0) {

                   List<ST_MENU> childItm = menuItem.Where(x => x.MNU_PARENT_ID == item.MNU_ID).ToList();
                   if (childItm.Count>0)
                   {
                       menu += "<a href='#' class='dropdown-toggle' data-toggle='dropdown'>" + item.MNU_LABEL + "<b class='caret'></b></a>";
                       menu += "<ul class='dropdown-menu'>";
                       foreach (ST_MENU chld in childItm)
                       {
                           List<ST_MENU> childItm2 = menuItem.Where(x => x.MNU_PARENT_ID == chld.MNU_ID).ToList();
                           if (childItm2.Count > 0)
                           {
                               menu += "<li class='dropdown-submenu'>";
                               menu += "<a href='#' tabindex='-1'>" + chld.MNU_LABEL + "</a>";
                               menu+="  <ul class='dropdown-menu'>";
                               foreach (ST_MENU chld2 in childItm2)
                               {
                                  
                                   List<ST_MENU> childItm3 = menuItem.Where(x => x.MNU_PARENT_ID == chld2.MNU_ID).ToList();
                                   if (childItm3.Count > 0)
                                   {
                                       foreach (ST_MENU chld3 in childItm3)
                                       {
                                           menu += "<li><a href='/" + chld3.MNU_CONTRL + "/" + chld3.MNU_ACTN + "'>" + chld3.MNU_LABEL + "</a></li>";
                                               //"<li>@Html.ActionLink('" + chld3.MNU_LABEL + "', '" + chld3.MNU_ACTN + "', '" + chld3.MNU_CONTRL + "')</li>";
                                       }
                                   }
                                   else {
                                       menu += "<li><a href='/" + chld2.MNU_CONTRL + "/" + chld2.MNU_ACTN + "'>" + chld2.MNU_LABEL + "</a></li>";
                                       //menu += "<li>@Html.ActionLink('" + chld2.MNU_LABEL + "', '" + chld2.MNU_ACTN + "', '" + chld2.MNU_CONTRL + "')</li>";
                                   }
                                 
                               }
                               menu += "</ul>";
                               menu += "</li>";
                           }
                           else {
                               menu += "<li class='dropdown-submenu'><a href='/" + chld.MNU_CONTRL + "/" + chld.MNU_ACTN + "'>" + chld.MNU_LABEL + "</a></li>";
                           }
                       }
                       menu += "</ul>";
                   }
                   else {
                       menu += "<li class='bar-line'><a href='/" + item.MNU_CONTRL + "/" + item.MNU_ACTN + "'>" + item.MNU_LABEL + "</a></li>";
                       
                   }
               }
               if (i > 0 && item.MNU_PARENT_ID==0)
               {
                   menu += "</li>";
               }
               i++;
           }
           //menu += "</ul>";
           return menu;
        }
        public List<ST_MENU> chiledMenu(List<ST_MENU> menuItem,Int32 parentId)
        {
            List<ST_MENU> child = new List<ST_MENU>();
            var _userLocaQuery =
                                    from item in menuItem
                                    where item.MNU_PARENT_ID== parentId 
                                    select item;
            return child;
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
                    else
                    {
                        return Json(new { success = false, data = _systemUserCompanyList, msg = Resource.NoCompanyAssignMsg }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(new { success = false, data = _systemUserCompanyList, msg = Resource.InvalidLoginDet }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, data = _systemUserCompanyList, msg =ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        //logoff action
        public ActionResult LogOff()
        {
            try
            {
                if ((string)Session["GlbIsExit"] == "false")
                {
                    CHNLSVC.CloseChannel();
                    CHNLSVC.Security.ExitLoginSession((string)Session["GlbUserID"], (string)Session["GlbUserComCode"], (string)Session["GlbUserSessionID"]);
                   Session["GlbIsExit"] = "true";
                }
                Session.RemoveAll();
                Session.Clear();
                if (!string.IsNullOrEmpty(Request.Form["request"]) && Request.Form["request"] == "req")
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Redirect("~/Login");
                }

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
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