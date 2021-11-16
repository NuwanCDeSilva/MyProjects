using FF.BusinessObjects;
using FF.BusinessObjects.Genaral;
using FF.BusinessObjects.Sales;
using FF.BusinessObjects.Search;
using FF.BusinessObjects.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastForward.Logistic.Controllers
{
    public class ValidationController : BaseController
    {
        // GET: Validation
        public JsonResult validateProfitCenter(string pccd)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                MST_PROFIT_CENTER pc = new MST_PROFIT_CENTER();
                if (pccd != "")
                {
                    pccd = pccd.Trim();
                    pc = CHNLSVC.Sales.getProfitCenterDetails(pccd, company, userId);
                }
                return Json(new { success = true, login = true, data = pc }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult validateEmployeeByCode(string empCd)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                MST_EMP EMP = new MST_EMP();
                if (empCd != "")
                {
                    empCd = empCd.Trim();
                    EMP = CHNLSVC.Sales.getEmployeeDetails(empCd, company);
                }
                return Json(new { success = true, login = true, data = EMP }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult validateConsigneeAccountCode(string cuscd, string type)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                MST_BUSENTITY cus = new MST_BUSENTITY();
                if (cuscd != "")
                {
                    cuscd = cuscd.Trim();
                    cus = CHNLSVC.Sales.getConsigneeDetailsByAccCode(cuscd, company, type);
                }
                return Json(new { success = true, login = true, data = cus }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult validateJobNumber(string jobNum)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                trn_jb_hdr job = new trn_jb_hdr();
                string error = "";
                if (jobNum != "")
                {
                    jobNum = jobNum.Trim();
                    job = CHNLSVC.Sales.GetJobDetails(jobNum, company, out error);

                }
                if (error == "")
                {
                    return Json(new { success = true, login = true, data = job }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        // List<trn_jb_hdr> _hdr = CHNLSVC.General.GetJobHdrbypouch(pouch);
        public JsonResult validatePouchNumber(string pouchno)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                List<trn_jb_hdr> _hdr = new List<trn_jb_hdr>();
                trn_jb_hdr job = new trn_jb_hdr();
                string error = "";
                if (pouchno != "")
                {
                    pouchno = pouchno.Trim();
                    _hdr = CHNLSVC.General.GetJobHdrbypouch(pouchno);
                    if (_hdr == null)
                    {
                        _hdr = new List<trn_jb_hdr>();
                    }
                    if (_hdr.Count > 0)
                    {
                        job = _hdr[0];
                    }


                }
                if (error == "")
                {
                    return Json(new { success = true, login = true, data = job }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult validateHouseBLNumber(string HBLno)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                string BLno = "";
                List<BL_DOC_NO> doclist = CHNLSVC.General.GetBLDocNo("HOUSE", "bl_h_doc_no", HBLno);
                if (doclist != null)
                {
                    if (doclist.Count > 0)
                    {
                        BLno = doclist.First().bl_doc_no.ToString();
                    }
                }
                int cont = 0;
                List<trn_bl_header> _hdr = CHNLSVC.General.GetBLHdr(BLno, company);
                if (_hdr != null)
                {
                    cont = _hdr.Count;
                }
                return Json(new { success = true, login = true, Count = cont }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult validateCostElement(string eleCode)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                MST_COST_ELEMENT job = new MST_COST_ELEMENT();
                string error = "";
                if (eleCode != "")
                {
                    eleCode = eleCode.Trim();
                    job = CHNLSVC.Sales.GetCostElementDetails(eleCode, out error);

                }
                if (error == "")
                {
                    return Json(new { success = true, login = true, data = job }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult validateUOM(string uomcd)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                FTW_MES_TP job = new FTW_MES_TP();
                string error = "";
                if (uomcd != "")
                {
                    uomcd = uomcd.Trim();
                    job = CHNLSVC.Sales.GetUOMDetails(uomcd, out error);

                }
                if (error == "")
                {
                    return Json(new { success = true, login = true, data = job }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult validateCurrencyCode(string curcd)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                MST_CUR job = new MST_CUR();
                string error = "";
                if (curcd != "")
                {
                    curcd = curcd.Trim();
                    job = CHNLSVC.Sales.GetCurrencyDetails(curcd, out error);

                }
                if (error == "")
                {
                    return Json(new { success = true, login = true, data = job }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult validateTelVehLc(string code)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                FTW_VEHICLE_NO job = new FTW_VEHICLE_NO();
                string error = "";
                if (code != "")
                {
                    code = code.Trim();
                    job = CHNLSVC.Sales.getTelVehLcDet(code, out error);

                }
                if (error == "")
                {
                    return Json(new { success = true, login = true, data = job }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getExcahaneRate(string currency)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_CUR> _cur = CHNLSVC.General.GetAllCurrency(null);
                    MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(company, userDefPro);
                    decimal _exchangRate = 0;
                    MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(company, currency, DateTime.Now, _pc.Mpc_def_exrate, userDefPro);
                    _exchangRate = (_exc1 != null) ? _exc1.Mer_bnkbuy_rt : 0;
                    string oList = String.Format("{0:#,##0.00;($#,##0.00);0.00}", _exchangRate);
                    if (_exc1 != null)
                    {
                        _exchangRate = _exc1.Mer_bnkbuy_rt;
                        oList = String.Format("{0:#,##0.00;($#,##0.00);0.00}", _exchangRate);

                    }
                    else if (currency == "LKR")
                    {
                        _exchangRate = 1;
                        oList = String.Format("{0:#,##0.00;($#,##0.00);0.00}", _exchangRate);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "please setup exchange rates for selected currency", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                return Json(new { success = false, login = true, data = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }
        public bool validateNumberDecimal(string number)
        {
            try
            {
                decimal n;
                bool isNumeric = decimal.TryParse(number, out n);
                return isNumeric;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool validateNumberInt(string number)
        {
            try
            {
                int n;
                bool isNumeric = int.TryParse(number, out n);
                return isNumeric;

            }
            catch (Exception ex)
            {
                throw ex;
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
                    List<MST_COUNTRY> countryList = CHNLSVC.General.getCountryDetails(country);
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
                    return Json(new { success = false, login = true, msg = "Error", type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult ValidatePort(string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<SEARCH_PORT> list = CHNLSVC.CommonSearch.getPorts("1", "10", "Code", searchVal, company);

                    if (list != null)
                    {
                        if (list.Count > 0)
                        {
                            return Json(new { success = true, login = true, data = 1 }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Please Enter Valid Port" }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Please Enter Valid Port" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult ValidayeVessels(string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<SEARCH_VESSEL> list = CHNLSVC.CommonSearch.getVessels("1", "10", "Code", searchVal, company);
                    if (list != null)
                    {
                        if (list.Count > 0)
                        {
                            return Json(new { success = true, login = true, data = 1 }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Please Enter Valid Flight" }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Please Enter Valid Flight" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

    }
}