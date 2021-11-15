using FastForward.WebAbansTours.Models;
using FF.BusinessObjects;
using FF.BusinessObjects.Tours;
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
    public class CostingSheetController : BaseController
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
                ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1021);
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
        // GET: CostingSheet
        List<QUO_COST_DET> oMainItems;
        QUO_COST_HDR oHeader;
        public ActionResult Index(string enqid = null)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["closeBtn"] = "true";
                ViewBag.enqId = enqid;
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }
        public JsonResult loadServiceTypes()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_COST_CATE> oCate = CHNLSVC.Tours.GET_COST_CATE(company, userDefPro);
                    if (oCate.Count > 0)
                    {
                        return Json(new { success = true, login = true, data = oCate }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No services defined.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult loadCurrencyCode()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MasterCurrency> _cur = CHNLSVC.General.GetAllCurrency(null);
                    if (_cur.Count > 0)
                    {
                        return Json(new { success = true, login = true, data = _cur }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult currencyCodeChange(string code, string chargeCd, string fare, string pax, string mrkUpMain, string service)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (code != "")
                    {
                        code = code.Trim();
                        chargeCd = chargeCd.Trim();
                        fare = fare.Trim();
                        pax = pax.Trim();
                        mrkUpMain = mrkUpMain.Trim();
                        service = service.Trim();
                        bool chargCodeErr = false;
                        string ItemExRate = "";
                        string fareVal = "";
                        string totalVal = "";
                        string totalLKRVal = "";
                        MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(company, userDefPro);
                        decimal _exchangRate = 0;
                        MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(company, code, DateTime.Now, _pc.Mpc_def_exrate, userDefPro);
                        if (_exc1 != null)
                        {
                            _exchangRate = _exc1.Mer_bnkbuy_rt;
                            ItemExRate = String.Format("{0:#,##0.00;($#,##0.00);0.00}", _exchangRate);

                        }
                        else if (code == "LKR")
                        {
                            _exchangRate = 1;
                            ItemExRate = String.Format("{0:#,##0.00;($#,##0.00);0.00}", _exchangRate);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "please update exchange rates for selected currency", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (!String.IsNullOrEmpty(chargeCd))
                        {
                            if (service == "AIRTVL")
                            {
                                SR_AIR_CHARGE oSR_AIR_CHARGE = CHNLSVC.Tours.GetChargeDetailsByCode(company, service, chargeCd, userDefPro);
                                if (oSR_AIR_CHARGE != null && oSR_AIR_CHARGE.SAC_CD != null)
                                {
                                    if (fare == "")
                                    {
                                        fare = oSR_AIR_CHARGE.SAC_RT.ToString();
                                    }
                                    if (!string.IsNullOrEmpty(pax))
                                    {
                                        totalVal = (Convert.ToDecimal(fare) * Convert.ToDecimal(pax)).ToString();
                                        totalLKRVal = Math.Round((Convert.ToDecimal(fare) * Convert.ToDecimal(pax) * Convert.ToDecimal(ItemExRate)), 0).ToString();

                                        if (!string.IsNullOrEmpty(mrkUpMain) && isdecimal(mrkUpMain) && Convert.ToDecimal(mrkUpMain) > 0)
                                        {
                                            decimal TotalLocalCost = Convert.ToDecimal(totalVal) * Convert.ToDecimal(ItemExRate);
                                            decimal MarkupValue = TotalLocalCost * Convert.ToDecimal(mrkUpMain) / 100;
                                            totalLKRVal = Math.Round((TotalLocalCost + MarkupValue), 0).ToString("N2");
                                        }
                                    }
                                }
                                else
                                {
                                    chargCodeErr = true;
                                }
                            }
                            else if (service == "TRANS")
                            {
                                SR_TRANS_CHA oSR_AIR_CHARGE = CHNLSVC.Tours.GetTransferChargeDetailsByCode(company, service, chargeCd,userDefPro);
                                if (oSR_AIR_CHARGE != null && oSR_AIR_CHARGE.STC_CD != null)
                                {
                                    if (fare == "")
                                    {
                                        fare = oSR_AIR_CHARGE.STC_RT.ToString();
                                    }
                                    if (!string.IsNullOrEmpty(pax))
                                    {
                                        totalVal = (Convert.ToDecimal(fare) * Convert.ToDecimal(pax)).ToString();
                                        totalLKRVal = Math.Round((Convert.ToDecimal(fare) * Convert.ToDecimal(ItemExRate)), 0).ToString();


                                        if (!string.IsNullOrEmpty(mrkUpMain) && isdecimal(mrkUpMain))
                                        {
                                            decimal TotalLocalCost = Convert.ToDecimal(totalVal) * Convert.ToDecimal(ItemExRate);
                                            decimal MarkupValue = TotalLocalCost * Convert.ToDecimal(mrkUpMain) / 100;
                                            totalLKRVal = Math.Round((TotalLocalCost + MarkupValue), 0).ToString("N2");
                                        }
                                    }
                                }
                                else
                                {
                                    chargCodeErr = true;
                                }
                            }
                            else if (service == "MSCELNS")
                            {
                                SR_SER_MISS oSR_SER_MISS = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, service, chargeCd, userDefPro);
                                if (oSR_SER_MISS != null && oSR_SER_MISS.SSM_CD != null)
                                {
                                    if (fare == "")
                                    {
                                        fare = oSR_SER_MISS.SSM_RT.ToString();
                                    }


                                    if (!string.IsNullOrEmpty(pax))
                                    {
                                        totalVal = (Convert.ToDecimal(fare) * Convert.ToDecimal(pax)).ToString();
                                        totalLKRVal = Math.Round((Convert.ToDecimal(fare) * Convert.ToDecimal(ItemExRate)), 0).ToString();


                                        if (!string.IsNullOrEmpty(mrkUpMain) && isdecimal(mrkUpMain))
                                        {
                                            decimal TotalLocalCost = Convert.ToDecimal(totalVal) * Convert.ToDecimal(ItemExRate);
                                            decimal MarkupValue = TotalLocalCost * Convert.ToDecimal(mrkUpMain) / 100;
                                            totalLKRVal = Math.Round((TotalLocalCost + MarkupValue), 0).ToString("N2");
                                        }
                                    }
                                }
                                else
                                {
                                    chargCodeErr = true;
                                }
                            }
                            else
                            {
                                SR_SER_MISS oSR_SER_MISS = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, service, chargeCd, userDefPro);
                                if (oSR_SER_MISS != null && oSR_SER_MISS.SSM_CD != null)
                                {
                                    if (fare == "")
                                    {
                                        fare = oSR_SER_MISS.SSM_RT.ToString();
                                    }


                                    if (!string.IsNullOrEmpty(pax))
                                    {
                                        totalVal = (Convert.ToDecimal(fare) * Convert.ToDecimal(pax)).ToString();
                                        totalLKRVal = Math.Round((Convert.ToDecimal(fare) * Convert.ToDecimal(ItemExRate)), 0).ToString();


                                        if (!string.IsNullOrEmpty(mrkUpMain) && isdecimal(mrkUpMain))
                                        {
                                            decimal TotalLocalCost = Convert.ToDecimal(totalVal) * Convert.ToDecimal(ItemExRate);
                                            decimal MarkupValue = TotalLocalCost * Convert.ToDecimal(mrkUpMain) / 100;
                                            totalLKRVal = Math.Round(TotalLocalCost + MarkupValue, 0).ToString("N2");
                                        }
                                    }
                                }
                                else
                                {
                                    chargCodeErr = true;
                                }
                            }

                            if (chargCodeErr)
                            {
                                return Json(new { success = true, login = true, ItemExRate = ItemExRate, spVal = false, msg = "Please enter valid charge code" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = true, login = true, spVal = true, ItemExRate = ItemExRate, fareVal = fareVal, totalVal = totalVal, totalLKRVal = totalLKRVal }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = true, login = true, ItemExRate = ItemExRate }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = true, login = true, msg = "Invalid currency code.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult loadChargCode(string code, string service)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (code != "")
                    {
                        if (service != "")
                        {
                            if (service == "TRANS")
                            {
                                SR_TRANS_CHA oSR_Trans_CHARGE = CHNLSVC.Tours.GetTransferChargeDetailsByCode(company, service, code,userDefPro);
                                if (oSR_Trans_CHARGE != null && oSR_Trans_CHARGE.STC_CD != null)
                                {
                                    return Json(new { success = true, login = true, data = oSR_Trans_CHARGE }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Please enter valid charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else if (service == "AIRTVL")
                            {
                                SR_AIR_CHARGE oSR_Airs_CHARGE = CHNLSVC.Tours.GetChargeDetailsByCode(company, service, code,userDefPro);
                                if (oSR_Airs_CHARGE != null && oSR_Airs_CHARGE.SAC_CD != null)
                                {
                                    return Json(new { success = true, login = true, data = oSR_Airs_CHARGE }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Please enter valid charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else if (service == "MSCELNS")
                            {
                                SR_SER_MISS oSR_Miss_CHARGE = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, service, code, userDefPro);
                                if (oSR_Miss_CHARGE != null && oSR_Miss_CHARGE.SSM_CD != null)
                                {
                                    return Json(new { success = true, login = true, data = oSR_Miss_CHARGE }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Please enter valid charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                SR_SER_MISS oSR_Miss_CHARGE = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, service, code, userDefPro);
                                if (oSR_Miss_CHARGE != null && oSR_Miss_CHARGE.SSM_CD != null)
                                {
                                    return Json(new { success = true, login = true, data = oSR_Miss_CHARGE }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Please enter valid charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }

                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Please select service.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult updateTourCharges(string chgCd, string service, string currencyCode, string pax, string fare, string tax, string markup, string markupAmt, string remarks, string totalPax, string serBy)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    chgCd = chgCd.Trim();
                    service = service.Trim();
                    currencyCode = currencyCode.Trim();
                    pax = pax.Trim();
                    fare = fare.Trim();
                    tax = tax.Trim();
                    markup = markup.Trim();
                    markupAmt = markupAmt.Trim();
                    remarks = remarks.Trim();
                    totalPax = totalPax.Trim();
                    string chargCdDesc = "";
                    decimal num;
                    if (String.IsNullOrEmpty(totalPax) || !decimal.TryParse(totalPax, out num))
                    {
                        return Json(new { success = false, login = true, msg = "Invalid total number of pax.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (String.IsNullOrEmpty(pax) || !decimal.TryParse(pax, out num))
                    {
                        return Json(new { success = false, login = true, msg = "Invalid line pax.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    if (String.IsNullOrEmpty(service))
                    {
                        return Json(new { success = false, login = true, msg = "Please select service.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (String.IsNullOrEmpty(chgCd))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (service == "TRANS")
                    {
                        SR_TRANS_CHA oSR_Trans_CHARGE = CHNLSVC.Tours.GetTransferChargeDetailsByCode(company, service, chgCd, userDefPro);
                        if (oSR_Trans_CHARGE == null || oSR_Trans_CHARGE.STC_CD == null)
                        {
                            return Json(new { success = false, login = true, msg = "Please enter valid charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            chargCdDesc = oSR_Trans_CHARGE.STC_DESC;
                        }
                    }
                    else if (service == "AIRTVL")
                    {
                        SR_AIR_CHARGE oSR_Airs_CHARGE = CHNLSVC.Tours.GetChargeDetailsByCode(company, service, chgCd, userDefPro);
                        if (oSR_Airs_CHARGE == null || oSR_Airs_CHARGE.SAC_CD == null)
                        {
                            return Json(new { success = false, login = true, msg = "Please enter valid charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            chargCdDesc = oSR_Airs_CHARGE.SAC_ADD_DESC;
                        }
                    }
                    else if (service == "MSCELNS")
                    {
                        SR_SER_MISS oSR_Miss_CHARGE = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, service, chgCd, userDefPro);
                        if (oSR_Miss_CHARGE == null || oSR_Miss_CHARGE.SSM_CD == null)
                        {
                            return Json(new { success = false, login = true, msg = "Please enter valid charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            chargCdDesc = oSR_Miss_CHARGE.SSM_DESC;
                        }
                    }
                    else
                    {
                        SR_SER_MISS oSR_Miss_CHARGE = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, service, chgCd, userDefPro);
                        if (oSR_Miss_CHARGE == null || oSR_Miss_CHARGE.SSM_CD == null)
                        {
                            return Json(new { success = false, login = true, msg = "Please enter valid charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            chargCdDesc = oSR_Miss_CHARGE.SSM_DESC;
                        }
                    }
                    if (String.IsNullOrEmpty(fare))
                    {
                        return Json(new { success = false, login = true, msg = "Please add cost of fare.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (!decimal.TryParse(fare, out num))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid cost of fare.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (String.IsNullOrEmpty(remarks))
                    {
                        return Json(new { success = false, login = true, msg = "Please add remarks.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (!String.IsNullOrEmpty(markup))
                    {
                        if (!decimal.TryParse(markup, out num))
                        {
                            return Json(new { success = false, login = true, msg = "Please enter valid markup.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (Convert.ToDecimal(markup) > 100 || Convert.ToDecimal(markup) < 0)
                        {
                            return Json(new { success = false, login = true, msg = "Maximum and minimum markup value range is 100%-0%.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (!String.IsNullOrEmpty(tax) && !decimal.TryParse(tax, out num))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid tax value.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    if (Session["oMainItems"] == null)
                    {
                        oMainItems = new List<QUO_COST_DET>();
                    }
                    else
                    {
                        oMainItems = (List<QUO_COST_DET>)Session["oMainItems"];
                    }
                    bool exists = oMainItems.Exists(m => m.QCD_CAT == service && m.QCD_SUB_CATE == chgCd && m.QCD_QTY == Convert.ToDecimal(pax) && m.QCD_RMK == remarks && m.QCD_UNIT_COST ==Convert.ToDecimal(fare));
                    if (exists) {
                        return Json(new { success = false, login = true, msg = "Cannot add same charge code details.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (oMainItems.Count > 0)
                    {
                        string search = currencyCode;
                        bool valid = true;
                        foreach (QUO_COST_DET item in oMainItems)
                        {
                            if (item.QCD_CURR != search)
                            {
                                valid = false;
                            }
                        }
                        if (!valid)
                        {
                            return Json(new { success = false, login = true, msg = "Cannot use different currecy type for charges.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    QUO_COST_DET oItem = new QUO_COST_DET();
                    oItem.QCD_SEQ = 0;
                    oItem.QCD_COST_NO = string.Empty;
                    oItem.QCD_CAT = service;
                    oItem.QCD_SUB_CATE = chgCd;
                    oItem.QCD_DESC = chargCdDesc;
                    oItem.QCD_CURR = currencyCode;
                    if (service == "AIRTVL")
                    {
                        oItem.QCD_ANAL1 = CHNLSVC.Tours.GetServiceByCode(company, userDefPro, chgCd);
                    }
                    else if (service == "TRANS")
                    {
                        oItem.QCD_ANAL1 = CHNLSVC.Tours.GetServiceByCodeTRANS(company, userDefPro, chgCd);
                    }
                    else if (service == "MSCELNS")
                    {
                        oItem.QCD_ANAL1 = CHNLSVC.Tours.GetServiceByCodeMSCELNS(company, userDefPro, chgCd);
                    }
                    else
                    {
                        oItem.QCD_ANAL1 = CHNLSVC.Tours.GetServiceByCodeMSCELNS(company, userDefPro, chgCd);
                    }
                    MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(company, userDefPro);
                    decimal _exchangRate = 0;
                    MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(company, currencyCode, DateTime.Now, _pc.Mpc_def_exrate, userDefPro);
                    if (_exc1 != null)
                    {
                        _exchangRate = _exc1.Mer_bnkbuy_rt;

                    }
                    else if (currencyCode == "LKR")
                    {
                        _exchangRate = 1;
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Please update exchange rates for selected currency", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }



                    oItem.QCD_EX_RATE = _exchangRate;
                    oItem.QCD_QTY = Convert.ToInt32(pax);
                    oItem.QCD_UNIT_COST = Convert.ToDecimal(fare);
                    oItem.QCD_TAX = (tax != "") ? Convert.ToDecimal(tax) : 0;
                    if (oItem.QCD_TAX != 0)
                    {
                        decimal tot=Convert.ToDecimal(pax) * Convert.ToDecimal(fare);
                        tot = tot + tot * oItem.QCD_TAX / 100;
                        oItem.QCD_TOT_COST = Math.Round(tot, 4);
                    }
                    else {
                        oItem.QCD_TOT_COST = Math.Round(Convert.ToDecimal(pax) * Convert.ToDecimal(fare), 4);
                    }
                    oItem.QCD_MARKUP = (markup != "") ? Convert.ToDecimal(markup) : 0;
                    oItem.QCD_MARKUP_AMT = (markupAmt != "") ? Convert.ToDecimal(markupAmt) : 0;

                    
                    if (markup != "")
                    {
                        oItem.QCD_TOT_LOCAL = Math.Round((Convert.ToDecimal(pax) * Convert.ToDecimal(fare) * _exchangRate) /*+ (Convert.ToDecimal(pax) * Convert.ToDecimal(fare) * _exchangRate) * oItem.QCD_MARKUP / 100*/, 4);
                        oItem.QCD_TOT_LOCAL = oItem.QCD_TOT_LOCAL + oItem.QCD_TOT_LOCAL * oItem.QCD_TAX / 100;
                    }
                    else
                    {
                        oItem.QCD_TOT_LOCAL = Math.Round(Convert.ToDecimal(pax) * Convert.ToDecimal(fare) * _exchangRate, 4);
                        oItem.QCD_TOT_LOCAL = oItem.QCD_TOT_LOCAL + oItem.QCD_TOT_LOCAL * oItem.QCD_TAX / 100;
                    }
                    //oItem.QCD_AF_MARKUP = Math.Round(oItem.QCD_TOT_LOCAL + (Convert.ToDecimal(pax) * Convert.ToDecimal(fare) * _exchangRate * oItem.QCD_MARKUP / 100), 0);
                    oItem.QCD_AF_MARKUP = Math.Round(oItem.QCD_TOT_LOCAL + (oItem.QCD_MARKUP_AMT), 4);
                    oItem.QCD_RMK = remarks;
                    oItem.QCD_STATUS = 1;
                    oItem.QCD_CRE_BY = userId;
                    oItem.QCD_CRE_DT = DateTime.Now;
                    oItem.QCD_MOD_BY = userId;
                    oItem.QCD_MOD_DT = DateTime.Now;

                    if (oMainItems.FindAll(x => x.QCD_SUB_CATE == "TOTAL" && x.QCD_CAT == oItem.QCD_CAT).Count > 0)
                    {
                        List<QUO_COST_DET> oCatItems = oMainItems.FindAll(x => x.QCD_CAT == oItem.QCD_CAT);
                        int Index = oMainItems.IndexOf(oCatItems[oCatItems.Count - 1]);
                        //int Index = oMainItems.Count - 1;
                        oMainItems.Insert(Index, oItem);
                    }
                    else
                    {
                        oMainItems.Add(oItem);
                    }
                    Session["oMainItems"] = oMainItems;
                    return Json(new { success = true, login = true, oMainItems = oMainItems }, JsonRequestBehavior.AllowGet);
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
        public JsonResult clearValues()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    Session["oMainItems"] = null;
                    Session["oHeader"] = null;
                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
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
        public JsonResult removeChargeItem(string chgCd, string service, string currencyCode, string pax, string totcst,string rmk)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (chgCd != "" && service != "" && currencyCode != "" && pax != "" && totcst != "")
                    {
                        List<QUO_COST_DET> Items = (List<QUO_COST_DET>)Session["oMainItems"];
                        if (Items == null)
                        {
                            oMainItems = new List<QUO_COST_DET>();
                        }
                        else
                        {
                            oMainItems = Items;
                        }

                        if (oMainItems.Count > 0)
                        {
                            try
                            {
                                var itemToRemove = oMainItems.First(r => r.QCD_CAT == service && r.QCD_SUB_CATE == chgCd && r.QCD_CURR == currencyCode && r.QCD_AF_MARKUP == Convert.ToDecimal(totcst) /*&& r.QCD_RMK == rmk*/);
                                oMainItems.Remove(itemToRemove);

                            }
                            catch (Exception e) {
                                return Json(new { success = false, login = true,msg=e.ToString(), type="Info" }, JsonRequestBehavior.AllowGet);
                            }
                            
                            Session["oMainItems"] = oMainItems;
                            return Json(new { success = true, login = true, oMainItems = oMainItems }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { success = false, login = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid data.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult saveCostSheet(QUO_COST_HDR formVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (Session["oMainItems"] == null)
                    {
                        return Json(new { success = false, login = true, msg = "Please add records.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    oMainItems = (List<QUO_COST_DET>)Session["oMainItems"];

                    if (oMainItems.FindAll(x => x.QCD_SUB_CATE != "").Count == 0)
                    {
                        return Json(new { success = false, login = true, msg = "Please add records.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (Session["oHeader"] != null)
                    {
                        oHeader = (QUO_COST_HDR)Session["oHeader"];
                        if (oHeader.QCH_ACT != 2)
                        {
                            return Json(new { success = false, login = true, msg = "Unable to save costing sheet(Invalid status).", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        oHeader.QCH_MARKUP = Convert.ToDecimal(formVal.QCH_MARKUP);
                        oHeader.QCH_MARKUP_AMT = Convert.ToDecimal(formVal.QCH_MARKUP_AMT);
                        oHeader.QCH_TOT_VALUE = Convert.ToDecimal(formVal.QCH_TOT_VALUE);
                        oHeader.QCH_TOT_PAX = Convert.ToInt32(formVal.QCH_TOT_PAX);
                        decimal sum = oMainItems.Sum(model => model.QCD_TOT_COST);//before mark up
                        decimal sumt = oMainItems.Sum(model => model.QCD_TOT_LOCAL);//before mark up
                        decimal sumLkr = oMainItems.Sum(model => model.QCD_AF_MARKUP);//after mark up
                        if (oHeader.QCH_MARKUP > 0)
                        {
                            oHeader.QCH_MARKUP_AMT = sumLkr * oHeader.QCH_MARKUP / 100;
                            sumLkr = sumLkr + sumLkr * oHeader.QCH_MARKUP / 100;
                        }
                        oHeader.QCH_TOT_COST = Convert.ToDecimal(sum);
                        oHeader.QCH_TOT_COST_LOCAL = Convert.ToDecimal(sumt);
                        oHeader.QCH_TOT_VALUE = Convert.ToDecimal(sumLkr);
                    }
                    else
                    {
                        oHeader = new QUO_COST_HDR();
                        oHeader.QCH_SEQ = 0;
                        oHeader.QCH_COM = company;
                        oHeader.QCH_SBU = userDefPro;
                        oHeader.QCH_COST_NO = string.Empty;
                        oHeader.QCH_DT = Convert.ToDateTime(formVal.QCH_DT);
                        oHeader.QCH_OTH_DOC = formVal.QCH_OTH_DOC;
                        oHeader.QCH_REF = formVal.QCH_REF;
                        oHeader.QCH_CUS_CD = formVal.QCH_CUS_CD;
                        oHeader.QCH_CUS_NAME = "";
                        oHeader.QCH_CUS_MOB = "";
                        oHeader.QCH_TOT_PAX = Convert.ToInt32(formVal.QCH_TOT_PAX);

                        decimal sumt = oMainItems.Sum(model => model.QCD_TOT_COST);//before mark up
                        decimal sum = oMainItems.Sum(model => model.QCD_TOT_LOCAL);
                        decimal sumLkr = oMainItems.Sum(model => model.QCD_AF_MARKUP);

                        oHeader.QCH_TOT_COST = Convert.ToDecimal(sumt);
                        oHeader.QCH_TOT_COST_LOCAL = Convert.ToDecimal(sum);
                        oHeader.QCH_MARKUP = Convert.ToDecimal(formVal.QCH_MARKUP);
                        oHeader.QCH_MARKUP_AMT = oHeader.QCH_TOT_COST * oHeader.QCH_MARKUP / 100;
                        oHeader.QCH_TOT_VALUE = Convert.ToDecimal(sumLkr);
                        oHeader.QCH_ACT = (Int32)ToursStatus.Pending;
                        oHeader.QCH_SEND_CUS = 0;
                        oHeader.QCH_CUS_SEND_DT = DateTime.MinValue.Date;
                        oHeader.QCH_CUS_APP = 0;
                        oHeader.QCH_CUS_APP_DT = DateTime.MinValue.Date;
                        oHeader.QCH_ANAL1 = "";
                        oHeader.QCH_ANAL2 = "";
                        oHeader.QCH_ANAL3 = "";
                        oHeader.QCH_ANAL4 = "";
                        oHeader.QCH_ANAL5 = 0;
                        oHeader.QCH_ANAL6 = 0;
                        oHeader.QCH_ANAL7 = 0;
                        oHeader.QCH_ANAL8 = DateTime.MinValue.Date;
                        oHeader.QCH_CRE_BY = userId;
                        oHeader.QCH_CRE_DT = DateTime.Now;
                        oHeader.QCH_MOD_BY = userId;
                        oHeader.QCH_MOD_DT = DateTime.Now;
                    }

                    MasterAutoNumber _Auto = new MasterAutoNumber();
                    _Auto.Aut_cate_cd = userDefPro;
                    _Auto.Aut_cate_tp = "CTSHT";
                    _Auto.Aut_direction = 1;
                    _Auto.Aut_modify_dt = null;
                    _Auto.Aut_moduleid = "CTSHT";
                    _Auto.Aut_number = 0;
                    _Auto.Aut_start_char = "CTSHT";
                    _Auto.Aut_year = null;// DateTime.Today.Year;

                    string err = "";
                    if (Convert.ToDecimal(oHeader.QCH_MARKUP_AMT) > 0)
                    {
                        //decimal sumOfItems = oMainItems.Sum(m => m.QCD_TOT_LOCAL);
                        //foreach (QUO_COST_DET item in oMainItems)
                        //{
                        //    decimal markUpPer = Math.Round(item.QCD_TOT_LOCAL / sumOfItems * 100, 4);
                        //    decimal markUpAmt = Math.Round(item.QCD_TOT_LOCAL / sumOfItems * oHeader.QCH_MARKUP_AMT, 0);
                        //    decimal afMarkUp = Math.Round(item.QCD_TOT_LOCAL, 0);

                        //    oMainItems.Where(d => d.QCD_RMK == item.QCD_RMK && d.QCD_SUB_CATE == item.QCD_SUB_CATE && d.QCD_CAT == item.QCD_CAT).First().QCD_MARKUP = markUpPer;
                        //    oMainItems.Where(d => d.QCD_RMK == item.QCD_RMK && d.QCD_SUB_CATE == item.QCD_SUB_CATE && d.QCD_CAT == item.QCD_CAT).First().QCD_MARKUP_AMT = markUpAmt;
                        //    oMainItems.Where(d => d.QCD_RMK == item.QCD_RMK && d.QCD_SUB_CATE == item.QCD_SUB_CATE && d.QCD_CAT == item.QCD_CAT).First().QCD_AF_MARKUP = afMarkUp;
                        //    oMainItems.Where(d => d.QCD_RMK == item.QCD_RMK && d.QCD_SUB_CATE == item.QCD_SUB_CATE && d.QCD_CAT == item.QCD_CAT).First().QCD_TOT_LOCAL = afMarkUp;
                        //}
                    }


                    Int32 result = CHNLSVC.Tours.SaveCostingSheet(oHeader, oMainItems, _Auto, out err);
                    if (result > 0)
                    {
                        return Json(new { success = true, login = true, msg = "Costing sheet saved successfully. Cost sheet Num : " + err, type = "Success" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        string msg = "Error Occur. Error : " + err;
                        return Json(new { success = false, login = true, msg = msg, type = "Error" }, JsonRequestBehavior.AllowGet);
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
        private bool isdecimal(string txt)
        {
            decimal dec;
            return decimal.TryParse(txt, out dec);
        }
        public JsonResult getEnquiryDetails(string enqId)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    enqId = enqId.Trim();
                    if (enqId != "")
                    {
                        string err = "";
                        Int32 result = CHNLSVC.Tours.getCostSheetDetails(company, userDefPro, enqId, "1,2,3", out oHeader, out oMainItems, out err);
                        if (err != "")
                        {
                            return Json(new { success = false, login = true, msg = err, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (oHeader.QCH_SEQ != 0 && oHeader.QCH_COST_NO != null)
                            {
                                Session["oHeader"] = oHeader;
                                Session["oMainItems"] = oMainItems;
                                return Json(new { success = true, login = true, oHeader = oHeader, oMainItems = oMainItems }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                GEN_CUST_ENQ oItem = CHNLSVC.Tours.getEnquiryDetails(company, userDefPro, enqId);
                                if (oItem != null)
                                {
                                    return Json(new { success = true, login = true, enqDt = oItem }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Invalid enquiry id.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }

                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Please enter enquiry id.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult approveCostingSheet(string enqId, string sendSms, string totVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    if (!CHNLSVC.Security.Is_OptionPerimitted(company, userId, 16048))
                    {
                        return Json(new { success = false, login = true, msg = "Sorry, You have no permission to approve this costing.( Advice: Required permission code : 16048) !", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    enqId = enqId.Trim();
                    totVal = totVal.Trim();
                    oHeader = (QUO_COST_HDR)Session["oHeader"];
                    if (oHeader == null)
                    {
                        oHeader = new QUO_COST_HDR();
                    }
                    if (oHeader.QCH_ACT == 0 || oHeader.QCH_ACT != 2)
                    {
                        return Json(new { success = false, login = true, msg = "Unable to approve costing sheet", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    MST_PCADDPARA addPa = CHNLSVC.Tours.getPcAdditionalPara(company, userDefPro, "CUSTOMER_APPROVE");
                    if (addPa.PARA_KEY == null)
                    {
                        oHeader.QCH_CUS_APP = 1;
                        oHeader.QCH_CUS_APP_DT = DateTime.Now;
                    }
                    else { 
                        
                    }
                    string err = string.Empty;
                    Int32 stage = (int)EnquiryStages.Quotation_Approved;
                    int result = CHNLSVC.Tours.UPDATE_COST_HDR_STATUS(stage, 1, oHeader.QCH_SEQ, company, userDefPro, userId, enqId, out  err,false,oHeader);

                    if (result > 0)
                    {
                        if (sendSms == "true")
                        {
                            List<MST_TEMP_MESSAGES> message = CHNLSVC.Tours.getTempSmsMessage(company, userDefPro, "COSTINGAPPROVE");
                            OutSMS _out = new OutSMS();
                            String msg = string.Empty ;
                            if (message.Count > 0)
                            {
                                msg = message[0].MMT_TEXT;
                                msg = msg.Replace("@enqId", enqId);
                                msg = msg.Replace("@totVal", totVal);
                                msg = msg.Replace("@QCH_SEQ", oHeader.QCH_SEQ.ToString());
                            }
                            else {
                                msg = "Dear Customer,\nYour quatation is finalized.\nEnquiry ID - " + enqId + "\nTotal Value - " + totVal + "\nReff Num :" + oHeader.QCH_SEQ.ToString() + " \nDo you want to approve?";
                            }
                           
                            
                            GEN_CUST_ENQ oEnquity = CHNLSVC.Tours.GET_CUST_ENQRY(company, userDefPro, enqId);
                            if (oEnquity.GCE_MOB.ToString() != "")
                            {
                                String mobi = "+94" + oEnquity.GCE_MOB.Substring(1, 9);
                                _out.Msgstatus = 0;
                                _out.Msgtype = "COST";
                                _out.Receivedtime = DateTime.Now;
                                _out.Receiver = mobi;
                                _out.Msg = msg;
                                _out.Receiverphno = mobi;
                                _out.Senderphno = mobi;
                                _out.Msgid = oHeader.QCH_SEQ.ToString();
                                _out.Refdocno = enqId;
                                _out.Sender = "Abans Tours";
                                _out.Createtime = DateTime.Now;
                                result = CHNLSVC.Tours.SendSMS(_out, out err);
                            }
                            return Json(new { success = true, login = true, msg = "Cost sheet approved.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = true, login = true, msg = "Cost sheet approved.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }


                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Error occurred.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult CostingReport(string costNo = null, string enqId = null)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                costNo = (costNo != null) ? costNo.Trim() : null;
                if (costNo != null)
                {
                    string content;
                    PrintModel model = new PrintModel();
                    content = Url.Content("~/Print_Module/Print_Viwer/CostingPrintViwer.aspx");
                    model.ReportPath = content;
                    model.enquiryId = enqId;
                    Session["CostSheetNumber"] = costNo;
                    return View("CostingReport", model);
                }
                else
                {
                    return Redirect("~/CostingSheet");
                }

            }
            else
            {
                return Redirect("~/Login");
            }
        }
        public JsonResult resetCostingSheet(string enqId)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    if (!CHNLSVC.Security.Is_OptionPerimitted(company, userId, 16048))
                    {
                        return Json(new { success = false, login = true, msg = "Sorry, You have no permission to reset this costing.( Advice: Required permission code : 16048) !", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    enqId = enqId.Trim();
                    oHeader = (QUO_COST_HDR)Session["oHeader"];
                    if (oHeader == null)
                    {
                        return Json(new { success = false, login = true, msg = "Invalid costing details.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (oHeader.QCH_ACT == 2)
                    {
                        return Json(new { success = false, login = true, msg = "Unable to reset costing sheet(Invalid status).", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    string err = string.Empty;
                    Int32 stage = (int)EnquiryStages.Quotation_Approved_Reset;
                    int result = CHNLSVC.Tours.UPDATE_COST_HDR_STATUS(stage, 2, oHeader.QCH_SEQ, company, userDefPro, userId, enqId, out  err,true);

                    if (result > 0)
                    {

                        List<MST_TEMP_MESSAGES> message = CHNLSVC.Tours.getTempSmsMessage(company, userDefPro, "COSTINCANCEL");

                        OutSMS _out = new OutSMS();
                        String msg = string.Empty;
                        if (message.Count > 0)
                        {
                            msg = message[0].MMT_TEXT + " " + enqId;
                        }
                        else {
                            msg = "Dear Customer,\nYour quatation is canceled.\nEnquiry ID -" + enqId;
                        }
                     
                        GEN_CUST_ENQ oEnquity = CHNLSVC.Tours.GET_CUST_ENQRY(company, userDefPro, enqId);
                        if (oEnquity.GCE_MOB.ToString() != "")
                        {
                            String mobi = "+94" + oEnquity.GCE_MOB.Substring(1, 9);
                            _out.Msgstatus = 0;
                            _out.Msgtype = "COST";
                            _out.Receivedtime = DateTime.Now;
                            _out.Receiver = mobi;
                            _out.Msg = msg;
                            _out.Receiverphno = mobi;
                            _out.Senderphno = mobi;
                            _out.Msgid = oHeader.QCH_SEQ.ToString();
                            _out.Refdocno = enqId;
                            _out.Sender = "Abans Tours";
                            _out.Createtime = DateTime.Now;
                            result = CHNLSVC.Tours.SendSMS(_out, out err);
                        }
                        return Json(new { success = true, login = true, msg = "Cost sheet reset success.", type = "Info" }, JsonRequestBehavior.AllowGet);
                   
                        }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Error occurred.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult genaratePurchaseOrder(string costingSheetNo)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {

                    if (!CHNLSVC.Security.Is_OptionPerimitted(company, userId, 16048))
                    {
                        return Json(new { success = false, login = true, msg = "Sorry, You have no permission to reset this costing.( Advice: Required permission code : 16048) !", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    costingSheetNo = costingSheetNo.Trim();
                    if (costingSheetNo == "")
                    {
                        return Json(new { success = false, login = true, msg = "Invalid costing sheet number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    oHeader = (QUO_COST_HDR)Session["oHeader"];
                    if (oHeader == null)
                    {
                        return Json(new { success = false, login = true, msg = "Invalid costing sheet.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    oMainItems = (List<QUO_COST_DET>)Session["oMainItems"];
                    if (oMainItems == null)
                    {
                        return Json(new { success = false, login = true, msg = "Invalid costing sheet.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (oHeader.QCH_ACT != 1)
                    {
                        return Json(new { success = false, login = true, msg = "Unable to genarate purchase order(Invalid status).", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    string err;
               
                            MasterAutoNumber _Auto = new MasterAutoNumber();
                            _Auto.Aut_cate_cd = company;
                            _Auto.Aut_cate_tp = "COM";
                            _Auto.Aut_direction = 1;
                            _Auto.Aut_modify_dt = null;
                            _Auto.Aut_moduleid = "POTBS";
                            _Auto.Aut_number = 0;
                            _Auto.Aut_start_char = "PR";
                            _Auto.Aut_year = null;//DateTime.Today.Year;

                            List<MST_PR_HED_DET> hetdet = new List<MST_PR_HED_DET>();
                            hetdet = CHNLSVC.Tours.getcostingforPurchaseOrder(costingSheetNo, company, userDefPro);
                            List<string> serpro = new List<string>();
                            serpro = CHNLSVC.Tours.getcostingSerProPurchaseOrder(costingSheetNo);

                            if (hetdet.Count == 0 &&  hetdet[0].QCD_ANAL1 != "") {
                                return Json(new { success = false, login = true, msg = "Unable to find costing details.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            if (serpro.Count == 0)
                            {
                                return Json(new { success = false, login = true, msg = "Invalid service provider details.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            string sessionid = Session["SessionID"].ToString();
                            Int32 result = CHNLSVC.Tours.genarateCostngPurchaseOrder(hetdet, serpro, _Auto, out err, company, userDefPro, userId, sessionid, (Int32)EnquiryStages.Quotation_Po_Genarated, (Int32)ToursStatus.POGenarated);
                            if (result > 0)
                            {
                                
                                return Json(new { success = true, login = true, msg = "Genarate PO successfull. PO Num : " + err, type = "Success" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                string msg = "Error Occur. Error : "+ err;
                                return Json(new { success = false, login = true, msg = msg, type = "Error" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid costing item details.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult cusCodeTextChanged(string cusCd)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (!string.IsNullOrEmpty(cusCd))
                    {
                        MasterBusinessEntity custProf = GetbyCustCD(cusCd.Trim());
                        if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                        {
                            return Json(new { success = true, local = true, login = true, data = custProf }, JsonRequestBehavior.AllowGet);
                        }
                        else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                        {
                            return Json(new { success = false, login = true, msg = Resource.txtInacCus, data = "", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            GroupBussinessEntity _grupProf = GetbyCustCDGrup(cusCd.Trim());
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
                                return Json(new { success = true, group = true, login = true, data = _grupProf }, JsonRequestBehavior.AllowGet);
                            }
                            else if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == false)
                            {
                                return Json(new { success = false, login = true, msg = Resource.txtInacCus, data = "" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = Resource.invalidCusCd, type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
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
    }
}