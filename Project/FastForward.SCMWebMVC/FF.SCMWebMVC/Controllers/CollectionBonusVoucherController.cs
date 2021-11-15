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
    public class CollectionBonusVoucherController : BaseController
    {
        // GET: CollectionBonusVoucher
        List<hpt_arr_acc> pclist = new List<hpt_arr_acc>();
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                Session["GPQualifiedAmount"] = null;
                Session["hpr_disr_val_ref"] = null;
                Session["hpr_disr_pc_defn"] = null;

                return View();
            }
            else
            {
                return Redirect("~/Login/index");
            }
        }

        public ActionResult GetBonusVoucher(string Date, string PcCode)
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

            _finaldt.Columns.Add("haa_pc", typeof(string));
            _finaldt.Columns.Add("haa_mng_cd", typeof(string));
            _finaldt.Columns.Add("haa_tot_net_bonus", typeof(decimal));
            _finaldt.Columns.Add("haa_com", typeof(string));
            _finaldt.Columns.Add("haa_epf", typeof(decimal));
            _finaldt.Columns.Add("haa_esd", typeof(decimal));
            _finaldt.Columns.Add("haa_netbonus", typeof(decimal));


            dr = param.NewRow();
            dr["comCode"] = company;
            dr["comName"] = comDtl.Rows[0].Field<string>("mc_desc");
            dr["comAdd"] = comDtl.Rows[0].Field<string>("mc_add1") + ", " + comDtl.Rows[0].Field<string>("mc_add2");
            param.Rows.Add(dr);

            DateTime FromDate = Convert.ToDateTime(Date).AddMonths(1).AddDays(-1);

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                string pc = "";
                pclist = Session["pc_list2"] as List<hpt_arr_acc>;
                if (pclist != null)
                {
                    foreach (var list in pclist)
                    {
                        //pc = pc + list.pccode + ",";
                    }
                }
                if (pc != null)
                {
                    ReportDocument rd = new ReportDocument();
                    DataTable dt = CHNLSVC.Finance.CollBonusVoucher(PcCode, FromDate, company);
                    drCon = _finaldt.NewRow();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if(i==0)
                        { 
                            //if (dt.Rows[i]["EmpType"].ToString() == "MANAGER") //dt.AsEnumerable().Any(row => "MANAGER" == row.Field<string>("EmpType"))
                            //{
                            //    drCon["ManContribution"] = dt.Rows[i]["Contribution"].ToString();
                            //    drCon["ManCode"] = dt.Rows[i]["ExeCode"].ToString();
                            //}
                            //else
                            //{
                            //    drCon["ExContribution"] = dt.Rows[i]["Contribution"].ToString();
                            //    drCon["ExeCode"] = dt.Rows[i]["ExeCode"].ToString();
                            //}
                        drCon["haa_pc"] = dt.Rows[i]["haa_pc"].ToString();
                        //drCon["InvoiceDate"] = Convert.ToDateTime(dt.Rows[i]["InvoiceDate"].ToString()).ToShortDateString();
                        drCon["haa_mng_cd"] = Date;
                        drCon["haa_tot_net_bonus"] = dt.Rows[i]["haa_tot_net_bonus"].ToString();
                        drCon["haa_com"] = dt.Rows[i]["haa_com"].ToString();
                        drCon["haa_epf"] = dt.Rows[i]["haa_epf"].ToString();
                        drCon["haa_esd"] = dt.Rows[i]["haa_esd"].ToString();
                        drCon["haa_netbonus"] = dt.Rows[i]["haa_netbonus"].ToString();

                        //if (_finaldt.AsEnumerable().Any(row => dt.Rows[i]["InvNo"].ToString() == row.Field<string>("InvNo") && dt.Rows[i]["item"].ToString() == row.Field<string>("item")))
                        //{
                        //    foreach (DataRow _dRow in _finaldt.AsEnumerable().Where(r => r.Field<string>("InvNo") == dt.Rows[i]["InvNo"].ToString() && r.Field<string>("item") == dt.Rows[i]["item"].ToString()))
                        //    {
                        //        if (dt.Rows[i]["EmpType"].ToString() == "MANAGER")
                        //        {
                        //            _dRow["ManContribution"] = dt.Rows[i]["Contribution"].ToString();
                        //            _dRow["ManCode"] = dt.Rows[i]["ExeCode"].ToString();
                        //        }
                        //        else
                        //        {
                        //            _dRow["ExContribution"] = dt.Rows[i]["Contribution"].ToString();
                        //            _dRow["ExeCode"] = dt.Rows[i]["ExeCode"].ToString();
                        //        }
                        //    }
                        //}
                        //else
                        //{
                            _finaldt.Rows.Add(drCon);
                        }
                        drCon = _finaldt.NewRow();
                    }
                    rd.Load(Server.MapPath("/Reports/" + "CollectionBonusVoucher.rpt"));
                    rd.Database.Tables["BonusCollection"].SetDataSource(_finaldt);
                    rd.Database.Tables["param"].SetDataSource(param);

                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();

                    try
                    {
                        this.Response.Clear();
                        this.Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "inline; filename=CollectionBonusVoucher.pdf");
                        return File(rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
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
    }
}