using FF.BusinessObjects;
using FF.BusinessObjects.ToursNew;
using FF.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FastForward.WebAbansTours.Controllers
{
    public class CostEnquiryController : BaseController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1024);
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
        // GET: CostEnquiry
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
        public JsonResult getEnqiyDetails(string report_inv, string CostShtRef, string Customer, string FrmDate, string ProfitCenter, string REPORT_TYPE, string ReqNo, string SerCde, string ToDate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    CostShtRef = CostShtRef.Trim();
                    Customer = Customer.Trim();
                    DateTime fmDt = Convert.ToDateTime(FrmDate.Trim());
                    DateTime tdt = Convert.ToDateTime(ToDate.Trim());
                    ProfitCenter = (ProfitCenter.Trim() != "") ? ProfitCenter.Trim() : userDefPro;
                    string repType = REPORT_TYPE.Trim();
                    ReqNo = ReqNo.Trim();
                    SerCde = SerCde.Trim();
                    decimal totalGP = 0;
                    decimal totalRevanue = 0;
                    decimal totalCostVal = 0;
                    List<QUO_COST_HDR> _cost_Hdr_list_Details = new List<QUO_COST_HDR>();
                    List<QUO_COST_HDR> _cost_Hdr_list_summery = new List<QUO_COST_HDR>();
                    if (repType != "")
                    {
                        if (repType == "chkDetails")
                        {
                            if (report_inv == "chkALL")
                            {
                                _cost_Hdr_list_Details = CHNLSVC.Tours.GET_COST_PRFITABILITY_DETAILS(company, ProfitCenter, ReqNo, CostShtRef, Customer, SerCde, fmDt, tdt);
                            }
                            else if (report_inv == "chkInvoiced")
                            {
                                _cost_Hdr_list_Details = CHNLSVC.Tours.GET_INVOICEDCOST_PRFITABILITY_DETAILS(company, ProfitCenter, ReqNo, CostShtRef, Customer, SerCde, fmDt, tdt);
                            }

                            foreach (QUO_COST_HDR _cost in _cost_Hdr_list_Details)
                            {
                                decimal GP = _cost.QCH_TOT_VALUE - _cost.QCH_TOT_COST_LOCAL;
                                _cost.QCH_GP = GP;
                                if (_cost.QCH_TOT_COST_LOCAL != 0 && _cost.QCH_TOT_VALUE!=0)
                                {
                                    decimal Pre = (GP / _cost.QCH_TOT_VALUE) * 100;
                                    string newpre = DoFormat(Pre);
                                    _cost.QCH_GP_Pre = newpre + "%";
                                }


                            }
                            totalGP = _cost_Hdr_list_Details.Sum(item => item.QCH_GP);
                            totalRevanue = _cost_Hdr_list_Details.Sum(item => item.QCH_TOT_VALUE);
                            totalCostVal = _cost_Hdr_list_Details.Sum(item => item.QCH_TOT_COST_LOCAL);
                        }
                        else if (repType == "chkSummery")
                        {
                            if (report_inv == "chkALL")
                            {
                                _cost_Hdr_list_summery = CHNLSVC.Tours.GET_COST_PRFITABILITY(company, ProfitCenter, ReqNo, CostShtRef, Customer, SerCde, fmDt, tdt);
                            }
                            else if (report_inv == "chkInvoiced")
                            {
                                _cost_Hdr_list_summery = CHNLSVC.Tours.GET_INVOICEDCOST_PRFITABILITY(company, ProfitCenter, ReqNo, CostShtRef, Customer, SerCde, fmDt, tdt);
                            }
                                foreach (QUO_COST_HDR _cost in _cost_Hdr_list_summery)
                            {
                                decimal GP = _cost.QCH_TOT_VALUE - _cost.QCH_TOT_COST_LOCAL;
                                _cost.QCH_GP = GP;
                                if (_cost.QCH_TOT_VALUE!=0) {
                                    decimal Pre = (GP / _cost.QCH_TOT_VALUE) * 100;
                                    string newpre = DoFormat(Pre);
                                    _cost.QCH_GP_Pre = newpre + "%";
                                }
                                


                            }
                            totalGP = _cost_Hdr_list_summery.Sum(item => item.QCH_GP);
                            totalRevanue = _cost_Hdr_list_summery.Sum(item => item.QCH_TOT_VALUE);
                            totalCostVal = _cost_Hdr_list_summery.Sum(item => item.QCH_TOT_COST_LOCAL);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "No data found", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
                        return Json(new { success = false, login = true, msg = "Please check details of summery to view.",type="Info" }, JsonRequestBehavior.AllowGet);
                    }

                    return Json(new { success = true, login = true, _cost_Hdr_list_Details = _cost_Hdr_list_Details, _cost_Hdr_list_summery = _cost_Hdr_list_summery, totalGP = totalGP, totalRevanue = totalRevanue, totalCostVal = totalCostVal }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError,type="Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public static string DoFormat(Decimal myNumber)
        {
            var s = string.Format("{0:0.00}", myNumber);

            if (s.EndsWith("00"))
            {
                return s;
            }
            else
            {
                return s;
            }
        }
    }
}