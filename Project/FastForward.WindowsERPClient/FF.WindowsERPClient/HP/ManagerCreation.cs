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
    public partial class ManagerCreation : Base
    {
        public ManagerCreation()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int date = dateTimePicker1Account.Value.Day;
            if (date > 15)
            {
                dateTimePickerSRCalculation.Value = new DateTime(dateTimePicker1Account.Value.Year, dateTimePicker1Account.Value.AddMonths(1).Month, 1);
            }
            else {
                dateTimePickerSRCalculation.Value = new DateTime(dateTimePicker1Account.Value.Year, dateTimePicker1Account.Value.Month, 1);
            }
            dateTimePickerCollectionBonusStart.Value = new DateTime(dateTimePicker1Account.Value.Year, dateTimePicker1Account.Value.AddMonths(1).Month, 1);
        }

        private void ManagerCreation_Load(object sender, EventArgs e)
        {
            dateTimePickerCollectionBonusStart.Format = DateTimePickerFormat.Custom;
            dateTimePickerCollectionBonusStart.CustomFormat = "MMM/yyyy";
            dateTimePicker1_ValueChanged(null, null);
            LoadGrid();
        }

        private void buttonSearchCompany_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Employee_Executive);
            DataTable _result = CHNLSVC.CommonSearch.GetEmployeeData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtManagerCode;
            _CommonSearch.ShowDialog();
            txtManagerCode.Select();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "SEX" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
            }
            return paramsText.ToString();
        }

        private void txtManagerCode_Leave(object sender, EventArgs e)
        {
            if(txtManagerCode.Text!=""){
                DataTable dt = CHNLSVC.Sales.GetEmployee(BaseCls.GlbUserComCode, txtManagerCode.Text);
                if (dt.Rows.Count > 0) {
                    lblManager.Text = dt.Rows[0]["ESEP_FIRST_NAME"].ToString() + " " + dt.Rows[0]["ESEP_LAST_NAME"].ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtLocation;
            _CommonSearch.txtSearchbyword.Text = txtLocation.Text;
            _CommonSearch.ShowDialog();
            txtLocation.Select();
        }

        private void txtLocation_Leave(object sender, EventArgs e)
        {
            if (txtLocation.Text != "")
            {
                MasterLocation loc = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtLocation.Text);
                lblLocation.Text = loc.Ml_loc_desc;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region validation

                if (txtManagerCode.Text == "")
                {
                    MessageBox.Show("Please enter Manager code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtManagerCode.Focus();
                    return;
                }
                if (txtLocation.Text == "")
                {
                    MessageBox.Show("Please enter Location code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLocation.Focus();
                    return;
                }
                DataTable dt = CHNLSVC.Sales.GetManagerCreation(BaseCls.GlbUserComCode, txtLocation.Text, txtManagerCode.Text);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Record already exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                #endregion
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                FF.BusinessObjects.ManegerCreation mgr = new FF.BusinessObjects.ManegerCreation();
                mgr.Hmfa_com = BaseCls.GlbUserComCode;
                mgr.Hmfa_act_stus = chkActive.Checked;
                mgr.Hmfa_acc_dt = dateTimePicker1Account.Value.Date;
                mgr.Hmfa_bonus_st_dt = dateTimePickerCollectionBonusStart.Value.Date;
                mgr.Hmfa_cre_by = BaseCls.GlbUserID;
                mgr.Hmfa_cre_dt = _date;
                mgr.Hmfa_mgr_cd = txtManagerCode.Text;
                mgr.Hmfa_mod_by = BaseCls.GlbUserID;
                mgr.Hmfa_mod_dt = _date;
                mgr.Hmfa_pc = txtLocation.Text;
                mgr.Hmfa_sr_open_dt = dateTimePickerSRCalculation.Value.Date;

                int result = CHNLSVC.Sales.SaveManagerCreation(mgr);
                if (result > 0)
                {
                    MessageBox.Show("Record insert Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Record not insert Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadGrid();
                ClearAll();
            }
            catch (Exception ex) {
                MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
        }

        private void LoadGrid()
        {
            gvDetails.AutoGenerateColumns = false;
            var source = new BindingSource();
            source.DataSource = CHNLSVC.Sales.GetManagerCreation("%", "%", "%");
            gvDetails.DataSource = source;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearAll() {
            txtManagerCode.Text = "";
            txtLocation.Text = "";

            lblLocation.Text = "";
            lblManager.Text = "";

            gvDetails.DataSource = null;

            dateTimePicker1Account.Value = DateTime.Now;
            dateTimePickerCollectionBonusStart.Value = DateTime.Now;
            dateTimePickerSRCalculation.Value = DateTime.Now;

            btnEdit.Enabled = false;
            btnSave.Enabled = true;
            LoadGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                this.Close();
            }
        }

        private void gvDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0) {
                txtManagerCode.Text = gvDetails.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtLocation.Text = gvDetails.Rows[e.RowIndex].Cells[1].Value.ToString();
                dateTimePicker1Account.Value = Convert.ToDateTime(gvDetails.Rows[e.RowIndex].Cells[6].Value);
                btnSave.Enabled = false;
                btnEdit.Enabled = true;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                #region validation

                if (txtManagerCode.Text == "")
                {
                    MessageBox.Show("Please enter Manager code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtManagerCode.Focus();
                    return;
                }
                if (txtLocation.Text == "")
                {
                    MessageBox.Show("Please enter Location code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLocation.Focus();
                    return;
                }

                #endregion

                //delete
                CHNLSVC.Sales.DeleteManager(BaseCls.GlbUserComCode, txtLocation.Text, txtManagerCode.Text);

                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                FF.BusinessObjects.ManegerCreation mgr = new FF.BusinessObjects.ManegerCreation();
                mgr.Hmfa_com = BaseCls.GlbUserComCode;
                mgr.Hmfa_act_stus = chkActive.Checked;
                mgr.Hmfa_acc_dt = dateTimePicker1Account.Value.Date;
                mgr.Hmfa_bonus_st_dt = dateTimePickerCollectionBonusStart.Value.Date;
                mgr.Hmfa_mgr_cd = txtManagerCode.Text;
                mgr.Hmfa_mod_by = BaseCls.GlbUserID;
                mgr.Hmfa_mod_dt = _date;
                mgr.Hmfa_pc = txtLocation.Text;
                mgr.Hmfa_sr_open_dt = dateTimePickerSRCalculation.Value.Date;

                int result = CHNLSVC.Sales.UpdateManagerCreation(mgr);
                if (result > 0)
                {
                    MessageBox.Show("Record updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Record not updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ClearAll();
                btnEdit.Enabled = false;
                btnSave.Enabled = true;
            }
            catch (Exception ex) {
                MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
        }

        private void txtManagerCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtLocation.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                buttonSearchCompany_Click(null, null);
            }
        }

        private void txtLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePicker1Account.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                button2_Click(null, null);
            }
        }

        private void dateTimePicker1Account_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }


        private void chkActive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStrip1.Focus();
                btnSave.Select();
            }
        }
    }
}
