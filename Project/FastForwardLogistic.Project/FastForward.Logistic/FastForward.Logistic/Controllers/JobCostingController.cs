using FF.BusinessObjects.Sales;
using FF.BusinessObjects.Search;
using FF.BusinessObjects;
using FF.BusinessObjects.Genaral;
using FF.BusinessObjects.Security;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using FastForward.Logistic.Models;
using CrystalDecisions.Shared;
using System.Security.Authentication;
using System.IO;

namespace FastForward.Logistic.Controllers
{
    public class JobCostingController : BaseController
    {
        // GET: JobCosting
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                TRN_MOD_MAX_APPLVL data = CHNLSVC.General.getMaxAppLvlPermission("PTYCSHREQ", company);
                if (data.TMAL_MODULE != null)
                {
                    Session["MAXAPPLVL"] = data;
                }
                else
                {
                    data.TMAL_COM = company;
                    data.TMAL_MAX_APPLVL = 3;
                    data.TMAL_MODULE = "PTYCSHREQ";
                    Session["MAXAPPLVL"] = data;
                }
                ViewBag.Logincompany = company;
                Session["old_job_cost"] = null;
                Session["new_job_cost"] = null;
                Session["old_job_actual_cost"] = null;
                return View();
            }
            else
            {
                return Redirect("~/Home/index");
            }
        }
        public JsonResult LoadJobCostinData(string JobNo)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    //List<TRN_JOB_COST> cost_list = CHNLSVC.Sales.GetJobCostData(JobNo);
                    List<TRN_JOB_COST> cost_list = CHNLSVC.Sales.GetJobCostData_New(JobNo, company, userDefPro);
                    
                    //List<TRN_JOB_COST> actual_list = CHNLSVC.Sales.GetJobActualCostData(JobNo);
                    List<TRN_JOB_COST> actual_list = CHNLSVC.Sales.GetJobActualCostData_New(JobNo, company, userDefPro);
                    if (cost_list == null)
                    {
                        cost_list = new List<TRN_JOB_COST>();
                    }
                    if (actual_list == null)
                    {
                        actual_list = new List<TRN_JOB_COST>();
                    }
                    Session["old_job_cost"] = cost_list;
                    Session["old_job_actual_cost"] = actual_list;
                    Session["new_job_cost"] = null;
                    Session["new_job_actual_cost"] = null;

                    decimal tot_cost = cost_list.AsEnumerable().Sum(o => o.TJC_COST_AMT);
                    decimal tot_Act_cost = actual_list.AsEnumerable().Sum(o => o.TJC_COST_AMT);

                    decimal costdiff = tot_Act_cost - tot_cost;

                    int TJC_APP_1 = cost_list.AsEnumerable().Sum(o => o.TJC_APP_1);
                    int TJC_APP_2 = cost_list.AsEnumerable().Sum(o => o.TJC_APP_2);
                    int TJC_ACT = cost_list.AsEnumerable().Sum(o => o.TJC_ACT);

                    return Json(new { success = true, login = true, costData = cost_list, actualCostData = actual_list, costdiff = costdiff, TJC_APP_1 = TJC_APP_1, TJC_APP_2 = TJC_APP_2, TJC_ACT = TJC_ACT }, JsonRequestBehavior.AllowGet);

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
        public JsonResult AddJobCostinData(string JobNo, string Desc, string Cost, string CstEle, string Margin)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    int lineno = 0;
                    int lineno_actual = 0;
                    List<TRN_JOB_COST> old_job_cost = Session["old_job_cost"] as List<TRN_JOB_COST>;
                    List<TRN_JOB_COST> new_job_cost = Session["new_job_cost"] as List<TRN_JOB_COST>;
                    List<TRN_JOB_COST> old_job_actual_cost = Session["old_job_actual_cost"] as List<TRN_JOB_COST>;
                    List<TRN_JOB_COST> new_job_actual_cost = Session["new_job_actual_cost"] as List<TRN_JOB_COST>;

                    if (old_job_cost != null)
                    {
                        if (old_job_cost.Count > 0)
                        {
                            lineno = old_job_cost.Max(a => a.TJC_LINE_NO);
                        }

                    }


                    if (new_job_cost == null)
                    {
                        new_job_cost = new List<TRN_JOB_COST>();
                    }
                    else
                    {
                        if (new_job_cost.Count > 0)
                        {
                            lineno = new_job_cost.Max(a => a.TJC_LINE_NO);
                        }
                    }
                    if (new_job_actual_cost == null)
                    {
                        new_job_actual_cost = new List<TRN_JOB_COST>();
                    }
                    else
                    {
                        if (new_job_actual_cost.Count > 0)
                        {
                            lineno_actual = new_job_actual_cost.Max(a => a.TJC_LINE_NO);
                        }
                    }
                    //-----------------------
                    if (old_job_cost != null)
                    {
                        if (old_job_cost.Count > 0)
                        {
                            //int count = old_job_cost.Where(a => a.TJC_DESC == Desc && a.TJC_COST_AMT == Convert.ToDecimal(Cost)).Count();
                            int count = old_job_cost.Where(a => a.TJC_DESC == Desc).Count();
                            if (count == 0)
                            {
                                //return Json(new { success = false, login = true, msg = "Cant Remove This Cost(Already added Cost)", Type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                //var removeitem = old_job_cost.First(a => a.TJC_DESC == Desc && a.TJC_COST_AMT == Convert.ToDecimal(Cost));
                                var removeitem = old_job_cost.First(a => a.TJC_DESC == Desc);
                                //new_job_cost.Remove(removeitem);
                                old_job_cost.Remove(removeitem);
                                Session["old_job_cost"] = old_job_cost;
                                Session["new_job_cost"] = new_job_cost;
                            }

                        }
                        else
                        {
                            //return Json(new { success = false, login = true, msg = "Cant Remove This Cost(Already added Cost)", Type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (old_job_actual_cost != null)
                    {
                        if (old_job_actual_cost.Count > 0)
                        {
                            //int count = old_job_cost.Where(a => a.TJC_DESC == Desc && a.TJC_COST_AMT == Convert.ToDecimal(Cost)).Count();
                            int count = old_job_actual_cost.Where(a => a.TJC_DESC == Desc).Count();
                            if (count == 0)
                            {
                                //return Json(new { success = false, login = true, msg = "Cant Remove This Cost(Already added Cost)", Type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                //var removeitem = old_job_cost.First(a => a.TJC_DESC == Desc && a.TJC_COST_AMT == Convert.ToDecimal(Cost));
                                var removeitem = old_job_actual_cost.First(a => a.TJC_DESC == Desc);
                                //new_job_cost.Remove(removeitem);
                                old_job_actual_cost.Remove(removeitem);
                                Session["old_job_actual_cost"] = old_job_actual_cost;
                                Session["new_job_actual_cost"] = new_job_actual_cost;
                            }

                        }
                        else
                        {
                            //return Json(new { success = false, login = true, msg = "Cant Remove This Cost(Already added Cost)", Type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    //-----------------------
                    TRN_JOB_COST _ob = new TRN_JOB_COST();
                    _ob.TJC_ACT = 1;
                    _ob.TJC_COM = company;
                    _ob.TJC_COST_AMT = Convert.ToDecimal(Cost);
                    _ob.TJC_CRE_BY = userId;
                    _ob.TJC_CRE_DT = DateTime.Now;
                    _ob.TJC_DESC = Desc;
                    _ob.TJC_DT = DateTime.Now;
                    _ob.TJC_ELEMENT_CODE = CstEle;
                    _ob.TJC_ELEMENT_TYPE = "";
                    _ob.TJC_JOB_NO = JobNo;
                    _ob.TJC_LINE_NO = lineno + 1;
                    _ob.TJC_MOD_BY = userId;
                    _ob.TJC_MOD_DT = DateTime.Now;
                    _ob.TJC_MARGIN = Convert.ToDecimal(Margin);
                    new_job_cost.Add(_ob);
                    old_job_cost.Add(_ob);

                    Session["old_job_cost"] = old_job_cost;
                    Session["new_job_cost"] = new_job_cost;

                    TRN_JOB_COST _obactual = new TRN_JOB_COST();
                    _obactual.TJC_ACT = 1;
                    _obactual.TJC_COM = company;
                    _obactual.TJC_COST_AMT = Convert.ToDecimal(Cost) + Convert.ToDecimal(Cost) * (Convert.ToDecimal(Margin) / 100);
                    _obactual.TJC_CRE_BY = userId;
                    _obactual.TJC_CRE_DT = DateTime.Now;
                    _obactual.TJC_DESC = Desc;
                    _obactual.TJC_DT = DateTime.Now;
                    _obactual.TJC_ELEMENT_CODE = CstEle;
                    _obactual.TJC_ELEMENT_TYPE = "";
                    _obactual.TJC_JOB_NO = JobNo;
                    _obactual.TJC_LINE_NO = lineno + 1;
                    _obactual.TJC_MOD_BY = userId;
                    _obactual.TJC_MOD_DT = DateTime.Now;
                    _obactual.TJC_MARGIN = Convert.ToDecimal(Margin);
                    new_job_actual_cost.Add(_obactual);
                    old_job_actual_cost.Add(_obactual);

                    Session["old_job_actual_cost"] = old_job_actual_cost;
                    Session["new_job_actual_cost"] = new_job_actual_cost;

                    return Json(new { success = true, login = true, data = old_job_cost, actualCostData = old_job_actual_cost }, JsonRequestBehavior.AllowGet);

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

        public JsonResult AddJobCostinDataRev(string JobNo, string Desc, string Cost, string CstEle, string Margin)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                int x = 0;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    int lineno = 0;
                    List<TRN_JOB_COST> old_job_cost = Session["old_job_actual_cost"] as List<TRN_JOB_COST>;
                    List<TRN_JOB_COST> new_job_cost = Session["new_job_actual_cost"] as List<TRN_JOB_COST>;

                    if (old_job_cost != null)
                    {
                        if (old_job_cost.Count > 0)
                        {
                            lineno = old_job_cost.Max(a => a.TJC_LINE_NO);
                        }

                    }


                    if (new_job_cost == null)
                    {
                        new_job_cost = new List<TRN_JOB_COST>();
                    }
                    else
                    {
                        if (new_job_cost.Count > 0)
                        {
                            lineno = new_job_cost.Max(a => a.TJC_LINE_NO);
                        }
                    }
                    //-----------------------
                    if (old_job_cost != null)
                    {
                        if (old_job_cost.Count > 0)
                        {
                            //int count = old_job_cost.Where(a => a.TJC_DESC == Desc && a.TJC_COST_AMT == Convert.ToDecimal(Cost)).Count();
                            int count = old_job_cost.Where(a => a.TJC_DESC == Desc).Count();
                            if (count == 0)
                            {
                                //return Json(new { success = false, login = true, msg = "Cant Remove This Cost(Already added Cost)", Type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                //var removeitem = old_job_cost.First(a => a.TJC_DESC == Desc && a.TJC_COST_AMT == Convert.ToDecimal(Cost));
                                var removeitem = old_job_cost.First(a => a.TJC_DESC == Desc);
                                if (removeitem.TJC_COST_AMT > Convert.ToDecimal(Cost))
                                {
                                    x = 1;
                                }
                                //new_job_cost.Remove(removeitem);
                                old_job_cost.Remove(removeitem);
                                Session["old_job_actual_cost"] = old_job_cost;
                                Session["new_job_actual_cost"] = new_job_cost;
                            }

                        }
                        else
                        {
                            //return Json(new { success = false, login = true, msg = "Cant Remove This Cost(Already added Cost)", Type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    //-----------------------
                    TRN_JOB_COST _ob = new TRN_JOB_COST();
                    _ob.TJC_ACT = 1;
                    _ob.TJC_COM = company;
                    _ob.TJC_COST_AMT = Convert.ToDecimal(Cost);
                    _ob.TJC_CRE_BY = userId;
                    _ob.TJC_CRE_DT = DateTime.Now;
                    _ob.TJC_DESC = Desc;
                    _ob.TJC_DT = DateTime.Now;
                    _ob.TJC_ELEMENT_CODE = CstEle;
                    _ob.TJC_ELEMENT_TYPE = "";
                    _ob.TJC_JOB_NO = JobNo;
                    _ob.TJC_LINE_NO = lineno + 1;
                    _ob.TJC_MOD_BY = userId;
                    _ob.TJC_MOD_DT = DateTime.Now;
                    _ob.TJC_MARGIN = Convert.ToDecimal(0);
                    new_job_cost.Add(_ob);
                    old_job_cost.Add(_ob);

                    Session["old_job_actual_cost"] = old_job_cost;
                    Session["new_job_actual_cost"] = new_job_cost;

                    return Json(new { success = true, login = true, data = old_job_cost, marginout = x }, JsonRequestBehavior.AllowGet);

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
        //public JsonResult RemoveCostDetails(string Desc, string Cost)
        //{
        //    string userId = HttpContext.Session["UserID"] as string;
        //    string company = HttpContext.Session["UserCompanyCode"] as string;
        //    string userDefPro = HttpContext.Session["UserDefProf"] as string;
        //    string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

        //    if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //    {
        //        List<TRN_JOB_COST> old_job_cost = Session["old_job_cost"] as List<TRN_JOB_COST>;
        //        List<TRN_JOB_COST> new_job_cost = Session["new_job_cost"] as List<TRN_JOB_COST>;

        //        if (new_job_cost != null)
        //        {
        //            if (new_job_cost.Count > 0)
        //            {
        //                int count = new_job_cost.Where(a => a.TJC_DESC == Desc && a.TJC_COST_AMT == Convert.ToDecimal(Cost)).Count();
        //                if (count == 0)
        //                {
        //                    return Json(new { success = false, login = true, msg = "Cant Remove This Cost(Already added Cost)", Type = "Info" }, JsonRequestBehavior.AllowGet);
        //                }
        //                else
        //                {
        //                    var removeitem = new_job_cost.First(a => a.TJC_DESC == Desc && a.TJC_COST_AMT == Convert.ToDecimal(Cost));
        //                    new_job_cost.Remove(removeitem);
        //                    old_job_cost.Remove(removeitem);
        //                    Session["old_job_cost"] = old_job_cost;
        //                    Session["new_job_cost"] = new_job_cost;
        //                }

        //            }
        //            else
        //            {
        //                return Json(new { success = false, login = true, msg = "Cant Remove This Cost(Already added Cost)", Type = "Info" }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        else
        //        {
        //            return Json(new { success = false, login = true, msg = "Cant Remove This Cost(Already added Cost)", Type = "Info" }, JsonRequestBehavior.AllowGet);
        //        }

        //        return Json(new { success = true, login = true, data = old_job_cost }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        public JsonResult RemoveCostDetails(string Desc, string Cost)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<TRN_JOB_COST> old_job_cost = Session["old_job_cost"] as List<TRN_JOB_COST>;
                List<TRN_JOB_COST> new_job_cost = Session["new_job_cost"] as List<TRN_JOB_COST>;

                if (old_job_cost != null)
                {
                    if (old_job_cost.Count > 0)
                    {
                        int count = old_job_cost.Where(a => a.TJC_DESC == Desc && a.TJC_COST_AMT == Convert.ToDecimal(Cost)).Count();
                        if (count == 0)
                        {
                            return Json(new { success = false, login = true, msg = "Cant Remove This Cost(Already added Cost)", Type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            var removeitem = old_job_cost.First(a => a.TJC_DESC == Desc && a.TJC_COST_AMT == Convert.ToDecimal(Cost));
                            //new_job_cost.Remove(removeitem);
                            old_job_cost.Remove(removeitem);
                            Session["old_job_cost"] = old_job_cost;
                            Session["new_job_cost"] = new_job_cost;
                        }

                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Cant Remove This Cost(Already added Cost)", Type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "Cant Remove This Cost(Already added Cost)", Type = "Info" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, login = true, data = old_job_cost }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveJobClose(string JobNo, string Date, string Remark)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<TRN_JOB_COST> old_job_cost = Session["old_job_cost"] as List<TRN_JOB_COST>;
                List<TRN_JOB_COST> new_job_cost = Session["new_job_cost"] as List<TRN_JOB_COST>;
                List<TRN_JOB_COST> old_job_actual_cost = Session["old_job_actual_cost"] as List<TRN_JOB_COST>;
                List<TRN_JOB_COST> new_job_actual_cost = Session["new_job_actual_cost"] as List<TRN_JOB_COST>;
                string err = "";
                //int effect = CHNLSVC.Sales.SaveJobCosting(JobNo, Remark, userId, Convert.ToDateTime(Date), new_job_cost, out err);
                if (old_job_cost != null || new_job_cost != null || old_job_actual_cost != null || new_job_actual_cost != null)
                {
                    int effect = CHNLSVC.Sales.SaveJobCosting(company, JobNo, Remark, userId, Convert.ToDateTime(Date), old_job_cost, old_job_actual_cost, out err);
                    if (effect > 0)
                    {
                        return Json(new { success = true, login = true, msg = "Successfully Saved!!!", Type = "succ" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = err, Type = "Error" }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(new { success = false, login = true, msg = "No data found", Type = "Info" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ApproveJobCost(string JobNo, string Date, string Remark)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<TRN_JOB_COST> old_job_cost = Session["old_job_cost"] as List<TRN_JOB_COST>;
                List<TRN_JOB_COST> new_job_cost = Session["new_job_cost"] as List<TRN_JOB_COST>;

                List<TRN_JOB_COST> old_job_actual_cost = Session["old_job_actual_cost"] as List<TRN_JOB_COST>;
                List<TRN_JOB_COST> new_job_actual_cost = Session["new_job_actual_cost"] as List<TRN_JOB_COST>;
                string err = "";

                Int32 appPer = Convert.ToInt32(Session["Log_Autho"].ToString());
                TRN_MOD_MAX_APPLVL lvl = (TRN_MOD_MAX_APPLVL)Session["MAXAPPLVL"];

                //if (appPer < lvl.TMAL_MAX_APPLVL)
                //{
                //    err = "Level " + appPer + " users don't have permission for approve job costing details.";
                //    return Json(new { success = false, login = true, msg = err, Type = "Error" }, JsonRequestBehavior.AllowGet);

                //}
                //else
                //{
                    //int effect1 = CHNLSVC.Sales.GetJobCostSavedData(JobNo, company, userDefPro);
                    //if (effect1 > 0)
                    //{

                    //}
                    
                    //int effect = CHNLSVC.Sales.SaveJobCosting(JobNo, Remark, userId, Convert.ToDateTime(Date), new_job_cost, out err);
                    int effect = CHNLSVC.Sales.ApproveJobCosting(JobNo, Remark, userId, Convert.ToDateTime(Date), old_job_cost, old_job_actual_cost,1, out err);
                    if (effect > 0)
                    {
                        return Json(new { success = true, login = true, msg = "Successfully Approved!!!", Type = "succ" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = err, Type = "Error" }, JsonRequestBehavior.AllowGet);
                    }
                //}

            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ApproveJobCost2(string JobNo, string Date, string Remark)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<TRN_JOB_COST> old_job_cost = Session["old_job_cost"] as List<TRN_JOB_COST>;
                List<TRN_JOB_COST> new_job_cost = Session["new_job_cost"] as List<TRN_JOB_COST>;

                List<TRN_JOB_COST> old_job_actual_cost = Session["old_job_actual_cost"] as List<TRN_JOB_COST>;
                List<TRN_JOB_COST> new_job_actual_cost = Session["new_job_actual_cost"] as List<TRN_JOB_COST>;
                string err = "";

                Int32 appPer = Convert.ToInt32(Session["Log_Autho"].ToString());
                TRN_MOD_MAX_APPLVL lvl = (TRN_MOD_MAX_APPLVL)Session["MAXAPPLVL"];

                //if (appPer < lvl.TMAL_MAX_APPLVL)
                if (appPer == 2)
                {
                    err = "Level " + appPer + " users don't have permission for approve job costing details.";
                    return Json(new { success = false, login = true, msg = err, Type = "Info" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    //int effect1 = CHNLSVC.Sales.GetJobCostSavedData(JobNo, company, userDefPro);
                    //if (effect1 > 0)
                    //{

                    //}

                    //int effect = CHNLSVC.Sales.SaveJobCosting(JobNo, Remark, userId, Convert.ToDateTime(Date), new_job_cost, out err);
                    int effect = CHNLSVC.Sales.ApproveJobCosting(JobNo, Remark, userId, Convert.ToDateTime(Date), old_job_cost, old_job_actual_cost,2, out err);
                    if (effect > 0)
                    {
                        return Json(new { success = true, login = true, msg = "Successfully Approved!!!", Type = "succ" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = err, Type = "Error" }, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        //public JsonResult ReopenJobClose(string JobNo, string Date, string Remark)
        //{
        //    string userId = HttpContext.Session["UserID"] as string;
        //    string company = HttpContext.Session["UserCompanyCode"] as string;
        //    string userDefPro = HttpContext.Session["UserDefProf"] as string;
        //    string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

        //    if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //    {
        //        List<TRN_JOB_COST> old_job_cost = Session["old_job_cost"] as List<TRN_JOB_COST>;
        //        List<TRN_JOB_COST> new_job_cost = Session["new_job_cost"] as List<TRN_JOB_COST>;
        //        string err = "";
        //        Int32 appPer = Convert.ToInt32(Session["Log_Autho"].ToString());
        //        if (appPer != 3)
        //        {
        //            return Json(new { success = false, login = true, msg = "Yoe don't have permission for edit job status.(Requsted permission code 1015)", Type = "Info" }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            int effect = CHNLSVC.Sales.ReopenJobClose(JobNo, Remark, userId, Convert.ToDateTime(Date), out err);
        //            if (effect > 0)
        //            {
        //                return Json(new { success = true, login = true, msg = "Successfully Re-Opened!!!", Type = "succ" }, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                return Json(new { success = false, login = true, msg = err, Type = "Error" }, JsonRequestBehavior.AllowGet);
        //            }
        //        }

        //    }
        //    else
        //    {
        //        return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        public void ClearSession()
        {
            Session["old_job_cost"] = null;
            Session["new_job_cost"] = null;
            Session["old_job_actual_cost"] = null;
        }
        public JsonResult JobOrPouchDetails(string code, string type)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<JOB_NUM_SEARCH> details = CHNLSVC.Sales.JobOrPouchCostDetails(code, type, company, userId);
                    int count = details.Count;

                    return Json(new { success = true, login = true, data = details, count = count }, JsonRequestBehavior.AllowGet);

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
        public ActionResult RequestPrint(List<string> selectedcompany, List<string> selectedprofcen, FormCollection collection, string JobNo)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    Session["ReportData"] = null;
                    MasterCompany _msCom = CHNLSVC.Security.GetCompByCode(Session["UserCompanyCode"].ToString());
                    ReportViewerViewModel model = new ReportViewerViewModel();
                    ReportDocument rd = new ReportDocument();
                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();
                    DataTable dtAll = new DataTable();

                    //
                    string reportName = "";
                    string fileName = "";
                    string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"].ToString();
                    string report = "";

                    DataTable comData = new DataTable("comdata");
                    comData = CHNLSVC.Sales.getCompanyDetailsBycd(company);

                    //THarindu 2018-01-04

                    dt1 = CHNLSVC.Sales.Get_jb_header(company, JobNo);
                    dt2 = CHNLSVC.Sales.GetJobCostingData_new(JobNo,"C");
                    //dt3 = CHNLSVC.Sales.GetJobActualCostingData(JobNo);
                    dt3 = CHNLSVC.Sales.GetJobCostingData_new(JobNo, "R");

                    dtAll = dt2.Copy();
                    dtAll.Merge(dt3);

                    //reportName = "rpt_JobCostingSheet.rpt";
                    reportName = "rpt_PreJobCostingSheet.rpt";
                    fileName = "PreJobCostingSheet.pdf";
                    //  reportName = "test.rpt";
                    //  fileName = "test.pdf";
                    //report = ReportPath + "\\" + reportName;
                    //rd.Load(report);
                    rd.Load(Path.Combine(Server.MapPath("~/Reports"), "rpt_PreJobCostingSheet.rpt"));

                    //     rd.Load(Server.MapPath("/Reports/" + "rpt_JobCostingSheet.rpt"));
                    rd.Database.Tables["JobHeader"].SetDataSource(dt1);
                    rd.Database.Tables["JobCost"].SetDataSource(dt2);
                    rd.Database.Tables["ActuvalJobCost"].SetDataSource(dt3);

                    rd.SetParameterValue("UserID", userId);
                    //rd.SetParameterValue("frmDate", model.frmDate);
                    //rd.SetParameterValue("toDate", model.todate);
                    rd.SetParameterValue("ComName", _msCom.Mc_desc);
                    rd.SetParameterValue("Add1", _msCom.Mc_add1);
                    rd.SetParameterValue("Add2", _msCom.Mc_add2);
                    rd.SetParameterValue("Tel", _msCom.Mc_tel);

                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();

                    try
                    {
                        this.Response.Clear();
                        this.Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);

                        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        stream.Seek(0, SeekOrigin.Begin);
                        rd.Close();
                        rd.Dispose();
                        return File(stream, "application/pdf");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    //return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                }
                else
                {
                    return Redirect("~/Home/index");
                }


            }

            catch (Exception ex)
            {
                CHNLSVC.General.SaveReportErrorLog("rpt_JobCostingSheet-PDF", "rpt_JobCostingSheet", ex.Message, Session["UserID"].ToString());
                ViewData["Error"] = ex.Message.ToString();
                return View("RequestPrint");
            }

            // return Redirect("~/Home/index");
        }
    }
}