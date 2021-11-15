using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.WebERPClient.UserControls;
using FF.BusinessObjects;
using System.Data;
using System.Text;
using System.Globalization;
using System.Transactions;

namespace FF.WebERPClient.Sales_Module
{
    public partial class Excess_Short_Rem : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindData();
                txtSetMonth.Text = Convert.ToDateTime("01/" + ddlMonth.SelectedValue.Substring(0, 3) + "/" + ddlYear.SelectedValue).ToString("dd/MMM/yyyy");
                txtProcMonth.Text = Convert.ToDateTime("01/" + ddlMonth_1.SelectedValue.Substring(0, 3) + "/" + ddlYear1.SelectedValue).ToString("dd/MMM/yyyy");
                txtSetDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                txtProcDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                BindSettlementData(GlbUserDefProf, Convert.ToDateTime(txtSetMonth.Text));
                BindBalanceData(GlbUserComCode, GlbUserDefProf);
                BindRemitTypes();
                txtProcDate.Attributes.Add("onchange", this.Page.ClientScript.GetPostBackEventReference(this.btnDate, ""));
                process_month_change();
                BindExcessShortOthRemData();


            }
        }

        #region UI events
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string _compDesc = "";
            string _compAddr = "";
            string Msg = "";
            Decimal _curBal = 0;
            Decimal _prvBal = 0;

            //delete from sat_excs_bal table and insert
            string _stus = "";
            if (lblStatus.Text == "PENDING")
            {
                _stus = "NOT CONFIRMED";
            }

            int X = CHNLSVC.Financial.PrintExcessShort(GlbUserComCode, GlbUserDefProf, GlbExcsShortID, Convert.ToDateTime(txtProcMonth.Text), _stus, GlbUserName, out _prvBal, out _curBal);

            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(GlbUserComCode);
            if (_masterComp != null)
            {
                _compDesc = _masterComp.Mc_desc;
                _compAddr = _masterComp.Mc_add1 + _masterComp.Mc_add2;
            }

            clearReportParameters();
            GlbReportUser = GlbUserName;
            GlbReportCompany = _compDesc;
            GlbReportCompanyAddr = _compAddr;
            GlbDocNosList = GlbExcsShortID;
            GlbCurBal = _curBal;
            GlbPrevBal = _prvBal;
            GlbManager = "kapila";
            GlbStatus = lblStatus.Text;
            GlbToDate = Convert.ToDateTime(txtProcMonth.Text);

            GlbMainPage = "~/Reports_Module/Sales_Rep/Sales_Rep.aspx";

            GlbReportName = "ExcessShort";
            GlbReportPath = "~\\Reports_Module\\Sales_Rep\\ExcessShort.rpt";
            GlbReportMapPath = "~/Reports_Module/Sales_Rep/ExcessShort.rpt";

            Msg = "<script>window.open('../../Reports_Module/Sales_Rep/ExcessShortPrint.aspx','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string _desc = "Month of " + ddlMonth_1.SelectedValue + " " + ddlYear1.SelectedValue;

            int X = CHNLSVC.Financial.ConfirmExcessShort(GlbUserComCode, GlbUserDefProf, Convert.ToDateTime(txtProcMonth.Text), Convert.ToDateTime(txtProcDate.Text), GlbExcsShortID, _desc, GlbUserName);

            lblStatus.Text = "CONFIRMED";
            btnProcess.Enabled = false;
            btnAdd.Enabled = false; ;
            btnDel.Enabled = false; ;

            clearProcess();
            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Confirmed.");
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            if (ddlRemType.SelectedValue.Equals("-1"))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Select remmitance type.");
                return;
            }

            Int32 Z = CHNLSVC.Financial.DeleteExcsShortOthRem(GlbExcsShortID, Convert.ToInt32(txtWeek.Text), "10", ddlRemType.SelectedValue);
            BindExcessShortOthRemData();

            clearProcess();
            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Deleted.");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Decimal _excsAmt = 0;
            Decimal _shortAmt = 0;
            ExcessShortDet _excsShortRemDet = new ExcessShortDet();

            if (ddlRemType.SelectedValue.Equals("-1"))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Select remmitance type.");
                return;
            }

            if (optE.Checked == true)
            {
                _excsAmt = Convert.ToDecimal(txtAmt.Text);
                _shortAmt = 0;
            }
            else
            {
                _excsAmt = 0;
                _shortAmt = Convert.ToDecimal(txtAmt.Text); ;
            }
            _excsShortRemDet.Esrd_id = GlbExcsShortID;
            _excsShortRemDet.Esrd_week = Convert.ToInt32(txtWeek.Text);
            _excsShortRemDet.Esrd_line = 1;
            _excsShortRemDet.Esrd_sec = "10";
            _excsShortRemDet.Esrd_cd = ddlRemType.SelectedValue;
            _excsShortRemDet.Esrd_desc = ddlRemType.SelectedItem.Text;
            _excsShortRemDet.Esrd_invs = "";
            _excsShortRemDet.Esrd_exces = _excsAmt;
            _excsShortRemDet.Esrd_short = _shortAmt;
            _excsShortRemDet.Esrd_fixed = false;


            Int32 row_aff = CHNLSVC.Financial.SaveExcessDetails(_excsShortRemDet);

            BindExcessShortOthRemData();

            clearProcess();
            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Saved.");


        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtSetMonth.Text = Convert.ToDateTime("01/" + ddlMonth.SelectedValue.Substring(0, 3) + "/" + ddlYear.SelectedValue).ToString("dd/MMM/yyyy");
            BindSettlementData(GlbUserDefProf, Convert.ToDateTime(txtSetMonth.Text));
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYear.SelectedValue.Equals("-1"))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Select the year");
                return;
            }

            txtSetMonth.Text = Convert.ToDateTime("01/" + ddlMonth.SelectedValue.Substring(0, 3) + "/" + ddlYear.SelectedValue).ToString("dd/MMM/yyyy");

            BindSettlementData(GlbUserDefProf, Convert.ToDateTime(txtSetMonth.Text));

            calc_Balance();
        }

        protected void ddlYear1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProcMonth.Text = Convert.ToDateTime("01/" + ddlMonth_1.SelectedValue.Substring(0, 3) + "/" + ddlYear1.SelectedValue).ToString("dd/MMM/yyyy");
        }

        protected void ddlMonth_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYear1.SelectedValue.Equals("-1"))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Select the year");
                return;
            }

            process_month_change();
            BindExcessShortOthRemData();

        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            Boolean _isNew = false;
            if (lblStatus.Text == "CONFIRMED")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Already confirmed this month.");
                return;
            }
            if (lblStatus.Text == "")
            {
                _isNew = true;
            }

            if (_isNew == true)
            {
                ExcessShortHeader _excsShortHdr = new ExcessShortHeader();
                _excsShortHdr.Esrh_id = GlbExcsShortID;
                _excsShortHdr.Esrh_pc = GlbUserDefProf;
                _excsShortHdr.Esrh_mnth = Convert.ToDateTime(txtProcMonth.Text);
                _excsShortHdr.Esrh_user = GlbUserName;
                _excsShortHdr.Esrh_stus = "P";
                _excsShortHdr.Esrh_cre_by = GlbUserName;
                _excsShortHdr.Esrh_cre_when = Convert.ToDateTime(DateTime.Now.Date).Date;
                _excsShortHdr.Esrh_mod_by = GlbUserName;
                _excsShortHdr.Esrh_mod_when = Convert.ToDateTime(DateTime.Now.Date).Date;
                _excsShortHdr.Esrh_session_id = GlbUserSessionID;

                int Y = CHNLSVC.Financial.SaveExcessHeader(_excsShortHdr);
            }
            else
            {
                Int32 Z = CHNLSVC.Financial.DeleteExcsShortDetail(GlbExcsShortID);
            }

            int X = CHNLSVC.Financial.ProcessExcessShort(GlbUserComCode, GlbUserDefProf, Convert.ToInt32(ddlYear1.SelectedValue), ddlMonth_1.SelectedIndex, GlbExcsShortID, GlbUserName);

            BindExcesShortData(GlbUserComCode, GlbUserDefProf, Convert.ToDateTime(txtProcMonth.Text));

            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Processed.");
        }

        protected void btnSetDel_Click(object sender, EventArgs e)
        {
            //check whether acc period is finalized.


            Int32 row_aff = CHNLSVC.Financial.DeleteExcsSettlement(GlbUserComCode, GlbUserDefProf, Convert.ToDateTime(txtSetMonth.Text), Convert.ToDateTime(txtSetDate.Text));

            BindSettlementData(GlbUserDefProf, Convert.ToDateTime(txtSetMonth.Text));
            BindBalanceData(GlbUserComCode, GlbUserDefProf);

            clearSettlement();
            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Deleted.");
        }

        protected void btnSetAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSetAmt.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Enter the Amount.");
                return;
            }


            //check whether acc period is finalized.


            ExcessShortTrna _excsShortRem = new ExcessShortTrna();

            _excsShortRem.Exss_com = GlbUserComCode;
            _excsShortRem.Exss_pc = GlbUserDefProf;
            _excsShortRem.Exss_mnth = Convert.ToDateTime(txtSetMonth.Text);
            _excsShortRem.Exss_tp = "S";
            _excsShortRem.Exss_txn_dt = Convert.ToDateTime(txtSetDate.Text);
            _excsShortRem.Exss_desc = "Month of " + ddlMonth.SelectedValue + " " + ddlYear.SelectedValue;
            if (Convert.ToDecimal(txtBalance.Text) > 0)
            {
                _excsShortRem.Exss_amt = Convert.ToDecimal(txtSetAmt.Text) * -1;
            }
            else
            {
                _excsShortRem.Exss_amt = Convert.ToDecimal(txtSetAmt.Text);
            }
            _excsShortRem.Exss_user = GlbUserName;
            _excsShortRem.Exss_mod_when = Convert.ToDateTime(DateTime.Now.Date).Date;
            _excsShortRem.Exss_rem = txtSetRem.Text;

            Int32 row_aff = CHNLSVC.Financial.SaveExcessTrans(_excsShortRem);

            BindSettlementData(GlbUserDefProf, Convert.ToDateTime(txtSetMonth.Text));
            BindBalanceData(GlbUserComCode, GlbUserDefProf);

            clearSettlement();
            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Saved.");
        }

        protected void opt_Changed(object sender, EventArgs e)
        {
            BindRemitTypes();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }
        #endregion

        #region User defined functions
        protected void calc_Balance()
        {
            Decimal _excsBal = 0;
            DataTable DT = CHNLSVC.Financial.GetExcessBalance(GlbUserComCode, GlbUserDefProf, Convert.ToDateTime(txtSetMonth.Text), out _excsBal);
            txtBalance.Text = _excsBal.ToString("0,0.00", CultureInfo.InvariantCulture);
        }



        protected void clearSettlement()
        {
            txtSetRem.Text = "";
            txtSetAmt.Text = "";
        }

        protected void clearProcess()
        {
            txtAmt.Text = "";
        }

        protected void BindBalanceData(string _com, string _pc)
        {
            gvBal.DataSource = CHNLSVC.Financial.GetExcsBalanceDridData(_com, _pc);
            gvBal.DataBind();

            Decimal _excsBal = 0;
            DataTable DT = CHNLSVC.Financial.GetExcessBalance(GlbUserComCode, GlbUserDefProf, Convert.ToDateTime(txtSetMonth.Text), out _excsBal);
            txtBalance.Text = _excsBal.ToString("0,0.00", CultureInfo.InvariantCulture);
        }

        protected void BindSettlementData(string _pc, DateTime _mnth)
        {
            GvSettle.DataSource = CHNLSVC.Financial.GetExcsSettlementDridData(_pc, _mnth);
            GvSettle.DataBind();
        }

        protected void BindExcessShortOthRemData()
        {
            GvOthRem.DataSource = CHNLSVC.Financial.GetExcsShortOthRemGridData(GlbExcsShortID);
            GvOthRem.DataBind();
        }

        protected void BindExcesShortData(string _com, string _pc, DateTime _mnth)
        {
            GvExcRemSum.DataSource = CHNLSVC.Financial.GetExcsShortGridData(_com, _pc, _mnth);
            GvExcRemSum.DataBind();
        }



        protected void BindRemitTypes()
        {
            Int16 _isExcs = 0;
            if (optE.Checked == true)
            {
                _isExcs = 1;
            }

            ddlRemType.Items.Clear();
            ddlRemType.Items.Add(new ListItem("--Select Remit Type--", "-1"));
            ddlRemType.DataSource = CHNLSVC.Financial.GetExcsShortRemitType(_isExcs);
            ddlRemType.DataTextField = "esr_desc";
            ddlRemType.DataValueField = "esr_cd";
            ddlRemType.DataBind();
        }

        protected void bindData()
        {
            ddlYear.Items.Clear();
            ddlYear.Items.Add(new ListItem("--Select Year--", "-1"));
            ddlYear.Items.Add("2012");
            ddlYear.Items.Add("2013");
            ddlYear.Items.Add("2014");
            ddlYear.Items.Add("2015");
            ddlYear.Items.Add("2016");

            int _Year = DateTime.Now.Year;
            ddlYear.SelectedIndex = _Year % 2011;

            ddlMonth.Items.Clear();
            ddlMonth.Items.Add(new ListItem("--Select Month--", "-1"));
            ddlMonth.Items.Add("January");
            ddlMonth.Items.Add("February");
            ddlMonth.Items.Add("March");
            ddlMonth.Items.Add("April");
            ddlMonth.Items.Add("May");
            ddlMonth.Items.Add("June");
            ddlMonth.Items.Add("July");
            ddlMonth.Items.Add("August");
            ddlMonth.Items.Add("September");
            ddlMonth.Items.Add("October");
            ddlMonth.Items.Add("November");
            ddlMonth.Items.Add("December");

            ddlMonth.SelectedIndex = DateTime.Now.Month;

            ddlYear1.Items.Clear();
            ddlYear1.Items.Add(new ListItem("--Select Year--", "-1"));
            ddlYear1.Items.Add("2012");
            ddlYear1.Items.Add("2013");
            ddlYear1.Items.Add("2014");
            ddlYear1.Items.Add("2015");
            ddlYear1.Items.Add("2016");

            int _Year1 = DateTime.Now.Year;
            ddlYear1.SelectedIndex = _Year1 % 2011;

            ddlMonth_1.Items.Clear();
            ddlMonth_1.Items.Add(new ListItem("--Select Month--", "-1"));
            ddlMonth_1.Items.Add("January");
            ddlMonth_1.Items.Add("February");
            ddlMonth_1.Items.Add("March");
            ddlMonth_1.Items.Add("April");
            ddlMonth_1.Items.Add("May");
            ddlMonth_1.Items.Add("June");
            ddlMonth_1.Items.Add("July");
            ddlMonth_1.Items.Add("August");
            ddlMonth_1.Items.Add("September");
            ddlMonth_1.Items.Add("October");
            ddlMonth_1.Items.Add("November");
            ddlMonth_1.Items.Add("December");

            ddlMonth_1.SelectedIndex = DateTime.Now.Month;


        }

        protected void process_month_change()
        {
            if (DateTime.Now.Day == 31)
            {
                txtProcDate.Text = Convert.ToString("30/" + ddlMonth_1.SelectedValue.Substring(0, 3) + "/" + ddlYear1.SelectedValue);
            }
            else
            {
                txtProcDate.Text = Convert.ToString(DateTime.Now.Day + "/" + ddlMonth_1.SelectedValue.Substring(0, 3) + "/" + ddlYear1.SelectedValue);
            }
            txtProcMonth.Text = Convert.ToDateTime("01/" + ddlMonth_1.SelectedValue.Substring(0, 3) + "/" + ddlYear1.SelectedValue).ToString("dd/MMM/yyyy");

            Decimal _weekNo = 0;
            int K = CHNLSVC.General.GetWeek(Convert.ToDateTime(txtProcDate.Text), out _weekNo);
            txtWeek.Text = _weekNo.ToString();

            string _excsStatus = "";
            string _ID = "";
            DataTable DT = CHNLSVC.Financial.GetExcsStatus(GlbUserDefProf, Convert.ToDateTime(txtProcMonth.Text), out _excsStatus, out _ID);
            switch (_excsStatus)
            {

                case "F":
                    {
                        btnProcess.Enabled = false;
                        btnConfirm.Enabled = false;
                        btnAdd.Enabled = false;
                        btnDel.Enabled = false;
                        lblStatus.Text = "CONFIRMED";
                        GlbExcsShortID = _ID;
                        break;
                    }
                case "P":
                    {
                        btnProcess.Enabled = true;
                        btnConfirm.Enabled = true;
                        btnAdd.Enabled = true;
                        btnDel.Enabled = true;
                        lblStatus.Text = "PENDING";
                        GlbExcsShortID = _ID;
                        break;
                    }
                default:
                    lblStatus.Text = "";
                    btnAdd.Enabled = true;
                    btnDel.Enabled = true;
                    btnProcess.Enabled = true;
                    GlbExcsShortID = GlbUserName + System.DateTime.Now.ToString("ddMMyyyyhhmmss");
                    btnConfirm.Enabled = false;
                    break;
            }
        }



        protected void Process_Date_change(object sender, EventArgs e)
        {
            Decimal _weekNo = 0;
            int X = CHNLSVC.General.GetWeek(Convert.ToDateTime(txtProcDate.Text), out _weekNo);
            txtWeek.Text = _weekNo.ToString();
        }
        #endregion







    }
}