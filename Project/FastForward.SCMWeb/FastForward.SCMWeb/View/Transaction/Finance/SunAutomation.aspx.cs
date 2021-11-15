using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.Financial;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Finance
{
    public partial class SunAutomation : BasePage
    {


        long jurnalNo = 0;
         private List<LocPurSun> editlist
         {
             get
             {
                 if (Session["LocPurSun"] != null)
                 {
                     return (List<LocPurSun>)Session["LocPurSun"];
                 }
                 else
                 {
                     return new List<LocPurSun>();
                 }
             }
             set { Session["LocPurSun"] = value; }
         }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtSDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                txtEDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                Session["_sunauto"] = null;
                //load sbu
                if (Session["UserCompanyCode"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }

                DataTable dt = CHNLSVC.Security.GetSBU_Company(Session["UserCompanyCode"].ToString(), "");
                // drpsbu.da
                drpsbu.DataSource = dt;
                drpsbu.DataTextField = "Code";// "srtp_desc";
                drpsbu.DataValueField = "Code";
                drpsbu.DataBind();
                drpsbu.SelectedItem.Text = "Select";
                Session["AlreadyRunSunUpload"] = null;
            }
            else
            {

            }
        }

        protected void Ddpaytype_SelectedIndexChanged(object sender, EventArgs e)
        { 
            try
             {
                 gvgrnlist.DataSource = null;
                 gvgrnlist.DataBind();
                if (Ddpaytype.SelectedValue == "LCLPC")
                {
                    DateTime fromdate = Convert.ToDateTime(txtSDate.Text.ToString());
                    DateTime todate = Convert.ToDateTime(txtEDate.Text.ToString());
                    DataTable drndata = CHNLSVC.Financial.SP_GetGRNDOC(fromdate, todate, Session["UserCompanyCode"].ToString());
                    gvgrnlist.DataSource = drndata;
                    gvgrnlist.DataBind();

                }
                else if (Ddpaytype.SelectedValue == "Invoice" || Ddpaytype.SelectedValue == "Reciept" || Ddpaytype.SelectedValue == "CRNT")
                {
                    //string type = Dddivtype.SelectedValue.ToString();
                    DataTable pcdata = new DataTable();

                    pcdata = CHNLSVC.Financial.GetSunPC("DEBT", Session["UserCompanyCode"].ToString());
                    gvpclist.DataSource = pcdata;
                    gvpclist.DataBind();

                }
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
            }
        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fromdate = Convert.ToDateTime(txtSDate.Text.ToString());
                DateTime todate = Convert.ToDateTime(txtEDate.Text.ToString());
                string company = Session["UserCompanyCode"].ToString();
                string adminteam = drpsbu.Text.ToString();
                string userid = Session["UserID"].ToString();
                Int32 _autoNo = 0;
                MasterAutoNumber _masterAutoNumber = new MasterAutoNumber();
                _masterAutoNumber.Aut_cate_cd = "SUN";
                _masterAutoNumber.Aut_cate_tp = "SUN";
                _masterAutoNumber.Aut_direction = 1;
                _masterAutoNumber.Aut_moduleid = "SUN";
                _masterAutoNumber.Aut_number = _autoNo;
                _masterAutoNumber.Aut_start_char = "SUN";
                _masterAutoNumber.Aut_modify_dt =Convert.ToDateTime( "01/jan/2018").Date;
                _masterAutoNumber.Aut_year =Convert.ToInt32( todate.Year.ToString().Substring(2)+ todate.Month.ToString());
                Session["_masterAutoNumber"] = _masterAutoNumber;

                MasterAutoNumber _masterAutoNumber2 = new MasterAutoNumber();
                _masterAutoNumber2.Aut_cate_cd = "SUNJ";
                _masterAutoNumber2.Aut_cate_tp = "SUNJ";
                _masterAutoNumber2.Aut_direction = 1;
                _masterAutoNumber2.Aut_moduleid = "SUNJ";
                _masterAutoNumber2.Aut_number = _autoNo;
                _masterAutoNumber2.Aut_start_char = "SUNJ";
                _masterAutoNumber2.Aut_year = 2020;
                _masterAutoNumber.Aut_modify_dt = Convert.ToDateTime("01/jan/2018").Date;
                Session["_masterAutoNumber2"] = _masterAutoNumber2;
                List<SunAuto> _sunauto = new List<SunAuto>();

                _autoNo = CHNLSVC.Financial.GetAutoNumber("SUN", 1, "SUN", "COM", company, _masterAutoNumber.Aut_modify_dt, _masterAutoNumber.Aut_year).Aut_number;
                #region Imports Schedule
                if (Ddpaytype.Text.ToString() == "IMPSDL")
                {
                    DataTable Dt = CHNLSVC.MsgPortal.ImportScheduleSun(fromdate, todate, company, adminteam, userid);
                    var query2 = from r in Dt.AsEnumerable()
                                 group r by r.Field<string>("si_no") into groupedTable
                                 select new
                                 {
                                     si_no = groupedTable.Key,
                                     lc = groupedTable.Max(s => s.Field<string>("lc")),
                                     si_date = groupedTable.Max(s => s.Field<DateTime>("si_date")),
                                     bank_bill = groupedTable.Sum(s => s.Field<decimal>("bank_bill") * s.Field<decimal>("GRN_QTY")),
                                     freight = groupedTable.Sum(s => s.Field<decimal>("freight") * s.Field<decimal>("GRN_QTY")),
                                     insuarence = groupedTable.Sum(s => s.Field<decimal>("insuarence") * s.Field<decimal>("GRN_QTY")),
                                     other = groupedTable.Sum(s => s.Field<decimal>("other") * s.Field<decimal>("GRN_QTY")),
                                     other_ex = groupedTable.Sum(s => s.Field<decimal>("other_ex") * s.Field<decimal>("GRN_QTY") / s.Field<decimal>("QTY")),
                                     CID = groupedTable.Sum(s => s.Field<decimal>("CID") * s.Field<decimal>("GRN_QTY") / s.Field<decimal>("QTY")),
                                     VAT = groupedTable.Sum(s => s.Field<decimal>("VAT") * s.Field<decimal>("GRN_QTY") / s.Field<decimal>("QTY")),
                                     EIC = groupedTable.Sum(s => s.Field<decimal>("EIC") * s.Field<decimal>("GRN_QTY") / s.Field<decimal>("QTY")),
                                     PAL = groupedTable.Sum(s => s.Field<decimal>("PAL") * s.Field<decimal>("GRN_QTY") / s.Field<decimal>("QTY")),
                                     XID_ = groupedTable.Sum(s => s.Field<decimal>("XID_") * s.Field<decimal>("GRN_QTY") / s.Field<decimal>("QTY")),
                                     NBT = groupedTable.Sum(s => s.Field<decimal>("NBT") * s.Field<decimal>("GRN_QTY") / s.Field<decimal>("QTY")),
                                     DELIVERY_TERM = groupedTable.Max(s => s.Field<string>("DELIVERY_TERM")),
                                     GRN_QTY = groupedTable.Max(s => s.Field<decimal>("GRN_QTY")),
                                     SUR = groupedTable.Sum(s => s.Field<decimal>("SUR") * s.Field<decimal>("GRN_QTY") / s.Field<decimal>("QTY")),
                                     SRL = groupedTable.Sum(s => s.Field<decimal>("SRL") * s.Field<decimal>("GRN_QTY") / s.Field<decimal>("QTY")),
                                     RIDL = groupedTable.Sum(s => s.Field<decimal>("RIDL") * s.Field<decimal>("GRN_QTY") / s.Field<decimal>("QTY")),
                                     ESC_VAL = groupedTable.Sum(s => s.Field<decimal>("ESC_VAL") * s.Field<decimal>("GRN_QTY") / s.Field<decimal>("QTY")),
                                     ETD_DT = groupedTable.Max(s => s.Field<DateTime>("ETD_DT")),
                                     ETA_DT = groupedTable.Max(s => s.Field<DateTime>("ETA_DT")),

                                 };
                    DataTable importdata = ConvertToDataTable(query2);
                    int i = 0;
                    foreach (var dtin in importdata.Rows)
                    {
                        SunAuto _ob = new SunAuto();
                        bool isfoc = CHNLSVC.Financial.IsTOTFOC(importdata.Rows[i]["si_no"].ToString());
                        decimal total = 0;
                        string grnno = "";
                        string grnsbu = "";
                        grnno = CHNLSVC.Financial.GetGRNno(importdata.Rows[i]["si_no"].ToString());
                        grnsbu = CHNLSVC.Financial.GetGRNnoSBU(importdata.Rows[i]["si_no"].ToString());
                        DateTime grndate = DateTime.Now.Date;
                        grndate = CHNLSVC.Financial.GetGRNDate(importdata.Rows[i]["si_no"].ToString());
                        string DeliTerm = importdata.Rows[i]["DELIVERY_TERM"].ToString();
                        DateTime eta = Convert.ToDateTime(importdata.Rows[i]["ETA_DT"].ToString());
                        DateTime etd = Convert.ToDateTime(importdata.Rows[i]["ETD_DT"].ToString());

                        if( !isfoc)
                        {
                            importdata.Rows[i]["lc"] = CHNLSVC.Financial.GetFinRefNo(importdata.Rows[i]["si_no"].ToString());
                        }
                        if (importdata.Rows[i]["si_no"].ToString() == "ABLSI181139")
                        {

                        }
                        #region   Same Formular Imports Schedule

                        #region INVENTORY
                        if (grnsbu == "INVENTORY")
                        {

                            if (importdata.Rows[i]["DELIVERY_TERM"].ToString() == "FOB")
                            {
                                total = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["freight"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString())
                                    + (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()))
                                    + Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString());

                                #region duty freee
                                if (Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString());
                                    _ob.CommonCat = "Bank Bill";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    if (!isfoc || grnsbu == "INVENTORY") _ob.AccType = "DF";
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-BIL " + shipseq;
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["freight"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["freight"].ToString());
                                    _ob.CommonCat = "Freight";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    if (!isfoc || grnsbu == "INVENTORY") _ob.AccType = "DF";
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-FRE " + shipseq;
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString());
                                    _ob.CommonCat = "Insuarance";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    if (!isfoc || grnsbu == "INVENTORY") _ob.AccType = "DF";
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-INS " + shipseq;
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other"].ToString());
                                    _ob.CommonCat = "Other";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    if (!isfoc || grnsbu == "INVENTORY") _ob.AccType = "DF";
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-EXWRK " + shipseq;
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString());
                                    _ob.CommonCat = "Other Ex";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    _ob.AccType = "DF";
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-OTHR " + shipseq;
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion

                            }
                            else if (importdata.Rows[i]["DELIVERY_TERM"].ToString() == "CIF")
                            {
                                total =Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString())
                                   + (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()))
                                   + Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString());

                                #region duty freee
                                if (Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString());
                                    _ob.CommonCat = "Bank Bill";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    if (!isfoc || grnsbu == "INVENTORY") _ob.AccType = "DF";
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-BIL " + shipseq;
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other"].ToString());
                                    _ob.CommonCat = "Other";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    if (!isfoc || grnsbu == "INVENTORY") _ob.AccType = "DF";
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-EXWRK " + shipseq;
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString());
                                    _ob.CommonCat = "Other Ex";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    _ob.AccType = "DF";
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-OTHR " + shipseq;
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion
                            }
                            else if (importdata.Rows[i]["DELIVERY_TERM"].ToString() == "CPT")
                            {
                                total = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString())
        + Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString())
        + (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()) * Convert.ToDecimal(importdata.Rows[i]["GRN_QTY"].ToString()))
        + Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString());

                                #region duty freee
                                if (Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString());
                                    _ob.CommonCat = "Bank Bill";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    if (!isfoc || grnsbu == "INVENTORY") _ob.AccType = "DF";
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-BIL " + shipseq;
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString());
                                    _ob.CommonCat = "Insuarance";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    if (!isfoc || grnsbu == "INVENTORY") _ob.AccType = "DF";
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-INS " + shipseq;
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other"].ToString());
                                    _ob.CommonCat = "Other";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    if (!isfoc || grnsbu == "INVENTORY") _ob.AccType = "DF";
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-EXWRK " + shipseq;
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString());
                                    _ob.CommonCat = "Other Ex";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    _ob.AccType = "DF";
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-OTHR " + shipseq;
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion
                            }
                            else
                            {
                                total = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString())
                                   + Convert.ToDecimal(importdata.Rows[i]["freight"].ToString())
                                   + Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString())
                                   + (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()))
                                   + Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString());

                                #region duty freee
                                if (Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString());
                                    _ob.CommonCat = "Bank Bill";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                     _ob.AccType = "DF";
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-BIL " + shipseq;
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["freight"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["freight"].ToString());
                                    _ob.CommonCat = "Freight";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                     _ob.AccType = "DF";
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-FRE " + shipseq;
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString());
                                    _ob.CommonCat = "Insuarance";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                     _ob.AccType = "DF";
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-INS " + shipseq;
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other"].ToString());
                                    _ob.CommonCat = "Other";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    _ob.AccType = "DF";
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-EXWRK " + shipseq;
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString());
                                    _ob.CommonCat = "Other Ex";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    _ob.AccType = "DF";
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-OTHR " + shipseq;
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion
                            }
                        }
                        #endregion
                        else if (grnsbu == "CAD")
                        {
                            #region CAD 
                            if (importdata.Rows[i]["DELIVERY_TERM"].ToString() == "FOB")
                            {
                                total = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["freight"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString())
                                    + (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()))
                                    + Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["CID"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString());

                                #region duty paid1
                                if (Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString());
                                    _ob.CommonCat = "Bank Bill";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "BANK BILL";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["freight"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["freight"].ToString());
                                    _ob.CommonCat = "Freight";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "FREIGHT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString());
                                    _ob.CommonCat = "Insuarance";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "INSURANCE";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other"].ToString());
                                    _ob.CommonCat = "Other";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "OTHER";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString());
                                    _ob.CommonCat = "Other Ex";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "OTHER EX";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion

                                #region duty paid
                                //Duty Paid Side
                                if (Convert.ToDecimal(importdata.Rows[i]["CID"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["CID"].ToString());
                                    _ob.CommonCat = "CID";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHCID");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "CID";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString());
                                    _ob.CommonCat = "VAT";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHVAT");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "VAT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString());
                                    _ob.CommonCat = "EIC";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHEIC");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "EIC";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString());
                                    _ob.CommonCat = "PAL";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHPAL");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "PAL";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString());
                                    _ob.CommonCat = "XID_";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHXID");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "XID";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString());
                                    _ob.CommonCat = "NBT";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHNBT");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "NBT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString());
                                    _ob.CommonCat = "ESC";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHESC");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "ESC";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion

                                #region grn
                                //GRN DEBITs
                                if (_sunauto != null && _sunauto.Count > 0 && total > 0)
                                {
                                    var cnt = _sunauto.Where(a => a.Description == grnno).Count();
                                    if (cnt == 0)
                                    {
                                        _ob = new SunAuto();
                                        _ob.CommonVal = total;
                                        _ob.CommonCat = "GRN";
                                        _ob.Amount = total;
                                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                        _ob.TransactionDate = todate;// Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                        _ob.JournalType = "GEN";
                                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHECAD");
                                        _ob.AccountCode = _acc;
                                        _ob.DebtCrdt = "D";
                                        _ob.PC = "N/A";
                                        _ob.JournalSours = "SMS";
                                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                        //Transaction Reference
                                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                        //description
                                        _ob.Description = grnno;
                                        _ob.Actgrnno = grnno;
                                        _ob.GrnDate = grndate;
                                        _ob.DiliveryTerm = DeliTerm;
                                        _ob.ETA = eta;
                                        _ob.ETD = etd;
                                        _sunauto.Add(_ob);
                                    }
                                }
                                else
                                {
                                    if (total > 0)
                                    {
                                        _ob = new SunAuto();
                                        _ob.CommonVal = total;
                                        _ob.CommonCat = "GRN";
                                        _ob.Amount = total;
                                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                        _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                        _ob.JournalType = "GEN";
                                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHGRN");
                                        _ob.AccountCode = _acc;
                                        _ob.DebtCrdt = "D";
                                        _ob.PC = "N/A";
                                        _ob.JournalSours = "SMS";
                                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                        //Transaction Reference
                                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                        //description
                                        _ob.Description = grnno;
                                        _ob.Actgrnno = grnno;
                                        _ob.GrnDate = grndate;
                                        _ob.DiliveryTerm = DeliTerm;
                                        _ob.ETA = eta;
                                        _ob.ETD = etd;
                                        _sunauto.Add(_ob);
                                    }

                                }
                                #endregion
                            }
                            else if (importdata.Rows[i]["DELIVERY_TERM"].ToString() == "CIF")
                            {
                                total = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString())
                                   + (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()))
                                   + Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["CID"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString());
                                #region duty paid1
                                if (Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString());
                                    _ob.CommonCat = "Bank Bill";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "BANK BILL";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }

                                if (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other"].ToString());
                                    _ob.CommonCat = "Other";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "OTHER";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString());
                                    _ob.CommonCat = "Other Ex";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "OTHER EX";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion

                                #region duty paid
                                //Duty Paid Side
                                if (Convert.ToDecimal(importdata.Rows[i]["CID"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["CID"].ToString());
                                    _ob.CommonCat = "CID";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHCID");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "CID";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString());
                                    _ob.CommonCat = "VAT";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHVAT");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "VAT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString());
                                    _ob.CommonCat = "EIC";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHEIC");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "EIC";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString());
                                    _ob.CommonCat = "PAL";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHPAL");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "PAL";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString());
                                    _ob.CommonCat = "XID_";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHXID");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "XID";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString());
                                    _ob.CommonCat = "NBT";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHNBT");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "NBT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString());
                                    _ob.CommonCat = "ESC";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHESC");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "ESC";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion

                                #region grn
                                //GRN DEBITs
                                if (_sunauto != null && _sunauto.Count > 0 && total > 0)
                                {
                                    var cnt = _sunauto.Where(a => a.Description == grnno).Count();
                                    if (cnt == 0)
                                    {
                                        _ob = new SunAuto();
                                        _ob.CommonVal = total;
                                        _ob.CommonCat = "GRN";
                                        _ob.Amount = total;
                                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                        _ob.TransactionDate = todate;// Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                        _ob.JournalType = "GEN";
                                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHECAD");
                                        _ob.AccountCode = _acc;
                                        _ob.DebtCrdt = "D";
                                        _ob.PC = "N/A";
                                        _ob.JournalSours = "SMS";
                                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                        //Transaction Reference
                                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                        //description
                                        _ob.Description = grnno;
                                        _ob.Actgrnno = grnno;
                                        _ob.GrnDate = grndate;
                                        _ob.DiliveryTerm = DeliTerm;
                                        _ob.ETA = eta;
                                        _ob.ETD = etd;
                                        _sunauto.Add(_ob);
                                    }
                                }
                                else
                                {
                                    if (total > 0)
                                    {
                                        _ob = new SunAuto();
                                        _ob.CommonVal = total;
                                        _ob.CommonCat = "GRN";
                                        _ob.Amount = total;
                                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                        _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                        _ob.JournalType = "GEN";
                                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHGRN");
                                        _ob.AccountCode = _acc;
                                        _ob.DebtCrdt = "D";
                                        _ob.PC = "N/A";
                                        _ob.JournalSours = "SMS";
                                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                        //Transaction Reference
                                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                        //description
                                        _ob.Description = grnno;
                                        _ob.Actgrnno = grnno;
                                        _ob.GrnDate = grndate;
                                        _ob.DiliveryTerm = DeliTerm;
                                        _ob.ETA = eta;
                                        _ob.ETD = etd;
                                        _sunauto.Add(_ob);
                                    }

                                }
                                #endregion
                            }
                            else if (importdata.Rows[i]["DELIVERY_TERM"].ToString() == "CPT")
                            {
                                total = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString())
                                 + (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()))
                                 + Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["CID"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString());
                                #region duty paid1
                                if (Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString());
                                    _ob.CommonCat = "Bank Bill";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "BANK BILL";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString());
                                    _ob.CommonCat = "Insuarance";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "INSURANCE";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other"].ToString());
                                    _ob.CommonCat = "Other";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "OTHER";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString());
                                    _ob.CommonCat = "Other Ex";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "OTHER EX";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion

                                #region duty paid
                                //Duty Paid Side
                                if (Convert.ToDecimal(importdata.Rows[i]["CID"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["CID"].ToString());
                                    _ob.CommonCat = "CID";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHCID");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "CID";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString());
                                    _ob.CommonCat = "VAT";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHVAT");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "VAT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString());
                                    _ob.CommonCat = "EIC";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHEIC");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "EIC";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString());
                                    _ob.CommonCat = "PAL";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHPAL");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "PAL";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString());
                                    _ob.CommonCat = "XID_";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHXID");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "XID";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString());
                                    _ob.CommonCat = "NBT";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHNBT");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "NBT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString());
                                    _ob.CommonCat = "ESC";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHESC");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "ESC";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion

                                #region grn
                                //GRN DEBITs
                                if (_sunauto != null && _sunauto.Count > 0 && total > 0)
                                {
                                    var cnt = _sunauto.Where(a => a.Description == grnno).Count();
                                    if (cnt == 0)
                                    {
                                        _ob = new SunAuto();
                                        _ob.CommonVal = total;
                                        _ob.CommonCat = "GRN";
                                        _ob.Amount = total;
                                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                        _ob.TransactionDate = todate;// Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                        _ob.JournalType = "GEN";
                                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHECAD");
                                        _ob.AccountCode = _acc;
                                        _ob.DebtCrdt = "D";
                                        _ob.PC = "N/A";
                                        _ob.JournalSours = "SMS";
                                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                        //Transaction Reference
                                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                        //description
                                        _ob.Description = grnno;
                                        _ob.Actgrnno = grnno;
                                        _ob.GrnDate = grndate;
                                        _ob.DiliveryTerm = DeliTerm;
                                        _ob.ETA = eta;
                                        _ob.ETD = etd;
                                        _sunauto.Add(_ob);
                                    }
                                }
                                else
                                {
                                    if (total > 0)
                                    {
                                        _ob = new SunAuto();
                                        _ob.CommonVal = total;
                                        _ob.CommonCat = "GRN";
                                        _ob.Amount = total;
                                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                        _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                        _ob.JournalType = "GEN";
                                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHGRN");
                                        _ob.AccountCode = _acc;
                                        _ob.DebtCrdt = "D";
                                        _ob.PC = "N/A";
                                        _ob.JournalSours = "SMS";
                                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                        //Transaction Reference
                                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                        //description
                                        _ob.Description = grnno;
                                        _ob.Actgrnno = grnno;
                                        _ob.GrnDate = grndate;
                                        _ob.DiliveryTerm = DeliTerm;
                                        _ob.ETA = eta;
                                        _ob.ETD = etd;
                                        _sunauto.Add(_ob);
                                    }

                                }
                                #endregion
                            }
                            else
                            {
                                total = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString())
                                   + Convert.ToDecimal(importdata.Rows[i]["freight"].ToString())
                                   + Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString())
                                   + (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()))
                                   + Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString())
                                   + Convert.ToDecimal(importdata.Rows[i]["CID"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString());
                                #region duty paid1
                                if (Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString());
                                    _ob.CommonCat = "Bank Bill";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "BANK BILL";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["freight"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["freight"].ToString());
                                    _ob.CommonCat = "Freight";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "FREIGHT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString());
                                    _ob.CommonCat = "Insuarance";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "INSURANCE";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other"].ToString());
                                    _ob.CommonCat = "Other";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "OTHER";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString());
                                    _ob.CommonCat = "Other Ex";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "OTHER EX";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion

                                #region duty paid
                                //Duty Paid Side
                                if (Convert.ToDecimal(importdata.Rows[i]["CID"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["CID"].ToString());
                                    _ob.CommonCat = "CID";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHCID");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "CID";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString());
                                    _ob.CommonCat = "VAT";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHVAT");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "VAT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString());
                                    _ob.CommonCat = "EIC";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHEIC");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "EIC";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString());
                                    _ob.CommonCat = "PAL";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHPAL");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "PAL";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString());
                                    _ob.CommonCat = "XID_";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHXID");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "XID";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString());
                                    _ob.CommonCat = "NBT";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHNBT");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "NBT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString());
                                    _ob.CommonCat = "ESC";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHESC");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "ESC";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion

                                #region grn
                                //GRN DEBITs
                                if (_sunauto != null && _sunauto.Count > 0 && total > 0)
                                {
                                    var cnt = _sunauto.Where(a => a.Description == grnno).Count();
                                    if (cnt == 0)
                                    {
                                        _ob = new SunAuto();
                                        _ob.CommonVal = total;
                                        _ob.CommonCat = "GRN";
                                        _ob.Amount = total;
                                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                        _ob.TransactionDate = todate;// Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                        _ob.JournalType = "GEN";
                                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHECAD");
                                        _ob.AccountCode = _acc;
                                        _ob.DebtCrdt = "D";
                                        _ob.PC = "N/A";
                                        _ob.JournalSours = "SMS";
                                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                        //Transaction Reference
                                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                        //description
                                        _ob.Description = grnno;
                                        _ob.Actgrnno = grnno;
                                        _ob.GrnDate = grndate;
                                        _ob.DiliveryTerm = DeliTerm;
                                        _ob.ETA = eta;
                                        _ob.ETD = etd;
                                        _sunauto.Add(_ob);
                                    }
                                }
                                else
                                {
                                    if (total > 0)
                                    {
                                        _ob = new SunAuto();
                                        _ob.CommonVal = total;
                                        _ob.CommonCat = "GRN";
                                        _ob.Amount = total;
                                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                        _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                        _ob.JournalType = "GEN";
                                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHGRN");
                                        _ob.AccountCode = _acc;
                                        _ob.DebtCrdt = "D";
                                        _ob.PC = "N/A";
                                        _ob.JournalSours = "SMS";
                                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                        //Transaction Reference
                                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                        //description
                                        _ob.Description = grnno;
                                        _ob.Actgrnno = grnno;
                                        _ob.GrnDate = grndate;
                                        _ob.DiliveryTerm = DeliTerm;
                                        _ob.ETA = eta;
                                        _ob.ETD = etd;
                                        _sunauto.Add(_ob);
                                    }

                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            #region except inv
                            if (importdata.Rows[i]["DELIVERY_TERM"].ToString() == "FOB")
                            {
                                total =Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["freight"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString())
                                    + (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()))
                                    + Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["CID"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString());

                                #region duty paid1
                                if (Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString());
                                    _ob.CommonCat = "Bank Bill";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "BANK BILL";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["freight"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["freight"].ToString());
                                    _ob.CommonCat = "Freight";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "FREIGHT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString());
                                    _ob.CommonCat = "Insuarance";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "INSURANCE";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other"].ToString());
                                    _ob.CommonCat = "Other";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "OTHER";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString());
                                    _ob.CommonCat = "Other Ex";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "OTHER EX";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion

                                #region duty paid
                                //Duty Paid Side
                                if (Convert.ToDecimal(importdata.Rows[i]["CID"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["CID"].ToString());
                                    _ob.CommonCat = "CID";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHCID");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "CID";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString());
                                    _ob.CommonCat = "VAT";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHVAT");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "VAT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString());
                                    _ob.CommonCat = "EIC";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHEIC");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "EIC";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString());
                                    _ob.CommonCat = "PAL";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHPAL");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "PAL";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString());
                                    _ob.CommonCat = "XID_";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHXID");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "XID";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString());
                                    _ob.CommonCat = "NBT";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHNBT");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "NBT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString());
                                    _ob.CommonCat = "ESC";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHESC");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "ESC";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion

                                #region grn
                                //GRN DEBITs
                                if (_sunauto != null && _sunauto.Count > 0 && total > 0)
                                {
                                    var cnt = _sunauto.Where(a => a.Description == grnno).Count();
                                    if (cnt == 0)
                                    {
                                        _ob = new SunAuto();
                                        _ob.CommonVal = total;
                                        _ob.CommonCat = "GRN";
                                        _ob.Amount = total;
                                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                        _ob.TransactionDate = todate;// Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                        _ob.JournalType = "GEN";
                                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHGRN");
                                        _ob.AccountCode = _acc;
                                        _ob.DebtCrdt = "D";
                                        _ob.PC = "N/A";
                                        _ob.JournalSours = "SMS";
                                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                        //Transaction Reference
                                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                        //description
                                        _ob.Description = grnno;
                                        _ob.GrnDate = grndate;
                                        _ob.DiliveryTerm = DeliTerm;
                                        _ob.ETA = eta;
                                        _ob.ETD = etd;
                                        _sunauto.Add(_ob);
                                    }
                                }
                                else
                                {
                                    if (total > 0)
                                    {
                                        _ob = new SunAuto();
                                        _ob.CommonVal = total;
                                        _ob.CommonCat = "GRN";
                                        _ob.Amount = total;
                                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                        _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                        _ob.JournalType = "GEN";
                                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHGRN");
                                        _ob.AccountCode = _acc;
                                        _ob.DebtCrdt = "D";
                                        _ob.PC = "N/A";
                                        _ob.JournalSours = "SMS";
                                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                        //Transaction Reference
                                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                        //description
                                        _ob.Description = grnno;
                                        _ob.GrnDate = grndate;
                                        _ob.DiliveryTerm = DeliTerm;
                                        _ob.ETA = eta;
                                        _ob.ETD = etd;
                                        _sunauto.Add(_ob);
                                    }

                                }
                                #endregion
                            }
                            else if (importdata.Rows[i]["DELIVERY_TERM"].ToString() == "CIF")
                            {
                                total = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString())
                                   + (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()))
                                   + Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["CID"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString())
                                    + Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString());
                                #region duty paid1
                                if (Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString());
                                    _ob.CommonCat = "Bank Bill";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "BANK BILL";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }

                                if (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other"].ToString());
                                    _ob.CommonCat = "Other";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "OTHER";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString());
                                    _ob.CommonCat = "Other Ex";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "OTHER EX";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion

                                #region duty paid
                                //Duty Paid Side
                                if (Convert.ToDecimal(importdata.Rows[i]["CID"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["CID"].ToString());
                                    _ob.CommonCat = "CID";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHCID");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "CID";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString());
                                    _ob.CommonCat = "VAT";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHVAT");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "VAT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString());
                                    _ob.CommonCat = "EIC";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHEIC");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "EIC";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString());
                                    _ob.CommonCat = "PAL";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHPAL");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "PAL";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString());
                                    _ob.CommonCat = "XID_";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHXID");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "XID";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString());
                                    _ob.CommonCat = "NBT";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHNBT");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "NBT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString());
                                    _ob.CommonCat = "ESC";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHESC");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "ESC";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion

                                #region grn
                                //GRN DEBITs
                                if (_sunauto != null && _sunauto.Count > 0 && total > 0)
                                {
                                    var cnt = _sunauto.Where(a => a.Description == grnno).Count();
                                    if (cnt == 0)
                                    {
                                        _ob = new SunAuto();
                                        _ob.CommonVal = total;
                                        _ob.CommonCat = "GRN";
                                        _ob.Amount = total;
                                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                        _ob.TransactionDate = todate;// Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                        _ob.JournalType = "GEN";
                                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHGRN");
                                        _ob.AccountCode = _acc;
                                        _ob.DebtCrdt = "D";
                                        _ob.PC = "N/A";
                                        _ob.JournalSours = "SMS";
                                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                        //Transaction Reference
                                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                        //description
                                        _ob.Description = grnno;
                                        _ob.Actgrnno = grnno;
                                        _ob.GrnDate = grndate;
                                        _ob.DiliveryTerm = DeliTerm;
                                        _ob.ETA = eta;
                                        _ob.ETD = etd;
                                        _sunauto.Add(_ob);
                                    }
                                }
                                else
                                {
                                    if (total > 0)
                                    {
                                        _ob = new SunAuto();
                                        _ob.CommonVal = total;
                                        _ob.CommonCat = "GRN";
                                        _ob.Amount = total;
                                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                        _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                        _ob.JournalType = "GEN";
                                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHGRN");
                                        _ob.AccountCode = _acc;
                                        _ob.DebtCrdt = "D";
                                        _ob.PC = "N/A";
                                        _ob.JournalSours = "SMS";
                                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                        //Transaction Reference
                                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                        //description
                                        _ob.Description = grnno;
                                        _ob.Actgrnno = grnno;
                                        _ob.GrnDate = grndate;
                                        _ob.DiliveryTerm = DeliTerm;
                                        _ob.ETA = eta;
                                        _ob.ETD = etd;
                                        _sunauto.Add(_ob);
                                    }

                                }
                                #endregion
                            }
                            else if (importdata.Rows[i]["DELIVERY_TERM"].ToString() == "CPT")
                            {
                                total = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString())
                                 + (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()))
                                 + Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["CID"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString());
                                #region duty paid1
                                if (Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString());
                                    _ob.CommonCat = "Bank Bill";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "BANK BILL";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString());
                                    _ob.CommonCat = "Insuarance";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "INSURANCE";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other"].ToString());
                                    _ob.CommonCat = "Other";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "OTHER";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString());
                                    _ob.CommonCat = "Other Ex";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "OTHER EX";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion

                                #region duty paid
                                //Duty Paid Side
                                if (Convert.ToDecimal(importdata.Rows[i]["CID"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["CID"].ToString());
                                    _ob.CommonCat = "CID";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHCID");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "CID";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString());
                                    _ob.CommonCat = "VAT";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHVAT");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "VAT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString());
                                    _ob.CommonCat = "EIC";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHEIC");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "EIC";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString());
                                    _ob.CommonCat = "PAL";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHPAL");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "PAL";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString());
                                    _ob.CommonCat = "XID_";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHXID");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "XID";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString());
                                    _ob.CommonCat = "NBT";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHNBT");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "NBT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString());
                                    _ob.CommonCat = "ESC";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHESC");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "ESC";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion

                                #region grn
                                //GRN DEBITs
                                if (_sunauto != null && _sunauto.Count > 0 && total > 0)
                                {
                                    var cnt = _sunauto.Where(a => a.Description == grnno).Count();
                                    if (cnt == 0)
                                    {
                                        _ob = new SunAuto();
                                        _ob.CommonVal = total;
                                        _ob.CommonCat = "GRN";
                                        _ob.Amount = total;
                                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                        _ob.TransactionDate = todate;// Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                        _ob.JournalType = "GEN";
                                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHGRN");
                                        _ob.AccountCode = _acc;
                                        _ob.DebtCrdt = "D";
                                        _ob.PC = "N/A";
                                        _ob.JournalSours = "SMS";
                                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                        //Transaction Reference
                                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                        //description
                                        _ob.Description = grnno;
                                        _ob.Actgrnno = grnno;
                                        _ob.GrnDate = grndate;
                                        _ob.DiliveryTerm = DeliTerm;
                                        _ob.ETA = eta;
                                        _ob.ETD = etd;
                                        _sunauto.Add(_ob);
                                    }
                                }
                                else
                                {
                                    if (total > 0)
                                    {
                                        _ob = new SunAuto();
                                        _ob.CommonVal = total;
                                        _ob.CommonCat = "GRN";
                                        _ob.Amount = total;
                                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                        _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                        _ob.JournalType = "GEN";
                                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHGRN");
                                        _ob.AccountCode = _acc;
                                        _ob.DebtCrdt = "D";
                                        _ob.PC = "N/A";
                                        _ob.JournalSours = "SMS";
                                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                        //Transaction Reference
                                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                        //description
                                        _ob.Description = grnno;
                                        _ob.Actgrnno = grnno;
                                        _ob.GrnDate = grndate;
                                        _ob.DiliveryTerm = DeliTerm;
                                        _ob.ETA = eta;
                                        _ob.ETD = etd;
                                        _sunauto.Add(_ob);
                                    }

                                }
                                #endregion
                            }
                            else
                            {
                                total = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString())
                                   + Convert.ToDecimal(importdata.Rows[i]["freight"].ToString())
                                   + Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString())
                                   + (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()))
                                   + Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString())
                                   + Convert.ToDecimal(importdata.Rows[i]["CID"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString())
                                 + Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString());
                                #region duty paid1
                                if (Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString());
                                    _ob.CommonCat = "Bank Bill";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "BANK BILL";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["freight"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["freight"].ToString());
                                    _ob.CommonCat = "Freight";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "FREIGHT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString());
                                    _ob.CommonCat = "Insuarance";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "INSURANCE";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other"].ToString());
                                    _ob.CommonCat = "Other";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "OTHER";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString());
                                    _ob.CommonCat = "Other Ex";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "OTHER EX";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion

                                #region duty paid
                                //Duty Paid Side
                                if (Convert.ToDecimal(importdata.Rows[i]["CID"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["CID"].ToString());
                                    _ob.CommonCat = "CID";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHCID");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "CID";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString());
                                    _ob.CommonCat = "VAT";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHVAT");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "VAT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString());
                                    _ob.CommonCat = "EIC";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHEIC");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "EIC";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString());
                                    _ob.CommonCat = "PAL";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHPAL");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "PAL";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString());
                                    _ob.CommonCat = "XID_";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHXID");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "XID";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString());
                                    _ob.CommonCat = "NBT";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHNBT");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "NBT";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                if (Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString()) > 0)
                                {
                                    _ob = new SunAuto();
                                    _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["ESC_VAL"].ToString());
                                    _ob.CommonCat = "ESC";
                                    _ob.Amount = total;
                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                    _ob.JournalType = "GEN";
                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHESC");
                                    _ob.AccountCode = _acc;
                                    _ob.DebtCrdt = "C";
                                    _ob.PC = "N/A";
                                    _ob.JournalSours = "SMS";
                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();

                                    //Transaction Reference
                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                    //description
                                    string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
                                    _ob.Description = "ESC";
                                    _ob.Actgrnno = grnno;
                                    _ob.GrnDate = grndate;
                                    _ob.DiliveryTerm = DeliTerm;
                                    _ob.ETA = eta;
                                    _ob.ETD = etd;
                                    _sunauto.Add(_ob);
                                }
                                #endregion

                                #region grn
                                //GRN DEBITs
                                if (_sunauto != null && _sunauto.Count > 0 && total > 0)
                                {
                                    var cnt = _sunauto.Where(a => a.Description == grnno).Count();
                                    if (cnt == 0)
                                    {
                                        _ob = new SunAuto();
                                        _ob.CommonVal = total;
                                        _ob.CommonCat = "GRN";
                                        _ob.Amount = total;
                                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                        _ob.TransactionDate = todate;// Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                        _ob.JournalType = "GEN";
                                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHGRN");
                                        _ob.AccountCode = _acc;
                                        _ob.DebtCrdt = "D";
                                        _ob.PC = "N/A";
                                        _ob.JournalSours = "SMS";
                                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                        //Transaction Reference
                                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                        //description
                                        _ob.Description = grnno;
                                        _ob.Actgrnno = grnno;
                                        _ob.GrnDate = grndate;
                                        _ob.DiliveryTerm = DeliTerm;
                                        _ob.ETA = eta;
                                        _ob.ETD = etd;
                                        _sunauto.Add(_ob);
                                    }
                                }
                                else
                                {
                                    if (total > 0)
                                    {
                                        _ob = new SunAuto();
                                        _ob.CommonVal = total;
                                        _ob.CommonCat = "GRN";
                                        _ob.Amount = total;
                                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
                                        _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
                                        _ob.JournalType = "GEN";
                                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHGRN");
                                        _ob.AccountCode = _acc;
                                        _ob.DebtCrdt = "D";
                                        _ob.PC = "N/A";
                                        _ob.JournalSours = "SMS";
                                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
                                        //Transaction Reference
                                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                                        //description
                                        _ob.Description = grnno;
                                        _ob.Actgrnno = grnno;
                                        _ob.GrnDate = grndate;
                                        _ob.DiliveryTerm = DeliTerm;
                                        _ob.ETA = eta;
                                        _ob.ETD = etd;
                                        _sunauto.Add(_ob);
                                    }

                                }
                                #endregion
                            }
                            #endregion
                        }


                        #endregion

                        #region Old

                        //  DateTime _grndate = CHNLSVC.Financial.GetGRNDate(importdata.Rows[i]["si_date"].ToString());
                        //importdata.Rows[i]["si_date"] = _grndate;



                      

    //                    if (isfoc)
    //                    {
    //                        total = _ob.Amount = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString()) + Convert.ToDecimal(importdata.Rows[i]["freight"].ToString()) + Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString()) + Convert.ToDecimal(importdata.Rows[i]["other"].ToString())
    //+ Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString()) + Convert.ToDecimal(importdata.Rows[i]["CID"].ToString()) + Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString()) +
    //Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString()) + Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString()) + Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString()) + Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString());


    //                        if (grnsbu != "INVENTORY")
    //                        {
    //                            #region grn
    //                            //GRN DEBITs
    //                            if (_sunauto != null && _sunauto.Count > 0 && total > 0)
    //                            {
    //                                var cnt = _sunauto.Where(a => a.Description == grnno).Count();
    //                                if (cnt == 0)
    //                                {
    //                                    _ob = new SunAuto();
    //                                    _ob.CommonVal = total;
    //                                    _ob.CommonCat = "GRN";
    //                                    _ob.Amount = total;
    //                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
    //                                    _ob.TransactionDate = todate;// Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
    //                                    _ob.JournalType = "GEN";
    //                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
    //                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHGRN");
    //                                    _ob.AccountCode = _acc;
    //                                    _ob.DebtCrdt = "D";
    //                                    _ob.PC = "N/A";
    //                                    _ob.JournalSours = "SMS";
    //                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
    //                                    //Transaction Reference
    //                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
    //                                    //description
    //                                    _ob.Description = grnno;
    //                                    _sunauto.Add(_ob);
    //                                }
    //                            }
    //                            else
    //                            {
    //                                if (total > 0)
    //                                {
    //                                    _ob = new SunAuto();
    //                                    _ob.CommonVal = total;
    //                                    _ob.CommonCat = "GRN";
    //                                    _ob.Amount = total;
    //                                    _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
    //                                    _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
    //                                    _ob.JournalType = "GEN";
    //                                    _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
    //                                    string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHGRN");
    //                                    _ob.AccountCode = _acc;
    //                                    _ob.DebtCrdt = "D";
    //                                    _ob.PC = "N/A";
    //                                    _ob.JournalSours = "SMS";
    //                                    _ob.Docno = importdata.Rows[i]["si_no"].ToString();
    //                                    //Transaction Reference
    //                                    _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
    //                                    //description
    //                                    _ob.Description = grnno;
    //                                    _sunauto.Add(_ob);
    //                                }

    //                            }
    //                            #endregion
    //                        }


    //                    }
    //                    else
    //                    {
    //                        total = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString()) + Convert.ToDecimal(importdata.Rows[i]["freight"].ToString()) + Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString()) + Convert.ToDecimal(importdata.Rows[i]["other"].ToString()) + Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString());
    //                    }
    //                    #region duty freee
    //                    if (Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString()) > 0)
    //                    {
    //                        _ob = new SunAuto();
    //                        _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["bank_bill"].ToString());
    //                        _ob.CommonCat = "Bank Bill";
    //                        _ob.Amount = total;
    //                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
    //                        _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
    //                        _ob.JournalType = "GEN";
    //                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
    //                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
    //                        _ob.AccountCode = _acc;
    //                        _ob.DebtCrdt = "C";
    //                        _ob.PC = "N/A";
    //                        _ob.JournalSours = "SMS";
    //                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
    //                        if (!isfoc || grnsbu == "INVENTORY") _ob.AccType = "DF";
    //                        //Transaction Reference
    //                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
    //                        //description
    //                        string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
    //                        _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-BIL " + shipseq;
    //                        _sunauto.Add(_ob);
    //                    }
    //                    if (Convert.ToDecimal(importdata.Rows[i]["freight"].ToString()) > 0)
    //                    {
    //                        _ob = new SunAuto();
    //                        _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["freight"].ToString());
    //                        _ob.CommonCat = "Freight";
    //                        _ob.Amount = total;
    //                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
    //                        _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
    //                        _ob.JournalType = "GEN";
    //                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
    //                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
    //                        _ob.AccountCode = _acc;
    //                        _ob.DebtCrdt = "C";
    //                        _ob.PC = "N/A";
    //                        _ob.JournalSours = "SMS";
    //                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
    //                        if (!isfoc || grnsbu == "INVENTORY") _ob.AccType = "DF";
    //                        //Transaction Reference
    //                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
    //                        //description
    //                        string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
    //                        _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-FRE " + shipseq;
    //                        _sunauto.Add(_ob);
    //                    }
    //                    if (Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString()) > 0)
    //                    {
    //                        _ob = new SunAuto();
    //                        _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["insuarence"].ToString());
    //                        _ob.CommonCat = "Insuarance";
    //                        _ob.Amount = total;
    //                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
    //                        _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
    //                        _ob.JournalType = "GEN";
    //                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
    //                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
    //                        _ob.AccountCode = _acc;
    //                        _ob.DebtCrdt = "C";
    //                        _ob.PC = "N/A";
    //                        _ob.JournalSours = "SMS";
    //                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
    //                        if (!isfoc || grnsbu == "INVENTORY") _ob.AccType = "DF";
    //                        //Transaction Reference
    //                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
    //                        //description
    //                        string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
    //                        _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-INS " + shipseq;
    //                        _sunauto.Add(_ob);
    //                    }
    //                    if (Convert.ToDecimal(importdata.Rows[i]["other"].ToString()) > 0)
    //                    {
    //                        _ob = new SunAuto();
    //                        _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other"].ToString());
    //                        _ob.CommonCat = "Other";
    //                        _ob.Amount = total;
    //                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
    //                        _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
    //                        _ob.JournalType = "GEN";
    //                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
    //                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
    //                        _ob.AccountCode = _acc;
    //                        _ob.DebtCrdt = "C";
    //                        _ob.PC = "N/A";
    //                        _ob.JournalSours = "SMS";
    //                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
    //                        if (!isfoc || grnsbu == "INVENTORY") _ob.AccType = "DF";
    //                        //Transaction Reference
    //                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
    //                        //description
    //                        string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
    //                        _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-EXWRK " + shipseq;
    //                        _sunauto.Add(_ob);
    //                    }
    //                    #endregion
    //                    if (Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString()) > 0)
    //                    {
    //                        _ob = new SunAuto();
    //                        _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["other_ex"].ToString());
    //                        _ob.CommonCat = "Other Ex";
    //                        _ob.Amount = total;
    //                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
    //                        _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
    //                        _ob.JournalType = "GEN";
    //                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
    //                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCH");
    //                        _ob.AccountCode = _acc;
    //                        _ob.DebtCrdt = "C";
    //                        _ob.PC = "N/A";
    //                        _ob.JournalSours = "SMS";
    //                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();
    //                        if (!isfoc || grnsbu == "INVENTORY") _ob.AccType = "DF";
    //                        //Transaction Reference
    //                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
    //                        //description
    //                        string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
    //                        _ob.Description = "IMP PUR " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy") + "-OTHR " + shipseq;
    //                        _sunauto.Add(_ob);
    //                    }
    //                    #region duty paid
    //                    //Duty Paid Side
    //                    if (Convert.ToDecimal(importdata.Rows[i]["CID"].ToString()) > 0 && isfoc)
    //                    {
    //                        _ob = new SunAuto();
    //                        _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["CID"].ToString());
    //                        _ob.CommonCat = "CID";
    //                        _ob.Amount = total;
    //                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
    //                        _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
    //                        _ob.JournalType = "GEN";
    //                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
    //                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHCID");
    //                        _ob.AccountCode = _acc;
    //                        _ob.DebtCrdt = "C";
    //                        _ob.PC = "N/A";
    //                        _ob.JournalSours = "SMS";
    //                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();

    //                        //Transaction Reference
    //                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
    //                        //description
    //                        string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
    //                        _ob.Description = "CID";
    //                        _sunauto.Add(_ob);
    //                    }
    //                    if (Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString()) > 0 && isfoc)
    //                    {
    //                        _ob = new SunAuto();
    //                        _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["VAT"].ToString());
    //                        _ob.CommonCat = "VAT";
    //                        _ob.Amount = total;
    //                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
    //                        _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
    //                        _ob.JournalType = "GEN";
    //                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
    //                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHVAT");
    //                        _ob.AccountCode = _acc;
    //                        _ob.DebtCrdt = "C";
    //                        _ob.PC = "N/A";
    //                        _ob.JournalSours = "SMS";
    //                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();

    //                        //Transaction Reference
    //                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
    //                        //description
    //                        string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
    //                        _ob.Description = "VAT";
    //                        _sunauto.Add(_ob);
    //                    }
    //                    if (Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString()) > 0 && isfoc)
    //                    {
    //                        _ob = new SunAuto();
    //                        _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["EIC"].ToString());
    //                        _ob.CommonCat = "EIC";
    //                        _ob.Amount = total;
    //                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
    //                        _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
    //                        _ob.JournalType = "GEN";
    //                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
    //                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHEIC");
    //                        _ob.AccountCode = _acc;
    //                        _ob.DebtCrdt = "C";
    //                        _ob.PC = "N/A";
    //                        _ob.JournalSours = "SMS";
    //                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();

    //                        //Transaction Reference
    //                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
    //                        //description
    //                        string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
    //                        _ob.Description = "EIC";
    //                        _sunauto.Add(_ob);
    //                    }
    //                    if (Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString()) > 0 && isfoc)
    //                    {
    //                        _ob = new SunAuto();
    //                        _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["PAL"].ToString());
    //                        _ob.CommonCat = "PAL";
    //                        _ob.Amount = total;
    //                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
    //                        _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
    //                        _ob.JournalType = "GEN";
    //                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
    //                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHPAL");
    //                        _ob.AccountCode = _acc;
    //                        _ob.DebtCrdt = "C";
    //                        _ob.PC = "N/A";
    //                        _ob.JournalSours = "SMS";
    //                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();

    //                        //Transaction Reference
    //                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
    //                        //description
    //                        string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
    //                        _ob.Description = "PAL";
    //                        _sunauto.Add(_ob);
    //                    }
    //                    if (Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString()) > 0 && isfoc)
    //                    {
    //                        _ob = new SunAuto();
    //                        _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["XID_"].ToString());
    //                        _ob.CommonCat = "XID_";
    //                        _ob.Amount = total;
    //                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
    //                        _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
    //                        _ob.JournalType = "GEN";
    //                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
    //                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHXID");
    //                        _ob.AccountCode = _acc;
    //                        _ob.DebtCrdt = "C";
    //                        _ob.PC = "N/A";
    //                        _ob.JournalSours = "SMS";
    //                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();

    //                        //Transaction Reference
    //                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
    //                        //description
    //                        string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
    //                        _ob.Description = "XID";
    //                        _sunauto.Add(_ob);
    //                    }
    //                    if (Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString()) > 0 && isfoc)
    //                    {
    //                        _ob = new SunAuto();
    //                        _ob.CommonVal = Convert.ToDecimal(importdata.Rows[i]["NBT"].ToString());
    //                        _ob.CommonCat = "NBT";
    //                        _ob.Amount = total;
    //                        _ob.LC_VehicleNo = importdata.Rows[i]["lc"].ToString();
    //                        _ob.TransactionDate = todate;//  Convert.ToDateTime(importdata.Rows[i]["si_date"].ToString()).Date;
    //                        _ob.JournalType = "GEN";
    //                        _ob.Period = _ob.TransactionDate.AddMonths(-3).ToString("M/yyyy");
    //                        string _acc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHNBT");
    //                        _ob.AccountCode = _acc;
    //                        _ob.DebtCrdt = "C";
    //                        _ob.PC = "N/A";
    //                        _ob.JournalSours = "SMS";
    //                        _ob.Docno = importdata.Rows[i]["si_no"].ToString();

    //                        //Transaction Reference
    //                        _ob.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
    //                        //description
    //                        string shipseq = CHNLSVC.Financial.GetShipSeqNo(importdata.Rows[i]["si_no"].ToString());
    //                        _ob.Description = "NBT";
    //                        _sunauto.Add(_ob);
    //                    }
    //                    #endregion

                        #endregion

                        i++;
                    }

                    #region totduty free

                    decimal _dftotal = _sunauto.Where(a => a.AccType == "DF").Sum(y => y.CommonVal);

                    SunAuto _ob1 = new SunAuto();
                    _ob1.CommonVal = _dftotal;
                    _ob1.CommonCat = "TOT";
                    _ob1.Amount = _dftotal;
                    _ob1.LC_VehicleNo = "N/A";
                    _ob1.TransactionDate = todate;//  DateTime.Now.Date;
                    _ob1.JournalType = "GEN";
                    _ob1.Period = _ob1.TransactionDate.AddMonths(-3).ToString("M/yyyy");
                    string _acc2 = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "IMPORTSCHTOT");
                    _ob1.AccountCode = _acc2;
                    _ob1.DebtCrdt = "D";
                    _ob1.PC = "N/A";
                    _ob1.JournalSours = "SMS";
                    _ob1.Docno = "N/A";

                    //Transaction Reference
                    _ob1.TransactionRef = "J/" + todate.Date.ToString("yy") + "/" + todate.Date.Month.ToString() + "/" + _autoNo.ToString();
                    //description
                    _ob1.Description = "IMP PURCHASE " + todate.Date.ToString("MMM") + "'" + todate.Date.ToString("yy");
                    _sunauto.Add(_ob1);
                    #endregion
                    Session["_sunauto"] = _sunauto;
                    grdsundata.DataSource = _sunauto;
                    grdsundata.DataBind();

                }
                #endregion
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
                return;
            }
        }

        protected void btnclear_Click(object sender, EventArgs e)
        {

        }
        public DataTable ConvertToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();


            // column names
            PropertyInfo[] oProps = null;


            if (varlist == null) return dtReturn;


            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;


                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }


                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }


                DataRow dr = dtReturn.NewRow();


                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }


                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }


        protected void btnexport_Click(object sender, EventArgs e)
        {
            List<SunAuto> _list = Session["_sunauto"] as List<SunAuto>;
            DataTable dtTemp = GlobalMethod.ToDataTable(_list);
            dtTemp.TableName = "dt";
            string out1 = "";
            string path = CHNLSVC.MsgPortal.ExportExcel2007(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), dtTemp, out out1);
            _copytoLocal(path);
            string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".xlsx','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
        }
        private void _copytoLocal(string _filePath)
        {
            string filenamenew = Session["UserID"].ToString();
            string name = _filePath;
            FileInfo file = new FileInfo(name);
            if (file.Exists)
            {
                string targetFileName = Server.MapPath("~\\Temp\\") + filenamenew + ".xlsx";
                System.IO.File.Copy(@"" + _filePath, "C:/Download_excel/" + filenamenew + ".xlsx", true);
                System.IO.File.Copy(@"" + _filePath, targetFileName, true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + "This file does not exist." + "')", true);
                return;
            }
        }
        
        [System.Web.Services.WebMethod]
        public void Sunupload(string upType,Boolean postCurrentPrd)
        {
            //return;
            chkPstCurPrd.Checked = false;
            MdlSunUpload.Hide();
            MasterAutoNumber _masterAutoNumber = Session["_masterAutoNumber"] as MasterAutoNumber;
            MasterAutoNumber _masterAutoNumber2 = Session["_masterAutoNumber2"] as MasterAutoNumber;
            SunUpload objSunuo = new SunUpload();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "disale", "<script>disale();</script>", false);

            DataTable pcdata = new DataTable();
            List<string> PCList = new List<String>();
            List<string> GRNlist = new List<string>();
            int effect = 0;
            string error = "";

            if (upType == "Invoice")
            {
                //pcdata = CHNLSVC.Financial.GetSunPC("DEBT", Session["UserCompanyCode"].ToString());
                //foreach (DataRow item in pcdata.Rows)
                //{
                //    PCList.Add(item["sn_pccd"].ToString());
                //}

                foreach (GridViewRow row in gvpclist.Rows)
                {
                    CheckBox chkselect = (CheckBox)row.FindControl("chkselect");
                    if (chkselect != null && chkselect.Checked)
                    {
                        //if (pctxt != "")
                        //{
                        //    pctxt = pctxt + ",";
                        //}
                        //Label lbpccd = (Label)row.FindControl("lbpccd");
                        //string com_cd = lbpccd.Text;
                        //pctxt = pctxt + com_cd;

                        //PCList.Add(row.FindControl("lbpccd").ToString());
                        Label lbpccd = (Label)row.FindControl("lbpccd");
                        string com_cd = lbpccd.Text;
                        PCList.Add(com_cd);
                    }
                }
                if (PCList.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Select PC to Update!" + "');", true);
                    return;
                }

                //objSunuo.Ddpaytype.Text
                objSunuo.postCurrPrd = postCurrentPrd;
                objSunuo.GetFileAddDirect(Ddpaytype.SelectedValue, Convert.ToDateTime(txtSDate.Text.ToString()), Convert.ToDateTime(txtEDate.Text.ToString()), PCList);

                if (objSunuo._sunall.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "No Records to Update!" + "');", true);
                }
                else if (validateSUNAcc(objSunuo, upType) == true) 
                {
                    effect = CHNLSVC.Financial.SaveSunEntries(objSunuo._sunall, Session["UserCompanyCode"].ToString(), Convert.ToDateTime(txtEDate.Text.ToString()), _masterAutoNumber, _masterAutoNumber2, out error, out jurnalNo);
                }

                

            }

            else if (upType == "Reciept")
            {

                //pcdata = CHNLSVC.Financial.GetSunPC("DEBT", Session["UserCompanyCode"].ToString());
                //foreach (DataRow item in pcdata.Rows)
                //{
                //    PCList.Add(item["sn_pccd"].ToString());
                //}

                foreach (GridViewRow row in gvpclist.Rows)
                {
                    CheckBox chkselect = (CheckBox)row.FindControl("chkselect");
                    if (chkselect != null && chkselect.Checked)
                    {
                        //if (pctxt != "")
                        //{
                        //    pctxt = pctxt + ",";
                        //}
                        //Label lbpccd = (Label)row.FindControl("lbpccd");
                        //string com_cd = lbpccd.Text;
                        //pctxt = pctxt + com_cd;
                        //PCList.Add(row.FindControl("lbpccd").ToString());
                        Label lbpccd = (Label)row.FindControl("lbpccd");
                        string com_cd = lbpccd.Text;
                        PCList.Add(com_cd);
                    }
                }

                if (PCList.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Select PC to Update!" + "');", true);
                    return;
                }
                //objSunuo.Ddpaytype.Text
                objSunuo.postCurrPrd = postCurrentPrd;
                objSunuo.GetFileAddDirect(Ddpaytype.SelectedValue, Convert.ToDateTime(txtSDate.Text.ToString()), Convert.ToDateTime(txtEDate.Text.ToString()), PCList);
                if (objSunuo._sunall.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "No Records to Update!" + "');", true);
                }
                else if (validateSUNAcc(objSunuo, upType) == true)
                {
                    effect = CHNLSVC.Financial.SaveSunEntries(objSunuo._sunall, Session["UserCompanyCode"].ToString(), Convert.ToDateTime(txtEDate.Text.ToString()), _masterAutoNumber, _masterAutoNumber2, out error, out jurnalNo);
                    //upload receipt
                    foreach (var item in objSunuo._sunall.Select(s => s.treference).Distinct())        
                   
                    //foreach (var item in objSunuo._sunall)
                    {
                        effect = CHNLSVC.Financial.Allocate_SUNReceipt(Session["UserCompanyCode"].ToString(), item);                      
                    }
                }
            }

            else if (upType == "LCLPC")
            {
                //objSunuo.Ddpaytype.Text
                foreach (GridViewRow row in gvgrnlist.Rows)
                {
                    CheckBox chkselectgrn = (CheckBox)row.FindControl("chkselectgrn");
                    if (chkselectgrn != null && chkselectgrn.Checked)
                    {
                        //if (grntxt != "")
                        //{
                        //    grntxt = grntxt + ",";
                        //}
                        Label lbgrnno = (Label)row.FindControl("lbgrnno");
                        string com_cd = lbgrnno.Text;
                        //grntxt = grntxt + com_cd;
                        GRNlist.Add(com_cd);

                    }
                }

                if (GRNlist.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Select GRN to Update!" + "');", true);
                    return;
                }

                objSunuo.GetFileAddDirect(Ddpaytype.SelectedValue, Convert.ToDateTime(txtSDate.Text.ToString()), Convert.ToDateTime(txtEDate.Text.ToString()), GRNlist);
                if (objSunuo._sunall.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "No Records to Update!" + "');", true);
                }else
                    if (validateSUNAcc(objSunuo, upType) == true)
                {
                    effect = CHNLSVC.Financial.SaveSunEntries(objSunuo._sunall, Session["UserCompanyCode"].ToString(), Convert.ToDateTime(txtEDate.Text.ToString()), _masterAutoNumber, _masterAutoNumber2, out error, out jurnalNo);
                }

              
            

            }

            else if (upType == "CRNT")
            {
                //pcdata = CHNLSVC.Financial.GetSunPC("DEBT", Session["UserCompanyCode"].ToString());
                //foreach (DataRow item in pcdata.Rows)
                //{
                //    PCList.Add(item["sn_pccd"].ToString());
                //}

                foreach (GridViewRow row in gvpclist.Rows)
                {
                    CheckBox chkselect = (CheckBox)row.FindControl("chkselect");
                    if (chkselect != null && chkselect.Checked)
                    {
                        //if (pctxt != "")
                        //{
                        //    pctxt = pctxt + ",";
                        //}
                        //Label lbpccd = (Label)row.FindControl("lbpccd");
                        //string com_cd = lbpccd.Text;
                        //pctxt = pctxt + com_cd;
                        //PCList.Add(row.FindControl("lbpccd").ToString());
                        Label lbpccd = (Label)row.FindControl("lbpccd");
                        string com_cd = lbpccd.Text;
                        PCList.Add(com_cd);
                    }
                }

                if (PCList.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Select PC to Update!" + "');", true);
                    return;
                }

                //objSunuo.Ddpaytype.Text
                objSunuo.GetFileAddDirect(Ddpaytype.SelectedValue, Convert.ToDateTime(txtSDate.Text.ToString()), Convert.ToDateTime(txtEDate.Text.ToString()), PCList);
                if (objSunuo._sunall.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "No Records to Update!" + "');", true);
                }
                else if (validateSUNAcc(objSunuo, upType) == true)
                {
                    effect = CHNLSVC.Financial.SaveSunEntries(objSunuo._sunall, Session["UserCompanyCode"].ToString(), Convert.ToDateTime(txtEDate.Text.ToString()), _masterAutoNumber, _masterAutoNumber2, out error, out jurnalNo);
                }
            }
          
            if (effect >= 0)
            {
                if (upType == "LCLPC")
                { ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + "Successfully Uploaded!.Junrnal # " + jurnalNo + "');", true); }
                else
                { ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + "Successfully Uploaded!" + "');", true); }

                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + error + "');", true);
                return;
            }
        }
        
        protected void btnupload_Click(object sender, EventArgs e)
        {
            //try
            //{

                #region Imports Schedule
                if (Ddpaytype.SelectedValue == "IMPSDL")
                {
                    List<SunAccountall> _sunall = new List<SunAccountall>();
                    List<SunAuto> _list = Session["_sunauto"] as List<SunAuto>;

                    //check DR-CR Validation
                    decimal dr = _list.Where(a => a.DebtCrdt == "D").Sum(z => z.CommonVal);
                    decimal cr = _list.Where(a => a.DebtCrdt == "C").Sum(z => z.CommonVal);

                    if (dr != cr)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "CR/DR Value Deference" + "');", true);
                        return;
                    }
                    MasterAutoNumber _masterAutoNumber = Session["_masterAutoNumber"] as MasterAutoNumber;
                    MasterAutoNumber _masterAutoNumber2 = Session["_masterAutoNumber2"] as MasterAutoNumber;
                    Int32 _norlenth = 0;
                    string mainsp15 = "               ";
                    foreach (var _lst in _list)
                    {
                        //Check Is SUN Debtor Code
                        List<SunAccountBusEntity> cuslist = new List<SunAccountBusEntity>();
                        cuslist = CHNLSVC.CustService.GetSunAccountDetails(_lst.AccountCode.Trim(), Session["UserCompanyCode"].ToString().Trim());

                        if (cuslist != null && cuslist.Count > 0)
                        {

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Setup Account Code SUN:" + _lst.AccountCode + "');", true);
                            return;
                        }

                        SunAccountall ob = new SunAccountall();

                        //Account Code Sun Length-15
                        if (_lst.AccountCode.Length > 15)
                        {
                            _lst.AccountCode = _lst.AccountCode.Substring(0, 15);
                        }
                        else
                        {
                            string Space15 = "               ";
                            _norlenth = Space15.Length - _lst.AccountCode.Length;
                            _lst.AccountCode = _lst.AccountCode + Space15.Substring(0, _norlenth);

                        }
                        ob.accnt_code = _lst.AccountCode;


                        //Transaction Date and Period 
                        ob.period = Convert.ToInt32(_lst.TransactionDate.AddMonths(-3).ToString("yyyy" + "0" + "MM"));
                        ob.trans_date = Convert.ToInt32(_lst.TransactionDate.ToString("yyyyMMdd"));
                        ob.amount = Convert.ToDecimal(_lst.CommonVal);
                        ob.d_c = _lst.DebtCrdt;
                        ob.allocation = " ";
                        ob.jrnal_type = "GEN";
                        ob.jrnal_srce = "SMS";


                        //Reference
                        if (_lst.TransactionRef.Length > 15)
                        {
                            _lst.TransactionRef = _lst.TransactionRef.Substring(0, 15);
                        }
                        else
                        {
                            string Space15 = "               ";
                            _norlenth = Space15.Length - _lst.TransactionRef.Length;
                            _lst.TransactionRef = _lst.TransactionRef + Space15.Substring(0, _norlenth);
                        }
                        ob.treference = _lst.TransactionRef;

                        //Description
                        if (_lst.Description.Length > 25)
                        {
                            _lst.Description = _lst.Description.Substring(0, 25);
                        }
                        else
                        {
                            string Space25 = "                         ";
                            _norlenth = Space25.Length - _lst.Description.Length;
                            _lst.Description = _lst.Description + Space25.Substring(0, _norlenth);
                        }
                        ob.descriptn = _lst.Description;
                        ob.entry_date = Convert.ToInt32(DateTime.Now.Date.ToString("yyyyMMdd"));
                        ob.entry_prd = Convert.ToInt32(DateTime.Now.Date.AddMonths(-3).ToString("yyyy" + "0" + "MM"));
                        ob.due_date = 0;
                        ob.alloc_ref = 0;
                        ob.alloc_date = 0;
                        ob.alloc_period = 0;
                        ob.asset_ind = " ";
                        ob.asset_code = "          ";
                        ob.asset_sub = "     ";
                        ob.conv_code = "     ";
                        ob.conv_rate = 0;
                        ob.other_amt = 0;
                        ob.other_dp = "2";//
                        ob.cleardown = "00000";
                        ob.reversal = " ";
                        ob.loss_gain = " ";
                        ob.rough_flag = " ";
                        ob.in_use_flag = " ";
                        ob.anal_t0 = mainsp15;
                        ob.anal_t1 = mainsp15;

                        //SetLC NO
                        //Description
                        if (_lst.LC_VehicleNo.Length > 15)
                        {
                            _lst.LC_VehicleNo = _lst.LC_VehicleNo.Substring(0, 15);
                        }
                        else
                        {

                            _norlenth = mainsp15.Length - _lst.LC_VehicleNo.Length;
                            _lst.LC_VehicleNo = _lst.LC_VehicleNo + mainsp15.Substring(0, _norlenth);
                        }
                        ob.anal_t2 = _lst.LC_VehicleNo;
                        ob.anal_t3 = mainsp15;
                        ob.anal_t4 = mainsp15;
                        ob.anal_t5 = mainsp15;
                        ob.anal_t6 = mainsp15;
                        ob.anal_t7 = mainsp15;
                        ob.anal_t8 = mainsp15;
                        ob.anal_t9 = mainsp15;
                        ob.posting_date = Convert.ToInt32(DateTime.Now.Date.ToString("yyyyMMdd"));
                        ob.alloc_in_progress = " ";
                        ob.hold_ref = 0;
                        ob.hold_op_id = "   ";
                        ob.last_change_user_id = "EMS";
                        ob.last_change_date = 0;
                        ob.originator_id = "   ";
                        ob.ActGRNNo = _lst.Actgrnno;
                        _sunall.Add(ob);

                    }
                    string error = "";
                    int effect = CHNLSVC.Financial.SaveSunEntries(_sunall, Session["UserCompanyCode"].ToString(), Convert.ToDateTime(txtEDate.Text.ToString()), _masterAutoNumber,_masterAutoNumber2, out error, out jurnalNo);
                    //save
                    if (effect >= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + "Successfully Uploaded!" + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + error + "');", true);
                        return;
                    }
                }
                #endregion
                #region  Credit Card
                else if (Ddpaytype.SelectedValue == "CRCDACK")
                {
                    DateTime fromdate = Convert.ToDateTime(txtSDate.Text.ToString());
                    DateTime todate = Convert.ToDateTime(txtEDate.Text.ToString());
                    MasterAutoNumber _masterAutoNumber2 = new MasterAutoNumber();
                    _masterAutoNumber2.Aut_cate_cd = "SUNJ";
                    _masterAutoNumber2.Aut_cate_tp = "SUNJ";
                    _masterAutoNumber2.Aut_direction = 1;
                    _masterAutoNumber2.Aut_modify_dt = todate.Date;
                    _masterAutoNumber2.Aut_moduleid = "SUNJ";
                    _masterAutoNumber2.Aut_number = 0;
                    _masterAutoNumber2.Aut_start_char = "SUNJ";
                    _masterAutoNumber2.Aut_year = 2020;
                    Session["_masterAutoNumber2"] = _masterAutoNumber2;
                    List<SAT_ADJ_CRCD> _adjlist = CHNLSVC.Financial.GetCreditCardSunData(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "CRDRCVACKW", Convert.ToDateTime(txtSDate.Text), Convert.ToDateTime(txtEDate.Text));
                    List<SunAccountall> _sunall = new List<SunAccountall>();
                    string mainsp15 = "               ";
                    foreach (var _list in _adjlist)
                    {

                        #region DEBT
                        SunAccountall ob = new SunAccountall();
                        ob.doc = _list.staj_ref;
                        ob.Com = Session["UserCompanyCode"].ToString();
                        //Account Code Sun Length-15
                        if (_list.staj_acc_dbt.Length > 15)
                        {
                            _list.staj_acc_dbt = _list.staj_acc_dbt.Substring(0, 15);
                        }
                        else
                        {
                            string Space15 = "               ";
                            Int32 _norlenth = Space15.Length - _list.staj_acc_dbt.Length;
                            _list.staj_acc_dbt = _list.staj_acc_dbt + Space15.Substring(0, _norlenth);

                        }
                        ob.accnt_code = _list.staj_acc_dbt;


                        //Transaction Date and Period 
                        ob.period = Convert.ToInt32(_list.staj_dt.AddMonths(-3).ToString("yyyy" + "0" + "MM"));
                        ob.trans_date = Convert.ToInt32(_list.staj_dt.ToString("yyyyMMdd"));
                        ob.amount = Convert.ToDecimal(_list.staj_amt);
                        ob.d_c = "D";
                        ob.allocation = " ";
                        ob.jrnal_type = "GEN";
                        ob.jrnal_srce = "SMS";


                        //Reference
                        if (_list.staj_ref.Length > 15)
                        {
                            _list.staj_ref = _list.staj_ref.Substring(0, 15);
                        }
                        else
                        {
                            string Space15 = "               ";
                            Int32 _norlenth = Space15.Length - _list.staj_ref.Length;
                            _list.staj_ref = _list.staj_ref + Space15.Substring(0, _norlenth);
                        }
                        ob.treference = _list.staj_ref;

                        //Description
                        if (_list.staj_rmk.Length > 25)
                        {
                            _list.staj_rmk = _list.staj_rmk.Substring(0, 25);
                        }
                        else
                        {
                            string Space25 = "                         ";
                            Int32 _norlenth = Space25.Length - _list.staj_rmk.Length;
                            _list.staj_rmk = _list.staj_rmk + Space25.Substring(0, _norlenth);
                        }
                        ob.descriptn = _list.staj_state_date.Date.ToString("dd/MMM/yyyy");
                        ob.entry_date = Convert.ToInt32(DateTime.Now.Date.ToString("yyyyMMdd"));
                        ob.entry_prd = Convert.ToInt32(DateTime.Now.Date.AddMonths(-3).ToString("yyyy" + "0" + "MM"));
                        ob.due_date = 0;
                        ob.alloc_ref = 0;
                        ob.alloc_date = 0;
                        ob.alloc_period = 0;
                        ob.asset_ind = " ";
                        ob.asset_code = "          ";
                        ob.asset_sub = "     ";
                        ob.conv_code = "     ";
                        ob.conv_rate = 0;
                        ob.other_amt = 0;
                        ob.other_dp = "2";//
                        ob.cleardown = "00000";
                        ob.reversal = " ";
                        ob.loss_gain = " ";
                        ob.rough_flag = " ";
                        ob.in_use_flag = " ";
                        ob.anal_t0 = mainsp15;
                        ob.anal_t1 = mainsp15;

                        ob.anal_t2 = "N/A            ";
                        ob.anal_t3 = mainsp15;
                        ob.anal_t4 = mainsp15;
                        ob.anal_t5 = mainsp15;
                        ob.anal_t6 = mainsp15;
                        ob.anal_t7 = mainsp15;
                        ob.anal_t8 = mainsp15;
                        ob.anal_t9 = mainsp15;
                        ob.posting_date = Convert.ToInt32(DateTime.Now.Date.ToString("yyyyMMdd"));
                        ob.alloc_in_progress = " ";
                        ob.hold_ref = 0;
                        ob.hold_op_id = "   ";
                        ob.last_change_user_id = "EMS";
                        ob.last_change_date = 0;
                        ob.originator_id = "   ";

                        _sunall.Add(ob);
                        #endregion
                        #region CRED
                        ob = new SunAccountall();
                        ob.doc = _list.staj_ref;
                        ob.Com = Session["UserCompanyCode"].ToString();
                        //Account Code Sun Length-15
                        if (_list.staj_acc_crd.Length > 15)
                        {
                            _list.staj_acc_crd = _list.staj_acc_crd.Substring(0, 15);
                        }
                        else
                        {
                            string Space15 = "               ";
                            Int32 _norlenth = Space15.Length - _list.staj_acc_crd.Length;
                            _list.staj_acc_crd = _list.staj_acc_crd + Space15.Substring(0, _norlenth);

                        }
                        ob.accnt_code = _list.staj_acc_crd;


                        //Transaction Date and Period 
                        ob.period = Convert.ToInt32(_list.staj_dt.AddMonths(-3).ToString("yyyy" + "0" + "MM"));
                        ob.trans_date = Convert.ToInt32(_list.staj_dt.ToString("yyyyMMdd"));
                        ob.amount = Convert.ToDecimal(_list.staj_amt);
                        ob.d_c = "C";
                        ob.allocation = " ";
                        ob.jrnal_type = "GEN";
                        ob.jrnal_srce = "SMS";


                        //Reference
                        if (_list.staj_ref.Length > 15)
                        {
                            _list.staj_ref = _list.staj_ref.Substring(0, 15);
                        }
                        else
                        {
                            string Space15 = "               ";
                            Int32 _norlenth = Space15.Length - _list.staj_ref.Length;
                            _list.staj_ref = _list.staj_ref + Space15.Substring(0, _norlenth);
                        }
                        ob.treference = _list.staj_ref;

                        //Description
                        if (_list.staj_rmk.Length > 25)
                        {
                            _list.staj_rmk = _list.staj_rmk.Substring(0, 25);
                        }
                        else
                        {
                            string Space25 = "                         ";
                            Int32 _norlenth = Space25.Length - _list.staj_rmk.Length;
                            _list.staj_rmk = _list.staj_rmk + Space25.Substring(0, _norlenth);
                        }
                        ob.descriptn = _list.staj_state_date.Date.ToString("dd/MMM/yyyy");
                        ob.entry_date = Convert.ToInt32(DateTime.Now.Date.ToString("yyyyMMdd"));
                        ob.entry_prd = Convert.ToInt32(DateTime.Now.Date.AddMonths(-3).ToString("yyyy" + "0" + "MM"));
                        ob.due_date = 0;
                        ob.alloc_ref = 0;
                        ob.alloc_date = 0;
                        ob.alloc_period = 0;
                        ob.asset_ind = " ";
                        ob.asset_code = "          ";
                        ob.asset_sub = "     ";
                        ob.conv_code = "     ";
                        ob.conv_rate = 0;
                        ob.other_amt = 0;
                        ob.other_dp = "2";//
                        ob.cleardown = "00000";
                        ob.reversal = " ";
                        ob.loss_gain = " ";
                        ob.rough_flag = " ";
                        ob.in_use_flag = " ";
                        ob.anal_t0 = mainsp15;
                        ob.anal_t1 = mainsp15;

                        ob.anal_t2 = "N/A            ";
                        ob.anal_t3 = mainsp15;
                        ob.anal_t4 = mainsp15;
                        ob.anal_t5 = mainsp15;
                        ob.anal_t6 = mainsp15;
                        ob.anal_t7 = mainsp15;
                        ob.anal_t8 = mainsp15;
                        ob.anal_t9 = mainsp15;
                        ob.posting_date = Convert.ToInt32(DateTime.Now.Date.ToString("yyyyMMdd"));
                        ob.alloc_in_progress = " ";
                        ob.hold_ref = 0;
                        ob.hold_op_id = "   ";
                        ob.last_change_user_id = "EMS";
                        ob.last_change_date = 0;
                        ob.originator_id = "   ";

                        _sunall.Add(ob);
                        #endregion
                     
                    }
                    string error = "";
                    int effect = CHNLSVC.Financial.SaveSunEntries(_sunall, Session["UserCompanyCode"].ToString(), Convert.ToDateTime(txtEDate.Text.ToString()), null, _masterAutoNumber2, out error, out jurnalNo);
                    //save
                    if (effect >= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + "Successfully Uploaded!" + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + error + "');", true);
                        return;
                    }
                }
                #endregion
                #region  Invoice Receipt LocalPur Credit Note
                else if (Ddpaytype.SelectedValue == "Invoice" || Ddpaytype.SelectedValue == "Reciept" || Ddpaytype.SelectedValue == "LCLPC" || Ddpaytype.SelectedValue == "CRNT")
                {

                    //From date Back Date and Not in current month
                    //if (CHNLSVC.Financial.SUNPeriodclose(Session["UserCompanyCode"].ToString(), "SUNDB1", Convert.ToDateTime(txtSDate.Text.ToString())))
                    //{
                    //    //Session["AlreadyRunSunUpload"] = false;
                    //    //Label19.Text = "Sun period closed for Transaction Date " + txtSDate.Text.ToString();
                    //    //Label20.Text = "Press ‘YES’ to upload " + Ddpaytype.SelectedValue + " details into current month.";
                    //    //Label21.Text = "Press ‘NO’ to abort upload.";
                    //    // MdlSunUpload.Show();
                    //    //btnPrdChngNo.Enabled = true;
                    //    //btnPrdChngYes.Enabled = true;
                    //    //return;

                    //    // ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg","$(document).ready( function ConfPrint() { var selectedvalueOrd = confirm(""Do you want to print ?""); if (selectedvalueOrd) { return true; } else { return false;});", true);
                    //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "VoteJsFunc", "alert('Hey!You are legible to vote!')", true);                      
                     
                    //    ///ScriptManager.RegisterStartupScript(this, typeof(Page), "confirm", "<script>confirmation();</script>", false);
                    //  //  Sunupload(Ddpaytype.SelectedValue, false);
                    //  //  string AAA = Request.Form["hdnValue"].ToString();

                    //   if (chkPstCurPrd.Checked == false) 
                    //   {

                    //       //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "SUN period closed.\\n pls use post current period option!" + "');", true); 
                    //       ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('SUN period closed.pls use post current period option');", true); 
                    //   }
                    //   else 
                    //   {
                    //       Sunupload(Ddpaytype.SelectedValue, true);
                    //   }
                    //}

                    //else
                    //{
                    //    Sunupload(Ddpaytype.SelectedValue, false);
                    //}

                    Sunupload(Ddpaytype.SelectedValue, false);
         
                }
                #endregion              


        }

        protected bool validateSUNAcc(SunUpload objSunuo,string UType)
        {
            string invalidAcc = "";
            bool invldAccfnd = false;         
            //var Acc = objSunuo._sunall.Select(s => s.accnt_code).Distinct();

            foreach (var accNo in objSunuo._sunall.Select(s => s.accnt_code).Distinct())
            {
                if (CHNLSVC.Financial.validateSUNACC(Session["UserCompanyCode"].ToString(), "SUNDB1", accNo) == false)
                {
                    invldAccfnd = true;
                    if (invalidAcc == "")
                    {
                        invalidAcc = accNo;
                    }
                    else
                    {
                        invalidAcc = invalidAcc + "," + accNo;
                    }
                }
            }

            if (invldAccfnd)
            {
                foreach (var invoiceNo in objSunuo._sunall.Select(s => s.treference).Distinct())
                {
                    if (UType == "Invoice")
                    {
                        CHNLSVC.Financial.UPDATE_INV_HDRENGLOG(invoiceNo.Trim(), 0, Session["UserCompanyCode"].ToString());
                    }
                    else if (UType == "Reciept")
                    {
                        CHNLSVC.Financial.UPDATE_RECIEPT_HDRENGLOG(invoiceNo.Trim(), 0, Session["UserCompanyCode"].ToString());
                    }
                    else if (UType == "LCLPC")
                    {
                        CHNLSVC.Financial.UPDATE_LOCPCH_HDRENGLOG(invoiceNo.Trim(),0, Session["UserCompanyCode"].ToString());
                    }
                    else if (UType == "CRNT")
                    {
                        CHNLSVC.Financial.UPDATE_INV_HDRENGLOG(invoiceNo.Trim(), 0, Session["UserCompanyCode"].ToString());
                    }

                }
               ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Setup Account Code SUN:" + invalidAcc + "');", true);
                return false;
            }
            return true;
        }
        protected void btnsavelpdes_Click(object sender, EventArgs e)
        {
            try
            {
                LocPurSun editlistob = new LocPurSun();
                GridViewRow drSelect = (sender as LinkButton).NamingContainer as GridViewRow;
                Label orderno = drSelect.FindControl("lbgrnno") as Label;
                TextBox descrip = drSelect.FindControl("txtlpsundes") as TextBox;
                TextBox period = drSelect.FindControl("txtlpsundate") as TextBox;
                CheckBox chkselect = drSelect.FindControl("chkselectgrn") as CheckBox;

                List<LocPurSun> editlist1 = editlist;
                editlistob.itb_doc_no = orderno.Text.ToString();
                editlistob.invoiceno = descrip.Text.ToString();
                editlistob.ith_doc_date = Convert.ToDateTime(period.Text.ToString());

                //CHECK ALREDY ADD AND UPDATE
                Int32 count = editlist.Where(a => a.itb_doc_no == editlistob.itb_doc_no).Count();
                if (count > 0)
                {
                    editlist.Where(w => w.itb_doc_no == orderno.Text.ToString()).ToList().ForEach(i => i.mbe_name = descrip.Text.ToString());
                    editlist.Where(w => w.itb_doc_no == orderno.Text.ToString()).ToList().ForEach(i => i.ith_doc_date = editlistob.ith_doc_date);
                }
                else
                {
                    editlist1.Add(editlistob);
                }
                editlist = editlist1;
                descrip.Enabled = false;
                period.Enabled = false;
                chkselect.Checked = true;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
            }
        }

        protected void btneditlpdes_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow drSelect = (sender as LinkButton).NamingContainer as GridViewRow;
                TextBox descrip = drSelect.FindControl("txtlpsundes") as TextBox;
                TextBox period = drSelect.FindControl("txtlpsundate") as TextBox;
                CheckBox chkselect = drSelect.FindControl("chkselectgrn") as CheckBox;
                descrip.Enabled = true;
                period.Enabled = true;
                chkselect.Checked = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
            }
        }

        protected void btnPrdChngYes_Click(object sender, EventArgs e)
        {
            MdlSunUpload.Hide();
            //bool AlreadyRunSunUpload=(Session["AlreadyRunSunUpload"]!=null) ? (bool)Session["AlreadyRunSunUpload"] : true;
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "pls wait ......" + "');", true);
            //if (AlreadyRunSunUpload == false)
            //{
               
                Session["AlreadyRunSunUpload"] = true;
                Sunupload(Ddpaytype.SelectedValue, true);
            //}
            //else
            //{
            //    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScriptXXX", "showStickyNoticeToast('" + "Already Uploading" + "');", true);
            //}
          }

        protected void btnPrdChngNo_Click(object sender, EventArgs e)
        {
            //btnPrdChngNo.Enabled = false;
            //btnPrdChngYes.Enabled = false;
        }

        protected void chkPstCurPrd_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Ddpaytype.SelectedValue == "LCLPC")
                {
                    foreach (GridViewRow row in gvgrnlist.Rows)
                    {
                        CheckBox chkselect = (CheckBox)row.FindControl("chkselectgrn");
                        if (chkall.Checked)
                        {
                            chkselect.Checked = true;

                        }
                        else
                        {
                            chkselect.Checked = false;
                        }
                    }
                }
                else
                {
                    foreach (GridViewRow row in gvpclist.Rows)
                    {
                        CheckBox chkselect = (CheckBox)row.FindControl("chkselect");
                        if (chkall.Checked)
                        {
                            chkselect.Checked = true;

                        }
                        else
                        {
                            chkselect.Checked = false;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
            }
        }

        
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
            //    return;
            //}
        //}
    }

  

}