//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using FF.BusinessObjects;
using System.Threading;


namespace FF.WindowsERPClient.Services
{
    public partial class ServiceDefinitions : Base
    {
        string _searchType = "";
        string _scp_tp = "";
        string _sc_desc = "";
        string _sc_pty_tp = "";
        string _type = "";
        int isDefault = 0;
        ServiceTaskLocations obj_items;
        List<ServiceTaskLocations> _lstAllocateItems;

        scv_prit_task obj_task;
        MasterServiceEmployee obj_Emp;
        List<scv_prit_task> _lstPrioTask;
        List<MasterServiceEmployee> _lstEmp;

        public ServiceDefinitions()
        {
            InitializeComponent();
            bind_Combo_Types();
            load_service_cat();
            LoadUtilitiDetails();
            load_priority();
        }

        private void load_priority()
        {
            DataTable dt = CHNLSVC.CustService.getPriorityData();
            grvPrio.AutoGenerateColumns = false;
            grvPrio.DataSource = dt;
        }

        private void load_service_cat()
        {
            DataTable dtgrdload = CHNLSVC.CustService.LoadCat();
            dgrmaintypesload.AutoGenerateColumns = false;
            dgrmaintypesload.DataSource = dtgrdload;
            int rowcount = 0;
            foreach (DataGridViewRow dgvr in dgrmaintypesload.Rows)
            {

                if (dgrmaintypesload.Rows[rowcount].Cells["Status"].Value.ToString() == "Deactive")
                {
                    dgrmaintypesload.Rows[rowcount].Cells["select"].ReadOnly = true;
                }
                rowcount++;
            }

        }

        private void bind_Combo_Types()
        {
            DataTable dttype = CHNLSVC.CustService.LoadTypes();
            if (dttype.Rows.Count > 0)
            {
                cmbtype.DataSource = dttype;
                cmbtype.DisplayMember = "SCD_DESC";
                cmbtype.ValueMember = "SCD_TP";
                cmbtype.Text = "--Select--";
            }
            else
            {
                cmbtype.DataSource = null;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ?", "Service Definitions", MessageBoxButtons.YesNo) == DialogResult.No) return;
            List<ServiceTaskLocations> _tsk_loc = new List<ServiceTaskLocations>();
            ServiceTaskLocations _serdif = new ServiceTaskLocations();
            _serdif.Is_active = 0;
            if (txtCode.Text.Trim() != string.Empty)
            {
                _serdif.Code = txtCode.Text.Trim();
            }
            else
            {
                MessageBox.Show("Definition Code Empty", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCode.Focus();
                return;
            }
            if (txtDescription.Text.Trim() != string.Empty)
            {
                _serdif.Description = txtDescription.Text.Trim();
            }
            else
            {
                MessageBox.Show("Code Definition  Empty", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescription.Focus();
                return;
            }
            if (cmbtype.Text != "--Select--")
            {
                _serdif.Type = cmbtype.SelectedValue.ToString();
            }
            else
            {
                MessageBox.Show("Select Direction", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbtype.Focus();
                return;
            }
            if (chkActive.Checked == true) _serdif.Is_active = 1;
            _tsk_loc.Add(_serdif);
            Int32 _eff = CHNLSVC.CustService.UpdateTaskType(_serdif.Code, _serdif.Description, _serdif.Type, _serdif.Is_active, BaseCls.GlbUserID);
            if (_eff == 1)
            {
                MessageBox.Show("Successfully Saved", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset_Controls();
                load_service_cat();
            }
            else if (_eff == 2)
            {
                MessageBox.Show("Successfully Updated", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load_service_cat();
            }
            else
            {
                MessageBox.Show("Process Error", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.CustGrade:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustSatis:
                    {
                        paramsText.Append(txtQCode.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustQuest:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Designation:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EmployeeAll:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServicePriority:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobStage:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                //    {
                //        paramsText.Append(txttpt_maincat.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                //        break;

                //    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        paramsText.Append(string.Empty + seperator + "Promotion" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Area:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UtiliLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SerDef_Code:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PayCircular:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Zone:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Region:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankALL:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.GPC:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GenDiscount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void btnSrchChannel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChanel;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtChanel.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSrhScha_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtChanel.Text))
                {
                    MessageBox.Show("Please select channel.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSChanel.Text = "";
                    txtChanel.Focus();
                    return;
                }
                _searchType = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSChanel;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtSChanel.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSrhLocation_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtPC.Select();

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void cmbCommDef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCommDef.Text == "Sub Channel")
            {
                txtPC.Enabled = false;
                btnSrhLocation.Enabled = false;
                txtChanel.Enabled = true;
                btnSrchChannel.Enabled = true;
                txtSChanel.Enabled = true;
                btnSrhScha.Enabled = true;
                txtChanel.Focus();
            }
            else if (cmbCommDef.Text == "Location")
            {
                txtChanel.Enabled = false;
                btnSrchChannel.Enabled = false;
                txtSChanel.Enabled = false;
                btnSrhScha.Enabled = false;
                txtPC.Enabled = true;
                btnSrhLocation.Enabled = true;
                txtPC.Focus();
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            Boolean _isFound = false;
            if (cmbCommDef.Text == "")
            {
                MessageBox.Show("Please select a type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbCommDef.Focus();
                return;
            }
            try
            {

                Base _basePage = new Base();

                if (cmbCommDef.Text == "Location")
                {
                    DataTable dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep_all(BaseCls.GlbUserComCode, txtChanel.Text, txtSChanel.Text, null, null, null, txtPC.Text);
                    foreach (DataRow drow in dt.Rows)
                    {
                        if (lstPC.Items.Count == 0) lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                        foreach (ListViewItem Item in lstPC.Items)
                        {
                            if (Item.Text == drow["PROFIT_CENTER"].ToString())
                            {
                                Item.Checked = false;
                                _isFound = true;
                            }
                        }
                        if (_isFound == false)
                        {
                            lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                        }
                        _isFound = false;
                    }
                }
                else
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _searchType = "";
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (!string.IsNullOrEmpty(txtSChanel.Text))
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtSChanel.Text);
                        foreach (DataRow drow in _result.Rows)
                        {
                            if (lstPC.Items.Count == 0) lstPC.Items.Add(drow["CODE"].ToString());
                            foreach (ListViewItem Item in lstPC.Items)
                            {
                                if (Item.Text == drow["CODE"].ToString())
                                    _isFound = true;
                            }
                            if (_isFound == false)
                            {
                                lstPC.Items.Add(drow["CODE"].ToString());
                            }
                            _isFound = false;
                        }
                    }
                    else
                    {
                        foreach (DataRow drow in _result.Rows)
                        {
                            if (lstPC.Items.Count == 0) lstPC.Items.Add(drow["CODE"].ToString());
                            foreach (ListViewItem Item in lstPC.Items)
                            {
                                if (Item.Text == drow["CODE"].ToString())
                                {
                                    Item.Checked = false;
                                    _isFound = true;
                                }
                            }
                            if (_isFound == false)
                            {
                                lstPC.Items.Add(drow["CODE"].ToString());
                            }
                            _isFound = false;
                        }
                    }
                }
            A:
                foreach (ListViewItem Item in lstPC.Items)
                {
                    Item.Checked = false;
                }
                txtChanel.Text = "";
                txtSChanel.Text = "";
                txtPC.Text = "";
                txtPC.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = false;
            }
        }



        //private void lstPC_ItemChecked(object sender, ItemCheckedEventArgs e)
        //{
        //    foreach (ListViewItem Item in lstPC.Items)
        //    {
        //        if (Item.Checked == true)
        //        {
        //            string partycode = Item.Text;
        //            dgrloaddifinitions.Rows.Add();
        //            dgrloaddifinitions["Company", dgrloaddifinitions.Rows.Count - 1].Value = BaseCls.GlbUserComCode;
        //            dgrloaddifinitions["PartyType", dgrloaddifinitions.Rows.Count - 1].Value = cmbCommDef.Text.ToString();
        //            dgrloaddifinitions["PartyCode", dgrloaddifinitions.Rows.Count - 1].Value = partycode;
        //            dgrloaddifinitions["TaskLoc", dgrloaddifinitions.Rows.Count - 1].Value = "WorkShop";
        //        }
        //    }
        //}

        private void btnAddtogrid_Click(object sender, EventArgs e)
        {
            AddValueToGrid();
        }

        private void AddValueToGrid()
        {
            try
            {
                List<string> pcList = new List<string>();
                foreach (ListViewItem Item in lstPC.Items)
                {
                    string partycode = Item.Text;

                    if (Item.Checked == true)
                        pcList.Add(partycode);
                }
                if (pcList.Count == 0)
                {
                    MessageBox.Show("Please select the Sub channel/profit center which you have to assign.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (string _pc in pcList)
                {

                    foreach (DataGridViewRow dgvr in dgrloaddifinitions.Rows)
                    {
                        if (cmbCommDef.Text == Convert.ToString(dgvr.Cells["PartyType"].Value) && _pc == Convert.ToString(dgvr.Cells["PartyCode"].Value) && _type == Convert.ToString(dgvr.Cells["TaskLoc"].Value) && _scp_tp == Convert.ToString(dgvr.Cells["scs_tp"].Value))
                        {

                            MessageBox.Show("Thease values are Already Added.", "Item Allocation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    Base _basePage = new Base();
                    string type = cmbCommDef.Text;
                    if (type == "Location")
                    {
                        type = "LOC";
                    }
                    else if (type == "Sub Channel")
                    {
                        type = "SCHNL";
                    }
                    DataTable dtchkDefault = CHNLSVC.CustService.CheckDefault(BaseCls.GlbUserComCode, type, _pc);
                    if (dtchkDefault.Rows.Count > 0) isDefault = 1;
                    dgrloaddifinitions.Rows.Add();
                    dgrloaddifinitions["Company", dgrloaddifinitions.Rows.Count - 1].Value = BaseCls.GlbUserComCode;
                    dgrloaddifinitions["PartyType", dgrloaddifinitions.Rows.Count - 1].Value = cmbCommDef.Text.ToString();
                    dgrloaddifinitions["PartyCode", dgrloaddifinitions.Rows.Count - 1].Value = _pc;
                    dgrloaddifinitions["TaskLoc", dgrloaddifinitions.Rows.Count - 1].Value = _type;
                    dgrloaddifinitions["scs_tp", dgrloaddifinitions.Rows.Count - 1].Value = _scp_tp;
                    dgrloaddifinitions["Desc", dgrloaddifinitions.Rows.Count - 1].Value = _sc_desc;
                    dgrloaddifinitions["DefaultYesNo", dgrloaddifinitions.Rows.Count - 1].Value = "YES";
                    dgrloaddifinitions["ActiveYesNo", dgrloaddifinitions.Rows.Count - 1].Value = "YES";
                    dgrloaddifinitions["Active", dgrloaddifinitions.Rows.Count - 1].Value = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CHNLSVC.CloseChannel();
            }
            finally
            {

            }
        }



        private void lstPC_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked == false)
            {
                int rowcount = 0;
                foreach (DataGridViewRow dgvr in dgrloaddifinitions.Rows)
                {
                    if (dgvr.Cells["PartyCode"].Value.ToString() == e.Item.Text)
                    {
                        dgrloaddifinitions.Rows.RemoveAt(dgvr.Index);
                    }
                    rowcount++;
                }

            }
        }



        private void dgrmaintypesload_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgrmaintypesload.Rows.Count > 0)
            {
                var gotIt = dgrmaintypesload.Rows[e.RowIndex].Cells["Type"].Value;
                _type = Convert.ToString(dgrmaintypesload.Rows[e.RowIndex].Cells["Type"].Value);
                _scp_tp = Convert.ToString(dgrmaintypesload.Rows[e.RowIndex].Cells["scp_tp"].Value);
                _sc_desc = Convert.ToString(dgrmaintypesload.Rows[e.RowIndex].Cells["Description"].Value);

                cmbCommDef.Enabled = true;
                btnAddItem.Enabled = true;
            }





        }

        private void btnSaveCat_Click(object sender, EventArgs e)
        {

            if (dgrloaddifinitions.Rows.Count > 0)
            {
                _lstAllocateItems = new List<ServiceTaskLocations>();

                #region validation

                if (lstPC.Items.Count <= 0)
                {
                    MessageBox.Show("Please select the Sub channel/profit center which you have to assign", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion

                _lstAllocateItems = fillToAllocateItemDets();
                if (_lstAllocateItems.Count > 0)
                {
                    if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    string _error = "";
                    int result = CHNLSVC.CustService.Save_Allocated_Category(_lstAllocateItems, out _error);
                    if (result == -1)
                    {
                        MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Records inserted Successfully", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgrloaddifinitions.ReadOnly = true;
                        //btnClear_Click(null, null);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select Definition details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private List<ServiceTaskLocations> fillToAllocateItemDets()
        {
            _lstAllocateItems = new List<ServiceTaskLocations>();
            //save Item Cat Details
            foreach (DataGridViewRow dgvr in dgrloaddifinitions.Rows)
            {

                obj_items = new ServiceTaskLocations();

                obj_items.Com = BaseCls.GlbUserComCode;
                string type = cmbCommDef.Text;
                if (type == "Location")
                {
                    obj_items.Scs_pty_tp = "LOC";
                }
                else if (type == "Sub Channel")
                {
                    obj_items.Scs_pty_tp = "SCHNL";
                }
                else if (type == "Channel")
                {
                    obj_items.Scs_pty_tp = "CHNL";
                }

                obj_items.Com = dgvr.Cells["Company"].Value.ToString();
                obj_items.Scs_pty_code = dgvr.Cells["PartyCode"].Value.ToString();
                obj_items.Scs_tp = dgvr.Cells["scs_tp"].Value.ToString();
                if (Convert.ToBoolean(dgvr.Cells["Default"].Value) == true)
                {
                    obj_items.Is_default = 1;
                }
                else
                {
                    obj_items.Is_default = 0;
                }
                if (Convert.ToBoolean(dgvr.Cells["Active"].Value) == true)
                {
                    obj_items.Is_active = 1;
                }
                else
                {
                    obj_items.Is_active = 0;
                }
                obj_items.Create_by = BaseCls.GlbUserID;


                _lstAllocateItems.Add(obj_items);

            }


            if (_lstAllocateItems.Count <= 0)
            {
                MessageBox.Show("Please add definition details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return _lstAllocateItems;
        }

        private void btnSrchUtiliLoc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UtiliLocation);
                DataTable _result = CHNLSVC.CommonSearch.Get_Utilization_Location(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textLocation;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                textLocation.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textLocation_TextChanged(object sender, EventArgs e)
        {


        }

        private void bind_task_loc(string _uti_loc)
        {
            DataTable dttype = CHNLSVC.CustService.bind_task_loc(_uti_loc);
            if (dttype.Rows.Count > 0)
            {
                cmbutilitask.Enabled = true;
                cmbutilitask.DataSource = dttype;
                cmbutilitask.DisplayMember = "SC_DESC";
                cmbutilitask.ValueMember = "SC_TP";
                cmbutilitask.Text = "--Select--";
            }
            else
            {
                MessageBox.Show("This location not define as task location", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textLocation.Clear();
                textLocation.Focus();
                cmbutilitask.DataSource = null;
                return;
            }
        }

        private void btnUtilationSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ?", "Service Definitions", MessageBoxButtons.YesNo) == DialogResult.No) return;
            ServiceTaskLocations _serdif = new ServiceTaskLocations();
            if (textLocation.Text.Trim() != string.Empty)
            {
                _serdif.Location = textLocation.Text;
            }
            else
            {
                _serdif.Location = "";
            }
            if (cmbutilitask.Text != "--Select--")
            {
                if (cmbutilitask.Text.ToString() != null)
                {
                    _serdif.Type = cmbutilitask.SelectedValue.ToString();
                }
            }
            if (textMaxTerminal.Text.Trim() != string.Empty)
            {
                _serdif.Capacity = Convert.ToDecimal(textMaxTerminal.Text);
            }
            else
            {
                _serdif.Capacity = 0;
            }
            string _usr = BaseCls.GlbUserID;
            string _com = BaseCls.GlbUserComCode;
            int isActive = 1;
            if (chkUtiActive.Checked == false) isActive = 0;
            Int32 _eff = CHNLSVC.CustService.SaveUpdateUtilization(isActive, _com, _serdif.Location, _serdif.Capacity, _serdif.Type, _usr);
            LoadUtilitiDetails();
            if (_eff == 1)
            {
                MessageBox.Show("Successfully Saved", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset_Controls_Uilize();

            }
            else if (_eff == 2)
            {
                MessageBox.Show("Successfully Updated", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load_service_cat();
            }
            else
            {
                MessageBox.Show("Process Error", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadUtilitiDetails()
        {
            DataTable dtbl = CHNLSVC.CustService.GetUtilitiDetails(BaseCls.GlbUserComCode);
            grdUtiliTaskLoad.AutoGenerateColumns = false;
            grdUtiliTaskLoad.DataSource = new DataTable();
            grdUtiliTaskLoad.DataSource = dtbl;
        }

        private void dgrloaddifinitions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgrloaddifinitions.Rows.Count > 0)
            {
                int rowIndex = e.RowIndex;
                DataGridViewCheckBoxCell IsDefault = new DataGridViewCheckBoxCell();
                IsDefault = (DataGridViewCheckBoxCell)dgrloaddifinitions.Rows[dgrloaddifinitions.CurrentRow.Index].Cells["Default"];
                DataGridViewCheckBoxCell IsActive = new DataGridViewCheckBoxCell();
                IsActive = (DataGridViewCheckBoxCell)dgrloaddifinitions.Rows[dgrloaddifinitions.CurrentRow.Index].Cells["Active"];
                if (e.ColumnIndex == dgrloaddifinitions.Columns["DefaultYesNo"].Index && dgrloaddifinitions.Columns["DefaultYesNo"].ReadOnly == true)
                {
                    if (dgrloaddifinitions.Rows[Convert.ToInt32(e.RowIndex)].Cells["DefaultYesNo"].Value.ToString() == "NO")
                    {
                        dgrloaddifinitions.Rows[Convert.ToInt32(e.RowIndex)].Cells["DefaultYesNo"].Value = "YES";
                        string Com = dgrloaddifinitions.Rows[Convert.ToInt32(e.RowIndex)].Cells["Company"].Value.ToString();
                        string Party_Type = dgrloaddifinitions.Rows[Convert.ToInt32(e.RowIndex)].Cells["PartyType"].Value.ToString();
                        string Party_Code = dgrloaddifinitions.Rows[Convert.ToInt32(e.RowIndex)].Cells["PartyCode"].Value.ToString();
                        IsDefault.Value = true;
                        int row = 0;
                        foreach (DataGridViewRow dgvr in dgrloaddifinitions.Rows)
                        {
                            if (Party_Type == Convert.ToString(dgvr.Cells["PartyType"].Value) && Party_Code == Convert.ToString(dgvr.Cells["PartyCode"].Value) && Com == Convert.ToString(dgvr.Cells["Company"].Value) && rowIndex != row)
                            {

                                dgvr.Cells["DefaultYesNo"].Value = "NO";
                                dgvr.Cells["Default"].Value = false;
                            }
                            row++;
                        }
                    }
                    else
                    {
                        dgrloaddifinitions.Rows[Convert.ToInt32(e.RowIndex)].Cells["DefaultYesNo"].Value = "NO";
                        IsDefault.Value = false;
                    }
                }
                if (e.ColumnIndex == dgrloaddifinitions.Columns["ActiveYesNo"].Index)
                {
                    if (dgrloaddifinitions.Rows[Convert.ToInt32(e.RowIndex)].Cells["ActiveYesNo"].Value.ToString() == "YES")
                    {
                        IsActive.Value = false;
                        dgrloaddifinitions.Rows[Convert.ToInt32(e.RowIndex)].Cells["ActiveYesNo"].Value = "NO";
                    }
                    else
                    {
                        IsActive.Value = true;
                        dgrloaddifinitions.Rows[Convert.ToInt32(e.RowIndex)].Cells["ActiveYesNo"].Value = "YES";
                    }
                }
            }
        }

        private void dgrloaddifinitions_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int row = e.RowIndex;
            dgrloaddifinitions.Rows[row].Cells["DefaultYesNo"].Value = "NO";
            if (isDefault == 0 && row == 0)
            {
                dgrloaddifinitions.Rows[row].Cells["DefaultYesNo"].Value = "YES";
            }
            else if (isDefault == 1)
            {
                dgrloaddifinitions.Rows[row].Cells["DefaultYesNo"].ReadOnly = true;
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {

        }

        private void btnsrchCode_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SerDef_Code);
                DataTable _result = CHNLSVC.CommonSearch.Get_Service_Def_Code(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;

                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCode;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtCode.Select();
                Get_CatogeryDetails();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Get_CatogeryDetails()
        {
            string Cat_Code = txtCode.Text.Trim();
            txtCode.ReadOnly = true;
            DataTable dt_Cat_Det = CHNLSVC.CustService.GetCatogeryDetails(Cat_Code);
            DataRow dr_cat;
            if (dt_Cat_Det.Rows.Count > 0)
            {
                dr_cat = dt_Cat_Det.Rows[0];
                txtDescription.Text = dr_cat["SC_DESC"].ToString();
                //  txtDescription.ReadOnly = true;
                string typ = dr_cat["SC_DIRECT"].ToString();
                if (typ == "W") cmbtype.Text = "WORKSHOP";
                if (typ == "F") cmbtype.Text = "FIELD";
                // cmbtype.Enabled = false;
                int is_act = Convert.ToInt32(dr_cat["SC_ACT"].ToString());
                if (is_act == 1) chkActive.Checked = true;
                if (is_act == 0) chkActive.Checked = false;




                _type = cmbtype.Text;
                _scp_tp = txtCode.Text;
                _sc_desc = txtDescription.Text;

                cmbCommDef.Enabled = true;
                btnAddItem.Enabled = true;


            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Reset_Controls();
        }

        private void Reset_Controls()
        {
            txtCode.Text = "";
            txtCode.ReadOnly = false;
            txtCode.Focus();
            txtDescription.Text = "";
            txtDescription.ReadOnly = false;
            bind_Combo_Types();
            cmbtype.Enabled = true;
            chkActive.Checked = true;
        }
        private void Reset_Controls_Uilize()
        {
            textLocation.Text = "";
            textMaxTerminal.Text = "";
            chkUtiActive.Checked = true;
            bind_task_loc(textLocation.Text);
            textLocation.Focus();
        }
        private void Reset_Controls_Definition()
        {
            txtChanel.Text = "";
            txtSChanel.Text = "";
            txtPC.Text = "";
            lstPC.Clear();
            dgrloaddifinitions.Rows.Clear();

        }
        private void Reset_Controls_Definition_prio()
        {
            //txtTaskChnl.Text = "";
            //txtTaskSbChnl.Text = "";
            //txtTaskLoc.Text = "";
            lstPrio.Clear();
            //grvPrioTask.Rows.Clear();

        }
        private void txtCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtCod_DoubleClick(null, null);
            }

            else if (e.KeyCode == Keys.Enter)
            {

                cmbtype.Focus();
            }

        }

        private void txtCod_DoubleClick(object sender, EventArgs e)
        {
            btnsrchCode_Click(null, null);
        }

        private void grdUtiliTaskLoad_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgrloaddifinitions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnUtilationCancel_Click(object sender, EventArgs e)
        {
            Reset_Controls_Uilize();
        }

        private void btnPcClear_Click(object sender, EventArgs e)
        {
            Reset_Controls_Definition();
        }

        private void cmbutilitask_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSChanel.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                txtChanel_DoubleClick(null, null);
            }

        }

        private void txtSChannel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddItem.Focus();
            }
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddItem.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                txtPC_DoubleClick(null, null);
            }
        }

        private void textMaxTerminal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtChanel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPC_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSChanel_TextChanged(object sender, EventArgs e)
        {

        }

        private void textMaxTerminal_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                textLocation_DoubleClick(null, null);
            }
        }

        private void textLocation_DoubleClick(object sender, EventArgs e)
        {
            btnSrchUtiliLoc_Click(null, null);
        }

        private void txtChanel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChanel;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtChanel.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSChanel_DoubleClick(object sender, EventArgs e)
        {
            btnSrhScha_Click(null, null);
        }

        private void txtPC_DoubleClick(object sender, EventArgs e)
        {
            btnSrhLocation_Click(null, null);

        }

        private void txtSChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtSChanel_DoubleClick(null, null);
            }

        }

        private void chkDefault_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDefault.Checked == true)
            {

                for (int i = 0; i < dgrloaddifinitions.Rows.Count; i++)
                {

                    dgrloaddifinitions["DefaultYesNo", i].Value = "YES";
                }
            }
            else
            {
                for (int i = 0; i < dgrloaddifinitions.Rows.Count; i++)
                {
                    dgrloaddifinitions["DefaultYesNo", i].Value = "NO";
                }
            }
        }

        private void chkActiveDef_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActiveDef.Checked == true)
            {
                for (int i = 0; i < dgrloaddifinitions.Rows.Count; i++)
                {
                    dgrloaddifinitions["ActiveYesNo", i].Value = "YES";
                }
            }
            else
            {
                for (int i = 0; i < dgrloaddifinitions.Rows.Count; i++)
                {
                    dgrloaddifinitions["ActiveYesNo", i].Value = "NO";
                }
            }
        }

        private void textMaxTerminal_Leave(object sender, EventArgs e)
        {
            if (textMaxTerminal.Text.Trim() != "")
            {
                if (Convert.ToDecimal(textMaxTerminal.Text.Trim()) < 0)
                {
                    MessageBox.Show("Quantity should be positive value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void textMaxTerminal_KeyPress(object sender, KeyPressEventArgs e)
        {
            int isNumber = 0;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }

        private void txtPC_Leave(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtPC.Text))
                {
                    MasterLocation _masterLocation = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtPC.Text.ToString());
                    if (_masterLocation != null)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Invalid location code!", "Incorrect Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPC.Clear();
                        txtPC.Focus();
                        return;
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void txtSChanel_Leave(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                if (string.IsNullOrEmpty(txtSChanel.Text)) return;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtSChanel.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid sub channel ", "Service Parameters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSChanel.Clear();
                    txtSChanel.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void txtChanel_Leave(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                if (string.IsNullOrEmpty(txtChanel.Text)) return;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtChanel.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid channel", "Service Parameters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtChanel.Clear();
                    txtChanel.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void textLocation_Leave(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(textLocation.Text))
                {
                    bind_task_loc(textLocation.Text);


                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSrchColor_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.ShowDialog();

            txtColor.Text = dlg.Color.Name;
        }

        private void btnClearPrio_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtColor.Text) && string.IsNullOrEmpty(txtPrioCode.Text) && string.IsNullOrEmpty(txtPrioDesc.Text) && chkAct.Checked == false && chkDef.Checked == false)
            {
                MessageBox.Show("Already cleared !", "Service Parameters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
            clearAll();
        }

        private void clearAll()
        {
            txtColor.Text = "";
            txtPrioCode.Text = "";
            txtPrioDesc.Text = "";
            chkAct.Checked = false;
            chkDef.Checked = false;
        }

        private void btnSavePrio_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPrioCode.Text))
            {
                MessageBox.Show("Enter priority code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtPrioDesc.Text))
            {
                MessageBox.Show("Enter priority description", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtColor.Text))
            {
                MessageBox.Show("Enter color code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Are you sure ?", "Service Definitions", MessageBoxButtons.YesNo) == DialogResult.No) return;

            Int32 _eff = CHNLSVC.CustService.updatePriorityData(txtPrioCode.Text, txtPrioDesc.Text, Convert.ToInt32(chkAct.Checked), Convert.ToInt32(chkDef.Checked), txtColor.Text, BaseCls.GlbUserID);
            MessageBox.Show("Successfully Saved", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);

            clearAll();

            load_priority();
        }

        private void btnSrchPrio_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServicePriority);
            DataTable _result = CHNLSVC.CommonSearch.SearchScvPriority(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPrioCode;
            _CommonSearch.ShowDialog();
            txtPrioCode.Focus();

        }

        private void load_prio_data(string _code)
        {
            DataTable _dt = CHNLSVC.CustService.getPriorityDataByCode(_code);
            if (_dt.Rows.Count > 0)
            {
                txtPrioDesc.Text = _dt.Rows[0]["scp_desc"].ToString();
                txtColor.Text = _dt.Rows[0]["scp_color"].ToString();
                chkAct.Checked = Convert.ToBoolean(_dt.Rows[0]["scp_act"]);
                chkDef.Checked = Convert.ToBoolean(_dt.Rows[0]["scp_def"]);
            }
            else
            {
                txtPrioDesc.Text = "";
                txtColor.Text = "";
                chkAct.Checked = false;
                chkDef.Checked = false;
            }
        }

        private void txtCode_Leave(object sender, EventArgs e)
        {

        }

        private void txtPrioCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPrioCode.Text))
                load_prio_data(txtPrioCode.Text);
        }

        private void cmbTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTask.Text == "Sub Channel")
            {
                txtTaskLoc.Enabled = false;
                btnSrchTaskLoc.Enabled = false;
                txtTaskChnl.Enabled = true;
                btnSrchTaskChnl.Enabled = true;
                txtTaskSbChnl.Enabled = true;
                btnSrchTaskSbChnl.Enabled = true;
                txtTaskChnl.Focus();
            }
            else if (cmbTask.Text == "Location")
            {
                txtTaskChnl.Enabled = false;
                btnSrchTaskChnl.Enabled = false;
                txtTaskSbChnl.Enabled = false;
                btnSrchTaskSbChnl.Enabled = false;
                txtTaskLoc.Enabled = true;
                btnSrchTaskLoc.Enabled = true;
                txtTaskLoc.Focus();
            }
        }

        private void btnSrchTaskChnl_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtTaskChnl;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtTaskChnl.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSrchTaskSbChnl_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTaskChnl.Text))
                {
                    MessageBox.Show("Please select channel.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTaskSbChnl.Text = "";
                    txtTaskChnl.Focus();
                    return;
                }
                _searchType = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtTaskSbChnl;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtTaskSbChnl.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSrchTaskLoc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtTaskLoc;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtTaskLoc.Select();

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            Boolean _isFound = false;
            if (cmbTask.Text == "")
            {
                MessageBox.Show("Please select a type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbTask.Focus();
                return;
            }
            try
            {

                Base _basePage = new Base();

                if (cmbTask.Text == "Location")
                {
                    DataTable dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep_all(BaseCls.GlbUserComCode, txtTaskChnl.Text, txtTaskSbChnl.Text, null, null, null, txtTaskLoc.Text);
                    foreach (DataRow drow in dt.Rows)
                    {
                        if (lstPrio.Items.Count == 0) lstPrio.Items.Add(drow["PROFIT_CENTER"].ToString());
                        foreach (ListViewItem Item in lstPrio.Items)
                        {
                            if (Item.Text == drow["PROFIT_CENTER"].ToString())
                            {
                                Item.Checked = false;
                                _isFound = true;
                            }
                        }
                        if (_isFound == false)
                        {
                            lstPrio.Items.Add(drow["PROFIT_CENTER"].ToString());
                        }
                        _isFound = false;
                    }
                }
                else
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _searchType = "";
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (!string.IsNullOrEmpty(txtTaskSbChnl.Text))
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtTaskSbChnl.Text);
                        foreach (DataRow drow in _result.Rows)
                        {
                            if (lstPrio.Items.Count == 0) lstPrio.Items.Add(drow["CODE"].ToString());
                            foreach (ListViewItem Item in lstPrio.Items)
                            {
                                if (Item.Text == drow["CODE"].ToString())
                                    _isFound = true;
                            }
                            if (_isFound == false)
                            {
                                lstPrio.Items.Add(drow["CODE"].ToString());
                            }
                            _isFound = false;
                        }
                    }
                    else
                    {
                        foreach (DataRow drow in _result.Rows)
                        {
                            if (lstPrio.Items.Count == 0) lstPrio.Items.Add(drow["CODE"].ToString());
                            foreach (ListViewItem Item in lstPrio.Items)
                            {
                                if (Item.Text == drow["CODE"].ToString())
                                {
                                    Item.Checked = false;
                                    _isFound = true;
                                }
                            }
                            if (_isFound == false)
                            {
                                lstPrio.Items.Add(drow["CODE"].ToString());
                            }
                            _isFound = false;
                        }
                    }
                }
            A:
                foreach (ListViewItem Item in lstPrio.Items)
                {
                    Item.Checked = false;
                }
                txtTaskChnl.Text = "";
                txtTaskSbChnl.Text = "";
                txtTaskLoc.Text = "";
                txtTaskLoc.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelPrio_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPrio.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnUnselPrio_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPrio.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnClr_Click(object sender, EventArgs e)
        {
            Reset_Controls_Definition_prio();
        }

        private void btnSrchTaskPrio_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServicePriority);
            DataTable _result = CHNLSVC.CommonSearch.SearchScvPriority(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtTaskPrio;
            _CommonSearch.ShowDialog();
            txtTaskPrio.Focus();
        }

        private void btnSrchJobStage_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobStage);
            DataTable _result = CHNLSVC.CommonSearch.SearchJobStage(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtStage;
            _CommonSearch.ShowDialog();
            txtStage.Focus();
        }

        private void btnSaveTask_Click(object sender, EventArgs e)
        {
            if (grvPrioTask.Rows.Count > 0)
            {
                _lstPrioTask = new List<scv_prit_task>();

                #region validation

                if (lstPrio.Items.Count <= 0)
                {
                    MessageBox.Show("Please select the Sub channel/profit center which you have to assign", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion

                _lstPrioTask = fillToAllocatePriority();
                if (_lstPrioTask.Count > 0)
                {
                    if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    string _error = "";
                    int result = CHNLSVC.CustService.Save_Allocated_Priority(_lstPrioTask, out _error);
                    if (result == -1)
                    {
                        MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Records inserted Successfully", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        grvPrioTask.ReadOnly = true;
                        txtTaskPrio.Text = "";
                        txtDur.Text = "";
                        //btnClear_Click(null, null);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select Definition details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private List<scv_prit_task> fillToAllocatePriority()
        {
            _lstPrioTask = new List<scv_prit_task>();
            foreach (DataGridViewRow dgvr in grvPrioTask.Rows)
            {

                obj_task = new scv_prit_task();

                obj_task.Spit_com = BaseCls.GlbUserComCode;
                string type = cmbTask.Text;
                if (type == "Location")
                {
                    obj_task.Spit_pty_tp = "LOC";
                }
                else if (type == "Sub Channel")
                {
                    obj_task.Spit_pty_tp = "SCHNL";
                }
                else if (type == "Channel")
                {
                    obj_task.Spit_pty_tp = "CHNL";
                }

                obj_task.Spit_pty_cd = dgvr.Cells["SPIT_PTY_CD"].Value.ToString();
                obj_task.Spit_prit_cd = dgvr.Cells["SPIT_PRIT_CD"].Value.ToString();
                obj_task.Spit_stage = Convert.ToDecimal(dgvr.Cells["spit_stage"].Value);
                obj_task.Spit_expt_dur = Convert.ToDecimal(dgvr.Cells["spit_expt_dur"].Value);
                obj_task.Spit_expt_tp = dgvr.Cells["spit_expt_tp"].Value.ToString();
                obj_task.Spit_cre_by = BaseCls.GlbUserID;
                obj_task.Spit_alrt_seq = 1;

                _lstPrioTask.Add(obj_task);

            }


            if (_lstPrioTask.Count <= 0)
            {
                MessageBox.Show("Please add definition details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return _lstPrioTask;
        }

        private List<MasterServiceEmployee> fillToAllocateEmp()
        {
            _lstEmp = new List<MasterServiceEmployee>();
            foreach (DataGridViewRow dgvr in grvEmp.Rows)
            {

                obj_Emp = new MasterServiceEmployee();

                obj_Emp.Msi_com = BaseCls.GlbUserComCode;
                string type = cmbEmp.Text;
                if (type == "Location")
                {
                    obj_Emp.Msi_pty_tp = "LOC";
                }
                else if (type == "Sub Channel")
                {
                    obj_Emp.Msi_pty_tp = "SCHNL";
                }
                else if (type == "Channel")
                {
                    obj_Emp.Msi_pty_tp = "CHNL";
                }

                obj_Emp.Msi_pty_cd = dgvr.Cells["Msi_pty_cd"].Value.ToString();
                obj_Emp.Msi_emp = dgvr.Cells["Msi_emp"].Value.ToString();
                obj_Emp.Msi_act = Convert.ToInt32(dgvr.Cells["Msi_act"].Value);
                obj_Emp.Msi_com = BaseCls.GlbUserComCode;
                obj_Emp.Msi_cre_by = BaseCls.GlbUserID;
                obj_Emp.Msi_mod_by = BaseCls.GlbUserID;
                obj_Emp.Msi_prnt_cate = dgvr.Cells["Msi_prnt_cate"].Value.ToString();
                obj_Emp.Msi_prnt_emp = dgvr.Cells["Msi_prnt_emp"].Value.ToString();
                obj_Emp.Msi_pty_tp = dgvr.Cells["Msi_pty_tp"].Value.ToString();


                //obj_Emp.Spit_stage = Convert.ToDecimal(dgvr.Cells["spit_stage"].Value);
                //obj_Emp.Spit_expt_dur = Convert.ToDecimal(dgvr.Cells["spit_expt_dur"].Value);
                //obj_Emp.Spit_expt_tp = dgvr.Cells["spit_expt_tp"].Value.ToString();
                //obj_Emp.Spit_cre_by = BaseCls.GlbUserID;
                //obj_Emp.Spit_alrt_seq = 1;

                _lstEmp.Add(obj_Emp);

            }


            if (_lstEmp.Count <= 0)
            {
                MessageBox.Show("Please add definition details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return _lstEmp;
        }

        private void btnAddPrio_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTaskPrio.Text))
            {
                MessageBox.Show("Please select the priority", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtStage.Text))
            {
                MessageBox.Show("Please select the stage", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtDur.Text))
            {
                MessageBox.Show("Please enter the duration", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AddValueToGridPrio();
        }

        private void AddValueToGridPrio()
        {
            try
            {
                List<string> pcList = new List<string>();
                foreach (ListViewItem Item in lstPrio.Items)
                {
                    string partycode = Item.Text;

                    if (Item.Checked == true)
                        pcList.Add(partycode);
                }
                if (pcList.Count == 0)
                {
                    MessageBox.Show("Please select the Sub channel/profit center which you have to assign.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (string _pc in pcList)
                {

                    foreach (DataGridViewRow dgvr in grvPrioTask.Rows)
                    {
                        if (cmbTask.Text == Convert.ToString(dgvr.Cells["SPIT_PTY_TP"].Value) && _pc == Convert.ToString(dgvr.Cells["SPIT_PTY_CD"].Value) && txtPrioCode.Text == Convert.ToString(dgvr.Cells["SPIT_PRIT_CD"].Value) && txtStage.Text == Convert.ToString(dgvr.Cells["SPIT_STAGE"].Value) && cmbDurTp.Text == Convert.ToString(dgvr.Cells["SPIT_EXPT_TP"].Value))
                        {

                            MessageBox.Show("Thease values are Already Added.", "Item Allocation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    Base _basePage = new Base();
                    string type = cmbTask.Text;
                    if (type == "Location")
                    {
                        _type = "LOC";
                    }
                    else if (type == "Sub Channel")
                    {
                        _type = "SCHNL";
                    }

                    grvPrioTask.Rows.Add();
                    grvPrioTask["SPIT_COM", grvPrioTask.Rows.Count - 1].Value = BaseCls.GlbUserComCode;
                    grvPrioTask["SPIT_PTY_TP", grvPrioTask.Rows.Count - 1].Value = _type;
                    grvPrioTask["SPIT_PTY_CD", grvPrioTask.Rows.Count - 1].Value = _pc;
                    grvPrioTask["SPIT_PRIT_CD", grvPrioTask.Rows.Count - 1].Value = txtTaskPrio.Text;
                    grvPrioTask["SPIT_STAGE", grvPrioTask.Rows.Count - 1].Value = txtStage.Text;
                    grvPrioTask["SPIT_EXPT_TP", grvPrioTask.Rows.Count - 1].Value = cmbDurTp.Text.ToString();
                    grvPrioTask["SPIT_EXPT_DUR", grvPrioTask.Rows.Count - 1].Value = txtDur.Text;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CHNLSVC.CloseChannel();
            }
            finally
            {

            }
        }

        private void txtPrioCode_DoubleClick(object sender, EventArgs e)
        {
            btnSrchPrio_Click(null, null);
        }

        private void txtPrioCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchPrio_Click(null, null);
        }

        private void txtTaskChnl_DoubleClick(object sender, EventArgs e)
        {
            btnSrchTaskChnl_Click(null, null);
        }

        private void txtTaskChnl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchTaskChnl_Click(null, null);
        }

        private void txtTaskSbChnl_DoubleClick(object sender, EventArgs e)
        {
            btnSrchTaskSbChnl_Click(null, null);
        }

        private void txtTaskSbChnl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchTaskSbChnl_Click(null, null);
        }

        private void txtTaskLoc_DoubleClick(object sender, EventArgs e)
        {
            btnSrchTaskLoc_Click(null, null);
        }

        private void txtTaskLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchTaskLoc_Click(null, null);
        }

        private void txtTaskPrio_DoubleClick(object sender, EventArgs e)
        {
            btnSrchTaskPrio_Click(null, null);
        }

        private void txtTaskPrio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchTaskPrio_Click(null, null);
        }

        private void btnClrTask_Click(object sender, EventArgs e)
        {
            if (cmbTask.SelectedIndex == -1 && string.IsNullOrEmpty(txtTaskChnl.Text) && string.IsNullOrEmpty(txtTaskLoc.Text) && string.IsNullOrEmpty(txtTaskPrio.Text) && string.IsNullOrEmpty(txtTaskSbChnl.Text) && string.IsNullOrEmpty(txtStage.Text) && string.IsNullOrEmpty(txtDur.Text) && string.IsNullOrEmpty(cmbDurTp.Text) && lstPrio.Items.Count == 0)
            {
                MessageBox.Show("Already cleared", "Infor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
            clearTask();


        }

        private void clearTask()
        {
            cmbTask.SelectedIndex = -1;
            txtTaskChnl.Text = "";
            txtTaskLoc.Text = "";
            txtTaskPrio.Text = "";
            txtTaskSbChnl.Text = "";
            txtStage.Text = "";
            txtDur.Text = "";
            cmbDurTp.SelectedIndex = -1; ;
            lstPrio.Clear();
            DataTable _dt = null;
            grvPrioTask.DataSource = _dt;
        }

        private void txtDur_Leave(object sender, EventArgs e)
        {
            if (!IsNumeric(txtDur.Text))
            {
                MessageBox.Show("Invalid amount", "Infor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDur.Text = "0";
                txtDur.Focus();
                return;
            }
        }

        private void cmbEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEmp.Text == "Sub Channel")
            {
                txtEmpLoc.Enabled = false;
                btnSrchEmpLoc.Enabled = false;
                txtEmpChnl.Enabled = true;
                btnSrchEmpChnl.Enabled = true;
                txtEmpSbChnl.Enabled = true;
                btnSrchEmpSbChnl.Enabled = true;
                txtEmpChnl.Focus();
            }
            else if (cmbEmp.Text == "Profit Center")
            {
                txtEmpChnl.Enabled = false;
                btnSrchEmpChnl.Enabled = false;
                txtEmpSbChnl.Enabled = false;
                btnSrchEmpSbChnl.Enabled = false;
                txtEmpLoc.Enabled = true;
                btnSrchEmpLoc.Enabled = true;
                txtEmpLoc.Focus();
            }
        }

        private void btnSrchEmpChnl_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEmpChnl;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtEmpChnl.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSrchEmpSbChnl_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEmpChnl.Text))
                {
                    MessageBox.Show("Please select channel.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEmpSbChnl.Text = "";
                    txtEmpChnl.Focus();
                    return;
                }
                _searchType = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEmpSbChnl;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtEmpSbChnl.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSrchEmpLoc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEmpLoc;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtEmpLoc.Select();

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAddEmp_Click(object sender, EventArgs e)
        {
            Boolean _isFound = false;
            if (cmbEmp.Text == "")
            {
                MessageBox.Show("Please select a type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbEmp.Focus();
                return;
            }
            try
            {
                lstEmp.Clear();
                Base _basePage = new Base();

                if (cmbEmp.Text == "Profit Center")
                {
                    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(BaseCls.GlbUserComCode, txtEmpChnl.Text, txtEmpSbChnl.Text, null, null, null, txtEmpLoc.Text);
                    foreach (DataRow drow in dt.Rows)
                    {
                        if (lstEmp.Items.Count == 0) lstEmp.Items.Add(drow["PROFIT_CENTER"].ToString());
                        foreach (ListViewItem Item in lstEmp.Items)
                        {
                            if (Item.Text == drow["PROFIT_CENTER"].ToString())
                            {
                                Item.Checked = false;
                                _isFound = true;
                            }
                        }
                        if (_isFound == false)
                        {
                            lstEmp.Items.Add(drow["PROFIT_CENTER"].ToString());
                        }
                        _isFound = false;
                    }
                }
                else
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _searchType = "";
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (!string.IsNullOrEmpty(txtEmpSbChnl.Text))
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtEmpSbChnl.Text);
                        foreach (DataRow drow in _result.Rows)
                        {
                            if (lstEmp.Items.Count == 0) lstEmp.Items.Add(drow["CODE"].ToString());
                            foreach (ListViewItem Item in lstEmp.Items)
                            {
                                if (Item.Text == drow["CODE"].ToString())
                                    _isFound = true;
                            }
                            if (_isFound == false)
                            {
                                lstEmp.Items.Add(drow["CODE"].ToString());
                            }
                            _isFound = false;
                        }
                    }
                    else
                    {
                        foreach (DataRow drow in _result.Rows)
                        {
                            if (lstEmp.Items.Count == 0) lstEmp.Items.Add(drow["CODE"].ToString());
                            foreach (ListViewItem Item in lstEmp.Items)
                            {
                                if (Item.Text == drow["CODE"].ToString())
                                {
                                    Item.Checked = false;
                                    _isFound = true;
                                }
                            }
                            if (_isFound == false)
                            {
                                lstEmp.Items.Add(drow["CODE"].ToString());
                            }
                            _isFound = false;
                        }
                    }
                }
            A:
                foreach (ListViewItem Item in lstEmp.Items)
                {
                    Item.Checked = false;
                }
                txtEmpChnl.Text = "";
                txtEmpSbChnl.Text = "";
                txtEmpLoc.Text = "";
                txtEmpLoc.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelEmp_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstEmp.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnUnselEmp_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstEmp.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnClearEmp_Click(object sender, EventArgs e)
        {
            lstEmp.Clear();
        }

        private void txtEmpLoc_DoubleClick(object sender, EventArgs e)
        {
            btnSrchEmpLoc_Click(null, null);
        }

        private void txtEmpLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchEmpLoc_Click(null, null);
        }

        private void txtEmpSbChnl_DoubleClick(object sender, EventArgs e)
        {
            btnSrchEmpSbChnl_Click(null, null);
        }

        private void txtEmpSbChnl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchEmpSbChnl_Click(null, null);
        }

        private void txtEmpChnl_DoubleClick(object sender, EventArgs e)
        {
            btnSrchEmpChnl_Click(null, null);
        }

        private void txtEmpChnl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchEmpChnl_Click(null, null);
        }

        private void btnSrchEmp_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeAll);
                DataTable _result = CHNLSVC.CommonSearch.Get_employee_All(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEmpCode;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtEmpCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSrchParentEmp_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeAll);
                DataTable _result = CHNLSVC.CommonSearch.Get_employee_All(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtParentEmp;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtParentEmp.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddList_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmpCode.Text))
            {
                MessageBox.Show("Please select the employee", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtParentEmp.Text))
            {
                MessageBox.Show("Please select the parent employee", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtParentCat.Text))
            {
                MessageBox.Show("Please select the parent category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            AddValueToGridEmp();
        }

        private void AddValueToGridEmp()
        {
            try
            {
                List<string> pcList = new List<string>();
                foreach (ListViewItem Item in lstEmp.Items)
                {
                    string partycode = Item.Text;

                    if (Item.Checked == true)
                        pcList.Add(partycode);
                }
                if (pcList.Count == 0)
                {
                    MessageBox.Show("Please select the Sub channel/profit center which you have to assign.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string type = cmbEmp.Text;
                if (type == "Profit Center")
                {
                    _type = "PC";
                }
                else if (type == "Sub Channel")
                {
                    _type = "SCHNL";
                }

                foreach (string _pc in pcList)
                {

                    foreach (DataGridViewRow dgvr in grvEmp.Rows)
                    {
                        if (_type == Convert.ToString(dgvr.Cells["msi_pty_tp"].Value) && _pc == Convert.ToString(dgvr.Cells["msi_pty_cd"].Value) && txtEmpCode.Text == Convert.ToString(dgvr.Cells["msi_emp"].Value) && txtParentCat.Text == Convert.ToString(dgvr.Cells["msi_prnt_cate"].Value) && txtParentEmp.Text == Convert.ToString(dgvr.Cells["msi_prnt_emp"].Value))
                        {
                            grvEmp.Update();
                            grvEmp["msi_act", dgvr.Index].Value = Convert.ToInt32(chkEmp.Checked);
                            //  MessageBox.Show("Thease values are Already Added.", "Item Allocation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    Base _basePage = new Base();

                    grvEmp.Rows.Add();
                    grvEmp["msi_com", grvEmp.Rows.Count - 1].Value = BaseCls.GlbUserComCode;
                    grvEmp["msi_pty_tp", grvEmp.Rows.Count - 1].Value = _type;
                    grvEmp["msi_pty_cd", grvEmp.Rows.Count - 1].Value = _pc;
                    grvEmp["msi_emp", grvEmp.Rows.Count - 1].Value = txtEmpCode.Text;
                    grvEmp["msi_prnt_cate", grvEmp.Rows.Count - 1].Value = txtParentCat.Text;
                    grvEmp["msi_prnt_emp", grvEmp.Rows.Count - 1].Value = txtParentEmp.Text;
                    grvEmp["msi_act", grvEmp.Rows.Count - 1].Value = Convert.ToInt32(chkEmp.Checked);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CHNLSVC.CloseChannel();
            }
            finally
            {

            }
        }

        private void btnClrEmp_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
            clear();
        }

        private void clear()
        {
            cmbEmp.SelectedIndex = -1;
            txtEmpChnl.Text = "";
            txtEmpLoc.Text = "";
            txtEmpSbChnl.Text = "";
            txtEmpCode.Text = "";
            txtParentCat.Text = "";
            txtParentEmp.Text = "";
            lstEmp.Clear();
            grvEmp.Rows.Clear();
            chkEmp.Checked = false;
        }

        private void btnSrchParentCat_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Designation);
                DataTable _result = CHNLSVC.CommonSearch.Get_Designations(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtParentCat;
                _CommonSearch.ShowDialog();

                txtParentCat.Focus();
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

        private void btnSaveEmp_Click(object sender, EventArgs e)
        {
            if (grvEmp.Rows.Count > 0)
            {
                _lstEmp = new List<MasterServiceEmployee>();

                #region validation

                if (lstEmp.Items.Count <= 0)
                {
                    MessageBox.Show("Please select the Sub channel/profit center which you have to assign", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion

                _lstEmp = fillToAllocateEmp();
                if (_lstEmp.Count > 0)
                {
                    if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    string _error = "";
                    int result = CHNLSVC.CustService.Save_Allocated_Employee(_lstEmp, out _error);
                    if (result == -1)
                    {
                        MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Records inserted Successfully", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        grvEmp.ReadOnly = true;
                        clear();
                        //btnClear_Click(null, null);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select Definition details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtEmpCode_DoubleClick(object sender, EventArgs e)
        {
            btnSrchEmp_Click(null, null);
        }

        private void txtEmpCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchEmp_Click(null, null);
        }

        private void txtParentCat_DoubleClick(object sender, EventArgs e)
        {
            btnSrchParentCat_Click(null, null);
        }

        private void txtParentCat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchParentCat_Click(null, null);
        }

        private void txtParentEmp_DoubleClick(object sender, EventArgs e)
        {
            btnSrchParentEmp_Click(null, null);
        }

        private void txtParentEmp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchParentEmp_Click(null, null);
        }

        private void txtEmpCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmpCode.Text))
            {
                DataTable _dt = CHNLSVC.Sales.GetEmployee(BaseCls.GlbUserComCode, txtEmpCode.Text);
                if (_dt.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid employee code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmpCode.Text = "";
                    txtEmpCode.Focus();
                    return;
                }
            }
        }

        private void txtParentEmp_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtParentEmp.Text))
            {
                DataTable _dt = CHNLSVC.Sales.GetEmployee(BaseCls.GlbUserComCode, txtParentEmp.Text);
                if (_dt.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid employee code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtParentEmp.Text = "";
                    txtParentEmp.Focus();
                    return;
                }
            }
        }

        private void txtParentCat_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtParentCat.Text))
            {
                MasterUserCategory _cat = CHNLSVC.General.GetUserCatByCode(txtParentCat.Text);
                if (_cat == null)
                {
                    MessageBox.Show("Invalid parent category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtParentCat.Text = "";
                    txtParentCat.Focus();
                    return;
                }
            }
        }

        private void grvEmp_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //txtEmpCode.Text = Convert.ToString(grvEmp.Rows[e.RowIndex].Cells["msi_emp"].Value);
            //txtParentCat.Text = Convert.ToString(grvEmp.Rows[e.RowIndex].Cells["msi_prnt_cate"].Value);
            //txtParentEmp.Text = Convert.ToString(grvEmp.Rows[e.RowIndex].Cells["msi_prnt_emp"].Value);
            //if (Convert.ToString(grvEmp.Rows[e.RowIndex].Cells["msi_pty_tp"].Value) == "SCHNL")
            //{
            //    cmbEmp.Text = "Sub Channel";
            //    txtEmpSbChnl.Text = Convert.ToString(grvEmp.Rows[e.RowIndex].Cells["msi_pty_cd"].Value);
            //}
            //else
            //{
            //    cmbEmp.Text = "Profit Center";
            //    txtEmpLoc.Text = Convert.ToString(grvEmp.Rows[e.RowIndex].Cells["msi_pty_cd"].Value);
            //}
            //chkEmp.Checked = Convert.ToBoolean(grvEmp.Rows[e.RowIndex].Cells["msi_act"].Value);
            //lstEmp.Clear();
            //lstEmp.Items.Add(Convert.ToString(grvEmp.Rows[e.RowIndex].Cells["msi_pty_cd"].Value));
            //lstEmp.Items[0].Checked = true;
        }

        private void grvEmp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                txtEmpCode.Text = Convert.ToString(grvEmp.Rows[e.RowIndex].Cells["msi_emp"].Value);
                txtParentCat.Text = Convert.ToString(grvEmp.Rows[e.RowIndex].Cells["msi_prnt_cate"].Value);
                txtParentEmp.Text = Convert.ToString(grvEmp.Rows[e.RowIndex].Cells["msi_prnt_emp"].Value);
                if (Convert.ToString(grvEmp.Rows[e.RowIndex].Cells["msi_pty_tp"].Value) == "SCHNL")
                {
                    cmbEmp.Text = "Sub Channel";
                    txtEmpSbChnl.Text = Convert.ToString(grvEmp.Rows[e.RowIndex].Cells["msi_pty_cd"].Value);
                }
                else
                {
                    cmbEmp.Text = "Profit Center";
                    txtEmpLoc.Text = Convert.ToString(grvEmp.Rows[e.RowIndex].Cells["msi_pty_cd"].Value);
                }
                chkEmp.Checked = Convert.ToBoolean(grvEmp.Rows[e.RowIndex].Cells["msi_act"].Value);
                lstEmp.Clear();
                lstEmp.Items.Add(Convert.ToString(grvEmp.Rows[e.RowIndex].Cells["msi_pty_cd"].Value));
                lstEmp.Items[0].Checked = true;
            }
        }

        private void btn_Srch_sbChnl_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtQSbChnl;
            _CommonSearch.ShowDialog();
            txtQSbChnl.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "SCHNL", txtQSbChnl.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtSChnlDesc.Text = row2["descp"].ToString();
            }
        }

        private void btn_Srch_Quest_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustQuest);
            DataTable _result = CHNLSVC.CommonSearch.GetCustQuestSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtQCode;
            _CommonSearch.ShowDialog();
            txtQCode.Select();

            if (!string.IsNullOrEmpty(txtQCode.Text))
                loadCustQ(Convert.ToInt32(txtQCode.Text));
        }

        private void loadCustSatis(Int32 _seq, Int32 _line)
        {
            DataTable _dt = CHNLSVC.CustService.GetCustSatisfData(_seq, _line);
            if (_dt.Rows.Count > 0)
            {
                txtSatis.Text = _dt.Rows[0]["ssv_desc"].ToString();
                txtGrade.Text = _dt.Rows[0]["ssv_grade"].ToString();
                chkActSatis.Checked = Convert.ToBoolean(_dt.Rows[0]["ssv_act"]);

            }
            else
            {
                txtSatis.Text = "";
                txtGrade.Text = "";
                chkActSatis.Checked = true;
            }
        }
        private void loadCustQ(Int32 _seq)
        {
            DataTable _dt = CHNLSVC.CustService.GetCustQuestData(_seq);
            if (_dt.Rows.Count > 0)
            {
                txtQSbChnl.Text = _dt.Rows[0]["ssq_schnl"].ToString();
                txtQ.Text = _dt.Rows[0]["ssq_quest"].ToString();
                chkActQuest.Checked = Convert.ToBoolean(_dt.Rows[0]["ssq_act"]);
                chkIsSMS.Checked = Convert.ToBoolean(_dt.Rows[0]["ssq_issms"]);

                DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "SCHNL", txtQSbChnl.Text);
                foreach (DataRow row2 in LocDes.Rows)
                {
                    txtSChnlDesc.Text = row2["descp"].ToString();
                }


                DataTable _dt1 = CHNLSVC.CustService.GetCustSatisfData(_seq, 0);
                if (_dt.Rows.Count > 0)
                {
                    grvSatis.AutoGenerateColumns = false;
                    grvSatis.DataSource = _dt1;
                }
            }
            else
            {
                txtQSbChnl.Text = "";
                txtQ.Text = "";
                chkActQuest.Checked = true;
                chkIsSMS.Checked = true;
            }
        }

        private void btn_Srch_satis_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQCode.Text))
            {
                MessageBox.Show("Please select the code", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustSatis);
            DataTable _result = CHNLSVC.CommonSearch.GetCustSatisSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSatisCode;
            _CommonSearch.ShowDialog();
            txtSatisCode.Select();

            if (!string.IsNullOrEmpty(txtSatisCode.Text))
                loadCustSatis(Convert.ToInt32(txtQCode.Text), Convert.ToInt32(txtSatisCode.Text));
        }

        private void btn_Srch_grade_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustGrade);
            DataTable _result = CHNLSVC.CommonSearch.GetCustGradeSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtGrade;
            _CommonSearch.ShowDialog();
            txtGrade.Select();
        }

        private void btnNewQuest_Click(object sender, EventArgs e)
        {
            pnlQ.Enabled = true;
            txtQCode.Text = "";
            txtQCode.Enabled = false;
            btn_Srch_Quest.Enabled = false;
        }

        private void btnNewSatis_Click(object sender, EventArgs e)
        {
            txtSatisCode.Text = "";
            pnlSatis.Enabled = true;
            txtSatisCode.Enabled = false;
            btn_Srch_satis.Enabled = false;

        }

        private void btnClearQ_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
            clearQ();
        }

        private void clearQ()
        {
            //pnlQ.Enabled = false;
            txtQCode.Enabled = true;
            btn_Srch_Quest.Enabled = true;
            // pnlSatis.Enabled = false;
            txtSatisCode.Enabled = true;
            btn_Srch_satis.Enabled = true;
            txtSatis.Text = "";
            txtSatisCode.Text = "";
            txtQ.Text = "";
            txtQCode.Text = "";
            txtQSbChnl.Text = "";
            txtGrade.Text = "";
            txtSChnlDesc.Text = "";
            chkActQuest.Checked = true;
            chkActSatis.Checked = true;
            chkIsSMS.Checked = false;

            grvSatis.AutoGenerateColumns = false;
            grvSatis.DataSource = null;

        }

        private void txtQCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_Srch_Quest_Click(null, null);
        }

        private void txtQCode_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Quest_Click(null, null);
        }

        private void txtQSbChnl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_Srch_sbChnl_Click(null, null);
        }

        private void txtQSbChnl_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_sbChnl_Click(null, null);
        }

        private void txtSatisCode_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_satis_Click(null, null);
        }

        private void txtSatisCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_Srch_satis_Click(null, null);
        }

        private void txtGrade_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_grade_Click(null, null);
        }

        private void txtGrade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_Srch_grade_Click(null, null);
        }

        private void btnSaveQ_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQCode.Text) && txtQCode.Enabled == true)
            {
                MessageBox.Show("Please select the code / press new button to create new", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtQ.Text))
            {
                MessageBox.Show("Please enter the question", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQ.Focus();
                return;
            }
            Int32 _seq = 0;

            if (MessageBox.Show("Are you sure ?", "Service Definitions", MessageBoxButtons.YesNo) == DialogResult.No) return;

            if (txtQCode.Enabled == false)
                _seq = 0;
            else
                _seq = Convert.ToInt32(txtQCode.Text);

            Int32 _eff = CHNLSVC.CustService.UpdateCustomerQuest(_seq, BaseCls.GlbUserComCode, txtQSbChnl.Text, txtQ.Text, Convert.ToInt32(chkActQuest.Checked), BaseCls.GlbUserID, Convert.ToInt32(chkIsSMS.Checked));
            if (_eff == 1)
            {
                MessageBox.Show("Successfully Saved", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearQ();
            }
        }

        private void btn_Add_satis_Click(object sender, EventArgs e)
        {

        }

        private void ServiceDefinitions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtQCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtQCode.Text))
                loadCustQ(Convert.ToInt32(txtQCode.Text));
        }

        private void btnSaveVal_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQCode.Text))
            {
                MessageBox.Show("Please select the question", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtSatisCode.Text) && txtSatisCode.Enabled == true)
            {
                MessageBox.Show("Please select the code / press new button to create new", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtSatis.Text))
            {
                MessageBox.Show("Please enter satisfaction", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtGrade.Text))
            {
                MessageBox.Show("Please select the grade", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Int32 _seq = 0;

            if (MessageBox.Show("Are you sure ?", "Service Definitions", MessageBoxButtons.YesNo) == DialogResult.No) return;

            if (txtSatisCode.Enabled == false)
                _seq = 0;
            else
                _seq = Convert.ToInt32(txtSatisCode.Text);

            Int32 _eff = CHNLSVC.CustService.UpdateCustomerQSatis(Convert.ToInt32(txtQCode.Text), _seq, txtSatis.Text, txtGrade.Text, Convert.ToInt32(chkActSatis.Checked),BaseCls.GlbUserID);
            if (_eff == 1)
            {
                loadCustQ(Convert.ToInt32(txtQCode.Text));
                MessageBox.Show("Successfully Saved", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSatisCode.Text = "";
                txtSatis.Text = "";
                txtGrade.Text = "";
            }
        }

        private void grvSatis_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(string.IsNullOrEmpty(txtQCode.Text))
            {
                MessageBox.Show("Please select the code", "Service Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int _ef = CHNLSVC.CustService.DeleteSatisVal(Convert.ToInt32(txtQCode.Text), Convert.ToInt32(grvSatis.Rows[e.RowIndex].Cells[1].Value));

                    loadCustQ(Convert.ToInt32(txtQCode.Text));
                }
            }
        }

        private void txtSatisCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtQCode.Text))
                if (!string.IsNullOrEmpty(txtSatisCode.Text))
                    loadCustSatis(Convert.ToInt32(txtQCode.Text), Convert.ToInt32(txtSatisCode.Text));
        }

        private void btnClearVal_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
            txtSatis.Text = "";
            txtSatisCode.Text = "";
            txtGrade.Text = "";
            txtSatisCode.Enabled = true;
            btn_Srch_satis.Enabled = true;
            grvSatis.AutoGenerateColumns = false;
            grvSatis.DataSource = null;
        }

        private void txtQSbChnl_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtQSbChnl.Text))
            {
                if (!CHNLSVC.General.CheckSubChannel(BaseCls.GlbUserComCode, txtQSbChnl.Text.Trim().ToUpper()))
                {
                    MessageBox.Show("Please check the sub channel.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQSbChnl.Clear();
                    txtSChnlDesc.Clear();
                    return;
                }
                else
                {
                    DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "SCHNL", txtQSbChnl.Text);
                    foreach (DataRow row2 in LocDes.Rows)
                    {
                        txtSChnlDesc.Text = row2["descp"].ToString();
                    }
                }
            }
        }

        private void txtSatisCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtQCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }





    }
}
