using FF.BusinessObjects;
using FF.BusinessObjects.ToursNew;
using FF.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace FastForward.WebAbansTours.Controllers
{
    public class DataEntryController : BaseController
    {
        public  DataEntryController(){
            //string userId = (String)(Session["UserId"]);
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1007);
                if (per.MNU_ID == 0)
                {
                    throw new AuthenticationException("You do not have the necessary permission to perform this action");
                }
            }
            else
            {
                Redirect("~/Login/index");
            }
            
        }
        public ActionResult CreateCustomerPartial()
        {
            return View("_Customer");
        }
        // GET: DataEntry

        List<Cus_chg_cds> chgcdDetList;
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["closeBtn"] = "false";
                Session["_isExsit"] = "false";
                Session["_isGroup"] = "false";
                Session["chgDetList"] = null;
                return View();
            }
            else {
                return Redirect("~/Login/index");
            }
        }

        public JsonResult CustomerCreation(MasterBusinessEntity cust, string asdriver)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string _cusCode = "";
                    Int32 _effect = 0;
                    //if (cust.Mbe_nic == null && cust.Mbe_br_no == null && cust.Mbe_pp_no == null && cust.Mbe_dl_no == null && cust.Mbe_mob == null)
                    //{
                    //    return Json(new { success = false, login = true, msg = Resource.txtInvalidNicBrDlMob ,type="Info"});
                    //}
                    if (cust.Mbe_agre_send_email == true)
                    {
                        if (string.IsNullOrEmpty(cust.Mbe_email))
                        {
                            return Json(new { success = false, login = true, msg = Resource.txtEnterEmail, type = "Info" });
                        }
                    }

                    if (string.IsNullOrEmpty(cust.Mbe_name))
                    {
                        return Json(new { success = false, login = true, msg = Resource.txtCusEnter, type = "Info" });
                    }
                    //if (string.IsNullOrEmpty(cust.Mbe_add1))
                    //{
                    //    return Json(new { success = false, login = true, msg = Resource.txtCustPerAdd, type = "Info" });
                    //}
                    if (cust.Mbe_is_tax == true)
                    {
                        if (string.IsNullOrEmpty(cust.Mbe_tax_no))
                        {
                            return Json(new { success = false, login = true, msg = Resource.txtVatRegNoEnter, type = "Info" });
                        }
                    }
                    if (cust.Mbe_is_svat == true)
                    {
                        if (string.IsNullOrEmpty(cust.Mbe_svat_no))
                        {
                            return Json(new { success = false, login = true, msg = Resource.txtSvatRegNoEnter, type = "Info" });
                        }
                    }
                    if (cust.Mbe_is_svat == true)
                    {
                        MasterCompany _newCom = new MasterCompany();
                        _newCom = CHNLSVC.General.GetCompByCode(HttpContext.Session["UserCompanyCode"] as string);

                        if (_newCom.Mc_cd != null)
                        {
                            if (string.IsNullOrEmpty(_newCom.Mc_tax2))
                            {
                                return Json(new { success = false, login = true, msg = Resource.txtUnableToCreUnderCmpy, type = "Info" });
                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = Resource.txtCantFndCmpy, type = "Info" });
                           
                        }
                    }
                    if (!string.IsNullOrEmpty(cust.Mbe_email))
                    {
                        if (!IsValidEmail(cust.Mbe_email))
                        {
                            return Json(new { success = false, login = true, msg = Resource.txtInvlEmail, type = "Info" });
                        }
                    }
                    if (!string.IsNullOrEmpty(cust.Mbe_wr_email))
                    {
                        if (!IsValidEmail(cust.Mbe_wr_email))
                        {
                            return Json(new { success = false, login = true, msg = Resource.txtInvWkEmail, type = "Info" });
                        }
                    }
                    if (!string.IsNullOrEmpty(cust.Mbe_wr_tel))
                    {
                        Boolean _isValid = IsValidMobileOrLandNo(cust.Mbe_wr_tel.Trim());

                        if (_isValid == false)
                        {
                            return Json(new { success = false, login = true, msg = Resource.TxtInvWkPh, type = "Info" });
                        }
                    }
                    if (!string.IsNullOrEmpty(cust.Mbe_tel))
                    {
                        Boolean _isvalid = IsValidMobileOrLandNo(cust.Mbe_tel.Trim());

                        if (_isvalid == false)
                        {
                            return Json(new { success = false, login = true, msg = Resource.txtInvPh, type = "Info" });
                        }

                    }
                    if (cust.Mbe_nic != null) {
                        List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(company, cust.Mbe_nic.Trim(), "", "", "", "", 1);
                        if (_custList != null && _custList.Count > 1 && cust.Mbe_nic.ToUpper() != "N/A")
                        {
                            string _custNIC = "Duplicate customers found ";
                            foreach (var _nicCust in _custList)
                            {
                                _custNIC = _custNIC + _nicCust.Mbe_cd.ToString() + " | " + _nicCust.Mbe_name.ToString() + "\n";
                            }
                            return Json(new { success = false, login = true, msg = _custNIC, type = "Error" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (cust.Mbe_mob != null) {
                        List<MasterBusinessEntity> custProf = CHNLSVC.Sales.GetActiveCustomerDetailList(null, company, null, cust.Mbe_mob, "C");
                        if (custProf != null && custProf.Count > 1 && cust.Mbe_mob != "N/A")
                        {
                            string _custMob = "Duplicate customers found ";
                            foreach (var _nicCust in custProf)
                            {
                                _custMob = _custMob + _nicCust.Mbe_cd.ToString() + " | " + _nicCust.Mbe_name.ToString() + "\n";
                            }
                            return Json(new { success = false, login = true, msg = _custMob, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    bool isExsit = (HttpContext.Session["_isExsit"] as string == "true") ? true : false;
                    bool isGroup = (HttpContext.Session["_isGroup"] as string == "true") ? true : false;
                   MasterBusinessEntity _mstBusEnt= Collect_Cust(cust);
                   GroupBussinessEntity _custGroup= Collect_GroupCust(cust);


                   List<MasterBusinessEntityInfo> _busInfoList = new List<MasterBusinessEntityInfo>();
                   CustomerAccountRef _account = new CustomerAccountRef();

                   List<BusEntityItem> busItemList = new List<BusEntityItem>();

                   //if (_isExsit.Value == false)
                   if (!isExsit)
                   {
                       _mstBusEnt.Mbe_BI_Year = cust.Mbe_BI_Year;
                       _mstBusEnt.MBE_CR_PERIOD = cust.MBE_CR_PERIOD;
                       _effect = CHNLSVC.Sales.SaveBusinessEntityDetailWithGroup(_mstBusEnt, _account, _busInfoList, busItemList, out _cusCode, null, isExsit, isGroup, _custGroup, false, null, asdriver, userDefPro);
                   }
                   else
                   {
                       _cusCode = _mstBusEnt.Mbe_cd;
                       _mstBusEnt.Mbe_BI_Year = cust.Mbe_BI_Year;
                       _mstBusEnt.Mbe_Credit_Period = cust.Mbe_Credit_Period;
                       _effect = CHNLSVC.Sales.UpdateBusinessEntityProfileWithGroup(_mstBusEnt, Session["UserID"].ToString(), Convert.ToDateTime(DateTime.Today).Date, 0, _busInfoList, null, busItemList, _custGroup);
                   }

                   if (_effect == 1)
                   {
                       if (!isExsit)
                       {
                           return Json(new { success = true, login = true, cusCd = _cusCode, msg = "New customer created. Customer Code : " + _cusCode, type = "Success" }, JsonRequestBehavior.AllowGet);
                       }
                       else
                       {
                           return Json(new { success = true, login = true, msg = "Existing customer updated.", type = "Success", cusCd = _cusCode }, JsonRequestBehavior.AllowGet);
                       }
                   }
                   else
                   {
                       if (!string.IsNullOrEmpty(_cusCode))
                       {
                           return Json(new { success = false, login = true, msg = "Error.:" + _cusCode ,type = "Error"}, JsonRequestBehavior.AllowGet);
                       }
                       else
                       {
                           return Json(new { success = false, login = true, msg = "Creation Fail.", type = "Error" }, JsonRequestBehavior.AllowGet);
                       }
                   }

                }
                else {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false,login=true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

            System.Text.RegularExpressions.Match match = Regex.Match(email.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }
        private MasterBusinessEntity Collect_Cust(MasterBusinessEntity _custProfilere)
        {
            try
            {
                Boolean _isSMS = false;
                Boolean _isSVAT = false;
                Boolean _isVAT = false;
                Boolean _TaxEx = false;
                Boolean _isEmail = false;
                MasterBusinessEntity _custProfile = new MasterBusinessEntity();
                _custProfile.Mbe_acc_cd = null;
                _custProfile.Mbe_act = true;
                _custProfile.Mbe_add1 = (_custProfilere.Mbe_add1 != null) ? _custProfilere.Mbe_add1.Trim() : "";
                _custProfile.Mbe_add2 = (_custProfilere.Mbe_add2 != null) ? _custProfilere.Mbe_add2.Trim() : "";
                if (_custProfilere.Mbe_agre_send_sms == true)
                {
                    _isSMS = true;
                }
                else
                {
                    _isSMS = false;
                }
                _custProfile.Mbe_agre_send_sms = _isSMS;
                _custProfile.Mbe_br_no = (_custProfilere.Mbe_br_no != null) ? _custProfilere.Mbe_br_no.Trim() : "";
                _custProfile.Mbe_cate = _custProfilere.Mbe_cate;

                if (HttpContext.Session["_isExsit"] as string == "false" && HttpContext.Session["_isGroup"] as string == "false")
                {
                    _custProfile.Mbe_cd = null;
                }
                else
                {
                    _custProfile.Mbe_cd = (_custProfilere.Mbe_cd != null) ? _custProfilere.Mbe_cd.Trim() : _custProfilere.Mbe_cd;
                }
                _custProfile.Mbe_com = Session["UserCompanyCode"].ToString();
                _custProfile.Mbe_contact = null;
                _custProfile.Mbe_country_cd = (_custProfilere.Mbe_country_cd != null) ? _custProfilere.Mbe_country_cd.Trim() : "";
                _custProfile.Mbe_cr_add1 = (_custProfilere.Mbe_cr_add1 != null) ? _custProfilere.Mbe_cr_add1.Trim() : "";
                _custProfile.Mbe_cr_add2 = (_custProfilere.Mbe_cr_add2 != null) ? _custProfilere.Mbe_cr_add2.Trim() : "";
                _custProfile.Mbe_cr_country_cd = (_custProfilere.Mbe_cr_country_cd != null) ? _custProfilere.Mbe_cr_country_cd.Trim() : "";
                _custProfile.Mbe_cr_distric_cd = (_custProfilere.Mbe_cr_distric_cd != null) ? _custProfilere.Mbe_cr_distric_cd : "";
                _custProfile.Mbe_cr_email = null;
                _custProfile.Mbe_cr_fax = null;
                _custProfile.Mbe_cr_postal_cd = (_custProfilere.Mbe_cr_postal_cd != null) ? _custProfilere.Mbe_cr_postal_cd.Trim() : "";
                _custProfile.Mbe_cr_province_cd = (_custProfilere.Mbe_cr_province_cd != null) ? _custProfilere.Mbe_cr_province_cd.Trim() : "";
                _custProfile.Mbe_cr_tel = (_custProfilere.Mbe_cr_tel != null) ? _custProfilere.Mbe_cr_tel.Trim() : "";
                _custProfile.Mbe_cr_town_cd = (_custProfilere.Mbe_cr_town_cd != null) ? _custProfilere.Mbe_cr_town_cd.Trim() : "";
                _custProfile.Mbe_cre_by = Session["UserID"].ToString();
                _custProfile.Mbe_cre_dt = Convert.ToDateTime(DateTime.Today).Date;
                _custProfile.Mbe_cre_pc = Session["UserDefProf"].ToString();
                _custProfile.Mbe_cust_com = Session["UserCompanyCode"].ToString();
                _custProfile.Mbe_cust_loc = Session["UserDefLoca"].ToString();
                _custProfile.Mbe_distric_cd = (_custProfilere.Mbe_distric_cd != null) ? _custProfilere.Mbe_distric_cd : "";
                _custProfile.Mbe_dl_no = (_custProfilere.Mbe_dl_no != null) ? _custProfilere.Mbe_dl_no.Trim() : "";
                _custProfile.Mbe_dob = Convert.ToDateTime(_custProfilere.Mbe_dob).Date;
                _custProfile.Mbe_email = (_custProfilere.Mbe_email != null) ? _custProfilere.Mbe_email.Trim() : "";
                _custProfile.Mbe_fax = null;
                _custProfile.Mbe_ho_stus = "GOOD";
                _custProfile.Mbe_income_grup = null;
                _custProfile.Mbe_intr_com = false;
                _custProfile.Mbe_is_suspend = false;


                if (_custProfilere.Mbe_is_svat == true)
                {
                    _isSVAT = true;
                }
                else
                {
                    _isSVAT = false;
                }

                _custProfile.Mbe_is_svat = _isSVAT;

                if (_custProfilere.Mbe_is_tax == true)
                {
                    _isVAT = true;
                }
                else
                {
                    _isVAT = false;
                }
                _custProfile.Mbe_is_tax = _isVAT;
                _custProfile.Mbe_mob = (_custProfilere.Mbe_mob != null) ? _custProfilere.Mbe_mob.Trim() : "";
                _custProfile.Mbe_name = (_custProfilere.Mbe_name != null) ? _custProfilere.Mbe_name.Trim() : "";
                _custProfile.Mbe_nic = (_custProfilere.Mbe_nic != null) ? _custProfilere.Mbe_nic.Trim() : "";
                _custProfile.Mbe_oth_id_no = null;
                _custProfile.Mbe_oth_id_tp = null;
                _custProfile.Mbe_pc_stus = "GOOD";
                _custProfile.Mbe_postal_cd = (_custProfilere.Mbe_postal_cd != null) ? _custProfilere.Mbe_postal_cd.Trim() : "";
                _custProfile.Mbe_pp_no = (_custProfilere.Mbe_pp_no != null) ? _custProfilere.Mbe_pp_no.Trim() : "";
                _custProfile.Mbe_province_cd = (_custProfilere.Mbe_province_cd != null) ? _custProfilere.Mbe_province_cd.Trim() : "";
                _custProfile.Mbe_sex = _custProfilere.Mbe_sex;
                _custProfile.Mbe_sub_tp = null;
                _custProfile.Mbe_svat_no = (_custProfilere.Mbe_svat_no != null) ? _custProfilere.Mbe_svat_no.Trim() : "";


                if (_custProfilere.Mbe_tax_ex == true)
                {
                    _TaxEx = true;
                }
                else
                {
                    _TaxEx = false;
                }
                _custProfile.Mbe_tax_ex = _TaxEx;
                _custProfile.Mbe_tax_no = (_custProfilere.Mbe_tax_no != null) ? _custProfilere.Mbe_tax_no.Trim() : "";
                _custProfile.Mbe_tel = (_custProfilere.Mbe_tel != null) ? _custProfilere.Mbe_tel.Trim() : "";
                _custProfile.Mbe_town_cd = (_custProfilere.Mbe_town_cd != null) ? _custProfilere.Mbe_town_cd.Trim() : "";
                _custProfile.Mbe_tp = "C";
                _custProfile.Mbe_wr_add1 = (_custProfilere.Mbe_wr_add1 != null) ? _custProfilere.Mbe_wr_add1.Trim() : "";
                _custProfile.Mbe_wr_add2 = (_custProfilere.Mbe_wr_add2 != null) ? _custProfilere.Mbe_wr_add2.Trim() : "";
                _custProfile.Mbe_wr_com_name = (_custProfilere.Mbe_wr_com_name != null) ? _custProfilere.Mbe_wr_com_name.Trim() : "";
                _custProfile.Mbe_wr_country_cd = null;
                _custProfile.Mbe_wr_dept = (_custProfilere.Mbe_wr_dept != null) ? _custProfilere.Mbe_wr_dept.Trim() : "";
                _custProfile.Mbe_wr_designation = (_custProfilere.Mbe_wr_designation != null) ? _custProfilere.Mbe_wr_designation.Trim() : "";
                _custProfile.Mbe_wr_distric_cd = null;
                _custProfile.Mbe_wr_email = (_custProfilere.Mbe_wr_email != null) ? _custProfilere.Mbe_wr_email.Trim() : "";
                _custProfile.Mbe_wr_fax = (_custProfilere.Mbe_wr_fax != null) ? _custProfilere.Mbe_wr_fax.Trim() : "";
                _custProfile.Mbe_wr_proffesion = null;
                _custProfile.Mbe_wr_province_cd = null;
                _custProfile.Mbe_wr_tel = (_custProfilere.Mbe_wr_tel != null) ? _custProfilere.Mbe_wr_tel.Trim() : "";
                _custProfile.Mbe_wr_town_cd = null;
                _custProfile.MBE_FNAME = (_custProfilere.MBE_FNAME != null) ? _custProfilere.MBE_FNAME.Trim() : "";
                _custProfile.MBE_SNAME = (_custProfilere.MBE_SNAME != null) ? _custProfilere.MBE_SNAME.Trim() : "";
                _custProfile.MBE_INI = (_custProfilere.MBE_INI != null) ? _custProfilere.MBE_INI.Trim() : "";
                _custProfile.MBE_TIT = (_custProfilere.MBE_TIT != null) ? _custProfilere.MBE_TIT.Trim() : "";
                _custProfile.Mbe_acc_cd = (_custProfilere.Mbe_acc_cd != null) ? _custProfilere.Mbe_acc_cd.Trim() : "";
                _custProfile.Mbe_pp_isu_dte = Convert.ToDateTime(_custProfilere.Mbe_pp_isu_dte).Date;
                _custProfile.Mbe_pp_exp_dte = Convert.ToDateTime(_custProfilere.Mbe_pp_exp_dte).Date;
                _custProfile.Mbe_dl_isu_dte = Convert.ToDateTime(_custProfilere.Mbe_dl_isu_dte).Date;
                _custProfile.Mbe_dl_exp_dte = Convert.ToDateTime(_custProfilere.Mbe_dl_exp_dte).Date;
                if (_custProfile.Mbe_agre_send_email == true)
                {
                    _isEmail = true;
                }
                else
                {
                    _isEmail = false;
                }
                _custProfile.Mbe_agre_send_email = _isEmail;
                _custProfile.Mbe_cust_lang = _custProfilere.Mbe_cust_lang;
                /* Boolean _isSMS = false;
                 Boolean _isSVAT = false;
                 Boolean _isVAT = false;
                 Boolean _TaxEx = false;
                 Boolean _isEmail = false;
                 _custProfile = new MasterBusinessEntity();
                 _custProfile.Mbe_acc_cd = null;
                 _custProfile.Mbe_act = true;
                 if (_custProfile.Mbe_agre_send_sms == true)
                 {
                     _isSMS = true;
                 }
                 else
                 {
                     _isSMS = false;
                 }
                 _custProfile.Mbe_agre_send_sms = _isSMS;

                 if (HttpContext.Session["_isExsit"] as string == "false" && HttpContext.Session["_isGroup"] as string == "false")
                 {
                     _custProfile.Mbe_cd = null;
                 }
                 _custProfile.Mbe_com = HttpContext.Session["UserCompanyCode"] as string;
                 _custProfile.Mbe_contact = null;
                 _custProfile.Mbe_cr_email = null;
                 _custProfile.Mbe_cr_fax = null;
                 _custProfile.Mbe_cre_by = HttpContext.Session["UserID"] as string;
                 _custProfile.Mbe_cre_dt = Convert.ToDateTime(DateTime.Today).Date;
                 _custProfile.Mbe_cre_pc = HttpContext.Session["UserDefProf"] as string;
                 _custProfile.Mbe_cust_com = HttpContext.Session["UserCompanyCode"] as string;
                 _custProfile.Mbe_cust_loc = HttpContext.Session["UserDefLoca"] as string;
                 _custProfile.Mbe_fax = null;
                 _custProfile.Mbe_ho_stus = "GOOD";
                 _custProfile.Mbe_income_grup = null;
                 _custProfile.Mbe_intr_com = false;
                 _custProfile.Mbe_is_suspend = false;

                 if (_custProfile.Mbe_is_svat == true)
                 {
                     _isSVAT = true;
                 }
                 else
                 {
                     _isSVAT = false;
                 }

                 _custProfile.Mbe_is_svat = _isSVAT;

                 if (_custProfile.Mbe_is_tax == true)
                 {
                     _isVAT = true;
                 }
                 else
                 {
                     _isVAT = false;
                 }
                 _custProfile.Mbe_is_tax = _isVAT;
                 _custProfile.Mbe_oth_id_no = null;
                 _custProfile.Mbe_oth_id_tp = null;
                 _custProfile.Mbe_pc_stus = "GOOD";
                 _custProfile.Mbe_sub_tp = null;

                 if (_custProfile.Mbe_tax_ex == true)
                 {
                     _TaxEx = true;
                 }
                 else
                 {
                     _TaxEx = false;
                 }
                 _custProfile.Mbe_tax_ex = _TaxEx;
                 _custProfile.Mbe_tp = "C";
                 _custProfile.Mbe_wr_country_cd = null;
                 _custProfile.Mbe_wr_distric_cd = null;
                 _custProfile.Mbe_wr_proffesion = null;
                 _custProfile.Mbe_wr_province_cd = null;
                 _custProfile.Mbe_wr_town_cd = null;
                 if (_custProfile.Mbe_agre_send_email == true)
                 {
                     _isEmail = true;
                 }
                 else
                 {
                     _isEmail = false;
                 }
                 _custProfile.Mbe_agre_send_email = _isEmail;*/
                return _custProfile;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        private GroupBussinessEntity Collect_GroupCust(MasterBusinessEntity _custProfile)
        {
            try
            {
                GroupBussinessEntity _custGroup = new GroupBussinessEntity();
                _custGroup.Mbg_cd = (_custProfile.Mbe_cd!=null)?_custProfile.Mbe_cd.Trim():"";
                _custGroup.Mbg_name = _custProfile.Mbe_name.Trim();
                _custGroup.Mbg_tit = _custProfile.MBE_TIT;
                _custGroup.Mbg_ini = (_custProfile.MBE_INI!=null)?_custProfile.MBE_INI.Trim():"";
                _custGroup.Mbg_fname = (_custProfile.MBE_FNAME!=null)?_custProfile.MBE_FNAME.Trim():"";
                _custGroup.Mbg_sname = (_custProfile.MBE_SNAME!=null)?_custProfile.MBE_SNAME.Trim():"";
                _custGroup.Mbg_nationality = "SL";
                _custGroup.Mbg_add1 = (_custProfile.Mbe_add1!=null)?_custProfile.Mbe_add1.Trim():"";
                _custGroup.Mbg_add2 = (_custProfile.Mbe_add2!=null)?_custProfile.Mbe_add2.Trim():"";
                _custGroup.Mbg_town_cd = (_custProfile.Mbe_town_cd!=null)?_custProfile.Mbe_town_cd.Trim():"";
                _custGroup.Mbg_distric_cd = (_custProfile.Mbe_distric_cd!=null)?_custProfile.Mbe_distric_cd.Trim():"";
                _custGroup.Mbg_province_cd = (_custProfile.Mbe_province_cd!=null)?_custProfile.Mbe_province_cd.Trim():"";
                _custGroup.Mbg_country_cd = (_custProfile.Mbe_country_cd!=null)?_custProfile.Mbe_country_cd.Trim():"";
                _custGroup.Mbg_tel = (_custProfile.Mbe_tel!=null)?_custProfile.Mbe_tel.Trim():"";
                _custGroup.Mbg_fax = "";
                _custGroup.Mbg_postal_cd = (_custProfile.Mbe_postal_cd!=null)?_custProfile.Mbe_postal_cd.Trim():"";
                _custGroup.Mbg_mob = (_custProfile.Mbe_mob!=null)?_custProfile.Mbe_mob.Trim():"";
                _custGroup.Mbg_nic = (_custProfile.Mbe_nic!=null)?_custProfile.Mbe_nic.Trim():"";
                _custGroup.Mbg_pp_no = (_custProfile.Mbe_pp_no!=null)?_custProfile.Mbe_pp_no.Trim():"";
                _custGroup.Mbg_dl_no = (_custProfile.Mbe_dl_no!=null)?_custProfile.Mbe_dl_no.Trim():"";
                _custGroup.Mbg_br_no = (_custProfile.Mbe_br_no!=null)?_custProfile.Mbe_br_no.Trim():"";
                _custGroup.Mbg_email = (_custProfile.Mbe_email!=null)?_custProfile.Mbe_email.Trim():"";


                _custGroup.Mbg_pp_isu_dte=Convert.ToDateTime(_custProfile.Mbe_pp_isu_dte).Date;
                _custGroup.Mbg_pp_exp_dte=Convert.ToDateTime(_custProfile.Mbe_pp_exp_dte).Date;
                _custGroup.Mbg_dl_isu_dte=Convert.ToDateTime(_custProfile.Mbe_dl_isu_dte).Date;
                _custGroup.Mbg_dl_exp_dte = Convert.ToDateTime(_custProfile.Mbe_dl_exp_dte).Date;

                _custGroup.Mbg_contact = "";
                _custGroup.Mbg_act = true;
                _custGroup.Mbg_is_suspend = false;
                _custGroup.Mbg_sex = _custProfile.Mbe_sex;
                _custGroup.Mbg_dob = Convert.ToDateTime(_custProfile.Mbe_dob).Date;
                _custGroup.Mbg_cre_by = HttpContext.Session["UserID"] as string;
                _custGroup.Mbg_mod_by = HttpContext.Session["UserID"] as string;
                return _custGroup;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        [HttpGet]
        public JsonResult LoadLanguage()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    DataTable _tbl = CHNLSVC.General.get_Language();
                    List<laguage> list = new List<laguage>();

                    list = (from DataRow row in _tbl.Rows
                            select new laguage
                           {
                               MLA_CD = row["MLA_CD"].ToString(),
                               MLA_DESC = row["MLA_DESC"].ToString()

                           }).ToList();
                    if (list.Count > 0)
                    {
                        return Json(new { success = true, login = true, data = list }, JsonRequestBehavior.AllowGet);
                    }
                    else {
                        return Json(new { success = false, login = true, data = list, msg = Resource.txtNoLang }, JsonRequestBehavior.AllowGet);
                    }
                
                }else{
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
            }
        }
        public  JsonResult LoadCustomerType()
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
                    //ComboBoxObject o1 = new ComboBoxObject();
                    //o1.Text = "INDIVIDUAL";
                    //o1.Value = "INDIVIDUAL";
                    //oList.Add(o1);

                    //ComboBoxObject o2 = new ComboBoxObject();
                    //o2.Text = "GROUP";
                    //o2.Value = "GROUP";
                    //oList.Add(o2);

                    //ComboBoxObject o3 = new ComboBoxObject();
                    //o3.Text = "LEASE";
                    //o3.Value = "LEASE";
                    //oList.Add(o3);
                    List<MST_CUSTOMER_TYPE> types = CHNLSVC.Tours.getCustomerTypes();
                    if (types.Count > 0) {
                        foreach (MST_CUSTOMER_TYPE typ in types) {
                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = typ.CUS_TP_DESC;
                            o1.Value = typ.CUS_TP_CD;
                            oList.Add(o1);
                        }
                    }


                    return Json(new { success = true, login = true, data = oList }, JsonRequestBehavior.AllowGet);
                }else{
                     return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                return Json(new { success = false, login = true, data = Resource.ServerError }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult loadTitles()
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
                    o1.Text = "MR.";
                    o1.Value = "MR.";
                    oList.Add(o1);

                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "MRS.";
                    o2.Value = "MRS.";
                    oList.Add(o2);

                    ComboBoxObject o3 = new ComboBoxObject();
                    o3.Text = "MS.";
                    o3.Value = "MS.";
                    oList.Add(o3);

                    ComboBoxObject o4 = new ComboBoxObject();
                    o4.Text = "MISS.";
                    o4.Value = "MISS.";
                    oList.Add(o4);

                    ComboBoxObject o5 = new ComboBoxObject();
                    o5.Text = "DR.";
                    o5.Value = "DR.";
                    oList.Add(o5);

                    ComboBoxObject o6 = new ComboBoxObject();
                    o6.Text = "REV.";
                    o6.Value = "REV.";
                    oList.Add(o6);
                    return Json(new { success = true, login = true, data = oList }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = Resource.ServerError }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult loadSex()
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
                    o1.Text = "MALE";
                    o1.Value = "MALE";
                    oList.Add(o1);

                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "FEMALE";
                    o2.Value = "FEMALE";
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
                return Json(new { success = false, login = true, data = Resource.ServerError }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult loadcreditperiod()
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
                    o1.Text = "1 Months";
                    o1.Value = "1 Months";
                    oList.Add(o1);

                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "2 Months";
                    o2.Value = "2 Months";
                    oList.Add(o2);

                    ComboBoxObject o3 = new ComboBoxObject();
                    o3.Text = "3 Months";
                    o3.Value = "3 Months";
                    oList.Add(o3);

                    ComboBoxObject o4 = new ComboBoxObject();
                    o4.Text = "6 Months";
                    o4.Value = "6 Months";
                    oList.Add(o4);
                    return Json(new { success = true, login = true, data = oList }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = Resource.ServerError }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult loadTaxPosition()
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
                    o1.Text = "S-Vat";
                    o1.Value = "1";
                    oList.Add(o1);

                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "Vat";
                    o2.Value = "1";
                    oList.Add(o2);
                    ComboBoxObject o3 = new ComboBoxObject();
                    o3.Text = "Exempted";
                    o3.Value = "1";
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
                return Json(new { success = false, login = true, data = Resource.ServerError }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult cusCodeTextChanged(string val)
        {
            string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (!string.IsNullOrEmpty(val))
                    {
                        Session["_isExsit"] = "false";
                        Session["_isGroup"] = "false";
                        MasterBusinessEntity custProf = GetbyCustCD(val.Trim());
                        chgcdDetList = CHNLSVC.Tours.GETBUSCHARGECODES(val.Trim());
                        Session["chgDetList"] = chgcdDetList;
                        if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                        {
                            Session["_isExsit"] = "true";
                            return Json(new { success = true, local = true, login = true, data = custProf, chgcdDetList = chgcdDetList }, JsonRequestBehavior.AllowGet);
                        }
                        else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                        {
                            return Json(new { success = false, login = true, msg = Resource.txtInacCus, data = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            GroupBussinessEntity _grupProf = GetbyCustCDGrup(val.Trim());
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
                                Session["_isExsit"] = "true";
                                Session["_isGroup"] = "true";
                                return Json(new { success = true,group=true,login = true, data = _grupProf }, JsonRequestBehavior.AllowGet);
                            }
                            else if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == false)
                            {
                                return Json(new { success = false, login = true, msg = Resource.txtInacCus, type="Info"}, JsonRequestBehavior.AllowGet);
                            }
                            else {
                                return Json(new { success = false, login = true, msg = Resource.invalidCusCd, type = "Info" }, JsonRequestBehavior.AllowGet);
                            }


                        }
                    }
                    else
                    {
                        return Json(new { success = true, login = true}, JsonRequestBehavior.AllowGet);
                    }
                }
                else {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception ex) {
               if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult preTownTextChanged(string val) {
             string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (!string.IsNullOrEmpty(val))
                    {
                        DataTable dt = new DataTable();

                        dt = CHNLSVC.General.Get_DetBy_town(val.Trim());
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                string district = dt.Rows[0]["DISTRICT"].ToString();
                                string province = dt.Rows[0]["PROVINCE"].ToString();
                                string postalCD = dt.Rows[0]["POSTAL_CD"].ToString();
                                string countryCD = dt.Rows[0]["COUNTRY_CD"].ToString();

                                return Json(new { success = true, login = true, data = new { district = district, province = province, postalCD = postalCD, countryCD = countryCD } }, JsonRequestBehavior.AllowGet);

                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = Resource.txtInvTown,type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = Resource.txtInvTown ,type = "Info"}, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
                        return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                    }
                }
                else {
                    return Json(new { success = false, login = false}, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
               if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public MasterBusinessEntity GetbyCustCD(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(custCD, null, null, null, null, HttpContext.Session["UserCompanyCode"] as string);
        }
        public GroupBussinessEntity GetbyCustCDGrup(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(custCD, null, null, null, null, null);
        }
        public GroupBussinessEntity GetbyCustCDGrupNic(string nic) {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(null, nic, null, null, null, null);
        }
        public JsonResult DLTextChanged(string val) {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (!string.IsNullOrEmpty(val))
                    {
                        Session["_isExsit"] = "false";
                        Session["_isGroup"] = "false";
                        MasterBusinessEntity custProf = GetbyDL(val.Trim());
                        chgcdDetList = CHNLSVC.Tours.GETBUSCHARGECODES(custProf.Mbe_cd);
                        Session["chgDetList"] = chgcdDetList;
                        if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                        {
                            Session["_isExsit"] = "true";
                            return Json(new { success = true, local = true, login = true, data = custProf, chgcdDetList = chgcdDetList }, JsonRequestBehavior.AllowGet);
                        }
                        else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                        {
                            return Json(new { success = false, login = true, msg = Resource.txtInacCus, data = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            GroupBussinessEntity _grupProf = GetbyDLGrup(val.Trim());
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
                                Session["_isExsit"] = "true";
                                Session["_isGroup"] = "true";
                                return Json(new { success = true, group = true, login = true, data = _grupProf }, JsonRequestBehavior.AllowGet);
                            }
                            else if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == false)
                            {
                                return Json(new { success = false, login = true, msg = Resource.txtInacCus, data = "" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                            }
                            //if (HttpContext.Session["_isExsit"] as string == "true")
                            //{
                            //    string DL = val.Trim().ToUpper();
                            //    MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            //    return Json(new { success = true, login = true, status = 2, data = cust_null }, JsonRequestBehavior.AllowGet);

                            //}
                            ////Check the group level
                            //GroupBussinessEntity _grupProf = GetbyDLGrup(val.Trim().ToUpper());
                            //if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            //{
                            //    Session["_isExsit"] = "false";
                            //    Session["_isGroup"] = "true";
                            //    return Json(new { success = true, login = true, status = 3, data = _grupProf }, JsonRequestBehavior.AllowGet);
                            //}
                            //else
                            //{
                            //    Session["_isExsit"] = "false";
                            //    return Json(new { success = true, login = true, status = 4 }, JsonRequestBehavior.AllowGet);
                            //}
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
               if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult BRTextChanged(string val)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (!string.IsNullOrEmpty(val))
                    {
                        Session["_isExsit"] = "false";
                        Session["_isGroup"] = "false";
                        MasterBusinessEntity custProf = GetbyBrNo(val.Trim());
                        chgcdDetList = CHNLSVC.Tours.GETBUSCHARGECODES(custProf.Mbe_cd);
                        Session["chgDetList"] = chgcdDetList;
                        if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                        {
                            Session["_isExsit"] = "true";
                            return Json(new { success = true, local = true, login = true, data = custProf, chgcdDetList = chgcdDetList }, JsonRequestBehavior.AllowGet);
                        }
                        else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                        {
                            return Json(new { success = false, login = true, status = 2, msg = Resource.txtInacCus, data = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {

                            GroupBussinessEntity _grupProf = GetbyBrNoGrup(val.Trim());
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
                                Session["_isExsit"] = "true";
                                Session["_isGroup"] = "true";
                                return Json(new { success = true, login = true, group = true, data = _grupProf }, JsonRequestBehavior.AllowGet);
                            }
                            else if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == false)
                            {
                                return Json(new { success = false, login = true, msg = Resource.txtInacCus, data = "" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                            }
                            //if (HttpContext.Session["_isExsit"] as string == "true")
                            //{
                            //    string DL = val.Trim().ToUpper();
                            //    MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            //    return Json(new { success = true, login = true, status = 2, data = cust_null }, JsonRequestBehavior.AllowGet);

                            //}
                            ////Check the group level
                            //GroupBussinessEntity _grupProf = GetbyBrNoGrup(val.Trim().ToUpper());
                            //if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            //{
                            //    Session["_isGroup"] = "true";
                            //    Session["_isExsit"] = "false";
                            //    return Json(new { success = true, login = true, status = 3, data = _grupProf }, JsonRequestBehavior.AllowGet);
                            //}
                            //else
                            //{
                            //    Session["_isExsit"] = "false";
                            //    return Json(new { success = true, login = true, status = 4 }, JsonRequestBehavior.AllowGet);
                            //}

                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
                }
                catch (Exception ex) {
                    if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                    {
                        return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                    }
            }
        }
        public JsonResult MobiletextChanged(string val)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (!string.IsNullOrEmpty(val))
                    {
                        Session["_isExsit"] = "false";
                        Session["_isGroup"] = "false";
                        List<MasterBusinessEntity> custProf = CHNLSVC.Sales.GetActiveCustomerDetailList(null, company, null, val, "C");
                        chgcdDetList = CHNLSVC.Tours.GETBUSCHARGECODES(custProf.FirstOrDefault().Mbe_cd);
                        Session["chgDetList"] = chgcdDetList;
                        if (custProf.Count > 1 && custProf != null&& val != "N/A")
                        {
                            string _custMob = "Duplicate customers found ";
                            foreach (var _nicCust in custProf)
                            {
                                _custMob = _custMob + _nicCust.Mbe_cd.ToString() + " | " + _nicCust.Mbe_name.ToString() + "\n";
                            }
                            return Json(new { success = false, login = true, msg = _custMob, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }

                        if (custProf.Count>0 && custProf[0].Mbe_cd != null && custProf[0].Mbe_act == true)
                        {
                            Session["_isExsit"] = "true";
                            return Json(new { success = true, local = true, login = true, data = custProf[0], chgcdDetList = chgcdDetList }, JsonRequestBehavior.AllowGet);
                        }
                        else if (custProf.Count > 0 &&  custProf[0].Mbe_cd != null && custProf[0].Mbe_act == false)
                        {
                            return Json(new { success = false, login = true, status = 2, msg = Resource.txtInacCus, type = "Info", data = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {

                            GroupBussinessEntity _grupProf = CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, null, null, null, val);
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
                                Session["_isExsit"] = "true";
                                Session["_isGroup"] = "true";
                                return Json(new { success = true, login = true, group = true, data = _grupProf }, JsonRequestBehavior.AllowGet);
                            }
                            else if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == false)
                            {
                                return Json(new { success = false, login = true, msg = Resource.txtInacCus, data = "" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                            }
                            //if (HttpContext.Session["_isExsit"] as string == "true")
                            //{
                            //    string DL = val.Trim().ToUpper();
                            //    MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            //    return Json(new { success = true, login = true, status = 2, data = cust_null }, JsonRequestBehavior.AllowGet);

                            //}
                            ////Check the group level
                            //GroupBussinessEntity _grupProf = GetbyBrNoGrup(val.Trim().ToUpper());
                            //if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            //{
                            //    Session["_isGroup"] = "true";
                            //    Session["_isExsit"] = "false";
                            //    return Json(new { success = true, login = true, status = 3, data = _grupProf }, JsonRequestBehavior.AllowGet);
                            //}
                            //else
                            //{
                            //    Session["_isExsit"] = "false";
                            //    return Json(new { success = true, login = true, status = 4 }, JsonRequestBehavior.AllowGet);
                            //}

                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public MasterBusinessEntity GetbyDL(string dl)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, null, dl, null, null, Session["UserCompanyCode"].ToString());
        }
        public GroupBussinessEntity GetbyDLGrup(string dl)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, dl, null, null, null);
        }
        public MasterBusinessEntity GetbyBrNo(string brNo)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, null, null, null, brNo, Session["UserCompanyCode"].ToString());
        }
        public GroupBussinessEntity GetbyBrNoGrup(string brNo)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, null, null, brNo, null);
        }
        public MasterBusinessEntity GetbyNic(string nic)
        {
            return CHNLSVC.Sales.GetCustomerProfileByNIC(nic);
        }
        public JsonResult getDataCustomerFromNic(string nic){
        string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (!string.IsNullOrEmpty(nic))
                    {
                        List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(company, nic.Trim(), "", "", "", "", 1);
                        if (_custList != null && _custList.Count > 1 && nic.ToUpper() != "N/A")
                        {
                            string _custNIC = "Duplicate customers found ";
                            foreach (var _nicCust in _custList)
                            {
                                _custNIC = _custNIC + _nicCust.Mbe_cd.ToString() + " | " + _nicCust.Mbe_name.ToString() + "\n";
                            }
                            return Json(new { success = false, login = true, msg = _custNIC, type="Error" }, JsonRequestBehavior.AllowGet);
                        }
                        Session["_isExsit"] = "false";
                        Session["_isGroup"] = "false";
                        MasterBusinessEntity custProf = GetbyNic(nic.Trim());
                        if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                        {
                            Session["_isExsit"] = "true";
                            return Json(new { success = true, local = true, login = true, data = custProf }, JsonRequestBehavior.AllowGet);
                        }
                        else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                        {
                            return Json(new { success = false, login = true, msg = Resource.txtInacCus, data = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            GroupBussinessEntity _grupProf = GetbyCustCDGrupNic(nic.Trim());
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
                                Session["_isExsit"] = "true";
                                Session["_isGroup"] = "true";
                                return Json(new { success = true, group = true, login = true, data = _grupProf }, JsonRequestBehavior.AllowGet);
                            }
                            else if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == false)
                            {
                                return Json(new { success = false, login = true, msg = Resource.txtInacCus, data = "" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                            }

                        }
                    }
                    else
                    {
                        return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult dobGeneration(string nic)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (nic != "")
                    {
                        String nic_ = nic.Trim();
                        char[] nicarray = nic_.ToCharArray();
                        string thirdNum = (nicarray[2]).ToString();

                        //---------DOB generation----------------------
                        string threechar = (nicarray[2]).ToString() + (nicarray[3]).ToString() + (nicarray[4]).ToString();
                        Int32 DPBnum = Convert.ToInt32(threechar);
                        if (DPBnum > 500)
                        {
                            DPBnum = DPBnum - 500;
                        }

                        // Int32 JAN = 1, FEF = 2, MAR = 3, APR = 4, MAY = 5, JUN = 6, JUL = 7, AUG = 8, SEP = 9, OCT = 10, NOV = 11, DEC = 12;
                        Dictionary<string, Int32> monthDict = new Dictionary<string, int>();
                        monthDict.Add("JAN", 31);
                        monthDict.Add("FEF", 29);
                        monthDict.Add("MAR", 31);
                        monthDict.Add("APR", 30);
                        monthDict.Add("MAY", 31);
                        monthDict.Add("JUN", 30);
                        monthDict.Add("JUL", 31);
                        monthDict.Add("AUG", 31);
                        monthDict.Add("SEP", 30);
                        monthDict.Add("OCT", 31);
                        monthDict.Add("NOV", 30);
                        monthDict.Add("DEC", 31);

                        string bornMonth = string.Empty;
                        Int32 bornDate = 0;

                        Int32 leftval = DPBnum;
                        foreach (var itm in monthDict)
                        {
                            bornDate = leftval;

                            if (leftval <= itm.Value)
                            {
                                bornMonth = itm.Key;

                                break;
                            }
                            leftval = leftval - itm.Value;
                        }

                        Dictionary<string, Int32> monthDict2 = new Dictionary<string, int>();
                        monthDict2.Add("JAN", 1);
                        monthDict2.Add("FEF", 2);
                        monthDict2.Add("MAR", 3);
                        monthDict2.Add("APR", 4);
                        monthDict2.Add("MAY", 5);
                        monthDict2.Add("JUN", 6);
                        monthDict2.Add("JUL", 7);
                        monthDict2.Add("AUG", 8);
                        monthDict2.Add("SEP", 9);
                        monthDict2.Add("OCT", 10);
                        monthDict2.Add("NOV", 11);
                        monthDict2.Add("DEC", 12);
                        Int32 dobMon = 0;
                        foreach (var itm in monthDict2)
                        {
                            if (itm.Key == bornMonth)
                            {
                                dobMon = itm.Value;
                            }
                        }
                        Int32 dobYear = 1900 + Convert.ToInt32((nicarray[0].ToString())) * 10 + Convert.ToInt32((nicarray[1].ToString()));

                        DateTime dob = new DateTime(dobYear, dobMon, bornDate);
                        if (dob.Date.ToString("dd-MMM-yyyy") == "")
                            return Json(new { success = true, login = true, dob = String.Empty }, JsonRequestBehavior.AllowGet);
                        else
                            return Json(new { success = true, login = true, dob = dob.Date.ToString("dd/MMM/yyyy") }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = true, login = true, dob = "" }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult validateCountry(string country)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_COUNTRY> countryList = CHNLSVC.Tours.getCountryDetails(country);
                    if (countryList.Count > 0)
                    {
                        return Json(new { success = true, login = true, countryList = countryList[0] }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid country code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult addChargeCodes(string cusCd, string chgtype, string chgcd)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    cusCd = cusCd.Trim();
                    if (cusCd != "" && chgtype != "")
                    {

                        if (Session["chgDetList"] != null)
                        {
                            chgcdDetList = (List<Cus_chg_cds>)Session["chgDetList"];
                        }
                        else
                        {
                            chgcdDetList = new List<Cus_chg_cds>();
                        }

                        var count = chgcdDetList.Where(a => a.bcd_chg_cd == chgcd).Count();
                        if(count>0)
                        {
                            return Json(new { success = false, login = true, msg = "Already aded this charge code", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        Cus_chg_cds ob = new Cus_chg_cds();
                        ob.bcd_cus_cd = cusCd;
                        ob.bcd_chg_type = chgtype;
                        ob.bcd_chg_cd = chgcd;
                        chgcdDetList.Add(ob);
                        Session["chgDetList"] = chgcdDetList;
                        return Json(new { success = true, login = true, chgcdDetList = chgcdDetList, balance = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid customer details.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult RemoveChargeCodes( string chgcd)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    

                        if (Session["chgDetList"] != null)
                        {
                            chgcdDetList = (List<Cus_chg_cds>)Session["chgDetList"];
                            var count = chgcdDetList.Single(a => a.bcd_chg_cd == chgcd);
                            chgcdDetList.Remove(count);
                        }
                        else
                        {
                            chgcdDetList = new List<Cus_chg_cds>();
                        }

                        
                       

                        Session["chgDetList"] = chgcdDetList;
                        return Json(new { success = true, login = true, chgcdDetList = chgcdDetList, balance = "" }, JsonRequestBehavior.AllowGet);
                   
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult SaveBusChg()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string error = "";
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    chgcdDetList = Session["chgDetList"] as List<Cus_chg_cds>;
                    int effect = CHNLSVC.Tours.SaveBusChargeCode(chgcdDetList,userId,out error);
                    return Json(new { success = true, login = true, chgcdDetList = "", balance = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }

            }catch(Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
    public class laguage
    {
        public string MLA_CD { get; set; }
        public string MLA_DESC { get; set; }
    }
}