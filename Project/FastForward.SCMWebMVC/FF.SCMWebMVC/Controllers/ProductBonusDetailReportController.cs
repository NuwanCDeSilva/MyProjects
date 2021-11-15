using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FF.BusinessObjects;
using FF.BusinessObjects.BITool;
using FF.BusinessObjects.Commission;
using FF.BusinessObjects.Financial;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace FF.SCMWebMVC.Controllers
{
    public class ProductBonusDetailReportController : BaseController
    {
        // GET: ProductBonusDetailReport
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
                return Redirect("~/Login/index");
            }
        }

        public JsonResult GetDateRange(string p_cir_code)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<RBH_FRMDT_TODT> dates = CHNLSVC.Sales.get_hdr_dates(p_cir_code);
                return Json(new { success = true, login = true, date = dates }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "no data found" }, JsonRequestBehavior.AllowGet);
            }

        }

        //public ActionResult ViewWithTarget(string p_circular_code, string p_scehema_code, string FromDate, string ToDate)
        //{

        //    string userId = HttpContext.Session["UserID"] as string;
        //    string company = HttpContext.Session["UserCompanyCode"] as string;
        //    string userDefPro = HttpContext.Session["UserDefProf"] as string;
        //    string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
        //    DataTable productbonus = null;
        //    DataTable param = new DataTable();
        //    DataRow dr;
        //    MasterCompany companydesc = CHNLSVC.General.GetCompByCode(company);
        //    if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //    {
               
        //        param.Columns.Add("cirNumber", typeof(string));
        //        param.Columns.Add("schNumber", typeof(string));
        //        param.Columns.Add("fromDate", typeof(string));
        //        param.Columns.Add("toDate", typeof(string));
        //        param.Columns.Add("luser", typeof(string));
        //        param.Columns.Add("lgcompany", typeof(string));
        //        param.Columns.Add("lgaddress", typeof(string));
        //        dr = param.NewRow();
        //        dr["cirNumber"] = p_circular_code;
        //        dr["schNumber"] = p_scehema_code;
        //        dr["fromDate"] = FromDate;
        //        dr["toDate"] = ToDate;
        //        dr["luser"] = userId;
        //        dr["lgcompany"] = companydesc.Mc_desc;
        //        dr["lgaddress"] = companydesc.Mc_add1;
        //        param.Rows.Add(dr);
        //        ReportDocument rd = new ReportDocument();
        //        productbonus = CHNLSVC.Sales.GetProductBonusDet(p_circular_code, p_scehema_code, FromDate, ToDate);
        //        rd.Load(Server.MapPath("/Reports/" + "ProductBonusDetailReport.rpt"));
        //        rd.Database.Tables["ProductBonusDetail"].SetDataSource(productbonus);
        //        rd.Database.Tables["pdparam"].SetDataSource(param);
        //        this.Response.Clear();
        //        this.Response.ContentType = "application/pdf";
        //        Response.AppendHeader("Content-Disposition", "inline; filename=ExecutiveTarget.pdf");
        //        return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
        //        //return Redirect("~/ProductBonusDetailReport");
        //    }
        //    else {
        //        return Redirect("~/Login");
        //    }
        
        //}

        public ActionResult ViewWithTarget(string p_circular_code, string p_scehema_code, string FromDate, string ToDate)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            DataTable productbonus = null;
            DataTable param = new DataTable();
            DataRow dr;
            MasterCompany companydesc = CHNLSVC.General.GetCompByCode(company);
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                param.Columns.Add("cirNumber", typeof(string));
                param.Columns.Add("schNumber", typeof(string));
                param.Columns.Add("fromDate", typeof(string));
                param.Columns.Add("toDate", typeof(string));
                param.Columns.Add("luser", typeof(string));
                param.Columns.Add("lgcompany", typeof(string));
                param.Columns.Add("lgaddress", typeof(string));
                dr = param.NewRow();
                dr["cirNumber"] = p_circular_code;
                dr["schNumber"] = p_scehema_code;
                dr["fromDate"] = FromDate;
                dr["toDate"] = ToDate;
                dr["luser"] = userId;
                dr["lgcompany"] = companydesc.Mc_desc;
                dr["lgaddress"] = companydesc.Mc_add1;
                param.Rows.Add(dr);
                ReportDocument rd = new ReportDocument();
               // productbonus = CHNLSVC.Sales.GetProductBonusDet(p_circular_code, p_scehema_code, FromDate, ToDate);
                productbonus = CHNLSVC.Sales.GetProductBonusInvoiceDetails(p_circular_code, p_scehema_code, FromDate, ToDate, company);
                rd.Load(Server.MapPath("/Reports/" + "ProductBonusSummaryNew.rpt"));
                rd.Database.Tables["ProductBonusDetailInvoice"].SetDataSource(productbonus);
                rd.Database.Tables["pdparam"].SetDataSource(param);
                this.Response.Clear();
                this.Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "inline; filename=ExecutiveTarget.pdf");
                return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                //return Redirect("~/ProductBonusDetailReport");
            }
            else
            {
                return Redirect("~/Login");
            }

        }


        public ActionResult ViewWithTargetSummary(string p_circular_code, string p_scehema_code, string FromDate, string ToDate)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            DataTable productbonus = null;
            DataTable param = new DataTable();
            DataRow dr;
            MasterCompany companydesc = CHNLSVC.General.GetCompByCode(company);
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                param.Columns.Add("cirNumber", typeof(string));
                param.Columns.Add("schNumber", typeof(string));
                param.Columns.Add("fromDate", typeof(string));
                param.Columns.Add("toDate", typeof(string));
                param.Columns.Add("luser", typeof(string));
                param.Columns.Add("lgcompany", typeof(string));
                param.Columns.Add("lgaddress", typeof(string));
                dr = param.NewRow();
                dr["cirNumber"] = p_circular_code;
                dr["schNumber"] = p_scehema_code;
                dr["fromDate"] = FromDate;
                dr["toDate"] = ToDate;
                dr["luser"] = userId;
                dr["lgcompany"] = companydesc.Mc_desc;
                dr["lgaddress"] = companydesc.Mc_add1;
                param.Rows.Add(dr);
                ReportDocument rd = new ReportDocument();
                productbonus = CHNLSVC.Sales.GetProductBonusDet(p_circular_code, p_scehema_code, FromDate, ToDate);
                rd.Load(Server.MapPath("/Reports/" + "ProductBonusSummary.rpt"));
                rd.Database.Tables["ProductBonusDetail"].SetDataSource(productbonus);
                rd.Database.Tables["pdparam"].SetDataSource(param);
                this.Response.Clear();
                this.Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "inline; filename=ExecutiveTarget.pdf");
                return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                //return Redirect("~/ProductBonusDetailReport");
            }
            else
            {
                return Redirect("~/Login");
            }

        }

        public JsonResult SchemeNumberSearch(string circularcode)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<REF_BONUS_SCHEME> documents = CHNLSVC.ComSearch.getSchemeCode("1", "10", "SCHEME_CD", "", company, circularcode);
                    if (documents != null)
                    {
                        return Json(new { success = true, login = true, data = documents}, JsonRequestBehavior.AllowGet);
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

    }
}