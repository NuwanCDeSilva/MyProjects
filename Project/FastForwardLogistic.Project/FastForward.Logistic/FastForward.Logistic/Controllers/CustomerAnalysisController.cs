using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FF.BusinessObjects.Genaral;
using FF.BusinessObjects.Security;
using System.Data;
using System.Text.RegularExpressions;
using FF.BusinessObjects.Search;
using FF.BusinessObjects.Sales;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace FastForward.Logistic.Controllers
{
    public class CustomerAnalysisController : BaseController
    {
        // GET: CustomerAnalysis
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            Session["MAP"] = "";

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                SEC_SYSTEM_MENU per = CHNLSVC.Security.getUserPermission(userId, company, 22);
                if (per.SSM_ID != 0)
                {
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
        public JsonResult addSelectedValues(string type, string value, string desc)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {

                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    if (type == "ctown")
                    {
                        List<MST_TOWN_SELECTED> selecteditem = (List<MST_TOWN_SELECTED>)Session["MST_TOWN_SELECTED"];
                        if (selecteditem != null && selecteditem.Count > 0)
                        {
                            if (selecteditem.Where(x => x.mt_cd == value).Count() > 0)
                            {
                                selecteditem.RemoveAll((x) => x.mt_cd == value);
                            }
                            else
                            {
                                MST_TOWN_SELECTED aded = new MST_TOWN_SELECTED();
                                aded.mt_cd = value;
                                aded.mt_desc = desc;
                                selecteditem.Add(aded);
                            }
                        }
                        else
                        {
                            selecteditem = new List<MST_TOWN_SELECTED>();
                            MST_TOWN_SELECTED aded = new MST_TOWN_SELECTED();
                            aded.mt_cd = value;
                            aded.mt_desc = desc;
                            selecteditem.Add(aded);
                        }
                        Session["MST_TOWN_SELECTED"] = selecteditem;
                        return Json(new { success = true, login = true, selecteditem = selecteditem }, JsonRequestBehavior.AllowGet);

                    }
                    else if (type == "cdistrict")
                    {
                        List<MST_DISTRICT_SELECTED> selecteditem = (List<MST_DISTRICT_SELECTED>)Session["MST_DISTRICT_SELECTED"];
                        if (selecteditem != null && selecteditem.Count > 0)
                        {
                            if (selecteditem.Where(x => x.mdis_cd == value).Count() > 0)
                            {
                                selecteditem.RemoveAll((x) => x.mdis_cd == value);
                            }
                            else
                            {
                                MST_DISTRICT_SELECTED aded = new MST_DISTRICT_SELECTED();
                                aded.mdis_cd = value;
                                aded.mdis_desc = desc;
                                selecteditem.Add(aded);
                            }
                        }
                        else
                        {
                            selecteditem = new List<MST_DISTRICT_SELECTED>();
                            MST_DISTRICT_SELECTED aded = new MST_DISTRICT_SELECTED();
                            aded.mdis_cd = value;
                            aded.mdis_desc = desc;
                            selecteditem.Add(aded);
                        }
                        Session["MST_DISTRICT_SELECTED"] = selecteditem;
                        return Json(new { success = true, login = true, selecteditem = selecteditem }, JsonRequestBehavior.AllowGet);
                    }
                    else if (type == "cprovince")
                    {
                        List<MST_PROVINCE_SELECTED> selecteditem = (List<MST_PROVINCE_SELECTED>)Session["MST_PROVINCE_SELECTED"];
                        if (selecteditem != null && selecteditem.Count > 0)
                        {
                            if (selecteditem.Where(x => x.mpro_cd == value).Count() > 0)
                            {
                                selecteditem.RemoveAll((x) => x.mpro_cd == value);
                            }
                            else
                            {
                                MST_PROVINCE_SELECTED aded = new MST_PROVINCE_SELECTED();
                                aded.mpro_cd = value;
                                aded.mpro_desc = desc;
                                selecteditem.Add(aded);
                            }
                        }
                        else
                        {
                            selecteditem = new List<MST_PROVINCE_SELECTED>();
                            MST_PROVINCE_SELECTED aded = new MST_PROVINCE_SELECTED();
                            aded.mpro_cd = value;
                            aded.mpro_desc = desc;
                            selecteditem.Add(aded);
                        }
                        Session["MST_PROVINCE_SELECTED"] = selecteditem;
                        return Json(new { success = true, login = true, selecteditem = selecteditem }, JsonRequestBehavior.AllowGet);
                    }
                    else if (type == "ccompany")
                    {
                        List<MST_COM_SELECTED> selecteditem = (List<MST_COM_SELECTED>)Session["MST_COM_SELECTED"];
                        if (selecteditem != null && selecteditem.Count > 0)
                        {
                            if (selecteditem.Where(x => x.Mc_cd == value).Count() > 0)
                            {
                                selecteditem.RemoveAll((x) => x.Mc_cd == value);
                            }
                            else
                            {
                                MST_COM_SELECTED aded = new MST_COM_SELECTED();
                                aded.Mc_cd = value;
                                aded.Mc_desc = desc;
                                selecteditem.Add(aded);
                            }
                        }
                        else
                        {
                            selecteditem = new List<MST_COM_SELECTED>();
                            MST_COM_SELECTED aded = new MST_COM_SELECTED();
                            aded.Mc_cd = value;
                            aded.Mc_desc = desc;
                            selecteditem.Add(aded);
                        }
                        Session["MST_COM_SELECTED"] = selecteditem;
                        return Json(new { success = true, login = true, selecteditem = selecteditem }, JsonRequestBehavior.AllowGet);                       
                    }
                    else if (type == "cmode")
                    {
                        List<MST_MS_SELECTED> selecteditem = (List<MST_MS_SELECTED>)Session["MST_MS_SELECTED"];
                        if (selecteditem != null && selecteditem.Count > 0)
                        {
                            if (selecteditem.Where(x => x.Mms_cd == value).Count() > 0)
                            {
                                selecteditem.RemoveAll((x) => x.Mms_cd == value);
                            }
                            else
                            {
                                MST_MS_SELECTED aded = new MST_MS_SELECTED();
                                aded.Mms_cd = value;
                                aded.Mms_desc = desc;
                                selecteditem.Add(aded);
                            }
                        }
                        else
                        {
                            selecteditem = new List<MST_MS_SELECTED>();
                            MST_MS_SELECTED aded = new MST_MS_SELECTED();
                            aded.Mms_cd = value;
                            aded.Mms_desc = desc;
                            selecteditem.Add(aded);
                        }
                        Session["MST_MS_SELECTED"] = selecteditem;
                        return Json(new { success = true, login = true, selecteditem = selecteditem }, JsonRequestBehavior.AllowGet);
                    }
                    else if (type == "cpc")
                    {
                        List<MST_PC_SELECTED> selecteditem = (List<MST_PC_SELECTED>)Session["MST_PC_SELECTED"];
                        if (selecteditem != null && selecteditem.Count > 0)
                        {
                            if (selecteditem.Where(x => x.Mpc_cd == value).Count() > 0)
                            {
                                selecteditem.RemoveAll((x) => x.Mpc_cd == value);
                            }
                            else
                            {
                                MST_PC_SELECTED aded = new MST_PC_SELECTED();
                                aded.Mpc_cd = value;
                                aded.Mpc_desc = desc;
                                selecteditem.Add(aded);
                            }
                        }
                        else
                        {
                            selecteditem = new List<MST_PC_SELECTED>();
                            MST_PC_SELECTED aded = new MST_PC_SELECTED();
                            aded.Mpc_cd = value;
                            aded.Mpc_desc = desc;
                            selecteditem.Add(aded);
                        }
                        Session["MST_PC_SELECTED"] = selecteditem;
                        return Json(new { success = true, login = true, selecteditem = selecteditem }, JsonRequestBehavior.AllowGet);
                    }
                    else if (type == "ccustomer")
                    {
                        List<MST_CUS_SELECTED> selecteditem = (List<MST_CUS_SELECTED>)Session["MST_CUS_SELECTED"];
                        if (selecteditem != null && selecteditem.Count > 0)
                        {
                            if (selecteditem.Where(x => x.Mbe_cd == value).Count() > 0)
                            {
                                selecteditem.RemoveAll((x) => x.Mbe_cd == value);
                            }
                            else
                            {
                                MST_CUS_SELECTED aded = new MST_CUS_SELECTED();
                                aded.Mbe_cd = value;
                                aded.Mbe_name = desc;
                                selecteditem.Add(aded);
                            }
                        }
                        else
                        {
                            selecteditem = new List<MST_CUS_SELECTED>();
                            MST_CUS_SELECTED aded = new MST_CUS_SELECTED();
                            aded.Mbe_cd = value;
                            aded.Mbe_name = desc;
                            selecteditem.Add(aded);
                        }
                        Session["MST_CUS_SELECTED"] = selecteditem;
                        return Json(new { success = true, login = true, selecteditem = selecteditem }, JsonRequestBehavior.AllowGet);
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

        public JsonResult getAgeCategory(string type)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    DataTable dtVal = new DataTable();
                    dtVal = CHNLSVC.Sales.GetBusinessEntityAllValues("CU", type);

                    string JSONresult;
                    JSONresult = JsonConvert.SerializeObject(dtVal);
                    return Json(new { success = true, login = true, data = JSONresult, type = type }, JsonRequestBehavior.AllowGet);

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
                    if (type == "ctown")
                    {
                        List<MST_TOWN_SELECTED> selecteditem = (List<MST_TOWN_SELECTED>)Session["MST_TOWN_SELECTED"];
                        if (selecteditem != null && selecteditem.Count > 0)
                        {
                            if (selecteditem.Where(x => x.mt_cd == value).Count() > 0)
                            {
                                selecteditem.RemoveAll((x) => x.mt_cd == value);
                            }
                        }
                        return Json(new { success = true, login = true, selecteditem = selecteditem }, JsonRequestBehavior.AllowGet);
                    }
                    else if (type == "ccompany")
                    {
                        List<MST_COM_SELECTED> selecteditem = (List<MST_COM_SELECTED>)Session["MST_COM_SELECTED"];
                        if (selecteditem != null && selecteditem.Count > 0)
                        {
                            if (selecteditem.Where(x => x.Mc_cd == value).Count() > 0)
                            {
                                selecteditem.RemoveAll((x) => x.Mc_cd == value);
                            }
                        }
                        return Json(new { success = true, login = true, selecteditem = selecteditem }, JsonRequestBehavior.AllowGet);
                    }
                    else if (type == "cmode")
                    {
                        List<MST_MS_SELECTED> selecteditem = (List<MST_MS_SELECTED>)Session["MST_MS_SELECTED"];
                        if (selecteditem != null && selecteditem.Count > 0)
                        {
                            if (selecteditem.Where(x => x.Mms_cd == value).Count() > 0)
                            {
                                selecteditem.RemoveAll((x) => x.Mms_cd == value);
                            }
                        }
                        return Json(new { success = true, login = true, selecteditem = selecteditem }, JsonRequestBehavior.AllowGet);
                    }
                    else if (type == "cpc")
                    {
                        List<MST_PC_SELECTED> selecteditem = (List<MST_PC_SELECTED>)Session["MST_PC_SELECTED"];
                        if (selecteditem != null && selecteditem.Count > 0)
                        {
                            if (selecteditem.Where(x => x.Mpc_cd == value).Count() > 0)
                            {
                                selecteditem.RemoveAll((x) => x.Mpc_cd == value);
                            }
                        }
                        return Json(new { success = true, login = true, selecteditem = selecteditem }, JsonRequestBehavior.AllowGet);
                    }
                    else if (type == "ccustomer")
                    {
                        List<MST_CUS_SELECTED> selecteditem = (List<MST_CUS_SELECTED>)Session["MST_CUS_SELECTED"];
                        if (selecteditem != null && selecteditem.Count > 0)
                        {
                            if (selecteditem.Where(x => x.Mbe_cd == value).Count() > 0)
                            {
                                selecteditem.RemoveAll((x) => x.Mbe_cd == value);
                            }
                        }
                        return Json(new { success = true, login = true, selecteditem = selecteditem }, JsonRequestBehavior.AllowGet);
                    }
                    else if (type == "cdistrict")
                    {
                        List<MST_DISTRICT_SELECTED> selecteditem = (List<MST_DISTRICT_SELECTED>)Session["MST_DISTRICT_SELECTED"];
                        if (selecteditem != null && selecteditem.Count > 0)
                        {
                            if (selecteditem.Where(x => x.mdis_cd == value).Count() > 0)
                            {
                                selecteditem.RemoveAll((x) => x.mdis_cd == value);
                            }
                        }
                        return Json(new { success = true, login = true, selecteditem = selecteditem }, JsonRequestBehavior.AllowGet);
                    }
                    else if (type == "cprovince")
                    {
                        List<MST_PROVINCE_SELECTED> selecteditem = (List<MST_PROVINCE_SELECTED>)Session["MST_PROVINCE_SELECTED"];
                        if (selecteditem != null && selecteditem.Count > 0)
                        {
                            if (selecteditem.Where(x => x.mpro_cd == value).Count() > 0)
                            {
                                selecteditem.RemoveAll((x) => x.mpro_cd == value);
                            }
                        }
                        return Json(new { success = true, login = true, selecteditem = selecteditem }, JsonRequestBehavior.AllowGet);
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
        public JsonResult addMapData(string type, string value)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {
                    Session["MAP"] = "MAP";
                    Session["MST_PDIST_SELECTED"] = null;
                    Session["MST_PPROV_SELECTED"] = null;
                    if (type == "DISTRICT")
                    {
                        List<MST_PDIST_SELECTED> selecteditem = (List<MST_PDIST_SELECTED>)Session["MST_PDIST_SELECTED"];
                        if (selecteditem != null && selecteditem.Count > 0)
                        {
                            if (selecteditem.Where(x => x.mds_dist_cd == value).Count() > 0)
                            {
                                selecteditem.RemoveAll((x) => x.mds_dist_cd == value);
                            }
                            else
                            {
                                MST_PDIST_SELECTED aded = new MST_PDIST_SELECTED();
                                aded.mds_dist_cd = value;
                                aded.mds_district = "";
                                selecteditem.Add(aded);
                            }
                        }
                        else
                        {
                            selecteditem = new List<MST_PDIST_SELECTED>();
                            MST_PDIST_SELECTED aded = new MST_PDIST_SELECTED();
                            aded.mds_dist_cd = value;
                            aded.mds_district = "";
                            selecteditem.Add(aded);
                        }
                        Session["MST_PDIST_SELECTED"] = selecteditem;
                        return Json(new { success = true, login = true, selecteditem = selecteditem }, JsonRequestBehavior.AllowGet);

                    }
                    else if (type == "REGION")
                    {
                        List<MST_PPROV_SELECTED> selecteditem = (List<MST_PPROV_SELECTED>)Session["MST_PPROV_SELECTED"];
                        if (selecteditem != null && selecteditem.Count > 0)
                        {
                            if (selecteditem.Where(x => x.mds_prov_cd == value).Count() > 0)
                            {
                                selecteditem.RemoveAll((x) => x.mds_prov_cd == value);
                            }
                            else
                            {
                                MST_PPROV_SELECTED aded = new MST_PPROV_SELECTED();
                                aded.mds_prov_cd = value;
                                aded.mds_province = "";
                                selecteditem.Add(aded);
                            }
                        }
                        else
                        {
                            selecteditem = new List<MST_PPROV_SELECTED>();
                            MST_PPROV_SELECTED aded = new MST_PPROV_SELECTED();
                            aded.mds_prov_cd = value;
                            aded.mds_province = "";
                            selecteditem.Add(aded);
                        }
                        Session["MST_PPROV_SELECTED"] = selecteditem;
                        return Json(new { success = true, login = true, selecteditem = selecteditem }, JsonRequestBehavior.AllowGet);

                    }
                    return Json(new { success = false, login = true, msg = "Invalid selection." }, JsonRequestBehavior.AllowGet);
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
        public JsonResult getCustomerDetails(//string selectedcompany, 
            DateTime SalesFrom, DateTime SalesTo, string CheckAmount, string CheckAge, string CheckSalary)
        {            
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            string Paymenttype = "";
            try
            {
                string map = Session["MAP"].ToString();
                string Channel = "";
                string Subchnl = "";
                string Area = "";
                string Region = "";
                string Zone = "";
                string pc = "";
                string selectedcompany = "";
                string Customer = "";
                string SalesType = ""; string SchemeType = ""; string SchemeCode = "";
                string CTown = ""; string PTown = ""; string BankCode = ""; string PtypeCode = "";
                string item = ""; string model = "";
                string maincat = ""; string cat2 = ""; string cat3 = ""; string town = ""; //string company = "";
                string purtown = ""; string invType = ""; string Brand = "";
                string dist = ""; string prov = ""; string mode = ""; string procenter = "";
                string district = ""; string province = "";

                List<MST_TOWN_SELECTED> tn = (List<MST_TOWN_SELECTED>)Session["MST_TOWN_SELECTED"];
                if (tn != null && tn.Count > 0)
                {
                    Int32 i = 1;
                    foreach (MST_TOWN_SELECTED cnl in tn)
                    {
                        town += (i == tn.Count) ? cnl.mt_desc : cnl.mt_desc + ",";
                        i++;
                    }
                }
                List<MST_COM_SELECTED> com = (List<MST_COM_SELECTED>)Session["MST_COM_SELECTED"];
                if (com != null && com.Count > 0)
                {
                    Int32 i = 1;
                    foreach (MST_COM_SELECTED cnl in com)
                    {
                        selectedcompany += (i == com.Count) ? cnl.Mc_cd : cnl.Mc_cd + ",";
                        i++;
                    }
                }
                if (selectedcompany == "NaN")
                {
                    return Json(new { success = false, login = true, msg = "Please select comany.", type = "Info" }, JsonRequestBehavior.AllowGet);

                }
                List<MST_MS_SELECTED> ms = (List<MST_MS_SELECTED>)Session["MST_MS_SELECTED"];
                if (ms != null && ms.Count > 0)
                {
                    Int32 i = 1;
                    foreach (MST_MS_SELECTED cnl in ms)
                    {
                        mode += (i == ms.Count) ? cnl.Mms_cd : cnl.Mms_cd + ",";
                        i++;
                    }
                }
                List<MST_PC_SELECTED> pro = (List<MST_PC_SELECTED>)Session["MST_PC_SELECTED"];
                if (pro != null && pro.Count > 0)
                {
                    Int32 i = 1;
                    foreach (MST_PC_SELECTED cnl in pro)
                    {
                        procenter += (i == pro.Count) ? cnl.Mpc_cd : cnl.Mpc_cd + ",";
                        i++;
                    }
                }

                List<MST_DISTRICT_SELECTED> distr = (List<MST_DISTRICT_SELECTED>)Session["MST_DISTRICT_SELECTED"];
                if (distr != null && distr.Count > 0)
                {
                    Int32 i = 1;
                    foreach (MST_DISTRICT_SELECTED cnl in distr)
                    {
                        district += (i == distr.Count) ? cnl.mdis_cd : cnl.mdis_cd + ",";
                        i++;
                    }
                }
                List<MST_PROVINCE_SELECTED> provi = (List<MST_PROVINCE_SELECTED>)Session["MST_PROVINCE_SELECTED"];
                if (provi != null && provi.Count > 0)
                {
                    Int32 i = 1;
                    foreach (MST_PROVINCE_SELECTED cnl in provi)
                    {
                        province += (i == provi.Count) ? cnl.mpro_cd : cnl.mpro_cd + ",";
                        i++;
                    }
                }
                if (map == "MAP")
                {
                    List<MST_PDIST_SELECTED> ptn1 = (List<MST_PDIST_SELECTED>)Session["MST_PDIST_SELECTED"];
                    if (ptn1 != null && ptn1.Count > 0)
                    {
                        Int32 i = 1;
                        foreach (MST_PDIST_SELECTED cnl in ptn1)
                        {
                            dist += (i == ptn1.Count) ? (((cnl.mds_district == "") ? cnl.mds_dist_cd : cnl.mds_district)) : ((cnl.mds_district == "") ? (cnl.mds_dist_cd + ",") : cnl.mds_district + ",");
                            i++;
                        }
                    }

                    List<MST_PPROV_SELECTED> ptn2 = (List<MST_PPROV_SELECTED>)Session["MST_PPROV_SELECTED"];
                    if (ptn2 != null && ptn2.Count > 0)
                    {
                        Int32 i = 1;
                        foreach (MST_PPROV_SELECTED cnl in ptn2)
                        {
                            prov += (i == ptn2.Count) ? (((cnl.mds_province == "") ? cnl.mds_prov_cd : cnl.mds_province)) : ((cnl.mds_province == "") ? (cnl.mds_prov_cd + ",") : cnl.mds_province + ",");

                            i++;
                        }
                    }
                }
                Session["MAP"] = "";
                if (CheckAge == "")
                {
                    CheckAge = "";
                }
                if (CheckSalary == "")
                {
                    CheckSalary = "";
                }
                if (CheckAmount == "")
                {
                    CheckAmount = "0";
                }
                if (selectedcompany == "")
                {
                    selectedcompany = "";
                }
                List<CUSTOMER_SALES> dt3 = new List<CUSTOMER_SALES>();
                //if (!included)
                //{
                dt3 = CHNLSVC.Sales.getCustomerDetails(selectedcompany, Channel, Subchnl, Area, Region, Zone, pc, SalesFrom, SalesTo, Brand, maincat, model, item, Convert.ToDecimal(CheckAmount), "", cat2, cat3, Convert.ToInt32(0), CheckAge, CheckSalary, Customer, invType, SchemeType, SchemeCode, town, purtown, BankCode, "", Paymenttype, userId, dist, prov);
                //dt3 = CHNLSVC.Sales.getCustomerDetails_new(town, selectedcompany, mode, procenter, district, province, dist, prov, SalesFrom, SalesTo, Convert.ToDecimal(CheckAmount), CheckAge, CheckSalary);
                //}
                //else
                //{
                //    List<CUSTOMER_SALES> dt = CHNLSVC.Sales.getCustomerDetails(selectedcompany, Channel, Subchnl, Area, Region, Zone, pc, SalesFrom, SalesTo, Brand, maincat, model, item, Convert.ToDecimal(CheckAmount), filterby, cat2, cat3, Convert.ToInt32(CheckVisit), CheckAge, CheckSalary, Customer, invType, SchemeType, SchemeCode, town, purtown, BankCode, Withserial, Paymenttype, userId, dist, prov);

                //    List<CUSTOMER_SALES> dt2 = CHNLSVC.Sales.getCustomerDetails(selectedcompany, Channel, Subchnl, Area, Region, Zone, pc, SalesTo, DateTime.Now, Brand, maincat, model, item, Convert.ToDecimal(CheckAmount), filterby, cat2, cat3, Convert.ToInt32(CheckVisit), CheckAge, CheckSalary, Customer, invType, SchemeType, SchemeCode, town, purtown, BankCode, Withserial, Paymenttype, userId, dist, prov);

                //    dt3 = new List<CUSTOMER_SALES>();
                //    foreach (CUSTOMER_SALES cs1 in dt)
                //    {
                //        bool add = true;
                //        foreach (CUSTOMER_SALES cs2 in dt2)
                //        {
                //            if (cs1.CUSTOMER_CODE.Equals(cs2.CUSTOMER_CODE))
                //            {
                //                add = false;
                //            }
                //        }
                //        if (add)
                //        {
                //            dt3.Add(cs1);
                //        }
                //    }
                //}

                //List<CUSTOMER_SALES> getCustomerInvDetails( selectedcompany, string Channel, string Subchnl, string Region, string Zone, string pc, DateTime SalesFrom, DateTime SalesTo, string Brand, string MainCat, string txtModel, string txtItem, decimal CheckAmount, string filterby, string cat2, string cat3, Int32 visit, Int32 age, Int32 salary, string customer, string invtype, string schemetype, string schemecode, string CTown, string PTown, string BankCode, string Withserial, string Paymenttype);

                //(selectedcompany, Channel, Subchnl, Region, Zone, pc, SalesFrom, SalesTo, Brand, cat1, model, item, Convert.ToDecimal(CheckAmount), filterby, cat2, cat3, Convert.ToInt32(CheckVisit), Convert.ToInt32(CheckAge), Convert.ToInt32(CheckSalary), Customer, SalesType, SchemeType, SchemeCode, CTown, PTown, BankCode, Withserial, PtypeCode);

                //(string selectedcompany, string Channel, string Subchnl, string Region, string Zone, string pc, DateTime SalesFrom, DateTime SalesTo, string Brand, string MainCat, string txtModel, string txtItem, decimal CheckAmount, string filterby, string cat2, string cat3, Int32 visit, Int32 age, Int32 salary, string invtype, string schemetype, string schemecode)
                if (dt3.Count > 0)
                {
                    dt3 = dt3.GroupBy(x => x.CUSTOMER_CODE).Select(y => y.First()).ToList();
                    Session["filterdata"] = dt3;
                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "No Data Found.", type = "Info" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult itmDetPaging(int? pgenum, int pgesize)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            List<CUSTOMER_SALES> finalDetails = Session["filterdata"] as List<CUSTOMER_SALES>;
            try
            {

                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
                {

                    var det = finalDetails.Skip(((pgenum - 1) ?? 0) * pgesize).Take(pgesize).ToList();

                    decimal pageCount = Math.Ceiling(finalDetails.Count / Convert.ToDecimal(pgesize));

                    if (finalDetails != null)
                    {
                        return Json(new { success = true, login = true, itmDetailsList = det, pageData = pageCount }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new { success = false, login = true, msg = "No as at details found for display", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
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
        public ActionResult Export()
        {

            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro))
            {
                List<CUSTOMER_SALES> data = new List<CUSTOMER_SALES>();
                //Load Data
                data = (List<CUSTOMER_SALES>)Session["filterdata"];
                if (data != null)
                {
                    string xml = String.Empty;
                    XmlDocument xmlDoc = new XmlDocument();

                    XmlSerializer xmlSerializer = new XmlSerializer(data.GetType());

                    using (MemoryStream xmlStream = new MemoryStream())
                    {
                        xmlSerializer.Serialize(xmlStream, data);
                        xmlStream.Position = 0;
                        xmlDoc.Load(xmlStream);
                        xml = xmlDoc.InnerXml;
                    }

                    var fName = "Customer Salses details.xls";

                    byte[] fileContents = Encoding.UTF8.GetBytes(xml);

                    return File(fileContents, "application/vnd.ms-excel", fName);
                    //return File(fileContents, "application/xls", fName);
                }
                else
                {
                    return File("", "application/vnd.ms-excel", "");
                }

            }
            else
            {
                return Redirect("~/Login/index");
            }

        }
        public ActionResult SendSMS(string mobileNumber, string sms, string name)
        {
            bool message = false;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            if (IsDigitsOnly(mobileNumber) && (mobileNumber.Length == 9 || mobileNumber.Length == 10))
            {
                message = CHNLSVC.General.SendSms(mobileNumber, sms, name, HttpContext.Session["UserID"] as string, company);
            }

            if (message)
            {
                return Json(new { success = true, login = true, message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = true, message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SendSMStoAll(string sms)
        {
            List<CUSTOMER_SALES> dt = new List<CUSTOMER_SALES>();
            string company = HttpContext.Session["UserCompanyCode"] as string;
            dt = Session["filterdata"] as List<CUSTOMER_SALES>;
            bool message = false;
            dt = dt.GroupBy(x => x.MOBILE).Select(y => y.First()).ToList();
            foreach (CUSTOMER_SALES cs1 in dt)
            {
                if (IsDigitsOnly(cs1.MOBILE) && (cs1.MOBILE.Length == 9 || cs1.MOBILE.Length == 10))
                {
                    message = CHNLSVC.General.SendSms(cs1.MOBILE, sms, cs1.NAME, HttpContext.Session["UserID"] as string, company);
                }
            }


            if (message)
            {
                return Json(new { success = true, login = true, message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = true, message }, JsonRequestBehavior.AllowGet);
            }
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}