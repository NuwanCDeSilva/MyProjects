using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using FF.BusinessObjects;
using FF.Interfaces;

namespace FF.WindowsERPClient.HP
{
    public partial class CollectionBonusDefinition : Base
    {
        #region properties

        FF.BusinessObjects.CollectionBonusDefinition _EditBonous;
        #endregion


        public CollectionBonusDefinition()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                this.Close();
            }
        }


        private void Save()
        {
            #region validation

            int val;
            try
            {
                if (!int.TryParse(txtArrearsFrom.Text, out val))
                {
                    MessageBox.Show("Arrears from has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(txtArrearsTo.Text, out val))
                {
                    MessageBox.Show("Arrears to has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Regex regex = new Regex(@"^[0-9]+$");

                if (!regex.IsMatch(txtLocCreation.Text))
                {
                    MessageBox.Show("Location Creation From has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!regex.IsMatch(txtLocCreationTo.Text))
                {
                    MessageBox.Show("Location Creation To has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(txtBonus.Text, out val))
                {
                    MessageBox.Show("Bonous has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(txtAddBonus.Text, out val))
                {
                    MessageBox.Show("Additional Bonous has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Convert.ToInt32(txtBonus.Text) < 0 || Convert.ToInt32(txtBonus.Text) > 100)
                {
                    MessageBox.Show("Bonous has to be 0-100 range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Convert.ToInt32(txtAddBonus.Text) < 0 || Convert.ToInt32(txtAddBonus.Text) > 100)
                {
                    MessageBox.Show("Additional Bonous has to be 0-100 range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!regex.IsMatch(txtContMonths.Text))
                {
                    MessageBox.Show("Continue Months has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!regex.IsMatch(txtAvgAcc.Text))
                {
                    MessageBox.Show("Average accounts has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                FF.BusinessObjects.CollectionBonusDefinition _colBonus = CHNLSVC.Sales.GetCollectionBonus(Convert.ToInt32(txtArrearsFrom.Text), Convert.ToInt32(txtArrearsTo.Text), Convert.ToInt32(txtLocCreation.Text), Convert.ToInt32(txtLocCreationTo.Text));

                if (_colBonus != null)
                {
                    MessageBox.Show("Already in the database", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            #endregion
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                FF.BusinessObjects.CollectionBonusDefinition _col = new FF.BusinessObjects.CollectionBonusDefinition();
                _col.Hcbd_ars_per_from = Convert.ToInt32(txtArrearsFrom.Text);
                _col.Hcbd_ars_per_to = Convert.ToInt32(txtArrearsTo.Text);
                _col.Hcbd_avg_acc = Convert.ToInt32(txtAvgAcc.Text);
                _col.Hcbd_bon_add_per = Convert.ToInt32(txtAddBonus.Text);
                _col.Hcbd_bon_per = Convert.ToInt32(txtBonus.Text);
                _col.Hcbd_contu_month = Convert.ToInt32(txtContMonths.Text);
                _col.Hcbd_year_from = Convert.ToInt32(txtLocCreation.Text);
                _col.Hcbd_year_to = Convert.ToInt32(txtLocCreationTo.Text);
                _col.Hcbd_cre_by = BaseCls.GlbUserID;
                _col.Hcbd_cre_dt = _date;
                _col.Hcbd_mod_by = BaseCls.GlbUserID;
                _col.Hcbd_mod_dt = _date;

                int result = CHNLSVC.Sales.SaveCollectionBonusDefinition(_col);
                if (result == 0)
                {
                    MessageBox.Show("Error Occurred Not saved!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Saved Successfully!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearAll();
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error occurred while processing\n" + ex.Message , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }

            GridviewDataBind();
        }

        private void GridviewDataBind()
        {
            var source = new BindingSource();
            source.DataSource = CHNLSVC.Sales.GetAllCollectionBonusDefinition();
            dataGridViewDetails.DataSource = source;
        }



        private void CollectionBonusDefinition_Load(object sender, EventArgs e)
        {
            dataGridViewDetails.AutoGenerateColumns = false;
            GridviewDataBind();

            //CHECK PERMISSION
            //dataGridViewDetails.Columns[0].Visible = false;
        }

        private void dataGridViewDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) {
                if (e.ColumnIndex == 0) {
                        btnEdit.Visible = true;
                        txtArrearsFrom.Text = dataGridViewDetails.Rows[e.RowIndex].Cells[1].Value.ToString();
                        txtArrearsTo.Text = dataGridViewDetails.Rows[e.RowIndex].Cells[2].Value.ToString();
                        txtLocCreation.Text = dataGridViewDetails.Rows[e.RowIndex].Cells[3].Value.ToString();
                        txtLocCreationTo.Text = dataGridViewDetails.Rows[e.RowIndex].Cells[4].Value.ToString();
                        txtBonus.Text = dataGridViewDetails.Rows[e.RowIndex].Cells[5].Value.ToString();
                        txtAddBonus.Text=dataGridViewDetails.Rows[e.RowIndex].Cells[6].Value.ToString();
                        txtContMonths.Text = dataGridViewDetails.Rows[e.RowIndex].Cells[7].Value.ToString();
                        txtAvgAcc.Text = dataGridViewDetails.Rows[e.RowIndex].Cells[8].Value.ToString();
                        _EditBonous = new BusinessObjects.CollectionBonusDefinition();
                        _EditBonous = CHNLSVC.Sales.GetCollectionBonus(Convert.ToInt32(txtArrearsFrom.Text), Convert.ToInt32(txtArrearsTo.Text), Convert.ToInt32(txtLocCreation.Text), Convert.ToInt32(txtLocCreationTo.Text));
                        btnSave.Enabled = false;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                //validate
                FF.BusinessObjects.CollectionBonusDefinition _colBonus = CHNLSVC.Sales.GetCollectionBonus(Convert.ToInt32(txtArrearsFrom.Text), Convert.ToInt32(txtArrearsTo.Text), Convert.ToInt32(txtLocCreation.Text), Convert.ToInt32(txtLocCreationTo.Text));
                if (_colBonus != null)
                {
                    MessageBox.Show("Already in the database", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //if has record
                if (_EditBonous != null)
                {
                    DateTime _date = CHNLSVC.Security.GetServerDateTime();
                    FF.BusinessObjects.CollectionBonusDefinition _col = new BusinessObjects.CollectionBonusDefinition();
                    _col.Hcbd_ars_per_from = Convert.ToInt32(txtArrearsFrom.Text);
                    _col.Hcbd_ars_per_to = Convert.ToInt32(txtArrearsTo.Text);
                    _col.Hcbd_avg_acc = Convert.ToInt32(txtAvgAcc.Text);
                    _col.Hcbd_bon_add_per = Convert.ToInt32(txtAddBonus.Text);
                    _col.Hcbd_bon_per = Convert.ToInt32(txtBonus.Text);
                    _col.Hcbd_contu_month = Convert.ToInt32(txtContMonths.Text);
                    _col.Hcbd_year_from = Convert.ToInt32(txtLocCreation.Text);
                    _col.Hcbd_year_to = Convert.ToInt32(txtLocCreationTo.Text);
                    _col.Hcbd_mod_by = BaseCls.GlbUserID;
                    _col.Hcbd_mod_dt = _date;

                    int result = CHNLSVC.Sales.UpdateCollectionBonus(_col, _EditBonous.Hcbd_ars_per_from, _EditBonous.Hcbd_ars_per_to, _EditBonous.Hcbd_year_from, _EditBonous.Hcbd_year_to);
                    if (result == 0)
                    {
                        MessageBox.Show("Updated Successfully!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    GridviewDataBind();
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
            btnEdit.Visible = false;
            btnSave.Enabled = true;
        }


        private void ClearAll() {
            txtArrearsFrom.Text = "0";
            txtArrearsTo.Text = "0";
            txtAvgAcc.Text = "0";
            txtContMonths.Text = "0";
            txtBonus.Text = "0";
            txtContMonths.Text = "0";
            txtLocCreation.Text = "0";
            txtLocCreationTo.Text = "0";
            txtAddBonus.Text = "0";

            btnEdit.Visible = false;
            btnSave.Enabled = true;
            _EditBonous=null;

            GridviewDataBind();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CollectionBonusDefinition_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void txtArrearsFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtArrearsTo.Focus();
            }
        }

        private void txtArrearsTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtLocCreation.Focus();
            }
        }

        private void txtLocCreation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtLocCreationTo.Focus();
            }
        }

        private void txtLocCreationTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBonus.Focus();
            }
        }

        private void txtBonus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddBonus.Focus();
            }
        }

        private void txtAddBonus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtContMonths.Focus();
            }
        }

        private void txtContMonths_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAvgAcc.Focus();
            }
        }

        private void txtAvgAcc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStrip1.Focus();
                btnSave.Select();
            }
        }
    }
}
