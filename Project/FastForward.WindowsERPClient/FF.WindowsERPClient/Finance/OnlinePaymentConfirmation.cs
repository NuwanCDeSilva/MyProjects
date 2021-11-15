using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Finance
{
    public partial class OnlinePaymentConfirmation : FF.WindowsERPClient.Base
    {
        public OnlinePaymentConfirmation()
        {
            InitializeComponent();
            cmbDocTp.SelectedIndex = 0;
            cmbTp.SelectedIndex = 0;
            btnSearch_Click(null, null);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbDocTp.Text))
            { MessageBox.Show("Please select the document type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            if (dtpFrmDt.Value.Date > dtpToDt.Value.Date)
            { MessageBox.Show("Please check the date range.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            string _status = string.Empty;
            if (cmbTp.Text.Contains("PENDING")) _status = "P";
            if (cmbTp.Text.Contains("APPROVED")) _status = "A";
            if (cmbTp.Text.Contains("REJECTED")) _status = "R";

            DataTable _tbl = CHNLSVC.Inventory.GetOnlinePayment(BaseCls.GlbUserComCode, _status, cmbDocTp.Text.Trim(), dtpFrmDt.Value.Date, dtpToDt.Value.Date);
            if (_tbl == null && _tbl.Rows.Count <= 0)
            { if (string.IsNullOrWhiteSpace(cmbTp.Text)) MessageBox.Show("There is no documents available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); else MessageBox.Show("There is no " + cmbTp.Text + " documents available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            gvItem.AutoGenerateColumns = false;
            gvItem.DataSource = _tbl;

        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            string _refno = "";
            cmbDocTp.Focus();
            cmbTp.Focus();
            btnReject.Select();

            if (gvItem.RowCount <= 0)
            { MessageBox.Show("Please select the document", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            var _check = from DataGridViewRow r in gvItem.Rows where Convert.ToBoolean(r.Cells["c_select"].Value) == true select r;
            if (_check == null || _check.Count() <= 0)
            { MessageBox.Show("Please select the document", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            string _msgTot = string.Empty;


            if (MessageBox.Show("Are you Sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            foreach (DataGridViewRow r in _check)
            {
                try
                {
                    string _msg = string.Empty;
                    _refno = Convert.ToString(r.Cells["c_webref"].Value);
                    CHNLSVC.Inventory.UpdateOnlinePayment(Convert.ToString(r.Cells["c_com"].Value), Convert.ToString(r.Cells["c_pc"].Value), Convert.ToString(r.Cells["c_webref"].Value), "R", BaseCls.GlbUserID, string.Empty, out _msg);
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        _msgTot += _msg + " when updating " + Convert.ToString(r.Cells["c_webref"].Value);
                       
                    }

                }
                catch (Exception ex)
                {
                    _msgTot += ex.Message;
                }
            }

            if (!string.IsNullOrEmpty(_msgTot))
                MessageBox.Show(_msgTot, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                MessageBox.Show("Successfully " + _refno + " rejected.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            gvItem.DataSource = null;
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            cmbDocTp.Focus();
            cmbTp.Focus();
            btnApprove.Select();
            if (gvItem.RowCount <= 0)
            { MessageBox.Show("Please select the document", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            var _check = from DataGridViewRow r in gvItem.Rows where Convert.ToBoolean(r.Cells["c_select"].Value) select r;
            if (_check == null || _check.Count() <= 0)
            { MessageBox.Show("Please select the pending document", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            List<RecieptHeader> _headerLst = new List<RecieptHeader>();
            List<RecieptItem> _itemLst = new List<RecieptItem>();

            StringBuilder _errorLst = new StringBuilder();

            foreach (DataGridViewRow r in _check)
            {

                RecieptHeader _hdr = new RecieptHeader();
                RecieptItem _itm = new RecieptItem();
                DataTable _cus = CHNLSVC.Sales.GetAccountCustomer(BaseCls.GlbUserComCode, Convert.ToString(r.Cells["c_pc"].Value), Convert.ToString(r.Cells["c_accno"].Value));
                if (_cus == null || _cus.Rows.Count <= 0)
                    if (string.IsNullOrEmpty(Convert.ToString(_errorLst)))
                        _errorLst.Append("Customer not found in " + Convert.ToString(r.Cells["c_accno"].Value));
                    else
                        _errorLst.Append("and Customer not found in " + Convert.ToString(r.Cells["c_accno"].Value));

                _hdr.Sar_acc_no = Convert.ToString(r.Cells["c_accno"].Value);
                _hdr.Sar_act = true;
                _hdr.Sar_com_cd = BaseCls.GlbUserComCode;
                _hdr.Sar_create_by = BaseCls.GlbUserID;
                _hdr.Sar_currency_cd = "LKR";
                _hdr.Sar_debtor_add_1 = _cus.Rows[0].Field<string>("mbe_add1");
                _hdr.Sar_debtor_add_2 = _cus.Rows[0].Field<string>("mbe_add2");
                _hdr.Sar_debtor_cd = _cus.Rows[0].Field<string>("mbe_cd");
                _hdr.Sar_debtor_name = _cus.Rows[0].Field<string>("mbe_name");
                _hdr.Sar_direct = true;
                _hdr.Sar_manual_ref_no = Convert.ToString(r.Cells["c_webref"].Value);
                _hdr.Sar_mob_no = _cus.Rows[0].Field<string>("mbe_mob");
                _hdr.Sar_mod_by = BaseCls.GlbUserID;
                _hdr.Sar_nic_no = _cus.Rows[0].Field<string>("mbe_nic");
                _hdr.Sar_prefix = "WEBHP";
                _hdr.Sar_profit_center_cd = Convert.ToString(r.Cells["c_pc"].Value);
                _hdr.Sar_receipt_date = dtpDate.Value.Date;
                _hdr.Sar_receipt_no = Convert.ToString(r.Cells["c_webref"].Value);
                _hdr.Sar_receipt_type = "WEBHP";
                _hdr.Sar_ref_doc = Convert.ToString(r.Cells["c_webref"].Value);
                _hdr.Sar_session_id = BaseCls.GlbUserID;
                _hdr.Sar_tel_no = _cus.Rows[0].Field<string>("mbe_contact");
                _hdr.Sar_tot_settle_amt = Convert.ToDecimal(r.Cells["c_amount"].Value);
                _hdr.Sar_used_amt = 0;
                _headerLst.Add(_hdr);

                _itm.Sard_inv_no = string.Empty;
                _itm.Sard_line_no = 1;
                _itm.Sard_pay_tp = Convert.ToString(r.Cells["c_paytp"].Value);
                _itm.Sard_receipt_no = Convert.ToString(r.Cells["c_webref"].Value);
                _itm.Sard_ref_no = Convert.ToString(r.Cells["c_webref"].Value);
                _itm.Sard_rmk = string.Empty;
                _itm.Sard_settle_amt = Convert.ToDecimal(r.Cells["c_amount"].Value);
                _itm.Sard_anal_3 = Convert.ToDecimal(r.Cells["c_bkcharge"].Value);
                _itemLst.Add(_itm);

            }

            if (!string.IsNullOrEmpty(_errorLst.ToString()))
            {
                MessageBox.Show("Please refer the error list below. " + _errorLst.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            try
            {
                int _effect = 0;
                List<string> _receipts = new List<string>();
                string _msg = string.Empty;
                _effect = CHNLSVC.Sales.ProcessOnlinePayment(BaseCls.GlbUserComCode, _headerLst, _itemLst, out _receipts, _msg);
                if (_receipts != null && _receipts.Count > 0)
                {
                    StringBuilder _bul = new StringBuilder();
                    foreach (string _rec in _receipts)
                        if (string.IsNullOrEmpty(Convert.ToString(_bul)))
                            _bul.Append(_rec);
                        else _bul.Append("," + _rec);

                    MessageBox.Show("Document Generated. " + _bul.ToString(),"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null, null);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dtpDate.Value = CHNLSVC.Security.GetServerDateTime();
            dtpToDt.Value = dtpDate.Value;
            cmbDocTp.SelectedIndex = 0;
            cmbTp.SelectedIndex = 0;
            gvItem.DataSource = new DataTable();
        }

        private void cmbTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Convert.ToString(cmbTp.Text).Contains("PENDING"))
            {
                btnApprove.Enabled = false;
                btnReject.Enabled = false;
            }
            else
            {
                btnApprove.Enabled = true;
                btnReject.Enabled = true;
            }
        }

        private void OnlinePaymentConfirmation_Load(object sender, EventArgs e)
        {

        }

    }
}
