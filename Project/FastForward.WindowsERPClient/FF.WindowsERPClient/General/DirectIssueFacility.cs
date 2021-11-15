using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace FF.WindowsERPClient.General
{
    public partial class DirectIssueFacility : FF.WindowsERPClient.Base
    {
        List<FF.BusinessObjects.DirectIssueLocation> _issue = null;
        public DirectIssueFacility()
        { InitializeComponent(); _issue = new List<FF.BusinessObjects.DirectIssueLocation>(); }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFromLoc.Text))
            { MessageBox.Show("Please select the from location", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); txtFromLoc.Clear(); return; }
            if (string.IsNullOrEmpty(txtToLoc.Text) && string.IsNullOrEmpty(txtToCat3.Text))
            { MessageBox.Show("Please select the to location or category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); txtToLoc.Clear(); txtToCat3.Clear(); return; }
            if (string.IsNullOrEmpty(txtToCat3.Text) && radCatWise.Checked)
            { MessageBox.Show("Selected location does not having a category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); txtToCat3.Clear(); return; }
            if (string.IsNullOrEmpty(txtToLoc.Text) && radLocWise.Checked)
            { MessageBox.Show("Please select the to-location", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); txtToLoc.Clear(); return; }
            var _chk = from DataGridViewRow r in gvAssign.Rows where Convert.ToString(r.Cells["sclt_to_cat"].Value) == (radCatWise.Checked ? txtToCat3.Text.Trim() : txtToLoc.Text.Trim()) && Convert.ToString(r.Cells["sclt_allow"].Value) == chkActive.Text.Trim() select r;
            if (_chk != null && _chk.Count() > 0)
            { MessageBox.Show("The record is already avaialble in " + (chkActive.Checked ? "active " : "inactive ") + " status", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (_issue != null && _issue.Count > 0) { var _chk2 = _issue.Where(x => x.Sclt_frm_loc.Trim() == txtFromLoc.Text.Trim() && x.Sclt_to_com.Trim() == txtToCompany.Text.Trim() && x.Sclt_to_cat.Trim() == (radCatWise.Checked ? txtToCat3.Text.Trim() : txtToLoc.Text.Trim())).ToList(); if (_chk2 != null && _chk2.Count() > 0) { MessageBox.Show("The record is available", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; } }
            FF.BusinessObjects.DirectIssueLocation _one = new BusinessObjects.DirectIssueLocation();
            _one.Sclt_allow = chkActive.Checked; _one.Sclt_cre_by = BaseCls.GlbUserID; _one.Sclt_cre_dt = DateTime.Now.Date; _one.Sclt_frm_com = BaseCls.GlbUserComCode; _one.Sclt_frm_loc = txtFromLoc.Text.Trim(); _one.Sclt_mod_by = BaseCls.GlbUserID;
            _one.Sclt_mod_dt = DateTime.Now.Date; _one.Sclt_module = "AODOUT_DIRECT"; _one.Sclt_to_cat = radCatWise.Checked ? txtToCat3.Text.Trim() : txtToLoc.Text.Trim(); _one.Sclt_to_com = txtToCompany.Text.Trim();
            _issue.Add(_one);
            gvCurrent.AutoGenerateColumns = false;
            gvCurrent.DataSource = new List<FF.BusinessObjects.DirectIssueLocation>();
            gvCurrent.DataSource = _issue;
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            switch (_type)
            { case CommonUIDefiniton.SearchUserControlType.Location: { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; } case CommonUIDefiniton.SearchUserControlType.ToLocation: { paramsText.Append(txtToCompany.Text.Trim() + seperator); break; } case CommonUIDefiniton.SearchUserControlType.LocationCat3: { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; } case CommonUIDefiniton.SearchUserControlType.Company: { paramsText.Append(seperator); break; } default: break; }
            return paramsText.ToString();
        }
        private void SystemErrorMessage(Exception ex)
        { CHNLSVC.CloseChannel(); this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        private void LoadLocation(TextBox _txt, CommonUIDefiniton.SearchUserControlType _type)
        {
            if (CommonUIDefiniton.SearchUserControlType.ToLocation == _type && string.IsNullOrEmpty(txtToCompany.Text))
            { MessageBox.Show("Please select the To Company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); txtToCompany.Clear(); txtToCompany.Focus(); return; }
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch(); _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(_type); DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result; _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = _txt; _CommonSearch.ShowDialog();
                _txt.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void btnSearch_FromLocation_Click(object sender, EventArgs e)
        { LoadLocation(txtFromLoc, CommonUIDefiniton.SearchUserControlType.Location); }
        private void btnSearch_ToLocation_Click(object sender, EventArgs e)
        { LoadLocation(txtToLoc, CommonUIDefiniton.SearchUserControlType.ToLocation); }
        private void btnSearch_ToCat3_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0; _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.LocationCat3);
                DataTable _result = CHNLSVC.CommonSearch.GetCategory3(_CommonSearch.SearchParams, null, null); _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result); _CommonSearch.obj_TragetTextBox = txtToCat3;
                _CommonSearch.ShowDialog(); txtToCat3.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void txtFromLoc_MouseDoubleClick(object sender, MouseEventArgs e)
        { LoadLocation(txtFromLoc, CommonUIDefiniton.SearchUserControlType.Location); }
        private void txtToLoc_MouseDoubleClick(object sender, MouseEventArgs e)
        { LoadLocation(txtToLoc, CommonUIDefiniton.SearchUserControlType.ToLocation); }
        private void txtToCat3_MouseDoubleClick(object sender, MouseEventArgs e)
        { btnSearch_ToCat3_Click(null, null); }
        private void txtFromLoc_KeyUp(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.F2) LoadLocation(txtFromLoc, CommonUIDefiniton.SearchUserControlType.Location); if (e.KeyCode == Keys.Enter) txtToCompany.Focus(); }
        private void txtToLoc_KeyUp(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.F2) LoadLocation(txtToLoc, CommonUIDefiniton.SearchUserControlType.ToLocation); if (e.KeyCode == Keys.Enter) if (txtToCat3.Enabled) txtToCat3.Focus(); else btnAdd.Focus(); }
        private void txtToCat3_KeyUp(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.F2) btnSearch_ToCat3_Click(null, null); if (e.KeyCode == Keys.Enter) btnAdd.Focus(); }
        private void txtToLoc_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtToLoc.Text)) { txtToCat3.Enabled = true; btnSearch_ToCat3.Enabled = true; return; } else { txtToCat3.Clear(); btnSearch_ToCat3.Enabled = false; txtToCat3.Enabled = false; }
            if (txtFromLoc.Text.Trim() == txtToLoc.Text.Trim())
            { MessageBox.Show("You can not assign same location as receiving location", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtToCompany.Text))
            { MessageBox.Show("Please select the To Company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); txtToCompany.Clear(); txtToCompany.Focus(); return; }
            FF.BusinessObjects.MasterLocation _chk = CHNLSVC.General.GetLocationByLocCode(txtToCompany.Text.Trim(), txtToLoc.Text.Trim());
            if (_chk == null || String.IsNullOrEmpty(_chk.Ml_com_cd))
            { MessageBox.Show("Please check the location code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); txtToLoc.Clear(); return; }
            txtToCat3.Text = _chk.Ml_cate_3;
            loadCategory3Location(txtToCat3.Text.Trim());
        }
        private void txtToCat3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtToCat3.Text)) return;
            if (string.IsNullOrEmpty(txtToCompany.Text))
            { MessageBox.Show("Please select the To Company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); txtToCompany.Clear(); txtToCompany.Focus(); return; }
            DataTable _cu = CHNLSVC.General.GetLocationCategory3();
            var _chk = _cu.AsEnumerable().Where(X => X.Field<string>("rlc3_cd") == txtToCat3.Text.Trim()).ToList();
            if (_chk == null || _chk.Count <= 0)
            { MessageBox.Show("Please check the category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); txtToCat3.Clear(); return; }
            loadCategory3Location(txtToCat3.Text.Trim());
        }
        private void txtFromLoc_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFromLoc.Text)) return;
            FF.BusinessObjects.MasterLocation _chk = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtFromLoc.Text.Trim());
            if (_chk == null || String.IsNullOrEmpty(_chk.Ml_com_cd)) { MessageBox.Show("Please check the location code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); txtFromLoc.Clear(); return; }
        }
        private void loadCategory3Location(string _cat3)
        {
            DataTable _tbl = CHNLSVC.Inventory.GetLocationByCat3(_cat3, txtToCompany.Text.Trim()); var _G = _tbl.AsEnumerable().OrderBy(X => X.Field<string>("ml_loc_cd"));
            foreach (DataRow _c in _G)
            { ListViewItem item = new ListViewItem(); item.Text = _c.Field<string>("ml_loc_cd"); item.ImageIndex = 0; this.lstView.Items.Add(item); }
            LoadAssignPermission();
        }
        private void LoadAssignPermission()
        { DataTable _tbl = CHNLSVC.Inventory.GetLocationTransaction(BaseCls.GlbUserComCode, txtFromLoc.Text.Trim()); gvAssign.AutoGenerateColumns = false; gvAssign.DataSource = _tbl; }
        private void chkActive_CheckedChanged(object sender, EventArgs e)
        { if (chkActive.Checked) chkActive.Text = "Active"; else chkActive.Text = "Inactive"; }
        private void btnSearch_ToCompany_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0; _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(_CommonSearch.SearchParams, null, null); _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result); _CommonSearch.obj_TragetTextBox = txtToCompany;
                _CommonSearch.ShowDialog(); txtToCompany.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void txtToCompany_MouseDoubleClick(object sender, MouseEventArgs e)
        { btnSearch_ToCompany_Click(null, null); }
        private void txtToCompany_KeyUp(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.F2) btnSearch_ToCompany_Click(null, null); if (e.KeyCode == Keys.Enter) txtToLoc.Focus(); }
        private void txtToCompany_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtToCompany.Text)) return;
            bool _isThatSo = CHNLSVC.General.CheckCompany(txtToCompany.Text.Trim());
            if (!_isThatSo) { MessageBox.Show("Selected company is invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); txtToCompany.Clear(); return; }
        }
        private void btnGrant_Click(object sender, EventArgs e)
        {
            if (gvCurrent.RowCount <= 0)
            { MessageBox.Show("Please select the record", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            string _error = string.Empty;
            int _i = CHNLSVC.Inventory.SaveDirectIssue(_issue, out _error);
            if (!string.IsNullOrEmpty(_error)) MessageBox.Show(_error);
            else { MessageBox.Show("Successfuly updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); btnClear_Click(null, null); }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFromLoc.Clear(); txtToLoc.Clear(); txtToCompany.Clear(); txtToCat3.Clear(); txtToCat3.Enabled = true; btnSearch_ToCat3.Enabled = true; chkActive.Checked = true;
            gvAssign.DataSource = new DataTable(); _issue = new List<BusinessObjects.DirectIssueLocation>(); gvCurrent.DataSource = _issue; lstView.Clear(); radCatWise.Checked = true;
        }
    }
}
