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
using System.IO;
using System.Configuration;
using System.Data.OleDb;

namespace FF.WindowsERPClient.Inventory
{
    public partial class StockAdjustment : FF.WindowsERPClient.Base
    {
        #region Varialbe
        public string AdjType = string.Empty;
        private List<InventoryRequestItem> ScanItemList = null;
        private string _receCompany = string.Empty;
        private string RequestNo = string.Empty;
        private string SelectedStatus = string.Empty;
        private MasterItem _itemdetail = null;
        bool _isDecimalAllow = false;
        private List<ReptPickSerials> serial_list = null;
        private List<ReptPickSerials> SelectedSerialList = null;
        //CommonSearch.CommonOutScan _commonOutScan = null;
        private string _chargeType = string.Empty;
        private List<string> SeqNumList = null;
        private string _iscostupdate = "N";
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

        //protected void BindPickSerials(int _userSeqNo)
        //{
        //    try
        //    {
        //        this.Cursor = Cursors.WaitCursor;
        //        SelectedSerialList.RemoveAll(X => X.Tus_usrseq_no == _userSeqNo);
        //        BindingSource _source = new BindingSource();

        //        List<ReptPickSerials> _list = new List<ReptPickSerials>();
        //        _source.DataSource = _list;
        //        gvSerial.DataSource = _source;

        //        _list = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, COM_OUT);
        //        if (_list == null || _list.Count <= 0) { if (SelectedSerialList != null) if (SelectedSerialList.Count > 0) { _source.DataSource = SelectedSerialList; gvSerial.DataSource = _source; } return; }
        //        SelectedSerialList.AddRange(_list); _source.DataSource = SelectedSerialList; gvSerial.DataSource = _source;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Cursor = Cursors.Default;
        //        MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        CHNLSVC.CloseChannel(); 
        //    }
        //    finally { this.Cursor = Cursors.Default; }
        //}

        private void InitializeForm(bool _isDocType)
        {
            try
            {
                dtpDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
                this.Cursor = Cursors.WaitCursor;

                SelectedSerialList = new List<ReptPickSerials>();
                ScanItemList = new List<InventoryRequestItem>();
                _itemdetail = new MasterItem();
                serial_list = new List<ReptPickSerials>();
                gvItems.AutoGenerateColumns = false;
                gvSerial.AutoGenerateColumns = false;
                //ddlAdjType.SelectedIndex = 0;
                ddlAdjTypeSearch.SelectedIndex = 0;
                BindUserCompanyItemStatusDDLData(ddlStatus);
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
                CHNLSVC.CloseAllChannels();
            }
        }
        public StockAdjustment()
        {
            //_commonOutScan = new CommonSearch.CommonOutScan();
            InitializeComponent();
            InitializeForm(true);
            //_commonOutScan.AddSerialClick += new EventHandler(_commonOutScan_AddSerialClick);
            //Added by Prabhath on 17/12/2013 ************* start **************
            LoadAFSL();
            //Added by Prabhath on 17/12/2013 ************* end **************
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
        #endregion

        #region Rooting for Screen Navigation
        private void txtDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ddlAdjType.Focus();
        }

        private void ddlAdjType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAdjSubType.Focus();

        }

        private void txtAdjSubType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtManualRef.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_AdjSubType_Click(null, null);
        }

        private void txtAdjSubType_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_AdjSubType_Click(null, null);
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

        private void ddlStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ddlAdjType.SelectedItem.ToString() == "ADJ-") txtQty.Focus(); else txtUnitCost.Focus();
            }
        }

        private void txtUnitCost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (ddlAdjType.SelectedItem.ToString() == "ADJ-")
                {
                    txtQty.Focus();
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtUnitCost.Text)) txtQty.Focus();
                }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (!string.IsNullOrEmpty(txtQty.Text)) btnAddItem.Focus();
        }

        private void txtAdjSubType_TextChanged(object sender, EventArgs e)
        {
            lblSubTypeDesc.Text = string.Empty;
        }

        private void txtAdjSubType_Leave(object sender, EventArgs e)
        {
            try
            {
                lblSubTypeDesc.Text = string.Empty;
                if (string.IsNullOrEmpty(txtAdjSubType.Text)) return;
                if (IsValidAdjustmentSubType() == false)
                {
                    MessageBox.Show("Invalid adjustment sub type.", "Adjustment Sub Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblSubTypeDesc.Text = string.Empty;
                    txtAdjSubType.Clear();
                    txtAdjSubType.Focus();
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

        private void txtUnitCost_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUnitCost.Text))
                {
                    if (IsNumeric(txtUnitCost.Text.Trim()) == false)
                    {
                        MessageBox.Show("Invalid unit cost. Please enter the valid amount.", "unit cost", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUnitCost.Clear();
                        txtUnitCost.Focus();
                        return;
                    }
                }
                else
                {
                    if (ddlAdjType.SelectedItem.ToString() == "ADJ-")
                    {
                        txtUnitCost.Text = "0";
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

        private void ddlAdjType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlAdjType.Enabled = false;
                if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
                {
                    SeqNumList = CHNLSVC.Inventory.Get_User_Seq_Batch(BaseCls.GlbUserID, "ADJ", 1, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                    pnlSup.Visible = true;
                }
                else
                {
                    SeqNumList = CHNLSVC.Inventory.Get_User_Seq_Batch(BaseCls.GlbUserID, "ADJ", 0, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                    pnlSup.Visible = false;
                }
                ddlSeqNo.DataSource = SeqNumList;
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

        private void ddlSeqNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ddlSeqNo.Text))
                {
                    txtUserSeqNo.Text = ddlSeqNo.Text;
                    LoadItems(txtUserSeqNo.Text);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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
            finally
            {
                CHNLSVC.CloseAllChannels();
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
                    gvItems.ReadOnly = false;
                    gvSerial.ReadOnly = false;
                    List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                    gvSerial.AutoGenerateColumns = false;
                    gvSerial.DataSource = _emptySer;

                    List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                    gvItems.AutoGenerateColumns = false;
                    gvItems.DataSource = _emptyItm;

                    btnSave.Enabled = true;
                    txtAdjSubType.Text = string.Empty;
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
                        cmdPrint.Enabled = true;
                        gvItems.ReadOnly = true;
                        gvSerial.ReadOnly = true;
                        btnSave.Enabled = false;
                        txtAdjSubType.Text = _invHdr.Ith_sub_tp;
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtDocumentNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAdjSubType.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_DocumentNo_Click(null, null);
        }

        private void txtDocumentNo_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Rooting for Load Item Detail
        private bool LoadItemDetail(string _item)
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
                        if (_itemdetail.Mi_itm_tp == "V")
                        {
                            MessageBox.Show("Virtual item not allowed", "Item Code", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtItem.Clear();
                            lblItemDescription.Text = "Description : " + string.Empty;
                            lblItemModel.Text = "Model : " + string.Empty;
                            lblItemBrand.Text = "Brand : " + string.Empty;
                            lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
                            txtUnitCost.Text = string.Empty;
                            txtQty.Text = string.Empty;
                            return false;
                        }


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
                        //kapila 3/7/2015
                        if (_itemdetail.MI_IS_EXP_DT == 1)
                        {
                            lblExpDt.Visible = true;
                            dtExp.Visible = true;
                        }
                        else
                        {
                            lblExpDt.Visible = false;
                            dtExp.Visible = false;
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
                    txtUnitCost.Text = string.Empty;
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
                CHNLSVC.CloseAllChannels();
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
                case CommonUIDefiniton.SearchUserControlType.GRNNo:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
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
                case CommonUIDefiniton.SearchUserControlType.AvailableSerial:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + txtItem.Text + seperator);
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

        private void btnSearch_AdjSubType_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
                DataTable _result = CHNLSVC.CommonSearch.GetMovementDocSubTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAdjSubType;
                _CommonSearch.ShowDialog();
                txtAdjSubType.Select();
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

        private void btnSearch_Request_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem;
                _CommonSearch.ShowDialog();
                txtItem.Select();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
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
            txtItem.Text = string.Empty;
            txtUnitCost.Text = string.Empty;
            txtQty.Text = string.Empty;
            //ddlStatus.DataSource = null;
            gvBalance.DataSource = null;
            gvBalance.AutoGenerateColumns = false;
            gvBalance.DataSource = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, string.Empty);
        }
        #endregion

        #region Rooting for Check Item
        protected void CheckItem()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtItem.Text)) return;

                if (BaseCls.GlbUserComCode == "AST")
                {
                    string _item = "";
                    //kapila 18/11/2013
                    if (txtItem.Text.Length == 16)
                        _item = txtItem.Text.Substring(1, 7);
                    else if (txtItem.Text.Length == 15)
                        _item = txtItem.Text.Substring(0, 7);
                    else if (txtItem.Text.Length == 8)
                        _item = txtItem.Text.Substring(1, 7);
                    else if (txtItem.Text.Length == 20)
                        _item = txtItem.Text.Substring(0, 12);
                    else
                        _item = txtItem.Text;

                    txtItem.Text = _item;
                }

                if (string.IsNullOrEmpty(ddlAdjType.Text))
                {
                    MessageBox.Show("Please select the adjustment type!", "Adjustment Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Clear();
                    ddlAdjType.Focus();
                    return;
                }

                if (LoadItemDetail(txtItem.Text.Trim()) == false)
                {
                    txtItem.Focus();
                    return;
                }

                txtItem.Text = txtItem.Text.Trim();

                DataTable _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), string.Empty);
                if (ddlAdjType.SelectedItem.ToString() == "ADJ-")
                {
                    if (_inventoryLocation == null || _inventoryLocation.Rows.Count <= 0)
                    {
                        MessageBox.Show("No stock balance available", "Qty Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ddlStatus.DataSource = new List<InventoryLocation>();
                        txtItem.Focus();
                        txtItem.SelectAll();
                        return;
                    }
                    else
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
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }

        }
        private void txtItem_Leave(object sender, EventArgs e)
        {
            CheckItem();
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

                this.Cursor = Cursors.Default;
                if (ddlAdjType.SelectedItem.ToString() == "ADJ-")
                {
                    //check for the location balance.
                    List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString());
                    if (_inventoryLocation.Count == 1)
                    {
                        foreach (InventoryLocation _loc in _inventoryLocation)
                        {
                            decimal _formQty = Convert.ToDecimal(txtQty.Text);
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
                CHNLSVC.CloseAllChannels();
            }
        }
        private void txtQty_Leave(object sender, EventArgs e)
        {
            try
            {
                CheckQty();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(_isDecimalAllow, sender, e);
        }
        #endregion

        #region Rooting for Check Adjustment Sub Type
        private bool IsValidAdjustmentSubType()
        {
            bool status = false;
            txtAdjSubType.Text = txtAdjSubType.Text.Trim().ToUpper().ToString();
            DataTable _adjSubType = CHNLSVC.Inventory.GetMoveSubTypeAllTable("ADJ", txtAdjSubType.Text.ToString());
            if (_adjSubType.Rows.Count > 0)
            {
                lblSubTypeDesc.Text = _adjSubType.Rows[0]["mmct_sdesc"].ToString();
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }
        #endregion

        #region Rooting for Add Item

        protected void AddItem()
        {
            try
            {

                if (string.IsNullOrEmpty(txtItem.Text.Trim()))
                {
                    MessageBox.Show("Please select the item.", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }
                if (_iscostupdate == "N")
                {
                    if (string.IsNullOrEmpty(ddlStatus.Text.ToString()))
                    {
                        MessageBox.Show("Please select the status.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ddlStatus.Focus();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    MessageBox.Show("Please select the qty.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Focus();
                    return;
                }
                if (pnlSup.Visible == true)
                    if (dtGRN.Value.Date > dtpDate.Value.Date)
                    {
                        MessageBox.Show("Invalid GRN Date", "GRN Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtGRN.Value = dtpDate.Value.Date;
                        return;
                    }


                //if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
                //{
                //    MasterItem _mstItm = new MasterItem();
                //    _mstItm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                //    if (_mstItm.Mi_is_ser1 == 1)    //kapila 26/8/2015
                //    {
                //        bool _permission = CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11065);
                //        if (!_permission)
                //        {
                //            if (string.IsNullOrEmpty(txtGRNno.Text) || string.IsNullOrEmpty(txtSup.Text) || string.IsNullOrEmpty(txtBatch.Text))
                //            {
                //                MessageBox.Show("Please enter supplier/GRN No/Batch No (Permission code-11067)", "Unit Cost", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                return;
                //            }
                //        }
                //        else
                //        {
                //            if (MessageBox.Show("GRN details not entered. Are you sure ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No) return;
                //        }
                //    }
                //}

                if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
                {

                    if (ddlStatus.Text.ToString().ToUpper() == "CONSIGNMENT")
                    {
                        if (string.IsNullOrEmpty(txtSup.Text))
                            {
                                MessageBox.Show("Please enter supplier", "Unit Cost", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                    }
                }

                if (ddlAdjType.SelectedItem.ToString() == "ADJ+" && AdjType != "AFSL")
                {
                    if (string.IsNullOrEmpty(txtUnitCost.Text))
                    {
                        MessageBox.Show("Please enter unit cost amount.", "Unit Cost", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUnitCost.Focus();
                        return;
                    }
                    else
                    {
                        if (Convert.ToDecimal(txtUnitCost.Text.ToString()) <= 0 && ddlStatus.Text.ToString().ToUpper() != "CONSIGNMENT")
                        {
                            if (_iscostupdate == "N")
                            {
                                if (_chargeType != "FOC")
                                {
                                    MessageBox.Show("Unit cost can't be less than or equal to zero amount.", "Unit Cost", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtUnitCost.Clear();
                                    txtUnitCost.Focus();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (ddlStatus.Text.ToString().ToUpper() == "CONSIGNMENT")
                            {
                                if (Convert.ToDecimal(txtUnitCost.Text.ToString()) != 0)
                                {
                                    MessageBox.Show("Unit cost should be zero(0.00) for CONSIGNMENT status.", "Unit Cost", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtUnitCost.Clear();
                                    txtUnitCost.Focus();
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    txtUnitCost.Text = "0";
                }


                this.Cursor = Cursors.WaitCursor;
                MasterItem _itms = new MasterItem();
                _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                InventoryRequestItem _itm = new InventoryRequestItem();

                //Added by Prabhath on 17/12/2013 ************* start **************
                Decimal _ucost = Convert.ToDecimal(txtUnitCost.Text.Trim());
                if (AdjType == "AFSL")
                {
                    _ucost = CHNLSVC.Inventory.GetLatestCost(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), "GOD");
                    //Common function written by Sachith, Where used it in Revert Module Line 816 - 836, pick it as same
                    List<InventoryCostRate> _costList = CHNLSVC.Inventory.GetInventoryCostRate(BaseCls.GlbUserComCode, "AFSLRV", Convert.ToString(ddlStatus.SelectedValue), (((dtpDate.Value.Year - dtpDate.Value.Year) * 12) + dtpDate.Value.Month - dtpDate.Value.Month), "GOD");
                    if (_costList != null && _costList.Count > 0)
                        if (_costList[0].Rcr_rt == 0) _ucost = Math.Round(_ucost - _costList[0].Rcr_val, 2);
                        else _ucost = Math.Round(_ucost - ((_ucost * _costList[0].Rcr_rt) / 100), 2);
                }
                //Added by Prabhath on 17/12/2013 ************* end **************

                if (ScanItemList != null)
                    if (ScanItemList.Count > 0)
                    {
                        var _duplicate = from _ls in ScanItemList
                                         where _ls.Itri_itm_cd == txtItem.Text.Trim() && _ls.Itri_itm_stus == ddlStatus.SelectedValue.ToString()
                                         select _ls;

                        if (_iscostupdate == "N")
                        {
                            if (_duplicate != null)
                                if (_duplicate.Count() > 0)
                                {
                                    this.Cursor = Cursors.Default;
                                    MessageBox.Show("Selected item already available", "Item Duplication", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                        }
                        var _maxline = (from _ls in ScanItemList
                                        select _ls.Itri_line_no).Max();
                        _itm.Itri_app_qty = Convert.ToDecimal(txtQty.Text);
                        _itm.Itri_itm_cd = txtItem.Text.Trim();
                        _itm.Itri_itm_stus = ddlStatus.SelectedValue.ToString();
                        _itm.Itri_line_no = Convert.ToInt32(_maxline) + 1;
                        _itm.Itri_qty = 0;
                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        //Added by Prabhath on 17/12/2013 ************* start **************
                        _itm.Itri_unit_price = _ucost;
                        //Added by Prabhath on 17/12/2013 ************* end **************
                        //kapila 3/7/2015
                        _itm.Itri_batchno = txtBatch.Text;
                        if (dtExp.Visible == true)
                            _itm.Itri_expdate = dtExp.Value.Date;

                        _itm.Itri_grndate = dtGRN.Value.Date;
                        _itm.Itri_grnno = txtGRNno.Text;
                        _itm.Itri_supplier = txtSup.Text;

                    }
                    else
                    {
                        _itm.Itri_app_qty = Convert.ToDecimal(txtQty.Text);
                        _itm.Itri_itm_cd = txtItem.Text.Trim();
                        _itm.Itri_itm_stus = ddlStatus.SelectedValue.ToString();
                        _itm.Itri_line_no = 1;
                        _itm.Itri_qty = 0;
                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        //Added by Prabhath on 17/12/2013 ************* start **************
                        _itm.Itri_unit_price = _ucost;
                        //Added by Prabhath on 17/12/2013 ************* end **************
                        //kapila 3/7/2015
                        _itm.Itri_batchno = txtBatch.Text;
                        if (dtExp.Visible == true)
                        _itm.Itri_expdate = dtExp.Value.Date;
                        _itm.Itri_grndate = dtGRN.Value.Date;
                        _itm.Itri_grnno = txtGRNno.Text;
                        _itm.Itri_supplier = txtSup.Text;
                    }

                ScanItemList.Add(_itm);

                if (string.IsNullOrEmpty(txtUserSeqNo.Text))
                {
                    GenerateNewUserSeqNo();
                }

                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                foreach (InventoryRequestItem _addedItem in ScanItemList)
                {
                    ReptPickItems _reptitm = new ReptPickItems();
                    _reptitm.Tui_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);
                    _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                    _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                    _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus;
                    _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_line_no);
                    _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                    _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;
                    _reptitm.Tui_sup = txtSup.Text;
                    _reptitm.Tui_grn = txtGRNno.Text;
                    _reptitm.Tui_batch = txtBatch.Text;
                    _reptitm.Tui_grn_dt = dtGRN.Value.Date;
                    if (dtExp.Visible == true)
                    _reptitm.Tui_exp_dt = dtExp.Value.Date;
                    _saveonly.Add(_reptitm);
                }
                CHNLSVC.Inventory.SavePickedItems(_saveonly);

                gvItems.DataSource = null;
                gvItems.DataSource = ScanItemList;

                ClearItemDetail();
                LoadItemDetail(string.Empty);
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
                CHNLSVC.CloseAllChannels();
                
                    //ClearItemDetail(); LoadItemDetail(string.Empty);
                    //if (MessageBox.Show("Do you need to add another item?", "Adding...", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    //    txtItem.Focus();
                    //else
                    //    gvItems.Focus();
                
            }
        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddItem();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        #endregion

        //Added by Prabhath on 17/12/2013 ************* end **************
        private void LoadAFSL()
        {
            if (AdjType == "AFSL")
            {
                ddlAdjType.SelectedIndex = 0;
                ddlAdjType.Enabled = false;
                txtAdjSubType.Enabled = false;
                txtAdjSubType.Text = "AFSL";
                lblSubTypeDesc.Text = "AFSL REVERTS";
                btnSearch_AdjSubType.Enabled = false;
                panel1.Enabled = false;
                this.Text = "AFSL Revert Entry";
                ddlStatus.SelectedValue = "RVT";
                ddlStatus.Enabled = false;
                label1.Visible = false;
                txtUnitCost.Visible = false;
                ddlStatus.Size = new System.Drawing.Size(241, 21);
                itri_unit_price.Visible = false;
                txtManualRef.Focus();
            }
        }
        //Added by Prabhath on 17/12/2013 ************* end **************

        #region Rooting for Clear Screen
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear the screen?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;

            this.Cursor = Cursors.WaitCursor;
            try
            {
                StockAdjustment _JobEntry = new StockAdjustment();
                _JobEntry.MdiParent = this.MdiParent;
                _JobEntry.Location = this.Location;
                _JobEntry.GlbModuleName = this.GlbModuleName;
                _JobEntry.Show();
                this.Close();
                this.Dispose();
                GC.Collect();

                //while (this.Controls.Count > 0)
                //{
                //    Controls[0].Dispose();
                //}
                ////_commonOutScan = new CommonSearch.CommonOutScan();
                //InitializeComponent();
                //InitializeForm(true);
                ////_commonOutScan.AddSerialClick += new EventHandler(_commonOutScan_AddSerialClick);
                //if (AdjType == "AFSL")
                //{
                //    ddlAdjType.SelectedIndex = 0;
                //    ddlAdjType.Enabled = false;
                //    txtAdjSubType.Enabled = false;
                //    txtAdjSubType.Text = "AFSL";
                //    lblSubTypeDesc.Text = "AFSL REVERTS";
                //    btnSearch_AdjSubType.Enabled = false;
                //    panel1.Enabled = false;
                //    this.Text = "AFSL Revert Entry";
                //    ddlStatus.SelectedValue = "RVT";
                //    ddlStatus.Enabled = false;
                //    label1.Visible = false;
                //    txtUnitCost.Visible = false;
                //    ddlStatus.Size = new System.Drawing.Size(241, 21);
                //    txtManualRef.Focus();
                //}
                //bool _allowCurrentTrans = false;
                //IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, dtpDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
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

                        if (e.ColumnIndex == 0)
                        {
                            #region Add Serials
                            if (gvItems.Columns[_colIndex].Name == "itm_AddSerial")
                            {
                                if (Convert.ToDecimal(gvItems.Rows[e.RowIndex].Cells["itm_AppQty"].Value.ToString()) <= 0) return;
                                if (Convert.ToDecimal(gvItems.Rows[e.RowIndex].Cells["itm_AppQty"].Value.ToString()) <= Convert.ToDecimal(gvItems.Rows[e.RowIndex].Cells["itm_PickQty"].Value.ToString())) return;

                                if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
                                {
                                    CommonSearch.CommonInScan _commonInScan = new CommonSearch.CommonInScan();
                                    _commonInScan.ModuleTypeNo = 1;
                                    _commonInScan.ScanDocument = txtUserSeqNo.Text.ToString();
                                    _commonInScan.DocumentType = "ADJ";
                                    _commonInScan.PopupItemCode = gvItems.Rows[e.RowIndex].Cells["itm_Item"].Value.ToString();
                                    _commonInScan.ItemStatus = gvItems.Rows[e.RowIndex].Cells["itm_Status"].Value.ToString();
                                    _commonInScan.ItemLineNo = Convert.ToInt32(gvItems.Rows[e.RowIndex].Cells["itm_Lineno"].Value.ToString());
                                    _commonInScan.UnitCost = Convert.ToDecimal(gvItems.Rows[e.RowIndex].Cells["itri_unit_price"].Value.ToString());
                                    _commonInScan.UnitPrice = 0;
                                    _commonInScan.DocQty = Convert.ToDecimal(gvItems.Rows[e.RowIndex].Cells["itm_AppQty"].Value.ToString());
                                    _commonInScan.ScanQty = Convert.ToDecimal(gvItems.Rows[e.RowIndex].Cells["itm_PickQty"].Value.ToString());
                                    _commonInScan.Location = new Point(((this.Width - _commonInScan.Width) / 2), ((this.Height - _commonInScan.Height) / 2) + 50);
                                    //kapila 3/7/2015
                                    _commonInScan.Supplier = gvItems.Rows[e.RowIndex].Cells["Itri_supplier"].Value.ToString();
                                    _commonInScan.PopupBatchNo = gvItems.Rows[e.RowIndex].Cells["Itri_batchno"].Value.ToString();
                                    _commonInScan.GRNNo = gvItems.Rows[e.RowIndex].Cells["Itri_grnno"].Value.ToString();
                                    _commonInScan.GRNDate = Convert.ToDateTime(gvItems.Rows[e.RowIndex].Cells["Itri_grndate"].Value);
                                    MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, gvItems.Rows[e.RowIndex].Cells["itm_Item"].Value.ToString());   //27/8/2015
                                    if (_itm.MI_IS_EXP_DT == 1)
                                    _commonInScan.PopupExpDate = Convert.ToDateTime(gvItems.Rows[e.RowIndex].Cells["Itri_expdate"].Value);
                                    _commonInScan.ShowDialog();
                                }
                                else
                                {
                                    CommonSearch.CommonOutScan _commonOutScan = new CommonSearch.CommonOutScan();
                                    _commonOutScan.ModuleTypeNo = 4;
                                    _commonOutScan.ScanDocument = txtUserSeqNo.Text.ToString();
                                    _commonOutScan.DocumentType = "COM_OUT";
                                    _commonOutScan.IsCheckStatus = true;
                                    _commonOutScan.PopupItemCode = gvItems.Rows[e.RowIndex].Cells["itm_Item"].Value.ToString();
                                    _commonOutScan.ItemStatus = gvItems.Rows[e.RowIndex].Cells["itm_Status"].Value.ToString();
                                    _commonOutScan.ItemLineNo = Convert.ToInt32(gvItems.Rows[e.RowIndex].Cells["itm_Lineno"].Value.ToString());
                                    _commonOutScan.PopupQty = Convert.ToDecimal(gvItems.Rows[e.RowIndex].Cells["itm_AppQty"].Value.ToString()) - Convert.ToDecimal(gvItems.Rows[e.RowIndex].Cells["itm_PickQty"].Value.ToString());
                                    _commonOutScan.ApprovedQty = Convert.ToDecimal(gvItems.Rows[e.RowIndex].Cells["itm_AppQty"].Value.ToString()) - Convert.ToDecimal(gvItems.Rows[e.RowIndex].Cells["itm_PickQty"].Value.ToString());
                                    _commonOutScan.ScanQty = Convert.ToDecimal(gvItems.Rows[e.RowIndex].Cells["itm_PickQty"].Value.ToString());
                                    _commonOutScan.IsCheckStatus = true;
                                    _commonOutScan.Location = new Point(((this.Width - _commonOutScan.Width) / 2), ((this.Height - _commonOutScan.Height) / 2) + 50);
                                    //this.Enabled = false;
                                    _commonOutScan.ShowDialog();
                                }
                                LoadItems(txtUserSeqNo.Text.ToString());
                            }
                            #endregion
                        }
                        else if (e.ColumnIndex == 1)
                        {
                            #region Remove Item
                            if (gvItems.Columns[_colIndex].Name == "itm_Remove")
                            {
                                if (MessageBox.Show("Do you need to remove this record?", "Remove...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                { OnRemoveFromItemGrid(_rowIndex, _colIndex); }
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        #region Rooting for Load Items/Serials
        private void LoadItems(string _seqNo)
        {
            try
            {
                int _direction = 0;
                if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
                {
                    _direction = 1;
                }

                Int32 user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "ADJ", BaseCls.GlbUserID, _direction, _seqNo);
                if (user_seq_num == -1)
                {
                    user_seq_num = GenerateNewUserSeqNo();
                }

                List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                List<ReptPickItems> _reptItems = new List<ReptPickItems>();
                _reptItems = CHNLSVC.Inventory.GetAllScanRequestItemsList(user_seq_num);
                foreach (ReptPickItems _reptitem in _reptItems)
                {
                    InventoryRequestItem _itm = new InventoryRequestItem();
                    MasterItem _itms = new MasterItem();
                    _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _reptitem.Tui_req_itm_cd);
                    _itm.Itri_app_qty = _reptitem.Tui_req_itm_qty;
                    _itm.Itri_itm_cd = _reptitem.Tui_req_itm_cd;
                    _itm.Itri_itm_stus = _reptitem.Tui_req_itm_stus;
                    _itm.Itri_line_no = Convert.ToInt32(_reptitem.Tui_pic_itm_cd.ToString());
                    _itm.Itri_qty = 0;
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    _itm.Itri_unit_price = Convert.ToDecimal(_reptitem.Tui_pic_itm_stus.ToString());
                    _itm.Itri_supplier = _reptitem.Tui_sup;
                    _itm.Itri_batchno = _reptitem.Tui_batch;
                    _itm.Itri_grnno = _reptitem.Tui_grn;
                    _itm.Itri_grndate = _reptitem.Tui_grn_dt;
                    _itm.Itri_expdate = _reptitem.Tui_exp_dt;
                    _itmList.Add(_itm);
                }
                ScanItemList = _itmList.OrderBy(o => o.Itri_line_no).ToList(); ;
                gvItems.AutoGenerateColumns = false;
                gvItems.DataSource = ScanItemList;

                List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                _serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "ADJ");
                if (_serList != null)
                {
                    if (_direction == 0)
                    {
                        //var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                        foreach (var itm in _scanItems)
                        {
                            foreach (DataGridViewRow row in this.gvItems.Rows)
                            {
                                if (itm.Peo.Tus_itm_cd == row.Cells["itm_Item"].Value.ToString() && itm.Peo.Tus_base_itm_line == Convert.ToInt32(row.Cells["itm_Lineno"].Value.ToString()))
                                {
                                    row.Cells["itm_PickQty"].Value = Convert.ToDecimal(itm.theCount); // Current scan qty
                                }
                            }
                        }
                    }
                    else
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                        foreach (var itm in _scanItems)
                        {
                            foreach (DataGridViewRow row in this.gvItems.Rows)
                            {
                                if (itm.Peo.Tus_itm_cd == row.Cells["itm_Item"].Value.ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(row.Cells["itm_Lineno"].Value.ToString()))
                                {
                                    row.Cells["itm_PickQty"].Value = Convert.ToDecimal(itm.theCount); // Current scan qty
                                }
                            }
                        }
                    }
                    gvSerial.AutoGenerateColumns = false;
                    gvSerial.DataSource = _serList;

                }
                else
                {
                    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                    gvSerial.AutoGenerateColumns = false;
                    gvSerial.DataSource = emptyGridList;

                }
                //gvItems.AutoGenerateColumns = false;
                //gvItems.DataSource = gvItems;
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
        #endregion

        #region Generate new user seq no
        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            Int16 _direction = 0;
            if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
            {
                _direction = 1;
            }
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "ADJ", _direction, BaseCls.GlbUserComCode);//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = "ADJ";
            RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
            RPH.Tuh_ischek_itmstus = true;//might change 
            RPH.Tuh_ischek_reqqty = true;//might change
            RPH.Tuh_ischek_simitm = true;//might change
            RPH.Tuh_session_id = BaseCls.GlbUserSessionID;
            RPH.Tuh_usr_com = BaseCls.GlbUserComCode;//might change 
            RPH.Tuh_usr_id = BaseCls.GlbUserID;
            RPH.Tuh_usrseq_no = generated_seq;
            if (_direction == 1)//direction always (-) for change status
            {
                RPH.Tuh_direct = true;
            }
            else
            {
                RPH.Tuh_direct = false;
            }
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
            try
            {
                Process();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void Process()
        {
            try
            {
                if (CheckServerDateTime() == false) return;

                if (string.IsNullOrEmpty(ddlAdjType.Text))
                {
                    MessageBox.Show("Select the adjustment type!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtAdjSubType.Text))
                {
                    MessageBox.Show("Select the adjustment sub type!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtManualRef.Text)) txtManualRef.Text = "N/A";
                if (string.IsNullOrEmpty(txtOtherRef.Text)) txtOtherRef.Text = string.Empty;
                if (string.IsNullOrEmpty(txtRemarks.Text)) txtRemarks.Text = string.Empty;

                bool _allowCurrentTrans = false;
                if (_iscostupdate == "N")
                {
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
                }

                //kapila 14/8/2015
                foreach (DataGridViewRow row in this.gvItems.Rows)
                {
                    if (Convert.ToDecimal(row.Cells["itm_AppQty"].Value) > Convert.ToDecimal(row.Cells["itm_PickQty"].Value))
                    {
                        MessageBox.Show("All serials not entered !", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                Cursor.Current = Cursors.WaitCursor;

                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                string documntNo = "";
                Int32 result = -99;
                Int32 _userSeqNo = 0;
                int _direction = 0;
                if (ddlAdjType.SelectedItem.ToString() == "ADJ+") _direction = 1;


                _userSeqNo = CHNLSVC.Inventory.Get_Scan_SeqNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "ADJ", BaseCls.GlbUserID, _direction, txtUserSeqNo.Text);
                reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, "ADJ");
                reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "ADJ");
                if (reptPickSerialsList == null)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                #region Check Referance Date and the Doc Date
                if (_direction == 0)
                {
                    if (IsReferancedDocDateAppropriate(reptPickSerialsList, dtpDate.Value.Date) == false)
                    {
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                }
                #endregion

                if (_iscostupdate == "N")
                {
                    if (MessageBox.Show("Do you want to save this?", "Saving... : " + ddlAdjType.SelectedItem.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    {
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                }
                #region Check Duplicate Serials
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
                #endregion
                InventoryHeader inHeader = new InventoryHeader();
                #region Fill InventoryHeader
                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                foreach (DataRow r in dt_location.Rows)
                {
                    // Get the value of the wanted column and cast it to string
                    inHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                    if (System.DBNull.Value != r["ML_CATE_2"])
                    {
                        inHeader.Ith_channel = (string)r["ML_CATE_2"];
                    }
                    else
                    {
                        inHeader.Ith_channel = string.Empty;
                    }
                }
                inHeader.Ith_acc_no = "STOCK_ADJ";
                inHeader.Ith_anal_1 = "";
                inHeader.Ith_anal_2 = "";
                inHeader.Ith_anal_3 = "";
                inHeader.Ith_anal_4 = "";
                inHeader.Ith_anal_5 = "";
                inHeader.Ith_anal_6 = 0;
                inHeader.Ith_anal_7 = 0;
                inHeader.Ith_anal_8 = DateTime.MinValue;
                inHeader.Ith_anal_9 = DateTime.MinValue;
                inHeader.Ith_anal_10 = false;
                inHeader.Ith_anal_11 = false;
                inHeader.Ith_anal_12 = false;
                inHeader.Ith_bus_entity = "";
                inHeader.Ith_cate_tp = txtAdjSubType.Text.ToString().Trim();

                inHeader.Ith_com = BaseCls.GlbUserComCode;
                inHeader.Ith_com_docno = "";
                inHeader.Ith_cre_by = BaseCls.GlbUserID;
                inHeader.Ith_cre_when = DateTime.Now;
                inHeader.Ith_del_add1 = "";
                inHeader.Ith_del_add2 = "";
                inHeader.Ith_del_code = "";
                inHeader.Ith_del_party = "";
                inHeader.Ith_del_town = "";
                if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
                {
                    inHeader.Ith_direct = true;
                }
                else
                {
                    inHeader.Ith_direct = false;
                }
                inHeader.Ith_doc_date = dtpDate.Value.Date;
                inHeader.Ith_doc_no = string.Empty;
                inHeader.Ith_doc_tp = "ADJ";
                inHeader.Ith_doc_year = dtpDate.Value.Date.Year;
                inHeader.Ith_entry_no = txtOtherRef.Text.ToString().Trim();
                inHeader.Ith_entry_tp = txtAdjSubType.Text.ToString().Trim();
                inHeader.Ith_git_close = true;
                inHeader.Ith_git_close_date = DateTime.MinValue;
                inHeader.Ith_git_close_doc = string.Empty;
                inHeader.Ith_isprinted = false;
                inHeader.Ith_is_manual = false;
                inHeader.Ith_job_no = string.Empty;
                inHeader.Ith_loading_point = string.Empty;
                inHeader.Ith_loading_user = string.Empty;
                inHeader.Ith_loc = BaseCls.GlbUserDefLoca;
                inHeader.Ith_manual_ref = txtManualRef.Text.Trim();
                inHeader.Ith_mod_by = BaseCls.GlbUserID;
                inHeader.Ith_mod_when = DateTime.Now;
                inHeader.Ith_noofcopies = 0;
                inHeader.Ith_oth_loc = string.Empty;
                inHeader.Ith_oth_docno = "N/A";
                inHeader.Ith_remarks = txtRemarks.Text;
                //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
                inHeader.Ith_session_id = BaseCls.GlbUserSessionID;
                inHeader.Ith_stus = "A";
                //inHeader.Ith_sub_tp = "NOR";
                inHeader.Ith_sub_tp = txtAdjSubType.Text.ToString().Trim();
                inHeader.Ith_vehi_no = string.Empty;
                #endregion
                MasterAutoNumber masterAuto = new MasterAutoNumber();
                #region Fill MasterAutoNumber
                masterAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                masterAuto.Aut_cate_tp = "LOC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "ADJ";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "ADJ";
                masterAuto.Aut_year = null;
                #endregion

                #region Update some serial items
                if (_direction == 1)
                {
                    foreach (var _seritem in reptPickSerialsList)
                    {
                        _seritem.Tus_exist_grncom = BaseCls.GlbUserComCode;
                        // _seritem.Tus_exist_grndt = dtpDate.Value.Date;   //kapila commented on 3/7/2015 which this data will be added item wise
                        _seritem.Tus_orig_grncom = BaseCls.GlbUserComCode;
                        //_seritem.Tus_orig_grndt = dtpDate.Value.Date;  //kapila commented on 3/7/2015 which this data will be added item wise
                    }
                }
                #endregion

                #region Save Adj+ / Adj-
                if (_direction == 1) result = CHNLSVC.Inventory.ADJPlus(inHeader, reptPickSerialsList, reptPickSubSerialsList, masterAuto, out documntNo);
                else result = CHNLSVC.Inventory.ADJMinus(inHeader, reptPickSerialsList, reptPickSubSerialsList, masterAuto, out documntNo);

                if (result != -99 && result >= 0)
                {
                    Cursor.Current = Cursors.Default;
                    if (_iscostupdate == "N")
                    {
                        if (MessageBox.Show("Successfully Saved! Document No : " + documntNo + "\nDo you want to print this?", "Process Completed : " + ddlAdjType.SelectedItem.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                            if (_direction == 1) { BaseCls.GlbReportTp = "INWARD"; } else { BaseCls.GlbReportTp = "OUTWARD"; }//Sanjeewa
                            if (BaseCls.GlbUserComCode == "SGL") //Sanjeewa 2014-01-07
                            {
                                if (_direction == 1) _view.GlbReportName = "Inward_Docs.rpt";
                                else _view.GlbReportName = "Outward_Docs.rpt";
                            }
                            else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
                            {
                                if (_direction == 1) _view.GlbReportName = "Dealer_Inward_Docs.rpt";
                                else _view.GlbReportName = "Dealer_Outward_Docs.rpt";
                            }
                            else
                            {
                                if (_direction == 1) _view.GlbReportName = "Inward_Docs.rpt";
                                else _view.GlbReportName = "Outward_Docs.rpt";
                            }
                            _view.GlbReportDoc = documntNo;
                            _view.Show();
                            _view = null;
                        }
                    }
                    StockAdjustment _JobEntry = new StockAdjustment();
                    _JobEntry.MdiParent = this.MdiParent;
                    _JobEntry.Location = this.Location;
                    _JobEntry.GlbModuleName = this.GlbModuleName;
                    _JobEntry.Show();
                    this.Close();
                    this.Dispose();
                    GC.Collect();
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(documntNo, "Process Terminated : " + ddlAdjType.SelectedItem.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                #endregion
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        #endregion

        #region Rooting for Delete items/Serials

        protected void OnRemoveFromSerialGrid(int _rowIndex)
        {
            try
            {
                int row_id = _rowIndex;

                if (string.IsNullOrEmpty(gvSerial.Rows[row_id].Cells["ser_Item"].Value.ToString())) return;

                string _item = Convert.ToString(gvSerial.Rows[row_id].Cells["ser_Item"].Value);
                string _status = Convert.ToString(gvSerial.Rows[row_id].Cells["ser_Status"].Value);
                Int32 _serialID = Convert.ToInt32(gvSerial.Rows[row_id].Cells["ser_SerialID"].Value);
                string _bin = Convert.ToString(gvSerial.Rows[row_id].Cells["ser_Bin"].Value);
                string serial_1 = Convert.ToString(gvSerial.Rows[row_id].Cells["ser_Serial1"].Value);
                string _requestno = Convert.ToString(gvSerial.Rows[row_id].Cells["ser_requestno"].Value);

                MasterItem _masterItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);

                if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                {
                    //modify Rukshan 05/oct/2015 add two parameters
                    CHNLSVC.Inventory.Del_temp_pick_ser(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, Convert.ToInt32(txtUserSeqNo.Text), Convert.ToInt32(_serialID),null,null);
                    CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, _serialID, 1);
                }
                else
                {
                    CHNLSVC.Inventory.DeleteTempPickSerialByItem(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, Convert.ToInt32(txtUserSeqNo.Text), _item, _status);

                }
                LoadItems(txtUserSeqNo.Text);
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
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void OnRemoveFromItemGrid(int _rowIndex, int _colIndex)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int row_id = _rowIndex;
                if (string.IsNullOrEmpty(gvItems.Rows[row_id].Cells["itm_Item"].Value.ToString())) return;
                string _itemCode = Convert.ToString(gvItems.Rows[row_id].Cells["itm_Item"].Value);
                string _itemStatus = Convert.ToString(gvItems.Rows[row_id].Cells["itm_Status"].Value);
                decimal _itemCost = Convert.ToDecimal(gvItems.Rows[row_id].Cells["itri_unit_price"].Value);
                decimal _qty = Convert.ToDecimal(gvItems.Rows[row_id].Cells["itm_PickQty"].Value);
                int _lineNo = Convert.ToInt32(gvItems.Rows[row_id].Cells["itm_Lineno"].Value);

                CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), _itemCode, _itemStatus, _lineNo, 2);

                List<ReptPickSerials> _list = new List<ReptPickSerials>();
                _list = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, Convert.ToInt32(txtUserSeqNo.Text), "ADJ");
                if (_list != null)
                    if (_list.Count > 0)
                    {
                        if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
                        {
                            var _delete = (from _lst in _list
                                           where _lst.Tus_itm_cd == _itemCode && _lst.Tus_itm_stus == _itemStatus && _lst.Tus_itm_line == _lineNo
                                           select _lst).ToList();
                            foreach (ReptPickSerials _ser in _delete)
                            {
                                Delete_Serials(_itemCode, _itemStatus, _ser.Tus_ser_id);
                            }
                        }
                        else
                        {
                            var _delete = (from _lst in _list
                                           where _lst.Tus_itm_cd == _itemCode && _lst.Tus_itm_stus == _itemStatus && _lst.Tus_base_itm_line == _lineNo
                                           select _lst).ToList();

                            foreach (ReptPickSerials _ser in _delete)
                            {
                                Delete_Serials(_itemCode, _itemStatus, _ser.Tus_ser_id);
                            }
                        }
                    }

                //ScanItemList.RemoveAll(x => x.Itri_itm_cd == _item && x.Itri_itm_stus == _itmStatus);
                LoadItems(txtUserSeqNo.Text);
                this.Cursor = Cursors.Default;
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
                CHNLSVC.CloseAllChannels();
            }
        }

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
        #endregion

        private void gvSerial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                                    OnRemoveFromSerialGrid(_rowCount);
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    ClearItemDetail();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtDocumentNo_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_DocumentNo_Click(null, null);
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(txtDocumentNo.Text))
                {
                    int _direction = 0;
                    if (ddlAdjTypeSearch.Text.ToString() == "ADJ+") _direction = 1;
                    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    if (_direction == 1) { BaseCls.GlbReportTp = "INWARD"; } else { BaseCls.GlbReportTp = "OUTWARD"; }//Sanjeewa
                    if (BaseCls.GlbUserComCode == "SGL") //Sanjeewa 2014-01-07
                    {
                        if (_direction == 1) _view.GlbReportName = "Inward_Docs.rpt";
                        else _view.GlbReportName = "Outward_Docs.rpt";
                    }
                    if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
                    {
                        if (_direction == 1) _view.GlbReportName = "Dealer_Inward_Docs.rpt";
                        else _view.GlbReportName = "Dealer_Outward_Docs.rpt";
                    }
                    else
                    {
                        if (_direction == 1) _view.GlbReportName = "Inward_Docs.rpt";
                        else _view.GlbReportName = "Outward_Docs.rpt";
                    }
                    _view.GlbReportDoc = txtDocumentNo.Text.ToString();
                    _view.Show();
                    _view = null;
                }
                Cursor.Current = Cursors.Default;
                StockAdjustment _JobEntry = new StockAdjustment();
                _JobEntry.MdiParent = this.MdiParent;
                _JobEntry.Location = this.Location;
                _JobEntry.GlbModuleName = this.GlbModuleName;
                _JobEntry.Show();
                this.Close();
                this.Dispose();
                GC.Collect();
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

        private void btnAddItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    ClearItemDetail();
                    txtItem.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void StockAdjustment_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, dtpDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
                //Added by Prabhath on 17/12/2013 ************* start **************
                LoadAFSL();
                //Added by Prabhath on 17/12/2013 ************* end **************
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btn_Srch_Sup_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
            DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSup;
            _CommonSearch.ShowDialog();
            txtSup.Select();
            load_sup();
        }

        private void load_sup()
        {
            List<MasterBusinessEntity> _custList = new List<MasterBusinessEntity>();
            if (!string.IsNullOrEmpty(txtSup.Text))
            {
                _custList = CHNLSVC.Sales.GetCustomerDetailList(BaseCls.GlbUserComCode, txtSup.Text, string.Empty, string.Empty, "S");
                if (_custList == null || _custList.Count == 0)
                {
                    MessageBox.Show("Invalid supplier", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSup.Text = "";
                    lblSup.Text = "";
                    txtSup.Focus();
                }
                else
                {
                    lblSup.Text = _custList[0].Mbe_name.ToString();
                }
            }
        }
        private void txtSup_Leave(object sender, EventArgs e)
        {
            load_sup();
        }

        private void btn_srch_grn_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = new DataTable();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNNo);
            _result = CHNLSVC.CommonSearch.searchGRNData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtGRNno;
            _CommonSearch.ShowDialog();
            txtGRNno.Select();

            load_grn_det();
        }

        private void load_grn_det()
        {
            InventoryHeader _invH = new InventoryHeader();
            _invH = CHNLSVC.Inventory.Get_Int_Hdr(txtGRNno.Text);
            if (_invH != null)
            {
                dtGRN.Value = Convert.ToDateTime(_invH.Ith_doc_date);
            }
        }

        private void txtSup_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Sup_Click(null, null);
        }

        private void txtSup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_Srch_Sup_Click(null, null);
        }

        private void txtGRNno_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_grn_Click(null, null);
        }

        private void txtGRNno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_grn_Click(null, null);
        }

        private void btnBrItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "Excel files(*.xls)|*.xls,*.xlsx|All files(*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            txtUploadItems.Text = openFileDialog1.FileName;
        }

        private void btn_UploadItems_Click(object sender, EventArgs e)
        {
            //Sanjeewa 2016-02-29
            try
            {
                string _msg = string.Empty;
                StringBuilder _errorLst = new StringBuilder();
                
                if (string.IsNullOrEmpty(txtUploadItems.Text))
                {
                    MessageBox.Show("Please select upload file path.", "Adjustmwnt Items Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUploadItems.Text = "";
                    txtUploadItems.Focus();
                    return;
                }

                System.IO.FileInfo fileObj = new System.IO.FileInfo(txtUploadItems.Text);

                if (fileObj.Exists == false)
                {
                    MessageBox.Show("Selected file does not exist at the following path.", "Adjustmwnt Items Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUploadItems.Focus();
                }

                string Extension = fileObj.Extension;

                string conStr = "";

                if (Extension.ToUpper() == ".XLS")
                {

                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                             .ConnectionString;
                }
                else if (Extension.ToUpper() == ".XLSX")
                {
                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                              .ConnectionString;
                }

                conStr = String.Format(conStr, txtUploadItems.Text, "NO");
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;

                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();
                                
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow _dr in dt.Rows)
                    {
                        if (string.IsNullOrEmpty(_dr[0].ToString()))
                        {
                            continue;
                        }

                        txtItem.Text = _dr[0].ToString();
                        ddlStatus.Text = _dr[1].ToString();
                        txtUnitCost.Text = _dr[2].ToString();
                        txtQty.Text = _dr[3].ToString() ;

                        txtSup.Text = _dr[4].ToString();
                        txtBatch.Text = _dr[5].ToString();
                        txtGRNno.Text = _dr[6].ToString();
                        dtGRN.Value = Convert.ToDateTime(_dr[7].ToString());

                        AddItem();
                    }
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show("Unable to upload. " + err.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnBulkUpload_Click(object sender, EventArgs e)
        {
            pnl_Itemupload.Visible = true;
            txtUploadItems.Text = "";
        }

        private void btn_closeItemupload_Click(object sender, EventArgs e)
        {
            pnl_Itemupload.Visible = false;
        }

        private void btn_costupdate_Click(object sender, EventArgs e)
        {
            int _direction = 0;
            string _seqNo = "";
            Int32 user_seq_num = -1;
            _iscostupdate = "Y";
            _chargeType = "FOC";

            DataTable _costdtl = CHNLSVC.Inventory.get_CostUpdateDetails();
            DataTable _costdtl_loc = _costdtl.DefaultView.ToTable(true, "loc_code");

            if (_costdtl_loc.Rows.Count > 0)
            {
                foreach (DataRow drow in _costdtl_loc.Rows)
                {                    
                    BaseCls.GlbUserDefLoca=drow["loc_code"].ToString();

                    //var _costdtl_filterloc = (from tem in _costdtl.AsEnumerable()
                    //             where (tem["loc_code"].ToString() == drow["loc_code"].ToString())
                    //                          group tem by new { LOC = tem["loc_code"], LOCDESC = tem["loc_desc"], ITEMCODE = tem["item_code"], ITEMSTATUS = tem["item_status"], ITEMCOST = tem["unit_cost"], SUPPCODE = tem["org_supp"], GRNNO = tem["grn_no"], GRNDT = tem["grn_dt"] } into ss
                    //             select new
                    //             {
                    //                 LOC_CODE = ss.Key.LOC,
                    //                 LOC_DESC = ss.Key.LOCDESC,
                    //                 ITEM_CODE = ss.Key.ITEMCODE,
                    //                 ITEM_STATUS = ss.Key.ITEMSTATUS,
                    //                 ITEM_COST = ss.Key.ITEMCOST,
                    //                 SUPP_CODE = ss.Key.SUPPCODE,
                    //                 GRN_NO = ss.Key.GRNNO,
                    //                 GRN_DT = ss.Key.GRNDT,
                    //                 ITEM_QTY = ss.Sum(tem => Convert.ToDecimal(tem["qty"]))                                     
                    //             }).ToList();

                    var _costdtl_filterloc = (from tem in _costdtl.AsEnumerable()
                                              where (tem["loc_code"].ToString() == drow["loc_code"].ToString())
                                              group tem by new { LOC = tem["loc_code"], LOCDESC = tem["loc_desc"], ITEMCODE = tem["item_code"], ITEMSTATUS = tem["item_status"], ITEMCOST = tem["unit_cost"] } into ss
                                              select new
                                              {
                                                  LOC_CODE = ss.Key.LOC,
                                                  LOC_DESC = ss.Key.LOCDESC,
                                                  ITEM_CODE = ss.Key.ITEMCODE,
                                                  ITEM_STATUS = ss.Key.ITEMSTATUS,
                                                  ITEM_COST = ss.Key.ITEMCOST,                                                  
                                                  ITEM_QTY = ss.Sum(tem => Convert.ToDecimal(tem["qty"]))
                                              }).ToList();

                    //ADJ Out -----------------------------------
                    ScanItemList.Clear();
                    dtpDate.Value = Convert.ToDateTime("15/SEP/2018").Date;
                    _direction = 0;
                    _seqNo = txtUserSeqNo.Text.ToString();
                    ddlAdjType.Text = "ADJ-";
                    txtAdjSubType.Text = "SYSER";
                    txtRemarks.Text = "System Issue Corrected (Jobs completed, items not delivered) - 2018-09-15";

                    user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "ADJ", BaseCls.GlbUserID, _direction, _seqNo);
                    if (user_seq_num == -1)
                    {
                        user_seq_num = GenerateNewUserSeqNo();
                    }

                    for (int i = 0; i < _costdtl_filterloc.Count; i++)
                    {
                        txtItem.Text = _costdtl_filterloc[i].ITEM_CODE.ToString();
                        //txtSup.Text = _costdtl_filterloc[i].SUPP_CODE.ToString();
                        ddlStatus.SelectedValue = _costdtl_filterloc[i].ITEM_STATUS.ToString();
                        txtUnitCost.Text = _costdtl_filterloc[i].ITEM_COST.ToString();
                        txtQty.Text = _costdtl_filterloc[i].ITEM_QTY.ToString();                        
                        txtBatch.Text = "";
                        RequestNo = "";
                        //txtGRNno.Text = _costdtl_filterloc[i].GRN_NO.ToString();
                        //dtGRN.Value = Convert.ToDateTime(_costdtl_filterloc[i].GRN_DT.ToString());
                            
                        AddItem();

                        //var _costdtl_serails = _costdtl.AsEnumerable().Where(X => X.Field<string>("loc_code") == drow["loc_code"].ToString() && X.Field<string>("item_code") == _costdtl_filterloc[i].ITEM_CODE.ToString() && X.Field<string>("item_status") == _costdtl_filterloc[i].ITEM_STATUS.ToString() &&
                        //    X.Field<decimal>("unit_cost") == Convert.ToDecimal(_costdtl_filterloc[i].ITEM_COST.ToString()) && X.Field<string>("org_supp") == _costdtl_filterloc[i].SUPP_CODE.ToString() && X.Field<string>("grn_no") == _costdtl_filterloc[i].GRN_NO.ToString() && X.Field<DateTime>("grn_dt").Date == Convert.ToDateTime(_costdtl_filterloc[i].GRN_DT.ToString()).Date).Select(s => new
                        //{
                        //    loc_code = s.Field<string>("loc_code"),
                        //    loc_desc = s.Field<string>("loc_desc"),
                        //    item_code = s.Field<string>("item_code"),
                        //    unit_cost = s.Field<decimal>("unit_cost"),
                        //    new_unit_cost = s.Field<decimal>("new_unit_cost"),
                        //    ser_id = s.Field<decimal>("ser_id"),
                        //    ser_no = s.Field<string>("ser_no"),
                        //    qty = s.Field<decimal>("qty"),
                        //    grn_dt = s.Field<DateTime>("grn_dt"),
                        //    item_status = s.Field<string>("item_status"),
                        //    org_supp = s.Field<string>("org_supp"),
                        //    exist_supp = s.Field<string>("exist_supp"),
                        //    ins_bin = s.Field<string>("ins_bin"),
                        //    doc_no = s.Field<string>("doc_no"),
                        //    doc_line = s.Field<decimal>("doc_line")
                        //}).Distinct().ToList();

                        var _costdtl_serails = _costdtl.AsEnumerable().Where(X => X.Field<string>("loc_code") == drow["loc_code"].ToString() && X.Field<string>("item_code") == _costdtl_filterloc[i].ITEM_CODE.ToString() && X.Field<string>("item_status") == _costdtl_filterloc[i].ITEM_STATUS.ToString() &&
                            X.Field<decimal>("unit_cost") == Convert.ToDecimal(_costdtl_filterloc[i].ITEM_COST.ToString())).Select(s => new
                            {
                                loc_code = s.Field<string>("loc_code"),
                                loc_desc = s.Field<string>("loc_desc"),
                                item_code = s.Field<string>("item_code"),
                                unit_cost = s.Field<decimal>("unit_cost"),
                                new_unit_cost = s.Field<decimal>("new_unit_cost"),
                                ser_id = s.Field<decimal>("ser_id"),
                                ser_no = s.Field<string>("ser_no"),
                                qty = s.Field<decimal>("qty"),
                                grn_dt = s.Field<DateTime>("grn_dt"),
                                item_status = s.Field<string>("item_status"),
                                org_supp = s.Field<string>("org_supp"),
                                exist_supp = s.Field<string>("exist_supp"),
                                ins_bin = s.Field<string>("ins_bin"),
                                doc_no = s.Field<string>("doc_no"),
                                doc_line = s.Field<decimal>("doc_line")
                            }).Distinct().ToList();

                        for (int j = 0; j < _costdtl_serails.Count; j++)
                        {
                            MasterItem _itms = new MasterItem();
                            _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _costdtl_serails[j].item_code.ToString());
                            if (_itms.Mi_is_ser1 != -1)
                            {
                                int rowCount = 0;

                                Int32 serID = Convert.ToInt32(_costdtl_serails[j].ser_id.ToString());
                                string binCode = _costdtl_serails[j].ins_bin.ToString();

                                ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, binCode, _costdtl_serails[j].item_code.ToString(), serID);
                                //Update_inrser_INS_AVAILABLE
                                Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _costdtl_serails[j].item_code.ToString(), serID, -1);

                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial_.Tus_usrseq_no = user_seq_num;
                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial_.Tus_base_doc_no = _costdtl_serails[j].doc_no.ToString();
                                _reptPickSerial_.Tus_base_itm_line =i+1;
                                _reptPickSerial_.Tus_itm_desc = _itms.Mi_shortdesc;
                                _reptPickSerial_.Tus_itm_model = _itms.Mi_model;
                                _reptPickSerial_.Tus_job_no = "";
                                _reptPickSerial_.Tus_pgs_prefix = "";
                                _reptPickSerial_.Tus_job_line = 0;

                                Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

                                rowCount++;

                            }
                            else
                            {
                                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial_.Tus_usrseq_no = user_seq_num;
                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial_.Tus_base_doc_no = user_seq_num.ToString();// _costdtl_serails[j].doc_no.ToString();
                                _reptPickSerial_.Tus_base_itm_line = i+1;
                                _reptPickSerial_.Tus_itm_desc = _itms.Mi_shortdesc;
                                _reptPickSerial_.Tus_itm_model = _itms.Mi_model;
                                _reptPickSerial_.Tus_com = BaseCls.GlbUserComCode;
                                _reptPickSerial_.Tus_loc = BaseCls.GlbUserDefLoca;
                                _reptPickSerial_.Tus_bin = BaseCls.GlbDefaultBin;
                                _reptPickSerial_.Tus_itm_cd = _costdtl_filterloc[i].ITEM_CODE.ToString();
                                _reptPickSerial_.Tus_itm_stus = _costdtl_filterloc[i].ITEM_STATUS.ToString();
                                _reptPickSerial_.Tus_qty = Convert.ToDecimal(_costdtl_serails[j].qty.ToString());
                                _reptPickSerial_.Tus_ser_1 = "N/A";
                                _reptPickSerial_.Tus_ser_2 = "N/A";
                                _reptPickSerial_.Tus_ser_3 = "N/A";
                                _reptPickSerial_.Tus_ser_4 = "N/A";
                                _reptPickSerial_.Tus_ser_id = 0;
                                _reptPickSerial_.Tus_serial_id = "0";
                                _reptPickSerial_.Tus_unit_cost = Convert.ToDecimal(_costdtl_filterloc[i].ITEM_COST.ToString());
                                _reptPickSerial_.Tus_unit_price = 0;
                                _reptPickSerial_.Tus_unit_price = 0;
                                _reptPickSerial_.Tus_job_no = "";
                                _reptPickSerial_.Tus_pgs_prefix = "";
                                _reptPickSerial_.Tus_job_line = 0;
                                //enter row into TEMP_PICK_SER
                                Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                            }
                        }
                    }

                    List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                    List<ReptPickItems> _reptItems = new List<ReptPickItems>();
                    _reptItems = CHNLSVC.Inventory.GetAllScanRequestItemsList(user_seq_num);

                    foreach (ReptPickItems _reptitem in _reptItems)
                    {
                        InventoryRequestItem _itm = new InventoryRequestItem();
                        MasterItem _itms = new MasterItem();
                        _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _reptitem.Tui_req_itm_cd);
                        _itm.Itri_app_qty = _reptitem.Tui_req_itm_qty;
                        _itm.Itri_itm_cd = _reptitem.Tui_req_itm_cd;
                        _itm.Itri_itm_stus = _reptitem.Tui_req_itm_stus;
                        _itm.Itri_line_no = Convert.ToInt32(_reptitem.Tui_pic_itm_cd.ToString());
                        _itm.Itri_qty = 0;
                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        _itm.Itri_unit_price = Convert.ToDecimal(_reptitem.Tui_pic_itm_stus.ToString());
                        _itm.Itri_supplier = _reptitem.Tui_sup;
                        _itm.Itri_batchno = _reptitem.Tui_batch;
                        _itm.Itri_grnno = _reptitem.Tui_grn;
                        _itm.Itri_grndate = _reptitem.Tui_grn_dt;
                        _itm.Itri_expdate = _reptitem.Tui_exp_dt;
                        _itmList.Add(_itm);                        
                    }

                    ScanItemList = _itmList.OrderBy(o => o.Itri_line_no).ToList(); ;
                    gvItems.AutoGenerateColumns = false;
                    gvItems.DataSource = ScanItemList;

                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "ADJ");
                    if (_serList != null)
                    {
                        if (_direction == 0)
                        {
                            //var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                            var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                            foreach (var itm in _scanItems)
                            {
                                foreach (DataGridViewRow row in this.gvItems.Rows)
                                {
                                    if (itm.Peo.Tus_itm_cd == row.Cells["itm_Item"].Value.ToString() && itm.Peo.Tus_base_itm_line == Convert.ToInt32(row.Cells["itm_Lineno"].Value.ToString()))
                                    {
                                        row.Cells["itm_PickQty"].Value = Convert.ToDecimal(itm.theCount); // Current scan qty
                                    }
                                }
                            }
                        }
                        else
                        {
                            var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                            foreach (var itm in _scanItems)
                            {
                                foreach (DataGridViewRow row in this.gvItems.Rows)
                                {
                                    if (itm.Peo.Tus_itm_cd == row.Cells["itm_Item"].Value.ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(row.Cells["itm_Lineno"].Value.ToString()))
                                    {
                                        row.Cells["itm_PickQty"].Value = Convert.ToDecimal(itm.theCount); // Current scan qty
                                    }
                                }
                            }
                        }
                        gvSerial.AutoGenerateColumns = false;
                        gvSerial.DataSource = _serList;

                    }
                    else
                    {
                        List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                        gvSerial.AutoGenerateColumns = false;
                        gvSerial.DataSource = emptyGridList;

                    }
                    btnSave_Click(null, null);

                    //ADJ In -----------------------------------
                    //_costdtl_filterloc1.Clear();
                    ScanItemList.Clear();

                   

                    //var _costdtl_filterloc1 = (from tem in _costdtl.AsEnumerable()
                    //                          where (tem["loc_code"].ToString() == drow["loc_code"].ToString())
                    //                      group tem by new { LOC = tem["loc_code"], LOCDESC = tem["loc_desc"], ITEMCODE = tem["item_code"], ITEMSTATUS = tem["item_status"], ITEMCOST = tem["new_unit_cost"], SUPPCODE = tem["org_supp"], GRNNO = tem["grn_no"], GRNDT = tem["grn_dt"] } into ss
                    //                          select new
                    //                          {
                    //                              LOC_CODE = ss.Key.LOC,
                    //                              LOC_DESC = ss.Key.LOCDESC,
                    //                              ITEM_CODE = ss.Key.ITEMCODE,
                    //                              ITEM_STATUS = ss.Key.ITEMSTATUS,
                    //                              ITEM_COST = ss.Key.ITEMCOST,
                    //                              SUPP_CODE = ss.Key.SUPPCODE,
                    //                              GRN_NO = ss.Key.GRNNO,
                    //                              GRN_DT = ss.Key.GRNDT,
                    //                              ITEM_QTY = ss.Sum(tem => Convert.ToDecimal(tem["qty"]))
                    //                          }).ToList();

                    //dtpDate.Value = Convert.ToDateTime("31/JUL/2016").Date;
                    //_direction = 1;
                    //_seqNo = txtUserSeqNo.Text.ToString();
                    //ddlAdjType.Text = "ADJ+";
                    //txtAdjSubType.Text = "DEVALUE";
                    //txtRemarks.Text = "De Value - Monday, July 04, 2016 4:43 PM Age 270";

                    //user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "ADJ", BaseCls.GlbUserID, _direction, _seqNo);
                    //if (user_seq_num == -1)
                    //{
                    //    user_seq_num = GenerateNewUserSeqNo();
                    //}

                    //for (int i = 0; i < _costdtl_filterloc1.Count; i++)
                    //{
                    //    txtItem.Text = _costdtl_filterloc1[i].ITEM_CODE.ToString();
                    //    txtSup.Text = _costdtl_filterloc1[i].SUPP_CODE.ToString();
                    //    ddlStatus.SelectedValue = _costdtl_filterloc1[i].ITEM_STATUS.ToString();
                    //    txtUnitCost.Text = _costdtl_filterloc1[i].ITEM_COST.ToString();
                    //    txtQty.Text = _costdtl_filterloc1[i].ITEM_QTY.ToString();                        
                    //    txtBatch.Text = "";
                    //    txtGRNno.Text = _costdtl_filterloc1[i].GRN_NO.ToString();
                    //    dtGRN.Value = Convert.ToDateTime(_costdtl_filterloc1[i].GRN_DT.ToString());

                    //    AddItem();
                    //    //&& X.Field<DateTime>("grn_dt").Date == Convert.ToDateTime(_costdtl_filterloc[i].GRN_DT.ToString()).Date
                    //    var _costdtl_serails = _costdtl.AsEnumerable().Where(X => X.Field<string>("loc_code") == drow["loc_code"].ToString() && X.Field<string>("item_code") == _costdtl_filterloc1[i].ITEM_CODE.ToString() && X.Field<string>("item_status") == _costdtl_filterloc1[i].ITEM_STATUS.ToString() &&
                    //        X.Field<decimal>("new_unit_cost") == Convert.ToDecimal(_costdtl_filterloc1[i].ITEM_COST.ToString()) && X.Field<string>("org_supp") == _costdtl_filterloc1[i].SUPP_CODE.ToString() && X.Field<string>("grn_no") == _costdtl_filterloc1[i].GRN_NO.ToString() && X.Field<DateTime>("grn_dt").Date == Convert.ToDateTime(_costdtl_filterloc1[i].GRN_DT.ToString()).Date).Select(s => new
                    //    {
                    //        loc_code = s.Field<string>("loc_code"),
                    //        loc_desc = s.Field<string>("loc_desc"),
                    //        item_code = s.Field<string>("item_code"),
                    //        unit_cost = s.Field<decimal>("unit_cost"),
                    //        new_unit_cost = s.Field<decimal>("new_unit_cost"),
                    //        ser_id = s.Field<decimal>("ser_id"),
                    //        ser_no = s.Field<string>("ser_no"),
                    //        ser_no2 = s.Field<string>("ser_no2"),
                    //        qty = s.Field<decimal>("qty"),
                    //        grn_dt = s.Field<DateTime>("grn_dt"),
                    //        grn_no = s.Field<string>("grn_no"),
                    //        item_status = s.Field<string>("item_status"),
                    //        org_supp = s.Field<string>("org_supp"),
                    //        exist_supp = s.Field<string>("exist_supp"),
                    //        ins_bin = s.Field<string>("ins_bin"),
                    //        doc_no = s.Field<string>("doc_no"),
                    //        doc_line = s.Field<decimal>("doc_line")
                    //    }).Distinct().ToList();

                    //    for (int j = 0; j < _costdtl_serails.Count; j++)
                    //    {
                    //        MasterItem _itms = new MasterItem();
                    //        _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _costdtl_serails[j].item_code.ToString());
                    //        //if (_itms.Mi_is_ser1 != -1)
                    //        //{
                    //        //    int rowCount = 0;

                    //        //    Int32 serID = Convert.ToInt32(_costdtl_serails[j].ser_id.ToString());
                    //        //    string binCode = _costdtl_serails[j].ins_bin.ToString();

                    //        //    ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, binCode, _costdtl_serails[j].item_code.ToString(), serID);
                    //        //    //Update_inrser_INS_AVAILABLE
                    //        //    Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _costdtl_serails[j].item_code.ToString(), serID, -1);

                    //        //    _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                    //        //    _reptPickSerial_.Tus_usrseq_no = user_seq_num;
                    //        //    _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                    //        //    _reptPickSerial_.Tus_base_doc_no = _costdtl_serails[j].doc_no.ToString();
                    //        //    _reptPickSerial_.Tus_base_itm_line = i;
                    //        //    _reptPickSerial_.Tus_itm_desc = _itms.Mi_shortdesc;
                    //        //    _reptPickSerial_.Tus_itm_model = _itms.Mi_model;
                    //        //    _reptPickSerial_.Tus_job_no = "";
                    //        //    _reptPickSerial_.Tus_pgs_prefix = "";
                    //        //    _reptPickSerial_.Tus_job_line = 0;

                    //        //    Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

                    //        //    rowCount++;

                    //        //}
                    //        //else
                    //        //{
                    //            ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                    //            _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                    //            _reptPickSerial_.Tus_usrseq_no = user_seq_num;
                    //            _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                    //            _reptPickSerial_.Tus_base_doc_no = _costdtl_serails[j].doc_no.ToString();
                    //            _reptPickSerial_.Tus_base_itm_line = i+1;
                    //            _reptPickSerial_.Tus_itm_desc = _itms.Mi_shortdesc;
                    //            _reptPickSerial_.Tus_itm_model = _itms.Mi_model;
                    //            _reptPickSerial_.Tus_com = BaseCls.GlbUserComCode;
                    //            _reptPickSerial_.Tus_loc = BaseCls.GlbUserDefLoca;
                    //            _reptPickSerial_.Tus_bin = _costdtl_serails[j].ins_bin.ToString();
                    //            _reptPickSerial_.Tus_itm_cd = _costdtl_serails[j].item_code.ToString();
                    //            _reptPickSerial_.Tus_itm_stus = _costdtl_serails[j].item_status.ToString();
                    //            _reptPickSerial_.Tus_qty = Convert.ToDecimal(_costdtl_serails[j].qty.ToString());
                    //            _reptPickSerial_.Tus_ser_1 = _costdtl_serails[j].ser_no.ToString();
                    //            _reptPickSerial_.Tus_ser_2 = _costdtl_serails[j].ser_no2.ToString(); ;
                    //            _reptPickSerial_.Tus_ser_3 = "N/A";
                    //            _reptPickSerial_.Tus_ser_4 = "N/A";
                    //            _reptPickSerial_.Tus_ser_id = Convert.ToInt32(_costdtl_serails[j].ser_id.ToString()); ;
                    //            _reptPickSerial_.Tus_serial_id = "0";
                    //            _reptPickSerial_.Tus_unit_cost = Convert.ToDecimal(_costdtl_serails[j].new_unit_cost.ToString());
                    //            _reptPickSerial_.Tus_unit_price = 0;
                    //            _reptPickSerial_.Tus_unit_price = 0;
                    //            _reptPickSerial_.Tus_job_no = "";
                    //            _reptPickSerial_.Tus_pgs_prefix = "";
                    //            _reptPickSerial_.Tus_job_line = 0;
                    //            _reptPickSerial_.Tus_itm_line = i+1;
                    //            _reptPickSerial_.Tus_exist_grndt = Convert.ToDateTime(_costdtl_serails[j].grn_dt.ToString()).Date;
                    //            _reptPickSerial_.Tus_exist_grnno = _costdtl_serails[j].grn_no.ToString();
                    //            _reptPickSerial_.Tus_orig_grndt = Convert.ToDateTime(_costdtl_serails[j].grn_dt.ToString()).Date;
                    //            _reptPickSerial_.Tus_orig_grnno = _costdtl_serails[j].grn_no.ToString();
                    //            //enter row into TEMP_PICK_SER
                    //            Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                    //        //}
                    //    }
                    //}

                    //_itmList = new List<InventoryRequestItem>();
                    //_reptItems = new List<ReptPickItems>();
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
                    //    _itm.Itri_qty = 0;
                    //    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    //    _itm.Mi_model = _itms.Mi_model;
                    //    _itm.Mi_brand = _itms.Mi_brand;
                    //    _itm.Itri_unit_price = Convert.ToDecimal(_reptitem.Tui_pic_itm_stus.ToString());
                    //    _itm.Itri_supplier = _reptitem.Tui_sup;
                    //    _itm.Itri_batchno = _reptitem.Tui_batch;
                    //    _itm.Itri_grnno = _reptitem.Tui_grn;
                    //    _itm.Itri_grndate = _reptitem.Tui_grn_dt;
                    //    _itm.Itri_expdate = _reptitem.Tui_exp_dt;
                    //    _itmList.Add(_itm);
                        
                    //}

                    //ScanItemList = _itmList.OrderBy(o => o.Itri_line_no).ToList(); ;
                    //gvItems.AutoGenerateColumns = false;
                    //gvItems.DataSource = ScanItemList;

                    //_serList = new List<ReptPickSerials>();
                    //_serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "ADJ");
                    //if (_serList != null)
                    //{
                    //    if (_direction == 0)
                    //    {
                    //        //var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    //        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                    //        foreach (var itm in _scanItems)
                    //        {
                    //            foreach (DataGridViewRow row in this.gvItems.Rows)
                    //            {
                    //                if (itm.Peo.Tus_itm_cd == row.Cells["itm_Item"].Value.ToString() && itm.Peo.Tus_base_itm_line == Convert.ToInt32(row.Cells["itm_Lineno"].Value.ToString()))
                    //                {
                    //                    row.Cells["itm_PickQty"].Value = Convert.ToDecimal(itm.theCount); // Current scan qty
                    //                }
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                    //        foreach (var itm in _scanItems)
                    //        {
                    //            foreach (DataGridViewRow row in this.gvItems.Rows)
                    //            {
                    //                if (itm.Peo.Tus_itm_cd == row.Cells["itm_Item"].Value.ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(row.Cells["itm_Lineno"].Value.ToString()))
                    //                {
                    //                    row.Cells["itm_PickQty"].Value = Convert.ToDecimal(itm.theCount); // Current scan qty
                    //                }
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

                    //btnSave_Click(null, null);

                }

            }
            MessageBox.Show("Completed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void StockAdjustment_FormClosed(object sender, FormClosedEventArgs e)
        {
            int _eff = 0;
            if (!string.IsNullOrEmpty(txtUserSeqNo.Text))
                _eff = CHNLSVC.Inventory.Delete_rept_Pick_Ser(Convert.ToInt32( txtUserSeqNo.Text));
        }


    }
}
