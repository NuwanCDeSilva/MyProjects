using FF.BusinessObjects;
using FF.BusinessObjects.BITool;
using FF.BusinessObjects.Commission;
using FF.BusinessObjects.Tours;
using FF.BusinessObjects.ToursNew;
using FF.BusinessObjects.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FF.BusinessObjects.Financial;
using FF.BusinessObjects.Account;

namespace FF.SCMWebMVC.Controllers
{
    public class SearchController : BaseController
    {
        //
        // GET: /Search/
        public JsonResult getProfitCenters(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
               // searchVal = searchVal.Trim();
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
                    return Json(new { success = false, login = true, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getMgProfitCenters(string pgeNum, string pgeSize, string searchFld, string searchVal, string mgr)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                // searchVal = searchVal.Trim();
                List<MST_PROFIT_CENTER_SEARCH_HEAD> data = CHNLSVC.ComSearch.getMgProfitCenters(pgeNum, pgeSize, searchFld, searchVal, company, mgr);
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
                    return Json(new { success = false, login = true, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = "Error" }, JsonRequestBehavior.AllowGet);
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
                List<EMP_SEARCH_HEAD_SCM> data = CHNLSVC.ComSearch.getEmployeeDetailsSCM(pgeNum, pgeSize, searchFld, searchVal.ToUpper(), company);
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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getManagerDetails(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<EMP_SEARCH_HEAD_SCM> data = CHNLSVC.ComSearch.getManagerDetails(pgeNum, pgeSize, searchFld, searchVal, company);
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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getSchemeType(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                //searchVal = "SchemeType";
                searchVal = searchVal.Trim();
                List<hpr_sch_tp> data = CHNLSVC.ComSearch.getSchemeType(pgeNum, pgeSize, searchFld, searchVal, company);
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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        //dilshan
        public JsonResult getCircular(string pgeNum, string pgeSize, string searchFld, string searchVal, string Cat)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<hpr_disr_val_ref> data = CHNLSVC.ComSearch.getCircular(pgeNum, pgeSize, searchFld, searchVal, company, Cat);
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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        //dilshan
        public JsonResult getCircularcbd(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<BonusDefinition> data = CHNLSVC.ComSearch.getCircularcbd(pgeNum, pgeSize, searchFld, searchVal, company);
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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getScheme(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                //if (searchVal == null)
                //{
                //    searchVal = "";
                //}
                //else
                //{
                //    searchVal = searchVal.Trim();
                //}
                //if(searchFld == null)
                //{
                //    searchFld = "Code";
                //}
                //if (pgeNum == null)
                //{
                //    pgeNum = "1";
                //}
                //if (pgeSize == null)
                //{
                //    pgeSize = "500";
                //}
                List<hpr_sch_det> data = CHNLSVC.ComSearch.getScheme(pgeNum, pgeSize, searchFld, searchVal, company);
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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getShwManagerdata(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<SHWRMMAN_SEARCH> data = CHNLSVC.ComSearch.getShowManager(pgeNum, pgeSize, searchFld, searchVal, company);
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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    
        public JsonResult getEmployeeCategory(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<EMP_SEARCH_HEAD_SCM> data = CHNLSVC.ComSearch.getEmployeeCatSCM(pgeNum, pgeSize, searchFld, searchVal, company);
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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getItems(string pgeNum, string pgeSize, string searchFld, string searchVal)
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
                    List<ITEM_SEARCH> documents = CHNLSVC.Dashboard.getItems(pgeNum, pgeSize, searchFld, searchVal, company);
                    if (documents != null || documents.Count == 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getItemModel(string pgeNum, string pgeSize, string searchFld, string searchVal)
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
                    List<ITEM_MODEL_SEARCH> documents = CHNLSVC.Dashboard.getItemModel(pgeNum, pgeSize, searchFld, searchVal, company);
                    if (documents != null || documents.Count == 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getItemBrands(string pgeNum, string pgeSize, string searchFld, string searchVal)
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
                    List<ITEM_BRAND_SEARCH> documents = CHNLSVC.Dashboard.getItemBrands(pgeNum, pgeSize, searchFld, searchVal, company);
                    if (documents != null || documents.Count == 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getMainCategory(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();  //string region, string subChannel, string channel, string company,string p_type
                    List<MAIN_CAT_SEARCH> documents = CHNLSVC.Dashboard.getMainCategory(pgeNum, pgeSize, searchFld, searchVal, company);
                    if (documents != null || documents.Count == 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getCategory2(string pgeNum, string pgeSize, string searchFld, string searchVal)
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
                    List<MAIN_CAT2_SEARCH> documents = CHNLSVC.Dashboard.getCategory2(pgeNum, pgeSize, searchFld, searchVal, company);
                    documents = documents.GroupBy(l => new { l.cat2_cd })
.Select(cl => new MAIN_CAT2_SEARCH
{
    cat2_cd = cl.First().cat2_cd,
    cat2_desc = cl.First().cat2_desc,
    RESULT_COUNT = cl.First().RESULT_COUNT,
    R__ = cl.First().R__
}).ToList();
                    if (documents != null || documents.Count == 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult CommissionCodeSearch(string pgeNum, string pgeSize, string searchFld, string searchVal)
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
                    List<ref_comm_hdr> documents = CHNLSVC.ComSearch.getCommissionCode(pgeNum, pgeSize, searchFld, searchVal, company);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult HandOverAccCodeSearch(string pgeNum, string pgeSize, string searchFld, string searchVal, string pc, string date)
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
                    DateTime firstdate = Convert.ToDateTime(date);
                    DateTime lastDayOfMonth = firstdate.AddMonths(1).AddDays(-1);
                    List<ACCCODESEARCH> documents = CHNLSVC.ComSearch.getHandAccCodes(pgeNum, pgeSize, searchFld, searchVal, company, pc, lastDayOfMonth);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult BonusCodeSearch(string pgeNum, string pgeSize, string searchFld, string searchVal, string schemeCode)
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
                    List<REF_BONUS_HDR> documents = CHNLSVC.ComSearch.getBonusCodeCode(pgeNum, pgeSize, searchFld, searchVal, company, schemeCode);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult ChartAccCodeSearch(string pgeNum, string pgeSize, string searchFld, string searchVal)
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
                    List<ref_cht_accgrp> documents = CHNLSVC.ComSearch.getChartAccCodeCode(pgeNum, pgeSize, searchFld, searchVal, company);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getInvoiceType(string pgeNum, string pgeSize, string searchFld, string searchVal)
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
                    List<Sar_Type> documents = CHNLSVC.ComSearch.GetCommissionInvTp(pgeNum, pgeSize, searchFld, searchVal, company);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getPriceCircula(string pgeNum, string pgeSize, string searchFld, string searchVal)
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
                    List<Sar_Type> documents = CHNLSVC.ComSearch.GetPromoCircula(pgeNum, pgeSize, searchFld, searchVal, company);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getLocHierarchy(string pgeNum, string pgeSize, string searchFld, string searchVal, string zone, string region, string area, string subChannel, string channel, string type, string com="")
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (com != null && com != "")
                    {
                        if (com != "undefined" )
                        {
                            company = com;
                        }
                        if (company=="")
                        {
                            //company = HttpContext.Session["UserCompanyCode"] as string;
                            company = "%";
                        }
                    }
                   
                    searchVal = searchVal.Trim();  //string region, string subChannel, string channel, string company,string p_type
                    List<LOC_HIRCH_SEARCH_HEAD> documents = CHNLSVC.Dashboard.getLocHierarchy(pgeNum, pgeSize, searchFld, searchVal, zone, region, area, subChannel, channel, company, type);
                    documents = documents.GroupBy(l => new { l.loc_hirch_cd})
.Select(cl => new LOC_HIRCH_SEARCH_HEAD
{
    loc_hirch_cd = cl.First().loc_hirch_cd,
    loc_hirch_desc = cl.First().loc_hirch_desc,
    RESULT_COUNT = cl.First().RESULT_COUNT,
    R__ = cl.First().R__
}).ToList();
                    if (documents != null && documents.Count > 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getLocHierarchyAll(string pgeNum, string pgeSize, string searchFld, string searchVal, string zone, string region, string area, string subChannel, string channel, string type, string com = "")
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (com != null && com != "")
                    {
                        if (com != "undefined")
                        {
                            company = com;
                        }
                        if (company == "")
                        {
                            company = HttpContext.Session["UserCompanyCode"] as string;
                        }
                    }

                    searchVal = searchVal.Trim();  //string region, string subChannel, string channel, string company,string p_type
                    List<LOC_HIRCH_SEARCH_HEAD> documents = CHNLSVC.Dashboard.getLocHierarchyAll(pgeNum, pgeSize, searchFld, searchVal, zone, region, area, subChannel, channel, company, type);
                    if (documents != null && documents.Count > 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getBTUVal(string pgeNum, string pgeSize, string searchFld, string searchVal, string Cat)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<BTU_SEARCH> data = CHNLSVC.ComSearch.getBTUCodes(pgeNum, pgeSize, searchFld, searchVal, Cat);
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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
       // 
        public JsonResult loadCompany()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<SystemUserCompany> _systemUserCompanyList = CHNLSVC.Security.GetUserCompany(userId.ToUpper());
                    if (_systemUserCompanyList != null)
                    {
                        return Json(new { success = true, login = true, companyList = _systemUserCompanyList }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No company found for display", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult loadScheme()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    //List<SystemUserCompany> _systemUserCompanyList = CHNLSVC.Security.GetUserCompany(userId.ToUpper());
                    List<hpr_sch_det> _systemUserCompanyList = CHNLSVC.ComSearch.getScheme("1", "1000", "Code", "", company);
                    if (_systemUserCompanyList != null)
                    {
                        return Json(new { success = true, login = true, companyList = _systemUserCompanyList }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No company found for display", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult loadSchemeType()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    //List<SystemUserCompany> _systemUserCompanyList = CHNLSVC.Security.GetUserCompany(userId.ToUpper());
                    List<hpr_sch_tp> _systemUserCompanyList = CHNLSVC.ComSearch.getSchemeType("1", "1000", "Code", "", company);
                    if (_systemUserCompanyList != null)
                    {
                        return Json(new { success = true, login = true, companyList = _systemUserCompanyList }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No company found for display", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult loadChannel()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    //List<SystemUserCompany> _systemUserCompanyList = CHNLSVC.Security.GetUserCompany(userId.ToUpper());
                    //List<hpr_sch_tp> _systemUserCompanyList = CHNLSVC.ComSearch.getSchemeType("1", "1000", "Code", "", company);
                    List<LOC_HIRCH_SEARCH_HEAD> _systemUserCompanyList = CHNLSVC.Dashboard.getLocHierarchyAll("1", "1000", "Code", "", "", "", "", "", "", company, "channel");
                    if (_systemUserCompanyList != null)
                    {
                        return Json(new { success = true, login = true, companyList = _systemUserCompanyList }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No company found for display", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getRentSCHDetail(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                List<RENT_SCH_SEARCH> data = CHNLSVC.ComSearch.getRentSCHDetail(pgeNum, pgeSize, searchFld, searchVal, company);
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
                    return Json(new { success = false, login = true, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }


        public JsonResult getPaymentTypes(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                List<PAYMENT_TYPES> data = CHNLSVC.ComSearch.getPaymentTypes(pgeNum, pgeSize, searchFld, searchVal, company);
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
                    return Json(new { success = false, login = true, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getPaymentSubTypes(string pgeNum, string pgeSize, string searchFld, string searchVal,string p_type)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                List<PAYMENT_SUB_TYPES> data = CHNLSVC.ComSearch.getPaymentSubTypes(pgeNum, pgeSize, searchFld, searchVal, company, p_type);
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
                    return Json(new { success = false, login = true, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        //Pasindu 2018/05/06
        public JsonResult SchemeNumberSearch(string pgeNum, string pgeSize, string searchFld, string searchVal,string circularcode)
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
                    List<REF_BONUS_SCHEME> documents = CHNLSVC.ComSearch.getSchemeCode(pgeNum, pgeSize, searchFld, searchVal, company, circularcode);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult searchPaymentRequest(string pgeNum, string pgeSize, string searchFld, string searchVal, string reqtp)
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
                    List<SRCH_PAY_REQ> documents = CHNLSVC.ComSearch.searchPaymentRequest(pgeNum, pgeSize, searchFld, searchVal, company, reqtp);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult searchAccountnumbers(string pgeNum, string pgeSize, string searchFld, string searchVal)
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
                    List<SRCH_ACC_DET> documents = CHNLSVC.ComSearch.searchAccountnumbers(pgeNum, pgeSize, searchFld, searchVal, company);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult getSearchField(string pgeNum, string pgeSize, string searchFld, string searchVal)
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
                    List<SRCH_TEMP_SRCH> documents = CHNLSVC.ComSearch.searchFieldValue(pgeNum, pgeSize, searchFld, searchVal, company);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getDynamicSrchValues(string pgeNum, string pgeSize, string searchFld, string searchVal, string field)
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
                    List<COMMON_SEARCH> documents = CHNLSVC.ComSearch.searchCommonDataValue(pgeNum, pgeSize, searchFld, searchVal, company, userId, field);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
        
        }
        public JsonResult srchAccPaymentTypes(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                List<PAYMENT_TYPES> data = CHNLSVC.ComSearch.getAccPaymentTypes(pgeNum, pgeSize, searchFld, searchVal, company);
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
                    return Json(new { success = false, login = true, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult srchTaxTypes(string pgeNum, string pgeSize, string searchFld, string searchVal,string creditor)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {

                string error = "";
                REF_CHT_ACC dat = CHNLSVC.Finance.getAccountDetail(creditor.Trim(), company, out error);
                if (error != "" || dat.RCA_ACC_NO == null)
                {
                    if (dat.RCA_ACC_NO == null)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid creditor account number." }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                }

                if (dat.RCA_ANAL5 == 0)
                {
                    creditor = "";
                }


                List<ACCOUNT_TAX> data = CHNLSVC.ComSearch.srchTaxTypes(pgeNum, pgeSize, searchFld, searchVal, company, creditor);
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
                    return Json(new { success = false, login = true, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult srchAccountPODet(string pgeNum, string pgeSize, string searchFld, string searchVal, string creditor, string type)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                string error = "";
                REF_CHT_ACC dat = CHNLSVC.Finance.getAccountDetail(creditor.Trim(), company, out error);
                if (error != "" || dat.RCA_ACC_NO == null)
                {
                    if (dat.RCA_ACC_NO == null)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid creditor account number." }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                }


                List<PUR_SEARCH> data = CHNLSVC.ComSearch.searchPOPayRequest(pgeNum, pgeSize, searchFld, searchVal, company, type, creditor);
                if (data != null)
                {
                    List<PUR_SELECTED> selecteditem = (List<PUR_SELECTED>)Session["PUR_SELECTED"];
                    if (selecteditem != null && selecteditem.Count > 0)
                    {
                        foreach (PUR_SELECTED item in selecteditem)
                        {
                            if (data.Where(x => x.PURNO == item.PURNO).Count() > 0)
                            {
                                data.Where(x => x.PURNO == item.PURNO).First().SELECT = 1;
                            }
                        }
                    }
                    else
                    {
                        data.ForEach(x => x.SELECT = 0);
                    }
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
        }
	}
}