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
    public partial class DisregardAmount : Base
    {

        List<DisregardValueDefinition> ListDisregard;
        List<DisregardValueDefinition> ListViewDisVal;

        int EditIndex;


        public DisregardAmount()
        {
            InitializeComponent();
            EditIndex = -1;
            ListDisregard = new List<DisregardValueDefinition>();
            ListViewDisVal = new List<DisregardValueDefinition>();
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
                int result = 0;
                foreach (DisregardValueDefinition _dis in ListDisregard)
                {
                    result = result + CHNLSVC.Sales.SaveDisregardValueDefinition(_dis);
                }
                if (result > 0)
                {
                    MessageBox.Show("Disregard Value insert Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                    MessageBox.Show("Nothing Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                ClearAll();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (EditIndex != -1)
            {
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                DisregardValueDefinition tem = new DisregardValueDefinition();
                tem.Hdvr_from_val = Convert.ToDecimal(txtAmountFrom.Text);
                tem.Hdvr_to_val = Convert.ToDecimal(txtAmountTo.Text);
                tem.Hdvr_val = Convert.ToDecimal(txtRate.Text);
                tem.Hdvr_mod_by = BaseCls.GlbUserID;
                tem.Hdvr_mod_dt = _date;
                tem.Hdvr_tp = false;

                int result = CHNLSVC.Sales.UpdateDisregardValueDefinition(tem, ListViewDisVal[EditIndex].Hdvr_from_val, ListViewDisVal[EditIndex].Hdvr_to_val, ListViewDisVal[EditIndex].Hdvr_val, 0);
                if (result <= 0)
                {

                    ListDisregard.First(x => x.Hdvr_from_val == ListViewDisVal[EditIndex].Hdvr_from_val && x.Hdvr_to_val == ListViewDisVal[EditIndex].Hdvr_to_val && x.Hdvr_val == ListViewDisVal[EditIndex].Hdvr_val).Hdvr_from_val = Convert.ToDecimal(txtAmountFrom.Text);
                    ListDisregard.First(x => x.Hdvr_from_val == ListViewDisVal[EditIndex].Hdvr_from_val && x.Hdvr_to_val == ListViewDisVal[EditIndex].Hdvr_to_val && x.Hdvr_val == ListViewDisVal[EditIndex].Hdvr_val).Hdvr_to_val = Convert.ToDecimal(txtAmountTo.Text);
                    ListDisregard.First(x => x.Hdvr_from_val == ListViewDisVal[EditIndex].Hdvr_from_val && x.Hdvr_to_val == ListViewDisVal[EditIndex].Hdvr_to_val && x.Hdvr_val == ListViewDisVal[EditIndex].Hdvr_val).Hdvr_val = Convert.ToDecimal(txtRate.Text);

                    ListViewDisVal[EditIndex].Hdvr_from_val = Convert.ToDecimal(txtAmountFrom.Text);
                    ListViewDisVal[EditIndex].Hdvr_to_val = Convert.ToDecimal(txtAmountTo.Text);
                    ListViewDisVal[EditIndex].Hdvr_val = Convert.ToDecimal(txtRate.Text);
                    LoadGrid(ListViewDisVal);
                }
                else
                {
                    ListViewDisVal = new List<DisregardValueDefinition>();
                    List<DisregardValueDefinition> temp = CHNLSVC.Sales.GetDisregardValueDefinitiom(0, 0, 0, 0, "ALL");
                    if (temp != null)
                    {
                        ListViewDisVal.AddRange(temp);
                    }
                    LoadGrid(ListViewDisVal);
                }

                btnSave.Enabled = true;
                btnEdit.Visible = false;
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

        private void ClearAll()
        {

            btnAdd.Text = "";
            txtAmountFrom.Text = "";
            txtAmountTo.Text = "";
            txtRate.Text = "";

            btnSave.Enabled = true;
            btnEdit.Visible = false;
            ListDisregard = new List<DisregardValueDefinition>();
            ListViewDisVal = new List<DisregardValueDefinition>();
            List<DisregardValueDefinition> temp = CHNLSVC.Sales.GetDisregardValueDefinitiom(0, 0, 0, 0, "ALL");
            if (temp != null)
            {
                ListViewDisVal.AddRange(temp);
            }
            LoadGrid(ListViewDisVal);
        }

        private void txtAmountFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtAmountTo.Focus();
            }
        }

        private void txtAmountTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtRate.Focus();
            }
        }

        private void txtRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                btnAdd.Focus();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            #region validation
            decimal value;
            if (txtAmountFrom.Text == "")
            {
                MessageBox.Show("Please enter From value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmountFrom.Focus();
                return;
            }
            if (txtAmountTo.Text == "")
            {
                MessageBox.Show("Please enter To Value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmountTo.Focus();
                return;
            }
            if (txtRate.Text == "")
            {
                MessageBox.Show("Please enter Precentage", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRate.Focus();
                return;
            }
            if (!decimal.TryParse(txtAmountFrom.Text, out value))
            {
                MessageBox.Show("From value has to be a Number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmountFrom.Focus();
                return;
            }
            if (!decimal.TryParse(txtAmountTo.Text, out value))
            {
                MessageBox.Show("To value has to be a Number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmountTo.Focus();
                return;
            }
            if (!decimal.TryParse(txtRate.Text, out value))
            {
                MessageBox.Show("Precentage has to be a Number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRate.Focus();
                return;
            }
            if (Convert.ToDecimal(txtAmountFrom.Text) > Convert.ToDecimal(txtAmountTo.Text))
            {
                MessageBox.Show("To value has to be greater than To value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmountFrom.Focus();
                return ;
            }
            #endregion
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            DisregardValueDefinition _dis = new DisregardValueDefinition();
            _dis.Hdvr_from_val = Convert.ToDecimal(txtAmountFrom.Text);
            _dis.Hdvr_to_val = Convert.ToDecimal(txtAmountTo.Text);
            _dis.Hdvr_val = Convert.ToDecimal(txtRate.Text);
            _dis.Hdvr_cre_by = BaseCls.GlbUserID;
            _dis.Hdvr_cre_dt = _date;
            _dis.Hdvr_mod_by = BaseCls.GlbUserID;
            _dis.Hdvr_mod_dt = _date;
            _dis.Hdvr_tp = false;

            ListDisregard.Add(_dis);
            ListViewDisVal.Add(_dis);
            LoadGrid(ListViewDisVal);

            txtAmountFrom.Text = "";
            txtAmountTo.Text = "";
            txtRate.Text = "";

        }

        private void LoadGrid(List<DisregardValueDefinition> List)
        {
            dataGridViewDigradeValue.AutoGenerateColumns = false;
            var source = new BindingSource();
            source.DataSource = List;
            dataGridViewDigradeValue.DataSource = source;
        }

        private void DisregardAmount_Load(object sender, EventArgs e)
        {
            List<DisregardValueDefinition> temp = CHNLSVC.Sales.GetDisregardValueDefinitiom(0, 0, 0, 0, "ALL");
            if (temp != null)
            {
                ListViewDisVal.AddRange(temp);
            }
            LoadGrid(ListViewDisVal);
        }

        private void dataGridViewDigradeValue_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                txtAmountFrom.Text = dataGridViewDigradeValue.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtAmountTo.Text = dataGridViewDigradeValue.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtRate.Text = dataGridViewDigradeValue.Rows[e.RowIndex].Cells[4].Value.ToString();
                btnEdit.Visible = true;
                btnSave.Enabled = false;
                EditIndex = e.RowIndex;
            }

            if (e.RowIndex != -1 && e.ColumnIndex == 1) {
                if (MessageBox.Show("Are you sure ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes) {
                    decimal amountFrom = Convert.ToDecimal(dataGridViewDigradeValue.Rows[e.RowIndex].Cells[2].Value);
                    decimal amountTo = Convert.ToDecimal(dataGridViewDigradeValue.Rows[e.RowIndex].Cells[3].Value);
                    decimal rate = Convert.ToDecimal(dataGridViewDigradeValue.Rows[e.RowIndex].Cells[4].Value);

                    List<DisregardValueDefinition> result = (from _res in ListDisregard
                                                             where _res.Hdvr_from_val == amountFrom && _res.Hdvr_to_val == amountTo && _res.Hdvr_val == rate
                                                             select _res).ToList<DisregardValueDefinition>();

                    if (result != null && result.Count > 0)
                    {
                        ListDisregard.Remove(result[0]);
                        ListViewDisVal.Remove(result[0]);
                        LoadGrid(ListViewDisVal);
                    }
                    else {
                        MessageBox.Show("Can not delete saved to database permanently", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                
                }
            }
        }
    }
}
