using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FF.BusinessObjects;
using FF.BusinessObjects.Genaral;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using FF.BusinessObjects.Security;
using FF.BusinessObjects.Search;

namespace FastForward.Logistic.Controllers
{
    public class CustomerDetailsController : BaseController
    {
        // GET: BusinessEntity
        public ActionResult Index(string returnUrl)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                SEC_SYSTEM_MENU per = CHNLSVC.Security.getUserPermission(userId,company,13);
                 if (per.SSM_ID != 0)
                 {
                // ViewBag.ReturnUrl = returnUrl;
                return View();
                 }
                 else
                 {
                     return Redirect("/Home/Error");
                 }
            }
            else
            {
                return Redirect("~/Home/index");
            }

        }

        /// <summary>
        /// Isuru 2017/05/26
        /// </summary>
        /// <param name="pgeNum"></param>
        /// <param name="pgeSize"></param>
        /// <param name="searchFld"></param>
        /// <param name="searchVal"></param>
        /// <returns></returns>

        public JsonResult loadcustomercoun(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            try
            {
                string userId = HttpContext.Session["UserId"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;


                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<custype> olist = new List<custype>();

                    List<MST_COUNTRY> custype = CHNLSVC.General.getCustomerCountry();

                    if (custype.Count > 0)
                    {
                        foreach (MST_COUNTRY typ in custype)
                        {
                            custype ob1 = new custype();
                            ob1.value = typ.MCU_CD;
                            ob1.text = typ.MCU_DESC;

                            olist.Add(ob1);

                        }
                    }
                    return Json(new { success = true, Login = true, Data = olist }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Isuru 2017/05/26
        /// </summary>
        /// <returns></returns>
        public JsonResult loadentitymodes()
        {
            try
            {
                string userId = HttpContext.Session["UserId"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefloca"] as string;


                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<custype> entitymode = new List<custype>();
                    custype ob1 = new custype();
                    ob1.text = "Internal";
                    ob1.value = "Internal";
                    entitymode.Add(ob1);

                    custype ob2 = new custype();
                    ob2.text = "External";
                    ob2.value = "External";
                    entitymode.Add(ob2);


                    return Json(new { success = true, Login = true, data = entitymode }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
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

        public static bool IsValidMobileOrLandNo(string mobile)
        {
            string pattern = @"^[0-9]{10}$";

            System.Text.RegularExpressions.Match match = Regex.Match(mobile.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }
        public static bool IsValidNumber(string mobile)
        {
            string pattern = @"^[0-9]*$";

            System.Text.RegularExpressions.Match match = Regex.Match(mobile.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }
      

        /// <summary>
        /// Isuru 2017/05/26
        /// </summary>
        /// <param name="pgeNum"></param>
        /// <param name="pgeSize"></param>
        /// <param name="searchFld"></param>
        /// <param name="searchVal"></param>
        /// <returns></returns>
        public JsonResult loadcustomerbrno(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            try
            {
                string userId = HttpContext.Session["UserId"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;


                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<cus_details> custype = CHNLSVC.General.getCustomerBRNo(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (custype != null)
                    {
                        decimal totcustype = Math.Ceiling(Convert.ToInt32(custype[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = custype, totcustype = totcustype });
                    }
                    return Json(new { success = false, login = true, msg = "No data found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// ISuru 2017/05/27
        /// </summary>
        /// <param name="pgeNum"></param>
        /// <param name="pgeSize"></param>
        /// <param name="searchFld"></param>
        /// <param name="searchVal"></param>
        /// <returns></returns>
        public JsonResult loadcustomerpasspno(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            try
            {
                string userId = HttpContext.Session["UserId"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;


                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<cus_details> custype = CHNLSVC.General.getCustomerPassPNo(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (custype != null)
                    {
                        decimal totpasspno = Math.Ceiling(Convert.ToInt32(custype[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = custype, totpasspno = totpasspno });
                    }
                    return Json(new { success = false, login = true, msg = "No data found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Isuru 2017/05/27
        /// </summary>
        /// <param name="pgeNum"></param>
        /// <param name="pgeSize"></param>
        /// <param name="searchFld"></param>
        /// <param name="searchVal"></param>
        /// <returns></returns>
        public JsonResult loadcustomerdlno(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            try
            {
                string userId = HttpContext.Session["UserId"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;


                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<cus_details> custype = CHNLSVC.General.getCustomerDLNo(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (custype != null)
                    {
                        decimal totdlno = Math.Ceiling(Convert.ToInt32(custype[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = custype, totdlno = totdlno });
                    }
                    return Json(new { success = false, login = true, msg = "No data found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 2017/05/27
        /// </summary>
        /// <param name="pgeNum"></param>
        /// <param name="pgeSize"></param>
        /// <param name="searchFld"></param>
        /// <param name="searchVal"></param>
        /// <returns></returns>
        public JsonResult loadcustomertelno(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            try
            {
                string userId = HttpContext.Session["UserId"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;


                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<cus_details> custype = CHNLSVC.General.getCustomerTelNo(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (custype != null)
                    {
                        decimal tottelno = Math.Ceiling(Convert.ToInt32(custype[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = custype, tottelno = tottelno });
                    }
                    return Json(new { success = false, login = true, msg = "No data found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult CreateCustomer(cus_details cust, string _IS_TAX, string _ACT, string _IS_SVAT, string _TAX_EX, string _IS_SUSPEND, string _AGRE_SEND_SMS, string _AGRE_SEND_EMAIL)
         {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if(_IS_TAX=="undefined")
                {
                    _IS_TAX = "false";
                }
                if(_ACT=="undefined")
                {
                    _ACT = "false";
                }
                if(_IS_SVAT=="undefined")
                {
                    _IS_SVAT = "false";
                }
                if(_TAX_EX=="undefined")
                {
                    _TAX_EX = "false";
                }
                if (_IS_SUSPEND == "undefined")
                {
                    _IS_SUSPEND = "false";
                }
                if(_AGRE_SEND_SMS=="undefined")
                {
                    _AGRE_SEND_SMS = "false";

                }
                if (_AGRE_SEND_EMAIL == "undefined")
                {
                    _AGRE_SEND_EMAIL = "false";

                }
                //cust.MBE_IS_TAX = Convert.ToBoolean(_IS_TAX);
                //cust.MBE_ACT = Convert.ToBoolean(_ACT);
                //cust.MBE_IS_SVAT = Convert.ToBoolean(_IS_SVAT);
                //cust.MBE_TAX_EX = Convert.ToBoolean(_TAX_EX);
                //cust.MBE_IS_SUSPEND = Convert.ToBoolean(_IS_SUSPEND);
                //cust.MBE_AGRE_SEND_SMS = Convert.ToBoolean(_AGRE_SEND_SMS);
                //cust.MBE_AGRE_SEND_EMAIL = Convert.ToBoolean(_AGRE_SEND_EMAIL);

                //Dictionary<string, string[]> tableValues = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(tableData);
                //Dictionary<string, string[]> allValues = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(alldata);
                //Dictionary<string, string[]> otherValues = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(otherdata);
                //cus_details cust = new cus_details();
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string err = "0";

                    if (string.IsNullOrEmpty(cust.MBE_CD))
                    {
                        //return Json(new { success = false, login = true, msg = "Please Enter Customer Code", type = "Info" }, JsonRequestBehavior.AllowGet);
                        //}
                        if (string.IsNullOrEmpty(cust.MBE_TP))
                        {
                            return Json(new { success = false, login = true, msg = "Please Enter Customer Type", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (string.IsNullOrEmpty(cust.MBE_BR_NO) && string.IsNullOrEmpty(cust.MBE_OTH_ID_NO) && string.IsNullOrEmpty(cust.MBE_NIC) && string.IsNullOrEmpty(cust.MBE_PP_NO) && string.IsNullOrEmpty(cust.MBE_DL_NO))
                        {
                            return Json(new { success = false, login = true, msg = "Please Enter BR Code,Tin Code,NIC,PP No or Driving Licence No", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (cust.MBE_AGRE_SEND_EMAIL == true)
                        {
                            if (string.IsNullOrEmpty(cust.MBE_EMAIL))
                            {
                                return Json(new { success = false, login = true, msg = "Please Enter Email", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (string.IsNullOrEmpty(cust.MBE_NAME))
                        {
                            return Json(new { success = false, login = true, msg = "Please Enter Name", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (cust.MBE_IS_TAX == true)
                        {
                            if (string.IsNullOrEmpty(cust.MBE_TAX_NO))
                            {
                                return Json(new { success = false, login = true, msg = "Please Enter Tax No", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }

                        if (cust.MBE_AGRE_SEND_SMS == true)
                        {
                            if (string.IsNullOrEmpty(cust.MBE_MOB))
                            {
                                return Json(new { success = false, login = true, msg = "Please Enter Mobile No", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }

                        if (!string.IsNullOrEmpty(cust.MBE_EMAIL))
                        {
                            if (!IsValidEmail(cust.MBE_EMAIL))
                            {
                                return Json(new { success = false, login = true, msg = "Please Enter Valid Email", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (!string.IsNullOrEmpty(cust.MBE_TEL))
                        {
                            Boolean _isvalid = IsValidMobileOrLandNo(cust.MBE_TEL.Trim());

                            if (_isvalid == false)
                            {
                                return Json(new { success = false, login = true, msg = "Please Enter Valid Telephone No", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }

                        }
                        if (!string.IsNullOrEmpty(cust.MBE_CREDIT_DAYS))
                        {
                            Boolean _isvalid = IsValidNumber(cust.MBE_CREDIT_DAYS.Trim());

                            if (_isvalid == false)
                            {
                                return Json(new { success = false, login = true, msg = "Please Enter Valid Credit limit", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }

                        }
                        //if (string.IsNullOrEmpty(cust.MBE_OTH_ID_NO))
                        //{
                        //    return Json(new { success = false, login = true, msg = "Please Enter Tin No", type = "Info" }, JsonRequestBehavior.AllowGet);
                        //}
                        //if (cust.MBE_NIC != null)
                        //{
                        //    List<cus_details> _custList = CHNLSVC.Sales.CustomerSearchAll(company, cust.MBE_NIC.Trim(), "", "", "", "", 1);
                        //    if (_custList != null && _custList.Count > 1 && cust.MBE_NIC.ToUpper() != "N/A")
                        //    {
                        //        string _custNIC = "Duplicate customers found ";
                        //        foreach (var _nicCust in _custList)
                        //        {
                        //            _custNIC = _custNIC + _nicCust.MBE_CD.ToString() + " | " + _nicCust.MBE_NAME.ToString() + "\n";
                        //        }
                        //        return Json(new { success = false, login = true, msg = _custNIC, type = "Error" }, JsonRequestBehavior.AllowGet);
                        //    }
                        //}
                        //if (string.IsNullOrEmpty(cust.MBE_NIC))
                        //{
                        //    return Json(new { success = false, login = true, msg = "Please Enter NIC No", type = "Info" }, JsonRequestBehavior.AllowGet);
                        //}
                        //if (string.IsNullOrEmpty(cust.MBE_PP_NO))
                        //{
                        //    return Json(new { success = false, login = true, msg = "Please Enter Passport No", type = "Info" }, JsonRequestBehavior.AllowGet);
                        //}

                        cust.MBE_COM = company;
                        cust.MBE_CRE_BY = Convert.ToString(Session["userID"]);
                        cust.MBE_CRE_DT = DateTime.Now;

                        string checksave = null;

                        //CHNLSVC.Sales.SaveCustomerDetails(cust, userDefPro, userId, company, userDefLoc, out err);

                        checksave = CHNLSVC.Sales.SaveCustomerDetails(cust, _IS_TAX, _ACT, _IS_SVAT, _TAX_EX, _IS_SUSPEND, _AGRE_SEND_SMS, _AGRE_SEND_EMAIL, userDefPro, userId, company, userDefLoc, out err);
                        //string x=cust.MBE_CD;
                        if (checksave != null)
                        {
                            return Json(new { success = true, login = true, msg = "Saved Successfully - " + checksave }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = err, type = "Error" }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Please use update option to update a record", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult loadcustomerexecutive(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            try
            {
                string userId = HttpContext.Session["UserId"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;


                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<cus_details> custype = CHNLSVC.CommonSearch.getCustomerExecutive(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (custype != null)
                    {
                        decimal totexecu = Math.Ceiling(Convert.ToInt32(custype[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = custype, tottelno = totexecu });
                    }
                    return Json(new { success = false, login = true, msg = "No data found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult loadcustomernicno(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            try
            {
                string userId = HttpContext.Session["UserId"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;


                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<cus_details> custype = CHNLSVC.General.getcustomernicno(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (custype != null)
                    {
                        decimal totexecu = Math.Ceiling(Convert.ToInt32(custype[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = custype, tottelno = totexecu });
                    }
                    return Json(new { success = false, login = true, msg = "No data found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Isuru 2017/05/31
        /// </summary>
        /// <param name="pgeNum"></param>
        /// <param name="pgeSize"></param>
        /// <param name="searchFld"></param>
        /// <param name="searchVal"></param>
        /// <returns></returns>
        public JsonResult loadcustomertype(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            try
            {
                string userId = HttpContext.Session["UserId"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;


                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<mst_bus_entity_tp> custype = CHNLSVC.General.getcustomertype(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (custype != null)
                    {
                        decimal totexecu = Math.Ceiling(Convert.ToInt32(custype[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = custype, tottelno = totexecu });
                    }
                    return Json(new { success = false, login = true, msg = "No data found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult LoadCustormerDetails(string CustormerCode)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<cus_details> P_list = CHNLSVC.General.GetCustormerdata(company, CustormerCode);
                    if (P_list == null)
                    {
                        P_list = new List<cus_details>();
                    }
                    return Json(new { success = true, login = true, data = P_list }, JsonRequestBehavior.AllowGet);

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

        public JsonResult LoadCountryTown(string CountryCode)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<cus_details> P_list = CHNLSVC.General.GetCountryTown(company, CountryCode);
                    if (P_list == null)
                    {
                        P_list = new List<cus_details>();
                    }
                    return Json(new { success = true, login = true, data = P_list }, JsonRequestBehavior.AllowGet);

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

        public JsonResult preTownTextChanged(string val)
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
                                return Json(new { success = false, login = true, msg = "Please enter valid Town ", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Error", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult AddCustomer(string CustormerCode, string CustomerType, string Exec)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MainServices> services = Session["Service_det"] as List<MainServices>;
                    if (services != null)
                    {
                        if (services.Count == 0)
                        {
                            //Please Select service
                            return Json(new { success = false, login = true, notice = true, msg = "Please Select Services" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        //Please Select service
                        return Json(new { success = false, login = true, notice = true, msg = "Please Select Services" }, JsonRequestBehavior.AllowGet);
                    }

                    List<trn_jb_cus_det> cus_det = Session["trn_jb_cus_det"] as List<trn_jb_cus_det>;
                    List<trn_job_serdet> job_det = Session["trn_job_serdet"] as List<trn_job_serdet>;
                    if (cus_det != null)
                    {

                        trn_jb_cus_det ob = new trn_jb_cus_det();
                        ob.Jc_cus_cd = CustormerCode;
                        ob.Jc_cus_tp = CustomerType;
                        ob.Jc_exe_cd = Exec;
                        ob.Jc_cre_by = userId;
                        ob.Jc_cre_dt = DateTime.Now;
                        ob.Jc_mod_by = userId;
                        ob.Jc_mod_dt = DateTime.Now;
                        cus_det.Add(ob);
                        Session["trn_jb_cus_det"] = cus_det;

                        foreach (var _ser in services)
                        {

                            if (job_det != null)
                            {
                                var samecus_ser = 0;
                                if (job_det.Count > 0)
                                {
                                    samecus_ser = job_det.Where(a => a.JS_CUS_CD == CustormerCode && a.JS_SER_TP == _ser.fms_ser_cd).Count();
                                    if (samecus_ser > 0)
                                    {
                                        return Json(new { success = false, login = true, notice = true, msg = "Service details has been already added for customer" }, JsonRequestBehavior.AllowGet);
                                    }

                                    int maxline = job_det.Max(a => a.JS_LINE_NO);
                                    trn_job_serdet obj = new trn_job_serdet();
                                    obj.JS_CRE_BY = userId;
                                    obj.JS_CRE_DT = DateTime.Now;
                                    obj.JS_CUS_CD = CustormerCode;
                                    obj.JS_LINE_NO = maxline + 1;
                                    obj.JS_MOD_BY = userId;
                                    obj.JS_MOD_DT = DateTime.Now;
                                    obj.JS_PC = userDefPro;
                                    obj.JS_RMK = "";
                                    obj.JS_SER_TP = _ser.fms_ser_cd;
                                    job_det.Add(obj);
                                    Session["trn_job_serdet"] = job_det;
                                }
                                else
                                {

                                    job_det = new List<trn_job_serdet>();
                                    trn_job_serdet obj = new trn_job_serdet();
                                    obj.JS_CRE_BY = userId;
                                    obj.JS_CRE_DT = DateTime.Now;
                                    obj.JS_CUS_CD = CustormerCode;
                                    obj.JS_LINE_NO = 1;
                                    obj.JS_MOD_BY = userId;
                                    obj.JS_MOD_DT = DateTime.Now;
                                    obj.JS_PC = userDefPro;
                                    obj.JS_RMK = "";
                                    obj.JS_SER_TP = _ser.fms_ser_cd;
                                    job_det.Add(obj);
                                    Session["trn_job_serdet"] = job_det;
                                }


                            }
                            else
                            {
                                job_det = new List<trn_job_serdet>();
                                trn_job_serdet obj = new trn_job_serdet();
                                obj.JS_CRE_BY = userId;
                                obj.JS_CRE_DT = DateTime.Now;
                                obj.JS_CUS_CD = CustormerCode;
                                obj.JS_LINE_NO = 1;
                                obj.JS_MOD_BY = userId;
                                obj.JS_MOD_DT = DateTime.Now;
                                obj.JS_PC = userDefPro;
                                obj.JS_RMK = "";
                                obj.JS_SER_TP = _ser.fms_ser_cd;
                                job_det.Add(obj);
                                Session["trn_job_serdet"] = job_det;
                            }


                        }
                    }
                    else
                    {
                        cus_det = new List<trn_jb_cus_det>();
                        trn_jb_cus_det ob = new trn_jb_cus_det();
                        ob.Jc_cus_cd = CustormerCode;
                        ob.Jc_cus_tp = CustomerType;
                        ob.Jc_exe_cd = Exec;
                        ob.Jc_cre_by = userId;
                        ob.Jc_cre_dt = DateTime.Now;
                        ob.Jc_mod_by = userId;
                        ob.Jc_mod_dt = DateTime.Now;
                        cus_det.Add(ob);
                        Session["trn_jb_cus_det"] = cus_det;

                        foreach (var _ser in services)
                        {

                            if (job_det != null)
                            {
                                int maxline = job_det.Max(a => a.JS_LINE_NO);
                                trn_job_serdet obj = new trn_job_serdet();
                                obj.JS_CRE_BY = userId;
                                obj.JS_CRE_DT = DateTime.Now;
                                obj.JS_CUS_CD = CustormerCode;
                                obj.JS_LINE_NO = maxline + 1;
                                obj.JS_MOD_BY = userId;
                                obj.JS_MOD_DT = DateTime.Now;
                                obj.JS_PC = userDefPro;
                                obj.JS_RMK = "";
                                obj.JS_SER_TP = _ser.fms_ser_cd;
                                job_det.Add(obj);
                                Session["trn_job_serdet"] = job_det;
                            }
                            else
                            {
                                job_det = new List<trn_job_serdet>();
                                trn_job_serdet obj = new trn_job_serdet();
                                obj.JS_CRE_BY = userId;
                                obj.JS_CRE_DT = DateTime.Now;
                                obj.JS_CUS_CD = CustormerCode;
                                obj.JS_LINE_NO = 1;
                                obj.JS_MOD_BY = userId;
                                obj.JS_MOD_DT = DateTime.Now;
                                obj.JS_PC = userDefPro;
                                obj.JS_RMK = "";
                                obj.JS_SER_TP = _ser.fms_ser_cd;
                                job_det.Add(obj);
                                Session["trn_job_serdet"] = job_det;
                            }


                        }

                    }

                    return Json(new { success = true, login = true, data = job_det }, JsonRequestBehavior.AllowGet);

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

        public JsonResult UpdateCustomer(cus_details cust, string _IS_TAX, string _ACT, string _IS_SVAT, string _TAX_EX, string _IS_SUSPEND, string _AGRE_SEND_SMS, string _AGRE_SEND_EMAIL)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                //cust.MBE_IS_TAX=Convert.ToBoolean(_IS_TAX);
                //cust.MBE_ACT=Convert.ToBoolean(_ACT);
                //cust.MBE_IS_SVAT=Convert.ToBoolean(_IS_SVAT);
                //cust.MBE_TAX_EX=Convert.ToBoolean(_TAX_EX);
                //cust.MBE_IS_SUSPEND=Convert.ToBoolean(_IS_SUSPEND);
                //cust.MBE_AGRE_SEND_SMS=Convert.ToBoolean(_AGRE_SEND_SMS);
                //cust.MBE_AGRE_SEND_EMAIL=Convert.ToBoolean(_AGRE_SEND_EMAIL);

                //Dictionary<string, string[]> tableValues = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(tableData);
                //Dictionary<string, string[]> allValues = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(alldata);
                //Dictionary<string, string[]> otherValues = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(otherdata);
                //cus_details cust = new cus_details();
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string err = "0";

                    if (string.IsNullOrEmpty(cust.MBE_CD))
                    {
                        return Json(new { success = false, login = true, msg = "Please Enter Customer Code", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    //if (string.IsNullOrEmpty(cust.MBE_DL_NO))
                    //{
                    //    return Json(new { success = false, login = true, msg = "Please Enter Customer Code", type = "Info" }, JsonRequestBehavior.AllowGet);
                    //}
                    if (string.IsNullOrEmpty(cust.MBE_TP))
                    {
                        return Json(new { success = false, login = true, msg = "Please Enter Customer Type", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(cust.MBE_BR_NO) && string.IsNullOrEmpty(cust.MBE_OTH_ID_NO) && string.IsNullOrEmpty(cust.MBE_NIC) && string.IsNullOrEmpty(cust.MBE_PP_NO) && string.IsNullOrEmpty(cust.MBE_DL_NO))
                    {
                        return Json(new { success = false, login = true, msg = "Please Enter BR Code,Tin Code,NIC,PP No or Driving Licence No", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (cust.MBE_AGRE_SEND_EMAIL == true)
                    {
                        if (string.IsNullOrEmpty(cust.MBE_EMAIL))
                        {
                            return Json(new { success = false, login = true, msg = "Please Enter Email", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (string.IsNullOrEmpty(cust.MBE_NAME))
                    {
                        return Json(new { success = false, login = true, msg = "Please Enter Name", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (cust.MBE_IS_TAX == true)
                    {
                        if (string.IsNullOrEmpty(cust.MBE_TAX_NO))
                        {
                            return Json(new { success = false, login = true, msg = "Please Enter Tax No", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (cust.MBE_AGRE_SEND_SMS == true)
                    {
                        if (string.IsNullOrEmpty(cust.MBE_MOB))
                        {
                            return Json(new { success = false, login = true, msg = "Please Enter Mobile No", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (!string.IsNullOrEmpty(cust.MBE_EMAIL))
                    {
                        if (!IsValidEmail(cust.MBE_EMAIL))
                        {
                            return Json(new { success = false, login = true, msg = "Please Enter Valid Email", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (!string.IsNullOrEmpty(cust.MBE_TEL))
                    {
                        Boolean _isvalid = IsValidMobileOrLandNo(cust.MBE_TEL.Trim());

                        if (_isvalid == false)
                        {
                            return Json(new { success = false, login = true, msg = "Please Enter Valid Telephone No", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    if (!string.IsNullOrEmpty(cust.MBE_CREDIT_DAYS))
                    {
                        Boolean _isvalid = IsValidNumber(cust.MBE_CREDIT_DAYS.Trim());

                        if (_isvalid == false)
                        {
                            return Json(new { success = false, login = true, msg = "Please Enter Valid Credit limit", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    //if (string.IsNullOrEmpty(cust.MBE_OTH_ID_NO))
                    //{
                    //    return Json(new { success = false, login = true, msg = "Please Enter Tin No", type = "Info" }, JsonRequestBehavior.AllowGet);
                    //}
                    //if (cust.MBE_NIC != null)
                    //{
                    //    List<cus_details> _custList = CHNLSVC.Sales.CustomerSearchAll(company, cust.MBE_NIC.Trim(), "", "", "", "", 1);
                    //    if (_custList != null && _custList.Count > 1 && cust.MBE_NIC.ToUpper() != "N/A")
                    //    {
                    //        string _custNIC = "Duplicate customers found ";
                    //        foreach (var _nicCust in _custList)
                    //        {
                    //            _custNIC = _custNIC + _nicCust.MBE_CD.ToString() + " | " + _nicCust.MBE_NAME.ToString() + "\n";
                    //        }
                    //        return Json(new { success = false, login = true, msg = _custNIC, type = "Error" }, JsonRequestBehavior.AllowGet);
                    //    }
                    //}
                    //if (string.IsNullOrEmpty(cust.MBE_NIC))
                    //{
                    //    return Json(new { success = false, login = true, msg = "Please Enter NIC No", type = "Info" }, JsonRequestBehavior.AllowGet);
                    //}
                    //if (string.IsNullOrEmpty(cust.MBE_PP_NO))
                    //{
                    //    return Json(new { success = false, login = true, msg = "Please Enter Passport No", type = "Info" }, JsonRequestBehavior.AllowGet);
                    //}

                    cust.MBE_COM = company;
                    cust.MBE_CRE_BY = Convert.ToString(Session["userID"]);
                    cust.MBE_CRE_DT = DateTime.Now;

                    int checksave = 0;

                    //CHNLSVC.Sales.UpdateCustomerDetails(cust, userDefPro, userId, company, userDefLoc, out err);

                    checksave = CHNLSVC.Sales.UpdateCustomerDetails(cust, _IS_TAX, _ACT, _IS_SVAT, _TAX_EX, _IS_SUSPEND, _AGRE_SEND_SMS, _AGRE_SEND_EMAIL, userDefPro, userId, company, userDefLoc, out err);
                    
                    if (checksave == 1)
                    {
                        return Json(new { success = true, login = true, msg = "Successfully Updated" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Please use SAVE option...", type = "Error" }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }

    
    }
}