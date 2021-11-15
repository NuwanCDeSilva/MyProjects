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
    public partial class ManagerIssueReceive : Base
    {
        List<ESDTxn> ESDTxnList;
        public ManagerIssueReceive()
        {
            InitializeComponent();
        }


        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.ManIssRec:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + lblAccountNo.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void txtAccount()
        {
            lblAccountNo.Visible = true;
            lblAccountDate.Visible = true;
            BindCustomerDetails(null);

            lblAccountNo.Text = "";
            lblAccountDate.Text = "";
            

            string location = BaseCls.GlbUserDefProf;
            string acc_seq = txtAccountNo.Text.Trim();
            try
            {
                Decimal accSeq = Convert.ToDecimal(acc_seq);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter Account's Sequence No."); return;
            }

            List<HpAccount> accList = new List<HpAccount>();
            accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, acc_seq, null);
            if (accList == null)
            {
                MessageBox.Show("Enter valid Account number!"); txtAccountNo.Text = null; return;
            }
            if (accList.Count == 0)
            {
                MessageBox.Show("Enter valid Account number!");
                txtAccountNo.Text = null; return;
            }
            else if (accList.Count == 1)
            {
                foreach (HpAccount ac in accList)
                {
                    BindAccountItem(ac.Hpa_acc_no);
                    lblAccountNo.Text = ac.Hpa_acc_no;
                    lblAccountDate.Text = ac.Hpa_acc_cre_dt.ToString();

                    load_Acc_Receipts(ac.Hpa_acc_no);

                    load_receive_receipts(ac.Hpa_acc_no);

                }
            }
            else if (accList.Count > 1)
            {
                //HpCollection_ECDReq f2 = new HpCollection_ECDReq(this);
                //f2.visible_panel_accountSelect(true);
                //f2.visible_panel_ReqApp(false);
                //f2.fill_AccountGrid(accList); f2.ShowDialog();
            }
        }

        private void load_receive_receipts(string _accNo)
        {
            grvDet.AutoGenerateColumns = false;
            grvDet.DataSource = CHNLSVC.Sales.Get_Manager_receive_rec(_accNo);
        }

        private void load_Acc_Receipts(string _accNo)
        {
            grv_ucAccDetails.AutoGenerateColumns = false;
            grv_ucAccDetails.DataSource = CHNLSVC.Sales.Get_Manager_Issue_rec (_accNo);

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

        private void BindCustomerDetails(InvoiceHeader _hdr)
        {
            lblACode.Text = string.Empty;
            lblAName.Text = string.Empty;
            lblAAddress1.Text = string.Empty;
            if (_hdr != null)
            {
                lblACode.Text = _hdr.Sah_cus_cd;
                lblAName.Text = _hdr.Sah_cus_name;
                lblAAddress1.Text = _hdr.Sah_cus_add1 + " " + _hdr.Sah_cus_add2;
            }
        }

        private void BindAccountItem(string _account)
        {
            List<InvoiceItem> _itemList = new List<InvoiceItem>();
            List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account);
            InvoiceHeader _hdrs = null;
            if (_invoice != null)
                if (_invoice.Count > 0) _hdrs = CHNLSVC.Sales.GetInvoiceHeaderDetails(_invoice[0].Sah_inv_no);

            if (_hdrs != null)
                BindCustomerDetails(_hdrs);
        }

        private void btnSearchRecDoc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ManIssRec);
                DataTable _result = CHNLSVC.CommonSearch.GetManagerIssuReceipts(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRecNoDocReceive;
                _CommonSearch.txtSearchbyword.Text = txtRecNoDocReceive.Text;
                _CommonSearch.ShowDialog();
                txtRecNoDocReceive.Focus();

                get_ACCNO_byRecNo(txtRecNoDocReceive.Text);
                txtAccount();
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

        private void get_ACCNO_byRecNo(string _recno)
        {
            DataTable DT = CHNLSVC.Sales.GetReceipt(_recno);
            if (DT.Rows.Count > 0)
            {
                string _accno= DT.Rows[0]["sar_acc_no"].ToString();
                lblAccountNo.Text =_accno;
                int X = _accno.IndexOf('-');
                string _accSeqNo = _accno.Substring(X + 1, 6);
                txtAccountNo.Text = _accSeqNo;
            }
        }

        private void ImgBtnAccountNo_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
            DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAccountNo;
            _CommonSearch.ShowDialog();
            txtAccountNo.Select();
            txtRecNoDocReceive.Text = "";
        }

        private void txtAccountNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13)
                return;
            if (!string.IsNullOrEmpty(txtAccountNo.Text))
            {
                txtRecNoDocReceive.Text = "";
                txtAccount();
            }
        }

        private void txtAccountNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAccountNo.Text))
            {
                txtRecNoDocReceive.Text = "";
                txtAccount();
            }
        }

        private void txtAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                ImgBtnAccountNo_Click(null, null);

        }

        private void grv_ucAccDetails_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtSysRec.Text = "";
            txtManRec.Text = "";
            lblTotRecAmt.Text = "0.00";
            lblTotReceive.Text = "0.00";
            lblBal.Text = "0.00";
            txtAmt.Text = ("0.00").ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void clear()
        {
            lblAccountDate.Text = "";
            lblTotRecAmt.Text = "0.00";
            lblTotReceive.Text = "0.00";
            lblBal.Text = "0.00";
            txtAmt.Text = ("0.00").ToString();
            grv_ucAccDetails.DataSource = null;
            grvDet.DataSource = null;
            lblACode.Text = "";
            lblAName.Text = "";
            lblAAddress1.Text = "";
            txtAccountNo.Text = "";
            txtRecNoDocReceive.Text = "";
            lblAccountNo.Text = "";
            txtSysRec.Text = "";
            txtManRec.Text = "";
            txtRem.Text = "";
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtAmt.Text) > Convert.ToDecimal(lblBal.Text))
            {
                MessageBox.Show("Invalid Amount !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmt.Focus();
                return;
            }
            if (Convert.ToDecimal(txtAmt.Text) <0)
            {
                MessageBox.Show("Invalid Amount !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmt.Focus();
                return;
            }

            if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            Int32 _eff=CHNLSVC.Sales.Save_mgr_rcv(BaseCls.GlbUserComCode,BaseCls.GlbUserDefProf,lblAccountNo.Text,txtSysRec.Text,dtDate.Value.Date,Convert.ToDecimal( txtAmt.Text),BaseCls.GlbUserID,txtRem.Text);
            MessageBox.Show("Successfully updated", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            load_receive_receipts(lblAccountNo.Text);
            load_Acc_Receipts(lblAccountNo.Text);

           // clear();
        }

        private void grvDet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (Convert.ToDateTime(grvDet.Rows[e.RowIndex].Cells[1].Value) == DateTime.Now.Date)
                {
                    if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Int32 _seqno = Convert.ToInt32(grvDet.Rows[e.RowIndex].Cells[4].Value);
                        string _recno=grvDet.Rows[e.RowIndex].Cells[2].Value.ToString();
                        Decimal _val=Convert.ToDecimal( grvDet.Rows[e.RowIndex].Cells[3].Value);

                        int _eff = CHNLSVC.Sales.DeleteMgrIssueReceive(_seqno, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _recno, _val);
                        MessageBox.Show("Successfully deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        load_receive_receipts(lblAccountNo.Text);
                        load_Acc_Receipts(lblAccountNo.Text);
                        

                    }
                }
                else
                {
                    MessageBox.Show("Cannot delete previous receives!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void txtRecNoDocReceive_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRecNoDocReceive.Text))
            {
                get_ACCNO_byRecNo(txtRecNoDocReceive.Text);
                txtAccount();
            }
        }

        private void txtRecNoDocReceive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRecNoDocReceive_Leave(null, null);
            }
        }

        private void grv_ucAccDetails_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           // if (e.RowIndex != -1 && e.ColumnIndex == 0)
           // {
                Decimal _tot = Convert.ToDecimal(grv_ucAccDetails.Rows[e.RowIndex].Cells[5].Value);
                Decimal _rec = Convert.ToDecimal(grv_ucAccDetails.Rows[e.RowIndex].Cells[8].Value);
                Decimal _bal = Convert.ToDecimal(grv_ucAccDetails.Rows[e.RowIndex].Cells[9].Value);
                lblTotRecAmt.Text = _tot.ToString("0.00");
                lblTotReceive.Text = _rec.ToString("0.00");
                lblBal.Text = _bal.ToString("0.00");

                txtSysRec.Text = grv_ucAccDetails.Rows[e.RowIndex].Cells[10].Value.ToString();
                txtManRec.Text = grv_ucAccDetails.Rows[e.RowIndex].Cells[7].Value.ToString();
                txtAmt.Focus();
           // }
        }

        private void grv_ucAccDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtAmt_KeyPress(object sender, KeyPressEventArgs e)
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






    }
}
