using FF.BusinessObjects;
using FF.BusinessObjects.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.UI;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
//using Microsoft.Office.Interop.Excel;
using FF.BusinessObjects.ToursNew;
using System.Data;
using FF.Resources;

namespace FastForward.WebAbansTours.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(int? page, string searchString = "", string SearchField = "SysId")
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                DataTable dt = CHNLSVC.Tours.HOME_CONFIG_DATA(userId, company, userDefPro, "HOMEPAGE");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() == "Index")
                        {
                            if (SearchField == "") SearchField = "SysId";
                            int pageSize = 12;
                            int pageNumber = (page ?? 1);
                            string Status = "0,1,2,3,4,5,6,7,8,11,12,13";
                            SearchField = SearchField.Trim();
                            searchString = searchString.Trim();
                            if (SearchField == "" || SearchField == "")
                            {
                                try
                                {
                                    DateTime.Parse(searchString);
                                }
                                catch
                                {
                                    SearchField = "SysId";
                                    searchString = "";
                                }
                            }
                            List<GEN_CUST_ENQ> oItems = CHNLSVC.ComSearch.GET_TOURS_ENQ(company, userDefPro, pageNumber.ToString(), pageSize.ToString(), SearchField, searchString, Status);
                            ViewData["NumberOfItems"] = oItems.Count().ToString();
                            ViewData["ItemList"] = oItems;
                            int pagecount = (oItems.Count > 0) ? (oItems.FirstOrDefault().RESULT_COUNT / pageSize) + 1 : 0;
                            ViewBag.pagecount = pagecount;
                            ViewBag.Currentpage = pageNumber;

                            ST_MENU costing = CHNLSVC.Tours.getAcccessPermission(userId, 1021);
                            ST_MENU quotation = CHNLSVC.Tours.getAcccessPermission(userId, 1026);
                            ST_MENU invoice = CHNLSVC.Tours.getAcccessPermission(userId, 1019);
                            ViewBag.perCosting = costing.MNU_ID;
                            ViewBag.perQuotation = quotation.MNU_ID;
                            ViewBag.perInvoice = invoice.MNU_ID;

                            return View(oItems.OrderByDescending(x => x.GCE_STUS).ToPagedList(1, pageSize));
                        }
                        else
                        {
                            return RedirectToAction(dt.Rows[0][0].ToString());
                        }
                    }
                    else
                    {
                        if (SearchField == "") SearchField = "SysId";
                        int pageSize = 12;
                        int pageNumber = (page ?? 1);
                        string Status = "0,1,2,3,4,5,6,7,8,11,12,13";
                        SearchField = SearchField.Trim();
                        searchString = searchString.Trim();
                        if (SearchField == "" || SearchField == "")
                        {
                            try
                            {
                                DateTime.Parse(searchString);
                            }
                            catch
                            {
                                SearchField = "SysId";
                                searchString = "";
                            }
                        }
                        List<GEN_CUST_ENQ> oItems = CHNLSVC.ComSearch.GET_TOURS_ENQ(company, userDefPro, pageNumber.ToString(), pageSize.ToString(), SearchField, searchString, Status);
                        ViewData["NumberOfItems"] = oItems.Count().ToString();
                        ViewData["ItemList"] = oItems;
                        int pagecount = (oItems.Count > 0) ? (oItems.FirstOrDefault().RESULT_COUNT / pageSize) + 1 : 0;
                        ViewBag.pagecount = pagecount;
                        ViewBag.Currentpage = pageNumber;

                        ST_MENU costing = CHNLSVC.Tours.getAcccessPermission(userId, 1021);
                        ST_MENU quotation = CHNLSVC.Tours.getAcccessPermission(userId, 1026);
                        ST_MENU invoice = CHNLSVC.Tours.getAcccessPermission(userId, 1019);
                        ViewBag.perCosting = costing.MNU_ID;
                        ViewBag.perQuotation = quotation.MNU_ID;
                        ViewBag.perInvoice = invoice.MNU_ID;

                        return View(oItems.OrderByDescending(x => x.GCE_STUS).ToPagedList(1, pageSize));
                    }

                }
                else
                {
                    if (SearchField == "") SearchField = "SysId";
                    int pageSize = 12;
                    int pageNumber = (page ?? 1);
                    string Status = "0,1,2,3,4,5,6,7,8,11,12,13";
                    SearchField = SearchField.Trim();
                    searchString = searchString.Trim();
                    if (SearchField == "" || SearchField == "")
                    {
                        try
                        {
                            DateTime.Parse(searchString);
                        }
                        catch
                        {
                            SearchField = "SysId";
                            searchString = "";
                        }
                    }
                    List<GEN_CUST_ENQ> oItems = CHNLSVC.ComSearch.GET_TOURS_ENQ(company, userDefPro, pageNumber.ToString(), pageSize.ToString(), SearchField, searchString, Status);
                    ViewData["NumberOfItems"] = oItems.Count().ToString();
                    ViewData["ItemList"] = oItems;
                    int pagecount = (oItems.Count > 0) ? (oItems.FirstOrDefault().RESULT_COUNT / pageSize) + 1 : 0;
                    ViewBag.pagecount = pagecount;
                    ViewBag.Currentpage = pageNumber;

                    ST_MENU costing = CHNLSVC.Tours.getAcccessPermission(userId, 1021);
                    ST_MENU quotation = CHNLSVC.Tours.getAcccessPermission(userId, 1026);
                    ST_MENU invoice = CHNLSVC.Tours.getAcccessPermission(userId, 1019);
                    ViewBag.perCosting = costing.MNU_ID;
                    ViewBag.perQuotation = quotation.MNU_ID;
                    ViewBag.perInvoice = invoice.MNU_ID;

                    return View(oItems.OrderByDescending(x => x.GCE_STUS).ToPagedList(1, pageSize));
                }

                //if (dt.Rows[0][0].ToString() == "Index" || )
                //{

                //}
                //else
                //{
                //    return RedirectToAction(dt.Rows[0][0].ToString());
                //}




            }
            else
            {
                return Redirect("~/Login/index");
            }
        }

        //home page 2

        public ActionResult Index2(int? page, string searchString = "", string SearchField = "SysId")
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                DataTable dt = CHNLSVC.Tours.HOME_CONFIG_DATA(userId, company, userDefPro, "HOMEPAGE");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() == "Index2")
                        {
                            if (SearchField == "") SearchField = "SysId";
                            int pageSize = 12;
                            int pageNumber = (page ?? 1);
                            string Status = "0,1,2,3,4,5,6,7,8,11,12,13";
                            SearchField = SearchField.Trim();
                            searchString = searchString.Trim();
                            if (SearchField == "" || SearchField == "")
                            {
                                try
                                {
                                    DateTime.Parse(searchString);
                                }
                                catch
                                {
                                    SearchField = "SysId";
                                    searchString = "";
                                }
                            }
                            List<DriverAllocationHome> oItems = CHNLSVC.Tours.GET_TOURS_DRIALLOC(company, userDefPro, pageNumber.ToString(), pageSize.ToString(), SearchField, searchString, Status);
                            ViewData["NumberOfItems"] = oItems.Count().ToString();
                            ViewData["ItemList"] = oItems;
                            int pagecount = (oItems.Count > 0) ? (oItems.FirstOrDefault().RESULT_COUNT / pageSize) + 1 : 0;
                            ViewBag.pagecount = pagecount;
                            ViewBag.Currentpage = pageNumber;

                            ST_MENU costing = CHNLSVC.Tours.getAcccessPermission(userId, 1021);
                            ST_MENU quotation = CHNLSVC.Tours.getAcccessPermission(userId, 1026);
                            ST_MENU invoice = CHNLSVC.Tours.getAcccessPermission(userId, 1019);
                            ViewBag.perCosting = costing.MNU_ID;
                            ViewBag.perQuotation = quotation.MNU_ID;
                            ViewBag.perInvoice = invoice.MNU_ID;
                            ViewBag.Regexp = "";
                            ViewBag.LisExp = "";

                            //Alert
                            List<FleetAlert> fleetalreg = CHNLSVC.Tours.FleetAlertdata(DateTime.Now.Date, "REG");
                            List<FleetAlert> fleetallis = CHNLSVC.Tours.FleetAlertdata(DateTime.Now.Date, "LIS");

                            if (fleetalreg.Count > 0)
                            {
                                ViewBag.Regexp = "true";
                            }
                            if (fleetallis.Count > 0)
                            {
                                ViewBag.LisExp = "true";
                            }


                            return View(oItems.OrderByDescending(x => x.gce_enq_id).ToPagedList(1, pageSize));
                        }
                        else if (dt.Rows[0][0].ToString() == "Index")
                        {
                            if (SearchField == "") SearchField = "SysId";
                            int pageSize = 12;
                            int pageNumber = (page ?? 1);
                            string Status = "0,1,2,3,4,5,6,7,8,11,12,13";
                            SearchField = SearchField.Trim();
                            searchString = searchString.Trim();
                            if (SearchField == "" || SearchField == "")
                            {
                                try
                                {
                                    DateTime.Parse(searchString);
                                }
                                catch
                                {
                                    SearchField = "SysId";
                                    searchString = "";
                                }
                            }
                            List<GEN_CUST_ENQ> oItems = CHNLSVC.ComSearch.GET_TOURS_ENQ(company, userDefPro, pageNumber.ToString(), pageSize.ToString(), SearchField, searchString, Status);
                            ViewData["NumberOfItems"] = oItems.Count().ToString();
                            ViewData["ItemList"] = oItems;
                            int pagecount = (oItems.Count > 0) ? (oItems.FirstOrDefault().RESULT_COUNT / pageSize) + 1 : 0;
                            ViewBag.pagecount = pagecount;
                            ViewBag.Currentpage = pageNumber;

                            ST_MENU costing = CHNLSVC.Tours.getAcccessPermission(userId, 1021);
                            ST_MENU quotation = CHNLSVC.Tours.getAcccessPermission(userId, 1026);
                            ST_MENU invoice = CHNLSVC.Tours.getAcccessPermission(userId, 1019);
                            ViewBag.perCosting = costing.MNU_ID;
                            ViewBag.perQuotation = quotation.MNU_ID;
                            ViewBag.perInvoice = invoice.MNU_ID;

                          //  return View(oItems.OrderByDescending(x => x.GCE_STUS).ToPagedList(1, pageSize));
                            return RedirectToAction(dt.Rows[0][0].ToString());
                        }
                        else
                        {
                            if (SearchField == "") SearchField = "SysId";
                            int pageSize = 12;
                            int pageNumber = (page ?? 1);
                            string Status = "0,1,2,3,4,5,6,7,8,11,12,13";
                            SearchField = SearchField.Trim();
                            searchString = searchString.Trim();
                            if (SearchField == "" || SearchField == "")
                            {
                                try
                                {
                                    DateTime.Parse(searchString);
                                }
                                catch
                                {
                                    SearchField = "SysId";
                                    searchString = "";
                                }
                            }
                            List<DriverAllocationHome> oItems = CHNLSVC.Tours.GET_TOURS_DRIALLOC(company, userDefPro, pageNumber.ToString(), pageSize.ToString(), SearchField, searchString, Status);
                            ViewData["NumberOfItems"] = oItems.Count().ToString();
                            ViewData["ItemList"] = oItems;
                            int pagecount = (oItems.Count > 0) ? (oItems.FirstOrDefault().RESULT_COUNT / pageSize) + 1 : 0;
                            ViewBag.pagecount = pagecount;
                            ViewBag.Currentpage = pageNumber;

                            ST_MENU costing = CHNLSVC.Tours.getAcccessPermission(userId, 1021);
                            ST_MENU quotation = CHNLSVC.Tours.getAcccessPermission(userId, 1026);
                            ST_MENU invoice = CHNLSVC.Tours.getAcccessPermission(userId, 1019);
                            ViewBag.perCosting = costing.MNU_ID;
                            ViewBag.perQuotation = quotation.MNU_ID;
                            ViewBag.perInvoice = invoice.MNU_ID;
                            ViewBag.Regexp = "";
                            ViewBag.LisExp = "";

                            //Alert
                            List<FleetAlert> fleetalreg = CHNLSVC.Tours.FleetAlertdata(DateTime.Now.Date, "REG");
                            List<FleetAlert> fleetallis = CHNLSVC.Tours.FleetAlertdata(DateTime.Now.Date, "LIS");

                            if (fleetalreg.Count > 0)
                            {
                                ViewBag.Regexp = "true";
                            }
                            if (fleetallis.Count > 0)
                            {
                                ViewBag.LisExp = "true";
                            }


                            return View(oItems.OrderByDescending(x => x.gce_enq_id).ToPagedList(1, pageSize));
                        }
                    }
                }

                if (SearchField == "") SearchField = "SysId";
                int pageSize1 = 12;
                int pageNumber1 = (page ?? 1);
                string Status1 = "0,1,2,3,4,5,6,7,8,11,12,13";
                SearchField = SearchField.Trim();
                searchString = searchString.Trim();
                if (SearchField == "" || SearchField == "")
                {
                    try
                    {
                        DateTime.Parse(searchString);
                    }
                    catch
                    {
                        SearchField = "SysId";
                        searchString = "";
                    }
                }
                List<DriverAllocationHome> oItems1 = CHNLSVC.Tours.GET_TOURS_DRIALLOC(company, userDefPro, pageNumber1.ToString(), pageSize1.ToString(), SearchField, searchString, Status1);
                ViewData["NumberOfItems"] = oItems1.Count().ToString();
                ViewData["ItemList"] = oItems1;
                int pagecount1 = (oItems1.Count > 0) ? (oItems1.FirstOrDefault().RESULT_COUNT / pageSize1) + 1 : 0;
                ViewBag.pagecount = pagecount1;
                ViewBag.Currentpage = pageNumber1;

                ST_MENU costing1 = CHNLSVC.Tours.getAcccessPermission(userId, 1021);
                ST_MENU quotation1 = CHNLSVC.Tours.getAcccessPermission(userId, 1026);
                ST_MENU invoice1 = CHNLSVC.Tours.getAcccessPermission(userId, 1019);
                ViewBag.perCosting = costing1.MNU_ID;
                ViewBag.perQuotation = quotation1.MNU_ID;
                ViewBag.perInvoice = invoice1.MNU_ID;
                ViewBag.Regexp = "";
                ViewBag.LisExp = "";

                //Alert
                List<FleetAlert> fleetalreg1 = CHNLSVC.Tours.FleetAlertdata(DateTime.Now.Date, "REG");
                List<FleetAlert> fleetallis1 = CHNLSVC.Tours.FleetAlertdata(DateTime.Now.Date, "LIS");

                if (fleetalreg1.Count > 0)
                {
                    ViewBag.Regexp = "true";
                }
                if (fleetallis1.Count > 0)
                {
                    ViewBag.LisExp = "true";
                }


                return View(oItems1.OrderByDescending(x => x.gce_enq_id).ToPagedList(1, pageSize1));

            }
            else
            {
                return Redirect("~/Login/index");
            }
        }

        public JsonResult GetEnqSerData(string enqid)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<GEN_CUST_ENQSER> invdata = CHNLSVC.Tours.GetEnqSerData(enqid);
                return Json(new { success = true, login = true, data = invdata }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SetEnqId(string enqid)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                return Json(new { success = true, login = true, data = enqid }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ChangeProfitCenter(string pc)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                if (pc != "")
                {
                    Session["UserDefProf"] = pc;
                    MasterProfitCenter proCenDet = CHNLSVC.Sales.GetProfitCenter(company, pc);
                    Session["Title"] = proCenDet.Mpc_oth_ref;
                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "Please select valid profit center.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }




        //public void Aggrement()
        //{
        //    // Create a PDF document
        //    PDFDocument doc = new PDFDocument();
        //    doc.WriteText("Bank of Money", 1, 1);
        //    doc.WriteText("---------------------------------", 1, 1.1f);

        //    // Render text
        //    PDFFont fntCourier = new PDFFont(StdType1Font.Courier, 16);

        //    doc.WriteText("Payee account #   :", fntCourier, 1, 1.4f);
        //    doc.WriteText("Payee name        :", fntCourier, 1, 1.7f);
        //    doc.WriteText("Amount transferred:", fntCourier, 1, 2f);
        //    doc.WriteText("Transaction #     :", fntCourier, 1, 2.3f);
        //    doc.WriteText("Date and Time #   :", fntCourier, 1, 2.6f);

        //    doc.WriteText("11111", fntCourier, 4, 1.4f);
        //    doc.WriteText("22222", fntCourier, 4, 1.7f);
        //    doc.WriteText("Rs. " + 56, fntCourier, 4, 2f);


        //    // Write the document to the browser
        //    this.Response.Clear();
        //    this.Response.ContentType = "application/pdf";
        //    Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
        //    File(this.Response.OutputStream, "application/pdf");
        //    doc.Save(this.Response.OutputStream);
        //    doc.Close();


        //}

        public ActionResult Aggrement(string EnqID)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                DataTable AgrementValues = new DataTable();
                DataTable CheckInOutData = new DataTable();
                DataTable param = new DataTable();
                if (EnqID != "")
                {
                    GEN_CUST_ENQ oItem = CHNLSVC.Tours.getEnquiryDetailsTours(company, userDefPro, EnqID);

                    if (oItem.GCE_ENQ_ID != null)
                    {
                        AgrementValues = AgrementData(oItem);
                        if (AgrementValues.Rows.Count > 0)
                        {
                            MST_CHKINOUT chkOutData = CHNLSVC.Tours.getEnqChkData(EnqID);
                            CheckInOutData = getCheckInOutDataTable(chkOutData);
                        }

                        param = getOtherEnquiryData(oItem);
                    }

                }


                DataTable dt = CHNLSVC.Tours.HOME_CONFIG_DATA(userId, company, userDefPro, "AGGRMNT");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ReportDocument rd = new ReportDocument();
                        rd.Load(Server.MapPath("/Reports/" + dt.Rows[0][0].ToString()));
                        rd.Database.Tables["AgrementData"].SetDataSource(AgrementValues);
                        rd.Database.Tables["CheckData"].SetDataSource(CheckInOutData);
                        rd.Database.Tables["param"].SetDataSource(param);
                        List<ST_ENQ_CHARGES> chargData = CHNLSVC.Tours.tempEnquiryCharges(EnqID);
                        decimal addRateAmt = 0;
                        decimal taxPer = 0;
                        foreach (ST_ENQ_CHARGES chg in chargData)
                        {
                            if (chg.SCH_ITM_SERVICE == "T")
                            {
                                SR_TRANS_CHA oSCHARGE = CHNLSVC.Tours.GetTransferChargeDetailsByCode(company, "TRANS", chg.SCH_ITM_CD, userDefPro);
                                if (oSCHARGE.STC_AD_RT > addRateAmt)
                                {
                                    addRateAmt = oSCHARGE.STC_AD_RT;
                                }
                                if (oSCHARGE.STC_TAX_RT > taxPer)
                                {
                                    taxPer = oSCHARGE.STC_TAX_RT;
                                }
                            }
                            else
                            {
                                SR_SER_MISS oSR_SER_MISS = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, "MSCELNS", chg.SCH_ITM_CD, userDefPro);
                                if (oSR_SER_MISS.SSM_TAX_RT > taxPer)
                                {
                                    taxPer = oSR_SER_MISS.SSM_TAX_RT;
                                }
                            }
                        }
                        DataTable add = new DataTable();
                        add.Columns.Add("extraKmRate", typeof(decimal));
                        add.Columns.Add("Tax", typeof(decimal));
                        add.Rows.Add(addRateAmt, taxPer);
                        rd.Database.Tables["Additional"].SetDataSource(add);

                        DataTable transDta = getTransChageDatable(chargData, "T");
                        //rd.Database.Tables["TransCharge"].SetDataSource(transDta);

                        DataTable OtherCharge = getTransChageDatable(chargData, "O");
                        //rd.Database.Tables["OtherCharge"].SetDataSource(OtherCharge);

                        foreach (object repOp in rd.ReportDefinition.ReportObjects)
                        {
                            string _s = repOp.GetType().ToString();
                            if (_s.ToLower().Contains("subreport"))
                            {
                                SubreportObject _cs = (SubreportObject)repOp;
                                if (_cs.SubreportName == "rptTransCharge")
                                {
                                    ReportDocument subRepDoc = rd.Subreports[_cs.SubreportName];
                                    subRepDoc.Database.Tables["TransCharge"].SetDataSource(transDta);
                                }
                                if (_cs.SubreportName == "rptOthCharge")
                                {
                                    ReportDocument subRepDoc = rd.Subreports[_cs.SubreportName];
                                    subRepDoc.Database.Tables["OtherCharge"].SetDataSource(OtherCharge);
                                }
                            }
                        }


                        //List<GEN_CUST_ENQSER> invdata = CHNLSVC.Tours.GetEnqSerData(EnqID);
                        //string Status = "0,1,2,3,4,5,6,7,8";
                        //string type = "TNSPT";
                        //List<GEN_CUST_ENQ> oItems = CHNLSVC.Tours.SP_TOUR_GET_TRANSPORT_ENQRY(company, userDefPro, Status, type, userId, 15001);
                        //List<GEN_CUST_ENQ> oItemsnew = oItems.Where(a => a.GCE_ENQ_ID == EnqID).ToList();
                        //string searchVal = oItems.FirstOrDefault().GCE_DRIVER;
                        //MST_EMPLOYEE_NEW_TBS employees = CHNLSVC.Tours.getMstEmployeeDetails(searchVal);
                        //MST_CHKINOUT chkOut = CHNLSVC.Tours.getEnqChkData(EnqID);
                        //List<InvoiceHeader> InvData3 = CHNLSVC.Tours.GetInvoiceData(EnqID);
                        //var InvNo = InvData3.Max(a => a.Sah_inv_no);
                        //List<InvoiceItem> Invdata4 = CHNLSVC.Sales.GetInvoiceHeaderDetailsList(InvNo).ToList();
                        //List<MST_CHKINOUT> chdate = new List<MST_CHKINOUT>();
                        //List<InvoiceItem> param = new List<InvoiceItem>();
                        //InvoiceItem paramob = new InvoiceItem();
                        //chdate.Add(chkOut);
                        //ReportDocument rd = new ReportDocument();
                        //rd.Load(Server.MapPath("/Reports/" +  "Aggrement.rpt"));
                        ////rd.Database.Tables["EnqData"].SetDataSource(oItemsnew);
                        ////rd.Database.Tables["CheckinData"].SetDataSource(chdate);
                        ////rd.Database.Tables["InvData"].SetDataSource(Invdata4);
                        ////rd.Database.Tables["InvHdrData"].SetDataSource(InvData3);
                        //decimal total = 0;
                        //decimal taxx = 0;
                        //string itemcode = "";
                        //decimal aacharge = 4500;
                        //decimal epxkm = 0;
                        //foreach (var invitems in Invdata4)
                        //{
                        //    decimal withouttotal = invitems.Sad_tot_amt;
                        //    decimal tax = invitems.Sad_itm_tax_amt;
                        //    total = total + withouttotal;
                        //    taxx = taxx + tax;
                        //    itemcode = invitems.Sad_itm_cd;
                        //    SR_TRANS_CHA TrnsChargeCode = CHNLSVC.Tours.GetTransferChargeDetailsByCode(company, "TRANS", itemcode, userDefPro);
                        //    epxkm = TrnsChargeCode.STC_TO_KM;
                        //}

                        //List<RecieptItem> recipt = CHNLSVC.Tours.getReceiptItemList(InvNo).ToList();
                        //decimal payrecipt = recipt.Sum(x => x.Sard_settle_amt);
                        //List<RecieptHeader> deposithr = CHNLSVC.Tours.GET_RECIEPT_BY_ENQ(company, userDefPro, EnqID);
                        //decimal deposittotal = deposithr.Sum(y => y.Sar_tot_settle_amt);
                        //paramob.Sad_tot_amt = total;
                        //paramob.Sad_itm_tax_amt = taxx;
                        //paramob.Sad_do_qty = aacharge;
                        //paramob.Sad_itm_cd = itemcode;
                        //paramob.Sad_qty = epxkm - chdate.FirstOrDefault().CHK_IN_KM;
                        //paramob.Sad_fws_ignore_qty = payrecipt;
                        //paramob.Sad_srn_qty = deposittotal;
                        //if (employees != null) paramob.Mi_anal1 = employees.MEMP_FIRST_NAME.ToString();

                        //param.Add(paramob);
                        //rd.Database.Tables["Param"].SetDataSource(param);
                        //  rd.SetDataSource(invdata);
                        Response.Buffer = false;
                        Response.ClearContent();
                        Response.ClearHeaders();
                        try
                        {
                            this.Response.Clear();
                            this.Response.ContentType = "application/pdf";
                            Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
                            return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                            //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            //stream.Seek(0, SeekOrigin.Begin);
                            //return File(stream, "application/pdf", "EverestList.pdf");
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                    else
                    {
                        ReportDocument rd = new ReportDocument();
                        rd.Load(Server.MapPath("/Reports/" + "Aggrement.rpt"));
                        rd.Database.Tables["AgrementData"].SetDataSource(AgrementValues);
                        rd.Database.Tables["CheckData"].SetDataSource(CheckInOutData);
                        rd.Database.Tables["param"].SetDataSource(param);
                        List<ST_ENQ_CHARGES> chargData = CHNLSVC.Tours.tempEnquiryCharges(EnqID);
                        decimal addRateAmt = 0;
                        decimal taxPer = 0;
                        foreach (ST_ENQ_CHARGES chg in chargData)
                        {
                            if (chg.SCH_ITM_SERVICE == "T")
                            {
                                SR_TRANS_CHA oSCHARGE = CHNLSVC.Tours.GetTransferChargeDetailsByCode(company, "TRANS", chg.SCH_ITM_CD, userDefPro);
                                if (oSCHARGE.STC_AD_RT > addRateAmt)
                                {
                                    addRateAmt = oSCHARGE.STC_AD_RT;
                                }
                                if (oSCHARGE.STC_TAX_RT > taxPer)
                                {
                                    taxPer = oSCHARGE.STC_TAX_RT;
                                }
                            }
                            else
                            {
                                SR_SER_MISS oSR_SER_MISS = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, "MSCELNS", chg.SCH_ITM_CD, userDefPro);
                                if (oSR_SER_MISS.SSM_TAX_RT > taxPer)
                                {
                                    taxPer = oSR_SER_MISS.SSM_TAX_RT;
                                }
                            }
                        }
                        DataTable add = new DataTable();
                        add.Columns.Add("extraKmRate", typeof(decimal));
                        add.Columns.Add("Tax", typeof(decimal));
                        add.Rows.Add(addRateAmt, taxPer);
                        rd.Database.Tables["Additional"].SetDataSource(add);

                        DataTable transDta = getTransChageDatable(chargData, "T");
                        //rd.Database.Tables["TransCharge"].SetDataSource(transDta);

                        DataTable OtherCharge = getTransChageDatable(chargData, "O");
                        //rd.Database.Tables["OtherCharge"].SetDataSource(OtherCharge);

                        foreach (object repOp in rd.ReportDefinition.ReportObjects)
                        {
                            string _s = repOp.GetType().ToString();
                            if (_s.ToLower().Contains("subreport"))
                            {
                                SubreportObject _cs = (SubreportObject)repOp;
                                if (_cs.SubreportName == "rptTransCharge")
                                {
                                    ReportDocument subRepDoc = rd.Subreports[_cs.SubreportName];
                                    subRepDoc.Database.Tables["TransCharge"].SetDataSource(transDta);
                                }
                                if (_cs.SubreportName == "rptOthCharge")
                                {
                                    ReportDocument subRepDoc = rd.Subreports[_cs.SubreportName];
                                    subRepDoc.Database.Tables["OtherCharge"].SetDataSource(OtherCharge);
                                }
                            }
                        }


                        //List<GEN_CUST_ENQSER> invdata = CHNLSVC.Tours.GetEnqSerData(EnqID);
                        //string Status = "0,1,2,3,4,5,6,7,8";
                        //string type = "TNSPT";
                        //List<GEN_CUST_ENQ> oItems = CHNLSVC.Tours.SP_TOUR_GET_TRANSPORT_ENQRY(company, userDefPro, Status, type, userId, 15001);
                        //List<GEN_CUST_ENQ> oItemsnew = oItems.Where(a => a.GCE_ENQ_ID == EnqID).ToList();
                        //string searchVal = oItems.FirstOrDefault().GCE_DRIVER;
                        //MST_EMPLOYEE_NEW_TBS employees = CHNLSVC.Tours.getMstEmployeeDetails(searchVal);
                        //MST_CHKINOUT chkOut = CHNLSVC.Tours.getEnqChkData(EnqID);
                        //List<InvoiceHeader> InvData3 = CHNLSVC.Tours.GetInvoiceData(EnqID);
                        //var InvNo = InvData3.Max(a => a.Sah_inv_no);
                        //List<InvoiceItem> Invdata4 = CHNLSVC.Sales.GetInvoiceHeaderDetailsList(InvNo).ToList();
                        //List<MST_CHKINOUT> chdate = new List<MST_CHKINOUT>();
                        //List<InvoiceItem> param = new List<InvoiceItem>();
                        //InvoiceItem paramob = new InvoiceItem();
                        //chdate.Add(chkOut);
                        //ReportDocument rd = new ReportDocument();
                        //rd.Load(Server.MapPath("/Reports/" +  "Aggrement.rpt"));
                        ////rd.Database.Tables["EnqData"].SetDataSource(oItemsnew);
                        ////rd.Database.Tables["CheckinData"].SetDataSource(chdate);
                        ////rd.Database.Tables["InvData"].SetDataSource(Invdata4);
                        ////rd.Database.Tables["InvHdrData"].SetDataSource(InvData3);
                        //decimal total = 0;
                        //decimal taxx = 0;
                        //string itemcode = "";
                        //decimal aacharge = 4500;
                        //decimal epxkm = 0;
                        //foreach (var invitems in Invdata4)
                        //{
                        //    decimal withouttotal = invitems.Sad_tot_amt;
                        //    decimal tax = invitems.Sad_itm_tax_amt;
                        //    total = total + withouttotal;
                        //    taxx = taxx + tax;
                        //    itemcode = invitems.Sad_itm_cd;
                        //    SR_TRANS_CHA TrnsChargeCode = CHNLSVC.Tours.GetTransferChargeDetailsByCode(company, "TRANS", itemcode, userDefPro);
                        //    epxkm = TrnsChargeCode.STC_TO_KM;
                        //}

                        //List<RecieptItem> recipt = CHNLSVC.Tours.getReceiptItemList(InvNo).ToList();
                        //decimal payrecipt = recipt.Sum(x => x.Sard_settle_amt);
                        //List<RecieptHeader> deposithr = CHNLSVC.Tours.GET_RECIEPT_BY_ENQ(company, userDefPro, EnqID);
                        //decimal deposittotal = deposithr.Sum(y => y.Sar_tot_settle_amt);
                        //paramob.Sad_tot_amt = total;
                        //paramob.Sad_itm_tax_amt = taxx;
                        //paramob.Sad_do_qty = aacharge;
                        //paramob.Sad_itm_cd = itemcode;
                        //paramob.Sad_qty = epxkm - chdate.FirstOrDefault().CHK_IN_KM;
                        //paramob.Sad_fws_ignore_qty = payrecipt;
                        //paramob.Sad_srn_qty = deposittotal;
                        //if (employees != null) paramob.Mi_anal1 = employees.MEMP_FIRST_NAME.ToString();

                        //param.Add(paramob);
                        //rd.Database.Tables["Param"].SetDataSource(param);
                        //  rd.SetDataSource(invdata);
                        Response.Buffer = false;
                        Response.ClearContent();
                        Response.ClearHeaders();
                        try
                        {
                            this.Response.Clear();
                            this.Response.ContentType = "application/pdf";
                            Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
                            return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                            //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            //stream.Seek(0, SeekOrigin.Begin);
                            //return File(stream, "application/pdf", "EverestList.pdf");
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }

                }
                else
                {
                    ReportDocument rd = new ReportDocument();
                    rd.Load(Server.MapPath("/Reports/" + "Aggrement.rpt"));
                    rd.Database.Tables["AgrementData"].SetDataSource(AgrementValues);
                    rd.Database.Tables["CheckData"].SetDataSource(CheckInOutData);
                    rd.Database.Tables["param"].SetDataSource(param);
                    List<ST_ENQ_CHARGES> chargData = CHNLSVC.Tours.tempEnquiryCharges(EnqID);
                    decimal addRateAmt = 0;
                    decimal taxPer = 0;
                    foreach (ST_ENQ_CHARGES chg in chargData)
                    {
                        if (chg.SCH_ITM_SERVICE == "T")
                        {
                            SR_TRANS_CHA oSCHARGE = CHNLSVC.Tours.GetTransferChargeDetailsByCode(company, "TRANS", chg.SCH_ITM_CD, userDefPro);
                            if (oSCHARGE.STC_AD_RT > addRateAmt)
                            {
                                addRateAmt = oSCHARGE.STC_AD_RT;
                            }
                            if (oSCHARGE.STC_TAX_RT > taxPer)
                            {
                                taxPer = oSCHARGE.STC_TAX_RT;
                            }
                        }
                        else
                        {
                            SR_SER_MISS oSR_SER_MISS = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, "MSCELNS", chg.SCH_ITM_CD, userDefPro);
                            if (oSR_SER_MISS.SSM_TAX_RT > taxPer)
                            {
                                taxPer = oSR_SER_MISS.SSM_TAX_RT;
                            }
                        }
                    }
                    DataTable add = new DataTable();
                    add.Columns.Add("extraKmRate", typeof(decimal));
                    add.Columns.Add("Tax", typeof(decimal));
                    add.Rows.Add(addRateAmt, taxPer);
                    rd.Database.Tables["Additional"].SetDataSource(add);

                    DataTable transDta = getTransChageDatable(chargData, "T");
                    //rd.Database.Tables["TransCharge"].SetDataSource(transDta);

                    DataTable OtherCharge = getTransChageDatable(chargData, "O");
                    //rd.Database.Tables["OtherCharge"].SetDataSource(OtherCharge);

                    foreach (object repOp in rd.ReportDefinition.ReportObjects)
                    {
                        string _s = repOp.GetType().ToString();
                        if (_s.ToLower().Contains("subreport"))
                        {
                            SubreportObject _cs = (SubreportObject)repOp;
                            if (_cs.SubreportName == "rptTransCharge")
                            {
                                ReportDocument subRepDoc = rd.Subreports[_cs.SubreportName];
                                subRepDoc.Database.Tables["TransCharge"].SetDataSource(transDta);
                            }
                            if (_cs.SubreportName == "rptOthCharge")
                            {
                                ReportDocument subRepDoc = rd.Subreports[_cs.SubreportName];
                                subRepDoc.Database.Tables["OtherCharge"].SetDataSource(OtherCharge);
                            }
                        }
                    }


                    //List<GEN_CUST_ENQSER> invdata = CHNLSVC.Tours.GetEnqSerData(EnqID);
                    //string Status = "0,1,2,3,4,5,6,7,8";
                    //string type = "TNSPT";
                    //List<GEN_CUST_ENQ> oItems = CHNLSVC.Tours.SP_TOUR_GET_TRANSPORT_ENQRY(company, userDefPro, Status, type, userId, 15001);
                    //List<GEN_CUST_ENQ> oItemsnew = oItems.Where(a => a.GCE_ENQ_ID == EnqID).ToList();
                    //string searchVal = oItems.FirstOrDefault().GCE_DRIVER;
                    //MST_EMPLOYEE_NEW_TBS employees = CHNLSVC.Tours.getMstEmployeeDetails(searchVal);
                    //MST_CHKINOUT chkOut = CHNLSVC.Tours.getEnqChkData(EnqID);
                    //List<InvoiceHeader> InvData3 = CHNLSVC.Tours.GetInvoiceData(EnqID);
                    //var InvNo = InvData3.Max(a => a.Sah_inv_no);
                    //List<InvoiceItem> Invdata4 = CHNLSVC.Sales.GetInvoiceHeaderDetailsList(InvNo).ToList();
                    //List<MST_CHKINOUT> chdate = new List<MST_CHKINOUT>();
                    //List<InvoiceItem> param = new List<InvoiceItem>();
                    //InvoiceItem paramob = new InvoiceItem();
                    //chdate.Add(chkOut);
                    //ReportDocument rd = new ReportDocument();
                    //rd.Load(Server.MapPath("/Reports/" +  "Aggrement.rpt"));
                    ////rd.Database.Tables["EnqData"].SetDataSource(oItemsnew);
                    ////rd.Database.Tables["CheckinData"].SetDataSource(chdate);
                    ////rd.Database.Tables["InvData"].SetDataSource(Invdata4);
                    ////rd.Database.Tables["InvHdrData"].SetDataSource(InvData3);
                    //decimal total = 0;
                    //decimal taxx = 0;
                    //string itemcode = "";
                    //decimal aacharge = 4500;
                    //decimal epxkm = 0;
                    //foreach (var invitems in Invdata4)
                    //{
                    //    decimal withouttotal = invitems.Sad_tot_amt;
                    //    decimal tax = invitems.Sad_itm_tax_amt;
                    //    total = total + withouttotal;
                    //    taxx = taxx + tax;
                    //    itemcode = invitems.Sad_itm_cd;
                    //    SR_TRANS_CHA TrnsChargeCode = CHNLSVC.Tours.GetTransferChargeDetailsByCode(company, "TRANS", itemcode, userDefPro);
                    //    epxkm = TrnsChargeCode.STC_TO_KM;
                    //}

                    //List<RecieptItem> recipt = CHNLSVC.Tours.getReceiptItemList(InvNo).ToList();
                    //decimal payrecipt = recipt.Sum(x => x.Sard_settle_amt);
                    //List<RecieptHeader> deposithr = CHNLSVC.Tours.GET_RECIEPT_BY_ENQ(company, userDefPro, EnqID);
                    //decimal deposittotal = deposithr.Sum(y => y.Sar_tot_settle_amt);
                    //paramob.Sad_tot_amt = total;
                    //paramob.Sad_itm_tax_amt = taxx;
                    //paramob.Sad_do_qty = aacharge;
                    //paramob.Sad_itm_cd = itemcode;
                    //paramob.Sad_qty = epxkm - chdate.FirstOrDefault().CHK_IN_KM;
                    //paramob.Sad_fws_ignore_qty = payrecipt;
                    //paramob.Sad_srn_qty = deposittotal;
                    //if (employees != null) paramob.Mi_anal1 = employees.MEMP_FIRST_NAME.ToString();

                    //param.Add(paramob);
                    //rd.Database.Tables["Param"].SetDataSource(param);
                    //  rd.SetDataSource(invdata);
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        this.Response.Clear();
                        this.Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
                        return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                        //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        //stream.Seek(0, SeekOrigin.Begin);
                        //return File(stream, "application/pdf", "EverestList.pdf");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }


            }
            else
            {
                return Redirect("~/Login");
            }
        }



        public DataTable AgrementData(GEN_CUST_ENQ oItem)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                string userName = HttpContext.Session["UserName"] as string;

                DataTable AgrementData = new DataTable();
                AgrementData.Columns.Add("ReservationNo", typeof(String));
                AgrementData.Columns.Add("RentalAgentNo", typeof(String));
                AgrementData.Columns.Add("CheckOutStation", typeof(String));
                AgrementData.Columns.Add("CheckOutDt", typeof(DateTime));
                AgrementData.Columns.Add("CheckInStation", typeof(String));
                AgrementData.Columns.Add("CheckInDate", typeof(DateTime));
                AgrementData.Columns.Add("VehicalModel/RegNo", typeof(String));
                AgrementData.Columns.Add("CarCategory", typeof(String));
                AgrementData.Columns.Add("RefurenceNumber", typeof(String));
                AgrementData.Columns.Add("DriverName", typeof(String));
                AgrementData.Columns.Add("TaxValue", typeof(decimal));
                AgrementData.Columns.Add("TotalPayment", typeof(decimal));
                AgrementData.Columns.Add("TotalDue", typeof(decimal));
                AgrementData.Columns.Add("CreateBy", typeof(String));
                AgrementData.Columns.Add("PaymentWithTax", typeof(decimal));
                AgrementData.Columns.Add("TaxAmount", typeof(decimal));
                AgrementData.Columns.Add("totalAmountWithoutTax", typeof(decimal));

                MST_EMPLOYEE_NEW_TBS employees = new MST_EMPLOYEE_NEW_TBS();
                string frmTown = "";
                string toTown = "";
                string driName = "";
                if (oItem.GCE_DRIVER != null)
                {
                    employees = CHNLSVC.Tours.getMstEmployeeDetails(oItem.GCE_DRIVER);
                    driName = (employees != null) ? employees.MEMP_FIRST_NAME : "";
                }
                List<MST_FAC_LOC> facList = CHNLSVC.Tours.getFacLocations(company, userDefPro);
                if (facList.Count > 0)
                {
                    bool exists = facList.Exists(element => element.FAC_CODE == oItem.GCE_FRM_TN);
                    if (exists)
                    {
                        var frmTownLst = facList.Find(p => p.FAC_CODE == oItem.GCE_FRM_TN);
                        frmTown = frmTownLst.FAC_DESC;
                    }
                    bool existsTo = facList.Exists(element => element.FAC_CODE == oItem.GCE_TO_TN);
                    if (existsTo)
                    {
                        var ToTownLst = facList.Find(p => p.FAC_CODE == oItem.GCE_TO_TN);
                        toTown = ToTownLst.FAC_DESC;
                    }


                }
                else
                {
                    DataTable dt = new DataTable();

                    dt = CHNLSVC.General.Get_DetBy_town(oItem.GCE_FRM_TN);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            frmTown = dt.Rows[0]["TOWN"].ToString();
                        }
                    }
                    dt = CHNLSVC.General.Get_DetBy_town(oItem.GCE_TO_TN);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            toTown = dt.Rows[0]["TOWN"].ToString();
                        }
                    }
                }
                decimal totalAmountWithTax = 0;
                decimal totalAmountWithoutTax = 0;
                decimal totPayAmount = 0;
                decimal totTaxAmount = 0;
                decimal totDue = 0;
                if (oItem.ENQ_CHARGES.Count > 0)
                {
                    totalAmountWithTax = oItem.ENQ_CHARGES.Sum(a => a.SCH_TOT_AMT);
                    totTaxAmount = oItem.ENQ_CHARGES.Sum(a => a.SCH_ITM_TAX_AMT);
                    totPayAmount = oItem.ENQ_CHARGES.Where(s => s.SCH_INVOICED == 1).Sum(l => l.SCH_TOT_AMT);
                    totalAmountWithoutTax = totalAmountWithTax - totTaxAmount;
                    totDue = totalAmountWithTax - totPayAmount;
                }
                AgrementData.Rows.Add(oItem.GCE_REF, oItem.GCE_ENQ_ID, frmTown, oItem.GCE_EXPECT_DT,
                    toTown, oItem.GCE_RET_DT, oItem.GCE_FLEET, "", oItem.GCE_REF,
                    driName, 15, totPayAmount, totDue, userName, totalAmountWithTax, totTaxAmount, totalAmountWithoutTax);

                return AgrementData;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        private DataTable getCheckInOutDataTable(MST_CHKINOUT chkOutData)
        {
            try
            {
                DataTable CheckData = new DataTable();
                CheckData.Columns.Add("CurrentMilage", typeof(decimal));
                CheckData.Columns.Add("CurrentFuel", typeof(decimal));

                CheckData.Rows.Add(chkOutData.CHK_OUT_KM, chkOutData.CHK_OUT_FUEL);
                return CheckData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable getOtherEnquiryData(GEN_CUST_ENQ oItem)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                string userName = HttpContext.Session["UserName"] as string;
                //List<RecieptHeader> deposithr = CHNLSVC.Tours.GET_RECIEPT_BY_ENQ(company, userDefPro, oItem.GCE_ENQ_ID);
                //decimal deposittotal = deposithr.Sum(y => y.Sar_tot_settle_amt);
                List<GEN_CUS_ENQ_DRIVER> drivers = CHNLSVC.Tours.getEnquiryDriverDetails(oItem.GCE_ENQ_ID);
                string driver = "";
                if (drivers.Count > 0)
                {
                    int i = 1;
                    List<GEN_CUS_ENQ_DRIVER> drive = drivers.Where(y => y.GCD_ACT == 1).GroupBy(x => x.GCD_DRIVER_CD).Select(y => y.First()).ToList();
                    foreach (GEN_CUS_ENQ_DRIVER dri in drive)
                    {
                        driver += dri.GCD_DRIVER_NAME + ((i != drive.Count) ? "/" : "");
                        i++;
                    }
                }


                DataTable param = new DataTable();
                param.Columns.Add("depositAmount", typeof(decimal));
                param.Columns.Add("extraKMRate", typeof(decimal));
                param.Columns.Add("AALisionFee", typeof(decimal));
                param.Columns.Add("AdditionalChargAmnt", typeof(decimal));
                param.Columns.Add("BasicRentalAmount", typeof(decimal));
                param.Columns.Add("Driver", typeof(string));

                param.Rows.Add(oItem.GCE_DEPOSIT_CHG, 0, 0, 0, 0, driver);
                return param;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getTransChageDatable(List<ST_ENQ_CHARGES> chargData, string Type)
        {
            try
            {
                DataTable transDta = new DataTable();
                transDta.Columns.Add("ChargCode", typeof(string));
                transDta.Columns.Add("Amount", typeof(decimal));
                foreach (ST_ENQ_CHARGES charge in chargData)
                {
                    if (charge.SCH_ITM_SERVICE == Type)
                    {
                        transDta.Rows.Add(charge.SCH_ITM_CD, Convert.ToDecimal(charge.SCH_TOT_AMT.ToString("#.##")));
                    }
                }
                return transDta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult senEnquirySMSCustomer(string enqid)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(enqid))
                {
                    Int32 result = 0;
                    enqid = enqid.Trim();
                    List<MST_TEMP_MESSAGES> message = CHNLSVC.Tours.getTempSmsMessage(company, userDefPro, "GSTENQMSG");
                    GEN_CUST_ENQ oItem = CHNLSVC.Tours.getEnquiryDetails(company, userDefPro, enqid);
                    List<RecieptItemTBS> recItems = new List<RecieptItemTBS>();
                    MST_EMPLOYEE_NEW_TBS employees = CHNLSVC.Tours.getMstEmployeeDetails(oItem.GCE_DRIVER);
                    if (oItem != null)
                    {
                        OutSMS _out = new OutSMS();
                        OutSMS _outDriver = new OutSMS();
                        string err = string.Empty;
                        string errdri = string.Empty;
                        String msg = string.Empty;
                        String msgdriv = string.Empty;
                        if (message.Count > 0)
                        {
                            msg = message[0].MMT_TEXT;
                            msg = msg.Replace("@enqId", enqid);
                            msg = msg.Replace("@pictime", oItem.GCE_EXPECT_DT.ToString());
                            msg = msg.Replace("@picloc", oItem.GCE_FRM_TN.ToString());

                            if (employees == null)
                            {
                                return Json(new { success = false, login = true, msg = "Please enter drive details.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            if (employees.MEMP_FIRST_NAME == null)
                            {
                                return Json(new { success = false, login = true, msg = "Please enter drive name.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            if (employees.MEMP_MOBI_NO == null)
                            {
                                return Json(new { success = false, login = true, msg = "Please enter drive mobile.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            msg = msg.Replace("@drvnme", employees.MEMP_FIRST_NAME.ToString());
                            msg = msg.Replace("@drvcnt", employees.MEMP_MOBI_NO.ToString());
                            msg = msg.Replace("@vehinum", oItem.GCE_FLEET.ToString());


                        }
                        else
                        {
                            msg = "Dear Customer,\nPlease find your tour details.\nEnquiry -  " + enqid + " \nPick Up Timr - " + oItem.GCE_EXPECT_DT.ToString() + " \nPick Up Location :" + oItem.GCE_FRM_TN.ToString() + " \nDriver Name :" + employees.MEMP_FIRST_NAME.ToString() + " \nDriver Contact:" + employees.MEMP_MOBI_NO.ToString() + " \nVehicle Number :" + oItem.GCE_FLEET.ToString() + ".";
                        }

                        if (oItem.GCE_CONT_MOB.ToString() != "")
                        {
                            String mobi = "+94" + oItem.GCE_CONT_MOB.Substring(1, 9);
                            _out.Msgstatus = 0;
                            _out.Msgtype = "ENQ";
                            _out.Receivedtime = DateTime.Now;
                            _out.Receiver = mobi;
                            _out.Msg = msg;
                            _out.Receiverphno = mobi;
                            _out.Senderphno = mobi;
                            _out.Msgid = oItem.GCE_SEQ.ToString();
                            _out.Refdocno = enqid;
                            _out.Sender = "Abans Tours";
                            _out.Createtime = DateTime.Now;
                            result = CHNLSVC.Tours.SendSMS(_out, out err);
                            if (err != string.Empty)
                            {
                                return Json(new { success = true, login = true, msg = err, type = "Info" }, JsonRequestBehavior.AllowGet);
                            }

                            return Json(new { success = true, login = true, msg = "Message send successful to Customer :" + oItem.GCE_CONT_PER.ToString() + " and Driver :" + employees.MEMP_FIRST_NAME.ToString(), type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid mobile number details.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid enquiry id.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "Please select valid enquiry id." }, JsonRequestBehavior.AllowGet);
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
        public JsonResult senEnquirySMSDriver(string enqid)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(enqid))
                {
                    Int32 result = 0;
                    enqid = enqid.Trim();
                    List<MST_TEMP_MESSAGES> msgdriver = CHNLSVC.Tours.getTempSmsMessage(company, userDefPro, "GSTDRIMSG");
                    GEN_CUST_ENQ oItem = CHNLSVC.Tours.getEnquiryDetails(company, userDefPro, enqid);
                    List<RecieptItemTBS> recItems = new List<RecieptItemTBS>();
                    MST_EMPLOYEE_NEW_TBS employees = CHNLSVC.Tours.getMstEmployeeDetails(oItem.GCE_DRIVER);
                    if (oItem != null)
                    {
                        OutSMS _out = new OutSMS();
                        OutSMS _outDriver = new OutSMS();
                        string err = string.Empty;
                        string errdri = string.Empty;
                        String msg = string.Empty;
                        String msgdriv = string.Empty;
                        if (msgdriver.Count > 0)
                        {

                            if (employees == null)
                            {
                                return Json(new { success = false, login = true, msg = "Please enter drive details.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            if (employees.MEMP_FIRST_NAME == null)
                            {
                                return Json(new { success = false, login = true, msg = "Please enter drive name.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            if (employees.MEMP_MOBI_NO == null)
                            {
                                return Json(new { success = false, login = true, msg = "Please enter drive mobile.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }

                            msgdriv = msgdriver[0].MMT_TEXT;
                            msgdriv = msgdriv.Replace("@DriName", employees.MEMP_FIRST_NAME.ToString());
                            msgdriv = msgdriv.Replace("@enqId", enqid);
                            msgdriv = msgdriv.Replace("@pictime", oItem.GCE_EXPECT_DT.ToString());
                            msgdriv = msgdriv.Replace("@picloc", oItem.GCE_FRM_TN.ToString());
                            msgdriv = msgdriv.Replace("@droploc", oItem.GCE_TO_TN.ToString());
                            msgdriv = msgdriv.Replace("@gstCon", oItem.GCE_CONT_MOB.ToString());
                            msgdriv = msgdriv.Replace("@gstName", oItem.GCE_CONT_PER.ToString());



                        }
                        else
                        {
                            msgdriv = "Dear " + employees.MEMP_FIRST_NAME.ToString() + ",\nPlease find your tour details.\nEnquiry -" + enqid + " \nPick Up Timr - " + oItem.GCE_EXPECT_DT.ToString() + " \nPick Up Location :" + oItem.GCE_FRM_TN.ToString() + " \nDrop Location :" + oItem.GCE_TO_TN.ToString() + " \nGuest Contact:" + oItem.GCE_CONT_MOB.ToString() + " \nGuest Mob :" + oItem.GCE_CONT_PER.ToString() + ".";
                        }

                        if (oItem.GCE_CONT_MOB.ToString() != "")
                        {

                            String mobidri = "+94" + employees.MEMP_MOBI_NO.Substring(1, 9);
                            _outDriver.Msgstatus = 0;
                            _outDriver.Msgtype = "ENQ";
                            _outDriver.Receivedtime = DateTime.Now;
                            _outDriver.Receiver = mobidri;
                            _outDriver.Msg = msgdriv;
                            _outDriver.Receiverphno = mobidri;
                            _outDriver.Senderphno = mobidri;
                            _outDriver.Msgid = oItem.GCE_SEQ.ToString();
                            _outDriver.Refdocno = enqid;
                            _outDriver.Sender = "Abans Tours";
                            _outDriver.Createtime = DateTime.Now;
                            result = CHNLSVC.Tours.SendSMS(_out, out errdri);
                            if (errdri != string.Empty)
                            {
                                return Json(new { success = true, login = true, msg = errdri, type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            return Json(new { success = true, login = true, msg = "Message send successful to Customer :" + oItem.GCE_CONT_PER.ToString() + " and Driver :" + employees.MEMP_FIRST_NAME.ToString(), type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid mobile number details.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid enquiry id.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "Please select valid enquiry id." }, JsonRequestBehavior.AllowGet);
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
        public JsonResult FleetAlertreg()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {

                List<FleetAlert> fleetalreg = CHNLSVC.Tours.FleetAlertdata(DateTime.Now.Date, "REG");
                string expdata = "Vehicle Registration Expire : ";
                if (fleetalreg.Count > 0)
                {
                    foreach (var expdet in fleetalreg)
                    {
                        expdata = expdata + expdet.MSTF_REGNO + " " + expdet.MSTF_MODEL + " " + expdet.MSTF_OWN_CONT + " " + expdet.MSTF_OWN_NM + "| " + Environment.NewLine;
                        //expdata = expdata.Replace("@", "@" + System.Environment.NewLine);
                    }
                }


                return Json(new { success = true, login = true, msg = expdata, type = "Info" }, JsonRequestBehavior.AllowGet);


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
        public JsonResult FleetAlertlis()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                List<FleetAlert> fleetallis = CHNLSVC.Tours.FleetAlertdata(DateTime.Now.Date, "LIS");
                string expdata = "Vehicl License Expire : ";
                if (fleetallis.Count > 0)
                {
                    foreach (var expdet in fleetallis)
                    {
                        expdata = expdata + expdet.MSTF_REGNO + " " + expdet.MSTF_MODEL + " " + expdet.MSTF_OWN_CONT + " " + expdet.MSTF_OWN_NM + " | " + "\n";
                    }
                }


                return Json(new { success = true, login = true, msg = expdata, type = "Info" }, JsonRequestBehavior.AllowGet);



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
    }

}
