using FF.BusinessObjects.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FastForward.Logistic.Controllers
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
            if (string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(company) && string.IsNullOrEmpty(userDefPro) && string.IsNullOrEmpty(userDefLoc))
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
                  
                     return Json(new { success = false, msg = "Please enter valid login details." });
                   
                }
                else
                {
                    if (string.IsNullOrEmpty(loginUser.Mst_com.Mc_cd))
                    {
                        return Json(new { success = false, msg = "No companies assign fo this user." });
                    }

                    string _msg = string.Empty;
                    string _msgTitle = string.Empty;
                    int _msgStatus = 0;
                    loginUser.User_name = loginUser.User_name.ToUpper();
                    string _WindowsLogonName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    string _WindowsUser = Environment.UserName;
                    //string GlbVersionNo = CHNLSVC.Security.GetCurrentVersion().ToString();
                    LoginUser _loginUser = CHNLSVC.Security.LoginToSystem(loginUser.User_name.ToUpper(), loginUser.Password.ToString(), loginUser.Mst_com.Mc_cd, GlbVersionNo, (string)Session["GlbUserIP"], (string)Session["GlbHostName"], _loginRetryInCounter, out _loginRetryOutCounter, out _msgStatus, out _msg, out _msgTitle);
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
                        Session["Log_Autho"] = _loginUser.SE_LOG_AUTHO;

                        Session["GlbUserID"] = loginUser.User_name.ToUpper();
                        Session["GlbUserComCode"] = (!string.IsNullOrEmpty(loginUser.Mst_com.Mc_cd)) ? loginUser.Mst_com.Mc_cd.ToString() : "";
                        Session["GlbUserIP"] = GetIP();
                        Session["GlbHostName"] = Dns.GetHostName();
                        SetLoginCacheLayer(_loginUser, _WindowsLogonName, _WindowsUser);
                        FormsAuthentication.SetAuthCookie(loginUser.User_name, false);
                        string menu = genarateUserMenu(loginUser.User_name.ToUpper(), loginUser.Mst_com.Mc_cd);
                        Session["Menu"] = menu;
                            ///added for check compny and set sesstion
                            Session["SessionID"] = CHNLSVC.Security.SaveLoginSession(loginUser.User_name, loginUser.Mst_com.Mc_cd.ToString(), Request.UserHostAddress, Request.UserHostName, "", "");

                            Session["UserDefChnl"] = _loginUser.User_def_chnl.ToString(); // Added by Chathura on 14-sep-2017

                            List<SystemUserProf> _userprofs = CHNLSVC.Security.GetUserProfCenters(loginUser.User_name.ToUpper(), loginUser.Mst_com.Mc_cd.ToString(), _loginUser.User_def_chnl.ToString()); // Changed by Chathura on 15-sep-2017 commented below
                            //List<SystemUserProf> _userprofs = CHNLSVC.Security.GetUserProfCenters(loginUser.User_name.ToUpper(), loginUser.Mst_com.Mc_cd.ToString());
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
                                return Json(new { success = false, msg = "Please setup user profit centers" });

                            }
                            string userDefPro = HttpContext.Session["UserDefProf"] as string;
                            string com = HttpContext.Session["UserCompanyCode"] as string;
                            MasterProfitCenter proCenDet = CHNLSVC.Security.GetProfitCenter(com, userDefPro);
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
                                    Session["UserName"] = "";
                                    Session["UserCompanyCode"] = "";
                                    Session["UserCompanyName"] = "";
                                    Session["UserIP"] = "";
                                    Session["UserComputer"] = "";
                                    Session["version"] = "";
                                    return Json(new { success = false, msg = "Please setup user locations." });

                                }
                                string company = HttpContext.Session["UserCompanyCode"] as string;
                                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                                MasterLocation oMstLoc1 = CHNLSVC.Security.GetLocationByLocCode(company, userDefLoc);
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
                    else if (_msgStatus == -1)
                    {
                        if (_loginUser.Login_attempts >= _loginRetryOutCounter)
                        {
                            _loginRetryInCounter = _loginRetryOutCounter;
                             return Json(new { success = false, msg = _msg });

                         
                        }
                        else
                        {
                            CHNLSVC.Security.Save_User_Falis(loginUser.User_name.ToString(), loginUser.Password.ToString(), loginUser.Mst_com.Mc_cd, (string)Session["GlbUserIP"], _WindowsLogonName, _WindowsUser);
                            _msg = "You have failed to remember your details. \nContact Administration for further instructions";
                            return Json(new { success = false, msg = _msg });
                           
                        }
                    }
                    else if (_msgStatus == -2)
                    {
                        _loginRetryInCounter = _loginRetryOutCounter;
                          return Json(new { success = false, msg = _msg });
                       
                    }
                    else if (_msgStatus == -99)
                    {
                         return Json(new { success = false, msg = _msg });
                        
                    }
                    else if (_msgStatus == -4)
                    {
                       
                            return Json(new { success = false, msg = _msg });
                        
                    }
                    return View(loginUser);
                }


            }
            catch (Exception ex)
            {
               
                    return Json(new { success = false, msg = ex.Message.ToString() });
                

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
                    else
                    {
                        return Json(new { success = false, data = _systemUserCompanyList, msg = "No company assign for this user"}, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(new { success = false, data = _systemUserCompanyList, msg = "Please enter user name and password." }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, data = _systemUserCompanyList, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LogOff()
        {
            try
            {
                if ((string)Session["GlbIsExit"] == "false")
                {
                    CHNLSVC.CloseChannel();
                    CHNLSVC.Security.ExitLoginSession((string)Session["UserID"], (string)Session["UserCompanyCode"], (string)Session["SessionID"]);
                   Session["GlbIsExit"] = "true";
                }
                Session.RemoveAll();
                Session.Clear();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                
                return Json(new { success = false, msg = err.Message.ToString(),type="Error" }, JsonRequestBehavior.AllowGet);
                
            }

        }
        private string genarateUserMenu(string loginUser,string company)
        {
            string menu = "";
            List<SEC_SYSTEM_MENU> menuItem = CHNLSVC.Security.getUserMenu(loginUser, company, "SUPUSR");
            if (menuItem.Count > 0)
            {
                List<SEC_SYSTEM_MENU> usrMnitm = menuItem.Where(x => x.SSM_PARENT_ID == 0).OrderBy(x => x.SSM_ORDER_ID).OrderBy(y=>y.SSM_ORDER_ID).ToList();
                int i = 0;
                foreach (SEC_SYSTEM_MENU item in menuItem)
                {
                    menu += "<li class='treeview'>";
                    if (item.SSM_PARENT_ID == 0)
                    {
                        menu += "<a href=/" + item.SSM_ACTION + ">";
                        menu += "<i class='" + item.SSM_LBL_IMG + "'></i> <span>" + item.SSM_LABEL + "</span>";
                        menu += "<span class='pull-right-container'>";
                        menu += "<i class='fa fa-angle-left pull-right'></i>";
                        menu += "</span>";
                        menu += "</a>";
                        List<SEC_SYSTEM_MENU> childItm = menuItem.Where(x => x.SSM_PARENT_ID == item.SSM_ID).ToList();
                        if (childItm.Count > 0)
                        {
                            
                           foreach (SEC_SYSTEM_MENU chld in childItm)
                            {
                                menu += "<ul class='treeview-menu'>";
                                List<SEC_SYSTEM_MENU> childItm2 = menuItem.Where(x => x.SSM_PARENT_ID == chld.SSM_ID).ToList();
                                if (childItm2.Count > 0)
                                {
                                    menu+="<li class='treeview'>";

                                    menu += " <a href=/" + item.SSM_ACTION + "><i class='" + chld.SSM_LBL_IMG + "'></i> " + chld.SSM_LABEL;
                                    menu+="<span class='pull-right-container'>";
                                    menu+="<i class='fa fa-angle-left pull-right'></i>";
                                    menu += "</span>";
                                    menu += "</a>";
                                    foreach (SEC_SYSTEM_MENU chld2 in childItm2)
                                    {
                                        menu += "<ul class='treeview-menu'>";
                                        List<SEC_SYSTEM_MENU> childItm3 = menuItem.Where(x => x.SSM_PARENT_ID == chld2.SSM_ID).ToList();
                                        if (childItm3.Count > 0)
                                        {
                                            menu += "<li class='treeview'>";

                                            menu += " <a href=/" + chld2.SSM_ACTION + "><i class='" + chld2.SSM_LBL_IMG + "'></i> " + chld2.SSM_LABEL;
                                            menu += "<span class='pull-right-container'>";
                                            menu += "<i class='fa fa-angle-left pull-right'></i>";
                                            menu += "</span>";
                                            menu += "</a>";
                                            menu += "<ul class='treeview-menu'>";
                                            foreach (SEC_SYSTEM_MENU chld3 in childItm3)
                                            {
                                                menu += "<li><a href='/" + chld3.SSM_CONTRL + "/" + chld3.SSM_ACTION + "'><i class='" + chld3.SSM_LBL_IMG + "'></i> " + chld3.SSM_LABEL + "</a></li>";
                                           }
                                            menu += "</ul>";
                                            menu += "</li>";
                                        }
                                        else
                                        {
                                            if (chld2.SSM_CONTRL == "#" && chld2.SSM_ACTION == "#")
                                            {
                                                menu += "<li><a href='/" + chld2.SSM_CONTRL + "'><i class='" + chld2.SSM_LBL_IMG + "'></i> " + chld2.SSM_LABEL + "</a></li>";
                                            }
                                            else
                                            {
                                                menu += "<li><a href='/" + chld2.SSM_CONTRL + "/" + chld2.SSM_ACTION + "'><i class='" + chld2.SSM_LBL_IMG + "'></i> " + chld2.SSM_LABEL + "</a></li>";
                                            }
                                       
                                        }
                                        menu += "</ul>";
                                    }
                                    menu += "</li>";
                                }
                                else
                                {
                                   
                                    if (chld.SSM_CONTRL =="#" &&  chld.SSM_ACTION=="#")
                                    {
                                        menu += "<li><a href='/" + chld.SSM_CONTRL + "'><i class='" + chld.SSM_LBL_IMG + "'></i> " + chld.SSM_LABEL + "</a></li>";
                                    }
                                    else
                                    {
                                        menu += "<li><a href='/" + chld.SSM_CONTRL + "/" + chld.SSM_ACTION + "'><i class='" + chld.SSM_LBL_IMG + "'></i> " + chld.SSM_LABEL + "</a></li>";
                                    }
                                    
                                    
                                }
                                menu += "</ul>";
                            }
                        }
                    }
                        menu += "</li>";
                    i++;
                }
            }
            return menu;
        }
      
    
    }
}