using FastForward.WebAbansTours.Models;
using FF.BusinessObjects;
using FF.BusinessObjects.Tours;
using FF.BusinessObjects.ToursNew;
using FF.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FastForward.WebAbansTours.Controllers
{
    public class ReservationManagementController : BaseController
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
                ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1029);
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
        private InvoiceHeader oHeader = new InvoiceHeader();
        // GET: TransportEnquiry
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["closeBtn"] = "true";
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
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
        public JsonResult loadTransportEnqData()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string Status = "1,2,3,4,5,6,7,8";
                    string type = "TNSPT";
                    //List<GEN_CUST_ENQ> oItems = CHNLSVC.Tours.GET_ENQRY_BY_PC_STATUS(company, userDefPro, Status, userId, 15001);
                    List<GEN_CUST_ENQ> oItems = CHNLSVC.Tours.SP_TOUR_GET_TRANSPORT_ENQRY(company, userDefPro, Status, type, userId, 15001);
                    if (oItems != null && oItems.Count > 0)
                    {
                        return Json(new { success = true, login = true, data = oItems }, JsonRequestBehavior.AllowGet);
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

        public JsonResult getEmployeeDetails(string val)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (val != "")
                    {
                        val = val.Trim();
                        MST_EMPLOYEE_NEW_TBS employees = CHNLSVC.Tours.getMstEmployeeDetails(val);
                        if (employees != null)
                        {
                            return Json(new { success = true, login = true, data = employees }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = true, login = true, msg = "Invalid driver epf.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid driver epf.", type = "Info" }, JsonRequestBehavior.AllowGet);
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

        public JsonResult getFleetDetails(string val)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    val = val.Trim();
                    MST_FLEET_NEW fleets = CHNLSVC.Tours.getMstFleetDetails(val);
                    if (fleets != null)
                    {
                        return Json(new { success = true, login = true, data = fleets }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid vehicle number.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult loadChargCode(string code,string service,string type)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                   
                    code = code.Trim();
                    if (code != "")
                    {
                        if (service == "T")
                        {
                            if (type != "")
                            {
                                SR_TRANS_CHA oSR_AIR_CHARGE = CHNLSVC.Tours.GetTransferChargeDetailsByCode(company, "TRANS", code, userDefPro);
                                if (oSR_AIR_CHARGE != null && oSR_AIR_CHARGE.STC_CD != null)
                                {
                                    return Json(new { success = true, login = true, code = oSR_AIR_CHARGE.STC_RT.ToString(), Curr = oSR_AIR_CHARGE.STC_CURR.ToString(), data = oSR_AIR_CHARGE }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Please enter valid charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else {
                                return Json(new { success = false, login = true, msg = "Please select charge type.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else if (service == "O")
                        {
                            SR_SER_MISS oSR_SER_MISS = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, "MSCELNS", code, userDefPro);
                            if (oSR_SER_MISS != null && oSR_SER_MISS.SSM_CD != null)
                            {
                                return Json(new { success = true, login = true,code = oSR_SER_MISS.SSM_RT.ToString(), Curr = oSR_SER_MISS.SSM_CUR.ToString(), data = oSR_SER_MISS }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "Please enter valid charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else {
                            return Json(new { success = false, login = true, msg = "Please select service.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid charg code.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getDataCustomerFromNic(string nic)
        {
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
                        MasterBusinessEntity custProf = GetbyNic(nic.Trim());
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
                            GroupBussinessEntity _grupProf = GetbyCustCDGrupNic(nic.Trim());
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
                                return Json(new { success = true, group = true, login = true, data = _grupProf }, JsonRequestBehavior.AllowGet);
                            }
                            else if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == false)
                            {
                                return Json(new { success = false, login = true, msg = Resource.txtInacCus, type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = Resource.errInvalidNIC, type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public MasterBusinessEntity GetbyNic(string nic)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, nic, null, null, null, null);
        }
        public GroupBussinessEntity GetbyCustCDGrupNic(string nic)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(null, nic, null, null, null, null);
        }
        public List<InvoiceItem> oMainInvoiceItems = null;

        public List<ST_ENQ_CHARGES> enqChrgItems = null;
        public JsonResult updateCharges(string chargCode, string passengers, string discountPercentage, string unitAmount, string deleteitem, string currency, string tax, string service, string numOfDays)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    // if (deleteitem == "Delete") Session["oMainInvoiceItems"] = null;
                    if (!string.IsNullOrEmpty(chargCode))
                    {
                        if (!string.IsNullOrEmpty(unitAmount))
                        {
                            decimal y;
                            bool isNumericUnitAmt = decimal.TryParse(unitAmount, out y);
                            if (isNumericUnitAmt)
                            {
                                if (service != "T") {
                                    numOfDays = "1";
                                }
                                decimal n;
                                bool isNumeric = decimal.TryParse(passengers, out n);
                                if (!string.IsNullOrEmpty(passengers))
                                {
                                    if (isNumeric)
                                    {
                                        decimal discountPer = 0;
                                        if (!string.IsNullOrEmpty(discountPercentage))
                                        {

                                            if (discountPer <= 100)
                                            {
                                                discountPer = Convert.ToDecimal(discountPercentage);
                                            }
                                            else
                                            {
                                                return Json(new { success = false, login = true, msg = "Discount percentage must be less than 100%.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }

                                        }
                                        SR_TRANS_CHA oSR_AIR_CHARGE = new SR_TRANS_CHA();
                                        SR_SER_MISS oSR_SER_MISS = new SR_SER_MISS();
                                        if (service == "T")
                                        {
                                            oSR_AIR_CHARGE = CHNLSVC.Tours.GetTransferChargeDetailsByCode(company, "TRANS", chargCode, userDefPro);
                                            if (oSR_AIR_CHARGE == null || oSR_AIR_CHARGE.STC_CD == null)
                                            {
                                                return Json(new { success = false, login = true, msg = "Please enter valid charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }

                                        }
                                        else if (service == "O")
                                        {
                                            oSR_SER_MISS = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, "MSCELNS", chargCode, userDefPro);
                                            if (oSR_SER_MISS == null || oSR_SER_MISS.SSM_CD == null)
                                            {
                                                return Json(new { success = false, login = true, msg = "Please enter valid charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }


                                        }
                                        DEPO_AMT_DATA existslibData = new DEPO_AMT_DATA();
                                        if (service == "O")
                                        {
                                            existslibData = CHNLSVC.Tours.getLiabilityDatabyChgCd(chargCode);
                                            if (existslibData.GCD_DAILY_RNT_CD != null)
                                            {
                                                DEPO_AMT_DATA libData = (DEPO_AMT_DATA)Session["libData"];

                                                if (libData == null)
                                                {
                                                    Session["libData"] = existslibData;
                                                }
                                                else
                                                {
                                                    return Json(new { success = false, login = true, msg = "Already exists liability chargers in charg list.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }

                                            }
                                        }
                                       
                                        if (Session["enqChrgItems"] != null)
                                        {
                                            enqChrgItems = (List<ST_ENQ_CHARGES>)Session["enqChrgItems"];
                                        }
                                        else
                                        {
                                            enqChrgItems = new List<ST_ENQ_CHARGES>();
                                        }
                                            // oMainInvoiceItems = (List<InvoiceItem>)Session["oMainInvoiceItems"];
                                        if (enqChrgItems.Count > 0)
                                            {
                                                var ratetype = enqChrgItems.FirstOrDefault().SCH_CURR;
                                                if (ratetype != currency)
                                                {
                                                    return Json(new { success = false, login = true, msg = "Please Select Same Currency.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                            }


                                        if (enqChrgItems.FindAll(x => x.SCH_ITM_CD == chargCode).Count > 0 && deleteitem != "Delete")
                                        {
                                            return Json(new { success = false, login = true, msg = "Selected charge code is already added.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                            tax = tax.Trim();
                                            if (tax != "0")
                                            {
                                                bool taxNumber = decimal.TryParse(tax, out n);
                                                if (!taxNumber)
                                                {
                                                    return Json(new { success = false, login = true, msg = "Invalid tax percentage.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                            numOfDays = Math.Round(Convert.ToDecimal(numOfDays), 0).ToString();
                                            decimal unitRate = Convert.ToDecimal(unitAmount);
                                            decimal disPercen = Convert.ToDecimal(discountPer);
                                            decimal passeng = Convert.ToDecimal(passengers);
                                            decimal totAmount = unitRate * passeng;
                                            decimal discountAmount = totAmount * disPercen / 100*Convert.ToInt32(numOfDays);
                                            decimal totBalanceAmount = totAmount - discountAmount;
                                            decimal taxAmount = 0;
                                            if (tax != "0")
                                            {
                                                taxAmount = totBalanceAmount * Convert.ToDecimal(tax) / 100;
                                                totBalanceAmount = totBalanceAmount + totBalanceAmount * Convert.ToDecimal(tax) / 100;
                                            }
                                            ST_ENQ_CHARGES oItem = new ST_ENQ_CHARGES();
                                            oItem.SCH_ITM_CD = chargCode;
                                            oItem.SCH_ITM_STUS = "GOD";
                                            oItem.SCH_QTY = passeng;
                                            oItem.SCH_UNIT_RT = Convert.ToDecimal(unitRate.ToString("#.##"));
                                            oItem.SCH_TOT_AMT = Convert.ToDecimal((totBalanceAmount * Convert.ToInt32(numOfDays)).ToString ("#.##"));
                                            if (service == "T") {
                                                oItem.SCH_CURR = oSR_AIR_CHARGE.STC_CURR;
                                                oItem.SCH_ALT_ITM_DESC = oSR_AIR_CHARGE.STC_DESC;
                                            }
                                            else if (service == "O") {
                                                oItem.SCH_CURR = oSR_SER_MISS.SSM_CUR;
                                                oItem.SCH_ALT_ITM_DESC = oSR_SER_MISS.SSM_DESC;
                                            }
                                           
                                            oItem.SCH_DISC_RT = disPercen;
                                            oItem.SCH_DISC_AMT = (discountAmount>0)?Convert.ToDecimal(discountAmount.ToString("#.##")):0;
                                            oItem.SCH_ITM_TAX_AMT = (taxAmount>0)?Convert.ToDecimal(taxAmount.ToString("#.##")):0;
                                            oItem.SCH_ITM_TAX_AMT = oItem.SCH_ITM_TAX_AMT * Convert.ToInt32(numOfDays);  
                                            oItem.SCH_INVOICED = 0;
                                            oItem.SCH_INVOICED_NO = "";
                                            oItem.SCH_ITM_SERVICE = service;
                                            if (deleteitem != "Delete") enqChrgItems.Add(oItem);
                                            var total = enqChrgItems.Where(m => m.SCH_INVOICED == 0).Sum(p => p.SCH_TOT_AMT);
                                            Session["enqChrgItems"] = enqChrgItems;



                                            return Json(new { success = true, login = true, data = oItem, total = total, libData = existslibData }, JsonRequestBehavior.AllowGet);

                                    }
                                    else
                                    {
                                        return Json(new { success = false, login = true, msg = "Invalid passenger count.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Please enter number of passengers.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "Please enter valid unit amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid charg code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Please enter a unit amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    Session["totalPaidAmount"] = null;
                    Session["RecieptItemList"] = null;
                    Session["oMainInvoiceItems"] = null;
                    Session["enqChrgItems"] = null;
                    Session["oldEnqChrgItems"] = null;
                    Session["ENQDRIVER"] = null;
                    Session["ENQVEHICLE"] = null;
                    Session["libData"] = null;
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
        public JsonResult saveEnquiryData(GEN_CUST_ENQ enqData, string InvTyp, string drivername, string drivercontact, string invoicing)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    //if (string.IsNullOrEmpty(enqData.GCE_ENQ_ID))
                    //{
                    //    return Json(new { success = false, login = true, msg = "Please enter reference num.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    //}
                    DateTime dateExpected = DateTime.Now;
                    dateExpected = Convert.ToDateTime(enqData.GCE_EXPECT_DT);

                    DateTime datReturne = DateTime.Now;
                    datReturne = Convert.ToDateTime(enqData.GCE_RET_DT);

                    if (enqData.GCE_ENQ_ID == "") {
                        DateTime now = Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy HH:mm"));
                        if (dateExpected > datReturne)
                        {
                            return Json(new { success = false, login = true, msg = "Please enter valid date and time range.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (dateExpected < now)
                        {
                            return Json(new { success = false, login = true, msg = "Please enter valid expected date and time.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (datReturne < now)
                        {
                            return Json(new { success = false, login = true, msg = "Please enter valid return date and time.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    
                    if (!string.IsNullOrEmpty(enqData.GCE_CUS_CD))
                    {
                        MasterBusinessEntity custProf = GetbyCustCD(enqData.GCE_CUS_CD);

                        if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                        {
                            return Json(new { success = false, login = true, msg = Resource.txtInacCus, data = "", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (custProf.Mbe_cd == null)
                        {
                            GroupBussinessEntity _grupProf = GetbyCustCDGrup(enqData.GCE_CUS_CD);
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == false)
                            {
                                return Json(new { success = false, login = true, msg = Resource.txtInacCus, data = "", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = Resource.invalidCusCd, type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    //if (string.IsNullOrEmpty(enqData.GCE_CUS_CD))
                    //{
                    //    if (string.IsNullOrEmpty(enqData.GCE_MOB) && string.IsNullOrEmpty(enqData.GCE_PP_NO))
                    //    {
                    //        return Json(new { success = false, login = true, msg = "Please enter passport or mobile number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    //    }
                    //}
                    if (string.IsNullOrEmpty(enqData.GCE_ENQ))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter remarks.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                     List<MST_FAC_LOC> facList = CHNLSVC.Tours.getFacLocations(company, userDefPro);
                    bool has=false;
                    if (string.IsNullOrEmpty(enqData.GCE_FRM_TN))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter picked up town.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (facList.Count > 0)
                        {
                            has = facList.Exists(ele => ele.FAC_CODE == enqData.GCE_FRM_TN);
                            if (!has) {
                                return Json(new { success = false, login = true, msg = "Invalid pick up town.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else {
                            DataTable dt = new DataTable();

                            dt = CHNLSVC.General.Get_DetBy_town(enqData.GCE_FRM_TN);
                            if (dt != null)
                            {
                                if (dt.Rows.Count <= 0)
                                {
                                    return Json(new { success = false, login = true, msg = "Invalid pick up town.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "Invalid pick up town.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        
                    }
                    if (string.IsNullOrEmpty(enqData.GCE_TO_TN))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter drop town.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (facList.Count > 0)
                        {
                            has = facList.Exists(ele => ele.FAC_CODE == enqData.GCE_TO_TN);
                            if (!has)
                            {
                                return Json(new { success = false, login = true, msg = "Invalid drop up town.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            DataTable dt = new DataTable();

                            dt = CHNLSVC.General.Get_DetBy_town(enqData.GCE_FRM_TN);
                            if (dt != null)
                            {
                                if (dt.Rows.Count <= 0)
                                {
                                    return Json(new { success = false, login = true, msg = "Invalid drop up town.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "Invalid drop up town.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    //if (string.IsNullOrEmpty(enqData.GCE_ADD1))
                    //{
                    //    return Json(new { success = false, login = true, msg = "Please enter address 1.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    //}
                    //else if (string.IsNullOrEmpty(enqData.GCE_ADD2))
                    //{
                    //    return Json(new { success = false, login = true, msg = "Please enter address 2.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    //}
                    if (string.IsNullOrEmpty(enqData.GCE_NO_PASS.ToString()))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter number of passengers.", type = "Info" }, JsonRequestBehavior.AllowGet);

                    }
                    else if (!isdecimal(enqData.GCE_NO_PASS.ToString()))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid number of passengers.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (Convert.ToDecimal(enqData.GCE_NO_PASS.ToString()) < 0)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid number of passengers.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    if (string.IsNullOrEmpty(enqData.GCE_REQ_NO_VEH.ToString()))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter number of request vehicles.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (!isInteger(enqData.GCE_REQ_NO_VEH.ToString()))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid number of request vehicles.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (Convert.ToInt32(enqData.GCE_REQ_NO_VEH.ToString()) < 1)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid number of request vehicles.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(enqData.GCE_REF)) {
                        return Json(new { success = false, login = true, msg = "Please enter refference number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    List<mst_fleet_driver> driverallow = CHNLSVC.Tours.getAlowcatedFleetAndDriverDetails(enqData.GCE_DRIVER, enqData.GCE_FLEET, enqData.GCE_EXPECT_DT.Date, enqData.GCE_RET_DT.Date, "DRIVER");
                    if (driverallow.Count > 0)
                    {
                        return Json(new { success = false, login = true, msg = "Driver already assigned for vehicle :" + driverallow[0].MFD_VEH_NO + " for this date range.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    List<mst_fleet_driver> fleet = CHNLSVC.Tours.getAlowcatedFleetAndDriverDetails(enqData.GCE_DRIVER, enqData.GCE_FLEET, enqData.GCE_EXPECT_DT.Date, enqData.GCE_RET_DT.Date, "FLEET");
                    if (fleet.Count > 0)
                    {
                        return Json(new { success = false, login = true, msg = "Fleet already assigned for driver:" + fleet[0].MFD_DRI + " for this date range.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    //List<GEN_CUST_ENQ> enqDrivaerAlow = CHNLSVC.Tours.getAlowcatedFleetAndDriverDetailsInEnquiry(enqData.GCE_DRIVER, enqData.GCE_FLEET, enqData.GCE_EXPECT_DT.Date, enqData.GCE_RET_DT.Date, "DRIVER");
                    //if (enqDrivaerAlow.Count > 0)
                    //{
                    //    string ids = "";
                    //    int i = 1;
                    //    foreach (GEN_CUST_ENQ enq in enqDrivaerAlow)
                    //    {
                    //        if (enqData.GCE_ENQ_ID != null && enq.GCE_ENQ_ID != enqData.GCE_ENQ_ID)
                    //        {
                    //            ids += enq.GCE_ENQ_ID;
                    //            ids = (ids != "") ? (i == enqDrivaerAlow.Count) ? "," : "" : "";
                    //        }
                    //        i++;
                    //    }
                    //    if (ids != "")
                    //    {
                    //        return Json(new { success = false, login = true, msg = "Driver already assigned for enquies :" + ids + " for this date range.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    //    }
                    //}
                    //List<GEN_CUST_ENQ> fleetEnqAlow = CHNLSVC.Tours.getAlowcatedFleetAndDriverDetailsInEnquiry(enqData.GCE_DRIVER, enqData.GCE_FLEET, enqData.GCE_EXPECT_DT.Date, enqData.GCE_RET_DT.Date, "FLEET");
                    //if (fleetEnqAlow.Count > 0)
                    //{
                    //    string ids = "";
                    //    int i = 1;
                    //    foreach (GEN_CUST_ENQ enq in fleetEnqAlow)
                    //    {
                    //        if (enqData.GCE_ENQ_ID != null && enq.GCE_ENQ_ID != enqData.GCE_ENQ_ID)
                    //        {
                    //            ids += enq.GCE_ENQ_ID;
                    //            ids = (ids != "") ? (i == enqDrivaerAlow.Count) ? "," : "" : "";
                    //        }
                    //        i++;
                    //    }
                    //    if (ids != "")
                    //    {
                    //        return Json(new { success = false, login = true, msg = "Fleet already assigned for enquiry :" + ids + " for this date range.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    //    } 
                    //}


                    string paidAmountString = Session["totalPaidAmount"] as string;
                    decimal totPaidAmount = Convert.ToDecimal(paidAmountString);
                    List<RecieptItemTBS> RecieptItemList = new List<RecieptItemTBS>();
                    List<RecieptItemTBS> sesRecieptItemList = Session["RecieptItemList"] as List<RecieptItemTBS>;
                    if (sesRecieptItemList == null)
                    {
                        sesRecieptItemList = new List<RecieptItemTBS>();
                    }

                    if (invoicing == "True")
                    {
                        if (sesRecieptItemList.Count == 0) {
                            return Json(new { success = false, login = true, msg = "Please add payments.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (enqData.GCE_ENQ_ID != "")
                        {
                            MST_CHKINOUT chkOut = CHNLSVC.Tours.getEnqChkData(enqData.GCE_ENQ_ID);
                            if (chkOut == null && chkOut.CHK_ENQ_ID == null)
                            {
                                return Json(new { success = false, login = true, msg = "Please add checkin/out details for invoice.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    RecieptItemList = sesRecieptItemList;
                    decimal totalPayed = 0;
                    decimal totRcpVal = 0;
                    if (Session["enqChrgItems"] != null)
                    {
                        enqChrgItems = (List<ST_ENQ_CHARGES>)Session["enqChrgItems"];
                    }
                    else
                    {
                        enqChrgItems = new List<ST_ENQ_CHARGES>();
                    }
                    if (enqChrgItems.Count > 0)
                    {
                        SR_TRANS_CHA oSR_AIR_CHARGES = new SR_TRANS_CHA();
                        SR_SER_MISS oSR_SER_MISSS = new SR_SER_MISS();
                        foreach (ST_ENQ_CHARGES charg in enqChrgItems)
                        {
                            if (charg.SCH_ITM_SERVICE == "T" && charg.SCH_INVOICED==0)
                            {
                                oSR_AIR_CHARGES = CHNLSVC.Tours.GetTransferChargeDetailsByCode(company, "TRANS", charg.SCH_ITM_CD, userDefPro);
                                if (oSR_AIR_CHARGES == null || oSR_AIR_CHARGES.STC_CD == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Charge items contain some invalid charges. Code :" + charg.SCH_ITM_CD, type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else if (charg.SCH_ITM_SERVICE == "O" && charg.SCH_INVOICED == 0)
                            {
                                oSR_SER_MISSS = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, "MSCELNS", charg.SCH_ITM_CD, userDefPro);
                                if (oSR_SER_MISSS == null || oSR_SER_MISSS.SSM_CD == null)
                                {
                                    return Json(new { success = false, login = true, msg = "Charge items contain some invalid charges. Code :" + charg.SCH_ITM_CD, type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                    if (invoicing == "True")
                    {
                        Session["oMainInvoiceItems"] = null;
                        var invItem = enqChrgItems.Where(m => m.SCH_INVOICED == 0);
                        oMainInvoiceItems = new List<InvoiceItem>();
                        Int32 line = 1;
                        foreach (ST_ENQ_CHARGES item in invItem)
                        {
                            InvoiceItem oItem = new InvoiceItem();
                            oItem.Sad_itm_line = line;
                            oItem.Sad_itm_cd = item.SCH_ITM_CD;
                            oItem.Sad_itm_stus = item.SCH_ITM_STUS;
                            oItem.Sad_qty = item.SCH_QTY;
                            oItem.Sad_unit_rt = item.SCH_UNIT_RT;
                            oItem.Sad_tot_amt = item.SCH_TOT_AMT;
                            oItem.SII_CURR = item.SCH_CURR;
                            oItem.Sad_alt_itm_desc = item.SCH_ALT_ITM_DESC;
                            oItem.Sad_disc_rt = item.SCH_DISC_RT;
                            oItem.Sad_disc_amt = item.SCH_DISC_AMT;
                            oItem.Sad_itm_tax_amt = item.SCH_ITM_TAX_AMT;
                            oMainInvoiceItems.Add(oItem);
                            line++;
                        }
                        Session["oMainInvoiceItems"] = oMainInvoiceItems;


                    }
                    else {
                        oMainInvoiceItems = null;
                    }
                    if (enqChrgItems.Count > 0)
                    {
                        totalPayed = enqChrgItems.Where(b=>b.SCH_INVOICED==0).Sum(a => a.SCH_TOT_AMT);
                    }
                    //else {
                    //    return Json(new { success = false, login = true, msg = "Please add charges.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    //}
                    if (RecieptItemList.Count > 0)
                    {
                        totRcpVal = RecieptItemList.Sum(b => b.Sird_settle_amt);
                    }
                    //else {
                    //    return Json(new { success = false, login = true, msg ="Please add payment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    //}
                    if (totalPayed == totRcpVal || enqChrgItems.Count == 0 || totRcpVal == 0)
                    {
                        int result = 0;
                        bool isinvoicvesave = false;



                        #region saveEnq
                        MasterAutoNumber _ReqInsAuto = new MasterAutoNumber();
                        //_ReqInsAuto.Aut_cate_cd = userDefPro;
                        //_ReqInsAuto.Aut_cate_tp = "PC";
                        //_ReqInsAuto.Aut_direction = null;
                        //_ReqInsAuto.Aut_modify_dt = null;
                        //_ReqInsAuto.Aut_moduleid = "AT";
                        //_ReqInsAuto.Aut_number = 0;
                        //_ReqInsAuto.Aut_start_char = "AT";
                        //_ReqInsAuto.Aut_year = DateTime.Today.Year;

                        MasterAutoNumber oMainReq = new MasterAutoNumber();
                        oMainReq.Aut_cate_cd = company;
                        oMainReq.Aut_cate_tp = "PC";
                        oMainReq.Aut_direction = null;
                        oMainReq.Aut_modify_dt = null;
                        oMainReq.Aut_moduleid = "ATMN";
                        oMainReq.Aut_number = 0;
                        oMainReq.Aut_start_char = "ATMN";
                        oMainReq.Aut_year = DateTime.Today.Year;

                        string err;
                        GEN_CUST_ENQ oItem = new GEN_CUST_ENQ();
                        oItem.GCE_SEQ = Convert.ToInt32(enqData.GCE_SEQ);
                        oItem.GCE_ENQ_ID = enqData.GCE_ENQ_ID;
                        oItem.GCE_REF = enqData.GCE_REF;
                        oItem.GCE_ENQ_TP = "TNSPT";
                        oItem.GCE_COM = company;
                        oItem.GCE_PC = userDefPro;
                        oItem.GCE_DT = DateTime.Now.Date;
                        oItem.GCE_CUS_CD = enqData.GCE_CUS_CD;
                        oItem.GCE_NAME = enqData.GCE_NAME;
                        oItem.GCE_ADD1 = enqData.GCE_ADD1;
                        oItem.GCE_ADD2 = enqData.GCE_ADD2;
                        oItem.GCE_MOB = enqData.GCE_MOB;
                        MasterBusinessEntity custProf = (enqData.GCE_CUS_CD!=null)?GetbyCustCD(enqData.GCE_CUS_CD.Trim()):new MasterBusinessEntity() ;
                        oItem.GCE_EMAIL = custProf.Mbe_email;
                        oItem.GCE_NIC = enqData.GCE_NIC;
                        DateTime date = Convert.ToDateTime(enqData.GCE_EXPECT_DT);
                        oItem.GCE_EXPECT_DT = date;
                        //oItem.GCE_EXPECT_DT = Convert.ToDateTime(txtRequestDate.Text);
                        oItem.GCE_SER_LVL = string.Empty;
                        oItem.GCE_ENQ = enqData.GCE_ENQ;
                        oItem.GCE_ENQ_COM = company;
                        oItem.GCE_ENQ_PC = userDefPro;
                        MasterProfitCenter oPc = CHNLSVC.General.GetPCByPCCode(company, userDefPro);
                        oItem.GCE_ENQ_PC_DESC = (oPc != null) ? oPc.Mpc_desc : string.Empty;
                        oItem.GCE_STUS = 1;
                        oItem.GCE_CRE_BY = userId;
                        oItem.GCE_CRE_DT = DateTime.Now;
                        oItem.GCE_MOD_BY = userId;
                        oItem.GCE_MOD_DT = DateTime.Now;
                        oItem.GCE_FLY_DATE = enqData.GCE_FLY_DATE;
                        oItem.GCE_FLY_NO = enqData.GCE_FLY_NO;
                        oItem.GCE_DL_TYPE = enqData.GCE_DL_TYPE;
                        List<MST_FACBY> oMST_FACBY = CHNLSVC.Tours.GET_FACILITY_BY(company, "Transport");
                        if (oMST_FACBY != null && oMST_FACBY.Count > 0 && oMST_FACBY.FindAll(x => x.MFB_FACPC == oItem.GCE_PC).Count > 0)
                        {
                            oItem.Gce_bill_cuscd = oMST_FACBY.FindAll(x => x.MFB_FACPC == oItem.GCE_PC)[0].MFB_BILL_CD;
                            //oItem.Gce_bill_cusname = oMST_FACBY.FindAll(x => x.MFB_FACPC == oItem.GCE_PC)[0].MFB_;
                        }
                        else
                        {
                            oItem.Gce_bill_cuscd = oItem.GCE_CUS_CD;
                            oItem.Gce_bill_cusname = oItem.Gce_bill_cusname;
                        }

                        oItem.GCE_FRM_TN = enqData.GCE_FRM_TN;
                        oItem.GCE_TO_TN = enqData.GCE_TO_TN;
                        oItem.GCE_FRM_ADD = (enqData.GCE_FRM_ADD != null) ? enqData.GCE_FRM_ADD : "";
                        oItem.GCE_TO_ADD = (enqData.GCE_TO_ADD != null) ? enqData.GCE_TO_ADD : "";
                        if (!string.IsNullOrEmpty(enqData.GCE_NO_PASS.ToString()))
                        {
                            oItem.GCE_NO_PASS = Convert.ToInt32(enqData.GCE_NO_PASS.ToString());
                        }
                        oItem.GCE_VEH_TP = enqData.GCE_VEH_TP;
                        date = Convert.ToDateTime(enqData.GCE_RET_DT);
                        oItem.GCE_RET_DT = date;
                        //oItem.GCE_RET_DT = Convert.ToDateTime(txtReturnDate.Text);
                        oItem.GCE_REF = enqData.GCE_REF;
                        oItem.GCE_FLEET = enqData.GCE_FLEET;
                        oItem.GCE_DRIVER = enqData.GCE_DRIVER;

                        oItem.GCE_CONT_PER = enqData.GCE_CONT_PER;
                        oItem.GCE_CONT_MOB = enqData.GCE_CONT_MOB;
                        oItem.GCE_REQ_NO_VEH = string.IsNullOrEmpty(enqData.GCE_REQ_NO_VEH.ToString()) ? 0 : Convert.ToInt32(enqData.GCE_REQ_NO_VEH);
                        oItem.GCE_ENQ_SUB_TP = enqData.GCE_ENQ_SUB_TP;
                        oItem.GCE_RENTAL_AGENT = enqData.GCE_RENTAL_AGENT;
                        oItem.GCE_CITY_OF_ISSUE = enqData.GCE_CITY_OF_ISSUE;
                        oItem.GCE_CUS_TYPE = enqData.GCE_CUS_TYPE;
                        oItem.GCE_PP_NO = enqData.GCE_PP_NO;
                        oItem.GCE_LBLTY_CHG = enqData.GCE_LBLTY_CHG;
                        oItem.GCE_DEPOSIT_CHG = enqData.GCE_DEPOSIT_CHG;
                        //  oGEN_CUST_ENQs.Add(oItem);
                        //  result = CHNLSVC.Tours.SaveEnquiryRequestList(oGEN_CUST_ENQs, _ReqInsAuto, oMainReq, out err);
                        #endregion
                        #region save
                        //MasterProfitCenter oPc = CHNLSVC.General.GetPCByPCCode(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());

                        //_ReqInsAuto = new MasterAutoNumber();
                        _ReqInsAuto.Aut_cate_cd = userDefPro;
                        _ReqInsAuto.Aut_cate_tp = "PC";
                        _ReqInsAuto.Aut_direction = null;
                        _ReqInsAuto.Aut_modify_dt = null;
                        _ReqInsAuto.Aut_moduleid = "AT";
                        _ReqInsAuto.Aut_number = 0;
                        _ReqInsAuto.Aut_start_char = "AT";
                        _ReqInsAuto.Aut_year = DateTime.Today.Year;
                        isinvoicvesave = true;
                        oItem.GCE_STUS = 5;

                        #region Invoice

                        //if (Session["oHeader"] != null)
                        //{
                        //    oHeader = (InvoiceHeader)Session["oHeader"];
                        //}
                        //else
                        //{
                        InvoiceHeaderTBS oHeader = new InvoiceHeaderTBS();
                        //}

                       // oMainInvoiceItems = (List<InvoiceItem>)Session["oMainInvoiceItems"];

                        MasterBusinessEntity oCust = GetbyCustCD(enqData.GCE_CUS_CD);
                        oHeader.Sih_com = company;
                        oHeader.Sih_cre_by = userId;
                        oHeader.Sih_cre_when = DateTime.Now;
                        oHeader.Sih_currency = "LKR";
                        oHeader.Sih_cus_add1 = oCust.Mbe_add1;
                        oHeader.Sih_cus_add2 = oCust.Mbe_add2;
                        oHeader.Sih_cus_cd = oCust.Mbe_cd;
                        oHeader.Sih_cus_name = oCust.MBE_FNAME;
                        oHeader.Sih_d_cust_add1 = oCust.Mbe_add1;
                        oHeader.Sih_d_cust_add2 = oCust.Mbe_add2;
                        oHeader.Sih_d_cust_cd = oCust.Mbe_cd;
                        oHeader.Sih_d_cust_name = oCust.MBE_FNAME;
                        oHeader.Sih_direct = true;
                        oHeader.Sih_dt = DateTime.Now.Date;
                        oHeader.Sih_epf_rt = 0;
                        oHeader.Sih_esd_rt = 0;
                        oHeader.Sih_ex_rt = 1;
                        oHeader.Sih_inv_no = "na";
                        oHeader.Sih_inv_sub_tp = "SA";
                        oHeader.Sih_inv_tp = InvTyp;
                        oHeader.Sih_is_acc_upload = false;
                        oHeader.Sih_man_ref = enqData.GCE_SEQ.ToString();
                        oHeader.Sih_manual = false;
                        oHeader.Sih_mod_by = userId;
                        oHeader.Sih_mod_when = DateTime.Now;
                        oHeader.Sih_pc = userDefPro;
                        oHeader.Sih_pdi_req = 0;
                        oHeader.Sih_ref_doc = enqData.GCE_ENQ_ID;
                        oHeader.Sih_sales_chn_cd = "";
                        oHeader.Sih_sales_chn_man = "";
                        oHeader.Sih_sales_ex_cd = userId;
                        oHeader.Sih_sales_region_cd = "";
                        oHeader.Sih_sales_region_man = "";
                        oHeader.Sih_sales_sbu_cd = "";
                        oHeader.Sih_sales_sbu_man = "";
                        oHeader.Sih_sales_str_cd = "";
                        oHeader.Sih_sales_zone_cd = "";
                        oHeader.Sih_sales_zone_man = "";
                        oHeader.Sih_seq_no = 1;
                        oHeader.Sih_session_id = Session["SessionID"].ToString();
                        // oHeader.Sih_structure_seq = txtQuotation.Text.Trim();
                        oHeader.Sih_stus = "A";
                        //  if (chkDeliverLater.Checked == false || chkDeliverNow.Checked) oHeader.Sih_stus = "D";
                        oHeader.Sih_town_cd = "";
                        oHeader.Sih_tp = "INV";
                        oHeader.Sih_wht_rt = 0;
                        oHeader.Sih_direct = true;
                        oHeader.Sih_tax_inv = oCust.Mbe_is_tax;
                        //oHeader.Sih_anal_11 = (chkDeliverLater.Checked || chkDeliverNow.Checked) ? 0 : 1;
                        //oHeader.Sih_del_loc = (chkDeliverLater.Checked == false || chkDeliverNow.Checked) ? BaseCls.GlbUserDefLoca : !string.IsNullOrEmpty(txtDelLocation.Text) ? txtDelLocation.Text : string.Empty;
                        oHeader.Sih_del_loc = string.Empty;
                        //oHeader.Sih_grn_com = _customerCompany;
                        //oHeader.Sih_grn_loc = _customerLocation;
                        //oHeader.Sih_is_grn = _isCustomerHasCompany;
                        //oHeader.Sih_grup_cd = string.IsNullOrEmpty(txtGroup.Text.Trim()) ? string.Empty : txtGroup.Text.Trim();
                        oHeader.Sih_is_svat = oCust.Mbe_is_svat;
                        oHeader.Sih_tax_exempted = oCust.Mbe_tax_ex;
                        oHeader.Sih_anal_2 = "SCV";
                        oHeader.Sih_anal_3 = "";
                        //oHeader.Sih_anal_6 = txtLoyalty.Text.Trim();
                        oHeader.Sih_man_cd = userDefPro;
                        oHeader.Sih_is_dayend = 0;
                        oHeader.Sih_remarks = "Invoice generated by trip request";

                        //if (Session["oHeader"] != null)
                        //{
                        //    oHeader = (InvoiceHeader)Session["oHeader"];
                        //}
                        //else
                        //{
                        InvoiceHeader oHeadernew = new InvoiceHeader();
                        //}

                        oHeadernew.Sah_com = oHeader.Sih_com;
                        oHeadernew.Sah_cre_by = oHeader.Sih_cre_by;
                        oHeadernew.Sah_cre_when = oHeader.Sih_cre_when;
                        oHeadernew.Sah_currency = oHeader.Sih_currency;
                        oHeadernew.Sah_cus_add1 = oHeader.Sih_cus_add1;
                        oHeadernew.Sah_cus_add2 = oHeader.Sih_cus_add2;
                        oHeadernew.Sah_cus_cd = oHeader.Sih_cus_cd;
                        oHeadernew.Sah_cus_name = oHeader.Sih_cus_name;
                        oHeadernew.Sah_d_cust_add1 = oHeader.Sih_d_cust_add1;
                        oHeadernew.Sah_d_cust_add2 = oHeader.Sih_d_cust_add2;
                        oHeadernew.Sah_d_cust_cd = oHeader.Sih_d_cust_cd;
                        oHeadernew.Sah_d_cust_name = oHeader.Sih_d_cust_name;
                        oHeadernew.Sah_direct = oHeader.Sih_direct;
                        oHeadernew.Sah_dt = oHeader.Sih_dt;
                        oHeadernew.Sah_epf_rt = oHeader.Sih_epf_rt;
                        oHeadernew.Sah_esd_rt = oHeader.Sih_esd_rt;
                        oHeadernew.Sah_ex_rt = oHeader.Sih_ex_rt;
                        oHeadernew.Sah_inv_no = oHeader.Sih_inv_no;
                        oHeadernew.Sah_inv_sub_tp = oHeader.Sih_inv_sub_tp;
                        oHeadernew.Sah_inv_tp = oHeader.Sih_inv_tp;
                        oHeadernew.Sah_is_acc_upload = oHeader.Sih_is_acc_upload;
                        oHeadernew.Sah_man_ref = oHeader.Sih_man_ref;
                        oHeadernew.Sah_manual = oHeader.Sih_manual;
                        oHeadernew.Sah_mod_by = oHeader.Sih_mod_by;
                        oHeadernew.Sah_mod_when = oHeader.Sih_mod_when;
                        oHeadernew.Sah_pc = oHeader.Sih_pc;
                        oHeadernew.Sah_pdi_req = oHeader.Sih_pdi_req;
                        oHeadernew.Sah_ref_doc = oHeader.Sih_ref_doc;
                        oHeadernew.Sah_sales_chn_cd = "";
                        oHeadernew.Sah_sales_chn_man = "";
                        oHeadernew.Sah_sales_ex_cd = userId;
                        oHeadernew.Sah_sales_region_cd = "";
                        oHeadernew.Sah_sales_region_man = "";
                        oHeadernew.Sah_sales_sbu_cd = "";
                        oHeadernew.Sah_sales_sbu_man = "";
                        oHeadernew.Sah_sales_str_cd = "";
                        oHeadernew.Sah_sales_zone_cd = "";
                        oHeadernew.Sah_sales_zone_man = "";
                        oHeadernew.Sah_seq_no = 1;
                        oHeadernew.Sah_session_id = Session["SessionID"].ToString();
                        // oHeadernew.Sah_structure_seq = txtQuotation.Text.Trim();
                        oHeadernew.Sah_stus = "A";
                        //  if (chkDeliverLater.Checked == false || chkDeliverNow.Checked) oHeadernew.Sah_stus = "D";
                        oHeadernew.Sah_town_cd = "";
                        oHeadernew.Sah_tp = "INV";
                        oHeadernew.Sah_wht_rt = 0;
                        oHeadernew.Sah_direct = true;
                        oHeadernew.Sah_tax_inv = oHeader.Sih_tax_inv;
                        //oHeadernew.Sah_anal_11 = (chkDeliverLater.Checked || chkDeliverNow.Checked) ? 0 : 1;
                        //oHeadernew.Sah_del_loc = (chkDeliverLater.Checked == false || chkDeliverNow.Checked) ? BaseCls.GlbUserDefLoca : !string.IsNullOrEmpty(txtDelLocation.Text) ? txtDelLocation.Text : string.Empty;
                        oHeadernew.Sah_del_loc = string.Empty;
                        //oHeadernew.Sah_grn_com = _customerCompany;
                        //oHeadernew.Sah_grn_loc = _customerLocation;
                        //oHeadernew.Sah_is_grn = _isCustomerHasCompany;
                        //oHeadernew.Sah_grup_cd = string.IsNullOrEmpty(txtGroup.Text.Trim()) ? string.Empty : txtGroup.Text.Trim();
                        oHeadernew.Sah_is_svat = oHeader.Sih_is_svat;
                        oHeadernew.Sah_tax_exempted = oHeader.Sih_tax_exempted;
                        oHeadernew.Sah_anal_2 = "SCV";
                        oHeadernew.Sah_anal_3 = "";
                        //oHeadernew.Sah_anal_6 = txtLoyalty.Text.Trim();
                        oHeadernew.Sah_man_cd = userDefPro;
                        oHeadernew.Sah_is_dayend = 0;
                        oHeadernew.Sah_remarks = "Invoice generated by trip request";






                        List<RecieptItemTBS> _recieptItems = RecieptItemList;
                        if (_recieptItems == null)
                        {
                            _recieptItems = new List<RecieptItemTBS>();
                        }

                        List<RecieptItem> _recieptItemsNew = new List<RecieptItem>();
                        if (_recieptItems.Count > 0)
                        {
                            Int32 lineno = 0;
                            foreach (RecieptItemTBS _reciepts in _recieptItems)
                            {
                                RecieptItem _Items = new RecieptItem();
                                _Items.Sard_anal_1 = _reciepts.Sird_anal_1;
                                _Items.Sard_anal_2 = _reciepts.Sird_anal_2;
                                _Items.Sard_anal_3 = _reciepts.Sird_anal_3;
                                _Items.Sard_anal_4 = _reciepts.Sird_anal_4;
                                _Items.Sard_anal_5 = _reciepts.Sird_anal_5;
                                _Items.Sard_cc_expiry_dt = _reciepts.Sird_cc_expiry_dt;
                                _Items.Sard_cc_is_promo = _reciepts.Sird_cc_is_promo;
                                _Items.Sard_cc_period = _reciepts.Sird_cc_period;
                                _Items.Sard_cc_tp = _reciepts.Sird_cc_tp;
                                _Items.Sard_chq_bank_cd = _reciepts.Sird_chq_bank_cd;
                                _Items.Sard_chq_branch = _reciepts.Sird_chq_branch;
                                _Items.Sard_credit_card_bank = _reciepts.Sird_credit_card_bank;
                                _Items.Sard_deposit_bank_cd = _reciepts.Sird_deposit_bank_cd;
                                _Items.Sard_deposit_branch = _reciepts.Sird_deposit_branch;
                                _Items.Sard_gv_issue_dt = _reciepts.Sird_gv_issue_dt;
                                _Items.Sard_gv_issue_loc = _reciepts.Sird_gv_issue_loc;
                                _Items.Sard_inv_no = _reciepts.Sird_inv_no;
                                _Items.Sard_line_no = lineno;
                                _Items.Sard_pay_tp = _reciepts.Sird_pay_tp;
                                _Items.Sard_receipt_no = _reciepts.Sird_receipt_no;
                                _Items.Sard_ref_no = _reciepts.Sird_ref_no;
                                _Items.Sard_seq_no = _reciepts.Sird_seq_no;
                                _Items.Sard_settle_amt = _reciepts.Sird_settle_amt;
                                _Items.Sard_sim_ser = _reciepts.Sird_sim_ser;
                                _Items.Sard_rmk = _reciepts.Sird_rmk;
                                _Items.Sard_cc_batch = _reciepts.Sird_cc_batch;
                                _Items.Sard_chq_dt = _reciepts.Sird_chq_dt;
                                _recieptItemsNew.Add(_Items);
                                lineno++;
                            }

                        }
                        //RecieptItemTBS _item = new RecieptItemTBS();

                        //_item.Sird_deposit_bank_cd = null;
                        //_item.Sird_deposit_branch = null;
                        //_item.Sird_pay_tp = "CS";
                        //_item.Sird_settle_amt = oMainInvoiceItems.Sum(x => x.Sad_tot_amt);

                        //_recieptItems.Add(_item);



                        RecieptHeaderTBS _ReceiptHeader = new RecieptHeaderTBS();
                        _ReceiptHeader.Sir_seq_no = -1; //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, BaseCls.GlbUserComCode);
                        _ReceiptHeader.Sir_com_cd = company;
                        _ReceiptHeader.Sir_receipt_type = "DIR";
                        // _ReceiptHeader.Sir_receipt_no = txtRecNo.Text.Trim();
                        MasterReceiptDivision _RecDiv = new MasterReceiptDivision();
                        _RecDiv = CHNLSVC.Sales.GetDefRecDivision(company, userDefPro);
                        if (_RecDiv.Msrd_cd != null)
                        {
                            _ReceiptHeader.Sir_prefix = _RecDiv.Msrd_cd;
                        }
                        else
                        {
                            _ReceiptHeader.Sir_prefix = "";
                        }
                        //_ReceiptHeader.Sir_prefix = txtDivision.Text.Trim();
                        // _ReceiptHeader.Sir_manual_ref_no = txtManual.Text.Trim();
                        _ReceiptHeader.Sir_receipt_date = DateTime.Now.Date;
                        _ReceiptHeader.Sir_direct = true;
                        _ReceiptHeader.Sir_acc_no = "";
                        _ReceiptHeader.Sir_is_oth_shop = false;
                        _ReceiptHeader.Sir_oth_sr = "";
                        _ReceiptHeader.Sir_profit_center_cd = userDefPro;
                        _ReceiptHeader.Sir_debtor_cd = oCust.Mbe_cd;
                        _ReceiptHeader.Sir_debtor_name = oCust.MBE_FNAME;
                        _ReceiptHeader.Sir_debtor_add_1 = oCust.Mbe_add1;
                        _ReceiptHeader.Sir_debtor_add_2 = oCust.Mbe_add2;
                        _ReceiptHeader.Sir_tel_no = "";
                        _ReceiptHeader.Sir_mob_no = oCust.Mbe_mob;
                        _ReceiptHeader.Sir_nic_no = oCust.Mbe_nic;


                        _ReceiptHeader.Sir_tot_settle_amt = _recieptItems.Sum(x => x.Sird_settle_amt);

                        _ReceiptHeader.Sir_comm_amt = 0;
                        _ReceiptHeader.Sir_is_mgr_iss = false;
                        _ReceiptHeader.Sir_esd_rate = 0;
                        _ReceiptHeader.Sir_wht_rate = 0;
                        _ReceiptHeader.Sir_epf_rate = 0;
                        _ReceiptHeader.Sir_currency_cd = "LKR";
                        _ReceiptHeader.Sir_uploaded_to_finance = false;
                        _ReceiptHeader.Sir_act = true;
                        _ReceiptHeader.Sir_direct_deposit_bank_cd = "";
                        _ReceiptHeader.Sir_direct_deposit_branch = "";
                        // _ReceiptHeader.Sir_remarks = txtNote.Text.Trim();
                        _ReceiptHeader.Sir_is_used = false;
                        _ReceiptHeader.Sir_ref_doc = "";
                        _ReceiptHeader.Sir_ser_job_no = "";
                        _ReceiptHeader.Sir_used_amt = 0;
                        _ReceiptHeader.Sir_create_by = userId;
                        _ReceiptHeader.Sir_mod_by = userId;
                        _ReceiptHeader.Sir_session_id = Session["SessionID"].ToString();
                        _ReceiptHeader.Sir_anal_1 = oCust.Mbe_cr_distric_cd;
                        _ReceiptHeader.Sir_anal_2 = oCust.Mbe_cr_province_cd;



                        //repeat for oth
                        RecieptHeader _ReceiptHeaderNew = new RecieptHeader();
                        _ReceiptHeaderNew.Sar_seq_no = -1; //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, BaseCls.GlbUserComCode);
                        _ReceiptHeaderNew.Sar_com_cd = company;
                        _ReceiptHeaderNew.Sar_receipt_type = "DIR";
                        // _ReceiptHeaderNew.Sar_receipt_no = txtRecNo.Text.Trim();\

                        _ReceiptHeaderNew.Sar_prefix = _ReceiptHeader.Sir_prefix;

                        //_ReceiptHeaderNew.Sar_prefix = txtDivision.Text.Trim();
                        // _ReceiptHeaderNew.Sar_manual_ref_no = txtManual.Text.Trim();
                        _ReceiptHeaderNew.Sar_receipt_date = DateTime.Now.Date;
                        _ReceiptHeaderNew.Sar_direct = true;
                        _ReceiptHeaderNew.Sar_acc_no = "";
                        _ReceiptHeaderNew.Sar_is_oth_shop = false;
                        _ReceiptHeaderNew.Sar_oth_sr = "";
                        _ReceiptHeaderNew.Sar_profit_center_cd = userDefPro;
                        _ReceiptHeaderNew.Sar_debtor_cd = oCust.Mbe_cd;
                        _ReceiptHeaderNew.Sar_debtor_name = oCust.MBE_FNAME;
                        _ReceiptHeaderNew.Sar_debtor_add_1 = oCust.Mbe_add1;
                        _ReceiptHeaderNew.Sar_debtor_add_2 = oCust.Mbe_add2;
                        _ReceiptHeaderNew.Sar_tel_no = "";
                        _ReceiptHeaderNew.Sar_mob_no = oCust.Mbe_mob;
                        _ReceiptHeaderNew.Sar_nic_no = oCust.Mbe_nic;


                        _ReceiptHeaderNew.Sar_tot_settle_amt = _recieptItems.Sum(x => x.Sird_settle_amt);

                        _ReceiptHeaderNew.Sar_comm_amt = 0;
                        _ReceiptHeaderNew.Sar_is_mgr_iss = false;
                        _ReceiptHeaderNew.Sar_esd_rate = 0;
                        _ReceiptHeaderNew.Sar_wht_rate = 0;
                        _ReceiptHeaderNew.Sar_epf_rate = 0;
                        _ReceiptHeaderNew.Sar_currency_cd = "LKR";
                        _ReceiptHeaderNew.Sar_uploaded_to_finance = false;
                        _ReceiptHeaderNew.Sar_act = true;
                        _ReceiptHeaderNew.Sar_direct_deposit_bank_cd = "";
                        _ReceiptHeaderNew.Sar_direct_deposit_branch = "";
                        // _ReceiptHeaderNew.Sar_remarks = txtNote.Text.Trim();
                        _ReceiptHeaderNew.Sar_is_used = false;
                        _ReceiptHeaderNew.Sar_ref_doc = "";
                        _ReceiptHeaderNew.Sar_ser_job_no = "";
                        _ReceiptHeaderNew.Sar_used_amt = 0;
                        _ReceiptHeaderNew.Sar_create_by = userId;
                        _ReceiptHeaderNew.Sar_mod_by = userId;
                        _ReceiptHeaderNew.Sar_session_id = Session["SessionID"].ToString();
                        _ReceiptHeaderNew.Sar_anal_1 = oCust.Mbe_cr_distric_cd;
                        _ReceiptHeaderNew.Sar_anal_2 = oCust.Mbe_cr_province_cd;


                        MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                        if (oMainInvoiceItems != null)
                        {
                            if (oMainInvoiceItems.Count > 0)
                            {
                                _invoiceAuto.Aut_cate_cd = company;
                                _invoiceAuto.Aut_cate_tp = "TINVO";
                                _invoiceAuto.Aut_direction = 1;
                                _invoiceAuto.Aut_modify_dt = null;
                                _invoiceAuto.Aut_moduleid = "CS";
                                _invoiceAuto.Aut_number = 0;
                                _invoiceAuto.Aut_start_char = "TINVO";
                                _invoiceAuto.Aut_year = DateTime.Now.Date.Year;
                            }
                        }
                        MasterAutoNumber _receiptAuto = null;
                        if (_recieptItems != null)
                            if (_recieptItems.Count > 0)
                            {
                                _receiptAuto = new MasterAutoNumber();
                                _receiptAuto.Aut_cate_cd = company;
                                _receiptAuto.Aut_cate_tp = "PRO";
                                _receiptAuto.Aut_direction = 1;
                                _receiptAuto.Aut_modify_dt = null;
                                _receiptAuto.Aut_moduleid = "RECEIPT";
                                _receiptAuto.Aut_number = 0;
                                _receiptAuto.Aut_start_char = "DIR";
                                _receiptAuto.Aut_year = DateTime.Now.Date.Year;
                            }

                        string _invoiceNo = "";
                        string _receiptNo = "";
                        string _deliveryOrder;
                        string _error;
                        string _buybackadj;

                        List<InvoiceSerial> _invoiceSerial = new List<InvoiceSerial>();
                        InventoryHeader _inventoryHeader = new InventoryHeader();
                        List<ReptPickSerials> _pickSerial = new List<ReptPickSerials>();
                        List<ReptPickSerialsSub> _pickSubSerial = new List<ReptPickSerialsSub>();

                        MasterAutoNumber _inventoryAuto = new MasterAutoNumber();
                        List<InvoiceVoucher> _voucher = new List<InvoiceVoucher>();
                        InventoryHeader _buybackheader = new InventoryHeader();
                        MasterAutoNumber _buybackauto = new MasterAutoNumber();
                        List<ReptPickSerials> _buybacklist = new List<ReptPickSerials>();



                        GEN_CUST_ENQSER _genCustEnqser = new GEN_CUST_ENQSER();
                        _genCustEnqser.GCS_LINE = 0;
                        _genCustEnqser.GCS_FAC = "TRANS";
                        _genCustEnqser.GCS_SERVICE = enqData.GCE_ENQ_SUB_TP;
                        _genCustEnqser.GCS_UNITS = enqData.GCE_REQ_NO_VEH;
                        _genCustEnqser.GCS_SER_PROVIDER = enqData.GEN_CUST_ENQSER.GCS_SER_PROVIDER;
                        _genCustEnqser.GCS_SER_COM = company;
                        _genCustEnqser.GCS_SER_PC = userDefPro;
                        _genCustEnqser.GCS_PICK_FRM = (enqData.GCE_FRM_ADD != null) ? enqData.GCE_FRM_ADD : "";
                        _genCustEnqser.GCS_PICK_TN = enqData.GCE_FRM_TN;
                        _genCustEnqser.GCS_EXP_DT = enqData.GCE_EXPECT_DT;
                        _genCustEnqser.GCS_EXP_TIME = enqData.GCE_EXPECT_DT;
                        _genCustEnqser.GCS_DROP = (enqData.GCE_TO_ADD != null) ? enqData.GCE_TO_ADD : "";
                        _genCustEnqser.GCS_DROP_TN = enqData.GCE_TO_TN;
                        _genCustEnqser.GCS_DROP_DT = enqData.GCE_RET_DT;
                        _genCustEnqser.GCS_DROP_TIME = enqData.GCE_RET_DT;
                        _genCustEnqser.GCS_VEH_TP = enqData.GCE_VEH_TP;
                        _genCustEnqser.GCS_COMMENT = enqData.GCE_ENQ;
                        // _genCustEnqser.GCS_CUS_TYPE = enqData.GCE_CUS_TYPE;
                        _genCustEnqser.GCS_STATUS = 1;
                        _genCustEnqser.GCS_CRE_BY = userId;
                        _genCustEnqser.GCS_CRE_DT = DateTime.Now;
                        _genCustEnqser.GCS_MOD_BY = userId;
                        _genCustEnqser.GCS_MOD_DT = DateTime.Now;



                        #endregion
                        //result = CHNLSVC.Tours.SaveTripRequestWithInvoice(oHeader, oMainInvoiceItems, null, _ReceiptHeader, _recieptItems, null, null, null, _invoiceAuto, _receiptAuto, null, false, out _invoiceNo, out _receiptNo, out _deliveryOrder, oCust, false, false, out _error, null, null, null, null, out _buybackadj, _ReqInsAuto, out err, isinvoicvesave, _genCustEnqser,oItem);
                        //result = CHNLSVC.Tours.SaveToursrInvoice(oHeadernew, oMainInvoiceItems, null, _ReceiptHeaderNew, _recieptItemsNew, null, null, null,null, _invoiceAuto, _receiptAuto, null, false, out _invoiceNo, out _receiptNo, out _deliveryOrder, oCust, false, false, out _error, null, null, null, null, out _buybackadj, _ReqInsAuto, out err, isinvoicvesave, _genCustEnqser, oItem);

                        //result = CHNLSVC.Tours.SaveToursrInvoice(oHeadernew, oMainInvoiceItems, null, _ReceiptHeaderNew, _recieptItemsNew, null, null, null, _invoiceAuto, _receiptAuto, null, false, out _invoiceNo, out _receiptNo, out  _deliveryOrder, oCust, false, false, out _error, null, null, null, null, out _buybackadj, false, _genCustEnqser, oItem);

                        if (oMainInvoiceItems == null || oMainInvoiceItems.Count == 0)
                        {
                            oHeadernew = null;
                        }

                        if (_recieptItemsNew.Count == 0)
                        {
                            _ReceiptHeaderNew = null;
                        }
                        if (invoicing == "False")
                        {
                            _ReceiptHeaderNew = null;
                            _recieptItemsNew = null;
                        }
                        MasterBusinessEntity _custProfile = new MasterBusinessEntity();
                        GroupBussinessEntity _custGroup = new GroupBussinessEntity();
                        if (String.IsNullOrEmpty(enqData.GCE_CUS_CD))
                        {
                            _custProfile.Mbe_acc_cd = null;
                            _custProfile.Mbe_act = true;
                            _custProfile.Mbe_add1 = (enqData.GCE_ADD1 != null) ? enqData.GCE_ADD1.Trim() : "";
                            _custProfile.Mbe_add2 = (enqData.GCE_ADD2 != null) ? enqData.GCE_ADD2.Trim() : "";
                            _custProfile.Mbe_cate = enqData.GCE_CUS_TYPE;
                            _custProfile.Mbe_com = company;
                            _custProfile.Mbe_cust_loc = userDefLoc;
                            _custProfile.Mbe_ho_stus = "GOOD";
                            _custProfile.Mbe_cre_by = userId;
                            _custProfile.Mbe_cre_dt = Convert.ToDateTime(DateTime.Today).Date;
                            _custProfile.Mbe_cre_pc = userDefPro;
                            _custProfile.Mbe_cust_com = company;
                            _custProfile.Mbe_cust_loc = userDefLoc;
                            _custProfile.Mbe_pc_stus = "GOOD";
                            _custProfile.Mbe_tp = "C";
                            _custProfile.Mbe_nationality = "SL";
                            _custProfile.Mbe_mob = (enqData.GCE_MOB != null) ? enqData.GCE_MOB.Trim() : "";
                            _custProfile.Mbe_name = (enqData.GCE_NAME != null) ? enqData.GCE_NAME.Trim() : "";
                            _custProfile.MBE_FNAME = (enqData.GCE_NAME != null) ? enqData.GCE_NAME.Trim() : "";
                            _custProfile.Mbe_nic = (enqData.GCE_NIC != null) ? enqData.GCE_NIC.Trim() : "";
                            _custProfile.Mbe_pp_no = (enqData.GCE_PP_NO != null) ? enqData.GCE_PP_NO.Trim() : "";
                            _custProfile.Mbe_cre_dt = DateTime.Now.Date;
                            _custProfile.Mbe_cre_by = userId;



                            _custGroup.Mbg_name = (enqData.GCE_NAME != null) ? enqData.GCE_NAME.Trim() : "";
                            _custGroup.Mbg_fname = (enqData.GCE_NAME != null) ? enqData.GCE_NAME.Trim() : "";
                            _custGroup.Mbg_add1 = (enqData.GCE_ADD1 != null) ? enqData.GCE_ADD1.Trim() : "";
                            _custGroup.Mbg_add2 = (enqData.GCE_ADD2 != null) ? enqData.GCE_ADD2.Trim() : "";
                            _custGroup.Mbg_mob = (enqData.GCE_MOB != null) ? enqData.GCE_MOB.Trim() : "";
                            _custGroup.Mbg_nic = (enqData.GCE_NIC != null) ? enqData.GCE_NIC.Trim() : "";
                            _custGroup.Mbg_pp_no = (enqData.GCE_PP_NO != null) ? enqData.GCE_PP_NO.Trim() : "";
                            _custGroup.Mbg_contact = "";
                            _custGroup.Mbg_act = true;
                            _custGroup.Mbg_nationality = "SL";
                            _custGroup.Mbg_is_suspend = false;
                            _custGroup.Mbg_cre_by = userId;
                            _custGroup.Mbg_mod_dt = DateTime.Now.Date;


                        }
                        else {
                            _custProfile.Mbe_cd = enqData.GCE_CUS_CD;
                            _custProfile.Mbe_mob = (enqData.GCE_MOB != null) ? enqData.GCE_MOB.Trim() : "";
                            _custProfile.Mbe_name = (enqData.GCE_NAME != null) ? enqData.GCE_NAME.Trim() : "";
                            _custProfile.MBE_FNAME = (enqData.GCE_NAME != null) ? enqData.GCE_NAME.Trim() : "";
                            _custProfile.Mbe_nic = (enqData.GCE_NIC != null) ? enqData.GCE_NIC.Trim() : "";
                            _custProfile.Mbe_pp_no = (enqData.GCE_PP_NO != null) ? enqData.GCE_PP_NO.Trim() : "";
                            _custProfile.Mbe_add1 = (enqData.GCE_ADD1 != null) ? enqData.GCE_ADD1.Trim() : "";
                            _custProfile.Mbe_add2 = (enqData.GCE_ADD2 != null) ? enqData.GCE_ADD2.Trim() : "";
                            _custProfile.Mbe_com = company;
                            _custProfile.Mbe_act = true;


                            _custGroup.Mbg_cd = enqData.GCE_CUS_CD;
                            _custGroup.Mbg_name = (enqData.GCE_NAME != null) ? enqData.GCE_NAME.Trim() : "";
                            _custGroup.Mbg_fname = (enqData.GCE_NAME != null) ? enqData.GCE_NAME.Trim() : "";
                            _custGroup.Mbg_add1 = (enqData.GCE_ADD1 != null) ? enqData.GCE_ADD1.Trim() : "";
                            _custGroup.Mbg_add2 = (enqData.GCE_ADD2 != null) ? enqData.GCE_ADD2.Trim() : "";
                            _custGroup.Mbg_mob = (enqData.GCE_MOB != null) ? enqData.GCE_MOB.Trim() : "";
                            _custGroup.Mbg_nic = (enqData.GCE_NIC != null) ? enqData.GCE_NIC.Trim() : "";
                            _custGroup.Mbg_pp_no = (enqData.GCE_PP_NO != null) ? enqData.GCE_PP_NO.Trim() : "";
                            _custGroup.Mbg_act = true;
                        }
                        List<GEN_CUS_ENQ_DRIVER> driverList = new List<GEN_CUS_ENQ_DRIVER>();
                        if (Session["ENQDRIVER"] != null)
                        {
                            driverList = (List<GEN_CUS_ENQ_DRIVER>)Session["ENQDRIVER"];
                        }
                        else
                        {
                            driverList = new List<GEN_CUS_ENQ_DRIVER>();
                        }
                        List<GEN_CUS_ENQ_FLEET> vehicleList = new List<GEN_CUS_ENQ_FLEET>();
                         if (Session["ENQVEHICLE"] != null)
                        {
                            vehicleList = (List<GEN_CUS_ENQ_FLEET>)Session["ENQVEHICLE"];
                        }
                        else
                        {
                            vehicleList = new List<GEN_CUS_ENQ_FLEET>();
                        }


                         result = CHNLSVC.Tours.SaveToursrInvoiceTransport(oHeadernew, oMainInvoiceItems, null, _ReceiptHeaderNew, _recieptItemsNew, null, null, null, _invoiceAuto, _receiptAuto, _inventoryAuto, false, out _invoiceNo, out _receiptNo, out _deliveryOrder, oCust, false, false, out _error, null, null, null, null, out _buybackadj, false, _genCustEnqser, oItem, _ReqInsAuto, null, true, _custProfile, _custGroup, enqChrgItems, driverList, vehicleList);
                        if (result == 1)
                        {
                            if (!string.IsNullOrEmpty(enqData.GCE_DRIVER))
                            {
                                MST_EMPLOYEE_TBS driver = CHNLSVC.Tours.Get_mst_employee(enqData.GCE_DRIVER);
                                OutSMS _out = new OutSMS();
                                _out.Msg = "Your vehicle details as follows.  " +
                                 "\nVehicle # : " + enqData.GCE_FLEET +
                                 "\nDriver Name : " + drivername +
                                 "\nDriver contact # : " + drivercontact;
                                _out.Msgstatus = 0;
                                _out.Msgtype = "S";
                                _out.Receivedtime = DateTime.Now;
                                _out.Receiver = "";
                                _out.Receiverphno = driver.MEMP_MOBI_NO;
                                // _out.Receiverphno = "+94712115036";
                                _out.Senderphno = "+94712115036";
                                _out.Refdocno = "0";
                                _out.Sender = "Message Agent";
                                _out.Createtime = DateTime.Now;

                                int smsResult = CHNLSVC.General.SaveSMSOut(_out);
                            }
                            if (!string.IsNullOrEmpty(enqData.GCE_CUS_CD))
                            {
                                // MasterBusinessEntity custProf = GetbyCustCD(txtCustomer.Text.Trim().ToUpper());
                                OutSMS _out = new OutSMS();
                                _out.Msg = "Your vehicle details as follows.  " +
                                 "\nVehicle # : " + enqData.GCE_FLEET +
                                 "\nDriver Name : " + drivername +
                                 "\nDriver contact # : " + drivercontact;
                                _out.Msgstatus = 0;
                                _out.Msgtype = "S";
                                _out.Receivedtime = DateTime.Now;
                                _out.Receiver = "";
                                _out.Receiverphno = custProf.Mbe_mob;
                                // _out.Receiverphno = "+94712115036";
                                _out.Senderphno = "+94712115036";
                                _out.Refdocno = "0";
                                _out.Sender = "Message Agent";
                                _out.Createtime = DateTime.Now;
                                int smsResult = CHNLSVC.General.SaveSMSOut(_out);
                            }

                        }
                        if (result > 0)
                        {
                            if (isinvoicvesave)
                            {
                                _error = _error + ((_invoiceNo != "") ? "  Invoice No :" + _invoiceNo : "") + ((_receiptNo != "") ? "  Receipt No :" + _receiptNo : "");
                                return Json(new { success = true, login = true, msg = _error }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(_error))
                                {
                                    return Json(new { success = true, login = true, msg = _error }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = true, login = true, msg = "Successfully Updated." }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Error Occurred." + _error, type = "Error" }, JsonRequestBehavior.AllowGet);
                        }
                        #endregion
                    }
                    else
                    {
                        if (totalPayed < totRcpVal)
                        {
                            return Json(new { success = false, login = true, msg = "Exceed the pay amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Please pay balance amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getEnquiryData(string enqId)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (!string.IsNullOrEmpty(enqId))
                    {
                        enqId = enqId.Trim();

                        GEN_CUST_ENQ oItem = CHNLSVC.Tours.getEnquiryDetailsTours(company, userDefPro, enqId);
                        List<RecieptItemTBS> recItems = new List<RecieptItemTBS>();
                        if (oItem != null && oItem.GCE_ENQ_ID != null)
                        {
                            if (oItem.CHARGER_VALUE != null)
                            {
                                if (oItem.CHARGER_VALUE.Count > 0)
                                {
                                    recItems = CHNLSVC.Tours.getReceiptItemByinvNo(oItem.CHARGER_VALUE[0].Sad_inv_no, company, userDefPro);
                                }
                            }

                            Session["enqChrgItems"] = oItem.ENQ_CHARGES;
                            Session["ENQDRIVER"] = null;
                            Session["ENQVEHICLE"] = null;
                            List<GEN_CUS_ENQ_DRIVER> drivers = CHNLSVC.Tours.getEnquiryDriverDetails(enqId);
                            Session["ENQDRIVER"] = drivers;
                            Int32 driCnt = 0;
                            foreach (GEN_CUS_ENQ_DRIVER dri in drivers)
                            {
                                if (dri.GCD_ACT == 1) {
                                    driCnt += 1;
                                }
                            }
                            List<GEN_CUS_ENQ_FLEET> FLEET = CHNLSVC.Tours.getEnquiryFleetDetails(enqId);
                            Session["ENQVEHICLE"] = FLEET;
                            Int32 vehCnt = 0;
                            foreach (GEN_CUS_ENQ_FLEET fle in FLEET)
                            {
                                if (fle.GCF_ACT == 1)
                                {
                                    vehCnt += 1;
                                }
                            }

                            return Json(new { success = true, login = true, data = oItem, recItems = recItems, driCnt = driCnt, vehCnt = vehCnt }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid enquiry id.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        private bool isdecimal(string txt)
        {
            decimal val;
            return decimal.TryParse(txt, out val);
        }
        private bool isInteger(string txt)
        {
            int val;
            return int.TryParse(txt, out val);
        }
        public JsonResult removeChargeItem(string chgCd, string pax, string totcst,string invoicing)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                if (invoicing == "False")
                {
                    if (Session["enqChrgItems"] != null)
                    {
                        enqChrgItems = (List<ST_ENQ_CHARGES>)Session["enqChrgItems"];
                    }
                    else
                    {
                        enqChrgItems = new List<ST_ENQ_CHARGES>();

                    }
                    var itemToRemove = enqChrgItems.First(r => r.SCH_ITM_CD == chgCd);
                    enqChrgItems.Remove(itemToRemove);
                    Session["enqChrgItems"] = enqChrgItems;
                    decimal totAmt = enqChrgItems.Where(m => m.SCH_INVOICED == 0).Sum(n => n.SCH_TOT_AMT);

                    DEPO_AMT_DATA existslibData = new DEPO_AMT_DATA();

                    existslibData = CHNLSVC.Tours.getLiabilityDatabyChgCd(chgCd);
                    if (existslibData.GCD_DAILY_RNT_CD != null)
                    {
                        Session["libData"] = null;
                    }
                    return Json(new { success = true, login = true, data = enqChrgItems, totAmt = totAmt, existslibData = existslibData }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { success = false, login = true, msg = "Unable to remove invoicing items.", data = "", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, login = true, data = Resource.ServerError }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult validateCountry(string country) { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_COUNTRY> countryList= CHNLSVC.Tours.getCountryDetails(country);
                    if (countryList.Count > 0) {
                        return Json(new { success = true, login = true, countryList = countryList[0]}, JsonRequestBehavior.AllowGet);
                    }else{
                        return Json(new { success = false, login = true,msg="Invalid country code.",type="Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult checkFacLocAvailability() {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_FAC_LOC> facList = CHNLSVC.Tours.getFacLocations(company,userDefPro);
                    if (facList.Count > 0)
                    {
                        return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
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
                        List<MST_FAC_LOC> facList = CHNLSVC.Tours.getFacLocations(company, userDefPro);
                        if (facList.Count > 0)
                        {
                            bool exists = facList.Exists(element => element.FAC_CODE == val);
                            if (!exists)
                            {
                                return Json(new { success = false, login = true, msg = Resource.txtInvTown, type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            else {
                                return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                            }
                           
                        }
                        else
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
                                    return Json(new { success = false, login = true, msg = Resource.txtInvTown, type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = Resource.txtInvTown, type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult ppNoTextChanged(string ppNo)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    ppNo=ppNo.Trim();
                    if (!string.IsNullOrEmpty(ppNo))
                    {
                        MasterBusinessEntity custProf = CHNLSVC.Sales.GetCustomerProfileByCom(null, null, null, ppNo, null, company);
                        if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                        {
                            return Json(new { success = true, local = true, login = true, data = custProf }, JsonRequestBehavior.AllowGet);
                        }
                        else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                        {
                            return Json(new { success = false, login = true, msg = "Invalid passport number.", data = "", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            GroupBussinessEntity _grupProf =CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, null, ppNo, null, null);
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
                                return Json(new { success = true, group = true, login = true, data = _grupProf }, JsonRequestBehavior.AllowGet);
                            }
                            else if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == false)
                            {
                                return Json(new { success = false, login = true, msg = "Invalid passport number.", data = "" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "Invalid passport number.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult mobileTextChanged(string mobile) {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    mobile = mobile.Trim();
                    if (!string.IsNullOrEmpty(mobile))
                    {
                        List<MasterBusinessEntity> custProf = CHNLSVC.Sales.GetActiveCustomerDetailList(null, company, null, mobile, "C");
                        if (custProf.Count > 1 && custProf != null && mobile != "N/A")
                        {
                            string _custMob = "Duplicate customers found ";
                            foreach (var _nicCust in custProf)
                            {
                                _custMob = _custMob + _nicCust.Mbe_cd.ToString() + " | " + _nicCust.Mbe_name.ToString() + "\n";
                            }
                            return Json(new { success = false, login = true, msg = _custMob, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }

                        if (custProf.Count > 0 && custProf[0].Mbe_cd != null && custProf[0].Mbe_act == true)
                        {
                            return Json(new { success = true, local = true, login = true, data = custProf[0] }, JsonRequestBehavior.AllowGet);
                        }
                        else if (custProf.Count > 0 && custProf[0].Mbe_cd != null && custProf[0].Mbe_act == false)
                        {
                            return Json(new { success = false, login = true, status = 2, msg = Resource.txtInacCus, type = "Info", data = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {

                            GroupBussinessEntity _grupProf = CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, null, null, null, mobile);
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
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
        public JsonResult LoadCustomerType()
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
                    List<MST_CUSTOMER_TYPE> types = CHNLSVC.Tours.getCustomerTypes();
                    if (types.Count > 0)
                    {
                        foreach (MST_CUSTOMER_TYPE typ in types)
                        {
                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = typ.CUS_TP_DESC;
                            o1.Value = typ.CUS_TP_CD;
                            oList.Add(o1);
                        }
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
                return Json(new { success = false, login = true, data = Resource.ServerError }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult enquiryCancel(string enqId,string remark) {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    enqId = enqId.Trim();
                    remark = remark.Trim();

                    if (enqId != "" && remark != "")
                    {

                        if (Session["oMainInvoiceItems"] != null)
                        {
                            oMainInvoiceItems = (List<InvoiceItem>)Session["oMainInvoiceItems"];
                        }
                        else
                        {
                            oMainInvoiceItems = new List<InvoiceItem>();

                        }
                        List<RecieptItemTBS> sesRecieptItemList = null;
                        if (Session["RecieptItemList"] != null)
                        {
                            sesRecieptItemList = Session["RecieptItemList"] as List<RecieptItemTBS>;
                        }
                        else {
                            sesRecieptItemList = new List<RecieptItemTBS>();
                        }
                        if (oMainInvoiceItems.Count > 0) {
                            return Json(new { success = false, login = true, msg = "Unable to cancel charg added enquiry.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (sesRecieptItemList.Count > 0) {
                            return Json(new { success = false, login = true, msg = "Unable to cancel invoiced enquiry.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        MST_PCADDPARA addPa = CHNLSVC.Tours.getPcAdditionalPara(company, userDefPro, "ENQ_PENCNCL_LIMIT");
                        if (addPa.PARA_KEY != null)
                        {

                            GEN_CUST_ENQ oItem = CHNLSVC.Tours.getEnquiryDetails(company, userDefPro, enqId);


                            DateTime dateForButton = oItem.GCE_EXPECT_DT.AddDays(-addPa.PARA_VALUE);
                            if (dateForButton > DateTime.Now)
                            {
                                Int32 effect = CHNLSVC.Tours.UPDATE_ENQ_STATUS_WITH_REASON(enqId, remark);
                                if (effect > 0)
                                {
                                    return Json(new { success = true, login = true, msg = "Enquiry cancel success.", type = "Success" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Unable to cancel enquiry.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else {
                                return Json(new { success = false, login = true, msg = "Unable to cancel enquiry.Enquiry can cancel before " + addPa.PARA_VALUE + "  days expected.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            
                        }
                        else
                        {
                            Int32 effect = CHNLSVC.Tours.UPDATE_ENQ_STATUS_WITH_REASON(enqId, remark);
                            if (effect > 0)
                            {
                                return Json(new { success = true, login = true, msg = "Enquiry cancel success.", type = "Success" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "Unable to cancel enquiry.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else {
                        return Json(new { success = false, login = true, msg = "Please enter enquiry id and remarks.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getUpdatedCharges(string enqId) { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (enqId != "")
                    {
                        List<ST_ENQ_CHARGES> charges = new List<ST_ENQ_CHARGES>();
                        if (Session["enqChrgItems"] != null)
                        {
                            charges = (List<ST_ENQ_CHARGES>)Session["enqChrgItems"];
                            Session["oldEnqChrgItems"] = charges;
                        }
                        else
                        {
                            charges = new List<ST_ENQ_CHARGES>();
                            Session["oldEnqChrgItems"] = null;
                        }
                        
                        List<ST_ENQ_CHARGES> newEnqChrgItems = new List<ST_ENQ_CHARGES>();
                        if (charges.Count > 0)
                        {
                            MST_CHKINOUT chkOut = CHNLSVC.Tours.getEnqChkData(enqId);
                            if (chkOut != null && chkOut.CHK_ENQ_ID != null)
                            {
                                SR_SER_MISS oSR_SER_MISS = new SR_SER_MISS();
                                SR_TRANS_CHA oSR_AIR_CHARGE = new SR_TRANS_CHA();
                                foreach (ST_ENQ_CHARGES item in charges)
                                {
                                    ST_ENQ_CHARGES newItem = new ST_ENQ_CHARGES();
                                    newItem.SCH_SEQ_NO = item.SCH_SEQ_NO;
                                    newItem.SCH_ITM_SERVICE = item.SCH_ITM_SERVICE;
                                    newItem.SCH_ITM_CD = item.SCH_ITM_CD;
                                    newItem.SCH_ITM_STUS = item.SCH_ITM_STUS;
                                    newItem.SCH_ITM_TP = item.SCH_ITM_TP;
                                    newItem.SCH_QTY = item.SCH_QTY;
                                    newItem.SCH_UNIT_RT = item.SCH_UNIT_RT;
                                    newItem.SCH_UNIT_AMT = item.SCH_UNIT_AMT;
                                    newItem.SCH_DISC_RT = item.SCH_DISC_RT;
                                    newItem.SCH_DISC_AMT = item.SCH_DISC_AMT;
                                    newItem.SCH_ITM_TAX_AMT = item.SCH_ITM_TAX_AMT;
                                    newItem.SCH_TOT_AMT = item.SCH_TOT_AMT;
                                    newItem.SCH_ENQ_NO = item.SCH_ENQ_NO;
                                    newItem.SCH_EX_RT = item.SCH_EX_RT;
                                    newItem.SCH_CURR = item.SCH_CURR;
                                    newItem.SCH_INVOICED = item.SCH_INVOICED;
                                    newItem.SCH_INVOICED_NO = item.SCH_INVOICED_NO;
                                    newItem.SCH_ALT_ITM_DESC = item.SCH_ALT_ITM_DESC;

                                    if (item.SCH_INVOICED == 0)
                                    {
                                        if (item.SCH_ITM_SERVICE == "O")
                                        {
                                            oSR_SER_MISS = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, "MSCELNS", item.SCH_ITM_CD, userDefPro);
                                            if (oSR_SER_MISS == null && oSR_SER_MISS.SSM_CD == null)
                                            {
                                                return Json(new { success = false, login = true, msg = "Invalid charge codes contain in the list.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }
                                            else
                                            {
                                                if (oSR_SER_MISS.SSM_PERDAY_RTE != 0)
                                                {
                                                    decimal chargDates = Convert.ToDecimal((oSR_SER_MISS.SSM_TO_DT - oSR_SER_MISS.SSM_FRM_DT).TotalDays);
                                                   // decimal checkDates = Convert.ToDecimal((chkOut.CHK_IN_DTE - chkOut.CHK_OUT_DTE).TotalDays);
                                                    decimal extraDateCharges = chargDates * oSR_SER_MISS.SSM_PERDAY_RTE;
                                                       // (checkDates > chargDates) ? (checkDates - chargDates) * oSR_SER_MISS.SSM_PERDAY_RTE : 0;
                                                    newItem.SCH_TOT_AMT = extraDateCharges;//newItem.SCH_TOT_AMT + extraDateCharges;

                                                    newEnqChrgItems.Add(newItem);
                                                }
                                                else
                                                {
                                                    newEnqChrgItems.Add(newItem);
                                                }
                                            }
                                        }
                                        else if (item.SCH_ITM_SERVICE == "T")
                                        {
                                            oSR_AIR_CHARGE = CHNLSVC.Tours.GetTransferChargeDetailsByCode(company, "TRANS", item.SCH_ITM_CD, userDefPro);
                                            if (oSR_AIR_CHARGE == null && oSR_AIR_CHARGE.STC_CD == null)
                                            {
                                                return Json(new { success = false, login = true, msg = "Invalid charge codes contain in the list.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }
                                            else
                                            {
                                                if (oSR_AIR_CHARGE.STC_AD_RT != 0)
                                                {
                                                    decimal chrgKm = oSR_AIR_CHARGE.STC_TO_KM - oSR_AIR_CHARGE.STC_FRM_KM;
                                                    decimal checkedKm = chkOut.CHK_IN_KM - chkOut.CHK_OUT_KM;
                                                    decimal extraKmChrges = (checkedKm > chrgKm) ? (checkedKm - chrgKm) * oSR_AIR_CHARGE.STC_AD_RT : 0;
                                                    newItem.SCH_TOT_AMT = newItem.SCH_TOT_AMT + extraKmChrges;
                                                    newEnqChrgItems.Add(newItem);
                                                }
                                                else
                                                {
                                                    newEnqChrgItems.Add(newItem);
                                                }

                                            }
                                        }
                                    }
                                }

                                Session["enqChrgItems"] = newEnqChrgItems;
                                decimal totAmt = newEnqChrgItems.Sum(n => n.SCH_TOT_AMT);
                                return Json(new { success = true, login = true, data = newEnqChrgItems, totAmt = totAmt }, JsonRequestBehavior.AllowGet);
                            }
                            else {
                                return Json(new { success = false, login = true, msg = "Please add chekin and out details for invoice.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Please add charges for invoice.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
                        return Json(new { success = false, login = true, msg = "Please select enquiry id.", type = "Info" }, JsonRequestBehavior.AllowGet);
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

        public JsonResult getOldCharges()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<ST_ENQ_CHARGES> oldCharges = new List<ST_ENQ_CHARGES>();
                    if (Session["oldEnqChrgItems"] != null)
                    {
                        oldCharges = (List<ST_ENQ_CHARGES>)Session["oldEnqChrgItems"];
                        Session["enqChrgItems"] = oldCharges;
                    }
                    else
                    {
                        oldCharges = new List<ST_ENQ_CHARGES>();
                        Session["enqChrgItems"] = null;

                    }
                    return Json(new { success = true, login = true, data = oldCharges, totAmt = 0 }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getVehicleTypes() { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<ST_VEHICLE_TP> types =CHNLSVC.Tours.getVehicleTypes();
                    return Json(new { success = true, login = true,data=types }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getPkgTypes()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<ST_PKG_TP> types = CHNLSVC.Tours.getpKGTypes();
                    return Json(new { success = true, login = true, data = types }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult addEnquiryDrivers(string driver, string fromdt, string todt, string GCE_EXPECT_DT, string GCE_RET_DT,string enqId)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<GEN_CUS_ENQ_DRIVER> driverList = null;
                    driver = driver.Trim();
                    MST_EMPLOYEE_NEW_TBS employees = CHNLSVC.Tours.getMstEmployeeDetails(driver);
                    if (employees == null || employees.MEMP_CD == null)
                    {
                        return Json(new { success = true, login = true, msg = "Please enter valid driver code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    List<mst_fleet_driver> driverallow = CHNLSVC.Tours.getAlowcatedFleetAndDriverDetails(driver, "", Convert.ToDateTime(fromdt), Convert.ToDateTime(todt), "DRIVER");
                    if (driverallow.Count > 0)
                    {
                        return Json(new { success = false, login = true, msg = "Driver already assigned for vehicle :" + driverallow[0].MFD_VEH_NO + " for this date range.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (Session["ENQDRIVER"] != null)
                    {
                        driverList = (List<GEN_CUS_ENQ_DRIVER>)Session["ENQDRIVER"];
                    }
                    else {
                        driverList = new List<GEN_CUS_ENQ_DRIVER>();
                    }
                    DateTime frmDt = Convert.ToDateTime(fromdt);
                    DateTime tDt = Convert.ToDateTime(todt);
                    if (frmDt > tDt)
                    {
                        return Json(new { success = false, login = true, msg = "From Date canot greater than from date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    DateTime expDt = Convert.ToDateTime(Convert.ToDateTime(GCE_EXPECT_DT).ToString("dd/MMM/yyyy"));
                    DateTime rtDt = Convert.ToDateTime(Convert.ToDateTime(GCE_RET_DT).ToString("dd/MMM/yyyy"));
                    if (frmDt < expDt || frmDt > rtDt || rtDt < tDt || rtDt < expDt)
                    {
                        return Json(new { success = false, login = true, msg = "Date range not maching with the expected and return dates.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    if (driverList.Count > 0) {
                        bool has = driverList.Any(d => d.GCD_DRIVER_CD == driver && d.GCD_ACT==1 &&  ( (d.GCD_FROM_DT <= frmDt && frmDt <= d.GCD_TO_DT) || (d.GCD_FROM_DT <= tDt && tDt <= d.GCD_TO_DT) || (d.GCD_FROM_DT >= frmDt && tDt >= d.GCD_TO_DT)));
                        if (has) {
                            return Json(new { success = false, login = true, msg = "Driver already allocate for this date ranges.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        bool used = driverList.Any(d => d.GCD_ACT == 1 && ((d.GCD_FROM_DT <= frmDt && frmDt <= d.GCD_TO_DT) || (d.GCD_FROM_DT <= tDt && tDt <= d.GCD_TO_DT) || (d.GCD_FROM_DT >= frmDt && tDt >= d.GCD_TO_DT)));
                        if (used) {
                            return Json(new { success = false, login = true, msg = "Some drivers already allocate for this date ranges.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    Int32 amentment = 0;
                    List<GEN_CUS_ENQ_DRIVER> enqDrivaerAlow = CHNLSVC.Tours.getAlowcatedFleetAndDriverDetailsInEnquiry(driver, expDt, rtDt);
                    if (enqDrivaerAlow.Count > 0)
                    {
                        string ids = "";
                        int i = 1;
                        foreach (GEN_CUS_ENQ_DRIVER enq in enqDrivaerAlow)
                        {
                            ids += enq.GCD_ENQ_NO;
                            ids += (ids != "") ? (i == enqDrivaerAlow.Count) ? "," : "" : "";
                            i++;
                        }
                        if (ids != "")
                        {
                            return Json(new { success = false, login = true, msg = "Driver already assigned for enquies :" + ids + " for this date range.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        
                    }
                    GEN_CUS_ENQ_DRIVER driverVal = new GEN_CUS_ENQ_DRIVER();
                    List<GEN_CUS_ENQ_DRIVER> drivers = CHNLSVC.Tours.getEnquiryDriverDetails(enqId);
                    if (enqId != "" && drivers.Count > 0)
                    {
                        amentment = drivers.Max(d => d.GCD_AMENTMENT);
                        if (amentment == 0)
                        {
                            amentment = 1;
                        }
                        else
                        {
                            amentment = amentment + 1;
                        }
                        decimal actlCst = drivers.Average(c => c.GCD_DRIVER_COST);
                        driverVal.GCD_ACTL_DRIVER_COST = actlCst;
                    }
                    else {
                        driverVal.GCD_ACTL_DRIVER_COST = employees.MEMP_COST;
                    }
                    driverVal.GCD_DRIVER_COST = employees.MEMP_COST;
                    driverVal.GCD_DRIVER_CD = driver;
                    driverVal.GCD_FROM_DT = Convert.ToDateTime(fromdt);
                    driverVal.GCD_TO_DT = Convert.ToDateTime(todt);
                    driverVal.GCD_DRIVER_NAME = employees.MEMP_FIRST_NAME;
                    driverVal.GCD_DRIVER_CONTACT = employees.MEMP_MOBI_NO;
                    driverVal.GCD_AMENTMENT = amentment;
                    driverVal.GCD_ACT = 1;
                    driverVal.GCD_ADDED_DT=DateTime.Now;
                    driverVal.GCD_MOD_DT = DateTime.Now;
                    driverVal.GCD_ADDED_BY = userId;
                    driverVal.GCD_MOD_BY = userId;
                    driverList.Add(driverVal);
                    Session["ENQDRIVER"] = driverList;
                    return Json(new { success = true, login = true, driverList = driverList }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getAssigedDrivers() { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<GEN_CUS_ENQ_DRIVER> driverList = new List<GEN_CUS_ENQ_DRIVER>();
                    if (Session["ENQDRIVER"] != null)
                    {
                        driverList = (List<GEN_CUS_ENQ_DRIVER>)Session["ENQDRIVER"];
                    }
                    else
                    {
                        driverList = new List<GEN_CUS_ENQ_DRIVER>();
                    }
                    return Json(new { success = true, login = true, driverList = driverList }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult removeEnquiryDrivers( string  driver, string  fromdt, string  todt, string  enqId) { 
        string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<GEN_CUS_ENQ_DRIVER> driverList = new List<GEN_CUS_ENQ_DRIVER>();
                    if (Session["ENQDRIVER"] != null)
                    {
                        driverList = (List<GEN_CUS_ENQ_DRIVER>)Session["ENQDRIVER"];
                    }
                    else
                    {
                        driverList = new List<GEN_CUS_ENQ_DRIVER>();
                    }
                    if (driverList.Count > 0)
                    {
                        DateTime fmdt = Convert.ToDateTime(fromdt.Trim());
                        DateTime tdt= Convert.ToDateTime(todt.Trim());
                        if(enqId.Trim()==""){
                            enqId=null;
                        }
                        driverList.First(a => a.GCD_DRIVER_CD == driver.Trim() && a.GCD_FROM_DT == fmdt && a.GCD_TO_DT == tdt && a.GCD_ENQ_NO == enqId).GCD_ACT = 0;
                        List<GEN_CUS_ENQ_DRIVER> drivers = CHNLSVC.Tours.getEnquiryDriverDetails(enqId);
                        Int32 amentment = 0;
                        if (enqId != "" && drivers.Count > 0)
                        {
                            amentment = drivers.Max(d => d.GCD_AMENTMENT);
                            if (amentment == 0)
                            {
                                amentment = 1;
                            }
                            else
                            {
                                amentment = amentment + 1;
                            }
                        }
                        driverList.First(a => a.GCD_DRIVER_CD == driver.Trim() && a.GCD_FROM_DT == fmdt && a.GCD_TO_DT == tdt && a.GCD_ENQ_NO == enqId).GCD_AMENTMENT = amentment;
                        Session["ENQDRIVER"] = driverList;
                    }
                    return Json(new { success = true, login = true, driverList = driverList }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
                
        }

        public JsonResult getAssigedVehicles()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<GEN_CUS_ENQ_FLEET> fleetList = new List<GEN_CUS_ENQ_FLEET>();
                    if (Session["ENQVEHICLE"] != null)
                    {
                        fleetList = (List<GEN_CUS_ENQ_FLEET>)Session["ENQVEHICLE"];
                    }
                    else
                    {
                        fleetList = new List<GEN_CUS_ENQ_FLEET>();
                    }
                    return Json(new { success = true, login = true, fleetList = fleetList }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult addEnquiryVehicles(string vehiNo, string fromdt, string todt, string GCE_EXPECT_DT, string GCE_RET_DT, string enqId)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<GEN_CUS_ENQ_FLEET> vehicleList = null;
                    vehiNo = vehiNo.Trim();
                    MST_FLEET_NEW fleets = CHNLSVC.Tours.getMstFleetDetails(vehiNo);
                    if (fleets == null || fleets.MSTF_REGNO == null)
                    {
                        return Json(new { success = true, login = true, msg = "Please enter vehicle register number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    List<mst_fleet_driver> driverallow = CHNLSVC.Tours.getAlowcatedFleetAndDriverDetails("", vehiNo, Convert.ToDateTime(fromdt), Convert.ToDateTime(todt), "VEHICLE");
                    if (driverallow.Count > 0)
                    {
                        return Json(new { success = false, login = true, msg = "Vehicle already assigned for driver :" + driverallow[0].MFD_DRI + " for this date range.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (Session["ENQVEHICLE"] != null)
                    {
                        vehicleList = (List<GEN_CUS_ENQ_FLEET>)Session["ENQVEHICLE"];
                    }
                    else
                    {
                        vehicleList = new List<GEN_CUS_ENQ_FLEET>();
                    }
                    DateTime frmDt = Convert.ToDateTime(fromdt);
                    DateTime tDt = Convert.ToDateTime(todt);
                    if (frmDt > tDt)
                    {
                        return Json(new { success = false, login = true, msg = "From Date canot greater than from date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    DateTime expDt = Convert.ToDateTime(Convert.ToDateTime(GCE_EXPECT_DT).ToString("dd/MMM/yyyy"));
                    DateTime rtDt = Convert.ToDateTime(Convert.ToDateTime(GCE_RET_DT).ToString("dd/MMM/yyyy"));
                    if (frmDt < expDt || frmDt > rtDt || rtDt < tDt || rtDt < expDt)
                    {
                        return Json(new { success = false, login = true, msg = "Date range not maching with the expected and return dates.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    if (vehicleList.Count > 0)
                    {
                        bool has = vehicleList.Any(d => d.GCF_FLEET == vehiNo && d.GCF_ACT == 1 && ((d.GCF_FROM_DT <= frmDt && frmDt <= d.GCF_TO_DT) || (d.GCF_FROM_DT <= tDt && tDt <= d.GCF_TO_DT) || (d.GCF_FROM_DT >= frmDt && tDt >= d.GCF_TO_DT)));
                        if (has)
                        {
                            return Json(new { success = false, login = true, msg = "Vehicle already allocate for this date ranges.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        bool used = vehicleList.Any(d => d.GCF_ACT == 1 && ((d.GCF_FROM_DT <= frmDt && frmDt <= d.GCF_TO_DT) || (d.GCF_FROM_DT <= tDt && tDt <= d.GCF_TO_DT) || (d.GCF_FROM_DT >= frmDt && tDt >= d.GCF_TO_DT)));
                        if (used)
                        {
                            return Json(new { success = false, login = true, msg = "Some Vehicles already allocate for this date ranges.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    Int32 amentment = 0;
                    List<GEN_CUS_ENQ_FLEET> enqFleetAlow = CHNLSVC.Tours.getAlowcatedFleetDetailsInEnquiry(vehiNo, expDt, rtDt);
                    if (enqFleetAlow.Count > 0)
                    {
                        string ids = "";
                        int i = 1;
                        foreach (GEN_CUS_ENQ_FLEET enq in enqFleetAlow)
                        {
                            ids += enq.GCF_ENQ_NO;
                            ids += (ids != "") ? (i == enqFleetAlow.Count) ? "," : "" : "";
                            i++;
                        }
                        if (ids != "")
                        {
                            return Json(new { success = false, login = true, msg = "Vehicle already assigned for enquies :" + ids + " for this date range.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    GEN_CUS_ENQ_FLEET fleettVal = new GEN_CUS_ENQ_FLEET();
                    List<GEN_CUS_ENQ_FLEET> FLEET = CHNLSVC.Tours.getEnquiryFleetDetails(enqId);
                    if (enqId != "" && FLEET.Count > 0)
                    {
                        amentment = FLEET.Max(d => d.GCF_AMENTMENT);
                        if (amentment == 0)
                        {
                            amentment = 1;
                        }
                        else
                        {
                            amentment = amentment + 1;
                        }
                        decimal aclCst = FLEET.Average(c=>c.GCF_FLEET_COST);
                        fleettVal.GCF_ACTL_FLEET_COST = aclCst;
                    }
                    else {
                        fleettVal.GCF_ACTL_FLEET_COST = fleets.MSTF_COST;
                    }
                    fleettVal.GCF_FLEET = vehiNo;
                    fleettVal.GCF_FROM_DT = Convert.ToDateTime(fromdt);
                    fleettVal.GCF_TO_DT = Convert.ToDateTime(todt);
                    fleettVal.GCF_MODEL = fleets.MSTF_MODEL;
                    fleettVal.GCF_BRAND = fleets.MSTF_BRD;
                    fleettVal.GCF_AMENTMENT = amentment;
                    fleettVal.GCF_ACT = 1;
                    
                    fleettVal.GCF_ADDED_DT = DateTime.Now;
                    fleettVal.GCF_MOD_DT = DateTime.Now;
                    fleettVal.GCF_ADDED_BY = userId;
                    fleettVal.GCF_MOD_BY = userId;
                    vehicleList.Add(fleettVal);
                    Session["ENQVEHICLE"] = vehicleList;
                    return Json(new { success = true, login = true, vehicleList = vehicleList }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult removeEnquiryVehicles(string fleet, string fromdt, string todt, string enqId)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<GEN_CUS_ENQ_FLEET> vehicleList = new List<GEN_CUS_ENQ_FLEET>();
                    if (Session["ENQVEHICLE"] != null)
                    {
                        vehicleList = (List<GEN_CUS_ENQ_FLEET>)Session["ENQVEHICLE"];
                    }
                    else
                    {
                        vehicleList = new List<GEN_CUS_ENQ_FLEET>();
                    }
                    if (vehicleList.Count > 0)
                    {
                        DateTime fmdt = Convert.ToDateTime(fromdt.Trim());
                        DateTime tdt = Convert.ToDateTime(todt.Trim());
                        if (enqId.Trim() == "")
                        {
                            enqId = null;
                        }
                        vehicleList.First(a => a.GCF_FLEET == fleet.Trim() && a.GCF_FROM_DT == fmdt && a.GCF_TO_DT == tdt && a.GCF_ENQ_NO == enqId).GCF_ACT = 0;
                        List<GEN_CUS_ENQ_FLEET> FLEET = CHNLSVC.Tours.getEnquiryFleetDetails(enqId);
                        Int32 amentment = 0;
                        if (enqId != "" && FLEET.Count > 0)
                        {
                            amentment = FLEET.Max(d => d.GCF_AMENTMENT);
                            if (amentment == 0)
                            {
                                amentment = 1;
                            }
                            else
                            {
                                amentment = amentment + 1;
                            }
                        }
                        vehicleList.First(a => a.GCF_FLEET == fleet.Trim() && a.GCF_FROM_DT == fmdt && a.GCF_TO_DT == tdt && a.GCF_ENQ_NO == enqId).GCF_AMENTMENT = amentment;
                        Session["ENQVEHICLE"] = vehicleList;
                    }
                    return Json(new { success = true, login = true, vehicleList = vehicleList }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public ActionResult ReservationInvoicingReport(string enqNo = null)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                enqNo = (enqNo != null) ? enqNo.Trim() : null;
                //invNo = "TINVO00024";
                if (enqNo != null)
                {
                    string content;
                    PrintModel model = new PrintModel();
                    content = Url.Content("~/Print_Module/Print_Viwer/ReservationInvoicePrintViwer.aspx");
                    model.ReportPath = content;
                    model.enquiryId = enqNo;
                    Session["enqNo"] = enqNo;
                    return View("ReservationInvoicingReport", model);
                }
                else
                {
                    return Redirect("~/ReservationManagement");
                }

            }
            else
            {
                return Redirect("~/Login");
            }

        }
        public JsonResult validateLiability(string liability)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    DEPO_AMT_DATA data = new DEPO_AMT_DATA();
                    if (liability != "")
                    {
                        
                        data = CHNLSVC.Tours.getLiabilityDetails(liability);
                        if (data.GCD_CD != "")
                        {
                            return Json(new { success = true, login = true, data = data }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg="Invalid amount entered." }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    return Json(new { success = false, login = true, data = data}, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }

        }
    }
}