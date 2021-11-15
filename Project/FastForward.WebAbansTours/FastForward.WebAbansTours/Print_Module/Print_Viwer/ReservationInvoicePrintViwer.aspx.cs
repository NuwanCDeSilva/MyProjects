using CrystalDecisions.CrystalReports.Engine;
using FastForward.WebAbansTours.Controllers;
using FF.BusinessObjects;
using FF.BusinessObjects.ToursNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.WebAbansTours.Print_Module.Print_Viwer
{
    public partial class EnquiryInvoicePrintViwer : System.Web.UI.Page
    {
        public FastForward.WebAbansTours.Print_Module.Reports.EnquiryInvoice_Report _enquiryInvocingFormat = new Reports.EnquiryInvoice_Report();
        public FastForward.WebAbansTours.Print_Module.Reports.EnquiryInvoice_ReportRCSL _enquiryInvocingFormatRCSL = new Reports.EnquiryInvoice_ReportRCSL();

        CrystalDecisions.CrystalReports.Engine.ReportDocument reportDocument = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string userId = Session["UserID"] as string;
            string company = Session["UserCompanyCode"] as string;
            string userDefPro = Session["UserDefProf"] as string;
            string userDefLoc = Session["UserDefLoca"] as string;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(userDefPro) && !string.IsNullOrEmpty(userDefLoc))
            {
                
                string enqNo = Session["enqNo"] as string;
                if (enqNo != "")
                {
                    DataTable EnquiryInvoice = new DataTable();
                    DataTable InvoiceItems = new DataTable();
                    DataTable param = new DataTable();
                    BaseController baseCon = new BaseController();
                    ReportDocument crystalReport = new ReportDocument();

                    EnquiryInvoice = baseCon.CHNLSVC.Tours.getEnquiryHeaderData(enqNo, userDefPro);
                    InvoiceItems = baseCon.CHNLSVC.Tours.getEnquiryInvoiceItems(enqNo);

                    List<GEN_CUS_ENQ_DRIVER> drivers = baseCon.CHNLSVC.Tours.getEnquiryDriverDetails(enqNo);
                    List<GEN_CUS_ENQ_FLEET> FLEET = baseCon.CHNLSVC.Tours.getEnquiryFleetDetails(enqNo);
                    string driver = "";
                    if (drivers.Count > 0) {
                        int i = 1;
                        List<GEN_CUS_ENQ_DRIVER> drive = drivers.Where(y => y.GCD_ACT ==1).GroupBy(x => x.GCD_DRIVER_CD).Select(y => y.First()).ToList();
                        foreach (GEN_CUS_ENQ_DRIVER dri in drive)
                        {
                            driver += dri.GCD_DRIVER_NAME + ((i != drive.Count) ? "/" : "");
                            i++;
                        }
                    }
                    string flet = "";
                    if (FLEET.Count > 0)
                    {
                        int i = 1;
                        List<GEN_CUS_ENQ_FLEET> FLET = FLEET.Where(y => y.GCF_ACT == 1).GroupBy(x => x.GCF_FLEET).Select(y => y.First()).ToList();
                        foreach (GEN_CUS_ENQ_FLEET fle in FLET)
                        {
                            flet += fle.GCF_FLEET + ((i != FLET.Count) ? "/" : "");
                            i++;
                        }
                    }
                    GEN_CUST_ENQ oItem = baseCon.CHNLSVC.Tours.getEnquiryDetailsTours(company, userDefPro, enqNo);

                    param.Columns.Add("DRIVER", typeof(String));
                    param.Columns.Add("VEHICLE", typeof(String));
                    param.Columns.Add("FROMTN", typeof(String));
                    param.Columns.Add("TOTN", typeof(String));
                    param.Columns.Add("INVNOS", typeof(String));
                    param.Columns.Add("RENTALAGREMENT", typeof(String));
                    param.Columns.Add("MILES", typeof(decimal));

                    string fromTwn = "";
                    string toTwn="";
                    List<MST_FAC_LOC> facList = baseCon.CHNLSVC.Tours.getFacLocations(company, userDefPro);
                    if (facList.Count > 0)
                    {
                        bool exists = facList.Exists(element => element.FAC_CODE == oItem.GCE_FRM_TN);
                        if (exists)
                        {
                            fromTwn= facList.Where(element => element.FAC_CODE == oItem.GCE_FRM_TN).First().FAC_DESC;
                        }
                        bool exists1 = facList.Exists(element => element.FAC_CODE == oItem.GCE_TO_TN);
                        if (exists1)
                        {
                            toTwn= facList.Where(element => element.FAC_CODE == oItem.GCE_TO_TN).First().FAC_DESC;
                        }


                    }else{
                        DataTable dt=new DataTable();
                        dt = baseCon.CHNLSVC.General.Get_DetBy_town(oItem.GCE_FRM_TN);
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                               fromTwn = dt.Rows[0]["TOWN"].ToString();
                            }
                        }
                         DataTable dt1=new DataTable();
                        dt1 = baseCon.CHNLSVC.General.Get_DetBy_town(oItem.GCE_TO_TN);
                        if (dt1 != null)
                        {
                            if (dt1.Rows.Count > 0)
                            {
                               toTwn = dt1.Rows[0]["TOWN"].ToString();
                            }
                        }
                    }
                    string invNo = "";
                   
                    if (oItem != null && oItem.ENQ_CHARGES != null && oItem.ENQ_CHARGES.Count > 0) {
                        int i = 1;
                        List<ST_ENQ_CHARGES> DistinctItems = oItem.ENQ_CHARGES.Where(y=>y.SCH_INVOICED_NO!="").GroupBy(x => x.SCH_INVOICED_NO).Select(y => y.First()).ToList();
                        foreach (ST_ENQ_CHARGES chg in DistinctItems)
                        {
                            if (chg.SCH_INVOICED == 1 && chg.SCH_INVOICED_NO!="")
                            {
                                invNo += chg.SCH_INVOICED_NO + ((i != DistinctItems.Count) ? "/" : "");
                                i++;
                            }
                        }
                    }
                    MST_CHKINOUT chkOut = baseCon.CHNLSVC.Tours.getEnqChkData(enqNo);
                    param.Rows.Add(driver, flet, fromTwn, toTwn, invNo, "", chkOut.CHK_IN_KM - chkOut.CHK_OUT_KM);

                    if (this.reportDocument != null)
                    {
                        this.reportDocument.Close();
                        this.reportDocument.Dispose();
                    }
                    if (company == "LGR")
                    {
                        _enquiryInvocingFormatRCSL.Database.Tables["EnquiryInvoice"].SetDataSource(EnquiryInvoice);
                        _enquiryInvocingFormatRCSL.Database.Tables["InvoiceItems"].SetDataSource(InvoiceItems);
                        _enquiryInvocingFormatRCSL.Database.Tables["param"].SetDataSource(param);
                        ReserInvoicingReport.ReportSource = _enquiryInvocingFormatRCSL;
                    }
                    else
                    {
                        _enquiryInvocingFormat.Database.Tables["EnquiryInvoice"].SetDataSource(EnquiryInvoice);
                        _enquiryInvocingFormat.Database.Tables["InvoiceItems"].SetDataSource(InvoiceItems);
                        _enquiryInvocingFormat.Database.Tables["param"].SetDataSource(param);
                        ReserInvoicingReport.ReportSource = _enquiryInvocingFormat;
                    }
                    
                    ReserInvoicingReport.RefreshReport();
                    ReserInvoicingReport.DisplayGroupTree = false;
                }
                else
                {
                    Response.Redirect("~/ReservationManagement");
                }

            }
            else
            {
                Response.Redirect("~/Login");
            }
        }
    }
}