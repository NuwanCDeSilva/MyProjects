using FF.BusinessObjects.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FF.SCMWebMVC.Controllers.Account
{
    public class TemplateManagerController : BaseController
    {

        public JsonResult getTemplateHeaderDetails(bool withdeleted, int pgeNum, int pgeSize, string searchFld, string searchVal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    string error = "";
                    List<REF_TMPLT_HED> list = CHNLSVC.Finance.getTemplateHeaderDetails(withdeleted, pgeNum, pgeSize, searchFld, searchVal, out error);
                    if (error != "")
                    {
                        return Json(new { success = false, login = true,msg = error }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (list.Count > 0)
                        {
                            decimal totalDoc = Math.Ceiling(Convert.ToInt32(list[0].RESULT_COUNT) / Convert.ToDecimal(pgeSize));
                            return Json(new { success = true, login = true, data = list, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            decimal totalDoc = 0;
                            return Json(new { success = true, login = true, data = list, totalDoc = totalDoc }, JsonRequestBehavior.AllowGet);
                        }
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
                { return Json(new { success = true, login = false }, JsonRequestBehavior.AllowGet);

                }
            }
        }
        public JsonResult updateStusTemplateHeader(Int32 hedid, Int32 stus)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    string error = "";
                    Int32 eff = CHNLSVC.Finance.updateStusTemplateHeader(hedid, stus,userId, out error);
                    if (error == "")
                    {
                        return Json(new { success = true, login = true,msg="Successfully update template" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true,msg = error }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getTemplateHedDet(Int32 hedid,bool readony)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    string error = "";
                    if (!readony)
                    {
                        Int32 cnt =CHNLSVC.Finance.getTepmplateUsedCount( hedid,out error);
                        if (cnt > 0)
                        {
                            if (error != "")
                            {
                                return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);

                            }
                            return Json(new { success = false, login = true, msg = "Already used template cannot edit." }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    REF_TMPLT_HED res = CHNLSVC.Finance.getTemplateHedDet(hedid, out error);
                    if (error == "")
                    {
                        List<REF_TMP_OBJHEDDET> det = CHNLSVC.Finance.getTemplateHedItemDet(hedid, out error);
                        if (error == "")
                        {
                            return Json(new { success = true, login = true, data = res,det = det }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getComboDetails(Int32 detid)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    string error = "";
                    List<REF_OBJ_LIST_DET> res = CHNLSVC.Finance.getTemplateComboDet(detid, out error);
                    if (error == "")
                    {
                        return Json(new { success = true, login = true, data = res }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
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
        public JsonResult deleteFieldItem(Int32 detid)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    string error = "";
                   Int32 eff= CHNLSVC.Finance.updateItemListStatus(detid,userId, out error);
                    if (error == "")
                    {
                        return Json(new { success = true, login = true, msg = "Successfully deleted template item." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getAllTemplateItemDetails()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    string error = "";
                    List<REF_OBJ> res = CHNLSVC.Finance.getAllTemplateItemDetails(out error);
                    if (error == "")
                    {
                        return Json(new { success = true, login = true, det = res }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getComboDetailsItmId(Int32 itmid)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    string error = "";
                    List<REF_OBJ_LIST_DET> res = CHNLSVC.Finance.getTemplateComboDetItmDet(itmid, out error);
                    if (error == "")
                    {
                        return Json(new { success = true, login = true, data = res }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
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
        public JsonResult addTemplateField(Int32 hedid,Int32 detid,string name)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    name = name.Trim();
                    if (name == "")
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid name." }, JsonRequestBehavior.AllowGet);
                    }
                    string error = "";
                    Int32 eff = CHNLSVC.Finance.addTemplateField(hedid, detid, name,userId, out error);
                    if (error == "")
                    {
                        return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
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
        public JsonResult updateLabelText(string itmid, string name)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    name = name.Trim();
                    if (name == "")
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid name." }, JsonRequestBehavior.AllowGet);
                    }
                    string error = "";
                    Int32 id = Convert.ToInt32(itmid.Split('_')[1]);
                    Int32 eff = CHNLSVC.Finance.updateLabelText(id, name, userId, out error);
                    if (error == "")
                    {
                        return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
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
        public JsonResult addNewTempHdrItem(string name,Int32 detid,string type) {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    name = name.Trim();
                    if (name == "")
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid name." }, JsonRequestBehavior.AllowGet);
                    }
                    List<REF_TMPLT_DET_SIN> det = new List<REF_TMPLT_DET_SIN>();
                    if (Session["REF_TMPLT_DET_SIN"] != null)
                    {
                        det = (List<REF_TMPLT_DET_SIN>)Session["REF_TMPLT_DET_SIN"];
                    }
                    REF_TMPLT_DET_SIN itm = new REF_TMPLT_DET_SIN();
                    Int32 max = 1;
                    if (det.Count > 0)
                    {
                        max = det.Max(x => x.RTD_ID) + 1;
                    }
                    itm.RTD_OBJ_ID = detid;
                    itm.RTD_ID= max;
                    itm.RTD_NAME = name;
                    itm.RTD_CRE_BY = userId;
                    itm.RTD_TYPE = type;
                    itm.RTD_IS_VALUE = 0;
                    det.Add(itm);

                    Session["REF_TMPLT_DET_SIN"] = det;
                    return Json(new { success = true, login = true, det = det }, JsonRequestBehavior.AllowGet);
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
       

        public JsonResult createNewTemplate(string name)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    name = name.Trim();
                    if (name == "")
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid name." }, JsonRequestBehavior.AllowGet);
                    }
                    List<REF_TMPLT_DET_SIN> det = new List<REF_TMPLT_DET_SIN>();
                    if (Session["REF_TMPLT_DET_SIN"] != null)
                    {
                        det = (List<REF_TMPLT_DET_SIN>)Session["REF_TMPLT_DET_SIN"];
                    }
                    if(det.Count==0){
                        return Json(new { success = false, login = true, msg = "Please add template fields." }, JsonRequestBehavior.AllowGet);
                    }
                    List<REF_TMPLT_DET_SIN> selected = det.Where(x => x.RTD_IS_VALUE == 1).ToList();
                    if (selected.Count == 0)
                    {
                        return Json(new { success = false, login = true, msg = "Please add number field as default value." }, JsonRequestBehavior.AllowGet);

                    }

                    string error = "";
                    Int32 eff = CHNLSVC.Finance.createNewTemplate(name, userId, det, out error);
                    if (error != "")
                    {
                        return Json(new { success = false, login = true,msg=error }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Session["REF_TMPLT_DET_SIN"] = null;
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
        public JsonResult clearSession() {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    Session["REF_TMPLT_DET_SIN"] = null;
                    Session["FROM_ADDED_TEMPLATE"] = null;
                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
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
        public JsonResult deleteTempHdrItmNew(Int32 detid,string name)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    List<REF_TMPLT_DET_SIN> det = new List<REF_TMPLT_DET_SIN>();
                    if (Session["REF_TMPLT_DET_SIN"] != null)
                    {
                        det = (List<REF_TMPLT_DET_SIN>)Session["REF_TMPLT_DET_SIN"];
                    }
                    if (det.Count > 0)
                    {
                        List<REF_TMPLT_DET_SIN> newdet = new List<REF_TMPLT_DET_SIN>();
                        bool param = false;
                        foreach (REF_TMPLT_DET_SIN itm in det)
                        {
                            if (param == false)
                            {
                                if (itm.RTD_OBJ_ID == detid && itm.RTD_NAME == name)
                                {
                                    param = true;
                                }
                                else
                                {
                                    newdet.Add(itm);
                                }
                            }
                            else
                            {
                                newdet.Add(itm);
                            }
                        }
                        Session["REF_TMPLT_DET_SIN"] = newdet;
                    }
                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getTemplateHedDetForForm(Int32 hedid)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    string error = "";
                    List<REF_OBJ_TEMPITMFRM> res= CHNLSVC.Finance.getTemplateDetailtoFrom(hedid, out error);
                    if (error == "")
                    {
                        if (res.Count > 0)
                        {
                            bool has1 = res.Exists(x => x.DEF_VAL_FLD == 1);
                            if (!has1)
                            {
                                return Json(new { success = false, login = true, msg = "Selected template does not have any value related field." }, JsonRequestBehavior.AllowGet); 

                            }
                            if (res[0].DETAIL_ID != 0)
                            {
                                List<REF_OBJ_TEMPITMFRMSELECT> addedid = new List<REF_OBJ_TEMPITMFRMSELECT>();
                                if(Session["FROM_ADDED_TEMPLATE"]!=null){
                                    addedid = (List<REF_OBJ_TEMPITMFRMSELECT>)Session["FROM_ADDED_TEMPLATE"];
                                }
                                bool has = addedid.Exists(x=>x.TEMPLATE_ID==hedid);
                                if (has)
                                {
                                    return Json(new { success = false, login = true, msg = "Template already added to the form.Please select different template." }, JsonRequestBehavior.AllowGet); 
                                }
                                if (addedid.Count > 0)
                                {
                                    return Json(new { success = false, login = true, msg = "Cannot add multiple template to one account." }, JsonRequestBehavior.AllowGet); 

                                }
                                foreach (REF_OBJ_TEMPITMFRM it in res)
                                {
                                    REF_OBJ_TEMPITMFRMSELECT mi = new REF_OBJ_TEMPITMFRMSELECT();
                                    mi.TEMPLATE_ID = hedid;
                                    mi.DETAIL_ID = it.DETAIL_ID;
                                    addedid.Add(mi);
                                }
                                Session["FROM_ADDED_TEMPLATE"] = addedid;
                                return Json(new { success = true, login = true, det = res }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = "Please add template item for adding to form." }, JsonRequestBehavior.AllowGet); 
                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Invalid template(Please check template items)." }, JsonRequestBehavior.AllowGet);
                        }
                       
                       
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
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
        public JsonResult removeAddedTemplate(string hedid, string assigncode)
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    string error = "";
                    
                    List<REF_OBJ_TEMPITMFRMSELECT> addedid = new List<REF_OBJ_TEMPITMFRMSELECT>();
                    if (Session["FROM_ADDED_TEMPLATE"] != null)
                    {
                        addedid = (List<REF_OBJ_TEMPITMFRMSELECT>)Session["FROM_ADDED_TEMPLATE"];
                        if (addedid.Count > 0)
                        {
                            string[] ids = hedid.Split('_');
                            Int32 hed = Convert.ToInt32(ids[1]);
                            Int32 eff = 0;
                            Int32 cnt = CHNLSVC.Finance.getTepmplateUsedCount(hed, out error);
                            if (cnt > 0)
                            {
                                if (error != "")
                                {
                                    return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);

                                }
                                return Json(new { success = false, login = true, msg = "Already used template cannot edit." }, JsonRequestBehavior.AllowGet);
                            }
                            if (assigncode != null && assigncode!="")
                            {
                                eff = CHNLSVC.Finance.removeDocumentTepllate(hed, assigncode,userId,out error);
                            }
                            if (error!="")
                            {
                                return Json(new { success = false, login = true,msg = error }, JsonRequestBehavior.AllowGet);
                               
                            }
                            bool has = addedid.Exists(x => x.TEMPLATE_ID == hed);
                            if (has)
                            {
                                addedid.RemoveAll(x => x.TEMPLATE_ID == hed);
                                return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "Invalid template." }, JsonRequestBehavior.AllowGet);

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
        public JsonResult getItemSavedValues(string code)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    Session["REF_TMPLT_DET_SIN"] = null;
                    Session["FROM_ADDED_TEMPLATE"] = null;
                    string error = "";
                    List<REF_OBJ_TEMPITMFRM> frmitm = CHNLSVC.Finance.getTeplateSavedData(code, company, out error);
                    List<TEMPLATE_HED_DET> itemList = new List<TEMPLATE_HED_DET>();
                    if (error != "")
                    {
                        return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                    }
                    if (frmitm.Count > 0)
                    {
                       

                        List<REF_OBJ_TEMPITMFRM> newfrmitm = frmitm.GroupBy(c => new { c.TEMPLATE_ID, c.TEMPLATE_NAME })
                         .Select(cl => new REF_OBJ_TEMPITMFRM
                         {
                             TEMPLATE_ID = cl.First().TEMPLATE_ID,
                             TEMPLATE_NAME = cl.First().TEMPLATE_NAME,
                             

                         }).ToList();

                        List<REF_OBJ_TEMPITMFRMSELECT> selected = new List<REF_OBJ_TEMPITMFRMSELECT>();

                        
                        foreach (REF_OBJ_TEMPITMFRM heddet in newfrmitm)
                        {
                            TEMPLATE_HED_DET im = new TEMPLATE_HED_DET();
                            im.TEMPLATE_ID = heddet.TEMPLATE_ID;
                            im.TEMPLATE_NAME = heddet.TEMPLATE_NAME;

                            im.TEMPLATE_DET = new List<TEMPLATE_ITM_DET>();
                            List<REF_OBJ_TEMPITMFRM> tempitmlst = frmitm.Where(x => x.TEMPLATE_ID == heddet.TEMPLATE_ID && x.TEMPLATE_NAME == heddet.TEMPLATE_NAME).ToList();
                            if (tempitmlst.Count > 0)
                            {
                                foreach (REF_OBJ_TEMPITMFRM ne in tempitmlst)
                                {
                                    TEMPLATE_ITM_DET i = new TEMPLATE_ITM_DET();
                                    i.DETAIL_ID = ne.DETAIL_ID;
                                    i.ITEMOBJ_ID = ne.ITEMOBJ_ID;
                                    i.FIELD_TYPE=ne.FIELD_TYPE;
                                    i.IS_HAVE_SEARCH =ne.IS_HAVE_SEARCH;
                                    i.FIELD_LENGTH =ne.FIELD_LENGTH;
                                    i.FIELD_NAME=ne.FIELD_NAME;
                                    i.SAVED_CODE = ne.SAVED_CODE;
                                    i.IS_HAVE_SEARCH = ne.IS_HAVE_SEARCH;
                                    i.SEARCH_FLD = ne.SEARCH_FLD;
                                    i.DEF_VAL_FLD = ne.DEF_VAL_FLD;
                                    im.TEMPLATE_DET.Add(i);

                                    //added selected templates
                                    REF_OBJ_TEMPITMFRMSELECT sel = new REF_OBJ_TEMPITMFRMSELECT();
                                    sel.TEMPLATE_ID = heddet.TEMPLATE_ID;
                                    sel.DETAIL_ID = i.DETAIL_ID;
                                    selected.Add(sel);
                                }
                            }
                            itemList.Add(im);
                        }
                        Session["FROM_ADDED_TEMPLATE"] = selected;
                        return Json(new { success = true, login = true, det = itemList }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = true, login = true ,dat=""}, JsonRequestBehavior.AllowGet);
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
        public JsonResult getSavedItemTemplates(string module,string code,string savedVavl=null)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    string error = "";
                    List<REF_OBJ_TEMPITMFRM> item = CHNLSVC.Finance.getSavedItemTemplateVal(module, code, company, savedVavl, out error);
                    List<FORM_TMPLT_VALUE> itemList = new List<FORM_TMPLT_VALUE>();
                    if (error == "")
                    {
                       
                        if (item.Count > 0)
                        {

                            List<REF_OBJ_TEMPITMFRM> newfrmitm1 = item.GroupBy(c => new { c.SEQ, c.MODULE })
                         .Select(cl => new REF_OBJ_TEMPITMFRM
                         {
                             SEQ = cl.First().SEQ,
                             MODULE = cl.First().MODULE,
                         }).ToList();
                            
                            foreach(REF_OBJ_TEMPITMFRM frmitmnw in  newfrmitm1 ){
                                FORM_TMPLT_VALUE finItm = new FORM_TMPLT_VALUE();
                                finItm.SEQ = frmitmnw.SEQ;
                                finItm.MODULE = frmitmnw.MODULE;
                                finItm.UNQ_CD = code;
                                finItm.ITEM = new List<TEMPLATE_HED_DET>();

                                List<REF_OBJ_TEMPITMFRM> frmitm = item.Where(x => x.SEQ == frmitmnw.SEQ && x.MODULE == frmitmnw.MODULE).ToList();


                                List<REF_OBJ_TEMPITMFRM> newfrmitm = frmitm.GroupBy(c => new { c.TEMPLATE_ID, c.TEMPLATE_NAME })
                         .Select(cl => new REF_OBJ_TEMPITMFRM
                         {
                             TEMPLATE_ID = cl.First().TEMPLATE_ID,
                             TEMPLATE_NAME = cl.First().TEMPLATE_NAME,


                         }).ToList();

                                List<REF_OBJ_TEMPITMFRMSELECT> selected = new List<REF_OBJ_TEMPITMFRMSELECT>();


                                foreach (REF_OBJ_TEMPITMFRM heddet in newfrmitm)
                                {
                                    TEMPLATE_HED_DET im = new TEMPLATE_HED_DET();
                                    im.TEMPLATE_ID = heddet.TEMPLATE_ID;
                                    im.TEMPLATE_NAME = heddet.TEMPLATE_NAME;

                                    im.TEMPLATE_DET = new List<TEMPLATE_ITM_DET>();
                                    List<REF_OBJ_TEMPITMFRM> tempitmlst = frmitm.Where(x => x.TEMPLATE_ID == heddet.TEMPLATE_ID && x.TEMPLATE_NAME == heddet.TEMPLATE_NAME).ToList();
                                    if (tempitmlst.Count > 0)
                                    {
                                        foreach (REF_OBJ_TEMPITMFRM ne in tempitmlst)
                                        {
                                            TEMPLATE_ITM_DET i = new TEMPLATE_ITM_DET();
                                            i.DETAIL_ID = ne.DETAIL_ID;
                                            i.ITEMOBJ_ID = ne.ITEMOBJ_ID;
                                            i.FIELD_TYPE = ne.FIELD_TYPE;
                                            i.IS_HAVE_SEARCH = ne.IS_HAVE_SEARCH;
                                            i.FIELD_LENGTH = ne.FIELD_LENGTH;
                                            i.FIELD_NAME = ne.FIELD_NAME;
                                            i.SAVED_CODE = ne.SAVED_CODE;
                                            i.FIELD_VALUE = ne.SAVED_VALUE;
                                            i.DEF_VAL_FLD = ne.DEF_VAL_FLD;
                                            i.SEARCH_FLD = ne.SEARCH_FLD;
                                            i.HAS_SEARCH = ne.HAS_SEARCH;
                                            im.TEMPLATE_DET.Add(i);

                                            //added selected templates
                                            REF_OBJ_TEMPITMFRMSELECT sel = new REF_OBJ_TEMPITMFRMSELECT();
                                            sel.TEMPLATE_ID = heddet.TEMPLATE_ID;
                                            sel.DETAIL_ID = i.DETAIL_ID;
                                            selected.Add(sel);
                                        }
                                    }
                                    finItm.ITEM.Add(im);
                                }
                                itemList.Add(finItm);
                            }
                            
                        }
                        Session["TEMP_VAL_DET"] = itemList;
                        return Json(new { success = true, login = true, itemList = itemList }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg=error }, JsonRequestBehavior.AllowGet);

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
        public JsonResult getNextTemplateField(string module, string code, Int32 seq,Int32 direction)
        { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    if (Session["TEMP_VAL_DET"] != null)
                    {
                        
                        List<FORM_TMPLT_VALUE> itemDet = (List<FORM_TMPLT_VALUE>)Session["TEMP_VAL_DET"];
                        List<FORM_TMPLT_VALUE> response = new List<FORM_TMPLT_VALUE>();
                        itemDet = itemDet.OrderBy(x => x.SEQ).ToList();
                        if (itemDet.Count > 0)
                        {
                            int previous = 0;
                            int nextValue = 0;
                            if (seq == 0)
                            {
                                Int32 minNumber = itemDet.Min(y => y.SEQ);

                                previous = 0;
                                try
                                {
                                    nextValue = itemDet[1].SEQ;
                                }
                                catch (Exception e)
                                { 
                                }
                                response = itemDet.Where(x => x.SEQ == minNumber && x.UNQ_CD == code && x.MODULE == module).OrderBy(y => y.SEQ).ToList();
                                return Json(new { success = true, login = true, data = response[0].ITEM, previous = previous, nextValue = nextValue }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (direction == -1)
                                {

                                    int index = itemDet.FindIndex(x => x.SEQ == seq);
                                    previous = (index >=1) ? itemDet[index - 1].SEQ :0;
                                    try
                                    {
                                        if (previous == 0)
                                        {
                                            nextValue = itemDet[1].SEQ;
                                        }
                                        else
                                        {
                                            nextValue = itemDet[index+1].SEQ;
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                    }
                                    response = itemDet.Where(x => x.SEQ == seq && x.UNQ_CD == code && x.MODULE == module).OrderBy(y => y.SEQ).ToList();
                                    return Json(new { success = true, login = true, data = response[0].ITEM, previous = previous, nextValue = nextValue }, JsonRequestBehavior.AllowGet);
                                
                                }
                                else
                                {
                                    int index = itemDet.FindIndex(x => x.SEQ == seq);
                                    previous = itemDet[index - 1].SEQ;
                                    try
                                    {
                                        nextValue = itemDet[index + 1].SEQ;
                                    }
                                    catch (Exception e)
                                    { 
                                    }
                                    response = itemDet.Where(x => x.SEQ == seq && x.UNQ_CD == code && x.MODULE == module).OrderBy(y => y.SEQ).ToList();
                                    return Json(new { success = true, login = true, data = response[0].ITEM, previous = previous, nextValue = nextValue }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
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
        public JsonResult updateDefaultTemplateNumberField(string detid,bool val,bool update=false)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    if (!update)
                    {
                        List<REF_TMPLT_DET_SIN> det = new List<REF_TMPLT_DET_SIN>();
                        if (Session["REF_TMPLT_DET_SIN"] != null)
                        {
                            det = (List<REF_TMPLT_DET_SIN>)Session["REF_TMPLT_DET_SIN"];
                        }
                        foreach (REF_TMPLT_DET_SIN rw in det)
                        {
                            if (rw.RTD_ID == Convert.ToInt32(detid) )
                            {
                                rw.RTD_IS_VALUE = (val) ? 1 : 0;
                            }
                        }
                        Session["REF_TMPLT_DET_SIN"] = det;
                    }
                    else
                    {
                        string error="";
                        Int32 up = CHNLSVC.Finance.updateTempFiledDefNumber(Convert.ToInt32(detid), val, userId, out error);
                        if (error != "")
                        {
                            return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
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
        public JsonResult addSearchForField(string objid, string detid, string val,bool update=false)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    if (!update)
                    {
                        List<REF_TMPLT_DET_SIN> det = new List<REF_TMPLT_DET_SIN>();
                        if (Session["REF_TMPLT_DET_SIN"] != null)
                        {
                            det = (List<REF_TMPLT_DET_SIN>)Session["REF_TMPLT_DET_SIN"];
                        }
                        foreach (REF_TMPLT_DET_SIN itm in det)
                        {
                            if (itm.RTD_OBJ_ID == Convert.ToInt32(objid) && itm.RTD_ID == Convert.ToInt32(detid))
                            {
                                itm.RTD_SRCH_FLD = val;
                            }
                        }

                        Session["REF_TMPLT_DET_SIN"] = det;
                        return Json(new { success = true, login = true, det = det }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        string error = "";
                        Int32 up = CHNLSVC.Finance.updateTempFiledSrch(Convert.ToInt32(detid), val, userId, out error);
                        if (error != "")
                        {
                            return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                        }
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
        public JsonResult checkDefaultFieldAdd(Int32 tempId)
        { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    string error = "";
                    List<REF_TMP_OBJHEDDET> det = CHNLSVC.Finance.getTemplateHedItemDet(tempId, out error);
                    if (error == "")
                    {
                        List<REF_TMP_OBJHEDDET> itm = det.Where(x => x.RTD_IS_VALUE == 1).ToList();
                        if (itm.Count > 0)
                        {
                            return Json(new { success = true, login = true}, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Please add one number field as value field." }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
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
    }
}