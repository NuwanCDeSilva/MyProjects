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
    public class CommissionProcessController : BaseController
    {
        //
        // GET: /CommissionProcess/
        List<Commission_pc> pclist = new List<Commission_pc>();
        public FF.SCMWebMVC.Reports.usercommissionTarget _commTarget = new Reports.usercommissionTarget();
        List<DELI_SALE_NEW> invoicedet = new List<DELI_SALE_NEW>();
        List<DELI_SALE_NEW> totsalelistavg = new List<DELI_SALE_NEW>();
        List<SalesTarget> SlsTarList = new List<SalesTarget>();

        Services.ChannelOperator bsObj;
        public CommissionProcessController()
        {
            bsObj = new Services.ChannelOperator();
        }
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["pc_list2"] = null;
                Session["all_sale"] = null;
                Session["Commissions"] = null;
                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }
        public JsonResult LoadCommissions(string ProfitCenter, string FromDate, string ToDate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string error = "";
                List<Invoice_Commission> summery = new List<Invoice_Commission>();
                List<DELI_SALE_NEW> all = new List<DELI_SALE_NEW>();
                pclist = Session["pc_list2"] as List<Commission_pc>;
                if (pclist == null || pclist.Count==0)
                {
                    return Json(new { success = false, login = true, type = "info", msg = "Please Select ProfitCenter" }, JsonRequestBehavior.AllowGet);
                }

                int eff = CHNLSVC.Finance.CommissionProcess(company, pclist, Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), out error, out summery, out all);
                Session["all_sale"] = all;
                Session["Commissions"] = summery;
                List<Invoice_Commission> comm = summery;
                List<Invoice_Commission> EMP = new List<Invoice_Commission>();
                string err = "";
                DateTime fdate = Convert.ToDateTime(FromDate);
                DateTime tdate = Convert.ToDateTime(ToDate);

                bool aa = summery.Any(x => x.InvoiceNo == "WPCD-01279");
                bool bb = all.Any(x => x.Inv_no == "WPCD-01279");
                //save
                foreach (var comdete in comm)
                {
                    if (comdete.EmpCommission > 0)
                    {
                        EMP.Add(comdete);
                    }
                }


                int effect = CHNLSVC.Finance.SaveCommissionInvoices(comm, EMP, fdate, tdate, out err);

                summery = summery.GroupBy(l => new { l.ExecCode, l.Item })
   .Select(cl => new Invoice_Commission
   {
       ExecCode = cl.First().ExecCode,
       ExecName = cl.First().ExecName,
       InvoiceNo = cl.First().InvoiceNo,
       Item = cl.First().Item,
       Qty = cl.Sum(a => a.Qty),
       TotValue = cl.Sum(a => a.TotValue),
       ItemCommission = cl.Sum(a => a.ItemCommission),
       EmpCommission = cl.Sum(a => a.EmpCommission),
       FinalCommission = cl.Sum(a => a.FinalCommission),
       taxammount = cl.Sum(a => a.taxammount),
       discountammount = cl.Sum(a => a.discountammount),
       commissioncode = cl.First().commissioncode
   }).ToList();
                return Json(new { success = true, login = true, summery = summery, effect = effect }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult LoadEiteCommissions(string ProfitCenter, string FromDate, string ToDate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string error = "";
                List<Invoice_Commission> summery = new List<Invoice_Commission>();
                List<DELI_SALE_NEW> all = new List<DELI_SALE_NEW>();
                pclist = Session["pc_list2"] as List<Commission_pc>;
                if (pclist == null)
                {
                    return Json(new { success = false, login = true, type = "info", msg = "Please Select ProfitCenter" }, JsonRequestBehavior.AllowGet);
                }

                int eff = CHNLSVC.Finance.CommissionProcess(company, pclist, Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), out error, out summery, out all);
                Session["all_sale"] = all;
                Session["Commissions"] = summery;
                List<Invoice_Commission> comm = summery;
                List<Invoice_Commission> EMP = new List<Invoice_Commission>();
                string err = "";
                DateTime fdate = Convert.ToDateTime(FromDate);
                DateTime tdate = Convert.ToDateTime(ToDate);
                //save
                foreach (var comdete in comm)
                {
                    if (comdete.EmpCommission > 0)
                    {
                        EMP.Add(comdete);
                    }
                }


                int effect = CHNLSVC.Finance.SaveCommissionInvoices(comm, EMP, fdate, tdate, out err);

                summery = summery.GroupBy(l => new { l.ExecCode, l.Item })
   .Select(cl => new Invoice_Commission
   {
       ExecCode = cl.First().ExecCode,
       ExecName = cl.First().ExecName,
       InvoiceNo = cl.First().InvoiceNo,
       Item = cl.First().Item,
       Qty = cl.Sum(a => a.Qty),
       TotValue = cl.Sum(a => a.TotValue),
       ItemCommission = cl.Sum(a => a.ItemCommission),
       EmpCommission = cl.Sum(a => a.EmpCommission),
       FinalCommission = cl.Sum(a => a.FinalCommission),
       taxammount = cl.Sum(a => a.taxammount),
       discountammount = cl.Sum(a => a.discountammount),
       commissioncode = cl.First().commissioncode
   }).ToList();
                return Json(new { success = true, login = true, summery = summery, effect = effect }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult AddProfitCenters(string proficenter)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (Session["pc_list2"] == null)
                {
                    pclist = new List<Commission_pc>();
                    Commission_pc ob = new Commission_pc();
                    ob.pccode = proficenter;
                    pclist.Add(ob);
                    Session["pc_list2"] = pclist;
                }
                else
                {
                    pclist = Session["pc_list2"] as List<Commission_pc>;
                    var count = pclist.Where(a => a.pccode == proficenter).Count();
                    if (count > 0)
                    {
                        return Json(new { success = false, login = true, msg = "Already added this pc!!", data = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Commission_pc ob = new Commission_pc();
                        ob.pccode = proficenter;
                        pclist.Add(ob);
                        Session["pc_list2"] = pclist;
                    }
                }

                if (pclist.Count > 0)
                {
                    return Json(new { success = true, login = true, data = pclist }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = true, msg = "No items found for this search criteria.", data = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    return Json(new { success = false, login = true, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult RemovePCCode(string profitcenter)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {

                if (Session["pc_list2"] != null)
                {
                    pclist = (List<Commission_pc>)Session["pc_list2"];
                }
                else
                {
                    pclist = new List<Commission_pc>();

                }
                var itemToRemove = pclist.First(r => r.pccode == profitcenter);
                pclist.Remove(itemToRemove);
                Session["pc_list2"] = pclist;
                return Json(new { success = true, login = true, data = pclist }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetPC(string Chanal)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<Sim_Pc> _channelpc = CHNLSVC.Dashboard.GetPcInfoData("CHNL", Chanal);
                if (_channelpc != null)
                {
                    foreach (var pcs in _channelpc)
                    {
                        Commission_pc ob = new Commission_pc();
                        ob.pccode = pcs.pc;
                        pclist.Add(ob);
                    }
                }
                pclist = pclist.GroupBy(l => new { l.pccode }).Select(cl => new Commission_pc
                {
                    pccode = cl.FirstOrDefault().pccode
                }).ToList();
                Session["pc_list2"] = pclist;
                return Json(new { success = true, login = true, pclist = pclist }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetInvoiceNumbers(string ExecCode)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<Invoice_Commission> comm = Session["Commissions"] as List<Invoice_Commission>;
                comm = comm.Where(a => a.ExecCode == ExecCode).ToList();
                return Json(new { success = true, login = true, allinvoice = comm }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult SaveCommissions(string Fromdate, string Todate)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                List<Invoice_Commission> EMP = new List<Invoice_Commission>();
                string err = "";
                DateTime fdate = Convert.ToDateTime(Fromdate);
                DateTime tdate = Convert.ToDateTime(Todate);
                List<Invoice_Commission> comm = Session["Commissions"] as List<Invoice_Commission>;
                //save
                foreach (var comdete in comm)
                {
                    if (comdete.EmpCommission > 0)
                    {
                        EMP.Add(comdete);
                    }
                }


                int effect = CHNLSVC.Finance.SaveCommissionInvoices(comm, EMP, fdate, tdate, out err);
                if (effect == 1)
                {
                    return Json(new { success = true, login = true, msg = "Successfully Finalized" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, login = true, type = "err", msg = err }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = false, login = false, data = "loggin pls" }, JsonRequestBehavior.AllowGet);
            }

        }

        //Isuru 2017/03/28
        public JsonResult ViewDetails(DateTime fdate, DateTime tdate, string commcode)
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
                    pclist = Session["pc_list2"] as List<Commission_pc>;
                    string pc = "";
                    if (pclist != null)
                    {

                        foreach (var list in pclist)
                        {
                            pc = pc + list.pccode + ",";
                            // Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), company, pc, null);

                        }
                    }



                    // DataTable eff = new DataTable();

                    //eff = CHNLSVC.Finance.GET_Commission_Details(fdate, tdate, commcode, userId);
                    // return Json(new { success = true, login = true, data = eff }, JsonRequestBehavior.AllowGet);

                    string path = CHNLSVC.MsgPortal.GetCommisionDetailExcl(fdate, tdate, company, userDefPro, commcode, userId, pc, out err);

                    _copytoLocal(path);
                    string pathnew = "/Temp/" + Session["UserID"].ToString() + ".xlsx";
                    return Json(new { login = true, success = true, number = 1, urlpath = pathnew }, JsonRequestBehavior.AllowGet);
                }


                    //return Json(new { success = true, login = true, data = eff }, JsonRequestBehavior.AllowGet);

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
        //Udaya Commission Summary Report cr
        public JsonResult ViewCommissions(string ProfitCenter, DateTime FromDate, DateTime ToDate, string circularCode)
        {
            string _err = "";
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            pclist = Session["pc_list2"] as List<Commission_pc>;
            if (pclist == null)
            {
                pclist = new List<Commission_pc>();
            }

            pclist = Session["pc_list2"] as List<Commission_pc>;
            if (pclist != null)
            {
                foreach (var list in pclist)
                {
                    string pc = list.pccode;
                    Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), company, pc, null);
                }
            }

            if (!string.IsNullOrEmpty(FromDate.ToString()) && !string.IsNullOrEmpty(ToDate.ToString()) && !string.IsNullOrEmpty(circularCode))
            {
                try
                {
                    string path = CHNLSVC.MsgPortal.GetCommisionProcessReport(FromDate, ToDate, company, userId, circularCode, Session["UserCompanyName"].ToString(), out _err);
                    _copytoLocal(path);
                    string pathnew = "/Temp/" + Session["UserID"].ToString() + ".xlsx";
                    if (_err == "" || _err == null)
                    {
                        return Json(new { login = true, success = true, number = 1, urlpath = pathnew }, JsonRequestBehavior.AllowGet);
                    }
                    else
                        return Json(new { success = false, login = true, type = "err", msg = _err }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, login = true, type = "err", msg = _err }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, login = true, data = "Please Check Report Empty Paramaters", msg = "Please Select At Least One Parameter Instead Of Date" }, JsonRequestBehavior.AllowGet);
            }
        }

        //Udaya With Target Report cr, 04.07.2017
        public ActionResult ViewWithTarget(DateTime FromDate, DateTime ToDate, string circularCode, bool ExcelChecked, string EmpCode)
        {
            string transcomm = string.Empty;
            DataTable sales_Level = new DataTable();
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            DataTable comDtl = CHNLSVC.General.GetCompanyByCode(company);
            DataTable salesInvTypes = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            param.Columns.Add("comCode", typeof(string));
            param.Columns.Add("comName", typeof(string));
            param.Columns.Add("comAdd", typeof(string));

            dr = param.NewRow();
            dr["comCode"] = company;
            dr["comName"] = comDtl.Rows[0].Field<string>("mc_desc");
            dr["comAdd"] = comDtl.Rows[0].Field<string>("mc_add1") + ", " + comDtl.Rows[0].Field<string>("mc_add2");
            param.Rows.Add(dr);

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string pc = "";
                pclist = Session["pc_list2"] as List<Commission_pc>;
                if (pclist != null)
                {
                    foreach (var list in pclist)
                    {
                        pc = pc + list.pccode + ",";
                    }
                }
                if (pc != null && !string.IsNullOrEmpty(circularCode))
                {
                    List<DELI_SALE_NEW> _salesTrget = new List<DELI_SALE_NEW>();
                    List<SalesTarget> SlsTarList = new List<SalesTarget>();
                    ReportDocument rd = new ReportDocument();
                    DataTable dt = CHNLSVC.Finance.CommisionReport(circularCode, pc, FromDate, ToDate, company, EmpCode, out _salesTrget, out SlsTarList, out sales_Level);
                    //SlsTarList = SlsTarList == null ? new List<SalesTarget>() : SlsTarList.Where(r => r.ExecutiveCode == EmpCode).ToList();

                    rd.Load(Server.MapPath("/Reports/" + "usercommissionTarget.rpt"));
                    rd.Database.Tables["CommisionTarget"].SetDataSource(dt);
                    rd.Database.Tables["param"].SetDataSource(param);

                    foreach (object repOp in rd.ReportDefinition.ReportObjects)
                    {
                        string _s = repOp.GetType().ToString();
                        if (_s.ToLower().Contains("subreport"))
                        {
                            SubreportObject _cs = (SubreportObject)repOp;
                            if (_cs.SubreportName == "rptTargetAchievement")
                            {
                                ReportDocument subRepDoc = rd.Subreports[_cs.SubreportName];
                                  subRepDoc.Database.Tables["TargetAchievement"].SetDataSource(_salesTrget);
                            }
                            if (_cs.SubreportName == "rptTargetLevel")
                            {
                                ReportDocument subRepDoc = rd.Subreports[_cs.SubreportName];
                                subRepDoc.Database.Tables["TargetLevel"].SetDataSource(sales_Level);
                            }
                            if (_cs.SubreportName == "SalesETarget")
                            {
                                ReportDocument subRepDoc = rd.Subreports[_cs.SubreportName];
                                subRepDoc.Database.Tables["SalesExTarget"].SetDataSource(SlsTarList);
                            }
                        }
                    }

                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();

                    try
                    {
                        if (ExcelChecked)
                        {
                            string path = "/Temp";//@"C:\Download_excel";
                            string excelAttch1 = "";
                            excelAttch1 = path + "\\ExecutiveTarget.xls";
                            string error = string.Empty;
                            ExportOptions CrExportOptions;

                            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                            ExcelFormatOptions CrFormatTypeOptions = new ExcelFormatOptions();
                            CrDiskFileDestinationOptions.DiskFileName = excelAttch1;
                            CrExportOptions = rd.ExportOptions;
                            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            CrExportOptions.ExportFormatType = ExportFormatType.Excel;
                            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                            CrExportOptions.FormatOptions = CrFormatTypeOptions;
                            rd.Export();
                            return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel), "application/xls");
                        }
                        else
                        {
                            this.Response.Clear();
                            this.Response.ContentType = "application/pdf";
                            Response.AppendHeader("Content-Disposition", "inline; filename=ExecutiveTarget.pdf");
                            return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                        }
                    }
                    catch(Exception ex)
                    {
                        throw;
                    }
                }
                else
                {
                    return Redirect("~/CommissionProcess");
                }
            }
            else
            {
                return Redirect("~/Login");
            }
        }

        //Added By Udaya 08.09.207 Collect Sales Exc Contribution
        public ActionResult SalesContribution(DateTime FromDate, DateTime ToDate, string circularCode, bool ExcelChecked)
        {
            string transcomm = string.Empty;
            DataTable sales_Level = new DataTable();
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            DataTable comDtl = CHNLSVC.General.GetCompanyByCode(company);
            DataTable salesInvTypes = new DataTable();
            DataTable param = new DataTable();
            DataTable _finaldt = new DataTable();
            DataRow dr;
            DataRow drCon;
            param.Columns.Add("comCode", typeof(string));
            param.Columns.Add("comName", typeof(string));
            param.Columns.Add("comAdd", typeof(string));

            _finaldt.Columns.Add("InvNo", typeof(string));
            _finaldt.Columns.Add("InvoiceDate", typeof(string));
            _finaldt.Columns.Add("ExeCode", typeof(string));
            _finaldt.Columns.Add("ManCode", typeof(string));
            _finaldt.Columns.Add("Description", typeof(string));
            _finaldt.Columns.Add("Qty", typeof(decimal));
            _finaldt.Columns.Add("Soldat", typeof(decimal));
            _finaldt.Columns.Add("Discount", typeof(decimal));
            _finaldt.Columns.Add("ManContribution", typeof(decimal));
            _finaldt.Columns.Add("ExContribution", typeof(decimal));
            _finaldt.Columns.Add("item", typeof(string));
            _finaldt.Columns.Add("Total", typeof(decimal));
            _finaldt.Columns.Add("ReceiptDate", typeof(string));

            dr = param.NewRow();
            dr["comCode"] = company;
            dr["comName"] = comDtl.Rows[0].Field<string>("mc_desc");
            dr["comAdd"] = comDtl.Rows[0].Field<string>("mc_add1") + ", " + comDtl.Rows[0].Field<string>("mc_add2");
            param.Rows.Add(dr);

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string pc = "";
                pclist = Session["pc_list2"] as List<Commission_pc>;
                if (pclist != null)
                {
                    foreach (var list in pclist)
                    {
                        pc = pc + list.pccode + ",";
                    }
                }
                if (pc != null)
                {
                    ReportDocument rd = new ReportDocument();
                    DataTable dt = CHNLSVC.Finance.SalesEx_Contribution(circularCode, pc, FromDate, ToDate, company);
                    drCon = _finaldt.NewRow();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i >= 1)
                        {
                            if (dt.Rows[i]["InvNo"].ToString() == dt.Rows[i - 1]["InvNo"].ToString() && dt.Rows[i]["item"].ToString() == dt.Rows[i - 1]["item"].ToString())
                            {
                                if (dt.Rows[i]["EmpType"].ToString() == "MANAGER") //dt.AsEnumerable().Any(row => "MANAGER" == row.Field<string>("EmpType"))
                                {
                                    drCon["ManContribution"] = dt.Rows[i]["Contribution"].ToString();
                                    drCon["ManCode"] = dt.Rows[i]["ExeCode"].ToString();
                                }
                                else
                                {
                                    drCon["ExContribution"] = dt.Rows[i]["Contribution"].ToString();
                                    drCon["ExeCode"] = dt.Rows[i]["ExeCode"].ToString();
                                }
                                drCon["InvNo"] = dt.Rows[i]["InvNo"].ToString();
                                drCon["InvoiceDate"] = Convert.ToDateTime(dt.Rows[i]["InvoiceDate"].ToString()).ToShortDateString();
                                drCon["Description"] = dt.Rows[i]["Description"].ToString();
                                drCon["Qty"] = dt.Rows[i]["Qty"].ToString();
                                drCon["Soldat"] = dt.Rows[i]["Soldat"].ToString();
                                drCon["Discount"] = dt.Rows[i]["Discount"].ToString();
                                drCon["item"] = dt.Rows[i]["item"].ToString();
                                drCon["Total"] = dt.Rows[i]["Total"].ToString();
                                drCon["ReceiptDate"] = Convert.ToDateTime(dt.Rows[i]["ReceiptDate"].ToString()).ToShortDateString();
                            }
                            else
                            {
                                if (dt.Rows[i]["EmpType"].ToString() == "MANAGER")//dt.AsEnumerable().Any(row => "MANAGER" == row.Field<string>("EmpType"))
                                {
                                    drCon["ManContribution"] = dt.Rows[i]["Contribution"].ToString();
                                    drCon["ManCode"] = dt.Rows[i]["ExeCode"].ToString();
                                }
                                else
                                {
                                    drCon["ExContribution"] = dt.Rows[i]["Contribution"].ToString();
                                    drCon["ExeCode"] = dt.Rows[i]["ExeCode"].ToString();
                                }
                                drCon["InvNo"] = dt.Rows[i]["InvNo"].ToString();
                                drCon["InvoiceDate"] = Convert.ToDateTime(dt.Rows[i]["InvoiceDate"].ToString()).ToShortDateString();
                                drCon["Description"] = dt.Rows[i]["Description"].ToString();
                                drCon["Qty"] = dt.Rows[i]["Qty"].ToString();
                                drCon["Soldat"] = dt.Rows[i]["Soldat"].ToString();
                                drCon["Discount"] = dt.Rows[i]["Discount"].ToString();
                                drCon["item"] = dt.Rows[i]["item"].ToString();
                                drCon["Total"] = dt.Rows[i]["Total"].ToString();
                                drCon["ReceiptDate"] = Convert.ToDateTime(dt.Rows[i]["ReceiptDate"].ToString()).ToShortDateString();
                            }
                        }
                        else
                        {
                            if (dt.Rows[i]["EmpType"].ToString() == "MANAGER") //dt.AsEnumerable().Any(row => "MANAGER" == row.Field<string>("EmpType"))
                            {
                                drCon["ManContribution"] = dt.Rows[i]["Contribution"].ToString();
                                drCon["ManCode"] = dt.Rows[i]["ExeCode"].ToString();
                            }
                            else
                            {
                                drCon["ExContribution"] = dt.Rows[i]["Contribution"].ToString();
                                drCon["ExeCode"] = dt.Rows[i]["ExeCode"].ToString();
                            }
                            drCon["InvNo"] = dt.Rows[i]["InvNo"].ToString();
                            drCon["InvoiceDate"] = Convert.ToDateTime(dt.Rows[i]["InvoiceDate"].ToString()).ToShortDateString();
                            drCon["Description"] = dt.Rows[i]["Description"].ToString();
                            drCon["Qty"] = dt.Rows[i]["Qty"].ToString();
                            drCon["Soldat"] = dt.Rows[i]["Soldat"].ToString();
                            drCon["Discount"] = dt.Rows[i]["Discount"].ToString();
                            drCon["item"] = dt.Rows[i]["item"].ToString();
                            drCon["Total"] = dt.Rows[i]["Total"].ToString();
                            drCon["ReceiptDate"] = Convert.ToDateTime(dt.Rows[i]["ReceiptDate"].ToString()).ToShortDateString();
                        }
                        if (_finaldt.AsEnumerable().Any(row => dt.Rows[i]["InvNo"].ToString() == row.Field<string>("InvNo") && dt.Rows[i]["item"].ToString() == row.Field<string>("item")))
                        {
                            foreach (DataRow _dRow in _finaldt.AsEnumerable().Where(r => r.Field<string>("InvNo") == dt.Rows[i]["InvNo"].ToString() && r.Field<string>("item") == dt.Rows[i]["item"].ToString()))
                            {
                                if (dt.Rows[i]["EmpType"].ToString() == "MANAGER")
                                {
                                    _dRow["ManContribution"] = dt.Rows[i]["Contribution"].ToString();
                                    _dRow["ManCode"] = dt.Rows[i]["ExeCode"].ToString();
                                }
                                else
                                {
                                    _dRow["ExContribution"] = dt.Rows[i]["Contribution"].ToString();
                                    _dRow["ExeCode"] = dt.Rows[i]["ExeCode"].ToString();
                                }
                            }
                        }
                        else
                        {
                            _finaldt.Rows.Add(drCon);
                        }
                        drCon = _finaldt.NewRow();
                    }
                    rd.Load(Server.MapPath("/Reports/" + "SalesExContribution.rpt"));
                    rd.Database.Tables["SalesExContribution"].SetDataSource(_finaldt);
                    rd.Database.Tables["param"].SetDataSource(param);

                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();

                    try
                    {
                        if (ExcelChecked)
                        {
                            string path = "/Temp";//@"C:\Download_excel";
                            string excelAttch1 = "";
                            excelAttch1 = path + "\\ExecutiveTarget.xls";
                            string error = string.Empty;
                            ExportOptions CrExportOptions;

                            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                            ExcelFormatOptions CrFormatTypeOptions = new ExcelFormatOptions();
                            CrDiskFileDestinationOptions.DiskFileName = excelAttch1;
                            CrExportOptions = rd.ExportOptions;
                            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            CrExportOptions.ExportFormatType = ExportFormatType.Excel;
                            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                            CrExportOptions.FormatOptions = CrFormatTypeOptions;
                            rd.Export();
                            return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel), "application/xls");
                        }
                        else
                        {
                            this.Response.Clear();
                            this.Response.ContentType = "application/pdf";
                            Response.AppendHeader("Content-Disposition", "inline; filename=ExecutiveTarget.pdf");
                            return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                else
                {
                    return Redirect("~/CommissionProcess");
                }
            }
            else
            {
                return Redirect("~/Login");
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