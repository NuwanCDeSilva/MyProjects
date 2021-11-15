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
    public partial class DisqulifiedBonus : Base
    {
        #region properties

        List<DisregardValueDefinition> ListDisregard;
        List<DisregardPCDefinition> ListPC;

        List<DisregardValueDefinition> ListViewDisVal;
        List<DisregardPCDefinition> ListViewDisPC;
        int PCEditIndex;
        int ValueEditIndex;

        #endregion

        public DisqulifiedBonus()
        {
            InitializeComponent();
            ListDisregard = new List<DisregardValueDefinition>();
            ListPC = new List<DisregardPCDefinition>();
            ListViewDisPC = new List<DisregardPCDefinition>();
            ListViewDisVal = new List<DisregardValueDefinition>();
            ValueEditIndex = -1;
            PCEditIndex = -1;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { 
            this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                //save degrade value
                int result = 0;
                int result1 = 0;
                foreach (DisregardValueDefinition _dis in ListDisregard)
                {
                    result = result + CHNLSVC.Sales.SaveDisregardValueDefinition(_dis);
                }
                foreach (DisregardPCDefinition _pc in ListPC)
                {
                    result1 = result1 + CHNLSVC.Sales.SaveDisregardPCDefinition(_pc);
                }
                if (result > 0 && result1 > 0)
                {
                    MessageBox.Show("Disregard Value insert Successfully\nDisregard Profit Center insert Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (result > 0)
                {
                    MessageBox.Show("Disregard Value insert Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (result1 > 0)
                {
                    MessageBox.Show("Disregard Profit Center insert Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Nothing Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ClearAll();
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
                if (ValueEditIndex != -1)
                {
                    if (ValidateValueAdd())
                    {
                        return;
                    }
                    else
                    {
                        DisregardValueDefinition tem = new DisregardValueDefinition();
                        tem.Hdvr_from_val = Convert.ToDecimal(txtValueFrom.Text);
                        tem.Hdvr_to_val = Convert.ToDecimal(txtValueTo.Text);
                        tem.Hdvr_val = Convert.ToDecimal(txtPresentage.Text);
                        tem.Hdvr_mod_by = BaseCls.GlbUserID;
                        tem.Hdvr_mod_dt = DateTime.Now;
                        tem.Hdvr_tp = true;

                        int result = CHNLSVC.Sales.UpdateDisregardValueDefinition(tem, ListViewDisVal[ValueEditIndex].Hdvr_from_val, ListViewDisVal[ValueEditIndex].Hdvr_to_val, ListViewDisVal[ValueEditIndex].Hdvr_val, 1);
                        if (result <= 0)
                        {
                            ListDisregard.First(x => x.Hdvr_from_val == ListViewDisVal[ValueEditIndex].Hdvr_from_val && x.Hdvr_to_val == ListViewDisVal[ValueEditIndex].Hdvr_to_val && x.Hdvr_val == ListViewDisVal[ValueEditIndex].Hdvr_val).Hdvr_from_val = Convert.ToDecimal(txtValueFrom.Text);
                            ListDisregard.First(x => x.Hdvr_from_val == ListViewDisVal[ValueEditIndex].Hdvr_from_val && x.Hdvr_to_val == ListViewDisVal[ValueEditIndex].Hdvr_to_val && x.Hdvr_val == ListViewDisVal[ValueEditIndex].Hdvr_val).Hdvr_from_val = Convert.ToDecimal(txtValueTo.Text);
                            ListDisregard.First(x => x.Hdvr_from_val == ListViewDisVal[ValueEditIndex].Hdvr_from_val && x.Hdvr_to_val == ListViewDisVal[ValueEditIndex].Hdvr_to_val && x.Hdvr_val == ListViewDisVal[ValueEditIndex].Hdvr_val).Hdvr_from_val = Convert.ToDecimal(txtPresentage.Text);

                            ListViewDisVal[ValueEditIndex].Hdvr_from_val = Convert.ToDecimal(txtValueFrom.Text);
                            ListViewDisVal[ValueEditIndex].Hdvr_to_val = Convert.ToDecimal(txtValueTo.Text);
                            ListViewDisVal[ValueEditIndex].Hdvr_val = Convert.ToDecimal(txtPresentage.Text);
                            LoadGrid(ListViewDisVal);
                        }
                        else
                        {
                            ListViewDisVal = new List<DisregardValueDefinition>();
                            List<DisregardValueDefinition> temp = CHNLSVC.Sales.GetDisregardValueDefinitiom(0, 0, 0, 1, "ALL");
                            if (temp != null)
                            {
                                ListViewDisVal.AddRange(temp);
                            }
                            LoadGrid(ListViewDisVal);
                        }
                    }
                }
                if (PCEditIndex != -1)
                {
                    if (txtLocation.Text == "")
                    {
                        MessageBox.Show("Please select Profit Center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        DisregardPCDefinition tem = new DisregardPCDefinition();
                        tem.Hdpd_pc = txtLocation.Text;
                        tem.Hdpd_from_dt = new DateTime(dateTimePickerMonthFrom.Value.Year, dateTimePickerMonthFrom.Value.Month, DateTime.DaysInMonth(dateTimePickerMonthFrom.Value.Year, dateTimePickerMonthFrom.Value.Month));
                        tem.Hdpd_to_dt = new DateTime(dateTimePickerMonthTo.Value.Year, dateTimePickerMonthTo.Value.Month, DateTime.DaysInMonth(dateTimePickerMonthTo.Value.Year, dateTimePickerMonthTo.Value.Month));
                        tem.Hdpd_mod_by = BaseCls.GlbUserID;
                        tem.Hdpd_mod_dt = DateTime.Now;
                        tem.Hdpd_com = BaseCls.GlbUserComCode;

                        int result = CHNLSVC.Sales.UpdateDisregardPCDefinition(tem, ListViewDisPC[PCEditIndex].Hdpd_com, ListViewDisPC[PCEditIndex].Hdpd_pc);
                        if (result <= 0)
                        {
                            ListPC.First(x => x.Hdpd_pc == ListViewDisPC[PCEditIndex].Hdpd_pc && x.Hdpd_from_dt == ListViewDisPC[PCEditIndex].Hdpd_from_dt && x.Hdpd_to_dt == ListViewDisPC[PCEditIndex].Hdpd_to_dt && x.Hdpd_com == ListViewDisPC[PCEditIndex].Hdpd_com).Hdpd_pc = txtLocation.Text;
                            ListPC.First(x => x.Hdpd_pc == ListViewDisPC[PCEditIndex].Hdpd_pc && x.Hdpd_from_dt == ListViewDisPC[PCEditIndex].Hdpd_from_dt && x.Hdpd_to_dt == ListViewDisPC[PCEditIndex].Hdpd_to_dt && x.Hdpd_com == ListViewDisPC[PCEditIndex].Hdpd_com).Hdpd_from_dt = new DateTime(dateTimePickerMonthFrom.Value.Year, dateTimePickerMonthFrom.Value.Month, DateTime.DaysInMonth(dateTimePickerMonthFrom.Value.Year, dateTimePickerMonthFrom.Value.Month));
                            ListPC.First(x => x.Hdpd_pc == ListViewDisPC[PCEditIndex].Hdpd_pc && x.Hdpd_from_dt == ListViewDisPC[PCEditIndex].Hdpd_from_dt && x.Hdpd_to_dt == ListViewDisPC[PCEditIndex].Hdpd_to_dt && x.Hdpd_com == ListViewDisPC[PCEditIndex].Hdpd_com).Hdpd_to_dt = new DateTime(dateTimePickerMonthTo.Value.Year, dateTimePickerMonthTo.Value.Month, DateTime.DaysInMonth(dateTimePickerMonthTo.Value.Year, dateTimePickerMonthTo.Value.Month));

                            ListViewDisPC[PCEditIndex].Hdpd_pc = txtLocation.Text;
                            ListViewDisPC[PCEditIndex].Hdpd_from_dt = new DateTime(dateTimePickerMonthFrom.Value.Year, dateTimePickerMonthFrom.Value.Month, DateTime.DaysInMonth(dateTimePickerMonthFrom.Value.Year, dateTimePickerMonthFrom.Value.Month));
                            ListViewDisPC[PCEditIndex].Hdpd_to_dt = new DateTime(dateTimePickerMonthTo.Value.Year, dateTimePickerMonthTo.Value.Month, DateTime.DaysInMonth(dateTimePickerMonthTo.Value.Year, dateTimePickerMonthTo.Value.Month));
                            LoadGridPC(ListViewDisPC);
                        }
                        else
                        {
                            ListViewDisPC = new List<DisregardPCDefinition>();
                            List<DisregardPCDefinition> temp = CHNLSVC.Sales.GetDisregardPCDefinitiom("", "", "ALL");
                            if (temp != null)
                            {
                                ListViewDisPC.AddRange(temp);
                            }
                            LoadGridPC(ListViewDisPC);
                        }
                    }
                }
                btnEdit.Visible = false;
                btnSave.Enabled = true;
            }
            catch (Exception ex) {
                MessageBox.Show("Error occured while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisqulifiedBonus_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewPC.AutoGenerateColumns = false;
                dataGridViewDigradeValue.AutoGenerateColumns = false;
                dateTimePickerMonthFrom.Format = DateTimePickerFormat.Custom;
                dateTimePickerMonthFrom.CustomFormat = "MMM/yyyy";
                dateTimePickerMonthTo.Format = DateTimePickerFormat.Custom;
                dateTimePickerMonthTo.CustomFormat = "MMM/yyyy";

                List<DisregardValueDefinition> temp = CHNLSVC.Sales.GetDisregardValueDefinitiom(0, 0, 0, 1, "ALL");
                if (temp != null)
                {
                    ListViewDisVal.AddRange(temp);
                }
                LoadGrid(ListViewDisVal);

                List<DisregardPCDefinition> tempPc = CHNLSVC.Sales.GetDisregardPCDefinitiom("", "", "ALL");
                if (tempPc != null)
                {
                    ListViewDisPC.AddRange(tempPc);
                }
                LoadGridPC(ListViewDisPC);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
        }

        private void LoadGrid(List<DisregardValueDefinition> List)
        {
            dataGridViewDigradeValue.AutoGenerateColumns = false;
            var source = new BindingSource();
            source.DataSource = List;
            dataGridViewDigradeValue.DataSource = source;
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
            }
            return paramsText.ToString();
        }

        private void ClearAll() {
            txtLocation.Text = "";
            txtPresentage.Text = "";
            txtValueFrom.Text = "";
            txtValueTo.Text = "";

            btnEdit.Visible = false;

            lblLocation.Text = "";

            btnEdit.Visible = false;
            btnSave.Enabled = true;

            dateTimePickerMonthFrom.Value = DateTime.Now;
            dateTimePickerMonthTo.Value = DateTime.Now;

            ListDisregard = new List<DisregardValueDefinition>();
            ListPC = new List<DisregardPCDefinition>();
            ListViewDisPC = new List<DisregardPCDefinition>();
            ListViewDisVal = new List<DisregardValueDefinition>();

            List<DisregardValueDefinition> temp = CHNLSVC.Sales.GetDisregardValueDefinitiom(0, 0, 0, 1, "ALL");
            if (temp != null)
            {
                ListViewDisVal.AddRange(temp);
            }
            LoadGrid(ListViewDisVal);

            List<DisregardPCDefinition> tempPc = CHNLSVC.Sales.GetDisregardPCDefinitiom("", "", "ALL");
            if (tempPc != null)
            {
                ListViewDisPC.AddRange(tempPc);
            }
            LoadGridPC(ListViewDisPC);
            ValueEditIndex = -1;
            PCEditIndex = -1;
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


        private bool ValidateProfitCenter(string code)
        {

            MasterProfitCenter pc = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, code);
            if (pc != null)
            {
                lblLocation.Text = pc.Mpc_desc;
                return true;
            }
            else
                return false;
        }

        private void txtValueFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtValueTo.Focus();
            }
        }

        private void txtValueTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtPresentage.Focus();
            }
        }

        private void txtPresentage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                buttonAdd.Focus();
            }
        }

        private void txtLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                dateTimePickerMonthFrom.Focus();
            }
            if (e.KeyCode == Keys.F2) {
                buttonSearchLocation_Click(null, null);
            }
        }

        private void dateTimePickerMonthFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                dateTimePickerMonthTo.Focus();
            }
        }

        private void dateTimePickerMonthTo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter){
                buttonAdd1.Focus();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (ValidateValueAdd())
            {
                DateTime _date = CHNLSVC.Security.GetServerDateTime();

                DisregardValueDefinition _dis = new DisregardValueDefinition();
                _dis.Hdvr_from_val = Convert.ToDecimal(txtValueFrom.Text);
                _dis.Hdvr_to_val = Convert.ToDecimal(txtValueTo.Text);
                _dis.Hdvr_val = Convert.ToDecimal(txtPresentage.Text);
                _dis.Hdvr_cre_by = BaseCls.GlbUserID;
                _dis.Hdvr_cre_dt = _date;
                _dis.Hdvr_mod_by = BaseCls.GlbUserID;
                _dis.Hdvr_mod_dt = _date;
                _dis.Hdvr_tp = true;

                ListDisregard.Add(_dis);
                ListViewDisVal.Add(_dis);
                LoadGrid(ListViewDisVal);

                txtValueFrom.Text = "";
                txtValueTo.Text = "";
                txtPresentage.Text = "";
            }
        }

        private bool ValidateValueAdd()
        {
            decimal value;
            if (txtValueFrom.Text == "")
            {
                MessageBox.Show("Please enter From value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValueFrom.Focus();
                return false;
            }
            if (txtValueTo.Text == "")
            {
                MessageBox.Show("Please enter To Value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValueTo.Focus();
                return false;
            }
            if (txtPresentage.Text == "")
            {
                MessageBox.Show("Please enter Precentage", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPresentage.Focus();
                return false; 
            }
            if (!decimal.TryParse(txtValueFrom.Text, out value))
            {
                MessageBox.Show("From value has to be a Number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValueFrom.Focus();
                return false;
            }
            if (!decimal.TryParse(txtValueTo.Text, out value))
            {
                MessageBox.Show("To value has to be a Number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValueTo.Focus();
                return false;
            }
            if (!decimal.TryParse(txtPresentage.Text, out value))
            {
                MessageBox.Show("Precentage has to be a Number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPresentage.Focus();
                return false;
            }
            if (Convert.ToDecimal(txtValueFrom.Text) > Convert.ToDecimal(txtValueTo.Text)) {
                MessageBox.Show("To value has to be greater than To value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValueFrom.Focus();
                return false;
            }
            return true;
        }

        private void dataGridViewDigradeValue_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                txtValueFrom.Text = dataGridViewDigradeValue.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtValueTo.Text = dataGridViewDigradeValue.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtPresentage.Text = dataGridViewDigradeValue.Rows[e.RowIndex].Cells[4].Value.ToString();
                btnEdit.Visible = true;
                btnSave.Enabled = false;
                ValueEditIndex = e.RowIndex;
                PCEditIndex = -1;
            }
            if (e.RowIndex != -1 && e.ColumnIndex == 1)
            {
                if (MessageBox.Show("Are you Sure ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    decimal fromVal = Convert.ToDecimal(dataGridViewDigradeValue.Rows[e.RowIndex].Cells[2].Value);
                    decimal toVal = Convert.ToDecimal(dataGridViewDigradeValue.Rows[e.RowIndex].Cells[3].Value);
                    decimal precentage = Convert.ToDecimal(dataGridViewDigradeValue.Rows[e.RowIndex].Cells[4].Value);

                    List<FF.BusinessObjects.DisregardValueDefinition> result = (from _res in ListDisregard
                                                                                where _res.Hdvr_from_val == fromVal && _res.Hdvr_to_val == toVal
                                                                                && _res.Hdvr_val == precentage
                                                                                select _res).ToList<DisregardValueDefinition>();
                    if (result != null && result.Count > 0)
                    {
                        ListDisregard.Remove(result[0]);
                        ListViewDisVal.Remove(result[0]);
                        LoadGrid(ListViewDisVal);
                    }
                    else
                    {
                        MessageBox.Show("Can not delete saved to database permanently", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void buttonAdd1_Click(object sender, EventArgs e)
        {
            if (txtLocation.Text == "") {
                MessageBox.Show("Please select Profit Center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime _date = CHNLSVC.Security.GetServerDateTime();

            DisregardPCDefinition _pc = new DisregardPCDefinition();
            _pc.Hdpd_pc = txtLocation.Text;
            _pc.Hdpd_com = BaseCls.GlbUserComCode;
            _pc.Hdpd_from_dt =new DateTime(dateTimePickerMonthFrom.Value.Year,dateTimePickerMonthFrom.Value.Month,DateTime.DaysInMonth(dateTimePickerMonthFrom.Value.Year,dateTimePickerMonthFrom.Value.Month));
            _pc.Hdpd_to_dt = new DateTime(dateTimePickerMonthTo.Value.Year, dateTimePickerMonthTo.Value.Month, DateTime.DaysInMonth(dateTimePickerMonthTo.Value.Year, dateTimePickerMonthTo.Value.Month));
            _pc.Hdpd_cre_by = BaseCls.GlbUserID;
            _pc.Hdpd_cre_dt = _date;
            _pc.Hdpd_mod_by = BaseCls.GlbUserID;
            _pc.Hdpd_mod_dt = _date;

            ListPC.Add(_pc);
            ListViewDisPC.Add(_pc);

            LoadGridPC(ListViewDisPC);

            txtLocation.Text = "";
            dateTimePickerMonthFrom.Value = DateTime.Now;
            dateTimePickerMonthTo.Value = DateTime.Now;
        }

        private void LoadGridPC(List<DisregardPCDefinition> list)
        {
            dataGridViewPC.AutoGenerateColumns = false;
            var source = new BindingSource();
            source.DataSource = list;
            dataGridViewPC.DataSource = source;
        }

        private void dataGridViewPC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                txtLocation.Text = dataGridViewPC.Rows[e.RowIndex].Cells[3].Value.ToString();
                dateTimePickerMonthFrom.Value = Convert.ToDateTime(dataGridViewPC.Rows[e.RowIndex].Cells[4].Value);
                dateTimePickerMonthTo.Value = Convert.ToDateTime(dataGridViewPC.Rows[e.RowIndex].Cells[5].Value);
                btnEdit.Visible = true;
                btnSave.Enabled = false;
                ValueEditIndex = -1;
                PCEditIndex = e.RowIndex;
            }
            if (e.RowIndex != -1 && e.ColumnIndex == 1)
            {
                if (MessageBox.Show("Are you sure ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string pc = dataGridViewPC.Rows[e.RowIndex].Cells[3].Value.ToString();
                    DateTime fromMonth = Convert.ToDateTime(dataGridViewPC.Rows[e.RowIndex].Cells[4].Value);
                    DateTime toMonth = Convert.ToDateTime(dataGridViewPC.Rows[e.RowIndex].Cells[5].Value);

                    List<DisregardPCDefinition> result = (from _res in ListPC
                                                          where _res.Hdpd_pc == pc && _res.Hdpd_com == BaseCls.GlbUserComCode && _res.Hdpd_from_dt == fromMonth
                                                          && _res.Hdpd_to_dt == toMonth
                                                          select _res).ToList<DisregardPCDefinition>();
                    if (result != null && result.Count > 0)
                    {
                        ListPC.Remove(result[0]);
                        ListViewDisPC.Remove(result[0]);
                        LoadGridPC(ListViewDisPC);
                    }
                    else
                    {
                        MessageBox.Show("Can not delete saved to database permanently", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void txtLocation_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSearchLocation_Click(null, null);
        }
    }
}
