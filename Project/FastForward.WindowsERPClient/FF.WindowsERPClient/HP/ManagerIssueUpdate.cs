using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;


namespace FF.WindowsERPClient.HP
{
    public partial class ManagerIssueUpdate : Base
    {
        public ManagerIssueUpdate()
        {
            InitializeComponent();
            BindBanks();
        }

        private void BindBanks()
        {
            try
            {
                DataTable datasource2 = CHNLSVC.Financial.GetBanks();
                DropDownListBank.DataSource = datasource2;
                DropDownListBank.DisplayMember = "mbi_desc";
                DropDownListBank.ValueMember = "mbi_cd";
                DropDownListBank.SelectedIndex = -1;

                TextBoxLocation.Text = BaseCls.GlbUserDefProf;
                dtDate.Value = DateTime.Now.Date;

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

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.MgrIssueCheque:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + TextBoxLocation.Text.Trim().ToUpper() + seperator + DropDownListBank.SelectedValue.ToString() + seperator + dtChqDate.Value.ToString("dd/MMM/yyyy") + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void load_receive_receipts(string _accNo)
        {
            grvDet.AutoGenerateColumns = false;
            grvDet.DataSource = CHNLSVC.Sales.Get_Manager_receive_rec(_accNo);
        }

        private void load_Acc_Receipts(string _accNo)
        {
            grv_ucAccDetails.AutoGenerateColumns = false;
            grv_ucAccDetails.DataSource = CHNLSVC.Sales.Get_Manager_Issue_rec(_accNo);

            //if (grv_ucAccDetails.Rows.Count > 0)
            //{
            //    foreach (DataGridViewRow row in grv_ucAccDetails.Rows)
            //    {
            //        if (row.Cells["Column9"].Value != null)
            //        {
            //            if (row.Cells["Column9"].Value.ToString() == "1")
            //            {
            //                row.DefaultCellStyle.BackColor = Color.OrangeRed;
            //            }
            //        }
            //    }
            //}
        }



        private void BindAccountItem(string _account)
        {
            List<InvoiceItem> _itemList = new List<InvoiceItem>();
            List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account);
            InvoiceHeader _hdrs = null;
            if (_invoice != null)
                if (_invoice.Count > 0) _hdrs = CHNLSVC.Sales.GetInvoiceHeaderDetails(_invoice[0].Sah_inv_no);

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void clear()
        {

            grv_ucAccDetails.DataSource = null;
            grvDet.DataSource = null;

            TextBoxChequeNumber.Text = "";
            DropDownListBank.SelectedIndex = -1;

            lblChqAmt.Text = "0.00";
            lblBal.Text = "0.00";
            lblIssAmt.Text = "0.00";
            TextBoxLocation.Focus();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            string _recno = "";
            Boolean _isSelected = false;
            if (grvDet.Rows.Count == 0)
            {
                MessageBox.Show("There is no data to update", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            foreach (DataGridViewRow row in grvDet.Rows)
            {
                if (Convert.ToInt32( row.Cells["Column1"].Value) == 1)
                {
                    _recno = row.Cells["Column10"].Value.ToString();
                    Int32 _eff = CHNLSVC.Sales.UpdateManagerIssueReceipt(BaseCls.GlbUserComCode, TextBoxLocation.Text, _recno, BaseCls.GlbUserID);
                    _isSelected = true;
                }
            }

            if (_isSelected == false)
            {
                MessageBox.Show("There is no data to update", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                MessageBox.Show("Successfully updated", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            clear();
        }

        private void btnCheque_Click(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListBank.SelectedIndex == -1)
                {
                    MessageBox.Show("Select Bank!");
                    return;
                }
                if (TextBoxLocation.Text.Trim() == "")
                {
                    MessageBox.Show("Enter Profit center!");
                    return;
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MgrIssueCheque);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchChqByDate(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxChequeNumber;
                _CommonSearch.ShowDialog();
                btnSave.Select();

                load_receipt_det();
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


        private void load_receipt_det()
        {
            Decimal _RecTotal = 0;
            Decimal _MgrIssTot = 0;

            grv_ucAccDetails.AutoGenerateColumns = false;
            grv_ucAccDetails.DataSource = CHNLSVC.Sales.GetReceiptDetByChqNo(TextBoxChequeNumber.Text, 1);

            foreach (DataGridViewRow row in grv_ucAccDetails.Rows)
            {
                if (Convert.ToString( row.Cells["ISMGRISS"].Value) == "Yes")
                {
                    _MgrIssTot = _MgrIssTot + Convert.ToInt32(row.Cells["sard_settle_amt"].Value);
                }
                _RecTotal = _RecTotal + Convert.ToInt32(row.Cells["sard_settle_amt"].Value);
            }

            lblChqAmt.Text = _RecTotal.ToString("0.00");
            lblIssAmt.Text = _MgrIssTot.ToString("0.00");
            lblBal.Text = (_RecTotal - _MgrIssTot).ToString("0.00");

            grvDet.AutoGenerateColumns = false;
            grvDet.DataSource = CHNLSVC.Sales.GetReceiptDetByChqNo(TextBoxChequeNumber.Text, 0);
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
                _CommonSearch.obj_TragetTextBox = TextBoxLocation;
                _CommonSearch.ShowDialog();
                TextBoxLocation.Select();


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

        private void TextBoxLocation_Leave(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(TextBoxLocation.Text))
            {
                Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, TextBoxLocation.Text);
                if (_IsValid == false)
                {
                    MessageBox.Show("Invalid Profit Center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    TextBoxLocation.Focus();
                    return;
                }
            }

            grv_ucAccDetails.DataSource = null;
            grvDet.DataSource = null;

            TextBoxChequeNumber.Text = "";
            DropDownListBank.SelectedIndex = -1;

            lblChqAmt.Text = "0.00";
            lblBal.Text = "0.00";
            lblIssAmt.Text = "0.00";
        }


        private void TextBoxLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnProfitCenter_Click(null, null);
        }

        private void DropDownListBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            grv_ucAccDetails.DataSource = null;
            grvDet.DataSource = null;

            TextBoxChequeNumber.Text = "";

            lblChqAmt.Text = "0.00";
            lblBal.Text = "0.00";
            lblIssAmt.Text = "0.00";
        }

        private void dtChqDate_ValueChanged(object sender, EventArgs e)
        {
            TextBoxChequeNumber.Text = "";
        }

        private void TextBoxChequeNumber_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxChequeNumber.Text))
                load_receipt_det();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAll.Checked == true)
                {
                    foreach (DataGridViewRow row in grvDet.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[5];
                        chk.Value = true;
                    }
                    grvDet.EndEdit();

                }
                else
                {
                    foreach (DataGridViewRow row in grvDet.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[5];
                        chk.Value = false;
                    }
                    grvDet.EndEdit();

                }
            }
            catch (Exception ex)
            {

            }
        }



    }
}
