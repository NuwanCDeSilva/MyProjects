using FF.BusinessObjects.Genaral;
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
    public class RoleCreationController : BaseController
    {
        // GET: RoleCreation
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string userDefChnl = HttpContext.Session["UserDefChnl"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                ViewBag.MenuAll = GetUserMenu();
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }


        public string GetSessionValues()
        {

            if (string.IsNullOrEmpty(Session["UserID"].ToString()))
            {

                return "";
            }
            else
            {

                return Session["UserID"].ToString();
            }

        }


        public JsonResult GetRoleIdDetails(string companyId, string roleid)
        {

            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    List<USER_ROLE> documents = CHNLSVC.General.getUserRoleID(companyId, roleid);

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


        public JsonResult GetUserDetailsByRID(string companyId, string roleid)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    List<UsersRoleData> documents = CHNLSVC.General.getUserDetailsByRID(companyId, roleid);

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


        public JsonResult GetMenuDetailsByRID(string userid, string companyId, string roleid)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(roleid) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    List<ROLE_MENU_BRID> documents = CHNLSVC.General.getMenuDetailsByRID(userid, companyId, roleid);

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

        public JsonResult Save_User_Roles(string company, string roleid, string rolename, string description, string createdby, string modifiedby, string createddate, string modifieddate, string choice, string active, string session)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string companyId = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string sessionID = HttpContext.Session["SessionID"] as string;

            if (choice == "false")
            {
                choice = "update";
            }
            else
            {
                choice = "create";
            }

            if (active == "true")
            {
                active = "1";
            }
            else
            {
                active = "0";
            }

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string err = "0";

                    int documents = CHNLSVC.Security.Save_User_Roles(company, roleid, rolename, description, userId, userId, choice, active, sessionID, out err);

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


        public JsonResult GetOptionID(string companyId, string roleid)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    List<OptionId_Details> documents = CHNLSVC.General.getOptions(companyId, roleid);

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


        public JsonResult Update_Option_IDs(List<string> Optioidlist, string company, string roleid)
        {
            List<string> OptionIdList = new List<string>();
            List<string> OptionStatusList = new List<string>();
            string[] div = new string[2];
            for (int i = 0; i < Optioidlist.Count; i++)
            {
                div = Optioidlist[i].Split('%');
                OptionIdList.Insert(i, div[0].ToString());
                if (div[1].ToString() == "false")
                {
                    OptionStatusList.Insert(i, "0");
                }
                else
                {
                    OptionStatusList.Insert(i, "1");
                }
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

                    int documents = CHNLSVC.General.Save_Option_IDS(OptionStatusList, company, roleid, OptionIdList, out err);

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

        public string GetUserMenu()
        {
            int gg = 0;
            string menu = "<ul id='example_tree' class='example_tree'>";
            List<SEC_SYSTEM_MENU> menuItem = CHNLSVC.Security.getMenuAll();
            List<SEC_SYSTEM_MENU> usrMnitm = menuItem.Where(x => x.SSM_PARENT_ID == 0).OrderBy(x => x.SSM_ID).ToList();

            foreach (SEC_SYSTEM_MENU item in usrMnitm)
            {

                menu += "<li class='Mparent'>" + "<input type='checkbox' class='MenuOpId' id='" + item.SSM_ID + "'>" + "<span>" + item.SSM_LABEL.ToString() + "</span>";


                List<SEC_SYSTEM_MENU> child1menu = menuItem.Where(x => x.SSM_PARENT_ID == item.SSM_ID).OrderBy(x => x.SSM_ORDER_ID).ToList();
                if (child1menu.Count == 0)
                {
                    menu += "</li>";
                }
                else
                {
                    menu += "<ul class='child1'>";
                    foreach (SEC_SYSTEM_MENU item1 in child1menu)
                    {

                        menu += "<li class='Mparent2'>" + "<input type='checkbox' class='MenuOpId' id='" + item1.SSM_ID + "'>" + "<span>" + item1.SSM_LABEL.ToString() + "</span>";

                        List<SEC_SYSTEM_MENU> child2menu = menuItem.Where(x => x.SSM_PARENT_ID == item1.SSM_ID).OrderBy(x => x.SSM_ORDER_ID).ToList();

                        if (child2menu.Count == 0)
                        {
                            menu += "</li>";
                        }
                        else
                        {

                            menu += "<ul class='child2'>";
                            foreach (SEC_SYSTEM_MENU item2 in child2menu)
                            {
                                menu += "<li class='Mparent3'> " + "<input type='checkbox' class='MenuOpId' id='" + item2.SSM_ID + "'>" + "<span>" + item2.SSM_LABEL.ToString() + "</span>";

                                List<SEC_SYSTEM_MENU> child3menu = menuItem.Where(x => x.SSM_PARENT_ID == item2.SSM_ID).OrderBy(x => x.SSM_ORDER_ID).ToList();

                                if (child3menu.Count == 0)
                                {
                                    menu += "</li>";
                                }
                                else {
                                    menu += "<ul class='child3'>";
                                    foreach (SEC_SYSTEM_MENU item3 in child3menu)
                                    {
                                        menu += "<li>" + "<input type='checkbox' class='MenuOpId' id='" + item3.SSM_ID + "'>" + "<span>" + item3.SSM_LABEL.ToString() + "</span></li>";
                                    }
                                    menu += "</ul>";
                                }


                            }

                            menu += "</ul>";
                            menu += "</li>";
                        }
                    }

                    menu += "</ul>";
                    menu += "</li>";
                }
            }

            menu += "</ul>";
            return menu;
        }

        public JsonResult GetMenusByCandRid(string companyId, string roleid)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<SEC_SYSTEM_MENU> documents = CHNLSVC.Security.getUserMenuByCompanyNRID(roleid, companyId);

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

        public JsonResult Update_User_Menu(List<string> MenuIdlist, string company, string roleid)
        {
            List<string> OptionIdList = new List<string>();
            List<string> OptionStatusList = new List<string>();
            string[] div = new string[2];
            for (int i = 0; i < MenuIdlist.Count; i++)
            {
                div = MenuIdlist[i].Split('%');
                OptionIdList.Insert(i, div[0].ToString());
                if (div[1].ToString() == "false")
                {
                    OptionStatusList.Insert(i, "0");
                }
                else
                {
                    OptionStatusList.Insert(i, "1");
                }
            }

            string userId = HttpContext.Session["UserId"] as string;
            string companyId = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string err = "";

                    int documents = CHNLSVC.Security.Update_Company_Menu_List(company, roleid, OptionIdList, OptionStatusList, out err);

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

        public JsonResult GetShipmentList(string company)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string companyId = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    List<MST_SHIPMENTS> documents = CHNLSVC.General.getShipmentList(company);

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

        public JsonResult Update_Shipment_List(List<string> ShipmentDetlist, string company,string mode_name, string ShipDesc, string choice)
        {
            List<string> OptionIdList = new List<string>();
            List<string> OptionStatusList = new List<string>();
            string[] div = new string[2];
            if (ShipmentDetlist != null)
            {
                for (int i = 0; i < ShipmentDetlist.Count; i++)
                {
                    div = ShipmentDetlist[i].Split('%');
                    OptionIdList.Insert(i, div[0].ToString());
                    if (div[1].ToString() == "false")
                    {
                        OptionStatusList.Insert(i, "0");
                    }
                    else
                    {
                        OptionStatusList.Insert(i, "1");
                    }
                }
            }
            if (ShipmentDetlist == null) {
                OptionIdList.Insert(0, mode_name);
                OptionStatusList.Insert(0, "1");
            
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

                    int documents = CHNLSVC.General.Update_Modeshipment_Details(company, OptionIdList, ShipDesc, OptionStatusList, choice, out err);

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
    }
}