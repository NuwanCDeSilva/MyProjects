using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.General
{
    public partial class frmItemConditionSetup : Base
    {
        private string _SerialSearchType = string.Empty;
        private string _receCompany = string.Empty;
        private List<ItemConditionSetup> oItemConditionSetups = new List<ItemConditionSetup>();
        private List<ItemConditionSetup> oMainList = new List<ItemConditionSetup>();
        private bool isNewitemExsists = false;

        public frmItemConditionSetup()
        {
            InitializeComponent();
        }

        private void frmItemConditionSetup_Load(object sender, EventArgs e)
        {
            FillCategories();
            dgvHistory.AutoGenerateColumns = false;
            lblDiscription.Text = "";
            lblItemStatus.Text = "";
            isNewitemExsists = false;
        }

        #region Events

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isNewitemExsists == false)
            {
                MessageBox.Show("Please add new items.", "Item Condition Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult dgr = MessageBox.Show("Do you want to Save?", "Item Condition Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dgr == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            if (dgvHistory.Rows.Count > 0)
            {
                int result = 0;
                for (int i = 0; i < dgvHistory.Rows.Count; i++)
                {
                    ItemConditionSetup oItemCondition = new ItemConditionSetup();
                    oItemCondition.irsc_ser_id = Convert.ToInt32(dgvHistory.Rows[i].Cells["irsc_ser_id"].Value.ToString());
                    oItemCondition.irsc_line = 0;
                    oItemCondition.irsc_com = BaseCls.GlbUserComCode;
                    oItemCondition.irsc_loc = BaseCls.GlbUserDefLoca;
                    oItemCondition.irsc_cat = dgvHistory.Rows[i].Cells["irsc_cat"].Value.ToString();
                    oItemCondition.irsc_tp = string.Empty;
                    oItemCondition.irsc_rmk = dgvHistory.Rows[i].Cells["irsc_rmk"].Value.ToString();
                    oItemCondition.irsc_cre_by = BaseCls.GlbUserID;
                    oItemCondition.irsc_cre_dt = DateTime.Now;
                    oItemCondition.irsc_stus = "A";
                    oItemCondition.ItemCode = txtItemCode.Text.Trim();

                    result = CHNLSVC.General.Save_itemConditions(oItemCondition);
                }
                if (result > 0)
                {
                    MessageBox.Show("Process succeeded.", "Item Condition Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearWithoutGrid();
                }
                else
                {
                    MessageBox.Show("Process Terminated.", "Item Condition Setup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please add items to grid.", "Item Condition Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearWithoutGrid();
            //txtItemCode.Clear();
            //txtSerialID.Clear();
            //txtSerialNo.Clear();
            //txtRemark.Clear();
            //txtSerialNo.Focus();
            //lblItemStatus.Text = "";
            //btnSave.Enabled = true;
            //dgvHistory.DataSource = null;
            //oItemConditionSetups.Clear();
            //lblDiscription.Text = "";
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_Item_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtSerialNo.Focus();
            }
        }

        private void btnAmend_Click(object sender, EventArgs e)
        {
            DialogResult dgr = MessageBox.Show("Do you want to update?", "Item Condition Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dgr == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            MasterItem _itemdetail = new MasterItem();

            _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCode.Text);
            if (_itemdetail != null)
            {
                if (_itemdetail.Mi_is_cond == 1)
                {
                    ItemConditionSetup oItemCondition = new ItemConditionSetup();
                    oItemCondition.irsc_ser_id = Convert.ToInt32(txtSerialID.Text);
                    oItemCondition.irsc_line = 0;
                    oItemCondition.irsc_com = BaseCls.GlbUserComCode;
                    oItemCondition.irsc_loc = BaseCls.GlbUserDefLoca;
                    oItemCondition.irsc_cat = ddlCategories.SelectedValue.ToString();
                    oItemCondition.irsc_tp = string.Empty;
                    oItemCondition.irsc_rmk = txtRemark.Text;
                    oItemCondition.irsc_cre_by = BaseCls.GlbUserID;
                    oItemCondition.irsc_cre_dt = DateTime.Now;
                    //if (ddlStatus.SelectedText == "Active")
                    //{
                    oItemCondition.irsc_stus = "A";
                    oItemCondition.StatusText = "Active";
                    oItemCondition.ItemCode = txtItemCode.Text.Trim();
                    //}
                    //else
                    //{
                    //    oItemCondition.irsc_stus = "R";
                    //    oItemCondition.StatusText = "Cancel";
                    //}

                    int result = CHNLSVC.General.Update_itemConditions(oItemCondition);
                    if (result > 0)
                    {
                        MessageBox.Show("Process succeeded.", "Item Condition Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearWithoutGrid();
                    }
                    else
                    {
                        MessageBox.Show("Process Terminated.", "Item Condition Setup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("This item is not eligible to add condition.", "Item Condition Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dgr = MessageBox.Show("Do you want to cancel?", "Item Condition Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dgr == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                int result = 0;
                if (isAnySelected())
                {
                    for (int i = 0; i < dgvHistory.Rows.Count; i++)
                    {
                        if (dgvHistory.Rows[i].Cells["test"].Value != null && dgvHistory.Rows[i].Cells["test"].Value.ToString().ToUpper() == "TRUE")
                        {
                            ItemConditionSetup oItemCondition = new ItemConditionSetup();
                            oItemCondition.irsc_ser_id = Convert.ToInt32(dgvHistory.Rows[i].Cells["irsc_ser_id"].Value.ToString());
                            oItemCondition.irsc_line = 0;
                            oItemCondition.irsc_com = BaseCls.GlbUserComCode;
                            oItemCondition.irsc_loc = BaseCls.GlbUserDefLoca;
                            oItemCondition.irsc_cat = dgvHistory.Rows[i].Cells["irsc_cat"].Value.ToString();
                            oItemCondition.irsc_tp = string.Empty;
                            oItemCondition.irsc_rmk = dgvHistory.Rows[i].Cells["irsc_rmk"].Value.ToString();
                            oItemCondition.irsc_cre_by = BaseCls.GlbUserID;
                            oItemCondition.irsc_cre_dt = DateTime.Now;
                            oItemCondition.irsc_stus = "R";
                            oItemCondition.ItemCode = txtItemCode.Text.Trim();
                            result = CHNLSVC.General.Update_itemConditions(oItemCondition);
                        }
                    }
                    MessageBox.Show("Process succeeded.", "Item Condition Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearWithoutGrid();
                }
                else
                {
                    MessageBox.Show("Please Select records.", "Item Condition Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnSearch_Serial1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItemCode.Text))
                {
                    _SerialSearchType = "SER1_WOITEM";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
                    DataTable _result = CHNLSVC.CommonSearch.Search_inr_ser_infor(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSerialNo;
                    _CommonSearch.ShowDialog();
                    txtSerialNo.Select();
                }
                else
                {
                    if (string.IsNullOrEmpty(txtItemCode.Text))
                    {
                        MessageBox.Show("Please select the item code", "Item Code", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItemCode.Focus();
                        return;
                    }
                    else
                    {
                        txtSerialID.Text = "";
                        txtSerialNo.Text = "";
                        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                        _CommonSearch.ReturnIndex = 0;
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SerialNonSerial);
                        DataTable _result = CHNLSVC.CommonSearch.GetAvailableSerialNonSerialSearchData(_CommonSearch.SearchParams, null, null);
                        //var ordered = _result.AsEnumerable().ToList().OrderBy(a=>a[""]);
                        _CommonSearch.dvResult.DataSource = _result;
                        _CommonSearch.BindUCtrlDDLData(_result);
                        _CommonSearch.obj_TragetTextBox = txtSerialID;
                        _CommonSearch.dvResult.Sort(_CommonSearch.dvResult.Columns["SEQ. ID"], ListSortDirection.Ascending);

                        _CommonSearch.ShowDialog();
                        txtSerialID.Select();
                    }
                }

                LoadSerialData();
                FillItemDetails();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtItemCode;
            _CommonSearch.ShowDialog();
            txtItemCode.Select();
        }

        private void txtSerialNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_Serial1_Click(null, null);
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                {
                    txtSerialNo_Leave(null, null);
                }
                ddlCategories.Focus();
            }
        }

        private void txtSerialNo_Leave(object sender, EventArgs e)
        {
            FillItemDetails();
            FillSerialHistory();
        }

        private void txtSerialNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Serial1_Click(null, null);
        }

        private void txtItemCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Item_Click(null, null);
        }

        private void dgvHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvHistory.Rows.Count > 0)
            {
                if (e.ColumnIndex == 1)
                {
                    ItemConditionSetup oItemCondition = new ItemConditionSetup();
                    oItemCondition = oMainList.Find(x => x.irsc_ser_id == Convert.ToInt32(dgvHistory.Rows[e.RowIndex].Cells["irsc_ser_id"].Value.ToString()));
                    txtSerialID.Text = oItemCondition.irsc_ser_id.ToString();
                    txtSerialNo.Text = oItemCondition.ItemSearial;
                    txtItemCode.Clear();
                    FillItemDetails();
                    txtRemark.Text = oItemCondition.irsc_rmk;

                    if (oItemCondition.irsc_cat == "CT001")
                    {
                        ddlCategories.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlCategories.SelectedIndex = 1;
                    }
                }
                else if (e.ColumnIndex == 0)
                {
                    DialogResult dgr = MessageBox.Show("Do you want to delete this record?", "Item Condition Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dgr == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    oMainList.Remove(oMainList.Find(x => x.irsc_ser_id == Convert.ToInt32(dgvHistory.Rows[e.RowIndex].Cells["irsc_ser_id"].Value.ToString())));
                    dgvHistory.DataSource = null;
                    if (oMainList.Count > 0)
                    {
                        dgvHistory.DataSource = oMainList;
                    }
                }
            }
        }

        private void label28_Click(object sender, EventArgs e)
        {
            pnlMultipleItem.Visible = false;
        }

        private void gvMultipleItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvMultipleItem.RowCount > 0)
            {
                {
                    string _item = gvMultipleItem.SelectedRows[0].Cells["MuItm_Item"].Value.ToString();
                    string _serial = gvMultipleItem.SelectedRows[0].Cells["MuItm_Serial"].Value.ToString();

                    txtSerialNo.Text = _serial;
                    txtItemCode.Text = _item;
                    pnlMultipleItem.Visible = false;
                    FillItemDetails();
                }
            }
        }

        private void ddlCategories_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRemark.Focus();
            }
        }

        private void btnAddtoGrid_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtItemCode.Text) || string.IsNullOrEmpty(txtSerialID.Text))
            {
                MessageBox.Show("Please Select a Serial.", "Item Condition Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtRemark.Text))
            {
                MessageBox.Show("Please enter a Remark.", "Item Condition Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MasterItem _itemdetail = new MasterItem();

            _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCode.Text);
            if (_itemdetail != null)
            {
                if (_itemdetail.Mi_is_cond == 1)
                {
                    ItemConditionSetup oItemCondition = new ItemConditionSetup();
                    oItemCondition.irsc_ser_id = Convert.ToInt32(txtSerialID.Text);
                    oItemCondition.irsc_line = 0;
                    oItemCondition.irsc_com = BaseCls.GlbUserComCode;
                    oItemCondition.irsc_loc = BaseCls.GlbUserDefLoca;
                    oItemCondition.irsc_cat = ddlCategories.SelectedValue.ToString();
                    oItemCondition.irsc_catText = ddlCategories.Text.ToString();
                    oItemCondition.irsc_tp = string.Empty;
                    oItemCondition.irsc_rmk = txtRemark.Text;
                    oItemCondition.irsc_cre_by = BaseCls.GlbUserID;
                    oItemCondition.irsc_cre_dt = DateTime.Now;
                    oItemCondition.irsc_stus = "A";
                    oItemCondition.StatusText = "Active";
                    oItemCondition.ItemSearial = txtSerialNo.Text;
                    oItemCondition.rcc_desc = lblDiscription.Text;
                    oItemCondition.ItemCode = txtItemCode.Text.Trim();
                    addtoMainList(oItemCondition);
                    isNewitemExsists = true;
                }
                else
                {
                    MessageBox.Show("This item is not eligible to add condition.", "Item Condition Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            dgvHistory.DataSource = null;
            if (oMainList.Count > 0)
            {
                dgvHistory.DataSource = oMainList;
            }

            txtItemCode.Clear();
            txtSerialID.Clear();
            txtSerialNo.Clear();
            lblDiscription.Text = "";
            lblItemStatus.Text = "";
            txtRemark.Clear();
            ddlCategories.SelectedIndex = 0;
        }

        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            getItemDetails();
        }

        private void btnReActive_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dgr = MessageBox.Show("Do you want to Re-Active?", "Item Condition Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dgr == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                int result = 0;
                if (isAnySelected())
                {
                    for (int i = 0; i < dgvHistory.Rows.Count; i++)
                    {
                        if (dgvHistory.Rows[i].Cells["test"].Value != null && dgvHistory.Rows[i].Cells["test"].Value.ToString().ToUpper() == "TRUE")
                        {
                            ItemConditionSetup oItemCondition = new ItemConditionSetup();
                            oItemCondition.irsc_ser_id = Convert.ToInt32(dgvHistory.Rows[i].Cells["irsc_ser_id"].Value.ToString());
                            oItemCondition.irsc_line = 0;
                            oItemCondition.irsc_com = BaseCls.GlbUserComCode;
                            oItemCondition.irsc_loc = BaseCls.GlbUserDefLoca;
                            oItemCondition.irsc_cat = dgvHistory.Rows[i].Cells["irsc_cat"].Value.ToString();
                            oItemCondition.irsc_tp = string.Empty;
                            oItemCondition.irsc_rmk = dgvHistory.Rows[i].Cells["irsc_rmk"].Value.ToString();
                            oItemCondition.irsc_cre_by = BaseCls.GlbUserID;
                            oItemCondition.irsc_cre_dt = DateTime.Now;
                            oItemCondition.irsc_stus = "A";
                            oItemCondition.ItemCode = txtItemCode.Text.Trim();
                            result = CHNLSVC.General.Update_itemConditions(oItemCondition);
                        }
                    }
                    MessageBox.Show("Process succeeded.", "Item Condition Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearWithoutGrid();
                }
                else
                {
                    MessageBox.Show("Please Select records.", "Item Condition Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnReActive_Click(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnCancel_Click(null, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            btnSave_Click(null, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            btnClear_Click(null, null);
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked)
            {
                for (int i = 0; i < dgvHistory.Rows.Count; i++)
                {
                    {
                        dgvHistory.Rows[i].Cells["Test"].Value = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < dgvHistory.Rows.Count; i++)
                {
                    {
                        dgvHistory.Rows[i].Cells["Test"].Value = false;
                    }
                }
            }
        }

        #endregion Events

        #region Methods

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(_receCompany + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes:
                    {
                        paramsText.Append(_SerialSearchType + seperator + BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + txtItemCode.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType:
                    {
                        paramsText.Append("ADJ" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        // paramsText.Append(ITEM_CODE + seperator + ITEM_STATUS + seperator + COMPANY + seperator + CHANNEL + seperator + SUB_CHANNEL + seperator + AREA + seperator + REAGION + seperator + ZONE + seperator + LOC + seperator + TYPE + seperator);
                        paramsText.Append(txtItemCode.Text + seperator + "GOD" + seperator + BaseCls.GlbUserComCode + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + BaseCls.GlbUserDefLoca + seperator + "Loc" + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch:
                //    {
                //        if (ddlAdjTypeSearch.Text == "ADJ+") paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "ADJ" + seperator + "1" + seperator);
                //        else paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "ADJ" + seperator + "0" + seperator);

                //        break;
                //    }

                case CommonUIDefiniton.SearchUserControlType.SerialNonSerial:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + txtItemCode.Text.ToUpper() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void FillCategories()
        {
            List<ItemConditionCategori> oonditionCategoris = new List<ItemConditionCategori>();
            oonditionCategoris = CHNLSVC.General.Get_ItemConditionCategories(BaseCls.GlbUserComCode);
            ddlCategories.DataSource = oonditionCategoris;
            ddlCategories.DisplayMember = "rcc_desc";
            ddlCategories.ValueMember = "rcc_cate";
        }

        private void FillSerialHistory()
        {
            if (!string.IsNullOrEmpty(txtSerialID.Text))
            {
                oItemConditionSetups = CHNLSVC.General.Get_ItemConditionBySerial(BaseCls.GlbUserComCode, txtSerialID.Text, BaseCls.GlbUserDefLoca, null);
                if (oItemConditionSetups != null && oItemConditionSetups.Count > 0)
                {
                    txtRemark.Text = oItemConditionSetups[0].irsc_rmk;

                    foreach (ItemConditionSetup item in oItemConditionSetups)
                    {
                        item.ItemSearial = txtSerialNo.Text;
                        if (item.irsc_stus == "A")
                        {
                            item.StatusText = "Active";
                        }
                        else
                        {
                            item.StatusText = "Cancel";
                        }
                    }

                    foreach (ItemConditionSetup item in oItemConditionSetups)
                    {
                        addtoMainList(item);
                    }

                    dgvHistory.DataSource = null;
                    if (oMainList.Count > 0)
                    {
                        dgvHistory.DataSource = oMainList;
                    }
                }
            }
        }

        private void LoadSerialData()
        {
            if (txtSerialID.Text != "")
            {
                ReptPickSerials _serial = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text.Trim(), "N/A", null, txtSerialID.Text);
                if (_serial.Tus_itm_cd != null)
                {
                    txtSerialNo.Text = _serial.Tus_ser_1.ToString();
                    FillSerialHistory();
                }
            }
        }

        private void FillItemDetails()
        {
            DataTable _serialTbl = null;

            if (string.IsNullOrEmpty(txtItemCode.Text))
            {
                if (string.IsNullOrEmpty(txtSerialNo.Text))
                {
                    return;
                }
                _serialTbl = CHNLSVC.Inventory.getDetail_on_serial1(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtSerialNo.Text);

                if (_serialTbl == null)
                {
                    MessageBox.Show("This serial no is not available! Please check again.", "Serial Scanning . . . ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSerialNo.Clear();
                    return;
                }
                if (_serialTbl.Rows.Count <= 0)
                {
                    MessageBox.Show("This serial no is not available! Please check again.", "Serial Scanning . . . ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSerialNo.Clear();
                    return;
                }
                if (_serialTbl.Rows.Count > 1)
                {
                    FillMultileSerials();
                    return;
                }
                else
                {
                    foreach (DataRow r in _serialTbl.Rows)
                    {
                        txtItemCode.Text = (string)r["INS_ITM_CD"].ToString();
                        txtSerialNo.Text = (string)r["INS_SER_1"].ToString();
                        txtSerialID.Text = (string)r["ins_ser_id"].ToString();
                        lblItemStatus.Text = (string)r["INS_ITM_STUS"].ToString();
                        lblDiscription.Text = CHNLSVC.Inventory.getItemDescription((string)r["INS_ITM_CD"].ToString());
                    }
                    return;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtItemCode.Text))
                {
                    if (string.IsNullOrEmpty(txtSerialNo.Text))
                    {
                        return;
                    }
                    string _txtSerialNo = string.Empty;
                    string _txtSer2 = string.Empty;

                    List<ReptPickSerials> _list = CHNLSVC.Inventory.Search_by_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text.Trim(), BaseCls.GlbDefaultBin, txtSerialNo.Text, _txtSer2);
                    if (_list == null)
                    {
                        MessageBox.Show("This serial no is not available! Please check again.", "Serial Scanning . . . ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSerialNo.Clear();
                        return;
                    }
                    if (_list.Count <= 0)
                    {
                        MessageBox.Show("This serial no is not available! Please check again.", "Serial Scanning . . . ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSerialNo.Clear();
                        return;
                    }
                    if (_list.Count > 1)
                    {
                       // FillMultileSerials();
                        return;
                    }

                    if (_list.Count == 1)
                    {
                        txtItemCode.Text = _list[0].Tus_itm_cd;
                        txtSerialNo.Text = _list[0].Tus_ser_1;
                        txtSerialID.Text = _list[0].Tus_ser_id.ToString();
                        lblItemStatus.Text = _list[0].Tus_itm_stus;
                        lblDiscription.Text = CHNLSVC.Inventory.getItemDescription(_list[0].Tus_itm_cd);

                        txtRemark.Clear();
                        ddlCategories.SelectedIndex = 0;

                        return;
                    }
                }
            }
        }

        private void FillMultileSerials()
        {
            DataTable _multiItemforSerial = CHNLSVC.Inventory.GetMultipleItemforOneSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtSerialNo.Text.Trim(), string.Empty);
            gvMultipleItem.DataSource = _multiItemforSerial;
            pnlMultipleItem.Visible = true;
        }

        private void ClearWithoutGrid()
        {
            txtItemCode.Clear();
            txtSerialID.Clear();
            txtSerialNo.Clear();
            txtRemark.Clear();
            txtSerialNo.Focus();
            lblItemStatus.Text = "";
            dgvHistory.DataSource = null;
            oItemConditionSetups.Clear();
            lblDiscription.Text = "";
            ddlCategories.SelectedIndex = 0;
            oMainList.Clear();
            isNewitemExsists = false;
            chkSelectAll.Checked = false;
            dgvHistory.Refresh();
            chkLoadHistory.Checked = false;
        }

        private void addtoMainList(ItemConditionSetup NewItem)
        {
            if (oMainList.Count > 0)
            {
                oMainList.Remove(oMainList.Find(x => x.irsc_ser_id == NewItem.irsc_ser_id));
            }
            oMainList.Add(NewItem);
        }

        private void getItemDetails()
        {
            MasterItem _itemdetail = new MasterItem();
            if (!string.IsNullOrEmpty(txtItemCode.Text))
            {
                _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCode.Text);
                if (_itemdetail != null)
                {
                    lblDiscription.Text = _itemdetail.Mi_shortdesc;
                    if (chkLoadHistory.Checked)
                    {
                        GetConditionsByItemCode(txtItemCode.Text);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter correct item code.", "Item Condition Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemCode.Clear();
                    txtItemCode.Focus();
                }
            }
        }

        private bool isAnySelected()
        {
            bool status = false;
            try
            {
                foreach (DataGridViewRow row in dgvHistory.Rows)
                {
                    if (row.Cells["test"].Value != null)
                    {
                        string asdasd = row.Cells["test"].Value.ToString().ToUpper();
                        if (asdasd == "TRUE")
                        {
                            status = true;
                            break;
                        }
                    }
                }
                return status;
            }
            catch (Exception ex)
            {
                return status;
            }
        }

        private void GetConditionsByItemCode(string ItemCOde)
        {
            if (!isNewitemExsists)
            {
                oMainList.Clear();
            }
            oItemConditionSetups = CHNLSVC.General.Get_ItemConditionBySerial(BaseCls.GlbUserComCode, null, BaseCls.GlbUserDefLoca, ItemCOde).FindAll(x => x.irsc_loc == BaseCls.GlbUserDefLoca);
            if (oItemConditionSetups != null && oItemConditionSetups.Count > 0)
            {
                foreach (ItemConditionSetup item in oItemConditionSetups)
                {
                    item.ItemCode = txtItemCode.Text;
                    if (item.irsc_stus == "A")
                    {
                        item.StatusText = "Active";
                    }
                    else
                    {
                        item.StatusText = "Cancel";
                    }
                }
                foreach (ItemConditionSetup item in oItemConditionSetups)
                {
                    addtoMainList(item);
                }

                dgvHistory.DataSource = null;
                if (oMainList.Count > 0)
                {
                    dgvHistory.DataSource = oMainList;
                }
            }
        }

        #endregion Methods

        private void dgvHistory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (dgvHistory.SelectedRows.Count > 0)
                {
                    if (dgvHistory.SelectedRows[0].Cells["test"].Value != null)
                    {
                        if (dgvHistory.SelectedRows[0].Cells["test"].Value.ToString().ToUpper() == "true".ToUpper())
                        {
                            dgvHistory.SelectedRows[0].Cells["test"].Value = false;
                        }
                        else
                        {
                            dgvHistory.SelectedRows[0].Cells["test"].Value = true;
                        }
                    }
                    else
                    {
                        dgvHistory.SelectedRows[0].Cells["test"].Value = true;
                    }
                }
            }
        }

        private void chkLoadHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLoadHistory.Checked)
            {
                txtItemCode_Leave(null, null);
            }
            else
            {
                btnClear_Click(null,null);
            }
        }
    }
}