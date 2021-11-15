using FF.BusinessObjects.Commission;
using FF.BusinessObjects.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FF.BusinessObjects.Financial;
using System.Data;

namespace FF.SCMWebMVC.Controllers
{
    public class CalculationAdjesmentController : BaseController
    {
        //
        // GET: /CalculationAdjesment/
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["hpt_arr_acc"] = null;
                Session["hdracc"] = null;
                return View();

            }
            else
            {
                return Redirect("~/Login/index");
            }
        }
        public JsonResult getShwroomManagerData(string ManagerCD)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            ManagerCD = ManagerCD.Trim();
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<MgrCreation> _list = new List<MgrCreation>();

                //_list = CHNLSVC.Finance.GetManagerDetails(company, "", ManagerCD);

                return Json(new { success = true, login = true, list = _list }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult getManagerTotAdj(string BonusMonth, string Manager , string EffectDAte)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            DateTime bonusmonth = Convert.ToDateTime(BonusMonth).AddMonths(1).AddDays(-1);
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<hpt_arr_acc> _list = new List<hpt_arr_acc>();
                _list = CHNLSVC.Finance.GetArrBalAccHdrByCode(company, Manager, bonusmonth);

                if (_list != null && _list.Count>0)
                {
                    foreach (var _alllist in _list)
                    {
                        var effcount = _list.Where(a => a.Haa_pc == _alllist.Haa_pc).Count();
                        if (effcount > 1)
                        {
                            //get max seq
                            Int64 seq = CHNLSVC.Finance.GetEffectiveCollHDRSeq(_alllist.Haa_com, _alllist.Haa_pc, _alllist.Haa_date, Convert.ToDateTime(EffectDAte).AddMonths(1).AddDays(-1));
                            if (seq == 0)
                            {
                                _alllist.ActiveStatus = 1;
                            }
                            else
                            {
                                if (seq == _alllist.Haa_seq)
                                {
                                    _alllist.ActiveStatus = 1;
                                }
                                else
                                {
                                    _alllist.ActiveStatus = 0;
                                }
                            }
                        }
                        else
                        {
                            _alllist.ActiveStatus = 1;
                        }
                    }
                    _list = _list.Where(a => a.ActiveStatus == 1).ToList();
                    _list = _list.OrderBy(a => a.Haa_mng_cd).ToList();
                }

                if (_list == null) _list = new List<hpt_arr_acc>();
                Session["hpt_arr_acc"] = _list;
                List<hpt_arr_acc> _managerwise = new List<hpt_arr_acc>();
                List<hpt_arr_acc> _pccatwise = new List<hpt_arr_acc>();

                _managerwise = _list.GroupBy(l => new { l.Haa_mng_cd })
 .Select(cl => new hpt_arr_acc
 {
     Haa_mng_cd = cl.First().Haa_mng_cd,
     Haa_date = cl.First().Haa_date,
 }).ToList();
                return Json(new { success = true, login = true, data = _managerwise }, JsonRequestBehavior.AllowGet);
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
                    List<hpt_arr_acc> _acclist = Session["hpt_arr_acc"] as List<hpt_arr_acc>;
                    _acclist = _acclist.Skip(10 * (newPageValue - 1)).Take(10).ToList();

                    return Json(new { success = true, login = true, data = _acclist }, JsonRequestBehavior.AllowGet);
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

        //EditDataCode
        public JsonResult EditDataCode(string Manager, string PC)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            Manager = Manager.Trim();
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<MgrCreation> _list = new List<MgrCreation>();
                List<hpt_arr_acc> _acclist = Session["hpt_arr_acc"] as List<hpt_arr_acc>;
                _list = CHNLSVC.Finance.GetManagerDetails(company, "", Manager);

                if (_list != null)
                {
                    if (_list.Count > 0)
                    {
                        if (_list.First().hmfa_pc_cat == "Profit center wise")
                        {
                            //Please Select PC
                            if (PC=="")
                            {
                                return Json(new { success = false, login = true, msg = "PC Wise Manager! Please Select PC" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                _acclist = _acclist.Where(a => a.Haa_mng_cd == Manager && a.Haa_pc==PC).ToList();
                            }
                        }
                        else if (_list.First().hmfa_pc_cat == "Manager wise")
                        {
                            _acclist = _acclist.Where(a => a.Haa_mng_cd == Manager).ToList();
                            _acclist = _acclist.GroupBy(l => new { l.Haa_mng_cd })
 .Select(cl => new hpt_arr_acc
 {
     Haa_pc=cl.First().Haa_pc,
     Haa_mng_cd = cl.First().Haa_mng_cd,
     Haa_date = cl.First().Haa_date,
     Haa_tot_clos_bal = cl.Sum(a => a.Haa_tot_clos_bal),
     Haa_act_arr_amt = cl.Sum(a => a.Haa_act_arr_amt),
     Haa_tot_arr_amt = cl.Sum(a => a.Haa_tot_arr_amt),
     Haa_tot_no_of_acc = cl.Sum(a => a.Haa_tot_no_of_acc),
     Haa_tot_no_of_act_acc = cl.Sum(a => a.Haa_tot_no_of_act_acc),
     Haa_peri_ovr_acc = cl.Sum(a => a.Haa_peri_ovr_acc),
     Haa_tot_no_of_arr_acc = cl.Sum(a => a.Haa_tot_no_of_arr_acc),
     Haa_currnt_month_due = cl.Sum(a => a.Haa_currnt_month_due),
     Haa_proce_complt = cl.Sum(a => a.Haa_proce_complt),
     HAA_DIRIYA_ADJ = cl.Sum(a => a.HAA_DIRIYA_ADJ),
     HAA_ADJ_TOT = cl.Sum(a => a.HAA_ADJ_TOT),
     HAA_ADJ_AMT = cl.Sum(a => a.HAA_ADJ_AMT),
     HAA_LOD_ADJ = cl.Sum(a => a.HAA_LOD_ADJ),
     HAA_SER_PROB = cl.Sum(a => a.HAA_SER_PROB),
     HAA_DISP_ADJ = cl.Sum(a => a.HAA_DISP_ADJ),
     HAA_OTH = cl.Sum(a => a.HAA_OTH),
     HAA_TOT_GRCE_SETT = cl.Sum(a => a.HAA_TOT_GRCE_SETT),
     HAA_GRCE_SETT = cl.Sum(a => a.HAA_GRCE_SETT),
     HAA_TOT_GRCE_SETT_ADJ = cl.Sum(a => a.HAA_TOT_GRCE_SETT_ADJ),
     HAA_GRCE_SETT_ADJ = cl.Sum(a => a.HAA_GRCE_SETT_ADJ),
     HAA_GRCE_PER_COLL = cl.Sum(a => a.HAA_GRCE_PER_COLL),
     HAA_CURR_DUE_TOT = cl.Sum(a => a.HAA_CURR_DUE_TOT),
     HAA_ADJ_DUE_TOT = cl.Sum(a => a.HAA_ADJ_DUE_TOT),
     HAA_PREV_ARR_TOT = cl.Sum(a => a.HAA_PREV_ARR_TOT),
     HAA_ADJ_PREV_TOTARR = cl.Sum(a => a.HAA_ADJ_PREV_TOTARR),
     HAA_ALL_DUE_TOT = cl.Sum(a => a.HAA_ALL_DUE_TOT),
     HAA_ADJ_CURR_ARR = cl.Sum(a => a.HAA_ADJ_CURR_ARR),
     HAA_ADJ_GRA_PER_SETT = cl.Sum(a => a.HAA_ADJ_GRA_PER_SETT),
     HAA_SHORT_REMITT = cl.Sum(a => a.HAA_SHORT_REMITT),
     HAA_ARR_SCH_ADJ = cl.Sum(a => a.HAA_ARR_SCH_ADJ),
     HAA_SHOP_COM_ADJ = cl.Sum(a => a.HAA_SHOP_COM_ADJ),
     HAA_ISSUE_CHQ_RTN_ADJ = cl.Sum(a => a.HAA_ISSUE_CHQ_RTN_ADJ),
     HAA_TOT_REMITT = cl.Sum(a => a.HAA_TOT_REMITT),
     HAA_ADJ_REMITT = cl.Sum(a => a.HAA_ADJ_REMITT),
     HAA_SUPP_COLL = cl.Sum(a => a.HAA_SUPP_COLL),
     HAA_SUPP_COLL_ADJ = cl.Sum(a => a.HAA_SUPP_COLL_ADJ),
     HAA_ADJ_GRA_PER = cl.Sum(a => a.HAA_ADJ_GRA_PER),
     HAA_PREV_GRACE_PER_COLL = cl.Sum(a => a.HAA_PREV_GRACE_PER_COLL),
     HAA_ADJ_PRE_GR_PER_COLL = cl.Sum(a => a.HAA_ADJ_PRE_GR_PER_COLL),
     HAA_PRE_MON_SUP_COLL = cl.Sum(a => a.HAA_PRE_MON_SUP_COLL),
     HAA_ADJ_PR_MO_SUP_COLL = cl.Sum(a => a.HAA_ADJ_PR_MO_SUP_COLL),
     HAA_NET_REMIT = cl.Sum(a => a.HAA_NET_REMIT),
     HAA_TOT_REC_BAL = cl.Sum(a => a.HAA_TOT_REC_BAL),
     HAA_HAND_OVER=cl.Sum(a=>a.HAA_HAND_OVER),
     PrevHand=cl.Sum(a=>a.PrevHand)
 }).ToList();

                        }
                        else
                        {
                            _list = _list.OrderBy(a => a.hmfa_pc_cat).ToList();
                            _acclist = _acclist.Where(a => a.Haa_pc == _list.First().hmfa_pc).ToList();
                        }
                    }
                }



                Session["hdracc"] = _acclist;
                decimal prvarr = 0;
                decimal prsupcoladj = 0;
                decimal prgrccolladj = 0;
                decimal prtotclbal = 0;
                decimal prtotnumact = 0;
                decimal prmsupcoll = 0;
                DataTable dt = CHNLSVC.Finance.GetPrevMonthActArrears(_acclist.FirstOrDefault().Haa_pc, _acclist.FirstOrDefault().Haa_date.AddDays(1).AddMonths(-1).AddDays(-1), _acclist.FirstOrDefault().Haa_date.AddDays(1).AddMonths(-1).AddDays(-1));
                if (dt != null && dt.Rows.Count>0)
                {
                    prvarr = Convert.ToDecimal(dt.Rows[0]["haa_act_arr_amt"].ToString());
                    prsupcoladj = Convert.ToDecimal(dt.Rows[0]["haa_supp_coll_adj"].ToString());
                    prgrccolladj = Convert.ToDecimal(dt.Rows[0]["haa_grc_prd_col_adj"].ToString());
                    prtotclbal = Convert.ToDecimal(dt.Rows[0]["haa_tot_clos_bal"].ToString());
                    prtotnumact = Convert.ToDecimal(dt.Rows[0]["haa_tot_no_of_act_acc"].ToString());
                    prmsupcoll = Convert.ToDecimal(dt.Rows[0]["haa_supp_coll"].ToString());
                }
                decimal prvadj = CHNLSVC.Finance.GetPrevMonthAdj(_acclist.FirstOrDefault().Haa_pc, _acclist.FirstOrDefault().Haa_date.AddDays(1).AddMonths(-1).AddDays(-1), _acclist.FirstOrDefault().Haa_date.AddDays(1).AddMonths(-1).AddDays(-1));

                return Json(new { success = true, login = true, list = _acclist, prvarr = prvarr, prvadj = prvadj, prsupcoladj = prsupcoladj, prgrccolladj = prgrccolladj, prtotclbal = prtotclbal, prtotnumact = prtotnumact, prmsupcoll = prmsupcoll }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }

        //ShowPCCodes
        public JsonResult ShowPCCodes(string Manager)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            Manager = Manager.Trim();
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<hpt_arr_acc> _acclist = Session["hpt_arr_acc"] as List<hpt_arr_acc>;
                List<hpt_arr_acc> _pclist = new List<hpt_arr_acc>();
                if (_acclist != null)
                {
                    if (_acclist.Count > 0)
                    {
                        _acclist = _acclist.Where(a => a.Haa_mng_cd == Manager).ToList();
                        _pclist = _acclist.GroupBy(l => new { l.Haa_pc })
.Select(cl => new hpt_arr_acc
{
    Haa_pc = cl.First().Haa_pc
}).ToList();
                    }
                }





                return Json(new { success = true, login = true, list = _pclist }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult SaveAdjDetails(string duetotadj, string prevmontharreadj, string duetotal, string currarradj, string gracepersettadj, string shortremitance,
            string issuechkrtn, string totremiadj, string supplcolladj, string gracepercolladj, string prevmonthgracecolladj, string prevmonthsupcolladj, string totrecbal, string effectdate, string othadj, string netremitance)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string err = "";
                bool isupdate = false;
                List<hpt_arr_acc> _list = Session["hdracc"] as List<hpt_arr_acc>;

                if (_list[0].HAA_EFFECT_DT.Date == Convert.ToDateTime(effectdate).AddMonths(1).AddDays(-1).Date)
                {
                    isupdate = true;
                }
                _list[0].HAA_ADJ_DUE_TOT = Convert.ToDecimal(duetotadj);
                _list[0].HAA_ADJ_PREV_TOTARR = Convert.ToDecimal(prevmontharreadj);
                _list[0].HAA_ADJ_CURR_ARR = Convert.ToDecimal(currarradj);
                _list[0].HAA_SHORT_REMITT = Convert.ToDecimal(shortremitance);
                _list[0].HAA_ISSUE_CHQ_RTN_ADJ = Convert.ToDecimal(issuechkrtn);
                _list[0].HAA_ADJ_REMITT = Convert.ToDecimal(totremiadj);
                _list[0].HAA_SUPP_COLL_ADJ = Convert.ToDecimal(supplcolladj);
                _list[0].HAA_GRC_PRD_COL_ADJ = Convert.ToDecimal(gracepercolladj);
                _list[0].HAA_ADJ_PRE_GR_PER_COLL = Convert.ToDecimal(prevmonthgracecolladj);
                _list[0].HAA_ADJ_PR_MO_SUP_COLL = Convert.ToDecimal(prevmonthsupcolladj);
                //_list[0].HAA_TOT_REC_BAL = Convert.ToDecimal(totrecbal);
                _list[0].Haa_anal2 = "1";
                _list[0].HAA_EFFECT_DT = Convert.ToDateTime(effectdate).AddMonths(1).AddDays(-1);
                _list[0].HAA_OTH = Convert.ToDecimal(othadj);
                _list[0].HAA_ADJ_GRA_PER_SETT = Convert.ToDecimal(gracepersettadj);
                _list[0].HAA_NET_REMIT = Convert.ToDecimal(netremitance);
                int effect = 0;
                if (isupdate)
                {
                    _list[0].Haa_mod_by = userId;
                    _list[0].Haa_mod_dt = DateTime.Now;
                    effect = CHNLSVC.Finance.UpdateARR_ACCNew(_list, out err);
                }
                else
                {
                    _list[0].Haa_cre_dt = DateTime.Now;
                    _list[0].Haa_cre_by = userId;
                    _list[0].Haa_mod_by = userId;
                    _list[0].Haa_mod_dt = DateTime.Now;
                    // insert SaveARR_ACCNew
                    effect = CHNLSVC.Finance.SaveARR_ACCNew(_list, out err);
                }
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

    }
}