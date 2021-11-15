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

namespace FF.WindowsERPClient.General
{
    public partial class SubLocation : Base
    {

        public SubLocation()
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
                case CommonUIDefiniton.SearchUserControlType.SubLoc:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtMLoc.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
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
            if (MessageBox.Show("Do you want to clear screen ?", "Sub Location", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No) return;
            Clear_Data();
            txtMLoc.Text = "";
            txtCode.Text = "";
            

        }
        private void Clear_Data()
        {
            txtAddr1.Text = "";
            txtAddr2.Text = "";
            txtContact.Text = "";
            txtContNo.Text = "";
            txtCoverRef.Text = "";
            txtDesc.Text = "";
            
            txtInsuVal.Text = "0.00";
            txtRem.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(dtpFrom.Value.Date > dtpTo.Value.Date)
            {
                MessageBox.Show("Start from date cannot be greater than operate till date", "Sub Locations ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtDesc.Text))
            {
                MessageBox.Show("Enter Description", "Sub Locations ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MessageBox.Show("Enter Sub Location Code", "Sub Locations ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtAddr1.Text))
            {
                MessageBox.Show("Enter Address 1", "Sub Locations ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtMLoc.Text))
            {
                MessageBox.Show("Enter Main Location Code", "Sub Locations ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            MasterSubLocation _mstSubLoc = new MasterSubLocation();
            _mstSubLoc.Msl_act = chkAct.Checked == true ? true : false;
            _mstSubLoc.Msl_com = BaseCls.GlbUserComCode;
            _mstSubLoc.Msl_cont_no = txtContNo.Text;
            _mstSubLoc.Msl_cont_per = txtContact.Text;
            _mstSubLoc.Msl_cre_by = BaseCls.GlbUserID;
            _mstSubLoc.Msl_mloc = txtMLoc.Text;
            _mstSubLoc.Msl_mod_by = BaseCls.GlbUserID;
            _mstSubLoc.Msl_operate_till = Convert.ToDateTime(dtpTo.Value);
            _mstSubLoc.Msl_rmk = txtRem.Text;
            _mstSubLoc.Msl_sadd1 = txtAddr1.Text;
            _mstSubLoc.Msl_sadd2 = txtAddr2.Text;
            _mstSubLoc.Msl_sloc = txtCode.Text;
            _mstSubLoc.Msl_sloc_desc = txtDesc.Text;
            _mstSubLoc.Msl_start_frm = Convert.ToDateTime(dtpFrom.Value);

            string _err = string.Empty;

            int row_aff = CHNLSVC.General.SaveSubLocations(_mstSubLoc, out _err);
            if (row_aff == 1)
            {
                MessageBox.Show(_err, "Sub Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear_Data();
                txtMLoc.Text = "";
                txtCode.Text = "";
            }
            else
            {
                MessageBox.Show(_err, "Sub Locations", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Clear_Data();
                txtMLoc.Text = "";
                txtCode.Text = "";
            }

        }

        private void txtMLoc_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMLoc.Text))
            {
                DataTable _dt = CHNLSVC.Sales.getLocDesc(BaseCls.GlbUserComCode, "LOC", txtMLoc.Text);
                if (_dt == null || _dt.Rows.Count <= 0)
                {
                    MessageBox.Show("Invalid location", "Sub Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMLoc.Clear();
                    txtMLoc.Focus();
                    return;
                }
            }
        }

        private void btn_srch_main_loc_Click(object sender, EventArgs e)
        {

            CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
            _commonSearch.ReturnIndex = 0;
            _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_commonSearch.SearchParams, null, null);
            _commonSearch.dvResult.DataSource = _result;
            _commonSearch.BindUCtrlDDLData(_result);
            _commonSearch.obj_TragetTextBox = txtMLoc;
            this.Cursor = Cursors.Default;
            _commonSearch.ShowDialog();
            txtMLoc.Select();

        }

        private void txtMLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_main_loc_Click(null, null);
        }

        private void txtMLoc_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_main_loc_Click(null, null);
        }

        private void txtCode_Leave(object sender, EventArgs e)
        {
            DataTable _dt = CHNLSVC.Sales.getSubLocationByCode(BaseCls.GlbUserComCode, txtMLoc.Text, txtCode.Text);
            if (_dt.Rows.Count > 0)
            {
                txtDesc.Text = _dt.Rows[0]["MSL_SLOC_DESC"].ToString();
                txtAddr1.Text = _dt.Rows[0]["MSL_SADD1"].ToString();
                txtAddr2.Text = _dt.Rows[0]["MSL_SADD2"].ToString();
                txtContact.Text = _dt.Rows[0]["MSL_CONT_PER"].ToString();
                txtContNo.Text = _dt.Rows[0]["MSL_CONT_NO"].ToString();
                txtCoverRef.Text = _dt.Rows[0]["MSL_COVER_REF"].ToString();
                txtInsuVal.Text = _dt.Rows[0]["MSL_INSU_VAL"].ToString();
                dtpFrom.Value = Convert.ToDateTime(_dt.Rows[0]["MSL_START_FRM"]);
                dtpTo.Value = Convert.ToDateTime(_dt.Rows[0]["MSL_OPERATE_TILL"]);
                dtpInsuDt.Value = Convert.ToDateTime(_dt.Rows[0]["MSL_INSU_DT"]);
                txtRem.Text = _dt.Rows[0]["MSL_RMK"].ToString();
            }
            else
                Clear_Data();
        }

        private void txtContNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtContNo.Text))
            {
                Boolean _isvalid = IsValidMobileOrLandNo(txtContNo.Text.Trim());

                if (_isvalid == false)
                {
                    MessageBox.Show("Invalid phone number.", "Sub Location", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContNo.Text = "";
                    txtContNo.Focus();
                    return;
                }
            }
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_sub_loc_Click(null, null);
        }

        private void btn_srch_sub_loc_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SubLoc);
            DataTable _result = CHNLSVC.CommonSearch.SearchSubLocationData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCode;
            _CommonSearch.ShowDialog();
            txtCode.Select();
        }

        private void txtCode_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_sub_loc_Click(null, null);
        }

    }
}
