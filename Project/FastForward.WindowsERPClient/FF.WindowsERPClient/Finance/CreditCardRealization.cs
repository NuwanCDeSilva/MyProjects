//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
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
    public partial class CreditCardRealization : Base
    {
        private List<BankRealDet> _bnkRealList = null;
        
        string phy_val = "";
        string net_val = "";
        public CreditCardRealization()
        {
            InitializeComponent();
            textProfitCenter.Text = BaseCls.GlbUserDefProf;
           
            lblUsrMsg.Visible = false;
            
        }

        private void bind_Combo_Accounts()
        {
             DataTable dtaccounts = CHNLSVC.Sales.LoadTransactionAccounts();
            if (dtaccounts.Rows.Count > 0)
            {

                cmbBankAcc.DataSource = dtaccounts;
                cmbBankAcc.DisplayMember = "MSTM_SUN_ACC";
                cmbBankAcc.Text = "--Select--";
                
                
              
            }
            else
            {
                cmbBankAcc.DataSource = null;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            lblUsrMsg.Visible = false;
            string pc = textProfitCenter.Text.Trim();
            if (checkAllpc.Checked == true) pc = "ALL";
            if (cmbBankAcc.Text == "--Select--")
            {
                MessageBox.Show("Please select Account No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dtDate.Value.Month + "/" + dtDate.Value.Year).Date;
                string MID = "ALL";
                Decimal _wkNo = 0;
                int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(dtDate.Text).Date, out _wkNo, BaseCls.GlbUserComCode);
                Int32 _eff = CHNLSVC.Financial.Process_bank_realization_crcd(BaseCls.GlbUserComCode,pc, dtDate.Value.Date, cmbBankAcc.Text, Convert.ToInt32(checkAllpc.Checked), BaseCls.GlbUserID, _tmpMonth, Convert.ToInt32(_wkNo));
                if (_eff == 1)
                {
                    DataTable dt = CHNLSVC.Financial.get_crcd_realization_det(BaseCls.GlbUserComCode, textProfitCenter.Text.Trim().ToUpper(), dtDate.Value.Date, cmbBankAcc.Text, null, 0, 0, 2, 0, 0, Convert.ToInt32(chkWithNIS.Checked), MID);
                    datagridrealizedprocess.AutoGenerateColumns = false;
                    datagridrealizedprocess.DataSource = dt;
                    set_grid_data();
                    check_is_realize();
                    cal_credit_card_netAmount();
                    if (dt.Rows.Count > 0)
                        MessageBox.Show("Completed", "Credit Card Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("No data found !", "Credit Card Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblUsrMsg.Visible = true;      
                }
            }
        }
        private void set_grid_data()
        {

            int rowCount = 0;
            foreach (DataGridViewRow row in datagridrealizedprocess.Rows)
            {
                DataGridViewCheckBoxCell chkIsrealized = new DataGridViewCheckBoxCell();
                chkIsrealized = (DataGridViewCheckBoxCell)datagridrealizedprocess.Rows[rowCount].Cells["IsRealized"];
                
                row.Cells["RealizedYesNo"].Value = "NO";
                if (Convert.ToString(row.Cells["bstd_is_extra"].Value) == "1")
                {
                    row.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    row.ReadOnly = true;
                }
                string RealizedDate = row.Cells["RealizedDate"].Value.ToString();
                if (RealizedDate != "31-DEC-2999")
                {
                    row.Cells["RealizedYesNo"].Value = "YES";
                    row.ReadOnly = true;
                    row.Cells["RealizedYesNo"].ReadOnly = true;
                    row.Cells["NetAmount"].ReadOnly = true;
                    chkIsrealized.Value = true;
                }
                else
                {
                    row.Cells["RealizedDate"].Value = DBNull.Value;
                }
                rowCount++;
            }
        }
        private void check_is_realize()
        {
            foreach (DataGridViewRow row in datagridrealizedprocess.Rows)
            {
                if (Convert.ToString(row.Cells["BSTD_IS_REALIZED"].Value) == "YES")
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["IsRealized"];
                    chk.Value = true;
                }
            }
        }

        //private void datagridrealizedprocess_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    int index = datagridrealizedprocess.CurrentRow.Index;
        //    if (e.RowIndex != -1 && e.ColumnIndex == 8)
        //    {
        //        DataGridViewCheckBoxCell chkIsrealized = new DataGridViewCheckBoxCell();
        //        chkIsrealized = (DataGridViewCheckBoxCell)datagridrealizedprocess.Rows[datagridrealizedprocess.CurrentRow.Index].Cells["IsRealized"];
        //        if (datagridrealizedprocess.Rows[Convert.ToInt32(e.RowIndex)].Cells["RealizedYesNo"].Value.ToString() == "YES")
        //        {
        //            datagridrealizedprocess.Rows[index].Cells["RealizedDate"].Value = DBNull.Value;
        //            datagridrealizedprocess.Rows[index].Cells["RealizedYesNo"].Value = "NO";
        //        }
        //        else
        //        {
        //            datagridrealizedprocess.Rows[index].Cells["RealizedDate"].Value = DateTime.Now.ToString("dd/MMM/yyyy");
        //            datagridrealizedprocess.Rows[index].Cells["RealizedYesNo"].Value = "YES";
        //            chkIsrealized.Value = true;
        //        }  
                
        //    }

        //}
        private void cal_credit_card_netAmount()
        {
            Decimal crcdnetAmnt = 0;
            foreach (DataGridViewRow row in datagridrealizedprocess.Rows)
            {
               
                    crcdnetAmnt = crcdnetAmnt + Convert.ToDecimal(row.Cells["NetAmount"].Value);
                
            }
            textcrdcardnetamount.Text = Convert.ToString(crcdnetAmnt);
        }

        private void datagridrealizedprocess_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 4 || e.ColumnIndex == 5)
                {
                    Decimal BankchrgAmnt = Convert.ToDecimal(datagridrealizedprocess.Rows[e.RowIndex].Cells["Physicalval"].Value) - Convert.ToDecimal(datagridrealizedprocess.Rows[e.RowIndex].Cells["NetAmount"].Value);
                    datagridrealizedprocess.Rows[e.RowIndex].Cells["Bnkchargs"].Value = BankchrgAmnt;
                    cal_credit_card_netAmount();
                }
            }
        }

        private void cmbBankAcc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string AccNo = cmbBankAcc.Text;
            //DataTable dtmidno = CHNLSVC.Sales.Load_MID_codes_perAcc(AccNo);
            //if (dtmidno.Rows.Count > 0)
            //{
            //    cmbmidno.DataSource = dtmidno;
            //    cmbmidno.DisplayMember = "MSTM_MID";
            //    cmbmidno.Text = "--Select--";
            //    cmbmidno.Focus();
            //}
            //else
            //{
            //    cmbmidno.DataSource = null;
            //}
        }

        private void cmbBankAcc_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //string pc="ALL";
            //if(!string.IsNullOrEmpty(textProfitCenter.Text))
            //{
            //    pc = textProfitCenter.Text;
            //}
            //string AccNo = cmbBankAcc.Text;
            //getBank();
            //DataTable dtmidno = CHNLSVC.Sales.Load_MID_codes_perAcc(AccNo,pc);
            //if (dtmidno.Rows.Count > 0)
            //{
            //    cmbmidno.DataSource = dtmidno;
            //    cmbmidno.DisplayMember = "MSTM_MID";
            //    cmbmidno.Text = "--Select--";
            //    cmbmidno.Focus();
            //}
            //else
            //{
            //    cmbmidno.DataSource = null;
            //}
        }

        private void getBank()
        {
            lblAccBankCd.Text = "";
            MasterBankAccount _tmpBankAcc = new MasterBankAccount();
            _tmpBankAcc = CHNLSVC.Sales.GetBankDetails(BaseCls.GlbUserComCode, null, cmbBankAcc.Text);

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

        private void btnSearchOthbnk_Click(object sender, EventArgs e)
        {
            btnAddAdj.Enabled = true; 
            string _pc = "ALL";
            lblUsrMsg.Visible = false;
            decimal amountfrom = 0;
            decimal amountTo = 999999999999;
            string MID = "ALL";
            datagridrealizedprocess.ReadOnly = false;
            Boolean Finalized=chkIsFinalized();
           
            if (cmbBankAcc.Text == "--Select--")
            {
                MessageBox.Show("Please select the account number", "Credit Card Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btn_srch_accno.Focus();
                return;
            }
            if (cmbmidno.Text == "--Select--" && checkallmid.Checked==false)
            {
                MessageBox.Show("Please select the MID code or Select 'ALL' option", "Credit Card Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbmidno.Focus();
                return;
            }
            //if (string.IsNullOrEmpty(textamountfrom.Text) || string.IsNullOrEmpty(textamountto.Text))
            //{
            //    MessageBox.Show("Please enter the value range", "Credit Card Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    textamountfrom.Focus();
            //    return;
            //}
            if (checkAllpc.Checked == false && string.IsNullOrEmpty(textProfitCenter.Text))
            {
                MessageBox.Show("Please select the profit center", "Credit Card Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textProfitCenter.Focus();
                return;
            }
           
            if (textProfitCenter.Text.Length > 0 && textProfitCenter.Enabled == true && checkAllpc.Checked == false) _pc = textProfitCenter.Text;
            if (cmbmidno.Text != "--Select--" && cmbmidno.Enabled==true) MID = cmbmidno.Text;
            
            if (!string.IsNullOrEmpty(textamountfrom.Text)) amountfrom = Convert.ToDecimal(textamountfrom.Text);
            if (!string.IsNullOrEmpty(textamountto.Text))   amountTo = Convert.ToDecimal(textamountto.Text);
            string _docType="CRCD";
            Int32 _rlsStatus = 2;
            if (checkrealize.Checked == true) _rlsStatus = 1;
            if (checknotrealize.Checked == true) _rlsStatus = 0;
            DataTable dt = CHNLSVC.Financial.get_crcd_realization_det(BaseCls.GlbUserComCode, _pc.ToUpper(), Convert.ToDateTime(dtDate.Value).Date, cmbBankAcc.Text, _docType, amountfrom, amountTo, _rlsStatus, Convert.ToInt32(chkNIS.Checked), Convert.ToInt32(chkOthBank.Checked), 0, MID);
            datagridrealizedprocess.AutoGenerateColumns = false;
            datagridrealizedprocess.DataSource = dt;
            set_grid_data();
            check_is_realize();
            cal_credit_card_netAmount();
            //MessageBox.Show("Done", "Credit Card Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (Finalized == true)
            {
                datagridrealizedprocess.ReadOnly = true;
                MessageBox.Show("Process finalized to selected date and account", "Credit Card Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
            }
        }

        private bool chkIsFinalized()
        {
            bool isFinalized = false;
            DataTable dt = CHNLSVC.Financial.chkIsFinalize(cmbBankAcc.Text, Convert.ToDateTime(dtDate.Value).Date);
            if (dt.Rows.Count > 0) isFinalized = true;
            return isFinalized;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ?", "Credit Card Realization", MessageBoxButtons.YesNo) == DialogResult.No) return;

            _bnkRealList = new List<BankRealDet>();
            foreach (DataGridViewRow row in datagridrealizedprocess.Rows)
            {
                phy_val = row.Cells["Physicalval"].Value.ToString();
                net_val = row.Cells["NetAmount"].Value.ToString();
               // Boolean validate = checkValidation();
               // if (validate == true)
               // {
                 BankRealDet _bnkReal = new BankRealDet();
                _bnkReal.Bstd_com = BaseCls.GlbUserComCode;
                _bnkReal.Bstd_pc = row.Cells["Bstd_pc"].Value.ToString();
                _bnkReal.Bstd_dt = Convert.ToDateTime(row.Cells["Date"].Value);
                _bnkReal.Bstd_accno = cmbBankAcc.Text;
                _bnkReal.Bstd_doc_tp = "CRCD";
                _bnkReal.Bstd_doc_desc = "CRCD";
                _bnkReal.Bstd_doc_ref = row.Cells["RefNo"].Value.ToString();
                _bnkReal.Bstd_sys_val = Convert.ToDecimal(row.Cells["Amount"].Value);
                _bnkReal.Bstd_hiddn_ref = row.Cells["RefHidden"].Value.ToString();

                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["IsRealized"];
                    _bnkReal.Bstd_is_realized = Convert.ToBoolean(chk.Value) == true ? 1 : 0;

                    if (!string.IsNullOrEmpty(row.Cells["RealizedDate"].Value.ToString()))
                        _bnkReal.Bstd_realized_dt = Convert.ToDateTime(row.Cells["RealizedDate"].Value);
                    else
                        _bnkReal.Bstd_realized_dt = Convert.ToDateTime("31/Dec/2999");

                    
                   
                    if (Convert.ToDecimal(row.Cells["Physicalval"].Value) > 0)
                        _bnkReal.Bstd_phy_val = Convert.ToDecimal(row.Cells["Physicalval"].Value);
                    else
                        _bnkReal.Bstd_phy_val = Convert.ToDecimal(row.Cells["Physicalval"].Value);

                    if (Convert.ToDecimal(row.Cells["NetAmount"].Value) > 0)
                        _bnkReal.Bstd_net_val = Convert.ToDecimal(row.Cells["NetAmount"].Value);
                    else
                        _bnkReal.Bstd_net_val = Convert.ToDecimal(row.Cells["NetAmount"].Value);

                    if (Convert.ToDecimal(row.Cells["Bnkchargs"].Value) > 0)
                        _bnkReal.Bstd_bnk_val = Convert.ToDecimal(row.Cells["Bnkchargs"].Value);
                    else
                        //_bnkReal.Bstd_bnk_val = Convert.ToDecimal(row.Cells["Bnkchargs"].Value);
                        _bnkReal.Bstd_bnk_val = 0;


                    if (row.Cells["Remark"].Value.ToString() != null)
                        _bnkReal.Bstd_rmk =Convert.ToString(row.Cells["Remark"].Value);
                    else
                        _bnkReal.Bstd_rmk = "";

                    _bnkReal.Bstd_deposit_bank = "";
                    _bnkReal.Bstd_doc_bank_cd = "";
                    _bnkReal.Bstd_doc_bank_branch = "";
                    _bnkReal.Bstd_is_no_sun = 0;
                    _bnkReal.Bstd_is_no_state = 0;
                    _bnkReal.Bstd_is_scan = 1;
                    _bnkReal.Bstd_cre_by = BaseCls.GlbUserID;
                    _bnkReal.Bstd_is_new = 0;
                    _bnkReal.bstd_is_extra = 0;
                    _bnkReal.Bstd_seq_no = Convert.ToInt32(row.Cells["hidden_seq"].Value);
                    _bnkRealList.Add(_bnkReal);
                    
               // }
               
                   
                
               
            }
            string _message = "";
            Int32 _eff = CHNLSVC.Financial.UpdateCRCDRealizationDet(_bnkRealList, out _message);
            if (_eff == 1)
            {
                MessageBox.Show("Successfully Saved !", "Credit Card Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSearchOthbnk_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Unsuccessfully Saved !", "Credit Card Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool checkValidation()
        {
            bool validate = true;
            if (!phy_val.All(c => Char.IsDigit(c)) || !phy_val.All(c => Char.IsDigit(c)))
            {
                MessageBox.Show("Amounts need to be a numeric", "Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
                validate = false;
            }
            else if (Convert.ToDecimal(phy_val) < 0 || Convert.ToDecimal(net_val) < 0 && validate == true)
            {
                MessageBox.Show("Amounts cannot be less than zer", "Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
                validate = false;
            }
            return validate;
        }

        private void checkrealize_CheckedChanged(object sender, EventArgs e)
        {
            if (checkrealize.Checked == true)
            {
                checknotrealize.Checked = false;
                checkall.Checked = false;
            }
        }

        private void checknotrealize_CheckedChanged(object sender, EventArgs e)
        {
            if (checknotrealize.Checked == true)
            {
                checkrealize.Checked = false;
                checkall.Checked = false;
            }
        }

        private void checkall_CheckedChanged(object sender, EventArgs e)
        {
            if (checkall.Checked == true)
            {
                checkrealize.Checked = false;
                checknotrealize.Checked = false;
            }
        }

        private void checkallmid_CheckedChanged(object sender, EventArgs e)
        {
            if (checkallmid.Checked == true)
            {
                cmbmidno.Enabled = false;
            }
            else
            {
                cmbmidno.Enabled = true;
                cmbmidno.Focus();
            }
        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            Int32 _eff = CHNLSVC.Financial.FinalizeBankRealization(BaseCls.GlbUserComCode, dtDate.Value.Date, cmbBankAcc.Text, BaseCls.GlbUserID );
            if (_eff == 1)
            {
                MessageBox.Show("Successfully finalized !", "Credit Card Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnFinalize.Enabled = false;
                btnProcess.Enabled = false;
            }
            else
            {
                MessageBox.Show("Unsuccesfully Process !", "Credit Card Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void checkAllpc_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAllpc.Checked == false)
            {
                textProfitCenter.Enabled = true;
                btn_srch_accno.Enabled = true;
            }
            else
            {
                textProfitCenter.Enabled = false;
                btn_srch_accno.Enabled = false;
            }
        }

        private void btn_srch_accno_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textProfitCenter;
                //_CommonSearch.txtSearchbyword.Text = txtPC.Text;
                _CommonSearch.ShowDialog();
                textProfitCenter.Focus();
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
                //case CommonUIDefiniton.SearchUserControlType.HpAccount:
                //    {
                //        paramsText.Append(BaseCls.GlbUserComCode + seperator + selectPC + seperator + "" + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.AccountDate:
                //    {
                //        paramsText.Append(BaseCls.GlbUserComCode + seperator + selectPC + seperator);
                //        break;
                //    }
                //case CommonUIDefiniton.SearchUserControlType.Scheme:
                //    {
                //        paramsText.Append(txtSchemeCD_MM_new.Text.Trim() + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.NIC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void textamountfrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textamountfrom.Text))
            {
                decimal x;
                if (decimal.TryParse(textamountfrom.Text, out x))
                {
                    if (textamountfrom.Text.IndexOf('.') != -1 && textamountfrom.Text.Split('.')[1].Length > 2)
                    {
                        MessageBox.Show("The maximum decimal points are 2!");
                        textamountfrom.Focus();
                    }
                    else textamountfrom.Text = x.ToString(".00");
                }
                else
                {
                    MessageBox.Show("Data invalid!");
                    textamountfrom.Focus();
                }
            }
        }

        private void textamountto_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textamountto.Text))
            {
                decimal x;
                if (decimal.TryParse(textamountto.Text, out x))
                {
                    if (textamountto.Text.IndexOf('.') != -1 && textamountto.Text.Split('.')[1].Length > 2)
                    {
                        MessageBox.Show("The maximum decimal points are 2!");
                        textamountto.Focus();
                    }
                    else textamountto.Text = x.ToString(".00");
                }
                else
                {
                    MessageBox.Show("Data invalid!");
                    textamountto.Focus();
                }
            }
        }

        private void textProfitCenter_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textProfitCenter.Text))
            {
                checkAllpc.Checked = true;
            }
        }

        private void datagridrealizedprocess_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
           
        }

        private void datagridrealizedprocess_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = datagridrealizedprocess.CurrentRow.Index;
           
            
            
            if(datagridrealizedprocess.Rows[index].Cells["RealizedYesNo"].ReadOnly==false)
            {
            if (e.RowIndex != -1 && e.ColumnIndex == 8)
            {
                DataGridViewCheckBoxCell chkIsrealized = new DataGridViewCheckBoxCell();
                chkIsrealized = (DataGridViewCheckBoxCell)datagridrealizedprocess.Rows[datagridrealizedprocess.CurrentRow.Index].Cells["IsRealized"];
                if (datagridrealizedprocess.Rows[Convert.ToInt32(e.RowIndex)].Cells["RealizedYesNo"].Value.ToString() == "YES")
                {
                    datagridrealizedprocess.Rows[index].Cells["RealizedDate"].Value = DBNull.Value;
                    datagridrealizedprocess.Rows[index].Cells["RealizedYesNo"].Value = "NO";
                }
                else
                {
                    datagridrealizedprocess.Rows[index].Cells["RealizedDate"].Value = DateTime.Now.ToString("dd/MMM/yyyy");
                    datagridrealizedprocess.Rows[index].Cells["RealizedYesNo"].Value = "YES";
                    chkIsrealized.Value = true;
                }

            }
        }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            datagridrealizedprocess.DataSource = null;
        }

        private void datagridrealizedprocess_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
           
           if (datagridrealizedprocess.IsCurrentCellDirty)
            {
                datagridrealizedprocess.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void btnAdj_Click(object sender, EventArgs e)
        {
            if (cmbBankAcc.Text == "--Select--")
            {
                MessageBox.Show("Please select the account number", "Credit Card Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btn_srch_accno.Focus();
                return;
            }
            datagridrealizedprocess.Enabled = false;
            pnlAdj.Visible = true;
            txtPCAdj.Focus();
        }

        private void btnAddAdj_Click(object sender, EventArgs e)
        {
            string MID = "ALL";
            if (cmbmidno.Text == "--Select--" || string.IsNullOrEmpty(cmbmidno.Text))
            {
                MessageBox.Show("Please select the mid number", "Credit Card Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btn_srch_accno.Focus();
                return;
            }
            
            if (string.IsNullOrEmpty(txtAdjRef.Text))
            {
                MessageBox.Show("Please enter the reference #", "Credit Card Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAdjRef.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPCAdj.Text))
            {
                MessageBox.Show("Please select the profit center", "Credit Card Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPCAdj.Focus();
                return;
            }
           
            if (string.IsNullOrEmpty(txtAdjAmt.Text) || Convert.ToDecimal(txtAdjAmt.Text) < 0)
            {
                MessageBox.Show("Please enter the amount", "Credit Card Realization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, txtPCAdj.Text);
            if (_IsValid == false)
            {
                MessageBox.Show("Invalid Profit Center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPCAdj.Focus();
                return;
            }

            if (MessageBox.Show("Are you sure ?", "Credit Card Realization", MessageBoxButtons.YesNo) == DialogResult.No) return;
            MID = cmbmidno.Text;
            Decimal _adjAmt = 0;
            if (optDr.Checked == true)
                _adjAmt = Convert.ToDecimal(txtAdjAmt.Text);
            else
                _adjAmt = Convert.ToDecimal(txtAdjAmt.Text) * -1;

            DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dtDate.Value.Month + "/" + dtDate.Value.Year).Date;

            Decimal _wkNo = 0;
            int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(dtDate.Text).Date, out _wkNo, BaseCls.GlbUserComCode);

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
            DOC.Grdd_com = BaseCls.GlbUserComCode;
            DOC.Grdd_cre_by = BaseCls.GlbUserID;
            DOC.Grdd_doc_bank_cd = lblAccBankCd.Text;
            DOC.Grdd_deposit_bank = cmbBankAcc.Text;
            DOC.Grdd_doc_desc = "CREDIT CARD";
            DOC.Grdd_doc_tp = "CRCD";
            DOC.Grdd_pc = txtPCAdj.Text;

            Int32 _eff = CHNLSVC.Financial.SaveBankAdj(DOC, BaseCls.GlbUserComCode, txtPCAdj.Text, dtDate.Value.Date, cmbBankAcc.Text,"CRCD","CRCD", _adjAmt, txtAdjRef.Text, txtAdjRem.Text, BaseCls.GlbUserID, _tmpMonth, Convert.ToInt32(_wkNo), "", "",MID);
            if (_eff != 0)
            {
                DataTable dt = CHNLSVC.Financial.get_crcd_realization_det(BaseCls.GlbUserComCode, textProfitCenter.Text.Trim().ToUpper(), dtDate.Value.Date, cmbBankAcc.Text, null, 0, 0, 2, 0, 0, Convert.ToInt32(chkWithNIS.Checked), MID);
                datagridrealizedprocess.AutoGenerateColumns = false;
                datagridrealizedprocess.DataSource = dt;
                set_grid_data();
                //calc();

                //set_grid_color();
               // check_is_realize();

                load_adjgriddate();
                cal_credit_card_netAmount();
                clear_adj();

               

                MessageBox.Show("Successfully updated !", "Credit Card Realization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void clear_adj()
        {
            txtAdjAmt.Text = "";
            txtAdjRef.Text = "";
            txtAdjRem.Text = "";
        }

        private void load_adjgriddate()
        {
            Decimal _totAdj = 0;

            DataTable _DT = CHNLSVC.Financial.GetBankAdj(BaseCls.GlbUserComCode, dtDate.Value.Date, cmbBankAcc.Text);
            grvAdj.AutoGenerateColumns = false;
            grvAdj.DataSource = _DT;

            //foreach (DataGridViewRow row in grvAdj.Rows)
            //{
            //    if (row.Cells["bsta_adj_tp"].Value.ToString() == "CHEQUE")
            //        _totAdj = _totAdj + Convert.ToDecimal(row.Cells["bsta_amt"].Value);
            //}
            

           // Decimal _totCC = 0;
           // int X = CHNLSVC.Financial.GetCCRecTot(BaseCls.GlbUserComCode, dtDate.Value.Date, cmbBankAcc.Text, out _totCC);
            

        }

        private void set_grid_color()
        {
            
        }

        private void calc()
        {
            //Decimal _totRealPrv = 0;
            //Decimal _totReal = 0;

            //foreach (DataGridViewRow row in datagridrealizedprocess.Rows)
            //{
            //    DataGridViewCheckBoxCell chk = datagridrealizedprocess.Rows[row.Index].Cells[7] as DataGridViewCheckBoxCell;
            //    if (datagridrealizedprocess.Rows[Convert.ToInt32(row.Index)].Cells["BSTD_IS_REALIZED"].Value.ToString() == "YES")
            //    {
            //        if (datagridrealizedprocess.Rows[row.Index].Cells["BSTD_DOC_TP"].Value.ToString() != "BANK_CHG" && datagridrealizedprocess.Rows[row.Index].Cells["BSTD_DOC_TP"].Value.ToString() != "RTN_CHQ")
            //        {
            //            if (datagridrealizedprocess.Rows[row.Index].Cells["bstd_is_no_state"].Value.ToString() == "NO")
            //                _totReal = _totReal - Convert.ToDecimal(datagridrealizedprocess.Rows[row.Index].Cells["BSTD_DOC_VAL_CR"].Value) + Convert.ToDecimal(datagridrealizedprocess.Rows[row.Index].Cells["BSTD_DOC_VAL_DR"].Value);
            //        }
            //    }
            //}
            //txtTotReal.Text = _totReal.ToString("0.00");
        }

        private void btnCloseAdj_Click(object sender, EventArgs e)
        {
            pnlAdj.Visible = false;
            datagridrealizedprocess.Enabled = true;
        }

        private void cmbBankAcc_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            if (cmbBankAcc.Text != "--Select--")
            {
                string pc = "ALL";
                if (!string.IsNullOrEmpty(textProfitCenter.Text))
                {
                    pc = textProfitCenter.Text;
                }
                string AccNo = cmbBankAcc.Text;
                getBank();
                DataTable dtmidno = CHNLSVC.Sales.Load_MID_codes_perAcc(AccNo, pc);
                if (dtmidno.Rows.Count > 0)
                {
                    cmbmidno.DataSource = dtmidno;
                    cmbmidno.DisplayMember = "MSTM_MID";
                    cmbmidno.Text = "--Select--";
                    cmbmidno.Focus();
                }
                else
                {
                    cmbmidno.DataSource = null;
                }
            }
        }

        private void btnSrchPc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPCAdj;
                //_CommonSearch.txtSearchbyword.Text = txtPC.Text;
                _CommonSearch.ShowDialog();
                txtPCAdj.Focus();
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

        private void cmbmidno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CreditCardRealization_Load(object sender, EventArgs e)
        {
            bind_Combo_Accounts();
            cmbBankAcc.Focus();
        }

        private void datagridrealizedprocess_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {

                e.Handled = true;

            }
        }

        //private void datagridrealizedprocess_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        //{
        //    datagridrealizedprocess.Rows[e.RowIndex].ErrorText = "";
        //    int newInteger;

        //    // Don't try to validate the 'new row' until finished  
        //    // editing since there 
        //    // is not any point in validating its initial value. 
        //    if (datagridrealizedprocess.Rows[e.RowIndex].IsNewRow) { return; }
        //    if (!int.TryParse(e.FormattedValue.ToString(),
        //        out newInteger) || newInteger < 0)
        //    {
        //        e.Cancel = true;
        //        datagridrealizedprocess.Rows[e.RowIndex].ErrorText = "the value must be a non-negative integer";
        //    }
        //}
    }
}
