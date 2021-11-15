using FF.BusinessObjects;
using FF.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FF.BusinessObjects.Tours;
using FF.BusinessObjects.ToursNew;

namespace FastForward.WebAbansTours.Controllers
{
    public class SearchController : BaseController
    {
        // GET: Search
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                return View();
            }
            else
            {
                return Redirect("~/Login/Login");
            }
        }
        public JsonResult getCustomerDetails(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_CUS_SEARCH_HEAD> documents = CHNLSVC.ComSearch.getCustomerDetails(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (documents != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(documents[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = documents, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getTownDetails(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_TOWN_SEARCH_HEAD> documents = CHNLSVC.ComSearch.getTownDetails(pgeNum, pgeSize, searchFld, searchVal);

                    if (documents != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(documents[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = documents, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getProfitCenters(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<MST_PROFIT_CENTER_SEARCH_HEAD> data = CHNLSVC.ComSearch.getProfitCenters(pgeNum, pgeSize, searchFld, searchVal, company);
                if (data != null)
                {
                    decimal totalDoc = Math.Ceiling(Convert.ToInt32(data[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                    return Json(new { success = true, login = true, data = data, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getEmployeeDetails(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<MST_EMPLOYEE_SEARCH_HEAD> data = CHNLSVC.ComSearch.getEmployeeDetails(pgeNum, pgeSize, searchFld, searchVal, company);
                if (data != null)
                {
                    decimal totalDoc = Math.Ceiling(Convert.ToInt32(data[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                    return Json(new { success = true, login = true, data = data, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getFleetDetails(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<mst_fleet_search_head> documents = CHNLSVC.ComSearch.getFleetDetails(pgeNum, pgeSize, searchFld, searchVal);

                    if (documents != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(documents[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = documents, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getDepositedBanks(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_BANKACC_SEARCH_HEAD> banks = CHNLSVC.ComSearch.getDepositedBanks(pgeNum, pgeSize, searchFld, searchVal, company);
                    if (banks != null)
                    {
                        decimal totalBnk = Math.Ceiling(Convert.ToInt32(banks[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = banks, totalDoc = totalBnk }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult getBusComBanks(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_BUSCOM_BANK_SEARCH_HEAD> banks = CHNLSVC.ComSearch.getBusComBanks(pgeNum, pgeSize, searchFld, searchVal);
                    if (banks != null)
                    {
                        decimal totalBnk = Math.Ceiling(Convert.ToInt32(banks[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = banks, totalDoc = totalBnk }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult getServiceProviderDetails(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_SER_PROVIDER_SEARCH_HEAD> documents = CHNLSVC.ComSearch.getServiceProviderDetails(pgeNum, pgeSize, searchFld, searchVal);

                    if (documents != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(documents[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = documents, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getBankBranchs(string bankcd, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (bankcd.Trim() != "")
                    {
                        searchVal = searchVal.Trim();
                        List<MST_BUSCOM_BANKBRANCH_SEARCH_HEAD> branchs = CHNLSVC.ComSearch.getBoscomBankBranchs(bankcd, pgeNum, pgeSize, searchFld, searchVal);

                        if (branchs != null)
                        {
                            decimal totalDoc = Math.Ceiling(Convert.ToInt32(branchs[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                            return Json(new { success = true, login = true, data = branchs, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Select bank first.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getAdvanceRerference(string cusCd, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_ADVAN_REF_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getAdvanceRerference(company, cusCd, pgeNum, pgeSize, searchFld, searchVal);
                    if (advref != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getCredNoteRerference(string customer, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_CREDIT_NOTE_REF_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getCredNoteRerference(customer, company, userDefPro, pgeNum, pgeSize, searchFld, searchVal);
                    if (advref != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        public JsonResult getGiftVoucherSearch(string customer, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_GIFTVOUCHER_SEARCH_HEAD> advref = CHNLSVC.Inventory.getGiftVoucherSearch(company, 0, pgeNum, pgeSize, searchFld, searchVal);
                    if (advref != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }


        }
        public JsonResult getLoyaltyCard(string customer, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_LOYALTYCARD_SEARCH_HEAD> loydet = CHNLSVC.ComSearch.getLoyaltyCard(customer, pgeNum, pgeSize, searchFld, searchVal);
                    if (loydet != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(loydet[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = loydet, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult geTtransportEnqiurySearch(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_TRANSPORT_ENQUIRY_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getTransportEnqiurySearch(company, userDefPro, pgeNum, pgeSize, searchFld, searchVal);
                    if (advref != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult getChargCodeList(string service,string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string category = "TRANS";
                    searchVal = searchVal.Trim();
                    List<MST_CHARG_CODE_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getChargCodeList(company, category, pgeNum, pgeSize, searchFld, searchVal,userDefPro);
                    if (advref != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult getChargCodeList2(string service,string option, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (option=="1")
                    {
                        string category = "TRANS";
                        searchVal = searchVal.Trim();
                        MasterProfitCenter _MasterProfitCenter = CHNLSVC.Inventory.GetProfitCenter(company, userDefPro);
                        List<MST_CHARG_CODE_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getChargCodeList(company, category, pgeNum, pgeSize, searchFld, searchVal, userDefPro);
                        List<Cus_chg_cds> chg = CHNLSVC.Tours.GETBUSCHARGECODES(service);
                        List<MST_CHARG_CODE_SEARCH_HEAD> advrefnew = new List<MST_CHARG_CODE_SEARCH_HEAD>();
                        if (_MasterProfitCenter.MPC_CHK_AUTO_APP==true)
                        {
                            foreach (var chglist in chg)
                            {
                                var list = advref.Where(a => a.STC_CD == chglist.bcd_chg_cd).ToList();
                                advrefnew.AddRange(list);
                            }
                        }
                        else
                        {
                            advrefnew.AddRange(advref);
                        }
                       

                        if (advrefnew != null && advrefnew.Count > 0)
                        {
                            decimal totalDoc = Math.Ceiling(Convert.ToInt32(advrefnew[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                            return Json(new { success = true, login = true, data = advrefnew, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        string category1 = "HTLRTS";
                        string category2 = "MSCELNS";
                        string category3 = "OVSLAGMT";
                        searchVal = searchVal.Trim();
                        List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD> advref1 = CHNLSVC.ComSearch.getChargCodeListMsclens(company, category1, pgeNum, pgeSize, searchFld, searchVal, userDefPro);
                        List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD> advref2 = CHNLSVC.ComSearch.getChargCodeListMsclens(company, category2, pgeNum, pgeSize, searchFld, searchVal, userDefPro);
                        List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD> advref3 = CHNLSVC.ComSearch.getChargCodeListMsclens(company, category3, pgeNum, pgeSize, searchFld, searchVal, userDefPro);
                        MasterProfitCenter _MasterProfitCenter = CHNLSVC.Inventory.GetProfitCenter(company, userDefPro);
                        List<Cus_chg_cds> chg = CHNLSVC.Tours.GETBUSCHARGECODES(service);
                        List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD> advrefnew = new List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD>();

                        if (_MasterProfitCenter.MPC_CHK_AUTO_APP == true)
                        {
                            foreach (var chglist in chg)
                            {
                                var list1 = advref1.Where(a => a.SSM_CD == chglist.bcd_chg_cd).ToList();
                                var list2 = advref2.Where(a => a.SSM_CD == chglist.bcd_chg_cd).ToList();
                                var list3 = advref3.Where(a => a.SSM_CD == chglist.bcd_chg_cd).ToList();

                                advrefnew.AddRange(list1);
                                advrefnew.AddRange(list2);
                                advrefnew.AddRange(list2);
                            }
                        }
                        else
                        {
                            advrefnew.AddRange(advref1);
                            advrefnew.AddRange(advref2);
                            advrefnew.AddRange(advref3);
                        }
                        if (advrefnew != null && advrefnew.Count > 0)
                        {
                            decimal totalDoc = Math.Ceiling(Convert.ToInt32(advrefnew[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                            return Json(new { success = true, login = true, data = advrefnew, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                        }
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult GetCountry(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_COUNTRY_SEARCH> oItems = CHNLSVC.ComSearch.getCountry(pgeNum, pgeSize, searchFld, searchVal);
                    decimal totalDoc = Math.Ceiling(Convert.ToInt32(oItems[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                    return Json(new { success = true, login = true, data = oItems, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult getChargCodeListArrival(string service,string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string category = "AIRTVL";
                    searchVal = searchVal.Trim();
                    List<MST_CHARG_CODE_AIRTVL_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getChargCodeListArrival(company, category, pgeNum, pgeSize, searchFld, searchVal,userDefPro);
                    if (advref != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getChargCodeListMsclens(string service,string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string category = service.Trim();
                    searchVal = searchVal.Trim();
                    List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getChargCodeListMsclens(company, category, pgeNum, pgeSize, searchFld, searchVal,userDefPro);
                    if (advref != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getChargCodeListMsclens2(string service,string option, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string category = service.Trim();
                    searchVal = searchVal.Trim();
                    string category1 = "HTLRTS";
                    string category2 = "MSCELNS";
                    string category3 = "OVSLAGMT";
                    searchVal = searchVal.Trim();
                    List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD> advref1 = CHNLSVC.ComSearch.getChargCodeListMsclens(company, category1, pgeNum, pgeSize, searchFld, searchVal, userDefPro);
                    List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD> advref2 = CHNLSVC.ComSearch.getChargCodeListMsclens(company, category2, pgeNum, pgeSize, searchFld, searchVal, userDefPro);
                    List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD> advref3 = CHNLSVC.ComSearch.getChargCodeListMsclens(company, category3, pgeNum, pgeSize, searchFld, searchVal, userDefPro);
                    MasterProfitCenter _MasterProfitCenter = CHNLSVC.Inventory.GetProfitCenter(company, userDefPro);
                    List<Cus_chg_cds> chg = CHNLSVC.Tours.GETBUSCHARGECODES(service);
                    List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD> advrefnew = new List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD>();

                    if (_MasterProfitCenter.MPC_CHK_AUTO_APP == true)
                    {
                        foreach (var chglist in chg)
                        {
                            var list1 = advref1.Where(a => a.SSM_CD == chglist.bcd_chg_cd).ToList();
                            var list2 = advref2.Where(a => a.SSM_CD == chglist.bcd_chg_cd).ToList();
                            var list3 = advref3.Where(a => a.SSM_CD == chglist.bcd_chg_cd).ToList();

                            advrefnew.AddRange(list1);
                            advrefnew.AddRange(list2);
                            advrefnew.AddRange(list2);
                        }
                    }
                    else
                    {
                        advrefnew.AddRange(advref1);
                        advrefnew.AddRange(advref2);
                        advrefnew.AddRange(advref3);
                    }
                   

                    if (advrefnew != null && advrefnew.Count > 0)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advrefnew[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = advrefnew, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getReceiptTypes(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_RECEIPT_TYPE_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getReceiptTypes(company, pgeNum, pgeSize, searchFld, searchVal);
                    if (advref != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getDivisions(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_DIVISION_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getDivisions(company, userDefPro, pgeNum, pgeSize, searchFld, searchVal);
                    if (advref != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult debtInvoiceSearch(string sroth, string srothval, string cusCd, string type, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    if (type == "DEBT")
                    {

                        if (string.IsNullOrEmpty(cusCd))
                        {
                            return Json(new { success = false, login = true, msg = "Please select customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        searchVal = searchVal.Trim();
                        if (sroth == "1")
                        {
                            if (srothval == "")
                            {
                                return Json(new { success = false, login = true, msg = "Please select showroom.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            else {
                                userDefPro = srothval;
                            }
                            List<MST_OUTSTANDING_INVOICE_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getOutstandingInvoice(company, userDefPro, cusCd, srothval, pgeNum, pgeSize, searchFld, searchVal);
                            if (advref != null)
                            {
                                decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                                return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            List<MST_OUTSTANDING_INVOICE_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getOutstandingInvoice(company, userDefPro, cusCd, "", pgeNum, pgeSize, searchFld, searchVal);
                            if (advref != null)
                            {
                                decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                                return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getAllProfitCenters(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_PROF_CEN_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getAllProfitCenters(company, pgeNum, pgeSize, searchFld, searchVal);
                    if (advref != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getGVISUVouchers(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_GIFTVOUCHER_ISSSUE_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getGVISUVouchers(company,"G", pgeNum, pgeSize, searchFld, searchVal);
                    if (advref != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getReceiptEntries(string unallow, string recTyp, string pgeNum, string pgeSize, string searchFld, string searchVal) {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    DateTime d1 = DateTime.Now;
                    d1 = d1.AddMonths(-1);
                    recTyp=recTyp.Trim();
                    if (recTyp == "DEBT")
                    {
                        if (unallow == "true")
                        {
                            List<MST_RECEIPT_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getUnallowReceiptEntries(company, userDefPro, d1, DateTime.Now, pgeNum, pgeSize, searchFld, searchVal);
                            if (advref.Count > 0)
                            {
                                decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                                return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else {
                            List<MST_RECEIPT_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getReceiptEntries(company, userDefPro, d1, DateTime.Now, pgeNum, pgeSize, searchFld, searchVal);
                            if (advref.Count > 0)
                            {
                                decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                                return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else {
                        List<MST_RECEIPT_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getReceiptEntries(company, userDefPro, d1, DateTime.Now, pgeNum, pgeSize, searchFld, searchVal);
                        if (advref.Count > 0)
                        {
                            decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                            return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                        }
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult debtInvoiceSearchNew(string sroth, string srothval, string cusCd, string type, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {


                List<MST_OUTSTANDING_INVOICE_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getOutstandingInvoice2(company, userDefPro, pgeNum, pgeSize, searchFld, searchVal);
                if (advref.Count>0)
                {
                    decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                    return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetEnqDataForNic(string nic, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_TRANSPORT_ENQUIRY_SEARCH_HEAD> data = CHNLSVC.ComSearch.getTransportEnqiurySearch(company, userDefPro, pgeNum, pgeSize, searchFld, searchVal);
                    List<MST_TRANSPORT_ENQUIRY_SEARCH_HEAD> data2 = data.Where(a => a.GCE_NIC == nic).ToList();
                    if (data2.Count > 0)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(data[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = data2, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getChannels(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_CHANNEL_SEARCH_HEAD> data = CHNLSVC.ComSearch.getChannels(company, pgeNum, pgeSize, searchFld, searchVal);
                    if (data != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(data[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = data, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }


        public JsonResult getSubChannels(string channel, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_SUBCHANNEL_SEARCH_HEAD> data = CHNLSVC.ComSearch.getSubChannels(channel, company, pgeNum, pgeSize, searchFld, searchVal);
                    if (data != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(data[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = data, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getServiceCodes(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_SERVICE_CODE_SEARCH_HEAD> data = CHNLSVC.ComSearch.getServiceCodes(company, userDefPro, pgeNum, pgeSize, searchFld, searchVal);
                    if (data != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(data[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = data, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getCoseSheets(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_COST_SHEET_SEARCH_HEAD> data = CHNLSVC.ComSearch.getCoseSheets(company, userDefPro, pgeNum, pgeSize, searchFld, searchVal);
                    if (data != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(data[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = data, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult GetCurrency(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_CURRENCY_SEARCH_HEAD> oItems = CHNLSVC.ComSearch.GetAllCurrencyNew(company, userDefPro, pgeNum, pgeSize, searchFld, searchVal);
                    decimal totalDoc = Math.Ceiling(Convert.ToInt32(oItems[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                    return Json(new { success = true, login = true, data = oItems, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult GetLogCompanies(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    string type = "TNSPT";
                    List<MST_COMPANIES_SEARCH_HEAD> oItems = CHNLSVC.ComSearch.GetLogCompanies(company, type, pgeNum, pgeSize, searchFld, searchVal);
                    decimal totalDoc = Math.Ceiling(Convert.ToInt32(oItems[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                    return Json(new { success = true, login = true, data = oItems, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult GetLogSheets(string pgeNum, string pgeSize, string searchFld, string searchVal,string selCompany,string profCen)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    selCompany = selCompany.Trim();
                    profCen = profCen.Trim();
                    List<MST_LOGSHEET_SEARCH_HEAD> oItems = CHNLSVC.ComSearch.GetLogSheets(selCompany, profCen, pgeNum, pgeSize, searchFld, searchVal);
                    decimal totalDoc = Math.Ceiling(Convert.ToInt32(oItems[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                    return Json(new { success = true, login = true, data = oItems, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult GetLogEnquiries(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    string type = "TNSPT";
                    string stage = "0,1,2,3,4,5,6,7,8,9,10,11,12,13";
                    List<MST_ENQUIRY_SEARCH_HEAD> oItems = CHNLSVC.ComSearch.GetLogEnquiries(company, userDefPro,type,stage, pgeNum, pgeSize, searchFld, searchVal);
                    decimal totalDoc = Math.Ceiling(Convert.ToInt32(oItems[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                    return Json(new { success = true, login = true, data = oItems, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult GetLogDrivers(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_EMPLOYEE_SEARCH_HEAD> oItems = CHNLSVC.ComSearch.GetLogDrivers(company, userDefPro, pgeNum, pgeSize, searchFld, searchVal);
                    decimal totalDoc = Math.Ceiling(Convert.ToInt32(oItems[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                    return Json(new { success = true, login = true, data = oItems, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult GetLogVehicles(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<mst_fleet_search_head> oItems = CHNLSVC.ComSearch.GetLogVehicles(company, userDefPro, pgeNum, pgeSize, searchFld, searchVal);
                    decimal totalDoc = Math.Ceiling(Convert.ToInt32(oItems[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                    return Json(new { success = true, login = true, data = oItems, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult geTRANSEnqiurySearch(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    string type = "TNSPT";
                    List<MST_TRANSPORT_ENQUIRY_SEARCH_HEAD> advref = CHNLSVC.ComSearch.geTRANSEnqiurySearch(company, userDefPro, type, pgeNum, pgeSize, searchFld, searchVal);
                    if (advref != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getAllEnquirySearch(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_TRANSPORT_ENQUIRY_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getAllEnquirySearch(company, userDefPro, pgeNum, pgeSize, searchFld, searchVal);
                    if (advref != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getUserProfitCenters(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_PROFIT_CENTER_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getUserProfitCenters(pgeNum, pgeSize, searchFld, searchVal, company, userId);
                    if (advref != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getCustomersByType(string pgeNum, string pgeSize, string searchFld, string searchVal, string type)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_CUS_SEARCH_HEAD> documents = CHNLSVC.ComSearch.getCustomerDetails(pgeNum, pgeSize, searchFld, searchVal, company,type);

                    if (documents != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(documents[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = documents, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getFacLocation(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_FAC_LOC_SEARCH_HEAD> documents = CHNLSVC.ComSearch.getFacLocation(pgeNum, pgeSize, searchFld, searchVal, company, userDefPro);

                    if (documents.Count > 0)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(documents[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = documents, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "", extraVal = true }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getOtherChargesForTransport(string service,string pgeNum, string pgeSize, string searchFld, string searchVal)
        { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<ST_ENQ_CHARGES> enqChrgItems = null;
                    if (Session["enqChrgItems"] != null)
                    {
                        enqChrgItems = (List<ST_ENQ_CHARGES>)Session["enqChrgItems"];
                    }
                    else
                    {
                        enqChrgItems = new List<ST_ENQ_CHARGES>();
                    }
                    string addedChgCd = "";
                    if (enqChrgItems.Count > 0)
                    {
                        Int32 i = 1;
                        foreach (ST_ENQ_CHARGES item in enqChrgItems)
                        {
                            if ((i != Convert.ToInt32(enqChrgItems.Count)))
                            {
                                addedChgCd +=item.SCH_ITM_CD+ ",";
                            }
                            else
                            {
                                addedChgCd += item.SCH_ITM_CD + "";
                            }
                            i++;
                        }
                    }
                    searchVal = searchVal.Trim();
                    service = service.Trim();
                    List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getOtherChargesForTransport(service, company, pgeNum, pgeSize, searchFld, searchVal, userDefPro, addedChgCd);
                    List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD> advref1 = CHNLSVC.ComSearch.getMescChargesWitoutParent(service, company, pgeNum, pgeSize, searchFld, searchVal, userDefPro, addedChgCd);

                    var allItems = advref.Concat(advref1)
                                    .ToList();
                    int x = 1;
                    List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD> finalList = new List<MST_CHARG_CODE_MSCELNS_SEARCH_HEAD>();
                    foreach (MST_CHARG_CODE_MSCELNS_SEARCH_HEAD item in allItems) {
                        item.R__ = x.ToString();
                        finalList.Add(item);
                        x++;
                    }
                    Int32 aCnt = (advref1.Count > 0) ? Convert.ToInt32(advref1[0].RESULT_COUNT) : 0;
                    Int32 bCnt=(advref.Count>0)?Convert.ToInt32(advref[0].RESULT_COUNT):0;
                    Int32 totDoc = aCnt + bCnt;
                    if (allItems.Count > 0)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(totDoc) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = finalList, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getChargCodeListWithType(string type, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (type != null || type != "")
                    {
                        string category = "TRANS";
                        searchVal = searchVal.Trim();
                        List<MST_CHARG_CODE_SEARCH_HEAD> advref = CHNLSVC.ComSearch.getChargCodeListWithType(company, category, pgeNum, pgeSize, searchFld, searchVal, userDefPro, type);
                        if (advref != null)
                        {
                            decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                            return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
                        return Json(new { success = false, login = true, msg = "Please select charge type.", data = "" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getDepositAmount(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<DEPO_AMT_SEARCH_HED> documents = CHNLSVC.ComSearch.getDepositAmounts(pgeNum, pgeSize, searchFld, searchVal);

                    if (documents.Count > 0)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(documents[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = documents, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "", extraVal = true }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }

    }
}