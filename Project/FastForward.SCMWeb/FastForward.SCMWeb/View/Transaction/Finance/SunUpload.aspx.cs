using FF.BusinessObjects;
using FF.BusinessObjects.Financial;
using FF.BusinessObjects.ToursNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Finance
{
    public partial class SunUpload : BasePage
    {

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
        public List<SunAccountall> _sunall = new List<SunAccountall>();
        public bool postCurrPrd = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtSDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                txtEDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");

            }
            else
            {

            }
        }


        protected void btnupload_Click(object sender, EventArgs e)
        {
            string sdate = txtSDate.Text.ToString();
            string edate = txtEDate.Text.ToString();

            DateTime temp;

            if (Regex.IsMatch(txtSDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$") == false)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Enter Valid " + "Start" + " Date " + "');", true);
                return;
            }

            if (Regex.IsMatch(txtEDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$") == false)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Enter Valid " + "End" + " Date " + "');", true);
                return;
            }


            GetFile(Convert.ToDateTime(sdate), Convert.ToDateTime(edate));
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + "Successfully Uploaded Path: " + Session["path"].ToString() + "');", true);
            List<LocPurSun> editlist = new List<LocPurSun>();
            gvgrnlist.DataSource = null;
            gvgrnlist.DataBind();
        }

        protected void btnclear_Click(object sender, EventArgs e)
        {

        }

        private string PrintSunLine(string AccCode, string Ammount, string AmmountType, DateTime trdate, string refNo, string cusname, string pc, string seq, string jnaltype, string execcode)
        {
            try
            {
                string space10 = "          ";
                string ammnotes18 = "000000000000000000";

                string space5 = "     ";
                string space2 = "  ";
                string space1 = " ";
                string space7 = "       ";
                DateTime summerdate = trdate;
                summerdate = summerdate.AddMonths(-3);
                string jrnum = "0000000";
                string jrlinenum = "00000";
                string allocmak = " ";
                string jrtype = jnaltype;
                //jnaltype
                if (jrtype.Length > 5)
                {
                    jrtype.Substring(0, 5);
                }
                else
                {
                    jrtype = jrtype + space5.Substring(0, space5.Length - jrtype.Length);
                }



                string jrscource = "SCM  ";
                string newrefNo;
                string space25 = "                         ";
                string newcusname;
                string otherdatenote8 = "00000000";
                string paymentallocnote10 = "0000000000";
                // string pppp = "00/00/0000 00:00:00 AM";
                //DateTime paymentallocdate8 = Convert.ToDateTime(pppp);
                string paymentallocdate8 = "00000000";
                string paymentallocdate7 = "0000000";
                string converrate = "000000000000000000";
                string otherammount = "000000000000000000";
                string oienter = "   ";
                string oipost = "SCM";
                string oilast = "   ";
                string nextperdrev = " ";
                string linktext = " ";
                //Addtional space
                string additinalsp2 = "  ";
                ///////////
                string space15 = "               ";
                string pcnew;
                string annlcd1 = space15;
                string annlcd2 = space15;
                string seqnew;
                string annlcd5 = space15;
                string annlcd6 = space15;
                string annlcd7 = execcode;
                string ann7new;
                string annlcd8 = space15;
                string annlcd9 = space15;
                string blankfinal = space15 + space15 + space15 + space15 + space15 + space15 + space15 + space7;

                ////////
                int sp10lenth = space10.Length;
                int spdef = sp10lenth - AccCode.Length;


                string acccdnew;
                if (spdef >= 0)
                {
                    acccdnew = AccCode + space10.Substring(0, spdef);
                }
                else
                {
                    acccdnew = AccCode.Substring(0, 10);
                }
                ////////////
                string[] words = Ammount.Split('.');
                string Ammountnew = words[0] + words[1];

                int ammnolenth = ammnotes18.Length;
                int ammlenth = Ammountnew.Length;
                int def = ammnolenth - ammlenth;
                string ammountnew;
                if (def >= 0)
                {
                    ammountnew = ammnotes18.Substring(0, def) + Ammountnew;
                }
                else
                {
                    ammountnew = Ammountnew.Substring(0, 18);
                }

                if (refNo.Length > 15)
                {
                    int sunrefmaxle = refNo.Length;

                    //newrefNo = refNo.Substring(0, 5) + refNo.Substring(refNo.Length - 5);
                    newrefNo = refNo.Substring(sunrefmaxle - 15);

                }
                else
                {
                    int defref = 15 - refNo.Length;
                    newrefNo = refNo + space15.Substring(0, defref);

                }

                int cusdef = space25.Length - cusname.Length;
                if (cusdef >= 0)
                {
                    newcusname = cusname + space25.Substring(0, cusdef);
                }
                else
                {
                    newcusname = cusname.Substring(0, 25);
                }
                int pcdef = space15.Length - pc.Length;
                if (pcdef >= 0)
                {
                    pcnew = pc + space15.Substring(0, pcdef);
                }
                else
                {
                    pcnew = pc.Substring(0, 15);
                }

                //chenged Vat Cat Z
                //seq = "Z";
                int seqdef = space15.Length - seq.Length;
                if (seqdef >= 0)
                {
                    seqnew = seq + space15.Substring(0, seqdef);
                }
                else
                {
                    seqnew = seq.Substring(0, 15);
                }

                int annl7def = space15.Length - annlcd7.Length;
                if (annl7def >= 0)
                {
                    ann7new = annlcd7 + space15.Substring(0, annl7def);
                }
                else
                {
                    ann7new = annlcd7.Substring(0, 15);
                }
                string line = acccdnew +
                    space5 +
                    summerdate.ToString("yyyy" + "0" + "MM") + trdate.ToString("yyyyMMdd") +
                    space2 +
                    "L" + jrnum + jrlinenum +
                    space2 +
                    ammountnew + AmmountType +
                    allocmak +
                    jrtype +
                    jrscource +
                    newrefNo +
                    //space5 +
                    newcusname +
                     trdate.ToString("yyyyMMdd") +
                     summerdate.ToString("yyyy" + "0" + "MM") +
                     otherdatenote8 +
                     paymentallocnote10 +
                     space5 +
                     paymentallocdate8 +
                     paymentallocdate7 +
                     space1 +
                     space10 +
                     space5 +
                     space5 +
                     converrate +
                     otherammount +
                     space1 +
                     oienter +
                     oipost +
                     oilast +
                     nextperdrev +
                     linktext +
                     additinalsp2 +
                     pcnew +
                     annlcd1 +
                     annlcd2 +
                     seqnew +
                     annlcd5 +
                     annlcd6 +
                     ann7new +
                     annlcd8 +
                     annlcd9 +
                     blankfinal
                     ;

                SunAccountall ob = new SunAccountall();
                ob.accnt_code = acccdnew;
                ob.period = Convert.ToInt32(summerdate.ToString("yyyy" + "0" + "MM"));
                ob.trans_date = Convert.ToInt32(trdate.ToString("yyyyMMdd"));
                if (postCurrPrd)
                {
                    ob.period = Convert.ToInt32(DateTime.Today.AddMonths(-3).ToString("yyyy" + "0" + "MM"));
                    ob.trans_date = Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd"));
                }
                ob.jrnal_no = Convert.ToInt32(jrnum);
                ob.jrnal_line = Convert.ToInt32(jrlinenum);
                ob.amount = Math.Round(Convert.ToDecimal(ammountnew) / 1000, 2);
                ob.d_c = AmmountType;
                ob.allocation = allocmak;
                ob.jrnal_type = jrtype;
                ob.jrnal_srce = jrscource;
                ob.treference = newrefNo;
                ob.descriptn = newcusname;
                ob.entry_date = Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd"));
                ob.entry_prd = Convert.ToInt32(DateTime.Today.AddMonths(-3).ToString("yyyy" + "0" + "MM"));
                ob.due_date = Convert.ToInt32(otherdatenote8);
                ob.alloc_ref = Convert.ToInt32(paymentallocnote10);
                ob.alloc_date = Convert.ToInt32(paymentallocdate8); ;
                ob.alloc_period = Convert.ToInt32(paymentallocdate7);
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

                //ob.anal_t0 = pcnew;
                //ob.anal_t1 = annlcd1;
                //ob.anal_t2 = annlcd2;
                //ob.anal_t3 = seqnew;
                //ob.anal_t4 = space15;
                //ob.anal_t5 = annlcd5;
                //ob.anal_t6 = ann7new;
                //ob.anal_t7 = annlcd6;
                //ob.anal_t8 = annlcd8;
                //ob.anal_t9 = annlcd9;
                ob.anal_t0 = pcnew;
                ob.anal_t1 = annlcd1;
                ob.anal_t2 = annlcd2;
                ob.anal_t3 = seqnew;
                ob.anal_t4 = annlcd5;
                ob.anal_t5 = annlcd6;
                ob.anal_t6 = ann7new;
                ob.anal_t7 = annlcd8;
                ob.anal_t8 = annlcd9;
                ob.anal_t9 = space15;
                ob.alloc_in_progress = " ";
                ob.posting_date = Convert.ToInt32(trdate.ToString("yyyyMMdd"));
                ob.hold_ref = 0;
                ob.hold_op_id = " ";
                ob.last_change_user_id = " ";
                ob.last_change_date = Convert.ToInt32(trdate.ToString("yyyyMMdd"));
                ob.originator_id = " ";

                //(param[40] = new OracleParameter("P_POSTING_DATE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sundata.posting_date;
                //(param[41] = new OracleParameter("P_ALLOC_IN_PROGRESS", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.alloc_in_progress;
                //(param[42] = new OracleParameter("P_HOLD_REF", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sundata.hold_ref;
                //(param[43] = new OracleParameter("P_HOLD_OP_ID", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.hold_op_id;
                //(param[44] = new OracleParameter("P_LAST_CHANGE_USER_ID", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.last_change_user_id;
                //(param[45] = new OracleParameter("P_LAST_CHANGE_DATE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _sundata.last_change_date;
                //(param[46] = new OracleParameter("P_ORIGINATOR_ID", OracleDbType.Char, null, ParameterDirection.Input)).Value = _sundata.originator_id;       
                _sunall.Add(ob);



                return line;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex + "');", true);
                return "";
            }

        }

        public void GetFileAddDirect(string type, DateTime startdate, DateTime enddate, List<string> PCList)
        {

            //Dddivtype.SelectedIndex = Dddivtype.Items.IndexOf(Dddivtype.Items.FindByText("Invoice"));
            // string type = Dddivtype.SelectedValue.ToString();


            // Ddpaytype.Text.ToString();
            GetFile_New(startdate, enddate, type, "DEBT", PCList);

        }

        public void GetFile(DateTime startdate, DateTime enddate)
        {
            try
            {
                SystemUser _systemuser = CHNLSVC.Security.GetUserByUserID(Session["UserID"].ToString());
                string execcode = "N/A";
                var copypath = System.Web.Configuration.WebConfigurationManager.AppSettings["SUNUploadPath"].ToString();
                var copypathper = System.Web.Configuration.WebConfigurationManager.AppSettings["SUNUploadPathper"].ToString();
                Session["path"] = "";
                string name = "C:/SUN" + "/"+_systemuser.Se_SUN_ID + "SCMinv.txt";
                string name2 = "C:/SUN" + "/"+_systemuser.Se_SUN_ID + "SCMREC.txt";
                string name3 = "C:/SUN" + "/"+_systemuser.Se_SUN_ID + "SCMPER.txt";
                string name4 = "C:/SUN" + "/"+_systemuser.Se_SUN_ID + "CRED.txt";
                string Acccode = "";
                string jnaltype = "";
                int count = 0;
                string type = Ddpaytype.Text.ToString();
                string defpc = Session["UserDefProf"].ToString();
                string paytypereal = Dddivtype.SelectedValue.ToString();
                string pctxt = "";
                string grntxt = "";
                string ISTAX = "";
                string date = enddate.Date.ToString("dd");
                date = Session["UserCompanyCode"].ToString() + date + ".txt";
                Int32 isnum = 0;
                foreach (GridViewRow row in gvpclist.Rows)
                {
                    CheckBox chkselect = (CheckBox)row.FindControl("chkselect");
                    if (chkselect != null && chkselect.Checked)
                    {
                        if (pctxt != "")
                        {
                            pctxt = pctxt + ",";
                        }
                        Label lbpccd = (Label)row.FindControl("lbpccd");
                        string com_cd = lbpccd.Text;
                        pctxt = pctxt + com_cd;

                    }
                }

                #region Invoice Upload
                if (type == "Invoice")
                {
                    FileInfo info = new FileInfo(name);
                    if (info.Exists || !info.Exists)
                    {
                        using (StreamWriter writer = info.CreateText())
                        {
                            List<SUN_JURNAL> gnrlist = CHNLSVC.Financial.GetSunJurnalnew(Session["UserCompanyCode"].ToString());
                            gnrlist = gnrlist.OrderBy(a => a.ledg_sales_tp).ToList();
                            List<SUNINVHDR> listorder = CHNLSVC.Financial.GetSunInvdatanew(Session["UserCompanyCode"].ToString(), pctxt, startdate, enddate);
                            if (Session["UserCompanyCode"].ToString() == "AAL" && listorder != null && listorder.Count > 0)
                            {
                                listorder = listorder.Where(a => a.sah_inv_tp != "CRED").ToList();
                            }

                            List<SUNINVHDR> LIORDERSUM = listorder.GroupBy(l => new { l.sah_inv_no })
    .Select(cl => new SUNINVHDR
    {
        sah_inv_no = cl.First().sah_inv_no,
        sah_inv_tp = cl.First().sah_inv_tp,
        sah_inv_sub_tp = cl.First().sah_inv_sub_tp,
        sah_tp = cl.First().sah_tp,
        total = cl.First().total,
        totalunit = cl.Sum(A => A.totalunit),
        taxtotal = cl.Sum(A => A.taxtotal),
        sah_seq_no = cl.First().sah_seq_no,
        sah_pc = cl.First().sah_pc,
        sah_cus_name = cl.First().sah_cus_name,
        sah_dt = cl.First().sah_dt,
        sah_cus_cd = cl.First().sah_cus_cd,
        sah_direct = cl.First().sah_direct,
        sah_man_ref = cl.First().sah_man_ref,
        tax_cd = cl.First().tax_cd,
        isdiliver = cl.First().isdiliver,
        sah_ref_doc = cl.First().sah_ref_doc,
        sah_sales_ex_cd = cl.First().sah_sales_ex_cd,
        sah_is_svat = cl.First().sah_is_svat,
        svatcd = cl.First().svatcd,
        EPF = cl.First().EPF,
        CODE = cl.First().CODE,
        RealTotalwithtax = cl.Sum(a => a.RealTotalwithtax),
        NBTValue = cl.Sum(a => a.NBTValue),
        sah_tax_exempted = cl.First().sah_tax_exempted,
        sah_anal_4 = cl.First().sah_anal_4,
        sad_itm_cd = cl.First().sad_itm_cd
    }).ToList();
                            LIORDERSUM = LIORDERSUM.OrderBy(a => a.sah_inv_no).ToList();

                            var vatemptycount = listorder.Where(a => a.tax_cd == "").Count();
                            if (vatemptycount > 0 || Session["UserCompanyCode"].ToString() == "BDL")
                            {
                                listorder = listorder.GroupBy(l => new { l.sah_inv_no })
.Select(cl => new SUNINVHDR
{
    sah_inv_no = cl.First().sah_inv_no,
    sah_inv_tp = cl.First().sah_inv_tp,
    sah_inv_sub_tp = cl.First().sah_inv_sub_tp,
    sah_tp = cl.First().sah_tp,
    total = cl.First().total,
    totalunit = cl.Sum(A => A.totalunit),
    taxtotal = cl.Sum(A => A.taxtotal),
    sah_seq_no = cl.First().sah_seq_no,
    sah_pc = cl.First().sah_pc,
    sah_cus_name = cl.First().sah_cus_name,
    sah_dt = cl.First().sah_dt,
    sah_cus_cd = cl.First().sah_cus_cd,
    sah_direct = cl.First().sah_direct,
    sah_man_ref = cl.First().sah_man_ref,
    tax_cd = cl.First().tax_cd,
    isdiliver = cl.First().isdiliver,
    sah_ref_doc = cl.First().sah_ref_doc,
    sah_sales_ex_cd = cl.First().sah_sales_ex_cd,
    sah_is_svat = cl.First().sah_is_svat,
    svatcd = cl.First().svatcd,
    EPF = cl.First().EPF,
    CODE = cl.First().CODE,
    RealTotalwithtax = cl.Sum(a => a.RealTotalwithtax),
    NBTValue = cl.Sum(a => a.NBTValue),
    sah_tax_exempted = cl.First().sah_tax_exempted,
    sah_anal_4 = cl.First().sah_anal_4,
    sad_itm_cd = cl.First().sad_itm_cd
}).ToList();
                            }
                            else
                            {
                                listorder = listorder.GroupBy(l => new { l.sah_inv_no, l.tax_cd })
.Select(cl => new SUNINVHDR
{
    sah_inv_no = cl.First().sah_inv_no,
    sah_inv_tp = cl.First().sah_inv_tp,
    sah_inv_sub_tp = cl.First().sah_inv_sub_tp,
    sah_tp = cl.First().sah_tp,
    total = cl.First().total,
    totalunit = cl.Sum(A => A.totalunit),
    taxtotal = cl.Sum(A => A.taxtotal),
    sah_seq_no = cl.First().sah_seq_no,
    sah_pc = cl.First().sah_pc,
    sah_cus_name = cl.First().sah_cus_name,
    sah_dt = cl.First().sah_dt,
    sah_cus_cd = cl.First().sah_cus_cd,
    sah_direct = cl.First().sah_direct,
    sah_man_ref = cl.First().sah_man_ref,
    tax_cd = cl.First().tax_cd,
    isdiliver = cl.First().isdiliver,
    sah_ref_doc = cl.First().sah_ref_doc,
    sah_sales_ex_cd = cl.First().sah_sales_ex_cd,
    sah_is_svat = cl.First().sah_is_svat,
    svatcd = cl.First().svatcd,
    EPF = cl.First().EPF,
    CODE = cl.First().CODE,
    RealTotalwithtax = cl.Sum(a => a.RealTotalwithtax),
    NBTValue = cl.Sum(a => a.NBTValue),
    sah_tax_exempted = cl.First().sah_tax_exempted,
    sah_anal_4 = cl.First().sah_anal_4,
    sad_itm_cd = cl.First().sad_itm_cd
}).ToList();
                            }


                            listorder = listorder.OrderBy(a => a.sah_inv_no).ToList();
                            string tempinvno = "";

                            if (listorder != null)
                                listorder = listorder.OrderBy(a => a.sah_inv_tp).ToList();
                            if (listorder != null && gnrlist != null)
                            {
                                foreach (var invdata in listorder)
                                {
                                    if (invdata.totalunit != 0)
                                    {
                                        //Commented by Wimal @ 19/07/2018 to upload
                                        //customer@char
                                        invdata.sah_cus_name = invdata.sah_cus_name.Replace(@"–", string.Empty);
                                        invdata.sah_cus_name = invdata.sah_cus_name.Replace(@"’", string.Empty);
                                        invdata.sah_cus_name = invdata.sah_cus_name.Replace(@"-", string.Empty);
                                        invdata.sah_cus_name = invdata.sah_cus_name.Replace(@"'", string.Empty);
                                        execcode = "N/A";
                                        if (invdata.sah_sales_ex_cd != null) execcode = invdata.sah_sales_ex_cd.ToString();
                                        //check exc code
                                        int n;
                                        bool isNumeric = int.TryParse(execcode, out n);
                                        if (isNumeric == true)
                                        {
                                            if (invdata.CODE != null)
                                            {
                                                isNumeric = int.TryParse(invdata.CODE, out n);
                                                if (isNumeric == true)
                                                {
                                                    if (invdata.EPF != null)
                                                        execcode = invdata.EPF.ToString();
                                                }
                                                else
                                                {
                                                    execcode = invdata.CODE.ToString();
                                                }
                                            }

                                        }


                                        int paytpcount = 0;
                                        string DUTY = "";
                                        string paytype = "1";
                                        if (invdata.sah_inv_tp.ToString() != null)
                                            paytype = invdata.sah_inv_tp.Trim().ToString();


                                        foreach (var gnr in gnrlist)
                                        {
                                            if (gnr.ledg_is_dealer == null)
                                            {
                                                gnr.ledg_is_dealer = "";
                                            }
                                            ISTAX = gnr.ledg_is_dealer;
                                            string gnrtype = "2";
                                            if (gnr.ledg_sales_tp != null)
                                                gnrtype = gnr.ledg_sales_tp.Trim().ToString();

                                            if (paytype == gnrtype)
                                                count = 1;

                                            if (paytype == gnrtype && gnr.ledg_sub_tp == "SA" && gnr.ledg_acc_tp == "DR" && gnr.ledg_pc == invdata.sah_pc && (gnr.ledg_desc == "CASH" | gnr.ledg_desc == "CRED" | gnr.ledg_desc == "DUTY" | gnr.ledg_desc == "TOTO" | gnr.ledg_desc == "UNITY"))
                                            {
                                                decimal alltotal = 0;
                                                if (tempinvno != invdata.sah_inv_no)
                                                    alltotal = invdata.total; //Convert.ToDecimal(LIORDERSUM.Where(a => a.sah_inv_no == invdata.sah_inv_no).First().totalunit) + Convert.ToDecimal(LIORDERSUM.Where(a => a.sah_inv_no == invdata.sah_inv_no).First().taxtotal);

                                                //REVERSISSUE
                                                tempinvno = invdata.sah_inv_no;
                                                decimal TOTNEW = Convert.ToDecimal(invdata.totalunit) + Convert.ToDecimal(invdata.taxtotal);
                                                string TOTNEW2 = alltotal.ToString("F3", CultureInfo.InvariantCulture);
                                                string Cuscode = "";
                                                Acccode = gnr.ledg_acc_cd.ToString();
                                                string TOTAmmount = invdata.total.ToString("F3", CultureInfo.InvariantCulture);
                                                string TAXAmmount = invdata.taxtotal.ToString("F3", CultureInfo.InvariantCulture);
                                                string TOTunit = invdata.totalunit.ToString("F3", CultureInfo.InvariantCulture);


                                                if (invdata.sah_cus_cd != null)
                                                    Cuscode = invdata.sah_cus_cd.ToString();
                                                DateTime transdate = invdata.sah_dt;
                                                string refNo = invdata.sah_inv_no.ToString();
                                                string cusname = invdata.sah_cus_name.ToString().Trim();
                                                string pc = invdata.sah_pc.ToString();
                                                string seq = invdata.tax_cd.ToString();
                                                jnaltype = gnr.ledg_jnl_tp.ToString();




                                                if ((Cuscode.Contains("CONT") || Cuscode.Contains("CASH")))
                                                {
                                                    if (pc == "502" && paytype == "HECS")
                                                    {
                                                        string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "502CONTCUS");
                                                        Cuscode = _cusacc;
                                                    }
                                                    else
                                                    {
                                                        string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "CONTCUS");
                                                        Cuscode = _cusacc;
                                                    }
                                                }
                                                //for AAL
                                                if (gnr.ledg_acc_cd == "BANK")
                                                {
                                                    string _recaccno = CHNLSVC.Financial.GetRecAccount(invdata.sah_inv_no);
                                                    if (_recaccno == "")
                                                    {
                                                        _recaccno = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "CONTCUS");
                                                    }

                                                    if (pc == "502" && paytype == "HECS")
                                                    {
                                                        _recaccno = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "502CONTCUS");                                                       
                                                    }
                                                    Cuscode = _recaccno;
                                                }

                                                if (seq == "" | seq == "M" | seq == "N") seq = "O";
                                                if (pc == "103")
                                                {
                                                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Pleace Check Customer Code in INV : " + refNo + " CODE: " + Cuscode +"Pay Type " + paytype+ " PC :"+pc+"');", true);
                                                    //return;
                                                    string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "103CUS");
                                                    Cuscode = _cusacc;
                                                }
                                                if ((Cuscode.Contains("CONT") | (Cuscode.Contains("CASH"))) && pc == "44A")
                                                {
                                                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Pleace Check Customer Code in INV : " + refNo + " CODE: " + Cuscode +"Pay Type " + paytype+ " PC :"+pc+"');", true);
                                                    //return;
                                                    string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "44CONTCUS");
                                                    Cuscode = _cusacc;
                                                }

                                                if ((Cuscode.Contains("CONT") | (Cuscode.Contains("CASH"))) && pc == "502" && paytype == "HECS")
                                                {
                                                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Pleace Check Customer Code in INV : " + refNo + " CODE: " + Cuscode +"Pay Type " + paytype+ " PC :"+pc+"');", true);
                                                    //return;
                                                    string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "502CONTCUS");
                                                    Cuscode = _cusacc;
                                                }

                                                if (gnr.ledg_desc == "UNITY")
                                                {
                                                    string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "UNITYCUS");
                                                    Cuscode = _cusacc;
                                                }
                                                //split ref

                                                string[] words = refNo.Split('-');

                                                if (words.Length > 2)
                                                {
                                                    if (words[0] == "103" || words[0] == "41")
                                                    {
                                                        refNo = words[0] + words[1];
                                                        if (refNo.Length > 5)
                                                        {
                                                            refNo = refNo.Substring(0, 5);
                                                        }
                                                        refNo = refNo + words[2];
                                                        if (refNo.Length > 10)
                                                        {
                                                            refNo = refNo.Substring(0, 10);
                                                        }
                                                    }
                                                    else if (words[0] == "44A" || words[0] == "129" || words[0] == "181" || words[0] == "89" || words[0] == "144" || words[0] == "194" || words[0] == "183" || words[0] == "37" || words[0] == "122" || words[0] == "76" || Int32.TryParse(words[0], out isnum) == true)
                                                    {
                                                        if (invdata.sah_inv_no.Contains("INDBN"))
                                                        {
                                                            refNo = words[1] + words[3];
                                                        }
                                                        else
                                                        {
                                                            refNo = words[1] + words[2];
                                                            if (refNo.Length > 10)
                                                            {
                                                                refNo = refNo.Substring(0, 10);
                                                            }
                                                        }


                                                    }
                                                    else
                                                    {
                                                        refNo = words[1] + words[2] + words[0];

                                                    }

                                                }
                                                //Change Ref
                                                //if (refNo.Contains("-INREV"))
                                                //{
                                                //    //refNo = invdata.sah_ref_doc.Substring(0, 4) + invdata.sah_inv_no;
                                                //    //if (refNo.Length > 10)
                                                //    //{
                                                //    //    refNo ="Z"+ refNo.Substring(0, 4) + refNo.Substring(refNo.Length - 6);

                                                //    //}
                                                //    refNo = "Z" + invdata.sah_ref_doc.ToString();
                                                //}
                                                if (invdata.sah_direct == 0)
                                                {
                                                    int invlenght = invdata.sah_inv_no.Length;
                                                    refNo = invdata.sah_ref_doc.ToString();
                                                    if (invdata.sah_anal_4 == "DISC")
                                                    {
                                                        refNo = invdata.sah_inv_no.ToString();
                                                    }
                                                    string[] words2 = refNo.Split('-');
                                                    if (words2.Length == 2)
                                                    {
                                                        if (invdata.sah_anal_4 == "DISC")
                                                        {
                                                            refNo = invdata.sah_pc.ToString() + words2[1];
                                                        }
                                                        else
                                                        {
                                                            refNo = "Z" + words2[0] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                        }
                                                    }
                                                    else if (words2.Length == 3)
                                                    {
                                                        if (invdata.sah_anal_4 == "DISC")
                                                        {
                                                            refNo = invdata.sah_pc.ToString() + words2[2];
                                                        }
                                                        else
                                                        {
                                                            if (words2[0] == "103")
                                                            {
                                                                refNo = "Z" + words2[0] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                            }
                                                            else if (words2[0] == "44A" || words2[0] == "129" || words2[0] == "181" || words[0] == "89" || words[0] == "144" || words[0] == "194" || words[0] == "183" || words[0] == "37" || words[0] == "122" || words[0] == "76" || Int32.TryParse(words[0], out isnum) == true)
                                                            {
                                                                refNo = "Z" + words2[1] + words2[2];
                                                            }
                                                            else
                                                            {
                                                                refNo = "Z" + words2[1] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                            }

                                                        }

                                                    }
                                                    else
                                                    {
                                                        if (invdata.sah_anal_4 == "DISC")
                                                        {
                                                            refNo = invdata.sah_pc.ToString() + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                        }
                                                        else
                                                        {
                                                            refNo = "Z" + refNo.Substring(0, 4) + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                        }

                                                    }
                                                }
                                                if (refNo.Length > 10)
                                                {
                                                    refNo = refNo.Replace(@"-", string.Empty);
                                                }

                                                if (invdata.sah_is_svat == 1) seq = invdata.svatcd.ToString();
                                                if (jnaltype == "") jnaltype = "INV1";
                                                if (gnr.ledg_desc == "DUTY")
                                                {
                                                    jnaltype = "INV8";
                                                    seq = "O";
                                                }
                                                if (invdata.sah_tax_exempted == 1)
                                                {
                                                    seq = "E";
                                                }

                                                ////check is discounted
                                                ////check ref no 
                                                //bool isupload = CHNLSVC.Financial.CheckSunRef(refNo, invdata.sah_dt.ToString("yyyyMMdd"));
                                                //if (isupload)
                                                //{
                                                //    continue;
                                                //}



                                                if (invdata.sah_direct == 1)
                                                {
                                                    string _drdesc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "AALCRDESC");
                                                    if (_drdesc == gnr.ledg_desc)
                                                    {
                                                        cusname = invdata.sah_anal_4;
                                                    }
                                                    string _drdesccss = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "AALCRDESCCS");
                                                    if (_drdesccss == gnr.ledg_desc && gnr.ledg_pc != "502")
                                                    {
                                                        List<SunAALRec> _sunref = CHNLSVC.Financial.GET_SUNRECREF(invdata.sah_inv_no);
                                                        if (_sunref != null && _sunref.Count > 0)
                                                        {
                                                            decimal _totsunrec = _sunref.Sum(a => a.sard_settle_amt);
                                                            if (Math.Abs(_totsunrec - Convert.ToDecimal(TOTNEW2)) > 1)
                                                            {
                                                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Check Invoice No :" + invdata.sah_inv_no + " Settlement Details!" + "');", true);
                                                                return;
                                                            }

                                                            foreach (var _list in _sunref)
                                                            {
                                                                if (_list.sard_deposit_bank_cd != null && _list.sard_deposit_bank_cd !="")
                                                                {
                                                                    Cuscode = _list.sard_deposit_bank_cd;
                                                                }
                                                                string TOTNEW3 = _list.sard_settle_amt.ToString("F3", CultureInfo.InvariantCulture);
                                                                writer.WriteLine(PrintSunLine(Cuscode, TOTNEW3.ToString(), "D", transdate, refNo, _list.sard_ref_no, pc, seq, jnaltype, execcode));
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (alltotal != 0)
                                                                writer.WriteLine(PrintSunLine(Cuscode, TOTNEW2.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (alltotal != 0)
                                                            writer.WriteLine(PrintSunLine(Cuscode, TOTNEW2.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                    }
                                                }
                                                else if (invdata.sah_direct == 0 && (gnr.ledg_desc == "TOTO" | gnr.ledg_desc == "UNITY"))
                                                {
                                                    jnaltype = "SRTN1";

                                                    if (Session["UserCompanyCode"].ToString() == "SAE")
                                                    {
                                                        jnaltype = "INVSAER";
                                                    }
                                                    cusname = invdata.sah_man_ref.ToString();
                                                    if (alltotal != 0)
                                                        writer.WriteLine(PrintSunLine("ABBCL1041", TOTNEW2.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                }
                                                else if (invdata.sah_direct == 0 && invdata.sah_anal_4 == "DISC")
                                                {
                                                    jnaltype = "REC1";
                                                    cusname = invdata.sah_ref_doc.ToString();
                                                    if (alltotal != 0)
                                                        writer.WriteLine(PrintSunLine(Cuscode, TOTNEW2.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                }
                                                else
                                                {

                                                    if (invdata.isdiliver == "0" && invdata.sah_man_ref.Contains("SRN") == false && invdata.sah_inv_no.Contains("INCRN") == false)
                                                    {
                                                        //cancel srn 2016-11-19
                                                        string refNo1 = invdata.sah_inv_no.Replace(@"-", string.Empty);
                                                        refNo1 = refNo1.Replace(@"REV", string.Empty);
                                                        int srnlenght = refNo1.Length;
                                                        if (srnlenght > 10)
                                                        {
                                                            //  refNo = refNo1.Substring(0, 10);
                                                            refNo = refNo1.Substring(0, 3) + "REV" + refNo1.Substring(srnlenght - 4);
                                                        }
                                                        else
                                                        {
                                                            refNo = refNo1;
                                                        }
                                                        cusname = invdata.sah_ref_doc.ToString();
                                                    }
                                                    else
                                                    {




                                                        cusname = invdata.sah_man_ref.ToString();
                                                        if (cusname == "" || cusname == "N/A" || (cusname.Contains("SRN") == false))
                                                        {
                                                            cusname = invdata.sah_ref_doc.ToString();
                                                        }
                                                        if (invdata.sah_direct == 0 && invdata.sah_inv_no.Contains("INCRN"))
                                                        {
                                                            cusname = invdata.sah_ref_doc.ToString();
                                                            string[] crwords = invdata.sah_inv_no.Split('-');
                                                            if (crwords.Length > 2)
                                                            {
                                                                refNo = crwords[0] + "CN" + crwords[3];
                                                            }
                                                            else
                                                            {
                                                                refNo = invdata.sah_inv_no;
                                                            }
                                                        }
                                                    }

                                                    ////check ref no 
                                                    //bool isupload2 = CHNLSVC.Financial.CheckSunRef(refNo, invdata.sah_dt.ToString("yyyyMMdd"));
                                                    //if (isupload2)
                                                    //{
                                                    //    continue;
                                                    //}


                                                    jnaltype = "SRTN1";
                                                    if (Session["UserCompanyCode"].ToString() == "SAE")
                                                    {
                                                        jnaltype = "INVSAER";
                                                    }
                                                    if (alltotal != 0)
                                                        writer.WriteLine(PrintSunLine(Cuscode, TOTNEW2.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                }

                                                paytpcount++;
                                                DUTY = gnr.ledg_desc;

                                            }
                                            if (paytype == gnrtype && gnr.ledg_sub_tp == "VAT" && gnr.ledg_acc_tp == "CR" && gnr.ledg_pc == invdata.sah_pc && (gnr.ledg_desc == "CASH" | gnr.ledg_desc == "CRED" | gnr.ledg_desc == "TOTO" | gnr.ledg_desc == "UNITY"))
                                            {

                                                Acccode = gnr.ledg_acc_cd.ToString();
                                                string TOTAmmount = invdata.total.ToString("F3", CultureInfo.InvariantCulture);
                                                string TAXAmmount = invdata.taxtotal.ToString("F3", CultureInfo.InvariantCulture);
                                                string TOTunit = invdata.totalunit.ToString("F3", CultureInfo.InvariantCulture);
                                                DateTime transdate = invdata.sah_dt;
                                                string refNo = invdata.sah_inv_no.ToString();
                                                string cusname = invdata.sah_cus_name.ToString().Trim();
                                                string pc = invdata.sah_pc.ToString();
                                                string seq = invdata.tax_cd.ToString();
                                                jnaltype = gnr.ledg_jnl_tp.ToString();
                                                if (seq == "" | seq == "M" | seq == "N") seq = "O";
                                                if (jnaltype == "") jnaltype = "INV1";

                                                if (invdata.sah_is_svat == 1) seq = invdata.svatcd.ToString();

                                                if (gnr.ledg_desc == "DUTY")
                                                {
                                                    jnaltype = "INV8";
                                                    seq = "O";
                                                }
                                                //split ref

                                                string[] words = refNo.Split('-');
                                                if (words.Length > 2)
                                                {
                                                    if (words[0] == "103" || words[0] == "41")
                                                    {
                                                        refNo = words[0] + words[1];
                                                        if (refNo.Length > 5)
                                                        {
                                                            refNo = refNo.Substring(0, 5);
                                                        }
                                                        refNo = refNo + words[2];
                                                        if (refNo.Length > 10)
                                                        {
                                                            refNo = refNo.Substring(0, 10);
                                                        }
                                                    }
                                                    else if (words[0] == "44A" || words[0] == "129" || words[0] == "181" || words[0] == "89" || words[0] == "144" || words[0] == "194" || words[0] == "183" || words[0] == "37" || words[0] == "122" || words[0] == "76" || Int32.TryParse(words[0], out isnum) == true)
                                                    {
                                                        if (invdata.sah_inv_no.Contains("INDBN"))
                                                        {
                                                            refNo = words[1] + words[3];
                                                        }
                                                        else
                                                        {
                                                            refNo = words[1] + words[2];
                                                            if (refNo.Length > 10)
                                                            {
                                                                refNo = refNo.Substring(0, 10);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        refNo = words[1] + words[2] + words[0];

                                                    }

                                                }
                                                //Change Ref
                                                //if (refNo.Contains("-INREV"))
                                                //{
                                                //    //refNo = invdata.sah_ref_doc.Substring(0, 4) + invdata.sah_inv_no;
                                                //    //if (refNo.Length > 10)
                                                //    //{
                                                //    //    refNo = "Z" + refNo.Substring(0, 4) + refNo.Substring(refNo.Length - 6);

                                                //    //}
                                                //    refNo = "Z" + invdata.sah_ref_doc.ToString();
                                                //}
                                                if (invdata.sah_direct == 0)
                                                {
                                                    int invlenght = invdata.sah_inv_no.Length;
                                                    refNo = invdata.sah_ref_doc.ToString();
                                                    if (invdata.sah_anal_4 == "DISC")
                                                    {
                                                        refNo = invdata.sah_inv_no.ToString();
                                                    }
                                                    string[] words2 = refNo.Split('-');
                                                    if (words2.Length == 2)
                                                    {
                                                        if (invdata.sah_anal_4 == "DISC")
                                                        {
                                                            refNo = invdata.sah_pc.ToString() + words2[1];
                                                        }
                                                        else
                                                        {
                                                            refNo = "Z" + words2[0] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                        }
                                                    }
                                                    else if (words2.Length == 3)
                                                    {
                                                        if (invdata.sah_anal_4 == "DISC")
                                                        {
                                                            refNo = invdata.sah_pc.ToString() + words2[2];
                                                        }
                                                        else
                                                        {
                                                            if (words2[0] == "103")
                                                            {
                                                                refNo = "Z" + words2[0] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                            }
                                                            else if (words2[0] == "44A" || words2[0] == "129" || words2[0] == "181" || words[0] == "89" || words[0] == "144" || words[0] == "194" || words[0] == "183" || words[0] == "37" || words[0] == "122" || words[0] == "76" || Int32.TryParse(words[0], out isnum) == true)
                                                            {
                                                                refNo = "Z" + words2[1] + words2[2];
                                                            }
                                                            else
                                                            {
                                                                refNo = "Z" + words2[1] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                            }

                                                        }

                                                    }
                                                    else
                                                    {
                                                        if (invdata.sah_anal_4 == "DISC")
                                                        {
                                                            refNo = invdata.sah_pc.ToString() + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                        }
                                                        else
                                                        {
                                                            refNo = "Z" + refNo.Substring(0, 4) + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                        }

                                                    }
                                                }
                                                if (refNo.Length > 10)
                                                {
                                                    refNo = refNo.Replace(@"-", string.Empty);
                                                }
                                                if (invdata.sah_tax_exempted == 1)
                                                {
                                                    seq = "E";
                                                }

                                                ////check ref no 
                                                //bool isupload3 = CHNLSVC.Financial.CheckSunRef(refNo, invdata.sah_dt.ToString("yyyyMMdd"));
                                                //if (isupload3)
                                                //{
                                                //    continue;
                                                //}



                                                if (invdata.sah_direct == 1)
                                                {
                                                    if (invdata.taxtotal != 0)
                                                    {
                                                        //if (invdata.NBTValue == 0)
                                                        //{
                                                        //    writer.WriteLine(PrintSunLine(Acccode, TAXAmmount.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                        //}
                                                        //else
                                                        //{
                                                        //    decimal itemtottax = invdata.taxtotal;
                                                        //    decimal nbtvalue = invdata.NBTValue;

                                                        //    decimal realtax = itemtottax - nbtvalue;
                                                        //    string strnbt = nbtvalue.ToString("F3", CultureInfo.InvariantCulture);
                                                        //    string strrealtax = realtax.ToString("F3", CultureInfo.InvariantCulture);

                                                        //    string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "NBT");
                                                        //    if (_nbtacc == "")
                                                        //    {
                                                        //        _nbtacc = Acccode;
                                                        //    }

                                                        //    writer.WriteLine(PrintSunLine(_nbtacc, strnbt.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                        //    writer.WriteLine(PrintSunLine(Acccode, strrealtax.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                        //}

                                                        //VAT-NBT-XID-
                                                        decimal _vatval = CHNLSVC.Financial.GetTaxValForCode(invdata.sah_inv_no, "VAT");
                                                        decimal _nbtval = CHNLSVC.Financial.GetTaxValForCode(invdata.sah_inv_no, "NBT");
                                                        decimal _xidval = CHNLSVC.Financial.GetTaxValForCode(invdata.sah_inv_no, "XID");
                                                        decimal _other = invdata.taxtotal - _vatval - _nbtval - _xidval;
                                                        if (_other > 0 && _other < 1)
                                                        {
                                                            _vatval = _vatval + _other;
                                                            _other = 0;
                                                        }
                                                        if (_vatval > 0)
                                                        {
                                                            string vvl = _vatval.ToString("F3", CultureInfo.InvariantCulture);
                                                            writer.WriteLine(PrintSunLine(Acccode, vvl.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                        }

                                                        if (_nbtval > 0)
                                                        {
                                                            string vvl = _nbtval.ToString("F3", CultureInfo.InvariantCulture);

                                                            string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "NBT");
                                                            if (_nbtacc == "")
                                                            {
                                                                _nbtacc = Acccode;
                                                            }

                                                            writer.WriteLine(PrintSunLine(_nbtacc, vvl.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                        }

                                                        if (_xidval > 0)
                                                        {
                                                            string vvl = _nbtval.ToString("F3", CultureInfo.InvariantCulture);

                                                            string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "XID");
                                                            if (_nbtacc == "")
                                                            {
                                                                _nbtacc = Acccode;
                                                            }

                                                            writer.WriteLine(PrintSunLine(_nbtacc, vvl.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                        }
                                                        if (_other > 0)
                                                        {
                                                            string vvl = _other.ToString("F3", CultureInfo.InvariantCulture);
                                                            writer.WriteLine(PrintSunLine(Acccode, vvl.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                        }


                                                    }

                                                }
                                                else
                                                {
                                                    if (invdata.isdiliver == "0" && invdata.sah_man_ref.Contains("SRN") == false && invdata.sah_inv_no.Contains("INCRN") == false)
                                                    {
                                                        //cancel srn 2016-11-19
                                                        string refNo1 = invdata.sah_inv_no.Replace(@"-", string.Empty);
                                                        refNo1 = refNo1.Replace(@"REV", string.Empty);
                                                        int srnlenght = refNo1.Length;
                                                        if (srnlenght > 10)
                                                        {
                                                            //refNo = refNo1.Substring(0, 10);
                                                            refNo = refNo1.Substring(0, 3) + "REV" + refNo1.Substring(srnlenght - 4);
                                                        }
                                                        else
                                                        {
                                                            refNo = refNo1;
                                                        }
                                                        cusname = invdata.sah_ref_doc.ToString();
                                                    }
                                                    else if (invdata.sah_direct == 0 && invdata.sah_inv_no.Contains("INCRN"))
                                                    {
                                                        cusname = invdata.sah_ref_doc.ToString();
                                                        string[] crwords = invdata.sah_inv_no.Split('-');
                                                        if (crwords.Length > 2)
                                                        {
                                                            refNo = crwords[0] + "CN" + crwords[3];
                                                        }
                                                        else
                                                        {
                                                            refNo = invdata.sah_inv_no;
                                                        }
                                                    }
                                                    else
                                                    {

                                                        cusname = invdata.sah_man_ref.ToString();
                                                        if (cusname == "" || cusname == "N/A" || (cusname.Contains("SRN") == false))
                                                        {
                                                            cusname = invdata.sah_ref_doc.ToString();
                                                        }
                                                    }

                                                    ////check ref no 
                                                    //bool isupload4 = CHNLSVC.Financial.CheckSunRef(refNo, invdata.sah_dt.ToString("yyyyMMdd"));
                                                    //if (isupload4)
                                                    //{
                                                    //    continue;
                                                    //}


                                                    jnaltype = "SRTN1";
                                                    if (Session["UserCompanyCode"].ToString() == "SAE")
                                                    {
                                                        jnaltype = "INVSAER";
                                                    }
                                                    string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "VATREV");
                                                    if (_nbtacc == "")
                                                    {
                                                        _nbtacc = Acccode;
                                                    }
                                                    if (invdata.taxtotal != 0)
                                                        writer.WriteLine(PrintSunLine(_nbtacc, TAXAmmount.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                }

                                                paytpcount++;

                                            }
                                            if (paytype == gnrtype && gnr.ledg_sub_tp == "SA" && gnr.ledg_acc_tp == "CR" && gnr.ledg_pc == invdata.sah_pc && (gnr.ledg_desc == "CASH" | gnr.ledg_desc == "CRED" | gnr.ledg_desc == "DUTY" | gnr.ledg_desc == "TOTO" | gnr.ledg_desc == "UNITY"))
                                            {
                                                Acccode = gnr.ledg_acc_cd.ToString();
                                                string TOTAmmount = invdata.total.ToString("F3", CultureInfo.InvariantCulture);
                                                string TAXAmmount = invdata.taxtotal.ToString("F3", CultureInfo.InvariantCulture);

                                                decimal realtax = invdata.taxtotal;
                                                decimal realvalue = invdata.RealTotalwithtax - realtax;

                                                string TOTunit = realvalue.ToString("F3", CultureInfo.InvariantCulture); //invdata.totalunit.ToString("F3", CultureInfo.InvariantCulture);
                                                DateTime transdate = invdata.sah_dt;
                                                string refNo = invdata.sah_inv_no.ToString();
                                                string cusname = invdata.sah_cus_name.ToString().Trim();
                                                string pc = invdata.sah_pc.ToString();
                                                string seq = invdata.tax_cd.ToString();
                                                jnaltype = gnr.ledg_jnl_tp.ToString();
                                                if (seq == "" | seq == "M" | seq == "N") seq = "O";
                                                if (jnaltype == "") jnaltype = "INV1";

                                                if (invdata.sah_is_svat == 1) seq = invdata.svatcd.ToString();

                                                if (gnr.ledg_desc == "DUTY")
                                                {
                                                    jnaltype = "INV8";
                                                    seq = "O";
                                                }
                                                //split ref

                                                string[] words = refNo.Split('-');
                                                if (words.Length > 2)
                                                {
                                                    if (words[0] == "103" || words[0] == "41")
                                                    {
                                                        refNo = words[0] + words[1];
                                                        if (refNo.Length > 5)
                                                        {
                                                            refNo = refNo.Substring(0, 5);
                                                        }
                                                        refNo = refNo + words[2];
                                                        if (refNo.Length > 10)
                                                        {
                                                            refNo = refNo.Substring(0, 10);
                                                        }
                                                    }
                                                    else if (words[0] == "44A" || words[0] == "129" || words[0] == "181" || words[0] == "89" || words[0] == "144" || words[0] == "194" || words[0] == "183" || words[0] == "37" || words[0] == "122" || words[0] == "76" || Int32.TryParse(words[0], out isnum) == true)
                                                    {
                                                        if (invdata.sah_inv_no.Contains("INDBN"))
                                                        {
                                                            refNo = words[1] + words[3];
                                                        }
                                                        else
                                                        {
                                                            refNo = words[1] + words[2];
                                                            if (refNo.Length > 10)
                                                            {
                                                                refNo = refNo.Substring(0, 10);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        refNo = words[1] + words[2] + words[0];

                                                    }

                                                }
                                                //Change Ref
                                                //if (refNo.Contains("-INREV"))
                                                //{
                                                //    //refNo = invdata.sah_ref_doc.Substring(0, 4) + invdata.sah_inv_no;
                                                //    //if (refNo.Length > 10)
                                                //    //{
                                                //    //    refNo = "Z" + refNo.Substring(0, 4) + refNo.Substring(refNo.Length - 6);

                                                //    //}
                                                //    refNo ="Z"+ invdata.sah_ref_doc.ToString();
                                                //}
                                                if (invdata.sah_direct == 0)
                                                {
                                                    int invlenght = invdata.sah_inv_no.Length;
                                                    refNo = invdata.sah_ref_doc.ToString();
                                                    if (invdata.sah_anal_4 == "DISC")
                                                    {
                                                        refNo = invdata.sah_inv_no.ToString();
                                                    }
                                                    string[] words2 = refNo.Split('-');
                                                    if (words2.Length == 2)
                                                    {
                                                        if (invdata.sah_anal_4 == "DISC")
                                                        {
                                                            refNo = invdata.sah_pc.ToString() + words2[1];
                                                        }
                                                        else
                                                        {
                                                            refNo = "Z" + words2[0] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                        }
                                                    }
                                                    else if (words2.Length == 3)
                                                    {
                                                        if (invdata.sah_anal_4 == "DISC")
                                                        {
                                                            refNo = invdata.sah_pc.ToString() + words2[2];
                                                        }
                                                        else
                                                        {
                                                            if (words2[0] == "103")
                                                            {
                                                                refNo = "Z" + words2[0] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                            }
                                                            else if (words2[0] == "44A" || words2[0] == "129" || words2[0] == "181" || words[0] == "89" || words[0] == "144" || words[0] == "194" || words[0] == "183" || words[0] == "37" || words[0] == "122" || words[0] == "76" || Int32.TryParse(words[0], out isnum) == true)
                                                            {
                                                                refNo = "Z" + words2[1] + words2[2];
                                                            }
                                                            else
                                                            {
                                                                refNo = "Z" + words2[1] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                            }

                                                        }

                                                    }
                                                    else
                                                    {
                                                        if (invdata.sah_anal_4 == "DISC")
                                                        {
                                                            refNo = invdata.sah_pc.ToString() + "CRN" + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                        }
                                                        else
                                                        {
                                                            refNo = "Z" + refNo.Substring(0, 4) + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                        }

                                                    }
                                                }
                                                if (refNo.Length > 10)
                                                {
                                                    refNo = refNo.Replace(@"-", string.Empty);
                                                }
                                                if (invdata.sah_tax_exempted == 1)
                                                {
                                                    seq = "E";
                                                }

                                                ////check ref no 
                                                //bool isupload5 = CHNLSVC.Financial.CheckSunRef(refNo, invdata.sah_dt.ToString("yyyyMMdd"));
                                                //if (isupload5)
                                                //{
                                                //    continue;
                                                //}



                                                if (invdata.sah_direct == 1)
                                                {
                                                    if (invdata.totalunit != 0)
                                                    {
                                                        if (Session["UserCompanyCode"].ToString() == "BDL")
                                                        {
                                                            decimal tritemcost = CHNLSVC.Financial.GetBDLTransINV(invdata.sah_inv_no);
                                                            if (tritemcost > 0)
                                                            {
                                                                string trcost = tritemcost.ToString("F3", CultureInfo.InvariantCulture);
                                                                decimal aftotunit = invdata.totalunit - tritemcost;
                                                                string aftotun = aftotunit.ToString("F3", CultureInfo.InvariantCulture);
                                                                writer.WriteLine(PrintSunLine("BDPOI1002", trcost.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                writer.WriteLine(PrintSunLine(Acccode, aftotun.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                            }
                                                            else
                                                            {
                                                                writer.WriteLine(PrintSunLine(Acccode, TOTunit.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                            }

                                                        }
                                                        else
                                                        {
                                                            writer.WriteLine(PrintSunLine(Acccode, TOTunit.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                        }

                                                    }


                                                }
                                                else
                                                {
                                                    if (invdata.isdiliver != "0" || invdata.sah_man_ref.Contains("SRN") == true)
                                                    {


                                                        cusname = invdata.sah_man_ref.ToString();
                                                        if ((cusname == "" || cusname == "N/A" || (cusname.Contains("SRN") == false)))
                                                        {
                                                            cusname = invdata.sah_ref_doc.ToString();
                                                            string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "REV");
                                                            if (_nbtacc == "")
                                                            {
                                                                _nbtacc = Acccode;
                                                            }
                                                            if (invdata.totalunit != 0)
                                                            {
                                                                if (Session["UserCompanyCode"].ToString() == "BDL")
                                                                {
                                                                    decimal tritemcost = CHNLSVC.Financial.GetBDLTransINV(invdata.sah_inv_no);
                                                                    if (tritemcost > 0)
                                                                    {
                                                                        string trcost = tritemcost.ToString("F3", CultureInfo.InvariantCulture);
                                                                        decimal aftotunit = invdata.totalunit - tritemcost;
                                                                        string aftotun = aftotunit.ToString("F3", CultureInfo.InvariantCulture);
                                                                        writer.WriteLine(PrintSunLine("BDPOI1002", trcost.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                        writer.WriteLine(PrintSunLine(Acccode, aftotun.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                    }
                                                                    else
                                                                    {
                                                                        writer.WriteLine(PrintSunLine(Acccode, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                    }

                                                                }
                                                                else
                                                                {
                                                                    writer.WriteLine(PrintSunLine(_nbtacc, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                }

                                                               
                                                            }
                                                               
                                                        }
                                                        else
                                                        {
                                                            jnaltype = "SRTN1";
                                                            if (Session["UserCompanyCode"].ToString() == "SAE")
                                                            {
                                                                jnaltype = "INVSAER";
                                                            }
                                                            string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "SRN");
                                                            if (_nbtacc == "")
                                                            {
                                                                _nbtacc = Acccode;
                                                            }
                                                            if (invdata.totalunit != 0)
                                                            {
                                                                if (Session["UserCompanyCode"].ToString() == "BDL")
                                                                {
                                                                    decimal tritemcost = CHNLSVC.Financial.GetBDLTransINV(invdata.sah_inv_no);
                                                                    if (tritemcost > 0)
                                                                    {
                                                                        string trcost = tritemcost.ToString("F3", CultureInfo.InvariantCulture);
                                                                        decimal aftotunit = invdata.totalunit - tritemcost;
                                                                        string aftotun = aftotunit.ToString("F3", CultureInfo.InvariantCulture);
                                                                        writer.WriteLine(PrintSunLine("BDPOI1002", trcost.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                        writer.WriteLine(PrintSunLine(Acccode, aftotun.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                    }
                                                                    else
                                                                    {
                                                                        writer.WriteLine(PrintSunLine(Acccode, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                    }

                                                                }
                                                                else
                                                                {
                                                                    writer.WriteLine(PrintSunLine(_nbtacc, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                }
                                                               
                                                            }
                                                        }

                                                    }
                                                    else if (((invdata.isdiliver == "0" && gnr.ledg_desc == "CASH") || (invdata.sah_man_ref.Contains("SRN") == false && gnr.ledg_desc == "CASH")) && invdata.sah_anal_4 != "DISC")
                                                    {
                                                        //cancel srn 2016-11-19
                                                        string refNo1 = invdata.sah_inv_no.Replace(@"-", string.Empty);
                                                        refNo1 = refNo1.Replace(@"REV", string.Empty);
                                                        int srnlenght = refNo1.Length;
                                                        if (srnlenght > 10)
                                                        {
                                                            // refNo = refNo1.Substring(0, 10);
                                                            refNo = refNo1.Substring(0, 3) + "REV" + refNo1.Substring(srnlenght - 4);
                                                        }
                                                        else
                                                        {
                                                            refNo = refNo1;
                                                        }
                                                        cusname = invdata.sah_ref_doc.ToString();
                                                        jnaltype = "SRTN1";
                                                        if (Session["UserCompanyCode"].ToString() == "SAE")
                                                        {
                                                            jnaltype = "INVSAER";
                                                        }
                                                        string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "REVCASH");
                                                        if (_nbtacc == "")
                                                        {
                                                            _nbtacc = Acccode;
                                                        }
                                                        if (invdata.totalunit != 0)
                                                        {
                                                            if (Session["UserCompanyCode"].ToString() == "BDL")
                                                            {
                                                                decimal tritemcost = CHNLSVC.Financial.GetBDLTransINV(invdata.sah_inv_no);
                                                                if (tritemcost > 0)
                                                                {
                                                                    string trcost = tritemcost.ToString("F3", CultureInfo.InvariantCulture);
                                                                    decimal aftotunit = invdata.totalunit - tritemcost;
                                                                    string aftotun = aftotunit.ToString("F3", CultureInfo.InvariantCulture);
                                                                    writer.WriteLine(PrintSunLine("BDPOI1002", trcost.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                    writer.WriteLine(PrintSunLine(Acccode, aftotun.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                }
                                                                else
                                                                {
                                                                    writer.WriteLine(PrintSunLine(Acccode, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                }

                                                            }
                                                            else
                                                            {
                                                                writer.WriteLine(PrintSunLine(_nbtacc, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                            }
                                                           
                                                        }
                                                    }
                                                    else if (invdata.sah_direct == 0 && invdata.sah_anal_4 == "DISC")
                                                    {
                                                        jnaltype = "REC1";
                                                        cusname = invdata.sah_ref_doc.ToString();

                                                        string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "DISC");
                                                        if (_nbtacc == "")
                                                        {
                                                            _nbtacc = Acccode;
                                                        }
                                                        if (invdata.totalunit != 0)
                                                        {
                                                            if (Session["UserCompanyCode"].ToString() == "BDL")
                                                            {
                                                                decimal tritemcost = CHNLSVC.Financial.GetBDLTransINV(invdata.sah_inv_no);
                                                                if (tritemcost > 0)
                                                                {
                                                                    string trcost = tritemcost.ToString("F3", CultureInfo.InvariantCulture);
                                                                    decimal aftotunit = invdata.totalunit - tritemcost;
                                                                    string aftotun = aftotunit.ToString("F3", CultureInfo.InvariantCulture);
                                                                    writer.WriteLine(PrintSunLine("BDPOI1002", trcost.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                    writer.WriteLine(PrintSunLine(Acccode, aftotun.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                }
                                                                else
                                                                {
                                                                    writer.WriteLine(PrintSunLine(Acccode, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                }

                                                            }
                                                            else
                                                            {
                                                                writer.WriteLine(PrintSunLine(_nbtacc, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                            }

                                                           
                                                        }
                                                    }
                                                    else if (invdata.sah_direct == 0 && invdata.sah_inv_no.Contains("INCRN"))
                                                    {
                                                        jnaltype = "SRTN1";
                                                        if (Session["UserCompanyCode"].ToString() == "SAE")
                                                        {
                                                            jnaltype = "INVSAER";
                                                        }
                                                        cusname = invdata.sah_ref_doc.ToString();

                                                        string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "INCRN");
                                                        if (_nbtacc == "")
                                                        {
                                                            _nbtacc = Acccode;
                                                        }
                                                        string[] crwords = invdata.sah_inv_no.Split('-');
                                                        if (crwords.Length > 2)
                                                        {
                                                            refNo = crwords[0] + "CN" + crwords[3];
                                                        }
                                                        else
                                                        {
                                                            refNo = invdata.sah_inv_no;
                                                        }
                                                        if (invdata.totalunit != 0)
                                                        {
                                                            if (Session["UserCompanyCode"].ToString() == "BDL")
                                                            {
                                                                decimal tritemcost = CHNLSVC.Financial.GetBDLTransINV(invdata.sah_inv_no);
                                                                if (tritemcost > 0)
                                                                {
                                                                    string trcost = tritemcost.ToString("F3", CultureInfo.InvariantCulture);
                                                                    decimal aftotunit = invdata.totalunit - tritemcost;
                                                                    string aftotun = aftotunit.ToString("F3", CultureInfo.InvariantCulture);
                                                                    writer.WriteLine(PrintSunLine("BDPOI1002", trcost.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                    writer.WriteLine(PrintSunLine(Acccode, aftotun.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                }
                                                                else
                                                                {
                                                                    writer.WriteLine(PrintSunLine(Acccode, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                }

                                                            }
                                                            else
                                                            {
                                                                writer.WriteLine(PrintSunLine(_nbtacc, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                            }
                                                           
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //cancel srn 2016-11-19
                                                        string refNo1 = invdata.sah_inv_no.Replace(@"-", string.Empty);
                                                        refNo1 = refNo1.Replace(@"REV", string.Empty);
                                                        int srnlenght = refNo1.Length;
                                                        if (srnlenght > 10)
                                                        {
                                                            // refNo = refNo1.Substring(0, 10);
                                                            refNo = refNo1.Substring(0, 3) + "REV" + refNo1.Substring(srnlenght - 4);
                                                        }
                                                        else
                                                        {
                                                            refNo = refNo1;
                                                        }
                                                        cusname = invdata.sah_ref_doc.ToString();
                                                        jnaltype = "SRTN1";
                                                        if (Session["UserCompanyCode"].ToString() == "SAE")
                                                        {
                                                            jnaltype = "INVSAER";
                                                        }
                                                        string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "REV");
                                                        if (_nbtacc == "")
                                                        {
                                                            _nbtacc = Acccode;
                                                        }

                                                        //check ref no 
                                                        //bool isupload6 = CHNLSVC.Financial.CheckSunRef(refNo, invdata.sah_dt.ToString("yyyyMMdd"));
                                                        //if (isupload6)
                                                        //{
                                                        //    continue;
                                                        //}


                                                        if (invdata.totalunit != 0)
                                                        {

                                                            if (Session["UserCompanyCode"].ToString() == "BDL")
                                                            {
                                                                decimal tritemcost = CHNLSVC.Financial.GetBDLTransINV(invdata.sah_inv_no);
                                                                if (tritemcost > 0)
                                                                {
                                                                    string trcost = tritemcost.ToString("F3", CultureInfo.InvariantCulture);
                                                                    decimal aftotunit = invdata.totalunit - tritemcost;
                                                                    string aftotun = aftotunit.ToString("F3", CultureInfo.InvariantCulture);
                                                                    writer.WriteLine(PrintSunLine("BDPOI1002", trcost.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                    writer.WriteLine(PrintSunLine(Acccode, aftotun.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                }
                                                                else
                                                                {
                                                                    writer.WriteLine(PrintSunLine(Acccode, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                                }

                                                            }
                                                            else
                                                            {
                                                                writer.WriteLine(PrintSunLine(_nbtacc, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                            }
                                                            
                                                        }
                                                    }

                                                }
                                                paytpcount++;
                                                DUTY = gnr.ledg_desc;

                                            }


                                        }
                                        if (paytpcount < 2 && DUTY != "DUTY" && paytype != "DEBT")
                                        {
                                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Not Define " + paytype + " Type " + invdata.sah_pc.ToString() + "');", true);
                                            return;
                                        }
                                        if (paytpcount > 3)
                                        {
                                            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Not Define " + paytype + " Type " + invdata.sah_pc.ToString() + "');", true);

                                        }
                                        if (count == 0)
                                        {
                                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Not Define " + paytype + " Type " + invdata.sah_pc.ToString() + "');", true);
                                            return;
                                        }

                                        int effect = CHNLSVC.Financial.UPDATE_INV_HDRENGLOG(invdata.sah_inv_no, 1, Session["UserCompanyCode"].ToString());

                                    }

                                }

                            }

                        }
                    }
                    string _PATH = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "PATHINV");
                    System.IO.File.Copy(@name, _PATH + "inv" + date, true);
                    Session["path"] = "File Name : inv" + date;
                    // System.IO.File.Copy(@"C:/SUN/" + name, "\\\\192.168.1.45\\SUN\\" + name, true);
                }

                #endregion
                #region Reciept Upload
                if (type == "Reciept")
                {
                    FileInfo info = new FileInfo(name2);
                    if (info.Exists || !info.Exists)
                    {
                        using (StreamWriter writer = info.CreateText())
                        {
                            List<SUNRECIEPTHDR> listorder = CHNLSVC.Financial.GetSunRecieptdatanew(Session["UserCompanyCode"].ToString(), pctxt, startdate, enddate, paytypereal);
                            listorder = listorder.GroupBy(l => new { l.sar_receipt_no })
   .Select(cl => new SUNRECIEPTHDR
   {
       sar_receipt_no = cl.First().sar_receipt_no,
       sar_receipt_type = cl.First().sar_receipt_type,
       sar_seq_no = cl.First().sar_seq_no,
       sar_profit_center_cd = cl.First().sar_profit_center_cd,
       sar_debtor_name = cl.First().sar_debtor_name,
       sar_receipt_date = cl.First().sar_receipt_date,
       sar_debtor_cd = cl.First().sar_debtor_cd,
       sard_ref_no = cl.First().sard_ref_no,
       sar_tot_settle_amt = cl.Sum(a => a.sar_tot_settle_amt),
       checkno = cl.First().checkno,
       sar_remarks = cl.First().sar_remarks,
       RecieptType = cl.First().RecieptType,
       sard_inv_no = cl.First().sard_inv_no,
       sard_chq_bank_cd = cl.First().sard_chq_bank_cd,
       sard_chq_branch = cl.First().sard_chq_branch,
       Branchdesc = cl.First().Branchdesc,
       BankdepositeDate = cl.First().BankdepositeDate
   }).ToList();

                            if (listorder != null)
                                listorder = listorder.OrderBy(a => a.sar_receipt_type).ToList();
                            if (listorder != null)
                            {
                                foreach (var invdata in listorder)
                                {
                                    if ((invdata.sar_receipt_no.Contains("SRN") == true || invdata.sar_receipt_no.Contains("ADV") == true) && Session["UserCompanyCode"].ToString() != "AEC")
                                    {
                                    }
                                    else
                                    {
                                        string Cuscode = "";
                                        Acccode = invdata.sard_ref_no.ToString();
                                        if (Acccode == null || Acccode == "")
                                        {
                                            string _defcusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "AALDEFLT");
                                            Acccode = _defcusacc;
                                        }
                                        if (Acccode.Trim() == "CASH" | Acccode.Trim() == "")
                                        {
                                            string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "RECACCCASH");
                                            Acccode = _cusacc;
                                        }

                                        string TOTAmmount = invdata.sar_tot_settle_amt.ToString("F3", CultureInfo.InvariantCulture);
                                        jnaltype = "REC1";
                                        if (invdata.sar_debtor_cd.ToString() != null)
                                            Cuscode = invdata.sar_debtor_cd.ToString();

                                        if (Cuscode.Trim() == "" | Cuscode.Trim() == "CASH")
                                        {
                                            string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "RECCASH");
                                            Cuscode = _cusacc;
                                        }
                                        DateTime transdate = invdata.sar_receipt_date;
                                        string refNo = invdata.sar_receipt_no.ToString();
                                        string cusname = invdata.checkno.ToString();
                                        string pc = invdata.sar_profit_center_cd.ToString();
                                        string seq = "Z"; //invdata.sar_seq_no.ToString();

                                        //svat
                                        if (invdata.sar_receipt_no.Contains("SVAT"))
                                        {
                                            string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "RECSVAT");
                                            Acccode = _cusacc;
                                            jnaltype = "REC1";
                                            seq = "W";
                                        }
                                        if (invdata.sar_receipt_no.Contains("ORC"))
                                        {
                                            cusname = invdata.sar_remarks.ToString();
                                        }
                                        if (invdata.RecieptType == "TNSPT")
                                        {
                                            string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "TNSPT");
                                            Cuscode = _cusacc;
                                        }
                                        if (invdata.RecieptType == "VHREG")
                                        {
                                            string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "VHREG");
                                            Cuscode = _cusacc;
                                        }
                                        string _desc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "DESC" + invdata.RecieptType);
                                        string _chassis = "";
                                        if (_desc == "CHASSIS")
                                        {

                                            if (invdata.sard_inv_no != null && invdata.sard_inv_no != "")
                                            {
                                                _chassis = CHNLSVC.Financial.GetRecChassisNo(invdata.sard_inv_no);
                                                if (_chassis == "")
                                                {
                                                    _chassis = invdata.sard_ref_no;
                                                }
                                            }
                                            else
                                            {
                                                _chassis = invdata.sard_ref_no;
                                            }

                                        }
                                        if (invdata.RecieptType == "RTCHQ")
                                        {
                                            int reflenght = cusname.Length;
                                            if (reflenght >= 6)
                                            {
                                                refNo = cusname.Substring(reflenght - 6);
                                            }
                                            else
                                            {
                                                refNo = cusname;
                                            }

                                            writer.WriteLine(PrintSunLine(Cuscode, TOTAmmount.ToString(), "D", transdate, "CHQ-" + refNo, refNo, pc, seq, "RTN1", execcode));
                                            writer.WriteLine(PrintSunLine(Acccode, TOTAmmount.ToString(), "C", transdate, "CHQ-" + refNo, refNo, pc, seq, "RTN1", execcode));
                                        }
                                        else
                                        {
                                            string _cusname2 = cusname;
                                            if (_desc == "CHASSIS")
                                            {
                                                _cusname2 = _chassis;
                                            }
                                            else
                                            {
                                                if (Session["UserCompanyCode"].ToString() == "AAL")
                                                {
                                                    _cusname2 = invdata.sard_inv_no;
                                                }
                                            }
                                            #region AAL
                                            if (Session["UserCompanyCode"].ToString() == "AAL")
                                            {
                                                if (invdata.sar_receipt_type == "CASH")
                                                {
                                                    //cusname = invdata.sar_receipt_date.Date.ToString("dd/MMM/yyyy");
                                                    //_cusname2 = invdata.sar_receipt_date.Date.ToString("dd/MMM/yyyy");
                                                    cusname = invdata.checkno ;
                                                    _cusname2 = invdata.checkno;

                                                }
                                                else if (invdata.sar_receipt_type == "CHEQUE")
                                                {
                                                    if (invdata.checkno.Length > 6)
                                                    {
                                                        invdata.checkno = invdata.checkno.Substring((invdata.checkno.Length - 6));
                                                        cusname = invdata.checkno + invdata.sard_chq_bank_cd + invdata.Branchdesc;
                                                        _cusname2 = invdata.checkno + invdata.sard_chq_bank_cd + invdata.Branchdesc;
                                                    }
                                                }
                                                else if (invdata.sar_receipt_type == "BANK_SLIP")
                                                {
                                                    cusname = invdata.BankdepositeDate.ToString("dd/MMM/yyyy");
                                                    _cusname2 = invdata.BankdepositeDate.ToString("dd/MMM/yyyy");
                                                    //cusname = invdata.checkno;
                                                    //_cusname2 = invdata.checkno;
                                                }
                                                else
                                                {
                                                    cusname = invdata.sar_receipt_date.Date.ToString("dd/MMM/yyyy");
                                                    _cusname2 = invdata.sar_receipt_date.Date.ToString("dd/MMM/yyyy");
                                                }


                                            }
                                            #endregion

                                            #region 44A
                                            if (invdata.sar_profit_center_cd == "44A")
                                            {
                                                if (invdata.sar_receipt_type == "CASH")
                                                {
                                                    Acccode = "ABBCA5020";
                                                }
                                                else if (invdata.sar_receipt_type == "CHEQUE")
                                                {
                                                    Acccode = "ABBCA5019";
                                                }
                                                else if (invdata.sar_receipt_type == "CRCD")
                                                {
                                                    Acccode = "ABDR51007";
                                                }
                                                else if (invdata.sar_receipt_type == "GVO" || invdata.sar_receipt_type == "GV")
                                                {
                                                    Acccode = "ABBCL1013";
                                                }
                                                else if (invdata.sar_receipt_type == "ADVAN")
                                                {
                                                    Acccode = "ABBCL1012";
                                                }
                                                else if (invdata.sar_receipt_type == "CRNOTE")
                                                {
                                                    Acccode = "ABBCL1041";
                                                }
                                            }
                                            #endregion

                                            if (invdata.RecieptType == "ADREF")
                                            {
                                                writer.WriteLine(PrintSunLine(Cuscode, TOTAmmount.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                writer.WriteLine(PrintSunLine(Acccode, TOTAmmount.ToString(), "C", transdate, refNo, _cusname2, pc, seq, jnaltype, execcode));
                                            }
                                            else
                                            {

                                                writer.WriteLine(PrintSunLine(Cuscode, TOTAmmount.ToString(), "C", transdate, refNo, _cusname2, pc, seq, jnaltype, execcode));
                                                writer.WriteLine(PrintSunLine(Acccode, TOTAmmount.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                            }

                                        }



                                        int effect = CHNLSVC.Financial.UPDATE_RECIEPT_HDRENGLOG(invdata.sar_receipt_no, 1, Session["UserCompanyCode"].ToString());
                                    }
                                }
                            }
                        }
                    }
                    string _PATH = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "PATHINV");
                    System.IO.File.Copy(@name2, _PATH + "rec" + date, true);
                    Session["path"] = "File Name : rec" + date;
                }
                #endregion
                #region Local Perchs Upload
                if (type == "LCLPC")
                {
                    foreach (GridViewRow row in gvgrnlist.Rows)
                    {
                        CheckBox chkselectgrn = (CheckBox)row.FindControl("chkselectgrn");
                        if (chkselectgrn != null && chkselectgrn.Checked)
                        {
                            if (grntxt != "")
                            {
                                grntxt = grntxt + ",";
                            }
                            Label lbgrnno = (Label)row.FindControl("lbgrnno");
                            string com_cd = lbgrnno.Text;
                            grntxt = grntxt + com_cd;

                        }
                    }


                    FileInfo info = new FileInfo(name3);
                    if (info.Exists || !info.Exists)
                    {
                        using (StreamWriter writer = info.CreateText())
                        {

                            List<LocPurSun> grnsunlist = CHNLSVC.Financial.SP_GetLocPerSUN(Session["UserCompanyCode"].ToString(), grntxt);
                            grnsunlist = grnsunlist.GroupBy(l => new { l.itb_doc_no, l.itb_itm_cd })
 .Select(cl => new LocPurSun
 {
     itb_doc_no = cl.First().itb_doc_no,
    // ith_remarks = cl.First().ith_remarks, //Wimal 05/Jan/2019 
     ith_seq_no = cl.First().ith_seq_no,
     ith_doc_date = cl.First().ith_doc_date,
     ith_bus_entity = cl.First().ith_bus_entity,
     mbe_name = cl.First().mbe_name,
     itb_qty = cl.Sum(a => a.itb_qty),
     pod_unit_price = cl.First().pod_unit_price,
     pod_dis_amt = cl.Sum(a => a.pod_dis_amt),
     pod_line_tax = cl.Sum(a => a.pod_line_tax),
     invoiceno = cl.First().invoiceno,
     itb_itm_cd = cl.First().itb_itm_cd,
     total = cl.Sum(a => a.total)
 }).ToList();
                            grnsunlist = grnsunlist.GroupBy(l => new { l.itb_doc_no })
.Select(cl => new LocPurSun
{
    itb_doc_no = cl.First().itb_doc_no,
   // ith_remarks = cl.First().ith_remarks, //Wimal 05/Jan/2019 
    ith_seq_no = cl.First().ith_seq_no,
    ith_doc_date = cl.First().ith_doc_date,
    ith_bus_entity = cl.First().ith_bus_entity,
    mbe_name = cl.First().mbe_name,
    itb_qty = cl.Sum(a => a.itb_qty),
    pod_unit_price = cl.First().pod_unit_price,
    pod_dis_amt = cl.Sum(a => a.pod_dis_amt),
    pod_line_tax = cl.Sum(a => a.pod_line_tax),
    invoiceno = cl.First().invoiceno,
    itb_itm_cd = cl.First().itb_itm_cd,
    total = cl.Sum(a => a.total)
}).ToList();

                            grnsunlist = grnsunlist.OrderBy(a => a.pod_line_tax).ToList();
                            foreach (var newlclist in editlist)
                            {
                                foreach (var lclist in grnsunlist)
                                {
                                    if (lclist.itb_doc_no == newlclist.itb_doc_no)
                                    {
                                        lclist.mbe_name = newlclist.mbe_name;
                                        lclist.ith_doc_date = newlclist.ith_doc_date;
                                        lclist.invoiceno = newlclist.invoiceno;
                                    }
                                }

                            }

                            if (grnsunlist != null)
                            {
                                foreach (var invdata in grnsunlist)
                                {

                                    string Cuscode = "";
                                    string AmmAcccode = "ABBCA1014";
                                    string VatAccode = "ABCR71019";

                                    if (invdata.ith_doc_date >= Convert.ToDateTime("01/Mar/2018"))
                                    {
                                        AmmAcccode = "ABCR1I081";
                                    }

                                    decimal ammount = invdata.total;
                                    decimal vat = invdata.pod_line_tax;
                                    decimal disc = invdata.pod_dis_amt;
                                    ammount = ammount - disc;
                                    string ammounts = ammount.ToString("F3", CultureInfo.InvariantCulture);
                                    string VATSS = vat.ToString("F3", CultureInfo.InvariantCulture);

                                    decimal totamm = (ammount + vat);
                                    string totammss = totamm.ToString("F3", CultureInfo.InvariantCulture);

                                    if (invdata.ith_bus_entity.ToString() != null)
                                        Cuscode = invdata.ith_bus_entity.ToString();
                                    DateTime transdate = invdata.ith_doc_date;
                                    string refNo = invdata.invoiceno.ToString();
                                    string cusname = invdata.itb_doc_no.ToString();
                                    //Wimal 05/Jan/2019 
                                    if (Session["UserCompanyCode"].ToString() == "AEC")
                                    {
                                        InventoryHeader _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(invdata.itb_doc_no.ToString());
                                        cusname = _invHdr.Ith_remarks.ToString();
                                        //cusname = invdata.ith_remarks.ToString();
                                        refNo = refNo.Replace("BILL NO", "");
                                    }
                                    string pc = "N/A";
                                    string seq = "Z";//invdata.ith_seq_no.ToString();

                                    if (Session["UserCompanyCode"].ToString() == "BDL")
                                    {
                                        AmmAcccode = "BDBCA1002";
                                        VatAccode = "BDBCA1002";
                                        seq = "E";
                                    }
                                    if (Session["UserCompanyCode"].ToString() == "AEC")
                                    {
                                        AmmAcccode = "ABBCA3017";
                                        VatAccode = "ABCR71013";
                                        seq = "E";
                                    }
                                    if (ammount > 0)
                                    {
                                        writer.WriteLine(PrintSunLine(AmmAcccode, ammounts.ToString(), "D", transdate, refNo, cusname, pc, seq, "LPCR", execcode));
                                    }
                                    if (vat > 0)
                                    {
                                        writer.WriteLine(PrintSunLine(VatAccode, VATSS.ToString(), "D", transdate, refNo, cusname, pc, seq, "LPCR", execcode));
                                    }
                                    if (totamm > 0)
                                    {
                                        writer.WriteLine(PrintSunLine(Cuscode, totammss.ToString(), "C", transdate, refNo, cusname, pc, seq, "LPCR", execcode));
                                    }

                                    if (Session["UserCompanyCode"].ToString() == "AEC")
                                    {
                                        CHNLSVC.Financial.UPDATE_LOCPCH_HDRENGLOG(invdata.itb_doc_no.ToString(), 5, Session["UserCompanyCode"].ToString());
                                    }
                                    else
                                    {
                                        CHNLSVC.Financial.UPDATE_LOCPCH_HDRENGLOG(cusname, 5, Session["UserCompanyCode"].ToString());
                                    }
                                    
                                }
                            }
                        }
                    }
                    string _PATH = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "PATHLOPR");
                    System.IO.File.Copy(@name3, _PATH + "per" + _systemuser.Se_SUN_ID + date, true);
                    Session["path"] = "D:Sun-SUN426-PER-per" + date;
                }
                #endregion
                #region Credit Note Upload
                if (type == "CRNT")
                {
                    FileInfo info = new FileInfo(name4);
                    if (info.Exists || !info.Exists)
                    {
                        using (StreamWriter writer = info.CreateText())
                        {
                            List<Suncreditnote> listorder = CHNLSVC.Financial.SP_SUN_CREDNOTE(Session["UserCompanyCode"].ToString(), startdate, enddate, pctxt);

                            if (listorder != null)
                            {
                                foreach (var invdata in listorder)
                                {
                                    if ((invdata.cr_amt != 0 && invdata.ith_doc_no.Contains("SRN") == false) && (invdata.job_no.Contains("RIT1A") == false))
                                    {
                                        string Cuscode = "";
                                        Acccode = "ABPSA1008"; //invdata.grah_fuc_cd.ToString();
                                        string TOTAmmount = invdata.cr_amt.ToString("F3", CultureInfo.InvariantCulture);
                                        jnaltype = "SRTN1";
                                        if (invdata.sjb_b_cust_cd.ToString() != null)
                                            Cuscode = invdata.sah_cus_cd.ToString();
                                        DateTime transdate = invdata.ith_doc_date;
                                        string refNo = invdata.ith_doc_no.ToString();
                                        refNo = refNo.Replace(@"-", string.Empty);
                                        string cusname = invdata.job_no.ToString();
                                        string pc = invdata.sah_pc.ToString();
                                        string seq = invdata.tax_cd.ToString(); //invdata.sar_seq_no.ToString();

                                        if (invdata.ith_direct == 1)
                                        {
                                            writer.WriteLine(PrintSunLine(Cuscode, TOTAmmount.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                            writer.WriteLine(PrintSunLine(Acccode, TOTAmmount.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));

                                        }
                                        else
                                        {
                                            writer.WriteLine(PrintSunLine(Cuscode, TOTAmmount.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                            writer.WriteLine(PrintSunLine(Acccode, TOTAmmount.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));

                                        }
                                    }

                                    //if (chkblock.Checked == true)
                                    //{
                                    //    int effect = CHNLSVC.Financial.UPDATE_INV_HDRENGLOG(invdata.ith_doc_no, 1, Session["UserCompanyCode"].ToString());
                                    //}

                                }
                            }
                        }
                    }
                    string _PATH = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "PATHINV");
                    System.IO.File.Copy(@name4, _PATH + "cred" + date, true);
                    Session["path"] = "File Name :" + "cred" + date;
                }
                #endregion
                #region Return Check Upload

                #endregion

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
            }
        }

        public void GetFile_New(DateTime startdate, DateTime enddate, string type, string paytypereal, List<string> uploadRef)
        {
            // try
            //{
            string execcode = "N/A";
            var copypath = System.Web.Configuration.WebConfigurationManager.AppSettings["SUNUploadPath"].ToString();
            var copypathper = System.Web.Configuration.WebConfigurationManager.AppSettings["SUNUploadPathper"].ToString();
            Session["path"] = "";
            string name = "C:/SUN" + "/SCMinv.txt";
            string name2 = "C:/SUN" + "/SCMREC.txt";
            string name3 = "C:/SUN" + "/SCMPER.txt";
            string name4 = "C:/SUN" + "/CRED.txt";
            string Acccode = "";
            string jnaltype = "";
            int count = 0;
            string defpc = Session["UserDefProf"].ToString();
            //string paytypereal = Dddivtype.SelectedValue.ToString();
            string pctxt = "";
            string grntxt = "";
            string ISTAX = "";
            string date = enddate.Date.ToString("dd");
            date = Session["UserCompanyCode"].ToString() + date + ".txt";
            Int32 isnum = 0;
            //foreach (GridViewRow row in gvpclist.Rows)
            //{
            //    CheckBox chkselect = (CheckBox)row.FindControl("chkselect");
            //    if (chkselect != null && chkselect.Checked)
            //    {
            //        if (pctxt != "")
            //        {
            //            pctxt = pctxt + ",";
            //        }
            //        Label lbpccd = (Label)row.FindControl("lbpccd");
            //        string com_cd = lbpccd.Text;
            //        pctxt = pctxt + com_cd;

            //    }
            //}

            foreach (string listPC in uploadRef)
            {
                if (pctxt != "")
                {
                    pctxt = pctxt + ",";
                }
                pctxt = pctxt + listPC;

            }


            #region Invoice Upload
            if (type == "Invoice")
            {
                FileInfo info = new FileInfo(name);
                if (info.Exists || !info.Exists)
                {
                    using (StreamWriter writer = info.CreateText())
                    {
                        List<SUN_JURNAL> gnrlist = CHNLSVC.Financial.GetSunJurnalnew(Session["UserCompanyCode"].ToString());
                        gnrlist = gnrlist.OrderBy(a => a.ledg_sales_tp).ToList();
                        List<SUNINVHDR> listorder = CHNLSVC.Financial.GetSunInvdatanew(Session["UserCompanyCode"].ToString(), pctxt, startdate, enddate);
                        if (listorder == null) return;
                        if (Session["UserCompanyCode"].ToString() == "AAL" && listorder != null && listorder.Count > 0)
                        {
                            listorder = listorder.Where(a => a.sah_inv_tp != "CRED").ToList();
                        }

                        List<SUNINVHDR> LIORDERSUM = listorder.GroupBy(l => new { l.sah_inv_no })
.Select(cl => new SUNINVHDR
{
    sah_inv_no = cl.First().sah_inv_no,
    sah_inv_tp = cl.First().sah_inv_tp,
    sah_inv_sub_tp = cl.First().sah_inv_sub_tp,
    sah_tp = cl.First().sah_tp,
    total = cl.First().total,
    totalunit = cl.Sum(A => A.totalunit),
    taxtotal = cl.Sum(A => A.taxtotal),
    sah_seq_no = cl.First().sah_seq_no,
    sah_pc = cl.First().sah_pc,
    sah_cus_name = cl.First().sah_cus_name,
    sah_dt = cl.First().sah_dt,
    sah_cus_cd = cl.First().sah_cus_cd,
    sah_direct = cl.First().sah_direct,
    sah_man_ref = cl.First().sah_man_ref,
    tax_cd = cl.First().tax_cd,
    isdiliver = cl.First().isdiliver,
    sah_ref_doc = cl.First().sah_ref_doc,
    sah_sales_ex_cd = cl.First().sah_sales_ex_cd,
    sah_is_svat = cl.First().sah_is_svat,
    svatcd = cl.First().svatcd,
    EPF = cl.First().EPF,
    CODE = cl.First().CODE,
    RealTotalwithtax = cl.Sum(a => a.RealTotalwithtax),
    NBTValue = cl.Sum(a => a.NBTValue),
    sah_tax_exempted = cl.First().sah_tax_exempted,
    sah_anal_4 = cl.First().sah_anal_4
}).ToList();
                        LIORDERSUM = LIORDERSUM.OrderBy(a => a.sah_inv_no).ToList();

                        listorder = listorder.GroupBy(l => new { l.sah_inv_no, l.tax_cd })
.Select(cl => new SUNINVHDR
{
    sah_inv_no = cl.First().sah_inv_no,
    sah_inv_tp = cl.First().sah_inv_tp,
    sah_inv_sub_tp = cl.First().sah_inv_sub_tp,
    sah_tp = cl.First().sah_tp,
    total = cl.First().total,
    totalunit = cl.Sum(A => A.totalunit),
    taxtotal = cl.Sum(A => A.taxtotal),
    sah_seq_no = cl.First().sah_seq_no,
    sah_pc = cl.First().sah_pc,
    sah_cus_name = cl.First().sah_cus_name,
    sah_dt = cl.First().sah_dt,
    sah_cus_cd = cl.First().sah_cus_cd,
    sah_direct = cl.First().sah_direct,
    sah_man_ref = cl.First().sah_man_ref,
    tax_cd = cl.First().tax_cd,
    isdiliver = cl.First().isdiliver,
    sah_ref_doc = cl.First().sah_ref_doc,
    sah_sales_ex_cd = cl.First().sah_sales_ex_cd,
    sah_is_svat = cl.First().sah_is_svat,
    svatcd = cl.First().svatcd,
    EPF = cl.First().EPF,
    CODE = cl.First().CODE,
    RealTotalwithtax = cl.Sum(a => a.RealTotalwithtax),
    NBTValue = cl.Sum(a => a.NBTValue),
    sah_tax_exempted = cl.First().sah_tax_exempted,
    sah_anal_4 = cl.First().sah_anal_4
}).ToList();
                        listorder = listorder.OrderBy(a => a.sah_inv_no).ToList();
                        string tempinvno = "";

                        if (listorder != null)
                            listorder = listorder.OrderBy(a => a.sah_inv_tp).ToList();
                        if (listorder != null && gnrlist != null)
                        {
                            foreach (var invdata in listorder)
                            {                              
                                if (invdata.totalunit != 0)
                                {
                                    //Commented by Wimal @ 19/07/2018 to upload
                                    //customer@char
                                    invdata.sah_cus_name = invdata.sah_cus_name.Replace(@"–", string.Empty);
                                    invdata.sah_cus_name = invdata.sah_cus_name.Replace(@"’", string.Empty);
                                    invdata.sah_cus_name = invdata.sah_cus_name.Replace(@"-", string.Empty);
                                    invdata.sah_cus_name = invdata.sah_cus_name.Replace(@"'", string.Empty);

                                    execcode = "N/A";
                                    if (invdata.sah_sales_ex_cd != null) execcode = invdata.sah_sales_ex_cd.ToString();
                                    //check exc code
                                    int n;
                                    bool isNumeric = int.TryParse(execcode, out n);
                                    if (isNumeric == true)
                                    {
                                        if (invdata.CODE != null)
                                        {
                                            isNumeric = int.TryParse(invdata.CODE, out n);
                                            if (isNumeric == true)
                                            {
                                                if (invdata.EPF != null)
                                                    execcode = invdata.EPF.ToString();
                                            }
                                            else
                                            {
                                                execcode = invdata.CODE.ToString();
                                            }
                                        }

                                    }


                                    int paytpcount = 0;
                                    string DUTY = "";
                                    string paytype = "1";
                                    if (invdata.sah_inv_tp.ToString() != null)
                                        paytype = invdata.sah_inv_tp.Trim().ToString();


                                    foreach (var gnr in gnrlist)
                                    {
                                        if (gnr.ledg_is_dealer == null)
                                        {
                                            gnr.ledg_is_dealer = "";
                                        }
                                        ISTAX = gnr.ledg_is_dealer;
                                        string gnrtype = "2";
                                        if (gnr.ledg_sales_tp != null)
                                            gnrtype = gnr.ledg_sales_tp.Trim().ToString();

                                        if (paytype == gnrtype)
                                            count = 1;

                                        if (paytype == gnrtype && gnr.ledg_sub_tp == "SA" && gnr.ledg_acc_tp == "DR" && gnr.ledg_pc == invdata.sah_pc && (gnr.ledg_desc == "CASH" | gnr.ledg_desc == "CRED" | gnr.ledg_desc == "DUTY" | gnr.ledg_desc == "TOTO" | gnr.ledg_desc == "UNITY"))
                                        {
                                            decimal alltotal = 0;
                                            if (tempinvno != invdata.sah_inv_no)
                                                alltotal = invdata.total; //Convert.ToDecimal(LIORDERSUM.Where(a => a.sah_inv_no == invdata.sah_inv_no).First().totalunit) + Convert.ToDecimal(LIORDERSUM.Where(a => a.sah_inv_no == invdata.sah_inv_no).First().taxtotal);

                                            //REVERSISSUE
                                            tempinvno = invdata.sah_inv_no;
                                            decimal TOTNEW = Convert.ToDecimal(invdata.totalunit) + Convert.ToDecimal(invdata.taxtotal);
                                            string TOTNEW2 = alltotal.ToString("F3", CultureInfo.InvariantCulture);
                                            string Cuscode = "";
                                            Acccode = gnr.ledg_acc_cd.ToString();
                                            string TOTAmmount = invdata.total.ToString("F3", CultureInfo.InvariantCulture);
                                            string TAXAmmount = invdata.taxtotal.ToString("F3", CultureInfo.InvariantCulture);
                                            string TOTunit = invdata.totalunit.ToString("F3", CultureInfo.InvariantCulture);


                                            if (invdata.sah_cus_cd != null)
                                                Cuscode = invdata.sah_cus_cd.ToString();
                                            DateTime transdate = invdata.sah_dt;
                                            string refNo = invdata.sah_inv_no.ToString();
                                            string cusname = invdata.sah_cus_name.ToString().Trim();
                                            string pc = invdata.sah_pc.ToString();
                                            string seq = invdata.tax_cd.ToString();
                                            jnaltype = gnr.ledg_jnl_tp.ToString();




                                            if ((Cuscode.Contains("CONT") || Cuscode.Contains("CASH")))
                                            {
                                                string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "CONTCUS");
                                                Cuscode = _cusacc;
                                            }
                                            //for AAL
                                            if (gnr.ledg_acc_cd == "BANK")
                                            {
                                                string _recaccno = CHNLSVC.Financial.GetRecAccount(invdata.sah_inv_no);
                                                if (_recaccno == "")
                                                {
                                                    _recaccno = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "CONTCUS");
                                                }
                                                Cuscode = _recaccno;
                                            }

                                            if (seq == "" | seq == "M" | seq == "N") seq = "O";
                                            if (pc == "103")
                                            {
                                                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Pleace Check Customer Code in INV : " + refNo + " CODE: " + Cuscode +"Pay Type " + paytype+ " PC :"+pc+"');", true);
                                                //return;
                                                string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "103CUS");
                                                Cuscode = _cusacc;
                                            }
                                            if ((Cuscode.Contains("CONT") | (Cuscode.Contains("CASH"))) && pc == "44A")
                                            {
                                                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Pleace Check Customer Code in INV : " + refNo + " CODE: " + Cuscode +"Pay Type " + paytype+ " PC :"+pc+"');", true);
                                                //return;
                                                string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "44CONTCUS");
                                                Cuscode = _cusacc;
                                            }
                                            if (gnr.ledg_desc == "UNITY")
                                            {
                                                string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "UNITYCUS");
                                                Cuscode = _cusacc;
                                            }
                                            //split ref

                                            //removed by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                            /*string[] words = refNo.Split('-');                                           
                                            if (words.Length > 2)
                                            {
                                                if (words[0] == "103" || words[0] == "41")
                                                {
                                                    refNo = words[0] + words[1];
                                                    if (refNo.Length > 5)
                                                    {
                                                        refNo = refNo.Substring(0, 5);
                                                    }
                                                    refNo = refNo + words[2];
                                                    if (refNo.Length > 10)
                                                    {
                                                        refNo = refNo.Substring(0, 10);
                                                    }
                                                }
                                                else if (words[0] == "44A" || words[0] == "129" || words[0] == "181" || words[0] == "89" || words[0] == "144" || words[0] == "194" || words[0] == "183" || words[0] == "37" || words[0] == "122" || words[0] == "76" || Int32.TryParse(words[0], out isnum) == true)
                                                {
                                                    if (invdata.sah_inv_no.Contains("INDBN"))
                                                    {
                                                        refNo = words[1] + words[3];
                                                    }
                                                    else
                                                    {
                                                        refNo = words[1] + words[2];
                                                        if (refNo.Length > 10)
                                                        {
                                                            refNo = refNo.Substring(0, 10);
                                                        }
                                                    }


                                                }
                                                else
                                                {
                                                    refNo = words[1] + words[2] + words[0];

                                                }

                                            }*/
                                            //Change Ref
                                            //if (refNo.Contains("-INREV"))
                                            //{
                                            //    //refNo = invdata.sah_ref_doc.Substring(0, 4) + invdata.sah_inv_no;
                                            //    //if (refNo.Length > 10)
                                            //    //{
                                            //    //    refNo ="Z"+ refNo.Substring(0, 4) + refNo.Substring(refNo.Length - 6);

                                            //    //}
                                            //    refNo = "Z" + invdata.sah_ref_doc.ToString();
                                            //}
                                            if (invdata.sah_direct == 0)
                                            {
                                                int invlenght = invdata.sah_inv_no.Length;
                                                //refNo = invdata.sah_ref_doc.ToString(); //removed by Wimal @ 17/08/2018 To send reversal number (currently send invoice ref number)
                                                refNo = invdata.sah_inv_no.ToString();
                                                if (invdata.sah_anal_4 == "DISC")
                                                {
                                                    refNo = invdata.sah_inv_no.ToString();
                                                }

                                                //removed by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                                /*string[] words2 = refNo.Split('-');
                                                if (words2.Length == 2)
                                                {
                                                    if (invdata.sah_anal_4 == "DISC")
                                                    {
                                                        refNo = invdata.sah_pc.ToString() + words2[1];
                                                    }
                                                    else
                                                    {
                                                        refNo = "Z" + words2[0] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                    }
                                                }
                                                else if (words2.Length == 3)
                                                {
                                                    if (invdata.sah_anal_4 == "DISC")
                                                    {
                                                        refNo = invdata.sah_pc.ToString() + words2[2];
                                                    }
                                                    else
                                                    {
                                                        if (words2[0] == "103")
                                                        {
                                                            refNo = "Z" + words2[0] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                        }
                                                        else if (words2[0] == "44A" || words2[0] == "129" || words2[0] == "181" || words[0] == "89" || words[0] == "144" || words[0] == "194" || words[0] == "183" || words[0] == "37" || words[0] == "122" || words[0] == "76" || Int32.TryParse(words[0], out isnum) == true)
                                                        {
                                                            refNo = "Z" + words2[1] + words2[2];
                                                        }
                                                        else
                                                        {
                                                            refNo = "Z" + words2[1] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                        }

                                                    }

                                                }
                                                else
                                                {
                                                    if (invdata.sah_anal_4 == "DISC")
                                                    {
                                                        refNo = invdata.sah_pc.ToString() + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                    }
                                                    else
                                                    {
                                                        refNo = "Z" + refNo.Substring(0, 4) + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                    }

                                                }*/
                                            }
                                            //removed by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                            /*if (refNo.Length > 10)
                                            {
                                                refNo = refNo.Replace(@"-", string.Empty);
                                            }*/

                                            if (invdata.sah_is_svat == 1) seq = invdata.svatcd.ToString();
                                            if (jnaltype == "") jnaltype = "INV1";
                                            if (gnr.ledg_desc == "DUTY")
                                            {
                                                jnaltype = "INV8";
                                                seq = "O";
                                            }
                                            if (invdata.sah_tax_exempted == 1)
                                            {
                                                seq = "E";
                                            }

                                            ////check is discounted
                                            ////check ref no 
                                            //bool isupload = CHNLSVC.Financial.CheckSunRef(refNo, invdata.sah_dt.ToString("yyyyMMdd"));
                                            //if (isupload)
                                            //{
                                            //    continue;
                                            //}



                                            if (invdata.sah_direct == 1)
                                            {
                                                string _drdesc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "AALCRDESC");
                                                if (_drdesc == gnr.ledg_desc)
                                                {
                                                    cusname = invdata.sah_anal_4;
                                                }
                                                string _drdesccss = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "AALCRDESCCS");
                                                if (_drdesccss == gnr.ledg_desc && gnr.ledg_pc != "502")
                                                {
                                                    List<SunAALRec> _sunref = CHNLSVC.Financial.GET_SUNRECREF(invdata.sah_inv_no);
                                                    if (_sunref != null && _sunref.Count > 0)
                                                    {
                                                        decimal _totsunrec = _sunref.Sum(a => a.sard_settle_amt);
                                                        if (Math.Abs(_totsunrec - Convert.ToDecimal(TOTNEW2)) > 1)
                                                        {
                                                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Check Invoice No :" + invdata.sah_inv_no + " Settlement Details!" + "');", true);
                                                            return;
                                                        }

                                                        foreach (var _list in _sunref)
                                                        {
                                                            string TOTNEW3 = _list.sard_settle_amt.ToString("F3", CultureInfo.InvariantCulture);
                                                            PrintSunLine(Cuscode, TOTNEW3.ToString(), "D", transdate, refNo, _list.sard_ref_no, pc, seq, jnaltype, execcode);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (alltotal != 0)
                                                            PrintSunLine(Cuscode, TOTNEW2.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                                    }
                                                }
                                                else
                                                {
                                                    if (alltotal != 0)
                                                        PrintSunLine(Cuscode, TOTNEW2.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                                }
                                            }
                                            else if (invdata.sah_direct == 0 && (gnr.ledg_desc == "TOTO" | gnr.ledg_desc == "UNITY"))
                                            {
                                                jnaltype = "SRTN1";
                                                cusname = invdata.sah_man_ref.ToString();
                                                if (alltotal != 0)
                                                    PrintSunLine("ABBCL1041", TOTNEW2.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                            }
                                            else if (invdata.sah_direct == 0 && invdata.sah_anal_4 == "DISC")
                                            {
                                                jnaltype = "REC1";
                                                cusname = invdata.sah_ref_doc.ToString();
                                                if (alltotal != 0)
                                                    PrintSunLine(Cuscode, TOTNEW2.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                            }
                                            else
                                            {

                                                if (invdata.isdiliver == "0" && invdata.sah_man_ref.Contains("SRN") == false && invdata.sah_inv_no.Contains("INCRN") == false)
                                                {
                                                    //removed by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                                    //cancel srn 2016-11-19
                                                    /*string refNo1 = invdata.sah_inv_no.Replace(@"-", string.Empty);
                                                    refNo1 = refNo1.Replace(@"REV", string.Empty);                                                 
                                                   
                                                    int srnlenght = refNo1.Length;
                                                    if (srnlenght > 10)
                                                    {
                                                        //  refNo = refNo1.Substring(0, 10);
                                                        refNo = refNo1.Substring(0, 3) + "REV" + refNo1.Substring(srnlenght - 4);
                                                    }
                                                    else
                                                    {
                                                        refNo = refNo1;
                                                    }*/
                                                    //added by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                                    refNo = invdata.sah_inv_no;
                                                    cusname = invdata.sah_ref_doc.ToString();
                                                }
                                                else
                                                {




                                                    cusname = invdata.sah_man_ref.ToString();
                                                    if (cusname == "" || cusname == "N/A" || (cusname.Contains("SRN") == false))
                                                    {
                                                        cusname = invdata.sah_ref_doc.ToString();
                                                    }
                                                    if (invdata.sah_direct == 0 && invdata.sah_inv_no.Contains("INCRN"))
                                                    {
                                                        cusname = invdata.sah_ref_doc.ToString();
                                                        //removed by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                                        /*
                                                        string[] crwords = invdata.sah_inv_no.Split('-');
                                                        if (crwords.Length > 2)
                                                        {
                                                            refNo = crwords[0] + "CN" + crwords[3];
                                                        }
                                                        else
                                                        {
                                                            refNo = invdata.sah_inv_no;
                                                        }*/
                                                        //added by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                                        refNo = invdata.sah_inv_no;
                                                    }
                                                }

                                                ////check ref no 
                                                //bool isupload2 = CHNLSVC.Financial.CheckSunRef(refNo, invdata.sah_dt.ToString("yyyyMMdd"));
                                                //if (isupload2)
                                                //{
                                                //    continue;
                                                //}


                                                jnaltype = "SRTN1";
                                                if (alltotal != 0)
                                                    PrintSunLine(Cuscode, TOTNEW2.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                            }

                                            paytpcount++;
                                            DUTY = gnr.ledg_desc;

                                        }
                                        if (paytype == gnrtype && gnr.ledg_sub_tp == "VAT" && gnr.ledg_acc_tp == "CR" && gnr.ledg_pc == invdata.sah_pc && (gnr.ledg_desc == "CASH" | gnr.ledg_desc == "CRED" | gnr.ledg_desc == "TOTO" | gnr.ledg_desc == "UNITY"))
                                        {

                                            Acccode = gnr.ledg_acc_cd.ToString();
                                            string TOTAmmount = invdata.total.ToString("F3", CultureInfo.InvariantCulture);
                                            string TAXAmmount = invdata.taxtotal.ToString("F3", CultureInfo.InvariantCulture);
                                            string TOTunit = invdata.totalunit.ToString("F3", CultureInfo.InvariantCulture);
                                            DateTime transdate = invdata.sah_dt;
                                            string refNo = invdata.sah_inv_no.ToString();
                                            string cusname = invdata.sah_cus_name.ToString().Trim();
                                            string pc = invdata.sah_pc.ToString();
                                            string seq = invdata.tax_cd.ToString();
                                            jnaltype = gnr.ledg_jnl_tp.ToString();
                                            if (seq == "" | seq == "M" | seq == "N") seq = "O";
                                            if (jnaltype == "") jnaltype = "INV1";

                                            if (invdata.sah_is_svat == 1) seq = invdata.svatcd.ToString();

                                            if (gnr.ledg_desc == "DUTY")
                                            {
                                                jnaltype = "INV8";
                                                seq = "O";
                                            }
                                            //split ref

                                            //removed by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                            /*string[] words = refNo.Split('-');
                                            if (words.Length > 2)
                                            {
                                                if (words[0] == "103" || words[0] == "41")
                                                {
                                                    refNo = words[0] + words[1];
                                                    if (refNo.Length > 5)
                                                    {
                                                        refNo = refNo.Substring(0, 5);
                                                    }
                                                    refNo = refNo + words[2];
                                                    if (refNo.Length > 10)
                                                    {
                                                        refNo = refNo.Substring(0, 10);
                                                    }
                                                }
                                                else if (words[0] == "44A" || words[0] == "129" || words[0] == "181" || words[0] == "89" || words[0] == "144" || words[0] == "194" || words[0] == "183" || words[0] == "37" || words[0] == "122" || words[0] == "76" || Int32.TryParse(words[0], out isnum) == true)
                                                {
                                                    if (invdata.sah_inv_no.Contains("INDBN"))
                                                    {
                                                        refNo = words[1] + words[3];
                                                    }
                                                    else
                                                    {
                                                        refNo = words[1] + words[2];
                                                        if (refNo.Length > 10)
                                                        {
                                                            refNo = refNo.Substring(0, 10);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    refNo = words[1] + words[2] + words[0];

                                                }

                                            }*/
                                            //Change Ref
                                            //if (refNo.Contains("-INREV"))
                                            //{
                                            //    //refNo = invdata.sah_ref_doc.Substring(0, 4) + invdata.sah_inv_no;
                                            //    //if (refNo.Length > 10)
                                            //    //{
                                            //    //    refNo = "Z" + refNo.Substring(0, 4) + refNo.Substring(refNo.Length - 6);

                                            //    //}
                                            //    refNo = "Z" + invdata.sah_ref_doc.ToString();
                                            //}
                                            if (invdata.sah_direct == 0)
                                            {
                                                int invlenght = invdata.sah_inv_no.Length;
                                                //refNo = invdata.sah_ref_doc.ToString();//removed by Wimal @ 17/08/2018 To send reversal number (currently send invoice ref number)
                                                refNo = invdata.sah_inv_no.ToString();
                                                if (invdata.sah_anal_4 == "DISC")
                                                {
                                                    refNo = invdata.sah_inv_no.ToString();
                                                }
                                                //removed by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                                /*string[] words2 = refNo.Split('-');
                                                if (words2.Length == 2)
                                                {
                                                    if (invdata.sah_anal_4 == "DISC")
                                                    {
                                                        refNo = invdata.sah_pc.ToString() + words2[1];
                                                    }
                                                    else
                                                    {
                                                        refNo = "Z" + words2[0] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                    }
                                                }
                                                else if (words2.Length == 3)
                                                {
                                                    if (invdata.sah_anal_4 == "DISC")
                                                    {
                                                        refNo = invdata.sah_pc.ToString() + words2[2];
                                                    }
                                                    else
                                                    {
                                                        if (words2[0] == "103")
                                                        {
                                                            refNo = "Z" + words2[0] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                        }
                                                        else if (words2[0] == "44A" || words2[0] == "129" || words2[0] == "181" || words[0] == "89" || words[0] == "144" || words[0] == "194" || words[0] == "183" || words[0] == "37" || words[0] == "122" || words[0] == "76" || Int32.TryParse(words[0], out isnum) == true)
                                                        {
                                                            refNo = "Z" + words2[1] + words2[2];
                                                        }
                                                        else
                                                        {
                                                            refNo = "Z" + words2[1] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                        }

                                                    }

                                                }
                                                else
                                                {
                                                    if (invdata.sah_anal_4 == "DISC")
                                                    {
                                                        refNo = invdata.sah_pc.ToString() + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                    }
                                                    else
                                                    {
                                                        refNo = "Z" + refNo.Substring(0, 4) + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                    }

                                                }*/
                                            }
                                            //removed by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                            /*
                                            if (refNo.Length > 10)
                                            {
                                                refNo = refNo.Replace(@"-", string.Empty);
                                            }*/
                                            if (invdata.sah_tax_exempted == 1)
                                            {
                                                seq = "E";
                                            }

                                            ////check ref no 
                                            //bool isupload3 = CHNLSVC.Financial.CheckSunRef(refNo, invdata.sah_dt.ToString("yyyyMMdd"));
                                            //if (isupload3)
                                            //{
                                            //    continue;
                                            //}



                                            if (invdata.sah_direct == 1)
                                            {
                                                if (invdata.taxtotal != 0)
                                                {
                                                    //if (invdata.NBTValue == 0)
                                                    //{
                                                    //    PrintSunLine(Acccode, TAXAmmount.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                    //}
                                                    //else
                                                    //{
                                                    //    decimal itemtottax = invdata.taxtotal;
                                                    //    decimal nbtvalue = invdata.NBTValue;

                                                    //    decimal realtax = itemtottax - nbtvalue;
                                                    //    string strnbt = nbtvalue.ToString("F3", CultureInfo.InvariantCulture);
                                                    //    string strrealtax = realtax.ToString("F3", CultureInfo.InvariantCulture);

                                                    //    string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "NBT");
                                                    //    if (_nbtacc == "")
                                                    //    {
                                                    //        _nbtacc = Acccode;
                                                    //    }

                                                    //    PrintSunLine(_nbtacc, strnbt.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                    //    PrintSunLine(Acccode, strrealtax.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode));
                                                    //}

                                                    //VAT-NBT-XID-
                                                    decimal _vatval = CHNLSVC.Financial.GetTaxValForCode(invdata.sah_inv_no, "VAT");
                                                    decimal _nbtval = CHNLSVC.Financial.GetTaxValForCode(invdata.sah_inv_no, "NBT");
                                                    decimal _xidval = CHNLSVC.Financial.GetTaxValForCode(invdata.sah_inv_no, "XID");
                                                    decimal _other = invdata.taxtotal - _vatval - _nbtval - _xidval;

                                                    if (_vatval > 0)
                                                    {
                                                        string vvl = _vatval.ToString("F3", CultureInfo.InvariantCulture);
                                                        PrintSunLine(Acccode, vvl.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                                    }

                                                    if (_nbtval > 0)
                                                    {
                                                        string vvl = _nbtval.ToString("F3", CultureInfo.InvariantCulture);

                                                        string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "NBT");
                                                        if (_nbtacc == "")
                                                        {
                                                            _nbtacc = Acccode;
                                                        }

                                                        PrintSunLine(_nbtacc, vvl.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                                    }

                                                    if (_xidval > 0)
                                                    {
                                                        string vvl = _nbtval.ToString("F3", CultureInfo.InvariantCulture);

                                                        string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "XID");
                                                        if (_nbtacc == "")
                                                        {
                                                            _nbtacc = Acccode;
                                                        }

                                                        PrintSunLine(_nbtacc, vvl.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                                    }
                                                    if (_other > 0)
                                                    {
                                                        string vvl = _other.ToString("F3", CultureInfo.InvariantCulture);
                                                        PrintSunLine(Acccode, vvl.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                                    }


                                                }

                                            }
                                            else
                                            {
                                                if (invdata.isdiliver == "0" && invdata.sah_man_ref.Contains("SRN") == false && invdata.sah_inv_no.Contains("INCRN") == false)
                                                {
                                                    //cancel srn 2016-11-19
                                                    //removed by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                                    /*
                                                    string refNo1 = invdata.sah_inv_no.Replace(@"-", string.Empty);
                                                    refNo1 = refNo1.Replace(@"REV", string.Empty);
                                                    int srnlenght = refNo1.Length;
                                                    if (srnlenght > 10)
                                                    {
                                                        //refNo = refNo1.Substring(0, 10);
                                                        refNo = refNo1.Substring(0, 3) + "REV" + refNo1.Substring(srnlenght - 4);
                                                    }
                                                    else
                                                    {
                                                        refNo = refNo1;
                                                    }*/
                                                    cusname = invdata.sah_ref_doc.ToString();
                                                }
                                                else if (invdata.sah_direct == 0 && invdata.sah_inv_no.Contains("INCRN"))
                                                {
                                                    cusname = invdata.sah_ref_doc.ToString();
                                                    //removed by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                                    /*
                                                    string[] crwords = invdata.sah_inv_no.Split('-');
                                                    if (crwords.Length > 2)
                                                    {
                                                        refNo = crwords[0] + "CN" + crwords[3];
                                                    }
                                                    else
                                                    {
                                                        refNo = invdata.sah_inv_no;
                                                    }*/
                                                    //added by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                                    refNo = invdata.sah_inv_no;
                                                }
                                                else
                                                {

                                                    cusname = invdata.sah_man_ref.ToString();
                                                    if (cusname == "" || cusname == "N/A" || (cusname.Contains("SRN") == false))
                                                    {
                                                        cusname = invdata.sah_ref_doc.ToString();
                                                    }
                                                }

                                                ////check ref no 
                                                //bool isupload4 = CHNLSVC.Financial.CheckSunRef(refNo, invdata.sah_dt.ToString("yyyyMMdd"));
                                                //if (isupload4)
                                                //{
                                                //    continue;
                                                //}


                                                jnaltype = "SRTN1";

                                                string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "VATREV");
                                                if (_nbtacc == "")
                                                {
                                                    _nbtacc = Acccode;
                                                }
                                                if (invdata.taxtotal != 0)
                                                    PrintSunLine(_nbtacc, TAXAmmount.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                            }

                                            paytpcount++;

                                        }
                                        if (paytype == gnrtype && gnr.ledg_sub_tp == "SA" && gnr.ledg_acc_tp == "CR" && gnr.ledg_pc == invdata.sah_pc && (gnr.ledg_desc == "CASH" | gnr.ledg_desc == "CRED" | gnr.ledg_desc == "DUTY" | gnr.ledg_desc == "TOTO" | gnr.ledg_desc == "UNITY"))
                                        {
                                            Acccode = gnr.ledg_acc_cd.ToString();
                                            string TOTAmmount = invdata.total.ToString("F3", CultureInfo.InvariantCulture);
                                            string TAXAmmount = invdata.taxtotal.ToString("F3", CultureInfo.InvariantCulture);

                                            decimal realtax = invdata.taxtotal;
                                            decimal realvalue = invdata.RealTotalwithtax - realtax;

                                            string TOTunit = realvalue.ToString("F3", CultureInfo.InvariantCulture); //invdata.totalunit.ToString("F3", CultureInfo.InvariantCulture);
                                            DateTime transdate = invdata.sah_dt;
                                            string refNo = invdata.sah_inv_no.ToString();
                                            string cusname = invdata.sah_cus_name.ToString().Trim();
                                            string pc = invdata.sah_pc.ToString();
                                            string seq = invdata.tax_cd.ToString();
                                            jnaltype = gnr.ledg_jnl_tp.ToString();
                                            if (seq == "" | seq == "M" | seq == "N") seq = "O";
                                            if (jnaltype == "") jnaltype = "INV1";

                                            if (invdata.sah_is_svat == 1) seq = invdata.svatcd.ToString();

                                            if (gnr.ledg_desc == "DUTY")
                                            {
                                                jnaltype = "INV8";
                                                seq = "O";
                                            }
                                            //split ref

                                            //removed by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                            /*string[] words = refNo.Split('-');
                                            if (words.Length > 2)
                                            {
                                                if (words[0] == "103" || words[0] == "41")
                                                {
                                                    refNo = words[0] + words[1];
                                                    if (refNo.Length > 5)
                                                    {
                                                        refNo = refNo.Substring(0, 5);
                                                    }
                                                    refNo = refNo + words[2];
                                                    if (refNo.Length > 10)
                                                    {
                                                        refNo = refNo.Substring(0, 10);
                                                    }
                                                }
                                                else if (words[0] == "44A" || words[0] == "129" || words[0] == "181" || words[0] == "89" || words[0] == "144" || words[0] == "194" || words[0] == "183" || words[0] == "37" || words[0] == "122" || words[0] == "76" || Int32.TryParse(words[0], out isnum) == true)
                                                {
                                                    if (invdata.sah_inv_no.Contains("INDBN"))
                                                    {
                                                        refNo = words[1] + words[3];
                                                    }
                                                    else
                                                    {
                                                        refNo = words[1] + words[2];
                                                        if (refNo.Length > 10)
                                                        {
                                                            refNo = refNo.Substring(0, 10);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    refNo = words[1] + words[2] + words[0];

                                                }

                                            }*/
                                            //Change Ref
                                            //if (refNo.Contains("-INREV"))
                                            //{
                                            //    //refNo = invdata.sah_ref_doc.Substring(0, 4) + invdata.sah_inv_no;
                                            //    //if (refNo.Length > 10)
                                            //    //{
                                            //    //    refNo = "Z" + refNo.Substring(0, 4) + refNo.Substring(refNo.Length - 6);

                                            //    //}
                                            //    refNo ="Z"+ invdata.sah_ref_doc.ToString();
                                            //}
                                            if (invdata.sah_direct == 0)
                                            {
                                                int invlenght = invdata.sah_inv_no.Length;
                                                //refNo = invdata.sah_ref_doc.ToString();//removed by Wimal @ 17/08/2018 To send reversal number (currently send invoice ref number)
                                                refNo = invdata.sah_inv_no.ToString();
                                                //removed by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                                /*if (invdata.sah_anal_4 == "DISC")
                                                {
                                                    refNo = invdata.sah_inv_no.ToString();
                                                }
                                                string[] words2 = refNo.Split('-');
                                                if (words2.Length == 2)
                                                {
                                                    if (invdata.sah_anal_4 == "DISC")
                                                    {
                                                        refNo = invdata.sah_pc.ToString() + words2[1];
                                                    }
                                                    else
                                                    {
                                                        refNo = "Z" + words2[0] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                    }
                                                }
                                                else if (words2.Length == 3)
                                                {
                                                    if (invdata.sah_anal_4 == "DISC")
                                                    {
                                                        refNo = invdata.sah_pc.ToString() + words2[2];
                                                    }
                                                    else
                                                    {
                                                        if (words2[0] == "103")
                                                        {
                                                            refNo = "Z" + words2[0] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                        }
                                                        else if (words2[0] == "44A" || words2[0] == "129" || words2[0] == "181" || words[0] == "89" || words[0] == "144" || words[0] == "194" || words[0] == "183" || words[0] == "37" || words[0] == "122" || words[0] == "76" || Int32.TryParse(words[0], out isnum) == true)
                                                        {
                                                            refNo = "Z" + words2[1] + words2[2];
                                                        }
                                                        else
                                                        {
                                                            refNo = "Z" + words2[1] + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                        }

                                                    }

                                                }
                                                else
                                                {
                                                    if (invdata.sah_anal_4 == "DISC")
                                                    {
                                                        refNo = invdata.sah_pc.ToString() + "CRN" + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                    }
                                                    else
                                                    {
                                                        refNo = "Z" + refNo.Substring(0, 4) + invdata.sah_inv_no.Substring(invlenght - 5, 5);
                                                    }

                                                }*/
                                            }
                                                //removed by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                                /*
                                            if (refNo.Length > 10)
                                            {
                                                refNo = refNo.Replace(@"-", string.Empty);
                                            }*/

                                                if (invdata.sah_tax_exempted == 1)
                                                {
                                                    seq = "E";
                                                }

                                                ////check ref no 
                                                //bool isupload5 = CHNLSVC.Financial.CheckSunRef(refNo, invdata.sah_dt.ToString("yyyyMMdd"));
                                                //if (isupload5)
                                                //{
                                                //    continue;
                                                //}



                                                if (invdata.sah_direct == 1)
                                                {
                                                    if (invdata.totalunit != 0)
                                                        PrintSunLine(Acccode, TOTunit.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                                }
                                                else
                                                {
                                                    if (invdata.isdiliver != "0" || invdata.sah_man_ref.Contains("SRN") == true)
                                                    {


                                                        cusname = invdata.sah_man_ref.ToString();
                                                        if ((cusname == "" || cusname == "N/A" || (cusname.Contains("SRN") == false)))
                                                        {
                                                            cusname = invdata.sah_ref_doc.ToString();
                                                            string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "REV");
                                                            if (_nbtacc == "")
                                                            {
                                                                _nbtacc = Acccode;
                                                            }
                                                            if (invdata.totalunit != 0)
                                                                PrintSunLine(_nbtacc, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                                        }
                                                        else
                                                        {
                                                            jnaltype = "SRTN1";
                                                            string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "SRN");
                                                            if (_nbtacc == "")
                                                            {
                                                                _nbtacc = Acccode;
                                                            }
                                                            if (invdata.totalunit != 0)
                                                                PrintSunLine(_nbtacc, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                                        }

                                                    }
                                                    else if (((invdata.isdiliver == "0" && gnr.ledg_desc == "CASH") || (invdata.sah_man_ref.Contains("SRN") == false && gnr.ledg_desc == "CASH")) && invdata.sah_anal_4 != "DISC")
                                                    {
                                                        //cancel srn 2016-11-19
                                                        //removed by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                                        /*
                                                        string refNo1 = invdata.sah_inv_no.Replace(@"-", string.Empty);
                                                        refNo1 = refNo1.Replace(@"REV", string.Empty);
                                                        int srnlenght = refNo1.Length;
                                                        if (srnlenght > 10)
                                                        {
                                                            // refNo = refNo1.Substring(0, 10);
                                                            refNo = refNo1.Substring(0, 3) + "REV" + refNo1.Substring(srnlenght - 4);
                                                        }
                                                        else
                                                        {
                                                            refNo = refNo1;
                                                        }*/
                                                        //added by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                                        refNo = invdata.sah_inv_no;
                                                        cusname = invdata.sah_ref_doc.ToString();
                                                        jnaltype = "SRTN1";

                                                        string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "REVCASH");
                                                        if (_nbtacc == "")
                                                        {
                                                            _nbtacc = Acccode;
                                                        }
                                                        if (invdata.totalunit != 0)
                                                            PrintSunLine(_nbtacc, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                                    }
                                                    else if (invdata.sah_direct == 0 && invdata.sah_anal_4 == "DISC")
                                                    {
                                                        jnaltype = "REC1";
                                                        cusname = invdata.sah_ref_doc.ToString();

                                                        string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "DISC");
                                                        if (_nbtacc == "")
                                                        {
                                                            _nbtacc = Acccode;
                                                        }
                                                        if (invdata.totalunit != 0)
                                                            PrintSunLine(_nbtacc, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                                    }
                                                    else if (invdata.sah_direct == 0 && invdata.sah_inv_no.Contains("INCRN"))
                                                    {
                                                        jnaltype = "SRTN1";
                                                        cusname = invdata.sah_ref_doc.ToString();

                                                        string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "INCRN");
                                                        if (_nbtacc == "")
                                                        {
                                                            _nbtacc = Acccode;
                                                        }
                                                        string[] crwords = invdata.sah_inv_no.Split('-');
                                                        if (crwords.Length > 2)
                                                        {
                                                            refNo = crwords[0] + "CN" + crwords[3];
                                                        }
                                                        else
                                                        {
                                                            refNo = invdata.sah_inv_no;
                                                        }
                                                        if (invdata.totalunit != 0)
                                                            PrintSunLine(_nbtacc, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                                    }
                                                    else
                                                    {
                                                        //cancel srn 2016-11-19
                                                        //removed by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                                        /*string refNo1 = invdata.sah_inv_no.Replace(@"-", string.Empty);
                                                        refNo1 = refNo1.Replace(@"REV", string.Empty);
                                                        int srnlenght = refNo1.Length;
                                                        if (srnlenght > 10)
                                                        {
                                                            // refNo = refNo1.Substring(0, 10);
                                                            refNo = refNo1.Substring(0, 3) + "REV" + refNo1.Substring(srnlenght - 4);
                                                        }
                                                        else
                                                        {
                                                            refNo = refNo1;
                                                        }*/
                                                        //added by wimal to avoid unloading ammended invoice number into SUn @ 23/07/2018
                                                        refNo = invdata.sah_inv_no;
                                                        cusname = invdata.sah_ref_doc.ToString();
                                                        jnaltype = "SRTN1";
                                                        string _nbtacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "REV");
                                                        if (_nbtacc == "")
                                                        {
                                                            _nbtacc = Acccode;
                                                        }

                                                        //check ref no 
                                                        //bool isupload6 = CHNLSVC.Financial.CheckSunRef(refNo, invdata.sah_dt.ToString("yyyyMMdd"));
                                                        //if (isupload6)
                                                        //{
                                                        //    continue;
                                                        //}


                                                        if (invdata.totalunit != 0)
                                                            PrintSunLine(_nbtacc, TOTunit.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                                    }

                                                }
                                                paytpcount++;
                                                DUTY = gnr.ledg_desc;

                                            }


                                        }
                                        if (paytpcount < 2 && DUTY != "DUTY" && paytype != "DEBT")
                                        {
                                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Not Define " + paytype + " Type " + invdata.sah_pc.ToString() + "');", true);
                                            return;
                                        }
                                        if (paytpcount > 3)
                                        {
                                            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Not Define " + paytype + " Type " + invdata.sah_pc.ToString() + "');", true);

                                        }
                                        if (count == 0)
                                        {
                                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Not Define " + paytype + " Type " + invdata.sah_pc.ToString() + "');", true);
                                            return;
                                        }
                                        // isAuto temp commented on 02/08/2018
                                        //if (!isAuto)
                                        //{
                                            int effect = CHNLSVC.Financial.UPDATE_INV_HDRENGLOG(invdata.sah_inv_no, 1, Session["UserCompanyCode"].ToString());
                                        //}

                                    }

                                }

                            }

                        }
                    }
                    string _PATH = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "PATHINV");
                    System.IO.File.Copy(@name, _PATH + "inv" + date, true);
                    Session["path"] = "File Name : inv" + date;
                    // System.IO.File.Copy(@"C:/SUN/" + name, "\\\\192.168.1.45\\SUN\\" + name, true);
                }

            #endregion
                #region Reciept Upload
                if (type == "Reciept")
                {
                    FileInfo info = new FileInfo(name2);
                    if (info.Exists || !info.Exists)
                    {
                        using (StreamWriter writer = info.CreateText())
                        {
                            List<SUNRECIEPTHDR> listorder = CHNLSVC.Financial.GetSunRecieptdatanew(Session["UserCompanyCode"].ToString(), pctxt, startdate, enddate, paytypereal);
                            if (listorder == null) return;
                            listorder = listorder.GroupBy(l => new { l.sar_receipt_no })
    .Select(cl => new SUNRECIEPTHDR
    {
        sar_receipt_no = cl.First().sar_receipt_no,
        sar_receipt_type = cl.First().sar_receipt_type,
        sar_seq_no = cl.First().sar_seq_no,
        sar_profit_center_cd = cl.First().sar_profit_center_cd,
        sar_debtor_name = cl.First().sar_debtor_name,
        sar_receipt_date = cl.First().sar_receipt_date,
        sar_debtor_cd = cl.First().sar_debtor_cd,
        sard_ref_no = cl.First().sard_ref_no,
        sar_tot_settle_amt = cl.Sum(a => a.sar_tot_settle_amt),
        checkno = cl.First().checkno,
        sar_remarks = cl.First().sar_remarks,
        RecieptType = cl.First().RecieptType,
        sard_inv_no = cl.First().sard_inv_no,
        sard_chq_bank_cd = cl.First().sard_chq_bank_cd,
        sard_chq_branch = cl.First().sard_chq_branch,
        Branchdesc = cl.First().Branchdesc,
        BankdepositeDate = cl.First().BankdepositeDate
    }).ToList();

                            if (listorder != null)
                                listorder = listorder.OrderBy(a => a.sar_receipt_type).ToList();
                            if (listorder != null)
                            {
                                foreach (var invdata in listorder)
                                {
                                    if ((invdata.sar_receipt_no.Contains("SRN") == true || invdata.sar_receipt_no.Contains("ADV") == true) && Session["UserCompanyCode"].ToString() != "AEC")
                                    {
                                    }
                                    else
                                    {
                                        string Cuscode = "";
                                        Acccode = invdata.sard_ref_no.ToString();
                                        if (Acccode == null || Acccode == "")
                                        {
                                            string _defcusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "AALDEFLT");
                                            Acccode = _defcusacc;
                                        }
                                        if (Acccode.Trim() == "CASH" | Acccode.Trim() == "")
                                        {
                                            string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "RECACCCASH");
                                            Acccode = _cusacc;
                                        }

                                        string TOTAmmount = invdata.sar_tot_settle_amt.ToString("F3", CultureInfo.InvariantCulture);
                                        jnaltype = "REC1";
                                        if (invdata.sar_debtor_cd.ToString() != null)
                                            Cuscode = invdata.sar_debtor_cd.ToString();

                                        if (Cuscode.Trim() == "" | Cuscode.Trim() == "CASH")
                                        {
                                            string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "RECCASH");
                                            Cuscode = _cusacc;
                                        }
                                        DateTime transdate = invdata.sar_receipt_date;
                                        string refNo = invdata.sar_receipt_no.ToString();
                                        string cusname = invdata.checkno.ToString();
                                        string pc = invdata.sar_profit_center_cd.ToString();
                                        string seq = "Z"; //invdata.sar_seq_no.ToString();

                                        //svat
                                        if (invdata.sar_receipt_no.Contains("SVAT"))
                                        {
                                            string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "RECSVAT");
                                            Acccode = _cusacc;
                                            jnaltype = "REC1";
                                            seq = "W";
                                        }
                                        if (invdata.sar_receipt_no.Contains("ORC"))
                                        {
                                            cusname = invdata.sar_remarks.ToString();
                                        }
                                        if (invdata.RecieptType == "TNSPT")
                                        {
                                            string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "TNSPT");
                                            Cuscode = _cusacc;
                                        }
                                        if (invdata.RecieptType == "VHREG")
                                        {
                                            string _cusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "VHREG");
                                            Cuscode = _cusacc;
                                        }
                                        string _desc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "DESC" + invdata.RecieptType);
                                        string _chassis = "";
                                        if (_desc == "CHASSIS")
                                        {

                                            if (invdata.sard_inv_no != null && invdata.sard_inv_no != "")
                                            {
                                                _chassis = CHNLSVC.Financial.GetRecChassisNo(invdata.sard_inv_no);
                                                if (_chassis == "")
                                                {
                                                    _chassis = invdata.sard_ref_no;
                                                }
                                            }
                                            else
                                            {
                                                _chassis = invdata.sard_ref_no;
                                            }

                                        }
                                        if (invdata.RecieptType == "RTCHQ")
                                        {
                                            int reflenght = cusname.Length;
                                            if (reflenght >= 6)
                                            {
                                                refNo = cusname.Substring(reflenght - 6);
                                            }
                                            else
                                            {
                                                refNo = cusname;
                                            }

                                            PrintSunLine(Cuscode, TOTAmmount.ToString(), "D", transdate, "CHQ-" + refNo, refNo, pc, seq, "RTN1", execcode);
                                            PrintSunLine(Acccode, TOTAmmount.ToString(), "C", transdate, "CHQ-" + refNo, refNo, pc, seq, "RTN1", execcode);
                                        }
                                        else
                                        {
                                            string _cusname2 = cusname;
                                            if (_desc == "CHASSIS")
                                            {
                                                _cusname2 = _chassis;
                                            }
                                            else
                                            {
                                                if (Session["UserCompanyCode"].ToString() == "AAL")
                                                {
                                                    _cusname2 = invdata.sard_inv_no;
                                                }
                                            }
                                            #region AAL
                                            if (Session["UserCompanyCode"].ToString() == "AAL")
                                            {
                                                if (invdata.sar_receipt_type == "BANK_SLIP")
                                                {
                                                    cusname = invdata.sar_receipt_date.Date.ToString("dd/MMM/yyyy");
                                                    _cusname2 = invdata.sar_receipt_date.Date.ToString("dd/MMM/yyyy");

                                                }
                                                else if (invdata.sar_receipt_type == "CHEQUE")
                                                {
                                                    if (invdata.checkno.Length > 6)
                                                    {
                                                        invdata.checkno = invdata.checkno.Substring((invdata.checkno.Length - 6));
                                                        cusname = invdata.checkno + invdata.sard_chq_bank_cd + invdata.Branchdesc;
                                                        _cusname2 = invdata.checkno + invdata.sard_chq_bank_cd + invdata.Branchdesc;
                                                    }
                                                }
                                                else if (invdata.sar_receipt_type == "BANK_SLIP")
                                                {
                                                    cusname = invdata.BankdepositeDate.ToString("dd/MMM/yyyy");
                                                    _cusname2 = invdata.BankdepositeDate.ToString("dd/MMM/yyyy");
                                                }
                                                else
                                                {
                                                    cusname = invdata.sar_receipt_date.Date.ToString("dd/MMM/yyyy");
                                                    _cusname2 = invdata.sar_receipt_date.Date.ToString("dd/MMM/yyyy");
                                                }


                                            }
                                            #endregion

                                            #region 44A
                                            if (invdata.sar_profit_center_cd == "44A")
                                            {
                                                if (invdata.sar_receipt_type == "CASH")
                                                {
                                                    Acccode = "ABBCA5020";
                                                }
                                                else if (invdata.sar_receipt_type == "CHEQUE")
                                                {
                                                    Acccode = "ABBCA5019";
                                                }
                                                else if (invdata.sar_receipt_type == "CRCD")
                                                {
                                                    Acccode = "ABDR51007";
                                                }
                                                else if (invdata.sar_receipt_type == "GVO" || invdata.sar_receipt_type == "GV")
                                                {
                                                    Acccode = "ABBCL1013";
                                                }
                                                else if (invdata.sar_receipt_type == "ADVAN")
                                                {
                                                    Acccode = "ABBCL1012";
                                                }
                                                else if (invdata.sar_receipt_type == "CRNOTE")
                                                {
                                                    Acccode = "ABBCL1041";
                                                }
                                            }
                                            #endregion

                                            if (invdata.RecieptType == "ADREF")
                                            {
                                                PrintSunLine(Cuscode, TOTAmmount.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                                PrintSunLine(Acccode, TOTAmmount.ToString(), "C", transdate, refNo, _cusname2, pc, seq, jnaltype, execcode);
                                            }
                                            else
                                            {

                                                PrintSunLine(Cuscode, TOTAmmount.ToString(), "C", transdate, refNo, _cusname2, pc, seq, jnaltype, execcode);
                                                PrintSunLine(Acccode, TOTAmmount.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                            }

                                        }



                                        int effect = CHNLSVC.Financial.UPDATE_RECIEPT_HDRENGLOG(invdata.sar_receipt_no, 1, Session["UserCompanyCode"].ToString());
                                    }

                                }
                            }
                        }
                    }
                    string _PATH = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "PATHINV");
                    System.IO.File.Copy(@name2, _PATH + "rec" + date, true);
                    Session["path"] = "File Name : rec" + date;
                }
                #endregion
                #region Local Perchs Upload
                if (type == "LCLPC")
                {
                    //foreach (GridViewRow row in gvgrnlist.Rows)
                    //{
                    //    CheckBox chkselectgrn = (CheckBox)row.FindControl("chkselectgrn");
                    //    if (chkselectgrn != null && chkselectgrn.Checked)
                    //    {
                    //        if (grntxt != "")
                    //        {
                    //            grntxt = grntxt + ",";
                    //        }
                    //        Label lbgrnno = (Label)row.FindControl("lbgrnno");
                    //        string com_cd = lbgrnno.Text;
                    //        grntxt = grntxt + com_cd;

                    //    }
                    //}
                    grntxt = "";
                    foreach (string listGRN in uploadRef)
                    {
                        if (grntxt != "")
                        {
                            grntxt = grntxt + ",";
                        }
                        grntxt = grntxt + listGRN;
                    }


                    FileInfo info = new FileInfo(name3);
                    if (info.Exists || !info.Exists)
                    {
                        using (StreamWriter writer = info.CreateText())
                        {

                            List<LocPurSun> grnsunlist = CHNLSVC.Financial.SP_GetLocPerSUN(Session["UserCompanyCode"].ToString(), grntxt);
                            grnsunlist = grnsunlist.GroupBy(l => new { l.itb_doc_no, l.itb_itm_cd })
    .Select(cl => new LocPurSun
    {
        itb_doc_no = cl.First().itb_doc_no,
        ith_seq_no = cl.First().ith_seq_no,
        ith_doc_date = cl.First().ith_doc_date,
        ith_bus_entity = cl.First().ith_bus_entity,
        mbe_name = cl.First().mbe_name,
        itb_qty = cl.Sum(a => a.itb_qty),
        pod_unit_price = cl.First().pod_unit_price,
        pod_dis_amt = cl.Sum(a => a.pod_dis_amt),
        pod_line_tax = cl.Sum(a => a.pod_line_tax),
        invoiceno = cl.First().invoiceno,
        itb_itm_cd = cl.First().itb_itm_cd,
        total = cl.Sum(a => a.total)
    }).ToList();
                            grnsunlist = grnsunlist.GroupBy(l => new { l.itb_doc_no })
    .Select(cl => new LocPurSun
    {
        itb_doc_no = cl.First().itb_doc_no,
        ith_seq_no = cl.First().ith_seq_no,
        ith_doc_date = cl.First().ith_doc_date,
        ith_bus_entity = cl.First().ith_bus_entity,
        mbe_name = cl.First().mbe_name,
        itb_qty = cl.Sum(a => a.itb_qty),
        pod_unit_price = cl.First().pod_unit_price,
        pod_dis_amt = cl.Sum(a => a.pod_dis_amt),
        pod_line_tax = cl.Sum(a => a.pod_line_tax),
        invoiceno = cl.First().invoiceno,
        itb_itm_cd = cl.First().itb_itm_cd,
        total = cl.Sum(a => a.total)
    }).ToList();

                            foreach (var newlclist in editlist)
                            {
                                foreach (var lclist in grnsunlist)
                                {
                                    if (lclist.itb_doc_no == newlclist.itb_doc_no)
                                    {
                                        lclist.mbe_name = newlclist.mbe_name;
                                        lclist.ith_doc_date = newlclist.ith_doc_date;
                                        lclist.invoiceno = newlclist.invoiceno;
                                    }
                                }

                            }

                            if (grnsunlist != null)
                            {
                                foreach (var invdata in grnsunlist)
                                {

                                    string Cuscode = "";
                                    string AmmAcccode = "ABBCA1014";
                                    string VatAccode = "ABCR71019";

                                    if (invdata.ith_doc_date >= Convert.ToDateTime("01/Mar/2018"))
                                    {
                                        AmmAcccode = "ABCR1I081";
                                    }

                                    decimal ammount = invdata.total;
                                    decimal vat = invdata.pod_line_tax;
                                    decimal disc = invdata.pod_dis_amt;
                                    ammount = ammount - disc;
                                    string ammounts = ammount.ToString("F3", CultureInfo.InvariantCulture);
                                    string VATSS = vat.ToString("F3", CultureInfo.InvariantCulture);

                                    decimal totamm = (ammount + vat);
                                    string totammss = totamm.ToString("F3", CultureInfo.InvariantCulture);

                                    if (invdata.ith_bus_entity.ToString() != null)
                                        Cuscode = invdata.ith_bus_entity.ToString();
                                    DateTime transdate = invdata.ith_doc_date;
                                    string refNo = invdata.invoiceno.ToString();
                                    string cusname = invdata.itb_doc_no.ToString();
                                    string pc = "N/A";
                                    string seq = "Z";//invdata.ith_seq_no.ToString();

                                    if (Session["UserCompanyCode"].ToString() == "BDL")
                                    {
                                        AmmAcccode = "BDBCA1002";
                                        VatAccode = "BDCR3A001";
                                        seq = "E";
                                    }

                                    PrintSunLine(AmmAcccode, ammounts.ToString(), "D", transdate, refNo, cusname, pc, seq, "LPCR", execcode);
                                    PrintSunLine(VatAccode, VATSS.ToString(), "D", transdate, refNo, cusname, pc, seq, "LPCR", execcode);
                                    PrintSunLine(Cuscode, totammss.ToString(), "C", transdate, refNo, cusname, pc, seq, "LPCR", execcode);
                                    CHNLSVC.Financial.UPDATE_LOCPCH_HDRENGLOG(cusname, 5, Session["UserCompanyCode"].ToString());
                                }
                            }
                        }
                    }
                    string _PATH = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "PATHLOPR");
                    System.IO.File.Copy(@name3, _PATH + "per" + date, true);
                    Session["path"] = "D:Sun-SUN426-PER-per" + date;
                }
                #endregion
                #region Credit Note Upload
                if (type == "CRNT")
                {
                    FileInfo info = new FileInfo(name4);
                    if (info.Exists || !info.Exists)
                    {
                        using (StreamWriter writer = info.CreateText())
                        {
                            List<Suncreditnote> listorder = CHNLSVC.Financial.SP_SUN_CREDNOTE(Session["UserCompanyCode"].ToString(), startdate, enddate, pctxt);

                            if (listorder != null)
                            {
                                foreach (var invdata in listorder)
                                {
                                    if ((invdata.cr_amt != 0 && invdata.ith_doc_no.Contains("SRN") == false) && (invdata.job_no.Contains("RIT1A") == false))
                                    {
                                        string Cuscode = "";
                                        Acccode = "ABPSA1008"; //invdata.grah_fuc_cd.ToString();
                                        string TOTAmmount = invdata.cr_amt.ToString("F3", CultureInfo.InvariantCulture);
                                        jnaltype = "SRTN1";
                                        if (invdata.sjb_b_cust_cd.ToString() != null)
                                            Cuscode = invdata.sah_cus_cd.ToString();
                                        DateTime transdate = invdata.ith_doc_date;
                                        string refNo = invdata.ith_doc_no.ToString();
                                        refNo = refNo.Replace(@"-", string.Empty);
                                        string cusname = invdata.job_no.ToString();
                                        string pc = invdata.sah_pc.ToString();
                                        string seq = invdata.tax_cd.ToString(); //invdata.sar_seq_no.ToString();

                                        if (invdata.ith_direct == 1)
                                        {
                                            PrintSunLine(Cuscode, TOTAmmount.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                            PrintSunLine(Acccode, TOTAmmount.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode);

                                        }
                                        else
                                        {
                                            PrintSunLine(Cuscode, TOTAmmount.ToString(), "D", transdate, refNo, cusname, pc, seq, jnaltype, execcode);
                                            PrintSunLine(Acccode, TOTAmmount.ToString(), "C", transdate, refNo, cusname, pc, seq, jnaltype, execcode);

                                        }
                                    }

                                    //if (chkblock.Checked == true)
                                    //{
                                    //    int effect = CHNLSVC.Financial.UPDATE_INV_HDRENGLOG(invdata.ith_doc_no, 1, Session["UserCompanyCode"].ToString());
                                    //}

                                }
                            }
                        }
                    }
                    string _PATH = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "PATHINV");
                    System.IO.File.Copy(@name4, _PATH + "cred" + date, true);
                    Session["path"] = "File Name :" + "cred" + date;
                }
                #endregion
                #region Return Check Upload

                #endregion

                //}
                //catch (Exception ex)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
                //}
           
        }

        protected void Dddivtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string type = Dddivtype.SelectedValue.ToString();
                DataTable pcdata = new DataTable();

                pcdata = CHNLSVC.Financial.GetSunPC(type, Session["UserCompanyCode"].ToString());
                gvpclist.DataSource = pcdata;
                gvpclist.DataBind();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
            }
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

        protected void Ddpaytype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Ddpaytype.SelectedValue == "LCLPC")
                {
                    DateTime fromdate = Convert.ToDateTime(txtSDate.Text.ToString());
                    DateTime todate = Convert.ToDateTime(txtEDate.Text.ToString());
                    DataTable drndata = CHNLSVC.Financial.SP_GetGRNDOC(fromdate, todate, Session["UserCompanyCode"].ToString());
                    gvgrnlist.DataSource = drndata;
                    gvgrnlist.DataBind();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
            }
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

        protected void chkfix_CheckedChanged(object sender, EventArgs e)
        {
            string sdate = txtSDate.Text.ToString();
            string edate = txtEDate.Text.ToString();

            DataTable ALLDOC = CHNLSVC.Sales.SP_GETERRLINEDOC("ABL", Convert.ToDateTime(sdate), Convert.ToDateTime(edate));
            int i = 0;
            string name = "C:/SUN" + "/1.txt";
            FileInfo info = new FileInfo(name);
            if (info.Exists || !info.Exists)
            {
                using (StreamWriter writer = info.CreateText())
                {
                    foreach (var doclist in ALLDOC.Rows)
                    {
                        DataTable errlist = CHNLSVC.Sales.SP_GETERRLINEDOC2(ALLDOC.Rows[i]["ith_oth_docno"].ToString(), ALLDOC.Rows[i]["itb_itm_cd"].ToString(), Convert.ToInt32(ALLDOC.Rows[i]["itb_itm_line"].ToString()), Convert.ToInt32(ALLDOC.Rows[i]["itb_batch_line"].ToString()), ALLDOC.Rows[i]["itb_itm_stus"].ToString());
                        if (errlist.Rows.Count > 0)
                        {
                            // writer.WriteLine(errlist.Rows[0]["itb_doc_no"].ToString() + "  " + errlist.Rows[0]["itb_base_ref_no"].ToString() + " " + errlist.Rows[0]["itb_itm_cd"].ToString());

                        }
                        else
                        {
                            writer.WriteLine(ALLDOC.Rows[i]["ith_oth_docno"].ToString() + "  " + ALLDOC.Rows[i]["itb_itm_cd"].ToString() + " " + ALLDOC.Rows[i]["itb_itm_stus"].ToString() + "  " + ALLDOC.Rows[i]["itb_doc_no"].ToString());
                        }
                        i++;
                    }
                }
            }
        }
    }
}