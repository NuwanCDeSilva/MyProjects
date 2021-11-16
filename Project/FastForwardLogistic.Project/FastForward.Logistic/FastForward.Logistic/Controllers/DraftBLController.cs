using FF.BusinessObjects.Genaral;
using FF.BusinessObjects.Search;
using FF.BusinessObjects.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastForward.Logistic.Controllers
{
    public class DraftBLController : BaseController
    {
        //
        // GET: /DraftBL/
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                
                SEC_SYSTEM_MENU per = CHNLSVC.Security.getUserPermission(userId,company,11);
                if (per.SSM_ID != 0)
                {
                    Session["trn_bl_cont_det"] = null;
                    return View();
                }
                else
                {
                    return Redirect("/Home/Error");
                }
            }
            else
            {
                return Redirect("~/Home/index");
            }
        }
        public JsonResult AddContainer(string ContainerCode, string ContainerType, string Seal)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<trn_bl_cont_det> _container = Session["trn_bl_cont_det"] as List<trn_bl_cont_det>;
                    if (_container != null)
                    {
                        if (_container.Count > 0)
                        {
                            var count = _container.Where(a => a.Blct_seal_no == Seal && a.Blct_cont_no == ContainerCode && a.blct_con_tp == ContainerType).Count();
                            if (count > 0)
                            {
                                return Json(new { success = true, login = true, msg = "Already Added Container", type = "Info" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                var maxnum = _container.Max(a => a.Blct_line);
                                maxnum = maxnum + 1;
                                trn_bl_cont_det ob = new trn_bl_cont_det();
                                ob.Blct_bl_doc = "";
                                ob.Blct_cont_no = ContainerCode;
                                //ob.Blct_fully_empty = fullemp;
                                ob.Blct_line = maxnum;
                                //ob.Blct_pack = pack;
                                ob.Blct_seal_no = Seal;
                                ob.Blct_seq_no = "0";
                                ob.blct_con_tp = ContainerType;
                                _container.Add(ob);
                                Session["trn_bl_cont_det"] = _container;

                            }

                        }
                        else
                        {
                            trn_bl_cont_det ob = new trn_bl_cont_det();
                            ob.Blct_bl_doc = "";
                            ob.Blct_cont_no = ContainerCode;
                            //ob.Blct_fully_empty = fullemp;
                            ob.Blct_line = 1;
                            //ob.Blct_pack = pack;
                            ob.Blct_seal_no = Seal;
                            ob.Blct_seq_no = "0";
                            ob.blct_con_tp = ContainerType;
                            _container.Add(ob);
                            Session["trn_bl_cont_det"] = _container;
                        }

                    }
                    else
                    {
                        _container = new List<trn_bl_cont_det>();
                        trn_bl_cont_det ob = new trn_bl_cont_det();
                        ob.Blct_bl_doc = "";
                        ob.Blct_cont_no = ContainerCode;
                        //ob.Blct_fully_empty = fullemp;
                        ob.Blct_line = 1;
                        //ob.Blct_pack = pack;
                        ob.Blct_seal_no = Seal;
                        ob.Blct_seq_no = "0";
                        ob.blct_con_tp = ContainerType;

                        _container.Add(ob);
                        Session["trn_bl_cont_det"] = _container;

                    }
                    return Json(new { success = true, login = true, data = _container }, JsonRequestBehavior.AllowGet);

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
        public JsonResult RemoveContainer(string Container, string Type, string Seal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<trn_bl_cont_det> _container = new List<trn_bl_cont_det>();
                if (Session["trn_bl_cont_det"] != null)
                {
                    _container = Session["trn_bl_cont_det"] as List<trn_bl_cont_det>;
                }
                else
                {
                    _container = new List<trn_bl_cont_det>();

                }
                var itemToRemove = _container.First(r => r.Blct_cont_no == Container && r.blct_con_tp == Type && r.Blct_seal_no == Seal);
                _container.Remove(itemToRemove);
                Session["trn_bl_cont_det"] = _container;
                return Json(new { success = true, login = true, data = _container }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateContainer(string Container, string Type, string Seal, string newContainer, string newType, string newSeal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<trn_bl_cont_det> _container = new List<trn_bl_cont_det>();
                if (Session["trn_bl_cont_det"] != null)
                {
                    _container = Session["trn_bl_cont_det"] as List<trn_bl_cont_det>;
                }
                else
                {
                    _container = new List<trn_bl_cont_det>();

                }
                var itemToRemove = _container.First(r => r.Blct_cont_no == Container && r.blct_con_tp == Type && r.Blct_seal_no == Seal);
                itemToRemove.Blct_cont_no = newContainer;
                itemToRemove.blct_con_tp = newType;
                itemToRemove.Blct_seal_no = newSeal;
                //itemToRemove.Blct_pack = newPack;
                //itemToRemove.Blct_fully_empty = newFullemp;
                //_container.Remove(itemToRemove);
                Session["trn_bl_cont_det"] = _container;
                return Json(new { success = true, login = true, data = _container }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveDraftBL(trn_bl_header _hdr)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string err = "";
                //Auto Number
                MasterAutoNumber mastAutoNo = new MasterAutoNumber();
                mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                mastAutoNo.Aut_cate_tp = "COM";
                mastAutoNo.Aut_direction = 1;
                mastAutoNo.Aut_moduleid = "BL";
                mastAutoNo.Aut_start_char = "DBL";
                mastAutoNo.Aut_year = DateTime.Now.Year;
                //hdr
                _hdr.Bl_com_cd = company;
                _hdr.bl_cre_by = userId;
                _hdr.bl_cre_dt = DateTime.Now;
                _hdr.bl_mod_by = userId;
                _hdr.bl_mod_dt = DateTime.Now;
                _hdr.Bl_sbu_cd = userDefPro;
                _hdr.Bl_mos_cd = "testmoscd";
                _hdr.Bl_tos_cd = "testtoscd";
                _hdr.Bl_bl_tp = "DRAFT";
                _hdr.Bl_act = 1;

                List<trn_bl_cont_det> con_det = Session["trn_bl_cont_det"] as List<trn_bl_cont_det>;
                List<trn_bl_det> bl_det = new List<trn_bl_det>();
                trn_bl_det ob = new trn_bl_det();
                if (Request["bld_mark_nos"] != null)
                {
                    ob.bld_mark_nos = Request["bld_mark_nos"].ToString();
                }
                if (Request["bld_grs_weight"] != null)
                {
                    if (Request["bld_grs_weight"].ToString()=="")
                    {
                        ob.bld_grs_weight = 0;
                    }
                    else
                    {
                        ob.bld_grs_weight = Convert.ToDecimal(Request["bld_grs_weight"].ToString());
                    }
                   
                }
                if (Request["bld_grs_weight_uom"] != null)
                {
                    ob.bld_grs_weight_uom = Request["bld_grs_weight_uom"].ToString();
                }
                if (Request["bld_desc_goods"] != null)
                {
                    ob.bld_desc_goods = Request["bld_desc_goods"].ToString();
                }
                if (Request["bld_measure"] != null)
                {
                    if (Request["bld_measure"].ToString() == "")
                    {
                        ob.bld_measure = 0;
                    }
                    else
                    {
                        ob.bld_measure = Convert.ToDecimal(Request["bld_measure"].ToString());
                    }
                   
                } 
                if (Request["bld_measure_uom"] != null)
                {
                    ob.bld_measure_uom = Request["bld_measure_uom"].ToString();
                }
                if (Request["bld_net_weight"] != null)
                {
                    if (Request["bld_net_weight"].ToString() == "")
                    {
                        ob.bld_net_weight = 0;
                    }
                    else
                    {
                        ob.bld_net_weight = Convert.ToDecimal(Request["bld_net_weight"]);
                    }
                   
                }
                if (Request["bld_net_weight_uom"] != null)
                {
                    ob.bld_net_weight_uom = Request["bld_net_weight_uom"].ToString();
                }
                if (Request["bld_package_nos"] != null)
                {
                    if (Request["bld_package_nos"].ToString() == "")
                    {
                        ob.bld_package_nos = 0;
                    }
                    else
                    {
                        ob.bld_package_nos = Convert.ToDecimal(Request["bld_package_nos"].ToString());
                    }
                    
                }
                ob.bld_line = 1;
                ob.bld_package_tp = "";
                ob.bld_seq_no = "0";
                bl_det.Add(ob);
                int effect = 0;

                effect = CHNLSVC.General.SaveBLDraftHdr(_hdr, bl_det, con_det, mastAutoNo, out err);


                if (effect > 0)
                {   // Changed by Chathura on 19-sep-2017
                    if (_hdr.Bl_d_doc_no == null || _hdr.Bl_d_doc_no == "")
                    {
                        return Json(new { success = true, login = true, msg = "Successfully Saved. ID : " + err }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = true, login = true, msg = "Successfully Update. ID : " + err }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, login = true, msg = err }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult BLNoTextChange(string BLno)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<trn_bl_header> _hdr = CHNLSVC.General.GetBLHdr(BLno,company);
                List<trn_bl_det> _bl_det = new List<trn_bl_det>();
                List<trn_bl_cont_det> _cont = new List<trn_bl_cont_det>();
                string cusname = "";
                string shipper = "";
                string agent = "";
                string notify = "";
                string consign = "";
                string carry = "";

                string cusnameadd1 = "";
                string shipperadd1 = "";
                string agentadd1 = "";
                string notifyadd1 = "";
                string consignadd1 = "";
                string carryadd1 = "";

                string cusnameadd2 = "";
                string shipperadd2 = "";
                string agentadd2 = "";
                string notifyadd2 = "";
                string consignadd2 = "";
                string carryadd2 = "";

                if (_hdr != null)
                {
                    if (_hdr.Count>0)
                    {
                        _bl_det = CHNLSVC.General.GetBLitemdetails(_hdr.First().Bl_seq_no);
                        _cont = CHNLSVC.General.GetBLContainer(_hdr.First().Bl_seq_no);
                        Session["trn_bl_cont_det"] = _cont;
                        List<GET_CUS_BASIC_DATA> Cus_det = CHNLSVC.General.GetCustormerBasicData(_hdr.First().Bl_cus_cd, company, "");
                        List<GET_CUS_BASIC_DATA> Shipper = CHNLSVC.General.GetCustormerBasicData(_hdr.First().Bl_shipper_cd, company, "");
                        List<GET_CUS_BASIC_DATA> agentlst = CHNLSVC.General.GetCustormerBasicData(_hdr.First().Bl_agent_cd, company, "");
                        List<GET_CUS_BASIC_DATA> notylist = CHNLSVC.General.GetCustormerBasicData(_hdr.First().Bl_ntfy_party_cd, company, "");
                        List<GET_CUS_BASIC_DATA> consiglist = CHNLSVC.General.GetCustormerBasicData(_hdr.First().Bl_consignee_cd, company, "");
                        List<GET_CUS_BASIC_DATA> carrylist = CHNLSVC.General.GetCustormerBasicData(_hdr.First().Bl_ship_line_cd, company, "");
                        if (Cus_det != null)
                        {
                            if (Cus_det.Count > 0)
                            {
                                cusname = Cus_det.First().mbe_name;
                                cusnameadd1 = Cus_det.First().mbe_add1;
                                cusnameadd2 = Cus_det.First().mbe_add2;
                            }

                        }
                        if (Shipper != null)
                        {

                            if (Shipper.Count > 0)
                            {
                                shipper = Shipper.First().mbe_name;
                                shipperadd1 = Shipper.First().mbe_add1;
                                shipperadd2 = Shipper.First().mbe_add2;
                            }
                        }
                        if (agentlst != null)
                        {

                            if (agentlst.Count > 0)
                            {
                                agent = agentlst.First().mbe_name;
                                agentadd1 = agentlst.First().mbe_add1;
                                agentadd2 = agentlst.First().mbe_add2;
                            }
                        }
                        if (notylist != null)
                        {
                            if (notylist.Count > 0)
                            {
                                notify = notylist.First().mbe_name;
                                notifyadd1 = notylist.First().mbe_add1;
                                notifyadd2 = notylist.First().mbe_add2;
                            }
                        }
                        if (consiglist != null)
                        {
                            if (consiglist.Count > 0)
                            {
                                consign = consiglist.First().mbe_name;
                                consignadd1 = consiglist.First().mbe_add1;
                                consignadd2 = consiglist.First().mbe_add2;
                            }
                        }
                        if (carrylist != null)
                        {
                            if (carrylist.Count > 0)
                            {
                                carry = carrylist.First().mbe_name;
                                carryadd1 = carrylist.First().mbe_add1;
                                carryadd2 = carrylist.First().mbe_add2;
                            }
                        }
                    }
                }

                return Json(new { success = true, 
                    login = true, 
                    hdrdata = _hdr, 
                    details = _bl_det, 
                    containers = _cont, 
                    cusname = cusname, 
                    shipper = shipper, 
                    agent = agent, 
                    notify = notify, 
                    consign = consign, 
                    carry = carry,
                    cusnameadd1 = cusnameadd1,
                    shipperadd1 = shipperadd1,
                    agentadd1 = agentadd1,
                    notifyadd1 = notifyadd1,
                    consignadd1 = consignadd1,
                    carryadd1 = carryadd1,
                    cusnameadd2 = cusnameadd2,
                    shipperadd2 = shipperadd2,
                    agentadd2 = agentadd2,
                    notifyadd2 = notifyadd2,
                    consignadd2 = consignadd2,
                    carryadd2 = carryadd2
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult LoadPayType()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<Pay_type> paytype = CHNLSVC.CommonSearch.GetPayTypes();
                    var _types = paytype.Select(y => y.Code).Distinct().ToList();
                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (_types.Count > 0)
                    {
                        foreach (var list in _types)
                        {
                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list;
                            o1.Value = list;
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
                return Json(new { success = false, login = true, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult LoadOutwordType()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<Pay_type> paytype = CHNLSVC.CommonSearch.GetInwordTypes();
                    var _types = paytype.Select(y => y.Code).Distinct().ToList();
                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (_types.Count > 0)
                    {
                        foreach (var list in _types)
                        {
                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list;
                            o1.Value = list;
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
                return Json(new { success = false, login = true, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public void ClearSession() 
        {
            Session["trn_bl_cont_det"] = null;
            Session["BLAll"] = null;
        }

        public JsonResult LoadFullyEmpty()
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
                    o1.Text = "Yes";
                    o1.Value = "1";
                    oList.Add(o1);
                    ComboBoxObject o2 = new ComboBoxObject();
                    o2.Text = "No";
                    o2.Value = "0";
                    oList.Add(o2);
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

	}
}