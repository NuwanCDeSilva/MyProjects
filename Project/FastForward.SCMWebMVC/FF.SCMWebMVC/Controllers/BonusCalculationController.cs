using FF.BusinessObjects;
using FF.BusinessObjects.Commission;
using FF.BusinessObjects.General;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FF.SCMWebMVC.Controllers
{
    public class BonusCalculationController : BaseController
    {
        //
        // GET: /BonusCalculation/
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["hpt_arr_acc2"] = null;
                Session["hdracc2"] = null;
                Session["year"] = null;
                return View();

            }
            else
            {
                return Redirect("~/Login/index");
            }
        }
        public JsonResult getManagerTotAdj(string BonusMonth, string Manager, string EffectDate)
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

                if (_list == null) _list = new List<hpt_arr_acc>();
                _list = _list.Where(a => a.HAA_EFFECT_DT.Date == Convert.ToDateTime(EffectDate).AddMonths(1).AddDays(-1).Date && (a.Haa_anal2 == "1" || a.Haa_anal2 == "2")).OrderBy(a => a.Haa_mng_cd).ToList();
                if (_list == null) _list = new List<hpt_arr_acc>();
                //
                foreach (var _alllist in _list)
                {
                    List<MgrCreation> _listm = new List<MgrCreation>();
                    _listm = CHNLSVC.Finance.GetManagerDetails(company, "", _alllist.Haa_mng_cd);
                    if (_listm != null && _listm.Count > 0)
                    {
                        _alllist.Haa_pc = _listm.First().hmfa_mainpc;
                    }

                    var effcount = _list.Where(a => a.Haa_pc == _alllist.Haa_pc).Count();
                    if (effcount > 1)
                    {
                        //get max seq
                        Int64 seq = CHNLSVC.Finance.GetEffectiveCollHDRSeq(_alllist.Haa_com, _alllist.Haa_pc, _alllist.Haa_date, Convert.ToDateTime(EffectDate).AddMonths(1).AddDays(-1));
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




                _list = _list.GroupBy(l => new { l.Haa_mng_cd })
.Select(cl => new hpt_arr_acc
{
    Haa_pc = cl.First().Haa_pc,
    Haa_com = cl.First().Haa_com,
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
    HAA_HAND_OVER = cl.Sum(a => a.HAA_HAND_OVER),
    PrevHand = cl.Sum(a => a.PrevHand),
    HAA_ARR_RELE_MONTHS = cl.Sum(a => a.HAA_ARR_RELE_MONTHS),
    HAA_GRC_PRD_COL_ADJ = cl.Sum(a => a.HAA_GRC_PRD_COL_ADJ),
    HAA_DISREG_AMT = cl.Sum(a => a.HAA_DISREG_AMT),
    HAA_GRC_PER_NT_QL = cl.Sum(a => a.HAA_GRC_PER_NT_QL),
    HAA_ALL_TOT_ADJ = cl.Sum(a => a.HAA_ALL_TOT_ADJ),
    HAA_NET_ARR_AMT = cl.Sum(a => a.HAA_NET_ARR_AMT),
    HAA_SPC_RMK = cl.First().HAA_SPC_RMK,
    HAA_ACC_DEPT_DEDUC = cl.Sum(a => a.HAA_ACC_DEPT_DEDUC),
    HAA_INV_DEPT_DEDUC = cl.Sum(a => a.HAA_INV_DEPT_DEDUC),
    HAA_CRED_DEPT_DEDUC = cl.Sum(a => a.HAA_CRED_DEPT_DEDUC),
    HAA_ACC_DEPT_REFUND = cl.Sum(a => a.HAA_ACC_DEPT_REFUND),
    HAA_INV_DEPT_REFUND = cl.Sum(a => a.HAA_INV_DEPT_REFUND),
    HAA_CRED_DEPT_REFUND = cl.Sum(a => a.HAA_CRED_DEPT_REFUND),
    HAA_ACC_DEPT_DEDUCRMK = cl.First().HAA_ACC_DEPT_DEDUCRMK,
    HAA_INV_DEPT_DEDUCRMK = cl.First().HAA_INV_DEPT_DEDUCRMK,
    HAA_CRED_DEPT_DEDUCRMK = cl.First().HAA_CRED_DEPT_DEDUCRMK,
    HAA_ACC_DEPT_REFUREMK = cl.First().HAA_ACC_DEPT_REFUREMK,
    HAA_INV_DEPT_REFUREMK = cl.First().HAA_INV_DEPT_REFUREMK,
    HAA_CRED_DEPT_REFUREMK = cl.First().HAA_CRED_DEPT_REFUREMK,
    HAA_SPC_VAL = cl.Sum(a => a.HAA_SPC_VAL),
    Haa_seq = cl.First().Haa_seq,
    Haa_anal2 = cl.First().Haa_anal2,
    HAA_EFFECT_DT = cl.First().HAA_EFFECT_DT,
    HAA_BONUS_REF_DED = cl.Sum(a => a.HAA_BONUS_REF_DED)

}).ToList();



                Session["hpt_arr_acc2"] = _list;
                List<hpt_arr_acc> _managerwise = new List<hpt_arr_acc>();
                List<hpt_arr_acc> _pccatwise = new List<hpt_arr_acc>();

                _managerwise = _list.GroupBy(l => new { l.Haa_mng_cd })
 .Select(cl => new hpt_arr_acc
 {
     Haa_mng_cd = cl.First().Haa_mng_cd,
     Haa_pc = cl.First().Haa_pc,
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

        public JsonResult CalcNotQualifiAmt(string closingbal, string com, string pc, string bonusdate)
        {
            decimal NVal = 0;
            decimal DVal = 0;
            List<hpr_disr_val_ref> _ntqly = CHNLSVC.Finance.GraceperiodnotQulified(Convert.ToDateTime(bonusdate).AddMonths(1).AddDays(-1), 1, com, pc, Convert.ToDecimal(closingbal));
            if (_ntqly != null)
            {
                if (_ntqly.Count > 0)
                {
                    NVal = _ntqly[0].hdvr_val * Convert.ToDecimal(closingbal) / 100;
                }

            }
            _ntqly = CHNLSVC.Finance.GraceperiodnotQulified(Convert.ToDateTime(bonusdate), 2, com, pc, Convert.ToDecimal(closingbal));
            if (_ntqly != null)
            {
                if (_ntqly.Count > 0)
                {
                    DVal = _ntqly[0].hdvr_val * Convert.ToDecimal(closingbal) / 100;
                }

            }
            return Json(new { success = true, login = true, NVal = NVal, DVal = DVal }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditDataCode(string Manager)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            Manager = Manager.Trim();
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<hpt_arr_acc> _acclist = Session["hpt_arr_acc2"] as List<hpt_arr_acc>;
                _acclist = _acclist.Where(a => a.Haa_mng_cd == Manager).ToList();

                List<MgrCreation> _list = new List<MgrCreation>();
                _list = CHNLSVC.Finance.GetManagerDetails(company, "", Manager);
                Int32 dates = 0;
                DateTime ManagerCrdt = _list[0].hmfa_cre_dt;
                if (_list == null)
                {
                    return Json(new { success = false, login = true, msg = "Please Create Manager :" + Manager, }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (_list.Count > 0)
                    {
                        string _mainshop = _list.First().hmfa_mainpc;
                        List<MgrCreation> _mainlist = _list.Where(a => a.hmfa_pc == _mainshop).ToList();
                        ManagerCrdt = _mainlist.First().hmfa_acc_dt;

                        _acclist[0].Haa_pc = _mainlist.First().hmfa_mainpc;
                        _acclist[0].Haa_pc_cat = _mainlist.First().hmfa_pc_cat;

                    }
                }
                //prev
                decimal prvarr = 0;
                decimal prsupcoladj = 0;
                decimal prgrccolladj = 0;
                decimal prtotclbal = 0;
                decimal prtotnumact = 0;
                decimal prmsupcoll = 0;
                DataTable dt1 = CHNLSVC.Finance.GetPrevMonthActArrears(_acclist.FirstOrDefault().Haa_pc, _acclist.FirstOrDefault().Haa_date.AddDays(1).AddMonths(-1).AddDays(-1), _acclist.FirstOrDefault().Haa_date.AddDays(1).AddMonths(-1).AddDays(-1));
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    prvarr = Convert.ToDecimal(dt1.Rows[0]["haa_act_arr_amt"].ToString());
                    prsupcoladj = Convert.ToDecimal(dt1.Rows[0]["haa_supp_coll_adj"].ToString());
                    prgrccolladj = Convert.ToDecimal(dt1.Rows[0]["haa_grc_prd_col_adj"].ToString());
                    prtotclbal = Convert.ToDecimal(dt1.Rows[0]["haa_tot_clos_bal"].ToString());
                    prtotnumact = Convert.ToDecimal(dt1.Rows[0]["haa_tot_no_of_act_acc"].ToString());
                    prmsupcoll = Convert.ToDecimal(dt1.Rows[0]["haa_supp_coll"].ToString());
                }
                decimal prvadj = CHNLSVC.Finance.GetPrevMonthAdj(_acclist.FirstOrDefault().Haa_pc, _acclist.FirstOrDefault().Haa_date.AddDays(1).AddMonths(-1).AddDays(-1), _acclist.FirstOrDefault().Haa_date.AddDays(1).AddMonths(-1).AddDays(-1));



                DateTime _bonusdate = _acclist[0].Haa_date;
                dates = _bonusdate.Subtract(ManagerCrdt).Days;

                //amount disregard
                //  List<hpr_disr_val_ref> _desreg = CHNLSVC.Finance.AmountDisregard(_acclist[0].Haa_tot_clos_bal, 2);
                List<hpr_disr_val_ref> _desreg = CHNLSVC.Finance.GraceperiodnotQulified(_bonusdate, 2, _acclist[0].Haa_com, _acclist[0].Haa_pc, _acclist[0].Haa_tot_clos_bal);
                if (_desreg != null)
                {
                    if (_desreg.Count > 0)
                    {
                        _acclist[0].HAA_DISREG_AMT = _desreg[0].hdvr_val * _acclist[0].Haa_tot_clos_bal / 100;
                    }
                }


                decimal dsprefu = 0;

                if (_acclist[0].HAA_EFFECT_DT == _acclist[0].Haa_date && _acclist[0].HAA_BONUS_REF_DED == 0)
                {
                    //DISPUTE BONUS REFUND
                    List<hpt_arr_acc> _allmothdata = CHNLSVC.Finance.GetAllBonusPCData(_acclist[0].Haa_pc, _acclist[0].Haa_date, _acclist[0].Haa_com, _acclist[0].Haa_mng_cd);
                    if (_allmothdata != null && _allmothdata.Count > 0)
                    {
                        foreach (var _allmonthob in _allmothdata)
                        {
                            List<hpt_arr_acc> _mothdata = CHNLSVC.Finance.GetMonthlyBonusPCData(_acclist[0].Haa_pc, _allmonthob.Haa_date, _acclist[0].Haa_com, _acclist[0].Haa_mng_cd, _allmonthob.Haa_date);
                            if (_mothdata != null && _mothdata.Count > 0)
                            {
                                dsprefu = dsprefu + _allmonthob.HAA_GRO_COLL_BONUS - _mothdata.First().HAA_GRO_COLL_BONUS;
                            }
                        }
                        _acclist[0].HAA_BONUS_REF_DED = dsprefu;
                    }
                }




                //grace period not qulified
                List<hpr_disr_val_ref> _ntqly = CHNLSVC.Finance.GraceperiodnotQulified(_bonusdate, 1, _acclist[0].Haa_com, _acclist[0].Haa_pc, _acclist[0].Haa_tot_clos_bal);
                if (_ntqly != null)
                {
                    if (_ntqly.Count > 0)
                    {
                        _acclist[0].HAA_GRC_PER_NT_QL = _ntqly[0].hdvr_val * _acclist[0].Haa_tot_clos_bal / 100;
                    }

                }
                _acclist[0].HAA_ALL_TOT_ADJ = _acclist[0].HAA_GRC_PER_NT_QL + _acclist[0].HAA_SHORT_REMITT + _acclist[0].HAA_ARR_SCH_ADJ
                    + _acclist[0].HAA_SHOP_COM_ADJ + _acclist[0].HAA_DIRIYA_ADJ + _acclist[0].HAA_LOD_ADJ + _acclist[0].HAA_SER_PROB
                    + _acclist[0].HAA_DISP_ADJ + _acclist[0].HAA_ISSUE_CHQ_RTN_ADJ + _acclist[0].HAA_DISREG_AMT + _acclist[0].HAA_ACC_DEPT_REFUND +
                    _acclist[0].HAA_ACC_DEPT_DEDUC + _acclist[0].HAA_INV_DEPT_REFUND + _acclist[0].HAA_INV_DEPT_DEDUC + _acclist[0].HAA_CRED_DEPT_REFUND
                    + _acclist[0].HAA_CRED_DEPT_DEDUC;
                _acclist[0].HAA_NET_ARR_AMT = _acclist[0].Haa_tot_arr_amt - _acclist[0].HAA_TOT_GRCE_SETT - _acclist[0].HAA_ALL_TOT_ADJ;


                decimal totremi = _acclist[0].HAA_TOT_REMITT - _acclist[0].HAA_HAND_OVER;
                //- _acclist[0].HAA_ADJ_REMITT + _acclist[0].HAA_SUPP_COLL;



                decimal totadjs = _acclist[0].HAA_GRC_PER_NT_QL + _acclist[0].HAA_SHORT_REMITT
            + _acclist[0].HAA_ARR_RELE_MONTHS + _acclist[0].HAA_SHOP_COM_ADJ + _acclist[0].HAA_DIRIYA_ADJ + _acclist[0].HAA_LOD_ADJ +
           _acclist[0].HAA_SER_PROB + _acclist[0].HAA_DISP_ADJ + _acclist[0].HAA_ISSUE_CHQ_RTN_ADJ + _acclist[0].HAA_DISREG_AMT + _acclist[0].HAA_OTH;

                decimal netarrea = _acclist[0].Haa_act_arr_amt - _acclist[0].HAA_HAND_OVER - _acclist[0].HAA_GRCE_SETT + _acclist[0].HAA_ADJ_GRA_PER_SETT - totadjs;
                decimal duetotal = _acclist[0].HAA_CURR_DUE_TOT + prvarr - _acclist[0].HAA_ADJ_DUE_TOT;


                if (duetotal == 0)
                {
                    _acclist[0].HAA_ARRE_PER = 999999999999;
                }
                else
                {
                    _acclist[0].HAA_ARRE_PER = Math.Round((netarrea * 100 / duetotal), 2);
                }

                //get bonus pers
                double _year1 = dates / 365.00;
                decimal _year = Math.Round(Convert.ToDecimal(_year1), 2);
                decimal _arrper = Math.Round(_acclist[0].HAA_ARRE_PER, 2);
                List<BonusDefinition> _bonus = new List<BonusDefinition>();
                decimal bonus_rt = 0;
                DataTable dt = CHNLSVC.Finance.GetPCType(_acclist[0].Haa_pc);
                string pccat = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    pccat = dt.Rows[0][0].ToString();
                }
                string yrrange = "";
                _acclist[0].HAA_TAG_PER = _acclist[0].Haa_tot_clos_bal;
                _bonus = CHNLSVC.Finance.GetCollecBonusDet(_acclist[0].Haa_com, _arrper, Math.Round(_year, 2), pccat.Trim(), _acclist[0].Haa_date, _acclist[0].Haa_tot_no_of_act_acc, _acclist[0].HAA_TAG_PER, _list[0].hmfa_bonus_st_dt, _acclist[0].Haa_pc);
                if (_bonus != null)
                {
                    if (_bonus.Count > 0)
                    {
                        bonus_rt = _bonus.First().hbp_bnsper;
                        _acclist[0].HAA_TAG_ACCT = _bonus.First().hbp_taccount;
                        _acclist[0].HAA_TAG_PER = _acclist[0].Haa_tot_clos_bal;
                        yrrange = yrrange + "(" + _bonus.First().hbp_sr_fyear.ToString() + "-" + _bonus.First().hbp_sr_tyear.ToString() + ")";
                    }
                }
                string yrstr = _year.ToString() + " " + yrrange;
                Session["year"] = _year.ToString();
                _acclist[0].HAA_BONUS_RT = bonus_rt;
                _acclist[0].HAA_BONUS_AMT = _acclist[0].HAA_BONUS_RT * totremi / 100;
                // _acclist[0].per
                //epf rate esp rate
                DataTable _dtepf = CHNLSVC.Finance.GetEPF_ESP(_acclist[0].Haa_com, _acclist[0].Haa_pc, _acclist[0].Haa_date);
                if (_dtepf != null)
                {
                    if (_dtepf.Rows.Count > 0)
                    {
                        _acclist[0].HAA_EPF_RT = Convert.ToDecimal(_dtepf.Rows[0]["mpch_epf"].ToString());
                        _acclist[0].HAA_ESD_RT = Convert.ToDecimal(_dtepf.Rows[0]["mpch_esd"].ToString());
                    }
                }
                _acclist[0].Years = _year;
                _acclist[0].mangercreadate = ManagerCrdt;
                _acclist[0].bonusstartdate = _list[0].hmfa_bonus_st_dt;
                _acclist[0].Pccat = pccat.Trim();
                Session["hdracc2"] = _acclist;

                return Json(new { success = true, login = true, list = _acclist, prvarr = prvarr, prvadj = prvadj, prsupcoladj = prsupcoladj, prgrccolladj = prgrccolladj, prtotclbal = prtotclbal, prtotnumact = prtotnumact, prmsupcoll = prmsupcoll, dsprefu = dsprefu, dates = yrstr }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetBonusCorrectData(string arreper, string remitance)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    List<hpt_arr_acc> _acclist = Session["hdracc2"] as List<hpt_arr_acc>;
                    string yrrange = "";
                    List<BonusDefinition> _bonus = CHNLSVC.Finance.GetCollecBonusDet(_acclist[0].Haa_com, Convert.ToDecimal(arreper), _acclist[0].Years, _acclist[0].Pccat, _acclist[0].Haa_date, _acclist[0].Haa_tot_no_of_act_acc, _acclist[0].HAA_TAG_PER, _acclist[0].bonusstartdate, _acclist[0].Haa_pc);
                    if (_bonus != null)
                    {
                        if (_bonus.Count > 0)
                        {
                            _acclist.First().HAA_BONUS_RT = _bonus.First().hbp_bnsper;
                            _acclist.First().HAA_TAG_ACCT = _bonus.First().hbp_taccount;
                            _acclist[0].HAA_BONUS_AMT = _acclist[0].HAA_BONUS_RT * Convert.ToDecimal(remitance) / 100 + _acclist[0].HAA_BONUS_REF_DED;
                            yrrange = yrrange + "(" + _bonus.First().hbp_sr_fyear.ToString() + "-" + _bonus.First().hbp_sr_tyear.ToString() + ")";
                        }
                    }
                    Session["hdracc2"] = _acclist;
                    return Json(new { success = true, login = true, bonusrate = _acclist.First().HAA_BONUS_RT, targetacc = _acclist.First().HAA_TAG_ACCT, bonusamt = _acclist[0].HAA_BONUS_AMT, dates = Session["year"].ToString() + " " + yrrange }, JsonRequestBehavior.AllowGet);
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

        //public JsonResult AccountsTextChange(string _arrper, string com, string pccat, string pc, string _year, string noacc, string closingbal, string monthremit, string Manager)
        //{
        //    decimal rate = 0;
        //    decimal targetacc = 0;
        //    decimal targetper = 0;
        //    decimal bonusamt = 0;
        //    DateTime ManagerCrdt = DateTime.Now;
        //    List<MgrCreation> _list = new List<MgrCreation>();
        //    _list = CHNLSVC.Finance.GetManagerDetails(com, "", Manager);
        //    if (_list != null && _list.Count > 0)
        //    {
        //        string _mainshop = _list.First().hmfa_mainpc;
        //        List<MgrCreation> _mainlist = _list.Where(a => a.hmfa_pc == _mainshop).ToList();
        //        ManagerCrdt = _mainlist.First().hmfa_acc_dt;
        //    }
        //    List<BonusDefinition> _bonus = CHNLSVC.Finance.GetCollecBonusDet(com, Convert.ToDecimal(_arrper), Convert.ToInt32(_year), pccat.Trim(), ManagerCrdt, Convert.ToInt32(noacc));
        //    if (_bonus != null)
        //    {
        //        if (_bonus.Count > 0)
        //        {
        //            rate = _bonus.First().hbp_bnsper;
        //            //_acclist[0].HAA_TAG_ACCT
        //            targetacc = _bonus.First().hbp_taccount;
        //            // _acclist[0].HAA_TAG_PER 
        //            targetper = Convert.ToDecimal(closingbal) / Convert.ToInt32(noacc);
        //        }
        //    }
        //    // _acclist[0].HAA_BONUS_AMT 
        //    bonusamt = rate * Convert.ToDecimal(monthremit) / 100;
        //    return Json(new { success = true, login = true, rate = rate, targetacc = targetacc, targetper = targetper, bonusamt = bonusamt }, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult SaveAdjDetails(string grossbonus, string epfval, string esd, string totalnetbonus, string adjesments, string deduc,
            string Accrefund, string Accrefrmk, string Accdedu, string Accdedurmk, string Invrefund, string Invrefrmk, string Invdedu,
            string Invdedurmk, string Credrefund, string Credrefrmk, string Creddedu, string Creddedurmk, string effectdate, string spcremk, string spcval)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string err = "";
                bool isupdate = false;
                List<hpt_arr_acc> _list = Session["hdracc2"] as List<hpt_arr_acc>;
                if (_list[0].HAA_EFFECT_DT.Date == Convert.ToDateTime(effectdate).AddMonths(1).AddDays(-1).Date)
                {
                    isupdate = true;
                }
                _list[0].oldnetbonus = _list[0].HAA_TOT_NET_BONUS;
                _list[0].HAA_GRO_COLL_BONUS = Convert.ToDecimal(grossbonus);
                _list[0].HAA_EPF_RT = Convert.ToDecimal(epfval);
                _list[0].HAA_ESD_RT = Convert.ToDecimal(esd);
                _list[0].HAA_TOT_NET_BONUS = Convert.ToDecimal(totalnetbonus);
                _list[0].HAA_BONUS_REF_DED = Convert.ToDecimal(deduc);
                _list[0].Haa_anal2 = "2";
                _list[0].HAA_SPC_RMK = spcremk;
                // _list[0].HAA_DISP_ADJ = _list[0].HAA_TOT_NET_BONUS - _list[0].oldnetbonus;
                _list[0].HAA_EFFECT_DT = Convert.ToDateTime(effectdate).AddMonths(1).AddDays(-1).Date;
                if (Accrefund != "")
                {
                    _list[0].HAA_ACC_DEPT_REFUND = Convert.ToDecimal(Accrefund);
                }
                else
                {
                    _list[0].HAA_ACC_DEPT_REFUND = 0;
                }
                if (Accdedu != "")
                {
                    _list[0].HAA_ACC_DEPT_DEDUC = Convert.ToDecimal(Accdedu);
                }
                else
                {
                    _list[0].HAA_ACC_DEPT_DEDUC = 0;
                }
                if (Invrefund != "")
                {
                    _list[0].HAA_INV_DEPT_REFUND = Convert.ToDecimal(Invrefund);
                }
                else
                {
                    _list[0].HAA_INV_DEPT_REFUND = 0;
                }
                if (Invdedu != "")
                {
                    _list[0].HAA_INV_DEPT_DEDUC = Convert.ToDecimal(Invdedu);
                }
                else
                {
                    _list[0].HAA_INV_DEPT_DEDUC = 0;
                }
                if (Credrefund != "")
                {
                    _list[0].HAA_CRED_DEPT_REFUND = Convert.ToDecimal(Credrefund);
                }
                else
                {
                    _list[0].HAA_CRED_DEPT_REFUND = 0;
                }
                if (Creddedu != "")
                {
                    _list[0].HAA_CRED_DEPT_DEDUC = Convert.ToDecimal(Creddedu);
                }
                else
                {
                    _list[0].HAA_CRED_DEPT_DEDUC = 0;
                }
                _list[0].HAA_ACC_DEPT_DEDUCRMK = Accdedurmk;
                _list[0].HAA_ACC_DEPT_REFUREMK = Accrefrmk;
                _list[0].HAA_INV_DEPT_DEDUCRMK = Invdedurmk;
                _list[0].HAA_INV_DEPT_REFUREMK = Invrefrmk;
                _list[0].HAA_CRED_DEPT_DEDUCRMK = Creddedurmk;
                _list[0].HAA_CRED_DEPT_REFUREMK = Credrefrmk;
                _list[0].HAA_SPC_VAL = Convert.ToDecimal(spcval);
                //save
                int effect = 0;
                if (isupdate)
                {
                    effect = CHNLSVC.Finance.UpdateARR_ACCNew(_list, out err);
                }
                else
                {
                    //save
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
        public JsonResult Finalize(string grossbonus, string ded_ref, string netbonus, string totalnetbonus)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string err = "";

                List<hpt_arr_acc> _list = Session["hdracc2"] as List<hpt_arr_acc>;
                hpt_col_bonus_vou _ob = new hpt_col_bonus_vou();
                _ob.arranal1 = Convert.ToInt32(_list[0].Haa_anal2);
                _ob.hpbv_claim_by = userId;
                _ob.hpbv_claim_dt = DateTime.Now;
                _ob.hpbv_claim_stus = "0";
                _ob.hpbv_com = _list[0].Haa_com;
                _ob.hpbv_cre_by = userId;
                _ob.hpbv_cre_dt = DateTime.Now;

                if (Convert.ToDecimal(ded_ref) > 0)
                {
                    _ob.hpbv_refund = Convert.ToDecimal(ded_ref);
                }
                else
                {
                    _ob.hpbv_deduct = Convert.ToDecimal(ded_ref);
                }


                _ob.hpbv_gross_bonus = Convert.ToDecimal(grossbonus);
                _ob.hpbv_month = _list[0].Haa_date;
                _ob.hpbv_net_bonus = Convert.ToDecimal(netbonus);
                _ob.hpbv_pc = _list[0].Haa_pc;



                //Auto Number
                MasterAutoNumber mastAutoNo = new MasterAutoNumber();
                mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                mastAutoNo.Aut_cate_tp = "COM";
                mastAutoNo.Aut_direction = 1;
                mastAutoNo.Aut_moduleid = "VOU";
                mastAutoNo.Aut_start_char = "VOU";
                mastAutoNo.Aut_year = DateTime.Now.Year;

                //save
                int effect = CHNLSVC.Finance.FinalizCollecBonus(_ob, mastAutoNo, out err);
                if (effect >= 0)
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
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateClosingbal(string closingbal, string accounts)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    string err = "";
                    List<hpt_arr_acc> lst = Session["hdracc2"] as List<hpt_arr_acc>;
                    if (lst != null && lst.Count > 0)
                    {
                        Int64 seq = lst.First().Haa_seq;
                        int effect = CHNLSVC.Finance.UPDATE_ClosingBalAccounts(Convert.ToInt64(accounts), Convert.ToDecimal(closingbal), seq, out err);
                        if (effect >= 0)
                        {
                            return Json(new { success = true, login = true, msg = "Successfuly Saved!", Type = "Success" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = err, type = "Error" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Please Select One Of Location", Type = "Info" }, JsonRequestBehavior.AllowGet);
                    }


                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateRemarks(HttpPostedFileBase uploadedFile)
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
                            List<hpt_arr_acc> _lst = new List<hpt_arr_acc>();
                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                string _loc = "";
                                string _date = "";

                                decimal accref = 0;
                                string accrefremk = "";
                                decimal accded = 0;
                                string accdedrmk = "";

                                decimal invref = 0;
                                string invrefremk = "";
                                decimal invded = 0;
                                string invdedrmk = "";

                                decimal credref = 0;
                                string credrefremk = "";
                                decimal credded = 0;
                                string creddedrmk = "";



                                if (workSheet.Cells[rowIterator, 1].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    _date = workSheet.Cells[rowIterator, 1].Value.ToString();

                                }
                                if (workSheet.Cells[rowIterator, 2].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    _loc = workSheet.Cells[rowIterator, 2].Value.ToString();

                                }
                                if (workSheet.Cells[rowIterator, 3].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {

                                    int n;
                                    bool isNumeric = int.TryParse(workSheet.Cells[rowIterator, 3].Value.ToString(), out n);

                                    if (isNumeric)
                                    {
                                        accref = Convert.ToDecimal(workSheet.Cells[rowIterator, 3].Value.ToString());
                                        if (accref < 0)
                                        {
                                            return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Invalid Value!!", data = "" }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    else
                                    {
                                        return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data Incorrect", data = "" }, JsonRequestBehavior.AllowGet);
                                    }

                                }
                                if (workSheet.Cells[rowIterator, 4].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    accrefremk = workSheet.Cells[rowIterator, 4].Value.ToString();

                                }
                                if (workSheet.Cells[rowIterator, 5].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {

                                    int n;
                                    bool isNumeric = int.TryParse(workSheet.Cells[rowIterator, 5].Value.ToString(), out n);

                                    if (isNumeric)
                                    {
                                        accded = Convert.ToDecimal(workSheet.Cells[rowIterator, 5].Value.ToString());
                                        if (accded < 0)
                                        {
                                            return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Invalid Value!!", data = "" }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    else
                                    {
                                        return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data Incorrect", data = "" }, JsonRequestBehavior.AllowGet);
                                    }

                                }
                                if (workSheet.Cells[rowIterator, 6].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    accdedrmk = workSheet.Cells[rowIterator, 6].Value.ToString();

                                }
                                if (workSheet.Cells[rowIterator, 7].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {

                                    int n;
                                    bool isNumeric = int.TryParse(workSheet.Cells[rowIterator, 7].Value.ToString(), out n);

                                    if (isNumeric)
                                    {
                                        invref = Convert.ToDecimal(workSheet.Cells[rowIterator, 7].Value.ToString());
                                        if (invref < 0)
                                        {
                                            return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Invalid Value!!", data = "" }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    else
                                    {
                                        return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data Incorrect", data = "" }, JsonRequestBehavior.AllowGet);
                                    }

                                }
                                if (workSheet.Cells[rowIterator, 8].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    invrefremk = workSheet.Cells[rowIterator, 8].Value.ToString();

                                }
                                if (workSheet.Cells[rowIterator, 9].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {

                                    int n;
                                    bool isNumeric = int.TryParse(workSheet.Cells[rowIterator, 9].Value.ToString(), out n);

                                    if (isNumeric)
                                    {
                                        invded = Convert.ToDecimal(workSheet.Cells[rowIterator, 9].Value.ToString());
                                        if (invded < 0)
                                        {
                                            return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Invalid Value!!", data = "" }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    else
                                    {
                                        return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data Incorrect", data = "" }, JsonRequestBehavior.AllowGet);
                                    }

                                }
                                if (workSheet.Cells[rowIterator, 10].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    invdedrmk = workSheet.Cells[rowIterator, 10].Value.ToString();

                                }
                                if (workSheet.Cells[rowIterator, 11].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {

                                    int n;
                                    bool isNumeric = int.TryParse(workSheet.Cells[rowIterator, 11].Value.ToString(), out n);

                                    if (isNumeric)
                                    {
                                        credref = Convert.ToDecimal(workSheet.Cells[rowIterator, 11].Value.ToString());
                                        if (credref < 0)
                                        {
                                            return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Invalid Value!!", data = "" }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    else
                                    {
                                        return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data Incorrect", data = "" }, JsonRequestBehavior.AllowGet);
                                    }

                                }
                                if (workSheet.Cells[rowIterator, 12].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    credrefremk = workSheet.Cells[rowIterator, 12].Value.ToString();

                                }
                                if (workSheet.Cells[rowIterator, 13].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {

                                    int n;
                                    bool isNumeric = int.TryParse(workSheet.Cells[rowIterator, 13].Value.ToString(), out n);

                                    if (isNumeric)
                                    {
                                        credded = Convert.ToDecimal(workSheet.Cells[rowIterator, 13].Value.ToString());
                                        if (credded < 0)
                                        {
                                            return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Invalid Value!!", data = "" }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    else
                                    {
                                        return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data Incorrect", data = "" }, JsonRequestBehavior.AllowGet);
                                    }

                                }
                                if (workSheet.Cells[rowIterator, 14].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    creddedrmk = workSheet.Cells[rowIterator, 14].Value.ToString();

                                }
                                hpt_arr_acc ob = new hpt_arr_acc();
                                ob.Haa_date = Convert.ToDateTime(_date);
                                ob.Haa_pc = _loc;
                                ob.HAA_ACC_DEPT_REFUND = accref;
                                if (accrefremk.Length >100)
                                {
                                    accrefremk = accrefremk.Substring(0,99);
                                }
                                ob.HAA_ACC_DEPT_REFUREMK = accrefremk;
                                ob.HAA_ACC_DEPT_DEDUC = accded;
                                if (accdedrmk.Length > 100)
                                {
                                    accdedrmk = accdedrmk.Substring(0, 99);
                                }
                                ob.HAA_ACC_DEPT_DEDUCRMK = accdedrmk;
                                ob.HAA_INV_DEPT_REFUND = invref;
                                if (invrefremk.Length > 100)
                                {
                                    invrefremk = invrefremk.Substring(0, 99);
                                }
                                ob.HAA_INV_DEPT_REFUREMK = invrefremk;
                                ob.HAA_INV_DEPT_DEDUC = invded;
                                if (invdedrmk.Length > 100)
                                {
                                    invdedrmk = invdedrmk.Substring(0, 99);
                                }
                                ob.HAA_INV_DEPT_DEDUCRMK = invdedrmk;
                                ob.HAA_CRED_DEPT_REFUND = credref;
                                if (credrefremk.Length > 100)
                                {
                                    credrefremk = credrefremk.Substring(0, 99);
                                }
                                ob.HAA_CRED_DEPT_REFUREMK = credrefremk;
                                ob.HAA_CRED_DEPT_DEDUC = credded;
                                if (creddedrmk.Length > 100)
                                {
                                    creddedrmk = creddedrmk.Substring(0, 99);
                                }
                                ob.HAA_CRED_DEPT_DEDUCRMK = creddedrmk;

                                _lst.Add(ob);
                            }
                            string err="";
                            int effect = CHNLSVC.Finance.UpdateHPRAccRemks(_lst,out err);
                            if (effect==1)
                            {
                                return Json(new { success = true, login = true, msg = "Sucessfully Updated" }, JsonRequestBehavior.AllowGet);
                            }
                            else if (effect == 0)
                            {
                                return Json(new { success = false, login = true, msg = err, data = "" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = err, data = "" }, JsonRequestBehavior.AllowGet);
                            }

                           
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