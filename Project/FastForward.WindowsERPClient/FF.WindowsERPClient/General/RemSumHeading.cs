using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Threading;
using System.Collections;
using FF.WindowsERPClient.General;


//Written By kapila on 05-03-2013
namespace FF.WindowsERPClient.General
{
    public partial class RemSumHeading : Base
    {
        static Int32 _tabIndex = 0;
        public RemSumHeading()
        {
            InitializeComponent();
            BindData();
        }

        private void BindData()
        {
            gvHead.AutoGenerateColumns = false;
            gvRemLimit.AutoGenerateColumns = false;

            cmbSec.Items.Clear();
            cmbSec.DataSource = null;
            cmbSec.DataSource = CHNLSVC.Financial.GetSection();
            cmbSec.DisplayMember = "RSS_DESC";
            cmbSec.ValueMember = "RSS_CD";

            cmbSecDef.Items.Clear();
            cmbSecDef.DataSource = null;
            cmbSecDef.DataSource = CHNLSVC.Financial.GetSection();
            cmbSecDef.DisplayMember = "RSS_DESC";
            cmbSecDef.ValueMember = "RSS_CD";

        }

        private void LoadPartyCodes()
        {
            //ddlPtyCd.Items.Clear();
            //ddlPtyCd.DataSource = CHNLSVC.Sales.get_pc_info_by_code(Convert.ToString(ddlPtyTp.SelectedValue));
            //ddlPtyCd.DisplayMember = "mpi_val";
            //ddlPtyCd.ValueMember = "mpi_val";
        }

        private void ddlPtyTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPartyCodes();
            BindRemSumLimitation();
        }

        private void LoadRemitTypes(string _sec)
        {
            ddlRemTp.DataSource = null;
            ddlRemTp.Items.Clear();
            ddlRemTp.DataSource = CHNLSVC.Financial.get_rem_type_by_sec(_sec, 2);
            ddlRemTp.DisplayMember = "rsd_desc";
            ddlRemTp.ValueMember = "rsd_cd";
        }

        private void cmbSecDef_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRemitTypes(Convert.ToString(cmbSecDef.SelectedValue));
        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
            if (_tabIndex == 0)
            {
                if (cmbSec.SelectedIndex == -1)
                {

                    MessageBox.Show("Please select section", "Remitance Heading", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtCode.Text == "")
                {

                    MessageBox.Show("Enter the code", "Remitance Heading", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (CHNLSVC.Financial.IsRemHeadFixed(Convert.ToString(cmbSec.SelectedValue), txtCode.Text) == true)
                {

                    MessageBox.Show("Fixed Heading cannot be changed !", "Remitance Heading", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtDesc.Text == "")
                {

                    MessageBox.Show("Enter the description", "Remitance Heading", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                RemitanceSumHeading _remSumHead = new RemitanceSumHeading();
                _remSumHead.Rsd_sec = Convert.ToString(cmbSec.SelectedValue);
                _remSumHead.Rsd_cd = txtCode.Text;
                _remSumHead.Rsd_desc = txtDesc.Text;
                _remSumHead.Rsd_fixed = 0;
                _remSumHead.Rsd_acc = "";
                _remSumHead.Rsd_cre_by = BaseCls.GlbUserID;
                _remSumHead.Rsd_cre_when = Convert.ToDateTime(DateTime.Now.Date).Date;
                _remSumHead.Rsd_mod_by = BaseCls.GlbUserID;
                _remSumHead.Rsd_mod_when = Convert.ToDateTime(DateTime.Now.Date).Date;
                _remSumHead.Rsd_stus = (chkActive.Checked == true) ? "A" : "D";
                _remSumHead.RSD_IS_ONCE = (chkOnce.Checked == true) ? 1 : 0;

                int row_aff = CHNLSVC.Financial.SaveRemSumHeading(_remSumHead);

                LoadRemSumHeading();
                MessageBox.Show("Successfully Updated", "Remitance Heading", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtCode.Text = "";
                txtDesc.Text = "";
                chkActive.Checked = false;
                chkOnce.Checked = false;
                chkFix.Checked = false;
                txtCode.Focus();
            }
            else
            {
                if (ddlRemTp.SelectedIndex == -1)
                {

                    MessageBox.Show("Please select Remitance", "Remitance Heading", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (cmbSecDef.SelectedIndex == -1)
                {

                    MessageBox.Show("Please select Section", "Remitance Heading", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(txtPtyCd.Text))
                {

                    MessageBox.Show("Please select the party code", "Remitance Heading", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(txtPtyTp.Text))
                {

                    MessageBox.Show("Please select the party type", "Remitance Heading", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                RemSumDefinitions _remSumLimit = new RemSumDefinitions();
                _remSumLimit.Rsmd_pty_tp = Convert.ToString(txtPtyTp.Text);
                _remSumLimit.Rsmd_pty_cd = Convert.ToString(txtPtyCd.Text);
                _remSumLimit.Rsmd_from_dt = Convert.ToDateTime(txtFrom.Text);
                _remSumLimit.Rsmd_to_dt = Convert.ToDateTime(txtTo.Text);
                _remSumLimit.Rsmd_sec = Convert.ToString(cmbSecDef.SelectedValue);
                _remSumLimit.Rsmd_cd = Convert.ToString(ddlRemTp.SelectedValue);
                _remSumLimit.Rsmd_max_val = Convert.ToDecimal(txtVal.Text);
                _remSumLimit.Rsmd_cre_by = BaseCls.GlbUserID;
                _remSumLimit.Rsmd_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                _remSumLimit.Rsmd_mod_by = BaseCls.GlbUserID;
                _remSumLimit.Rsmd_mod_dt = Convert.ToDateTime(DateTime.Now.Date).Date;

                int row_aff = CHNLSVC.Financial.SaveRemSumLimitations(_remSumLimit);
                BindRemSumLimitation();

                MessageBox.Show("Successfully Updated!", "Remitance Heading", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ddlRemTp.SelectedIndex = -1;
                txtVal.Text = "";
                txtPtyCd.Text = "";
                txtPtyTp.Text = "";
                cmbSecDef.SelectedIndex = -1;
                lblCdDesc.Text = "";
                lblTpDesc.Text = "";

            }
        }

        private void BindRemSumLimitation()
        {
            //gvRemLimit.DataSource = CHNLSVC.Financial.GetRemSumLimitations(Convert.ToString(ddlPtyTp.SelectedValue), Convert.ToString(ddlPtyCd.SelectedValue), Convert.ToString( cmbSecDef.SelectedValue),Convert.ToString( ddlRemTp.SelectedValue));
            gvRemLimit.DataSource = CHNLSVC.Financial.GetRemSumLimitations(Convert.ToString(txtPtyTp.Text), null, null, null);
        }

        private void LoadRemSumHeading()
        {
            gvHead.DataSource = CHNLSVC.Financial.GetRemSumHeadingBySec(Convert.ToString(cmbSec.SelectedValue));
        }

        private void cmbSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRemSumHeading();
            txtCode.Text = "";
            txtDesc.Text = "";
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            _tabIndex = tabControl1.SelectedIndex;
        }

        private void ddlPtyTp_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            gvHead.DataSource = null;
            gvRemLimit.DataSource = null;
            cmbSec.SelectedIndex = -1;
            cmbSecDef.SelectedIndex = -1;
            txtCode.Text = "";
            txtDesc.Text = "";
            txtVal.Text = "";
            txtPtyTp.Text = "";
            txtPtyCd.Text = "";
            ddlRemTp.SelectedIndex = -1;
            lblCdDesc.Text = "";
            lblTpDesc.Text = "";
        }

        private void cmbSec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCode.Focus();
            
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDesc.Focus();
            
        }

        private void ddlPtyTp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPtyCd.Focus();
            
        }

        private void ddlPtyCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
                cmbSecDef.Focus();
            
        }

        private void cmbSecDef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ddlRemTp.Focus();

        }

        private void txtCode_Leave(object sender, EventArgs e)
        {
            txtDesc.Text = "";
            RemitanceSumHeading _remHead = CHNLSVC.Financial.GetRemitanceData(Convert.ToString(cmbSec.SelectedValue), txtCode.Text);
            if (_remHead != null)
            {
                txtDesc.Text = _remHead.Rsd_desc;
                if (_remHead.Rsd_fixed == 1)
                {
                    chkFix.Checked = true;
                }
                else
                {
                    chkFix.Checked = false;
                }
                if (_remHead.Rsd_stus == "A")
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (_remHead.RSD_IS_ONCE == 1)
                {
                    chkOnce.Checked = true;
                }
                else
                {
                    chkOnce.Checked = false;
                }
            }
        }

        private void ImgBtnTp_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PartyType);
            DataTable _result = CHNLSVC.CommonSearch.Get_Party_Types(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPtyTp;
            _CommonSearch.txtSearchbyword.Text = txtPtyTp.Text;
            _CommonSearch.ShowDialog();
            txtPtyTp.Focus();
            txtPtyTp_Leave(null, null);
            
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.PartyType:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PartyCode:
                    {
                        paramsText.Append(txtPtyTp.Text + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void ImgBtnCd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPtyTp.Text))
            {
                MessageBox.Show("Please select the party type", "Remitance Heading", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PartyCode);
                DataTable _result = CHNLSVC.CommonSearch.Get_Party_Codes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPtyCd;
                _CommonSearch.txtSearchbyword.Text = txtPtyCd.Text;
                _CommonSearch.ShowDialog();
                txtPtyCd.Focus();
            }
        }

        private void txtPtyTp_Leave(object sender, EventArgs e)
        {
            lblTpDesc.Text = "";
            DataTable dt = CHNLSVC.General.Get_Party_Types(txtPtyTp.Text);
            if (dt.Rows.Count > 0)
            {
                lblTpDesc.Text = dt.Rows[0]["rpt_desc"].ToString();
            }
            BindRemSumLimitation();

        }

        private void txtPtyTp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                ImgBtnTp_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtPtyCd.Focus();

        }

        private void txtPtyCd_Leave(object sender, EventArgs e)
        {
            lblCdDesc.Text = "";
            if (!string.IsNullOrEmpty(txtPtyCd.Text) && !string.IsNullOrEmpty(txtPtyTp.Text))
            {
                if (!string.IsNullOrEmpty(lblTpDesc.Text))
                {
                    DataTable dt = CHNLSVC.General.Get_Partycodes(txtPtyTp.Text, txtPtyCd.Text);
                    if (dt.Rows.Count > 0)
                    {
                        lblCdDesc.Text = dt.Rows[0]["Description"].ToString();
                    }
                }
            }
        }

        private void txtPtyTp_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ImgBtnTp_Click(null, null);
        }

        private void txtPtyCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                ImgBtnCd_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                cmbSecDef.Focus();
        }

        private void txtPtyCd_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ImgBtnCd_Click(null, null);
        }
    }
}


