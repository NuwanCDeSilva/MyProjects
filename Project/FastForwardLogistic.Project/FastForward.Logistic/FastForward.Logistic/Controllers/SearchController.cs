using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FF.BusinessObjects;
using FF.BusinessObjects.Genaral;
using System.Text.RegularExpressions;
using FF.BusinessObjects.Search;
using FF.BusinessObjects.Sales;
using FF.BusinessObjects.Security;


namespace FastForward.Logistic.Controllers
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

        /// <summary>
        /// Isuru 2017/05/26
        /// </summary>
        /// <param name="pgeNum"></param>
        /// <param name="pgeSize"></param>
        /// <param name="searchFld"></param>
        /// <param name="searchVal"></param>
        /// <returns></returns>
        public JsonResult GetCustomerDetails(string pgeNum, string pgeSize, string searchFld, string searchVal) 
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_CUS_SEARCH_HEAD> customers = CHNLSVC.CommonSearch.getCustomerDetails(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            
        }

        //added by dilshan on 26/01/2018
        public JsonResult GetCustomerDetailsByType(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_CUS_SEARCH_HEAD> customers = CHNLSVC.CommonSearch.getCustomerDetailsByType(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        //added by dilshan on 26/01/2018
        public JsonResult GetCustomerDetailsByType_New(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_CUS_SEARCH_HEAD> customers = CHNLSVC.CommonSearch.getCustomerDetailsByType_New(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        // Added by Chathura on 2-oct-2017
        public JsonResult GetCustomerDetailsByJobNo(string jobno, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_CUS_SEARCH_HEAD> customers = CHNLSVC.CommonSearch.getCustomerDetailsByJobNo(jobno, pgeNum, pgeSize, searchFld, searchVal, company);

                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        //public JsonResult GetJobNumber(string pgeNum, string pgeSize, string searchFld, string searchVal)
        //{
        //    string userId = HttpContext.Session["UserID"] as string;
        //    string company = HttpContext.Session["UserCompanyCode"] as string;
        //    string userDefPro = HttpContext.Session["UserDefProf"] as string;
        //    string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

        //    try
        //    {
        //        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //        {
        //            List<JOB_NUM_SEARCH> customers = CHNLSVC.CommonSearch.getJobNumber(pgeNum, pgeSize, searchFld, searchVal, company);

        //            if (customers != null)
        //            {
        //                decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
        //                return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        else
        //        {
        //            return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //        {
        //            return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
        //        }
        //    }

        //}
        public JsonResult GetJobNumber(string pgeNum, string pgeSize, string searchFld, string searchVal, string fromDate, string todate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            searchVal = searchVal.Trim();
            DateTime d1 = DateTime.Now;
            d1 = d1.AddMonths(-1);

            if (fromDate == null || todate == null || fromDate == "" || todate == "")
            {
                fromDate = d1.ToString();
                todate = DateTime.Now.ToString();
            }

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<JOB_NUM_SEARCH> customers = CHNLSVC.CommonSearch.getJobNumber(Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), pgeNum, pgeSize, searchFld, searchVal, company, userDefPro);

                    if (customers.Count != 0)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        //dilshan
        public JsonResult GetAllJobNumbers(string pgeNum, string pgeSize, string searchFld, string searchVal, string fromDate, string todate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            searchVal = searchVal.Trim();
            DateTime d1 = DateTime.Now;
            d1 = d1.AddMonths(-1);

            if (fromDate == null || todate == null || fromDate == "" || todate == "")
            {
                fromDate = d1.ToString();
                todate = DateTime.Now.ToString();
            }

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<JOB_NUM_SEARCH> customers = CHNLSVC.CommonSearch.GetAllJobNumber(Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), pgeNum, pgeSize, searchFld, searchVal, company, userDefPro);

                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult GetCustomerDetailsC(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_CUS_SEARCH_HEAD> cc = CHNLSVC.CommonSearch.getCustomerDetailsWithtype(pgeNum, pgeSize, searchFld, searchVal, company, "", "ACC_CRD");
                    List<MST_CUS_SEARCH_HEAD> cr = CHNLSVC.CommonSearch.getCustomerDetailsWithtype(pgeNum, pgeSize, searchFld, searchVal, company, "", "CUST");
                    List<MST_CUS_SEARCH_HEAD> ac = CHNLSVC.CommonSearch.getCustomerDetailsWithtype(pgeNum, pgeSize, searchFld, searchVal, company, "", "ACC_CUS");
                    List<MST_CUS_SEARCH_HEAD> reg = CHNLSVC.CommonSearch.getCustomerDetailsWithtype(pgeNum, pgeSize, searchFld, searchVal, company, "", "REG");

                    //List<MST_CUS_SEARCH_HEAD> customers = CHNLSVC.CommonSearch.getCustomerDetailsWithtype(pgeNum, pgeSize, searchFld, searchVal, company,"","CUST");
                    List<MST_CUS_SEARCH_HEAD> customers = cc.Concat(cr).Concat(ac).Concat(reg).ToList();

                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult GetCustomerDetailsCJobFiltered(string jobno, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_CUS_SEARCH_HEAD> cc = CHNLSVC.CommonSearch.getCustomerDetailsJobFiltered(pgeNum, pgeSize, searchFld, searchVal, company, "", "ACC_CRD",jobno);
                    List<MST_CUS_SEARCH_HEAD> cr = CHNLSVC.CommonSearch.getCustomerDetailsJobFiltered(pgeNum, pgeSize, searchFld, searchVal, company, "", "CUST",jobno);
                    List<MST_CUS_SEARCH_HEAD> ac = CHNLSVC.CommonSearch.getCustomerDetailsJobFiltered(pgeNum, pgeSize, searchFld, searchVal, company, "", "ACC_CUS", jobno);
                    List<MST_CUS_SEARCH_HEAD> reg = CHNLSVC.CommonSearch.getCustomerDetailsJobFiltered(pgeNum, pgeSize, searchFld, searchVal, company, "", "REG", jobno);

                    //List<MST_CUS_SEARCH_HEAD> customers = CHNLSVC.CommonSearch.getCustomerDetailsWithtype(pgeNum, pgeSize, searchFld, searchVal, company,"","CUST");
                    List<MST_CUS_SEARCH_HEAD> customers = new List<MST_CUS_SEARCH_HEAD>();

                    if (cc != null) { customers = customers.Concat(cc).ToList(); }
                    if (cr != null) { customers = customers.Concat(cr).ToList(); }
                    if (ac != null) { customers = customers.Concat(ac).ToList(); }
                    if (reg != null) { customers = customers.Concat(reg).ToList(); }
                    //customers = cc.Concat(cr).Concat(ac).ToList();

                    if (customers != null && customers.Count>0)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult GetCustomerDetailsNP(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_CUS_SEARCH_HEAD> customers = CHNLSVC.CommonSearch.getCustomerDetailsWithtype(pgeNum, pgeSize, searchFld, searchVal, company,"","NOTP");

                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult GetSupplir(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_CUS_SEARCH_HEAD> customers = CHNLSVC.CommonSearch.getCustomerDetailsWithtype(pgeNum, pgeSize, searchFld, searchVal, company, "", "SHIPR");

                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult GetConsignee(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_CUS_SEARCH_HEAD> customers = CHNLSVC.CommonSearch.getCustomerDetailsWithtype(pgeNum, pgeSize, searchFld, searchVal, company, "", "CONS");

                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult GetAgent(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_CUS_SEARCH_HEAD> customers = CHNLSVC.CommonSearch.getCustomerDetailsWithtype(pgeNum, pgeSize, searchFld, searchVal, company, "", "AGET");

                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult GetServiceAgent(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_CUS_SEARCH_HEAD> customers = CHNLSVC.CommonSearch.getCustomerDetailsWithtype(pgeNum, pgeSize, searchFld, searchVal, company, "", "SHIPL");

                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult GetBLNumberD(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<BL_NUM_SEARCH> customers = CHNLSVC.CommonSearch.getBLNumber(pgeNum, pgeSize, searchFld, searchVal, company,"DRAFT");

                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        // Added by Chathura on 9-oct-2017
        public JsonResult GetBLNumberDdf(string pgeNum, string pgeSize, string searchFld, string searchVal, string fromDate, string todate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            searchVal = searchVal.Trim();
            DateTime d1 = DateTime.Now;
            d1 = d1.AddMonths(-1);

            if (fromDate == null || todate == null || fromDate == "" || todate == "")
            {
                fromDate = d1.ToString();
                todate = DateTime.Now.ToString();
            }

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<BL_NUM_SEARCH_DBL> customers = CHNLSVC.CommonSearch.getBLNumberDDf(Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), pgeNum, pgeSize, searchFld, searchVal, company, "DRAFT", userDefPro);

                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult GetBLNumberH(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<BL_NUM_SEARCH> customers = CHNLSVC.CommonSearch.getBLNumber(pgeNum, pgeSize, searchFld, searchVal, company, "HOUSE");

                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult GetBLNumberM(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<BL_NUM_SEARCH> customers = CHNLSVC.CommonSearch.getBLNumber(pgeNum, pgeSize, searchFld, searchVal, company, "MASTER");


                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult GetBLNumberDf(string pgeNum, string pgeSize, string searchFld, string searchVal, string fromDate, string todate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            searchVal = searchVal.Trim();
            DateTime d1 = DateTime.Now;
            d1 = d1.AddMonths(-1);

            if (fromDate == null || todate == null || fromDate == "" || todate == "")
            {
                fromDate = d1.ToString();
                todate = DateTime.Now.ToString();
            }

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<BL_NUM_SEARCH> customers = CHNLSVC.CommonSearch.getBLNumberDf(Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), pgeNum, pgeSize, searchFld, searchVal, company, "MASTER");


                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult GetBLNumberDfMBL(string pgeNum, string pgeSize, string searchFld, string searchVal, string fromDate, string todate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            searchVal = searchVal.Trim();
            DateTime d1 = DateTime.Now;
            d1 = d1.AddMonths(-1);

            if (fromDate == null || todate == null || fromDate == "" || todate == "")
            {
                fromDate = d1.ToString();
                todate = DateTime.Now.ToString();
            }

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<BL_NUM_SEARCH_MBL> customers = CHNLSVC.CommonSearch.getBLNumberDfMBL(Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), pgeNum, pgeSize, searchFld, searchVal, company, "MASTER");


                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult GetUOM(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<UOM_SEARCH> customers = CHNLSVC.CommonSearch.getUOM(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult getAllSearch(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string[] words = pgeNum.Split('-');
                    pgeNum = words[0].ToString();
                    string column = words[1].ToString();
                    if (column.Contains("BL_H_DOC_NO"))
                    {
                        column = "BL_H_DOC_NO";
                    }

                    List<FIELD_SEARCH> fielsds = CHNLSVC.CommonSearch.getAllSearch(pgeNum, pgeSize, searchFld, searchVal, column);

                    fielsds = fielsds.OrderByDescending(A => A.CODE).ToList();
                    if (fielsds != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(fielsds[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = fielsds, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult getAllSearchDf(string pgeNum, string pgeSize, string searchFld, string searchVal, string fromDate, string todate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            searchVal = searchVal.Trim();
            DateTime d1 = DateTime.Now;
            d1 = d1.AddMonths(-1);

            if (fromDate == null || todate == null || fromDate == "" || todate == "")
            {
                fromDate = d1.ToString();
                todate = DateTime.Now.ToString();
            }

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string[] words = pgeNum.Split('-');
                    pgeNum = words[0].ToString();
                    string column = words[1].ToString();
                    if (column.Contains("BL_H_DOC_NO"))
                    {
                        column = "BL_H_DOC_NO";
                    }

                    List<FIELD_SEARCH_DF> fielsds = CHNLSVC.CommonSearch.getAllSearchDf(Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), pgeNum, pgeSize, searchFld, searchVal, column);

                    fielsds = fielsds.OrderByDescending(A => A.CODE).ToList();
                    if (fielsds != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(fielsds[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = fielsds, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult getAllSearchDfHBL(string pgeNum, string pgeSize, string searchFld, string searchVal, string fromDate, string todate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            searchVal = searchVal.Trim();
            DateTime d1 = DateTime.Now;
            d1 = d1.AddMonths(-1);

            if (fromDate == null || todate == null || fromDate == "" || todate == "")
            {
                fromDate = d1.ToString();
                todate = DateTime.Now.ToString();
            }

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string[] words = pgeNum.Split('-');
                    pgeNum = words[0].ToString();
                    string column = words[1].ToString();
                    if (column.Contains("BL_H_DOC_NO"))
                    {
                        column = "BL_H_DOC_NO";
                    }

                    List<FIELD_SEARCH_DF_HBL> fielsds = CHNLSVC.CommonSearch.getAllSearchDfHBL(Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), pgeNum, pgeSize, searchFld, searchVal, column);

                    fielsds = fielsds.OrderByDescending(A => A.CODE).ToList();
                    if (fielsds != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(fielsds[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = fielsds, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult getAllSearchDfHBLJob(string pgeNum, string pgeSize, string searchFld, string searchVal, string fromDate, string todate, string jobno)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            searchVal = searchVal.Trim();
            DateTime d1 = DateTime.Now;
            d1 = d1.AddMonths(-1);

            if (fromDate == null || todate == null || fromDate == "" || todate == "")
            {
                fromDate = d1.ToString();
                todate = DateTime.Now.ToString();
            }

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string[] words = pgeNum.Split('-');
                    pgeNum = words[0].ToString();
                    string column = words[1].ToString();
                    if (column.Contains("BL_H_DOC_NO"))
                    {
                        column = "BL_H_DOC_NO";
                    }

                    List<FIELD_SEARCH_DF_HBL> fielsds = CHNLSVC.CommonSearch.getAllSearchDfHBLJob(Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), jobno, pgeNum, pgeSize, searchFld, searchVal, column);

                    fielsds = fielsds.OrderByDescending(A => A.CODE).ToList();
                    if (fielsds != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(fielsds[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = fielsds, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult getAllSearchByJobNo(string jobno, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string[] words = pgeNum.Split('-');
                    pgeNum = words[0].ToString();
                    string column = words[1].ToString();
                    if (column.Contains("BL_H_DOC_NO"))
                    {
                        column = "BL_H_DOC_NO";
                    }

                    List<FIELD_SEARCH> fielsds = CHNLSVC.CommonSearch.getAllSearchByJobNo(jobno, pgeNum, pgeSize, searchFld, searchVal, column);

                    fielsds = fielsds.OrderByDescending(A => A.CODE).ToList();
                    if (fielsds != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(fielsds[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = fielsds, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult getPorts(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<SEARCH_PORT> list = CHNLSVC.CommonSearch.getPorts(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (list != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(list[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = list, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult getPortsRef(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<SEARCH_PORT> list = CHNLSVC.CommonSearch.getPortsRef(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (list != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(list[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = list, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult getVessels(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<SEARCH_VESSEL> list = CHNLSVC.CommonSearch.getVessels(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (list != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(list[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = list, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult getVesselsRef(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<SEARCH_VESSEL> list = CHNLSVC.CommonSearch.getVesselsRef(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (list != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(list[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = list, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult GetMesureTp(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<SEARCH_MESURE_TP> customers = CHNLSVC.CommonSearch.Get_Mesure_Tp(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        // Changed by chathura on 15-sep-2017 and commented below
        public JsonResult getUserProfitCenters(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string userDefChanel = HttpContext.Session["UserDefChnl"] as string; // CMC

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_PC_SEARCH> advref = CHNLSVC.CommonSearch.getUserProfitCenters(pgeNum, pgeSize, searchFld, searchVal, company, userId, userDefChanel);
                    if (advref.Count > 0)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        //public JsonResult getUserProfitCenters(string pgeNum, string pgeSize, string searchFld, string searchVal)
        //{
        //    string userId = HttpContext.Session["UserID"] as string;
        //    string company = HttpContext.Session["UserCompanyCode"] as string;
        //    string userDefPro = HttpContext.Session["UserDefProf"] as string;
        //    string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //        {
        //            searchVal = searchVal.Trim();
        //            List<MST_PC_SEARCH> advref = CHNLSVC.CommonSearch.getUserProfitCenters(pgeNum, pgeSize, searchFld, searchVal, company, userId);
        //            if (advref.Count > 0 )
        //            {
        //                decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
        //                return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "",type="Info" }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        else
        //        {
        //            return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //        {
        //            return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(new { success = false, login = false, msg = ex.Message.ToString(),type="Error" }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //}

        // Added by Chathura on 13-Sep-2017
        public JsonResult getModeOfShipment(string pgeNum, string pgeSize, string searchFld, string searchVal)
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
                    List<MST_MS_SEARCH> advref = CHNLSVC.CommonSearch.getModeOfShipment(pgeNum, pgeSize, searchFld, searchVal, company, userId);
                    if (advref.Count > 0)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getUserCompanySet(string pgeNum, string pgeSize, string searchFld, string searchVal)
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
                    List<MST_COM_SEARCH> advref = CHNLSVC.CommonSearch.getUserCompanySet(pgeNum, pgeSize, searchFld, searchVal, company, userId);
                    if (advref.Count > 0)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
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
                List<MST_EMPLOYEE_SEARCH_HEAD> data = CHNLSVC.CommonSearch.getEmployeeDetails(pgeNum, pgeSize, searchFld, searchVal, company);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString()}, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        //dilshan on 26/05/2018
        public JsonResult getEmployeeDetailsEx(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<MST_EMPLOYEE_SEARCH_HEAD> data = CHNLSVC.CommonSearch.getEmployeeDetailsEx(pgeNum, pgeSize, searchFld, searchVal, company);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        //********************
        //public JsonResult getPtyChshReqDet(string pgeNum, string pgeSize, string searchFld, string searchVal,string type)
        //{
        //    string userId = HttpContext.Session["UserID"] as string;
        //    string company = HttpContext.Session["UserCompanyCode"] as string;
        //    string userDefPro = HttpContext.Session["UserDefProf"] as string;
        //    string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
        //    try
        //    {
        //        searchVal = searchVal.Trim();
        //        List<PETTYCASH_REQHDR_SRCHHED> data = CHNLSVC.CommonSearch.ptyCshReqSearch(pgeNum, pgeSize, searchFld, searchVal, company, userDefPro, type);
        //        if (data.Count > 0)
        //        {
        //            decimal totalDoc = Math.Ceiling(Convert.ToInt32(data[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
        //            return Json(new { success = true, login = true, data = data, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //        {
        //            return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //}

        public JsonResult getPtyChshReqDet(string pgeNum, string pgeSize, string searchFld, string searchVal, string type, string fromDate, string todate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            searchVal = searchVal.Trim();
            DateTime d1 = DateTime.Now;
            d1 = d1.AddMonths(-1);
            type = type.Trim();

            if (fromDate == null || todate == null || fromDate == "" || todate == "")
            {
                fromDate = d1.ToString();
                todate = DateTime.Now.ToString();
            }

            try
            {
                searchVal = searchVal.Trim();
                List<PETTYCASH_REQHDR_SRCHHED> data = CHNLSVC.CommonSearch.ptyCshReqSearch(Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), pgeNum, pgeSize, searchFld, searchVal, company, userDefPro, type);
                if (data.Count > 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getConsigneeDetails(string pgeNum, string pgeSize, string searchFld, string searchVal,string type)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<CONS_SEARCH_HEAD> data = CHNLSVC.CommonSearch.getConsigneeDetails(pgeNum, pgeSize, searchFld, searchVal, company, type);
                if (data.Count > 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getPettyCashJob(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<JOB_NUM_SEARCH> data = CHNLSVC.CommonSearch.getPettyCashJobSearch(pgeNum, pgeSize, searchFld, searchVal, company, userDefPro);
                if (data.Count > 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult getCostElement(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<MST_COST_ELEMENT_SEARCH> data = CHNLSVC.CommonSearch.getCostElemts(pgeNum, pgeSize, searchFld, searchVal);
                if (data.Count > 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult getRevenueElements(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<MST_COST_ELEMENT_SEARCH> data = CHNLSVC.CommonSearch.getRevenueElements(pgeNum, pgeSize, searchFld, searchVal);
                if (data.Count > 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult getCostElementRef(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<MST_COST_ELEMENT_SEARCH> data = CHNLSVC.CommonSearch.getCostElemtsRef(pgeNum, pgeSize, searchFld, searchVal);
                if (data.Count > 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult getCurrencyCodes(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<MST_CUR_SEARCH> data = CHNLSVC.CommonSearch.getCurrency(pgeNum, pgeSize, searchFld, searchVal);
                if (data.Count > 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }


        public JsonResult getTeVehLcDetails(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<FTW_VEHICLE_NO_SEARCH> data = CHNLSVC.CommonSearch.getTelVehLcDetails(pgeNum, pgeSize, searchFld, searchVal);
                if (data.Count > 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult getInvoiceNo(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<SEARCH_INVOICE> data = CHNLSVC.CommonSearch.getInvoiceNo(pgeNum, pgeSize, searchFld, searchVal,company,userDefPro);
                if (data.Count > 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult getInvoiceNoCrd(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<SEARCH_INVOICE> data = CHNLSVC.CommonSearch.getInvoiceNoCrd(pgeNum, pgeSize, searchFld, searchVal, company, userDefPro);
                if (data.Count > 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult getDebtNo(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<SEARCH_INVOICE> data = CHNLSVC.CommonSearch.getDebitNoteNo(pgeNum, pgeSize, searchFld, searchVal, company, userDefPro);
                if (data.Count > 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult getInvoiceNoByCus(string pgeNum, string pgeSize, string searchFld, string searchVal, string cus, string othpc)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (cus == null || cus == "")
                {
                    return Json(new { success = false, login = true, msg = "Please select Debtor Code."}, JsonRequestBehavior.AllowGet);

                }
                searchVal = searchVal.Trim();
                List<SEARCH_INVOICE_BAL> data = CHNLSVC.CommonSearch.getInvoiceNoByCus(pgeNum, pgeSize, searchFld, searchVal, company, cus, userDefPro, othpc);
                if (data.Count > 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult getSettlementList(string pgeNum, string pgeSize, string searchFld, string searchVal, string fromDate, string todate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            searchVal = searchVal.Trim();
            DateTime d1 = DateTime.Now;
            d1 = d1.AddMonths(-1);

            if (fromDate == null || todate == null || fromDate == "" || todate == "")
            {
                fromDate = d1.ToString();
                todate = DateTime.Now.ToString();
            }

            try
            {
                searchVal = searchVal.Trim();
                List<PETTYCASH_SETTLE_SEARCH> data = CHNLSVC.CommonSearch.getSettlementList(Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), pgeNum, pgeSize, searchFld, searchVal, company, userDefPro);
                if (data.Count > 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
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
                    List<MST_COUNTRY_SEARCH> oItems = CHNLSVC.CommonSearch.getCountry(pgeNum, pgeSize, searchFld, searchVal);
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
                    return Json(new { success = false, login = true, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = "Error" }, JsonRequestBehavior.AllowGet);
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
                    List<MST_TOWN_SEARCH_HEAD> documents = CHNLSVC.CommonSearch.getTownDetails(pgeNum, pgeSize, searchFld, searchVal);

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
                    return Json(new { success = false, login = true, msg = "Error"}, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        //Dilshan
        public JsonResult getTownwithCountry(string pgeNum, string pgeSize, string searchFld, string searchVal, string searchVal1)
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
                    List<MST_TOWN_SEARCH_HEAD> documents = CHNLSVC.CommonSearch.getTownwithCountry(pgeNum, pgeSize, searchFld, searchVal, searchVal1);

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
                    return Json(new { success = false, login = true, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        //Dilshan
        public JsonResult getDistrictwithCountry(string pgeNum, string pgeSize, string searchFld, string searchVal, string searchVal1)
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
                    List<MST_DISTRICT_SEARCH> documents = CHNLSVC.CommonSearch.getDistrictwithCountry(pgeNum, pgeSize, searchFld, searchVal, searchVal1);

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
                    return Json(new { success = false, login = true, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        //Dilshan
        public JsonResult getProvincewithCountry(string pgeNum, string pgeSize, string searchFld, string searchVal, string searchVal1)
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
                    List<MST_PROVINCE_SEARCH> documents = CHNLSVC.CommonSearch.getProvincewithCountry(pgeNum, pgeSize, searchFld, searchVal, searchVal1);

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
                    return Json(new { success = false, login = true, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getJobPouchSearch(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {


                    List<FIELD_SEARCH> fielsds = CHNLSVC.CommonSearch.getJobPouchSearch(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (fielsds != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(fielsds[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = fielsds, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getJobPouchSearch2(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {


                    List<FIELD_SEARCH2> fielsds = CHNLSVC.CommonSearch.getJobPouchSearch2(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (fielsds != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(fielsds[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = fielsds, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
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
                    List<MST_BANKACC_SEARCH_HEAD> banks = CHNLSVC.CommonSearch.getDepositedBanks(pgeNum, pgeSize, searchFld, searchVal, company);
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
                    return Json(new { success = false, login = true, msg = ex .Message}, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
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
                    List<MST_BUSCOM_BANK_SEARCH_HEAD> banks = CHNLSVC.CommonSearch.getBusComBanks(pgeNum, pgeSize, searchFld, searchVal);
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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
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
                        List<MST_BUSCOM_BANKBRANCH_SEARCH_HEAD> branchs = CHNLSVC.CommonSearch.getBoscomBankBranchs(bankcd, pgeNum, pgeSize, searchFld, searchVal);

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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        // Added by Chathura on 20-sep-2017
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
                    List<MST_DIVISION_SEARCH> advref = CHNLSVC.CommonSearch.getDivisions(company, userDefPro, pgeNum, pgeSize, searchFld, searchVal);
                    if (advref != null && advref.Count>0)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No divisions found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true,msg=ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
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
                    List<MST_RECEIPT_TYPE_SEARCH_HEAD> advref = CHNLSVC.CommonSearch.getReceiptTypes(company, pgeNum, pgeSize, searchFld, searchVal);
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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        //public JsonResult getReceiptEntries(string unallow, string recTyp, string pgeNum, string pgeSize, string searchFld, string searchVal)
        //{

        //    string userId = HttpContext.Session["UserID"] as string;
        //    string company = HttpContext.Session["UserCompanyCode"] as string;
        //    string userDefPro = HttpContext.Session["UserDefProf"] as string;
        //    string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //        {
        //            searchVal = searchVal.Trim();
        //            DateTime d1 = DateTime.Now;
        //            d1 = d1.AddMonths(-1);
        //            recTyp = recTyp.Trim();
        //            if (recTyp == "DEBT")
        //            {
        //                if (unallow == "true")
        //                {
        //                    List<MST_RECEIPT_SEARCH_HEAD> advref = CHNLSVC.CommonSearch.getUnallowReceiptEntries(company, userDefPro, d1, DateTime.Now, pgeNum, pgeSize, searchFld, searchVal);
        //                    if (advref.Count > 0)
        //                    {
        //                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
        //                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
        //                    }
        //                    else
        //                    {
        //                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
        //                    }
        //                }
        //                else
        //                {
        //                    List<MST_RECEIPT_SEARCH_HEAD> advref = CHNLSVC.CommonSearch.getReceiptEntries(company, userDefPro, d1, DateTime.Now, pgeNum, pgeSize, searchFld, searchVal);
        //                    if (advref.Count > 0)
        //                    {
        //                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
        //                        return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
        //                    }
        //                    else
        //                    {
        //                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                List<MST_RECEIPT_SEARCH_HEAD> advref = CHNLSVC.CommonSearch.getReceiptEntries(company, userDefPro, d1, DateTime.Now, pgeNum, pgeSize, searchFld, searchVal);
        //                if (advref.Count > 0)
        //                {
        //                    decimal totalDoc = Math.Ceiling(Convert.ToInt32(advref[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
        //                    return Json(new { success = true, login = true, data = advref, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
        //                }
        //                else
        //                {
        //                    return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
        //                }
        //            }


        //        }
        //        else
        //        {
        //            return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //        {
        //            return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //}

        //// Commented above and added below function by Chathura on 27-sep-2017
        public JsonResult getReceiptEntries(string unallow, string recTyp, string customer, string pgeNum, string pgeSize, string searchFld, string searchVal, string fromDate, string todate)
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
                    DateTime d1 = DateTime.Now;
                    d1 = d1.AddMonths(-1);
                    recTyp = recTyp.Trim();

                    if (fromDate == null || todate == null || fromDate == "" || todate == "")
                    {
                        fromDate = d1.ToString();
                        todate = DateTime.Now.ToString();
                    }

                    if (recTyp == "DEBT")
                    {
                        if (unallow == "true")
                        {
                            if (string.IsNullOrEmpty(customer))
                            {
                                return Json(new { success = false, login = true, msg = "Please select customer for get unallowcate receipt.", data = "" }, JsonRequestBehavior.AllowGet);

                            }
                            List<MST_RECEIPT_SEARCH_HEAD> advref = CHNLSVC.CommonSearch.getUnallowReceiptEntries(company, userDefPro, Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), customer,pgeNum, pgeSize, searchFld, searchVal);
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
                        else
                        {
                            List<MST_RECEIPT_SEARCH_HEAD> advref = CHNLSVC.CommonSearch.getReceiptEntries(company, userDefPro, Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), pgeNum, pgeSize, searchFld, searchVal);
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
                        List<MST_RECEIPT_SEARCH_HEAD> advref = CHNLSVC.CommonSearch.getReceiptEntries(company, userDefPro, Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), pgeNum, pgeSize, searchFld, searchVal);
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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getShipmentType(string pgeNum, string pgeSize, string searchFld, string searchVal)
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
                    List<TYPE_OF_SHIPMENT> advref = CHNLSVC.CommonSearch.getShipmentTypes(company, pgeNum, pgeSize, searchFld, searchVal);
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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getJobNoByPouch(string pgeNum, string pgeSize, string searchFld, string searchVal, string pouch)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                searchVal = searchVal.Trim();
                List<Job_No_Search> data = CHNLSVC.CommonSearch.getJobNoForPouch(pgeNum, pgeSize, searchFld, searchVal, company, pouch);
                if (data.Count > 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult loadcustomertype(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
                string userId = HttpContext.Session["UserId"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

                try
                {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<mst_bus_entity_tp> documents = CHNLSVC.General.getcustomertype(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (documents != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(documents[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = documents, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No data found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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

        public JsonResult getJobPouchSearchDateFiltered(string pgeNum, string pgeSize, string searchFld, string searchVal, string fromDate, string todate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            searchVal = searchVal.Trim();
            DateTime d1 = DateTime.Now;
            d1 = d1.AddMonths(-1);

            if (fromDate == null || todate == null || fromDate == "" || todate == "")
            {
                fromDate = d1.ToString();
                todate = DateTime.Now.ToString();
            }

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {


                    List<JOB_NUM_SEARCH> fielsds = CHNLSVC.CommonSearch.getJobPouchSearchDateFiltered(Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), pgeNum, pgeSize, searchFld, searchVal, company);

                    if (fielsds != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(fielsds[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = fielsds, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult GetJobNumberInClose(string pgeNum, string pgeSize, string searchFld, string searchVal, string fromDate, string todate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            searchVal = searchVal.Trim();
            DateTime d1 = DateTime.Now;
            d1 = d1.AddMonths(-1);

            if (fromDate == null || todate == null || fromDate == "" || todate == "")
            {
                fromDate = d1.ToString();
                todate = DateTime.Now.ToString();
            }

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<JOB_NUM_SEARCH> customers = CHNLSVC.CommonSearch.GetJobNumberInClose(Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), pgeNum, pgeSize, searchFld, searchVal, company, userDefPro);

                    if (customers != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(customers[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = customers, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult getInvoiceNoDateFiltered(string pgeNum, string pgeSize, string searchFld, string searchVal, string fromDate, string todate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            searchVal = searchVal.Trim();
            DateTime d1 = DateTime.Now;
            d1 = d1.AddMonths(-1);

            if (fromDate == null || todate == null || fromDate == "" || todate == "")
            {
                fromDate = d1.ToString();
                todate = DateTime.Now.ToString();
            }

            try
            {
                searchVal = searchVal.Trim();
                List<SEARCH_INVOICE> data = CHNLSVC.CommonSearch.getInvoiceNoDateFiltered(Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), pgeNum, pgeSize, searchFld, searchVal, company, userDefPro);
                if (data.Count > 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public JsonResult getInvoiceNoDf(string pgeNum, string pgeSize, string searchFld, string searchVal, string fromDate, string todate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            Int32 applvl = Convert.ToInt32(Session["Log_Autho"].ToString());

            searchVal = searchVal.Trim();
            DateTime d1 = DateTime.Now;
            d1 = d1.AddMonths(-1);

            if (fromDate == null || todate == null || fromDate == "" || todate == "")
            {
                fromDate = d1.ToString();
                todate = DateTime.Now.ToString();
            }

            try
            {
                searchVal = searchVal.Trim();
                if (applvl == 3)
                {
                    List<SEARCH_INVOICE> data = CHNLSVC.CommonSearch.getInvoiceNoDfNonPC(Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), pgeNum, pgeSize, searchFld, searchVal, company, userDefPro);
                    if (data.Count > 0)
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
                    List<SEARCH_INVOICE> data = CHNLSVC.CommonSearch.getInvoiceNoDf(Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), pgeNum, pgeSize, searchFld, searchVal, company, userDefPro);
                    if (data.Count > 0)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(data[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = data, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                    }
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
        public JsonResult getInvoiceNoDfApp(string pgeNum, string pgeSize, string searchFld, string searchVal, string fromDate, string todate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            Int32 applvl = Convert.ToInt32(Session["Log_Autho"].ToString());

            searchVal = searchVal.Trim();
            DateTime d1 = DateTime.Now;
            d1 = d1.AddMonths(-1);

            if (fromDate == null || todate == null || fromDate == "" || todate == "")
            {
                fromDate = d1.ToString();
                todate = DateTime.Now.ToString();
            }

            try
            {
                searchVal = searchVal.Trim();
                if (applvl == 3)
                {
                    List<SEARCH_INVOICE> data = CHNLSVC.CommonSearch.getInvoiceNoDfNonPCApp(Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), pgeNum, pgeSize, searchFld, searchVal, company, userDefPro);
                    if (data.Count > 0)
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
                    List<SEARCH_INVOICE> data = CHNLSVC.CommonSearch.getInvoiceNoDfApp(Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), pgeNum, pgeSize, searchFld, searchVal, company, userDefPro);
                    if (data.Count > 0)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(data[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = data, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                    }
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

        public JsonResult getInvoiceNoDfNew(string pgeNum, string pgeSize, string searchFld, string searchVal, string fromDate, string todate, string hbl)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            searchVal = searchVal.Trim();
            DateTime d1 = DateTime.Now;
            d1 = d1.AddMonths(-1);

            if (fromDate == null || todate == null || fromDate == "" || todate == "")
            {
                fromDate = d1.ToString();
                todate = DateTime.Now.ToString();
            }

            try
            {
                searchVal = searchVal.Trim();
                List<SEARCH_INVOICE> data = CHNLSVC.CommonSearch.getInvoiceNoDfNew(Convert.ToDateTime(fromDate), Convert.ToDateTime(todate), pgeNum, pgeSize, searchFld, searchVal, company, userDefPro,hbl);
                if (data.Count > 0)
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }


        public JsonResult loadpaymenttype(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<PAY_TP_SEARCH> documents = CHNLSVC.CommonSearch.getPaymentTypes(pgeNum, pgeSize, searchFld, searchVal);

                    if (documents != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(documents[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = documents, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No data found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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

        public JsonResult loadcreateuser(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<USER_SEARCH> documents = CHNLSVC.CommonSearch.getUserDetails(pgeNum, pgeSize, searchFld, searchVal, company);

                    if (documents != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(documents[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = documents, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No data found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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



        public JsonResult loadroleid(string companyId,string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_USERROLEID_SEARCH> documents = CHNLSVC.CommonSearch.getUserRoleID(pgeNum, pgeSize, searchFld, searchVal, companyId);

                    if (documents != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(documents[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = documents, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No data found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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


        public JsonResult getUserList( string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_USERS> documents = CHNLSVC.CommonSearch.getUserList(pgeNum, pgeSize, searchFld, searchVal);


                    if (documents != null) 
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(documents[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = documents, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No data found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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

        public JsonResult getDeptList(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_DEPARTMENT> documents = CHNLSVC.CommonSearch.getDeptList(pgeNum, pgeSize, searchFld, searchVal);


                    if (documents != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(documents[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = documents, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No data found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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

        public JsonResult getDesignationList(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_DESIGNATION> documents = CHNLSVC.CommonSearch.getDesignationtList(pgeNum, pgeSize, searchFld, searchVal);


                    if (documents != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(documents[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = documents, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No data found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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

        public JsonResult getCompanyList(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    searchVal = searchVal.Trim();
                    List<MST_COM_SEARCH> documents = CHNLSVC.CommonSearch.getCompanySet(pgeNum, pgeSize, searchFld, searchVal);


                    if (documents != null)
                    {
                        decimal totalDoc = Math.Ceiling(Convert.ToInt32(documents[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                        return Json(new { success = true, login = true, data = documents, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No data found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
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