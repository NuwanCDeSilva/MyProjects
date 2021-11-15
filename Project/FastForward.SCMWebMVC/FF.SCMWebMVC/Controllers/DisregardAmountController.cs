using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FF.BusinessObjects.Commission;
using FF.BusinessObjects.General;

namespace FF.SCMWebMVC.Controllers
{
    public class DisregardAmountController : BaseController
    {
        // GET: DisregardAmount
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["Disregard_Amount"] = null;
                Session["hpr_disr_val_ref"] = null;
                Session["hpr_disr_pc_defn"] = null;
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }

        public JsonResult AddMainDetails(string Circode, string Valuefrom, string Valueto, string Percentage, string Rate, string Base )
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<hpr_disr_val_ref> _lst = new List<hpr_disr_val_ref>();
                    if (Session["hpr_disr_val_ref"] == null)
                    {

                        hpr_disr_val_ref ob = new hpr_disr_val_ref();
                        //ob.Hhoa_ac = AccNo;
                        ob.hdvr_circular = Circode;
                        ob.hdvr_cre_by = userId;
                        ob.hdvr_cre_dt = DateTime.Now.Date;
                        ob.hdvr_from_val = Convert.ToDecimal(Valuefrom);
                        ob.hdvr_to_val = Convert.ToDecimal(Valueto);
                        //ob.hdvr_tp = Convert.ToInt32(Percentage);
                        ob.hdvr_val = Convert.ToDecimal(Percentage);
                        ob.hdvr_ratevalue = Rate;
                        ob.hdvr_base = Base;

                        _lst.Add(ob);
                        Session["hpr_disr_val_ref"] = _lst;
                    }
                    else
                    {
                        _lst = Session["hpr_disr_val_ref"] as List<hpr_disr_val_ref>;
                        //var count = _lst.Where(a => a.hdvr_circular.ToString() == Circode && a.hdvr_from_val >= Convert.ToDecimal(Valuefrom) && a.hdvr_to_val <= Convert.ToDecimal(Valueto) && a.hdvr_val == Convert.ToDecimal(Percentage)).Count();
                        var count = _lst.Where(a => a.hdvr_circular.ToString() == Circode && a.hdvr_from_val >= Convert.ToDecimal(Valuefrom) && a.hdvr_to_val <= Convert.ToDecimal(Valueto)).Count();
                        if (count > 0)
                        {
                            return Json(new { success = false, login = true, msg = "Already added this Range!!", data = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            var count1 = _lst.Where(a => a.hdvr_circular.ToString() == Circode && a.hdvr_from_val <= Convert.ToDecimal(Valuefrom) && a.hdvr_to_val >= Convert.ToDecimal(Valuefrom)).Count();
                            var count2 = _lst.Where(a => a.hdvr_circular.ToString() == Circode && a.hdvr_from_val <= Convert.ToDecimal(Valueto) && a.hdvr_to_val >= Convert.ToDecimal(Valueto)).Count();
                            //var count3 = _lst.Where(a => a.hdvr_circular.ToString() == Circode && a.hdvr_to_val >= Convert.ToDecimal(Valuefrom) && a.hdvr_to_val <= Convert.ToDecimal(Valuefrom)).Count();
                            //var count4 = _lst.Where(a => a.hdvr_circular.ToString() == Circode && a.hdvr_to_val >= Convert.ToDecimal(Valueto) && a.hdvr_to_val <= Convert.ToDecimal(Valueto)).Count();
                            if (count1 > 0)
                            {
                                return Json(new { success = false, login = true, msg = "You entered “From value” is available in the added definition value range. Please enter valid from value", data = "" }, JsonRequestBehavior.AllowGet);
                            }
                            else if (count2 > 0)
                            {
                                return Json(new { success = false, login = true, msg = "You entered “To value” is available in the added definition value range. Please enter valid to value", data = "" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                hpr_disr_val_ref ob = new hpr_disr_val_ref();
                                //ob.Hhoa_ac = AccNo;
                                ob.hdvr_circular = Circode;
                                ob.hdvr_cre_by = userId;
                                ob.hdvr_cre_dt = DateTime.Now.Date;
                                ob.hdvr_from_val = Convert.ToDecimal(Valuefrom);
                                ob.hdvr_to_val = Convert.ToDecimal(Valueto);
                                //ob.hdvr_tp = Convert.ToInt32(Percentage);
                                ob.hdvr_val = Convert.ToDecimal(Percentage);
                                ob.hdvr_ratevalue = Rate;
                                ob.hdvr_base = Base;

                                _lst.Add(ob);
                                Session["hpr_disr_val_ref"] = _lst;
                            }
                        }
                    }
                    return Json(new { success = true, login = true, list = _lst }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, msg = "Please enter correct value" }, JsonRequestBehavior.AllowGet);
                //return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        

        }
        public JsonResult RemoveMainDetails(string Circode, string Valuefrom, string Valueto, string Percentage )
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<hpr_disr_val_ref> _lst = new List<hpr_disr_val_ref>();
                if (Session["hpr_disr_val_ref"] != null)
                {
                    _lst = (List<hpr_disr_val_ref>)Session["hpr_disr_val_ref"];
                }
                else
                {
                    _lst = new List<hpr_disr_val_ref>();

                }
                var itemToRemove = _lst.First(r => r.hdvr_circular == Circode && r.hdvr_from_val == Convert.ToDecimal(Valuefrom) && r.hdvr_to_val == Convert.ToDecimal(Valueto) && r.hdvr_val == Convert.ToDecimal(Percentage));
                //var count = _lst.Where(a => a.hmfa_mgr_cd.ToString() == Manager && a.hmfa_pc.ToString() == Location && a.hmfa_bonus_st_dt == Convert.ToDateTime(BonusMonth).AddMonths(1).AddDays(-1)).Count();

                _lst.Remove(itemToRemove);
                Session["hpr_disr_val_ref"] = _lst;
                return Json(new { success = true, login = true, list = _lst }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveAllDetails()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string err = "";

                if (Session["hpr_disr_val_ref"] != null || Session["hpr_disr_pc_defn"] != null)
                {
                List<hpr_disr_val_ref> list1 = new List<hpr_disr_val_ref>();
                List<hpr_disr_pc_defn> list2 = new List<hpr_disr_pc_defn>();

                list1 = (List<hpr_disr_val_ref>)Session["hpr_disr_val_ref"];
                list2 = (List<hpr_disr_pc_defn>)Session["hpr_disr_pc_defn"];

                //save
                //int effect = CHNLSVC.Finance.SaveHandOverAccounts(list, out err);
                int effect = CHNLSVC.General.SaveCirDetails(list1, 2, out err);
                int effect1 = CHNLSVC.General.SaveLocDetails(list2, out err);

                //int effect = 1;
                //if (effect == 1 || effect == 0)
                if (effect == 1 || effect == 0 || effect1 == 1 || effect1 == 0)
                {
                    return Json(new { success = true, login = true, msg = "Successfull Saved " }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = err }, JsonRequestBehavior.AllowGet);
                }
                }
                else
                {
                    return Json(new { success = true, login = true, msg = "Please add record" }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult LoadMainDetails(string Circode, string Cat)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            Circode = Circode.Trim();
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<hpr_disr_val_ref> _listcheck = new List<hpr_disr_val_ref>();
                List<hpr_disr_val_ref> _list = new List<hpr_disr_val_ref>();
                _listcheck = CHNLSVC.General.CheckCircularData(company, Circode, Cat);
                if(_listcheck != null)
                {
                    return Json(new { success = false, login = true, msg = "This circular number already saved with differnt type" }, JsonRequestBehavior.AllowGet);
                }
                
                _list = CHNLSVC.General.GetCircularData(company, Circode, Cat);
                Session["hpr_disr_val_ref"] = _list;
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
                Session["hpr_disr_val_ref"] = null;
                Session["hpr_disr_pc_defn"] = null;
                return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, msg = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult AddLocDetails(string Circode, string Channel, string Location, string Mnthfrom, string Mnthto)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                try
                {
                    DateTime dtmMnthfrom = Convert.ToDateTime(Mnthfrom);
                    DateTime dtmMnthto = Convert.ToDateTime(Mnthto);
                    List<hpr_disr_pc_defn> _lst = new List<hpr_disr_pc_defn>();
                    if (dtmMnthfrom < dtmMnthto)
                    {

                        //dtpFromEffDate.Value = new DateTime(dCalcDate.Year, dCalcDate.Month, 1);
                        //List<hpr_disr_pc_defn> _lst = new List<hpr_disr_pc_defn>();
                        if (Session["hpr_disr_val_ref"] != null)
                        {
                            //List<hpr_disr_pc_defn> _lst = new List<hpr_disr_pc_defn>();
                            if (Session["hpr_disr_pc_defn"] == null)
                            {

                                hpr_disr_pc_defn ob = new hpr_disr_pc_defn();
                                ob.hdpd_circular = Circode;
                                ob.hdpd_com = company;
                                ob.hdpd_cre_by = userId;
                                ob.hdpd_cre_dt = DateTime.Now;
                                ob.hdpd_from_dt = Convert.ToDateTime(Mnthfrom).AddMonths(1).AddDays(-1);
                                ob.hdpd_channel = Channel;
                                ob.hdpd_pc = Location;
                                ob.hdpd_to_dt = Convert.ToDateTime(Mnthto).AddMonths(1).AddDays(-1);

                                _lst.Add(ob);
                                Session["hpr_disr_pc_defn"] = _lst;
                            }
                            else
                            {
                                _lst = Session["hpr_disr_pc_defn"] as List<hpr_disr_pc_defn>;
                                //var count = _lst.Where(a => a.hdpd_manager.ToString() == Manager && a.hdpd_pc == Location && a.hdpd_from_dt == Convert.ToDateTime(Mnthfrom).AddMonths(1).AddDays(-1) && a.hdpd_to_dt == Convert.ToDateTime(Mnthto).AddMonths(1).AddDays(-1)).Count();
                                //var count = _lst.Where(a => a.hdpd_pc == Location && a.hdpd_from_dt == Convert.ToDateTime(Mnthfrom).AddMonths(1).AddDays(-1) && a.hdpd_to_dt == Convert.ToDateTime(Mnthto).AddMonths(1).AddDays(-1)).Count();
                                //var count1 = _lst.Where(a => a.hdpd_pc == Location && a.hdpd_from_dt <= Convert.ToDateTime(Mnthfrom).AddMonths(1).AddDays(-1) && a.hdpd_to_dt >= Convert.ToDateTime(Mnthfrom).AddMonths(1).AddDays(-1)).Count();
                                //var count2 = _lst.Where(a => a.hdpd_pc == Location && a.hdpd_from_dt <= Convert.ToDateTime(Mnthto).AddMonths(1).AddDays(-1) && a.hdpd_to_dt >= Convert.ToDateTime(Mnthto).AddMonths(1).AddDays(-1)).Count();
                                var count = 0;
                                var count1 = 0;
                                var count2 = 0;

                                if (Channel != "")
                                {
                                    count = _lst.Where(a => a.hdpd_channel == Channel && a.hdpd_from_dt == Convert.ToDateTime(Mnthfrom).AddMonths(1).AddDays(-1) && a.hdpd_to_dt == Convert.ToDateTime(Mnthto).AddMonths(1).AddDays(-1)).Count();
                                    count1 = _lst.Where(a => a.hdpd_channel == Channel && a.hdpd_from_dt <= Convert.ToDateTime(Mnthfrom).AddMonths(1).AddDays(-1) && a.hdpd_to_dt >= Convert.ToDateTime(Mnthfrom).AddMonths(1).AddDays(-1)).Count();
                                    count2 = _lst.Where(a => a.hdpd_channel == Channel && a.hdpd_from_dt <= Convert.ToDateTime(Mnthto).AddMonths(1).AddDays(-1) && a.hdpd_to_dt >= Convert.ToDateTime(Mnthto).AddMonths(1).AddDays(-1)).Count();

                                }
                                if (Location != "")
                                {
                                    count = _lst.Where(a => a.hdpd_pc == Location && a.hdpd_from_dt == Convert.ToDateTime(Mnthfrom).AddMonths(1).AddDays(-1) && a.hdpd_to_dt == Convert.ToDateTime(Mnthto).AddMonths(1).AddDays(-1)).Count();
                                    count1 = _lst.Where(a => a.hdpd_pc == Location && a.hdpd_from_dt <= Convert.ToDateTime(Mnthfrom).AddMonths(1).AddDays(-1) && a.hdpd_to_dt >= Convert.ToDateTime(Mnthfrom).AddMonths(1).AddDays(-1)).Count();
                                    count2 = _lst.Where(a => a.hdpd_pc == Location && a.hdpd_from_dt <= Convert.ToDateTime(Mnthto).AddMonths(1).AddDays(-1) && a.hdpd_to_dt >= Convert.ToDateTime(Mnthto).AddMonths(1).AddDays(-1)).Count();


                                }


                                if (count > 0 || count1 > 0 || count2 > 0)
                                {
                                    return Json(new { success = false, login = true, msg = "Already added this Record!!", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    hpr_disr_pc_defn ob = new hpr_disr_pc_defn();
                                    ob.hdpd_circular = Circode;
                                    ob.hdpd_com = company;
                                    ob.hdpd_cre_by = userId;
                                    ob.hdpd_cre_dt = DateTime.Now;
                                    ob.hdpd_from_dt = Convert.ToDateTime(Mnthfrom).AddMonths(1).AddDays(-1);
                                    ob.hdpd_channel = Channel;
                                    ob.hdpd_pc = Location;
                                    ob.hdpd_to_dt = Convert.ToDateTime(Mnthto).AddMonths(1).AddDays(-1);

                                    _lst.Add(ob);
                                    Session["hpr_disr_pc_defn"] = _lst;
                                }
                            }

                            return Json(new { success = true, login = true, list = _lst }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { success = true, login = true, list = _lst, msg = "Please select the circular code" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //return Json(new { success = true, login = true, list = _lst, msg = "From date can not be greater than To date!!" }, JsonRequestBehavior.AllowGet);
                        return Json(new { success = false, login = true, msg = "From date can not be greater than To date!!" }, JsonRequestBehavior.AllowGet);
                    }
                }

                catch
                {
                    return Json(new { success = false, login = true, msg = "Invalide Date!!" }, JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult RemoveLocDetails(string Circode, string Manager, string Location, string Mnthfrom, string Mnthto)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<hpr_disr_pc_defn> _lst = new List<hpr_disr_pc_defn>();
                if (Session["hpr_disr_pc_defn"] != null)
                {
                    _lst = (List<hpr_disr_pc_defn>)Session["hpr_disr_pc_defn"];
                }
                else
                {
                    _lst = new List<hpr_disr_pc_defn>();

                }
                //var itemToRemove = _lst.First(r => r.hdpd_manager == Manager && r.hdpd_pc == Location );
                var itemToRemove = _lst.First(r => r.hdpd_manager == Manager);
                //var count = _lst.Where(a => a.hmfa_mgr_cd.ToString() == Manager && a.hmfa_pc.ToString() == Location && a.hmfa_bonus_st_dt == Convert.ToDateTime(BonusMonth).AddMonths(1).AddDays(-1)).Count();

                _lst.Remove(itemToRemove);
                Session["hpr_disr_val_ref"] = _lst;
                return Json(new { success = true, login = true, list = _lst }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}