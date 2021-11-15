using FastForward.WebASYCUDA.Models;
using FF.BusinessObjects.Asycuda;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace FastForward.WebASYCUDA.Controllers
{
    public class CusdecController : BaseController
    {
        private string ComErrorMsg=null;
        // GET: Cusdec view action
        /// <summary>
        /// index action
        /// </summary>
        /// <returns>ActionResult</returns>
        public const string SchemeId = "071B4252-4D0D-4FBD-93ED-F42A26554618";
        public const string BaseAddress = "http://nuv-uat.southeastasia.cloudapp.azure.com/abans/";
        public const string SecurityToken = "<Request from NUV administrator>";
        public const string BranchId = "94D7AE72-124D-424C-80BE-76073B22257A";
        string type;
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            if (string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(company))
            {

                return RedirectToAction("Login","Login");
            }
            else
            {
                //User user = CHNLSVC.Asycuda.getLoyUser("8071001090486312", SchemeId, BaseAddress, SecurityToken, BranchId,"1");
                if (TempData["Notice"] != null) {
                    ViewBag.Notice = TempData["Notice"];
                }
                else if (TempData["Error"] != null)
                {
                    ViewBag.ErrorMsg = TempData["Error"];
                }
                return View();
            }
        }
        
        /// <summary>
        /// get database list for drop down
        /// </summary>
        /// <returns>JsonResult</returns>
        public JsonResult GetDatabaseList() {
            string userId = HttpContext.Session["UserID"] as string;
            try
            {
                 if (!string.IsNullOrEmpty(userId))
                 {
                     List<ASY_DB_SOURCE> _asyDatabaseList = new List<ASY_DB_SOURCE>();
                     _asyDatabaseList= CHNLSVC.Security.GetSystemDatabaseList();
                     if (_asyDatabaseList != null)
                     {
                         return Json(new { success = true, login = true, data = _asyDatabaseList }, JsonRequestBehavior.AllowGet);
                     }
                     else {
                         return Json(new { success = false, login = true, msg = Resource.DbSouceErr }, JsonRequestBehavior.AllowGet);
                     }
                     
                 }
                 else 
                 {
                     return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet); 
                 }
            }
            catch (Exception ex) {
                if (!string.IsNullOrEmpty(userId))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
               
            }
        }

        /// <summary>
        /// get group list for drop down
        /// </summary>
        /// <param name="database">database</param>
        /// <returns>JsonResult</returns>
        public JsonResult GetGroupList(string database)
        {
            string userId = HttpContext.Session["UserID"] as string;
            try
            {
                    if (!string.IsNullOrEmpty(userId))
                    {
                        List<ASY_DOC_GRUP> _asyGroupList = new List<ASY_DOC_GRUP>();
                        _asyGroupList = CHNLSVC.Security.GetAsycudaGrpList();
                        if (!string.IsNullOrEmpty(database))
                        {
                            if (_asyGroupList != null)
                            {
                                return Json(new { success = true, login = true, data = _asyGroupList }, JsonRequestBehavior.AllowGet);
                            }
                            else {
                                return Json(new { success = false, login = true, msg = Resource.EmptyGrp }, JsonRequestBehavior.AllowGet);
                            }
                            
                        }
                        else
                        {
                            return Json(new { success = false, login = true, msg = Resource.SelectDatabase }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                    }
                
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        /// <summary>
        /// get document type list for cusdec doctype dropdown
        /// </summary>
        /// <param name="database">database id</param>
        /// <param name="groupid">group id</param>
        /// <returns>JsonResult</returns>
        public JsonResult GetTypeList(string database, string groupid) 
        {
            string userId = HttpContext.Session["UserID"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    List<ASY_DOC_TP> _asyTypeList = new List<ASY_DOC_TP>();
                    if (!string.IsNullOrEmpty(database) && !string.IsNullOrEmpty(groupid))
                    {
                        _asyTypeList = CHNLSVC.Security.GetAsycudaTypeList(groupid, database);
                        return Json(new { success = true, login = true, data = _asyTypeList }, JsonRequestBehavior.AllowGet);
                    }
                    else {
                        return Json(new { success = false, login = true, msg = Resource.SelectGroup }, JsonRequestBehavior.AllowGet);
                    }
                }
                else {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex) {
                if (!string.IsNullOrEmpty(userId))
                {
                    return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        /// <summary>
        ///check details for genarate xml and download the xml
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult GenarateXml()
        {
            string userId = HttpContext.Session["UserID"] as string;
            Session["ExportPath"] = CHNLSVC.Security.GetMasterCompanyPath(HttpContext.Session["UserCompanyCode"] as string);
            bool _checkRange = false;

            if(Session["DocNumbers"]!=null)
            {
                List<string> checkList = Session["DocNumbers"] as List<string>;
                if(checkList.Count>1)
                {
                    _checkRange = true;
                }
            }

            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    if (!string.IsNullOrEmpty(Request.Form["DBModelList.Add_db_id"])
                        && !string.IsNullOrEmpty(Request.Form["DocGrpList.Adg_grup_id"])
                        && !string.IsNullOrEmpty(Request.Form["DocTypeList.Adt_tp_id"])
                        && !string.IsNullOrEmpty(Request.Form["Doc_Number"]))
                    {
                        string documentNo = Request.Form["Doc_Number"].Trim();                        
                        int dbsrcId = int.Parse(Request.Form["DBModelList.Add_db_id"].ToString());
                        int goupId = int.Parse(Request.Form["DocGrpList.Adg_grup_id"].ToString());
                        int goupTypeId = int.Parse(Request.Form["DocTypeList.Adt_tp_id"].ToString());
                        string documentNoTo = Request.Form["Doc_Number_To"].Trim();                     

                        string error = "";
                        if (!string.IsNullOrEmpty(Request.Form["request"]) && Request.Form["request"] == "req")
                        {
                            if (documentNoTo.Equals(string.Empty))
                            {
                                string asyType = CHNLSVC.Security.getAsyTypeCodefromId(goupTypeId, goupId);
                                if (!string.IsNullOrEmpty(asyType))
                                {
                                    bool available = CHNLSVC.Security.CheckDocumentAvailability(documentNo, dbsrcId, asyType, out error);

                                    //Added By Dulaj 2018/Jul/09                                    
                                    List<string> docNumbers = new List<string>();                                   
                                   
                                    if (available)
                                    {
                                        docNumbers.Add(documentNo);
                                    }
                                     Session["DocNumbers"] = docNumbers;
                                    ///
                                    if (error != "")
                                    {
                                        return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                                    }
                                    if (available == false)
                                    {
                                        return Json(new { success = false, login = true, msg = "Please enter valid document number." }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        ASY_ALT_HEADER_DTLS _altDet = new ASY_ALT_HEADER_DTLS();
                                        _altDet = CHNLSVC.Security.SetAlterHeaderDetails(documentNo, dbsrcId, asyType, out error);
                                        if (_altDet == null || _altDet.Ach_doc_no == null)
                                        {
                                            return Json(new { success = false, login = true, msg = "Invalid document number." }, JsonRequestBehavior.AllowGet);
                                        }
                                        if (error != "")
                                        {
                                            return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Invalid document type selected." }, JsonRequestBehavior.AllowGet);

                                }
                            }
                            else
                            {
                                string asyType = CHNLSVC.Security.getAsyTypeCodefromId(goupTypeId, goupId);
                                if (!string.IsNullOrEmpty(asyType))
                                {
                                    bool available = CHNLSVC.Security.CheckDocumentAvailability(documentNo, dbsrcId, asyType, out error);
                                    _checkRange = true;
                                    //Added By Dulaj 2018/Jul/09
                                    DataTable dataTypeTable = CHNLSVC.Security.GetDocNoListForAsyCuda(documentNo, documentNoTo, asyType);
                                    List<string> docNumbers = new List<string>();
                                    foreach (DataRow dr in dataTypeTable.Rows)
                                    {
                                        string docno = dr["CUH_DOC_NO"].ToString();
                                        if (CHNLSVC.Security.CheckDocumentAvailability(docno, dbsrcId, asyType, out error))
                                        { docNumbers.Add(docno); }
                                    }
                                    Session["DocNumbers"] = docNumbers;
                                    ///
                                    if (error != "")
                                    {
                                        return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                                    }
                                    if (available == false)
                                    {
                                        return Json(new { success = false, login = true, msg = "Please enter valid document number." }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        ASY_ALT_HEADER_DTLS _altDet = new ASY_ALT_HEADER_DTLS();
                                        _altDet = CHNLSVC.Security.SetAlterHeaderDetails(documentNo, dbsrcId, asyType, out error);
                                        if (_altDet == null || _altDet.Ach_doc_no == null)
                                        {
                                            return Json(new { success = false, login = true, msg = "Invalid document number." }, JsonRequestBehavior.AllowGet);
                                        }
                                        if (error != "")
                                        {
                                            return Json(new { success = false, login = true, msg = error }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    return Json(new { success = true, login = true }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, login = true, msg = "Invalid document type selected." }, JsonRequestBehavior.AllowGet);

                                }
                            }
                        }
                        else
                        {
                            //Modified By Dulaj 2018/Jul/10 
                            if (_checkRange)
                            {
                                List<string> docNumberList = Session["DocNumbers"] as List<string>;
                                foreach (string docNo in docNumberList)
                                {
                                    bool response = genarateXml(docNo, dbsrcId, goupId, goupTypeId, _checkRange);
                                    if (response == true)
                                    {
                                        TempData["Notice"] = Resource.FileDownloadCmpltMsg;
                                        TempData["Notice"] = " XML files are generated, location is : "+Session["ExportPath"].ToString()+"XMLDocs/";
                                    }
                                    else
                                    {
                                        TempData["Error"] = ComErrorMsg;
                                        ComErrorMsg = null;
                                    }
                                }
                            }
                            else
                            {
                                List<string> docNumberList = Session["DocNumbers"] as List<string>;
                                if (docNumberList.Count > 0)
                                {
                                    bool response = genarateXml(docNumberList[0], dbsrcId, goupId, goupTypeId, _checkRange);
                                    if (response == true)
                                    {
                                        TempData["Notice"] = Resource.FileDownloadCmpltMsg;
                                    }
                                    else
                                    {
                                        TempData["Error"] = ComErrorMsg;
                                        ComErrorMsg = null;
                                    }
                                }
                            }
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Request.Form["request"]) && Request.Form["request"] == "req")
                        {
                            if (string.IsNullOrEmpty(Request.Form["DBModelList.Add_db_id"]))
                            {
                                return Json(new { success = false, login = true, msg = Resource.SelectDatabase }, JsonRequestBehavior.AllowGet);

                            }
                            else if (string.IsNullOrEmpty(Request.Form["DocGrpList.Adg_grup_id"]))
                            {
                                return Json(new { success = false, login = true, msg = Resource.SelectGroup }, JsonRequestBehavior.AllowGet);

                            }
                            else if (string.IsNullOrEmpty(Request.Form["DocTypeList.Adt_tp_id"]))
                            {
                                return Json(new { success = false, login = true, msg = Resource.SelectType }, JsonRequestBehavior.AllowGet);

                            }
                            else
                            {
                                return Json(new { success = false, login = true, msg = Resource.DocNumberEmpty }, JsonRequestBehavior.AllowGet);
                            }

                        }
                        else
                        {
                            if (string.IsNullOrEmpty(Request.Form["DBModelList.Add_db_id"]))
                            {
                                ViewBag.ErrorMsg = Resource.SelectDatabase;
                                return RedirectToAction("Index", "Cusdec");

                            }
                            else if (string.IsNullOrEmpty(Request.Form["DocGrpList.Adg_grup_id"]))
                            {
                                ViewBag.ErrorMsg = Resource.SelectGroup;
                                return RedirectToAction("Index", "Cusdec");

                            }
                            else if (string.IsNullOrEmpty(Request.Form["DocTypeList.Adt_tp_id"]))
                            {
                                ViewBag.ErrorMsg = Resource.SelectType;
                                return RedirectToAction("Index", "Cusdec");
                            }
                            else
                            {
                                ViewBag.ErrorMsg = Resource.DocNumberEmpty;
                                return RedirectToAction("Index", "Cusdec");
                            }

                        }
                    }
                }
                else {
                    if (!string.IsNullOrEmpty(Request.Form["request"]) && Request.Form["request"] == "req")
                    {
                        return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                    }
                    else {
                        return RedirectToAction("Login", "Login");
                    }
                   
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(Request.Form["request"]) && Request.Form["request"] == "req")
                {
                    if (!string.IsNullOrEmpty(userId))
                    {
                        return Json(new { success = false, login = true, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, login = false, msg = Resource.ServerError }, JsonRequestBehavior.AllowGet);
                    }
                }
                else {
                    if (!string.IsNullOrEmpty(userId))
                    {
                        ViewBag.ErrorMsg = Resource.ServerError;
                        return RedirectToAction("Index", "Cusdec");
                    }
                    else {
                        ViewBag.ErrorMsg = Resource.ServerError;
                        return RedirectToAction("Login", "Login");
                    }
                }
            }
        }
       /// <summary>
       /// genarate xml
       /// </summary>
       /// <param name="documentNo">document number</param>
       /// <param name="dbsrcId">database source id</param>
       /// <param name="goupId">group id</param>
       /// <param name="goupTypeId">group type id</param>
       /// <returns></returns>
        public bool genarateXml(string documentNo, int dbsrcId, int goupId, int goupTypeId,bool _checkRange)
        {
            try{

                string asyType = CHNLSVC.Security.getAsyTypeCodefromId(goupTypeId, goupId);
                string error = "";
                
                if (!string.IsNullOrEmpty(asyType))
                {
                    bool available = CHNLSVC.Security.CheckDocumentAvailability(documentNo, dbsrcId, asyType,out error);
                    if (error != "") {
                        ComErrorMsg = error;
                        return false;
                    }
                    if (available == true)
                    {
                        if (asyType == "BOI" || asyType == "EXP")
                        {
                            ASY_IMP_CUSDEC_HDR heder = CHNLSVC.Security.GetCusdecHdrDetails(documentNo, dbsrcId);
                            if (heder.CUH_DOC_NO != null) {
                                List<ASY_CUSDEC_ITEM_DTLS_MODEL> itms = CHNLSVC.Security.GetAsycudaItemsListDetails(documentNo, dbsrcId, asyType);
                                if (itms.Count > 0) {
                                    return genaratAlterDownXml(documentNo, heder, itms);
                                }
                            }
                            return true;
                        }
                        else {
                            ASY_ALT_HEADER_DTLS _altDet = new ASY_ALT_HEADER_DTLS();
                            _altDet = CHNLSVC.Security.SetAlterHeaderDetails(documentNo, dbsrcId, asyType, out error);
                            if (error != "")
                            {
                                ComErrorMsg = error;
                                return false;
                            }
                            if (_altDet != null)
                            {
                                List<ASY_CUSDEC_ITM> _ItemListMode = new List<ASY_CUSDEC_ITM>();
                                _ItemListMode = CHNLSVC.Security.GetItems(documentNo, dbsrcId, asyType, _altDet, out error);
                                if (string.IsNullOrEmpty(error))
                                {
                                    return genaratDownXml(documentNo, _altDet, _ItemListMode, asyType, documentNo, _checkRange);
                                }
                                else
                                {
                                    ComErrorMsg = error;
                                    return false;
                                }
                            }
                            else
                            {
                                ComErrorMsg = Resource.ErrLoadData;
                                return false;
                            }
                        }
                       
                    }
                    else {
                        ComErrorMsg = Resource.InvalidDocNo;
                        return false;
                    }
                }
                else {
                    ComErrorMsg = Resource.InvalidDocType;
                    return false;
                
                }

            }catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// genarate and download xml
        /// </summary>
        /// <param name="docnum">document number</param>
        /// <param name="_altDet">header detail object</param>
        /// <param name="itemList">item list object</param>
        public bool genaratDownXml(string docnum, ASY_ALT_HEADER_DTLS _altDet, List<ASY_CUSDEC_ITM> itemList, string asyType, string docno,bool checkRange)
        {
            try
            {
                //XElement docEle = new XElement("Asycuda");
                string version = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
                string xmlHed = ReplaceHeaderValue(_altDet, asyType,docno);
                string xmlItems = ReplaceItemValue(itemList, asyType, _altDet);
                //List<ASY_XML_TAG> subEle = getChildNodes(1);
                //List<ASY_XML_TAG> subEleItm = getChildNodesForItm(1);
                //Dictionary<string, string> DataDi = AltDetArray(_altDet);

                //XElement xmlHed = xmlString(docEle, subEle, DataDi, "HED");
                //int i = 0;
                //foreach (ASY_CUSDEC_ITM item in itemList)
                //{
                //    i = i + 1;
                //    Dictionary<string, string> itmData = AltItmArray(item);
                //    xmlHed = xmlString(xmlHed, subEleItm, itmData, "ITEM");

                //    string path = @"C:\Users\nuwanc.ABANS\Desktop\WriteText.txt";
                //    using (StreamWriter file = new StreamWriter(path, true))
                //    {
                //        file.WriteLine(DateTime.Now.ToString());
                //    }


                //}
                if (checkRange)
                {
                    //Modified By Dulaj 2018/Jul/10 Remove Response
                   
                    string xml = version + "\n<ASYCUDA>" + "\n" + xmlHed.ToString() + "\n" + xmlItems.ToString() + "\n</ASYCUDA>";
                    XDocument doc = XDocument.Parse(xml);
                    string exportPath = Session["ExportPath"].ToString().Trim();
                    //doc.Save(@"C:\temp\"+docno+".xml");
                    string dir = "XMLDocs/";
                    exportPath = exportPath + dir;
                    doc.Save(exportPath.Trim() + docno + ".xml");
                }
                else
                {
                    Response.Clear();
                    Response.ContentType = "text/xml";
                    Response.AddHeader("content-disposition", "attachment;filename=" + docnum + ".xml");
                    Response.Write(version + "\n<ASYCUDA>" + "\n" + xmlHed.ToString() + "\n" + xmlItems.ToString() + "\n</ASYCUDA>");
                    //Response.Write(version + "\n" + xmlHed.ToString()  );
                    Response.End();
                }
             
                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// replace item list details xml parameter values
        /// </summary>
        /// <param name="itemList">item list object</param>
        /// <returns>string</returns>
        public string ReplaceItemValue(List<ASY_CUSDEC_ITM> itemList,string doctp,ASY_ALT_HEADER_DTLS _altDet)
        {
            try
            {
                string retStr = "";
                if (itemList != null)
                {
                    string itemXml = "";
                    if (doctp == "MV")
                    {
                         itemXml = CHNLSVC.Security.getDocumentXml("ITMMV");

                    }
                    else
                    {
                         itemXml = CHNLSVC.Security.getDocumentXml("ITM");

                    }
                    Int32 i = 1;
                    foreach (ASY_CUSDEC_ITM item in itemList)
                    {
                        
                        
                        string itmXml = itemXml;
                        if (itemList.Count == i)
                        {
                            decimal pk = (Convert.ToDecimal(_altDet.Ach_tot_pkg)- itemList.Sum(x => x.Ach_num_of_pkg));
                            itmXml = itmXml.Replace("@numofpkg", (item.Ach_num_of_pkg != 0) ? Math.Round(item.Ach_num_of_pkg + pk, 2).ToString() : "0");
                        }
                        else
                        {
                            itmXml = itmXml.Replace("@numofpkg", (item.Ach_num_of_pkg != 0) ? Math.Round(item.Ach_num_of_pkg, 2).ToString() : "0");
                        }                     
                        
                        
                        itmXml = itmXml.Replace("@mk1pkg", item.Ach_mrk1_pkg);                        
                        if (doctp == "EX" || doctp == "RE")
                        { itmXml = itmXml.Replace("@mk2pkg", (item.Ach_to_bond_no+ " " + item.Ach_cus_dec_no + " LINE NO: " + item.Ach_to_bond_line_no.ToString()).Replace("&", " &amp;")); }
                        else
                        { itmXml = itmXml.Replace("@mk2pkg", item.Aci_quota.Replace("&", " &amp;")); }                    
                        itmXml = itmXml.Replace("@kndofpkgcd", item.Ach_knd_of_pkg);
                        itmXml = itmXml.Replace("@pkgname", item.Ach_knd_of_pkg_name);
                        itmXml = itmXml.Replace("@commcd", item.Aci_comd_cd);
                        itmXml = itmXml.Replace("@prec1", item.Aci_prec_1);
                        itmXml = itmXml.Replace("@prec4", /*item.Aci_prec_4*/"<null/>");
                        itmXml = itmXml.Replace("@extcustproc", item.Ach_ext_cust_proc);
                        itmXml = itmXml.Replace("@natcusproc", item.Ach_nat_cust_proc);
                        itmXml = itmXml.Replace("@supunitcd", (item.Aci_qty != 0) ? Math.Round(item.Aci_qty,2).ToString() : "0");
                        if (doctp=="MV")
                        {
                            itmXml = itmXml.Replace("@codessa", (item.Aci_qty != 0) ? Math.Round(item.Aci_qty, 2).ToString() : "0");
                            itmXml = itmXml.Replace("@rested", "0");

                        }
                        itmXml = itmXml.Replace("@itmprce", (item.Aci_item_price != 0) ? Math.Round(item.Aci_item_price,2).ToString() : "0");
                        itmXml = itmXml.Replace("@valitm", item.Ach_val_itm);
                        itmXml = itmXml.Replace("@cntyoforgn", item.Ach_cnty_of_oregn);
                        itmXml = itmXml.Replace("@desofgods", (item.Aci_desc_of_goods != null) ? item.Aci_desc_of_goods.Replace("&", " &amp;") : "");
                        itmXml = itmXml.Replace("@comdesc", item.Aci_model);
                        if (doctp == "RE")
                        {
                            itmXml = itmXml.Replace("@sumdecl", _altDet.Ach_doc_no.Replace("&", " &amp;"));

                        }
                        else
                        {
                            itmXml = itmXml.Replace("@sumdecl", item.Aci_bl_no.Replace("&", " &amp;"));
                        }
                        itmXml = itmXml.Replace("@groswght", (item.Aci_gross_mass != 0) ? Math.Round(item.Aci_gross_mass,2).ToString() : "0");
                        itmXml = itmXml.Replace("@netwght", (item.Aci_net_mass != 0) ? Math.Round(item.Aci_net_mass,2).ToString() : "0");
                        itmXml = itmXml.Replace("@totcst", (item.Aci_tot_cost_itm != 0) ? Math.Round(item.Aci_tot_cost_itm,2).ToString() : "0");
                        itmXml = itmXml.Replace("@cifitm", "");
                        itmXml = itmXml.Replace("@rteofadj", item.Aci_rte_of_adj);
                        itmXml = itmXml.Replace("@ststval", (item.Aci_itm_stat_val != 0) ? Math.Round(item.Aci_itm_stat_val,2).ToString() : "0");
                        itmXml = itmXml.Replace("@invamntnatcur", (item.Aci_inv_nat_curr != 0) ? Math.Round(item.Aci_inv_nat_curr,2).ToString() : "0");
                        itmXml = itmXml.Replace("@invamntfogncur", (item.Aci_inv_forgn_curr != 0) ? item.Aci_inv_forgn_curr.ToString() : "0");
                        itmXml = itmXml.Replace("@invcurcd", item.Ach_cur_cd);
                        itmXml = itmXml.Replace("@invcurname", "No foreign currency");
                        itmXml = itmXml.Replace("@itmextnatcur", (item.Aci_ext_fre_nat_curr != 0) ? Math.Round(item.Aci_ext_fre_nat_curr,2).ToString() : "0");
                        itmXml = itmXml.Replace("@itmextfogncur", (item.Aci_ext_fre_forgn_curr != 0) ? Math.Round(item.Aci_ext_fre_forgn_curr,2).ToString() : "0");
                        itmXml = itmXml.Replace("@itmextcurcd", item.Ach_cur_cd);
                        itmXml = itmXml.Replace("@itmextcurname", "No foreign currency");
                        itmXml = itmXml.Replace("@itmintnatcur", (item.Aci_int_fre_nat_curr != 0) ? Math.Round(item.Aci_int_fre_nat_curr,2).ToString() : "0");
                        itmXml = itmXml.Replace("@itmintfogncur", (item.Aci_int_fre_forgn_curr != 0) ? Math.Round(item.Aci_int_fre_forgn_curr,2).ToString() : "0");
                        itmXml = itmXml.Replace("@itmintcurcd", item.Ach_cur_cd);
                        itmXml = itmXml.Replace("@itmintcurname", "No foreign currency");
                        itmXml = itmXml.Replace("@itmothnatcur", (item.Aci_oth_cst_nat_curr != 0) ? Math.Round(item.Aci_oth_cst_nat_curr,2).ToString() : "0");
                        itmXml = itmXml.Replace("@itmothfogncur", (item.Aci_oth_cst_forgn_curr != 0) ? Math.Round(item.Aci_oth_cst_forgn_curr,2).ToString() : "0");
                        itmXml = itmXml.Replace("@itmothcurcd", item.Ach_cur_cd);
                        itmXml = itmXml.Replace("@itmothcurname", "No foreign currency");
                        itmXml = itmXml.Replace("@itmdednatcur", (item.Aci_dedu_nat_curr != 0) ? Math.Round(item.Aci_dedu_nat_curr,2).ToString() : "0");
                        itmXml = itmXml.Replace("@itmdedfogncur", (item.Aci_dedu_forgn_curr != 0) ? Math.Round(item.Aci_dedu_forgn_curr,2).ToString() : "0");
                        itmXml = itmXml.Replace("@itmdedcurcd", item.Ach_cur_cd);
                        itmXml = itmXml.Replace("@itmdedcurname", "No foreign currency");
                        itmXml = itmXml.Replace("@prefcode", "<null/>");
                        if (doctp == "RE")
                        {
                            itmXml = itmXml.Replace("@prewhcode", "7200725");
                        }
                        else
                        {
                            itmXml = itmXml.Replace("@prewhcode", "<null/>");
                        }
                        itmXml = itmXml.Replace("@curamnt", "0");

                        retStr += "\n" + itmXml;
                        i++;
                    }
                }
                return retStr;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// replace header xml parameters
        /// </summary>
        /// <param name="_altDet">header details object</param>
        /// <returns>string</returns>
        public string ReplaceHeaderValue(ASY_ALT_HEADER_DTLS _altDet, string asyType, string documentNo)
        {
            try
            {
                var hedXml = CHNLSVC.Security.getDocumentXml("HED");
                string COST = "";
                string FRGT = "";
                string INSU = "";
                string OTH = "";


                if (asyType == "RE" || asyType == "EX")
                {
                    DataTable currDeduct = CHNLSVC.Security.getCurrencyBreakDown(documentNo);
                    if (currDeduct != null && currDeduct.Rows.Count > 0)
                    {
                        foreach (DataRow row in currDeduct.Rows)
                        {
                            if (row["CUS_ELE_CD"] != DBNull.Value && row["CUS_ELE_CD"].ToString() == "COST")
                            {
                                if (row["CUS_AMT"] != DBNull.Value)
                                {
                                    COST = row["CUS_AMT"].ToString();
                                }
                            }
                            if (row["CUS_ELE_CD"] != DBNull.Value && row["CUS_ELE_CD"].ToString() == "FRGT")
                            {
                                if (row["CUS_AMT"] != DBNull.Value)
                                {
                                    FRGT = row["CUS_AMT"].ToString();
                                }
                            }
                            if (row["CUS_ELE_CD"] != DBNull.Value && row["CUS_ELE_CD"].ToString() == "INSU")
                            {
                                if (row["CUS_AMT"] != DBNull.Value)
                                {
                                    INSU = row["CUS_AMT"].ToString();
                                }
                            }
                            if (row["CUS_ELE_CD"] != DBNull.Value && row["CUS_ELE_CD"].ToString() == "OTH")
                            {
                                if (row["CUS_AMT"] != DBNull.Value)
                                {
                                    OTH = row["CUS_AMT"].ToString();
                                }
                            }
                        }


                    }

                }
                hedXml = hedXml.Replace("</Header>", "");
                hedXml = hedXml.Replace("<Header>", "");
                hedXml = hedXml.Replace("@sadflow", _altDet.Ach_sad_flow);
                hedXml = hedXml.Replace("@numofform", _altDet.Ach_noof_forms.ToString());
                hedXml = hedXml.Replace("@totnumofforms", _altDet.Ach_tot_noof_forms.ToString());
                hedXml = hedXml.Replace("@numofitems", (_altDet.Ach_items_qty != 0) ?Math.Round(_altDet.Ach_items_qty,2).ToString() : "0");
                hedXml = hedXml.Replace("@numofpkgs", _altDet.Ach_tot_pkg);
                hedXml = hedXml.Replace("@selectedpge", _altDet.Ach_selected_page.ToString());
                hedXml = hedXml.Replace("@ofsentrycode", _altDet.Ach_off_entry_cd);
                hedXml = hedXml.Replace("@ofsentrydesc", _altDet.Ach_off_entry_desc);
                hedXml = hedXml.Replace("@declcd", _altDet.Ach_decl_1n2);
                hedXml = hedXml.Replace("@genproccd", _altDet.Ach_decl_3);
                hedXml = hedXml.Replace("@manirefnum", _altDet.Ach_manifest_ref_no.Replace("&", " &amp;"));
                hedXml = hedXml.Replace("@datenow", DateTime.Now.ToString("M/d/yy"));
                hedXml = hedXml.Replace("@expcd", _altDet.Ach_exp_cd);
                string nameaa = (_altDet.Ach_exp_add.Length > 173) ? _altDet.Ach_exp_add.Trim().Substring(0, 173) : _altDet.Ach_exp_add.Trim();
                hedXml = hedXml.Replace("@expnameaddress", nameaa.Replace("&", " &amp;"));
                hedXml = hedXml.Replace("@conscd", _altDet.Ach_cons_tin);
                hedXml = hedXml.Replace("@consnameaddress", _altDet.Ach_cons_add.Replace("&", " &amp;"));
                hedXml = hedXml.Replace("@fincd", _altDet.Ach_fin_code);
                hedXml = hedXml.Replace("@finnameaddress", _altDet.Ach_fin_name.Replace("&", " &amp;"));
                hedXml = hedXml.Replace("@deccd", _altDet.Ach_dec_tin);
                hedXml = hedXml.Replace("@decnameaddress", _altDet.Ach_dec_name.Replace("&", " &amp;"));
                hedXml = hedXml.Replace("@decrep", _altDet.Ach_cons_add.Replace("&", " &amp;"));
                hedXml = hedXml.Replace("@docnum", _altDet.Ach_doc_no);
                hedXml = hedXml.Replace("@cntyfstdest", _altDet.Ach_trading_cnty);
                hedXml = hedXml.Replace("@cap", "");
                hedXml = hedXml.Replace("@tradcnty", _altDet.Ach_trading_cnty);
                hedXml = hedXml.Replace("@expcntycd", _altDet.Ach_exp_cnty_cd);
                hedXml = hedXml.Replace("@expcntyname", FirstCharToUpper(_altDet.Ach_exp_cnty.ToLower().Replace("&", " &amp;")));
                hedXml = hedXml.Replace("@destcntycd", _altDet.Ach_dest_cnty_cd);
                hedXml = hedXml.Replace("@destcntyname", FirstCharToUpper(_altDet.Ach_dest_cnty.ToLower().Replace("&", " &amp;")));
                hedXml = hedXml.Replace("@cntyoforgnname", _altDet.Ach_orig_cnty_cd);
                hedXml = hedXml.Replace("@valdet", _altDet.Ach_val_det.ToString());
                hedXml = hedXml.Replace("@deparrvidentity", _altDet.Ach_vessel);
                hedXml = hedXml.Replace("@bodinfidentity", _altDet.Ach_voyage);
                hedXml = hedXml.Replace("@destcntyname", _altDet.Ach_voyage);
                hedXml = hedXml.Replace("@bodinfmode", _altDet.Ach_border_info_mode);
                hedXml = hedXml.Replace("@contflag", _altDet.Ach_fcl);
                hedXml = hedXml.Replace("@deltermcd", _altDet.Ach_delivery_terms.Replace("&", " &amp;") );
                hedXml = hedXml.Replace("@bodofscd", _altDet.Ach_off_entry_cd);
                hedXml = hedXml.Replace("@bodofname", _altDet.Ach_off_entry_desc);
                hedXml = hedXml.Replace("@locofgods", _altDet.Ach_loc_goods_cd);
                hedXml = hedXml.Replace("@bnkcd", (_altDet.Ach_bank_cd.Length > 4) ? _altDet.Ach_bank_cd.Substring(0, 4) : _altDet.Ach_bank_cd);
                string bank_desc = CHNLSVC.Security.GetBankName(_altDet.Ach_bank_cd);
                hedXml = hedXml.Replace("@bnkname", (bank_desc != null) ? bank_desc.Replace("&", " &amp;") : "");
                hedXml = hedXml.Replace("@bnkbrnch", _altDet.Ach_bank_branch);
                hedXml = hedXml.Replace("@bnkref", _altDet.Ach_oth_doc_no);
                hedXml = hedXml.Replace("@trmcd", _altDet.Ach_terms_of_payment);
                hedXml = hedXml.Replace("@trmdesc", _altDet.Ach_terms_of_payment_desc);
                if (string.IsNullOrEmpty(_altDet.Ach_acc_no.Trim()))
                {
                    hedXml = hedXml.Replace("@difpayref", "<null/>");
                }
                else
                {
                    hedXml = hedXml.Replace("@difpayref", _altDet.Ach_acc_no);
                }
               
                hedXml = hedXml.Replace("@guramnt", (_altDet.Ach_garentee_amt != 0) ? Math.Round(_altDet.Ach_garentee_amt,2).ToString() : "0");
                if (asyType == "EX" || asyType == "RE" || asyType == "TO")
                {
                    hedXml = hedXml.Replace("@wherhidentif", _altDet.Ach_wh_and_period);
                    hedXml = hedXml.Replace("@wherhdel", _altDet.Ach_wh_delay.ToString());
                }
                else
                {
                    hedXml = hedXml.Replace("@wherhidentif", "<null/>");
                    hedXml = hedXml.Replace("@wherhdel", "<null/>");
                }
                hedXml = hedXml.Replace("@calwrkmde", _altDet.Ach_cal_working_mode);
                hedXml = hedXml.Replace("@totcst", (_altDet.Ach_tot_cost != 0) ? Math.Round(_altDet.Ach_tot_cost,2).ToString() : "0");
                hedXml = hedXml.Replace("@invcurcd", _altDet.Ach_cur_cd);
                hedXml = hedXml.Replace("@invcurname", _altDet.Ach_cur_name);
                if (asyType == "EX" || asyType == "RE")
                {
                    hedXml = hedXml.Replace("@invfrgncur", COST);
                    hedXml = hedXml.Replace("@extfrefogncur", FRGT);
                    hedXml = hedXml.Replace("@insfrgncur", INSU);
                    hedXml = hedXml.Replace("@othfrgncur", OTH);
                }
                else
                {
                    hedXml = hedXml.Replace("@invfrgncur", (_altDet.Ach_tot_amt != 0) ? Math.Round(_altDet.Ach_tot_amt, 2).ToString() : "0");
                    hedXml = hedXml.Replace("@extfrefogncur", (_altDet.Ach_fre_amt != 0) ? Math.Round(_altDet.Ach_fre_amt, 2).ToString() : "0");
                    hedXml = hedXml.Replace("@insfrgncur", (_altDet.Ach_insu_amt != 0) ? Math.Round(_altDet.Ach_insu_amt, 2).ToString() : "0");
                    hedXml = hedXml.Replace("@othfrgncur", (_altDet.Ach_oth_amt != 0) ? Math.Round(_altDet.Ach_oth_amt, 2).ToString() : "0");
                }

                hedXml = hedXml.Replace("@extfrenatcur", "0");
                hedXml = hedXml.Replace("@extfrecurcd", _altDet.Ach_cur_cd);
                hedXml = hedXml.Replace("@extfrename", _altDet.Ach_cur_name);
                hedXml = hedXml.Replace("@intfrenatcur", "0");
                hedXml = hedXml.Replace("@intfrefogncur", "0");
                hedXml = hedXml.Replace("@intfrecurcd", _altDet.Ach_cur_cd);
                hedXml = hedXml.Replace("@intfrecurname", _altDet.Ach_cur_name);
                hedXml = hedXml.Replace("@insnatcur", "0");
                hedXml = hedXml.Replace("@inscurcd", _altDet.Ach_cur_cd);
                hedXml = hedXml.Replace("@inscurname", _altDet.Ach_cur_name);
                hedXml = hedXml.Replace("@othnatcur", "0");
                hedXml = hedXml.Replace("@othcurcd", _altDet.Ach_cur_cd);
                hedXml = hedXml.Replace("@othcurname", _altDet.Ach_cur_name);
                hedXml = hedXml.Replace("@gsdednatcur", "0");
                hedXml = hedXml.Replace("@gsdedfrgncur", "0");
                hedXml = hedXml.Replace("@gsdedcurcd", "<null/>");
                hedXml = hedXml.Replace("@gsdedcurname", _altDet.Ach_cur_name);
                hedXml = hedXml.Replace("@totinv", (_altDet.Ach_tot_amt != 0) ? Math.Round(_altDet.Ach_tot_amt,2).ToString() : "0");
                hedXml = hedXml.Replace("@totwgt", (_altDet.Ach_gross_mass != 0) ? Math.Round(_altDet.Ach_gross_mass,2).ToString() : "0");

                return hedXml;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// genarate xml file
        /// </summary>
        /// <param name="docEle">main xml element</param>
        /// <param name="subEle1">xml sub element</param>
        /// <param name="dicStr">dictionary string</param>
        /// <param name="type">document type</param>
        /// <returns></returns>
        public XElement xmlString(XElement docEle, List<ASY_XML_TAG> subEle1, Dictionary<string, string> dicStr, string type) 
        {
            try
            {
                foreach (ASY_XML_TAG xml_tags1 in subEle1)
                {
                    XElement sub1 = new XElement(xml_tags1.Axt_tag_name);
                    docEle.Add(sub1);
                    List<ASY_XML_TAG> subEle2 = null;
                    if (type == "ITEM")
                    {
                        subEle2 = getChildNodesForItm(xml_tags1.Axt_possision_id);
                    }
                    else
                    {
                        subEle2 = getChildNodes(xml_tags1.Axt_possision_id);
                    }

                    if (subEle2 != null)
                    {
                        xmlString(sub1, subEle2, dicStr, type);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(xml_tags1.Axt_column_name))
                            sub1.Add(getValue(dicStr, xml_tags1.Axt_column_name));
                    }
                }

                return docEle;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// get xml chiled nodes
        /// </summary>
        /// <param name="paretntid">parent id</param>
        /// <returns>List<ASY_XML_TAG></returns>
        public List<ASY_XML_TAG> getChildNodes(decimal paretntid)
        {
            List<ASY_XML_TAG> _xmlTagList = new List<ASY_XML_TAG>();
            _xmlTagList = CHNLSVC.Security.getXmlTagList(paretntid);
            return _xmlTagList;
        }
        /// <summary>
        /// get child node for item
        /// </summary>
        /// <param name="paretntid">parent id</param>
        /// <returns>List<ASY_XML_TAG></returns>
        public List<ASY_XML_TAG> getChildNodesForItm(decimal paretntid)
        {
            try
            {
                List<ASY_XML_TAG> _xmlTagList = new List<ASY_XML_TAG>();
                _xmlTagList = CHNLSVC.Security.getXmlTagListForItems(paretntid);
                return _xmlTagList;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        ///// <summary>
        ///// create dictionary list for header values
        ///// </summary>
        ///// <param name="_altHedDet">header details object</param>
        ///// <returns>Dictionary<string, string></returns>
        //public Dictionary<string, string> AltDetArray(ASY_ALT_HEADER_DTLS _altHedDet)
        //{
        //    try
        //    {
        //        Dictionary<string, string> HetDetail = new Dictionary<string, string>();


        //        HetDetail["ACH_DOC_NO"] = _altHedDet.Ach_doc_no;
        //        HetDetail["ACH_OTH_DOC_NO"] = _altHedDet.Ach_oth_doc_no;
        //        HetDetail["ACH_DOC_DT"] = _altHedDet.Ach_doc_dt.ToString();
        //        HetDetail["ACH_COM"] = _altHedDet.Ach_com;
        //        HetDetail["ACH_DB"] = _altHedDet.Ach_db;
        //        HetDetail["ACH_PROC_CD"] = _altHedDet.Ach_proc_cd;
        //        HetDetail["ACH_DECL_1"] = _altHedDet.Ach_decl_1;
        //        HetDetail["ACH_DECL_2"] = _altHedDet.Ach_decl_2;
        //        HetDetail["ACH_DECL_3"] = _altHedDet.Ach_decl_3;
        //        HetDetail["ACH_DECL_1N2"] = _altHedDet.Ach_decl_1n2;
        //        HetDetail["ACH_EXP_CD"] = _altHedDet.Ach_exp_cd;
        //        HetDetail["ACH_EXP_TIN"] = _altHedDet.Ach_exp_tin;
        //        HetDetail["ACH_EXP_NAME"] = _altHedDet.Ach_exp_name;
        //        HetDetail["ACH_EXP_ADD"] = _altHedDet.Ach_exp_add;
        //        HetDetail["ACH_CONS_CD"] = _altHedDet.Ach_cons_cd;
        //        HetDetail["ACH_CONS_TIN"] = _altHedDet.Ach_cons_tin;
        //        HetDetail["ACH_CONS_NAME"] = _altHedDet.Ach_cons_name;
        //        HetDetail["ACH_CONS_ADD"] = _altHedDet.Ach_cons_add;
        //        HetDetail["ACH_DEC_CD"] = _altHedDet.Ach_dec_cd;
        //        HetDetail["ACH_DEC_TIN"] = _altHedDet.Ach_dec_tin;
        //        HetDetail["ACH_DEC_NAME"] = _altHedDet.Ach_dec_name;
        //        HetDetail["ACH_DEC_ADD"] = _altHedDet.Ach_dec_add;
        //        HetDetail["ACH_ITEMS_QTY"] = _altHedDet.Ach_items_qty.ToString();
        //        HetDetail["ACH_TOT_PKG"] = _altHedDet.Ach_tot_pkg;
        //        HetDetail["ACH_EXP_CNTY_CD"] = _altHedDet.Ach_exp_cnty_cd;
        //        HetDetail["ACH_EXP_CNTY"] = _altHedDet.Ach_exp_cnty;
        //        HetDetail["ACH_DEST_CNTY_CD"] = _altHedDet.Ach_dest_cnty_cd;
        //        HetDetail["ACH_DEST_CNTY"] = _altHedDet.Ach_dest_cnty;
        //        HetDetail["ACH_ORIG_CNTY_CD"] = _altHedDet.Ach_orig_cnty_cd;
        //        HetDetail["ACH_ORIG_CNTY"] = _altHedDet.Ach_orig_cnty;
        //        HetDetail["ACH_LOAD_CNTY_CD"] = _altHedDet.Ach_load_cnty_cd;
        //        HetDetail["ACH_LOAD_CNTY"] = _altHedDet.Ach_load_cnty;
        //        HetDetail["ACH_VESSEL"] = _altHedDet.Ach_vessel;
        //        HetDetail["ACH_VOYAGE"] = _altHedDet.Ach_voyage;
        //        HetDetail["ACH_VOYAGE_DT"] = _altHedDet.Ach_voyage_dt.ToString();
        //        HetDetail["ACH_FCL"] = _altHedDet.Ach_fcl;
        //        HetDetail["ACH_MARKS_AND_NO"] = _altHedDet.Ach_marks_and_no;
        //        HetDetail["ACH_DELIVERY_TERMS"] = _altHedDet.Ach_delivery_terms;
        //        HetDetail["ACH_CUR_CD"] = _altHedDet.Ach_cur_cd;
        //        HetDetail["ACH_TOT_AMT"] = _altHedDet.Ach_tot_amt.ToString();
        //        HetDetail["ACH_EX_RT"] = _altHedDet.Ach_ex_rt.ToString();
        //        HetDetail["ACH_BANK_CD"] = _altHedDet.Ach_bank_cd;
        //        HetDetail["ACH_BANK_NAME"] = _altHedDet.Ach_bank_name;
        //        HetDetail["ACH_BANK_BRANCH"] = _altHedDet.Ach_bank_branch;
        //        HetDetail["ACH_TERMS_OF_PAYMENT"] = _altHedDet.Ach_terms_of_payment;
        //        HetDetail["ACH_ACC_NO"] = _altHedDet.Ach_acc_no;
        //        HetDetail["ACH_WH_AND_PERIOD"] = _altHedDet.Ach_wh_and_period;
        //        HetDetail["ACH_OFF_ENTRY_CD"] = _altHedDet.Ach_off_entry_cd;
        //        HetDetail["ACH_OFF_ENTRY_DESC"] = _altHedDet.Ach_off_entry_desc;
        //        HetDetail["ACH_LOC_GOODS_CD"] = _altHedDet.Ach_loc_goods_cd;
        //        HetDetail["ACH_LOC_GOODS_DESC"] = _altHedDet.Ach_loc_goods_desc;
        //        HetDetail["ACH_TOT_PKG_UNIT"] = _altHedDet.Ach_tot_pkg_unit;
        //        HetDetail["ACH_PROC_ID_1"] = _altHedDet.Ach_proc_id_1;
        //        HetDetail["ACH_PROC_ID_2"] = _altHedDet.Ach_proc_id_2;
        //        HetDetail["ACH_LISION_NO"] = _altHedDet.Ach_lision_no;
        //        HetDetail["ACH_TRADING_CNTY"] = _altHedDet.Ach_trading_cnty;
        //        HetDetail["ACH_MAIN_HS"] = _altHedDet.Ach_main_hs;
        //        HetDetail["ACH_NOOF_FORMS"] = _altHedDet.Ach_noof_forms.ToString();
        //        HetDetail["ACH_TOT_NOOF_FORMS"] = _altHedDet.Ach_tot_noof_forms.ToString();
        //        HetDetail["ACH_REG_DT"] = _altHedDet.Ach_reg_dt.ToString();
        //        HetDetail["ACH_WH_DELAY"] = _altHedDet.Ach_wh_delay.ToString();
        //        HetDetail["ACH_COST_AMT"] = _altHedDet.Ach_cost_amt.ToString();
        //        HetDetail["ACH_FRE_AMT"] = _altHedDet.Ach_fre_amt.ToString();
        //        HetDetail["ACH_INSU_AMT"] = _altHedDet.Ach_insu_amt.ToString();
        //        HetDetail["ACH_OTH_AMT"] = _altHedDet.Ach_oth_amt.ToString();
        //        HetDetail["ACH_GROSS_MASS"] = _altHedDet.Ach_gross_mass.ToString();
        //        HetDetail["ACH_NET_MASS"] = _altHedDet.Ach_net_mass.ToString();
        //        HetDetail["ACH_SAD_FLOW"] = _altHedDet.Ach_sad_flow;
        //        HetDetail["ACH_SELECTED_PAGE"] = _altHedDet.Ach_selected_page.ToString();
        //        HetDetail["ACH_VAL_DET"] = _altHedDet.Ach_val_det.ToString();
        //        HetDetail["ACH_FIN_CODE"] = _altHedDet.Ach_fin_code;
        //        HetDetail["ACH_FIN_NAME"] = _altHedDet.Ach_fin_name;
        //        HetDetail["ACH_BORDER_INFO_MODE"] = _altHedDet.Ach_border_info_mode;
        //        HetDetail["ACH_CAL_WORKING_MODE"] = _altHedDet.Ach_cal_working_mode;
        //        HetDetail["ACH_MANIFEST_REF_NO"] = _altHedDet.Ach_manifest_ref_no;
        //        HetDetail["ACH_TOT_COST"] = _altHedDet.Ach_tot_cost.ToString();
        //        HetDetail["ACH_TERMS_OF_PAYMENT_DESC"] = _altHedDet.Ach_terms_of_payment_desc.ToString();
        //        HetDetail["ACH_GARENTEE_AMT"] = _altHedDet.Ach_garentee_amt.ToString();
        //        HetDetail["ACH_CUR_NAME"] = _altHedDet.Ach_cur_name.ToString();
        //        return HetDetail;
        //    }
        //    catch (Exception ex) {
        //        throw ex;
        //    }
        //}
        ///// <summary>
        ///// create dictionary list for items
        ///// </summary>
        ///// <param name="_altItem">items details object</param>
        ///// <returns>Dictionary<string, string></returns>
        //public Dictionary<string, string> AltItmArray(ASY_CUSDEC_ITM _altItem)
        //{
        //    try
        //    {
        //        Dictionary<string, string> _altItemDet = new Dictionary<string, string>();
        //        _altItemDet["ACH_DOC_NO"] = _altItem.Ach_doc_no;
        //        _altItemDet["ACI_LINE"] = _altItem.Aci_line.ToString();
        //        _altItemDet["ACI_ITM_CD"] = _altItem.Aci_itm_cd;
        //        _altItemDet["ACI_HS_CD"] = _altItem.Aci_hs_cd;
        //        _altItemDet["ACI_MODEL"] = _altItem.Aci_model;
        //        _altItemDet["ACI_ITM_DESC"] = _altItem.Aci_itm_desc;
        //        _altItemDet["ACI_QTY"] = _altItem.Aci_qty.ToString();
        //        _altItemDet["ACI_UOM"] = _altItem.Aci_uom;
        //        _altItemDet["ACI_UNIT_COST"] = _altItem.Aci_unit_cost.ToString();
        //        _altItemDet["ACI_ITEM_PRICE"] = _altItem.Aci_item_price.ToString();
        //        _altItemDet["ACI_GROSS_MASS "] = _altItem.Aci_gross_mass.ToString();
        //        _altItemDet["ACI_NET_MASS"] = _altItem.Aci_net_mass.ToString();
        //        _altItemDet["ACI_PREFERANCE "] = _altItem.Aci_preferance;
        //        _altItemDet["ACI_QUOTA"] = _altItem.Aci_quota;
        //        _altItemDet["ACI_BL_NO"] = _altItem.Aci_bl_no;
        //        _altItemDet["ACH_OTHDOC1_NO"] = _altItem.Ach_othdoc1_no;
        //        _altItemDet["ACH_OTHDOC1_LINE"] = _altItem.Ach_othdoc1_line.ToString();
        //        _altItemDet["ACH_OTHDOC2_NO"] = _altItem.Ach_othdoc2_no;
        //        _altItemDet["ACH_OTHDOC2_LINE"] = _altItem.Ach_othdoc2_line.ToString();
        //        _altItemDet["ACH_NUM_OF_PKG"] = _altItem.Ach_num_of_pkg.ToString();
        //        _altItemDet["ACH_MRK1_PKG"] = _altItem.Ach_mrk1_pkg;
        //        _altItemDet["ACH_KND_OF_PKG"] = _altItem.Ach_knd_of_pkg;
        //        _altItemDet["ACH_KND_OF_PKG_NAME"] = _altItem.Ach_knd_of_pkg_name;
        //        _altItemDet["ACI_COMD_CD"] = _altItem.Aci_comd_cd;
        //        _altItemDet["ACI_PREC_1"] = _altItem.Aci_prec_1;
        //        _altItemDet["ACI_PREC_4"] = _altItem.Aci_prec_4;
        //        _altItemDet["ACH_EXT_CUST_PROC"] = _altItem.Ach_ext_cust_proc;
        //        _altItemDet["ACH_NAT_CUST_PROC"] = _altItem.Ach_nat_cust_proc;
        //        _altItemDet["ACH_VAL_ITM"] = _altItem.Ach_val_itm;
        //        _altItemDet["ACH_CNTY_OF_OREGN"] = _altItem.Ach_cnty_of_oregn;
        //        _altItemDet["ACI_DESC_OF_GOODS"] = _altItem.Aci_desc_of_goods;
        //        _altItemDet["ACI_ITM_STAT_VAL"] = _altItem.Aci_itm_stat_val.ToString();
        //        _altItemDet["ACI_TOT_COST_ITM"] = _altItem.Aci_tot_cost_itm.ToString();
        //        _altItemDet["ACI_RTE_OF_ADJ"] = _altItem.Aci_rte_of_adj;
        //        _altItemDet["ACH_CUR_CD"] = _altItem.Ach_cur_cd;
        //        _altItemDet["ACI_INV_NAT_CURR"] = _altItem.Aci_inv_nat_curr.ToString();
        //        _altItemDet["ACI_INV_FORGN_CURR"] = _altItem.Aci_inv_forgn_curr.ToString();
        //        _altItemDet["ACI_INT_FRE_NAT_CURR"] = _altItem.Aci_int_fre_nat_curr.ToString();
        //        _altItemDet["ACI_INT_FRE_FORGN_CURR"] = _altItem.Aci_int_fre_forgn_curr.ToString();
        //        _altItemDet["ACI_INS_NAT_CURR"] = _altItem.Aci_ins_nat_curr.ToString();
        //        _altItemDet["ACI_INS_FORGN_CURR"] = _altItem.Aci_ins_forgn_curr.ToString();
        //        _altItemDet["ACI_OTH_CST_NAT_CURR"] = _altItem.Aci_oth_cst_nat_curr.ToString();
        //        _altItemDet["ACI_OTH_CST_FORGN_CURR"] = _altItem.Aci_oth_cst_forgn_curr.ToString();
        //        _altItemDet["ACI_DEDU_NAT_CURR"] = _altItem.Aci_dedu_nat_curr.ToString();
        //        _altItemDet["ACI_DEDU_FORGN_CURR"] = _altItem.Aci_dedu_forgn_curr.ToString();
        //        _altItemDet["ACI_CURR_AMNT"] = _altItem.Aci_curr_amnt.ToString();

        //        return _altItemDet;
        //    }
        //    catch (Exception ex) {
        //        throw ex;
        //    }
        //}
        /// <summary>
        /// get value for xml tag
        /// </summary>
        /// <param name="dic">dictionary string</param>
        /// <param name="value">tag value</param>
        /// <returns>string</returns>
        public string getValue(Dictionary<string, string> dic,string value)
        {
            try
            {
                string val = null;
                if (dic.ContainsKey(value))
                    val = dic[value];
                return val;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public bool genaratAlterDownXml(string docnum, ASY_IMP_CUSDEC_HDR _altDet, List<ASY_CUSDEC_ITEM_DTLS_MODEL> itemList)
        {
            try
            {
                //XElement docEle = new XElement("Asycuda");
                string version = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
                string xmlHed = ReplaceAlterHeaderValue(_altDet,itemList);
                string xmlItems = ReplaceAlterItemValue(itemList,_altDet);
                //List<ASY_XML_TAG> subEle = getChildNodes(1);
                //List<ASY_XML_TAG> subEleItm = getChildNodesForItm(1);
                //Dictionary<string, string> DataDi = AltDetArray(_altDet);

                //XElement xmlHed = xmlString(docEle, subEle, DataDi, "HED");
                //int i = 0;
                //foreach (ASY_CUSDEC_ITM item in itemList)
                //{
                //    i = i + 1;
                //    Dictionary<string, string> itmData = AltItmArray(item);
                //    xmlHed = xmlString(xmlHed, subEleItm, itmData, "ITEM");

                //    string path = @"C:\Users\nuwanc.ABANS\Desktop\WriteText.txt";
                //    using (StreamWriter file = new StreamWriter(path, true))
                //    {
                //        file.WriteLine(DateTime.Now.ToString());
                //    }


                //}

                Response.Clear();
                Response.ContentType = "text/xml";
                Response.AddHeader("content-disposition", "attachment;filename=" + docnum + ".xml");
                Response.Write(version + "\n<ASYCUDA>" + "\n" + xmlHed.ToString() + "\n" + xmlItems.ToString() + "\n</ASYCUDA>");
                //Response.Write(version + "\n" + xmlHed.ToString()  );
                Response.End();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ReplaceAlterItemValue(List<ASY_CUSDEC_ITEM_DTLS_MODEL> itemList,ASY_IMP_CUSDEC_HDR _altDet )
        {
            try
            {
                string retStr = "";
                if (itemList != null && itemList.Count>0 )
                {
                    
                    itemList = itemList.OrderBy(x => x.TO_BOND_ITEM_LINE_NO).ToList();
                    if (_altDet.CUH_TP == "BOI" || _altDet.CUH_TP == "EXP")
                    {
                        itemList = itemList.OrderBy(x => x.BOI_LINE_NO).ToList();
                    }
                    int i=1;
                    string itemXml = CHNLSVC.Security.getDocumentXml("ITM");
                    foreach (ASY_CUSDEC_ITEM_DTLS_MODEL item in itemList)
                    {
                        string itmXml = itemXml;
                        //itmXml = itmXml.Replace("@numofpkg", (item.TOT_NUMOF_PKG != 0) ? Math.Round(item.TOT_NUMOF_PKG, 2).ToString() : "0");
                        itmXml = itmXml.Replace("@numofpkg", (item.BOND_QTY != 0) ? Math.Round(item.BOND_QTY, 2).ToString() : "0");
                        itmXml = itmXml.Replace("@mk1pkg", item.S_NUMBER);
                        itmXml = itmXml.Replace("@mk2pkg", (item.OTHER_BOND_NO + " LINE NO: " + item.OTHER_BOND_NO_LINE_NO.ToString()).Replace("&", " &amp;"));

                        MST_UOM_CATE pkg = CHNLSVC.Security.getPackageDetailsforcode(_altDet.CUH_TOT_PKG_UNIT);
                        itmXml = itmXml.Replace("@kndofpkgcd", pkg.MSUC_CD);
                        itmXml = itmXml.Replace("@pkgname", pkg.MSUC_ASY_DESC.Replace("&", " &amp;"));
                        itmXml = itmXml.Replace("@commcd", item.HS_CODE.Replace(".", "").Replace(" ", "").PadRight(8, '0'));
                        itmXml = itmXml.Replace("@prec1", "00");
                        itmXml = itmXml.Replace("@prec4","<null/>"/* item.TO_BOND_ITEM_LINE_NO.ToString()*/);
                        //itmXml = itmXml.Replace("@prec4", item.BOI_LINE_NO.ToString());
                        itmXml = itmXml.Replace("@extcustproc", _altDet.CUH_PROCE_CD_1);
                        itmXml = itmXml.Replace("@natcusproc", _altDet.CUH_PROCE_CD_2);
                        itmXml = itmXml.Replace("@supunitcd", (item.BOND_QTY != 0) ? Math.Round(item.BOND_QTY, 2).ToString() : "0");
                        itmXml = itmXml.Replace("@itmprce", (item.TOTAL_UNIT_COST != 0) ? Math.Round(item.TOTAL_UNIT_COST, 2).ToString() : "0");
                        itmXml = itmXml.Replace("@valitm", (i==1)?"0+0+0+0-0":"");
                        itmXml = itmXml.Replace("@cntyoforgn", item.COUNTRY_OF_ORGIN);
                        itmXml = itmXml.Replace("@desofgods", item.DESCRIPTION.Replace("&", " &amp;"));
                        itmXml = itmXml.Replace("@comdesc", "MODEL: " + item.MODEL + " (" + item.BOND_QTY+ " U)");
                        if (_altDet.CUH_TP == "BOI")
                        {
                            itmXml = itmXml.Replace("@sumdecl", _altDet.CUH_VOYAGE + " OF " + _altDet.CUH_VOYAGE_DT.ToString("dd/MM/yyy"));

                        }else if (_altDet.CUH_TP == "RE")
                        {
                            itmXml = itmXml.Replace("@sumdecl", _altDet.CUH_DOC_NO.Replace("&", " &amp;"));

                        }
                        else
                        {
                            itmXml = itmXml.Replace("@sumdecl", _altDet.CUH_BL_NO.Replace("&", " &amp;"));

                        }
                        itmXml = itmXml.Replace("@groswght", (item.GROSS_MASS != 0) ? Math.Round(item.GROSS_MASS, 2).ToString() : "0");
                        itmXml = itmXml.Replace("@netwght", (item.NET_MASS != 0) ? Math.Round(item.NET_MASS, 2).ToString() : "0");
                        itmXml = itmXml.Replace("@totcst", "0");
                        itmXml = itmXml.Replace("@cifitm", "");
                        itmXml = itmXml.Replace("@rteofadj", "1");
                        itmXml = itmXml.Replace("@ststval",Math.Round((item.ITEM_PRICE * _altDet.CUH_EX_RT),2).ToString());
                        itmXml = itmXml.Replace("@invamntnatcur", Math.Round((item.ITEM_PRICE * _altDet.CUH_EX_RT),2).ToString());
                        itmXml = itmXml.Replace("@invamntfogncur", (item.TOTAL_UNIT_COST != 0) ? Math.Round(item.TOTAL_UNIT_COST, 2).ToString() : "0");
                        itmXml = itmXml.Replace("@invcurcd", _altDet.CUH_CUR_CD);
                        itmXml = itmXml.Replace("@invcurname", "No foreign currency");
                        itmXml = itmXml.Replace("@itmextnatcur", "0");
                        itmXml = itmXml.Replace("@itmextfogncur","0");
                        itmXml = itmXml.Replace("@itmextcurcd", _altDet.CUH_CUR_CD);
                        itmXml = itmXml.Replace("@itmextcurname", "No foreign currency");
                        itmXml = itmXml.Replace("@itmintnatcur", "0");
                        itmXml = itmXml.Replace("@itmintfogncur",  "0");
                        itmXml = itmXml.Replace("@itmintcurcd", _altDet.CUH_CUR_CD);
                        itmXml = itmXml.Replace("@itmintcurname", "No foreign currency");
                        itmXml = itmXml.Replace("@itmothnatcur", "0");
                        itmXml = itmXml.Replace("@itmothfogncur","0");
                        itmXml = itmXml.Replace("@itmothcurcd",  _altDet.CUH_CUR_CD);
                        itmXml = itmXml.Replace("@itmothcurname", "No foreign currency");
                        itmXml = itmXml.Replace("@itmdednatcur", "0");
                        itmXml = itmXml.Replace("@itmdedfogncur", "0");
                        itmXml = itmXml.Replace("@itmdedcurcd",  _altDet.CUH_CUR_CD);
                        itmXml = itmXml.Replace("@itmdedcurname", "No foreign currency");
                        itmXml = itmXml.Replace("@prefcode", "BOICM");
                        if (_altDet.CUH_TP == "RE")
                        {
                            itmXml = itmXml.Replace("@prewhcode", "7200725");
                        }
                        else
                        {
                            itmXml = itmXml.Replace("@prewhcode", "<null/>");
                        }
                        itmXml = itmXml.Replace("@curamnt", "0");

                        retStr += "\n" + itmXml;
                        i++;
                    }
                }
                return retStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ReplaceAlterHeaderValue(ASY_IMP_CUSDEC_HDR _altDet, List<ASY_CUSDEC_ITEM_DTLS_MODEL>  itemList)
        {
            try
            {
                var hedXml = CHNLSVC.Security.getDocumentXml("HED");
                string COST = "";
                string FRGT = "";
                string INSU = "";
                string OTH = "";

                
                if (_altDet.CUH_TP == "RE" || _altDet.CUH_TP == "EX")
                {
                    DataTable currDeduct = CHNLSVC.Security.getCurrencyBreakDown(_altDet.CUH_DOC_NO);
                    if (currDeduct != null && currDeduct.Rows.Count > 0)
                    {
                        foreach (DataRow row in currDeduct.Rows)
                        {
                            if (row["CUS_ELE_CD"] != DBNull.Value && row["CUS_ELE_CD"].ToString() == "COST")
                            {
                                if (row["CUS_AMT"] != DBNull.Value)
                                {
                                    COST = row["CUS_AMT"].ToString();
                                }
                            }
                            if (row["CUS_ELE_CD"] != DBNull.Value && row["CUS_ELE_CD"].ToString() == "FRGT")
                            {
                                if (row["CUS_AMT"] != DBNull.Value)
                                {
                                    FRGT = row["CUS_AMT"].ToString();
                                }
                            }
                            if (row["CUS_ELE_CD"] != DBNull.Value && row["CUS_ELE_CD"].ToString() == "INSU")
                            {
                                if (row["CUS_AMT"] != DBNull.Value)
                                {
                                    INSU = row["CUS_AMT"].ToString();
                                }
                            }
                            if (row["CUS_ELE_CD"] != DBNull.Value && row["CUS_ELE_CD"].ToString() == "OTH")
                            {
                                if (row["CUS_AMT"] != DBNull.Value)
                                {
                                    OTH = row["CUS_AMT"].ToString();
                                }
                            }
                        }
                       

                    }
                    
                }
                hedXml = hedXml.Replace("</Header>", "");
                hedXml = hedXml.Replace("<Header>", "");
                hedXml = hedXml.Replace("@sadflow", "I");
                hedXml = hedXml.Replace("@numofform", "1");
                hedXml = hedXml.Replace("@totnumofforms", CHNLSVC.Security.getTotalForms(_altDet.CUH_ITEMS_QTY).ToString());
                hedXml = hedXml.Replace("@numofitems", (_altDet.CUH_ITEMS_QTY != 0) ? Math.Round(_altDet.CUH_ITEMS_QTY, 2).ToString() : "0");
                //hedXml = hedXml.Replace("@numofpkgs", _altDet.CUH_TOT_PKG.ToString());
                if (_altDet.CUH_TP == "BOI")
                {
                    hedXml = hedXml.Replace("@numofpkgs", itemList.Sum(x=>x.BOND_QTY).ToString());

                }
                else
                {
                    hedXml = hedXml.Replace("@numofpkgs", (Convert.ToDecimal(_altDet.CUH_TOT_PKG) != 0) ? Math.Round(Convert.ToDecimal(_altDet.CUH_TOT_PKG), 2).ToString().ToString() : "0");

                }
                hedXml = hedXml.Replace("@selectedpge", "1");
                hedXml = hedXml.Replace("@ofsentrycode",_altDet.CUH_INSU_TEXT);
                hedXml = hedXml.Replace("@ofsentrydesc",CHNLSVC.Security.GetOfficeOfEntryDescription(_altDet.CUH_INSU_TEXT));
                hedXml = hedXml.Replace("@declcd", _altDet.CUH_DECL_1 + _altDet.CUH_DECL_2);
                hedXml = hedXml.Replace("@genproccd", _altDet.CUH_DECL_3);
                hedXml = hedXml.Replace("@manirefnum", _altDet.CUH_DOC_NO.Replace("&", " &amp;"));
                hedXml = hedXml.Replace("@datenow", DateTime.Now.ToString("M/d/yy"));
                if (_altDet.CUH_COM == "SGD" || _altDet.CUH_COM == "SGL")
                {
                    hedXml = hedXml.Replace("@expcd", "SINGHAGIRI PVT LTD 515 &#13;DARLEY RD,COLOMBO 10");
                    hedXml = hedXml.Replace("@expnameaddress", "SINGHAGIRI PVT LTD 515 &#13;DARLEY RD,COLOMBO 10");
                }
                else
                {
                    hedXml = hedXml.Replace("@expcd", "1040800657000");
                    hedXml = hedXml.Replace("@expnameaddress", "ABANS PLC &#13;498,GALLE ROAD,COLOMBO 03");
                }
               
                
                hedXml = hedXml.Replace("@conscd", _altDet.CUH_CONSI_TIN.Replace("-",""));
                hedXml = hedXml.Replace("@consnameaddress", _altDet.CUH_CONSI_NAME.Replace("&", " &amp;") + "&#13;" + _altDet.CUH_CONSI_ADDR);
                hedXml = hedXml.Replace("@fincd", _altDet.CUH_SUPP_TIN);
                hedXml = hedXml.Replace("@finnameaddress", _altDet.CUH_CONSI_NAME.Replace("&", " &amp;") + "&#13;" + _altDet.CUH_CONSI_ADDR);
                hedXml = hedXml.Replace("@deccd", _altDet.CUH_DECL_TIN);
                hedXml = hedXml.Replace("@decnameaddress", _altDet.CUH_DECL_NAME.Replace("&", " &amp;"));
                hedXml = hedXml.Replace("@decrep", _altDet.CUH_CONSI_NAME.Replace("&", " &amp;") + "&#13;" + _altDet.CUH_CONSI_ADDR);
                hedXml = hedXml.Replace("@docnum", _altDet.CUH_DOC_NO);
                hedXml = hedXml.Replace("@cntyfstdest", _altDet.CUH_TRADING_COUNTRY);
                hedXml = hedXml.Replace("@cap", "");
                hedXml = hedXml.Replace("@tradcnty", _altDet.CUH_TRADING_COUNTRY);
                hedXml = hedXml.Replace("@expcntycd", _altDet.CUH_CNTY_OF_EXPORT);
                hedXml = hedXml.Replace("@expcntyname", (_altDet.CUH_EXP_CNTY_NAME == "Local sales") ? _altDet.CUH_EXP_CNTY_NAME.Replace("&", " &amp;") : FirstCharToUpper(_altDet.CUH_EXP_CNTY_NAME.ToLower().Replace("&", " &amp;")));
                hedXml = hedXml.Replace("@destcntycd", _altDet.CUH_CNTY_OF_DESTINATION);
                hedXml = hedXml.Replace("@destcntyname", FirstCharToUpper(_altDet.CUH_DESTI_CNTY_NAME.ToLower().Replace("&", " &amp;")));
                hedXml = hedXml.Replace("@cntyoforgnname", _altDet.CUH_CNTY_OF_ORIGIN);
                hedXml = hedXml.Replace("@valdet", Math.Round(_altDet.CUH_TOT_AMT,2).ToString());
                hedXml = hedXml.Replace("@deparrvidentity", _altDet.CUH_VESSEL);
                hedXml = hedXml.Replace("@bodinfidentity", _altDet.CUH_VOYAGE);
                hedXml = hedXml.Replace("@destcntyname", _altDet.CUH_CNTY_OF_DESTINATION);
                hedXml = hedXml.Replace("@bodinfmode", "3");
                hedXml = hedXml.Replace("@contflag", _altDet.CUH_FCL);
                hedXml = hedXml.Replace("@deltermcd", _altDet.CUH_DELIVERY_TERMS.Replace("&", " &amp;"));
                hedXml = hedXml.Replace("@bodofscd", _altDet.CUH_OFFICE_OF_ENTRY);
                hedXml = hedXml.Replace("@bodofname", CHNLSVC.Security.GetOfficeOfEntryDescription(_altDet.CUH_OFFICE_OF_ENTRY));
                hedXml = hedXml.Replace("@locofgods", _altDet.CUH_LOCATION_OF_GOODS);
                hedXml = hedXml.Replace("@bnkcd", (_altDet.CUH_BANK_CD.Length > 4) ? _altDet.CUH_BANK_CD.Substring(0, 4) : _altDet.CUH_BANK_CD);
                hedXml = hedXml.Replace("@bnkname", CHNLSVC.Security.GetBankName(_altDet.CUH_BANK_CD).Replace("&", " &amp;"));
                hedXml = hedXml.Replace("@bnkbrnch", _altDet.CUH_BANK_BRANCH);
                hedXml = hedXml.Replace("@bnkref", _altDet.CUH_DOC_NO);
                hedXml = hedXml.Replace("@trmcd", _altDet.CUH_CUSTOM_LC_TP);
                hedXml = hedXml.Replace("@trmdesc", CHNLSVC.Security.getTermsOfPaymentDescription(_altDet.CUH_CUSTOM_LC_TP));
                hedXml = hedXml.Replace("@difpayref","<null/>");
                hedXml = hedXml.Replace("@guramnt", "0");
                if (_altDet.CUH_TP == "EX" || _altDet.CUH_TP == "RE" || _altDet.CUH_TP == "TO" || _altDet.CUH_TP == "BOI")
                {
                    hedXml = hedXml.Replace("@wherhidentif", _altDet.CUH_WH_AND_PERIOD);
                    hedXml = hedXml.Replace("@wherhdel", "180");
                }
                else
                {
                    hedXml = hedXml.Replace("@wherhidentif", "<null/>");
                    hedXml = hedXml.Replace("@wherhdel", "<null/>");
                }
                hedXml = hedXml.Replace("@calwrkmde", "0");
                hedXml = hedXml.Replace("@totcst", (_altDet.CUH_TOT_AMT != 0) ? Math.Round(_altDet.CUH_TOT_AMT, 2).ToString() : "0");
                if (_altDet.CUH_TP == "EX" || _altDet.CUH_TP == "RE")
                {
                    hedXml = hedXml.Replace("@invfrgncur", COST);
                    hedXml = hedXml.Replace("@extfrefogncur", FRGT);
                    hedXml = hedXml.Replace("@insfrgncur", INSU);
                    hedXml = hedXml.Replace("@othfrgncur", OTH);
                }
                else
                {
                    hedXml = hedXml.Replace("@invfrgncur", (_altDet.CUH_TOT_AMT != 0) ? Math.Round(_altDet.CUH_TOT_AMT, 2).ToString() : "0");
                    hedXml = hedXml.Replace("@extfrefogncur", "0");
                    hedXml = hedXml.Replace("@insfrgncur", "0");
                    hedXml = hedXml.Replace("@othfrgncur", "0");
                }
                hedXml = hedXml.Replace("@invcurcd", _altDet.CUH_CUR_CD);
                hedXml = hedXml.Replace("@invcurname", "No foreign currency");
                hedXml = hedXml.Replace("@extfrenatcur", "0");
                
                hedXml = hedXml.Replace("@extfrecurcd", _altDet.CUH_CUR_CD);
                hedXml = hedXml.Replace("@extfrename", "No foreign currency");
                hedXml = hedXml.Replace("@intfrenatcur", "0");
                hedXml = hedXml.Replace("@intfrefogncur", "0");
                hedXml = hedXml.Replace("@intfrecurcd", _altDet.CUH_CUR_CD);
                hedXml = hedXml.Replace("@intfrecurname", "No foreign currency");
                hedXml = hedXml.Replace("@insnatcur", "0");
                
                hedXml = hedXml.Replace("@inscurcd", _altDet.CUH_CUR_CD);
                hedXml = hedXml.Replace("@inscurname", "No foreign currency");
                hedXml = hedXml.Replace("@othnatcur", "0");
                
                hedXml = hedXml.Replace("@othcurcd", _altDet.CUH_CUR_CD);
                hedXml = hedXml.Replace("@othcurname", "No foreign currency");
                hedXml = hedXml.Replace("@gsdednatcur", "0");
                hedXml = hedXml.Replace("@gsdedfrgncur", "0");
                hedXml = hedXml.Replace("@gsdedcurcd", "<null/>");
                hedXml = hedXml.Replace("@gsdedcurname", "No foreign currency");
                hedXml = hedXml.Replace("@totinv", (_altDet.CUH_TOT_AMT != 0) ? Math.Round(_altDet.CUH_TOT_AMT, 2).ToString() : "0");
                hedXml = hedXml.Replace("@totwgt", (_altDet.CUH_TOT_GROSS_MASS != 0) ? Math.Round(_altDet.CUH_TOT_GROSS_MASS, 2).ToString() : "0");

                return hedXml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}