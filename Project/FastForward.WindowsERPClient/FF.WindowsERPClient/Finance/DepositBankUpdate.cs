using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Finance
{
    public partial class DepositBankUpdate : Base
    {

        List<Deposit_Bank_Pc_wise> _lstDeposit;

        List<Deposit_Bank_Pc_wise> _listCheque;
        Deposit_Bank_Pc_wise objDepositBank;
        List<Deposit_Bank_Pc_wise> _listCreditCard;
        Deposit_Bank_Pc_wise objCreditcard;

        List<Deposit_Bank_Pc_wise> _lstBank;

        List<Deposit_Bank_Pc_wise> _listReceiveCheque;
        Deposit_Bank_Pc_wise objReceiveChq;
        List<Deposit_Bank_Pc_wise> _listBankSlip;
        Deposit_Bank_Pc_wise objbank_slip;


        public DepositBankUpdate()
        {
            InitializeComponent();
        }

        private void DepositBankUpdate_Load(object sender, EventArgs e)
        {
            // LoadBankDetails(BaseCls.GlbUserComCode);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            #region validation

            if (txtProfitCenter.Text.Trim() == "")
            {
                MessageBox.Show("Please select Profit center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dtpFromDate.Value.Date > dtpTodate.Value.Date)
            {
                MessageBox.Show("From Date must be less than To Date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            #endregion

            DateTime _fromd = Convert.ToDateTime(dtpFromDate.Text.ToString());
            DateTime _Todate = dtpTodate.Value.Date;
            //string _accNO = cmbDipositAccNo.Text;
            //string[] arr = _accNO.Split(new string[] { "||" }, StringSplitOptions.None);
            //string _acc = arr[0].Trim();

            string _pc = txtProfitCenter.Text;

            DataTable dtb = CHNLSVC.Sales.Get_LoadDipDetais(_pc, _fromd, _Todate);
            if (dtb != null && dtb.Rows.Count > 0)
            {
                dgvDepositbanks.AutoGenerateColumns = false;
                this.dgvDepositbanks.Columns["clmdesc"].ReadOnly = true;
                this.dgvDepositbanks.Columns["clmReceiptNo"].ReadOnly = true;
                this.dgvDepositbanks.Columns["clmPrefix"].ReadOnly = true;
                this.dgvDepositbanks.Columns["clmManualRefNo"].ReadOnly = true;
                this.dgvDepositbanks.Columns["clmSettleAmount"].ReadOnly = true;
                this.dgvDepositbanks.Columns["clmSeqNo"].ReadOnly = true;
                this.dgvDepositbanks.Columns["clmLineNo"].ReadOnly = true;

                string type = "CHEQUE";
                DataRow[] foundRows;
                DataTable dtsub = null;
                string expression = "SARD_PAY_TP = '" + type + "'";
                foundRows = dtb.Select(expression);
                if (foundRows.Count() > 0)
                {
                    dtsub = foundRows.CopyToDataTable<DataRow>();
                    dgvDepositbanks.DataSource = null;
                    dgvDepositbanks.DataSource = dtsub;
                    foreach (DataGridViewRow dr in dgvDepositbanks.Rows)
                    {
                        //string settleAmnt = Convert.ToString(dr.Cells["clmSettleAmount"].Value);
                        //dr.Cells["clmSettleAmount"].Value = settleAmnt.ToString("N");
                        if (Convert.ToString(dr.Cells["clmsar_anal_3"].Value) != "")
                        {
                            dr.Cells["clmManualRefNo"].Value = Convert.ToString(dr.Cells["clmsar_anal_3"].Value);
                        }
                        else
                        {
                            dr.Cells["clmManualRefNo"].Value = Convert.ToString(dr.Cells["clmManualRefNo"].Value);
                        }

                        string _type = Convert.ToString(dr.Cells["clmsar_receipt_type"].Value);
                        if (_type == "DIR")
                        {
                            dr.Cells["clmManualRefNo"].Value = Convert.ToString(dr.Cells["clmSARD_INV_NO"].Value);
                        }
                    }


                    DataTable dt = CHNLSVC.Sales.GetBankDetais(BaseCls.GlbUserComCode);
                    ddlAccNo.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        ddlAccNo.Items.Add(dt.Rows[i][2] + " || " + dt.Rows[i][3]);

                    }
                }
                else
                {
                    dgvDepositbanks.DataSource = null;
                }

                string Ctype = "CRCD";
                DataRow[] foundRowscred;
                // dtsub.Rows.Clear();
                DataTable dtCred = null;
                string expression_crd = "SARD_PAY_TP = '" + Ctype + "'";
                foundRowscred = dtb.Select(expression_crd);
                if (foundRowscred.Count() > 0)
                {
                    dtCred = foundRowscred.CopyToDataTable<DataRow>();
                    dgvCreditCardDets.DataSource = null;
                    dgvCreditCardDets.AutoGenerateColumns = false;
                    this.dgvCreditCardDets.Columns["clmCrDesc"].ReadOnly = true;
                    this.dgvCreditCardDets.Columns["clmCrRecept"].ReadOnly = true;
                    this.dgvCreditCardDets.Columns["clmCrPrefix"].ReadOnly = true;
                    this.dgvCreditCardDets.Columns["clmCrManualRef"].ReadOnly = true;
                    this.dgvCreditCardDets.Columns["clmCrSettleAmt"].ReadOnly = true;
                    this.dgvCreditCardDets.Columns["clmCrMid"].ReadOnly = true;
                    //this.dgvCreditCardDets.Columns["clmLineNo"].ReadOnly = true;

                    dgvCreditCardDets.DataSource = dtCred;

                    foreach (DataGridViewRow drCred in dgvCreditCardDets.Rows)
                    {
                        if (Convert.ToString(drCred.Cells["clmCrsar_anal_3"].Value) != "")
                        {
                            drCred.Cells["clmCrManualRef"].Value = Convert.ToString(drCred.Cells["clmCrsar_anal_3"].Value);
                        }
                        else
                        {
                            drCred.Cells["clmCrManualRef"].Value = Convert.ToString(drCred.Cells["clmCrManualRef"].Value);
                        }

                        string _type = Convert.ToString(drCred.Cells["clmCrReceiptType"].Value);
                        if (_type == "DIR")
                        {
                            drCred.Cells["clmCrManualRef"].Value = Convert.ToString(drCred.Cells["clmCrSardInvno"].Value);
                        }
                    }

                    DataTable dt = CHNLSVC.Sales.GetBankDetais(BaseCls.GlbUserComCode);
                    clmCrAccNo.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        clmCrAccNo.Items.Add(dt.Rows[i][2] + " || " + dt.Rows[i][3]);

                    }

                    clmCrMIDcNo.Items.Clear();
                    DataTable ODT = CHNLSVC.Sales.getAllMid_Detailsnew(txtProfitCenter.Text.ToString().Trim());
                    for (int i = 0; i < ODT.Rows.Count; i++)
                    {

                        clmCrMIDcNo.Items.Add(ODT.Rows[i]["MSTM_BANK"] + " || " + ODT.Rows[i]["MPM_MID_NO"] + " || " + ODT.Rows[i]["mstm_sun_acc"] + " || " + ODT.Rows[i]["mbi_cd"]);

                    }
                }
                else
                {
                    dgvCreditCardDets.DataSource = null;
                }


            }
            else
            {
                dgvDepositbanks.DataSource = null;
                dgvCreditCardDets.DataSource = null;
            }

            DataTable dtBank = CHNLSVC.Sales.Get_Bank_Deposit_Slip(_pc, _fromd, _Todate);
            if (dtBank != null && dtBank.Rows.Count > 0)
            {
                dgvDepositBankSlip.AutoGenerateColumns = false;
                dgvDepositBankSlip.DataSource = dtBank;

                DataTable dt = CHNLSVC.Sales.GetBankDetais(BaseCls.GlbUserComCode);
                clmDAccNo.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    clmDAccNo.Items.Add(dt.Rows[i][2] + " || " + dt.Rows[i][3]);

                }

            }
            else
            {
                dgvDepositBankSlip.DataSource = null;
            }


            DataTable dtReChq = CHNLSVC.Sales.Get_Receive_Cheque_details(_pc, _fromd, _Todate);
            if (dtReChq != null && dtReChq.Rows.Count > 0)
            {
                dgvReceiveCheque.AutoGenerateColumns = false;
                dgvReceiveCheque.DataSource = dtReChq;

                DataTable dt = CHNLSVC.Sales.GetBankDetais(BaseCls.GlbUserComCode);
                clmRcAcc.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    clmRcAcc.Items.Add(dt.Rows[i][2] + " || " + dt.Rows[i][3]);

                }

            }
            else
            {
                dgvReceiveCheque.DataSource = null;
            }

            //else
            //{
            //    MessageBox.Show("No Data Found..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}




        }

        private void LoadBankDetails(string company)
        {
            DataTable dt = CHNLSVC.Sales.GetBankDetais(company);
            if (dt != null && dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    // cmbDipositAccNo.Items.Add(dt.Rows[i][2] + " || " + dt.Rows[i][3] + " || " + dt.Rows[i][1]);    

                }
                // cmbDipositAccNo.Text = "--Select Account No--";



            }
            else
            {
                //cmbDipositAccNo.DataSource = null;
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.ChequeNo:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtProfitCenter.Text + seperator + dtpFromDate.Value.Date.ToShortDateString() + seperator + dtpTodate.Value.Date.ToShortDateString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllBankAccount:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.VoucherNo:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EmployeeEPF:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "SRMGR");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }


                default:
                    break;
            }

            return paramsText.ToString();
        }


        private void ImgBtnPC_Click(object sender, EventArgs e)
        {
            try
            {
                txtProfitCenter.Text = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtProfitCenter;
                _CommonSearch.txtSearchbyword.Text = txtProfitCenter.Text;
                _CommonSearch.ShowDialog();
                txtProfitCenter.Focus();

                // get_PCDet();
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

        private List<Deposit_Bank_Pc_wise> fillToDepositBankDetails(out string _msg)
        {
            string msg = string.Empty;
            _lstDeposit = new List<Deposit_Bank_Pc_wise>();
            _listCheque = new List<Deposit_Bank_Pc_wise>();
            _listCreditCard = new List<Deposit_Bank_Pc_wise>();
            //save Cheque Details
            foreach (DataGridViewRow dgvr in dgvDepositbanks.Rows)
            {

                bool chk = Convert.ToBoolean(dgvr.Cells["option"].Value);
                if (chk)
                {

                    objDepositBank = new Deposit_Bank_Pc_wise();
                    DataGridViewComboBoxCell ddl = (DataGridViewComboBoxCell)dgvr.Cells["ddlAccNo"];

                    if (ddl.Value != null)
                    {
                        //check whether already realised -- 22/4/2015
                        if (CHNLSVC.Financial.checkDocrealized(BaseCls.GlbUserComCode, txtProfitCenter.Text, Convert.ToDateTime(dgvr.Cells["clmRecDate"].Value), "CHEQUE", Convert.ToString(dgvr.Cells["clmBankAccNo"].Value), Convert.ToString(dgvr.Cells["clmCheqNo"].Value)) == true)
                        {
                            msg = "Process halted. Cheque no " + " " + Convert.ToString(dgvr.Cells["clmCheqNo"].Value) + " already realized.";
                            _msg = msg;
                            return _lstDeposit;
                        }

                        string _accountNo = ddl.Value.ToString();
                        string[] arr_acc = _accountNo.Split(new string[] { "||" }, StringSplitOptions.None);
                        string _accd = arr_acc[0].Trim();
                        objDepositBank.SunAccNo = _accd;
                        objDepositBank.Mid_no = "";
                        objDepositBank.Line_no = Convert.ToInt32(dgvr.Cells["clmLineNo"].Value);
                        objDepositBank.Seq_no = Convert.ToDouble(dgvr.Cells["clmSeqNo"].Value);
                        objDepositBank.Adj_Type = Convert.ToString(dgvr.Cells["clmType"].Value);
                        objDepositBank.Modifyby = BaseCls.GlbUserID;
                        objDepositBank.Company = Convert.ToString(dgvr.Cells["sar_com_cd"].Value);
                        objDepositBank.Profit_center = txtProfitCenter.Text;
                        objDepositBank.Rem_chq_no = Convert.ToString(dgvr.Cells["clmCheqNo"].Value);
                        objDepositBank.Date = Convert.ToDateTime(dgvr.Cells["clmRecDate"].Value);

                        _listCheque.Add(objDepositBank);
                    }
                    else
                    {
                        MessageBox.Show("Please select Account No for Cheque Details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        msg = "Please select Account No for Cheque Details";
                    }

                }
            }

            //save Credit Card Details
            foreach (DataGridViewRow dgvr in dgvCreditCardDets.Rows)
            {
              
                bool chk = Convert.ToBoolean(dgvr.Cells["clmCrSelect"].Value);
                if (chk)
                {

                    objCreditcard = new Deposit_Bank_Pc_wise();
                    DataGridViewComboBoxCell ddl = (DataGridViewComboBoxCell)dgvr.Cells["clmCrAccNo"];
                    DataGridViewComboBoxCell dd2 = (DataGridViewComboBoxCell)dgvr.Cells["clmCrMIDcNo"];
                    //if (ddl.Value != null)
                    if (dd2.Value != null)
                    {

                        //string _accountNo = ddl.Value.ToString();
                        //string[] arr_acc = _accountNo.Split(new string[] { "||" }, StringSplitOptions.None);
                        //string _accd = arr_acc[0].Trim();
                        //objCreditcard.SunAccNo = _accd;

                        //string mid = CHNLSVC.Sales.Get_midno(txtProfitCenter.Text.Trim(), _accd);
                        //objDepositBank.Mid_no = mid;

                        ///ADD BY THARANGA 2018/08/23
                        string _mid_no = dd2.Value.ToString();
                        string[] arr_mid = _mid_no.Split(new string[] { "||" }, StringSplitOptions.None);
                        string _mid = arr_mid[1].Trim();
                        objCreditcard.Mid_no = _mid.Trim();
                        objCreditcard.SunAccNo = arr_mid[2].Trim();
                        objCreditcard.BankCode = arr_mid[3].Trim();
                        objCreditcard.Line_no = Convert.ToInt32(dgvr.Cells["clmCrLine"].Value);
                        objCreditcard.Seq_no = Convert.ToDouble(dgvr.Cells["clmCrSeq"].Value);
                        objCreditcard.Adj_Type = Convert.ToString(dgvr.Cells["clmCrType"].Value);
                        objCreditcard.Modifyby = BaseCls.GlbUserID;
                        objCreditcard.Rem_chq_no = Convert.ToString(dgvr.Cells["clmCrNo"].Value);
                        _listCreditCard.Add(objCreditcard);
                    }
                    else
                    {
                        MessageBox.Show("Please select Account No for credit card", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        msg = "Please select Account No for credit card";
                    }

                }
            }

            var result = _listCheque.Concat(_listCreditCard);
            _lstDeposit = result.ToList();

            _msg = msg;
            return _lstDeposit;
        }

        private List<Deposit_Bank_Pc_wise> fillToBankSlipDets(out string _msg)
        {
            _lstBank = new List<Deposit_Bank_Pc_wise>();
            _listReceiveCheque = new List<Deposit_Bank_Pc_wise>();
            _listBankSlip = new List<Deposit_Bank_Pc_wise>();

            //save BankSlip Details
            foreach (DataGridViewRow dgvr in dgvDepositBankSlip.Rows)
            {

                bool chk = Convert.ToBoolean(dgvr.Cells["clmDselection"].Value);
                if (chk)
                {

                    objbank_slip = new Deposit_Bank_Pc_wise();
                    DataGridViewComboBoxCell ddl = (DataGridViewComboBoxCell)dgvr.Cells["clmDAccNo"];
                    if (ddl.Value != null)
                    {
                        //check whether already realised -- 22/4/2015
                        if (CHNLSVC.Financial.checkDocrealized(BaseCls.GlbUserComCode, txtProfitCenter.Text, Convert.ToDateTime(dgvr.Cells["clmDdt"].Value), "DEPOSIT", Convert.ToString(dgvr.Cells["clmSBankAcc"].Value), Convert.ToString(dgvr.Cells["clmDRemark"].Value)) == true)
                        {
                            _msg = "Process halted. Slip no " + " " + Convert.ToString(dgvr.Cells["clmDRemark"].Value) + " already realized.";
                            return _lstDeposit;
                        }

                        string _accountNo = ddl.Value.ToString();
                        string[] arr_acc = _accountNo.Split(new string[] { "||" }, StringSplitOptions.None);
                        string _accd = arr_acc[0].Trim();
                        objbank_slip.SunAccNo = _accd;
                        objbank_slip.Company = Convert.ToString(dgvr.Cells["clmDcom"].Value);
                        objbank_slip.Profit_center = Convert.ToString(dgvr.Cells["clmDpc"].Value);
                        objbank_slip.Date = Convert.ToDateTime(dgvr.Cells["clmDdt"].Value);
                        objbank_slip.Rem_sec = Convert.ToString(dgvr.Cells["clmrem_sec"].Value);
                        objbank_slip.Rem_cd = Convert.ToString(dgvr.Cells["clmrem_cd"].Value);
                        objbank_slip.Rem_wk = Convert.ToString(dgvr.Cells["clmDWeek"].Value);

                        objbank_slip.Rem_rmk = Convert.ToString(dgvr.Cells["clmDRemark"].Value);
                        objbank_slip.Rem_chq_no = Convert.ToString(dgvr.Cells["clmDRemark"].Value);
                        objbank_slip.Rem_description = Convert.ToString(dgvr.Cells["clmDdesc"].Value);
                        _listBankSlip.Add(objbank_slip);
                    }
                }
            }

            //save Receive Chq Details
            foreach (DataGridViewRow dgvr in dgvReceiveCheque.Rows)
            {

                bool chk = Convert.ToBoolean(dgvr.Cells["clmRcSelection"].Value);
                if (chk)
                {

                    objReceiveChq = new Deposit_Bank_Pc_wise();
                    DataGridViewComboBoxCell ddl = (DataGridViewComboBoxCell)dgvr.Cells["clmRcAcc"];
                    if (ddl.Value != null)
                    {
                        //check whether already realised -- 22/4/2015
                        if (CHNLSVC.Financial.checkDocrealized(BaseCls.GlbUserComCode, txtProfitCenter.Text, Convert.ToDateTime(dgvr.Cells["clmRcDate"].Value), "CHEQUE", Convert.ToString(dgvr.Cells["clmRcBankacc"].Value), Convert.ToString(dgvr.Cells["clmRemChq"].Value)) == true)
                        {
                            _msg = "Process halted. Cheque no " + " " + Convert.ToString(dgvr.Cells["clmRemChq"].Value) + " already realized.";
                            return _lstDeposit;
                        }

                        string _accountNo = ddl.Value.ToString();
                        string[] arr_acc = _accountNo.Split(new string[] { "||" }, StringSplitOptions.None);
                        string _accd = arr_acc[0].Trim();
                        objReceiveChq.SunAccNo = _accd;
                        objReceiveChq.Company = Convert.ToString(dgvr.Cells["clmRcCom"].Value);
                        objReceiveChq.Profit_center = Convert.ToString(dgvr.Cells["clmRcProfit"].Value);
                        objReceiveChq.Date = Convert.ToDateTime(dgvr.Cells["clmRcDate"].Value);
                        objReceiveChq.Rem_sec = Convert.ToString(dgvr.Cells["clmRcRemsec"].Value);
                        objReceiveChq.Rem_cd = Convert.ToString(dgvr.Cells["clmRcRemcd"].Value);
                        objReceiveChq.Rem_wk = Convert.ToString(dgvr.Cells["clmRcWk"].Value);

                        objReceiveChq.Rem_rmk = Convert.ToString(dgvr.Cells["clmRcRem"].Value);
                        objReceiveChq.Rem_chq_no = Convert.ToString(dgvr.Cells["clmRemChq"].Value);
                        objReceiveChq.Rem_description = Convert.ToString(dgvr.Cells["clmRcDesc"].Value);
                        _listReceiveCheque.Add(objReceiveChq);
                    }
                }
            }

            var resultb = _listBankSlip.Concat(_listReceiveCheque);
            _lstBank = resultb.ToList();

            _msg = "";
            return _lstBank;

        }




        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (dgvDepositbanks.Rows.Count <= 0 && dgvCreditCardDets.Rows.Count <= 0 && dgvDepositBankSlip.Rows.Count <= 0 && dgvReceiveCheque.Rows.Count <= 0)
            {
                MessageBox.Show("Please select required details to update", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string _msg = "";
            _lstDeposit = fillToDepositBankDetails(out _msg);
            if (!string.IsNullOrEmpty(_msg))
            {
                MessageBox.Show(_msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            _msg = "";
            _lstBank = fillToBankSlipDets(out _msg);
            if (!string.IsNullOrEmpty(_msg))
            {
                MessageBox.Show(_msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Are you sure want to update ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            string _error = "";
            int result = CHNLSVC.Sales.UpdateToDiposit(_lstDeposit, _lstBank, out _error);
            if (result == -1)
            {
                MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                MessageBox.Show("Records updated Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnClear_Click(null, null);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtProfitCenter.Text = "";
            // cmbDipositAccNo.Text = "--Select Account No--";
            dgvDepositbanks.DataSource = null;
            dgvCreditCardDets.DataSource = null;
            dgvDepositBankSlip.DataSource = null;
            dgvReceiveCheque.DataSource = null;
        }

        private void cmbDipositAccNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)Keys.None;
        }

        private void txtProfitCenter_Leave(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtProfitCenter.Text))
            //{
            //    Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, txtProfitCenter.Text);
            //    if (_IsValid == false)
            //    {
            //        MessageBox.Show("Invalid Profit Center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        txtProfitCenter.Focus();
            //        return;
            //    }
            //}
        }

        private void btn_srch_accno_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllBankAccount);
            DataTable _result = CHNLSVC.CommonSearch.GetAllBankAccounts(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAccNo;
            _CommonSearch.ShowDialog();
            txtAccNo.Select();
        }

        private void btnBlkUpd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProfitCenter.Text))
            {
                MessageBox.Show("Select the profot center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtChqNo.Text))
            {
                MessageBox.Show("Enter the cheque number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtAccNo.Text))
            {
                MessageBox.Show("Select the account number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            #region select CHEQUE is realized cannot update account no add by tharanga 2018/05/08
            DataTable odt = CHNLSVC.Financial.chk_cheque_realized(BaseCls.GlbUserComCode, txtProfitCenter.Text, dtpFromDate.Value.Date, "CHEQUE", txtChqNo.Text, lblDepAc.Text);
            if (odt.Rows.Count > 0)
            {
                if (Convert.ToInt32(odt.Rows[0]["bstd_is_realized"]) == 1)
                {
                    MessageBox.Show("Select cheque already realized ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            #endregion

            if (MessageBox.Show("Are you sure want to update ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            int result = CHNLSVC.Sales.UpdateBulkToDipositBank(BaseCls.GlbUserComCode,txtProfitCenter.Text,txtChqNo.Text,lblDepAc.Text,txtAccNo.Text,BaseCls.GlbUserID);
            if (result == -1)
            {
                MessageBox.Show("Error occurred while processing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                MessageBox.Show("Records updated Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAccNo.Text = "";
                txtChqNo.Text = "";
                lblDepAc.Text = "";
                lblChqVal.Text = "0.00";
                btnSearch_Click(null, null);
            }

        }

        private void btn_srch_chq_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProfitCenter.Text))
            {
                MessageBox.Show("Select the profot center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ChequeNo);
            DataTable _result = CHNLSVC.CommonSearch.GetDepositChequeSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtChqNo;
            _CommonSearch.ShowDialog();
            btn_srch_accno.Select();

            load_chq_det();
        }

        private void load_chq_det()
        {
            Decimal _val=0;
            DataTable _dt = CHNLSVC.Financial.GetReceiptByRefNo(BaseCls.GlbUserComCode, txtProfitCenter.Text,dtpFromDate.Value.Date,dtpTodate.Value.Date, txtChqNo.Text);
            if (_dt.Rows.Count > 0)
            {
                _val=Convert.ToDecimal(_dt.Rows[0]["sar_tot_settle_amt"]);
                lblChqVal.Text = _val.ToString("0.00");
                lblDepAc.Text = _dt.Rows[0]["sard_deposit_bank_cd"].ToString();
            }
            else
            {
                MessageBox.Show("Invalid cheque number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblChqVal.Text = "0.00";
                lblDepAc.Text = "";
                txtChqNo.Text = "";
                txtChqNo.Focus();
            }
        }


        private void btnCloseAdj_Click(object sender, EventArgs e)
        {
            txtAccNo.Text = "";
            txtChqNo.Text = "";
            lblDepAc.Text = "";
            lblChqVal.Text = "0.00";
            pnlBulk.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnlBulk.Visible = true;
        }

        private void txtChqNo_Leave(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtChqNo.Text))
                load_chq_det();
        }
    }
}
