using FF.BusinessObjects;
using FF.BusinessObjects.BITool;
using FF.BusinessObjects.Commission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FF.SCMWebMVC.Controllers
{
    public class CommissionDefinitionController : BaseController
    {
        //
        // GET: /CommissionDefinition/

        List<Commission_pc> pclist = new List<Commission_pc>();
        List<Override_Commission> Ov_commission = new List<Override_Commission>();
        List<Item_Value_Commission> Item_value_commission = new List<Item_Value_Commission>();
        List<ref_comm_target> target_commission = new List<ref_comm_target>();
        List<ref_comm_target_ovrt> target_commission_ovt = new List<ref_comm_target_ovrt>();
        List<ref_comm_collect_ovrt> collect_ovrt = new List<ref_comm_collect_ovrt>();
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["pc_list"] = null;
                Session["Ov_commission"] = null;
                Session["Item_value_commission"] = null;
                Session["target_commission"] = null;
                Session["target_commission_ovt"] = null;
                Session["collect_ovrt"] = null;
                Session["ref_comm_add_trgt"] = null;
                Session["ref_eli_comm_targ"] = null;
                return View();

            }
            else
            {
                return Redirect("~/Login/index");
            }
        }
        public JsonResult AddProfitCenters(string proficenter, string AVGTarget)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (Session["pc_list"] == null)
                {
                    pclist = new List<Commission_pc>();
                    Commission_pc ob = new Commission_pc();
                    ob.pccode = proficenter;
                    ob.avgtarget = AVGTarget;
                    pclist.Add(ob);
                    Session["pc_list"] = pclist;
                }
                else
                {
                    pclist = Session["pc_list"] as List<Commission_pc>;
                    var count = pclist.Where(a => a.pccode == proficenter).Count();
                    if (count > 0)
                    {
                        return Json(new { success = false, login = true, msg = "Already added this pc!!", data = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Commission_pc ob = new Commission_pc();
                        ob.pccode = proficenter;
                        ob.avgtarget = AVGTarget;
                        pclist.Add(ob);
                        Session["pc_list"] = pclist;
                    }
                }

                if (pclist.Count > 0)
                {
                    return Json(new { success = true, login = true, data = pclist }, JsonRequestBehavior.AllowGet);
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
        public JsonResult RemovePCCode(string profitcenter)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                if (Session["pc_list"] != null)
                {
                    pclist = (List<Commission_pc>)Session["pc_list"];
                }
                else
                {
                    pclist = new List<Commission_pc>();

                }
                var itemToRemove = pclist.First(r => r.pccode == profitcenter);
                pclist.Remove(itemToRemove);
                Session["pc_list"] = pclist;
                return Json(new { success = true, login = true, data = pclist }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddOverrideEmployee(string employee, string commission, string Epf, string StartDate, string EndDates, string Ovrt, string invbtu)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (StartDate == "")
                {
                    StartDate = "0";
                }
                if (EndDates == "")
                {
                    EndDates = "0";
                }
                if (Session["Ov_commission"] == null)
                {
                    Ov_commission = new List<Override_Commission>();
                    Override_Commission ob = new Override_Commission();
                    ob.Commission = Convert.ToDecimal(commission);
                    ob.Epf = Epf;
                    ob.EmpCode = employee;
                    ob.startdays = Convert.ToInt32(StartDate);
                    ob.enddays = Convert.ToInt32(EndDates);
                    ob.Ovrt = Ovrt;
                    ob.btuinv = invbtu;
                    Ov_commission.Add(ob);
                    Session["Ov_commission"] = Ov_commission;
                }
                else
                {
                    Ov_commission = Session["Ov_commission"] as List<Override_Commission>;
                    var count = Ov_commission.Where(a => a.Epf == Epf && a.startdays == Convert.ToInt32(StartDate) && a.enddays == Convert.ToInt32(EndDates) && a.Commission == Convert.ToDecimal(commission)).Count();
                    if (count > 0)
                    {
                        return Json(new { success = false, login = true, msg = "Already added this Employee!!", data = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Override_Commission ob = new Override_Commission();
                        ob.Commission = Convert.ToDecimal(commission);
                        ob.EmpCode = employee;
                        ob.Epf = Epf;
                        ob.startdays = Convert.ToInt32(StartDate);
                        ob.enddays = Convert.ToInt32(EndDates);
                        ob.Ovrt = Ovrt;
                        ob.btuinv = invbtu;
                        Ov_commission.Add(ob);
                        Session["Ov_commission"] = Ov_commission;
                    }
                }

                if (Ov_commission.Count > 0)
                {
                    return Json(new { success = true, login = true, data = Ov_commission }, JsonRequestBehavior.AllowGet);
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
        public JsonResult RemoveEmpCode(string EmpCode, string Daterange, string comm)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                if (Session["Ov_commission"] != null)
                {
                    Ov_commission = (List<Override_Commission>)Session["Ov_commission"];
                }
                else
                {
                    Ov_commission = new List<Override_Commission>();

                }
                var itemToRemove = Ov_commission.First(r => r.Epf == EmpCode && (r.startdays.ToString() + "-" + r.enddays.ToString()) == Daterange && r.Commission == Convert.ToDecimal(comm));
                Ov_commission.Remove(itemToRemove);
                Session["Ov_commission"] = Ov_commission;
                return Json(new { success = true, login = true, data = Ov_commission }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddItemValueRange(string Itemcode, string Model, string Brand, string Cat1, string Cat2, string MinVal, string MaxVal, string Stlstart, string Stlend, string Commission, string invtype)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (Session["Item_value_commission"] == null)
                {
                    Item_value_commission = new List<Item_Value_Commission>();
                    Item_Value_Commission ob = new Item_Value_Commission();
                    ob.ItemBrand = Brand;
                    ob.ItemCode = Itemcode;
                    ob.ItemModel = Model;
                    ob.MaxValue = Convert.ToDecimal(MaxVal);
                    ob.MinValue = Convert.ToDecimal(MinVal);
                    ob.Range = ob.MinValue.ToString() + "-" + ob.MaxValue.ToString();
                    ob.Cat1 = Cat1;
                    ob.Cat2 = Cat2;
                    ob.StlmntStDays = Convert.ToInt32(Stlstart);
                    ob.StlmntEndDays = Convert.ToInt32(Stlend);
                    ob.Commission = Convert.ToDecimal(Commission);
                    ob.SettlRange = ob.StlmntStDays.ToString() + "-" + ob.StlmntEndDays.ToString();
                    ob.InvType = invtype;
                    Item_value_commission.Add(ob);
                    Session["Item_value_commission"] = Item_value_commission;
                }
                else
                {
                    Item_value_commission = Session["Item_value_commission"] as List<Item_Value_Commission>;
                    var count = Item_value_commission.Where(a => a.ItemCode == Itemcode && a.ItemModel == Model && a.ItemBrand == Brand && a.Cat1 == Cat1 && a.Cat2 == Cat2 && a.MaxValue == Convert.ToDecimal(MaxVal) && a.MinValue == Convert.ToDecimal(MinVal) && a.StlmntStDays == Convert.ToInt32(Stlstart) && a.StlmntEndDays == Convert.ToInt32(Stlend) && a.InvType == invtype).Count();
                    if (count > 0)
                    {
                        return Json(new { success = false, login = true, msg = "Already added this  Range!!", data = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Item_Value_Commission ob = new Item_Value_Commission();
                        ob.ItemBrand = Brand;
                        ob.ItemCode = Itemcode;
                        ob.ItemModel = Model;
                        ob.MaxValue = Convert.ToDecimal(MaxVal);
                        ob.MinValue = Convert.ToDecimal(MinVal);
                        ob.Range = ob.MinValue.ToString() + "-" + ob.MaxValue.ToString();
                        ob.Cat1 = Cat1;
                        ob.Cat2 = Cat2;
                        ob.StlmntStDays = Convert.ToInt32(Stlstart);
                        ob.StlmntEndDays = Convert.ToInt32(Stlend);
                        ob.SettlRange = ob.StlmntStDays.ToString() + "-" + ob.StlmntEndDays.ToString();
                        ob.Commission = Convert.ToDecimal(Commission);
                        ob.InvType = invtype;
                        Item_value_commission.Add(ob);
                        Session["Item_value_commission"] = Item_value_commission;
                    }
                }

                if (Item_value_commission.Count > 0)
                {
                    return Json(new { success = true, login = true, data = Item_value_commission.Take(500).ToList() }, JsonRequestBehavior.AllowGet);
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
        public JsonResult RemoveItemValueRange(string ItemCode, string Model, string Brand, string Cat1, string Cat2, string valrange, string daterange, string invtype)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                if (Session["Item_value_commission"] != null)
                {
                    Item_value_commission = (List<Item_Value_Commission>)Session["Item_value_commission"];
                }
                else
                {
                    Item_value_commission = new List<Item_Value_Commission>();

                }
                var itemToRemove = Item_value_commission.First(r => r.ItemCode == ItemCode && r.ItemModel == Model && r.ItemBrand == Brand && r.Cat1 == Cat1 && r.Cat2 == Cat2 && r.Range.ToString() == valrange && r.SettlRange.ToString() == daterange && r.InvType == invtype);
                Item_value_commission.Remove(itemToRemove);
                Session["Item_value_commission"] = Item_value_commission;
                return Json(new { success = true, login = true, data = Item_value_commission.Take(500).ToList() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveCommissionDefinition(string FromDate, string ToDate, string Period, string SaleType, string SaleMode, string CommiType, string CommissionCode, string Active, string CollectType)
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
                mastAutoNo.Aut_moduleid = "COMM";
                mastAutoNo.Aut_start_char = "COMM";
                mastAutoNo.Aut_year = DateTime.Now.Year;
                //hdr
                ref_comm_hdr commhdr = new ref_comm_hdr();
                commhdr.Rch_act = Convert.ToInt32(Active);
                commhdr.Rch_com = company;
                commhdr.Rch_to_dt = Convert.ToDateTime(ToDate);
                commhdr.Rch_from_dt = Convert.ToDateTime(FromDate);
                commhdr.Rch_anal1 = "";
                commhdr.Rch_anal2 = "";
                commhdr.Rch_calc_type = SaleType;
                commhdr.Rch_comm_type = CommiType;
                commhdr.Rch_cre_by = userId;
                commhdr.Rch_cre_dt = DateTime.Now.Date;
                commhdr.Rch_cre_session = Session["SessionID"].ToString();
                commhdr.Rch_mod_by = userId;
                commhdr.Rch_mod_dt = DateTime.Now.Date;
                commhdr.Rch_mod_session = Session["SessionID"].ToString();
                commhdr.Rch_sales_type = SaleMode;
                commhdr.Rch_settl_period = Convert.ToInt32(Period);
                commhdr.Rch_comm_cd = CommissionCode;
                commhdr.Rch_collect_tp = CollectType;

                List<ref_comm_det> comm_det = new List<ref_comm_det>();
                Item_value_commission = (List<Item_Value_Commission>)Session["Item_value_commission"];
                int i = 1;
                if (Item_value_commission != null)
                {
                    foreach (var ob_comm_det in Item_value_commission)
                    {
                        ref_comm_det ob = new ref_comm_det();
                        ob.Rcd_seq = 0;
                        ob.Rcd_comm_cd = commhdr.Rch_comm_cd;
                        ob.Rcd_brand = ob_comm_det.ItemBrand;
                        ob.Rcd_cat1 = ob_comm_det.Cat1;
                        ob.Rcd_cat2 = ob_comm_det.Cat2;
                        ob.Rcd_comm_val = ob_comm_det.Commission;
                        ob.Rcd_end_val = ob_comm_det.MaxValue;
                        ob.Rcd_st_val = ob_comm_det.MinValue;
                        ob.Rcd_item_cd = ob_comm_det.ItemCode;
                        ob.Rcd_line = i;
                        ob.Rcd_anal1 = ob_comm_det.StlmntStDays.ToString();
                        ob.Rcd_anal2 = ob_comm_det.StlmntEndDays.ToString();
                        ob.Rcd_inv_tp = ob_comm_det.InvType;
                        ob.Rcd_cat3 = ob_comm_det.Cat3;
                        if (ob_comm_det.Btu1 == null || ob_comm_det.Btu1 == "")
                        {
                            ob.Rcd_btu_f = "0";
                        }
                        else
                        {
                            ob.Rcd_btu_f = ob_comm_det.Btu1;
                        }
                        if (ob_comm_det.Btu2 == null || ob_comm_det.Btu2 == "")
                        {
                            ob.Rcd_btu_e = "0";
                        }
                        else
                        {
                            ob.Rcd_btu_e = ob_comm_det.Btu2;
                        }


                        comm_det.Add(ob);
                        i++;
                    }
                }


                List<ref_comm_emp> comm_emp = new List<ref_comm_emp>();
                Ov_commission = Session["Ov_commission"] as List<Override_Commission>;
                if (Ov_commission != null)
                {
                    foreach (var ob_emp in Ov_commission)
                    {
                        ref_comm_emp ob = new ref_comm_emp();
                        ob.Rce_comm_cd = commhdr.Rch_comm_cd;
                        ob.Rce_commission = ob_emp.Commission;
                        ob.Rce_emp_type = ob_emp.EmpCode;
                        ob.Rce_seq = 0;
                        ob.Rce_anal1 = ob_emp.Epf;
                        ob.Rce_st_days = ob_emp.startdays;
                        ob.Rce_end_days = ob_emp.enddays;
                        ob.Rce_anal2 = ob_emp.Ovrt;
                        ob.Rce_btu_inv = ob_emp.btuinv;

                        comm_emp.Add(ob);
                    }
                }
                List<ref_comm_pc> comm_pc = new List<ref_comm_pc>();
                pclist = (List<Commission_pc>)Session["pc_list"];
                if (pclist != null)
                {
                    if (pclist.Count == 0)
                    {
                        return Json(new { success = false, login = true, msg = "Please Select Profit Centers" }, JsonRequestBehavior.AllowGet);
                    }
                    foreach (var ob_pc in pclist)
                    {
                        ref_comm_pc ob = new ref_comm_pc();
                        ob.Rcp_comm_cd = commhdr.Rch_comm_cd;
                        ob.Rcp_pc = ob_pc.pccode;
                        ob.Rcp_seq = 0;
                        ob.Rcp_anal1 = ob_pc.avgtarget;
                        comm_pc.Add(ob);
                    }

                }
                collect_ovrt = (List<ref_comm_collect_ovrt>)Session["collect_ovrt"];
                target_commission_ovt = Session["target_commission_ovt"] as List<ref_comm_target_ovrt>;
                target_commission = Session["target_commission"] as List<ref_comm_target>;
                List<ref_comm_add_trgt> _eliteadd = Session["ref_comm_add_trgt"] as List<ref_comm_add_trgt>;
                List<ref_eli_comm_targ> _elitecomm = Session["ref_eli_comm_targ"] as List<ref_eli_comm_targ>;
                //save
                int effect = CHNLSVC.Finance.SaveCommissionDeffinition(commhdr, comm_det, comm_emp, comm_pc, target_commission, target_commission_ovt, collect_ovrt, _elitecomm, _eliteadd, mastAutoNo, out err);
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
        public JsonResult LoadDetails(string CommCode)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            CommCode = CommCode.Trim();
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<ref_comm_hdr> hdrdata = CHNLSVC.Finance.GetCommissionHDR(company, CommCode);
                List<ref_comm_det> itemdata = CHNLSVC.Finance.GetCommissionDetails(CommCode);
                List<ref_comm_emp> empdata = CHNLSVC.Finance.GetCommissionEmp(CommCode);
                List<ref_comm_pc> pcdata = CHNLSVC.Finance.GetCommissionPC(CommCode);
                List<ref_comm_target> targets = CHNLSVC.Finance.GetCommissionTargetDetails(CommCode);
                List<ref_comm_target_ovrt> targetsovt = CHNLSVC.Finance.GetCommissionTargetOvtDetails(CommCode);
                List<ref_comm_collect_ovrt> collectovt = CHNLSVC.Finance.GetCommissionCollectionOvtDetails(CommCode);
                List<ref_eli_comm_targ> _eli_comm = CHNLSVC.Finance.GetEliteCommTrgt(hdrdata.FirstOrDefault().Rch_seq);
                List<ref_comm_add_trgt> _eli_add = CHNLSVC.Finance.GetEliteCommAdditional(hdrdata.FirstOrDefault().Rch_seq);

                if (targets == null)
                {
                    targets = new List<ref_comm_target>();
                }
                if (targetsovt == null)
                {
                    targetsovt = new List<ref_comm_target_ovrt>();
                }
                if (collectovt == null)
                {
                    collectovt = new List<ref_comm_collect_ovrt>();
                }
                if (empdata == null)
                {
                    empdata = new List<ref_comm_emp>();
                }
                if (_eli_comm == null)
                {
                    _eli_comm = new List<ref_eli_comm_targ>();
                }
                if (_eli_add == null)
                {
                    _eli_add = new List<ref_comm_add_trgt>();
                }

                Session["target_commission"] = targets;
                Session["target_commission_ovt"] = targetsovt;
                Session["collect_ovrt"] = collectovt;

                //set pc
                if (pcdata != null)
                {
                    pclist = new List<Commission_pc>();
                    foreach (var pclistn in pcdata)
                    {
                        Commission_pc ob = new Commission_pc();
                        ob.pccode = pclistn.Rcp_pc;
                        ob.avgtarget = pclistn.Rcp_anal1;
                        pclist.Add(ob);
                    }
                    Session["pc_list"] = pclist;
                }
                //set item data
                if (itemdata != null)
                {
                    Item_value_commission = new List<Item_Value_Commission>();
                    foreach (var itemdatan in itemdata)
                    {
                        Item_Value_Commission ob = new Item_Value_Commission();
                        ob.ItemBrand = itemdatan.Rcd_brand;
                        ob.ItemCode = itemdatan.Rcd_item_cd;
                        ob.ItemModel = itemdatan.Rcd_model;
                        ob.MaxValue = itemdatan.Rcd_end_val;
                        ob.MinValue = itemdatan.Rcd_st_val;
                        ob.Range = ob.MinValue.ToString() + "-" + ob.MaxValue.ToString();
                        ob.Cat1 = itemdatan.Rcd_cat1;
                        ob.Cat2 = itemdatan.Rcd_cat2;
                        ob.Commission = itemdatan.Rcd_comm_val;
                        ob.SettlRange = itemdatan.Rcd_anal1 + "-" + itemdatan.Rcd_anal2;
                        ob.StlmntStDays = Convert.ToInt32(itemdatan.Rcd_anal1);
                        ob.StlmntEndDays = Convert.ToInt32(itemdatan.Rcd_anal2);
                        ob.InvType = itemdatan.Rcd_inv_tp;
                        ob.Cat3 = itemdatan.Rcd_cat3;
                        ob.Btu1 = itemdatan.Rcd_btu_f;
                        ob.Btu2 = itemdatan.Rcd_btu_e;
                        Item_value_commission.Add(ob);
                    }
                    Session["Item_value_commission"] = Item_value_commission;
                }
                //set emp data
                if (empdata != null)
                {
                    Ov_commission = new List<Override_Commission>();
                    foreach (var empdatan in empdata)
                    {
                        Override_Commission ob = new Override_Commission();
                        ob.Commission = empdatan.Rce_commission;
                        ob.EmpCode = empdatan.Rce_emp_type;
                        ob.enddays = empdatan.Rce_end_days;
                        ob.Ovrt = empdatan.Rce_anal2;
                        ob.startdays = empdatan.Rce_st_days;
                        ob.btuinv = empdatan.Rce_btu_inv;
                        ob.Epf = empdatan.Rce_anal1;
                        Ov_commission.Add(ob);
                    }
                    Session["Ov_commission"] = Ov_commission;
                }
                return Json(new { success = true, login = true, hdrdata = hdrdata, Ov_commission = Ov_commission, Item_value_commission = Item_value_commission.Take(500).ToList(), pclist = pclist, targets = targets, targetsovt = targetsovt, collectovt = collectovt, _eli_comm = _eli_comm, _eli_add = _eli_add }, JsonRequestBehavior.AllowGet);
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
                Session["pc_list"] = null;
                Session["Ov_commission"] = null;
                Session["Item_value_commission"] = null;
                Session["target_commission"] = null;
                Session["target_commission_ovt"] = null;
                Session["collect_ovrt"] = null;
                return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, msg = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetPC(string Chanal, string AVG)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<Sim_Pc> _channelpc = CHNLSVC.Dashboard.GetPcInfoData("CHNL", Chanal);
                if (_channelpc != null)
                {
                    foreach (var pcs in _channelpc)
                    {
                        Commission_pc ob = new Commission_pc();
                        ob.pccode = pcs.pc;
                        ob.avgtarget = AVG;
                        pclist.Add(ob);
                    }
                }
                pclist = pclist.GroupBy(l => new { l.pccode }).Select(cl => new Commission_pc
                {
                    pccode = cl.FirstOrDefault().pccode,
                    avgtarget = cl.FirstOrDefault().avgtarget
                }).ToList();
                Session["pc_list"] = pclist;
                return Json(new { success = true, login = true, pclist = pclist }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult TargetBaseCommissionAdd(string Stval, string Endval, string Rate, string Exec)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                if (Exec == "") Exec = "All";
                if (Session["target_commission"] == null)
                {
                    target_commission = new List<ref_comm_target>();
                    ref_comm_target ob = new ref_comm_target();
                    ob.rct_st_val = Convert.ToDecimal(Stval);
                    ob.rct_end_val = Convert.ToDecimal(Endval);
                    ob.rct_rate = Convert.ToDecimal(Rate);
                    ob.rct_anal1 = Exec;
                    target_commission.Add(ob);
                    Session["target_commission"] = target_commission;
                }
                else
                {
                    target_commission = Session["target_commission"] as List<ref_comm_target>;
                    var count = target_commission.Where(a => a.rct_st_val.ToString() == Stval && a.rct_end_val.ToString() == Endval && a.rct_rate.ToString() == Rate).Count();
                    if (count > 0)
                    {
                        return Json(new { success = false, login = true, msg = "Already added this  Range!!", data = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ref_comm_target ob = new ref_comm_target();
                        ob.rct_st_val = Convert.ToDecimal(Stval);
                        ob.rct_end_val = Convert.ToDecimal(Endval);
                        ob.rct_rate = Convert.ToDecimal(Rate);
                        ob.rct_anal1 = Exec;
                        target_commission.Add(ob);
                        Session["target_commission"] = target_commission;
                    }

                }
                return Json(new { success = true, login = true, list = target_commission }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult RemoveTargetBaseCommission(string Range, string Commission, string Exec)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                if (Session["target_commission"] != null)
                {
                    target_commission = (List<ref_comm_target>)Session["target_commission"];
                }
                else
                {
                    target_commission = new List<ref_comm_target>();

                }
                var itemToRemove = target_commission.First(r => r.rct_rate.ToString() == Commission && (r.rct_st_val.ToString() + "-" + r.rct_end_val.ToString()) == Range && r.rct_anal1 == Exec);
                target_commission.Remove(itemToRemove);
                Session["target_commission"] = target_commission;
                return Json(new { success = true, login = true, list = target_commission }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult TargetBaseOverrideAdd(string Stval, string Endval, string Rate, string emp, string empcat)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                if (Session["target_commission_ovt"] == null)
                {
                    target_commission_ovt = new List<ref_comm_target_ovrt>();
                    ref_comm_target_ovrt ob = new ref_comm_target_ovrt();
                    ob.rcto_emp_cat = empcat;
                    ob.rcto_emp_cd = emp;
                    ob.rcto_end_val = Convert.ToDecimal(Endval);
                    ob.rcto_rate = Convert.ToDecimal(Rate);
                    ob.rcto_st_val = Convert.ToDecimal(Stval);
                    target_commission_ovt.Add(ob);
                    Session["target_commission_ovt"] = target_commission_ovt;
                }
                else
                {
                    target_commission_ovt = Session["target_commission_ovt"] as List<ref_comm_target_ovrt>;
                    var count = target_commission_ovt.Where(a => a.rcto_st_val.ToString() == Stval && a.rcto_end_val.ToString() == Endval && a.rcto_rate.ToString() == Rate && a.rcto_emp_cd == emp).Count();
                    if (count > 0)
                    {
                        return Json(new { success = false, login = true, msg = "Already added this  Range!!", data = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        target_commission_ovt = Session["target_commission_ovt"] as List<ref_comm_target_ovrt>;
                        ref_comm_target_ovrt ob = new ref_comm_target_ovrt();
                        ob.rcto_emp_cat = empcat;
                        ob.rcto_emp_cd = emp;
                        ob.rcto_end_val = Convert.ToDecimal(Endval);
                        ob.rcto_rate = Convert.ToDecimal(Rate);
                        ob.rcto_st_val = Convert.ToDecimal(Stval);
                        target_commission_ovt.Add(ob);
                        Session["target_commission_ovt"] = target_commission_ovt;
                    }

                }
                return Json(new { success = true, login = true, list = target_commission_ovt }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult RemoveTargetBaseOverride(string Range, string Commission, string Emp)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                if (Session["target_commission_ovt"] != null)
                {
                    target_commission_ovt = (List<ref_comm_target_ovrt>)Session["target_commission_ovt"];
                }
                else
                {
                    target_commission_ovt = new List<ref_comm_target_ovrt>();

                }
                var itemToRemove = target_commission_ovt.First(r => r.rcto_rate.ToString() == Commission && (r.rcto_st_val.ToString() + "-" + r.rcto_end_val.ToString()) == Range && r.rcto_emp_cd == Emp);
                target_commission_ovt.Remove(itemToRemove);
                Session["target_commission_ovt"] = target_commission_ovt;
                return Json(new { success = true, login = true, list = target_commission_ovt }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CollectionBaseOverrideAdd(string Stval, string Endval, string Rate, string emp, string empcat, string invtype, string stldate_st, string stldate_end)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                if (Session["collect_ovrt"] == null)
                {
                    collect_ovrt = new List<ref_comm_collect_ovrt>();
                    ref_comm_collect_ovrt ob = new ref_comm_collect_ovrt();
                    ob.rcco_emp_cat = empcat;
                    ob.rcco_emp_cd = emp;
                    ob.rcco_end_val = Convert.ToDecimal(Endval);
                    ob.rcco_rate = Convert.ToDecimal(Rate);
                    ob.rcco_st_val = Convert.ToDecimal(Stval);
                    ob.rcco_inv_tp = invtype;
                    ob.rcco_stl_st_dt = Convert.ToInt32(stldate_st);
                    ob.rcco_stl_end_dt = Convert.ToInt32(stldate_end);
                    collect_ovrt.Add(ob);
                    Session["collect_ovrt"] = collect_ovrt;
                }
                else
                {
                    collect_ovrt = Session["collect_ovrt"] as List<ref_comm_collect_ovrt>;
                    var count = collect_ovrt.Where(a => a.rcco_st_val.ToString() == Stval && a.rcco_end_val.ToString() == Endval && a.rcco_rate.ToString() == Rate && a.rcco_emp_cd == emp && a.rcco_inv_tp == invtype && a.rcco_stl_st_dt.ToString() == stldate_st && a.rcco_stl_end_dt.ToString() == stldate_end).Count();
                    if (count > 0)
                    {
                        return Json(new { success = false, login = true, msg = "Already added this  Range!!", data = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        collect_ovrt = Session["collect_ovrt"] as List<ref_comm_collect_ovrt>;
                        ref_comm_collect_ovrt ob = new ref_comm_collect_ovrt();
                        ob.rcco_emp_cat = empcat;
                        ob.rcco_emp_cd = emp;
                        ob.rcco_end_val = Convert.ToDecimal(Endval);
                        ob.rcco_rate = Convert.ToDecimal(Rate);
                        ob.rcco_st_val = Convert.ToDecimal(Stval);
                        ob.rcco_inv_tp = invtype;
                        ob.rcco_stl_st_dt = Convert.ToInt32(stldate_st);
                        ob.rcco_stl_end_dt = Convert.ToInt32(stldate_end);
                        collect_ovrt.Add(ob);
                        Session["collect_ovrt"] = collect_ovrt;
                    }

                }
                return Json(new { success = true, login = true, list = collect_ovrt }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult RemoveCollectionBaseOverride(string Range, string Commission, string Emp, string daterange, string invtype)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                if (Session["collect_ovrt"] != null)
                {
                    collect_ovrt = (List<ref_comm_collect_ovrt>)Session["collect_ovrt"];
                }
                else
                {
                    collect_ovrt = new List<ref_comm_collect_ovrt>();

                }
                var itemToRemove = collect_ovrt.First(r => r.rcco_rate.ToString() == Commission && (r.rcco_st_val.ToString() + "-" + r.rcco_end_val.ToString()) == Range && r.rcco_emp_cd == Emp && r.rcco_inv_tp == invtype && (r.rcco_stl_st_dt.ToString() + "-" + r.rcco_stl_end_dt.ToString()) == daterange);
                collect_ovrt.Remove(itemToRemove);
                Session["collect_ovrt"] = collect_ovrt;
                return Json(new { success = true, login = true, list = collect_ovrt }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult LoadBTUType()
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
                    o1.Text = "INV";
                    o1.Value = "INV";
                    oList.Add(o1);
                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "OTHER";
                    o2.Value = "OTHER";
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
        public JsonResult BTUItemsAdd(string Stval, string Endval, string Rate, string Cat, string BTU, string BTU2, string BTUtype)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                // DataTable dt = CHNLSVC.Finance.Sp_GetbtuItems(Cat, BTUtype, BTU, BTU2);
                //if (dt != null)
                //{
                //    if (dt.Rows.Count>0)
                //    {
                //int i = 0;
                //foreach (var data in dt.Rows)
                //{
                // string itemcode = dt.Rows[i]["mi_cd"].ToString();
                if (Session["Item_value_commission"] == null)
                {
                    Item_value_commission = new List<Item_Value_Commission>();
                    Item_Value_Commission ob = new Item_Value_Commission();
                    ob.ItemBrand = "";
                    ob.ItemCode = "";
                    ob.ItemModel = "";
                    ob.MaxValue = Convert.ToDecimal(Endval);
                    ob.MinValue = Convert.ToDecimal(Stval);
                    ob.Range = ob.MinValue.ToString() + "-" + ob.MaxValue.ToString();
                    ob.Cat1 = Cat;
                    ob.Cat2 = "";
                    ob.Commission = Convert.ToDecimal(Rate);
                    ob.SettlRange = ob.StlmntStDays.ToString() + "-" + ob.StlmntEndDays.ToString();
                    ob.InvType = "";
                    ob.Btu1 = BTU;
                    ob.Btu2 = BTU2;
                    ob.Cat3 = BTUtype;
                    Item_value_commission.Add(ob);
                    Session["Item_value_commission"] = Item_value_commission;
                }
                else
                {
                    Item_value_commission = Session["Item_value_commission"] as List<Item_Value_Commission>;
                    var count = Item_value_commission.Where(a => a.Cat1 == Cat && a.MaxValue == Convert.ToDecimal(Endval) && a.MinValue == Convert.ToDecimal(Stval) && a.Cat3 == BTUtype && a.Btu1 == BTU && a.Btu2 == BTU2).Count();
                    if (count > 0)
                    {
                        return Json(new { success = false, login = true, msg = " Already added this  Range!!", data = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Item_Value_Commission ob = new Item_Value_Commission();
                        ob.ItemBrand = "";
                        ob.ItemCode = "";
                        ob.ItemModel = "";
                        ob.MaxValue = Convert.ToDecimal(Endval);
                        ob.MinValue = Convert.ToDecimal(Stval);
                        ob.Range = ob.MinValue.ToString() + "-" + ob.MaxValue.ToString();
                        ob.Cat1 = Cat;
                        ob.Cat2 = "";
                        ob.Commission = Convert.ToDecimal(Rate);
                        ob.SettlRange = ob.StlmntStDays.ToString() + "-" + ob.StlmntEndDays.ToString();
                        ob.InvType = "";
                        ob.Btu1 = BTU;
                        ob.Btu2 = BTU2;
                        ob.Cat3 = BTUtype;
                        Item_value_commission.Add(ob);
                        Session["Item_value_commission"] = Item_value_commission;
                    }
                }
                // i++;
                // }
                //  }
                //  }


                return Json(new { success = true, login = true, data = Item_value_commission.Take(500).ToList() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult LoadTarget()
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
                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "Select";
                    o2.Value = "";
                    oList.Add(o2);

                    ComboBoxObject o1 = new ComboBoxObject();
                    o1.Text = "Manager";
                    o1.Value = "MNG";
                    oList.Add(o1);
                    ComboBoxObject o3 = new ComboBoxObject();
                    o3.Text = "Excecutive";
                    o3.Value = "EXC";
                    oList.Add(o3);

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
        public JsonResult LoadAdditionalTarget()
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
                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "Select";
                    o2.Value = "";
                    oList.Add(o2);

                    ComboBoxObject o1 = new ComboBoxObject();
                    o1.Text = "Singal";
                    o1.Value = "S";
                    oList.Add(o1);
                    ComboBoxObject o3 = new ComboBoxObject();
                    o3.Text = "All";
                    o3.Value = "A";
                    oList.Add(o3);

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
        public JsonResult AddEliteItems(string pc, string TargetType, string Month, string TargetVal, string fromper, string toper, string mngcomm, string execomm, string ovrcashcomm, string ovrhelpcomm)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<ref_eli_comm_targ> ref_eli_comm_targg = new List<ref_eli_comm_targ>();
                if (Session["ref_eli_comm_targ"] == null)
                {
                    ref_eli_comm_targg = new List<ref_eli_comm_targ>();
                    ref_eli_comm_targ ob = new ref_eli_comm_targ();
                    ob.rect_pc = pc;
                    ob.rect_com = company;
                    ob.rect_tp = TargetType;
                    ob.rect_month = Convert.ToDateTime(Month).Month;
                    ob.rect_target = Convert.ToDecimal(TargetVal);
                    ob.rect_frm_per = Convert.ToDecimal(fromper);
                    ob.rect_to_per = Convert.ToDecimal(toper);
                    ob.rect_mng_comm = Convert.ToDecimal(mngcomm);
                    ob.rect_exc_comm = Convert.ToDecimal(execomm);
                    ob.rect_cashi_comm = Convert.ToDecimal(ovrcashcomm);
                    ob.rect_help_comm = Convert.ToDecimal(ovrhelpcomm);
                    ref_eli_comm_targg.Add(ob);
                    Session["ref_eli_comm_targ"] = ref_eli_comm_targg;
                }
                else
                {
                    ref_eli_comm_targg = Session["ref_eli_comm_targ"] as List<ref_eli_comm_targ>;
                    var count = ref_eli_comm_targg.Where(a => a.rect_pc == pc && a.rect_tp == TargetType && a.rect_month == Convert.ToDateTime(Month).Month).Count();
                    if (count > 0)
                    {
                        return Json(new { success = false, login = true, msg = " Already added this  Range!!", data = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ref_eli_comm_targ ob = new ref_eli_comm_targ();
                        ob.rect_pc = pc;
                        ob.rect_tp = TargetType;
                        ob.rect_month = Convert.ToDateTime(Month).Month;
                        ob.rect_target = Convert.ToDecimal(TargetVal);
                        ob.rect_frm_per = Convert.ToDecimal(fromper);
                        ob.rect_to_per = Convert.ToDecimal(toper);
                        ob.rect_mng_comm = Convert.ToDecimal(mngcomm);
                        ob.rect_exc_comm = Convert.ToDecimal(execomm);
                        ob.rect_cashi_comm = Convert.ToDecimal(ovrcashcomm);
                        ob.rect_help_comm = Convert.ToDecimal(ovrhelpcomm);
                        ob.rect_com = company;
                        ref_eli_comm_targg.Add(ob);
                        Session["ref_eli_comm_targ"] = ref_eli_comm_targg;
                    }
                }
                return Json(new { success = true, login = true, data = ref_eli_comm_targg }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult RemoveEliteItemValueRange(string pc, string Type, string Month)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<ref_eli_comm_targ> ref_eli_comm_targg = new List<ref_eli_comm_targ>();
                if (Session["ref_eli_comm_targ"] != null)
                {
                    ref_eli_comm_targg = (List<ref_eli_comm_targ>)Session["ref_eli_comm_targ"];
                }
                else
                {
                    ref_eli_comm_targg = new List<ref_eli_comm_targ>();

                }
                var itemToRemove = ref_eli_comm_targg.First(r => r.rect_pc == pc && r.rect_tp == Type &&  r.rect_month==Convert.ToInt32(Month));
                ref_eli_comm_targg.Remove(itemToRemove);
                Session["ref_eli_comm_targ"] = ref_eli_comm_targg;
                return Json(new { success = true, login = true, data = pclist }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddEliteAdditinalItems(string pc, string slab, string from, string to, string comm, string type2, string gap)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<ref_comm_add_trgt> ref_comm_add_trgtg = new List<ref_comm_add_trgt>();
                if (Session["ref_comm_add_trgt"] == null)
                {
                    ref_comm_add_trgtg = new List<ref_comm_add_trgt>();
                    ref_comm_add_trgt ob = new ref_comm_add_trgt();
                    ob.rcat_pc = pc;
                    ob.rcat_slab = Convert.ToInt32(slab) ;
                    ob.rcat_from = Convert.ToDecimal(from);
                    ob.rcat_to = Convert.ToDecimal(to);
                    ob.rcat_comm = Convert.ToDecimal(comm);
                    ob.rcat_type = type2;
                    ob.rcat_gapval = Convert.ToDecimal(gap);
                    ref_comm_add_trgtg.Add(ob);
                    Session["ref_comm_add_trgt"] = ref_comm_add_trgtg;
                }
                else
                {
                    ref_comm_add_trgtg = Session["ref_comm_add_trgt"] as List<ref_comm_add_trgt>;
                    var count = ref_comm_add_trgtg.Where(a => a.rcat_pc == pc && a.rcat_type == type2 && a.rcat_slab == Convert.ToDecimal(slab)).Count();
                    if (count > 0)
                    {
                        return Json(new { success = false, login = true, msg = " Already added this  Range!!", data = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ref_comm_add_trgt ob = new ref_comm_add_trgt();
                        ob.rcat_pc = pc;
                        ob.rcat_slab = Convert.ToInt32(slab);
                        ob.rcat_from = Convert.ToDecimal(from);
                        ob.rcat_to = Convert.ToDecimal(to);
                        ob.rcat_comm = Convert.ToDecimal(comm);
                        ob.rcat_type = type2;
                        ob.rcat_gapval = Convert.ToDecimal(gap);
                        ref_comm_add_trgtg.Add(ob);
                        Session["ref_comm_add_trgt"] = ref_comm_add_trgtg;
                    }
                }
                return Json(new { success = true, login = true, data = ref_comm_add_trgtg }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult RemoveAddiEliteItemValueRange(string pc, string Slab)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<ref_comm_add_trgt> ref_comm_add_trgtg = new List<ref_comm_add_trgt>();
                if (Session["ref_comm_add_trgt"] != null)
                {
                    ref_comm_add_trgtg = (List<ref_comm_add_trgt>)Session["ref_comm_add_trgt"];
                }
                else
                {
                    ref_comm_add_trgtg = new List<ref_comm_add_trgt>();

                }
                var itemToRemove = ref_comm_add_trgtg.First(r => r.rcat_pc == pc && r.rcat_slab == Convert.ToInt32( Slab) );
                ref_comm_add_trgtg.Remove(itemToRemove);
                Session["ref_comm_add_trgt"] = ref_comm_add_trgtg;
                return Json(new { success = true, login = true, data = ref_comm_add_trgtg }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}