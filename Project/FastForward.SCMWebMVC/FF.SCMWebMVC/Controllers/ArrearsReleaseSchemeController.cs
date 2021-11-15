using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FF.BusinessObjects.Commission;
using FF.BusinessObjects.General;
using System.Data;

namespace FF.SCMWebMVC.Controllers
{
    public class ArrearsReleaseSchemeController : BaseController
    {
        // GET: ArrearsReleaseScheme
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["hpr_ars_rls_sch"] = null;
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }

        public JsonResult AddMainDetails(string Deftype, string Valuefrom, string Valueto, string Defcode, string Rental, string Channel, string Location, string dataset, string Accfrom, string Accto)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string Type="";
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<hpr_ars_rls_sch> _lst = new List<hpr_ars_rls_sch>();
                    //DataTable checkterm = new DataTable();

                    Int32 checkterm = CHNLSVC.General.GetSchemeTerm(Deftype, Defcode, Rental);
                    if (checkterm == 0 )
                    {
                        return Json(new { success = false, login = true, msg = "You can not enter no of monthly rentals above the original created scheme period !!", data = "" }, JsonRequestBehavior.AllowGet);
                    }             
   
                    if (Session["hpr_ars_rls_sch"] == null)
                    {
                        if(Channel != "")
                        {
                            Type = "Channel";
                        }
                        if(Location != "")
                        {
                            Type = "PC";
                        }

                        hpr_ars_rls_sch ob = new hpr_ars_rls_sch();
                        //ob.Hhoa_ac = AccNo;
                        ob.hars_sch = Defcode;
                        ob.hars_cre_by = userId;
                        ob.hars_cre_dt = DateTime.Now.Date;
                        ob.hars_no_rnt = Convert.ToDecimal(Rental);
                        ob.hars_eff_from = Convert.ToDateTime(Valuefrom);
                        ob.hars_eff_to = Convert.ToDateTime(Valueto);
                        ob.hars_channel = Channel;
                        ob.hars_pc = Location;
                        ob.hars_acc_from = Convert.ToDateTime(Accfrom);
                        ob.hars_acc_to = Convert.ToDateTime(Accto);

                        //ob.hdvr_tp = Convert.ToInt32(Percentage);
                        //ob. = Convert.ToInt32(Defcode);

                        _lst.Add(ob);
                        Session["hpr_ars_rls_sch"] = _lst;
                    }
                    else
                    {
                        _lst = Session["hpr_ars_rls_sch"] as List<hpr_ars_rls_sch>;
                        var count = 0;

                        if (Channel != "")
                        {
                            count = _lst.Where(a => a.hars_channel.ToString() == Channel && a.hars_sch.ToString() == Defcode && a.hars_eff_from == Convert.ToDateTime(Valuefrom) && a.hars_eff_to == Convert.ToDateTime(Valueto) && a.hars_acc_from == Convert.ToDateTime(Accfrom) && a.hars_acc_to == Convert.ToDateTime(Accto) && a.hars_no_rnt == Convert.ToDecimal(Rental)).Count();
                           
                        }
                        if (Location != "")
                        {
                            count = _lst.Where(a => a.hars_pc.ToString() == Location && a.hars_sch.ToString() == Defcode && a.hars_eff_from == Convert.ToDateTime(Valuefrom) && a.hars_eff_to == Convert.ToDateTime(Valueto) && a.hars_acc_from == Convert.ToDateTime(Accfrom) && a.hars_acc_to == Convert.ToDateTime(Accto) && a.hars_no_rnt == Convert.ToDecimal(Rental)).Count();
                        
                        }

                        if (count > 0)
                        {
                            return Json(new { success = false, login = true, msg = "Already added this Record!!", data = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            var count1 = 0;
                            var count2 = 0;
                            var count3 = 0;
                            var count4 = 0;

                            if (Channel != "")
                            {
                                count1 = _lst.Where(a => a.hars_channel.ToString() == Channel && a.hars_sch.ToString() == Defcode && a.hars_eff_from <= Convert.ToDateTime(Valuefrom) && a.hars_eff_to >= Convert.ToDateTime(Valuefrom)).Count();
                                count2 = _lst.Where(a => a.hars_channel.ToString() == Channel && a.hars_sch.ToString() == Defcode && a.hars_eff_from <= Convert.ToDateTime(Valueto) && a.hars_eff_to >= Convert.ToDateTime(Valueto)).Count();

                                count3 = _lst.Where(a => a.hars_channel.ToString() == Channel && a.hars_sch.ToString() == Defcode && a.hars_acc_from <= Convert.ToDateTime(Accfrom) && a.hars_acc_to >= Convert.ToDateTime(Accfrom)).Count();
                                count4 = _lst.Where(a => a.hars_channel.ToString() == Channel && a.hars_sch.ToString() == Defcode && a.hars_acc_from <= Convert.ToDateTime(Accto) && a.hars_acc_to >= Convert.ToDateTime(Accto)).Count();
                                //var count1 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_from_arrper <= Convert.ToDecimal(Arrfpercentage) && a.hbp_from_arrper >= Convert.ToDecimal(Arrfpercentage)).Count();
                                //var count2 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_to_arrper <= Convert.ToDecimal(Arrtpercentage) && a.hbp_to_arrper >= Convert.ToDecimal(Arrtpercentage)).Count();
                                //var count3 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_from_bal <= Convert.ToDecimal(Accfbalance) && a.hbp_from_bal >= Convert.ToDecimal(Accfbalance)).Count();
                                //var count4 = _lst.Where(a => a.hbp_circular.ToString() == Circode && a.hbp_to_bal <= Convert.ToDecimal(Acctbalance) && a.hbp_to_bal >= Convert.ToDecimal(Acctbalance)).Count();
                            }
                            if (Location != "")
                            {
                                count1 = _lst.Where(a => a.hars_pc.ToString() == Location && a.hars_sch.ToString() == Defcode && a.hars_eff_from <= Convert.ToDateTime(Valuefrom) && a.hars_eff_to >= Convert.ToDateTime(Valuefrom)).Count();
                                count2 = _lst.Where(a => a.hars_pc.ToString() == Location && a.hars_sch.ToString() == Defcode && a.hars_eff_from <= Convert.ToDateTime(Valueto) && a.hars_eff_to >= Convert.ToDateTime(Valueto)).Count();

                                count3 = _lst.Where(a => a.hars_pc.ToString() == Location && a.hars_sch.ToString() == Defcode && a.hars_acc_from <= Convert.ToDateTime(Accfrom) && a.hars_acc_to >= Convert.ToDateTime(Accfrom)).Count();
                                count4 = _lst.Where(a => a.hars_pc.ToString() == Location && a.hars_sch.ToString() == Defcode && a.hars_acc_from <= Convert.ToDateTime(Accto) && a.hars_acc_to >= Convert.ToDateTime(Accto)).Count();
                                
                            }

                            //if(count1 > 0)
                            //{
                            //    if(count2 > 0)
                            //   {
                            //        if (count3 > 0)
                            //        {
                            //            if (count4 > 0)
                            //            {
                            //                return Json(new { success = false, login = true, msg = "This range is already exist!!", data = "" }, JsonRequestBehavior.AllowGet);                            
                            //            }
                            //        }
                            //    }
                            //}

                            if (count1 > 0 || count2 > 0)
                            {                                  
                                if (count3 > 0 || count4 > 0)
                                  {
                                    return Json(new { success = false, login = true, msg = "This range is already exist!!", data = "" }, JsonRequestBehavior.AllowGet);
                                  } 
                            }

                            var count_1 = 0;
                            var count_2 = 0;
                            var count_3 = 0;
                            var count_4 = 0;

                            if (Channel != "")
                            {
                                count_1 = _lst.Where(a => a.hars_channel.ToString() == Channel && a.hars_sch.ToString() == Defcode && Convert.ToDateTime(Valuefrom) < a.hars_eff_from && Convert.ToDateTime(Valueto) > a.hars_eff_from).Count();
                                count_2 = _lst.Where(a => a.hars_channel.ToString() == Channel && a.hars_sch.ToString() == Defcode && Convert.ToDateTime(Valuefrom) < a.hars_eff_to && Convert.ToDateTime(Valueto) > a.hars_eff_to).Count();

                                count_3 = _lst.Where(a => a.hars_channel.ToString() == Channel && a.hars_sch.ToString() == Defcode && Convert.ToDateTime(Accfrom) < a.hars_acc_from && Convert.ToDateTime(Accto) > a.hars_acc_from).Count();
                                count_4 = _lst.Where(a => a.hars_channel.ToString() == Channel && a.hars_sch.ToString() == Defcode && Convert.ToDateTime(Accfrom) < a.hars_acc_to && Convert.ToDateTime(Accto) > a.hars_acc_to).Count();
                            }
                            if (Location != "")
                            {
                                count_1 = _lst.Where(a => a.hars_pc.ToString() == Location && a.hars_sch.ToString() == Defcode && Convert.ToDateTime(Valuefrom) < a.hars_eff_from && Convert.ToDateTime(Valueto) > a.hars_eff_from).Count();
                                count_2 = _lst.Where(a => a.hars_pc.ToString() == Location && a.hars_sch.ToString() == Defcode && Convert.ToDateTime(Valuefrom) < a.hars_eff_to && Convert.ToDateTime(Valueto) > a.hars_eff_to).Count();

                                count_3 = _lst.Where(a => a.hars_pc.ToString() == Location && a.hars_sch.ToString() == Defcode && Convert.ToDateTime(Accfrom) < a.hars_acc_from && Convert.ToDateTime(Accto) > a.hars_acc_from).Count();
                                count_4 = _lst.Where(a => a.hars_pc.ToString() == Location && a.hars_sch.ToString() == Defcode && Convert.ToDateTime(Accfrom) < a.hars_acc_to && Convert.ToDateTime(Accto) > a.hars_acc_to).Count();
                            }

                            if (count_1 > 0 || count_2 > 0)
                            {
                                return Json(new { success = false, login = true, msg = "This range is already exist!!", data = "" }, JsonRequestBehavior.AllowGet);
                            }

                            if (count_3 > 0 || count_4 > 0)
                            {
                                return Json(new { success = false, login = true, msg = "This range is already exist!!", data = "" }, JsonRequestBehavior.AllowGet);
                            }

                            //if (count1 > 0 || count2 > 0 || count3 > 0 || count4 > 0)
                            //{
                            //    return Json(new { success = false, login = true, msg = "This range is already exist!!", data = "" }, JsonRequestBehavior.AllowGet);
                            //}
                            //else
                            //{
                                hpr_ars_rls_sch ob = new hpr_ars_rls_sch();
                                //ob.Hhoa_ac = AccNo;
                                ob.hars_sch = Defcode;
                                ob.hars_cre_by = userId;
                                ob.hars_cre_dt = DateTime.Now.Date;
                                ob.hars_no_rnt = Convert.ToDecimal(Rental);
                                ob.hars_eff_from = Convert.ToDateTime(Valuefrom);
                                ob.hars_eff_to = Convert.ToDateTime(Valueto);
                                ob.hars_channel = Channel;
                                ob.hars_pc = Location;
                                ob.hars_acc_from = Convert.ToDateTime(Accfrom);
                                ob.hars_acc_to = Convert.ToDateTime(Accto);

                                _lst.Add(ob);
                                Session["hpr_ars_rls_sch"] = _lst;
                            //}
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
        public JsonResult RemoveMainDetails(string Valuefrom, string Valueto, string Defcode, string Accfrom, string Accto)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<hpr_ars_rls_sch> _lst = new List<hpr_ars_rls_sch>();
                if (Session["hpr_ars_rls_sch"] != null)
                {
                    _lst = (List<hpr_ars_rls_sch>)Session["hpr_ars_rls_sch"];
                }
                else
                {
                    _lst = new List<hpr_ars_rls_sch>();

                }
                var itemToRemove = _lst.First(r => r.hars_sch == Defcode && r.hars_eff_from == Convert.ToDateTime(Valuefrom) && r.hars_eff_to == Convert.ToDateTime(Valueto) && r.hars_acc_from == Convert.ToDateTime(Accfrom) && r.hars_acc_to == Convert.ToDateTime(Accto));
                //var count = _lst.Where(a => a.hmfa_mgr_cd.ToString() == Manager && a.hmfa_pc.ToString() == Location && a.hmfa_bonus_st_dt == Convert.ToDateTime(BonusMonth).AddMonths(1).AddDays(-1)).Count();

                _lst.Remove(itemToRemove);
                Session["hpr_ars_rls_sch"] = _lst;
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
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string err = "";
                    //Auto Number
                    if (Session["hpr_ars_rls_sch"] != null)
                    {
                        List<hpr_ars_rls_sch> list1 = new List<hpr_ars_rls_sch>();

                        list1 = (List<hpr_ars_rls_sch>)Session["hpr_ars_rls_sch"];

                        //save
                        //int effect = CHNLSVC.Finance.SaveHandOverAccounts(list, out err);
                        int effect = CHNLSVC.General.SaveArrearsDetails(list1, out err);

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
                        return Json(new { success = true, login = true, msg = "Please add record" }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                //return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
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
                Session["hpr_ars_rls_sch"] = null;
                return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, msg = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult LoadMainDetails(string Channel, string Location)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            //Circode = Circode.Trim();
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<hpr_ars_rls_sch> _list = new List<hpr_ars_rls_sch>();
                _list = CHNLSVC.General.GetSchemeData(company,Channel,Location);
                Session["hpr_ars_rls_sch"] = _list;
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