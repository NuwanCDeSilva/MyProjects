using CrystalDecisions.CrystalReports.Engine;
using FF.BusinessObjects;
using FF.BusinessObjects.Genaral;
using FF.BusinessObjects.Sales;
using FF.BusinessObjects.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace FastForward.Logistic.Controllers
{
    public class PettyCashController : BaseController
    {
        // GET: PettyCash
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string; 
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                SEC_SYSTEM_MENU per = CHNLSVC.Security.getUserPermission(userId, company, 8);
                if (per.SSM_ID != 0)
                {
                    TRN_MOD_MAX_APPLVL data = CHNLSVC.General.getMaxAppLvlPermission("PTYCSHREQ", company);
                    if (data.TMAL_MODULE != null)
                    {
                        Session["MAXAPPLVL"] = data;
                    }
                    else
                    {
                        data.TMAL_COM = company;
                        data.TMAL_MAX_APPLVL = 3;
                        data.TMAL_MODULE = "PTYCSHREQ";
                        Session["MAXAPPLVL"] = data;
                    }
                    return View();
                }
                else
                {
                    return Redirect("/Home/Error");
                }
            }
            else
            {
                return Redirect("~/Login");
            }
        }
        [HttpGet]
        public JsonResult loadRetuestTypes()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string error = "";
                    List<MST_REQ_TYPE> request = CHNLSVC.Sales.getReqyestTypes("PETTYCSH", out error);
                    if (request.Count > 0)
                    {
                        return Json(new { success = true, login = true, data = request }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, data = "", type = "Info", msg = "Please setup reuest types." }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult loadRequestDetails(string type, string reqno)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string error = "";
                    TRN_PETTYCASH_REQ_HDR request = CHNLSVC.Sales.getReqyestDetials(type, reqno, company, userDefPro, out error);
                    if (error == "")
                    {
                        Int32 appPer = Convert.ToInt32(Session["Log_Autho"].ToString());
                        TRN_MOD_MAX_APPLVL lvl = (TRN_MOD_MAX_APPLVL)Session["MAXAPPLVL"];
                        bool perm = CHNLSVC.Security.Is_OptionPerimitted(company, userId, 1004);
                        string pemissionMsg="";
                        bool editable = true;
                        if (request.TPRH_APP_3 == 1 || perm != true || appPer < lvl.TMAL_MAX_APPLVL)
                        {
                            if (appPer < lvl.TMAL_MAX_APPLVL)
                            {
                                editable = false;
                                pemissionMsg = "Level " + appPer + " users don't have permission for edit job details.";

                            }
                            else
                            {
                                if (request.TPRH_APP_3 == 1)
                                {
                                    editable = false;
                                    pemissionMsg = "Alredy approved by level 3 or Maximum approved level user.Cannot update job details";
                                }
                                else if (perm != true)
                                {
                                    editable = false;
                                    pemissionMsg = "You don't have permission to update pettycash request job details.(Requsted permission code 1004)";
                                }
                            }

                        }

                        if (request.TPRH_STUS == "C")
                        {
                            return Json(new { success = false, login = true, msg = "Request '" + reqno + "' already canceled by " + request.TPRH_MOD_BY, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (request.TPRH_STUS == "R")
                        {
                            return Json(new { success = false, login = true, msg = "Request '" + reqno + "' already rejected by " + request.TPRH_MOD_BY, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (request.TPRH_REQ_NO != null)
                        {
                            List<TRN_PETTYCASH_REQ_DTL> reqDet = CHNLSVC.Sales.getReqyestItemDetials(request.TPRH_SEQ, out error);
                            if (error == "")
                            {
                                Session["Item"] = reqDet;
                                Session["Hdr"] = request;
                                decimal tot = reqDet.Sum(x => x.TPRD_ELEMENT_AMT);
                                return Json(new { success = true, login = true, data = request, item = reqDet, total = tot, updateJob = editable, pemissionMsg = pemissionMsg }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid request number", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult loadCompanyDta()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string error = "";
                    MST_COM request = CHNLSVC.Sales.getCompanyDetails(company, out error);
                    if (request.MC_CD != null)
                    {
                        return Json(new { success = true, login = true, data = request }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (error != "")
                        {
                            return Json(new { success = false, login = true, data = "", type = "Info", msg = error }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, data = "", type = "Info", msg = "Invalid company code." }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult updateAmount(string units, string uprice, string cur, string exrng)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (units == "" || uprice == "" || exrng == "")
                    {
                        return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                    }
                    ValidationController validate = new ValidationController();
                    if (units != "" && uprice != "" && exrng != "")
                    {
                        //bool valid = validate.validateNumberInt(units);
                        //if (valid == false)
                        //{
                        //    return Json(new { success = false, login = true, msg = "Please enter valid unit.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        //}
                        bool valid = validate.validateNumberDecimal(units);//changed by dilshan on 07/06/2018
                        if (valid == false)
                        {
                            return Json(new { success = false, login = true, msg = "Please enter valid unit.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        valid = validate.validateNumberDecimal(uprice);
                        if (valid == false)
                        {
                            return Json(new { success = false, login = true, msg = "Please enter valid unit price.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }

                        valid = validate.validateNumberDecimal(exrng);
                        if (valid == false)
                        {
                            return Json(new { success = false, login = true, msg = "Please enter valid exchange rate.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (cur == "")
                        {
                            return Json(new { success = false, login = true, msg = "Please select valid currency code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (Convert.ToDecimal(units) <= 0 && Convert.ToDecimal(uprice) <= 0)
                    {
                        return Json(new { success = false, login = true, msg = "Units and Unit price must be greater than 0.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    decimal priceAmt = calculatePriceAmount(Convert.ToDecimal(units), Convert.ToDecimal(uprice), cur, company, userDefPro, Convert.ToDecimal(exrng));
                    return Json(new { success = true, login = true, priceAmt = priceAmt }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public decimal calculatePriceAmount(decimal units, decimal uprice, string curencycd, string company, string userDefPro, decimal exrng)
        {
            try
            {

                List<MST_CUR> _cur = CHNLSVC.General.GetAllCurrency(null);
                MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(company, userDefPro);
                //decimal _exchangRate = 0;
                //MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(company, curencycd, DateTime.Now, _pc.Mpc_def_exrate, userDefPro);
                //_exchangRate = (_exc1 != null) ? _exc1.Mer_bnkbuy_rt : 0;
                return exrng * units * uprice;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal getExchangeRt(string curencycd, string company, string userDefPro)
        {
            List<MST_CUR> _cur = CHNLSVC.General.GetAllCurrency(null);
            MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(company, userDefPro);
            decimal _exchangRate = 0;
            MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(company, curencycd, DateTime.Now, _pc.Mpc_def_exrate, userDefPro);
            _exchangRate = (_exc1 != null) ? _exc1.Mer_bnkbuy_rt : 0;
            return _exchangRate;
        }
        public JsonResult getApprovePermission()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (Session["Log_Autho"] != null && Session["Log_Autho"].ToString() != "")
                    {
                        Int32 appPer = Convert.ToInt32(Session["Log_Autho"].ToString());
                        return Json(new { success = true, login = true, appPer = appPer }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Please set up user approve permission.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult addJobDetails(string JobNo, string CstEle, string UOM, string Units, string UnitPrice, string Currency, string Comments, string UploadDate, string VehLcTel, string InvNo, string InvDt, string exrng)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string error = "";
                    if (JobNo != "")
                    {
                        trn_jb_hdr job = new trn_jb_hdr();
                        JobNo = JobNo.Trim();
                        job = CHNLSVC.Sales.GetJobDetails(JobNo, company, out error);
                        if (job.Jb_jb_no != null)
                        {
                            if (error == "")
                            {
                                if (CstEle != "")
                                {
                                    MST_COST_ELEMENT cst = new MST_COST_ELEMENT();

                                    CstEle = CstEle.Trim();
                                    cst = CHNLSVC.Sales.GetCostElementDetails(CstEle, out error);
                                    if (cst.MCE_CD != null)
                                    {
                                        if (error == "")
                                        {
                                            FTW_MES_TP uomdet = new FTW_MES_TP();
                                            if (UOM != "")
                                            {
                                                UOM = UOM.Trim();
                                                uomdet = CHNLSVC.Sales.GetUOMDetails(UOM, out error);
                                                if (uomdet.MT_CD != null)
                                                {
                                                    if (error == "")
                                                    {
                                                        if (VehLcTel != "")
                                                        {
                                                            FTW_VEHICLE_NO veh = new FTW_VEHICLE_NO();
                                                            VehLcTel = VehLcTel.Trim();
                                                            veh = CHNLSVC.Sales.getTelVehLcDet(VehLcTel, out error);
                                                            if (error != "")
                                                            {
                                                                return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                                                            }
                                                            if (veh.FVN_CD == null)
                                                            {
                                                                return Json(new { success = false, login = true, msg = "Invalid Tel/Veh/LC Number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                            }
                                                        }
                                                        TRN_PETTYCASH_REQ_HDR requestHdr = new TRN_PETTYCASH_REQ_HDR();
                                                        List<TRN_PETTYCASH_REQ_DTL> reqDet = new List<TRN_PETTYCASH_REQ_DTL>();
                                                        if (Session["Hdr"] != null)
                                                        {
                                                            requestHdr = (TRN_PETTYCASH_REQ_HDR)Session["Hdr"];
                                                        }
                                                        if (Session["Item"] != null)
                                                        {
                                                            reqDet = (List<TRN_PETTYCASH_REQ_DTL>)Session["Item"];
                                                        }
                                                        Int32 appPer = Convert.ToInt32(Session["Log_Autho"].ToString());
                                                        TRN_MOD_MAX_APPLVL lvl=(TRN_MOD_MAX_APPLVL)Session["MAXAPPLVL"];
                                                         bool perm = CHNLSVC.Security.Is_OptionPerimitted(company, userId, 1004);
                                                         //if (!(requestHdr.TPRH_APP_3 == 0 && appPer == lvl.TMAL_MAX_APPLVL && perm == true))
                                                         //{
                                                         //    if (requestHdr.TPRH_APP_1 == 1 || requestHdr.TPRH_APP_2 == 1 || requestHdr.TPRH_APP_3 == 1)
                                                         //    {
                                                         //        return Json(new { success = false, login = true, msg = "Cannot add jobs to already approved requests by any level.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                         //    }
                                                         //}
                                                         if (appPer == 1)
                                                         {
                                                             if (requestHdr.TPRH_APP_1 == 1)
                                                             {
                                                                 return Json(new { success = false, login = true, msg = "Cannot add jobs request already approved requests by  level 1", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                             }
                                                         }
                                                         else if (appPer == 2)
                                                         {
                                                             if (requestHdr.TPRH_APP_2 == 1)
                                                             {
                                                                 return Json(new { success = false, login = true, msg = "Cannot add jobs request already approved requests by  level 2", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                             }
                                                         }
                                                         else if (appPer == 3)
                                                         {
                                                             if (requestHdr.TPRH_APP_3 == 1)
                                                             {
                                                                 return Json(new { success = false, login = true, msg = "Cannot remove jobs request already approved requests by  level 3", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                             }
                                                         }
                                                         string pemissionMsg = "";
                                                         bool editable = true;
                                                         if (requestHdr.TPRH_APP_3 == 1 || perm != true)
                                                         {
                                                             if (requestHdr.TPRH_APP_3 == 1)
                                                             {
                                                                 editable = false;
                                                                 pemissionMsg = "Alredy approved by level 3 or Maximum approved level user.Cannot update job details";
                                                             }
                                                             else if (perm != true)
                                                             {
                                                                 editable = false;
                                                                 pemissionMsg = "You don't have permission to update pettycash request job details.(Requsted permission code 1004)";
                                                             }

                                                         }
                                                        decimal amt = calculatePriceAmount(Convert.ToDecimal(Units), Convert.ToDecimal(UnitPrice), Currency, company, userDefPro, Convert.ToDecimal(exrng));
                                                        TRN_PETTYCASH_REQ_DTL dtl = new TRN_PETTYCASH_REQ_DTL();
                                                        Int32 lineno = 0;
                                                        if (reqDet.Count > 0)
                                                        {
                                                            lineno = reqDet.Max(x => x.TPRD_LINE_NO);
                                                            dtl.TPRD_LINE_NO = lineno + 1;
                                                        }
                                                        else
                                                        {
                                                            dtl.TPRD_LINE_NO = 1;
                                                        }
                                                        dtl.TPRD_SEQ = requestHdr.TPRH_SEQ;
                                                        dtl.TPRD_REQ_NO = requestHdr.TPRH_REQ_NO;
                                                        dtl.TPRD_VEC_TELE = VehLcTel;
                                                        dtl.TPRD_ELEMENT_CD = CstEle;
                                                        dtl.TPRD_ELEMENT_AMT = amt;
                                                        dtl.TPRD_ELEMENT_DESC = cst.MCE_DESC;
                                                        dtl.TPRD_CURRENCY_CODE = Currency;
                                                        dtl.TPRD_JOB_NO = JobNo;
                                                        dtl.TPRD_CRE_BY = userId;
                                                        dtl.TPRD_CRE_DT = DateTime.Now;
                                                        dtl.TPRD_BALANCE_SET = amt;
                                                        dtl.TPRD_COMMENTS = Comments;
                                                        dtl.TPRD_UOM = UOM;
                                                        dtl.TPRD_NO_UNITS = Convert.ToDecimal(Units);
                                                        dtl.TPRD_UNIT_PRICE = Convert.ToDecimal(UnitPrice);
                                                        dtl.TPRD_EX_RATE = Convert.ToDecimal(exrng);//getExchangeRt(Currency, company, userDefPro);
                                                        dtl.TPRD_PRO_CURRENCY = Currency;
                                                        dtl.TPRD_INV_NO = InvNo.Trim();
                                                        dtl.TPRD_ACT = 1;
                                                        if (InvDt != "")
                                                        {
                                                            try
                                                            {
                                                                DateTime ab = Convert.ToDateTime(InvDt);
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                return Json(new { success = false, login = true, msg = "Please enter valid invoice date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                            }
                                                        }
                                                        dtl.TPRD_INV_DT = InvDt;
                                                        try
                                                        {
                                                            dtl.TPRD_UPLOAD_DATE = Convert.ToDateTime(UploadDate);
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            return Json(new { success = false, login = true, msg = "Please enter valid upload date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                        }
                                                        reqDet.Add(dtl);
                                                        Session["Item"] = reqDet;
                                                        decimal tot = reqDet.Where(x=>x.TPRD_ACT == 1).Sum(x => x.TPRD_ELEMENT_AMT);
                                                        return Json(new { success = true, login = true, reqDet = reqDet, total = tot, updateJob = editable, pemissionMsg = pemissionMsg }, JsonRequestBehavior.AllowGet);
                                                    }
                                                    else
                                                    {
                                                        return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                                                    }
                                                }
                                                else
                                                {
                                                    return Json(new { success = false, login = true, msg = "Invalid UOM.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                            else
                                            {
                                                return Json(new { success = false, login = true, msg = "Please enter UOM.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }

                                        }
                                        else
                                        {
                                            return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    else
                                    {
                                        return Json(new { success = false, login = true, msg = "Invalid cost element.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Please enter cost element.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid job number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Please enter job number.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        [HttpPost]
        public JsonResult savePettyCaseDet(FormCollection data)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string sessionid = Session["SessionID"].ToString();
                    if (data["RequestNo"] != null && data["RequestNo"].ToString() != "")
                    {
                        return Json(new { success = false, login = true, msg = "Unable to save exists request number", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    //if (data["ProfitCenter"].ToString() != "")
                    //{
                    //MST_PROFIT_CENTER pc = new MST_PROFIT_CENTER();
                    //string pccd = data["ProfitCenter"].ToString().Trim();
                    //pc = CHNLSVC.Sales.getProfitCenterDetails(pccd, company, userId);
                    //if (pc.MPC_CD != null)
                    //{
                    string empCd = data["ReqBy"].ToString().Trim();

                    MST_EMP EMP = new MST_EMP();
                    if (empCd != "")
                    {
                        empCd = empCd.Trim();
                        EMP = CHNLSVC.Sales.getEmployeeDetails(empCd, company);
                        if (EMP.ESEP_EPF != null)
                        {

                            List<TRN_PETTYCASH_REQ_DTL> reqDet = new List<TRN_PETTYCASH_REQ_DTL>();
                            if (Session["Item"] != null)
                            {
                                reqDet = (List<TRN_PETTYCASH_REQ_DTL>)Session["Item"];
                            }
                            if (reqDet.Count == 0)
                            {
                                return Json(new { success = false, login = true, msg = "Please add jobs.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            TRN_PETTYCASH_REQ_HDR hdr = new TRN_PETTYCASH_REQ_HDR();
                            try
                            {
                                //hdr.TPRH_REQ_DT = Convert.ToDateTime(data["ReqDate"].ToString());
                                //hdr.TPRH_REQ_DT = Convert.ToDateTime(data["ReqDate"]).Date;
                                hdr.TPRH_REQ_DT = DateTime.Now.Date;
                            }
                            catch (Exception ex)
                            {
                                return Json(new { success = false, login = true, msg = "Please enter valid request date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }

                            hdr.TPRH_REQ_BY = Request["ReqBy"].ToString().Trim();
                            hdr.TPRH_PC_CD = userDefPro;//Request["ProfitCenter"].ToString().Trim();
                            hdr.TPRH_MANUAL_REF = Request["ManualRef"].ToString().Trim();
                            hdr.TPRH_REMARKS = Request["Remarks"].ToString().Trim();
                            hdr.TPRH_TYPE = data["ReqType"].ToString();
                            try
                            {
                                hdr.TPRH_PAYMENT_DT = Convert.ToDateTime(data["PayDate"].ToString());
                            }
                            catch (Exception ex)
                            {
                                return Json(new { success = false, login = true, msg = "Please enter valid payment date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            hdr.TPRH_CRE_BY = userId;
                            hdr.TPRH_CRE_DT = DateTime.Now;
                            hdr.TPRH_STUS = "P";
                            hdr.TPRH_SETTLE = "P";
                            hdr.TPRH_TOT_AMT = reqDet.Sum(x => x.TPRD_ELEMENT_AMT);
                            if (hdr.TPRH_TYPE == "PAYREQ")
                            {
                                try
                                {
                                    hdr.TPRH_PAY_TO = data["PayTo"].ToString().Trim();
                                }
                                catch (Exception)
                                {
                                    return Json(new { success = false, login = true, msg = "Invalid pay to customer code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                MST_BUSENTITY cus = new MST_BUSENTITY();

                                cus = CHNLSVC.Sales.getConsigneeDetailsByAccCode(hdr.TPRH_PAY_TO, company, "AC");
                                hdr.TPRH_PAY_TO_ADD1 = cus.MBE_ADD1;
                                hdr.TPRH_PAY_TO_ADD2 = cus.MBE_ADD2;
                                hdr.TPRH_PAY_TO_NAME = cus.MBE_NAME;
                            }
                            else if (hdr.TPRH_TYPE == "TTREQ")
                            {
                                try
                                {
                                    hdr.TPRH_PAY_TO = data["Consignee"].ToString().Trim();
                                }
                                catch (Exception)
                                {
                                    return Json(new { success = false, login = true, msg = "Invalid consignee code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }

                                MST_BUSENTITY cus = new MST_BUSENTITY();

                                cus = CHNLSVC.Sales.getConsigneeDetailsByAccCode(hdr.TPRH_PAY_TO, company, "CS");
                                hdr.TPRH_PAY_TO_ADD1 = cus.MBE_ADD1;
                                hdr.TPRH_PAY_TO_ADD2 = cus.MBE_ADD2;
                                hdr.TPRH_PAY_TO_NAME = cus.MBE_NAME;
                            }
                            hdr.TPRH_COM_CD = company;
                            hdr.TPRH_CRE_SESSION_ID = sessionid;
                            MasterAutoNumber _ptyAuto = new MasterAutoNumber();
                           // _ptyAuto.Aut_cate_cd = userDefPro;
                           // _ptyAuto.Aut_cate_tp = "PC";
                            _ptyAuto.Aut_cate_cd = company;
                            _ptyAuto.Aut_cate_tp = "COM";
                            _ptyAuto.Aut_direction = 1;
                            _ptyAuto.Aut_modify_dt = DateTime.MinValue;
                            _ptyAuto.Aut_moduleid = "REQ";
                            _ptyAuto.Aut_number = 0;
                            //_ptyAuto.Aut_start_char = userDefPro;
                            _ptyAuto.Aut_start_char = "REQ";
                            _ptyAuto.Aut_year = Convert.ToDateTime(hdr.TPRH_REQ_DT).Year;

                            string error = "";
                            Int32 res = CHNLSVC.Sales.savePetttyCashRequest(hdr, reqDet, _ptyAuto, out error);
                            if (res < 0)
                            {
                                return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            return Json(new { success = true, login = true, msg = "Successfully saved request no :" + error + ". ", type = "Success" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid request by user code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid request by user.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }




                    //}
                    //else
                    //{
                    //    return Json(new { success = false, login = true, msg = "Invalid profit center code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    //}


                    //}
                    //else
                    //{
                    //    return Json(new { success = false, login = true, msg = "Please enter profit center.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    //}
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult clearSession()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    Session["Hdr"] = null;
                    Session["Item"] = null;
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpPost]
        public JsonResult approveRequest(string reqno, string type, string appLvl, DateTime paydate)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string sessionid = Session["SessionID"].ToString();
                    string error = "";
                    TRN_PETTYCASH_REQ_HDR request = CHNLSVC.Sales.getReqyestDetials(type, reqno, company, userDefPro, out error);
                    if (error == "")
                    {
                        if (request.TPRH_REQ_NO != null)
                        {
                            if (Session["Log_Autho"] != null && Session["Log_Autho"].ToString() != "")
                            {
                                Int32 log_autho = Convert.ToInt32(Session["Log_Autho"].ToString());
                                Int32 appl = Convert.ToInt32(appLvl);
                                if (log_autho != appl)
                                {
                                    return Json(new { success = false, login = true, msg = "You don't have permission to approve this level.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                if (appl == 1)
                                {
                                    if (request.TPRH_APP_1 == 1)
                                    {
                                        return Json(new { success = false, login = true, msg = "Request is already approved in Level 1.", type = "Info" }, JsonRequestBehavior.AllowGet);

                                    }
                                    else
                                    {
                                        request.TPRH_APP_1 = 1;
                                        request.TPRH_APP_1_BY = userId;
                                        request.TPRH_APP_1_DT = DateTime.Now;
                                        request.TPRH_MOD_SESSION_ID = sessionid;
                                    }
                                }
                                else if (appl == 2)
                                {
                                    if (request.TPRH_APP_2 == 1)
                                    {
                                        return Json(new { success = false, login = true, msg = "Request is already approved in Level 2.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        if (request.TPRH_APP_1 == 0)
                                        {
                                            return Json(new { success = false, login = true, msg = "Request must be approved in Level 1.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                        request.TPRH_APP_2 = 1;
                                        request.TPRH_APP_2_BY = userId;
                                        request.TPRH_APP_2_DT = DateTime.Now;
                                        request.TPRH_MOD_SESSION_ID = sessionid;
                                    }
                                }
                                else if (appl == 3)
                                {
                                    if (request.TPRH_APP_3 == 1)
                                    {
                                        return Json(new { success = false, login = true, msg = "Request is already approved in Level 3.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        if (request.TPRH_APP_2 == 0)
                                        {
                                            return Json(new { success = false, login = true, msg = "Request must be approved in Level 2.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                        request.TPRH_APP_3 = 1;
                                        request.TPRH_APP_3_BY = userId;
                                        request.TPRH_APP_3_DT = DateTime.Now;
                                        request.TPRH_MOD_SESSION_ID = sessionid;
                                        request.TPRH_PAYMENT_DT = paydate;
                                    }
                                }
                                request.TPRH_MOD_BY = userId;
                                request.TPRH_MOD_DT = DateTime.Now;
                                if (request.TPRH_APP_3 == 1 && appl == 3)
                                {
                                    request.TPRH_STUS = "A";
                                }
                                Int32 eff = CHNLSVC.Sales.updateRequestApproveStus(request, appl, out error);
                                if (eff == 1)
                                {
                                    return Json(new { success = true, login = true, type = "Success", msg = "Successfully approved request " + reqno + " by level " + appLvl }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    if (error != "")
                                    {
                                        return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        return Json(new { success = false, login = true, msg = "Unable to approve request.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "Please set up user approve permission.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }


                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid request number", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult loadPendingRequests(string fromdt, string todt)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    DateTime fmdt;
                    DateTime tdt;
                    try
                    {
                        fmdt = Convert.ToDateTime(fromdt);
                        tdt = Convert.ToDateTime(todt);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter date range", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }

                    if (fmdt > tdt)
                    {
                        return Json(new { success = false, login = true, msg = "Invalid date range", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    if (Session["Log_Autho"] == null || Session["Log_Autho"].ToString() == "")
                    {
                        return Json(new { success = false, login = true, msg = "Please set up user approve permission.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    Int32 applvl = Convert.ToInt32(Session["Log_Autho"].ToString());
                    string error = "";
                    List<TRN_PETTYCASH_REQ_HDR> data = CHNLSVC.Sales.loadPendingptyReq(company, userDefPro, fmdt, tdt, applvl, out error);
                    if (error == "")
                    {
                        foreach (TRN_PETTYCASH_REQ_HDR dt in data)
                        {
                            dt.TPRH_TYPE_DESC = CHNLSVC.Sales.requestTypeDesc(dt.TPRH_SEQ);
                        }
                        return Json(new { success = true, login = true, data = data }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult loadRequestDetailsbySeq(string seq)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (seq == "" || seq == null)
                    {
                        return Json(new { success = false, login = true, msg = "Invalid request number", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    string error = "";
                    TRN_PETTYCASH_REQ_HDR request = CHNLSVC.Sales.loadRequestDetailsbySeq(Convert.ToInt32(seq), company, userDefPro, out error);
                    if (error == "")
                    {
                        if (request.TPRH_REQ_NO != null)
                        {
                            List<TRN_PETTYCASH_REQ_DTL> reqDet = CHNLSVC.Sales.getReqyestItemDetials(request.TPRH_SEQ, out error);
                            if (error == "")
                            {
                                Session["Item"] = reqDet;
                                Session["Hdr"] = request;
                                decimal tot = reqDet.Sum(x => x.TPRD_ELEMENT_AMT);
                                return Json(new { success = true, login = true, data = request, item = reqDet, total = tot }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid request number", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult validatePrint(string seq)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (seq == "" || seq == null)
                    {
                        return Json(new { success = false, login = true, msg = "Invalid request number", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    string error = "";
                    TRN_PETTYCASH_REQ_HDR request = CHNLSVC.Sales.loadRequestDetailsbySeq_val(Convert.ToInt32(seq), company, userDefPro, out error);//loadRequestDetailsbySeq
                    SystemUser _userLvl = CHNLSVC.Security.GetUserByUserID(userId);
                    if (error == "")
                    {
                        if (request.TPRH_REQ_NO != null)
                        {
                            if (request.TPRH_APP_2 == 1)
                            {
                                //if (request.TPRH_APP_3 == 1)
                                if ((request.TPRH_APP_2 == 1 && (_userLvl.Se_Log_Autho == 2 || _userLvl.Se_Log_Autho == 1) && request.TPRH_APP_3 != 1) || (request.TPRH_APP_2 == 1 && _userLvl.Se_Log_Autho == 0 && request.TPRH_APP_3 != 1) || (_userLvl.Se_Log_Autho == 3))
                                {
                                    //if (request.TPRH_IS_PRINT != 1)
                                    //{
                                        return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                                    //}
                                    //else
                                    //{
                                    //    return Json(new { success = false, login = true, msg = "This Request Already Printed and Approved by Accounts.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    //}
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "This Request Already Printed and Approved by Accounts.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                //else
                                //{
                                //    return Json(new { success = false, login = true, msg = "Need level 3 approve to print.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                //}
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "Need level 2 approve to print.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid request number", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult Print(string seq)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string error = "";
                Int32 seqNo = Convert.ToInt32(seq);
                TRN_PETTYCASH_REQ_HDR request = CHNLSVC.Sales.loadRequestDetailsbySeq_val(seqNo, company, userDefPro, out error);
                if (error == "")
                {
                    if (request.TPRH_REQ_NO != null)
                    {
                        string reportName = "";
                        string fileName = "";
                        string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"].ToString();
                        string report = "";
                        ReportDocument rd = new ReportDocument();
                        DataTable rptData = new DataTable();
                        rptData = CHNLSVC.Sales.getPetyCshRptData(seqNo, request.TPRH_TYPE, out error);
                        DataTable comData = new DataTable("comdata");
                        comData = CHNLSVC.Sales.getCompanyDetailsBycd(company);
                        SystemUser _userLvl = CHNLSVC.Security.GetUserByUserID(userId);

                        string err_option = "";
                        string err_form = "";

                        if (request.TPRH_TYPE == "PTCSHREQ")
                        {

                            err_option = "PTCSHREQ-PDF";
                            err_form = "PTCSHREQ";
                            //reportName = "rpt_PettyCashVoucher.rpt";
                            fileName = "Petty Cash Voucher.pdf";
                            //report = ReportPath + "\\" + reportName;
                            //rd.Load(report);
                            //rd.Database.Tables["PettyCashVoucher"].SetDataSource(rptData);
                            //rd.Database.Tables["COMPANY"].SetDataSource(comData);

                            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "rpt_PettyCashVoucher.rpt"));

                            rd.Database.Tables["PettyCashVoucher"].SetDataSource(rptData);
                            rd.Database.Tables["COMPANY"].SetDataSource(comData);

                        }
                        else if (request.TPRH_TYPE == "PAYREQ")
                        {
                            err_option = "PAYREQ-PDF";
                            err_form = "PAYREQ";
                            //reportName = "rpt_PaymentVoucher.rpt";
                            fileName = "Payment Voucher.pdf";
                            //report = ReportPath + "\\" + reportName;
                            //rd.Load(report);
                            //rd.Database.Tables["PaymentVoucher"].SetDataSource(rptData);
                            //rd.Database.Tables["COMPANY"].SetDataSource(comData);

                            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "rpt_PaymentVoucher.rpt"));

                            rd.Database.Tables["PaymentVoucher"].SetDataSource(rptData);
                            rd.Database.Tables["COMPANY"].SetDataSource(comData);
                        }
                        else if (request.TPRH_TYPE == "TTREQ")
                        {
                            err_option = "TTREQ-PDF";
                            err_form = "TTREQ";
                            //reportName = "rpt_TT_Request.rpt";
                            fileName = "TT Payment Voucher.pdf";
                            //report = ReportPath + "\\" + reportName;
                            //rd.Load(report);
                            //rd.Database.Tables["PaymentVoucher"].SetDataSource(rptData);
                            //rd.Database.Tables["COMPANY"].SetDataSource(comData);

                            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "rpt_TT_Request.rpt"));

                            rd.Database.Tables["PaymentVoucher"].SetDataSource(rptData);
                            rd.Database.Tables["COMPANY"].SetDataSource(comData);
                        }
                        Response.Buffer = false;
                        Response.ClearContent();
                        Response.ClearHeaders();
                        try
                        {
                            this.Response.Clear();
                            this.Response.ContentType = "application/pdf";
                            Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                            //return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                            //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            //stream.Seek(0, SeekOrigin.Begin);
                            //return File(stream, "application/pdf", "EverestList.pdf");

                            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            stream.Seek(0, SeekOrigin.Begin);
                            rd.Close();
                            rd.Dispose();
                            return File(stream, "application/pdf"); 
                        }
                        catch (Exception ex)
                        {
                            CHNLSVC.General.SaveReportErrorLog(err_option, err_form, ex.Message, Session["UserID"].ToString());
                            throw;
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid request number", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Redirect("~/Login");
            }
        }
        public JsonResult rejectRequest(string seq)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (seq == "" || seq == null)
                    {
                        return Json(new { success = false, login = true, msg = "Invalid request number", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    string error = "";
                    TRN_PETTYCASH_REQ_HDR request = CHNLSVC.Sales.loadRequestDetailsbySeq(Convert.ToInt32(seq), company, userDefPro, out error);
                    if (error == "")
                    {
                        if (request.TPRH_REQ_NO != null)
                        {
                            Int32 sqNo = Convert.ToInt32(seq);
                            string sessionid = Session["SessionID"].ToString();
                            //if (Session["Log_Autho"] != null && Session["Log_Autho"].ToString() != "")
                            //{
                            //Int32 log_autho = Convert.ToInt32(Session["Log_Autho"].ToString());
                            //if (log_autho == 1)
                            //{
                            //    if (request.TPRH_APP_2 == 1)
                            //    {
                            //        return Json(new { success = false, login = true, msg = "Unable to reject. Request is already approved in Level 2.", type = "Info" }, JsonRequestBehavior.AllowGet);

                            //    }
                            //}
                            //else if (log_autho == 2)
                            //{
                            //    if (request.TPRH_APP_3 == 1)
                            //    {
                            //        return Json(new { success = false, login = true, msg = "Unable to reject. Request is already approved in Level 3.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            //    }
                            //}
                            //else
                            //{
                            //    TRN_MOD_MAX_APPLVL data = (TRN_MOD_MAX_APPLVL)Session["MAXAPPLVL"];
                            //    if(log_autho!=data.TMAL_MAX_APPLVL)
                            //        return Json(new { success = false, login = true, msg = "You don't have permission to reject request.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            //}
                            bool perm = CHNLSVC.Security.Is_OptionPerimitted(company, userId, 1001);
                            if (perm)
                            {
                                Int32 res = CHNLSVC.Sales.rejectPtyCshRequest(sqNo, userId, DateTime.Now, sessionid, out error);
                                if (res > 0)
                                {
                                    return Json(new { success = true, login = true, msg = "Successfully rejected request :" + request.TPRH_REQ_NO, type = "Success" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    if (error == "")
                                    {
                                        return Json(new { success = false, login = true, msg = "Unable to reject request.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }

                                }
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "You don't have permission to reject pettycash request.(Requsted permission code 1001)", type = "Error" }, JsonRequestBehavior.AllowGet);
                            }


                            //}
                            //else
                            //{
                            //    return Json(new { success = false, login = true, msg = "Please set up user approve permission.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            //}


                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid request number", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult updateRequest(string seq, string maualRef, string ManualRef, string Remarks, string ProfitCenter)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (seq == "" || seq == null)
                    {
                        return Json(new { success = false, login = true, msg = "Invalid request number", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    string sessionid = Session["SessionID"].ToString();
                    string error = "";
                    TRN_PETTYCASH_REQ_HDR request = CHNLSVC.Sales.loadRequestDetailsbySeq(Convert.ToInt32(seq), company, userDefPro, out error);
                    if (error == "")
                    {
                        Int32 appPer = Convert.ToInt32(Session["Log_Autho"].ToString());
                        TRN_MOD_MAX_APPLVL lvl = (TRN_MOD_MAX_APPLVL)Session["MAXAPPLVL"];
                        bool perm = CHNLSVC.Security.Is_OptionPerimitted(company, userId, 1004);
                        if (( request.TPRH_APP_3 == 1) || perm != true || appPer < lvl.TMAL_MAX_APPLVL)
                        {
                            if (request.TPRH_APP_1 == 1 && request.TPRH_APP_2 == 1)
                            {

                                if (appPer < lvl.TMAL_MAX_APPLVL)
                                {
                                    return Json(new { success = false, login = true, msg = "Level " + appPer + " users don't have permission for edit job details.", type = "Info" }, JsonRequestBehavior.AllowGet);

                                }
                                else
                                {
                                    if (request.TPRH_APP_3 == 1)
                                    {
                                        return Json(new { success = false, login = true, msg = "Alredy approved by level 3 or Maximum approved level user.Cannot update job details", type = "Info" }, JsonRequestBehavior.AllowGet);

                                    }
                                    else if (perm != true)
                                    {
                                        return Json(new { success = false, login = true, msg = "You don't have permission to update pettycash request job details.(Requsted permission code 1004)", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                        //if (!(request.TPRH_APP_3 == 0 && appPer == lvl.TMAL_MAX_APPLVL && perm == true))
                        //{
                        //    if (request.TPRH_APP_1 == 1 || request.TPRH_APP_2 == 1 || request.TPRH_APP_3 == 1)
                        //    {
                        //        return Json(new { success = false, login = true, msg = "Cannot update already approved requests by any level", type = "Info" }, JsonRequestBehavior.AllowGet);
                        //    }
                        //}
                        if (appPer == 1)
                        {
                            if (request.TPRH_APP_1 == 1)
                            {
                                return Json(new { success = false, login = true, msg = "Cannot remove jobs request already approved requests by  level 1", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else if (appPer == 2)
                        {
                            if (request.TPRH_APP_2 == 1)
                            {
                                return Json(new { success = false, login = true, msg = "Cannot remove jobs request already approved requests by  level 2", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else if (appPer == 3)
                        {
                            if (request.TPRH_APP_3 == 1)
                            {
                                return Json(new { success = false, login = true, msg = "Cannot remove jobs request already approved requests by  level 3", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }

                        if (request.TPRH_STUS != "P")
                        {
                            return Json(new { success = false, login = true, msg = "Only pending requests can be updated.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (request.TPRH_SETTLE == "S")
                        {
                            return Json(new { success = false, login = true, msg = "Already settled request cannot be updated.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        request.TPRH_MANUAL_REF = ManualRef.Trim();
                        request.TPRH_PC_CD = ProfitCenter;
                        request.TPRH_REMARKS = Remarks.Trim();
                        request.TPRH_MOD_BY = userId;
                        request.TPRH_MOD_DT = DateTime.Now;
                        request.TPRH_MOD_SESSION_ID = sessionid;

                        List<TRN_PETTYCASH_REQ_DTL> reqDet = new List<TRN_PETTYCASH_REQ_DTL>();
                        if (Session["Item"] != null)
                        {
                            reqDet = (List<TRN_PETTYCASH_REQ_DTL>)Session["Item"];
                        }
                        if (reqDet.Count == 0)
                        {
                            return Json(new { success = false, login = true, msg = "Please add jobs.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        Int32 res = CHNLSVC.Sales.updatePetttyCashRequest(request, reqDet, out error);
                        if (res < 0)
                        {
                            return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { success = true, login = true, msg = "Petty case request update success.Request no :" + request.TPRH_REQ_NO + ". ", type = "Success" }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult removeJob(string itmline, string jobnum, string seq)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (seq.Trim() != "")
                    {
                        string error = "";
                        TRN_PETTYCASH_REQ_HDR request = CHNLSVC.Sales.loadRequestDetailsbySeq(Convert.ToInt32(seq), company, userDefPro, out error);
                        if (error == "")
                        {
                            Int32 appPer = Convert.ToInt32(Session["Log_Autho"].ToString());
                            TRN_MOD_MAX_APPLVL lvl = (TRN_MOD_MAX_APPLVL)Session["MAXAPPLVL"];
                            bool perm = CHNLSVC.Security.Is_OptionPerimitted(company, userId, 1004);
                            string pemissionMsg = "";
                            bool editable = true;
                            if (request.TPRH_APP_3 == 1 || perm != true || appPer < lvl.TMAL_MAX_APPLVL)
                            {
                                if (request.TPRH_APP_1 == 1 && request.TPRH_APP_2 == 1)
                                {
                                    if (appPer < lvl.TMAL_MAX_APPLVL)
                                    {
                                        editable = false;
                                        pemissionMsg = "Level " + appPer + " users don't have permission for edit job details.";

                                    }
                                    else
                                    {
                                        if (request.TPRH_APP_3 == 1)
                                        {
                                            editable = false;
                                            pemissionMsg = "Alredy approved by level 3 or Maximum approved level user.Cannot update job details";
                                        }
                                        else if (perm != true)
                                        {
                                            editable = false;
                                            pemissionMsg = "You don't have permission to update pettycash request job details.(Requsted permission code 1004)";
                                        }
                                    }
                                }
                            }
                            //if (!(request.TPRH_APP_3 == 0 && appPer == lvl.TMAL_MAX_APPLVL && perm == true))
                            //{
                            //    if (request.TPRH_APP_1 == 1 || request.TPRH_APP_2 == 1 || request.TPRH_APP_3 == 1)
                            //    {
                            //        return Json(new { success = false, login = true, msg = "Cannot remove jobs request already approved requests by any level", type = "Info" }, JsonRequestBehavior.AllowGet);
                            //    }
                            //}                    
                            if (appPer == 1)
                            {
                                if (request.TPRH_APP_1 == 1)
                                {
                                    return Json(new { success = false, login = true, msg = "Cannot remove jobs request already approved requests by  level 1", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else if (appPer == 2)
                            {
                                if (request.TPRH_APP_2 == 1)
                                {
                                    return Json(new { success = false, login = true, msg = "Cannot remove jobs request already approved requests by  level 2", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else if (appPer == 3)
                            {
                                if (request.TPRH_APP_3 == 1)
                                {
                                    return Json(new { success = false, login = true, msg = "Cannot remove jobs request already approved requests by  level 3", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            if (request.TPRH_STUS != "P")
                            {
                                return Json(new { success = false, login = true, msg = "Only pending requests job can be remove or update.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            if (request.TPRH_SETTLE == "S")
                            {
                                return Json(new { success = false, login = true, msg = "Already settled request job cannot be remove or update.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            List<TRN_PETTYCASH_REQ_DTL> reqDet = new List<TRN_PETTYCASH_REQ_DTL>();
                            if (Session["Item"] != null)
                            {
                                reqDet = (List<TRN_PETTYCASH_REQ_DTL>)Session["Item"];
                            }

                            if (reqDet.Count > 0)
                            {
                                try
                                {
                                    foreach (TRN_PETTYCASH_REQ_DTL rq in reqDet)
                                    {
                                        if (rq.TPRD_JOB_NO == jobnum.Trim() && rq.TPRD_LINE_NO == Convert.ToInt32(itmline))
                                        {
                                            rq.TPRD_ACT = 0;
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    return Json(new { success = false, login = true, msg = e.ToString(), type = "Info" }, JsonRequestBehavior.AllowGet);
                                }

                                Session["Item"] = reqDet;
                                decimal tot = reqDet.Where(y => y.TPRD_ACT == 1).Sum(x => x.TPRD_ELEMENT_AMT);
                                return Json(new { success = true, login = true, reqDet = reqDet, total = tot , updateJob = editable, pemissionMsg = pemissionMsg}, JsonRequestBehavior.AllowGet);

                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        List<TRN_PETTYCASH_REQ_DTL> reqDet = new List<TRN_PETTYCASH_REQ_DTL>();
                        if (Session["Item"] != null)
                        {
                            reqDet = (List<TRN_PETTYCASH_REQ_DTL>)Session["Item"];
                        }

                        if (reqDet.Count > 0)
                        {
                            try
                            {
                                var itemToRemove = reqDet.First(r => r.TPRD_LINE_NO == Convert.ToInt32(itmline) && r.TPRD_JOB_NO == jobnum);
                                reqDet.Remove(itemToRemove);
                                Int32 i = 1;
                                foreach (TRN_PETTYCASH_REQ_DTL rq in reqDet)
                                {
                                    rq.TPRD_LINE_NO = i;
                                    i++;
                                }

                            }
                            catch (Exception e)
                            {
                                return Json(new { success = false, login = true, msg = e.ToString(), type = "Info" }, JsonRequestBehavior.AllowGet);
                            }

                            Session["Item"] = reqDet;
                            decimal tot = reqDet.Where(y => y.TPRD_ACT == 1).Sum(x => x.TPRD_ELEMENT_AMT);
                            return Json(new { success = true, login = true, reqDet = reqDet, total = tot }, JsonRequestBehavior.AllowGet);

                        }
                    }

                    return Json(new { success = false, login = true, reqDet = "", total = 0 }, JsonRequestBehavior.AllowGet);

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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getRequestUser()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                MST_EMP EMP = new MST_EMP();
                if (userId != "")
                {
                    userId = userId.Trim();
                    EMP = CHNLSVC.Sales.getReqEmployeeDetails(userId, company);
                }
                return Json(new { success = true, login = true, data = EMP }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}