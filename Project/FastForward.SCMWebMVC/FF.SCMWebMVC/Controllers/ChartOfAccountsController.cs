using FF.BusinessObjects;
using FF.BusinessObjects.Financial;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FF.SCMWebMVC.Controllers
{
    public class ChartOfAccountsController : BaseController
    {
        // GET: ChartOfAccounts
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["ref_cht_acc"] = null;
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }
        public JsonResult LoadMainTypes()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                 
                    DataTable mmaintypes = CHNLSVC.Finance.GetAccountFMainTypes(company);
                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (mmaintypes != null && mmaintypes.Rows.Count > 0)
                    {
                        ComboBoxObject o2 = new ComboBoxObject();
                        o2.Text = "Select";
                        o2.Value = "";
                        oList.Add(o2);
                        int i = 0;
                        foreach (var list in mmaintypes.Rows)
                        {
                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = mmaintypes.Rows[i]["ram_desc"].ToString();
                            o1.Value = mmaintypes.Rows[i]["ram_cd"].ToString();
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
        public JsonResult LoadSubTypes(string MainType)
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
                    if (MainType=="")
                    {
                        ComboBoxObject o1 = new ComboBoxObject();
                        o1.Text = "Select";
                        o1.Value = "";
                        oList.Add(o1);
                    }
                    else
                    {
                        //DataTable subtypes = CHNLSVC.Finance.GetSubTypes(company);
                        //oList = new List<ComboBoxObject>();
                        //if (subtypes != null && subtypes.Rows.Count > 0)
                        //{
                        //    ComboBoxObject o2 = new ComboBoxObject();
                        //    o2.Text = "Select";
                        //    o2.Value = "";
                        //    oList.Add(o2);
                        //    int i = 0;
                        //    foreach (var list in subtypes.Rows)
                        //    {
                        //        ComboBoxObject o1 = new ComboBoxObject();
                        //        o1.Text = subtypes.Rows[i]["rat_desc"].ToString();
                        //        o1.Value = subtypes.Rows[i]["rat_cd"].ToString();
                        //        oList.Add(o1);
                        //        i++;
                        //    }


                        //}
                        List<ref_acc_sgrp> maintypes = CHNLSVC.Finance.GetAccMainType(company, MainType);
                        if (maintypes != null && maintypes.Count > 0)
                        {
                            ComboBoxObject o2 = new ComboBoxObject();
                            o2.Text = "Select";
                            o2.Value = "";
                            oList.Add(o2);
                            foreach (var list in maintypes)
                            {
                                ComboBoxObject o1 = new ComboBoxObject();
                                o1.Text = list.ras_desc;
                                o1.Value = list.ras_cd;
                                oList.Add(o1);

                            }
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
        public JsonResult AdditionalTypes(string Subtype)
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
                    if (Subtype == "")
                    {
                        ComboBoxObject o1 = new ComboBoxObject();
                        o1.Text = "Select";
                        o1.Value = "";
                        oList.Add(o1);
                    }
                    else
                    {
                        DataTable subtypes = CHNLSVC.Finance.GetSubTypes(company, Subtype);
                        oList = new List<ComboBoxObject>();
                        if (subtypes != null && subtypes.Rows.Count > 0)
                        {
                            ComboBoxObject o2 = new ComboBoxObject();
                            o2.Text = "Select";
                            o2.Value = "";
                            oList.Add(o2);
                            int i = 0;
                            foreach (var list in subtypes.Rows)
                            {
                                ComboBoxObject o1 = new ComboBoxObject();
                                o1.Text = subtypes.Rows[i]["rss_desc"].ToString();
                                o1.Value = subtypes.Rows[i]["rss_cd"].ToString();
                                oList.Add(o1);
                                i++;
                            }


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
        public JsonResult LoadYesNoType()
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
                    o1.Text = "Yes";
                    o1.Value = "1";
                    oList.Add(o1);
                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "No";
                    o2.Value = "0";
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
        public JsonResult SaveAccountGRPDetails(string Code, string Type, string Desc, string OrdeNo, string Header, string IsSTp)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string err = "";
                    ref_acc_sgrp Ob = new ref_acc_sgrp();
                    Ob.ras_act = 1;
                    Ob.ras_cd = Code;
                    Ob.ras_com = company;
                    Ob.ras_cre_by = userId;
                    Ob.ras_cre_dt = DateTime.Now;
                    Ob.ras_desc = Desc;
                    Ob.ras_hed_cd = Header;
                    Ob.ras_hed_desc = Header;
                    Ob.ras_hed_ord = Convert.ToInt32(OrdeNo);
                    Ob.ras_is_sbtp = Convert.ToInt32(IsSTp);
                    Ob.ras_mgrp_cd = Type;
                    Ob.ras_mod_by = userId;
                    Ob.ras_mod_dt = DateTime.Now;
                    Ob.ras_sgrp_ord = Convert.ToInt32(OrdeNo);
                    int effect = CHNLSVC.Finance.SaveAccountGroupDetails(Ob, out err);

                    if (effect>=0)
                    {
                        return Json(new { success = true, login = true, Msg = "Successfully Saved.." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, Msg = err }, JsonRequestBehavior.AllowGet);
                    }

                   

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
        public JsonResult AddAccountDetails(string MainType, string SubType, string Addtype, string AccountCode, string AccountName, string OtherRef, string SuppAddr, string VatRegNo, string Active)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    List<ref_acc_sgrp> adddata = CHNLSVC.Finance.GetAccMainTypeDet(company, MainType, SubType);
                    DataTable _dtadd = CHNLSVC.Finance.GetSubTypesDetails(company, MainType, SubType);
                   // Session["ref_acc_sgrp"]
                    List<ref_cht_acc> _list = Session["ref_cht_acc"] as List<ref_cht_acc>;

                    if (_list != null && _list.Count>0)
                    {
                        var count = _list.Where(a => a.rca_acc_no == AccountCode).Count();
                        if (count>0)
                        {
                            return Json(new { success = false, login = true, Msg = "Already Added This Account" , Type="Info"}, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            

                            ref_cht_acc _ob = new ref_cht_acc();
                            _ob.rca_acc_desc = AccountName;
                            _ob.rca_acc_fmu = "";
                            _ob.rca_acc_no = AccountCode;
                            _ob.rca_acc_rmk = OtherRef;
                            _ob.rca_act = Convert.ToInt32(Active);
                            _ob.rca_com = company;
                            _ob.rca_cre_by = userId;
                            _ob.rca_cre_dt = DateTime.Now;
                            _ob.rca_grp_acc = AccountCode;

                            if (adddata != null && adddata.Count>0)
                            {
                                _ob.rca_hed_cd = adddata.First().ras_hed_cd;
                                _ob.rca_hed_desc = adddata.First().ras_hed_desc;
                                _ob.rca_hed_ord = adddata.First().ras_hed_ord;
                                _ob.rca_sgrp_cd = SubType;
                                _ob.rca_sgrp_desc = adddata.First().ras_desc;
                                _ob.rca_sgrp_ord = adddata.First().ras_sgrp_ord;
                            }

                            if (_dtadd != null && _dtadd.Rows.Count>0)
                            {
                                _ob.rca_ssub_cd = Addtype;
                                _ob.rca_ssub_desc = _dtadd.Rows[0]["rss_desc"].ToString();
                                _ob.rca_ssub_ord = Convert.ToInt32(_dtadd.Rows[0]["rss_ord"].ToString());
                            }
                           
                            _ob.rca_mgrp_cd = "";
                            _ob.rca_mod_by = userId;
                            _ob.rca_mod_dt = DateTime.Now;
                            _ob.rca_sbu = "";
                            _ob.rca_session = "";
                           
                           

                            _list.Add(_ob);
                            Session["ref_cht_acc"] = _list;
                        }
                    }
                    else
                    {
                       _list = new List<ref_cht_acc>();
                       ref_cht_acc _ob = new ref_cht_acc();
                       _ob.rca_acc_desc = AccountName;
                       _ob.rca_acc_fmu = "";
                       _ob.rca_acc_no = AccountCode;
                       _ob.rca_acc_rmk = OtherRef;
                       _ob.rca_act = Convert.ToInt32(Active);
                       _ob.rca_com = company;
                       _ob.rca_cre_by = userId;
                       _ob.rca_cre_dt = DateTime.Now;
                       _ob.rca_grp_acc = AccountCode;
                     
                       _ob.rca_mgrp_cd = "";
                       _ob.rca_mod_by = userId;
                       _ob.rca_mod_dt = DateTime.Now;
                       _ob.rca_sbu = "";
                       _ob.rca_session = "";
                       _ob.rca_sgrp_cd = SubType;
                       _ob.rca_sgrp_desc = "";
                       _ob.rca_sgrp_ord = 1;
                       if (adddata != null && adddata.Count > 0)
                       {
                           _ob.rca_hed_cd = adddata.First().ras_hed_cd;
                           _ob.rca_hed_desc = adddata.First().ras_hed_desc;
                           _ob.rca_hed_ord = adddata.First().ras_hed_ord;
                           _ob.rca_sgrp_cd = SubType;
                           _ob.rca_sgrp_desc = adddata.First().ras_desc;
                           _ob.rca_sgrp_ord = adddata.First().ras_sgrp_ord;
                       }

                       if (_dtadd != null && _dtadd.Rows.Count > 0)
                       {
                           _ob.rca_ssub_cd = Addtype;
                           _ob.rca_ssub_desc = _dtadd.Rows[0]["rss_desc"].ToString();
                           _ob.rca_ssub_ord = Convert.ToInt32(_dtadd.Rows[0]["rss_ord"].ToString());
                       }

                        _list.Add(_ob);
                        Session["ref_cht_acc"] = _list;
                    }

                    return Json(new { success = true, login = true, Data = _list , Type="Sucsess" }, JsonRequestBehavior.AllowGet);
                    
                    


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
                            List<ref_cht_acc> _listtt = new List<ref_cht_acc>();
                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                string _accno = "";
                                string _subType = "";
                                string _addiType = "";
                                string _remark = "";
                                if (workSheet.Cells[rowIterator, 1].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    _accno = workSheet.Cells[rowIterator, 1].Value.ToString();

                                }
                                if (workSheet.Cells[rowIterator, 2].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    _subType = workSheet.Cells[rowIterator, 2].Value.ToString();

                                }
                                if (workSheet.Cells[rowIterator, 3].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    _addiType = workSheet.Cells[rowIterator, 3].Value.ToString();

                                }
                                if (workSheet.Cells[rowIterator, 4].Value == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Row Number :" + rowIterator + " Some Data empty", data = "" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    _remark = workSheet.Cells[rowIterator, 4].Value.ToString();
                                    
                                }
                                // Session["ref_acc_sgrp"]
                                _listtt = Session["ref_cht_acc"] as List<ref_cht_acc>;

                                if (_listtt != null && _listtt.Count > 0)
                                {
                                    var count = _listtt.Where(a => a.rca_acc_no == _accno).Count();
                                    if (count > 0)
                                    {
                                        return Json(new { success = false, login = true, Msg = "Already Added This Account", Type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        ref_cht_acc _ob = new ref_cht_acc();
                                        _ob.rca_acc_desc = _accno;
                                        _ob.rca_acc_fmu = "";
                                        _ob.rca_acc_no = _accno;
                                        _ob.rca_acc_rmk = _remark;
                                        _ob.rca_act = Convert.ToInt32(1);
                                        _ob.rca_com = company;
                                        _ob.rca_cre_by = userId;
                                        _ob.rca_cre_dt = DateTime.Now;
                                        _ob.rca_grp_acc = _accno;
                                        _ob.rca_hed_cd = "";
                                        _ob.rca_hed_desc = "";
                                        _ob.rca_hed_ord = 1;
                                        _ob.rca_mgrp_cd = "";
                                        _ob.rca_mod_by = userId;
                                        _ob.rca_mod_dt = DateTime.Now;
                                        _ob.rca_sbu = "";
                                        _ob.rca_session = "";
                                        _ob.rca_sgrp_cd = _subType;
                                        _ob.rca_sgrp_desc = "";
                                        _ob.rca_sgrp_ord = 1;
                                        _ob.rca_ssub_cd = _addiType;
                                        _ob.rca_ssub_desc = "";
                                        _ob.rca_ssub_ord = 1;

                                        _listtt.Add(_ob);
                                        Session["ref_cht_acc"] = _listtt;
                                    }
                                }
                                else
                                {
                                    _listtt = new List<ref_cht_acc>();
                                    ref_cht_acc _ob = new ref_cht_acc();
                                    _ob.rca_acc_desc = _accno;
                                    _ob.rca_acc_fmu = "";
                                    _ob.rca_acc_no = _accno;
                                    _ob.rca_acc_rmk = _remark;
                                    _ob.rca_act = Convert.ToInt32(1);
                                    _ob.rca_com = company;
                                    _ob.rca_cre_by = userId;
                                    _ob.rca_cre_dt = DateTime.Now;
                                    _ob.rca_grp_acc = _accno;
                                    _ob.rca_hed_cd = "";
                                    _ob.rca_hed_desc = "";
                                    _ob.rca_hed_ord = 1;
                                    _ob.rca_mgrp_cd = "";
                                    _ob.rca_mod_by = userId;
                                    _ob.rca_mod_dt = DateTime.Now;
                                    _ob.rca_sbu = "";
                                    _ob.rca_session = "";
                                    _ob.rca_sgrp_cd = _subType;
                                    _ob.rca_sgrp_desc = "";
                                    _ob.rca_sgrp_ord = 1;
                                    _ob.rca_ssub_cd = _addiType;
                                    _ob.rca_ssub_desc = "";
                                    _ob.rca_ssub_ord = 1;

                                    _listtt.Add(_ob);
                                    Session["ref_cht_acc"] = _listtt;
                                }

                            }
                            return Json(new { success = true, login = true, list = _listtt }, JsonRequestBehavior.AllowGet);
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
        public JsonResult RemoveItemDet(string AccNo, string Mtype, string Stype, string OtherType)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<ref_cht_acc> _list = new List<ref_cht_acc>();

                if (Session["ref_cht_acc"] != null)
                {
                    _list = (List<ref_cht_acc>)Session["ref_cht_acc"];
                }
                else
                {
                    _list = new List<ref_cht_acc>();

                }
                var itemToRemove = _list.First(r => r.rca_acc_no == AccNo && r.rca_mgrp_cd == Mtype && r.rca_sgrp_cd == Stype && r.rca_ssub_cd == OtherType);
                _list.Remove(itemToRemove);
                Session["ref_cht_acc"] = _list;
                return Json(new { success = true, login = true, data = _list }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        //SaveCharAccounts
        public JsonResult SaveCharAccounts()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string err = "";
                List<ref_cht_acc> _list = new List<ref_cht_acc>();
                
                if (Session["ref_cht_acc"] != null)
                {
                    _list = (List<ref_cht_acc>)Session["ref_cht_acc"];
                    int effect = CHNLSVC.Finance.SaveChartAccDetails(_list,out err);
                    if (effect >= 0)
                    {
                        return Json(new { success = true, login = true, Msg = "Successfully Saved", Type="Success" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, Msg = err, Type = "Err" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true, Msg = "Please Enter Details", Type = "Info" }, JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult LoadDetails(string Code)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<ref_cht_acc> _list = new List<ref_cht_acc>();
                List<ref_cht_accgrp> _listG = new List<ref_cht_accgrp>();
                _listG = CHNLSVC.Finance.GetAccGroup(Code);
                _list = CHNLSVC.Finance.GetAcc(Code);
                string Maintype = "";
                string SubType = "";
                string AddType = "";
                String AccName = "";

                if (_listG != null && _listG.Count>0)
                {
                    Maintype = _listG.First().rcg_mgrp_cd;
                    SubType = _listG.First().rcg_sgrp_cd;
                    AddType = _listG.First().rcg_ssub_cd;
                    AccName = _listG.First().rcg_acc_desc;
                }

                Session["ref_cht_acc"] = _list;
                return Json(new { success = true, login = true, data = _list, Maintype = Maintype, SubType = SubType, AddType = AddType, AccName = AccName }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult LoadHeading()
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
                    DataTable hed = CHNLSVC.Finance.GetAccHeading();
                        oList = new List<ComboBoxObject>();
                        if (hed != null && hed.Rows.Count > 0)
                        {
                            ComboBoxObject o2 = new ComboBoxObject();
                            o2.Text = "Select";
                            o2.Value = "";
                            oList.Add(o2);
                            int i = 0;
                            foreach (var list in hed.Rows)
                            {
                                ComboBoxObject o1 = new ComboBoxObject();
                                o1.Text = hed.Rows[i]["rah_desc"].ToString();
                                o1.Value = hed.Rows[i]["rah_cd"].ToString();
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