using FF.BusinessObjects;
using FF.BusinessObjects.Tours;
using FF.BusinessObjects.ToursNew;
using FF.Resources;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FastForward.WebAbansTours.Controllers
{
    public class ChargeCodeDefController : BaseController
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
                ST_MENU per = CHNLSVC.Tours.getAcccessPermission(userId, 1010);
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
        // GET: ChargeCodeDef
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                ViewBag.Alert = Session["DataAdd"];
                Session["DataAdd"] = null;
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }
        public JsonResult LoadServiceProvider(string type)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<ComboBoxObject> oItemsa = CHNLSVC.Tours.GetServiceProviders(company, type);


                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (oItemsa.Count > 0)
                    {
                        foreach (var list in oItemsa)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list.Text;
                            o1.Value = list.Value;
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
        public JsonResult LoadClass(string type)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<ComboBoxObject> oItemsa = CHNLSVC.Tours.GetServiceClasses(company, type);


                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (oItemsa.Count > 0)
                    {
                        foreach (var list in oItemsa)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list.Text;
                            o1.Value = list.Value;
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
        public JsonResult LoadIsChild(string type)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {


                    List<ComboBoxObject> oItems = new List<ComboBoxObject>();

                    ComboBoxObject oNo1 = new ComboBoxObject();
                    oNo1.Text = " ";
                    oNo1.Value = "-1";
                    oItems.Add(oNo1);

                    ComboBoxObject oNo = new ComboBoxObject();
                    oNo.Text = "No";
                    oNo.Value = "1";
                    oItems.Add(oNo);

                    ComboBoxObject oYes = new ComboBoxObject();
                    oYes.Text = "Yes";
                    oYes.Value = "0";
                    oItems.Add(oYes);
                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (oItems.Count > 0)
                    {
                        foreach (var list in oItems)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list.Text;
                            o1.Value = list.Value;
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
        public JsonResult LoadServiceByForTravel(string type)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<ComboBoxObject> oItemsa = CHNLSVC.Tours.GetServiceProviders(company, type);


                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (oItemsa.Count > 0)
                    {
                        foreach (var list in oItemsa)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list.Text;
                            o1.Value = list.Value;
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
        public JsonResult LoadClassForTravel(string type)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<ComboBoxObject> oItemsa = CHNLSVC.Tours.GetServiceClasses(company, type);


                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (oItemsa.Count > 0)
                    {
                        foreach (var list in oItemsa)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list.Text;
                            o1.Value = list.Value;
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
        public JsonResult LoadServiceProviderForMsc(string type)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<ComboBoxObject> oItemsa = CHNLSVC.Tours.GetServiceProviders(company, type);


                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (oItemsa.Count > 0)
                    {
                        foreach (var list in oItemsa)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list.Text;
                            o1.Value = list.Value;
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
        public JsonResult getAirChargeCodedet(string val, string chgcode)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                val = val.Trim();


                SR_AIR_CHARGE oAirChargeCode = CHNLSVC.Tours.GetChargeDetailsByCode(company, chgcode, val, userDefPro);
                List<SR_AIR_CHARGE> _list_air_charge = new List<SR_AIR_CHARGE>();
                _list_air_charge.Add(oAirChargeCode);
                if (_list_air_charge.Max(a => a.SAC_CD) != null)
                {
                    return Json(new { success = true, login = true, data = _list_air_charge }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = true, data = "" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getTrnsChargeCodedet(string val, string chgcode)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                val = val.Trim();


                SR_TRANS_CHA TrnsChargeCode = CHNLSVC.Tours.GetTransferChargeDetailsByCode(company, chgcode, val, userDefPro);
                List<SR_TRANS_CHA> _list_trns_charge = new List<SR_TRANS_CHA>();
                _list_trns_charge.Add(TrnsChargeCode);
                if (_list_trns_charge.Max(a => a.STC_CD) != null)
                {
                    return Json(new { success = true, login = true, data = _list_trns_charge }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = true, data = "" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult getMscChargeCodedet(string val, string chgcode)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                val = val.Trim();


                SR_SER_MISS MscChargeCode = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, chgcode, val, userDefPro);
                List<SR_SER_MISS> _list_msc_charge = new List<SR_SER_MISS>();
                _list_msc_charge.Add(MscChargeCode);
                if (_list_msc_charge.Max(a => a.SSM_CD) != null)
                {
                    return Json(new { success = true, login = true, data = _list_msc_charge }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = true, data = "" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult SaveAirChageCodes(SR_AIR_CHARGE _air, string chgcd)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string err;
                _air.SAC_ACT = 1;
                _air.SAC_COM = company;
                _air.SAC_CRE_BY = userId;
                _air.SAC_CRE_DT = DateTime.Now;
                _air.SAC_CATE = chgcd;
                _air.SAC_MOD_BY = userId;
                _air.SAC_MOD_DT = DateTime.Now;
                _air.SAC_PC = userDefPro;
                Int32 seqno;
                SR_AIR_CHARGE oAirChargeCode = CHNLSVC.Tours.GetChargeDetailsByCode(company, chgcd, _air.SAC_CD, userDefPro);
                if (oAirChargeCode == null)
                {
                    seqno = 0;
                }
                else
                {
                    seqno = oAirChargeCode.SAC_SEQ;
                }

                _air.SAC_SEQ = seqno;

                if (_air.SAC_CD == "" | _air.SAC_CD == null)
                {
                    return Json(new { success = false, login = true, msg = "Please enter code", type = "Info" }, JsonRequestBehavior.AllowGet);
                }

                //if (_air.SAC_RT == 0 | _air.SAC_RT == 0)
                //{
                //    return Json(new { success = false, login = true, msg = "Please Enter Rate", type = "Info" }, JsonRequestBehavior.AllowGet);
                //}
                if (_air.SAC_IS_CHILD == -1)
                {
                    return Json(new { success = false, login = true, msg = "Please select Is Child Option.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_air.SAC_FRM_DT == null)
                {
                    return Json(new { success = false, login = true, msg = "From Date required.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_air.SAC_TO_DT == null)
                {
                    return Json(new { success = false, login = true, msg = "To Date required.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_air.SAC_TIC_DESC == null | _air.SAC_TIC_DESC == "")
                {
                    return Json(new { success = false, login = true, msg = "Please enter ticket details.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_air.SAC_ADD_DESC == null | _air.SAC_ADD_DESC == "")
                {
                    return Json(new { success = false, login = true, msg = "Please enter aditional details.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_air.SAC_FROM == null | _air.SAC_FROM == "")
                {
                    return Json(new { success = false, login = true, msg = "Please enter From country.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_air.SAC_TO == null | _air.SAC_TO == "")
                {
                    return Json(new { success = false, login = true, msg = "Please enter To country.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_air.SAC_TP == null | _air.SAC_TP == "")
                {
                    return Json(new { success = false, login = true, msg = "Please enter type.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }

                Int32 effect = CHNLSVC.Tours.SaveAirChageCodes(_air, out err);
                if (effect == 1 | effect == 2)
                {
                    return Json(new { success = true, login = true, msg = "Air Travel details added.", type = "Success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = err, type = "Error" }, JsonRequestBehavior.AllowGet);
                }


            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveMSCData(SR_SER_MISS _msc, string chgcd)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string err;
                _msc.SSM_ACT = 1;
                _msc.SSM_COM = company;
                _msc.SSM_CRE_BY = userId;
                _msc.SSM_CRE_DT = DateTime.Now;
                _msc.SSM_CATE = chgcd;
                _msc.SSM_MOD_BY = userId;
                _msc.SSM_MOD_DT = DateTime.Now;
                _msc.SSM_PC = userDefPro;
                Int32 seqno;
                SR_SER_MISS MscChargeCode = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(company, chgcd, _msc.SSM_CD, userDefPro);
                if (MscChargeCode == null)
                {
                    seqno = 0;
                }
                else
                {
                    seqno = MscChargeCode.SSM_SEQ;
                }

                _msc.SSM_SEQ = seqno;


                if (_msc.SSM_CD == "" | _msc.SSM_CD == null)
                {
                    return Json(new { success = false, login = true, msg = "Please enter code.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_msc.SSM_DESC == "" | _msc.SSM_DESC == null)
                {
                    return Json(new { success = false, login = true, msg = "Please enter description.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_msc.SSM_SER_PRO == null)
                {
                    return Json(new { success = false, login = true, msg = "Please select service provider.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_msc.SSM_RT_TP == null)
                {
                    return Json(new { success = false, login = true, msg = "Please enter rate type.", type = "Info" }, JsonRequestBehavior.AllowGet);
                }


                Int32 effect = CHNLSVC.Tours.SaveMiscellaneousChageCodes(_msc, out err);
                if (effect == 1)
                {
                    return Json(new { success = true, login = true, msg = "Charg code added successful.", type = "Success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = err, type = "Error" }, JsonRequestBehavior.AllowGet);
                }


            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveTravelChageCodes(SR_TRANS_CHA _trs, string chgcd)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                string err;
                _trs.STC_ACT = 1;
                _trs.STC_COM = company;
                _trs.STC_CRE_BY = userId;
                _trs.STC_CRE_DT = DateTime.Now;
                _trs.STC_CATE = chgcd;
                _trs.STC_MOD_BY = userId;
                _trs.STC_MOD_DT = DateTime.Now;
                _trs.STC_PC = userDefPro;
                Int32 seqno;
                SR_TRANS_CHA TrnsChargeCode = CHNLSVC.Tours.GetTransferChargeDetailsByCode(company, chgcd, _trs.STC_CD,userDefPro);
                if (TrnsChargeCode == null)
                {
                    seqno = 0;
                }
                else
                {
                    seqno = TrnsChargeCode.STC_SEQ;
                }

                _trs.STC_SEQ = seqno;


                if (_trs.STC_CD == "" | _trs.STC_CD == null)
                {
                    return Json(new { success = false, login = true, msg = "Please Enter Code ", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_trs.STC_VEH_TP == "" | _trs.STC_VEH_TP == null)
                {
                    return Json(new { success = false, login = true, msg = "Please Enter Vehicle Type", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_trs.STC_DESC == null | _trs.STC_DESC == "")
                {
                    return Json(new { success = false, login = true, msg = "Please Enter Description", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_trs.STC_RT_TP == null | _trs.STC_RT_TP == "")
                {
                    return Json(new { success = false, login = true, msg = "Please Enter Rate Type", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_trs.STC_SER_BY == null | _trs.STC_SER_BY == "")
                {
                    return Json(new { success = false, login = true, msg = "Please Enter Service By", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_trs.STC_CLS == null | _trs.STC_CLS == "")
                {
                    return Json(new { success = false, login = true, msg = "Please Select Class", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_trs.STC_FRM_KM == 0)
                {
                    return Json(new { success = false, login = true, msg = "Please Enter From Km", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_trs.STC_TO_KM == 0)
                {
                    return Json(new { success = false, login = true, msg = "Please Enter To Km", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                //if (_trs.STC_RT == null | _trs.STC_RT == 0)
                //{
                //    return Json(new { success = false, login = true, msg = "Please Enter Rate", type = "Info" }, JsonRequestBehavior.AllowGet);
                //}
                //if (_trs.STC_AD_RT == null)
                //{
                //    return Json(new { success = false, login = true, msg = "Please Enter Additional Rate", type = "Info" }, JsonRequestBehavior.AllowGet);
                //}
                if (_trs.STC_FRM == null | _trs.STC_FRM == "")
                {
                    return Json(new { success = false, login = true, msg = "Please Enter From", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                if (_trs.STC_TO == null | _trs.STC_TO == "")
                {
                    return Json(new { success = false, login = true, msg = "Please Enter To", type = "Info" }, JsonRequestBehavior.AllowGet);
                }
                Int32 effect = CHNLSVC.Tours.SaveTrasportChageCodes(_trs, out err);
                if (effect == 1)
                {
                    return Json(new { success = true, login = true, msg = "Transport Details Added", type = "Success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = err, type = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveExcelData(FormCollection formCollection)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string err;
                Int32 effect;
                if (Request != null)
                {
                    HttpPostedFileBase file = Request.Files["UploadedFile"];
                    string chgcd = Request["ChargeCode"];
                        if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName) && (file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || file.ContentType == "application/vnd.ms-excel" || file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.template" || file.ContentType == "application/vnd.ms-excel.sheet.macroEnabled.12"))
                    {
                        string fileName = file.FileName;
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        // var _msc = new List<SR_SER_MISS>();

                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;


                            if (chgcd == "OVSLAGMT" | chgcd == "MSCELNS")
                            {
                                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                {
                                    var _msc = new SR_SER_MISS();
                                    _msc.SSM_ACT = 1;
                                    _msc.SSM_COM = company;
                                    _msc.SSM_CRE_BY = userId;
                                    _msc.SSM_CRE_DT = DateTime.Now;
                                    _msc.SSM_CATE = chgcd;
                                    _msc.SSM_MOD_BY = userId;
                                    _msc.SSM_MOD_DT = DateTime.Now;
                                    _msc.SSM_PC = userDefPro;
                                    if (workSheet.Cells[rowIterator, 1].Value == null)
                                    {
                                        Session["DataAdd"] = "Row Number :" + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        _msc.SSM_CD = workSheet.Cells[rowIterator, 1].Value.ToString();
                                       
                                    }

                                    if (workSheet.Cells[rowIterator, 2].Value == null)
                                    {
                                        Session["DataAdd"] = "Row Number :" + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        _msc.SSM_SER_PRO = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    }

                                    if (workSheet.Cells[rowIterator, 3].Value == null)
                                    {
                                        Session["DataAdd"] = "Row Number :" + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        if (IsDate(workSheet.Cells[rowIterator, 3].Value.ToString()) == true)
                                        {
                                            _msc.SSM_FRM_DT = Convert.ToDateTime(workSheet.Cells[rowIterator, 3].Value.ToString());
                                        }
                                        else
                                        {
                                            Session["DataAdd"] = "Row Number :" + rowIterator + " From date Invalid";
                                            return RedirectToAction("Index");
                                        }
                                       
                                    }

                                    if (workSheet.Cells[rowIterator, 4].Value == null)
                                    {
                                        Session["DataAdd"] = "Row Number :" + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        if (IsDate(workSheet.Cells[rowIterator, 4].Value.ToString()) == true)
                                        {
                                            _msc.SSM_TO_DT = Convert.ToDateTime(workSheet.Cells[rowIterator, 4].Value.ToString());
                                        }
                                        else
                                        {
                                            Session["DataAdd"] = "Row Number :" + rowIterator + " To date Invalid";
                                            return RedirectToAction("Index");
                                        }
                                       
                                    }


                                    if (workSheet.Cells[rowIterator, 5].Value == null)
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        if (IsDecimalNumber(workSheet.Cells[rowIterator, 5].Value.ToString()) == true)
                                        {
                                            _msc.SSM_RT = Convert.ToDecimal(workSheet.Cells[rowIterator, 5].Value.ToString());
                                        }
                                        else
                                        {
                                            Session["DataAdd"] = "Row Number :" + rowIterator + " Invalid Rate";
                                            return RedirectToAction("Index");
                                        }
                                      
                                    }

                                    if (workSheet.Cells[rowIterator, 6].Value == null)
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        _msc.SSM_CUR = workSheet.Cells[rowIterator, 6].Value.ToString();
                                    }

                                    if (workSheet.Cells[rowIterator, 7].Value == null)
                                    {

                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        _msc.SSM_RT_TP = workSheet.Cells[rowIterator, 7].Value.ToString();
                                    }

                                    if (workSheet.Cells[rowIterator, 8].Value == null)
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        _msc.SSM_DESC = workSheet.Cells[rowIterator, 8].Value.ToString();
                                    }


                                    // _msc.Add(_msc2);


                                    effect = CHNLSVC.Tours.SaveMiscellaneousChageCodes(_msc, out err);
                                    if (effect != 1)
                                    {
                                        return Json(new { success = false, login = true, msg = err, type = "Error" }, JsonRequestBehavior.AllowGet);
                                    }
                                }

                                Session["DataAdd"] = "Data Aded";
                                return RedirectToAction("Index");
                            }
                            if (chgcd == "AIRTVL")
                            {
                                //
                                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                {
                                    var _air = new SR_AIR_CHARGE();
                                    _air.SAC_ACT = 1;
                                    _air.SAC_COM = company;
                                    _air.SAC_CRE_BY = userId;
                                    _air.SAC_CRE_DT = DateTime.Now;
                                    _air.SAC_CATE = chgcd;
                                    _air.SAC_MOD_BY = userId;
                                    _air.SAC_MOD_DT = DateTime.Now;
                                    _air.SAC_PC = userDefPro;
                                    if (workSheet.Cells[rowIterator, 1].Value == null)
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        _air.SAC_CD = workSheet.Cells[rowIterator, 1].Value.ToString();
                                    }

                                    if (workSheet.Cells[rowIterator, 2].Value == null)
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        _air.SAC_SCV_BY = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    }

                                    if (workSheet.Cells[rowIterator, 3].Value == null)
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        if (IsDate(workSheet.Cells[rowIterator, 3].Value.ToString()) == true)
                                        {
                                            _air.SAC_FRM_DT = Convert.ToDateTime(workSheet.Cells[rowIterator, 3].Value.ToString());
                                        }
                                        else
                                        {
                                            Session["DataAdd"] = "Row Number : " + rowIterator + " Invalid From Date";
                                            return RedirectToAction("Index");
                                        }
                                       
                                    }

                                    if (workSheet.Cells[rowIterator, 4].Value == null)
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        if (IsDate(workSheet.Cells[rowIterator, 4].Value.ToString()) == true)
                                        {
                                            _air.SAC_TO_DT = Convert.ToDateTime(workSheet.Cells[rowIterator, 4].Value.ToString());
                                        }
                                        else
                                        {
                                            Session["DataAdd"] = "Row Number : " + rowIterator + " To Date Invalid";
                                            return RedirectToAction("Index");
                                        }
                                      
                                    }

                                    if (workSheet.Cells[rowIterator, 5].Value == null)
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        if (IsDecimalNumber(workSheet.Cells[rowIterator, 5].Value.ToString()) == true)
                                        {
                                            _air.SAC_RT = Convert.ToDecimal(workSheet.Cells[rowIterator, 5].Value.ToString());
                                        }
                                        else
                                        {
                                            Session["DataAdd"] = "Row Number : " + rowIterator + " Invalid Rate";
                                            return RedirectToAction("Index");
                                        }
                                      
                                    }

                                    if (workSheet.Cells[rowIterator, 6].Value == null)
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        _air.SAC_CUR = workSheet.Cells[rowIterator, 6].Value.ToString();
                                    }
                                    if (workSheet.Cells[rowIterator, 7].Value == null)
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        _air.SAC_TP = workSheet.Cells[rowIterator, 7].Value.ToString();
                                    }

                                    if (workSheet.Cells[rowIterator, 8].Value==null)
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }else
                                    {
                                        _air.SAC_TIC_DESC = workSheet.Cells[rowIterator, 8].Value.ToString();
                                    }

                                    if (workSheet.Cells[rowIterator, 9].Value==null)
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }else{
                                        _air.SAC_ADD_DESC = workSheet.Cells[rowIterator, 9].Value.ToString();
                                    }

                                    if (workSheet.Cells[rowIterator, 10].Value==null)
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        _air.SAC_CLS = workSheet.Cells[rowIterator, 10].Value.ToString();
                                    }

                                    if (workSheet.Cells[rowIterator, 10].Value==null)
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        _air.SAC_FROM = workSheet.Cells[rowIterator, 11].Value.ToString();
                                    }

                                    if (workSheet.Cells[rowIterator, 12].Value==null)
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Some Data empty";
                                        return RedirectToAction("Index");
                                    }else
                                    {
                                        _air.SAC_TO = workSheet.Cells[rowIterator, 12].Value.ToString();
                                    }
                                  

                                    effect = CHNLSVC.Tours.SaveAirChageCodes(_air, out err);
                                    if (effect != 1)
                                    {
                                        return Json(new { success = false, login = true, msg = err, type = "Error" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                Session["DataAdd"] = "Data Aded";
                                return RedirectToAction("Index");
                            }

                            if (chgcd == "TRANS")
                            {
                                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                {
                                    var _trs = new SR_TRANS_CHA();
                                    _trs.STC_ACT = 1;
                                    _trs.STC_COM = company;
                                    _trs.STC_CRE_BY = userId;
                                    _trs.STC_CRE_DT = DateTime.Now;
                                    _trs.STC_CATE = chgcd;
                                    _trs.STC_MOD_BY = userId;
                                    _trs.STC_MOD_DT = DateTime.Now;
                                    _trs.STC_PC = userDefPro;
                                    for (int i = 1; i < noOfCol; i++)
                                    {
                                        if (workSheet.Cells[rowIterator, i].Value == null)
                                        {
                                            Session["DataAdd"] = "Row Number : " + rowIterator +" Some Data empty";
                                            return RedirectToAction("Index");
                                        }
                                    }

                                  

                                    _trs.STC_CD = workSheet.Cells[rowIterator, 1].Value.ToString();

                                    List<ComboBoxObject> oItemsa = CHNLSVC.Tours.GetServiceProviders(company, "TRANS");
                                    int count = 0;
                                    foreach (var li in oItemsa)
                                    {
                                        
                                        if (li.Value == workSheet.Cells[rowIterator, 2].Value.ToString())
                                        {
                                            count = 1;

                                        }
                                        }
                                    if (count == 1)
                                    {
                                        _trs.STC_SER_BY = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    }
                                    else
                                    {
                                        Session["DataAdd"] = "Wrong Service By" + "Row Number : " + rowIterator;
                                        return RedirectToAction("Index");
                                    } 
                                 

                                    if (IsDate(workSheet.Cells[rowIterator, 3].Value.ToString())==true)
                                    {

                                        _trs.STC_FRM_DT = Convert.ToDateTime(workSheet.Cells[rowIterator, 3].Value.ToString());
                                    }
                                    else
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Invalid From date";
                                        return RedirectToAction("Index");
                                    }
                                    if (IsDate(workSheet.Cells[rowIterator, 4].Value.ToString()) == true)
                                    {

                                        _trs.STC_TO_DT = Convert.ToDateTime(workSheet.Cells[rowIterator, 4].Value.ToString());
                                    }
                                    else
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Invalid To date";
                                        return RedirectToAction("Index");
                                    }

                                    if (IsDecimalNumber(workSheet.Cells[rowIterator, 5].Value.ToString()) == true)
                                    {
                                        _trs.STC_RT = Convert.ToDecimal(workSheet.Cells[rowIterator, 5].Value.ToString());
                                    }
                                    else
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Invalid Rate";
                                        return RedirectToAction("Index");
                                    }

                                    if (IsDecimalNumber(workSheet.Cells[rowIterator, 6].Value.ToString()) == true)
                                    {
                                        _trs.STC_AD_RT = Convert.ToDecimal(workSheet.Cells[rowIterator, 6].Value.ToString());
                                    }
                                    else
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Invalid Aditional Rate";
                                        return RedirectToAction("Index");
                                    }

                                    if (IsDecimalNumber(workSheet.Cells[rowIterator, 7].Value.ToString()) == true)
                                    {
                                        _trs.STC_FRM_KM = Convert.ToInt16(workSheet.Cells[rowIterator, 7].Value.ToString());
                                    }
                                    else
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Invalid From KM";
                                        return RedirectToAction("Index");
                                    }

                                    if (IsDecimalNumber(workSheet.Cells[rowIterator, 8].Value.ToString()) == true)
                                    {
                                        _trs.STC_TO_KM = Convert.ToInt16(workSheet.Cells[rowIterator, 8].Value.ToString());
                                    }
                                    else
                                    {
                                        Session["DataAdd"] = "Row Number : " + rowIterator + " Invalid To KM";
                                        return RedirectToAction("Index");
                                    }
                                
                                    _trs.STC_CURR = workSheet.Cells[rowIterator, 9].Value.ToString();
                                    _trs.STC_RT_TP = workSheet.Cells[rowIterator, 10].Value.ToString();
                                    _trs.STC_DESC = workSheet.Cells[rowIterator, 11].Value.ToString();
                                    _trs.STC_VEH_TP = workSheet.Cells[rowIterator, 12].Value.ToString();
                                    _trs.STC_CLS = workSheet.Cells[rowIterator, 13].Value.ToString();
                                    _trs.STC_FRM = workSheet.Cells[rowIterator, 14].Value.ToString();
                                    _trs.STC_TO = workSheet.Cells[rowIterator, 15].Value.ToString();

                                    effect = CHNLSVC.Tours.SaveTrasportChageCodes(_trs, out err);
                                    if (effect != 1)
                                    {
                                        return Json(new { success = false, login = true, msg = err, type = "Error" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                Session["DataAdd"] = "Data Aded";
                                return RedirectToAction("Index");
                            }

                        }

                    }

                }

               // return Json(new { success = true, login = true, msg = "Cannot Process", type = "Success" }, JsonRequestBehavior.AllowGet);
                Session["DataAdd"] = "Invalid excel Sheet";
                return RedirectToAction("Index");


            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }
        private bool isValidateEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsDate(string date)
        {
            DateTime dateTime2;
            if (DateTime.TryParse(date, out dateTime2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsDecimalNumber(string number)
        {
            decimal num;

            if (decimal.TryParse(number,out num))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public JsonResult CostType()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MST_COST_CATE> oCate = CHNLSVC.Tours.GET_COST_CATE(company, userDefPro);
                    List<MST_COST_CATE> oCat2 = oCate.OrderBy(a => a.MCC_DESC).ToList();

                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (oCat2.Count > 0)
                    {
                        foreach (var list in oCat2)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list.MCC_DESC;
                            o1.Value = list.MCC_CD;
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
        public JsonResult Currency()
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<MasterCurrency> _cur = CHNLSVC.General.GetAllCurrency(null);


                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    if (_cur.Count > 0)
                    {
                        foreach (var list in _cur)
                        {

                            ComboBoxObject o1 = new ComboBoxObject();
                            o1.Text = list.Mcr_cd;
                            o1.Value = list.Mcr_cd;
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
        public JsonResult getAirTicketTypes() { 
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<ST_AIRTCKT_TYPS> types = CHNLSVC.Tours.getAirTicketTypes();
                    return Json(new { success = true, login = true, data = types }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getPkgTypes(){
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    List<ST_PKG_TP> types = CHNLSVC.Tours.getpKGTypes();
                    return Json(new { success = true, login = true, data = types }, JsonRequestBehavior.AllowGet);
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
    }
}