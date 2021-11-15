using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.General
{
    public partial class ServiceAgentDefinition : FF.WindowsERPClient.Base
    {
        /*
         * Originally Written by Prabhath on 24/02/2014
         * Format taken from POS Screen
         * Modify History
         * User                 Date                    code
         */


        DataTable _category = null;
        bool _isInit = false;
        public ServiceAgentDefinition()
        {
            InitializeComponent();
            _isInit = true;
            _category = new DataTable();
            LoadCategory();
            _isInit = false;
            ucLoactionSearch1.Company = BaseCls.GlbUserComCode;

        }
        private void SystemErrorMessage(Exception ex)
        {
            CHNLSVC.CloseChannel();
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void ClearScreen(bool isAgent)
        {
            //Clear the screen
            txtAdd1.Clear();
            txtAdd2.Clear();
            if (isAgent) txtAgent.Clear();
            txtContPerson.Clear();
            txtCordinator.Clear();
            txtFax.Clear();
            txtMappedLoc.Clear();
            txtName.Clear();
            txtRemarks.Clear();
            txtTown.Clear();
            lblMapped.Text = string.Empty;
            chkActive.Checked = true;
            chkActive.Text = "Active";
            cmbCategory.SelectedIndex = _count;
        }
        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            //Set the caption of the check box
            if (chkActive.Checked) chkActive.Text = "Active";
            else chkActive.Text = "Inactive";
        }
        private int _count = 0;
        private void LoadCategory()
        {
            //Load Category
            _category = CHNLSVC.General.GetAgentCategory();
            _category.Rows.Add("");
            cmbCategory.DisplayMember = "sac_desn";
            cmbCategory.ValueMember = "sac_cd";
            cmbCategory.DataSource = _category;
            _count = Convert.ToInt32(_category.Rows.Count - 1);
            cmbCategory.SelectedIndex = _count;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearScreen(true);
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.ServiceAgent:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void btnSearch_ServiceAgent_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceAgent);
            DataTable _result = CHNLSVC.CommonSearch.GetServiceAgent(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAgent;
            _CommonSearch.ShowDialog();
            txtAgent.Select();
        }
        private void txtAgent_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAgent.Text)) return;

            List<MasterBusinessEntity> _com = CHNLSVC.Sales.GetCustomerDetailList(BaseCls.GlbUserComCode, txtAgent.Text.Trim(), string.Empty, string.Empty, "SA");
            if (_com == null || _com.Count <= 0)
            {
                ClearScreen(false);
                gvUserPC.DataSource = null;
                return;
            }

            txtAdd1.Text = _com[0].Mbe_add1;
            txtAdd2.Text = _com[0].Mbe_add2;
            txtContPerson.Text = _com[0].Mbe_cr_add1;
            txtCordinator.Text = _com[0].Mbe_cr_add2;
            txtFax.Text = _com[0].Mbe_fax;
            // txtMappedLoc.Text = _com[0].Mbe_cust_loc;
            txtName.Text = _com[0].Mbe_name;
            txtRemarks.Text = _com[0].Mbe_wr_add1;
            txtTown.Text = _com[0].Mbe_town_cd;
            cmbCategory.SelectedValue = _com[0].Mbe_cate;
            txtTel.Text = _com[0].Mbe_tel;
            if (_com[0].Mbe_act) chkActive.Checked = true; else chkActive.Checked = false;
            txtMappedLoc.Text = _com[0].Mbe_acc_cd;

            LoadLoc();

        }
        private void txtTel_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTel.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtTel.Text.Trim());
                if (_isValid == false)
                {
                    MessageBox.Show("Invalid telephone number.", "Service Agent", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtTel.Focus();
                    return;
                }
            }
        }
        private void txtFax_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFax.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtFax.Text.Trim());
                if (_isValid == false)
                {
                    MessageBox.Show("Invalid fax number.", "Service Agent", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtFax.Focus();
                    return;
                }
            }
        }
        private void btnSearch_Town_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
            DataTable _result = CHNLSVC.CommonSearch.GetTown(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtTown;
            _CommonSearch.ShowDialog();
            txtTown.Select();

        }
        private void btnSearch_MappedLoc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMappedLoc;
                _CommonSearch.ShowDialog();
                txtMappedLoc.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void txtMappedLoc_Leave(object sender, EventArgs e)
        {
            lblMapped.Text = string.Empty;
            if (string.IsNullOrEmpty(txtMappedLoc.Text)) return;

            DataTable _tbl = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, txtMappedLoc.Text);
            if (_tbl == null || _tbl.Rows.Count <= 0)
            {
                MessageBox.Show("Please check the location code which you going to allocate for the mapped location.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMappedLoc.Clear();
                txtMappedLoc.Focus();
                return;
            }

            lblMapped.Text = _tbl.Rows[0].Field<string>("ml_loc_desc");

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool _isRecall = false;
            DataTable _recall = new DataTable();
            if (!string.IsNullOrEmpty(txtAgent.Text))
            {
                _isRecall = true;
                _recall = CHNLSVC.General.SearchServiceAgent(BaseCls.GlbUserComCode, txtAgent.Text.Trim());
                if (_recall != null && _recall.Rows.Count > 0)
                    _isRecall = true;
                else _isRecall = false;
            }
            else _isRecall = false;

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please select the agent name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtAdd1.Text))
            {
                MessageBox.Show("Please select the address", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtAdd2.Text))
                txtAdd2.Text = ".";

            if (string.IsNullOrEmpty(Convert.ToString(cmbCategory.SelectedValue)))
            {
                MessageBox.Show("Please select the agent category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable _mstComp = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            if (_mstComp.Rows[0]["Mc_anal24"].ToString() == "DIRIYA")   //ABL.LRP
            {
                if (string.IsNullOrEmpty(txtMappedLoc.Text))
                {
                    MessageBox.Show("Please select the mapped location", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            MasterBusinessEntity _one = new MasterBusinessEntity();
            _one.Mbe_act = chkActive.Checked;
            _one.Mbe_add1 = txtAdd1.Text.Trim();
            _one.Mbe_add2 = txtAdd2.Text.Trim();
            _one.Mbe_cate = Convert.ToString(cmbCategory.SelectedValue);
            _one.Mbe_cd = txtAgent.Text;
            _one.Mbe_com = BaseCls.GlbUserComCode;
            _one.Mbe_contact = txtTel.Text.Trim();
            _one.Mbe_cr_add1 = txtContPerson.Text.Trim();
            _one.Mbe_cr_add2 = txtCordinator.Text.Trim();
            _one.Mbe_fax = txtFax.Text.Trim();
            //   _one.Mbe_cust_loc = txtMappedLoc.Text.Trim();
            _one.Mbe_name = txtName.Text.Trim();
            _one.Mbe_wr_add1 = txtRemarks.Text;
            _one.Mbe_town_cd = txtTown.Text.Trim();
            _one.Mbe_tp = "SA";
            _one.Mbe_tel = txtTel.Text.Trim();
            _one.Mbe_nationality = "SL";
            _one.Mbe_acc_cd = txtMappedLoc.Text.Trim();
            int _effect = 0;
            string _cusCode = string.Empty;
            List<MasterBusinessEntityInfo> _infor = new List<MasterBusinessEntityInfo>();

            try
            {
                if (!_isRecall)
                {
                    _effect = CHNLSVC.Sales.SaveServiceAgentDetail(_one);
                    if (_effect > 0)
                        MessageBox.Show("Successfully Agent " + _cusCode + "Created!. ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _one.Mbe_cd = txtAgent.Text.Trim();
                    _cusCode = txtAgent.Text.Trim();
                    _effect = CHNLSVC.Sales.UpdateBusinessEntityProfile(_one, BaseCls.GlbUserID, Convert.ToDateTime(DateTime.Today).Date, 0, _infor, null);
                    if (_effect > 0)
                        MessageBox.Show("Successfully Updated!. ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (_effect <= 0)
                    MessageBox.Show("Process terminated!. ", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearScreen(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInit) return;
            if (string.IsNullOrEmpty(Convert.ToString(cmbCategory.SelectedValue))) return;
            DataTable _mstComp = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            if (_mstComp.Rows[0]["Mc_anal24"].ToString() == "DIRIYA")   //ABL.LRP
            {
                string _selectOne = Convert.ToString(cmbCategory.SelectedValue);
                var _ho = _category.AsEnumerable().Where(x => x.Field<string>("sac_cd") == _selectOne).Select(x => x.Field<ValueType>("sac_is_ho")).ToList();
                if (_ho != null && _ho.Count > 0)
                {
                    foreach (var n in _ho)
                    {
                        if (Convert.ToInt32(n) == 0)
                            btnSave.Enabled = false;
                        else
                            btnSave.Enabled = true;
                        break;
                    }
                }
            }

        }
        private void txtAgent_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtName.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_ServiceAgent_Click(null, null);
        }
        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAdd1.Focus();
        }
        private void txtAdd1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAdd2.Focus();
        }
        private void txtAdd2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtTel.Focus();
        }
        private void txtTel_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtFax.Focus();
        }
        private void txtFax_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtTown.Focus();
        }
        private void txtTown_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbCategory.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_Town_Click(null, null);
        }
        private void cmbCategory_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtContPerson.Focus();

        }
        private void txtContPerson_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCordinator.Focus();
        }
        private void txtCordinator_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRemarks.Focus();
        }
        private void txtRemarks_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtMappedLoc.Focus();
        }
        private void txtMappedLoc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Select();
            if (e.KeyCode == Keys.F2)
                btnSearch_MappedLoc_Click(null, null);
        }
        private void txtAgent_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_ServiceAgent_Click(null, null);
        }
        private void txtTown_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Town_Click(null, null);
        }
        private void txtMappedLoc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_MappedLoc_Click(null, null);
        }

        private void btnAddLocs_Click(object sender, EventArgs e)
        {
            try
            {
                string com = ucLoactionSearch1.Company;
                string chanel = ucLoactionSearch1.Channel;
                string subChanel = ucLoactionSearch1.SubChannel;
                string area = ucLoactionSearch1.Area;
                string region = ucLoactionSearch1.Regien;
                string zone = ucLoactionSearch1.Zone;
                string pc = ucLoactionSearch1.ProfitCenter;

                DataTable dt = CHNLSVC.Inventory.GetLOC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
                // select_LOC_List.Merge(dt);
                grvLocs.DataSource = null;
                grvLocs.AutoGenerateColumns = false;
                grvLocs.DataSource = dt;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void LoadLoc()
        {
            gvUserPC.DataSource = null;
            gvUserPC.AutoGenerateColumns = false;
            gvUserPC.DataSource = CHNLSVC.Inventory.GetLocsByAgent(BaseCls.GlbUserComCode, txtAgent.Text);
        }

        private void btn_AddPC_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAgent.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a agent", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //-------------------------------------------------------------
                Boolean _isAgent = CHNLSVC.Sales.IsCheckServiceAgent(BaseCls.GlbUserComCode, txtAgent.Text);
                if (_isAgent == false)
                {
                    MessageBox.Show("Invalid service agent!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //----------------------------------------------------------------
                List<RCCLocations> loc_list = GetSelectedLocationList();
                if (loc_list.Count < 1)
                {
                    MessageBox.Show("Please select location(s)", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Int32 eff = CHNLSVC.Inventory.UpdateRCCLocations(loc_list);
                MessageBox.Show("Successfully Updated!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLoc();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private List<RCCLocations> GetSelectedLocationList()
        {
            List<RCCLocations> list = new List<RCCLocations>();
            foreach (DataGridViewRow dgvr in grvLocs.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    RCCLocations _rccLoc = new RCCLocations();
                    _rccLoc.Ragl_agent = txtAgent.Text;
                    _rccLoc.Ragl_com = ucLoactionSearch1.Company;
                    _rccLoc.Ragl_loc = dgvr.Cells["LOCATION"].Value.ToString();
                    _rccLoc.Ragl_mod_by = BaseCls.GlbUserID;

                    list.Add(_rccLoc);
                }
            }
            return list;
        }

        private void btn_DelPC_Click(object sender, EventArgs e)
        {
            try
            {
                List<RCCLocations> delete_list = GetSelectedLocationList_toUpdate();
                if (delete_list.Count == 0)
                {
                    MessageBox.Show("Please select locations to delete!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtAgent.Text == "")
                {
                    MessageBox.Show("Please select agent!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int row_arr = CHNLSVC.Inventory.DeleteRCCLocation(delete_list);
                MessageBox.Show("Successfully deleted!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLoc();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private List<RCCLocations> GetSelectedLocationList_toUpdate()
        {
            List<RCCLocations> list = new List<RCCLocations>();
            foreach (DataGridViewRow dgvr in gvUserPC.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    RCCLocations _rccLoc = new RCCLocations();
                    _rccLoc.Ragl_loc = dgvr.Cells["p_SUP_PC_CD"].Value.ToString();
                    _rccLoc.Ragl_com = BaseCls.GlbUserComCode;
                    _rccLoc.Ragl_agent = txtAgent.Text;
                    _rccLoc.Ragl_mod_by = BaseCls.GlbUserID;

                    list.Add(_rccLoc);
                }
            }
            return list;
        }

        private void chkAll_Itm_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAll_Itm.Checked == true)
                {
                    foreach (DataGridViewRow row in grvLocs.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    grvLocs.EndEdit();

                }
                else
                {
                    foreach (DataGridViewRow row in grvLocs.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = false;
                    }
                    grvLocs.EndEdit();

                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
