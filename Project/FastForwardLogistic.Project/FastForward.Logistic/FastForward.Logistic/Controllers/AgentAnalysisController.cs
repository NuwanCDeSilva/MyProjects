using FF.BusinessObjects.Genaral;
using FF.BusinessObjects.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastForward.Logistic.Controllers
{
    public class AgentAnalysisController : BaseController
    {
        // GET: AgentAnalysis
        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;

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
        public JsonResult getAgetPortDetails(string frmdt, string todt)
        {
            string userId = HttpContext.Session["UserID"] as string;
            string company = HttpContext.Session["UserCompanyCode"] as string;
            string userDefPro = HttpContext.Session["UserDefProf"] as string;
            string userDefLoc = HttpContext.Session["UserDefLoca"] as string;
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
                {
                    DateTime fromdate;
                    DateTime todate;
                    try
                    {
                        fromdate = Convert.ToDateTime(frmdt);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid from date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    try
                    {
                        todate = Convert.ToDateTime(todt);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, login = true, msg = "Please enter valid to date.", type = "Info" }, JsonRequestBehavior.AllowGet);
                    }
                    string error = string.Empty;
                    List<PortAgentDet> det = CHNLSVC.General.getPortDetails(fromdate, todate, company, out error);
                    if (det.Count > 0)
                    {
                        foreach (PortAgentDet dta in det)
                        {
                            DataTable crs = CHNLSVC.General.getShipmentContainers(fromdate, todate, company, dta.BL_PORT_LOAD, dta.BL_DEL_AGENT_CD);
                            string pots = "";
                            Int32 cnt = 1;
                            foreach (DataRow cm in crs.Rows)
                            {
                                string con_type = "";
                                if (cm["BLCT_CON_TP"].ToString()=="")
                                {
                                    con_type = "AIR";
                                }
                                else
                                {
                                    con_type = cm["BLCT_CON_TP"].ToString();
                                    //if(con_type=="0")
                                    //{
                                    //    con_type = "x";
                                    //}
                                }
                                //pots = pots + ((cnt != crs.Rows.Count) ? cm["BLCT_CON_TP"].ToString() + " * " + cm["CNT"].ToString() + " / " : cm["BLCT_CON_TP"].ToString() + " * " + cm["CNT"].ToString());
                                pots = pots + ((cnt != crs.Rows.Count) ? con_type + " * " + cm["CNT"].ToString() + " / " : con_type + " * " + cm["CNT"].ToString());
                                cnt++;
                            }
                            dta.CONTAINERFCL = pots;
                        }
                    }
                    List<BarChartDataPort> dataPort = CHNLSVC.General.getPortTotal(fromdate, todate, company);
                    List<BarChartDataAgent> dataAgent = CHNLSVC.General.getAgentTotal(fromdate, todate, company);

                    return Json(new { success = true, login = true, data = det, portTotal = dataPort, agentTotal = dataAgent }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = false, login = true, msg = ex.Message.ToString(), type = "Error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, login = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}