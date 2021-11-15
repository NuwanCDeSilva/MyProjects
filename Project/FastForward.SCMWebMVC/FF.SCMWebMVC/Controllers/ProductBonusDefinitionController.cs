using FF.BusinessObjects;
using FF.BusinessObjects.BITool;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using FF.BusinessObjects.General;
using FF.BusinessObjects.Commission;
using FF.BusinessObjects.Tours;
using System.IO;

namespace FF.SCMWebMVC.Controllers
{
    public class ProductBonusDefinitionController : BaseController
    {
        //
        // GET: /ProductBonusDefinition/

        List<ref_bonus_loc> bonus_loc = new List<ref_bonus_loc>();
        List<REF_BONUS_HDR> bonus_hdr = new List<REF_BONUS_HDR>();
        List<ref_bonus_det> bonus_det = new List<ref_bonus_det>();
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["ref_bonus_loc"] = null;
                Session["REF_BONUS_HDR"] = null;
                Session["ref_bonus_det"] = null;
                Session["ExcelItem"] = null;
                Session["StrExcelItem"] = null;
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }
        public JsonResult ChanalelTextChange(string Chanal, string Com)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                if (Com != null && Com != "")
                {
                    if (Com != "undefined")
                    {
                        company = Com;
                    }
                }
                List<LOC_HIRCH_SEARCH_HEAD> documents = CHNLSVC.Dashboard.getLocHierarchy("1", "10", "Code", Chanal, null, null, null, null, null, company, "channel");
                if (documents != null && documents.Count > 0)
                {
                    return Json(new { success = true, login = true, data = documents }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult SubChanalelTextChange(string SChanal, string Com)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                if (Com != null && Com != "")
                {
                    if (Com != "undefined")
                    {
                        company = Com;
                    }
                }
                List<LOC_HIRCH_SEARCH_HEAD> documents = CHNLSVC.Dashboard.getLocHierarchy("1", "10", "Code", SChanal, null, null, null, null, null, company, "sub_channel");
                if (documents != null && documents.Count > 0)
                {
                    return Json(new { success = true, login = true, data = documents }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult RegionTextChange(string Region, string Com)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                if (Com != null && Com != "")
                {
                    if (Com != "undefined")
                    {
                        company = Com;
                    }
                }
                List<LOC_HIRCH_SEARCH_HEAD> documents = CHNLSVC.Dashboard.getLocHierarchy("1", "10", "Code", Region, null, null, null, null, null, company, "region");
                if (documents != null && documents.Count > 0)
                {
                    return Json(new { success = true, login = true, data = documents }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult ZoneTextChange(string Zone, string Com)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                if (Com != null && Com != "")
                {
                    if (Com != "undefined")
                    {
                        company = Com;
                    }
                }
                List<LOC_HIRCH_SEARCH_HEAD> documents = CHNLSVC.Dashboard.getLocHierarchy("1", "10", "Code", Zone, null, null, null, null, null, company, "zone");
                if (documents != null && documents.Count > 0)
                {
                    return Json(new { success = true, login = true, data = documents }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult PCTextChange(string PC, string Com)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                if (Com != null && Com != "")
                {
                    if (Com != "undefined")
                    {
                        company = Com;
                    }
                }
                List<LOC_HIRCH_SEARCH_HEAD> documents = CHNLSVC.Dashboard.getLocHierarchy("1", "10", "Code", PC, null, null, null, null, null, company, "PC");
                if (documents != null && documents.Count > 0)
                {
                    return Json(new { success = true, login = true, data = documents }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult LoadPriceBook(string invType)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    List<PriceBookRef> pricebook = CHNLSVC.Sales.GetPriceBooklist(company);
                    var _books = pricebook.Select(x => x.Sapb_pb).Distinct().ToList();

                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (_books.Count > 0)
                    {
                        ComboBoxObject o2 = new ComboBoxObject();
                        o2.Text = "Select";
                        o2.Value = "";
                        oList.Add(o2);
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
        public JsonResult LoadPriceLevel(string invType, string book)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    List<PriceBookLevelRef> pricebook = CHNLSVC.Sales.GetPriceLevelList(company, book, "");
                    pricebook = pricebook.OrderBy(a => a.Sapl_pb_lvl_cd).ToList();
                    var _levels = pricebook.Select(y => y.Sapl_pb_lvl_cd).Distinct().ToList();
                    

                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (_levels.Count > 0)
                    {
                        foreach (var list in _levels)
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
        //dilshan**************************
        public JsonResult LoadSSubType(string invType, string book)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    //List<PriceBookLevelRef> pricebook = CHNLSVC.Sales.GetPriceLevelList(company, book, "");
                    //pricebook = pricebook.OrderBy(a => a.Sapl_pb_lvl_cd).ToList();
                    //var _levels = pricebook.Select(y => y.Sapl_pb_lvl_cd).Distinct().ToList();

                    DataTable dt = CHNLSVC.General.Get_sar_price_type("");

                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (var list in dt.Rows)
                        {
                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = dt.Rows[i]["sarpt_cd"].ToString();
                            o1.Value = dt.Rows[i]["sarpt_cd"].ToString();
                            oList.Add(o1);
                            i++;
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
        //***********************************
        public JsonResult LoadPayType()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    List<PaymentTypeRef> paytype = CHNLSVC.Sales.GetAllPaymentType(company, "", "");
                    var _types = paytype.Select(y => y.Sapt_cd).Distinct().ToList();

                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (_types.Count > 0)
                    {
                        foreach (var list in _types)
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
        public JsonResult LoadSlabBase()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = "QTY";
                            o1.Value = "QTY";
                            oList.Add(o1);
                            ComboBoxObject o2 = new ComboBoxObject();
                            o2.Text = "VALUE";
                            o2.Value = "VALUE";
                            oList.Add(o2);

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
        public JsonResult LoadTargetBase()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    ComboBoxObject o1 = new ComboBoxObject();
                    o1.Text = "Exec";
                    o1.Value = "Exec";
                    oList.Add(o1);
                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "Loc";
                    o2.Value = "Loc";
                    oList.Add(o2);
                    ComboBoxObject o3 = new ComboBoxObject();
                    o3.Text = "Area";
                    o3.Value = "Area";
                    oList.Add(o3);
                    ComboBoxObject o4 = new ComboBoxObject();
                    o4.Text = "Region";
                    o4.Value = "Region";
                    oList.Add(o4);
                    ComboBoxObject o5 = new ComboBoxObject();
                    o5.Text = "Zone";
                    o5.Value = "Zone";
                    oList.Add(o5);

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
        public JsonResult AddLocationDetails(string Channel, string Schannel, string Region, string Zone, string PC, String Company)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (Session["ref_bonus_loc"] == null)
                {
                    bonus_loc = new List<ref_bonus_loc>();
                    ref_bonus_loc ob = new ref_bonus_loc();
                    ob.Rbl_chnl = Channel;
                    ob.Rbl_pc = PC;
                    ob.Rbl_region = Region;
                    ob.Rbl_sub_chnl = Schannel;
                    ob.Rbl_zone = Zone;
                    ob.Rbl_anal1 = Company;
                    bonus_loc.Add(ob);
                    Session["ref_bonus_loc"] = bonus_loc;
                }
                else
                {
                    bonus_loc = Session["ref_bonus_loc"] as List<ref_bonus_loc>;
                    var count = bonus_loc.Where(a => a.Rbl_zone == Zone && a.Rbl_sub_chnl == Schannel && a.Rbl_region == Region && a.Rbl_pc == PC && a.Rbl_chnl == Channel && a.Rbl_anal1 == Company).Count();
                    if (count > 0)
                    {
                        return Json(new { success = false, login = true, msg = "Already added this Loc Data!!", data = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ref_bonus_loc ob = new ref_bonus_loc();
                        ob.Rbl_chnl = Channel;
                        ob.Rbl_pc = PC;
                        ob.Rbl_region = Region;
                        ob.Rbl_sub_chnl = Schannel;
                        ob.Rbl_zone = Zone;
                        ob.Rbl_anal1 = Company;
                        bonus_loc.Add(ob);
                        Session["ref_bonus_loc"] = bonus_loc;
                    }
                }

                if (bonus_loc.Count > 0)
                {
                    return Json(new { success = true, login = true, data = bonus_loc }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult RemoveLocDet(string Chnl, string Schnl, string Region, string Zone, string PC, string Company)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                if (Session["ref_bonus_loc"] != null)
                {
                    bonus_loc = (List<ref_bonus_loc>)Session["ref_bonus_loc"];
                }
                else
                {
                    bonus_loc = new List<ref_bonus_loc>();

                }
                var itemToRemove = bonus_loc.First(r => r.Rbl_chnl == Chnl && r.Rbl_pc == PC && r.Rbl_region == Region && r.Rbl_sub_chnl == Schnl && r.Rbl_zone == Zone && r.Rbl_anal1 == Company);
                bonus_loc.Remove(itemToRemove);
                Session["ref_bonus_loc"] = bonus_loc;
                return Json(new { success = true, login = true, data = bonus_loc }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddItemDetails(string itemCode, string cat1, string cat2, string brand, string model, string PriceBook, string PriceLevel, string pricecircular, string invtype, string paymode, string paymodesub, string slabbaseon, string custormer, string hpscheme, string fromval, string toval, string marks, string malty, string targetbase, string CombineNo, string TotCombQty, string PLALL, string STALL)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                string itemexcel = "";
                if (Session["StrExcelItem"] != null)
                {
                    itemexcel = Session["StrExcelItem"].ToString();
                }
                if (Session["ref_bonus_det"] == null)
                {
                    bonus_det = new List<ref_bonus_det>();

                    string pball = PLALL;
                    string stall = STALL;
                    if (pball != "" && stall == "" || stall == null)
                    {
                        pball = pball.Replace(@"chkpl=", string.Empty);
                        string[] words2 = pball.Split('&');

                        if (itemexcel != "")
                        {
                            string[] wordsexcel = itemexcel.Split('&');
                            foreach (var excellist in wordsexcel)
                            {
                                foreach (var pblist in words2)
                                {
                                    ref_bonus_det ob = new ref_bonus_det();
                                    ob.Rbd_cat1 = cat1;
                                    ob.Rbd_cat2 = cat2;
                                    ob.Rbd_cus_cd = custormer;
                                    ob.Rbd_from_val = Convert.ToInt64(fromval);
                                    ob.Rbd_hp_schm = hpscheme;
                                    ob.Rbd_item_cd = excellist;
                                    ob.Rbd_model = model;
                                    ob.Rbd_pay_mode = paymode;
                                    ob.Rbd_pay_sub_tp = paymodesub;
                                    ob.Rbd_pb = PriceBook;
                                    ob.Rbd_pl = pblist;
                                    ob.Rbd_price_circul = pricecircular;
                                    ob.Rbd_sales_tp = invtype;
                                    ob.Rbd_slab_base = slabbaseon;
                                    ob.Rdb_marks = Convert.ToDecimal(marks);
                                    ob.Rdb_to_val = Convert.ToInt64(toval);
                                    ob.Rdb_brand = brand;
                                    ob.Rdb_anal1 = malty;
                                    ob.Rdb_anal2 = targetbase;
                                    ob.Rdb_anal3 = CombineNo;
                                    ob.Rdb_tot_comb_qty = Convert.ToDecimal(TotCombQty);

                                    bonus_det.Add(ob);
                                }
                            }
                        }
                        else
                        {
                            foreach (var pblist in words2)
                            {
                                ref_bonus_det ob = new ref_bonus_det();
                                ob.Rbd_cat1 = cat1;
                                ob.Rbd_cat2 = cat2;
                                ob.Rbd_cus_cd = custormer;
                                ob.Rbd_from_val = Convert.ToInt64(fromval);
                                ob.Rbd_hp_schm = hpscheme;
                                ob.Rbd_item_cd = itemCode;
                                ob.Rbd_model = model;
                                ob.Rbd_pay_mode = paymode;
                                ob.Rbd_pay_sub_tp = paymodesub;
                                ob.Rbd_pb = PriceBook;
                                ob.Rbd_pl = pblist;
                                ob.Rbd_price_circul = pricecircular;
                                ob.Rbd_sales_tp = invtype;
                                ob.Rbd_slab_base = slabbaseon;
                                ob.Rdb_marks = Convert.ToDecimal(marks);
                                ob.Rdb_to_val = Convert.ToInt64(toval);
                                ob.Rdb_brand = brand;
                                ob.Rdb_anal1 = malty;
                                ob.Rdb_anal2 = targetbase;
                                ob.Rdb_anal3 = CombineNo;
                                ob.Rdb_tot_comb_qty = Convert.ToDecimal(TotCombQty);

                                bonus_det.Add(ob);
                            }
                        }
                    }
                    else if (stall != "" && pball == "" || pball == null)
                    {
                        stall = stall.Replace(@"chkst=", string.Empty);
                        string[] words2 = stall.Split('&');

                        if (itemexcel != "")
                        {
                            string[] wordsexcel = itemexcel.Split('&');
                            foreach (var excellist in wordsexcel)
                            {

                                foreach (var stlist in words2)
                                {
                                    ref_bonus_det ob = new ref_bonus_det();
                                    ob.Rbd_cat1 = cat1;
                                    ob.Rbd_cat2 = cat2;
                                    ob.Rbd_cus_cd = custormer;
                                    ob.Rbd_from_val = Convert.ToInt64(fromval);
                                    ob.Rbd_hp_schm = hpscheme;
                                    ob.Rbd_item_cd = excellist;
                                    ob.Rbd_model = model;
                                    ob.Rbd_pay_mode = paymode;
                                    ob.Rbd_pay_sub_tp = stlist;
                                    ob.Rbd_pb = PriceBook;
                                    ob.Rbd_pl = PriceLevel;
                                    ob.Rbd_price_circul = pricecircular;
                                    ob.Rbd_sales_tp = invtype;
                                    ob.Rbd_slab_base = slabbaseon;
                                    ob.Rdb_marks = Convert.ToDecimal(marks);
                                    ob.Rdb_to_val = Convert.ToInt64(toval);
                                    ob.Rdb_brand = brand;
                                    ob.Rdb_anal1 = malty;
                                    ob.Rdb_anal2 = targetbase;
                                    ob.Rdb_anal3 = CombineNo;
                                    ob.Rdb_tot_comb_qty = Convert.ToDecimal(TotCombQty);

                                    bonus_det.Add(ob);
                                }
                            }
                        }
                        else
                        {
                            foreach (var stlist in words2)
                            {
                                ref_bonus_det ob = new ref_bonus_det();
                                ob.Rbd_cat1 = cat1;
                                ob.Rbd_cat2 = cat2;
                                ob.Rbd_cus_cd = custormer;
                                ob.Rbd_from_val = Convert.ToInt64(fromval);
                                ob.Rbd_hp_schm = hpscheme;
                                ob.Rbd_item_cd = itemCode;
                                ob.Rbd_model = model;
                                ob.Rbd_pay_mode = paymode;
                                ob.Rbd_pay_sub_tp = stlist;
                                ob.Rbd_pb = PriceBook;
                                ob.Rbd_pl = PriceLevel;
                                ob.Rbd_price_circul = pricecircular;
                                ob.Rbd_sales_tp = invtype;
                                ob.Rbd_slab_base = slabbaseon;
                                ob.Rdb_marks = Convert.ToDecimal(marks);
                                ob.Rdb_to_val = Convert.ToInt64(toval);
                                ob.Rdb_brand = brand;
                                ob.Rdb_anal1 = malty;
                                ob.Rdb_anal2 = targetbase;
                                ob.Rdb_anal3 = CombineNo;
                                ob.Rdb_tot_comb_qty = Convert.ToDecimal(TotCombQty);

                                bonus_det.Add(ob);
                            }
                        }
                    }
                    else if (stall != "" && pball != "")
                    {
                        stall = stall.Replace(@"chkst=", string.Empty);
                        string[] words2 = stall.Split('&');

                        if (itemexcel != "")
                        {
                            string[] wordsexcel = itemexcel.Split('&');
                            foreach (var excellist in wordsexcel)
                            {
                                foreach (var stlist in words2)
                                {
                                    pball = pball.Replace(@"chkpl=", string.Empty);
                                    string[] words1 = pball.Split('&');
                                    foreach (var pblist in words1)
                                    {
                                        ref_bonus_det ob = new ref_bonus_det();
                                        ob.Rbd_cat1 = cat1;
                                        ob.Rbd_cat2 = cat2;
                                        ob.Rbd_cus_cd = custormer;
                                        ob.Rbd_from_val = Convert.ToInt64(fromval);
                                        ob.Rbd_hp_schm = hpscheme;
                                        ob.Rbd_item_cd = excellist;
                                        ob.Rbd_model = model;
                                        ob.Rbd_pay_mode = paymode;
                                        ob.Rbd_pay_sub_tp = stlist;
                                        ob.Rbd_pb = PriceBook;
                                        ob.Rbd_pl = pblist;
                                        ob.Rbd_price_circul = pricecircular;
                                        ob.Rbd_sales_tp = invtype;
                                        ob.Rbd_slab_base = slabbaseon;
                                        ob.Rdb_marks = Convert.ToDecimal(marks);
                                        ob.Rdb_to_val = Convert.ToInt64(toval);
                                        ob.Rdb_brand = brand;
                                        ob.Rdb_anal1 = malty;
                                        ob.Rdb_anal2 = targetbase;
                                        ob.Rdb_anal3 = CombineNo;
                                        ob.Rdb_tot_comb_qty = Convert.ToDecimal(TotCombQty);

                                        bonus_det.Add(ob);
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var stlist in words2)
                            {
                                pball = pball.Replace(@"chkpl=", string.Empty);
                                string[] words1 = pball.Split('&');
                                foreach (var pblist in words1)
                                {
                                    ref_bonus_det ob = new ref_bonus_det();
                                    ob.Rbd_cat1 = cat1;
                                    ob.Rbd_cat2 = cat2;
                                    ob.Rbd_cus_cd = custormer;
                                    ob.Rbd_from_val = Convert.ToInt64(fromval);
                                    ob.Rbd_hp_schm = hpscheme;
                                    ob.Rbd_item_cd = itemCode;
                                    ob.Rbd_model = model;
                                    ob.Rbd_pay_mode = paymode;
                                    ob.Rbd_pay_sub_tp = stlist;
                                    ob.Rbd_pb = PriceBook;
                                    ob.Rbd_pl = pblist;
                                    ob.Rbd_price_circul = pricecircular;
                                    ob.Rbd_sales_tp = invtype;
                                    ob.Rbd_slab_base = slabbaseon;
                                    ob.Rdb_marks = Convert.ToDecimal(marks);
                                    ob.Rdb_to_val = Convert.ToInt64(toval);
                                    ob.Rdb_brand = brand;
                                    ob.Rdb_anal1 = malty;
                                    ob.Rdb_anal2 = targetbase;
                                    ob.Rdb_anal3 = CombineNo;
                                    ob.Rdb_tot_comb_qty = Convert.ToDecimal(TotCombQty);

                                    bonus_det.Add(ob);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (itemexcel != "")
                        {
                            string[] wordsexcel = itemexcel.Split('&');
                            foreach (var excellist in wordsexcel)
                            {
                                ref_bonus_det ob = new ref_bonus_det();
                                ob.Rbd_cat1 = cat1;
                                ob.Rbd_cat2 = cat2;
                                ob.Rbd_cus_cd = custormer;
                                ob.Rbd_from_val = Convert.ToInt64(fromval);
                                ob.Rbd_hp_schm = hpscheme;
                                ob.Rbd_item_cd = itemCode;
                                ob.Rbd_model = model;
                                ob.Rbd_pay_mode = paymode;
                                ob.Rbd_pay_sub_tp = paymodesub;
                                ob.Rbd_pb = PriceBook;
                                ob.Rbd_pl = PriceLevel;
                                ob.Rbd_price_circul = pricecircular;
                                ob.Rbd_sales_tp = invtype;
                                ob.Rbd_slab_base = slabbaseon;
                                ob.Rdb_marks = Convert.ToDecimal(marks);
                                ob.Rdb_to_val = Convert.ToInt64(toval);
                                ob.Rdb_brand = brand;
                                ob.Rdb_anal1 = malty;
                                ob.Rdb_anal2 = targetbase;
                                ob.Rdb_anal3 = CombineNo;
                                ob.Rdb_tot_comb_qty = Convert.ToDecimal(TotCombQty);

                                bonus_det.Add(ob);
                            }
                        }
                        else
                        {
                            ref_bonus_det ob = new ref_bonus_det();
                            ob.Rbd_cat1 = cat1;
                            ob.Rbd_cat2 = cat2;
                            ob.Rbd_cus_cd = custormer;
                            ob.Rbd_from_val = Convert.ToInt64(fromval);
                            ob.Rbd_hp_schm = hpscheme;
                            ob.Rbd_item_cd = itemCode;
                            ob.Rbd_model = model;
                            ob.Rbd_pay_mode = paymode;
                            ob.Rbd_pay_sub_tp = paymodesub;
                            ob.Rbd_pb = PriceBook;
                            ob.Rbd_pl = PriceLevel;
                            ob.Rbd_price_circul = pricecircular;
                            ob.Rbd_sales_tp = invtype;
                            ob.Rbd_slab_base = slabbaseon;
                            ob.Rdb_marks = Convert.ToDecimal(marks);
                            ob.Rdb_to_val = Convert.ToInt64(toval);
                            ob.Rdb_brand = brand;
                            ob.Rdb_anal1 = malty;
                            ob.Rdb_anal2 = targetbase;
                            ob.Rdb_anal3 = CombineNo;
                            ob.Rdb_tot_comb_qty = Convert.ToDecimal(TotCombQty);

                            bonus_det.Add(ob);
                        }
                    }

                    Session["ref_bonus_det"] = bonus_det;
                }
                else
                {
                    bonus_det = Session["ref_bonus_det"] as List<ref_bonus_det>;

                    string pball = PLALL;
                    string stall = STALL;
                    //if (pball != "")
                    //{
                    //    pball = pball.Replace(@"chkpl=", string.Empty);
                    //    string[] words2 = pball.Split('&');
                    //    foreach (var pblist in words2)
                    //    {

                    //        var count = bonus_det.Where(a => a.Rbd_cat1 == cat1 && a.Rbd_cat2 == cat2 && a.Rbd_cus_cd == custormer && a.Rbd_from_val.ToString() == fromval && a.Rbd_hp_schm == hpscheme && a.Rbd_item_cd == itemCode && a.Rbd_model == model && a.Rbd_pay_mode == paymode && a.Rbd_pay_sub_tp == paymodesub && a.Rbd_pb == PriceBook && a.Rbd_pl == PriceLevel && a.Rbd_price_circul == pricecircular && a.Rbd_sales_tp == invtype && a.Rbd_slab_base == slabbaseon && a.Rdb_marks.ToString() == marks && a.Rdb_to_val.ToString() == toval && a.Rdb_brand == brand).Count();
                    //        if (count > 0)
                    //        {
                    //            return Json(new { success = false, login = true, msg = "Already added this Item Data!!", data = "" }, JsonRequestBehavior.AllowGet);
                    //        }
                    //        else
                    //        {
                    //            ref_bonus_det ob = new ref_bonus_det();
                    //            ob.Rbd_cat1 = cat1;
                    //            ob.Rbd_cat2 = cat2;
                    //            ob.Rbd_cus_cd = custormer;
                    //            ob.Rbd_from_val = Convert.ToInt64(fromval);
                    //            ob.Rbd_hp_schm = hpscheme;
                    //            ob.Rbd_item_cd = itemCode;
                    //            ob.Rbd_model = model;
                    //            ob.Rbd_pay_mode = paymode;
                    //            ob.Rbd_pay_sub_tp = paymodesub;
                    //            ob.Rbd_pb = PriceBook;
                    //            ob.Rbd_pl = pblist;
                    //            ob.Rbd_price_circul = pricecircular;
                    //            ob.Rbd_sales_tp = invtype;
                    //            ob.Rbd_slab_base = slabbaseon;
                    //            ob.Rdb_marks = Convert.ToDecimal(marks);
                    //            ob.Rdb_to_val = Convert.ToInt64(toval);
                    //            ob.Rdb_brand = brand;
                    //            ob.Rdb_anal1 = malty;
                    //            ob.Rdb_anal2 = targetbase;
                    //            ob.Rdb_anal3 = CombineNo;
                    //            ob.Rdb_tot_comb_qty = Convert.ToDecimal(TotCombQty);
                    //            bonus_det.Add(ob);
                    //        }

                    //    }
                    //}
                    if (itemexcel != "")
                    {
                        string[] wordsexcel = itemexcel.Split('&');
                        foreach (var excellist in wordsexcel)
                        {
                            if (pball != "" && stall == "" || stall == null)
                            {
                                pball = pball.Replace(@"chkpl=", string.Empty);
                                string[] words2 = pball.Split('&');
                                foreach (var pblist in words2)
                                {
                                    var count = bonus_det.Where(a => a.Rbd_cat1 == cat1 && a.Rbd_cat2 == cat2 && a.Rbd_cus_cd == custormer && a.Rbd_from_val.ToString() == fromval && a.Rbd_hp_schm == hpscheme && a.Rbd_item_cd == itemCode && a.Rbd_model == model && a.Rbd_pay_mode == paymode && a.Rbd_pay_sub_tp == paymodesub && a.Rbd_pb == PriceBook && a.Rbd_pl == pblist && a.Rbd_price_circul == pricecircular && a.Rbd_sales_tp == invtype && a.Rbd_slab_base == slabbaseon && a.Rdb_marks.ToString() == marks && a.Rdb_to_val.ToString() == toval && a.Rdb_brand == brand).Count();
                                    if (count > 0)
                                    {
                                        return Json(new { success = false, login = true, msg = "Already added this Item Data!!", data = "" }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        ref_bonus_det ob = new ref_bonus_det();
                                        ob.Rbd_cat1 = cat1;
                                        ob.Rbd_cat2 = cat2;
                                        ob.Rbd_cus_cd = custormer;
                                        ob.Rbd_from_val = Convert.ToInt64(fromval);
                                        ob.Rbd_hp_schm = hpscheme;
                                        ob.Rbd_item_cd = excellist;
                                        ob.Rbd_model = model;
                                        ob.Rbd_pay_mode = paymode;
                                        ob.Rbd_pay_sub_tp = paymodesub;
                                        ob.Rbd_pb = PriceBook;
                                        ob.Rbd_pl = pblist;
                                        ob.Rbd_price_circul = pricecircular;
                                        ob.Rbd_sales_tp = invtype;
                                        ob.Rbd_slab_base = slabbaseon;
                                        ob.Rdb_marks = Convert.ToDecimal(marks);
                                        ob.Rdb_to_val = Convert.ToInt64(toval);
                                        ob.Rdb_brand = brand;
                                        ob.Rdb_anal1 = malty;
                                        ob.Rdb_anal2 = targetbase;
                                        ob.Rdb_anal3 = CombineNo;
                                        ob.Rdb_tot_comb_qty = Convert.ToDecimal(TotCombQty);

                                        bonus_det.Add(ob);
                                    }
                                }
                            }
                            else if (stall != "" && pball == "" || pball == null)
                            {
                                stall = stall.Replace(@"chkst=", string.Empty);
                                string[] words2 = stall.Split('&');
                                foreach (var stlist in words2)
                                {
                                    var count = bonus_det.Where(a => a.Rbd_cat1 == cat1 && a.Rbd_cat2 == cat2 && a.Rbd_cus_cd == custormer && a.Rbd_from_val.ToString() == fromval && a.Rbd_hp_schm == hpscheme && a.Rbd_item_cd == itemCode && a.Rbd_model == model && a.Rbd_pay_mode == paymode && a.Rbd_pay_sub_tp == stlist && a.Rbd_pb == PriceBook && a.Rbd_pl == PriceLevel && a.Rbd_price_circul == pricecircular && a.Rbd_sales_tp == invtype && a.Rbd_slab_base == slabbaseon && a.Rdb_marks.ToString() == marks && a.Rdb_to_val.ToString() == toval && a.Rdb_brand == brand).Count();
                                    if (count > 0)
                                    {
                                        return Json(new { success = false, login = true, msg = "Already added this Item Data!!", data = "" }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        ref_bonus_det ob = new ref_bonus_det();
                                        ob.Rbd_cat1 = cat1;
                                        ob.Rbd_cat2 = cat2;
                                        ob.Rbd_cus_cd = custormer;
                                        ob.Rbd_from_val = Convert.ToInt64(fromval);
                                        ob.Rbd_hp_schm = hpscheme;
                                        ob.Rbd_item_cd = excellist;
                                        ob.Rbd_model = model;
                                        ob.Rbd_pay_mode = paymode;
                                        ob.Rbd_pay_sub_tp = stlist;
                                        ob.Rbd_pb = PriceBook;
                                        ob.Rbd_pl = PriceLevel;
                                        ob.Rbd_price_circul = pricecircular;
                                        ob.Rbd_sales_tp = invtype;
                                        ob.Rbd_slab_base = slabbaseon;
                                        ob.Rdb_marks = Convert.ToDecimal(marks);
                                        ob.Rdb_to_val = Convert.ToInt64(toval);
                                        ob.Rdb_brand = brand;
                                        ob.Rdb_anal1 = malty;
                                        ob.Rdb_anal2 = targetbase;
                                        ob.Rdb_anal3 = CombineNo;
                                        ob.Rdb_tot_comb_qty = Convert.ToDecimal(TotCombQty);

                                        bonus_det.Add(ob);
                                    }
                                }
                            }
                            else if (stall != "" && pball != "")
                            {
                                stall = stall.Replace(@"chkst=", string.Empty);
                                string[] words2 = stall.Split('&');
                                foreach (var stlist in words2)
                                {
                                    pball = pball.Replace(@"chkpl=", string.Empty);
                                    string[] words1 = pball.Split('&');
                                    foreach (var pblist in words1)
                                    {
                                        var count = bonus_det.Where(a => a.Rbd_cat1 == cat1 && a.Rbd_cat2 == cat2 && a.Rbd_cus_cd == custormer && a.Rbd_from_val.ToString() == fromval && a.Rbd_hp_schm == hpscheme && a.Rbd_item_cd == itemCode && a.Rbd_model == model && a.Rbd_pay_mode == paymode && a.Rbd_pay_sub_tp == stlist && a.Rbd_pb == PriceBook && a.Rbd_pl == pblist && a.Rbd_price_circul == pricecircular && a.Rbd_sales_tp == invtype && a.Rbd_slab_base == slabbaseon && a.Rdb_marks.ToString() == marks && a.Rdb_to_val.ToString() == toval && a.Rdb_brand == brand).Count();
                                        if (count > 0)
                                        {
                                            return Json(new { success = false, login = true, msg = "Already added this Item Data!!", data = "" }, JsonRequestBehavior.AllowGet);
                                        }
                                        else
                                        {
                                            ref_bonus_det ob = new ref_bonus_det();
                                            ob.Rbd_cat1 = cat1;
                                            ob.Rbd_cat2 = cat2;
                                            ob.Rbd_cus_cd = custormer;
                                            ob.Rbd_from_val = Convert.ToInt64(fromval);
                                            ob.Rbd_hp_schm = hpscheme;
                                            ob.Rbd_item_cd = excellist;
                                            ob.Rbd_model = model;
                                            ob.Rbd_pay_mode = paymode;
                                            ob.Rbd_pay_sub_tp = stlist;
                                            ob.Rbd_pb = PriceBook;
                                            ob.Rbd_pl = pblist;
                                            ob.Rbd_price_circul = pricecircular;
                                            ob.Rbd_sales_tp = invtype;
                                            ob.Rbd_slab_base = slabbaseon;
                                            ob.Rdb_marks = Convert.ToDecimal(marks);
                                            ob.Rdb_to_val = Convert.ToInt64(toval);
                                            ob.Rdb_brand = brand;
                                            ob.Rdb_anal1 = malty;
                                            ob.Rdb_anal2 = targetbase;
                                            ob.Rdb_anal3 = CombineNo;
                                            ob.Rdb_tot_comb_qty = Convert.ToDecimal(TotCombQty);

                                            bonus_det.Add(ob);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                var count = bonus_det.Where(a => a.Rbd_cat1 == cat1 && a.Rbd_cat2 == cat2 && a.Rbd_cus_cd == custormer && a.Rbd_from_val.ToString() == fromval && a.Rbd_hp_schm == hpscheme && a.Rbd_item_cd == itemCode && a.Rbd_model == model && a.Rbd_pay_mode == paymode && a.Rbd_pay_sub_tp == paymodesub && a.Rbd_pb == PriceBook && a.Rbd_pl == PriceLevel && a.Rbd_price_circul == pricecircular && a.Rbd_sales_tp == invtype && a.Rbd_slab_base == slabbaseon && a.Rdb_marks.ToString() == marks && a.Rdb_to_val.ToString() == toval && a.Rdb_brand == brand).Count();
                                if (count > 0)
                                {
                                    return Json(new { success = false, login = true, msg = "Already added this Item Data!!", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    ref_bonus_det ob = new ref_bonus_det();
                                    ob.Rbd_cat1 = cat1;
                                    ob.Rbd_cat2 = cat2;
                                    ob.Rbd_cus_cd = custormer;
                                    ob.Rbd_from_val = Convert.ToInt32(fromval);
                                    ob.Rbd_hp_schm = hpscheme;
                                    ob.Rbd_item_cd = excellist;
                                    ob.Rbd_model = model;
                                    ob.Rbd_pay_mode = paymode;
                                    ob.Rbd_pay_sub_tp = paymodesub;
                                    ob.Rbd_pb = PriceBook;
                                    ob.Rbd_pl = PriceLevel;
                                    ob.Rbd_price_circul = pricecircular;
                                    ob.Rbd_sales_tp = invtype;
                                    ob.Rbd_slab_base = slabbaseon;
                                    ob.Rdb_marks = Convert.ToDecimal(marks);
                                    ob.Rdb_to_val = Convert.ToInt32(toval);
                                    ob.Rdb_brand = brand;
                                    ob.Rdb_anal1 = malty;
                                    ob.Rdb_anal2 = targetbase;
                                    ob.Rdb_anal3 = CombineNo;
                                    ob.Rdb_tot_comb_qty = Convert.ToDecimal(TotCombQty);
                                    bonus_det.Add(ob);
                                }
                            }
                            Session["ref_bonus_det"] = bonus_det;
                        }
                    }
                    else
                    {
                        if (pball != "" && stall == "" || stall == null)
                        {
                            pball = pball.Replace(@"chkpl=", string.Empty);
                            string[] words2 = pball.Split('&');
                            foreach (var pblist in words2)
                            {
                                var count = bonus_det.Where(a => a.Rbd_cat1 == cat1 && a.Rbd_cat2 == cat2 && a.Rbd_cus_cd == custormer && a.Rbd_from_val.ToString() == fromval && a.Rbd_hp_schm == hpscheme && a.Rbd_item_cd == itemCode && a.Rbd_model == model && a.Rbd_pay_mode == paymode && a.Rbd_pay_sub_tp == paymodesub && a.Rbd_pb == PriceBook && a.Rbd_pl == pblist && a.Rbd_price_circul == pricecircular && a.Rbd_sales_tp == invtype && a.Rbd_slab_base == slabbaseon && a.Rdb_marks.ToString() == marks && a.Rdb_to_val.ToString() == toval && a.Rdb_brand == brand).Count();
                                if (count > 0)
                                {
                                    return Json(new { success = false, login = true, msg = "Already added this Item Data!!", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    ref_bonus_det ob = new ref_bonus_det();
                                    ob.Rbd_cat1 = cat1;
                                    ob.Rbd_cat2 = cat2;
                                    ob.Rbd_cus_cd = custormer;
                                    ob.Rbd_from_val = Convert.ToInt64(fromval);
                                    ob.Rbd_hp_schm = hpscheme;
                                    ob.Rbd_item_cd = itemCode;
                                    ob.Rbd_model = model;
                                    ob.Rbd_pay_mode = paymode;
                                    ob.Rbd_pay_sub_tp = paymodesub;
                                    ob.Rbd_pb = PriceBook;
                                    ob.Rbd_pl = pblist;
                                    ob.Rbd_price_circul = pricecircular;
                                    ob.Rbd_sales_tp = invtype;
                                    ob.Rbd_slab_base = slabbaseon;
                                    ob.Rdb_marks = Convert.ToDecimal(marks);
                                    ob.Rdb_to_val = Convert.ToInt64(toval);
                                    ob.Rdb_brand = brand;
                                    ob.Rdb_anal1 = malty;
                                    ob.Rdb_anal2 = targetbase;
                                    ob.Rdb_anal3 = CombineNo;
                                    ob.Rdb_tot_comb_qty = Convert.ToDecimal(TotCombQty);

                                    bonus_det.Add(ob);
                                }
                            }
                        }
                        else if (stall != "" && pball == "" || pball == null)
                        {
                            stall = stall.Replace(@"chkst=", string.Empty);
                            string[] words2 = stall.Split('&');
                            foreach (var stlist in words2)
                            {
                                var count = bonus_det.Where(a => a.Rbd_cat1 == cat1 && a.Rbd_cat2 == cat2 && a.Rbd_cus_cd == custormer && a.Rbd_from_val.ToString() == fromval && a.Rbd_hp_schm == hpscheme && a.Rbd_item_cd == itemCode && a.Rbd_model == model && a.Rbd_pay_mode == paymode && a.Rbd_pay_sub_tp == stlist && a.Rbd_pb == PriceBook && a.Rbd_pl == PriceLevel && a.Rbd_price_circul == pricecircular && a.Rbd_sales_tp == invtype && a.Rbd_slab_base == slabbaseon && a.Rdb_marks.ToString() == marks && a.Rdb_to_val.ToString() == toval && a.Rdb_brand == brand).Count();
                                if (count > 0)
                                {
                                    return Json(new { success = false, login = true, msg = "Already added this Item Data!!", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    ref_bonus_det ob = new ref_bonus_det();
                                    ob.Rbd_cat1 = cat1;
                                    ob.Rbd_cat2 = cat2;
                                    ob.Rbd_cus_cd = custormer;
                                    ob.Rbd_from_val = Convert.ToInt64(fromval);
                                    ob.Rbd_hp_schm = hpscheme;
                                    ob.Rbd_item_cd = itemCode;
                                    ob.Rbd_model = model;
                                    ob.Rbd_pay_mode = paymode;
                                    ob.Rbd_pay_sub_tp = stlist;
                                    ob.Rbd_pb = PriceBook;
                                    ob.Rbd_pl = PriceLevel;
                                    ob.Rbd_price_circul = pricecircular;
                                    ob.Rbd_sales_tp = invtype;
                                    ob.Rbd_slab_base = slabbaseon;
                                    ob.Rdb_marks = Convert.ToDecimal(marks);
                                    ob.Rdb_to_val = Convert.ToInt64(toval);
                                    ob.Rdb_brand = brand;
                                    ob.Rdb_anal1 = malty;
                                    ob.Rdb_anal2 = targetbase;
                                    ob.Rdb_anal3 = CombineNo;
                                    ob.Rdb_tot_comb_qty = Convert.ToDecimal(TotCombQty);

                                    bonus_det.Add(ob);
                                }
                            }
                        }
                        else if (stall != "" && pball != "")
                        {
                            stall = stall.Replace(@"chkst=", string.Empty);
                            string[] words2 = stall.Split('&');
                            foreach (var stlist in words2)
                            {
                                pball = pball.Replace(@"chkpl=", string.Empty);
                                string[] words1 = pball.Split('&');
                                foreach (var pblist in words1)
                                {
                                    var count = bonus_det.Where(a => a.Rbd_cat1 == cat1 && a.Rbd_cat2 == cat2 && a.Rbd_cus_cd == custormer && a.Rbd_from_val.ToString() == fromval && a.Rbd_hp_schm == hpscheme && a.Rbd_item_cd == itemCode && a.Rbd_model == model && a.Rbd_pay_mode == paymode && a.Rbd_pay_sub_tp == stlist && a.Rbd_pb == PriceBook && a.Rbd_pl == pblist && a.Rbd_price_circul == pricecircular && a.Rbd_sales_tp == invtype && a.Rbd_slab_base == slabbaseon && a.Rdb_marks.ToString() == marks && a.Rdb_to_val.ToString() == toval && a.Rdb_brand == brand).Count();
                                    if (count > 0)
                                    {
                                        return Json(new { success = false, login = true, msg = "Already added this Item Data!!", data = "" }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        ref_bonus_det ob = new ref_bonus_det();
                                        ob.Rbd_cat1 = cat1;
                                        ob.Rbd_cat2 = cat2;
                                        ob.Rbd_cus_cd = custormer;
                                        ob.Rbd_from_val = Convert.ToInt64(fromval);
                                        ob.Rbd_hp_schm = hpscheme;
                                        ob.Rbd_item_cd = itemCode;
                                        ob.Rbd_model = model;
                                        ob.Rbd_pay_mode = paymode;
                                        ob.Rbd_pay_sub_tp = stlist;
                                        ob.Rbd_pb = PriceBook;
                                        ob.Rbd_pl = pblist;
                                        ob.Rbd_price_circul = pricecircular;
                                        ob.Rbd_sales_tp = invtype;
                                        ob.Rbd_slab_base = slabbaseon;
                                        ob.Rdb_marks = Convert.ToDecimal(marks);
                                        ob.Rdb_to_val = Convert.ToInt64(toval);
                                        ob.Rdb_brand = brand;
                                        ob.Rdb_anal1 = malty;
                                        ob.Rdb_anal2 = targetbase;
                                        ob.Rdb_anal3 = CombineNo;
                                        ob.Rdb_tot_comb_qty = Convert.ToDecimal(TotCombQty);

                                        bonus_det.Add(ob);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var count = bonus_det.Where(a => a.Rbd_cat1 == cat1 && a.Rbd_cat2 == cat2 && a.Rbd_cus_cd == custormer && a.Rbd_from_val.ToString() == fromval && a.Rbd_hp_schm == hpscheme && a.Rbd_item_cd == itemCode && a.Rbd_model == model && a.Rbd_pay_mode == paymode && a.Rbd_pay_sub_tp == paymodesub && a.Rbd_pb == PriceBook && a.Rbd_pl == PriceLevel && a.Rbd_price_circul == pricecircular && a.Rbd_sales_tp == invtype && a.Rbd_slab_base == slabbaseon && a.Rdb_marks.ToString() == marks && a.Rdb_to_val.ToString() == toval && a.Rdb_brand == brand).Count();
                            if (count > 0)
                            {
                                return Json(new { success = false, login = true, msg = "Already added this Item Data!!", data = "" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                ref_bonus_det ob = new ref_bonus_det();
                                ob.Rbd_cat1 = cat1;
                                ob.Rbd_cat2 = cat2;
                                ob.Rbd_cus_cd = custormer;
                                ob.Rbd_from_val = Convert.ToInt32(fromval);
                                ob.Rbd_hp_schm = hpscheme;
                                ob.Rbd_item_cd = itemCode;
                                ob.Rbd_model = model;
                                ob.Rbd_pay_mode = paymode;
                                ob.Rbd_pay_sub_tp = paymodesub;
                                ob.Rbd_pb = PriceBook;
                                ob.Rbd_pl = PriceLevel;
                                ob.Rbd_price_circul = pricecircular;
                                ob.Rbd_sales_tp = invtype;
                                ob.Rbd_slab_base = slabbaseon;
                                ob.Rdb_marks = Convert.ToDecimal(marks);
                                ob.Rdb_to_val = Convert.ToInt32(toval);
                                ob.Rdb_brand = brand;
                                ob.Rdb_anal1 = malty;
                                ob.Rdb_anal2 = targetbase;
                                ob.Rdb_anal3 = CombineNo;
                                ob.Rdb_tot_comb_qty = Convert.ToDecimal(TotCombQty);
                                bonus_det.Add(ob);
                            }
                        }
                        Session["ref_bonus_det"] = bonus_det;
                    }
                }
                if (bonus_det.Count > 0)
                {
                    return Json(new { success = true, login = true, data = bonus_det }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult RemoveItemDet(string itemCode, string cat1, string cat2, string brand, string model, string PriceBook, string PriceLevel, string pricecircular, string invtype, string paymode, string paymodesub, string slabbaseon, string custormer, string hpscheme, string fromval, string toval, string marks)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                if (Session["ref_bonus_det"] != null)
                {
                    bonus_det = (List<ref_bonus_det>)Session["ref_bonus_det"];
                }
                else
                {
                    bonus_det = new List<ref_bonus_det>();

                }
                //var itemToRemove = bonus_det.First(a => a.Rbd_cat1 == cat1 && a.Rbd_cat2 == cat2 && a.Rbd_cus_cd == custormer && a.Rbd_from_val.ToString() == fromval && a.Rbd_hp_schm == hpscheme && a.Rbd_item_cd == itemCode && a.Rbd_model == model && a.Rbd_pay_mode == paymode && a.Rdb_anal2 == paymodesub && a.Rbd_pb == PriceBook && a.Rbd_pl == PriceLevel && a.Rbd_price_circul == pricecircular && a.Rbd_sales_tp == invtype && a.Rbd_slab_base == slabbaseon && a.Rdb_marks.ToString() == marks && a.Rdb_to_val.ToString() == toval && a.Rdb_brand == brand);
                var itemToRemove = bonus_det.First(a => a.Rbd_cat1 == cat1 && a.Rbd_cat2 == cat2 && a.Rbd_cus_cd == custormer && a.Rbd_from_val.ToString() == fromval && a.Rbd_hp_schm == hpscheme && a.Rbd_item_cd == itemCode && a.Rbd_model == model && a.Rbd_pay_mode == paymode && a.Rbd_pay_sub_tp == paymodesub && a.Rbd_pb == PriceBook && a.Rbd_pl == PriceLevel && a.Rbd_price_circul == pricecircular && a.Rbd_sales_tp == invtype && a.Rbd_slab_base == slabbaseon && a.Rdb_marks.ToString() == marks && a.Rdb_to_val.ToString() == toval && a.Rdb_brand == brand);
                bonus_det.Remove(itemToRemove);
                Session["ref_bonus_det"] = bonus_det;
                return Json(new { success = true, login = true, data = bonus_det }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveProductBonusDefinition(string Date, string Fsalelimit, string Areas, string Outslimit,
            string Outdatelimit, string CalcType, string SalesMethod, string Active, string doc, string circ_cd, 
            string iscommitem, string combinetot, string maxdiscount, string withpb, string remarks, string withpromo
            , string validfrom, string Option)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string err = "";
                //Auto Number
                MasterAutoNumber mastAutoNo = new MasterAutoNumber();
                mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                mastAutoNo.Aut_cate_tp = "COM";
                mastAutoNo.Aut_direction = 1;
                mastAutoNo.Aut_moduleid = "BON";
                mastAutoNo.Aut_start_char = "BON";
                mastAutoNo.Aut_year = DateTime.Now.Year;
                //hdr
                REF_BONUS_HDR bonhdr = new REF_BONUS_HDR();
                if (Areas == "")
                {
                    Areas = "0";
                }
                if (Fsalelimit=="")
                {
                    Fsalelimit = "0";
                }
                if (Outdatelimit=="")
                {
                    Outdatelimit = "0";
                }
                if (Outslimit == "")
                {
                    Outslimit = "0";
                }
                bonhdr.Rbh_areas_lmt = Convert.ToInt32(Areas);
                bonhdr.Rbh_calc_methd = CalcType;
                bonhdr.Rbh_create_by = userId;
                bonhdr.Rbh_create_dt = DateTime.Now.Date;
                bonhdr.Rbh_date = Convert.ToDateTime(Date);
                bonhdr.Rbh_fw_sale_lmt = Convert.ToInt32(Fsalelimit);
                bonhdr.Rbh_mod_by = userId;
                bonhdr.Rbh_mod_dt = DateTime.Now.Date;
                bonhdr.Rbh_outs_dt_lmt = Convert.ToInt32(Outdatelimit);
                bonhdr.Rbh_outs_lmt = Convert.ToInt32(Outslimit);
                bonhdr.Rbh_sales_methd = SalesMethod;
                bonhdr.Rbh_cre_session_id = Session["SessionID"].ToString();
                bonhdr.Rbh_mod_session_id = Session["SessionID"].ToString();
                bonhdr.Rbh_doc_no = doc;
                bonhdr.Rbh_com = company;
                bonhdr.Rbh_act = Convert.ToInt32(Active);
                bonhdr.Rbh_man_circucd = circ_cd;
                bonhdr.Rbh_anal2 = iscommitem;
                bonhdr.Rbh_anal3 = combinetot;
                bonhdr.Rbh_disc_con = Convert.ToInt32(maxdiscount);
                bonhdr.Rbh_pb_cond = Convert.ToInt32(withpb);
                bonhdr.Rbh_anal4 = remarks;
                bonhdr.Rbh_anal5 = withpromo;
                bonhdr.Rbh_rerept_opt = Option;
                bonhdr.Rbh_valid_from = Convert.ToDateTime(validfrom);
                bonus_det = Session["ref_bonus_det"] as List<ref_bonus_det>;
                bonus_loc = (List<ref_bonus_loc>)Session["ref_bonus_loc"];

                int effect = CHNLSVC.Finance.SaveProductBonus(bonhdr, bonus_det, bonus_loc, mastAutoNo, out err);
                if (effect == 1 || effect == 0)
                {
                    return Json(new { success = true, login = true, msg = "Successfull Saved Doc : " + err }, JsonRequestBehavior.AllowGet);
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
        public JsonResult ClearAll()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["ref_bonus_loc"] = null;
                Session["REF_BONUS_HDR"] = null;
                Session["ref_bonus_det"] = null;
                return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, msg = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult LoadDetails(string BonusCode)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            BonusCode = BonusCode.Trim();
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<REF_BONUS_HDR> hdrdata = CHNLSVC.Finance.GetBonusHDR(company, BonusCode);
                List<ref_bonus_det> itemdetails = CHNLSVC.Finance.GetBonusDetails(BonusCode);
                List<ref_bonus_loc> Locdetails = CHNLSVC.Finance.GetBonusLoc(BonusCode);
                if (hdrdata == null)
                {
                    return Json(new { success = true, login = true, data = "", msg = "Invalid Doc" }, JsonRequestBehavior.AllowGet);
                }
                //set item data
                if (itemdetails != null)
                {

                    Session["ref_bonus_det"] = itemdetails;
                }
                else
                {
                    itemdetails = new List<ref_bonus_det>();
                }
                //set loc data
                if (Locdetails != null)
                {
                    Session["ref_bonus_loc"] = Locdetails;
                }
                else
                {
                    Locdetails = new List<ref_bonus_loc>();
                }
                return Json(new { success = true, login = true, hdrdata = hdrdata, itemdetails = itemdetails, Locdetails = Locdetails }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        // Udesh 12-Oct-2018
        public JsonResult PrintDetails(string BonusCode)
        {
            string err = "";

            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (BonusCode.Trim().Length > 0)
                    {
                        string path = CHNLSVC.Finance.GetProductBonusExcel(company, userId, BonusCode, out err);

                        _copytoLocal(path);
                        string pathnew = "/Temp/" + Session["UserID"].ToString() + ".xlsx";
                        return Json(new { login = true, success = true, number = 1, urlpath = pathnew }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, type = "err", msg = "Please enter bouns code" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false, data = "Please loggin" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, type = "err", msg = err }, JsonRequestBehavior.AllowGet);
            }
        }

        private void _copytoLocal(string _filePath)
        {
            string filenamenew = Session["UserID"].ToString();
            string name = _filePath;
            FileInfo file = new FileInfo(name);
            if (file.Exists)
            {
                string targetFileName = Server.MapPath("~\\Temp\\") + filenamenew + ".xlsx";
                System.IO.File.Copy(@"" + _filePath, targetFileName, true);
            }
            else
            {
                return;
            }
        }

        public JsonResult LoadSalesSubType()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    DataTable dt = CHNLSVC.General.Get_sar_price_type("");

                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ComboBoxObject o2 = new ComboBoxObject();
                        o2.Text = "Select";
                        o2.Value = "";
                        oList.Add(o2);
                        int i = 0;
                        foreach (var list in dt.Rows)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = dt.Rows[i]["sarpt_cd"].ToString();
                            o1.Value = dt.Rows[i]["sarpt_cd"].ToString();
                            oList.Add(o1);
                            i++;
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
        [HttpPost]
        public JsonResult BindExceldata(HttpPostedFileBase uploadedFile)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                if (Request != null)
                {
                    HttpPostedFileBase file = Request.Files["UploadedFile"];
                    var PostedFile = Request.Files[0];
                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName) && (file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || file.ContentType == "application/vnd.ms-excel" || file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.template" || file.ContentType == "application/vnd.ms-excel.sheet.macroEnabled.12"))
                    {
                        string fileName = file.FileName;
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;
                            List<ref_bonus_det> _lst = new List<ref_bonus_det>();
                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                string _item = "";
                                if (workSheet.Cells[rowIterator, 1].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    _item = workSheet.Cells[rowIterator, 1].Value.ToString();

                                }

                                string searchVal = _item.Trim();
                                List<ITEM_SEARCH> documents = CHNLSVC.Dashboard.getItems("1", "10", "Code", searchVal, company);
                                if (documents.Count > 0)
                                {
                                    //decimal totalDoc = Math.Ceiling(Convert.ToInt32(documents[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                                    //return Json(new { success = true, login = true, data = documents, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Invalid Item Code :" + _item + " ", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                               

                                if (Session["ExcelItem"] == null)
                                {

                                    ref_bonus_det ob = new ref_bonus_det();
                                    ob.Rbd_item_cd = _item;
                                    _lst.Add(ob);
                                    Session["ExcelItem"] = _lst;
                                    Session["StrExcelItem"] = _item;
                                }
                                else
                                {
                                    _lst = Session["ExcelItem"] as List<ref_bonus_det>;
                                    var count = _lst.Where(a => a.Rbd_item_cd.ToString() == _item).Count();
                                    if (count > 0)
                                    {
                                        return Json(new { success = false, login = true, msg = "Already added this  Item!! " + _item, data = "" }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        ref_bonus_det ob = new ref_bonus_det();
                                        ob.Rbd_item_cd = _item;
                                        _lst.Add(ob);
                                        Session["ExcelItem"] = _lst;
                                        Session["StrExcelItem"] = Session["StrExcelItem"] +"&"+ _item;
                                    }
                                }
                            }
                            return Json(new { success = true, login = true, list = _lst }, JsonRequestBehavior.AllowGet);
                        }


                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Cant Find Excel", data = "" }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(new { success = false, login = true, msg = "Cant Find Excel", data = "" }, JsonRequestBehavior.AllowGet);
                }


            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult BindExceldata2(HttpPostedFileBase uploadedFile)
        {
            List<ExcelUploadErr> excelUploadErr = new List<ExcelUploadErr>();
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                if (Request != null)
                {
                    HttpPostedFileBase file = Request.Files["UploadedFile"];
                    var PostedFile = Request.Files[0];
                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName) && (file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || file.ContentType == "application/vnd.ms-excel" || file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.template" || file.ContentType == "application/vnd.ms-excel.sheet.macroEnabled.12"))
                    {
                        string fileName = file.FileName;
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;
                            List<ref_bonus_det> _lst = new List<ref_bonus_det>();
                            excelUploadErr = new List<ExcelUploadErr>();
                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                ExcelUploadErr exUpErr = new ExcelUploadErr();
                                ref_bonus_det _ob = new ref_bonus_det();
                                DataTable dt = null;
                                if (workSheet.Cells[rowIterator, 1].Value != null)
                                {                                    
                                    _ob.Rbd_item_cd = workSheet.Cells[rowIterator, 1].Value.ToString();
                                    List<ITEM_SEARCH> documents = CHNLSVC.Dashboard.getItems("1", "10", "Code",_ob.Rbd_item_cd, company);
                                    if(documents==null)
                                    {
                                        documents = new List<ITEM_SEARCH>();
                                    }
                                    documents = documents.Where(x => x.srtp_cd.Equals(_ob.Rbd_item_cd)).ToList();
                                    if (documents.Count <= 0)
                                    {
                                        exUpErr = new ExcelUploadErr(rowIterator, _ob.Rbd_item_cd, "Invalid item Code");
                                        excelUploadErr.Add(exUpErr);
                                    }
                                }
                                else
                                {
                                    _ob.Rbd_item_cd = "";
                                }
                                if (workSheet.Cells[rowIterator, 2].Value != null)
                                {
                                    _ob.Rbd_cat1 = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    List<MAIN_CAT_SEARCH> documents = CHNLSVC.Dashboard.getMainCategory("1", "10", "Code", _ob.Rbd_cat1, company);
                                    if(documents==null)
                                    {
                                        documents = new List<MAIN_CAT_SEARCH>();
                                    }
                                    documents = documents.Where(x => x.main_cat_cd.Equals(_ob.Rbd_cat1)).ToList();
                                    if (documents.Count <= 0)
                                    {
                                        exUpErr = new ExcelUploadErr(rowIterator, _ob.Rbd_cat1, "Invalid Category 01");
                                        excelUploadErr.Add(exUpErr);
                                    }
                                }
                                else
                                {
                                    _ob.Rbd_cat1 = "";
                                }
                                if (workSheet.Cells[rowIterator, 3].Value != null)
                                {
                                    _ob.Rbd_cat2 = workSheet.Cells[rowIterator, 3].Value.ToString();
                                    List<MAIN_CAT2_SEARCH> documents = CHNLSVC.Dashboard.getCategory2("1", "10", "Code", _ob.Rbd_cat2, company);
                                    if(documents==null)
                                    {
                                        documents = new List<MAIN_CAT2_SEARCH>();
                                    }
                                    documents = documents.Where(x => x.cat2_cd.Equals(_ob.Rbd_cat2)).ToList();
                                    if (documents.Count <= 0)
                                    {
                                        exUpErr = new ExcelUploadErr(rowIterator, _ob.Rbd_cat2, "Invalid Category 02");
                                        excelUploadErr.Add(exUpErr);
                                    }

                                }
                                else
                                {
                                    _ob.Rbd_cat2 = "";
                                }
                                if (workSheet.Cells[rowIterator, 4].Value != null)
                                {                                    
                                    _ob.Rbd_model = workSheet.Cells[rowIterator, 4].Value.ToString();
                                     List<ITEM_MODEL_SEARCH> documents = CHNLSVC.Dashboard.getItemModel("1", "10", "Code", _ob.Rbd_model, company);
                                     if (documents==null)
                                     {
                                         documents = new List<ITEM_MODEL_SEARCH>();
                                     }
                                     documents = documents.Where(x => x.mm_cd.Equals(_ob.Rbd_model)).ToList();
                                     if (documents.Count <= 0)
                                     {
                                         exUpErr = new ExcelUploadErr(rowIterator, _ob.Rbd_model, "Invalid model");
                                         excelUploadErr.Add(exUpErr);
                                     }
                                }
                                else
                                {
                                    _ob.Rbd_model = "";
                                }
                                if (workSheet.Cells[rowIterator, 5].Value != null)
                                {
                                    _ob.Rdb_brand = workSheet.Cells[rowIterator, 5].Value.ToString();
                                    List<ITEM_BRAND_SEARCH> documents = CHNLSVC.Dashboard.getItemBrands("1", "10", "Code", _ob.Rdb_brand, company);
                                    if (documents==null)
                                    {
                                        documents = new List<ITEM_BRAND_SEARCH>();
                                    }
                                    documents = documents.Where(x => x.mb_cd.Equals(_ob.Rdb_brand)).ToList();
                                    if (documents.Count <= 0)
                                    {
                                        exUpErr = new ExcelUploadErr(rowIterator, _ob.Rdb_brand, "Invalid brand");
                                        excelUploadErr.Add(exUpErr);
                                    }
                                }
                                else
                                {
                                    _ob.Rdb_brand = "";
                                }
                                if (workSheet.Cells[rowIterator, 6].Value != null)
                                {
                                    _ob.Rbd_pb = workSheet.Cells[rowIterator, 6].Value.ToString();
                                    List<PriceBookRef> pricebook = CHNLSVC.Sales.GetPriceBooklist(company);
                                    if (pricebook==null)
                                    {
                                        pricebook = new List<PriceBookRef>();
                                    }
                                    pricebook = pricebook.Where(x => x.Sapb_pb.Equals(_ob.Rbd_pb)).ToList();
                                    if (pricebook.Count <= 0)
                                    {
                                        exUpErr = new ExcelUploadErr(rowIterator, _ob.Rbd_pb, "Invalid pricebook");
                                        excelUploadErr.Add(exUpErr);
                                    }

                                }
                                else
                                {
                                    _ob.Rbd_pb = "";
                                }
                                if (workSheet.Cells[rowIterator, 7].Value != null)
                                {
                                    if (workSheet.Cells[rowIterator, 6].Value != null)
                                    {
                                        _ob.Rbd_pl = workSheet.Cells[rowIterator, 7].Value.ToString();
                                        List<PriceBookLevelRef> pricebook = CHNLSVC.Sales.GetPriceLevelList(company, workSheet.Cells[rowIterator, 6].Value.ToString(), "");
                                        if (pricebook==null)
                                        {
                                            pricebook = new List<PriceBookLevelRef>();
                                        }
                                        pricebook = pricebook.OrderBy(a => a.Sapl_pb_lvl_cd).ToList();
                                        var _levels = pricebook.Select(y => y.Sapl_pb_lvl_cd).Distinct().ToList();
                                        if(pricebook.Count<=0)
                                        {
                                            exUpErr = new ExcelUploadErr(rowIterator, _ob.Rbd_pl, "Invalid pricebook level");
                                            excelUploadErr.Add(exUpErr);
                                        }
                                    }
                                    else
                                    {
                                        exUpErr = new ExcelUploadErr(rowIterator, _ob.Rbd_pl, "Price book is empty");
                                        excelUploadErr.Add(exUpErr);
                                    }
                                }
                                else
                                {
                                    _ob.Rbd_pl = "";
                                }
                                if (workSheet.Cells[rowIterator, 8].Value != null)
                                {
                                    _ob.Rbd_price_circul = workSheet.Cells[rowIterator, 8].Value.ToString();
                                    List<Sar_Type> documents =null;// CHNLSVC.ComSearch.GetPromoCircula("1", "10", "Code", _ob.Rbd_price_circul, company);
                                    
                                    if (documents==null)
                                    {
                                        documents = new List<Sar_Type>();
                                    }
                                    documents = documents.Where(x => x.srtp_cd.Equals(_ob.Rbd_price_circul)).ToList();
                                    if (documents.Count <= 0)
                                    {
                                        //exUpErr = new ExcelUploadErr(rowIterator, _ob.Rbd_price_circul, "Invalid Category 01");
                                       // excelUploadErr.Add(exUpErr);
                                    }
                                }
                                else
                                {
                                    _ob.Rbd_price_circul = "";
                                }
                                if (workSheet.Cells[rowIterator, 9].Value != null)
                                {
                                    _ob.Rbd_cus_cd = workSheet.Cells[rowIterator, 9].Value.ToString();
                                    List<MST_CUS_SEARCH_HEAD> documents = CHNLSVC.ComSearch.getCustomerDetails("1", "10", "Code", _ob.Rbd_cus_cd, company);
                                    if (documents == null)
                                    {
                                        documents = new List<MST_CUS_SEARCH_HEAD>();
                                    }
                                    documents = documents.Where(x => x.Mbe_cd.Equals(_ob.Rbd_cus_cd)).ToList();
                                    if (documents.Count <= 0)
                                    {
                                        exUpErr = new ExcelUploadErr(rowIterator, _ob.Rbd_cus_cd, "Invalid customercode");
                                        excelUploadErr.Add(exUpErr);
                                    }
                                }
                                else
                                {
                                    _ob.Rbd_cus_cd = "";
                                }
                               
                                if (workSheet.Cells[rowIterator, 10].Value != null)
                                {
                                    _ob.Rbd_hp_schm = workSheet.Cells[rowIterator, 10].Value.ToString();
                                    List<hpr_sch_det> _systemUserCompanyList = CHNLSVC.ComSearch.getScheme("1", "1000", "Code","", company);
                                    if (_systemUserCompanyList==null)
                                    {
                                        _systemUserCompanyList = new List<hpr_sch_det>();
                                    }
                                    _systemUserCompanyList = _systemUserCompanyList.Where(x => x.hsd_cd.Equals(_ob.Rbd_hp_schm)).ToList();
                                    if (_systemUserCompanyList.Count <= 0)
                                    {
                                        exUpErr = new ExcelUploadErr(rowIterator, _ob.Rbd_hp_schm, "Invalid scheme code");
                                        excelUploadErr.Add(exUpErr);
                                    }
                                }
                                else
                                {
                                    _ob.Rbd_hp_schm = "";
                                }
                                if (workSheet.Cells[rowIterator, 11].Value != null)
                                {
                                    _ob.Rbd_sales_tp = workSheet.Cells[rowIterator, 11].Value.ToString();
                                    List<Sar_Type> documents = CHNLSVC.ComSearch.GetCommissionInvTp("1","10", "Code", _ob.Rbd_sales_tp, company);//Sales types
                                    if (documents==null)
                                    {
                                        documents = new List<Sar_Type>();
                                    }
                                    documents = documents.Where(x => x.srtp_cd.Equals(_ob.Rbd_sales_tp)).ToList();
                                    if (documents.Count <= 0)
                                    {
                                        exUpErr = new ExcelUploadErr(rowIterator, _ob.Rbd_sales_tp, "Invalid sales type");
                                        excelUploadErr.Add(exUpErr);
                                    }
                                }
                                else
                                {
                                    _ob.Rbd_sales_tp = "";
                                }
                                if (workSheet.Cells[rowIterator, 12].Value != null)
                                {
                                    _ob.Rbd_pay_mode = workSheet.Cells[rowIterator, 12].Value.ToString();
                                    List<PaymentTypeRef> paytype = CHNLSVC.Sales.GetAllPaymentType(company, "", "");
                                    if (paytype==null)
                                    {
                                        paytype = new List<PaymentTypeRef>();
                                    }
                                    paytype = paytype.Where(x => x.Sapt_cd.Equals(_ob.Rbd_pay_mode)).ToList();
                                    if (paytype.Count <= 0)
                                    {
                                        exUpErr = new ExcelUploadErr(rowIterator, _ob.Rbd_pay_mode, "Invalid paymode");
                                        excelUploadErr.Add(exUpErr);
                                    }
                                }
                                else
                                {
                                    _ob.Rbd_pay_mode = "";
                                }
                                if (workSheet.Cells[rowIterator, 13].Value != null)
                                {
                                    _ob.Rbd_pay_sub_tp = workSheet.Cells[rowIterator, 13].Value.ToString();

                                    DataTable dtd = CHNLSVC.General.Get_sar_price_type("");
                                    List<ComboBoxObject> documents = new List<ComboBoxObject>();
                                    if (dtd != null && dtd.Rows.Count > 0)
                                    {
                                        int i = 0;
                                        foreach (var list in dtd.Rows)
                                        {
                                            ComboBoxObject o1 = new ComboBoxObject();
                                            o1.Text = dtd.Rows[i]["sarpt_cd"].ToString();
                                            o1.Value = dtd.Rows[i]["sarpt_cd"].ToString();
                                            documents.Add(o1);
                                            i++;
                                        }
                                    }                              
                                                                        
                                    if (documents==null)
                                    {
                                        documents = new List<ComboBoxObject>();
                                    }
                                    documents = documents.Where(x => x.Value.Equals(_ob.Rbd_pay_sub_tp)).ToList();
                                    if (documents.Count <= 0)
                                    {
                                        exUpErr = new ExcelUploadErr(rowIterator, _ob.Rbd_pay_sub_tp, "Invalid promotional code");
                                        excelUploadErr.Add(exUpErr);
                                    }
                                }
                                else
                                {
                                    _ob.Rbd_pay_sub_tp = "";
                                   
                                }
                                if (workSheet.Cells[rowIterator, 14].Value != null)
                                {
                                    _ob.Rbd_slab_base = workSheet.Cells[rowIterator, 14].Value.ToString();
                                    if(!(_ob.Rbd_slab_base.Equals("QTY"))&&!(_ob.Rbd_slab_base.Equals("VALUE")))
                                    {
                                        exUpErr = new ExcelUploadErr(rowIterator, _ob.Rbd_slab_base, "Invalid slabbase");
                                        excelUploadErr.Add(exUpErr);
                                    }
                                }
                                else
                                {
                                    _ob.Rbd_slab_base = "";
                                }
                              
                                if (workSheet.Cells[rowIterator, 15].Value != null)
                                {
                                    int n;
                                    bool isNumeric = int.TryParse(workSheet.Cells[rowIterator, 15].Value.ToString(), out n);
                                    if (isNumeric) _ob.Rbd_from_val =Convert.ToInt64( workSheet.Cells[rowIterator, 15].Value.ToString());
                                    else
                                    {
                                        exUpErr = new ExcelUploadErr(rowIterator, workSheet.Cells[rowIterator, 15].Value.ToString(), "Invalid from value");
                                        excelUploadErr.Add(exUpErr);
                                    }
                                    if (workSheet.Cells[rowIterator, 16].Value != null)
                                    {
                                        int n2;
                                        isNumeric = int.TryParse(workSheet.Cells[rowIterator, 16].Value.ToString(), out n2);
                                        if (isNumeric) _ob.Rdb_to_val = Convert.ToInt64(workSheet.Cells[rowIterator, 16].Value.ToString());

                                    }
                                    if(_ob.Rbd_from_val>_ob.Rdb_to_val)
                                    {
                                        exUpErr = new ExcelUploadErr(rowIterator, workSheet.Cells[rowIterator, 15].Value.ToString(), "Invalid from value is greater than to value");
                                        excelUploadErr.Add(exUpErr);
                                    }
                                    if(_ob.Rbd_from_val<0)
                                    {
                                        exUpErr = new ExcelUploadErr(rowIterator, workSheet.Cells[rowIterator, 15].Value.ToString(), "From value cannot be minus");
                                        excelUploadErr.Add(exUpErr);
                                    }
                                }
                                else
                                {
                                    _ob.Rbd_from_val = 0;
                                }
                                if (workSheet.Cells[rowIterator, 16].Value != null)
                                {
                                    int n;
                                    bool isNumeric = int.TryParse(workSheet.Cells[rowIterator, 16].Value.ToString(), out n);
                                    if (isNumeric)
                                    {
                                        _ob.Rdb_to_val = Convert.ToInt64(workSheet.Cells[rowIterator, 16].Value.ToString());
                                        if (_ob.Rdb_to_val < 0)
                                        {
                                            exUpErr = new ExcelUploadErr(rowIterator, workSheet.Cells[rowIterator, 16].Value.ToString(), "To value cannot be minus");
                                            excelUploadErr.Add(exUpErr);
                                        }
                                    }
                                }
                                else
                                {
                                    _ob.Rdb_to_val = 0;
                                }
                                if (workSheet.Cells[rowIterator, 17].Value != null)
                                {
                                    int n;
                                    bool isNumeric = int.TryParse(workSheet.Cells[rowIterator, 17].Value.ToString(), out n);
                                    if (isNumeric)
                                    {
                                        _ob.Rdb_marks = Convert.ToDecimal(workSheet.Cells[rowIterator, 17].Value.ToString());
                                        if (_ob.Rdb_marks < 0)
                                        {
                                            exUpErr = new ExcelUploadErr(rowIterator, _ob.Rdb_marks.ToString(), "Marks cannot be minus");
                                            excelUploadErr.Add(exUpErr);
                                        }
                                    }

                                }
                                else
                                {
                                    _ob.Rdb_marks = 0;
                                }
                                if (workSheet.Cells[rowIterator, 18].Value != null)
                                {
                                    _ob.Rdb_anal1 = workSheet.Cells[rowIterator, 18].Value.ToString();
                                                                     
                                }
                                else
                                {
                                    _ob.Rdb_anal1 = "";
                                }
                                if (workSheet.Cells[rowIterator, 19].Value != null)
                                {
                                    _ob.Rdb_anal2 = workSheet.Cells[rowIterator, 19].Value.ToString();
                                    //Exec Loc Area  Region Zone
                                    if (!(_ob.Rdb_anal2.Equals("Exec")) && !(_ob.Rdb_anal2.Equals("Loc")) && !(_ob.Rdb_anal2.Equals("Area")) && !(_ob.Rdb_anal2.Equals("Region")) && !(_ob.Rdb_anal2.Equals("Zone")))
                                    {
                                        exUpErr = new ExcelUploadErr(rowIterator, _ob.Rdb_anal2, "Invalid target/base");
                                        excelUploadErr.Add(exUpErr);
                                    }                                  

                                }
                                else
                                {
                                    _ob.Rdb_anal2 = "";
                                }
                                if (workSheet.Cells[rowIterator, 20].Value != null)
                                {
                                    _ob.Rdb_anal3 = workSheet.Cells[rowIterator, 20].Value.ToString();
                                }
                                else
                                {
                                    _ob.Rdb_anal3 = "";
                                }
                                if (workSheet.Cells[rowIterator, 21].Value != null)
                                {
                                    int n;
                                    bool isNumeric = int.TryParse(workSheet.Cells[rowIterator, 21].Value.ToString(), out n);
                                    if (isNumeric) _ob.Rdb_tot_comb_qty = Convert.ToDecimal(workSheet.Cells[rowIterator, 21].Value.ToString());

                                }
                                else
                                {
                                    _ob.Rdb_tot_comb_qty =0;
                                }
                                if (workSheet.Cells[rowIterator, 22].Value != null)
                                {
                                    int n;
                                    bool isNumeric = int.TryParse(workSheet.Cells[rowIterator, 22].Value.ToString(), out n);
                                    if (isNumeric) _ob.Rdb_line = Convert.ToInt32(workSheet.Cells[rowIterator, 22].Value.ToString());

                                }
                                else
                                {
                                    _ob.Rdb_line = 0;
                                }
                                _lst.Add(_ob);
                               
                            }
                            if (excelUploadErr.Count>0)
                            {
                                //validation fales
                                return Json(new { success = false, login = true, msg = "Invalid Item rowl", data = excelUploadErr }, JsonRequestBehavior.AllowGet);
                            }
                            Session["ref_bonus_det"] = _lst;
                            return Json(new { success = true, login = true, list = _lst }, JsonRequestBehavior.AllowGet);
                        }


                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Cant Find Excel", data = "" }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(new { success = false, login = true, msg = "Cant Find Excel", data = "" }, JsonRequestBehavior.AllowGet);
                }


            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
