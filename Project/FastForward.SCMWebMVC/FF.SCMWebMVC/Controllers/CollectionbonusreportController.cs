using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FF.BusinessObjects.BITool;
using FF.BusinessObjects.Commission;
using FF.BusinessObjects.Financial;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FF.SCMWebMVC.Controllers
{
    public class CollectionbonusreportController : BaseController
    {
        // GET: Collectionbonusreport
        List<hpt_arr_acc> pclist = new List<hpt_arr_acc>();
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["hpt_arr_acc"] = null;
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }

        //Dilshan 2017/10/03
        public JsonResult ViewDetails(string fdate, string tdate, string commcode)
        {
            string err = "";
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    //DateTime fdate1 = Convert.ToDateTime(fdate);
                    DateTime fdate1 = Convert.ToDateTime(fdate).AddMonths(1).AddDays(-1);

                    DateTime tdate1 = Convert.ToDateTime(tdate);
                    pclist = Session["hpt_arr_acc"] as List<hpt_arr_acc>;
                    string pc = "";
                    if (pclist != null)
                    {

                        foreach (var list in pclist)
                        {
                            //pc = pc + list.pccode + ",";
                            // Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), company, pc, null);

                        }
                    }

                    string path = CHNLSVC.MsgPortal.GetCollectionBonusExcl(fdate1, tdate1, company, userDefPro, commcode, userId, pc, out err);

                    _copytoLocal(path);
                    string pathnew = "/Temp/" + Session["UserID"].ToString() + ".xlsx";
                    return Json(new { login = true, success = true, number = 1, urlpath = pathnew }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, type = "err", msg = err }, JsonRequestBehavior.AllowGet);
            }
        }

        private void _copytoLocal(string _filePath)
        {
            string filenamenew = Session["UserID"].ToString();
            string name = _filePath;
            FileInfo file = new FileInfo(name);
            if (file.Exists)
            {
                string targetFileName = Server.MapPath("~\\Temp\\") + filenamenew + ".xlsx";
                // System.IO.File.Copy(@"" + _filePath, "C:/Download_excel/" + filenamenew + ".xlsx", true);
                System.IO.File.Copy(@"" + _filePath, targetFileName, true);
            }
            else
            {
                return;
            }
        }
    }
}