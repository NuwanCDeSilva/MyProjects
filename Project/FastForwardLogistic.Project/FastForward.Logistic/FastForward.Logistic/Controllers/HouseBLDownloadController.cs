using FF.BusinessObjects.Genaral;
using FF.BusinessObjects.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastForward.Logistic.Controllers
{
    public class HouseBLDownloadController : BaseController
    {
        // GET: HouseBLDownload
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserId"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                SEC_SYSTEM_MENU per = CHNLSVC.Security.getUserPermission(userId, company, 33);
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
                return Redirect("~/Home/index");
            }
        }
        public ActionResult Download(List<string> selectedBl,string MasterBLNumber)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
          
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                try
                {
                    string finstring="";
                    if (selectedBl.Count > 0)
                    {
                        Int32 i = 1;
                        foreach (string bl in selectedBl)
                        {
                            finstring += replaceValueSegmant(bl.Trim(), company,i);
                            i++;
                        }
                        string hdr = replaceHeader(selectedBl[0], company);
                        finstring = hdr.Replace("@segmant", finstring);
                        Response.Clear();
                        Response.ContentType = "text/xml";
                        Response.AddHeader("content-disposition", "attachment;filename=" + selectedBl[0] + ".xml");
                        Response.Write(finstring);
                        //Response.Write(version + "\n" + xmlHed.ToString()  );
                        Response.End();
                    }
                   
                    return View();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }
        public JsonResult validateDocument(string blno)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
          
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                try
                {
                    List<trn_bl_header> _hdr = CHNLSVC.General.GetBLHdr(blno, company);
                    if (_hdr == null || _hdr.Count == 0 || _hdr[0].Bl_doc_no==null)
                     {
                         return Json(new { success = false, login = true, msg = "Please enter valid document number." }, JsonRequestBehavior.AllowGet);

                     }
                    List<HBLSelectedData> hbl = CHNLSVC.General.GetHBLNumbersForMaster(blno);
                    if (hbl == null || hbl.Count == 0)
                    {
                        return Json(new { success = false, login = true, msg = "No house BL found for this master BL number." }, JsonRequestBehavior.AllowGet);

                    }
                    return Json(new { success = true, login = true, data = hbl }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    return Json(new { success = false, login = false, msg=ex.Message.ToString() }, JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                return Json(new { success = false, login = false}, JsonRequestBehavior.AllowGet);
            }
        }
        public string replaceHeader(string hseblno, string company)
        {
            try
            {
                string fileName = Server.MapPath("~") + "XML\\HouseBL.xml";
                string xmlString = System.IO.File.ReadAllText(fileName);
                List<trn_bl_header> _hdr = CHNLSVC.General.GetBLHdr(hseblno, company);
                if (_hdr != null && _hdr.Count > 0)
                {
                    trn_bl_header hdr = _hdr[0];
                    xmlString = xmlString.Replace("@cusofcd", "SECMB");
                    xmlString = xmlString.Replace("@vygno", hdr.Bl_voage_no);
                    xmlString = xmlString.Replace("@dteofdp", hdr.Bl_est_time_dep.ToString("yyyy-MM-dd"));
                    xmlString = xmlString.Replace("@refnum", (hdr.bl_manual_m_ref != null) ? hdr.bl_manual_m_ref.ToString() : "");
                }
                return xmlString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string   replaceValueSegmant(string hseblno, string company,Int32 line)
        {
            try
            {
                string fileName = Server.MapPath("~") + "XML\\Segmant.xml";
                string xmlString = System.IO.File.ReadAllText(fileName);
                List<trn_bl_header> _hdr = CHNLSVC.General.GetBLHdr(hseblno, company);
                if (_hdr != null && _hdr.Count > 0)
                {
                    List<trn_bl_det> _bl_det = CHNLSVC.General.GetBLitemdetails(_hdr.First().Bl_seq_no);
                    List<trn_bl_cont_det>  _cont = CHNLSVC.General.GetBLContainer(_hdr.First().Bl_seq_no);
                    trn_bl_header hdr = _hdr[0];


                    xmlString = xmlString.Replace("@bolref", hdr.bl_manual_h_ref.ToString());
                    xmlString = xmlString.Replace("@lineno", line.ToString());
                    xmlString = xmlString.Replace("@bmlnet", "23");
                    xmlString = xmlString.Replace("@bmltyp", "HSB");
                    xmlString = xmlString.Replace("@concrgo", "0");
                    xmlString = xmlString.Replace("@plcoflod", hdr.Bl_port_load.ToString());
                    xmlString = xmlString.Replace("@plcofunlod", hdr.Bl_port_discharge.ToString());
                    if (company == "CCD")
                    {
                        xmlString = xmlString.Replace("@carcd", "FF197");
                        xmlString = xmlString.Replace("@carnme", "Crown City Developers(Pvt) Ltd.");
                        xmlString = xmlString.Replace("@caradd", "No. 46/4,5th Floor,IBM Building II,Nawam Mawatha,Colombo 02,Sri Lanka");
                    }
                    else if (company == "ALO")
                    {
                        xmlString = xmlString.Replace("@carcd", "FF498");
                        xmlString = xmlString.Replace("@carnme", "ABANS Logistics (Pvt) Ltd.");
                        xmlString = xmlString.Replace("@caradd", "No.46/4,5th Floor,IBM Building II,Nawam Mawatha,Colombo 02,Sri Lanka");
               
                    }


                    xmlString = xmlString.Replace("@expnme", (hdr.Bl_shipper_name.Length > 35) ? hdr.Bl_shipper_name.Substring(0, 35) : hdr.Bl_shipper_name);
                    xmlString = xmlString.Replace("@expadd", hdr.Bl_shipper_add1.Replace("&", "&amp; ") + "," + hdr.Bl_shipper_add2.Replace("&", "&amp; "));
                    xmlString = xmlString.Replace("@ntfynme", (hdr.Bl_ntfy_party_name.Length > 35) ? hdr.Bl_ntfy_party_name.Substring(0, 35) : hdr.Bl_ntfy_party_name);
                    xmlString = xmlString.Replace("@ntfyadd", hdr.Bl_ntfy_party_add1.Replace("&", "&amp; ") + "," + hdr.Bl_ntfy_party_add2.Replace("&", "&amp; "));
                    xmlString = xmlString.Replace("@consnme", (hdr.Bl_consignee_name.Length>35) ? hdr.Bl_consignee_name.Substring(0, 35) :hdr.Bl_consignee_name) ;
                    xmlString = xmlString.Replace("@consadd", hdr.Bl_consignee_add1.Replace("&", "&amp; ") + "," + hdr.Bl_consignee_add2.Replace("&", "&amp; "));

                    string fileNamecont = Server.MapPath("~") + "XML\\Container.xml";
                    
                    string val = "";
                    foreach (trn_bl_cont_det c in _cont)
                    {
                        string cont = System.IO.File.ReadAllText(fileNamecont);
                        cont = cont.Replace("@ctnref", c.Blct_cont_no);
                        cont = cont.Replace("@ctnnumofpkg", c.Blct_pack);
                        cont = cont.Replace("@ctntypofcon",c.blct_con_tp);
                        cont = cont.Replace("@ctnemtful", (c.Blct_fully_empty.Length == 1) ? "0" + c.Blct_fully_empty : c.Blct_fully_empty);
                        val += cont;
                    }
                    xmlString = xmlString.Replace(" @Container", val);

                    xmlString = xmlString.Replace("@gdnumofpkg", _cont.Sum(x => Convert.ToDecimal(x.Blct_pack)).ToString());
                    xmlString = xmlString.Replace("@gdnpkgtycd", hdr.bl_pack_uom.ToString());
                    xmlString = xmlString.Replace("@grsmas", _bl_det[0].bld_grs_weight.ToString());
                    xmlString = xmlString.Replace("@shipmks", _bl_det[0].bld_mark_nos.ToString().Replace("&", "&amp; "));
                    xmlString = xmlString.Replace("@godsdesc", _bl_det[0].bld_desc_goods.ToString().Replace("&", "&amp; "));
                    xmlString = xmlString.Replace("@volcubmet", _bl_det[0].bld_measure.ToString());
                    xmlString = xmlString.Replace("@volctnbol", _cont.Count.ToString());
                    xmlString = xmlString.Replace("@info", hdr.Bl_rmk.ToString().Replace("&", "&amp; "));
                    return xmlString;
                }
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}