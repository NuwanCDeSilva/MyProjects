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
    public class LogSheetController : BaseController
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
                ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1017);
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
        private List<TR_LOGSHEET_DET> oMainItems = new List<TR_LOGSHEET_DET>();
        // GET: LogSheet
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
        public JsonResult chargCodeDetail(string chgCd, string chgTyp)
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
                    if (chgCd != "")
                    {
                        if (chgTyp != "")
                        {
                            if (chgTyp == "M")
                            {
                                SR_SER_MISS oSR_SER_MISS = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, "MSCELNS", chgCd, userDefPro);
                                if (oSR_SER_MISS != null && oSR_SER_MISS.SSM_CD != null)
                                {
                                    return Json(new { success = true, login = true, data = oSR_SER_MISS }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Please enter valid charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else if (chgTyp == "T")
                            {
                                SR_TRANS_CHA oSR_Trans_CHARGE = CHNLSVC.Tours.GetTransferChargeDetailsByCode(company, "TRANS", chgCd,userDefPro);
                                if (oSR_Trans_CHARGE != null && oSR_Trans_CHARGE.STC_CD != null)
                                {
                                    return Json(new { success = true, login = true, data = oSR_Trans_CHARGE }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Please enter valid charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else {
                                return Json(new { success = false, login = true, msg = "Invalid charg code type selected.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else {
                            return Json(new { success = false, login = true, msg = "Invalid charg code type selected.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult updateChargeCodes(string chgCd,string description,string rteType ,string qty,string unitrate,string tax,string disRate,string disAmt,string isDri,string isCus) { 
        string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    chgCd = chgCd.Trim();
                    qty = qty.Trim();
                    unitrate = unitrate.Trim();
                    tax = tax.Trim();
                    disRate = disRate.Trim();
                    disAmt = disAmt.Trim();
                    description = description.Trim();
                    rteType = rteType.Trim();
                    if (Session["oLogMainItems"] != null)
                    {
                        oMainItems = (List<TR_LOGSHEET_DET>)Session["oLogMainItems"];

                    }
                    else
                    {
                        oMainItems = new List<TR_LOGSHEET_DET>();
                    }

                    if (String.IsNullOrEmpty(chgCd))
                    {
                        return Json(new { success = false, login = true, msg = "Please select a charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    SR_SER_MISS oSR_SER_MISS = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, "MSCELNS", chgCd,userDefPro);
                    if (oSR_SER_MISS == null && oSR_SER_MISS.SSM_CD == null)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(description))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter a description.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(qty))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter quantity.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(rteType))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter a rate type.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(unitrate))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter a unit rate.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (isCus == "false" && isDri == "false")
                    {
                        return Json(new { success = false, login = true, msg = "Please select is customer or is driver.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    if (oMainItems.FindAll(x => x.TLD_CHR_CD == chgCd).Count > 0)
                    {
                        return Json(new { success = false, login = true, msg = "This charge code is already added.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    TR_LOGSHEET_DET oItems = new TR_LOGSHEET_DET();
                    oItems.TLD_SEQ = 0;
                    oItems.TLD_LINE = oMainItems.Count + 1;
                    oItems.TLD_CHR_CD = chgCd;
                    oItems.TLD_CHR_DESC = description;
                    oItems.TLD_QTY = Convert.ToDecimal(qty);
                    oItems.TLD_RT_TP = rteType;
                    oItems.TLD_U_RT = Convert.ToDecimal(unitrate);
                    oItems.TLD_U_AMT = Convert.ToDecimal(oItems.TLD_QTY * oItems.TLD_U_RT);
                    oItems.TLD_TAX = (tax != "") ? Convert.ToDecimal(tax) : 0;
                    oItems.TLD_DIS_RT = (disRate != "") ? Convert.ToDecimal(disRate) : 0;
                    oItems.TLD_DIS_AMT = (disAmt != "") ? Convert.ToDecimal(disAmt) : 0;

                    oItems.TLD_TAX = (oItems.TLD_U_AMT - oItems.TLD_DIS_AMT) * oItems.TLD_TAX / 100;

                    oItems.TLD_TOT = oItems.TLD_U_AMT + oItems.TLD_TAX - oItems.TLD_DIS_AMT;
                    oItems.TLD_IS_CUS = (isCus == "true") ? 1 : 0;
                    oItems.TLD_IS_DRI = (isDri =="true") ? 1 : 0;
                    oItems.TLD_IS_ADD = 1;
                    oMainItems.Add(oItems);
                    Session["oLogMainItems"] = oMainItems;
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
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
                    Session["oLogMainItems"] = null;
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult removeChargeItems(string chargCode)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    chargCode = chargCode.Trim();
                    if (chargCode != "")
                    {
                        if (Session["oLogMainItems"] != null)
                        {
                            oMainItems = (List<TR_LOGSHEET_DET>)Session["oLogMainItems"];

                        }
                        else
                        {
                            oMainItems = new List<TR_LOGSHEET_DET>();
                        }
                        if (oMainItems.FindAll(x => x.TLD_CHR_CD == chargCode).Count > 0)
                        {
                            try
                            {
                                var itemToRemove = oMainItems.First(x => x.TLD_CHR_CD == chargCode);
                                oMainItems.Remove(itemToRemove);
                            }
                            catch (Exception e)
                            {
                                return Json(new { success = false, login = true, msg = e.ToString(), type = "Info" }, JsonRequestBehavior.AllowGet);
                            }

                            Session["oLogMainItems"] = oMainItems;
                            return Json(new { success = true, login = true, oMainItems = oMainItems }, JsonRequestBehavior.AllowGet);
                        }
                        else {
                            return Json(new { success = false, login = true, msg = "Invalid charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
                        return Json(new { success = false, login = true, msg = "Invalid charge code.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getLogDetails(string logNum,string selCompany,string profCen)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    logNum = logNum.Trim();
                    selCompany = selCompany.Trim();
                    profCen = profCen.Trim();
                    if (logNum != "")
                    {
                        if (string.IsNullOrEmpty(selCompany))
                        {
                            return Json(new { success = false, login = true, msg = "Please select a company code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (string.IsNullOrEmpty(profCen))
                        {
                            return Json(new { success = false, login = true, msg = "please select a profit center.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        TR_LOGSHEET_HDR oheader = CHNLSVC.Tours.GetLogSheetHeader(selCompany, profCen, logNum);
                        if (oheader != null && oheader.TLH_LOG_NO != null)
                        {
                            List<TR_LOGSHEET_DET> oItems = CHNLSVC.Tours.GetLogSheetDetails(oheader.TLH_SEQ);
                            oMainItems = oItems;
                            Session["oLogMainItems"] = oMainItems;
                            return Json(new { success = true, login = true, oheader = oheader, oMainItems = oMainItems }, JsonRequestBehavior.AllowGet);
                        }
                        else {
                            return Json(new { success = false, login = true, msg = "Invalid log sheet number." ,type="Info"}, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
                        return Json(new { success = false, login = true, msg = "Invalid log sheet number.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult saveLogSheet(TR_LOGSHEET_HDR oheader)
        { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (string.IsNullOrEmpty(oheader.TLH_COM))
                    {
                        return Json(new { success = false, login = true, msg = "Please select a company.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(oheader.TLH_PC))
                    {
                        return Json(new { success = false, login = true, msg = "Please select a profit center.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    if (string.IsNullOrEmpty(oheader.TLH_DT.ToString()))
                    {
                        return Json(new { success = false, login = true, msg = "Please select a date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(oheader.TLH_CUS_CD))
                    {
                        return Json(new { success = false, login = true, msg = "Please select a customer.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(oheader.TLH_ST_DT.ToString()))
                    {
                        return Json(new { success = false, login = true, msg = "Please select a start date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(oheader.TLH_ED_DT.ToString()))
                    {
                        return Json(new { success = false, login = true, msg = "Please select a finish date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(oheader.TLH_DRI_CD))
                    {
                        return Json(new { success = false, login = true, msg = "Please select a driver.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(oheader.TLH_FLEET))
                    {
                        return Json(new { success = false, login = true, msg = "Please select a vehicle.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(oheader.TLH_RMK))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter a remark.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(oheader.TLH_INV_MIL.ToString()))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter invoiced mileage.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(oheader.TLH_DRI_MIL.ToString()))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter driver mileage.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(oheader.TLH_MET_IN.ToString()))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter meter in value.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(oheader.TLH_MET_OUT.ToString()))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter meter out value.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (!isNum(oheader.TLH_INV_MIL.ToString()))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid invoice milage.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (!isNum(oheader.TLH_DRI_MIL.ToString()))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid driver milage.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (!isNum(oheader.TLH_MET_IN.ToString()))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid Meter In.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (!isNum(oheader.TLH_MET_OUT.ToString()))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid Meter Out.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToDecimal(oheader.TLH_MET_IN) > Convert.ToDecimal(oheader.TLH_MET_OUT))
                    {
                        return Json(new { success = false, login = true, msg = "Meter in value cannot be less than meter out valve.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToDecimal(oheader.TLH_INV_MIL) == 0)
                    {
                        return Json(new { success = false, login = true, msg = "Invoice mile age cannot be '0'.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToDecimal(oheader.TLH_DRI_MIL) == 0)
                    {
                        return Json(new { success = false, login = true, msg = "Driver mile age cannot be '0'.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToDateTime(oheader.TLH_ST_DT) > Convert.ToDateTime(oheader.TLH_ED_DT))
                    {
                        return Json(new { success = false, login = true, msg = "Please select a valid date ranges.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    List<TR_LOGSHEET_DET> oMainItems = new List<TR_LOGSHEET_DET>();
                    if (Session["oLogMainItems"] != null)
                    {
                        oMainItems = (List<TR_LOGSHEET_DET>)Session["oLogMainItems"];
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "please add items to save.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    bool isNew = true;
                    String err = string.Empty;
                    String logNumber = string.Empty;

                    TR_LOGSHEET_HDR oHeader = new TR_LOGSHEET_HDR();
                    oHeader.TLH_SEQ = (oheader.TLH_SEQ != null) ? oheader.TLH_SEQ : 0;
                    oHeader.TLH_COM = oheader.TLH_COM.Trim();
                    oHeader.TLH_PC = oheader.TLH_PC.Trim();
                    oHeader.TLH_LOG_NO = (oheader.TLH_LOG_NO != null) ? oheader.TLH_LOG_NO.Trim() : "";
                    oHeader.TLH_DT = Convert.ToDateTime(oheader.TLH_DT);
                    oHeader.TLH_REQ_NO = (oheader.TLH_REQ_NO != null) ? oheader.TLH_REQ_NO.Trim() : "";

                    oHeader.TLH_ST_DT = Convert.ToDateTime(oheader.TLH_ST_DT);

                    oHeader.TLH_ED_DT = Convert.ToDateTime(oheader.TLH_ED_DT);

                    oHeader.TLH_CUS_CD = oheader.TLH_CUS_CD.Trim();
                    oHeader.TLH_DRI_CD = oheader.TLH_DRI_CD.Trim();
                    oHeader.TLH_GUST = (oheader.TLH_GUST != null) ? oheader.TLH_GUST.Trim() : "";
                    oHeader.TLH_FLEET = oheader.TLH_FLEET.Trim();
                    oHeader.TLH_RMK = oheader.TLH_RMK.Trim();
                    oHeader.TLH_INV_MIL = Convert.ToDecimal(oheader.TLH_INV_MIL);
                    oHeader.TLH_DRI_MIL = Convert.ToDecimal(oheader.TLH_DRI_MIL);
                    oHeader.TLH_MET_IN = Convert.ToDecimal(oheader.TLH_MET_IN);
                    oHeader.TLH_MET_OUT = Convert.ToDecimal(oheader.TLH_MET_OUT);
                    oHeader.TLH_CRE_BY = userId;
                    oHeader.TLH_CRE_DT = DateTime.Now;
                    oHeader.TLH_MOD_BY = userId;
                    oHeader.TLH_MOD_DT = DateTime.Now;
                    oHeader.TLH_ANAL1 = String.Empty;
                    oHeader.TLH_ANAL2 = String.Empty;
                    oHeader.TLH_ANAL3 = String.Empty;
                    oHeader.TLH_ANAL4 = 0;
                    oHeader.TLH_ANAL5 = 0;
                    oHeader.TLH_PAY_DRI = 0;
                    oHeader.TLH_INV = 0;

                    MasterAutoNumber _Auto = new MasterAutoNumber();
                    _Auto.Aut_cate_cd = userDefPro;
                    _Auto.Aut_cate_tp = "LOGSHT";
                    _Auto.Aut_direction = 1;
                    _Auto.Aut_modify_dt = null;
                    _Auto.Aut_moduleid = "LOGSHT";
                    _Auto.Aut_number = 0;
                    _Auto.Aut_start_char = "LOGSHT";
                    _Auto.Aut_year = DateTime.Today.Year;

                    Int32 result = CHNLSVC.Tours.saveLogSheet(oHeader, oMainItems, isNew, _Auto, out err, out logNumber);

                    if (result > 0)
                    {
                        return Json(new { success = true, login = true, msg ="Successfully saved. Log sheet number : " + logNumber, type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Process Terminated. Log sheet number : " + logNumber, type = "Error" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = false, msg = Resource.ServerError, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public bool isNum(string num) {
            decimal d;
            if (decimal.TryParse(num, out d))
            {
                return true;
            }else{
                return false;
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

                        GEN_CUST_ENQ oItem = CHNLSVC.Tours.getEnquiryDetails(company, userDefPro, enqId);
                        List<RecieptItemTBS> recItems = new List<RecieptItemTBS>();
                        if (oItem != null)
                        {
                            if (oItem.CHARGER_VALUE != null)
                            {
                                if (oItem.CHARGER_VALUE.Count > 0)
                                {
                                    recItems = CHNLSVC.Tours.getReceiptItemByinvNo(oItem.CHARGER_VALUE[0].Sad_inv_no, company, userDefPro);
                                }
                            }

                            Session["oMainInvoiceItems"] = oItem.CHARGER_VALUE;
                            return Json(new { success = true, login = true, data = oItem, recItems = recItems }, JsonRequestBehavior.AllowGet);
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
    }
}