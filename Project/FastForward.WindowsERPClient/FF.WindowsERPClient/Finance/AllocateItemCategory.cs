using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Finance
{
    public partial class AllocateItemCategory : Base
    {
        string _searchType = "";
        Deposit_Bank_Pc_wise obj_items;
        List<Deposit_Bank_Pc_wise> _lstAllocateItems;
        int avl;

        public AllocateItemCategory()
        {
            InitializeComponent();
        }


        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

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
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txttpt_maincat.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                        break;

                    }
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





        private void btnSearch_tptMainCat_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txttpt_maincat;
            _CommonSearch.ShowDialog();
            txttpt_maincat.Select();
        }


        private void AddValueToGrid()
        {
            try
            {
                List<string> pcList = new List<string>();
                foreach (ListViewItem Item in lstPC.Items)
                {
                    string pc = Item.Text;

                    if (Item.Checked == true)
                        pcList.Add(pc);
                }
                if (pcList.Count == 0)
                {
                    MessageBox.Show("Please select the Sub channel/profit center which you have to assign.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (string _pc in pcList)
                {
                    string _type = null;
                    string tpe = cmbCommDef.Text;
                    if (tpe == "Location")
                    {
                        _type = "PC";
                    }
                    else if (tpe == "Sub Channel")
                    {
                       _type = "SCHNL";
                    }

                    DataTable dt = CHNLSVC.General.get_availble_AllocateItems(BaseCls.GlbUserComCode,_type,_pc,txttpt_maincat.Text.Trim());
                    if(dt.Rows.Count > 0)
                    {
                        //MessageBox.Show("These values are Already Saved. Please select another", "Item Allocation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        continue;
                    }

                    foreach (DataGridViewRow dgvr in dgvItemDets.Rows)
                    {
                        if (cmbCommDef.Text == Convert.ToString(dgvr.Cells["clmType"].Value) && _pc == Convert.ToString(dgvr.Cells["clmCode"].Value) && txttpt_maincat.Text == Convert.ToString(dgvr.Cells["clmItemCat"].Value))
                        {

                            MessageBox.Show("These values are Already Added.", "Item Allocation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    dgvItemDets.Rows.Add();

                    dgvItemDets["clmType", dgvItemDets.Rows.Count - 1].Value = cmbCommDef.Text;
                    dgvItemDets["clmCode", dgvItemDets.Rows.Count - 1].Value = _pc;
                    dgvItemDets["clmItemCat", dgvItemDets.Rows.Count - 1].Value = txttpt_maincat.Text.Trim();

                    //foreach (DataGridViewRow dgr in dgvItemDets.Rows)
                    //{
                    //    string val = dgr.Cells["clmCode"].Value.ToString();
                    //    DataTable dt = CHNLSVC.General.get_Default_val(val);
                    //    if (dt.Rows.Count > 0)
                    //    {
                           
                    //        dgr.Cells["clmDefault"].ReadOnly = true;
                    //        dgr.Cells["clmdelete"].ReadOnly = false;
                           
                    //    }
                    //}


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

        private void clearfields()
        {
            txttpt_maincat.Text = "";
            //txtChanel.Text = "";
            //txtSChanel.Text = "";
            //txtPC.Text = "";
            //lstPC.Clear();
            //txtChanel.Focus();
        }

        private void btnAddtogrid_Click(object sender, EventArgs e)
        {

            if (txttpt_maincat.Text.Trim() == "")
            {
                MessageBox.Show("Please select a category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txttpt_maincat.Focus();
                return;
            }

            AddValueToGrid();
            clearfields();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txttpt_maincat_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_tptMainCat_Click(null, null);
        }

        private void txttpt_maincat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_tptMainCat_Click(null, null);
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
                                _isFound = true;
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
                                    _isFound = true;
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
                    Item.Checked = true;
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

        private void cmbCommDef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCommDef.Text == "Location")
            {
                txtChanel.Text = "";
                txtSChanel.Text = "";
                txtPC.Text = "";
                txtChanel.Enabled = true;
                txtPC.Enabled = true;
                btnSrhLocation.Enabled = true;
                txtSChanel.Enabled = true;
                btnSearchChannel.Enabled = true;
                btnSearchPC.Enabled = true;
                btnSearchSubChannel.Enabled = true;
                lstPC.Clear();
                txtChanel.Focus();

            }
            else
            {
                txtChanel.Text = "";
                txtSChanel.Text = "";
                txtPC.Text = "";
                txtChanel.Enabled = true;
                txtPC.Enabled = false;
                btnSrhLocation.Enabled = false;
                txtSChanel.Enabled = true;
                btnSearchChannel.Enabled = true;
                btnSearchPC.Enabled = false;
                btnSearchSubChannel.Enabled = true;
                lstPC.Clear();
                txtChanel.Focus();
            }
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
                    MessageBox.Show("Please select channel.", "Item Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnPcClear_Click(object sender, EventArgs e)
        {
            txtChanel.Text = "";
            txtSChanel.Text = "";
            txtPC.Text = "";
            lstPC.Clear();
            txtChanel.Focus();
        }


        private List<Deposit_Bank_Pc_wise> fillToAllocateItemDets()
        {
            _lstAllocateItems = new List<Deposit_Bank_Pc_wise>();
            //save Item Cat Details
            foreach (DataGridViewRow dgvr in dgvItemDets.Rows)
            {

                obj_items = new Deposit_Bank_Pc_wise();

                obj_items.Company = BaseCls.GlbUserComCode;
                string type = Convert.ToString(dgvr.Cells["clmType"].Value);
                if (type == "Location")
                {
                    obj_items.Profit_center = "PC";
                }
                else if (type == "Sub Channel")
                {
                    obj_items.Profit_center = "SCHNL";
                }
                obj_items.BankCode = dgvr.Cells["clmCode"].Value.ToString();
                obj_items.Mid_no = dgvr.Cells["clmItemCat"].Value.ToString();

                if (Convert.ToBoolean(dgvr.Cells["clmDefault"].Value) == true)
                {
                    obj_items.Pun_tp = 1;
                    string val = obj_items.BankCode;
                    DataTable dt = CHNLSVC.General.get_Default_val(val);
                    if (dt.Rows.Count > 0)
                    {
                        obj_items.Pun_tp = 0;
                    }

                }
                else
                {
                    obj_items.Pun_tp = 0;
                }



                obj_items.Create_by = BaseCls.GlbUserID;
                obj_items.Modifyby = BaseCls.GlbUserID;

                _lstAllocateItems.Add(obj_items);

            }


            if (_lstAllocateItems.Count <= 0)
            {
                MessageBox.Show("Please add item category details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return _lstAllocateItems;
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvItemDets.Rows.Count > 0)
            {
                _lstAllocateItems = new List<Deposit_Bank_Pc_wise>();

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
                    int result = CHNLSVC.Sales.SaveToAllocateItems(_lstAllocateItems, out _error);
                    if (result == -1)
                    {
                        MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Records inserted Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnClear_Click(null, null);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please add item category details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvItemDets_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            //if (e.RowIndex >= 0)
            //{


            //    if (dgvItemDets.Rows[e.RowIndex].Cells["clmDefault"].ReadOnly == true)
            //    {
            //        return;
            //    }


            //    string val = dgvItemDets.Rows[e.RowIndex].Cells["clmCode"].Value.ToString();
            //    DataTable dt = CHNLSVC.General.get_Default_val(val);
            //    if (dt.Rows.Count > 0)
            //    {
            //        MessageBox.Show("default is setup for this location.please set another", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        dgvItemDets.Rows[e.RowIndex].Cells["clmDefault"].Value = false;
            //    }
            //    else
            //    {

            //        for (int i = 0; i < dgvItemDets.Rows.Count; i++)
            //        {
            //            if (val == Convert.ToString(dgvItemDets.Rows[i].Cells["clmCode"].Value))
            //            {
            //                if (e.RowIndex != i)
            //                {
            //                    //dgvItemDets.Rows[i].Cells["clmDefault"].ReadOnly = true;
            //                    // DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dgvItemDets.Rows[i].Cells["clmDefault"];
            //                    //chk.ReadOnly = true;
            //                    dgvItemDets.Rows[i].Cells["clmDefault"].Value = null;


            //                }
            //                else
            //                {

            //                    dgvItemDets.Rows[i].Cells["clmDefault"].ReadOnly = false;


            //                }

            //            }
            //            else
            //            {
            //                dgvItemDets.Rows[i].Cells["clmDefault"].ReadOnly = false;

            //            }
            //        }

            //    }




            //    if (dgvItemDets.Rows.Count > 0)
            //    {
            //        if (e.ColumnIndex == 4)
            //        {
            //            DialogResult dgr = MessageBox.Show("Do you want to delete this record?", "Item Allocation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //            if (dgr == System.Windows.Forms.DialogResult.No)
            //            {
            //                return;
            //            }

            //            dgvItemDets.Rows.RemoveAt(e.RowIndex);

            //        }
            //    }
            //}
        }

        private void dgvItemDets_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            //dgvItemDets_CurrentCellDirtyStateChanged(null, null);

            //dgvItemDets.Rows[e.RowIndex].Cells["clmDefault"].Value = true;

            if (e.RowIndex >= 0)
            {


                if (dgvItemDets.Rows[e.RowIndex].Cells["clmDefault"].ReadOnly == true)
                {
                    return;
                }


                string val = dgvItemDets.Rows[e.RowIndex].Cells["clmCode"].Value.ToString();
                DataTable dt = CHNLSVC.General.get_Default_val(val);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("default is setup for this location.please set another", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvItemDets.Rows[e.RowIndex].Cells["clmDefault"].Value = false;
                }
                else
                {

                    for (int i = 0; i < dgvItemDets.Rows.Count; i++)
                    {
                        if (val == Convert.ToString(dgvItemDets.Rows[i].Cells["clmCode"].Value))
                        {
                            if (e.RowIndex != i)
                            {
                                //dgvItemDets.Rows[i].Cells["clmDefault"].ReadOnly = true;
                                // DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dgvItemDets.Rows[i].Cells["clmDefault"];
                                //chk.ReadOnly = true;
                                dgvItemDets.Rows[i].Cells["clmDefault"].Value = null;


                            }
                            else
                            {

                                dgvItemDets.Rows[i].Cells["clmDefault"].ReadOnly = false;


                            }

                        }
                        else
                        {
                            dgvItemDets.Rows[i].Cells["clmDefault"].ReadOnly = false;

                        }
                    }

                }




                if (dgvItemDets.Rows.Count > 0)
                {
                    if (e.ColumnIndex == 4)
                    {
                        DialogResult dgr = MessageBox.Show("Do you want to delete this record?", "Item Allocation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dgr == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }

                        dgvItemDets.Rows.RemoveAt(e.RowIndex);

                    }
                }
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvItemDets.Rows.Clear();
            txttpt_maincat.Text = "";

            txtChanel.Text = "";
            txtSChanel.Text = "";
            txtPC.Text = "";
            lstPC.Clear();
            txtChanel.Focus();


        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
            clsSalesRep objsales = new clsSalesRep();
            BaseCls.GlbReportName = "AllocateItemReport.rpt";
            BaseCls.GlbReportHeading = "Allocated Item Category Details";

             List<string> pcList = new List<string>();
                foreach (ListViewItem Item in lstPC.Items)
                {
                    string pc = Item.Text;

                    if (Item.Checked == true)
                        pcList.Add(pc);
                }

            string type = cmbCommDef.Text;
            string tp = null;
            if (type == "Location")
            {
                tp = "PC";
            }
            else if (type == "Sub Channel")
            {
                tp = "SCHNL";
            }
            string cat = txttpt_maincat.Text.Trim();
            objsales.get_AllocateItems_dets(tp, cat, pcList);

            _view.Show();
            _view = null;
        }

        private void txttpt_maincat_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txttpt_maincat.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txttpt_maincat.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid main category code", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txttpt_maincat.Clear();
                    txttpt_maincat.Focus();
                    return;
                }
                //txtSubCate.Clear();
                // txtItemRange.Clear();

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

        private void dgvItemDets_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvItemDets.IsCurrentCellDirty)
            {
                dgvItemDets.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked == true)
            {
                foreach (DataGridViewRow gvr in dgvItemDets.Rows)
                {

                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gvr.Cells["clmDefault"];
                    chk.Value = true;
                    lblSelect.Text = "Deselect All";
                }
            }
            else
            {
                foreach (DataGridViewRow gvr in dgvItemDets.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gvr.Cells["clmDefault"];
                    chk.Value = false;
                    lblSelect.Text = "Select All";
                }
            }
        }
    }
}
