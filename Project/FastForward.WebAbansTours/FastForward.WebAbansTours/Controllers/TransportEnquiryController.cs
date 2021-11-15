using CrystalDecisions.CrystalReports.Engine;
using FF.BusinessObjects;
using FF.BusinessObjects.Tours;
using FF.BusinessObjects.ToursNew;
using FF.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FastForward.WebAbansTours.Controllers
{
    public class TransportEnquiryController : BaseController
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
                ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1015);
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
                            return Json(new { success = false, login = true, msg = Resource.txtInacCus, data = "",type="Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult loadTransportEnqData() { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string Status = "0,1,2,3,4,5,6,7,8";
                    string type = "TNSPT";
                    //List<GEN_CUST_ENQ> oItems = CHNLSVC.Tours.GET_ENQRY_BY_PC_STATUS(company, userDefPro, Status, userId, 15001);
                    List<GEN_CUST_ENQ> oItems = CHNLSVC.Tours.SP_TOUR_GET_TRANSPORT_ENQRY(company, userDefPro, Status, type, userId, 15001);
                    if (oItems != null && oItems.Count > 0)
                    {
                        return Json(new { success = true, login = true, data = oItems }, JsonRequestBehavior.AllowGet);
                    }
                    else {
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError ,type="Error"}, JsonRequestBehavior.AllowGet);
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
        public JsonResult loadChargCode(string code)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    code=code.Trim();
                    if (code != "")
                    {
                        SR_TRANS_CHA oSR_AIR_CHARGE = CHNLSVC.Tours.GetTransferChargeDetailsByCode(company, "TRANS", code, userDefPro);
                        if (oSR_AIR_CHARGE != null && oSR_AIR_CHARGE.STC_CD != null)
                        {
                            return Json(new { success = true, login = true, code = oSR_AIR_CHARGE.STC_RT.ToString(), Curr = oSR_AIR_CHARGE.STC_CURR.ToString(), data = oSR_AIR_CHARGE }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Please enter valid charge code.",type="Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
                        return Json(new { success = false, login = true ,msg="Invalid charg code.",type="Info"}, JsonRequestBehavior.AllowGet);
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
        public JsonResult loadChargCode2(string code)
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

                        SR_TRANS_CHA oSR_AIR_CHARGE = new SR_TRANS_CHA();
                        SR_SER_MISS MscChargeCode = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, "HTLRTS", code, userDefPro);
                        SR_SER_MISS MscChargeCode2 = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, "MSCELNS", code, userDefPro);
                        SR_SER_MISS MscChargeCode3 = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, "OVSLAGMT", code, userDefPro);

                        if (MscChargeCode != null && MscChargeCode.SSM_CD !=null)
                        {
                            oSR_AIR_CHARGE.STC_RT = MscChargeCode.SSM_RT;
                            oSR_AIR_CHARGE.STC_CURR = MscChargeCode.SSM_CUR;
                            oSR_AIR_CHARGE.STC_AD_RT = MscChargeCode.SSM_PERDAY_RTE;
                            oSR_AIR_CHARGE.STC_TAX_RT = MscChargeCode.SSM_TAX_RT;
                            return Json(new { success = true, login = true, code = oSR_AIR_CHARGE.STC_RT.ToString(), Curr = oSR_AIR_CHARGE.STC_CURR.ToString(), data = oSR_AIR_CHARGE }, JsonRequestBehavior.AllowGet);
                        }else
                            if (MscChargeCode2 != null && MscChargeCode2.SSM_CD !=null)
                        {
                            oSR_AIR_CHARGE.STC_RT = MscChargeCode2.SSM_RT;
                            oSR_AIR_CHARGE.STC_CURR = MscChargeCode2.SSM_CUR;
                            oSR_AIR_CHARGE.STC_AD_RT = MscChargeCode2.SSM_PERDAY_RTE;
                            oSR_AIR_CHARGE.STC_TAX_RT = MscChargeCode2.SSM_TAX_RT;
                            return Json(new { success = true, login = true, code = oSR_AIR_CHARGE.STC_RT.ToString(), Curr = oSR_AIR_CHARGE.STC_CURR.ToString(), data = oSR_AIR_CHARGE }, JsonRequestBehavior.AllowGet);
                        }else
                                if (MscChargeCode3 != null && MscChargeCode3.SSM_CD != null)
                        {
                            oSR_AIR_CHARGE.STC_RT = MscChargeCode3.SSM_RT;
                            oSR_AIR_CHARGE.STC_CURR = MscChargeCode3.SSM_CUR;
                            oSR_AIR_CHARGE.STC_AD_RT = MscChargeCode3.SSM_PERDAY_RTE;
                            oSR_AIR_CHARGE.STC_TAX_RT = MscChargeCode3.SSM_TAX_RT;
                            return Json(new { success = true, login = true, code = oSR_AIR_CHARGE.STC_RT.ToString(), Curr = oSR_AIR_CHARGE.STC_CURR.ToString(), data = oSR_AIR_CHARGE }, JsonRequestBehavior.AllowGet);
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
                            return Json(new { success = false, login = true, msg = Resource.txtInacCus, data = "",type="Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult updateCharges(string chargCode, string passengers, string discountPercentage, string unitAmount, string deleteitem, string currency, string tax, string items)
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
                                      
                                        SR_TRANS_CHA oSR_AIR_CHARGE = CHNLSVC.Tours.GetTransferChargeDetailsByCode(company, "TRANS", chargCode, userDefPro);
                                        if (oSR_AIR_CHARGE == null || oSR_AIR_CHARGE.STC_CD == null)
                                        {
                                          SR_SER_MISS MscChargeCode = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, "HTLRTS", chargCode, userDefPro);
                                          if (MscChargeCode != null && MscChargeCode.SSM_CD !=null)
                                            {
                                                oSR_AIR_CHARGE.STC_RT = MscChargeCode.SSM_RT;
                                                oSR_AIR_CHARGE.STC_CURR = MscChargeCode.SSM_CUR;
                                                oSR_AIR_CHARGE.STC_AD_RT = MscChargeCode.SSM_PERDAY_RTE;
                                                oSR_AIR_CHARGE.STC_TAX_RT = MscChargeCode.SSM_TAX_RT;
                                                oSR_AIR_CHARGE.STC_CD = MscChargeCode.SSM_CD;
                                            }
                                          else
                                          {
                                              SR_SER_MISS MscChargeCode2 = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, "MSCELNS", chargCode, userDefPro);
                                              if (MscChargeCode2 != null && MscChargeCode2.SSM_CD != null)
                                              {
                                                  oSR_AIR_CHARGE.STC_RT = MscChargeCode2.SSM_RT;
                                                  oSR_AIR_CHARGE.STC_CURR = MscChargeCode2.SSM_CUR;
                                                  oSR_AIR_CHARGE.STC_AD_RT = MscChargeCode2.SSM_PERDAY_RTE;
                                                  oSR_AIR_CHARGE.STC_TAX_RT = MscChargeCode2.SSM_TAX_RT;
                                                  oSR_AIR_CHARGE.STC_CD = MscChargeCode2.SSM_CD;
                                              }
                                              else
                                              {
                                                  SR_SER_MISS MscChargeCode3 = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, "OVSLAGMT", chargCode, userDefPro);
                                                  if (MscChargeCode3 != null && MscChargeCode3.SSM_CD != null)
                                                  {
                                                      oSR_AIR_CHARGE.STC_RT = MscChargeCode3.SSM_RT;
                                                      oSR_AIR_CHARGE.STC_CURR = MscChargeCode3.SSM_CUR;
                                                      oSR_AIR_CHARGE.STC_AD_RT = MscChargeCode3.SSM_PERDAY_RTE;
                                                      oSR_AIR_CHARGE.STC_TAX_RT = MscChargeCode3.SSM_TAX_RT;
                                                      oSR_AIR_CHARGE.STC_CD = MscChargeCode3.SSM_CD;
                                                  }
                                              }
                                          }
                                        }

                                       
                                       
                                      
                                        if (oSR_AIR_CHARGE != null && oSR_AIR_CHARGE.STC_CD != null)
                                        {


                                            if (Session["oMainInvoiceItems"] != null)
                                            {
                                                oMainInvoiceItems = (List<InvoiceItem>)Session["oMainInvoiceItems"];
                                            }
                                            else
                                            {
                                                oMainInvoiceItems = new List<InvoiceItem>();
                                            }
                                               // oMainInvoiceItems = (List<InvoiceItem>)Session["oMainInvoiceItems"];
                                            if (oMainInvoiceItems.Count > 0)
                                            {
                                                var ratetype = oMainInvoiceItems.FirstOrDefault().SII_CURR;
                                                if (ratetype != currency)
                                                {
                                                    return Json(new { success = false, login = true, msg = "Please Select Same Currency.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                           

                                            if (oMainInvoiceItems.FindAll(x => x.Sad_itm_cd == chargCode).Count > 0 && deleteitem != "Delete")
                                            {
                                                return Json(new { success = false, login = true, msg = "Selected charge code is already added.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }
                                            tax=tax.Trim();
                                            if (tax != "0") {
                                                bool taxNumber = decimal.TryParse(tax, out n);
                                                if (!taxNumber) {
                                                    return Json(new { success = false, login = true, msg = "Invalid tax percentage.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                            }

                                            decimal unitRate = Convert.ToDecimal(unitAmount);
                                            decimal disPercen = Convert.ToDecimal(discountPer);
                                            decimal passeng = Convert.ToDecimal(passengers);
                                            decimal itemss = Convert.ToDecimal(items);
                                            decimal totAmount = unitRate * passeng * itemss;
                                            decimal discountAmount = totAmount * disPercen / 100;
                                            decimal totBalanceAmount = totAmount - discountAmount;
                                            decimal taxAmount=0;
                                            if (tax != "0") {
                                                taxAmount=totBalanceAmount * Convert.ToDecimal(tax) / 100;
                                                totBalanceAmount = totBalanceAmount +totBalanceAmount * Convert.ToDecimal(tax) / 100;
                                            }
                                            InvoiceItem oItem = new InvoiceItem();
                                            oItem.Sad_itm_cd = chargCode;
                                            oItem.Sad_itm_stus = "GOD";
                                            oItem.Sad_qty = passeng;
                                            oItem.Sad_unit_rt = unitRate;
                                            oItem.Sad_tot_amt = totBalanceAmount;
                                            oItem.SII_CURR = oSR_AIR_CHARGE.STC_CURR;
                                            oItem.Sad_alt_itm_desc = oSR_AIR_CHARGE.STC_DESC;
                                            oItem.Sad_disc_rt = disPercen;
                                            oItem.Sad_disc_amt = discountAmount;
                                            oItem.Sad_itm_tax_amt = taxAmount;
                                            if (deleteitem != "Delete") oMainInvoiceItems.Add(oItem);
                                             Session["oMainInvoiceItems"] = oMainInvoiceItems;
                                          
                                            return Json(new { success = true, login = true, data = oItem }, JsonRequestBehavior.AllowGet);

                                        }
                                        else
                                        {
                                            return Json(new { success = false, login = true, msg = "Please enter valid charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
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
                    else {
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
        public JsonResult saveEnquiryData(GEN_CUST_ENQ enqData, string InvTyp, string drivername, string drivercontact)
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

                    DateTime now =Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy HH:mm"));
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
                  DataTable TD=  CHNLSVC.Tours.CHECK_DRIVER_ALLOC(enqData.GCE_FLEET, enqData.GCE_EXPECT_DT, enqData.GCE_RET_DT);
                  if (Convert.ToInt32( TD.Rows[0][0].ToString()) >0)
                    {
                        return Json(new { success = false, login = true, msg = "Already Vehicle Allocation ", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(enqData.GCE_CUS_CD))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter customer code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    else
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
                                return Json(new { success = false, login = true, msg = Resource.txtInacCus, data = "",type="Info" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = Resource.invalidCusCd, type = "Error" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(enqData.GCE_ENQ))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter remarks.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                     if (string.IsNullOrEmpty(enqData.GCE_FRM_TN))
                     {
                         return Json(new { success = false, login = true, msg = "Please enter picked up town.", type = "Info" }, JsonRequestBehavior.AllowGet);
                     }
                     else {
                         DataTable dt = new DataTable();

                         dt = CHNLSVC.General.Get_DetBy_town(enqData.GCE_FRM_TN);
                         if (dt != null)
                         {
                             if (dt.Rows.Count <=0)
                             {
                                 return Json(new { success = false, login = true, msg = "Invalid pick up town.", type = "Info" }, JsonRequestBehavior.AllowGet);
                             }
                         }
                         else
                         {
                             return Json(new { success = false, login = true, msg = "Invalid pick up town.", type = "Info" }, JsonRequestBehavior.AllowGet);
                         }
                     }
                     if (string.IsNullOrEmpty(enqData.GCE_TO_TN))
                     {
                         return Json(new { success = false, login = true, msg = "Please enter drop town.", type = "Info" }, JsonRequestBehavior.AllowGet);
                     }
                     else {
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
                    if (string.IsNullOrEmpty(enqData.GCE_ADD1))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter address 1.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    //else if (string.IsNullOrEmpty(enqData.GCE_ADD2))
                    //{
                    //    return Json(new { success = false, login = true, msg = "Please enter address 2.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    //}
                     if (string.IsNullOrEmpty(enqData.GCE_NO_PASS.ToString()))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter number of passengers.", type = "Info" }, JsonRequestBehavior.AllowGet);
                       
                    }
                     else if(!isdecimal(enqData.GCE_NO_PASS.ToString()))
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


                     string paidAmountString = Session["totalPaidAmount"] as string;
                     decimal totPaidAmount = Convert.ToDecimal(paidAmountString);
                     List<RecieptItemTBS> RecieptItemList = new List<RecieptItemTBS>();
                     List<RecieptItemTBS> sesRecieptItemList = Session["RecieptItemList"] as List<RecieptItemTBS>;
                     if (sesRecieptItemList == null)
                     {
                         sesRecieptItemList = new List<RecieptItemTBS>();
                     }
                    RecieptItemList = sesRecieptItemList;
                    decimal totalPayed = 0;
                    decimal totRcpVal = 0;
                     if (Session["oMainInvoiceItems"] != null)
                     {
                         oMainInvoiceItems = (List<InvoiceItem>)Session["oMainInvoiceItems"];
                     }
                     else
                     {
                         oMainInvoiceItems = new List<InvoiceItem>();
                     }
                     if (oMainInvoiceItems.Count>0)
                     {
                         totalPayed = oMainInvoiceItems.Sum(a => a.Sad_tot_amt);
                     }
                     //else {
                     //    return Json(new { success = false, login = true, msg = "Please add charges.", type = "Info" }, JsonRequestBehavior.AllowGet);
                     //}
                     if (RecieptItemList.Count>0)
                     {
                         totRcpVal = RecieptItemList.Sum(b => b.Sird_settle_amt);
                     }
                     //else {
                     //    return Json(new { success = false, login = true, msg ="Please add payment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                     //}
                     if (totalPayed == totRcpVal || oMainInvoiceItems.Count == 0 || totRcpVal == 0)
                     {
                         int result = 0;
                         bool isinvoicvesave = false;



                         #region saveEnq
                         MasterAutoNumber _ReqInsAuto = new MasterAutoNumber();
                         _ReqInsAuto.Aut_cate_cd = company;
                         _ReqInsAuto.Aut_cate_tp = "PC";
                         _ReqInsAuto.Aut_direction = null;
                         _ReqInsAuto.Aut_modify_dt = null;
                         _ReqInsAuto.Aut_moduleid = "AT";
                         _ReqInsAuto.Aut_number = 0;
                         _ReqInsAuto.Aut_start_char = "AT";
                         _ReqInsAuto.Aut_year = DateTime.Today.Year;

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
                         MasterBusinessEntity custProf = GetbyCustCD(enqData.GCE_CUS_CD.Trim());
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
                         oItem.GCE_FRM_ADD = (enqData.GCE_FRM_ADD!=null)?enqData.GCE_FRM_ADD:"";
                         oItem.GCE_TO_ADD = (enqData.GCE_TO_ADD!=null)?enqData.GCE_TO_ADD:"";
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
                         oItem.GCE_CONT_EMAIL = enqData.GCE_CONT_EMAIL;
                         oItem.GCE_GUESS = enqData.GCE_GUESS;
                         oItem.GCE_CONT_MOB = enqData.GCE_CONT_MOB;
                         oItem.GCE_REQ_NO_VEH = string.IsNullOrEmpty(enqData.GCE_REQ_NO_VEH.ToString()) ? 0 : Convert.ToInt32(enqData.GCE_REQ_NO_VEH);
                         oItem.GCE_ENQ_SUB_TP = enqData.GCE_ENQ_SUB_TP;
                         //  oGEN_CUST_ENQs.Add(oItem);
                         //  result = CHNLSVC.Tours.SaveEnquiryRequestList(oGEN_CUST_ENQs, _ReqInsAuto, oMainReq, out err);
                         #endregion
                         #region save
                         //MasterProfitCenter oPc = CHNLSVC.General.GetPCByPCCode(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());

                         _ReqInsAuto = new MasterAutoNumber();
                         _ReqInsAuto.Aut_cate_cd = company;
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

                         oMainInvoiceItems = (List<InvoiceItem>)Session["oMainInvoiceItems"];

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

                         oMainInvoiceItems = (List<InvoiceItem>)Session["oMainInvoiceItems"];
                         Int32 lno = 0;
                         if (oMainInvoiceItems != null)
                         {
                             foreach (InvoiceItem itm in oMainInvoiceItems)
                             {
                                 var obj = oMainInvoiceItems.FirstOrDefault(x => x.Sad_itm_cd == itm.Sad_itm_cd);
                                 if (obj != null) obj.Sad_itm_line = lno;
                                 lno++;
                             }
                         }
                         oHeadernew.Sah_com =oHeader.Sih_com;
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
                         if (_recieptItems.Count > 0) {
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
                         if (oMainInvoiceItems!=null )
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

                         string _invoiceNo="";
                         string _receiptNo="";
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
                        _genCustEnqser.GCS_LINE =0;
                        _genCustEnqser.GCS_FAC ="TRANS";
                        _genCustEnqser.GCS_SERVICE =enqData.GCE_ENQ_SUB_TP;
                        _genCustEnqser.GCS_UNITS =enqData.GCE_REQ_NO_VEH;
                        _genCustEnqser.GCS_SER_PROVIDER =enqData.GEN_CUST_ENQSER.GCS_SER_PROVIDER;
                        _genCustEnqser.GCS_SER_COM =company;
                        _genCustEnqser.GCS_SER_PC =userDefPro;
                         _genCustEnqser.GCS_PICK_FRM = (enqData.GCE_FRM_ADD!=null)?enqData.GCE_FRM_ADD:"";
                        _genCustEnqser.GCS_PICK_TN= enqData.GCE_FRM_TN;
                        _genCustEnqser.GCS_EXP_DT =enqData.GCE_EXPECT_DT;
                        _genCustEnqser.GCS_EXP_TIME = enqData.GCE_EXPECT_DT;
                         _genCustEnqser.GCS_DROP = (enqData.GCE_TO_ADD!=null)?enqData.GCE_TO_ADD:"";
                        _genCustEnqser.GCS_DROP_TN = enqData.GCE_TO_TN;
                        _genCustEnqser.GCS_DROP_DT =enqData.GCE_RET_DT;
                        _genCustEnqser.GCS_DROP_TIME = enqData.GCE_RET_DT; 
                        _genCustEnqser.GCS_VEH_TP =enqData.GCE_VEH_TP;
                        _genCustEnqser.GCS_COMMENT =enqData.GCE_ENQ;
                       // _genCustEnqser.GCS_CUS_TYPE = enqData.GCE_CUS_TYPE;
                        _genCustEnqser.GCS_STATUS =1;
                        _genCustEnqser.GCS_CRE_BY =userId;
                        _genCustEnqser.GCS_CRE_DT =DateTime.Now;
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
                         
                        if (_recieptItemsNew.Count == 0) {
                            _ReceiptHeaderNew = null;
                        }
                        //save busentity

                        MasterBusinessEntity cus = new MasterBusinessEntity();
                        cus.Mbe_act = true;
                        cus.Mbe_cd = oItem.GCE_CONT_CD;
                        cus.Mbe_com = company;
                        cus.Mbe_contact = oItem.GCE_CONT_MOB;
                        cus.Mbe_cre_by = userId;
                        cus.Mbe_cre_dt = DateTime.Now.Date;
                        cus.Mbe_email = oItem.GCE_CONT_EMAIL;
                        cus.MBE_FNAME = oItem.GCE_GUESS;
                        cus.Mbe_mob = oItem.GCE_CONT_MOB;
                        cus.Mbe_mod_by = userId;
                        cus.Mbe_mod_dt = DateTime.Now.Date;
                        cus.Mbe_name = oItem.GCE_GUESS;
                        cus.Mbe_nic = Request["Mbe_nic"].ToString();
                        cus.Mbe_sub_tp = "D";
                        cus.Mbe_tp = "C";

                        GroupBussinessEntity cusg = new GroupBussinessEntity();
                        cusg.Mbg_name = oItem.GCE_GUESS;
                        cusg.Mbg_cd = oItem.GCE_CONT_CD;
                        cusg.Mbg_act = true;
                        cusg.Mbg_contact = oItem.GCE_CONT_MOB;
                        cusg.Mbg_email = oItem.GCE_CONT_EMAIL;
                        cusg.Mbg_nationality = "SL";

                        List<ST_ENQ_CHARGES> enqChrgItems = new List<ST_ENQ_CHARGES>();

                        if (oMainInvoiceItems !=null)
                         {
                             foreach (var enqchg in oMainInvoiceItems)
                             {
                                 ST_ENQ_CHARGES ob = new ST_ENQ_CHARGES();
                                 ob.SCH_ALT_ITM_DESC = enqchg.Sad_alt_itm_desc;
                                 ob.SCH_CURR = enqchg.SII_CURR;
                                 ob.SCH_DISC_AMT = enqchg.Sad_disc_amt;
                                 ob.SCH_DISC_RT = enqchg.Sad_disc_rt;
                                 ob.SCH_ENQ_NO = _genCustEnqser.GCS_ENQ_ID;
                                 ob.SCH_EX_RT = enqchg.SII_EX_RT;
                                 ob.SCH_INVOICED = 0;
                                 ob.SCH_INVOICED_NO = "";
                                 ob.SCH_ITM_CD = enqchg.Sad_itm_cd;
                                 ob.SCH_ITM_SERVICE = "T";
                                 ob.SCH_ITM_STUS = "GOD";
                                 ob.SCH_ITM_TAX_AMT = enqchg.Sad_itm_tax_amt;
                                 ob.SCH_ITM_TP = "";
                                 ob.SCH_QTY = enqchg.Sad_qty;
                                 ob.SCH_SEQ_NO = _genCustEnqser.GCS_SEQ;
                                 ob.SCH_TOT_AMT = enqchg.Sad_tot_amt;
                                 ob.SCH_UNIT_AMT = enqchg.Sad_unit_amt;
                                 ob.SCH_UNIT_RT = enqchg.Sad_unit_rt;
                                 enqChrgItems.Add(ob);
                             }
                         }
                        

                         result = CHNLSVC.Tours.SaveToursrInvoice(null, null, null, null, null, null, null, null, _invoiceAuto, _receiptAuto, _inventoryAuto, false, out _invoiceNo, out _receiptNo, out _deliveryOrder, oCust, false, false, out _error, null, null, null, null, out _buybackadj, false, _genCustEnqser, oItem, _ReqInsAuto, null, true, cus, cusg, enqChrgItems);
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
                                  "\nVehicle # : " + enqData.GCE_FLEET+
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
                                 return Json(new { success = true, login = true, msg = _error}, JsonRequestBehavior.AllowGet);
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
                         else {
                             return Json(new { success = false, login = true, msg = "Error Occurred." + _error, type = "Error" }, JsonRequestBehavior.AllowGet);
                         }
                         #endregion
                     }
                     else {
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
        public JsonResult getEnquiryData(string enqId) {
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
                       
                        GEN_CUST_ENQ oItem = CHNLSVC.Tours.getEnquiryDetails(company, userDefPro, enqId);
                        List<RecieptItemTBS> recItems = new List<RecieptItemTBS>();
                        if (oItem != null)
                        {
                            if (oItem.CHARGER_VALUE != null) 
                            {
                                if (oItem.CHARGER_VALUE.Count > 0) {
                                    recItems = CHNLSVC.Tours.getReceiptItemByinvNo(oItem.CHARGER_VALUE[0].Sad_inv_no, company, userDefPro);
                                }
                                else
                                {
                                    oItem.CHARGER_VALUE = new List<InvoiceItem>();
                                    List<ST_ENQ_CHARGES> chgenq = CHNLSVC.Tours.tempEnquiryCharges(enqId);
                                    if (chgenq != null)
                                    {
                                        foreach (var inv in chgenq)
                                        {
                                            InvoiceItem ob = new InvoiceItem();
                                            ob.Sad_itm_cd = inv.SCH_ITM_CD;
                                            ob.Sad_unit_rt = inv.SCH_UNIT_RT;
                                            ob.Sad_disc_rt = inv.SCH_DISC_RT;
                                            ob.Sad_itm_tax_amt = inv.SCH_ITM_TAX_AMT;
                                            ob.Sad_tot_amt = inv.SCH_TOT_AMT;
                                            ob.Sad_qty = inv.SCH_QTY;
                                            ob.SII_CURR = inv.SCH_CURR;
                                            oItem.CHARGER_VALUE.Add(ob);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                oItem.CHARGER_VALUE = new List<InvoiceItem>();
                                List<ST_ENQ_CHARGES> chgenq = CHNLSVC.Tours.tempEnquiryCharges(enqId);
                                if (chgenq !=null)
                                {
                                    foreach(var inv in chgenq)
                                    {
                                        InvoiceItem ob = new InvoiceItem();
                                        ob.Sad_itm_cd = inv.SCH_ITM_CD;
                                        ob.Sad_unit_rt = inv.SCH_UNIT_RT;
                                        ob.Sad_disc_rt = inv.SCH_DISC_RT;
                                        ob.Sad_itm_tax_amt = inv.SCH_ITM_TAX_AMT;
                                        ob.Sad_tot_amt = inv.SCH_TOT_AMT;
                                        ob.Sad_qty = inv.SCH_QTY;
                                        ob.SII_CURR = inv.SCH_CURR;
                                        oItem.CHARGER_VALUE.Add(ob);
                                    }
                                }

                            }
                            
                            Session["oMainInvoiceItems"] = oItem.CHARGER_VALUE;
                            return Json(new { success = true, login = true, data = oItem, recItems = recItems }, JsonRequestBehavior.AllowGet);
                        }
                        else {
                            return Json(new { success = false, login = true ,msg="Invalid enquiry id.",type="Info"}, JsonRequestBehavior.AllowGet);
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

        //print
        public ActionResult TransportPrint(string enqno)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                enqno = enqno.Trim();
                ReportDocument rd = new ReportDocument();

                GEN_CUST_ENQ Enqdata = CHNLSVC.Tours.getEnquiryDetailsTours(company, userDefPro, enqno);
                List<TransportEnq> trenq = new List<TransportEnq>();

                foreach (var chglist in Enqdata.ENQ_CHARGES)
                {
                    TransportEnq enqdatareal = new TransportEnq();
                    enqdatareal.GCE_BILL_CUSNAME = Enqdata.Gce_bill_cusname;
                    enqdatareal.GCE_DEPOSIT_CHG = Enqdata.GCE_DEPOSIT_CHG.ToString();
                    enqdatareal.GCE_ENQ = Enqdata.GCE_ENQ;
                    enqdatareal.GCE_ENQ_ID = Enqdata.GCE_ENQ_ID;
                    enqdatareal.GCE_FLY_DATE = Enqdata.GCE_FLY_DATE;
                    enqdatareal.GCE_FLY_NO = Enqdata.GCE_FLY_NO;
                    enqdatareal.GCE_FRM_TN = Enqdata.GCE_FRM_TN;
                     enqdatareal.GCE_TO_TN = Enqdata.GCE_TO_TN;
                     if (enqdatareal.GCE_FRM_TN=="AIRPT")
                    {
                        enqdatareal.TripType = "Pick Up";
                    }
                     else if (enqdatareal.GCE_TO_TN=="AIRPT")
                    {
                         enqdatareal.TripType = "Drop";
                    }
                     else
                     {
                         enqdatareal.TripType = "City tour";
                     }

                    enqdatareal.GCE_REF = Enqdata.GCE_REF;
                   
                    enqdatareal.Sad_alt_itm_desc = chglist.SCH_ALT_ITM_DESC;
                    enqdatareal.Sad_itm_cd = chglist.SCH_ITM_CD;
                    enqdatareal.Sad_tot_amt = chglist.SCH_TOT_AMT;
                    enqdatareal.gce_expect_dt = Enqdata.GCE_EXPECT_DT;
                    enqdatareal.gce_ret_dt = Enqdata.GCE_RET_DT;
                    enqdatareal.gce_veh_tp = Enqdata.GCE_VEH_TP;
                    enqdatareal.gce_driver = Enqdata.GCE_DRIVER;
                    enqdatareal.gce_fleet = Enqdata.GCE_FLEET;
                    enqdatareal.UserLocation = userDefLoc;
                    enqdatareal.GCE_NAME = Enqdata.GCE_NAME;
                    MST_CHKINOUT chkOutData = CHNLSVC.Tours.getEnqChkData(Enqdata.GCE_ENQ_ID);
                    enqdatareal.END_MILEAGE = chkOutData.CHK_IN_KM;
                    enqdatareal.START_MILEAGE = chkOutData.CHK_OUT_KM;

                    trenq.Add(enqdatareal);
                }

                    rd.Load(Server.MapPath("/Reports/" + "TransportSlip.rpt"));

                    rd.Database.Tables["EnqHdr"].SetDataSource(trenq);
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                try
                {
                    this.Response.Clear();
                    this.Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
                    return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
            {

                return Redirect("~/Login");

            }
        }
        private bool isdecimal(string txt)
        {
            decimal val;
            return decimal.TryParse(txt, out val);
        }
        private bool isInteger(string txt)
        {
            int val ;
            return int.TryParse(txt, out val);
        }
        public JsonResult removeChargeItem(string chgCd, string pax, string totcst)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                if (Session["oMainInvoiceItems"] != null)
                {
                    oMainInvoiceItems = (List<InvoiceItem>)Session["oMainInvoiceItems"];
                }
                else
                {
                    oMainInvoiceItems = new List<InvoiceItem>();

                }
                var itemToRemove = oMainInvoiceItems.First(r => r.Sad_itm_cd == chgCd);
                oMainInvoiceItems.Remove(itemToRemove);
                Session["oMainInvoiceItems"] = oMainInvoiceItems;
                decimal totAmt= oMainInvoiceItems.Sum(m => m.Sad_tot_amt);
                return Json(new { success = true, login = true, data = oMainInvoiceItems, totAmt = totAmt }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = true, data = Resource.ServerError }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult getVehicleTypes()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<ST_VEHICLE_TP> types = CHNLSVC.Tours.getVehicleTypes();
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
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult senEnquirySMS(string enqNo)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(enqNo))
                {
                    Int32 result = 0;
                    enqNo = enqNo.Trim();
                    List<MST_TEMP_MESSAGES> message = CHNLSVC.Tours.getTempSmsMessage(company, userDefPro, "GSTENQMSG");
                    List<MST_TEMP_MESSAGES> msgdriver = CHNLSVC.Tours.getTempSmsMessage(company, userDefPro, "GSTDRIMSG");
                    GEN_CUST_ENQ oItem = CHNLSVC.Tours.getEnquiryDetails(company, userDefPro, enqNo);
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
                            msg = msg.Replace("@enqId", enqNo);
                            msg = msg.Replace("@pictime", oItem.GCE_EXPECT_DT.ToString());
                            msg = msg.Replace("@picloc", oItem.GCE_FRM_TN.ToString());
                            
                            if (employees==null)
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
                            if (msgdriver.Count > 0)
                            {
                                msgdriv = msgdriver[0].MMT_TEXT;
                                msgdriv = msgdriv.Replace("@DriName", employees.MEMP_FIRST_NAME.ToString());
                                msgdriv = msgdriv.Replace("@enqId", enqNo);
                                msgdriv = msgdriv.Replace("@pictime", oItem.GCE_EXPECT_DT.ToString());
                                msgdriv = msgdriv.Replace("@picloc", oItem.GCE_FRM_TN.ToString());
                                msgdriv = msgdriv.Replace("@droploc", oItem.GCE_TO_TN.ToString());
                                msgdriv = msgdriv.Replace("@gstCon", oItem.GCE_CONT_MOB.ToString());
                                msgdriv = msgdriv.Replace("@gstName", oItem.GCE_CONT_PER.ToString());

                            }
                            else {
                                msgdriv = "Dear " + employees.MEMP_FIRST_NAME.ToString() + ",\nPlease find your tour details.\nEnquiry -" + enqNo + " \nPick Up Timr - " + oItem.GCE_EXPECT_DT.ToString() + " \nPick Up Location :" + oItem.GCE_FRM_TN.ToString() + " \nDrop Location :" + oItem.GCE_TO_TN.ToString() + " \nGuest Contact:" + oItem.GCE_CONT_MOB.ToString() + " \nGuest Mob :" + oItem.GCE_CONT_PER.ToString() + ".";
                            }
                            
                        }
                        else
                        {
                            msg = "Dear Customer,\nPlease find your tour details.\nEnquiry -  " + enqNo + " \nPick Up Timr - " + oItem.GCE_EXPECT_DT.ToString() + " \nPick Up Location :" + oItem.GCE_FRM_TN.ToString() + " \nDriver Name :" + employees.MEMP_FIRST_NAME.ToString() + " \nDriver Contact:" + employees.MEMP_MOBI_NO.ToString() + " \nVehicle Number :" + oItem.GCE_FLEET.ToString() + ".";
                            msgdriv = "Dear " + employees.MEMP_FIRST_NAME.ToString() + ",\nPlease find your tour details.\nEnquiry -" + enqNo + " \nPick Up Timr - " + oItem.GCE_EXPECT_DT.ToString() + " \nPick Up Location :" + oItem.GCE_FRM_TN.ToString() + " \nDrop Location :" + oItem.GCE_TO_TN.ToString() + " \nGuest Contact:" + oItem.GCE_CONT_MOB.ToString() + " \nGuest Mob :" + oItem.GCE_CONT_PER.ToString() + ".";
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
                            _out.Refdocno = enqNo;
                            _out.Sender = "Abans Tours";
                            _out.Createtime = DateTime.Now;
                            result = CHNLSVC.Tours.SendSMS(_out, out err);
                            if (err != string.Empty)
                            {
                                return Json(new { success = true, login = true, msg = err, type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            String mobidri = "+94" + employees.MEMP_MOBI_NO.Substring(1, 9);
                            _outDriver.Msgstatus = 0;
                            _outDriver.Msgtype = "ENQ";
                            _outDriver.Receivedtime = DateTime.Now;
                            _outDriver.Receiver = mobidri;
                            _outDriver.Msg = msgdriv;
                            _outDriver.Receiverphno = mobidri;
                            _outDriver.Senderphno = mobidri;
                            _outDriver.Msgid = oItem.GCE_SEQ.ToString();
                            _outDriver.Refdocno = enqNo;
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
        public JsonResult checkFacLocAvailability()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_FAC_LOC> facList = CHNLSVC.Tours.getFacLocations(company, userDefPro);
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
                            else
                            {
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
   
    }
}