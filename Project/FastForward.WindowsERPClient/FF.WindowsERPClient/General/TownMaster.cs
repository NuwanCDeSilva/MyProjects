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
using FF.WindowsERPClient.Reports.Sales;


namespace FF.WindowsERPClient.General
{
    public partial class TownMaster : Base
    {

        public TownMaster(string userID, string UserComCode, string _OrgPC)
        {
            InitializeComponent();
        }
        public TownMaster()
        {
            InitializeComponent();
            bind_Combo_cmbType();
        }

        private void bind_Combo_cmbType()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("MCITY", "Main City");
            PartyTypes.Add("CITY", "City");
            PartyTypes.Add("BCITY", "Big Town");
            PartyTypes.Add("OTH", "Other Town");

            cmbType.DataSource = new BindingSource(PartyTypes, null);
            cmbType.DisplayMember = "Value";
            cmbType.ValueMember = "Key";
        }

        private void CustomerCreation_Load(object sender, EventArgs e)
        {
            Clear_Data();
            txtProv.Text = "";
            lblProv.Text = "";
            txtDist.Text = "";
            lblDist.Text = "";
            txtDivSec.Text = "";
            lblDiv.Text = "";
        }

        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Province:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.District:
                    {
                        paramsText.Append(lblProv.Text + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion


        private void Clear_Data()
        {
            txtTown.Text = "";
            //txtProv.Text = "";
            //lblProv.Text = "";
            //txtDist.Text = "";
            //lblDist.Text = "";
            //txtDivSec.Text = "";
            //lblDiv.Text = "";
            txtHeight.Text = "0";
            txtHeightUOM.Text = "";
            cmbType.SelectedIndex = -1;
            txtLat.Text = "";
            txtLong.Text = "";
            txtPostal.Text = "";
            txtDistFrom.Text = "";
            lblDistFrom.Text = "";
            txtDistnce.Text = "0";
            txtDistUOM.Text = "";
        }

        private void btnSrchTown_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 3;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
            DataTable _result = CHNLSVC.CommonSearch.GetTown(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCode;
            _CommonSearch.ShowDialog();
            txtTown.Focus();
            txtCode_Leave(null, null);

        }

        private void txtCode_Leave(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                DataTable dt = new DataTable();

                dt = CHNLSVC.General.GetTownByCode(txtCode.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    txtTown.Text = dt.Rows[0]["MT_DESC"].ToString();
                    cmbType.SelectedValue = dt.Rows[0]["MT_TP"].ToString();
                    lblDist.Text = dt.Rows[0]["MT_DISTRIC_CD"].ToString();
                    load_Dist();
                    lblProv.Text = dt.Rows[0]["MT_PROVINCE_CD"].ToString();
                    load_Prov();
                    lblDiv.Text = dt.Rows[0]["MT_DS_CD"].ToString();
                    load_Div();
                    lblDistFrom.Text = dt.Rows[0]["MT_DISTANCE_FROM"].ToString();
                    load_DistFrom();

                    txtPostal.Text = dt.Rows[0]["MT_POSTAL_CD"].ToString();
                    txtDistUOM.Text = dt.Rows[0]["MT_DISTANCE_UOM"].ToString();
                    txtDistnce.Text = dt.Rows[0]["MT_DISTANCE"].ToString();
                    txtLat.Text = dt.Rows[0]["MT_LAT"].ToString();
                    txtLong.Text = dt.Rows[0]["MT_LON"].ToString();
                    txtHeightUOM.Text = dt.Rows[0]["MT_HEIGHT_UOM"].ToString();
                    txtHeight.Text = dt.Rows[0]["MT_HEIGHT"].ToString();



                }
                else
                {
                    Clear_Data();
                    //txtProv.Text = "";
                    //lblProv.Text = "";
                    //txtDist.Text = "";
                    //lblDist.Text = "";
                    //txtDivSec.Text = "";
                    //lblDiv.Text = "";
                    txtTown.Focus();
                }

            }
        }

        private void load_Dist()
        {
            DataTable DT = CHNLSVC.General.GetDistrictByCode(lblDist.Text);
            if (DT.Rows.Count > 0)
                txtDist.Text = DT.Rows[0]["MDIS_DESC"].ToString();
            else
                txtDist.Text = "";
        }
        private void load_Prov()
        {
            DataTable DT = CHNLSVC.General.GetProvinceByCode(lblProv.Text);
            if (DT.Rows.Count > 0)
                txtProv.Text = DT.Rows[0]["MPRO_DESC"].ToString();
            else
                txtProv.Text = "";
        }
        private void load_DistFrom()
        {
            DataTable DT = CHNLSVC.General.GetTownByCode(lblDistFrom.Text);
            if (DT.Rows.Count > 0)
                txtDistFrom.Text = DT.Rows[0]["MT_DESC"].ToString();
            else
                txtDistFrom.Text = "";
        }
        private void load_Div()
        {
            DataTable DT = CHNLSVC.General.GetTownByCode(lblDiv.Text);
            if (DT.Rows.Count > 0)
                txtDivSec.Text = DT.Rows[0]["MT_DESC"].ToString();
            else
                txtDivSec.Text = "";
        }
        private void btnSrchDistFrom_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 3;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
            DataTable _result = CHNLSVC.CommonSearch.GetTown(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = lblDistFrom;
            _CommonSearch.ShowDialog();
            lblDistFrom.Focus();
            load_DistFrom();
        }

        private void btnSrchDiv_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 3;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
            DataTable _result = CHNLSVC.CommonSearch.GetTown(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = lblDiv;
            _CommonSearch.ShowDialog();
            lblDiv.Focus();
            load_Div();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MessageBox.Show("Enter town code!", "Town Master", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtTown.Text))
            {
                MessageBox.Show("Enter town name!", "Town Master", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtDist.Text))
            {
                MessageBox.Show("Enter district!", "Town Master", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtProv.Text))
            {
                MessageBox.Show("Enter province!", "Town Master", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (string.IsNullOrEmpty(txtDivSec.Text))
            //{
            //    MessageBox.Show("Enter divisional secretariat!", "Town Master", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            MasterTown _mstTown = new MasterTown();
            _mstTown.Mt_country_cd = "LK";
            _mstTown.Mt_cd = txtCode.Text;
            _mstTown.Mt_desc = txtTown.Text;
            _mstTown.Mt_tp = cmbType.SelectedValue.ToString();
            _mstTown.Mt_province_cd = lblProv.Text;
            _mstTown.Mt_distric_cd = lblDist.Text;
            _mstTown.Mt_postal_cd = txtPostal.Text;
            _mstTown.Mt_distance_from = lblDistFrom.Text;
            _mstTown.Mt_distance_uom = txtDistUOM.Text;
            if (string.IsNullOrEmpty(txtDistnce.Text))
                _mstTown.Mt_distance = 0;
            else
                _mstTown.Mt_distance = Convert.ToDecimal(txtDistnce.Text);
            _mstTown.Mt_lat = txtLat.Text;
            _mstTown.Mt_lon = txtLong.Text;
            _mstTown.Mt_height_uom = txtHeightUOM.Text;
            if (string.IsNullOrEmpty(txtHeight.Text))
                _mstTown.Mt_height = 0;
            else
                _mstTown.Mt_height = Convert.ToDecimal(txtHeight.Text);
            _mstTown.Mt_act = chkAct.Checked;
            _mstTown.Mt_cre_by = BaseCls.GlbUserID;
            _mstTown.Mt_mod_by = BaseCls.GlbUserID;
            _mstTown.Mt_session_id = BaseCls.GlbUserSessionID;
            _mstTown.Mt_ds_cd = lblDiv.Text;

            string _msg = "";
            Int32 _eff = CHNLSVC.Sales.SaveTownMaster(_mstTown, out _msg);
            if (_eff > 0)
            {
                MessageBox.Show("Successfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear_Data();
                txtCode.Text = "";
                txtCode.Focus();
            }
            else
            {
                MessageBox.Show("Please try again." + _msg, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        private void btnSrchProv_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Province);
            DataTable _result = CHNLSVC.CommonSearch.GetProvinceData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = lblProv;
            _CommonSearch.ShowDialog();
            lblProv.Focus();
            txtDist.Text = "";
            lblDist.Text = "";
            load_Prov();
        }

        private void btnSrchDist_Click(object sender, EventArgs e)
        {
            if (txtProv.Text == "")
            {
                MessageBox.Show("Select the Province", "Profit Center Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.District);
            DataTable _result = CHNLSVC.CommonSearch.GetDistrictByProvinceData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = lblDist;
            _CommonSearch.ShowDialog();
            lblDist.Focus();
            load_Dist();
        }

        private void btnSrchDistUOM_Click(object sender, EventArgs e)
        {

        }

        private void TownMaster_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtProv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchProv_Click(null, null);
        }

        private void txtDist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchDist_Click(null, null);
        }

        private void txtDivSec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchDiv_Click(null, null);
        }



    }
}
