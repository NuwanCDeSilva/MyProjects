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
    public class FeedbackController : BaseController
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
                ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1027);
                if (per.MNU_ID == 0)
                {
                    throw new AuthenticationException("You do not have the necessary permission to perform this action");
                }
            }
            else
            {
                Redirect("~/Login/Index");
            }

        }
        // GET: Feedback
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string channel = HttpContext.Session["UserChannl"] as string;
                List<ST_SATIS_QUEST> Ques = CHNLSVC.Tours.getFeedBackQuestions(channel, company, userDefPro);
                return View(Ques);
            }
            else
            {
                return Redirect("~/Login/Index");
            }
        }
        [HttpPost]
        public JsonResult SaveFeedback(FormCollection data)
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
                    string channel = HttpContext.Session["UserChannl"] as string;
                    List<ST_SATIS_QUEST> question=CHNLSVC.Tours.getFeedBackQuestionsOnly(channel, company, userDefPro);
                    if (question.Count > 0)
                    {
                        if (data.Count == question.Count+1)
                        {
                            if (data["EnqId"].ToString() != "")
                            {
                                List<ST_SATIS_RESULT> AllData = new List<ST_SATIS_RESULT>();
                                int i = 1;
                                foreach (ST_SATIS_QUEST one in question)
                                {
                                    ST_SATIS_RESULT oneQ = new ST_SATIS_RESULT();
                                    oneQ.SSVL_ENQID = data["EnqId"].ToString();
                                    oneQ.SSVL_QSTSEQ = one.SSQ_SEQ;
                                    oneQ.SSVL_CRE_BY = userId;
                                    oneQ.SSVL_CRE_DT = DateTime.Now.Date;
                                    oneQ.SSVL_CHNL = channel;
                                    oneQ.SSVL_COM = company;
                                    oneQ.SSVL_PC = userDefPro;
                                    if (one.SSQ_TYPE == "Radio")
                                    {
                                        string[] ids = data[i].ToString().Split('-');
                                        Int32 value = Convert.ToInt32(ids[1]);
                                        oneQ.SSVL_VALSEQ = value;
                                        oneQ.SSVL_ANS = ids[2];

                                    }
                                    else if (one.SSQ_TYPE == "Text")
                                    {
                                        oneQ.SSVL_ANS =(data[i]!=null)? data[i].ToString():"";
                                    }
                                    else {
                                        oneQ.SSVL_ANS = "";
                                    }
                                    AllData.Add(oneQ);
                                    i++;
                                }
                                Int32 result = CHNLSVC.Tours.SaveCustomerFeedBack(AllData,out error);
                                if (result == 1)
                                {
                                    return Json(new { success = true, login = true,msg="Feedback added successful.",type="Success" }, JsonRequestBehavior.AllowGet);
                                }
                                else {
                                    return Json(new { success = false, login = true, msg = error, type = "Info" }, JsonRequestBehavior.AllowGet);
                                }
                                
                            }
                            else {
                                return Json(new { success = false, login = true ,msg="Please add enquiry id.",type="Info"}, JsonRequestBehavior.AllowGet);
                            }
                            
                        }
                        else {
                            return Json(new { success = false, login = true, msg = "Please fill all fields.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
                        return Json(new { success = false, login = true,msg="No quetion for feedback.",type="Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError,type="Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getEnquiryFeedback(string enqId) { 
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
                        GEN_CUST_ENQ oItem = CHNLSVC.Tours.getEnquiryDetails(company, userDefPro, enqId);
                        if (oItem != null)
                        {
                            string channel = HttpContext.Session["UserChannl"] as string;
                            List<ST_SATIS_RESULT> feedData = new List<ST_SATIS_RESULT>();
                            feedData = CHNLSVC.Tours.getCustermerFeedData(channel,company,userDefPro,enqId);
                            if (feedData.Count > 0)
                            {
                                return Json(new { success = true, login = true, feedData = feedData }, JsonRequestBehavior.AllowGet);
                            }
                            else {
                                return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else {
                            return Json(new { success = false, login = true, msg = "Invalid enquiry id.", type = "Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
                        return Json(new { success = false, login = true,msg="Please enter enquiry id.",type="Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = Resource.ServerError,type="Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        List<QUO_COST_DET> oMainItems;
        QUO_COST_HDR oHeader;
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
    }
}