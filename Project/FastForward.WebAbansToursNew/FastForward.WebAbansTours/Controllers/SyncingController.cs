using FF.BusinessObjects;
using FF.BusinessObjects.ToursNew;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastForward.WebAbansTours.Controllers
{
    public class SyncingController : Controller
    {
        // GET: Syncing
        public ActionResult Index()
        {
            return View();
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
                 
                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName) && (file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || file.ContentType == "application/vnd.ms-excel" || file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.template" || file.ContentType == "application/vnd.ms-excel.sheet.macroEnabled.12"))
                    {
                        string fileName = file.FileName;
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        string query;
                        string fields="";
                        string queryHeader = "INSERT INTO table_name (";
                        string queryHeader2 = "VALUES (";
                        string values = "";
                        string Mainquery="";
                        // var _msc = new List<SR_SER_MISS>();
                        string name = "C:/Users/nuwanc/Desktop/Query.txt";
                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;
                            List<string> variabletypelist = new List<string>();
                            for (int rowIterator = 1; rowIterator <=1; rowIterator++)
                            {
                              


                                for (int i = 1; i <= noOfCol; i++)
                                {
                                    if (i == noOfCol)
                                    {
                                        string colfield = (workSheet.Cells[rowIterator, i].Value!=null)?workSheet.Cells[rowIterator, i].Value.ToString():"";
                                        string[] words = colfield.Split('.');
                                        variabletypelist.Add(words[1]);
                                        fields = fields + words[0];
                                    }
                                    else
                                    {
                                        string colfield = (workSheet.Cells[rowIterator, i].Value!=null)?workSheet.Cells[rowIterator, i].Value.ToString():"";
                                       
                                            string[] words = colfield.Split('.');
                                            variabletypelist.Add(words[1]);
                                            fields = fields + words[0] + ",";
                                    }
                                  
                                }

                              

                            }

                            string qu1version = queryHeader + fields+")";
                          //  ViewBag.qu1version = qu1version;


                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                var _fleet = new mst_fleet();
                                values = "";

                                for (int i = 1; i <= noOfCol; i++)
                                {
                                    var col = workSheet.Cells[rowIterator, i].Value;
                                   
                                    if (col == null && variabletypelist[i - 1] == "NVARCHAR2") col = "";
                                    if (col == null && variabletypelist[i - 1] == "NUMBER") col =0;
                                    if (col == null && variabletypelist[i - 1] == "DATE") col = DateTime.MinValue;
                                    if (variabletypelist[i - 1] == "DATE")
                                    {
                                        string[] daynon = col.ToString().Split(' ');
                                        string[] dayan2 = daynon[0].Split('/');
                                        if (dayan2[2] == "0000") col = DateTime.MinValue;
                                    }
                                    
                                    if (i == noOfCol && variabletypelist[i-1]=="NVARCHAR2")
                                    {
                                        col = col.ToString().Replace("'", "");
                                        values = values +"'"+ col+"'";
                                    }
                                    if (i != noOfCol && variabletypelist[i - 1] == "NVARCHAR2")
                                    {
                                        col = col.ToString().Replace("'", "");
                                        values = values + "'" + col + "'" + ",";
                                    }
                                    if (i == noOfCol && variabletypelist[i - 1] == "NUMBER")
                                    {
                                        values = values  +Convert.ToDecimal( col) ;
                                    }
                                    if (i != noOfCol && variabletypelist[i - 1] == "NUMBER")
                                    {
                                        values = values + Convert.ToDecimal(col)  + ",";
                                    }
                                    if (i == noOfCol && variabletypelist[i - 1] == "DATE")
                                    {
                                        string date = col.ToString();
                                        DateTime datet = Convert.ToDateTime(date);
                                        date = datet.ToString("yyyy/MM/dd HH:mm");
                                        values = values + " TO_DATE('" + date + "' , 'yyyy/mm/dd hh24:mi:ss')";
                                    }
                                    if (i != noOfCol && variabletypelist[i - 1] == "DATE")
                                    {
                                        string date = col.ToString();
                                        DateTime datet = Convert.ToDateTime(date);
                                        date = datet.ToString("yyyy/MM/dd HH:mm");
                                        values = values + " TO_DATE('" + date + "' , 'yyyy/mm/dd hh24:mi:ss')" + ",";
                                    }
                                }

                                //values = values + Environment.NewLine;
                                query = qu1version + queryHeader2 + values + ");";
                                Mainquery = Mainquery+ query + Environment.NewLine;
                                Session["DataAdd"] = Mainquery;
                                FileInfo info = new FileInfo(name);
                                if (info.Exists || !info.Exists)
                                {
                                    using (StreamWriter writer = info.CreateText())
                                    {
                                        writer.WriteLine(Mainquery);
                                    }
                                }
                            }
                           
                                return RedirectToAction("Index");
                            


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


    }
}