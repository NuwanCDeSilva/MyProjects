using FF.BusinessObjects.Commission;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;

namespace FF.SCMWebMVC.Controllers
{
    public class HandingOverAccountsController : BaseController
    {
        //
        // GET: /HandingOverAccounts/
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["hpr_hand_over_ac"] = null;
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }

        }
        public JsonResult AddHandOverAccounts(string Date, string BonusMonth, string Location, string AccNo, string RejectAmm)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                decimal _bal = 0;
                bool _reject = CHNLSVC.Finance.CheckClosBal(AccNo, Convert.ToDateTime(BonusMonth).AddMonths(1).AddDays(-1), Convert.ToDecimal(RejectAmm), out _bal);
                if (_reject == false)
                {
                    return Json(new { success = false, login = true, msg = "Rejected amount cannot be exceed than account Balance (Rs. " + _bal.ToString("#,##0.00") + ") as at " + Convert.ToDateTime(BonusMonth).AddMonths(1).AddDays(-1).Date, data = "" }, JsonRequestBehavior.AllowGet);
                }

                int count2 = 0;
                List<hpr_hand_over_ac> _hlist = CHNLSVC.Finance.GetHandOverData(company, Location, true);
                if (_hlist != null)
                {
                    count2 = _hlist.Where(a => a.Hhoa_ac == AccNo).Count();
                }
                if (count2 > 0)
                {
                    return Json(new { success = false, login = true, msg = "Already available  this account as handing over", data = "" }, JsonRequestBehavior.AllowGet);
                }
                List<hpr_hand_over_ac> _hlist2 = CHNLSVC.Finance.GetHandOverData(company, Location, false);
                if (_hlist2 != null)
                {
                    count2 = _hlist2.Where(a => a.Hhoa_ac == AccNo).Count();
                }
                if (count2 > 0)
                {
                    return Json(new { success = false, login = true, msg = "Already available  this account as handing over", data = "" }, JsonRequestBehavior.AllowGet);
                }

                List<hpr_hand_over_ac> _lst = new List<hpr_hand_over_ac>();
                if (Session["hpr_hand_over_ac"] == null)
                {

                    hpr_hand_over_ac ob = new hpr_hand_over_ac();
                    ob.Hhoa_ac = AccNo;
                    ob.Hhoa_bonus_month = Convert.ToDateTime(BonusMonth).AddMonths(1).AddDays(-1);
                    ob.Hhoa_com = company;
                    ob.Hhoa_cre_by = userId;
                    ob.Hhoa_cre_dt = DateTime.Now;
                    ob.Hhoa_pc = Location;
                    ob.Hhoa_rej_lmt = Convert.ToDecimal(RejectAmm);
                    _lst.Add(ob);
                    Session["hpr_hand_over_ac"] = _lst;
                }
                else
                {
                    _lst = Session["hpr_hand_over_ac"] as List<hpr_hand_over_ac>;
                    var count = _lst.Where(a => a.Hhoa_ac.ToString() == AccNo && a.Hhoa_bonus_month == Convert.ToDateTime(BonusMonth).AddMonths(1).AddDays(-1) && a.Hhoa_rej_lmt.ToString() == RejectAmm && a.Hhoa_pc == Location).Count();
                    if (count > 0)
                    {
                        return Json(new { success = false, login = true, msg = "Already added this  Range!!", data = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        hpr_hand_over_ac ob = new hpr_hand_over_ac();
                        ob.Hhoa_ac = AccNo;
                        ob.Hhoa_bonus_month = Convert.ToDateTime(BonusMonth).AddMonths(1).AddDays(-1);
                        ob.Hhoa_com = company;
                        ob.Hhoa_cre_by = userId;
                        ob.Hhoa_cre_dt = DateTime.Now;
                        ob.Hhoa_pc = Location;
                        ob.Hhoa_rej_lmt = Convert.ToDecimal(RejectAmm);
                        _lst.Add(ob);
                        Session["hpr_hand_over_ac"] = _lst;
                    }

                }
                return Json(new { success = true, login = true, list = _lst }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult RemoveAccountCode(string PC, string Account, string Month, string Ammount)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<hpr_hand_over_ac> _lst = new List<hpr_hand_over_ac>();
                if (Session["hpr_hand_over_ac"] != null)
                {
                    _lst = (List<hpr_hand_over_ac>)Session["hpr_hand_over_ac"];
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "Can't Delete", data = "" }, JsonRequestBehavior.AllowGet);

                }
                var itemToRemove = _lst.First(r => r.Hhoa_ac == Account && r.Hhoa_pc == PC && r.Hhoa_bonus_month == Convert.ToDateTime(Month).AddMonths(1).AddDays(-1) && r.Hhoa_rej_lmt == Convert.ToDecimal(Ammount));
                _lst.Remove(itemToRemove);
                Session["hpr_hand_over_ac"] = _lst;
                return Json(new { success = true, login = true, list = _lst }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveHandAccount()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string err = "";
                //Auto Number
                List<hpr_hand_over_ac> list = new List<hpr_hand_over_ac>();
                list = (List<hpr_hand_over_ac>)Session["hpr_hand_over_ac"];
                //save
                if (Session["hpr_hand_over_ac"] == null)
                {
                    return Json(new { success = false, login = true, msg = "No new records to save", data = "" }, JsonRequestBehavior.AllowGet);
                }
                int effect = CHNLSVC.Finance.SaveHandOverAccounts(list, out err);
                if (effect == 1 || effect == 0)
                {
                    return Json(new { success = true, login = true, msg = "Successfull Saved  " + err }, JsonRequestBehavior.AllowGet);
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
        public JsonResult LoadHandOverDetails(string Pc, string Isbalance)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            Pc = Pc.Trim();
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<hpr_hand_over_ac> _list = new List<hpr_hand_over_ac>();
                if (Isbalance == "1")
                {
                    _list = CHNLSVC.Finance.GetHandOverData(company, Pc, true);
                }
                else
                {
                    _list = CHNLSVC.Finance.GetHandOverData(company, Pc, false);
                }
                if (_list == null)
                {
                    _list = new List<hpr_hand_over_ac>();
                }
                return Json(new { success = true, login = true, list = _list }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult ArrearsProcess(string Month)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            Month = Month.Trim();
            string err = "";
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                int effect = CHNLSVC.Finance.AccountsArrearsProcess(Convert.ToDateTime(Month).AddMonths(1).AddDays(-1), company, userId, out  err);
                return Json(new { success = true, login = true, list = "" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public void ClearAll()
        {
            Session["hpr_hand_over_ac"] = null;
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
                            List<hpr_hand_over_ac> _lst = new List<hpr_hand_over_ac>();
                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                decimal _bal = 0;
                                string _loc = "";
                                string _account = "";
                                decimal _amt = 0;
                                string _month = "";
                                if (workSheet.Cells[rowIterator, 1].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    _loc = workSheet.Cells[rowIterator, 1].Value.ToString();

                                }
                                if (workSheet.Cells[rowIterator, 2].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    _account = workSheet.Cells[rowIterator, 2].Value.ToString();

                                }
                                if (workSheet.Cells[rowIterator, 3].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    _month = workSheet.Cells[rowIterator, 3].Value.ToString();

                                }
                                if (workSheet.Cells[rowIterator, 4].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    _amt = Convert.ToDecimal(workSheet.Cells[rowIterator, 4].Value.ToString());
                                    if (_amt < 0)
                                    {
                                        return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + "Invalid Value", data = "" }, JsonRequestBehavior.AllowGet);
                                    }
                                }

                                bool _reject = CHNLSVC.Finance.CheckClosBal(_account, Convert.ToDateTime(_month).AddMonths(1).AddDays(-1).Date, Convert.ToDecimal(_amt), out _bal);
                                if (_reject == false)
                                {
                                    return Json(new { success = false, login = true, msg = "Rejected amount cannot be exceed than account Balance ("+_account+") "+" (Rs. " + _bal.ToString("#,##0.00") + ") as at " + Convert.ToDateTime(_month).AddMonths(1).AddDays(-1).Date, data = "" }, JsonRequestBehavior.AllowGet);
                                }

                                int count2 = 0;
                                List<hpr_hand_over_ac> _hlist = CHNLSVC.Finance.GetHandOverData(company, _loc, true);
                                if (_hlist != null)
                                {
                                    count2 = _hlist.Where(a => a.Hhoa_ac == _account).Count();
                                }
                                if (count2 > 0)
                                {
                                    return Json(new { success = false, login = true, msg = "Already available  this account as handing over (" + _account + ") " + "", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                List<hpr_hand_over_ac> _hlist2 = CHNLSVC.Finance.GetHandOverData(company, _loc, false);
                                if (_hlist2 != null)
                                {
                                    count2 = _hlist2.Where(a => a.Hhoa_ac == _account).Count();
                                }
                                if (count2 > 0)
                                {
                                    return Json(new { success = false, login = true, msg = "Already available  this account as handing over (" + _account + ") " + "", data = "" }, JsonRequestBehavior.AllowGet);
                                }

                              
                                if (Session["hpr_hand_over_ac"] == null)
                                {

                                    hpr_hand_over_ac ob = new hpr_hand_over_ac();
                                    ob.Hhoa_ac = _account;
                                    ob.Hhoa_bonus_month = Convert.ToDateTime(_month).AddMonths(1).AddDays(-1);
                                    ob.Hhoa_com = company;
                                    ob.Hhoa_cre_by = userId;
                                    ob.Hhoa_cre_dt = DateTime.Now;
                                    ob.Hhoa_pc = _loc;
                                    ob.Hhoa_rej_lmt = Convert.ToDecimal(_amt);
                                    _lst.Add(ob);
                                    Session["hpr_hand_over_ac"] = _lst;
                                }
                                else
                                {
                                    _lst = Session["hpr_hand_over_ac"] as List<hpr_hand_over_ac>;
                                    var count = _lst.Where(a => a.Hhoa_ac.ToString() == _account && a.Hhoa_bonus_month == Convert.ToDateTime(_month).AddMonths(1).AddDays(-1) && Convert.ToDecimal(a.Hhoa_rej_lmt.ToString()) == _amt && a.Hhoa_pc == _loc).Count();
                                    if (count > 0)
                                    {
                                        return Json(new { success = false, login = true, msg = "Already added this  Range!!", data = "" }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        hpr_hand_over_ac ob = new hpr_hand_over_ac();
                                        ob.Hhoa_ac = _account;
                                        ob.Hhoa_bonus_month = Convert.ToDateTime(_month).AddMonths(1).AddDays(-1);
                                        ob.Hhoa_com = company;
                                        ob.Hhoa_cre_by = userId;
                                        ob.Hhoa_cre_dt = DateTime.Now;
                                        ob.Hhoa_pc = _loc;
                                        ob.Hhoa_rej_lmt = Convert.ToDecimal(_amt);
                                        _lst.Add(ob);
                                        Session["hpr_hand_over_ac"] = _lst;
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
    }
}