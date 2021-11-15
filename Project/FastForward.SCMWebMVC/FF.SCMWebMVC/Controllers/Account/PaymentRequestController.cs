using FF.BusinessObjects;
using FF.BusinessObjects.Account;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FF.SCMWebMVC.Controllers.Account
{
    public class PaymentRequestController : BaseController
    {
        // GET: PaymentRequest
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["ACCOUNT_DETAILS"] = null;
                Session["VALUE_ITM_LIST"] = null;
                Session["HEADER_DET"] = null;
                Session["HEADER_DET"] = null;
                Session["MST_ACC_TAX"] = null;
                Session["PUR_SELECTED"] = null;
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }
        public JsonResult addAccountDetails(string accNo,string amount)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    accNo=accNo.Trim();
                    amount=amount.Trim();
                    if (accNo == "" || amount=="")
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid account number and amount." }, JsonRequestBehavior.AllowGet);
                    }
                    List<MST_PAY_REQ_DET> accdet = (List<MST_PAY_REQ_DET>)Session["ACCOUNT_DETAILS"];
                    bool has = false;
                    if (accdet != null && accdet.Count > 0)
                    {
                        has = accdet.Any(x => x.MPRD_ACC_NO == accNo  && x.MPRD_STUS==1);
                    }
                    if (has)
                    {
                        return Json(new { success = false, login = true, msg = "Account number <b>'"+accNo+"'</b> already in the list." }, JsonRequestBehavior.AllowGet);
                    }
                    bool hve=false;
                    if (accdet != null && accdet.Count > 0)
                    {
                        hve = accdet.Any(x => x.MPRD_ACC_NO == accNo && x.MPRD_STUS == 0 && x.MPRD_AMT==Convert.ToDecimal(amount));
                    }
                    if (hve)
                    {
                        foreach (MST_PAY_REQ_DET i in accdet)
                        {
                            if (i.MPRD_ACC_NO == accNo && i.MPRD_STUS == 0 && i.MPRD_AMT == Convert.ToDecimal(amount))
                            {
                                i.MPRD_STUS = 1;
                                i.UPDATED = 1;
                            }
                        }

                        Session["ACCOUNT_DETAILS"] = accdet;
                        return Json(new { success = true, login = true, data = accdet }, JsonRequestBehavior.AllowGet);
                    }

                    if (accdet == null)
                    {
                        accdet = new List<MST_PAY_REQ_DET>();
                    }
                    MST_PAY_REQ_DET acc = new MST_PAY_REQ_DET();
                    acc.MPRD_ACC_NO = accNo;
                    acc.NEW_ADDED = 1;
                    acc.MPRD_CRE_BY = userId;
                    acc.MPRD_STUS = 1;

                    Int32 line = 1;
                    if (accdet != null && accdet.Count > 0)
                    {
                        line = accdet.Max(a => a.MPRD_ITM_LINE) + 1;
                    }
                    acc.MPRD_ITM_LINE = line;
                    try
                    {
                        acc.MPRD_AMT = Convert.ToDecimal(amount);
                    }
                    catch (Exception e)
                    {
                        return Json(new { success = false, login = true,msg="Please enter valid amount." }, JsonRequestBehavior.AllowGet);
                    }
                    accdet.Add(acc);
                    Session["ACCOUNT_DETAILS"] = accdet;
                    return Json(new { success = true, login = true ,data=accdet }, JsonRequestBehavior.AllowGet);
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
        public JsonResult removeAccountDetaills(string module, string LineNo)
        {  string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    //acccd = acccd.Trim();
                    //if(string.IsNullOrEmpty(acccd))
                    //{
                    //    return Json(new { success = false, login = true, msg = "Selected account code is invalid." }, JsonRequestBehavior.AllowGet);

                    //}
                    List<MST_PAY_REQ_DET> accdet = (List<MST_PAY_REQ_DET>)Session["ACCOUNT_DETAILS"];
                    if (accdet.Count > 0)
                    {
                        bool has = accdet.Any(x => x.MPRD_ITM_LINE == Convert.ToInt32(LineNo) && x.NEW_ADDED==1);
                        if (has)
                        {
                            accdet.RemoveAll(x => x.MPRD_ITM_LINE == Convert.ToInt32(LineNo));
                            Session["ACCOUNT_DETAILS"] = accdet;
                            return Json(new { success = true, login = true, data = accdet }, JsonRequestBehavior.AllowGet);

                        }
                        else
                        {
                            foreach (MST_PAY_REQ_DET it in accdet)
                            {
                                if (it.MPRD_ITM_LINE == Convert.ToInt32(LineNo))
                                {
                                    it.MPRD_MOD_BY = userId;
                                    it.UPDATED = 1;
                                    it.MPRD_STUS = 0;
                                }
                            }
                            Session["ACCOUNT_DETAILS"] = accdet;
                            return Json(new { success = true, login = true, data = accdet }, JsonRequestBehavior.AllowGet);
                        }
                        //bool has = accdet.Any(x => x.ACC_NO == acccd);
                        //if (has)
                        //{
                        //     accdet.RemoveAll(x => x.ACC_NO == acccd);
                        //     Session["ACCOUNT_DETAILS"] = accdet;
                        //     return Json(new { success = true, login = true, data = accdet }, JsonRequestBehavior.AllowGet);

                        //}
                        //else
                        //{
                        //    return Json(new { success = false, login = true, msg = "Selected account code is invalid." }, JsonRequestBehavior.AllowGet);

                        //}
                    }
                    return Json(new { success = true, login = true, data = accdet }, JsonRequestBehavior.AllowGet);

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
        public JsonResult getSavedValueDetails(string code, string savedVal)
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
                    //List<VALUE_ITM_LIST> VALUE_ITM_LIST = null;
                    //    (List<VALUE_ITM_LIST>) (Session["VALUE_ITM_LIST"]);
                    //if(VALUE_ITM_LIST==null){
                    //    VALUE_ITM_LIST=new List<VALUE_ITM_LIST>();
                    //}
                    if (!string.IsNullOrEmpty(code))
                    {
                        string error = "";
                        List<TABLE_HED> tblHed = CHNLSVC.Finance.getTemplateTableColum(company, code, out error);
                        if (error != "")
                        { 
                            return Json(new { success = false, login = true,msg=error }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            List<REF_TMPLT_ITM_VALUE_TABLE> value = CHNLSVC.Finance.getItemSavedValues("PAYREQ", code, company, savedVal, out error);
                            if (error != "")
                            {
                                return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                List<VALUE_ITM_LIST> VALUE_ITM_LIST = new List<VALUE_ITM_LIST>();
                                ////bool has=VALUE_ITM_LIST.Any(x=>x.UNQ_VAL==code);
                                //if (value.Count > 0 /*&& has==false*/)
                                //{
                                //        var customerIds = value.GroupBy(x => x.RTIV_SEQ).Select(p => p.Select(o => o.RTIV_SEQ));
                                //        foreach (var id in customerIds)
                                //        {
                                //            VALUE_ITM_LIST itm = new VALUE_ITM_LIST();
                                //            itm.RTIV_SEQ = Convert.ToInt32(id.ToArray()[0].ToString());                                            
                                //            itm.IS_UPDATED = 0;
                                //            itm.NEW_ADDED = 0;
                                //            itm.ITEMS = new List<REF_TMPLT_ITM_VALUE_TABLE>();
                                //            List<REF_TMPLT_ITM_VALUE_TABLE> newLst = value.Where(x => x.RTIV_SEQ == Convert.ToInt32(id.ToArray()[0].ToString())).OrderBy(x => x.RTIV_DET_ID).ToList();

                                //            foreach (REF_TMPLT_ITM_VALUE_TABLE i in newLst)
                                //            {
                                //                itm.RTIV_DIRECT = i.RTIV_DIRECT;
                                //                itm.UNQ_VAL = i.RTIV_UNQ_CD;
                                //                REF_TMPLT_ITM_VALUE_TABLE ni = new REF_TMPLT_ITM_VALUE_TABLE();
                                //                ni.RTIV_DIRECT = i.RTIV_DIRECT;
                                //                ni.RTIV_SEQ = Convert.ToInt32(id.ToArray()[0].ToString());
                                //                ni.RTIV_ID = i.RTIV_ID;
                                //                ni.RTIV_HED_ID = i.RTIV_HED_ID;
                                //                ni.RTIV_DET_ID = i.RTIV_DET_ID;
                                //                ni.RTIV_UNQ_CD = i.RTIV_UNQ_CD;
                                //                ni.RTIV_COM = i.RTIV_COM;
                                //                ni.RTIV_VALUE = i.RTIV_VALUE;
                                //                ni.RTIV_MODULE = i.RTIV_MODULE;
                                //                ni.RO_TYPE = i.RO_TYPE;
                                //                ni.DEF_VAL_FLD = i.DEF_VAL_FLD;
                                //                itm.ITEMS.Add(ni);
                                //            }
                                //            itm.STUS = newLst[0].RTIV_STUS;
                                //            VALUE_ITM_LIST.Add(itm);
                                //        }
                                   
                                //    Session["VALUE_ITM_LIST"] = VALUE_ITM_LIST;
                                    
                                //}

                                VALUE_ITM_LIST = (List<VALUE_ITM_LIST>)Session["VALUE_ITM_LIST"];
                                List<VALUE_ITM_LIST> CODE_VAL = new List<VALUE_ITM_LIST>();
                                if (VALUE_ITM_LIST != null)
                                {
                                    if (VALUE_ITM_LIST.Count > 0)
                                    {
                                        CODE_VAL = VALUE_ITM_LIST.Where(x => x.UNQ_VAL == code && x.STUS == 1).ToList();
                                    }
                                }
                                return Json(new { success = true, login = true, hedDet = tblHed, value = CODE_VAL }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult removeRequestItem(string detid)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<VALUE_ITM_LIST> VALUE_ITM_LIST = (List<VALUE_ITM_LIST>)(Session["VALUE_ITM_LIST"]);
                    string[] itm = detid.Split('_');
                    if (VALUE_ITM_LIST.Count > 0)
                    {
                        bool has = VALUE_ITM_LIST.Any(x => x.UNQ_VAL == itm[0].ToString() && x.RTIV_SEQ == Convert.ToInt32(itm[1].ToString()));
                        if (has)
                        {
                            foreach (VALUE_ITM_LIST x in VALUE_ITM_LIST)
                            {
                                if (x.RTIV_SEQ == Convert.ToInt32(itm[1].ToString()) && x.UNQ_VAL == itm[0].ToString())
                                {
                                    x.NEW_ADDED = 0;
                                    x.IS_UPDATED = 1;
                                    x.STUS = 0;
                                }
                            }
                        }
                        Session["VALUE_ITM_LIST"] = VALUE_ITM_LIST;
                    }
                    List<VALUE_ITM_LIST> ACC_DET = new List<VALUE_ITM_LIST>();

                    List<MST_PAY_REQ_DET> MST_PAY_REQ_DET = new List<MST_PAY_REQ_DET>();
                    if (VALUE_ITM_LIST.Count > 0)
                    {
                        foreach (VALUE_ITM_LIST im in VALUE_ITM_LIST)
                        {
                            if (im.STUS == 1)
                            {
                                foreach (REF_TMPLT_ITM_VALUE_TABLE i in im.ITEMS)
                                {
                                    MST_PAY_REQ_DET details = new MST_PAY_REQ_DET();
                                    if (i.DEF_VAL_FLD == 1)
                                    {
                                        details.MPRD_AMT = Convert.ToDecimal(i.RTIV_VALUE);
                                        details.MPRD_ITM_LINE = 0;
                                        details.MPRD_ACC_NO = i.RTIV_UNQ_CD;
                                        details.ACC_DESC = "";
                                        MST_PAY_REQ_DET.Add(details);
                                    }
                                }
                            }
                        }

                        MST_PAY_REQ_DET = MST_PAY_REQ_DET.GroupBy(c => new { c.MPRD_ACC_NO })
                             .Select(cl => new MST_PAY_REQ_DET
                             {
                                 MPRD_ACC_NO = cl.First().MPRD_ACC_NO,
                                 ACC_DESC = cl.First().ACC_DESC,
                                 MPRD_ITM_LINE = cl.First().MPRD_ITM_LINE,
                                 MPRD_AMT = cl.Sum(c => decimal.Parse(c.MPRD_AMT.ToString(), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint))

                             }).ToList();
                        ACC_DET = VALUE_ITM_LIST.Where(c => c.UNQ_VAL == itm[0].ToString() && c.STUS==1).ToList();
                    }

                    return Json(new { success = true, login = true, value = ACC_DET, MST_PAY_REQ_DET = MST_PAY_REQ_DET }, JsonRequestBehavior.AllowGet);
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
        public JsonResult addNewDynamicValue(FormCollection data, string code, Int32 iscredit) 
        { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (string.IsNullOrEmpty(code.Trim()))
                    {
                        return Json(new { success = true, login = false,msg="Please select valid debitor account code." }, JsonRequestBehavior.AllowGet);

                    }
                    List<VALUE_ITM_LIST> VALUE_ITM_LIST = (List<VALUE_ITM_LIST>)(Session["VALUE_ITM_LIST"]);
                    if (VALUE_ITM_LIST == null)
                    {
                        VALUE_ITM_LIST = new List<VALUE_ITM_LIST>();
                    }
                    VALUE_ITM_LIST itm = new VALUE_ITM_LIST();
                    itm.STUS = 1;
                    itm.RTIV_SEQ = 1;
                    itm.NEW_ADDED = 1;
                    itm.IS_UPDATED = 0;
                    itm.RTIV_DIRECT = iscredit;
                    itm.UNQ_VAL = code.Trim();
                    itm.ITEMS = new List<REF_TMPLT_ITM_VALUE_TABLE>();
                    string error = "";
                    Int32 nxtseq = 1;
                    if(VALUE_ITM_LIST !=null && VALUE_ITM_LIST.Count>0){
                        nxtseq = VALUE_ITM_LIST.Max(x => x.RTIV_SEQ)+1;
                        itm.RTIV_SEQ = nxtseq;
                    }
                    List<FORM_TMPLT_VALUE> tmplt = (List<FORM_TMPLT_VALUE>)Session["TEMP_VAL_DET"];
                    if (tmplt != null)
                    {
                        foreach (var key in data.Keys)
                        {


                            REF_TMPLT_ITM_VALUE_TABLE ni = new REF_TMPLT_ITM_VALUE_TABLE();
                            string keys = key.ToString();
                            string value = data[key.ToString()];
                            if (string.IsNullOrEmpty(value))
                            {
                                return Json(new { success = false, login = true, msg = "Please fill all field values." }, JsonRequestBehavior.AllowGet);
                            }
                            string[] keyval = keys.Split('_');
                            ni.RTIV_ID = 0;
                            ni.RTIV_HED_ID = Convert.ToInt32(keyval[2].ToString());
                            ni.RTIV_DET_ID = Convert.ToInt32(keyval[3].ToString());
                            ni.RTIV_UNQ_CD = code.Trim();
                            ni.RTIV_COM = company;
                            ni.RTIV_SEQ = nxtseq;
                            ni.RTIV_VALUE = value;
                            ni.RTIV_MODULE = "PAYREQ";
                            ni.RTIV_DIRECT = iscredit;
                            Int32 defValfld = 0;
                            if (tmplt.Count > 0)
                            {
                                foreach (TEMPLATE_ITM_DET it in tmplt[0].ITEM[0].TEMPLATE_DET)
                                {
                                    if (it.DETAIL_ID == Convert.ToInt32(keyval[3].ToString()))
                                    {
                                        defValfld = it.DEF_VAL_FLD;
                                    }
                                }
                            }
                            ni.DEF_VAL_FLD = defValfld;
                            ni.RO_TYPE = CHNLSVC.Finance.getFieldType(ni.RTIV_HED_ID, ni.RTIV_DET_ID, out error);
                            if (error != "")
                            {
                                return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);

                            }
                            itm.ITEMS.Add(ni);

                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid template details." }, JsonRequestBehavior.AllowGet);
                    }
                    VALUE_ITM_LIST.Add(itm);
                    Session["VALUE_ITM_LIST"]=VALUE_ITM_LIST;
                    List<VALUE_ITM_LIST> ACC_VAL=  VALUE_ITM_LIST.Where(x=>x.UNQ_VAL==code).ToList();

                    List<MST_PAY_REQ_DET> MST_PAY_REQ_DET = new List<MST_PAY_REQ_DET>();

                    foreach (VALUE_ITM_LIST im in VALUE_ITM_LIST)
                    {
                        foreach (REF_TMPLT_ITM_VALUE_TABLE i in im.ITEMS)
                        {
                            MST_PAY_REQ_DET details = new MST_PAY_REQ_DET();
                            if (i.DEF_VAL_FLD == 1)
                            {
                                if (i.RTIV_DIRECT == 0)
                                {
                                    details.MPRD_AMT = Convert.ToDecimal(i.RTIV_VALUE);
                                }
                                else
                                {
                                    details.MPRD_AMT = -1*Convert.ToDecimal(i.RTIV_VALUE);
                                }
                                
                                details.MPRD_ITM_LINE = 0;
                                details.MPRD_ACC_NO = i.RTIV_UNQ_CD;
                                details.ACC_DESC = "";
                                MST_PAY_REQ_DET.Add(details);
                            }
                        }
                    }
                    
                    MST_PAY_REQ_DET =  MST_PAY_REQ_DET.GroupBy(c => new { c.MPRD_ACC_NO })
                         .Select(cl => new MST_PAY_REQ_DET
                         {
                             MPRD_ACC_NO = cl.First().MPRD_ACC_NO,
                             ACC_DESC = cl.First().ACC_DESC,
                             MPRD_ITM_LINE = cl.First().MPRD_ITM_LINE,
                             MPRD_AMT = cl.Sum(c => decimal.Parse(c.MPRD_AMT.ToString(), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint))

                         }).ToList();
                    return Json(new { success = true, login = true, data = VALUE_ITM_LIST, ACC_VAL = ACC_VAL, MST_PAY_REQ_DET = MST_PAY_REQ_DET }, JsonRequestBehavior.AllowGet);
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
        public JsonResult savePaymentRequest(FormCollection data,string operation) 
        { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<VALUE_ITM_LIST> VALUE_ITM_LIST = (List<VALUE_ITM_LIST>)(Session["VALUE_ITM_LIST"]);
                    List<MST_PAY_REQ_DET> accdet = new List<MST_PAY_REQ_DET>();
                        //(List<MST_PAY_REQ_DET>)Session["ACCOUNT_DETAILS"];
                    //if (accdet == null || accdet.Count == 0)
                    //{
                    //    return Json(new { success = false, login = true, msg = "Please add crediter details." }, JsonRequestBehavior.AllowGet);

                    //}
                    if (VALUE_ITM_LIST == null || VALUE_ITM_LIST.Count == 0)
                    {
                        return Json(new { success = false, login = true, msg = "Please add debitor account value details." }, JsonRequestBehavior.AllowGet);

                    }
                    string reqno = data["ReqNo"].ToString();
                    string payType = data["PayType"].ToString();
                    string creditor = data["Creditor"].ToString();
                    string description = data["description"].ToString();
                    decimal grosamt = (data["GrosAmt"].ToString() != "") ? Convert.ToDecimal(data["GrosAmt"].ToString()) : 0;
                    decimal tax = (data["Tax"].ToString() != "") ? Convert.ToDecimal(data["Tax"].ToString()) : 0;
                    decimal netamt = (data["NetAmt"].ToString() != "") ? Convert.ToDecimal(data["NetAmt"].ToString()) : 0;

                    decimal accVal = accdet.Where(y=>y.MPRD_STUS == 1).Sum(x => x.MPRD_AMT);
                    decimal addVal = 0;
                    List<MST_ACC_TAX> MST_ACC_TAX = (List<MST_ACC_TAX>)Session["MST_ACC_TAX"];
                    if (tax !=0 && ( MST_ACC_TAX == null || MST_ACC_TAX.Count == 0))
                    {
                        return Json(new { success = false, login = true, msg = "Please add tax details." }, JsonRequestBehavior.AllowGet);
                    }
                    if (tax != 0)
                    {
                        decimal addedtax = MST_ACC_TAX.Where(x => x.MAT_STUS == 1).Sum(x => x.MAT_TAX_AMT);

                        if (addedtax != tax)
                        {
                            return Json(new { success = false, login = true, msg = "Total tax value not tally with tax break-up." }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    string error = "";
                    REF_CHT_ACC dat = CHNLSVC.Finance.getAccountDetail(creditor.Trim(), company, out error);
                    if (error != "" || dat.RCA_ACC_NO == null)
                    {
                        if (dat.RCA_ACC_NO == null)
                        {
                            return Json(new { success = false, login = true, msg = "Please enter valid creditor account number." }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                    }

                    if (dat.RCA_ANAL5 == 1)
                    {
                        MST_ACC_TAX.ForEach(x =>
                        {
                            x.UPDATED = 0;
                            x.NEW_ADDED = 0;
                        });
                    }

                    foreach (VALUE_ITM_LIST mm in VALUE_ITM_LIST)
                    {
                        foreach (REF_TMPLT_ITM_VALUE_TABLE gg in mm.ITEMS)
                        {
                            if (gg.DEF_VAL_FLD == 1 && mm.STUS == 1)
                            {
                                if (gg.RTIV_DIRECT == 0)
                                {
                                    addVal += Convert.ToDecimal(gg.RTIV_VALUE);
                                }
                                else
                                {
                                    addVal  =addVal - Convert.ToDecimal(gg.RTIV_VALUE);
                                }
                            }
                        }
                    }
                    if (/*netamt != accVal || */netamt != addVal /*|| accVal != addVal*/)
                    {
                        return Json(new { success = false, login = true, msg = "Net amount not tally with other amounts." }, JsonRequestBehavior.AllowGet);

                    }
                    DateTime date = new DateTime();
                    try
                    {
                        date = Convert.ToDateTime(data["ReqDate"].ToString());
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid request date." }, JsonRequestBehavior.AllowGet);

                    }
                    List<PUR_SELECTED> PUR_SELECTED = new List<PUR_SELECTED>();
                    if (data["DropdownReqTp"].ToString() != "PTTYCSH")
                    {
                        PUR_SELECTED=(List<PUR_SELECTED>)Session["PUR_SELECTED"];
                        if (PUR_SELECTED == null || PUR_SELECTED.Count == 0)
                        {
                            return Json(new { success = false, login = true, msg = "No purchase order details found." }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    MasterAutoNumber _masterAutoNumber = new MasterAutoNumber();

                    _masterAutoNumber.Aut_moduleid="PAYREQ";
                    _masterAutoNumber.Aut_direction=1;
                    _masterAutoNumber.Aut_start_char="PAYREQ";
                    _masterAutoNumber.Aut_cate_tp="PAYTYP";
                    _masterAutoNumber.Aut_cate_cd = "PAYREQ";
                    _masterAutoNumber.Aut_modify_dt=null;
                    _masterAutoNumber.Aut_year = DateTime.Now.Date.Year;

                    MST_PAY_REQ_HDR hdr = new MST_PAY_REQ_HDR();  
                    hdr.MPRH_REQ_DT = date;
                    hdr.MPRH_PAY_TP = payType;
                    hdr.MPRH_CREDITOR =creditor;
                    hdr.MPRH_RMK =description;
                    hdr.MPRH_GROS_AMT =grosamt;
                    hdr.MPRH_TAX =tax;
                    hdr.MPRH_NET_AMT =grosamt+tax;
                    hdr.MPRH_STUS ="P";
                    hdr.MPRH_CRE_BY =userId;
                    hdr.MPRH_CRE_DT = DateTime.Now;
                    hdr.MPRH_MOD_BY = userId;
                    hdr.MPRH_MOD_DT = DateTime.Now;
                    hdr.MPRH_COM = company;
                    hdr.MPRH_REQ_NO = reqno;
                    hdr.MPRH_SESSION_ID = (string)Session["SessionID"];
                    hdr.MPRH_REQ_TP = data["DropdownReqTp"].ToString();
                    hdr.MPRH_REF_NO = (data["RefNo"].ToString() != "") ?  data["RefNo"].ToString()  : "";
                    if (Session["HEADER_DET"] != null)
                    {
                        MST_PAY_REQ_HDR nhd = (MST_PAY_REQ_HDR)Session["HEADER_DET"];
                        if (nhd != null && nhd.MPRH_SEQ != 0)
                        {
                            hdr.MPRH_REQ_NO = nhd.MPRH_REQ_NO;
                            hdr.MPRH_SEQ = nhd.MPRH_SEQ;
                            if (nhd.MPRH_STUS == "F")
                            {
                                return Json(new { success = false, login = true, msg = "Processed request cannot update." }, JsonRequestBehavior.AllowGet);
                            }
                            if (nhd.MPRH_STUS != "P")
                            {
                                return Json(new { success = false, login = true, msg = "You can update only pending request." }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    //List<MST_PAY_REQ_DET> det = new List<MST_PAY_REQ_DET>();
                    //foreach (ACCOUNT_DETAILS acd in accdet)
                    //{
                    //    MST_PAY_REQ_DET itm = new MST_PAY_REQ_DET();
                    //    itm.MPRD_ACC_NO = acd.ACC_NO;
                    //    itm.MPRD_AMT = acd.ACC_AMOUNT;
                    //    det.Add(itm);
                    //} 
                    Int32 eff = 0;
                    if (operation == "Save")
                    {
                        eff = CHNLSVC.Finance.savePaymentRequestDetails(hdr, accdet, VALUE_ITM_LIST,MST_ACC_TAX,PUR_SELECTED, _masterAutoNumber, out error);
                    }
                    else
                    {
                        eff = CHNLSVC.Finance.updatePaymentRequestDetails(hdr, accdet, VALUE_ITM_LIST,MST_ACC_TAX, out error);
                    }
                   
                   

                    if (error != "" && eff == -1)
                    {
                        return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { success = true, login = true,msg=error }, JsonRequestBehavior.AllowGet);
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
        public JsonResult clearSesstion()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    Session["ACCOUNT_DETAILS"] = null;
                    Session["VALUE_ITM_LIST"] = null;
                    Session["HEADER_DET"] = null;
                    Session["HEADER_DET"] = null;
                    Session["MST_ACC_TAX"] = null;
                    Session["PUR_SELECTED"] = null;
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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }

            }

        }
        public JsonResult getRequestDetails(string reqno, string reqtp) 
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    reqno = reqno.Trim();
                    if (reqno != "")
                    {
                        Session["ACCOUNT_DETAILS"] = null;
                        Session["VALUE_ITM_LIST"] = null;
                        Session["HEADER_DET"] = null;
                        Session["HEADER_DET"] = null;
                        Session["MST_ACC_TAX"] = null;
                        Session["PUR_SELECTED"] = null;

                        string error = "";
                        MST_PAY_REQ_HDR hdr = CHNLSVC.Finance.getPaymentreqHdr(reqno, company, reqtp, out error);
                        if (error != "")
                        {
                            return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);

                        }
                        if (hdr != null && hdr.MPRH_REQ_NO != null)
                        {
                            Session["HEADER_DET"] = hdr;
                            List<MST_PAY_REQ_DET> det = new List<MST_PAY_REQ_DET>();
                            List<PUR_SELECTED> selecteditem = new List<PUR_SELECTED>(); 
                            det = CHNLSVC.Finance.getPayReqdetails(hdr.MPRH_COM,"PAYREQ" ,reqno, out error);
                            if (error == "")
                            {
                                //List<ACCOUNT_DETAILS> acc = new List<ACCOUNT_DETAILS>();
                                //foreach (MST_PAY_REQ_DET rq in det)
                                //{
                                //    ACCOUNT_DETAILS itm = new ACCOUNT_DETAILS();
                                //    itm.ACC_NO = rq.MPRD_ACC_NO;
                                //    itm.ACC_AMOUNT = rq.MPRD_AMT;
                                //    acc.Add(itm);
                                //}

                                Session["ACCOUNT_DETAILS"] = det;

                                #region saved item details
                                List<REF_TMPLT_ITM_VALUE_TABLE> value = CHNLSVC.Finance.getItemSavedValues("PAYREQ", null, company, reqno, out error);
                                if (error != "")
                                {
                                    return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    List<VALUE_ITM_LIST> VALUE_ITM_LIST = new List<VALUE_ITM_LIST>();
                                    //bool has=VALUE_ITM_LIST.Any(x=>x.UNQ_VAL==code);
                                    if (value.Count > 0 /*&& has==false*/)
                                    {
                                        var customerIds = value.GroupBy(x => x.RTIV_SEQ).Select(p => p.Select(o => o.RTIV_SEQ));
                                        foreach (var id in customerIds)
                                        {
                                            VALUE_ITM_LIST itm = new VALUE_ITM_LIST();
                                            itm.RTIV_SEQ = Convert.ToInt32(id.ToArray()[0].ToString());
                                            itm.IS_UPDATED = 0;
                                            itm.NEW_ADDED = 0;
                                            itm.ITEMS = new List<REF_TMPLT_ITM_VALUE_TABLE>();
                                            List<REF_TMPLT_ITM_VALUE_TABLE> newLst = value.Where(x => x.RTIV_SEQ == Convert.ToInt32(id.ToArray()[0].ToString())).OrderBy(x => x.RTIV_DET_ID).ToList();

                                            foreach (REF_TMPLT_ITM_VALUE_TABLE i in newLst)
                                            {
                                                itm.RTIV_DIRECT = i.RTIV_DIRECT;
                                                itm.UNQ_VAL = i.RTIV_UNQ_CD;
                                                REF_TMPLT_ITM_VALUE_TABLE ni = new REF_TMPLT_ITM_VALUE_TABLE();
                                                ni.RTIV_DIRECT = i.RTIV_DIRECT;
                                                ni.RTIV_SEQ = Convert.ToInt32(id.ToArray()[0].ToString());
                                                ni.RTIV_ID = i.RTIV_ID;
                                                ni.RTIV_HED_ID = i.RTIV_HED_ID;
                                                ni.RTIV_DET_ID = i.RTIV_DET_ID;
                                                ni.RTIV_UNQ_CD = i.RTIV_UNQ_CD;
                                                ni.RTIV_COM = i.RTIV_COM;
                                                ni.RTIV_VALUE = i.RTIV_VALUE;
                                                ni.RTIV_MODULE = i.RTIV_MODULE;
                                                ni.RO_TYPE = i.RO_TYPE;
                                                ni.DEF_VAL_FLD = i.DEF_VAL_FLD;
                                                itm.ITEMS.Add(ni);
                                            }
                                            itm.STUS = newLst[0].RTIV_STUS;
                                            VALUE_ITM_LIST.Add(itm);
                                        }

                                        Session["VALUE_ITM_LIST"] = VALUE_ITM_LIST;

                                    }
                                    //List<VALUE_ITM_LIST> CODE_VAL = new List<VALUE_ITM_LIST>();
                                    //if (VALUE_ITM_LIST.Count > 0)
                                    //{
                                    //    CODE_VAL = VALUE_ITM_LIST.Where(x=>x.UNQ_VAL==code).ToList();
                                    //}

                                #endregion
                                    REF_CHT_ACC dat = CHNLSVC.Finance.getAccountDetail(hdr.MPRH_CREDITOR, company, out error);
                                    #region tax details
                                    List<MST_ACC_TAX> taxdet = CHNLSVC.Finance.getRequestTaxDetails(hdr.MPRH_COM, reqno,out error);
                                    if (error != "")
                                    {
                                        return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                                    }
                                    Session["MST_ACC_TAX"] = taxdet;
                                    #endregion
                                    #region po det
                                    List<MST_PAY_REQ_REF> payRef = new List<MST_PAY_REQ_REF>();
                                    if(reqtp!="PTTYCSH"){

                                        payRef = CHNLSVC.Finance.getPayReqPoDet(reqno, reqtp, company,out error);
                                        if (error != "")
                                        {
                                            return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                                        }
                                        
                                        if(payRef.Count>0){
                                           
                                            foreach(MST_PAY_REQ_REF itm in payRef){
                                                PUR_SELECTED x = new PUR_SELECTED();
                                                x.PURNO = itm.PRR_REF_NO;
                                                x.COST = itm.PRR_COST;
                                                x.TAX = itm.PRR_TAX;
                                                selecteditem.Add(x);
                                            }
                                        }
                                    }
                                    decimal totTax = selecteditem.Sum(x=>x.TAX);
                                    decimal totCost = selecteditem.Sum(x => x.COST);
                                    Session["PUR_SELECTED"] = selecteditem;
                                     #endregion
                                    return Json(new { success = true, login = true, hdr = hdr, det = det, tax = taxdet, auto = dat.RCA_ANAL5, totTax = totTax, totCost = totCost,selecteditem = selecteditem }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);

                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true,msg="Please enter valid payment request number." }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }

            }
        }
        public JsonResult validatePayTp(string code)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (!string.IsNullOrEmpty(code))
                    {
                        string error = "";
                        SAR_PAY_TP dat = CHNLSVC.Finance.getAccPAymentType(code.Trim(), company, out error);
                        if (error != "")
                        {
                            return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                        }
                        if (dat.SAPT_CD != null)
                        {
                            return Json(new { success = true, login = true, data = dat }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid payment type." }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }

            }
        }
        public JsonResult validateAccountCde(string code)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (!string.IsNullOrEmpty(code))
                    {
                        string error = "";
                        REF_CHT_ACC dat = CHNLSVC.Finance.getAccountDetail(code.Trim(), company, out error);
                        if (error != "" || dat.RCA_ACC_NO==null)
                        {
                            if (dat.RCA_ACC_NO == null)
                            {
                                return Json(new { success = false, login = true, msg = "Please enter valid account number." }, JsonRequestBehavior.AllowGet);

                            }
                            return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                        }
                        if (dat.RCA_ACC_NO != null)
                        {
                            return Json(new { success = true, login = true, data = dat }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid Account Number." }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }

            }
        }
        public JsonResult approveRequest(string ReqNo,string val,string reason=null)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    ReqNo = ReqNo.Trim();
                    if (!string.IsNullOrEmpty(ReqNo))
                    {
                        Int32 perm = 16117;
                        if (!CHNLSVC.Security.Is_OptionPerimitted(company, userId, perm))
                        {
                            return Json(new { success = false, login = true, msg = "Sorry, You have no permission to approve/Reject this request.( Advice: Required permission code : " + perm + ") !", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        MST_PAY_REQ_HDR hdr = (MST_PAY_REQ_HDR)Session["HEADER_DET"];
                        if (hdr == null || hdr.MPRH_REQ_NO == null)
                        {
                            return Json(new { success = false, login = true, msg = "Please select valid request.", type = "Info" }, JsonRequestBehavior.AllowGet);

                        }
                        if (hdr.MPRH_STUS == "F")
                        {
                            return Json(new { success = false, login = true, msg = "Already processed request cannot update/Reject.", type = "Info" }, JsonRequestBehavior.AllowGet);

                        }
                        string error = "";
                        string stus = "";
                        switch (val){
                            case "Approve":
                                stus = "A";
                                break;
                            case "Reject":
                                stus = "R";
                                break;
                            case "Reset":
                                stus = "P";
                                break;
                            case "Cancel":
                                stus = "C";
                                break;
                            default:
                                stus="";
                                break;
                        }
                        if (stus == "")
                        {
                            return Json(new { success = false, login = true, msg = "Please select valid stus", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        string sessionid = (string)Session["SessionID"];
                        Int32 eff = CHNLSVC.Finance.approvePaymentRequet(ReqNo, company, userId, stus,reason,sessionid, out error);
                        if (error != "")
                        {
                            return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);

                        }
                        return Json(new { success = true, login = true, msg = "Successfully " + val + " request number : " + ReqNo }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new { success = false, login = true,msg="Please enter valid request number." }, JsonRequestBehavior.AllowGet);
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
        public JsonResult processRequest(string ReqNo,string reqtp)
        {
              string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    ReqNo = ReqNo.Trim();
                    if (!string.IsNullOrEmpty(ReqNo))
                    {
                        Int32 perm = 16118;
                        if (!CHNLSVC.Security.Is_OptionPerimitted(company, userId, perm))
                        {
                            return Json(new { success = false, login = true, msg = "Sorry, You have no permission to process this request.( Advice: Required permission code : " + perm + ") !", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                        string error = "";
                        MasterAutoNumber _masterAutoNumber = new MasterAutoNumber();

                        _masterAutoNumber.Aut_moduleid = "PAY";
                        _masterAutoNumber.Aut_direction = 1;
                        _masterAutoNumber.Aut_start_char = "PAY";
                        _masterAutoNumber.Aut_cate_tp = "PAY";
                        _masterAutoNumber.Aut_cate_cd = "PAY";
                        _masterAutoNumber.Aut_modify_dt = null;
                        _masterAutoNumber.Aut_year = DateTime.Now.Date.Year;
                        string sessionid = (string)Session["SessionID"];
                        Int32 eff = CHNLSVC.Finance.processPaymentRequest(ReqNo, company, userId, _masterAutoNumber,sessionid, reqtp,out error);
                        if (eff == -1 || error != "")
                        {
                            return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);

                        }
                        else {
                            return Json(new { success = true, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid request number." }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getPendingRequest(string type)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (type == null)
                    {
                        type = "PTTYCSH";
                    }
                    string error="";
                    List<MST_PAY_REQ_HDR> dtl = new List<MST_PAY_REQ_HDR>();
                    dtl = CHNLSVC.Finance.getAllRequestDetails(company,"P",type,out error);

                    Int32 perm = 16118;
                    if (CHNLSVC.Security.Is_OptionPerimitted(company, userId, perm))
                    {
                        List<MST_PAY_REQ_HDR> dtl1 = CHNLSVC.Finance.getAllRequestDetails(company, "A", type, out error);
                        if (error != "")
                        {
                            return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                        }
                        dtl.AddRange(dtl1);
                        List<MST_PAY_REQ_HDR> dtl2 = CHNLSVC.Finance.getAllRequestDetails(company, "F", type, out error);
                        if (error != "")
                        {
                            return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                        }
                        dtl.AddRange(dtl2);
                        dtl.OrderBy(x => x.MPRH_STUS).OrderBy(x=>x.MPRH_REQ_DT);
                    }
                    if (error != "")
                    {
                        return Json(new { success = false, login = true,msg=error }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = true, login = true,data=dtl }, JsonRequestBehavior.AllowGet);
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
        public JsonResult addRequestTax(string accNo, string TaxCode, string TaxAmount)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (string.IsNullOrEmpty(accNo))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid creditor account number." }, JsonRequestBehavior.AllowGet);
                    }
                    string error = "";
                    REF_CHT_ACC dat = CHNLSVC.Finance.getAccountDetail(accNo.Trim(), company, out error);
                    if (error != "" || dat.RCA_ACC_NO == null)
                    {
                        if (dat.RCA_ACC_NO == null)
                        {
                            return Json(new { success = false, login = true, msg = "Please enter valid creditor account number." }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                    }
                    if (dat.RCA_ANAL5 == 1)
                    {
                        return Json(new { success = false, login = true, msg = "Cannot add taxes for auto tax calculated creditor." }, JsonRequestBehavior.AllowGet);
                    }
                    List<MST_ACC_TAX> MST_ACC_TAX = (List<MST_ACC_TAX>)Session["MST_ACC_TAX"];

                    if (MST_ACC_TAX == null)
                    {
                        MST_ACC_TAX = new List<MST_ACC_TAX>();
                    }
                    MST_ACC_TAX itm = new MST_ACC_TAX();
                    itm.MAT_TAX_AMT = Convert.ToDecimal(TaxAmount);
                    itm.MAT_ACC_NO = accNo.Trim();
                    itm.MAT_TAX_CD = TaxCode;
                    itm.MAT_CRE_BY = userId;
                    itm.MAT_CRE_DT = DateTime.Now;
                    itm.MAT_MOD_BY = userId;
                    itm.MAT_MOD_DT = DateTime.Now;
                    itm.MAT_SESSION_ID = (string)Session["SessionID"];
                    itm.MAT_STUS = 1;
                    itm.NEW_ADDED = 1;
                    MST_ACC_TAX.Add(itm);
                    itm.MAT_COM = company;
                    Session["MST_ACC_TAX"] = MST_ACC_TAX;
                    return Json(new { success = true, login = true, data = MST_ACC_TAX }, JsonRequestBehavior.AllowGet);
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
        public JsonResult removeTaxDetaills(Int32 seq)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_ACC_TAX> MST_ACC_TAX = (List<MST_ACC_TAX>)Session["MST_ACC_TAX"];

                    if (MST_ACC_TAX == null || MST_ACC_TAX.Count==0)
                    {
                        return Json(new { success = false, login = true, msg = "No tax item to remove." }, JsonRequestBehavior.AllowGet);
                    }
                    if (MST_ACC_TAX.Count > 0)
                    {
                        foreach (MST_ACC_TAX r in MST_ACC_TAX)
                        {
                            if (r.MAT_SEQ == seq)
                            {
                                r.MAT_MOD_BY = userId;
                                r.MAT_MOD_DT = DateTime.Now;
                                r.MAT_CRE_BY = userId;
                                r.MAT_CRE_DT = DateTime.Now;
                                r.MAT_STUS = 0;
                                r.UPDATED = 1;
                            }
                        }
                    }

                    Session["MST_ACC_TAX"]=MST_ACC_TAX;
                    return Json(new { success = true, login = true, data = MST_ACC_TAX }, JsonRequestBehavior.AllowGet);
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
        public JsonResult validateAmtAndTax(string amount, string creditor)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    if (string.IsNullOrEmpty(creditor))
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid creditor account number." }, JsonRequestBehavior.AllowGet);
                    }
                    decimal amt = 0;
                    try
                    {
                        amt = Convert.ToDecimal(amount);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid gross amount." }, JsonRequestBehavior.AllowGet);

                    }
                    string error = "";
                    REF_CHT_ACC dat = CHNLSVC.Finance.getAccountDetail(creditor.Trim(), company, out error);
                    if (error != "" || dat.RCA_ACC_NO == null)
                    {
                        if (dat.RCA_ACC_NO == null)
                        {
                            return Json(new { success = false, login = true, msg = "Please enter valid creditor account number." }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                    }
                    if (dat.RCA_ANAL5 == 1)
                    {
                        List<MST_BUSENTITY_TAX> taxstr = CHNLSVC.Finance.getCreditorTaxStructure(creditor,company,out error);
                        if (error != "")
                        {
                            return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                        }
                        if (taxstr != null && taxstr.Count == 0)
                        {
                            return Json(new { success = false, login = true, msg = "Tax rates not defined for creditor '" + creditor+"'" }, JsonRequestBehavior.AllowGet);
                        }
                        List<MST_ACC_TAX> MST_ACC_TAX = new List<MST_ACC_TAX>();
                        foreach (MST_BUSENTITY_TAX tax in taxstr)
                        {

                            MST_ACC_TAX itm = new MST_ACC_TAX();
                            itm.MAT_TAX_AMT = Math.Round((amt * tax.MBIT_TAX_RT) / 100, 2);
                            itm.MAT_ACC_NO = creditor.Trim();
                            itm.MAT_TAX_CD = tax.MBIT_TAX_RT_CD;
                            itm.MAT_TAX_RT = tax.MBIT_TAX_RT;
                            itm.MAT_CRE_BY = userId;
                            itm.MAT_CRE_DT = DateTime.Now;
                            itm.MAT_MOD_BY = userId;
                            itm.MAT_MOD_DT = DateTime.Now;
                            itm.MAT_SESSION_ID = (string)Session["SessionID"];
                            itm.MAT_STUS = 1;
                            itm.NEW_ADDED = 1;
                            itm.UPDATED = 1;
                            MST_ACC_TAX.Add(itm);
                            itm.MAT_COM = company;
                        }
                        Session["MST_ACC_TAX"] = MST_ACC_TAX;      
                        decimal totalTax = MST_ACC_TAX.Where(x=>x.MAT_STUS==1).Sum(x => x.MAT_TAX_AMT);
                        decimal netAmt = totalTax + amt;
                        return Json(new { success = true, login = true, auto = dat.RCA_ANAL5, data = MST_ACC_TAX, totalTax = totalTax, netAmt = netAmt }, JsonRequestBehavior.AllowGet);
                    }

                    return Json(new { success = true, login = true,auto= dat.RCA_ANAL5, netAmt = amt }, JsonRequestBehavior.AllowGet);

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

        public JsonResult addSelectedValues(string type, string value, string crecost,string creditor)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {

                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    if (type == "POSRC")
                    {
                        List<PUR_SELECTED> selecteditem = (List<PUR_SELECTED>)Session["PUR_SELECTED"];
                        if (selecteditem != null && selecteditem.Count > 0)
                        {
                            if (selecteditem.Where(x => x.PURNO == value).Count() > 0)
                            {
                                selecteditem.RemoveAll((x) => x.PURNO == value);
                            }
                            else
                            {
                                PUR_SELECTED aded = new PUR_SELECTED();
                                aded.PURNO = value;
                                aded.COST = Convert.ToDecimal(crecost);
                                selecteditem.Add(aded);
                            }
                        }
                        else
                        {
                            selecteditem = new List<PUR_SELECTED>();
                            PUR_SELECTED aded = new PUR_SELECTED();
                            aded.PURNO = value;
                            aded.COST = Convert.ToDecimal(crecost);
                            selecteditem.Add(aded);
                        }
                        decimal totalNBT = 0;
                        decimal totalVAT = 0;
                        foreach (PUR_SELECTED itm in selecteditem)
                        {
                            if (itm.COST != 0)
                            {
                                decimal cost = 0;
                                decimal vat = 0;
                                decimal nbt = 0;
                                string error = "";
                                Int32 ret = CHNLSVC.Finance.getPurCostAndTax(itm.PURNO, company, out cost, out vat, out nbt, out error);
                                itm.TAX = nbt + vat;
                                itm.COST = cost;
                                totalNBT += nbt;
                                totalVAT += vat;
                            }
                        }
                        List<MST_ACC_TAX> MST_ACC_TAX = new List<MST_ACC_TAX>();

                        MST_ACC_TAX taxitm = new MST_ACC_TAX();
                        taxitm.MAT_TAX_AMT = totalVAT;
                        taxitm.MAT_ACC_NO = creditor.Trim();
                        taxitm.MAT_TAX_CD = "VAT";
                        taxitm.MAT_TAX_RT = 0;
                        taxitm.MAT_CRE_BY = userId;
                        taxitm.MAT_CRE_DT = DateTime.Now;
                        taxitm.MAT_MOD_BY = userId;
                        taxitm.MAT_MOD_DT = DateTime.Now;
                        taxitm.MAT_SESSION_ID = (string)Session["SessionID"];
                        taxitm.MAT_STUS = 1;
                        taxitm.NEW_ADDED = 1;
                        taxitm.UPDATED = 0;
                        taxitm.MAT_COM = company;
                        MST_ACC_TAX.Add(taxitm);
                         
                        MST_ACC_TAX taxitm1 = new MST_ACC_TAX();
                        taxitm1.MAT_TAX_AMT = totalNBT;
                        taxitm1.MAT_ACC_NO = creditor.Trim();
                        taxitm1.MAT_TAX_CD = "NBT";
                        taxitm1.MAT_TAX_RT = 0;
                        taxitm1.MAT_CRE_BY = userId;
                        taxitm1.MAT_CRE_DT = DateTime.Now;
                        taxitm1.MAT_MOD_BY = userId;
                        taxitm1.MAT_MOD_DT = DateTime.Now;
                        taxitm1.MAT_SESSION_ID = (string)Session["SessionID"];
                        taxitm1.MAT_STUS = 1;
                        taxitm1.NEW_ADDED = 1;
                        taxitm1.UPDATED = 0;
                        taxitm1.MAT_COM = company;
                        MST_ACC_TAX.Add(taxitm1);

                        
                        Session["MST_ACC_TAX"] = MST_ACC_TAX;



                        Session["PUR_SELECTED"] = selecteditem;
                        decimal totTax = selecteditem.Sum(x => x.TAX);
                        decimal totCost = selecteditem.Sum(x => x.COST);


                        return Json(new { success = true, login = true, selecteditem = selecteditem, totTax = totTax, totCost = totCost, MST_ACC_TAX = MST_ACC_TAX }, JsonRequestBehavior.AllowGet);

                    }
                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult removeSelected(string type, string value)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    if (type == "POSRC")
                    {
                        List<PUR_SELECTED> selecteditem = (List<PUR_SELECTED>)Session["PUR_SELECTED"];
                        if (selecteditem != null && selecteditem.Count > 0)
                        {
                            if (selecteditem.Where(x => x.PURNO == value).Count() > 0)
                            {
                                selecteditem.RemoveAll((x) => x.PURNO == value);
                            }
                        }
                        decimal totTax = selecteditem.Sum(x=>x.TAX);
                        decimal totCost = selecteditem.Sum(x => x.COST);

                        return Json(new { success = true, login = true, selecteditem = selecteditem, totTax = totTax, totCost = totCost }, JsonRequestBehavior.AllowGet);
                    }
                    
                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
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