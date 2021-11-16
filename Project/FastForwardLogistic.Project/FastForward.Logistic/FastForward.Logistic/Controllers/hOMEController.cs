using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FF.BusinessObjects;
using System.IO;
using System.Web.UI;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;
using FF.BusinessObjects.Security;
//using FF.BusinessObjects.Security;

namespace FastForward.Logistic.Controllers
{
    public class HomeController : BaseController
    {
        // GET: hOME
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string userDefChnl = HttpContext.Session["UserDefChnl"] as string; // Added by Chathura on 14-sep-2017

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }
        public ActionResult Error() 
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

        public JsonResult ChangeProfitCenter(string pc)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                if (pc != "")
                {
                    Session["UserDefProf"] = pc;
                    MasterProfitCenter proCenDet = CHNLSVC.Sales.GetProfitCenter(company, pc);
                    Session["Title"] = proCenDet.Mpc_oth_ref;
                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "Please select valid profit center.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // Changed by Chathura on 15-sep-2017 and commented below
        public JsonResult ChangeCompany(string pc)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            //MasterProfitCenter proCenDetDup = null;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                if (pc != "")
                {
                    
                    company = pc;

                    string menu = genarateUserMenu(userId.ToUpper(), company);
                    Session["Menu"] = menu;

                    List<SystemUserChannel> advref = CHNLSVC.Sales.GetUserChannels(userId, company);
                    if (advref != null)
                    {
                        var _advrefQuery =
                        from _advref in advref
                        //where userProf.Sup_usr_id == userId.ToUpper() && userProf.Sup_com_cd == company.ToString() && userProf.Sup_def_pccd == true
                        select _advref;

                        string selChanel = "";

                        foreach (var _useradvref in _advrefQuery)
                        {
                            selChanel = _useradvref.Msc_cd;                            
                        }

                        List<SystemUserProf> _userprofs = CHNLSVC.Sales.GetUserProfCenters(userId.ToUpper(), company.ToString(), selChanel); // Changed by Chathura
                        if (_userprofs != null)
                        {
                            var _userprofQuery =
                            from userProf in _userprofs
                            select userProf;

                            foreach (var _userprof in _userprofQuery)
                            {
                                Session["UserDefProf"] = _userprof.Sup_pc_cd;                                
                            }
                            //proCenDetDup = CHNLSVC.Sales.GetProfitCenter(company, pc);

                            Session["UserDefChnl"] = selChanel;
                            Session["UserCompanyCode"] = pc;
                            MasterCompany proCenDet = CHNLSVC.Sales.GetUserCompanySet(company, pc);
                            Session["Title"] = proCenDet.Mc_cd;
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "No default type of shipment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            //Session["UserDefProf"] = "";
                        }

                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No default type of shipment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        //Session["UserDefProf"] = "";
                    }

                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "Please select valid Company.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }

        //public JsonResult ChangeCompany(string pc)
        //{
        //    string userId = HttpContext.Session["UserID"] as string;
        //    string company = HttpContext.Session["UserCompanyCode"] as string;
        //    string userDefPro = HttpContext.Session["UserDefProf"] as string;
        //    string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

        //    if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //    {
        //        if (pc != "")
        //        {
        //            Session["UserCompanyCode"] = pc;
        //            company = pc;
        //            MasterCompany proCenDet = CHNLSVC.Sales.GetUserCompanySet(company, pc);
        //            Session["Title"] = proCenDet.Mc_cd;
        //            return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(new { success = false, login = true, msg = "Please select valid Company.", type = "Info" }, JsonRequestBehavior.AllowGet);
        //        }

        //    }
        //    else
        //    {
        //        return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        // Added by Chathura on 15-sep-2017
        public JsonResult ChangeModeOfShipment(string pc)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefLoc))
            {
                if (pc != "")
                {
                    

                    List<SystemUserProf> _userprofs = CHNLSVC.Sales.GetUserProfCenters(userId.ToUpper(), company.ToString(), pc.ToString()); // Changed by Chathura
                    if (_userprofs != null)
                    {
                        var _userprofQuery =
                        from userProf in _userprofs
                        //where userProf.Sup_usr_id == userId.ToUpper() && userProf.Sup_com_cd == company.ToString() && userProf.Sup_def_pccd == true
                        select userProf;

                        foreach (var _userprof in _userprofQuery)
                        {

                            Session["UserDefProf"] = _userprof.Sup_pc_cd;

                        }

                        Session["UserDefChnl"] = pc;
                        Session["Title"] = pc;

                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No default type of shipment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        //Session["UserDefProf"] = "";
                    }

                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { success = false, login = true, msg = "Please select valid mode of shipment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }

        private string genarateUserMenu(string loginUser, string company)
        {
            string menu = "";
            List<SEC_SYSTEM_MENU> menuItem = CHNLSVC.Security.getUserMenu(loginUser, company, "SUPUSR");
            if (menuItem.Count > 0)
            {
                List<SEC_SYSTEM_MENU> usrMnitm = menuItem.Where(x => x.SSM_PARENT_ID == 0).OrderBy(x => x.SSM_ORDER_ID).OrderBy(y => y.SSM_ORDER_ID).ToList();
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
                                    menu += "<li class='treeview'>";

                                    menu += " <a href=/" + item.SSM_ACTION + "><i class='" + chld.SSM_LBL_IMG + "'></i> " + chld.SSM_LABEL;
                                    menu += "<span class='pull-right-container'>";
                                    menu += "<i class='fa fa-angle-left pull-right'></i>";
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

                                    if (chld.SSM_CONTRL == "#" && chld.SSM_ACTION == "#")
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