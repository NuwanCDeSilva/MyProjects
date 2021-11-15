using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FastForward.WebAbansTours.Controllers
{
    public class ImageUploadController : BaseController
    {
        // GET: ImageUpload
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Multiple(IEnumerable<HttpPostedFileBase> files)
        {
            try
            {
                string userId = HttpContext.Session["UserID"] as string;
                string company = HttpContext.Session["UserCompanyCode"] as string;
                string userDefPro = HttpContext.Session["UserDefProf"] as string;
                string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
                string err;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    string mainfolder = "../Uploads";
                    string subfolder = "../Uploads/TEnquiry";
                    string contr = Request["controllers"].ToString();

                    bool existsmain = System.IO.Directory.Exists(Server.MapPath(mainfolder));
                    bool existsub = System.IO.Directory.Exists(Server.MapPath(subfolder));

                    if (!existsmain)
                        System.IO.Directory.CreateDirectory(Server.MapPath(mainfolder));

                    if (!existsub)
                        System.IO.Directory.CreateDirectory(Server.MapPath(subfolder));

                    List<TBS_IMG_UPLOAD> oMainList = new List<TBS_IMG_UPLOAD>();

                    if (Session["jobrole"] != null | Session["jobrole"] == "")
                    {
                        foreach (var file in files)
                        {
                            TBS_IMG_UPLOAD image = new TBS_IMG_UPLOAD();
                            if (file != null && file.ContentLength > 0)
                            {
                                string fileName = file.FileName;
                                string jobrole = Session["jobrole"].ToString();
                                string[] enqid = jobrole.Split(' ');
                                string jobroleNew = enqid[6];
                                string[] enqidnewfmt = jobroleNew.Split('/');
                                string[] name = fileName.Split('.');
                                image.Jbimg_img = enqidnewfmt[0] + enqidnewfmt[1] + enqidnewfmt[2] + "_" + fileName;
                                image.Jbimg_jobno = jobroleNew;
                                oMainList.Add(image);
                                Session["fileempty"] = "";
                                file.SaveAs(Path.Combine(Server.MapPath("../Uploads/TEnquiry/"), enqidnewfmt[0] + enqidnewfmt[1] + enqidnewfmt[2] + "_" + name[0] + Path.GetExtension(fileName)));
                            }
                            else
                            {
                                Session["fileempty"] = "Please Attach File ";
                                return RedirectToAction("Index", contr);
                            }

                        }
                        CHNLSVC.CustService.ServiceSaveScanImages(company, userDefPro, "Uploads/TEnquiry/", oMainList, out err);
                        Session["jobrole"] = "";
                        return RedirectToAction("Index", contr);
                    }
                    else
                    {
                        foreach (var file in files)
                        {
                            TBS_IMG_UPLOAD image = new TBS_IMG_UPLOAD();
                            if (file != null && file.ContentLength > 0)
                            {
                                string fileName = file.FileName;
                                string jobrole = Request["job_number"].ToString();
                                string[] enqidnewfmt = jobrole.Split('/');
                                if (enqidnewfmt.Length > 1)
                                {
                                    image.Jbimg_img = enqidnewfmt[0] + enqidnewfmt[1] + enqidnewfmt[2] + "_" + fileName;
                                }
                                else
                                {
                                    image.Jbimg_img = enqidnewfmt[0] + "_" + fileName;
                                }

                                string[] name = fileName.Split('.');
                                image.Jbimg_jobno = jobrole;
                                oMainList.Add(image);
                                if (enqidnewfmt.Length > 1)
                                {
                                    file.SaveAs(Path.Combine(Server.MapPath("../Uploads/TEnquiry/"), enqidnewfmt[0] + enqidnewfmt[1] + enqidnewfmt[2] + "_" + name[0] + Path.GetExtension(fileName)));
                                }
                                else
                                {
                                    file.SaveAs(Path.Combine(Server.MapPath("../Uploads/TEnquiry/"), enqidnewfmt[0] + "_" + name[0] + Path.GetExtension(fileName)));
                                }
                            }

                        }
                        CHNLSVC.CustService.ServiceSaveScanImages(company, userDefPro, "Uploads/TEnquiry/", oMainList, out err);

                        return RedirectToAction("Index", contr);
                    }


                }
                else
                {
                    return RedirectToAction("Index", "Account");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetImageDetails(string enqid)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                List<TBS_IMG_UPLOAD> oCostMainItems = CHNLSVC.CustService.GetImageDetails(enqid);
                List<TBS_IMG_UPLOAD> oCostMainItemsnew = new List<TBS_IMG_UPLOAD>();
                foreach (var oCostMainItems1 in oCostMainItems)
                {

                    string url = oCostMainItems1.Jbimg_img_path.ToString() + oCostMainItems1.Jbimg_img.ToString();
                    if (checkImage(url) == true)
                    {
                        oCostMainItemsnew.Add(oCostMainItems1);
                    }



                }
                return Json(new { success = true, login = true, data = oCostMainItemsnew }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
            }
        }
        private bool checkImage(string url)
        {
            WebRequest request = WebRequest.Create(Server.MapPath("~/") + url);

            try
            {
                WebResponse response = request.GetResponse();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}