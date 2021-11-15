using FF.BusinessObjects;
using FF.BusinessObjects.Commission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FF.SCMWebMVC.Controllers
{
    public class CollectionBonusAdjustmentsController : BaseController
    {
        //
        // GET: /CollectionBonusAdjustments/
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["hpt_arr_acc_det"] = null;
                return View();

            }
            else
            {
                return Redirect("~/Login/index");
            }
        }
        public JsonResult LoadBalaArrearsList(string ProfCenter, string month, string effectivedate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            ProfCenter = ProfCenter.Trim();
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<hpt_arr_acc_det> _list = new List<hpt_arr_acc_det>();
                List<hpt_arr_acc_det> _finallist = new List<hpt_arr_acc_det>();

                _list = CHNLSVC.Finance.GetArrBalAccDetails(ProfCenter, Convert.ToDateTime(month).AddMonths(1).AddDays(-1));

                if (_list != null && _list.Count>0)
                {
                    foreach (var _alllist in _list)
                    {
                        Int64 seq = 0;
                        if (Convert.ToDateTime(month) == Convert.ToDateTime(effectivedate))
                        {
                            seq = CHNLSVC.Finance.GetAllCollSeq(_alllist.Haad_com, _alllist.Haad_pc, _alllist.Haad_acc_cd, _alllist.Haad_date, Convert.ToDateTime(effectivedate).AddMonths(1).AddDays(-1));
                        }
                        else
                        {
                            seq = CHNLSVC.Finance.GetEffectiveCollSeq(_alllist.Haad_com, _alllist.Haad_pc, _alllist.Haad_acc_cd, _alllist.Haad_date, Convert.ToDateTime(effectivedate).AddMonths(1).AddDays(-1));
                        }
                      
                        if (seq == 0)
                        {
                            _alllist.ActStatus = 1;
                        }
                        else
                        {
                            if (seq == _alllist.Haad_seq)
                            {
                                _alllist.ActStatus = 1;
                            }
                            else
                            {
                                _alllist.ActStatus = 0;
                            }
                        }
                    }
                    _finallist = _list.Where(a => a.ActStatus == 1).ToList();
                }
              

                Session["hpt_arr_acc_det"] = _finallist;
                if (_finallist == null)
                {
                    _finallist = new List<hpt_arr_acc_det>();
                }
                return Json(new { success = true, login = true, list = _finallist }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetPageData(int newPageValue)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    List<hpt_arr_acc_det> _list = (List<hpt_arr_acc_det>)Session["hpt_arr_acc_det"];
                    _list = _list.Skip(10 * (newPageValue - 1)).Take(10).ToList();

                    return Json(new { success = true, login = true, list = _list }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetSearchData(string Accno)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    List<hpt_arr_acc_det> _list = (List<hpt_arr_acc_det>)Session["hpt_arr_acc_det"];
                    _list = _list.Where(a => a.Haad_acc_cd.Contains(Accno)).ToList();
                    return Json(new { success = true, login = true, list = _list }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult EditAdjAccount(string PC, string Account, string Month, string Arre)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string[] ACC = Account.Split('(');
                Account = ACC[0].ToString().Trim();

                List<hpt_arr_acc_det> _list = Session["hpt_arr_acc_det"] as List<hpt_arr_acc_det>;
                _list = _list.Where(a => a.Haad_pc == PC && a.Haad_acc_cd == Account && a.Haad_date == Convert.ToDateTime(Month).AddMonths(1).AddDays(-1)).ToList();

                List<hpr_hand_over_ac> _handover = CHNLSVC.Finance.GetHandOverDataAccAll(company, PC, Convert.ToDateTime(Month).AddMonths(1).AddDays(-1), Account);
                if (_handover != null)
                {
                    if (_handover.Count >0)
                    {
                        _list[0].handoverreject = _handover[0].Hhoa_rej_lmt;
                    }
                }
                
                string _schem = CHNLSVC.Finance.GetSchemePeriod(Account);

                return Json(new { success = true, login = true, list = _list, schem = _schem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult SaveAdjDetails(string pc, string month, string accno, string ngrsett, string realarr, string ndue, string nser, string ntadj, string nlod, string ndisput, string netarr, string ndiri, string nother, string srcomp, string reson, string effectdate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<hpt_arr_acc_det> _listold = new List<hpt_arr_acc_det>();
                string err = "";
                List<hpt_arr_acc_det> _list = Session["hpt_arr_acc_det"] as List<hpt_arr_acc_det>;
                _list = _list.Where(a => a.Haad_pc == pc && a.Haad_acc_cd == accno && a.Haad_date == Convert.ToDateTime(month).AddMonths(1).AddDays(-1)).ToList();
                _listold.AddRange(_list);


                hpt_arr_acc_det _ob2 = new hpt_arr_acc_det();
                _ob2.Haad_acc_cd = accno;
                _ob2.HAAD_ADJ_AMT = _list[0].HAAD_ADJ_AMT;
                _ob2.HAAD_ADJ_TOT = _list[0].HAAD_ADJ_TOT;
                _ob2.Haad_currnt_month_due = _list[0].Haad_currnt_month_due;
                _ob2.Haad_date = _list[0].Haad_date;
                _ob2.HAAD_EFFECT_DT = _list[0].HAAD_EFFECT_DT;
                _ob2.HAAD_DIRIYA_ADJ = _list[0].HAAD_DIRIYA_ADJ;
                _ob2.HAAD_DISP_ADJ = _list[0].HAAD_DISP_ADJ;
                _ob2.HAAD_GRCE_SETT = _list[0].HAAD_GRCE_SETT;
                _ob2.HAAD_LOD_ADJ = _list[0].HAAD_LOD_ADJ;
                _ob2.HAAD_OTH = _list[0].HAAD_OTH;
                _ob2.Haad_tot_arr_amt = _list[0].Haad_tot_arr_amt;
                _ob2.HAAD_SHOP_COM_ADJ = _list[0].HAAD_SHOP_COM_ADJ;
                _ob2.HAAD_REMARK = _list[0].HAAD_REMARK;
                _ob2.HAAD_SER_PROB = _list[0].HAAD_SER_PROB;


                hpt_arr_acc_det _ob = new hpt_arr_acc_det();
                _ob = _list[0];
                _ob.Haad_acc_cd = accno;
                _ob.HAAD_ADJ_AMT =Convert.ToDecimal( ntadj);
                _ob.HAAD_ADJ_TOT = Convert.ToDecimal(ntadj);
                _ob.Haad_currnt_month_due = Convert.ToDecimal(ndue);
                _ob.Haad_date = Convert.ToDateTime(month).AddMonths(1).AddDays(-1);
                _ob.HAAD_EFFECT_DT = Convert.ToDateTime(effectdate).AddMonths(1).AddDays(-1);
                _ob.HAAD_DIRIYA_ADJ = Convert.ToDecimal(ndiri);
                _ob.HAAD_DISP_ADJ = Convert.ToDecimal(ndisput);
                _ob.HAAD_GRCE_SETT = Convert.ToDecimal(ngrsett);
                _ob.HAAD_LOD_ADJ = Convert.ToDecimal(nlod);
                _ob.HAAD_OTH = Convert.ToDecimal(nother);
                if (_ob.HAAD_OTH > 0)
                {
                    if (string.IsNullOrEmpty(reson))
                    {
                        return Json(new { success = false, login = true, msg = "Please select reason." }, JsonRequestBehavior.AllowGet);

                    }
                }
                _ob.Haad_tot_arr_amt = Convert.ToDecimal(netarr);
                _ob.HAAD_SHOP_COM_ADJ = Convert.ToDecimal(srcomp);
               // _ob.HAAD_SHOP_COM_ACC = Convert.ToDecimal(srcomp);
                _ob.HAAD_REMARK = reson;
                _ob.HAAD_SER_PROB = Convert.ToDecimal(nser);

                int effect = 0;
                //save
                if (Convert.ToDateTime( month) == Convert.ToDateTime( effectdate))
                {
                    effect = CHNLSVC.Finance.SaveAccountAdjDetails(_ob2, _ob, out err);
                }
                else
                {
                    //insert or update
                    effect = CHNLSVC.Finance.SaveAccountAdjEffectDetails(_ob2, _ob, out err);
                }
              
                if (effect == 1 || effect == 0)
                {
                    return Json(new { success = true, login = true, msg = "Successfull Saved"  }, JsonRequestBehavior.AllowGet);
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
        public JsonResult LoadGraceDate(string ProfCenter, string month)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            ProfCenter = ProfCenter.Trim();
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                DateTime gracedate = CHNLSVC.Finance.GetPCComGraceDate(company, ProfCenter, Convert.ToDateTime(month).AddMonths(1).AddDays(-1));
                return Json(new { success = true, login = true, date = gracedate }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult LoadReason()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    DataTable _data = CHNLSVC.Finance.LoadBonusAdjResons();

                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (_data != null && _data.Rows.Count>0)
                    {
                        int i = 0;
                        foreach (var list in _data.Rows)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = _data.Rows[i][0].ToString();
                            o1.Value = _data.Rows[i][0].ToString();
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
	}
}