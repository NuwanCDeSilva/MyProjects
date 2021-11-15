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
    public class CollectionBonusDefinitionController : BaseController
    {
        // GET: CollectionBonusDefinition
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["BonusDefinition"] = null;
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
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

        public JsonResult AddMainDetails(string Circode, string Startdate, string Pccat, string Taccount, string Bonusp, string Fromdate, string Todate, string Arrfpercentage, string Arrtpercentage, string Accfbalance, string Acctbalance, string Channel, string ShStartdate, string ABvalue)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<BonusDefinition> _lst = new List<BonusDefinition>();
                    if (Session["BonusDefinition"] == null)
                    {

                        BonusDefinition ob = new BonusDefinition();
                        //ob.Hhoa_ac = AccNo;
                        //ob.hbp_accbalance = Convert.ToInt32(Accbalance);
                        //ob.hbp_arrper=Convert.ToInt32(Arrpercentage);
                        //ob.hbp_bnsper=Convert.ToInt32(Bonusp);
                        //ob.hbp_circular=Circode;
                        //ob.hbp_com=company;
                        //ob.hbp_cre_by=userId;
                        //ob.hbp_cre_dt=DateTime.Now.Date;
                        //ob.hbp_frmdate=Convert.ToDateTime(Fromdate);
                        //ob.hbp_pccat=Pccat;
                        //ob.hbp_sryear=Convert.ToInt32(Sayears);
                        //ob.hbp_strdate = Convert.ToDateTime(Startdate);
                        //ob.hbp_taccount=Convert.ToInt32(Taccount);
                        //ob.hbp_todate = Convert.ToDateTime(Todate);

                        ob.hbp_circular = Circode;
                        ob.hbp_strdate = Convert.ToDateTime(Startdate);
                        ob.hbp_pccat = Pccat;
                        ob.hbp_taccount = Convert.ToInt32(Taccount);
                        ob.hbp_bnsper = Convert.ToDecimal(Bonusp);
                        ob.hbp_sr_fyear = Convert.ToInt32(Fromdate);
                        ob.hbp_sr_tyear = Convert.ToInt32(Todate);
                        ob.hbp_from_arrper = Convert.ToDecimal(Arrfpercentage);
                        ob.hbp_to_arrper = Convert.ToDecimal(Arrtpercentage);
                        ob.hbp_from_bal = Convert.ToDecimal(Accfbalance);
                        ob.hbp_to_bal = Convert.ToDecimal(Acctbalance);
                        ob.hbp_cre_by = userId;
                        ob.hbp_cre_dt = DateTime.Now.Date;
                        ob.hbp_com = company;
                        ob.hbp_channel = Channel;
                        ob.hbp_shstrdate = Convert.ToDateTime(ShStartdate);
                        ob.hbp_shstrab = ABvalue;

                        _lst.Add(ob);
                        Session["BonusDefinition"] = _lst;
                    }
                    else
                    {
                        _lst = Session["BonusDefinition"] as List<BonusDefinition>;
                        var count = 0;
                        if (Channel != "")
                        {
                            count = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_pccat.ToString() == Pccat && a.hbp_sr_fyear == Convert.ToInt32(Fromdate) && a.hbp_sr_tyear == Convert.ToInt32(Todate) && a.hbp_from_arrper == Convert.ToDecimal(Arrfpercentage) && a.hbp_to_arrper == Convert.ToDecimal(Arrtpercentage) && a.hbp_from_bal == Convert.ToDecimal(Accfbalance) && a.hbp_to_bal == Convert.ToDecimal(Acctbalance) && a.hbp_channel.ToString() == Channel && a.hbp_shstrab.ToString() == ABvalue).Count();
                        }
                        else
                        {
                            count = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_pccat.ToString() == Pccat && a.hbp_sr_fyear == Convert.ToInt32(Fromdate) && a.hbp_sr_tyear == Convert.ToInt32(Todate) && a.hbp_from_arrper == Convert.ToDecimal(Arrfpercentage) && a.hbp_to_arrper == Convert.ToDecimal(Arrtpercentage) && a.hbp_from_bal == Convert.ToDecimal(Accfbalance) && a.hbp_to_bal == Convert.ToDecimal(Acctbalance) && a.hbp_shstrab.ToString() == ABvalue).Count();
                        }
                            //var count = 0;
                        if (count > 0)
                        {
                            return Json(new { success = false, login = true, msg = "Already added this Record!!", data = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            //var count1 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_from_arrper <= Convert.ToDecimal(Arrfpercentage) && a.hbp_from_arrper >= Convert.ToDecimal(Arrfpercentage)).Count();
                            //var count2 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_to_arrper <= Convert.ToDecimal(Arrtpercentage) && a.hbp_to_arrper >= Convert.ToDecimal(Arrtpercentage)).Count();
                            //var count3 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_from_bal <= Convert.ToDecimal(Accfbalance) && a.hbp_from_bal >= Convert.ToDecimal(Accfbalance)).Count();
                            //var count4 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_to_bal <= Convert.ToDecimal(Acctbalance) && a.hbp_to_bal >= Convert.ToDecimal(Acctbalance)).Count();
                            //var count1 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_strdate == Convert.ToDateTime(Startdate) && a.hbp_pccat.ToString() == Pccat && a.hbp_channel.ToString() == Channel && a.hbp_taccount == Convert.ToInt32(Taccount) && a.hbp_bnsper == Convert.ToDecimal(Bonusp) && a.hbp_shstrdate == Convert.ToDateTime(ShStartdate) && a.hbp_sr_fyear >= Convert.ToInt32(Fromdate) && a.hbp_sr_tyear <= Convert.ToInt32(Todate) && a.hbp_shstrab.ToString() == ABvalue).Count();

                            //var count2 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_strdate == Convert.ToDateTime(Startdate) && a.hbp_pccat.ToString() == Pccat && a.hbp_channel.ToString() == Channel && a.hbp_taccount == Convert.ToInt32(Taccount) && a.hbp_bnsper == Convert.ToDecimal(Bonusp) && a.hbp_shstrdate == Convert.ToDateTime(ShStartdate) && a.hbp_from_arrper >= Convert.ToDecimal(Arrfpercentage) && a.hbp_to_arrper <= Convert.ToDecimal(Arrtpercentage) && a.hbp_shstrab.ToString() == ABvalue).Count();

                            //var count3 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_strdate == Convert.ToDateTime(Startdate) && a.hbp_pccat.ToString() == Pccat && a.hbp_channel.ToString() == Channel && a.hbp_taccount == Convert.ToInt32(Taccount) && a.hbp_bnsper == Convert.ToDecimal(Bonusp) && a.hbp_shstrdate == Convert.ToDateTime(ShStartdate) && a.hbp_from_bal >= Convert.ToDecimal(Accfbalance) && a.hbp_to_bal <= Convert.ToDecimal(Acctbalance) && a.hbp_shstrab.ToString() == ABvalue).Count();

                            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                            //var count1 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_strdate == Convert.ToDateTime(Startdate) && a.hbp_pccat.ToString() == Pccat && a.hbp_channel.ToString() == Channel && a.hbp_taccount == Convert.ToInt32(Taccount) && a.hbp_shstrdate == Convert.ToDateTime(ShStartdate) && a.hbp_sr_fyear >= Convert.ToInt32(Fromdate) && a.hbp_sr_tyear <= Convert.ToInt32(Todate) && a.hbp_shstrab.ToString() == ABvalue).Count();

                            //var count2 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_strdate == Convert.ToDateTime(Startdate) && a.hbp_pccat.ToString() == Pccat && a.hbp_channel.ToString() == Channel && a.hbp_taccount == Convert.ToInt32(Taccount) && a.hbp_shstrdate == Convert.ToDateTime(ShStartdate) && a.hbp_from_arrper >= Convert.ToDecimal(Arrfpercentage) && a.hbp_to_arrper <= Convert.ToDecimal(Arrtpercentage) && a.hbp_shstrab.ToString() == ABvalue).Count();

                            //var count3 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_strdate == Convert.ToDateTime(Startdate) && a.hbp_pccat.ToString() == Pccat && a.hbp_channel.ToString() == Channel && a.hbp_taccount == Convert.ToInt32(Taccount) && a.hbp_shstrdate == Convert.ToDateTime(ShStartdate) && a.hbp_from_bal >= Convert.ToDecimal(Accfbalance) && a.hbp_to_bal <= Convert.ToDecimal(Acctbalance) && a.hbp_shstrab.ToString() == ABvalue).Count();

                            //var count4 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_strdate == Convert.ToDateTime(Startdate) && a.hbp_pccat.ToString() == Pccat && a.hbp_channel.ToString() == Channel && a.hbp_taccount == Convert.ToInt32(Taccount) && a.hbp_shstrdate == Convert.ToDateTime(ShStartdate) && a.hbp_sr_fyear >= Convert.ToInt32(Fromdate) && a.hbp_sr_tyear <= Convert.ToInt32(Todate) && a.hbp_from_arrper >= Convert.ToDecimal(Arrfpercentage) && a.hbp_to_arrper <= Convert.ToDecimal(Arrtpercentage) && a.hbp_from_bal >= Convert.ToDecimal(Accfbalance) && a.hbp_to_bal <= Convert.ToDecimal(Acctbalance) && a.hbp_shstrab.ToString() == ABvalue).Count();
                            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                            var count_1 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_strdate == Convert.ToDateTime(Startdate) && a.hbp_pccat.ToString() == Pccat && a.hbp_channel.ToString() == Channel && a.hbp_taccount == Convert.ToInt32(Taccount) && a.hbp_shstrdate == Convert.ToDateTime(ShStartdate) && a.hbp_sr_fyear <= Convert.ToInt32(Fromdate) && a.hbp_sr_tyear >= Convert.ToInt32(Fromdate) && a.hbp_shstrab.ToString() == ABvalue).Count();
                            var count_2 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_strdate == Convert.ToDateTime(Startdate) && a.hbp_pccat.ToString() == Pccat && a.hbp_channel.ToString() == Channel && a.hbp_taccount == Convert.ToInt32(Taccount) && a.hbp_shstrdate == Convert.ToDateTime(ShStartdate) && a.hbp_sr_fyear <= Convert.ToInt32(Todate) && a.hbp_sr_tyear >= Convert.ToInt32(Todate) && a.hbp_shstrab.ToString() == ABvalue).Count();

                            var count_3 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_strdate == Convert.ToDateTime(Startdate) && a.hbp_pccat.ToString() == Pccat && a.hbp_channel.ToString() == Channel && a.hbp_taccount == Convert.ToInt32(Taccount) && a.hbp_shstrdate == Convert.ToDateTime(ShStartdate) && a.hbp_from_arrper <= Convert.ToDecimal(Arrfpercentage) && a.hbp_to_arrper >= Convert.ToDecimal(Arrfpercentage) && a.hbp_shstrab.ToString() == ABvalue).Count();
                            var count_4 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_strdate == Convert.ToDateTime(Startdate) && a.hbp_pccat.ToString() == Pccat && a.hbp_channel.ToString() == Channel && a.hbp_taccount == Convert.ToInt32(Taccount) && a.hbp_shstrdate == Convert.ToDateTime(ShStartdate) && a.hbp_from_arrper <= Convert.ToDecimal(Arrtpercentage) && a.hbp_to_arrper >= Convert.ToDecimal(Arrtpercentage) && a.hbp_shstrab.ToString() == ABvalue).Count();

                            var count_5 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_strdate == Convert.ToDateTime(Startdate) && a.hbp_pccat.ToString() == Pccat && a.hbp_channel.ToString() == Channel && a.hbp_taccount == Convert.ToInt32(Taccount) && a.hbp_shstrdate == Convert.ToDateTime(ShStartdate) && a.hbp_from_bal <= Convert.ToDecimal(Accfbalance) && a.hbp_to_bal >= Convert.ToDecimal(Accfbalance) && a.hbp_shstrab.ToString() == ABvalue).Count();
                            var count_6 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_strdate == Convert.ToDateTime(Startdate) && a.hbp_pccat.ToString() == Pccat && a.hbp_channel.ToString() == Channel && a.hbp_taccount == Convert.ToInt32(Taccount) && a.hbp_shstrdate == Convert.ToDateTime(ShStartdate) && a.hbp_from_bal <= Convert.ToDecimal(Acctbalance) && a.hbp_to_bal >= Convert.ToDecimal(Acctbalance) && a.hbp_shstrab.ToString() == ABvalue).Count();

                            var count4 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_strdate == Convert.ToDateTime(Startdate) && a.hbp_pccat.ToString() == Pccat && a.hbp_channel.ToString() == Channel && a.hbp_taccount == Convert.ToInt32(Taccount) && a.hbp_shstrdate == Convert.ToDateTime(ShStartdate) && a.hbp_sr_fyear >= Convert.ToInt32(Fromdate) && a.hbp_sr_tyear <= Convert.ToInt32(Todate) && a.hbp_from_arrper >= Convert.ToDecimal(Arrfpercentage) && a.hbp_to_arrper <= Convert.ToDecimal(Arrtpercentage) && a.hbp_from_bal >= Convert.ToDecimal(Accfbalance) && a.hbp_to_bal <= Convert.ToDecimal(Acctbalance) && a.hbp_shstrab.ToString() == ABvalue).Count();

                            if (count_1 > 0 || count_2 > 0)
                            {
                                if (count_3 > 0 || count_4 > 0)
                                {
                                    if (count_5 > 0 || count_6 > 0)
                                    {
                                        return Json(new { success = false, login = true, msg = "This range is already exist!!", data = "" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                            //var count1 = 0;
                            //var count2 = 0;
                            //var count3 = 0;
                            //var count4 = 0;

                            //if (count1 > 0 || count2 > 0 || count3 > 0)
                            //{
                            if (count4 > 0)
                            {
                                return Json(new { success = false, login = true, msg = "This range is already exist!!", data = "" }, JsonRequestBehavior.AllowGet);
                            }
                            //}
                            else
                            {
                                BonusDefinition ob = new BonusDefinition();
                                ob.hbp_circular = Circode;
                                ob.hbp_strdate = Convert.ToDateTime(Startdate);
                                ob.hbp_pccat = Pccat;
                                ob.hbp_taccount = Convert.ToInt32(Taccount);
                                ob.hbp_bnsper = Convert.ToDecimal(Bonusp);
                                ob.hbp_sr_fyear = Convert.ToInt32(Fromdate);
                                ob.hbp_sr_tyear = Convert.ToInt32(Todate);
                                ob.hbp_from_arrper = Convert.ToDecimal(Arrfpercentage);
                                ob.hbp_to_arrper = Convert.ToDecimal(Arrtpercentage);
                                ob.hbp_from_bal = Convert.ToDecimal(Accfbalance);
                                ob.hbp_to_bal = Convert.ToDecimal(Acctbalance);
                                ob.hbp_cre_by = userId;
                                ob.hbp_cre_dt = DateTime.Now.Date;
                                ob.hbp_com = company;
                                ob.hbp_channel = Channel;
                                ob.hbp_shstrdate = Convert.ToDateTime(ShStartdate);
                                ob.hbp_shstrab = ABvalue;
                                _lst.Add(ob);
                                Session["BonusDefinition"] = _lst;
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
        public JsonResult RemoveMainDetails(string Circode, string Startdate, string Pccat)   //, string Arrpercentage, string Sayears, string Fromdate, string Todate, string Taccount, string Accbalance, string Bonusp)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<BonusDefinition> _lst = new List<BonusDefinition>();
                if (Session["BonusDefinition"] != null)
                {
                    _lst = (List<BonusDefinition>)Session["BonusDefinition"];
                }
                else
                {
                    _lst = new List<BonusDefinition>();

                }
                var itemToRemove = _lst.First(r => r.hbp_circular == Circode && r.hbp_strdate == Convert.ToDateTime(Startdate) && r.hbp_pccat == Pccat);
                //var count = _lst.Where(a => a.hmfa_mgr_cd.ToString() == Manager && a.hmfa_pc.ToString() == Location && a.hmfa_bonus_st_dt == Convert.ToDateTime(BonusMonth).AddMonths(1).AddDays(-1)).Count();

                _lst.Remove(itemToRemove);
                Session["BonusDefinition"] = _lst;
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
                if (Session["BonusDefinition"] != null)
                {
                    //Auto Number
                    List<BonusDefinition> list1 = new List<BonusDefinition>();

                    list1 = (List<BonusDefinition>)Session["BonusDefinition"];

                    //save
                    //int effect = CHNLSVC.Finance.SaveHandOverAccounts(list, out err);
                    int effect = CHNLSVC.General.SaveBonusDetails(list1, out err);

                    //int effect = 1;
                    if (effect == 1 || effect == 0)
                    {
                        return Json(new { success = true, login = true, msg = "Successfull Saved " + err }, JsonRequestBehavior.AllowGet);
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
        public JsonResult SaveAdjDetails(string duetotadj, string prevmontharreadj)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string err = "";

                List<hpt_arr_acc> _list = Session["hdracc2"] as List<hpt_arr_acc>;
               
                //save
                int effect = CHNLSVC.Finance.UpdateARR_ACCNew(_list, out err);
                if (effect == 1 || effect == 0)
                {
                    return Json(new { success = true, login = true, msg = "Successfull Saved" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "" }, JsonRequestBehavior.AllowGet);
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
                Session["BonusDefinition"] = null;
                return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, msg = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult LoadMainDetails(string Circode)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            Circode = Circode.Trim();
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<BonusDefinition> _list = new List<BonusDefinition>();
                _list = CHNLSVC.General.GetCircularcbdData(company, Circode);
                Session["BonusDefinition"] = _list;
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

    }
}