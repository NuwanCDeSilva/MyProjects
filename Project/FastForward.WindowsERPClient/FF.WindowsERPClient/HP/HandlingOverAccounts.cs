using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FF.BusinessObjects;
using FF.Interfaces;

namespace FF.WindowsERPClient.HP
{
    public partial class HandlingOverAccounts : Base
    {
        #region properties

        List<FF.BusinessObjects.HandlingOverAccount> ListHandlinOverAccounts;
        List<FF.BusinessObjects.HandlingOverAccount> ListViewHandling;
        int EditIndex;

        #endregion


        public HandlingOverAccounts()
        {
            InitializeComponent();
            ListHandlinOverAccounts = new List<HandlingOverAccount>();
            ListViewHandling = new List<HandlingOverAccount>();
            EditIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                this.Close();
            }
        }

        private void dateTimePickerDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                dateTimePickerBonusMonth.Focus();
            }
        }

        private void dateTimePickerBonusMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtLocation.Focus();
            }
        }

        private void txtLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtAccountNo.Focus();
            }
            if (e.KeyCode == Keys.F2) {
                buttonSearchLocation_Click(null, null);
            }
        }

        private void txtAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                btnAdd.Focus();
            }
        }

        private void buttonSearchLocation_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtLocation;
            _CommonSearch.txtSearchbyword.Text = txtLocation.Text;
            _CommonSearch.ShowDialog();
            txtLocation.Select();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtLocation.Text + seperator + "A" + seperator);
                        break;
                    }
            }
            return paramsText.ToString();
        }

        private void ClearAll()
        {
            txtAccountNo.Text = "";
            txtLocation.Text = "";

            lblLocation.Text = "";

            dateTimePickerBonusMonth.Value = DateTime.Now;
            dateTimePickerDate.Value = DateTime.Now;

            ListHandlinOverAccounts = new List<HandlingOverAccount>();
            ListViewHandling = new List<HandlingOverAccount>();

            List<FF.BusinessObjects.HandlingOverAccount> temp = CHNLSVC.Sales.GetHandlingOverAccounts("", "", "", DateTime.Now, "ALL");
            if (temp != null)
                ListViewHandling.AddRange(temp);

            LoadGrid(ListViewHandling);

            btnSave.Enabled = true;
            btnEdit.Visible = false;
        }

        private void txtLocation_Leave(object sender, EventArgs e)
        {
            if (txtLocation.Text != "")
            {
                if (!ValidateProfitCenter(txtLocation.Text)) {
                    MessageBox.Show("Invalid Profit Center code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLocation.Text = "";
                    txtLocation.Focus();
                }
            }
        }

        private void HandlingOverAccounts_Load(object sender, EventArgs e)
        {
            dataGridViewDetails.AutoGenerateColumns = false;
            dateTimePickerBonusMonth.Format = DateTimePickerFormat.Custom;
            dateTimePickerBonusMonth.CustomFormat = "MMM/yyyy";

            List<FF.BusinessObjects.HandlingOverAccount> temp = CHNLSVC.Sales.GetHandlingOverAccounts("", "", "", DateTime.Now, "ALL");
            if(temp!=null)
            ListViewHandling.AddRange(temp);

            LoadGrid(ListViewHandling);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateSave())
                {
                    DateTime _date = CHNLSVC.Security.GetServerDateTime();
                    FF.BusinessObjects.HandlingOverAccount _hand = new HandlingOverAccount();
                    _hand.Hhoa_com = BaseCls.GlbUserComCode;
                    _hand.Hhoa_pc = txtLocation.Text;
                    _hand.Hhoa_bonus_month = new DateTime(dateTimePickerBonusMonth.Value.Year, dateTimePickerBonusMonth.Value.Month, DateTime.DaysInMonth(dateTimePickerBonusMonth.Value.Year, dateTimePickerBonusMonth.Value.Month));
                    _hand.Hhoa_cre_by = BaseCls.GlbUserID;
                    _hand.Hhoa_ac = txtAccountNo.Text;
                    _hand.Hhoa_cre_dt = _date;

                    ListHandlinOverAccounts.Add(_hand);
                    ListViewHandling.Add(_hand);
                    LoadGrid(ListViewHandling);
                    txtLocation.Text = "";
                    lblLocation.Text = "";
                    txtAccountNo.Text = "";
                    dateTimePickerBonusMonth.Value = _date;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
        }

        private bool ValidateSave()
        {
            if (txtLocation.Text == "")
            {
                MessageBox.Show("Please select Profit Center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLocation.Focus();
                return false;
            }
            if (txtAccountNo.Text == "")
            {
                MessageBox.Show("Please select Account Number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccountNo.Focus();
                return false;
            }
            FF.BusinessObjects.HandlingOverAccount hand = ListViewHandling.Find(x => x.Hhoa_ac == txtAccountNo.Text && x.Hhoa_bonus_month == new DateTime(dateTimePickerBonusMonth.Value.Year, dateTimePickerBonusMonth.Value.Month, DateTime.DaysInMonth(dateTimePickerBonusMonth.Value.Year, dateTimePickerBonusMonth.Value.Month)));
            if (hand != null) {
                MessageBox.Show("Account and Bonus Month can not duplicated", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void buttonSearchAccountNo_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
            DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAccountNo;
            _CommonSearch.ShowDialog();
            txtAccountNo.Select();
        }

        private void LoadGrid(List<FF.BusinessObjects.HandlingOverAccount> list) {
            dataGridViewDetails.AutoGenerateColumns = false;
            var source = new BindingSource();
            source.DataSource = list;
            dataGridViewDetails.DataSource = source;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                foreach (HandlingOverAccount hand in ListHandlinOverAccounts)
                {
                    result = result + CHNLSVC.Sales.SaveHandlingOveAccounts(hand);
                }
                if (result > 0)
                {
                    MessageBox.Show("Successfully Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearAll();
                }
                else
                {
                    MessageBox.Show("Nothing Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (EditIndex != -1 && ValidateSave())
                {

                    FF.BusinessObjects.HandlingOverAccount _hand = new HandlingOverAccount();
                    _hand.Hhoa_com = BaseCls.GlbUserComCode;
                    _hand.Hhoa_pc = txtLocation.Text;
                    _hand.Hhoa_ac = txtAccountNo.Text;
                    _hand.Hhoa_bonus_month = new DateTime(dateTimePickerBonusMonth.Value.Year, dateTimePickerBonusMonth.Value.Month, DateTime.DaysInMonth(dateTimePickerBonusMonth.Value.Year, dateTimePickerBonusMonth.Value.Month));

                    int result = CHNLSVC.Sales.UpdateHandlinOverAccount(_hand, ListViewHandling[EditIndex].Hhoa_com, ListViewHandling[EditIndex].Hhoa_pc, ListViewHandling[EditIndex].Hhoa_ac);
                    if (result <= 0)
                    {
                        ListHandlinOverAccounts.First(x => x.Hhoa_com == ListViewHandling[EditIndex].Hhoa_com && x.Hhoa_pc == ListViewHandling[EditIndex].Hhoa_pc && x.Hhoa_ac == ListViewHandling[EditIndex].Hhoa_ac).Hhoa_pc = txtLocation.Text;
                        ListHandlinOverAccounts.First(x => x.Hhoa_com == ListViewHandling[EditIndex].Hhoa_com && x.Hhoa_pc == ListViewHandling[EditIndex].Hhoa_pc && x.Hhoa_ac == ListViewHandling[EditIndex].Hhoa_ac).Hhoa_ac = txtAccountNo.Text;
                        ListHandlinOverAccounts.First(x => x.Hhoa_com == ListViewHandling[EditIndex].Hhoa_com && x.Hhoa_pc == ListViewHandling[EditIndex].Hhoa_pc && x.Hhoa_ac == ListViewHandling[EditIndex].Hhoa_ac).Hhoa_bonus_month = new DateTime(dateTimePickerBonusMonth.Value.Year, dateTimePickerBonusMonth.Value.Month, DateTime.DaysInMonth(dateTimePickerBonusMonth.Value.Year, dateTimePickerBonusMonth.Value.Month)); ;

                        ListViewHandling[EditIndex].Hhoa_pc = txtLocation.Text;
                        ListViewHandling[EditIndex].Hhoa_ac = txtAccountNo.Text;
                        ListViewHandling[EditIndex].Hhoa_bonus_month = new DateTime(dateTimePickerBonusMonth.Value.Year, dateTimePickerBonusMonth.Value.Month, DateTime.DaysInMonth(dateTimePickerBonusMonth.Value.Year, dateTimePickerBonusMonth.Value.Month)); ;
                        LoadGrid(ListViewHandling);
                    }
                    else
                    {
                        ListViewHandling = new List<HandlingOverAccount>();
                        List<HandlingOverAccount> temp = CHNLSVC.Sales.GetHandlingOverAccounts("", "", "", DateTime.MinValue, "ALL");
                        if (temp != null)
                        {
                            ListViewHandling.AddRange(temp);
                        }
                        LoadGrid(ListViewHandling);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
            btnEdit.Visible = false;
            btnSave.Enabled = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0) {

                dateTimePickerBonusMonth.Value = Convert.ToDateTime(dataGridViewDetails.Rows[e.RowIndex].Cells[5].Value);
                txtAccountNo.Text = dataGridViewDetails.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtLocation.Text = dataGridViewDetails.Rows[e.RowIndex].Cells[3].Value.ToString();
                EditIndex = e.RowIndex;
                btnSave.Enabled = false;
                btnEdit.Visible = true;
            }
            if (e.RowIndex != -1 && e.ColumnIndex == 1)
            {
                                DateTime month = Convert.ToDateTime(dataGridViewDetails.Rows[e.RowIndex].Cells[5].Value);
                string acc= dataGridViewDetails.Rows[e.RowIndex].Cells[4].Value.ToString();
                string pc = dataGridViewDetails.Rows[e.RowIndex].Cells[3].Value.ToString();


                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    List<HandlingOverAccount> temp = (from _res in ListHandlinOverAccounts
                                                      where _res.Hhoa_pc == pc && _res.Hhoa_ac == acc && _res.Hhoa_bonus_month == month
                                                      select _res).ToList<HandlingOverAccount>();
                    if (temp != null && temp.Count > 0)
                    {
                        ListHandlinOverAccounts.Remove(temp[0]);
                        ListViewHandling.Remove(temp[0]);
                        LoadGrid(ListViewHandling);
                    }
                    else
                    {
                        MessageBox.Show("Can not delete saved to database permanently", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private bool ValidateAccountNo(string account)
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            DataTable dt = CHNLSVC.Sales.GetHp_ActiveAccounts(BaseCls.GlbUserComCode, txtLocation.Text, _date.Date, _date.Date, _date.Date, txtAccountNo.Text, "A");
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidateProfitCenter(string code) {

            MasterProfitCenter pc = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode,code);
            if (pc != null)
            {
                lblLocation.Text = pc.Mpc_desc;
                return true;
            }
            else
                return false;
        }

        private void txtAccountNo_Leave(object sender, EventArgs e)
        {
            if (txtAccountNo.Text != "") {
                if (!ValidateAccountNo(txtAccountNo.Text)) {
                    MessageBox.Show("Invalid account No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAccountNo.Focus();
                    txtAccountNo.Text = "";
                }
            }
        }

    }
}
