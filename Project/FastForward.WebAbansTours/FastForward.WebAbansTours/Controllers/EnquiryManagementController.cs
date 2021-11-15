using FF.BusinessObjects;
using FF.BusinessObjects.Tours;
using FF.BusinessObjects.Services;
using FF.BusinessObjects.ToursNew;
using FF.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Microsoft.Office.Interop.Excel;
using System.Web.Routing;
using System.Security.Authentication;

namespace FastForward.WebAbansTours.Controllers
{
    public class EnquiryManagementController : BaseController
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
                ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1016);
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
        // GET: EnquiryManagement

        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                Session["closeBtn"] = "true";
                string Status = "1";
                List<GEN_CUST_ENQ> oItems = CHNLSVC.Tours.GET_ENQRY_BY_PC_STATUS(company, userDefPro, Status, userId, 15001);
                ViewBag.pendingList = oItems;
                string EnqCre = HttpContext.Session["EnqCre"] as string;

                if (EnqCre == "5")
                {
                    var endate = oItems.Max(a => a.GCE_CRE_DT);
                    var enqno = oItems.Where(x => x.GCE_CRE_DT == endate).Max(x => x.GCE_ENQ_ID);
                    ViewBag.enqno = enqno;
                    Session["EnqCre"] = "0";
                }
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }


        }

        //public JsonResult GetEnqData(string id)
        //{


        //    return Json();
        //}
        public JsonResult GetEnqData(string id, int status, string reason, string cuscd, string mobile, string nic, string name, string add1, string add2, string email, string gcuscd, string gname, string gmob, string gemail, string flyno, DateTime flydate)
        {
            List<GEN_CUST_ENQSER> enqserdata = CHNLSVC.Tours.GetEnqSerData(id);

            return Json(new { success = true, login = true, id = id, status = status, reason = reason, cuscd = cuscd, mobile = mobile, nic = nic, name = name, add1 = add1, add2 = add2, email = email, gcuscd = gcuscd, gname = gname, gmob = gmob, gemail = gemail, flyno = flyno,flydate=flydate, data = enqserdata }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult SaveToursEnqData(GEN_CUST_ENQ _cus_enq, MasterAutoNumber m, GEN_CUST_ENQSER _cus_enq_ser)
        //{
        //    string userId = HttpContext.Session["UserID"] as string;
        //    string company = HttpContext.Session["UserCompanyCode"] as string;
        //    string userDefPro = HttpContext.Session["UserDefProf"] as string;
        //    string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
        //    string err;
        //    if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
        //    {
        //        _cus_enq_ser.GCS_SERVICE = Request["GCS_SERVICE"].ToString();
        //        _cus_enq_ser.GCS_FAC = Request["GCS_FAC"].ToString();
        //        _cus_enq_ser.GCS_SER_PROVIDER = Request["GCS_SER_PROVIDER"].ToString();
        //        _cus_enq_ser.GCS_UNITS = Convert.ToInt32(Request["GCS_UNITS"]);
        //        _cus_enq_ser.GCS_COMMENT = Request["GCS_COMMENT"].ToString();
        //        _cus_enq_ser.GCS_PICK_FRM = Request["GCS_PICK_FRM"].ToString();
        //        _cus_enq_ser.GCS_PICK_TN = Request["GCS_PICK_TN"].ToString();

        //        if (Request["GCS_EXP_DT"].ToString() != "") _cus_enq_ser.GCS_EXP_DT = Convert.ToDateTime(Request["GCS_EXP_DT"].ToString());
        //        if (Request["GCS_EXP_TIME"].ToString() != "") _cus_enq_ser.GCS_EXP_TIME = Convert.ToDateTime(Request["GCS_EXP_TIME"].ToString());
        //        if (Request["GCS_DROP_DT"].ToString() != "") _cus_enq_ser.GCS_DROP_DT = Convert.ToDateTime(Request["GCS_DROP_DT"].ToString());
        //        if (Request["GCS_DROP_TIME"].ToString() != "") _cus_enq_ser.GCS_DROP_TIME = Convert.ToDateTime(Request["GCS_DROP_TIME"].ToString());

        //        _cus_enq_ser.GCS_DROP = Request["GCS_DROP"].ToString();
        //        _cus_enq_ser.GCS_DROP_TN = Request["GCS_DROP_TN"].ToString();


        //        _cus_enq_ser.GCS_SER_COM = company;
        //        _cus_enq_ser.GCS_SER_PC = userDefPro;
        //        _cus_enq_ser.GCS_CRE_BY = userId;
        //        _cus_enq_ser.GCS_CRE_DT = DateTime.Now;
        //        _cus_enq_ser.GCS_STATUS = 1;



        //        m.Aut_cate_cd = company;
        //        m.Aut_cate_tp = "PC";
        //        m.Aut_direction = null;
        //        m.Aut_modify_dt = null;
        //        m.Aut_moduleid = "AT";
        //        m.Aut_number = 0;
        //        m.Aut_start_char = "ATMN";
        //        m.Aut_year = DateTime.Today.Year;


        //        _cus_enq.GCE_COM = company;
        //        _cus_enq.GCE_CRE_BY = userId;
        //        _cus_enq.GCE_CRE_DT = DateTime.Today;
        //        _cus_enq.GCE_ENQ_TP = _cus_enq_ser.GCS_FAC;
        //        _cus_enq.GCE_PC = userDefPro;
        //        _cus_enq.GCE_DT = DateTime.Today;
        //        _cus_enq.GCE_ENQ = _cus_enq_ser.GCS_COMMENT;
        //        _cus_enq.GCE_ENQ_COM = company;
        //        _cus_enq.GCE_PC = userDefPro;
        //        _cus_enq.GCE_STUS = 1;
        //        _cus_enq.GCE_FRM_TN = _cus_enq_ser.GCS_PICK_TN;
        //        _cus_enq.GCE_TO_TN = _cus_enq_ser.GCS_DROP_TN;
        //        _cus_enq.GCE_FRM_ADD = _cus_enq_ser.GCS_PICK_FRM;
        //        _cus_enq.GCE_TO_ADD = _cus_enq_ser.GCS_DROP;
        //        _cus_enq.GCE_RET_DT = _cus_enq_ser.GCS_DROP_DT;

        //        List<MST_COUNTRY_SEARCH> oItems = CHNLSVC.ComSearch.getCountry("1", "10", "", "");
        //        var countrycode = oItems.Where(x => x.MCU_DESC == _cus_enq.GCE_FRM_CONTRY).Max(y => y.MCU_CD);

               

        //        Int32 effect = CHNLSVC.Tours.Save_ENQ_DATA(_cus_enq, m, _cus_enq_ser, out err, countrycode, userDefPro);
        //        Session["EnqCre"] = "5";
        //        return Redirect("Index");
        //    }
        //    else
        //    {
        //        return Redirect("~/Login/index");
        //    }


        //}

        public ActionResult UpdateEnq()
        {
            string enqid = Request["enqid"];
            string status = Request["status"];
            string reason = Request["reason"];

            Int32 effect = CHNLSVC.Tours.UPDATE_ENQ_STATUS_WITH_REASON(enqid, reason);

            return RedirectToAction("Index");
        }

        public JsonResult RequiredType(string fac)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_ENQSUBTP> domains = CHNLSVC.Tours.GET_ENQRY_SUB_TP(new MST_ENQSUBTP()
                    {
                        MEST_COM = company,
                        MEST_TPCD = fac
                    });
                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (domains.Count > 0)
                    {

                        foreach (var list in domains)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list.MEST_DESC;
                            o1.Value = list.MEST_STPCD;
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

        public JsonResult Facility()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_ENQTP> oEnqType = CHNLSVC.Tours.GET_ENQUIRY_TYPE(Session["UserCompanyCode"].ToString());
                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (oEnqType.Count > 0)
                    {

                        foreach (var list in oEnqType)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list.MET_DESC;
                            o1.Value = list.MET_CD;
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
        public JsonResult SaveToursEnqDataNew(GEN_CUST_ENQ _cus_enq, MasterAutoNumber m, GEN_CUST_ENQSER _cus_enq_ser)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string err;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                if (_cus_enq.GCE_CUS_CD == "") {
                    return Json(new { success = false, login = true, msg = "Please enter customer code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_cus_enq.GCE_NO_PASS.ToString() == "")
                {
                    return Json(new { success = false, login = true, msg = "Please enter number of pax.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                List<MST_COUNTRY> countryfrm = CHNLSVC.Tours.getCountryDetails(_cus_enq.GCE_FRM_CONTRY);
                if (countryfrm.Count == 0)
                {
                    return Json(new { success = false, login = true, msg = "Invalid from country.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                List<MST_COUNTRY> countryto = CHNLSVC.Tours.getCountryDetails(_cus_enq.GCE_DEST_CONTRY);
                if (countryto.Count == 0)
                {
                    return Json(new { success = false, login = true, msg = "Invalid to country.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }

                _cus_enq_ser.GCS_COMMENT = Request["GCS_COMMENT"].ToString();

                if (_cus_enq_ser.GCS_COMMENT == "")
                {
                    return Json(new { success = false, login = true, msg = "Please enter Comment.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                string EnqId = Request["GCE_ENQ_ID"].ToString();
                
                  _cus_enq.GCE_ENQ_ID = EnqId;

              
                   _cus_enq_ser.GCS_SERVICE = Request["GCS_SERVICE"].ToString();
                
            
                _cus_enq_ser.GCS_FAC = Request["GCS_FAC"].ToString();
                _cus_enq_ser.GCS_SER_PROVIDER = Request["GCS_SER_PROVIDER"].ToString();
                _cus_enq_ser.GCS_UNITS = Convert.ToInt32(Request["GCS_UNITS"]);
               
                _cus_enq_ser.GCS_PICK_FRM = Request["GCS_PICK_FRM"].ToString();
                _cus_enq_ser.GCS_PICK_TN = Request["GCS_PICK_TN"].ToString();

              

                if (Request["GCS_EXP_DT"].ToString() != "") _cus_enq_ser.GCS_EXP_DT = Convert.ToDateTime(Request["GCS_EXP_DT"].ToString());
                if (Request["GCS_EXP_TIME"].ToString() != "") _cus_enq_ser.GCS_EXP_TIME = Convert.ToDateTime(Request["GCS_EXP_TIME"].ToString());
                if (Request["GCS_DROP_DT"].ToString() != "") _cus_enq_ser.GCS_DROP_DT = Convert.ToDateTime(Request["GCS_DROP_DT"].ToString());
                if (Request["GCS_DROP_TIME"].ToString() != "") _cus_enq_ser.GCS_DROP_TIME = Convert.ToDateTime(Request["GCS_DROP_TIME"].ToString());

                _cus_enq_ser.GCS_DROP = Request["GCS_DROP"].ToString();
                _cus_enq_ser.GCS_DROP_TN = Request["GCS_DROP_TN"].ToString();


                _cus_enq_ser.GCS_SER_COM = company;
                _cus_enq_ser.GCS_SER_PC = userDefPro;
                _cus_enq_ser.GCS_CRE_BY = userId;
                _cus_enq_ser.GCS_CRE_DT = DateTime.Now;
                _cus_enq_ser.GCS_STATUS = 1;



                m.Aut_cate_cd = company;
                m.Aut_cate_tp = "PC";
                m.Aut_direction = null;
                m.Aut_modify_dt = null;
                m.Aut_moduleid = "AT";
                m.Aut_number = 0;
                m.Aut_start_char = "ATMN";
                m.Aut_year = DateTime.Today.Year;


                _cus_enq.GCE_COM = company;
                _cus_enq.GCE_CRE_BY = userId;
                _cus_enq.GCE_CRE_DT = DateTime.Today;
                _cus_enq.GCE_ENQ_TP = _cus_enq_ser.GCS_FAC;
                _cus_enq.GCE_PC = userDefPro;
                _cus_enq.GCE_DT = DateTime.Today;
                _cus_enq.GCE_ENQ = _cus_enq_ser.GCS_COMMENT;
                _cus_enq.GCE_ENQ_COM = company;
                _cus_enq.GCE_PC = userDefPro;
                _cus_enq.GCE_STUS = 1;
                _cus_enq.GCE_FRM_TN = _cus_enq_ser.GCS_PICK_TN;
                _cus_enq.GCE_TO_TN = _cus_enq_ser.GCS_DROP_TN;
                _cus_enq.GCE_FRM_ADD = _cus_enq_ser.GCS_PICK_FRM;
                _cus_enq.GCE_TO_ADD = _cus_enq_ser.GCS_DROP;
                _cus_enq.GCE_RET_DT = _cus_enq_ser.GCS_DROP_DT;

                
                string countrycode = _cus_enq.GCE_FRM_CONTRY;


                //save busentity

                MasterBusinessEntity cus = new MasterBusinessEntity();
                cus.Mbe_act = true;
                cus.Mbe_add1 = Request["Mbe_address"].ToString();
                cus.Mbe_cd = _cus_enq.GCE_CONT_CD;
                cus.Mbe_com = company;
                cus.Mbe_contact = _cus_enq.GCE_CONT_MOB;
                cus.Mbe_cre_by = userId;
                cus.Mbe_cre_dt = DateTime.Now.Date;
                cus.Mbe_email = _cus_enq.GCE_CONT_EMAIL;
                cus.MBE_FNAME = _cus_enq.GCE_GUESS;
                cus.Mbe_mob = _cus_enq.GCE_CONT_MOB;
                cus.Mbe_mod_by = userId;
                cus.Mbe_mod_dt = DateTime.Now.Date;
                cus.Mbe_name =_cus_enq.GCE_GUESS;
                cus.Mbe_nic = Request["Mbe_nic"].ToString();
                cus.Mbe_sub_tp = "D";
                cus.Mbe_tp = "C";



                //List<MST_COUNTRY_SEARCH> oItems = CHNLSVC.ComSearch.getCountry("1", "10", "", "");
              //  var countrycode = oItems.Where(x => x.MCU_DESC == _cus_enq.GCE_FRM_CONTRY).Max(y => y.MCU_CD);

                //var frmcountryCount = oItems.Where(x => x.MCU_CD == _cus_enq.GCE_FRM_CONTRY).Count();
                //var tocountryCode = oItems.Where(x => x.MCU_CD == _cus_enq.GCE_DEST_CONTRY).Count();

                //if (frmcountryCount==0)
                //{
                //    return Json(new { success = false, login = true, msg = "Invalid From Country", type = "Info" }, JsonRequestBehavior.AllowGet);
                //}
                //if (tocountryCode == 0)
                //{
                //    return Json(new { success = false, login = true, msg = "Invalid  Country", type = "Info" }, JsonRequestBehavior.AllowGet);
                //}
                Int32 effect = CHNLSVC.Tours.Save_ENQ_DATA(_cus_enq, m, _cus_enq_ser, out err, countrycode, userDefPro,cus);
                // Int32 effect = CHNLSVC.Tours.Save_ENQ_DATA(_cus_enq,m,_cus_enq_ser,out err,countrycode,userDefPro);
                if (effect >0)
                {
                    Session["jobrole"] = err;
                    return Json(new { success = true, login = true, msg = err, EnqNo = err, type = "Success" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { success = false, login = true, msg = "Get Error", type = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Upload()
        {
            foreach (HttpPostedFileBase file in Request.Files)
            {
                if (file.ContentLength > 0)
                {
                    file.SaveAs(Server.MapPath("~/Upload/" + file.FileName));
                }
            }

            return Json(new { result = true });
        }
        
        public JsonResult getCountryDetails(string countryCd) {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string err;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    countryCd = countryCd.Trim();
                    if (countryCd != "")
                    {
                        List<MST_COUNTRY> country = CHNLSVC.Tours.getCountryDetails(countryCd);
                        if (country.Count > 0)
                        {
                            return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                        }
                        else {
                            return Json(new { success = false, login = true,msg="Invalid country code.",type="Info" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else {
                        return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
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
        public JsonResult LoadExecutive()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<SEARCH_MST_EMP> emp = CHNLSVC.Tours.Get_mst_emp(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());


                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (emp.Count > 0)
                    {
                        foreach (var list in emp)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list.ESEP_FIRST_NAME;
                            o1.Value = list.ESEP_CD;
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
    }
}