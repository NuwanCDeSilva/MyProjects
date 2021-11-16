using FF.BusinessObjects;
using FF.BusinessObjects.Genaral;
using FF.BusinessObjects.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastForward.Logistic.Controllers
{
    public class JobDefinitionController : BaseController
    {
        //
        // GET: /JobDefinition/
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["trn_jb_cus_det"] = null;
                Session["Pending_req"] = null;
                Session["Service_det"] = null;
                Session["trn_jb_cus_det"] = null;
                Session["trn_job_serdet"] = null;
                return View();
            }
            else
            {
                return Redirect("~/Home/index");
            }
        }
        public JsonResult LoadPendingJobRequse()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<PendingServiceRequest> P_list = CHNLSVC.General.GetPendingJobRequse(company);
                    if (P_list == null)
                    {
                        P_list = new List<PendingServiceRequest>();
                    }
                    return Json(new { success = true, login = true, data = P_list }, JsonRequestBehavior.AllowGet);

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
        public JsonResult LoadJobStatus()
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

                    ComboBoxObject o1 = new ComboBoxObject();
                    o1.Text = "Pending";
                    o1.Value = "P";
                    oList.Add(o1);
                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "Approve";
                    o2.Value = "A";
                    oList.Add(o2);
                    ComboBoxObject o3 = new ComboBoxObject();
                    o3.Text = "Cancel";
                    o3.Value = "C";
                    oList.Add(o3);

                    return Json(new { success = true, login = true, data = oList }, JsonRequestBehavior.AllowGet);

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
        public JsonResult LoadEntityTypes()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<EntityType> _entity = CHNLSVC.General.GetJobEntity();



                    List<ComboBoxObject> oList = new List<ComboBoxObject>();

                    foreach (var items in _entity)
                    {
                        ComboBoxObject o1 = new ComboBoxObject();
                        o1.Text = items.tt_trans_sbtp;
                        o1.Value = items.tt_trans_sbtp;
                        oList.Add(o1);
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
                return Json(new { success = false, login = true, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult LoadJobServices()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MainServices> P_list = CHNLSVC.General.GetMainServicesCodes();
                    if (P_list == null)
                    {
                        P_list = new List<MainServices>();
                    }
                    return Json(new { success = true, login = true, data = P_list }, JsonRequestBehavior.AllowGet);

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
        public JsonResult SaveJobDefinition(trn_jb_hdr _jobs, string PendingReq)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string userDefchnl = HttpContext.Session["UserDefChnl"] as string; // Added by Chathura on 4-oct-2017

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                //check job use

                if (_jobs.Jb_jb_no != "" && _jobs.Jb_jb_no != null)
                {
                    int count = CHNLSVC.Sales.CheckJobUse(_jobs.Jb_jb_no);
                    if (count >0)
                    {
                        return Json(new { success = false, login = true, msg = "Can't Update, Already Used this Job No!!", type="Info"}, JsonRequestBehavior.AllowGet);
                    }
                }

                string err = "";
                //Auto Number
                MasterAutoNumber mastAutoNo = new MasterAutoNumber();
                mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                mastAutoNo.Aut_cate_tp = "COM";
                mastAutoNo.Aut_direction = 1;
                mastAutoNo.Aut_moduleid = "JOB";
                mastAutoNo.Aut_start_char = "JOB";
                mastAutoNo.Aut_year = DateTime.Now.Year;
                //hdr
                _jobs.Jb_com_cd = company;
                _jobs.Jb_cre_by = userId;
                _jobs.Jb_cre_dt = DateTime.Now;
                _jobs.Jb_mod_by = userId;
                _jobs.Jb_mod_de = DateTime.Now;
                _jobs.Jb_mos_cd = "";
                _jobs.Jb_oth_ref = "";
                _jobs.Jb_sbu_cd = userDefPro;
                _jobs.Jb_stage = 1;
                _jobs.Jb_stus = "P";
                _jobs.Jb_tos_cd = "";
                _jobs.pc = userDefPro;
                _jobs.chnl = userDefchnl; // Added by Chathura on 4-oct-2017
                List<trn_job_serdet> _det=Session["trn_job_serdet"] as List<trn_job_serdet>;
                List<trn_jb_cus_det> _cus = Session["trn_jb_cus_det"] as List<trn_jb_cus_det>;
                List<PendingServiceRequest> _penging = Session["Pending_req"] as List<PendingServiceRequest>;
                int effect = 0;
                if (_penging != null)
                {
                    if (_penging.Count >0)
                    {
                        effect = CHNLSVC.General.SaveJobDetailswithPendingRequest(_penging, _jobs, mastAutoNo, out err);
                    }
                    else
                    {
                        effect = CHNLSVC.General.SaveJobDetails(_jobs, _det, _cus, mastAutoNo, out err);
                    }
                }
                else
                {
                    effect = CHNLSVC.General.SaveJobDetails(_jobs, _det, _cus, mastAutoNo, out err);
                }

               
                if (effect>0)
                {
                    return Json(new { success = true, login = true, msg = "Successfully Saved Doc : " + err }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = err ,type = "Error"}, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult LoadCustormerDetails(string CustormerCode)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<cus_details> P_list = CHNLSVC.General.GetCustormerdata(company, CustormerCode);
                    if (P_list == null)
                    {
                        P_list = new List<cus_details>();
                    }
                    return Json(new { success = true, login = true, data = P_list }, JsonRequestBehavior.AllowGet);

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
        public JsonResult AddCustomer(string CustormerCode, string CustomerType, string Exec, string Name)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MainServices> services = Session["Service_det"] as List<MainServices>;
                    if (services != null)
                    {
                        if (services.Count == 0)
                        {
                            //Please Select service
                            return Json(new { success = false, login = true, notice = true, msg = "Please Select Services" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        //Please Select service
                        return Json(new { success = false, login = true, notice = true, msg = "Please Select Services" }, JsonRequestBehavior.AllowGet);
                    }

                    List<trn_jb_cus_det> cus_det = Session["trn_jb_cus_det"] as List<trn_jb_cus_det>;
                    List<trn_job_serdet> job_det = Session["trn_job_serdet"] as List<trn_job_serdet>;
                    if (cus_det != null)
                    {
                       
                        trn_jb_cus_det ob = new trn_jb_cus_det();
                        ob.Jc_cus_cd = CustormerCode;
                        ob.Jc_cus_tp = CustomerType;
                        ob.Jc_exe_cd = Exec;
                        ob.Jc_cre_by = userId;
                        ob.Jc_cre_dt = DateTime.Now;
                        ob.Jc_mod_by = userId;
                        ob.Jc_mod_dt = DateTime.Now;
                        ob.Name = Name;
                        cus_det.Add(ob);
                        Session["trn_jb_cus_det"] = cus_det;

                        foreach (var _ser in services)
                        {
                          
                            if (job_det != null)
                            {
                                var samecus_ser = 0;
                                if (job_det.Count>0)
                                {
                                    samecus_ser = job_det.Where(a => a.JS_CUS_CD == CustormerCode && a.JS_SER_TP == _ser.fms_ser_cd).Count();
                                    if (samecus_ser > 0)
                                    {
                                        return Json(new { success = false, login = true, notice = true, msg = "Service details has been already added for customer" }, JsonRequestBehavior.AllowGet);
                                    }

                                    int maxline = job_det.Max(a => a.JS_LINE_NO);
                                    trn_job_serdet obj = new trn_job_serdet();
                                    obj.JS_CRE_BY = userId;
                                    obj.JS_CRE_DT = DateTime.Now;
                                    obj.JS_CUS_CD = CustormerCode;
                                    obj.JS_LINE_NO = maxline + 1;
                                    obj.JS_MOD_BY = userId;
                                    obj.JS_MOD_DT = DateTime.Now;
                                    obj.JS_PC = userDefPro;
                                    obj.JS_RMK = "";
                                    obj.JS_SER_TP = _ser.fms_ser_cd;
                                    obj.Name = Name;

                                    job_det.Add(obj);
                                    Session["trn_job_serdet"] = job_det;
                                }
                                else
                                {

                                    job_det = new List<trn_job_serdet>();
                                    trn_job_serdet obj = new trn_job_serdet();
                                    obj.JS_CRE_BY = userId;
                                    obj.JS_CRE_DT = DateTime.Now;
                                    obj.JS_CUS_CD = CustormerCode;
                                    obj.JS_LINE_NO = 1;
                                    obj.JS_MOD_BY = userId;
                                    obj.JS_MOD_DT = DateTime.Now;
                                    obj.JS_PC = userDefPro;
                                    obj.JS_RMK = "";
                                    obj.JS_SER_TP = _ser.fms_ser_cd;
                                    obj.Name = Name;
                                    job_det.Add(obj);
                                    Session["trn_job_serdet"] = job_det;
                                }
                                
                                
                            }
                            else
                            {
                                job_det = new List<trn_job_serdet>();
                                trn_job_serdet obj = new trn_job_serdet();
                                obj.JS_CRE_BY = userId;
                                obj.JS_CRE_DT = DateTime.Now;
                                obj.JS_CUS_CD = CustormerCode;
                                obj.JS_LINE_NO =  1;
                                obj.JS_MOD_BY = userId;
                                obj.JS_MOD_DT = DateTime.Now;
                                obj.JS_PC = userDefPro;
                                obj.JS_RMK = "";
                                obj.JS_SER_TP = _ser.fms_ser_cd;
                                obj.Name = Name;
                                job_det.Add(obj);
                                Session["trn_job_serdet"] = job_det;
                            }


                        }
                    }
                    else
                    {
                        cus_det = new List<trn_jb_cus_det>();
                        trn_jb_cus_det ob = new trn_jb_cus_det();
                        ob.Jc_cus_cd = CustormerCode;
                        ob.Jc_cus_tp = CustomerType;
                        ob.Jc_exe_cd = Exec;
                        ob.Jc_cre_by = userId;
                        ob.Jc_cre_dt = DateTime.Now;
                        ob.Jc_mod_by = userId;
                        ob.Jc_mod_dt = DateTime.Now;
                        ob.Name = Name;
                        cus_det.Add(ob);
                        Session["trn_jb_cus_det"] = cus_det;

                        foreach (var _ser in services)
                        {

                            if (job_det != null)
                            {
                                int maxline = job_det.Max(a => a.JS_LINE_NO);
                                trn_job_serdet obj = new trn_job_serdet();
                                obj.JS_CRE_BY = userId;
                                obj.JS_CRE_DT = DateTime.Now;
                                obj.JS_CUS_CD = CustormerCode;
                                obj.JS_LINE_NO = maxline + 1;
                                obj.JS_MOD_BY = userId;
                                obj.JS_MOD_DT = DateTime.Now;
                                obj.JS_PC = userDefPro;
                                obj.JS_RMK = "";
                                obj.JS_SER_TP = _ser.fms_ser_cd;
                                obj.Name = Name;
                                job_det.Add(obj);
                                Session["trn_job_serdet"] = job_det;
                            }
                            else
                            {
                                job_det = new List<trn_job_serdet>();
                                trn_job_serdet obj = new trn_job_serdet();
                                obj.JS_CRE_BY = userId;
                                obj.JS_CRE_DT = DateTime.Now;
                                obj.JS_CUS_CD = CustormerCode;
                                obj.JS_LINE_NO = 1;
                                obj.JS_MOD_BY = userId;
                                obj.JS_MOD_DT = DateTime.Now;
                                obj.JS_PC = userDefPro;
                                obj.JS_RMK = "";
                                obj.JS_SER_TP = _ser.fms_ser_cd;
                                obj.Name = Name;
                                job_det.Add(obj);
                                Session["trn_job_serdet"] = job_det;
                            }


                        }

                    }

                    return Json(new { success = true, login = true, data = job_det }, JsonRequestBehavior.AllowGet);

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
        public JsonResult CheckChangeRequest(string ReqNo, string PerNo ,string consoles)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<PendingServiceRequest> pending_req = Session["Pending_req"] as List<PendingServiceRequest>;
                    List<PendingServiceRequest> requsts = CHNLSVC.General.req_all_data(company, ReqNo);
                    List<trn_req_cus_det> req_cus_data = CHNLSVC.General.req_cus_data(requsts.First().rq_seq_no);
                    string cuscode = "";
                    if (req_cus_data==null)
                    {
                        req_cus_data = new List<trn_req_cus_det>();
                    }
                    else
                    {
                        cuscode = req_cus_data.First().Rc_cus_cd;
                    }
                    if (pending_req != null)
                    {
                        var count = pending_req.Where(a => a.rq_no == ReqNo).Count();
                        if (count > 0)
                        {
                            var item = pending_req.First(a => a.rq_no == ReqNo);
                            pending_req.Remove(item);
                        }
                        else
                        {
                            PendingServiceRequest ob = new PendingServiceRequest();
                            ob.rq_no = ReqNo;
                            ob.rq_pouch_no = PerNo;

                            if (consoles =="0")
                            {
                                var conscus = pending_req.Where(a => a.customer != cuscode).Count();
                                if (conscus >0)
                                {
                                    return Json(new { success = false, login = true, msg = "Multiple customer request can be selected only for console jobs !" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                           

                            pending_req.Add(ob);
                            Session["Pending_req"] = pending_req;
                        }
                    }
                    else
                    {
                        pending_req = new List<PendingServiceRequest>();
                        PendingServiceRequest ob = new PendingServiceRequest();
                        ob.rq_no = ReqNo;
                        ob.rq_pouch_no = PerNo;
                        ob.customer = cuscode;
                        pending_req.Add(ob);
                        Session["Pending_req"] = pending_req;

                    }

                    return Json(new { success = true, login = true, data = pending_req }, JsonRequestBehavior.AllowGet);

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
        public JsonResult CheckChangeService(string Code, string Desc)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MainServices> services = Session["Service_det"] as List<MainServices>;
                    if (services != null)
                    {
                        var count = services.Where(a => a.fms_ser_cd == Code).Count();
                        if (count > 0)
                        {
                            var item = services.First(a => a.fms_ser_cd == Code);
                            services.Remove(item);
                        }
                        else
                        {
                            MainServices ob = new MainServices();
                            ob.fms_ser_cd = Code;
                            ob.fms_ser_desc = Desc;
                            services.Add(ob);
                            Session["Service_det"] = services;
                        }
                    }
                    else
                    {
                        services = new List<MainServices>();
                        MainServices ob = new MainServices();
                        ob.fms_ser_cd = Code;
                        ob.fms_ser_desc = Desc;
                        services.Add(ob);
                        Session["Service_det"] = services;
                    }

                    return Json(new { success = true, login = true, data = services }, JsonRequestBehavior.AllowGet);

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
        public JsonResult RemoveJobDet(string Custormer, string Type)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                 List<trn_job_serdet> _serdet= new List<trn_job_serdet>();
                if (Session["trn_job_serdet"] != null)
                {
                     _serdet = Session["trn_job_serdet"] as List<trn_job_serdet>;
                }
                else
                {
                   _serdet = new List<trn_job_serdet>();

                }
                var itemToRemove = _serdet.First(r => r.JS_CUS_CD == Custormer && r.JS_SER_TP == Type);
                _serdet.Remove(itemToRemove);
                Session["trn_job_serdet"] = _serdet;
                return Json(new { success = true, login = true, data = _serdet }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult LoadAlldata(string Job)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<trn_jb_hdr> _hdr = CHNLSVC.General.GetJobHdr(Job);
                List<trn_job_serdet> _job_det = new List<trn_job_serdet>();
                List<trn_jb_cus_det> _cus = new List<trn_jb_cus_det>();

                if (_hdr != null)
                {
                    if (_hdr.Count > 0)
                    {
                        _job_det = CHNLSVC.General.GetJobServicesdetails(_hdr.First().Jb_seq_no);
                        _cus = CHNLSVC.General.GetJobCustomerDetails(_hdr.First().Jb_seq_no);
                        Session["trn_jb_cus_det"] = _cus;
                        Session["trn_job_serdet"] = _job_det;
                    }
                }

                return Json(new { success = true, login = true, hdrdata = _hdr, details = _job_det, customers = _cus }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
           
        }
        public JsonResult LoadAlldataByPouch(string pouch)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<trn_jb_hdr> _hdr = CHNLSVC.General.GetJobHdrbypouch(pouch);
                List<trn_job_serdet> _job_det = new List<trn_job_serdet>();
                List<trn_jb_cus_det> _cus = new List<trn_jb_cus_det>();

                if (_hdr != null)
                {
                    if (_hdr.Count > 0)
                    {
                        _job_det = CHNLSVC.General.GetJobServicesdetails(_hdr.First().Jb_seq_no);
                        _cus = CHNLSVC.General.GetJobCustomerDetails(_hdr.First().Jb_seq_no);
                        Session["trn_jb_cus_det"] = _cus;
                        Session["trn_job_serdet"] = _job_det;
                    }
                }

                return Json(new { success = true, login = true, hdrdata = _hdr, details = _job_det, customers = _cus }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public void ClearSession()
        {
            Session["trn_jb_cus_det"] = null;
            Session["Pending_req"] = null;
            Session["Service_det"] = null;
            Session["trn_jb_cus_det"] = null;
            Session["trn_job_serdet"] = null;
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
                                return Json(new { success = false, login = true, msg = "Please enter valid Town ", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = "Error", type = "Info" }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}