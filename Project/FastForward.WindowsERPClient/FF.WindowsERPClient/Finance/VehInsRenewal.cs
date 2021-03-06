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
    public partial class VehInsRenewal : Base
    {

        DataTable param = new DataTable();


        public VehInsRenewal()
        {
            InitializeComponent();
            TextBoxLocation.Text = BaseCls.GlbUserDefProf;
        }


        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.VehicalInsuranceRegNo:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.VehicleInsuranceRef:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ManIssRec:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + TextBoxLocation.Text + seperator + lblAccountNo.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + TextBoxLocation.Text + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
            TextBoxLocation.Text = "";
            txtAccountNo.Text = "";
        }

        private void clear()
        {
            lblAccountDate.Text = "";

            lblBal.Text = "0.00";
            lblRecTot.Text = "0.00";
            lblSelTot.Text = "0.00";
            lblPaidTot.Text = "0.00";
            lblPayable.Text = "0.00";
            lblInitial.Text = "0.00";
            lblCashVal.Text = "0.00";
            lblHireVal.Text = "0.00";
            lblPeriod.Text = "";
            lblAccountDate.Text = "";

            //txtAmt.Text = ("0.00").ToString();
            grv_insRent.DataSource = null;

            grvReceipts.DataSource = null;


            txtEngNo.Text = "";
            txtChasisNo.Text = "";
            txtVehNo.Text = "";
            txtRecNo.Text = "";
            txtRecAmt.Text = "";
            txtLine.Text = "";
            txtTotRecAmt.Text = "";
            txtisused.Text = "";

            lblAName.Text = "";
            lblAccountNo.Text = "";

        }



        private void btnSave_Click(object sender, EventArgs e)
        {

            if (Convert.ToDecimal(lblSelTot.Text) == 0)
            {
                MessageBox.Show("Please select receipts !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtReqAmt.Text))
            {
                MessageBox.Show("Please enter request amount !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReqAmt.Focus();
                return;
            }
            if (Convert.ToDecimal(txtReqAmt.Text) <= 0)
            {
                MessageBox.Show("Please enter valid request amount !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReqAmt.Focus();
                return;
            }

            if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            string _errmsg = "";
            param.TableName = "dt";
            Int32 _eff = CHNLSVC.Financial.ProcessVehInsPayReq(BaseCls.GlbUserComCode, TextBoxLocation.Text, lblAccountNo.Text, dtDate.Value.Date, Convert.ToDecimal(txtReqAmt.Text), BaseCls.GlbUserID, param, out _errmsg);
            if (_eff > 0)
            {
                MessageBox.Show("Successfully updated", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
                txtAccountNo.Text = "";
                TextBoxLocation.Text = "";
                param = new DataTable();
            }
            else
            {
                MessageBox.Show(_errmsg, "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            // clear();
        }

        private bool CheckBankBranch(string bank, string branch)
        {
            if (!string.IsNullOrEmpty(branch))
            {
                MasterOutsideParty _bankAccounts = CHNLSVC.Sales.GetOutSidePartyDetailsById(bank.ToUpper().Trim());
                if (_bankAccounts != null)
                {
                    bool valid = CHNLSVC.Sales.validateBank_and_Branch(_bankAccounts.Mbi_cd, branch, "BANK");
                    return valid;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool CheckBank(string bank, Label lbl)
        {
            MasterOutsideParty _bankAccounts = new MasterOutsideParty();
            if (!string.IsNullOrEmpty(bank))
            {
                _bankAccounts = CHNLSVC.Sales.GetOutSidePartyDetailsById(bank.ToUpper().Trim());

                if (_bankAccounts.Mbi_cd != null)
                {

                    lbl.Text = _bankAccounts.Mbi_desc;
                    return true;
                }
                else
                {
                    MessageBox.Show("Please select the valid bank.", "Invild Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return false;

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

                clear();
                txtAccountNo.Text = "";

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
            clear();
            txtAccountNo.Text = "";
        }

        private void TextBoxLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnProfitCenter_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtAccountNo.Focus();
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

            Load_Account_Det();

        }

        private void txtAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                ImgBtnAccountNo_Click(null, null);
        }

        private void Load_Account_Det()
        {
            Decimal _totRec = 0;
            Decimal _totPaid = 0;

            lblAccountNo.Visible = true;
            lblAccountDate.Visible = true;
            BindCustomerDetails(null);

            clear();

            string location = TextBoxLocation.Text;
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

                    lblCashVal.Text = ac.Hpa_cash_val.ToString("#,#00.00");
                    lblHireVal.Text = ac.Hpa_hp_val.ToString("#,#00.00");
                    lblPeriod.Text = ac.Hpa_term.ToString();

                    load_Acc_Receipts(ac.Hpa_acc_no);

                }
                foreach (DataGridViewRow row in grv_insRent.Rows)
                {
                    if (Convert.ToString(row.Cells["ISUSED"].Value) == "YES")
                    {
                        row.DefaultCellStyle.BackColor = Color.PeachPuff;
                        _totPaid = _totPaid + Convert.ToDecimal(row.Cells["sar_bal_amt"].Value);
                        row.ReadOnly = true;
                    }
                    _totRec = _totRec + Convert.ToDecimal(row.Cells["sar_tot_settle_amt"].Value);
                }
                lblPaidTot.Text = _totPaid.ToString("#,#00.00");
                lblRecTot.Text = _totRec.ToString("#,#00.00");
                lblBal.Text = (_totRec - _totPaid).ToString("#,#00.00");

            }
        }

        private void BindAccountItem(string _account)
        {
            List<InvoiceItem> _itemList = new List<InvoiceItem>();
            List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, TextBoxLocation.Text, _account);
            InvoiceHeader _hdrs = null;
            if (_invoice != null)
                if (_invoice.Count > 0) _hdrs = CHNLSVC.Sales.GetInvoiceHeaderDetails(_invoice[0].Sah_inv_no);

            if (_hdrs != null)
                BindCustomerDetails(_hdrs);

            List<VehicleInsuarance> _insurance = CHNLSVC.Sales.GetVehicalInsuranceByInvoice(_invoice[0].Sah_inv_no);
            if (_insurance != null)
            {
                foreach (VehicleInsuarance _tmp in _insurance)
                {
                    txtEngNo.Text = _tmp.Svit_engine;
                    txtChasisNo.Text = _tmp.Svit_chassis;
                    txtVehNo.Text = _tmp.Svit_veh_reg_no;
                }
            }
        }

        private void load_Acc_Receipts(string _invNo)
        {
            grv_insRent.AutoGenerateColumns = false;
            grv_insRent.DataSource = CHNLSVC.Financial.getVehInsReceipts(BaseCls.GlbUserComCode, TextBoxLocation.Text, dtDate.Value.Date, _invNo);

        }

        private void BindCustomerDetails(InvoiceHeader _hdr)
        {
            lblAName.Text = string.Empty;
            if (_hdr != null)
            {
                lblAName.Text = _hdr.Sah_cus_name;
            }
        }

        private void txtAccountNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13)
                return;
            if (!string.IsNullOrEmpty(txtAccountNo.Text))
            {
                Load_Account_Det();
            }
        }

        private void txtAccountNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAccountNo.Text))
            {
                Load_Account_Det();
            }
        }


        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblSelTot.Text = "0.00";
                if (chkAll.Checked == true)
                {
                    foreach (DataGridViewRow row in grv_insRent.Rows)
                    {
                        if (row.DefaultCellStyle.BackColor != Color.PeachPuff)
                        {
                            calc(Convert.ToDecimal(row.Cells[2].Value));
                            row.Cells[3].Value = "YES";
                            row.DefaultCellStyle.BackColor = Color.PaleTurquoise;
                        }
                    }
                    grv_insRent.EndEdit();

                }
                else
                {
                    //  txtAmt.Text = "0.00";
                    foreach (DataGridViewRow row in grv_insRent.Rows)
                    {
                        if (row.DefaultCellStyle.BackColor != Color.PeachPuff)
                        {
                            // calc(-Convert.ToDecimal(row.Cells[2].Value));
                            row.Cells[3].Value = "NO";
                            row.DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                    grv_insRent.EndEdit();

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void calc(Decimal _val)
        {
            lblSelTot.Text = (Convert.ToDecimal(lblSelTot.Text) + _val).ToString("#,#00.00");
            //  txtAmt.Text = Convert.ToDecimal(lblSelTot.Text).ToString("0.00");
        }

        private void grv_insRent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtRecNo.Text = grv_insRent.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtRecAmt.Text = grv_insRent.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtLine.Text = e.RowIndex.ToString();
            txtTotRecAmt.Text = grv_insRent.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtRecDate.Text = Convert.ToDateTime(grv_insRent.Rows[e.RowIndex].Cells[0].Value).Date.ToShortDateString();
            txtisused.Text = grv_insRent.Rows[e.RowIndex].Cells[4].Value.ToString();
        }



        private bool CheckBankAcc(string code)
        {
            MasterBankAccount account = CHNLSVC.Sales.GetBankDetails(BaseCls.GlbUserComCode, null, code);
            if (account == null || account.Msba_com == null || account.Msba_com == "")
            {
                return false;
            }
            else
                return true;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (txtisused.Text == "YES")
            {
                MessageBox.Show("This receipt already used !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtRecNo.Text))
            {
                MessageBox.Show("Please select receipt number !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtRecAmt.Text))
            {
                MessageBox.Show("Please enter value !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRecAmt.Focus();
                return;
            }
            if (Convert.ToDecimal(txtRecAmt.Text) > Convert.ToDecimal(txtTotRecAmt.Text))
            {
                MessageBox.Show("Value cannot be greater than balance amount !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRecAmt.Focus();
                return;
            }
            if (Convert.ToDecimal(lblSelTot.Text) + Convert.ToDecimal(txtRecAmt.Text) > Convert.ToDecimal(txtReqAmt.Text))
            {
                MessageBox.Show("Value cannot be greater than requested amount !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRecAmt.Focus();
                return;
            }

            foreach (DataGridViewRow row in grvReceipts.Rows)
            {
                if (row.Cells[1].Value.ToString().Equals(txtRecNo.Text))
                    return;
            }

            try
            {

                DataRow dr;
                DataTable _tmp = new DataTable();

                _tmp.Columns.Add("recno", typeof(string));
                _tmp.Columns.Add("amt", typeof(Decimal));
                _tmp.Columns.Add("line", typeof(Int32));
                _tmp.Columns.Add("recamt", typeof(Decimal));
                _tmp.Columns.Add("balamt", typeof(Decimal));
                _tmp.Columns.Add("recdate", typeof(DateTime));

                dr = _tmp.NewRow();
                dr["recno"] = txtRecNo.Text;
                dr["amt"] = Convert.ToDecimal(txtRecAmt.Text);
                dr["line"] = Convert.ToInt32(txtLine.Text);
                dr["recamt"] = Convert.ToDecimal(txtTotRecAmt.Text);
                dr["balamt"] = Convert.ToDecimal(txtTotRecAmt.Text) - Convert.ToDecimal(txtRecAmt.Text);
                dr["recdate"] = Convert.ToDateTime(txtRecDate.Text);
                _tmp.Rows.Add(dr);

                param.Merge(_tmp);

                grvReceipts.AutoGenerateColumns = false;
                grvReceipts.DataSource = param;

                if (grv_insRent.Rows[Convert.ToInt32(txtLine.Text)].Cells["ISUSED"].Value.ToString() == "YES")
                {
                    if (grv_insRent.Rows[Convert.ToInt32(txtLine.Text)].DefaultCellStyle.BackColor != Color.PeachPuff)
                    {
                        grv_insRent.Rows[Convert.ToInt32(txtLine.Text)].Cells["ISUSED"].Value = "NO";
                        calc(-Convert.ToDecimal(txtRecAmt.Text));
                        grv_insRent.Rows[Convert.ToInt32(txtLine.Text)].DefaultCellStyle.BackColor = Color.White;
                    }
                }
                else
                {
                    if (Convert.ToDecimal(txtRecAmt.Text) == Convert.ToDecimal(txtTotRecAmt.Text))
                        grv_insRent.Rows[Convert.ToInt32(txtLine.Text)].Cells["ISUSED"].Value = "YES";

                    calc(Convert.ToDecimal(txtRecAmt.Text));
                    grv_insRent.Rows[Convert.ToInt32(txtLine.Text)].DefaultCellStyle.BackColor = Color.PaleTurquoise;
                }



                txtRecNo.Text = "";
                txtRecAmt.Text = "";
                txtLine.Text = "";
                txtTotRecAmt.Text = "";
                txtRecDate.Text = "";
                txtisused.Text = "";

            }
            catch (Exception ex)
            {

            }
        }

        private void grvReceipts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    calc(Convert.ToDecimal(grvReceipts.Rows[e.RowIndex].Cells[2].Value) * -1);
                    grv_insRent.Rows[Convert.ToInt32(grvReceipts.Rows[e.RowIndex].Cells[3].Value)].DefaultCellStyle.BackColor = Color.White;
                    grv_insRent.Rows[Convert.ToInt32(grvReceipts.Rows[e.RowIndex].Cells[3].Value)].Cells["ISUSED"].Value = "NO";

                    param.Rows.RemoveAt(e.RowIndex);
                    grvReceipts.DataSource = param;
                }
            }
        }

        private void grv_insRent_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtRecNo.Text = grv_insRent.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtRecAmt.Text = grv_insRent.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtLine.Text = e.RowIndex.ToString();
            txtTotRecAmt.Text = grv_insRent.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtRecDate.Text = Convert.ToDateTime(grv_insRent.Rows[e.RowIndex].Cells[0].Value).Date.ToShortDateString();
            txtisused.Text = grv_insRent.Rows[e.RowIndex].Cells[4].Value.ToString();

            if (txtisused.Text == "YES")
            {
                MessageBox.Show("This receipt already used !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            btn_Add_Click(null, null);
        }

        private void txtRecAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btn_Add_Click(null, null);
        }

        private void btn_srchVehNo_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VehicalInsuranceRegNo);
            DataTable _result = CHNLSVC.CommonSearch.GetvehicalInsuranceRegistrationNUmber(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtVehicleNo;
            _CommonSearch.ShowDialog();
            txtVehicleNo.Select();

            load_accbyVehNo();
        }

        private void load_accbyVehNo()
        {
            DataTable _dt = CHNLSVC.Sales.GetInsuranceOnEngine(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "Veh", txtVehicleNo.Text);
            if (_dt.Rows.Count > 0)
            {
                txtAccountNo.Text = _dt.Rows[0]["hpa_seq"].ToString();
                TextBoxLocation.Text = _dt.Rows[0]["hpa_pc"].ToString();
            }
            else
            {
                txtAccountNo.Text = "";
                TextBoxLocation.Text = "";
            }

            txtAccountNo_Leave(null, null);

        }

        private void txtVehicleNo_Leave(object sender, EventArgs e)
        {
            load_accbyVehNo();
            txtAccountNo_Leave(null, null);
        }



    }
}
