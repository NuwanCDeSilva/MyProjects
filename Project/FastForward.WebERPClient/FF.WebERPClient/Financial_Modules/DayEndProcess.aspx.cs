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

namespace FF.WebERPClient.Financial_Modules
{
    public partial class DayEndProcess : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                //IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, GlbUserDefProf, Page, string.Empty, imgFromDate, lblDispalyInfor);
                loadInit();
                BindData();
                BindGridView();

                txtVal.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnVal, ""));
                txtAdd.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnCalc, ""));
                txtDeduct.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnCalc, ""));
                txtDate.Attributes.Add("onchange", this.Page.ClientScript.GetPostBackEventReference(this.btnDate, ""));
            }
        }

        private void BindData()
        {

            ddlSecDef.Items.Clear();
            ddlSecDef.Items.Add(new ListItem("--Select Section--", "-1"));
            ddlSecDef.DataSource = CHNLSVC.Financial.GetSection();
            ddlSecDef.DataTextField = "rss_desc";
            ddlSecDef.DataValueField = "rss_cd";
            ddlSecDef.DataBind();

        }

        protected void ddlSecDef_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRemitTypes(ddlSecDef.SelectedValue);
        }

        protected void ddlRemTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSecDef.SelectedValue.Equals("02") && ddlRemTp.SelectedValue.Equals("013"))
            {
                //txtGross.Enabled = true;
                txtAdd.Enabled = true;
                txtDeduct.Enabled = true;
                txtNet.Enabled = true;
                txtVoucher.Enabled = true;
            }
            else
            {
                //txtGross.Enabled = false;
                txtAdd.Enabled = false;
                txtDeduct.Enabled = false;
                txtNet.Enabled = false;
                txtVoucher.Enabled = false;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime _DT=Convert.ToDateTime(DateTime.Now.Date).Date;
                if (CHNLSVC.Financial.IsValidDayEndDate(Convert.ToDateTime(txtDate.Text), GlbUserDefProf,out _DT) == false)
                {
                    throw new UIValidationException("Cannot process. Not a valid date");
                }
                if (ddlRemTp.SelectedValue.Equals("-1"))
                {
                    throw new UIValidationException("Please select Remitance");
                }
                if (ddlSecDef.SelectedValue.Equals("-1"))
                {
                    throw new UIValidationException("Please select Section");
                }
                if (ddlSecDef.SelectedValue.Equals("02") && ddlRemTp.SelectedValue.Equals("013"))
                {
                    if (string.IsNullOrEmpty(txtGross.Text) || string.IsNullOrEmpty(txtAdd.Text) || string.IsNullOrEmpty(txtDeduct.Text) || string.IsNullOrEmpty(txtNet.Text))
                    {
                        throw new UIValidationException("Please Collection bonus amount");
                    }
                }

                RemitanceSummaryDetail _remSumDet = new RemitanceSummaryDetail();
                Decimal _weekNo = 0;
                _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(txtDate.Text), out _weekNo);

                DataTable dtESD_EPF_WHT = new DataTable();
                dtESD_EPF_WHT = CHNLSVC.Sales.Get_ESD_EPF_WHT(GlbUserComCode, GlbUserDefProf, Convert.ToDateTime(txtDate.Text));

                Decimal ESD_rt = 0; Decimal EPF_rt = 0; Decimal WHT_rt = 0;
                if (dtESD_EPF_WHT.Rows.Count > 0)
                {
                    ESD_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_ESD"]);
                    EPF_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_EPF"]);
                    WHT_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_WHT"]);

                }

                _remSumDet.Rem_com = GlbUserComCode;
                _remSumDet.Rem_pc = GlbUserDefProf;
                _remSumDet.Rem_dt = Convert.ToDateTime(txtDate.Text);
                _remSumDet.Rem_sec = ddlSecDef.SelectedValue;
                _remSumDet.Rem_cd = ddlRemTp.SelectedValue;
                if (ddlSecDef.SelectedValue == "02" && ddlRemTp.SelectedValue == "013")  //col bonus
                {
                    _remSumDet.Rem_sh_desc = (ddlRemTp.SelectedItem.Text + "-" + txtVoucher.Text).ToString();
                    _remSumDet.Rem_lg_desc = (ddlRemTp.SelectedItem.Text + "-" + txtVoucher.Text).ToString().ToUpper();
                }
                else
                {
                    _remSumDet.Rem_sh_desc = ddlRemTp.SelectedItem.Text;
                    _remSumDet.Rem_lg_desc = ddlRemTp.SelectedItem.Text.ToUpper();
                }

                _remSumDet.Rem_val = Convert.ToDecimal(txtVal.Text);
                _remSumDet.Rem_val_final = Convert.ToDecimal(txtVal.Text);
                _remSumDet.Rem_week = (_weekNo + "S").ToString();
                _remSumDet.Rem_ref_no = txtVoucher.Text;
                _remSumDet.Rem_rmk = txtRem.Text;
                _remSumDet.Rem_cr_acc = "";
                _remSumDet.Rem_db_acc = "";
                _remSumDet.Rem_del_alw = false;
                _remSumDet.Rem_cre_by = GlbUserName;
                _remSumDet.Rem_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                if (ddlSecDef.SelectedValue == "05" && ddlRemTp.SelectedValue == "001")       //slip
                {
                    _remSumDet.Rem_is_sos = false;
                }
                else
                {
                    _remSumDet.Rem_is_sos = true;
                }

                _remSumDet.Rem_is_dayend = true;
                _remSumDet.Rem_is_sun = true;
                _remSumDet.Rem_cat = 17;
                _remSumDet.Rem_add = Convert.ToDecimal(txtAdd.Text);
                _remSumDet.Rem_ded = Convert.ToDecimal(txtDeduct.Text);
                _remSumDet.Rem_net = Convert.ToDecimal(txtNet.Text);
                _remSumDet.Rem_epf = EPF_rt;
                _remSumDet.Rem_esd = ESD_rt;
                _remSumDet.Rem_wht = WHT_rt;
                _remSumDet.Rem_add_fin = Convert.ToDecimal(txtAdd.Text);
                _remSumDet.Rem_ded_fin = Convert.ToDecimal(txtDeduct.Text);
                _remSumDet.Rem_net_fin = Convert.ToDecimal(txtNet.Text);
                _remSumDet.Rem_rmk_fin = txtRem.Text;
                _remSumDet.Rem_bnk_cd = "";
                _remSumDet.Rem_is_rem_sum = true;

                int row_aff = CHNLSVC.Financial.SaveRemSummaryDetails(_remSumDet);
                BindGridView();

                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Updated");

                //clear
                ddlSecDef.SelectedValue = "-1";
                ddlRemTp.SelectedValue = "-1";
                txtVal.Text = "";
                txtGross.Text = "0";
                txtDeduct.Text = "0";
                txtAdd.Text = "0";
                txtNet.Text = "0";
                txtRem.Text = "";
                txtVoucher.Text = "";

                //string Msg = "<script>alert('Successfully Updated!');window.location = 'RemitanceSummary.aspx';</script>";
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            catch (UIValidationException ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            }
            catch (Exception e1)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e1.Message);
            }
        }

        private void LoadRemitTypes(string _sec)
        {
            ddlRemTp.Items.Clear();
            ddlRemTp.Items.Add(new ListItem("--Select Type--", "-1"));
            ddlRemTp.DataSource = CHNLSVC.Financial.get_rem_type_by_sec(_sec, 0);
            ddlRemTp.DataTextField = "rsd_desc";
            ddlRemTp.DataValueField = "rsd_cd";
            ddlRemTp.DataBind();
        }


        protected void gvRemLimit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRemLimit.PageIndex = e.NewPageIndex;
            //this.LoadRemSumHeading();
        }

        private void BindGridView()
        {
            List<RemitanceSummaryDetail> _remsumdet = CHNLSVC.Financial.GetRemitanceSumDetail(Convert.ToDateTime(txtDate.Text), GlbUserDefProf);
            gvRemLimit.DataSource = _remsumdet;
            gvRemLimit.DataBind();
        }

        protected void loadInit()
        {
            Decimal _CIH = 0;
            Decimal _Comm = 0;
            int X = CHNLSVC.Financial.GetDayEndInit(Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(txtDate.Text), "03", "012", "CIH", GlbUserDefProf, out  _Comm, out _CIH);
            txtColBonus.Text = _Comm.ToString("0.00", CultureInfo.InvariantCulture);
            txtCIH.Text = _CIH.ToString("0.00", CultureInfo.InvariantCulture);
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        //protected void btnConfirm_Click(object sender, EventArgs e)
        //{
        //    DayEnd _dayEnd = new DayEnd();
        //    _dayEnd.Upd_com = GlbUserComCode;
        //    _dayEnd.Upd_pc = GlbUserDefProf;
        //    _dayEnd.Upd_dt = Convert.ToDateTime(txtDate.Text);
        //    _dayEnd.Upd_cre_dt = DateTime.Now;
        //    _dayEnd.Upd_cre_by = GlbUserName;
        //    _dayEnd.Upd_ov_wrt = false;

        //    int X = CHNLSVC.Financial.SaveDayEnd(_dayEnd);
        //    string Msg = "<script>alert('Successfully Confirmed');window.location = 'DayEndProcess.aspx';</script>";
        //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

        //    imgFromDate.Visible = true;
        //}

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Financial_Modules/DayEndProcess.aspx");
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                //if (IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, string.Empty, Page, txtDate.Text, imgFromDate, lblDispalyInfor) == true)
                //{
                //    throw new UIValidationException("Back date not allow for selected date!");
                //}
                //check whether session variable are empty
                if (GlbUserComCode == string.Empty)
                {
                    throw new UIValidationException("Cannot process. Session is expired");
                }

                DateTime _DT = Convert.ToDateTime(DateTime.Now.Date).Date;
                if (CHNLSVC.Financial.IsValidDayEndDate(Convert.ToDateTime(txtDate.Text), GlbUserDefProf,out _DT) == false)
                {
                    throw new UIValidationException("Cannot process. Not a valid date");
                }

                //check whether day end is not run for the previous day with having transactions
                DateTime _curDay = Convert.ToDateTime(txtDate.Text);
                DateTime _prevDay = _curDay.AddDays(-1);
                if (CHNLSVC.Financial.IsDayEndDone(GlbUserComCode, GlbUserDefProf, _prevDay, _prevDay) == false)
                {
                    if (CHNLSVC.Financial.IsPrvDayTxnsFound(GlbUserComCode, GlbUserDefProf, _prevDay) == true)
                    {
                        throw new UIValidationException("Cannot process. Day end is not done for the previous day");
                    }
                }

                //process commission
                int S = CHNLSVC.Sales.Process_DayEnd_Commission(GlbUserComCode, GlbUserDefProf, Convert.ToDateTime(txtDate.Text));

                Decimal _wkNo = 0;
                int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(txtDate.Text), out _wkNo);

                if (_wkNo == 0)
                {
                    throw new UIValidationException("Week Definition not found. Contact Accounts dept.");
                }

                DataTable DT = CHNLSVC.Financial.ProcessDayEnd(Convert.ToDateTime(txtDate.Text), GlbUserComCode, GlbUserDefProf,Convert.ToInt32(_wkNo), GlbUserName);

                RemitanceSummaryDetail _remSumDet = new RemitanceSummaryDetail();


                DataTable dtESD_EPF_WHT = new DataTable();
                dtESD_EPF_WHT = CHNLSVC.Sales.Get_ESD_EPF_WHT(GlbUserComCode, GlbUserDefProf, Convert.ToDateTime(txtDate.Text));

                Decimal ESD_rt = 0; Decimal EPF_rt = 0; Decimal WHT_rt = 0;
                if (dtESD_EPF_WHT.Rows.Count > 0)
                {
                    ESD_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_ESD"]);
                    EPF_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_EPF"]);
                    WHT_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_WHT"]);

                }

                _remSumDet.Rem_com = GlbUserComCode;
                _remSumDet.Rem_pc = GlbUserDefProf;
                _remSumDet.Rem_dt = Convert.ToDateTime(txtDate.Text);
                _remSumDet.Rem_sec = "03";
                _remSumDet.Rem_cd = "012";
                _remSumDet.Rem_sh_desc = "Commission Withdrawn";
                _remSumDet.Rem_lg_desc = "Commission Withdrawn";
                _remSumDet.Rem_val = Convert.ToDecimal(txtColBonus.Text);
                _remSumDet.Rem_val_final = Convert.ToDecimal(txtColBonus.Text);
                _remSumDet.Rem_week = (_weekNo + "S").ToString();
                _remSumDet.Rem_ref_no = "";
                _remSumDet.Rem_rmk = "";
                _remSumDet.Rem_cr_acc = "";
                _remSumDet.Rem_db_acc = "";
                _remSumDet.Rem_cre_by = GlbUserName;
                _remSumDet.Rem_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                _remSumDet.Rem_is_sos = false;
                _remSumDet.Rem_is_dayend = true;
                _remSumDet.Rem_is_sun = false;
                _remSumDet.Rem_del_alw = true;
                _remSumDet.Rem_is_rem_sum = false;
                _remSumDet.Rem_cat = 12;
                _remSumDet.Rem_add = 0;
                _remSumDet.Rem_ded = 0;
                _remSumDet.Rem_net = 0;
                _remSumDet.Rem_epf = EPF_rt;
                _remSumDet.Rem_esd = ESD_rt;
                _remSumDet.Rem_wht = WHT_rt;
                _remSumDet.Rem_add_fin = 0;
                _remSumDet.Rem_ded_fin = 0;
                _remSumDet.Rem_net_fin = 0;
                _remSumDet.Rem_rmk_fin = "";
                _remSumDet.Rem_bnk_cd = "";


                //save commission
                int row_aff = CHNLSVC.Financial.SaveRemSummaryDetails(_remSumDet);

                RemitanceSummaryDetail _remSumDet1 = new RemitanceSummaryDetail();

                _remSumDet1.Rem_com = GlbUserComCode;
                _remSumDet1.Rem_pc = GlbUserDefProf;
                _remSumDet1.Rem_dt = Convert.ToDateTime(txtDate.Text);
                _remSumDet1.Rem_sec = "06";
                _remSumDet1.Rem_cd = "001";
                _remSumDet1.Rem_sh_desc = "Cash In Hand";
                _remSumDet1.Rem_lg_desc = "Cash In Hand";
                _remSumDet1.Rem_val = Convert.ToDecimal(txtCIH.Text);
                _remSumDet1.Rem_val_final = Convert.ToDecimal(txtCIH.Text);
                _remSumDet1.Rem_week = (_weekNo + "S").ToString();
                _remSumDet1.Rem_ref_no = "";
                _remSumDet1.Rem_rmk = "";
                _remSumDet1.Rem_cr_acc = "";
                _remSumDet1.Rem_db_acc = "";
                _remSumDet1.Rem_cre_by = GlbUserName;
                _remSumDet1.Rem_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                _remSumDet1.Rem_is_sos = false;
                _remSumDet1.Rem_is_dayend = true;
                _remSumDet1.Rem_is_sun = false;
                _remSumDet1.Rem_del_alw = true;
                _remSumDet1.Rem_is_rem_sum = false;
                _remSumDet1.Rem_cat = 12;
                _remSumDet1.Rem_add = 0;
                _remSumDet1.Rem_ded = 0;
                _remSumDet1.Rem_net = 0;
                _remSumDet1.Rem_epf = EPF_rt;
                _remSumDet1.Rem_esd = ESD_rt;
                _remSumDet1.Rem_wht = WHT_rt;
                _remSumDet1.Rem_add_fin = 0;
                _remSumDet1.Rem_ded_fin = 0;
                _remSumDet1.Rem_net_fin = 0;
                _remSumDet1.Rem_rmk_fin = "";
                _remSumDet1.Rem_bnk_cd = "";

                //save cash in hand
                int row_aff1 = CHNLSVC.Financial.SaveRemSummaryDetails(_remSumDet1);

                RemitanceSummaryDetail _remSumDet2 = new RemitanceSummaryDetail();
                //DateTime _curDay = Convert.ToDateTime(txtDate.Text);
                //DateTime _nextDay = _curDay.AddDays(1);
                Decimal _prvDayCIH = 0;
                int XX = CHNLSVC.Financial.Get_prv_day_CIH(Convert.ToDateTime(txtDate.Text), GlbUserDefProf, out _prvDayCIH);

                _remSumDet2.Rem_com = GlbUserComCode;
                _remSumDet2.Rem_pc = GlbUserDefProf;
                _remSumDet2.Rem_dt = Convert.ToDateTime(txtDate.Text);
                _remSumDet2.Rem_sec = "03";
                _remSumDet2.Rem_cd = "008";
                _remSumDet2.Rem_sh_desc = "Prv. Day Cash in Hand";
                _remSumDet2.Rem_lg_desc = "Prv. Day Cash in Hand";
                _remSumDet2.Rem_val = _prvDayCIH;
                _remSumDet2.Rem_val_final = _prvDayCIH;
                _remSumDet2.Rem_week = (_weekNo + "S").ToString();
                _remSumDet2.Rem_ref_no = "";
                _remSumDet2.Rem_rmk = "";
                _remSumDet2.Rem_cr_acc = "";
                _remSumDet2.Rem_db_acc = "";
                _remSumDet2.Rem_del_alw = false;
                _remSumDet2.Rem_cre_by = GlbUserName;
                _remSumDet2.Rem_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                _remSumDet2.Rem_is_sos = false;
                _remSumDet2.Rem_is_dayend = true;
                _remSumDet2.Rem_is_sun = false;
                _remSumDet2.Rem_is_rem_sum = true;
                _remSumDet2.Rem_cat = 12;
                _remSumDet2.Rem_add = 0;
                _remSumDet2.Rem_ded = 0;
                _remSumDet2.Rem_net = 0;
                _remSumDet2.Rem_epf = EPF_rt;
                _remSumDet2.Rem_esd = ESD_rt;
                _remSumDet2.Rem_wht = WHT_rt;
                _remSumDet2.Rem_add_fin = 0;
                _remSumDet2.Rem_ded_fin = 0;
                _remSumDet2.Rem_net_fin = 0;
                _remSumDet2.Rem_rmk_fin = "";
                _remSumDet2.Rem_bnk_cd = "";

                //save prv. day cash in hand
                int row_aff2 = CHNLSVC.Financial.SaveRemSummaryDetails(_remSumDet2);

                BindReceiptGridData(GlbUserDefProf, Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(txtDate.Text));
                BindDisbursementGridData(GlbUserDefProf, Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(txtDate.Text));
                BindSummaryGridData(GlbUserDefProf, Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(txtDate.Text));
                BindLessGridData(GlbUserDefProf, Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(txtDate.Text));

                //Boolean _isOK = calc();
                //BindSummaryGridData(GlbUserDefProf, Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(txtDate.Text));

                //btnConfirm.Enabled = true;
                //DayEnd _dayEnd = new DayEnd();
                //_dayEnd.Upd_com = GlbUserComCode;
                //_dayEnd.Upd_pc = GlbUserDefProf;
                //_dayEnd.Upd_dt = Convert.ToDateTime(txtDate.Text);
                //_dayEnd.Upd_cre_dt = DateTime.Now;
                //_dayEnd.Upd_cre_by = GlbUserName;
                //_dayEnd.Upd_ov_wrt = false;

                //int X = CHNLSVC.Financial.SaveDayEnd(_dayEnd);

                Boolean _isOK = calc();
                if (_isOK == true)
                {
                    BindSummaryGridData(GlbUserDefProf, Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(txtDate.Text));

                    //btnConfirm.Enabled = true;
                    DayEnd _dayEnd = new DayEnd();
                    _dayEnd.Upd_com = GlbUserComCode;
                    _dayEnd.Upd_pc = GlbUserDefProf;
                    _dayEnd.Upd_dt = Convert.ToDateTime(txtDate.Text);
                    _dayEnd.Upd_cre_dt = DateTime.Now;
                    _dayEnd.Upd_cre_by = GlbUserName;
                    _dayEnd.Upd_ov_wrt = false;

                    int X = CHNLSVC.Financial.SaveDayEnd(_dayEnd);
                }
                else
                {
                    clearAll();
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot process short !");
                }
                //imgFromDate.Visible = false;
            }
            catch (UIValidationException ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            }
            catch (Exception e1)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e1.Message);
            }
        }

        private void clearAll()
        {
            gvRec.DataSource = null;
            gvRec.DataBind();

            gvSum.DataSource = null;
            gvSum.DataBind();

            gvDisb.DataSource = null;
            gvDisb.DataBind();

            gvLess.DataSource = null;
            gvLess.DataBind();

            lbl_banked.Text = "0.00";
            lbl_CIH.Text = "0.00";
            lbl_comm_wdr.Text = "0.00";
            lbl_diff.Text = "0.00";
            lbl_TotRem.Text = "0.00";

            txtDisbTot.Text = "0.00";
            txtRecTot.Text = "0.00";
            txtLessTot.Text = "0.00";
            txtSumTot.Text = "0.00";

        }

        private Boolean calc()
        {
            //prv day excess
            Boolean _isSuccess = true;
            Decimal _prvDayExcess = 0;
            Decimal _excessRem = 0;
            int Y = CHNLSVC.Financial.GetPrvDayExcess(Convert.ToDateTime(txtDate.Text), GlbUserDefProf, out _prvDayExcess);

            //total deductions (sum of section 03 without gross remitance and Commission Withdrawn)
            Decimal _totDeduct = 0;
            int Z = CHNLSVC.Financial.GetTotDeductions(Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(txtDate.Text), GlbUserDefProf, out _totDeduct);

            //excess remitance for the day (tot remitance + prev. day excess - tot disbursement + commision withdrawn)
            Decimal _totDisb = 0;
            int K = CHNLSVC.Financial.GetTotDisbForCalc_Excess(Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(txtDate.Text), GlbUserDefProf, out _totDisb);
            _excessRem = _totDisb + _prvDayExcess - _totDeduct;

//            _excessRem = _totDisb + _prvDayExcess - _totDeduct - Convert.ToDecimal(txtColBonus.Text);
            _excessRem = _totDisb + _prvDayExcess - _totDeduct;

            //check whether comm withdrawn is exceeds the excess remitance
            if (_excessRem > 0)
            {
                if ( _excessRem*1000 > Convert.ToDecimal(3))
                {
                    if (Convert.ToDecimal(txtColBonus.Text) > _excessRem)
                    {
                        _isSuccess = false;
                    }
                    else
                    {
                        _isSuccess = true;
                        _excessRem = _totDisb + _prvDayExcess - _totDeduct - Convert.ToDecimal(txtColBonus.Text);
                    }
                }
            }
            //remitance to be banked
            Decimal _totSum = Convert.ToDecimal(txtSumTot.Text) + _excessRem;
            Decimal _totLess = Convert.ToDecimal(txtLessTot.Text);
            lbl_banked.Text = (_totSum - _totLess).ToString("0,0.00", CultureInfo.InvariantCulture);

            //cash in hand
            Decimal _CIH = Convert.ToDecimal(txtCIH.Text);
            lbl_CIH.Text = _CIH.ToString("0,0.00", CultureInfo.InvariantCulture);

            //total remitance entered by manually 
            Decimal _totRemManual = 0;
            Decimal _totRemManualFinal = 0;
            DataTable _tbl = CHNLSVC.Financial.GetRemSummaryReport(GlbUserDefProf, Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(txtDate.Text), "05", out _totRemManual, out _totRemManualFinal);

            //total remitance
            Decimal _totRem = 0;
            int X = CHNLSVC.Financial.GetTotRemitance(Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(txtDate.Text), GlbUserDefProf, out _totRem);
            _totRem = _totRem + _totRemManual;
            lbl_TotRem.Text = (_totRem).ToString("0,0.00", CultureInfo.InvariantCulture);

            Decimal _weekNo = 0;
            _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(txtDate.Text), out _weekNo);

            DataTable dtESD_EPF_WHT = new DataTable();
            dtESD_EPF_WHT = CHNLSVC.Sales.Get_ESD_EPF_WHT(GlbUserComCode, GlbUserDefProf, Convert.ToDateTime(txtDate.Text));

            Decimal ESD_rt = 0; Decimal EPF_rt = 0; Decimal WHT_rt = 0;
            if (dtESD_EPF_WHT.Rows.Count > 0)
            {
                ESD_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_ESD"]);
                EPF_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_EPF"]);
                WHT_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_WHT"]);

            }

            //add total remitance into gnt_rem_sum table (sec-99 , code-001) use by shani
            RemitanceSummaryDetail _remSumDet001 = new RemitanceSummaryDetail();

            _remSumDet001.Rem_com = GlbUserComCode;
            _remSumDet001.Rem_pc = GlbUserDefProf;
            _remSumDet001.Rem_dt = Convert.ToDateTime(txtDate.Text);
            _remSumDet001.Rem_sec = "99";
            _remSumDet001.Rem_cd = "001";
            _remSumDet001.Rem_sh_desc = "Total Remmitance";
            _remSumDet001.Rem_lg_desc = "Total Remmitance";
            _remSumDet001.Rem_val = _totRem;
            _remSumDet001.Rem_val_final = _totRem;
            _remSumDet001.Rem_week = (_weekNo + "S").ToString();
            _remSumDet001.Rem_ref_no = "";
            _remSumDet001.Rem_rmk = "";
            _remSumDet001.Rem_cr_acc = "";
            _remSumDet001.Rem_db_acc = "";
            _remSumDet001.Rem_del_alw = false;
            _remSumDet001.Rem_cre_by = GlbUserName;
            _remSumDet001.Rem_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
            _remSumDet001.Rem_is_sos = false;
            _remSumDet001.Rem_is_dayend = true;
            _remSumDet001.Rem_is_sun = false;
            _remSumDet001.Rem_is_rem_sum = true;
            _remSumDet001.Rem_cat = 12;
            _remSumDet001.Rem_add = 0;
            _remSumDet001.Rem_ded = 0;
            _remSumDet001.Rem_net = 0;
            _remSumDet001.Rem_epf = EPF_rt;
            _remSumDet001.Rem_esd = ESD_rt;
            _remSumDet001.Rem_wht = WHT_rt;
            _remSumDet001.Rem_add_fin = 0;
            _remSumDet001.Rem_ded_fin = 0;
            _remSumDet001.Rem_net_fin = 0;
            _remSumDet001.Rem_rmk_fin = "";
            _remSumDet001.Rem_bnk_cd = "";

            //save total remmitance
            int row_aff2 = CHNLSVC.Financial.SaveRemSummaryDetails(_remSumDet001);

            //difference
            lbl_diff.Text = (_totSum - _totLess - _CIH - _totRem).ToString("0,0.00", CultureInfo.InvariantCulture);

            //add difference into gnt_rem_sum table (sec-99 , code-002)  use by shani
            RemitanceSummaryDetail _remSumDet002 = new RemitanceSummaryDetail();

            _remSumDet001.Rem_com = GlbUserComCode;
            _remSumDet001.Rem_pc = GlbUserDefProf;
            _remSumDet001.Rem_dt = Convert.ToDateTime(txtDate.Text);
            _remSumDet001.Rem_sec = "99";
            _remSumDet001.Rem_cd = "002";
            _remSumDet001.Rem_sh_desc = "Difference";
            _remSumDet001.Rem_lg_desc = "Difference";
            _remSumDet001.Rem_val = Convert.ToDecimal(lbl_diff.Text);
            _remSumDet001.Rem_val_final = Convert.ToDecimal(lbl_diff.Text);
            _remSumDet001.Rem_week = (_weekNo + "S").ToString();
            _remSumDet001.Rem_ref_no = "";
            _remSumDet001.Rem_rmk = "";
            _remSumDet001.Rem_cr_acc = "";
            _remSumDet001.Rem_db_acc = "";
            _remSumDet001.Rem_del_alw = false;
            _remSumDet001.Rem_cre_by = GlbUserName;
            _remSumDet001.Rem_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
            _remSumDet001.Rem_is_sos = false;
            _remSumDet001.Rem_is_dayend = true;
            _remSumDet001.Rem_is_sun = false;
            _remSumDet001.Rem_is_rem_sum = true;
            _remSumDet001.Rem_cat = 12;
            _remSumDet001.Rem_add = 0;
            _remSumDet001.Rem_ded = 0;
            _remSumDet001.Rem_net = 0;
            _remSumDet001.Rem_epf = EPF_rt;
            _remSumDet001.Rem_esd = ESD_rt;
            _remSumDet001.Rem_wht = WHT_rt;
            _remSumDet001.Rem_add_fin = 0;
            _remSumDet001.Rem_ded_fin = 0;
            _remSumDet001.Rem_net_fin = 0;
            _remSumDet001.Rem_rmk_fin = "";
            _remSumDet001.Rem_bnk_cd = "";

            //save difference
            int row_aff3 = CHNLSVC.Financial.SaveRemSummaryDetails(_remSumDet001);

            //commission withdrawn
            Decimal _commWdr = Convert.ToDecimal(txtColBonus.Text);
            lbl_comm_wdr.Text = _commWdr.ToString("0,0.00", CultureInfo.InvariantCulture);

            //save excess remitance----------------------------------------------------------------------------------------------------
            RemitanceSummaryDetail _remSumDet = new RemitanceSummaryDetail();

            _remSumDet.Rem_com = GlbUserComCode;
            _remSumDet.Rem_pc = GlbUserDefProf;
            _remSumDet.Rem_dt = Convert.ToDateTime(txtDate.Text);
            _remSumDet.Rem_sec = "03";
            _remSumDet.Rem_cd = "011";
            _remSumDet.Rem_sh_desc = "Excess Remitance";
            _remSumDet.Rem_lg_desc = "Excess Remitance";
            _remSumDet.Rem_val = _excessRem;
            _remSumDet.Rem_val_final = _excessRem;
            _remSumDet.Rem_week = (_weekNo + "S").ToString();
            _remSumDet.Rem_ref_no = "";
            _remSumDet.Rem_rmk = "";
            _remSumDet.Rem_cr_acc = "";
            _remSumDet.Rem_db_acc = "";
            _remSumDet.Rem_cre_by = GlbUserName;
            _remSumDet.Rem_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
            _remSumDet.Rem_is_sos = false;
            _remSumDet.Rem_is_dayend = true;
            _remSumDet.Rem_is_sun = false;
            _remSumDet.Rem_del_alw = true;
            _remSumDet.Rem_is_rem_sum = false;
            _remSumDet.Rem_cat = 12;
            _remSumDet.Rem_add = 0;
            _remSumDet.Rem_ded = 0;
            _remSumDet.Rem_net = 0;
            _remSumDet.Rem_epf = EPF_rt;
            _remSumDet.Rem_esd = ESD_rt;
            _remSumDet.Rem_wht = WHT_rt;
            _remSumDet.Rem_add_fin = 0;
            _remSumDet.Rem_ded_fin = 0;
            _remSumDet.Rem_net_fin = 0;
            _remSumDet.Rem_rmk_fin = "";
            _remSumDet.Rem_bnk_cd = "";


            //save excess remitance
            int row_aff = CHNLSVC.Financial.SaveRemSummaryDetails(_remSumDet);


            return _isSuccess;
        }

        private void BindReceiptGridData(string _pc, DateTime _fromdate, DateTime _todate)
        {
            Decimal _total = 0;
            Decimal _totalFinal = 0;
            DataTable _tbl = CHNLSVC.Financial.GetRemSummaryReport(_pc, Convert.ToDateTime(_fromdate), Convert.ToDateTime(_todate), "01", out _total, out _totalFinal);

            if (_tbl.Rows.Count <= 0)
            {

                var _tblItems =
                   from dr in _tbl.AsEnumerable()
                   group dr by new { rem_cd = dr["rem_cd"], rem_sh_desc = dr["rem_sh_desc"], rem_val1 = dr["rem_val1"] } into item
                   select new
                   {
                       rem_cd = item.Key.rem_cd,
                       rem_sh_desc = item.Key.rem_sh_desc,
                       rem_val1 = item.Key.rem_val1,

                   };

                gvRec.DataSource = _tblItems;
                txtRecTot.Text = "0.00";
            }
            else
            {
                gvRec.DataSource = CHNLSVC.Financial.GetRemSummaryReport(_pc, Convert.ToDateTime(_fromdate), Convert.ToDateTime(_todate), "01", out _total, out _totalFinal);
                txtRecTot.Text = _total.ToString("0,0.00", CultureInfo.InvariantCulture);
            }
            gvRec.DataBind();

        }

        private void BindDisbursementGridData(string _pc, DateTime _fromdate, DateTime _todate)
        {
            Decimal _total = 0;
            Decimal _totalFinal = 0;
            DataTable _tbl = CHNLSVC.Financial.GetRemSummaryReport(_pc, Convert.ToDateTime(_fromdate), Convert.ToDateTime(_todate), "02", out _total, out _totalFinal);

            if (_tbl.Rows.Count <= 0)
            {

                var _tblItems =
                   from dr in _tbl.AsEnumerable()
                   group dr by new { rem_cd = dr["rem_cd"], rem_sh_desc = dr["rem_sh_desc"], rem_val1 = dr["rem_val1"] } into item
                   select new
                   {
                       rem_cd = item.Key.rem_cd,
                       rem_sh_desc = item.Key.rem_sh_desc,
                       rem_val1 = item.Key.rem_val1,

                   };

                gvDisb.DataSource = _tblItems;
                txtDisbTot.Text = "0.00";
            }
            else
            {
                gvDisb.DataSource = CHNLSVC.Financial.GetRemSummaryReport(_pc, Convert.ToDateTime(_fromdate), Convert.ToDateTime(_todate), "02", out _total, out _totalFinal);
                txtDisbTot.Text = _total.ToString("0,0.00", CultureInfo.InvariantCulture);
            }
            gvDisb.DataBind();

        }

        private void BindSummaryGridData(string _pc, DateTime _fromdate, DateTime _todate)
        {
            Decimal _total = 0;
            Decimal _totalFinal = 0;
            DataTable _tbl = CHNLSVC.Financial.GetRemSummaryReport_without_comm_withdraw(_pc, Convert.ToDateTime(_fromdate), Convert.ToDateTime(_todate), "03", out _total, out _totalFinal, 0);

            if (_tbl.Rows.Count <= 0)
            {

                var _tblItems =
                   from dr in _tbl.AsEnumerable()
                   group dr by new { rem_cd = dr["rem_cd"], rem_sh_desc = dr["rem_sh_desc"], rem_val1 = dr["rem_val1"] } into item
                   select new
                   {
                       rem_cd = item.Key.rem_cd,
                       rem_sh_desc = item.Key.rem_sh_desc,
                       rem_val1 = item.Key.rem_val1,

                   };

                gvSum.DataSource = _tblItems;
                txtSumTot.Text = "0.00";
            }
            else
            {
                gvSum.DataSource = CHNLSVC.Financial.GetRemSummaryReport_without_comm_withdraw(_pc, Convert.ToDateTime(_fromdate), Convert.ToDateTime(_todate), "03", out _total, out _totalFinal, 0);
                txtSumTot.Text = _total.ToString("0,0.00", CultureInfo.InvariantCulture);
            }
            gvSum.DataBind();

        }

        private void BindLessGridData(string _pc, DateTime _fromdate, DateTime _todate)
        {
            Decimal _total = 0;
            Decimal _totalFinal = 0;
            DataTable _tbl = CHNLSVC.Financial.GetRemSummaryReport(_pc, Convert.ToDateTime(_fromdate), Convert.ToDateTime(_todate), "04", out _total, out _totalFinal);

            if (_tbl.Rows.Count <= 0)
            {

                var _tblItems =
                   from dr in _tbl.AsEnumerable()
                   group dr by new { rem_cd = dr["rem_cd"], rem_sh_desc = dr["rem_sh_desc"], rem_val1 = dr["rem_val1"] } into item
                   select new
                   {
                       rem_cd = item.Key.rem_cd,
                       rem_sh_desc = item.Key.rem_sh_desc,
                       rem_val1 = item.Key.rem_val1,

                   };

                gvLess.DataSource = _tblItems;
                txtLessTot.Text = "0.00";
            }
            else
            {
                gvLess.DataSource = CHNLSVC.Financial.GetRemSummaryReport(_pc, Convert.ToDateTime(_fromdate), Convert.ToDateTime(_todate), "04", out _total, out _totalFinal);
                txtLessTot.Text = _total.ToString("0,0.00", CultureInfo.InvariantCulture);
            }
            gvLess.DataBind();

        }

        protected void gvRemLimit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "DELETEITEM":
                    {
                        ImageButton imgbtndelete = (ImageButton)e.CommandSource;
                        GridViewRow gvr = (GridViewRow)imgbtndelete.NamingContainer;
                        int pageindex = gvRemLimit.PageIndex;
                        int pagesize = gvRemLimit.PageSize;
                        int rowIndex = (pageindex * pagesize) + gvr.RowIndex;
                        int delrowIndex = gvr.RowIndex;
                        RemoveItemFromGrid(rowIndex, delrowIndex);
                        break;
                    }
            }
        }

        private void RemoveItemFromGrid(int rowIndex, int delIndex)
        {

            //remove from the table
            Int32 X = CHNLSVC.Financial.DeleteRemSum(GlbUserComCode, GlbUserDefProf, Convert.ToDateTime(txtDate.Text), Convert.ToString(((Label)gvRemLimit.Rows[delIndex].FindControl("lblSec")).Text), Convert.ToString(((Label)gvRemLimit.Rows[delIndex].FindControl("lblCode")).Text));

            BindGridView();
        }

        protected void CalBonusNet(object sender, EventArgs e)
        {
            txtNet.Text = (Convert.ToDecimal(txtVal.Text) + Convert.ToDecimal(txtAdd.Text) - Convert.ToDecimal(txtDeduct.Text)).ToString();
        }

        protected void Date_change(object sender, EventArgs e)
        {
            loadInit();
            BindGridView();
        }

        protected void LoadGrossBonus(object sender, EventArgs e)
        {
            if (ddlSecDef.SelectedValue.Equals("02") && ddlRemTp.SelectedValue.Equals("013"))
            {
                txtGross.Text = txtVal.Text;
            }
            else
            {
                txtGross.Text = (0).ToString();
            }
            CalBonusNet(null, null);

        }

    }
}