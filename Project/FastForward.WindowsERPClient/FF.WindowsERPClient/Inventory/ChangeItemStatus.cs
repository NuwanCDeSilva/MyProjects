using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using FF.BusinessObjects;
using FF.Interfaces;
using FF.WindowsERPClient.Reports.Sales;
using FF.WindowsERPClient.Reports.Inventory;

namespace FF.WindowsERPClient.Inventory
{
    public partial class ChangeItemStatus : FF.WindowsERPClient.Base
    {
        #region Varialbe
        private string _SerialSearchType = string.Empty;
        private List<InventoryRequestItem> ScanItemList = null;
        private string _receCompany = string.Empty;
        private string RequestNo = string.Empty;
        private string SelectedStatus = string.Empty;
        private MasterItem _itemdetail = null;
        bool _isDecimalAllow = false;
        private List<ReptPickSerials> SelectedItemList = null;
        private string _chargeType = string.Empty;
        private List<string> SeqNumList = null;
        private Boolean _isStusChngPerm = false;
        #endregion

        #region Rooting for Form Initializing
        class DocumentType
        {
            string _displayMemener = string.Empty;
            string _valueMemeber = string.Empty;

            public string DisplayMemener
            {
                get { return _displayMemener; }
                set { _displayMemener = value; }
            }
            public string ValueMemeber
            {
                get { return _valueMemeber; }
                set { _valueMemeber = value; }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you need to close " + this.Text + "?", "Closing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void InitializeForm(bool _isDocType)
        {
            try
            {
                dtpDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
                this.Cursor = Cursors.WaitCursor;

                SelectedItemList = new List<ReptPickSerials>();
                ScanItemList = new List<InventoryRequestItem>();
                _itemdetail = new MasterItem();
                gvItems.AutoGenerateColumns = false;
                gvSerial.AutoGenerateColumns = false;
                //ddlAdjType.SelectedIndex = 0;
                ddlAdjTypeSearch.SelectedIndex = 0;
                BindUserCompanyItemStatusDDLData(ddlStatus);
                BindUserCompanyItemStatusDDLData(ddlNewStatus);
                BindUserSeqNos();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
            }
            finally
            {
                //BindPickSerials(0);
                //BackDatePermission();
                RequestNo = string.Empty;
                this.Cursor = Cursors.Default;
            }
        }
        public ChangeItemStatus()
        {
            //_commonOutScan = new CommonSearch.CommonOutScan();
            InitializeComponent();
            InitializeForm(true);
            //_commonOutScan.AddSerialClick += new EventHandler(_commonOutScan_AddSerialClick);
        }

        private void BindUserCompanyItemStatusDDLData(ComboBox ddl)
        {
            DataTable _tbl = CHNLSVC.Inventory.GetAllCompanyStatus(BaseCls.GlbUserComCode); ;
            var _s = (from L in _tbl.AsEnumerable()
                      select new
                      {
                          MIS_DESC = L.Field<string>("MIS_DESC"),
                          MIC_CD = L.Field<string>("MIC_CD")
                      }).ToList();
            var _n = new { MIS_DESC = string.Empty, MIC_CD = string.Empty };
            _s.Insert(0, _n);
            ddl.DataSource = _s;
            ddl.DisplayMember = "MIS_DESC";
            ddl.ValueMember = "MIC_CD";
            ddl.Text = "GOOD";
        }

        private void BindUserSeqNos()
        {
            SeqNumList = CHNLSVC.Inventory.Get_User_Seq_Batch(BaseCls.GlbUserID, "ADJ-S", 0, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            ddlSeqNo.DataSource = SeqNumList;
        }
        #endregion

        #region Rooting for Screen Navigation
        private void txtDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtManualRef.Focus();
        }

        private void txtManualRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtOtherRef.Focus();
        }

        private void txtOtherRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRemarks.Focus();
        }

        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtItem.Focus();
        }

        private void txtUnitCost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtQty.Focus();
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ddlNewStatus.Focus();
        }

        private void txtAdjSubType_TextChanged(object sender, EventArgs e)
        {
            lblSubTypeDesc.Text = string.Empty;
        }

        private void ddlAdjType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeqNumList = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(BaseCls.GlbUserID, "ADJ", 0, BaseCls.GlbUserComCode);
            ddlSeqNo.DataSource = SeqNumList;
        }

        private void ddlSeqNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlSeqNo.Text))
            {
                txtUserSeqNo.Text = ddlSeqNo.Text;
                LoadItems(txtUserSeqNo.Text);
            }
        }


        private void btnSearch_DocumentNo_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(_CommonSearch.SearchParams, null, null, dtpDate.Value.Date.AddMonths(-1), dtpDate.Value.Date);
                _CommonSearch.dtpFrom.Value = dtpDate.Value.Date.AddMonths(-1);
                _CommonSearch.dtpTo.Value = dtpDate.Value.Date;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDocumentNo;
                _CommonSearch.ShowDialog();
                txtDocumentNo.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDocumentNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDocumentNo.Text))
                {
                    bool _invalidDoc = true;
                    int _direction = 0;
                    int _lineNo = 0;
                    if (ddlAdjTypeSearch.Text == "ADJ+") _direction = 1;

                    #region Clear Data
                    List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                    gvSerial.AutoGenerateColumns = false;
                    gvSerial.DataSource = _emptySer;

                    List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                    gvItems.AutoGenerateColumns = false;
                    gvItems.DataSource = _emptyItm;

                    btnSave.Enabled = true;
                    lblSubTypeDesc.Text = "";
                    txtManualRef.Text = string.Empty;
                    txtOtherRef.Text = string.Empty;
                    txtRemarks.Text = string.Empty;
                    txtUserSeqNo.Clear();
                    #endregion

                    InventoryHeader _invHdr = new InventoryHeader();

                    _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(txtDocumentNo.Text);
                    #region Check Valid Document No
                    if (_invHdr == null)
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_doc_tp != "ADJ")
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_direct == true && _direction == 0)
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_direct == false && _direction == 1)
                    {
                        _invalidDoc = false;
                        goto err;
                    }

                err:
                    if (_invalidDoc == false)
                    {
                        MessageBox.Show("Invalid Document No!", "Adjustment No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDocumentNo.Clear();
                        txtDocumentNo.Focus();
                        return;
                    }
                    else
                    {
                        btnSave.Enabled = false;
                        txtManualRef.Text = _invHdr.Ith_manual_ref;
                        txtOtherRef.Text = _invHdr.Ith_entry_no;
                        txtRemarks.Text = _invHdr.Ith_remarks;
                        txtUserSeqNo.Clear();
                        ddlSeqNo.Text = string.Empty;
                    }
                    #endregion

                    #region Get Serials
                    List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.Get_Int_Ser(txtDocumentNo.Text);
                    if (_serList != null)
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand, x.Tus_unit_cost }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {
                            _lineNo += 1;
                            InventoryRequestItem _itm = new InventoryRequestItem();
                            _itm.Itri_app_qty = Convert.ToDecimal(itm.theCount);
                            _itm.Itri_itm_cd = itm.Peo.Tus_itm_cd;
                            _itm.Itri_itm_stus = itm.Peo.Tus_itm_stus;
                            _itm.Itri_line_no = _lineNo;
                            _itm.Itri_qty = Convert.ToDecimal(itm.theCount);
                            _itm.Mi_longdesc = itm.Peo.Tus_itm_desc;
                            _itm.Mi_model = itm.Peo.Tus_itm_model;
                            _itm.Mi_brand = itm.Peo.Tus_itm_brand;
                            _itm.Itri_unit_price = itm.Peo.Tus_unit_cost;
                            _itmList.Add(_itm);

                        }
                        ScanItemList = _itmList;

                        gvItems.AutoGenerateColumns = false;
                        gvItems.DataSource = ScanItemList;

                        gvSerial.AutoGenerateColumns = false;
                        gvSerial.DataSource = _serList;

                    }
                    else
                    {
                        MessageBox.Show("Item not found!", "Adjustment No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDocumentNo.Clear();
                        txtDocumentNo.Focus();
                        return;
                    }
                    #endregion

                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDocumentNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtManualRef.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_DocumentNo_Click(null, null);
        }

        private void txtDocumentNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkDirectScanningSer1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDirectScanningSer1.Checked == true)
            {
                chkDirectScanningSer2.Checked = false;
                txtItem.Clear();
                txtItem.Enabled = false;
                btnSearch_Item.Enabled = false;
                txtSer1.Clear();
                txtSer1.Enabled = true;
                btnSearch_Serial1.Enabled = true;
                txtSer2.Clear();
                txtSer2.Enabled = false;
                btnSearch_Serial2.Enabled = false;
                ddlStatus.SelectedText = string.Empty;
                ddlStatus.Enabled = false;
                txtQty.Enabled = false;
                txtQty.Text = "1";
                txtSer1.Focus();
            }
            else
            {
                ClearItemDetail();
            }
        }

        private void chkDirectScanningSer2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDirectScanningSer2.Checked == true)
            {
                chkDirectScanningSer1.Checked = false;
                txtItem.Clear();
                txtItem.Enabled = false;
                btnSearch_Item.Enabled = false;
                txtSer1.Clear();
                txtSer1.Enabled = false;
                btnSearch_Serial1.Enabled = false;
                txtSer2.Clear();
                txtSer2.Enabled = true;
                btnSearch_Serial2.Enabled = true;
                ddlStatus.SelectedText = string.Empty;
                ddlStatus.Enabled = false;
                txtQty.Enabled = false;
                txtQty.Text = "1";
                txtSer2.Focus();
            }
            else
            {
                ClearItemDetail();
            }
        }

        #endregion

        #region Rooting for Load Item Detail
        private bool LoadItemDetail(string _item, bool _focus)
        {
            bool _isValid = false;
            try
            {
                _chargeType = string.Empty;
                lblItemDescription.Text = "Description : " + string.Empty;
                lblItemModel.Text = "Model : " + string.Empty;
                lblItemBrand.Text = "Brand : " + string.Empty;
                lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
                _itemdetail = new MasterItem();

                if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                if (_itemdetail != null)
                {
                    if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        _isValid = true;
                        _chargeType = _itemdetail.Mi_chg_tp;
                        string _description = _itemdetail.Mi_longdesc;
                        string _model = _itemdetail.Mi_model;
                        string _brand = _itemdetail.Mi_brand;
                        string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Serialized" : "Non-Serialized";

                        lblItemDescription.Text = "Description : " + _description;
                        lblItemModel.Text = "Model : " + _model;
                        lblItemBrand.Text = "Brand : " + _brand;
                        lblItemSerialStatus.Text = "Serial Status : " + _serialstatus;
                        _isDecimalAllow = CHNLSVC.Inventory.IsUOMDecimalAllow(_item);
                        if (_focus == true)
                        {
                            if (_itemdetail.Mi_is_ser1 == 1)
                            {
                                if (txtSer1.Enabled == true)
                                {
                                    txtSer1.Focus();
                                }
                            }
                            else
                            {
                                ddlStatus.Focus();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid item code", "Item Code", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Clear();
                    lblItemDescription.Text = "Description : " + string.Empty;
                    lblItemModel.Text = "Model : " + string.Empty;
                    lblItemBrand.Text = "Brand : " + string.Empty;
                    lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
                    //txtUnitCost.Text = string.Empty;
                    txtQty.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                _isValid = false;
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return _isValid;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            return _isValid;
        }
        #endregion

        #region Rooting for Additional Searching
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
                        paramsText.Append(_SerialSearchType + seperator + BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + txtItem.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType:
                    {
                        paramsText.Append("ADJ" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch:
                    {
                        if (ddlAdjTypeSearch.Text == "ADJ+") paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "ADJ" + seperator + "1" + seperator);
                        else paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "ADJ" + seperator + "0" + seperator);

                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }


        private void btnSearch_Request_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchType = "ITEMS";
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtItem;
            _CommonSearch.ShowDialog();
            txtItem.Select();
        }

        private void txtRequest_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Request_Click(null, null);
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Item_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                ddlStatus.Focus();
        }

        private void txtItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Item_Click(null, null);
        }

        #endregion

        #region Rooting for Clear Item Detail
        private void ClearItemDetail()
        {
            chkDirectScanningSer1.Checked = false;
            chkDirectScanningSer2.Checked = false;
            txtItem.Clear();
            txtItem.Enabled = true;
            btnSearch_Item.Enabled = true;
            txtSer1.Clear();
            txtSer2.Clear();
            txtSer1.Enabled = true;
            txtSer2.Enabled = true;
            btnSearch_Serial2.Enabled = true;
            ddlStatus.SelectedText = string.Empty;
            ddlStatus.Enabled = true;
            txtQty.Enabled = true;
            txtQty.Text = string.Empty;

            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;
            lblItemSerialStatus.Text = "Serial Status : " + string.Empty;

            //ddlStatus.DataSource = null;
            //gvBalance.DataSource = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, string.Empty);
        }
        #endregion

        #region Rooting for Check Item
        protected void CheckItem(bool _checkStatus, bool _focus)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtItem.Text)) return;

                if (LoadItemDetail(txtItem.Text.Trim(), _focus) == false)
                {
                    txtItem.Focus();
                    return;
                }

                DataTable _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), string.Empty);
                if (_inventoryLocation == null || _inventoryLocation.Rows.Count <= 0)
                {
                    MessageBox.Show("No stock balance available", "Qty Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ddlStatus.DataSource = new List<InventoryLocation>();
                    return;
                }
                else
                {
                    if (_checkStatus == true)
                    {
                        ddlStatus.DataSource = null;
                        ddlStatus.DataSource = _inventoryLocation;
                        ddlStatus.DisplayMember = "mis_desc";
                        ddlStatus.ValueMember = "inl_itm_stus";
                    }
                }

                gvBalance.DataSource = null;
                gvBalance.AutoGenerateColumns = false;
                gvBalance.DataSource = _inventoryLocation;


            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
            finally { this.Cursor = Cursors.Default; }

        }
        private void txtItem_Leave(object sender, EventArgs e)
        {
            CheckItem(true, true);
        }
        #endregion

        #region Rooting for Check Qty
        protected void CheckQty()
        {
            try
            {
                if (string.IsNullOrEmpty(txtQty.Text)) return;

                if (string.IsNullOrEmpty(txtItem.Text.Trim()))
                {
                    MessageBox.Show("Please select the item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(ddlStatus.Text.ToString()))
                {
                    MessageBox.Show("Please select the status", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ddlStatus.Focus();
                    return;
                }

                decimal _formQty = Convert.ToDecimal(txtQty.Text);
                if (ScanItemList != null && ScanItemList.Count > 0) 
                {
                    var _pickItems = ScanItemList.Where(x => x.Itri_itm_cd == txtItem.Text.Trim().ToUpper() && x.Itri_itm_stus == ddlStatus.SelectedValue.ToString()).ToList();
                    if (_pickItems != null && _pickItems.Count > 0)
                    {
                        _formQty += _pickItems.Sum(x => x.Itri_app_qty);
                    }
                }

                this.Cursor = Cursors.Default;
                //check for the location balance.
                List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString());
                if (_inventoryLocation.Count == 1)
                {
                    foreach (InventoryLocation _loc in _inventoryLocation)
                    {
                        
                        if (_formQty > _loc.Inl_free_qty)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Please check the inventory balance!", "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtQty.Text = string.Empty;
                            txtQty.Focus();
                            return;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
            finally { this.Cursor = Cursors.Default; }
        }
        private void txtQty_Leave(object sender, EventArgs e)
        {
            CheckQty();
        }
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(_isDecimalAllow, sender, e);
        }
        #endregion

        #region Rooting for Add Item
        protected void AddItem()
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text.Trim()))
                {
                    MessageBox.Show("Please select the item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(ddlStatus.Text.ToString()))
                {
                    MessageBox.Show("Please select the item status", "Current Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ddlStatus.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    MessageBox.Show("Please select the qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(ddlNewStatus.Text.ToString()))
                {
                    MessageBox.Show("Please select the new item status", "New Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ddlNewStatus.Focus();
                    return;
                }
                if (ddlNewStatus.SelectedValue.ToString() == "CONS")
                {
                    MessageBox.Show("New status cannot be Consignment!", "New Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ddlNewStatus.Focus();
                    return;
                }
                if (ddlStatus.SelectedValue.ToString() == ddlNewStatus.SelectedValue.ToString())
                {
                    MessageBox.Show("New status cannot be same as available status!", "New Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ddlNewStatus.Focus();
                    return;
                }
                DataTable _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString());
                if (_inventoryLocation == null || _inventoryLocation.Rows.Count <= 0)
                {
                    MessageBox.Show("No stock balance available", "Qty Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ddlStatus.DataSource = new List<InventoryLocation>();
                    return;
                }

                foreach (DataRow dr in _inventoryLocation.Rows)
                {
                    if (Convert.ToDecimal(dr["inl_free_qty"].ToString()) <= 0)
                    {
                        MessageBox.Show("No free balance available", "Qty Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ddlStatus.DataSource = new List<InventoryLocation>();
                        return;
                    }
                }

                this.Cursor = Cursors.WaitCursor;
                Int32 _lineNo = 0;
                Int32 _serID = 0;
                bool _updateItem = false;
                MasterItem _itms = new MasterItem();
                _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());

                if (_itms.Mi_is_ser1 == 1)
                {

                    if (string.IsNullOrEmpty(txtSer1.Text.ToString()))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please select the serial no 1", "Serial No 1", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (txtSer1.Enabled == true) txtSer1.Focus();
                        if (txtSer2.Enabled == true) txtSer2.Focus();
                        return;
                    }
                    if (_itms.Mi_is_ser2 == 1)
                    {
                        if (string.IsNullOrEmpty(txtSer2.Text.ToString()))
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Please select the serial no 2", "Serial No 2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (txtSer1.Enabled == true) txtSer1.Focus();
                            if (txtSer2.Enabled == true) txtSer2.Focus();
                            return;
                        }
                    }
                }
                else
                { txtSer1.Text = "N/A"; }

                InventoryRequestItem _itm = new InventoryRequestItem();
                if (ScanItemList != null && ScanItemList.Count > 0)
                {
                    var _duplicate = from _ls in ScanItemList
                                     where _ls.Itri_itm_cd == txtItem.Text.Trim() && _ls.Itri_itm_stus == ddlStatus.SelectedValue.ToString() && _ls.Itri_note == ddlNewStatus.SelectedValue.ToString()
                                     select _ls;

                    if (_duplicate != null && _duplicate.Count() > 0)
                    {
                        this.Cursor = Cursors.Default;
                        //MessageBox.Show("Selected item already available", "Item Duplication", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //return;
                        //add by tharanga 2017/09/04 

                        //updated by akila  2017/09/19
                        _lineNo = _duplicate.FirstOrDefault().Itri_line_no;
                        decimal _qty = _duplicate.FirstOrDefault().Itri_app_qty;
                        if (_itms.Mi_is_ser1 == 1) { _qty += 1; } else { _qty += decimal.Parse(string.IsNullOrEmpty(txtQty.Text) ? "0" : txtQty.Text); }
                        ScanItemList.Where(x => x.Itri_line_no == _lineNo).ToList().ForEach(x => { x.Itri_app_qty = _qty; x.Itri_qty = _qty; });

                        //foreach (DataGridViewRow row in gvItems.Rows)
                        //{
                        //    if (row.Cells[2].Value.ToString().Equals(txtItem.Text.Trim()))
                        //    {

                        //        _lineNo = Convert.ToInt32(row.Cells[10].Value.ToString());

                        //   
                        //}

                        // _updateItem = true;
                        //goto _updateLine;
                    }
                    else
                    {
                        var _maxline = (from _ls in ScanItemList
                                        select _ls.Itri_line_no).Max();
                        _itm.Itri_app_qty = Convert.ToDecimal(txtQty.Text);
                        _itm.Itri_qty = _itm.Itri_app_qty;
                        _itm.Itri_itm_cd = txtItem.Text.Trim();
                        _itm.Itri_itm_stus = ddlStatus.SelectedValue.ToString();
                        _itm.Itri_line_no = Convert.ToInt32(_maxline) + 1;
                        _lineNo = _itm.Itri_line_no;                     
                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        _itm.Itri_unit_price = 0;
                        _itm.Itri_note = ddlNewStatus.SelectedValue.ToString();
                        ScanItemList.Add(_itm);
                    }                    
                }
                else
                {
                    _itm.Itri_app_qty = Convert.ToDecimal(txtQty.Text);
                    _itm.Itri_qty = _itm.Itri_app_qty;
                    _itm.Itri_itm_cd = txtItem.Text.Trim();
                    _itm.Itri_itm_stus = ddlStatus.SelectedValue.ToString();
                    _itm.Itri_line_no = 1;
                    _lineNo = _itm.Itri_line_no;
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    _itm.Itri_unit_price = 0;
                    _itm.Itri_note = ddlNewStatus.SelectedValue.ToString();
                    ScanItemList.Add(_itm);
                }
                //ScanItemList.Add(_itm);

            //_updateLine:

                if (string.IsNullOrEmpty(txtUserSeqNo.Text))
                {
                    GenerateNewUserSeqNo();
                }

                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                foreach (InventoryRequestItem _addedItem in ScanItemList)
                {
                    ReptPickItems _reptitm = new ReptPickItems();
                    _reptitm.Tui_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);

                    //updated by akila 2017/09/20
                    _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                    _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;
                    _reptitm.Tui_pic_itm_cd = _addedItem.Itri_line_no.ToString();
                    _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                    _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus; //Current Status
                    _reptitm.Tui_pic_itm_stus = _addedItem.Itri_note; //New Status
                    _saveonly.Add(_reptitm);                 
                }

                //DELETE all items and again add all
                //CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), string.Empty, string.Empty, 0, 3);
                //CHNLSVC.Inventory.SavePickedItems(_saveonly);

                //gvItems.DataSource = null;
                //gvItems.DataSource = ScanItemList;

                if (_itms.Mi_is_ser1 == 1)
                {
                    string _SER1 = txtSer1.Text;
                    string _SER2 = txtSer2.Text;
                   
                    if (_SER2 == "N/A") _SER2 = "";
                    List<ReptPickSerials> _list = CHNLSVC.Inventory.Search_by_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), null, _SER1, _SER2);
                    if (_list != null && _list.Count > 0)
                    {
                        CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), string.Empty, string.Empty, 0, 3);
                        CHNLSVC.Inventory.SavePickedItems(_saveonly);

                        ReptPickSerials _listItem = new ReptPickSerials();
                        foreach (ReptPickSerials _pickItem in _list)
                        {
                            txtItem.Text = _pickItem.Tus_itm_cd;
                            ddlStatus.SelectedItem = _pickItem.Tus_itm_stus;
                            _serID = _pickItem.Tus_ser_id;
                        }

                        ReptPickSerials _reptPickSerial = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, null, txtItem.Text, _serID);
                        //Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text, _serID, -1);
                        _reptPickSerial.Tus_cre_by = BaseCls.GlbUserID;
                        _reptPickSerial.Tus_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text.ToString());
                        _reptPickSerial.Tus_cre_by = BaseCls.GlbUserID;
                        _reptPickSerial.Tus_base_doc_no = txtUserSeqNo.Text;
                        _reptPickSerial.Tus_base_itm_line = _lineNo;
                        _reptPickSerial.Tus_itm_desc = lblItemDescription.Text.Replace("Description : ", "");
                        _reptPickSerial.Tus_itm_model = lblItemModel.Text.Replace("Model : ", "");
                        _reptPickSerial.Tus_itm_brand = lblItemBrand.Text.Replace("Brand : ", "");
                        _reptPickSerial.Tus_new_status = ddlNewStatus.SelectedValue.ToString();
                        _reptPickSerial.Tus_new_remarks = "N/A";
                        Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial, null);
                        if (affected_rows > 0)
                        {
                            Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text, _serID, -1);
                        }
                        //LoadItems(txtUserSeqNo.Text.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Serial details not found", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                    //KAPILA 16/6/2017
                else if (_itms.Mi_is_ser1==0)
                {
                    //string _SER1 = txtSer1.Text;
                    //string _SER2 = txtSer2.Text;
                    string _SER1 = "N/A";
                    string _SER2 = txtSer2.Text;
                    if (_SER2 == "N/A") _SER2 = "";
                    int qty = 0;
                    List<ReptPickSerials> _list = CHNLSVC.Inventory.Search_by_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(),  null, _SER1, _SER2);
                    if (_list != null && _list.Count >0)
                    {
                        //ReptPickSerials _listItem = new ReptPickSerials();
                        ReptPickSerials _reptPickSerial = new ReptPickSerials();
                        _list = _list.Where(c => c.Tus_itm_stus == ddlStatus.SelectedValue.ToString()).ToList();
                        if (_list != null && _list.Count > 0)
                        {
                            CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), string.Empty, string.Empty, 0, 3);
                            CHNLSVC.Inventory.SavePickedItems(_saveonly);

                            foreach (ReptPickSerials _pickItem in _list)
                            {
                                _reptPickSerial = _pickItem;
                                txtItem.Text = _pickItem.Tus_itm_cd;
                                //ddlStatus.SelectedItem = _pickItem.Tus_itm_stus;
                                _serID = _pickItem.Tus_ser_id;
                                //_reptPickSerial = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, null, txtItem.Text, _serID);
                                _reptPickSerial.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial.Tus_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text.ToString());
                                _reptPickSerial.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial.Tus_base_doc_no = txtUserSeqNo.Text;
                                _reptPickSerial.Tus_base_itm_line = _lineNo;
                                _reptPickSerial.Tus_itm_desc = lblItemDescription.Text.Replace("Description : ", "");
                                _reptPickSerial.Tus_itm_model = lblItemModel.Text.Replace("Model : ", "");
                                _reptPickSerial.Tus_itm_brand = lblItemBrand.Text.Replace("Brand : ", "");
                                _reptPickSerial.Tus_new_status = ddlNewStatus.SelectedValue.ToString();
                                _reptPickSerial.Tus_new_remarks = "N/A";

                                Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial, null);
                                if (affected_rows > 0)
                                {
                                    Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text, _serID, -1);
                                }
                                qty++;
                                if (qty == int.Parse(txtQty.Text.ToString()))
                                {
                                    break;
                                    //goto l1;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Serial details not found for selected status - " + ddlStatus.SelectedValue.ToString(), "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }                       
                    }
                    else
                    {
                        MessageBox.Show("Serial details not found", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else if (_itms.Mi_is_ser1 == -1)
                {
                    CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), string.Empty, string.Empty, 0, 3);
                    CHNLSVC.Inventory.SavePickedItems(_saveonly);

                    string _SER1 = "N/A";
                    string _SER2 = "N/A";
                    ReptPickSerials _reptPickSerial = new ReptPickSerials();

                    _reptPickSerial.Tus_cre_by = BaseCls.GlbUserID;
                    _reptPickSerial.Tus_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text.ToString());

                    _reptPickSerial.Tus_base_doc_no = txtUserSeqNo.Text;
                    _reptPickSerial.Tus_base_itm_line = _lineNo;
                    _reptPickSerial.Tus_itm_desc = lblItemDescription.Text.Replace("Description : ", "");
                    _reptPickSerial.Tus_itm_model = lblItemModel.Text.Replace("Model : ", "");
                    _reptPickSerial.Tus_itm_brand = lblItemBrand.Text.Replace("Brand : ", "");
                    _reptPickSerial.Tus_new_status = ddlNewStatus.SelectedValue.ToString();
                    _reptPickSerial.Tus_itm_stus = ddlStatus.SelectedValue.ToString();
                    _reptPickSerial.Tus_new_remarks = "N/A";
                    _reptPickSerial.Tus_com = BaseCls.GlbUserComCode;
                    _reptPickSerial.Tus_loc = BaseCls.GlbUserDefLoca;
                    _reptPickSerial.Tus_bin = BaseCls.GlbDefaultBin;
                    _reptPickSerial.Tus_itm_cd = txtItem.Text.Trim().ToUpper();
                    _reptPickSerial.Tus_qty = string.IsNullOrEmpty(txtQty.Text) ? 0 : Convert.ToDecimal(txtQty.Text);
                    _reptPickSerial.Tus_ser_1 = "N/A";
                    _reptPickSerial.Tus_ser_2 = "N/A";
                    _reptPickSerial.Tus_ser_3 = "N/A";
                    _reptPickSerial.Tus_ser_4 = "N/A";
                    _reptPickSerial.Tus_ser_id = 0;
                    _reptPickSerial.Tus_serial_id = "0";

                    Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial, null);
                    if (affected_rows > 0)
                    {
                        Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text, _serID, -1);
                    }
                }

                LoadItems(txtUserSeqNo.Text.ToString());
                string _tragetObj = string.Empty;
                if (chkDirectScanningSer1.Checked == true) _tragetObj = "SER1";
                if (chkDirectScanningSer2.Checked == true) _tragetObj = "SER2";
                ClearItemDetail();

                if (_tragetObj == "SER1") chkDirectScanningSer1.Checked = true;
                if (_tragetObj == "SER2") chkDirectScanningSer2.Checked = true;

                txtItem.Focus();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                //ClearItemDetail(); LoadItemDetail(string.Empty,false);
                //if (MessageBox.Show("Do you need to add another item?", "Adding...", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                //txtItem.Focus();
                //else 
                //gvItems.Focus();
            }
        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddItem();
        }
        #endregion

        #region Rooting for Clear Screen
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            while (this.Controls.Count > 0)
            {
                Controls[0].Dispose();
            }
            //_commonOutScan = new CommonSearch.CommonOutScan();
            InitializeComponent();
            InitializeForm(true);
            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, dtpDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
            //_commonOutScan.AddSerialClick += new EventHandler(_commonOutScan_AddSerialClick);
            this.Cursor = Cursors.Default;
        }
        #endregion

        #region Rooting for Add Serials
        private void gvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (btnSave.Enabled == true)
                {
                    if (gvItems.ColumnCount > 0 && e.RowIndex >= 0)
                    {
                        int _rowIndex = e.RowIndex;
                        int _colIndex = e.ColumnIndex;

                        if (e.ColumnIndex == 1)
                        {
                            #region Remove Item
                            if (gvItems.Columns[_colIndex].Name == "itm_Remove")
                            {
                                if (MessageBox.Show("Do you need to remove this record?", "Remove...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    //OnRemoveFromItemGrid(_rowIndex, _colIndex); 
                                    DeleteSelectedLine(e.RowIndex, Convert.ToInt32(txtUserSeqNo.Text), "ITEM");
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Rooting for Load Items/Serials
        private void LoadItems(string _seqNo)
        {
            //updated by akila 2017/09/20
            List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
            List<ReptPickItems> _pickItmes = new List<ReptPickItems>();
            List<ReptPickSerials> _pickedSerials = new List<ReptPickSerials>();

            try
            {
                _pickItmes = CHNLSVC.Inventory.GetAllScanRequestItemsList(Convert.ToInt32(_seqNo));
                if (_pickItmes != null && _pickItmes.Count > 0)
                {
                    foreach (ReptPickItems _pickItem in _pickItmes)
                    {
                        InventoryRequestItem _itm = new InventoryRequestItem();
                        MasterItem _itms = new MasterItem();
                        _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _pickItem.Tui_req_itm_cd);

                        _itm.Itri_app_qty = _pickItem.Tui_req_itm_qty;
                        _itm.Itri_itm_cd = _pickItem.Tui_req_itm_cd;
                        _itm.Itri_itm_stus = _pickItem.Tui_req_itm_stus;
                        _itm.Itri_line_no = Convert.ToInt32(_pickItem.Tui_pic_itm_cd);
                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        _itm.Itri_unit_price = 0;
                        _itm.Itri_note = _pickItem.Tui_pic_itm_stus;

                        _pickedSerials = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, Convert.ToInt32(_seqNo), "ADJ-S");
                        if (_pickedSerials != null && _pickedSerials.Count > 0)
                        {
                            decimal _pickSerialQty = _pickedSerials.Where(x => x.Tus_itm_cd == _pickItem.Tui_req_itm_cd && x.Tus_itm_stus == _pickItem.Tui_req_itm_stus && x.Tus_base_itm_line == Convert.ToInt32(_pickItem.Tui_pic_itm_cd)).Sum(x => x.Tus_qty);
                            _itm.Itri_qty += _pickSerialQty;
                        }
                        else
                        {
                            _itm.Itri_qty += _pickItem.Tui_req_itm_qty;
                        }
                        _itmList.Add(_itm);
                    }
                }

                gvSerial.AutoGenerateColumns = false;
                gvSerial.DataSource = _pickedSerials;

                ScanItemList = _itmList;
                gvItems.AutoGenerateColumns = false;
                gvItems.DataSource = ScanItemList;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }

            #region old code
            //List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
            //List<ReptPickItems> _reptItems = new List<ReptPickItems>();
            //List<ReptPickSerials> _serList = new List<ReptPickSerials>();

            //Int32 user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "ADJ-S", BaseCls.GlbUserID, 0, _seqNo);
            //if (user_seq_num == -1)
            //{
            //    user_seq_num = GenerateNewUserSeqNo();
            //}

            //_reptItems = CHNLSVC.Inventory.GetAllScanRequestItemsList(user_seq_num);
            //foreach (ReptPickItems _reptitem in _reptItems)
            //{
            //    InventoryRequestItem _itm = new InventoryRequestItem();
            //    MasterItem _itms = new MasterItem();
            //    _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _reptitem.Tui_req_itm_cd);
            //    _itm.Itri_app_qty = _reptitem.Tui_req_itm_qty;
            //    _itm.Itri_itm_cd = _reptitem.Tui_req_itm_cd;
            //    _itm.Itri_itm_stus = _reptitem.Tui_req_itm_stus;
            //    _itm.Itri_line_no = Convert.ToInt32(_reptitem.Tui_pic_itm_cd.ToString());
            //    //_itm.Itri_qty = 0;
            //    _itm.Mi_longdesc = _itms.Mi_longdesc;
            //    _itm.Mi_model = _itms.Mi_model;
            //    _itm.Mi_brand = _itms.Mi_brand;
            //    _itm.Itri_unit_price = 0;
            //    _itm.Itri_note = _reptitem.Tui_pic_itm_stus;

            //    //updated by akila. - if item serail type is -1 then pick qty will update with selected qty
            //    //if (_itms.Mi_is_ser1 != 1 && !string.IsNullOrEmpty(txtQty.Text)) { _itm.Itri_qty = decimal.Parse(string.IsNullOrEmpty(txtQty.Text) ? "0" : txtQty.Text); }

            //    _itmList.Add(_itm);
            //}
            //ScanItemList = _itmList;
            //gvItems.AutoGenerateColumns = false;
            //gvItems.DataSource = _itmList;

            ////updated by akila 2017/09/18
            //_serList = GetScanedItemSerails(_seqNo);
            ////_serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "ADJ-S");
            //if (_serList != null)
            //{
            //    var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
            //    foreach (var itm in _scanItems)
            //    {
            //        foreach (DataGridViewRow row in this.gvItems.Rows)
            //        {
            //            if (itm.Peo.Tus_itm_cd == row.Cells["itm_Item"].Value.ToString() && itm.Peo.Tus_base_itm_line == Convert.ToInt32(row.Cells["itm_Lineno"].Value.ToString()))
            //            {
            //                row.Cells["itm_PickQty"].Value = Convert.ToDecimal(itm.theCount); // Current scan qty
            //            }
            //        }
            //    }
            //    gvSerial.AutoGenerateColumns = false;
            //    gvSerial.DataSource = _serList;
            //}
            //else
            //{
            //    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
            //    gvSerial.AutoGenerateColumns = false;
            //    gvSerial.DataSource = emptyGridList;

            //}
            ////gvItems.AutoGenerateColumns = false;
            ////gvItems.DataSource = gvItems;

            ////return _serList;
            #endregion
        }
        #endregion

        #region Generate new user seq no
        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "ADJ", 0, BaseCls.GlbUserComCode);//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = "ADJ-S";
            RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
            RPH.Tuh_ischek_itmstus = true;//might change 
            RPH.Tuh_ischek_reqqty = true;//might change
            RPH.Tuh_ischek_simitm = true;//might change
            RPH.Tuh_session_id = BaseCls.GlbUserSessionID;
            RPH.Tuh_usr_com = BaseCls.GlbUserComCode;//might change 
            RPH.Tuh_usr_id = BaseCls.GlbUserID;
            RPH.Tuh_usrseq_no = generated_seq;
            RPH.Tuh_direct = false;
            RPH.Tuh_doc_no = generated_seq.ToString();
            //write entry to TEMP_PICK_HDR
            int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
            if (affected_rows == 1)
            {
                txtUserSeqNo.Text = generated_seq.ToString();
                return generated_seq;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region Rooting for Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            //kapila 23/1/2017
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10152))
            {
                _isStusChngPerm = true;
            }
            Process();
        }

        private void Process()
        {
            try
            {
                if (CheckServerDateTime() == false) return;

                if (string.IsNullOrEmpty(txtManualRef.Text)) txtManualRef.Text = "N/A";
                if (string.IsNullOrEmpty(txtOtherRef.Text)) txtOtherRef.Text = string.Empty;
                if (string.IsNullOrEmpty(txtRemarks.Text)) txtRemarks.Text = string.Empty;

                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty.ToUpper().ToString(), this.GlbModuleName, dtpDate, lblH1, dtpDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (dtpDate.Value.Date != DateTime.Now.Date)
                        {
                            dtpDate.Enabled = true;
                            MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpDate.Focus();
                            return;
                        }
                    }
                    else
                    {
                        dtpDate.Enabled = true;
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpDate.Focus();
                        return;
                    }
                }

                Cursor.Current = Cursors.WaitCursor;

                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                string documntNo_minus = "";
                string documntNo_plus = "";
                string error = string.Empty;
                Int32 _userSeqNo = Convert.ToInt32(txtUserSeqNo.Text);
                //int _direction = 0;

                //_userSeqNo = CHNLSVC.Inventory.Get_Scan_SeqNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "ADJ-S", BaseCls.GlbUserID, _direction, txtUserSeqNo.Text);
                //reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, "ADJ-S");

                //updated by akila 2017/09/18
                reptPickSerialsList = GetScanedItemSerails(_userSeqNo.ToString());
                //reptPickSerialsList = LoadItems(_userSeqNo.ToString());
                #region Check Duplicate Serials
                if (reptPickSerialsList != null)
                {
                    var _dup = reptPickSerialsList.Where(x => x.Tus_ser_1 != "N/A").Select(y => y.Tus_ser_id).ToList();

                    string _duplicateItems = string.Empty;
                    bool _isDuplicate = false;
                    if (_dup != null)
                        if (_dup.Count > 0)
                            foreach (Int32 _id in _dup)
                            {
                                Int32 _counts = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
                                if (_counts > 1)
                                {
                                    _isDuplicate = true;
                                    var _item = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
                                    foreach (string _str in _item)
                                        if (string.IsNullOrEmpty(_duplicateItems))
                                            _duplicateItems = _str;
                                        else
                                            _duplicateItems += "," + _str;
                                }
                            }
                    if (_isDuplicate)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                #endregion
                //string error = CHNLSVC.Inventory.AutoPickNonSerialItemsAll(_userSeqNo, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbDefaultBin, BaseCls.GlbUserID, ScanItemList);
                //if (!string.IsNullOrEmpty(error))
                //{
                //    Cursor.Current = Cursors.Default;
                //    MessageBox.Show(error, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                //updated by akila 2017/09/18
                reptPickSerialsList = GetScanedItemSerails(_userSeqNo.ToString());
                //reptPickSerialsList = LoadItems(_userSeqNo.ToString());
                reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "ADJ-S");
                //if (reptPickSerialsList == null)
                //{
                //    Cursor.Current = Cursors.Default;
                //    MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                //else
                //{
                //    if (reptPickSerialsList.Count <= 0)
                //    {
                //        Cursor.Current = Cursors.Default;
                //        MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }
                //}

                #region Check Serial Scan or not

                if (gvItems == null)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (gvItems.Rows.Count <= 0)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                foreach (DataGridViewRow row in this.gvItems.Rows)
                {
                    MasterItem _itms = new MasterItem();
                    _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, row.Cells["itm_Item"].Value.ToString());
                    if (_itms.Mi_is_ser1 == 1)
                    {
                        if (Convert.ToDecimal(row.Cells["itm_PickQty"].Value) == 0)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("Serial nos not pick for item " + _itms.Mi_cd, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                #endregion

                decimal _outpara = 0;
                if (_isStusChngPerm == false)
                {
                    if (MessageBox.Show("No permission for direct status change (Permission code:10152)\nDo you want to check the definition?", "Process Terminated", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No) return;

                    //kapila 25/4/2017
                    foreach (InventoryRequestItem _addedItem in ScanItemList)
                    {

                        _outpara = CHNLSVC.Financial.Get_Inr_sys_para(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _addedItem.Itri_itm_cd, _addedItem.Itri_note, Convert.ToInt32(_addedItem.Itri_qty));
                            if (_outpara == -2)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("Cannot Process !\n Item : " + _addedItem.Itri_itm_cd + " has not defined to change the status. \n Please contact Inventory Department.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            else if (_outpara > 0)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("Cannot Process !\n No of allowed status change for the " + _addedItem.Itri_itm_cd + " is exceeded. Please request status change from the Inventory Dept ", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                        
                    }
                }

                #region Check Referance Date and the Doc Date
                if (IsReferancedDocDateAppropriate(reptPickSerialsList, dtpDate.Value.Date) == false)
                {
                    Cursor.Current = Cursors.Default;
                    return;
                }
                #endregion

                InventoryHeader _hdrMinus = new InventoryHeader();
                #region Fill InventoryHeader
                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                foreach (DataRow r in dt_location.Rows)
                {
                    // Get the value of the wanted column and cast it to string
                    _hdrMinus.Ith_sbu = (string)r["ML_OPE_CD"];
                    if (System.DBNull.Value != r["ML_CATE_2"])
                    {
                        _hdrMinus.Ith_channel = (string)r["ML_CATE_2"];
                    }
                    else
                    {
                        _hdrMinus.Ith_channel = string.Empty;
                    }
                }
                _hdrMinus.Ith_acc_no = "STATUS_CHANGE";
                _hdrMinus.Ith_anal_1 = "";
                _hdrMinus.Ith_anal_2 = "";
                _hdrMinus.Ith_anal_3 = "";
                _hdrMinus.Ith_anal_4 = "";
                _hdrMinus.Ith_anal_5 = "";
                _hdrMinus.Ith_anal_6 = _userSeqNo;
                _hdrMinus.Ith_anal_7 = 0;
                _hdrMinus.Ith_anal_8 = DateTime.MinValue;
                _hdrMinus.Ith_anal_9 = DateTime.MinValue;
                _hdrMinus.Ith_anal_10 = false;
                _hdrMinus.Ith_anal_11 = false;
                _hdrMinus.Ith_anal_12 = false;
                _hdrMinus.Ith_bus_entity = "";
                _hdrMinus.Ith_cate_tp = "STUS"; //Sub Type
                _hdrMinus.Ith_com = BaseCls.GlbUserComCode;
                _hdrMinus.Ith_com_docno = "";
                _hdrMinus.Ith_cre_by = BaseCls.GlbUserID;
                _hdrMinus.Ith_cre_when = DateTime.Now;
                _hdrMinus.Ith_del_add1 = "";
                _hdrMinus.Ith_del_add2 = "";
                _hdrMinus.Ith_del_code = "";
                _hdrMinus.Ith_del_party = "";
                _hdrMinus.Ith_del_town = "";
                _hdrMinus.Ith_direct = false;
                _hdrMinus.Ith_doc_date = dtpDate.Value;
                _hdrMinus.Ith_doc_no = string.Empty;
                _hdrMinus.Ith_doc_tp = "ADJ";
                _hdrMinus.Ith_doc_year = dtpDate.Value.Date.Year;
                _hdrMinus.Ith_entry_no = txtOtherRef.Text.ToString().Trim();
                _hdrMinus.Ith_entry_tp = "STTUS";
                _hdrMinus.Ith_git_close = true;
                _hdrMinus.Ith_git_close_date = DateTime.MinValue;
                _hdrMinus.Ith_git_close_doc = BaseCls.GlbDefaultBin; //Bin
                _hdrMinus.Ith_isprinted = false;
                _hdrMinus.Ith_is_manual = false;
                _hdrMinus.Ith_job_no = string.Empty;
                _hdrMinus.Ith_loading_point = string.Empty;
                _hdrMinus.Ith_loading_user = string.Empty;
                _hdrMinus.Ith_loc = BaseCls.GlbUserDefLoca;
                _hdrMinus.Ith_manual_ref = txtManualRef.Text.Trim();
                _hdrMinus.Ith_mod_by = BaseCls.GlbUserID;
                _hdrMinus.Ith_mod_when = DateTime.Now;
                _hdrMinus.Ith_noofcopies = 0;
                _hdrMinus.Ith_oth_loc = string.Empty;
                _hdrMinus.Ith_oth_docno = "N/A";
                _hdrMinus.Ith_remarks = txtRemarks.Text;
                //_hdrMinus.Ith_seq_no = 6;
                _hdrMinus.Ith_session_id = BaseCls.GlbUserSessionID;
                _hdrMinus.Ith_stus = "A";
                _hdrMinus.Ith_sub_tp = "STTUS";
                _hdrMinus.Ith_vehi_no = string.Empty;
                #endregion
                MasterAutoNumber _autonoMinus = new MasterAutoNumber();
                #region Fill MasterAutoNumber
                _autonoMinus.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                _autonoMinus.Aut_cate_tp = "LOC";
                _autonoMinus.Aut_direction = null;
                _autonoMinus.Aut_modify_dt = null;
                _autonoMinus.Aut_moduleid = "ADJ";
                _autonoMinus.Aut_number = 5;//what is Aut_number
                _autonoMinus.Aut_start_char = "ADJ";
                _autonoMinus.Aut_year = null;
                #endregion
                InventoryHeader _hdrPlus = new InventoryHeader();
                #region Fill InventoryHeader
                _hdrPlus.Ith_channel = _hdrMinus.Ith_channel;
                _hdrPlus.Ith_sbu = _hdrMinus.Ith_sbu;
                _hdrPlus.Ith_acc_no = "STATUS_CHANGE";
                _hdrPlus.Ith_anal_1 = "";
                _hdrPlus.Ith_anal_2 = "";
                _hdrPlus.Ith_anal_3 = "";
                _hdrPlus.Ith_anal_4 = "";
                _hdrPlus.Ith_anal_5 = "";
                _hdrPlus.Ith_anal_6 = 0;
                _hdrPlus.Ith_anal_7 = 0;
                _hdrPlus.Ith_anal_8 = DateTime.MinValue;
                _hdrPlus.Ith_anal_9 = DateTime.MinValue;
                _hdrPlus.Ith_anal_10 = false;
                _hdrPlus.Ith_anal_11 = false;
                _hdrPlus.Ith_anal_12 = false;
                _hdrPlus.Ith_bus_entity = "";
                _hdrPlus.Ith_cate_tp = "STTUS";
                _hdrPlus.Ith_com = BaseCls.GlbUserComCode;
                _hdrPlus.Ith_com_docno = "";
                _hdrPlus.Ith_cre_by = BaseCls.GlbUserID;
                _hdrPlus.Ith_cre_when = DateTime.Now;
                _hdrPlus.Ith_del_add1 = "";
                _hdrPlus.Ith_del_add2 = "";
                _hdrPlus.Ith_del_code = "";
                _hdrPlus.Ith_del_party = "";
                _hdrPlus.Ith_del_town = "";
                _hdrPlus.Ith_direct = true;
                _hdrPlus.Ith_doc_date = dtpDate.Value;
                _hdrPlus.Ith_doc_no = string.Empty;
                _hdrPlus.Ith_doc_tp = "ADJ";
                _hdrPlus.Ith_doc_year = dtpDate.Value.Date.Year;
                _hdrPlus.Ith_entry_no = txtOtherRef.Text.ToString().Trim();
                _hdrPlus.Ith_entry_tp = "STTUS";
                _hdrPlus.Ith_git_close = true;
                _hdrPlus.Ith_git_close_date = DateTime.MinValue;
                _hdrPlus.Ith_git_close_doc = string.Empty;
                _hdrPlus.Ith_isprinted = false;
                _hdrPlus.Ith_is_manual = false;
                _hdrPlus.Ith_job_no = string.Empty;
                _hdrPlus.Ith_loading_point = string.Empty;
                _hdrPlus.Ith_loading_user = string.Empty;
                _hdrPlus.Ith_loc = BaseCls.GlbUserDefLoca;
                _hdrPlus.Ith_manual_ref = txtManualRef.Text.Trim();
                _hdrPlus.Ith_mod_by = BaseCls.GlbUserID;
                _hdrPlus.Ith_mod_when = DateTime.Now;
                _hdrPlus.Ith_noofcopies = 0;
                _hdrPlus.Ith_oth_loc = string.Empty;
                _hdrPlus.Ith_oth_docno = "N/A";
                _hdrPlus.Ith_remarks = txtRemarks.Text;
                //_hdrMinus.Ith_seq_no = 6;
                _hdrPlus.Ith_session_id = BaseCls.GlbUserSessionID;
                _hdrPlus.Ith_stus = "A";
                _hdrPlus.Ith_sub_tp = "STTUS";
                _hdrPlus.Ith_vehi_no = string.Empty;
                #endregion
                MasterAutoNumber _autonoPlus = new MasterAutoNumber();
                #region Fill MasterAutoNumber
                _autonoPlus.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                _autonoPlus.Aut_cate_tp = "LOC";
                _autonoPlus.Aut_direction = null;
                _autonoPlus.Aut_modify_dt = null;
                _autonoPlus.Aut_moduleid = "ADJ";
                _autonoPlus.Aut_number = 5;//what is Aut_number
                _autonoPlus.Aut_start_char = "ADJ";
                _autonoPlus.Aut_year = null;
                #endregion

                #region Status Change Adj- >>>> Adj+
                error = CHNLSVC.Inventory.InventoryStatusChangeEntry(_hdrMinus, _hdrPlus, reptPickSerialsList, reptPickSubSerialsList, _autonoMinus, _autonoPlus, ScanItemList, out documntNo_minus, out documntNo_plus);
                //if (result != -99 && result >= 0)
                //if (string.IsNullOrEmpty(error))
                if ((string.IsNullOrEmpty(error)) || (error.Contains("|")))
                {
                    Cursor.Current = Cursors.Default;
                    if (MessageBox.Show("Successfully Saved! document no (-ADJ) : " + documntNo_minus + " and document no (+ADJ) :" + documntNo_plus + "\nDo you want to print this?", "Process Completed : STTUS", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        BaseCls.GlbReportTp = "OUTWARD";
                        Reports.Inventory.ReportViewerInventory _viewMinus = new Reports.Inventory.ReportViewerInventory();
                        if (BaseCls.GlbUserComCode == "SGL") //Sanjeewa 2014-01-07
                            _viewMinus.GlbReportName = "SOutward_Docs.rpt";
                        else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
                            _viewMinus.GlbReportName = "Dealer_Outward_Docs.rpt";
                        else _viewMinus.GlbReportName = "Outward_Docs.rpt";
                        _viewMinus.GlbReportDoc = documntNo_minus;
                        _viewMinus.Show();
                        _viewMinus = null;

                        BaseCls.GlbReportTp = "INWARD";
                        Reports.Inventory.ReportViewerInventory _viewPlus = new Reports.Inventory.ReportViewerInventory();
                        if (BaseCls.GlbUserComCode == "SGL") //Sanjeewa 2014-01-07
                            _viewPlus.GlbReportName = "Inward_Docs.rpt";
                        else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
                            _viewPlus.GlbReportName = "Dealer_Inward_Docs.rpt";
                        else _viewPlus.GlbReportName = "Inward_Docs.rpt";
                        _viewPlus.GlbReportDoc = documntNo_plus;
                        _viewPlus.Show();
                        _viewPlus = null;
                    }
                    btnClear_Click(null, null);
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(error + " with save Failed!", "Process Completed : STTUS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                #endregion
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Rooting for Delete items/Serials

        void Delete_Serials(string _itemCode, string _itemStatus, Int32 _serialID)
        {
            MasterItem _masterItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itemCode);
            if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
            {
                //modify Rukshan 05/oct/2015 add two parameters
                CHNLSVC.Inventory.Del_temp_pick_ser(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, Convert.ToInt32(txtUserSeqNo.Text), Convert.ToInt32(_serialID),null,null);
                CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _itemCode, _serialID, 1);
            }
            else
            {
                CHNLSVC.Inventory.DeleteTempPickSerialByItem(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, Convert.ToInt32(txtUserSeqNo.Text), _itemCode, _itemStatus);
            }
        }

        protected void OnRemoveFromItemGrid(int _rowIndex, int _colIndex)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int row_id = _rowIndex;
                if (string.IsNullOrEmpty(gvItems.Rows[row_id].Cells["itm_Item"].Value.ToString())) return;

                string _item = Convert.ToString(gvItems.Rows[row_id].Cells["itm_Item"].Value);
                string _itmStatus = Convert.ToString(gvItems.Rows[row_id].Cells["itm_Status"].Value);
                decimal _itmCost = Convert.ToDecimal(gvItems.Rows[row_id].Cells["itri_unit_price"].Value);
                decimal _qty = Convert.ToDecimal(gvItems.Rows[row_id].Cells["itm_PickQty"].Value);
                int _lineNo = Convert.ToInt32(gvItems.Rows[row_id].Cells["itm_Lineno"].Value);

                CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), _item, _itmStatus, _lineNo, 2);

                List<ReptPickSerials> _list = new List<ReptPickSerials>();
                _list = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, Convert.ToInt32(txtUserSeqNo.Text), "ADJ-S");
                if (_list != null)
                {
                    if (_list.Count > 0)
                    {
                        var _delete = (from _lst in _list
                                       where _lst.Tus_itm_cd == _item && _lst.Tus_itm_stus == _itmStatus && _lst.Tus_base_itm_line == _lineNo
                                       select _lst).ToList();

                        foreach (ReptPickSerials _ser in _delete)
                        {
                            Delete_Serials(_item, _itmStatus, _ser.Tus_ser_id);
                        }
                    }
                }
                //ScanItemList.RemoveAll(x => x.Itri_itm_cd == _item && x.Itri_itm_stus == _itmStatus);
                LoadItems(txtUserSeqNo.Text);
                this.Cursor = Cursors.Default;
                if (gvItems.Rows.Count < 1) { btnClear_Click(null, null); }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void gvSerial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (btnSave.Enabled == true)
                {
                    if (gvSerial.RowCount > 0)
                    {
                        int _rowCount = e.RowIndex;
                        if (_rowCount != -1)
                        {
                            if (gvSerial.Columns[e.ColumnIndex].Name == "ser_Remove")
                                if (MessageBox.Show("Do you need to remove this record?", "Remove...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    //OnRemoveFromSerialGrid(_rowCount);
                                    DeleteSelectedLine(e.RowIndex, Convert.ToInt32(txtUserSeqNo.Text), "SERIAL");
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void OnRemoveFromSerialGrid(int _rowIndex)
        {
            try
            {
                int row_id = _rowIndex;

                if (string.IsNullOrEmpty(gvSerial.Rows[row_id].Cells["ser_Item"].Value.ToString())) return;

                string _item = Convert.ToString(gvSerial.Rows[row_id].Cells["ser_Item"].Value);
                string _itmStatus = Convert.ToString(gvSerial.Rows[row_id].Cells["ser_Status"].Value);
                Int32 _serialID = Convert.ToInt32(gvSerial.Rows[row_id].Cells["ser_SerialID"].Value);
                string _bin = Convert.ToString(gvSerial.Rows[row_id].Cells["ser_Bin"].Value);
                string serial_1 = Convert.ToString(gvSerial.Rows[row_id].Cells["ser_Serial1"].Value);
                string _requestno = Convert.ToString(gvSerial.Rows[row_id].Cells["ser_requestno"].Value);
                Int32 _lineNo = Convert.ToInt32(gvSerial.Rows[row_id].Cells["ser_BaseLineNo"].Value);

                MasterItem _masterItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);

                if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                {
                    //modify Rukshan 05/oct/2015 add two parameters
                    bool _isdeleted = CHNLSVC.Inventory.Del_temp_pick_ser(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, Convert.ToInt32(txtUserSeqNo.Text), Convert.ToInt32(_serialID),null,null);
                    if (_isdeleted)
                    {
                        CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, _serialID, 1);
                    }
                    //CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, _serialID, 1);
                }
                else
                {
                    //CHNLSVC.Inventory.DeleteTempPickSerialByItem(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, Convert.ToInt32(txtUserSeqNo.Text), _item, _status);
                }
              //  CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), _item, _itmStatus, _lineNo, 2);
                LoadItems(txtUserSeqNo.Text);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
            finally { this.Cursor = Cursors.Default; }
        }
        #endregion

        #region Sub functions
        private void CheckAlreadySelectedSerial()
        {
            foreach (DataGridViewRow _r in gvSerial.Rows)
            {
                string _id = _r.Cells["ser_SerialID"].Value.ToString();
                DataGridViewCheckBoxCell _chk = _r.Cells["ser_Select"] as DataGridViewCheckBoxCell;
                if (SelectedItemList != null)
                    if (SelectedItemList.Count > 0)
                    {
                        var _exist = SelectedItemList.Where(x => x.Tus_ser_id == Convert.ToInt32(_id)).ToList();
                        if (_exist != null)
                            if (_exist.Count > 0)
                                _chk.Value = true;
                    }
            }
        }

        private void ScanSerial(bool _directScan, string _obj)
        {
            DataTable _t = CHNLSVC.Inventory.GetAllCompanyStatus(BaseCls.GlbUserComCode);
            if (_directScan == true)
            {
                DataTable _serialTbl = null;
                if (chkDirectScanningSer1.Checked == true)
                {
                    _serialTbl = CHNLSVC.Inventory.getDetail_on_serial1(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtSer1.Text);
                }
                else if (chkDirectScanningSer2.Checked == true)
                {
                    _serialTbl = CHNLSVC.Inventory.getDetail_on_serial_Supplier(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, null, null, txtSer2.Text);
                }

                if (_serialTbl == null)
                {
                    MessageBox.Show("This serial no is not available! Please check again.", "Serial Scanning . . . ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSer1.Clear();
                    txtSer2.Clear();
                    if (_obj == "txtSer1") txtSer1.Focus();
                    if (_obj == "txtSer2") txtSer2.Focus();
                    return;
                }
                if (_serialTbl.Rows.Count <= 0)
                {
                    MessageBox.Show("This serial no is not available! Please check again.", "Serial Scanning . . . ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSer1.Clear();
                    txtSer2.Clear();
                    if (_obj == "txtSer1") txtSer1.Focus();
                    if (_obj == "txtSer2") txtSer2.Focus();
                    return;
                }
                if (_serialTbl.Rows.Count > 1)
                {
                    MessageBox.Show("More than one serials exist for this serial no.", "Serial Scanning . . . ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSer1.Clear();
                    txtSer2.Clear();
                    if (_obj == "txtSer1") txtSer1.Focus();
                    if (_obj == "txtSer2") txtSer2.Focus();
                    return;
                }
                else
                {
                    foreach (DataRow r in _serialTbl.Rows)
                    {
                        //string bin_code = (string)r["INS_BIN"];
                        //Decimal unitCost = (Decimal)r["INS_UNIT_COST"];
                        //string itemCode = (string)r["INS_ITM_CD"];
                        //string itemStatus = (string)r["INS_ITM_STUS"];
                        if ((string)r["INS_ITM_STUS"].ToString() == "CONS")
                        {
                            MessageBox.Show("Status of a Consignment item cannot be change!", "Serial Scanning . . . ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtSer1.Clear();
                            txtSer1.Focus();
                            return;
                        }
                        txtItem.Text = (string)r["INS_ITM_CD"].ToString();
                        txtSer1.Text = (string)r["INS_SER_1"].ToString();
                        txtSer2.Text = (string)r["INS_SER_2"].ToString();
                        ddlStatus.SelectedValue = (string)r["INS_ITM_STUS"].ToString();
                        txtQty.Text = "1";
                        CheckItem(false, false);
                    }
                    ddlNewStatus.Focus();
                    return;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtItem.Text))
                {
                    string _txtSer1 = string.Empty;
                    string _txtSer2 = string.Empty;
                    if (_obj == "txtSer1")
                    {
                        _txtSer1 = txtSer1.Text.Trim().ToString();
                    }
                    else if (_obj == "txtSer2")
                    {
                        _txtSer2 = txtSer2.Text.Trim().ToString();
                    }
                    List<ReptPickSerials> _list = CHNLSVC.Inventory.Search_by_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), null, _txtSer1, _txtSer2);
                    if (_list == null)
                    {
                        MessageBox.Show("This serial no is not available! Please check again.", "Serial Scanning . . . ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSer1.Clear();
                        txtSer2.Clear();
                        if (_obj == "txtSer1") txtSer1.Focus();
                        if (_obj == "txtSer2") txtSer2.Focus();
                        return;
                    }
                    if (_list.Count <= 0)
                    {
                        MessageBox.Show("This serial no is not available! Please check again.", "Serial Scanning . . . ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSer1.Clear();
                        txtSer2.Clear();
                        if (_obj == "txtSer1") txtSer1.Focus();
                        if (_obj == "txtSer2") txtSer2.Focus();
                        return;
                    }
                    if (_list.Count > 1)
                    {
                        MessageBox.Show("More than one serials exist for this serial no.", "Serial Scanning . . . ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSer1.Clear();
                        txtSer2.Clear();
                        if (_obj == "txtSer1") txtSer1.Focus();
                        if (_obj == "txtSer2") txtSer2.Focus();
                        return;
                    }

                    if (_list.Count == 1)
                    {
                        ReptPickSerials _listItem = new ReptPickSerials();
                        foreach (ReptPickSerials _pickItem in _list)
                        {
                            txtItem.Text = _pickItem.Tus_itm_cd;
                            txtSer1.Text = _pickItem.Tus_ser_1;
                            txtSer2.Text = _pickItem.Tus_ser_2;

                            var query = from p in _t.AsEnumerable()
                                        where p.Field<string>("MIC_CD") == _pickItem.Tus_itm_stus.ToString()
                                        select new
                                        {
                                            name = p.Field<string>("MIS_DESC"),
                                        };
                            foreach (var array in query)
                            {
                                ddlStatus.Text = array.name;
                                ddlStatus.SelectedText = array.name;
                            }

                        }

                        txtQty.Text = "1";
                        CheckItem(false, false);
                        ddlNewStatus.Focus();
                        return;
                    }
                }

            }
        }

        #endregion

        private void txtSer1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_Serial1_Click(null, null);
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtSer1.Text.Trim()))
                {
                    ddlNewStatus.Focus();
                }
            }
        }

        private void txtSer1_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSer1.Text.Trim()))
            {
                if (chkDirectScanningSer1.Checked == true)
                    ScanSerial(true, "txtSer1");
                else
                    ScanSerial(false, "txtSer1");
            }
        }

        private void txtSer2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_Serial2_Click(null, null);
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtSer2.Text.Trim()))
                {
                    ddlNewStatus.Focus();
                }
            }
        }

        private void txtSer2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSer2.Text.Trim()))
            {
                if (chkDirectScanningSer2.Checked == true)
                    ScanSerial(true, "txtSer2");
                else
                    ScanSerial(false, "txtSer2");
            }
        }

        private void ddlStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(ddlStatus.Text))
                {
                    if (txtQty.Enabled == true) txtQty.Focus();
                }
            }
        }

        private void ddlNewStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(ddlNewStatus.Text))
                {
                    btnAddItem.Focus();
                }
            }
        }

        private void btnSearch_Serial1_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkDirectScanningSer1.Checked == true)
                {
                    _SerialSearchType = "SER1_WOITEM";
                }
                else
                {
                    if (string.IsNullOrEmpty(txtItem.Text))
                    {
                        MessageBox.Show("Please select the item code", "Item Code", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItem.Focus();
                        return;
                    }
                    else
                    {
                        _SerialSearchType = "SER1_WITEM";
                    }
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
                DataTable _result = CHNLSVC.CommonSearch.Search_inr_ser_infor(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSer1;
                _CommonSearch.ShowDialog();
                txtSer1.Select();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
        }

        private void btnSearch_Serial2_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkDirectScanningSer2.Checked == true)
                {
                    _SerialSearchType = "SER2_WOITEM";
                }
                else
                {
                    if (string.IsNullOrEmpty(txtItem.Text))
                    {
                        MessageBox.Show("Please select the item code", "Item Code", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItem.Focus();
                        return;
                    }
                    else
                    {
                        _SerialSearchType = "SER1_WITEM";
                    }
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
                DataTable _result = CHNLSVC.CommonSearch.Search_inr_ser_infor(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSer2;
                _CommonSearch.ShowDialog();
                txtSer2.Select();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
        }

        private void txtSer1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Serial1_Click(null, null);
        }

        private void txtSer2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Serial2_Click(null, null);
        }

        private void btnAddItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                string _tragetObj = string.Empty;
                if (chkDirectScanningSer1.Checked == true) _tragetObj = "SER1";
                if (chkDirectScanningSer2.Checked == true) _tragetObj = "SER2";
                ClearItemDetail();

                if (_tragetObj == "SER1") chkDirectScanningSer1.Checked = true;
                if (_tragetObj == "SER2") chkDirectScanningSer2.Checked = true;

                txtItem.Focus();
            }
        }

        private void ChangeItemStatus_Load(object sender, EventArgs e)
        {
            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, dtpDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
            //IsAllowBackDateForModule
        }

        //add by akila 2017/09/18
        private List<ReptPickSerials> GetScanedItemSerails(string _seqNo)
        {
            List<ReptPickSerials> _serList = new List<ReptPickSerials>();

            try
            {
                Int32 user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "ADJ-S", BaseCls.GlbUserID, 0, _seqNo);
                if (user_seq_num == -1)
                {
                    user_seq_num = GenerateNewUserSeqNo();
                }

                _serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "ADJ-S");
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
            }
            return _serList;
        }

        private void DeleteSelectedLine(int _rowIndex, Int32 _seqNo, string _deleteFrom)
        {
            try
            {
                string _item = string.Empty;
                string _itmStatus = string.Empty;
                Int32 _serialID = 0;
                decimal _qty = 0;
                int _lineNo = 0;

                this.Cursor = Cursors.WaitCursor;

                if (_deleteFrom == "ITEM")
                {
                    if (string.IsNullOrEmpty(gvItems.Rows[_rowIndex].Cells["itm_Item"].Value.ToString())) return;
                    _item = Convert.ToString(gvItems.Rows[_rowIndex].Cells["itm_Item"].Value);
                    _itmStatus = Convert.ToString(gvItems.Rows[_rowIndex].Cells["itm_Status"].Value);
                    _qty = Convert.ToDecimal(gvItems.Rows[_rowIndex].Cells["itm_PickQty"].Value);
                    _lineNo = Convert.ToInt32(gvItems.Rows[_rowIndex].Cells["itm_Lineno"].Value);
                }
                else if (_deleteFrom == "SERIAL")
                {
                    if (string.IsNullOrEmpty(gvSerial.Rows[_rowIndex].Cells["ser_Item"].Value.ToString())) return;
                    _item = Convert.ToString(gvSerial.Rows[_rowIndex].Cells["ser_Item"].Value);
                    _itmStatus = Convert.ToString(gvSerial.Rows[_rowIndex].Cells["ser_Status"].Value);
                    _serialID = Convert.ToInt32(gvSerial.Rows[_rowIndex].Cells["ser_SerialID"].Value);
                    _lineNo = Convert.ToInt32(gvSerial.Rows[_rowIndex].Cells["ser_BaseLineNo"].Value);
                    _qty = Convert.ToDecimal(gvSerial.Rows[_rowIndex].Cells["ser_Qty"].Value);
                }
                
                //load all items
                List<ReptPickItems> _pickItmes = new List<ReptPickItems>();
                _pickItmes = CHNLSVC.Inventory.GetAllScanRequestItemsList(_seqNo);
                if (_pickItmes != null && _pickItmes.Count > 0)
                {
                    //get selected item details
                    _pickItmes = _pickItmes.Where(x => x.Tui_req_itm_cd == _item && x.Tui_req_itm_stus == _itmStatus && x.Tui_pic_itm_cd == _lineNo.ToString()).ToList();
                    if (_pickItmes != null && _pickItmes.Count > 0)
                    {
                        foreach (ReptPickItems _deleteItem in _pickItmes)
                        {
                            _deleteItem.Tui_req_itm_qty -= _qty;
                            _deleteItem.Tui_pic_itm_qty -= _qty;

                            List<ReptPickSerials> _pickedSerials = new List<ReptPickSerials>();
                            _pickedSerials = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, Convert.ToInt32(_seqNo), "ADJ-S");
                            if (_pickedSerials != null && _pickedSerials.Count > 0)
                            {
                                if (_deleteFrom == "SERIAL")
                                {
                                    _pickedSerials = _pickedSerials.Where(x => x.Tus_usrseq_no == _deleteItem.Tui_usrseq_no && x.Tus_itm_cd == _deleteItem.Tui_req_itm_cd && x.Tus_itm_stus == _deleteItem.Tui_req_itm_stus && x.Tus_base_itm_line == Convert.ToInt32(_deleteItem.Tui_pic_itm_cd) && x.Tus_ser_id == _serialID).ToList();
                                }
                                else 
                                {
                                    _pickedSerials = _pickedSerials.Where(x => x.Tus_usrseq_no == _deleteItem.Tui_usrseq_no && x.Tus_itm_cd == _deleteItem.Tui_req_itm_cd && x.Tus_itm_stus == _deleteItem.Tui_req_itm_stus && x.Tus_base_itm_line == Convert.ToInt32(_deleteItem.Tui_pic_itm_cd)).ToList();
                                }
                                
                                if (_pickedSerials != null && _pickedSerials.Count > 0)
                                {
                                    foreach (ReptPickSerials _pickSerial in _pickedSerials)
                                    {
                                        bool _isdeleted = CHNLSVC.Inventory.Del_temp_pick_ser(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca,  _pickSerial.Tus_usrseq_no, _pickSerial.Tus_ser_id, null, null, _deleteItem);
                                        if (_isdeleted)
                                        {
                                            CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _pickSerial.Tus_itm_cd, _serialID, 1);
                                        }
                                    }
                                }
                            }
                            else { CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), _item, _itmStatus, _lineNo, 2); }

                            if (_deleteItem.Tui_pic_itm_qty < 1 || _deleteItem.Tui_req_itm_qty < 1)
                            {
                                CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), _item, _itmStatus, _lineNo, 2);
                            }
                        }
                    }
                }

                LoadItems(txtUserSeqNo.Text);
                this.Cursor = Cursors.Default;
                if (gvItems.Rows.Count < 1) { btnClear_Click(null, null); }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

    }

}
