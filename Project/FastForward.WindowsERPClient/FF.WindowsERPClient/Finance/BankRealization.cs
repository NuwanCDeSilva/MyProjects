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
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Configuration;

namespace FF.WindowsERPClient.Finance
{
    public partial class BankRealization : FF.WindowsERPClient.Base
    {
        private List<BankRealDet> _bnkRealList = null;

        public BankRealization()
        {

            InitializeComponent();
            lblDtRls.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtPCAdj.Text = BaseCls.GlbUserDefProf;
            txtComp.Text = BaseCls.GlbUserComCode;
            bind_Combo_adjTypes();
            BindBanks(DropDownListNBank);
            pnlbnkdetails.Visible = false;
        }

        private void bind_Combo_adjTypes()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("CHEQUE", "CHEQUE");
            //PartyTypes.Add("CS_CHEQUE", "CS SETTLEMENT-CHEQUES");
            PartyTypes.Add("DEPOSIT", "BANK DEPOSIT SLIP");
            PartyTypes.Add("BANK_CHG", "BANK CHARGE");
            PartyTypes.Add("RTNBNK_CHG", "RETURN CHEQUE BANK CHARGE");
            PartyTypes.Add("RTN_CHQ", "RETURN CHEQUE");
            PartyTypes.Add("CC", "CREDIT CARD");
            PartyTypes.Add("SAL_TFR", "SALARY TRANSFER");
            PartyTypes.Add("FUND_TFR", "FUND TRANSFER");
            PartyTypes.Add("OTH", "OTHER");

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

            DataTable _dt = CHNLSVC.Financial.getBankRlsHeader(BaseCls.GlbUserComCode, dtDate.Value.Date, txtAccNo.Text);
            if (_dt.Rows.Count > 0)
            {
                _val = Convert.ToDecimal(_dt.Rows[0]["BSTH_OPBAL"]);
                txtOpenBal.Text = FormatToCurrency(_val.ToString());
                _val = Convert.ToDecimal(_dt.Rows[0]["BSTH_REALIZES"]);
                txtTotReal.Text = FormatToCurrency(_val.ToString());
                _val = Convert.ToDecimal(_dt.Rows[0]["BSTH_PRV_REALIZE"]);
                txtTotPrev.Text = FormatToCurrency(_val.ToString());
                _val = Convert.ToDecimal(_dt.Rows[0]["BSTH_CC"]);
                txtTotCC.Text = FormatToCurrency(_val.ToString());
                _val = Convert.ToDecimal(_dt.Rows[0]["BSTH_ADJ"]);
                txtTotAdj.Text = FormatToCurrency(_val.ToString());
                _val = Convert.ToDecimal(_dt.Rows[0]["BSTH_CLBAL"]);
                txtCloseBal.Text = FormatToCurrency(_val.ToString());
                _val = Convert.ToDecimal(_dt.Rows[0]["bsth_state_bal"]);
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

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearScreen();
            btnAddAdj.Enabled = false;
            btnAddExtra.Enabled = false;
            grvDet.Enabled = false;
            grvDet.DataSource = null;
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
                case CommonUIDefiniton.SearchUserControlType.BankBranch:
                    {
                        paramsText.Append(DropDownListNBank.SelectedValue.ToString() + seperator);//DropDownListNBank
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
            if (string.IsNullOrEmpty(txtAccNo.Text))
            {
                MessageBox.Show("Please select the account number", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btn_srch_accno.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtFromAmt.Text) || string.IsNullOrEmpty(txtToAmt.Text))
            {
                MessageBox.Show("Please enter the value range", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtFromAmt.Focus();
                return;
            }
            if (chkAll.Checked == false && string.IsNullOrEmpty(txtPC.Text))
            {
                MessageBox.Show("Please select the profit center", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPC.Focus();
                return;
            }
            if (chkAllRemTp.Checked == false && cmbRemTp.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the document type", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

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


        gvEnable:

            string _docType = null;
            if (cmbRemTp.SelectedIndex == 0) _docType = "DEPOSIT";
            if (cmbRemTp.SelectedIndex == 1) _docType = "CHEQUE";
            //if (cmbRemTp.SelectedIndex == 2) _docType = "CS_CHEQUE";

            Int32 _rlsStatus = 2;
            if (optR.Checked == true) _rlsStatus = 0;
            if (optNR.Checked == true) _rlsStatus = 1;

            DataTable dt = CHNLSVC.Financial.get_bank_realization_det(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(dtDate.Value).Date, txtAccNo.Text, _docType, Convert.ToDecimal(txtFromAmt.Text), Convert.ToDecimal(txtToAmt.Text), _rlsStatus, Convert.ToInt32(chkNIS.Checked), Convert.ToInt32(chkOthBank.Checked), 0, txtRef.Text);
            grvDet.AutoGenerateColumns = false;
            grvDet.DataSource = dt;

            set_grid_color();

            Load_header_data();

            check_is_realize();
            btnAddAdj.Enabled = true;
            btnAddExtra.Enabled = true;
            MessageBox.Show("Done", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void getBank()
        {
            lblAccBankCd.Text = "";
            MasterBankAccount _tmpBankAcc = new MasterBankAccount();
            _tmpBankAcc = CHNLSVC.Sales.GetBankDetails(BaseCls.GlbUserComCode, null, txtAccNo.Text);

            if (_tmpBankAcc != null)
            {
                lblAccName.Text = _tmpBankAcc.Msba_acc_desc;
                lblAccBank.Text = _tmpBankAcc.Msba_cd;

                MasterOutsideParty _tmpMasterParty = new MasterOutsideParty();
                _tmpMasterParty = CHNLSVC.Sales.GetOutSidePartyDetailsById(lblAccBank.Text);
                lblAccBankCd.Text = _tmpMasterParty.Mbi_cd;
            }
            else
            {
                lblAccName.Text = "";
                lblAccBank.Text = "";
            }

        }


        private void btn_srch_accno_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAccNo;
                _CommonSearch.ShowDialog();
                txtAccNo.Select();

                ClearScreen();
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
            lblDtRls.Text = dtDate.Value.Date.ToString("dd/MMM/yyyy");
            btnFinalize.Enabled = false;
            btnAddAdj.Enabled = false;
            btnAddExtra.Enabled = false;
            grvDet.Enabled = false;
            btnSearch.Enabled = false;
            //  btnSearch.Enabled = false;

            check_month_status();

            ClearScreen();
            grvDet.DataSource = null;
        }

        private void check_month_status()
        {
            DataTable _dt = CHNLSVC.Financial.getBankRlsHeader(BaseCls.GlbUserComCode, dtDate.Value.Date, txtAccNo.Text);
            if (_dt.Rows.Count > 0)
            {
                if (_dt.Rows[0]["bsth_stus"].ToString() == "F")
                {
                    lblStus.Text = "FINALIZED";
                    //btnProcess.Enabled = false;
                    btnSearch.Enabled = true;
                    btn_remove_finalize.Enabled = true;
                    btnUploadFile_spv.Enabled = false;
                }
                else
                {
                    lblStus.Text = "PENDING";
                    //btnProcess.Enabled = true;
                    btnSearch.Enabled = false;
                    btn_remove_finalize.Enabled = false;
                    btnUploadFile_spv.Enabled = true;
                }
            }
            else
            {
                lblStus.Text = "PENDING";
                btnProcess.Enabled = true;
                btn_remove_finalize.Enabled = false;
                btnUploadFile_spv.Enabled = true;
            }
        }
        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAccNo.Text))
            {
                MessageBox.Show("Please select the account number", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            DataTable _dtRL = CHNLSVC.Financial.get_bank_realization_Hdr(BaseCls.GlbUserComCode, dtDate.Value.Date.AddDays(-1), txtAccNo.Text);
            if (_dtRL.Rows.Count != 0)
            {
                if (_dtRL.Rows[0]["bsth_stus"].ToString() == "F")
                {
                    Decimal _val = Convert.ToDecimal(_dtRL.Rows[0]["bsth_clbal"]);
                    txtOpenBal.Text = FormatToCurrency(_val.ToString());
                    _OpBalavailable = true;
                }
            }
            //if (Convert.ToDateTime(dtDate.Text) > Convert.ToDateTime("31/Oct/2014"))
            //{
            //    if (Convert.ToDateTime(dtDate.Text) != Convert.ToDateTime("01/Nov/2014"))
            //    {
            //        if (_OpBalavailable == false)
            //        { MessageBox.Show("Previous day is not processed/Finalized", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            //    }
            //}
            if (MessageBox.Show("Are you sure ?", "Bank Realization", MessageBoxButtons.YesNo) == DialogResult.No) return;

            btnProcess.Enabled = false;
            DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dtDate.Value.Month + "/" + dtDate.Value.Year).Date;

            Decimal _wkNo = 0;
            int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(dtDate.Text).Date, out _wkNo, BaseCls.GlbUserComCode);

            //save---------
            Int32 _eff = CHNLSVC.Financial.Process_bank_realization(BaseCls.GlbUserComCode, txtPC.Text, dtDate.Value.Date, txtAccNo.Text, Convert.ToInt32(chkAll.Checked), BaseCls.GlbUserID, _tmpMonth, Convert.ToInt32(_wkNo));

            //load to grid--------------
            DataTable dt = CHNLSVC.Financial.get_bank_realization_det(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), dtDate.Value.Date, txtAccNo.Text, null, 0, 0, 2, 0, 0, Convert.ToInt32(chkWithNIS.Checked), txtRef.Text);
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

            if (dt.Rows.Count > 0)
                MessageBox.Show("Completed", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No data found !", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void check_is_realize()
        {
            foreach (DataGridViewRow row in grvDet.Rows)
            {
                if (Convert.ToString(row.Cells["BSTD_IS_REALIZED"].Value) == "YES")
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[7];
                    chk.Value = true;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _bnkRealList = new List<BankRealDet>();
            if (lblStus.Text == "FINALIZED")
            {
                MessageBox.Show("Already finalized this date", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (grvDet.Rows.Count == 0)
            {
                MessageBox.Show("No data", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable _dt = CHNLSVC.Financial.getBankRlsHeader(BaseCls.GlbUserComCode, dtDate.Value.Date.AddDays(-1), txtAccNo.Text);
            if (_dt.Rows.Count > 0)
            {
                if (_dt.Rows[0]["bsth_stus"].ToString() == "P")
                {
                    if (Convert.ToDateTime(dtDate.Text) > Convert.ToDateTime("31/Oct/2014"))
                    {
                        if (Convert.ToDateTime(dtDate.Text) != Convert.ToDateTime("01/Nov/2014"))
                        {
                            MessageBox.Show("Previous day not finalized !", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                        MessageBox.Show("Previous day not saved !", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }

            if (chkUpd.Checked == true)
            {
                if (MessageBox.Show("Are you sure you want to update remarks only?", "Bank Realization", MessageBoxButtons.YesNo) == DialogResult.No) return;

                foreach (DataGridViewRow row in grvDet.Rows)
                {
                    BankRealDet _bnkReal = new BankRealDet();

                    _bnkReal.Bstd_com = BaseCls.GlbUserComCode;
                    _bnkReal.Bstd_pc = row.Cells["Bstd_pc"].Value.ToString();
                    _bnkReal.Bstd_dt = Convert.ToDateTime(row.Cells["Bstd_dt"].Value);
                    _bnkReal.Bstd_accno = txtAccNo.Text;
                    _bnkReal.Bstd_doc_tp = row.Cells["Bstd_doc_tp"].Value.ToString();
                    _bnkReal.Bstd_doc_ref = row.Cells["Bstd_doc_ref"].Value.ToString();

                    if (row.Cells["Bstd_rmk"].Value != null)
                        _bnkReal.Bstd_rmk = row.Cells["Bstd_rmk"].Value.ToString();
                    else
                        _bnkReal.Bstd_rmk = "";

                    _bnkReal.Bstd_seq_no = Convert.ToInt32(row.Cells["BSTD_SEQ_NO"].Value);

                    _bnkRealList.Add(_bnkReal);
                }

                string _messag = "";
                Int32 _eff1 = CHNLSVC.Financial.UpdateBankRealizationDet(_bnkRealList, BaseCls.GlbUserComCode, dtDate.Value.Date, txtAccNo.Text, Convert.ToDecimal(txtOpenBal.Text), Convert.ToDecimal(txtTotReal.Text), Convert.ToDecimal(txtTotPrev.Text), Convert.ToDecimal(txtTotCC.Text), Convert.ToDecimal(txtTotAdj.Text), Convert.ToDecimal(txtCloseBal.Text), Convert.ToDecimal(txtStateBal.Text), "P", BaseCls.GlbUserID, out _messag);
                if (_eff1 > 0)
                {
                    MessageBox.Show("Successfully updated !", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    chkUpd.Checked = false;
                }
                else
                    MessageBox.Show(_messag, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                return;
            }

            if (MessageBox.Show("Are you sure ?", "Bank Realization", MessageBoxButtons.YesNo) == DialogResult.No) return;

            foreach (DataGridViewRow row in grvDet.Rows)
            {
                BankRealDet _bnkReal = new BankRealDet();

                _bnkReal.Bstd_com = BaseCls.GlbUserComCode;
                _bnkReal.Bstd_pc = row.Cells["Bstd_pc"].Value.ToString();
                _bnkReal.Bstd_dt = Convert.ToDateTime(row.Cells["Bstd_dt"].Value);
                _bnkReal.Bstd_accno = txtAccNo.Text;
                _bnkReal.Bstd_doc_tp = row.Cells["Bstd_doc_tp"].Value.ToString();
                _bnkReal.Bstd_doc_desc = row.Cells["Bstd_doc_desc"].Value.ToString();
                _bnkReal.Bstd_doc_ref = row.Cells["Bstd_doc_ref"].Value.ToString();
                _bnkReal.Bstd_sys_val = Convert.ToDecimal(row.Cells["Bstd_sys_val"].Value);

                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[7];
                _bnkReal.Bstd_is_realized = Convert.ToBoolean(chk.Value) == true ? 1 : 0;

                if (!string.IsNullOrEmpty(row.Cells["Bstd_realized_dt"].Value.ToString()))
                    _bnkReal.Bstd_realized_dt = Convert.ToDateTime(row.Cells["Bstd_realized_dt"].Value);
                else
                    _bnkReal.Bstd_realized_dt = Convert.ToDateTime("31/Dec/2999");

                if (Convert.ToDecimal(row.Cells["Bstd_doc_val_cr"].Value) > 0)
                    _bnkReal.Bstd_doc_val = Convert.ToDecimal(row.Cells["Bstd_doc_val_cr"].Value) * -1;
                else
                    _bnkReal.Bstd_doc_val = Convert.ToDecimal(row.Cells["Bstd_doc_val_dr"].Value);

                if (row.Cells["Bstd_doc_bank"].Value != null)
                    _bnkReal.Bstd_doc_bank = row.Cells["Bstd_doc_bank"].Value.ToString();
                else
                    _bnkReal.Bstd_doc_bank = "";

                if (row.Cells["Bstd_rmk"].Value != null)
                    _bnkReal.Bstd_rmk = row.Cells["Bstd_rmk"].Value.ToString();
                else
                    _bnkReal.Bstd_rmk = "";

                _bnkReal.Bstd_deposit_bank = "";
                _bnkReal.Bstd_doc_bank_cd = "";
                _bnkReal.Bstd_doc_bank_branch = "";
                _bnkReal.Bstd_is_no_sun = 0;
                _bnkReal.Bstd_is_no_state = row.Cells["bstd_is_no_state"].Value == "YES" ? 1 : 0;
                _bnkReal.Bstd_is_scan = 1;
                _bnkReal.Bstd_cre_by = BaseCls.GlbUserID;
                _bnkReal.Bstd_is_new = 0;
                _bnkReal.bstd_is_extra = 0;
                _bnkReal.Bstd_seq_no = Convert.ToInt32(row.Cells["BSTD_SEQ_NO"].Value);

                _bnkRealList.Add(_bnkReal);
            }

            string _message = "";
            Int32 _eff = CHNLSVC.Financial.UpdateBankRealizationDet(_bnkRealList, BaseCls.GlbUserComCode, dtDate.Value.Date, txtAccNo.Text, Convert.ToDecimal(txtOpenBal.Text), Convert.ToDecimal(txtTotReal.Text), Convert.ToDecimal(txtTotPrev.Text), Convert.ToDecimal(txtTotCC.Text), Convert.ToDecimal(txtTotAdj.Text), Convert.ToDecimal(txtCloseBal.Text), Convert.ToDecimal(txtStateBal.Text), "P", BaseCls.GlbUserID, out _message);
            if (_eff > 0)
            {
                MessageBox.Show("Successfully updated !", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (e.RowIndex != -1 && e.ColumnIndex == 8)
            {
                if (grvDet.Rows[e.RowIndex].DefaultCellStyle.BackColor != Color.LightSkyBlue)
                {
                    if (grvDet.Rows[Convert.ToInt32(e.RowIndex)].Cells["bstd_is_no_state"].Value.ToString() == "YES")
                    {
                        MessageBox.Show("Please NTS first !", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    DataGridViewCheckBoxCell chk = grvDet.Rows[e.RowIndex].Cells[7] as DataGridViewCheckBoxCell;
                    if (grvDet.Rows[Convert.ToInt32(e.RowIndex)].Cells["BSTD_IS_REALIZED"].Value.ToString() == "YES")
                    {
                        grvDet.Rows[e.RowIndex].Cells["BSTD_IS_REALIZED"].Value = "NO";
                        chk.Value = false;
                        grvDet.Rows[e.RowIndex].Cells["Bstd_realized_dt"].Value = string.Empty;
                        grvDet.Rows[e.RowIndex].Cells["bstd_is_no_state"].Value = "NO";

                        if (dtDate.Value.Date != Convert.ToDateTime(grvDet.Rows[e.RowIndex].Cells["bstd_dt"].Value))
                            _totRealPrv = _totRealPrv + Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value) - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value);
                        else
                            _totReal = _totReal + Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value) - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value);
                    }
                    else
                    {
                        grvDet.Rows[e.RowIndex].Cells["BSTD_IS_REALIZED"].Value = "YES";
                        chk.Value = true;
                        grvDet.Rows[e.RowIndex].Cells["Bstd_realized_dt"].Value = dtDate.Value.Date;

                        if (dtDate.Value.Date != Convert.ToDateTime(grvDet.Rows[e.RowIndex].Cells["bstd_dt"].Value))
                            _totRealPrv = _totRealPrv - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value) + Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value);
                        else
                            _totReal = _totReal - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value) + Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value);
                    }
                }
                //else
                //{
                //    if (Convert.ToString(grvDet.Rows[e.RowIndex].Cells["BSTD_IS_REALIZED"].Value) == "YES")  //1/11/2014
                //    {
                //        txtTotAdj.Text =FormatToCurrency( Convert.ToDecimal(txtTotAdj.Text) + Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTA_AMT"].Value).ToString());
                //    }
                //}
            }

            //not to statement
            if (e.RowIndex != -1 && e.ColumnIndex == 16)
            {
                if (grvDet.Rows[Convert.ToInt32(e.RowIndex)].Cells["BSTD_IS_REALIZED"].Value.ToString() != "YES")
                {
                    //MessageBox.Show("Please realize first !", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //5/6/2015
                    if (MessageBox.Show("Are you sure ?", "Bank Realization", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                        return;
                }
                if (grvDet.Rows[Convert.ToInt32(e.RowIndex)].Cells["bstd_is_no_state"].Value.ToString() == "YES")
                {
                    grvDet.Rows[e.RowIndex].Cells["bstd_is_no_state"].Value = "NO";
                    if (grvDet.Rows[Convert.ToInt32(e.RowIndex)].Cells["BSTD_IS_REALIZED"].Value.ToString() == "YES")
                    {
                        if (dtDate.Value.Date != Convert.ToDateTime(grvDet.Rows[e.RowIndex].Cells["bstd_dt"].Value))
                            _totRealPrv = _totRealPrv - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value) + Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value);
                        else
                            _totReal = _totReal - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value) + Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value);
                    }

                }
                else
                {
                    grvDet.Rows[e.RowIndex].Cells["bstd_is_no_state"].Value = "YES";
                    if (grvDet.Rows[Convert.ToInt32(e.RowIndex)].Cells["BSTD_IS_REALIZED"].Value.ToString() == "YES")
                    {
                        if (dtDate.Value.Date != Convert.ToDateTime(grvDet.Rows[e.RowIndex].Cells["bstd_dt"].Value))
                            _totRealPrv = _totRealPrv + Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value) - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value);
                        else
                            _totReal = _totReal + Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_CR"].Value) - Convert.ToDecimal(grvDet.Rows[e.RowIndex].Cells["BSTD_DOC_VAL_DR"].Value);
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
                if (grvDet.Rows[Convert.ToInt32(row.Index)].Cells["BSTD_IS_REALIZED"].Value.ToString() == "YES")
                {
                    if (grvDet.Rows[row.Index].Cells["BSTD_DOC_TP"].Value.ToString() != "SAL_TFR" && grvDet.Rows[row.Index].Cells["BSTD_DOC_TP"].Value.ToString() != "CC" && grvDet.Rows[row.Index].Cells["BSTD_DOC_TP"].Value.ToString() != "BANK_CHG" && grvDet.Rows[row.Index].Cells["BSTD_DOC_TP"].Value.ToString() != "RTN_CHQ" && grvDet.Rows[row.Index].Cells["BSTD_DOC_TP"].Value.ToString() != "OTH")
                    {
                        if (Convert.ToDateTime(grvDet.Rows[row.Index].Cells["BSTD_DT"].Value) == (dtDate.Value.Date))
                        {
                            if (grvDet.Rows[row.Index].Cells["bstd_is_no_state"].Value.ToString() == "NO")
                                _totReal = _totReal - Convert.ToDecimal(grvDet.Rows[row.Index].Cells["BSTD_DOC_VAL_CR"].Value) + Convert.ToDecimal(grvDet.Rows[row.Index].Cells["BSTD_DOC_VAL_DR"].Value);
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
                BankRealDet _bnkReal = new BankRealDet();

                _bnkReal.Bstd_com = BaseCls.GlbUserComCode;
                _bnkReal.Bstd_pc = row.Cells["Bstd_pc"].Value.ToString();
                _bnkReal.Bstd_dt = Convert.ToDateTime(row.Cells["Bstd_dt"].Value);
                _bnkReal.Bstd_accno = txtAccNo.Text;
                _bnkReal.Bstd_doc_tp = row.Cells["Bstd_doc_tp"].Value.ToString();
                _bnkReal.Bstd_doc_desc = row.Cells["Bstd_doc_desc"].Value.ToString();
                _bnkReal.Bstd_doc_ref = row.Cells["Bstd_doc_ref"].Value.ToString();
                _bnkReal.Bstd_sys_val = Convert.ToDecimal(row.Cells["Bstd_sys_val"].Value);

                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[7];
                _bnkReal.Bstd_is_realized = Convert.ToBoolean(chk.Value) == true ? 1 : 0;

                if (!string.IsNullOrEmpty(row.Cells["Bstd_realized_dt"].Value.ToString()))
                    _bnkReal.Bstd_realized_dt = Convert.ToDateTime(row.Cells["Bstd_realized_dt"].Value);
                else
                    _bnkReal.Bstd_realized_dt = Convert.ToDateTime("31/Dec/2999");

                if (Convert.ToDecimal(row.Cells["Bstd_doc_val_cr"].Value) > 0)
                    _bnkReal.Bstd_doc_val = Convert.ToDecimal(row.Cells["Bstd_doc_val_cr"].Value) * -1;
                else
                    _bnkReal.Bstd_doc_val = Convert.ToDecimal(row.Cells["Bstd_doc_val_dr"].Value);

                if (row.Cells["Bstd_doc_bank"].Value != null)
                    _bnkReal.Bstd_doc_bank = row.Cells["Bstd_doc_bank"].Value.ToString();
                else
                    _bnkReal.Bstd_doc_bank = "";

                if (row.Cells["Bstd_rmk"].Value != null)
                    _bnkReal.Bstd_rmk = row.Cells["Bstd_rmk"].Value.ToString();
                else
                    _bnkReal.Bstd_rmk = "";

                _bnkReal.Bstd_deposit_bank = "";
                _bnkReal.Bstd_doc_bank_cd = "";
                _bnkReal.Bstd_doc_bank_branch = "";
                _bnkReal.Bstd_is_no_sun = 0;
                _bnkReal.Bstd_is_no_state = row.Cells["bstd_is_no_state"].Value == "YES" ? 1 : 0;
                _bnkReal.Bstd_is_scan = 1;
                _bnkReal.Bstd_cre_by = BaseCls.GlbUserID;
                _bnkReal.Bstd_is_new = 0;
                _bnkReal.bstd_is_extra = 0;
                _bnkReal.Bstd_seq_no = Convert.ToInt32(row.Cells["BSTD_SEQ_NO"].Value);

                _bnkRealList.Add(_bnkReal);
            }
            Int32 _eff = CHNLSVC.Financial.FinalizeBankRealization(BaseCls.GlbUserComCode, dtDate.Value.Date, txtAccNo.Text, BaseCls.GlbUserID, _bnkRealList);
            MessageBox.Show("Successfully finalized !", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (string.IsNullOrEmpty(txtAccNo.Text))
            {
                MessageBox.Show("Please select the account number", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
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

            DataTable _DT = CHNLSVC.Financial.GetBankAdj(BaseCls.GlbUserComCode, dtDate.Value.Date, txtAccNo.Text);
            grvAdj.AutoGenerateColumns = false;
            grvAdj.DataSource = _DT;

            foreach (DataGridViewRow row in grvAdj.Rows)
            {
                if (row.Cells["bsta_adj_tp"].Value.ToString() == "SAL_TFR" || row.Cells["bsta_adj_tp"].Value.ToString() == "CC" || row.Cells["bsta_adj_tp"].Value.ToString() == "BANK_CHG" || row.Cells["bsta_adj_tp"].Value.ToString() == "RTN_CHQ" || row.Cells["bsta_adj_tp"].Value.ToString() == "OTH")
                {
                    _totAdj = _totAdj + Convert.ToDecimal(row.Cells["bsta_amt"].Value);
                }
            }
            txtTotAdj.Text = FormatToCurrency(_totAdj.ToString());

            Decimal _totCC = 0;
            int X = CHNLSVC.Financial.GetCCRecTot(BaseCls.GlbUserComCode, dtDate.Value.Date, txtAccNo.Text, out _totCC);
            txtTotCC.Text = FormatToCurrency(_totCC.ToString());


        }

        private void set_grid_color()
        {
            foreach (DataGridViewRow row in grvDet.Rows)
            {
                if (Convert.ToString(row.Cells["bstd_is_extra"].Value) == "1")
                {
                    row.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    row.ReadOnly = true;
                }
                if (Convert.ToDateTime(row.Cells["bstd_dt"].Value) != dtDate.Value.Date)
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
                    MessageBox.Show("Please select the company", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            if (cmbAdjType.SelectedValue == "RTN_CHQ")
            {
                if (DropDownListNBank.SelectedValue == null)
                {
                    MessageBox.Show("Please select the Bank #", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }
                if (string.IsNullOrEmpty(TextBoxBranch.Text))
                {
                    MessageBox.Show("Please enter the Branch #", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    TextBoxBranch.Focus();
                    return;
                }

            }

            if (string.IsNullOrEmpty(txtAdjRef.Text))
            {
                MessageBox.Show("Please enter the reference #", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAdjRef.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPCAdj.Text))
            {
                MessageBox.Show("Please select the profit center", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPCAdj.Focus();
                return;
            }
            if (cmbAdjType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the adjustment type", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtAdjAmt.Text) || Convert.ToDecimal(txtAdjAmt.Text) < 0)
            {
                MessageBox.Show("Please enter the amount", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(txtComp.Text, txtPCAdj.Text);
            if (_IsValid == false)
            {
                MessageBox.Show("Invalid Profit Center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPCAdj.Focus();
                return;
            }

            if (CHNLSVC.Financial.checkDocBankState(txtPCAdj.Text, dtDate.Value.Date, cmbAdjType.SelectedValue.ToString(), txtAdjRef.Text) == true)
            {
                MessageBox.Show("Cannot add. This reference # already exists.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAdjRef.Focus();
                return;
            }

            if (MessageBox.Show("Are you sure ?", "Bank Realization", MessageBoxButtons.YesNo) == DialogResult.No) return;

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
            DOC.Grdd_doc_bank_cd = lblAccBankCd.Text;
            DOC.Grdd_deposit_bank = txtAccNo.Text;
            DOC.Grdd_doc_desc = cmbAdjType.Text;
            DOC.Grdd_doc_tp = cmbAdjType.SelectedValue.ToString();
            DOC.Grdd_pc = txtPCAdj.Text;
            DOC.Grdd_doc_bank_branch = TextBoxBranch.Text.ToString();
            DOC.BSTD_SUN_ACC = txtsunAccount.Text.ToString().Trim();

            Int32 _eff = CHNLSVC.Financial.SaveBankAdj(DOC, txtComp.Text, txtPCAdj.Text, dtDate.Value.Date, txtAccNo.Text, cmbAdjType.SelectedValue.ToString(), cmbAdjType.Text.ToString(), _adjAmt, txtAdjRef.Text, txtAdjRem.Text, BaseCls.GlbUserID, _tmpMonth, Convert.ToInt32(_wkNo), "", lblAccBank.Text, "");
            #region Returen Checq add by tharanga 2018/05/19
            if (cmbAdjType.SelectedValue == "RTN_CHQ")
            {
                int seqNo = CHNLSVC.Inventory.GetSerialID();
                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                //get reciept number
                _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;//BaseCls.GlbUserDefProf;
                _receiptAuto.Aut_cate_tp = BaseCls.GlbUserDefProf;
                _receiptAuto.Aut_start_char = "RCHQ";
                _receiptAuto.Aut_direction = 0;
                _receiptAuto.Aut_modify_dt = null;
                _receiptAuto.Aut_moduleid = "RTCHQ";
                _receiptAuto.Aut_number = 0;
                _receiptAuto.Aut_year = 2012;
                string _cusNo = CHNLSVC.Sales.GetRecieptNo(_receiptAuto);
                //insert reciept
                RecieptHeader recieptHeadder = new RecieptHeader();
                recieptHeadder.Sar_seq_no = seqNo;
                recieptHeadder.Sar_receipt_no = _cusNo;
                recieptHeadder.Sar_com_cd = BaseCls.GlbUserComCode;
                recieptHeadder.Sar_receipt_type = "RTCHQ";
                recieptHeadder.Sar_receipt_date = CHNLSVC.Security.GetServerDateTime().Date;
                recieptHeadder.Sar_profit_center_cd = txtPCAdj.Text.ToUpper();
                recieptHeadder.Sar_debtor_name = "CHEQUE";
                recieptHeadder.Sar_tot_settle_amt = Convert.ToDecimal(txtAdjAmt.Text);
                recieptHeadder.Sar_direct = false;
                recieptHeadder.Sar_act = true;
                recieptHeadder.Sar_create_by = BaseCls.GlbUserID;
                recieptHeadder.Sar_session_id = BaseCls.GlbUserSessionID;
                recieptHeadder.Sar_create_when = CHNLSVC.Security.GetServerDateTime().Date;

                //  CHNLSVC.Sales.SaveReceiptHeader(recieptHeadder); **

                //insert reciept item
                RecieptItem recieptItem = new RecieptItem();
                recieptItem.Sard_seq_no = seqNo;
                recieptItem.Sard_line_no = 1;
                recieptItem.Sard_receipt_no = _cusNo;

                recieptItem.Sard_chq_bank_cd = DropDownListNBank.SelectedValue.ToString();
                recieptItem.Sard_pay_tp = "CASH";
                recieptItem.Sard_settle_amt = 0;

                //CHNLSVC.Sales.SaveReceiptItem(recieptItem); **

                //add return cheque record
                ChequeReturn chequeReturn = new ChequeReturn();
                chequeReturn.Seq = seqNo;
                chequeReturn.RefNo = _cusNo;
                //chequeReturn.Bank = DropDownListNBank.SelectedValue.ToString();
                chequeReturn.Bank = lblAccBankCd.Text;
                chequeReturn.Pc = txtPCAdj.Text;
                chequeReturn.Bank_type = 1;// listBoxBnkTp.SelectedItem.ToString()=="CASH"?1:0;
                chequeReturn.Returndate = dtDate.Value.Date;
                //chequeReturn.Return_bank = lblAccBankCd.Text.Trim();
               // chequeReturn.Return_bank = DropDownListNBank.SelectedValue.ToString();
                chequeReturn.Return_bank = txtAccNo.Text;               
                chequeReturn.Cheque_no = txtAdjRef.Text;
                chequeReturn.Company = BaseCls.GlbUserComCode;
                chequeReturn.Create_by = BaseCls.GlbUserID;
                chequeReturn.Create_Date = CHNLSVC.Security.GetServerDateTime().Date;
                chequeReturn.Act_value = Convert.ToDecimal(txtAdjAmt.Text);
                chequeReturn.Sys_value = Convert.ToDecimal(txtAdjAmt.Text);
                chequeReturn.Srcq_mgr_chg = 0;
                chequeReturn.Srcq_chq_branch = TextBoxBranch.Text.Trim();
                // chequeReturn.Settle_val = Convert.ToDecimal(TextBoxReturnAmount.Text);

                //Int32 eff= CHNLSVC.Financial.SaveReturnCheque(chequeReturn);//**
                Int32 eff = 0;
                try
                {
                    eff = CHNLSVC.Financial.ChequeReturn(recieptHeadder, recieptItem, chequeReturn);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    eff = 0;
                    return;

                }

                if (eff > 0)
                {
                   
                    MessageBox.Show("Successfully Saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   // this.btnClear_Click(null, null);

                    DropDownListNBank.SelectedIndex = -1;
                    TextBoxBranch.Text = "";
                    txtbrows.Text = "";
                   // btnAddAdj.Enabled = false;
                    btnAddExtra.Enabled = false;
                   // grvDet.Enabled = false;
                   // grvDet.DataSource = null;
                }
                else
                {
                    MessageBox.Show("Not Saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion

            // ADD BY tHARINDU
            //btnSave_Click(null, null);

            if (_eff != 0)
            {
                DataTable dt = CHNLSVC.Financial.get_bank_realization_det(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(dtDate.Value).Date, txtAccNo.Text, null, 0, 0, 2, 0, 0, Convert.ToInt32(chkWithNIS.Checked), txtRef.Text);
                grvDet.AutoGenerateColumns = false;
                grvDet.DataSource = dt;

                calc();

                set_grid_color();
                check_is_realize();

                load_adjgriddate();

                clear_adj();

                calc_close_val();

                //Int32 _ef = CHNLSVC.Financial.UpdateBankRealHdr(BaseCls.GlbUserComCode, dtDate.Value.Date, txtAccNo.Text, Convert.ToDecimal(txtOpenBal.Text), Convert.ToDecimal(txtTotReal.Text), Convert.ToDecimal(txtTotPrev.Text), Convert.ToDecimal(txtTotCC.Text), Convert.ToDecimal(txtTotAdj.Text), Convert.ToDecimal(txtCloseBal.Text), Convert.ToDecimal(txtStateBal.Text), "P", BaseCls.GlbUserID);

                MessageBox.Show("Successfully updated !", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void clear_adj()
        {
            txtAdjAmt.Text = "";
            txtAdjRef.Text = "";
            txtAdjRem.Text = "";
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
                MessageBox.Show("Please select the account number", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                MessageBox.Show("Please select the profit center", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPCExtra.Focus();
                return;
            }
            if (cmbDocType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the document type", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtExtraAmt.Text) || Convert.ToDecimal(txtExtraAmt.Text) < 0)
            {
                MessageBox.Show("Please enter the amount", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

            if (MessageBox.Show("Are you sure ?", "Bank Realization", MessageBoxButtons.YesNo) == DialogResult.No) return;


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

            _bnkRealList = new List<BankRealDet>();
            BankRealDet _bnkReal = new BankRealDet();
            _bnkReal.Bstd_com = BaseCls.GlbUserComCode;
            _bnkReal.Bstd_pc = txtPCExtra.Text;
            _bnkReal.Bstd_dt = Convert.ToDateTime(dtDate.Value);
            _bnkReal.Bstd_accno = txtAccNo.Text;
            _bnkReal.Bstd_doc_tp = cmbDocType.SelectedValue.ToString();
            _bnkReal.Bstd_doc_desc = cmbDocType.Text;
            _bnkReal.Bstd_doc_ref = txtExtraRef.Text;
            _bnkReal.Bstd_sys_val = Convert.ToDecimal(txtExtraAmt.Text);
            _bnkReal.Bstd_is_realized = 0;
            _bnkReal.Bstd_realized_dt = Convert.ToDateTime("31/Dec/9999");
            _bnkReal.Bstd_doc_val = Convert.ToDecimal(txtExtraAmt.Text);

            _bnkReal.Bstd_doc_bank = "";

            _bnkReal.Bstd_rmk = txtExtraRem.Text;
            _bnkReal.Bstd_deposit_bank = "";
            _bnkReal.Bstd_doc_bank_cd = "";
            _bnkReal.Bstd_doc_bank_branch = "";
            _bnkReal.Bstd_is_no_sun = 0;
            _bnkReal.Bstd_is_no_state = 0;
            _bnkReal.Bstd_is_scan = 1;
            _bnkReal.Bstd_cre_by = BaseCls.GlbUserID;
            _bnkReal.Bstd_is_new = 0;
            _bnkReal.bstd_is_extra = 0;

            _bnkRealList.Add(_bnkReal);

            string _msg = "";
            int eff = CHNLSVC.Financial.UpdateBankRealizationDetails(DOC, _bnkRealList, out _msg);
            if (eff != 0)
            {

                DataTable dt = CHNLSVC.Financial.get_bank_realization_det(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(dtDate.Value).Date, txtAccNo.Text, null, Convert.ToDecimal(txtFromAmt.Text), Convert.ToDecimal(txtToAmt.Text), 2, Convert.ToInt32(chkNIS.Checked), Convert.ToInt32(chkOthBank.Checked), Convert.ToInt32(chkWithNIS.Checked), txtRef.Text);
                grvDet.AutoGenerateColumns = false;
                grvDet.DataSource = dt;
                clear_extra();
                calc_close_val();
                MessageBox.Show("Successfully updated !", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (grvDet.Rows[Convert.ToInt32(e.RowIndex)].Cells["BSTD_IS_REALIZED"].Value.ToString() == "YES")
                {
                    MessageBox.Show("Cannot change the value. Already realized !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SendKeys.Send("{TAB}");
                }
            }
            if (e.RowIndex != -1 && e.ColumnIndex == 4)
            {
                if (grvDet.Rows[Convert.ToInt32(e.RowIndex)].Cells["BSTD_IS_REALIZED"].Value.ToString() == "YES")
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
            if (e.RowIndex != -1 && e.ColumnIndex == 6)
            {
                if (Convert.ToDateTime(grvDet.Rows[e.RowIndex].Cells["BSTD_REALIZED_DT"].Value) > Convert.ToDateTime(dtDate.Value))
                {
                    MessageBox.Show("Invalid realize date !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    grvDet.Rows[e.RowIndex].Cells["BSTD_REALIZED_DT"].Value = dtDate.Value.Date;
                    SendKeys.Send("{TAB}");
                }
            }
        }

        private void grvAdj_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 6)
            {
                if (grvAdj.Rows[e.RowIndex].Cells["BSTA_PC"].Value.ToString() != "AARCL" && grvAdj.Rows[e.RowIndex].Cells["BSTA_PC"].Value.ToString() != "SDRHO" && grvAdj.Rows[e.RowIndex].Cells["BSTA_PC"].Value.ToString() != "SGRHO" && grvAdj.Rows[e.RowIndex].Cells["BSTA_PC"].Value.ToString() != "RRCL" && grvAdj.Rows[e.RowIndex].Cells["BSTA_PC"].Value.ToString() != "VRCL" && grvAdj.Rows[e.RowIndex].Cells["BSTA_PC"].Value.ToString() != "PNGHO")
                {
                    MessageBox.Show("Cannot change !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SendKeys.Send("{TAB}");
                }
            }
        }

        private void btnUpdatePC_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ?", "Bank Realization", MessageBoxButtons.YesNo) == DialogResult.No) return;

            //try
            //{
            foreach (DataGridViewRow row in grvAdj.Rows)
            {
                if (grvAdj.Rows[row.Index].Cells["SR"].Value != null)
                {
                    if ((grvAdj.Rows[row.Index].Cells["BSTA_PC"].Value.ToString() == "AARCL" || grvAdj.Rows[row.Index].Cells["BSTA_PC"].Value.ToString() == "SGRHO" || grvAdj.Rows[row.Index].Cells["BSTA_PC"].Value.ToString() == "SDRHO" || grvAdj.Rows[row.Index].Cells["BSTA_PC"].Value.ToString() == "VRCL" || grvAdj.Rows[row.Index].Cells["BSTA_PC"].Value.ToString() == "RRCL" || grvAdj.Rows[row.Index].Cells["BSTA_PC"].Value.ToString() == "PNGHO") && !string.IsNullOrEmpty(grvAdj.Rows[row.Index].Cells["SR"].Value.ToString()))
                    {
                        Int32 _eff = CHNLSVC.Financial.UpdateBankAdjPC(grvAdj.Rows[row.Index].Cells["BSTA_COM"].Value.ToString(), grvAdj.Rows[row.Index].Cells["BSTA_PC"].Value.ToString(), Convert.ToDateTime(dtDate.Value.Date), txtAccNo.Text, grvAdj.Rows[row.Index].Cells["bsta_adj_tp"].Value.ToString(), grvAdj.Rows[row.Index].Cells["bsta_refno"].Value.ToString(), grvAdj.Rows[row.Index].Cells["SR"].Value.ToString(), Convert.ToDecimal(grvAdj.Rows[row.Index].Cells["bsta_amt"].Value), grvAdj.Rows[row.Index].Cells["bsta_refno"].Value.ToString());
                    }
                }

            }
            DataTable _DT = CHNLSVC.Financial.GetBankAdj(BaseCls.GlbUserComCode, dtDate.Value.Date, txtAccNo.Text);
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

        private void btn_updat_remark_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16107))
            {
                MessageBox.Show("Sorry, You have no permission to process this!\n( Advice: Required permission code :16107 )");
                return;
            }
            foreach (DataGridViewRow row in this.grvAdj.Rows)
            {
                Int32 eff = CHNLSVC.Financial.Update_bank_recon_remark(BaseCls.GlbUserComCode, row.Cells["BSTA_PC"].Value.ToString(), Convert.ToDateTime(row.Cells["dataGridViewTextBoxColumn2"].Value.ToString())
                    , txtAccNo.Text.ToString(), row.Cells["bsta_adj_tp"].Value.ToString(), row.Cells["bsta_refno"].Value.ToString(), row.Cells["bsta_rem"].Value.ToString(), Convert.ToInt32(row.Cells["bsta_seq"].Value.ToString()));
            }
            MessageBox.Show("Successfully updated !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_remove_finalize_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16108))
            {
                MessageBox.Show("Sorry, You have no permission to process this !\n( Advice: Required permission code :16108 )");
                return;
            }

            DataTable _dt = CHNLSVC.Financial.getBankRlsHeader(BaseCls.GlbUserComCode, dtDate.Value.Date, txtAccNo.Text);
            Int32 eff = 0;
            if (_dt.Rows.Count > 0)
            {
                if (_dt.Rows[0]["bsth_stus"].ToString() == "F")
                {
                    eff = CHNLSVC.Financial.Update_bank_recon_hdr_status(BaseCls.GlbUserComCode, Convert.ToDateTime(dtDate.Value.Date),
                        txtAccNo.Text, _dt.Rows[0]["bsth_stus"].ToString());

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

        private void btnloadacc_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
            DataTable _result = CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
            grdaccouts.AutoGenerateColumns = false;
            grdaccouts.DataSource = _result;
        }

        private void btnprcess_more_acc_Click(object sender, EventArgs e)
        {
            Int32 _eff = 0;
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16109))
            {
                MessageBox.Show("Sorry, You have no permission to process this !\n( Advice: Required permission code :16109 )");
                return;
            }
            if (Convert.ToDateTime(dateprcessaccounts.Value.Date) >= DateTime.Now.Date)
            {
                MessageBox.Show("Cannot process. Invalid date !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure ?", "Bank Realization", MessageBoxButtons.YesNo) == DialogResult.No) return;
            List<string> _documentNolist = new List<string>();
            btnProcess.Enabled = false;
            foreach (DataGridViewRow dgvr in grdaccouts.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells["Select"] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    string account = dgvr.Cells["Code"].Value.ToString();


                    DataTable _dtRL = CHNLSVC.Financial.get_bank_realization_Hdr(BaseCls.GlbUserComCode, dtDate.Value.Date.AddDays(-1), account);
                    if (_dtRL.Rows.Count != 0)
                    {
                        if (_dtRL.Rows[0]["bsth_stus"].ToString() == "F")
                        {
                            Decimal _val = Convert.ToDecimal(_dtRL.Rows[0]["bsth_clbal"]);
                                                       
                            txtOpenBal.Text = FormatToCurrency(_val.ToString());


                            DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dateprcessaccounts.Value.Month + "/" + dateprcessaccounts.Value.Year).Date;

                            Decimal _wkNo = 0;
                            int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(dateprcessaccounts.Text).Date, out _wkNo, BaseCls.GlbUserComCode);

                            //save---------
                            Int32 _ef = CHNLSVC.Financial.UpdateBankRealHdr(BaseCls.GlbUserComCode, dateprcessaccounts.Value.Date, account, Convert.ToDecimal(txtOpenBal.Text), Convert.ToDecimal(0), Convert.ToDecimal(0), Convert.ToDecimal(0), Convert.ToDecimal(0), Convert.ToDecimal(0), Convert.ToDecimal(0), "P", BaseCls.GlbUserID);

                            _eff = CHNLSVC.Financial.Process_bank_realization(BaseCls.GlbUserComCode, txtPC.Text, dateprcessaccounts.Value.Date, account, Convert.ToInt32(chkAll.Checked), BaseCls.GlbUserID, _tmpMonth, Convert.ToInt32(_wkNo));


                        }
                        else
                        {
                            _documentNolist.Add(account);
                        }
                    }
                    else
                    {
                        _documentNolist.Add(account);
                    }
                    //DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dateprcessaccounts.Value.Month + "/" + dateprcessaccounts.Value.Year).Date;

                    //Decimal _wkNo = 0;
                    //int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(dateprcessaccounts.Text).Date, out _wkNo, BaseCls.GlbUserComCode);

                    ////save---------
                    //Int32 _ef = CHNLSVC.Financial.UpdateBankRealHdr(BaseCls.GlbUserComCode, dateprcessaccounts.Value.Date, account, Convert.ToDecimal(0), Convert.ToDecimal(0), Convert.ToDecimal(0), Convert.ToDecimal(0), Convert.ToDecimal(0), Convert.ToDecimal(0), Convert.ToDecimal(0), "P", BaseCls.GlbUserID);

                    //_eff = CHNLSVC.Financial.Process_bank_realization(BaseCls.GlbUserComCode, txtPC.Text, dateprcessaccounts.Value.Date, account, Convert.ToInt32(chkAll.Checked), BaseCls.GlbUserID, _tmpMonth, Convert.ToInt32(_wkNo));

                }
            }
            if (_eff > 0)
            {
                if (_documentNolist.Count > 0)
                {
                    string reqst = string.Join<string>(" , ", _documentNolist);
                    MessageBox.Show("Following Accounts are not Finalized .\n " + reqst, "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }

                MessageBox.Show("Completed", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                if (_documentNolist.Count > 0)
                {
                    string reqst = string.Join<string>(" , ", _documentNolist);
                    MessageBox.Show("Following Accounts are not Finalized .\n " + reqst, "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
            }

        }

        private void btnSearchFile_spv_Click(object sender, EventArgs e)
        {
            txtbrows.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            txtbrows.Text = openFileDialog1.FileName;
        }

        private void btnUploadFile_spv_Click(object sender, EventArgs e)
        {
            #region Upload Excel
            StringBuilder _errorLst = new StringBuilder();
            string _msg = string.Empty;
            ReminderLetter _ltr = new ReminderLetter();
            if (string.IsNullOrEmpty(txtbrows.Text))
            {
                MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtbrows.Clear();
                txtbrows.Focus();
                return;
            }

            System.IO.FileInfo fileObj = new System.IO.FileInfo(txtbrows.Text);
            if (fileObj.Exists == false)
            {
                MessageBox.Show("Selected file does not exist at the following path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //btnGvBrowse.Focus();
                return;
            }

            string Extension = fileObj.Extension;

            string conStr = "";

            if (Extension.ToUpper() == ".XLS")
            {

                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                         .ConnectionString;
            }
            else if (Extension.ToUpper() == ".XLSX")
            {
                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                          .ConnectionString;

            }
            else
            {
                MessageBox.Show("Invalid upload file Format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtbrows.Text = "";
                return;
            }
            string _excelConnectionString = ConfigurationManager.ConnectionStrings[Extension == ".XLS" ? "ConStringExcel03" : "ConStringExcel07"].ConnectionString;


            _excelConnectionString = String.Format(conStr, txtbrows.Text, "YES");
            OleDbConnection connExcel = new OleDbConnection(_excelConnectionString);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable _dt = new DataTable();
            cmdExcel.Connection = connExcel;
            try
            {
                connExcel.Open();
            }
            catch (Exception)
            {

                MessageBox.Show("First Colse the Excle file. ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(_dt);
            connExcel.Close();
            if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            #endregion
            if (lblStus.Text == "PENDING")
            {


                List<ScanPhysicalDocReceiveDet> _ScanPhysicalDocReceiveDetlist = new List<ScanPhysicalDocReceiveDet>();
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow _dr in _dt.Rows)
                    {

                        DataTable _result = CHNLSVC.CustService.get_profitcenter(BaseCls.GlbUserComCode, _dr[0].ToString().Trim());
                        if (_result.Rows.Count < 1)
                        {
                            if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("and invalid Profit Center Code - " + _dr[0].ToString());
                            else _errorLst.Append(" and invalid Profit Center Code  - " + _dr[0].ToString());

                        }
                    }
                    if (_dt.Rows.Count > 0)
                    {

                        if (string.IsNullOrEmpty(_errorLst.ToString()))
                        {


                            Decimal _wkNo = 0;
                            int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(dtDate.Text).Date, out _wkNo, txtComp.Text);

                            foreach (DataRow _dr in _dt.Rows)
                            {
                                Decimal _adjAmt = 0;
                                if (optDr.Checked == true)
                                    _adjAmt = Convert.ToDecimal(_dr["AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(_dr["AMOUNT"].ToString().Trim()));
                                else
                                    _adjAmt = Convert.ToDecimal(_dr["AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(_dr["AMOUNT"].ToString().Trim())) * -1;

                                ScanPhysicalDocReceiveDet DOC = new ScanPhysicalDocReceiveDet();
                                DOC.Grdd_rcv_by = BaseCls.GlbUserID;
                                DOC.Grdd_rcv_dt = DateTime.Now.Date;
                                DOC.Grdd_doc_rcv = true;
                                DOC.Grdd_rmk = _dr["REMARK"] == DBNull.Value ? string.Empty : _dr["REMARK"].ToString().Trim();
                                DOC.Grdd_doc_val = _dr["AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(_dr["AMOUNT"].ToString().Trim());
                                DOC.Grdd_dt = dtDate.Value.Date;
                                DOC.Grdd_month = Convert.ToDateTime("01" + "/" + dtDate.Value.Month + "/" + dtDate.Value.Year).Date;
                                DOC.Grdd_week = Convert.ToInt32(_wkNo);
                                DOC.Grdd_is_extra = true;
                                DOC.Grdd_short_ref = 0;
                                DOC.Grdd_doc_bank = lblAccBank.Text;
                                DOC.Grdd_com = txtComp.Text;
                                DOC.Grdd_cre_by = BaseCls.GlbUserID;
                                DOC.Grdd_doc_bank_cd = _dr["BANK"] == DBNull.Value ? string.Empty : _dr["BANK"].ToString().Trim();
                                DOC.Grdd_deposit_bank = txtAccNo.Text;
                                DOC.Grdd_doc_desc = _dr["TYPE"] == DBNull.Value ? string.Empty : _dr["TYPE"].ToString().Trim();
                                DOC.Grdd_doc_tp = _dr["TYPE"] == DBNull.Value ? string.Empty : _dr["TYPE"].ToString().Trim();
                                DOC.Grdd_pc = _dr["PC"] == DBNull.Value ? string.Empty : _dr["PC"].ToString().Trim();
                                DOC.Grdd_doc_bank_branch = _dr["BRANCH"] == DBNull.Value ? string.Empty : _dr["BRANCH"].ToString().Trim();
                                DOC.bank_id = lblAccBank.Text;

                                DOC.bsta_com = BaseCls.GlbUserComCode;
                                DOC.bsta_pc = _dr["PC"] == DBNull.Value ? string.Empty : _dr["PC"].ToString().Trim();
                                DOC.bsta_dt = dtDate.Value.Date;
                                DOC.bsta_accno = txtAccNo.Text;
                                DOC.bsta_adj_tp = _dr["TYPE"] == DBNull.Value ? string.Empty : _dr["TYPE"].ToString().Trim();
                                DOC.bsta_amt = _adjAmt;
                                DOC.bsta_refno = _dr["REFERENCE"] == DBNull.Value ? string.Empty : _dr["REFERENCE"].ToString().Trim();
                                DOC.bsta_rem = _dr["REMARK"] == DBNull.Value ? string.Empty : _dr["REMARK"].ToString().Trim();
                                DOC.bsta_mid = _dr["MID"] == DBNull.Value ? string.Empty : _dr["MID"].ToString().Trim();
                                DOC.BSTD_SUN_ACC = _dr["SUNACC"] == DBNull.Value ? string.Empty : _dr["SUNACC"].ToString().Trim();

                                DOC.bsta_cre_by = BaseCls.GlbUserID;
                                _ScanPhysicalDocReceiveDetlist.Add(DOC);

                            }

                        }
                        Int32 _eff = CHNLSVC.Financial.uploda_excle_BankAdj(_ScanPhysicalDocReceiveDetlist);
                        if (_eff != 0)
                        {
                            DataTable dt = CHNLSVC.Financial.get_bank_realization_det(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(dtDate.Value).Date, txtAccNo.Text, null, 0, 0, 2, 0, 0, Convert.ToInt32(chkWithNIS.Checked), txtRef.Text);
                            grvDet.AutoGenerateColumns = false;
                            grvDet.DataSource = dt;

                            calc();

                            set_grid_color();
                            check_is_realize();

                            load_adjgriddate();

                            clear_adj();

                            calc_close_val();
                            MessageBox.Show("Successfully updated !", "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Following discrepancies found when checking the file.\n " + _errorLst.ToString(), "Bank Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                }

            }
            else
            {
                MessageBox.Show("Cannot process. already Finalized !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void BindBanks(ComboBox ddl1)
        {
            try
            {
                DataTable datasource = CHNLSVC.Financial.GetBanks();
                ddl1.DataSource = datasource;
                ddl1.DisplayMember = "mbi_desc";
                ddl1.ValueMember = "mbi_cd";
                ddl1.SelectedIndex = -1;


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

        private void btnBranchSerarch_Click(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListNBank.SelectedIndex == -1)
                {
                    MessageBox.Show("Select the new Bank!");
                    return;
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                DataTable _result = CHNLSVC.CommonSearch.SearchBankBranchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxBranch;
                _CommonSearch.ShowDialog();
                TextBoxBranch.Select();
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

        private void cmbAdjType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAdjType.SelectedValue == "RTN_CHQ")
            {
                DropDownListNBank.Enabled = true;
                btnBranchSerarch.Enabled = true;
            }
        }

        private void ChkselectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkselectAll.Checked == true)
            {
                for (int i = 0; i < grdaccouts.RowCount; i++)
                {
                    grdaccouts[0, i].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < grdaccouts.RowCount; i++)
                {
                    grdaccouts[0, i].Value = false;
                }
            }
        }

        private void txtAdjRef_Leave(object sender, EventArgs e)
        {
            try
            {
                if(cmbAdjType.SelectedValue == "RTN_CHQ")
                {
                    DataTable dt = new DataTable();
                    dt = CHNLSVC.Financial.GetBnkdetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtAdjRef.Text, txtAccNo.Text);

                    if (dt.Rows.Count == 1)
                    {
                        txtAdjAmt.Text = dt.Rows[0]["Amount"].ToString();
                        TextBoxBranch.Text = dt.Rows[0]["BranchCode"].ToString();
                        DropDownListNBank.Text = dt.Rows[0]["Bank"].ToString();
                    }
                    else if (dt.Rows.Count > 1)
                    {
                        pnlbnkdetails.Visible = true;
                        gdvbnkdetails.DataSource = dt;
                        gdvbnkdetails.AutoGenerateColumns = false;
                    }
                    else
                    {
                        MessageBox.Show("No Records Available", "Bankrelization", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bankrelization", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gdvbnkdetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DropDownListNBank.Text = gdvbnkdetails.Rows[e.RowIndex].Cells[0].Value.ToString();
            TextBoxBranch.Text = gdvbnkdetails.Rows[e.RowIndex].Cells[1].Value.ToString();      
            txtAdjAmt.Text = gdvbnkdetails.Rows[e.RowIndex].Cells[2].Value.ToString();
            pnlbnkdetails.Visible = false;
        }

        private void label30_Click(object sender, EventArgs e)
        {

        }
    }
}
