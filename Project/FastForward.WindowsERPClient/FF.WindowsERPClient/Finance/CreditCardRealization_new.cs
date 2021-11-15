using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using FF.BusinessObjects;
using System.Threading;

namespace FF.WindowsERPClient.Finance
{
    public partial class CreditCardRealization_new : FF.WindowsERPClient.Base
    {
        private List<gnt_cred_stmnt_det> _credRealList = null;
        private Int32 punch_type = 0;
        private String _bankCD = "";
        private Int32 seqno = 0;
        public CreditCardRealization_new()
        {
            InitializeComponent();
            lblDtRls.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtPCAdj.Text = BaseCls.GlbUserDefProf;
            txtComp.Text = BaseCls.GlbUserComCode;
            bind_Combo_adjTypes();
            load_bank();
            rdoOffline_CheckedChanged(null, null);
        }

        private void bind_Combo_adjTypes()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("CCT", "CREDIT CARD TRANSACTION");
            //PartyTypes.Add("CS_CHEQUE", "CS SETTLEMENT-CHEQUES");
            PartyTypes.Add("CHG_BK", "CHARGE BACK");
            PartyTypes.Add("REVERSE", "REVERSALS");
            PartyTypes.Add("ADJ_BK_CHG", "ADDITIOANL BANK CHARGES");
            PartyTypes.Add("BNK_REBT", "BANK REBATE");
            //PartyTypes.Add("SAL_TFR", "SALARY TRANSFER");
            PartyTypes.Add("OTHER", "OTHER");

            cmbAdjType.DataSource = new BindingSource(PartyTypes, null);
            cmbAdjType.DisplayMember = "Value";
            cmbAdjType.ValueMember = "Key";
        }

        private void SystemErrorMessage(Exception ex)
        {
            CHNLSVC.CloseChannel();
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Load_header_data()
        {
            Decimal _val = 0;
            txtTotReal.Text = "0.00";
            txtTotPrev.Text = "0.00";
            txtTotCC.Text = "0.00";
            txtTotAdj.Text = "0.00";
            txtCloseBal.Text = "0.00";
            txtStateBal.Text = "0.00";
            txtDiff.Text = "0.00";

            //DataTable _dt = CHNLSVC.Financial.getBankRlsHeader(BaseCls.GlbUserComCode, dtDate.Value.Date, txtAccNo.Text);
            //DataTable _dt = CHNLSVC.Financial.get_cred_realization_Hdr(BaseCls.GlbUserComCode, txtAccNo.Text.Trim(), _bankCD, dtDate.Value.Date, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, lblAccName.Text.ToString());
            DataTable _dt = CHNLSVC.Financial.get_cred_realization_Hdr(BaseCls.GlbUserComCode, lblAccName.Text.Trim(), _bankCD, dtDate.Value.Date, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, lblAccName.Text.ToString());
            if (_dt.Rows.Count > 0)
            {
                if (_dt.Rows[0]["cred_sth_stus"].ToString() == "F")
                {
                    lblStus.Text = "FINALIZED";
                    //btnProcess.Enabled = false;
                    btnSearch.Enabled = true;
                    _val = Convert.ToDecimal(_dt.Rows[0]["cred_sth_clbal"]);
                    txtOpenBal.Text = FormatToCurrency(_val.ToString());
                }
                else
                {
                    lblStus.Text = "PENDING";
                    //btnProcess.Enabled = true;
                    //btnSearch.Enabled = false;
                    _val = Convert.ToDecimal(_dt.Rows[0]["cred_sth_opbal"]);
                    txtOpenBal.Text = FormatToCurrency(_val.ToString());
                }
            }
            else
            {
                lblStus.Text = "PENDING";
                btnProcess.Enabled = true;
            }

            if (_dt.Rows.Count > 0)
            {
                _val = Convert.ToDecimal(_dt.Rows[0]["cred_sth_opbal"]);
                txtOpenBal.Text = FormatToCurrency(_val.ToString());
                _val = Convert.ToDecimal(_dt.Rows[0]["cred_sth_realizes"]);
                txtTotReal.Text = FormatToCurrency(_val.ToString());
                _val = Convert.ToDecimal(_dt.Rows[0]["cred_sth_prv_realize"]);
                txtTotPrev.Text = FormatToCurrency(_val.ToString());
                _val = Convert.ToDecimal(_dt.Rows[0]["cred_sth_cc"]);
                txtTotCC.Text = FormatToCurrency(_val.ToString());
                _val = Convert.ToDecimal(_dt.Rows[0]["cred_sth_adj"]);
                txtTotAdj.Text = FormatToCurrency(_val.ToString());
                _val = Convert.ToDecimal(_dt.Rows[0]["cred_sth_clbal"]);
                txtCloseBal.Text = FormatToCurrency(_val.ToString());
                _val = Convert.ToDecimal(_dt.Rows[0]["cred_sth_state_bal"]);
                txtStateBal.Text = FormatToCurrency(_val.ToString());

            }
            else
            {
                calc();
            }


            load_adjgriddate();
            calc_close_val();

        }

        private void ClearScreen()
        {

            txtOpenBal.Text = "0.00";
            txtTotReal.Text = "0.00";
            txtTotPrev.Text = "0.00";
            txtTotCC.Text = "0.00";
            txtTotAdj.Text = "0.00";
            txtCloseBal.Text = "0.00";
            txtStateBal.Text = "0.00";
            txtDiff.Text = "0.00";
            grvDet.Enabled = true;
            txtAccNo.Text = string.Empty;
            lblAccName.Text = string.Empty;
            lblStus.Text = "PENDING";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearScreen();
            btnAddAdj.Enabled = false;
            btnAddExtra.Enabled = false;
            grvDet.Enabled = true;
            grvDet.DataSource = null;
            btnProcess.Enabled = true;
            chkallmid.Checked = true;
            chkallmid_CheckedChanged(null, null);
            InitializeComponent();
            load_bank();

        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(txtComp.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(txtComp.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotor:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MID_SERCH:
                    {
                        paramsText.Append(cmbbanknew.SelectedValue.ToString() + seperator + punch_type);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
            {
                txtPC.Text = "";
                txtPC.Enabled = false;
                btnProfitCenter.Enabled = false;
            }
            else
            {
                txtPC.Enabled = true;
                txtPC.Focus();
                btnProfitCenter.Enabled = true;
            }

        }

        private void btnProfitCenter_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC;
                _CommonSearch.ShowDialog();
                txtPC.Select();


            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblAccName.Text))
            {
                MessageBox.Show("Please select the account number", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbbanknew.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtFromAmt.Text) || string.IsNullOrEmpty(txtToAmt.Text))
            {
                MessageBox.Show("Please enter the value range", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtFromAmt.Focus();
                return;
            }
            if (chkAll.Checked == false && string.IsNullOrEmpty(txtPC.Text))
            {
                MessageBox.Show("Please select the profit center", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPC.Focus();
                return;
            }
            if (chkAllRemTp.Checked == false && cmbRemTp.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the document type", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Load_header_data();
            grvDet.Enabled = true;
            if (chkOthBank.Checked == true)
            {
                grvDet.Enabled = false;
                goto gvEnable;
            }
            if (lblStus.Text == "FINALIZED")
            {
                grvDet.Enabled = false;
                goto gvEnable;
            }
            if (lblStus.Text == "PENDING")
            {
                grvDet.Enabled = true;
                goto gvEnable;
            }


        gvEnable:

            string _docType = null;
            if (cmbRemTp.SelectedIndex == 0) _docType = "DEPOSIT";
            if (cmbRemTp.SelectedIndex == 1) _docType = "CHEQUE";
            //if (cmbRemTp.SelectedIndex == 2) _docType = "CS_CHEQUE";

            Int32 _rlsStatus = 2;
            if (optR.Checked == true) _rlsStatus = 0;
            if (optNR.Checked == true) _rlsStatus = 1;

            if (chkAll.Checked == true)
            {
                txtPC.Text = "";
            }
            DataTable dt = CHNLSVC.Financial.get_cred_serch_det(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(dtDate.Value).Date, lblAccName.Text, _docType, Convert.ToDecimal(txtFromAmt.Text), Convert.ToDecimal(txtToAmt.Text), _rlsStatus, Convert.ToInt32(chkNIS.Checked), Convert.ToInt32(chkOthBank.Checked), 0, "%" + txtRef.Text + "%", _bankCD, txtAccNo.Text);
            grvDet.AutoGenerateColumns = false;
            grvDet.DataSource = dt;

            set_grid_color();

            Load_header_data();

            check_is_realize();
            btnAddAdj.Enabled = true;
            btnAddExtra.Enabled = true;
            //MessageBox.Show("Done", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void getBank()
        {

            // DataTable odt = CHNLSVC.Financial.GetMIDDETAILS(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtAccNo.Text);
            lblAccBankCd.Text = "";
            //MasterBankAccount _tmpBankAcc = new MasterBankAccount();
            //_tmpBankAcc = CHNLSVC.Sales.GetBankDetails(BaseCls.GlbUserComCode, null, txtAccNo.Text);
            DataTable odt = CHNLSVC.Financial.LOAD_MID_DET(cmbbanknew.SelectedValue.ToString(), txtAccNo.Text);
            if (odt.Rows.Count > 0)
            {
                lblAccName.Text = odt.Rows[0]["mstm_sun_acc"].ToString();
                lblAccBank.Text = odt.Rows[0]["mstm_bank_cd"].ToString();
                if (string.IsNullOrEmpty(lblAccName.Text))
                {
                    lblAccName.Text = "";
                    lblAccBank.Text = "";
                    MessageBox.Show("Acoount details not found", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }


            }

            //if (_tmpBankAcc != null)
            //{
            //    lblAccName.Text = _tmpBankAcc.Msba_acc_desc;
            //    lblAccBank.Text = _tmpBankAcc.Msba_cd;

            //    MasterOutsideParty _tmpMasterParty = new MasterOutsideParty();
            //    _tmpMasterParty = CHNLSVC.Sales.GetOutSidePartyDetailsById(lblAccBank.Text);
            //    lblAccBankCd.Text = _tmpMasterParty.Mbi_cd;
            //}
            else
            {

                lblAccName.Text = "";
                lblAccBank.Text = "";
                MessageBox.Show("Acoount details not found", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;

            }

        }


        private void btn_srch_accno_Click(object sender, EventArgs e)
        {
            try
            {
                ClearScreen();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MID_SERCH);
                DataTable _result = CHNLSVC.CommonSearch.Search_mid_account(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAccNo;
                _CommonSearch.ShowDialog();




                txtAccNo.Select();
                Loda_mid_det();

                getBank();
                grvDet.DataSource = null;
                //btnFinalize.Enabled = false;
                //btnAddAdj.Enabled = false;
                //btnAddExtra.Enabled = false;

                check_month_status();

                grvDet.Enabled = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void Loda_mid_det()
        {

            DataTable newodt = CHNLSVC.Financial.GetMIDDETAILS(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtAccNo.Text.ToString());
        }
        private void chkAllRemTp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllRemTp.Checked == true)
            { cmbRemTp.SelectedIndex = -1; cmbRemTp.Enabled = false; }
            else
                cmbRemTp.Enabled = true;
        }

        private void calc_diff()
        {
            try
            {
                txtDiff.Text = FormatToCurrency((Convert.ToDecimal(txtStateBal.Text) - Convert.ToDecimal(txtCloseBal.Text)).ToString());
            }
            catch (Exception ex)
            {

            }
        }

        private void calc_close_val()
        {
            txtCloseBal.Text = FormatToCurrency((Convert.ToDecimal(txtOpenBal.Text) + Convert.ToDecimal(txtTotReal.Text) + Convert.ToDecimal(txtTotPrev.Text) + Convert.ToDecimal(txtTotCC.Text) + Convert.ToDecimal(txtTotAdj.Text)).ToString());
            txtDiff.Text = FormatToCurrency((Convert.ToDecimal(txtStateBal.Text) - Convert.ToDecimal(txtCloseBal.Text)).ToString());
        }

        private void dtDate_ValueChanged(object sender, EventArgs e)
        {
            //ClearScreen();
            lblDtRls.Text = dtDate.Value.Date.ToString("dd/MMM/yyyy");
            //btnFinalize.Enabled = false;
            //btnAddAdj.Enabled = false;
            //btnAddExtra.Enabled = false;
            //grvDet.Enabled = false;
            //check_month_status();
            //cmbbanknew_Leave(null, null);

            //DataTable odt = new DataTable();
            //grvDet.DataSource = odt;
        }

        private void check_month_status()
        {
            DataTable _dtRL = CHNLSVC.Financial.get_cred_realization_Hdr(BaseCls.GlbUserComCode, lblAccName.Text.Trim(), _bankCD, dtDate.Value.Date.AddDays(-1), BaseCls.GlbUserID, BaseCls.GlbUserSessionID, lblAccName.Text.ToString());
            if (_dtRL.Rows.Count != 0)
            {
                if (_dtRL.Rows[0]["cred_sth_stus"].ToString() == "F")
                {
                    Decimal _val = Convert.ToDecimal(_dtRL.Rows[0]["cred_sth_clbal"]);
                    txtOpenBal.Text = FormatToCurrency(_val.ToString());
                }
            }

            DataTable _dt = CHNLSVC.Financial.get_cred_realization_Hdr(BaseCls.GlbUserComCode, lblAccName.Text.Trim(), _bankCD, dtDate.Value.Date, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, lblAccName.Text.ToString());
            if (_dt.Rows.Count > 0)
            {
                if (_dt.Rows[0]["cred_sth_stus"].ToString() == "F")
                {
                    lblStus.Text = "FINALIZED";
                    btnSearch.Enabled = true;
                    Decimal _val = Convert.ToDecimal(_dt.Rows[0]["cred_sth_clbal"]);
                    txtOpenBal.Text = FormatToCurrency(_val.ToString());
                    btn_remove_finalize.Enabled = true;
                }
                else
                {
                    lblStus.Text = "PENDING";
                    btn_remove_finalize.Enabled = false;
                }
            }
            else
            {
                btn_remove_finalize.Enabled = false;
                lblStus.Text = "PENDING";
                btnProcess.Enabled = true;
            }

        }
        private void btnProcess_Click(object sender, EventArgs e)
        {
            cmbbanknew_Leave(null, null);
            DataTable dtnew = new DataTable();
            DataTable dt = new DataTable();
            DataTable dtall = new DataTable();
            DataTable _dtRL = new DataTable();
            DataTable allmid = new DataTable();
            _dtRL = CHNLSVC.Financial.get_cred_realization_Hdr(BaseCls.GlbUserComCode, lblAccName.Text.Trim(), _bankCD, dtDate.Value.Date.AddDays(-1), BaseCls.GlbUserID, BaseCls.GlbUserSessionID, lblAccName.Text.ToString());
            if (_dtRL.Rows.Count != 0)
            {
                if (_dtRL.Rows[0]["cred_sth_stus"].ToString() == "F")
                {
                    Decimal _val = Convert.ToDecimal(_dtRL.Rows[0]["cred_sth_clbal"]);
                    txtOpenBal.Text = FormatToCurrency(_val.ToString());
                }
                else
                {
                    MessageBox.Show("Previous day is not processed/Finalized", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Previous day is not processed/Finalized", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //if (chkallmid.Checked == false)
            //{
            //if (string.IsNullOrEmpty(txtAccNo.Text))
            //{
            //    MessageBox.Show("Please select the account number", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}
            //}
            if (string.IsNullOrEmpty(lblAccName.Text))
            {
                MessageBox.Show("Please select the account number", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (lblStus.Text == "FINALIZED")
            {
                MessageBox.Show("Already Finalized", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            if (Convert.ToDateTime(dtDate.Value.Date) >= DateTime.Now.Date)
            {
                MessageBox.Show("Cannot process. Invalid date !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //get open bal
            txtOpenBal.Text = "0.00";
            Boolean _OpBalavailable = false;
            //if (chkallmid.Checked == true)
            //{
            DataTable odt = new DataTable();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            punch_type = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MID_SERCH);
            odt = CHNLSVC.CommonSearch.Search_mid_account(_CommonSearch.SearchParams, null, null);
            allmid.Merge(odt);
            punch_type = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MID_SERCH);
            odt = CHNLSVC.CommonSearch.Search_mid_account(_CommonSearch.SearchParams, null, null);
            allmid.Merge(odt);
            _dtRL = CHNLSVC.Financial.get_cred_realization_Hdr(BaseCls.GlbUserComCode, lblAccName.Text.ToString(), _bankCD, dtDate.Value.Date, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, lblAccName.Text.ToString());
            //}
            //else
            //{
            //_dtRL = CHNLSVC.Financial.get_cred_realization_Hdr(BaseCls.GlbUserComCode, lblAccName.Text.Trim(), _bankCD, dtDate.Value.Date.AddDays(-1), BaseCls.GlbUserID, BaseCls.GlbUserSessionID, lblAccName.Text.ToString());
            //if (_dtRL.Rows.Count != 0)
            //{
            //    if (_dtRL.Rows[0]["cred_sth_stus"].ToString() == "F")
            //    {
            //        Decimal _val = Convert.ToDecimal(_dtRL.Rows[0]["cred_sth_clbal"]);
            //        txtOpenBal.Text = FormatToCurrency(_val.ToString());
            //        _OpBalavailable = true;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Previous day is not processed/Finalized", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        return; 
            //    }
            //}
            //else
            //{ 
            // MessageBox.Show("Previous day is not processed/Finalized", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
            //    return; 
            //}
            //}

            if (MessageBox.Show("Are you sure ?", "Credit Cared Realization", MessageBoxButtons.YesNo) == DialogResult.No) return;

            btnProcess.Enabled = false;
            DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dtDate.Value.Month + "/" + dtDate.Value.Year).Date;

            Decimal _wkNo = 0;
            int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(dtDate.Text).Date, out _wkNo, BaseCls.GlbUserComCode);
            if (chkAll.Checked == true)
            {
                txtPC.Text = "";
            }
            //save---------
            //if (chkallmid.Checked == true)
            //{

            if (string.IsNullOrEmpty(lblAccName.Text))
            {
                MessageBox.Show("Please select the account number", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            check_month_status();
            gnt_cred_stmnt_hdr _gnt_cred_stmnt_hdr = new gnt_cred_stmnt_hdr();
            _gnt_cred_stmnt_hdr.cred_sth_com = BaseCls.GlbUserComCode;
            _gnt_cred_stmnt_hdr.cred_sth_dt = dtDate.Value.Date;
            _gnt_cred_stmnt_hdr.cred_sth_bank = _bankCD;
            _gnt_cred_stmnt_hdr.cred_sth_mid = lblAccName.Text.ToString();
            _gnt_cred_stmnt_hdr.cred_sth_accno = lblAccName.Text.ToString();
            _gnt_cred_stmnt_hdr.cred_sth_opbal = Convert.ToDecimal(txtOpenBal.Text);
            _gnt_cred_stmnt_hdr.cred_sth_realizes = 0;
            _gnt_cred_stmnt_hdr.cred_sth_prv_realize = 0;
            _gnt_cred_stmnt_hdr.cred_sth_cc = 0;
            _gnt_cred_stmnt_hdr.cred_sth_adj = 0;
            _gnt_cred_stmnt_hdr.cred_sth_clbal = 0;
            _gnt_cred_stmnt_hdr.cred_sth_state_bal = 0;
            _gnt_cred_stmnt_hdr.cred_sth_cre_by = BaseCls.GlbUserID;
            _gnt_cred_stmnt_hdr.cred_sth_stus = "P";

            _gnt_cred_stmnt_hdr.cred_sth_cre_session = BaseCls.GlbUserSessionID;
            string err = "";
            Int32 eff = CHNLSVC.Financial.save_cred_rls_hdr(_gnt_cred_stmnt_hdr, out err);

            _dtRL = CHNLSVC.Financial.get_cred_realization_Hdr(BaseCls.GlbUserComCode, lblAccName.Text.ToString(), _bankCD, dtDate.Value.Date, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, lblAccName.Text.ToString());
            if (_dtRL.Rows.Count != 0)
            {

                seqno = Convert.ToInt32(_dtRL.Rows[0]["cred_seq"].ToString());
            }

            foreach (DataRow dtRow in allmid.Rows)
            {
                dtnew = CHNLSVC.Financial.Process_cred_realization(BaseCls.GlbUserComCode, txtPC.Text.ToString(), dtDate.Value.Date, dtDate.Value.Date, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, seqno, _bankCD, dtRow["MID"].ToString(), lblAccName.Text.ToString());
                dt.Merge(dtnew);
            }
            //}
            //else
            //{
            //                            gnt_cred_stmnt_hdr _gnt_cred_stmnt_hdr=new gnt_cred_stmnt_hdr ();
            //                            _gnt_cred_stmnt_hdr.cred_sth_com = BaseCls.GlbUserComCode;
            //                            _gnt_cred_stmnt_hdr.cred_sth_dt= dtDate.Value.Date;
            //                            _gnt_cred_stmnt_hdr.cred_sth_bank = _bankCD;
            //                            _gnt_cred_stmnt_hdr.cred_sth_mid=txtAccNo.Text.ToString();
            //                            _gnt_cred_stmnt_hdr.cred_sth_accno = lblAccName.Text.ToString();
            //                            _gnt_cred_stmnt_hdr.cred_sth_opbal = Convert.ToDecimal(txtOpenBal.Text);
            //                            _gnt_cred_stmnt_hdr.cred_sth_realizes=0;
            //                            _gnt_cred_stmnt_hdr.cred_sth_prv_realize=0;
            //                            _gnt_cred_stmnt_hdr.cred_sth_cc=0;
            //                            _gnt_cred_stmnt_hdr.cred_sth_adj=0;
            //                            _gnt_cred_stmnt_hdr.cred_sth_clbal=0;
            //                            _gnt_cred_stmnt_hdr.cred_sth_state_bal=0;
            //                            _gnt_cred_stmnt_hdr.cred_sth_cre_by=BaseCls.GlbUserID;
            //                            _gnt_cred_stmnt_hdr.cred_sth_stus="P";

            //                            _gnt_cred_stmnt_hdr.cred_sth_cre_session = BaseCls.GlbUserSessionID;
            //                            string err = "";
            //    Int32 eff = CHNLSVC.Financial.save_cred_rls_hdr(_gnt_cred_stmnt_hdr,out err);
            //    if (!string.IsNullOrEmpty(err))
            //    {
            //        MessageBox.Show("Error Occurred while processing...\n" + err, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            //        return;
            //    }
            //     _dtRL = CHNLSVC.Financial.get_cred_realization_Hdr(BaseCls.GlbUserComCode, txtAccNo.Text.Trim(), _bankCD, dtDate.Value.Date, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, lblAccName.Text.ToString());
            //     if (_dtRL.Rows.Count != 0)
            //     {

            //         seqno = Convert.ToInt32(_dtRL.Rows[0]["cred_seq"].ToString());
            //     }
            //    dt = CHNLSVC.Financial.Process_cred_realization(BaseCls.GlbUserComCode, txtPC.Text.ToString(), dtDate.Value.Date, dtDate.Value.Date, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, seqno, _bankCD, txtAccNo.Text.ToString(), lblAccName.Text.ToString());
            //}

            //load to grid--------------
            dt = CHNLSVC.Financial.Process_cred_realization(BaseCls.GlbUserComCode, txtPC.Text.ToString(), dtDate.Value.Date, dtDate.Value.Date, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, seqno, _bankCD, lblAccName.Text.ToString(), lblAccName.Text.ToString());

            grvDet.AutoGenerateColumns = false;
            grvDet.DataSource = dt;
            set_grid_color();
            check_is_realize();

            Load_header_data();

            btnSearch.Enabled = true;
            btnAddAdj.Enabled = true;
            btnAddExtra.Enabled = true;
            grvDet.Enabled = true;
            btnProcess.Enabled = true;  //kapila 7/4/2016

            if (dt.Rows.Count > 0 || dtall.Rows.Count > 0)
                MessageBox.Show("Completed", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No data found !", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void check_is_realize()
        {
            foreach (DataGridViewRow row in grvDet.Rows)
            {
                if (Convert.ToString(row.Cells["cred_std_is_realized"].Value) == "YES")
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[10];
                    chk.Value = true;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _credRealList = new List<gnt_cred_stmnt_det>();
            if (lblStus.Text == "FINALIZED")
            {
                MessageBox.Show("Already finalized this date", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (grvDet.Rows.Count == 0)
            {
                MessageBox.Show("No data", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable _dt = CHNLSVC.Financial.get_cred_realization_Hdr(BaseCls.GlbUserComCode, lblAccName.Text.Trim(), _bankCD, dtDate.Value.Date.AddDays(-1), BaseCls.GlbUserID, BaseCls.GlbUserSessionID, lblAccName.Text.ToString());
            if (_dt.Rows.Count > 0)
            {
                if (_dt.Rows[0]["cred_sth_stus"].ToString() == "P")
                {
                    if (Convert.ToDateTime(dtDate.Text) > Convert.ToDateTime("31/Oct/2014"))
                    {
                        if (Convert.ToDateTime(dtDate.Text) != Convert.ToDateTime("01/Nov/2014"))
                        {
                            MessageBox.Show("Previous day not finalized !", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
            }
            else
            {
                if (Convert.ToDateTime(dtDate.Text) > Convert.ToDateTime("31/Oct/2014"))
                {
                    if (Convert.ToDateTime(dtDate.Text) != Convert.ToDateTime("01/Nov/2014"))
                    {
                        MessageBox.Show("Previous day not saved !", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }

            if (chkUpd.Checked == true)
            {
                if (MessageBox.Show("Are you sure you want to update remarks only?", "Credit Cared Realization", MessageBoxButtons.YesNo) == DialogResult.No) return;

                foreach (DataGridViewRow row in grvDet.Rows)
                {
                    gnt_cred_stmnt_det _crdReal = new gnt_cred_stmnt_det();

                    _crdReal.cred_std_com = BaseCls.GlbUserComCode;
                    _crdReal.cred_std_pc = row.Cells["Bstd_pc"].Value.ToString();
                    _crdReal.cred_sth_dt = Convert.ToDateTime(row.Cells["Bstd_dt"].Value);
                    _crdReal.cred_std_accno = lblAccName.Text;
                    _crdReal.cred_std_doc_tp = row.Cells["Bstd_doc_tp"].Value.ToString();
                    _crdReal.cred_std_doc_ref = row.Cells["Bstd_doc_ref"].Value.ToString();

                    if (row.Cells["cred_std_rmk"].Value != null)
                        _crdReal.cred_std_rmk = row.Cells["cred_std_rmk"].Value.ToString();
                    else
                        _crdReal.cred_std_rmk = "";

                    _crdReal.cred_seq = Convert.ToInt32(row.Cells["cred_seq"].Value);

                    _credRealList.Add(_crdReal);
                }

                string _messag = "";
                Int32 _eff1 = CHNLSVC.Financial.UpdatecrdRealizationDet(_credRealList, BaseCls.GlbUserComCode, dtDate.Value.Date, lblAccName.Text, Convert.ToDecimal(txtOpenBal.Text), Convert.ToDecimal(txtTotReal.Text), Convert.ToDecimal(txtTotPrev.Text), Convert.ToDecimal(txtTotCC.Text), Convert.ToDecimal(txtTotAdj.Text), Convert.ToDecimal(txtCloseBal.Text), Convert.ToDecimal(txtStateBal.Text), "P", BaseCls.GlbUserID, lblAccName.Text.ToString(), out _messag, BaseCls.GlbUserSessionID);
                if (_eff1 > 0)
                {
                    MessageBox.Show("Successfully updated !", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    chkUpd.Checked = false;
                }
                else
                    MessageBox.Show(_messag, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                return;
            }

            if (MessageBox.Show("Are you sure ?", "Credit Cared Realization", MessageBoxButtons.YesNo) == DialogResult.No) return;

            foreach (DataGridViewRow row in grvDet.Rows)
            {
                gnt_cred_stmnt_det _gnt_cred_stmnt_det = new gnt_cred_stmnt_det();

                _gnt_cred_stmnt_det.cred_std_com = BaseCls.GlbUserComCode;
                _gnt_cred_stmnt_det.cred_std_pc = row.Cells["cred_std_pc"].Value.ToString();
                _gnt_cred_stmnt_det.cred_sth_dt = Convert.ToDateTime(row.Cells["cred_std_dt"].Value);
                _gnt_cred_stmnt_det.cred_std_accno = lblAccName.Text;
                _gnt_cred_stmnt_det.cred_std_doc_tp = row.Cells["cred_std_doc_tp"].Value.ToString();
                _gnt_cred_stmnt_det.cred_std_doc_desc = row.Cells["cred_std_doc_desc"].Value.ToString();
                _gnt_cred_stmnt_det.cred_std_doc_ref = row.Cells["cred_std_doc_ref"].Value.ToString();
                _gnt_cred_stmnt_det.cred_std_sys_val = Convert.ToDecimal(row.Cells["cred_std_sys_val"].Value);

                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[10];
                _gnt_cred_stmnt_det.cred_std_is_realized = Convert.ToBoolean(chk.Value) == true ? 1 : 0;

                if (!string.IsNullOrEmpty(row.Cells["cred_std_realized_dt"].Value.ToString()))
                    _gnt_cred_stmnt_det.cred_std_realized_dt = Convert.ToDateTime(row.Cells["cred_std_realized_dt"].Value);
                else
                    _gnt_cred_stmnt_det.cred_std_realized_dt = Convert.ToDateTime("31/Dec/2999");

                if (Convert.ToDecimal(row.Cells["Bstd_doc_val_cr"].Value) > 0)
                    _gnt_cred_stmnt_det.cred_std_doc_val = Convert.ToDecimal(row.Cells["Bstd_doc_val_cr"].Value) * -1;
                else
                    _gnt_cred_stmnt_det.cred_std_doc_val = Convert.ToDecimal(row.Cells["Bstd_doc_val_dr"].Value);




                if (row.Cells["cred_std_doc_bank"].Value != null)
                    _gnt_cred_stmnt_det.cred_std_doc_bank = row.Cells["cred_std_doc_bank"].Value.ToString();
                else
                    _gnt_cred_stmnt_det.cred_std_doc_bank = "";

                if (row.Cells["cred_std_rmk"].Value != null)
                    _gnt_cred_stmnt_det.cred_std_rmk = row.Cells["cred_std_rmk"].Value.ToString();
                else
                    _gnt_cred_stmnt_det.cred_std_rmk = "";

                _gnt_cred_stmnt_det.cred_std_deposit_bank = "";
                _gnt_cred_stmnt_det.cred_std_doc_bank_cd = "";
                _gnt_cred_stmnt_det.cred_std_doc_bank_branch = "";
                _gnt_cred_stmnt_det.cred_std_is_no_sun = 0;
                _gnt_cred_stmnt_det.cred_std_is_no_state = row.Cells["bstd_is_no_state"].Value == "YES" ? 1 : 0;
                _gnt_cred_stmnt_det.cred_std_is_scan = 1;
                _gnt_cred_stmnt_det.cred_std_cre_by = BaseCls.GlbUserID;
                _gnt_cred_stmnt_det.cred_std_is_new = 0;
                _gnt_cred_stmnt_det.cred_std_is_extra = 0;
                _gnt_cred_stmnt_det.cred_seq = Convert.ToInt32(row.Cells["cred_seq"].Value);
                _gnt_cred_stmnt_det.cred_std_bnk_chg = Convert.ToDecimal(row.Cells["Bank_charge"].Value);
                _gnt_cred_stmnt_det.cred_std_mid = Convert.ToString(row.Cells["Mid"].Value);
                _gnt_cred_stmnt_det.cred_settlement_amt = Convert.ToDecimal(row.Cells["BSTD_DOC_VAL_DR"].Value) - Convert.ToDecimal(row.Cells["Bank_charge"].Value);
                _gnt_cred_stmnt_det.cred_seq_line = Convert.ToInt32(row.Cells["cred_seq_line"].Value);
                _credRealList.Add(_gnt_cred_stmnt_det);
            }

            string _message = "";
            Int32 _eff = CHNLSVC.Financial.UpdatecrdRealizationDet(_credRealList, BaseCls.GlbUserComCode, dtDate.Value.Date, lblAccName.Text, Convert.ToDecimal(txtOpenBal.Text), Convert.ToDecimal(txtTotReal.Text), Convert.ToDecimal(txtTotPrev.Text), Convert.ToDecimal(txtTotCC.Text), Convert.ToDecimal(txtTotAdj.Text), Convert.ToDecimal(txtCloseBal.Text), Convert.ToDecimal(txtStateBal.Text), "P", BaseCls.GlbUserID, lblAccName.Text.ToString(), out _message, BaseCls.GlbUserSessionID);
            if (_eff > 0)
            {
                MessageBox.Show("Successfully updated !", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                chkUpd.Checked = false;
                if (pnlAdj.Visible != true)
                    btnFinalize.Enabled = true;
            }
            else
                MessageBox.Show(_message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void grvDet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Decimal _totReal = Convert.ToDecimal(txtTotReal.Text);
            Decimal _totRealPrv = Convert.ToDecimal(txtTotPrev.Text);

            if (e.RowIndex != -1 && e.ColumnIndex == 11)
            {
                if (grvDet.Rows[e.RowIndex].DefaultCellStyle.BackColor != Color.LightSkyBlue)
                {
                    if (grvDet.Rows[Convert.ToInt32(e.RowIndex)].Cells["bstd_is_no_state"].Value.ToString() == "YES")
                    {
                        MessageBox.Show("Please NTS first !", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    DataGridViewCheckBoxCell chk = grvDet.Rows[e.RowIndex].Cells[10] as DataGridViewCheckBoxCell;
                    if (grvDet.Rows[Convert.ToInt32(e.RowIndex)].Cells["cred_std_is_realized"].Value.ToString() == "YES")
                    {
                        grvDet.Rows[e.RowIndex].Cells["cred_std_is_realized"].Value = "NO";
                        chk.Value = false;
                        grvDet.Rows[e.RowIndex].Cells["cred_std_realized_dt"].Value = string.Empty;
                        grvDet.Rows[e.RowIndex].Cells["cred_std_is_realized"].Value = "NO";

                        if (dtDate.Value.Date != Convert.ToDateTime(grvDet.Rows[e.RowIndex].Cells["cred_std_dt"].Value))
                            _totRealPrv = _totRealPrv + Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["settlement"].Value);
                        else
                            _totReal = _totReal - ((Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value) - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value)) - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["Bank_charge"].Value));
                        //Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["settlement"].Value);// Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value) - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value);
                    }
                    else
                    {
                        grvDet.Rows[e.RowIndex].Cells["cred_std_is_realized"].Value = "YES";
                        chk.Value = true;
                        grvDet.Rows[e.RowIndex].Cells["cred_std_realized_dt"].Value = dtDate.Value.Date;

                        if (dtDate.Value.Date != Convert.ToDateTime(grvDet.Rows[e.RowIndex].Cells["cred_std_dt"].Value))
                            // _totRealPrv = _totRealPrv - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value) + Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value);
                            _totRealPrv = _totRealPrv - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["settlement"].Value);
                        else
                            _totReal = _totReal + ((Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value) - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value)) - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["Bank_charge"].Value));
                        //Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["settlement"].Value);//Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value) + Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value);
                    }
                }

            }

            if (e.RowIndex != -1 && e.ColumnIndex == 16)
            {
                if (grvDet.Rows[Convert.ToInt32(e.RowIndex)].Cells["cred_std_is_realized"].Value.ToString() != "YES")
                {
                    if (MessageBox.Show("Are you sure ?", "Credit Cared Realization", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                        return;
                }
                if (grvDet.Rows[Convert.ToInt32(e.RowIndex)].Cells["bstd_is_no_state"].Value.ToString() == "YES")
                {
                    grvDet.Rows[e.RowIndex].Cells["bstd_is_no_state"].Value = "NO";
                    if (grvDet.Rows[Convert.ToInt32(e.RowIndex)].Cells["bstd_is_no_state"].Value.ToString() == "YES")
                    {
                        if (dtDate.Value.Date != Convert.ToDateTime(grvDet.Rows[e.RowIndex].Cells["cred_std_dt"].Value))
                            _totRealPrv = _totRealPrv - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value) + Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value);
                        else
                            _totReal = _totReal - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value) + Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value);
                    }
                    else
                    {
                        if (grvDet.Rows[Convert.ToInt32(e.RowIndex)].Cells["cred_std_is_realized"].Value.ToString() == "YES")
                        {
                            if (dtDate.Value.Date != Convert.ToDateTime(grvDet.Rows[e.RowIndex].Cells["cred_std_dt"].Value))
                                _totRealPrv = _totRealPrv + Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value) - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value);
                            else
                                //_totReal = _totReal + Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value) - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value);
                                _totReal = _totReal + ((Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value) - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value)) - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["Bank_charge"].Value));
                            //Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["settlement"].Value); 
                        }

                    }



                }
                else
                {
                    grvDet.Rows[e.RowIndex].Cells["bstd_is_no_state"].Value = "YES";
                    if (grvDet.Rows[Convert.ToInt32(e.RowIndex)].Cells["cred_std_is_realized"].Value.ToString() == "YES")
                    {
                        if (dtDate.Value.Date != Convert.ToDateTime(grvDet.Rows[e.RowIndex].Cells["cred_std_dt"].Value))
                            _totRealPrv = _totRealPrv + Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value) - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value);
                        else
                            //_totReal = _totReal + Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value) - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value);
                            _totReal = _totReal - ((Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value) - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value)) - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["Bank_charge"].Value));
                        //Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["settlement"].Value);
                    }
                }
            }
            if (e.RowIndex != -1 && e.ColumnIndex == 7)
            {
                if (grvDet.Rows[e.RowIndex].DefaultCellStyle.BackColor != Color.LightSkyBlue)
                {
                    if (grvDet.Rows[Convert.ToInt32(e.RowIndex)].Cells["cred_std_is_realized"].Value.ToString() == "YES")
                    {
                        MessageBox.Show("Please unrelized first !", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }
            txtTotReal.Text = FormatToCurrency(_totReal.ToString());
            txtTotPrev.Text = FormatToCurrency(_totRealPrv.ToString());
            calc_close_val();
            //calc_diff();
        }

        private void txtPC_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPC.Text))
            {
                Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, txtPC.Text);
                if (_IsValid == false)
                {
                    MessageBox.Show("Invalid Profit Center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPC.Focus();
                    return;
                }
            }
        }

        private void calc()
        {
            Decimal _totRealPrv = 0;
            Decimal _totReal = 0;

            foreach (DataGridViewRow row in grvDet.Rows)
            {
                DataGridViewCheckBoxCell chk = grvDet.Rows[row.Index].Cells[7] as DataGridViewCheckBoxCell;
                if (grvDet.Rows[Convert.ToInt32(row.Index)].Cells["cred_std_is_realized"].Value.ToString() == "YES")
                {
                    if (grvDet.Rows[row.Index].Cells["cred_std_doc_tp"].Value.ToString() != "SAL_TFR" && grvDet.Rows[row.Index].Cells["cred_std_doc_tp"].Value.ToString() != "CC" && grvDet.Rows[row.Index].Cells["cred_std_doc_tp"].Value.ToString() != "BANK_CHG" && grvDet.Rows[row.Index].Cells["cred_std_doc_tp"].Value.ToString() != "RTN_CHQ" && grvDet.Rows[row.Index].Cells["cred_std_doc_tp"].Value.ToString() != "OTH")
                    {
                        if (Convert.ToDateTime(grvDet.Rows[row.Index].Cells["cred_std_dt"].Value) == (dtDate.Value.Date))
                        {
                            if (grvDet.Rows[row.Index].Cells["bstd_is_no_state"].Value.ToString() == "NO")
                                _totReal = _totReal + Convert.ToDecimal(grvDet.Rows[row.Index].Cells["settlement"].Value);
                            //Convert.ToDecimal(grvDet.Rows[row.Index].Cells["BSTD_DOC_VAL_CR"].Value) + Convert.ToDecimal(grvDet.Rows[row.Index].Cells["BSTD_DOC_VAL_DR"].Value);
                        }
                    }
                }
            }
            txtTotReal.Text = FormatToCurrency(_totReal.ToString());
        }

        private void chkNIS_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNIS.Checked == true)
                optNR.Checked = true;
            else
                optAll.Checked = true;

            grvDet.DataSource = null;
            Load_header_data();
        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtDiff.Text) != 0)
            {
                MessageBox.Show("Cannot finalize with a difference", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStateBal.Focus();
                return;
            }
            foreach (DataGridViewRow row in grvDet.Rows)
            {
                //BankRealDet _bnkReal = new BankRealDet();
                gnt_cred_stmnt_det _bnkReal = new gnt_cred_stmnt_det();
                _bnkReal.cred_std_com = BaseCls.GlbUserComCode;
                _bnkReal.cred_std_pc = row.Cells["cred_std_pc"].Value.ToString();
                _bnkReal.cred_sth_dt = Convert.ToDateTime(row.Cells["cred_std_dt"].Value);
                _bnkReal.cred_std_accno = lblAccName.Text;
                _bnkReal.cred_std_doc_tp = row.Cells["cred_std_doc_tp"].Value.ToString();
                _bnkReal.cred_std_doc_desc = row.Cells["cred_std_doc_desc"].Value.ToString();
                _bnkReal.cred_std_doc_ref = row.Cells["cred_std_doc_ref"].Value.ToString();
                _bnkReal.cred_std_sys_val = Convert.ToDecimal(row.Cells["cred_std_sys_val"].Value);

                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[9];
                _bnkReal.cred_std_is_realized = Convert.ToBoolean(chk.Value) == true ? 1 : 0;

                if (!string.IsNullOrEmpty(row.Cells["cred_std_realized_dt"].Value.ToString()))
                    _bnkReal.cred_std_realized_dt = Convert.ToDateTime(row.Cells["cred_std_realized_dt"].Value);
                else
                    _bnkReal.cred_std_realized_dt = Convert.ToDateTime("31/Dec/2999");

                if (Convert.ToDecimal(row.Cells["Bstd_doc_val_cr"].Value) > 0)
                    _bnkReal.cred_std_doc_val = Convert.ToDecimal(row.Cells["Bstd_doc_val_cr"].Value) * -1;
                else
                    _bnkReal.cred_std_doc_val = Convert.ToDecimal(row.Cells["Bstd_doc_val_dr"].Value);


                if (row.Cells["cred_std_doc_bank"].Value != null)
                    _bnkReal.cred_std_doc_bank = row.Cells["cred_std_doc_bank"].Value.ToString();
                else
                    _bnkReal.cred_std_doc_bank = "";

                if (row.Cells["cred_std_rmk"].Value != null)
                    _bnkReal.cred_std_rmk = row.Cells["cred_std_rmk"].Value.ToString();
                else
                    _bnkReal.cred_std_rmk = "";

                _bnkReal.cred_std_deposit_bank = "";
                _bnkReal.cred_std_doc_bank_cd = "";
                _bnkReal.cred_std_doc_bank_branch = "";
                _bnkReal.cred_std_is_no_sun = 0;
                _bnkReal.cred_std_is_no_state = row.Cells["bstd_is_no_state"].Value == "YES" ? 1 : 0;
                _bnkReal.cred_std_is_scan = 1;
                _bnkReal.cred_std_cre_by = BaseCls.GlbUserID;
                _bnkReal.cred_std_is_new = 0;
                _bnkReal.cred_std_is_extra = 0;
                _bnkReal.cred_seq = Convert.ToInt32(row.Cells["cred_seq"].Value);

                _credRealList.Add(_bnkReal);
            }
            Int32 _eff = CHNLSVC.Financial.Finalize_cred_Realization(BaseCls.GlbUserComCode, dtDate.Value.Date, lblAccName.Text, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, _credRealList, txtAccNo.Text);
            MessageBox.Show("Successfully finalized !", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnFinalize.Enabled = false;
            btnProcess.Enabled = false;
            lblStus.Text = "FINALIZED";

        }

        private void txtStateBal_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStateBal.Text))
                calc_diff();
        }

        private void chkOthBank_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOthBank.Checked == true)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;
        }

        private void btnAdj_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(lblAccName.Text))
            {
                MessageBox.Show("Please select the account number", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            DataTable _dt = CHNLSVC.Financial.get_cred_realization_Hdr(BaseCls.GlbUserComCode, lblAccName.Text.Trim(), _bankCD, dtDate.Value.Date.AddDays(-1), BaseCls.GlbUserID, BaseCls.GlbUserSessionID, lblAccName.Text.ToString());
            if (_dt.Rows.Count > 0)
            {
                if (_dt.Rows[0]["cred_sth_stus"].ToString() == "P")
                {
                    if (Convert.ToDateTime(dtDate.Text) > Convert.ToDateTime("31/Oct/2014"))
                    {
                        if (Convert.ToDateTime(dtDate.Text) != Convert.ToDateTime("01/Nov/2014"))
                        {
                            MessageBox.Show("Previous day not finalized !", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
            }
            else
            {
                if (Convert.ToDateTime(dtDate.Text) > Convert.ToDateTime("31/Oct/2014"))
                {
                    if (Convert.ToDateTime(dtDate.Text) != Convert.ToDateTime("01/Nov/2014"))
                    {
                        MessageBox.Show("Previous day not saved !", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }
            btnSave_Click(null, null);
            pnlAcc.Enabled = false;
            pnlBtn.Enabled = false;
            pnlFilter.Enabled = false;
            grvDet.Enabled = false;

            pnlAdj.Visible = true;
            if (lblStus.Text == "FINALIZED")
                btnAddAdj.Enabled = false;
            else
                btnAddAdj.Enabled = true;
            txtPCAdj.Focus();

        }

        private void load_adjgriddate()
        {
            Decimal _totAdj = 0;

            DataTable _DT = CHNLSVC.Financial.Get_cred_Adj(BaseCls.GlbUserComCode, dtDate.Value.Date, lblAccName.Text);
            grvAdj.AutoGenerateColumns = false;
            grvAdj.DataSource = _DT;

            foreach (DataGridViewRow row in grvAdj.Rows)
            {
                if (row.Cells["cred_sta_adj_tp"].Value.ToString() == "SAL_TFR" || row.Cells["cred_sta_adj_tp"].Value.ToString() == "CC" || row.Cells["cred_sta_adj_tp"].Value.ToString() == "BANK_CHG" || row.Cells["cred_sta_adj_tp"].Value.ToString() == "RTN_CHQ" || row.Cells["cred_sta_adj_tp"].Value.ToString() == "OTH")
                {
                    _totAdj = _totAdj + Convert.ToDecimal(row.Cells["cred_sta_amt"].Value);
                }
            }
            txtTotAdj.Text = FormatToCurrency(_totAdj.ToString());

            Decimal _totCC = 0;
            int X = CHNLSVC.Financial.GetCCRecTot_cred(BaseCls.GlbUserComCode, dtDate.Value.Date, lblAccName.Text, out _totCC);
            txtTotCC.Text = FormatToCurrency(_totCC.ToString());


        }

        private void set_grid_color()
        {
            foreach (DataGridViewRow row in grvDet.Rows)
            {
                if (Convert.ToString(row.Cells["cred_std_is_extra"].Value) == "1")
                {
                    row.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    row.ReadOnly = true;
                }
                if (Convert.ToDateTime(row.Cells["cred_std_dt"].Value) != dtDate.Value.Date)
                {
                    row.DefaultCellStyle.BackColor = Color.PeachPuff;
                }
            }
        }

        private void btnCloseAdj_Click(object sender, EventArgs e)
        {
            pnlAcc.Enabled = true;
            pnlBtn.Enabled = true;
            pnlFilter.Enabled = true;
            grvDet.Enabled = true;
            pnlAdj.Visible = false;
        }

        private void btnSrchPc_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtComp.Text))
                {
                    MessageBox.Show("Please select the company", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtComp.Focus();
                    return;
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPCAdj;
                _CommonSearch.ShowDialog();
                txtPCAdj.Select();


            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAddAdj_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAdjRef.Text))
            {
                MessageBox.Show("Please enter the reference #", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAdjRef.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtadjmid.Text))
            {
                MessageBox.Show("Please enter the MID #", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btn_serch_mid.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPCAdj.Text))
            {
                MessageBox.Show("Please select the profit center", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPCAdj.Focus();
                return;
            }
            if (cmbAdjType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the adjustment type", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtAdjAmt.Text) || Convert.ToDecimal(txtAdjAmt.Text) < 0)
            {
                MessageBox.Show("Please enter the amount", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(txtComp.Text, txtPCAdj.Text);
            if (_IsValid == false)
            {
                MessageBox.Show("Invalid Profit Center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPCAdj.Focus();
                return;
            }

            //if (CHNLSVC.Financial.checkDoc_credk_State(txtPCAdj.Text, dtDate.Value.Date, cmbAdjType.SelectedValue.ToString(), txtAdjRef.Text) == true)
            //{
            //    MessageBox.Show("Cannot add. This reference # already exists.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    txtAdjRef.Focus();
            //    return;
            //}

            if (MessageBox.Show("Are you sure ?", "Credit Cared Realization", MessageBoxButtons.YesNo) == DialogResult.No) return;

            Decimal _adjAmt = 0;
            if (optDr.Checked == true)
                _adjAmt = Convert.ToDecimal(txtAdjAmt.Text);
            else
                _adjAmt = Convert.ToDecimal(txtAdjAmt.Text) * -1;

            DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dtDate.Value.Month + "/" + dtDate.Value.Year).Date;

            Decimal _wkNo = 0;
            int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(dtDate.Text).Date, out _wkNo, txtComp.Text);

            //update scan physical-----------------------------------------
            ScanPhysicalDocReceiveDet DOC = new ScanPhysicalDocReceiveDet();
            DOC.Grdd_rcv_by = BaseCls.GlbUserID;
            DOC.Grdd_rcv_dt = DateTime.Now.Date;
            DOC.Grdd_doc_rcv = true;
            DOC.Grdd_rmk = txtAdjRem.Text.Trim();
            DOC.Grdd_doc_val = Convert.ToDecimal(txtAdjAmt.Text);
            DOC.Grdd_dt = dtDate.Value.Date;
            DOC.Grdd_month = Convert.ToDateTime("01" + "/" + dtDate.Value.Month + "/" + dtDate.Value.Year).Date;
            DOC.Grdd_week = Convert.ToInt32(_wkNo);
            DOC.Grdd_is_extra = true;
            DOC.Grdd_short_ref = 0;
            DOC.Grdd_doc_bank = lblAccBank.Text;
            DOC.Grdd_com = txtComp.Text;
            DOC.Grdd_cre_by = BaseCls.GlbUserID;
            DOC.Grdd_doc_bank_cd = _bankCD;
            // DOC.Grdd_deposit_bank = txtAccNo.Text;
            DOC.Grdd_deposit_bank = txtadjmid.Text;
            DOC.Grdd_doc_desc = cmbAdjType.Text;
            DOC.Grdd_doc_tp = cmbAdjType.SelectedValue.ToString();
            DOC.Grdd_pc = txtPCAdj.Text;
            try
            {
                DOC.bsta_bnk_charge = Convert.ToDecimal(txtbankchrage.Text);
            }
            catch (Exception)
            {

                DOC.bsta_bnk_charge = 0;
            }
            Int32 _eff = CHNLSVC.Financial.Save_cred_Adj(DOC, txtComp.Text, txtPCAdj.Text, dtDate.Value.Date, lblAccName.Text, cmbAdjType.SelectedValue.ToString(), cmbAdjType.Text.ToString(), _adjAmt, txtAdjRef.Text, txtAdjRem.Text, BaseCls.GlbUserID, _tmpMonth, Convert.ToInt32(_wkNo), "", _bankCD, txtadjmid.Text.ToString(), seqno);
            if (_eff != 0)
            {
                // DataTable dt = CHNLSVC.Financial.get_cred_serch_det(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(dtDate.Value).Date, txtAccNo.Text, null, 0, 0, 2, 0, 0, Convert.ToInt32(chkWithNIS.Checked), txtRef.Text, _bankCD);
                DataTable dt = CHNLSVC.Financial.Process_cred_realization(BaseCls.GlbUserComCode, txtPC.Text.ToString(), dtDate.Value.Date, dtDate.Value.Date, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, seqno, _bankCD, lblAccName.Text.ToString(), lblAccName.Text.ToString());

                grvDet.AutoGenerateColumns = false;
                grvDet.DataSource = dt;

                calc();

                set_grid_color();
                check_is_realize();

                load_adjgriddate();

                clear_adj();

                calc_close_val();

                //Int32 _ef = CHNLSVC.Financial.UpdateBankRealHdr(BaseCls.GlbUserComCode, dtDate.Value.Date, txtAccNo.Text, Convert.ToDecimal(txtOpenBal.Text), Convert.ToDecimal(txtTotReal.Text), Convert.ToDecimal(txtTotPrev.Text), Convert.ToDecimal(txtTotCC.Text), Convert.ToDecimal(txtTotAdj.Text), Convert.ToDecimal(txtCloseBal.Text), Convert.ToDecimal(txtStateBal.Text), "P", BaseCls.GlbUserID);

                MessageBox.Show("Successfully updated !", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void clear_adj()
        {
            txtAdjAmt.Text = "";
            txtAdjRef.Text = "";
            txtAdjRem.Text = "";
            txtadjmid.Text = "";
        }

        private void clear_extra()
        {
            txtExtraAmt.Text = "";
            txtExtraRef.Text = "";
            txtExtraRem.Text = "";
        }

        private void btnExtraDocs_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAccNo.Text))
            {
                MessageBox.Show("Please select the account number", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            pnlAcc.Enabled = false;
            pnlBtn.Enabled = false;
            pnlFilter.Enabled = false;
            pnlExtra.Visible = true;
            pnlExtra.Location = new Point(10, 255);
            txtPCExtra.Focus();

        }

        private void btnAddExtra_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPCExtra.Text))
            {
                MessageBox.Show("Please select the profit center", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPCExtra.Focus();
                return;
            }
            if (cmbDocType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the document type", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtExtraAmt.Text) || Convert.ToDecimal(txtExtraAmt.Text) < 0)
            {
                MessageBox.Show("Please enter the amount", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtExtraAmt.Focus();
                return;
            }
            Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, txtPCExtra.Text);
            if (_IsValid == false)
            {
                MessageBox.Show("Invalid Profit Center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPCAdj.Focus();
                return;
            }

            if (MessageBox.Show("Are you sure ?", "Credit Cared Realization", MessageBoxButtons.YesNo) == DialogResult.No) return;


            ScanPhysicalDocReceiveDet DOC = new ScanPhysicalDocReceiveDet();
            //DOC = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC_on_Seq(_ShortRef);
            DOC.Grdd_rcv_by = BaseCls.GlbUserID;
            DOC.Grdd_rcv_dt = DateTime.Now.Date;
            DOC.Grdd_doc_rcv = true;
            DOC.Grdd_rmk = txtExtraRem.Text.Trim();
            DOC.Grdd_doc_val = Convert.ToDecimal(txtExtraAmt.Text);
            DOC.Grdd_dt = dtDate.Value.Date;
            DOC.Grdd_month = Convert.ToDateTime("01" + "/" + dtDate.Value.Month + "/" + dtDate.Value.Year).Date;

            Decimal week = 0;
            CHNLSVC.General.GetWeek(Convert.ToDateTime(dtDate.Value.Date).Date, out week, BaseCls.GlbUserComCode);
            DOC.Grdd_week = Convert.ToInt32(week);

            DOC.Grdd_is_extra = true;
            DOC.Grdd_short_ref = 0;
            DOC.Grdd_doc_bank = txtAccNo.Text;

            _credRealList = new List<gnt_cred_stmnt_det>();
            gnt_cred_stmnt_det _cedReal = new gnt_cred_stmnt_det();
            _cedReal.cred_std_com = BaseCls.GlbUserComCode;
            _cedReal.cred_std_pc = txtPCExtra.Text;
            _cedReal.cred_sth_dt = Convert.ToDateTime(dtDate.Value);
            _cedReal.cred_std_accno = lblAccName.Text;
            _cedReal.cred_std_doc_tp = cmbDocType.SelectedValue.ToString();
            _cedReal.cred_std_doc_desc = cmbDocType.Text;
            _cedReal.cred_std_doc_ref = txtExtraRef.Text;
            _cedReal.cred_std_sys_val = Convert.ToDecimal(txtExtraAmt.Text);
            _cedReal.cred_std_is_realized = 0;
            _cedReal.cred_std_realized_dt = Convert.ToDateTime("31/Dec/9999");
            _cedReal.cred_std_doc_val = Convert.ToDecimal(txtExtraAmt.Text);

            _cedReal.cred_std_doc_bank = "";

            _cedReal.cred_std_rmk = txtExtraRem.Text;
            _cedReal.cred_std_deposit_bank = "";
            _cedReal.cred_std_doc_bank_cd = "";
            _cedReal.cred_std_doc_bank_branch = "";
            _cedReal.cred_std_is_no_sun = 0;
            _cedReal.cred_std_is_no_state = 0;
            _cedReal.cred_std_is_scan = 1;
            _cedReal.cred_std_cre_by = BaseCls.GlbUserID;
            _cedReal.cred_std_is_new = 0;
            _cedReal.cred_std_is_extra = 0;

            _credRealList.Add(_cedReal);

            string _msg = "";
            int eff = 0;
            //CHNLSVC.Financial.UpdateBankRealizationDetails(DOC, _credRealList, out _msg);
            if (eff != 0)
            {

                DataTable dt = CHNLSVC.Financial.get_bank_realization_det(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(dtDate.Value).Date, txtAccNo.Text, null, Convert.ToDecimal(txtFromAmt.Text), Convert.ToDecimal(txtToAmt.Text), 2, Convert.ToInt32(chkNIS.Checked), Convert.ToInt32(chkOthBank.Checked), Convert.ToInt32(chkWithNIS.Checked), txtRef.Text);
                grvDet.AutoGenerateColumns = false;
                grvDet.DataSource = dt;
                clear_extra();
                calc_close_val();
                MessageBox.Show("Successfully updated !", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void load_Extra_doc_grid_data()
        {
            //DataTable dt = CHNLSVC.Financial.Get_ShortBankDocs(BaseCls.GlbUserComCode, txtPCExtra.Text, Convert.ToDateTime("01" + "/" + dtDate.Value.Month + "/" + dtDate.Value.Year).Date, Convert.ToInt32(ddExtraDoclWeek.SelectedIndex + 1), null);
            //grvExtra.DataSource = null;
            //grvExtra.AutoGenerateColumns = false;
            //grvExtra.DataSource = dt;

        }
        private void btnSrchExtraPC_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPCExtra;
                _CommonSearch.ShowDialog();
                txtPCExtra.Select();


            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtPCAdj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchPc_Click(null, null);
        }

        private void txtPCExtra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchExtraPC_Click(null, null);
        }

        private void btnCloseExtra_Click(object sender, EventArgs e)
        {
            pnlAcc.Enabled = true;
            pnlBtn.Enabled = true;
            pnlFilter.Enabled = true;
            pnlExtra.Visible = false;
        }

        private void txtAdjAmt_Leave(object sender, EventArgs e)
        {
            if (txtAdjAmt.Text != "")
            {
                decimal val;
                if (!decimal.TryParse(txtAdjAmt.Text, out val))
                {
                    MessageBox.Show("Amount has to be in number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAdjAmt.Focus();
                    return;
                }
            }
        }

        private void txtFromAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

        }

        private void txtToAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtAdjAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtStateBal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

        }

        private void grvDet_Sorted(object sender, EventArgs e)
        {
            check_is_realize();
            set_grid_color();
        }

        private void txtFromAmt_TextChanged(object sender, EventArgs e)
        {
            txtToAmt.Text = txtFromAmt.Text;
        }

        private void txtFromAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(null, null);
        }


        private void grvDet_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 5)
            {
                if (grvDet.Rows[Convert.ToInt32(e.RowIndex)].Cells["cred_std_is_realized"].Value.ToString() == "YES")
                {
                    MessageBox.Show("Cannot change the value. Already realized !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SendKeys.Send("{TAB}");
                }
            }
            if (e.RowIndex != -1 && e.ColumnIndex == 4)
            {
                if (grvDet.Rows[Convert.ToInt32(e.RowIndex)].Cells["cred_std_is_realized"].Value.ToString() == "YES")
                {
                    MessageBox.Show("Cannot change the value. Already realized !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SendKeys.Send("{TAB}");
                }
            }

        }

        private void txtPCAdj_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPCAdj.Text))
            {
                Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(txtComp.Text, txtPCAdj.Text);
                if (_IsValid == false)
                {
                    MessageBox.Show("Invalid Profit Center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPCAdj.Focus();
                    return;
                }
            }
        }

        private void btn_srch_comp_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
            DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtComp;
            _CommonSearch.ShowDialog();
            txtPCAdj.Text = "";
            txtComp.Select();
        }

        private void grvDet_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 1)
            {
                if (Convert.ToDateTime(grvDet.Rows[e.RowIndex].Cells["cred_std_dt"].Value) > Convert.ToDateTime(dtDate.Value))
                {
                    MessageBox.Show("Invalid realize date !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    grvDet.Rows[e.RowIndex].Cells["cred_std_dt"].Value = dtDate.Value.Date;
                    SendKeys.Send("{TAB}");
                }
            }
            if (e.RowIndex != -1 && e.ColumnIndex == 8)
            {

                grvDet.Rows[e.RowIndex].Cells["settlement"].Value = Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value) - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["Bank_charge"].Value);
                //if (Convert.ToDateTime(grvDet.Rows[e.RowIndex].Cells["cred_std_dt"].Value) > Convert.ToDateTime(dtDate.Value))
                //{
                //    MessageBox.Show("Invalid realize date !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    grvDet.Rows[e.RowIndex].Cells["cred_std_dt"].Value = dtDate.Value.Date;
                //    SendKeys.Send("{TAB}");
                //}
            }
            if ((e.RowIndex != -1 && e.ColumnIndex == 7) || (e.RowIndex != -1 && e.ColumnIndex == 6))
            {

                if (grvDet.Rows[e.RowIndex].DefaultCellStyle.BackColor != Color.LightSkyBlue)
                {
                    if (grvDet.Rows[Convert.ToInt32(e.RowIndex)].Cells["cred_std_is_realized"].Value.ToString() == "YES")
                    {
                        MessageBox.Show("Please un-realize to change the bank charge percentage!", "Credit Cared Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        grvDet.Rows[e.RowIndex].Cells["Bank_Change_pre"].Value = (Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["Bank_charge"].Value) / Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value)) * 100;

                        return;
                    }
                }


                decimal temp, bank_charge_temp = 0;
                try
                {
                    temp = Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["Bank_Change_pre"].Value);
                    if (temp > 100 || temp < 0)
                    {
                        MessageBox.Show("Please provide number only");
                        grvDet.Rows[e.RowIndex].Cells["Bank_Change_pre"].Value = "0";
                        return;
                    }
                    if (temp != 0)
                    {
                        bank_charge_temp = (Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value) * temp) / 100;
                        grvDet.Rows[e.RowIndex].Cells["Bank_Change_pre"].Value = temp;
                        grvDet.Rows[e.RowIndex].Cells["Bank_charge"].Value = bank_charge_temp;
                        decimal DOC_VAL_DR = Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value);
                        grvDet.Rows[e.RowIndex].Cells["settlement"].Value = Convert.ToString((DOC_VAL_DR - bank_charge_temp) - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value));
                    }
                    else
                    {

                        grvDet.Rows[e.RowIndex].Cells["Bank_Change_pre"].Value = "0";
                        grvDet.Rows[e.RowIndex].Cells["Bank_charge"].Value = "0";
                        grvDet.Rows[e.RowIndex].Cells["settlement"].Value = grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value;
                    }

                }
                catch (Exception h)
                {
                    MessageBox.Show("Please provide number only");
                    grvDet.Rows[e.RowIndex].Cells["Bank_Change_pre"].Value = "0";
                    grvDet.Rows[e.RowIndex].Cells["Bank_charge"].Value = "0";

                    return;
                }
                if (temp == 0)
                {

                }

            }
        }

        private void grvAdj_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 6)
            {
                if (grvAdj.Rows[e.RowIndex].Cells["cred_sta_pc"].Value.ToString() != "AARCL" && grvAdj.Rows[e.RowIndex].Cells["cred_sta_pc"].Value.ToString() != "SDRHO" && grvAdj.Rows[e.RowIndex].Cells["cred_sta_pc"].Value.ToString() != "SGRHO" && grvAdj.Rows[e.RowIndex].Cells["cred_sta_pc"].Value.ToString() != "RRCL" && grvAdj.Rows[e.RowIndex].Cells["cred_sta_pc"].Value.ToString() != "VRCL" && grvAdj.Rows[e.RowIndex].Cells["cred_sta_pc"].Value.ToString() != "PNGHO")
                {
                    MessageBox.Show("Cannot change !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SendKeys.Send("{TAB}");
                }
            }
        }

        private void btnUpdatePC_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ?", "Credit Cared Realization", MessageBoxButtons.YesNo) == DialogResult.No) return;

            //try
            //{
            foreach (DataGridViewRow row in grvAdj.Rows)
            {
                if (grvAdj.Rows[row.Index].Cells["SR"].Value != null)
                {
                    if ((grvAdj.Rows[row.Index].Cells["cred_sta_pc"].Value.ToString() == "AARCL" || grvAdj.Rows[row.Index].Cells["cred_sta_pc"].Value.ToString() == "SGRHO" || grvAdj.Rows[row.Index].Cells["cred_sta_pc"].Value.ToString() == "SDRHO" || grvAdj.Rows[row.Index].Cells["cred_sta_pc"].Value.ToString() == "VRCL" || grvAdj.Rows[row.Index].Cells["cred_sta_pc"].Value.ToString() == "RRCL" || grvAdj.Rows[row.Index].Cells["cred_sta_pc"].Value.ToString() == "PNGHO") && !string.IsNullOrEmpty(grvAdj.Rows[row.Index].Cells["SR"].Value.ToString()))
                    {
                        Int32 _eff = CHNLSVC.Financial.Update_cred_AdjPC(grvAdj.Rows[row.Index].Cells["cred_sta_com"].Value.ToString(), grvAdj.Rows[row.Index].Cells["cred_sta_pc"].Value.ToString(), Convert.ToDateTime(dtDate.Value.Date), txtAccNo.Text, grvAdj.Rows[row.Index].Cells["cred_sta_adj_tp"].Value.ToString(), grvAdj.Rows[row.Index].Cells["cred_sta_refno"].Value.ToString(), grvAdj.Rows[row.Index].Cells["SR"].Value.ToString(), Convert.ToDecimal(grvAdj.Rows[row.Index].Cells["cred_sta_amt"].Value), grvAdj.Rows[row.Index].Cells["cred_sta_refno"].Value.ToString());
                    }
                }

            }
            DataTable _DT = CHNLSVC.Financial.Get_cred_Adj(BaseCls.GlbUserComCode, dtDate.Value.Date, txtAccNo.Text);
            grvAdj.AutoGenerateColumns = false;
            grvAdj.DataSource = _DT;
            //}
            //catch (Exception ex)
            //{

            //}
            MessageBox.Show("Successfully updated !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void grvAdj_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(grvAdj.Rows[e.RowIndex].Cells["SR"].Value.ToString()))
                {
                    Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(grvAdj.Rows[e.RowIndex].Cells["bsta_com"].Value.ToString(), grvAdj.Rows[e.RowIndex].Cells["SR"].Value.ToString());
                    if (_IsValid == false)
                    {
                        MessageBox.Show("Invalid Profit Center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        grvAdj.Rows[e.RowIndex].Cells["SR"].Value = "";
                        SendKeys.Send("{TAB}");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(null, null);
        }

        private void txtAccNo_TextChanged(object sender, EventArgs e)
        {

        }
        private void load_bank()
        {
            DataTable dt = CHNLSVC.Sales.LoadBankNewDets();
            if (dt != null && dt.Rows.Count > 0)
            {

                cmbbanknew.DataSource = dt;
                cmbbanknew.DisplayMember = "MBI_DESC";
                cmbbanknew.ValueMember = "MBI_ID";
                cmbbanknew.Text = "---Select a Bank---";

            }
            else
            {
                cmbbanknew.DataSource = null;
            }
        }

        private void rdoOffline_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoOffline.Checked == true)
            {
                punch_type = 0;
            }
            else
            { punch_type = 1; }
        }

        private void cmbbanknew_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterOutsideParty _tmpMasterParty = new MasterOutsideParty();
            _tmpMasterParty = CHNLSVC.Sales.GetOutSidePartyDetailsById(cmbbanknew.SelectedValue.ToString());
            _bankCD = _tmpMasterParty.Mbi_cd;
            DataTable odt = CHNLSVC.Financial.LOAD_MID_DET(_tmpMasterParty.Mbi_id, null);
            if (odt.Rows.Count > 0)
            {
                lblAccName.Text = odt.Rows[0]["mstm_sun_acc"].ToString();
                check_month_status();
            }

        }

        private void chkallmid_CheckedChanged(object sender, EventArgs e)
        {
            if (chkallmid.Checked == true)
            {
                rdoOffline.Enabled = false;
                rdoOnline.Enabled = false;
                txtAccNo.Text = "";
                btn_srch_accno.Enabled = false;
                btn_srch_accno.Enabled = false;
            }
            else
            {
                btn_srch_accno.Enabled = true;
                rdoOffline.Enabled = true;
                btn_srch_accno.Enabled = true;
                rdoOnline.Enabled = true;
            }
        }

        private void CreditCardRealization_new_Load(object sender, EventArgs e)
        {

        }

        private void btn_serch_mid_Click(object sender, EventArgs e)
        {
            //ClearScreen();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MID_SERCH);
            DataTable _result = CHNLSVC.CommonSearch.Search_mid_account(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtadjmid;
            _CommonSearch.ShowDialog();
            txtadjmid.Select();
        }

        private void btn_remove_finalize_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16111))
            {
                MessageBox.Show("Sorry, You have no permission to process this !\n( Advice: Required permission code :16111 )");
                return;
            }

            //  DataTable _dt = CHNLSVC.Financial.getBankRlsHeader(BaseCls.GlbUserComCode, dtDate.Value.Date, txtAccNo.Text);
            DataTable _dt = CHNLSVC.Financial.get_cred_realization_Hdr(BaseCls.GlbUserComCode, lblAccName.Text.Trim(), _bankCD, dtDate.Value.Date, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, lblAccName.Text.ToString());
            Int32 eff = 0;
            if (_dt.Rows.Count > 0)
            {
                if (_dt.Rows[0]["cred_sth_stus"].ToString() == "F")
                {
                    eff = CHNLSVC.Financial.Update_cred_recon_hdr_status(BaseCls.GlbUserComCode, Convert.ToDateTime(dtDate.Value.Date),
                        lblAccName.Text, _dt.Rows[0]["cred_sth_stus"].ToString());

                }
                else
                {
                    MessageBox.Show("You can Finalize this account. this account already pending !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (eff == 1)
            {
                MessageBox.Show("Successfully updated !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("No account found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_updat_remark_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16110))
            {
                MessageBox.Show("Sorry, You have no permission to process this!\n( Advice: Required permission code :16110 )");
                return;
            }
            foreach (DataGridViewRow row in this.grvAdj.Rows)
            {
                Int32 eff = CHNLSVC.Financial.Update_cred_recon_remark(BaseCls.GlbUserComCode, row.Cells["cred_sta_pc"].Value.ToString(), Convert.ToDateTime(row.Cells["cred_sta_dt"].Value.ToString())
                    , lblAccName.Text.ToString(), row.Cells["cred_sta_adj_tp"].Value.ToString(), row.Cells["cred_sta_refno"].Value.ToString(), row.Cells["cred_sta_rem"].Value.ToString(), Convert.ToInt32(row.Cells["cred_sta_seq"].Value.ToString()));
            }
            MessageBox.Show("Successfully updated !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnAdj_Click(null, null);

        }

        private void cmbbanknew_Leave(object sender, EventArgs e)
        {
            MasterOutsideParty _tmpMasterParty = new MasterOutsideParty();
            _tmpMasterParty = CHNLSVC.Sales.GetOutSidePartyDetailsById(cmbbanknew.SelectedValue.ToString());
            _bankCD = _tmpMasterParty.Mbi_cd;
            DataTable odt = CHNLSVC.Financial.LOAD_MID_DET(_tmpMasterParty.Mbi_id, null);
            if (odt.Rows.Count > 0)
            {
                lblAccName.Text = odt.Rows[0]["mstm_sun_acc"].ToString();
                check_month_status();
            }
        }

        private void dtDate_Leave(object sender, EventArgs e)
        {
            lblDtRls.Text = dtDate.Value.Date.ToString("dd/MMM/yyyy");
        }



    }
}
