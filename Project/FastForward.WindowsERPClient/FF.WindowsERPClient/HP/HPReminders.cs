using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using FF.WindowsERPClient.HP;
using System.IO;
//using IWshRuntimeLibrary;

namespace FF.WindowsERPClient.HP
{
    public partial class HPReminders : Base
    {
        const string InvoiceBackDateName = "HPREMINDER";
        private List<HpAccount> accountsList;
        public List<HpAccount> AccountsList
        {
            get { return accountsList; }
            set { accountsList = value; }
        }
        static Int32 _SeqNo;

        public HPReminders()
        {
            InitializeComponent();
            InitializeValuesNDefaultValueSet();
        }

        private void InitializeValuesNDefaultValueSet()
        {
            BackDatePermission();
            gvView.AutoGenerateColumns = false;

            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Inactive");
            cmbStatus.Items.Add("Close");
            cmbStatus.Items.Add("All");

            ucHpAccountSummary1.Clear();
        }

        private void BackDatePermission()
        {
            
            //IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, InvoiceBackDateName, txtDate, lblBackDateInfor, string.Empty);
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "A" + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            try
            {
                string location = BaseCls.GlbUserDefProf;
                string acc_seq = txtAccNo.Text.Trim();
                List<HpAccount> accList = new List<HpAccount>();
                accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, acc_seq, "A");
                if (txtCustMob.Text != "" && txtCustMob.Text.Length < 10)
                {
                    MessageBox.Show("Invalid customer mobile no", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtManagerMob.Text != "" && txtManagerMob.Text.Length < 10)
                {
                    MessageBox.Show("Invalid manager mobile no", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtAccNo.Text != "")
                {
                    if (accList == null)
                    {
                        MessageBox.Show("Invalid account no", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                if (txtAccNo.Text == "")    //|| txtCustMob.Text == "" || txtManagerMob.Text == ""
                {
                    MessageBox.Show("Please enter account no, customer mobile or manager mobile", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (MessageBox.Show("Are you sure ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                HPReminder _rmd = new HPReminder();

                _rmd.Hra_ref = accList[0].Hpa_acc_no;
                _rmd.Hra_tp = "ACC";
                _rmd.Hra_stus = "A";
                _rmd.Hra_stus_dt = DateTime.Now;
                _rmd.Hra_cre_by = BaseCls.GlbUserID;
                _rmd.Hra_cre_dt = DateTime.Now;
                _rmd.Hra_mod_by = "";
                _rmd.Hra_dt = Convert.ToDateTime(txtRemindDate.Text);
                _rmd.Hra_rmd = txtMessage.Text;
                _rmd.Hra_cust_mob = txtCustMob.Text;
                _rmd.Hra_mgr_mob = txtManagerMob.Text;
                _rmd.Hra_pc = BaseCls.GlbUserDefProf;
                _rmd.Hra_com = BaseCls.GlbUserComCode;

                int result = CHNLSVC.Sales.SaveHPReminder(_rmd);

                if (result > 0)
                {
                    DataTable rmdList = CHNLSVC.Sales.GetReminders(accList[0].Hpa_acc_no, "A", BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "No_Date", DateTime.MinValue);
                    gvView.DataSource = rmdList;
                    MessageBox.Show("Record Insert Sucessfully!!", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAccNo.Text = "";
                    lblAccountNo.Text = "";
                    txtMessage.Text = "";
                    txtCustMob.Text = "";
                    txtManagerMob.Text = "";

                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void ImgBtnAccountNo_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAccNo;
                _CommonSearch.txtSearchbyword.Text = txtAccNo.Text;
                _CommonSearch.ShowDialog();
                txtAccNo.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                string _stus = null;
                string _acc = string.Empty;
                if (cmbStatus.Text != "ALL")
                {
                    switch (cmbStatus.Text)
                    {
                        case "Active":
                            {
                                _stus = "A";
                                break;
                            }
                        case "Inactive":
                            {
                                _stus = "I";
                                break;
                            }
                        case "Close":
                            {
                                _stus = "C";
                                break;
                            }
                        default:
                            break;
                    }

                }
                if (!string.IsNullOrEmpty(txtAccNo.Text)) { _acc = lblAccountNo.Text; }
                this.Cursor = Cursors.WaitCursor;
                DataTable rmdList = CHNLSVC.Sales.GetReminders(_acc, _stus, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "No_Date", DateTime.MinValue);
                gvView.DataSource = rmdList;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void chkView_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkView.Checked)
                {
                    if (string.IsNullOrEmpty(lblAccountNo.Text))
                    {
                        MessageBox.Show("Please enter account no", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        //ucHpAccountSummary1.Visible = true;
                        LoadAccountDetail(lblAccountNo.Text, CHNLSVC.Security.GetServerDateTime().Date);
                    }
                }
                else
                {
                    ucHpAccountSummary1.Clear();
                    //ucHpAccountSummary1.Visible = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtAccount()
        {
            try
            {
                lblAccountNo.Visible = true;
                lblAccountNo.Text = "";
                chkView.Checked = false;
                //lblAccountDate.Visible = true;

                //BindAccountItem(string.Empty);
                //BindSelectedItems(null);
                //BindCustomerDetails(null);
                //_selectedItemList = new List<ReptPickSerials>();


                if (string.IsNullOrEmpty(BaseCls.GlbUserDefProf))
                {
                    MessageBox.Show("Please select the valid profit center", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string location = BaseCls.GlbUserDefProf;
                string acc_seq = "0";
                if (!string.IsNullOrEmpty(txtAccNo.Text))
                {
                    acc_seq = txtAccNo.Text.Trim();
                }

                List<HpAccount> accList = new List<HpAccount>();
                accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, acc_seq, "A");
                AccountsList = accList;//save in veiw state
                if (accList == null)
                {
                    //MessageBox.Show("Enter valid Account number!", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAccNo.Text = null;
                    return;
                }
                if (accList.Count == 0)
                {
                    //MessageBox.Show("Enter valid Account number!", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAccNo.Text = null;
                    return;
                }
                else if (accList.Count == 1)
                {
                    //show summury
                    foreach (HpAccount ac in accList)
                    {
                        LoadAccountDetail(ac.Hpa_acc_no, CHNLSVC.Security.GetServerDateTime().Date);
                    }
                }
                else if (accList.Count > 1)
                {
                    //show a pop up to select the account number
                    //grvMpdalPopUp.DataSource = accList;
                    //grvMpdalPopUp.DataBind();
                    //ModalPopupExtItem.Show();
                    //HpCollection_ECDReq f2 = new HpCollection_ECDReq(this);
                    //f2.visible_panel_accountSelect(true);
                    //f2.visible_panel_ReqApp(false);
                    //f2.fill_AccountGrid(accList);
                    //f2.ShowDialog();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        public void LoadAccountDetail(string _account, DateTime _date)
        {
            try
            {
                lblAccountNo.Text = _account;
                this.Cursor = Cursors.WaitCursor;
                //show acc balance.
                Decimal accBalance = CHNLSVC.Sales.Get_AccountBalance(CHNLSVC.Security.GetServerDateTime().Date, _account);
                //lblACC_BAL.Text = accBalance.ToString();

                // set UC values.
                if (AccountsList != null)
                {
                    HpAccount account = new HpAccount();
                    foreach (HpAccount acc in AccountsList)
                    {
                        if (_account == acc.Hpa_acc_no)
                        {
                            account = acc;
                        }
                    }


                    if (chkView.Checked)
                        ucHpAccountSummary1.set_all_values(account, BaseCls.GlbUserDefProf, _date.Date, BaseCls.GlbUserDefProf);
                    //BindAccountItem(account.Hpa_acc_no);
                    //btnProcess.Enabled = true;

                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtAccNo_Leave(object sender, EventArgs e)
        {
            try
            {
                txtAccount();
                ucHpAccountSummary1.Clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void gvView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int _rowIndex = e.RowIndex;
            _SeqNo = Convert.ToInt32(gvView.Rows[_rowIndex].Cells["hra_seq"].Value);

        }

        private void btnInactive_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_SeqNo.ToString()) || _SeqNo == 0)
                {
                    MessageBox.Show("Select account number !", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Are you sure ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                HPReminder _rmd = new HPReminder();
                _rmd.Hra_seq = _SeqNo;
                _rmd.Hra_stus = "I";
                _rmd.Hra_stus_dt = DateTime.Now;
                _rmd.Hra_mod_by = BaseCls.GlbUserID;

                //update process
                int result = CHNLSVC.Sales.UpdateHPReminder(_rmd);
                if (result > 0)
                {
                    MessageBox.Show("Record Updated Sucessfully!!", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SeqNo = 0;
                    btnView_Click(null, null);
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtAccNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRemindDate.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                ImgBtnAccountNo_Click(null, null);
            }
        }

        private void txtRemindDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //txtCustMob.Focus();
            }
        }

        private void txtCustMob_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtManagerMob.Focus();
            }
        }

        private void txtManagerMob_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMessage.Focus();
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            ucHpAccountSummary1.Clear();
            txtAccNo.Text = "";
            txtCustMob.Text = "";
            txtManagerMob.Text = "";
            txtMessage.Text = "";
            gvView.DataSource = null;
        }





    }
}
