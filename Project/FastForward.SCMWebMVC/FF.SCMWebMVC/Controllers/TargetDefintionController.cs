using FF.BusinessObjects.Commission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FF.SCMWebMVC.Controllers
{
    public class TargetDefintionController : BaseController
    {
        //
        // GET: /TargetDefintion/
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["ref_comm_trgt_comm"] = null;
                return View();

            }
            else
            {
                return Redirect("~/Login/index");
            }
        }
        public JsonResult AddTargets(string CommCode, string month, string execcd, string target, string fromper, string toper, string execcomm, string manager, string managercomm, string brand)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                List<ref_comm_trgt_comm> _lst = new List<ref_comm_trgt_comm>();
                if (Session["ref_comm_trgt_comm"] == null)
                {
                    ref_comm_trgt_comm ob = new ref_comm_trgt_comm();
                    ob.rctc_cre_dt = DateTime.Now; ;
                    ob.rctc_creby = userId;
                    ob.rctc_doc_no = CommCode;
                    ob.rctc_end_per = Convert.ToDecimal(toper);
                    ob.rctc_exc_rate = Convert.ToDecimal(execcomm);
                    ob.rctc_exec = execcd;
                    ob.rctc_mngr = manager;
                    ob.rctc_stus = 1;
                    if (managercomm == "") managercomm = "0";

                    ob.rctc_mngr_rate = Convert.ToDecimal(managercomm);
                    ob.rctc_month = Convert.ToDateTime(month).Month;
                    ob.rctc_st_per = Convert.ToDecimal(fromper);
                    ob.rctc_target = Convert.ToDecimal(target);
                    ob.rctc_anal1 = brand;
                    _lst.Add(ob);
                    Session["ref_comm_trgt_comm"] = _lst;
                }
                else
                {
                    _lst = Session["ref_comm_trgt_comm"] as List<ref_comm_trgt_comm>;
                    var count = _lst.Where(a => a.rctc_doc_no == CommCode && a.rctc_month == Convert.ToDateTime(month).Month && a.rctc_exec == execcd && a.rctc_st_per == Convert.ToDecimal(fromper) && a.rctc_end_per == Convert.ToDecimal(toper) && a.rctc_anal1==brand).Count();
                    if (count > 0)
                    {
                        return Json(new { success = false, login = true, msg = "Already added this  Exec!!", data = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ref_comm_trgt_comm ob = new ref_comm_trgt_comm();
                        ob.rctc_cre_dt = DateTime.Now; ;
                        ob.rctc_creby = userId;
                        ob.rctc_doc_no = CommCode;
                        ob.rctc_end_per = Convert.ToDecimal(toper);
                        ob.rctc_exc_rate = Convert.ToDecimal(execcomm);
                        ob.rctc_exec = execcd;
                        ob.rctc_mngr = manager;
                        ob.rctc_stus = 1;
                        if (managercomm == "") managercomm = "0";

                        ob.rctc_mngr_rate = Convert.ToDecimal(managercomm);
                        ob.rctc_month = Convert.ToDateTime(month).Month;
                        ob.rctc_year = Convert.ToDateTime(month).Year;
                        ob.rctc_st_per = Convert.ToDecimal(fromper);
                        ob.rctc_target = Convert.ToDecimal(target);
                        ob.rctc_anal1 = brand;
                        _lst.Add(ob);
                        Session["ref_comm_trgt_comm"] = _lst;
                    }

                }
                return Json(new { success = true, login = true, list = _lst }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult RemoveGrid(string docno, string execcd, string month, string from, string to, string brand)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<ref_comm_trgt_comm> _lst = new List<ref_comm_trgt_comm>();
                if (Session["ref_comm_trgt_comm"] != null)
                {
                    _lst = (List<ref_comm_trgt_comm>)Session["ref_comm_trgt_comm"];
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "Can't Delete", data = "" }, JsonRequestBehavior.AllowGet);

                }
                if (_lst.Count > 0)
                {
                    foreach (ref_comm_trgt_comm r in _lst)
                    {
                        if (r.rctc_doc_no == docno && r.rctc_exec == execcd && r.rctc_month == Convert.ToInt32(month) && r.rctc_st_per == Convert.ToDecimal(from) && r.rctc_end_per == Convert.ToDecimal(to) && r.rctc_anal1 == brand)
                        {
                            r.rctc_modby = userId;
                            r.rctc_mod_dt = DateTime.Now;
                            r.rctc_stus = 0;
                        }
                    }
                }
                //var itemToRemove = _lst.First(r => r.rctc_doc_no == docno && r.rctc_exec == execcd && r.rctc_month == Convert.ToInt32(month) && r.rctc_st_per == Convert.ToDecimal(from) && r.rctc_end_per == Convert.ToDecimal(to) && r.rctc_anal1==brand);
                //_lst.Remove(itemToRemove);
                Session["ref_comm_trgt_comm"] = _lst;
                return Json(new { success = true, login = true, list = _lst.Where(x=>x.rctc_stus==1) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveDetails()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string err = "";
                //Auto Number
                List<ref_comm_trgt_comm> list = new List<ref_comm_trgt_comm>();
                list = (List<ref_comm_trgt_comm>)Session["ref_comm_trgt_comm"];
                //save
                if (Session["ref_comm_trgt_comm"] == null)
                {
                    return Json(new { success = false, login = true, msg = "No new records to save", data = "" }, JsonRequestBehavior.AllowGet);
                }
                int effect = CHNLSVC.Finance.SaveCommTargetCommission(list, out err);
                if (effect > 0)
                {
                    return Json(new { success = true, login = true, msg = "Successfull Saved"  }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = err }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult LoadDetails(string CommCode, string month, string execcd)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<ref_comm_trgt_comm> _list = new List<ref_comm_trgt_comm>();
                _list = CHNLSVC.Finance.GetTargetCommRates(CommCode, execcd, Convert.ToDateTime(month).Month, Convert.ToDateTime(month).Year);
                if (_list == null)
                {
                    _list = new List<ref_comm_trgt_comm>();
                }
                Session["ref_comm_trgt_comm"] = _list;
                return Json(new { success = true, login = true, list = _list }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
             
        }
        public JsonResult ClearSession() {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            { 
                Session["ref_comm_trgt_comm"] = null;
                return Json(new { success = true, login = true}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}