using FF.BusinessObjects.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FF.SCMWebMVC.Controllers.Account
{
    public class AccountDefinitionController : BaseController
    {
        // GET: AccountDefinition
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult save(FormCollection formData) {

            
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    string name = formData["accountcode"].ToString();
                    if (string.IsNullOrEmpty(name))
                    {
                        return Json(new { success = false, login = true, msg = "Please select account number." }, JsonRequestBehavior.AllowGet);
                    }
                    TemplateManagerController mngrcnt=new TemplateManagerController();
                    List<REF_TMPLT_VALUE> totValue = getDynamicSaveVaAddedlues(formData, name, userId, company);
                    string error = "";
                    Int32 eff = CHNLSVC.Finance.saveAccountDefinitionData(name, totValue, out error);
                    if (error != "")
                    {
                        return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    return Json(new { success = false, login = true, msg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);

                }
            }
        }
        public List<REF_TMPLT_VALUE> getDynamicSaveVaAddedlues(FormCollection formData, string uniqcd, string userId,string company)
        {
            List<REF_OBJ_TEMPITMFRMSELECT> addedid = new List<REF_OBJ_TEMPITMFRMSELECT>();
            if (Session["FROM_ADDED_TEMPLATE"] != null)
            {
                addedid = (List<REF_OBJ_TEMPITMFRMSELECT>)Session["FROM_ADDED_TEMPLATE"];
            }
            List<REF_TMPLT_VALUE> totValue = new List<REF_TMPLT_VALUE>();
            if (addedid.Count > 0)
            {
                foreach (REF_OBJ_TEMPITMFRMSELECT item in addedid)
                {
                    REF_TMPLT_VALUE itemvalue = new REF_TMPLT_VALUE();
                    itemvalue.RTV_HED_ID = item.TEMPLATE_ID;
                    itemvalue.RTV_DET_ID = item.DETAIL_ID;
                    itemvalue.RTV_CRE_BY = userId;
                    itemvalue.RTV_COM = company;
                    //itemvalue.RTV_VALUE = (formData["name_" + item.TEMPLATE_ID + "_" + item.DETAIL_ID].ToString()).ToString();
                    totValue.Add(itemvalue);
                }
            }
            return totValue;
        }

       
    }
}