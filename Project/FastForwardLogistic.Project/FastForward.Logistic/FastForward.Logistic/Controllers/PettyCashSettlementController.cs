using CrystalDecisions.CrystalReports.Engine;
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
    public class PettyCashSettlementController : BaseController
    {
        // GET: PettyCashSettlement
        int countnum = 1;
        public ActionResult Index()
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            Session["RefundSettlement"] = null;
            Session["ToRefundSettlement"] = null;
            Session["RecieptItemList"] = null;
            Session["RefundSettlementColl"] = null;
            Session["RecieptItemList"] = null;
            Session.Remove("RefundSettlement");
            Session.Remove("ToRefundSettlement");
            Session.Remove("RecieptItemList");
            Session["permissonSession"] = null;
            
            
            if (CHNLSVC.Security.Is_OptionPerimitted(company, userId.ToUpper(), 1006))
            {
                Session["permissonSession"] = true;
            }
            else
            {
                Session["permissonSession"] = false;
            }
            


            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                SEC_SYSTEM_MENU per = CHNLSVC.Security.getUserPermission(userId, company, 9);
                if (per.SSM_ID != 0)
                {

                    TRN_MOD_MAX_APPLVL data = CHNLSVC.General.getMaxAppLvlPermission("PTYCSHREQSETTLE", company);
                    if (data.TMAL_MODULE != null)
                    {
                        Session["MAXAPPLVL"] = data;
                    }
                    else
                    {
                        data.TMAL_COM = company;
                        data.TMAL_MAX_APPLVL = 3;
                        data.TMAL_MODULE = "PTYCSHREQSETTLE";
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
        public JsonResult loadPendingSettlementRequests(string fromdt, string todt, string jobno)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    DateTime fmdt = DateTime.MinValue;
                    try
                    {
                        fmdt = Convert.ToDateTime(fromdt);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, login = true, msg = "Please select valid from date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    DateTime tdt = DateTime.MinValue;
                    try
                    {
                        tdt = Convert.ToDateTime(todt);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, login = true, msg = "Please select valid to date.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    string type = "PTCSHREQ";
                    List<TRN_PETTYCASH_REQ_HDR> data = CHNLSVC.Sales.loadPendingSetReq(company, userDefPro, type, fmdt, tdt,jobno, out error);
                    if (error == "")
                    {
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

        public JsonResult loadJobPendingSettlementRequests(string fromdt, string todt, string jobno)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    DateTime fmdt = DateTime.MinValue;
                    try
                    {
                        fmdt = Convert.ToDateTime(fromdt);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, login = true, msg = "Please select valid from date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    DateTime tdt = DateTime.MinValue;
                    try
                    {
                        tdt = Convert.ToDateTime(todt);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, login = true, msg = "Please select valid to date.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    string type = "PTCSHREQ";
                    List<TRN_PETTYCASH_REQ_HDR> data = CHNLSVC.Sales.loadPendingSetReq(company, userDefPro, type, fmdt, tdt, jobno, out error);
                    if (error == "")
                    {
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

        public JsonResult getSetlementDetails(string seq, string reqNo)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (reqNo == "")
                    {
                        return Json(new { success = false, login = true, msg = "Invalid settlement number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    //Int32 reqSeq = Convert.ToInt32(seq);
                    Int32 appPer = Convert.ToInt32(Session["Log_Autho"].ToString());
                    string error = "";
                    TRN_PETTYCASH_SETTLE_HDR data = CHNLSVC.Sales.loadSettlementHdr(company, userDefPro, reqNo, out error);
                    if (data.TPSH_SETTLE_NO != null)
                    {
                        if (data.TPSH_STUS == "C")
                        {
                            return Json(new { success = false, login = true, msg = "Settlement '" + reqNo + "' already canceled by " + data.TPSH_MOD_BY, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (data.TPSH_STUS == "R")
                        {
                            return Json(new { success = false, login = true, msg = "Settlement '" + reqNo + "' already rejected by " + data.TPSH_MOD_BY, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        List<TRN_PETTYCASH_SETTLE_DTL> itmdet = CHNLSVC.Sales.loadSettlementDet(company, userDefPro, reqNo, data.TPSH_SEQ_NO, out  error);
                        if (error == "")
                        {
                            string pemissionMsg = "";
                            bool editable = true;
                            bool perm = CHNLSVC.Security.Is_OptionPerimitted(company, userId, 1003);
                            if (!perm)
                            {
                                if (data.TPSH_APP3 == 1)
                                {
                                    editable = false;
                                    pemissionMsg = "You don't have permission to update pettycash settlement job details.(Requsted permission code 1003)";
                                }
                            }
                            else
                            {
                                if (appPer == 1 || appPer == 2)
                                {
                                    editable = false;
                                    pemissionMsg = "Unable to edit settlement details for level " + appPer + " users";
                                }
                            }

                            Session["setHdr"] = data;
                            Session["SetDet"] = itmdet;

                            //List<TRN_PETTYCASH_SETTLE_DTL> settleGroup = (from sg in itmdet
                            //                                              group sg by new
                            //                                              {
                            //                                                  TPSD_JOB_NO = sg.TPSD_JOB_NO
                            //                                              } into sgd
                            //                                              select new TRN_PETTYCASH_SETTLE_DTL
                            //                                              {
                            //                                                  TPSD_REQ_NO = sgd.Select(x => x.TPSD_REQ_NO).FirstOrDefault(),
                            //                                                  TPSD_JOB_NO = sgd.Key.TPSD_JOB_NO,
                            //                                                  TPSD_ELEMENT_CD = "REFUND",
                            //                                                  TPSD_ELEMENT_DESC = "REFUND",
                            //                                                  TPSD_REQ_AMT = 0, //sgd.Sum(x => x.TPSD_REQ_AMT),
                            //                                                  TPSD_SETTLE_AMT = 0 - (sgd.Sum(x => x.TPSD_REQ_AMT) - sgd.Sum(x => x.TPSD_SETTLE_AMT)),
                            //                                                  TPSD_CRE_BY = sgd.Select(x => x.TPSD_CRE_BY).FirstOrDefault(),
                            //                                                  TPSD_LINE_NO = sgd.Select(x => x.TPSD_LINE_NO).LastOrDefault() + 1,
                            //                                                  TPSD_CRE_DT = sgd.Select(x => x.TPSD_CRE_DT).FirstOrDefault(),
                            //                                                  TPSD_REMARKS = "", //sgd.Select(x => x.TPSD_REMARKS).FirstOrDefault(),
                            //                                                  TPSD_VEC_TELE = sgd.Select(x => x.TPSD_VEC_TELE).FirstOrDefault(),
                            //                                                  TPSD_ACT = 1 //int.Parse(sgd.Select(x => x.TPSD_REQ_NO).FirstOrDefault())
                            //                                              }).ToList();

                            //Session["RefundSettlement"] = settleGroup;

                            decimal reqAmt = itmdet.Where(x=>x.TPSD_ACT==1).Sum(x => x.TPSD_REQ_AMT);
                            decimal setAmt = itmdet.Where(x => x.TPSD_ACT == 1).Sum(x => x.TPSD_SETTLE_AMT);
                            return Json(new { success = true, login = true, hdr = data, itm = itmdet, reqAmt = reqAmt, setAmt = setAmt, userPermLvl = appPer, editable = editable, pemissionMsg = pemissionMsg }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        if (error != "")
                        {
                            return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid settlement number.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult loadRequstDetails(string seq, string reqno)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (seq != "" && reqno != "")
                    {
                        TRN_PETTYCASH_SETTLE_HDR hdr = new TRN_PETTYCASH_SETTLE_HDR();
                        if (Session["setHdr"] != null)
                        {
                            hdr = (TRN_PETTYCASH_SETTLE_HDR)Session["setHdr"];
                        }
                        if (hdr.TPSH_APP1 == 1 || hdr.TPSH_APP2 == 1 || hdr.TPSH_APP3 == 1)
                        {
                            return Json(new { success = false, login = true, msg = "Can't add request to already approved document.", type = "Info" }, JsonRequestBehavior.AllowGet);

                        }
                        Int32 seqNo = Convert.ToInt32(seq);
                        string error = "";
                        List<TRN_PETTYCASH_REQ_DTL> reqDet = CHNLSVC.Sales.getReqyestItemDetials(seqNo, out error);
                        if (error != "")
                        {
                            return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        if (reqDet.Count > 0)
                        {
                            List<TRN_PETTYCASH_SETTLE_DTL> setDets = new List<TRN_PETTYCASH_SETTLE_DTL>();
                            if (Session["SetDet"] != null)
                            {
                                setDets = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["SetDet"];
                            }
                            //int countnum = 1;
                            foreach (TRN_PETTYCASH_REQ_DTL req in reqDet)
                            {
                                TRN_PETTYCASH_SETTLE_DTL set = new TRN_PETTYCASH_SETTLE_DTL();
                                set.TPSD_REQ_NO = reqno;
                                set.TPSD_JOB_NO = req.TPRD_JOB_NO;
                                set.TPSD_ELEMENT_CD = req.TPRD_ELEMENT_CD;
                                set.TPSD_ELEMENT_DESC = req.TPRD_ELEMENT_DESC;
                                set.TPSD_REQ_AMT = req.TPRD_ELEMENT_AMT;
                                set.TPSD_LINE_NO = req.TPRD_LINE_NO;
                                set.TPSD_CRE_BY = userId;
                                set.TPSD_CRE_DT = DateTime.Now;
                                set.TPSD_REMARKS = req.TPRD_COMMENTS;
                                set.TPSD_VEC_TELE = req.TPRD_VEC_TELE;
                                set.TPSD_ACT = 1;
                                set.TPSD_SET_LINE = countnum++;
                                setDets.Add(set);
                            }
                            decimal reqAmt = setDets.Where(a => a.TPSD_ACT == 1).Sum(x => x.TPSD_REQ_AMT);
                            Session["SetDet"] = setDets;


                            //List<TRN_PETTYCASH_SETTLE_DTL> RefundSettlement = new List<TRN_PETTYCASH_SETTLE_DTL>();
                            //RefundSettlement = CHNLSVC.Sales.LoadAllRefundableJobData(jobno);

                            List<TRN_PETTYCASH_SETTLE_DTL> settleGroup = (from sg in setDets
                                                                          group sg by new
                                                                          {
                                                                              TPSD_SETTLE_NO = sg.TPSD_SETTLE_NO
                                                                          } into sgd
                                                                          select new TRN_PETTYCASH_SETTLE_DTL
                                                                          {
                                                                              TPSD_SETTLE_NO = sgd.Select(x => x.TPSD_SETTLE_NO).FirstOrDefault(),
                                                                              TPSD_REQ_NO = sgd.Select(x => x.TPSD_REQ_NO).FirstOrDefault(),
                                                                              TPSD_JOB_NO = sgd.Select(x => x.TPSD_JOB_NO).FirstOrDefault(),//sgd.Key.TPSD_JOB_NO,
                                                                              TPSD_ELEMENT_CD = "REFUND",
                                                                              TPSD_ELEMENT_DESC = "REFUND",
                                                                              TPSD_REQ_AMT = 0, //sgd.Sum(x => x.TPSD_REQ_AMT),
                                                                              TPSD_SETTLE_AMT = (sgd.Sum(x => x.TPSD_REQ_AMT) - sgd.Sum(x => x.TPSD_SETTLE_AMT)),
                                                                              TPSD_CRE_BY = sgd.Select(x => x.TPSD_CRE_BY).FirstOrDefault(),
                                                                              TPSD_LINE_NO = sgd.Select(x => x.TPSD_LINE_NO).LastOrDefault() + 1,
                                                                              TPSD_CRE_DT = sgd.Select(x => x.TPSD_CRE_DT).FirstOrDefault(),
                                                                              TPSD_REMARKS = "", //sgd.Select(x => x.TPSD_REMARKS).FirstOrDefault(),
                                                                              TPSD_VEC_TELE = sgd.Select(x => x.TPSD_VEC_TELE).FirstOrDefault(),
                                                                              TPSD_ACT = 1 //int.Parse(sgd.Select(x => x.TPSD_REQ_NO).FirstOrDefault())
                                                                          }).ToList();

                            List<TRN_PETTYCASH_SETTLE_DTL> currSettle = new List<TRN_PETTYCASH_SETTLE_DTL>();
                            if (Session["RefundSettlementColl"] != null)
                            {
                                currSettle = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["RefundSettlementColl"];
                                settleGroup.AddRange(currSettle);
                            }

                            Session["RefundSettlementColl"] = settleGroup;

                            //decimal reqAmt = RefundSettlement.AsEnumerable().Sum(o => o.TPSD_REQ_AMT);
                            //decimal setAmt = RefundSettlement.AsEnumerable().Sum(o => o.TPSD_SETTLE_AMT);


                            return Json(new { success = true, login = true, data = setDets, reqAmt = reqAmt }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid request number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "Invalid request number.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult removeRequstDetails(string seq, string reqno)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<TRN_PETTYCASH_SETTLE_DTL> setDets = new List<TRN_PETTYCASH_SETTLE_DTL>();
                    List<TRN_PETTYCASH_SETTLE_DTL> newSetDets = new List<TRN_PETTYCASH_SETTLE_DTL>();
                    if (Session["SetDet"] != null)
                    {
                        setDets = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["SetDet"];
                    }
                    if (setDets.Count > 0)
                    {
                        foreach (TRN_PETTYCASH_SETTLE_DTL set in setDets)
                        {
                            if (set.TPSD_REQ_NO != reqno)
                            {
                                TRN_PETTYCASH_SETTLE_DTL setval = new TRN_PETTYCASH_SETTLE_DTL();
                                setval.TPSD_REQ_NO = set.TPSD_REQ_NO;
                                setval.TPSD_JOB_NO = set.TPSD_JOB_NO;
                                setval.TPSD_ELEMENT_CD = set.TPSD_ELEMENT_CD;
                                setval.TPSD_ELEMENT_DESC = set.TPSD_ELEMENT_DESC;
                                setval.TPSD_REQ_AMT = set.TPSD_REQ_AMT;
                                setval.TPSD_CRE_BY = set.TPSD_CRE_BY;
                                setval.TPSD_LINE_NO = set.TPSD_LINE_NO;
                                setval.TPSD_CRE_DT = set.TPSD_CRE_DT;
                                setval.TPSD_REMARKS = set.TPSD_REMARKS;
                                setval.TPSD_VEC_TELE = set.TPSD_VEC_TELE;
                                setval.TPSD_ACT = set.TPSD_ACT;
                                newSetDets.Add(setval);
                            }
                        }

                        Session["SetDet"] = newSetDets;
                        decimal reqAmt = newSetDets.Where(a => a.TPSD_ACT == 1).Sum(x => x.TPSD_REQ_AMT);
                        return Json(new { success = true, login = true, data = newSetDets, reqAmt = reqAmt }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No settlement element details found to remove.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    Session["setHdr"] = null;
                    Session["SetDet"] = null;
                    Session["newSetDets"] = null;
                    Session["RefundSettlement"] = null;
                    Session["ToRefundSettlement"] = null;
                    Session["RecieptItemList"] = null;
                    Session["RefundSettlementColl"] = null;
                    Session["RecieptItemList"] = null;
                    Session.Remove("RefundSettlement");
                    Session.Remove("ToRefundSettlement");
                    Session.Remove("RecieptItemList");
                    Session.Remove("RefundSettlementColl");

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
        public JsonResult addSettlement(string reqno, string jobno, string lineno)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (reqno != "" && jobno != "" && lineno != "")
                    {
                        List<TRN_PETTYCASH_SETTLE_DTL> setDets = new List<TRN_PETTYCASH_SETTLE_DTL>();
                        if (Session["SetDet"] != null)
                        {
                            setDets = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["SetDet"];
                        }
                        if (setDets.Count > 0)
                        {
                            Int32 line = 0;
                            try
                            {
                                line = Convert.ToInt32(lineno);
                            }
                            catch (Exception e)
                            {
                                return Json(new { success = false, login = true, msg = "Invalid request details(Line).", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            bool isexist = setDets.Exists(x => x.TPSD_JOB_NO == jobno && x.TPSD_LINE_NO == line && x.TPSD_REQ_NO == reqno && x.TPSD_ACT == 1);
                            if (isexist == true)
                            {
                                string error = "";
                                TRN_PETTYCASH_SETTLE_HDR data = CHNLSVC.Sales.loadSettlementHdr(company, userDefPro, reqno, out error);
                                Int32 appPer = Convert.ToInt32(Session["Log_Autho"].ToString());
                                string pemissionMsg = "";
                                bool editable = true;
                                bool perm = CHNLSVC.Security.Is_OptionPerimitted(company, userId, 1003);
                                if (!perm)
                                {
                                    if (data.TPSH_APP3 == 1)
                                    {
                                        editable = false;
                                        pemissionMsg = "You don't have permission to update pettycash settlement job details.(Requsted permission code 1003)";
                                    }
                                }
                                else
                                {
                                    if (appPer == 1 || appPer == 2)
                                    {
                                        editable = true;
                                        pemissionMsg = "Unable to edit settlement details for level " + appPer + " users";
                                    }
                                }

                                TRN_PETTYCASH_SETTLE_DTL itm = setDets.Where(x => x.TPSD_JOB_NO == jobno && x.TPSD_LINE_NO == line && x.TPSD_REQ_NO == reqno && x.TPSD_ACT == 1).ToList()[0];
                                return Json(new { success = true, login = true, reqAmt = itm.TPSD_REQ_AMT, editable = editable, pemissionMsg = pemissionMsg }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "Request is not available in settlement element details.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Request is not available in settlement element details.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid request details.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult addSettleDetails(string req, string jobno, string lineno, string cstEle, string setAmt, string recAttr, string rmk, string veh, string settleNo)
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
                    if (req != "")
                    {
                        if (jobno != "")
                        {
                            if (cstEle != "")
                            {
                                cstEle = cstEle.Trim();
                                MST_COST_ELEMENT cst = CHNLSVC.Sales.GetCostElementDetails(cstEle, out error);
                                if (cst.MCE_CD != null)
                                {
                                    if (error == "")
                                    {
                                        decimal setlementAmt = 0;
                                        if (setAmt != "")
                                        {
                                            try
                                            {
                                                setlementAmt = Convert.ToDecimal(setAmt);
                                            }
                                            catch (Exception e)
                                            {
                                                return Json(new { success = false, login = true, msg = "Please enter valid settle amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }


                                            if (setlementAmt <= 0)
                                            {
                                                return Json(new { success = false, login = true, msg = "Settlement amount should be greater than 0.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }
                                            if (recAttr == "")
                                            {
                                                return Json(new { success = false, login = true, msg = "Please select recipt att.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }
                                            if (veh != "")
                                            {
                                                FTW_VEHICLE_NO vehO = new FTW_VEHICLE_NO();
                                                veh = veh.Trim();
                                                vehO = CHNLSVC.Sales.getTelVehLcDet(veh, out error);
                                                if (error != "")
                                                {
                                                    return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                                if (vehO.FVN_CD == null)
                                                {
                                                    return Json(new { success = false, login = true, msg = "Invalid Tel/Veh/LC Number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                }
                                            }

                                            Int32 line = 0;
                                            try
                                            {
                                                line = Convert.ToInt32(lineno);
                                            }
                                            catch (Exception e)
                                            {
                                                return Json(new { success = false, login = true, msg = "Invalid request details(Line).", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }

                                            List<TRN_PETTYCASH_SETTLE_DTL> setDets = new List<TRN_PETTYCASH_SETTLE_DTL>();
                                            if (Session["SetDet"] != null)
                                            {
                                                setDets = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["SetDet"];
                                            }
                                            List<TRN_PETTYCASH_SETTLE_DTL> newSetDets = new List<TRN_PETTYCASH_SETTLE_DTL>();

                                            if (!string.IsNullOrEmpty(settleNo))
                                            {
                                                if (Session["newSetDets"] != null)
                                                {
                                                    newSetDets = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["newSetDets"];
                                                }
                                            }



                                            bool isexist = setDets.Exists(x => x.TPSD_JOB_NO == jobno && x.TPSD_LINE_NO == line && x.TPSD_REQ_NO == req && x.TPSD_ACT == 1);
                                            if (isexist == true)
                                            {
                                                decimal reqsetamt = setDets.Where(a => a.TPSD_REQ_NO == req && a.TPSD_JOB_NO == jobno && a.TPSD_LINE_NO == line && a.TPSD_ACT == 1).Sum(x => x.TPSD_SETTLE_AMT);
                                                decimal reqAllamt = setDets.Where(a => a.TPSD_REQ_NO == req && a.TPSD_JOB_NO == jobno && a.TPSD_LINE_NO == line && a.TPSD_ACT == 1).Sum(x => x.TPSD_REQ_AMT);
                                                //if (reqAllamt < reqsetamt + setlementAmt)
                                                //{
                                                //    return Json(new { success = false, login = true, msg = "Cannot exceed settle amount than request amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                                //}


                                                TRN_PETTYCASH_SETTLE_DTL setval = new TRN_PETTYCASH_SETTLE_DTL();
                                                setval.TPSD_REQ_NO = req;
                                                setval.TPSD_JOB_NO = jobno;
                                                setval.TPSD_ELEMENT_CD = cst.MCE_CD;
                                                setval.TPSD_ELEMENT_DESC = cst.MCE_DESC;
                                                setval.TPSD_REQ_AMT = 0;
                                                setval.TPSD_SETTLE_AMT = setlementAmt;
                                                setval.TPSD_CRE_BY = userId;
                                                setval.TPSD_LINE_NO = line;
                                                setval.TPSD_CRE_DT = DateTime.Now;
                                                setval.TPSD_REMARKS = rmk;
                                                setval.TPSD_VEC_TELE = veh;
                                                setval.TPSD_ACT = 1;
                                                Int32 setMaxLine = 0;
                                                try
                                                {
                                                    setMaxLine = setDets.Where(x => x.TPSD_SETTLE_AMT > 0).Max(x => x.TPSD_SETLE_LINO_NO);
                                                }
                                                catch (Exception)
                                                {

                                                }
                                                setval.TPSD_SETLE_LINO_NO = setMaxLine + 1;
                                                setval.TPSD_ATT_RECEIPT = Convert.ToInt32(recAttr);
                                                setDets.Add(setval);
                                                Session["SetDet"] = setDets;
                                                newSetDets.Add(setval);
                                                Session["newSetDets"] = newSetDets;

                                                // Group and collect balance data to refund
                                                List<TRN_PETTYCASH_SETTLE_DTL> settleGroupMod = new List<TRN_PETTYCASH_SETTLE_DTL>();
                                                if (Session["SetDet"] != null)
                                                {
                                                    settleGroupMod = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["SetDet"];
                                                }

                                                List<TRN_PETTYCASH_SETTLE_DTL> settleGroup = (from sg in settleGroupMod
                                                                   group sg by new
                                                                   {
                                                                       TPSD_JOB_NO = sg.TPSD_JOB_NO
                                                                   } into sgd
                                                                   select new TRN_PETTYCASH_SETTLE_DTL
                                                                   {
                                                                       TPSD_REQ_NO = sgd.Select(x => x.TPSD_REQ_NO).FirstOrDefault(),
                                                                       TPSD_JOB_NO = sgd.Key.TPSD_JOB_NO,
                                                                       TPSD_ELEMENT_CD = "REFUND",
                                                                       TPSD_ELEMENT_DESC = "REFUND",
                                                                       TPSD_REQ_AMT = 0, //sgd.Sum(x => x.TPSD_REQ_AMT),
                                                                       TPSD_SETTLE_AMT = (sgd.Sum(x => x.TPSD_REQ_AMT) - sgd.Sum(x => x.TPSD_SETTLE_AMT)),
                                                                       TPSD_CRE_BY = sgd.Select(x => x.TPSD_CRE_BY).FirstOrDefault(),
                                                                       TPSD_LINE_NO = sgd.Select(x => x.TPSD_LINE_NO).LastOrDefault() + 1,
                                                                       TPSD_CRE_DT = sgd.Select(x => x.TPSD_CRE_DT).FirstOrDefault(),
                                                                       TPSD_REMARKS = "", //sgd.Select(x => x.TPSD_REMARKS).FirstOrDefault(),
                                                                       TPSD_VEC_TELE = sgd.Select(x => x.TPSD_VEC_TELE).FirstOrDefault(),
                                                                       TPSD_ACT = 1 //int.Parse(sgd.Select(x => x.TPSD_REQ_NO).FirstOrDefault())
                                                                   }).ToList();

                                                Session["RefundSettlement"] = settleGroup;

                                                decimal TotSetAmt = setDets.Where(y => y.TPSD_ACT == 1).Sum(x => x.TPSD_SETTLE_AMT);
                                                decimal TotReqAmt = setDets.Where(y => y.TPSD_ACT == 1).Sum(x => x.TPSD_REQ_AMT);

                                                return Json(new { success = true, login = true, data = setDets, TotSetAmt = TotSetAmt, TotReqAmt = TotReqAmt }, JsonRequestBehavior.AllowGet);
                                            }
                                            else
                                            {
                                                return Json(new { success = false, login = true, msg = "Request not contain in item list.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                            }

                                        }
                                        else
                                        {
                                            return Json(new { success = false, login = true, msg = "Please add settle amount.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                                return Json(new { success = false, login = true, msg = "Please select cost element.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Please select job number.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Please select request number.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult saveSettlmentDet(FormCollection formData)
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
                    DateTime SettlementDate;
                    try
                    {
                        SettlementDate = Convert.ToDateTime(formData["SettlementDate"].ToString());
                    }
                    catch (Exception)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid settlement date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    string ManRef = formData["ManRef"].ToString();
                    DateTime payDt;
                    try
                    {
                        payDt = Convert.ToDateTime(formData["payDt"].ToString());
                    }
                    catch (Exception e)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid payment date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    string ProfitCenter = formData["ProfitCenter"].ToString();
                    string Remarks = formData["Remarks"].ToString();
                    List<TRN_PETTYCASH_SETTLE_DTL> setDets = new List<TRN_PETTYCASH_SETTLE_DTL>();
                    if (Session["SetDet"] != null)
                    {
                        setDets = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["SetDet"];
                    }
                    if (setDets.Count == 0)
                    {
                        return Json(new { success = false, login = true, msg = "Please add settlement job details.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    decimal setAmt = setDets.Where(a => a.TPSD_ACT == 1).Sum(x => x.TPSD_SETTLE_AMT);
                    if (setAmt <= 0)
                    {
                        return Json(new { success = false, login = true, msg = "Please add settlement to save.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    TRN_PETTYCASH_SETTLE_HDR hdr = new TRN_PETTYCASH_SETTLE_HDR();
                    hdr.TPSH_PAY_DT = payDt;
                    hdr.TPSH_PC_CD = userDefPro;
                    hdr.TPSH_SETTLE_AMT = setAmt;
                    hdr.TPSH_MAN_REF = ManRef;
                    hdr.TPSH_SETTLE_DT = SettlementDate;
                    hdr.TPSH_REMARKS = Remarks;
                    hdr.TPSH_STUS = "P";
                    hdr.TPSH_REJECT = 0;
                    hdr.TPSH_CRE_BY = userId;
                    hdr.TPSH_CRE_DT = DateTime.Now;
                    hdr.TPSH_COM_CD = company;
                    hdr.TPSH_CRE_SES_ID = Convert.ToInt32(sessionid);

                    hdr.TPSH_MOD_BY = userId;
                    hdr.TPSH_MOD_DT = DateTime.Now;
                    hdr.TPSH_MOD_SES_ID = Convert.ToInt32(sessionid);


                    MasterAutoNumber _ptyAuto = new MasterAutoNumber();
                    //_ptyAuto.Aut_cate_cd = userDefPro;
                    //_ptyAuto.Aut_cate_tp = "PC";
                    _ptyAuto.Aut_cate_cd = company;
                    _ptyAuto.Aut_cate_tp = "COM";
                    _ptyAuto.Aut_direction = 1;
                    _ptyAuto.Aut_modify_dt = DateTime.MinValue;
                    _ptyAuto.Aut_moduleid = "SET";
                    _ptyAuto.Aut_number = 0;
                    //_ptyAuto.Aut_start_char = userDefPro;
                    _ptyAuto.Aut_start_char = "SET";
                    _ptyAuto.Aut_year = Convert.ToDateTime(SettlementDate).Year;
                    string error = "";
                    string error1 = "";
                    Int32 eff = 0;
                    if (!string.IsNullOrEmpty(formData["SettlementNo"].ToString()))
                    {
                        TRN_PETTYCASH_SETTLE_HDR request = CHNLSVC.Sales.loadSettlementHdr(company, userDefPro, formData["SettlementNo"].ToString().Trim(), out error);

                        List<TRN_PETTYCASH_SETTLE_DTL> newSetDets = new List<TRN_PETTYCASH_SETTLE_DTL>();
                        if (Session["newSetDets"] != null)
                        {
                            newSetDets = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["newSetDets"];
                        }
                        Int32 inccount = setDets.Where(X => X.TPSD_ACT == 1).Count();
                        if (newSetDets.Count == 0 && inccount == 0)
                        {
                            return Json(new { success = false, login = true, msg = "No any changes to update.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        hdr.TPSH_SETTLE_NO = formData["SettlementNo"].ToString().Trim();
                        Int32 seq = Convert.ToInt32(formData["SettlementSeq"].ToString());

                        bool perm = CHNLSVC.Security.Is_OptionPerimitted(company, userId, 1003);
                        Int32 appPer = Convert.ToInt32(Session["Log_Autho"].ToString());

                        if (appPer == 1)
                        {
                            if (request.TPSH_APP1 == 1)
                            {
                                return Json(new { success = false, login = true, msg = "Cannot update already approved settlement by  level 1", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else if (appPer == 2)
                        {
                            if (request.TPSH_APP2 == 1)
                            {
                                return Json(new { success = false, login = true, msg = "Cannot update already approved settlement by  level 2", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else if (appPer == 3)
                        {
                            if (request.TPSH_APP3 == 1)
                            {
                                if (!perm)
                                {
                                    return Json(new { success = true, login = true, msg = "You don't have permission to update pettycash request settlements.(Requsted permission code 1003)", type = "Error" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        eff = CHNLSVC.Sales.updateSetlementDetails(hdr, setDets, newSetDets, seq, sessionid, out error);
                        if (eff <= 0)
                        {
                            return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { success = true, login = true, msg = "Successfully updated settlement no :" + error + ". ", type = "Success" }, JsonRequestBehavior.AllowGet);

                    }
                    eff = CHNLSVC.Sales.saveSetlementDetails(hdr, setDets, _ptyAuto, sessionid, out error);
                    if (eff < 0)
                    {
                        return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        eff = CHNLSVC.Sales.saveSetlementDetailsAllocate(error, userId, DateTime.Now.Date, sessionid, out error1);
                    }
                    return Json(new { success = true, login = true, msg = "Successfully saved settlement no :" + error + ". ", type = "Success" }, JsonRequestBehavior.AllowGet);

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
        public JsonResult saveSettlmentDetRefund(FormCollection formData)
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
                    DateTime SettlementDate;
                    try
                    {
                        SettlementDate = Convert.ToDateTime(formData["SettlementDate"].ToString());
                    }
                    catch (Exception)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid settlement date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    string ManRef = formData["ManRef"].ToString();
                    DateTime payDt;
                    try
                    {
                        payDt = Convert.ToDateTime(formData["payDt"].ToString());
                    }
                    catch (Exception e)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid payment date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    string ProfitCenter = formData["ProfitCenter"].ToString();
                    string Remarks = formData["Remarks"].ToString();
                    List<TRN_PETTYCASH_SETTLE_DTL> setDetsCopy = new List<TRN_PETTYCASH_SETTLE_DTL>();
                    List<TRN_PETTYCASH_SETTLE_DTL> setDetsDup = new List<TRN_PETTYCASH_SETTLE_DTL>();
                    List<TRN_PETTYCASH_SETTLE_DTL> setDets = new List<TRN_PETTYCASH_SETTLE_DTL>();
                    if (Session["ToRefundSettlement"] != null)
                    {
                        setDetsCopy = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["ToRefundSettlement"];
                        //setDetsDup = setDetsCopy.Select()
                        setDetsCopy.RemoveAll(item => item == null);
                        setDets = setDetsCopy.Select(s => { s.TPSD_RECEIPT_NO = formData["receiptNo"].ToString(); return s; })
                                  .ToList();
                    }
                    TRN_PETTYCASH_SETTLE_HDR hdr = new TRN_PETTYCASH_SETTLE_HDR();
                    
                    string error = "";
                    Int32 eff = 0;
                    //if (!string.IsNullOrEmpty(formData["SettlementNo"].ToString()))
                    //{
                        List<TRN_PETTYCASH_SETTLE_DTL> newSetDets = new List<TRN_PETTYCASH_SETTLE_DTL>();
                        if (Session["ToRefundSettlement"] != null)
                        {
                            newSetDets = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["ToRefundSettlement"];
                        }
                        Int32 inccount = setDets.Where(X => X.TPSD_ACT == 1).Count();
                        if (newSetDets.Count == 0 && inccount == 0)
                        {
                            return Json(new { success = false, login = true, msg = "No any changes to update.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        //hdr.TPSH_SETTLE_NO = formData["SettlementNo"].ToString().Trim();
                        Int32 seq = 0;
                        try
                        { 
                            seq = Convert.ToInt32(formData["SettlementSeq"].ToString());
                        }
                        catch { }
                        //eff = CHNLSVC.Sales.updateSetlementDetailsRefund(hdr, setDets, newSetDets, seq, sessionid, out error);
                        eff = CHNLSVC.Sales.saveSetlementDetailsRefund(hdr, setDets, null, sessionid, out error);
                        if (eff <= 0)
                        {
                            return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { success = true, login = true, msg = "Successfully saved refund amount" }, JsonRequestBehavior.AllowGet);
                        
                        //if (eff < 0)
                        //{
                        //    return Json(new { success = true, login = true, msg = "Successfully saved refund amount" }, JsonRequestBehavior.AllowGet);
                        //}
                        //else
                        //{
                        //    return Json(new { success = true, login = true, msg = "Cannot save refund amount" }, JsonRequestBehavior.AllowGet);
                        //}
                    //}
                    //return Json(new { success = true, login = true, msg = "Successfully saved refund amount" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getMaxApproveLevel()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    TRN_MOD_MAX_APPLVL data = new TRN_MOD_MAX_APPLVL();
                    if (Session["MAXAPPLVL"] != null)
                    {
                        data = (TRN_MOD_MAX_APPLVL)Session["MAXAPPLVL"];
                    }
                    Int32 log_autho = 0;
                    if (Session["Log_Autho"] != null && Session["Log_Autho"].ToString() != "")
                    {
                        log_autho = Convert.ToInt32(Session["Log_Autho"].ToString());
                    }
                    if (data.TMAL_MODULE != null)
                    {
                        return Json(new { success = true, login = true, data = data, log_autho = log_autho }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Please module max approval leval.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult approveRequest(string reqno, string appLvl, DateTime paydate)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    Int32 sessionid = Convert.ToInt32(Session["SessionID"]);
                    string error = "";
                    TRN_PETTYCASH_SETTLE_HDR request = CHNLSVC.Sales.loadSettlementHdr(company, userDefPro, reqno, out error);

                    if (error == "")
                    {
                        if (request.TPSH_SETTLE_NO != null)
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
                                    if (request.TPSH_APP1 == 1)
                                    {
                                        return Json(new { success = false, login = true, msg = "Already approve this by Level 1.", type = "Info" }, JsonRequestBehavior.AllowGet);

                                    }
                                    else
                                    {
                                        request.TPSH_APP1 = 1;
                                        request.TPSH_APP1_BY = userId;
                                        request.TPSH_APP1_DT = DateTime.Now;
                                        request.TPSH_MOD_SES_ID = sessionid;
                                    }
                                }
                                else if (appl == 2)
                                {
                                    if (request.TPSH_APP2 == 1)
                                    {
                                        return Json(new { success = false, login = true, msg = "Already approve this by Level 2.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        if (request.TPSH_APP1 == 0)
                                        {
                                            return Json(new { success = false, login = true, msg = "Still not approve this by Level 1.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                        request.TPSH_APP2 = 1;
                                        request.TPSH_APP2_BY = userId;
                                        request.TPSH_APP2_DT = DateTime.Now;
                                        request.TPSH_MOD_SES_ID = sessionid;
                                    }
                                }
                                else if (appl == 3)
                                {
                                    if (request.TPSH_APP3 == 1)
                                    {
                                        return Json(new { success = false, login = true, msg = "Already approve this by Level 3.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        if (request.TPSH_APP2 == 0)
                                        {
                                            return Json(new { success = false, login = true, msg = "Still not approve this by Level 2.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                        }
                                        request.TPSH_APP3 = 1;
                                        request.TPSH_APP3_BY = userId;
                                        request.TPSH_APP3_DT = DateTime.Now;
                                        request.TPSH_MOD_SES_ID = sessionid;
                                        request.TPSH_PAY_DT = paydate;
                                    }
                                }
                                request.TPSH_MOD_BY = userId;
                                request.TPSH_MOD_DT = DateTime.Now;
                                TRN_MOD_MAX_APPLVL applv = CHNLSVC.General.getMaxAppLvlPermission("PTYCSHREQSETTLE", company);

                                //if (appl == applv.TMAL_MAX_APPLVL)
                                //{
                                //    request.TPSH_STUS = "A";
                                //}
                                if (request.TPSH_APP3 == 1 && appl == 3)
                                {
                                    request.TPSH_STUS = "A";
                                }
                                Int32 eff = CHNLSVC.Sales.updateSetlementApproveStus(request, appl, out error);
                                if (eff == 1)
                                {
                                    return Json(new { success = true, login = true, type = "Success", msg = "Successfully approved settlement " + reqno + " by level " + appLvl }, JsonRequestBehavior.AllowGet);
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
                            return Json(new { success = false, login = true, msg = "Invalid settlement number", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult rejectRequest(string reqno)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (reqno == null)
                    {
                        return Json(new { success = false, login = true, msg = "Invalid settlement number", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    string error = "";
                    TRN_PETTYCASH_SETTLE_HDR request = CHNLSVC.Sales.loadSettlementHdr(company, userDefPro, reqno, out error);
                    if (error == "")
                    {
                        if (request.TPSH_SETTLE_NO != null)
                        {
                            reqno = reqno.Trim();
                            string sessionid = Session["SessionID"].ToString();
                            //if (Session["Log_Autho"] != null && Session["Log_Autho"].ToString() != "")
                            //{
                            //Int32 log_autho = Convert.ToInt32(Session["Log_Autho"].ToString());
                            //if (log_autho == 1)
                            //{
                            //    if (request.TPSH_APP2 == 1)
                            //    {
                            //        return Json(new { success = false, login = true, msg = "Unable to reject settlement.Already approve this by Level 2.", type = "Info" }, JsonRequestBehavior.AllowGet);

                            //    }
                            //}
                            //else if (log_autho == 2)
                            //{
                            //    if (request.TPSH_APP3 == 1)
                            //    {
                            //        return Json(new { success = false, login = true, msg = "Unable to reject settlement.Already approve this by Level 3.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            //    }
                            //}
                            //else
                            //{
                            //    return Json(new { success = false, login = true, msg = "You don't have permission to reject settlement.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            //}
                            bool perm = CHNLSVC.Security.Is_OptionPerimitted(company, userId, 1002);
                            if (perm)
                            {
                                List<TRN_PETTYCASH_SETTLE_DTL> setDets = new List<TRN_PETTYCASH_SETTLE_DTL>();
                                if (Session["SetDet"] != null)
                                {
                                    setDets = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["SetDet"];
                                }
                                if (setDets.Count == 0)
                                {
                                    return Json(new { success = false, login = true, msg = "Invalid settlement request details.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                Int32 res = CHNLSVC.Sales.rejectSettlementRequest(reqno, userId, DateTime.Now, sessionid, setDets, out error);


                                if (res > 0)
                                {
                                    return Json(new { success = true, login = true, msg = "Successfully rejected settlement :" + request.TPSH_SETTLE_NO, type = "Success" }, JsonRequestBehavior.AllowGet);
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
                                return Json(new { success = false, login = true, msg = "You don't have permission to reject pettycash request.(Requsted permission code 1002)", type = "Error" }, JsonRequestBehavior.AllowGet);
                                //return Json(new { success = false, login = true, msg = "Invalid settlement request details.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    TRN_PETTYCASH_SETTLE_HDR request = CHNLSVC.Sales.load_PtyC_Setl_RequestDtl_Validate(Convert.ToInt32(seq), company, userDefPro, out error);
                    SystemUser _userLvl = CHNLSVC.Security.GetUserByUserID(userId);
                    if (error == "")
                    {
                        if (request.TPSH_SETTLE_NO != null)
                        {
                            //if (request.TPSH_APP3 == 1)
                            //{
                            //    //if (request.TPRH_IS_PRINT != 1)
                            //    //{
                            //    //    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                            //    //}
                            //    //else
                            //    //{
                            //    //    return Json(new { success = false, login = true, msg = "This Request Already Printed and Approved by Accounts.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            //    //}
                            //    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                            //}
                            //else
                            //{
                            //    return Json(new { success = false, login = true, msg = "Need level 3 approve to print.", type = "Info" }, JsonRequestBehavior.AllowGet);
                            //}
                            if (request.TPSH_APP2 == 1)
                            {
                                //if (request.TPRH_APP_3 == 1)
                                if ((request.TPSH_APP2 == 1 && (_userLvl.Se_Log_Autho == 2 || _userLvl.Se_Log_Autho == 1) && request.TPSH_APP3 != 1) || (_userLvl.Se_Log_Autho == 0 && request.TPSH_APP2 == 1 && request.TPSH_APP3 != 1) || _userLvl.Se_Log_Autho == 3)
                                {
                                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "This Request Already Printed and Approved by Accounts.", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
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
            SystemUser _userLvl = CHNLSVC.Security.GetUserByUserID(userId);
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string error = "";
                Int32 seqNo = Convert.ToInt32(seq);
                TRN_PETTYCASH_SETTLE_HDR request = CHNLSVC.Sales.load_PtyC_Setl_RequestDtl_Validate(seqNo, company, userDefPro, out error);//load_PtyC_Setl_RequestDetails
                if (error == "")
                {
                    if (request.TPSH_SETTLE_NO != null)
                    {
                        string reportName = "";
                        string fileName = "";
                        string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"].ToString();
                        string report = "";
                        ReportDocument rd = new ReportDocument();
                        DataTable rptData = new DataTable("Settlement");
                        rptData = CHNLSVC.Sales.PettyCash_SettlementDetls(seqNo, company, out error);
                        DataTable comData = new DataTable("comdata");
                        comData = CHNLSVC.Sales.getCompanyDetailsBycd(company);

                        //try
                        //{
                        //    Settltment_Summary(seqNo, company);
                        //}
                        //catch(Exception ex)
                        //{
                        //    return Json(new { success = false, login = true, msg = ex.Message, type = "Info" }, JsonRequestBehavior.AllowGet);
                        //}
                        reportName = "rpt_PettyCashSettlement.rpt";
                        fileName = "Petty Cash Settlement.pdf";
                        //report = ReportPath + "\\" + reportName;
                        //rd.Load(report);
                        rd.Load(Path.Combine(Server.MapPath("~/Reports"), "rpt_PettyCashSettlement.rpt"));
                        rd.Database.Tables["PetyCashSettlement"].SetDataSource(rptData);
                        rd.Database.Tables["COMPANY"].SetDataSource(comData);

                        Response.Buffer = false;
                        Response.ClearContent();
                        Response.ClearHeaders();
                        try
                        {
                            this.Response.Clear();
                            this.Response.ContentType = "application/pdf";
                            Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);

                            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            stream.Seek(0, SeekOrigin.Begin);
                            rd.Close();
                            rd.Dispose();
                            return File(stream, "application/pdf"); 

                            //return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                        }
                        catch (Exception ex)
                        {
                            CHNLSVC.General.SaveReportErrorLog("rpt_PettyCashSettlement-PDF", "rpt_PettyCashSettlement", ex.Message, Session["UserID"].ToString());
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

        public ActionResult Settltment_Summary(string seq)
        {
            string reportNameSumm = "";
            string fileNameSumm = "";
            string report = "";
            string error = "";
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            Int32 seqNo = Convert.ToInt32(seq);
            TRN_PETTYCASH_SETTLE_HDR request = CHNLSVC.Sales.load_PtyC_Setl_RequestDtl_Validate(seqNo, company, userDefPro, out error);
            string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"].ToString();
            ReportDocument rdSumm = new ReportDocument();
            DataTable rptData_summ = new DataTable("SettlementSumm");
            rptData_summ = CHNLSVC.Sales.PettyCash_SettlementDetls_Summ(seqNo, company, out error);
            DataTable comData = new DataTable("comdata");
            comData = CHNLSVC.Sales.getCompanyDetailsBycd(company);

            reportNameSumm = "rpt_PettyCashSettlement_summary.rpt";
            fileNameSumm = "Petty Cash Settlement Summary.pdf";
            //report = ReportPath + "\\" + reportNameSumm;
            //rdSumm.Load(report);
            rdSumm.Load(Path.Combine(Server.MapPath("~/Reports"), "rpt_PettyCashSettlement_summary.rpt"));
            rdSumm.Database.Tables["PetyCashSettlement"].SetDataSource(rptData_summ);
            rdSumm.Database.Tables["COMPANY"].SetDataSource(comData);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                this.Response.Clear();
                this.Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "inline; filename=" + fileNameSumm);

                Stream stream = rdSumm.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rdSumm.Close();
                rdSumm.Dispose();
                return File(stream, "application/pdf"); 

                //return File(rdSumm.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
            }
            catch (Exception ex)
            {
                CHNLSVC.General.SaveReportErrorLog("rpt_PettyCashSettlement_summary-PDF", "rpt_PettyCashSettlement_summary", ex.Message, Session["UserID"].ToString());
                throw;
            }
        }
        //public ActionResult SaveData(HttpPostedFileBase file)
        //{
        //    return Redirect("~/");

        //}
        public JsonResult removeSettlement(string itmline, string jobnum, string seq, string setlineno)
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
                        Int32 seqNo = Convert.ToInt32(seq);
                        string error = "";
                        TRN_PETTYCASH_SETTLE_HDR request = CHNLSVC.Sales.load_PtyC_Setl_RequestDetails(seqNo, company, userDefPro, out error);
                        if (error == "")
                        {
                            string pemissionMsg = "";
                            bool editable = true;
                            bool perm = CHNLSVC.Security.Is_OptionPerimitted(company, userId, 1003);
                            Int32 appPer = Convert.ToInt32(Session["Log_Autho"].ToString());
                            if (!perm)
                            {
                                if (request.TPSH_APP3 == 1)
                                {
                                    editable = false;
                                    pemissionMsg = "You don't have permission to update pettycash settlement job details.(Requsted permission code 1003)";
                                }
                            }
                            else
                            {
                                if (appPer == 1 || appPer == 2)
                                {
                                    editable = true;
                                    pemissionMsg = "Unable to edit settlement details for level " + appPer + " users";
                                }
                            }
                            if (request.TPSH_SETTLE_NO == null)
                            {
                                return Json(new { success = false, login = true, msg = "Invalid settlement number.Please reload and check.", type = "Info" }, JsonRequestBehavior.AllowGet);

                            }
                            if (appPer == 1)
                            {
                                if (request.TPSH_APP1 == 1)
                                {
                                    return Json(new { success = false, login = true, msg = "Cannot remove jobs request already approved settlement by  level 1", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else if (appPer == 2)
                            {
                                if (request.TPSH_APP2 == 1)
                                {
                                    return Json(new { success = false, login = true, msg = "Cannot remove jobs request already approved settlement by  level 2", type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else if (appPer == 3)
                            {
                                if (request.TPSH_APP3 == 1)
                                {
                                    if (!perm)
                                    {
                                        return Json(new { success = true, login = true, msg = "You don't have permission to remove pettycash request settlements.(Requsted permission code 1003)", type = "Error" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }

                            List<TRN_PETTYCASH_SETTLE_DTL> SetDet = new List<TRN_PETTYCASH_SETTLE_DTL>();
                            if (Session["SetDet"] != null)
                            {
                                SetDet = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["SetDet"];
                            }
                            List<TRN_PETTYCASH_SETTLE_DTL> newSetDets = new List<TRN_PETTYCASH_SETTLE_DTL>();
                            if (Session["newSetDets"] != null)
                            {
                                newSetDets = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["newSetDets"];
                            }
                            if (newSetDets.Count > 0)
                            {
                                try
                                {
                                    var itemToRemove = newSetDets.Single(r => r.TPSD_LINE_NO == Convert.ToInt32(itmline) && r.TPSD_REQ_NO == jobnum && r.TPSD_SETLE_LINO_NO == Convert.ToInt32(setlineno));
                                    newSetDets.Remove(itemToRemove);
                                }
                                catch (Exception ex) { }
                            }
                            if (SetDet.Count > 0)
                            {
                                try
                                {
                                    foreach (TRN_PETTYCASH_SETTLE_DTL rq in SetDet)
                                    {
                                        if (rq.TPSD_REQ_NO == jobnum.Trim() && rq.TPSD_LINE_NO == Convert.ToInt32(itmline) && rq.TPSD_SETTLE_AMT > 0 && rq.TPSD_SETLE_LINO_NO == Convert.ToInt32(setlineno))
                                        {
                                            rq.TPSD_ACT = 0;
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    return Json(new { success = false, login = true, msg = e.ToString(), type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            Session["newSetDets"] = newSetDets;
                            Session["SetDet"] = SetDet;
                            decimal TotSetAmt = SetDet.Where(y => y.TPSD_ACT == 1).Sum(x => x.TPSD_SETTLE_AMT);
                            decimal TotReqAmt = SetDet.Where(y => y.TPSD_ACT == 1).Sum(x => x.TPSD_REQ_AMT);

                            return Json(new { success = true, login = true, data = SetDet, TotSetAmt = TotSetAmt, TotReqAmt = TotReqAmt, editable = editable, pemissionMsg = pemissionMsg }, JsonRequestBehavior.AllowGet);


                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        List<TRN_PETTYCASH_SETTLE_DTL> SetDet = new List<TRN_PETTYCASH_SETTLE_DTL>();
                        if (Session["SetDet"] != null)
                        {
                            SetDet = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["SetDet"];
                        }
                        List<TRN_PETTYCASH_SETTLE_DTL> newSetDets = new List<TRN_PETTYCASH_SETTLE_DTL>();
                        if (Session["newSetDets"] != null)
                        {
                            newSetDets = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["newSetDets"];
                        }
                        if (SetDet.Count > 0)
                        {
                            try
                            {
                                var itemToRemove = SetDet.Where(y => y.TPSD_SETTLE_AMT > 0).First(r => r.TPSD_LINE_NO == Convert.ToInt32(itmline) && r.TPSD_REQ_NO == jobnum && r.TPSD_SETLE_LINO_NO == Convert.ToInt32(setlineno));
                                SetDet.Remove(itemToRemove);
                                Int32 i = 1;
                                foreach (TRN_PETTYCASH_SETTLE_DTL rq in SetDet)
                                {
                                    if (rq.TPSD_SETTLE_AMT > 0)
                                    {
                                        rq.TPSD_LINE_NO = i;
                                        i++;
                                    }
                                }

                            }
                            catch (Exception e)
                            {
                                return Json(new { success = false, login = true, msg = e.ToString(), type = "Info" }, JsonRequestBehavior.AllowGet);
                            }

                            if (newSetDets.Count > 0)
                            {
                                try
                                {
                                    var itemToRemove = newSetDets.Single(r => r.TPSD_LINE_NO == Convert.ToInt32(itmline) && r.TPSD_REQ_NO == jobnum && r.TPSD_SETLE_LINO_NO == Convert.ToInt32(setlineno));
                                    newSetDets.Remove(itemToRemove);
                                }
                                catch (Exception ex) { }
                            }
                            if (SetDet.Count > 0)
                            {
                                try
                                {
                                    foreach (TRN_PETTYCASH_SETTLE_DTL rq in SetDet)
                                    {
                                        if (rq.TPSD_REQ_NO == jobnum.Trim() && rq.TPSD_LINE_NO == Convert.ToInt32(itmline) && rq.TPSD_SETTLE_AMT > 0)
                                        {
                                            rq.TPSD_ACT = 0;
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    return Json(new { success = false, login = true, msg = e.ToString(), type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            Session["newSetDets"] = newSetDets;
                            Session["SetDet"] = SetDet;
                            decimal TotSetAmt = SetDet.Where(x=>x.TPSD_ACT==1).Sum(x => x.TPSD_SETTLE_AMT);
                            decimal TotReqAmt = SetDet.Where(x => x.TPSD_ACT == 1).Sum(x => x.TPSD_REQ_AMT);

                            return Json(new { success = true, login = true, data = SetDet, TotSetAmt = TotSetAmt, TotReqAmt = TotReqAmt }, JsonRequestBehavior.AllowGet);

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

        public JsonResult ShowRefundAmounts()
        {
            List<TRN_PETTYCASH_SETTLE_DTL> RefundSettlementColl = new List<TRN_PETTYCASH_SETTLE_DTL>();
            List<TRN_PETTYCASH_SETTLE_DTL> RefundSettlementDup = new List<TRN_PETTYCASH_SETTLE_DTL>();
            if (Session["RefundSettlementColl"] != null)
            {
                RefundSettlementColl = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["RefundSettlementColl"];
                if (Session["RefundSettlement"] != null)
                {
                    RefundSettlementDup = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["RefundSettlement"];
                    RefundSettlementDup.AddRange(RefundSettlementColl);
                    Session["RefundSettlement"] = RefundSettlementDup;
                }
                else
                {
                    Session["RefundSettlement"] = RefundSettlementColl;
                }
                
            }

            List<TRN_PETTYCASH_SETTLE_DTL> RefundSettlement = new List<TRN_PETTYCASH_SETTLE_DTL>();
            if (Session["RefundSettlementColl"] != null)
            {
                RefundSettlement = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["RefundSettlementColl"];
            }
            return Json(new { success = true, login = true, data = RefundSettlement }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadAllRefundableJobData(string jobno)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            List<TRN_PETTYCASH_SETTLE_DTL> RefundSettlement = new List<TRN_PETTYCASH_SETTLE_DTL>();            
            RefundSettlement = CHNLSVC.Sales.LoadAllRefundableJobData(jobno);

            List<TRN_PETTYCASH_SETTLE_DTL> RefundedSettlementJob = new List<TRN_PETTYCASH_SETTLE_DTL>();
            RefundedSettlementJob = CHNLSVC.Sales.CheckJobAlreadyHasRefunds(jobno);

            List<TRN_PETTYCASH_SETTLE_DTL> settleGroup = (from sg in RefundSettlement
                                                          where sg.TPSD_ELEMENT_CD != "REFUND"
                                                          group sg by new
                                                          {
                                                              TPSD_SETTLE_NO = sg.TPSD_SETTLE_NO
                                                          } into sgd
                                                          select new TRN_PETTYCASH_SETTLE_DTL
                                                          {
                                                              TPSD_SETTLE_NO = sgd.Select(x => x.TPSD_SETTLE_NO).FirstOrDefault(),
                                                              TPSD_REQ_NO = sgd.Select(x => x.TPSD_REQ_NO).FirstOrDefault(),
                                                              TPSD_JOB_NO = sgd.Select(x => x.TPSD_JOB_NO).FirstOrDefault(),//sgd.Key.TPSD_JOB_NO,
                                                              TPSD_ELEMENT_CD = "REFUND",
                                                              TPSD_ELEMENT_DESC = "REFUND",
                                                              TPSD_REQ_AMT = 0, //sgd.Sum(x => x.TPSD_REQ_AMT),
                                                              TPSD_SETTLE_AMT = (sgd.Sum(x => x.TPSD_REQ_AMT) - sgd.Sum(x => x.TPSD_SETTLE_AMT)),
                                                              TPSD_CRE_BY = sgd.Select(x => x.TPSD_CRE_BY).FirstOrDefault(),
                                                              TPSD_LINE_NO = sgd.Select(x => x.TPSD_LINE_NO).LastOrDefault() + 1,
                                                              TPSD_CRE_DT = sgd.Select(x => x.TPSD_CRE_DT).FirstOrDefault(),
                                                              TPSD_REMARKS = "", //sgd.Select(x => x.TPSD_REMARKS).FirstOrDefault(),
                                                              TPSD_VEC_TELE = sgd.Select(x => x.TPSD_VEC_TELE).FirstOrDefault(),
                                                              TPSD_ACT = 1 //int.Parse(sgd.Select(x => x.TPSD_REQ_NO).FirstOrDefault())
                                                          }).ToList();

            Session["RefundSettlementColl"] = settleGroup;

            decimal reqAmt = RefundSettlement.AsEnumerable().Sum(o => o.TPSD_REQ_AMT);
            decimal setAmt = RefundSettlement.AsEnumerable().Sum(o => Math.Abs(o.TPSD_SETTLE_AMT));

            bool pms = false;

            if (CHNLSVC.Security.Is_OptionPerimitted(company, userId.ToUpper(), 1006))
            {
                pms = true;
            }
            else
            {
                pms = false;
            }

            int hasRefund = 0;
            decimal setAmtRefunded = RefundedSettlementJob.AsEnumerable().Sum(o => o.TPSD_SETTLE_AMT);
            if (RefundedSettlementJob.Count > 0) { hasRefund = 1; }

            

            return Json(new { success = true, login = true, data = RefundSettlement, reqAmt = reqAmt, setAmt = setAmt, permission = pms, hasRefund = hasRefund }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddRemoveRefunds(string settleNo, bool remove)
        {
            List<TRN_PETTYCASH_SETTLE_DTL> currentRefund = new List<TRN_PETTYCASH_SETTLE_DTL>();
            if (Session["RefundSettlement"] != null)
            {
                currentRefund = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["RefundSettlement"];
            }

            if (remove == false)
            {
                List<TRN_PETTYCASH_SETTLE_DTL> toRefund = new List<TRN_PETTYCASH_SETTLE_DTL>();
                if (Session["ToRefundSettlement"] != null)
                {
                    toRefund = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["ToRefundSettlement"];
                }
                toRefund.Add((from rs in currentRefund
                              where rs.TPSD_SETTLE_NO == settleNo
                              select new TRN_PETTYCASH_SETTLE_DTL
                              {
                                  TPSD_SETTLE_NO = rs.TPSD_SETTLE_NO,
                                  TPSD_REQ_NO = rs.TPSD_REQ_NO,
                                  TPSD_JOB_NO = rs.TPSD_JOB_NO,
                                  TPSD_ELEMENT_CD = rs.TPSD_ELEMENT_CD,
                                  TPSD_ELEMENT_DESC = rs.TPSD_ELEMENT_DESC,
                                  TPSD_REQ_AMT = rs.TPSD_REQ_AMT,
                                  TPSD_SETTLE_AMT = -1 * rs.TPSD_SETTLE_AMT,
                                  TPSD_CRE_BY = rs.TPSD_CRE_BY,
                                  TPSD_LINE_NO = rs.TPSD_LINE_NO,
                                  TPSD_CRE_DT = rs.TPSD_CRE_DT,
                                  TPSD_REMARKS = rs.TPSD_REMARKS,
                                  TPSD_VEC_TELE = rs.TPSD_VEC_TELE,
                                  TPSD_ACT = rs.TPSD_ACT
                              }
                              ).FirstOrDefault());
                Session["ToRefundSettlement"] = toRefund;
            }
            else
            {
                List<TRN_PETTYCASH_SETTLE_DTL> toRefund = new List<TRN_PETTYCASH_SETTLE_DTL>();
                if (Session["ToRefundSettlement"] != null)
                {
                    toRefund = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["ToRefundSettlement"];
                }
                toRefund.Add((from rs in currentRefund
                              where rs.TPSD_SETTLE_NO != settleNo
                              select rs).FirstOrDefault());
                Session["ToRefundSettlement"] = toRefund;
            }
            

            return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CollectAllRefundData(bool remove)
        {
            decimal totalamnt = 0;

            List<TRN_PETTYCASH_SETTLE_DTL> currentRefund = new List<TRN_PETTYCASH_SETTLE_DTL>();
            if (Session["RefundSettlement"] != null)
            {
                currentRefund = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["RefundSettlement"];
            }

            if (remove == false)
            {
                List<TRN_PETTYCASH_SETTLE_DTL> toRefund = new List<TRN_PETTYCASH_SETTLE_DTL>();
                if (Session["ToRefundSettlement"] != null)
                {
                    toRefund = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["ToRefundSettlement"];
                }
                toRefund.Add((from rs in currentRefund
                              select new TRN_PETTYCASH_SETTLE_DTL
                              {
                                  TPSD_SETTLE_NO = rs.TPSD_SETTLE_NO,
                                  TPSD_REQ_NO = rs.TPSD_REQ_NO,
                                  TPSD_JOB_NO = rs.TPSD_JOB_NO,
                                  TPSD_ELEMENT_CD = rs.TPSD_ELEMENT_CD,
                                  TPSD_ELEMENT_DESC = rs.TPSD_ELEMENT_DESC,
                                  TPSD_REQ_AMT = rs.TPSD_REQ_AMT,
                                  TPSD_SETTLE_AMT = -1 * rs.TPSD_SETTLE_AMT,
                                  TPSD_CRE_BY = rs.TPSD_CRE_BY,
                                  TPSD_LINE_NO = rs.TPSD_LINE_NO,
                                  TPSD_CRE_DT = rs.TPSD_CRE_DT,
                                  TPSD_REMARKS = rs.TPSD_REMARKS,
                                  TPSD_VEC_TELE = rs.TPSD_VEC_TELE,
                                  TPSD_ACT = rs.TPSD_ACT
                              }
                              ).FirstOrDefault());
                Session["ToRefundSettlement"] = toRefund;

                totalamnt = currentRefund.AsEnumerable().Sum(o => o.TPSD_SETTLE_AMT);
            }
            else
            {
                List<TRN_PETTYCASH_SETTLE_DTL> toRefund = new List<TRN_PETTYCASH_SETTLE_DTL>();
                if (Session["ToRefundSettlement"] != null)
                {
                    toRefund = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["ToRefundSettlement"];
                }
                toRefund.Add((from rs in currentRefund
                              select rs).FirstOrDefault());
                Session["ToRefundSettlement"] = toRefund;
            }


            return Json(new { success = true, login = true, data = totalamnt }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult validateHasJobWiseExists(string jobno)
        {
            bool status = false;

            List<TRN_PETTYCASH_SETTLE_DTL> toRefund = new List<TRN_PETTYCASH_SETTLE_DTL>();
            if (Session["RefundSettlement"] != null)
            {
                toRefund = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["RefundSettlement"];
            }

            foreach (TRN_PETTYCASH_SETTLE_DTL reff in toRefund)
            {
                if (reff.TPSD_JOB_NO == jobno)
                {
                    status = true;
                    break;
                }
            }

            bool isAdded = false;
            bool hasApprove = false;
            bool hasRejected = false;

            List<TRN_PETTYCASH_SETTLE_DTL> RefundedSettlementJob = new List<TRN_PETTYCASH_SETTLE_DTL>();
            RefundedSettlementJob = CHNLSVC.Sales.CheckJobAlreadyHasRefunds(jobno);
            if (RefundedSettlementJob.Count > 0) isAdded = true; else isAdded = false;

            List<TRN_PETTYCASH_SETTLE_DTL> hasRejectedData = new List<TRN_PETTYCASH_SETTLE_DTL>();
            hasRejectedData = CHNLSVC.Sales.CheckSettlementRejected(jobno);
            if (hasRejectedData.Count > 0) hasRejected = true; else hasRejected = false;

            List<TRN_PETTYCASH_SETTLE_DTL> hasApproveData = new List<TRN_PETTYCASH_SETTLE_DTL>();
            hasApproveData = CHNLSVC.Sales.CheckSettlementApproved(jobno);
            if (hasApproveData.Count > 0) hasApprove = true; else hasApprove = false;


            return Json(new { success = true, login = true, data = status, isAdded = isAdded, hasApprove = hasApprove, hasRejected = hasRejected }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveRecieptEntryRefund(string jobno, string depBank, string depBranch, string debtor, string manRef)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string err = "";
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                RecieptHeader _ReceiptHeader = new RecieptHeader();

                decimal total_settle_amnt = 0;

                _ReceiptHeader.Sar_profit_center_cd = userDefPro;
                _ReceiptHeader.Sar_com_cd = company;
                _ReceiptHeader.Sar_create_by = userId;
                _ReceiptHeader.Sar_create_when = DateTime.Now;
                _ReceiptHeader.Sar_mod_by = userId;
                _ReceiptHeader.Sar_mod_when = DateTime.Now;
                _ReceiptHeader.Sar_act = true;
                List<RecieptItem> _recieptItem = new List<RecieptItem>();
                List<RecieptItemTBS> _recieptItemtbs = (List<RecieptItemTBS>)Session["RecieptItemList"];
                if (_recieptItemtbs == null)
                {
                    _recieptItemtbs = new List<RecieptItemTBS>();
                }
                if (_recieptItemtbs.Count > 0)
                {
                    _recieptItem = new List<RecieptItem>();
                    foreach (RecieptItemTBS tbs in _recieptItemtbs)
                    {
                        RecieptItem recItm = new RecieptItem();
                        recItm.Sard_anal_1 = tbs.Sird_anal_1;
                        recItm.Sard_anal_2 = tbs.Sird_anal_2;
                        recItm.Sard_anal_3 = tbs.Sird_anal_3;
                        recItm.Sard_anal_4 = tbs.Sird_anal_4;
                        recItm.Sard_anal_5 = tbs.Sird_anal_5;
                        recItm.Sard_cc_batch = tbs.Sird_cc_batch;
                        recItm.Sard_cc_expiry_dt = tbs.Sird_cc_expiry_dt;
                        recItm.Sard_cc_is_promo = tbs.Sird_cc_is_promo;
                        recItm.Sard_cc_period = tbs.Sird_cc_period;
                        recItm.Sard_cc_tp = tbs.Sird_cc_tp;
                        recItm.Sard_chq_bank_cd = tbs.Sird_chq_bank_cd;
                        recItm.Sard_chq_branch = tbs.Sird_chq_branch;
                        recItm.Sard_chq_dt = tbs.Sird_chq_dt;
                        recItm.Sard_credit_card_bank = tbs.Sird_credit_card_bank;
                        recItm.Sard_deposit_bank_cd = tbs.Sird_deposit_bank_cd;
                        recItm.Sard_deposit_branch = tbs.Sird_deposit_branch;
                        recItm.Sard_gv_issue_dt = tbs.Sird_gv_issue_dt;
                        recItm.Sard_gv_issue_loc = tbs.Sird_gv_issue_loc;
                        recItm.Sard_inv_no = tbs.Sird_inv_no;
                        recItm.Sard_line_no = tbs.Sird_line_no;
                        recItm.Sard_pay_tp = tbs.Sird_pay_tp;
                        recItm.Sard_receipt_no = tbs.Sird_receipt_no;
                        recItm.Sard_ref_no = tbs.Sird_ref_no;
                        recItm.Sard_rmk = tbs.Sird_rmk;
                        recItm.Sard_seq_no = tbs.Sird_seq_no;
                        recItm.Sard_settle_amt = tbs.Sird_settle_amt;
                        recItm.Sard_sim_ser = tbs.Sird_sim_ser;
                        recItm.Newpayment = tbs.Newpayment;
                        _recieptItem.Add(recItm);

                        total_settle_amnt = total_settle_amnt + tbs.Sird_settle_amt;
                    }

                    _ReceiptHeader.Sar_seq_no = -1; //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, BaseCls.GlbUserComCode);
                    // _ReceiptHeader.Sar_receipt_no = txtRecNo.Text.Trim();
                    MasterReceiptDivision _RecDiv = new MasterReceiptDivision();
                    _RecDiv = CHNLSVC.Sales.GetDefRecDivision(company, userDefPro);
                    if (_RecDiv.Msrd_cd != null)
                    {
                        _ReceiptHeader.Sar_prefix = _RecDiv.Msrd_cd;
                    }
                    else
                    {
                        _ReceiptHeader.Sar_prefix = "";
                    }

                    //_ReceiptHeader.Sar_prefix = txtDivision.Text.Trim();
                    _ReceiptHeader.Sar_manual_ref_no = manRef;
                    _ReceiptHeader.Sar_direct = true; // true to false
                    _ReceiptHeader.Sar_acc_no = "";
                    _ReceiptHeader.Sar_is_oth_shop = false;
                    _ReceiptHeader.Sar_oth_sr = "";
                    _ReceiptHeader.Sar_tel_no = "";
                    _ReceiptHeader.Sar_mob_no = "";
                    _ReceiptHeader.Sar_nic_no = "";
                    _ReceiptHeader.Sar_comm_amt = 0;
                    _ReceiptHeader.Sar_is_mgr_iss = false;
                    _ReceiptHeader.Sar_esd_rate = 0;
                    _ReceiptHeader.Sar_wht_rate = 0;
                    _ReceiptHeader.Sar_epf_rate = 0;
                    _ReceiptHeader.Sar_currency_cd = "LKR";
                    _ReceiptHeader.Sar_uploaded_to_finance = false;
                    _ReceiptHeader.Sar_act = true;
                    _ReceiptHeader.Sar_direct_deposit_bank_cd = depBank;
                    _ReceiptHeader.Sar_direct_deposit_branch = depBranch;
                    // _ReceiptHeader.Sar_remarks = txtNote.Text.Trim();
                    _ReceiptHeader.Sar_is_used = false;
                    _ReceiptHeader.Sar_ref_doc = "";
                    _ReceiptHeader.Sar_ser_job_no = jobno;
                    _ReceiptHeader.Sar_used_amt = 0;
                    _ReceiptHeader.Sar_create_by = Session["UserID"].ToString();
                    _ReceiptHeader.Sar_mod_by = Session["UserID"].ToString();
                    _ReceiptHeader.Sar_session_id = Session["SessionID"].ToString();
                    _ReceiptHeader.Sar_anal_1 = "";
                    _ReceiptHeader.Sar_anal_2 = "";
                    _ReceiptHeader.Sar_receipt_type = "REFUND";
                    _ReceiptHeader.Sar_debtor_cd = debtor;
                    _ReceiptHeader.Sar_tot_settle_amt = total_settle_amnt;
                }

                MasterAutoNumber _receiptAuto = null;
                if (_recieptItem != null)
                    if (_recieptItem.Count > 0)
                    {
                        _receiptAuto = new MasterAutoNumber();
                        _receiptAuto.Aut_cate_cd = userDefPro;
                        _receiptAuto.Aut_cate_tp = "PRO";
                        _receiptAuto.Aut_direction = 1;
                        _receiptAuto.Aut_moduleid = "RECEIPT";
                        _receiptAuto.Aut_number = 0;
                        _receiptAuto.Aut_start_char = "REC";
                        //_receiptAuto.Aut_year = Convert.ToDateTime(_ReceiptHeader.Sar_receipt_date).Year;
                        _receiptAuto.Aut_year = Convert.ToDateTime(_ReceiptHeader.Sar_create_when).Year; // Added by Chathura due to above value is always 1
                    }

                if (_recieptItem.Count < 1)
                {
                    return Json(new { success = false, login = true, msg = "Please add payment details.", Type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int effect = CHNLSVC.Sales.SaveJobReciept(_ReceiptHeader, _recieptItem, _receiptAuto, out err);
                    if (effect > 0)
                    {
                        return Json(new { success = true, login = true, msg = "Successfully Saved : " + err, receiptNo = err }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = err, Type = "Error" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CheckPermissionForRefund(string status)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            bool pms = false;

            if (CHNLSVC.Security.Is_OptionPerimitted(company, userId.ToUpper(), 1006))
            {
                Session["permissonSession"] = true;
            }
            else
            {
                Session["permissonSession"] = false;
            }

            return Json(new { success = true, login = true, data = pms }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CancelRefund(string jobno)
        {
            //int status = 1;// (int)CHNLSVC.Sales.CancelRefund(jobno);

            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (CHNLSVC.Security.Is_OptionPerimitted(company, userId.ToUpper(), 1006))
                    {

                        int effect = (int)CHNLSVC.Sales.CancelRefund(jobno);
                        if (effect > 0)
                        {
                            return Json(new { success = true, login = true, msg = "Successfully Canceled!", type = "Succ" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = true, login = true, msg = "Error", type = "Error" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = true, login = true, msg = "No permission to cancel", type = "Error" }, JsonRequestBehavior.AllowGet);
                    }


                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult validateHasFullRefundExists(string reqno)
        {
            bool status = false;
            string code = "";

            List<TRN_PETTYCASH_SETTLE_DTL> toRefund = new List<TRN_PETTYCASH_SETTLE_DTL>();
            if (Session["RefundSettlement"] != null)
            {
                toRefund = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["RefundSettlement"];
            }

            List<TRN_PETTYCASH_SETTLE_DTL> toAddedRefund = new List<TRN_PETTYCASH_SETTLE_DTL>();
            if (Session["ToRefundSettlement"] != null)
            {
                toAddedRefund = (List<TRN_PETTYCASH_SETTLE_DTL>)Session["ToRefundSettlement"];
            }

            foreach (TRN_PETTYCASH_SETTLE_DTL reff in toRefund)
            {
                foreach (TRN_PETTYCASH_SETTLE_DTL reffAdd in toAddedRefund)
                {
                    if (reff.TPSD_REQ_NO == reffAdd.TPSD_REQ_NO)
                    {
                        status = true;
                        code = reffAdd.TPSD_REQ_NO;
                        break;
                    }
                }
                
            }

            //bool isAdded = false;
            //List<TRN_PETTYCASH_SETTLE_DTL> RefundedSettlementJob = new List<TRN_PETTYCASH_SETTLE_DTL>();
            //RefundedSettlementJob = CHNLSVC.Sales.CheckJobAlreadyHasRefunds(reqno);
            //if (RefundedSettlementJob.Count > 0) isAdded = true; else isAdded = false;


            return Json(new { success = true, login = true, data = status, reqNo = code }, JsonRequestBehavior.AllowGet);
        }


    }
}