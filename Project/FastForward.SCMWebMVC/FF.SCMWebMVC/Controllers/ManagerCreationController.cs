using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FF.BusinessObjects.Commission;
using FF.BusinessObjects.General;
using FF.BusinessObjects;
using FF.BusinessObjects.BITool;
using System.Data;

namespace FF.SCMWebMVC.Controllers
{
    public class ManagerCreationController : BaseController
    {
        // GET: ManagerCreation
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["MgrCreation"] = null;
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }

        public JsonResult AddHandOverAccounts(string Date, string BonusMonth, string Srdate, string Location, string Manager, string Calmethod, string Pccat, string Mgrname, string Mainlocation )
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<MgrCreation> _lst = new List<MgrCreation>();
                if (Session["MgrCreation"] == null)
                {

                    MgrCreation ob = new MgrCreation();
                    //ob.Hhoa_ac = AccNo;
                    ob.hmfa_mgr_cd = Manager;
                    ob.hmfa_mgr_name = Mgrname;
                    ob.hmfa_pc = Location;
                    ob.hmfa_bonus_st_dt = Convert.ToDateTime(BonusMonth).AddMonths(1).AddDays(-1);
                    ob.hmfa_sr_open_dt = Convert.ToDateTime(Srdate); 
                    ob.hmfa_acc_dt = Convert.ToDateTime(Date);
                    ob.hmfa_pc_cat = Pccat;
                    ob.hmfa_com = company;
                    ob.hmfa_cre_by = userId;
                    ob.hmfa_cre_dt = DateTime.Now.Date;
                    ob.hmfa_bonus_method = Calmethod;
                    ob.hmfa_act_stus = true;
                    ob.hmfa_mainpc = Mainlocation;
                    _lst.Add(ob);
                    Session["MgrCreation"] = _lst;
                }
                else
                {
                    _lst = Session["MgrCreation"] as List<MgrCreation>;
                    var count = 0;
                    //var count = _lst.Where(a => a.hmfa_mgr_cd.ToString() == Manager && a.hmfa_pc.ToString() == Location && a.hmfa_bonus_st_dt == Convert.ToDateTime(BonusMonth).AddMonths(1).AddDays(-1)).Count();
                    count = _lst.Where(a => a.hmfa_mgr_cd.ToString() == Manager && a.hmfa_pc.ToString() == Location).Count();
                    
                    if (count > 0)
                    {
                        return Json(new { success = false, login = true, msg = "Already added this Location!!", data = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        MgrCreation ob = new MgrCreation();
                        ob.hmfa_mgr_cd = Manager;
                        ob.hmfa_mgr_name = Mgrname;
                        ob.hmfa_pc = Location;
                        ob.hmfa_bonus_st_dt = Convert.ToDateTime(BonusMonth).AddMonths(1).AddDays(-1);
                        ob.hmfa_sr_open_dt = Convert.ToDateTime(Srdate);
                        ob.hmfa_acc_dt = Convert.ToDateTime(Date);
                        ob.hmfa_pc_cat = Pccat;
                        ob.hmfa_com = company;
                        ob.hmfa_cre_by = userId;
                        ob.hmfa_cre_dt = DateTime.Now.Date;
                        ob.hmfa_bonus_method = Calmethod;
                        ob.hmfa_act_stus = true;
                        ob.hmfa_mainpc = Mainlocation;
                        _lst.Add(ob);
                        Session["MgrCreation"] = _lst;
                    }

                }
                return Json(new { success = true, login = true, list = _lst }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult LoadPCCat(string invType)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    List<GetPCCategory> pricebook = CHNLSVC.Sales.GetPCCatlist(company);
                    var _books = pricebook.Select(x => x.MSPH_TP).Distinct().ToList();

                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (_books.Count > 0)
                    {
                        foreach (var list in _books)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list;
                            o1.Value = list;
                            oList.Add(o1);

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
                return Json(new { success = false, login = true, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult RemoveAccountCode(string PC, string Manager, string Month, string Ammount)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<MgrCreation> _lst = new List<MgrCreation>();
                if (Session["MgrCreation"] != null)
                {
                    _lst = (List<MgrCreation>)Session["MgrCreation"];
                }
                else
                {
                    _lst = new List<MgrCreation>();

                }
                var itemToRemove = _lst.First(r => r.hmfa_pc == PC && r.hmfa_bonus_st_dt == Convert.ToDateTime(Month).AddMonths(1).AddDays(-1) && r.hmfa_mgr_cd == Manager);
                //var count = _lst.Where(a => a.hmfa_mgr_cd.ToString() == Manager && a.hmfa_pc.ToString() == Location && a.hmfa_bonus_st_dt == Convert.ToDateTime(BonusMonth).AddMonths(1).AddDays(-1)).Count();
  
                _lst.Remove(itemToRemove);
                Session["MgrCreation"] = _lst;
                return Json(new { success = true, login = true, list = _lst }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveHandAccount()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string err = "";
                if (Session["MgrCreation"] != null)
                {
                    //Auto Number
                    List<MgrCreation> list = new List<MgrCreation>();
                    list = (List<MgrCreation>)Session["MgrCreation"];
                    //save
                    //int effect = CHNLSVC.Finance.SaveHandOverAccounts(list, out err);
                    int effect = CHNLSVC.General.SaveMangerDetails(list, out err);

                    //int effect = 1;
                    if (effect == 1 || effect == 0)
                    {
                        return Json(new { success = true, login = true, msg = "Successfull Saved" + err }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = err }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = true, login = true, msg = "Please enter the records" }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult LoadHandOverDetails(string Manager)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            //Manager = Manager.Trim();
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<MgrCreation> _list = new List<MgrCreation>();
                _list = CHNLSVC.General.GetMgrextData(company, Manager);
                Session["MgrCreation"] = _list;
                //if (Isbalance == "1")
                //{
                //    //_list = CHNLSVC.Finance.GetHandOverData(company, Pc, true);
                //}
                //else
                //{
                //   // _list = CHNLSVC.Finance.GetHandOverData(company, Pc, false);
                //}
                if (_list == null)
                {
                    return Json(new { success = true, login = true, list = _list }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = true, list = _list }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult ClearAll()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["MgrCreation"] = null;
                return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, msg = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}